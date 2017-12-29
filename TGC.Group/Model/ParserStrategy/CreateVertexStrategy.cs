using System.Collections.Generic;
using System.Linq;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateVertexStrategy : ObjParseStrategy
    {
        public CreateVertexStrategy()
        {
            Keyword = VERTEX;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var vertex = CreateVector3(line);
            listObjMesh.Last().VertexListV.Add(vertex);
        }
    }
}