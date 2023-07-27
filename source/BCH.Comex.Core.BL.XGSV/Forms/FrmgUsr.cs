using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGSV.Forms
{
    public static class FrmgUsr
    {
        public static void Cierre_Click(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            globales.FrmUsr.MsgCierre = string.Empty;
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            // Verificar si ya se realizó el cierre diario
            if (!string.IsNullOrWhiteSpace(globales.VUsr[0].FecOut))
            {
                if (DateTime.Parse(globales.VUsr[0].FecOut).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                {

                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Ya se realizó el Cierre de Comercio Exterior de este día.",
                        Type = TipoMensaje.Informacion,
                        Title = MODGUSR.MsgUsr
                    });

                    globales.FrmUsr.UnCierre = true;
                    globales.FrmUsr.Cierre = false;
                    return;
                }
            }

            // Validar que sea un líder
            if (globales.UsrEsp.jerarquia != 0)
            {

                foreach (var x in globales.VUsr)
                {
                    if (x.Jerarquia != MODGSUP.Jerarquia_Sec && x.CodEsp != "00")
                    {
                        // Validar que cada especialista haya realizado Fin de Día
                        if (x.ConFin == 0)
                        {
                            globales.ListaMensajesError.Add(new UI_Message
                            {
                                Text = "El Cierre Diario se puede realizar sólo si todos los especialistas han hecho Fin de Día.",
                                Type = TipoMensaje.Error,
                                Title = MODGUSR.MsgUsr
                            });
                            globales.FrmUsr.UnCierre = false;
                            globales.FrmUsr.Cierre = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Sólo los líderes están autorizados a realizar el Cierre Diario de Comercio Exterior.",
                    Type = TipoMensaje.Error,
                    Title = MODGUSR.MsgUsr
                });

                return;
            }

            // Si se cumplen todas las condiciones, se efectúa el Cierre
            if (MODGUSR1.SyUpd_Usr(globales.UsrEsp.cent_costo, globales.UsrEsp.id_especia, "O", fechaActual, globales, uow) != 0)
            {
                foreach (var x in globales.VUsr)
                {
                    x.FecOut = fechaActual;
                }

                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "El Cierre Diario de Comercio Exterior se realizó exitosamente.",
                    Type = TipoMensaje.Informacion,
                    Title = MODGUSR.MsgUsr
                });

                globales.FrmUsr.UnCierre = true;
                globales.FrmUsr.Cierre = false;
                globales.FrmUsr.MsgCierre = "El Cierre diario de Comercio Exterior se realizó con fecha " + fechaActual;
            }

        }

        public static void FinDia_Click(string CenCos, string CodEsp, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            // Habilitamos el Log
            using (Tracer tracer = new Tracer())
            {
                int x = 0;
                string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                var item = (from obj in globales.VUsr where obj.CenCos == CenCos && obj.CodEsp == CodEsp select obj).FirstOrDefault();

                if (item.ConFin != 0)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "El usuario ya hizo fin de dia",
                        Type = TipoMensaje.Informacion,
                        Title = MODGUSR1.MsgCie
                    });
                    return;
                }
                else
                {
                    //if (6 == ((int)MigrationSupport.Utils.MsgBox("Esta seguro que quiere forzar el fin de dia de: " + MODGUSR1.VUsr[i].NomEsp, MODGPYF0.pito(36).Cast<MigrationSupport.MsgBoxStyle>(), "")))
                    //{
                    // Se marca el Proceso de Fin de Dia.-
                    tracer.TraceInformation("Inicio Proceso Cierre Diario Forzado (Supervisor).");
                    x = MODGUSR1.SyUpd_Usr(CenCos, CodEsp, "F", fechaActual, globales, uow);
                    if (x != 0)
                    {
                        // indicamos cual es el centro de costo y especialista que se esta cerrando
                        tracer.AddToContext("codcct", CenCos);
                        tracer.AddToContext("codesp", CodEsp);
                        // indicamos el centro de costo y especialista que cerró realmente el dia
                        tracer.AddToContext("codcctori", globales.DatosUsuario.Identificacion_CentroDeCostosOriginal);
                        tracer.AddToContext("codespori", globales.DatosUsuario.Identificacion_IdEspecialistaOriginal);

                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Fin de día realizado exitosamente " + "(" + CenCos + "-" + CodEsp + ")",
                            Type = TipoMensaje.Informacion,
                            Title = MODGUSR1.MsgCie,
                            AutoClose = true
                        });

                        tracer.TraceInformation("El Proceso de Fin de Día Forzado ha terminado exitosamente. (Supervisor)");
                    }

                }
                if (!MODGUSR1.SyGetn_Usr1(globales.UsrEsp.cent_costo, globales, uow))
                {
                    return;
                }
                // }
            }
        }

        public static void Form_Load(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            globales.FrmUsr.MsgCierre = string.Empty;

            if (!MODGUSR1.SyGetn_Usr1(globales.UsrEsp.cent_costo, globales, uow))
            {
                return;
            }

            globales.FrmUsr.Supervisor = globales.UsrEsp.cent_costo + "-" + globales.UsrEsp.id_especia + "  :  " + globales.UsrEsp.nombre;

            globales.FrmUsr.Opcion = new Dictionary<string, string>();
            globales.FrmUsr.Opcion.Add("0", "Sus Especialistas");
            globales.FrmUsr.Opcion.Add("1", "Todos los Especialistas");

            string fechaCierre = globales.VUsr.FirstOrDefault().FecOut;
            globales.FrmUsr.ClassMsgCierre = "warning";

            if (fechaCierre == "01-01-1900 00:00:00" || string.IsNullOrWhiteSpace(fechaCierre))
            {
                globales.FrmUsr.MsgCierre = "El Cierre diario de Comercio Exterior aún no se ha realizado";
                globales.FrmUsr.UnCierre = false;
                globales.FrmUsr.Cierre = true;
            }

            if (!string.IsNullOrWhiteSpace(fechaCierre) && fechaCierre != "01-01-1900 00:00:00")
            {
                globales.FrmUsr.MsgCierre = "El Cierre diario de Comercio Exterior se realizó con fecha " + globales.VUsr[1].FecOut;
                globales.FrmUsr.ClassMsgCierre = "info";
                globales.FrmUsr.UnCierre = true;
            }

        }

        public static bool UnCierre_Click(string CenCos, string CodEsp, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            bool deshacerCierreOK = false;

            globales.FrmUsr.MsgCierre = string.Empty;
            int x = 0;
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            // Se deja sin efecto el Cierre del día.-
            x = MODGUSR1.SyUpd_Usr(CenCos, CodEsp, "N", fechaActual, globales, uow);

            if (x != 0)
            {
                foreach (var item in globales.VUsr)
                {
                    item.FecOut = string.Empty;
                }

                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "El Cierre Diario de Comercio Exterior ha quedado sin efecto.",
                    Type = TipoMensaje.Informacion,
                    Title = MODGUSR1.MsgCie
                });

                globales.FrmUsr.UnCierre = false;
                globales.FrmUsr.Cierre = true;
                globales.FrmUsr.MsgCierre = "El Cierre general aún no se ha realizado";

                deshacerCierreOK = true;
            }
            else
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "No se ha podido dejar sin efecto el Cierre Diario de Comercio Exterior.",
                    Type = TipoMensaje.Error,
                    Title = MODGUSR1.MsgCie
                });

                globales.FrmUsr.UnCierre = true;
                globales.FrmUsr.Cierre = false;
            }

            return deshacerCierreOK;
        }

    }
}
