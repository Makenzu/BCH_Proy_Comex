using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using System;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using System.Collections.Generic;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class DOC_CVDI
    {
        // ============================================================================
        // VARIABLES Y CODIGO EN EL SERVIDOR DE IMPRESION.-
        // ============================================================================
        // Datos Generales para Carta de Compra Venta.-
        // ----------------------------------------------------------------------------
        public struct STipo_DocCV
        {
            public string NroRef;   //Número de la Cobranza.-    NomPrt      As String       'Nombre del Importador.-
            public string Oprel;   //número de operación relacionada
            public string Nomprt;
            public string DirPrt;   //Dirección del Importador.-
            public string CiuPrt;   //Ciudad del Importador.-
            public string EstPrt;   //Estado del Importador.-
            public string PosPrt;   //Postal del Importador.-
            public string NomEsp;   //Nombre del Especialista.-
            public string DirEsp;   //Direccion        "
            public string TelEsp;   //Telefono         "
            public string FaxEsp;   //Fax              "
            public int tipocarta;   //1:Compra; 2:Venta; 3:Ambos.-
            public string MonSeg;   //Moneda del Seguro.-
            public string MtoSeg;   //Monto del Seguro.-
            public string MonFle;   //Moneda del Flete.-
            public string MtoFle;   //Monto del Flete.-
            public string MonGas;   //Moneda del Gasto del Cedente.-
            public string MtoGas;   //Monto del Gasto del Cedente.-
            public string Totpes;   //Total en Pesos, en Detalle.-
            public string Concepto;
        }
        // ----------------------------------------------------------------------------
        // Montos, Monedas y Tipo de Cambio de las compras y/o ventas
        // ----------------------------------------------------------------------------
        public struct Scompra_venta
        {
            public string tipo;   //1 si es compra, 2 si es venta
            public string Moneda;   //Moneda del Principal.-
            public double Monto;   //Monto de lo Cubierto.-
            public double Cambio;   //Tipo de Cambio.-
            public double total_peso;   //total en pesos de la compra o venta
            public int NumPla;   //numero planilla
            public string NumDec;   //numero declaracion
        }
        // ----------------------------------------------------------------------------
        //  Monedas y montos de los arbitrajes
        // ----------------------------------------------------------------------------
        public struct sarbitraje
        {
            public string moneda_1;
            public string monto_1;
            public string moneda_2;
            public string monto_2;
            public string Paridad;
        }
        // ----------------------------------------------------------------------------
        // Beneficiarios del Pago.-
        // ----------------------------------------------------------------------------
        public struct STipo_BenCV
        {
            public string Benef;   //Nombre del Beneficiario.-
            public string Via;   //Vía Ej. Cta. Cte. + #Cta.-
            public string Moneda;
            public string Monto;
        }
        // ----------------------------------------------------------------------------
        // Débitos por efecto de Pago.-
        // ----------------------------------------------------------------------------
        public struct STipo_DebCV
        {
            public string deb_hab;
            public string Debito;   //Tipo de Débito, Ej. Cuenta Contable.-
            public string Moneda;
            public string Monto;
        }
        // ----------------------------------------------------------------------------
        // Detalle de Impuestos en $.-
        // ----------------------------------------------------------------------------
        public struct STipo_DetCV
        {
            public string Detalle;   //Glosa de Impuestos + Varios ,en $.-
            public string Moneda;
            public string Monto;
        }
        // ----------------------------------------------------------------------------
        public static STipo_DocCV SDoccv = new STipo_DocCV();
        public static Scompra_venta[] smtocv = null;     // Montos Pagados.-
        public static Scompra_venta[] smtotr = null;     // Montos Trasfer.-
        public static STipo_BenCV[] sbencv = null;     // Beneficiarios del Pago.-
        public static STipo_DebCV[] Sdebcv = null;     // Débitos por Pago.-
        public static STipo_DetCV[] sdetcv = null;     // Detalle de Impuestos.-
        public static sarbitraje[] smonarb = null;
        public static string Ciudad = "";
        public static int chequea_linea(ref int Lineas)
        {
            int chequea_linea = 0;


            Lineas = Lineas + 1;
            if (Lineas > 48)
            {
                chequea_linea = 5;
            }
            else
            {
                chequea_linea = Lineas;
            }

            return chequea_linea;
        }
        public static int HayOperacion(string tipo)
        {
            int HayOperacion = 0;
            int i = 0;
            //Operación Import realizada sin transferencia.
            for (i = 0; i <= smtocv.GetUpperBound(0); i += 1)
            {
                if (smtocv[i].tipo == tipo)
                {
                    HayOperacion = true.ToInt();
                    return HayOperacion;
                }
            }
            return false.ToInt();
        }
        // Header para Carta de Compra Venta.-
        public static void Header06(DocumentoReporteModel documento)
        {
            string Hoy = "";
            if (MODXDOC.VOpe.NumOpe.Mid(1, 3) == "753")
            {
                documento.TituloCabezal = "Servicios M/E";
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Servicios M/E");
            }
            else
            {
                documento.TituloCabezal = "Comercio Exterior";
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Comercio Exterior");
            }
            Hoy = MigrationSupport.Utils.Format(DateTime.Now, "dd/mm/yyyy");
            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(70),Ciudad + ", " + MODXDOC.Glosa_Fecha_Hoy_Espanol(Hoy)});
            documento.Ciudad = Ciudad + ", " + MODXDOC.Glosa_Fecha_Hoy_Espanol(MODXDOC01.VDocx.FecEmi);
            // 
            // Referencia.-
            documento.NuestraReferencia = "Compra-Venta  Divisas Nº :";
            documento.NuestraReferenciaValue = MODXDOC.VOpe.NumOpe_t.TrimB();   //MODXDOC.Raya_Cobranza(MODXDOC.VOpe.NumOpe);
            if (!string.IsNullOrEmpty(SDoccv.Oprel))
            {
                documento.SuReferencia = "Operación Relacionada Nº :";
                documento.SuReferenciaValue = MODXDOC.Raya_Cobranza(SDoccv.Oprel);
            }
            MODXDOC.linea = 8;
        }
        // Recibe los datos de la Carta de Compra Venta.-
        public static void Pr_Load_Doc402(string Dato)
        {
            using (var trace = new Tracer("Pr_Load_Doc402"))
            {
                trace.TraceInformation("Datos de la carta: ", Dato);
                try
                {
                    double Sum = 0.0;
                    int eltop = 0;
                    int i = 0;

                    smonarb = new sarbitraje[1];
                    smtocv = new Scompra_venta[1];
                    sbencv = new STipo_BenCV[0];
                    Sdebcv = new STipo_DebCV[0];
                    sdetcv = new STipo_DetCV[0];

                    MODXDOC.VOpe.NumOpe = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumIni()).TrimB();
                    SDoccv.Oprel = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    trace.TraceInformation("Nro Operación: ", MODXDOC.VOpe.NumOpe);
                    // ------------------------------------------------------------------------
                    // Datos del Cliente.-
                    // ------------------------------------------------------------------------
                    MODXDOC.PartysOpe = new MODXDOC.PartyKey[2];
                    MODXDOC.PartysOpe[0].NombreUsado = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].DireccionUsado = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].CiudadUsado = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].EstadoUsado = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].PostalUsado = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].Fax = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].CasBanco = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].CasPostal = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    MODXDOC.PartysOpe[0].Enviara = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                    // ------------------------------------------------------------------------
                    // Compra, Venta o Ambos.-
                    // ------------------------------------------------------------------------
                    SDoccv.tipocarta = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                    // ------------------------------------------------------------------------
                    // Otros Pagos de : Seguro, Flete, Gastos Cedente.-
                    // ------------------------------------------------------------------------
                    SDoccv.NomEsp = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    SDoccv.DirEsp = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    SDoccv.TelEsp = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    SDoccv.FaxEsp = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();

                    // ------------------------------------------------------------------------
                    // Concepto de la CVD.-
                    // ------------------------------------------------------------------------
                    SDoccv.Concepto = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    // ------------------------------------------------------------------------
                    // Compras y ventas Divisas realizadas.-
                    // ------------------------------------------------------------------------
                    i = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                    smtocv = new Scompra_venta[i];

                    for (i = 0; i < smtocv.Length; i += 1)
                    {
                        smtocv[i].tipo = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                        smtocv[i].Moneda = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                        smtocv[i].Monto = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                        smtocv[i].Cambio = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                        smtocv[i].total_peso = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                        smtocv[i].NumPla = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                        smtocv[i].NumDec = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    }

                    // ------------------------------------------------------------------------
                    // Transferencias realizadas.-
                    // ------------------------------------------------------------------------
                    i = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                    smtotr = new Scompra_venta[i];

                    for (i = 0; i < smtotr.Length; i += 1)
                    {
                        smtotr[i].Moneda = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                        smtotr[i].Monto = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                        smtotr[i].NumPla = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                        smtotr[i].NumDec = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                    }

                    // ------------------------------------------------------------------------
                    // Número de Vias.-
                    // ------------------------------------------------------------------------
                    i = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                    if (i != -1)
                    {
                        sbencv = new STipo_BenCV[i];
                        for (i = 0; i < sbencv.Length; i += 1)
                        {
                            sbencv[i].Benef = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            sbencv[i].Via = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            sbencv[i].Moneda = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            if (sbencv[i].Moneda == "$")
                                sbencv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Trim(), MODXDOC.FormatoSinDec);
                            else
                                sbencv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Trim(), MODXDOC.Formato);
                        }
                    }
                    // ------------------------------------------------------------------------
                    // Número de Débitos.-
                    // ------------------------------------------------------------------------
                    i = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToInt();
                    if (i > 0)
                    {
                        Sdebcv = new STipo_DebCV[i];
                        for (i = 0; i < Sdebcv.Length; i += 1)
                        {
                            Sdebcv[i].deb_hab = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            Sdebcv[i].Debito = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            Sdebcv[i].Moneda = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            if (Sdebcv[i].Moneda == "$")
                                Sdebcv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Trim(), MODXDOC.FormatoSinDec);
                            else
                                Sdebcv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Trim(), MODXDOC.Formato);
                        }
                    }
                    // ------------------------------------------------------------------------
                    // Número de Detalles.-
                    // ------------------------------------------------------------------------
                    i = (MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).ToVal()).ToInt();
                    if (i > 0)
                    {
                        sdetcv = new STipo_DetCV[i];
                        for (i = 0; i < sdetcv.Length; i += 1)
                        {
                            sdetcv[i].Detalle = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            sdetcv[i].Moneda = MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).TrimB();
                            if (sdetcv[i].Moneda == "$")
                                sdetcv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Trim(), MODXDOC.FormatoSinDec);
                            else
                                sdetcv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato, 9.Char(), MODGSYB.NumSig()).Trim(), MODXDOC.Formato);

                        }
                    }
                    // ------------------------------------------------------------------------
                    // Se calcula por mientras la suma del detalle.-
                    // ------------------------------------------------------------------------
                    eltop = 0;
                    eltop = sdetcv.Length;
                    if (eltop > 0)
                    {
                        Sum = 0;
                        for (i = 0; i < sdetcv.Length; i += 1)
                        {
                            Sum = Sum + MigrationSupport.Utils.Format(sdetcv[i].Monto, String.Empty).ToDbl();
                        }
                        SDoccv.Totpes = MODGDOC.forma((Sum.ToInt()).Str().TrimB(), MODXDOC.FormatoSinDec);
                    }
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
            }
        }
        //Carta de Venta y Divisas.-
        /// <summary>
        /// Construye el formato de la carta de venta y divisas hacia la vista.
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static void Pr_Principal_13(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int intCaseArg = 0;
            int eltop = 0;
            int i = 0;
            int sw = 0;
            int A_CPo = 0;
            int A_CBa = 0;
            int A_Fax = 0;
            int A_Dir = 0;
            int NLinea = 0;
            int bien = 0;

            string cad = "";
            string fax_eje = "";
            string tel_eje = "";
            string dir_eje = "";
            string nom_eje = "";
            string s = "";
            string s2 = "";

            double TotDetalle = 0.0;
            bool EsOrigenPesos = false;

            NLinea = 5;

            // ----------------------------------------------------------
            // Titulo para Señores.-
            // ----------------------------------------------------------

            // ------------------------------------------------------------------------
            // Datos del Importador.-
            // ------------------------------------------------------------------------
            if (MODXDOC.PartysOpe[0].EstadoUsado.TrimB().UCase() != "CHILE")
            {
                s2 = MODXDOC.Concatena(MODXDOC.PartysOpe[0].CiudadUsado, MODXDOC.PartysOpe[0].EstadoUsado, MODXDOC.PartysOpe[0].PostalUsado);
            }
            else
            {
                if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[0].PostalUsado))
                {
                    s2 = MODXDOC.PartysOpe[0].CiudadUsado + "," + MODXDOC.PartysOpe[0].PostalUsado;
                }
                else
                {
                    s2 = MODXDOC.PartysOpe[0].CiudadUsado;
                }
            }

            documento.NombreCliente = MODXDOC.PartysOpe[0].NombreUsado;
            intCaseArg = MODXDOC.PartysOpe[0].Enviara;

            if (intCaseArg == A_Dir || intCaseArg == A_Fax)
            {
                documento.DireccionCliente = MODXDOC.PartysOpe[0].DireccionUsado;
                documento.CiudadCliente = s2;
            }
            else if (intCaseArg == A_CBa)
            {
            }
            else if (intCaseArg == A_CPo)
            {
            }
            NLinea = NLinea + 6;

            // ------------------------------------------------------------------------
            // Encabezado.-
            // ------------------------------------------------------------------------
            s = "De acuerdo a vuestras instrucciones hemos emitido ";
            switch (SDoccv.tipocarta)
            {
                case 1:
                    s = s + "planillas, según el siguiente detalle :";
                    break;
                case 2:
                    s = s + "planillas, ";
                    if (!string.IsNullOrEmpty(SDoccv.Concepto))
                    {
                        s = s + "por Concepto de " + SDoccv.Concepto;
                        NLinea = NLinea + 1;
                        s = "";
                    }
                    s = s + " según el siguiente detalle :";
                    break;
                case 3:
                    s = s + "Compras y Ventas de Divisas, según el siguiente detalle :";
                    break;
                case 4:
                    s = "Hemos efectuado Arbitraje de Divisas, según el siguiente detalle/distribución: ";
                    break;
            }
            documento.Parrafo1 = s;
            NLinea = NLinea + 2;

            // ------------------------------------------------------------------------
            // Monto de las compras y/o ventas.-
            // ------------------------------------------------------------------------
            switch (SDoccv.tipocarta)
            {
                case 1:
                case 2:
                case 3:
                    if (HayOperacion("1") != 0)
                    {
                        sw = 0;
                        //Operación de Importación realizada sin transferencia. 
                        for (i = 0; i <= smtocv.GetUpperBound(0); i += 1)
                        {
                            if (smtocv[i].Monto != 0)
                            {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 1)
                        {
                            //Operación Importaciones realizada cin transferencia.
                            for (i = 0; i <= smtocv.GetUpperBound(0); i += 1)
                            {
                                if (smtocv[i].tipo == "1")
                                {
                                    if (smtocv[i].Monto != 0)
                                    {
                                        var transferencia = new Transferencia
                                        {
                                            Moneda = smtocv[i].Moneda.UCase(),
                                            Monto = MODGDOC.forma(smtocv[i].Monto, MODXDOC.Formato),
                                            TipoCambio = smtocv[i].Cambio.ToString(),
                                            NroPlanilla = smtocv[i].NumPla.ToString(),
                                            DeclaracionImportacion = smtocv[i].NumDec
                                        };
                                        documento.TituloPlanillaImport = "Ventas de Divisas";
                                        documento.LineasTransferencia.Add(transferencia);
                                        NLinea = chequea_linea(ref NLinea);
                                    }
                                }
                            }
                        }
                        NLinea = NLinea + 2;
                    }

                    if (HayOperacion("2") != 0)
                    {
                        //Ventas
                        sw = 0;
                        //Operación Import realizada Sin Transferencia.
                        for (i = 0; i < smtocv.Length; i += 1)
                        {
                            if (smtotr[i].Monto != 0)
                            {
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 1)
                        {
                            NLinea = NLinea - 1;
                            NLinea = chequea_linea(ref NLinea);
                            NLinea = NLinea + 2;

                            for (i = 0; i <= smtocv.GetUpperBound(0); i += 1)
                            {
                                if (smtocv[i].tipo == "2")
                                {
                                    if (smtocv[i].Monto != 0)
                                    {
                                        var transferencia = new Transferencia
                                        {
                                            Moneda = smtocv[i].Moneda.UCase(),
                                            Monto = MODGDOC.forma(smtocv[i].Monto, MODXDOC.Formato),
                                            TipoCambio = smtocv[i].Cambio.ToString(),
                                            NroPlanilla = smtocv[i].NumPla.ToString(),
                                            DeclaracionImportacion = smtocv[i].NumDec
                                        };
                                        documento.TituloPlanillaImport = "Ventas de Divisas";
                                        documento.LineasTransferencia.Add(transferencia);
                                        NLinea = chequea_linea(ref NLinea);
                                    }
                                }
                            }
                        }
                        NLinea = NLinea + 2;
                    }

                    sw = 0;

                    for (i = 0; i < smtotr.Length; i += 1)
                    {
                        if (smtotr[i].Monto != 0)
                        {
                            sw = 1;
                            break;
                        }
                    }

                    if (sw == 1)
                    {
                        for (i = 0; i < smtotr.Length; i += 1)
                        {
                            if (smtotr[i].Monto != 0)
                            {
                                var transferencia = new Transferencia
                                {
                                    Moneda = smtotr[i].Moneda.UCase(),
                                    Monto = MODGDOC.forma(smtotr[i].Monto, MODXDOC.Formato),
                                    NroPlanilla = smtotr[i].NumPla.ToString(),
                                    DeclaracionImportacion = smtotr[i].NumDec
                                };
                                documento.TituloPlanillaImport = "Transferencias";
                                documento.LineasTransferencia.Add(transferencia);
                                NLinea = chequea_linea(ref NLinea);
                            }
                        }
                    }

                    break;

                case 4:
                    sw = 0;
                    for (i = 1; i <= smonarb.GetUpperBound(0); i += 1)
                    {
                        if (smonarb[i].monto_1.ToInt() != 0)
                        {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 1)
                    {
                        NLinea = NLinea - 1;
                        NLinea = chequea_linea(ref NLinea);
                        NLinea = NLinea + 2;
                        for (i = 0; i <= smonarb.GetUpperBound(0); i += 1)
                        {
                            if (!string.IsNullOrEmpty(smonarb[i].monto_1))
                            {
                                NLinea = chequea_linea(ref NLinea);
                            }
                        }
                    }
                    NLinea = NLinea + 1;
                    break;
            }
            NLinea = NLinea + 1;

            // ------------------------------------------------------------------------
            // Beneficiarios.-
            // ------------------------------------------------------------------------
            eltop = -1;
            eltop = sbencv.Length;
            if (eltop > 0)
            {
                NLinea = NLinea - 1;
                NLinea = chequea_linea(ref NLinea);
                s = "La moneda extranjera vendida fue distribuida de la siguiente forma :";

                // --------------------------------------------------------------------
                // Título de Beneficiarios.-
                // --------------------------------------------------------------------
                sw = 0;
                for (i = 0; i < sbencv.Length; i += 1)
                {
                    if (!string.IsNullOrEmpty(sbencv[i].Monto))
                    {
                        sw = 1;
                        break;
                    }
                }

                if (sw == 1)
                {
                    NLinea = chequea_linea(ref NLinea);
                    // --------------------------------------------------------------------
                    // Detalle de Beneficiarios.-
                    // --------------------------------------------------------------------
                    NLinea = NLinea + 3;
                    for (i = 0; i < sbencv.Length; i += 1)
                    {
                        if (!string.IsNullOrEmpty(sbencv[i].Monto))
                        {
                            var remesa = new Remesa { Beneficiario = sbencv[i].Benef, ViaRemesa = sbencv[i].Via, Moneda = sbencv[i].Moneda.UCase(), Monto = sbencv[i].Monto };
                            documento.LineasRemesa.Add(remesa);
                            NLinea = chequea_linea(ref NLinea);
                        }
                    }
                }
            }
            NLinea = NLinea + 1;
            // ------------------------------------------------------------------------
            // Débitos.-
            // ------------------------------------------------------------------------
            eltop = -1;
            eltop = Sdebcv.Length;
            if (eltop > 0)
            {
                NLinea = NLinea - 1;
                NLinea = chequea_linea(ref NLinea);
                s = "En consecuencia efectuamos los débitos y/o abonos que se detallan: ";
                NLinea = NLinea + 3;
                // --------------------------------------------------------------------
                // Detalle de Débitos.-
                // --------------------------------------------------------------------
                for (i = 0; i < Sdebcv.Length; i += 1)
                {
                    if (!string.IsNullOrEmpty(Sdebcv[i].Monto))
                    {
                        var cargoAbono = new CargoAbono { NombreVia = Sdebcv[i].deb_hab, DescripcionVia = Sdebcv[i].Debito, Moneda = Sdebcv[i].Moneda.UCase(), Monto = Sdebcv[i].Monto };
                        documento.LineasCargoAbono.Add(cargoAbono);
                        if (Sdebcv[i].Debito.Mid(1, 23) == "Cuenta Corriente M/N  $")
                        {
                            EsOrigenPesos = true;
                        }
                        NLinea = chequea_linea(ref NLinea);
                    }
                }
            }
            EsOrigenPesos = true;
            if (SDoccv.tipocarta == 4)
            {
                EsOrigenPesos = false;
            }
            if (EsOrigenPesos)
            {
                // --------------------------------------------------------------------
                // Impuestos en $.-
                // --------------------------------------------------------------------
                NLinea = NLinea + 2;
                NLinea = chequea_linea(ref NLinea);
                eltop = -1;
                eltop = sdetcv.Length;
                if (eltop >= 0)
                {
                    // ----------------------------------------------------------------
                    // Detalle de Impuestos.-
                    // ----------------------------------------------------------------
                    NLinea = NLinea + 3;
                    TotDetalle = 0;
                    for (i = 0; i < sdetcv.Length; i += 1)
                    {
                        if (sdetcv[i].Detalle.Mid(1, 5) == "Monto" && SDoccv.tipocarta == 4)
                        {

                        }
                        else
                        {
                            if (i + 1 == sdetcv.Length && sdetcv.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(sdetcv[i].Monto))
                                {
                                    if (sdetcv[i].Detalle == "Monto en pesos de la moneda extranjera vendida")
                                    {
                                        if (sdetcv[i].Monto.ToInt() > 0)
                                        {
                                            var impuesto = new Impuesto { Detalle = sdetcv[i].Detalle, Moneda = "$", Monto = sdetcv[i].Monto };
                                            documento.LineasImpuesto.Add(impuesto);
                                            TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto, String.Empty).ToDbl();
                                            NLinea = chequea_linea(ref NLinea);
                                        }
                                    }
                                    else
                                    {
                                        var impuesto = new Impuesto { Detalle = sdetcv[i].Detalle, Moneda = "$", Monto = sdetcv[i].Monto };
                                        documento.LineasImpuesto.Add(impuesto);
                                        TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto, String.Empty).ToDbl();
                                        NLinea = chequea_linea(ref NLinea);
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sdetcv[i].Monto))
                                {

                                    if (sdetcv[i].Detalle == "Monto en pesos de la moneda extranjera vendida")
                                    {
                                        if (sdetcv[i].Monto.ToInt() > 0)
                                        {
                                            var impuesto = new Impuesto { Detalle = sdetcv[i].Detalle, Moneda = "$", Monto = sdetcv[i].Monto };
                                            documento.LineasImpuesto.Add(impuesto);
                                            TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto, String.Empty).ToDbl();
                                            NLinea = chequea_linea(ref NLinea);
                                        }
                                    }
                                    else
                                    {
                                        var impuesto = new Impuesto { Detalle = sdetcv[i].Detalle, Moneda = "$", Monto = sdetcv[i].Monto };
                                        documento.LineasImpuesto.Add(impuesto);
                                        TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto, String.Empty).ToDbl();
                                        NLinea = chequea_linea(ref NLinea);
                                    }
                                }
                            }
                        }
                    }
                }
                if (TotDetalle > 0)
                {
                    documento.TotalImpuestos = SDoccv.Totpes;
                }
            }
            while (NLinea < 45)
            {
                NLinea = NLinea + 1;
            }


            // ***************************************** MPN *******************************************************************
            int X = 0;
            UnitOfWorkCext01 unit = new UnitOfWorkCext01();
            X = Sygetn_Ejecutivos(unit);

            Module1.Hab_SGTCliEje = 1;
            if (Module1.Hab_SGTCliEje != 0)
            {
                //Module1.Cliente_SGT = Es_Cliente(Module1.rutiparty.Mid(1, 9));
                Module1.Cliente_SGT = 1;
                if (Module1.Cliente_SGT != 0)
                {
                    bien = Lee_SgtCliEsp(Module1.rutiparty.Mid(1, 9), uow);
                    if (bien != 0)
                    {
                        for (i = 1; i <= Module1.VSGTCliEsp.GetUpperBound(0); i += 1)
                        {
                            if (Module1.VSGTCliEsp[i].tipo == Module1.SGT_tipopimp)
                            {
                                nom_eje = Obtiene_NomEsp(Module1.VSGTCliEsp[i].ofieje, Module1.VSGTCliEsp[i].codeje);
                            }
                            else if (Module1.VSGTCliEsp[i].tipo == Module1.SGT_tipopexp)
                            {
                                nom_eje = Obtiene_NomEsp(Module1.VSGTCliEsp[i].ofieje, Module1.VSGTCliEsp[i].codeje);
                            }
                            else if (Module1.VSGTCliEsp[i].tipo == Module1.SGT_tipnegoc)
                            {
                                nom_eje = Obtiene_NomEsp(Module1.VSGTCliEsp[i].ofieje, Module1.VSGTCliEsp[i].codeje);
                            }
                        }
                        // End If
                        for (i = 1; i <= Module1.VEjc.GetUpperBound(0); i += 1)
                        {
                            if (nom_eje == Minuscula2(Module1.VEjc[i].nombre))
                            {
                                //Busca información del ejecutivo en la tabla sce_usr.
                                var info_Ejecutivo = Obtiene_info_Ejecutivo(uow, Module1.VEjc[i].rut);
                                if (info_Ejecutivo.Count > 0)
                                {
                                    nom_eje = info_Ejecutivo[0].nombre;
                                    dir_eje = info_Ejecutivo[0].direccion;
                                    tel_eje = info_Ejecutivo[0].telefono;
                                    fax_eje = info_Ejecutivo[0].fax;

                                    if (fax_eje == "")
                                    {
                                        fax_eje = " ";
                                    }
                                    if (tel_eje == "")
                                    {
                                        tel_eje = " ";
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }


            if (!string.IsNullOrEmpty(nom_eje))
            {
                SDoccv.NomEsp = nom_eje;
                SDoccv.DirEsp = dir_eje;
                SDoccv.TelEsp = tel_eje;
                SDoccv.FaxEsp = fax_eje;
            }

            //Cadena respecto al ejecutivo del especialista:
            cad = MODXDOC.VOpe.NumOpe_t.TrimB() + "~" + SDoccv.NomEsp.TrimB() + " " + SDoccv.DirEsp.TrimB() + "~" + SDoccv.TelEsp.TrimB() + "~" + SDoccv.FaxEsp.TrimB();

            s = MODFRA.SyGet_Fra(uow, 9002, "E", cad);
            documento.PiePagina = s;
        }
        public static string sacar_direccion(UnitOfWorkCext01 uow, string rut_eje)
        {
            string result = "";
            try
            {
                result = uow.SceRepository.sce_usr_s25_MS(MODGSYB.dbcharSy(rut_eje));
            }
            catch (Exception exc)
            {
            }
            return result;
        }
        public static int Lee_SgtCliEsp(string rutcli, UnitOfWorkCext01 uow)
        {
            int Lee_SgtCliEsp = 0;
            string R = "";
            int h = 0;

            try
            {
                var result = uow.SceRepository.ejc_prty_ejc_s_01_MS(rutcli);
                if (R == "-1")
                {
                    return Lee_SgtCliEsp;
                }

                Module1.RSGTCliEsp = new Module1.CliEsp[1];
                Module1.VSGTCliEsp = new Module1.CliEsp[1];

                foreach (var item in result)
                {
                    item.ejc_tpo = "0" + item.ejc_tpo;
                    if (item.ejc_tpo == Module1.SGT_tipopimp || item.ejc_tpo == Module1.SGT_tipopexp || item.ejc_tpo == Module1.SGT_tipnegoc)
                    {
                        h = h + 1;
                        Array.Resize(ref Module1.RSGTCliEsp, h + 1);
                        Array.Resize(ref Module1.VSGTCliEsp, h + 1);
                        Module1.RSGTCliEsp[h].nrut = item.prty_rut; // R.Mid((5 + sig), 9);
                        Module1.RSGTCliEsp[h].tipo = item.ejc_tpo;  // R.Mid((14 + sig), 2);
                        Module1.RSGTCliEsp[h].ofieje = string.Format("{0:000}", item.ejc_ofi.ToString());// R.Mid((16 + sig), 3);
                        Module1.RSGTCliEsp[h].codeje = string.Format("{0:000}", item.ejc_cod.ToString());// R.Mid((19 + sig), 3);
                        Module1.RSGTCliEsp[h].feccre = item.create_at.ToString();// R.Mid((22 + sig), 8);

                        /*
                        Module1.RSGTCliEsp[h].rutope = R.Mid((30 + sig), 9);
                        Module1.RSGTCliEsp[h].drutope = R.Mid((39 + sig), 1);
                        Module1.RSGTCliEsp[h].filler = R.Mid((40 + sig), 35);
                        */

                        Module1.RSGTCliEsp[h].borrar = false.ToInt();
                        Module1.VSGTCliEsp[h] = Module1.RSGTCliEsp[h];
                    }
                    Lee_SgtCliEsp = true.ToInt();
                    return Lee_SgtCliEsp;
                }
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Lee_SgtCliEsp = 0;
            }
            return Lee_SgtCliEsp;
        }
        // Deja un string de varias palabras separadas por un blanco como la primera
        // letra en Mayúscula y el resto de la palabra en minúscula.-
        public static string Minuscula2(string PDato)
        {
            string Minuscula2 = "";

            string strCaseArg = "";
            int j = 0;
            string s = "";
            int i = 0;
            string[] Palabras = null;
            //  *********************************  MPN  *************************************************************
            // D.S.B.
            Palabras = new string[1];
            string Dato = "";

            // Deja cada palabra separado con un solo blanco
            Dato = MODGDOC.Componer(PDato, "  ", " ");
            i = 1;
            s = MODGDOC.CopiarDeString(Dato, " ", i);
            while (!string.IsNullOrEmpty(s))
            {
                // Verifica si la palabra siguiente contiene la cantidad de caracteres necesarios.
                j = Palabras.GetUpperBound(0) + 1;
                Array.Resize(ref Palabras, j + 1);
                strCaseArg = s.UCase().TrimB();
                if (strCaseArg == "S.A." || strCaseArg == "M/E" || strCaseArg == "M/N")
                {
                    Palabras[j] = s.UCase();
                }
                else if (strCaseArg == "A" || strCaseArg == "DE" || strCaseArg == "Y" || strCaseArg == "O" || strCaseArg == "Y/O" || strCaseArg == "U" || strCaseArg == "AL" || strCaseArg == "DE" || strCaseArg == "LO" || strCaseArg == "LA" || strCaseArg == "EL" ||
                   strCaseArg == "SI" || strCaseArg == "NO" || strCaseArg == "E" || strCaseArg == "POR" || strCaseArg == "OF")
                {
                    Palabras[j] = s.LCase();
                }
                else
                {
                    Palabras[j] = s.Mid(1, 1).UCase() + s.Mid(2, s.Len() - 1).LCase();
                }
                i = i + 1;
                s = MODGDOC.CopiarDeString(Dato, " ", i);
            }
            s = "";
            for (i = 1; i <= Palabras.GetUpperBound(0); i += 1)
            {
                s = s + Palabras[i] + " ";
            }
            Minuscula2 = s.TrimB();

            return Minuscula2;
        }
        public static string Obtiene_NomEsp(string codofi, string CodEsp)
        {
            string Obtiene_NomEsp = "";


            int i = 0;
            int fin = 0;


            Obtiene_NomEsp = "";

            fin = 0;
            fin = Module1.VEjc.GetUpperBound(0);

            for (i = 1; i <= fin; i += 1)
            {
                if (Module1.VEjc[i].codofi == codofi && Module1.VEjc[i].codejc == CodEsp)
                {
                    Obtiene_NomEsp = Minuscula2(Module1.VEjc[i].nombre);
                    break;
                }
            }
            return Obtiene_NomEsp;
        }
        public static int Es_Cliente(string rutcli)
        {
            int Es_Cliente = 0;

            string R = "";
            //  colocado para poder buscar a los ejecutivos
            string rut = "";
            int fin = 0;
            int i = 0;
            int sig = 0;
            int h = 0;

            try
            {

                rut = "12345678";
                R = "";
                string argTemp1 = "";
                R = RespuestaSgt2(Module1.ParamSgt.Nodo, rut, Module1.ParamSgt.SerLee, Module1.ParamSgt.VisClt, ref argTemp1, rutcli, "V0009");

                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Hay problemas de comunicación con el SRM o bien con la vista L001. No se podrá rescatar el nombre del ejecutivo.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "Atención");
                    return Es_Cliente;
                }

                if (R == "")
                {
                    Es_Cliente = false.ToInt();
                }
                else
                {
                    //  Es Cliente
                    Es_Cliente = true.ToInt();
                }

                // 

                return Es_Cliente;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Es_Cliente = false.ToInt();
                throw new XegiException("Error al Actualizar en tablas SGT : [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }
        public static string RespuestaSgt2(string Nodo, string RutCons, string Servidor, string Vista, ref string Tabla, string llave, string Oper)
        {
            string RespuestaSgt2 = "";

            string s = "";
            string R = "";
            int x = 0;
            string m = "";


            int Intentos = 0;
            MODGSRM.RowCount = 0;
            Intentos = 0;

            Conssgt2:
            Intentos = Intentos + 1;
            Tabla = Tabla.UCase();
            m = RutCons + Vista.TrimB() + Oper.TrimB() + Tabla.TrimB() + llave;
            Module1.ParamSgt.Mensaje = m;
            Module1.ParamSgt.largo = m.Len();
            Module1.ParamSgt.Status = "00";
            Module1.ParamSgt.Funcion = "08";
            Module1.ParamSgt.Contexto = "00";

            object argTemp1 = Module1.ParamSgt.largo;
            x = MODGSRM.srmw8(Nodo, Servidor, Module1.ParamSgt.Mensaje, ref argTemp1, Module1.ParamSgt.Status, Module1.ParamSgt.Funcion, Module1.ParamSgt.Contexto, Module1.ParamSgt.Control);

            if (!(x == 0 && Module1.ParamSgt.Mensaje.Left(2) == "00"))
            {
                if (Module1.ParamSgt.Mensaje.Left(2) == "96")
                {
                    RespuestaSgt2 = "";
                    return RespuestaSgt2;
                }

                if (Intentos <= 1)
                {
                    goto Conssgt2;
                }
                RespuestaSgt2 = "-1";
                return RespuestaSgt2;
            }
            R = Module1.ParamSgt.Mensaje.TrimB();

            s = R.Mid(3, R.Len() - 2);

            RespuestaSgt2 = s;


            return RespuestaSgt2;
        }
        private static int Sygetn_Ejecutivos(UnitOfWorkCext01 uow)
        {
            int Sygetn_Ejecutivos = 0;
            bool Leyo_Ok = false;
            int n = 0;

            try
            {
                Module1.VEjc = new Module1.T_Especialista[n + 1];
                var dbSelectPrtyEsp = uow.SceRepository.Sgt_ejc_S03_MS(Module1.EJCOPIMP, Module1.EJCOPEXP, Module1.EJCNEGOC);
                Module1.VEjc = new Module1.T_Especialista[dbSelectPrtyEsp.Count];
                for (int i = 0; i < dbSelectPrtyEsp.Count; i++)
                {
                    Module1.VEjc[i] = new Module1.T_Especialista();
                    Module1.VEjc[i].codofi = string.Format("{0:000}", dbSelectPrtyEsp[i].ejc_ejcofi); //Convert.ToString(dbSelectPrtyEsp[i].ejc_ejcofi); // Se agrega formato "000" a la izquierda, segun fix reportados
                    Module1.VEjc[i].codejc = string.Format("{0:000}", dbSelectPrtyEsp[i].ejc_ejccod); //Convert.ToString(dbSelectPrtyEsp[i].ejc_ejccod); // Se agrega formato "000" a la izquierda, segun fix reportados
                    Module1.VEjc[i].rut = dbSelectPrtyEsp[i].ejc_ejcrut;
                    Module1.VEjc[i].nombre = dbSelectPrtyEsp[i].ejc_ejcnom;
                    Module1.VEjc[i].tipo = dbSelectPrtyEsp[i].ejc_ejctpo;
                }

                Sygetn_Ejecutivos = true.ToInt();
                return Sygetn_Ejecutivos;
            }
            catch (Exception exc)
            {
                Leyo_Ok = false;
            }
            return Sygetn_Ejecutivos;
        }
        public static int Obtine_Nom_Ejecutivo(UnitOfWorkCext01 uow, string rut_eje)
        {
            int Lee_SgtCliEjc = 0;

            var _NombreEjecutivo = "";
            try
            {
                var result = uow.SceRepository.sce_usr_s26_MS(MODGSYB.dbcharSy(rut_eje));
                _NombreEjecutivo = result[0].nombre;

                MODXDOC.UsrEsp.nombre = result[0].nombre;
                MODXDOC.UsrEsp.Direccion = result[0].direccion;
                MODXDOC.UsrEsp.telefono = result[0].telefono;
                MODXDOC.UsrEsp.Fax = result[0].fax;

                Lee_SgtCliEjc = true.ToInt();
                return Lee_SgtCliEjc;
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Lee_SgtCliEjc = 0;
            }
            return Lee_SgtCliEjc;
        }
        private static List<sce_usr_s26_MS_Result> Obtiene_info_Ejecutivo(UnitOfWorkCext01 uow, string rut_eje)
        {
            List<sce_usr_s26_MS_Result> result = new List<sce_usr_s26_MS_Result>();
            try
            {
                result = uow.SceRepository.sce_usr_s26_MS(MODGSYB.dbcharSy(rut_eje));
            }
            catch (Exception exc)
            {
            }
            return result;
        }
    }
}
