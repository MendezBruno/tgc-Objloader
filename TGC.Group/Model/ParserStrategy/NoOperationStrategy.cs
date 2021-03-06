﻿using System.Linq;

namespace TGC.Group.Model.ParserStrategy
{
    public class NoOperationStrategy : ObjParseStrategy
    {
        private readonly string[] _keyWords;

        public NoOperationStrategy()
        {
            this._keyWords = new string[] { COMMENT, EMPTYLINE, WHITELINE, MTLLIB };
        }

        public override void ProccesLine(string line, ObjMeshContainer objMeshContainer)
        {
            return;
        }

        public override bool ResponseTo(string action)
        {
            return this._keyWords.Contains(action);
        }
    }
}