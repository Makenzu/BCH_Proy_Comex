
namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGEX40
    {
        public static short Fn_GetMtsCV(string codope, short estado, string TipGra, string numneg, string tippro, string NumCpa, string numcuo, string NumCob)
        {
            
            //MIGRACION FUNCION 01-09-2015
            //short _retValue = 0;
            //string R = "";
            //short n = 0;
            //short i = 0;
            //try
            //{
            //    var result = unit.SceRepository.sce_mts_s01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 4, 2)),
            //                                               MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 11, 5)), decimal.Parse(numneg),
            //                                               decimal.Parse(tippro), decimal.Parse(NumCpa), decimal.Parse(numcuo), decimal.Parse(NumCob), estado, TipGra);
            //    if (result != null)
            //    {

            //        T_MODSWENN MODSWENN = initObject.MODSWENN;

            //        for (i = 0; i <= (short)VB6Helpers.UBound(result); i++)
            //        {
            //            MODSWENN.VMts[i].fecmsg = VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "F", R));
            //            MODSWENN.VMts[i].id_mensaje = VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
            //            MODSWENN.VMts[i].NroRpt = VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
            //            // R = Mdl_SRM.NuevaRespuesta(3, R);
            //        }

            //        _retValue = -1;
            //    }
            //    else
            //    {
            //        _retValue = 0;
            //    }
            
            
            short _retValue = 0;
            //string Que = "";
            //string R = "";
            //short n = 0;
            //short i = 0;
            //try
            //{
            //    Que = "";
            //    Que = Que + "Exec " + Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + "." + "sce_mts_s01_MS ";
            //    Que = VB6Helpers.LCase(Que);
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 1, 3)) + ", ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 4, 2)) + ", ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 6, 2)) + ", ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 8, 3)) + ", ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(codope, 11, 5)) + ", ";
            //    Que = Que + MODGSYB.dbnumesy(numneg) + ", ";
            //    Que = Que + MODGSYB.dbnumesy(tippro) + ", ";
            //    Que = Que + MODGSYB.dbnumesy(NumCpa) + ", ";
            //    Que = Que + MODGSYB.dbnumesy(numcuo) + ", ";
            //    Que = Que + MODGSYB.dbnumesy(NumCob) + ", ";
            //    Que = Que + MODGSYB.dbnumesy(estado) + ", ";
            //    Que += MODGSYB.dbcharSy(TipGra);

            //    //Se ejecuta el Procedimiento Almacenado.
            //    R = Mdl_SRM.RespuestaQuery(ref Que);
            //    if (Mdl_SRM.HayErr_Com(R) != 0)
            //    {
            //        VB6Helpers.MsgBox("Se ha producido un error de Comunicación al tratar Desactivar los Swift en la Base Cext01. El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", (MsgBoxStyle)MODGPYF0.pito(48), "SWIFT");
            //        goto Fn_GetMtsCVEnd;
            //    }

            //    //Resultado nulo de la Consulta.-
            //    InitializationObject. MODSWENN.VMts = new T_Mtes[1];
            //    if (R == "")
            //    {
            //        goto Fn_GetMtsCVEnd;
            //    }
            //    n = Mdl_SRM.RowCount;
            //    MODSWENN.VMts = new T_Mtes[n + 1];
            //    for (i = 1; i <= (short)n; i++)
            //    {
            //        MODSWENN.VMts[i].fecmsg = VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "F", R));
            //        MODSWENN.VMts[i].id_mensaje = VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
            //        MODSWENN.VMts[i].NroRpt = VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
            //        R = Mdl_SRM.NuevaRespuesta(3, R);
            //    }

            //    _retValue = (short)(true ? -1 : 0);

            //Fn_GetMtsCVEnd:
            return _retValue;
            //}
            //catch (Exception _ex)
            //{
            //    // IGNORED: Fn_GetMtsCVErr:
            //    VB6Helpers.SetError(_ex);
            //    VB6Helpers.MsgBox("[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number), (MsgBoxStyle)MODGPYF0.pito(48), "Planillas Visibles de Exportación");
            //    // UPGRADE_ISSUE (#04B8): The Resume keyword has been converted to a goto statement
            //    goto Fn_GetMtsCVEnd;
            //}
        }
    }
}

