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
}
