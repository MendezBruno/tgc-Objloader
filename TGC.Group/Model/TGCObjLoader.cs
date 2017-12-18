using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TGC.Group.Model.ParserStrategy;


namespace TGC.Group.Model
{
    public class TgcObjLoader
    {
        //public readonly ObjParsingDelagator Delegator = new ObjParsingDelagator();
        //internal Dictionary<string, Action<string>> ActionsDictionary;
        //refactor
        public List<ObjParseStrategy> Strategies { get; set; }
        public List<ObjMesh> ListObjMesh { get; set; }


        public TgcObjLoader()
        {
            Strategies = new List<ObjParseStrategy>();
            Strategies.Add(new CreateNewMeshStrategy());
            ListObjMesh = new List<ObjMesh>();
           /* ActionsDictionary = new Dictionary<string, Action<string>>
            {
                ["o"] = Delegator.CreateNewMesh,
                ["v"] = Delegator.CreateVertex,
                ["vt"] = Delegator.CreateTextCoord,
                ["vn"] = Delegator.CreateNormal,
                ["f"] = Delegator.CreateFace,
                ["mtllib"] = (process) => { },
                ["usemtl"] = (process) => { },
                ["s"] = process => { }
            };
            */
        }

        public string GetPathObj()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "obj files (*.obj)|*.obj|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };


            return openFileDialog1.ShowDialog() == DialogResult.OK ? openFileDialog1.FileName : "";
        }

        public string GetPathObjforCurrentDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), @"UnitTestProjectObj\DatosPrueba\cubo.obj");
        }

        public void LoadObjFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                ProccesLine(line);
            }


        }

        public void ProccesLine(string line)
        {
            //string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // TODO CAMBIAR ESTO POR UN NOOP
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) return;

            string action = line.Split(' ').FirstOrDefault();
            //TODO COMPLETAR if (action != null) ;

            foreach (var strategy in Strategies)
            {
                if (strategy.ResponseTo(action))
                {
                    strategy.ProccesLine(line, ListObjMesh);
                    return;
                }
            }

            throw new InvalidOperationException($"Cannot find a correct parsing process for line {line}");

            //string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            /*if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) return false;
            string action = line.Split(' ').FirstOrDefault();
            if (action != null && ActionsDictionary.ContainsKey(action))
            {
                ActionsDictionary[action].Invoke(line);
            }
            else
            {
                throw new InvalidOperationException ($"Cannot find a correct parsing process for line {line}");
            }
            return true;*/
        }
    }


}
