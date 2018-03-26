using NUnit.Framework;
using System.Collections.Generic;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    internal class NoOperationStrategyTest
    {
        public ObjMeshContainer ObjMeshContainer;
        private readonly NoOperationStrategy _noOperationStrategy = new NoOperationStrategy();

        [SetUp]
        public void Init()
        {
            ObjMeshContainer = new ObjMeshContainer();
        }

        [Test]
        public void ProccesLineNoOperationHashtagOk()
        {
            List<ObjMesh> ListObjMesh = new List<ObjMesh>();
            var line = "# o Cube";
            _noOperationStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ListObjMesh.Count == 0);
        }
    }
}