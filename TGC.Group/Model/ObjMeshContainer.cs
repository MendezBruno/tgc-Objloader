﻿using Microsoft.DirectX;
using System.Collections.Generic;

namespace TGC.Group.Model
{
    public class ObjMeshContainer
    {
        public List<Vector3> VertexListV { get; set; }
        public List<Vector2> VertexListVt { get; set; }
        public List<Vector3> VertexListVn { get; set; }
        public List<ObjMesh> ListObjMesh { get; set; }

        public ObjMeshContainer()
        {
            VertexListV = new List<Vector3>();
            VertexListVt = new List<Vector2>();
            VertexListVn = new List<Vector3>();
            ListObjMesh = new List<ObjMesh>();
        }
    }
}