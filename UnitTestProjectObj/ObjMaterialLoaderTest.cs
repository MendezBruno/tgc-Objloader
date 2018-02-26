using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    class ObjMaterialLoaderTest
    {
        ObjMaterialsLoader _objMaterialLoader = new ObjMaterialsLoader();
        private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullMaterialPath;

        [SetUp]
        public void Init()
        {
            const string testDataArchivoBle = "DatosPrueba\\cubo.obj";  //TODO agregar el archivo para testear
            const string testDataArchivoBla = "DatosPrueba\\cubotexturacaja.obj";  //TODO agregar el archivo para testear

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullMaterialPath = Path.Combine(dir.Parent.FullName, testDataArchivoBla);


            //Obtengo la lista de materiales para las pruebas
            var lines = File.ReadAllLines(_fullMaterialPath);
            _tgcObjLoader.GetListOfMaterials(lines);
        }


        [TestCase]
        public void LoadObjMaterialFromFileOk()
        {
            _objMaterialLoader.LoadMaterialsFromFiles(_fullMaterialPath, _tgcObjLoader.ListMtllib);
            Assert.NotNull(_objMaterialLoader.ListObjMaterialMesh.First());
        }

        [TestCase]
        public void GetArrayLinesOk()
        {
            var lines = File.ReadAllLines(_fullMaterialPath);
            Assert.True(lines.Length > 0);
        }

        [TestCase]
        public void GetPathMaterialOk()
        {
            string pathMaterial = _objMaterialLoader.GetPathMaterial(_fullMaterialPath, "\\cubotexturacaja.mtl");
            Assert.True(File.Exists(pathMaterial));
        }
    }
}
