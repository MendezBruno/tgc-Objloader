using NUnit.Framework;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    internal class AddShadowStrategyTest
    {
        private readonly AddShadowStrategy _addShadowStrategy = new AddShadowStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public ObjMeshContainer ObjMeshContainer;

        [SetUp]
        public void Init()
        {
            ObjMeshContainer = new ObjMeshContainer();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ObjMeshContainer);
        }

        [Test]
        public void AsignateAttributeShadowOk()
        {
            var line = "s 1";
            _addShadowStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().Shadow);
        }

        [Test]
        public void AsignateAttributeFalisByNumberOfParameters()
        {
            var line = "s";
            Assert.That(() => { _addShadowStrategy.ProccesLine(line, ObjMeshContainer); }, Throws.ArgumentException);
        }

        [Test]
        public void AsignateAttributeShadowFaliedForWrongParametro()
        {
            var line = "s badParameter";
            Assert.That(() => { _addShadowStrategy.ProccesLine(line, ObjMeshContainer); }, Throws.ArgumentException);
        }

        //TODO hacer el test cuando s viene 1 ó 0 por si es smooth o no
    }
}