using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGCOR
    {

        /// <summary>
        /// 1.  Recorre los Corresponsales donde se pueda pagar dado un país y
        ///     moneda. Sólo se seleccionan los indicados en los parámetros como
        ///     Aladi/NoAladi.
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="codPais"></param>
        /// <param name="codMoneda"></param>
        /// <param name="aladi"></param>
        /// <returns></returns>
        public static IList<T_Cor> Filtra_Cor(DatosGlobales initObj, short codPais, short codMoneda, bool aladi)
        {
            IList<T_Nom> bancos = initObj.MODGTAB0.VNom.Where(b => b.Nom_Mda == codMoneda &&
                ((aladi && b.Nom_Ala != 0) || (!aladi && b.Nom_Ala == 0))).ToList();
            if (codPais > 0)
            {
                bancos = bancos.Where(b => b.Nom_Pai == codPais).ToList();
            }

            List<T_Cor> corresponsales = new List<T_Cor>();
            foreach (T_Nom banco in bancos)
            {
                corresponsales.AddRange(
                    initObj.MODGTAB0.VCor.Where(x => x.Cor_Swf == banco.Nom_Swf).ToList()
                    );
            }

            return corresponsales;
        }

        
        // ****************************************************************************
        //    1.  Recorre los Corresponsales donde se pueda pagar dado un país y
        //        moneda. Sólo se seleccionan los indicados en los parámetros como
        //        Activo/NoActivo y Aladi/NoAladi.
        //        P_Activo =>    0: Activo.
        //                       1: No Activo.              7
        //                       2: Activos y No Activos.
        // ****************************************************************************
        public static int Filtra_Cor(DatosGlobales Globales, int P_Pais, int P_Moneda, ref bool P_Todos, int P_Aladi, UI_ListBox P_Lista)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            int Filtra_Cor = 0;

            int k = 0;
            int j = 0;
            bool Selecciona = false;
            int i = 0;

            if (P_Todos)
            {
                P_Todos = true;
            }

           ((dynamic)P_Lista).Clear();
            for (i = 1; i <= MODGTAB0.VNom.GetUpperBound(0); i += 1)
            {
                if ((P_Pais == 0 || MODGTAB0.VNom[i].Nom_Pai == P_Pais) && MODGTAB0.VNom[i].Nom_Mda == P_Moneda)
                {
                    Selecciona = true;
                    if ((P_Aladi & (~MODGTAB0.VNom[i].Nom_Ala)) != 0)
                    {
                        Selecciona = false;
                    }
                    if (((~P_Aladi) & MODGTAB0.VNom[i].Nom_Ala) != 0)
                    {
                        Selecciona = false;
                    }
                    // If Not P_Todos And Not VNom(i%).Act Then
                    //     Selecciona% = False
                    // End If
                    if (Selecciona)
                    {
                        j = Find_Cor(Globales, MODGTAB0.VNom[i].Nom_Swf);
                        if (j != -1) 
                        {
                            P_Lista.Items.Add(new UI_ListBoxItem() { Data = i, Value = MODGTAB0.VNom[i].Nom_Swf + 9.Char() + MODGTAB0.VCor[j].Cor_Nom });
                            if (MODGTAB0.VNom[i].Nom_Swf == "BCHIUS33XXX")
                            {
                                k = i;
                            }
                        }
                    }
                }
            }
            if (P_Lista.Items.Count.ToInt() != 0 && P_Pais == 225)
            {
                Filtra_Cor = k;
            }
            else
            {
                Filtra_Cor = 0;
            }

            return Filtra_Cor;
        }

        // ****************************************************************************
        //    1.  Busca en Arreglo COR (direcciones Corresponsales) un Banco
        //        dado su Swift.
        // ****************************************************************************
        public static int Find_Cor(DatosGlobales Globales, string codigo)
        {
            return Globales.MODGTAB0.VCor.ToList().FindIndex(x => x.Cor_Swf == codigo);
        }

    }
}
