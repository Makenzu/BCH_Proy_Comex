using BCH.Comex.Common;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XEGI.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public class MODPRD
    {

        // Lectura de la Productividad de los Especialistas.-
        public static bool Sygetn_Prd(string codCct, string codUsr, int mes, int anio, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                bool returnValue = false;

                try
                {
                    globales.Vprd = new List<DatosGlobales.T_Prd>();
                    var result = uow.SceRepository.sce_prd_i01_MS(MODGSYB.dbcharSy(codCct), MODGSYB.dbcharSy(codUsr), mes, anio);

                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            DatosGlobales.T_Prd prd = new DatosGlobales.T_Prd();

                            prd.CodCct = item.cod_cct;
                            prd.CodUsr = item.cod_usr;
                            prd.nombre_esp = item.nombre_esp;
                            prd.numcob = item.num_cob;
                            prd.numret = item.num_ret;
                            prd.numpli = item.num_pli;
                            prd.numplv = item.num_plv;
                            prd.numdec = item.num_dec;
                            prd.num_gl = item.num_gl;
                            prd.numcce = item.num_cce;

                            globales.Vprd.Add(prd);
                        }
                        returnValue = true;
                    }
                    else if (result == null)
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Se ha producido un error al tratar de leer la(s) Tabla(s) de Prod..",
                            Type = TipoMensaje.Error,
                            Title = MODGUSR.MsgUsr
                        });
                        tracer.TraceError("Se ha producido un error al tratar de leer la(s) Tabla(s) de Prod.");
                        return returnValue;
                    }

                    return returnValue;
                }
                catch (Exception ex)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer la(s) Tabla(s) de Prod. : " + ex.Message,
                        Type = TipoMensaje.Error,
                        Title = "Error"
                    });
                    tracer.TraceException("Se ha producido un error al tratar de leer la(s) Tabla(s) de Prod.", ex);
                    throw ex;
                }
            }


        }



        // ***********************************************************************
        //    1.  Imprime Producción Com.Ext.                    *
        // ***********************************************************************
        public static string Prn_Prod(string CodCct, string CodUsr, string periodo, DatosGlobales globales)
        {

            Printer printer = new Printer();

            string Esp = "";
            int s_cce = 0;
            int s_gl = 0;
            int s_dec = 0;
            int s_plv = 0;
            int s_pli = 0;
            int s_ret = 0;
            int s_cob = 0;
            int pasa = 0;
            string Separa = "------------------------------";
            int i = 0;
            int num_p = 0;

            num_p = 1;
            i = 1;

            // printer.PrintList(": 20", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "20"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(io.MODGSWF.VGSwf.NumOpe));


            //printer.Font = MigrationSupport.Utils.FontChangeItalic(printer.Font, false);
            //printer.Font = MigrationSupport.Utils.FontChangeUnderline(printer.Font, false);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, true);
            //printer.Print();

            GetEnc_Prn_Prod(ref printer, CodCct, CodUsr, periodo, num_p, globales);
            //// realiza la impresion encabezado
            //printer.PrintList(new object[] { Printer.TAB(1),"Comercio Exterior", Printer.TAB(25),"PRODUCTIVIDAD EXPORTACIONES", Printer.TAB(60),"Fecha  : ",
            //Printer.TAB(69), DateTime.Now.ToString("dd/MM/yyyy")});
            //printer.PrintList(new object[] { Printer.TAB(25), "---------------------------", Printer.TAB(60), "Página : ", Printer.TAB(69), num_p });
            ////printer.Print();
            //printer.PrintList(new object[] { Printer.TAB(1), "Lider   : ", Printer.TAB(11), CodCct + "-" + CodUsr, Printer.TAB(18), globales.UsrEsp.nombre });
            //printer.PrintList(new object[] { Printer.TAB(1), "Periodo : ", Printer.TAB(11), periodo });
            ////printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, false);
            //Separa = "------------------------------";
            //printer.PrintList(new object[] { Separa, Separa, Separa });
            //printer.PrintList(new object[]{Printer.TAB(1),"Especialista ",Printer.TAB(20),"Nombre Especialista",Printer.TAB(46),"#COB",Printer.TAB(51),"#RET",
            //Printer.TAB(56),"#PLI",Printer.TAB(61),"#PLV",Printer.TAB(66),"#DEC",Printer.TAB(71),"#GL",Printer.TAB(76),"#CCE"});
            //printer.PrintList(new object[] { Separa, Separa, Separa });
            ////printer.Print();

            // Impresión de información

            pasa = 1;
            s_cob = 0;
            s_ret = 0;
            s_pli = 0;
            s_plv = 0;
            s_dec = 0;
            s_gl = 0;
            s_cce = 0;



            foreach (var item in globales.Vprd)
            {

                s_cob = s_cob + item.numcob;
                s_ret = s_ret + item.numret;
                s_pli = s_pli + item.numpli;
                s_plv = s_plv + item.numplv;
                s_dec = s_dec + item.numdec;
                s_gl = s_gl + item.num_gl;
                s_cce = s_cce + item.numcce;
                Esp = item.CodCct + "-" + item.CodUsr;

                printer.PrintList(Printer.TAB(3), Esp, Printer.TAB(15), item.nombre_esp.Trim(), Printer.TAB(46), ("0000" + item.numcob.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(51), ("0000" + item.numret.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(56), ("0000" + item.numpli.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(61), ("0000" + item.numplv.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(66), ("0000" + item.numdec.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(71), ("0000" + item.num_gl.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(76), ("0000" + item.numcce.ToString().Trim()).PadRight(4));


                i = i + 1;
                pasa = pasa + 1;

                if (pasa > 40 && i < globales.Vprd.Count())
                {
                    // lleno% = 1
                    printer.Print();
                    printer.PrintList(Separa, Separa, Separa);
                    printer.PrintList(Printer.TAB(46), ("0000" + s_cob.ToString().Trim()).PadRight(4), Printer.TAB(51), ("0000" +
                        s_ret.ToString().Trim()).PadRight(4), Printer.TAB(56), ("0000" +
                        s_pli.ToString().Trim()).PadRight(4), Printer.TAB(61), ("0000" +
                        s_plv.ToString().Trim()).PadRight(4), Printer.TAB(66), ("0000" +
                        s_dec.ToString().Trim()).PadRight(4), Printer.TAB(71), ("0000" +
                        s_gl.ToString().Trim()).PadRight(4), Printer.TAB(76), ("0000" +
                        s_cce.ToString().Trim()).PadRight(4));

                    printer.PrintList(Separa, Separa, Separa);
                    printer.Print();
                    printer.PrintList(Printer.TAB(1), "#COB:", Printer.TAB(6), "Cobranza Exportaciones");
                    printer.PrintList(Printer.TAB(1), "#RET:", Printer.TAB(6), "Retorno Exportaciones");
                    printer.PrintList(Printer.TAB(1), "#PLI:", Printer.TAB(6), "Planillas Invisibles");
                    printer.PrintList(Printer.TAB(1), "#PLV:", Printer.TAB(6), "Planillas Visibles");
                    printer.PrintList(Printer.TAB(1), "#DEC:", Printer.TAB(6), "Declaración Exportación");
                    printer.PrintList(Printer.TAB(1), "#GL :", Printer.TAB(6), "Contabilidad Genérica");
                    printer.PrintList(Printer.TAB(1), "#CCE:", Printer.TAB(6), "Carta Crédito");
                    printer.NewPage();
                    num_p = num_p + 1;
                    pasa = 1;
                    //goto enca;
                    GetEnc_Prn_Prod(ref printer, CodCct, CodUsr, periodo, num_p, globales);

                }

            }


            if (globales.Vprd.Count() > i)
            {
                printer.NewPage();
                num_p = num_p + 1;
                // imprime nuevamente encabezado para nueva página.-
                GetEnc_Prn_Prod(ref printer, CodCct, CodUsr, periodo, num_p, globales);
            }
            else if (pasa < 40)
            {
                // término de impresión
                //printer.Print();
                printer.PrintList(Separa, Separa, Separa);

                printer.PrintList(Printer.TAB(46), ("0000" + s_cob.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(51), ("0000" + s_ret.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(56), ("0000" + s_pli.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(61), ("0000" + s_plv.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(66), ("0000" + s_dec.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(71), ("0000" + s_gl.ToString().Trim()).PadRight(4));
                printer.PrintList(Printer.TAB(76), ("0000" + s_cce.ToString().Trim()).PadRight(4));

                printer.PrintList(Separa, Separa, Separa);
                printer.Print();
                printer.PrintList(Printer.TAB(1), "#COB : ", Printer.TAB(8), "Cobranza Exportaciones");
                printer.PrintList(Printer.TAB(1), "#RET : ", Printer.TAB(8), "Retorno Exportaciones");
                printer.PrintList(Printer.TAB(1), "#PLI : ", Printer.TAB(8), "Planillas Invisibles");
                printer.PrintList(Printer.TAB(1), "#PLV : ", Printer.TAB(8), "Planillas Visibles");
                printer.PrintList(Printer.TAB(1), "#DEC : ", Printer.TAB(8), "Declaración Exportación");
                printer.PrintList(Printer.TAB(1), "#GL  : ", Printer.TAB(8), "Contabilidad Genérica");
                printer.PrintList(Printer.TAB(1), "#CCE : ", Printer.TAB(8), "Carta Crédito");

                printer.EndDoc();

            }

            return printer.ToString();
        }

        private static void GetEnc_Prn_Prod(ref Printer printer, string CodCct, string CodUsr, string periodo, int num_p, DatosGlobales globales)
        {
            string Separa = "";

            // realiza la impresion encabezado
            printer.PrintList(Printer.TAB(1),"Comercio Exterior", Printer.TAB(25),"PRODUCTIVIDAD EXPORTACIONES", Printer.TAB(60),"Fecha  : ",
            Printer.TAB(69), DateTime.Now.ToString("dd/MM/yyyy"));
            printer.PrintList(Printer.TAB(25), "---------------------------", Printer.TAB(60), "Página : ", Printer.TAB(69), num_p);
            printer.Print();
            printer.PrintList(Printer.TAB(1), "Lider   : ", Printer.TAB(11), CodCct + "-" + CodUsr, Printer.TAB(18), globales.UsrEsp.nombre);
            printer.PrintList(Printer.TAB(1), "Periodo : ", Printer.TAB(11), periodo);
            //printer.Font = MigrationSupport.Utils.FontChangeBold(printer.Font, false);
            Separa = "------------------------------";
            printer.PrintList(Separa, Separa, Separa);
            printer.PrintList(Printer.TAB(1),"Especialista ",Printer.TAB(20),"Nombre Especialista",Printer.TAB(46),"#COB",Printer.TAB(51),"#RET",
            Printer.TAB(56),"#PLI",Printer.TAB(61),"#PLV",Printer.TAB(66),"#DEC",Printer.TAB(71),"#GL",Printer.TAB(76),"#CCE");
            printer.PrintList(Separa, Separa, Separa);
            printer.Print();


        }

    }
}
