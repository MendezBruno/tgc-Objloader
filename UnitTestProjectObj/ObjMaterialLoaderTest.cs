using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Core.Direct3D;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    class ObjMaterialLoaderTest
    {
        ObjMaterialsLoader _objMaterialLoader = new ObjMaterialsLoader();
        private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullMaterialPath;
        private System.Windows.Forms.Panel panel3D;

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
        public void LoadObjMaterialFromFileOk()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            var lines = File.ReadAllLines(_fullMaterialPath);
            _tgcObjLoader.GetListOfMaterials(lines, _fullMaterialPath);
            ObjMaterialsLoader _objMaterialLoader = new ObjMaterialsLoader();
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
            ObjMaterialsLoader _objMaterialLoader = new ObjMaterialsLoader();
            _objMaterialLoader.SetDirectoryPathMaterial(_fullMaterialPath);
            string pathMaterial = _objMaterialLoader.GetPathMaterial(_fullMaterialPath, "\\cubotexturacaja.mtl");
            Assert.True(File.Exists(pathMaterial));
        }
    }
}
