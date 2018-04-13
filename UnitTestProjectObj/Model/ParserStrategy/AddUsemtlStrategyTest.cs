using NUnit.Framework;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    public class AddUsemtlStrategyTest
    {
        private readonly AddUsemtlStrategy _addUsemtlStrategy = new AddUsemtlStrategy();
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
        public void HaveAttributeUsemtlinitialized()
        {
            Assert.True(_objMeshContainer.ListObjMesh.Last().Usemtl.Count == 0);
            Assert.True(_objMeshContainer.ListObjMesh.Last().Usemtl != null);
        }

        [Test]
        public void AsignateAttributeUsemtlOk()
        {
            var line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, _objMeshContainer);
            Assert.True(_objMeshContainer.ListObjMesh.Last().Usemtl.Count > 0);
        }

        [Test]
        public void AsignateAttributeUsemtlWhenIsNoneOk()
        {
            var line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, _objMeshContainer);
            line = "usemtl None";
            _addUsemtlStrategy.ProccesLine(line, _objMeshContainer);
            Assert.True(_objMeshContainer.ListObjMesh.Last().Usemtl.Last().Equals("None"));
        }

        [Test]
        public void AsignateAttributeUsemtlFalisByNumberOfParameters()
        {
            var line = "s bad Parameter";
            Assert.That(() => { _addUsemtlStrategy.ProccesLine(line, _objMeshContainer); }, Throws.ArgumentException);
        }
    }
}