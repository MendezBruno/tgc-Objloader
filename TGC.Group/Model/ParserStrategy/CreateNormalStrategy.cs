using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Vector3 vertex = CreateVector3(line);
            listObjMesh.Last().VertexListVn.Add(vertex);
        }
    }
}
