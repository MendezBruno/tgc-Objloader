using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.DirectX;

namespace TGC.Group.Model.ParserStrategy
{
    public abstract class ObjParseStrategy
    {
        internal const string OBJECT = "o";
        internal const string VERTEX = "v";
        internal const string TEXTURE = "vt";
        internal const string NORMAL = "vn";
        internal const string FACE = "f";
        internal const string MTLLIB = "mtllib";
        internal const string USEMTL = "usemtl";
        internal const string SHADOW = "s";
        internal const string COMMENT = "#";
        internal const string WHITELINE = "      ";
        internal const string EMPTYLINE = "";

        internal string Keyword = null;
        private NumberStyles Style { get; } = NumberStyles.Any;
        private CultureInfo Info { get; } = CultureInfo.InvariantCulture;

        public abstract void ProccesLine(string line, List<ObjMesh> listObjMesh);

        public virtual bool ResponseTo(string action)
        {
            return action == Keyword;
        }

        public Vector3 CreateVector3(string line)
        {
            var indices = line.Split(' ');
            if (indices.Length != 4) throw new ArgumentException("El Archivo .obj no fue exportado de forma triangular");
            var vertex = new Vector3
            {
                X = float.Parse(indices[1], Style, Info),
                Y = float.Parse(indices[2], Style, Info),
                Z = float.Parse(indices[3], Style, Info)
            };
            return vertex;
        }
    }
}