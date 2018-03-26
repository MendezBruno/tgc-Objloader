using NUnit.Framework;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateNewMeshStrategyTest
    {
        [SetUp]
        public void Init()
        {
            ObjMeshContainer = new ObjMeshContainer();
        }

        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public ObjMeshContainer ObjMeshContainer;

        [Test]
        public void ProccesLineNewObjetOk()
        {
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.First().Name.Equals("Cube"));
        }
    }
}