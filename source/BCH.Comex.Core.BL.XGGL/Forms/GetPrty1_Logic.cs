using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using System;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class GetPrty1_Logic
    {
        public static void Aceptar_Click(DatosGlobales Globales)
        {
            UI_GetPrty1 GetPrty1 = Globales.GetPrty1;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            string NumOp = "";
            string Txt = "";
            object IDCANCEL = null;
            int aa = 0;

            // Marcar la Salida
            GetPrty1.Aceptar.Tag = MigrationSupport.Utils.Format(T_SYGETPRT.GPrt_RetExiste, String.Empty);

            if (SYGETPRT.PrtControl.EsHost != 0)
            {
                //todo: @emiliano -> Validar que este mensaje no se de nunca

                // es seleccion host
                //aa = ((int)MigrationSupport.Utils.MsgBox(GPrt_Seg1 + GPrt_Seg2, MODGPYF0.pito(49).Cast<MigrationSupport.MsgBoxStyle>(), SYGETPRT.GPrt_Caption));
                //if (aa == IDCANCEL.ToInt())
                //{
                //    Cancelar_Click(null, new EventArgs());
                //    return;
                //}

                // Deshabilitar todo lo visible
                GetPrty1.Aceptar.Enabled = false;
                GetPrty1.Cancelar.Enabled = false;
                GetPrty1.Nome.Enabled = false;
                GetPrty1.Dire.Enabled = false;

                //// activamos la aplicacion o la levantamos
                //aa = ActivarAplicacion("BajaPrty.exe /Server", "Sce_BajaParty");
                //if (aa != 0)
                //{
                //    Txt = GPrt_BusyServer;
                //    if (aa == GPrt_RetNoServer)
                //    {
                //        Txt = GPrt_NoServer;
                //    }
                //    MigrationSupport.Utils.MsgBox(Txt, MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), SYGETPRT.GPrt_Caption);
                //    Cancelar_Click(null, new EventArgs());
                //    return;
                //}

                // conectamos al server
                // llave de indexacion
                NumOp = SYGETPRT.PrtControl.NumOpe.Cent_costo + SYGETPRT.PrtControl.NumOpe.Id_Product;
                NumOp = NumOp + SYGETPRT.PrtControl.NumOpe.Id_Especia + SYGETPRT.PrtControl.NumOpe.Id_Empresa;
                NumOp = NumOp + SYGETPRT.PrtControl.NumOpe.Id_Operacion;

                //segun @estanislao no hace nada
                //ActivarServer(NumOp, GetPrty0.DefInstance.Llave.Tag.ToStr());
                return;
            }
        }
    }
}
