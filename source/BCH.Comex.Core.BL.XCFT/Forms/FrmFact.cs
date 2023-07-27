using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class FrmFact
    {
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            
            short[] Tabs = null;
            
            //Titulo(0) = " Nro.Operación                         Nro.Factura            Nro.Reporte             Fecha Fac.                 $ Monto"
            initObj.FrmFact.Titulo[0].Text = "     Nro.Factura           Nro.Reporte           Fecha Fac.            Tipo Fac.       $ Neto                         $ Iva                        $ Total";

            //MZ Modificamenusistema FrmCta
            //initObj.MODGPYF0.Modificamenusistema(this);

            // UPGRADE_INFO (#0181): Reference to default form instance 'FrmFact' was converted to Me/this keyword.
            //T_MODGPYF0.CenterForm(this);
            Tabs = new short[2];
            Tabs[0] = 50;
            Tabs[1] = 115;

            initObj.FrmFact.Tx_NumOpe[0].Text = initObj.MODCVDIM.ope0;  //"729"
            initObj.FrmFact.Tx_NumOpe[1].Text = initObj.MODCVDIM.ope1;  //"05"
            initObj.FrmFact.Tx_NumOpe[2].Text = initObj.MODCVDIM.ope2;  //"93"
            initObj.FrmFact.Tx_NumOpe[3].Text = initObj.MODCVDIM.ope3;  //"240"
            initObj.FrmFact.Tx_NumOpe[4].Text = initObj.MODCVDIM.ope4;  //"00870"

            Pr_Buscar_Documentos(initObj);
        }

        public static void Pr_Buscar_Documentos(InitializationObject initObj)
        {

            // ReDim VPrn_cre(0)   'Inicialización del arreglo
            //         b% = SyGetn_Cre(1, Trim$(Tx_NumOpe(0).Text + Tx_NumOpe(1).Text + Tx_NumOpe(2).Text + Tx_NumOpe(3).Text + Tx_NumOpe(4).Text))
            initObj.FrmFact.L_Print_Sort = new UI_Combo();

            if (initObj.Mdl_Funciones.VPrn_cre.Length > 0)
            {
                Pr_Ordenamiento(initObj);
                Pr_Muestra_Lista_Sort(initObj);
                if (initObj.FrmFact.L_Print.ListCount > 0)
                {
                    //initObj.FrmFact.L_Print.set_Selected(0, true);
                    initObj.FrmFact.L_Print.SelectedValue = 0;
                }
                //VB6Helpers.Beep();
            }
            else
            {
                initObj.FrmFact.L_Print = new UI_Combo();
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: No existen Facturas para el número de operación ingresado.",
                    Type = TipoMensaje.Informacion,
                    Title = "Notas de Crédito"
                });

                initObj.FrmFact.bot_acep.Enabled = false;
            }
        }

        public static void Pr_Ordenamiento(InitializationObject initObj)
        {
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            //VB6Helpers.Invoke(VB6Helpers.CObj(Lista_Sort), "Clear");
            initObj.FrmFact.L_Print_Sort = new UI_Combo();
            for (i = 0; i < (short)initObj.Mdl_Funciones.VPrn_cre.Length; i++)
            {
                initObj.FrmFact.L_Print_Sort.AddItem(i, initObj.Mdl_Funciones.VPrn_cre[i].codcct.Trim() + "-" + initObj.Mdl_Funciones.VPrn_cre[i].codpro.Trim()
                                        + "-" + initObj.Mdl_Funciones.VPrn_cre[i].codesp.Trim() + "-" + initObj.Mdl_Funciones.VPrn_cre[i].codofi.Trim()
                                        + "-" + initObj.Mdl_Funciones.VPrn_cre[i].codope.Trim() + initObj.Mdl_Funciones.VPrn_cre[i].Factura.ToString("00000000")
                                        + initObj.Mdl_Funciones.VPrn_cre[i].NroRpt.ToString("00000000")
                                        );
                            
                //VB6Helpers.Invoke(VB6Helpers.CObj(Lista_Sort), "AddItem", VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[i].codcct) + "-" + VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[i].codpro) + "-" + VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[i].codesp) + "-" + VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[i].codofi) + "-" + VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[i].codope) + VB6Helpers.Format(VB6Helpers.CStr(initObj.Mdl_Funciones.VPrn_cre[i].Factura), "00000000") + VB6Helpers.Format(VB6Helpers.CStr(initObj.Mdl_Funciones.VPrn_cre[i].NroRpt), "00000000"));
                //// UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista_Sort'. Consider using the GetDefaultMember6 helper method.
                //VB6Helpers.Set(VB6Helpers.CObj(Lista_Sort), "ItemData", VB6Helpers.Invoke(VB6Helpers.CObj(Lista_Sort), "NewIndex"), i);
            }

        }

        private static void Pr_Muestra_Lista_Sort(InitializationObject initObj)
        {
            short j = 0;
            short i = 0;

            initObj.FrmFact.L_Print.Enabled = true;
            initObj.FrmFact.L_Print = new UI_Combo();
           
            for (j = 0; j < (short)initObj.FrmFact.L_Print_Sort.Items.Count(); j++)
            {
                i = (short)initObj.FrmFact.L_Print_Sort.get_ItemData_(j);
                initObj.FrmFact.L_Print.AddItem(i, Fn_Linea_Lista(i, initObj));
            }

            if (initObj.FrmFact.L_Print.Items.Count() > 0)
            {
                initObj.FrmFact.L_Print.ListIndex = 0;
            }
        }

        private static string Fn_Linea_Lista(short Indice, InitializationObject initObj)
        {
            string Paso1 = "";
            string Paso2 = "";
            string Paso3 = "";
            string Paso4 = "";
            string Paso5 = "";
            string Paso6 = "";
            string Paso7 = "";
            string Paso8 = "";
            string paso9 = "";
            string s = "";

            Paso1 = VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].codcct) + "-" + 
                VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].codpro) + "-" + 
                VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].codesp) + "-" + 
                VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].codofi) + "-" + 
                VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].codope);

            Paso2 = Format.FormatCurrency(initObj.Mdl_Funciones.VPrn_cre[Indice].Factura, T_MODGCON0.FormatoSinDec).PadLeft(14);

            //Paso3 = VB6Helpers.Trim(VB6Helpers.Str(initObj.Mdl_Funciones.VPrn_cre[Indice].NroRpt));
            //Paso3 = VB6Helpers.Right(VB6Helpers.Space(14) + VB6Helpers.Format(VB6Helpers.Str(Paso3), T_MODGCON0.FormatoSinDec), 14);
            Paso3 = Format.FormatCurrency(initObj.Mdl_Funciones.VPrn_cre[Indice].NroRpt, T_MODGCON0.FormatoSinDec).PadLeft(14);

            Paso4 = VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].FecOpe);

            //Paso5 = VB6Helpers.Trim(VB6Helpers.Str(initObj.Mdl_Funciones.VPrn_cre[Indice].monedafac));
            //Paso5 = VB6Helpers.Right(VB6Helpers.Space(2) + VB6Helpers.Format(VB6Helpers.Str(Paso5), T_MODGCON0.FormatoConDec), 2);
            Paso5 = Format.FormatCurrency(initObj.Mdl_Funciones.VPrn_cre[Indice].monedafac, T_MODGCON0.FormatoSinDec).PadLeft(2);

            Paso6 = Format.FormatCurrency(initObj.Mdl_Funciones.VPrn_cre[Indice].neto, T_MODGCON0.FormatoConDec).PadLeft(17);

            Paso7 = Format.FormatCurrency(initObj.Mdl_Funciones.VPrn_cre[Indice].iva, T_MODGCON0.FormatoConDec).PadLeft(17);

            Paso8 = Format.FormatCurrency(initObj.Mdl_Funciones.VPrn_cre[Indice].monto, T_MODGCON0.FormatoConDec).PadLeft(17);

            paso9 = VB6Helpers.Trim(initObj.Mdl_Funciones.VPrn_cre[Indice].tipofac);
            if (paso9 == "A")
            {
                paso9 = "Afecta".PadLeft(9);
            }
            if (paso9 == "E")
            {
                paso9 = "Exenta".PadLeft(9);
            }

            //--------------Retorna la Línea de la Lista-----------------
            //Paso1 es el número de operación
            //Paso2 es Factura
            //Paso3 es NroRpt
            //Paso7 es Monto

            //' s$ = Right(Paso2$, 14) + Chr$(9) + Right(Paso3$, 14) + Chr$(9) + Right(Paso4$, 10) + Chr$(9) + Right(Paso6$, 12) + Chr$(9) + Right(Paso7$, 12) + Chr$(9) + Right(Paso8$, 15) + Chr$(9) + Right(Paso9$, 12)
            if (initObj.Mdl_Funciones.VPrn_cre[Indice].iva > 0)
            {
                s = VB6Helpers.Right(Paso2, 14) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso3, 14) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso4, 10) + 
                    VB6Helpers.Chr(9) + paso9 + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso6, 17) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso7, 17) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso8, 17);
            }
            else
            {
                s = VB6Helpers.Right(Paso2, 14) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso3, 14) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso4, 10) + 
                    VB6Helpers.Chr(9) + paso9 +
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso6, 17) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso7, 17) + 
                    VB6Helpers.Chr(9) + VB6Helpers.Right(Paso8, 17);
            }

            return s;
        }

        public static void bot_acep_Click(InitializationObject initObj)
        {
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            //i% = Lista.ItemData(Lista.ListIndex)
            //   VCtaGl.NemCta = CtaCtb(i%).Cta_Nem
            //  Unload Me

            i = (short)initObj.FrmFact.L_Print.get_ItemData_((short)initObj.FrmFact.L_Print.ListIndex);

            //MZ    VCtaGl.NemCta = CtaCtb(i%).Cta_Nem
            //    VCtaGl.NemCta = VPrn(i%).FecOpe

            initObj.MODCVDIM.VNotaCreGl.NumFac = VB6Helpers.CStr(initObj.Mdl_Funciones.VPrn_cre[i].Factura);
            initObj.MODCVDIM.VNotaCreGl.FecOpe = initObj.Mdl_Funciones.VPrn_cre[i].FecOpe;
            initObj.MODCVDIM.VNotaCreGl.NumRep = VB6Helpers.CStr(initObj.Mdl_Funciones.VPrn_cre[i].NroRpt);

            initObj.MODCVDIM.VNotaCreGl.Moneda = VB6Helpers.CStr(initObj.Mdl_Funciones.VPrn_cre[i].monedafac);
            //'' VNotaCreGl.Moneda = LCase(Trim$(VMnd(CDbl(VNotaCreGl.Moneda)).Mnd_MndNom))
            //initObj.MODCVDIM.VNotaCreGl.Moneda = MODGPYF1.Minuscula(initObj.MODGTAB0.VMnd[(int)Format.StringToDouble(initObj.MODCVDIM.VNotaCreGl.Moneda)].Mnd_MndNom);  // Minuscula(VMnd(i%).Mnd_MndNom)
            initObj.MODCVDIM.VNotaCreGl.Moneda = MODGPYF1.Minuscula(initObj.MODGTAB0.VMnd.Where(c=>c.Mnd_MndCod == (short)Format.StringToDouble(initObj.MODCVDIM.VNotaCreGl.Moneda)).SingleOrDefault().Mnd_MndNom);

            //'' 17-07-2009 INGESYS
            //'' ADJ
            initObj.MODCVDIM.VNotaCreGl.monto = MODGSYB.dbmontoSyForRead(initObj.Mdl_Funciones.VPrn_cre[i].monto);
            initObj.MODCVDIM.VNotaCreGl.netofac = MODGSYB.dbmontoSyForRead(initObj.Mdl_Funciones.VPrn_cre[i].neto);
            initObj.MODCVDIM.VNotaCreGl.ivafac = MODGSYB.dbmontoSyForRead(initObj.Mdl_Funciones.VPrn_cre[i].iva);
            //'' 06-08-2009
            //'' ADJ INGESYS
            //'' se descomentario las 3 lineas anteriores y se le puso comentario a las 3 sgtes.
            //''VNotaCreGl.monto = VPrn_cre(i%).monto / 100
            //''VNotaCreGl.netofac = VPrn_cre(i%).neto / 100
            //''VNotaCreGl.ivafac = VPrn_cre(i%).iva / 100

            if (initObj.Mdl_Funciones.VPrn_cre[i].tipofac == "A")
            {
                initObj.MODCVDIM.VNotaCreGl.tipofac = "Afecta";
            }

            if (initObj.Mdl_Funciones.VPrn_cre[i].tipofac == "E")
            {
                initObj.MODCVDIM.VNotaCreGl.tipofac = "Exenta";
            }
            //Debo volver a la pantalla de Emitir Nota Credito
            //VB6Helpers.Unload(this);

            initObj.FormularioQueAbrir = "EmitirNotaCredito";
        }

    }
}
