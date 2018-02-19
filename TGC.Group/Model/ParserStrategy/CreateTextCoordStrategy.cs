using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DirectX;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateTextCoordStrategy : ObjParseStrategy
    {
        public CreateTextCoordStrategy()
        {
            Keyword = TEXTURE;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var indices = line.Split(' ');
            if (indices.Length != 3) throw new ArgumentException("El Archivo .obj no fue exportado de forma triangular");
            var vertex = CreateVector3(line);
            listObjMesh.Last().VertexListVt.Add((Vector2)vertex);
        }
    }
}