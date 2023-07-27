using BCH.Comex.Core.BL.XEGI.Forms;
using BCH.Comex.Core.BL.XEGI.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
/*using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;*/
using System;
using System.Collections.Generic;
namespace BCH.Comex.Core.BL.XEGI
{
    public class XegiService
    {
        private const string DOCUMENTO_ACEPTACION_LETRAS_602  = "Aceptacion Letras Cobranza Exportacion.htm";

        public DocumentoReporteModel Documento { get; set; }
        private UnitOfWorkCext01 uow;
        private sce_xdoc sce_xdoc;


        public XegiService()
        {
            uow = new UnitOfWorkCext01();
            //Documento = new AbonoCargoResultDTO();
        }

        public DocumentoReporteModel ProcesaComando(string numeroOperacion, decimal coddoc, decimal nrocor, bool primero, bool segundo, bool tercero, bool cuarto)
        {            
            string directory = System.AppDomain.CurrentDomain.BaseDirectory + "\\html\\";
            ServxDoc servxDoc = new ServxDoc();
            sce_xdoc documento = Init(servxDoc, numeroOperacion, coddoc, nrocor);
           
            string html = string.Empty;
            Documento = new DocumentoReporteModel(html);
            if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobReg)
            {
                MODXDOC.Pr_Load_Datos_Memo();
                if (segundo)
                {
                    
                    servxDoc.Pr_Principal_2(Documento, uow);//TODO 
                }
                if (tercero)
                {
                    servxDoc.Pr_Principal_3(Documento, uow);
                }
                if (cuarto)
                {
                    servxDoc.Pr_Principal_4(Documento, uow);
                }
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocArb)
            {
                // Arbitarje.
                MODXDOC.Pr_Load_Doc621();
                //for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_12(Documento, uow);
                //}
                // -----------------------------------------'
                // Realsystems Nov.2008 Estab.Comex.Grp5.4  '
                // -----------------------------------------'
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxPagDir)
            {
                // Pago Directo.
                MODXDOC.Pr_Load_Exp610();
                //for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_6(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobCan || MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCanRet)
            {
                // Cancelación.
                MODXDOC.Pr_Load_Exp611();
                //for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_7(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegRet)
            {
                // Registro Retorno.
                MODXDOC.Pr_Load_Exp612();
                //for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_8(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegPln)
            {
                // Registro Planilla. 613 Planillas Visible Export.
                MODXDOC.Pr_Load_Exp613();
                //for (int c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_9(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegCanRet)
            {
                // Registro Retorno.
                MODXDOC.Pr_Load_Exp614();
                //for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_14(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxAceLet)
            {
                // Aceptación Letras.
                MODXDOC.Pr_Load_Datos_Letra();

                for (int c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                {
                    //html = new System.IO.StreamReader(Path.Combine(directory, DOCUMENTO_ACEPTACION_LETRAS_602)).ReadToEnd();
                    
                    servxDoc.Pr_Principal_5(Documento, uow);
                }
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocCVD)
            {
                // Compra - Venta.
                MODXDOC.Pr_Load_Doc620();
                //for (int c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
                //{
                servxDoc.Pr_Principal_11(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocGAcre || MODXDOC.VOpe.TipoDoc == MODXDOC.DocGAdeb)
            {
                // Aviso Débito/Crédito.
                MODXDOC.Pr_Load_Doc999();
                //for (c = 1; c <= 1; c += 1)
                //{
                    // CPCM.-
                servxDoc.Pr_Principal_10(Documento, uow);
                //}
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocCVDI)
            {
                /*Venta y Divisas de Importaciones*/
                DOC_CVDI.Pr_Load_Doc402(MODXDOC.Memo);
                MODXDOC.SetupLetras(); //Seteo de Letras
                DOC_CVDI.Header06(Documento);
                DOC_CVDI.Pr_Principal_13(Documento, uow);
            }
            return this.Documento;
        }

        private sce_xdoc Init(ServxDoc servxDoc, string numeroOperacion, decimal coddoc, decimal nrocor)
        {
            
            numeroOperacion = numeroOperacion.Trim();
            sce_xdoc documento = new sce_xdoc();
            this.sce_xdoc = documento;
            documento.codcct = numeroOperacion.Substring(0, 3);
            documento.codpro = numeroOperacion.Substring(3, 2);
            documento.codesp = numeroOperacion.Substring(5, 2);
            documento.codofi = numeroOperacion.Substring(7, 3);
            documento.codope = numeroOperacion.Substring(10, 5);
            documento.coddoc = coddoc;
            documento.nrocor = nrocor;

            documento.NumOpe = numeroOperacion;
            documento.NumCop = 0;//Deberia ser un parametro  

            string Numero_Operacion = numeroOperacion + nrocor.ToString().Trim();//no tiene sentido el + nrocor, ya que no se usa luego
            documento.NumOpe_t = Numero_Operacion.Substring(0, 3) + "-" +
                                 Numero_Operacion.Substring(3, 2) + "-" +
                                 Numero_Operacion.Substring(5, 2) + "-" +
                                 Numero_Operacion.Substring(7, 3) + "-" +
                                 Numero_Operacion.Substring(10, 5);

            if (documento.NumCop == 0)
            {
                documento.NumCop = 1;
            }

            MODFRA.CENTRO_COSTO = Numero_Operacion.Substring(0, 3);
            Module1.rutiparty = servxDoc.busejc_party(uow, documento.codcct, documento.codpro, documento.codesp, documento.codofi, documento.codope);

            while (Module1.rutiparty.Length <= 9)
            {
                Module1.rutiparty = "0" + Module1.rutiparty;
            }

            var xdoc = uow.SceRepository.sce_xdoc_s01_MS(
                documento.codcct, documento.codpro, documento.codesp, documento.codofi, documento.codope, nrocor);

            documento.fecing = xdoc.fecing;
            documento.codmem = xdoc.codmem;

            if (documento.codmem == 0)
            {
                throw new XegiException("Codmem inválido");
            }
            else
            {
                documento.Memo = uow.SceRepository.sce_memg_s01_MS("x", documento.codmem);
            }
            documento.RefBae = uow.SceRepository.sce_refe_s01_MS(documento.codcct, documento.codpro, documento.codesp, documento.codofi, documento.codope);           

            //inicio de carga de arreglos
            MODXDOC.VOpe.NumOpe = documento.NumOpe; //MODGDOC.CopiarDeString(Comando, 9.Char(), 2).TrimB();
            MODXDOC.VOpe.TipoDoc = documento.coddoc.ToInt(); //MODGDOC.CopiarDeString(Comando, 9.Char(), 3).ToInt();
            MODXDOC.VOpe.NumCor = documento.nrocor.ToInt(); //MODGDOC.CopiarDeString(Comando, 9.Char(), 4).ToInt();
            MODXDOC.VOpe.NumCop = documento.NumCop; //MODGDOC.CopiarDeString(Comando, 9.Char(), 5).ToInt();
            
            if (MODXDOC.VOpe.NumCop == 0)
            {
                MODXDOC.VOpe.NumCop = 1;
            }

            Numero_Operacion = MODXDOC.VOpe.NumOpe + MODXDOC.VOpe.NumCor.Str().TrimB();
            MODXDOC.VOpe.NumOpe_t = Numero_Operacion.Mid(1, 3).TrimB() + "-" + Numero_Operacion.Mid(4, 2).TrimB() + "-" + Numero_Operacion.Mid(6, 2).TrimB() + "-" + Numero_Operacion.Mid(8, 3).TrimB() + "-" + Numero_Operacion.Mid(11, 5).TrimB();

            MODXDOC01.VDocx.NroMem = (int)documento.codmem; //MODXDOC01.SyGet_xDoc(MODXDOC.VOpe.NumOpe.TrimB(), MODXDOC.VOpe.NumCor);
            MODXDOC01.VDocx.FecEmi = documento.fecing.ToString();
            MODXDOC.Memo = documento.Memo;

            MODXDOC.tabulador = 71;
            MODXDOC.tab_doc_letra = 54;
            MODXDOC.tab_doc_cod = 59;
            MODXDOC.tab_doc_nem = 65;
            MODXDOC.tab_doc_monto = 89;
            MODXDOC.tab_mto_descr = 57;
            MODXDOC.tab_mto_monto = 65;
            MODXDOC.tab_titulos = 45;

            return documento;
        }

        public string sce_refe_s01_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return uow.SceRepository.sce_refe_s01_MS(codcct, codpro, codesp, codofi, codope);
        }

        public string sce_xdoc_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string nrocor, string cencos, string codusr, string coddoc, string fecing, string codmeme)
        {
            throw new NotImplementedException();
            //return uow.SceRepository.sce_xdoc_i01_MS(codcct, codpro, codesp, codofi, codope, nrocor, cencos, codusr, coddoc, fecing, codmeme);
        }

        public string sce_xdoc_s02_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            throw new NotImplementedException();
            //return uow.SceRepository.sce_xdoc_s02_MS(codcct, codpro, codesp, codofi, codope);
        }

        public List<object> sce_doc_s01_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            throw new NotImplementedException();
            //return uow.SceRepository.sce_doc_s01_MS(codcct, codpro, codesp, codofi, codope);
        }

        public string sce_xdoc_s01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string NroCor)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_xdoc_s01_MS(codcct, codpro, codesp, codofi, codope, NroCor);
        }

        public string sce_ccof_s01_MS(string ccosto)
        {
            throw new NotImplementedException();
            //return uow.SceRepository.sce_ccof_s01_MS(ccosto);
        }

        public string sce_usr_s25_MS(string rut_eje)
        {
            return uow.SceRepository.sce_usr_s25_MS(rut_eje);
        }

        public List<sce_ini_s01_MS_Result> sce_ini_s01_MS(string grupo, string elem)
        {            
            return uow.SceRepository.sce_ini_s01_MS(grupo, elem);
        }

        public List<object> sce_fra_s06_MS(string CodFra, string Idioma)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_fra_s06_MS(CodFra, Idioma);
        }

        public string sce_memg_s03_MS(string Tabla, string M)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_memg_s03_MS(Tabla, M);
        }

        public string ScePAMemg11(string Tabla)
        {
            return uow.SceRepository.ScePAMemg11(Tabla);
        }

        public string sce_memg_s01_MS(string Tabla, string CodMem)
        {
            return uow.SceRepository.sce_memg_s01_MS(Tabla, CodMem);
        }

        public string rce_memg_s01_MS(string Tabla, string CodMem)
        {
            return uow.SceRepository.rce_memg_s01_MS(Tabla, CodMem);
        }

        public string sce_memg_d01_MS(string Tabla, string CodMem)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_memg_d01_MS(Tabla, CodMem);
        }

        public string sce_memg_i01_MS(string Tabla, string CodMem, string i, string lineas)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_memg_i01_MS(Tabla, CodMem, i, lineas);
        }

        public string sce_vrng_s01_MS(string codcct, string codpro, string codesp)
        {
            return uow.SceRepository.sce_vrng_s01_MS(codcct, codpro, codesp);
        }

        public string sce_trng_s01_MS()
        {
            throw new NotImplementedException();// uow.SceRepository.sce_trng_s01_MS();
        }

        public string sce_rng_i01_MS(string cct, string esp, string doc, string rut, string inf, string sup, string act)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_rng_i01_MS(cct, esp, doc, rut, inf, sup, act);
        }

        public string sce_usr_u02_MS(string CenCos, string CodUsr, string CodFec, string fecha)
        {
           throw new NotImplementedException();// uow.SceRepository.sce_usr_u02_MS(CenCos, CodUsr, CodFec, fecha);
        }

        public string sce_usr_s03_MS(string CenCos)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_usr_s03_MS(CenCos);
        }

        public List<object> sce_serv_imp_01_MS(string ls_rutcli, string ls_codcct)
        {
            throw new NotImplementedException();// uow.SceRepository.sce_serv_imp_01_MS(ls_rutcli, ls_codcct);
        }

        public string sgt_suc_s04_MS(string oficina)
        {
            throw new NotImplementedException(); // uow.SceRepository.sgt_suc_s04_MS(oficina);
        }

        public string sce_usr_s02_MS(string cencos, string CodUsr)
        {
            throw new NotImplementedException(); // uow.SceRepository.sce_usr_s02_MS(cencos, CodUsr);
        }
    }
}
