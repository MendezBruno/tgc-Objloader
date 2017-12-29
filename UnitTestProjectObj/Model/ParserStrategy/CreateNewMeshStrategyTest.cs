using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    internal class CreateNewMeshStrategyTest
    {
        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
        }

        private readonly CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }


        [Test]
        public void ProccesLineNewObjetOk()
        {
            var line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.First().Name.Equals("Cube"));
        }
    }
}