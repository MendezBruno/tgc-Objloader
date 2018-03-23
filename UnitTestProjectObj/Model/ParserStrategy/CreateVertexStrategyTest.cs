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
            ObjMeshContainer = new ObjMeshContainer();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ObjMeshContainer);
        }

        private readonly CreateVertexStrategy _createVertexStrategy = new CreateVertexStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public ObjMeshContainer ObjMeshContainer;

        [Test]
        public void ProccesLinewithVertexOk()
        {
            var line = "v 1.000000 -1.000000 -1.000000";
            _createVertexStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.VertexListV.Count > 0);
        }
    }
}