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
        internal const string NEWMTL = "newmtl";
        internal const string NS = "Ns";
        internal const string KA = "Ka";
        internal const string KD = "Kd";
        internal const string KS = "Ks";
        internal const string KE = "Ke";
        internal const string NI = "Ni";
        internal const string D = "d";
        internal const string ILUMN = "ilumn";
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

        public Object CreateVector3(string line) //TODO esto podria ir en una clase utils porque se repite para Obj
        {
            var vertex = new Vector3();
            var indices = line.Split(' ');
            if (indices.Length != 4) throw new ArgumentException("El Archivo esta corrupto o tiene cantidad incorrecta de parametros");
           vertex = new Vector3
                {
                    X = float.Parse(indices[1], Style, Info),
                    Y = float.Parse(indices[2], Style, Info),
                    Z = float.Parse(indices[3], Style, Info)
                };
           
            return vertex;
        }
    }
}
