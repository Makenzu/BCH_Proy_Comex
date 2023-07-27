

namespace BCH.Comex.Common.XGPY.T_Modulos
{


    public class CliEsp
    {
        public string nrut;
        public string tipo;
        public string ofieje;
        public string codeje;
        public string feccre;
        public string rutope;
        public string drutope;
        public string filler;
        public int modifica;
        public int nuevo;
        public int borrar;
    }

 

    public class T_PRTYENT2
    {
        public CliEsp[] VSGTCliEsp = null;
        public CliEsp[] RSGTCliEsp = null;
        public const string SGT_tipopimp = "03";
        public const string SGT_tipopexp = "04";
        //constante modificada para tener acceso
        public const string SGT_tipnegoc = "05";
        public static string KeyPrt = "";
        public static Type_ParamSgt ParamSgt = new Type_ParamSgt();
        //public static extern int AISGetRutUsr(string RutUsr);
        public static string RutwAis = "";
        public T_PRTYENT2()
        {
            VSGTCliEsp = new CliEsp[0];
            RSGTCliEsp = new CliEsp[0];

        }

    }

}
