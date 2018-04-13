using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
        private ObjMesh _resObjMesh;
        private Panel _panel3D;

        [SetUp]
        public void Init()
        {
            //constantes
            const string testDatabb8Multimaterial = "Resources\\bb8\\bb8.obj";
            const string testDataCuboTextura = "Resources\\cubotexturacaja.obj";
            const string testDataMeshColorSolo = "Resources\\tgcito\\Tgcito color solo.obj";

            //Instanciamos un panel para crear un divice
            _panel3D = new Panel();
            //Crear Graphics Device
            //
            // _panel3D
            //
            this._panel3D.Dock = DockStyle.Fill;
            this._panel3D.Location = new System.Drawing.Point(0, 0);
            this._panel3D.Name = "panel3D";
            this._panel3D.Size = new System.Drawing.Size(784, 561);
            this._panel3D.TabIndex = 0;

            D3DDevice.Instance.InitializeD3DDevice(_panel3D);

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
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
                .AddDxMesh(_resObjMesh.FaceTriangles.Count);
            Assert.True(auxMeshBuilder.DXMesh.NumberVertices == _resObjMesh.FaceTriangles.Count * 3);
            Assert.True(auxMeshBuilder.DXMesh.NumberFaces == _resObjMesh.FaceTriangles.Count);
            Assert.NotNull(auxMeshBuilder.DXMesh);
        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberVerticesOK()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .AddDxMesh(_resObjMesh.FaceTriangles.Count);
            Assert.True(auxMeshBuilder.DXMesh.NumberVertices == _resObjMesh.FaceTriangles.Count * 3);
        }

        [TestCase]
        public void CreateInstanceDxMeshWithNumberFacesOK()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder auxMeshBuilder = new MeshBuilder()
               .AddDxMesh(_resObjMesh.FaceTriangles.Count);
            Assert.True(auxMeshBuilder.DXMesh.NumberFaces == _resObjMesh.FaceTriangles.Count);
        }

        [TestCase]
        public void BuildTgcMeshWithAutotransformTrueOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .Build(_resObjMesh);
            Assert.True(tgcMesh.AutoTransform);
        }

        [TestCase]
        public void BuildTgcMeshWithEnableTrueOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .SetEnable(true)
                .Build(_resObjMesh);
            Assert.True(tgcMesh.Enabled);
        }

        [TestCase]
        public void TgcMeshBuildedCanSetPosition()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(_resObjMesh);
            tgcMesh.Position = new TGCVector3(25, 0, 0);
            Assert.True(tgcMesh.Position.Equals(new TGCVector3(25, 0, 0)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSetScale()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(_resObjMesh);
            tgcMesh.Scale = new TGCVector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Scale.Equals(new TGCVector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildedCanSet()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(false)
                .Build(_resObjMesh);
            tgcMesh.Rotation = new TGCVector3(8.0f, 8.5f, 8.5f);
            Assert.True(tgcMesh.Rotation.Equals(new TGCVector3(8.0f, 8.5f, 8.5f)));
        }

        [TestCase]
        public void TgcMeshBuildWithTextureOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials()
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .AddAutotransform(true)
                .SetEnable(true)
                .SetHasBoundingBox(true)
                .Build(_resObjMesh);
            Assert.True(tgcMesh.Materials.Length > 0);
        }

        [TestCase]
        public void BuildTgcMeshWithBoundingBoxOk()  //TODO el boundingbox no deberia estar acoplado al mesh
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0)
                .SetEnable(true)
                .AddAutotransform(true)
                .SetHasBoundingBox(false)
                .Build(_resObjMesh);
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
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpath);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader).ChargueMaterials();
            Assert.NotNull(meshBuilder.MeshMaterials);
        }

        [TestCase]
        public void AddMultiMaterialToBuilderOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader).ChargueMaterials();
            Assert.NotNull(meshBuilder.MeshMaterials);
        }

        [TestCase]
        public void FirstMaterialMeshHaveIndexZero()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials();
            Assert.NotNull(meshBuilder.MeshMaterials[0]);
        }

        [TestCase]
        public void CreateMeshOnlyColor()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmeshcolorsolo);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder().AddDxMesh(_resObjMesh.FaceTriangles.Count).ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0);
            Assert.NotNull(meshBuilder.DXMesh);
        }

        [TestCase]
        public void CreateMeshColorAndDifusseMap()
        {
            //TODO el test de cuando el mesh es color y difuse
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials()
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0);
            Assert.NotNull(meshBuilder.DXMesh);
        }

        [TestCase]
        public void GetTextureCountOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials();
            Assert.True(meshBuilder.GetTextureCount() == 2);
        }

        [TestCase]
        public void EnsureRightTypeRenderIsLoadedDiffuseMapBranch()
        {
            //TODO asegurar que si tiene material el tipo de render sea difuse map, o si tiene ligth map que sea difuse mas ligth map
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials();
            Assert.True(meshBuilder.ChargueBufferStrategy.RenderType == TgcMesh.MeshRenderType.DIFFUSE_MAP);
        }

        [TestCase]
        public void EnsureRightTypeRenderIsLoadedOnlyColorBranch()
        {
            //TODO asegurar que si tiene material el tipo de render sea difuse map, o si tiene ligth map que sea difuse mas ligth map
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmeshcolorsolo);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddDxMesh(_resObjMesh.FaceTriangles.Count);
            Assert.True(meshBuilder.ChargueBufferStrategy.RenderType == TgcMesh.MeshRenderType.VERTEX_COLOR);
        }

        [TestCase]
        public void CheckIndexBufferIsChargedOk()
        {
            //TODO verificar que el idexbuffer se cargo .
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder()
                .AddMaterials(tgcObjLoader.ObjMaterialsLoader)
                .ChargueMaterials()
                .AddDxMesh(_resObjMesh.FaceTriangles.Count)
                .ChargeBuffer(tgcObjLoader.ObjMeshContainer, 0);
            Assert.True(meshBuilder.DXMesh.IndexBuffer.SizeInBytes > 0);
        }

        [TestCase]
        public void CreateDxMeshWithVertexLimit()
        {
            // 21845 * 3 = 65535
            // Int range 0 to 65535

            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmeshcolorsolo);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            MeshBuilder meshBuilder = new MeshBuilder().AddDxMesh(21844);
            Assert.NotNull(meshBuilder.DXMesh);
        }

        //Estos test se van hacer despues pensando en que puede haber un refactor de tipo estrategia para la creacion del mesh
        //TODO el test de cuando el mesh es color difuse y ligth map
    }
}