using BCH.Comex.Core.Entities.Custodia.ControlIntegral.T_Modulos;

namespace BCH.Comex.Core.BL.CONTROLINTEGRAL.Modulos
{
    public class ModFunc
    {

        public static T_ModFunc getModFunc()
        {
            return new T_ModFunc();
        }        

        //public static int Verificar_Rut(string rut)
        //{
        //    int esrut = 0;

        //    string DvCal = "";

        //    const string Son = "1234567890K";
        //    string a = "";
        //    int i = 0;
        //    string b = "";
        //    string dvrut = "";
        //    int aa = 0;
        //    int suma = 0;
        //    int es = 0;

        //    // limpiar el Rut
        //    for (i = 1; i <= rut.Len(); i += 1)
        //    {
        //        a = rut.Mid(i, 1);
        //        if (a == "k")
        //        {
        //            a = "K";
        //        }
        //        if (Son.InStr(a, 1, StringComparison.CurrentCulture) > 0)
        //        {
        //            b = b + a;
        //        }
        //    }

        //    dvrut = b.Right(1);
        //    b = b.Left((b.Len() - 1));

        //    for (i = 1; i <= b.Len(); i += 1)
        //    {
        //        a = b.Right(i);
        //        aa = (a.Left(1)).ToInt();

        //        if (i < 7)
        //        {
        //            suma = suma + aa * (i + 1);
        //        }
        //        else
        //        {
        //            suma = suma + aa * (i - 5);
        //        }
        //    }

        //    es = 11 - suma % 11;
        //    switch (es)
        //    {
        //        case 11:
        //            DvCal = "0";
        //            break;
        //        case 10:
        //            DvCal = "K";
        //            break;
        //        default:
        //            DvCal = string.Format(String.Empty,es);
        //            break;
        //    }

        //    esrut = 0;
        //    if (DvCal == dvrut)
        //    {
        //        esrut = -1;
        //    }

        //    return esrut;
        //}

        //public static string Verificar_Rut(string nums)
        //{
        //    short i = 0;
        //    // UPGRADE_INFO (#0561): The 'suma' symbol was defined without an explicit "As" clause.
        //    dynamic suma = null;
        //    // UPGRADE_INFO (#0561): The 'digito' symbol was defined without an explicit "As" clause.
        //    dynamic digito = null;
        //    short Resultado = 0;
        //    // UPGRADE_INFO (#0561): The 'cuales' symbol was defined without an explicit "As" clause.
        //    const string cuales = "765432";

        //    for (i = 1; i <= 6; i++)
        //    {
        //        suma = Format.StringToDouble(suma) + (VB6Helpers.Val(VB6Helpers.Mid(nums, i, 1)) * VB6Helpers.Val(VB6Helpers.Mid(cuales, i, 1)));
        //    }

        //    Resultado = (short)(Format.StringToDouble(suma) % 11);

        //    digito = 11 - Resultado;

        //    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'digito'. Consider using the GetDefaultMember6 helper method.
        //    if (Format.StringToDouble(digito) == 11)
        //    {
        //        return "00";
        //    }
        //    else
        //    {
        //        return string.Format("00",digito);
        //    }

        //}

        public static string Verificar_Rut(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }



    }
}
