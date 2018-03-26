using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace TGC.Group.Model
{
    public class ObjMaterialsLoader
    {
        private const string Separador = "\\";

        public ObjMaterialsLoader()
        {
            Strategies = new List<ObjMaterialsParseStrategy>();
            Strategies.Add(new CreateNewMaterialStrategy());
            Strategies.Add(new ParseMaterialAndColorStrategy());
            Strategies.Add(new NoOperationStrategyForMaterial());
            ListObjMaterialMesh = new List<ObjMaterialMesh>();
        }

        public List<ObjMaterialsParseStrategy> Strategies { get; set; }
        public List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }
        public string currentDirectory { get; set; }

        public void LoadMaterialsFromFiles(string pathMtllib, List<string> listMtllib)
        {
            foreach (string mtllib in listMtllib)
            {
                string pathMaterial = Path.GetDirectoryName(pathMtllib) + Separador + mtllib;
                if (File.Exists(pathMaterial))
                {
                    ParseMtlLib(pathMaterial);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot find file: {mtllib}");
                }
            }
        }

        public string GetPathMaterial(string pathMtllib, string mtllib)
        {
            return currentDirectory + Separador + mtllib;
        }

        private void ParseMtlLib(string path)
        {
            //Se leen todas las lineas
            var lines = File.ReadAllLines(path);

            //Parse de las sentencias
            foreach (var line in lines)
                ProccesLine(line);
        }

        private void ProccesLine(string line)
        {
            var action = line.Split(' ').FirstOrDefault().Trim();
            if (action == null && !String.IsNullOrWhiteSpace(line)) throw new InvalidOperationException($"Cannot find action for this line {line}");

            foreach (var strategy in Strategies)
                if (strategy.ResponseTo(action))
                {
                    strategy.ProccesLine(line, ListObjMaterialMesh);
                    return;
                }
            throw new InvalidOperationException($"Cannot find a correct parsing process for line {line}");
        }

        public void SetDirectoryPathMaterial(string path)
        {
            currentDirectory = Path.GetDirectoryName(path);
        }
    }
}