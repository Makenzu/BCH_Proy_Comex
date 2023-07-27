using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //----------------------------------------------------------------------------
    //Arreglo de Monedas.
    //----------------------------------------------------------------------------
    public class T_Mnd
    {
        public short Mnd_MndCod { get; set; }  //Código Banco Chile.
        public short Mnd_MndCbc { get; set; }  //Código Banco Central.
        public string Mnd_MndNom { get; set; }  //Nombre o Descripción.
        public string Mnd_MndNmc { get; set; }  //Nemónico.
        public string Mnd_MndSwf { get; set; }  //Código Swift.
        public short Mnd_MndSin { get; set; }  //Indica Sin Decimal.
    }

    //----------------------------------------------------------------------------
    //Arreglo de Paises.
    //----------------------------------------------------------------------------
    public class T_Pai
    {
        public short Pai_PaiCod { get; set; }  //Código País.
        public string Pai_PaiNom { get; set; }  //Nombre País.
        public short Pai_PaiAla { get; set; }  //País Aladi?.
        public string Pai_PaiIng { get; set; }  //Nombre País en Inglés
        public short Estado { get; set; }  //Usado solo en admini y en Visual
    }
    //----------------------------------------------------------------------------
    //Tabla de Traducción de Países : Banco de Chile N(03) v/s BIC C(02).
    //----------------------------------------------------------------------------
    public class T_Cou
    {
        public string Cou_Pai;  //Código País del BIC X(02).
        public string Cou_Nom;  //Nombre País del BIC.
        public short Cou_Cod;  //Código País SGT N(03).
    }
    //----------------------------------------------------------------------------
    //Arreglo Nómina de Corresponsales.
    //----------------------------------------------------------------------------
    public class T_Nom
    {
        public short Nom_Pai;  //Código Pais.
        public string Nom_Swf;  //Swift completo : BCHILRMXXX.
        public short Nom_Bco;  //Código de Banco.
        public short Nom_Mda;  //Código de Moneda que utiliza.
        public string Nom_cta;  //Cuenta en tal Banco.
        public short Nom_Act;  //Corresponsal Activo?.
        public short Nom_Ala;  //Banco Aladi?.
        public short Nom_Emi;  //Es una cuenta de Cheque Emitido?.
    }
    //----------------------------------------------------------------------------
    //Arreglo Datos de Bancos Corresponsales.
    //----------------------------------------------------------------------------
    public class T_Cor
    {
        public string Cor_Swf {get; set;}  //Swift  Corresponsal.
        public string Cor_Nom { get; set; }   //Nombre Corresponsal.
        public string Cor_Ciu { get; set; }  //Ciudad Corresponsal.
        public string Cor_Dir { get; set; }  //Direccion Corresponsal.
        public string Cor_Pos { get; set; }  //Código Postal Corresponsal.
        public string Cor_Pai { get; set; }  //País Corresponsal.
        public short Cor_CPa { get; set; }  //Código de País.
    }
    //----------------------------------------------------------------------------
    //Banco Corresponsal.
    //----------------------------------------------------------------------------
    public class Ty_Cor
    {
        public string CorSwf;  //Swift  Corresponsal.
        public string CorNom;  //Nombre Corresponsal.
        public string CorCiu;  //Ciudad Corresponsal.
        public string CorDes;  //?
        public string CorDir;  //Direccion Corresponsal.
        public string CorPos;  //Código Postal Corresponsal.
        public string CorPai;  //País Corresponsal.
        public short CorBco;  //Código Banco Chile.
        public short CorCPa;  //Código de País.
    }
    //----------------------------------------------------------------------------
    //Paridades.
    //----------------------------------------------------------------------------
    public class T_Vmd
    {
        public short VmdCod;  //Código Moneda.-
        public string VmdFec;  //Fecha.-
        public double VmdMbc;  //T.C. Mercado Bancario Comprador.
        public double VmdMbv;  //T.C. Mercado Bancario Vendedor.
        public double VmdMcc;  //T.C. Mercado Corredores Comprador.
        public double VmdMcv;  //T.C. Mercado Corredores Vendedor.
        public double VmdPrd;  //Paridad.
        public double VmdAcd;  //Dólar Acuerdo.
        public double VmdObs;  //T.C. Observado.
    }

    //----------------------------------------------------------------------------
    //Tabla de Bancos.
    //----------------------------------------------------------------------------
    public class T_Bco
    {
        public short CodBco;  //Código del Banco.
        public string NomBco;  //Nombre del Banco.
    }

    //----------------------------------------------------------------------------
    //Tipo de Régimen de Importación
    //----------------------------------------------------------------------------
    public class T_Reg
    {
        public short CodReg;  //Código del Régimen
        public string NomReg;
    }

    public class T_SecEc
    {
        public short CodSec;
        public string NomSec;
    }

    public class T_Acr
    {
        public short acr_bco;
        public short acr_mda;
        public string acr_swf;
    }
    
    public class T_MODGTAB0
    {
        public const string MsgTab0 = "Tablas Generales";
        
        public const short MndNac = 1;
        //Moneda Nacional.-
        public const short MndDol = 11;
        public const short PaisEEUU = 225;
        public const string PaisEEUUEn59F = "US";
        
        public  T_Mnd[] VMnd;
        //MOneda Dolar.-
        public  T_Pai[] VPai;
        public  T_Nom[] VNom;  //Arreglo de Nómina.
        public  T_Cor[] VCor;  //Arreglo de Dir. Corresp.
        //----------------------------------------------------------------------------
        //Arreglo de Días Feriados.
        //----------------------------------------------------------------------------
        public  string[] VFer;  //Arreglo de Días Feriados.
        public  T_Vmd VVmd;
        public  T_Vmd VVmdNul;
        public  T_Bco[] VBco;
        public  T_SecEc[] SecEc;
        public  T_Acr[] VAcr;

        public T_MODGTAB0() {
            VMnd = new T_Mnd[0];
            VPai = new T_Pai[0];
            VNom=  new T_Nom[0];//Arreglo de Nómina.
            VCor=  new T_Cor[0];//Arreglo de Dir. Corresp.
        //----------------------------------------------------------------------------
        //Arreglo de Días Feriados.
        //----------------------------------------------------------------------------
            VFer=  new string[0];//Arreglo de Días Feriados.
            VVmd= new T_Vmd();
            VVmdNul= new T_Vmd();
            VBco= new T_Bco[0];
            SecEc= new T_SecEc[0];
            VAcr = new T_Acr[0];
        }

        //Retorna el nemónico de la moneda especificada.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        //public static string Get_NemMnd(short CodMnd)
        //{
        //    short n = 0;
        //    short i = 0;
        //    short X = 0;
        //    //
        //    //n = (short)VB6Helpers.UBound(VMnd);
        //    // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
        //    // IGNORED: On Error GoTo 0
        //    //if (n == 0)
        //    //{
        //    //    X = SyGetn_Mnd();
        //    //}
        //    for (i = 1; i <= (short)VB6Helpers.UBound(VMnd); i++)
        //    {
        //        if (VMnd[i].Mnd_MndCod == CodMnd)
        //        {
        //            return VMnd[i].Mnd_MndNmc;
        //        }

        //    }

        //    return "";
        //}
    }
}
