//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public static class MODGMEM
    {

        public static string SyGetn_Mem(InitializationObject initObject, UnitOfWorkCext01 unit, string Tabla, int CodMem)
        {
            string _retValue = "";
            string s = "";

            try
            {              
                //Si no viene el código del memo => retornar vacío.-
                if (CodMem == 0)
                {
                    return String.Empty;
                }
                var res = unit.SceRepository.EjecutarSP<string>("sce_memg_s01_MS", Tabla, CodMem.ToString());
                //Confeccionar consulta.-
                foreach (string z in res)
                {
                    string aux = z;
                    switch (Tabla)
                    {
                        case "s":
                            aux = UTILES.Componer(z, "*", " ");
                            break;
                        case "p":
                            break;
                        default:
                            if (VB6Helpers.Mid(z, 255, 1) == "Ç")
                            {
                                aux = VB6Helpers.Left(z, 254) + " ";
                            }

                            break;
                    }
                    s += aux;
                }
                _retValue = s;
            }
            catch (Exception _ex)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = "Se ha producido un error al tratar de leer el Campo Memo (Sce_Mem*).",
                    Type = TipoMensaje.Error
                });
            }
            return _retValue;
        }


        public static short Puntos(string Numero_Str)
        {
            short Contador = 0;
            short i = 0;

            if (!String.IsNullOrEmpty(Numero_Str))
            {
                for (i = 0; i < Numero_Str.Length; i++)
                {
                    if (Numero_Str[i] == '.')
                    {
                        Contador = (short)(Contador + 1);
                    }

                }
            }

            return Contador;
        }

        // Graba un Campo Memo y retorna el código de éste.-
        // Retorno    <> 0    : Retorna el # del Memo.-
        //            =  0    : Error o Grabación no Exitosa.-
        public static int SyPutn_Mem(String Tabla, Int32 CodMem, String Memo, ref InitializationObject iO, XgpyService xS)
        {
            bool HayError = false;
            int m = 0;

            string[] Lineas = null;
            try
            {
                // Si no existe el memo => Se procede a obtener su código.-
                if (CodMem == 0 && !String.IsNullOrEmpty(Memo))
                {
                    switch (Tabla.ToLower())
                    {
                        case "x":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memx, iO, xS).ToInt();
                            break;
                        case "m":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memm, iO, xS).ToInt();
                            break;
                        case "s":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Mems, iO, xS).ToInt();
                            break;
                        case "y":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memy, iO, xS).ToInt();
                            break;
                        case "i":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memi, iO, xS).ToInt();
                            break;
                        case "p":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memp, iO, xS).ToInt();
                            break;
                        case "f":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memf, iO, xS).ToInt();
                            break;
                        case "jm":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memjm, iO, xS).ToInt();
                            break;
                        case "jd":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memjd, iO, xS).ToInt();
                            break;
                        case "e":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Meme, iO, xS).ToInt();
                            break;
                        case "lm":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memlm, iO, xS).ToInt();
                            break;
                        case "ld":
                            m = MODGRNG.LeeSceRng(MODGRNG.Rng_Memld, iO, xS).ToInt();
                            break;
                    }
                    if (m <= 0)
                    {
                        return 0;
                    }
                }

                // Si el Memo existe
                if (CodMem != 0)
                {
                    var resSceMemg = xS.Sce_Memg_D01_MS(Tabla, Convert.ToString(CodMem));
                    if (resSceMemg.Column1 == -1) {
                        return 0;
                    }
                    m = CodMem;
                }
                if (m == 0)
                {
                    return 0;
                }

                // Para un string determinado se confecciona un arreglo de lineas.-
                GetLineas(Memo, ref Lineas, 255, Tabla);

                // Hace un Put en Sce_Mem.-
                HayError = false;
                for (int i = 1; i < Lineas.Count(); i++)
                {
                    String linea;
                    if (Tabla == "s")
                    {
                        linea = UTILES.Componer(Lineas[i], " ", "*");
                    }
                    else
                    {
                        linea = Lineas[i];
                    }
                    var resSceMemg = xS.Sce_Memg_I01_MS(Tabla, Convert.ToString(m), Convert.ToString(i), linea);
                    if (resSceMemg.Column1 == -1)
                    {
                        return 0;
                    }
                }

                var resSceMemgS03 = xS.Sce_Memg_S03_MS(Tabla, Convert.ToString(m));
                if(!resSceMemgS03.HasValue)
                {
                    return 0;
                }
                if (resSceMemgS03.Value != Lineas.GetUpperBound(0))
                {
                    return 0;
                }

                if (!HayError)
                {
                    return m;
                }

                return 0;

            } catch (Exception ex){
                throw ex;
            }

            return 0;
        }

        public static void GetLineas(string Dato, ref string[] Arreglo, int largo, string Tabla)
        {
            int n = 0;
            string s = "";

            Arreglo = new string[1];
            s = Dato;
            while (s != "")
            {
                n = Arreglo.GetUpperBound(0) + 1;
                Array.Resize(ref Arreglo, n + 1);
                Arreglo[n] = s.Left(largo);
                // No Válido para Swift, sólo cartas.-
                if (Arreglo[n].Mid(255, 1) == " " && Tabla != "s")
                {
                    Arreglo[n] = Arreglo[n].Left(254) + "Ç";
                }
                if (s.Len() < largo)
                {
                    s = "";
                }
                else
                {
                    s = s.Right((s.Len() - largo));
                }
            }

        }

        public static int SyPutn_Mem_New(String Tabla, Int32 CodMem, String Memo, ref InitializationObject iO, XgpyService xS)
        {
            bool HayError = false;
            int m = 0;

            string[] Lineas = null;
            try
            {
                // Si no existe el memo => Se procede a obtener su código.-
                if (CodMem == 0 && !String.IsNullOrEmpty(Memo))
                {
                    switch (Tabla.ToLower())
                    {
                        case "x":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memx, iO, xS).ToInt();
                            break;
                        case "m":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memm, iO, xS).ToInt();
                            break;
                        case "s":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Mems, iO, xS).ToInt();
                            break;
                        case "y":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memy, iO, xS).ToInt();
                            break;
                        case "i":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memi, iO, xS).ToInt();
                            break;
                        case "p":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memp, iO, xS).ToInt();
                            break;
                        case "f":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memf, iO, xS).ToInt();
                            break;
                        case "jm":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memjm, iO, xS).ToInt();
                            break;
                        case "jd":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memjd, iO, xS).ToInt();
                            break;
                        case "e":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Meme, iO, xS).ToInt();
                            break;
                        case "lm":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memlm, iO, xS).ToInt();
                            break;
                        case "ld":
                            m = MODGRNG.LeeSceRng_New(MODGRNG.Rng_Memld, iO, xS).ToInt();
                            break;
                    }
                    if (m <= 0)
                    {
                        return m;
                    }
                }

                // Si el Memo existe
                if (CodMem != 0)
                {
                    var resSceMemg = xS.Sce_Memg_D01_MS(Tabla, Convert.ToString(CodMem));
                    if (resSceMemg.Column1 == -1)
                    {
                        return 0;
                    }
                    m = CodMem;
                }
                if (m == 0)
                {
                    return 0;
                }

                // Para un string determinado se confecciona un arreglo de lineas.-
                GetLineas(Memo, ref Lineas, 255, Tabla);

                // Hace un Put en Sce_Mem.-
                HayError = false;
                for (int i = 1; i < Lineas.Count(); i++)
                {
                    String linea;
                    if (Tabla == "s")
                    {
                        linea = UTILES.Componer(Lineas[i], " ", "*");
                    }
                    else
                    {
                        linea = Lineas[i];
                    }
                    var resSceMemg = xS.Sce_Memg_I01_MS(Tabla, Convert.ToString(m), Convert.ToString(i), linea);
                    if (resSceMemg.Column1 == -1)
                    {
                        return 0;
                    }
                }

                var resSceMemgS03 = xS.Sce_Memg_S03_MS(Tabla, Convert.ToString(m));
                if (!resSceMemgS03.HasValue)
                {
                    return 0;
                }
                if (resSceMemgS03.Value != Lineas.GetUpperBound(0))
                {
                    return 0;
                }

                if (!HayError)
                {
                    return m;
                }

                return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
    }
}
