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

       /* public FaceTriangle(string[] vertexSplit, int cantSplit)
        {
            SetValues(vertexSplit, cantSplit);
        }
        */
        public FaceTriangle(string[] f)
        {
            int cantSplit = f[0].Split('/').Length;
           // SetValues(f, cantSplit);
           

        }
        /*
        private string[] getVertex(string[] f)
        {
            var v = new String[3];
            foreach (var vertex in f)
            {
                
            }


        }

        private void SetValues(string[] f, int cant)
        {
            switch (cant)
            {
                case 1:
                    SetOnlyVertex(getVertex(f));
                    break;
                case 2:
                    SetVertexWithoutNormal(f);
                    break;
                case 3:

                    break;
            }
        }

        private void SetVertexWithoutNormal(string[] f)
        {
            string[] auxVertex;
            foreach (var vertex in f)
            {
                var item = vertex.Split('/');
                auxVertex.
            }
        }

        private void SetOnlyVertex(string[] f)
        {
          V1 = uint.Parse(f[0], Style, Info);
          V2 = uint.Parse(f[1], Style, Info);
          V3 = uint.Parse(f[2], Style, Info);
        }

        private void SetValueV3(string v)
        {
            var items = v.Split('/');
            V3 = uint.Parse(items[0], Style, Info);
            if (!string.IsNullOrWhiteSpace(items[2])) Vt3 = uint.Parse(items[2], Style, Info);
            if (!string.IsNullOrWhiteSpace(items[2])) Vn3 = uint.Parse(items[2], Style, Info);
        }

        private void SetValueV2(string v)
        {
            var items = v.Split('/');
            V2 = uint.Parse(items[0], Style, Info);
            if (!string.IsNullOrWhiteSpace(items[1])) Vt2 = uint.Parse(items[1], Style, Info);
            if (!string.IsNullOrWhiteSpace(items[1])) Vn2 = uint.Parse(items[1], Style, Info);
        }

        private void SetValueV1(string v)
        {
            var items = v.Split('/');
            V1 = uint.Parse(items[0], Style, Info);
            if (!string.IsNullOrWhiteSpace(items[0])) Vt1 = uint.Parse(items[0], Style, Info);
            if (!string.IsNullOrWhiteSpace(items[0])) Vn1 = uint.Parse(items[0], Style, Info);
        }
        */

        
    }
}