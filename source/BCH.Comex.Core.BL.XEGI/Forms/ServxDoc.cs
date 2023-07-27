using BCH.Comex.Core.BL.XEGI.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XEGI.Forms
{
    public class ServxDoc
    {
        private bool _disposed = false;
        public ServxDoc()
        {
        }

        // Busca al ejecutivo que tiene asignado el participante, en las diferentes tablas
        public string busejc_party(UnitOfWorkCext01 uow, string codcct, string copro, string coesp, string coofi, string coope)
        {
            string busejc_party = string.Empty, RutUsado = string.Empty;

            var result = uow.SceRepository.sce_doc_s01_MS(
                MODGSYB.dbcharSy(codcct),
                MODGSYB.dbcharSy(copro),
                MODGSYB.dbcharSy(coesp),
                MODGSYB.dbcharSy(coofi),
                MODGSYB.dbcharSy(coope)
                );

            if (result == null || result.Count == 0)
            {
                return string.Empty;
            }

            RutUsado = result[0];
            char pad = '0';
            RutUsado = RutUsado.Replace("~", " ").Trim().PadLeft(10, pad);
            busejc_party = RutUsado;
            return busejc_party;
        }
        
        public double SyGet_Ini(XegiService service, string grupo, string elem, int flgAviso)
        {
            double SyGet_Ini = 0.0;
            string Query = "", tipo = "", valor = "", MsgCom = "";
            List<sce_ini_s01_MS_Result> R = null;            
            int largo = 0, decim = 0;
            try
            {
                
                R = service.sce_ini_s01_MS(MODGSYB.dbcharSy(grupo), MODGSYB.dbcharSy(elem));
                if (R != null && R.Count > 0)
                {
                    tipo = R[0].tipov;
                    largo = Convert.ToInt32(R[0].largo);
                    decim = Convert.ToInt32(R[0].decim);
                    valor = R[0].valor;
                }

                if (tipo == "N")
                {
                    SyGet_Ini = valor.ToVal();
                }
                else if (tipo == "C")
                {
                    SyGet_Ini = valor.ToDbl();
                }

                return SyGet_Ini;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }

        public int Habil_SGTCliEje(XegiService service)
        {
            int Habil_SGTCliEje = 0;
            //  D.S.B.
            int habil = 0;
            habil = false.ToInt();
            habil = SyGet_Ini(service, "sgtclieje", "xx", false.ToInt()).ToInt();
            Habil_SGTCliEje = habil;
            return Habil_SGTCliEje;
        }

        public string RespuestaSgt2(string Nodo, string RutCons, string Servidor, string Vista, ref string Tabla, string llave, string Oper)
        {
            string RespuestaSgt2 = "", s = "", R = "", m = "";
            int x = 0;
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
        public int Es_Cliente(string rutcli)
        {
            int Es_Cliente = 0;

            string R = "";

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
        public int Lee_SgtCliEsp(string rutcli, UnitOfWorkCext01 uow)
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

        // ****************************************************************************
        //    1.  Detalle de los Cargos y Abonos.
        // ****************************************************************************
        private void Pr_Abonos(DocumentoReporteModel documento)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int b = MODXDOC.VxOri.Length;
            int a = MODXDOC.VxVia.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    a = MODXDOC.VxVia.GetUpperBound(0);
            //    b = MODXDOC.VxOri.GetUpperBound(0);
            //});

            s = "En consecuencia efectuamos los débitos y/o abonos que se detallan:";

            //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
            //TODO: Hay que revisar por qué las Lineas de MODXDOC llegan en null
            //for (i = 0; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            //{
            //    if (MODXDOC.Lineas[i].TrimB() != "")
            //    {
            //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
            //        MODXDOC.Pr_Salto_Pagina();
            //    }
            //}

            for (i = 0; i < a; i += 1)
            {
                var cargoAbono = new CargoAbono
                {
                    DescripcionVia = MODXDOC.VxVia[i].Descri.TrimB(),
                    NombreVia = MODXDOC.VxVia[i].NomVia.TrimB(),
                    Moneda = MODXDOC.VxVia[i].NemMon.TrimB(),
                    Monto = MODGDOC.forma(MODXDOC.VxVia[i].MtoTot, MODXDOC.Formato)
                };
                documento.LineasCargoAbono.Add(cargoAbono);
                //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(MODXDOC.VxVia[i].MtoTot,MODXDOC.Formato)});
            }
            for (i = 0; i < b; i += 1)
            {
                var cargoAbono = new CargoAbono
                {
                    DescripcionVia = MODXDOC.VxOri[i].Descri.TrimB(),
                    NombreVia = MODXDOC.VxOri[i].NomOri.TrimB(),
                    Moneda = MODXDOC.VxOri[i].NemMon.TrimB(),
                    Monto = MODGDOC.forma(MODXDOC.VxOri[i].MtoTot, MODXDOC.Formato)
                };
                documento.LineasCargoAbono.Add(cargoAbono);
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(MODXDOC.VxOri[i].MtoTot,MODXDOC.Formato)});
            }

        }
        // ****************************************************************************
        //    1.  Detalle de la Cuenta Corriente para la carta de Exportador.
        // ****************************************************************************
        private void Pr_Cta_Cte(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta)
        {
            string MndAnt = "";
            int Contador_Arreglo = 0;
            int x = 0;
            int i = 0;
            int c = 0;
            string s = "";
            bool lugar = false;
            int n = MODXDOC.VDet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDet.GetUpperBound(0);
            //});

            // Printer.Print : Call Pr_Salto_Pagina
            // Printer.Print : Call Pr_Salto_Pagina

            lugar = true;
            // Pago Directo Cobranza Export.(610)  - 'Planillas Visible Export.(613)
            if (Carta == MODXDOC.DocxPagDir)
            {
                s = MODFRA.SyGet_Fra(uow, 6701, "E", "");
                // s$ = "En consecuencia de lo anterior hemos efectuado los débitos que se detallan:"
                // Cancelación Cobranza Export. - Cancelación Retorno Export.
            }
            else if (Carta == MODXDOC.DocxCobCan || Carta == MODXDOC.DocxCanRet)
            {
                s = MODFRA.SyGet_Fra(uow, 6702, "E", "");
                // s$ = "En consecuencia efectuamos los abonos y/o débitos que se detallan:"
            }
            else if (Carta == MODXDOC.DocxCobReg)
            {
                c = 0;
                for (i = 0; i < n; i += 1)
                {
                    if (MODXDOC.VDet[i].tipo.TrimB() == "D")
                    {
                        c = c + 1;
                    }
                }
                if (c > 0)
                {
                    MODXDOC.Pr_Salto_Pagina();
                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(MODXDOC.tab_titulos)),"Cargos"});
                    //MigrationSupport.Printer.DefInstance.PrintList(new object[] { MigrationSupport.FileSystem.TAB((short)(MODXDOC.tab_titulos)), "Cargos" });
                }
                lugar = false;
            }

            if (lugar)
            {
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                /*for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                {
                    if (MODXDOC.Lineas[i].TrimB() != "")
                    {
                        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                        MODXDOC.Pr_Salto_Pagina();
                    }
                }*/
            }

            Contador_Arreglo = 0;
            for (i = 0; i < n; i += 1)
            {
                if (MODXDOC.VDet[i].tipo.TrimB() == "D")
                {
                    Contador_Arreglo = i;
                    var comisionGasto = new ComisionGasto { Tipo = "D", TipoOperacion = MODXDOC.VDet[i].Glosa, Moneda = MODXDOC.VDet[i].MonDet, Monto = MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(), MODXDOC.Formato) };
                    documento.LineasComisionGasto.Add(comisionGasto);
                    //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MODXDOC.VDet[i].Glosa,MigrationSupport.FileSystem.TAB(79),MODXDOC.VDet[i].MonDet,MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(),MODXDOC.Formato)});
                    MODXDOC.Pr_Salto_Pagina();
                }
                else if (MODXDOC.VDet[i].tipo != null)
                {
                    break;
                }
            }
            MODXDOC.Pr_Salto_Pagina();
            if (Contador_Arreglo < n)
            {
                //MigrationSupport.Printer.DefInstance.Print("Detalle comisiones y gastos cobrados:");

                MndAnt = "";
                for (i = Contador_Arreglo; i < n; i += 1)
                {
                    var comisionGasto = new ComisionGasto();
                    if (MODXDOC.VDet[i].tipo.TrimB() == "C")
                    {
                        comisionGasto.Tipo = "C";
                        if (!string.IsNullOrEmpty(MndAnt) && MODXDOC.VDet[i].MonDet != MndAnt)
                        {
                            MODXDOC.Pr_Salto_Pagina();
                            comisionGasto.Moneda = MODXDOC.VDet[i].MonDet;
                            MndAnt = MODXDOC.VDet[i].MonDet;
                        }
                        else
                        {
                            comisionGasto.Moneda = MODXDOC.VDet[i].MonDet;
                            MndAnt = MODXDOC.VDet[i].MonDet;
                        }
                        if (MODXDOC.VDet[i].Monto.ToVal() > 0)
                        {
                            comisionGasto.TipoOperacion = MODXDOC.VDet[i].Glosa;
                            comisionGasto.Moneda = MODXDOC.VDet[i].MonDet;
                        }
                        if (i == n)
                        {
                            if (MODXDOC.VDet[i].Monto.ToVal() > 0)
                            {
                                comisionGasto.Monto = MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(), MODXDOC.Formato);
                                //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(),MODXDOC.Formato)});
                            }
                            else
                            {
                                comisionGasto.Monto = new string(' ', 35);
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(88),new string(' ',35)});
                            }
                        }
                        else
                        {
                            if (MODXDOC.VDet[i].Monto.ToVal() > 0)
                            {
                                comisionGasto.Monto = MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(), MODXDOC.Formato);
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(),MODXDOC.Formato)});
                            }
                        }
                        if (i < n)
                        {
                            MODXDOC.Pr_Salto_Pagina();
                        }
                        else
                        {
                            MODXDOC.Pr_SaltoPag();
                        }
                    }
                    else
                    {
                        //break;
                    }
                    documento.LineasComisionGasto.Add(comisionGasto);
                }

                // Printer.Print : Call Pr_SaltoPag
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(65).Column, "Total");
                documento.MonedaTotal = MODXDOC.MonTotal;
                documento.TotalComisionGasto = MODGDOC.forma(MODXDOC.MtoTotal.ToVal(), MODXDOC.Formato);
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(79),MODXDOC.MonTotal,MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.MtoTotal.ToVal(),MODXDOC.Formato)});
            }

        }
        // ****************************************************************************
        //    1.  Despliega la información general para este tipo de carta.
        // ****************************************************************************
        private void Pr_Informacion(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            string s3 = "";
            string s2 = "";
            string s1 = "";
            string s0 = "";
            string p = "";
            int n = 0;


            n = MODXDOC.VDet == null ? 0 : MODXDOC.VDet.GetUpperBound(0);


            if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobCan || MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCanRet)
            {
                n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 1).ToInt();
                if (n > 0)
                {
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 2);
                    s0 = MODFRA.SyGet_Fra(uow, n, "E", p) + 13.Char() + 10.Char();
                }
                else
                {
                    s0 = "";
                }
                if (MODXDOC.VInf.InfMto1.ToVal() > 0)
                {
                    s1 = "Moneda extranjera liquidada: " + MODXDOC.VInf.InfMon1.TrimB() + " " + MODGDOC.forma(MODXDOC.VInf.InfMto1.ToVal(), MODXDOC.Formato).TrimB() + " al tipo de cambio " + MODXDOC.VInf.InfMon2.TrimB() + " " +
                       MODGDOC.forma(MODXDOC.VInf.InfMto2.ToVal(), MODXDOC.Formato).TrimB() + "  =  " + MODXDOC.VInf.InfMon3.TrimB() + " " + MODGDOC.forma(MODXDOC.VInf.InfMto3.ToDbl(), MODXDOC.Formato).TrimB() + 13.Char() + 10.Char();
                }
                if (MODXDOC.FraseVa != 0)
                {
                    s2 = MODFRA.SyGet_Fra(uow, 6703, "E", "") + 13.Char() + 10.Char() + 13.Char() + 10.Char();
                    // s2$ = "Para el retorno creado, sírvase hacernos llegar sus instrucciones para efectuar el pago del mismo, adjuntando la correspondiente declaración de exportación, si procede." + Chr(13) + Chr(10) + Chr(13) + Chr(10)
                }
                else
                {
                    s2 = "";
                }
                if (!string.IsNullOrEmpty(MODXDOC.Instrucciones.TrimB()))
                {
                    s3 = MODXDOC.Instrucciones + 13.Char() + 10.Char();
                }
                else
                {
                    s3 = "";
                }
                s = s0 + s1 + s2 + s3;
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegRet)
            {
                s1 = MODFRA.SyGet_Fra(uow, 6704, "E", "") + 13.Char() + 10.Char() + 13.Char() + 10.Char();
                // s1$ = "A su más pronta conveniencia, sírvase hacernos llegar sus instrucciones para efectuar el pago de este retorno, adjuntando la correspondiente declaración de exportación, si procede." + Chr(13) + Chr(10) + Chr(13) + Chr(10)
                if (!string.IsNullOrEmpty(MODXDOC.Instrucciones.TrimB()))
                {
                    s2 = MODXDOC.Instrucciones + 13.Char() + 10.Char();
                }
                else
                {
                    s2 = "";
                }
                s = s1 + s2;
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocCVD || MODXDOC.VOpe.TipoDoc == MODXDOC.DocArb || MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegPln)
            {
                s = MODXDOC.VInsEsp.InsExp;
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegCanRet)
            {
                if (!string.IsNullOrEmpty(MODXDOC.SaldoRet))
                {
                    s = MODFRA.SyGet_Fra(uow, 6725, "E", MODXDOC.SaldoRet);
                }
                if (!string.IsNullOrEmpty(MODXDOC.Instrucciones))
                {
                    s = s + 13.Char() + 10.Char() + MODXDOC.Instrucciones;
                }
            }
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el primer Párrafo de los documentos.
        // Observaciones  : Coloca un párrafo en el documento dependiendo del
        //                  tipo de carta
        // ****************************************************************************
        private void Pr_Parrafo_1(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta_Aux)
        {
            int i = 0;
            int x = 0;
            string Paso = "";
            string TipoAviso_t = "";
            string Cas = "";
            string s = "";
            string p = "";
            string d = "";

            switch (Carta_Aux)
            {
                case 0:
                case 20:
                    d = MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado + ", " + MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado);
                    d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado);
                    if (MODXDOC.Idioma == "I")
                    {
                        p = MigrationSupport.Utils.Format(MODXDOC.VxCob.FecIng, "mm/dd/yyyy") + "~" + MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1) + "~" + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                    }
                    else
                    {
                        p = MODXDOC.VxCob.FecIng + "~" + MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1) + "~" + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                    }
                    s = MODFRA.SyGet_Fra(uow, 6706, MODXDOC.Idioma, p);
                    // s$ = "Por cuenta de nuestros clientes, adjuntamos los siguientes documentos adicionales para su cobro, los cuales deben consideranse como parte integrante de nuestro envío original del " + Trim$(Glosa_Fecha_Hoy_Espanol()) + ", y ser entregados contra " + CopiarDeString(VxCob.Condicion, ";", 1) + ", a los girados: " + Trim$(PartysOpe(IExp1).NombreUsado)
                    if (!string.IsNullOrEmpty(d))
                    {
                        s = s + ", " + d;
                    }
                    break;
                case 1:
                    if (MODXDOC.Idioma == "I")
                    {
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal))
                        {
                            Cas = "P.O.Box " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                        }
                        d = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado, Cas, MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PaiEnIng(MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado));
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].telefono))
                        {
                            d = d + ", Phone : " + MODXDOC.PartysOpe[MODXDOC.IGir].telefono;
                        }
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].Fax))
                        {
                            d = d + ", Fax : " + MODXDOC.PartysOpe[MODXDOC.IGir].Fax;
                        }
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].Telex))
                        {
                            d = d + ", Telex : " + MODXDOC.PartysOpe[MODXDOC.IGir].Telex;
                        }
                        // P$ = CopiarDeString(VxCob.Condicion, ";", 2) + Trim$(PartysOpe(IGir).NombreUsado)
                        p = MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 2) + ", " + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                        s = MODFRA.SyGet_Fra(uow, 6707, MODXDOC.Idioma, p);
                        // s$ = "On behalf of our Customer, attached find the following documents for collection, which must be delivered to the drawees" + " against " + CopiarDeString(VxCob.Condicion, ";", 2) + Trim$(PartysOpe(IGir).NombreUsado)
                        if (!string.IsNullOrEmpty(d))
                        {
                            s = s + ", " + d;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal))
                        {
                            Cas = "Casilla Postal " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                        }
                        d = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado, Cas, MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado);
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].telefono))
                        {
                            d = d + ", Teléfono : " + MODXDOC.PartysOpe[MODXDOC.IGir].telefono;
                        }
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].Fax))
                        {
                            d = d + ", Fax : " + MODXDOC.PartysOpe[MODXDOC.IGir].Fax;
                        }
                        if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].Telex))
                        {
                            d = d + ", Télex : " + MODXDOC.PartysOpe[MODXDOC.IGir].Telex;
                        }
                        p = MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1) + "~" + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                        s = MODFRA.SyGet_Fra(uow, 6708, MODXDOC.Idioma, p);
                        // s$ = "Por cuenta de nuestros clientes, adjuntamos los siguientes documentos para su cobro, los cuales deben ser entregados contra " + CopiarDeString(VxCob.Condicion, ";", 1) + ", a los girados: " + Trim$(PartysOpe(IGir).NombreUsado)
                        if (!string.IsNullOrEmpty(d))
                        {
                            s = s + ", " + d;
                        }
                    }
                    break;
                case 2:
                    s = MODFRA.SyGet_Fra(uow, 6709, "E", MODXDOC.PartysOpe[MODXDOC.ICob].NombreUsado);
                    // s$ = "De acuerdo a su carta instrucción, hemos procedido al registro y envío de carta de cobro de la cobranza indicada en la referencia, a través del " + PartysOpe(ICob).NombreUsado + ", según el siguiente detalle:"
                    break;
                case 3:
                    p = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.ICob].NombreUsado, MODXDOC.PartysOpe[MODXDOC.ICob].DireccionUsado, MODXDOC.PartysOpe[MODXDOC.ICob].CiudadUsado.TrimB());
                    if (MODXDOC.Idioma == "I")
                    {
                        p = MODXDOC.Concatena(p, MODXDOC.PaiEnIng(MODXDOC.PartysOpe[MODXDOC.ICob].PaisUsado), "");
                    }
                    else
                    {
                        p = MODXDOC.Concatena(p, MODXDOC.PartysOpe[MODXDOC.ICob].PaisUsado, "");
                    }
                    if (!string.IsNullOrEmpty(MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal))
                    {
                        switch (MODXDOC.Idioma)
                        {
                            case "I":
                                Cas = "P.O. Box " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                                break;
                            case "E":
                                Cas = "Casilla Postal " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                                break;
                        }
                    }
                    d = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado, MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado, Cas);
                    d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado, "");
                    d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado);
                    if (MODXDOC.Idioma == "I")
                    {
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PaiEnIng(MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado));
                    }
                    else
                    {
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado);
                    }
                    s = MODFRA.SyGet_Fra(uow, 6710, MODXDOC.Idioma, p + "~" + d);
                    // s$ = "De acuerdo a instrucciones del exportador, hemos procedido al registro y envío de carta de cobro de la cobranza indicada en la referencia, a través del Banco " + Trim$(PartysOpe(ICob).NombreUsado) + " " + Trim$(PartysOpe(ICob).CiudadUsado) + " " + Trim$(PartysOpe(ICob).PaisUsado) + ", a cargo de " + Trim$(PartysOpe(IExp1).NombreUsado) + " " + Trim$(PartysOpe(IExp1).CiudadUsado) + " " + Trim$(PartysOpe(IExp1).PaisUsado)
                    break;
                case 4:
                    s = MODFRA.SyGet_Fra(uow, 6711, "E", "");
                    // s$ = "No es grato informarles, que la(s) letra(s) girada(s) por esta cobranza extrajera, ha(n) sido aceptada(s) por el girado, según el siguiente detalle."
                    break;
                case 5:
                    s = MODFRA.SyGet_Fra(uow, 6712, "E", "");
                    // s$ = "Cúmplenos informarles que, con esta fecha hemos procedido a descargar de nuestros registros la cobranza indicada en la referencia, en atención a que ha sido cancelada en forma directa a ustedes."
                    break;
                case 6:
                    p = MODXDOC.Total_Parcial.TrimB();
                    if (p.InStr("cobranza", 1, StringComparison.CurrentCulture) != 0)
                    {
                        s = MODFRA.SyGet_Fra(uow, 6724, "E", p);
                    }
                    else
                    {
                        if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCanRet)
                        {
                            s = p + " producto de lo cual efectuamos la siguiente distribución:";
                        }
                        else
                        {
                            s = p;
                        }
                    }
                    // s$ = "De acuerdo a sus instrucciones hemos procedido al pago " + Trim$(TipoPago) + " de " + Trim$(PagoDe) + " producto de lo cual efectuamos la siguiente distribución:"
                    break;
                case 7:
                    s = MODFRA.SyGet_Fra(uow, 6714, "E", "");
                    // s$ = "Nos es grato poner en su conocimiento, que con esta fecha hemos procedido a registrar el retorno citado en la referencia, según el siguiente detalle:"
                    break;
                case 8:
                    s = MODFRA.SyGet_Fra(uow, 6715, "E", "");
                    // s$ = "De acuerdo a sus instrucciones, hemos procedido a emitir planillas de ingreso de comercio visible, según el siguiente detalle:"
                    break;
                case 9:
                    switch (MODXDOC.TipoDC.TrimB())
                    {
                        case "D":
                            TipoAviso_t = "débito";
                            break;
                        case "C":
                            TipoAviso_t = "crédito";
                            break;
                    }
                    p = TipoAviso_t.TrimB() + "~" + MODXDOC.NroCtaCte.TrimB() + "~" + MODXDOC.Concepto.TrimB();
                    s = MODFRA.SyGet_Fra(uow, 6716, "E", p);
                    // s$ = "Hemos efectuado el siguiente " + Trim$(TipoAviso_t) + " en su cuenta corriente " + Trim$(NroCtaCte) + " por concepto de " + Trim$(Concepto)
                    break;
                case 10:
                    if (MODXDOC.CuantasCompras != 0 && MODXDOC.CuantasVentas != 0)
                    {
                        Paso = "Compras y Ventas de Divisas, ";
                    }
                    else if (MODXDOC.CuantasCompras != 0)
                    {
                        Paso = "Compras de Divisas, ";
                    }
                    else if (MODXDOC.CuantasVentas != 0)
                    {
                        Paso = "Ventas de Divisas, ";
                    }
                    p = Paso.TrimB();
                    s = MODFRA.SyGet_Fra(uow, 6717, "E", p);
                    // s$ = "De acuerdo a vuestras instrucciones hemos efectuado " + Paso$ + " según el siguiente detalle:"
                    break;
                case 11:
                    s = MODFRA.SyGet_Fra(uow, 6718, "E", "");
                    // s$ = "Hemos efectuado Arbitraje de Divisas, según el siguiente detalle/distribución:"
                    break;
                case 14:
                    //MigrationSupport.Printer.DefInstance.Print();
                    MODXDOC.Pr_Salto_Pagina();
                    s = MODXDOC.Total_Parcial.TrimB();
                    break;
            }

            documento.Parrafo1 = s;
            //MODXDOC.Lineas = new string[1];
            //TODO ARKANO 
            /*x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);

            for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            {
                if (MODXDOC.Lineas[i].TrimB() != "")
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                    MODXDOC.Pr_Salto_Pagina();
                }
            }*/
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el segundo Párrafo de los documentos.
        // Observaciones  : Coloca un párrafo en el documento dependiendo del
        //                  tipo de carta
        // ****************************************************************************
        private void Pr_Parrafo_2(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta_Aux)
        {
            string s = "";
            int i = 0;
            int x = 0;
            string s7 = "";
            string s6 = "";
            string s5 = "";
            string s4 = "";
            string s3 = "";
            string s2 = "";
            string s1 = "";
            string p = "";
            int n = 0;

            //MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            switch (Carta_Aux)
            {
                case 0:
                case 1:
                case 20:
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 1).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 2);
                        s1 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 7).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 8);
                        s2 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 9).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 10);
                        s3 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 11).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 12);
                        s4 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 13).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 14);
                        s5 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 15).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 16);
                        s6 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 25).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 26);
                        s7 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    if (!string.IsNullOrEmpty(s1.TrimB()))
                    {
                        s1 = s1 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s2.TrimB()))
                    {
                        s2 = s2 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s3.TrimB()))
                    {
                        s3 = s3 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s4.TrimB()))
                    {
                        s4 = s4 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s5.TrimB()))
                    {
                        s5 = s5 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s6.TrimB()))
                    {
                        s6 = s6 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s7.TrimB()))
                    {
                        s7 = s7 + 13.Char() + 10.Char();
                    }
                    s1 = s1 + s2 + s3 + s4 + s5 + s6 + s7 + 13.Char() + 10.Char();
                    s2 = "";
                    s3 = "";
                    s4 = "";
                    s5 = "";
                    s6 = "";
                    s7 = "";
                    s1 = s1.TrimB() + MODXDOC.VInsEsp.InsCob.TrimB() + 13.Char() + 10.Char();
                    break;
                case 2:
                    // Frase de las comisiones.-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 3).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 4);
                    s1 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    // Frase del Agente.-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 5).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 6);
                    s2 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    // Frase de Protesto.-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 23).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 24);
                    s3 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    if (!string.IsNullOrEmpty(s1.TrimB()))
                    {
                        s1 = s1 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s2.TrimB()))
                    {
                        s2 = s2 + 13.Char() + 10.Char();
                    }
                    if (!string.IsNullOrEmpty(s3.TrimB()))
                    {
                        s3 = s3 + 13.Char() + 10.Char();
                    }
                    s1 = s1 + s2 + s3 + 13.Char() + 10.Char();
                    s2 = "";
                    s3 = "";
                    s1 = s1.TrimB() + MODXDOC.VInsEsp.InsExp.TrimB() + 13.Char() + 10.Char();
                    // Frase de Solicitamos....-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 19).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 20);
                    s2 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    s1 = s1 + 13.Char() + 10.Char() + s2 + 13.Char() + 10.Char();
                    s2 = "";
                    break;
                case 3:
                    s1 = MODXDOC.VInsEsp.InsAge + 13.Char() + 10.Char();
                    break;
                case 4:
                    s1 = MODXDOC.VInsEsp.InsExp + 13.Char() + 10.Char();
                    break;
                case 5:
                    s1 = MODFRA.SyGet_Fra(uow, 6719, "E", "") + 13.Char() + 10.Char();
                    // s1$ = "Solicitamos revisar cuidadosamente los datos contenidos en esta carta. Si estos no se ajustan a lo instruído por ustedes, sírvase contactarse a la brevedad con nosotros." + Chr(13) + Chr(10)
                    s2 = "";
                    s3 = "";
                    break;
                case 10:
                case 11:
                    s1 = MODFRA.SyGet_Fra(uow, 6720, "E", "");
                    // s1$ = "La moneda extranjera vendida fue distribuida de la siguiente forma:"
                    s2 = "";
                    s3 = "";
                    break;
            }

            s4 = s1 + s2 + s3;
            //TODO ARKANO x = MODXDOC.GetLines(s4,CajaMultilinea,ref MODXDOC.Lineas);
            /*for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            {
                if (MODXDOC.Lineas[i].TrimB() != "")
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                    MODXDOC.Pr_Salto_Pagina();
                }
            }*/
            documento.Paso1 = s4;
            if (Carta_Aux == 3)
            {
                s = MODFRA.SyGet_Fra(uow, 6742, MODXDOC.Idioma, "");
                //MigrationSupport.Printer.DefInstance.Print(s);
            }

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el Pie de Página de los documentos.
        // Observaciones  : Coloca un pie de página en el documento
        // ****************************************************************************
        private void Pr_Pie_Pagina(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int j = 0;
            int x = 0;
            bool restaurar = false;
            string s = "";
            string p = "";
            string fax_eje = "";
            string tel_eje = "";
            string dir_eje = "";
            string nom_eje = "";
            string codofi = "";
            int k = 0;
            int a = 0;
            int n = 0;
            const string Publicacion = " 522.";

            // CFV-10/11/2006
            // Se cambia llamada a otra funcion
            // a% = Fn_Buscar_Indice(UsrEsp.cencos, UsrEsp.CodUsr)
            // 
            // ------------------------------------------------------
            // RealSystems - Código Original - Inicio
            // Fecha Comentado: 20100518
            // Responsable: Mauricio Aguilera V.
            // Version: 1
            // Descripcion: Busca datos del ejecutivo
            // ------------------------------------------------------
            // 
            //  a% = Fn_Buscar_Indice_2(UsrEsp.cencos, UsrEsp.CodUsr, rutiparty, CENTRO_COSTO)
            // CFV
            // 
            //  If a% <> 0 Then
            //     UsrEsp.nombre = UsrEsps(a%).nombre
            //     UsrEsp.Direccion = UsrEsps(a%).Direccion
            //     UsrEsp.Ciudad = UsrEsps(a%).Ciudad
            //     UsrEsp.telefono = UsrEsps(a%).telefono
            //     UsrEsp.Fax = UsrEsps(a%).Fax
            //  End If
            // 
            //  If Idioma = "" Then
            //     Idioma = "E"
            //  End If
            // --------------------------------------------------
            // RealSystems - Código Original - Termino
            // --------------------------------------------------

            MODXDOC.UsrEsps = new MODXDOC.EstrucUsuarios[1];
            n = MODXDOC.UsrEsps.Length;
            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.UsrEsps.GetUpperBound(0);
            //});

            // -------------------------------------------------
            // RealSystems - Código Nuevo - Inicio
            // Fecha Comentado: 20100518
            // Responsable: Mauricio Aguilera V.
            // Version: 1.1
            // Descripcion: Busca datos del ejecutivo
            // -------------------------------------------------

            a = 0;
            for (k = 0; k < n; k += 1)
            {
                if (MODXDOC.UsrEsps[k].cencos == MODXDOC.UsrEsp.cencos && MODXDOC.UsrEsps[k].CodUsr == MODXDOC.UsrEsp.CodUsr && !string.IsNullOrEmpty(MODXDOC.UsrEsps[k].nombre))
                {
                    a = k;
                    break;
                }
            }

            if (a == 0)
            {
                a = MODXDOC.Fn_Buscar_Indice_2(uow, MODXDOC.UsrEsp.cencos, MODXDOC.UsrEsp.CodUsr, Module1.rutiparty, MODFRA.CENTRO_COSTO);
            }
            // --------------------------------------------------
            // RealSystems - Código Nuevo - Termino
            // --------------------------------------------------
            // 
            // 
            // If UsrEsps(a%).nombre = "" Then     Se cambia la condicion del IF  22.06.2010
            if (a == 0 || string.IsNullOrEmpty(MODXDOC.UsrEsps[a].nombre))
            {
                a = MODXDOC.Fn_Buscar_Indice(uow, MODXDOC.UsrEsp.cencos, MODXDOC.UsrEsp.CodUsr);
            }

            MODXDOC.UsrEsp.nombre = MODXDOC.UsrEsps[a].nombre;
            MODXDOC.UsrEsp.Direccion = MODXDOC.UsrEsps[a].Direccion;
            MODXDOC.UsrEsp.Ciudad = MODXDOC.UsrEsps[a].Ciudad;
            MODXDOC.UsrEsp.telefono = MODXDOC.UsrEsps[a].telefono;
            MODXDOC.UsrEsp.Fax = MODXDOC.UsrEsps[a].Fax;

            if (string.IsNullOrEmpty(MODXDOC.Idioma))
            {
                MODXDOC.Idioma = "E";
            }

            // 
            // CFV-10/11/2006
            // Se comenta el llenado de datos del ejecutivo
            //if (Module1.Hab_SGTCliEje != 0)
            //{
                int i;
                int bien = 0;
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
                            //Verificar si existe que el participante tenga más de un ejecutivo.
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
            //}

            if (codofi != "000" && string.IsNullOrEmpty(MODXDOC.UsrEsp.nombre))
            {
                a = MODXDOC.GetDatOfi(uow, MODXDOC.VOpe.NumOpe.Mid(1, 3), MODXDOC.VOpe.NumOpe.Mid(8, 3));
            }

            if (string.IsNullOrEmpty(nom_eje))
            {
                nom_eje = MODXDOC.UsrEsp.nombre.TrimB();
                dir_eje = MODXDOC.UsrEsp.Direccion.TrimB();
                tel_eje = MODXDOC.UsrEsp.telefono.TrimB();
                fax_eje = MODXDOC.UsrEsp.Fax.TrimB();
            }
            
            switch (MODXDOC.Carta)
            {
                // Cartas = Nº 5;6;7
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                    if (MODFRA.RefBae == "")
                    {
                        if (MODXDOC.Carta == 6 || MODXDOC.Carta == 7 || MODXDOC.Carta == 9)
                        {
                            p = MODXDOC.VOpe.NumOpe_t.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();

                        }
                        else
                        {
                            p = MODXDOC.VOpe.NumOpe_t.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                        }
                    }
                    else
                    {
                        p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                    }
                    s = MODFRA.SyGet_Fra(uow, 9002, "E", p);
                    break;
                default:
                    // Carta = N º1
                    if (MODXDOC.Carta == 1 || MODXDOC.Carta == 20)
                    {
                        if (MODXDOC.Idioma == "I")
                        {
                            if (MODFRA.RefBae == "")
                            {
                                p = nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + MODXDOC.VOperac.ConRaya.TrimB() + "~" + Publicacion;
                            }
                            else
                            {
                                p = nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + MODFRA.RefBae.TrimB() + "~" + Publicacion;
                            }
                            s = MODFRA.SyGet_Fra(uow, 9003, MODXDOC.Idioma, p);
                        }
                        else
                        {
                            if (MODFRA.RefBae == "")
                            {
                                // modificado por MPN
                                p = MODXDOC.VOperac.ConRaya.TrimB() + "~" + nom_eje.TrimB() + " " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                            }
                            else
                            {
                                //  modificado por MPN
                                p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                            }
                            s = MODFRA.SyGet_Fra(uow, 9001, MODXDOC.Idioma, p);
                        }
                    }
                    else if (MODXDOC.Carta == 14)
                    {
                        if (MODFRA.RefBae == "")
                        {
                            //  modificado por MPN
                            // p$ = Trim$(Referencia) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                            p = MODXDOC.Referencia.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                        }
                        else
                        {
                            //  modificado por MPN
                            // p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                            p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                        }
                        s = MODFRA.SyGet_Fra(uow, 9002, "E", p);
                        // Carta siguientes
                    }
                    else
                    {
                        if (MODXDOC.VOperac.ConRaya == "")
                        {
                            restaurar = true;
                            MODXDOC.VOperac.ConRaya = MODXDOC.VOpe.NumOpe_t;
                        }
                        switch (MODXDOC.VOperac.ConRaya.Mid(5, 2))
                        {
                            case "10":
                            case "20":
                            case "17":
                                if (MODFRA.RefBae == "")
                                {
                                    //  modificado por MPN
                                    // p$ = Trim$(VOperac.ConRaya) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                                    p = MODXDOC.VOperac.ConRaya.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                                }
                                else
                                {
                                    //  modificado por MPN
                                    //  p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                                    p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                                }
                                s = MODFRA.SyGet_Fra(uow, 9002, "E", p);
                                break;
                            default:
                                if (MODFRA.RefBae == "")
                                {
                                    //  modificado por MPN
                                    // p$ = Trim$(VOperac.ConRaya) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                                    p = MODXDOC.VOperac.ConRaya.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                                }
                                else
                                {
                                    //  modificado por MPN
                                    // p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                                    p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                                }
                                s = MODFRA.SyGet_Fra(uow, 9004, "E", p);
                                break;
                        }
                        // s$ = "En toda correspondencia sírvase citar nuestro número " + Trim$(VOperac.ConRaya) + " y remitida a la atención de " + Trim$(UsrEsp.Nombre) + " " + Trim$(UsrEsp.Direccion) + ", fono " + Trim$(UsrEsp.Telefono) + ", fax " + Trim$(UsrEsp.Fax) + ". Esta cobranza se acoge a las Reglas y Costumbres de la Cámara Internacional de Comercio, publicación Nº" + Publicacion
                        if (restaurar)
                        {
                            MODXDOC.VOperac.ConRaya = "";
                        }
                    }
                    break;
            }

            documento.PiePagina = s;


            //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
            /*for (j = 1; j <= MODXDOC.Lineas.GetUpperBound(0); j += 1)
            {
                if (j == 1)
                {
                    MigrationSupport.Printer.DefInstance.Print();
                    MODXDOC.Pr_SaltoPag();
                }
                MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[j]);
                MODXDOC.Pr_SaltoPag();
            }*/

            // FTF:22032001, Direccion Internet Negocios Internacionales
            //TODO:Revisar, se comenta pero no se deberia de comentar
            //s = MODFRA.SyGet_Fra(uow, 9900, MODXDOC.Idioma, "");

            //MigrationSupport.Printer.DefInstance.Print(s);

        }
        public string Obtiene_NomEsp(object codofi, string CodEsp)
        {
            string Obtiene_NomEsp = "";
            int i = 0;
            int fin = 0;
            Obtiene_NomEsp = "";

            fin = 0;
            fin = Module1.VEjc.GetUpperBound(0);

            for (i = 1; i <= fin; i += 1)
            {
                if (Module1.VEjc[i].codofi == codofi.ToStr() && Module1.VEjc[i].codejc == CodEsp)
                {
                    Obtiene_NomEsp = Minuscula2(Module1.VEjc[i].nombre);
                    break;
                }
            }
            return Obtiene_NomEsp;
        }
        // Deja un string de varias palabras separadas por un blanco como la primera
        // letra en Mayúscula y el resto de la palabra en minúscula.-
        public string Minuscula2(string PDato)
        {
            string Minuscula2 = "";

            string strCaseArg = "";
            int j = 0;
            string s = "";
            int i = 0;
            string[] Palabras = null;
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

        public void Pr_Cargar_Espec(System.Windows.Forms.Control Cbo_Us, string oficina, string tipejc)
        {
            string s = "";
            int i = 0;
            int n = 0;

            // 
            // carga Lista Cbo_Us

            n = Module1.VEjc.GetUpperBound(0);


            // 

            ((dynamic)Cbo_Us).Clear();
            if (n > 0)
            {
                for (i = 1; i <= n; i += 1)
                {
                    if (Module1.VEjc[i].codofi == oficina && Module1.VEjc[i].tipo == tipejc)
                    {
                        s = "";
                        s = s + MigrationSupport.Utils.Format(Module1.VEjc[i].codofi, "000") + "-";
                        s = s + MigrationSupport.Utils.Format(Module1.VEjc[i].codejc, "000") + " : ";
                        s = s + Minuscula2(Module1.VEjc[i].nombre);

                        ((dynamic)Cbo_Us).AddItem(s);
                        //TODO ARKANO ((dynamic)Cbo_Us).ItemData(((object)((dynamic)Cbo_Us).NewIndex())) = i;

                    }
                }

                // If Cbo_Us.ListCount > 0 Then Cbo_Us.Selected(0) = True
            }

        }

        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Banco Cobrador......Girador
        // Observaciones  : Contiene Párrafos, Documentos (letras, etc),
        //                  Instrucciones al Bco. Cobrador, Pie de Página
        // ****************************************************************************
        private void Pr_Principal_1(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int m = 0;
            int n = 0;

            n = MODXDOC.VxLet.GetUpperBound(0);
            m = MODXDOC.VxDem.GetUpperBound(0);

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 20;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.ICob, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            MODXDOC.Pr_Instrucciones(MODXDOC.Num_Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Num_Carta);     // Segundo Párrafo.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Septiembre 1995
        // Propósito      : Llamada desde Carta:....
        // Observaciones  : Contiene Párrafos, Montos, Pie de Página, etc.
        // ****************************************************************************
        public void Pr_Principal_10(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = MODXDOC.VMontos.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VMontos.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 9;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Mto_DebitoCredito(documento);     // Documentos.
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Octubre 1995
        // Propósito      : Llamada desde Carta: 620.
        // ****************************************************************************
        public void Pr_Principal_11(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int p = MODXDOC.VPlan.Length;
            int e = MODXDOC.VxOri.Length;
            int d = MODXDOC.VxVia.Length;
            int c = MODXDOC.VxRemesa.Length;
            int b = MODXDOC.Ventas.Length;
            int a = MODXDOC.Compras.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    a = MODXDOC.Compras.GetUpperBound(0);
            //    b = MODXDOC.Ventas.GetUpperBound(0);
            //    c = MODXDOC.VxRemesa.GetUpperBound(0);
            //    d = MODXDOC.VxVia.GetUpperBound(0);
            //    e = MODXDOC.VxOri.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 10;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (a > 0)
            {
                MODXDOC.Pr_Compras(documento);     // Compras.
            }
            if (b > 0)
            {
                MODXDOC.Pr_Ventas(documento);     // Ventas.
            }
            if (c > 0)
            {
                Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Segundo Párrafo.
                MODXDOC.Pr_Remesa(documento);     // Remesas.
            }
            if (d > 0 || e > 0)
            {
                Pr_Abonos(documento);     // Cargos y Abonos.
            }

            Pr_Cta_Cte(documento, uow, MODXDOC.DocCVD);     // Cuenta Corriente(Débitos).


            if (p > 0)
            {
                //MigrationSupport.Printer.DefInstance.Print("Planilla(s) Invisible(s)");
                for (i = 1; i <= p; i += 1)
                {
                    //MigrationSupport.Printer.DefInstance.Print(MODXDOC.VPlan[i].NroPlan.TrimB());
                }
            }

            if (!string.IsNullOrEmpty(MODXDOC.VInsEsp.InsExp))
            {
                Pr_Informacion(documento, uow);     // Información.
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Octubre 1995
        // Propósito      : Llamada desde Carta:621.
        // ****************************************************************************
        public void Pr_Principal_12(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int p = 0;
            int d = MODXDOC.VxOri.Length;
            int c = MODXDOC.VxVia.Length;
            int b = MODXDOC.VxRemesa.Length;
            int a = MODXDOC.VArb.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    a = MODXDOC.VArb.GetUpperBound(0);
            //    b = MODXDOC.VxRemesa.GetUpperBound(0);
            //    c = MODXDOC.VxVia.GetUpperBound(0);
            //    d = MODXDOC.VxOri.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 11;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (a > 0)
            {
                MODXDOC.Pr_Arbitrajes(documento);     // Arbitraje.
            }
            if (b > 0)
            {
                Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Segundo Párrafo.
                MODXDOC.Pr_Remesa(documento);     // Remesas.
            }
            if (d > 0)
            {
                Pr_Abonos(documento);     // Cargos y Abonos.
            }
            Pr_Cta_Cte(documento, uow, MODXDOC.DocArb);     // Cuenta Corriente(Débitos).

            if (p > 0)
            {
                //MigrationSupport.Printer.DefInstance.Print("Planilla(s) Invisible(s)");
                for (i = 1; i <= p; i += 1)
                {
                    //MigrationSupport.Printer.DefInstance.Print(MODXDOC.VPlan[i].NroPlan.TrimB());
                }
            }

            if (!string.IsNullOrEmpty(MODXDOC.VInsEsp.InsExp))
            {
                Pr_Informacion(documento, uow);     // Información.
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 611.
        // Observaciones  : Contiene Párrafos, Distribuciones, Información General,
        //                  Abonos y/o Cargos.
        // ****************************************************************************
        public void Pr_Principal_14(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int p = MODXDOC.VPlan.Length;
            int m = MODXDOC.VDet.Length;
            int n = MODXDOC.VDist.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDist.GetUpperBound(0);
            //    m = MODXDOC.VDet.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 14;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Distribucion(documento);     // Distribuciones.
            }
            Pr_Informacion(documento, uow);     // Información.
            if (p > 0)
            {
                Pr_Parrafo_1(documento, uow, 8);     // Primer Párrafo.
                MODXDOC.Pr_Planilla(documento);     // Planillas.
            }
            MODXDOC.Pr_Titulo_Abono();     // Título de Abonos y/o Cargos.
            if (m > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxCobCan);     // Cuenta Corriente(Débitos).
            }
            if (MODXDOC.TipCam != 0)
            {
                s = MODFRA.SyGet_Fra(uow, 8804, "E", MODGDOC.forma(MODXDOC.TipCam, MODXDOC.FormatoTipCamb));
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            documento.NuestraReferencia = "Nuestra Referencia";
            documento.NuestraReferenciaValue = MODXDOC.Referencia.TrimB();

            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Banco Cobrador......Girador
        // Observaciones  : Contiene Párrafos, Montos (monto, intereses, gastos,
        //                  totales, vencimientos), Documentos (letras, etc),
        //                  Tipo de Embarque, Instrucciones al Bco. Cobrador,
        //                  Pie de Página
        // ****************************************************************************
        public void Pr_Principal_2(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int m = MODXDOC.VxDem.Length;
            int n = MODXDOC.VxLet.Length;

            /*string FileName = "dale3.ps";            
            MigrationSupport.Printer.DefInstance.PrintFileName = FileName;
            MigrationSupport.Printer.DefInstance.PrintAction = System.Drawing.Printing.PrintAction.PrintToFile;*/

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxLet.GetUpperBound(0);
            //    m = MODXDOC.VxDem.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 1;
            MODXDOC.Num_Carta = MODXDOC.Carta;

            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.ICob, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            MODXDOC.Pr_Montos(documento, MODXDOC.Carta);
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            Pr_Tipo_Embarque(documento, uow, MODXDOC.Carta);     // Detalle de Tipo de Embarque.
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }

            //MigrationSupport.Printer.DefInstance.EndDoc();manda imprimir

            //Ghostscript(FileName);
        }

        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Exportador......Girado
        // Observaciones  : Contiene Párrafos, Montos (montos, intereses, gastos,
        //                  totales, vencimientos), Documentos (letras, etc),
        //                  Tipo de Embarque, Información al Exportador,
        //                  Pie de Página
        // ****************************************************************************
        public void Pr_Principal_3(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int p = MODXDOC.VDet.Length;
            int m = MODXDOC.VxDem.Length;
            int n = MODXDOC.VxLet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxLet.GetUpperBound(0);
            //    m = MODXDOC.VxDem.GetUpperBound(0);
            //    p = MODXDOC.VDet.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 2;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            MODXDOC.Pr_Montos(documento, MODXDOC.Carta);
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            Pr_Tipo_Embarque(documento, uow, MODXDOC.Carta);     // Detalle de Tipo de Embarque.
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            if (p > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxCobReg);     // Cuenta Corriente(Débitos).
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Agente......Exportador
        // Observaciones  : Contiene Párrafos, Montos (montos, intereses, gastos,
        //                  totales, vencimientos), Documentos (letras, etc),
        //                  Tipo de Embarque, Información al Agente,
        //                  Pie de Página
        // ****************************************************************************
        public void Pr_Principal_4(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int m = MODXDOC.VxDem.Length;
            int n = MODXDOC.VxLet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxLet.GetUpperBound(0);
            //    m = MODXDOC.VxDem.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 3;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IAge, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            MODXDOC.Pr_Montos(documento, MODXDOC.Carta);
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            Pr_Tipo_Embarque(documento, uow, MODXDOC.Carta);     // Detalle de Tipo de Embarque.
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            //Pr_Pie_Pagina() Tomar en cuenta que puede ser necesario llamarlo para el impresor antiguo.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Exportador......Girado
        // Observaciones  : Contiene Párrafos, Detalle de aceptación (letras, etc),
        //                  Información al Exportador, Pie de Página
        // ****************************************************************************
        public void Pr_Principal_5(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = 0;

            n = MODXDOC.VxLet.GetUpperBound(0);

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 4;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Detalle(documento);     // Detalle de aceptación.
            }
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }/**/
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 610.
        // Observaciones  : Contiene Párrafos, Comisiones y Gastos Cobrados.
        // ****************************************************************************
        public void Pr_Principal_6(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = MODXDOC.VDet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDet.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 5;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado sólo para el caso de la carta Exportador.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxPagDir);     // Cuenta Corriente(Débitos).
            }
            // Call Pr_Parrafo_2(Carta)    'Segundo Párrafo.
            Pr_Texto_Libre();     // Despliegue de Texto Libre del Usuario.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 611.
        // Observaciones  : Contiene Párrafos, Distribuciones, Información General,
        //                  Abonos y/o Cargos.
        // ****************************************************************************
        public void Pr_Principal_7(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int p = MODXDOC.VPlan.Length;
            int m = MODXDOC.VDet.Length;
            int n = MODXDOC.VDist.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDist.GetUpperBound(0);
            //    m = MODXDOC.VDet.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 6;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Distribucion(documento);     // Distribuciones.
            }
            Pr_Informacion(documento, uow);     // Información.
            // If p% > 0 Then
            //     Call Pr_Planilla        'Planillas.
            // End If
            if (p > 0)
            {
                Pr_Parrafo_1(documento, uow, 8);     // Primer Párrafo.
                MODXDOC.Pr_Planilla(documento);     // Planillas.
            }
            MODXDOC.Pr_Titulo_Abono();     // Título de Abonos y/o Cargos.
            if (m > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxCobCan);     // Cuenta Corriente(Débitos).
            }
            if (MODXDOC.TipCam != 0)
            {
                s = MODFRA.SyGet_Fra(uow, 8804, "E", MODGDOC.forma(MODXDOC.TipCam, MODXDOC.FormatoTipCamb));
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 0; i <= MODXDOC.Lineas.Length; i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 612.
        // Observaciones  : Contiene Párrafos, Detalle, Información General,
        // ****************************************************************************
        public void Pr_Principal_8(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = MODXDOC.VDetalle.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDetalle.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 7;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado sólo para el caso de la carta Exportador.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Ordenante(documento);     // Distribuciones.
            }
            Pr_Informacion(documento, uow);     // Información.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 613.
        // Observaciones  : Contiene Párrafos, Detalle, Información General,
        // ****************************************************************************
        public void Pr_Principal_9(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int e = MODXDOC.VxOri == null ? 0 : MODXDOC.VxOri.Length;
            int d = MODXDOC.VxVia == null ? 0 : MODXDOC.VxVia.Length;
            int m = MODXDOC.VDet.Length;
            int n = MODXDOC.VPlan.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VPlan.GetUpperBound(0);
            //    m = MODXDOC.VDet.GetUpperBound(0);
            //    d = MODXDOC.VxVia.GetUpperBound(0);
            //    e = MODXDOC.VxOri.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 8;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado sólo para el caso de la carta Exportador.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Planilla(documento);     // Planillas.
            }
            if (d > 0 || e > 0)
            {
                Pr_Abonos(documento);     // Cargos y Abonos.
            }
            if (m > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxRegPln);     // Cuenta Corriente(Débitos).
            }
            Pr_Informacion(documento, uow);     // Información.
            MODXDOC.Pr_Salto_Pagina();
            if (MODXDOC.TipCam != 0)
            {
                s = MODFRA.SyGet_Fra(uow, 8804, "E", MODGDOC.forma(MODXDOC.TipCam, MODXDOC.FormatoTipCamb));
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                {
                    if (!string.IsNullOrEmpty(MODXDOC.Lineas[i].TrimB()))
                    {
                        //MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                    }
                }
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        //    1.  Es el despliegue del texto libre que existe en la carta al Exportador.
        // ****************************************************************************
        private void Pr_Texto_Libre()
        {
            int i = 0;
            int x = 0;
            string s = "";

            // Orieta.
            // Printer.Print : Call Pr_Salto_Pagina
            // Orieta.

            if (!string.IsNullOrEmpty(MODXDOC.Instrucciones.TrimB()))
            {
                s = MODXDOC.Instrucciones + 13.Char() + 10.Char();
            }
            else
            {
                s = "";
            }
            //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
            //for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            //{
            //    if (MODXDOC.Lineas[i].TrimB() != "")
            //    {
            //        // Orieta.
            //        if (i == 1)
            //        {
            //            MigrationSupport.Printer.DefInstance.Print();
            //            MODXDOC.Pr_Salto_Pagina();
            //        }
            //        // Orieta.
            //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
            //        MODXDOC.Pr_Salto_Pagina();
            //    }
            //}
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el Tipo de Embarque.
        // Observaciones  : Coloca un texto semi - fijo con la descripción de la
        //                  Mercadería, para luego continuar con el detalle del
        //                  embarque.
        // ****************************************************************************
        private void Pr_Tipo_Embarque(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta_Aux)
        {
            int i = 0;
            int x = 0;
            string s = "";
            string s3 = "";
            string s2 = "";
            int m = 0;
            string s1 = "";
            string p = "";
            int n = MODXDOC.VxCem.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxCem.GetUpperBound(0);
            //});

            // Al Exportador.-
            if (Carta_Aux == 2)
            {

                // Condición de Entrega.-
                p = MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1).TrimB();
                s1 = MODFRA.SyGet_Fra(uow, 6723, "E", p);
                //MigrationSupport.Printer.DefInstance.Print(s1.TrimB());
                // Frase de Devolución.-
                m = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 21).ToInt();
                if (m > 0)
                {
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 22);
                    s2 = MODFRA.SyGet_Fra(uow, m, "E", p) + 13.Char() + 10.Char();
                    // Printer.Print s$: Call Pr_Salto_Pagina
                }

                // Frase de Protesto.-
                m = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 17).ToInt();
                p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 18);
                s3 = MODFRA.SyGet_Fra(uow, m, "E", p);
                // Printer.Print s$: Call Pr_Salto_Pagina

                s = s2 + s3;
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}


            }

            // Mercadería.
            if (!string.IsNullOrEmpty(MODXDOC.VxCob.Mercad_t))
            {
                switch (Carta_Aux)
                {
                    case 1:
                    case 3:
                        if (MODXDOC.Idioma == "I")
                        {
                            p = MODXDOC.VxCob.Mercad_t.TrimB();
                            s1 = MODFRA.SyGet_Fra(uow, 6721, "I", p);
                            // s1$ = "This collection cover shipment of " + Trim$(VxCob.Mercad_t)
                        }
                        else
                        {
                            p = MODXDOC.VxCob.Mercad_t.TrimB();
                            s1 = MODFRA.SyGet_Fra(uow, 6722, "E", p);
                            // s1$ = "Esta cobranza cubre el embarque de :   " + Trim$(VxCob.Mercad_t)
                        }
                        break;
                    default:
                        p = MODXDOC.VxCob.Mercad_t.TrimB();
                        s1 = MODFRA.SyGet_Fra(uow, 6722, "E", p);
                        // s1$ = "Esta cobranza cubre el embarque de :   " + Trim$(VxCob.Mercad_t)
                        break;
                }

                //TODO ARKANO x = MODXDOC.GetLines(s1,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 0; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}
            }
            documento.Paso2 = s1;
            // Conocimientos de Embarque.
            if (n != 0)
            {
                switch (Carta_Aux)
                {
                    case 1:
                    case 3:
                        if (MODXDOC.Idioma == "I")
                        {
                            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Document Nr.");
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(37).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Shipment Date");
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(55).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Loading Port");
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(94).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Discharge Port");
                            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        }
                        else
                        {
                            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("C. Embarque");
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(37).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("F. Embarque");
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(55).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Embarque");
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(94).Column);
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Destino");
                            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        }
                        break;
                    default:
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("C. Embarque");
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(37).Column);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("F. Embarque");
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(55).Column);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Embarque");
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(94).Column);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Destino");
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        break;
                }
                MODXDOC.Pr_Salto_Pagina();
                for (i = 0; i < n; i += 1)
                {
                    if (!string.IsNullOrEmpty(MODXDOC.VxCem[i].NumCem.TrimB()))
                    {
                        var embarque = new Embarque();
                        embarque.Codigo = MODXDOC.VxCem[i].NumCem.Mid(1, 30).TrimB();
                        // Para la carta en inglés el formato de la fecha es mm/dd/yyyy.
                        switch (Carta_Aux)
                        {
                            case 1:
                            case 3:
                                if (MODXDOC.Idioma == "I")
                                {
                                    embarque.Fecha = MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "mm/dd/yyyy").TrimB();
                                }
                                else
                                {
                                    embarque.Fecha = MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "dd/mm/yyyy").TrimB();
                                }
                                break;
                            default:
                                embarque.Fecha = MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "dd/mm/yyyy").TrimB();
                                break;
                        }
                        embarque.Origen = MODXDOC.VxCem[i].EmbDes.Mid(1, 30).TrimB();
                        embarque.Destino = MODXDOC.VxCem[i].EmbHac.Mid(1, 30).TrimB();
                        documento.LineasEmbarque.Add(embarque);
                    }
                }
            }

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
