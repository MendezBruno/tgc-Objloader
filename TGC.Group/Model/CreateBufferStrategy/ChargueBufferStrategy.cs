using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using TGC.Core.SceneLoader;

namespace TGC.Group.Model.CreateBufferStrategy
{
    public abstract class ChargueBufferStrategy
    {
        public abstract VertexElement[] VertexElementInstance { get; set; }

        public TgcMesh.MeshRenderType RenderType;
        /// <summary>
        ///    Carga el buffer del mesh de DirectX
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        /// <returns>MeshBuilder</returns>
        public abstract void ChargeBuffer(ObjMesh objMesh, Mesh dxMesh);

        /// <summary>
        ///   Cargar indexBuffer del mesh de DirectX en forma plana 
        /// </summary>
        /// <param name="objMesh">ObjMesh</param>
        public void ChargeIndexBuffer(ObjMesh objMesh, Mesh dxMesh)
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


    }
}
