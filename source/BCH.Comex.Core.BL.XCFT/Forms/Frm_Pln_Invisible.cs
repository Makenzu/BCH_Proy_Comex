using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Pln_Invisible
    {

        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = 0;
            short a = 0;
            short b = 0;
            short n = 0;

            initObj.Frm_Pln_Invisible.Tx_Planilla[43].Enabled = true;
            initObj.Frm_Pln_Invisible.Ch_ZonFra.Enabled = true;

            //Carga el Arreglo de Países.
            a = MODGTAB0.SyGetn_Pai(initObj.MODGTAB0, uow);

            //Carga el Arreglo de las Monedas.
            b = MODGTAB0.SyGetn_Mnd(initObj.MODGTAB0, uow);

            n = (short)(initObj.MODGPLI1.Vplis.Length);


            //Inicializa.
            for (i = 0; i < (short)n; i++)
            {
                initObj.MODGPLI1.Vplis[i].Acepto = (short)(false ? -1 : 0);
            }

            if (initObj.MODGPLI1.IndPli == 0)
            {
                initObj.Frm_Pln_Invisible.Boton[0].Tag = 0;
            }
            else
            {
                initObj.Frm_Pln_Invisible.Boton[0].Tag = initObj.MODGPLI1.IndPli;
            }


            if (n == 1)
            {
                initObj.Frm_Pln_Invisible.Atras.Enabled = false;
                initObj.Frm_Pln_Invisible.Adelante.Enabled = false;
            }

        }

        private static void Pr_Cargar_Datos(InitializationObject initObj, UnitOfWorkCext01 uow, short Indice)
        {
            short m = 0;
            string R = string.Empty;
            string s = string.Empty;
            string numdec = string.Empty;
            string n = string.Empty;
            short i = 0;
            short Texto = 0;
            short a = 0;
            string rut = string.Empty;
            string Lc_Concepto = string.Empty;

            initObj.Frm_Pln_Invisible.Tx_Planilla[0].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].CodOci), "000");
            initObj.Frm_Pln_Invisible.Tx_Planilla[1].Text = initObj.MODGPLI1.Vplis[Indice].NumPli;
            //Fecha
            initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].FecPli, "dd/MM/yyyy");
            initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text.Replace("-", "/"); 
            if (initObj.MODGPLI1.Vplis[Indice].TipPln == T_Mdl_Funciones.TPli_TranIng || initObj.MODGPLI1.Vplis[Indice].TipPln == T_Mdl_Funciones.TPli_TranEg || initObj.MODGPLI1.Vplis[Indice].TipPln == T_Mdl_Funciones.TPli_AnuTranIng || initObj.MODGPLI1.Vplis[Indice].TipPln == T_Mdl_Funciones.TPli_AnuTranEg)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[6].Enabled = true;
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[6].Enabled = false;
            }

            //-------------------------------------------
            //Codigo Plaza Banco Central que Contabiliza.
            //-------------------------------------------
            if (initObj.MODGPLI1.Vplis[Indice].PlzBcc != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[8].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].PlzBcc), "00");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[8].Text = string.Empty;
            }

            m = MODGTAB1.Get_VPbc(initObj, uow, initObj.MODGPLI1.Vplis[Indice].PlzBcc);
            initObj.Frm_Pln_Invisible.Tx_Planilla[7].Text = string.Empty;
            if (m >= 0)
              initObj.Frm_Pln_Invisible.Tx_Planilla[7].Text = VB6Helpers.Trim(VB6Helpers.UCase(initObj.MODGTAB1.VPbc[m].Pbc_PbcDes));


            //-------------------------------------------
            //Rut.
            //-------------------------------------------
            //If Trim$(Vplis(Indice).rutcli) <> string.Empty Then
            //    R$ = ConvRut(Vplis(Indice).rutcli)
            //    Tx_planilla(11).Text = Mid$(R$, 1, Len(R$) - 1) + "-" + Mid$(R$, Len(R$), 1)
            //Else
            //    Tx_planilla(11).Text = string.Empty
            //End If
            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].rutcli) != string.Empty)
            {
                R = MODXPLN1.ConvRut(initObj.MODGPLI1.Vplis[Indice].rutcli);
                initObj.Frm_Pln_Invisible.Tx_Planilla[11].Text = VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[11].Text = string.Empty;
            }

            Lc_Concepto = VB6Helpers.CStr(Mdl_Funciones_Varias.Rescata_descripcion_parametros(uow, "CONCEPTO", 0));

            //nombre del particpante
            if (initObj.MODGPLI1.Vplis[Indice].codcom + initObj.MODGPLI1.Vplis[Indice].Concep == Lc_Concepto)
            {
                if (initObj.MODGPLI1.Vplis[Indice].MtoOpe < 9999.99)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[12].Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrtn(uow, initObj.MODGPLI1.Vplis[Indice].PrtCli, initObj.MODGPLI1.Vplis[Indice].IndNom, initObj.MODGPLI1.Vplis[i].IndDir, "N", "DC"));
                }
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[12].Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrtn(uow, initObj.MODGPLI1.Vplis[Indice].PrtCli, initObj.MODGPLI1.Vplis[Indice].IndNom, initObj.MODGPLI1.Vplis[i].IndDir, "N", "DC"));
            }

            initObj.Frm_Pln_Invisible.Tx_Planilla[14].Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrtn(uow, initObj.MODGPLI1.Vplis[Indice].PrtCli, initObj.MODGPLI1.Vplis[Indice].IndNom, initObj.MODGPLI1.Vplis[i].IndDir, "D", "DC"));
            //-------------------------------------------
            //Código Tipo de Operación.
            //-------------------------------------------
            if (initObj.MODGPLI1.Vplis[Indice].TipPln != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[5].Text = VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].TipPln);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[5].Text = string.Empty;
            }

            s = initObj.MODGPLI1.Vplis[Indice].codcom;
            if (VB6Helpers.Trim(s) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[17].Text = VB6Helpers.Left(s, 2) + "." + VB6Helpers.Mid(s, 3, 2) + "." + VB6Helpers.Right(s, 2);
                initObj.Frm_Pln_Invisible.Tx_Planilla[17].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[17].Text.Remove(initObj.Frm_Pln_Invisible.Tx_Planilla[17].Text.Length - 1);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[17].Text = string.Empty;
            }
            initObj.Frm_Pln_Invisible.Tx_Planilla[18].Text = initObj.MODGPLI1.Vplis[Indice].Concep;

            //-------------------------------------------
            //Nombre Código de Comercio.
            //-------------------------------------------
            try
            {
                s = initObj.MODGPLI1.Vplis[Indice].codcom + initObj.MODGPLI1.Vplis[Indice].Concep;
                m = MODGTAB1.Get_VTcp(initObj.MODGTAB1, uow, s);
                if (m != -1)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[16].Text = initObj.MODGTAB1.VTcp[m].DesTcp;
                }
                else
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[16].Text = string.Empty;
                }
            }
            catch (Exception)
            {}

            //-------------------------------------------
            //Código Pais.
            //-------------------------------------------
            if (initObj.MODGPLI1.Vplis[Indice].codpai != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[4].Text = initObj.MODGPLI1.Vplis[Indice].codpai.ToString(); 
                
                //País.
                m = MODGTAB0.Get_VPai(initObj, uow, initObj.MODGPLI1.Vplis[Indice].codpai);
                if (m != 0)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[3].Text = VB6Helpers.Trim(VB6Helpers.UCase(initObj.MODGTAB0.VPai[m].Pai_PaiNom));
                }
                else
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[3].Text = string.Empty;
                }
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[4].Text = string.Empty;
                initObj.Frm_Pln_Invisible.Tx_Planilla[3].Text = string.Empty;
            }

            //-------------------------------------------
            //Código Moneda.
            //-------------------------------------------
            if (initObj.MODGPLI1.Vplis[Indice].CodMndBC != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].CodMndBC), "000");
                initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text.Remove(initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text.Length - 1);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text = string.Empty;
            }

            //-------------------------------------------
            //Moneda.
            //-------------------------------------------
            m = MODGTAB0.Get_VMndBC(initObj.MODGTAB0, uow, initObj.MODGPLI1.Vplis[Indice].CodMndBC);
            if (m != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[9].Text = VB6Helpers.Trim(VB6Helpers.UCase(initObj.MODGTAB0.VMnd[m].Mnd_MndNom));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[9].Text = string.Empty;
            }
            //Monto Operación:
            if (initObj.MODGPLI1.Vplis[Indice].MtoOpe != 0)
            {

                initObj.Frm_Pln_Invisible.Tx_Planilla[13].Tag = "_____________.__";
                initObj.Frm_Pln_Invisible.Tx_Planilla[13].Text = MODGPYF0.forma(initObj.MODGPLI1.Vplis[Indice].MtoOpe, MODGPYF1.FmtObjeto(initObj.Frm_Pln_Invisible.Tx_Planilla[13]));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[13].Text = string.Empty;
            }

            //Paridad Banco Chile:
            if (initObj.MODGPLI1.Vplis[Indice].Mtopar != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[15].Tag = "_______.__________";
                initObj.Frm_Pln_Invisible.Tx_Planilla[15].Text = MODGPYF0.forma(initObj.MODGPLI1.Vplis[Indice].Mtopar, MODGPYF1.FmtObjeto(initObj.Frm_Pln_Invisible.Tx_Planilla[15]));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[15].Text = string.Empty;
            }

            //Monto en Dolares
            if (initObj.MODGPLI1.Vplis[Indice].MtoDol != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[19].Tag = "_____________.__";
                initObj.Frm_Pln_Invisible.Tx_Planilla[19].Text = MODGPYF0.forma(initObj.MODGPLI1.Vplis[Indice].MtoDol, MODGPYF1.FmtObjeto(initObj.Frm_Pln_Invisible.Tx_Planilla[19]));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[19].Text = string.Empty;
            }

            //-------------------------------------------
            //Planilla Anulada.
            //-------------------------------------------
            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].AnuNum) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[20].Text = VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].AnuNum);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[20].Text = string.Empty;
            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].AnuFec) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[21].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].AnuFec, "dd/MM/yyyy");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[21].Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].AnuPbc != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[22].Text = VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].AnuPbc);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[22].Text = string.Empty;
            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].ApcTip) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[24].Text = VB6Helpers.Trim(VB6Helpers.UCase(initObj.MODGPLI1.Vplis[Indice].ApcTip));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[24].Text = string.Empty;
            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].ApcNum) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[25].Text = VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].ApcNum);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[25].Text = string.Empty;
            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].ApcFec) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[26].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].ApcFec, "dd/MM/yyyy");
                initObj.Frm_Pln_Invisible.Tx_Planilla[26].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[26].Text.Replace("-", "/");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[26].Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].ApcPbc != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[27].Text = VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].ApcPbc);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[27].Text = string.Empty;
            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].Desacu) != string.Empty)
            {
                m = MODGPYF0.cuentadestring(initObj.MODGPLI1.Vplis[Indice].Desacu, ";");
                initObj.Frm_Pln_Invisible.Tx_Planilla[47].Text = VB6Helpers.Trim(VB6Helpers.Str(m));
                Texto = 48;
                for (a = 1; a <= (short)m; a++)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[Texto].Text = VB6Helpers.Trim(MODGPYF0.copiardestring(initObj.MODGPLI1.Vplis[Indice].Desacu, ";", a));
                    Texto = (short)(Texto + 1);
                }
            }
            else
            {
                for (a = 47; a <= 52; a++)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[a].Text = string.Empty;
                }
            }

            //Tipo de Cambio-------------------------------------------
            if (initObj.MODGPLI1.Vplis[Indice].TipCam != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[23].Tag = "_______.____";
                initObj.Frm_Pln_Invisible.Tx_Planilla[23].Text = MODGPYF0.forma(initObj.MODGPLI1.Vplis[Indice].TipCam, MODGPYF1.FmtObjeto(initObj.Frm_Pln_Invisible.Tx_Planilla[23]));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[23].Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].MtoNac != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[28].Tag = "_____________.__";
                initObj.Frm_Pln_Invisible.Tx_Planilla[28].Text = MODGPYF0.forma(VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].MtoNac), "0"), MODGPYF1.FmtObjeto(initObj.Frm_Pln_Invisible.Tx_Planilla[28]));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[28].Text = string.Empty;
            }

            //-------------------------------------------
            n = initObj.MODGPLI1.Vplis[Indice].DieNum;
            if (n != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[29].Text = VB6Helpers.Mid(n, 1, VB6Helpers.Len(n) - 1) + "-" + VB6Helpers.Mid(n, VB6Helpers.Len(n), 1);
            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].DieFec) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[30].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].DieFec, "dd/MM/yyyy");
                initObj.Frm_Pln_Invisible.Tx_Planilla[30].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[30].Text.Replace("-", "/"); 
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[30].Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].DiePbc != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[31].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].DiePbc), "00");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[31].Text = string.Empty;
            }

            //-------------------------------------------
            // número de aceptación
            // ----------------------
            numdec = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].numdec, "0000000");
            if (VB6Helpers.Trim(numdec) != string.Empty && VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].FecDec) != string.Empty)
            {
                if (VB6Helpers.Trim(numdec) != string.Empty)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[34].Text = VB6Helpers.Trim(VB6Helpers.Left(numdec, 6)) + "-" + VB6Helpers.Trim(VB6Helpers.Right(numdec, 1));
                }
                else
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[34].Text = string.Empty;
                }

                //fecha de aceptación
                if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].FecDec) != string.Empty)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[35].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].FecDec, "dd/MM/yyyy");
                    initObj.Frm_Pln_Invisible.Tx_Planilla[35].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[35].Text.Replace("-", "/"); 
                }
                else
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[35].Text = string.Empty;
                }

                // Codigo aduana
                if (initObj.MODGPLI1.Vplis[Indice].CodAdn != 0)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[36].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].CodAdn), "00");
                }
                else
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[36].Text = string.Empty;
                }

                if (initObj.MODGPLI1.Vplis[Indice].CodEOR != string.Empty)
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[53].Text = initObj.MODGPLI1.Vplis[Indice].CodEOR;
                }
                else
                {
                    initObj.Frm_Pln_Invisible.Tx_Planilla[53].Text = string.Empty;
                }

            }

            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].FecDeb) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[32].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].FecDeb, "dd/MM/yyyy");
                initObj.Frm_Pln_Invisible.Tx_Planilla[32].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[32].Text.Replace("-", "/");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[32].Text = string.Empty;
            }

            initObj.Frm_Pln_Invisible.Tx_Planilla[33].Text = initObj.MODGPLI1.Vplis[Indice].DocNac;
            initObj.Frm_Pln_Invisible.Tx_Planilla[37].Text = initObj.MODGPLI1.Vplis[Indice].DocExt;
            if (initObj.MODGPLI1.Vplis[Indice].BcoExt != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[38].Text = VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].BcoExt);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[38].Text = string.Empty;
            }

            //-------------------------------------------
            initObj.Frm_Pln_Invisible.Tx_Planilla[39].Text = VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].NumCre);
            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].FecCre) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[40].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].FecCre, "dd/MM/yyyy");
                initObj.Frm_Pln_Invisible.Tx_Planilla[40].Text = initObj.Frm_Pln_Invisible.Tx_Planilla[40].Text.Replace("-", "/");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[40].Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].MndCre != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[41].Text = VB6Helpers.CStr(initObj.MODGPLI1.Vplis[Indice].MndCre);
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[41].Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].MtoCre != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[42].Text = MODGPYF0.forma(initObj.MODGPLI1.Vplis[Indice].MtoCre, "#,###,###,###,##0.00");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[42].Text = string.Empty;
            }

            //-------------------------------------------
            if (VB6Helpers.Trim(initObj.MODGPLI1.Vplis[Indice].CodAcu) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[44].Text = VB6Helpers.Format(initObj.MODGPLI1.Vplis[Indice].CodAcu, "00");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[44].Text = string.Empty;
            }

            initObj.Frm_Pln_Invisible.Tx_Planilla[45].Text = initObj.MODGPLI1.Vplis[Indice].RegAcu;

            rut = initObj.MODGPLI1.Vplis[Indice].RutAcu;
            if (VB6Helpers.Trim(rut) != string.Empty)
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[46].Text = VB6Helpers.Trim(VB6Helpers.Left(rut, 9)) + "-" + VB6Helpers.Trim(VB6Helpers.Right(rut, 1));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_Planilla[46].Text = string.Empty;
            }

            initObj.Frm_Pln_Invisible.Tx_Planilla[43].Text = initObj.MODGPLI1.Vplis[Indice].ObsPli;

            if (initObj.MODGPLI1.Vplis[Indice].ZonFra != 0)
            {
                initObj.Frm_Pln_Invisible.Ch_ZonFra.Value = 1;
            }
            else
            {
                initObj.Frm_Pln_Invisible.Ch_ZonFra.Value = 0;
            }

            if (initObj.MODGPLI1.Vplis[Indice].SecBen != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_SecBen.Text = VB6Helpers.Trim(VB6Helpers.Str(initObj.MODGPLI1.Vplis[Indice].SecBen));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_SecBen.Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].SecInv != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_SecInv.Text = VB6Helpers.Trim(VB6Helpers.Str(initObj.MODGPLI1.Vplis[Indice].SecInv));
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_SecInv.Text = string.Empty;
            }

            if (initObj.MODGPLI1.Vplis[Indice].PrcPar != 0)
            {
                initObj.Frm_Pln_Invisible.Tx_PrcPar.Text = VB6Helpers.Format(VB6Helpers.Trim(VB6Helpers.Str(initObj.MODGPLI1.Vplis[Indice].PrcPar)), "0.0");
            }
            else
            {
                initObj.Frm_Pln_Invisible.Tx_PrcPar.Text = string.Empty;
            }

        }

        public static void Form_Activate(InitializationObject initObj, UnitOfWorkCext01 uow)
        {

            short n = 0;
            short a = 0;

            n = (short)(initObj.MODGPLI1.Vplis.Length);

            //n = (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);

            a = (short)VB6Helpers.DoEvents();
            //Falta filtrar por planillas anuladas.
            if (n > 0)
            {
                Pr_Cargar_Datos(initObj, uow, (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Boton[0].Tag));
            }
        }

        private static short Fn_Valida_Campos(InitializationObject initObj, UnitOfWorkCext01 uow, short CampoInicial, short CampoFinal)
        {
            short _retValue = 0;
            short Fecha_Paso = 0;
            short a = 0;
            short i = 0;
            short m = 0;
            _retValue = 0;

            if (initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text == string.Empty)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe ingresar un valor para la fecha de la planilla.",
                    Title = "Planilla Invisible",
                    Type = TipoMensaje.Informacion
                });

                return _retValue;
            }

            //La fecha no puede ser sábado ni domingo
            Fecha_Paso = VB6Helpers.Weekday(VB6Helpers.CDate(initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text));
            if (Fecha_Paso == 1 || Fecha_Paso == 7)
            {
                //Sólo si es fin de semana
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: La Fecha no puede ser fin de semana.",
                    Title = "Planilla Invisible",
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            //La fecha no puede ser un feriado de este año
            a = MODGTAB0.Fn_Buscar_Fecha_Fer(initObj.MODGTAB0, uow, VB6Helpers.Trim(initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text));
            if (a == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: La Fecha no corresponde, porque existe como fecha feriada de este año.",
                    Title = "Planilla Invisible",
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            for (i = (short)CampoInicial; i <= (short)CampoFinal; i++)
            {
                switch (i)
                {
                    case 1:
                        if (VB6Helpers.Trim(initObj.Frm_Pln_Invisible.Tx_Planilla[4].Text) != string.Empty)
                        {
                            m = MODGTAB0.Get_VPai(initObj, uow, (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Tx_Planilla[4].Text));
                            if (m != 0)
                            {
                                initObj.Frm_Pln_Invisible.Tx_Planilla[3].Text = VB6Helpers.Trim(VB6Helpers.UCase(initObj.MODGTAB0.VPai[m].Pai_PaiNom));
                            }
                            else
                            {
                                initObj.Frm_Pln_Invisible.Tx_Planilla[3].Text = string.Empty;
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "No existe el Código del País. Modifiquelo por favor.",
                                    Title = "Planilla Invisible",
                                    Type = TipoMensaje.Informacion
                                });

                                return _retValue;
                            }
                        }

                        break;
                    case 2:
                        if (VB6Helpers.Trim(initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text) != string.Empty)
                        {
                            m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Tx_Planilla[10].Text));
                            if (m != 0)
                            {
                                initObj.Frm_Pln_Invisible.Tx_Planilla[9].Text = VB6Helpers.Trim(VB6Helpers.UCase(initObj.MODGTAB0.VMnd[m].Mnd_MndNom));
                            }
                            else
                            {
                                initObj.Frm_Pln_Invisible.Tx_Planilla[9].Text = string.Empty;
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "No Existe el Código de la Moneda. Modifiquelo por favor.",
                                    Title = "Planilla Invisible",
                                    Type = TipoMensaje.Informacion
                                });

                                return _retValue;
                            }
                        }
                        break;
                }
            }
            return 1;
        }

        public static void aceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short a = Fn_Valida_Campos(initObj, uow, 1, 1);
            short i = 0;
            if (a != 0)
            {
                i = (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Boton[0].Tag);
                initObj.MODGPLI1.Vplis[i].Acepto = (short)(true ? -1 : 0);
                initObj.MODGPLI1.Vplis[i].codpai = (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Tx_Planilla[4].Text);
                initObj.MODGPLI1.Vplis[i].ObsPli = VB6Helpers.Trim(initObj.Frm_Pln_Invisible.Tx_Planilla[43].Text);
                if ((int)initObj.Frm_Pln_Invisible.Ch_ZonFra.Value == 1)
                {
                    initObj.MODGPLI1.Vplis[i].ZonFra = (short)(true ? -1 : 0);
                }
                else
                {
                    initObj.MODGPLI1.Vplis[i].ZonFra = (short)(false ? -1 : 0);
                }

                initObj.MODGPLI1.Vplis[i].FecPli = VB6Helpers.Format(initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text, "dd/MM/yyyy");
                initObj.MODGPLI1.Vplis[i].Fecing = VB6Helpers.Format(initObj.Frm_Pln_Invisible.Tx_Planilla[6].Text, "dd/MM/yyyy");
            }

        }

        public static void adelante_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short n = 0;

            n = (short)initObj.MODGPLI1.Vplis.Length;

            if (n > 0)
            {
                initObj.Frm_Pln_Invisible.Adelante.Enabled = true;
                initObj.Frm_Pln_Invisible.Atras.Enabled = true;

                if (Format.StringToDouble((string)initObj.Frm_Pln_Invisible.Boton[0].Tag) < n)
                {
                    initObj.Frm_Pln_Invisible.Boton[0].Tag = VB6Helpers.CInt(initObj.Frm_Pln_Invisible.Boton[0].Tag) + 1;
                }

                if (Format.StringToDouble((string)initObj.Frm_Pln_Invisible.Boton[0].Tag) == n)
                {
                    initObj.Frm_Pln_Invisible.Adelante.Enabled = false;
                }

                Pr_Cargar_Datos(initObj, uow, (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Boton[0].Tag));
            }

        }

        public static void atras_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short n = 0;

            n = (short)initObj.MODGPLI1.Vplis.Length;

            //Falta filtrar por planillas anuladas.
            if (n > 0)
            {
                initObj.Frm_Pln_Invisible.Atras.Enabled = true;
                initObj.Frm_Pln_Invisible.Adelante.Enabled = true;
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Boton[0].Tag'. Consider using the GetDefaultMember6 helper method.
                if (Format.StringToDouble((string)initObj.Frm_Pln_Invisible.Boton[0].Tag) > 1)
                {
                    // UPGRADE_WARNING (#0364): Unable to assign default member of symbol 'Boton[0].Tag'. Consider using the SetDefaultMember6 helper method.
                    initObj.Frm_Pln_Invisible.Boton[0].Tag = VB6Helpers.CInt(initObj.Frm_Pln_Invisible.Boton[0].Tag) - 1;
                }

                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Boton[0].Tag'. Consider using the GetDefaultMember6 helper method.
                if (Format.StringToDouble((string)initObj.Frm_Pln_Invisible.Boton[0].Tag) == 1)
                {
                    initObj.Frm_Pln_Invisible.Atras.Enabled = false;
                }

                Pr_Cargar_Datos(initObj, uow, (short)VB6Helpers.Val(initObj.Frm_Pln_Invisible.Boton[0].Tag));
            }

        }

    }
}
