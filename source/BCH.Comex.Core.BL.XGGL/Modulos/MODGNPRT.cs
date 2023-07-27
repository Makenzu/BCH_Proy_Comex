using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGNPRT
    {

        /// <summary>
        /// Busca el nombre del Party; 1º en memoria; 2º a disco.
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        /// <param name="PrtImp"></param>
        /// <param name="IndNom"></param>
        /// <param name="IndDir"></param>
        /// <param name="NomDir"></param>
        /// <returns></returns>
        public static string GetDatPrt(DatosGlobales initObject, UnitOfWorkCext01 unit, string PrtImp, short IndNom, short IndDir, string NomDir)
        {
            T_MODGNPRT MODGNPRT = initObject.MODGNPRT;

            string _retValue = string.Empty;
            string p = string.Empty;
            short i = 0;
            string s1 = string.Empty;
            string s2 = string.Empty;
            short n = 0;
            if (string.IsNullOrEmpty(PrtImp))
            {
                return _retValue;
            }
            p = PrtImp;
            p = PoneMarcaParty(PrtImp);
            i = VB6Helpers.CShort(FindPrt(MODGNPRT, p, IndNom, IndDir));
            if (i != -1)
            {
                s1 = MODGNPRT.DatPrtys[i].NomPrty;
                s2 = MODGNPRT.DatPrtys[i].DirPrty;
            }
            else
            {
                if (IndNom != -1)
                {
                    s1 = VB6Helpers.Trim(SyGet_Rsa(unit, p, IndNom));
                }
                if (IndDir != -1)
                {
                    s2 = VB6Helpers.Trim(SyGet_Dad(unit, p, IndDir));
                }
                n = (short)(VB6Helpers.UBound(MODGNPRT.DatPrtys) + 1);
                VB6Helpers.RedimPreserve(ref MODGNPRT.DatPrtys, 0, n);
                MODGNPRT.DatPrtys[n].PrtImp = p;
                MODGNPRT.DatPrtys[n].IndNom = IndNom;
                MODGNPRT.DatPrtys[n].IndDir = IndDir;
                MODGNPRT.DatPrtys[n].NomPrty = s1;
                MODGNPRT.DatPrtys[n].DirPrty = s2;
            }

            if (NomDir == "N")
            {
                _retValue = s1;
            }
            if (NomDir == "D")
            {
                return s2;
            }

            return _retValue;
        }

        /// <summary>
        /// Rellena el Party con la marca |.-
        /// </summary>
        /// <param name="Party"></param>
        /// <returns></returns>
        public static string PoneMarcaParty(string Party)
        {
            string p = Party;
            short n = 0;
            short i = 0;
            if (VB6Helpers.Instr(p, "|") == 0)
            {
                n = (short)(12 - VB6Helpers.Len(p));
            }

            if (n > 0)
            {
                for (i = 1; i <= (short)n; i++)
                {
                    p += "|";
                }

            }
            return p;
        }

        /// <summary>
        /// Retorna el indice en el arreglo de nombres de partys en memoria.
        /// </summary>
        /// <param name="MODGNPRT"></param>
        /// <param name="PrtImp"></param>
        /// <param name="IndNom"></param>
        /// <param name="IndDir"></param>
        /// <returns></returns>
        public static dynamic FindPrt(T_MODGNPRT MODGNPRT, string PrtImp, short IndNom, short IndDir)
        {
            dynamic _retValue = null;
            short n = 0;
            short i = 0;
            _retValue = -1;  //Inicializa como no encontrado.-
            try
            {
                n = (short)VB6Helpers.UBound(MODGNPRT.DatPrtys);
                if (n >= 0)
                {
                    for (i = 0; i <= (short)n; i++)
                    {
                        if (MODGNPRT.DatPrtys[i].PrtImp == PrtImp && MODGNPRT.DatPrtys[i].IndNom == IndNom && MODGNPRT.DatPrtys[i].IndDir == IndDir)
                        {
                            _retValue = i;
                            break;
                        }

                    }

                }
                else
                {
                    MODGNPRT.DatPrtys = new Type_DatPrty[0];
                }
            }
            catch (Exception e)
            {
                MODGNPRT.DatPrtys = new Type_DatPrty[0];
            }
            return _retValue;
        }

        /// <summary>
        /// Retorna la Razón Social del Party.-
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="IdParty"></param>
        /// <param name="IdNombre"></param>
        /// <returns></returns>
        public static string SyGet_Rsa(UnitOfWorkCext01 unit, string IdParty, short IdNombre)
        {
            string _retValue = "-1";
            try
            {
                _retValue = unit.SceRepository.EjecutarSP<string>("sce_rsa_s03", MODGSYB.dbcharSy(IdParty), IdNombre.ToString()).First();
            }
            catch (Exception _ex)
            {
                _retValue = "-1";
            }
            return _retValue;
        }

        /// <summary>
        /// Retorna la Dirección de un Party en particular.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="IdParty"></param>
        /// <param name="IdDireccion"></param>
        /// <returns></returns>
        public static string SyGet_Dad(UnitOfWorkCext01 unit, string IdParty, short IdDireccion)
        {
            string _retValue = String.Empty;
            try
            {
                _retValue = unit.SceRepository.EjecutarSP<string>("sce_dad_s03", MODGSYB.dbcharSy(IdParty), IdDireccion.ToString()).First();
            }
            catch (Exception _ex)
            {
                _retValue = String.Empty;
            }
            return _retValue;
        }

    }
}
