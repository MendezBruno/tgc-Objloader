using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TGC.Group.Model;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace UnitTestProjectObj.Model.ParseMaterialStrategy
{
    [TestFixture]
    class ParseMaterialAndColorStrategyTest
    {
        [SetUp]
        public void Init()
        {
            ListObjMaterialMesh = new List<ObjMaterialMesh>();
            var line = "newmtl Material.001";
            _createNewMeshMaterialStrategy.ProccesLine(line, ListObjMaterialMesh);
        }

        private readonly ParseMaterialAndColorStrategy _parseMaterialAndColorStrategy = new ParseMaterialAndColorStrategy();
        private readonly CreateNewMaterialStrategy _createNewMeshMaterialStrategy = new CreateNewMaterialStrategy();
        public List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }


        [Test]
        public void ProccesLineWithAmbientValueOk()
        {
            var line = "Ka 0.000000 0.000000 0.000000";
            _parseMaterialAndColorStrategy.ProccesLine(line, ListObjMaterialMesh);
            Assert.True(ListObjMaterialMesh.First().Name.Equals("Material.001"));
        }
    }
}
