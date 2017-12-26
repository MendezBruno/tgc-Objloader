using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.DirectX;
using NUnit.Framework.Constraints;
using TGC.Core.Geometry;
using TGC.Core.Utils;

namespace TGC.Group.Model
{
    public class ObjParsingDelagator
    {
        public List<ObjMesh> ListObjMesh { get; set; }
        public ObjMesh CurrentObjMesh;
        internal NumberStyles Style { get; } = NumberStyles.Any;
        internal CultureInfo Info { get; } = CultureInfo.InvariantCulture;

        public ObjParsingDelagator()
        {
            CurrentObjMesh = null;
        }

        public void CreateNewMesh(string obj)
        {
            if(CurrentObjMesh == null)  CurrentObjMesh = new ObjMesh(); 
            else { ListObjMesh.Add(CurrentObjMesh); CurrentObjMesh = new ObjMesh(); }
        }

        public void CreateVertex(string line)
        {
            Vector3 vertex = CreateVector3(line);
            CurrentObjMesh.VertexListV.Add(vertex);
        }

        public void CreateTextCoord(string line)
        {
            Vector3 vertex = CreateVector3(line);
            CurrentObjMesh.VertexListVt.Add(vertex);
        }

        public void CreateNormal(string line)
        {
            Vector3 vertex = CreateVector3(line);
            CurrentObjMesh.VertexListVt.Add(vertex);
        }

        public void CreateFace(string line)
        {
            var vertexSplit = line.Remove(0, 2).Split(' ');
            string[] cantSplit = vertexSplit[0].Split('/');
           // FaceTriangle face = new FaceTriangle(vertexSplit, cantSplit);
           // CurrentObjMesh.FaceTrianglesList.Add(face);
            
        }



        public Vector3 CreateVector3(string line)
        {
            if (CurrentObjMesh == null) throw new ArgumentException("No existe ningun objeto para cargar los respectivos vertices");
            string[] indices = line.Split(' ');
            if (indices.Length != 4) throw new ArgumentException("El Archivo obj no fue exportado de forma triangular");
            Vector3 vertex = new Vector3()
            {
                X = float.Parse(indices[1], Style, Info),
                Y = float.Parse(indices[2], Style, Info),
                Z = float.Parse(indices[3], Style, Info)
            };
            return vertex;
        }



    }
}