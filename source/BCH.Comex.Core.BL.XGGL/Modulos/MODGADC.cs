using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGADC
    {
        //****************************************************************************
        //   1.  Función que en base a ciertos parámetros permite conformar un string
        //       con los cuales se estructura la carta de caracter general Nro. 999.
        //   2.  Los Parámetros son los sgtes.:
        //           -   Número de la Operación,
        //           -   Indice del PartysOpe(),
        //           -   Débito o Crédito,
        //           -   Número de Cta. Cte. Concepto,
        //           -   Arreglo de Montos().
        //   3.  Retorna el correlativo con el cual se graba la carta.-
        //****************************************************************************
        public static int Doc_gAdc(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe, ref PartyKey Party, string TipoDC, string NroCtaCte, string Concepto, string NemMnd, double[] Montos, string Referencia, string usuario)
        {
            int Doc_gAdc = 0;

            int i = 0;
            string s = "";
            int n = 0;

            n = Montos.GetUpperBound(0);

            s = "";
            s = s + NumOpe.TrimB() + 9.Char();
            s = s + Party.NombreUsado + 9.Char();
            s = s + Party.DireccionUsado + 9.Char();
            s = s + Party.CiudadUsado + 9.Char();
            s = s + Party.EstadoUsado + 9.Char();
            s = s + Party.PaisUsado + 9.Char();
            s = s + Party.PostalUsado + 9.Char();
            s = s + Party.Fax + 9.Char();
            s = s + Party.CasBanco + 9.Char();
            s = s + Party.CasPostal + 9.Char();
            s = s + MigrationSupport.Utils.Format(Party.Enviara, String.Empty) + 9.Char();

            s = s + TipoDC.TrimB() + 9.Char();     // <D>ébito/<C>rédito.
            s = s + NroCtaCte.TrimB() + 9.Char();
            s = s + Concepto.TrimB() + 9.Char();

            s = s + n.Str().TrimB() + 9.Char();
            for (i = 1; i <= n; i += 1)
            {
                s = s + NemMnd + 9.Char();
                s = s + (Format.FormatCL(Montos[i])) + 9.Char();
            }
            s = s + Referencia + 9.Char();

            // Atributos del Usuario
            s = s + usuario.Left(3) + 9.Char();     // Centro de Costo.
            s = s + usuario.Right(2) + 9.Char();     // Especialista.

            // Retona el correlativo de la carta.
            if (TipoDC == "D")
            {
                Doc_gAdc = MODXDOC0.SyPut_xDoc(Globales,unit, NumOpe, T_MODGADC.DocGAdeb, s, usuario);
            }

            if (TipoDC == "C")
            {
                Doc_gAdc = MODXDOC0.SyPut_xDoc(Globales, unit, NumOpe, T_MODGADC.DocGAcre, s, usuario);
            }

            return Doc_gAdc;
        }
    }
}
