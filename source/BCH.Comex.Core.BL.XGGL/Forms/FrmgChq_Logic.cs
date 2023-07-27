using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class FrmgChq_Logic
    {
        public static void Form_Load(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            string BcxEntrada2 = "";
            object EsSolBcx = null;
            int n = 0;
            int i = 0;
            

            // javier
            FrmgChq.EsBcx = 0;
            FrmgChq.Label1.Text = "Moneda Monto Documento Generado";
            MODGCHQ.VGChq.Acepto = false.ToInt();
            FrmgChq.En_Load = true.ToInt();
            // Carga los países.
            BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.CargaEnLista_Pai(Globales,unit, FrmgChq.l_plaza);
            
            // ------------------------------------------------------------------------
            // Si no se ha entrado al Cheque/VV, se cargan los valores.
            // ------------------------------------------------------------------------
            for (i = 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    MODGCHQ.V_Chq_VVi[i].TipoDoc_t = "Vale Vista";
                }
                else
                {
                    MODGCHQ.V_Chq_VVi[i].TipoDoc_t = "Cheque";
                }
                n = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Find_Mnd(MODGCHQ.V_Chq_VVi[i].CodMon,Globales);
                MODGCHQ.V_Chq_VVi[i].nemmon = MODGTAB0.VMnd[n].Mnd_MndNmc.TrimB();
                MODGCHQ.V_Chq_VVi[i].MtoDoc_t = Format.FormatCurrency(MODGCHQ.V_Chq_VVi[i].MtoDoc, T_MODGCHQ.FormatoChq);
            }
            FrmgChq.Co_Aceptar.Enabled = false;
            // ------------------------------------------------------------------------
            // Carga la lista de Montos de los Documentos.
            // ------------------------------------------------------------------------
            FrmgChq.l_montos.Items.Clear();
            Pr_Cargar_Montos(Globales);
            // ------------------------------------------------------------------------
            // Carga la lista de Participantes.-
            // ------------------------------------------------------------------------
            FrmgChq.l_benef.Items.Clear();
            for (i = 1; i <= MODGCHQ.BenDocVal.GetUpperBound(0); i += 1)
            {
                FrmgChq.l_benef.AddItem(i, MODGCHQ.BenDocVal[i].FunBen);
            }
            // ------------------------------------------------------------------------
            FrmgChq.En_Load = false.ToInt();
            FrmgChq.l_benef.ListIndex = FrmgChq.l_benef.Items.Count - 1;     // el último.-
            l_benef_Click(Globales);
            FrmgChq.l_montos.ListIndex = 0;            
            L_Montos_Click(Globales, unit);
            // javier
            if (EsSolBcx.ToBool() && BcxEntrada2.Mid(4, 1) == "0")
            {
                FrmgChq.EsBcx = 1;
            }

            if (BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Todos_Cheq_Generados(Globales) != 0)
            {
                FrmgChq.Co_Aceptar.Enabled = true;
            }
        }

        private static void Pr_Cargar_Montos(DatosGlobales Globales)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            string Generado = "";
            string s = "";
            int i = 0;

            for (i = 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                s = MODGCHQ.V_Chq_VVi[i].nemmon + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[i].MtoDoc_t + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[i].TipoDoc_t + 9.Char();
                if (MODGCHQ.V_Chq_VVi[i].Generado != 0)
                {
                    Generado = "SI";
                }
                else
                {
                    Generado = "NO";
                }
                s = s + Generado;
                FrmgChq.l_montos.Items.Add(new UI_ListBoxItem() { Data = i, Value = s });
            }

        }

        public static void l_benef_Click(DatosGlobales Globales)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int indice_posNew = 0;
            int i = 0;
            
            i = FrmgChq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = FrmgChq.l_montos.get_ItemData(i);
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Generado != 0)
            {
                switch (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc)
                {
                    case T_MODGCHQ.DOCVAL_CHEQUE:
                        FrmgChq.Tx_Rut.Enabled = false;
                        FrmgChq.l_plaza.Enabled = true;
                        FrmgChq.l_cor.Enabled = true;
                        var aux = FrmgChq.l_plaza.ListIndex;
                        FrmgChq.l_plaza.ListIndex = FrmgChq.l_plaza.Items.FindIndex(x => x.Data == MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBanco);
                        if (FrmgChq.l_plaza.ListIndex != aux)
                        {
                            l_plaza_Click(Globales);
                        }
                        FrmgChq.l_cor.ListIndex = FrmgChq.l_cor.Items.FindIndex(x => x.Data == MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndCorres);
                        indice_posNew = FrmgChq.l_benef.Items.FindIndex(x => x.Data == MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                        if (indice_posNew == FrmgChq.l_benef.ListIndex)
                        {
                            //se saca porque no aporta nada
                            //FrmgChq.l_benef.ListIndex = MODGPYF0.PosLista(l_benef, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                            FrmgChq.Tx_Nombre.Text = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen.UCase().TrimB();
                        }
                        else
                        {
                            i = FrmgChq.l_benef.get_ItemData_(FrmgChq.l_benef.ListIndex);
                            FrmgChq.Tx_Nombre.Text = MODGCHQ.BenDocVal[i].NomBen.UCase().TrimB();
                        }
                        break;
                    case T_MODGCHQ.DOCVAL_VALVIS:
                        FrmgChq.Tx_Rut.Enabled = true;
                        var aux2 = FrmgChq.l_plaza.ListIndex;
                        FrmgChq.l_plaza.ListIndex = FrmgChq.l_plaza.Items.FindIndex(x=>x.Data==997);
                        if(aux2!= FrmgChq.l_plaza.ListIndex)
                        {
                            l_plaza_Click(Globales);
                        }
                        FrmgChq.l_plaza.Enabled = false;
                        FrmgChq.l_cor.Enabled = false;
                        indice_posNew = FrmgChq.l_benef.Items.FindIndex(x => x.Data == MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                        if (indice_posNew == FrmgChq.l_benef.ListIndex)
                        {
                            //se saca porque no aporta nada
                            //FrmgChq.l_benef.ListIndex = MODGPYF0.PosLista(l_benef, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                            FrmgChq.Tx_Nombre.Text = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen.UCase().TrimB();
                            FrmgChq.Tx_Rut.Text = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].RutTom.UCase().TrimB();
                        }
                        else
                        {
                            i = FrmgChq.l_benef.get_ItemData_(FrmgChq.l_benef.ListIndex);
                            FrmgChq.Tx_Nombre.Text = MODGCHQ.BenDocVal[i].NomBen.UCase().TrimB();
                            FrmgChq.Tx_Rut.Text = MODGCHQ.BenDocVal[i].RutTom.UCase().TrimB();
                        }
                        break;
                }
            }
            else
            {
                i = FrmgChq.l_benef.get_ItemData_(FrmgChq.l_benef.ListIndex);
                FrmgChq.Tx_Nombre.Text = MODGCHQ.BenDocVal[i].NomBen.UCase().TrimB();
                switch (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc)
                {
                    case T_MODGCHQ.DOCVAL_CHEQUE:
                        FrmgChq.Tx_Rut.Enabled = false;
                        FrmgChq.l_plaza.Enabled = true;
                        // L_Plaza.ListIndex = PosLista(L_Plaza, BenDocVal(i%).PaiBen)
                        FrmgChq.l_cor.Enabled = true;
                        break;
                    case T_MODGCHQ.DOCVAL_VALVIS:
                        FrmgChq.Tx_Rut.Enabled = true;
                        // L_Plaza.ListIndex = PosLista(L_Plaza, 997)
                        FrmgChq.l_plaza.Enabled = false;
                        FrmgChq.l_cor.Enabled = false;
                        FrmgChq.Tx_Rut.Text = MODGCHQ.BenDocVal[i].RutTom.UCase().TrimB();
                        break;
                }
            }

        }

        public static void L_Montos_Click(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int i = 0;

            FrmgChq.Tx_Rut.Enabled = false;
            i = FrmgChq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = FrmgChq.l_montos.get_ItemData(i);
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Generado != 0)
            {
                switch (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc)
                {
                    case T_MODGCHQ.DOCVAL_CHEQUE:
                        FrmgChq.Tx_Rut.Enabled = false;
                        FrmgChq.l_plaza.Enabled = true;
                        FrmgChq.l_cor.Enabled = true;
                        var aux = FrmgChq.l_plaza.ListIndex;
                        FrmgChq.l_plaza.ListIndex = FrmgChq.l_plaza.Items.FindIndex(x=>x.Data== MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBanco);
                        if(aux!= FrmgChq.l_plaza.ListIndex)
                        {
                            l_plaza_Click(Globales);
                        }
                        FrmgChq.l_cor.ListIndex = FrmgChq.l_cor.Items.FindIndex(x=>x.Data==MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndCorres);
                        break;
                    case T_MODGCHQ.DOCVAL_VALVIS:
                        FrmgChq.Tx_Rut.Enabled = true;
                        var aux2 = FrmgChq.l_plaza.ListIndex;
                        FrmgChq.l_plaza.ListIndex = FrmgChq.l_plaza.Items.FindIndex(x=>x.Data==997);
                        if (aux2 != FrmgChq.l_plaza.ListIndex)
                        {
                            l_plaza_Click(Globales);
                        }
                        FrmgChq.l_plaza.Enabled = false;
                        FrmgChq.l_cor.Items.Clear();
                        FrmgChq.l_cor.Enabled = false;
                        break;
                }
                var aux1 = FrmgChq.l_benef.ListIndex;
                FrmgChq.l_benef.ListIndex = FrmgChq.l_benef.Items.FindIndex(x=>x.Data==MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                if(aux1!= FrmgChq.l_benef.ListIndex)
                {
                    l_benef_Click(Globales);
                }
                FrmgChq.Tx_Nombre.Enabled = true;
                FrmgChq.Tx_Nombre.Text = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen.UCase().TrimB();
                FrmgChq.Tx_Rut.Text = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].RutTom.UCase().TrimB();
            }
            else
            {
                FrmgChq.l_plaza.ListIndex = -1;
                l_plaza_Click(Globales);
                FrmgChq.l_cor.Items.Clear();
                FrmgChq.Tx_Nombre.Text = "";
                FrmgChq.Tx_Nombre.Enabled = true;
                i = FrmgChq.l_benef.get_ItemData_(FrmgChq.l_benef.ListIndex);
                FrmgChq.Tx_Nombre.Text = MODGCHQ.BenDocVal[i].NomBen.UCase().TrimB();
                switch (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc)
                {
                    case T_MODGCHQ.DOCVAL_CHEQUE:
                        FrmgChq.Tx_Rut.Enabled = false;
                        FrmgChq.l_plaza.Enabled = true;
                        var aux = FrmgChq.l_plaza.ListIndex;
                        FrmgChq.l_plaza.ListIndex = FrmgChq.l_plaza.Items.FindIndex(x => x.Data == BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Fn_GetPai(MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].CodMon));
                        if (FrmgChq.l_plaza.ListIndex != aux)
                        {
                            l_plaza_Click(Globales);
                        }
                        FrmgChq.l_cor.Enabled = true;
                        break;
                    case T_MODGCHQ.DOCVAL_VALVIS:
                        FrmgChq.Tx_Rut.Enabled = true;
                        var aux2 = FrmgChq.l_plaza.ListIndex;
                        FrmgChq.l_plaza.ListIndex = FrmgChq.l_plaza.Items.FindIndex(x => x.Data == 997);
                        if (FrmgChq.l_plaza.ListIndex != aux2)
                        {
                            l_plaza_Click(Globales);
                        }
                        FrmgChq.Tx_Rut.Text = MODGCHQ.BenDocVal[i].RutTom.UCase().TrimB();
                        FrmgChq.l_plaza.Enabled = false;
                        FrmgChq.l_cor.Items.Clear();
                        FrmgChq.l_cor.Enabled = false;
                        break;
                }

            }

        }

        public static void l_plaza_LostFocus(DatosGlobales Globales)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int i = 0;

            if (FrmgChq.En_Load != 0)
            {
                return;
            }
            if (FrmgChq.l_plaza.ListIndex == -1)
            {
                return;
            }

            i = FrmgChq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = FrmgChq.l_montos.get_ItemData(i);
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == "VV")
            {
                return;
            }

            // Determia si al Beneficiario le estoy pagando en su país.
            FrmgChq.Plaza_Pago = FrmgChq.l_plaza.get_ItemData_(FrmgChq.l_plaza.ListIndex);
        }
        public static void l_plaza_Click(DatosGlobales Globales)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int i = 0;

            FrmgChq.Co_Generar.Enabled = true;

            i = FrmgChq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = FrmgChq.l_montos.get_ItemData(i);
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            i = FrmgChq.l_montos.ListIndex + 1;
            if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
            {
                return;
            }
            FrmgChq.l_cor.Items.Clear();
            if (FrmgChq.l_plaza.ListIndex != -1)
            {
                if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == "VV")
                {
                    return;
                }
                // Determia si al Beneficiario le estoy pagando en su país.
                FrmgChq.Plaza_Pago = FrmgChq.l_plaza.get_ItemData_(FrmgChq.l_plaza.ListIndex);
                Pr_Carga_Nomina(Globales);
            }

            if (FrmgChq.l_cor.ListIndex == -1)
            {
                FrmgChq.Co_Generar.Enabled = false;
            }

        }

        private static void Pr_Carga_Nomina(DatosGlobales Globales)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int n = 0;
            int i = 0;
            int p = 0;

            p = FrmgChq.Plaza_Pago;
            i = FrmgChq.l_montos.ListIndex + 1;
            FrmgChq.l_cor.Items.Clear();

            // Busca en Nómina, sólo los Bancos Activos y no Aladi.-
            bool argTemp1 = false;
            n = MODGCOR.Filtra_Cor(Globales, p, MODGCHQ.V_Chq_VVi[i].CodMon, ref argTemp1, false.ToInt(), FrmgChq.l_cor);
            if (MODGCHQ.V_Chq_VVi[FrmgChq.l_montos.get_ItemData(FrmgChq.l_montos.ListIndex)].Generado != 0)
            {
                FrmgChq.l_cor.ListIndex = FrmgChq.l_cor.Items.FindIndex(x=>x.Data== MODGCHQ.V_Chq_VVi[FrmgChq.l_montos.get_ItemData(FrmgChq.l_montos.ListIndex)].IndCorres);
            }
            else
            {
                if (FrmgChq.l_cor.Items.Count > 0)
                {
                    FrmgChq.l_cor.ListIndex = FrmgChq.l_cor.Items.FindIndex(x => x.Data == n);
                    if (FrmgChq.l_cor.ListIndex == -1)
                    {
                        FrmgChq.l_cor.ListIndex = 0;
                    }
                }
            }
        }

        public static void Co_Generar_Click(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            UI_FrmgChq FrmgChq = Globales.FrmgChq;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MODGRNG MODGRNG = Globales.MODGRNG;
            T_MODGUSR MODGUSR = Globales.MODGUSR;

            double q = 0.0;
            int j = 0;
            int Ind_Cor = 0;
            string s = "";
            int Ind_Bco = 0;
            int Ind_Ben = 0;
            int i = 0;
            bool EsValeVista = false;
            bool OtroBenef = false;

            OtroBenef = false;
            EsValeVista = false;

            i = FrmgChq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = FrmgChq.l_montos.get_ItemData(i).ToInt();
            }
            else
            {
                MODGCHQ.Indice = 0;
            }


            if (FrmgChq.l_benef.ListIndex == FrmgChq.l_benef.Items.Count - 1)
            {
                OtroBenef = true;
            }
            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
            {
                EsValeVista = true;
            }

            // Validaciones para emisión de los documentos.
            if (OtroBenef && FrmgChq.Tx_Nombre.Text.TrimB() == "")
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Debe Ingresar Nombre del Beneficiario para emitir el Documento.",
                    Title = T_MODGCHQ.MsgDocVal,
                    Type =TipoMensaje.Error,
                    ControlName = "Tx_Nombre"
                });
                FrmgChq.Tx_Nombre.Enabled = true;
                return;
            }
            if (OtroBenef && EsValeVista && FrmgChq.Tx_Rut.Text.TrimB() == "")
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Debe Ingresar Rut del Beneficiario para emitir el Vale Vista.",
                    Title = T_MODGCHQ.MsgDocVal,
                    Type = TipoMensaje.Error,
                    ControlName = "Tx_Rut"
                });
                FrmgChq.Tx_Rut.Enabled = true;
                return;
            }
            // Orieta
            if (FrmgChq.l_plaza.ListIndex == -1)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Debe Elegir una Plaza de Pago para emitir el Documento.",
                    Title = T_MODGCHQ.MsgDocVal,
                    Type = TipoMensaje.Error,
                    ControlName = "l_plaza"
                });
                FrmgChq.l_plaza.Enabled = true;
                return;
            }

            // ------------------------------------------------------------------------
            // Datos Beneficiario.
            // ------------------------------------------------------------------------

            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == T_MODGCHQ.DOCVAL_CHEQUE)
            {
                Ind_Ben = FrmgChq.l_benef.get_ItemData_(FrmgChq.l_benef.ListIndex).ToInt();
            }
            else if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
            {
                Ind_Ben = FrmgChq.l_benef.get_ItemData_(FrmgChq.l_benef.Items.Count - 1).ToInt();
            }

            Ind_Bco = FrmgChq.l_plaza.get_ItemData_(FrmgChq.l_plaza.ListIndex).ToInt();
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef = Ind_Ben;
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBanco = Ind_Bco;

            // Orieta
            FrmgChq.Nombre_Ben = FrmgChq.Tx_Nombre.Text.TrimB();
            FrmgChq.Rut_Ben = FrmgChq.Tx_Rut.Text.TrimB();
            FrmgChq.Nombre_Cli = SYGETPRT.PartysOpe[MODGCHQ.DocVals.I_Clte].NombreUsado.TrimB();
            // ------------------------------------------------------------------------
            if (!EsValeVista)
            {
                // Busca datos del Banco Corresponsal para Cheques.
                s = FrmgChq.l_cor.Items[FrmgChq.l_cor.ListIndex].Value;

                // Orieta
                if (FrmgChq.l_cor.ListIndex != -1)
                {
                    Ind_Cor = FrmgChq.l_cor.get_ItemData(FrmgChq.l_cor.ListIndex).ToInt();
                    MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndCorres = Ind_Cor;
                }
                // Orieta

                FrmgChq.Swift_Corresponsal = MODGPYF0.copiardestring(s, "\xA0", 1).TrimB();
                FrmgChq.Swift_Corresponsal = MODGPYF0.copiardestring(FrmgChq.Swift_Corresponsal, "\t", 1).TrimB(); // en el segundo paso por aqui se transforma el texto con un \t por lo que se lo quitamos.
                i = MODGCOR.Find_Cor(Globales, FrmgChq.Swift_Corresponsal);     // para buscar dir.
                j = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Find_Nom(Globales, FrmgChq.Swift_Corresponsal, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].CodMon);     // para buscar cta.
                                                                                                        // Datos exclusivos para el Cheque.

                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomPag = MODGTAB0.VCor[i].Cor_Nom;
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].SwfPag = MODGTAB0.VCor[i].Cor_Swf;
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].DirPag = MODGTAB0.VCor[i].Cor_Dir.UCase();
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].CiuPag = MODGTAB0.VCor[i].Cor_Ciu.UCase();
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].PaiPag = MODGTAB0.VCor[i].Cor_Pai.UCase();
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].BcoPag = MODGTAB0.VNom[j].Nom_Bco;
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NumCta = MODGTAB0.VNom[j].Nom_cta;
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].ChqEmi = MODGTAB0.VNom[j].Nom_Emi;
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomCli = FrmgChq.Nombre_Cli;
            }
            else
            {
                // Dato exclusivo para el Vale Vista.
                q = BCH.Comex.Core.BL.XGGL.Modulos.MODGRNG.LeeSceRng(Globales,unit, T_MODGRNG.Rng_ValVis);
                if (q == -1)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "No se pudo obtener el Número de Folio del Vale Vista. Reporte este problema inmediatamente y cancele esta operación.",
                        Title = T_MODGCHQ.MsgDocVal,
                        Type = TipoMensaje.Error
                    });
                    return;
                }
                MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Folio = q.ToInt();
            }
            // ------------------------------------------------------------------------
            // Datos para Cheques y Vales Vistas.
            // ------------------------------------------------------------------------
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NroOpe = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Referencia(Globales);
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].SupUsr = MODGUSR.UsrEsp.Id_Super;
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].CCosto = MODGUSR.UsrEsp.CostoSuper;
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NroPro = MODGCHQ.DocVals.CodPro;
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen = FrmgChq.Nombre_Ben;
            // V_Chq_Vvi(indice%).RutTom = SoloNumeros(PartysOpe(ITom).Rut, 10)
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].RutTom = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.SoloNumeros(FrmgChq.Tx_Rut.Text, 10);
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomTom = SYGETPRT.PartysOpe[MODGCHQ.ITom].NombreUsado;
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].MtoDoc = Format.StringToDouble(MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].MtoDoc_t);
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NroSiu = MODGPYF0.Siu(Globales);
            // ------------------------------------------------------------------------
            if (!EsValeVista)
            {
                // Genera Datos del Cheque.
                s = "";
                s = s + BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Referencia(Globales) + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].nemmon + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].MtoDoc_t + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomPag + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].DirPag + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].SwfPag + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].CiuPag + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].PaiPag + 9.Char();
                // Siempre tengo que tener cuenta en el Banco a través del cual pago el Cheque.
                j = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Find_Nom(Globales,FrmgChq.Swift_Corresponsal, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].CodMon);
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NumCta + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomCli + 9.Char();
                s = s + MODGCHQ.DocVals.ProChq + 9.Char();
                s = s + BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Referencia(Globales).Mid(6, 2) + 9.Char();
            }
            else
            {
                // Genera Datos del Vale Vista.
                s = "";
                s = s + BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Referencia(Globales) + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].MtoDoc_t + 9.Char();
                s = s + MigrationSupport.Utils.Format(MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Folio, "000000") + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen + 9.Char();
                s = s + MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].RutTom + 9.Char();
            }
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Documento = s;
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Pinchado = false.ToInt();
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Generado = true.ToInt();
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].FecEmi = MigrationSupport.Utils.Format(DateTime.Now, "dd/mm/yyyy");
            // ------------------------------------------------------------------------
            // 
            // Se marca el Cheque como ya generado.
            LineaChq(Globales, MODGCHQ.Indice - 1, FrmgChq.l_montos);
            FrmgChq.Co_Generar.Enabled = false;
            FrmgChq.l_cor.Enabled = true;

            // l_montos.SetFocus
            // If l_montos.ListIndex <> l_montos.ListCount - 1 Then
            //     l_montos.ListIndex = l_montos.ListIndex + 1
            // Else
            //     l_montos.ListIndex = 0
            // End If

            BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Pr_No_Generado_Chq(Globales, FrmgChq.l_montos);
            L_Montos_Click(Globales, unit);
            if (BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Todos_Cheq_Generados(Globales) != 0)
            {
                FrmgChq.Co_Aceptar.Enabled = true;
            }
        }

        // Asigna una linea en la lista de Cheques/ValesVistas.
        private static void LineaChq(DatosGlobales Globales, int Indice, UI_ListBox Lista)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            UI_FrmgChq FrmgChq = Globales.FrmgChq;

            string s = "";
            int i = 0;

            i = Indice + 1;
            s = MODGCHQ.V_Chq_VVi[i].nemmon + 9.Char() + MODGCHQ.V_Chq_VVi[i].MtoDoc_t + 9.Char() + MODGCHQ.V_Chq_VVi[i].TipoDoc_t + 9.Char() + "SI";
            if (Indice > Lista.Items.Count.ToInt())
            {
                FrmgChq.l_montos.Items.Add(new UI_ListBoxItem() { Data = Lista.Items.Count, Value = s });
            }
            else
            {
                FrmgChq.l_montos.Items[Indice].Value = s;
            }

        }

        public static void Co_Aceptar_Click(DatosGlobales Globales)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            string BcxEntrada2 = "";
            object EsSolBcx = null;

            MODGCHQ.DocVals.AceptoChq = true.ToInt();
            MODGCHQ.VGChq.Acepto = true.ToInt();

            //  javier
            if (EsSolBcx.ToBool() && BcxEntrada2.Mid(4, 1) == "0")
            {
                MODGLORI.CambiaBcxEntrada("C");
            }

            //Globales.FrmgChq = null;
        }

        public static void Co_Cancelar_Click(DatosGlobales Globales)
        {

            // Verifica si por lo menos un Cheque ha sido generado.
            if (MODGCHQ.Existe_Generado_Chq(Globales) != 0)
            {
                if (Globales.vieneDeMsg)
                {
                    if (Globales.resMsg)
                    {
                        Globales.MODGCHQ.V_Chq_VVi = new Chq_Vvi[1] { new Chq_Vvi() };
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text= "Existen documentos generados que se perderán al cancelar esta ventana. ¿ Desea realmente cancelarla ? ",
                        Type=TipoMensaje.YesNo
                    });
                    return;
                }
            }
            else
            {
                if (Globales.vieneDeMsg)
                {
                    if (Globales.resMsg)
                    {
                        Globales.MODGCHQ.V_Chq_VVi = new Chq_Vvi[1] { new Chq_Vvi() };
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "¿ Desea realmente cancelar esta ventana ?",
                        Type = TipoMensaje.YesNo
                    });
                    return;
                }
            }

        }
    }
}
