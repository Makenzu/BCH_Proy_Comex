using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.Domain;
using BCH.Comex.Common.XGPY.UI_Modulos;
using System;

namespace BCH.Comex.Common.XGPY.Datatypes
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
        public UI_Mdi_Principal Mdi_Principal { set; get; }

        public string PaginaWebQueAbrir = String.Empty;


        #endregion

        public InitializationObject()
        {
            PrtEnt00 = new UI_PrtEnt00();
            PrtEnt01 = new UI_PrtEnt01();
            PrtEnt04 = new UI_PrtEnt04();
            PrtEnt06 = new UI_PrtEnt06();
            PrtEnt07 = new UI_PrtEnt07();
            PrtEnt08 = new UI_PrtEnt08();
            PrtEnt09 = new UI_PrtEnt09();
            PrtEnt10 = new UI_PrtEnt10();
            PrtEnt11 = new UI_PrtEnt11();
            PrtEnt13 = new UI_PrtEnt13();         
            Mdi_Principal = new UI_Mdi_Principal();
        }
    }
}
