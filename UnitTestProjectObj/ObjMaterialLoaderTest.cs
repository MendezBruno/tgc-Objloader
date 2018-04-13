using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TGC.Core.Direct3D;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    public class ObjMaterialLoaderTest
    {
        private string _fullMaterialPath;
        private Panel _panel3D;

        [SetUp]
        public void Init()
        {
            const string testDataArchivoBle = "Resources\\cubo.obj";  //TODO agregar el archivo para testear
            const string testDataArchivoBla = "Resources\\cubotexturacaja.obj";  //TODO agregar el archivo para testear

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullMaterialPath = Path.Combine(dir.Parent.FullName, testDataArchivoBla);

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
        public void LoadObjMaterialFromFileOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            var lines = File.ReadAllLines(_fullMaterialPath);
            tgcObjLoader.GetListOfMaterials(lines, _fullMaterialPath);
            ObjMaterialsLoader objMaterialLoader = new ObjMaterialsLoader();
            objMaterialLoader.LoadMaterialsFromFiles(_fullMaterialPath, tgcObjLoader.ListMtllib);
            Assert.NotNull(objMaterialLoader.ListObjMaterialMesh.First());
        }

        [TestCase]
        public void GetPathMaterialOk()
        {
            ObjMaterialsLoader objMaterialLoader = new ObjMaterialsLoader();
            objMaterialLoader.SetDirectoryPathMaterial(_fullMaterialPath);
            string pathMaterial = objMaterialLoader.MaterialPath("\\cubotexturacaja.mtl");
            Assert.True(File.Exists(pathMaterial));
        }
    }
}