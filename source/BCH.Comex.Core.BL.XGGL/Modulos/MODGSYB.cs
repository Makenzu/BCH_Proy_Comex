using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGSYB
    {
        //Guarda las estructuras de las Tablas.-
        public static short SyContador;

        //Retorna el valor de un campo teniendo el resultado de la consulta.-
        public static dynamic GetPosSy(short Posicion, string Tipo, string Resultado)
        {
            string f = string.Empty;
            string ResultQuery = MODGPYF0.copiardestring(Resultado, "~", Posicion);
            string hora = string.Empty;
            string Fecha = string.Empty;
            string h = string.Empty;
            string monto = string.Empty;

            //TipoCampo=[N/C/F/L]: Numérico, Caracter, Fecha, Lógico.-
            switch (Tipo)
            {
                case "N":  //Numérico.-
                    return VB6Helpers.Val(ResultQuery);
                case "C":
                    ResultQuery = MODGPYF0.Componer(ResultQuery, "|", "~");
                    ResultQuery = MODGPYF0.Componer(ResultQuery, "´", "'");
                    ResultQuery = MODGPYF0.Componer(ResultQuery, "¢", "ó");
                    ResultQuery = MODGPYF0.Componer(ResultQuery, "§", "º");
                    return ResultQuery;
                case "F":  //Fecha.-
                    f = MODGPYF0.copiardestring(VB6Helpers.Trim(ResultQuery), " ", 1);
                    if (DateTime.Parse(f).ToString("dd/MM/yyyy") == "01/01/1900")
                    {
                        //Fecha nula.-
                        return string.Empty;
                    }
                    else
                    {

                        return f;
                    }

                case "H":  //Hora.-
                    f = MODGPYF0.copiardestring(VB6Helpers.Trim(ResultQuery), " ", 2);
                    return VB6Helpers.Left(f, 8);
                case "D":  //Date : Fecha + Hora (dd/mm/yyyy + hh:mm:ss).-
                    ResultQuery = VB6Helpers.Trim(ResultQuery);
                    Fecha = MODGPYF0.moverdestring(ref ResultQuery, " ", 1);
                    if (VB6Helpers.Right(ResultQuery, 2) == "PM" || VB6Helpers.Right(ResultQuery, 2) == "AM")
                    {
                        hora = VB6Helpers.Trim(VB6Helpers.Left(ResultQuery, 8)) + VB6Helpers.Right(ResultQuery, 2);
                    }
                    else
                    {
                        hora = VB6Helpers.Trim(VB6Helpers.Left(ResultQuery, 8));
                    }

                    if (DateTime.Parse(Fecha).ToShortDateString() != DateTime.Parse("01/01/1900").ToShortDateString())
                    {
                        f = DateTime.Parse(Fecha).ToString("dd/MM/yyyy");
                        h = TimeSpan.Parse(hora).ToString("hh:nn:ss");
                        return f + " " + h;
                    }
                    else
                    {
                        return string.Empty;
                    }

                case "f":  //Fecha SmallDateTime.-
                    f = transfecha(ResultQuery);
                    if (DateTime.Parse(f).ToString("dd/MM/yyyy") == "01/01/1900")
                    {
                        return string.Empty;
                    }
                    else
                    {
                        //GetPosSy = Format$(GetFechaOK(f$), "dd/MM/yyyy")
                        return DateTime.Parse(f).ToString("dd/MM/yyyy");
                    }

                case "L":  //Lógico.-
                    if (VB6Helpers.Val(ResultQuery) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                case "T":  //Fecha
                    f = ResultQuery;
                    return DateTime.Parse(f).ToString("dd/MM/yyyy");
                case "CD":
                    //TODO ARKANO monto = VB6Helpers.Format(VB6Helpers.CStr(Format.StringToDouble(Mdl_SRM.srm_num(ResultQuery))), "#.#0");
                    return MODGPYF0.Componer(monto, ",", string.Empty);
            }

            return null;
        }

        public static string transfecha(string Fecha)
        {

            if (VB6Helpers.Mid(Fecha, 1, 3) == "Jan")
            {
                return "Ene" + VB6Helpers.Mid(Fecha, 4, VB6Helpers.Len(Fecha));
            }
            else
            {
                if (VB6Helpers.Mid(Fecha, 1, 3) == "Apr")
                {
                    return "Abr" + VB6Helpers.Mid(Fecha, 4, VB6Helpers.Len(Fecha));
                }
                else
                {
                    if (VB6Helpers.Mid(Fecha, 1, 3) == "Aug")
                    {
                        return "Ago" + VB6Helpers.Mid(Fecha, 4, VB6Helpers.Len(Fecha));
                    }
                    else
                    {
                        if (VB6Helpers.Mid(Fecha, 1, 3) == "Dec")
                        {
                            return "Dic" + VB6Helpers.Mid(Fecha, 4, VB6Helpers.Len(Fecha));
                        }
                        else
                        {
                            return Fecha;
                        }

                    }

                }

            }
        }

        //Inicializa Contador de Campos.-
        public static short NumIni()
        {

            SyContador = 1;
            return SyContador;
        }

        //Retorna el contador incrementado.-
        public static short NumSig()
        {

            SyContador = (short)(SyContador + 1);
            return SyContador;
        }

        public static string dbcharSy(string Valor)
        {
            string s = Valor;
            if (VB6Helpers.Instr(s, "~") != 0)
            {
                s = MODGPYF0.Componer(s, "~", "|");
            }
            if (VB6Helpers.Instr(s, "'") != 0)
            {
                s = MODGPYF0.Componer(s, "'", "´");
            }
            if (String.IsNullOrEmpty(s))
            {
                s = String.Empty;
            }
            return s;
        }

        public static string dbdatesy(string Valor)
        {
            try
            {
                if (String.IsNullOrEmpty(Valor))
                {
                    //return DateTime.Now.ToString("yyyy-MM-dd");
                    return String.Empty;
                }
                return DateTime.Parse(Valor).ToString("yyyy-MM-dd");
            }
            catch
            {
                return String.Empty;
            }
            
        }

        public static string dblogisy(short val)
        {
            if(val == 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public static string dblogisy2(short val)
        {
            return val == 0 ? "false" : "true";
        }

        public static string dbtasaSy(double Valor)
        {
            return Format.FormatUSA(Valor, "#0.000000");
        }

        public static string dbtasaSyForRead(double Valor)
        {
            return Format.FormatCurrency(Valor, "#0.000000");
        }

        public static string dbnumesy(double val)
        {
            return val.ToString();
        }

        public static string dbnumesy(dynamic val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return "0";
            }
            return val.ToString();
        }

        public static string dbmontoSy(double Valor)
        {
            string _retValue = string.Empty;
            //TODO: PROBLEMA CON LOS MONTOS, DETERMINAR EL FORMATO EN QUE SE DEBEN ENVIAR
            string monto = Format.FormatUSA(Valor, "#0.00");
            //string monto = Format.FormatCurrency(Valor, "#0.00");
            _retValue = monto;
            return _retValue;
        }

        public static string dbmontoSyForRead(double Valor)
        {
            string _retValue = string.Empty;
            string monto = Format.FormatCurrency(Valor, "#0.00");
            _retValue = monto;
            return _retValue;
        }

        public static decimal dbDecimal(string val)
        {
            if (String.IsNullOrEmpty(val))
            {
                return 0;
            }
            else
            {
                decimal res = 0;
                bool ok = decimal.TryParse(val, out res);
                return res;
            }
        }

        //Retorna un valor fecha + hora formateado para operar con Sybase.-
        // UPGRADE_INFO (#0561): The 'dbdatetimeSy' symbol was defined without an explicit "As" clause.
        public static dynamic dbdatetimeSy(string Valor)
        {
            return "'" + VB6Helpers.Format(Valor, "yyyy-mm-dd") + " " + VB6Helpers.Format(Valor, "hh:nn:ss") + "'";
        }

        public static string dbPardSy(double Valor)
        {
            return Format.FormatUSA(Valor, "#0.00000000");
        }

        public static string dbPardSyForRead(double Valor)
        {
            return Format.FormatCurrency(Valor, "#0.00000000");
        }

        public static string dbTCamSy(double Valor)
        {
            return Format.FormatUSA(Valor, "#0.0000");
        }

        public static string dbTCamSyForRead(double Valor)
        {
            return Format.FormatCurrency(Valor, "#0.0000");
        }

        

    }
}
