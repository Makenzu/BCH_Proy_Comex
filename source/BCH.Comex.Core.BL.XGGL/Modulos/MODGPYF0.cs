using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGPYF0
    {

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
        }

        /// <summary>
        /// Copia el Cual% elemento de DeDonde$ delimitado por Delim$
        /// la forma del string es "----,----,-----,----"
        /// </summary>
        /// <param name="DeDonde"></param>
        /// <param name="Delim"></param>
        /// <param name="Cual"></param>
        /// <returns></returns>
        public static string copiardestring(string DeDonde, string Delim, int Cual)
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

        //Mueve el Cual% elemento de DeDonde$ delimitado por Delim$
        //la forma del string es "----,----,-----,----"
        public static string moverdestring(ref string DeDonde, string Delim, short Cual)
        {
            string _retValue = "";
            short Inicio = 1;
            short largo = (short)VB6Helpers.Len(DeDonde);
            short i = 0;
            short Fin = 0;
            short Mas = (short)VB6Helpers.Len(Delim);

            //primero buscamos el primer delimitador
            //primer elemento no tiene delimitador al inicio
            for (i = 1; i <= (short)(Cual - 1); i++)
            {
                Fin = (short)VB6Helpers.Instr(Inicio, DeDonde, Delim);
                if (Fin == 0)
                {
                    return _retValue;
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
                _retValue = VB6Helpers.Mid(DeDonde, Inicio, Fin - Inicio);
                //extraemos el pedazo de string dejando un separador
                DeDonde = VB6Helpers.Left(DeDonde, Inicio - 1) + VB6Helpers.Right(DeDonde, largo - Fin);
            }
            else
            {
                //ultimo elemento
                _retValue = VB6Helpers.Right(DeDonde, largo - Inicio + 1);
                //extraemos el ultimo pedazo de string sin dejar separador
                DeDonde = VB6Helpers.Left(DeDonde, Inicio - 2);
            }

            return _retValue;
        }

        public static string GetSceIni(string Key,string Value)
        {
            using(var tracer = new Tracer("GetSceIni"))
            {
                try
                {
                    return WebConfigurationManager.AppSettings[Key + "." + Value];
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public static string GetUbicacion(string Entry)
        {
            using(var tracer = new Tracer("GetUbicacion"))
            {
                try
                {
                    string algo = string.Empty;
                    algo = GetSceIni("Ubicacion", Entry);
                    if (algo.Length == 0){
                        return string.Empty;
                    }

                    if (!algo.EndsWith("\\")){
                        algo = algo + "\\";
                    }

                    string GetUbicacion = algo;
                    return GetUbicacion;
                }
                catch(Exception e)
                {
                    tracer.AddToContext("Excepcion", String.Format("Entry = {0} :: {1} ",Entry,e.Message));
                    return String.Empty;
                }
            }
        }

        public static string GetSceEntry(string Section)
        {
            using(var tracer = new Tracer("GetSceEntry"))
            {
                try
                {
                    var a = Section.Split('.');
                    return GetSceIni(a[0], a[1]);
                }
                catch(Exception e)
                {
                    tracer.AddToContext("Excepcion", e.Message);
                    return String.Empty;
                }
            }
        }

        // Determina Cuantos substring separados por Separa hay dentro de
        // EnDonde. El string tiene la forma "----,----,----"
        public static int cuentadestring(string EnDonde, string Separa)
        {
            return EnDonde.Split(Separa).Length;
        }

        // saca caracteres desde adelante o atras
        public static string TrimChar(string Que, string Cual, int Modo)
        {
            string TrimChar = "";

            int posi = 0;
            int i = 0;
            int paso = 0;
            int fin = 0;
            int initr = 0;
            int largo = 0;
            // largo del buscado
            largo = Cual.Len();
            if (largo > Que.Len() || Que == "")
            {
                return TrimChar;
            }

            // hacerlo desde la izquierda
            initr = 1;
            fin = Que.Len() - largo + 1;
            paso = largo;

            if (Modo != 0)
            {
                initr = fin;
                fin = 1;
                paso = -paso;
            }

            for (i = initr; i <= fin; i += paso)
            {
                if (Que.Mid(i, largo) != Cual)
                {
                    posi = i;
                    break;
                }
            }

            if (posi == 0)
            {
                return TrimChar;
            }

            if (Modo != 0)
            {
                // por la derecha
                TrimChar = Que.Mid(1, posi + largo - 1);
            }
            else
            {
                TrimChar = Que.Mid(posi);
            }

            return TrimChar;
        }

        public static string SyGet_OfiCod(DatosGlobales Globales,UnitOfWorkCext01 unit, string cencos)
        {
            string SyGet_OfiCod = "";

            string Nombre = "";
            try
            {
                var res = unit.SceRepository.sce_ccof_s01_MS(cencos);

                // Obtiene los datos del Reemplazo.-
                SyGet_OfiCod = res.oficon;
                Nombre = res.glsofi;


                return SyGet_OfiCod;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Title="Servidor de Impresión",
                    Text= "Se ha producido un error al tratar de leer la Oficina de Transferencia."
                });


            }
            return SyGet_OfiCod;
        }

        public static string Siu(DatosGlobales Globales)
        {
            T_MODGPYF0 MODGPYF0 = Globales.MODGPYF0;
            string Siu = "";

            string Fechora = "";
            double hoy = 0.0;
            const int ChrBase = 40;
            const int MaxVez = 59;

            do
            {
                hoy = DateTime.Now.ToDbl();
                Fechora = DateTime.Now.ToString("hh:mm:ss");

                if (Fechora != MODGPYF0.Siu_FechaHora)
                {
                    // SIU distinto, ok
                    MODGPYF0.Siu_FechaHora = Fechora;
                    MODGPYF0.Siu_Vez = 0;
                    Siu = Fechora + ChrBase.Char();

                    return Siu;
                }
                else if (MODGPYF0.Siu_Vez + 1 <= MaxVez)
                {
                    // SIU igual, nueva secuencia
                    MODGPYF0.Siu_Vez = MODGPYF0.Siu_Vez + 1;
                    Siu = Fechora + (ChrBase + MODGPYF0.Siu_Vez).Char();
                }     // secuencia agotada
            } while (true);     // repetir hasta encuetre uno

            return Siu;
        }
    }
}
