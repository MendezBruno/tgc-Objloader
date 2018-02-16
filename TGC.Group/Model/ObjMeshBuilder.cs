using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;

namespace TGC.Group.Model
{
    public class ObjMeshBuilder
    {
        private ObjMesh objMesh;
        internal Mesh dxMesh;

        private ObjMeshBuilder() { }

        ObjMeshBuilder(MeshBuilder builder)
        {
           this.dxMesh = builder.getInstaceDxMesh();
        }


    }
}
