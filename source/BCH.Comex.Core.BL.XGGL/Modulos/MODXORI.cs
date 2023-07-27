using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODXORI
    {
        public static T_MODXORI GetMODXORI()
        {
            return new T_MODXORI();
        }

        public static bool SyGet_CtaCte(DatosGlobales initObj, UnitOfWorkCext01 unit, string Party)
        {
            try
            {
                var result = unit.SceRepository.sce_ctas_s03_MS(Party);
                if (result.cuenta == null)
                {
                    return false;
                }
                initObj.MODXORI.gs_ctacte_party = result.cuenta;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static void Inet1_StateChanged(string TOKEN, DatosGlobales initObj)
        {

            if (TOKEN == "YTD" || TOKEN == "YEX")
            {
                initObj.MODXORI.gb_esCosmos = true;
                initObj.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: Cliente posee Cuenta Cosmos ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
            }

            if (TOKEN == "CTD" || TOKEN == "CEX")
            {
                initObj.MODXORI.gb_esCosmos = false;
                initObj.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: Cliente posee Cuenta Banco de Chile ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
            }

            if (string.IsNullOrEmpty(TOKEN))
            {
                initObj.MESSAGES.Add(new UI_Message
                {
                    Text = "No se encontró el Tipo de Cuenta del Participante, Se asume que es una Cuenta Banco de Chile.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });

            }

        }

        public static string SrvGetCtaCos(string nCta)
        {
            string token = null;

            try
            {
                token = XCFTServices.ConsultaCuentaCorriente(nCta);
            }
            catch (Exception)
            {
                //TODO: manejo de excepcion
            }

            return token;
        }

        public static string Get_CtaCte(UnitOfWorkCext01 unit, string Party)
        {
            try
            {
                var result = unit.SceRepository.sce_ctas_s03_MS(Party);
                if (result == null || result.cuenta == null)
                {
                    return "";
                }
                return result.cuenta;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
