using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Cta_Logic
    {
        #region METODOS PUBLICOS
        public static void Form_Load(InitializationObject initObject)
        {
            UI_Frm_Cta Frm_Cta = initObject.Frm_Cta;
            T_MODCONT MODCONT = initObject.MODCONT;
            short a;
            Frm_Cta.Lista.Header = new List<string>() { "Número","Nemónico","Descripción" };

            Frm_Cta.tipoord_cta.Items.Add(new UI_ComboItem()
            {
                Data=UI_Frm_Cta.PorNum,
                Value = "Número Cuenta Contable"
            });
            Frm_Cta.tipoord_cta.Items.Add(new UI_ComboItem()
            {
                Data = UI_Frm_Cta.PorNem,
                Value = "Nemónico Cuenta Contable"
            });
            Frm_Cta.tipoord_cta.Items.Add(new UI_ComboItem()
            {
                Data = UI_Frm_Cta.PorNom,
                Value = "Nombre Cuenta Contable"
            });
            Frm_Cta.tipoord_cta.ListIndex = 0;
            
            //esto es un cambio con respecto al original. se cargan todas las cuentas aca y se ordenan solo del lado del cliente
            for (short i = 0; i < MODCONT.CtaCtb.Length; i++)
            {
                Frm_Cta.Lista.Items.Add(LineaCta(initObject, i));
            }
            
        }

        public static void bot_acep_Click(InitializationObject initObject,string num,string nem,string desc)
        {
            UI_Frm_Cta Frm_Cta = initObject.Frm_Cta;
            T_MODCONT MODCONT = initObject.MODCONT;
            int i = MODCONT.CtaCtb.ToList().FindIndex(x => x.Cta_Num.Equals(num) && x.Cta_Nem.Equals(nem) && x.Cta_Nom.Equals(desc));
            MODCONT.VCtaGl.NemCta = MODCONT.CtaCtb[i].Cta_Nem;
            initObject.FormularioQueAbrir = initObject.VieneDe;
            initObject.VieneDe = String.Empty;
            initObject.Frm_Cta = null;
        }

        public static void bot_canc_Click(InitializationObject initObject)
        {
            UI_Frm_Cta Frm_Cta = initObject.Frm_Cta;
            initObject.FormularioQueAbrir = Frm_Cta.VieneDe??initObject.VieneDe;
            initObject.Frm_Cta = null;
        }
        #endregion

        #region METODOS PRIVADOS
        //Muestra la linea que representa cada Declaración en el Formulario.-
        private static UI_GridItem LineaCta(InitializationObject initObject, short Indice)
        {
            T_MODCONT MODCONT = initObject.MODCONT;

            string s = "";
            string n = MODCONT.CtaCtb[Indice].Cta_Num;
            var item = new UI_GridItem();
            item.AddColumn("Cta_Num", VB6Helpers.Left(n, 3) + "." + VB6Helpers.Mid(n, 4, 2) + "." + VB6Helpers.Mid(n, 6, 2) + "-" + VB6Helpers.Right(n, 1));
            item.AddColumn("Cta_Nem", MODCONT.CtaCtb[Indice].Cta_Nem);
            item.AddColumn("Cta_Nom", MODCONT.CtaCtb[Indice].Cta_Nom);
            item.ID = Indice.ToString();
            return item;
        }
        #endregion
    }
}
