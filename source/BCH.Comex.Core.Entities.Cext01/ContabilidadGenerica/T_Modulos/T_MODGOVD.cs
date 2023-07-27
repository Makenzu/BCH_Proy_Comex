using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_Suc
    {
        public string nomsuc;   //Nombre de la Sucurasal.
        public int codsuc;   //Código de la Sucursal.

        public T_Suc()
        {
            nomsuc = String.Empty;
        }
    }

    public class T_OriCC
    {
        public string ctacte;   //Número de Cta.Cte.
        public string CtaCte_t;   //Número de Cta.Cte(Descripción).
        public int MonExt;   //Indicador de moneda extranjera.
        public int codmnd;   //Código de la moneda.
        public int Activa;   //Esta Activa la Cuenta?(1/0).

        public T_OriCC()
        {
            ctacte = String.Empty;
            CtaCte_t = String.Empty;
        }
    }

    public class T_Ovd
    {
        public int numcta;   //Código de Origen Via Destino.
        public string NomCta;   //Descripción de Origen Vía Destino. MAX 50 caracteres
        public string NemCta;   //Nemónico de Origen Via Destino. MAX 15 caracteres
        public int monnac;   //Moneda de Origen Via Destino.
        public int CtaOri;   //Origen de Origen Via Destino.
        public int CtaVia;   //Vía de Origen Via Destino.
        public int CtaVue;   //Vuelto de Origen Via Destino.

        public T_Ovd()
        {
            NomCta = String.Empty;
            NemCta = String.Empty;
        }
    }



    public class T_MODGOVD
    {
        public T_Suc[] Vx_Suc = new T_Suc[0];
        public T_OriCC[] Vx_OriCC = new T_OriCC[0];
        public T_Ovd[] VOvd = new T_Ovd[0];
        public int Gvar_NotaCredito = 0;
        public string ope0 = "";
        public string ope1 = "";
        public string ope2 = "";
        public string ope3 = "";
        public string ope4 = "";
    }
}
