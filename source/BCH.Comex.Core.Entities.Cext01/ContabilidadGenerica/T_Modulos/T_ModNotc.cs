
namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_ModNotc
    {
        // ****************************************************************************
        // Arreglo para las variables de de la Nota de Crédito
        public struct T_Prn_cre
        {
            public string CodCct;   //Centro de Costo  (Mch, xDoc, Swf).
            public string CodPro;   //Producto  (Mch, xDoc, Swf).
            public string CodEsp;   //Especialista  (Mch, xDoc, Swf).
            public string CodOfi;   //Empresa  (Mch, xDoc, Swf).
            public string CodOpe;   //Operación  (Mch, xDoc, Swf).
            public double Factura;   //MZ 2009
            public int NroRpt;   //Correlativo (id_usr + hora)  (Mch, xDoc, Swf).
            public string FecOpe;   //Mch, xDoc, Swf
            public double neto;
            public double iva;
            public double monto;   //Mch
            public int monedafac;
            public string tipofac;   //TipoFactura Afecta o excenta
            public int CodDoc;   //xDoc
            public int TipSwf;   //Swf
            public int NroSwf;   //Swf
            public int NroMem;   //Swf
            public int TipDoc;   //Identifica 1 = Carta, 2 = Swift, 3 = Contabilidad
        }
        public T_Prn_cre[] VPrn_cre = null;     // MZ
        // ****************************************************************************
        // Tabla de Descripción Tipos de Documentos.
        public struct T_TDoc_cre
        {
            public int CodTDoc;   //Descripción Tipos de Documentos
            public string DesTDoc;
        }
        public T_TDoc_cre[] VTDoc_cre = null;     // MZ




    }
}
