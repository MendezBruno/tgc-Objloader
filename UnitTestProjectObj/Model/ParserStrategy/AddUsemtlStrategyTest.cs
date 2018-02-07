using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    class AddUsemtlStrategyTest
    {
        private readonly AddUsemtlStrategy _addUsemtlStrategy = new AddUsemtlStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

        [Test]
        public void HaveAttributeUsemtlinitialized()
        {
            Assert.True(ListObjMesh.Last().Usemtl.Count == 0);
            Assert.True(ListObjMesh.Last().Usemtl != null);
        }

        [Test]
        public void AsignateAttributeUsemtlOk()
        {
            var line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().Usemtl.Count > 0);
        }

        [Test]
        public void AsignateAttributeUsemtlWhenIsNoneOK()
        {
            var line = "usemtl Material.001";
            _addUsemtlStrategy.ProccesLine(line, ListObjMesh);
            line = "usemtl None";
            _addUsemtlStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().Usemtl.Last().Equals("None"));
        }

        [Test]
        public void AsignateAttributeUsemtlFalisByNumberOfParameters()
        {
            var line = "s badParameter";
            Assert.That(() => { _addUsemtlStrategy.ProccesLine(line, ListObjMesh); }, Throws.ArgumentException);

        }


    }
}
