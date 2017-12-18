using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParserStrategy;

namespace UnitTestProjectObj
{
    [TestFixture]
    class CreateNewMeshStrategyTest
    {
        private CreateNewMeshStrategy _createNewMeshStrategy = new CreateNewMeshStrategy();
        public List<ObjMesh> ListObjMesh { get; set; }

        [SetUp]
        public void Init()
        {
            ListObjMesh = new List<ObjMesh>();
        }


        [Test]
        public void ProccesLineNewObjet()
        {
            
            string line = "o Cube";
            _createNewMeshStrategy.ProccesLine(line, ListObjMesh);
            Assert.Equals(typeof(ObjMesh), ListObjMesh.First());
        }
    }

    
}
