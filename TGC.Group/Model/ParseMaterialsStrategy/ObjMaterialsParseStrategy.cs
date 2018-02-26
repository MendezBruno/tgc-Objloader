using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.DirectX;

namespace TGC.Group.Model.ParseMaterialsStrategy
{
    public abstract class ObjMaterialsParseStrategy
    {
        internal const string Newmtl = "newmtl";
        internal const string Ns = "Ns";
        internal const string Ka = "Ka";
        internal const string Kd = "Kd";
        internal const string Ks = "Ks";
        internal const string Ke = "Ke";
        internal const string Ni = "Ni";
        internal const string d = "d";
        internal const string Ilumn = "illum";
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

        public Vector3 CreateVector3(string line) //TODO esto podria ir en una clase utils porque se repite para Obj
        {
           var indices = line.Split(' ');
           if (indices.Length != 4) throw new ArgumentException("El Archivo esta corrupto o tiene cantidad incorrecta de parametros");
           return  new Vector3
                {
                    X = float.Parse(indices[1], Style, Info),
                    Y = float.Parse(indices[2], Style, Info),
                    Z = float.Parse(indices[3], Style, Info)
                };
           
           
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
    }
}
