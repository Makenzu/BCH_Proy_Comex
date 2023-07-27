using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGRNG
    {
        public static T_MODGRNG GetMODGRNG()
        {
            return new T_MODGRNG();
        }

        //Retorna la estructura Codop con la siguiente Operación.
        //True : Exitoso.
        // UPGRADE_INFO (#0561): The 'RngPro' symbol was defined without an explicit "As" clause.
        public static short SgteNumOpr(T_MODGRNG MODGRNG, T_Module1 Module1, T_MODGUSR MODGUSR,
            UI_Mdi_Principal Mdi_Principal, UnitOfWorkCext01 unit, dynamic RngPro, string RngDoc, bool actualizar = false, int? numMinRequerido = null)
        {
            using (var tracer = new Tracer("NuevaOperacion"))
            {
                short _retValue = 0;
                short a = 0;
                short k = 0;
                short i = 0;
                double q = 0;
                //Si la Codop no está limpia, => Número de Operación actual.
                if (RngDoc == T_Mdl_Funciones_Varias.cod_producto && !String.IsNullOrEmpty(Module1.Codop_FT.Id_Operacion) && !actualizar)
                {
                    Module1.Codop.Cent_Costo = MODGUSR.UsrEsp.CentroCosto;
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'RngPro'. Consider using the GetDefaultMember6 helper method.
                    Module1.Codop.Id_Product = VB6Helpers.CStr(RngPro);
                    Module1.Codop.Id_Especia = MODGUSR.UsrEsp.Especialista;
                    Module1.Codop.Id_Operacion = Module1.Codop_FT.Id_Operacion.PadLeft(5, '0');
                    return (short)(true ? -1 : 0);
                }
                else if (RngDoc == T_Mdl_Funciones_Varias.cod_producto_CVD && !String.IsNullOrEmpty(Module1.Codop_CVD.Id_Operacion) && !actualizar)
                {
                    Module1.Codop.Cent_Costo = MODGUSR.UsrEsp.CentroCosto;
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'RngPro'. Consider using the GetDefaultMember6 helper method.
                    Module1.Codop.Id_Product = VB6Helpers.CStr(RngPro);
                    Module1.Codop.Id_Especia = MODGUSR.UsrEsp.Especialista;
                    Module1.Codop.Id_Operacion = Module1.Codop_CVD.Id_Operacion.PadLeft(5, '0');
                    return (short)(true ? -1 : 0);
                }

                //Inicializa variables de Rangos.
                a = SyGetn_xTrng(MODGRNG, unit);

                k = -1;
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGRNG.RngDocs); i++)
                {
                    if (MODGRNG.RngDocs[i].DocCod == RngDoc)
                    {
                        k = i;
                        break;
                    }

                }

                if (k == -1)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se conoce documento sobre el cual se pide el número." });
                    return _retValue;
                }

                MODGRNG.RngGeneral = MODGRNG.RngDocs[k].DocGen;
                MODGRNG.VRng.RngDoc = MODGRNG.RngDocs[k].DocCod;
                if (MODGRNG.RngGeneral != 0)
                {
                    MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                    MODGRNG.VRng.RngEsp = "00";
                    MODGRNG.VRng.RngRut = "0000000000";
                }
                else
                {
                    MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                    MODGRNG.VRng.RngEsp = MODGUSR.UsrEsp.Especialista;
                    MODGRNG.VRng.RngRut = MODGUSR.UsrEsp.Rut;
                }

                //Actualización y lectura de rango seleccionado.-
                var mensajeDeBD = string.Empty;
                try
                {
                    q = SyGetUpd_Rng(MODGRNG, unit, out mensajeDeBD, numMinRequerido);
                }
                catch (Exception ex)
                {
                    throw new ComexUserException("Error al obtener el próximo numero de operación. Detalles: " + ex.Message, ex);
                }

                if (q > 0)
                {
                    Module1.Codop.Cent_Costo = MODGUSR.UsrEsp.CentroCosto;
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'RngPro'. Consider using the GetDefaultMember6 helper method.
                    Module1.Codop.Id_Product = VB6Helpers.CStr(RngPro);
                    Module1.Codop.Id_Especia = MODGUSR.UsrEsp.Especialista;
                    Module1.Codop.Id_Operacion = q.ToString("00000");
                    return (short)(true ? -1 : 0);
                }
                else
                {
                    throw new ComexUserException(mensajeDeBD);
                }

                return _retValue;
            }
        }

        //Lee las Listado de Números de Operaciones.
        //Retorno    <> ""  : Lectura Exitosa.
        //           =  ""  : Error o Lectura no Exitosa.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_xTrng(T_MODGRNG MODGRNG, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            short n = 0;
            n = (short)VB6Helpers.UBound(MODGRNG.RngDocs);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n >= 0)
            {
                _retValue = (short)(true ? -1 : 0);
            }
            else
            {

                MODGRNG.RngDocs = unit.SceRepository.EjecutarSP<sce_trng_s01_MS_Result>("sce_trng_s01_MS").Select(x => new Type_RngDoc()
                {
                    DocCod = x.codnope,
                    DocGen = (short)x.grlnope
                }).ToArray();

            }
            return _retValue;
        }


        //Lee y actualiza los números de Rangos.
        public static double SyGetUpd_Rng(T_MODGRNG MODGRNG, UnitOfWorkCext01 unit, out string mensaje, int? minNumRequerido = null)
        {
            var codcct = MODGRNG.VRng.RngCct;
            var codesp = MODGRNG.VRng.RngEsp;
            var codfun = MODGRNG.VRng.RngDoc;

            return unit.SceRepository.sce_rng_u01_MS(codcct, codesp, codfun, out mensaje, minNumRequerido);
        }


        //Rescata el máximo número de operación de un documento *ya ingresado* con respecto al centro
        //de costo, producto y especialista.-
        public static string SyGetMax_Rng(UnitOfWorkCext01 unit, string NumOpe)
        {
            string codcct = VB6Helpers.Mid(NumOpe, 1, 3);
            string codpro = VB6Helpers.Mid(NumOpe, 4, 2);
            string codesp = VB6Helpers.Mid(NumOpe, 6, 2);

            string res = unit.SceRepository.EjecutarSP<string>("sce_vrng_s01_MS", codcct, codpro, codesp).First();
            if (res == null || res.Contains('X')) { throw new ComexUserException(string.Format("No fue posible obtener un número de operación válido, debido a que el usuario no tiene rango asignado para el producto {0}", codpro)); }

            return res ?? string.Empty;
        }

        //Lee un número asociado a la operación que se pide.
        public static double LeeSceRng(T_MODGRNG MODGRNG, T_MODGUSR MODGUSR, UI_Mdi_Principal Mdi_Principal, UnitOfWorkCext01 unit, string RngDoc)
        {
            double _retValue = 0;
            short a = SyGetn_xTrng(MODGRNG, unit);
            double z = 0;
            _retValue = (true ? -1 : 0);

            //Inicializa variables de Rangos.

            Type_RngDoc docBuscado = MODGRNG.RngDocs.Where(d => d.DocCod == RngDoc).FirstOrDefault();
            if (docBuscado == null)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No se conoce documento sobre el cual se pide el número."
                });
                Deshabilita_botones(MODGRNG, Mdi_Principal);
                return _retValue;
            }
            else
            {
                MODGRNG.RngGeneral = docBuscado.DocGen;
                MODGRNG.VRng.RngDoc = docBuscado.DocCod;
                if (MODGRNG.RngGeneral != 0)
                {
                    MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                    MODGRNG.VRng.RngEsp = "00";
                    MODGRNG.VRng.RngRut = "0000000000";
                }
                else
                {
                    MODGRNG.VRng.RngCct = MODGUSR.UsrEsp.CentroCosto;
                    MODGRNG.VRng.RngEsp = MODGUSR.UsrEsp.Especialista;
                    MODGRNG.VRng.RngRut = MODGUSR.UsrEsp.Rut;
                }

                var mensaje = string.Empty;
                z = SyGetUpd_Rng(MODGRNG, unit, out mensaje);
                if (z == -1)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "No se puede continuar con la operación, " + mensaje
                    });
                    Deshabilita_botones(MODGRNG, Mdi_Principal);
                }
                else if (z == 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = mensaje
                    });
                    Deshabilita_botones(MODGRNG, Mdi_Principal);
                }
                else
                {
                    return z;
                }

                return _retValue;
            }
        }

        public static dynamic Deshabilita_botones(T_MODGRNG MODGRNG, UI_Mdi_Principal Mdi_Principal)
        {
            MODGRNG.Rango_Permitido = false;
// Desabilitar todos los botones y solo habilita impresion.
            Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key =>
            {
                Mdi_Principal.BUTTONS[key].Enabled = (key == "tbr_impresion" || key == "tbr_nuevo") ? true : false;
            });
            return null;
        }
    }
}
