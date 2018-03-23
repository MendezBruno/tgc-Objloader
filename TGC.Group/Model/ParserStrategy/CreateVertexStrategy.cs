using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DirectX;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateVertexStrategy : ObjParseStrategy
    {
        public CreateVertexStrategy()
        {
            Keyword = VERTEX;
        }

        public override void ProccesLine(string line, ObjMeshContainer objMeshContainer)
        {
            var indices = line.Split(' ');
            if (indices.Length != 4) throw new ArgumentException("El Archivo .obj no fue exportado de forma triangular");
            var vertex = CreateVector3(line);
            objMeshContainer.VertexListV.Add((Vector3)vertex);
        }
    }
}