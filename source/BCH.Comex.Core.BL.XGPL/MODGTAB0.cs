using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPL
{
    /// <summary>
    /// Tablas Generales
    /// </summary>
    public static class MODGTAB0
    {
        public static string GetNombreAduana(decimal codigoAduana, XgplService service)
        {
            string nombre;
            if (service.ObtenerAduanas().TryGetValue(codigoAduana, out nombre))
            {
                return nombre;
            }
            return "";
        }

        public static string GetNombrePlazaBancoCentral(int codigoPlazaBancoCentral, XgplService service)
        {
            string nombre;
            if (service.ObtenerPlazasBancoCentral().TryGetValue(codigoPlazaBancoCentral, out nombre))
            {
                return nombre;
            }
            return "";
        }

        public static string GetDireccionParty(string party, byte indDireccion, XgplService service)
        {
            return service.Sce_Dad_S04(party.PadRight(12, '|'), indDireccion, "DC").FirstOrDefault();
        }

        public static string GetNombreParty(string party, int indNombre, XgplService service)
        {
            return service.Sce_Rsa_S03(party.PadRight(12, '|'), indNombre).FirstOrDefault();
        }

        public static string GetNombreMoneda(int CodigoMoneda, XgplService service)
        {
            return service.GetNombreMoneda(CodigoMoneda);
        }

        public static short GetCodigoMonedaBancoCentral(int codigoMoneda, XgplService service)
        {
            return service.GetCodigoMonedaBancoCentral(codigoMoneda);
        }

        public static string GetNombreMonedaBancoCentral(int CodigoMonedaBancoCentral, XgplService service)
        {
            return service.GetNombreMonedaBancoCentral(CodigoMonedaBancoCentral);
        }

        public static string GetSimboloMoneda(int CodigoMoneda, XgplService service)
        {
            return service.GetSimboloMoneda(CodigoMoneda);
        }

        public static string GetNombrePais(int CodigoPais, XgplService service)
        {
            return service.GetNombrePais(CodigoPais);
        }

        public static IQueryable GetPaises(XgplService service)
        {
            return service.GetPaises();
        }

        public static bool EsFechaHabil(DateTime fecha, XgplService service)
        {
            return !EsFeriado(fecha, service) && !EsFinDeSemana(fecha);
        }

        public static bool EsFeriado(DateTime fecha, XgplService service)
        {
            return service.GetFeriados().Where(f => f == fecha).SingleOrDefault() != null;
        }

        public static bool EsFinDeSemana(DateTime fecha)
        {
            return fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday;
        }

        public static string GetNombreBanco(int codigoBanco, XgplService service)
        {
            return service.GetNombreBanco(codigoBanco);
        }

        public static string GetNombreCodigoComercio(string CodigoComercio, string Concepto, XgplService service)
        {
            return service.GetNombreCodigoComercio(CodigoComercio, Concepto);
        }

        public static bool EsFecha2000(DateTime fecha)
        {
            return Math.Abs(fecha.Year - DateTime.Now.Year) <= 20;
        }
    }
}
