using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODSYGETPRT
    {
        //Lee los datos de los partys faltantes
        public static short SyGet_Prt(ref CdOper Operacion, short NoOperacion, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            #region Inicializacion Variables
            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'Retorno' variable wasn't declared explicitly.
            short Retorno = 0;
            // UPGRADE_INFO (#05B1): The 'GPrt_NoPath' variable wasn't declared explicitly.
            dynamic GPrt_NoPath = null;
            // UPGRADE_INFO (#05B1): The 'GPrt_GetParty' variable wasn't declared explicitly.
            dynamic GPrt_GetParty = null;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'Llave' variable wasn't declared explicitly.
            string Llave = "";
            // UPGRADE_INFO (#05B1): The 'Nome' variable wasn't declared explicitly.
            string Nome = "";
            // UPGRADE_INFO (#05B1): The 'Dire' variable wasn't declared explicitly.
            string Dire = "";
            // UPGRADE_INFO (#05B1): The 'Que' variable wasn't declared explicitly.
            string Que = "";
            // UPGRADE_INFO (#05B1): The 'R' variable wasn't declared explicitly.
            string R = "";
            // UPGRADE_INFO (#05B1): The 'Borrado' variable wasn't declared explicitly.
            short Borrado = 0;
            // UPGRADE_INFO (#05B1): The 'T' variable wasn't declared explicitly.
            short T = 0;
            // UPGRADE_INFO (#05B1): The 'MsgxRet' variable wasn't declared explicitly.
            dynamic MsgxRet = null;
            // UPGRADE_INFO (#05B1): The 'Sentencia' variable wasn't declared explicitly.
            dynamic Sentencia = null;
            // UPGRADE_INFO (#05B1): The 'camps' variable wasn't declared explicitly.
            string camps = "";
            // UPGRADE_INFO (#05B1): The 'dd' variable wasn't declared explicitly.
            string dd = "";
            // UPGRADE_INFO (#05B1): The 'Empresa' variable wasn't declared explicitly.
            string Empresa = "";
            // UPGRADE_INFO (#05B1): The 'Opera' variable wasn't declared explicitly.
            string Opera = "";
            // UPGRADE_INFO (#05B1): The 'RR' variable wasn't declared explicitly.
            short RR = 0;
            // UPGRADE_INFO (#05B1): The 'k' variable wasn't declared explicitly.
            short k = 0;


            #endregion
            using (Tracer tracer = new Tracer("SyGet_Prt"))
            {
                try
                {
                    #region Verifico si debo leer parametros
                    if (initObj.Module1.PrtControl.Leidos == 0)
                    {
                        Retorno = VB6Helpers.CShort(Module1.LeeParametrosParty(initObj.Module1));
                        if (Retorno != 0)
                        {
                            return (short)(true ? -1 : 0);
                        }
                    }
                    #endregion
                    #region Recorrer los PartysOpe viendo cada llave distinta de vacio.
                    for (i = (short)VB6Helpers.LBound(initObj.Module1.PartysOpe); i <= (short)VB6Helpers.UBound(initObj.Module1.PartysOpe); i++)
                    {
                        Llave = VB6Helpers.Trim(initObj.Module1.PartysOpe[i].LlaveArchivo); //Por si es NULL
                        Nome = VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PartysOpe[i].IndNombre));
                        Dire = VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PartysOpe[i].IndDireccion), "00");

                        if (!string.IsNullOrEmpty(Llave))
                        {
                            if (initObj.Module1.PartysOpe[i].Ubicacion == T_Module1.GPrt_EnParty)
                            {
                                #region Datos del Participante.-
                                var ret = unit.SceRepository.pro_sce_prty_s02_MS_1(MODGSYB.dbcharSy(Llave), "1");
                                if (ret != null)
                                {
                                    _retValue = (short)(true ? -1 : 0);
                                    //return _retValue;
                                }
                                else
                                {
                                    throw new ComexUserException("El participante no existe.");
                                }

                                Borrado = (short)(ret.borrado ? -1 : 0);
                                initObj.Module1.PartysOpe[i].TipoParty = (short)ret.tipo_party;
                                initObj.Module1.PartysOpe[i].FlagParty = (short)ret.flag;
                                T = (short)(ret.tiene_rut ? -1 : 0);
                                initObj.Module1.PartysOpe[i].rut = ret.rut;
                                initObj.Module1.PartysOpe[i].CodBanco = ret.cod_bco.ToString();
                                initObj.Module1.PartysOpe[i].Swift = ret.swift;
                                #endregion

                                #region Datos del Nombre del Participante.-
                                //Procedimiento Almacenado generado de forma automatica.
                                var result = unit.SceRepository.pro_sce_prty_s05_MS(MODGSYB.dbcharSy(Llave), Convert.ToInt32(Nome), 1);
                                if (result != null)
                                {
                                    _retValue = (short)(true ? -1 : 0);
                                    //return _retValue;
                                }
                                else
                                {
                                    throw new ComexUserException("El participante no tiene razones sociales asociadas.");
                                }
                                Borrado = Convert.ToSByte(result.borrado);
                                initObj.Module1.PartysOpe[i].NombreUsado = result.razon_soci;
                                #endregion

                                #region Datos de la Direccion del Participante.-
                                //Procedimiento Almacenado generado de forma manual:retorna un solo registro.
                                var result1 = unit.SceRepository.pro_sce_prty_s05_MS_01(MODGSYB.dbcharSy(Llave), Convert.ToInt32(Dire), 2);
                                if (result1 != null)
                                {
                                    _retValue = (short)(true ? -1 : 0);
                                    //return _retValue;
                                }
                                else
                                {
                                    throw new ComexUserException("El participante no tiene direcciones asociadas.");
                                }

                                var tmpModule1 = new List<T_Module1>();

                                //Inicializar clase
                                Borrado = Convert.ToSByte(result1.borrado);
                                initObj.Module1.PartysOpe[i].DireccionUsado = result1.direccion;
                                initObj.Module1.PartysOpe[i].ComunaUsado = result1.comuna;
                                initObj.Module1.PartysOpe[i].PostalUsado = result1.cod_postal;
                                initObj.Module1.PartysOpe[i].EstadoUsado = result1.estado;
                                initObj.Module1.PartysOpe[i].CiudadUsado = result1.ciudad;
                                initObj.Module1.PartysOpe[i].PaisUsado = result1.pais;
                                initObj.Module1.PartysOpe[i].CodPais = (short)result1.cod_pais;
                                initObj.Module1.PartysOpe[i].Telefono = VB6Helpers.Format(result1.telefono, "0");
                                initObj.Module1.PartysOpe[i].Fax = VB6Helpers.Format(result1.fax, "0");
                                initObj.Module1.PartysOpe[i].Telex = result1.telex;
                                initObj.Module1.PartysOpe[i].Enviara = (short)result1.envio_sce;
                                initObj.Module1.PartysOpe[i].CasPostal = result1.cas_postal;
                                initObj.Module1.PartysOpe[i].CasBanco = result1.cas_banco;

                                if (VB6Helpers.Val(initObj.Module1.PartysOpe[i].Telefono) == 0)
                                {
                                    initObj.Module1.PartysOpe[i].Telefono = "";
                                }
                                if (VB6Helpers.Val(initObj.Module1.PartysOpe[i].Fax) == 0)
                                {
                                    initObj.Module1.PartysOpe[i].Fax = "";
                                }

                                _retValue = (short)(true ? -1 : 0);
                                #endregion
                            }
                            else
                            {
                                #region En operacion
                                //Procedimiento Almacenado generado de forma automatica.
                                Empresa = VB6Helpers.Left(Operacion.Id_Empresa, 2);
                                Opera = VB6Helpers.Right(Operacion.Id_Empresa, 1) + Operacion.Id_Operacion;

                                var result2 = unit.SceRepository.pro_sce_prty_s03_MS(
                                           MODGSYB.dbcharSy(Operacion.Cent_Costo),
                                           MODGSYB.dbcharSy(Operacion.Id_Product),
                                           MODGSYB.dbcharSy(Operacion.Id_Especia),
                                           MODGSYB.dbcharSy(Operacion.Id_Empresa),
                                           MODGSYB.dbcharSy(Operacion.Id_Operacion),
                                           MODGSYB.dbcharSy(VB6Helpers.Format(VB6Helpers.CStr(i), "00")),
                                           1
                                );

                                if (result2 == null)
                                {

                                    RR = VB6Helpers.CShort(MODGSYB.GetPosSy(MODGSYB.NumIni(), "L", R));
                                    if (RR != 0)
                                    {
                                        initObj.Module1.PartysOpe[i].TipoParty = T_Module1.GPrt_TipoBcoOperacion;  //bco operacion
                                        initObj.Module1.PartysOpe[i].rut = "";
                                        initObj.Module1.PartysOpe[i].Swift = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                                    }
                                    else
                                    {
                                        initObj.Module1.PartysOpe[i].TipoParty = T_Module1.GPrt_TipoEnOperacion;  //en operacion
                                        initObj.Module1.PartysOpe[i].rut = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                                        initObj.Module1.PartysOpe[i].Swift = "";
                                    }
                                    //initObj.Module1.PartysOpe[i].NombreUsado = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                                    initObj.Module1.PartysOpe[i].DireccionUsado = result2.direccion;
                                    initObj.Module1.PartysOpe[i].ComunaUsado = result2.comuna;
                                    initObj.Module1.PartysOpe[i].EstadoUsado = result2.estado;
                                    initObj.Module1.PartysOpe[i].CiudadUsado = result2.ciudad;
                                    initObj.Module1.PartysOpe[i].PaisUsado = result2.pais;
                                    initObj.Module1.PartysOpe[i].CodPais = (short)result2.cod_pais;
                                    initObj.Module1.PartysOpe[i].PostalUsado = result2.cod_postal;
                                    initObj.Module1.PartysOpe[i].Telefono = result2.telefono;
                                    initObj.Module1.PartysOpe[i].Fax = result2.fax;
                                    initObj.Module1.PartysOpe[i].Telex = result2.telex;
                                    //initObj.Module1.PartysOpe[i].Enviara =result2.envio_sce;
                                    initObj.Module1.PartysOpe[i].CasPostal = result2.cas_postal;
                                    initObj.Module1.PartysOpe[i].CasBanco = result2.cas_banco;

                                    #region Llenar estructura con partys pegados a la operación
                                    k = MODXANU.Nuevo_Pope(initObj);
                                    initObj.Module1.PopeOpe[k].Status = T_Module1.GPrt_StatIntacto;
                                    initObj.Module1.PopeOpe[k].Secuencia = i;
                                    initObj.Module1.PopeOpe[k].EsBanco = RR;
                                    if (RR != 0)
                                    {
                                        initObj.Module1.PopeOpe[k].RutSwift = initObj.Module1.PartysOpe[i].Swift;
                                    }
                                    else
                                    {
                                        initObj.Module1.PopeOpe[k].RutSwift = initObj.Module1.PartysOpe[i].rut;
                                    }

                                    initObj.Module1.PopeOpe[k].Nombre = initObj.Module1.PartysOpe[i].NombreUsado;
                                    initObj.Module1.PopeOpe[k].Direccion = initObj.Module1.PartysOpe[i].DireccionUsado;
                                    initObj.Module1.PopeOpe[k].comuna = initObj.Module1.PartysOpe[i].ComunaUsado;
                                    initObj.Module1.PopeOpe[k].Ciudad = initObj.Module1.PartysOpe[i].CiudadUsado;
                                    initObj.Module1.PopeOpe[k].estado = initObj.Module1.PartysOpe[i].EstadoUsado;
                                    initObj.Module1.PopeOpe[k].Pais = initObj.Module1.PartysOpe[i].PaisUsado;
                                    initObj.Module1.PopeOpe[k].CodPais = initObj.Module1.PartysOpe[i].CodPais;
                                    initObj.Module1.PopeOpe[k].Postal = initObj.Module1.PartysOpe[i].PostalUsado;
                                    initObj.Module1.PopeOpe[k].Telefono = initObj.Module1.PartysOpe[i].Telefono;
                                    initObj.Module1.PopeOpe[k].Fax = initObj.Module1.PartysOpe[i].Fax;
                                    initObj.Module1.PopeOpe[k].Telex = initObj.Module1.PartysOpe[i].Telex;
                                    initObj.Module1.PopeOpe[k].CasPostal = initObj.Module1.PartysOpe[i].CasPostal;
                                    initObj.Module1.PopeOpe[k].CasBanco = initObj.Module1.PartysOpe[i].CasBanco;
                                    #endregion
                                }
                                else
                                {
                                    return (short)(true ? -1 : 0);
                                }

                                #endregion
                            }
                        }
                    }
                    #endregion
                }
                catch (ComexUserException ex)
                {
                    tracer.TraceException("Alerta al leer datos de los partys faltantes, ComexUserException", ex);
                    #region Excepcion
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Error al leer los datos del Participante: " + ex.Message,
                        Title = T_MODGCVD.MsgCVD
                    });
                    #endregion
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta al leer datos de los partys faltantes", ex);
                    #region Excepcion
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Error al leer los datos del Participante: " + ex,
                        Title = T_MODGCVD.MsgCVD
                    });
                    #endregion
                }
                _retValue = (short)(false ? -1 : 0); 
            }
            return _retValue;
        }
    }
}
