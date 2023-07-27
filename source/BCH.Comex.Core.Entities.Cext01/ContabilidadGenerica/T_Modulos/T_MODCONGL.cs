using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_CtasCtes
    {
        public string Cuenta = String.Empty;   //Cuenta Corriente.-
        public string llave = String.Empty;   //Llave del Party.-
        public int Moneda;   //Moneda.-
    }
    public class T_MODCONGL
    {
        public const int CodFun_GL = 999;     // Cte. Contabilidad Genérica.-
        public T_CtasCtes[] Cuentas = new T_CtasCtes[0];

    }
}
