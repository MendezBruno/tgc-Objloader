﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateFaceStrategy : ObjParseStrategy
    {
        internal const int VERTEX = 0;
        internal const int TEXTURE = 1;
        internal const int NORMAL = 2;
        internal const int COMPLETEARRAY = 6; //Igual a 6 quiere decir que los tres vertices tienen un valor de textura y uno de posicion 
        internal const char TYPEVERTEXDELIMITER = '/';
        internal const char VERTEXDELIMITER = ' ';


        public CreateFaceStrategy()
        {
            Keyword = FACE;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {


            var f = CheckFaceFormatCorrect(line);
            var arrayVertex1 = CheckTriangleFormatCorrect(f[0]);
            var arrayVertex2 = CheckTriangleFormatCorrect(f[1]);
            var arrayVertex3 = CheckTriangleFormatCorrect(f[2]);

            var face = new FaceTriangle(arrayVertex1[VERTEX], arrayVertex2[VERTEX], arrayVertex3[VERTEX]);
            ;
            if (arrayVertex1.Length + arrayVertex2.Length + arrayVertex3.Length == COMPLETEARRAY)
            {
                face.SetTexturesValues(arrayVertex1[TEXTURE], arrayVertex2[TEXTURE], arrayVertex3[TEXTURE]);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(arrayVertex1[TEXTURE]))
                    face.SetTexturesValues(arrayVertex1[TEXTURE], arrayVertex2[TEXTURE], arrayVertex3[TEXTURE]);
                face.SetNormalValues(arrayVertex1[NORMAL], arrayVertex2[NORMAL], arrayVertex3[NORMAL]);
            }
            face.Usemtl = listObjMesh.Last().Usemtl.Last();
            listObjMesh.Last().FaceTrianglesList.Add(face);
        }

        private string[] CheckTriangleFormatCorrect(string f)
        {
            var arrayVertex = f.Split(TYPEVERTEXDELIMITER);
            if (arrayVertex.Length == 4) throw new ArgumentException("Formato no soportado, cantidad de vertices: 4 ");
            if (arrayVertex.Length > 4 || arrayVertex.Length == 0)
                throw new ArgumentException($"Cantidad de argumentos invalidos, se esperaban entre 1 y 3 y se obtuvieron: {arrayVertex.Length}");
            return arrayVertex;
        }

        private string[] CheckFaceFormatCorrect(string line)
        {
            var face = line.Remove(0, 2).Split(VERTEXDELIMITER);
            if (face.Length == 4) throw new ArgumentException("Formato no soportado, Se esperaba exportado en forma triangular");
            if (face.Length > 4 || face.Length == 0)
                throw new ArgumentException("Cantidad de argumentos invalidos");
            return face;
        }
    }
}