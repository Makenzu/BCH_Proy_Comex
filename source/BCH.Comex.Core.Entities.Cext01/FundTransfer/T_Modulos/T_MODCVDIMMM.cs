
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //******************************

    //estructura con montos rebajados para las planillas
    public class TIPO_DETREB
    {
        public string NDec;
        public string FDec;
        public string NIdi;
        public string FIdi;
        public string Nemb;
        public string FEmb;
        public double MtoFob;
        public double mtoflt;
        public double MtoSeg;
        public double merfob;
        public double merflt;
        public double merseg;
        public double MtoCif;
        public short IndRDec;
        public short IndDec;
        public short PagCh;
        public string NomMon;
    }

    public class r_dec
    {
        public string RDec_numero;
        public string RDec_fecha;
        public string RDec_principal;
        public double RDec_relfob;
        public double RDec_relflete;
        public double RDec_relseguro;
        public double RDec_relmerma;
        public double RDec_relcif;
        public double RDec_cubfob;
        public double RDec_cubflete;
        public double RDec_cubseguro;
        public double RDec_cubcif;
        public double RDec_cubmerma;
        public double RDec_fobmer;
        public double RDec_flemer;
        public double RDec_segmer;
        public short Rdec_codmoneda;
        public string Rdec_moneda;
        public string Rdec_NemMoneda;
        public double RDec_paridad;
        public string RDec_numidi;
        public string RDec_fechaidi;
        public string Rdec_estado;
        public short rdec_status;
    }

    public class estr_comisiones
    {
        public string monto;
        public string moneda_monto;
        public string Intereses;
        public string gastos_ced;
        public string mon_gas;
        public string sobre_pago;
        public string mon_sobre;
        public string tipo_camb_vta;
        public string Fecha_Declaracion;
        public string monto_declaracion;
    }

    // UPGRADE_INFO (#0501): The 'TipoReempl' member isn't used anywhere in current application.
    public class TipoReempl
    {
        public string Cod_Comercio;
        public string Concepto;
        public string NConcepto;
        public string monto;
        public string MtoPesos;
        public short IndCpto;
    }

    public class ParaAnuCob
    {
        public string NumPla;
        public string NumIdi;
        public string FecIdi;
        public string numdec;
        public string FecDec;
        public short NumPri;
        public short PagChi;
        public short Moneda;
        public double MtoFob;
        public double MtoFle;
        public double MtoSeg;
        public double FobMer;
        public double FleMer;
        public double SegMer;
        public double MtoCif;
        public double CifUsd;
        public double ParDec;
        public double ParIdi;
        public double TCVDia;
    }

    public class VI_Com
    {
        public string Party;
        public string codsis;
        public string codpro;
        public string CodEta;
        public short CodMon;
        public double MtoCob;
        public double TipCmO;
        public double TipCmC;
        public string TipCnp;
    }

    public class T_CvdI
    {
        public short Acepto;
        public short Grabar;
        public short NoVtas;
    }

    public class T_MODCVDIMMM
    {
        // UPGRADE_INFO (#0561): The 'TitMsg' symbol was defined without an explicit "As" clause.
        public const string TitMsg = "Selección de Oficinas";

        public  string FechaHoy ;
        public  short UltServ;
        public  short ServAct;
        public  short YaEntroUnaVez;
        public  short ISCVD;
        public  r_dec[] RDecIni;
        public  r_dec[] RDecFin;
        public  ParaAnuCob[] AnuCob;
        public  short EsReverso;
        public  short[] Monedas;
        public  short Ind_Planilla;
        public  short Contador;
        public  VI_Com VComI;
        public  T_CvdI VgCVDI;

        public T_MODCVDIMMM()
        {
            RDecIni = new r_dec[0];
        }
    }
}
