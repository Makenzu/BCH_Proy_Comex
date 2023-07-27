using BCH.Comex.Core.BL.SWEM.Datatypes;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.SWRE
{
    public class SwreService
    {
        public const string MSG_ERROR = "Ha ocurrido un error, contacte al administrador: ";
        public const int AVOID_SEARCH = -1;
        private UnitOfWorkSwift uow;
        
        public SwreService()
        {
            uow = new UnitOfWorkSwift();
        }

        public IList<sw_casillas> GetTodasLasCasillas()
        {
            return uow.CasillaRepository.Get(orderBy: (q => q.OrderBy(c => c.nombre_casilla)));
        }

        public InitialBotones RecepcionMensajeriaSwiftInit()
        {
            InitialBotones initBtn = new InitialBotones();
            return initBtn;
        }

        public bool ExisteConfiguraCasilla(int rutUsuario)
        {
            return uow.SwRepository.ExisteConfiguracionCasilla(rutUsuario);
        }

        public bool ExisteObservacionMensaje(int sesion, int secuencia)
        {
            return uow.SwRepository.ExisteObservacionMensaje(sesion, secuencia);
        }

        //Lista Pendientes
        public object BuscarListaMensajesPendiente(int Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            object data;
            try
            {

                if (Casilla == AVOID_SEARCH)
                {
                    data = new List<proc_sw_rec_trae_enc_rango_MS_Result>();
                }
                else
                {

                    IList<proc_sw_rec_trae_enc_rango_MS_Result> result = ListaPendiente(Casilla, fechaDesde, fechaHasta);

                    if (result.Count == 0)
                    {
                        data = new List<proc_sw_rec_trae_enc_rango_MS_Result>();
                    }
                    else
                    {
                        data = result.
                            Select(i => new proc_sw_rec_trae_enc_rango_MS_Result
                            {
                                sesion = i.sesion,
                                secuencia = i.secuencia,
                                casilla = i.casilla,
                                nombre_casilla = i.nombre_casilla,
                                tipo_msg = i.tipo_msg,
                                nombre_tipo = i.nombre_tipo,
                                prioridad = i.prioridad,
                                estado_msg = i.estado_msg,
                                fecha_env = i.fecha_env,
                                hora_env = i.hora_env,
                                fecha_enc = i.fecha_enc,
                                hora_enc = i.hora_enc,
                                cod_banco_rec = i.cod_banco_rec,
                                branch_rec = i.branch_rec,
                                cod_banco_em = i.cod_banco_em,
                                branch_em = i.branch_em,
                                nombre_banco = i.nombre_banco,
                                ciudad_banco = i.ciudad_banco,
                                pais_banco = i.pais_banco,
                                oficina_banco = i.oficina_banco,
                                cod_moneda = i.cod_moneda,
                                monto = i.monto,
                                referencia = i.referencia,
                                beneficiario = i.beneficiario,
                                total_imp = i.total_imp,
                                comentario = i.comentario

                            }).ToList();
                    }
                }



            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }
        public IList<proc_sw_rec_trae_enc_rango_MS_Result> ListaPendiente(int Casilla, DateTime? fechaDesde, DateTime fechaHasta)
        {
            return uow.SwRepository.ListaMensajesPendientes(Casilla, fechaDesde, fechaHasta).ToList();
        }

        //Lista Confirmados
        public object BuscarListaMensajesConfirmados(int Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            object data;
            try
            {

                if (Casilla == AVOID_SEARCH)
                {
                    data = new List<proc_sw_rec_trae_cnf_rango_MS_Result>();
                }
                else
                {

                    int cantidad = ListaConfirmados(Casilla, fechaDesde, fechaHasta).ToString().Count();

                    if (cantidad == 0)
                    {
                        data = new List<proc_sw_rec_trae_cnf_rango_MS_Result>();
                    }
                    else
                    {
                        data = ListaConfirmados(Casilla, fechaDesde, fechaHasta).
                        Select(i => new proc_sw_rec_trae_cnf_rango_MS_Result
                        {
                            sesion = i.sesion,
                            secuencia = i.secuencia,
                            casilla = i.casilla,
                            nombre_casilla = i.nombre_casilla,
                            tipo_msg = i.tipo_msg,
                            nombre_tipo = i.nombre_tipo,
                            prioridad = i.prioridad,
                            estado_msg = i.estado_msg,
                            fecha_enc = i.fecha_enc,
                            hora_enc = i.hora_enc,
                            fecha_rcb = i.fecha_rcb,
                            hora_rcb = i.hora_rcb,
                            cod_banco_rec = i.cod_banco_rec,
                            branch_rec = i.branch_rec,
                            cod_banco_em = i.cod_banco_em,
                            branch_em = i.branch_em,
                            nombre_banco = i.nombre_banco,
                            ciudad_banco = i.ciudad_banco,
                            pais_banco = i.pais_banco,
                            oficina_banco = i.oficina_banco,
                            cod_moneda = i.cod_moneda,
                            monto = i.monto,
                            referencia = i.referencia,
                            beneficiario = i.beneficiario,
                            total_imp = i.total_imp,
                            comentario = i.comentario

                        }).ToList();
                    }
                }

            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }

        public IList<proc_sw_rec_trae_cnf_rango_MS_Result> ListaConfirmados(int Casilla, DateTime fecha1, DateTime fecha2)
        {
            return uow.SwRepository.ListaMensajesConfirmados(Casilla, fecha1, fecha2).ToList();
        }


        //Lista Impresos

        public object BuscarListaMensajesImpresos(int Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            object data;
            try
            {

                if (Casilla == AVOID_SEARCH)
                {
                    data = new List<proc_sw_rec_trae_imp_rango_MS_Result>();
                }
                else
                {

                    int cantidad = ListaImpresos(Casilla, fechaDesde, fechaHasta).ToString().Count();

                    if (cantidad == 0)
                    {
                        data = new List<proc_sw_rec_trae_imp_rango_MS_Result>();
                    }
                    else
                    {
                        data = ListaImpresos(Casilla, fechaDesde, fechaHasta).
                        Select(i => new proc_sw_rec_trae_imp_rango_MS_Result
                        {
                            sesion = i.sesion,
                            secuencia = i.secuencia,
                            casilla = i.casilla,
                            nombre_casilla = i.nombre_casilla,
                            tipo_msg = i.tipo_msg,
                            nombre_tipo = i.nombre_tipo,
                            prioridad = i.prioridad,
                            estado_msg = i.estado_msg,
                            fecha_enc = i.fecha_enc,
                            hora_enc = i.hora_enc,
                            fecha_imp = i.fecha_imp,
                            hora_imp = i.hora_imp,
                            cod_banco_rec = i.cod_banco_rec,
                            branch_rec = i.branch_rec,
                            cod_banco_em = i.cod_banco_em,
                            branch_em = i.branch_em,
                            nombre_banco = i.nombre_banco,
                            ciudad_banco = i.ciudad_banco,
                            pais_banco = i.pais_banco,
                            oficina_banco = i.oficina_banco,
                            cod_moneda = i.cod_moneda,
                            monto = i.monto,
                            referencia = i.referencia,
                            beneficiario = i.beneficiario,
                            total_imp = i.total_imp,
                            comentario = i.comentario

                        }).ToList();
                    }
                }

            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }
        public IList<proc_sw_rec_trae_imp_rango_MS_Result> ListaImpresos(int Casilla, DateTime fecha1, DateTime fecha2)
        {
            return uow.SwRepository.ListaMensajesImpresos(Casilla, fecha1, fecha2).ToList();
        }

        //Lista Reenviados
        public object BuscarListaMensajesReenviados(int Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            object data;
            try
            {

                if (Casilla == AVOID_SEARCH)
                {
                    data = new List<proc_sw_rec_trae_ree_rango_MS_Result>();
                }
                else
                {

                    IList<proc_sw_rec_trae_ree_rango_MS_Result> result = ListaReenviados(Casilla, fechaDesde, fechaHasta);

                    if (result.Count == 0)
                    {
                        data = new List<proc_sw_rec_trae_ree_rango_MS_Result>();
                    }
                    else
                    {
                        data = result.
                            Select(i => new proc_sw_rec_trae_ree_rango_MS_Result
                            {
                                sesion = i.sesion,
                                secuencia = i.secuencia,
                                casilla = i.casilla,
                                nombre_casilla = i.nombre_casilla,
                                tipo_msg = i.tipo_msg,
                                nombre_tipo = i.nombre_tipo,
                                prioridad = i.prioridad,
                                estado_msg = i.estado_msg,
                                fecha_rec = i.fecha_rec,
                                hora_rec = i.hora_rec,
                                fecha_ree = i.fecha_ree,
                                hora_ree = i.hora_ree,
                                cod_banco_rec = i.cod_banco_rec,
                                branch_rec = i.branch_rec,
                                cod_banco_em = i.cod_banco_em,
                                branch_em = i.branch_em,

                                nombre_banco = i.nombre_banco,
                                ciudad_banco = i.ciudad_banco,
                                pais_banco = i.pais_banco,
                                oficina_banco = i.oficina_banco,
                                cod_moneda = i.cod_moneda,
                                monto = i.monto,
                                referencia = i.referencia,
                                beneficiario = i.beneficiario,
                                total_imp = i.total_imp,
                                comentario = i.comentario

                            }).ToList();
                    }
                }

            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }
        public IList<proc_sw_rec_trae_ree_rango_MS_Result> ListaReenviados(int Casilla, DateTime? fecha1, DateTime fecha2)
        {
            return uow.SwRepository.ListaMensajesReenviados(Casilla, fecha1, fecha2).ToList();
        }

        public void GrabaConfirmacion(int casilla, int sesion, int secuencia, int rutLog, string comentario)
        {
            uow.MensajeRepository.Proc_Sw_Rec_Graba_Conf(casilla, sesion, secuencia, rutLog, comentario);
        }

        public void GrabaRechazo(int casilla, int sesion, int secuencia, int rutLog, string estado, string texto)
        {
            uow.MensajeRepository.Proc_Sw_Rec_Graba_Rech(casilla, sesion, secuencia, rutLog, estado, texto);
        }

        public int Proc_sw_trae_flag_impreso(int sesion, int secuencia)
        {
            return uow.MensajeRepository.Proc_sw_rec_trae_nro_imp(sesion, secuencia);
        }

        public void Proc_sw_rec_graba_imp(int casilla, int sesion, int secuencia, int rutaLog, string estado, string comentario)
        {
            uow.MensajeRepository.Proc_sw_rec_graba_imp(casilla, sesion, secuencia, rutaLog, estado, comentario);
        }
    }
}
