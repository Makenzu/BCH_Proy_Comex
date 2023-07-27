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
    public class FrmxAnu
    {
        private const string Tx_valores = "02;04;09;13;15;16;17;19;24;25;26;27;28;30;32";
        private const string Tx_Focus = "02;04;07;09;10;11;13;15;16;17;19;20;21;22;30;31;32;24;25;26;27;29;37";
        private const string Tx_Notmodi = "00;01;03;05;06;08;12;14;18;23;24;27;28;30;31;32";

        public static void Form_Activate(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short n = 0;
            n = (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus);
            //if (n > 0)
            if (n >= 0)
            {
                Pr_Cargar_Datos((short)VB6Helpers.Val(initObj.Frmxanu.Boton[0].Tag), initObj, unit);
            }

            if (initObj.MODXANU.VxAnus[n].TipPln != 403)
            {
                initObj.Frmxanu.Cb_Autor.Enabled = true;
                initObj.Frmxanu.Tx_PlAnu[30].Enabled = true;
                initObj.Frmxanu.Tx_PlAnu[31].Enabled = true;
                initObj.Frmxanu.Tx_PlAnu[32].Enabled = true;
            }
            else
            {
                initObj.Frmxanu.Cb_Autor.Enabled = false;
                initObj.Frmxanu.Tx_PlAnu[30].Enabled = false;
                initObj.Frmxanu.Tx_PlAnu[31].Enabled = false;
                initObj.Frmxanu.Tx_PlAnu[32].Enabled = false;
            }
        }
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short a = 0;
            short n = 0;
            short i = 0;

            for (a = 1; a <= 23; a++)
            {
                initObj.Frmxanu.Focus_RENAMED[a] = MODGPYF0.copiardestring(Tx_Focus, ";", a);
            }
            n = (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus);
            if (~initObj.MODXANU.VgAnu.AnuSin != 0)
            {
                for (i = 0; i <= 28; i++)
                {
                    if (i != 1)
                    {
                        initObj.Frmxanu.Tx_PlAnu[i].Enabled = false;
                    }
                }
            }

            if (initObj.MODXANU.VgAnu.AnuSin != 0)
            {
                for (i = 0; i <= 28; i++)
                {
                    if (i != 1)
                    {
                        if (VB6Helpers.Instr(Tx_Notmodi, VB6Helpers.Format(VB6Helpers.CStr(i), "00")) != 0)
                        {
                            initObj.Frmxanu.Tx_PlAnu[i].Enabled = false;
                        }
                        else
                        {
                            initObj.Frmxanu.Tx_PlAnu[i].Enabled = true;
                        }
                    }
                }
            }

            Pr_Cargar_Autorizacion(initObj, unit);

            if (initObj.MODXANU.VxAnus[0].TipPln != 403)
            {
                initObj.Frmxanu.Cb_Autor.Enabled = true;
                initObj.Frmxanu.Tx_PlAnu[30].Enabled = true;
                initObj.Frmxanu.Tx_PlAnu[31].Enabled = true;
                initObj.Frmxanu.Tx_PlAnu[32].Enabled = true;
            }
            else
            {
                initObj.Frmxanu.Cb_Autor.Enabled = false;
                initObj.Frmxanu.Tx_PlAnu[30].Enabled = false;
                initObj.Frmxanu.Tx_PlAnu[31].Enabled = false;
                initObj.Frmxanu.Tx_PlAnu[32].Enabled = false;
            }

            a = MODGTAB0.SyGetn_Bco(initObj, unit);

            if (initObj.MODXPLN1.IndPlv == 0)
            {
                //initObj.Frmxanu.Boton[0].Tag = 1;
                initObj.Frmxanu.Boton[0].Tag = 0;
            }
            else
            {
                initObj.Frmxanu.Boton[0].Tag = initObj.MODXPLN1.IndPlv;
            }


            //Deshabilita TextBox segun original 
            initObj.Frmxanu.Tx_PlAnu[0].Enabled = false;
            initObj.Frmxanu.Tx_PlAnu[1].Enabled = false;
            initObj.Frmxanu.Tx_PlAnu[1].Text = "15";


        }

        public static void BotonRetroceder_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
            short n = 0;
            short i = 0;

            n = (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus);
            //Falta filtrar por planillas anuladas.
            //if (n > 0)
            if (n >= 0)
            {
                initObj.Frmxanu.Boton[0].Enabled = true;
                initObj.Frmxanu.Boton[1].Enabled = true;

                //if (Format.StringToDouble(initObj.Frmxanu.Boton[0].Tag) > 1)
                if (Format.StringToDouble((string)initObj.Frmxanu.Boton[0].Tag) >= 1)
                {
                    initObj.Frmxanu.Boton[0].Tag = VB6Helpers.CInt(initObj.Frmxanu.Boton[0].Tag) - 1;
                }

                //if (Format.StringToDouble(initObj.Frmxanu.Boton[0].Tag) == 1)
                if (Format.StringToDouble((string)initObj.Frmxanu.Boton[0].Tag) == 0)
                {
                    initObj.Frmxanu.Boton[0].Enabled = false;
                }

                if (initObj.MODXANU.VgAnu.AnuSin != 0)
                {
                    for (i = 2; i <= 28; i++)
                    {
                        initObj.Frmxanu.Tx_PlAnu[i].Text = "";
                    }

                    for (i = 31; i <= 32; i++)
                    {
                        initObj.Frmxanu.Tx_PlAnu[i].Text = "";
                    }
                }

                Pr_Cargar_Datos((short)VB6Helpers.Val(initObj.Frmxanu.Boton[0].Tag), initObj, uow);
                MODPREEM.CargaEnLista_TipAut(Mdl_Funciones_Varias, initObj.Frmxanu.Cb_Autor);
                initObj.Frmxanu.Cb_Autor.ListIndex = -1;
            }
        }

        public static void BotonAvanzar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
            short n = 0;
            short i = 0;

            n = (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus);

            //Falta filtrar por planillas anuladas.
            //if (n > 0)
            if (n >= 0)
            {
                initObj.Frmxanu.Boton[1].Enabled = true;
                initObj.Frmxanu.Boton[0].Enabled = true;

                //if (Format.StringToDouble(initObj.Frmxanu.Boton[0].Tag) < n)
                if (Format.StringToDouble((string)initObj.Frmxanu.Boton[0].Tag) <= n)
                {
                    initObj.Frmxanu.Boton[0].Tag = VB6Helpers.CInt(initObj.Frmxanu.Boton[0].Tag) + 1;
                }

                if (Format.StringToDouble((string)initObj.Frmxanu.Boton[0].Tag) == n)
                {
                    initObj.Frmxanu.Boton[1].Enabled = false;
                }

                if (initObj.MODXANU.VgAnu.AnuSin != 0)
                {
                    for (i = 2; i <= 28; i++)
                    {
                        initObj.Frmxanu.Tx_PlAnu[i].Text = "";
                    }

                    for (i = 31; i <= 32; i++)
                    {
                        initObj.Frmxanu.Tx_PlAnu[i].Text = "";
                    }
                }

                Pr_Cargar_Datos((short)VB6Helpers.Val(initObj.Frmxanu.Boton[0].Tag), initObj, uow);
                MODPREEM.CargaEnLista_TipAut(Mdl_Funciones_Varias, initObj.Frmxanu.Cb_Autor);
            }

        }

        public static void BotonTickear_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = 0;
            short Fecha_Paso = 0;
            short a = 0;

            if (~initObj.MODXANU.VgAnu.AnuSin != 0)
            {
                if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_Fecha.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe ingresar fecha presentación de la planilla.",
                        ControlName = "Tx_Fecha_Text"
                    });
                    return;
                }

                //La fecha no puede ser sábado ni domingo
                Fecha_Paso = VB6Helpers.Weekday(VB6Helpers.CDate(initObj.Frmxanu.Tx_Fecha.Text));
                if (Fecha_Paso == 1 || Fecha_Paso == 7)
                {
                    //Sólo si es fin de semana
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: La Fecha no puede ser fin de semana.",
                        ControlName = "Tx_Fecha_Text"
                    });
                    return;
                }

                //La fecha no puede ser un feriado de este año
                a = MODGTAB0.Fn_Buscar_Fecha_Fer(initObj.MODGTAB0, uow, VB6Helpers.Trim(initObj.Frmxanu.Tx_Fecha.Text));
                if (a == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: La Fecha no corresponde, porque existe como fecha feriada de este año.",
                        ControlName = "Tx_Fecha_Text"
                    });
                    return;
                }

                i = (short)VB6Helpers.Val(initObj.Frmxanu.Boton[0].Tag);
                initObj.MODXANU.VxAnus[i].ObsPln = initObj.Frmxanu.Tx_PlAnu[29].Text;
                initObj.MODXANU.VxAnus[i].fecpre = VB6Helpers.Format(initObj.Frmxanu.Tx_Fecha.Text, "dd/MM/yyyy");
                initObj.MODXANU.VxAnus[i].Fecing = VB6Helpers.Format(initObj.Frmxanu.Tx_Fecha.Text, "dd/MM/yyyy");
            }

            //Traspaso los campos al la estructura de anulación
            //-------------------------------------------------
            i = VB6Helpers.CShort(initObj.Frmxanu.Boton[0].Tag);
            if (Valida(initObj) != 0)
            {
                Trasp_Anu(i, initObj, uow);
               // short _tempVar1 = 1;
                BotonAvanzar_Click(initObj, uow);
            }
        }

        public static void BotonAceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = 0;

            for (i = 0; i <= (short)initObj.MODGCVD.VxPlaAnu.Nropla; i++)
            {
                if (initObj.MODXANU.VxAnus[i].PlnOK == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Faltan Planillas por ingresar.",
                        Title = "Planillas de Anulación"
                    });
                    return;
                }

            }
            initObj.MODXANU.VxAnus[1].Acepto = (short)(true ? -1 : 0);
        }

        public static void BotonCancelar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.MODXANU.VxAnus[1].Acepto = (short)(false ? -1 : 0);
        }

        private static void Pr_Cargar_Datos(short Indice, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short i = 0;
            string n = "";
            short m = 0;
            string R = "";
            short NN = 0;
            double Monto1 = 0;
            double Monto2 = 0;
            double Monto3 = 0;
            short k = 0;
            short j = 0;

            i = Indice;

            //Número Presentación.
            if(!string.IsNullOrEmpty(initObj.MODXANU.VxAnus[i].NumPre))
            {
                n = VB6Helpers.Format(initObj.MODXANU.VxAnus[i].NumPre, "0000000");
                initObj.Frmxanu.Tx_PlAnu[0].Text = VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1);
            }

            //Fecha Presentación.
            initObj.Frmxanu.Tx_Fecha.Text = VB6Helpers.Format(initObj.MODXANU.VxAnus[i].fecpre, "dd/MM/yyyy");

            if (initObj.MODXANU.VxAnus[i].PlnEst != 0)
            {
                initObj.Frmxanu.Tx_Fecha.Enabled = true;
            }
            else
            {
                initObj.Frmxanu.Tx_Fecha.Enabled = false;
            }

            //Tipo de Anulación.
            initObj.Frmxanu.Tx_PlAnu[2].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].TipAnu), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[2]));

            //Plaza Banco Central que Contabiliza
            m = MODGTAB1.Get_VPbc(25, initObj, unit);
            if (m != 0)
            {
                initObj.Frmxanu.Tx_PlAnu[3].Text = initObj.MODGTAB1.VPbc[m].Pbc_PbcDes;
            }
            else
            {
                initObj.Frmxanu.Tx_PlAnu[3].Text = "";
            }

            //Codigo Plaza Banco Central que Contabiliza.
            initObj.Frmxanu.Tx_PlAnu[4].Text = VB6Helpers.Format("25", MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[4]));
            //End If

            //Nombre.
            if (VB6Helpers.Mid(initObj.MODXANU.VxAnus[i].PrtExp, 1, 1) == "0")
            {
                initObj.MODXANU.VxAnus[i].PrtExp = VB6Helpers.Mid(initObj.MODXANU.VxAnus[i].PrtExp, 2) + "~";
            }

            initObj.Frmxanu.Tx_PlAnu[5].Text = Mdl_Funciones_Varias.GetDatPrtn(initObj, unit, initObj.MODXANU.VxAnus[i].PrtExp, initObj.MODXANU.VxAnus[i].IndNom, initObj.MODXANU.VxAnus[i].IndDir, "N", "DC");

            //Dirección.
            initObj.Frmxanu.Tx_PlAnu[6].Text = Mdl_Funciones_Varias.GetDatPrtn(initObj, unit, initObj.MODXANU.VxAnus[i].PrtExp, initObj.MODXANU.VxAnus[i].IndNom, initObj.MODXANU.VxAnus[i].IndDir, "D", "DC");

            //Rut.
            if(!string.IsNullOrWhiteSpace(initObj.MODXANU.VxAnus[i].RutExp))
            {
                R = VB6Helpers.Right("000000000" + initObj.MODXANU.VxAnus[i].RutExp, 9);
                if (string.IsNullOrEmpty(R))
                {
                    R = initObj.MODXANU.VxAnus[i].RutExp;
                }

                initObj.Frmxanu.Tx_PlAnu[7].Text = VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1);
            }

            //Entidad que autoriza
            if (~initObj.MODXANU.VgAnu.AnuSin != 0)
            {
                m = MODGTAB0.Get_Bco(initObj, unit, 15);
                if (m >= 0)
                {
                    initObj.Frmxanu.Tx_PlAnu[8].Text = VB6Helpers.Trim(initObj.MODGTAB0.VBco[m].NomBco);
                }

                initObj.Frmxanu.Tx_PlAnu[9].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].EntAut), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[9]));
                m = MODGTAB0.Get_Bco(initObj, unit, initObj.MODXANU.VxAnus[i].EntAut);
                if (m >= 0)
                {
                    initObj.Frmxanu.Tx_PlAnu[8].Text = VB6Helpers.Trim(initObj.MODGTAB0.VBco[m].NomBco);
                }
            }
            else
            {
                initObj.Frmxanu.Tx_PlAnu[9].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].EntAut), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[9]));
                m = MODGTAB0.Get_Bco(initObj, unit, initObj.MODXANU.VxAnus[i].EntAut);
                if (m >= 0)
                {
                    initObj.Frmxanu.Tx_PlAnu[8].Text = VB6Helpers.Trim(initObj.MODGTAB0.VBco[m].NomBco);
                }
            }

            //Número Presentación Original.
            if(!string.IsNullOrEmpty(initObj.MODXANU.VxAnus[i].NumpreO))
            {
                n = initObj.MODXANU.VxAnus[i].NumpreO;
                initObj.Frmxanu.Tx_PlAnu[10].Text = n;
            }

            //Fecha Presentación Original.
            initObj.Frmxanu.Tx_PlAnu[11].Text = VB6Helpers.Format(initObj.MODXANU.VxAnus[i].FecpreO, "dd/MM/yyyy");

            //Tipo de Operación.
            if (initObj.MODXANU.VxAnus[i].TipPln != 0)
            {
                initObj.Frmxanu.Tx_PlAnu[12].Text = MODXPLN1.GetNomPLn(initObj.MODXANU.VxAnus[i].TipPln);

                //Código Tipo de Operación.
                initObj.Frmxanu.Tx_PlAnu[13].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].TipPln), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[13]));
            }

            //Codigo Plaza Banco Central que Contabiliza.
            if (initObj.MODXANU.VxAnus[i].CodPbc != 0)
            {
                //Plaza Banco Central que Contabiliza
                m = MODGTAB1.Get_VPbc(initObj.MODXANU.VxAnus[i].CodPbc, initObj, unit);
                if (m != 0)
                {
                    initObj.Frmxanu.Tx_PlAnu[14].Text = initObj.MODGTAB1.VPbc[m].Pbc_PbcDes;
                }
                else
                {
                    initObj.Frmxanu.Tx_PlAnu[14].Text = "";
                }

                //Codigo Plaza Banco Central que Contabiliza.
                initObj.Frmxanu.Tx_PlAnu[15].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].CodPbc), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[15]));
            }

            //Monto US$.
            initObj.Frmxanu.Tx_PlAnu[16].Text = MODGPYF0.forma(initObj.MODXANU.VxAnus[i].MtoDol, MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[16]));

            //Paridad a US$.
            initObj.Frmxanu.Tx_PlAnu[17].Text = MODGPYF0.forma(initObj.MODXANU.VxAnus[i].Mtopar, MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[17]));

            //Aduana.
            if (initObj.MODXANU.VxAnus[i].CodAdn != 0)
            {
                //Descripción de la Aduana.
                initObj.Frmxanu.Tx_PlAnu[18].Text = string.Empty;
                m = MODGTAB1.Get_VAdn(initObj.MODGTAB1, unit, initObj.MODXANU.VxAnus[i].CodAdn);
                if (m >= 0)               
                    initObj.Frmxanu.Tx_PlAnu[18].Text = initObj.MODGTAB1.VAdn[m].NomAdn;  

                //Código Aduana.
                initObj.Frmxanu.Tx_PlAnu[19].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].CodAdn), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[19]));
            }

            //Número Aceptación.
            if(!string.IsNullOrWhiteSpace(initObj.MODXANU.VxAnus[i].numdec))
            {
                n = initObj.MODXANU.VxAnus[i].numdec;
                initObj.Frmxanu.Tx_PlAnu[20].Text = n;
            }

            //Fecha Aceptación.
            initObj.Frmxanu.Tx_PlAnu[21].Text = VB6Helpers.Format(initObj.MODXANU.VxAnus[i].FecDec, "dd/MM/yyyy");

            //Fecha Vencimiento Retorno.
            initObj.Frmxanu.Tx_PlAnu[22].Text = VB6Helpers.Format(initObj.MODXANU.VxAnus[i].FecVen, "dd/MM/yyyy");

            //Moneda.
            if (~initObj.MODXANU.VgAnu.AnuSin != 0)
            {
                if (initObj.MODXANU.VxAnus[i].CodMnd != 0)
                {
                    m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, unit, initObj.MODXANU.VxAnus[i].CodMnd);
                    if (m != 0)
                    {
                        initObj.Frmxanu.Tx_PlAnu[23].Text = initObj.MODGTAB0.VMnd[m].Mnd_MndNom;
                    }
                    else
                    {
                        initObj.Frmxanu.Tx_PlAnu[23].Text = "";
                    }

                    //Código Moneda.
                    initObj.Frmxanu.Tx_PlAnu[24].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODGTAB0.VMnd[m].Mnd_MndCbc), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[24]));
                }
            }
            else
            {
                NN = initObj.MODXANU.VxAnus[i].CodMnd;
                m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, unit, NN);
                initObj.Frmxanu.Tx_PlAnu[23].Text = initObj.MODGTAB0.VMnd[m].Mnd_MndNom;
                initObj.Frmxanu.Tx_PlAnu[24].Text = VB6Helpers.CStr(initObj.MODGTAB0.VMnd[m].Mnd_MndCbc);
            }

            //Monto Anulado.
            initObj.Frmxanu.Tx_PlAnu[25].Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODXANU.VxAnus[i].MtoAnu), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[25]));

            //Paridad a US$ de Anulación.
            initObj.Frmxanu.Tx_PlAnu[26].Text = MODGPYF0.forma(initObj.MODXANU.VxAnus[i].MtoParA, MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[26]));

            Monto1 = initObj.MODXANU.VxAnus[i].MtoAnu;
            Monto2 = initObj.MODXANU.VxAnus[i].MtoParA;
            Monto3 = 0;
            if (Monto1 > 0 && Monto2 > 0)
            {
                Monto3 = (Monto1 / Monto2);
            }

            //Monto en US$ de Anulación.
            initObj.Frmxanu.Tx_PlAnu[27].Text = MODGPYF0.forma(Monto3, MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[27]));

            //Monto en US$ Original.
            initObj.Frmxanu.Tx_PlAnu[28].Text = MODGPYF0.forma(initObj.MODXANU.VxAnus[i].MtoDolPo, MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[28]));

            //Observaciones.
            initObj.Frmxanu.Tx_PlAnu[29].Text = initObj.MODXANU.VxAnus[i].ObsPln;

            //Nro. de Autorización.-
            if (string.IsNullOrEmpty(initObj.MODXANU.VxAnus[i].TipAut))
            {
                initObj.Frmxanu.Cb_Autor.ListIndex = 6;
            }
            else
            {
                for (k = 0; k < (short)initObj.Frmxanu.Cb_Autor.ListCount; k++)
                {
                    if (VB6Helpers.Left(VB6Helpers.Trim(initObj.Frmxanu.Cb_Autor.get_List(k)), 2) == VB6Helpers.Left(VB6Helpers.Trim(initObj.MODXANU.VxAnus[i].TipAut), 2))
                    {
                        initObj.Frmxanu.Cb_Autor.ListIndex = k;
                    }
                }
            }

            if (initObj.MODXANU.VxAnus[i].NroAut == 0)
            {
                initObj.Frmxanu.Tx_PlAnu[30].Text = "";
            }
            else
            {
                initObj.Frmxanu.Tx_PlAnu[30].Text = VB6Helpers.Str(initObj.MODXANU.VxAnus[i].NroAut);
            }

            //Fecha de Autorización.-
            initObj.Frmxanu.Tx_PlAnu[31].Text = VB6Helpers.Format(initObj.MODXANU.VxAnus[i].FecAut, "dd/MM/yyyy");

            initObj.Frmxanu.Tx_PlAnu[32].Text = MODGPYF0.forma(initObj.MODXANU.VxAnus[i].TipCam, MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[32]));

            //Habilita Botones.
            for (j = 0; j <= 4; j++)
            {
                initObj.Frmxanu.Boton[j].Enabled = true;
            }

            if (i == VB6Helpers.UBound(initObj.MODXANU.VxAnus))
            {
                initObj.Frmxanu.Boton[1].Enabled = false;
            }

            if (i == 0)
            {
                initObj.Frmxanu.Boton[0].Enabled = false;
            }
        }

        private static short Valida(InitializationObject initObj)
        {
            dynamic MsAnu = null;
            short n = 0;
            double to1 = 0;
            double to2 = 0;
            string NN = "";
            int Num = 0;
            string d = "";
            string dv = "";

            MsAnu = "Planillas de Anulación";

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_Fecha.Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar fecha presentación de la planilla.",
                    ControlName = "Tx_Fecha_Text"
                });
                return 0;
            }

            if (VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[2].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar tipo de anulación.",
                    ControlName = "Tx_TipoAnulacion_Text"
                });
                return 0;
            }

            if (VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[4].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar código plaza central que contabiliza.",
                    ControlName = "Tx_CodigoDos_Text"
                });
                return 0;
            }

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[7].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar rut.",
                    ControlName = "Tx_Rut_Text"
                });
                return 0;
            }

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[9].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Falta Código de Aduana.",
                    ControlName = "Tx_CodEntidadAutorizadaPlanillaAnulada_Text"
                });
                return 0;
            }

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[10].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar número de presentación.",
                    ControlName = "Tx_NumeroPresentacionAnulada_Text"
                });
                return 0;
            }

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[11].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar fecha de presentación.",
                    ControlName = "Tx_FechaPresentacionAnulada_Text"
                });
                return 0;
            }

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[13].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar tipo de operación.",
                    ControlName = "Tx_CodigoOperacionAnulada_Text"
                });
                return 0;
            }

            if (VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "607" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "401" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "402" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "403" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "407" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "500" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "501" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "502" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "570" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "601" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "603" && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[13].Text) != "511")
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "El tipo de Planilla no es válido.",
                    ControlName = "Tx_CodigoOperacionAnulada_Text"
                });
                return 0;
            }

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[15].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar plaza banco central.",
                    ControlName = "Tx_CodigoBancaCentralAnulada_Text"
                });
                return 0;
            }

            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[16].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar monto en dolares.",
                    ControlName = "Tx_MontoMMONAnulada_Text"
                });
                return 0;
            }

            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[17].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar paridad en dolares.",
                    ControlName = "Tx_ParidadMMONAnulada_Text"
                });
                return 0;
            }

            n = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[13].Text);

            if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[24].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar código de la moneda.",
                    ControlName = "Tx_CodigoMonedaAnulado_Text"
                });
                return 0;
            }

            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[25].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar monto anulado.",
                    ControlName = "Tx_MontoAnulado_Text"
                });
                return 0;
            }

            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[26].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar paridad a dolares.",
                    ControlName = "Tx_ParidadMMONMontoAnulado_Text"
                });
                return 0;
            }

            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[27].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar monto en dolares de la anulación.",
                    ControlName = "Tx_MontoMMONMontoAnulado_Text"
                });
                return 0;
            }

            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[28].Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar monto en dolares paridad original.",
                    ControlName = "Tx_MontoMMONParidadOriginalAnulado_Text"
                });
                return 0;
            }

            to1 = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[25].Text);
            to2 = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[16].Text);
            if (VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[24].Text) == 13)
            {
                if (to1 > to2)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "El monto a Anular debe ser menor o igual al Monto Anulado.",
                        ControlName = "Tx_MontoAnulado_Text"
                    });
                    return 0;
                }
            }

            if((!string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[10].Text) || initObj.Frmxanu.Tx_PlAnu[10].Text != null) && (!string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[11].Text) || initObj.Frmxanu.Tx_PlAnu[11].Text != null))
            {
                NN = VB6Helpers.Format(initObj.Frmxanu.Tx_PlAnu[10].Text, "0000000");
                Num = (int)VB6Helpers.Val(VB6Helpers.Mid(NN, 1, 6));
                d = VB6Helpers.Format(initObj.Frmxanu.Tx_PlAnu[11].Text, "yy");
                dv = VB6Helpers.Right(MODXPLN1.Fn_DigVer_xPlv(initObj.MODGSCE.VGen.CodPbc, VB6Helpers.CShort(MODGPYF1.Fn_GetN(initObj.Frmxanu.Tx_PlAnu[9].Text, initObj)), Num, (short)VB6Helpers.Val(VB6Helpers.Format(initObj.Frmxanu.Tx_PlAnu[11].Text, "yyyy"))), 1);
                if (dv != VB6Helpers.Trim(VB6Helpers.Right(NN, 1)))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "El Dígito verificador del número de presentación no es válido.",
                        ControlName = "Tx_NumeroPresentacionAnulada_Text"
                    });
                    return 0;
                }

            }
            if (Format.StringToDouble((initObj.Frmxanu.Tx_PlAnu[32].Text)) == 0 && initObj.Frmxanu.Tx_PlAnu[13].Text != "511" && initObj.Frmxanu.Tx_PlAnu[13].Text != "402")
            {
                initObj.Frmxanu.Tx_PlAnu[32].Enabled = true;
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar el tipo de cambio.",
                    ControlName = "Tx_TipoCambioAutorizacion_Text"
                });
                return 0;
            }

            if (initObj.Frmxanu.Cb_Autor.ListIndex != -1)
            {
                if (initObj.Frmxanu.Cb_Autor.get_ItemData_((short)initObj.Frmxanu.Cb_Autor.ListIndex) != 7)
                {
                    if (string.IsNullOrWhiteSpace(initObj.Frmxanu.Tx_PlAnu[30].Text))
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe ingresar número de autorización.",
                            ControlName = "Tx_NumeroAutorizacion_Text"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(initObj.Frmxanu.Tx_PlAnu[31].Text))
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe ingresar fecha de autorización.",
                            ControlName = "Tx_FechaAutorizacion_Text"
                        });
                        return 0;
                    }
                    else
                    {
                        if (~MODGPYF1.EsFecha2000(initObj.Frmxanu.Tx_PlAnu[31].ToString(), initObj, T_MODXANU.MsgxAnu) == 0)
                        {                            
                            return 0;
                        }


                    }

                }

            }

            return (short)(true ? -1 : 0);
        }

        private static void Trasp_Anu(short Indice, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            string X = "";
            string ru = "";
            short m = 0;

            X = initObj.Frmxanu.Tx_PlAnu[7].Text;
            if(!string.IsNullOrEmpty(X))
            {
                ru = VB6Helpers.Trim(VB6Helpers.Mid(X, 1, VB6Helpers.Len(X) - 2)) + VB6Helpers.Right(X, 1);
            }
            initObj.MODXANU.VxAnus[Indice].cencos = initObj.MODGUSR.UsrEsp.CentroCosto;
            initObj.MODXANU.VxAnus[Indice].codusr = initObj.MODGUSR.UsrEsp.Especialista;
            initObj.MODXANU.VxAnus[Indice].Fecing = DateTime.Now.ToString("dd/MM/yyyy");
            initObj.MODXANU.VxAnus[Indice].Estado = 1;

            initObj.MODXANU.VxAnus[Indice].codcct = initObj.MODXANU.VgAnu.codcct;
            initObj.MODXANU.VxAnus[Indice].codpro = initObj.MODXANU.VgAnu.codpro;
            initObj.MODXANU.VxAnus[Indice].codesp = initObj.MODXANU.VgAnu.codesp;
            initObj.MODXANU.VxAnus[Indice].codofi = initObj.MODXANU.VgAnu.codofi;
            initObj.MODXANU.VxAnus[Indice].codope = initObj.MODXANU.VgAnu.codope;

            initObj.MODXANU.VxAnus[Indice].TipAnu = VB6Helpers.CShort(initObj.Frmxanu.Tx_PlAnu[2].Text);
            initObj.MODXANU.VxAnus[Indice].PlzBcc = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[4].Text);

            initObj.MODXANU.VxAnus[Indice].RutExp = ru;
            initObj.MODXANU.VxAnus[Indice].PrtExp = Mdl_Funciones_Varias.PoneMarcaParty(ru);
            initObj.MODXANU.VxAnus[Indice].IndNom = 0;
            initObj.MODXANU.VxAnus[Indice].IndDir = 0;

            initObj.MODXANU.VxAnus[Indice].EntAut = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[9].Text);
            initObj.MODXANU.VxAnus[Indice].NumpreO = initObj.Frmxanu.Tx_PlAnu[10].Text;
            initObj.MODXANU.VxAnus[Indice].FecpreO = initObj.Frmxanu.Tx_PlAnu[11].Text;
            initObj.MODXANU.VxAnus[Indice].TipPln = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[13].Text);

            initObj.MODXANU.VxAnus[Indice].CodPbc = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[15].Text);
            initObj.MODXANU.VxAnus[Indice].CodAdn = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[19].Text);
            initObj.MODXANU.VxAnus[Indice].numdec = initObj.Frmxanu.Tx_PlAnu[20].Text;
            initObj.MODXANU.VxAnus[Indice].FecDec = initObj.Frmxanu.Tx_PlAnu[21].Text;
            initObj.MODXANU.VxAnus[Indice].FecVen = initObj.Frmxanu.Tx_PlAnu[22].Text;

            m = MODGTAB0.Get_VMndBC(initObj.MODGTAB0, unit, (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[24].Text));
            initObj.MODXANU.VxAnus[Indice].CodMnd = initObj.MODGTAB0.VMnd[m].Mnd_MndCod;

            initObj.MODXANU.VxAnus[Indice].MtoDol = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[16].Text);
            initObj.MODXANU.VxAnus[Indice].Mtopar = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[17].Text);
            initObj.MODXANU.VxAnus[Indice].MtoAnu = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[25].Text);
            initObj.MODXANU.VxAnus[Indice].MtoParA = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[26].Text);
            initObj.MODXANU.VxAnus[Indice].MtoDolA = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[27].Text);
            initObj.MODXANU.VxAnus[Indice].MtoDolPo = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[28].Text);
            initObj.MODXANU.VxAnus[Indice].ObsPln = initObj.Frmxanu.Tx_PlAnu[29].Text;

            if (initObj.Frmxanu.Cb_Autor.ListIndex != -1)
            {
                if (initObj.Frmxanu.Cb_Autor.get_ItemData_((short)initObj.Frmxanu.Cb_Autor.ListIndex) != 7)
                {
                    initObj.MODXANU.VxAnus[Indice].TipAut = initObj.Frmxanu.Cb_Autor.get_List((short)initObj.Frmxanu.Cb_Autor.ListIndex);
                }
                else
                {
                    initObj.MODXANU.VxAnus[Indice].TipAut = "";
                }

            }

            if(!string.IsNullOrEmpty(initObj.MODXANU.VxAnus[Indice].TipAut))
            {
                initObj.MODXANU.VxAnus[Indice].NroAut = Format.StringToDouble((initObj.Frmxanu.Tx_PlAnu[30].Text));
                initObj.MODXANU.VxAnus[Indice].FecAut = initObj.Frmxanu.Tx_PlAnu[31].Text;
            }
            else
            {
                initObj.MODXANU.VxAnus[Indice].NroAut = 0;
                initObj.MODXANU.VxAnus[Indice].FecAut = "";
            }

            initObj.MODXANU.VxAnus[Indice].TipCam = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[32].Text);
            initObj.MODXANU.VxAnus[Indice].PlnOK = (short)(true ? -1 : 0);  //PACP
        }

        private string DigV_NumE(short CodsBCH, double NumInf, string AnoInf)
        {
            string n = "";
            short Sum = 0;
            short Con = 0;
            short i = 0;
            short Ent = 0;
            short Den = 0;
            short dif = 0;
            short div = 0;
            string Dig = "";

            n = VB6Helpers.Format(VB6Helpers.CStr(CodsBCH), "00") + VB6Helpers.Format(VB6Helpers.CStr(NumInf), "000000") + VB6Helpers.Format(AnoInf, "00");

            Sum = 0;
            Con = 5;
            for (i = 1; i <= 10; i++)
            {
                Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(n, i, 1)) * Con);
                Con = (short)(Con - 1);
                if (Con == 1)
                {
                    Con = 7;
                }
            }

            Ent = (short)VB6Helpers.Int(Sum / 11);
            Den = (short)(Ent * 11);
            dif = (short)(Sum - Den);
            div = (short)(11 - dif);
            Dig = VB6Helpers.Str(div);

            if (div == 11)
            {
                Dig = "0";
            }

            if (div == 10)
            {
                Dig = "K";
            }

            return Dig;
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
                case 11: DvCal = "0";
                    break;
                case 10: DvCal = "K";
                    break;
                default: DvCal = VB6Helpers.Format(VB6Helpers.CStr(es));
                    break;
            }

            _retValue = 0;
            if (DvCal == DvRut)
            {
                return -1;
            }

            return _retValue;
        }

        private static string For_cam(short i, InitializationObject initObj)
        {
            string X = "";
            X = VB6Helpers.Format(initObj.Frmxanu.Tx_PlAnu[i].Text, "000");
            return X;
        }

        //private static int msg_err(string Frase, short Indice, InitializationObject initObj)
        //{
        //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
        //    {
        //        Type = TipoMensaje.Error,
        //        Text = "No existe Código.",
        //    });
        //    initObj.Frmxanu.Tx_PlAnu[Indice].Text = "";
        //    return 0;
        //}

        private static short PlnNoEsRut(string rut)
        {
            string D_V = "";
            string b = "";
            short i = 0;
            string a = "";
            short aa = 0;
            short suma = 0;
            short es = 0;
            string DvCal = "";

            D_V = VB6Helpers.Right(rut, 1);
            b = VB6Helpers.Left(rut, VB6Helpers.Len(rut) - 1);

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
                case 11: DvCal = "0";
                    break;
                case 10: DvCal = "K";
                    break;
                default: DvCal = VB6Helpers.Format(VB6Helpers.CStr(es));
                    break;
            }

            if (DvCal != VB6Helpers.UCase(D_V))
            {
                return (short)(true ? -1 : 0);
            }

            return 0;
        }

        private static void Pr_Cargar_Autorizacion(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            //T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
            initObj.Frmxanu.Cb_Autor.Items.Clear();

            Mdl_Funciones_Varias.SyGet_TipAut(initObj, unit);
            MODPREEM.CargaEnLista_TipAut(initObj.Mdl_Funciones_Varias, initObj.Frmxanu.Cb_Autor);
            initObj.Frmxanu.Cb_Autor.ListIndex = -1;
        }

        private void Tx_Fecha_GotFocus(InitializationObject initObj)
        {
            MODGPYF1.selTexto(initObj.Frmxanu.Tx_Fecha);
        }

        private void Tx_Fecha_LostFocus(InitializationObject iniObject)
        {
            if(!string.IsNullOrEmpty(iniObject.Frmxanu.Tx_Fecha.Text))
            {
                if (~MODGPYF1.EsFecha2000(iniObject.Frmxanu.Tx_Fecha.ToString(), iniObject, T_MODXANU.MsgxAnu) != 0)
                {
                    return;
                }
            }
        }

        public static void Tx_PlAnu_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow, short Index)
        {

            string X = "";
            short z = 0;
            dynamic NumInf = null;
            short c = 0;
            short n = 0;
            short m = 0;
            string rut = "";
            string Rutax = "";
            string T = "";
            string s1 = "";
            string s2 = "";
            string s3 = "";
            short MN = 0;
            double mto = 0;
            double Par = 0;
            double MUS = 0;

            if (VB6Helpers.Instr(Tx_valores, VB6Helpers.Format(VB6Helpers.CStr(Index), "00")) != 0)
            {
                initObj.Frmxanu.Tx_PlAnu[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(MODGPYF1.ValTexto(initObj.Frmxanu.Tx_PlAnu[Index].Text, initObj)), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[Index]));
            }
            //------------------------------------------------------------------------
            // SE REALIZAN FORMATOS Y VALIDACIONES.-
            //------------------------------------------------------------------------
            switch (Index)
            {
                case 10:
                case 20:
                    if(!string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[Index].Text))
                    {
                        X = VB6Helpers.Format(initObj.Frmxanu.Tx_PlAnu[Index].Text, "        ");
                        z = (short)(7 - VB6Helpers.Len(VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[Index].Text)));
                        NumInf = MODGPYF1.Zeros(z) + VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[Index].Text);
                    }

                    break;
                case 1:
                case 11:
                case 21:
                case 22:
                case 31:
                    if (~MODGPYF1.EsFecha2000(initObj.Frmxanu.Tx_PlAnu[Index].Text, initObj, T_MODXANU.MsgxAnu) != 0)
                    {
                        return;
                    }
                    break;
                case 32:  //PACP
                    if (VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[32].Text) > 0)
                    {
                        initObj.Frmxanu.Tx_PlAnu[32].Text = VB6Helpers.Format(VB6Helpers.CStr(MODGPYF1.ValTexto(initObj.Frmxanu.Tx_PlAnu[32].Text, initObj)), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[32]));
                    }

                    break;
            }

            //------------------------------------------------------------------------
            // LLENAR CON VALORES LAS CAJAS TEXTO.-
            //------------------------------------------------------------------------
            if (VB6Helpers.Instr(Tx_Notmodi, VB6Helpers.Format(VB6Helpers.CStr(Index), "00")) == 0)
            {
                if(!string.IsNullOrWhiteSpace(initObj.Frmxanu.Tx_PlAnu[Index].Text) && VB6Helpers.Trim(initObj.Frmxanu.Tx_PlAnu[Index].Text) != "0")
                {
                    switch (Index)
                    {
                        case 2:
                            initObj.Frmxanu.Tx_PlAnu[2].Text = For_cam(2, initObj);
                            if (Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[2].Text) != 85 && Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[2].Text) != 95)
                            {
                                //   c = msg_err("Tipo de Anulación", Index); //Error
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "No existe Tipo de Anulación.",
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_TipoAnulacion_Text"
                                });
                                initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                            }

                            break;
                        case 4:
                            initObj.Frmxanu.Tx_PlAnu[3].Text = "";
                            initObj.Frmxanu.Tx_PlAnu[4].Text = For_cam(4, initObj);
                            n = VB6Helpers.CShort(initObj.Frmxanu.Tx_PlAnu[4].Text);
                            m = MODGTAB1.Get_VPbc(initObj, uow, n);
                            if (m >= 0)
                            {
                                initObj.Frmxanu.Tx_PlAnu[3].Text = VB6Helpers.Trim(initObj.MODGTAB1.VPbc[m].Pbc_PbcDes);
                            }
                            else
                            {
                                //c = msg_err("Plaza Banco Central", Index, initObj);
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "No existe Plaza Banco Central.",
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_CodigoDos_Text"
                                });
                                initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                            }

                            break;
                        case 7:
                            initObj.Frmxanu.Tx_PlAnu[5].Text = "";
                            initObj.Frmxanu.Tx_PlAnu[6].Text = "";
                            rut = initObj.Frmxanu.Tx_PlAnu[7].Text;
                            rut = MODGPYF0.Componer(rut, "-", "");
                            if(!string.IsNullOrEmpty(rut))
                            {
                                if (PlnNoEsRut((rut)) != 0)
                                {
                                    // VB6Helpers.MsgBox(Module1.GPrt_ErrRut, (MsgBoxStyle)MODGPYF0.pito(48), Module1.GPrt_Caption);

                                }
                                else
                                {
                                    Rutax = VB6Helpers.Right("000000000" + rut, 9);
                                    Rutax = VB6Helpers.Mid(rut, 1, VB6Helpers.Len(rut) - 1) + "-" + VB6Helpers.Right(rut, 1);
                                    initObj.Frmxanu.Tx_PlAnu[7].Text = Rutax;
                                    initObj.Frmxanu.Tx_PlAnu[5].Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObj, uow, rut, 0, 0, "N"));
                                    initObj.Frmxanu.Tx_PlAnu[6].Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObj, uow, rut, 0, 0, "D"));
                                    if (string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[5].Text))
                                    {
                                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Type = TipoMensaje.Informacion,
                                            Text = " No existe participante.",
                                            Title = T_MODXANU.MsgxAnu,
                                            ControlName = "Tx_Rut_Text"
                                        });
                                    }
                                }
                            }

                            break;
                        case 9:
                            initObj.Frmxanu.Tx_PlAnu[8].Text = "";
                            initObj.Frmxanu.Tx_PlAnu[9].Text = For_cam(9, initObj);
                            n = VB6Helpers.CShort(initObj.Frmxanu.Tx_PlAnu[9].Text);
                            m = MODGTAB0.Get_Bco(initObj, uow, n);                            
                            if (m >= 0)
                            {
                                initObj.Frmxanu.Tx_PlAnu[8].Text = VB6Helpers.Trim(initObj.MODGTAB0.VBco[m].NomBco);
                            }
                            else
                            {
                                //c = msg_err("Entidad Autorizada", Index);
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "No existe Entidad Autorizada.",
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_CodEntidadAutorizadaPlanillaAnulada_Text"
                                });
                                initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                            }

                            break;
                        case 13:  //Tipo de Operación
                            initObj.Frmxanu.Tx_PlAnu[12].Text = "";
                            initObj.Frmxanu.Tx_PlAnu[13].Text = For_cam(13, initObj);
                            n = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[13].Text);
                            T = VB6Helpers.Trim(MODXPLN1.GetNomPLn(n));
                            if(!string.IsNullOrEmpty(T))
                            {
                                initObj.Frmxanu.Tx_PlAnu[12].Text = T;
                            }
                            else
                            {
                                //c = msg_err("Tipo de Operación", Index);
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "No existe Código Tipo de Operación.",
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_CodigoOperacionAnulada_Text"
                                });
                                initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                            }

                            break;
                        case 15:
                            initObj.Frmxanu.Tx_PlAnu[15].Text = For_cam(15, initObj);
                            n = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[15].Text);
                            m = MODGTAB1.Get_VPbc(initObj, uow, n);                           
                            if (m >= 0)
                            {
                                initObj.Frmxanu.Tx_PlAnu[14].Text = VB6Helpers.Trim(initObj.MODGTAB1.VPbc[m].Pbc_PbcDes);
                            }
                            else
                            {
                                //c = msg_err("Plaza Banco Central", Index, initObj);    
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "No existe Código Plaza Banco Central.",
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_CodigoBancaCentralAnulada_Text"
                                });
                                initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                            }

                            break;
                        case 19:
                            initObj.Frmxanu.Tx_PlAnu[19].Text = For_cam(19, initObj);
                            n = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[19].Text);
                            m = MODGTAB1.Get_VAdn(initObj.MODGTAB1, uow, n);
                            initObj.Frmxanu.Tx_PlAnu[Index].Text = string.Empty;
                            if (m >= 0)                                              
                                initObj.Frmxanu.Tx_PlAnu[18].Text = VB6Helpers.Trim(initObj.MODGTAB1.VAdn[m].NomAdn);
                           
                            else
                            {
                                // c = msg_err("Aduana", Index);
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "No existe Aduana.",
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_CodigoExportacion_Text"
                                });                              
                            }

                            break;
                        case 20:  //Número de Aceptación
                            if(!string.IsNullOrEmpty(initObj.Frmxanu.Tx_PlAnu[19].Text))
                            {
                                s1 = VB6Helpers.Format(initObj.Frmxanu.Tx_PlAnu[19].Text, "000");
                                z = (short)(7 - VB6Helpers.Len(initObj.Frmxanu.Tx_PlAnu[20].Text));
                                s2 = MODGPYF1.Zeros(z) + initObj.Frmxanu.Tx_PlAnu[20].Text;
                                s3 = s1 + s2;
                                c = EsRut(s3);
                                if (~c != 0)
                                {
                                    // VB6Helpers.MsgBox("Debe corregir el Código de Aduana o el Dígito Verificador del Número de la Declaración. Alguno de ellos no está correcto.", (MsgBoxStyle)MODGPYF0.pito(48), MODXANU.MsgxAnu);
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Debe corregir el Código de Aduana o el Dígito Verificador del Número de la Declaración. Alguno de ellos no está correcto.",
                                        Type = TipoMensaje.Informacion,
                                        Title = T_MODXANU.MsgxAnu,
                                        ControlName = "Tx_NumeroAceptacionExportacion_Text"
                                    });
                                    initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                                    return;
                                }

                            }

                            z = (short)(7 - VB6Helpers.Len(initObj.Frmxanu.Tx_PlAnu[20].Text));
                            initObj.Frmxanu.Tx_PlAnu[20].Text = MODGPYF1.Zeros(z) + VB6Helpers.UCase(initObj.Frmxanu.Tx_PlAnu[20].Text);

                            break;
                        case 24:
                            initObj.Frmxanu.Tx_PlAnu[24].Text = For_cam(24, initObj);
                            n = (short)VB6Helpers.Val(initObj.Frmxanu.Tx_PlAnu[24].Text);
                            m = MODGTAB0.Get_VMndBC(initObj.MODGTAB0, uow, n);
                            if (m != 0)
                            {
                                initObj.Frmxanu.Tx_PlAnu[23].Text = initObj.MODGTAB0.VMnd[m].Mnd_MndNom;
                                MN = initObj.MODGTAB0.VMnd[m].Mnd_MndCod;
                                if (MN != 11)
                                {
                                    initObj.Frmxanu.Tx_PlAnu[26].Text = VB6Helpers.CStr(MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, MN, DateTime.Now.ToString("dd/mm/yyyy"), "P"));
                                }
                                else
                                {
                                    initObj.Frmxanu.Tx_PlAnu[26].Text = "0";
                                }

                            }
                            else
                            {
                                // c = msg_err("Moneda Banco de Chile", Index);
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "Debe corregir Moneda Banco de Chile.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_MODXANU.MsgxAnu,
                                    ControlName = "Tx_CodigoMonedaAnulado_Text"
                                });
                                initObj.Frmxanu.Tx_PlAnu[Index].Text = "";
                                return;

                            }

                            break;
                    }

                }

            }
            //------------------------------------------------------------------------
            // REALIZO CALCULO SOBRE LA PLANILLA.-
            //------------------------------------------------------------------------
            if (VB6Helpers.Instr(Tx_Notmodi, VB6Helpers.Format(VB6Helpers.CStr(Index), "00")) == 0)
            {

                mto = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[25].Text);
                Par = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[26].Text);
                if (mto > 0 && Par > 0)
                {
                    MUS = (mto / Par);
                    initObj.Frmxanu.Tx_PlAnu[27].Text = VB6Helpers.Format(VB6Helpers.CStr(MUS), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[27]));
                }
                else
                {
                    initObj.Frmxanu.Tx_PlAnu[27].Text = "0";
                }

                Par = Format.StringToDouble(initObj.Frmxanu.Tx_PlAnu[17].Text);
                if (mto > 0 && Par > 0)
                {
                    MUS = (mto / Par);
                    initObj.Frmxanu.Tx_PlAnu[28].Text = VB6Helpers.Format(VB6Helpers.CStr(MUS), MODGPYF1.DecObjeto(initObj.Frmxanu.Tx_PlAnu[28]));
                }
                else
                {
                    initObj.Frmxanu.Tx_PlAnu[28].Text = "0";
                }

            }

        }
    }
}