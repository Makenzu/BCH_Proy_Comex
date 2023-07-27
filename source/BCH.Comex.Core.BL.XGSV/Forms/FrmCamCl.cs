using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;

namespace BCH.Comex.Core.BL.XGSV.Forms
{
    public class FrmCamCl
    {
        public static void Bot_Aceptar_Click(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            if (globales.FrmCamCl.claveActual != globales.FrmCamCl.claveSup)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Clave Incorrecta",
                    Type = TipoMensaje.Error,
                    Title = "Cambio de Clave"
                });
                return;
            }

            if (globales.FrmCamCl.nuevaClave != globales.FrmCamCl.nuevaClaveComp)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Error en la Nueva Clave, Ingresela Nuevamente",
                    Type = TipoMensaje.Error,
                    Title = "Cambio de Clave"
                });

                globales.FrmCamCl.nuevaClave = string.Empty;
                globales.FrmCamCl.nuevaClaveComp = string.Empty;
                return;
            }

            if (!MODGUSR.SyUpd2_Usr(globales, uow))
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Error al cambiar clave",
                    Type = TipoMensaje.Error,
                    Title = "Cambio de Clave"
                });
                return;
            }
            else
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Clave Modificada",
                    Type = TipoMensaje.Informacion,
                    Title = "Cambio de Clave"
                });
                globales.FrmCamCl.claveSup = MODGUSR.SyGet_ClaUsr(globales, uow);
                globales.FrmCamCl.claveActual = string.Empty;
                globales.FrmCamCl.nuevaClave = string.Empty;
                globales.FrmCamCl.nuevaClaveComp = string.Empty;
            }

        }

        public static void Form_Load(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            globales.FrmCamCl.claveSup = MODGUSR.SyGet_ClaUsr(globales, uow);
        }

    }
}
