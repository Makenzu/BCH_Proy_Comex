using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.XGSV.Forms
{
    public static class FrmCeder
    {
        public static void Form_Load(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
           
            MODUSRS.SyGetv_Usr(String.Empty, uow);

            foreach (var user in MODUSRS.VUsrs)
            {
                string userInfo = String.Format("{0}-{1}   {2}", user.CCtUsr.Trim(), user.codusr.Trim(), user.NomUsr.Trim());

                if (user.CCtUsr == globales.UsrEsp.cent_costo)
                    globales.FrmCeder.UsuariosActuales.Add(userInfo);

                globales.FrmCeder.UsuariosNuevos.Add(userInfo);
            }
            MODPROD.SyGetn_Pro(globales, uow);
          
        }

        public static void Form_ObtenerClientes(DatosGlobales globales, UnitOfWorkCext01 uow, string usuarioActual, string producto)
        {
            if (!String.IsNullOrWhiteSpace(usuarioActual) && !string.IsNullOrWhiteSpace(producto))
            {
                string centroCosto   = usuarioActual.Substring(0, 6).Substring(0, 3);
                string codigoUsuario = usuarioActual.Substring(0, 6).Substring(4, 2);
                MODCEDER.SyGetn_Prt(globales, uow, centroCosto, codigoUsuario, producto);
            }
        }

        public static void Form_ObtenerOperaciones(DatosGlobales globales, UnitOfWorkCext01 uow, string usuarioActual, string producto, string clienteID)
        {
            if (!String.IsNullOrEmpty(usuarioActual))
            {
                string centroCosto   = usuarioActual.Substring(0, 6).Substring(0, 3);
                string codigoUsuario = usuarioActual.Substring(0, 6).Substring(4, 2);

                MODCEDER.SyGetn_Prod(globales, uow, centroCosto, codigoUsuario, producto, clienteID);
            }
        }

        public static void Form_Save(DatosGlobales globales, UnitOfWorkCext01 uow, string usuarioActual, string usuarioNuevo, string cliente, string producto)
        {
            string centroCostoActual   = usuarioActual.Substring(0, 6).Substring(0, 3);
            string codigoUsuarioActual = usuarioActual.Substring(0, 6).Substring(4, 2);
            string centroCostoNuevo    = usuarioNuevo.Substring(0, 6).Substring(0, 3);
            string codigoUsuarioNuevo  = usuarioNuevo.Substring(0, 6).Substring(4, 2);
           
            if (cliente.Length > 0)
            {
                cliente = cliente.Substring(0, 12).Trim();
                MODCEDER.SyPut_Cart(globales, uow, centroCostoActual, codigoUsuarioActual, centroCostoNuevo, codigoUsuarioNuevo, cliente, producto);
            }
        }

        private static bool Fn_Verifica_Producto(DatosGlobales globales)
        {
            bool returnValue = false;


            int i = 0;
            bool contador = false;
            var producto = string.Empty;

            if (!contador)
            {
                globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Atención: Es necesario que seleccione al menos un producto para poder realizar la operación deseada.",
                        Type = TipoMensaje.Informacion,
                        Title = MODCEDER.MsgCeder
                    });

                return false;  
            }


            return true;
        }

        public static bool ObtenerProductos(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            return MODPROD.SyGetn_Pro(globales, uow);
        }

    }
}
