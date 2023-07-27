using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;
using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    
    public class MODGPYF0
    {
        public static SeteosWin Win;

        public static T_MODGPYF0 GetMODGPYF0()
        {
            return new T_MODGPYF0();
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


        public static short Mascara(short tecla, dynamic Obj)
        {
            short posi = 0;
            short Pre = 0;
            short cosa = 0;
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Obj'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble(VB6Helpers.Invoke(VB6Helpers.CObj(Obj), "SelStart")) == 0 && (Format.StringToDouble(VB6Helpers.Invoke(VB6Helpers.CObj(Obj), "SelLength")) == VB6Helpers.Len(VB6Helpers.CStr(Obj.Text))))
            {
                Obj.Text = "";
            }

            if (Win.SepDecimal == "")
            {
                //leer parametros
                GetSeteoWin(); //obtiene seteo win
            }

            //determino cuanto caracteres debo aceptar
            posi = (short)VB6Helpers.Instr(1, VB6Helpers.CStr(Obj.Tag), ".");
            if (posi != 0)
            {
                Pre = (short)VB6Helpers.Len(VB6Helpers.Left(VB6Helpers.CStr(Obj.Tag), posi - 1));
                posi = (short)(VB6Helpers.Len(VB6Helpers.CStr(Obj.Tag)) - posi);
            }
            else
            {
                Pre = (short)VB6Helpers.Len(VB6Helpers.CStr(Obj.Tag));
            }

            if (tecla == 13 || tecla == 8)
            {
                //return, backspace
            }
            else if (tecla == 46 || tecla == 44)
            {
                //punto o coma
                if (posi != 0)
                {
                    if (VB6Helpers.Instr(1, VB6Helpers.CStr(Obj.Text), Win.SepDecimal) != 0)
                    {
                        VB6Helpers.Beep();
                    }
                    else
                    {
                        return (short)VB6Helpers.Asc(Win.SepDecimal);
                    }

                    return 0;
                }
                else
                {
                    VB6Helpers.Beep();
                    return 0;
                }

            }
            else if (tecla >= 48 && tecla <= 57)
            {
                cosa = (short)VB6Helpers.Instr(1, VB6Helpers.CStr(Obj.Text), Win.SepDecimal);
                if (cosa != 0)
                {
                    //tiene sep decimal
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Obj'. Consider using the GetDefaultMember6 helper method.
                    if (cosa > Format.StringToDouble(VB6Helpers.Invoke(VB6Helpers.CObj(Obj), "SelStart")))
                    {
                        //en parte entera
                        if (cosa >= Pre + 1)
                        {
                            VB6Helpers.Beep();
                            return 0;
                        }

                    }
                    else
                    {
                        if (VB6Helpers.Len(VB6Helpers.CStr(Obj.Text)) - cosa >= posi)
                        {
                            VB6Helpers.Beep();
                            return 0;
                        }

                    }

                }
                else
                {
                    if (VB6Helpers.Len(VB6Helpers.CStr(Obj.Text)) >= Pre)
                    {
                        VB6Helpers.Beep();
                        return 0;
                    }

                }

            }
            else
            {
                VB6Helpers.Beep();
                return 0;
            }

            return tecla;
        }

        public static string SyGet_OfiCod(UnitOfWorkCext01 unit, string cencos)
        {
            string _retValue = "";
            try
            {
                // IGNORED: On Error GoTo SyGet_OfiCodErr
                _retValue = unit.SceRepository.EjecutarSP<sce_ccof_s01_MS_Result>("sce_ccof_s01_MS", cencos).First().oficon;
                //VB6Helpers.MsgBox("Se ha producido un error al tratar de leer la Oficina de Transferencia. El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Servidor de Impresión");
            }
            catch (Exception _ex)
            {
                _retValue = "-1";
            }
            return _retValue;
        }

        public static short SetConfigValue(string Key, string Value) {
            WebConfigurationManager.AppSettings[Key] = Value;
            return -1;
        }
        
        public static string GetUbicacion(string Entry) {
            string algo = Mdl_Acceso.GetConfigValue(String.Format("FundTransfer.Ubicacion.{0}", Entry));
            if (algo.Length == 0)
            {
                return "";
            }
            if (!algo.Last().Equals('\\'))
            {
                algo = algo + @"\";
            }
            return algo;
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

        
        //recibe un string en formato Val() o un dato numerico y lo devuelve en
        //formato despliege windows rellenando con espacios al principio a modo
        //de dejar alineado.
        //Atencion:  Solo funciona si el font con que se despliega es Bold=false
        //
        public static string forma(dynamic Numero, string Mascara)
        {
            string _retValue = "";
            // UPGRADE_INFO (#0561): The 'V_STRING' symbol was defined without an explicit "As" clause.
            const short V_STRING = 8;
            decimal tt = 0;
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Numero'. Consider using the GetDefaultMember6 helper method.
            dynamic ElNum = Numero;
            short dd = 0;
            string ente = "";
            string Deci = "";
            string Temp = "";
            short Lt = 0;
            short lm = 0;
            short EsMenor = (short)(false ? -1 : 0);
            short i = 0;
            short cta = 0;
            //convertir al formato windows
            if ((int)VB6Helpers.VarType(ElNum) == V_STRING)
            {
                if (VB6Helpers.Val(ElNum) < 0)
                {

                    EsMenor = (short)(true ? -1 : 0);
                }

                dd = (short)VB6Helpers.Instr(1, VB6Helpers.CStr(ElNum), ".");
                if (dd != 0)
                {
                    ente = VB6Helpers.Left(VB6Helpers.CStr(ElNum), dd - 1);
                    Deci = "0." + VB6Helpers.Right(VB6Helpers.CStr(ElNum), VB6Helpers.Len(VB6Helpers.CStr(ElNum)) - dd);
                }
                else
                {
                    ente = VB6Helpers.Left(VB6Helpers.CStr(ElNum), VB6Helpers.Len(VB6Helpers.CStr(ElNum)));
                    Deci = "0";
                }

                tt = (decimal)(VB6Helpers.Abs(VB6Helpers.Val(ente)) + VB6Helpers.Val(Deci));
                if (EsMenor != 0)
                {
                    tt = tt * -1;
                }

                Temp = VB6Helpers.Format(VB6Helpers.CStr(tt), Mascara);
            }
            else //si es nro
            {
                double? aux = ElNum as double?;
                if (aux.HasValue)
                    Temp = aux.Value.ToString(Mascara);
            }

            Lt = (short)VB6Helpers.Len(Temp);
            lm = (short)VB6Helpers.Len(Mascara);

            if (Lt > lm)
            {
                return Temp;
                //listo
            }
            else
            {
                _retValue = VB6Helpers.Format(Temp, VB6Helpers.String(lm, "@"));
            }

            //debemos rellenar con espacios los caracteres a la izquierda
            //contemos caracteres y separadores
            for (i = 1; i <= (short)(lm - Lt); i++)
            {
                if (VB6Helpers.Mid(Mascara, i, 1) == "," || VB6Helpers.Mid(Mascara, i, 1) == ".")
                {
                    cta = (short)(cta + 1);
                }
                else
                {
                    cta = (short)(cta + 2);
                }

            }

            return VB6Helpers.Space(cta) + Temp;
        }


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

        //reformatea un string numerico en formato despliege Windows al formato Val()
        public static string unformat(T_MODGPYF0 MODGPYF0, string Numero)
        {
            string _retValue = "";
            string Temp = "";
            short Donde = 0;
            if (String.IsNullOrEmpty(MODGPYF0.Win.SepDecimal) || String.IsNullOrEmpty(MODGPYF0.Win.SepCientos))
            {
                //leer parametros
                GetSeteoWin(MODGPYF0); //obtiene seteo win
            }

            //temporal sacando espacios finales e iniciales
            Temp = VB6Helpers.LTrim(VB6Helpers.RTrim(Numero));

            //primero sacamos los separadores de cientos

            Donde = (short)VB6Helpers.Instr(1, Temp, MODGPYF0.Win.SepCientos);
            while (Donde != 0)
            {

                Temp = VB6Helpers.Left(Temp, Donde - 1) + VB6Helpers.Right(Temp, VB6Helpers.Len(Temp) - Donde);
                Donde = (short)VB6Helpers.Instr(1, Temp, MODGPYF0.Win.SepCientos);
            }

            //ok, ya sacamos los separadores de cientos, cambiemos el separador decimal
            Donde = (short)VB6Helpers.Instr(1, Temp, MODGPYF0.Win.SepDecimal);
            _retValue = Temp;
            if (Donde != 0)
            {
                return VB6Helpers.Left(Temp, Donde - 1) + "." + VB6Helpers.Right(Temp, VB6Helpers.Len(Temp) - Donde);
            }

            return _retValue;
        }

        public static void GetSeteoWin(T_MODGPYF0 MODGPYF0)
        {
            //se cambia el formato por defecto al chileno en vez del americano

            short i = 0;
            //separador decimal
            MODGPYF0.Win.SepDecimal = GetWinIni("Intl", "sDecimal", "win.ini");
            if (String.IsNullOrEmpty(MODGPYF0.Win.SepDecimal))
            {
                MODGPYF0.Win.SepDecimal = ",";  //forma chilena
            }

            //separador de cientos
            MODGPYF0.Win.SepCientos = GetWinIni("Intl", "sThousand", "win.ini");
            if (String.IsNullOrEmpty(MODGPYF0.Win.SepCientos))
            {
                MODGPYF0.Win.SepCientos = ".";  //forma chilena
            }

            //separador fechas
            MODGPYF0.Win.SepFecha = GetWinIni("Intl", "sDate", "win.ini");
            if (String.IsNullOrEmpty(MODGPYF0.Win.SepFecha))
            {
                MODGPYF0.Win.SepFecha = "/";  //forma americano
            }

            //formato corto de fecha
            MODGPYF0.Win.MinDate = VB6Helpers.UCase(GetWinIni("Intl", "sShortDate", "win.ini"));
            if (VB6Helpers.Len(MODGPYF0.Win.MinDate) <= 8)
            {
                MODGPYF0.Win.MinDate = "DD/MM/YYYY";  //forma chilena
            }

            //posicion de fechas
            for (i = 1; i <= 3; i++)
            {
                string _switchVar1 = VB6Helpers.Left(copiardestring(MODGPYF0.Win.MinDate, MODGPYF0.Win.SepFecha, i), 1);
                if (_switchVar1 == "D")
                {
                    MODGPYF0.Win.PosDia = i;
                }
                else if (_switchVar1 == "M")
                {
                    MODGPYF0.Win.PosMes = i;
                }
                else if (_switchVar1 == "Y")
                {
                    MODGPYF0.Win.PosAno = i;
                }

            }

        }

        //obtiene un valor del Win.ini
        public static string GetWinIni(string Section, string Key, string Archivo)
        {
            return Mdl_Acceso.GetConfigValue(String.Format("FundTransfer.{0}.{1}.{2}", Archivo, Section, Key));
        }

        public static short seteatabulador(dynamic Objeto, short[] Tabla)
        {
            short Fin = 0;
            short Inicio = 0;
            short Total = 0;
            //verifiquemos que el objeto es una lista o combo
            if (Objeto is VB6ListBox)
            {
                //es lista
            }
            else
            {
                //no es
                //seteatabulador = 0;
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

        public static string TrimChar(string Que, string Cual, short Modo)
        {
            short largo = (short)VB6Helpers.Len(Cual);
            short initr = 0;
            short Fin = 0;
            short Paso = 0;
            short i = 0;
            short posi = 0;
            //largo del buscado
            if (largo > VB6Helpers.Len(Que) || Que == "")
            {
                return "";
            }

            //hacerlo desde la izquierda
            initr = 1;
            Fin = (short)(VB6Helpers.Len(Que) - largo + 1);
            Paso = largo;

            if (Modo != 0)
            {
                initr = Fin;
                Fin = 1;
                Paso = (short)(-Paso);
            }

            for (i = (short)initr; i <= (short)Fin; i += (short)Paso)
            {
                if (VB6Helpers.Mid(Que, i, largo) != Cual)
                {
                    posi = i;
                    break;
                }

            }

            if (posi == 0)
            {
                return "";
            }

            if (Modo != 0)
            {
                //por la derecha
                return VB6Helpers.Mid(Que, 1, posi + largo - 1);
            }
            else
            {
                return VB6Helpers.Mid(Que, posi);
            }

        }
        
        //recorta un string basado en largo posible segun font
        //public static string RecortaTexto(dynamic Lista, string Texto, short Maximo)
        //{
        //    short LarMax = 0;
        //    string Aux = "";
        //    short i = 0;
        //    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
        //    if (En.FontBold != VB6Helpers.CBool(VB6Helpers.Invoke(VB6Helpers.CObj(Lista), "FontBold")))
        //    {
        //        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
        //        En.FontBold = VB6Helpers.Invoke<bool>(VB6Helpers.CObj(Lista), "FontBold");
        //    }
        //    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
        //    if (En.FontName != VB6Helpers.Invoke<string>(VB6Helpers.CObj(Lista), "FontName"))
        //    {
        //        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
        //        En.FontName = VB6Helpers.Invoke<string>(VB6Helpers.CObj(Lista), "FontName");
        //    }

        //    //LarMax = (short)En.TextWidth(VB6Helpers.String(Maximo, "W"));

        //    Aux = VB6Helpers.Trim(Texto);
        //    for (i = (short)VB6Helpers.Len(Aux); i >= 1; i--)
        //    {
        //        if (En.TextWidth(VB6Helpers.Left(Aux, i)) <= LarMax)
        //        {
        //            return VB6Helpers.Left(Aux, i);
        //        }

        //    }

        //    //si salgo por aqui es que especificaron maximo = 0 ==> string nulo
        //    return string.Empty;
        //}

        //reformatea un string numerico en formato despliege Windows al formato Val()
        //TODO: Al parecer para esto se utilizará solo el tema de globalization
        //public static string unformat(string Numero)
        //{
        //    string _retValue = "";
        //    string Temp = "";
        //    short Donde = 0;
        //    if (Win.SepDecimal == "" || Win.SepCientos == "")
        //    {
        //        //leer parametros
        //        GetSeteoWin(); //obtiene seteo win
        //    }

        //    //temporal sacando espacios finales e iniciales
        //    Temp = VB6Helpers.LTrim(VB6Helpers.RTrim(Numero));

        //    //primero sacamos los separadores de cientos

        //    Donde = (short)VB6Helpers.Instr(1, Temp, Win.SepCientos);
        //    while (Donde != 0)
        //    {

        //        Temp = VB6Helpers.Left(Temp, Donde - 1) + VB6Helpers.Right(Temp, VB6Helpers.Len(Temp) - Donde);
        //        Donde = (short)VB6Helpers.Instr(1, Temp, Win.SepCientos);
        //    }

        //    //ok, ya sacamos los separadores de cientos, cambiemos el separador decimal
        //    Donde = (short)VB6Helpers.Instr(1, Temp, Win.SepDecimal);
        //    _retValue = Temp;
        //    if (Donde != 0)
        //    {
        //        return VB6Helpers.Left(Temp, Donde - 1) + "." + VB6Helpers.Right(Temp, VB6Helpers.Len(Temp) - Donde);
        //    }

        //    return _retValue;
        //}

        public static short PosListaLin(UI_Combo Lista, string numlinea)
        {
            short _retValue = 0;

            short i = 0;

            _retValue = -1;
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
            for (i = 0; i < Lista.Items.Count; i++)
            {
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
                if (Lista.Items.ElementAt(i).Value.Equals(numlinea))
                {
                    return i;
                }

            }

            return _retValue;
        }

        //Determina Cuantos substring separados por Separa hay dentro de
        //EnDonde. El string tiene la forma "----,----,----"
        public static short cuentadestring(string EnDonde, string Separa)
        {
            short largo = (short)VB6Helpers.Len(Separa);
            short Inicio = 1;
            short Total = 0;
            short Fin = 0;
            while (Inicio != 0)
            {

                Fin = (short)VB6Helpers.Instr(Inicio, EnDonde, Separa);
                if (Fin != 0)
                {
                    //hay otro separador

                    Total = (short)(Total + 1);
                    Inicio = (short)(Fin + largo);
                }
                else
                {
                    //no hay otro
                    if (Inicio <= VB6Helpers.Len(EnDonde))
                    {
                        Total = (short)(Total + 1);
                    }
                    break;
                }

            }

            return Total;
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

        public static short PosLista(UI_Combo combo, short Codigo)
        {
            short _retValue = 0;
            short i = 0;
            _retValue = -1;

            for (i = 0; i < combo.Items.Count; i++)
            {
                if (Convert.ToInt16(combo.Items[i].Data) == Codigo)
                {
                    return i;                    
                }
            }
            return _retValue;
        }

        public static short PosLista(UI_ListBox lista, short Codigo)
        {
            short _retValue = 0;
            short i = 0;
            _retValue = -1;

            for (i = 0; i < lista.Items.Count; i++)
            {
                if (Convert.ToInt16(lista.Items[i].Data) == Codigo)
                {
                    return i;
                }
            }
            return _retValue;
        }

        public static short PosLista(UI_Grid lista, short Codigo)
        {
            short _retValue = 0;
            short i = 0;
            _retValue = -1;

            for (i = 0; i < lista.Items.Count; i++)
            {
                if (Convert.ToInt16(lista.Items[i].ID) == Codigo)
                {
                    return i;                    
                }
            }
            return _retValue;
        }

        //public static short PosLista(BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos.UI_Combo combo, short Codigo)
        //{
        //    short _retValue = 0;
        //    short i = 0;
        //    _retValue = -1;
        //    var Lista = ((BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos.UI_Combo)combo);
        //    for (i = 0; i < ((BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos.UI_Combo)combo).Items.Count; i++)
        //    {
        //        if (Convert.ToInt16(Lista.Items[i].Data) == Codigo)
        //        {
        //            return i;
        //        }
        //    }
        //    return _retValue;
        //}

        public static short PosListBox(UI_ListBox ListaDynamic, short Codigo)
        {
            short _retValue = 0;
            short i = 0;
            _retValue = -1;
            var Lista = ListaDynamic;
            for (i = 0; i < Lista.Items.Count; i++)
            {
                if (Convert.ToInt16(Lista.Items[i].Data) == Codigo)
                {
                    return i;
                }
            }
            return _retValue;
        }

        public static short MascaraLost(UI_TextBox Obj, T_MODGPYF0 MODGPYF0)
        {
            short posi = 0;
            short Pre = 0;
            short pos_tag = 0;
            short PosText = 0;
            short PreText = 0;
            string es = "";
            short i = 0;
            string Txt = "";
            // UPGRADE_INFO (#0561): The 'vale' symbol was defined without an explicit "As" clause.
            const string vale = "0123456789";

            if (MODGPYF0.Win.SepDecimal == "")
            {
                //leer parametros
                GetSeteoWin(MODGPYF0); //obtiene seteo win
            }

            //determino cuanto caracteres debo aceptar
            posi = (short)VB6Helpers.Instr(1, VB6Helpers.CStr(Obj.Tag), ".");
            if (posi != 0)
            {
                Pre = (short)VB6Helpers.Len(VB6Helpers.Left(VB6Helpers.CStr(Obj.Tag), posi - 1));
                pos_tag = (short)(VB6Helpers.Len(VB6Helpers.CStr(Obj.Tag)) - posi);
            }
            else
            {
                Pre = (short)Obj.Tag.ToString().Length;
            }

            //determino cuanto caracteres tiene dato
            posi = (short)VB6Helpers.Instr(1, VB6Helpers.CStr(Obj.Text), MODGPYF0.Win.SepDecimal);
            if (posi != 0)
            {
                PreText = (short)VB6Helpers.Len(VB6Helpers.Left(VB6Helpers.CStr(Obj.Text), posi - 1));
                PosText = (short)(VB6Helpers.Len(VB6Helpers.CStr(Obj.Text)) - posi);
            }
            else
            {
                PreText = (short)VB6Helpers.Len(VB6Helpers.CStr(Obj.Text));
            }

            es = vale + MODGPYF0.Win.SepDecimal;
            for (i = 1; i <= (short)VB6Helpers.Len(VB6Helpers.CStr(Obj.Text)); i++)
            {
                if (VB6Helpers.Instr(1, es, VB6Helpers.Mid(VB6Helpers.CStr(Obj.Text), i, 1)) == 0)
                {
                    //VB6Helpers.MsgBox("Algún caracter ingresado no es válido, sólo se permiten caracteres numéricos y el separador de decimales.", MsgBoxStyle.Information, "Formato Numérico");
                     //VB6Helpers.Invoke(VB6Helpers.CObj(Obj), "SetFocus");
                    return (short)(true ? -1 : 0);
                }

            }

            if (PreText > Pre || PosText > pos_tag)
            {
                Txt = "Número ingresado no es válido, se permiten hasta " + VB6Helpers.Format(VB6Helpers.CStr(Pre)) + " enteros";
                if (pos_tag != 0)
                {
                    Txt = Txt + " y " + VB6Helpers.Format(VB6Helpers.CStr(pos_tag)) + " decimales.";
                }
                else
                {
                    Txt += ".";
                }

                //VB6Helpers.MsgBox(Txt, MsgBoxStyle.Information, "Formato Numérico");
                
                if (Obj.Enabled)
                {
                    //VB6Helpers.Invoke(VB6Helpers.CObj(Obj), "SetFocus");
                }
                return (short)(true ? -1 : 0);
            }

            return 0;
        }



        public static double FDouble(double Num, short NDecs, T_MODGPYF0 modgpyf0)
        {
            string Fto = "0." + VB6Helpers.String(NDecs, "0");
            string mto = unformat(modgpyf0, Num.ToString(Fto));
            double MtoD = VB6Helpers.Val(mto);

            return MtoD;
        }

        private static string FechaHora;
        private static short Vez;
        public static string Siu()
        {
            string _retValue = "";
            double hoy = 0;
            string Fechora = "";
            // UPGRADE_INFO (#0561): The 'ChrBase' symbol was defined without an explicit "As" clause.
            const short ChrBase = 40;
            // UPGRADE_INFO (#0561): The 'MaxVez' symbol was defined without an explicit "As" clause.
            const short MaxVez = 59;

            do
            {

                hoy = VB6Helpers.DateToDouble(DateTime.Now);
                Fechora = VB6Helpers.Hex(VB6Helpers.Int(hoy)) + VB6Helpers.Chr(ChrBase + VB6Helpers.Hour(VB6Helpers.DoubleToDate(hoy))) + VB6Helpers.Chr(ChrBase + VB6Helpers.Minute(VB6Helpers.DoubleToDate(hoy))) + VB6Helpers.Chr(ChrBase + VB6Helpers.Second(VB6Helpers.DoubleToDate(hoy)));

                if (Fechora != FechaHora)
                {
                    // distinto, ok
                    FechaHora = Fechora;
                    Vez = 0;
                    return Fechora + VB6Helpers.Chr(ChrBase);
                }
                else if (Vez + 1 <= MaxVez)
                {
                    //igual, nueva secuencia
                    Vez = (short)(Vez + 1);
                    _retValue = Fechora + VB6Helpers.Chr(ChrBase + Vez);
                }
                //secuencia agotada
            }
            while (true); //repetir hasta encuetre uno

            return _retValue;
        }
    }
}
