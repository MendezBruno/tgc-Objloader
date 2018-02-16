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

        }

        [TestCase]
        public void BuildTgcMeshFromObjOk()
        {
           
            _tgcObjLoader.LoadObjFromFile(_fullobjpath);
            ObjMesh resObjMesh = _tgcObjLoader.ListObjMesh.First();
            TgcMesh tgcMesh = new MeshBuilder()
                .instaceDxMesh(resObjMesh.FaceTrianglesList.Count, resObjMesh.VertexListV.Count)
                .chargeBuffer(resObjMesh)
                .build(resObjMesh);

        }
    }
}
