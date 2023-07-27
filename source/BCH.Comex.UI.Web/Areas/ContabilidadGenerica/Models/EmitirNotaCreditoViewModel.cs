using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using System.Collections.Generic;
using System;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models
{
    public class EmitirNotaCreditoViewModel : RelacionarOperacionViewModel
    {

        public List<FrmgNCFacturasDTO> ListaFacturas { get; set; }

        public EmitirNotaCreditoViewModel() { }

        public EmitirNotaCreditoViewModel(DatosGlobales Globales)
        {
            ListaErrores = Globales.MESSAGES;
            Cb_Producto = Globales.FrmgAsoNC.Cb_Producto;
            Tx_NumOpe_000 = Globales.FrmgAsoNC.Tx_NumOpe_000;
            Tx_NumOpe_001 = Globales.FrmgAsoNC.Tx_NumOpe_001;
            Tx_NumOpe_002 = Globales.FrmgAsoNC.Tx_NumOpe_002;
            Tx_NumOpe_003 = Globales.FrmgAsoNC.Tx_NumOpe_003;
            Tx_NumOpe_004 = Globales.FrmgAsoNC.Tx_NumOpe_004;
            Tx_NumOpe_005 = Globales.FrmgAsoNC.Tx_NumOpe_005;
            Tx_NumOpe_006 = Globales.FrmgAsoNC.Tx_NumOpe_006;
            Tx_DirPrt = Globales.FrmgAsoNC.Tx_DirPrt;
            Tx_NomPrt = Globales.FrmgAsoNC.Tx_NomPrt;
            Tx_RutPrt = Globales.FrmgAsoNC.Tx_RutPrt;

            ListaFacturas = Globales.FrmgAsoNC.Facturas != null ? Globales.FrmgAsoNC.Facturas : new System.Collections.Generic.List<FrmgNCFacturasDTO>();
        }

        public void Update(EmitirNotaCreditoViewModel vm, DatosGlobales Globales)
        {
            Globales.FrmgAsoNC.Cb_Producto = vm.Cb_Producto;
            Globales.FrmgAsoNC.Tx_NumOpe_000 = vm.Tx_NumOpe_000;
            Globales.FrmgAsoNC.Tx_NumOpe_001 = vm.Tx_NumOpe_001;
            Globales.FrmgAsoNC.Tx_NumOpe_002 = vm.Tx_NumOpe_002;
            Globales.FrmgAsoNC.Tx_NumOpe_003 = vm.Tx_NumOpe_003;
            Globales.FrmgAsoNC.Tx_NumOpe_004 = vm.Tx_NumOpe_004;
            Globales.FrmgAsoNC.Tx_NumOpe_005 = vm.Tx_NumOpe_005;
            Globales.FrmgAsoNC.Tx_NumOpe_006 = vm.Tx_NumOpe_006;
            Globales.FrmgAsoNC.Tx_DirPrt = vm.Tx_DirPrt;
            Globales.FrmgAsoNC.Tx_NomPrt = vm.Tx_NomPrt;
            Globales.FrmgAsoNC.Tx_RutPrt = vm.Tx_RutPrt;

            Globales.FrmgAsoNC.Facturas = vm.ListaFacturas;
        }

        public void Update(EmitirNotaCreditoViewModel vm, FrmgNCFacturasDTO factura, DatosGlobales Globales)
        {
            Globales.FrmgAsoNC.Cb_Producto = vm.Cb_Producto;
            Globales.FrmgAsoNC.Tx_NumOpe_000 = vm.Tx_NumOpe_000;
            Globales.FrmgAsoNC.Tx_NumOpe_001 = vm.Tx_NumOpe_001;
            Globales.FrmgAsoNC.Tx_NumOpe_002 = vm.Tx_NumOpe_002;
            Globales.FrmgAsoNC.Tx_NumOpe_003 = vm.Tx_NumOpe_003;
            Globales.FrmgAsoNC.Tx_NumOpe_004 = vm.Tx_NumOpe_004;
            Globales.FrmgAsoNC.Tx_NumOpe_005 = vm.Tx_NumOpe_005;
            Globales.FrmgAsoNC.Tx_NumOpe_006 = vm.Tx_NumOpe_006;
            Globales.FrmgAsoNC.Tx_DirPrt = vm.Tx_DirPrt;
            Globales.FrmgAsoNC.Tx_NomPrt = vm.Tx_NomPrt;
            Globales.FrmgAsoNC.Tx_RutPrt = vm.Tx_RutPrt;

            Globales.FrmgAsoNC.Facturas = vm.ListaFacturas;

            Globales.MODCTA.VNotaCreGl.NumFac = factura.NroFactura;
            Globales.MODCTA.VNotaCreGl.FecOpe = factura.FechaFactura;
            Globales.MODCTA.VNotaCreGl.NumRep = factura.NroReporte;
            Globales.MODCTA.VNotaCreGl.tipofac = factura.Tipo;
            Globales.MODCTA.VNotaCreGl.Moneda = factura.Moneda;
            Globales.MODCTA.VNotaCreGl.monto = factura.Total;
            Globales.MODCTA.VNotaCreGl.netofac = factura.Neto;
            Globales.MODCTA.VNotaCreGl.ivafac = factura.Iva;

        }

    }
}