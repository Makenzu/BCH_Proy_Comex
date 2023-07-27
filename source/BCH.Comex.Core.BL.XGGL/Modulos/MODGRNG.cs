using BCH.Comex.Common.Exceptions;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGRNG
    {
        // Retorna la estructura Codop con la siguiente Operación.
        // True : Exitoso.
        public static int SgteNumOpr(DatosGlobales Globales,UnitOfWorkCext01 unit,string RngPro, string RngDoc)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGRNG MODGRNG = Globales.MODGRNG;

            int SgteNumOpr = 0;

            double q = 0.0;
            int i = 0;
            int k = 0;
            int a = 0;

            // Si la Codop no está limpia, => Número de Operación actual.
            if (SYGETPRT.Codop.Id_Operacion != "")
            {
                SYGETPRT.Codop.Cent_costo = MODGUSR.UsrEsp.CentroCosto;
                SYGETPRT.Codop.Id_Product = RngPro;
                SYGETPRT.Codop.Id_Especia = MODGUSR.UsrEsp.Especialista;
                SYGETPRT.Codop.Id_Operacion = MigrationSupport.Utils.Format(SYGETPRT.Codop.Id_Operacion, "00000");
                SgteNumOpr = Convert.ToInt16(true);
                return SgteNumOpr;
            }

            // Inicializa variables de Rangos.
            a = SyGetn_xTrng(Globales,unit);

            k = -1;
            for (i = 1; i <= MODGRNG.RngDocs.GetUpperBound(0); i += 1)
            {
                if (MODGRNG.RngDocs[i].DocCod == RngDoc)
                {
                    k = i;
                    break;
                }
            }
            if (k == -1)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Title = T_MODGRNG.MsgRng,
                    Text= "No se conoce documento sobre el cual se pide el número.",
                    Type = Common.UI_Modulos.TipoMensaje.Error
                });
                return SgteNumOpr;
            }
            MODGRNG.RngGeneral = MODGRNG.RngDocs[k].DocGen;
            MODGRNG.VRng.RngDoc = MODGRNG.RngDocs[k].DocCod;
            if (MODGRNG.RngGeneral != 0)
            {
                MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                MODGRNG.VRng.RngEsp = "00";
                MODGRNG.VRng.RngRut = "0000000000";
            }
            else
            {
                MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                MODGRNG.VRng.RngEsp = MODGUSR.UsrEsp.Especialista;
                MODGRNG.VRng.RngRut = MODGUSR.UsrEsp.Rut;
            }

            // Actualización y lectura de rango seleccionado.-
            q = SyGetUpd_Rng(Globales,unit, MODGRNG.VRng.RngCct, MODGRNG.VRng.RngEsp, MODGRNG.VRng.RngDoc);
            if (q > 0)
            {
                SYGETPRT.Codop.Cent_costo = MODGUSR.UsrEsp.CentroCosto;
                SYGETPRT.Codop.Id_Product = RngPro;
                SYGETPRT.Codop.Id_Especia = MODGUSR.UsrEsp.Especialista;
                SYGETPRT.Codop.Id_Operacion = MigrationSupport.Utils.Format(q, "00000");
                SgteNumOpr = Convert.ToInt16(true);
            }
            else if (q == 0)
            {
                throw new ComexUserException("No fue posible obtener un número de operación válido, debido a que el usuario no tiene rango asignado para el producto " + RngPro);
            }
            else
            {
                throw new ComexUserException("Ya se han asignado todos los numeros permitidos para esta operación.");
            }

            return SgteNumOpr;
        }

        // Lee las Listado de Números de Operaciones.
        // Retorno    <> ""  : Lectura Exitosa.
        //            =  ""  : Error o Lectura no Exitosa.
        public static int SyGetn_xTrng(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGRNG MODGRNG = Globales.MODGRNG;


            int SyGetn_xTrng = 0;

            int i = 0;
            string R = string.Empty;
            string Que = string.Empty;
            int n = 0;
            string ResultadoQuery = string.Empty;

            // Verifica si ya se leyó la tabla.-
            n = MODGRNG.RngDocs.GetUpperBound(0);

            //if (n == 0)
            //{
            //    MODGRNG.RngDocs = new Type_RngDoc[1];
            //}
            if (n > 0)
            {
                SyGetn_xTrng = Convert.ToInt16(true);
                return SyGetn_xTrng;
            }

            try
            {
                var aux = unit.SceRepository.EjecutarSP<sce_trng_s01_MS_Result>("sce_trng_s01_MS");
                aux.Insert(0, new sce_trng_s01_MS_Result());
                MODGRNG.RngDocs = aux.Select(x => new Type_RngDoc() { DocCod = x.codnope, DocGen = (int)x.grlnope }).ToArray();
                SyGetn_xTrng = true.ToInt();
            }
            catch(Exception e)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text = "Se ha producido un error al tratar de leer los Números de Operaciones Posibles(Sce_Trng).",
                    Title= "Números de Operaciones",
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });
            }
            return SyGetn_xTrng;
        }

        // Lee y actualiza los números de Rangos.
        public static double SyGetUpd_Rng(DatosGlobales Globales, UnitOfWorkCext01 unit, string Codcct, string codesp, string codfun)
        {
            double SyGetUpd_Rng = 0.0;
            try
            {
                var mensaje = string.Empty;
                return unit.SceRepository.sce_rng_u01_MS(Codcct, codesp, codfun, out mensaje);
            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text= "Se ha producido un error al tratar de leer los datos de la Asignación de Rangos (Sce_Rng).",
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });
            }
            return SyGetUpd_Rng;
        }

        // Lee un número asociado a la operación que se pide.
        public static double LeeSceRng(DatosGlobales Globales,UnitOfWorkCext01 unit, string RngDoc)
        {
            T_MODGRNG MODGRNG = Globales.MODGRNG;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            double LeeSceRng = 0.0;

            double z = 0.0;
            int i = 0;
            int k = 0;
            int a = 0;

            LeeSceRng = true.ToDbl();

            // Inicializa variables de Rangos.
            a = SyGetn_xTrng(Globales,unit);

            k = -1;
            for (i = 1; i <= MODGRNG.RngDocs.GetUpperBound(0); i += 1)
            {
                if (MODGRNG.RngDocs[i].DocCod == RngDoc)
                {
                    k = i;
                    break;
                }
            }
            if (k == -1)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text= "No se conoce documento sobre el cual se pide el número.",
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Title= T_MODGRNG.MsgRng
                });
                return LeeSceRng;
            }
            MODGRNG.RngGeneral = MODGRNG.RngDocs[k].DocGen;
            MODGRNG.VRng.RngDoc = MODGRNG.RngDocs[k].DocCod;
            if (MODGRNG.RngGeneral != 0)
            {
                MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                MODGRNG.VRng.RngEsp = "00";
                MODGRNG.VRng.RngRut = "0000000000";
            }
            else
            {
                MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                MODGRNG.VRng.RngEsp = MODGUSR.UsrEsp.Especialista;
                MODGRNG.VRng.RngRut = MODGUSR.UsrEsp.Rut;
            }

            z = SyGetUpd_Rng(Globales,unit, MODGRNG.VRng.RngCct, MODGRNG.VRng.RngEsp, MODGRNG.VRng.RngDoc);
            switch (z.ToInt())
            {
                case -1:
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type=Common.UI_Modulos.TipoMensaje.Error,
                        Text= "Ya se han asignado todos los numeros permitidos para esta operación.",
                        Title= T_MODGRNG.MsgRng
                    });
                    break;
                case 0:
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se pudo establecer el Número requerido para la Operación.",
                        Title = T_MODGRNG.MsgRng
                    });
                    break;
                default:
                    LeeSceRng = z;
                    break;
            }

            return LeeSceRng;
        }
    }
}
