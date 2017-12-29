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
    class NoOperationStrategyTest
    {
        public List<ObjMesh> ListObjMesh { get; set; }
        private readonly NoOperationStrategy _noOperationStrategy = new NoOperationStrategy();


    [SetUp]
        public void Init()
        {
            List<ObjMesh> ListObjMesh = new List<ObjMesh>();
        }

    [Test]
    public void ProccesLineNoOperationHashtagOk()
    {
        List<ObjMesh> ListObjMesh = new List<ObjMesh>();
        var line = "# o Cube";
        _noOperationStrategy.ProccesLine(line, ListObjMesh);
        Assert.True(ListObjMesh.Count == 0);
    }


    }
}
