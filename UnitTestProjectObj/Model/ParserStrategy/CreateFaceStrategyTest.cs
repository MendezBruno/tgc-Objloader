using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateFaceStrategyTest
    {
        private readonly CreateFaceStrategy _createFaceStrategy = new CreateFaceStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }
        
        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }
        
        [Test]
        public void CreateFaceOk()
        {
            var line = "f 2/2/1 4/1/1 1/1/1";
            _createFaceStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Count > 0);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Last().V1 == 2);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Last().Vt1 == 2);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Last().Vn1 == 1);
        }

        [Test]
        public void CreateFaceWithoutVertexNormalOk()
        {
            var line = "f 2/2 4/2 1/2";
            _createFaceStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Last().Vn2 == 0);
        }

        [Test]
        public void CreateFaceWithoutVertexTextureOk()
        {
            var line = "f 2//1 4//1 1//1";
            _createFaceStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Last().Vt2 == 0);
        }
    }
}