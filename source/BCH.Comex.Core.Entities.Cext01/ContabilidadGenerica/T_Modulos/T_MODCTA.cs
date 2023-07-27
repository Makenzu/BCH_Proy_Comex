
namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{

     public class T_CtaCtb
      {
         public string Cta_Nem;
         public int Cta_Mon;
         public string Cta_Num;
         public string Cta_Nom;
         public int Cta_Vig;
      }
          
      // Variables Generales de Cuentas.-
     public class T_CtaGl
      {
         public int moncta;
         public string NemCta;
      }
     
      // Variables Generales de Notas de Crédito-
     public class T_CtaNcre
      {
         public string Cta_Nem;
         public int Cta_Mon;
         public string Cta_Num;
         public string Cta_Nom;
      }

     public class T_NotaCreGl
      {
         public string NumFac;
         public string FecOpe;
         public string NumRep;
         public string Moneda;
         public string netofac;
         public string ivafac;
         public string tipofac;
         public string monto;
      }
      

    public class T_MODCTA
    {
        public T_CtaCtb[] CtaCtb; 
        public T_CtaGl VCtaGl;
        public T_CtaNcre[] CtaNcre;
        public T_NotaCreGl VNotaCreGl;


        public T_MODCTA()
        {
            CtaCtb = new T_CtaCtb[0];
            VCtaGl = new T_CtaGl();
            CtaNcre = new T_CtaNcre[0];
            VNotaCreGl = new T_NotaCreGl();
        }
    }


}
