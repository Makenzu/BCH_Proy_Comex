using BCH.Comex.Common;
using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    public static class modcuacc
    {
        public static bool SyGet_CCtx(T_MODCUACC MODCUACC, IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 uow)
        {
            bool SyGet_CCtx = false;


            using (Tracer tracer = new Tracer())
            {
                try
                {
                    int i = 0;
                    
                    MODCUACC.VCodTra = new List<T_CodTra>();

                    SyGet_CCtx = true;

                    //gsQuery = "";
                    //gsQuery = "Exec " + MODGSRM.ParamSrm8k.Base + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_cctx_s01 ";

                    //R = MODGSRM.RespuestaQuery(ref gsQuery);

                    var Resp = uow.SceRepository.sce_cctx_s01_MS().ToList();

                    if (Resp == null || Resp.Count() == 0)
                    {
                        //MigrationSupport.Utils.MsgBox("Error al leer Códigos de Transacción para Cuenta Corriente en Línea. El Servidor reporta : [" + MODGSRM.ParamSrm8k.mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle
                        //   >(), "Cuenta Corriente en Línea");
                        ListaMensajesError.Add(new UI_Message() { Text = "Error al leer Códigos de Transacción para Cuenta Corriente en Línea", Type = TipoMensaje.Error });
                        SyGet_CCtx = false;
                        return SyGet_CCtx;
                    }

                    //n = MODGSRM.getocurrs();
                    //if (n > 0)
                    //{

                    //    MigrationSupport.Utils.ResumeNext(() =>
                    //    {
                    //        tope = VCodTra.GetUpperBound(0);
                    //    });

                    //    total = tope + n;
                    //    Array.Resize(ref VCodTra, total + 1);
                    foreach(sce_cctx_s01_MS_Result R in Resp)
                    {
                        MODCUACC.VCodTra.Add(new T_CodTra()
                        {
                            codtra = R.codtra,
                            destra = R.destra,
                            nemtra = R.nemtra
                        });

                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido leer códigos de transacción para cuentas corrientes", ex);
                    SyGet_CCtx = false;
                }
            }
            //}


            //return SyGet_CCtx;

            //MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescrption(MigrationSupport.GlobalException.Instance.Number), MODGPYF0.pito(48).Cast<
            //   MigrationSupport.MsgBoxStyle>(), "Cuenta Corriente en Linea");
            //SyGet_CCtx = false.ToInt();
            // Resume SyGet_CcTxEnd;


            return SyGet_CCtx;
        }

        public static bool SyGet_RutOficina(string cenCosto, string especialista, ref string rutesp,
            UnitOfWorkCext01 uow, IList<UI_Message> listaMensajes)
        {
            bool SyGet_RutOficina = false;
            rutesp = "";

            //Que = "";
            //Que = Que + "select substring(rut, 2, 9) from " + MODGSRM.ParamSrm8k.Base + ".";
            //Que = Que + MODGSRM.ParamSrm8k.usuario + ".sce_usr ";
            //Que = Que + "where cent_costo = " + MODGSYB.dbcharSy(CenCosto) + " and ";
            //Que = Que + "id_especia = " + MODGSYB.dbcharSy(Especialista);
            var result = uow.SceRepository.Sce_Usr_S05_MS(cenCosto, especialista);
            if (result == null)
            {
                listaMensajes.Add(new UI_Message
                {
                    Text = "Error al leer Rut del Especialista",
                    Title = "Cuenta Corriente en Linea",
                    Type = TipoMensaje.Error
                });

                return SyGet_RutOficina;
            }

            //if (rutesp.Length < 9)
            //{
            //    rutesp = rutesp.Substring(2);
            //        //MigrationSupport.Utils.Format(rutesp.Mid(2, rutesp.Len()), "000000000");
            //}
            rutesp = result.rut.Substring(1, 9).PadLeft(9, '0');

            //lo saco de aca. se ubtiene de UsrEsp en globales
            //codofi = Environment.GetEnvironmentVariable("OFICINA");
            //codofi = MigrationSupport.Utils.Format(codofi, "00000");

            rutesp = rutesp.Substring(0, 8);

            SyGet_RutOficina = true;

            return SyGet_RutOficina;
        }

        /// <summary>
        /// Obtiene Contabilidad del Día realizada por el Especialista Sce_Mcd
        /// </summary>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="rutesp"></param>
        /// <param name="fecmov"></param>
        /// <param name="modCuaCC"></param>
        /// <returns></returns>
        public static bool SyGet_ContaEsp(string cencos, string codusr, string rutesp, DateTime fecmov, T_MODCUACC modCuaCC,
            UnitOfWorkCext01 uow, IList<UI_Message> listaMensajes)
        {
            modCuaCC.VConCCLin = new List<T_ConCCLin>();

            var result = uow.SceRepository.Sce_mcd_s71_MS(cencos, codusr, rutesp, fecmov);

            if (result == null)
            {
                listaMensajes.Add(new UI_Message
                {
                    Title = "Cuenta Corriente en Línea",
                    Text = "Error al leer Contabilidad del Especialista " + cencos + " - " + codusr + " - " + fecmov +
                        " en tabla Sce_Mcd, para Chequeo de Cuenta Corriente en Línea. Reporte este problema.",
                    Type = TipoMensaje.Error
                });

                return false;
            }

            // Resultado nulo de la Consulta.-
            if (result.Count == 0)
            {
                return true;
            }

            foreach (var item in result)
            {
                if (item.nemmon == "US$")
                {
                    modCuaCC.VConCCLin.Add(new T_ConCCLin
                    {
                        cencos = cencos,
                        codusr = codusr,
                        codcct = item.codcct,
                        codpro = item.codpro,
                        codesp = item.codesp,
                        codofi = item.codofi,
                        codope = item.codope,
                        nrorpt = (int)item.nrorpt,
                        fecmov = item.fecmov.ToString("dd/MM/yyyy"),
                        cod_dh = item.cod_dh,
                        nemcta = item.nemcta,
                        numcct = item.numcct,
                        mtomcd = (double)item.mtomcd,
                        nemmon = item.nemmon,
                        estado = (int)item.estado,
                        cuadra = 0,
                        error = 0,
                    });
                }
            }

            return true;

        }

        public static bool SyGet_CargoAbono(string centroCosto, string especialista, UnitOfWorkCext01 uow, T_MODCUACC modCuaCC,
            IList<UI_Message> listaMensajes)
        {
            bool SyGet_CargoAbono = false;

            try
            {
                var result = uow.SceRepository.Sce_mcd_s70(centroCosto, especialista);

                //modCuaCC.V_CYA = new List<BCH.Comex.Core.Entities.Cext01.FinDia.CargoAbono>();
                foreach (var item in result)
                {
                    var temp = new BCH.Comex.Core.Entities.Cext01.FinDia.CargoAbono();

                    temp.numcct = item.numcct;
                    temp.nemcta = item.nemcta;
                    temp.fecmov = item.fecmov.ToString("dd/MM/yyyy");
                    temp.nroimp = (int)item.nroimp;
                    temp.nombre = item.Column1;
                    temp.cod_dh = item.cod_dh;
                    temp.nemmon = item.nemmon;
                    temp.codmon = (int)item.codmon;
                    temp.mtomcd = (double)item.mtomcd;
                    temp.operacion = item.Column2;
                    temp.nrorpt = (int)item.nrorpt;
                    temp.enlinea = (int)(item.enlinea ?? 0 );
                    temp.estado = (int)item.estado;
                    temp.codcct = item.codcct;
                    temp.codpro = item.codpro;
                    temp.codesp = item.codesp;
                    temp.codofi = item.codofi;
                    temp.codope = item.codope;
                    temp.codfun = (int)item.Column3;
                    temp.indice = result.IndexOf(item);

                    modCuaCC.V_CYA.Add(temp);
                }

                SyGet_CargoAbono = true;

                return SyGet_CargoAbono;
            }
            catch (Exception exc)
            {
                listaMensajes.Add(new UI_Message
                {
                    Text = "Se ha producido un error al tratar de leer los datos de los Usuarios",
                    Title = "Fin de día",
                    Type = TipoMensaje.Error
                });

                if (ExceptionPolicy.HandleException(exc, "PoliticaBLFundTransfer")) throw;
            }

            return SyGet_CargoAbono;
        }

        public static string Tiene_TrxMEInyectar(IList<BCH.Comex.Core.Entities.Cext01.FinDia.CargoAbono> vCyAList, string nombreUsuario)
        {
            string Tiene_TrxMEInyectar = string.Empty;
            string MsgRng = "";

            int fin = 0;
            int i = 0;
            bool hay = false;
            bool encabezado = false;
            bool encabezadoInyectar = false;
            bool encabezadoReversar = false;
            int reg = 0;

            try
            {
                reg = 0;
                hay = false;
                encabezado = true;
                encabezadoInyectar = true;
                encabezadoReversar = true;
                Printer printer = new Printer();
                foreach (var item in vCyAList)
	            {
                    //Se verifica si hay movimientos por inyectar de operaciones vigentes || movimiento inyectodos de operaciones anuladas
                    if ((item.enlinea == 0 && item.estado == 1) || (item.enlinea == 1 && item.estado == 9))                    
                    {
                        if (encabezado)
                        {
                            // Se imprime el primer encabezado
                            Imprime_Header(printer, nombreUsuario);
                            encabezado = false;
                        }

                        if(item.enlinea == 0 && item.estado == 1)
                        {
                            if (encabezadoInyectar)
                            {
                                // Se imprime encabezado cuando hay pendientes por inyectar
                                Imprime_Header_Inyectar(printer, nombreUsuario);
                                encabezadoInyectar = false;
                            }
                            Imprimir(item, printer, nombreUsuario);
                            reg = reg + 1;
                            hay = true;
                        }
                        
                        if(item.enlinea == 1 && item.estado == 9)
                        {
                            if (encabezadoReversar)
                            {
                                // Se imprime cuando hay Movimientos Anulados Pendientes por Reversar
                                Imprime_Header_Reversar(printer, nombreUsuario);
                                encabezadoReversar = false;
                            }
                            Imprimir(item, printer, nombreUsuario);
                            reg = reg + 1;
                            hay = true;
                        }
                    }
	            }

                if (hay)
                {
                    // Imprime total de movimientos por resolver 
                    imprime_trailer(reg, printer);
                }

                Tiene_TrxMEInyectar = printer.ToString().Trim();

                return Tiene_TrxMEInyectar;
            }
            catch (Exception exc)
            {
                if (ExceptionPolicy.HandleException(exc, "PoliticaBLFundTransfer")) throw;
            }

            return Tiene_TrxMEInyectar;
        }

        public static void imprime_trailer(int num, Printer printer)
        {

           printer.Print();
           printer.Print();
           //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, true);
           //printer.Font = MigrationSupport.Utils.FontChangeSize(printer.Font, 11);
           printer.Print();
           printer.PrintList(Printer.TAB(31), "Total de Movimientos por Resolver : " + num);
           //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, false);
           //printer.EndDoc();
        }

        private static void Imprimir(BCH.Comex.Core.Entities.Cext01.FinDia.CargoAbono item, Printer printer, string nombreUsuario)
        {
            string paso5 = "";
            string paso4 = "";
            string paso3 = "";
            string paso2 = "";
            string paso1 = "";


            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, false);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(printer.Font, 10);
            //printer.Font = new Font("Courier New", 9F);
            
            //if (item.enlinea == 1 && item.estado == 9)
            //{
            //    paso1 = "REVERSAR " + item.codcct.Trim() + "-" + item.codpro.Trim() + "-" + item.codesp.Trim() + "-" + 
            //        item.codofi.Trim() + "-" + item.codope.Trim();
            //}
            //else
            //{
            paso1 = item.codcct.Trim() + "-" + item.codpro.Trim() + "-" + item.codesp.Trim() + "-" +
            item.codofi.Trim() + "-" + item.codope.Trim();
            //}

            paso2 = item.nombre.Trim();
            if (paso2.Length > 40)
            {
                paso2 = paso2.Substring(0, 40);
            }
            if (item.cod_dh.Trim() == "D")
            {
                paso3 = "Cargos";
            }
            else
            {
                paso3 = "Abonos";
            }
            paso4 = item.nemmon.Trim();
            paso5 = item.mtomcd.ToString("#,###,###,###,##0.00");
            printer.PrintWithoutCrlf(Printer.TAB(1), paso1, Printer.TAB(23), paso2, Printer.TAB(65), paso3, 
                Printer.TAB(73), paso4.PadLeft(3),
                Printer.TAB(78), paso5.PadLeft(19));
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
        }

        public static void Imprime_Header(Printer printer, string nombreUsuario)
        {
            int nropag = 0;
            //printer.Font = new Font("Arial", 9F);
            //printer.Font = MigrationSupport.Utils.FontChangeItalic(printer.Font, false);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, false);
            //printer.Font = MigrationSupport.Utils.FontChangeUnderline(printer.Font, false);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(printer.Font, 17);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, true);
            printer.PrintList(Printer.TAB(1), "Banco de Chile");
            printer.PrintWithoutCrlf(Printer.TAB(1), "Comercio Exterior");
            //printer.Font = new Font("Courier New", 9F);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(printer.Font, 11);
            printer.PrintList(Printer.TAB(79), "Fecha : " + DateTime.Now.ToString("dd/MM/yyyy"));
            SaltoPagina(printer, nombreUsuario);
            printer.PrintList(Printer.TAB(79), "Pag   : " + nropag.ToString().PadRight(3));
            SaltoPagina(printer, nombreUsuario);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, false);
            //printer.Font = new Font("Arial", 9F);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(printer.Font, 12);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, true);
            //printer.Font = MigrationSupport.Utils.FontChangeUnderline(printer.Font, true);
            printer.PrintList(Printer.TAB(35), "Listado de Movimientos en Linea");
            SaltoPagina(printer, nombreUsuario);
            printer.PrintList(Printer.TAB(39), "por Inyectar - Reversar");
            SaltoPagina(printer, nombreUsuario);
            //printer.Font = MigrationSupport.Utils.FontChangeUnderline(printer.Font, false);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.Print("Lider :  " + nombreUsuario);//MODGUSR.UsrEsp.nombre);
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            //printer.Font = new Font("Courier New", 9F);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(printer.Font, 10);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, true);
            //printer.Font = MigrationSupport.Utils.FontChangeUnderline(printer.Font, true);
        }

        public static void Imprime_Header_Reversar(Printer printer, string nombreUsuario)
        {
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.PrintList(Printer.TAB(1), "------------------------------------------------------------------------------------------------");
            SaltoPagina(printer, nombreUsuario);
            printer.PrintList(Printer.TAB(30), "Movimientos Anulados Pendientes por Reversar");
            printer.PrintList(Printer.TAB(1), "------------------------------------------------------------------------------------------------");
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            printer.PrintWithoutCrlf("# Operación", Printer.TAB(30), "Nombre Cliente", Printer.TAB(66), "C/A",
                Printer.TAB(72), "Moneda", Printer.TAB(89), "Monto");
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            //printer.Font = MigrationSupport.Utils.FontChangeUnderline(printer.Font, true);
            printer.PrintList(Printer.TAB(1), "------------------------------------------------------------------------------------------------");
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
        }

        public static void Imprime_Header_Inyectar(Printer printer, string nombreUsuario)
        {
            printer.PrintList(Printer.TAB(1), "------------------------------------------------------------------------------------------------");
            SaltoPagina(printer, nombreUsuario);
            printer.PrintList(Printer.TAB(40), "Pendientes por Inyectar");
            printer.PrintList(Printer.TAB(1), "------------------------------------------------------------------------------------------------");
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            printer.PrintWithoutCrlf("# Operación", Printer.TAB(30), "Nombre Cliente", Printer.TAB(66), "C/A",
                Printer.TAB(72), "Moneda", Printer.TAB(89), "Monto");
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
            printer.PrintList(Printer.TAB(1), "------------------------------------------------------------------------------------------------");
            SaltoPagina(printer, nombreUsuario);
            printer.Print();
            SaltoPagina(printer, nombreUsuario);
        }

        public static void SaltoPagina(Printer printer, string nombreUsuario)
        {
            int nropag = 0;
            int nrolin = 0;

            if (nrolin >= 55)
            {
                nropag = nropag + 1;
                nrolin = 0;
                printer.NewPage();
                Imprime_Header(printer, nombreUsuario);
            }
            else
            {
                nrolin = nrolin + 1;
            }
        }


    }
}
