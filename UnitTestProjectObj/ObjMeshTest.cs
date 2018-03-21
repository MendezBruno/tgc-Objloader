using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using NUnit.Framework;
using TGC.Core.Direct3D;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    class ObjMeshTest
    {

        private string _fullobjpath;
        private string _fullobjpathmultimaterial;
        ObjMesh resObjMesh;
        private List<ObjMaterialMesh> listObjMaterialMesh;
        internal Mesh dxMesh;
        private System.Windows.Forms.Panel panel3D;




        [SetUp]
        public void Init()
        {
            //constantes
            const string testDatabb8Multimaterial = "DatosPrueba\\bb8\\bb8.obj";
            const string testDataCuboTextura = "DatosPrueba\\cubotexturacaja.obj";

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

            //Creamos los materiales para luego poder probar la creacion del mesh
            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullobjpath = Path.Combine(dir.Parent.FullName, testDataCuboTextura);
            _fullobjpathmultimaterial = Path.Combine(dir.Parent.FullName, testDatabb8Multimaterial);
        }


        [TestCase]
        public void CreateMaterialIdsArrayOk()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            Assert.True(resObjMesh.FaceTrianglesList.Count == resObjMesh.CreateMaterialIdsArray().Length);
        }

        [TestCase]
        public void IndexMaterialIdsArrayOk()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            int[] materialIds = resObjMesh.CreateMaterialIdsArray();
            Assert.True(materialIds[15810] == 0);
            Assert.True(materialIds[17010] == 1);
            
        }
    }
}
