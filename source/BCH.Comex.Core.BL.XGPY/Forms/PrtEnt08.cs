using System;
using System.Collections.Generic;
using CodeArchitects.VB6Library;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public static class PrtEnt08
    {
        public static void Aceptar_Click(InitializationObject InitObject, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("PrtEnt08 Aceptar_Click Comisiones"))
            {
                try
                {
                    string l1 = string.Empty;
                    string l2 = string.Empty;
                    int fin = 0;
                    int tope = 0;
                    string ind = string.Empty;
                    string est = string.Empty;
                    string lista = string.Empty;
                    int i = 0;
                    string espec = string.Empty;
                    string v = string.Empty;
                    int j = 0;
                    int fila = -1;
                    int z = 0; //JF

                    // ******************************* MPN ************************************
                    if (T_PRTGLOB.Pertenece == 0)
                    {
                        InitObject.Mdi_Principal.Archivo[2].Enabled = false;  // menú salvar
                        InitObject.Mdi_Principal.Archivo[4].Enabled = false;  // menú eliminar
                        InitObject.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;      // boton
                    }
                    else
                    {
                        if (T_MODWS.ACCESO == "1")
                        {
                            InitObject.Mdi_Principal.Archivo[2].Enabled = true;  // menú salvar
                            InitObject.Mdi_Principal.Archivo[4].Enabled = true;  // menú eliminar
                            InitObject.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;      // boton salvar o guardar
                        }
                    }

                    if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
                        l1 = InitObject.PrtEnt08.Lista1.Items[0].Value;
                    if (InitObject.PrtEnt08.Lista2.Items.Count > 0)
                        l2 = InitObject.PrtEnt08.Lista2.Items[0].Value;

                    switch (InitObject.PRTGLOB.Party.tipo)
                    {
                        case T_PRTGLOB.tipo_cliente:
                            if (string.IsNullOrEmpty(UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1)) && string.IsNullOrEmpty(UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1)))
                            {
                                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = " Debe ingresar por lo menos una Cuenta Corriente.",
                                    Title = T_PRTGLOB.TitCuentas,
                                    Type = TipoMensaje.Informacion
                                });

                                return;
                            }
                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCuentas) == 0)
                            {
                                InitObject.PRTGLOB.Party.Flag = InitObject.PRTGLOB.Party.Flag + 4;
                                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.FlagParty))
                                {
                                    InitObject.PRTGLOB.Party.PrtGlob.FlagParty = Convert.ToInt32(true);

                                    if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                        InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                                }
                            }

                            //if (UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1) != "" && UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1) != "")
                            if (!string.IsNullOrEmpty(UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1)) && !string.IsNullOrEmpty(UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1)))
                            {
                                fin = -1;
                                tope = InitObject.PrtEnt08.Lista1.Items.Count + InitObject.PrtEnt08.Lista2.Items.Count - 3;
                                InitObject.PRTGLOB.ctaclie = new prtyccta[tope + 1];
                                z = 0;

                                foreach (prtyccta cta in InitObject.PRTGLOB.ctaclie)
                                {
                                    InitObject.PRTGLOB.ctaclie[z] = new prtyccta();
                                    z++;
                                }

                                PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista1, 1);
                                PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista2, 2);
                                //if (T_MODWS.ACCESO == "1")

                                if (InitObject.UsrEsp.Jerarquia == 1 || InitObject.UsrEsp.Tipeje == "N") //if (InitObject.UsrEsp.Jerarquia == 1)
                                {
                                    if (string.IsNullOrEmpty(InitObject.PRTGLOB.Party.ejecutivo.Trim().ToStr()))
                                    {
                                        //PRTYENT.llama07(InitObject, uow);
                                        InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = true;
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1)))
                                {
                                    fin = -1;
                                    tope = InitObject.PrtEnt08.Lista1.Items.Count - 2;
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[tope + 1];
                                    z = 0;

                                    foreach (prtyccta cta in InitObject.PRTGLOB.ctaclie)
                                    {
                                        InitObject.PRTGLOB.ctaclie[z] = new prtyccta();
                                        z++;
                                    }

                                    PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista1, 1);

                                    if (InitObject.PRTGLOB.Party.ejecutivo == "")
                                    {
                                        //PRTYENT.llama07(InitObject, uow);
                                        InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = true;
                                    }
                                }
                                if (!string.IsNullOrEmpty(UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1)))
                                {
                                    fin = -1;
                                    tope = InitObject.PrtEnt08.Lista2.Items.Count - 2;
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[tope + 1];
                                    z = 0;

                                    foreach (prtyccta cta in InitObject.PRTGLOB.ctaclie)
                                    {
                                        InitObject.PRTGLOB.ctaclie[z] = new prtyccta();
                                        z++;
                                    }

                                    PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista2, 2);

                                    if (InitObject.PRTGLOB.Party.ejecutivo == "")
                                    {
                                        //PRTYENT.llama07(InitObject, uow);
                                        InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = true;
                                    }
                                }
                            }

                            for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                            {
                                if (InitObject.PRTGLOB.ctaclie[i].estado == T_PRTGLOB.eliminado_nuevo || InitObject.PRTGLOB.ctaclie[i].estado == T_PRTGLOB.eliminado_leido || InitObject.PRTGLOB.ctaclie[i].estado == T_PRTGLOB.eliminado_modificado)
                                    InitObject.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 1;
                                else
                                {
                                    InitObject.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 0;
                                    break;
                                }
                            }

                            break;

                        case T_PRTGLOB.tipo_banco:
                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == T_PRTGLOB.Gprt_FlagCorresponsal && UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1) == "")
                            {
                                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = " Debe ingresar por lo menos una Cuenta Corriente en banco corresponsal.",
                                    Title = T_PRTGLOB.TitCtaLin,
                                    Type = TipoMensaje.Informacion
                                });

                                return;
                            }
                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == T_PRTGLOB.Gprt_FlagAcreedor && UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1) == "")
                            {
                                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = " Debe ingresar por lo menos una línea de crédito en banco acreedor.",
                                    Title = T_PRTGLOB.TitCtaLin,
                                    Type = TipoMensaje.Informacion
                                });

                                return;
                            }

                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == 0 && (InitObject.PrtEnt08.Lista1.Items.Count != 0 && UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1) != ""))
                            {
                                InitObject.PRTGLOB.Party.Flag = InitObject.PRTGLOB.Party.Flag + 8;

                                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.FlagParty))
                                {
                                    InitObject.PRTGLOB.Party.PrtGlob.FlagParty = 1;

                                    if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                        InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                                }
                            }

                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == 0 && (InitObject.PrtEnt08.Lista2.Items.Count != 0 && UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1) != ""))
                            {
                                InitObject.PRTGLOB.Party.Flag = InitObject.PRTGLOB.Party.Flag + 16;

                                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.FlagParty))
                                {
                                    InitObject.PRTGLOB.Party.PrtGlob.FlagParty = 1;
                                    if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                        InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;

                                }
                            }

                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == T_PRTGLOB.Gprt_FlagCorresponsal && UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1) != "")
                            {
                                fin = -1;
                                tope = InitObject.PrtEnt08.Lista1.Items.Count - 2;
                                InitObject.PRTGLOB.ctabancos = new prtybcta[tope + 1];
                                InitObject.PrtEnt08.Lista1.ListIndex = -1;

                                for (j = 0; j <= InitObject.PrtEnt08.Lista1.Items.Count - 1; j += 1)
                                {
                                    lista = InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value.ToString();
                                    ind = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                                    i = PRTYENT.BuscaIndices(InitObject, "ctabancos", ind);
                                    v = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                                    est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                                    espec = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                                    InitObject.PRTGLOB.ctabancos[i].especial = Convert.ToInt32(espec == "0"); 
                                    InitObject.PRTGLOB.ctabancos[i].cuenta = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);
                                    InitObject.PRTGLOB.ctabancos[i].indice = Convert.ToInt32(ind);
                                    InitObject.PRTGLOB.ctabancos[i].estado = Convert.ToInt32(v);
                                    InitObject.PRTGLOB.ctabancos[i].moneda = InitObject.PrtEnt08.Lista1.Text.ToString();

                                    InitObject.PRTGLOB.ctabancos[i].activa = est == "Activa" ? 1 : 0;

                                    if (InitObject.PrtEnt08.Lista1.ListIndex < InitObject.PrtEnt08.Lista1.Items.Count - 2)
                                        InitObject.PrtEnt08.Lista1.ListIndex = InitObject.PrtEnt08.Lista1.ListIndex + 1;
                                }

                                for (i = 0; i <= InitObject.PRTGLOB.ctabancos.GetUpperBound(0); i += 1)
                                {
                                    if (InitObject.PRTGLOB.ctabancos[i].estado == T_PRTGLOB.eliminado_nuevo || InitObject.PRTGLOB.ctabancos[i].estado == T_PRTGLOB.eliminado_leido || InitObject.PRTGLOB.ctabancos[i].estado == T_PRTGLOB.eliminado_modificado)
                                    {
                                        T_PRTGLOB.bctas_eliminadas = Convert.ToInt32(true);
                                        InitObject.PRTGLOB.Party.PrtGlob.bctas_eliminadas = 1;
                                    }
                                    else
                                    {
                                        T_PRTGLOB.bctas_eliminadas = Convert.ToInt32(false);
                                        InitObject.PRTGLOB.Party.PrtGlob.bctas_eliminadas = 0;
                                        break;
                                    }
                                }
                            }

                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == T_PRTGLOB.Gprt_FlagAcreedor && UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1) != "")
                            {
                                fin = -1;
                                tope = InitObject.PrtEnt08.Lista2.Items.Count - 2;
                                InitObject.PRTGLOB.linbancos = new prtyblinea[tope + 1];
                                InitObject.PrtEnt08.Lista2.ListIndex = -1;

                                for (j = 0; j <= InitObject.PrtEnt08.Lista2.Items.Count - 1; j += 1)
                                {
                                    //lista = InitObject.PrtEnt08.Lista2.SelectedValue.ToString();
                                    lista = InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.SelectedValue].Value.ToString();
                                    ind = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                                    i = PRTYENT.BuscaIndices(InitObject, "linbancos", ind);
                                    v = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                                    est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                                    InitObject.PRTGLOB.linbancos[i].linea = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);
                                    InitObject.PRTGLOB.linbancos[i].indice = Convert.ToInt32(ind);
                                    InitObject.PRTGLOB.linbancos[i].estado = Convert.ToInt32(v);
                                    InitObject.PRTGLOB.linbancos[i].moneda = InitObject.PrtEnt08.Lista2.Text.ToString();
                                    InitObject.PRTGLOB.linbancos[i].activa = est == "Activa" ? 1 : 0;

                                    if (InitObject.PrtEnt08.Lista2.ListIndex < InitObject.PrtEnt08.Lista2.Items.Count - 2)
                                        InitObject.PrtEnt08.Lista2.ListIndex = InitObject.PrtEnt08.Lista2.ListIndex + 1;
                                    
                                }
                                for (i = 0; i <= InitObject.PRTGLOB.linbancos.GetUpperBound(0); i += 1)
                                {
                                    if (InitObject.PRTGLOB.linbancos[i].estado == T_PRTGLOB.eliminado_nuevo || InitObject.PRTGLOB.linbancos[i].estado == T_PRTGLOB.eliminado_leido || InitObject.PRTGLOB.linbancos[i].estado == T_PRTGLOB.eliminado_modificado)
                                        T_PRTGLOB.blin_eliminadas = 1;
                                    else
                                    {
                                        T_PRTGLOB.blin_eliminadas = 0;
                                        break;
                                    }
                                }
                            }

                            if (InitObject.PRTGLOB.Party.codbco == 0)
                                PRTYENT.llama11(InitObject);

                            break;

                        case T_PRTGLOB.individuo:
                            if (string.IsNullOrEmpty(UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1)) && string.IsNullOrEmpty(UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1)))
                            {
                                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = " Debe ingresar por lo menos una Cuenta Corriente.",
                                    Title = T_PRTGLOB.TitCuentas,
                                    Type = TipoMensaje.Informacion
                                });

                                return;
                            }

                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCuentas) == 0)
                            {
                                InitObject.PRTGLOB.Party.Flag = InitObject.PRTGLOB.Party.Flag + 4;

                                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.FlagParty))
                                {
                                    InitObject.PRTGLOB.Party.PrtGlob.FlagParty = Convert.ToInt32(true);
                                    if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                        InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                                    
                                }
                            }

                            //if (UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1) != "" && UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1) != "")
                            if (!string.IsNullOrEmpty(UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1)) && !string.IsNullOrEmpty(UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1)))
                            {
                                fin = -1;
                                tope = InitObject.PrtEnt08.Lista1.Items.Count + InitObject.PrtEnt08.Lista2.Items.Count - 3;
                                InitObject.PRTGLOB.ctaclie = new prtyccta[tope + 1];
                                z = 0;

                                foreach (prtyccta cta in InitObject.PRTGLOB.ctaclie)
                                {
                                    InitObject.PRTGLOB.ctaclie[z] = new prtyccta();
                                    z++;
                                }

                                PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista1, 1);
                                PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista2, 2);
                                //if (T_MODWS.ACCESO == "1")

                                if (InitObject.UsrEsp.Jerarquia == 1 || InitObject.UsrEsp.Tipeje == "N") //if (InitObject.UsrEsp.Jerarquia == 1)
                                {
                                    if (string.IsNullOrEmpty(InitObject.PRTGLOB.Party.ejecutivo.Trim().ToStr()))
                                    {
                                        //PRTYENT.llama07(InitObject, uow);
                                        InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = true;
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(UTILES.copiardestring(l1, VB6Helpers.Chr(9), 1)))
                                {
                                    fin = -1;
                                    tope = InitObject.PrtEnt08.Lista1.Items.Count - 2;
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[tope + 1];
                                    z = 0;

                                    foreach (prtyccta cta in InitObject.PRTGLOB.ctaclie)
                                    {
                                        InitObject.PRTGLOB.ctaclie[z] = new prtyccta();
                                        z++;
                                    }

                                    PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista1, 1);
                                    
                                    if (InitObject.PRTGLOB.Party.ejecutivo == "")
                                    {
                                        //PRTYENT.llama07(InitObject, uow);
                                        InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = true;
                                    }
                                }

                                if (!string.IsNullOrEmpty(UTILES.copiardestring(l2, VB6Helpers.Chr(9), 1)))
                                {
                                    fin = -1;
                                    tope = InitObject.PrtEnt08.Lista2.Items.Count - 2;
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[tope + 1];
                                    z = 0;

                                    foreach (prtyccta cta in InitObject.PRTGLOB.ctaclie)
                                    {
                                        InitObject.PRTGLOB.ctaclie[z] = new prtyccta();
                                        z++;
                                    }

                                    PRTYENT.AceptaLista(InitObject, InitObject.PrtEnt08.Lista2, 2);

                                    if (InitObject.PRTGLOB.Party.ejecutivo == "")
                                    {
                                        //PRTYENT.llama07(InitObject, uow);
                                        InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = true;
                                    }
                                }
                            }

                            for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                            {
                                if (InitObject.PRTGLOB.ctaclie[i].estado == T_PRTGLOB.eliminado_nuevo || InitObject.PRTGLOB.ctaclie[i].estado == T_PRTGLOB.eliminado_leido || InitObject.PRTGLOB.ctaclie[i].estado == T_PRTGLOB.eliminado_modificado)
                                    InitObject.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 1;
                                else
                                {
                                    InitObject.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 0;
                                    break;
                                }
                            }

                            break;
                    }

                    if (InitObject.PRTGLOB.Party.estado != T_PRTGLOB.nuevo)
                        InitObject.PRTGLOB.Party.estado = 5;

                    if (InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas)
                        InitObject.PaginaWebQueAbrir = "CaracteristicasParticipante";
                    
                    else
                        InitObject.PaginaWebQueAbrir = "Index";
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta PrtEnt08 Aceptar_Click", ex);
                    throw;
                }
            }
        }

        public static void Form_Load(InitializationObject InitObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer("PRTENT08 - Form_Load"))
            {
                int[] tabul = null, tabula = null,  tabu = null;
                int a = 0;
                tabu = new int[4];
                tabu[0] = 110;
                tabu[1] = 210;
                tabu[2] = 450;     // para mantener el estado de las cuentas
                tabu[3] = 470;     // para mantener la secuencia de la cuenta
                tabula = new int[5];
                tabula[0] = 110;
                tabula[1] = 210;
                tabula[2] = 450;     // para mantener el estado de las cuentas
                tabula[3] = 470;     // para mantener la secuencia de la cuenta
                tabula[4] = 500;
                tabul = new int[5];
                tabul[0] = 60;
                tabul[1] = 120;
                tabul[2] = 210;
                tabul[3] = 450;     // para mantener el estado de las cuentas
                tabul[4] = 470;     // para mantener la secuencia de la cuenta

                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        a = UTILES.seteaTabulador(InitObject.PrtEnt08.Lista1, tabu);
                        a = UTILES.seteaTabulador(InitObject.PrtEnt08.Lista2, tabul);
                        break;

                    case T_PRTGLOB.tipo_banco:
                        a = UTILES.seteaTabulador(InitObject.PrtEnt08.Lista1, tabula);
                        a = UTILES.seteaTabulador(InitObject.PrtEnt08.Lista1, tabu);
                        break;
                }

                InitObject.PrtEnt08.Label7.Visible = false; //LABEL OFICINA

                if (InitObject.UsrEsp.Jerarquia != 1 && InitObject.UsrEsp.Tipeje != "N")
                {
                    InitObject.PrtEnt08.Lista1.Enabled = false;
                    InitObject.PrtEnt08.Lista2.Enabled = false;
                    InitObject.PrtEnt08.aceptar.Enabled = false;
                }
                else
                {
                    InitObject.PrtEnt08.Lista1.Enabled = true;
                    InitObject.PrtEnt08.Lista2.Enabled = true;
                    InitObject.PrtEnt08.aceptar.Enabled = true;
                }

                if (InitObject.MODWS.CtaCCOL.Length == 0)
                {
                    string msgreto = string.Empty, rut = string.Empty, rutCliente = string.Empty;

                    if (T_MODWS.FLAGOPEN == "ABIERTO")
                    {
                        msgreto = MODWS.MensajeWs(InitObject, "SALIR", "");
                        MODWS.LlamaServerWS();
                    }
                    else
                        MODWS.LlamaServerWS();

                    rutCliente = (!string.IsNullOrEmpty(InitObject.PRTGLOB.Party.rut.Trim())) ? InitObject.PRTGLOB.Party.rut.Trim() : InitObject.PRTGLOB.Party.idparty;
                    rut = MODWS.ExtraeRut(rutCliente);
                    InitObject.PRTGLOB.Party.rut = rut;

                    var cuentasPorRut = new XgpyService().ObtenerCuentasPorRut(rut, ref msgreto);

                    if (cuentasPorRut == null)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = !string.IsNullOrEmpty(msgreto) ? msgreto : "Existe un problema con el servicio de cuentas corriente, favor de re intentar.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Error
                        });

                        /// Se bloquean los controles como en el Legacy
                        InitObject.PrtEnt08.Lista1.Enabled = false;
                        InitObject.PrtEnt08.Lista2.Enabled = false;
                        InitObject.PrtEnt08.aceptar.Enabled = false;
                        return;
                    }

                    if (cuentasPorRut.Count == 0)
                    {
                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "No hay cuentas para el RUT " + rut +". " + msgreto,
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }

                    InitObject.MODWS.CtaCCOL = new Cuentas[cuentasPorRut.Count];
                    for (int i = 0; i < cuentasPorRut.Count; i++)
                    {
                        InitObject.MODWS.CtaCCOL[i] = new Cuentas();
                        InitObject.MODWS.CtaCCOL[i].nrocta = cuentasPorRut[i].numProducto;
                        InitObject.MODWS.CtaCCOL[i].tipo = cuentasPorRut[i].codigoProducto;
                    }
                }
            }
        }

        public static void lista1_ModalSi(InitializationObject InitObject)
        {
            //int cod_ofi = 0;
            string est = string.Empty;
            string linea = string.Empty;
            string lista = string.Empty;
            string a = string.Empty;
            string s = string.Empty;
            string msj = string.Empty;
            string espec = string.Empty;
            string moneda = string.Empty;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "MN";
            InitObject.PrtEnt08.Lista2.ListIndex = -1;
            T_PRTGLOB.primeralista = true.ToInt();
            InitObject.PrtEnt10.listmon.Visible = false;
            InitObject.PrtEnt10.Combo1.Visible = true;
            InitObject.PrtEnt10.Combo1.Enabled = true;
            InitObject.PrtEnt10.Combo1.Items.Clear();
            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);
            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta";

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                //fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                fila = (short)InitObject.PrtEnt08.Lista1.ListIndex;
                lista = InitObject.PrtEnt08.Lista1.Items[fila].Value;//UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista1.Items[fila].Value);
                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
            }

            s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);
            est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
            espec = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
            int largo = 50;
            int largo2 = 40;

            switch (InitObject.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    switch (Convert.ToInt32(s))
                    {
                        case T_PRTGLOB.eliminado_nuevo:
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                s = "3";
                            else
                                s = T_PRTGLOB.nuevo.ToString();
                            break;

                        case T_PRTGLOB.eliminado_leido:
                            s = T_PRTGLOB.leido.ToString();
                            break;

                        case T_PRTGLOB.eliminado_modificado:
                            s = T_PRTGLOB.modificado.ToString();
                            break;
                    }

                    largo = 50;
                    largo2 = 40;
                    linea = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(a, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2), 10), largo2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                    //linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) ;
                    InitObject.PrtEnt08.Lista1.Items[fila].Value = linea;
                    InitObject.PrtEnt08.Lista1.Items[fila].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                    break;

                case T_PRTGLOB.tipo_banco:
                    switch (Convert.ToInt32(s))
                    {
                        case T_PRTGLOB.eliminado_nuevo:
                            s = T_PRTGLOB.nuevo.ToString();
                            break;

                        case T_PRTGLOB.eliminado_leido:
                            s = T_PRTGLOB.leido.ToString();
                            break;

                        case T_PRTGLOB.eliminado_modificado:
                            s = T_PRTGLOB.modificado.ToString();
                            break;
                    }

                    largo = 50;
                    largo2 = 40;
                    //linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5) + VB6Helpers.Chr(9) + espec;
                    //linea = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(a, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2), 10), largo2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                    linea = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(a, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2), 10), largo2) + VB6Helpers.Chr(9) + est;
                    //linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value = linea;
                    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                    break;

                case T_PRTGLOB.individuo:
                    switch (Convert.ToInt32(s))
                    {
                        case T_PRTGLOB.eliminado_nuevo:
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                s = "3";
                            else
                                s = T_PRTGLOB.nuevo.ToString();
                            break;

                        case T_PRTGLOB.eliminado_leido:
                            s = T_PRTGLOB.leido.ToString();
                            break;

                        case T_PRTGLOB.eliminado_modificado:
                            s = T_PRTGLOB.modificado.ToString();
                            break;
                    }

                    largo = 50;
                    largo2 = 40;
                    linea = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(a, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2), 10), largo2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                    //linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) ;
                    InitObject.PrtEnt08.Lista1.Items[fila].Value = linea;
                    InitObject.PrtEnt08.Lista1.Items[fila].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                    break;
            }
        }

        public static void lista2_ModalSi(InitializationObject InitObject)
        {
            int conta = 0;
            string moneda = string.Empty;
            int cod_ofi = 0;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string est = string.Empty;
            string s = string.Empty;
            string ind = string.Empty;
            string linea = string.Empty;
            string oficina = string.Empty;
            int j = 0;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "ME";
            InitObject.PrtEnt08.Lista1.ListIndex = -1;
            T_PRTGLOB.primeralista = Convert.ToInt32(false);
            InitObject.PrtEnt10.especial.Visible = false;
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);
            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

            switch (InitObject.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    switch (Convert.ToInt32(s))
                    {
                        case T_PRTGLOB.eliminado_nuevo:
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                s = "3";
                            else
                                s = T_PRTGLOB.nuevo.ToString();

                            break;

                        case T_PRTGLOB.eliminado_leido:
                            s = T_PRTGLOB.leido.ToString();
                            break;

                        case T_PRTGLOB.eliminado_modificado:
                            s = T_PRTGLOB.modificado.ToString();
                            break;
                    }

                    int largo = 33;
                    int largo2 = 35;
                    int largo3 = 37;
                    ind = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                    oficina = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);
                    //linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind;
                    //linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est; //Esconder columnas el 25-02-2016
                    linea = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(a, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(oficina, largo2) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3), largo3) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                    InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value = linea;
                    InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind;
                    break;

                case T_PRTGLOB.tipo_banco:
                    switch (Convert.ToInt32(s))
                    {
                        case T_PRTGLOB.eliminado_nuevo:
                            s = T_PRTGLOB.nuevo.ToString();
                            break;

                        case T_PRTGLOB.eliminado_leido:
                            s = T_PRTGLOB.leido.ToString();
                            break;

                        case T_PRTGLOB.eliminado_modificado:
                            s = T_PRTGLOB.modificado.ToString();
                            break;
                    }

                    //PrtEnt10.DefInstance.prtactiva[0].Visible = false;
                    ind = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                    largo = 33;
                    largo2 = 35;
                    largo3 = 37;
                    //linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind;
                    linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est; //Esconder columnas el 25-02-2016
                    linea = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(a, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3), largo3) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                    InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value = linea;
                    InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind;
                    break;
            }
        }

        public static void lista1_dblclick(InitializationObject InitObject)
        {
            int conta = 0;
            int j = 0;
            //int cod_ofi = 0;
            string est = string.Empty;
            string linea = string.Empty;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string s = string.Empty;
            string msj = string.Empty;
            string espec = string.Empty;
            string moneda = string.Empty;
            short fila = -1;
            int resp = 0;
            InitObject.PrtEnt10.Tag.Tag = "MN";
            InitObject.PrtEnt08.Lista2.ListIndex = -1;
            T_PRTGLOB.primeralista = true.ToInt();
            InitObject.PrtEnt10.listmon.Visible = false;
            InitObject.PrtEnt10.Combo1.Visible = true;
            InitObject.PrtEnt10.Combo1.Enabled = true;
            InitObject.PrtEnt10.Combo1.Items.Clear();
            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);
            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta";

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                //fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                fila = (short)InitObject.PrtEnt08.Lista1.ListIndex;
                lista = InitObject.PrtEnt08.Lista1.Items[fila].Value;//UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista1.Items[fila].Value);
                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
            }

            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);
            InitObject.PrtEnt10.idEstadoMensaje = 0;

            if (string.IsNullOrEmpty(a.Trim()))
            {
                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                        {
                            InitObject.PrtEnt10.especial.Visible = false;
                            InitObject.PrtEnt10._prtactiva_0.Visible = true;
                            InitObject.PrtEnt10.oficina.Visible = true;
                            InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Nacional ";
                            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                            
                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim().ToString() == T_PRTGLOB.moneda_nac)
                                {
                                    InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                    break;
                                }
                            }

                            InitObject.PrtEnt10.Combo1.Enabled = false;
                            InitObject.PrtEnt10.prtcuenta.Mask = "############";
                            //InitObject.PrtEnt10.prtcuenta.Text = "____________";
                            InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                            InitObject.PrtEnt10.oficina.Text = string.Empty;
                            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                            InitObject.PrtEnt10.aceptar.Enabled = false;
                            InitObject.PrtEnt10.CuentaBae.Enabled = false;
                        }
                        else
                        {
                            InitObject.PrtEnt10.especial.Visible = false;
                            InitObject.PrtEnt10._prtactiva_0.Visible = true;
                            InitObject.PrtEnt10.oficina.Visible = true;
                            //InitObject.PrtEnt10.Label3.Visible = true;
                            InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Nacional ";
                            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;

                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (!string.IsNullOrEmpty(T_PRTGLOB.moneda_nac.Trim().ToString()))
                                {
                                    if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString().Trim() == T_PRTGLOB.moneda_nac.Trim())
                                    {
                                        //InitObject.PrtEnt10.Combo1.ListIndex = i;
                                        InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                        break;
                                    }
                                }
                            }

                            InitObject.PrtEnt10.Combo1.Enabled = false;
                            InitObject.PrtEnt10.prtcuenta.Mask = "000\\-#####\\-##";//"###\\-#####\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                            InitObject.PrtEnt10.oficina.Text = string.Empty;
                            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                            InitObject.PrtEnt10._prtactiva_0.Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                            InitObject.PrtEnt10.aceptar.Enabled = false;
                        }

                        break;

                    case T_PRTGLOB.tipo_banco:

                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        InitObject.PrtEnt10.especial.Visible = true;
                        InitObject.PrtEnt10.oficina.Visible = false;
                        InitObject.PrtEnt10.Label3.Enabled = false;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Banco Corresponsal";
                        //InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.MaxLength = 25;
                        InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        InitObject.PrtEnt10.Combo1.Enabled = true;
                        InitObject.PrtEnt10.Combo1.ListIndex = -1;
                        InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                        break;

                    case T_PRTGLOB.individuo:
                        InitObject.PrtEnt10.especial.Visible = false;
                        InitObject.PrtEnt10._prtactiva_0.Visible = true;
                        InitObject.PrtEnt10.oficina.Visible = true;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Nacional ";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                        InitObject.PrtEnt10.Combo1.SelectedValue = -1;

                        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim().ToString() == T_PRTGLOB.moneda_nac)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                break;
                            }
                        }

                        InitObject.PrtEnt10.Combo1.Enabled = false;
                        InitObject.PrtEnt10.prtcuenta.Mask = "############";
                        //InitObject.PrtEnt10.prtcuenta.Text = "____________";
                        InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        InitObject.PrtEnt10.oficina.Text = string.Empty;
                        InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                        InitObject.PrtEnt10._prtactiva_0.Checked = true;
                        InitObject.PrtEnt10.aceptar.Enabled = false;
                        InitObject.PrtEnt10.CuentaBae.Enabled = false;
                        break;
                }

                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                InitObject.PrtEnt10._prtactiva_1.Checked = true;// true;
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.Eliminar.Enabled = false;
                InitObject.PrtEnt10.especial.Checked = false;// false;
            }
            else
            {
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.Eliminar.Enabled = true;

                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        InitObject.PrtEnt10.especial.Visible = false;
                        InitObject.PrtEnt10._prtactiva_0.Visible = true;

                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "00\\-#####\\-##";//"##\\-######\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "__-______-__";
                            }

                            //InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt08.cuenta.Text = VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask);
                            InitObject.PrtEnt10.CuentaBae.Checked = true;
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                            }
                            //InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt08.cuenta.Text = string.Empty;

                            if (!string.IsNullOrEmpty(VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask)))
                                InitObject.PrtEnt08.cuenta.Text = VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask).Trim();

                            InitObject.PrtEnt10.CuentaBae.Checked = false;
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();
                        }

                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        if (s.ToVal() == T_PRTGLOB.eliminado_nuevo || s.ToVal() == T_PRTGLOB.eliminado_modificado || s.ToVal() == T_PRTGLOB.eliminado_leido)
                        {
                            InitObject.PrtEnt10.idEstadoMensaje = 1;
                            return;
                            //msj = "Cuenta Corriente en proceso de Borrado." + VB6Helpers.Chr(10) + "Si desea reactivar la Cuenta Corriente" + VB6Helpers.Chr(10) + "debería elegir SI y podrá acceder a toda su información." + VB6Helpers.Chr(10) + "Si elige NO volverá a la lista de Cuentas Corrientes.";
                            //resp = MigrationSupport.Utils.MsgBox(msj, MigrationSupport.MsgBoxStyle.YesNo, T_PRTGLOB.TitCuentas);


                            //if (resp == System.Windows.Forms.DialogResult.No)
                            //{
                            //    return;
                            //}
                            //else
                            //{
                            //    switch (Convert.ToInt32(s))
                            //    {
                            //        case T_PRTGLOB.eliminado_nuevo:
                            //            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            //                s = "3";
                            //            else
                            //                s = T_PRTGLOB.nuevo.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_leido:
                            //            s = T_PRTGLOB.leido.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_modificado:
                            //            s = T_PRTGLOB.modificado.ToString();
                            //            break;
                            //    }

                            //    linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) ;
                            //    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value = linea;
                            //    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                            //}
                        }

                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3).Trim();

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                // enviar mensaje de por qué se desactivó
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                // Case "Eliminada"
                                break;

                            case "Cerrada":
                                break;
                        }

                        InitObject.PrtEnt10.oficina.Visible = true;
                        InitObject.PrtEnt10.Label3.Visible = true;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Nacional ";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";

                        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim().ToString() == T_PRTGLOB.moneda_nac)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                break;
                            }
                        }

                        InitObject.PrtEnt10.Combo1.Enabled = false;

                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            InitObject.PrtEnt10.prtcuenta.Mask = "00\\-######\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt10.prtcuenta.Mask = "############";
                                //InitObject.PrtEnt10.prtcuenta.Text = "____________";
                                //InitObject.PrtEnt10.prtcuenta.Text =  InitObject.PrtEnt08.cuenta.Text;
                                InitObject.PrtEnt10.prtcuenta.Text = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt10.prtcuenta.Mask).Trim();
                            }
                            else
                            {
                                InitObject.PrtEnt10.prtcuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                                //InitObject.PrtEnt10.prtcuenta.Text = "___-_____-__";
                                //InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                                InitObject.PrtEnt10.prtcuenta.Text = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt10.prtcuenta.Mask).Trim();
                            }
                        }

                        if (InitObject.PrtEnt10.CuentaBae.Checked == false)
                        {
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();

                            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                        }
                        else
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();

                        break;

                    case T_PRTGLOB.tipo_banco:
                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        InitObject.PrtEnt10.especial.Visible = true;
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        espec = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        if (Convert.ToInt32(s) == T_PRTGLOB.eliminado_nuevo || Convert.ToInt32(s) == T_PRTGLOB.eliminado_modificado || Convert.ToInt32(s) == T_PRTGLOB.eliminado_leido)
                        {

                            InitObject.PrtEnt10.idEstadoMensaje = 2;
                            return;
                            //msj = "Cuenta Corriente en proceso de Borrado." + VB6Helpers.Chr(10) + "Si desea reactivar la Cuenta Corriente" + VB6Helpers.Chr(10) + "debería elegir SI y podrá acceder a toda su información." + VB6Helpers.Chr(10) + "Si elige NO volverá a la lista de Cuentas Corrientes.";
                            //resp = MigrationSupport.Utils.MsgBox(msj, MigrationSupport.MsgBoxStyle.YesNo, T_PRTGLOB.TitCtaLin);

                            //if (resp == System.Windows.Forms.DialogResult.No)
                            //{
                            //    return;
                            //}
                            //else
                            //{
                            //    switch (Convert.ToInt32(s))
                            //    {
                            //        case T_PRTGLOB.eliminado_nuevo:
                            //            s = T_PRTGLOB.nuevo.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_leido:
                            //            s = T_PRTGLOB.leido.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_modificado:
                            //            s = T_PRTGLOB.modificado.ToString();
                            //            break;
                            //    }
                            //    //linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5) + VB6Helpers.Chr(9) + espec;
                            //    linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                            //    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value = linea;
                            //    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5) + VB6Helpers.Chr(9) + espec; //add columnas escondida el 25-02-2016
                            //}
                        }

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                // enviar mensaje de por qué se desactivó
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                break;

                            case "Cerrada":
                                // enviar mensaje de por qué se cerró
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = false;
                                InitObject.PrtEnt10.Eliminar.Enabled = false;
                                break;
                        }

                        InitObject.PrtEnt10.oficina.Visible = false;
                        InitObject.PrtEnt10.Label3.Visible = false;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Banco Corresponsal";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        //InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.Text = a;
                        moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);

                        for (j = 0; j <= InitObject.PrtEnt10.Combo1.Items.Count - 1; j += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[j].Value.ToString() == moneda)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(j);
                                break;
                            }
                        }

                        InitObject.PrtEnt10.especial.Checked = espec == "0";
                        break;

                    case T_PRTGLOB.individuo:
                        InitObject.PrtEnt10.especial.Visible = false;
                        InitObject.PrtEnt10._prtactiva_0.Visible = true;

                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "00\\-#####\\-##";//"##\\-######\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "__-______-__";
                            }

                            //InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt08.cuenta.Text = VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask);
                            InitObject.PrtEnt10.CuentaBae.Checked = true;
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                            }

                            //InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt08.cuenta.Text = string.Empty;
                            
                            if (!string.IsNullOrEmpty(VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask)))
                                InitObject.PrtEnt08.cuenta.Text = VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask).Trim();

                            InitObject.PrtEnt10.CuentaBae.Checked = false;
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();
                        }

                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        if (s.ToVal() == T_PRTGLOB.eliminado_nuevo || s.ToVal() == T_PRTGLOB.eliminado_modificado || s.ToVal() == T_PRTGLOB.eliminado_leido)
                        {
                            InitObject.PrtEnt10.idEstadoMensaje = 1;
                            return;
                            //msj = "Cuenta Corriente en proceso de Borrado." + VB6Helpers.Chr(10) + "Si desea reactivar la Cuenta Corriente" + VB6Helpers.Chr(10) + "debería elegir SI y podrá acceder a toda su información." + VB6Helpers.Chr(10) + "Si elige NO volverá a la lista de Cuentas Corrientes.";
                            //resp = MigrationSupport.Utils.MsgBox(msj, MigrationSupport.MsgBoxStyle.YesNo, T_PRTGLOB.TitCuentas);


                            //if (resp == System.Windows.Forms.DialogResult.No)
                            //{
                            //    return;
                            //}
                            //else
                            //{
                            //    switch (Convert.ToInt32(s))
                            //    {
                            //        case T_PRTGLOB.eliminado_nuevo:
                            //            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            //                s = "3";
                            //            else
                            //                s = T_PRTGLOB.nuevo.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_leido:
                            //            s = T_PRTGLOB.leido.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_modificado:
                            //            s = T_PRTGLOB.modificado.ToString();
                            //            break;
                            //    }

                            //    linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) ;
                            //    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value = linea;
                            //    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                            //}
                        }

                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3).Trim();
                        
                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                // enviar mensaje de por qué se desactivó
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                // Case "Eliminada"
                                break;

                            case "Cerrada":
                                break;
                        }

                        InitObject.PrtEnt10.oficina.Visible = true;
                        InitObject.PrtEnt10.Label3.Visible = true;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Nacional ";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";

                        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim().ToString() == T_PRTGLOB.moneda_nac)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                break;
                            }
                        }

                        InitObject.PrtEnt10.Combo1.Enabled = false;
                        
                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            InitObject.PrtEnt10.prtcuenta.Mask = "00\\-######\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt10.prtcuenta.Mask = "############";
                                //InitObject.PrtEnt10.prtcuenta.Text = "____________";
                                //InitObject.PrtEnt10.prtcuenta.Text =  InitObject.PrtEnt08.cuenta.Text;
                                InitObject.PrtEnt10.prtcuenta.Text = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt10.prtcuenta.Mask).Trim();
                            }
                            else
                            {
                                InitObject.PrtEnt10.prtcuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                                //InitObject.PrtEnt10.prtcuenta.Text = "___-_____-__";
                                //InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                                InitObject.PrtEnt10.prtcuenta.Text = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt10.prtcuenta.Mask).Trim();
                            }
                        }

                        if (InitObject.PrtEnt10.CuentaBae.Checked == false)
                        {
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();

                            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                        }
                        else
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();

                        break;
                }
            }

            if (InitObject.PrtEnt10.idEstadoMensaje == 0)
            {
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.cbo_cta.ListIndex = -1;

                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco) && !string.IsNullOrEmpty(InitObject.PrtEnt10.prtcuenta.Text.Trim().ToString()))
                {
                    for (conta = 0; conta <= InitObject.PrtEnt10.cbo_cta.Items.Count - 1; conta += 1)
                    {
                        if (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() == InitObject.PrtEnt10.prtcuenta.Text)
                        {
                            InitObject.PrtEnt10.cbo_cta.ListIndex = conta;
                            // InitObject.PrtEnt10.cbo_cta.SelectedValue = InitObject.PrtEnt10.cbo_cta.get_ItemData_(conta);
                            break;
                        }
                    }
                }
            }
        }

        public static void lista1_CuentaCorriente_tipocliente_Si_dblclick(InitializationObject InitObject)
        {
            int conta = 0;
            int j = 0;
            string est = string.Empty;
            string linea = string.Empty;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string s = string.Empty;
            string msj = string.Empty;
            string espec = string.Empty;
            string moneda = string.Empty;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "MN";
            InitObject.PrtEnt08.Lista2.ListIndex = -1;
            T_PRTGLOB.primeralista = true.ToInt();
            InitObject.PrtEnt10.listmon.Visible = false;
            InitObject.PrtEnt10.Combo1.Visible = true;
            InitObject.PrtEnt10.Combo1.Enabled = true;
            InitObject.PrtEnt10.Combo1.Items.Clear();
            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);
            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta";

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                lista = InitObject.PrtEnt08.Lista1.Items[fila].Value;//UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista1.Items[fila].Value);

                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
            }

            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

            if (!string.IsNullOrEmpty(a.Trim()))
            {
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.Eliminar.Enabled = true;

                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        InitObject.PrtEnt10.especial.Visible = false;
                        InitObject.PrtEnt10._prtactiva_0.Visible = true;

                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "00\\-#####\\-##";//"##\\-######\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "__-______-__";
                            }

                            //InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt08.cuenta.Text = VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask);
                            InitObject.PrtEnt10.CuentaBae.Checked = true;
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                            }

                            //InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt08.cuenta.Text = string.Empty;
                            if (!string.IsNullOrEmpty(VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask)))
                                InitObject.PrtEnt08.cuenta.Text = VB6Helpers.Format(a, InitObject.PrtEnt08.cuenta.Mask).Trim();

                            InitObject.PrtEnt10.CuentaBae.Checked = false;
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();
                        }

                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        //if (s.ToVal() == T_PRTGLOB.eliminado_nuevo || s.ToVal() == T_PRTGLOB.eliminado_modificado || s.ToVal() == T_PRTGLOB.eliminado_leido)
                        //{

                        //msj = "Cuenta Corriente en proceso de Borrado." + VB6Helpers.Chr(10) + "Si desea reactivar la Cuenta Corriente" + VB6Helpers.Chr(10) + "debería elegir SI y podrá acceder a toda su información." + VB6Helpers.Chr(10) + "Si elige NO volverá a la lista de Cuentas Corrientes.";
                        //resp = MigrationSupport.Utils.MsgBox(msj, MigrationSupport.MsgBoxStyle.YesNo, T_PRTGLOB.TitCuentas);

                        //if (resp == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return;
                        //}
                        //else
                        //{

                        switch (Convert.ToInt32(s))
                        {
                            case T_PRTGLOB.eliminado_nuevo:
                                if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                    s = "3";
                                else
                                    s = T_PRTGLOB.nuevo.ToString();

                                break;

                            case T_PRTGLOB.eliminado_leido:
                                s = T_PRTGLOB.leido.ToString();
                                break;

                            case T_PRTGLOB.eliminado_modificado:
                                s = T_PRTGLOB.modificado.ToString();
                                break;
                        }

                        linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        //InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value = linea;
                        //InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                        InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value = linea;
                        InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                        //}
                        //}
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3).Trim();
                        
                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                // enviar mensaje de por qué se desactivó
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                break;

                            case "Cerrada":
                                break;
                        }

                        InitObject.PrtEnt10.oficina.Visible = true;
                        InitObject.PrtEnt10.Label3.Visible = true;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Nacional ";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";

                        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim().ToString() == T_PRTGLOB.moneda_nac)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                break;
                            }
                        }

                        InitObject.PrtEnt10.Combo1.Enabled = false;
                        
                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            InitObject.PrtEnt10.prtcuenta.Mask = "00\\-######\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt10.prtcuenta.Mask = "############";
                                //InitObject.PrtEnt10.prtcuenta.Text = "____________";
                                //InitObject.PrtEnt10.prtcuenta.Text =  InitObject.PrtEnt08.cuenta.Text;
                                InitObject.PrtEnt10.prtcuenta.Text = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt10.prtcuenta.Mask).Trim();
                            }
                            else
                            {
                                InitObject.PrtEnt10.prtcuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                                //InitObject.PrtEnt10.prtcuenta.Text = "___-_____-__";
                                //InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                                InitObject.PrtEnt10.prtcuenta.Text = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt10.prtcuenta.Mask).Trim();
                            }
                        }


                        if (InitObject.PrtEnt10.CuentaBae.Checked == false)
                        {
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();

                            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                        }
                        else
                            InitObject.PrtEnt10.oficina.Text = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2).Trim();

                        break;
                }
            }

            InitObject.PrtEnt10.aceptar.Enabled = false;
            InitObject.PrtEnt10.cbo_cta.ListIndex = -1;

            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco) && !string.IsNullOrEmpty(InitObject.PrtEnt10.prtcuenta.Text.Trim().ToString()))
            {
                for (conta = 0; conta <= InitObject.PrtEnt10.cbo_cta.Items.Count - 1; conta += 1)
                {
                    if (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() == InitObject.PrtEnt10.prtcuenta.Text)
                    {
                        InitObject.PrtEnt10.cbo_cta.ListIndex = conta;
                        // InitObject.PrtEnt10.cbo_cta.SelectedValue = InitObject.PrtEnt10.cbo_cta.get_ItemData_(conta);
                        break;
                    }
                }
            }
        }

        public static void lista1_CuentaCorriente_tipo_Banco_Si_dblclick(InitializationObject InitObject)
        {
            int conta = 0;
            int j = 0;
            string est = string.Empty;
            string linea = string.Empty;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string s = string.Empty;
            string msj = string.Empty;
            string espec = string.Empty;
            string moneda = string.Empty;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "MN";
            InitObject.PrtEnt08.Lista2.ListIndex = -1;
            T_PRTGLOB.primeralista = true.ToInt();
            InitObject.PrtEnt10.listmon.Visible = false;
            InitObject.PrtEnt10.Combo1.Visible = true;
            InitObject.PrtEnt10.Combo1.Enabled = true;
            InitObject.PrtEnt10.Combo1.Items.Clear();
            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);
            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta";

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                lista = InitObject.PrtEnt08.Lista1.Items[fila].Value;//UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista1.Items[fila].Value);
                
                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
            }

            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);
            InitObject.PrtEnt10.idEstadoMensaje = 0;

            if (!string.IsNullOrEmpty(a.Trim()))
            {
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.Eliminar.Enabled = true;
                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_banco:
                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        InitObject.PrtEnt10.especial.Visible = true;
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        espec = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        //if (Convert.ToInt32(s) == T_PRTGLOB.eliminado_nuevo || Convert.ToInt32(s) == T_PRTGLOB.eliminado_modificado || Convert.ToInt32(s) == T_PRTGLOB.eliminado_leido)
                        //{


                        //msj = "Cuenta Corriente en proceso de Borrado." + VB6Helpers.Chr(10) + "Si desea reactivar la Cuenta Corriente" + VB6Helpers.Chr(10) + "debería elegir SI y podrá acceder a toda su información." + VB6Helpers.Chr(10) + "Si elige NO volverá a la lista de Cuentas Corrientes.";
                        //resp = MigrationSupport.Utils.MsgBox(msj, MigrationSupport.MsgBoxStyle.YesNo, T_PRTGLOB.TitCtaLin);

                        //if (resp == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return;
                        //}
                        //else
                        //{

                        switch (Convert.ToInt32(s))
                        {
                            case T_PRTGLOB.eliminado_nuevo:
                                s = T_PRTGLOB.nuevo.ToString();
                                break;

                            case T_PRTGLOB.eliminado_leido:
                                s = T_PRTGLOB.leido.ToString();
                                break;

                            case T_PRTGLOB.eliminado_modificado:
                                s = T_PRTGLOB.modificado.ToString();
                                break;
                        }

                        //linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3) + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5) + VB6Helpers.Chr(9) + espec;
                        linea = a + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value = linea;
                        InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5) + VB6Helpers.Chr(9) + espec; //add columnas escondida el 25-02-2016
                        //}
                        //}

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                // enviar mensaje de por qué se desactivó
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                break;

                            case "Cerrada":
                                // enviar mensaje de por qué se cerró
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = false;
                                InitObject.PrtEnt10.Eliminar.Enabled = false;
                                break;
                        }

                        InitObject.PrtEnt10.oficina.Visible = false;
                        InitObject.PrtEnt10.Label3.Visible = false;
                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Banco Corresponsal";
                        InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        //InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.Text = a;
                        moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);

                        for (j = 0; j <= InitObject.PrtEnt10.Combo1.Items.Count - 1; j += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[j].Value.ToString() == moneda)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(j);
                                break;
                            }
                        }

                        if (espec == "0")
                            InitObject.PrtEnt10.especial.Checked = false;
                        else
                            InitObject.PrtEnt10.especial.Checked = true;

                        break;
                }
            }

            InitObject.PrtEnt10.aceptar.Enabled = false;
            InitObject.PrtEnt10.cbo_cta.ListIndex = -1;
            
            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco) && !string.IsNullOrEmpty(InitObject.PrtEnt10.prtcuenta.Text.Trim().ToString()))
            {
                for (conta = 0; conta <= InitObject.PrtEnt10.cbo_cta.Items.Count - 1; conta += 1)
                {
                    if (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() == InitObject.PrtEnt10.prtcuenta.Text)
                    {
                        InitObject.PrtEnt10.cbo_cta.ListIndex = conta;
                        // InitObject.PrtEnt10.cbo_cta.SelectedValue = InitObject.PrtEnt10.cbo_cta.get_ItemData_(conta);
                        break;
                    }
                }
            }
        }

        public static void lista2_dblclick(InitializationObject InitObject)
        {
            int conta = 0;
            string moneda = string.Empty;
            int cod_ofi = 0;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string est = string.Empty;
            string s = string.Empty;
            string ind = string.Empty;
            string linea = string.Empty;
            string oficina = string.Empty;
            int j = 0;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "ME";
            InitObject.PrtEnt08.Lista1.ListIndex = -1;
            T_PRTGLOB.primeralista = Convert.ToInt32(false);
            InitObject.PrtEnt10.especial.Visible = false;
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);

            //MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta, InitObject.PrtEnt08.Lista2); //Se cambio logica

            if (InitObject.PrtEnt08.Lista2.Items.Count > 1)
            {
                //fila = (short)InitObject.PrtEnt08.Lista2.get_ItemData_((short)InitObject.PrtEnt08.Lista2.ListIndex);
                fila = (short)InitObject.PrtEnt08.Lista2.ListIndex;
                lista = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[fila].Value);

                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista2.Items[fila].Tag;
            }

            InitObject.PrtEnt10.idEstadoMensaje = 0; //Mensaje si es 0 no entra a ningun dialogs
            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

            if (string.IsNullOrEmpty(a))
            {
                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrWhiteSpace(InitObject.PRTGLOB.Party.Bnumber))
                        {
                            InitObject.PrtEnt10.listmon.Visible = false;
                            InitObject.PrtEnt10.Combo1.Visible = true;
                            InitObject.PrtEnt10.Combo1.Enabled = true;
                            InitObject.PrtEnt10.Combo1.Items.Clear();
                            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                            InitObject.PrtEnt10.Combo1.ListIndex = -1;
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                            InitObject.PrtEnt10._prtactiva_0.Visible = true;
                            InitObject.PrtEnt10.oficina.Visible = true;
                            //InitObject.PrtEnt10.Label3.Visible = true;
                            InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";
                            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                            InitObject.PrtEnt10.prtcuenta.Mask = "############";
                            InitObject.PrtEnt10.prtcuenta.Text = string.Empty; //"____________";

                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                                {
                                    InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                                    break;
                                }
                            }

                            InitObject.PrtEnt10.oficina.Text = string.Empty;
                            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                            InitObject.PrtEnt10.CuentaBae.Enabled = false;
                        }
                        else
                        {
                            // ---------------------------------------------
                            //  RealSystems - Código Nuevo - Termino
                            // ---------------------------------------------
                            InitObject.PrtEnt10.listmon.Visible = false;
                            InitObject.PrtEnt10.Combo1.Visible = true;
                            InitObject.PrtEnt10.Combo1.Enabled = true;
                            InitObject.PrtEnt10.Combo1.Items.Clear();
                            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                            InitObject.PrtEnt10.Combo1.ListIndex = -1;
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                            InitObject.PrtEnt10._prtactiva_0.Visible = true;
                            InitObject.PrtEnt10.oficina.Visible = true;
                            InitObject.PrtEnt10.Label3.Visible = true;
                            InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";
                            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                            InitObject.PrtEnt10.prtcuenta.Mask = "####\\-#####\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = string.Empty;//"____-_____-__";

                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                                {
                                    InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                                    break;
                                }
                            }

                            InitObject.PrtEnt10.oficina.Text = string.Empty;
                            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                        }

                        break;

                    case T_PRTGLOB.tipo_banco:
                        InitObject.PrtEnt10.Combo1.Visible = false;
                        InitObject.PrtEnt10.listmon.Visible = true;
                        InitObject.PrtEnt10.listmon.Items.Clear();
                        PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.listmon);
                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        InitObject.PrtEnt10.oficina.Visible = false;
                        InitObject.PrtEnt10.txtTitulo.Text = "Líneas de Crédito Banco Acreedor";
                        InitObject.PrtEnt10.Label1.Text = "Código de Línea";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        //InitObject.PrtEnt10.prtcuenta.MaxLength = 11;
                        InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        break;
                }

                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.Eliminar.Enabled = false;
            }
            else
            {
                InitObject.PrtEnt10.aceptar.Enabled = true;
                InitObject.PrtEnt10.Eliminar.Enabled = true;

                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        InitObject.PrtEnt10.listmon.Visible = false;
                        InitObject.PrtEnt10.Combo1.Visible = true;
                        InitObject.PrtEnt10.Combo1.Enabled = true;
                        InitObject.PrtEnt10.Combo1.Items.Clear();
                        PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                        string tmpMonedaSelected = InitObject.PrtEnt08.Lista2.Text.Split("\t")[2].Trim();
                        if (tmpMonedaSelected != string.Empty)
                        {
                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim() == tmpMonedaSelected.Replace(" ", " "))
                                {
                                    InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            InitObject.PrtEnt10.Combo1.ListIndex = -1;
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                        }

                        // InitObject.PrtEnt10.Combo1.ListIndex = -1;

                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "1")
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "####\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                            }

                            InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, Convert.ToString(cod_ofi));
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "####\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                            }

                            InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt10.CuentaBae.Checked = false;
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, Convert.ToString(cod_ofi));
                        }

                        InitObject.PrtEnt10._prtactiva_0.Visible = true;
                        InitObject.PrtEnt10.oficina.Visible = true;
                        InitObject.PrtEnt10.Label3.Visible = true;
                        moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);

                        if (Convert.ToInt32(s) == T_PRTGLOB.eliminado_nuevo || Convert.ToInt32(s) == T_PRTGLOB.eliminado_modificado || Convert.ToInt32(s) == T_PRTGLOB.eliminado_leido)
                        {
                            InitObject.PrtEnt10.idEstadoMensaje = 1;
                            return;

                            //msj = "Cuenta Corriente en proceso de Borrado." + VB6Helpers.Chr(10) + "Si desea reactivar la Cuenta Corriente" + VB6Helpers.Chr(10) + "debería elegir SI y podrá acceder a toda su información." + VB6Helpers.Chr(10) + "Si elige NO volverá a la lista de Cuentas Corrientes.";

                            //InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            //{
                            //    Text = msj,
                            //    Title = T_PRTGLOB.TitCuentas,
                            //    Type = TipoMensaje.YesNo
                            //});
                            //if (resp == System.Windows.Forms.DialogResult.No)
                            //{
                            //    return;
                            //}
                            //else
                            //{
                            //    switch (Convert.ToInt32(s))
                            //    {
                            //        case T_PRTGLOB.eliminado_nuevo:
                            //            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")                                        
                            //                s = "3";                                      
                            //            else 
                            //                s = T_PRTGLOB.nuevo.ToString();

                            //            break;
                            //        case T_PRTGLOB.eliminado_leido:
                            //            s = T_PRTGLOB.leido.ToString();
                            //            break;
                            //        case T_PRTGLOB.eliminado_modificado:
                            //            s = T_PRTGLOB.modificado.ToString();
                            //            break;
                            //    }
                            //    ind = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                            //    oficina = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);
                            //    //linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind;
                            //    linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est; //Esconder columnas el 25-02-2016                                 
                            //    InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value = linea;
                            //    InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind; //Esconder columnas el 25-02-2016 

                            //}
                        }

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                // Case "Eliminada"
                                break;

                            case "Cerrada":
                                // enviar mensaje de por qué se cerró
                                InitObject.PrtEnt10._prtactiva_0.Checked = false;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10.Eliminar.Enabled = false;
                                break;
                        }

                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";

                        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                            {
                                InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                                break;
                            }
                        }

                        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                        {
                            ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "############";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }
                        else
                        {
                            ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "####\\-#####\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }

                        if (InitObject.PrtEnt10.CuentaBae.Checked == System.Windows.Forms.CheckState.Unchecked.ToBool())
                        {
                            if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) != String.Empty)
                            {
                                try
                                {
                                    cod_ofi = Convert.ToInt32(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2));
                                }
                                catch { cod_ofi = 0; }
                            }
                            else
                                cod_ofi = 0;

                            //cod_ofi = PRTYENT.quitaceros(InitObject.PrtEnt08.cuenta.Text);
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, cod_ofi.ToString());
                        }
                        else
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, cod_ofi.ToString());
                        

                        for (j = 0; j <= InitObject.PrtEnt10.Combo1.Items.Count - 1; j += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[j].Value.ToString() == moneda)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(j);
                                break;
                            }
                        }

                        if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                        
                        break;

                    case T_PRTGLOB.tipo_banco:
                        InitObject.PrtEnt10.Combo1.Visible = false;
                        InitObject.PrtEnt10.listmon.Visible = true;
                        InitObject.PrtEnt10.listmon.Items.Clear();
                        PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.listmon);
                        InitObject.PrtEnt10.oficina.Visible = false;
                        //InitObject.PrtEnt10.Label3.Visible = false;
                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        if (Convert.ToInt32(s) == T_PRTGLOB.eliminado_nuevo || Convert.ToInt32(s) == T_PRTGLOB.eliminado_modificado || Convert.ToInt32(s) == T_PRTGLOB.eliminado_leido)
                        {
                            InitObject.PrtEnt10.idEstadoMensaje = 2;
                            return;
                        }

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                break;

                            case "Cerrada":
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                InitObject.PrtEnt10._prtactiva_0.Visible = false;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = false;
                                InitObject.PrtEnt10.Eliminar.Enabled = false;
                                break;
                        }

                        InitObject.PrtEnt10.txtTitulo.Text = "Líneas de Crédito Banco Acreedor";
                        InitObject.PrtEnt10.Label1.Text = "Línea de Crédito";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.Text = a;
                        moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);

                        for (j = 0; j <= InitObject.PrtEnt10.listmon.Items.Count - 1; j += 1)
                        {
                            if (InitObject.PrtEnt10.listmon.Items[j].Value.ToString() == moneda)
                            {
                                //InitObject.PrtEnt10.listmon.ListIndex = j;//, true);
                                InitObject.PrtEnt10.listmon.SelectedValue = InitObject.PrtEnt10.listmon.get_ItemData_(j);
                                break;
                            }
                        }

                        break;
                }
            }

            if (InitObject.PrtEnt10.idEstadoMensaje == 0)
            {
                InitObject.PrtEnt10.cbo_cta.ListIndex = -1;

                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco) && (!string.IsNullOrEmpty(InitObject.PrtEnt10.prtcuenta.Text.Trim()) || InitObject.PrtEnt10.prtcuenta.Text != null))
                {
                    for (conta = 0; conta <= InitObject.PrtEnt10.cbo_cta.Items.Count - 1; conta += 1)
                    {
                        if (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() == InitObject.PrtEnt10.prtcuenta.Text)
                        {
                            conta = (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() != string.Empty ? conta : -1);
                            InitObject.PrtEnt10.cbo_cta.ListIndex = conta;
                            //InitObject.PrtEnt10.cbo_cta.SelectedValue = InitObject.PrtEnt10.cbo_cta.get_ItemData_(conta);
                            break;
                        }
                    }
                }

                InitObject.PrtEnt10.aceptar.Enabled = false;
            }
        }

        public static void Lista2_CuentaCorriente_Si(InitializationObject InitObject)
        {
            int conta = 0;
            string moneda = string.Empty;
            int cod_ofi = 0;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string est = string.Empty;
            string s = string.Empty;
            string ind = string.Empty;
            string linea = string.Empty;
            string oficina = string.Empty;
            int j = 0;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "ME";
            InitObject.PrtEnt08.Lista1.ListIndex = -1;
            T_PRTGLOB.primeralista = Convert.ToInt32(false);
            InitObject.PrtEnt10.especial.Visible = false;
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);

            //  MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta, InitObject.PrtEnt08.Lista2); //Se cambio logica

            if (InitObject.PrtEnt08.Lista2.Items.Count > 1)
            {
                fila = (short)InitObject.PrtEnt08.Lista2.get_ItemData_((short)InitObject.PrtEnt08.Lista2.ListIndex);
                lista = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[fila].Value);

                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista2.Items[fila].Tag;
            }

            InitObject.PrtEnt10.idEstadoMensaje = 0; //Mensaje si es 0 no entra a ningun dialogs
            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

            if (!string.IsNullOrEmpty(a))
            {
                InitObject.PrtEnt10.aceptar.Enabled = true;
                InitObject.PrtEnt10.Eliminar.Enabled = true;

                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        InitObject.PrtEnt10.listmon.Visible = false;
                        InitObject.PrtEnt10.Combo1.Visible = true;
                        InitObject.PrtEnt10.Combo1.Enabled = true;
                        InitObject.PrtEnt10.Combo1.Items.Clear();
                        PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                        string tmpMonedaSelected = InitObject.PrtEnt08.Lista2.Text.Split("\t")[2].Trim();

                        if (tmpMonedaSelected != string.Empty)
                        {
                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim() == tmpMonedaSelected.Replace(" ", " "))
                                {
                                    InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            InitObject.PrtEnt10.Combo1.ListIndex = -1;
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                        }

                        // InitObject.PrtEnt10.Combo1.ListIndex = -1;

                        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "1")
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "####\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                            }

                            InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, Convert.ToString(cod_ofi));
                        }
                        else
                        {
                            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";
                            }
                            else
                            {
                                ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "####\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                            }

                            InitObject.PrtEnt08.cuenta.Text = a;
                            InitObject.PrtEnt10.CuentaBae.Checked = false;
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, Convert.ToString(cod_ofi));
                        }

                        InitObject.PrtEnt10._prtactiva_0.Visible = true;
                        InitObject.PrtEnt10.oficina.Visible = true;
                        InitObject.PrtEnt10.Label3.Visible = true;
                        moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);

                        switch (Convert.ToInt32(s))
                        {
                            case T_PRTGLOB.eliminado_nuevo:
                                if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                                    s = "3";
                                else
                                    s = T_PRTGLOB.nuevo.ToString();

                                break;

                            case T_PRTGLOB.eliminado_leido:
                                s = T_PRTGLOB.leido.ToString();
                                break;

                            case T_PRTGLOB.eliminado_modificado:
                                s = T_PRTGLOB.modificado.ToString();
                                break;
                        }

                        ind = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6);
                        oficina = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);
                        //linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind;
                        linea = a + VB6Helpers.Chr(9) + oficina + VB6Helpers.Chr(9) + moneda + VB6Helpers.Chr(9) + est; //Esconder columnas el 25-02-2016                                 
                        InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value = linea;
                        InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + s + VB6Helpers.Chr(9) + ind; //Esconder 

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_0.Checked = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                // Case "Eliminada"
                                break;

                            case "Cerrada":
                                // enviar mensaje de por qué se cerró
                                InitObject.PrtEnt10._prtactiva_0.Checked = false;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10.Eliminar.Enabled = false;
                                break;
                        }

                        InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";
                        
                        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                            {
                                InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                                break;
                            }
                        }

                        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                        {
                            ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "############";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }
                        else
                        {
                            ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "####\\-#####\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                        }

                        if (InitObject.PrtEnt10.CuentaBae.Checked == System.Windows.Forms.CheckState.Unchecked.ToBool())
                        {
                            if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) != String.Empty)
                            {
                                try
                                {
                                    cod_ofi = Convert.ToInt32(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2));
                                }
                                catch { cod_ofi = 0; }
                            }
                            else
                                cod_ofi = 0;

                            //cod_ofi = PRTYENT.quitaceros(InitObject.PrtEnt08.cuenta.Text);
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, cod_ofi.ToString());
                        }
                        else
                            InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, cod_ofi.ToString());
                        

                        for (j = 0; j <= InitObject.PrtEnt10.Combo1.Items.Count - 1; j += 1)
                        {
                            if (InitObject.PrtEnt10.Combo1.Items[j].Value.ToString() == moneda)
                            {
                                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(j);
                                break;
                            }
                        }

                        if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                        
                        break;
                }
            }

            InitObject.PrtEnt10.cbo_cta.ListIndex = -1;

            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco) && !string.IsNullOrEmpty(InitObject.PrtEnt10.prtcuenta.Text))
            {
                for (conta = 0; conta <= InitObject.PrtEnt10.cbo_cta.Items.Count - 1; conta += 1)
                {
                    if (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() == InitObject.PrtEnt10.prtcuenta.Text)
                    {
                        conta = (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() != string.Empty ? conta : -1);
                        InitObject.PrtEnt10.cbo_cta.ListIndex = conta;
                        //InitObject.PrtEnt10.cbo_cta.SelectedValue = InitObject.PrtEnt10.cbo_cta.get_ItemData_(conta);
                        break;
                    }
                }
            }
            InitObject.PrtEnt10.aceptar.Enabled = false;
        }

        public static void Lista2_LineaCredito_Si(InitializationObject InitObject)
        {
            int conta = 0;
            string moneda = string.Empty;
            int cod_ofi = 0;
            string lista = string.Empty;
            string a = string.Empty;
            int i = 0;
            string est = string.Empty;
            string s = string.Empty;
            string ind = string.Empty;
            string linea = string.Empty;
            string oficina = string.Empty;
            int j = 0;
            short fila = -1;
            InitObject.PrtEnt10.Tag.Tag = "ME";
            InitObject.PrtEnt08.Lista1.ListIndex = -1;
            T_PRTGLOB.primeralista = Convert.ToInt32(false);
            InitObject.PrtEnt10.especial.Visible = false;
            MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta);

            //MODWS.Llena_Cuentas(InitObject, InitObject.PrtEnt10.cbo_cta, InitObject.PrtEnt08.Lista2); //Se cambio logica

            if (InitObject.PrtEnt08.Lista2.Items.Count > 1)
            {
                fila = (short)InitObject.PrtEnt08.Lista2.get_ItemData_((short)InitObject.PrtEnt08.Lista2.ListIndex);
                lista = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[fila].Value);
                if (!string.IsNullOrEmpty(lista))
                    lista += InitObject.PrtEnt08.Lista2.Items[fila].Tag;
            }

            InitObject.PrtEnt10.idEstadoMensaje = 0; //Mensaje si es 0 no entra a ningun dialogs

            a = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

            if (string.IsNullOrEmpty(a))
            {
                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    case T_PRTGLOB.tipo_cliente:
                        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrWhiteSpace(InitObject.PRTGLOB.Party.Bnumber))
                        {
                            InitObject.PrtEnt10.listmon.Visible = false;
                            InitObject.PrtEnt10.Combo1.Visible = true;
                            InitObject.PrtEnt10.Combo1.Enabled = true;
                            InitObject.PrtEnt10.Combo1.Items.Clear();
                            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                            InitObject.PrtEnt10.Combo1.ListIndex = -1;
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                            InitObject.PrtEnt10._prtactiva_0.Visible = true;
                            InitObject.PrtEnt10.oficina.Visible = true;
                            //InitObject.PrtEnt10.Label3.Visible = true;
                            InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";
                            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                            InitObject.PrtEnt10.prtcuenta.Mask = "############";
                            InitObject.PrtEnt10.prtcuenta.Text = string.Empty; //"____________";

                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                                {
                                    InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                                    break;
                                }
                            }

                            InitObject.PrtEnt10.oficina.Text = string.Empty;
                            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                            InitObject.PrtEnt10.CuentaBae.Enabled = false;
                        }
                        else
                        {
                            // ---------------------------------------------
                            //  RealSystems - Código Nuevo - Termino
                            // ---------------------------------------------
                            InitObject.PrtEnt10.listmon.Visible = false;
                            InitObject.PrtEnt10.Combo1.Visible = true;
                            InitObject.PrtEnt10.Combo1.Enabled = true;
                            InitObject.PrtEnt10.Combo1.Items.Clear();
                            PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                            InitObject.PrtEnt10.Combo1.ListIndex = -1;
                            InitObject.PrtEnt10.Combo1.SelectedValue = -1;
                            InitObject.PrtEnt10._prtactiva_0.Visible = true;
                            InitObject.PrtEnt10.oficina.Visible = true;
                            InitObject.PrtEnt10.Label3.Visible = true;
                            InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";
                            InitObject.PrtEnt10.Label1.Text = "Nº Cuenta Corriente";
                            InitObject.PrtEnt10.prtcuenta.Mask = "####\\-#####\\-##";
                            InitObject.PrtEnt10.prtcuenta.Text = "____-_____-__";

                            for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                            {
                                if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                                {
                                    InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                                    break;
                                }
                            }

                            InitObject.PrtEnt10.oficina.Text = string.Empty;
                            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                        }

                        break;

                    case T_PRTGLOB.tipo_banco:
                        InitObject.PrtEnt10.Combo1.Visible = false;
                        InitObject.PrtEnt10.listmon.Visible = true;
                        InitObject.PrtEnt10.listmon.Items.Clear();
                        PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.listmon);
                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        InitObject.PrtEnt10.oficina.Visible = false;
                        InitObject.PrtEnt10.txtTitulo.Text = "Líneas de Crédito Banco Acreedor";
                        InitObject.PrtEnt10.Label1.Text = "Código de Línea";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        //InitObject.PrtEnt10.prtcuenta.MaxLength = 11;
                        InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        break;
                }

                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                InitObject.PrtEnt10.aceptar.Enabled = false;
                InitObject.PrtEnt10.Eliminar.Enabled = false;
            }
            else
            {
                InitObject.PrtEnt10.aceptar.Enabled = true;
                InitObject.PrtEnt10.Eliminar.Enabled = true;

                switch (InitObject.PRTGLOB.Party.tipo)
                {
                    //case T_PRTGLOB.tipo_cliente:
                    //    InitObject.PrtEnt10.listmon.Visible = false;
                    //    InitObject.PrtEnt10.Combo1.Visible = true;
                    //    InitObject.PrtEnt10.Combo1.Enabled = true;
                    //    InitObject.PrtEnt10.Combo1.Items.Clear();
                    //    PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.Combo1);
                    //    string tmpMonedaSelected = InitObject.PrtEnt08.Lista2.Text.Split("\t")[2].Trim();
                    //    if (tmpMonedaSelected != string.Empty)
                    //    {
                    //        for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                    //        {
                    //            if (InitObject.PrtEnt10.Combo1.Items[i].Value.Trim() == tmpMonedaSelected.Replace(" ", " "))
                    //            {
                    //                InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(i);
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        InitObject.PrtEnt10.Combo1.ListIndex = -1;
                    //    }

                    //    // InitObject.PrtEnt10.Combo1.ListIndex = -1;

                    //    if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 6) == "1")
                    //    {
                    //        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                    //        {
                    //            ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "############";
                    //            InitObject.PrtEnt08.cuenta.Text = "____________";
                    //        }
                    //        else
                    //        {
                    //            ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "####\\-#####\\-##";
                    //            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                    //        }
                    //        InitObject.PrtEnt08.cuenta.Text = a;
                    //        InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, Convert.ToString(cod_ofi));
                    //    }
                    //    else
                    //    {
                    //        if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                    //        {
                    //            ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "############";
                    //            InitObject.PrtEnt08.cuenta.Text = "____________";
                    //        }
                    //        else
                    //        {
                    //            ((dynamic)InitObject.PrtEnt08.cuenta).Mask = "####\\-#####\\-##";
                    //            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                    //        }
                    //        InitObject.PrtEnt08.cuenta.Text = a;
                    //        InitObject.PrtEnt10.CuentaBae.Checked = false;
                    //        InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, Convert.ToString(cod_ofi));
                    //    }
                    //    InitObject.PrtEnt10._prtactiva_0.Visible = true;
                    //    InitObject.PrtEnt10.oficina.Visible = true;
                    //    InitObject.PrtEnt10.Label3.Visible = true;
                    //    moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                    //    est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);
                    //    s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 5);
                    //    if (Convert.ToInt32(s) == T_PRTGLOB.eliminado_nuevo || Convert.ToInt32(s) == T_PRTGLOB.eliminado_modificado || Convert.ToInt32(s) == T_PRTGLOB.eliminado_leido)
                    //    {

                    //        InitObject.PrtEnt10.idEstadoMensaje = 1;
                    //        return;                          
                    //    }

                    //    switch (est)
                    //    {
                    //        case "Activa":
                    //            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                    //            InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                    //            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                    //            InitObject.PrtEnt10._prtactiva_1.Checked = true;
                    //            break;
                    //        case "No Activa":
                    //            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                    //            InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                    //            InitObject.PrtEnt10._prtactiva_0.Checked = true;
                    //            InitObject.PrtEnt10._prtactiva_1.Checked = false;
                    //            // Case "Eliminada"
                    //            break;
                    //        case "Cerrada":
                    //            // enviar mensaje de por qué se cerró
                    //            InitObject.PrtEnt10._prtactiva_0.Checked = false;
                    //            InitObject.PrtEnt10._prtactiva_1.Checked = false;
                    //            InitObject.PrtEnt10._prtactiva_0.Enabled = true;
                    //            InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                    //            InitObject.PrtEnt10.Eliminar.Enabled = false;
                    //            break;
                    //    }

                    //    InitObject.PrtEnt10.txtTitulo.Text = "Cuentas Corrientes Moneda Extranjera ";
                    //    for (i = 0; i <= InitObject.PrtEnt10.Combo1.Items.Count - 1; i += 1)
                    //    {
                    //        if (InitObject.PrtEnt10.Combo1.Items[i].Value.ToString() == T_PRTGLOB.moneda_nac)
                    //        {
                    //            InitObject.PrtEnt10.Combo1.Items.RemoveAt(i);
                    //            break;
                    //        }
                    //    }
                    //    if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.Bnumber != "")
                    //    {
                    //        ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "############";
                    //        InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                    //    }
                    //    else
                    //    {
                    //        ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "####\\-#####\\-##";
                    //        InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt08.cuenta.Text;
                    //    }
                    //    if (InitObject.PrtEnt10.CuentaBae.Checked == System.Windows.Forms.CheckState.Unchecked.ToBool())
                    //    {
                    //        if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) != String.Empty)
                    //        {
                    //            try
                    //            {
                    //                cod_ofi = Convert.ToInt32(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2));
                    //            }
                    //            catch { cod_ofi = 0; }
                    //        }
                    //        else
                    //            cod_ofi = 0;
                    //        //cod_ofi = PRTYENT.quitaceros(InitObject.PrtEnt08.cuenta.Text);
                    //        InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, cod_ofi.ToString());
                    //    }
                    //    else
                    //    {
                    //        InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, cod_ofi.ToString());
                    //    }
                    //    for (j = 0; j <= InitObject.PrtEnt10.Combo1.Items.Count - 1; j += 1)
                    //    {
                    //        if (InitObject.PrtEnt10.Combo1.Items[j].Value.ToString() == moneda)
                    //        {
                    //            InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(j);
                    //            break;
                    //        }
                    //    }
                    //    if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                    //    {
                    //        InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                    //    }
                    //    break;

                    case T_PRTGLOB.tipo_banco:
                        InitObject.PrtEnt10.Combo1.Visible = false;
                        InitObject.PrtEnt10.listmon.Visible = true;
                        InitObject.PrtEnt10.listmon.Items.Clear();
                        PRTYENT.llena_moneda(InitObject, InitObject.PrtEnt10.listmon);
                        InitObject.PrtEnt10.oficina.Visible = false;
                        //InitObject.PrtEnt10.Label3.Visible = false;
                        InitObject.PrtEnt10._prtactiva_0.Visible = false;
                        est = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 3);
                        s = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 4);

                        if (Convert.ToInt32(s) == T_PRTGLOB.eliminado_nuevo || Convert.ToInt32(s) == T_PRTGLOB.eliminado_modificado || Convert.ToInt32(s) == T_PRTGLOB.eliminado_leido)
                        {
                            InitObject.PrtEnt10.idEstadoMensaje = 2;
                            return;
                        }

                        switch (est)
                        {
                            case "Activa":
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = true;
                                break;

                            case "No Activa":
                                InitObject.PrtEnt10._prtactiva_1.Enabled = true;
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                break;

                            case "Cerrada":
                                InitObject.PrtEnt10._prtactiva_1.Checked = false;
                                InitObject.PrtEnt10._prtactiva_0.Visible = false;
                                InitObject.PrtEnt10._prtactiva_1.Enabled = false;
                                InitObject.PrtEnt10.Eliminar.Enabled = false;
                                break;
                        }

                        InitObject.PrtEnt10.txtTitulo.Text = "Líneas de Crédito Banco Acreedor";
                        InitObject.PrtEnt10.Label1.Text = "Línea de Crédito";
                        InitObject.PrtEnt10.prtcuenta.Mask = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.Text = string.Empty;
                        InitObject.PrtEnt10.prtcuenta.Text = a;
                        moneda = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2);

                        for (j = 0; j <= InitObject.PrtEnt10.listmon.Items.Count - 1; j += 1)
                        {
                            if (InitObject.PrtEnt10.listmon.Items[j].Value.ToString() == moneda)
                            {
                                //InitObject.PrtEnt10.listmon.ListIndex = j;//, true);
                                InitObject.PrtEnt10.listmon.SelectedValue = InitObject.PrtEnt10.listmon.get_ItemData_(j);
                                break;
                            }
                        }

                        break;
                }
            }

            InitObject.PrtEnt10.cbo_cta.ListIndex = -1;

            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco) && !string.IsNullOrEmpty(InitObject.PrtEnt10.prtcuenta.Text))
            {
                for (conta = 0; conta <= InitObject.PrtEnt10.cbo_cta.Items.Count - 1; conta += 1)
                {
                    if (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() == InitObject.PrtEnt10.prtcuenta.Text)
                    {
                        conta = (InitObject.PrtEnt10.cbo_cta.Items[conta].Value.ToString() != string.Empty ? conta : -1);
                        InitObject.PrtEnt10.cbo_cta.ListIndex = conta;
                        //InitObject.PrtEnt10.cbo_cta.SelectedValue = InitObject.PrtEnt10.cbo_cta.get_ItemData_(conta);
                        break;
                    }
                }
            }

            InitObject.PrtEnt10.aceptar.Enabled = false;

        }

        //public static void Lista2_KeyPress(InitializationObject initObj)
        //{

        //    if (initObj.PrtEnt08.Lista2.Items.Count != 0)
        //    {
        //        //if (KeyAscii == 13)
        //        //{
        //        if (initObj.PrtEnt08.Lista2.Items.Count == 1 && initObj.PrtEnt08.Lista2.ListIndex == -1)
        //        {
        //            initObj.PrtEnt08.Lista2.ListIndex = 0;
        //        }
        //        lista2_dblclick(initObj);
        //        //}
        //        //else
        //        //{
        //        //    KeyAscii = 0;
        //        //    Console.Beep();
        //        //}
        //    }
        //    //else
        //    //{
        //    //    Console.Beep();
        //    //}

        //}
    }
}