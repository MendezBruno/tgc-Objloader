using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using TGC.Core.Direct3D;
using TGC.Core.SceneLoader;
using static TGC.Core.SceneLoader.TgcSceneLoader;
using Microsoft.DirectX;
using Microsoft.DirectX.PrivateImplementationDetails;
using TGC.Core.BoundingVolumes;
using TGC.Core.Textures;

namespace TGC.Group.Model
{
    public class MeshBuilder
    {
        //Constante
        internal const string SEPARADOR = "\\";
        //Variables
        private TgcMesh tgcMesh { get; set; }
        private Mesh dxMesh { get; set; }
        public IMeshFactory MeshFactory { get; set; }
        private int[] materialsIds { get; set; }
        public Material[] MeshMaterials { get; set; }
        private TgcTexture[] MeshTextures { get; set; }
        private bool autoTransform { get; set; }
        private bool enable { get; set; }
        private bool hasBoundingBox { get; set; }
        private VertexElement[] VertexElementInstance { get; set; }
        private List<TgcObjMaterialAux> MaterialsArray { get; set; }
       
        public Device Device { get; set; }

        public MeshBuilder()
        {
            MeshFactory = new DefaultMeshFactory();
            VertexElementInstance = VertexColorVertexElements; //por defecto solo color (?) 
        }

        public Mesh GetInstaceDxMesh()
        {
            return dxMesh;
        }

        /// <summary>
        ///     Agrega El/Los materiales, cambia el tipo de VertexElement y luego los hace el set de los atributos 
        ///     meshMaterials y meshTextures
        /// </summary>
        /// <param name="objMaterialLoader">Clase ObjMaterialLader</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder AddMaterials(ObjMaterialsLoader objMaterialLoader)
        {
            //create material
            // TODO
            MaterialsArray = new List<TgcObjMaterialAux>();
            objMaterialLoader.ListObjMaterialMesh.ForEach((objMaterialMesh =>
                    {
                        MaterialsArray.Add(createTextureAndMaterial(objMaterialMesh, objMaterialLoader.currentDirectory));
                    }
            ));

            //set nueva mesh strategy
            VertexElementInstance = DiffuseMapVertexElements; // TODO ver que pasa caundo viene ligthmap
            
            //contruyo la textura y los materiales que envié
           // ChargueMaterials(MaterialsArray);
            return this;
        }

        /// <summary>
        ///     Agrega El/Los materiales y luego los hace el set de los atributos 
        ///      meshMaterials y meshTextures
        /// </summary>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder ChargueMaterials()
        {
          /*  if (MaterialsArray.Count <= 1)
            {
                var matAux = MaterialsArray.First();
                MeshMaterials = new[] { matAux.materialId };
                MeshTextures = new[]
                    {TgcTexture.createTexture(D3DDevice.Instance.Device, matAux.textureFileName, matAux.texturePath)};
            }
            //Configurar Material y Textura para varios SubSet
            else
            {*/
                   //Cargar array de Materials y Texturas  TODO separar la creacion del material de la textura
                MeshMaterials = new Material[MaterialsArray.Count];
                MeshTextures = new TgcTexture[GetTextureCount()];
                var indexTexture = 0;
                MaterialsArray.ForEach((objMaterial) =>
                {
                        MeshMaterials[MaterialsArray.IndexOf(objMaterial)] = objMaterial.materialId;

                        if (objMaterial.textureFileName != null)
                        {
                        MeshTextures[indexTexture] = TgcTexture.createTexture(
                            D3DDevice.Instance.Device,
                            objMaterial.textureFileName,
                            objMaterial.texturePath);
                            indexTexture++;
                        }
                });

            return this;
        }

        /// <summary>
        ///     Obtiene la cantidad materiales que poseen textura
        /// </summary>
        /// <returns>int</returns>
        public int GetTextureCount()
        {
            var count = 0;

            MaterialsArray.ForEach((objMaterial) =>
            {
                if (objMaterial.textureFileName != null) count++;
            });

            return count;
        }

        /// <summary>
        ///     Cargar attributeBuffer con los id de las texturas de cada triángulo
        /// </summary>
        /// <param name="materialsIds">int[]</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder ChargeAttributeBuffer(int[] materialsIds)
        {
            var attributeBuffer = dxMesh.LockAttributeBufferArray(LockFlags.None);
            Array.Copy(materialsIds , attributeBuffer, attributeBuffer.Length); 
            dxMesh.UnlockAttributeBuffer(attributeBuffer);
            return this;
        }

        /// <summary>
        ///     Crea una instancia del mesh de DirectX
        /// </summary>
        /// <param name="cantFace">int</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder AddDxMesh(int cantFace)
        {
            this.dxMesh = new Mesh(cantFace, cantFace * 3, MeshFlags.Managed, VertexElementInstance, D3DDevice.Instance.Device);
            return this;
        }

        /// <summary>
        ///     Indica al builder si el mesh posee autotransformacion 
        /// </summary>
        /// <param name="flag">boolean</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder AddAutotransform(bool flag)
        {
            this.autoTransform = flag;
            return this;
        }

        /// <summary>
        ///     Indica al builder si el mesh esta disponible para modificaciones 
        /// </summary>
        /// <param name="flag">boolean</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder SetEnable(bool flag)
        {
            this.enable = flag;
            return this;
        }

        /// <summary>
        ///     Indica al builder si se debe crear un bounding box
        ///     en base a los parametros de objMesh, por defecto genera uno stardar 
        /// </summary>
        /// <param name="flag">boolean</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder SetHasBoundingBox(bool flag)
        {
            this.hasBoundingBox = flag;
            return this;
        }

        



        /// <summary>
        ///    Carga el buffer del mesh de DirectX
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder ChargeBuffer(ObjMesh objMesh)
        {
            //Cargar VertexBuffer
            using (var vb = this.dxMesh.VertexBuffer)
            {
                var data = vb.Lock(0, 0, LockFlags.None);
                var v = new VertexMesh();
                objMesh.FaceTrianglesList.ForEach(face =>
                {
                    
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V1) -1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn1) -1];
                    v.Tu0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt1)-1].X;
                    v.Tv0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt1)-1].Y;
                    v.Color = -1;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V2)-1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn2)-1];
                    v.Tu0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt2)-1].X;
                    v.Tv0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt2)-1].Y;
                    v.Color = -1;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V3)-1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn3)-1];
                    v.Tu0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt3)-1].X;
                    v.Tv0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt3)-1].Y;
                    v.Color = -1;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);

                });
                vb.Unlock();
            }

            ChargeIndexBuffer(objMesh);

            return this;
        }

        /// <summary>
        ///    crea el mesh con estructura de datos que posee solo color
        /// </summary>
        /// <param name="cantFace">int</param>
        /// /// <param name="cantVertex">int</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder InstaceDxMeshColorSolo(int cantFace, int cantVertex)
        {
            this.dxMesh = new Mesh(cantFace, cantFace * 3,
                 MeshFlags.Managed, VertexColorVertexElements, D3DDevice.Instance.Device);
            return this;
        }

        /// <summary>
        ///    carga el mesh con estructura de datos que posee solo color
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder ChargeBufferColorSolo(ObjMesh objMesh)
        {
            //Cargar VertexBuffer
            using (var vb = this.dxMesh.VertexBuffer)
            {
                var data = vb.Lock(0, 0, LockFlags.None);
                var v = new VertexColorVertex();
                objMesh.FaceTrianglesList.ForEach(face =>
                {

                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V1) - 1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn1) - 1];
                    v.Color = -16777047;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V2) - 1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn2) - 1];
                    v.Color = -16777047;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V3) - 1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn3) - 1];
                    v.Color = -16777047;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);

                });
                vb.Unlock();
            }

            ChargeIndexBuffer(objMesh);

            return this;
        }

        /// <summary>
        ///   Cargar indexBuffer del mesh de DirectX en forma plana 
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        private void ChargeIndexBuffer(ObjMesh objMesh)
        {
            using (var ib = dxMesh.IndexBuffer)
            {
                var indices = new short[objMesh.FaceTrianglesList.Count * 3];
                for (var i = 0; i < indices.Length; i++)
                {
                    indices[i] = (short)i;
                }
                ib.SetData(indices, 0, LockFlags.None);
            }

        }

        /// <summary>
        ///   Cargar indexBuffer del mesh de DirectX en forma plana 
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        public TgcMesh Build(ObjMesh objMesh)
        {
            TgcMesh unMesh =  MeshFactory.createNewMesh(dxMesh, objMesh.Name, TgcMesh.MeshRenderType.DIFFUSE_MAP);
            SetBoundingBox(unMesh);
            unMesh.AutoTransformEnable = autoTransform;
            unMesh.Enabled = enable;
            unMesh.Materials = MeshMaterials;
            unMesh.DiffuseMaps = MeshTextures;
            return unMesh;
        }

        private void SetBoundingBox(TgcMesh unMesh)
        {
            //Crear BoundingBox, aprovechar lo que viene del OBJ o crear uno por nuestra cuenta
            if (hasBoundingBox)
            {
                unMesh.BoundingBox = new TgcBoundingAxisAlignBox(
                    new Vector3(1,1,1),   //Esto es re saraza TODO hay que ver si la info del obj puede calcular los puntos minimos y maximos. o si se pueden agregar al archivo.
                    new Vector3(1, 1, 1),
                    unMesh.Position,
                    unMesh.Scale
                );
            }
            else
            {
                unMesh.createBoundingBox();
            }
        }


        /// <summary>
        ///     Estructura de Vertice para formato de malla DIFFUSE_MAP
        /// </summary>
        public struct VertexMesh
        {
            public Vector3 Position;
            public Vector3 Normal;
            public int Color;
            public float Tu0;
            public float Tv0;
        }


        /// <summary>
        ///     FVF para formato de malla  DIFFUSE_MAP
        /// </summary>
        public static readonly VertexElement[] DiffuseMapVertexElements =
        {
            new VertexElement(0, 0, DeclarationType.Float3,
                DeclarationMethod.Default,
                DeclarationUsage.Position, 0),
            new VertexElement(0, 12, DeclarationType.Float3,
                DeclarationMethod.Default,
                DeclarationUsage.Normal, 0),
            new VertexElement(0, 24, DeclarationType.Color,
                DeclarationMethod.Default,
                DeclarationUsage.Color, 0),
            new VertexElement(0, 28, DeclarationType.Float2,
                DeclarationMethod.Default,
                DeclarationUsage.TextureCoordinate, 0),
            VertexElement.VertexDeclarationEnd
        };



        /// <summary>
        ///     FVF para formato de malla VERTEX_COLOR
        /// </summary>
        public static readonly VertexElement[] VertexColorVertexElements =
        {
            new VertexElement(0, 0, DeclarationType.Float3,
                DeclarationMethod.Default,
                DeclarationUsage.Position, 0),
            new VertexElement(0, 12, DeclarationType.Float3,
                DeclarationMethod.Default,
                DeclarationUsage.Normal, 0),
            new VertexElement(0, 24, DeclarationType.Color,
                DeclarationMethod.Default,
                DeclarationUsage.Color, 0),
            VertexElement.VertexDeclarationEnd
        };

        /// <summary>
        ///     Estructura de Vertice para formato de malla VERTEX_COLOR
        /// </summary>
        public struct VertexColorVertex
        {
            public Vector3 Position;
            public Vector3 Normal;
            public int Color;
        }

        /// <summary>
        ///     Estructura auxiliar para cargar SubMaterials y Texturas
        /// </summary>
        internal class TgcObjMaterialAux
        {
            public Material materialId;
           // public TgcSceneLoaderMaterialAux[] subMaterials; por el momento no lo voy a usar
            public string textureFileName;
            public string texturePath;
        }

        /// <summary>
        ///     Crea Material y Textura
        /// </summary>

        private TgcObjMaterialAux createTextureAndMaterial(ObjMaterialMesh objMaterialMesh, string currentDirectory)
        {
            var matAux = new TgcObjMaterialAux();

            //Crear material
            var material = new Material();
            matAux.materialId = material;
            material.AmbientColor = objMaterialMesh.Ka;
            material.DiffuseColor = objMaterialMesh.Kd;
            material.SpecularColor = objMaterialMesh.Ks;

            //TODO ver que hacer con Ni, con d, con Ns, con Ke.

            //guardar datos de textura
            matAux.texturePath = objMaterialMesh.getTextura() ?? Path.GetFullPath(currentDirectory + SEPARADOR + objMaterialMesh.getTexturaFileName());
            matAux.textureFileName = objMaterialMesh.getTexturaFileName();
            
            return matAux;
        }
    }
}
