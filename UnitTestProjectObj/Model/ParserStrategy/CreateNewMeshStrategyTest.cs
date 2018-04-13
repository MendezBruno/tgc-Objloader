using NUnit.Framework;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    public class CreateNewMeshStrategyTest
    {
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        private ObjMeshContainer _objMeshContainer;

        [SetUp]
        public void Init()
        {
            _objMeshContainer = new ObjMeshContainer();
        }

        [Test]
        public void ProccesLineNewObjetOk()
        {
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, _objMeshContainer);
            Assert.True(_objMeshContainer.ListObjMesh.First().Name.Equals("Cube"));
        }
    }
}