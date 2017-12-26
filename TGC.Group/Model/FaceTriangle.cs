using System;

namespace TGC.Group.Model
{
    public class FaceTriangle
    {
        private string[] _vertexSplit;
        private int _cantSplit;

        internal uint V1 { get; set; }
        internal uint Vt1 { get; set; }
        internal uint Vn1 { get; set; }
        internal uint V2 { get; set; }
        internal uint Vt2 { get; set; }
        internal uint Vn2 { get; set; }
        internal uint V3 { get; set; }
        internal uint Vt3 { get; set; }
        internal uint Vn3 { get; set; }

        public FaceTriangle() { }

        public FaceTriangle(string[] vertexSplit, int cantSplit)
        {
            SetValues(vertexSplit, cantSplit);
        }

        private void SetValues(string[] vertexSplit, int cantSplit)
        {
            throw new NotImplementedException();
        }



        /*   switch (items.Length)
           {
               case 1:
                   if (!string.IsNullOrWhiteSpace(items[0])) face.v1 = uint.Parse(items[0], Style, Info);
                   break;
               case 2:
                   if (!string.IsNullOrWhiteSpace(items[0])) item.Vertex = uint.Parse(items[0], Style, Info);
                   if (!string.IsNullOrWhiteSpace(items[1])) item.Texture = uint.Parse(items[1], Style, Info);
                   break;
               case 3:
                   if (!string.IsNullOrWhiteSpace(items[0])) item.Vertex = uint.Parse(items[0], Style, Info);
                   if (!string.IsNullOrWhiteSpace(items[1])) item.Texture = uint.Parse(items[1], Style, Info);
                   if (!string.IsNullOrWhiteSpace(items[2])) item.Normal = uint.Parse(items[2], Style, Info);
                   break;
           }
        */
    }
}