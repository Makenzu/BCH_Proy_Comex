using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGTAB1
    {
        
        public static T_MODGTAB1 GetMODGTAB1()
        {
            return new T_MODGTAB1();
        }

        public static short Get_VPbc(short Codigo, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short n = 0;
            short i = 0;
            short X = 0;

            n = (short)VB6Helpers.UBound(initObj.MODGTAB1.VPbc);
            if (n == 0)
            {
                X = SyGetn_Pbc(initObj.MODGTAB1, unit);
            }

            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGTAB1.VPbc); i++)
            {
                if (initObj.MODGTAB1.VPbc[i].Pbc_PbcCod == Codigo)
                {
                    return i;
                }

            }

            return 0;
        }

        public static short SyGetn_Pbc(T_MODGTAB1 MODGTAB1, UnitOfWorkCext01 unit)
        {
            short _retValue;
            const string cacheKey = "VPbcCache";
            var cache = MemoryCache.Default;
            try
            {
                if (!cache.Contains(cacheKey))
                {
                    var result = unit.SgtRepository.EjecutarSP<sgt_pbc_s01_MS_Result>("sgt_pbc_s01_MS").Select(x => new T_Pbc()
                    {
                        Pbc_PbcCod = (short)x.pbc_pbccod,
                        Pbc_PbcDes = x.pbc_pbcdes
                    }).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                }
                MODGTAB1.VPbc = cache[cacheKey] as T_Pbc[];
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //Retorna Paridad o Tipo de Cambio según moneda y período (mm-yy).
        public static double SyGet_Vmc(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, short CodMnd, string Fecha, string ParTC)
        {
            double _retValue = 0;
            short mes = 0;
            short año = 0;
            short Mes_Anterior = 0;
            string Que = "";
            string R = "";
            double p = 0;
            double T = 0;
            string Fecha_Paridad = "";

            try
            {
                mes = VB6Helpers.Month(VB6Helpers.CDate(Fecha));
                año = VB6Helpers.Year(VB6Helpers.CDate(Fecha));
                Mes_Anterior = (short)(mes - 1);
                if (Mes_Anterior == 0)
                {
                    Mes_Anterior = 12;
                    año = (short)(año - 1);
                }

                //Hace la llamada para el recate del último día hábil del mes.
                Fecha_Paridad = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Fn_Calcula_Dia_Habil(MODGTAB0, unit, VB6Helpers.Trim(VB6Helpers.Str(año)), VB6Helpers.Trim(VB6Helpers.Str(Mes_Anterior)));
                sgt_vmc_s01_MS_Result res = unit.SgtRepository.EjecutarSP<sgt_vmc_s01_MS_Result>("sgt_vmc_s01_MS", CodMnd.ToString(), Fecha_Paridad).FirstOrDefault();
                if (res != null)
                {
                    if (ParTC.Equals("P"))
                    {
                        _retValue = (double)res.vmc_vmcprc;
                    }
                    else if (ParTC.Equals("T"))
                    {
                        _retValue = (double)res.vmc_vmctca;
                    }
                    else
                    {
                        _retValue = 0;
                    }
                }
                else
                {
                    _retValue = 0;
                }
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }

            return _retValue;
        }

        //Retorno > 0 : Indice en VAdn del Código Aduana.-
        //Retorno = 0 : No se encontró el  Código Aduana.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VAdn(T_MODGTAB1 MODGTAB1, UnitOfWorkCext01 unit, short Codigo)
        {
            short n = 0;
            short i = 0;
            short X = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB1.VAdn);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            //if (n == 0)
            if (n == -1)
            {
                X = SyGetn_Adn(MODGTAB1, unit);
            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB1.VAdn); i++)
            {
                if (MODGTAB1.VAdn[i].CodAdn == Codigo)
                {
                    return i;
                }

            }

            return -1;
        }

        //Tabla de Aduana
        //Retorno    <> ""  : Lectura Exitosa.-
        //           =  ""  : Error o Lectura no Exitosa.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Adn(T_MODGTAB1 MODGTAB1, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            short n = 0;
            try
            {
                n = (short)VB6Helpers.UBound(MODGTAB1.VAdn);
                if (n > 0)
                {
                    return -1;
                }
                MODGTAB1.VAdn = unit.SceRepository.EjecutarSP<sce_adn_s01_MS_Result>("sce_adn_s01_MS").Select(x => new T_Adn()
                {
                    CodAdn = (short)x.codadn,
                    NomAdn = x.nomadn
                }).ToArray();
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //Retorno > 0 : Indice en VPbc del Código Plaza Banco Central.-
        //Retorno = 0 : No se encontró el  Código Plaza Banco Central.-
        public static short Get_VPbc(InitializationObject initObj, UnitOfWorkCext01 unit, short Codigo)
        {
            short n = 0;
            short i = 0;
            short X = 0;

            n = (short)VB6Helpers.UBound(initObj.MODGTAB1.VPbc);
            if (n < 0)
            {
                X = SyGetn_Pbc(initObj.MODGTAB1, unit);
            }
            //Joel Fernandez: CAmbie que i = 0
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGTAB1.VPbc); i++)
            {
                if (initObj.MODGTAB1.VPbc[i].Pbc_PbcCod == Codigo)
                {
                    return i;
                }
            }
            return -1;
        }        

        //****************************************************************************
        //   1.  Recorre el Arreglo de Conceptos de Planillas por un Código determinado
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VTcp(T_MODGTAB1 MODGTAB1,UnitOfWorkCext01 unit, string Codigo)
        {
            short n = 0;
            short i = 0;
            short X = 0;
            //Verifica lectura previa.-
            
            n = (short)VB6Helpers.UBound(MODGTAB1.VTcp);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n < 0)
            {
                X = SyGetn_Tcp(MODGTAB1,unit );
            }

            //Se busca el código del concepto.-
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB1.VTcp); i++)
            {
                if (MODGTAB1.VTcp[i].CodTcp.Equals(Codigo))
                {
                    return i;
                }

            }

            return 0;
        }


        //Tabla de Conceptos de Planilla
        //Retorno    <> ""  : Lectura Exitosa.
        //           =  ""  : Error o Lectura no Exitosa.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Tcp(T_MODGTAB1 MODGTAB1, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
            short _retValue = 0;
            string R = "";
            short n = 0;
            string Que = "";
            short i = 0;
            n = (short)VB6Helpers.UBound(MODGTAB1.VTcp);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n > 0)
            {
                _retValue = (short)(true ? -1 : 0);
            }
            else
            {
                try
                {
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        var aux = new List<T_Tcp>();
                        while (reader.Read())
                        {
                            T_Tcp VTcp = new T_Tcp();
                            VTcp.CodTcp = reader.GetString(0);
                            VTcp.CodOci = short.Parse(reader.GetString(1));
                            VTcp.DesTcp = reader.GetString(2);
                            VTcp.Tcpimp = (short)reader.GetInt32(3);
                            VTcp.Tcpexp = (short)reader.GetInt32(4);
                            VTcp.Tcpcam = (short)reader.GetInt32(5);
                            VTcp.Tcpzpr = (short)reader.GetInt32(6);
                            VTcp.Tcpvis = (short)reader.GetInt32(7);
                            VTcp.TcpInv = (short)reader.GetInt32(8);
                            VTcp.TcpCom = (short)reader.GetInt32(9);
                            VTcp.TcpArb = (short)reader.GetInt32(10);
                            VTcp.TcpDec = (short)(reader.GetBoolean(11) ? -1 : 0);
                            VTcp.TcpPai = (short)(reader.GetBoolean(12) ? -1 : 0);
                            VTcp.TcpCon = (short)(reader.GetBoolean(12) ? -1 : 0);
                            VTcp.TcpAut = (short)(reader.GetBoolean(13) ? -1 : 0);
                            VTcp.TcpOp1 = (short)(reader.GetBoolean(14) ? -1 : 0);
                            VTcp.TcpOp2 = (short)(reader.GetInt32(15));
                            VTcp.TcpOp3 = (short)(reader.GetInt32(16));
                            VTcp.TcpOp4 = (short)(reader.GetInt32(17));
                            aux.Add(VTcp);
                        }
                        MODGTAB1.VTcp = aux.ToArray();

                    }, "sce_tcp_s01_MS");
                    _retValue = -1;
                }
                    catch (Exception e)
                {
                        tracer.TraceException("Alerta", e);

                    _retValue = 0;
                }
            }
            return _retValue;
        }
        }

        //Retorna Paridad o Tipo de Cambio según moneda y período (mm-yy) de Declaración.-
        public static double SyGet_Vmf(InitializationObject initObject, UnitOfWorkCext01 unit, short CodMnd, string Fecha, string ParTC)
        {
            using (var trace = new Tracer("SyGet_Vmf: Retorna Paridad o Tipo de Cambio según moneda y período (mm-yy) de Declaración"))
            {
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                double _retValue = 0;
                short mes = 0;
                short año = 0;
                short Mes_Anterior = 0;
                string Fecha_Paridad = "";
                double p = 0;
                double T = 0;
                try
                {
                    // IGNORED: On Error GoTo SyGet_VmfErr

                    mes = VB6Helpers.Month(VB6Helpers.CDate(Fecha));
                    año = VB6Helpers.Year(VB6Helpers.CDate(Fecha));
                    Mes_Anterior = (short)(mes - 1);
                    if (Mes_Anterior == 0)
                    {
                        Mes_Anterior = 12;
                        año = (short)(año - 1);
                    }

                    //Hace la llamada para el recate del último día hábil del mes.
                    trace.TraceInformation("Datos para Fn_Penultimo_Dia_Habil: Año: {0}, Mes: {1}", año, Mes_Anterior);
                    Fecha_Paridad = MODGTAB0.Fn_Penultimo_Dia_Habil(initObject, unit, VB6Helpers.Trim(VB6Helpers.Str(año)), VB6Helpers.Trim(VB6Helpers.Str(Mes_Anterior)));

                    trace.TraceInformation("Datos para sgt_vmd_s02_MS_Result: Fecha_Paridad: {0}, CodMnd: {1}", Fecha_Paridad, CodMnd);
                    List<sgt_vmd_s02_MS_Result> aux = unit.SgtRepository.EjecutarSP<sgt_vmd_s02_MS_Result>("sgt_vmd_s02_MS", MODGSYB.dbdatesy(Fecha_Paridad), CodMnd.ToString());
                    if (aux.Count == 0)
                    {
                        return 0;
                    }
                    sgt_vmd_s02_MS_Result res = aux.First();
                    //Se encontraron los datos.

                    p = (double)res.vmd_vmdprd;
                    T = (double)res.vmd_vmdobs;

                    switch (ParTC)
                    {
                        case "P":
                            _retValue = p;
                            break;
                        case "T":
                            _retValue = T;
                            break;
                    }
                }
                catch (Exception _ex)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de leer la Tabla de Paridades Mensuales (Sgt_Vmc).  Reporte este problema.",
                        Type = TipoMensaje.Error
                    });
                    trace.TraceException("Alerta", _ex);
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Deja el arreglo de Plazas del Banco Central VPbc() en una lista.-
        //public static void CargaEnListaPbc(dynamic Lista, InitializationObject initObject)
        public static void CargaEnListaPbc(UI_Combo combo, InitializationObject initObject)
        {
            short X = (short)VB6Helpers.UBound(initObject.MODGTAB1.VPbc);
            
            combo.Items = initObject.MODGTAB1.VPbc.Select(x => new UI_ComboItem() 
            {
                ID = x.Pbc_PbcCod.ToString(),
                Value = x.Pbc_PbcDes,
                Data = (int)x.Pbc_PbcCod,
            }).ToList();

            #region Codigo Antiguo
            /*
            short n = 0;
            short i = 0;

            
            n = (short)VB6Helpers.UBound(initObject.MODGTAB1.VPbc);
            
            for (i = 1; i <= (short)n; i++)
            {
                //VB6Helpers.Invoke(VB6Helpers.CObj(Lista), "AddItem", initObject.MODGTAB1.VPbc[i].Pbc_PbcDes.value);
                VB6Helpers.Invoke(VB6Helpers.CObj(Lista), "AddItem", initObject.MODGTAB1.VPbc[i].Pbc_PbcDes);
                VB6Helpers.Set(VB6Helpers.CObj(Lista), "ItemData", VB6Helpers.Invoke(VB6Helpers.CObj(Lista), "NewIndex"), initObject.MODGTAB1.VPbc[i].Pbc_PbcCod);
            }
            */
            #endregion
        }
    }
}
