using Microsoft.DirectX;

namespace TGC.Group.Model
{
    public class ObjMaterialMesh
    {
        //Material Name 
        public string Name { get; set; }
        //Material color & ilumination
        public Vector3 Ka { get; set; }
        public Vector3 kd { get; set; }
        public Vector3 Ks { get; set; }
        public Vector3 Tf { get; set; }
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
        public int Ilum { get; set; }
        public float d { get; set; }
        public float Ns { get; set; }
        public int sharpness { get; set; }
        public float Ni { get; set; }


        //Texture mas statament 
        

        //Reflection map statament


        public ObjMaterialMesh(string name) {
            this.Name = name;
        }

        
    }
}