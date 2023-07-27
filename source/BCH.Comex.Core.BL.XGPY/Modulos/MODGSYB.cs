using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class MODGSYB
    {
        public static int SyContador = 0;
        public static string dbcharSy(string Valor)
        {
            string dbcharSy = "";
            string s = "";
            s = Valor;
            if (VB6Helpers.Instr(s, "~") != 0)
            {
                s = UTILES.Componer(s, "~", "|");
            }
            if (VB6Helpers.Instr(s, "'") != 0)
            {
                s = UTILES.Componer(s, "'", "´");
            }
            dbcharSy = "'" + s + "'";
            return dbcharSy;
        }

        public static string dbdatesy(string Valor)
        {
            if (String.IsNullOrEmpty(Valor))
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
            return VB6Helpers.Format(Valor, "yyyy-mm-dd");
        }

        public static dynamic dbdatetimeSy(string Valor)
        {
            return "'" + VB6Helpers.Format(Valor, "yyyy-mm-dd") + " " + VB6Helpers.Format(Valor, "hh:nn:ss") + "'";
        }

        public static string dblogisy(short val)
        {
            if (val == 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public static string dbnumesy(double val)
        {
            return val.ToString();
        }

        public static string dbPardSy(double Valor)
        {
            return Format.FormatCurrency(Valor, "#0.00000000");
        }
        public static string dbTCamSy(double Valor)
        {
            return Format.FormatUSA(Valor, "#0.0000");
        }
        public static double GetPosSy(int Posicion, string Tipo, string Resultado)
        {
            double GetPosSy = 0.0;
            string h = "";
            string hora = "";
            string Fecha = "";
            string f = "";
            string ResultQuery = "";
            ResultQuery = UTILES.copiardestring(Resultado, "~", (short)Posicion);
            if (Tipo == "N")
            {
                GetPosSy = ResultQuery.ToVal();
            }
            else if (Tipo == "C")
            {
                ResultQuery = UTILES.Componer(ResultQuery, "|", "~");
                ResultQuery = UTILES.Componer(ResultQuery, "´", "'");
                ResultQuery = UTILES.Componer(ResultQuery, "¢", "ó");
                ResultQuery = UTILES.Componer(ResultQuery, "§", "º");
                GetPosSy = Convert.ToDouble(ResultQuery);
            }
            else if (Tipo == "F")
            {
                f = UTILES.copiardestring(ResultQuery.Trim(), " ", 1);
                if (MigrationSupport.Utils.Format(f, "dd/mm/yyyy") == "01/01/1900")
                {
                    GetPosSy = Convert.ToDouble(0);
                }
                else
                {
                    GetPosSy = Convert.ToDouble(f);
                }
            }
            else if (Tipo == "H")
            {
                f = UTILES.copiardestring(ResultQuery.Trim(), " ", 2);
                GetPosSy = GetPosSy = Convert.ToDouble(f.Left(8));
            }
            else if (Tipo == "D")
            {
                ResultQuery = ResultQuery.Trim();
                Fecha = UTILES.moverdestring(ref ResultQuery, " ", 1);
                if (ResultQuery.Right(2) == "PM" || ResultQuery.Right(2) == "AM")
                {
                    hora = ResultQuery.Left(8).Trim() + ResultQuery.Right(2);
                }
                else
                {
                    hora = ResultQuery.Left(8).Trim();
                }
                if (MigrationSupport.Utils.Format(Fecha, "yyyymmdd") != MigrationSupport.Utils.Format("01/01/1900", "yyyymmdd"))
                {
                    f = MigrationSupport.Utils.Format(Fecha.GetDate(), "dd/mm/yyyy");
                    h = MigrationSupport.Utils.Format(hora.GetTime(), "hh:nn:ss");
                    GetPosSy = Convert.ToDouble(f + " " + h);
                }
                else
                {
                    GetPosSy = Convert.ToDouble(0);
                }
            }
            else if (Tipo == "f")
            {
                f = ResultQuery;
                if (MigrationSupport.Utils.Format(f, "dd/mm/yyyy") == "01/01/1900")
                {
                    GetPosSy = Convert.ToDouble(0);
                }
                else
                {
                    GetPosSy = Convert.ToDouble(MigrationSupport.Utils.Format(f, "dd/mm/yyyy"));
                }
            }
            else if (Tipo == "L")
            {
                if (ResultQuery.ToVal() == 0)
                {
                    GetPosSy = Convert.ToDouble(false);
                }
                else
                {
                    GetPosSy = Convert.ToDouble(true);
                }
            }
            else if (Tipo == "T")
            {
                f = ResultQuery;
                GetPosSy = Convert.ToDouble(MigrationSupport.Utils.Format(f, "dd/mm/yyyy"));
            }

            return GetPosSy;
        }
        public static int NumIni()
        {
            int NumIni = 0;
            SyContador = 1;
            NumIni = SyContador;
            return NumIni;
        }
        public static int NumSig()
        {
            int NumSig = 0;


            SyContador = SyContador + 1;
            NumSig = SyContador;

            return NumSig;
        }

    }
}
