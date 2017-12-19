using System.Collections.Generic;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateNewMeshStrategy : ObjParseStrategy
    {
        const int nameObject = 1;
        public CreateNewMeshStrategy()
        {
            Keyword = OBJECT;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var split = line.Split(' ');
            listObjMesh.Add(new ObjMesh(split[nameObject]));
        }
    }
}
