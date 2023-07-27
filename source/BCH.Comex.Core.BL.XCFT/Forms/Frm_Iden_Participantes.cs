using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using CodeArchitects.VB6Library;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Iden_Paticipantes
    {
        private const string GPrt_Seg1 = "El participante seleccionado se encuentra en la base de participantes del Host. ";
        private const string GPrt_Seg2 = "Para poder utilizarlo será copiado a la base local.";
        private const string GPrt_NoServer = "El Servidor de actualización de participantes no se encuentra disponible en su estación.";
        private const string GPrt_BusyServer = "El Servidor de actualizacion de participantes se encuentra ocupado por otra aplicación.";
        private const short GPrt_RetNoServer = 1;
        private const short GPrt_RetBusyServer = 2;

        public static void Aceptar_Click(InitializationObject initObj)
        {
            short aa = 0;
            string NumOp = "";
            short IDCANCEL = 0;
            string Txt = "";
            //Marcar la Salida
            initObj.Frm_Iden_Participantes.Aceptar.Tag = VB6Helpers.Format(VB6Helpers.CStr(T_Module1.GPrt_RetExiste));

            if (initObj.Module1.PrtControl.EsHost != 0)
            {
                aa = 1;
                //es seleccion host
                //aa = (short)VB6Helpers.MsgBox(GPrt_Seg1 + GPrt_Seg2, (MsgBoxStyle)49, Module1.GPrt_Caption);

                //if (aa == IDCANCEL)
                //{
                //    Cancelar_Click(initObj);
                //    return;
                //}

                //Deshabilitar todo lo visible
                initObj.Frm_Iden_Participantes.Aceptar.Enabled = false;
                initObj.Frm_Iden_Participantes.Cancelar.Enabled = false;
                initObj.Frm_Iden_Participantes.Nome.Enabled = false;
                initObj.Frm_Iden_Participantes.Dire.Enabled = false;

                //@estanislao: No hace nada. ActivarAplicacion siempre devuelve 0
                //activamos la aplicacion o la levantamos
                //aa = ActivarAplicacion("BajaPrty.exe /Server", "Sce_BajaParty");
                //if (aa != 0)
                //{
                //    Txt = GPrt_BusyServer;
                //    if (aa == GPrt_RetNoServer)
                //    {
                //        Txt = GPrt_NoServer;
                //    }

                //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                //    {
                //        Text = Txt,
                //        Type = TipoMensaje.Informacion,
                //        Title = T_Module1.GPrt_Caption
                //    });
                    
                //    Cancelar_Click(initObj);
                //    return;
                //}

                //conectamos al server
                //llave de indexacion
                NumOp = initObj.Module1.PrtControl.NumOpe.Cent_Costo + initObj.Module1.PrtControl.NumOpe.Id_Product;
                NumOp = NumOp + initObj.Module1.PrtControl.NumOpe.Id_Especia + initObj.Module1.PrtControl.NumOpe.Id_Empresa;
                NumOp += initObj.Module1.PrtControl.NumOpe.Id_Operacion;

                //no hace nada
                ActivarServer(NumOp, VB6Helpers.CStr((initObj.Frm_Participantes.Llave.Tag)));
                
                return;
            }
        }

        private static short ActivarAplicacion(string Nombre, string Lista)
        {

            return 0;
        }

        //activa al servidor de baja partys
        private static void ActivarServer(string Operacion, string Llave)
        {
            //establecemos el link
            //seteamos el numero de operacion
            //seteamos la llave
            //pedimos realizar la operacion
        }

        public static void Cancelar_Click(InitializationObject initObj)
        {
            initObj.Frm_Iden_Participantes.Aceptar.Tag = VB6Helpers.Format(VB6Helpers.CStr(T_Module1.GPrt_RetCancelo));
            
        }

        public static void Dire_Click(InitializationObject initObj)
        {
            if (initObj.Module1.PrtControl.EsHost != 0)
            {
                return;
            }

            initObj.Frm_Iden_Participantes.Otro.ListIndex = initObj.Frm_Iden_Participantes.Dire.ListIndex;
        }

    }
}
