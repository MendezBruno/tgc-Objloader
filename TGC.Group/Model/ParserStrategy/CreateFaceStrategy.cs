using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Model.ParserStrategy
{
    public class CreateFaceStrategy : ObjParseStrategy
    {
        public CreateFaceStrategy()
        {
            Keyword = FACE;
        }

        public override void ProccesLine(string line, List<ObjMesh> listObjMesh)
        {
            var f = line.Remove(0, 2).Split(' ');
            int cantSplit = f[0].Split('/').Length;
            try
            {
                CheckFormatCorrect(cantSplit);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: ", e);
            }
            FaceTriangle face = new FaceTriangle(f);
            listObjMesh.Last().FaceTrianglesList.Add(face);
        }

        private void CheckFormatCorrect(int cantSplit)
        {
            if (cantSplit == 4) throw new ArgumentException("Formato no soportado");
            if (cantSplit != 4 && cantSplit != 3) throw new ArgumentException("Formato invalido");
            if (cantSplit > 3 || cantSplit == 0) throw new ArgumentException("Cantidad de argumentos invalidos");

        }
    }
}
