using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BCH.Comex.Core.BL.XGSV.Forms
{
    public class FrmAyM
    {
        public static void Boton_Click(int anio, int mes, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            string periodo = "";

            if (MODPRD.Sygetn_Prd(globales.UsrEsp.cent_costo, globales.UsrEsp.id_especia, mes, anio, globales, uow))
            {
                periodo = ("00" + mes.ToString().Trim()).PadRight(2) + "/" + anio;
                globales.FrmAyM.stringHtml = MODPRD.Prn_Prod(globales.UsrEsp.cent_costo, globales.UsrEsp.id_especia, periodo, globales);
            }

        }

        public static void Form_Load(DatosGlobales globales)
        {
            globales.FrmAyM.ltMeses = new Dictionary<string, string>();
            for (int i = 0; i < 12; i++)
            {
                globales.FrmAyM.ltMeses.Add(i.ToString(), CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i]);
            }
            globales.FrmAyM.anio = DateTime.Now.Year.ToString();

        }

    }
}
