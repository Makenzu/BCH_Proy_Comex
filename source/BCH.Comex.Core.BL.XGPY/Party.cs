using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using System;

namespace BCH.Comex.Core.BL.XGPY
{
    public enum NextStatusOld
    {
        DialogFlagCuentas,
        MainIndex,
        DialogRazSocDirEliminadas,
        DialogRazSocEliminada,
        DialogDirEliminada,
        DialogBancoCtaLin,
        DialogBancoCta,
        DialogBancoLin,
        DialogRazSocDir,
        DialogRazSoc,
        DialogDir,
        End,
        ErrorGrave
    }

    public class Party
    {
        public String keySearchByIdParty { get; set; }
        public String IdParty { get; set;}
        public Int32 IdNombre { get; set; }
        public Int32 IdDir { get; set; }
        public Int32 CreaCosto { get; set; }
        public Int32 CreaUser { get; set; }

       

        #region MODULOS DE UI
        public UI_Mdi_Principal Mdi_Principal { set; get; }

        #endregion

        public Party()
        {
            Mdi_Principal = new UI_Mdi_Principal();

        }
    }
    
}
