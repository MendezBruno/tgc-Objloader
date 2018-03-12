using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TGC.Core.SceneLoader;
using TGC.Group.Model.ParserStrategy;

namespace TGC.Group.Model
{
    public class TgcObjLoader
    {
        public TgcObjLoader()
        {
            Strategies = new List<ObjParseStrategy>();
            Strategies.Add(new AddShadowStrategy());
            Strategies.Add(new AddUsemtlStrategy());
            Strategies.Add(new CreateNewMeshStrategy());
            Strategies.Add(new CreateNormalStrategy());
            Strategies.Add(new CreateFaceStrategy());
            Strategies.Add(new CreateTextCoordStrategy());
            Strategies.Add(new CreateVertexStrategy());
            Strategies.Add(new NoOperationStrategy());
            ListObjMesh = new List<ObjMesh>();
            ListMtllib = new List<string>();
            MeshBuilder = new MeshBuilder();
            ObjMaterialsLoader = new ObjMaterialsLoader();

        }

        internal const string MTLLIB = "mtllib";
        private const int INDEXATTR = 2;
        private const int ELEMENTOFMTLLIB = 2;
        private const int INICIO = 0;
        public List<ObjParseStrategy> Strategies { get; set; }

        public ObjMaterialsLoader ObjMaterialsLoader;
        public List<ObjMesh> ListObjMesh { get; set; }
        public List<string> ListMtllib { get; set; }
        public MeshBuilder MeshBuilder { get; set; }

        public string GetPathObjforCurrentDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), @"UnitTestProjectObj\DatosPrueba\cubo.obj");
        }

        public void LoadObjFromFile(string path)
        {
            //Se leen todas las lineas
            var lines = File.ReadAllLines(path);
            //Se recolectan los materiales
            GetListOfMaterials(lines, path);
             //Se Parsea de los objetos
            foreach (var line in lines)
                ProccesLine(line);
        }

        public void ProccesLine(string line)
        {
            var action = line.Split(' ').FirstOrDefault();
            if (action == null) throw new InvalidOperationException($"Cannot find action for this line {line}");

            foreach (var strategy in Strategies)
                if (strategy.ResponseTo(action))
                {
                    strategy.ProccesLine(line, ListObjMesh);
                    return;
                }
            throw new InvalidOperationException($"Cannot find a correct parsing process for line {line}");
        }

        public void GetListOfMaterials(string[] lines, string path)
        {
            //se obtiene puramente todas las lineas que son de material
            var linesFiltered = FilterByKeyword(lines, MTLLIB);
            //nos fijamos si tiene material
            if (linesFiltered.Length == 0) return;
            //agregamos la ruta de la carpeta donde se encuentran los materiales
            ObjMaterialsLoader.SetDirectoryPathMaterial(path);
            //
            foreach (var line in linesFiltered)
                SetMtllib(line);
            //Se hace parse de los materiales
            ObjMaterialsLoader.LoadMaterialsFromFiles(path, ListMtllib);  //TODO ver si devuelve una lista de materiales o le pasamos el objmesh como parametro
           // MeshBuilder.AddMaterials(ObjMaterialsLoader);
        }

        private void SetMtllib(string line)
        {
            string[] splitLine = line.Split(' ');
            if (splitLine.Length < ELEMENTOFMTLLIB) throw new ArgumentException("El atributo Mtllib tiene formato incorrecto");
            var attribute = splitLine.Last(); //TODO ver que pasa con los nombres de textura que tienen espacio
            if (!Path.GetExtension(attribute).Equals(".mtl"))
            {
                throw new ArgumentException("La extención de Mtllib es incorrecta, se esperaba: .mtl y se obtuvo: " + Path.GetExtension(attribute));
            }
            ListMtllib.Add(attribute);
        }

        public string[] FilterByKeyword(string[] lines, string keyWord)
        {
            List<string> linesWithKeyword = new List<string>();

            foreach (string line in lines)
            {
                if (line.Split(' ').FirstOrDefault().Equals(keyWord))
                    linesWithKeyword.Add(line);
            }

            return linesWithKeyword.ToArray();
        }

        public TgcMesh LoadTgcMeshFromObj(string fullobjpath, int index)
        {
            LoadObjFromFile(fullobjpath);
            ObjMesh objMesh = ListObjMesh[index];
            MeshBuilder.AddMaterials(ObjMaterialsLoader)
                        .AddDxMesh(objMesh.FaceTrianglesList.Count)
                        .chargeBuffer(objMesh)
                        .SetEnable(true)
                        .SetAutotransform(true)
                        .SetHasBoundingBox(false)
                        .build(objMesh);


            return MeshBuilder.build(ListObjMesh[index]); //TODO pasarle el name no mas
        }
    }
}