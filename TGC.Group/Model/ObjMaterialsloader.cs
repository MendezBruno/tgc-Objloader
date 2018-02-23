using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace TGC.Group.Model
{
    public class ObjMaterialsLoader
    {
        public ObjMaterialsLoader()
        {
            Strategies = new List<ObjMaterialsParseStrategy>();
            ListObjMaterialMesh = new List<ObjMaterialMesh>();
        }

        public static List<ObjMaterialsParseStrategy> Strategies { get; set; }
        public static List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }

        public static void LoadMaterialsFromFiles(string pathMtllib, List<string> listMtllib)
        {
            foreach (string mtllib in listMtllib)
            {
                if (File.Exists(pathMtllib + "" + mtllib))  //TODO agregar el retroceso de carpeta
                {
                    ParseMtlLib(pathMtllib + "" + mtllib);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot find action file: {mtllib}");
                }
              
            }
        }

        private static void ParseMtlLib(string path)
        {
            //Se leen todas las lineas
            var lines = File.ReadAllLines(path);

            //separar las lineas en sentencias
            var statements = splitForstatements(lines);

            //Parse de las sentencias
            foreach (var statement in statements)
                ProccesStatement(statement);
        }

        private static List<string[]> splitForstatements(string[] lines)
        {
            throw new NotImplementedException();
        }

        private static void ProccesStatement(string[] line)
        {

            //por Cada sentencia completamos el objeto ObjMaterialMesh creado.
         /*   var action = line.Split(' ').FirstOrDefault();
            if (action == null && !String.IsNullOrWhiteSpace(line)) throw new InvalidOperationException($"Cannot find action for this line {line}");

            foreach (var strategy in Strategies)
                if (strategy.ResponseTo(action))
                {
                    strategy.ProccesLine(line, ListObjMaterialMesh);
                    return;
                }
            throw new InvalidOperationException($"Cannot find a correct parsing process for line {line}"); */
        }
    }
}