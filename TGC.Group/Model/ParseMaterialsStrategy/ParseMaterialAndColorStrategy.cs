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
        private const char  Espacio = ' ';
        private readonly  string[] _vector3Variables;
        private readonly string[] _vectorVariables;

        public ParseMaterialAndColorStrategy()
        {
            this._keyWords = new string[] { Ns, Ka, Kd, Ks, Ke, Ni, d, illum };
            this._vector3Variables = new string[] { Ka, Kd, Ks, Ke};
            this._vectorVariables = new string[] { Ns, Ni, d };
        }

       public override void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh)
       {
           string key = line.Split(Espacio).FirstOrDefault();
           ObjMaterialMesh auxObjMaterialMesh = listObjMaterialMesh.Last();
           PropertyInfo pInfo = auxObjMaterialMesh.GetType().GetProperty(key);
           if (pInfo == null) throw new ArgumentException("No se encuantra el atributo de clase con la key: ", key);
            if (this._vector3Variables.Contains(key))
           {
               //= createColoValue(line);
               pInfo.SetValue(auxObjMaterialMesh, CreateColorValue(line), null);
               
           }

           if (this._vectorVariables.Contains(key))
           {
               pInfo.SetValue(auxObjMaterialMesh, ParseLineToFLoatValue(line), null);
            }

           if (key.Equals(illum))
           {
               pInfo.SetValue(auxObjMaterialMesh, ParseLineToIntValue(line), null);
            }

        }

        public override bool ResponseTo(string action)
        {
            return this._keyWords.Contains(action);
        }
    }
}
