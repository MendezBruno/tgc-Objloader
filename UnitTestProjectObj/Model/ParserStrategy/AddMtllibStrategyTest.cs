using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj.Model.ParserStrategy
{
    [TestFixture]
    internal class AddMtllibStrategyTest
    {
        private readonly AddMtllibStrategy _addMtllibStrategy = new AddMtllibStrategy();
        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

     /*   [Test]
        public void AsignateAttributeOk()
        {
            var line = "mtllib Cubo Triangulado.mtl";
            _addMtllibStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().Mtllib.Equals("Triangulado.mtl"));
        }*/

        [Test]
        public void AsignateAttributeFaliedForAttributeNull()
        {
            var line = "mtllib badString";
            Assert.That(() => { _addMtllibStrategy.ProccesLine(line, ListObjMesh); }, Throws.ArgumentException);
        }

        [Test]
        public void AttributeHasBadExtension()
        {
            var line = "mtllib Cubo Triangulado.bad";
            Assert.That(() => { _addMtllibStrategy.ProccesLine(line, ListObjMesh); }, Throws.ArgumentException);
        }
    }
}
