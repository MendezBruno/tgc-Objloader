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
       // private TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        private string _fullobjpath;
        ObjMesh resObjMesh;
        private List<ObjMaterialMesh> listObjMaterialMesh;
        internal Mesh dxMesh;
        private System.Windows.Forms.Panel panel3D;
       



        [SetUp]
        public void Init()
        {
            //constantes
            const string testDataFolder = "DatosPrueba\\cubo.obj";
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
        }

       

        [TestCase]
        public void CreateInstaceDxMeshOk()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberVertices == resObjMesh.FaceTrianglesList.Count * 3);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberFaces == resObjMesh.FaceTrianglesList.Count);
            Assert.NotNull(auxMeshBuilder.GetInstaceDxMesh());

        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberVerticesOK()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .AddDxMesh(resObjMesh.FaceTrianglesList.Count);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberVertices == resObjMesh.FaceTrianglesList.Count * 3);
         
        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberFacesOK()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .AddDxMesh(resObjMesh.FaceTrianglesList.Count);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberFaces == resObjMesh.FaceTrianglesList.Count);
            
        }

        
        [TestCase]
        public void BuildTgcMeshWithAutotransformTrueOk()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .Build(resObjMesh);
            Assert.True(tgcMesh.AutoTransformEnable);
        }
        

        [TestCase]
        public void BuildTgcMeshWithEnableTrueOk()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetEnable(true)
                .Build(resObjMesh);
            Assert.True(tgcMesh.Enabled);
        }

        

        [TestCase]
        public void TgcMeshBuildedCanSetPosition()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            tgcMesh.Position = new Vector3(25, 0, 0);
            Assert.True(tgcMesh.Position.Equals(new Vector3(25, 0, 0)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSetScale()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            tgcMesh.Scale = new Vector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Scale.Equals(new Vector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSet()
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            tgcMesh.Rotation = new Vector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Rotation.Equals(new Vector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildWithTextureOk()
        {

            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(true)
                .Build(resObjMesh);
            Assert.True(tgcMesh.Materials.Length > 0); 
        }

        
        [TestCase]
        public void BuildTgcMeshWithBoundingBoxOk()  //TODO el boundingbox no deberia estar acoplado al mesh
        {
            TgcObjLoader _tgcObjLoader = new TgcObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTrianglesList.Count)
                .ChargeBuffer(resObjMesh)
                .SetEnable(true)
                .SetAutotransform(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            Assert.NotNull(tgcMesh.BoundingBox);
        }

        [TestCase]
        public void InstanceDxMeshFailWithIncorrectVectorStructure()
        {
            //TODO no se si esta excepcion va a ocurrir o va a estar controlada
        }

        [TestCase]
        public void AddMaterialToBuilderOk()
        {
            //TODO agregar un material y chequear que se cargo correctamente
        }

        [TestCase]
        public void CreateMeshOnlyColor()
        {
            //TODO el test de cuando el mesh es solo color
        }

        [TestCase]
        public void CreateMeshColorAndDifusseMap()
        {
            //TODO el test de cuando el mesh es color y difuse
        }


        
        //Estos test se van hacer despues pensando en que puede haber un refactor de tipo estrategia para la creacion del mesh
        //TODO el test de cuando el mesh es color difuse y ligth map


    }
}
