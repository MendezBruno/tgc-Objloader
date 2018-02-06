using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    internal class AddShadowStrategyTest
    {
        private readonly AddShadowStrategy _addShadowStrategy = new AddShadowStrategy();
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
        public void AsignateAttributeShadowOk()
        {
            var line = "s on";
            _addShadowStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().Shadow);
        }

        [Test]
        public void AsignateAttributeFalisByNumberOfParameters()
        {
            var line = "s";
            Assert.That(() => { _addShadowStrategy.ProccesLine(line, ListObjMesh); }, Throws.ArgumentException);
        }

        [Test]
        public void AsignateAttributeShadowFaliedForWrongParametro()
        {
            var line = "s badParameter";
            Assert.That(() => { _addShadowStrategy.ProccesLine(line, ListObjMesh); }, Throws.ArgumentException);
        }
    }
}
