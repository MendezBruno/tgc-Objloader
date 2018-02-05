using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParserStrategy
{
    public class AddUsemtlStrategy : ObjParseStrategy
    {
        private const int INDEXATTR = 1;

        public AddUsemtlStrategy()
        {
            Keyword = USEMTL;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var attribute = line.Split(' ')[INDEXATTR];
            //TODO validar el atributo correctamente
            listObjMesh.Last().Mtllib = attribute;
        }
    }
}
