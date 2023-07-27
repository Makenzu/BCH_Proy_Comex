using System;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODGUSR1
    {
        // Estructura para mantener los datos de los especialistas
        public struct T_Usr
        {
            public string CenCos;
            public string CodEsp;
            public string CenSup;
            public string CodSup;
            public string NomEsp;
            public int Jerarquia;
            public string FecIni;
            public string FecFin;
            public string FecOut;
            public int ConFin;
        }
        public static T_Usr[] VUsr = null;
        public const string MsgCie = "Cierre Diario de Comercio Exterior";
        // Lee todos los usuarios asociados a un centro de costo.
        // Retorno <> 0 Lectura exitosa
        //          = 0 Error o letura no exitosa
        public static int SyGetn_Usr1(XegiService service, string CenCos)
        {
            int SyGetn_Usr1 = 0;

            int I = 0;
            int n = 0;
            string MsgUsr = "";
            string R = "";
            string Que = "";

            try
            {

                /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_usr_s03_MS ";
                Que = Que.LCase();
                Que = Que + MODGSYB.dbcharSy(CenCos);

                // Se ejecuta el Procedimiento Almacenado.-
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = service.sce_usr_s03_MS(MODGSYB.dbcharSy(CenCos));

                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al leer los usuarios asociados al centro de costo " + CenCos + ". El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgUsr);
                    return SyGetn_Usr1;
                }

                if (R == "")
                {
                    MigrationSupport.Utils.MsgBox("No existen usuarios asociados al centro de costo " + CenCos + ".", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MsgUsr);
                    return SyGetn_Usr1;
                }

                n = MODGSRM.RowCount;
                VUsr = new T_Usr[n + 1];

                for (I = 1; I <= n; I += 1)
                {
                    VUsr[I].CenCos = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).ToStr();
                    VUsr[I].CodEsp = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                    VUsr[I].CenSup = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                    VUsr[I].CodSup = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                    VUsr[I].NomEsp = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                    VUsr[I].Jerarquia = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                    VUsr[I].FecIni = MODGSYB.GetPosSy(MODGSYB.NumSig(), "D", R).ToStr();
                    VUsr[I].FecFin = MODGSYB.GetPosSy(MODGSYB.NumSig(), "D", R).ToStr();
                    VUsr[I].FecOut = MODGSYB.GetPosSy(MODGSYB.NumSig(), "D", R).ToStr();
                    // --------------------------------------------
                    if(!string.IsNullOrEmpty(VUsr[I].FecIni) && !string.IsNullOrEmpty(VUsr[I].FecFin))
                    {
                        if (String.CompareOrdinal(MigrationSupport.Utils.Format(VUsr[I].FecIni, "yyyymmdd") + MigrationSupport.Utils.Format(VUsr[I].FecIni, "hhmmss"), MigrationSupport.Utils.Format(VUsr[I].FecFin, "yyyymmdd") + MigrationSupport.Utils.Format(VUsr[I].FecFin, "hhmmss"))
                           < 0)
                        {
                            VUsr[I].ConFin = true.ToInt();
                        }
                    }
                    R = MODGSRM.NuevaRespuesta(9, R);
                }

                SyGetn_Usr1 = true.ToInt();

                return SyGetn_Usr1;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
        // Graba un Campo Memo y retorna el código de éste.-
        // Retorno    <> 0    : Grabación Exitosa-
        //            =  0    : Error o Grabación no Exitosa.-
        public static int SyUpd_Usr(XegiService service, string CenCos, string CodUsr, string CodFec)
        {
            int SyUpd_Usr = 0;

            string MsgUsr = "";
            bool HayError = false;
            string R = "";
            string Que = "";

            try
            {

                /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_usr_u02_MS ";
                Que = Que.LCase();
                Que = Que + MODGSYB.dbcharSy(CenCos) + " , ";
                Que = Que + MODGSYB.dbcharSy(CodUsr) + " , ";
                Que = Que + MODGSYB.dbcharSy(CodFec) + " , ";
                Que = Que + MODGSYB.dbcharSy(MigrationSupport.Utils.Format(DateTime.Now,"mm/dd/yyyy") + " " + MigrationSupport.Utils.Format(DateTime.Now,"hh:mm:ss"));

                // Se ejecuta el Procedimiento Almacenado.-
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = service.sce_usr_u02_MS(MODGSYB.dbcharSy(CenCos), MODGSYB.dbcharSy(CodUsr), MODGSYB.dbcharSy(CodFec),
                    MODGSYB.dbcharSy(MigrationSupport.Utils.Format(DateTime.Now,"mm/dd/yyyy") + " " + MigrationSupport.Utils.Format(DateTime.Now,"hh:mm:ss")));

                if (R == "-1" || MODGDOC.CopiarDeString(MODGSRM.ParamSrm8k.Mensaje, "~", 2) != "Grabacion Exitosa")
                {
                    HayError = true;
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de actualizar la fecha en Sce_Usr. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle
                       >(), MsgUsr);
                }

                SyUpd_Usr = (!HayError).ToInt();

                return SyUpd_Usr;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
    }
}
