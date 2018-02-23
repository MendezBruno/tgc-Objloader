using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace TGC.Group.Model
{
    public class ObjMaterialsLoader
    {
         const string SEPARADOR = "\\";

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
                string pathMaterial = Path.GetDirectoryName(pathMtllib) +SEPARADOR+ mtllib;
                if (File.Exists(pathMaterial))  //TODO agregar el retroceso de carpeta
                {
                    ParseMtlLib(pathMaterial);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot find action file: {mtllib}");
                }
              
            }
        }

        public string GetPathMaterial(string pathMtllib, string mtllib)
        {
            return Path.GetDirectoryName(pathMtllib) +SEPARADOR+ mtllib;
        }

        private static void ParseMtlLib(string path)
        {
            //Se leen todas las lineas
            var lines = File.ReadAllLines(path);

            //separar las lineas en sentencias
            //var statements = splitForStatements(lines);

            //Parse de las sentencias
            foreach (var line in lines)
                ProccesLine(line);
        }

        /*
        private static List<string[]> splitForStatements(string[] lines)
        {
            List<string> statements = new List<string>();
            List<string> statement = new List<string>();
            foreach (var line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    statement.Add(line);
                }
                else
                {
                    statements.Add(statement);
                    List<string> statement = new List<string>();

                }

            }
        }*/

        private static void ProccesLine(string line)
        {
            var action = line.Split(' ').FirstOrDefault();
            if (action == null && !String.IsNullOrWhiteSpace(line)) throw new InvalidOperationException($"Cannot find action for this line {line}");

            foreach (var strategy in Strategies)
                if (strategy.ResponseTo(action))
                {
                    strategy.ProccesLine(line, ListObjMaterialMesh);
                    return;
                }
            throw new InvalidOperationException($"Cannot find a correct parsing process for line {line}"); 
        }
    }
}