using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODGMEM
    {
        //Graba un Campo Memo y retorna el código de éste.-
        //Retorno    <> 0    : Retorna el # del Memo.-
        //           =  0    : Error o Grabación no Exitosa.-
        public static int SyPutn_Mem(InitializationObject initObject, UnitOfWorkCext01 unit, string Tabla, int CodMem, string Memo)
        {
            T_MODGRNG MODGRNG = initObject.MODGRNG;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            int _retValue = 0;
            int m = 0;
            short x = 0;
            short i = 0;
           
            short n = 0;
            string[] Lineas = null;
            try
            {
                // IGNORED: On Error GoTo SyPutn_MemErr

                //Si no existe el memo => Se procede a obtener su código.-
                if(CodMem == 0 && !string.IsNullOrEmpty(Memo))
                {
                    string _switchVar1 = VB6Helpers.LCase(Tabla);
                    if (_switchVar1 == "x")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG,MODGUSR,Mdi_Principal,unit, T_MODGRNG.Rng_Memx));
                    }
                    else if (_switchVar1 == "m")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memm));
                    }
                    else if (_switchVar1 == "s")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Mems));
                    }
                    else if (_switchVar1 == "y")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memy));
                    }
                    else if (_switchVar1 == "i")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memi));
                    }
                    else if (_switchVar1 == "p")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memp));
                    }
                    else if (_switchVar1 == "f")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memf));
                    }
                    else if (_switchVar1 == "jm")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memjm));
                    }
                    else if (_switchVar1 == "jd")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memjd));
                    }
                    else if (_switchVar1 == "e")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Meme));
                    }
                    else if (_switchVar1 == "lm")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memlm));
                    }
                    else if (_switchVar1 == "ld")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_Memld));
                    }
                    else if (_switchVar1 == "c")
                    {
                        m = VB6Helpers.CInt(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.RngMemC));
                    }

                    if (m <= 0)
                    {
                        return 0;
                    }
                }

                //Si el Memo existe
                if (CodMem != 0)
                {
                    int resOpe = -1;
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {
                            resOpe = reader.GetInt32(0);
                        }
                        else
                        {
                            resOpe = 9;
                        }
                    }, "sce_memg_d01_MS", Tabla, CodMem.ToString());
                    if (resOpe == 0)
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type=TipoMensaje.Error,
                            Text= "Se ha producido un error al tratar de borrar el campo Memo(Sce_Mem *)."
                        });
                        return 0;
                    }
                    m = CodMem;
                }

                if (m == 0)
                {
                    return 0;
                }

                //Para un string determinado se confecciona un arreglo de lineas.-
                x = VB6Helpers.CShort(GetLineas(Memo, ref Lineas, 255, Tabla));

                //Hace un Put en Sce_Mem.-
               
                for (i = 0; i < Lineas.Length; i++)
                {
                    string parAux = String.Empty;
                    if (Tabla == "s")
                    {
                        parAux = MODGPYF0.Componer(Lineas[i], " ", "*");
                    }
                    else
                    {
                        parAux = Lineas[i];
                    }
                    int resOpe = -1;//verificar lo que viene
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {
                            resOpe = reader.GetInt32(0);
                        }
                        else
                        {
                            resOpe = -1;
                        }
                    }, "sce_memg_i01_MS", Tabla, m.ToString(), (i+1).ToString(), parAux);
                    if (resOpe == -1)
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error al tratar de grabar el Campo Memo (Sce_Mem*)."
                        });
                        return 0;
                    }
                }

                n = (short)unit.SceRepository.EjecutarSP<int>("sce_memg_s03_MS", Tabla, m.ToString()).First();
                //Si el Memo existe
                
                if (n != Lineas.Length)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "El Campo Memo se ha grabado parcialmente. Intente una nueva grabación o Reporte el problema a Sistemas (Memg_s03)."
                    });
                    return 0;
                }
                _retValue = m;
            }
            catch (Exception _ex)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de Validar la Existencia del Campo Memo (Memg_s03)."
                });

                _retValue = 0;
            }
            return _retValue;
        }

        //Retorna un arreglo de Lineas para un string de largo especificado.-
        // UPGRADE_INFO (#0561): The 'GetLineas' symbol was defined without an explicit "As" clause.
        public static dynamic GetLineas(string Dato, ref string[] Arreglo, short largo, string Tabla)
        {
            string s = Dato;
            short n = 0;
            Arreglo = new string[0];
            while(!string.IsNullOrEmpty(s))
            {

                n = (short)(VB6Helpers.UBound(Arreglo) + 1) ;
                VB6Helpers.RedimPreserve(ref Arreglo, 0, n);
                Arreglo[n] = VB6Helpers.Left(s, largo);
                //No Válido para Swift, sólo cartas.-
                if (VB6Helpers.Mid(Arreglo[n], 255, 1) == " " && Tabla != "s")
                {
                    Arreglo[n] = VB6Helpers.Left(Arreglo[n], 254) + "Ç";
                }

                if (VB6Helpers.Len(s) < largo)
                {
                    s = "";
                }
                else
                {
                    s = VB6Helpers.Right(s, VB6Helpers.Len(s) - largo);
                }

            }

            return null;
        }


        //Lee un Campo Memo.-
        //Retorno    <> ""  : Lectura Exitosa.-
        //           =  ""  : Error o Lectura no Exitosa.-
        public static string SyGetn_Mem(InitializationObject initObject,UnitOfWorkCext01 unit, string Tabla, int CodMem)
        {
            string _retValue = "";
            string s = "";

            try
            {
                // IGNORED: On Error GoTo SyGetn_MemErr

                //Si no viene el código del memo => retornar vacío.-
                if (CodMem == 0)
                {
                    return String.Empty;
                }
                var res = unit.SceRepository.EjecutarSP<string>("sce_memg_s01_MS", Tabla, CodMem.ToString());
                //Confeccionar consulta.-
                foreach(string z in res)
                {
                    string aux = z;
                    switch (Tabla)
                    {
                        case "s":
                            aux = MODGPYF0.Componer(z, "*", " ");
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
                    Text= "Se ha producido un error al tratar de leer el Campo Memo (Sce_Mem*).",
                    Type=TipoMensaje.Error
                });            
            }
            return _retValue;
        }
    }
}
