
namespace BCH.Comex.Common.XGPY.T_Modulos
{

    public class T_Especialista
    {
        public string codofi;
        public string codejc;
        public string rut;
        public string nombre;
        public string tipo;
    }




    public class T_PRTYENT
    {
        public static int offsec = 0;
      
        public T_Especialista[] VEjc;
        public static int Cliente_SGT = 0;
        public static int Hab_SGTCliEje = 0;

        public const string EJCOPIMP = "3";
        public const string EJCOPEXP = "4";
        public const string EJCNEGOC = "5";

        public T_PRTYENT()
        {
            VEjc = new T_Especialista[0];

        }
    }
}
