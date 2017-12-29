using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateVertexStrategyTest
    {
        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

        private readonly CreateVertexStrategy _createVertexStrategy = new CreateVertexStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [Test]
        public void ProccesLinewithVertexOk()
        {
            var line = "v 1.000000 -1.000000 -1.000000";
            _createVertexStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().VertexListV.Count > 0);
        }
    }
}