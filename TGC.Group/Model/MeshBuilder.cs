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
        internal TgcMesh tgcMesh;
        internal Mesh dxMesh;
        internal Material[] MeshMaterials { get; set; }
        internal TgcTexture[] MeshTextures { get; set; }
        internal bool autoTransform;
        internal bool enable;
        internal bool hasBoundingBox;
        internal VertexElement[] VertexElementInstance { get; set; }
        internal List<TgcObjMaterialAux> MaterialsArray { get; set; }
        public IMeshFactory MeshFactory { get; set; }
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
            ChargueMaterials(MaterialsArray);
            return this;
        }

        /// <summary>
        ///     Agrega El/Los materiales y luego los hace el set de los atributos 
        ///      meshMaterials y meshTextures
        /// </summary>
        /// <param name="objMaterialLoader">Mesh de Direct3D</param>
        /// <returns>MeshBuilder</returns>
        private void ChargueMaterials(List<TgcObjMaterialAux> tgcObjMaterialAuxes)
        {
            var matAux = tgcObjMaterialAuxes.First();

            if (tgcObjMaterialAuxes.Count <= 1)
            {
                MeshMaterials = new[] { matAux.materialId };
                MeshTextures = new[]
                    {TgcTexture.createTexture(D3DDevice.Instance.Device, matAux.textureFileName, matAux.texturePath)};
            }
            
            //Configurar Material y Textura para varios SubSet
            else
            {
                //Cargar attributeBuffer con los id de las texturas de cada triángulo
                var attributeBuffer = dxMesh.LockAttributeBufferArray(LockFlags.None);
                Array.Copy(GetMaterialsIds(tgcObjMaterialAuxes).ToArray(), attributeBuffer, attributeBuffer.Length);  //aca tengo que ver que son todos los materials ID
                dxMesh.UnlockAttributeBuffer(attributeBuffer);

                //Cargar array de Materials y Texturas
                MeshMaterials = new Material[tgcObjMaterialAuxes.Count - 1];
                MeshTextures = new TgcTexture[tgcObjMaterialAuxes.Count - 1];
                tgcObjMaterialAuxes.ForEach((objMaterial) =>
                    {
                        MeshMaterials[tgcObjMaterialAuxes.IndexOf(objMaterial)] = objMaterial.materialId;
                        MeshTextures[tgcObjMaterialAuxes.IndexOf(objMaterial)] = TgcTexture.createTexture(D3DDevice.Instance.Device,
                            objMaterial.textureFileName,
                            objMaterial.texturePath);
                            
                    });
            }
        }

        private ArrayList GetMaterialsIds(List<TgcObjMaterialAux> tgcObjMaterialAuxes)
        {
            var matrialsIds = new ArrayList();
            tgcObjMaterialAuxes.ForEach((objMaterial) =>
            {
               matrialsIds.Add(objMaterial.materialId);
            });
            return matrialsIds;
        }

        public MeshBuilder AddDxMesh(int cantFace)
        {
            this.dxMesh = new Mesh(cantFace, cantFace * 3, MeshFlags.Managed, VertexElementInstance, D3DDevice.Instance.Device);
            return this;
        }

        public MeshBuilder SetAutotransform(bool flag)
        {
            this.autoTransform = flag;
            return this;
        }

        public MeshBuilder SetEnable(bool flag)
        {
            this.enable = flag;
            return this;
        }

        public MeshBuilder SetHasBoundingBox(bool flag)
        {
            this.hasBoundingBox = flag;
            return this;
        }

        



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
            //Cargar indexBuffer en forma plana
            using (var ib = dxMesh.IndexBuffer)
            {
                var indices = new short[objMesh.FaceTrianglesList.Count * 3];
                for (var i = 0; i < indices.Length; i++)
                {
                    indices[i] = (short)i;
                }
                ib.SetData(indices, 0, LockFlags.None);
            }

            return this;
        }


        public MeshBuilder InstaceDxMeshColorSolo(int cantFace, int cantVertex)
        {
            this.dxMesh = new Mesh(cantFace, cantFace * 3,
                 MeshFlags.Managed, VertexColorVertexElements, D3DDevice.Instance.Device);
            return this;
        }

        public MeshBuilder chargeBufferColorSolo(ObjMesh objMesh)
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
            //Cargar indexBuffer en forma plana
            using (var ib = dxMesh.IndexBuffer)
            {
                var indices = new short[objMesh.FaceTrianglesList.Count * 3];
                for (var i = 0; i < indices.Length; i++)
                {
                    indices[i] = (short)i;
                }
                ib.SetData(indices, 0, LockFlags.None);
            }

            return this;
        }
        
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

            //TODO ver que hacer con Ni, con d, con Ns.

            //guardar datos de textura
            matAux.texturePath = objMaterialMesh.getTextura() ?? Path.GetFullPath(currentDirectory + SEPARADOR + objMaterialMesh.getTexturaFileName());
            matAux.textureFileName = objMaterialMesh.getTexturaFileName();
            
            return matAux;
        }
    }
}
