using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace TGC.Group.Model.ParseMaterialsStrategy
{
    public abstract class ObjMaterialsParseStrategy
    {
        //variable keywords que pertenecen a la sentencia de Color
        internal const string Newmtl = "newmtl";
        internal const string Ns = "Ns";
        internal const string Ka = "Ka";
        internal const string Kd = "Kd";
        internal const string Ks = "Ks";
        internal const string Ke = "Ke";
        internal const string Ni = "Ni";
        internal const string d = "d";
        internal const string illum = "illum";
        // variable keywords que pertenecen a la sentencia de textura
        internal const string map_Kd = "map_Kd";
        internal const string disp = "disp";
        internal const string map_bump = "map_bump";
        internal const string map_Ka = "map_Ka";
        internal const string map_ks = "map_ks";
        internal const string map_d = "map_d";
        // otras keywords
        internal const string COMMENT = "#";
        internal const string WHITELINE = "      ";
        internal const string EMPTYLINE = "";


        internal string Keyword = null;
        private NumberStyles Style { get; } = NumberStyles.Any;
        private CultureInfo Info { get; } = CultureInfo.InvariantCulture;


        public abstract void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh);

        public virtual bool ResponseTo(string action)
        {
        return action == Keyword;
        }

        public ColorValue CreateColorValue(string line) //TODO esto podria ir en una clase utils porque se repite para Obj
        {
           var indices = line.Split(' ');
           if (indices.Length != 4) throw new ArgumentException("El Archivo esta corrupto o tiene cantidad incorrecta de parametros");
           return  new ColorValue
                (
                    float.Parse(indices[1], Style, Info),
                    float.Parse(indices[2], Style, Info),
                    float.Parse(indices[3], Style, Info)
                );
         }

        public float ParseLineToFLoatValue(string line)
        {
            var indices = line.Split(' ');
            if (indices.Length != 2) throw new ArgumentException("El Archivo esta corrupto o tiene cantidad incorrecta de parametros");
            return float.Parse(indices[1], Style, Info);
        }

        public int ParseLineToIntValue(string line)
        {
            var indices = line.Split(' ');
            if (indices.Length != 2) throw new ArgumentException("El Archivo esta corrupto o tiene cantidad incorrecta de parametros");
            return int.Parse(indices[1], Style, Info);
        }

        public object ParseLineToStringValue(string line)
        {
            var indices = line.Split(' ');
            if (indices.Length != 2) throw new ArgumentException("El Archivo esta corrupto o tiene cantidad incorrecta de parametros");
            return indices[1];
        }
    }
}
