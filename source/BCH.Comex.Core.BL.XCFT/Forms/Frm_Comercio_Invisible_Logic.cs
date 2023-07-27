using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_Comercio_Invisible_Logic
    {
        #region METODOS PRIVADOS

        //****************************************************************************
        //   1.  Carga los datos del Ide.
        //****************************************************************************
        private static short Fn_CargaIde(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, UnitOfWorkCext01 unit)
        {
            string Nd = "";
            string FD = "";
            short CA = 0;
            short x = 0;
            Mdl_Funciones_Varias.VxDec = Mdl_Funciones_Varias.VxDecVacia;
            if (!string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NroDec.Text) && !string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecDec.Text) && !string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_CodAdn.Text))
            {
                Nd = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_NroDec.Text);
                FD = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_FecDec.Text);
                CA = (short)VB6Helpers.Val(Frm_Comercio_Invisible.Tx_CodAdn.Text);
                Nd = MODGPYF1.Zeros((short)(7 - VB6Helpers.Len(Nd))) + Nd;
                x = Mdl_Funciones.SyGet_xDec(Mdl_Funciones_Varias, unit, Nd, FD, CA);
                if (x == 0)
                {
                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de leer la Declaración de Exportación",
                        Type = TipoMensaje.Error
                    });
                }
            }

            return 0;
        }

        //****************************************************************************
        //   1.  Retorna una linea para la lista de Operaciones.
        //****************************************************************************
        private static string[] Fn_Linea_Lt_Oper(T_MODGCVD MODGCVD, short Indice)
        {
            string[] s = new string[3];
            string Paso = "";
            string ls_oper = "";
            string ls_moneda = "";
            string ls_monto = "";
            short ls_largo_oper = 0;
            short ls_largo_mda = 0;
            string _switchVar1 = MODGCVD.VgPli[Indice].TipCVD;
            if (_switchVar1 == "C")
            {
                Paso = "Compra";
            }
            else if (_switchVar1 == "V")
            {
                Paso = "Ventas";
            }
            else if (_switchVar1 == "TI")
            {
                Paso = "Transf. ingreso";
            }
            else if (_switchVar1 == "TE")
            {
                Paso = "Transf. egreso";
            }
            else if (_switchVar1 == "TIN")
            {
                Paso = "Transf. interna";
            }

            ls_largo_oper = (short)VB6Helpers.Len(Paso);
            ls_oper = VB6Helpers.Trim(Paso) + Mdl_Funciones_Varias.llena_blancos_der(" ", (short)(25 - ls_largo_oper));

            ls_largo_mda = (short)VB6Helpers.Len(VB6Helpers.LTrim(MODGCVD.VgPli[Indice].NemMnd));
            ls_moneda = VB6Helpers.LTrim(MODGCVD.VgPli[Indice].NemMnd) + Mdl_Funciones_Varias.llena_blancos_der(" ", (short)(25 - ls_largo_mda));

            ls_monto = Format.FormatCurrency(MODGCVD.VgPli[Indice].MtoCVD, "##,###0.00");

            s[0] = ls_oper;
            s[1] = ls_moneda;
            s[2] = ls_monto;
            return s;
        }

        /// <summary>
        /// Carga los Datos desde la Estructura de VgPli() hacia los campos en Pantalla
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        /// <param name="Indice_Oper"></param>
        private static void Pr_Cargar_Datos_Operacion(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice_Oper)
        {
            string Paso = "";
            short n = 0;
            short Posicion = 0;

            var VgPli = initObject.MODGCVD.VgPli[Indice_Oper];
            string _switchVar1 = VB6Helpers.Trim(VgPli.TipCVD);

            if (_switchVar1 == "C")
            {
                Paso = "Compras";
            }
            else if (_switchVar1 == "V")
            {
                Paso = "Ventas";
            }
            else if (_switchVar1 == "TI")
            {
                Paso = "Transf. ingreso";
            }
            else if (_switchVar1 == "TE")
            {
                Paso = "Transf. egreso";
            }
            else if (_switchVar1 == "TIN")
            {
                Paso = "Transf. interna";
            }

            //SE CAMBIO LA FUNCION PARA QUE USE LINQ
            initObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex = VB6Helpers.CInt(initObject.Frm_Comercio_Invisible.Cb_Divisa.Items.FindIndex(x => x.Value.Equals(Paso)));
            Cb_Divisa_click(initObject, unit);
            initObject.Frm_Comercio_Invisible.Cb_Pais.ListIndex = initObject.Frm_Comercio_Invisible.Cb_Pais.Items.FindIndex(x => x.ID.Equals(VgPli.codpai.ToString()));
            initObject.Frm_Comercio_Invisible.Cb_Moneda.ListIndex = initObject.Frm_Comercio_Invisible.Cb_Moneda.Items.FindIndex(x => x.ID.Equals(VgPli.CodMnd.ToString()));
            Cb_Moneda_Click(initObject, unit);

            initObject.Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Format.FormatCurrency(VgPli.MtoCVD, MODGPYF1.DecObjeto(initObject.Frm_Comercio_Invisible.Tx_MtoCV[0]));
            initObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(VgPli.TipCam, MODGPYF1.DecObjeto(initObject.Frm_Comercio_Invisible.Tx_MtoCV[1]));
            initObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Text = Format.FormatCurrency(VgPli.MtoPes, MODGPYF1.FmtObjeto(initObject.Frm_Comercio_Invisible.Tx_MtoCV[2]));
            initObject.Frm_Comercio_Invisible.Tx_MtoCV[3].Text = Format.FormatCurrency(VgPli.Mtopar, MODGPYF1.DecObjeto(initObject.Frm_Comercio_Invisible.Tx_MtoCV[3]));

            //SE CAMBIO LO FUNCION PARA QUE USE LINQ
            n = (short)initObject.Frm_Comercio_Invisible.Lt_Tcp.Items.FindIndex(x => (initObject.ModChVrf.VCcpl[x.Data].CodCom + initObject.ModChVrf.VCcpl[x.Data].CptCom).Equals(initObject.MODGCVD.VgPli[Indice_Oper].CodTcp.ToString()));
            if (n == -1)
            {
                n = 0;
            }
            initObject.Frm_Comercio_Invisible.Lt_Tcp.ListIndex = n;
            Lt_Tcp_Click(initObject, unit);

            if (VgPli.Conven == 0)
            {
                initObject.Frm_Comercio_Invisible.Ch_Convenio.Value = 0;
            }
            else
            {
                initObject.Frm_Comercio_Invisible.Ch_Convenio.Value = -1;
            }

            if (VgPli.afeder == 0)
            {
                initObject.Frm_Comercio_Invisible.ch_AfDer.Value = 0;
            }
            else
            {
                initObject.Frm_Comercio_Invisible.ch_AfDer.Value = -1;
            }

            if (VgPli.ZonFra == 0)
            {
                initObject.Frm_Comercio_Invisible.ch_ZoFra.Value = 0;
            }
            else
            {
                initObject.Frm_Comercio_Invisible.ch_ZoFra.Value = -1;
            }

            //Carga los datos de la Declaración.
            if (initObject.Frm_Comercio_Invisible.Fr_Declaracion.Enabled == true)
            {
                initObject.Frm_Comercio_Invisible.Tx_NroDec.Text = VB6Helpers.Trim(VgPli.numdec);
                initObject.Frm_Comercio_Invisible.Tx_FecDec.Text = VB6Helpers.Trim(VgPli.FecDec);
                if (VgPli.CodAdn == 0)
                {
                    initObject.Frm_Comercio_Invisible.Tx_CodAdn.Text = "";
                }
                else
                {
                    initObject.Frm_Comercio_Invisible.Tx_CodAdn.Text = VB6Helpers.Trim(VB6Helpers.Str(VgPli.CodAdn));
                }
            }

            //Carga los datos de Convenio Crédito REcíproco.
            if (initObject.Frm_Comercio_Invisible.Fr_Convenio.Enabled == true)
            {
                initObject.Frm_Comercio_Invisible.Tx_FecDeb.Text = (VgPli.FecDeb ?? string.Empty).Trim();
                initObject.Frm_Comercio_Invisible.Tx_DocNac.Text = (VgPli.DocNac ?? string.Empty).Trim();
                initObject.Frm_Comercio_Invisible.Tx_DocExt.Text = (VgPli.DocExt ?? string.Empty).Trim();
                initObject.Frm_Comercio_Invisible.Tx_ER.Text = (VgPli.CodEOR ?? string.Empty).Trim();
            }

            //Carga Datos Autorización BC
            Posicion = -1;
            if (initObject.Frm_Comercio_Invisible.Fr_Autori.Enabled == true)
            {
                //SE CAMBIO LO FUNCION PARA QUE USE LINQ
                if (!string.IsNullOrEmpty(initObject.MODGCVD.VgPli[Indice_Oper].ApcTip ?? ""))
                {
                    Posicion = (short)initObject.Frm_Comercio_Invisible.Cb_TipAut.Items.FindIndex(x => x.Value.Substring(0, 2) == (initObject.MODGCVD.VgPli[Indice_Oper].ApcTip ?? "").Trim());
                }
                else
                {
                    Posicion = (short)(initObject.Frm_Comercio_Invisible.Cb_TipAut.Items.Count() - 1);
                }
                initObject.Frm_Comercio_Invisible.Cb_TipAut.ListIndex = Posicion;
                initObject.Frm_Comercio_Invisible.Tx_NroAut.Text = VgPli.ApcNum;
                initObject.Frm_Comercio_Invisible.Tx_FecAut.Text = VgPli.ApcFec;
                initObject.Frm_Comercio_Invisible.Tx_SucBcch.Text = VB6Helpers.CStr(VgPli.ApcPbc);
            }

            //Carga Datos de Operaciones Relacionadas
            if (initObject.Frm_Comercio_Invisible.Fr_OpeD.Enabled)
            {
                initObject.Frm_Comercio_Invisible.Tx_NumCon.Text = VB6Helpers.Str(VgPli.NumCon);
                initObject.Frm_Comercio_Invisible.Tx_FecSus.Text = VB6Helpers.Trim(VgPli.fecsus);
                initObject.Frm_Comercio_Invisible.Tx_FecVen.Text = VB6Helpers.Trim(VgPli.VenOd);

                //SE CAMBIO LO FUNCION PARA QUE USE LINQ
                initObject.Frm_Comercio_Invisible.Cb_InsUt.ListIndex = initObject.Frm_Comercio_Invisible.Cb_InsUt.Items.FindIndex(x => x.ID.Equals(VgPli.insuti.ToString()));

                initObject.Frm_Comercio_Invisible.Tx_ParTip.Text = VB6Helpers.Str(VgPli.partip);

                //SE CAMBIO LO FUNCION PARA QUE USE LINQ
                initObject.Frm_Comercio_Invisible.Cb_ArCon.ListIndex = initObject.Frm_Comercio_Invisible.Cb_ArCon.Items.FindIndex(x => x.ID.Equals(VgPli.arecon.ToString()));

            }

            //Carga Datos de Operación Relacionada
            if (initObject.Frm_Comercio_Invisible.Fr_OpRe.Enabled)
            {
                initObject.Frm_Comercio_Invisible.Tx_FecPre.Text = VB6Helpers.Trim(VgPli.AnuFec);
                initObject.Frm_Comercio_Invisible.Tx_NumPre.Text = VB6Helpers.Str(VgPli.AnuNum);
                initObject.Frm_Comercio_Invisible.Tx_CodIns.Text = VB6Helpers.Str(VgPli.AnuPbc);
            }

            //Carga Datos de Operaciones Financieras Internacionales
            if (initObject.Frm_Comercio_Invisible.Fr_OFI.Enabled)
            {
                initObject.Frm_Comercio_Invisible.Tx_NumIns.Text = VB6Helpers.Str(VgPli.NumCre);
                initObject.Frm_Comercio_Invisible.Tx_FecIns.Text = VB6Helpers.Trim(VgPli.fecins);
                initObject.Frm_Comercio_Invisible.Tx_NomFin.Text = VB6Helpers.Trim(VgPli.NomFin);
                initObject.Frm_Comercio_Invisible.Tx_FecVC.Text = VB6Helpers.Trim(VgPli.VenOfi);
                initObject.Frm_Comercio_Invisible.Tx_Fecha.Text = VB6Helpers.Trim(VgPli.FecCre);
                //SE CAMBIO LO FUNCION PARA QUE USE LINQ
                initObject.Frm_Comercio_Invisible.Cb_MonDes.ListIndex = initObject.Frm_Comercio_Invisible.Cb_MonDes.Items.FindIndex(x => x.ID.Equals(VgPli.MndCre.ToString()));
                initObject.Frm_Comercio_Invisible.Tx_Mto.Text = MODGSYB.dbmontoSyForRead(VgPli.MtoCre);
            }

            if (initObject.Frm_Comercio_Invisible.Fr_Sec.Enabled)
            {
                //SE CAMBIO LO FUNCION PARA QUE USE LINQ
                initObject.Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex = initObject.Frm_Comercio_Invisible.Cb_SecEcBen.Items.FindIndex(x => x.ID.Equals(VgPli.SecBen.ToString()));
                //SE CAMBIO LO FUNCION PARA QUE USE LINQ
                initObject.Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex = initObject.Frm_Comercio_Invisible.Cb_SecEcIn.Items.FindIndex(x => x.ID.Equals(VgPli.SecInv.ToString()));
                initObject.Frm_Comercio_Invisible.Tx_PrcPar.Text = MODGSYB.dbPardSyForRead(VgPli.PrcPar);
            }
        }

        /// <summary>
        /// rutina que borra de pantalla datos que no se ocupan en otra operación
        /// </summary>
        /// <param name="Frm_Comercio_Invisible"></param>
        private static void Pr_Descarga(UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible)
        {

            Frm_Comercio_Invisible.Cb_Pais.ListIndex = -1;
            Frm_Comercio_Invisible.Ch_Convenio.Value = 0;
            Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[0]));  //Format(Tx_MtoCV(0), "##,###0.00") '
            Frm_Comercio_Invisible.Tx_MtoCV[2].Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[2]));  //Format(Tx_MtoCV(2), "##,###0")  '

            Frm_Comercio_Invisible.Tx_NroDec.Text = "";
            Frm_Comercio_Invisible.Tx_FecDec.Text = "";
            Frm_Comercio_Invisible.Tx_CodAdn.Text = "";
            Frm_Comercio_Invisible.Tx_ER.Text = "E";
            Frm_Comercio_Invisible.Tx_FecDeb.Text = "";
            Frm_Comercio_Invisible.Tx_DocNac.Text = "";
            Frm_Comercio_Invisible.Tx_DocExt.Text = "";

            Frm_Comercio_Invisible.Cb_TipAut.ListIndex = -1;
            Frm_Comercio_Invisible.Tx_NroAut.Text = "";
            Frm_Comercio_Invisible.Tx_FecAut.Text = "";
            Frm_Comercio_Invisible.Tx_SucBcch.Text = "";
        }

        /// <summary>
        /// Carga los Tipos de Divisas existentes dejando la primera en Compra.
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        private static void Pr_Cargar_Divisa(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;
            Frm_Comercio_Invisible.Cb_Divisa.Items.Clear();

            Frm_Comercio_Invisible.Cb_Divisa.Items.Add(new UI_ComboItem() { ID = "0", Value = "Compras", Data = 0 });
            Frm_Comercio_Invisible.Cb_Divisa.Items.Add(new UI_ComboItem() { ID = "1", Value = "Ventas", Data = 1 });
            Frm_Comercio_Invisible.Cb_Divisa.Items.Add(new UI_ComboItem() { ID = "2", Value = "Transf. ingreso", Data = 2 });
            Frm_Comercio_Invisible.Cb_Divisa.Items.Add(new UI_ComboItem() { ID = "3", Value = "Transf. egreso", Data = 3 });
            Frm_Comercio_Invisible.Cb_Divisa.Items.Add(new UI_ComboItem() { ID = "4", Value = "Transf. interna", Data = 4 });
            Frm_Comercio_Invisible.Cb_Divisa.ListIndex = 0;
            Cb_Divisa_click(initObject, unit);
        }

        /// <summary>
        /// Llena la lista Lt_Operacion.
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        private static void Pr_Llena_Lt_Operacion(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            short n = 0;

            n = (short)VB6Helpers.UBound(MODGCVD.VgPli);
            short s = 0;
            initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.Clear();
            //SE CAMBIA YA QUE AL ELIMINAR UN ELEMENTO DE DEL MEDIO DE LA LISTA, LO VUELVE A INCLUIR AL NO RESPETAR EL ID
            //initObject.Frm_Comercio_Invisible.Lt_Operacion.Items = MODGCVD.VgPli.Where(x => x.Status != T_MODGCVD.EstadoEli).Select(x =>
            //{
            //    var g = Fn_Linea_Lt_Oper(x);
            //    g.ID = s.ToString();
            //    s++;
            //    return g;
            //}
            //).ToList();
            for (int i = 0; i <= n; i++)
            {
                if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                {
                    var g = Fn_Linea_Lt_Oper(MODGCVD.VgPli[i]);
                    g.ID = i.ToString();
                    initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.Add(g);
                }
            }
            initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex = -1;
            Lt_Operacion_Click(initObject, unit);
        }

        /// <summary>
        /// Retorna una linea para la lista de Operaciones.
        /// </summary>
        /// <param name="TVgPli"></param>
        /// <returns></returns>
        private static UI_GridItem Fn_Linea_Lt_Oper(T_gPli TVgPli)
        {
            string s = "";
            string Paso = "";
            string ls_oper = "";
            string ls_moneda = "";
            string ls_monto = "";
            short ls_largo_oper = 0;
            short ls_largo_mda = 0;
            string _switchVar1 = TVgPli.TipCVD;
            if (_switchVar1 == "C")
            {
                Paso = "Compra";
            }
            else if (_switchVar1 == "V")
            {
                Paso = "Ventas";
            }
            else if (_switchVar1 == "TI")
            {
                Paso = "Transf. ingreso";
            }
            else if (_switchVar1 == "TE")
            {
                Paso = "Transf. egreso";
            }
            else if (_switchVar1 == "TIN")
            {
                Paso = "Transf. interna";
            }
            UI_GridItem item = new UI_GridItem();
            item.AddColumn("tipo", Paso);
            item.AddColumn("moneda", TVgPli.NemMnd);
            item.AddColumn("monto", Format.FormatCurrency(TVgPli.MtoCVD, "##,###0.00"));

            return item;
        }

        //Instrumentos Utilizados
        public static void CargaEnLista_Instru(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UI_Combo Lista)
        {
            Lista.Items = Mdl_Funciones_Varias.V_Instru.Select(x => new UI_ComboItem()
            {
                Value = x.CodIntr + ":" + x.DesIntr,
                ID = x.CodIntr,
                Data = Convert.ToInt32(x.CodIntr)
            }).ToList();
        }

        //Areas Contables
        public static void CargaEnLista_AreCon(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UI_Combo Lista)
        {
            Lista.Items = Mdl_Funciones_Varias.V_AreCon.Select(x => new UI_ComboItem()
            {
                Value = x.CodACon + ":" + x.DesACon,
                ID = x.CodACon,
                Data = Convert.ToInt32(x.CodACon)
            }).ToList();
        }

        public static dynamic Pr_CargaAUTOMATICA(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;

            short j = 0;

            //MONEDA ORIGEN
            if (Mdl_Funciones_Varias.LC_MONEDA != 0)
            {
                for (j = 0; j <= (short)(Frm_Comercio_Invisible.Cb_Moneda.Items.Count - 1); j++)
                {
                    if (Frm_Comercio_Invisible.Cb_Moneda.get_ItemData_(j) == Mdl_Funciones_Varias.LC_MONEDA)
                    {
                        Frm_Comercio_Invisible.Cb_Moneda.ListIndex = j;
                        Cb_Moneda_Click(initObject, unit);
                        Mdl_Funciones_Varias.LC_NOM_MDA = Frm_Comercio_Invisible.Cb_Moneda.Text;  //NOMBRE MONEDA
                        break;
                    }

                }

            }

            //MONTO DE LA OPERACION
            Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Mdl_Funciones_Varias.LC_MONTO;
            return null;
        }

        // UPGRADE_INFO (#0561): The 'Solo_moneda' symbol was defined without an explicit "As" clause.
        private static dynamic Solo_moneda(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;

            short T = (short)(short.Parse(Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID) + 1);
            if (T != Frm_Comercio_Invisible.TipDiv)
            {
                Frm_Comercio_Invisible.TipDiv = T;
                MODGCVD.CargaEnListaTcp(ModChVrf, Frm_Comercio_Invisible.Lt_Tcp, T);
                if (Frm_Comercio_Invisible.Lt_Tcp.Items.Count > 0)
                {
                    Frm_Comercio_Invisible.Lt_Tcp.ListIndex = 0;
                    Lt_Tcp_Click(initObject, unit);
                }
            }

            if (Frm_Comercio_Invisible.Cb_Moneda.ListIndex != -1)
            {
                string v = Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID;
                if (v.Equals("0"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbc, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                    //            Tx_MtoCV(3).Text = Format$(VVmd.VmdPrd, DecObjeto(Tx_MtoCV(3)))
                }
                else if (v.Equals("1"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbv, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                    //            Tx_MtoCV(3).Text = Format$(VVmd.VmdPrd, DecObjeto(Tx_MtoCV(3)))
                }
                else if (v.Equals("2"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbc, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                    //            Tx_MtoCV(3).Text = Format$(VVmd.VmdPrd, DecObjeto(Tx_MtoCV(3)))
                }
                else if (v.Equals("3"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbv, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                    //            Tx_MtoCV(3).Text = Format$(VVmd.VmdPrd, DecObjeto(Tx_MtoCV(3)))
                }
                else if (v.Equals("4"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbv, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                    //            Tx_MtoCV(3).Text = Format$(VVmd.VmdPrd, DecObjeto(Tx_MtoCV(3)))
                    return null;
                }

                short _tempVar1 = 0;
                Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref _tempVar1);
            }

            return null;
        }

        /// <summary>
        /// Valida los campos referentes a la Compra Venta.
        /// </summary>
        /// <param name="MODGPYF0"></param>
        /// <param name="ModChVrf"></param>
        /// <param name="Mdi_Principal"></param>
        /// <param name="Frm_Comercio_Invisible"></param>
        /// <param name="CampoInicial"></param>
        /// <param name="CampoFinal"></param>
        /// <returns></returns>
        private static short Fn_Validar_Campos(T_MODGPYF0 MODGPYF0, T_ModChVrf ModChVrf, UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, short CampoInicial, short CampoFinal)
        {
            using (var trace = new Tracer("Fn_Validar_Campos"))
            {
                trace.AddToContext("Fn_Validar_Campos", "Valida los campos referentes a la Compra Venta.");
                short HayDec = 0, m = 0, HayIde = 0, i = 0;
                string Con = string.Empty, Paso = string.Empty, Msg = string.Empty;

                /// 
                /// Valida Declaración e Informes.
                /// 
                if (Frm_Comercio_Invisible.Fr_Declaracion.Enabled)
                {
                    if (!string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_NroDec.Text) || !string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_FecDec.Text) || !string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_CodAdn.Text))
                    {
                        HayDec = (short)(true ? -1 : 0);
                    }

                }

                m = short.Parse(Frm_Comercio_Invisible.Lt_Tcp.Items.ElementAt(Frm_Comercio_Invisible.Lt_Tcp.ListIndex).ID);
                if (ModChVrf.VCcpl[m].dataut == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_TipAut.Value == -1 && Frm_Comercio_Invisible.Cb_TipAut.Visible == true)
                    {
                        Msg = "Atención: Falta selecionar el tipo de autorización.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_TipAut"
                        });
                        return 0;
                    }

                    if (Frm_Comercio_Invisible.Cb_TipAut.Visible == true)
                    {
                        if (Frm_Comercio_Invisible.Cb_TipAut.Value != 6)
                        {
                            if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_NroAut.Text))
                            {
                                Msg = "Atención: Falta el Número de la Autorización del Banco Central.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Tx_NroAut"
                                });
                                return 0;
                            }

                            if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecAut.Text))
                            {
                                Msg = "Atención: Falta la Fecha de la Autorización del Banco Central.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Tx_FecAut"
                                });
                                return 0;
                            }

                            if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_SucBcch.Text))
                            {
                                Msg = "Atención: Falta Ingresar el código de la sucursal del Banco Central.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Tx_SucBcch"
                                });
                                return 0;
                            }

                        }

                    }

                }

                ///
                /// No hay datos de Declaración ni de Informe.
                /// 
                if (((Frm_Comercio_Invisible.Fr_Declaracion.Enabled ? -1 : 0) & (~HayDec & ~HayIde)) != 0)
                {
                    Msg = "Debe ingresar datos de la Declaración.";
                    trace.TraceInformation(Msg);
                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = Msg
                    });
                    return 0;
                }

                /// 
                /// Hay datos inconclusos de Declaraciones.
                /// 
                if (ModChVrf.VCcpl[m].decexp == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NroDec.Text))
                    {
                        Msg = "Atención: Falta Ingresar el Número de la Declaración.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NroDec"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecDec.Text))
                    {
                        Msg = "Atención: Falta Ingresar la Fecha de la Declaración de Exportación.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecDec"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_CodAdn.Text))
                    {
                        Msg = "Atención: Falta Ingresar el Código de la Aduana.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_CodAdn"
                        });
                        return 0;
                    }

                }

                ///
                /// Hay datos inconclusos de Declaraciones.
                /// Valida el resto de los campos.
                /// 
                for (i = (short)CampoInicial; i <= (short)CampoFinal; i++)
                {
                    switch (i)
                    {

                        case 1:  //Cb_Pais
                            Con = VB6Helpers.Mid(Frm_Comercio_Invisible.Lt_Tcp.Text, 1, 6);
                            if (VB6Helpers.Instr(UI_Frm_Comercio_Invisibles.Concepai, Con) == 0)
                            {
                                if (Frm_Comercio_Invisible.Cb_Pais.Enabled && Frm_Comercio_Invisible.Cb_Pais.ListIndex == -1)
                                {
                                    Msg = "Atención: Falta Seleccionar un País.";
                                    trace.TraceInformation(Msg);
                                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = Msg,
                                        ControlName = "Cb_Pais"
                                    });
                                    return 0;
                                }

                            }

                            break;
                        case 2:  //Tx_MtoCV(0).text
                            if (Format.StringToDouble((Frm_Comercio_Invisible.Tx_MtoCV[0].Text)) == 0)
                            {
                                Paso = VB6Helpers.Trim(Frm_Comercio_Invisible.Cb_Divisa.Text) + ".";
                                Msg = "Atención: Falta Ingresar el Monto de " + Paso;
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "monto"
                                });
                                return 0;
                            }

                            break;
                        case 3:  //Tx_MtoCV(1).text
                            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0") || Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                            {
                                if (Format.StringToDouble((Frm_Comercio_Invisible.Tx_MtoCV[1].Text)) == 0)
                                {
                                    Msg = "Atención: Falta Ingresar el Tipo de Cambio " + Paso;
                                    trace.TraceInformation(Msg);
                                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = Msg,
                                        ControlName = "tipoCambio"
                                    });
                                    return 0;
                                }

                            }

                            break;
                        case 4:  //Tx_MtoCV(2).text
                            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0") || Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                            {
                                if (Format.StringToDouble((Frm_Comercio_Invisible.Tx_MtoCV[2].Text)) == 0)
                                {
                                    Msg = "Atención: Falta Calcular la Equivalencia en $." + Paso;
                                    trace.TraceInformation(Msg);
                                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = Msg,
                                        ControlName = "montoPesos"
                                    });
                                    return 0;
                                }

                            }

                            break;
                        case 5:  //Lt_Tcp
                            if (Frm_Comercio_Invisible.Lt_Tcp.ListIndex == -1)
                            {
                                Msg = "Atención: Falta Seleccionar un Concepto de la Planilla.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Lt_Tcp"
                                });
                                return 0;
                            }

                            break;
                        case 6:
                            if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_MtoCV[3].Text))
                            {
                                Msg = "Atención: Falta ingresar la paridad";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "paridad"
                                });
                                Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = true;
                                return 0;
                            }

                            break;
                    }

                }

                /// 
                /// Valida que haya seleccionado alguna moneda.-
                /// 
                if (Frm_Comercio_Invisible.Cb_Moneda.ListIndex == -1)
                {
                    Msg = "Atención: Debe Seleccionar la moneda asociada a la Operación.";
                    trace.TraceInformation(Msg);
                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = Msg,
                        ControlName = "Cb_Moneda"
                    });
                    return 0;
                }

                if (ModChVrf.VCcpl[m].operel == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecPre.Text))
                    {
                        Msg = "Atención: Falta Ingresar la fecha de presentación de la operación relacionada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecPre"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NumPre.Text))
                    {
                        Msg = "Atención: Falta Ingresar el numero de presentación de la operación relacionada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NumPre"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_CodIns.Text))
                    {
                        Msg = "Atención: Falta Ingresar el Código de Institución de la operación relacionada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_CodIns"
                        });
                        return 0;
                    }

                }

                /// 
                /// Operaciones Financieras Internacionales
                /// 
                if (ModChVrf.VCcpl[m].numins == -1 || ModChVrf.VCcpl[m].vtocic == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NumIns.Text))
                    {
                        Msg = "Atención: Falta Ingresar el numero de inscripción de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NumIns"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].fecins == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecIns.Text))
                    {
                        Msg = "Atención: Falta Ingresar la fecha inscripción de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecIns"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].finext == -1 || ModChVrf.VCcpl[m].vtocic == -1)
                {

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NomFin.Text))
                    {
                        Msg = "Atención: Falta Ingresar el nombre del financista de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NomFin"
                        });
                        return 0;
                    }

                    if (!(Regex.Match(Frm_Comercio_Invisible.Tx_NomFin.Text, @"^[a-zA-Z\s]{8,}$")).Success)
                    {
                        Msg = "Atención: El nombre del financista de la operación financiera internacional, no cumple con el criterio de mayor de 8 caracteres o no contener número o ningún carácter especial.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NomFin"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].vtocic == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecVC.Text))
                    {
                        Msg = "Atención: Falta Ingresar la fecha de vencimiento de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecVC"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].fecdes == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_Fecha.Text))
                    {
                        Msg = "Atención: Falta Ingresar la fecha de desembolso de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_Fecha"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].mondes == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_MonDes.ListIndex == -1)
                    {
                        Msg = "Atención: Falta seleccionar la moneda de desembolso de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_MonDes"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].mtodes == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_Mto.Text))
                    {
                        Msg = "Atención: Falta Ingresar el monto equivalente de la operación financiera internacional.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_Mto"
                        });
                        return 0;
                    }

                }

                /// 
                /// Sectores
                /// 
                if (ModChVrf.VCcpl[m].infimp == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex == -1)
                    {
                        Msg = "Atención: Falta ingresar el Sector Económico del Beneficiario.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_SecEcBen"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].infexp == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex == -1)
                    {
                        Msg = "Atención: Falta ingresar el Sector Económico del Inversionista.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_SecEcIn"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].datint == -1)
                {
                    if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_PrcPar.Text))
                    {
                        Msg = "Atención: Falta ingresar el Porcentaje de Participación.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_PrcPar"
                        });
                        return 0;
                    }

                }

                if (ModChVrf.VCcpl[m].datder == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecSus.Text))
                    {
                        Msg = "Atención: Falta Ingresar la fecha de suscripción de la operación derivada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecSus"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NumCon.Text))
                    {
                        Msg = "Atención: Falta Ingresar el numero de contrato de la operación derivada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NumCon"
                        });
                        return 0;
                    }

                    if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0") || Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                    {
                        if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_ParTip.Text))
                        {
                            Msg = "Atención: Falta Ingresar la paridad/tipo cambio de la operación derivada.";
                            trace.TraceInformation(Msg);
                            Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = Msg,
                                ControlName = "Tx_ParTip"
                            });
                            return 0;
                        }

                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecVen.Text))
                    {
                        Msg = "Atención: Falta Ingresar la fecha de vencimiento de la operación derivada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecVen"
                        });
                        return 0;
                    }

                    if (Frm_Comercio_Invisible.Cb_InsUt.ListIndex == -1)
                    {
                        Msg = "Atención: Falta seleccionar el instrumento de utilización de la operación derivada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_InsUt"
                        });
                        return 0;
                    }

                    if (Frm_Comercio_Invisible.Cb_ArCon.ListIndex == -1)
                    {
                        Msg = "Atención: Falta seleccionar el área de contrato de la operación derivada.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_ArCon"
                        });
                        return 0;
                    }
                }
                return 1;
            }
        }

        private static short EsRut(string rut)
        {
            short _retValue = 0;
            short i = 0;
            string a = "";
            string b = "";
            string DvRut = "";
            short aa = 0;
            short suma = 0;
            short es = 0;
            string DvCal = "";
            const string Son = "1234567890K";

            //limpiar el Rut
            for (i = 1; i <= (short)VB6Helpers.Len(rut); i++)
            {
                a = VB6Helpers.Mid(rut, i, 1);
                if (a == "k")
                {
                    a = "K";
                }
                if (VB6Helpers.Instr(1, Son, a) != 0)
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'b' variable as a StringBuilder6 object.
                    b += a;
                }
            }

            DvRut = VB6Helpers.Right(b, 1);
            b = VB6Helpers.Left(b, VB6Helpers.Len(b) - 1);

            for (i = 1; i <= (short)VB6Helpers.Len(b); i++)
            {
                a = VB6Helpers.Right(b, i);
                aa = (short)VB6Helpers.Val(VB6Helpers.Left(a, 1));

                if (i < 7)
                {
                    suma = (short)(suma + aa * (i + 1));
                }
                else
                {
                    suma = (short)(suma + aa * (i - 5));
                }

            }

            es = (short)(11 - (suma % 11));
            switch (es)
            {
                case 11:
                    DvCal = "0";
                    break;
                case 10:
                    DvCal = "K";
                    break;
                default:
                    DvCal = es.ToString();
                    break;
            }

            _retValue = 0;
            if (DvCal == DvRut)
            {
                return -1;
            }

            return _retValue;
        }

        //****************************************************************************
        //   1.  Busca los datos de la Declaración y valida la información existente.
        //****************************************************************************
        private static short Fn_Declaracion(T_ModChVrf ModChVrf, T_MODGCVD MODGCVD, T_MODGTAB0 MODGTAB0, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            double Mtopar = 0;
            short PrtyOK = 0;
            string p = "";
            string PrtExp1 = "";
            string PrtExp2 = "";
            short j = 0;
            string Var = "";

            _retValue = 0;

            if ((~Frm_Comercio_Invisible.IndDec | (string.IsNullOrEmpty(Mdl_Funciones_Varias.VxDec.numdec) ? -1 : 0)) != 0)
            {
                return 1;
            }

            //Paridad mensual (Sgt_Vmc).-
            Mtopar = MODGTAB1.SyGet_Vmc(MODGTAB0, unit, short.Parse(Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt(Frm_Comercio_Invisible.Cb_MonDes.ListIndex).ID), DateTime.Now.ToString("yyyy-MM-dd"), "P");
            if (Mtopar == 0)
            {
                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema."
                });
                return _retValue;
            }

            //Cuando Exista la Declaración => Validar que los Partys concuerden.
            PrtyOK = 0;
            p = MODGCVD.VgCvd.PrtCli;
            if (VB6Helpers.Instr(Mdl_Funciones_Varias.VxDec.PrtExp1, "|") != 0)
            {
                PrtExp1 = MODGPYF0.Componer(Mdl_Funciones_Varias.VxDec.PrtExp1, "|", "");
            }
            if (VB6Helpers.Instr(Mdl_Funciones_Varias.VxDec.PrtExp1, "~") != 0)
            {
                PrtExp1 = MODGPYF0.Componer(Mdl_Funciones_Varias.VxDec.PrtExp1, "~", "");
            }
            if (VB6Helpers.Instr(Mdl_Funciones_Varias.VxDec.PrtExp2, "|") != 0)
            {
                PrtExp2 = MODGPYF0.Componer(Mdl_Funciones_Varias.VxDec.PrtExp2, "|", "");
            }
            if (VB6Helpers.Instr(Mdl_Funciones_Varias.VxDec.PrtExp2, "~") != 0)
            {
                PrtExp2 = MODGPYF0.Componer(Mdl_Funciones_Varias.VxDec.PrtExp2, "~", "");
            }
            if (VB6Helpers.Instr(p, "|") != 0)
            {
                p = MODGPYF0.Componer(p, "|", "");
            }
            if (VB6Helpers.Instr(p, "~") != 0)
            {
                p = MODGPYF0.Componer(p, "~", "");
            }
            if (p == PrtExp1)
            {
                PrtyOK = 1;
            }

            if (p == PrtExp2)
            {
                PrtyOK = 2;
            }

            if (PrtyOK == 0)
            {
                //Fr_Declaracion.Enabled = True
                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "Atención: La Declaración existe pero está asociada a otro Exportador."
                });
                if (Frm_Comercio_Invisible.Tx_NroDec.Enabled)
                {

                }
                return _retValue;
            }

            //Validaciones de las Declaraciones.
            j = short.Parse(Frm_Comercio_Invisible.Lt_Tcp.Items.ElementAt(Frm_Comercio_Invisible.Lt_Tcp.ListIndex).ID);
            Var = ModChVrf.VCcpl[j].CptCom + ModChVrf.VCcpl[j].CodCom;

            switch (Var)
            {
                //Flete.-
                case "25111901K":
                    //Cláusula Compra-Venta 1,2,9
                    if ((Mdl_Funciones_Varias.VxDec.CodCCv != 1) && (Mdl_Funciones_Varias.VxDec.CodCCv != 2) && (Mdl_Funciones_Varias.VxDec.CodCCv != 9))
                    {
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "Atención: La Cláusula de Compra-Venta de la Declaración no coincide con la estipulada:" + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.CodCCv)) + "."
                        });
                        return _retValue;
                    }

                    //Monto <= ValFle
                    switch (PrtyOK)
                    {
                        case 1:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValFle1 - Mdl_Funciones_Varias.VxDec.ValFle1c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al Flete disponible en la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValFle1 - Mdl_Funciones_Varias.VxDec.ValFle1c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                        case 2:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValFle2 - Mdl_Funciones_Varias.VxDec.ValFle2c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al Flete disponible en la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValFle1 - Mdl_Funciones_Varias.VxDec.ValFle1c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                    }

                    //Seguro.-
                    break;
                case "251305014":
                    //Cláusula Compra-Venta 1,2,9
                    if ((Mdl_Funciones_Varias.VxDec.CodCCv != 1) && (Mdl_Funciones_Varias.VxDec.CodCCv != 2) && (Mdl_Funciones_Varias.VxDec.CodCCv != 9))
                    {
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "Atención: La Cláusula de Compra-Venta de la Declaración no coincide con la estipulada:" + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.CodCCv)) + "."
                        });
                        return _retValue;
                    }

                    //Monto <= ValSeg
                    switch (PrtyOK)
                    {
                        case 1:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValSeg1 - Mdl_Funciones_Varias.VxDec.ValSeg1c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al Seguro disponible en la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValSeg1 - Mdl_Funciones_Varias.VxDec.ValSeg1c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                        case 2:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValSeg2 - Mdl_Funciones_Varias.VxDec.ValSeg2c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al Seguro disponible en la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValSeg1 - Mdl_Funciones_Varias.VxDec.ValSeg1c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                    }

                    break;
                case "251240014":
                    //Monto <= ValCom
                    switch (PrtyOK)
                    {
                        case 1:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValCom1 - Mdl_Funciones_Varias.VxDec.ValCom1c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al valor de la Comisión de la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValCom1 - Mdl_Funciones_Varias.VxDec.ValCom1c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                        case 2:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValCom2 - Mdl_Funciones_Varias.VxDec.ValCom2c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al valor de la Comisión de la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValCom2 - Mdl_Funciones_Varias.VxDec.ValCom2c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                    }

                    break;

                    //Cláusula Compra-Venta 1,7,9
                    if ((Mdl_Funciones_Varias.VxDec.CodCCv != 1) && (Mdl_Funciones_Varias.VxDec.CodCCv != 7) && (Mdl_Funciones_Varias.VxDec.CodCCv != 9))
                    {
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "Atención: La Cláusula de Compra-Venta de la Declaración no coincide con la estipulada:" + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.CodCCv)) + "."
                        });
                        return _retValue;
                    }

                    break;
                case "251607010":
                    //Monto <= ValLiq
                    switch (PrtyOK)
                    {
                        case 1:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValLiq1 - Mdl_Funciones_Varias.VxDec.ValLiq1c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al valor Líquido de la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValLiq1 - Mdl_Funciones_Varias.VxDec.ValLiq1c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                        case 2:
                            if (Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) > Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.VxDec.ValLiq2 - Mdl_Funciones_Varias.VxDec.ValLiq2c) * Mtopar, "0.00")))
                            {
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "Atención: El Monto de la Compra-Venta debe ser menor o igual al valor Líquido de la Declaración: US$ " + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.VxDec.ValLiq2 - Mdl_Funciones_Varias.VxDec.ValLiq2c)) + "."
                                });
                                return _retValue;
                            }

                            break;
                    }

                    break;
            }
            return 1;
        }

        //****************************************************************************
        //   1.  Carga la Estructura de VgPli() que será la que actualiza la
        //       Lista de Operaciones.
        //****************************************************************************
        private static void Pr_Carga_Estructura(T_MODGPYF0 MODGPYF0, T_ModChVrf ModChVrf, T_MODGTAB0 MODGTAB0, T_MODGCVD MODGCVD, UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, UnitOfWorkCext01 unit, short Indice)
        {
            short Moneda = 0;
            short n = 0;
            short m = 0;
            MODGCVD.VgPli[Indice].numcor = (short)(Frm_Comercio_Invisible.Lt_Operacion.ListCount + 1);

            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0"))
            {
                MODGCVD.VgPli[Indice].TipCVD = T_Mdl_Funciones.CompraDivisa;
            }

            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
            {
                MODGCVD.VgPli[Indice].TipCVD = T_Mdl_Funciones.VentaDivisa;
            }

            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("2"))
            {
                MODGCVD.VgPli[Indice].TipCVD = T_Mdl_Funciones.TransIngresoDivisa;
            }

            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("3"))
            {
                MODGCVD.VgPli[Indice].TipCVD = T_Mdl_Funciones.TransEgresoDivisa;
            }

            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("4"))
            {
                MODGCVD.VgPli[Indice].TipCVD = T_Mdl_Funciones.TransInternaDivisa;
            }

            MODGCVD.VgPli[Indice].codpai = 0;
            if (Frm_Comercio_Invisible.Cb_Pais.ListIndex >= 0)
            {
                MODGCVD.VgPli[Indice].codpai = short.Parse(Frm_Comercio_Invisible.Cb_Pais.Items.ElementAt(Frm_Comercio_Invisible.Cb_Pais.ListIndex).ID);
            }

            MODGCVD.VgPli[Indice].CodMnd = short.Parse(Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt(Frm_Comercio_Invisible.Cb_Moneda.ListIndex).ID);

            Moneda = MODGCVD.VgPli[Indice].CodMnd;
            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, Moneda);
            MODGCVD.VgPli[Indice].MndCBC = MODGTAB0.VMnd[n].Mnd_MndCbc;
            MODGCVD.VgPli[Indice].NemMnd = MODGTAB0.VMnd[n].Mnd_MndNmc;
            MODGCVD.VgPli[Indice].MtoCVD = Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text);
            MODGCVD.VgPli[Indice].TipCam = Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[1].Text);
            MODGCVD.VgPli[Indice].MtoPes = Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[2].Text);
            MODGCVD.VgPli[Indice].Mtopar = Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[3].Text);

            m = short.Parse(Frm_Comercio_Invisible.Lt_Tcp.Items.ElementAt(Frm_Comercio_Invisible.Lt_Tcp.ListIndex).ID);
            MODGCVD.VgPli[Indice].CodTcp = ModChVrf.VCcpl[m].CodCom + ModChVrf.VCcpl[m].CptCom;
            if (ModChVrf.VCcpl[m].dataut != 0)
            {
                MODGCVD.VgPli[Indice].ApcNum = Frm_Comercio_Invisible.Tx_NroAut.Text;
                MODGCVD.VgPli[Indice].ApcFec = Frm_Comercio_Invisible.Tx_FecAut.Text;
                MODGCVD.VgPli[Indice].ApcPbc = VB6Helpers.CShort(MODGPYF1.ValTexto(MODGPYF0, Frm_Comercio_Invisible.Tx_SucBcch.Text));
                //TODO: EMILIANO -> VALIDAR SI SE CAMBIA ESTO O SE CAMBIA EL FRAME Y POR TRANSITIVA FUNCIONA
                if (Frm_Comercio_Invisible.Cb_TipAut.Visible == true)
                {
                    if (Frm_Comercio_Invisible.Cb_TipAut.Items[Frm_Comercio_Invisible.Cb_TipAut.ListIndex].Data.Equals(6))
                    {
                        MODGCVD.VgPli[Indice].ApcTip = "";
                    }
                    else
                    {
                        if (Frm_Comercio_Invisible.Cb_TipAut.Items[Frm_Comercio_Invisible.Cb_TipAut.ListIndex].Value.IndexOf(':') > -1)
                        {
                            MODGCVD.VgPli[Indice].ApcTip = Frm_Comercio_Invisible.Cb_TipAut.Items[Frm_Comercio_Invisible.Cb_TipAut.ListIndex].Value.Split(':')[0];
                        }
                    }

                }

            }
            else
            {
                MODGCVD.VgPli[Indice].ApcNum = "";
                MODGCVD.VgPli[Indice].ApcFec = "";
                MODGCVD.VgPli[Indice].ApcPbc = 0;
                MODGCVD.VgPli[Indice].ApcTip = "";
            }

            MODGCVD.VgPli[Indice].CodOci = VB6Helpers.CShort(ModChVrf.VCcpl[m].tipope);
            if (MODGCVD.VgPli[Indice].CodOci == 0)
            {
                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                {
                    Text = "Atención: El Tipo de Operación de Cambios de Comercio Invisible no tiene asignado un valor.",
                    Type = TipoMensaje.Informacion
                });
            }

            if (ModChVrf.VCcpl[m].flging != 0)
            {
                if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0"))
                {
                    MODGCVD.VgPli[Indice].IngEgr = "I";
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("2"))
                {
                    MODGCVD.VgPli[Indice].IngEgr = T_Mdl_Funciones.TransIngresoDivisa;
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("4"))
                {
                    MODGCVD.VgPli[Indice].IngEgr = T_Mdl_Funciones.TransInternaDivisa;
                }

            }
            else
            {
                if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                {
                    MODGCVD.VgPli[Indice].IngEgr = "E";
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("3"))
                {
                    MODGCVD.VgPli[Indice].IngEgr = T_Mdl_Funciones.TransEgresoDivisa;
                }

            }

            if (Frm_Comercio_Invisible.Ch_Convenio.Value == -1)
            {
                MODGCVD.VgPli[Indice].Conven = (short)(true ? -1 : 0);
                MODGCVD.VgPli[Indice].NumAcu = 1;
                MODGCVD.VgPli[Indice].Desacu = "1493;";
            }
            else
            {
                MODGCVD.VgPli[Indice].Conven = (short)(false ? -1 : 0);
                MODGCVD.VgPli[Indice].NumAcu = 0;
                MODGCVD.VgPli[Indice].Desacu = "";
            }

            MODGCVD.VgPli[Indice].Status = T_MODGCVD.EstadoIng;

            //Carga los datos de la Declaración.
            MODGCVD.VgPli[Indice].numdec = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_NroDec.Text);
            MODGCVD.VgPli[Indice].FecDec = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_FecDec.Text);
            MODGCVD.VgPli[Indice].CodAdn = (short)VB6Helpers.Val(Frm_Comercio_Invisible.Tx_CodAdn.Text);
            MODGCVD.VgPli[Indice].CodEOR = Frm_Comercio_Invisible.Tx_ER.Text ?? string.Empty;

            //Carga los datos del IDE.
            MODGCVD.VgPli[Indice].DiePbc = 0;
            //Verifica si es Declaración.
            if (Frm_Comercio_Invisible.IndDec != 0)
            {
                MODGCVD.VgPli[Indice].IndDec = (short)(true ? -1 : 0);
            }
            else
            {
                MODGCVD.VgPli[Indice].IndDec = (short)(false ? -1 : 0);
            }

            //Carga los datos del Convenio Crédito Recíproco.
            MODGCVD.VgPli[Indice].FecDeb = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_FecDeb.Text);
            MODGCVD.VgPli[Indice].DocNac = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_DocNac.Text);
            MODGCVD.VgPli[Indice].DocExt = VB6Helpers.Trim(Frm_Comercio_Invisible.Tx_DocExt.Text);

            //Carga Datos Adicionales de la Planilla Invisible
            MODGCVD.VgPli[Indice].FecCre = Frm_Comercio_Invisible.Tx_Fecha.Text;
            MODGCVD.VgPli[Indice].NumCre = Format.StringToDouble((Frm_Comercio_Invisible.Tx_NumIns.Text ?? "0"));
            if (Frm_Comercio_Invisible.Cb_MonDes.ListIndex != -1)
            {
                MODGCVD.VgPli[Indice].MndCre = short.Parse(Frm_Comercio_Invisible.Cb_MonDes.Items.ElementAt(Frm_Comercio_Invisible.Cb_MonDes.ListIndex).ID);
            }

            MODGCVD.VgPli[Indice].MtoCre = Format.StringToDouble((Frm_Comercio_Invisible.Tx_Mto.Text ?? "0"));
            MODGCVD.VgPli[Indice].fecins = Frm_Comercio_Invisible.Tx_FecIns.Text;
            MODGCVD.VgPli[Indice].NomFin = Frm_Comercio_Invisible.Tx_NomFin.Text;
            MODGCVD.VgPli[Indice].VenOfi = Frm_Comercio_Invisible.Tx_FecVC.Text;

            //TODO: REVISAR SI NO NECESITA EL FORMATO YYYY-MM-DD
            MODGCVD.VgPli[Indice].AnuFec = VB6Helpers.Format(Frm_Comercio_Invisible.Tx_FecPre.Text, "dd/MM/yyyy");
            MODGCVD.VgPli[Indice].AnuNum = VB6Helpers.CInt(MODGPYF1.ValTexto(MODGPYF0, Frm_Comercio_Invisible.Tx_NumPre.Text));
            MODGCVD.VgPli[Indice].AnuPbc = VB6Helpers.CShort(MODGPYF1.ValTexto(MODGPYF0, Frm_Comercio_Invisible.Tx_CodIns.Text));
            MODGCVD.VgPli[Indice].NumCon = VB6Helpers.CInt(MODGPYF1.ValTexto(MODGPYF0, Frm_Comercio_Invisible.Tx_NumCon.Text));
            MODGCVD.VgPli[Indice].fecsus = VB6Helpers.Format(Frm_Comercio_Invisible.Tx_FecSus.Text, "dd/MM/yyyy");

            if (Frm_Comercio_Invisible.Cb_InsUt.ListIndex != -1)
            {
                MODGCVD.VgPli[Indice].insuti = short.Parse(Frm_Comercio_Invisible.Cb_InsUt.Items.ElementAt(Frm_Comercio_Invisible.Cb_InsUt.ListIndex).ID);
            }

            MODGCVD.VgPli[Indice].partip = Format.StringToDouble((Frm_Comercio_Invisible.Tx_ParTip.Text ?? "0"));

            if (Frm_Comercio_Invisible.Cb_ArCon.ListIndex != -1)
            {
                MODGCVD.VgPli[Indice].arecon = short.Parse(Frm_Comercio_Invisible.Cb_ArCon.Items.ElementAt(Frm_Comercio_Invisible.Cb_ArCon.ListIndex).ID);
            }

            if (Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex != -1)
            {
                MODGCVD.VgPli[Indice].SecBen = short.Parse(Frm_Comercio_Invisible.Cb_SecEcBen.Items.ElementAt(Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex).ID);
            }

            if (Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex != -1)
            {
                MODGCVD.VgPli[Indice].SecInv = short.Parse(Frm_Comercio_Invisible.Cb_SecEcIn.Items.ElementAt(Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex).ID);
            }

            MODGCVD.VgPli[Indice].VenOd = VB6Helpers.Format(Frm_Comercio_Invisible.Tx_FecVen.Text, "dd/MM/yyyy");
            MODGCVD.VgPli[Indice].canacu = VB6Helpers.CInt(MODGPYF1.ValTexto(MODGPYF0, Frm_Comercio_Invisible.Tx_CanAc.Text));
            if (Frm_Comercio_Invisible.ch_AfDer.Value == -1)
            {
                MODGCVD.VgPli[Indice].afeder = (short)(true ? -1 : 0);
            }
            else
            {
                MODGCVD.VgPli[Indice].afeder = (short)(false ? -1 : 0);
            }

            if (Frm_Comercio_Invisible.ch_ZoFra.Value == -1)
            {
                MODGCVD.VgPli[Indice].ZonFra = (short)(true ? -1 : 0);
            }
            else
            {
                MODGCVD.VgPli[Indice].ZonFra = (short)(false ? -1 : 0);
            }

            MODGCVD.VgPli[Indice].PrcPar = Format.StringToDouble((Frm_Comercio_Invisible.Tx_PrcPar.Text ?? "0"));
        }

        //****************************************************************************
        //   1.  Limpia todos los campos de la Pantalla cuando se seleciona otro
        //       Tipo de Conceptos de Planillas.
        //****************************************************************************
        private static void Pr_Limpiar_Campos(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;

            if (Frm_Comercio_Invisible.Switch == 0)
            {
                Frm_Comercio_Invisible.Cb_Pais.ListIndex = -1;
                Frm_Comercio_Invisible.Cb_Moneda.ListIndex = -1;
                Cb_Moneda_Click(initObject, unit);
                Frm_Comercio_Invisible.Tx_MtoCV[0].Text = "";
                Frm_Comercio_Invisible.Tx_MtoCV[1].Text = "";
                Frm_Comercio_Invisible.Tx_MtoCV[2].Text = "";
                Frm_Comercio_Invisible.Tx_MtoCV[3].Text = "";
                Frm_Comercio_Invisible.Tx_CanAc.Text = "";
                Frm_Comercio_Invisible.Ch_Convenio.Value = 0;
                Frm_Comercio_Invisible.ch_AfDer.Value = 0;
                Frm_Comercio_Invisible.ch_ZoFra.Value = 0;
            }

            //Limpiar los Campos de la Declaración.
            Frm_Comercio_Invisible.Tx_NroDec.Text = "";
            Frm_Comercio_Invisible.Tx_FecDec.Text = "";
            Frm_Comercio_Invisible.Tx_CodAdn.Text = "";
            Frm_Comercio_Invisible.Tx_ER.Text = "";

            //Limpiar los Campos del Convenio Crédito Recíproco.
            Frm_Comercio_Invisible.Tx_FecDeb.Text = "";
            Frm_Comercio_Invisible.Tx_DocExt.Text = "";
            Frm_Comercio_Invisible.Tx_DocNac.Text = "";

            //Limpiar los campos de la autorización del banco central.-
            Frm_Comercio_Invisible.Tx_NroAut.Text = "";
            Frm_Comercio_Invisible.Tx_FecAut.Text = "";
            Frm_Comercio_Invisible.Tx_SucBcch.Text = "";
            Frm_Comercio_Invisible.Cb_TipAut.ListIndex = -1;

            //Limpiar los campos de la operaciòn relacionada
            Frm_Comercio_Invisible.Tx_FecPre.Text = "";
            Frm_Comercio_Invisible.Tx_NumPre.Text = "";
            Frm_Comercio_Invisible.Tx_CodIns.Text = "";

            //Limpiar los campos de las operaciones financieras internacionales
            Frm_Comercio_Invisible.Tx_NumIns.Text = "";
            Frm_Comercio_Invisible.Tx_FecIns.Text = "";
            Frm_Comercio_Invisible.Tx_NomFin.Text = "";
            Frm_Comercio_Invisible.Tx_FecVC.Text = "";
            Frm_Comercio_Invisible.Tx_Fecha.Text = "";
            Frm_Comercio_Invisible.Cb_MonDes.ListIndex = -1;
            Frm_Comercio_Invisible.Tx_Mto.Text = "";
            Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex = -1;
            Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex = -1;
            Frm_Comercio_Invisible.Tx_PrcPar.Text = "";

            //Limpiar los campos de las operaciones de derivados
            Frm_Comercio_Invisible.Tx_NumCon.Text = "";
            Frm_Comercio_Invisible.Tx_FecSus.Text = "";
            Frm_Comercio_Invisible.Tx_FecVen.Text = "";
            Frm_Comercio_Invisible.Cb_InsUt.ListIndex = -1;
            Frm_Comercio_Invisible.Tx_ParTip.Text = "";
            Frm_Comercio_Invisible.Cb_ArCon.ListIndex = -1;
        }
        #endregion

        #region METODOS PUBLICOS

        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_MODCARMAS MODCARMAS = initObject.MODCARMAS;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;
            UI_Frm_Principal Frm_Principal = initObject.Frm_Principal;
            T_ModChVrf ModChVrf = initObject.ModChVrf;

            Frm_Comercio_Invisible.OPESIN = MODGCVD.VgCvd.OpeSin;

            short[] Tab1 = null;
            short x = 0;
            short a;
            short k = 0;
            short i = 0;
            short c = 0;
            short d = 0;
            short b = 0;


            Frm_Comercio_Invisible.Fr_Autori.Visible = false;
            Frm_Comercio_Invisible.Fr_OpRe.Visible = false;
            Frm_Comercio_Invisible.Fr_Declaracion.Visible = false;
            Frm_Comercio_Invisible.Fr_Convenio.Visible = false;


            // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.

            Frm_Comercio_Invisible.TipDiv = 0;
            Frm_Comercio_Invisible.OPER_AUTOMATICA = 0;
            Frm_Comercio_Invisible.Switch = 0;

            //Lee los Países.
            BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.CargaEnLista_Pai(MODGTAB0, Frm_Comercio_Invisible.Cb_Pais, unit);


            k = (short)VB6Helpers.UBound(MODGTAB0.VMnd);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            for (i = 0; i <= (short)k; i++)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndCod != T_MODGTAB0.MndNac)
                {
                    Frm_Comercio_Invisible.Cb_Moneda.Items.Add(new UI_ComboItem()
                    {
                        Value = MODGTAB0.VMnd[i].Mnd_MndNom,
                        ID = MODGTAB0.VMnd[i].Mnd_MndCod.ToString(),
                        Data = MODGTAB0.VMnd[i].Mnd_MndCod
                    });
                    Frm_Comercio_Invisible.Cb_MonDes.Items.Add(new UI_ComboItem()
                    {
                        Value = MODGPYF1.Minuscula(MODGTAB0.VMnd[i].Mnd_MndNom),
                        ID = MODGTAB0.VMnd[i].Mnd_MndCod.ToString(),
                        Data = MODGTAB0.VMnd[i].Mnd_MndCod
                    });
                }
            }

            //Carga los sectores económicos del beneficiarios

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGTAB0.SyGetSecEc()'. Consider using the GetDefaultMember6 helper method.
            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetSecEc(MODGTAB0, unit));
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.SecEc); i++)
            {
                Frm_Comercio_Invisible.Cb_SecEcBen.Items.Add(new UI_ComboItem()
                {
                    Value = MODGTAB0.SecEc[i].NomSec,
                    ID = MODGTAB0.SecEc[i].CodSec.ToString(),
                    Data = MODGTAB0.SecEc[i].CodSec
                });
                Frm_Comercio_Invisible.Cb_SecEcIn.Items.Add(new UI_ComboItem()
                {
                    Value = MODGTAB0.SecEc[i].NomSec,
                    ID = MODGTAB0.SecEc[i].CodSec.ToString(),
                    Data = MODGTAB0.SecEc[i].CodSec
                });
            }

            //Lee los Conceptos de Planillas.
            c = BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.SyGet_ccpl(ModChVrf, unit);

            //Cargar la Combox que determina si es Compra o Venta.
            Pr_Cargar_Divisa(initObject, unit);

            //Carga los días feriados del año 1995.
            d = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetn_VFer(MODGTAB0, unit);

            //Precarga algunos datos.
            //----->> ESTO SE CAMBIO POR LA FORMA EN QUE SE TRATAN LAS OPERACIONES -->> MODGCVD.VgPli[0].TipCVD = "C";

            //Inicializa la Lista de las Operaciones con un Item en blanco para el ingreso de Datos Nuevos.
            Frm_Comercio_Invisible.Lt_Operacion.Items.Clear();
            //Lt_Operacion.AddItem("");
            //Lt_Operacion.set_ItemData(Lt_Operacion.NewIndex, 0);
            //Lt_Operacion.set_Selected(0, true);

            Frm_Comercio_Invisible.Tx_ER.Text = "E";

            //Carga algunos datos ya generados.-
            Pr_Llena_Lt_Operacion(initObject, unit);

            b = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.SyGet_TipAut(Mdl_Funciones_Varias, unit);
            MODPREEM.CargaEnLista_TipAut(Mdl_Funciones_Varias, Frm_Comercio_Invisible.Cb_TipAut);

            b = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.SyGet_Instru(Mdl_Funciones_Varias, unit);
            CargaEnLista_Instru(Mdl_Funciones_Varias, Frm_Comercio_Invisible.Cb_InsUt);

            b = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.SyGet_AreCon(Mdl_Funciones_Varias, unit);
            CargaEnLista_AreCon(Mdl_Funciones_Varias, Frm_Comercio_Invisible.Cb_ArCon);

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                Pr_CargaAUTOMATICA(initObject, unit);
                if (Format.StringToDouble(Mdl_Funciones_Varias.LC_PRD) == 62)
                {
                    Frm_Comercio_Invisible.Cb_Divisa.ListIndex = 2;  //' Incoming = 62
                    Cb_Divisa_click(initObject, unit);
                }
                if (Format.StringToDouble(Mdl_Funciones_Varias.LC_PRD) != 62)
                {
                    Frm_Comercio_Invisible.Cb_Divisa.ListIndex = 3;  //' Outgoing
                    Cb_Divisa_click(initObject, unit);
                }
                Frm_Comercio_Invisible.Cb_Divisa.Enabled = false;
            }

            Frm_Comercio_Invisible.CargaAutomatica = Mdl_Funciones_Varias.CARGA_AUTOMATICA;

            short Y = 0;
            if (MODGCVD.NOTACRED == true)
            {
                Frm_Comercio_Invisible.Cb_Divisa.ListIndex = 4;
                //es necesario ejecutar el evento para que llene nuevamente el listado de monedas
                Cb_Divisa_click(initObject, unit);
                Frm_Comercio_Invisible.Cb_Divisa.Enabled = false;

                for (Y = 0; Y < (short)Frm_Comercio_Invisible.Cb_Moneda.Items.Count; Y++)
                {
                    if (Frm_Comercio_Invisible.Cb_Moneda.Items[Y].ID.Equals(T_MODGTAB0.MndNac.ToString()))
                    {
                        Frm_Comercio_Invisible.Cb_Moneda.ListIndex = Y;
                        break;
                    }
                }

                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGetn_Codtran(MODXORI, unit);
                Frm_Comercio_Invisible.Cb_Moneda.Enabled = false;
                MODGCVD.TIN = true;
                /// Se asigna a la variabe el valor de uno, para que en el formulario la carga del formulario
                /// se posicione en el combox Concepto
                Frm_Comercio_Invisible.CargaAutomatica = 1;
            }

            if (MODCVDIM.Gvar_NotaCredito == 1 && MODGCVD.TIN == true)
            {
                Frm_Comercio_Invisible.Tx_MtoCV[0].Text = VB6Helpers.Trim(Frm_Principal.Tx_MtoOri.Text);
                Frm_Comercio_Invisible.Tx_MtoCV[0].Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        public static void ok_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer("ok_Click"))
            {
                trace.AddToContext("Frm_Comercio_Invisible_Logic", "ok_Click");
                T_MODGPYF0 MODGPFY0 = initObject.MODGPYF0;
                T_ModChVrf ModChVrf = initObject.ModChVrf;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                string s1 = "", s2 = "", s3 = "", NN = "", Msg = string.Empty;
                short ldec = 0;
                short x = 0;
                short b = 0, Indice_Arreglo = 0;
                short a = Fn_Validar_Campos(MODGPFY0, ModChVrf, Mdi_Principal, initObject.Frm_Comercio_Invisible, 1, 6);

                if (a == 0)
                {
                    return;
                }

                /// 
                /// Se verifica el dígito verificador de la declaración
                /// 
                if (!String.IsNullOrEmpty(initObject.Frm_Comercio_Invisible.Tx_NroDec.Text))
                {
                    s1 = VB6Helpers.Format(initObject.Frm_Comercio_Invisible.Tx_CodAdn.Text, "000");
                    ldec = (short)(7 - VB6Helpers.Len(initObject.Frm_Comercio_Invisible.Tx_NroDec.Text));
                    s2 = MODGPYF1.Zeros(ldec) + initObject.Frm_Comercio_Invisible.Tx_NroDec.Text;
                    s3 = s1 + s2;
                    x = EsRut(s3);
                    if (~x != 0)
                    {
                        Msg = "Debe corregir el Código de Aduana o el Dígito Verificador del Número de la Declaración. Alguno de ellos no está correcto.";
                        trace.TraceError(Msg);
                        initObject.Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = Msg
                        });
                        return;
                    }
                }

                ldec = (short)(7 - VB6Helpers.Len(initObject.Frm_Comercio_Invisible.Tx_NroDec.Text));
                NN = MODGPYF1.Zeros(ldec) + VB6Helpers.UCase(initObject.Frm_Comercio_Invisible.Tx_NroDec.Text);

                b = Fn_Declaracion(ModChVrf, MODGCVD, MODGTAB0, Mdl_Funciones_Varias, Mdi_Principal, initObject.Frm_Comercio_Invisible, unit);
                if (b == 0)
                {
                    return;
                }
                if (initObject.Frm_Comercio_Invisible.Ch_Convenio.Value == -1)
                {
                    if (string.IsNullOrEmpty(initObject.Frm_Comercio_Invisible.Tx_FecDeb.Text))
                    {
                        Msg = "Debe ingresar Fecha Débito Convenio Reciproco";
                        trace.TraceError(Msg);
                        initObject.Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = Msg
                        });
                        return;
                    }

                    if (string.IsNullOrEmpty(initObject.Frm_Comercio_Invisible.Tx_DocNac.Text))
                    {
                        Msg = "Debe ingresar Nro. Documento Nacional";
                        trace.TraceError(Msg);
                        initObject.Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = Msg
                        });
                        return;
                    }

                    if (string.IsNullOrEmpty(initObject.Frm_Comercio_Invisible.Tx_DocExt.Text))
                    {
                        Msg = "Debe ingresar Nro. Documento Extranjero";
                        trace.TraceError(Msg);
                        initObject.Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = Msg
                        });
                        return;
                    }
                }

                int _switchVar1 = initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex;
                if (_switchVar1 == -1)
                {
                    /// 
                    /// Nuevo = Se adiciona Uno Más.
                    /// 
                    Indice_Arreglo = (short)(VB6Helpers.UBound(MODGCVD.VgPli) + 1);
                    VB6Helpers.RedimPreserve(ref MODGCVD.VgPli, 0, Indice_Arreglo);
                }
                else if (_switchVar1 != -1)
                {
                    /// 
                    /// Viejo = Se Modifica este.
                    /// 
                    Indice_Arreglo = short.Parse(initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.ElementAt(initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex).ID);
                }
                Pr_Carga_Estructura(MODGPFY0, ModChVrf, MODGTAB0, MODGCVD, Mdi_Principal, initObject.Frm_Comercio_Invisible, unit, Indice_Arreglo);
                Mdl_Funciones_Varias.VaPli = Mdl_Funciones_Varias.VaPliNul;

                /// 
                /// Almacena los últimos datos registrados
                /// 
                MODGCVD.AntOper = (short)initObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex;
                MODGCVD.Antmon = short.Parse(initObject.Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt(initObject.Frm_Comercio_Invisible.Cb_Moneda.ListIndex).ID);
                MODGCVD.Anttip = Format.StringToDouble(initObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text);

                Pr_Llena_Lt_Operacion(initObject, unit);
                Pr_Limpiar_Campos(initObject, unit);
            }
        }

        public static void Lt_Tcp_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;
            T_MODCARMAS MODCARMAS = initObject.MODCARMAS;
            T_MODGCVD MODGCVD = initObject.MODGCVD;

            short i = 0;
            string Con = "";
            if (Frm_Comercio_Invisible.Lt_Tcp.ListIndex != -1)
            {
                i = short.Parse(Frm_Comercio_Invisible.Lt_Tcp.Items.ElementAt(Frm_Comercio_Invisible.Lt_Tcp.ListIndex).ID);
            }

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0 && MODCARMAS.CARGA_MASIVA == false)
            {
                if (!MODGCVD.NOTACRED)
                    Pr_Limpiar_Campos(initObject, unit);
            }

            /// 
            /// Habilitado o Inhabilita el Frame de Convenio Crédito Recíproco.
            /// 
            if (Frm_Comercio_Invisible.Ch_Convenio.Checked)
            {
                Frm_Comercio_Invisible.Fr_Convenio.Enabled = true;
                Frm_Comercio_Invisible.Fr_Convenio.Visible = true;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_Convenio.Enabled = false;
                Frm_Comercio_Invisible.Fr_Convenio.Visible = false;
            }

            Con = VB6Helpers.Mid(Frm_Comercio_Invisible.Lt_Tcp.Text, 1, 6);
            if (VB6Helpers.Instr(UI_Frm_Comercio_Invisibles.Concepai, Con) == 0)
            {
                /// 
                /// Requiere País.
                /// 
                Frm_Comercio_Invisible.Cb_Pais.Enabled = true;
            }
            else
            {
                Frm_Comercio_Invisible.Cb_Pais.Enabled = false;
                Frm_Comercio_Invisible.Cb_Pais.ListIndex = -1;
            }

            if (ModChVrf.VCcpl[i].dataut == -1)
            {
                Frm_Comercio_Invisible.Fr_Autori.Enabled = true;
                Frm_Comercio_Invisible.Fr_Autori.Visible = true;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_Autori.Enabled = false;
                Frm_Comercio_Invisible.Fr_Autori.Visible = false;
            }

            if (ModChVrf.VCcpl[i].operel == -1)
            {
                Frm_Comercio_Invisible.Fr_OpRe.Enabled = true;
                Frm_Comercio_Invisible.Fr_OpRe.Visible = true;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_OpRe.Enabled = false;
                Frm_Comercio_Invisible.Fr_OpRe.Visible = false;
            }

            if (ModChVrf.VCcpl[i].numins == -1 || ModChVrf.VCcpl[i].fecins == -1 || ModChVrf.VCcpl[i].finext == -1 || ModChVrf.VCcpl[i].vtocic == -1 || ModChVrf.VCcpl[i].fecdes == -1 || ModChVrf.VCcpl[i].mondes == -1 || ModChVrf.VCcpl[i].mtodes == -1)
            {
                Frm_Comercio_Invisible.Fr_OFI.Visible = true;
                Frm_Comercio_Invisible.Fr_OFI.Enabled = true;
                Frm_Comercio_Invisible.Cb_MonDes.Enabled = true;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_OFI.Enabled = false;
                Frm_Comercio_Invisible.Fr_OFI.Visible = false;
                Frm_Comercio_Invisible.Cb_MonDes.Enabled = false;
            }

            if (ModChVrf.VCcpl[i].infimp == -1 || ModChVrf.VCcpl[i].infexp == -1 || ModChVrf.VCcpl[i].datint == -1)
            {
                Frm_Comercio_Invisible.Fr_Sec.Enabled = true;
                Frm_Comercio_Invisible.Fr_Sec.Visible = true;
                Frm_Comercio_Invisible.Cb_SecEcBen.Enabled = true;
                Frm_Comercio_Invisible.Cb_SecEcIn.Enabled = true;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_Sec.Enabled = false;
                Frm_Comercio_Invisible.Fr_Sec.Visible = false;
                Frm_Comercio_Invisible.Cb_SecEcBen.Enabled = false;
                Frm_Comercio_Invisible.Cb_SecEcIn.Enabled = false;
            }

            if (ModChVrf.VCcpl[i].decexp == -1)
            {
                Frm_Comercio_Invisible.Fr_Declaracion.Enabled = true;
                Frm_Comercio_Invisible.Fr_Declaracion.Visible = true;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_Declaracion.Enabled = false;
                Frm_Comercio_Invisible.Fr_Declaracion.Visible = false;
            }

            if (ModChVrf.VCcpl[i].datder == -1)
            {
                Frm_Comercio_Invisible.Fr_OpeD.Enabled = true;
                Frm_Comercio_Invisible.Fr_OpeD.Visible = true;
                Frm_Comercio_Invisible.ch_AfDer.Value = -1;
            }
            else
            {
                Frm_Comercio_Invisible.Fr_OpeD.Enabled = false;
                Frm_Comercio_Invisible.Fr_OpeD.Visible = false;
                Frm_Comercio_Invisible.ch_AfDer.Value = 0;
            }

        }

        public static void Ch_Convenio_Click(T_MODGCVD MODGCVD, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible)
        {
            bool ls_valida = !Frm_Comercio_Invisible.Ch_Convenio.Checked;

            if (ls_valida)
            {
                Frm_Comercio_Invisible.Fr_Convenio.Enabled = true;
                Frm_Comercio_Invisible.Fr_Convenio.Visible = true;
                Frm_Comercio_Invisible.Tx_FecDeb.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Frm_Comercio_Invisible.Tx_DocExt.Text = MODGCVD.VgCvd.OpeSin;
            }
            else
            {
                Frm_Comercio_Invisible.Tx_FecDeb.Text = "";
                Frm_Comercio_Invisible.Tx_DocExt.Text = "";
                Frm_Comercio_Invisible.Fr_Convenio.Enabled = false;
                Frm_Comercio_Invisible.Fr_Convenio.Visible = true;
            }

        }

        public static void Cb_Divisa_click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;

            short T = (short)(short.Parse(Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID) + 1);
            short d = 0;

            if (T != Frm_Comercio_Invisible.TipDiv)
            {
                Frm_Comercio_Invisible.TipDiv = T;
                BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.CargaEnListaTcp(ModChVrf, Frm_Comercio_Invisible.Lt_Tcp, T);
                if (Frm_Comercio_Invisible.Lt_Tcp.Items.Count > 0)
                {
                    Frm_Comercio_Invisible.Lt_Tcp.ListIndex = 0;
                    Lt_Tcp_Click(initObject, unit);
                }
            }

            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("4"))
            {
                //TRANSFERENCIA INTERNA
                MODGCVD.TIN = true;
                var monedaNacional = MODGTAB0.VMnd[T_MODGTAB0.VMndDict[T_MODGTAB0.MndNac]];
                if (!Frm_Comercio_Invisible.Cb_Moneda.Items.Any(c => c.ID == "1"))
                {
                    Frm_Comercio_Invisible.Cb_Moneda.Items.Add(new UI_ComboItem()
                    {
                        ID = monedaNacional.Mnd_MndCod.ToString(),
                        Value = monedaNacional.Mnd_MndNom.ToUpper(),
                        Data = monedaNacional.Mnd_MndCod
                    });
                }

                Frm_Comercio_Invisible.Cb_Moneda.ListIndex = Frm_Comercio_Invisible.Cb_Moneda.Items.Count - 1;
                Cb_Moneda_Click(initObject, unit);
                Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = false;
                Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = false;
            }
            else
            {
                MODGCVD.TIN = false;
                for (d = 0; d <= (Frm_Comercio_Invisible.Cb_Moneda.Items.Count - 1); d++)
                {
                    if (Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt(d).ID.Equals(T_MODGTAB0.MndNac.ToString()))
                    {
                        Frm_Comercio_Invisible.Cb_Moneda.Items.RemoveAt(d);
                        break;
                    }
                }

                Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = true;
                Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = true;
            }

            Frm_Comercio_Invisible.OPER_AUTOMATICA = (short)(Frm_Comercio_Invisible.OPER_AUTOMATICA + 1);

            if (Frm_Comercio_Invisible.Cb_Moneda.ListIndex != -1)
            {
                if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbc, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbv, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("2"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbc, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("3"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbv, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                }
                else if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("4"))
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(MODGTAB0.VVmd.VmdMbv, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[1]));
                }

                if (Frm_Comercio_Invisible.Lt_Tcp.ListIndex == -1)
                {
                    Frm_Comercio_Invisible.Lt_Tcp.ListIndex = 0;
                    Lt_Tcp_Click(initObject, unit);
                }
            }

        }

        public static void Tx_MtoCV_LostFocus(UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, ref short Index)
        {
            double mto = 0;
            switch (Index)
            {
                case 0:
                    break;
                case 1:
                    if (!string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) && !string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_MtoCV[1].Text))
                    {
                        mto = Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) * Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[1].Text);
                        Frm_Comercio_Invisible.Tx_MtoCV[2].Text = Format.FormatCurrency(mto, "##,###0");  //forma(mto#, FmtObjeto(Tx_MtoCV(2)))
                    }

                    if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_MtoCV[0].Text))
                    {
                        Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[0]));
                    }

                    break;
            }

            if (!Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt((short)Frm_Comercio_Invisible.Cb_Divisa.ListIndex).Equals("4"))
            {
                if (VB6Helpers.Val(Frm_Comercio_Invisible.Tx_MtoCV[3].Text) > 0)
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = false;
                }
                else
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = true;
                }

            }

        }

        public static void Cb_Moneda_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            //return;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;

            short Indice_Moneda = 0;
            short n = 0;
            double a = 0;
            short b = 0;
            if (Frm_Comercio_Invisible.Cb_Moneda.ListIndex == -1)
            {
                Frm_Comercio_Invisible.Tx_MtoCV[1].Text = "";
                return;
            }

            //Decimales para Moneda.-
            Indice_Moneda = short.Parse(Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt(Frm_Comercio_Invisible.Cb_Moneda.ListIndex).ID);
            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, Indice_Moneda);
            if (MODGTAB0.VMnd[n].Mnd_MndSin != 0)
            {
                Frm_Comercio_Invisible.Tx_MtoCV[0].Tag = "_____________";
            }
            else
            {
                Frm_Comercio_Invisible.Tx_MtoCV[0].Tag = "_____________.__";
            }

            a = MODGTAB1.SyGet_Vmc(MODGTAB0, unit, Indice_Moneda, DateTime.Now.ToString("yyyy-MM-dd"), "P");
            if (a != 0)
            {
                Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = false;
                Frm_Comercio_Invisible.Tx_MtoCV[3].Text = Format.FormatCurrency(a, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[3]));
            }
            else
            {
                Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = true;
                Frm_Comercio_Invisible.Tx_MtoCV[3].Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Comercio_Invisible.Tx_MtoCV[3]));
            }

            b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(MODGTAB0, unit, DateTime.Now.ToString("yyyy-MM-dd"), Indice_Moneda);
            if (b != 0)
            {
                Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = true;
            }
            else
            {
                Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = true;
            }

            Solo_moneda(initObject, unit);

            if (MODGCVD.TIN == true)
            {
                if (Frm_Comercio_Invisible.Cb_Moneda.ListIndex == -1)
                {
                    Frm_Comercio_Invisible.Cb_Moneda.ListIndex = 0;
                }
                if (Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt((short)Frm_Comercio_Invisible.Cb_Moneda.ListIndex).ID.Equals("1"))
                {
                    //Peso
                    Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = false;
                    Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled = false;
                    Frm_Comercio_Invisible.Tx_MtoCV[2].Enabled = false;

                    if (Frm_Comercio_Invisible.Tx_MtoCV[0].Text == null ||
                            Frm_Comercio_Invisible.Tx_MtoCV[0].Text.Trim().Equals(string.Empty))
                        Frm_Comercio_Invisible.Tx_MtoCV[0].Text = "0";
                    else
                    {
                        string[] value = Frm_Comercio_Invisible.Tx_MtoCV[0].Text.Split(new String[] { "," }, StringSplitOptions.None);
                        Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Format.FormatCurrency(Format.StringToDouble(value[0]), "##,###0");
                    }

                    Frm_Comercio_Invisible.Tx_MtoCV[2].Text = "0";
                }
                else
                {
                    Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Format.FormatCurrency(Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text), "##,###0.00");
                }

            }
            short d = 0;
            short k = 0;
            Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
            d = 1;
            Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
            d = 2;
            Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
            d = 3;
            Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);

        }

        //EN VERDAD NO SE VA A EJECUTAR EN KEYPRESS SINO EN EL BLUR PERO SE DEJO EL NOMBRE PARA TENER REFERENCIA A LA FUNCION ORIGINAL
        public static void Tx_MtoCV_KeyPress(UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, ref short Index, ref short KeyAscii)
        {
            double mto = 0;
            int divListIndex = Frm_Comercio_Invisible.Cb_Divisa.ListIndex;
            int monListIndex = Frm_Comercio_Invisible.Cb_Moneda.ListIndex;
            bool esCorrecto = false;
            string texto = Frm_Comercio_Invisible.Tx_MtoCV[Index].Text;
            int intText = 0;
            if (monListIndex != -1)
            {
                if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(divListIndex).ID.Equals("4") && Frm_Comercio_Invisible.Cb_Moneda.Items.ElementAt(monListIndex).ID.Equals("1"))
                {
                    //Mdl_Funciones_Varias.ControlaEntero(Tx_MtoCV[Index], ref KeyAscii, 20);
                    bool esEntero = int.TryParse(texto, out intText);
                    if (esEntero)
                    {
                        Frm_Comercio_Invisible.Tx_MtoCV[0].Text = Format.FormatCurrency(Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text), "##,###0");
                        return;
                    }

                }
                else
                {
                    decimal d = 0;
                    esCorrecto = decimal.TryParse(texto, out d);
                }
            }

            if (esCorrecto)
            {
                if (Index != 3)
                {
                    //Me.Tx_MtoCV(Index).Text = Format(Tx_MtoCV(Index), "##,###0.0000") MAV
                    texto = Format.FormatCurrency(Format.StringToDouble(texto), "##,###0.00");
                    if (Index == 0 && VB6Helpers.Val(Frm_Comercio_Invisible.Tx_MtoCV[1].Text) > 0)
                    {
                        if (string.IsNullOrWhiteSpace(texto))
                        {
                            Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Debe Ingresar el Monto."
                            });
                        }
                        else
                        {
                            mto = Format.StringToDouble(texto) * Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[1].Text);
                            Frm_Comercio_Invisible.Tx_MtoCV[2].Text = Format.FormatCurrency(mto, "##,###0");
                        }

                    }

                    if (Index == 1)
                    {
                        if (string.IsNullOrWhiteSpace(texto))
                        {
                            Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Debe Ingresar el tipo de cambio."
                            });
                        }
                        else
                        {
                            mto = Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[0].Text) * Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[1].Text);
                            Frm_Comercio_Invisible.Tx_MtoCV[2].Text = Format.FormatCurrency(mto, "##,###0");
                            texto = Format.FormatCurrency(Format.StringToDouble(Frm_Comercio_Invisible.Tx_MtoCV[1].Text), "##,###0.0000");
                        }

                    }

                }
                else
                {
                    texto = Format.FormatCurrency(Format.StringToDouble(texto), "##,###0.0000000000");
                }

            }
            else
            {
            }
            Frm_Comercio_Invisible.Tx_MtoCV[Index].Text = texto;
        }

        public static void Lt_Operacion_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            short index = (short)(initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.Count == 0 ? -1 : initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex);
            if (index == -1)
            {
                Pr_Descarga(initObject.Frm_Comercio_Invisible);
                initObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex = initObject.MODGCVD.AntOper;
                if ((short)initObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex != 4)
                {
                    //En el legacy no existe esta llamada, pero para no impactar este modulo no va a entrar cuando sea transferencia interna.
                    Cb_Divisa_click(initObject, unit);
                }

                //Se cambio lo que se hacia por linq
                initObject.Frm_Comercio_Invisible.Cb_Moneda.ListIndex = initObject.Frm_Comercio_Invisible.Cb_Moneda.Items.FindIndex(x => x.ID.Equals(initObject.MODGCVD.Antmon.ToString()));
                Cb_Moneda_Click(initObject, unit);
                initObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text = Format.FormatCurrency(initObject.MODGCVD.Anttip, MODGPYF1.DecObjeto(initObject.Frm_Comercio_Invisible.Tx_MtoCV[1]));  // Format(Tx_MtoCV(1), "##,###0.0000") '
            }
            else
            {
                short i = short.Parse(initObject.Frm_Comercio_Invisible.Lt_Operacion.get_ItemData(initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex));
                initObject.Frm_Comercio_Invisible.Switch = 1;
                Pr_Cargar_Datos_Operacion(initObject, unit, i);
                initObject.Frm_Comercio_Invisible.Switch = 0;
            }

        }

        public static void Co_Boton_Click(T_Module1 Module1, T_MODGRNG MODGRNG, T_MODGUSR MODGUSR, T_MODGSCE MODGSCE, T_MODGCVD MODGCVD, T_Mdl_Funciones Mdl_Funciones, T_MODGPLI1 MODGPLI1, UI_Mdi_Principal Mdi_Principal, UnitOfWorkCext01 unit, ref short Index)
        {
            short i = 0;
            switch (Index)
            {
                //Aceptar
                case 0:
                    BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.CargaPln_CVD(MODGRNG, MODGCVD, MODGPLI1, MODGUSR, Module1, MODGSCE, Mdi_Principal, unit);

                    var hayNumeroPlanilla = false;
                    var hayTransferenciaInterna = false;

                    /// Validar si existe numero de planillas
                    foreach (var item in MODGPLI1.Vplis)
                    {
                        hayNumeroPlanilla = Convert.ToInt32(item.NumPli) > 0;
                        if (hayNumeroPlanilla) break;
                    }

                    /// Validamos los tipos planilla
                    foreach (var item in MODGCVD.VgPli)
                    {
                        hayTransferenciaInterna = item.TipCVD == T_Mdl_Funciones.TransInternaDivisa;
                        if (hayTransferenciaInterna) break;
                    }

                    if (hayNumeroPlanilla || hayTransferenciaInterna)
                    {
                        Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                        MODGCVD.VgCvd.Acepto = (short)(true ? -1 : 0);
                        MODGCVD.VgCvd.Etapa = "VIAORI";
                    }
                    break;
                //Cancelar
                case 1:
                    Mdi_Principal.MESSAGES = new System.Collections.Generic.List<UI_Message>();
                    Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                    MODGCVD.VgCvd.Acepto = (short)(false ? -1 : 0);
                    break;
            }

        }

        public static void NO_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            short i = (short)initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex;
            short j = 0;
            short p = 0;

            string tipo = "";
            string moneda = "";
            double monto = 0;

            if (i >= 0)
            {
                j = short.Parse(initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.ElementAt(i).ID);
                #region Recorre listado de operaciones de la grilla:
                foreach (var item in initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.Where(x => x.ID == j.ToString()))
                {
                    foreach (var _item in item.columns)
                    {
                        if (_item.Key == "tipo")
                        {
                            tipo = _item.Value;
                            if (tipo == "Compra")
                            {
                                tipo = "C";
                            }
                            else if (tipo == "Ventas")
                            {
                                tipo = "V";
                            }
                            else if (tipo == "Transf. ingreso")
                            {
                                tipo = "TI";
                            }
                            else if (tipo == "Transf. egreso")
                            {
                                tipo = "TE";
                            }
                            else if (tipo == "Transf. interna")
                            {
                                tipo = "TIN";
                            }
                        }
                        else if (_item.Key == "moneda")
                        {
                            moneda = _item.Value;
                        }
                        else if (_item.Key == "monto")
                        {
                            monto = Convert.ToDouble(_item.Value.Replace(".", ""));
                            monto = Convert.ToDouble(monto.ToString().Replace(".", ","));
                        }
                    }
                    break;
                }
                #endregion

                //foreach (var VgPli in initObject.MODGCVD.VgPli.Where(x => x.TipCVD == tipo && x.NemMnd == moneda && x.MtoCVD ==monto && x.Status != T_MODGCVD.EstadoEli))
                //{
                initObject.MODGCVD.VgPli[j].Status = T_MODGCVD.EstadoEli;
                //    VgPli.Status = T_MODGCVD.EstadoEli;
                //}                

                if (i == initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.Count - 1)
                {
                    p = 0;
                }
                else
                {
                    p = i;
                }

                initObject.Frm_Comercio_Invisible.Lt_Operacion.Items.RemoveAt(i);
                initObject.Frm_Comercio_Invisible.Lt_Operacion.ListIndex = p;
                Lt_Operacion_Click(initObject, unit);
                Pr_Llena_Lt_Operacion(initObject, unit);
            }
        }

        public static void Tx_CodAdn_LostFocus(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, T_MODGTAB1 MODGTAB1, UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, UnitOfWorkCext01 unit)
        {
            //short a = MODGPYF0.MascaraLost(Tx_CodAdn);
            short i = 0;
            short x = 0;
            //Verifica que Aduana esté correcto.
            if (!string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_CodAdn.Text))
            {
                i = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VAdn(MODGTAB1, unit, (short)VB6Helpers.Val(Frm_Comercio_Invisible.Tx_CodAdn.Text));
                //if (i == 0)
                if (i >= 0)
                {
                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Código de Aduana no existe."
                    });
                    return;
                }

            }

            //Carga el Ide.
            x = Fn_CargaIde(Mdl_Funciones_Varias, Frm_Comercio_Invisible, unit);
        }

        public static void Tx_DocNac_LostFocus(UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible)
        {
            string s = Frm_Comercio_Invisible.Tx_DocNac.Text;
            short x = 0;
            if (!string.IsNullOrWhiteSpace(s))
            {
                x = Mdl_Funciones_Varias.Fn_ValidaAladi(Mdi_Principal, s);
            }

        }

        public static void Tx_ER_LostFocus(UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible)
        {

            if (!string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_ER.Text))
            {
                if (Frm_Comercio_Invisible.Tx_ER.Text != "E" && Frm_Comercio_Invisible.Tx_ER.Text != "R")
                {
                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Código mal ingresado"
                    });
                }

            }

        }

        public static void Tx_NroDec_LostFocus(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, UnitOfWorkCext01 unit)
        {
            short n = 0;
            string z = "";
            string s = "";
            short x = 0;
            //Verifica que si quiere abrir una declaración, esta exista.
            // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
            if (!string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NroDec.Text))
            {
                n = (short)(7 - VB6Helpers.Len(Frm_Comercio_Invisible.Tx_NroDec.Text));
                z = MODGPYF1.Zeros(n) + Frm_Comercio_Invisible.Tx_NroDec.Text;
                s = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.SyExis_xDec(unit, z, "", 0);
                if (!string.IsNullOrWhiteSpace(s))
                {
                    //TODO: REVISAR QUE ESTE BIEN EL FORMATO
                    Frm_Comercio_Invisible.Tx_FecDec.Text = VB6Helpers.Format(VB6Helpers.Trim(MODGPYF0.copiardestring(s, ";", 2)), "dd/MM/yyyy");
                    Frm_Comercio_Invisible.Tx_CodAdn.Text = VB6Helpers.Trim(MODGPYF0.copiardestring(s, ";", 3));
                }

            }

            //Carga el Ide.
            x = Fn_CargaIde(Mdl_Funciones_Varias, Frm_Comercio_Invisible, unit);
        }

        #endregion
    }
}
