using System.Collections.Generic;
using System.Linq;
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
       // public string Mtllib { get; set; }
        public bool Shadow { get; set; } = false;
        public new List<string> Usemtl = new List<string>();
        public List<Vector3> VertexListV { get; set; } = new List<Vector3>();
        public List<Vector2> VertexListVt { get; set; } = new List<Vector2>();
        public List<Vector3> VertexListVn { get; set; } = new List<Vector3>();
        public List<FaceTriangle> FaceTrianglesList { get; set; } = new List<FaceTriangle>();

        public int[] CreateMaterialIdsArray()
        {
            int[] materialsId = new int[FaceTrianglesList.Count];
            var numMaterial = 0;
            var index = 0;
            var usemtl = Usemtl.First();
            FaceTrianglesList.ForEach((face) =>
            {
                if (!usemtl.Equals(face.Usemtl))
                {
                    numMaterial++;
                }
                materialsId[index] = numMaterial;
                index++;
            });
            return materialsId;
        }
    }


   
}