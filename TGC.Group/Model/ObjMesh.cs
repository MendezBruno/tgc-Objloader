using System.Collections.Generic;
using Microsoft.DirectX;

namespace TGC.Group.Model
{
    public class ObjMesh
    {
        public ObjMesh()
        {
        }

        public ObjMesh(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Mtllib { get; set; }
        public string Usemtl { get; set; }
        public bool Shadow { get; set; } = false;
        public List<Vector3> VertexListV { get; set; } = new List<Vector3>();
        public List<Vector3> VertexListVt { get; set; } = new List<Vector3>();
        public List<Vector3> VertexListVn { get; set; } = new List<Vector3>();
        public List<FaceTriangle> FaceTrianglesList { get; set; } = new List<FaceTriangle>();
    }
}