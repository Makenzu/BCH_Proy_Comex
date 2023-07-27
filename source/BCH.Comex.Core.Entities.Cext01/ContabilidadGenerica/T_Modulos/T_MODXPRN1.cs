using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    // ****************************************************************************
    // Arreglo para las variables de Impresión.
    public class T_Prn
    {
        public string CodCct;   //Centro de Costo  (Mch, xDoc, Swf).
        public string CodPro;   //Producto  (Mch, xDoc, Swf).
        public string CodEsp;   //Especialista  (Mch, xDoc, Swf).
        public string CodOfi;   //Empresa  (Mch, xDoc, Swf).
        public string CodOpe;   //Operación  (Mch, xDoc, Swf).
        public int NroCor;   //Correlativo (id_usr + hora)  (Mch, xDoc, Swf).
        public string FecOpe;   //Mch, xDoc, Swf
        public int codfun;   //Mch
        public int CodDoc;   //xDoc
        public int TipSwf;   //Swf
        public int NroSwf;   //Swf
        public int NroMem;   //Swf
        public int TipDoc;   //Identifica 1 = Carta, 2 = Swift, 3 = Contabilidad

        public T_Prn()
        {
            CodCct= String.Empty;   //Centro de Costo  (Mch, xDoc, Swf).
            CodPro= String.Empty;   //Producto  (Mch, xDoc, Swf).
            CodEsp= String.Empty;   //Especialista  (Mch, xDoc, Swf).
            CodOfi= String.Empty;   //Empresa  (Mch, xDoc, Swf).
            CodOpe= String.Empty;   //Operación  (Mch, xDoc, Swf).
            FecOpe= String.Empty;   //Mch, xDoc, Swf
        }
    }
    // ****************************************************************************
    // Tabla de Descripción Tipos de Documentos.
    public class T_TDoc
    {
        public int CodTDoc;   //Descripción Tipos de Documentos
        public string DesTDoc; //MAX 60

        public T_TDoc()
        {
            DesTDoc = String.Empty;
        }
    }
    public class T_MODXPRN1
    {
        public T_Prn[] VPrn = new T_Prn[0];
        public T_TDoc[] VTDoc = new T_TDoc[0];
        // ****************************************************************************
        public int Indice_Print = 0;     // Indice de lista de Printer.
        public string FechaProc = "";     // Almacena una fecha parámetro para las lecturas de Swf, XDoc, Mch.
        public string Despliege_Texto = "";     // Almacena temporalmente el Reporte Contable para ser desplegado por pantalla.
        public string IdProd_xPrn = "";     // Código del Producto.-
    }
}
