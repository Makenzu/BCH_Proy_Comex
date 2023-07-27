
namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos
{
   public class T_MODGRNG
    {
       public static Type_RngDoc[] RngDocs;
       public static Type_Rng VRng = new Type_Rng();
       public static Type_Rng VRngNul = new Type_Rng();

       public T_MODGRNG()
        {
            RngDocs = new Type_RngDoc[0];
           // Ope_OP = new OrdenPago[0];
        }
    }

        
    public class Type_RngDoc
    {
        public string DocCod;   //Código del Documento.
        public string DocGls;   //Glosa del Documento.
        public int DocGen;   //Es General? (no depende del usuario).
        public string DocFmt;   //Formato numérico (99;999;99999, etc.)
    }
      
    public class Type_Rng
    {
        public string RngRut;   //Rut Especialista.
        public string RngCct;   //CCosto Especialista.
        public string RngEsp;   //Especialista.
        public string RngDoc;   //Tipo de Documento.
        public double RngInf;   //Limite Inferior.
        public double RngSup;   //Limite Superior.
        public double RngAct;   //Ultimo Usado.
        public string RngInf_t;   //Limite Inferior para Srm.
        public string RngSup_t;   //Limite Superior para Srm.
        public string RngAct_t;   //Ultimo Usado para Srm.
    }

    // 



}
