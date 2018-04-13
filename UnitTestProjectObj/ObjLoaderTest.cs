using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TGC.Core.Direct3D;
using TGC.Core.SceneLoader;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    public class ObjLoaderTest
    {
        private string _fullobjpath;
        private string _fullobjpathmeshcolorsolo;
        private string _fullobjpathmeshcontextura;
        private Panel _panel3D;

        [SetUp]
        public void Init()
        {
            const string testDataCuboTextura = "Resources\\cubotexturacaja.obj";
            const string testDataMeshColorSolo = "Resources\\tgcito\\Tgcito color solo.obj";
            const string testDataMeshConTextura = "Resources\\tgcito\\tgcito con textura.obj";

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullobjpath = Path.Combine(dir.Parent.FullName, testDataCuboTextura);
            _fullobjpathmeshcolorsolo = Path.Combine(dir.Parent.FullName, testDataMeshColorSolo);
            _fullobjpathmeshcontextura = Path.Combine(dir.Parent.FullName, testDataMeshConTextura);
            //Instanciamos un panel para crear un divice
            _panel3D = new Panel();
            //Crear Graphics Device
            //
            // panel3D
            //
            this._panel3D.Dock = DockStyle.Fill;
            this._panel3D.Location = new System.Drawing.Point(0, 0);
            this._panel3D.Name = "panel3D";
            this._panel3D.Size = new System.Drawing.Size(784, 561);
            this._panel3D.TabIndex = 0;

            D3DDevice.Instance.InitializeD3DDevice(_panel3D);
        }

        [TestCase]
        public void LoadObjFromFileOk()
        {
            var tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            Assert.True(tgcObjLoader.ObjMeshContainer.ListObjMesh.Count > 0);
            Assert.True(tgcObjLoader.ObjMeshContainer.VertexListV.Count == 8);
        }

        [TestCase]
        public void ProcessLineReturnWithLineBlanck()
        {
            var tgcObjLoader = new TGCObjLoader();
            var line = "";
            tgcObjLoader.ProccesLine(line);
            Assert.IsTrue(tgcObjLoader.ObjMeshContainer.ListObjMesh.Count == 0);
        }

        [TestCase]
        public void ProcessLineReturnWithSpaceBlanck()
        {
            var tgcObjLoader = new TGCObjLoader();
            var line = "        ";
            tgcObjLoader.ProccesLine(line);
            Assert.IsTrue(tgcObjLoader.ObjMeshContainer.ListObjMesh.Count == 0);
        }

        [TestCase]
        public void ProcessLineReturnWithFirstCaracterHastag()
        {
            var tgcObjLoader = new TGCObjLoader();
            var line = "# Blender v2.79 (sub 0) OBJ File: ''";
            tgcObjLoader.ProccesLine(line);
            Assert.IsTrue(tgcObjLoader.ObjMeshContainer.ListObjMesh.Count == 0);
        }

        [TestCase]
        public void ProcessLineThrowWithBadAction()
        {
            var tgcObjLoader = new TGCObjLoader();
            var line = "badAction Blender v2.79 (sub 0) OBJ File: ''";
            Assert.That(() => { tgcObjLoader.ProccesLine(line); }, Throws.InvalidOperationException);
        }

        [TestCase]
        public void ProccesLineNewObjet()
        {
            var tgcObjLoader = new TGCObjLoader();
            var line = "o Cube";
            tgcObjLoader.ProccesLine(line);
            Assert.True(tgcObjLoader.ObjMeshContainer.ListObjMesh.First().Name.Equals("Cube"));
        }

        [TestCase]
        public void GetListOfMaterialsOk()
        {
            var tgcObjLoader = new TGCObjLoader();
            var lines = File.ReadAllLines(_fullobjpath);
            tgcObjLoader.GetListOfMaterials(lines, _fullobjpath);
            Assert.True(tgcObjLoader.ListMtllib.Count > 0);
        }

        [TestCase]
        public void GetListOfMaterialsWithNameOK()
        {
            var tgcObjLoader = new TGCObjLoader();
            var lines = File.ReadAllLines(_fullobjpath);
            tgcObjLoader.GetListOfMaterials(lines, _fullobjpath);
            Assert.True(tgcObjLoader.ListMtllib.First().Equals("cubotexturacaja.mtl"));
        }

        [TestCase]
        public void GetListOfMaterialsWithWhiteSpaceOK()
        {
            var tgcObjLoader = new TGCObjLoader();
            var lines = File.ReadAllLines(_fullobjpathmeshcontextura);
            tgcObjLoader.GetListOfMaterials(lines, _fullobjpathmeshcontextura);
            Assert.True(tgcObjLoader.ListMtllib.First().Equals("tgcito con textura.mtl"));
        }

        [TestCase]
        public void FilterByKeyWordOk()
        {
            var tgcObjLoader = new TGCObjLoader();
            var lines = File.ReadAllLines(_fullobjpath);
            string mtllib = tgcObjLoader.FilterByKeyword(lines, "mtllib")[0];
            Assert.True(mtllib.Equals("mtllib cubotexturacaja.mtl"));
        }

        [TestCase]
        public void LoadTgcMeshFromObjwithOutMaterialsOk()
        {
            var tgcObjLoader = new TGCObjLoader();
            TgcMesh tgcMesh = tgcObjLoader.LoadTgcMeshFromObj(_fullobjpathmeshcolorsolo, 0);
            Assert.NotNull(tgcMesh);
        }

        [TestCase]
        public void LoadTgcMeshFromObjOk()
        {
            var tgcObjLoader = new TGCObjLoader();
            TgcMesh tgcMesh = tgcObjLoader.LoadTgcMeshFromObj(_fullobjpath, 0);
            Assert.NotNull(tgcMesh);
        }
    }
}