using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TGC.Group.Model.ParserStrategy;

namespace TGC.Group.Model
{
    public class TgcObjLoader
    {
        public TgcObjLoader()
        {
            Strategies = new List<ObjParseStrategy>();
            Strategies.Add(new CreateNewMeshStrategy());
            Strategies.Add(new CreateNormalStrategy());
            Strategies.Add(new CreateFaceStrategy());
            Strategies.Add(new CreateTextCoordStrategy());
            Strategies.Add(new CreateVertexStrategy());
            Strategies.Add(new NoOperationStrategy());
            ListObjMesh = new List<ObjMesh>();
           
        }

        public List<ObjParseStrategy> Strategies { get; set; }

        public List<ObjMesh> ListObjMesh { get; set; }

        public string GetPathObj()
        {
            var openFileDialog1 = new OpenFileDialog
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
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
                ProccesLine(line);
        }

        public void ProccesLine(string line)
        {
            var action = line.Split(' ').FirstOrDefault();
            if (action == null) throw new InvalidOperationException($"Cannot find action for this line {line}");

            foreach (var strategy in Strategies)
                if (strategy.ResponseTo(action))
                {
                    strategy.ProccesLine(line, ListObjMesh);
                    return;
                }
            throw new InvalidOperationException($"Cannot find a correct parsing process for line {line}");
        }
    }
}