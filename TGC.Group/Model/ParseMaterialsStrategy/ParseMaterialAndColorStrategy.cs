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
        private readonly  string[] _ColorVariables;
        private readonly string[] _vectorVariables;
        private string[] _pathTextureVariables;

        public ParseMaterialAndColorStrategy()
        {
            this._keyWords = new string[] { Ns, Ka, Kd, Ks, Ke, Ni, d, illum, map_Kd, disp, map_bump, map_Ka, map_ks, map_d };
            this._ColorVariables = new string[] { Ka, Kd, Ks, Ke};
            this._vectorVariables = new string[] { Ns, Ni, d };
            this._pathTextureVariables = new string[] { map_Kd, disp, map_bump, map_Ka, map_ks, map_d };
        }

        //TODO MONO este proccesLine esta bien feito, refactorizarlo 
       public override void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh)
       {
           string key = line.Split(Espacio).FirstOrDefault();
           ObjMaterialMesh auxObjMaterialMesh = listObjMaterialMesh.Last();
           PropertyInfo pInfo = auxObjMaterialMesh.GetType().GetProperty(key);
           if (pInfo == null) throw new ArgumentException("No se encuantra el atributo de clase con la key: ", key);
           if (this._ColorVariables.Contains(key))
           {
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

           if (this._pathTextureVariables.Contains(key))
           {
               pInfo.SetValue(auxObjMaterialMesh, ParsePathToStringExistValue(line), null);
           }

        }
        
        public override bool ResponseTo(string action)
        {
            return this._keyWords.Contains(action);
        }
    }
}
