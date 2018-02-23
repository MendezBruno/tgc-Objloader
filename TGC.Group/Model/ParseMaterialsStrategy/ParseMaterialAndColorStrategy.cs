using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParseMaterialsStrategy
{
    public class ParseMaterialAndColorStrategy: ObjMaterialsParseStrategy
    {
        private readonly string[] _keyWords;
        private const char  ESPACIO = ' ';
        private readonly  string[] _vector3Variables;
        private readonly string[] _vectorVariables;

        public ParseMaterialAndColorStrategy()
        {
            this._keyWords = new string[] { NS, KA, KD, KS, KE, NI, D, ILUMN };
            this._vector3Variables = new string[] { KA, KD, KS, KE};
            this._vectorVariables = new string[] { NS, NI, D };
        }

       public override void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh)
       {
           string key = line.Split(ESPACIO).FirstOrDefault();
           if (this._vector3Variables.Contains(key))
           {
               listObjMaterialMesh.Last().GetType().GetMember(key); //= createVector3(line);
               //TODO asignar
           }

           if (this._vectorVariables.Contains(key))
           {
               listObjMaterialMesh.Last().GetType(); //= parsearFloat(line);
               //TODO asignar
           }

           if (key.Equals(ILUMN))
           {
               listObjMaterialMesh.Last().GetType(); //= asignar Ilum
               //TODO asignar
           }

        }

        public override bool ResponseTo(string action)
        {
            return this._keyWords.Contains(action);
        }
    }
}
