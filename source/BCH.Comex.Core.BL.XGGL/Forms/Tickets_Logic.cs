using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class Tickets_Logic
    {
        public static void Form_Load(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            UI_Tickets Tickets = Globales.Tickets;
            T_MODGTIC MODGTIC = Globales.MODGTIC;
            T_MODTDME MODTDME = Globales.MODTDME;
            int i = 0;
            int X = 0;

            Tickets.CBO_DeHa.Items.Add(new Common.UI_Modulos.UI_ComboItem() { Value = "Débito", Data = 0 });
            Tickets.CBO_DeHa.Items.Add(new Common.UI_Modulos.UI_ComboItem() { Value = "Crédito", Data = 1 });

            Tickets.CAM_Nombre.Text = MODGTIC.Strtic.Nomtic;
            Tickets.CAM_Nemonico.Text = MODGTIC.Strtic.Nemtic;
            Tickets.CAM_Monto.Text = MODGTIC.Strtic.Montic;
            Tickets.CAM_Cuenta.Text = MODGTIC.Strtic.Cuetic;
            Tickets.CBO_DeHa.ListIndex = MODGTIC.Strtic.Dehtic;
            
            // Carga datos a combo Cb_ticket de tabla Sce_Tdme      .-
            // Datos Sce_Tdme: VTDme(i%).CodDme ... VTDme(i%).DesDme.-

            X = BCH.Comex.Core.BL.XGGL.Modulos.MODTDME.SyGetn_Tdme(Globales,unit);

            for (i = 1; i <= MODTDME.VTDme.GetUpperBound(0); i += 1)
            {
                Tickets.Cb_ticket.Items.Add(new Common.UI_Modulos.UI_ComboItem()
                {
                    Value= MODGPYF1.Minuscula(MODTDME.VTDme[i].DesDme),
                    Data=i
                });
            }

            if (MODGTIC.Strtic.Glosa != "")
            {
                Tickets.CAM_Concepto.Text = MODGTIC.Strtic.Glosa;
            }
            else
            {
                Tickets.Cb_ticket.ListIndex = 0;
                Tickets.CAM_Concepto.Text = Tickets.Cb_ticket.Items[0].Value;
            }
        }

        public static void Aceptar_Click(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGTIC MODGTIC = Globales.MODGTIC;
            UI_Tickets Tickets = Globales.Tickets;
            if (Tickets.CAM_Cuenta.Text != "" && Tickets.CAM_Concepto.Text.TrimB() == "")
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text= "Debe ingresar concepto de la cuenta corriente",
                    Title= "Ticket",
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });
                return;
            }

            // If CAM_Concepto.Text = "" Then
            //     CAM_Concepto.Text = "Ajuste Contable"
            // End If
            MODGTIC.Strtic.Contic = Tickets.CAM_Concepto.Text;
            Tickets.Tag = "A";
            if (Tickets.MONTO == null)
            {
                Tickets.MONTO = new double[] { Format.StringToDouble(Tickets.CAM_Monto.Text) };
            }
            Globales.STR_TICKETS = Frm_gl.LisDeCr_2(Globales, unit, Tickets.IMPUESTO, Tickets.TIP, Tickets.S, Tickets.MONTO, Tickets.A, Tickets.ST);
        }

        public static void Cancelar_Click(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGTIC MODGTIC = Globales.MODGTIC;
            UI_Tickets Tickets = Globales.Tickets;
            if (Tickets.CAM_Cuenta.Text != "" && Tickets.CAM_Concepto.Text.TrimB() == "")
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Title="Ticket",
                    Text= "Debe ingresar concepto de la cuenta corriente",
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });
                return;
            }

            // If CAM_Concepto.Text = "" Then
            //     CAM_Concepto.Text = "Ajuste Contable"
            // End If

            MODGTIC.Strtic.Contic = Tickets.CAM_Concepto.Text;
            Tickets.Tag = "C";
            Globales.Controller = "Grabar";
            Globales.Action = "Ticket_Post";
            Globales.STR_TICKETS = Frm_gl.LisDeCr_2(Globales, unit, Tickets.IMPUESTO, Tickets.TIP, Tickets.S, Tickets.MONTO, Tickets.A, Tickets.ST);
        }

        public static void otro_Click(DatosGlobales Globales)
        {
            UI_Tickets Tickets = Globales.Tickets;
            T_MODGTIC MODGTIC = Globales.MODGTIC;
            if (Tickets.otro.Checked == true)
            {
                MODGTIC.Strtic.Demtci = true.ToInt();
            }
            else
            {
                MODGTIC.Strtic.Demtci = false.ToInt();
            }

        }
    }
}
