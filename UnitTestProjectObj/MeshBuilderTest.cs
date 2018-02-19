using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using NUnit.Framework;
using TGC.Core.Direct3D;
using TGC.Core.SceneLoader;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    class MeshBuilderTest
    {
        private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullobjpath;
        ObjMesh resObjMesh;
        internal Mesh dxMesh;
        private System.Windows.Forms.Panel panel3D;
        public static D3DDevice Instance;



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
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();

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
        public void BuildTgcMeshFromObjOk()
        {
            
            TgcMesh tgcMesh = new MeshBuilder()
                .instaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .build(resObjMesh);
            Assert.NotNull(tgcMesh);


        }

        [TestCase]
        public void CreateInstaceDxMeshOk()
        {
            MeshBuilder auxMeshBuilder = new MeshBuilder()
                .instaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberVertices == resObjMesh.FaceTrianglesList.Count * 3);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberFaces == resObjMesh.FaceTrianglesList.Count);
            Assert.NotNull(auxMeshBuilder.getInstaceDxMesh());

        }

      
    }
}
