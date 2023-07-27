using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using CodeArchitects.VB6Library;
using System;
using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class UTILES
    {
        public static T_UTILES GetUTILES()
        {
            return new T_UTILES();
        }

        public static SeteosWin Win;

        public static string GetUbicacion(string Entry)
        {
            string GetUbicacion = "";

            string algo = "";

            //algo = GetSceIni("Ubicacion", Entry);
            //if (algo.Length == 0)
            //{
            //    return GetUbicacion;
            //}
            //if (algo.Substring(algo.Length, 1) != "\\")
            //{
            //    algo = algo + "\\";
            //}
            GetUbicacion = algo;

            return GetUbicacion;
        }
        public static string RecortaTexto(UI_Combo Lista, string Texto, int Maximo)
        {
            string RecortaTexto = "";

            int i = 0;
            string Aux = "";
            int LarMax = Maximo;// 0;      
            // if ((Texto != null) || Texto != "")
            if (Texto != null)
                Aux = Texto.Trim();
            else
                Aux = "";
            for (i = Aux.Length; i >= 1; i += -1)
            {
                if (Aux.Substring(0, i).Length <= LarMax)
                {
                    RecortaTexto = Aux.Substring(0, i);
                    return RecortaTexto;
                }
            }
            // si salgo por aqui es que especificaron maximo = 0 ==> string nulo
            return RecortaTexto;
        }
        public static string Componer(string Donde, string Que, string En)
        {
            if (!string.IsNullOrEmpty(Donde))
                return Donde.Replace(Que, En);
            else
                return string.Empty;
        }
        public static string unformat(string Numero)
        {
            string unformat = "";
            string _retValue = "";
            int Donde = 0;
            string Temp = "";


            if (String.IsNullOrEmpty(T_UTILES.SeteosWin.SepDecimal) || String.IsNullOrEmpty(T_UTILES.SeteosWin.SepCientos))
            {
                // leer parametros
                //  GetSeteoWin();     // obtiene seteo win
            }

            // temporal sacando espacios finales e iniciales
            Temp = VB6Helpers.LTrim(VB6Helpers.RTrim(Numero));

            // primero sacamos los separadores de cientos                       
            Donde = (short)VB6Helpers.Instr(1, Temp, T_UTILES.SeteosWin.SepCientos);
            while (Donde != 0)
            {
                Temp = VB6Helpers.Left(Temp, Donde - 1) + VB6Helpers.Right(Temp, VB6Helpers.Len(Temp) - Donde);
                Donde = (short)VB6Helpers.Instr(1, Temp, T_UTILES.SeteosWin.SepCientos);
            }

            // ok, ya sacamos los separadores de cientos, cambiemos el separador decimal           
            Donde = (short)VB6Helpers.Instr(1, Temp, T_UTILES.SeteosWin.SepDecimal);
            _retValue = Temp;
            if (Donde != 0)
            {
                // unformat = temp.Left((Donde - 1)) + "." + temp.Right((temp.Len() - Donde));
                return VB6Helpers.Left(Temp, Donde - 1) + "." + VB6Helpers.Right(Temp, VB6Helpers.Len(Temp) - Donde);
            }

            return unformat;
        }

        public static string GetConfigValue(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        public static void GetSeteoWin()
        {
            short i = 0;
            //separador decimal
            Win.SepDecimal = GetWinIni("Intl", "sDecimal", "win.ini");
            if (Win.SepDecimal == "")
            {
                Win.SepDecimal = ".";  //forma americano
            }

            //separador de cientos
            Win.SepCientos = GetWinIni("Intl", "sThousand", "win.ini");
            if (Win.SepCientos == "")
            {
                Win.SepCientos = ",";  //forma americana
            }

            //separador fechas
            Win.SepFecha = GetWinIni("Intl", "sDate", "win.ini");
            if (Win.SepFecha == "")
            {
                Win.SepFecha = "/";  //forma americano
            }

            //formato corto de fecha
            Win.MinDate = VB6Helpers.UCase(GetWinIni("Intl", "sShortDate", "win.ini"));
            if (VB6Helpers.Len(Win.MinDate) <= 8)
            {
                Win.MinDate = "MM/DD/YYYY";  //forma americana
            }

            //posicion de fechas
            for (i = 1; i <= 3; i++)
            {
                string _switchVar1 = VB6Helpers.Left(copiardestring(Win.MinDate, Win.SepFecha, i), 1);
                if (_switchVar1 == "D")
                {
                    Win.PosDia = i;
                }
                else if (_switchVar1 == "M")
                {
                    Win.PosMes = i;
                }
                else if (_switchVar1 == "Y")
                {
                    Win.PosAno = i;
                }

            }

        }

        public static string GetWinIni(string Section, string Key, string Archivo)
        {
            return GetConfigValue(String.Format("AdminParticipantes.{0}.{1}.{2}", Archivo, Section, Key));
        }
        public static string copiardestring(string DeDonde, string Delim, short Cual)
        {
            short Inicio = 1;
            short Mas = (short)VB6Helpers.Len(Delim);
            short i = 1;
            short Fin = 0;
            double largo = VB6Helpers.Len(DeDonde);
        
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

        public static short seteaTabulador(dynamic Objeto, int[] Tabla)
        {
            short Fin = 0;
            short Inicio = 0;
            short Total = 0;
            //verifiquemos que el objeto es una lista o combo
            if (Objeto is UI_Combo)
            {
                //es lista
            }
            else
            {
                //no es
                return 0;  //no coresponde
            }

            //verificamos el tamaño de la tabla
            Fin = -1;

            Fin = (short)VB6Helpers.UBound(Tabla);  //limite superior
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (Fin < 0)
            {
                //no dimensionada
                return 0;  //no corresponde
            }

            Inicio = (short)VB6Helpers.LBound(Tabla);  //limite inferior
            Total = (short)(Fin - Inicio + 1);  //num elementos

            return -1;
        }
        public static string forma(object Numero, string Mascara)
        {
            string forma = "";

            int Cta = 0;
            int i = 0;
            int lm = 0;
            int lt = 0;
            string temp = "";
            string Deci = "";
            string ente = "";
            int dd = 0;
            bool EsMenor = false;
            const int V_STRING = 8;
            decimal tt = 0.00M;
            string ElNum = "";

            EsMenor = false;
            ElNum = Numero.ToString();
            // convertir al formato windows
            if (((int)VB6Helpers.VarType(ElNum)) == V_STRING)
            {
                if (Convert.ToDecimal(ElNum) < 0)
                {
                    // ElNum = unformat(Format$(Abs(Val(ElNum))))
                    EsMenor = true;
                }
                dd = VB6Helpers.Instr(ElNum, "."); //ElNum.InStr(".", 1, StringComparison.CurrentCulture);
                if (dd != 0)
                {
                    ente = VB6Helpers.Left(ElNum, (dd - 1));   //  ElNum.Left((dd - 1));
                    //Deci = "0." + ElNum.Right((ElNum.Len() - dd));
                    Deci = "0." + VB6Helpers.Right(ElNum, (ElNum.Length - 1));
                }
                else
                {
                    ente = VB6Helpers.Left(ElNum, ElNum.Length); //ElNum.Left((ElNum.Len()));
                    Deci = "0";
                }
                tt = (Math.Abs(Convert.ToDecimal(ente) + Convert.ToDecimal(Deci)));
                if (EsMenor)
                {
                    tt = tt * -1;
                }
                temp = VB6Helpers.Format(tt, Mascara);
            }
            else
            {
                temp = VB6Helpers.Format(ElNum, Mascara);
            }

            lt = temp.Length;
            lm = Mascara.Length;

            if (lt > lm)
            {
                forma = temp;
                return forma;
            }

            // debemos rellenar con espacios los caracteres a la izquierda
            // contemos caracteres y separadores
            for (i = 1; i <= lm - lt; i += 1)
            {
                //if (Mascara.Mid(i, 1) == "," || Mascara.Mid(i, 1) == ".")
                if (VB6Helpers.Mid(Mascara, 1) == "" || VB6Helpers.Mid(Mascara, 1) == ".") //   (Mascara.Mid(i, 1) == "," || Mascara.Mid(i, 1) == ".")
                {
                    Cta = Cta + 1;
                }
                else
                {
                    Cta = Cta + 2;
                }
            }

            forma = new string(' ', Cta) + temp;

            // 

            return forma;
        }

        //public static string EspaciosEnBlanco(string Numero, int largo)
        //{
        //    string DatoFinal = string.Empty;
        //    if (string.IsNullOrEmpty(Numero))
        //        DatoFinal = string.Empty;
        //    else
        //        DatoFinal = Numero.PadRight(largo, ' ').Replace(" ", "&nbsp;");

        //    return DatoFinal;
        //}

        //public static string EspaciosEnBlancoRight(string Valor, int Largo)
        //{
        //    string DatoFinal = string.Empty;
        //    if (string.IsNullOrEmpty(Valor))
        //        DatoFinal = string.Empty;
        //    else
        //        DatoFinal = Valor.PadRight(Largo, ' ').Replace(" ", "\xA0");

        //    return DatoFinal;
        //}

        public static string EspaciosAlineado(string Valor, int LargoFijo)
        {
            string DatoFinal = string.Empty;
            string LargoValor = string.Empty;
            int diferencia = 0;
            if (string.IsNullOrEmpty(Valor.Trim()))
            {
                DatoFinal = Valor.TrimR().PadLeft(LargoFijo, ' ').Replace(" ", "\xA0");//string.Empty;
            }
            else
            {
                diferencia = (LargoFijo - Valor.TrimR().Length);
                if (diferencia > 0)
                    DatoFinal = Valor.TrimR().PadRight(diferencia, ' ').Replace(" ", "\xA0");
                else //if (diferencia == 0)
                    DatoFinal = Valor.TrimR().PadLeft(LargoFijo, ' ').Replace(" ", "\xA0");

            }
            return DatoFinal;
        }

        public static string EspaciosAlineadoMonoSpace(string Valor, int LargoFijo)
        {
            string DatoFinal = string.Empty;
            string LargoValor = string.Empty;
            int diferencia = 0;
            if (string.IsNullOrEmpty(Valor.Trim()))
            {
                DatoFinal = Valor.TrimR().PadLeft(LargoFijo, ' ').Replace(" ", "\xA0");//string.Empty;
            }
            else
            {
                diferencia = (LargoFijo - Valor.TrimR().Length);
                if (diferencia > 0)
                    DatoFinal = Valor.TrimR().PadRight(LargoFijo, ' ').Replace(" ", "\xA0");
                else //if (diferencia == 0)
                    DatoFinal = Valor.TrimR().PadLeft(LargoFijo, ' ').Replace(" ", "\xA0");

            }
            return DatoFinal;
        }


        //public static string EspaciosEnBlancoLeft(string valor, int largo)
        //{
        //    string DatoFinal = string.Empty;
        //    if (string.IsNullOrEmpty(valor))
        //        DatoFinal = string.Empty;
        //    else
        //        DatoFinal = valor.PadLeft(largo, ' ').Replace(" ", "\xA0");

        //    return DatoFinal;
        //}
        public static string QuitaEspaciosEnBlanco(string valor)
        {
            string DatoFinal = string.Empty;

            if (string.IsNullOrEmpty(valor))
                DatoFinal = string.Empty;
            else
                DatoFinal = valor.Replace("\xA0", "");//.Replace("&nbsp;", "");

            return DatoFinal;


        }
        public static string moverdestring(ref string DeDonde, string Delim, int Cual)
        {
            string moverdestring = string.Empty;
            int fin = 0;
            int i = 0;
            int mas = 0;
            int Largo = 0;
            int Inicio = 0;

            Inicio = 1;
            Largo = DeDonde.Len();
            mas = Delim.Len();

            for (i = 1; i <= Cual - 1; i += 1)
            {
                fin = DeDonde.InStr(Delim, Inicio, StringComparison.CurrentCulture);
                if (fin == 0)
                {
                    return moverdestring;     // no existe elemento
                }     // no existe elemento
                Inicio = fin + mas;
            }

            // en inicio tengo el primer caracter del string
            // busquemos el final

            fin = DeDonde.InStr(Delim, Inicio, StringComparison.CurrentCulture);
            if (fin != 0)
            {
                // tiene delim final
                moverdestring = DeDonde.Mid(Inicio, fin - Inicio);
                // extraemos el pedazo de string dejando un separador
                DeDonde = DeDonde.Left((Inicio - 1)) + DeDonde.Right((Largo - fin));
            }
            else
            {
                // ultimo elemento
                moverdestring = DeDonde.Right((Largo - Inicio + 1));
                // extraemos el ultimo pedazo de string sin dejar separador
                DeDonde = DeDonde.Left((Inicio - 2));
            }

            return moverdestring;
        }

        public static string ValidaRut(string rut)
        {
            string DevuelveRutFormateado = "0"; //string.Empty;
            int EsRut = 0;
            DevuelveRutFormateado = PRTYENT.descero(rut);
            if (!string.IsNullOrEmpty(DevuelveRutFormateado))
            {
                EsRut = PRTYENT.esrut(DevuelveRutFormateado);
                if (EsRut != 0)
                    return DevuelveRutFormateado;
                else
                    return "0"; //No es valido
            }
            return DevuelveRutFormateado;
        }


    }
}
