using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.UI.Web.Areas.Devengo.Models;
using BCH.Comex.UI.Web.Common;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class ConsultaCuentasContablesController : BaseController
    {
        //
        // GET: /Devengo/ConsultaCuentasContables/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaCuentasContables()
        {
            ConsultaCuentasContablesViewModel o = new ConsultaCuentasContablesViewModel();
            return View(o);
        }

        [HttpPost, FileDownload]
        public ActionResult DescargarConsultaCuentasContables(ConsultaCuentasContablesViewModel model, string Command)
        {
            int limitRows = 1048576; //quantity of rows in the excel document - 4 rows of the header
            List<UI_Message> listaMensajes = new List<UI_Message>();
            if (Command == "Volver")
            {
                return new RedirectResult("~/Devengo");
            }
            else
            {
                T_CTACTE ctacte = new T_CTACTE(model.txtNemcta.Text, model.txtNumcta.Text, model.txtCentroCosto.Text, model.txtCuentaCorriente.Text, model.txtFechaDesde.Text, model.txtFechaHasta.Text, model.FiltroSelected);
                this.service.FrmCtaCte_Buscar(ctacte, listaMensajes);

                if (ctacte.DvgCta.Count > (limitRows - 4))
                {
                    listaMensajes.Add(new UI_Message() {
                        Text = "La consulta excede el limite de filas (" + limitRows + ") para el documento Excel a exportar, por favor agregar mas filtros a la consulta.", 
                        Type = TipoMensaje.Error,
                        AutoClose = true
                    });                
                }


                if (listaMensajes.Count == 0)
                {
                    if (ctacte.DvgCta.Count > 0)
                    {
                        MemoryStream ms = this.service.FrmCtaCte_GetFile(ctacte.DvgCta, listaMensajes);
                        if (listaMensajes.Count == 0)
                        {
                            ms.Position = 0;
                            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CONSULTACUENTASCONTABLES.xlsx");
                        }
                    }
                    else
                    {
                        listaMensajes.Add(new UI_Message() { 
                            Text = "No existe información para la consulta realizada.", 
                            Type = TipoMensaje.Error,
                            AutoClose = true}
                        );
                    }
                }
            }

            ConsultaCuentasContablesViewModel o = new ConsultaCuentasContablesViewModel(model);
            o.update(listaMensajes);
            return View("ConsultaCuentasContables", o);
        }
    }
}