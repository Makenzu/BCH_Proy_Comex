using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODGMEM
    {
        // Retorna un arreglo de Lineas para un string de largo especificado.-
        public static void GetLineas(string Dato, ref string[] Arreglo, int largo, string Tabla)
        {
            int n = 0;
            string s = "";

            Arreglo = new string[1];
            s = Dato;
            while(!string.IsNullOrEmpty(s))
            {
                n = Arreglo.GetUpperBound(0) + 1;
                Array.Resize(ref Arreglo, n + 1);
                Arreglo[n] = s.Left(largo);
                // No Válido para Swift, sólo cartas.-
                if (Arreglo[n].Mid(255, 1) == " " && Tabla != "s")
                {
                    Arreglo[n] = Arreglo[n].Left(254) + "Ç";
                }
                if (s.Len() < largo)
                {
                    s = "";
                }
                else
                {
                    s = s.Right((s.Len() - largo));
                }
            }

        }
        // Retorna un código de memo.-
        public static int SyGetMax_Mem(XegiService service, string Tabla)
        {
            int SyGetMax_Mem = 0;

            int n = 0;
            string R = "";
            string Que = "";

            try
            {

                // Obtiene el Correlativo para el memo.-
                /*Que = "";
                // Que$ = Que$ + "Exec " + ParamSrm8K.Base + "." + ParamSrm8K.Usuario + "." + "ScePAMemg11 "
                Que = Que + "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "Sce_Memg_s02 ";
                Que = Que + MODGSYB.dbcharSy(Tabla);
                Que = Que.LCase();

                // Se ejecuta el Procedimiento Almacenado.-
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = service.ScePAMemg11(MODGSYB.dbcharSy(Tabla));

                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer el correlativo del Memo (Sce_Mem*). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), "");
                    return SyGetMax_Mem;
                }

                // Resultado nulo de la Consulta.-
                n = MODGSRM.RowCount;
                SyGetMax_Mem = (MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R).ToInt() + 1).ToInt();

                return SyGetMax_Mem;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }

        // Lee un Campo Memo.-
        // Retorno    <> ""  : Lectura Exitosa.-
        //            =  ""  : Error o Lectura no Exitosa.-
        public static string SyGetn_Mem(UnitOfWorkCext01 uow, string Tabla, int CodMem)
        {
            string SyGetn_Mem = "";
            string R = "";
            try
            {
                // Si no viene el código del memo => retornar vacío.-
                if (CodMem == 0)
                {
                    return SyGetn_Mem;
                }

                // Confeccionar consulta.-
                R = uow.SceRepository.sce_memg_s01_MS(MODGSYB.dbcharSy(Tabla), MODGSYB.dbnumesy(CodMem));
                return R;
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
        // Lee un Campo Memo.-
        // Retorno    <> ""  : Lectura Exitosa.-
        //            =  ""  : Error o Lectura no Exitosa.-
        public static string SyGetn_RMem(XegiService service, string Tabla, int CodMem)
        {
            string SyGetn_RMem = "";

            string MsgxCob = "";
            string s = "";
            string z = "";
            int i = 0;
            int n = 0;
            string R = "";
            string Que = "";

            string ResultadoQuery = "";
            try
            {

                // Si no viene el código del memo => retornar vacío.-
                if (CodMem == 0)
                {
                    return SyGetn_RMem;
                }

                // Confeccionar consulta.-
                /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "rce_memg_s01_MS ";
                Que = Que + MODGSYB.dbcharSy(Tabla) + ", ";
                Que = Que + MODGSYB.dbnumesy(CodMem);
                Que = Que.LCase();

                R = MODGSRM.RespuestaQuery(ref Que);*/
                R = service.rce_memg_s01_MS(MODGSYB.dbcharSy(Tabla), MODGSYB.dbnumesy(CodMem));

                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer el Campo Memo (Sce_Mem*). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle
                       >(), "Lectura de Campos Memo");
                    return SyGetn_RMem;
                }

                // Resultado nulo de la Consulta.-
                n = MODGSRM.RowCount;
                for (i = 1; i <= n; i += 1)
                {
                    z = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).ToStr();
                    switch (Tabla)
                    {
                        case "s":
                            z = MODGDOC.Componer(z, "*", " ");
                            break;
                        case "p":
                            break;
                        default:
                            if (z.Mid(255, 1) == "Ç")
                            {
                                z = z.Left(254) + " ";
                            }
                            break;
                    }
                    s = s + z;
                    R = MODGSRM.NuevaRespuesta(1, R);
                }
                SyGetn_RMem = s;

                return SyGetn_RMem;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
        // Graba un Campo Memo y retorna el código de éste.-
        // Retorno    <> 0    : Retorna el # del Memo.-
        //            =  0    : Error o Grabación no Exitosa.-
        public static int SyPutn_Mem(XegiService service, string Tabla, int CodMem, string Memo)
        {
            int SyPutn_Mem = 0;

            int n = 0;
            int i = 0;
            bool HayError = false;
            int x = 0;
            string MsgxCob = "";
            string R = "";
            string Que = "";
            int M = 0;

            string[] Lineas = null;
            try
            {

                // Si no existe el memo => Se procede a obtener su código.-
                if(CodMem == 0 && !string.IsNullOrEmpty(Memo))
                {
                    switch (Tabla.LCase())
                    {
                        case "x":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memx).ToInt();
                            break;
                        case "m":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memm).ToInt();
                            break;
                        case "s":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Mems).ToInt();
                            break;
                        case "y":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memy).ToInt();
                            break;
                        case "i":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memi).ToInt();
                            break;
                        case "p":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memp).ToInt();
                            break;
                        case "f":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memf).ToInt();
                            break;
                        case "jm":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memjm).ToInt();
                            break;
                        case "jd":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memjd).ToInt();
                            break;
                        case "e":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Meme).ToInt();
                            break;
                        case "lm":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memlm).ToInt();
                            break;
                        case "ld":
                            M = MODGRNG.LeeSceRng(service, MODGRNG.Rng_Memld).ToInt();
                            break;
                    }
                    if (M <= 0)
                    {
                        return SyPutn_Mem;
                    }
                }

                // Si el Memo existe
                if (CodMem != 0)
                {
                    /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_memg_d01_MS ";
                    Que = Que + MODGSYB.dbcharSy(Tabla) + ", ";
                    Que = Que + MODGSYB.dbnumesy(CodMem);
                    Que = Que.LCase();
                    R = MODGSRM.RespuestaQuery(ref Que);*/

                    R = service.sce_memg_d01_MS(MODGSYB.dbcharSy(Tabla), MODGSYB.dbnumesy(CodMem));

                    if (R == "-1" || MODGDOC.CopiarDeString(MODGSRM.ParamSrm8k.Mensaje, "~", 2) != "Borrado Exitoso")
                    {
                        MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de borrar el campo Memo (Sce_Mem*). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle
                           >(), MsgxCob);
                        return SyPutn_Mem;
                    }
                    M = CodMem;
                }
                if (M == 0)
                {
                    return SyPutn_Mem;
                }

                // Para un string determinado se confecciona un arreglo de lineas.-
                GetLineas(Memo, ref Lineas, 255, Tabla);

                // Hace un Put en Sce_Mem.-
                HayError = false;
                for (i = 1; i <= Lineas.GetUpperBound(0); i += 1)
                {
                    /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_memg_i01_MS ";
                    Que = Que.LCase();
                    Que = Que + MODGSYB.dbcharSy(Tabla) + " , ";
                    Que = Que + MODGSYB.dbnumesy(M) + " , ";
                    Que = Que + MODGSYB.dbnumesy(i) + ", ";
                    if (Tabla == "s")
                    {
                        Que = Que + MODGSYB.dbcharSy(MODGDOC.Componer(Lineas[i], " ", "*"));
                    }
                    else
                    {
                        Que = Que + MODGSYB.dbcharSy(Lineas[i]);
                    }
                    // Se ejecuta el Procedimiento Almacenado.-
                    R = MODGSRM.RespuestaQuery(ref Que);*/

                    R = service.sce_memg_i01_MS(MODGSYB.dbcharSy(Tabla), MODGSYB.dbnumesy(M), MODGSYB.dbnumesy(i),
                        Tabla == "s" ? MODGSYB.dbcharSy(MODGDOC.Componer(Lineas[i], " ", "*")) : MODGSYB.dbcharSy(Lineas[i]));

                    if (R == "-1" || MODGDOC.CopiarDeString(MODGSRM.ParamSrm8k.Mensaje, "~", 2) != "Grabación Exitosa")
                    {
                        HayError = true;
                        MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de grabar el Campo Memo (Sce_Mem*). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle
                           >(), MsgxCob);
                    }
                }

                // Si el Memo existe
                /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_memg_s03_MS ";
                Que = Que + MODGSYB.dbcharSy(Tabla) + ", ";
                Que = Que + MODGSYB.dbnumesy(M);
                Que = Que.LCase();
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = service.sce_memg_s03_MS(MODGSYB.dbcharSy(Tabla), MODGSYB.dbnumesy(M));
                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de Validar la Existencia del Campo Memo (Memg_s03). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), "Validación de Campos Memo");
                    return SyPutn_Mem;
                }
                n = MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R).ToInt();
                if (n != Lineas.GetUpperBound(0))
                {
                    MigrationSupport.Utils.MsgBox("El Campo Memo se ha grabado parcialmente. Intente una nueva grabación o Reporte el problema a Sistemas (Memg_s03). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",
                       MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "Validación de Campos Memo");
                    return SyPutn_Mem;
                }

                if (!HayError)
                {
                    SyPutn_Mem = M;
                }

                return SyPutn_Mem;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
    }
}
