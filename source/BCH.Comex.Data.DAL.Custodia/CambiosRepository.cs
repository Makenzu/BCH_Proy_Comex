using BCH.Comex.Core.Entities.Custodia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Data.DAL.Custodia
{
    public class CambiosRepository : GenericRepository<CAMBIOS_GRAL_SCE_CTA, CustodiaEntities>
    {
        public CambiosRepository(CustodiaEntities context) : base(context) { }

        public List<cambios_gral_consulta_00_MS_Result> cambios_gral_consulta_00_MS(byte opcion)
        {
            try
            {
                return context.cambios_gral_consulta_00_MS(opcion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cambios_mift_callfax_00_MS_Result> cambios_mift_callfax_00_MS(byte opcion, long rut, string cuenta)
        {
            return context.cambios_mift_callfax_00_MS(opcion, rut, cuenta).ToList();
        }

        public int cambios_mift_check_list_log_01_MS(string accCuenta, string desMoneda, decimal monto, string usuario, string chkLista)
        {
            return context.cambios_mift_check_list_log_01_MS(accCuenta, desMoneda, monto, usuario, chkLista);
        }

        public List<cambios_mift_citidoc_consulta_01_MS_Result> cambios_mift_citidoc_consulta_01_MS(string accNum, decimal monto)
        {
            try
            {
                return context.cambios_mift_citidoc_consulta_01_MS(accNum, monto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<cambios_mift_citidoc_duplicados_01_MS_Result> cambios_mift_citidoc_duplicados_01_MS(string accNum, decimal monto)
        {
            try
            {
                return context.cambios_mift_citidoc_duplicados_01_MS(accNum, monto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<string> cambios_mift_citidoc_duplicados_hora_01_MS()
        {
            try
            {
                return context.cambios_mift_citidoc_duplicados_hora_01_MS().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cambios_mift_cliente_01_MS_Result> cambios_mift_cliente_01_MS(byte opcion, string nombre)
        {
            try
            {
                return context.cambios_mift_cliente_01_MS(opcion, nombre).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<int?> cambios_mift_cuenta_00_MS(byte opcion, long rut, string cuenta)
        {
            try
            {
                return context.cambios_mift_cuenta_00_MS(opcion, rut, cuenta).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int cambios_mift_log_insert_2_MS(string fecha_hora, string fecha, string rut, string rutDv, string cuenta, string nombreClte,
                                                     string segmento, string ejecutivo, string moneda, long monto, string cuentaBnf, string nombreBnf,
                                                     string bancoInt, string bancoBnf, string ind_Con_Otros, string ind_Con_Mift, string ind_Con_Fax,
                                                     string ind_Con_Citi, string ind_Con_Fax_NY, string ind_Con_Mail, string txt_Con_Otros, string resultado,
                                                     string mensaje, string usuario)
        {
            try
            {
                int result = context.cambios_mift_log_insert_2_MS(fecha_hora, fecha, rut, rutDv, cuenta, nombreClte, segmento, ejecutivo, moneda, monto, cuentaBnf, nombreBnf, bancoInt, bancoBnf,
                    ind_Con_Otros, ind_Con_Mift, ind_Con_Fax, ind_Con_Citi, ind_Con_Fax_NY, ind_Con_Mail, txt_Con_Otros, resultado, mensaje, usuario);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IList<string> cambios_mift_mensajes_01_MS(byte tipo, string accnum)
        {
            try
            {
                return context.cambios_mift_mensajes_01_MS(tipo, accnum).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cambios_mift_mesa_cvd_00a_MS_ResultDTO> cambios_mift_mesa_cvd_00a_MS(short opcion, Nullable<long> rutCliente, string accnum, string moneda, decimal monto)
        {
            List<cambios_mift_mesa_cvd_00a_MS_ResultDTO> result = new List<cambios_mift_mesa_cvd_00a_MS_ResultDTO>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    int c = 0;
                    result.Add(new cambios_mift_mesa_cvd_00a_MS_ResultDTO
                    {
                        R = reader.GetInt32(0),
                        G = reader.GetInt32(1),
                        B = reader.GetInt32(2),
                        ESTADO = reader.GetInt32(3),
                        MENSAJE = reader.GetString(4)

                    });
                }
            }, "cambios_mift_mesa_cvd_00a_MS", opcion.ToString(), rutCliente.ToString(), accnum, moneda, monto.ToString());

            return result;
        }

        public IList<cambios_mift_mesa_cvd_00b_MS_Result> cambios_mift_mesa_cvd_00b_MS(short? opcion, long rutCliente, string accnum, string moneda, decimal monto)
        {
            try
            {
                return context.cambios_mift_mesa_cvd_00b_MS(opcion, rutCliente, accnum, moneda, monto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<cambios_mift_recurrencia_01b_MS_Result> cambios_mift_recurrencia_01b_MS(short? Ord_rut_aux, string ord_cuenta, string bnf_swfbco, string bnf_cuenta, string bnf_nombre, string moneda, decimal monto)
        {
            try
            {
                return context.cambios_mift_recurrencia_01b_MS(Ord_rut_aux, ord_cuenta, bnf_swfbco, bnf_cuenta, bnf_nombre, moneda, monto).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int cambios_mift_recurrencia_manual_01_MS(byte opcion, string accnum, string ord_cuenta, string bnf_cuenta, string bnf_nombre, string bnf_swfbco)
        {
            try
            {
                int result = context.cambios_mift_recurrencia_manual_01_MS(opcion, accnum, ord_cuenta, bnf_cuenta, bnf_nombre, bnf_swfbco);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int cambios_mift_reparo_log_01_MS(string accnum, string rut, string nomcli, string moneda, decimal monto, string doc_name,
                                                         string mailejec, string usuario, string reparo_list, string otro_1, string otro_2, string otro_3,
                                                         string otro_4, string otro_5)
        {
            try
            {
                int result = context.cambios_mift_reparo_log_01_MS(accnum, rut, nomcli, moneda, monto, doc_name, mailejec, usuario, reparo_list, otro_1, otro_2, otro_3, otro_4, otro_5);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<cambios_mift_tarifas_obs_01_MS_Result> cambios_mift_tarifas_obs_01_MS(string accCuenta)
        {
            var result = new List<cambios_mift_tarifas_obs_01_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    var item = new cambios_mift_tarifas_obs_01_MS_Result
                    {
                        Observaciones = Utils.GetStringFromDataReader(reader, 0),
                    };
                    result.Add(item);
                }
            }, "cambios_mift_tarifas_obs_01_MS", accCuenta);

            return result;

        }

        public IList<cambios_mift_tarifas_pizarra_01_MS_Result> cambios_mift_tarifas_pizarra_01_MS(string tipo, string subtipo, string moneda, decimal monto)
        {

            return context.cambios_mift_tarifas_pizarra_01_MS(tipo, subtipo, moneda, monto).ToList();
        }

        public IList<cambios_mift_tarifas_pizarra_02_MS_Result> cambios_mift_tarifas_pizarra_02_MS(string tipo, string subtipo, string moneda, decimal monto)
        {

            return context.cambios_mift_tarifas_pizarra_02_MS(tipo, subtipo, moneda, monto).ToList();
        }

        public IList<cambios_mift_cliente_00_MS_Result> cambios_mift_cliente_00_MS(byte? opcion, long? rut, string cuenta)
        {
            return context.cambios_mift_cliente_00_MS(opcion, rut, cuenta).ToList();
        }

        public IList<cambios_mift_contratos_00_MS_Result> cambios_mift_contratos_00_MS(byte? opcion, long? rut, string cuenta)
        {
            return context.cambios_mift_contratos_00_MS(opcion, rut, cuenta).ToList();
        }

        public IList<cambios_mift_tarifas_01_MS_Result> cambios_mift_tarifas_01_MS(string accCuenta, string desMoneda, decimal monto)
        {
            return context.cambios_mift_tarifas_01_MS(accCuenta, desMoneda, monto).ToList();
        }
        public List<short?> cambios_mift_recurrencia_02_MS(byte? Opcion, string ord_Rut, string ord_cuenta, string bnf_swfbco, string bnf_cuenta, string Desmoneda, decimal? Monto)
        {
            try
            {
                return context.cambios_mift_recurrencia_02_MS(Opcion, ord_Rut, ord_cuenta, bnf_swfbco, bnf_cuenta, Desmoneda, Monto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
