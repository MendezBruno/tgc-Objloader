using System.IO;
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
        //TODO prodia ser una estructura de dato que represente la sentencia de textura
        public string map_Kd { get; set; }
        public string disp { get; set; }
        public string map_bump { get; set; }
        public string map_Ka { get; set; }
        public string map_Ks { get; set; }
        public string map_d { get; set; }

        //Reflection map statament


        public ObjMaterialMesh(string name) {
            this.Name = name;
        }


        public string getTextura()
        {
            return File.Exists(map_d)? map_d : null; // prodia ser cualquiera, TODO indentificar bien cual usar
        }


        public string getTexturaFileName()
        {
            return Path.GetFileName(map_d);
        }
    }
}