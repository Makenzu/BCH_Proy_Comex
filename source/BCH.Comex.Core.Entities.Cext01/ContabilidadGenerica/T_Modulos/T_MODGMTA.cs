using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_Cobro
    {
        public string CodSis;   //Sistema.-
        public string CodPro;   //Producto.-
        public string CodEta;   //Etapa.-

        public T_Cobro()
        {
            CodSis = String.Empty;
            CodPro = String.Empty;
            CodEta = String.Empty;
        }
    }
    public class T_Cob
    {
        public string CodSis;   //Sistema.- MAX 3
        public string CodPro;   //Producto.- MAX 3
        public string CodEta;   //Etapa.- MAX 3
        public int EsComi;   //Es Comisión?.-
        public int EsGast;   //Es Gasto?.-
        public int EsInte;   //Es Interés?.-
        public int EsImpu;   //Es Impuesto?.-
        public int EsOtro;   //Es Otro?.-
        public string DetImp;   //Detalle Impuesto.- MAX 40
        public string DetOtr;   //Detalle Otro.- MAX 40

        public T_Cob()
        {
            CodSis = String.Empty;   //Sistema.- MAX 3
            CodPro = String.Empty;   //Producto.- MAX 3
            CodEta = String.Empty;   //Etapa.- MAX 3
            DetImp = String.Empty;   //Detalle Impuesto.- MAX 40
            DetOtr = String.Empty;   //Detalle Otro.- MAX 40
        }
    }
    public class T_Com
    {
        public string CodSis;   //Sistema.- MAX 3
        public string CodPro;   //Producto.- MAX 3
        public string CodEta;   //Etapa.- MAX 3
        public int MtoFij;   //Es Monto Fijo?.-
        public double TasMin;   //Tasa Mínima.-
        public double tasmax;   //Tasa Máxima.-
        public double MtoMin;   //Monto Mínimo.-
        public double MtoMax;   //Monto Máximo.-
        public string FecIni;   //Fecha Inicial.-
        public int CodMon;   //Código Moneda.-
        public int hayiva;   //Se cobra IVA?.-
        public string cta_mn;   //Cuenta Moneda Nacional.- MAX 12
        public string cta_me;   //Cuenta Moneda Extranjera.- MAX 12
        public double RanMin;
        public double RanMax;
        public double CorMta;

        public T_Com()
        {
            CodSis = String.Empty;   //Sistema.- MAX 3
            CodPro = String.Empty;   //Producto.- MAX 3
            CodEta = String.Empty;   //Etapa.- MAX 3
            FecIni = String.Empty;   //Fecha Inicial.-
            cta_mn = String.Empty;   //Cuenta Moneda Nacional.- MAX 12
            cta_me = String.Empty;   //Cuenta Moneda Extranjera.- MAX 12
        }
    }
    public class T_Gas
    {
        public string CodSis;   //Sistema.- MAX 3
        public string CodPro;   //Producto.- MAX 3
        public string CodEta;   //Etapa.- MAX 3
        public double MtoMin;   //Monto Mínimo.-
        public double MtoMax;   //Monto Máximo.-
        public int CodMon;   //Código Moneda.-
        public int hayiva;   //Se cobra IVA?.-
        public string cta_mn;   //* 12  'Cuenta Moneda Nacional.-
        public string cta_me;   //* 12  'Cuenta Moneda Extranjera.-

        public T_Gas()
        {
            CodSis = String.Empty;   //Sistema.- MAX 3
            CodPro = String.Empty;   //Producto.- MAX 3
            CodEta = String.Empty;   //Etapa.- MAX 3
            cta_mn = String.Empty;   //* 12  'Cuenta Moneda Nacional.-
            cta_me = String.Empty;   //* 12  'Cuenta Moneda Extranjera.-
        }
    }
    public class T_Imp
    {
        public string CodImp;   //Sistema.- MAX 3
        public string NomImp;   //Producto.-
        public int MtoFij;   //Es Monto Fijo?.-
        public double TasMin;   //Tasa Mínima.-
        public double tasmax;   //Tasa Máxima.-
        public double MtoMin;   //Monto Mínimo.-
        public double MtoMax;   //Monto Máximo.-
        public string cta_mn;   //* 12  'Cuenta Moneda Nacional.-
        public string cta_me;   //* 12  'Cuenta Moneda Extranjera.-

        public T_Imp()
        {
            CodImp = String.Empty;   //Sistema.- MAX 3
            NomImp = String.Empty;   //Producto.-
            cta_mn = String.Empty;   //* 12  'Cuenta Moneda Nacional.-
            cta_me = String.Empty;   //* 12  'Cuenta Moneda Extranjera.-
        }
    }
    public class T_DatCob
    {
        public string LlaCli;   //Identif. del cliente.-
        public string CodSis;   //Código Sistema.-
        public string CodPro;   //Código Producto.-
        public string CodEta;   //Código Etapa.-
        public string FecRef;   //Fecha de referencia para buscar los valores a cobrar.-
        public int MonCob;   //Moneda en que se debe efectuar el cobro.-
        public int monnac;
        public int MonMto;
        public double MtoCom;   //Monto sobre el cual se deben calcular las comisiones.-
        public double MtoInt;   //Monto de intereses para Impuesto por Remesa de Intereses.-
        public int numchq;   //Número de cheques emitidos para Impuesto por Emisión de Cheques.-

        public T_DatCob()
        {
            LlaCli = String.Empty;   //Identif. del cliente.-
            CodSis = String.Empty;   //Código Sistema.-
            CodPro = String.Empty;   //Código Producto.-
            CodEta = String.Empty;   //Código Etapa.-
            FecRef = String.Empty;   //Fecha de referencia para buscar los valores a cobrar.-
        }
    }
    public class T_Con
    {
        public int NroCor;
        public int tipcon;   //Tipo de concepto cobrado
        public int MonCon;
        public double MtoCon;
        public double MtoCob;
        public int hayiva;
        public double ivacon;
        public string glscon;
        public string FecCon;
        public string NemCta;
        public string ctaiva;
        public int gdacon;
        public int estado;
        public int EstReg;
        public int corcon;   //Correlativo para itemdata de nuevas comisiones

        public T_Con()
        {
            glscon = String.Empty;
            FecCon = String.Empty;
            NemCta = String.Empty;
            ctaiva = String.Empty;
        }
    }
    public class T_Par
    {
        public int CodMon;
        public string FecVmd;
        public double VmdPar;
        public double VmdObs;

        public T_Par()
        {
            FecVmd = String.Empty;
        }
    }
    public class T_Vdi
    {
        public string NumDec;
        public string FecDec;
        public int MonDec;
        public double MtoDec;
        public double MtoVdi;
        public double TipCam;

        public T_Vdi()
        {
            NumDec = String.Empty;
            FecDec = String.Empty;
        }
    }
    public class T_Pgi
    {
        public string FecOtr;
        public int MonPag;
        public double MtoPag;
        public double TipCam;
        public string FecVto;
        public T_Pgi()
        {
            FecOtr = String.Empty;
            FecVto = String.Empty;
        }
    }
    public class T_Pgf
    {
        public int MonPag;
        public double MtoPag;
        public double TipCam;
    }
    public class Hay_Comis
    {
        public int HayCom;   //Comisión
        public int HayGas;   //Gasto
        public int hayiva;   //Iva
        public int HaySch;   //Impto. sobre cheque
        public int HayVdi;   //Impto. Venta Divisa
        public int HayRei;   //Impto. Remesa
        public int HayPgi;   //Impto. al Pagaré
    }
    public class T_MODGMTA
    {
        // ----------------------------------------------------------
        // CONSTANTES
        // ----------------------------------------------------------
        // Identifica tipo Concepto en VCon
        // --------------------------------
        public const int EsCom = 1;     // Comisión
        public const int EsGas = 2;     // Gasto
        public const int EsIva = 3;     // Iva
        public const int EsSch = 4;     // Impto. sobre cheque
        public const int EsVdi = 5;     // Impto. Venta Divisa
        public const int EsRei = 6;     // Impto. Remesa
        public const int EsPgi = 7;     // Impto. al Pagaré
        public const int EsPgf = 8;     // Impto. al Pagaré Fijo
                                        // ----------------------------------------------------------
                                        // DECLARACIÓN DE VARIABLES
                                        // ----------------------------------------------------------
        public int impflag = 0;     // Flag Impuesto JFO 07/02/2007
                                           // ----------------------------------------------------------
                                           // String para Mensajes
                                           // --------------------
        public const string MsgCom = "Cobro de Comisiones";

        public T_Cobro VCobro = new T_Cobro();
        public T_Cob[] VCob = new T_Cob[0];
        public T_Cob VgCob = new T_Cob();
        public T_Com[] VCom = new T_Com[0];
        public T_Com Vgcom = new T_Com();
        public T_Gas[] VGas = new T_Gas[0];
        public T_Gas VgGas = new T_Gas();
        public T_Imp[] VImp = new T_Imp[0];
        public T_DatCob VDatCob = new T_DatCob();
        public T_Con[] RCon = new T_Con[0];
        public T_Con[] VCon = new T_Con[0];
        public T_Par[] VPar = new T_Par[0];
        public T_Vdi[] VVdi = new T_Vdi[0];
        public T_Pgi VPgi = new T_Pgi();
        public T_Pgf VPgf = new T_Pgf();
        public Hay_Comis V_G_Comis = new Hay_Comis();
    }
}
