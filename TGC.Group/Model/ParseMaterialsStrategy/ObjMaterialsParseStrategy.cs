using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParseMaterialsStrategy
{
    public abstract class ObjMaterialsParseStrategy
    {
        internal const string NEWMTL = "newmtl";
        internal const string NS = "Ns";
        internal const string KA = "Ka";
        internal const string KD = "Kd";
        internal const string KE = "Ke";
        internal const string NI = "Ni";
        internal const string D = "d";
        internal const string ILUMN = "ilumn";
        internal const string COMMENT = "#";
        internal const string WHITELINE = "      ";
        internal const string EMPTYLINE = "";
   

        internal string Keyword = null;
  

        public abstract void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh);

        public virtual bool ResponseTo(string action)
        {
        return action == Keyword;
        }
    }
}
