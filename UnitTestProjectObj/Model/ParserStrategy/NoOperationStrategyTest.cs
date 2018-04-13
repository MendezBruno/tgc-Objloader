using NUnit.Framework;
using System.Collections.Generic;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    public class NoOperationStrategyTest
    {
        private ObjMeshContainer _objMeshContainer;
        private readonly NoOperationStrategy _noOperationStrategy = new NoOperationStrategy();

        [SetUp]
        public void Init()
        {
            _objMeshContainer = new ObjMeshContainer();
        }

        [Test]
        public void ProccesLineNoOperationHashtagOk()
        {
            List<ObjMesh> listObjMesh = new List<ObjMesh>();
            var line = "# o Cube";
            _noOperationStrategy.ProccesLine(line, _objMeshContainer);
            Assert.True(listObjMesh.Count == 0);
        }
    }
}