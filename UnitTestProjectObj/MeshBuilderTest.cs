using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TGC.Core.Direct3D;
using TGC.Core.Mathematica;
using TGC.Core.SceneLoader;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class MeshBuilderTest
    {
        // private TGCObjLoader _tgcObjLoader = new TGCObjLoader();
        private string _fullobjpath;

        private string _fullobjpathmultimaterial;
        private string _fullobjpathmeshcolorsolo;
        private ObjMesh resObjMesh;
        private List<ObjMaterialMesh> listObjMaterialMesh;
        internal Mesh dxMesh;
        private System.Windows.Forms.Panel panel3D;

        [SetUp]
        public void Init()
        {
            //constantes
            const string testDatabb8Multimaterial = "DatosPrueba\\bb8\\bb8.obj";
            const string testDataCuboTextura = "DatosPrueba\\cubotexturacaja.obj";
            const string testDataMeshColorSolo = "DatosPrueba\\tgcito\\Tgcito color solo.obj";

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
            _fullobjpathmeshcolorsolo = Path.Combine(dir.Parent.FullName, testDataMeshColorSolo);
        }

        [TestCase]
        public void CreateInstaceDxMeshOk()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
                .AddDxMesh(resObjMesh.FaceTriangles.Count);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberVertices == resObjMesh.FaceTriangles.Count * 3);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberFaces == resObjMesh.FaceTriangles.Count);
            Assert.NotNull(auxMeshBuilder.GetInstaceDxMesh());
        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberVerticesOK()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .AddDxMesh(resObjMesh.FaceTriangles.Count);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberVertices == resObjMesh.FaceTriangles.Count * 3);
        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberFacesOK()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .AddDxMesh(resObjMesh.FaceTriangles.Count);
            Assert.True(auxMeshBuilder.GetInstaceDxMesh().NumberFaces == resObjMesh.FaceTriangles.Count);
        }

        [TestCase]
        public void BuildTgcMeshWithAutotransformTrueOk()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .Build(resObjMesh);
            Assert.True(tgcMesh.AutoTransform);
        }

        [TestCase]
        public void BuildTgcMeshWithEnableTrueOk()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .SetEnable(true)
                .Build(resObjMesh);
            Assert.True(tgcMesh.Enabled);
        }

        [TestCase]
        public void TgcMeshBuildedCanSetPosition()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            tgcMesh.Position = new TGCVector3(25, 0, 0);
            Assert.True(tgcMesh.Position.Equals(new TGCVector3(25, 0, 0)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSetScale()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            tgcMesh.Scale = new TGCVector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Scale.Equals(new TGCVector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSet()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(resObjMesh);
            tgcMesh.Rotation = new TGCVector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Rotation.Equals(new TGCVector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildWithTextureOk()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials()
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(true)
                .Build(resObjMesh);
            Assert.True(tgcMesh.Materials.Length > 0);
        }

        [TestCase]
        public void BuildTgcMeshWithBoundingBoxOk()  //TODO el boundingbox no deberia estar acoplado al mesh
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0)
                .SetEnable(true)
                .AddAutotransform(true)
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
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader).ChargueMaterials();
            Assert.NotNull(meshBuilder.MeshMaterials);
        }

        [TestCase]
        public void AddMultiMaterialToBuilderOk()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader).ChargueMaterials();
            Assert.NotNull(meshBuilder.MeshMaterials);
        }

        [TestCase]
        public void FirstMaterialMeshHaveIndexZero()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials();
            Assert.NotNull(meshBuilder.MeshMaterials[0]);
        }

        [TestCase]
        public void CreateMeshOnlyColor()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmeshcolorsolo);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder().AddDxMesh(resObjMesh.FaceTriangles.Count).ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0);
            Assert.NotNull(meshBuilder.GetInstaceDxMesh());
        }

        [TestCase]
        public void CreateMeshColorAndDifusseMap()
        {
            //TODO el test de cuando el mesh es color y difuse
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials()
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0);
            Assert.NotNull(meshBuilder.GetInstaceDxMesh());
        }

        [TestCase]
        public void GetTextureCountOk()
        {
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials();
            Assert.True(meshBuilder.GetTextureCount() == 2);
        }

        [TestCase]
        public void EnsureRightTypeRenderIsLoadedDiffuseMapBranch()
        {
            //TODO asegurar que si tiene material el tipo de render sea difuse map, o si tiene ligth map que sea difuse mas ligth map
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials();
            Assert.True(meshBuilder.ChargueBufferStrategy.RenderType == TgcMesh.MeshRenderType.DIFFUSE_MAP);
        }

        [TestCase]
        public void EnsureRightTypeRenderIsLoadedOnlyColorBranch()
        {
            //TODO asegurar que si tiene material el tipo de render sea difuse map, o si tiene ligth map que sea difuse mas ligth map
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmeshcolorsolo);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddDxMesh(resObjMesh.FaceTriangles.Count);
            Assert.True(meshBuilder.ChargueBufferStrategy.RenderType == TgcMesh.MeshRenderType.VERTEX_COLOR);
        }

        [TestCase]
        public void CheckIndexBufferIsChargedOk()
        {
            //TODO verificar que el idexbuffer se cargo .
            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(_tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials()
                .AddDxMesh(resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(_tgcObjLoader.ObjMeshContainer, 0);
            Assert.True(meshBuilder.GetInstaceDxMesh().IndexBuffer.SizeInBytes > 0);
        }

        [TestCase]
        public void CreateDxMeshWithVertexLimit()
        {
            // 21845 * 3 = 65535
            // Int range 0 to 65535

            TGCObjLoader _tgcObjLoader = new TGCObjLoader();
            _tgcObjLoader.LoadObjFromFile(_fullobjpathmeshcolorsolo);
            resObjMesh = _tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder().AddDxMesh(21844);
            Assert.NotNull(meshBuilder.GetInstaceDxMesh());

        }

        //Estos test se van hacer despues pensando en que puede haber un refactor de tipo estrategia para la creacion del mesh
        //TODO el test de cuando el mesh es color difuse y ligth map
    }
}