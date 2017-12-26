using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    class CreateNormalStrategyTest
    {
        private CreateNormalStrategy _createNormalStrategy = new CreateNormalStrategy();
        private CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
            string line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
        }

        [Test]
        public void ProccesLinewithNormalOk()
        {

            string line = "v 1.000000 -1.000000 -1.000000";
            _createNormalStrategy.ProccesLine(line, ListObjMesh);
            Assert.True(ListObjMesh.Last().VertexListVn.Count > 0);

        }
    }
}
