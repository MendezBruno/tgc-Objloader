using System.Collections.Generic;
using System.Linq;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateNormalStrategy : ObjParseStrategy
    {
        public CreateNormalStrategy()
        {
            Keyword = NORMAL;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var vertex = CreateVector3(line);
            listObjMesh.Last().VertexListVn.Add(vertex);
        }
    }
}