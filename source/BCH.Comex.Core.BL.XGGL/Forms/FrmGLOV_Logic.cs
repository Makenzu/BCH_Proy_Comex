using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class FrmGLOV_Logic
    {
        public static void Form_Load(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODCONGL MODCONGL = Globales.MODCONGL;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            UI_gl gl = Globales.gl;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            int iNewIndex = 0;
            string Mon = "";
            int m = 0;
            int b = 0;

            // ----------------------------
            b = MODGOVD.SyGetn_Suc(Globales,unit);
            Pr_RecibeDatos(Globales,unit);
            Pr_Titulo_Mensajes(Globales);
            // ----------------------------

            if (MODCONGL.Cuentas.Length > 1)
            {
                FrmGLOV.L_Cuentas.Enabled = true;
                int codMonedaSeleccionada = gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt();

                List<T_CtasCtes> cuentasMonedaSeleccionada = MODCONGL.Cuentas.Where(x => x.Moneda == codMonedaSeleccionada).ToList();
                m = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VMnd(Globales, unit, codMonedaSeleccionada);
                Mon = MODGTAB0.VMnd[m].Mnd_MndNom;
                iNewIndex = FrmGLOV.Cb_Mnd.Items.Count;
                FrmGLOV.Cb_Mnd.Items.Add(new UI_ComboItem() { Value = Mon, Data = codMonedaSeleccionada });
                FrmGLOV.Cb_Mnd.ListIndex = 1;
                FrmGLOV.Tx_Mto.Text = gl.monto.Text;
                
                for (int i = 0; i < cuentasMonedaSeleccionada.Count; i++)
                {
                    T_CtasCtes cta = cuentasMonedaSeleccionada[i];
                    FrmGLOV.L_Cuentas.Items.Add(new UI_ComboItem() { Data= i, Value= cta.Cuenta });
                }
            }
        }

        // Se encarga de recibir los datos adicionales para la cuenta especificada.-
        private static void Pr_RecibeDatos(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            int j = 0;
            int m = 0;
            int i = 0;

            // ----------------------------
            // Inicializa.
            switch (MODGLOV.VgOV.TipoOV)
            {
                case "V":
                    FrmGLOV.Text = "Destino de Fondos";
                    break;
                case "O":
                    FrmGLOV.Text = "Origen de Fondos";
                    break;
                default:
                    FrmGLOV.Text = "";
                    break;
            }

            for (i = 0; i <= 3; i += 1)
            {
                FrmGLOV.Lb_Datos[i].Text = "";
                FrmGLOV.Tx_Datos[i].Visible = false;
            }

            FrmGLOV.Lb_Oficina.Visible = false;
            FrmGLOV.L_Cuentas.Enabled = false;

            // Escondemos el label de informacion
            FrmGLOV.Lb_Info.Visible = false;
            FrmGLOV.Lb_Info.Text = string.Empty;

            m = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VMnd(Globales,unit, MODGLOV.VgOV.codmnd);
            MODGLOV.VgOV.NemMnd = MODGTAB0.VMnd[m].Mnd_MndNmc;
            MODGLOV.VgOV.NomMnd = MODGTAB0.VMnd[m].Mnd_MndNom;

            FrmGLOV.Cb_Mnd.Enabled = false;
            FrmGLOV.Cb_Mnd.Items.Clear();
            FrmGLOV.Cb_Mnd.Items.Add(new UI_ComboItem() { Value= MODGPYF1.Minuscula(MODGLOV.VgOV.NomMnd), Data= FrmGLOV.Cb_Mnd.Items.Count });
            // ----------------------------
            // Carga los datos adicionales que vienen desde la pantalla anterior.
            // ----------------------------
            if (FrmGLOV.Cb_Mnd.Items.Count > 0)
            {
                FrmGLOV.Cb_Mnd.ListIndex = 0;
            }
            FrmGLOV.Tx_Mto.Text = Format.FormatCurrency(MODGLOV.VgOV.MtoMnd, "0.00");
            FrmGLOV.Tx_NomCta.Text = MODGLOV.VgOV.NomCta.TrimB();
            // ----------------------------
            switch (MODGLOV.VgOV.Id_Cta)
            {
                // ----------------------------
                // Cuenta Corriente M/N : Origen y Via.-
                case T_MODGCON0.IdCta_CtaCteMN:
                    FrmGLOV.L_Cuentas.Enabled = true;
                    // For k% = 0 To UBound(Beneficiario)
                    //     If InStr(VgxOri.Partys, Trim$(Str$(k%))) <> 0 Then
                    //         L_Partys.AddItem Beneficiario(k%)
                    //         L_Partys.ItemData(L_Partys.NewIndex) = k%
                    //     End If
                    // Next
                    // If L_Partys.ListCount > 0 Then L_Partys.ListIndex = 0
                    // ----------------------------
                    // Cuenta Corriente M/E : Origen y Via.-
                    break;
                case T_MODGCON0.IdCta_CtaCteME:
                case T_MODGCON0.IdCta_ChqCCME:
                    // L_Partys.Enabled = True
                    FrmGLOV.L_Cuentas.Enabled = true;
                    // For k% = 0 To UBound(Beneficiario)
                    //     If InStr(VgxOri.Partys, Trim$(Str$(k%))) <> 0 Then
                    //         L_Partys.AddItem Beneficiario(k%)
                    //         L_Partys.ItemData(L_Partys.NewIndex) = k%
                    //     End If
                    // Next
                    // If L_Partys.ListCount > 0 Then L_Partys.ListIndex = 0
                    // ----------------------------
                    // Saldos c/ Sucursales M/N y M/E : Origen y Via.-
                    break;
                case T_MODGCON0.IdCta_SCSMN:
                case T_MODGCON0.IdCta_SCSME:
                    for (j = 0; j <= 2; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Oficina.Visible = true;
                    FrmGLOV.Lb_Oficina.Text = "";
                    FrmGLOV.Lb_Datos[0].Text = "Código Oficina";
                    FrmGLOV.Lb_Datos[1].Text = "Tipo Movimiento";
                    FrmGLOV.Lb_Datos[2].Text = "Número de Partida";
                    FrmGLOV.Tx_Datos[0].MaxLength = 3;
                    FrmGLOV.Tx_Datos[1].MaxLength = 1;
                    FrmGLOV.Tx_Datos[2].MaxLength = 8;
                    // ----------------------------
                    // Cheque M/E Emi. x B. Chile : Origen.-
                    break;
                case T_MODGCON0.IdCta_CHMEBCH:
                case T_MODGCON0.IdCta_CHMEOBC:
                    for (j = 0; j <= 1; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Swift";
                    FrmGLOV.Lb_Datos[1].Text = "Nº de Cheque";
                    FrmGLOV.Tx_Datos[0].MaxLength = 11;
                    FrmGLOV.Tx_Datos[1].MaxLength = 15;
                    // ----------------------------
                    // Vale Vista Otro Banco : Origen.-
                    break;
                case T_MODGCON0.IdCta_VVOB:
                    for (j = 0; j <= 1; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Swift";
                    FrmGLOV.Lb_Datos[1].Text = "Nº de Vale Vista";
                    FrmGLOV.Tx_Datos[0].MaxLength = 11;
                    FrmGLOV.Tx_Datos[1].MaxLength = 20;
                    // ----------------------------
                    // Cta. Cte. BC/Ord/Divisas : Origen y Via.-
                    break;
                case T_MODGCON0.IdCta_CTACTEBC:
                case T_MODGCON0.IdCta_CTAORD:
                case T_MODGCON0.IdCta_DIVENPEN:
                case T_MODGCON0.IdCta_CHVBNYM:
                    for (j = 0; j <= 1; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Swift";
                    FrmGLOV.Lb_Datos[1].Text = "Nº de Referencia";
                    FrmGLOV.Tx_Datos[0].MaxLength = 11;
                    FrmGLOV.Tx_Datos[1].MaxLength = 15;
                    // ----------------------------
                    // VAE Import/Export/Mº Corredores : Origen y Via.-
                    break;
                case T_MODGCON0.IdCta_VAM:
                case T_MODGCON0.IdCta_VAX:
                case T_MODGCON0.IdCta_VAMC:
                    for (j = 0; j <= 0; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Participante";
                    FrmGLOV.Tx_Datos[0].MaxLength = 12;
                    FrmGLOV.Lb_Oficina.Visible = true;
                    FrmGLOV.Lb_Oficina.Text = "";
                    // ----------------------------
                    // Vale Vista Bco. Chile : Origen.-
                    break;
                case T_MODGCON0.IdCta_VVBCH:
                    for (j = 0; j <= 1; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Código Oficina";
                    FrmGLOV.Lb_Datos[1].Text = "Nº Vale Vista";
                    FrmGLOV.Tx_Datos[0].MaxLength = 3;
                    FrmGLOV.Tx_Datos[1].MaxLength = 20;
                    FrmGLOV.Lb_Oficina.Visible = true;
                    FrmGLOV.Lb_Oficina.Text = "";
                    // ----------------------------
                    // Orden de Pago Convenio : Origen.-
                    break;
                case T_MODGCON0.IdCta_OPC:
                    for (j = 0; j <= 1; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Swift";
                    FrmGLOV.Lb_Datos[1].Text = "Nº Reembolso";
                    FrmGLOV.Tx_Datos[0].MaxLength = 15;
                    FrmGLOV.Tx_Datos[1].MaxLength = 0;
                    // ----------------------------
                    // Orden de Pago Otros Países : Origen.-
                    break;
                case T_MODGCON0.IdCta_OPOP:
                    for (j = 0; j <= 2; j += 1)
                    {
                        FrmGLOV.Tx_Datos[j].Text = "";
                        FrmGLOV.Tx_Datos[j].Visible = true;
                        FrmGLOV.Lb_Datos[j].Visible = true;
                    }
                    FrmGLOV.Lb_Datos[0].Text = "Swift";
                    FrmGLOV.Lb_Datos[1].Text = "Cta. Corriente";
                    FrmGLOV.Lb_Datos[2].Text = "Nº Referencia";
                    FrmGLOV.Tx_Datos[0].MaxLength = 15;
                    FrmGLOV.Tx_Datos[1].MaxLength = 20;
                    FrmGLOV.Tx_Datos[2].MaxLength = 20;
                    break;
                default:
                    // Validamos si es una obligacion
                    if (unit.SceRepository.EsObligacion(MODGLOV.VgOV.Id_Cta))
                    {
                        for (j = 0; j <= 3; j += 1)
                        {
                            if (j != 2) // para saltarme el número de partida
                            {
                                FrmGLOV.Tx_Datos[j].Text = "";
                                FrmGLOV.Tx_Datos[j].Visible = true;
                                FrmGLOV.Lb_Datos[j].Visible = true;
                            }
                        }
                        FrmGLOV.Lb_Datos[0].Text = "Swift";
                        FrmGLOV.Lb_Datos[1].Text = "Nº de Referencia";
                        FrmGLOV.Lb_Datos[3].Text = "Fecha Vencimiento";
                        FrmGLOV.Tx_Datos[0].MaxLength = 11;
                        FrmGLOV.Tx_Datos[1].MaxLength = 15;
                        FrmGLOV.Tx_Datos[3].MaxLength = 10;
                        FrmGLOV.Tx_Datos[3].Text = DateTime.Now.ToString("dd/MM/yyyy");
                        // obtenemos los rangos
                        tbl_sce_tabcomex_num_s01_MS_Result rangosCtaCorrienteObligacion = unit.SceRepository.GetRangosCtaCorrienteObligaciones();
                        FrmGLOV.Lb_Info.Text = string.Format("El nemónico ingresado corresponde a una Obligación debido a que el rango de la cuenta está entre {0}.XX.XX-X y {1}.XX.XX-X, por lo que los campos Swift y Fecha de Vencimiento son de carácter obligatorio.", rangosCtaCorrienteObligacion.rng_min, rangosCtaCorrienteObligacion.rng_max);
                        FrmGLOV.Lb_Info.Visible = true;
                    }
                    break;
            }

        }
        private static void Pr_Titulo_Mensajes(DatosGlobales Globales)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            switch (MODGLOV.VgOV.TipoOV)
            {
                case "V":
                    MODGLORI.Titulo = "Destino de Fondos";
                    break;
                case "O":
                    MODGLORI.Titulo = "Origen de Fondos";
                    break;
                default:
                    MODGLORI.Titulo = "";
                    break;
            }
            Globales.FrmGLOV.Fr_DatAdic.Caption = MODGLORI.Titulo;

        }

        public static void Tx_Datos_KeyPress(DatosGlobales Globales,UnitOfWorkCext01 unit, int Index)
        {
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MODGNCTA MODGNCTA = Globales.MODGNCTA;

            UI_TextBox Tx_Datos;
            Tx_Datos = FrmGLOV.Tx_Datos[Index];

            string Texto = "";
            int b = 0;
            string razon = "";
            string a = "";

            
            if (Index == 1)
            {
                // Verifica que Movimiento sea 1 ó 2.-
                if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_SCSMN || MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_SCSME)
                {
                    if (FrmGLOV.Tx_Datos[1].Text != "1" && FrmGLOV.Tx_Datos[1].Text != "2" && FrmGLOV.Tx_Datos[1].Text != "3" && FrmGLOV.Tx_Datos[1].Text != "4")
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Text= "Los Tipos de Movimiento Deben ser 1 ó 2.",
                            Title= "Contabilidad Genérica",
                            Type=TipoMensaje.Error,
                            ControlName = "Tx_Datos_000"
                        });
                        return;
                    }
                }
                
                if (FrmGLOV.Tx_Datos[1].Text == "1" || FrmGLOV.Tx_Datos[1].Text == "3")
                {
                    FrmGLOV.Tx_Datos[2].Enabled = false;
                }
                if (FrmGLOV.Tx_Datos[1].Text == "2" || FrmGLOV.Tx_Datos[1].Text == "4")
                {
                    FrmGLOV.Tx_Datos[2].Enabled = true;
                    FrmGLOV.Tx_Datos[2].Text = "";
                }
            }

            switch (Index)
            {
                case 0:
                    if (FrmGLOV.Tx_Datos[0].Text.TrimB() != "")
                    {
                        if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_SCSMN || MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_SCSME)
                        {
                            if (MigrationSupport.Utils.IsNumeric(FrmGLOV.Tx_Datos[0].Text))
                            {
                                a = MODGOVD.Fn_Buscar_Suc(Globales, FrmGLOV.Tx_Datos[0].Text.TrimB());
                                if (a.TrimB() != "")
                                {
                                    if (FrmGLOV.Tx_Datos[0].Text.TrimB() != MODGUSR.UsrEsp.CentroCosto)
                                    {
                                        FrmGLOV.Lb_Oficina.Text = a;
                                    }
                                }
                                else
                                {
                                    FrmGLOV.Lb_Oficina.Text = "";
                                }
                            }
                            else
                            {
                                FrmGLOV.Lb_Oficina.Text = "";
                            }
                        }
                        // Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                        if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_VAM || MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_VAX || MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_VAMC)
                        {
                            razon = MODGOVD.SyGet_Partys(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB());
                            FrmGLOV.Lb_Oficina.Text = razon.TrimB();
                            if (razon.TrimB() == "")
                            {
                                FrmGLOV.Lb_Oficina.Text = "";
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text= "Participante no se encuentra registrado.",
                                    Title= MODGLORI.Titulo,
                                    Type=TipoMensaje.Error,
                                    ControlName = "Tx_Datos_000"
                                });
                                return;
                            }
                        }
                        // Vale Vista Bco. Chile
                        if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_VVBCH)
                        {
                            if (MigrationSupport.Utils.IsNumeric(FrmGLOV.Tx_Datos[0].Text.TrimB()))
                            {
                                a = MODGOVD.Fn_Buscar_Suc(Globales, FrmGLOV.Tx_Datos[0].Text.TrimB());
                                if (a.TrimB() != "")
                                {
                                    FrmGLOV.Lb_Oficina.Text = a;
                                }
                                else
                                {
                                    FrmGLOV.Lb_Oficina.Text = "";
                                }
                            }
                        }
                        // Cuenta Corriente Corresponsal.-
                        if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_OPOP)
                        {
                            b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
                            //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
                            if (b == -1)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text= "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted está ingresando.",
                                    Type=TipoMensaje.Error,
                                    Title= T_MODGLORI.MsgxOri,
                                    ControlName = "Tx_Datos_000"
                                });
                                return;
                            }
                            if (MODGTAB0.VNom[b].Nom_Ala == 1)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "El Banco que Usted acaba de ingresar es Aladi. Debe ingresar un Banco Corresponsal.",
                                    Type = TipoMensaje.Error,
                                    Title = T_MODGLORI.MsgxOri,
                                    ControlName = "Tx_Datos_000"
                                });
                                return;
                            }
                            if (FrmGLOV.Tx_Datos[1].Text == "")
                            {
                                FrmGLOV.Tx_Datos[1].Text = MODGTAB0.VNom[b].Nom_cta;
                            }
                        }
                        // Otro Nemónico M/N --- Otro Nemónico M/E.
                        if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_ONMN || MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_ONME)
                        {
                            // Otro Nemónico M/N ---- Otro Nemónico M/E
                            b = BCH.Comex.Core.BL.XGGL.Modulos.MODGNCTA.Get_Cta(FrmGLOV.Tx_Datos[0].Text.TrimB(),Globales,unit);
                            if (b == 0)
                            {
                                FrmGLOV.Lb_Oficina.Text = "";
                            }
                            else
                            {
                                switch (MODGLOV.VgOV.Id_Cta)
                                {
                                    case T_MODGCON0.IdCta_ONMN:
                                        if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                        {
                                            FrmGLOV.Lb_Oficina.Text = "";
                                        }
                                        else
                                        {
                                            FrmGLOV.Lb_Oficina.Text = MODGNCTA.VCta[b].Cta_Nom.TrimB();
                                        }
                                        break;
                                    case T_MODGCON0.IdCta_ONME:
                                        if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                        {
                                            FrmGLOV.Lb_Oficina.Text = "";
                                        }
                                        else
                                        {
                                            FrmGLOV.Lb_Oficina.Text = MODGNCTA.VCta[b].Cta_Nom.TrimB();
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    // ******************************************
                    break;
                case 1:
                    if (FrmGLOV.Tx_Datos[1].Text.TrimB() != "")
                    {
                        if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_SCSMN || MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_SCSME)
                        {
                            Texto = "01;02;03;04";
                            if (Texto.InStr(MigrationSupport.Utils.Format(FrmGLOV.Tx_Datos[1].Text, "00"), 1, StringComparison.CurrentCulture) != 0)
                            {
                                if (FrmGLOV.Tx_Datos[1].Text == "1" || FrmGLOV.Tx_Datos[1].Text == "3")
                                {
                                    Pr_Generar_Automatica_Ini(Globales, unit);
                                }
                            }
                            else
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "El Tipo de Movimiento debe estar entre 1 y 4.",
                                    Type = TipoMensaje.Error,
                                    Title = T_MODGLORI.MsgxOri,
                                    ControlName = "Tx_Datos_001"
                                });
                                FrmGLOV.Tx_Datos[1].Enabled = true;
                                return;
                            }
                        }
                    }
                    break;
            }
        }

        public static void Tx_Datos_LostFocus(DatosGlobales Globales,UnitOfWorkCext01 unit, int Index)
        {
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            int KeyAscii = 0;
            int b = 0;
            string t = "";
            string a = "";
            string m = "";


            if (FrmGLOV.Tx_Datos[0].Text.ToVal() != 0)
            {
                switch (MODGLOV.VgOV.Id_Cta)
                {
                    case T_MODGCON0.IdCta_OPOP:
                        switch (Index)
                        {
                            case 0:
                                m = FrmGLOV.Cb_Mnd.Text;
                                a = MODGL.moneda_reves(Globales, m).TrimB();
                                t = FrmGLOV.Tx_Datos[0].Text;
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales, unit, t, a.ToInt()).ToInt();
                                //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
                                if (b > -1)
                                {
                                    FrmGLOV.Tx_Datos[1].Text = MODGTAB0.VNom[b].Nom_cta;
                                }
                                FrmGLOV.Tx_Datos[1].Enabled = false;
                                KeyAscii = 0;
                                MigrationSupport.Utils.SendKeys("{Tab}", false);
                                break;
                        }
                        break;
                }
            }
            // SendKeys "{Tab}"
        }

        private static void Pr_Generar_Automatica_Ini(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGL MODGL = Globales.MODGL;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            string linea = "";
            string tp = "";
            bool si = false;

            // Generación Automática
            si = false;
            if (MODGL.delista == 0)
            {
                // If Tx_Datos(2).Enabled Then
                si = true;
                // End If
            }
            else
            {
                // linea$ = L_Ori.List(L_Ori.ListIndex)
                tp = MODGPYF0.copiardestring(linea, 9.Char(), 7);
                if (tp.ToVal() != T_MODGLOV.TP_INI)
                {
                    si = true;
                    if (tp.ToVal() == T_MODGLOV.TP_COM && !FrmGLOV.Tx_Datos[2].Enabled)
                    {
                        si = false;
                    }
                }
            }
            if (si)
            {
                FrmGLOV.Tx_Datos[2].Text = MODGLORI.Fn_Genera_Num(Globales, unit);
                if (FrmGLOV.Tx_Datos[2].Text.TrimB() != "")
                {
                    FrmGLOV.Tx_Datos[2].Enabled = false;
                }
            }

        }

        public static void Boton_Click(DatosGlobales Globales,UnitOfWorkCext01 unit, int Index)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGLORI MODGLORI = Globales.MODGLORI;
       
            double Monto = 0.0;
            int n = 0;
            var validaSwift = true;
            var validarCuenta = true;

            switch (Index)
            {
                case 0:
                    // Se cargan los montos.
                    switch (MODGLOV.VgOV.Id_Cta)
                    {
                        // Cuenta Corriente M/N.
                        case T_MODGCON0.IdCta_CtaCteMN:
                        case T_MODGCON0.IdCta_CtaCteME:
                        case T_MODGCON0.IdCta_ChqCCME:
                            if (Fn_Cargar_Cta_Cte(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            if (MODGLOV.VgOV.TipoOV == "O")
                            {
                                // **************************
                                // Karina Rojas
                                // Valida Monto con Saldo de Ccte
                                Monto = ModSaldo.Obtiene_Monto(Globales,unit, T_MODGCON0.IdCta_CtaCteMN, T_MODGCON0.IdCta_CtaCteME, MODGLOV.VgOV.Id_Cta, MODGLOV.VgOV.CtaCte);
                                if (Monto < MODGLOV.VgOV.MtoMnd)
                                {
                                    if (!Globales.vieneDeMsg)
                                    {

                                    }
                                    else
                                    {
                                        bool respuesta = Globales.resMsg;
                                        Globales.vieneDeMsg = false;
                                        Globales.resMsg = false;
                                        if (respuesta)
                                        {

                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                                // ***************************
                            }
                            // Saldos c/ Sucursales M/N.
                            break;
                        case T_MODGCON0.IdCta_SCSMN:
                        case T_MODGCON0.IdCta_SCSME:
                            switch (MODGLOV.VgOV.Id_Cta)
                            {
                                case T_MODGCON0.IdCta_SCSMN:
                                    if (Fn_Cargar_Saldos(Globales,unit, n, T_MODGCON0.IdCta_SCSMN) == 0)
                                    {
                                        return;
                                    }
                                    validarCuenta = false;
                                    break;
                                case T_MODGCON0.IdCta_SCSME:
                                    if (Fn_Cargar_Saldos(Globales, unit, n, T_MODGCON0.IdCta_SCSME) == 0)
                                    {
                                        return;
                                    }
                                    validarCuenta = false;
                                    break;
                            }
                            FrmGLOV.Tx_Datos[1].Enabled = false;
                            // Cheque M/E Emi. x B. Chile
                            break;
                        case T_MODGCON0.IdCta_CHMEOBC:
                            if (Fn_Cargar_Cheques(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            // Vale Vista Otro Banco
                            break;
                        case T_MODGCON0.IdCta_VVOB:
                            if (Fn_Cargar_Vales_Vistas(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            validaSwift = false;
                            // Cta. Cte. Banco Central
                            break;
                        case T_MODGCON0.IdCta_CTACTEBC:
                            if (Fn_Cargar_Bco_Central(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            // Corresponsal cuenta ordinaria
                            break;
                        case T_MODGCON0.IdCta_CTAORD:
                        case T_MODGCON0.IdCta_CHMEBCH:
                        case T_MODGCON0.IdCta_CHVBNYM:
                            if (Fn_Cargar_Corresponsal(Globales, unit, n) == 0)
                            {
                                return;
                            }
                            // Divisas Pendientes.
                            break;
                        case T_MODGCON0.IdCta_DIVENPEN:
                            if (Fn_Cargar_Divisas_Pendientes(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            // Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                            break;
                        case T_MODGCON0.IdCta_VAM:
                        case T_MODGCON0.IdCta_VAX:
                        case T_MODGCON0.IdCta_VAMC:
                            switch (MODGLOV.VgOV.Id_Cta)
                            {
                                case T_MODGCON0.IdCta_VAM:
                                    if (Fn_Cargar_Acreedores(Globales,unit, n, T_MODGCON0.IdCta_VAM) == 0)
                                    {
                                        return;
                                    }
                                    break;
                                case T_MODGCON0.IdCta_VAX:
                                    if (Fn_Cargar_Acreedores(Globales, unit, n, T_MODGCON0.IdCta_VAX) == 0)
                                    {
                                        return;
                                    }
                                    validarCuenta = false;
                                    break;
                                case T_MODGCON0.IdCta_VAMC:
                                    if (Fn_Cargar_Acreedores(Globales, unit, n, T_MODGCON0.IdCta_VAMC) == 0)
                                    {
                                        return;
                                    }
                                    break;
                            }
                            // Vale Vista Bco. Chile
                            break;
                        case T_MODGCON0.IdCta_VVBCH:
                            if (Fn_Cargar_Vales_Vista_Bco(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            // Orden de Pago Convenio
                            break;
                        case T_MODGCON0.IdCta_OPC:
                            if (Fn_Cargar_Orden_Convenio(Globales,unit, n) == 0)
                            {
                                return;
                            }
                            // Orden de Pago Otros Países
                            break;
                        case T_MODGCON0.IdCta_OPOP:
                            if (Fn_Cargar_Orden_Paises(Globales,unit,n) == 0)
                            {
                                return;
                            }
                            // Otro Nemónico M/N ---- Otro Nemónico M/E
                            break;
                        case T_MODGCON0.IdCta_ONMN:
                        case T_MODGCON0.IdCta_ONME:
                            switch (MODGLOV.VgOV.Id_Cta)
                            {
                                case T_MODGCON0.IdCta_ONMN:
                                    if (Fn_Cargar_Otro_Nemonico(Globales,unit, n, T_MODGCON0.IdCta_ONMN) == 0)
                                    {
                                        return;
                                    }
                                    break;
                                case T_MODGCON0.IdCta_ONME:
                                    if (Fn_Cargar_Otro_Nemonico(Globales, unit, n, T_MODGCON0.IdCta_ONME) == 0)
                                    {
                                        return;
                                    }
                                    break;
                            }
                            break;
                        default:
                            // Validamos si es una obligacion
                            if (unit.SceRepository.EsObligacion(MODGLOV.VgOV.Id_Cta))
                            {
                                validaSwift = false;
                                if (Fn_Cargar_Corresponsal_Obligaciones(Globales, unit, n) == 0)
                                {
                                    return;
                                }
                            }
                            break;
                    }
                    MODGLOV.VgOV.Acepto = true.ToInt();
                    GLOBAL.datos_cuenta.Cuenta = FrmGLOV.L_Cuentas.Text == null ? null : FrmGLOV.L_Cuentas.Text.ToStr();// .Items[FrmGLOV.L_Cuentas.ListIndex].Value.ToStr();
                    GLOBAL.datos_cuenta.Moneda = FrmGLOV.Cb_Mnd.Value == null ? null : FrmGLOV.Cb_Mnd.Value.ToStr();
                    if (GLOBAL.datos_cuenta.Cuenta == "" && validarCuenta)
                    {
                        if (MODGLOV.VgOV.CodSwf == "" && validaSwift)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Es necesario que ingrese una Cuenta Corriente para realizar la operación.",
                                Title = MODGLORI.Titulo,
                                Type = TipoMensaje.Error,
                                ControlName = "cuentas"
                            });
                        }
                    }
                    break;
                case 1:
                    MODGLOV.VgOV.Acepto = false.ToInt();
                    break;
            }
            Globales.FrmGLOV = null;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Cuenta Corriente M/N." y si la validación es correcta
        //        carga los datos en los campos correspondientes dentro del arreglo
        //        VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Cta_Cte(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGOVD MODGOVD = Globales.MODGOVD;

            int Fn_Cargar_Cta_Cte = 0;

            int l = 0;

            Fn_Cargar_Cta_Cte = 0;

            if (Fn_Validar_Campos(Globales,unit, 3, 4) == 0)
            {
                return Fn_Cargar_Cta_Cte;
            }

            l = FrmGLOV.L_Cuentas.get_ItemData_(FrmGLOV.L_Cuentas.ListIndex).ToInt();
            MODGLOV.VgOV.CtaCte = MODGOVD.Vx_OriCC[l].ctacte.TrimB();
            MODGLOV.VgOV.CtaCte_t = MODGOVD.Vx_OriCC[l].CtaCte_t.TrimB();
            FrmGLOV.L_Cuentas.ListIndex = -1;
            Fn_Cargar_Cta_Cte = 1;

            return Fn_Cargar_Cta_Cte;
        }

        /// <summary>
        /// Valida los campos de existen dentro de la pantalla.
        /// </summary>
        /// <param name="Globales"></param>
        /// <param name="unit"></param>
        /// <param name="Campo_Inicial"></param>
        /// <param name="Campo_Final"></param>
        /// <returns></returns>
        private static int Fn_Validar_Campos(DatosGlobales Globales,UnitOfWorkCext01 unit, int Campo_Inicial, int Campo_Final)
        {
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGNCTA MODGNCTA = Globales.MODGNCTA;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGBIC MODGBIC = Globales.MODGBIC;


            int Fn_Validar_Campos = 0;

            string el_dv = "";
            string Msg = "";
            string Texto = "";
            string Secuencia = "";
            string Swift = "";
            int b = 0;
            string a = "";
            int i = 0;
            string num_par = "";
            string nums = "";
            string dv = "";

            Fn_Validar_Campos = 0;

            for (i = Campo_Inicial; i <= Campo_Final; i += 1)
            {
                switch (i)
                {
                    // **************************************************
                    case 0:
                        switch (MODGLOV.VgOV.Id_Cta)
                        {
                            case T_MODGCON0.IdCta_SCSMN:
                            case T_MODGCON0.IdCta_SCSME:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text= "Es necesario que ingrese una Oficina.",
                                        Title= MODGLORI.Titulo,
                                        Type=TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    return Fn_Validar_Campos;
                                }
                                if (MigrationSupport.Utils.IsNumeric(FrmGLOV.Tx_Datos[0].Text.TrimB()))
                                {
                                    FrmGLOV.Tx_Datos[0].MaxLength = 0;
                                    a = MODGOVD.Fn_Buscar_Suc(Globales, FrmGLOV.Tx_Datos[0].Text.TrimB());
                                    if (a.TrimB() != "")
                                    {
                                        if (FrmGLOV.Tx_Datos[0].Text.TrimB() == MODGPYF0.SyGet_OfiCod(Globales,unit, MODGUSR.UsrEsp.CentroCosto))
                                        {
                                            Globales.MESSAGES.Add(new UI_Message()
                                            {
                                                Text = "La oficina de destino no puede ser igual al origen.",
                                                Title = MODGLORI.Titulo,
                                                Type = TipoMensaje.Error,
                                                ControlName = "Tx_Datos_000"
                                            });
                                            FrmGLOV.Tx_Datos[0].Enabled = true;
                                            return Fn_Validar_Campos;
                                        }
                                        else
                                        {
                                            FrmGLOV.Lb_Oficina.Text = a;
                                        }
                                    }
                                    else
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Ingresar nuevo código: Código de Oficina no válido.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Tx_Datos[0].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                else
                                {
                                    FrmGLOV.Lb_Oficina.Text = "";
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Ingresar nuevo código: Código de Oficina no válido.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_CHMEBCH:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
                                //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
                                if (b == -1)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta ingresando.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    return Fn_Validar_Campos;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Tx_Datos[0].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                break;
                            case T_MODGCON0.IdCta_CHMEOBC:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                Swift = FrmGLOV.Tx_Datos[0].Text.Mid(1, 8).TrimB();
                                Secuencia = FrmGLOV.Tx_Datos[0].Text.Mid(9, 11).TrimB();
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGBIC.SyGet_VBic(Globales,unit, Swift, Secuencia);
                                if (b == 0)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar no está en la tabla de Sce_Bic, por lo tanto se le solicita ingresar otro Banco.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_VVOB:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() != "")
                                {
                                    //Globales.MESSAGES.Add(new UI_Message()
                                    //{
                                    //    Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                    //    Title = MODGLORI.Titulo,
                                    //    Type = TipoMensaje.Error
                                    //});
                                    //FrmGLOV.Tx_Datos[0].Enabled = true;

                                    //return Fn_Validar_Campos;

                                    Swift = FrmGLOV.Tx_Datos[0].Text.Substring(0, 8).Trim();
                                    Secuencia = FrmGLOV.Tx_Datos[0].Text.Substring(8).Trim();
                                    b = BCH.Comex.Core.BL.XGGL.Modulos.MODGBIC.SyGet_VBic(Globales, unit, Swift, Secuencia);
                                    if (b == 0)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "El Banco que Usted acaba de ingresar no está en la tabla Sce_Bic, por lo tanto se le solicita ingresar otro Banco.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Tx_Datos[0].Enabled = true;

                                        return Fn_Validar_Campos;
                                    }
                                }
                                break;
                            case T_MODGCON0.IdCta_CTAORD:
                            case T_MODGCON0.IdCta_DIVENPEN:
                            case T_MODGCON0.IdCta_CHVBNYM:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
                                //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
                                if (b == -1)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar no es Corresponsal o no opera en la Moneda especificada.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Tx_Datos[0].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                break;
                            case T_MODGCON0.IdCta_CTACTEBC:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                Swift = FrmGLOV.Tx_Datos[0].Text.Mid(1, 8).TrimB();
                                Secuencia = FrmGLOV.Tx_Datos[0].Text.Mid(9, 11).TrimB();
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGBIC.SyGet_VBic(Globales, unit, Swift, Secuencia);
                                if (b == 0)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar no está en la tabla Sce_Bic, por lo tanto se le solicita ingresar otro Banco.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                if (!MODGBIC.VBic.BicAla)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar NO es Aladi, por lo tanto se le solicita ingresar otro Banco que sea Aladi.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_VAM:
                            case T_MODGCON0.IdCta_VAX:
                            case T_MODGCON0.IdCta_VAMC:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Participante para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                FrmGLOV.Lb_Oficina.Text = MODGOVD.SyGet_Partys(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB()).TrimB();
                                if (FrmGLOV.Lb_Oficina.Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Participante que usted acaba de ingresar no existe dentro de los registros de los Partys.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_VVBCH:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que ingrese una Oficina.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                if (MigrationSupport.Utils.IsNumeric(FrmGLOV.Tx_Datos[0].Text.TrimB()))
                                {
                                    FrmGLOV.Tx_Datos[0].MaxLength = 0;
                                    a = MODGOVD.Fn_Buscar_Suc(Globales,FrmGLOV.Tx_Datos[0].Text.TrimB());
                                    if (a.TrimB() != "")
                                    {
                                        FrmGLOV.Lb_Oficina.Text = a;
                                    }
                                    else
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Ingresar nuevo código: Código de Oficina no válido.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Lb_Oficina.Text = "";
                                        FrmGLOV.Tx_Datos[0].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                break;
                            case T_MODGCON0.IdCta_OPC:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Fn_Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB());
                                if (b != 0)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar es Corresponsal, por lo tanto se solicita ingresar un Banco Aladi.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });                                    
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                if (BCH.Comex.Core.BL.XGGL.Modulos.MODGLORI.Fn_Valida_Aladi(Globales, FrmGLOV.Tx_Datos[1].Text.TrimB()) == 0)
                                {
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_OPOP:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
                                //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
                                if (b == -1)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta ingresando.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });                                        
                                        FrmGLOV.Tx_Datos[0].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                break;
                            case T_MODGCON0.IdCta_ONMN:
                            case T_MODGCON0.IdCta_ONME:
                                if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Código de Nemónico para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                b = BCH.Comex.Core.BL.XGGL.Modulos.MODGNCTA.Get_Cta(FrmGLOV.Tx_Datos[0].Text.TrimB(),Globales,unit);
                                if (b == 0)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "El Nemónico que Usted acaba de ingresar no se encuentra dentro de las Cuentas Contables.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_000"
                                    });
                                    FrmGLOV.Tx_Datos[0].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                else
                                {
                                    switch (MODGLOV.VgOV.Id_Cta)
                                    {
                                        case T_MODGCON0.IdCta_ONMN:
                                            if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                            {
                                                Globales.MESSAGES.Add(new UI_Message()
                                                {
                                                    Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional.",
                                                    Title = MODGLORI.Titulo,
                                                    Type = TipoMensaje.Error,
                                                    ControlName = "Tx_Datos_000"
                                                });
                                                FrmGLOV.Tx_Datos[0].Enabled = true;

                                                FrmGLOV.Lb_Oficina.Text = "";
                                                return Fn_Validar_Campos;
                                            }
                                            else
                                            {
                                                FrmGLOV.Lb_Oficina.Text = MODGNCTA.VCta[b].Cta_Nom.TrimB();
                                            }
                                            break;
                                        case T_MODGCON0.IdCta_ONME:
                                            if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                            {
                                                Globales.MESSAGES.Add(new UI_Message()
                                                {
                                                    Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero.",
                                                    Title = MODGLORI.Titulo,
                                                    Type = TipoMensaje.Error,
                                                    ControlName = "Tx_Datos_000"
                                                });
                                                FrmGLOV.Tx_Datos[0].Enabled = true;

                                                FrmGLOV.Lb_Oficina.Text = "";
                                                return Fn_Validar_Campos;
                                            }
                                            else
                                            {
                                                FrmGLOV.Lb_Oficina.Text = MODGNCTA.VCta[b].Cta_Nom.TrimB();
                                            }
                                            break;
                                    }
                                }
                                break;
                            default:
                                // Validamos si es una obligacion
                                if (unit.SceRepository.EsObligacion(MODGLOV.VgOV.Id_Cta))
                                {
                                    if (FrmGLOV.Tx_Datos[0].Text.TrimB() == "")
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Es necesario que se ingrese un Banco para poder realizar la operación.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Tx_Datos[0].Enabled = true;

                                        return Fn_Validar_Campos;
                                    }
                                    b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales, unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
                                    if (b == 0)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "El Banco que Usted acaba de ingresar no es Corresponsal o no opera en la Moneda especificada.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_000"
                                        });
                                        FrmGLOV.Tx_Datos[0].Enabled = true;

                                        return Fn_Validar_Campos;
                                    }
                                    else
                                    {
                                        if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                        {
                                            Globales.MESSAGES.Add(new UI_Message()
                                            {
                                                Text = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.",
                                                Title = MODGLORI.Titulo,
                                                Type = TipoMensaje.Error,
                                                ControlName = "Tx_Datos_000"
                                            });
                                            FrmGLOV.Tx_Datos[0].Enabled = true;

                                            return Fn_Validar_Campos;
                                        }
                                    }
                                }
                                break;
                        }
                        // ************************************************************
                        break;
                    case 1:
                        switch (MODGLOV.VgOV.Id_Cta)
                        {
                            case T_MODGCON0.IdCta_SCSMN:
                            case T_MODGCON0.IdCta_SCSME:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() != "")
                                {
                                    Texto = "01;02;03;04";
                                    if (Texto.InStr(MigrationSupport.Utils.Format(FrmGLOV.Tx_Datos[1].Text, "00"), 1, StringComparison.CurrentCulture) == 0)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "El Tipo de Movimiento debe estar entra 1 y 4.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_001"
                                        });
                                        FrmGLOV.Tx_Datos[1].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                switch (FrmGLOV.Tx_Datos[1].Text.ToInt())
                                {
                                    case T_MODGLOV.TP_INI:
                                        Pr_Generar_Automatica_Ini(Globales,unit);     // Generación Automática
                                        break;
                                    case T_MODGLOV.TP_CON:
                                    case T_MODGLOV.TP_COR:
                                        FrmGLOV.Tx_Datos[2].Enabled = true;
                                        break;
                                    case T_MODGLOV.TP_COM:
                                        if (!Globales.vieneDeMsg)
                                        {
                                            Globales.MESSAGES.Add(new UI_Message()
                                            {
                                                Text = "Desea hacer iniciativa de la partida ?",
                                                Title = MODGLORI.Titulo,
                                                Type = TipoMensaje.YesNo
                                            });
                                        }
                                        else
                                        {
                                            if (Globales.resMsg)
                                            {
                                                Pr_Generar_Automatica_Com(Globales,unit);
                                            }
                                            else
                                            {
                                                FrmGLOV.Tx_Datos[2].Enabled = true;
                                            }
                                        }
                                        break;
                                    case 0:
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Es necesario que ingrese un Tipo de Movimiento.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_001"
                                        });
                                        FrmGLOV.Tx_Datos[1].Enabled = true;
                                        return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_CHMEBCH:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Número de Cheque.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_001"
                                    });
                                    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_CHMEOBC:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Número de Cheque.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_001"
                                    });
                                    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_VVOB:
                                //if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                //{
                                //    Globales.MESSAGES.Add(new UI_Message()
                                //    {
                                //        Text = "Es necesario que se ingrese un Número de Vale Vista.",
                                //        Title = MODGLORI.Titulo,
                                //        Type = TipoMensaje.Error
                                //    });
                                //    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                //    return Fn_Validar_Campos;
                                //}
                                break;
                            case T_MODGCON0.IdCta_CTACTEBC:
                            case T_MODGCON0.IdCta_CTAORD:
                            case T_MODGCON0.IdCta_DIVENPEN:
                            case T_MODGCON0.IdCta_CHVBNYM:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_001"
                                    });
                                    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                if (MODGLOV.VgOV.Id_Cta == T_MODGCON0.IdCta_CTACTEBC)
                                {
                                    if (MigrationSupport.Utils.IsNumeric(FrmGLOV.Tx_Datos[1].Text.TrimB()))
                                    {
                                        if (BCH.Comex.Core.BL.XGGL.Modulos.MODGLORI.Fn_Valida_Aladi(Globales, FrmGLOV.Tx_Datos[1].Text.TrimB()) == 0)
                                        {
                                            FrmGLOV.Tx_Datos[1].Enabled = true;
                                            
                                            return Fn_Validar_Campos;
                                        }
                                    }
                                    else
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Error al digitar el Número de Referencia, ingreselo nuevamente.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_001"
                                        });
                                        FrmGLOV.Tx_Datos[1].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                break;
                            case T_MODGCON0.IdCta_VVBCH:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Número de Vale Vista.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_001"
                                    });
                                    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_OPC:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Número de Reembolso.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_001"
                                    });
                                    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_OPOP:
                                if (FrmGLOV.Tx_Datos[1].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese una Cta. Corriente.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_001"
                                    });
                                    FrmGLOV.Tx_Datos[1].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                        }
                        // ******************************************
                        break;
                    case 2:
                        switch (MODGLOV.VgOV.Id_Cta)
                        {
                            case T_MODGCON0.IdCta_SCSMN:
                            case T_MODGCON0.IdCta_SCSME:
                                if (FrmGLOV.Tx_Datos[2].Text.TrimB() != "")
                                {
                                    if (!MigrationSupport.Utils.IsNumeric(FrmGLOV.Tx_Datos[2].Text))
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Corregir Número de Partida: Se debe ingresar sólo números.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_002"
                                        });
                                        FrmGLOV.Tx_Datos[2].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                    num_par = FrmGLOV.Tx_Datos[2].Text;
                                    nums = num_par.Left(6);
                                    el_dv = num_par.Right(2);
                                    dv = BCH.Comex.Core.BL.XGGL.Modulos.MODGLORI.Fn_Calcula_Dig(nums.TrimB());
                                    if (dv != el_dv)
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Corregir Número de Partida: Dígito Verificador no corresponde.",
                                            Title = MODGLORI.Titulo,
                                            Type = TipoMensaje.Error,
                                            ControlName = "Tx_Datos_002"
                                        });
                                        FrmGLOV.Tx_Datos[2].Enabled = true;
                                        
                                        return Fn_Validar_Campos;
                                    }
                                }
                                else
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que ingrese el Número de Partida para continuar con la operación.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_002"
                                    });
                                    FrmGLOV.Tx_Datos[2].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                            case T_MODGCON0.IdCta_OPOP:
                                if (FrmGLOV.Tx_Datos[2].Text.TrimB() == "")
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Es necesario que se ingrese un Número de Referencia.",
                                        Title = MODGLORI.Titulo,
                                        Type = TipoMensaje.Error,
                                        ControlName = "Tx_Datos_002"
                                    });
                                    FrmGLOV.Tx_Datos[2].Enabled = true;
                                    
                                    return Fn_Validar_Campos;
                                }
                                break;
                        }
                        // **************************************************
                        break;
                    case 3:
                        if (FrmGLOV.L_Cuentas.ListIndex == -1)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Es Necesario que seleccione una Cuenta.",
                                Title = MODGLORI.Titulo,
                                Type = TipoMensaje.Error,
                                ControlName = "Tx_Datos_003"
                            });
                            FrmGLOV.L_Cuentas.Enabled = true;
                            
                            return Fn_Validar_Campos;
                        }
                        // **************************************************
                        break;
                    case 4:
                        // If L_Partys.ListIndex = -1 Then
                        //     MsgBox "Es Necesario que seleccione un Partys.", pito(48), Titulo
                        //     L_Partys.Enabled = True
                        //     L_Partys.SetFocus
                        //     Exit Function
                        // End If
                        break;
                }
            }

            Fn_Validar_Campos = 1;

            return Fn_Validar_Campos;
        }

        private static void Pr_Generar_Automatica_Com(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGL MODGL = Globales.MODGL;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            string linea = "";
            string tp = "";
            bool si = false;

            // Generación Automática
            si = false;
            if (MODGL.delista == 0)
            {
                if (FrmGLOV.Tx_Datos[2].Enabled)
                {
                    si = true;
                }
            }
            else
            {
                // linea$ = L_Ori.List(L_Ori.ListIndex)
                tp = MODGPYF0.copiardestring(linea, 9.Char(), 7);
                if (tp.ToVal() != T_MODGLOV.TP_COM)
                {
                    si = true;
                    if (tp.ToVal() == T_MODGLOV.TP_INI && !FrmGLOV.Tx_Datos[2].Enabled)
                    {
                        si = false;
                    }
                }
            }
            if (si)
            {
                FrmGLOV.Tx_Datos[2].Text = MODGLORI.Fn_Genera_Num(Globales,unit);
                if (FrmGLOV.Tx_Datos[2].Text != "")
                {
                    FrmGLOV.Tx_Datos[2].Enabled = false;
                }
            }

        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Saldos c/ Sucursales M/N." y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Saldos(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice, int Saldo)
        {
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGLOV MODGLOV = Globales.MODGLOV;

            int Fn_Cargar_Saldos = 0;


            Fn_Cargar_Saldos = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 2) == 0)
            {
                return Fn_Cargar_Saldos;
            }

            MODGLOV.VgOV.CodOfi = FrmGLOV.Tx_Datos[0].Text.ToInt();
            MODGLOV.VgOV.TipMov = FrmGLOV.Tx_Datos[1].Text.ToInt();
            MODGLOV.VgOV.NumPar = FrmGLOV.Tx_Datos[2].Text.ToInt();
            Fn_Cargar_Saldos = 1;

            return Fn_Cargar_Saldos;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Cheque M/E Emi. x B. Chile" y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro del
        //        arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Cheques(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            int Fn_Cargar_Cheques = 0;


            Fn_Cargar_Cheques = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Cheques;
            }
            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            Fn_Cargar_Cheques = 1;

            return Fn_Cargar_Cheques;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Vale Vista Otro Banco" y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Vales_Vistas(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            int Fn_Cargar_Vales_Vistas = 0;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            Fn_Cargar_Vales_Vistas = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Vales_Vistas;
            }

            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            Fn_Cargar_Vales_Vistas = 1;

            return Fn_Cargar_Vales_Vistas;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Cta. Cte. Banco Central" y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro del
        //        arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Bco_Central(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            int Fn_Cargar_Bco_Central = 0;


            Fn_Cargar_Bco_Central = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Bco_Central;
            }
            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            Fn_Cargar_Bco_Central = 1;

            return Fn_Cargar_Bco_Central;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Corresponsal cuenta ordinaria" y si la validación
        //        es correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Corresponsal(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            int Fn_Cargar_Corresponsal = 0;

            int b = 0;

            Fn_Cargar_Corresponsal = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Corresponsal;
            }

            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
            //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
            if (b > -1)
            {
                MODGLOV.VgOV.CodBco = MODGTAB0.VNom[b].Nom_Bco;
                Fn_Cargar_Corresponsal = 1;
            }
            
            return Fn_Cargar_Corresponsal;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Corresponsal cuenta ordinaria" y si la validación
        //        es correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Corresponsal_Obligaciones(DatosGlobales Globales, UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            int Fn_Cargar_Corresponsal = 0;

            int b = 0;

            Fn_Cargar_Corresponsal = 0;

            if (Fn_Validar_Campos(Globales, unit, 0, 1) == 0)
            {
                return Fn_Cargar_Corresponsal;
            }

            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            MODGLOV.VgOV.fecVen = FrmGLOV.Tx_Datos[3].Text.TrimB();
            b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales, unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
            MODGLOV.VgOV.CodBco = MODGTAB0.VNom[b].Nom_Bco;
            Fn_Cargar_Corresponsal = 1;

            return Fn_Cargar_Corresponsal;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Divisas Pendientes." y si la validación es correcta
        //        carga los datos en los campos correspondientes dentro del arreglo
        //        VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Divisas_Pendientes(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            int Fn_Cargar_Divisas_Pendientes = 0;

            int b = 0;

            Fn_Cargar_Divisas_Pendientes = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Divisas_Pendientes;
            }
            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            b = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, FrmGLOV.Tx_Datos[0].Text.TrimB(), MODGLOV.VgOV.codmnd);
            //Si retorna -1 el banco ingresado no maneja la moneda ingresada o no es corresponsal
            if (b > -1)
            {
                MODGLOV.VgOV.CodBco = MODGTAB0.VNom[b].Nom_Bco;
                Fn_Cargar_Divisas_Pendientes = 1;
            }            

            return Fn_Cargar_Divisas_Pendientes;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Varios Acreedores Import., Varios Acreedores Export.,
        //        Varios Acreedores Mcdo. Corr" y si la validación es correcta carga
        //        los datos en los campos correspondientes dentro del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Acreedores(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice, int Acreedores)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            int Fn_Cargar_Acreedores = 0;


            Fn_Cargar_Acreedores = 0;

            switch (Acreedores)
            {
                case T_MODGCON0.IdCta_VAM:
                    if (Fn_Validar_Campos(Globales,unit, 0, 0) == 0)
                    {
                        return Fn_Cargar_Acreedores;
                    }
                    break;
                case T_MODGCON0.IdCta_VAX:
                    if (Fn_Validar_Campos(Globales, unit, 0, 0) == 0)
                    {
                        return Fn_Cargar_Acreedores;
                    }
                    break;
                case T_MODGCON0.IdCta_VAMC:
                    if (Fn_Validar_Campos(Globales, unit, 0, 0) == 0)
                    {
                        return Fn_Cargar_Acreedores;
                    }
                    break;
            }

            MODGLOV.VgOV.IdPrty = FrmGLOV.Tx_Datos[0].Text.TrimB();
            Fn_Cargar_Acreedores = 1;

            return Fn_Cargar_Acreedores;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Otro Nemónico M/N ---- Otro Nemónico M/E" y si la
        //        validación es correcta carga los datos en los campos correspondientes
        //        dentro del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Otro_Nemonico(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice, int Nemonico)
        {
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            int Fn_Cargar_Otro_Nemonico = 0;


            Fn_Cargar_Otro_Nemonico = 0;

            switch (Nemonico)
            {
                case T_MODGCON0.IdCta_ONMN:
                    if (Fn_Validar_Campos(Globales,unit, 0, 0) == 0)
                    {
                        return Fn_Cargar_Otro_Nemonico;
                    }
                    break;
                case T_MODGCON0.IdCta_ONME:
                    if (Fn_Validar_Campos(Globales, unit, 0, 0) == 0)
                    {
                        return Fn_Cargar_Otro_Nemonico;
                    }
                    break;
            }

            MODGLORI.VxOri[Indice].NemCta = FrmGLOV.Tx_Datos[0].Text.TrimB();
            Fn_Cargar_Otro_Nemonico = 1;

            return Fn_Cargar_Otro_Nemonico;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Orden de Otros Países" y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Orden_Paises(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;


            int Fn_Cargar_Orden_Paises = 0;

            int X = 0;

            Fn_Cargar_Orden_Paises = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 2) == 0)
            {
                return Fn_Cargar_Orden_Paises;
            }
            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();

            X = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGet_Cor(Globales,unit, MODGLOV.VgOV.CodSwf);
            MODGLOV.VgOV.CodBco = MODGTAB0.WCor.CorBco;

            MODGLOV.VgOV.CtaCte = FrmGLOV.Tx_Datos[1].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[2].Text.TrimB();
            Fn_Cargar_Orden_Paises = 1;

            return Fn_Cargar_Orden_Paises;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Orden de Pago Convenio" y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Orden_Convenio(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            int Fn_Cargar_Orden_Convenio = 0;


            Fn_Cargar_Orden_Convenio = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Orden_Convenio;
            }
            MODGLOV.VgOV.CodSwf = FrmGLOV.Tx_Datos[0].Text.TrimB();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            Fn_Cargar_Orden_Convenio = 1;

            return Fn_Cargar_Orden_Convenio;
        }

        // ****************************************************************************
        //    1.  Envía orden para validar los campos necesarios para la operación
        //        con respecto a "Vale Vista Bco. Chile" y si la validación es
        //        correcta carga los datos en los campos correspondientes dentro
        //        del arreglo VxOri.
        // ****************************************************************************
        private static int Fn_Cargar_Vales_Vista_Bco(DatosGlobales Globales, UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            UI_FrmGLOV FrmGLOV = Globales.FrmGLOV;

            int Fn_Cargar_Vales_Vista_Bco = 0;


            Fn_Cargar_Vales_Vista_Bco = 0;

            if (Fn_Validar_Campos(Globales,unit, 0, 1) == 0)
            {
                return Fn_Cargar_Vales_Vista_Bco;
            }

            MODGLOV.VgOV.CodOfi = FrmGLOV.Tx_Datos[0].Text.ToInt();
            MODGLOV.VgOV.numdoc = FrmGLOV.Tx_Datos[1].Text.TrimB();
            Fn_Cargar_Vales_Vista_Bco = 1;

            return Fn_Cargar_Vales_Vista_Bco;
        }
    }
}
