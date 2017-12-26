using System;
using System.Globalization;
using NUnit.Framework.Constraints;

namespace TGC.Group.Model
{
    public class FaceTriangle
    {
        private string[] _vertexSplit;
        private int _cantSplit;
        internal NumberStyles Style { get; } = NumberStyles.Any;
        internal CultureInfo Info { get; } = CultureInfo.InvariantCulture;


        public uint V1 { get; set; }
        public uint Vt1 { get; set; }
        public uint Vn1 { get; set; }
        public uint V2 { get; set; }
        public uint Vt2 { get; set; }
        public uint Vn2 { get; set; }
        public uint V3 { get; set; }
        public uint Vt3 { get; set; }
        public uint Vn3 { get; set; }

        public FaceTriangle() { }
              
        public FaceTriangle(string v, string v1, string v2)
        {
            V1 = uint.Parse(v, Style, Info);
            V2 = uint.Parse(v1, Style, Info);
            V3 = uint.Parse(v2, Style, Info);
        }

        internal void SetTexturesValues(string v1, string v2, string v3)
        {
            Vt1 = uint.Parse(v1, Style, Info);
            Vt2 = uint.Parse(v2, Style, Info);
            Vt3 = uint.Parse(v3, Style, Info);
        }

        internal void SetNormalValues(string v1, string v2, string v3)
        {
            Vn1 = uint.Parse(v1, Style, Info);
            Vn2 = uint.Parse(v2, Style, Info);
            Vn3 = uint.Parse(v3, Style, Info);
        }
    }
}