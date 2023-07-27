using BCH.Comex.Core.Entities.Swift;

namespace BCH.Comex.Core.BL.SWEM
{
    public static class FormatUtils
    {
        public static string FormatMontoUSD(string pvarMoneda, string pvarDescripcion)
        {

            string wvarCopia = pvarDescripcion;
            int largo = wvarCopia.Length;
            string wvarMoneda = pvarDescripcion.Substring(0, 3);
            string wvarMonedaAux;
            string wvarMonto = wvarCopia.Substring(3, largo-3);
            int wvarPos1 = wvarCopia.IndexOf(wvarMoneda);

            wvarMonedaAux = wvarCopia.Substring(0, wvarPos1 + 2);
            int wvarResto = (largo - wvarPos1 - 2);

            return wvarMoneda + "   " + double.Parse(wvarMonto).ToString("#,0.00");
        }

        public static string FormaFecha(string fecha)
        {
            return string.Format("{0}{1}/{2}/{3}",
                20,
                fecha.Substring(0, 2),
                fecha.Substring(2, 2),
                fecha.Substring(4, 2));
        }

        public static void CargarDatosFijosSwift(string mensajeRaw, ResultadoBusquedaSwift swift)
        {
            string separador = "{2:";
            int indiceInteresa = mensajeRaw.IndexOf(separador);
            int anio;

            if (indiceInteresa >= 0)
            {
                anio = int.Parse(mensajeRaw.Substring(indiceInteresa + 11, 2));

                string siglo = "19";
                if (anio <= 79)
                {
                    siglo = "20";
                }

                swift.FechaEmi = string.Format("{0}/{1}/{2}{3}",
                   mensajeRaw.Substring(indiceInteresa + 15, 2),
                   mensajeRaw.Substring(indiceInteresa + 13, 2),
                   siglo,
                   anio.ToString());

                swift.HoraEmi = string.Format("{0}:{1}:00",
                    mensajeRaw.Substring(indiceInteresa + 7, 2),
                    mensajeRaw.Substring(indiceInteresa + 9, 2));

                swift.SesionEmi = mensajeRaw.Substring(indiceInteresa + 29, 4);
                swift.SecuenciaEmi = mensajeRaw.Substring(indiceInteresa + 33, 6);
            }
        }
    }
}
