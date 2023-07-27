using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGBIC
    {
        // ****************************************************************************
        //    1.  Lee los Bancos asociados a un swift y una secuencia.
        // ****************************************************************************
        public static int SyGet_VBic(DatosGlobales Globales,UnitOfWorkCext01 unit, string Swift, string Secuencial)
        {
            T_MODGBIC MODGBIC = Globales.MODGBIC;

            int SyGet_VBic = 0;

            string R = "";
            string Que = "";
            string SecSwf = "";

            try
            {
                MODGBIC.VBic = MODGBIC.VBicNul;

                if (Secuencial == "")
                {
                    SecSwf = "XXX";
                }
                else
                {
                    SecSwf = Secuencial;
                }


                var res = unit.SceRepository.EjecutarSP<sce_bic_s02_MS_Result>("sce_bic_s02_MS", Swift, SecSwf);
                // Genera Sentencia.
                
                

                // Se realizó el Query pero la consulta no retornó datos.
                if (res.Count==0)
                {
                    return SyGet_VBic;
                }
                var item = res.First();

                MODGBIC.VBic.BicSwf = item.bic_swf;
                MODGBIC.VBic.BicSec = item.bic_sec;
                MODGBIC.VBic.BicNom = item.bic_nom;
                MODGBIC.VBic.BicDes = item.bic_des;
                MODGBIC.VBic.BicCiu = item.bic_ciu;
                MODGBIC.VBic.BicDir = item.bic_dir;
                MODGBIC.VBic.BicPos = item.bic_pos;
                MODGBIC.VBic.BicPai = item.bic_pai;
                MODGBIC.VBic.BicAla = item.bic_ala;
                MODGBIC.VBic.BicCod = item.bic_cod;

                SyGet_VBic = true.ToInt();

                return SyGet_VBic;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text= "Se ha producido un error al tratar de leer los datos de los Bancos.",
                    Type=TipoMensaje.Error,
                    Title= T_MODGBIC.MsgBic
                });

            }
            return SyGet_VBic;
        }

        public static int GetBancoPaymentPlus(DatosGlobales Globales, UnitOfWorkSwift unit, string Swift, string Secuencial)
        {
            T_MODGBIC MODGBIC = Globales.MODGBIC;

            int SyGet_VBic = 0;

            string R = "";
            string Que = "";
            string SecSwf = "";

            try
            {
                MODGBIC.VBic = MODGBIC.VBicNul;

                if (Secuencial == "")
                {
                    SecSwf = "XXX";
                }
                else
                {
                    SecSwf = Secuencial;
                }


                var res = unit.PaymentPlusRepository.GetBancoPorSwift(Swift, SecSwf);
                // Genera Sentencia.



                // Se realizó el Query pero la consulta no retornó datos.
                if (res.Count == 0)
                {
                    return SyGet_VBic;
                }
                var item = res.First();

                MODGBIC.VBic.BicAla = false;    //TODO
                MODGBIC.VBic.BicCiu = item.trans_dsc_city;
                MODGBIC.VBic.BicCod = item.trans_dsc_iso_country_code;
                MODGBIC.VBic.BicDes = item.trans_dsc_zip_code + " " + item.trans_dsc_city;
                MODGBIC.VBic.BicDir = string.Join("", item.trans_dsc_street_address_1,
                    item.trans_dsc_street_address_2, item.trans_dsc_street_address_3, item.trans_dsc_street_address_4);
                MODGBIC.VBic.BicNom = item.trans_dsc_institution_name;
                MODGBIC.VBic.BicPai = item.trans_dsc_country_name;
                MODGBIC.VBic.BicPos = item.trans_dsc_zip_code;
                MODGBIC.VBic.BicSec = item.trans_dsc_branch_bic;
                MODGBIC.VBic.BicSwf = item.trans_dsc_bic8;

                SyGet_VBic = true.ToInt();

                return SyGet_VBic;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Se ha producido un error al tratar de leer los datos de los Bancos.",
                    Type = TipoMensaje.Error,
                    Title = T_MODGBIC.MsgBic
                });

            }
            return SyGet_VBic;
        }
    }
}
