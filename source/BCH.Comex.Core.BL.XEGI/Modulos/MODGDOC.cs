using System;
using System.Runtime.InteropServices;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODGDOC
    {
        [DllImport("Kernel")]
        public static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, object lpString, string lpFileName);
        [DllImport("Kernel")]
        public static extern int GetPrivateProfileString(string lpApplicationName, object lpKeyName, string lpDefault, string lpReturnedString, int nSize, string lpFileName);
        [DllImport("User")]
        public static extern void MessageBeep(int wType);
        // forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
        // en "Donde".  Si no encuentra ninguna retorna "Donde"
        public static string Componer(string Donde, string Que, string En)
        {
            string Componer = "";

            int Aqui = 0;
            string Sale = "";

            Sale = Donde;
            // repetimos para todas las ocurrencias de Que
            Aqui = Sale.InStr(Que, 1, StringComparison.CurrentCulture);
            while (Aqui != 0)
            {
                Sale = Sale.Left((Aqui - 1)) + En + Sale.Mid((Aqui + Que.Len()));
                Aqui = Sale.InStr(Que, 1, StringComparison.CurrentCulture);
            }
            Componer = Sale;

            return Componer;
        }
        // Copia el Cual% elemento de DeDonde$ delimitado por Delim$
        // la forma del string es "----,----,-----,----"
        public static string CopiarDeString(string DeDonde, string Delim, int Cual)
        {
            try
            {
                var strings = DeDonde.Split(new string[] { Delim }, StringSplitOptions.None);
                return strings[Cual - 1];
            }
            catch
            {
                return String.Empty;
            }
            //string CopiarDeString = "";

            //int fin = 0;
            //int i = 0;
            //int mas = 0;
            //int largo = 0;
            //int Inicio = 0;

            //Inicio = 1;
            //largo = DeDonde.Len();
            //mas = Delim.Len();

            //// primero buscamos el primer delimitador
            //// primer elemento no tiene delimitador al inicio

            //for(i = 1; i <= Cual; i += 1)
            //{
            //   fin = DeDonde.InStr(Delim,Inicio,StringComparison.CurrentCulture);
            //   if (fin == 0)
            //   {
            //      return CopiarDeString;     // no existe elemento
            //   }     // no existe elemento
            //   Inicio = fin + mas;
            //}

            //// en inicio tengo el primer caracter del string
            //// busquemos el final

            //fin = DeDonde.InStr(Delim,Inicio,StringComparison.CurrentCulture);
            //if (fin != 0)
            //{
            //        // tiene delim final
            //   CopiarDeString = DeDonde.Mid(Inicio, fin - Inicio);
            //}
            //else
            //{
            //        // ultimo elemento
            //   CopiarDeString = DeDonde.Right((largo - Inicio + 1));
            //}

            //return CopiarDeString;
        }
        // Determina Cuantos substring separados por Separa hay dentro de
        // EnDonde. El string tiene la forma "----,----,----"
        public static int CuentaDeString(string EnDonde, string Separa)
        {
            int CuentaDeString = 0;

            int fin = 0;
            int Total = 0;
            int Inicio = 0;
            int largo = 0;

            largo = Separa.Len();
            Inicio = 1;
            Total = 0;
            while (Inicio != 0)
            {
                fin = EnDonde.InStr(Separa, Inicio, StringComparison.CurrentCulture);
                if (fin != 0)
                {
                    // hay otro separador
                    // If fin% >= Inicio% + largo% Then Total% = Total% + 1
                    Total = Total + 1;
                    Inicio = fin + largo;
                }
                else
                {
                    // no hay otro
                    if (Inicio <= EnDonde.Len())
                    {
                        Total = Total + 1;
                    }
                    break;
                }
            }
            CuentaDeString = Total;

            return CuentaDeString;
        }
        // recibe un string en formato Val() o un dato numerico y lo devuelve en
        // formato despliege windows rellenando con espacios al principio a modo
        // de dejar alineado.
        // Atencion:  Solo funciona si el font con que se despliega es Bold=false

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
            ElNum = Numero.ToStr();
            // convertir al formato windows
            if (((int)MigrationSupport.Utils.VarType(ElNum)) == V_STRING)
            {
                if (ElNum.ToVal() < 0)
                {
                    // ElNum = unformat(Format$(Abs(Val(ElNum))))
                    EsMenor = true;
                }
                dd = ElNum.InStr(".", 1, StringComparison.CurrentCulture);
                if (dd != 0)
                {
                    ente = ElNum.Left((dd - 1));
                    Deci = "0." + ElNum.Right((ElNum.Len() - dd));
                }
                else
                {
                    ente = ElNum.Left((ElNum.Len()));
                    Deci = "0";
                }
                //tt = (Math.Abs(ente.ToVal()) + Deci.ToVal()).ToDec();
                if (Numero != "")
                {
                    tt = Math.Round(Convert.ToDecimal(Numero.ToString().Replace(".", ",")), 2);
                }

                if (EsMenor)
                {
                    tt = tt * -1;
                }

                if (Mascara == "#,###,###,###,##0" && Deci != string.Empty)
                {
                    if(Deci.Contains("."))
                        tt = decimal.Parse(ente) - decimal.Parse(Deci.Replace(".",","));
                }

                temp = tt.ToString(Mascara);
            }
            else
            {
                temp = MigrationSupport.Utils.Format(ElNum, Mascara);
            }

            lt = temp.Len();
            lm = Mascara.Len();

            if (lt > lm)
            {
                forma = temp;
                return forma;     // listo
                                  // Else
                                  //     forma = Format$(Temp$, String$(lm%, "@"))
            }

            // debemos rellenar con espacios los caracteres a la izquierda
            // contemos caracteres y separadores
            for (i = 1; i <= lm - lt; i += 1)
            {
                if (Mascara.Mid(i, 1) == "," || Mascara.Mid(i, 1) == ".")
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
        public static string GetSceIni(string Section, string Key)
        {
            return Mdl_Acceso.GetConfigValue(Mdl_Acceso.MODULO + "." + Section + "." + Key);
            /*string GetSceIni = "";

            string retVal = "";
            int worked = 0;
            retVal = new string(0.ToChar(),255);
            worked = GetPrivateProfileString(Section,Key,"",retVal,retVal.Len(),"Sce.ini");
            if (worked == 0)
            {
               GetSceIni = "";
            }
            else
            {
               GetSceIni = retVal.Left(worked);
            }
            return GetSceIni;*/
        }

        // Deja un string de varias palabras separadas por un blanco como la primera
        // letra en Mayúscula y el resto de la palabra en minúscula.-
        public static string Minuscula(string Pdato)
        {
            string Minuscula = "";

            string strCaseArg = "";
            string s = "";
            int k = 0;
            int m = 0;
            int i = 0;
            string[] Palabras = null;

            Palabras = new string[1];
            string Dato = "";

            Dato = Pdato;
            i = 1;
            m = CuentaDeString(Dato, " ");
            Palabras = new string[m + 1];
            for (k = 1; k <= m; k += 1)
            {
                // Verifica si la palabra siguiente contiene la cantidad de caracteres necesarios.
                s = CopiarDeString(Dato, " ", k);
                Palabras[k] = s;
                strCaseArg = s.UCase().TrimB();
                if (strCaseArg == "S.A." || strCaseArg == "S.A.C." || strCaseArg == "M/E" || strCaseArg == "M/N" || strCaseArg == "N.A.")
                {
                    Palabras[k] = s.UCase();
                }
                else if (strCaseArg == "A" || strCaseArg == "DE" || strCaseArg == "Y" || strCaseArg == "O" || strCaseArg == "Y/O" || strCaseArg == "U" || strCaseArg == "AL" || strCaseArg == "DE" || strCaseArg == "LO" || strCaseArg == "LA" || strCaseArg == "EL" ||
                   strCaseArg == "SI" || strCaseArg == "NO" || strCaseArg == "E" || strCaseArg == "POR" || strCaseArg == "OF")
                {
                    Palabras[k] = s.LCase();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Palabras[k]))
                    {
                        Palabras[k] = s.Mid(1, 1).UCase() + s.Mid(2, s.Len() - 1).LCase();
                    }
                }
            }

            s = "";
            for (i = 1; i <= Palabras.GetUpperBound(0); i += 1)
            {
                if (!string.IsNullOrEmpty(Palabras[i]))
                {
                    s = s + Palabras[i] + " ";
                }
            }
            Minuscula = s.TrimB();

            return Minuscula;
        }
        // Mueve el Cual% elemento de DeDonde$ delimitado por Delim$
        // la forma del string es "----,----,-----,----"
        public static string moverdestring(ref string DeDonde, string Delim, int Cual)
        {
            string moverdestring = "";

            int fin = 0;
            int i = 0;
            int mas = 0;
            int largo = 0;
            int Inicio = 0;

            Inicio = 1;
            largo = DeDonde.Len();
            mas = Delim.Len();

            // primero buscamos el primer delimitador
            // primer elemento no tiene delimitador al inicio
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
        // funcion de sonidos para MsgBox (c) WaldoSoft
        private static int Pito_OldValue = 0;
        public static int Pito(int Como)
        {
            int Pito = 0;

            string Valor = "";

            // OldValue tiene el valor 0 cuando no ha sido leida la configuracion,
            // OldValue = -1 si es falso
            // OldValue = -2 si es true

            if (Pito_OldValue == 0 || Como == -3)
            {
                if (GetSceIni("Configuracion", "Sonidos").ToBool())
                {
                    Pito_OldValue = ((true) ? -1 : 0) - 1;
                }
                else
                {
                    Pito_OldValue = ((false) ? -1 : 0) - 1;
                }
            }

            switch (Como)
            {
                case -3:
                    Pito = Pito_OldValue + 1;
                    break;
                case -2:
                case -1:
                    if (Como != Pito_OldValue)
                    {
                        Valor = "0";
                        if (Como + 1 != 0)
                        {
                            Valor = "1";
                        }
                        if (!WriteSceIni("Configuracion", "Sonidos", Valor).ToBool())
                        {
                            Pito_OldValue = Como;
                        }
                    }
                    Pito = Pito_OldValue + 1;
                    break;
                default:
                    if (Pito_OldValue + 1 != 0)
                    {
                        MessageBeep(Como);
                    }
                    Pito = Como;
                    break;
            }

            return Pito;
        }
        public static int WriteSceIni(string Section, string Key, string Value)
        {
            int WriteSceIni = 0;

            int worked = 0;
            if (!string.IsNullOrEmpty(Value))
            {
                worked = WritePrivateProfileString(Section, Key, Value, "Sce.ini");
            }
            else
            {
                worked = WritePrivateProfileString(Section, Key, 0, "Sce.ini");
            }
            if (worked == 0)
            {
                WriteSceIni = true.ToInt();
            }

            return WriteSceIni;
        }
    }
}
