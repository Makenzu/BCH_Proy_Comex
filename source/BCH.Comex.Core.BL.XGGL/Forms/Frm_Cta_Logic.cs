using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class Frm_Cta_Logic
    {
        #region METODOS PUBLICOS
        public static void Form_Load(DatosGlobales Globales)
        {
            UI_Frm_Cta Frm_Cta = Globales.Frm_Cta;
            T_MODCTA MODCTA = Globales.MODCTA;
            short a;
            Frm_Cta.Lista.Header = new List<string>() { "Número", "Nemónico", "Descripción" };

            Frm_Cta.tipoord_cta.Items.Add(new UI_ComboItem()
            {
                Data = UI_Frm_Cta.PorNum,
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
            for (short i = 0; i < MODCTA.CtaCtb.Length; i++)
            {
                Frm_Cta.Lista.Items.Add(LineaCta(Globales, i));
            }

        }

        public static void bot_acep_Click(DatosGlobales Globales)
        {
            UI_Frm_Cta Frm_Cta = Globales.Frm_Cta;
            string num = Frm_Cta.NUM;
            string nem = Frm_Cta.NEM;
            string desc = Frm_Cta.DESC;

            T_MODCTA MODCTA = Globales.MODCTA;
            int i = MODCTA.CtaCtb.ToList().FindIndex(x => x.Cta_Num.Equals(num) && x.Cta_Nem.Equals(nem) && x.Cta_Nom.Equals(desc));
            MODCTA.VCtaGl.NemCta = MODCTA.CtaCtb[i].Cta_Nem;
            
            Globales.Action= Globales.VieneDeAction;
            Globales.Controller = Globales.VieneDeController;
            Globales.VieneDeAction = String.Empty;
            Globales.VieneDeController = String.Empty;
            Globales.Frm_Cta = null;
        }

        public static void bot_canc_Click(DatosGlobales Globales)
        {
            UI_Frm_Cta Frm_Cta = Globales.Frm_Cta;
            Globales.Action = Globales.VieneDeAction;
            Globales.Controller = Globales.VieneDeController;
            Globales.VieneDeAction = String.Empty;
            Globales.VieneDeController = String.Empty;
            Globales.Frm_Cta = null;
        }
        #endregion

        #region METODOS PRIVADOS
        //Muestra la linea que representa cada Declaración en el Formulario.-
        private static UI_GridItem LineaCta(DatosGlobales Globales, short Indice)
        {
            T_MODCTA MODCTA = Globales.MODCTA;

            string s = "";
            string n = MODCTA.CtaCtb[Indice].Cta_Num;
            var item = new UI_GridItem();
            item.AddColumn("Cta_Num", VB6Helpers.Left(n, 3) + "." + VB6Helpers.Mid(n, 4, 2) + "." + VB6Helpers.Mid(n, 6, 2) + "-" + VB6Helpers.Right(n, 1));
            item.AddColumn("Cta_Nem", MODCTA.CtaCtb[Indice].Cta_Nem);
            item.AddColumn("Cta_Nom", MODCTA.CtaCtb[Indice].Cta_Nom);
            item.ID = Indice.ToString();
            return item;
        }
        #endregion
    }
}
