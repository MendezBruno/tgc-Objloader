using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TGC.Core.SceneLoader;

namespace TGC.Group.Model.MeshFactory
{
    /// <summary>
    ///     Factory default que crea una instancia de la clase TgcMesh
    /// </summary>
    public class DefaultMeshFactory
    {
        public TgcMesh CreateNewMesh(Mesh d3dMesh, string meshName, TgcMesh.MeshRenderType renderType)
        {
            return new TgcMesh(d3dMesh, meshName, renderType);
        }

        public TgcMesh CreateNewMeshInstance(string meshName, TgcMesh originalMesh, Vector3 translation,
            Vector3 rotation, Vector3 scale)
        {
            return new TgcMesh(meshName, originalMesh, translation, rotation, scale);
        }
    }
}
