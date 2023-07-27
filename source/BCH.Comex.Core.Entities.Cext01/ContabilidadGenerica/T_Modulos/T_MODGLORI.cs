using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_xVia
    {
        public int numcta;   //Id. Cuenta Contable.
        public string NemCta = String.Empty;   //Nemónico Cuenta Contable.
        public string NomVia = String.Empty;   //Nombre de la Vía.
        public int CodMon;   //Moneda de la Comisión.
        public double MtoTot;   //Monto total para la moneda.
        public string NemMon = String.Empty;   //Nemónico Moneda. MAX: 3
        public int Status;   //1:Ing;2:Mod;3:Eli.
        public string IdPrty = String.Empty;   //Llave del Party.
        public string NomPrty = String.Empty;   //Nombre del Party.
        public int PosPrty;   //Posición en PartysOpe.
        public string ctacte = String.Empty;   //# Cuenta Corriente. MAX 15
        public string CtaCte_t = String.Empty;   //# Cuenta Corriente_t.
        public int MonExt;   //Indica si es Mon. Extranjera.
        public int CodOfi;   //Oficina en ScS's.
        public int TipMov;   //Tipo de Movimiento en ScS's.
        public int NumPar;   //Numero de Partida en ScS's.
        public string CodSwf = String.Empty;   //Swift  del Banco.
        public string numdoc = String.Empty;   //Número del Documento.
        public int IndSwf;   //Indice Swf().-
        public int IndChq;   //Indice Chq().-
        public int CodDme;   //Código de Destino Fondoc M/E.
        public int Vuelto;   //Indica si es Vuelto.-
        public int ImpChq;   //Indica si lleva Impuesto Sobre Cheque.-
        public int cctlin;   //Se realizó transacción Cta. Cte. en Línea
    }
    public class T_MODGLORI
    {
        public T_xMtoOri[] VxMtoOri = new T_xMtoOri[0];
        public T_xOri[] VxOri = new T_xOri[0];
        public T_gxOri VgxOri = new T_gxOri();
        public T_gxOri VgxOriNul = new T_gxOri();
        public T_xVia[] VxVia = null;
        public const string MsgxOri = "Orígenes de Fondos";     // Título de caja de Mensajes.
        public int Indice_Partys = 0;     // Almacena el indice de la lista de los Partys.
        public string Titulo = "";     // Registra el Título de la caja de mensajes y para un determinado frame.
        public const int ExVia_Eli = 3;     // Estado Eliminado.-
        public string RutwAis = "";
    }
}
