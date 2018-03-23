using System;
using System.Collections.Generic;
using System.Linq;


namespace TGC.Group.Model.ParserStrategy
{
    public class AddShadowStrategy: ObjParseStrategy
    {
        private const int INDEXATTR = 1;

        public AddShadowStrategy()
        {
            Keyword = SHADOW;
        }

        public override void ProccesLine(string line, ObjMeshContainer objMeshContainer)
        {
           
            string attribute = CheckAttribute(line); // 
            if (attribute.Equals("1") ) objMeshContainer.ListObjMesh.Last().Shadow = true;
        }

        private string CheckAttribute(string line)
        {
            if(line.Split(' ').Length != 2) throw new ArgumentException("El atributo Shadow tiene formato incorrecto");
            var attribute = line.Split(' ')[INDEXATTR];
            if(!attribute.Equals("1") && !attribute.Equals("off") ) throw new ArgumentException("Comando para el atributo shadow incorrecto. Se esperaba: \"1\" u \"off\" y se obtuvo:" + attribute);
            return attribute;
        }
    }
}
