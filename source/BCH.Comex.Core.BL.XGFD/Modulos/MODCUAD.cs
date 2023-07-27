using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;

namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    public class MODCUAD
    {
        private const string Msg_M = "MT no está en la base SWIFT";
        public const string Msg_A = "MT anulado en base SWIFT";
        private const string Msg_S = "MT no está en la base COMEX";

        public static bool ValidacionCuadraturaSwift(int rutSwift, DateTime fechaSwift, decimal rut, DateTime fecha, UnitOfWorkCext01 uow, UnitOfWorkSwift uows, DatosGlobales Globales)
        {
            Globales.MODCUAD = new T_MODCUAD();
            int bandera = 0, cont = 0;
            bool band = false;
            Globales.MODCUAD.Elementos = new No_Ele[0];

            var resultMts = uow.SyGet_Mts(rut, fecha);
            var resultSwift = uows.SyGet_Swift(rutSwift, fechaSwift);

            foreach (var valor in resultMts)
            {
                if (valor.tipgra == "R")
                {
                    foreach (var valorSwift in resultSwift)
                    {
                        if (valorSwift.tipo_ingreso == "A")
                        {
                            if (valor.id_mensaje == valorSwift.id_mensaje)
                            {
                                // verificar estado
                                if (valorSwift.descripcion != "Anulado")
                                {
                                    bandera = 1;
                                    break;
                                }
                                else
                                {
                                    bandera = 2;
                                    break;
                                }
                            }
                        }
                    }
                    //bandeta = 0 no esta en base swift ; bandera = 2 esta en swift pero esta anulado 
                    if (bandera == 0 || bandera == 2)
                    {
                        Array.Resize(ref Globales.MODCUAD.Elementos, cont + 1);
                        Globales.MODCUAD.Elementos[cont].referencia = valor.Column1;
                        Globales.MODCUAD.Elementos[cont].ncorr = valor.id_mensaje;
                        Globales.MODCUAD.Elementos[cont].tipo_mt_decimal = valor.tipmt;
                        Globales.MODCUAD.Elementos[cont].codcct = valor.codcct;
                        Globales.MODCUAD.Elementos[cont].codpro = valor.codpro;
                        Globales.MODCUAD.Elementos[cont].codesp = valor.codesp;
                        Globales.MODCUAD.Elementos[cont].codofi = valor.codofi;
                        Globales.MODCUAD.Elementos[cont].codope = valor.codope;
                        Globales.MODCUAD.Elementos[cont].tipo_mt = valor.tipmt.ToString();
                        Globales.MODCUAD.Elementos[cont].glosa_estado = string.Empty;

                        if (bandera == 0)
                        {
                            Globales.MODCUAD.Elementos[cont].mensaje_error = Msg_M;
                        }
                        else
                        {
                            Globales.MODCUAD.Elementos[cont].mensaje_error = MODCUAD.Msg_A;
                        }
                        cont = cont + 1;
                    }
                }
                bandera = 0;
            }


            foreach (var valorSwift in resultSwift)
            {
                if (valorSwift.tipo_ingreso == "A")
                {
                    foreach (var valor in resultMts)
                    {
                        if (valor.tipgra == "R")
                        {
                            if (valorSwift.id_mensaje == valor.id_mensaje)
                            {
                                band = true;
                                break;
                            }
                        }
                    }
                    if (!band)
                    {
                        if (valorSwift.descripcion != "Anulado")
                        {
                            Array.Resize(ref Globales.MODCUAD.Elementos, cont + 1);
                            Globales.MODCUAD.Elementos[cont].ncorr = valorSwift.id_mensaje;
                            Globales.MODCUAD.Elementos[cont].tipo_mt = valorSwift.tipo_msg;
                            Globales.MODCUAD.Elementos[cont].referencia = valorSwift.referencia;
                            Globales.MODCUAD.Elementos[cont].glosa_estado = valorSwift.descripcion;
                            Globales.MODCUAD.Elementos[cont].mensaje_error = Msg_S;

                            // Javier Cane 11/07/2002 Autor Errores Sw por Supervisor
                            Array.Resize(ref MODFDIA.Err_Sw_Sup, cont + 1);
                            MODFDIA.Err_Sw_Sup[cont].referencia = valorSwift.referencia;
                            MODFDIA.Err_Sw_Sup[cont].tipmt = valorSwift.tipo_msg;
                            MODFDIA.Err_Sw_Sup[cont].ncorr = valorSwift.id_mensaje;
                            MODFDIA.Err_Sw_Sup[cont].glosa_estado = valorSwift.descripcion;
                            MODFDIA.Err_Sw_Sup[cont].mensaje = Msg_S;

                            cont++;
                        }
                    }
                }
                band = false;
            }
            try
            {
                if (Globales.MODCUAD.Elementos.Length > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static int EliminaMT(string codcct, string codesp, string codofi, string codope, string codpro, string glosa_estado, string mensaje_error, string ncorr, string referencia, string tipo_mt, string tipo_mt_decimal)
        {
            return 0;
        }
    }
}
