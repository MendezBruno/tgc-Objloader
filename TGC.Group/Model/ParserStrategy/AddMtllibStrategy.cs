using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParserStrategy
{
    public class AddMtllibStrategy : ObjParseStrategy
    {
        private const int INDEXATTR = 2;

        public AddMtllibStrategy()
        {
            Keyword = MTLLIB;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            listObjMesh.Last().Mtllib = CheckAttributeLine(line);
        }

        private string CheckAttributeLine(string line)
        {
            if (line.Split(' ').Length != 3) throw new ArgumentException("El atributo Mtllib tiene formato incorrecto");
            var attribute = line.Split(' ')[INDEXATTR];
            //if (attribute == null) throw new ArgumentException("No se encontró el atributo para asignar");
            if (!Path.GetExtension(attribute).Equals(".mtl"))
            {
                throw new ArgumentException("La extención de Mtllib es incorrecta, se esperaba: .mtl y se obtuvo: " + Path.GetExtension(attribute));
            }
            
            return attribute;
        }
    }
}
