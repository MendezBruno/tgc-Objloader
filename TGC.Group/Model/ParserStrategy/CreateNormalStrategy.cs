using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DirectX;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateNormalStrategy : ObjParseStrategy
    {
        public CreateNormalStrategy()
        {
            Keyword = NORMAL;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var indices = line.Split(' ');
            if (indices.Length != 4 && indices.Length != 3) throw new ArgumentException("El Archivo .obj no fue exportado de forma triangular");
            var vertex = CreateVector3(line);
            listObjMesh.Last().VertexListVn.Add((Vector3)vertex);
        }
    }
}