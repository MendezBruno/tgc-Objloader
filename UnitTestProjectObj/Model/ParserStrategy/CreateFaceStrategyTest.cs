using NUnit.Framework;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateFaceStrategyTest
    {
        private readonly CreateFaceStrategy _createFaceStrategy = new CreateFaceStrategy();
        private readonly AddUsemtlStrategy _addUsemtlStrategy = new AddUsemtlStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public ObjMeshContainer ObjMeshContainer;

        [SetUp]
        public void Init()
        {
            ObjMeshContainer = new ObjMeshContainer();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ObjMeshContainer);
            line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, ObjMeshContainer);
        }

        [Test]
        public void CreateFaceOk()
        {
            var line = "f 2/2/1 4/1/1 1/1/1";
            _createFaceStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Count > 0);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Last().V1 == 2);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Last().Vt1 == 2);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Last().Vn1 == 1);
        }

        [Test]
        public void CreateFaceWithoutVertexNormalOk()
        {
            var line = "f 2/2 4/2 1/2";
            _createFaceStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Last().Vn2 == 0);
        }

        [Test]
        public void CreateFaceWithoutVertexTextureOk()
        {
            var line = "f 2//1 4//1 1//1";
            _createFaceStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Last().Vt2 == 0);
        }

        [Test]
        public void AttributeUsemtlAdded()
        {
            var line = "f 2/2 4/2 1/2";
            _createFaceStrategy.ProccesLine(line, ObjMeshContainer);
            line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().FaceTrianglesList.Last().Usemtl.Equals("Material.001"));
        }

        [TestCase]
        public void CreateFaceProccesLineWithTextureFailsWhenCountParametersUp()
        {
            var line = "f 2/2/1 4/1/1 1/1/1 1/1/1";
            Assert.That(() => { _createFaceStrategy.ProccesLine(line, ObjMeshContainer); }, Throws.ArgumentException);
        }
    }
}