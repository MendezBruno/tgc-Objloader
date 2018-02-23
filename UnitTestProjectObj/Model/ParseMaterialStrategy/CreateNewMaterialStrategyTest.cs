using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace UnitTestProjectObj.Model.ParseMaterialStrategy
{
    [TestFixture]
    class CreateNewMaterialStrategyTest
    {

        [SetUp]
        public void Init()
        {
            ListObjMaterialMesh = new List<ObjMaterialMesh>();
        }

        private readonly CreateNewMaterialStrategy _createNewMeshMaterialStrategy = new CreateNewMaterialStrategy();
        public List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }


        [Test]
        public void ProccesLineNewObjetOk()
        {
            var line = "newmtl Material.001";
            _createNewMeshMaterialStrategy.ProccesLine(line, ListObjMaterialMesh);
            Assert.True(ListObjMaterialMesh.First().Name.Equals("Material.001"));
        }
    }
}
