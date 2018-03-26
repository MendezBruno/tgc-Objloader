using Microsoft.DirectX.Direct3D;

namespace TGC.Group.Model
{
    public class ObjMeshBuilder
    {
        private ObjMesh objMesh;
        internal Mesh dxMesh;

        private ObjMeshBuilder()
        {
        }

        private ObjMeshBuilder(MeshBuilder builder)
        {
            this.dxMesh = builder.GetInstaceDxMesh();
        }
    }
}