using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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

        [SetUp]
        public void Init()
        {
            const string testDataFolder = "DatosPrueba\\cubo.obj";

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullobjpath = Path.Combine(dir.Parent.FullName, testDataFolder);
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            resObjMesh = _tgcObjLoader.ListObjMesh.First();

        }

        [TestCase]
        public void BuildTgcMeshFromObjOk()
        {
            
            TgcMesh tgcMesh = new MeshBuilder()
                .instaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .build(resObjMesh);

        }

        [TestCase]
        public void CreateInstaceDxMeshOk()
        {
            MeshBuilder auxMeshBuilder = new MeshBuilder()
                .instaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberVertices == resObjMesh.VertexListV.Count);
            Assert.True(auxMeshBuilder.getInstaceDxMesh().NumberFaces == resObjMesh.FaceTrianglesList.Count);

        }
    }
}
