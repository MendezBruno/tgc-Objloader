using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParseMaterialsStrategy
{
    public class CreateNewMaterialStrategy: ObjMaterialsParseStrategy
    {
        private const int nameMaterial = 1;

        public CreateNewMaterialStrategy()
        {
            Keyword = NEWMTL;
        }

        public override void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh)
        {
            var split = line.Split(' ');
            listObjMaterialMesh.Add(new ObjMaterialMesh(split[nameMaterial]));
        }

       
    }
}
