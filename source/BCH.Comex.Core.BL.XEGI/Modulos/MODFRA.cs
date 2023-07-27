using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODFRA
    {
        public const string MsgFra = "Frases Estandares";
        public static string RefBae = "";
        // CFV-10/11/2006
        public static string CENTRO_COSTO = "";
        public static void Get_RefBae(UnitOfWorkCext01 uow, string NumOpe)
        {
            /*string R = "";
            string Que = "";
            string RutaSyb = "";

            RutaSyb = MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + ".";

            Que = "";
            Que = Que + "exec " + RutaSyb + "sce_refe_s01_MS ";
            Que = Que.LCase();
            Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(1, 3)) + " , ";
            Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(4, 2)) + " , ";
            Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(6, 2)) + " , ";
            Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(8, 3)) + " , ";
            Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(11, 5));

            R = MODGSRM.RespuestaQuery(ref Que);

            if (R == "-1")
            {
               MigrationSupport.Utils.MsgBox("Se ha producido un error al leer el número de referencia BAE. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(80) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                  "Servidor de Impresión");
               return;
            }

            if (R != "")
            {
               RefBae = MODGSYB.GetPosSy(MODGSYB.NumIni(),"C",R).ToStr();
            }
            else
            {
               RefBae = "";
            }*/

            RefBae = uow.SceRepository.sce_refe_s01_MS(
                MODGSYB.dbcharSy(NumOpe.Mid(1, 3)),
                MODGSYB.dbcharSy(NumOpe.Mid(4, 2)),
                MODGSYB.dbcharSy(NumOpe.Mid(6, 2)),
                MODGSYB.dbcharSy(NumOpe.Mid(8, 3)),
                MODGSYB.dbcharSy(NumOpe.Mid(11, 5)));

        }
        // forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
        // en "Donde".  Si no encuentra ninguna retorna "Donde"
        public static string ComponerUna(string Donde, string Que, string En)
        {
            string ComponerUna = "";

            int Aqui = 0;
            string Sale = "";

            Sale = Donde;
            Aqui = Sale.InStr(Que, 1, StringComparison.CurrentCulture);
            if (Aqui != 0)
            {
                Sale = Sale.Left((Aqui - 1)) + En + Sale.Mid((Aqui + Que.Len()));
            }
            ComponerUna = Sale;

            return ComponerUna;
        }
        // ****************************************************************************
        //    1.  Lee una frase dependiendo del código de esta + Idioma.
        //    2.  Luego la concatena con una cadena variable.
        // ****************************************************************************
        public static string SyGet_Fra(UnitOfWorkCext01 uow, int CodFra, string Idioma, string cadena)
        {
            string SyGet_Fra = "";

            string s = "";
            int i = 0;
            int n = 0;
            int CodMemo = 0;
            string Frase = "";
            List<object> R = null;
            
            try
            {
                var result = uow.SceRepository.sce_fra_s06_MS(MODGSYB.dbnumesy(CodFra), MODGSYB.dbcharSy(Idioma));

                // Resultado nulo de la Consulta.
                if (result.Count == 0) return string.Empty;
                Frase = result[0].frase.ToString();
                CodMemo = Convert.ToInt32(result[0].numero);

                // Rescata en campo Memo.
                if (CodMemo > 0)
                {
                    Frase = MODGMEM.SyGetn_Mem(uow, "f", CodMemo);
                }

                // --------------------------------------
                // Concatena la Frase con la Cadena variable.
                if(!string.IsNullOrEmpty(cadena.TrimB()))
                {
                    n = MODGDOC.CuentaDeString(cadena, "~");
                    for (i = 1; i <= n; i += 1)
                    {
                        s = MODGDOC.CopiarDeString(cadena, "~", i);
                        if(!string.IsNullOrEmpty(Frase.TrimB()))
                        {
                            Frase = ComponerUna(Frase, "@", s);
                        }
                    }
                }
                SyGet_Fra = Frase.TrimB();
                return SyGet_Fra;
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
    }
}
