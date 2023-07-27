using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGL
    {

        public static DatosGlobales Main(UnitOfWorkCext01 unit, IDatosUsuario datosUsuario)
        {
            using(var tracer = new Tracer("Main"))
            {
                int X = 0;
                string monedas = "";
                string ch = "";
                string arb = "";
                string Algo = "";
                int i = 0;
                DatosGlobales Globales = new DatosGlobales();
                Globales.DatosUsuario = datosUsuario;
                T_GLOBAL GLOBAL = Globales.GLOBAL;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                T_CONTAB01 CONTAB01 = Globales.CONTAB01;
                T_MODXPRN1 MODXPRN1 = Globales.MODXPRN1;
                T_Modswen Modswen = Globales.Modswen;

                string Operaciones = "";

                
                if (BCH.Comex.Core.BL.XGGL.Modulos.MODGUSR.VerRegistroUsuario(Globales, unit, true.ToInt()) != 0)
                {
                    tracer.AddToContext("VerRegistroUsuario", "Ha ocurrido un error");
                    return Globales;
                }


                if (MODGSCE.GetSceGen(Globales, unit) == 0)
                {
                    tracer.AddToContext("GetSceGen", "Error al Leer los Datos Generales");
                    return Globales;
                }

                // Comentado Akzio
                // Inicio
                // descomentado 20140820
                if (ModSaldo.SyGet_NodoSal(Globales) == 0)
                {
                    return Globales;
                }
                // Fin
                // 
                // Se controla la ejecución de la aplicación según Cierre Diario de Comercio Exterior.-
                // If SeHizoCierre() Then End

                // ----------------------------------------------------------------------------
                // Se obtienen los Path's del Sce.Ini.-
                // ----------------------------------------------------------------------------
                GLOBAL.PathExes = MODGPYF0.GetUbicacion("PathExes");     // Ejecutables.-
                GLOBAL.PathTablas = MODGPYF0.GetUbicacion("PathTablas");     // Tablas.-
                GLOBAL.PathTablasSgt = MODGPYF0.GetUbicacion("PathTablasSGT");     // Tablas SGT
                GLOBAL.PathDoc = MODGPYF0.GetUbicacion("PathDoc");     // Documentos.-
                GLOBAL.PathContab = MODGPYF0.GetUbicacion("PathContab");     // Contabilidad.-
                GLOBAL.PathPartys = MODGPYF0.GetUbicacion("PathPartys");     // Participantes
                GLOBAL.PathUsuarios = MODGPYF0.GetUbicacion("PathUsuarios");     // Sistema
                GLOBAL.PathSwf = MODGPYF0.GetUbicacion("PathSwf");     // Swift.-
                GLOBAL.ModSerPath = MODGPYF0.GetUbicacion("ModSerPath");

                // ----------------------------------------------------------------------------
                Operaciones = MODGPYF0.GetSceEntry("GL.Operaciones");
                GLOBAL.LasOperLimite = -1;
                for (i = 0; i <= MODGPYF0.cuentadestring(Operaciones, ";") - 1; i += 1)
                {
                    Array.Resize(ref GLOBAL.LasOper, i + 1);
                    GLOBAL.LasOperLimite = i;
                    GLOBAL.LasOper[i] = new Operaciones();
                    GLOBAL.LasOper[i].Texto = MODGPYF0.copiardestring(Operaciones, ";", (short)(i + 1));
                    Algo = MODGPYF0.GetSceIni("GL.Operaciones", GLOBAL.LasOper[i].Texto);
                    GLOBAL.LasOper[i].Path = MODGPYF0.copiardestring(Algo, ";", 1);
                    GLOBAL.LasOper[i].Tabla = MODGPYF0.copiardestring(Algo, ";", 2);
                    GLOBAL.LasOper[i].Campo = MODGPYF0.copiardestring(Algo, ";", 3);
                    GLOBAL.LasOper[i].PrtTabla = MODGPYF0.copiardestring(Algo, ";", 4);
                    GLOBAL.LasOper[i].PrtCampo = MODGPYF0.copiardestring(Algo, ";", 5);
                    GLOBAL.LasOper[i].RefImp = MODGPYF0.copiardestring(Algo, ";", 6);
                }

                // ----------------------------------------------------------------------------
                // cargamos la estructura de numero de operacion con los datos leidos
                SYGETPRT.Codop.Cent_costo = MODGUSR.UsrEsp.CentroCosto;
                SYGETPRT.Codop.Id_Product = T_MODGUSR.IdPro_ConGen;
                SYGETPRT.Codop.Id_Especia = MODGUSR.UsrEsp.Especialista;
                // Obtención de la Moneda Exclusiva para las operaciones en convenio aladi
                GLOBAL.moneda_aladi = MODGPYF0.GetSceIni("Monedas", "MonedaAladi");

                arb = MODGPYF0.GetSceIni("OVD.GL", "Arbitraje");

                CONTAB01.NEMO_PAR = MODGPYF0.copiardestring(arb, ";", 1);
                CONTAB01.PUENTEARB = MODGPYF0.copiardestring(arb, ";", 2);

                ch = MODGPYF0.GetSceIni("OVD.GL", "Cheque");
                CONTAB01.REEM_CHEQUE = MODGPYF0.copiardestring(ch, ";", 1);

                GLOBAL.CodBCCh = MODGPYF0.GetSceIni("Planillas", "CodBCCH").ToInt();

                // Obtención de la moneda nacional
                GLOBAL.moneda_nac = MODGPYF0.GetSceIni("Monedas", "MonedaNacional");
                monedas = MODGPYF0.GetSceIni("Monedas", "MonedaSinDecimal");
                GLOBAL.cod_monac = MODGPYF0.GetSceIni("General", "MndNac").ToInt();

                MODXPRN1.IdProd_xPrn = T_MODGUSR.IdPro_ConGen;

                // Obtención de las monedas cuyos Montos no pueden tener decimales
                // 
                // Cargando tablas para el Sistema
                X = MODGTAB0.SyGetn_Mnd(Globales, unit);

                lee_ovd(Globales, unit);
                // leevia = SyGetn_Ovd()
                // 
                // Se carga arreglo de Beneficiarios.-
                carga_benef(Globales);

                // 
                // _____inicia la Contabilidad Genérica
                // 
                // 
                // Poly
                Modswen.hab_swift = BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Habil_SWIFT(Globales, unit, T_MODGUSR.IdPro_ConGen);
                return Globales;
            }
        }

        public static void carga_benef(DatosGlobales Globales)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            int a = 0;

            GLOBAL.Beneficiario = new string[2];

            GLOBAL.Beneficiario[0] = "Cliente";
            GLOBAL.Beneficiario[1] = "Tercero";
            a = SYGETPRT.ResetParty(Globales,GLOBAL.Beneficiario);

        }

        // cambia el status del boton
        public static void PicEnabled(UI_Button Boton, int Valor)
        {
            Boton.Enabled = Valor == 0 ? false : true;
            //int a = 0;

            //a = Boton.Tag.ToInt();
            //if (Valor != ((object)((dynamic)Boton.Parent).PicRes(a).Tag()).ToVal())
            //{
            //    return;
            //}

            //Boton.Enabled = Valor != 0;
            //((dynamic)Boton.Parent).PicTmp.Picture = ((UserControl)(Boton)).BackgroundImage;     // respaldo actual
            //((UserControl)(Boton)).BackgroundImage = MigrationSupport.Utils.ToPicture(((object)((dynamic)Boton.Parent).PicRes(a).Picture()));
            //((dynamic)Boton.Parent).PicRes(a).Picture = ((object)((dynamic)Boton.Parent).PicTmp.Picture());
            //((dynamic)Boton.Parent).PicRes(a).Tag = MigrationSupport.Utils.Format((~Valor), String.Empty);

        }

        public static void lee_ovd(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            int i = 0;
            int X = 0;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_MODGOVD MODGOVD = Globales.MODGOVD;

            X = BCH.Comex.Core.BL.XGGL.Modulos.MODGOVD.SyGetn_Ovd(Globales,unit);
            GLOBAL.ovd = new estr_ovd[MODGOVD.VOvd.GetUpperBound(0) + 1 ];
            for (i = 0; i <= MODGOVD.VOvd.GetUpperBound(0); i += 1)
            {
                GLOBAL.ovd[i] = new estr_ovd();
                GLOBAL.ovd[i].id_cuenta = MODGOVD.VOvd[i].numcta;
                GLOBAL.ovd[i].Glosa = MODGOVD.VOvd[i].NomCta;
                GLOBAL.ovd[i].Nemonico = MODGOVD.VOvd[i].NemCta;
                GLOBAL.ovd[i].nacional = MODGOVD.VOvd[i].monnac;
                GLOBAL.ovd[i].origen = MODGOVD.VOvd[i].CtaOri;
                GLOBAL.ovd[i].via = MODGOVD.VOvd[i].CtaVia;
                GLOBAL.ovd[i].Vuelto = MODGOVD.VOvd[i].CtaVue;
            }

        }

        public static void Inicializa_formatos(UI_Control Obj, string Moneda)
        {
            Obj.Tag = T_GLOBAL.con_decimales;
        }

        public static string moneda_reves(DatosGlobales Globales, string Parametro)
        {
            string moneda_reves = "";

            int i = 0;

            for (i = 1; i <= Globales.MODGTAB0.VMnd.GetUpperBound(0); i += 1)
            {
                if (Globales.MODGTAB0.VMnd[i].Mnd_MndNom.Trim() == Parametro.UCase())
                {
                    moneda_reves = Globales.MODGTAB0.VMnd[i].Mnd_MndCod.Str();
                    break;
                }
            }

            return moneda_reves;
        }
    }
}
