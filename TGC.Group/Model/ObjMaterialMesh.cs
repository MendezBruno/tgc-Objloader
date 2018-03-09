using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace TGC.Group.Model
{
    public class ObjMaterialMesh
    {
        //Material Name 
        public string Name { get; set; }
        //Material color & ilumination
        public ColorValue Ka { get; set; }
        public ColorValue Kd { get; set; }
        public ColorValue Ks { get; set; }
        public ColorValue Tf { get; set; }
        /*
         ILUM REFENCE NUMBERS
            0 Color on and Ambient off
            1 Color on and Ambient on
            2 Highlight on
            3 Reflection on and Ray trace on
            4 Transparency: Glass on
              Reflection: Ray trace on
            5 Reflection: Fresnel on and Ray trace on
            6 Transparency: Refraction on
              Reflection: Fresnel off and Ray trace on
            7 Transparency: Refraction on
              Reflection: Fresnel on and Ray trace on
            8 Reflection on and Ray trace off
            9 Transparency: Glass on
              Reflection: Ray trace off
           10 Casts shadows onto invisible surfaces
        */
        public int illum { get; set; }
        public float d { get; set; }
        public float Ns { get; set; }
        public int Sharpness { get; set; }
        public float Ni { get; set; }


        //Texture mas statament 
        

        //Reflection map statament


        public ObjMaterialMesh(string name) {
            this.Name = name;
        }

        
    }
}