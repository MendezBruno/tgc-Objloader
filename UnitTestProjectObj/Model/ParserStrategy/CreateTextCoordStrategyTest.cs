using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateTextCoordStrategyTest
    {
        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

        private readonly CreateTextCoordStrategy _createTextCoordStrategy = new CreateTextCoordStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [Test]
        public void ProccesLinewithTextureOk()
        {
            var line = "v 1.000000 -1.000000 -1.000000";
            _createTextCoordStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().VertexListVt.Count > 0);
        }
    }
}