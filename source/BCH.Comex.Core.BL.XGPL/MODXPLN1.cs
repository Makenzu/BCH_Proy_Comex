using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPL
{
    public static class MODXPLN1
    {
        // ******************************************************
        // Estado de las Planillas Visibles.-
        // ******************************************************
        public const int ExPlv_Emi = 1;
        public const int ExPlv_Anu = 9;
        public const int ExPlv_Rev = 10;
        // *******************************************************
        // Estados de una Planilla Visible de Export.-
        // *******************************************************
        public const int ExPlv_Generada = 1;
        public const int ExPlv_Impresa = 2;
        public const int ExPlv_Eliminada = 3;
        public const int ExPlv_Anulada = 9;
        public const int ExPlv_Reversada = 10;
        // *******************************************************
        // Estados de una Planilla Visible de Import.-
        // *******************************************************
        public const int ExPlvImp_Generada = 5;

        public static readonly IDictionary<int, string> EstadoPlanilla = new Dictionary<int, string> {
            {ExPlv_Generada, "GEN"},
            {ExPlv_Impresa, "IMP"},
            {ExPlv_Eliminada, "ELI"},
            {ExPlv_Anulada, "ANU"},
            {ExPlvImp_Generada, "GEN"},
            {ExPlv_Reversada, "REV"}
        };

        public static string GetEstadoPlanilla(int value)
        {
            string resultado;
            if (MODXPLN1.EstadoPlanilla.TryGetValue(value, out resultado))
            {   
                return resultado;
            }
            return "";
        }

        // variables que indican tipo de planilla a cobrar comisiones

        public static readonly decimal[] PLNLIQ = { 401, 403, 407, 500 };
        public static readonly decimal[] PLNINF = { 501, 502 };
        public static readonly decimal[] PLNTRN = { 511 };
        public const string PLN400 = "401;403;407;402";
        public const string P400ES = "402";
        public const string P500ES = "540;570";
        public const string P600ES = "601;603;607";
        public const string P095CO = "095";
        // planilas de que deben tener nuémro de declaración
        public const string PLNCDEC = "500;501;502;570;601;603;607";
        public const string PLNSDEC = "401;402;403;407;540";
        // Titulo de para definir planillas visibles
        public const string MsgxPlv = "Planillas Visibles de Exportación";


        static readonly IDictionary<int, string> NombreTipoPlanilla = new Dictionary<int, string>
        { 
            {401, "ANTIC. COM RET Y LIQUIDADO MCF"},
            {402, "ANTICIPO DE COMPRADOR RETORNADO"},
            {403, "CRÉDITO INTERNO"},
            {407, "CRÉDITO EXTERNO"},
            {500, "DIVISAS RETORNADAS Y LIQUIDADAS"},
            {511, "DIVISAS RETORNADAS Y NO LIQUIDADAS"},
            {501, "DIVISAS RETORNADAS"},
            {502, "DIVISAS RETORNADAS 502"},
            {540, "RET. EMPRESAS"},
            {570, "RETORNOS POR DEDUCCIÓN"}
        };

        public static string GetNombrePlanilla(int CodigoTipoPlanilla)
        {
            string Nombre = "";
            if (NombreTipoPlanilla.TryGetValue(CodigoTipoPlanilla, out Nombre))
            {
                return Nombre;
            }
            else if (CodigoTipoPlanilla >= 600)
            {
                Nombre = "EX-FINANCIAMIENTO";
            }
            return Nombre;
        }

        public static decimal? GetPlanillaPais(string numeroPlanilla, DateTime fechaPlanilla, XgplService service)
        {
            try
            {
                return service.Sce_Plia_S01(numeroPlanilla, fechaPlanilla).FirstOrDefault().codpai;
            }
            catch
            {
                return null;
            }
        }

        public static string ConvRut(string rut)
        {
            string Srut = "";
            string Rfin = "";
            short a = 0;
            for (a = 1; a <= (short)VB6Helpers.Len(rut); a++)
            {
                Srut = VB6Helpers.Mid(rut, a, 1);
                if (Srut == "0")
                {
                    Rfin = VB6Helpers.Right(rut, VB6Helpers.Len(rut) - a);
                }
                else
                {
                    Srut = "";
                    break;
                }
            }
            if (!string.IsNullOrWhiteSpace(Srut))
            {
                return Rfin;
            }
            else
            {
                return rut;
            }
        }
    }
    public class T_xPlv
    {
        // *****************************************************
        // Estructura de Planillas Visibles de Exportación.
        // *****************************************************

        public string NumPre;   //# Presentación.
        public string FecAnt;   //Fecha de la planilla (fecpre) antes de ser modificada.
        public DateTime fecpre;   //Fecha Presentación.
        public decimal TipPln;   //Tipo de Planilla.
        public string codcct;   //Centro de Costo Ope. asoc
        public string CodPro;   //Producto Operación   asoc
        public string codesp;   //Especialista Ope.    asoc
        public string codofi;   //Empresa Operación    asoc
        public string codope;   //Correlativo Ope.     asoc
        public string CodAnu;   //Código de Anulación.
        public decimal estado;   //Estado Planilla.
        public string NumDec;   //Número Declaración.
        public DateTime FecDec;   //Fecha Declaración.
        public decimal codadn;   //Código Aduana.
        public DateTime FecVen;   //Fecha Ven. Retorno.
        public string RutExp;   //Rut del Exportador.
        public string PrtExp;   //Llave del Exportador.
        public decimal IndNom;   //Indice Nombre.
        public decimal IndDir;   //Indice Direccion.
        public decimal CodMnd;   //Código Moneda Planilla.
        public decimal MtoBru;   //Monto Bruto.
        public double TotAnu;   //Total de la Anulación.
        public decimal MtoCom;   //Monto Comisiones.
        public decimal MtoOtg;   //Monto Otros Gastos.
        public decimal MtoLiq;   //Monto Líquido.
        public decimal Mtopar;   //Monto Paridad.
        public decimal MtoDol;   //Monto Dólares.
        public decimal TipCam;   //Tipo de Cambio.
        public double TipCamo;   //Tipo de Cambio Operación.
        public decimal PlzBcc;   //Plaza B. Central Contabil
        public decimal DfoCea;   //DFO, Código Entidad Autor
        public decimal DfoCtf;   //DFO, Código Tipo Financia
        public decimal DfoCbc;   //DFO, Código Plaza B. Cent
        public string DfoNpr;   //DFO, Número Presentación.
        public DateTime DfoFpr;   //DFO, Fecha  Presentación.
        public decimal AfiMnd;   //AFI, Código Moneda.
        public decimal AfiPar;   //AFI, Paridad.
        public decimal AfiMto;   //AFI, Monto.
        public decimal? AfiMtoD;   //AFI, Monto Dolar.
        public decimal AfiVen;   //AFI, Plazo Vencimiento (d
        public decimal DiePbc;   //DIE, Plaza B. Central.
        public string DieNum;   //DIE, Número Emisión.
        public DateTime DieFec;   //DIE, Fecha  Emisión.
        public string ObsPln;   //Observaciones.
        public string Fecing;   //Fecha de Ingreso.
        public string fecact;   //Fecha Actualización.
        public string Cencos;   //Centro Costo dueño.
        public string CodUsr;   //Especialista dueño.
        public int Status;   //Uso interno x planillas.
        public int Acepto;   //Indica si Acepto el Frm.
        public int IndPrt;   //Indice Party en PartysOpe.
        public int TipMto;   //Uso Interno, 1-2-3 : L,I,E.
        public double ValRet;   //Valor Retorno  Dec.
        public double ValCom;   //Valor Comision Dec.
        public double ValGas;   //Valor Gastos   Dec.
        public double ValLiq;   //Valor Liquido  Dec.
        public double ValFle;   //Valor Flete    Dec.
        public double ValSeg;   //Valor Seguro   Dec.
        public bool DedCom;
        public int DedFle;
        public int DedSeg;
        public bool PlnEst;
        public double NumCre;   //Número del Crédito.
        public double DatImp;
        public string FecIns;   //Fecha Inscripción.
        public string NomFin;
        public string Fecha_Vto;
        public decimal? CodPai;   //Pais de la Operación

    }

    /// <summary>
    /// Planilla Visible de Exportación Anulada
    /// </summary>
    public struct T_xAnu
    {
        public string NumPre;   //# Presentación.
        public string fecpre;   //Fecha Presentación.
        public string FecAnt;   //Fecha de la planilla (fecpre) antes de ser modificada.
        public string Cencos;   //Centro Costo dueño.
        public string CodUsr;   //Especialista dueño.
        public string Fecing;   //Fecha de Ingreso.
        public int estado;   //Estado Planilla.
        public string codcct;   //Centro de Costo Ope. asociada.
        public string CodPro;   //Producto Operación   asociada.
        public string codesp;   //Especialista Ope.    asociada.
        public string codofi;   //Empresa Operación    asociada.
        public string codope;   //Correlativo Ope.     asociada.
        public int TipAnu;   //Tipo de Anulación.
        public int PlzBcc;   //Plaza B. Central Contabiliza.
        public string RutExp;   //Rut del Exportador.
        public string PrtExp;   //Llave del Exportador.
        public int IndNom;   //Indice Nombre.
        public int IndDir;   //Indice Direccion.
        public int EntAut;   //Entidad que Autoriza.
        public string NumPreO;   //# Presentación Original.
        public string FecPreO;   //Fecha Presentación Original.
        public int TipPln;   //Tipo de Planilla.
        public int CodPbc;   //Código de Plaza Banco Central.
        public string NumDec;   //Número Declaración.
        public string FecDec;   //Fecha Declaración.
        public int codadn;   //Código Aduana.
        public string FecVen;   //Fecha Ven. Retorno.
        public int CodMnd;   //Código Moneda Planilla.
        public double MtoDol;   //Monto Dólares.
        public double Mtopar;   //Monto Paridad.
        public double MtoAnu;   //Monto Anulación.
        public double MtoParA;   //Monto Paridad de Anulación.
        public double MtoDolA;   //Monto Dólares de Anulación.
        public double MtoDolPo;   //Monto Dólares Paridad Original.
        public string ObsPln;   //Observaciones.
        public int Acepto;   //Aceptar
        public int PlnEst;   //indice si la planilla es estadistica
    }



    // Estructura original: EstrucUsuarios
    public class UsuarioEspecialista
    {
        public string rut { set; get; }  //rut_usuario
        public short Jerarquia { set; get; }  //nivel de jerarquia
        public string CentroCosto { set; get; }  //cent_costo_esp
        public string Especialista { set; get; }  //id_especialista
        public string CCtOrig { set; get; }  //Centro Costo Original.-
        public string EspOrig { set; get; }  //Especialista Original.-
        public short Delegada { set; get; }  //es del mismo nivel que super
        public string CostoSuper { set; get; }  //jefe superior
        public string Id_Super { set; get; }  //jefe superior
        public string Nombre { set; get; }  //nombre_esp
        public string Direccion { set; get; }  //direcc_esp
        public string comuna { set; get; }  //comuna especialista
        public string Ciudad { set; get; }  //ciudad_esp
        public string Seccion { set; get; }  //seccion_esp
        public short Oficina { set; get; }  //codigo de su oficina
        public string Telefono { set; get; }  //Telefono_esp
        public string Swift { set; get; }  //swift_esp
        public string Fax { set; get; }  //fax_esp
        public string Tipeje { set; get; }  //Tipo Ejecutivo Operativo Negocios XOtro
        public string reemplazos { set; get; }  //Reemplazos.-
        public string RempOrig { set; get; }  //Reemplazos Usuario Original.-
        public string OfixUser { set; get; }  //Oficinas que puede atender el usuario
    }

}
