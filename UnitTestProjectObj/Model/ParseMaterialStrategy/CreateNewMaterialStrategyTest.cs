using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TGC.Group.Model;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace UnitTestProjectObj.Model.ParseMaterialStrategy
{
    [TestFixture]
    internal class CreateNewMaterialStrategyTest
    {
        private readonly CreateNewMaterialStrategy _createNewMeshMaterialStrategy = new CreateNewMaterialStrategy();
        private List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }

        [SetUp]
        public void Init()
        {
            ListObjMaterialMesh = new List<ObjMaterialMesh>();
        }

        [TestCase]
        public void ProccesLineNewObjetOk()
        {
            var line = "newmtl Material.001";
            _createNewMeshMaterialStrategy.ProccesLine(line, ListObjMaterialMesh);
            Assert.True(ListObjMaterialMesh.First().Name.Equals("Material.001"));
        }
    }
}