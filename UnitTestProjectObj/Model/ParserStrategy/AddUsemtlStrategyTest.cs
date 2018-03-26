using NUnit.Framework;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    internal class AddUsemtlStrategyTest
    {
        private readonly AddUsemtlStrategy _addUsemtlStrategy = new AddUsemtlStrategy();
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
        public void HaveAttributeUsemtlinitialized()
        {
            Assert.True(ObjMeshContainer.ListObjMesh.Last().Usemtl.Count == 0);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().Usemtl != null);
        }

        [Test]
        public void AsignateAttributeUsemtlOk()
        {
            var line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().Usemtl.Count > 0);
        }

        [Test]
        public void AsignateAttributeUsemtlWhenIsNoneOK()
        {
            var line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, ObjMeshContainer);
            line = "usemtl None";
            _addUsemtlStrategy.ProccesLine(line, ObjMeshContainer);
            Assert.True(ObjMeshContainer.ListObjMesh.Last().Usemtl.Last().Equals("None"));
        }

        [Test]
        public void AsignateAttributeUsemtlFalisByNumberOfParameters()
        {
            var line = "s bad Parameter";
            Assert.That(() => { _addUsemtlStrategy.ProccesLine(line, ObjMeshContainer); }, Throws.ArgumentException);
        }
    }
}