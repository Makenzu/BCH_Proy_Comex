using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_Consulta
    {
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            var Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
            Mdl_Funciones_Varias.RUT_PARTY_SERV = "";
            Mdl_Funciones_Varias.NOM_PARTY_SERV = "";
            Mdl_Funciones_Varias.TIP_CTA_SERV = "";
            Mdl_Funciones_Varias.CARGA_AUTOMATICA = 0;
            Mdl_Funciones_Varias.LC_MONEDA = 0;
            Mdl_Funciones_Varias.LC_MONTO = "";
            Mdl_Funciones_Varias.LC_XREF = "";
            Mdl_Funciones_Varias.LC_CONREFNUM = "";
            Mdl_Funciones_Varias.LC_PRD = "";
            Mdl_Funciones_Varias.LC_OUTGOING = "";
            Mdl_Funciones_Varias.LC_INCOMING = "";
            Mdl_Funciones_Varias.LC_ORD_INST1 = "";  //DESTINO FONDOS
            Mdl_Funciones_Varias.LC_PMNT_DET1 = "";  //DESTINO FONDOS
            Mdl_Funciones_Varias.LC_PMNT_DET2 = "";  //DESTINO FONDOS
            Mdl_Funciones_Varias.LC_PMNT_DET3 = "";  //DESTINO FONDOS
            Mdl_Funciones_Varias.LC_PMNT_DET4 = "";  //DESTINO FONDOS
            Mdl_Funciones_Varias.LC_COD_TRANS = "";  //CODIGO TRANSACCION
            Mdl_Funciones_Varias.LC_DEBIT_REF = "";  //DESTINO FONDOS
            Mdl_Funciones_Varias.LC_SWFT = "";
            Mdl_Funciones_Varias.LC_BEN_INST1 = "";  //BEN_INST1
            Mdl_Funciones_Varias.LC_ULT_BEN1 = "";  //ULT_BEN1
            Mdl_Funciones_Varias.LC_ULT_BEN2 = "";  //ULT_BEN2
            Mdl_Funciones_Varias.LC_ULT_BEN3 = "";  //ULT_BEN3
            Mdl_Funciones_Varias.LC_ULT_BEN4 = "";  //ULT_BEN4
            Mdl_Funciones_Varias.LC_CHG_WHOM = "";  //CHG_WHOM
            Mdl_Funciones_Varias.LC_FCCFT = "";  //FCCFT
            Mdl_Funciones_Varias.LC_DRVALDT = "";
            Mdl_Funciones_Varias.LC_NOM_MDA = "";
            Mdl_Funciones_Varias.LC_INTRMD1 = "";
            Mdl_Funciones_Varias.LC_INTRMD2 = "";
            Mdl_Funciones_Varias.LC_US_PAY_ID = "";
            Mdl_Funciones_Varias.LC_RECVR_CORRES1 = "";
            Mdl_Funciones_Varias.LC_RECVR_CORRES2 = "";
            Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1 = "";
            Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2 = "";
            Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3 = "";
            Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4 = "";
            Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5 = "";
            Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6 = "";

            //ESTADOS
            initObj.Frm_Consulta.cmb_estado.AddItem(1, "Vigente");
            initObj.Frm_Consulta.cmb_estado.AddItem(2, "Cursada");
            initObj.Frm_Consulta.cmb_estado.AddItem(3, "Anulada");
            initObj.Frm_Consulta.cmb_estado.ListIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public static List<pro_sce_prty_s04_MS_Result> img_Buscar_Click(InitializationObject initObj, UnitOfWorkCext01 uow, string nroTransaccion, string estado)
        {
            initObj.Frm_Consulta.img_Pare.Tag = "";
            initObj.Frm_Consulta.img_Buscar.Visible = false;
            initObj.Frm_Consulta.img_Pare.Visible = true;
            
            VB6Helpers.DoEvents();
            if (VB6Helpers.CStr(initObj.Frm_Consulta.img_Pare.Tag) == "*")
            {
                return new List<pro_sce_prty_s04_MS_Result>();
            }
            
            initObj.Frm_Consulta.img_Buscar.Visible = true;
            initObj.Frm_Consulta.img_Pare.Visible = false;
            
            return consulta_operaciones(initObj, uow, nroTransaccion, estado);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="estado"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<pro_sce_prty_s04_MS_Result> img_Buscar_Click(InitializationObject initObj, UnitOfWorkCext01 uow, string nroTransaccion, string estado, string year)
        {
            initObj.Frm_Consulta.img_Pare.Tag = "";
            initObj.Frm_Consulta.img_Buscar.Visible = false;
            initObj.Frm_Consulta.img_Pare.Visible = true;

            VB6Helpers.DoEvents();
            if (VB6Helpers.CStr(initObj.Frm_Consulta.img_Pare.Tag) == "*")
            {
                return new List<pro_sce_prty_s04_MS_Result>();
            }

            initObj.Frm_Consulta.img_Buscar.Visible = true;
            initObj.Frm_Consulta.img_Pare.Visible = false;

            return consulta_operaciones(initObj, uow, nroTransaccion, estado, year);
        }

        public static void img_Pare_Click(InitializationObject initObj)
        {
            initObj.Frm_Consulta.img_Pare.Tag = "*";
        }
        public static void lbl_Cerrar_Click(InitializationObject initObj)
        {
            //VB6Helpers.Unload(this);
        }
        public static void lbl_limpiar_Click(InitializationObject initObj)
        {
            //TODO ARKANO Mdl_Funciones_Varias.Limpia_Grilla(this.msg_operaciones);
            initObj.Frm_Consulta.txt_folio.Text = "";
            initObj.Frm_Consulta.txt_tot_oper.Text = "";
            initObj.Frm_Consulta.cmb_estado.ListIndex = -1;
        }
        public static void msg_operaciones_DblClick(InitializationObject initObj, UnitOfWorkCext01 uow, pro_sce_prty_s04_MS_Result data)
        {
            var Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
            var mdi_Principal = initObj.Mdi_Principal;

            if (Convert.ToInt32(data.estado) == 1)
            {
                //Al hacer doble clic sobre una operación en la grilla se debe realizar la carga automatica.
                Mdl_Funciones_Varias.CARGA_AUTOMATICA = 1;
                initObj.Frm_Consulta.items[data.row].Seleccionado = 1;
                initObj.MODXORI.gb_esCosmos = true;
                
                //DEBE BUSCAR LOS DATOS FALTANTES DE LA OPERACION
                Mdl_Funciones_Varias.LC_MONEDA = VB6Helpers.CShort(data.codmnd_bch);  //CODIGO MONEDA
                Mdl_Funciones_Varias.LC_MONTO = VB6Helpers.CStr(data.DRAMT.Replace(".",","));  //MONTO
                Mdl_Funciones_Varias.LC_XREF = VB6Helpers.CStr(data.XREF);  //XREFerencia

                string _switchVar1 = data.TIPO_TRX;
                if (_switchVar1 == "Outgoing 103")
                {
                    Mdl_Funciones_Varias.LC_PRD = "72";
                }
                else if (_switchVar1 == "Outgoing 202")
                {
                    Mdl_Funciones_Varias.LC_PRD = "74";
                }
                else if (_switchVar1 == "Incoming")
                {
                    Mdl_Funciones_Varias.LC_PRD = "62";
                }

                //LC_PRD = Me.msg_operaciones.TextMatrix(Me.msg_operaciones.RowSel, 7)  //IDENTIFICACION PARTICIPANTE
                Mdl_Funciones_Varias.LC_INCOMING = VB6Helpers.CStr(data.Beneficiario);  //072-074 OUTGOING CRACC
                Mdl_Funciones_Varias.LC_OUTGOING = VB6Helpers.CStr(data.Ordenante);  //062 INCOMING     DRACC
                Mdl_Funciones_Varias.LC_ORD_INST1 = VB6Helpers.CStr(data.ORD_INST1);  //DESTINO FONDOS
                Mdl_Funciones_Varias.LC_PMNT_DET1 = VB6Helpers.CStr(data.PMNT_DET1);  //DESTINO FONDOS
                Mdl_Funciones_Varias.LC_PMNT_DET2 = VB6Helpers.CStr(data.PMNT_DET2);  //DESTINO FONDOS
                Mdl_Funciones_Varias.LC_PMNT_DET3 = VB6Helpers.CStr(data.PMNT_DET3);  //DESTINO FONDOS
                Mdl_Funciones_Varias.LC_PMNT_DET4 = VB6Helpers.CStr(data.PMNT_DET4);  //DESTINO FONDOS
                Mdl_Funciones_Varias.LC_DEBIT_REF = VB6Helpers.CStr(data.DEBIT_REF);  //DESTINO FONDOS
                Mdl_Funciones_Varias.LC_SWFT = VB6Helpers.CStr(data.str_swft);  //ORIGEN FONDOS
                Mdl_Funciones_Varias.LC_BEN_INST1 = VB6Helpers.CStr(data.BEN_INST1);  //BEN_INST1
                Mdl_Funciones_Varias.LC_ULT_BEN1 = VB6Helpers.CStr(data.ULT_BEN1);  //ULT_BEN1
                Mdl_Funciones_Varias.LC_ULT_BEN2 = VB6Helpers.CStr(data.ULT_BEN2);  //ULT_BEN2
                Mdl_Funciones_Varias.LC_ULT_BEN3 = VB6Helpers.CStr(data.ULT_BEN3);  //ULT_BEN3
                Mdl_Funciones_Varias.LC_ULT_BEN4 = VB6Helpers.CStr(data.ULT_BEN4);  //ULT_BEN4
                Mdl_Funciones_Varias.LC_CHG_WHOM = VB6Helpers.CStr(data.CHG_WHOM);  //CHG_WHOM
                Mdl_Funciones_Varias.LC_FCCFT = VB6Helpers.CStr(data.FCCFT);  //FCCFT
                Mdl_Funciones_Varias.LC_DRVALDT = VB6Helpers.CStr(data.DRVALDT);  //DRVALDT
                Mdl_Funciones_Varias.LC_INTRMD1 = VB6Helpers.CStr(data.INTRMD1);  //INTRMD1
                Mdl_Funciones_Varias.LC_INTRMD2 = VB6Helpers.CStr(data.INTRMD2);  //INTRMD2
                Mdl_Funciones_Varias.LC_US_PAY_ID = VB6Helpers.CStr(data.US_PAY_ID);  //LC_US_PAY_ID
                Mdl_Funciones_Varias.LC_RECVR_CORRES1 = VB6Helpers.CStr(data.RECVR_CORRES1);  //RECVR_CORRES1
                Mdl_Funciones_Varias.LC_RECVR_CORRES2 = VB6Helpers.CStr(data.RECVR_CORRES2);  //RECVR_CORRES2
                Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1 = VB6Helpers.CStr(data.SNDR_RECVR_INFO1);  //SNDR_RECVR_INFO1
                Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2 = VB6Helpers.CStr(data.SNDR_RECVR_INFO2);  //SNDR_RECVR_INFO2
                Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3 = VB6Helpers.CStr(data.SNDR_RECVR_INFO3);  //SNDR_RECVR_INFO3
                Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4 = VB6Helpers.CStr(data.SNDR_RECVR_INFO4);  //SNDR_RECVR_INFO4
                Mdl_Funciones_Varias.LC_COD_TRANS = VB6Helpers.CStr(data.trxid);  //CODIGO TRANSACCION ID
                Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5 = VB6Helpers.CStr(data.SNDR_RECVR_INFO5);  //SNDR_RECVR_INFO5
                Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6 = VB6Helpers.CStr(data.SNDR_RECVR_INFO6);  //SNDR_RECVR_INFO6

                initObj.Mdi_Principal.BUTTONS["tbr_participantes"].Enabled = true;
                initObj.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                initObj.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = false;

                Mdl_Funciones_Varias.LC_CONREFNUM = VB6Helpers.CStr(BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Rescata_Contract_Auto(uow, Mdl_Funciones_Varias.LC_COD_TRANS));
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.mdi_PrincipalLogic.NuevaCVD2(initObj, uow, "I");
                    //se setea el foco en principal
                    initObj.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObj.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_participantes" ? true : false); });
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        private static List<pro_sce_prty_s04_MS_Result> consulta_operaciones(InitializationObject initObj, UnitOfWorkCext01 uow, string nroTransaccion, string estado)
        {
            return consulta_operaciones(initObj, uow, nroTransaccion, estado, "0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="estado"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private static List<pro_sce_prty_s04_MS_Result> consulta_operaciones(InitializationObject initObj, UnitOfWorkCext01 uow, string nroTransaccion, string estado, string year)
        {
            var Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
            nroTransaccion = string.IsNullOrEmpty(nroTransaccion) ? null : nroTransaccion;
            estado = string.IsNullOrEmpty(estado) ? "1" : estado;

            if (initObj.Frm_Consulta.items.Where(c=> c.Seleccionado == 1).Count() == 0)
                initObj.Frm_Consulta.items = uow.SceRepository.pro_sce_prty_s04_MS(nroTransaccion, estado);

            int cont = 0;
            if (initObj.Frm_Consulta.items.Count > 0)
            {
                foreach (var item in initObj.Frm_Consulta.items)
                {
                    item.row = cont;
                    cont++;

                    if (item.estado == "1")
                    {
                        item.color = "blue";//VIGENTE
                    }
                    if (item.estado == "2")
                    {
                        item.color = "black";//CURSADA
                    }
                    if (item.estado == "3")
                    {
                        item.color = "red";//ANULADA
                    }


                    if (item.fecingreso.IndexOf('/') == -1) { 
                        string str_fecha = item.fecingreso;
                        item.fecingreso = VB6Helpers.Mid(str_fecha, 7, 2) + "/" + VB6Helpers.Mid(str_fecha, 5, 2) + "/" + VB6Helpers.Mid(str_fecha, 1, 4);
                    }
                    string monto = item.DRAMT;
                    //item.DRAMT = Utils.Format.FormatDblFromDB(monto, "##,###0.00");
                    item.MONTO_ORIGINAL = VB6Helpers.Format(VB6Helpers.CStr(monto), "00000000000000000000");

                    double _switchVar1 = VB6Helpers.Int(item.PRD);
                    if (_switchVar1 == Format.StringToDouble("72"))
                    {
                        item.TIPO_TRX = "Outgoing 103";
                    }
                    else if (_switchVar1 == Format.StringToDouble("74"))
                    {
                        item.TIPO_TRX = "Outgoing 202";
                    }
                    else if (_switchVar1 == Format.StringToDouble("62"))
                    {
                        item.TIPO_TRX = "Incoming";
                    }

                    if (item.TIPO_TRX == "Outgoing 103" || item.TIPO_TRX == "Outgoing 202")
                    {
                        item.Beneficiario = item.CRACC;
                        item.Ordenante = item.DRACC;
                    }
                    else if (item.TIPO_TRX == "Incoming")
                    {
                        item.Beneficiario = item.DRACC;
                        item.Ordenante = item.CRACC;
                    }
                    item.str_cod_estado = item.estado;

                    if (item.TIPO_TRX == "Outgoing 103")
                    {
                        Mdl_Funciones_Varias.LC_PRD = "72";
                    }
                    else if (item.TIPO_TRX == "Outgoing 202")
                    {
                        Mdl_Funciones_Varias.LC_PRD = "74";
                    }
                    else if (item.TIPO_TRX == "Incoming")
                    {
                        Mdl_Funciones_Varias.LC_PRD = "62";
                    }

                    Mdl_Funciones_Varias.LC_INCOMING = VB6Helpers.CStr(item.Beneficiario);  //072-074 OUTGOING CRACC
                    Mdl_Funciones_Varias.LC_OUTGOING = VB6Helpers.CStr(item.Ordenante);  //062 INCOMING     DRACC

                    if (VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) == Format.StringToDouble("72") || VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) == Format.StringToDouble("74"))
                    {
                        Mdl_Funciones_Varias.Lc_BaseNumber = Mdl_Funciones_Varias.LC_OUTGOING;  //DRACC
                    }

                    if (VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) == Format.StringToDouble("62"))
                    {
                        Mdl_Funciones_Varias.Lc_BaseNumber = Mdl_Funciones_Varias.LC_INCOMING;  //CRACC
                    }

                    //LC_BASENUMBER TIENE LARGO 12
                    //HAY QUE DEJARLO DE LARGO 6
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.Lc_BaseNumber) == 10)
                    {
                        Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO = VB6Helpers.Mid(Mdl_Funciones_Varias.Lc_BaseNumber, 1, 6);
                    }
                    else if (VB6Helpers.Len(Mdl_Funciones_Varias.Lc_BaseNumber) == 9)
                    {
                        Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO = VB6Helpers.Mid(Mdl_Funciones_Varias.Lc_BaseNumber, 1, 6);
                    }
                    item.razonSocial = VerPartySy(initObj, uow, 2, Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO);
                }
            }
            else {

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                      Type = TipoMensaje.Informacion,
                      Text = "No existen operaciones para la consulta seleccionada.",
                      Title = "Carga Operaciones"
                });
                return initObj.Frm_Consulta.items;
            }
            return initObj.Frm_Consulta.items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="opcion"></param>
        /// <param name="rut"></param>
        /// <returns></returns>
        public static string VerPartySy(InitializationObject initObj, UnitOfWorkCext01 uow, short opcion, string rut)
        {
            string razon = null;
            var result = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_2_Result>("pro_sce_prty_s02_MS", MODGSYB.dbcharSy(rut + VB6Helpers.String(12 - VB6Helpers.Len(rut), 126)), "2").FirstOrDefault();

            if (result != null) razon = result.razon_soci;
            return razon;
        }
    }

    public static class PagingExtensions
    {
        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

    }
}
