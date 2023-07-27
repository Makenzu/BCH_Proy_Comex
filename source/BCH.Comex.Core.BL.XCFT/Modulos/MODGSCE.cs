using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGSCE
    {

        public static T_MODGSCE GetMODGSCE(){
            return new T_MODGSCE();
        }

        public static short GetSceGen(T_MODGMTA MODGMTA, T_MODGUSR MODGUSR, T_MODGSCE MODGSCE, UI_Mdi_Principal Mdi_Principal, 
            IDatosUsuario datosUsuario, UnitOfWorkCext01 unit)
        {
            try
            {
                MODGSCE.VGen.MndNac = short.Parse(Mdl_Acceso.GetConfigValue("FundTransfer.General.MndNac"));
                MODGSCE.VGen.MndDol = short.Parse(Mdl_Acceso.GetConfigValue("FundTransfer.General.MndDol"));
                MODGSCE.VGen.CodPbc = short.Parse(datosUsuario.CodPBC);//short.Parse(datosUsuario.MODGUSR_UsrEsp_CentroCosto_CodPBC??"0");// Mdl_Acceso.GetConfigValue("FundTransfer." + MODGUSR.UsrEsp.CentroCosto + ".CodPBC"));
                MODGSCE.VGen.CodBch = short.Parse(datosUsuario.CodBCH);//short.Parse(datosUsuario.MODGUSR_UsrEsp_CentroCosto_CodBCH ?? "0");// Mdl_Acceso.GetConfigValue("FundTransfer." + MODGUSR.UsrEsp.CentroCosto + ".CodBCH"));
                MODGSCE.VGen.SucBCH = short.Parse(datosUsuario.SucBCH);//short.Parse(datosUsuario.MODGUSR_UsrEsp_CentroCosto_SucBCH ?? "0");// Mdl_Acceso.GetConfigValue("FundTransfer." + MODGUSR.UsrEsp.CentroCosto + ".SucBCH"));
                MODGSCE.VGen.CodBCCh = short.Parse(datosUsuario.CodBCCH);//short.Parse(datosUsuario.MODGUSR_UsrEsp_CentroCosto_CodBCCH ?? "0");//  Mdl_Acceso.GetConfigValue("FundTransfer." + MODGUSR.UsrEsp.CentroCosto + ".CodBCCH"));

                short a = BCH.Comex.Core.BL.XCFT.Modulos.MODGMTA.FindImp(MODGMTA,unit, "IVA");
                if (a < 0)
                {
                    //error
                }
                MODGSCE.VGen.MtoIva = MODGMTA.VImp[a].tasmax;

                if (MODGSCE.VGen.MndNac == 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Código de la Moneda Nacional." });
                    //VB6Helpers.MsgBox("No se ha podido establecer el Código de la Moneda Nacional.", MsgBoxStyle.Information, "Datos Generales");
                    return 0;
                }

                if (MODGSCE.VGen.MndDol == 0)
                {
                    //VB6Helpers.MsgBox("No se ha podido establecer el Código de la Moneda Extranjera.", MsgBoxStyle.Information, "Datos Generales");
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Código de la Moneda Extranjera." });
                    return 0;
                }

                if (MODGSCE.VGen.CodPbc == 0)
                {
                    //VB6Helpers.MsgBox("No se ha podido establecer el Código de la Plaza del Banco Central.", MsgBoxStyle.Information, "Datos Generales");
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Código de la Plaza del Banco Central." });
                    return 0;
                }

                if (MODGSCE.VGen.CodBch == 0)
                {
                    //VB6Helpers.MsgBox("No se ha podido establecer el Código de la Entidad Banco Chile.", MsgBoxStyle.Information, "Datos Generales");
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Código de la Entidad Banco Chile." });
                    return 0;
                }

                if (MODGSCE.VGen.SucBCH == 0)
                {
                    //VB6Helpers.MsgBox("No se ha podido establecer el Código de la Sucursal Banco Chile.", MsgBoxStyle.Information, "Datos Generales");
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Código de la Sucursal Banco Chile." });
                    return 0;
                }

                if (MODGSCE.VGen.CodBCCh == 0)
                {
                    //VB6Helpers.MsgBox("No se ha podido establecer el Código del Banco Central de Chile.", MsgBoxStyle.Information, "Datos Generales");
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Código del Banco Central de Chile." });
                    return 0;
                }

                if (MODGSCE.VGen.MtoIva == 0)
                {
                    //VB6Helpers.MsgBox("No se ha podido establecer el Monto asociado al IVA.", MsgBoxStyle.Information, "Datos Generales");
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se ha podido establecer el Monto asociado al IVA." });
                    return 0;
                }

                SybGet_gen(MODGSCE, MODGMTA,unit);

                return -1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public static short SybGet_gen(T_MODGSCE MODGSCE, T_MODGMTA MODGMTA, UnitOfWorkCext01 unit) {
            short _retValue;
            try
            {
                //var cheques = unit.SceRepository.EjecutarSP<sce_mta3_s03_MS_Result>("sce_mta3_s03_MS","SCH").First();
                //MODGSCE.VGen.MtoDeb = (double)cheques.mtomin;
                MODGSCE.VGen.MtoDeb = (double)(MODGMTA.VImp[T_MODGMTA.VImpDict["SCH"]].MtoMin);
                var impflag = unit.SceRepository.EjecutarSP<bool>("sce_impflag_s01_MS").First();
                T_MODGMTA.impflag = (short)(impflag ? -1 : 0);
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }
    }
}
