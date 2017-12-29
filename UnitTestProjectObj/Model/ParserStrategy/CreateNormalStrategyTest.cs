using System.Collections.Generic;
using System.Linq;
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
            ListObjMesh = new List<ObjMesh>();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

        private readonly CreateNormalStrategy _createNormalStrategy = new CreateNormalStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [Test]
        public void ProccesLinewithNormalOk()
        {
            var line = "v 1.000000 -1.000000 -1.000000";
            _createNormalStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().VertexListVn.Count > 0);
        }
    }
}