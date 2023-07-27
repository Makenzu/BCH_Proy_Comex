using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODABDG
    {
        // Limpia los registros de intentos anteriores .-
        public static int DesActivaBD(DatosGlobales Globales,UnitOfWorkCext01 unit, string OpeSin, string CodAnu, int NroRpt, string FecMov)
        {
            try
            {
                string a = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3);
                string b = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2);
                string c = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2);
                string d = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3);
                string e = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5);
                unit.SceRepository.EjecutarSP<object>("sce_gen_d02", a, b, c, d, e, CodAnu, MODGSYB.dbnumesy(NroRpt), MODGSYB.dbdatesy(FecMov));
                return -1;
            }
            catch (Exception _ex)
            {
                throw new Exception("Se ha producido un error al tratar de desactivar los registros anulados (Gen_D02).");
            }
        }

        //Activa todos los Registros de las Bases de Datos.-
        public static int ActivaBases(DatosGlobales Globales, UnitOfWorkCext01 unit, string OpeSin, string CodAnu)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int ActivaBases = 0;

            int i = 0;
            int n = 0;
            int X = 0;

            // Contabilidad.-
            if (MODGCON0.VMch.NroRpt > 0)
            {
                ActivaBD_Con(Globales,unit);
            }

            // Swift's.-
            n = 0;
            for (i = 1; i <= MODGSWF.VSwf.GetUpperBound(0); i += 1)
            {
                n = n + 1;
            }
            if (n > 0)
            {
                ActivaBD_Swf(Globales,unit,OpeSin, CodAnu);
            }

            // Cheques.-
            n = 0;
            for (i = 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_CHEQUE)
                {
                    n = n + 1;
                }
            }
            if (n > 0)
            {
                ActivaBD_Chq(Globales,unit,OpeSin, CodAnu);
            }

            // Vale Vista.-
            n = 0;
            for (i = 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    n = n + 1;
                }
            }
            if (n > 0)
            {
                ActivaBD_Vvi(Globales,unit, OpeSin, CodAnu);
            }

            ActivaBases = true.ToInt();

            return ActivaBases;
        }

        //Activa un Reporte Contable.-
        public static void ActivaBD_Con(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            BCH.Comex.Core.BL.XGGL.Modulos.MODXDATA.Cmd_Put(Globales, () =>
            {
                return unit.SceRepository.sce_mch_u02_MS(Globales.MODGCON0.VMch.NroRpt, MODGSYB.dbdatesy(Globales.MODGCON0.VMch.FecMov), T_MODGCON0.ECC_ING);
            });
        }

        //Activa los Swift's.-        
        public static short ActivaBD_Swf(DatosGlobales Globales, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            short _retValue = 0;
            try
            {
                BCH.Comex.Core.BL.XGGL.Modulos.MODXDATA.Cmd_Put(Globales, () =>
                {
                    return unit.SceRepository.sce_swf_u01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                });

                return _retValue;
            }
            catch (Exception _ex)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODGCON0.MsgCon
                });
                throw _ex;
            }
        }


        //Activa Cheques.-
        public static void ActivaBD_Chq(DatosGlobales Globales, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            BCH.Comex.Core.BL.XGGL.Modulos.MODXDATA.Cmd_Put(Globales, () =>
            {
                return unit.SceRepository.sce_chq_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
            });       
        }

        //Activa Vales Vistas.-        
        public static void ActivaBD_Vvi(DatosGlobales Globales, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            BCH.Comex.Core.BL.XGGL.Modulos.MODXDATA.Cmd_Put(Globales, () =>
            {
                return unit.SceRepository.sce_vvi_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
            });       
        }
    }
}
