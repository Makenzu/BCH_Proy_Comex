using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //----------------------------------------------------------
    //ESTRUCTURAS
    //----------------------------------------------------------
    //Estructura de Códigos para Cobro
    //--------------------------------
    public class T_Cobro
    {
        public string codsis;  //Sistema.-
        public string codpro;  //Producto.-
        public string CodEta;  //Etapa.-
    }

    //Tabla de Cobro
    //--------------
    public class T_Cob
    {
        public string codsis;  //Sistema.-
        public string codpro;  //Producto.-
        public string CodEta;  //Etapa.-
        public short EsComi;  //Es Comisión?.-
        public short EsGast;  //Es Gasto?.-
        public short EsInte;  //Es Interés?.-
        public short EsImpu;  //Es Impuesto?.-
        public short EsOtro;  //Es Otro?.-
        public string DetImp;  //Detalle Impuesto.-
        public string DetOtr;  //Detalle Otro.-
    }

    //Tabla de Comisiones
    //-------------------
    public class T_Com 
    {
        public string codsis;  //Sistema.-
        public string codpro;  //Producto.-
        public string CodEta;  //Etapa.-
        public short MtoFij;  //Es Monto Fijo?.-
        public double TasMin;  //Tasa Mínima.-
        public double tasmax;  //Tasa Máxima.-
        public double MtoMin;  //Monto Mínimo.-
        public double MtoMax;  //Monto Máximo.-
        public DateTime FecIni;  //Fecha Inicial.-
        public short CodMon;  //Código Moneda.-
        public short hayiva;  //Se cobra IVA?.-
        public string cta_mn;  //Cuenta Moneda Nacional.-
        public string cta_me;  //Cuenta Moneda Extranjera.-
        public double RanMin;
        public double RanMax;
        public double CorMta;

        public T_Com(bool dummyArg)
        {
            codsis = String.Empty;
            codpro = String.Empty;
            CodEta = String.Empty;
            cta_mn = String.Empty;
            cta_me = String.Empty;
            MtoFij = 0;
            TasMin = 0;
            tasmax = 0;
            MtoMin = 0;
            MtoMax = 0;
            FecIni = DateTime.Now;
            CodMon = 0;
            hayiva = 0;
            RanMin = 0;
            RanMax = 0;
            CorMta = 0;
        }

    }

    //Tabla de Gastos
    //---------------
    public class T_Gas 
    {
        public string codsis;  //Sistema.-
        public string codpro;  //Producto.-
        public string CodEta;  //Etapa.-
        public double MtoMin;  //Monto Mínimo.-
        public double MtoMax;  //Monto Máximo.-
        public short CodMon;  //Código Moneda.-
        public short hayiva;  //Se cobra IVA?.-
        public string cta_mn;  //* 12  'Cuenta Moneda Nacional.-
        public string cta_me;  //* 12  'Cuenta Moneda Extranjera.-
    }

    //Tabla de Impuestos
    //------------------
    public class T_Imp
    {
        public string CodImp;  //Sistema.-
        public string NomImp;  //Producto.-
        public short MtoFij;  //Es Monto Fijo?.-
        public double TasMin;  //Tasa Mínima.-
        public double tasmax;  //Tasa Máxima.-
        public double MtoMin;  //Monto Mínimo.-
        public double MtoMax;  //Monto Máximo.-
        public string cta_mn;  //* 12  'Cuenta Moneda Nacional.-
        public string cta_me;  //* 12  'Cuenta Moneda Extranjera.-

        public T_Imp() {
            CodImp = String.Empty;
        }
    }

    //Estructura para el cobro
    //------------------------
    public class T_DatCob
    {
        public string LlaCli;  //Identif. del cliente.-
        public string codsis;  //Código Sistema.-
        public string codpro;  //Código Producto.-
        public string CodEta;  //Código Etapa.-
        public string FecRef;  //Fecha de referencia para buscar los valores a cobrar.-
        public short MonCob;  //Moneda en que se debe efectuar el cobro.-
        public short monnac;
        public short MonMto;
        public double MtoCom;  //Monto sobre el cual se deben calcular las comisiones.-
        public double MtoInt;  //Monto de intereses para Impuesto por Remesa de Intereses.-
        public short numchq;  //Número de cheques emitidos para Impuesto por Emisión de Cheques.-
    }

    //Estructura que contiene los conceptos y valores a cobrar
    //--------------------------------------------------------
    public class T_Con
    {
        public short NroCor;
        public short tipcon;  //Tipo de concepto cobrado
        public short MonCon;
        public double MtoCon;
        public double MtoCob;
        public short hayiva;
        public double ivacon;
        public string glscon;
        public string FecCon;
        public string NemCta;
        public string ctaiva;
        public short gdacon;
        public short Estado;
        //---------------------
        public short EstReg;
        public short corcon;  //Correlativo para itemdata de nuevas comisiones
    }

    //Estructura para mantener paridades y tipos de cambio ingresados manualmente
    //---------------------------------------------------------------------------
    public class T_Par
    {
        public short CodMon;
        public string FecVmd;
        public double VmdPar;
        public double VmdObs;
    }

    //Estructura para detalle Impuesto Venta de Divisas
    //-------------------------------------------------
    public class T_Vdi
    {
        public string numdec;
        public string FecDec;
        public short MonDec;
        public double MtoDec;
        public double MtoVdi;
        public double TipCam;

        public T_Vdi Copy()
        {
            return (T_Vdi)this.MemberwiseClone();
        }
    }

    //Estructura para detalle Impuesto al Pagaré
    //------------------------------------------
    public class T_Pgi
    {
        public string FecOtr;
        public short MonPag;
        public double MtoPag;
        public double TipCam;
        public string FecVto;
    }

    //Estructura para detalle Impuesto al Pagaré Fijo
    //-----------------------------------------------
    public class T_Pgf
    {
        public short MonPag;
        public double MtoPag;
        public double TipCam;
    }

    public class Hay_Comis
    {
        public short HayCom;  //Comisión
        public short HayGas;  //Gasto
        public short hayiva;  //Iva
        public short HaySch;  //Impto. sobre cheque
        public short HayVdi;  //Impto. Venta Divisa
        public short HayRei;  //Impto. Remesa
        public short HayPgi;  //Impto. al Pagaré
    }
    public class T_MODGMTA
    {
        //----------------------------------------------------------
        //CONSTANTES
        //----------------------------------------------------------
        //Identifica tipo Concepto en VCon
        //--------------------------------
        // UPGRADE_INFO (#0561): The 'EsCom' symbol was defined without an explicit "As" clause.
        public const short EsCom = 1;
        //Comisión
        // UPGRADE_INFO (#0561): The 'EsGas' symbol was defined without an explicit "As" clause.
        public const short EsGas = 2;
        //Gasto
        // UPGRADE_INFO (#0561): The 'EsIva' symbol was defined without an explicit "As" clause.
        public const short EsIva = 3;
        //Iva
        // UPGRADE_INFO (#0561): The 'EsSch' symbol was defined without an explicit "As" clause.
        public const short EsSch = 4;
        //Impto. sobre cheque
        // UPGRADE_INFO (#0561): The 'EsVdi' symbol was defined without an explicit "As" clause.
        public const short EsVdi = 5;
        //Impto. Venta Divisa
        // UPGRADE_INFO (#0561): The 'EsRei' symbol was defined without an explicit "As" clause.
        public const short EsRei = 6;
        //Impto. Remesa
        // UPGRADE_INFO (#0561): The 'EsPgi' symbol was defined without an explicit "As" clause.
        public const short EsPgi = 7;
        //Impto. al Pagaré
        // UPGRADE_INFO (#0561): The 'EsPgf' symbol was defined without an explicit "As" clause.
        public const short EsPgf = 8;
        //Impto. al Pagaré Fijo
        //----------------------------------------------------------
        //String para Mensajes
        //--------------------
        // UPGRADE_INFO (#0561): The 'MsgCom' symbol was defined without an explicit "As" clause.
        public const string MsgCom = "Cobro de Comisiones";

        //----------------------------------------------------------
        //DECLARACIÓN DE VARIABLES
        //----------------------------------------------------------
        public static  short impflag;  //Flag Impuesto

        
        public  T_Cob VgCob ;
        public  T_Com[] VCom;
        public  T_Com Vgcom ;
        public  T_Gas VgGas ;
        public  T_Imp[] VImp;
        public static Dictionary<string, short> VImpDict = new Dictionary<string, short>();
        public  T_DatCob VDatCob;
        public  T_Con[] VCon;
        public  T_Par[] VPar;
        public  T_Vdi[] VVdi;
        public  T_Pgi VPgi;
        public  T_Pgf VPgf;
        public  short RegNva;

        public T_MODGMTA() {
            VgCob = new T_Cob();
            VCom = new T_Com[0];
            Vgcom = new T_Com(false);
            VgGas = new T_Gas();
            VImp = new T_Imp[0];
            VDatCob = new T_DatCob();
            VCon = new T_Con[0];
            VPar = new T_Par[0];
            VVdi = new T_Vdi[0];
            VPgi = new T_Pgi();
            VPgf = new T_Pgf();
        }
    }
}
