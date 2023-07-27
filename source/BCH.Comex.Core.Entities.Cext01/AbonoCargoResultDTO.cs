using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class AbonoCargoResultDTO
    {
        private const string CUENTA_CITI = "CITI";
        private const string CUENTA_BCH = "BCH";

        public enum TipoCuenta: byte
        {
            SinDefinir,
            Citi,
            Bch
        }

        public string numcct { get; set; }
        public string nemcta { get; set; }
        public DateTime fecmov { get; set; }
        public decimal nroimp { get; set; }
        public string nomcli { get; set; }
        public string cod_dh { get; set; }
        public string moneda { get; set; }
        public decimal codmon { get; set; }
        public decimal mtomcd { get; set; }
        public string operacion { get; set; }
        public decimal nrorpt { get; set; }
        public decimal enlinea { get; set; }
        public decimal estado { get; set; }
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string codofi { get; set; }
        public string codope { get; set; }
        public decimal codfun { get; set; }
        public decimal Indice { get; set; }
        public string trx_id { get; set; }
        public string cod_prod { get; set; }
        public string cod_trx_fc { get; set; }
        public string cod_ext { get; set; }
        public string cod_swf { get; set; }
        public string data1 { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
        public string data5 { get; set; }
        public string cod_trx_cosmos { get; set; }
        public string tip_cta { get; set; }

        public Guid IdAux { get; set; } //campo auxiliar para identificar inequívocamente un elemento en una lista (para no utilizar otras claves del negocio "compuestas")
        public short PosEnLista { get; set; }

        public bool TrxIdRepetido { get; set; }

        public string NroOperacionSinRaya
        {
            get
            {
                return this.codcct + this.codpro + this.codesp + this.codofi + this.codope;
            }
        }

        public TipoCuenta TipoCuentaAsEnum
        {
            get
            {
                if (this.tip_cta == CUENTA_CITI)
                {
                    return TipoCuenta.Citi;
                }
                else if (this.tip_cta == CUENTA_BCH)
                {
                    return TipoCuenta.Bch;
                }
                else
                {
                    return TipoCuenta.SinDefinir;
                }
            }
        }
    }
}
