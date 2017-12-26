using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateFaceStrategy : ObjParseStrategy
    {
        internal const int VERTEX = 0;
        internal const int TEXTURE = 1;
        internal const int NORMAL = 2;


        public CreateFaceStrategy()
        {
            Keyword = FACE;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var f = line.Remove(0, 2).Split(' ');
            var arrayVertex1 = f[0].Split('/');
            var arrayVertex2 = f[1].Split('/');
            var arrayVertex3 = f[2].Split('/');
            try
            {
                CheckFormatCorrect(arrayVertex1.Length);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: ", e);
            }
            
            FaceTriangle face = new FaceTriangle(arrayVertex1[VERTEX], arrayVertex2[VERTEX], arrayVertex3[VERTEX]); ;
            if ((arrayVertex1.Length + arrayVertex2.Length + arrayVertex3.Length) == 6) face.SetTexturesValues(arrayVertex1[TEXTURE], arrayVertex2[TEXTURE], arrayVertex3[TEXTURE]); //Igual a 6 quiere decir que los tres vertices tienen un valor de textura y uno de posicion 
            else
            {
                if (!string.IsNullOrWhiteSpace(arrayVertex1[TEXTURE])) face.SetTexturesValues(arrayVertex1[TEXTURE], arrayVertex2[TEXTURE], arrayVertex3[TEXTURE]);
                face.SetNormalValues(arrayVertex1[NORMAL], arrayVertex2[NORMAL], arrayVertex3[NORMAL]);
            }
            listObjMesh.Last().FaceTrianglesList.Add(face);
        }

        private void CheckFormatCorrect(int cantSplit)
        {
            if (cantSplit == 4) throw new ArgumentException("Formato no soportado");
            //if (cantSplit != 4 && cantSplit != 3) throw new ArgumentException("Formato invalido");
            if (cantSplit > 3 || cantSplit == 0) throw new ArgumentException("Cantidad de argumentos invalidos");

        }
    }
}
