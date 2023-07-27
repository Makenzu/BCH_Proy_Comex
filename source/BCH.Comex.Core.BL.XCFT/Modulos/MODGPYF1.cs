using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGPYF1
    {
        public static double Dec_EnMto(InitializationObject initObj, UnitOfWorkCext01 uow, double mto, short mnd)
        {
            short m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, mnd);
            if (initObj.MODGTAB0.VMnd[m].Mnd_MndSin != 0)
            {
                return Format.StringToDouble(Format.FormatCurrency(mto, "0"));
            }
            else
            {
                return Format.StringToDouble(Format.FormatCurrency(mto, "0.00"));
            }

        }
        public static dynamic Fn_GetN(dynamic caja, InitializationObject initObj)
        {
            string s = VB6Helpers.CStr(caja);
            s = MODGPYF0.unformat(initObj.MODGPYF0, s);
            return VB6Helpers.Val(s);
        }

        public static void selTexto(dynamic caja)
        {
            //VB6Helpers.Set(VB6Helpers.CObj(caja), "SelStart", 0);          
            //VB6Helpers.Set(VB6Helpers.CObj(caja), "SelLength", VB6Helpers.Len(VB6Helpers.CStr(caja.Text)));
        }
        public static string Minuscula(string Pdato)
        {
            // UPGRADE_INFO (#05B1): The 'Palabras' variable wasn't declared explicitly.
            string[] Palabras = null;

            Palabras = new string[1];
            string Dato = MODGPYF0.Componer(Pdato, "  ", " ");
            short i = 1;
            string s = MODGPYF0.copiardestring(Dato, " ", i);
            short j = 0;
            //Deja cada palabra separado con un solo blanco
            while(!string.IsNullOrEmpty(s))
            {

                //Verifica si la palabra siguiente contiene la cantidad de caracteres necesarios.
                j = (short)(VB6Helpers.UBound(Palabras) + 1);
                VB6Helpers.RedimPreserve(ref Palabras, 0, j);
                string _switchVar1 = VB6Helpers.Trim(VB6Helpers.UCase(s));
                if (_switchVar1 == "S.A." || _switchVar1 == "M/E" || _switchVar1 == "M/N")
                {
                    Palabras[j] = VB6Helpers.UCase(s);
                }
                else if (_switchVar1 == "A" || _switchVar1 == "DE" || _switchVar1 == "Y" || _switchVar1 == "O" || _switchVar1 == "Y/O" || _switchVar1 == "U" || _switchVar1 == "AL" || _switchVar1 == "DE" || _switchVar1 == "LO" || _switchVar1 == "LA" || _switchVar1 == "EL" || _switchVar1 == "SI" || _switchVar1 == "NO" || _switchVar1 == "E" || _switchVar1 == "POR" || _switchVar1 == "OF")
                {
                    Palabras[j] = VB6Helpers.LCase(s);
                }
                else
                {
                    Palabras[j] = VB6Helpers.UCase(VB6Helpers.Mid(s, 1, 1)) + VB6Helpers.LCase(VB6Helpers.Mid(s, 2, VB6Helpers.Len(s) - 1));
                }

                i = (short)(i + 1);
                s = MODGPYF0.copiardestring(Dato, " ", i);
            }

            s = "";
            for (i = 0; i <= (short)VB6Helpers.UBound(Palabras); i++)
            {
                // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                s = s + Palabras[i] + " ";
            }

            return VB6Helpers.Trim(s);
        }

        //****************************************************************************
        //   1.  Retorna el formato decimal del número del objeto.
        //****************************************************************************
        public static string DecObjeto(dynamic Objeto)
        {
            string s = String.Empty;
            try
            {
                s = VB6Helpers.CStr(Objeto.Tag);
            }
            catch
            {
                s = VB6Helpers.CStr(Objeto.Tag[0]);
                Objeto.Tag = s;
            }

            if (s == null && Objeto.GetType().GetProperty("Mask") != null) //Se agrega la mascara como última opción, si es que existe la propiedad y está cargada
            {
                s = (string)Objeto.Mask;
            }

            string d = MODGPYF0.copiardestring(s, ".", 2);
            if (d == "")
            {
                return "0";
            }
            else
            {
                return "0." + Zeros((short)VB6Helpers.Len(d));
            }

        }

        //Retorna una cadena de caracteres con "Cuantos" ceros concatenados.-
        public static string Zeros(short Cuantos)
        {
            short i = 0;
            string s = "";
            for (i = 1; i <= (short)Cuantos; i++)
            {
                // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                s += "0";
            }

            return s;
        }

        //****************************************************************************
        //   1.  Retorna el formato del número del objeto.
        //****************************************************************************
        public static string FmtObjeto(dynamic Objeto)
        {
            string s = String.Empty;
            try
            {
                s = VB6Helpers.CStr(Objeto.Tag);
            }
            catch
            {
                s = VB6Helpers.CStr(Objeto.Tag[0]);
            }
            string e = MODGPYF0.copiardestring(s, ".", 1);
            string d = MODGPYF0.copiardestring(s, ".", 2);
            string z = "";
            string Y = "";
            int _switchVar1 = VB6Helpers.Len(e);
            if (_switchVar1 == 2)
            {
                z += "#0";
            }
            else if (_switchVar1 == 7)
            {
                z += "#,###,##0";
            }
            else if (_switchVar1 == 13)
            {
                z += "#,###,###,###,##0";
            }

            Y = Zeros((short)VB6Helpers.Len(d));

            if(!string.IsNullOrEmpty(Y))
            {
                z = z + "." + Y;
            }
            return z;
        }

        //Retorna el Valor Numérico de una Caja de Texto con Formato.-
        public static double ValTexto(T_MODGPYF0 MODGPYF0, string caja)
        {
            string s = String.IsNullOrEmpty(caja) ? "0" : caja;
            //s = BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, s);
            //return VB6Helpers.Val(s);
            return Double.Parse(s);
        }

        public static double ValidaFecha(string Fecha)
        {
            double _retValue = 0;

            try
            {
                // IGNORED: On Error GoTo MiError
                if (VB6Helpers.IsDate(Fecha))
                {
                    _retValue = VB6Helpers.DateToDouble(VB6Helpers.DateValue(Fecha));
                }
                else
                {
                    _retValue = 0;
                }
            }
            catch (Exception _ex)
            {
                _retValue = (false ? -1 : 0);
            }
            return _retValue;
        }

        public static double Dec_EnMto(T_MODGTAB0 MODGTAB0, T_MODGPYF0 MODGPYF0, UnitOfWorkCext01 unit, double mto, short mnd)
        {
            short m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, mnd);
            if (MODGTAB0.VMnd[m].Mnd_MndSin != 0)
            {
                return Format.StringToDouble(Format.FormatCurrency(mto, "0"));
            }
            else
            {
                return Format.StringToDouble(Format.FormatCurrency(mto, "0.00"));
            }

        }

        //Función que retorna una Operación con Raya.-
        public static string Fn_OpeConRaya(string Operacion)
        {
            return VB6Helpers.Mid(Operacion, 1, 3) + "-" + VB6Helpers.Mid(Operacion, 4, 2) + "-" + VB6Helpers.Mid(Operacion, 6, 2) + "-" + VB6Helpers.Mid(Operacion, 8, 3) + "-" + VB6Helpers.Mid(Operacion, 11, 5);
        }

        public static void SeparaL(List<UI_Message> MESSAGES, dynamic Texto, ref string[] Lineas, short largo, short LargoTot, string Campo = "")
        {

            //Separa un string en lineas del largo que se le indique y se deja en Lineas()
            //Además depliega mensaje warning cuando supera el largo total entregado por parámetro

            short j = 1;
            short posini = 0;
            short k = 0;
            string Resto = VB6Helpers.RTrim(VB6Helpers.CStr(Texto));
            string Unidad = "";
            short largtot = 0;

            Lineas = new string[j + 1];
            while(!string.IsNullOrEmpty(Resto))
            {

                posini = (short)VB6Helpers.Instr(1, Resto, " ");
                if (posini != 0)
                {
                    Unidad = VB6Helpers.Left(Resto, posini);
                    Resto = VB6Helpers.Right(Resto, VB6Helpers.Len(Resto) - posini);
                }
                else
                {
                    Unidad = Resto;
                    Resto = "";
                }

                //se valida que el largo de la unidad no sea superior al largo de la linea solicitado
                if (Unidad.Length > largo && !string.IsNullOrEmpty(Campo))
                {
                    MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "No es posible realizar la separación de texto en líneas para el campo "+ Campo + " ya que sobrepasa el máximo de "+ largo + " carácteres continuos" ,
                        Title = "Separador de Texto"
                    });

                    return;
                }

                largtot = 0;
                for (k = 1; k <= (short)VB6Helpers.UBound(Lineas); k++)
                {
                    largtot = (short)(largtot + VB6Helpers.Len(Lineas[k]));
                }

                largtot = (short)(largtot + VB6Helpers.Len(Unidad));

                if (largtot <= LargoTot)
                {
                    if (VB6Helpers.Len(Lineas[j]) + VB6Helpers.Len(Unidad) <= largo)
                    {
                        Lineas[j] += Unidad;
                    }
                    else
                    {
                        j = (short)(j + 1);
                        VB6Helpers.RedimPreserve(ref Lineas, 0, j);
                        Lineas[j] += Unidad;
                    }
                    

                }
                
                else
                {
                    MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "No es posible realizar la separación de texto en líneas, se recibió un texto mayor.",
                        Title = "Separador de Texto"
                    });

                    return;
                }
                // valida que la linea nro 5 tenga un largo entre 33, 34 o 35 caracteres.
                if (j == 4 && (VB6Helpers.Len(Lineas[4]) == 33 || VB6Helpers.Len(Lineas[4]) == 34 || VB6Helpers.Len(Lineas[4]) == 35))
                {
                    return;
                }
            }
        }

        public static short EsFecha2000(string Objeto, InitializationObject iniObject, string title)
        {
            short dif = 0;

            if(!string.IsNullOrEmpty(Objeto))
            {
                if (VB6Helpers.Len(VB6Helpers.CStr(Objeto)) != 10)
                {
                    //VB6Helpers.Beep();
                    //VB6Helpers.MsgBox("La Fecha ingresada debe estar en el siguiente formato : dd/mm/aaaa.", MsgBoxStyle.Critical, "Fecha Inválida");
                    //VB6Helpers.Invoke(VB6Helpers.CObj(Objeto), "SetFocus");
                    iniObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "La Fecha ingresada debe estar en el siguiente formato : dd/mm/aaaa.",
                        Title = title
                    });
                    return 0;
                }

                //Para Año Bisiesto valida cantidad de días de febrero
                //if (~ValDiasEnMes((short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 1, 2)), (short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 4, 2)), (short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 7, 4))) != 0)
                if (~ValDiasEnMes((short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto), 1, 2)), (short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto), 4, 2)), (short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto), 7, 4))) != 0)
                {
                    iniObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "La Fecha no es valida.",
                        Title = title
                    });
                    //  VB6Helpers.Invoke(VB6Helpers.CObj(Objeto), "SetFocus");
                    return 0;
                }

                //Rango de Fechas válidas + - 20 años.-
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Objeto'. Consider using the GetDefaultMember6 helper method.
                dif = (short)(VB6Helpers.Year(VB6Helpers.CDate(Objeto)) - VB6Helpers.Year(DateTime.Now));
                if (VB6Helpers.Abs(dif) > 20)
                {
                    //VB6Helpers.Beep();
                    //VB6Helpers.MsgBox("La Fecha ingresada sobrepasa el rango permitido (20 años) respecto del año actual.", MsgBoxStyle.Critical, "Fecha Inválida");
                    //VB6Helpers.Invoke(VB6Helpers.CObj(Objeto), "SetFocus");
                    iniObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "La Fecha ingresada sobrepasa el rango permitido (20 años) respecto del año actual.",
                        Title = title
                    });
                    return 0;
                }

            }

            return (short)(true ? -1 : 0);
        }

        public static short ValDiasEnMes(short ndias, short nmes, short nano)
        {
            switch (nmes)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (ndias <= 31)
                    {
                        return (short)(true ? -1 : 0);
                    }
                    else
                    {
                        VB6Helpers.Beep();
                        VB6Helpers.MsgBox("El mes sólo tiene 31 días.", MsgBoxStyle.Critical, "Entrada Inválida");
                        return (short)(false ? -1 : 0);
                    }

                case 2:
                    //Es Bisiesto
                    if (((nano % 4 == 0) && (nano % 100 != 0)) || (nano % 400 == 0))
                    {
                        if (ndias <= 29)
                        {
                            return (short)(true ? -1 : 0);
                        }
                        else
                        {
                            VB6Helpers.Beep();
                            VB6Helpers.MsgBox("El mes puede tener 28 ó 29 días.", MsgBoxStyle.Critical, "Entrada Inválida");
                            return (short)(false ? -1 : 0);
                        }

                        //No es Bisiesto
                    }
                    else
                    {
                        if (ndias <= 28)
                        {
                            return (short)(true ? -1 : 0);
                        }
                        else
                        {
                            VB6Helpers.Beep();
                            VB6Helpers.MsgBox("El mes sólo tiene 28 días.", MsgBoxStyle.Critical, "Entrada Inválida");
                            return (short)(false ? -1 : 0);
                        }

                    }

                case 4:
                case 6:
                case 9:
                case 11:
                    if (ndias <= 30)
                    {
                        return (short)(true ? -1 : 0);
                    }
                    else
                    {
                        VB6Helpers.Beep();
                        VB6Helpers.MsgBox("El mes sólo tiene 30 días.", MsgBoxStyle.Critical, "Entrada Inválida");
                        return (short)(false ? -1 : 0);
                    }

            }

            return 0;
        }

        public static double ValTexto(string caja, InitializationObject initObj)
        {
            string s = VB6Helpers.CStr(caja);
            s = MODGPYF0.unformat(initObj.MODGPYF0, s);
            return VB6Helpers.Val(s);
        }

        //Incluye n caracteres a la izquierad o derecha de un string.-
        // UPGRADE_INFO (#0561): The 'Caracter' symbol was defined without an explicit "As" clause.
        public static string PoneChar(string Texto, dynamic Caracter, string DerIzq, short largo)
        {
            string T = VB6Helpers.Trim(Texto);
            short i = 0;
            string s = "";
            if (VB6Helpers.Len(T) >= largo)
            {
                return T;
            }

            for (i = 1; i <= (short)(largo - VB6Helpers.Len(T)); i++)
            {
                // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Caracter'. Consider using the GetDefaultMember6 helper method.
                s += Caracter;
            }

            if (DerIzq == "D")
            {
                return T + s;
            }
            else if (DerIzq == "H")
            {
                return s + T;
            }

            return "";
        }

        //Nombre del Modulo FORMATFECHA
        //Parámetros  OBJETO y TECLA presionada
        //
        //Este Modulo Recibe un Objeto de tipo TEXT (objeto de Ingreso de Texto)
        //y el código ascii presionado
        //Deja la entrada del objeto en el formato 99/99/9999
        //Para que el algoritmo funcione correctamente se debe poner
        //en el LostFocus del objeto una llamada al modulo valifafecha
        //
        //
        public static void formatfecha(UI_TextBox  Objeto, ref short tecla)
        {

            if (VB6Helpers.Len(VB6Helpers.CStr(Objeto.Text)) > 9)
            {
                if (tecla != 8)
                {
                    VB6Helpers.Beep();
                    tecla = 0;
                    return;
                }

            }
            else
            {
                if ((tecla >= 0 && tecla <= 7) || (tecla >= 9 && tecla <= 47) || (tecla >= 58 && tecla <= 255))
                {
                    VB6Helpers.Beep();
                    tecla = 0;
                    return;
                }
                else if (tecla >= 48 && tecla <= 57)
                {
                    //Dígitos 0..9
                    //Se debe Formatear la Entrada con 99/99/9999
                    int _switchVar1 = VB6Helpers.Len(VB6Helpers.CStr(Objeto.Text));
                    if (_switchVar1 == 2)
                    {
                        //Debe ser menor que 31
                        if (VB6Helpers.Val(Objeto.Text) > 31 || VB6Helpers.Val(Objeto.Text) < 1)
                        {
                            tecla = 0;
                            VB6Helpers.MsgBox("Desde 1 Hasta 31 días por mes", MsgBoxStyle.Critical, "Entrada Invalida");
                            return;
                        }
                        else
                        {
                            Objeto.Text = Objeto.Text + "/";
                            //VB6Helpers.Set(VB6Helpers.CObj(Objeto), "SelStart", VB6Helpers.Len(VB6Helpers.CStr(Objeto.Text)));
                        }

                    }
                    else if (_switchVar1 == 5 || _switchVar1 == 6)
                    {
                        //Los Meses deben ser<12
                        if (tecla != 49 && tecla != 50)
                        {
                            VB6Helpers.Beep();
                            tecla = 0;
                            return;
                        }

                        if (VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 4, 2)) > 12 || VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 4, 2)) < 1)
                        {
                            VB6Helpers.Beep();
                            tecla = 0;
                            VB6Helpers.MsgBox("Desde 1 Hasta 12 meses", MsgBoxStyle.Critical, "Entrada Invalida");
                            return;
                        }
                        else
                        {
                            if (~ValDiasEnMes((short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 1, 2)), (short)VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.CStr(Objeto.Text), 4, 2)), 0) != 0)
                            {
                                tecla = 0;
                                return;
                            }
                            else
                            {
                                if (VB6Helpers.Len(VB6Helpers.CStr(Objeto.Text)) == 5)
                                {
                                    Objeto.Text = Objeto.Text + "/";
                                }
                                //VB6Helpers.Set(VB6Helpers.CObj(Objeto), "SelStart", VB6Helpers.Len(VB6Helpers.CStr(Objeto.Text)));
                            }

                        }

                    }

                }

            }

        }
    }
}
