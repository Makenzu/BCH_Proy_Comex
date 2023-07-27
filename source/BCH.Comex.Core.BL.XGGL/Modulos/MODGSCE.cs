using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGSCE
    {
        // Lee los datos generales.-
        public static int GetSceGen(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("GetSceGen"))
            {
                T_MODGSCE MODGSCE = Globales.MODGSCE;
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                T_MODGMTA MODGMTA = Globales.MODGMTA;

                int GetSceGen = 0;

                int X = 0;
                int a = 0;

                int.TryParse(MODGPYF0.GetSceIni("General", "MndNac"), out MODGSCE.VGen.MndNac);
                int.TryParse(MODGPYF0.GetSceIni("General", "MndDol"), out MODGSCE.VGen.MndDol);
                int.TryParse(Globales.DatosUsuario.CodPBC, out MODGSCE.VGen.CodPbc);
                int.TryParse(Globales.DatosUsuario.CodBCH, out MODGSCE.VGen.CodBCH);
                int.TryParse(Globales.DatosUsuario.SucBCH, out MODGSCE.VGen.SucBCH);
                int.TryParse(Globales.DatosUsuario.CodBCCH, out MODGSCE.VGen.CodBCCh);

                a = BCH.Comex.Core.BL.XGGL.Modulos.MODGMTA.FindImp(Globales, unit, "IVA");
                MODGSCE.VGen.MtoIva = MODGMTA.VImp[a].tasmax;


                if (MODGSCE.VGen.MndNac == 0)
                {
                    tracer.AddToContext("Moneda Nacional", "No se ha podido establecer el Código de la Moneda Nacional.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Código de la Moneda Nacional."
                    });
                    return GetSceGen;
                }
                if (MODGSCE.VGen.MndDol == 0)
                {
                    tracer.AddToContext("Moneda Extranjera", "No se ha podido establecer el Código de la Moneda Extranjera.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Código de la Moneda Extranjera."
                    });
                    return GetSceGen;
                }
                if (MODGSCE.VGen.CodPbc == 0)
                {
                    tracer.AddToContext("Codigo Plaza", "No se ha podido establecer el Código de la Plaza del Banco Central.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Código de la Plaza del Banco Central."
                    });
                    return GetSceGen;
                }
                if (MODGSCE.VGen.CodBCH == 0)
                {
                    tracer.AddToContext("Codigo BCH", "No se ha podido establecer el Código de la Entidad Banco Chile.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Código de la Entidad Banco Chile."
                    });
                    return GetSceGen;
                }
                if (MODGSCE.VGen.SucBCH == 0)
                {
                    tracer.AddToContext("Sucursal BCH", "No se ha podido establecer el Código de la Sucursal Banco Chile.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Código de la Sucursal Banco Chile."
                    });
                    return GetSceGen;
                }
                if (MODGSCE.VGen.CodBCCh == 0)
                {
                    tracer.AddToContext("Codigo BCCH", "No se ha podido establecer el Código del Banco Central de Chile.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Código del Banco Central de Chile."
                    });
                    return GetSceGen;
                }
                if (MODGSCE.VGen.MtoIva == 0)
                {
                    tracer.AddToContext("IVA", "No se ha podido establecer el Monto asociado al IVA.");
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se ha podido establecer el Monto asociado al IVA."
                    });
                    return GetSceGen;
                }
                // ----------------------------------------------
                // Función que trae el impuesto sobre los cheque
                // ---------------------------------------------
                X = SybGet_gen(Globales, unit);

                GetSceGen = 1;

                return GetSceGen;
            }
        }

        // Procedimiento que trae el impuesto a cobrar sobre los cheques
        // --------------------------------------------------------------

        public static int SybGet_gen(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SybGet_gen"))
            {
                T_MODGSCE MODGSCE = Globales.MODGSCE;
                T_MODGMTA MODGMTA = Globales.MODGMTA;

                int SybGet_gen = 0;

                short paso = 0;
                double c = 0.0;
                int b = 0;
                string aa = "";
                
                try
                {

                    paso = 1;
                    var res = unit.SceRepository.EjecutarSP<sce_mta3_s03_MS_Result>("sce_mta3_s03_MS", "SCH").First();


                    aa = res.codimp;
                    aa = res.nomimp;
                    b = res.mtofij.ToInt();
                    c = (double)res.tasmin;
                    c = (double)res.tasmax;
                    MODGSCE.VGen.MtoDeb = (double)res.mtomin;
                    c = (double)res.mtomax;
                    aa = res.cta_mn;
                    aa = res.cta_me;

                    paso = 2;

                    var aux = unit.SceRepository.EjecutarSP<bool>("sce_impflag_s01_MS").First();

                    MODGMTA.impflag = aux.ToInt();

                    return SybGet_gen;

                }
                catch (Exception exc)
                {
                    if (paso == 1)
                    {
                        tracer.AddToContext("Excepcion", "Se ha producido un error al tratar de leer una Cuenta Contable (Sce_Cta).");
                        Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Text = "Se ha producido un error al tratar de leer una Cuenta Contable (Sce_Cta).",
                            Type = Common.UI_Modulos.TipoMensaje.Error
                        });
                    }
                    else if (paso == 2)
                    {
                        tracer.AddToContext("Excepcion", "Se ha producido un error al tratar de leer la tabla Impuestos.");
                        Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Text = "Se ha producido un error al tratar de leer la tabla Impuestos.",
                            Type = Common.UI_Modulos.TipoMensaje.Error
                        });
                    }

                    return SybGet_gen;
                }
            }
        }
    }
}
