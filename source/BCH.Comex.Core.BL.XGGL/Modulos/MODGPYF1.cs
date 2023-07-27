using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGPYF1
    {
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
            while (!string.IsNullOrEmpty(s))
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

        public static void SeparaL(DatosGlobales Globales, string Texto, ref string[] Lineas, int largo, int LargoTot)
        {
            int largtot = 0;

            // Separa un string en lineas del largo que se le indique y se deja en Lineas()
            // Además depliega mensaje warning cuando supera el largo total entregado por parámetro

            int FlgUlt = 0;
            int j = 0;
            int posini = 0;
            int k = 0;
            string Resto = "";
            string Unidad = "";

            Resto = Texto.TrimR();
            j = 1;
            Lineas = new string[j + 1];
            while (Resto != "")
            {

                posini = Resto.InStr(" ", 1, StringComparison.CurrentCulture);
                if (posini != 0)
                {
                    Unidad = Resto.Left(posini);
                    Resto = Resto.Right((Resto.Len() - posini));
                }
                else
                {
                    Unidad = Resto;
                    Resto = "";
                }

                largtot = 0;
                for (k = 1; k <= Lineas.GetUpperBound(0); k += 1)
                {
                    largtot = largtot + Lineas[k].Len();
                }
                largtot = largtot + Unidad.Len();

                if (largtot <= LargoTot)
                {
                    if (Lineas[j].Len() + Unidad.Len() <= largo)
                    {
                        Lineas[j] = Lineas[j] + Unidad;
                    }
                    else
                    {
                        j = j + 1;
                        Array.Resize(ref Lineas, j + 1);
                        Lineas[j] = String.Empty;
                        Lineas[j] = Lineas[j] + Unidad;
                    }
                }
                else
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Text= "Función separador de texto en líneas (SeparaL) recibió un largo mayor al indicado por lo tanto se generó truncado. Revise o avisar al encargado.",
                        Type=Common.UI_Modulos.TipoMensaje.Error,
                        Title= "Separador de Texto"
                    });
                    return;
                }
            }

            //     Dim Txt As String
            // 
            //     Txt = Trim$(Texto)
            //     Txt = Componer(Txt, Chr(13) + Chr(10), " ")
            // 
            //     Veces = LargoTot \ Largo
            //     For i% = 1 To Veces
            //     ReDim Preserve Lineas(i%)
            //     If Len(Txt) < Largo Then
            //         Lineas(i%) = Txt
            //         Exit Sub
            //     Else
            //         Lineas(i%) = Mid(Txt, 1, Largo)
            //         Txt = Mid(Txt, Largo + 1, Len(Txt))
            //     End If
            //     Next

        }

        // ****************************************************************************
        //    1.  Retorna el formato decimal del número del objeto.
        // ****************************************************************************
        public static string DecObjeto(UI_Control Objeto)
        {
            string DecObjeto = "";

            string d = "";
            string s = "";

            s = Objeto.Tag.ToStr();
            d = MODGPYF0.copiardestring(s, ".", 2);
            if (d == "")
            {
                DecObjeto = "0";
            }
            else
            {
                DecObjeto = "0." + Zeros(d.Len());
            }

            return DecObjeto;
        }

        // Retorna una cadena de caracteres con "Cuantos" ceros concatenados.-
        public static string Zeros(int Cuantos)
        {
            string Zeros = "";

            string s = "";
            int i = 0;

            for (i = 1; i <= Cuantos; i += 1)
            {
                s = s + "0";
            }
            Zeros = s;

            return Zeros;
        }

        // Formatea un Partys agregando el caracter | a la cola.-
        public static string Fn_FormateaPrty(string Party)
        {
            string Fn_FormateaPrty = "";

            int i = 0;
            int n = 0;
            string llave = "";

            llave = Party.TrimB();
            n = 12 - llave.Len();
            if (n > 0)
            {
                for (i = 1; i <= n; i += 1)
                {
                    llave = llave + "|";
                }
            }
            Fn_FormateaPrty = llave;

            return Fn_FormateaPrty;
        }
    }
}
