using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TGC.Core.Geometry;

namespace TGC.Group.Model
{
    public class ObjMesh
    {
        public string Name { get; set; }
        public List<Vector3> VertexListV  { get; set; }
        public List<Vector3> VertexListVt { get; set; }
        public List<Vector3> VertexListVn { get; set; }
        public List<FaceTriangle> FaceTrianglesList { get; set; }

        public ObjMesh(){}

        public ObjMesh(string name)
        {
            this.Name = name;
        }
    }
}