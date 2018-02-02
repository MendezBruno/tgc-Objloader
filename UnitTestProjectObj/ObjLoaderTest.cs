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
        [SetUp]
        public void Init()
        {
            // _fullobjpath = _tgcObjLoader.GetPathObjforCurrentDirectory();
            // _fullobjpath = @"I:\proyectos net\tgc-group\UnitTestProjectObj\DatosPrueba\cubo.obj";
            //_fullobjpathHome = @"D:\workspace\proyectosnet\tgc-Objloader\UnitTestProjectObj\DatosPrueba\cubo.obj";


            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullobjpath = dir.Parent.FullName;
            string[] objFile = (Properties.Resources.cubotxt).Split(new string[] { "\\n" }, StringSplitOptions.None);
            var lines = File.ReadAllLines(_fullobjpath);
            var lala = "nono";


        }

        private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullobjpath;
        private string _fullobjpathHome;

        /*   [TestCase]
        public void LoadObjFromFileOk()
        {
            string path = tgcObjLoader.getPathObj();

            NUnit.Framework.Assert.True(); El resultado de este test deberia ser una lista de ObjMesh

        }
     */

        [TestCase]
        public void GetArrayLines()
        {
            //var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //assembly.Location; aca tengo la dll del test.
            string mono = Environment.CurrentDirectory;
            string mono2 = AppDomain.CurrentDomain.BaseDirectory;
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
            Assert.That(() => { _tgcObjLoader.ProccesLine(line); }, Throws.InvalidOperationException
            );
        }

        [TestCase]
        public void ProccesLineNewObjet()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var line = "o Cube";
            _tgcObjLoader.ProccesLine(line);
            Assert.True(_tgcObjLoader.ListObjMesh.First().Name.Equals("Cube"));
        }

        /*
        [Test]
        public void ProccesLineWithVertex()
        {
            string line = "v 1.000000 - 1.000000 - 1.000000";
            TgcObjLoader.ProccesLine(line);
            Assert.True(TgcObjLoader.Delegator.CurrentObjMesh.VertexListV.Count > 0);
        }

        [Test]
        public void CreateVectorFaliedForCurrentMeshNull()
        {
            string line = "v 1.000000 - 1.000000 - 1.000000";
            Assert.That(() =>
                {
                    TgcObjLoader.ProccesLine(line);
                }, Throws.InvalidOperationException

            );

        }

        [Test]
        public void CreateVectorFaliedForFormatInvalid()
        {
            
        }
        */
    }
}