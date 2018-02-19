using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using NUnit.Framework;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class ObjLoaderTest
    {

        private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullobjpath;
        private string _fullobjpathHome;


        [SetUp]
        public void Init()
        {
            const string testDataFolder = "DatosPrueba\\cubo.obj";
            const string testDataCuboTextura = "DatosPrueba\\cubocontextura.obj";

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullobjpath = Path.Combine(dir.Parent.FullName, testDataCuboTextura);
        }

        [TestCase]
        public void LoadObjFromFileOk()
        {
            var _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            Assert.True(_tgcObjLoader.ListObjMesh.Count > 0);
            ObjMesh resObjMesh = _tgcObjLoader.ListObjMesh.First();
            Assert.True(resObjMesh.VertexListV.Count == 8);

            

        }
     

        [TestCase]
        public void GetArrayLinesOk()
        {
            var lines = File.ReadAllLines(_fullobjpath);
            Assert.True(lines.Length > 0);
        }


        [TestCase]
        public void ProcessLineReturnWithLineBlanck()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var line = "";
            _tgcObjLoader.ProccesLine(line);
            Assert.IsTrue(_tgcObjLoader.ListObjMesh.Count == 0);
        }

        [TestCase]
        public void ProcessLineReturnWithSpaceBlanck()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var line = "        ";
            _tgcObjLoader.ProccesLine(line);
            Assert.IsTrue(_tgcObjLoader.ListObjMesh.Count == 0);
        }

        [TestCase]
        public void ProcessLineReturnWithFirstCaracterHastag()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var line = "# Blender v2.79 (sub 0) OBJ File: ''";
            _tgcObjLoader.ProccesLine(line);
            Assert.IsTrue(_tgcObjLoader.ListObjMesh.Count == 0);
        }

        [TestCase]
        public void ProcessLineThrowWithBadAction()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var line = "badAction Blender v2.79 (sub 0) OBJ File: ''";
            Assert.That(() => { _tgcObjLoader.ProccesLine(line); }, Throws.InvalidOperationException);
        }

        [TestCase]
        public void ProccesLineNewObjet()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var line = "o Cube";
            _tgcObjLoader.ProccesLine(line);
            Assert.True(_tgcObjLoader.ListObjMesh.First().Name.Equals("Cube"));
        }

        [TestCase]
        public void GetListOfMaterialsOk()
        {
            var lines = File.ReadAllLines(_fullobjpath);
            _tgcObjLoader.GetListOfMaterials(lines);
            Assert.True(_tgcObjLoader.ListMtllib.Count > 0);
        }

        [TestCase]
        public void GetListwithOutMaterialsOk()
        {

        }
    }
}