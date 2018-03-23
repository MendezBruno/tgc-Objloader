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
            ObjMeshContainer = new ObjMeshContainer();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ObjMeshContainer);
        }

        private readonly CreateTextCoordStrategy _createTextCoordStrategy = new CreateTextCoordStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public ObjMeshContainer ObjMeshContainer;

        [Test]
        public void ProccesLinewithTextureOk()
        {
            var line = "vt 0.666628 0.167070";
            _createTextCoordStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.VertexListVt.Count > 0);
        }

        [TestCase]
        public void ProccesLineWithTextureFailsWhenCountParametersUp()
        {
            var line = "vt 0.666628 0.167070 0.167070 ";
            Assert.That(() => { _createTextCoordStrategy.ProccesLine(line, ObjMeshContainer); }, Throws.ArgumentException);
        }
    }
}