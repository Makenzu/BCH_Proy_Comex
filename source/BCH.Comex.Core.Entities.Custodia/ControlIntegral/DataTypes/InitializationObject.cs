using BCH.Comex.Core.Entities.Custodia.ControlIntegral.T_Modulos;
using BCH.Comex.Core.Entities.Custodia.ControlIntegral.UI_Modulos;
using System;

namespace BCH.Comex.Core.Entities.Custodia.ControlIntegral.DataTypes
{
    public class InitializationObject
    {

        #region MODULOS
        public T_ModFunc ModFunc { set; get; } //emula las propiedades del modulo MODFUNC
        public T_Modulo Modulo { set; get; } //emula las propiedades del modulo Modulo

        #endregion

        #region MODULOS DE UI
        public UI_frmEReparo frmEReparo { set; get; }
        public UI_frmChkList frmChkList { set; get; }

        #endregion

        public string PaginaWebQueAbrir = String.Empty;

        public InitializationObject()
        {
            this.ModFunc = new T_ModFunc();
            this.Modulo = new T_Modulo();
            this.frmEReparo = new UI_frmEReparo();
            this.frmChkList = new UI_frmChkList();
        }

    }

}
