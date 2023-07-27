using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGCN.Modulos
{
    public static class MODGPYF0
    {
        /// <summary>
        /// Copia el Cual% elemento de DeDonde$ delimitado por Delim$
        /// la forma del string es "----,----,-----,----"
        /// </summary>
        /// <param name="DeDonde"></param>
        /// <param name="Delim"></param>
        /// <param name="Cual"></param>
        /// <returns></returns>
        public static string copiardestring(string DeDonde, string Delim, short Cual)
        {
            short Inicio = 1;
            short Mas = (short)VB6Helpers.Len(Delim);
            short i = 0;
            short Fin = 0;
            double largo = VB6Helpers.Len(DeDonde);

            //primero buscamos el primer delimitador
            //primer elemento no tiene delimitador al inicio

            for (i = 1; i <= (short)(Cual - 1); i++)
            {
                Fin = (short)VB6Helpers.Instr(Inicio, DeDonde, Delim);
                if (Fin == 0)
                {
                    return "";
                    //no existe elemento
                }
                Inicio = (short)(Fin + Mas);
            }

            //en inicio tengo el primer caracter del string
            //busquemos el final

            Fin = (short)VB6Helpers.Instr(Inicio, DeDonde, Delim);
            if (Fin != 0)
            {
                //tiene delim final
                return VB6Helpers.Mid(DeDonde, Inicio, Fin - Inicio);
            }
            else
            {
                //ultimo elemento
                return VB6Helpers.Right(DeDonde, (int)(largo - Inicio + 1));
            }

        }

        /// <summary>
        /// forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
        /// en "Donde".  Si no encuentra ninguna retorna "Donde"
        /// @estanislao: hace un Donde.Replace(Que, En)
        /// </summary>
        /// <param name="Donde"></param>
        /// <param name="Que"></param>
        /// <param name="En"></param>
        /// <returns></returns>
        public static string Componer(string Donde, string Que, string En)
        {
            if (!string.IsNullOrEmpty(Donde))
            {
                return Donde.Replace(Que, En);
            }
            else
            {
                return string.Empty;
            }

            //string Sale = Donde;
            //short Aqui = (short)VB6Helpers.Instr(1, Sale, Que);
            ////repetimos para todas las ocurrencias de Que
            //while (Aqui != 0)
            //{

            //    Sale = VB6Helpers.Left(Sale, Aqui - 1) + En + VB6Helpers.Mid(Sale, Aqui + VB6Helpers.Len(Que));
            //    Aqui = (short)VB6Helpers.Instr(1, Sale, Que);
            //}
            //return Sale;
        }

        // Mueve el Cual% elemento de DeDonde$ delimitado por Delim$
        // la forma del string es "----,----,-----,----"
        public static string moverdestring(ref string DeDonde, string Delim, int Cual)
        {
            string moverdestring = "";

            int fin = 0;
            int i = 0;
            int Mas = 0;
            int largo = 0;
            int Inicio = 0;

            Inicio = 1;
            largo = DeDonde.Len();
            Mas = Delim.Len();

            // primero buscamos el primer delimitador
            // primer elemento no tiene delimitador al inicio
            for (i = 1; i <= Cual - 1; i += 1)
            {
                fin = DeDonde.InStr(Delim, Inicio, StringComparison.CurrentCulture);
                if (fin == 0)
                {
                    return moverdestring;     // no existe elemento
                }     // no existe elemento
                Inicio = fin + Mas;
            }

            // en inicio tengo el primer caracter del string
            // busquemos el final

            fin = DeDonde.InStr(Delim, Inicio, StringComparison.CurrentCulture);
            if (fin != 0)
            {
                // tiene delim final
                moverdestring = DeDonde.Mid(Inicio, fin - Inicio);
                // extraemos el pedazo de string dejando un separador
                DeDonde = DeDonde.Left((Inicio - 1)) + DeDonde.Right((largo - fin));
            }
            else
            {
                // ultimo elemento
                moverdestring = DeDonde.Right((largo - Inicio + 1));
                // extraemos el ultimo pedazo de string sin dejar separador
                DeDonde = DeDonde.Left((Inicio - 2));
            }

            return moverdestring;
        }
     
    }
}
