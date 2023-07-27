using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using System;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes
{
    public class InitializationObject
    {

        public T_PARTY PARTY { set; get; }
        public T_MODGRNG MODGRNG { set; get; } //emula las propiedades del modulo MODGRNG
        public T_MODGSGT MODGSGT { set; get; } //emula las propiedades del modulo MODGSGT
        public T_MODWS MODWS { set; get; }
        public T_PRTGLOB PRTGLOB { set; get; }
        public T_UTILES UTILES { set; get; }
        public T_PRTYENT PRTYENT { set; get; }
        public T_PRTYENT2 PRTYENT2 { set; get; }

        public T_MODGUSR.EstrucUsuarios UsrEsp { get; set; }
    

        #region MODULOS DE UI

        public UI_PrtEnt00 PrtEnt00 { set; get; }
        public UI_PrtEnt01 PrtEnt01 { set; get; }
        public UI_PrtEnt04 PrtEnt04 { set; get; }
        public UI_PrtEnt06 PrtEnt06 { set; get; }
        public UI_PrtEnt07 PrtEnt07 { set; get; }
        public UI_PrtEnt08 PrtEnt08 { set; get; }
        public UI_PrtEnt09 PrtEnt09 { set; get; }
        public UI_PrtEnt10 PrtEnt10 { set; get; }
        public UI_PrtEnt11 PrtEnt11 { set; get; }
        public UI_PrtEnt13 PrtEnt13 { set; get; }
        //INICIO MODIFICACION CNC - ACCENTURE //
        public UI_PrtEnt14 PrtEnt14 { set; get; }

        //FIN MODIFICACION CNC - ACCENTURE //
        public UI_Mdi_Principal Mdi_Principal { set; get; }

        public string PaginaWebQueAbrir = String.Empty;


        #endregion

        public InitializationObject()
        {
            PrtEnt00 = new UI_PrtEnt00();
            PrtEnt01 = new UI_PrtEnt01();
            PrtEnt04 = new UI_PrtEnt04();
            PrtEnt06 = new UI_PrtEnt06();
            PrtEnt07 = new UI_PrtEnt07(false);
            PrtEnt08 = new UI_PrtEnt08();
            PrtEnt09 = new UI_PrtEnt09();
            PrtEnt10 = new UI_PrtEnt10();
            PrtEnt11 = new UI_PrtEnt11();
            PrtEnt13 = new UI_PrtEnt13();

            //INICIO MODIFICACION CNC - ACCENTURE 
            PrtEnt14 = new UI_PrtEnt14();
            //FIN MODIFICACION CNC - ACCENTURE  
            
            Mdi_Principal = new UI_Mdi_Principal();
            UsrEsp = new T_MODGUSR.EstrucUsuarios();
            MODWS = new T_MODWS();
        }

        public InitializationObject(bool inicializar)
        {
            PrtEnt00 = new UI_PrtEnt00();
            PrtEnt01 = new UI_PrtEnt01();
            PrtEnt04 = new UI_PrtEnt04();
            PrtEnt06 = new UI_PrtEnt06();
            PrtEnt07 = new UI_PrtEnt07(inicializar);
            PrtEnt08 = new UI_PrtEnt08();
            PrtEnt09 = new UI_PrtEnt09();
            PrtEnt10 = new UI_PrtEnt10();
            PrtEnt11 = new UI_PrtEnt11();
            PrtEnt13 = new UI_PrtEnt13();

            //INICIO MODIFICACION CNC - ACCENTURE 
            PrtEnt14 = new UI_PrtEnt14();
            //FIN MODIFICACION CNC - ACCENTURE
            
            Mdi_Principal = new UI_Mdi_Principal();
            UsrEsp = new T_MODGUSR.EstrucUsuarios();
            MODWS = new T_MODWS();
        }
    }
}
