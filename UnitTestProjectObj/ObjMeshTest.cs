using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TGC.Core.Direct3D;
using TGC.Group.Model;

namespace UnitTestProjectObj
{
    [TestFixture]
    public class ObjMeshTest
    {
        private string _fullobjpathmultimaterial;
        private ObjMesh _resObjMesh;
        private Panel _panel3D;

        [SetUp]
        public void Init()
        {
            //constantes
            const string testDatabb8Multimaterial = "Resources\\bb8\\bb8.obj";
            const string testDataCuboTextura = "Resources\\cubotexturacaja.obj";

            //Instanciamos un panel para crear un divice
            _panel3D = new Panel();
            //Crear Graphics Device
            //
            // panel3D
            //
            this._panel3D.Dock = DockStyle.Fill;
            this._panel3D.Location = new System.Drawing.Point(0, 0);
            this._panel3D.Name = "panel3D";
            this._panel3D.Size = new System.Drawing.Size(784, 561);
            this._panel3D.TabIndex = 0;

            D3DDevice.Instance.InitializeD3DDevice(_panel3D);

            //Creamos los materiales para luego poder probar la creacion del mesh
            var dir = new DirectoryInfo(Path.GetFullPath(TestContext.CurrentContext.TestDirectory));
            while (!dir.Parent.Name.Equals("UnitTestProjectObj"))
            {
                dir = new DirectoryInfo(dir.Parent.FullName);
            }

            _fullobjpathmultimaterial = Path.Combine(dir.Parent.FullName, testDatabb8Multimaterial);
        }

        [TestCase]
        public void CreateMaterialIdsArrayOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            Assert.True(_resObjMesh.FaceTriangles.Count == _resObjMesh.CreateMaterialIdsArray().Length);
        }

        [TestCase]
        public void IndexMaterialIdsArrayOk()
        {
            TGCObjLoader tgcObjLoader = new TGCObjLoader();
            tgcObjLoader.LoadObjFromFile(_fullobjpathmultimaterial);
            _resObjMesh = tgcObjLoader.ObjMeshContainer.ListObjMesh.First();
            int[] materialIds = _resObjMesh.CreateMaterialIdsArray();
            Assert.True(materialIds[15810] == 0);
            Assert.True(materialIds[17010] == 1);
        }
    }
}