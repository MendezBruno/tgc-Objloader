using System.Collections.Generic;
using System.Linq;

namespace TGC.Group.Model.ParseMaterialsStrategy
{
    internal class NoOperationStrategyForMaterial : ObjMaterialsParseStrategy
    {
        private readonly string[] _keyWords;

        public NoOperationStrategyForMaterial()
        {
            this._keyWords = new[] { COMMENT, EMPTYLINE, WHITELINE };
        }

        public override void ProccesLine(string line, List<ObjMaterialMesh> listObjMaterialMesh)
        {
            //There is no need to perform any operation.
        }

        public override bool ResponseTo(string action)
        {
            return this._keyWords.Contains(action);
        }
    }
}