using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
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
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .build(resObjMesh);
            Assert.NotNull(tgcMesh);


        }

        [TestCase]
        public void CreateInstaceDxMeshOk()
        {
            MeshBuilder auxMeshBuilder = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberVertices == resObjMesh.FaceTrianglesList.Count * 3);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberFaces == resObjMesh.FaceTrianglesList.Count);
            Assert.NotNull(auxMeshBuilder.getInstaceDxMesh());

        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberVerticesOK()
        {
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberVertices == resObjMesh.FaceTrianglesList.Count * 3);
         
        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberFacesOK()
        {
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberFaces == resObjMesh.FaceTrianglesList.Count);
            
        }

        [TestCase]
        public void BuildTgcMeshWithAutotransformTrueOk()
        {

            TgcMesh tgcMesh = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .build(resObjMesh);
            Assert.True(tgcMesh.AutoTransformEnable);
        }

        [TestCase]
        public void BuildTgcMeshWithEnableTrueOk()
        {

            TgcMesh tgcMesh = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .SetEnable(true)
                .build(resObjMesh);
            Assert.True(tgcMesh.Enabled);
        }

        [TestCase]
        public void BuildTgcMeshWithBoundingBoxOk()  //TODO el boundingbox no deberia estar acoplado al mesh
        {

            TgcMesh tgcMesh = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .SetEnable(true)
                .SetAutotransform(true)
                .SetHasBoundingBox(false)
                .build(resObjMesh);
            Assert.NotNull(tgcMesh.BoundingBox);
        }

        [TestCase]
        public void TgcMeshBuildedCanSetPosition()
        {
            TgcMesh tgcMesh = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .build(resObjMesh);
            tgcMesh.Position = new Vector3(25, 0, 0);
            Assert.True(tgcMesh.Position.Equals(new Vector3(25, 0, 0)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSetScale()
        {
            TgcMesh tgcMesh = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .build(resObjMesh);
            tgcMesh.Scale = new Vector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Scale.Equals(new Vector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSet()
        {
            TgcMesh tgcMesh = new MeshBuilder()
                .InstaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .build(resObjMesh);
            tgcMesh.Rotation = new Vector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Rotation.Equals(new Vector3(8.0f, 8.5f, 8.5f)));
        }

        //Estos test se van hacer despues pensando en que puede haber un refactor de tipo estrategia para la creacion del mesh
        //TODO el test de cuando el mesh es solo color
        //TODO el test de cuando el mesh es color y difuse
        //TODO el test de cuando el mesh es color difuse y ligth map




    }
}
