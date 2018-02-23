﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    class ObjMaterialLoaderTest
    {
        ObjMaterialsLoader _objMaterialLoader = new ObjMaterialsLoader();
        private string _fullMaterialPath;

        [SetUp]
        public void Init()
        {
            const string testDataArchivoBle = "DatosPrueba\\cubo.obj";  //TODO agregar el archivo para testear
            const string testDataArchivoBla = "DatosPrueba\\cubocontextura.obj";  //TODO agregar el archivo para testear

            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }
            _fullMaterialPath = Path.Combine(dir.Parent.FullName, testDataArchivoBle);
        }


        [TestCase]
        public void LoadObjMaterialFromFileOk()
        {
            
        }

        [TestCase]
        public void GetArrayLinesOk()
        {
            var lines = File.ReadAllLines(_fullMaterialPath);
            Assert.True(lines.Length > 0);
        }
    }
}