﻿using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    public class CreateTextCoordStrategyTest
    {
        private readonly CreateTextCoordStrategy _createTextCoordStrategy = new CreateTextCoordStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        private ObjMeshContainer _objMeshContainer;

        [SetUp]
        public void Init()
        {
            _objMeshContainer = new ObjMeshContainer();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, _objMeshContainer);
        }

        [Test]
        public void ProccesLinewithTextureOk()
        {
            var line = "vt 0.666628 0.167070";
            _createTextCoordStrategy.ProccesLine(line, _objMeshContainer);
            Assert.True(_objMeshContainer.VertexListVt.Count > 0);
        }

        [TestCase]
        public void ProccesLineWithTextureFailsWhenCountParametersUp()
        {
            var line = "vt 0.666628 0.167070 0.167070 ";
            Assert.That(() => { _createTextCoordStrategy.ProccesLine(line, _objMeshContainer); }, Throws.ArgumentException);
        }
    }
}