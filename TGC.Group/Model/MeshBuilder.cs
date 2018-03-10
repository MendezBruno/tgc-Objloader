using System;
using System.Collections;
using System.Collections.Generic;
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

namespace TGC.Group.Model
{
    public class MeshBuilder
    {
        internal TgcMesh tgcMesh;
        internal Mesh dxMesh;
        internal bool autoTransform;
        internal bool enable;
        internal bool hasBoundingBox;
        internal VertexElement[] VertexElementInstance { get; set; }
        public IMeshFactory MeshFactory { get; set; }
        public Device Device { get; set; }

        public MeshBuilder()
        {
            MeshFactory = new DefaultMeshFactory();
            VertexElementInstance = VertexColorVertexElements;
        }

        public Mesh getInstaceDxMesh()
        {
            return dxMesh;
        }


        public MeshBuilder AddMaterials(List<ObjMaterialMesh> listObjMaterialMesh, string materialPath)
        {
            //create material
            // TODO
            var materialsArray = new List<TgcSceneLoaderMaterialAux>();
            listObjMaterialMesh.ForEach((objMaterialMesh =>
                    {
                        materialsArray.Add(createTextureAndMaterial(objMaterialMesh, materialPath));
                    }
                    ));
            //set nueva mesh strategy
            VertexElementInstance = DiffuseMapVertexElements; // TODO ver que pasa caundo viene ligthmap
            return this;
        }

        public MeshBuilder InstaceDxMesh(int cantFace)
        {
            this.dxMesh = new Mesh(cantFace, cantFace * 3,
                MeshFlags.Managed, VertexElementInstance, D3DDevice.Instance.Device);
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

        



        public MeshBuilder chargeBuffer(ObjMesh objMesh)
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
                    v.Color = 255;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V2)-1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn2)-1];
                    v.Tu0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt2)-1].X;
                    v.Tv0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt2)-1].Y;
                    v.Color = 255;  //TODO que corresponde poner aca con respecto obj Mesh
                    data.Write(v);
                    v.Position = objMesh.VertexListV[Convert.ToInt32(face.V3)-1];
                    v.Normal = objMesh.VertexListVn[Convert.ToInt32(face.Vn3)-1];
                    v.Tu0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt3)-1].X;
                    v.Tv0 = objMesh.VertexListVt[Convert.ToInt32(face.Vt3)-1].Y;
                    v.Color = 255;  //TODO que corresponde poner aca con respecto obj Mesh
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

        public MeshBuilder chargeMaterial(List<ObjMaterialMesh> listObjMaterialMesh)
        {


            return this;
        }


        public TgcMesh build(ObjMesh objMesh)
        {
            TgcMesh unMesh =  MeshFactory.createNewMesh(dxMesh, objMesh.Name, TgcMesh.MeshRenderType.VERTEX_COLOR);
            SetBoundingBox(unMesh);
            unMesh.AutoTransformEnable = autoTransform;
            unMesh.Enabled = enable;
            return unMesh;
        }

        private void SetBoundingBox(TgcMesh unMesh)
        {
            //Crear BoundingBox, aprovechar lo que viene del XML o crear uno por nuestra cuenta
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
        private class TgcSceneLoaderMaterialAux
        {
            public Material materialId;
            public TgcSceneLoaderMaterialAux[] subMaterials;
            public string textureFileName;
            public string texturePath;
        }

        /// <summary>
        ///     Crea Material y Textura
        /// </summary>

        private TgcSceneLoaderMaterialAux createTextureAndMaterial(ObjMaterialMesh objMaterialMesh)
        {
            var matAux = new TgcSceneLoaderMaterialAux();

            //Crear material
            var material = new Material();
            matAux.materialId = material;
            material.AmbientColor = objMaterialMesh.Ka;
            material.DiffuseColor = objMaterialMesh.Kd;
            material.SpecularColor = objMaterialMesh.Ks;

            //TODO ver que hacer con la opacity

            //guardar datos de textura
            if (objMaterialMesh.getTextura() != null)
            {
                matAux.texturePath = getTextura();
                matAux.textureFileName = getTexturaFileName();
            }
            else
            {
                matAux.texturePath = null;
                matAux.textureFileName = null;
            }
            return matAux;
        }

        


        #region MeshFactory

        /// <summary>
        ///     Factory para permitir crear una instancia especifica de la clase TgcMesh
        /// </summary>
        public interface IMeshFactory
        {
            /// <summary>
            ///     Crear una nueva instancia de la clase TgcMesh o derivados
            /// </summary>
            /// <param name="d3DMesh">Mesh de Direct3D</param>
            /// <param name="meshName">Nombre de la malla</param>
            /// <param name="renderType">Tipo de renderizado de la malla</param>
            /// <param name="meshData">Datos de la malla</param>
            /// <returns>Instancia de TgcMesh creada</returns>
            TgcMesh createNewMesh(Mesh d3DMesh, string meshName, TgcMesh.MeshRenderType renderType);

            /// <summary>
            ///     Crear una nueva malla que es una instancia de otra malla original.
            ///     Crear una instancia de la clase TgcMesh o derivados
            /// </summary>
            /// <param name="name">Nombre de la malla</param>
            /// <param name="parentInstance">Malla original desde la cual basarse</param>
            /// <param name="translation">Traslación respecto de la malla original</param>
            /// <param name="rotation">Rotación respecto de la malla original</param>
            /// <param name="scale">Escala respecto de la malla original</param>
            /// <returns>Instancia de TgcMesh creada</returns>
            TgcMesh createNewMeshInstance(string meshName, TgcMesh originalMesh, Vector3 translation, Vector3 rotation,
                Vector3 scale);
        }

        /// <summary>
        ///     Factory default que crea una instancia de la clase TgcMesh
        /// </summary>
        public class DefaultMeshFactory : IMeshFactory
        {
            public TgcMesh createNewMesh(Mesh d3dMesh, string meshName, TgcMesh.MeshRenderType renderType)
            {
                return new TgcMesh(d3dMesh, meshName, renderType);
            }

            public TgcMesh createNewMeshInstance(string meshName, TgcMesh originalMesh, Vector3 translation,
                Vector3 rotation, Vector3 scale)
            {
                return new TgcMesh(meshName, originalMesh, translation, rotation, scale);
            }
        }

        #endregion MeshFactory


      
    }
}
