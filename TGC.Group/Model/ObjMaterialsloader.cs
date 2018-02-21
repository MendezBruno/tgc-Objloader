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

        }

        public static List<ObjMaterialsParseStrategy> Strategies { get; set; }
        public static List<ObjMaterialMesh> ListObjMaterialMesh { get; set; }

        public static void LoadMaterialsFromFiles(string pathMtllib, List<string> listMtllib)
        {


            foreach (string mtllib in listMtllib)
            {
                ParseMtlLib(pathMtllib + "" + mtllib);
            }

            
        }

        private static void ParseMtlLib(string path)
        {
            //Se leen todas las lineas
            var lines = File.ReadAllLines(path);

            //Se Parse de los objetos
            foreach (var line in lines)
                ProccesLine(line);
        }

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