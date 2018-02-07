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
            var attribute = CheckAttribute(line);
            listObjMesh.Last().Usemtl.Add(attribute);
        }

        private string CheckAttribute(string line)
        {
            if (line.Split(' ').Length != 2) throw new ArgumentException("El atributo usemtl tiene formato incorrecto");
            var attribute = line.Split(' ')[INDEXATTR];
            return attribute;
        }
    }
}
