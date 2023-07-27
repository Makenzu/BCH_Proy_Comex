using BCH.Comex.Core.BL.XCFT;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Models.FundTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models
{
    public class BaseController : Controller
    {
        private FundTransferService ftService;
        private Dictionary<string, Action> initialize;
        private Dictionary<string, Func<Object>> viewModels;

        public BaseController()
        {
            ftService = new FundTransferService();
            initialize = new Dictionary<string, Action>();
            viewModels = new Dictionary<string, Func<object>>();

            #region INITIALIZE
            initialize.Add("ComercioInvisible", () =>
            {
                ftService.LoadFrmComercioInvisible(this.InitObject);
            });
            initialize.Add("Ingreso_Valores", () =>
            {
                ftService.LoadIngresoValores(this.InitObject);
            });
            initialize.Add("Arbitrajes", () =>
            {
                ftService.LoadFrmArbitrajes(this.InitObject);
            });
            initialize.Add("ComercioVisibleExport", () =>
            {
                ftService.COMVISEXP_Form_Load(this.InitObject);
            });
            initialize.Add("OrigenFondos", () =>
            {
                ftService.ORIFOND_Form_Load(this.InitObject);
                ftService.ORIFOND_Form_Activate(this.InitObject);
                ftService.ORIFOND_l_mnd_Click(this.InitObject);
                ftService.ORIFOND_L_Cta_Click(this.InitObject);
                ftService.ORIFOND_L_Partys_Click(this.InitObject);
                ftService.ORIFOND_L_Cuentas_Click(this.InitObject);
            });
            #endregion
            #region VIEW MODELS
            viewModels.Add("Index", () => GetIndexViewModel());
            viewModels.Add("ComercioInvisible", () => GetComercioInvisibleViewModel());
            viewModels.Add("Ingreso_Valores", () => GetIngresoValoresViewModel());
            viewModels.Add("Arbitrajes", () => GetArbitrajesViewModel());
            viewModels.Add("ComercioVisibleExport", () => GetComercioVisibleExportViewModel());
            //viewModels.Add("OrigenFondos", () => GetOrigenFondosViewModel());
            #endregion 

           
        }

        public InitializationObject InitObject
        {
            get
            {
                var res = Session[SessionKeys.FundTransfer.InitializationObjectKey] as InitializationObject;
                //if (res == null)
                //{
                //    res = ftService.Setup();
                //    this.InitObject = res;
                //}
                return res;
            }
            set
            {
                Session[SessionKeys.FundTransfer.InitializationObjectKey] = value;
            }
        }

        #region UTILIDADES FUNDTRANSFER
        public IndexViewModel GetIndexViewModel()
        {
            var model = new IndexViewModel(this.InitObject.Frm_Principal, InitObject.Mdi_Principal);
            return model;
        }
        public ComercioInvisibleViewModel GetComercioInvisibleViewModel()
        {
            ComercioInvisibleViewModel modelo = new ComercioInvisibleViewModel();
            modelo.CB_Moneda = this.InitObject.Frm_Comercio_Invisible.Cb_Moneda;
                //new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_Moneda.Items.Select(
                //x => new SelectListItem()
                //{
                //    Value = x.ID,
                //    Text = x.Value
                //}));
            modelo.CB_Pais = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_Pais.Items.Select(
                x => new SelectListItem()
                {
                    Value = x.ID,
                    Text = x.Value
                }));
            modelo.Cb_Divisa = this.InitObject.Frm_Comercio_Invisible.Cb_Divisa;
                //new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_Divisa.Items.Select(
                //x => new SelectListItem()
                //{
                //    Text = x.Value,
                //    Value = x.ID
                //}));
            modelo.Lt_Tcp = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_InsUt = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_InsUt.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_ArCon = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_ArCon.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_MonDes = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_MonDes.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_SecEcBen = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_SecEcBen.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_SecEcIn = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_SecEcIn.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_TipAut = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_TipAut.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));

            modelo.indexMoneda = this.InitObject.Frm_Comercio_Invisible.Cb_Moneda.ListIndex;
            modelo.indexPais = this.InitObject.Frm_Comercio_Invisible.Cb_Pais.ListIndex;
            modelo.indexDivisa = this.InitObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex;
            modelo.indexLt_Tcp = this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.ListIndex;
            modelo.indexInsUt = this.InitObject.Frm_Comercio_Invisible.Cb_InsUt.ListIndex;
            modelo.indexArCon = this.InitObject.Frm_Comercio_Invisible.Cb_ArCon.ListIndex;
            modelo.indexMonDes = this.InitObject.Frm_Comercio_Invisible.Cb_MonDes.ListIndex;
            modelo.indexSecEcBen = this.InitObject.Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex;
            modelo.indexSecEcIn = this.InitObject.Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex;
            modelo.indexTipAut = this.InitObject.Frm_Comercio_Invisible.Cb_TipAut.ListIndex;


            //************************ FR OPE ******************************************
            modelo.Tx_MtoCV[0] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[0].Text; ;
            modelo.BTx_MtoCV[0] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[0].Enabled;
            modelo.Tx_MtoCV[1] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text; ;
            modelo.BTx_MtoCV[1] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled;
            modelo.Tx_MtoCV[2] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Text; ;
            modelo.BTx_MtoCV[2] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Enabled;
            modelo.Tx_MtoCV[3] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[3].Text; ;
            modelo.BTx_MtoCV[3] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled;

            modelo.Tx_CanAc = this.InitObject.Frm_Comercio_Invisible.Tx_CanAc.Text;
            //***************************************************

            //************************ FR OPE D ******************************************
            modelo.Tx_NumCon = this.InitObject.Frm_Comercio_Invisible.Tx_NumCon.Text;
            modelo.Tx_FecSus = this.InitObject.Frm_Comercio_Invisible.Tx_FecSus.Text;
            modelo.Tx_FecVen = this.InitObject.Frm_Comercio_Invisible.Tx_FecVen.Text;
            modelo.Tx_ParTip = this.InitObject.Frm_Comercio_Invisible.Tx_ParTip.Text;
            //***************************************************

            //************************ FR OFI ******************************************
            modelo.Tx_NumIns = this.InitObject.Frm_Comercio_Invisible.Tx_NumIns.Text;
            modelo.Tx_FecIns = this.InitObject.Frm_Comercio_Invisible.Tx_FecIns.Text;
            modelo.Tx_NomFin = this.InitObject.Frm_Comercio_Invisible.Tx_NomFin.Text;
            modelo.Tx_FecVC = this.InitObject.Frm_Comercio_Invisible.Tx_FecVC.Text;
            modelo.Tx_Fecha = this.InitObject.Frm_Comercio_Invisible.Tx_Fecha.Text;
            modelo.Tx_Mto = this.InitObject.Frm_Comercio_Invisible.Tx_Mto.Text;
            //***************************************************

            //************************ FR SEC ******************************************
            modelo.Tx_PrcPar = this.InitObject.Frm_Comercio_Invisible.Tx_PrcPar.Text;
            //***************************************************

            //************************ FR CONVENIO ******************************************
            modelo.Tx_FecDeb = this.InitObject.Frm_Comercio_Invisible.Tx_FecDeb.Text;
            modelo.Tx_DocNac = this.InitObject.Frm_Comercio_Invisible.Tx_DocNac.Text;
            modelo.Tx_DocExt = this.InitObject.Frm_Comercio_Invisible.Tx_DocExt.Text;
            //***************************************************

            //************************ FR AUTORI ******************************************
            modelo.Tx_NroAut = this.InitObject.Frm_Comercio_Invisible.Tx_NroAut.Text;
            modelo.Tx_FecAut = this.InitObject.Frm_Comercio_Invisible.Tx_FecAut.Text;
            modelo.Tx_SucBcch = this.InitObject.Frm_Comercio_Invisible.Tx_SucBcch.Text;
            //***************************************************

            //************************ FR DECLARACION **************************************
            modelo.Tx_NroDec = this.InitObject.Frm_Comercio_Invisible.Tx_NroDec.Text;
            modelo.Tx_FecDec = this.InitObject.Frm_Comercio_Invisible.Tx_FecDec.Text;
            modelo.Tx_CodAdn = this.InitObject.Frm_Comercio_Invisible.Tx_CodAdn.Text;
            modelo.Tx_ER = this.InitObject.Frm_Comercio_Invisible.Tx_ER.Text;
            //***************************************************

            //*********************** FRAMES ************************************************
            modelo.Fr_Ope_V = this.InitObject.Frm_Comercio_Invisible.Fr_Ope.Visible;
            modelo.Fr_Ope_D_V = this.InitObject.Frm_Comercio_Invisible.Fr_OpeD.Visible;
            modelo.Fr_Ofi_V = this.InitObject.Frm_Comercio_Invisible.Fr_OFI.Visible;
            modelo.Fr_Sec_V = this.InitObject.Frm_Comercio_Invisible.Fr_Sec.Visible;
            modelo.Fr_Convenio_V = this.InitObject.Frm_Comercio_Invisible.Fr_Convenio.Visible;
            modelo.Fr_Autori_V = this.InitObject.Frm_Comercio_Invisible.Fr_Autori.Visible;
            modelo.Fr_Declaracion_V = this.InitObject.Frm_Comercio_Invisible.Fr_Declaracion.Visible;
            modelo.Fr_OpRe_V = this.InitObject.Frm_Comercio_Invisible.Fr_OpRe.Visible;

            modelo.Fr_Ope_E = this.InitObject.Frm_Comercio_Invisible.Fr_Ope.Enabled;
            modelo.Fr_Ope_D_E = this.InitObject.Frm_Comercio_Invisible.Fr_OpeD.Enabled;
            modelo.Fr_Ofi_E = this.InitObject.Frm_Comercio_Invisible.Fr_OFI.Enabled;
            modelo.Fr_Sec_E = this.InitObject.Frm_Comercio_Invisible.Fr_Sec.Enabled;
            modelo.Fr_Convenio_E = this.InitObject.Frm_Comercio_Invisible.Fr_Convenio.Enabled;
            modelo.Fr_Autori_E = this.InitObject.Frm_Comercio_Invisible.Fr_Autori.Enabled;
            modelo.Fr_Declaracion_E = this.InitObject.Frm_Comercio_Invisible.Fr_Declaracion.Enabled;
            modelo.Fr_OpRe_E = this.InitObject.Frm_Comercio_Invisible.Fr_OpRe.Enabled;

            //***************************************************

            //******* OPERACIONES
            int i = 0;
            foreach (var elem in this.InitObject.Frm_Comercio_Invisible.Lt_Operacion.Items)
            {
                modelo.Lt_Operacion.Add(new Lt_Operacion_Model()
                {
                    Indice = i++,
                    Moneda = elem.GetColumn("moneda"),
                    Monto = elem.GetColumn("monto"),
                    Tipo = elem.GetColumn("tipo")
                });
            }

            modelo.Errores = this.InitObject.Frm_Comercio_Invisible.Errors.Select(x => x.Text).ToList();
            this.InitObject.Frm_Comercio_Invisible.Errors.Clear();
            //*******************
            return modelo;
        }
        public IngresoValoresViewModel GetIngresoValoresViewModel()
        {
            IngresoValoresViewModel modelo = new IngresoValoresViewModel();
            modelo.Moneda = this.InitObject.Frm_Ingreso_Valores.Moneda.Text;
            modelo.Fecha = this.InitObject.Frm_Ingreso_Valores.Fecha.Text;
            modelo.Paridad = this.InitObject.Frm_Ingreso_Valores.Paridad.Text;
            modelo.TipoCambio = this.InitObject.Frm_Ingreso_Valores.TC_Obs.Text;
            return modelo;
        }
        public ArbitrajesViewModel GetArbitrajesViewModel()
        {
            ArbitrajesViewModel modelo = new ArbitrajesViewModel();
            //modelo.irAIngresoValores = (this.InitObject.FormularioQueAbrir == "Ingreso_Valores");
            //modelo.Headers = this.InitObject.Frm_Arbitrajes.Lt_Operacion.Header;
            //modelo.Cb_Pais = this.InitObject.Frm_Arbitrajes.Cb_Pais.Items.Select(x => new SelectListItem()
            //{
            //    Text = x.Value,
            //    Value = x.ID
            //}).ToList();
            //modelo.Cb_Moneda_Compra = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Compra.Items.Select(x => new SelectListItem()
            //{
            //    Text = x.Value,
            //    Value = x.ID
            //}).ToList();
            //modelo.Cb_Moneda_Venta = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Venta.Items.Select(x => new SelectListItem()
            //{
            //    Text = x.Value,
            //    Value = x.ID
            //}).ToList();

            //modelo.indexPais = this.InitObject.Frm_Arbitrajes.Cb_Pais.ListIndex;
            //modelo.idPais = int.Parse(this.InitObject.Frm_Arbitrajes.Cb_Pais.Items.ElementAt(this.InitObject.Frm_Arbitrajes.Cb_Pais.ListIndex).ID);
            //modelo.indexMonedaVenta = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Venta.ListIndex;
            //modelo.indexMonedaCompra = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Compra.ListIndex;

            //modelo.Cb_Pais_Habilitado = this.InitObject.Frm_Arbitrajes.Cb_Pais.Enabled;

            //modelo.Tx_Mtoarb_000 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Text;
            //modelo.Tx_Mtoarb_001 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[1].Text;
            //modelo.Tx_Mtoarb_002 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[2].Text;
            //modelo.Tx_Mtoarb_003 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[3].Text;
            //modelo.Tx_Mtoarb_004 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[4].Text;
            //modelo.Tx_Mtoarb_005 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[5].Text;

            //modelo.Lt_Operacion = this.InitObject.Frm_Arbitrajes.Lt_Operacion.Items.Select((x => new Lt_Operacion_Arbitraje_Model()
            //{
            //    MonedaCompra = x.GetColumn("mdacom"),
            //    MontoCompra = x.GetColumn("moncom"),
            //    MonedaVenta = x.GetColumn("mdaven"),
            //    MontoVenta = x.GetColumn("monven")
            //})).ToList();

            //modelo.MensajesDeError = this.InitObject.Frm_Arbitrajes.PopUps.Where(x => x.Type == TipoMensaje.Error || x.Type == TipoMensaje.Informacion).Select(x => x.Text).ToList();
            //modelo.MensajesDeConfirmacion = this.InitObject.Frm_Arbitrajes.PopUps.Where(x => x.Type == TipoMensaje.YesNo).Select(x => x.Text).ToList();
            //this.InitObject.Frm_Arbitrajes.PopUps = new List<UI_Message>();
            //modelo.Ch_Futuro = (this.InitObject.Frm_Arbitrajes.Ch_Futuro.Value == -1) ? true : false;
            return modelo;
        }
        public ComercioVisibleExportViewModel GetComercioVisibleExportViewModel()
        {
            var model = new ComercioVisibleExportViewModel();
            model.Caption = this.InitObject.Frm_VisE.Caption;
            model.Cb_Mnd = this.InitObject.Frm_VisE.Cb_Mnd.Items.Select(x => new SelectListItem()
            {
                Value = x.ID,
                Text = x.Value
            }).ToList();
            model.indexCbMnd = this.InitObject.Frm_VisE.Cb_Mnd.ListIndex;
            model.idCbMnd = int.Parse(this.InitObject.Frm_VisE.Cb_Mnd.Items.ElementAt(model.indexCbMnd).ID);
            model.Tx_MtoVisE_000 = this.InitObject.Frm_VisE.Tx_MtoVisE[0].Text;
            model.Tx_MtoVisE_001 = this.InitObject.Frm_VisE.Tx_MtoVisE[1].Text;
            model.Tx_MtoVisE_002 = this.InitObject.Frm_VisE.Tx_MtoVisE[2].Text;
            model.Tx_MtoVisE_003 = this.InitObject.Frm_VisE.Tx_MtoVisE[3].Text;
            model.MensajesDeError = this.InitObject.Frm_VisE.Messages;
            this.InitObject.Frm_VisE.Messages.Clear();
            return model;
        }
        //public OrigenFondosViewModel GetOrigenFondosViewModel()
        //{
        //    var model = new OrigenFondosViewModel();

        //    model.l_mto_headers = this.InitObject.Frm_Origen_Fondos.l_mto.Header;
        //    model.l_mto_items = this.InitObject.Frm_Origen_Fondos.l_mto.Items.Select(x => new L_Mto_Model()
        //    {
        //        monto = x.GetColumn("tot"),
        //        moneda = x.GetColumn("nom"),
        //        Origen = x.GetColumn("ori")
        //    }).ToList();

        //    //TODO: CAMBIAR EL HEADER
        //    //model.l_ori_headers = this.InitObject.Frm_Origen_Fondos.l_ori.Header;
        //    model.l_ori_items = this.InitObject.Frm_Origen_Fondos.l_ori.Items.Select(x => new L_Fondo_Model()
        //    {

        //        monto = x.GetColumn("tot"),
        //        moneda = x.GetColumn("nom")
        //    }).ToList();

        //    model.l_mnd = this.InitObject.Frm_Origen_Fondos.l_mnd.Items.Select(x => new SelectListItem()
        //    {
        //        Text = x.Value,
        //        Value = x.ID
        //    }).ToList();
        //    try
        //    {
        //        model.selected_mnd_id = int.Parse(this.InitObject.Frm_Origen_Fondos.l_mnd.get_ItemData(this.InitObject.Frm_Origen_Fondos.l_mnd.ListIndex));
        //    }
        //    catch (Exception e)
        //    {
        //        model.selected_mnd_id = -1;
        //    }


        //    model.L_Partys = this.InitObject.Frm_Origen_Fondos.L_Partys.Items.Select(x => new SelectListItem()
        //    {
        //        Text = x.Value,
        //        Value = x.ID
        //    }).ToList();
        //    try
        //    {
        //        model.selected_partys_id = int.Parse(this.InitObject.Frm_Origen_Fondos.L_Partys.get_ItemData(this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex));
        //    }
        //    catch (Exception e)
        //    {
        //        model.selected_partys_id = -1;
        //    }


        //    model.L_Cuentas = this.InitObject.Frm_Origen_Fondos.L_Cuentas.Items.Select(x => new SelectListItem()
        //    {
        //        Text = x.Value,
        //        Value = x.ID
        //    }).ToList();
        //    try
        //    {
        //        model.selected_cuentas_id = int.Parse(this.InitObject.Frm_Origen_Fondos.L_Partys.get_ItemData(this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex));
        //    }
        //    catch (Exception e)
        //    {
        //        model.selected_cuentas_id = -1;
        //    }


        //    model.cmb_codtran = this.InitObject.Frm_Origen_Fondos.cmb_codtran.Items.Select(x => new SelectListItem()
        //    {
        //        Text = x.Value,
        //        Value = x.ID
        //    }).ToList();
        //    try
        //    {
        //        model.selected_cmb_codtran = int.Parse(this.InitObject.Frm_Origen_Fondos.cmb_codtran.get_ItemData(this.InitObject.Frm_Origen_Fondos.cmb_codtran.ListIndex));
        //    }
        //    catch (Exception e)
        //    {
        //        model.selected_cmb_codtran = -1;
        //    }

        //    model.L_Cta = this.InitObject.Frm_Origen_Fondos.L_Cta.Items.Select(x => new SelectListItem()
        //    {
        //        Text = x.Value,
        //        Value = x.ID
        //    }).ToList();
        //    try
        //    {
        //        model.selected_l_cta_id = int.Parse(this.InitObject.Frm_Origen_Fondos.L_Cta.get_ItemData(this.InitObject.Frm_Origen_Fondos.L_Cta.ListIndex));
        //    }
        //    catch (Exception e)
        //    {
        //        model.selected_l_cta_id = -1;
        //    }


        //    model.txt_cuenta = this.InitObject.Frm_Origen_Fondos.txt_cuenta.Text;
        //    model.txt_CRN = this.InitObject.Frm_Origen_Fondos.txt_CRN.Text;

        //    model.frm_infoctagap_enabled = this.InitObject.Frm_Origen_Fondos.frm_infoctagap.Enabled;
        //    model.frm_infoctagap_visible = this.InitObject.Frm_Origen_Fondos.frm_infoctagap.Visible;

        //    return model;
        //}
        #endregion
    }
}