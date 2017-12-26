using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    class CreateFaceStrategyTest
    {
        private CreateFaceStrategy _createFaceStrategy = new CreateFaceStrategy();
        private CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            string line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

        [Test]
        public void CreateFaceOk()
        {

            string line = "f 2/2/1 4/1/1 1/1/1";
            _createFaceStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().FaceTrianglesList.Count > 0);

        }

        [Test]
        public void CreateFaceWithoutVertexTextureOk()
        {
            string line = "f 2//1 4//1 1//1";
            _createFaceStrategy.ProccesLine(line, ListObjMesh);
            Assert.IsNull(ListObjMesh.Last().FaceTrianglesList.Last().Vt2);
        }

        [Test]
        public void CreateFaceWithoutVertexNormalOk()
        {
            string line = "f 2/2/ 4/2/ 1/2/";
            _createFaceStrategy.ProccesLine(line, ListObjMesh);
            Assert.IsNull(ListObjMesh.Last().FaceTrianglesList.Last().Vn2);
        }

    }
}
