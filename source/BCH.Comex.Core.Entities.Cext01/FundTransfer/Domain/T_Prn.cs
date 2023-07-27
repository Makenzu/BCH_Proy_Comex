
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain
{
    public class T_Prn
    {
        public string codcct{set;get;}  //Centro de Costo  (Mch, xDoc, Swf).
        public string codpro{set;get;}  //Producto  (Mch, xDoc, Swf).
        public string codesp{set;get;}  //Especialista  (Mch, xDoc, Swf).
        public string codofi{set;get;}  //Empresa  (Mch, xDoc, Swf).
        public string codope{set;get;}  //Operación  (Mch, xDoc, Swf).
        public int NroCor{set;get;}  //Correlativo (id_usr + hora)  (Mch, xDoc, Swf).
        public string FecOpe{set;get;}  //Mch, xDoc, Swf
        public short codfun{set;get;}  //Mch
        public short CodDoc{set;get;}  //xDoc
        public short TipSwf{set;get;}  //Swf
        public short NroSwf{set;get;}  //Swf
        public int NroMem{set;get;}  //Swf
        public short TipDoc{set;get;}  //Identifica 1 = Carta, 2 = Swift, 3 = Contabilidad
    }
}
