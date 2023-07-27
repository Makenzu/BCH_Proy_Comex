using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BCH.Comex.Core.BL.XGSV.Forms
{
    public class FrmgChq
    {        

        public static void Form_Load(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            globales.FrmgChq.FechaEmision = DateTime.Now;
            globales.VMndIng = MODGCHQ.SyGetn_MndIng(globales, uow);
        }
       
        public static List<T_Chq> ObtenerCheques(int opPlazaPago, string fechaEmision, bool todos, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            List<T_Chq> result = new List<T_Chq>();
            var Usr = todos ? string.Empty : globales.UsrEsp.id_especia;
            var obj = MODGCHQ.SyGetn_Chq(fechaEmision, globales.UsrEsp.cent_costo, Usr, globales, uow);

            foreach (var item in obj)
            {
                if (opPlazaPago == 0)
                {
                    if (item.swfpag != "BCHIUS33XXX")
                    {
                        switch (item.estado)
                        {
                            case MODGCHQ.DOCEMI:
                                item.estimp = "EMI";
                                break;
                            case MODGCHQ.DOCIMP:
                                item.estimp = "IMP";
                                break;
                        }

                        item.indice = result.Count;
                        result.Add(item);
                    }
                }
                else if (item.swfpag == "BCHIUS33XXX")
                    {
                        item.indice = result.Count;
                        result.Add(item);
                    }
                }

            return result;
        }

        public static List<T_Vvi> ObtenerValeVista(int opPlazaPago, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            List<T_Vvi> result = new List<T_Vvi>();
            var obj = MODGCHQ.SyGetn_Vvi(globales.FrmgChq.FechaEmision.ToShortDateString(), globales, uow);

            foreach (var item in obj)
            {
                switch (item.estado)
                {
                    case MODGCHQ.DOCEMI:
                        item.estimp = "EMI";
                        break;
                    case MODGCHQ.DOCIMP:
                        item.estimp = "IMP";
                        break;
                }
            }
            return result;
        }

        public static bool SetChequeImprimir(string codcct, string codpro, string codesp, string codemp, string codope, string nrocor, string nrofol, string estado, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            return MODGCHQ.SyUpd_Chq(codcct, codpro, codesp, codemp, codope, nrocor, nrofol, estado, globales, uow);
        }

        public static void Co_Imprimir_Click(T_Chq V_Chq, DatosGlobales globales, UnitOfWorkCext01 uow)
        {

            SetMontoIngles(V_Chq, globales); 
            if (V_Chq.codpro == MODGUSR.IdPro_CobExp)
            {
                V_Chq.NomPro = "COBRANZAS";
            }
            V_Chq.MtoChq_t = SetAsterisco(V_Chq.MonSwf + Utils.Format.FormatCurrency(V_Chq.MtoChq, "#,###,###,##0.00"), 23);
            V_Chq.FechaIng = DateTime.Parse(V_Chq.FecEmi).ToString("MMMM dd, yyyy", new CultureInfo("en-us"));
            V_Chq.FecEmi = DateTime.Parse(V_Chq.FecEmi).ToString("dd-MM-yyyy");

            V_Chq.estado = MODGCHQ.DOCIMP;
            if (!MODGCHQ.SyUpd_Chq(V_Chq.codcct, V_Chq.codpro, V_Chq.codesp, V_Chq.CodEmp, V_Chq.codope, 
                V_Chq.NroCor.ToString(), V_Chq.NroFol.ToString(), V_Chq.estado.ToString(), globales, uow))
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Se ha producido un error al tratar de actualizar el estado y número de folio del cheque.",
                    Type = TipoMensaje.Critical,
                    Title = MODGCHQ.MsgDoc
                });
            }
        }


        /// <summary>
        /// 1.  Rescata el punto de miles y el punto decimal.
        /// 2.  Rescata en palabras el monto en inglés.
        /// </summary>
        /// <param name="cheque"></param>
        /// <returns></returns>
        public static void SetMontoIngles(T_Chq V_Chq, DatosGlobales globales)
        {
            string montoCheque = string.Empty;
            montoCheque = DecimalToWords(V_Chq.MtoChq, new CultureInfo("en-US"));
            montoCheque += " " + MODGCHQ.Fn_NomMonIng(V_Chq.MonSwf, globales).Trim();
            V_Chq.DescMontoIngles = SetAsterisco(montoCheque.ToUpper(), 60);
        }

        public static string DecimalToWords(double number, CultureInfo culture)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + DecimalToWords(Math.Abs(number), culture);

            string words = "";

            decimal decNumber = (decimal)number;

            int intPortion = (int)decNumber;
            int decPortion = (int)((decNumber - intPortion) * 100);

            words = intPortion.ToWords(culture);
            if (decPortion > 0)
            {
                words += " and " + decPortion.ToString("D2") + "/100";
            }
            else
            {
                words += " NO" + "/100";
            }
            return words;
        }

        public static string SetAsterisco(string elemento, int largo)
        {
            return elemento.PadRight(largo, '*');
        }


    }
}
