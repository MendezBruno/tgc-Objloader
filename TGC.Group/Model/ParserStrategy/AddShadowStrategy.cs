using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParserStrategy
{
    public class AddShadowStrategy: ObjParseStrategy
    {
        private const int INDEXATTR = 1;

        public AddShadowStrategy()
        {
            Keyword = SHADOW;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            string attribute = line.Split(' ')[INDEXATTR];
            //TODO validar el atributo correctamente
            if (attribute.Equals("on") ) listObjMesh.Last().Shadow = true;
        }
    }
}
