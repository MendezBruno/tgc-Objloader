using System.Collections.Generic;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateNewMeshStrategy : ObjParseStrategy
    {
        public CreateNewMeshStrategy()
        {
            Keyword = OBJECT;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            listObjMesh.Add(new ObjMesh());
        }
    }
}
