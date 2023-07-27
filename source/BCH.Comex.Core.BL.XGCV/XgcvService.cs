using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGCV
{
    public class XgcvService
    {
        private UnitOfWorkCext01 uow;
        private sce_xdoc sce_xdoc;

        private static XgcvService instance;
        public static XgcvService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XgcvService();
                }
                return instance;
            }
        }

        public XgcvService()
        {
            uow = new UnitOfWorkCext01();
            sce_xdoc = new sce_xdoc();
        }

        public string Print(string comando)
        {
            var server = new ServxDoc();

            server.ProcesaComando(comando);

            return Printer.DefInstance.ToHTML();
        }


        public string Sce_refe_s01(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return uow.SceRepository.sce_refe_s01_MS(codcct, codpro, codesp, codofi, codope);
        }

        public string Sce_xDoc_i01(string codcct, string codpro, string codesp, string codofi, string codope, string nrocor, string cencos, string codusr, string coddoc, string fecing, string codmeme)
        {
            return "";
            //return uow.SceRepository.Sce_xDoc_i01(codcct, codpro, codesp, codofi, codope, nrocor, cencos, codusr, coddoc, fecing, codmeme);
        }

        public string Sce_xDoc_s02(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return "";
            //return uow.SceRepository.Sce_xDoc_s02(codcct, codpro, codesp, codofi, codope);
        }

        public List<object> Sce_doc_s01(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return null;
            //return uow.SceRepository.Sce_doc_s01(codcct, codpro, codesp, codofi, codope);
        }

        public List<sce_xdoc_s01_MS_Result> Sce_xDoc_s01(string codcct, string codpro, string codesp, string codofi, string codope, string NroCor)
        {
            return uow.SceRepository.sce_xdoc_s01_MS(codcct, codpro, codesp, codofi, codope, NroCor);
        }

        public string sce_ccof_s01(string ccosto)
        {
            return "";
            //return uow.SceRepository.sce_ccof_s01(ccosto);
        }

        public string sce_usr_s25(string rut_eje)
        {
            return uow.SceRepository.sce_usr_s25_MS(rut_eje);
        }

        public List<object> sce_ini_s01(string grupo, string elem)
        {
            return null;
            //return uow.SceRepository.sce_ini_s01(grupo, elem);
        }

        public List<object> Sce_Fra_S06(string CodFra, string Idioma)
        {
            return null;// uow.SceRepository.Sce_Fra_S06(CodFra, Idioma);
        }

        public string Sce_Memg_s03(string Tabla, string M)
        {
            return null;// uow.SceRepository.Sce_Memg_s03(Tabla, M);
        }

        public string ScePAMemg11(string Tabla)
        {
            return uow.SceRepository.ScePAMemg11(Tabla);
        }

        public string Sce_Memg_s01(string Tabla, string CodMem)
        {
            return uow.SceRepository.sce_memg_s01_MS(Tabla, CodMem);
        }

        public string rce_Memg_s01(string Tabla, string CodMem)
        {
            return uow.SceRepository.rce_memg_s01_MS(Tabla, CodMem);
        }

        public string Sce_Memg_d01(string Tabla, string CodMem)
        {
            return null;// uow.SceRepository.Sce_Memg_d01(Tabla, CodMem);
        }

        public string Sce_Memg_i01(string Tabla, string CodMem, string i, string lineas)
        {
            return null;// uow.SceRepository.Sce_Memg_i01(Tabla, CodMem, i, lineas);
        }

        public string sce_vrng_s01(string codcct, string codpro, string codesp)
        {
            return uow.SceRepository.sce_vrng_s01_MS(codcct, codpro, codesp);
        }

        public string Sce_Trng_S01()
        {
            return null;// uow.SceRepository.Sce_Trng_S01();
        }

        public string sce_rng_i01(string cct, string esp, string doc, string rut, string inf, string sup, string act)
        {
            return null;// uow.SceRepository.sce_rng_i01(cct, esp, doc, rut, inf, sup, act);
        }

        public string sce_usr_u02(string CenCos, string CodUsr, string CodFec, string fecha)
        {
            return null;// uow.SceRepository.sce_usr_u02(CenCos, CodUsr, CodFec, fecha);
        }

        public string sce_usr_s03(string CenCos)
        {
            return null;// uow.SceRepository.sce_usr_s03(CenCos);
        }

        public List<object> sce_serv_imp_01(string ls_rutcli, string ls_codcct)
        {
            return null;// uow.SceRepository.sce_serv_imp_01(ls_rutcli, ls_codcct);
        }

        public List<sgt_suc_s04_MS_Result> sgt_suc_s04(string oficina)
        {
            return uow.SceRepository.sgt_suc_s04_MS(oficina);
        }

        public List<sce_usr_s02_MS_Result> Sce_Usr_S02(string cencos, string CodUsr)
        {
            return uow.SceRepository.sce_usr_s02_MS(cencos, CodUsr);
        }

    }

}
