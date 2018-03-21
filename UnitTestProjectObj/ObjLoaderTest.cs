using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using NUnit.Framework;
using TGC.Core.Direct3D;
using TGC.Core.SceneLoader;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class ObjLoaderTest
    {

        private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullobjpath;
        private string _fullobjpathmeshcolorsolo;
        private System.Windows.Forms.Panel panel3D;



        [SetUp]
        public void Init()
        {
            const string testDataFolder = "DatosPrueba\\cubo.obj";
            const string testDataCuboTextura = "DatosPrueba\\cubotexturacaja.obj";
            const string testDataMeshColorSolo = "DatosPrueba\\tgcito\\Tgcito color solo.obj";

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullobjpath = Path.Combine(dir.Parent.FullName, testDataCuboTextura);
            _fullobjpathmeshcolorsolo = Path.Combine(dir.Parent.FullName, testDataMeshColorSolo);
            //Instanciamos un panel para crear un divice
            panel3D = new System.Windows.Forms.Panel();
            //Crear Graphics Device
            // 
            // panel3D
            // 
            this.panel3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3D.Location = new System.Drawing.Point(0, 0);
            this.panel3D.Name = "panel3D";
            this.panel3D.Size = new System.Drawing.Size(784, 561);
            this.panel3D.TabIndex = 0;

            D3DDevice.Instance.InitializeD3DDevice(panel3D);

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
            _tgcObjLoader.GetListOfMaterials(lines, _fullobjpath);
            Assert.True(_tgcObjLoader.ListMtllib.Count > 0);
        }

        [TestCase]
        public void GetListOfMaterialsWithNameOK()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var lines = File.ReadAllLines(_fullobjpath);
            _tgcObjLoader.GetListOfMaterials(lines, _fullobjpath);
            Assert.True(_tgcObjLoader.ListMtllib.First().Equals("cubotexturacaja.mtl"));
        }

        [TestCase]
        public void GetListOfMaterialsWithWhiteSpaceOK()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var lines = File.ReadAllLines(_fullobjpathmeshcolorsolo);
            _tgcObjLoader.GetListOfMaterials(lines, _fullobjpathmeshcolorsolo);
            Assert.True(_tgcObjLoader.ListMtllib.First().Equals("Triangulado 2 materiales.mtl"));
        }

        [TestCase]
        public void FilterByKeyWordOk()
        {
            var _tgcObjLoader = new TgcObjLoader();
            var lines = File.ReadAllLines(_fullobjpath);
            string mtllib = _tgcObjLoader.FilterByKeyword(lines, "mtllib")[0];
            Assert.True(mtllib.Equals("mtllib cubotexturacaja.mtl"));
        }

        [TestCase]
        public void LoadTgcMeshFromObjwithOutMaterialsOk()
        {
            var _tgcObjLoader = new TgcObjLoader();
            TgcMesh tgcMesh = _tgcObjLoader.LoadTgcMeshFromObj(_fullobjpathmeshcolorsolo, 0);
            Assert.NotNull(tgcMesh);
        }

        [TestCase]
        public void LoadTgcMeshFromObjOk()
        {
            var _tgcObjLoader = new TgcObjLoader();
            TgcMesh tgcMesh = _tgcObjLoader.LoadTgcMeshFromObj(_fullobjpath, 0);
            Assert.NotNull(tgcMesh);
        }

    }
}