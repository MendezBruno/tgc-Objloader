using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateNormalStrategyTest
    {
        [SetUp]
        public void Init()
        {
            ObjMeshContainer = new ObjMeshContainer();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ObjMeshContainer);
        }

        private readonly CreateNormalStrategy _createNormalStrategy = new CreateNormalStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public ObjMeshContainer ObjMeshContainer;

        [Test]
        public void ProccesLinewithNormalOk()
        {
            var line = "v 1.000000 -1.000000 -1.000000";
            _createNormalStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.VertexListVn.Count > 0);
        }
    }
}