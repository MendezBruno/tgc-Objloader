using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.IO;
using TGC.Core.BoundingVolumes;
using TGC.Core.Direct3D;
using TGC.Core.Mathematica;
using TGC.Core.SceneLoader;
using TGC.Core.Textures;
using TGC.Group.Model.CreateBufferStrategy;
using static TGC.Core.SceneLoader.TgcSceneLoader;

namespace TGC.Group.Model
{
    public class MeshBuilder
    {
        //Constante
        internal const string SEPARADOR = "\\";

        //Variables
        private Mesh dxMesh { get; set; }

        public IMeshFactory MeshFactory { get; set; }
        public ChargueBufferStrategy ChargueBufferStrategy { get; set; }
        public Material[] MeshMaterials { get; set; }
        private TgcTexture[] MeshTextures { get; set; }
        private bool autoTransform { get; set; }
        private bool enable { get; set; }
        private bool hasBoundingBox { get; set; }
        private List<TgcObjMaterialAux> MaterialsArray { get; set; }

        public Device Device { get; set; }

        public MeshBuilder()
        {
            MeshFactory = new DefaultMeshFactory();
            ChargueBufferStrategy = new ChargueBufferColorSoloStrategy();
        }

        public Mesh GetInstaceDxMesh()
        {
            return dxMesh;
        }

        /// <summary>
        ///     Agrega El/Los materiales, cambia el tipo de VertexElement
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
            ChargueBufferStrategy = new ChargueBufferDiffuseMapStrategy();  // TODO ver que pasa caundo viene ligthmap
            return this;
        }

        /// <summary>
        ///     Agrega El/Los materiales y luego los hace el set de los atributos
        ///      meshMaterials y meshTextures
        /// </summary>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder ChargueMaterials()
        {
            //Cargar array de Materials y Texturas  TODO separar la creacion del material de la textura
            MeshMaterials = new Material[MaterialsArray.Count];
            MeshTextures = new TgcTexture[GetTextureCount()];     //GetTextureCount()
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
            Array.Copy(materialsIds, attributeBuffer, attributeBuffer.Length);
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
            this.dxMesh = new Mesh(cantFace, cantFace * 3, MeshFlags.Managed, ChargueBufferStrategy.VertexElementInstance, D3DDevice.Instance.Device);
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
        ///    Carga el buffer del mesh de DirectX usando la estrategia correcta para s estructura
        /// </summary>
        /// <param name="objMeshContainer"></param>
        /// <param name="index"></param>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        public MeshBuilder ChargeBuffer(ObjMeshContainer objMeshContainer, int index)
        {
            ChargueBufferStrategy.ChargeBuffer(objMeshContainer, this.dxMesh, index);
            return this;
        }

        /// <summary>
        ///   Construye el mesh con los atributos que se le fueron agregando
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        public TgcMesh Build(ObjMesh objMesh)
        {
            TgcMesh unMesh = MeshFactory.createNewMesh(dxMesh, objMesh.Name, ChargueBufferStrategy.RenderType);
            SetBoundingBox(unMesh);
            unMesh.AutoTransform = autoTransform;
            unMesh.Enabled = enable;
            unMesh.Materials = MeshMaterials;
            unMesh.DiffuseMaps = MeshTextures;
            return unMesh;
        }

        /// <summary>
        ///   Cargar indexBuffer del mesh de DirectX en forma plana
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        private void SetBoundingBox(TgcMesh unMesh)
        {
            //Crear BoundingBox, aprovechar lo que viene del OBJ o crear uno por nuestra cuenta
            if (hasBoundingBox)
            {
                unMesh.BoundingBox = new TgcBoundingAxisAlignBox(
                    new TGCVector3(1, 1, 1),   //Esto es re saraza TODO hay que ver si la info del obj puede calcular los puntos minimos y maximos. o si se pueden agregar al archivo.
                    new TGCVector3(1, 1, 1),
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
        ///     Estructura auxiliar para cargar SubMaterials y Texturas
        /// </summary>
        internal class TgcObjMaterialAux
        {
            public Material materialId;
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