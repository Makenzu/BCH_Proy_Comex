using BCH.Comex.Core.Entities.Cext01.Common;
using System;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Tabla de Documentos Export.-
    public class TxDoc
    {
        public string FecEmi;
        public int NroMem;
    }
    //----------------------------------------------------------------------------
    //Arreglo de Paises.
    //----------------------------------------------------------------------------
    public class Det_Pai
    {
        public string PaiCod;  //Código País.End
        public string PaiNom;  //Nombre País.
        public short Painum;
    }

    //Estructura que contiene los MT generados
    public class T_MT_R
    {
        public short codmt;  //Código MT
        public string Titulo;  //Título MT
        public string ValAnt;  //Valor anterior de MT
        public string ValAct;  //Valor actual de MT
        public short ConMod;  //Indica si se modificó el MT
    }

    //Estructura donde se almacena el titulo del MT
    public class T_MT_H
    {
        public string Sender;
        public string Type;
        public string Reciver;
        public string DateIssue;
    }

    //Estructura donde se almacenan la descripcion del MT
    public class T_MT_D
    {
        public string campo;
        public string Titulo;
        public string Valor;
        public short Manual;
    }

    //Variables Generales.-
    public class T_MT_G
    {
        public string CamMan;
    }

    //****************************************************************************
    //Arreglo para las variables de de la Nota de Crédito
    public class T_Prn_cre
    {
        public string codcct;  //Centro de Costo  (Mch, xDoc, Swf).
        public string codpro;  //Producto  (Mch, xDoc, Swf).
        public string codesp;  //Especialista  (Mch, xDoc, Swf).
        public string codofi;  //Empresa  (Mch, xDoc, Swf).
        public string codope;  //Operación  (Mch, xDoc, Swf).
        public double Factura;  //MZ 2009
        public int NroRpt;  //Correlativo (id_usr + hora)  (Mch, xDoc, Swf).
        public string FecOpe;  //Mch, xDoc, Swf
        public double neto;
        public double iva;
        public double monto;  //Mch
        public int monedafac;
        public string tipofac;  //TipoFactura Afecta o excenta
        public short CodDoc;  //xDoc
        public short TipSwf;  //Swf
        public short NroSwf;  //Swf
        public int NroMem;  //Swf
        public short TipDoc;  //Identifica 1 = Carta, 2 = Swift, 3 = Contabilidad
    }

    //****************************************************************************
    //Tabla de Descripción Tipos de Documentos.
    public class T_TDoc_cre
    {
        public short CodTDoc;  //Descripción Tipos de Documentos
        public string DesTDoc;

        #region Initialization

        public T_TDoc_cre(bool dummyArg)
        {
            DesTDoc = String.Empty;
            CodTDoc = 0;
        }

        #endregion
    }
    public class T_Mdl_Funciones
    {
        //Tipos de Planilla Invisible.-
        // UPGRADE_INFO (#0561): The 'TPli_Ingreso' symbol was defined without an explicit "As" clause.
        public const short TPli_Ingreso = 1;
        // UPGRADE_INFO (#0561): The 'TPli_Egreso' symbol was defined without an explicit "As" clause.
        public const short TPli_Egreso = 2;
        // UPGRADE_INFO (#0561): The 'TPli_AnuIng' symbol was defined without an explicit "As" clause.
        public const short TPli_AnuIng = 3;
        // UPGRADE_INFO (#0561): The 'TPli_AnuEgr' symbol was defined without an explicit "As" clause.
        public const short TPli_AnuEgr = 4;
        // UPGRADE_INFO (#0561): The 'TPli_TranIng' symbol was defined without an explicit "As" clause.
        public const short TPli_TranIng = 8;
        // UPGRADE_INFO (#0561): The 'TPli_TranEg' symbol was defined without an explicit "As" clause.
        public const short TPli_TranEg = 9;
        // UPGRADE_INFO (#0561): The 'TPli_AnuTranIng' symbol was defined without an explicit "As" clause.
        public const short TPli_AnuTranIng = 10;
        // UPGRADE_INFO (#0561): The 'TPli_AnuTranEg' symbol was defined without an explicit "As" clause.
        public const short TPli_AnuTranEg = 11;

        //Constantes de Impresión y Salir.-
        // UPGRADE_INFO (#0561): The 'CmdDoc_Imp' symbol was defined without an explicit "As" clause.
        public const short CmdDoc_Imp = 1;
        // UPGRADE_INFO (#0561): The 'CmdDoc_Sal' symbol was defined without an explicit "As" clause.
        public const short CmdDoc_Sal = 9;

        public const string MsgMmt = "Modificación de MT";

        // UPGRADE_INFO (#0561): The 'MsgBic' symbol was defined without an explicit "As" clause.
        public const string MsgBic = "Bancos del Mundo";

        public string PathExes = "";
        
        ////Flag 50F
        ////------------------------------------------------------------------------------
        ////Modulo que contiene declaraciones, y 2 funciones  para llenado de combo de pais
        ////-------------------------------------------------------------------------------
        ////Variable global booleana para identificar
        ////si Checkbox 50F se encuentra activo
        //public bool CHK_50F;
        //public string[,] VG_50F;
        //public Det_Pai[] VG_Pais;
        //public T_MT_R[] VMT_R;

	    //Defino arreglo donde se gurda linia por line el swift
	    public string[] VMT_L;
	    public T_MT_H VMT_H;
	    public T_MT_D[] VMT_D;
        public T_MT_G VMT_G;

        public string recive = "";
        public T_Bic VBic;
        public T_Bic VBicNul;
       
        public T_Prn_cre[] VPrn_cre;

        public const string CompraDivisa = "C";
        public const string VentaDivisa = "V";
        public const string TransIngresoDivisa = "TI";
        public const string TransEgresoDivisa = "TE";
        public const string TransInternaDivisa = "TIN";
    }
}
