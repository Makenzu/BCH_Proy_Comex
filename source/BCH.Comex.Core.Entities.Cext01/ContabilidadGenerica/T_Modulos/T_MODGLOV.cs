using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{

    public class T_gOV
    {
        public string TipoOV = String.Empty;   //<O>rigen - <V>ia.- MAX 1
        public int Id_Cta;   //Input  : Cuenta Contable.-
        public string NomCta = String.Empty;   //Input  : Nombre Cta. Ctb.-
        public int codmnd;   //Input  : Código   Moneda.-
        public string NemMnd = String.Empty;   //Input  : Nemónico Moneda.-
        public string NomMnd = String.Empty;   //Input  : Nombre   Moneda.-
        public double MtoMnd;   //Input  : Monto.-
        public string IdPrty = String.Empty;   //OutPut : Party.-
        public string CtaCte = String.Empty;   //OutPut : Cta. Corriente.-
        public string CtaCte_t = String.Empty;   //OutPut : Cta. Corriente.-
        public int MonExt;   //OutPut : Es moneda Ext?.-
        public int CodOfi;   //OutPut : Oficina.-
        public int TipMov;   //OutPut : Tipo Movimiento.-
        public int NumPar;   //OutPut : Número Partida.-
        public string CodSwf = String.Empty;   //OutPut : Swift  Banco.-
        public int CodBco;   //OutPut : Código Banco.-
        public string numdoc = String.Empty;   //OutPut : #Documento.-
        public int Acepto;   //Acepto o Cancelo frm.-
        public string fecVen; // Fecha Vencimiento 

        public T_gOV Copy()
        {
            return (T_gOV)this.MemberwiseClone();
        }
    }
    public class T_MODGLOV
    {
        public T_gOV VgOV = new T_gOV();
        public T_gOV VgOVNul = new T_gOV();
        // ****************************************************************************
        // Constantes para Tipo de Movimientos en Saldos con Sucursales.-
        // ****************************************************************************
        public const int TP_INI = 1;     // Iniciativa.
        public const int TP_CON = 2;     // Contrapartida.
        public const int TP_COM = 3;     // Comunicado.
        public const int TP_COR = 4;     // Corrección.
    }
}
