using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Vector3 vertex = CreateVector3(line);
            listObjMesh.Last().VertexListV.Add(vertex);
        }
    }
}
