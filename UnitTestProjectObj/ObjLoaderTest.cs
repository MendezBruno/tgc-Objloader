using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TGC.Group.Model;
using System.IO;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProjectObj
{



    [TestFixture]
    class ObjLoaderTest
    {
        TgcObjLoader _tgcObjLoader = new TgcObjLoader();
        string _fullobjpath;
        string _fullobjpathHome;

        [SetUp]
        public void Init()
        {
           // _fullobjpath = _tgcObjLoader.GetPathObjforCurrentDirectory();
           _fullobjpath = @"I:\proyectos net\tgc-group\UnitTestProjectObj\DatosPrueba\cubo.obj";
            _fullobjpathHome = @"D:\workspace\proyectosnet\tgc-Objloader\UnitTestProjectObj\DatosPrueba\cubo.obj";
        }

     /*   [TestCase]
        public void LoadObjFromFileOk()
        {
            string path = tgcObjLoader.getPathObj();

            NUnit.Framework.Assert.True(); El resultado de este test deberia ser una lista de ObjMesh

        }
     */

        [TestCase]
        public void GetArrayLines()
        {
            string[] lines = System.IO.File.ReadAllLines(_fullobjpath);
            NUnit.Framework.Assert.True(lines.Length > 0);
        }

        
        [TestCase]
        public void ProcessLineReturnWithLineBlanck()
        {
            var line = "";
            _tgcObjLoader.ProccesLine(line);
            Assert.IsEmpty(_tgcObjLoader.ListObjMesh); 
        }

        [TestCase]
        public void ProcessLineReturnWithSpaceBlanck()
        {
            string line = "        ";
            _tgcObjLoader.ProccesLine(line);
            Assert.IsEmpty(_tgcObjLoader.ListObjMesh);
        }

        [TestCase]
        public void ProcessLineReturnWithFirsCaracterHastag()
        {
            string line = "# Blender v2.79 (sub 0) OBJ File: ''";
            _tgcObjLoader.ProccesLine(line);
            Assert.IsFalse(_tgcObjLoader.ListObjMesh.Count == 0);
        }

        [Test]
        public void ProcessLineThrowWithBadAction()
        {
            string line = "badAction Blender v2.79 (sub 0) OBJ File: ''";
            Assert.That(() =>
                {
                    _tgcObjLoader.ProccesLine(line);
                }, Throws.InvalidOperationException

            );
        }

        [Test]
        public void ProccesLineNewObjet()
        {
            string line = "o Cube";
            _tgcObjLoader.ProccesLine(line);
            Assert.True(_tgcObjLoader.ListObjMesh.First().Name.Equals("Cube") );
        }

        /*
        [Test]
        public void ProccesLineWithVertex()
        {
            string line = "v 1.000000 - 1.000000 - 1.000000";
            TgcObjLoader.ProccesLine(line);
            Assert.True(TgcObjLoader.Delegator.CurrentObjMesh.VertexListV.Count > 0);
        }

        [Test]
        public void CreateVectorFaliedForCurrentMeshNull()
        {
            string line = "v 1.000000 - 1.000000 - 1.000000";
            Assert.That(() =>
                {
                    TgcObjLoader.ProccesLine(line);
                }, Throws.InvalidOperationException

            );

        }

        [Test]
        public void CreateVectorFaliedForFormatInvalid()
        {
            
        }
        */

    }
}
