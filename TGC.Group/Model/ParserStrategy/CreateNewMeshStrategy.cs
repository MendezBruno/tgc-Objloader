using System.Collections.Generic;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateNewMeshStrategy : ObjParseStrategy
    {
        private const int nameObject = 1;

        public CreateNewMeshStrategy()
        {
            Keyword = OBJECT;
        }

        public override void ProccesLine(string line, ObjMeshContainer objMeshContainer)
        {
            var split = line.Split(' ');
            objMeshContainer.ListObjMesh.Add(new ObjMesh(split[nameObject]));
        }
    }
}