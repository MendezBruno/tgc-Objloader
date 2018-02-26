using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TGC.Group.Model.ParseMaterialsStrategy;

namespace TGC.Group.Model
{
    public class ObjMaterialsLoader
    {
        const string Separador = "\\";

        public ObjMaterialsLoader()
        {
            Strategies = new List<ObjMaterialsParseStrategy>();
            Strategies.Add(new CreateNewMaterialStrategy());
            Strategies.Add(new ParseMaterialAndColorStrategy());
            Strategies.Add(new NoOperationStrategyForMaterial());
            ListObjMaterialMesh = new List<ObjMaterialMesh>();
        }

        public List<ObjMaterialsParseStrategy> Strategies { get; set; }
        public List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }

        public void LoadMaterialsFromFiles(string pathMtllib, List<string> listMtllib)
        {
            foreach (string mtllib in listMtllib)
            {
                string pathMaterial = Path.GetDirectoryName(pathMtllib) +Separador+ mtllib;
                if (File.Exists(pathMaterial))  
                {
                    ParseMtlLib(pathMaterial);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot find file: {mtllib}");
                }
              
            }
        }

        public string GetPathMaterial(string pathMtllib, string mtllib)
        {
            return Path.GetDirectoryName(pathMtllib) +Separador+ mtllib;
        }

        private void ParseMtlLib(string path)
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

        private void ProccesLine(string line)
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