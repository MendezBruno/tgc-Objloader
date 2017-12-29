using System.Collections.Generic;
using System.Linq;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateTextCoordStrategy : ObjParseStrategy
    {
        public CreateTextCoordStrategy()
        {
            Keyword = TEXTURE;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var vertex = CreateVector3(line);
            listObjMesh.Last().VertexListVt.Add(vertex);
        }
    }
}