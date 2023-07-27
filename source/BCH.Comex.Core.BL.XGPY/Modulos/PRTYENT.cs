using System;
using System.Linq;
using System.Collections.Generic;
using BCH.Comex.Core.BL.XGPY.Forms;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class PRTYENT
    {
        public static int EstadoParty = 0;

        public static T_PRTYENT GetPRTYENT()
        {
            return new T_PRTYENT();
        }
     
        public static void respalda_ctas(InitializationObject InitObject, string Que)
        {
            int i = 0;

            switch (Que.ToLower())
            {
                case "ctaclie":
                    InitObject.PRTGLOB.ctaclie_aux = new cuenta_indice[InitObject.PRTGLOB.ctaclie.GetUpperBound(0) + 1];
                    for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                    {
                        InitObject.PRTGLOB.ctaclie_aux[i] = new cuenta_indice();
                        InitObject.PRTGLOB.ctaclie_aux[i].indice = InitObject.PRTGLOB.ctaclie[i].indice;
                        InitObject.PRTGLOB.ctaclie_aux[i].cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;
                    }
                    break;

                case "ctabancos":
                    InitObject.PRTGLOB.ctaclie_aux = new cuenta_indice[InitObject.PRTGLOB.ctabancos.GetUpperBound(0) + 1];
                    for (i = 0; i <= InitObject.PRTGLOB.ctabancos.GetUpperBound(0); i += 1)
                    {
                        InitObject.PRTGLOB.ctaclie_aux[i] = new cuenta_indice();
                        InitObject.PRTGLOB.ctaclie_aux[i].indice = InitObject.PRTGLOB.ctabancos[i].indice;
                        InitObject.PRTGLOB.ctaclie_aux[i].cuenta = InitObject.PRTGLOB.ctabancos[i].cuenta;
                    }
                    break;

                case "linbancos":
                    InitObject.PRTGLOB.linbancos_aux = new cuenta_indice[InitObject.PRTGLOB.linbancos.GetUpperBound(0) + 1];
                    for (i = 0; i <= InitObject.PRTGLOB.linbancos.GetUpperBound(0); i += 1)
                    {
                        InitObject.PRTGLOB.ctaclie_aux[i] = new cuenta_indice();
                        InitObject.PRTGLOB.linbancos_aux[i].indice = InitObject.PRTGLOB.linbancos[i].indice;
                        InitObject.PRTGLOB.linbancos_aux[i].cuenta = InitObject.PRTGLOB.linbancos[i].linea;
                    }
                    break;
            }

        }

        public static void lee_bctaSy(InitializationObject InitObject, string llave)
        {
            //que es esto?
            return;
            /*
            Lee_bctaSyErr:
            //System.Windows.Forms.MessageBox.Show("Error", "", MessageBoxButtons.OK);
            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
            {
                Text = "Error",
                Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                Type = TipoMensaje.Error
            });*/
        }

        public static void lee_blineaSy(InitializationObject InitObject, string llave)
        {
        }   

        public static int BuscaIndices(InitializationObject InitObject, string Tabla, string ind)
        {
            int BuscaIndices = 0;
            int i = 0;

            switch (Tabla)
            {
                case "ctaclie":

                    if (InitObject.PRTGLOB.ctaclie == null)
                        InitObject.PRTGLOB.ctaclie = new prtyccta[0];

                    for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                    {
                        if (InitObject.PRTGLOB.ctaclie[i].cuenta == "" || InitObject.PRTGLOB.ctaclie[i].cuenta == null)
                        //if ((InitObject.PRTGLOB.ctaclie[i] == null))                       
                        {
                            BuscaIndices = i;
                            break;
                        }
                    }

                    break;

                case "ctabancos":
                    //if (InitObject.PRTGLOB.ctabancos != null)
                    //{
                    if (InitObject.PRTGLOB.ctabancos == null)
                        InitObject.PRTGLOB.ctabancos = new prtybcta[0];

                    for (i = 0; i <= InitObject.PRTGLOB.ctabancos.GetUpperBound(0); i += 1)
                    {
                        if ((InitObject.PRTGLOB.ctabancos[i].cuenta == "") || (InitObject.PRTGLOB.ctabancos[i].cuenta == null))
                        {
                            BuscaIndices = i;
                            break;
                        }
                    }
                    //}

                    break;

                case "linbancos":
                    //if (InitObject.PRTGLOB.linbancos != null)
                    //{
                    if (InitObject.PRTGLOB.linbancos == null)
                        InitObject.PRTGLOB.linbancos = new prtyblinea[0];

                    for (i = 0; i <= InitObject.PRTGLOB.linbancos.GetUpperBound(0); i += 1)
                    {
                        if ((InitObject.PRTGLOB.linbancos[i].linea == "") || (InitObject.PRTGLOB.linbancos[i].linea == null))
                        {
                            BuscaIndices = i;
                            break;
                        }
                    }
                    //}
                    break;
            }
            return BuscaIndices;
        }

        //public static void AceptaLista(InitializationObject InitObject, UI_Combo_ lista, int numero)
        //{
        //    int i = 0;
        //    int j = 0;
        //    string linea = "";
        //    string ind = "";
        //    string v = "";
        //    string est = "";
        //    string a = "";
        //    string oficina = "";
        //    lista.ListIndex = 0;
        //    if (numero == 1) //Cuenta Nacional
        //    {
        //        for (j = 0; j <= lista.Items.Count - 1; j += 1)
        //        {
        //            if (lista.Items.Count > 0)
        //                linea = lista.Items[j].Value;

        //            a = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 1);
        //            oficina = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2);
        //            est = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 3);
        //            v = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4);
        //            ind = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
        //             //i = BuscaIndices(InitObject, "ctaclie", ind);
        //           // i = ind.ToInt();

        //            if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6) == "BAE")
        //            {
        //                InitObject.PrtEnt08.cuenta.Mask = "##\\-######\\-##";
        //                InitObject.PrtEnt08.cuenta.Text = "__-______-__";
        //            }
        //            else
        //            {
        //                if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")
        //                {
        //                    InitObject.PrtEnt08.cuenta.Mask = "############";
        //                    InitObject.PrtEnt08.cuenta.Text = "____________";

        //                }
        //                else
        //                {
        //                    InitObject.PrtEnt08.cuenta.Mask = "###\\-#####\\-##";
        //                    InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
        //                }
        //            }
        //            InitObject.PrtEnt08.cuenta.Text = a;
        //            InitObject.PRTGLOB.ctaclie[i] = new prtyccta();

        //            InitObject.PRTGLOB.ctaclie[i].estado = v.ToInt();
        //            InitObject.PRTGLOB.ctaclie[i].cuenta = InitObject.PrtEnt08.cuenta.Text.ToStr();
        //            InitObject.PRTGLOB.ctaclie[i].extranjera = false.ToInt();

        //            switch (est)
        //            {
        //                case "Activa":
        //                    InitObject.PRTGLOB.ctaclie[i].activace = 1;
        //                    InitObject.PRTGLOB.ctaclie[i].activabco = 1;
        //                    break;
        //                case "No Activa":
        //                    InitObject.PRTGLOB.ctaclie[i].activace = 0;
        //                    InitObject.PRTGLOB.ctaclie[i].activabco = 1;
        //                    break;
        //                case "Cerrada":
        //                    InitObject.PRTGLOB.ctaclie[i].activace = 0;
        //                    InitObject.PRTGLOB.ctaclie[i].activabco = 0;
        //                    break;
        //            }
        //            InitObject.PRTGLOB.ctaclie[i].moneda = lista.ListIndex.ToStr(); //((object)((dynamic)lista).ItemData(((System.Windows.Forms.ListBox)(lista)).SelectedIndex)).ToStr();
        //            InitObject.PRTGLOB.ctaclie[i].indice = ind.ToInt();

        //            if (lista.ListIndex.ToInt() < lista.Items.Count.ToInt() - 2)
        //            {
        //                lista.ListIndex = lista.ListIndex.ToInt() + 1;
        //            }

        //            i = BuscaIndices(InitObject, "ctaclie", ind);
        //        }

        //        if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")
        //        {
        //            InitObject.PrtEnt08.cuenta.Mask = "############";
        //            InitObject.PrtEnt08.cuenta.Text = "____________";
        //        }
        //        else
        //        {
        //            InitObject.PrtEnt08.cuenta.Mask = "###\\-#####\\-##";
        //            InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
        //        }
        //    }
        //    else if (numero == 2)  //Cuenta Extranjera
        //    {
        //        if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")
        //        {
        //            InitObject.PrtEnt08.cuenta.Mask = "############";
        //        }
        //        else
        //        {
        //            InitObject.PrtEnt08.cuenta.Mask = "####\\-#####\\-##";
        //        }

        //        for (j = 0; j <= lista.Items.Count.ToInt() - 1; j += 1)
        //        {
        //            if (lista.Items.Count > 0)
        //                linea = lista.Items[j].Value;

        //            ind = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6);
        //            i = BuscaIndices(InitObject, "ctaclie", ind);
        //            v = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
        //            est = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4);
        //            a = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 1);
        //            oficina = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2);

        //            if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")
        //            {
        //                InitObject.PrtEnt08.cuenta.Text = "____________";
        //            }
        //            else
        //            {
        //                InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
        //            }

        //            InitObject.PrtEnt08.cuenta.Text = a;
        //            InitObject.PRTGLOB.ctaclie[i] = new prtyccta();
        //            InitObject.PRTGLOB.ctaclie[i].estado = v.ToInt();
        //            InitObject.PRTGLOB.ctaclie[i].cuenta = InitObject.PrtEnt08.cuenta.Text;
        //            InitObject.PRTGLOB.ctaclie[i].extranjera = true.ToInt();
        //            switch (est)
        //            {
        //                case "Activa":
        //                    InitObject.PRTGLOB.ctaclie[i].activace = 1;
        //                    InitObject.PRTGLOB.ctaclie[i].activabco = 1;
        //                    break;
        //                case "No Activa":
        //                    InitObject.PRTGLOB.ctaclie[i].activace = 0;
        //                    InitObject.PRTGLOB.ctaclie[i].activabco = 1;
        //                    break;
        //                case "Cerrada":
        //                    InitObject.PRTGLOB.ctaclie[i].activace = 0;
        //                    InitObject.PRTGLOB.ctaclie[i].activabco = 0;
        //                    break;
        //            }
        //            InitObject.PRTGLOB.ctaclie[i].moneda = lista.ListIndex.ToStr();
        //            InitObject.PRTGLOB.ctaclie[i].indice = ind.ToInt();

        //            if (lista.ListIndex.ToInt() < lista.Items.Count.ToInt() - 2)
        //            {
        //                lista.ListIndex = lista.ListIndex.ToInt() + 1;
        //            }
        //        }

        //        if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool())
        //        {
        //            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
        //        }
        //    }

        //}

        public static void AceptaLista(InitializationObject InitObject, BCH.Comex.Common.UI_Modulos.UI_Combo lista, int numero)
        {
            int i;
            int j;
            string linea = string.Empty;
            string ind = string.Empty;
            string v = string.Empty;
            string est = string.Empty;
            string a = string.Empty;
            string oficina = string.Empty;
            lista.ListIndex = 0;

            if (numero == 1) //Cuenta Nacional
            {
                for (j = 0; j <= lista.Items.Count - 1; j += 1)
                {
                    if (lista.Items.Count > 0)
                        linea = lista.Items[j].Value;

                    if (!string.IsNullOrEmpty(linea))
                        linea += lista.Items[j].Tag;
                    

                    a = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 1);

                    if (a != "")
                    {
                        oficina = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2);
                        est = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 3);
                        v = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4);
                        ind = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                        i = BuscaIndices(InitObject, "ctaclie", ind);
                        //i = ind.ToInt();

                        if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6) == "BAE")
                        {
                            InitObject.PrtEnt08.cuenta.Mask = "00\\-######\\-##"; //"##\\-######\\-##"; //para que incluya 0
                            InitObject.PrtEnt08.cuenta.Text = "__-______-__";
                        }
                        else
                        {
                            //if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")
                            if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "############";
                                InitObject.PrtEnt08.cuenta.Text = "____________";

                            }
                            else
                            {
                                InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##";//"###\\-#####\\-##";
                                InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                            }
                        }

                        InitObject.PrtEnt08.cuenta.Text = a;
                        InitObject.PRTGLOB.ctaclie[i] = new prtyccta();
                        InitObject.PRTGLOB.ctaclie[i].estado = v.ToInt();
                        InitObject.PRTGLOB.ctaclie[i].cuenta = PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt08.cuenta.Text.ToStr());  // InitObject.PrtEnt08.cuenta.Text.ToStr(); //Al guardar se quita mascara (-)
                        InitObject.PRTGLOB.ctaclie[i].extranjera = false.ToInt();

                        switch (est.Trim())
                        {
                            case "Activa":
                                InitObject.PRTGLOB.ctaclie[i].activace = 1;
                                InitObject.PRTGLOB.ctaclie[i].activabco = 1;
                                break;

                            case "No Activa":
                                InitObject.PRTGLOB.ctaclie[i].activace = 0;
                                InitObject.PRTGLOB.ctaclie[i].activabco = 1;
                                break;

                            case "Cerrada":
                                InitObject.PRTGLOB.ctaclie[i].activace = 0;
                                InitObject.PRTGLOB.ctaclie[i].activabco = 0;
                                break;
                        }
                        InitObject.PRTGLOB.ctaclie[i].moneda = lista.Items[j].Data.ToStr();
                        //InitObject.PRTGLOB.ctaclie[i].moneda = lista.ListIndex.ToStr();
                        InitObject.PRTGLOB.ctaclie[i].indice = ind.ToInt();

                        //if (lista.ListIndex.ToInt() < lista.Items.Count.ToInt() - 2)
                        //    lista.ListIndex = lista.ListIndex.ToInt() + 1;
                    }
                }
                //if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")
                if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                {
                    InitObject.PrtEnt08.cuenta.Mask = "############";
                    InitObject.PrtEnt08.cuenta.Text = "____________";
                }
                else
                {
                    InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##";//"###\\-#####\\-##";
                    InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                }
            }
            else if (numero == 2)  //Cuenta Extranjera
            {
                //if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.Bnumber != "")

                InitObject.PrtEnt08.cuenta.Mask = InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber) ? "############" : "0000\\-#####\\-##";

                for (j = 0; j <= lista.Items.Count.ToInt() - 1; j += 1)
                {
                    if (lista.Items.Count > 0)
                        linea = lista.Items[j].Value;

                    if (!string.IsNullOrEmpty(linea))
                        linea += lista.Items[j].Tag;
                    
                    a = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 1);

                    if (a != "")
                    {
                        oficina = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2);
                        est = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4);
                        v = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                        ind = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6);
                        i = BuscaIndices(InitObject, "ctaclie", ind);

                        InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrWhiteSpace(InitObject.PRTGLOB.Party.Bnumber) ? "____________" : "____-_____-__";

                        InitObject.PrtEnt08.cuenta.Text = a;
                        InitObject.PRTGLOB.ctaclie[i] = new prtyccta();
                        InitObject.PRTGLOB.ctaclie[i].estado = v.ToInt();
                        InitObject.PRTGLOB.ctaclie[i].cuenta = PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt08.cuenta.Text.ToStr());  // InitObject.PrtEnt08.cuenta.Text.ToStr(); //Al guardar se quita mascara (-)
                        InitObject.PRTGLOB.ctaclie[i].extranjera = true.ToInt();

                        switch (est.Trim())
                        {
                            case "Activa":
                                InitObject.PRTGLOB.ctaclie[i].activace = 1;
                                InitObject.PRTGLOB.ctaclie[i].activabco = 1;
                                break;

                            case "No Activa":
                                InitObject.PRTGLOB.ctaclie[i].activace = 0;
                                InitObject.PRTGLOB.ctaclie[i].activabco = 1;
                                break;

                            case "Cerrada":
                                InitObject.PRTGLOB.ctaclie[i].activace = 0;
                                InitObject.PRTGLOB.ctaclie[i].activabco = 0;
                                break;
                        }
                        InitObject.PRTGLOB.ctaclie[i].moneda = lista.Items[j].Data.ToStr();
                        InitObject.PRTGLOB.ctaclie[i].indice = ind.ToInt();

                        //if (lista.ListIndex.ToInt() < lista.Items.Count.ToInt() - 2)
                        //    lista.ListIndex = lista.ListIndex.ToInt() + 1;
                    }
                }
                if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool())
                    InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
            }
        }

        public static int carga_tabmonSy(string MONEDAS)
        {
            int carga_tabmonSy = 0;

            /*string Que = "";
            int j = 0;
            string m = "";
            int i = 0;
            int n = 0;
            string R = "";
            string Query = "";
            int fin = 0;
            fin = -1;*/
            return carga_tabmonSy;
        }

        public static void llama07(InitializationObject InitObj, UnitOfWorkCext01 uow)
        {
            //string s = "";
            int fin = 0;
            //int j = 0;

            InitObj.PrtEnt07.prtrut.Text = descero(InitObj.PRTGLOB.Party.rut);
            InitObj.PrtEnt07.prtrut.Enabled = false;
            InitObj.PrtEnt07.prtcliente[0].Selected = true;
            InitObj.PrtEnt07.prtcliente[1].Selected = false;

            if ((InitObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCuentas) != 0)
            {
                switch (InitObj.PRTGLOB.Party.estado)
                {
                    case T_PRTGLOB.leido:
                    case T_PRTGLOB.modificado:
                        if (InitObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                        {
                            InitObj.PrtEnt07.prtcliente[0].Enabled = false;
                            InitObj.PrtEnt07.prtcliente[1].Enabled = false;
                        }
                        else
                        {
                            InitObj.PrtEnt07.prtcliente[0].Enabled = true;
                            InitObj.PrtEnt07.prtcliente[1].Enabled = true;
                        }

                        escribeinfoparty(InitObj, uow);
                        break;

                    case T_PRTGLOB.nuevo:
                        fin = -1;
                        fin = InitObj.PRTGLOB.ctaclie.GetUpperBound(0);

                        if (fin != -1)
                        {
                            //if (fin == 0 && (InitObj.PRTGLOB.ctaclie[0].cuenta == "" || InitObj.PRTGLOB.ctaclie[0].cuenta == null))
                            if (fin == 0 && (string.IsNullOrEmpty(InitObj.PRTGLOB.ctaclie[0].cuenta.Trim() == null ? string.Empty : InitObj.PRTGLOB.ctaclie[0].cuenta.Trim())))
                            {
                                InitObj.PrtEnt07.prtcliente[0].Enabled = true;
                                InitObj.PrtEnt07.prtcliente[1].Enabled = true;
                            }
                            else
                            {
                                if (InitObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0)
                                {
                                    InitObj.PrtEnt07.prtcliente[0].Enabled = true;
                                    InitObj.PrtEnt07.prtcliente[1].Enabled = true;
                                }
                                else
                                {
                                    InitObj.PrtEnt07.prtcliente[0].Enabled = false;
                                    InitObj.PrtEnt07.prtcliente[1].Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            InitObj.PrtEnt07.prtcliente[0].Enabled = true;
                            InitObj.PrtEnt07.prtcliente[1].Enabled = true;
                        }
                        //if (InitObj.PRTGLOB.Party.oficina.Trim() != "" || InitObj.PRTGLOB.Party.oficina.Trim() != null)

                        if (!string.IsNullOrEmpty(InitObj.PRTGLOB.Party.oficina.Trim() == null ? string.Empty : InitObj.PRTGLOB.Party.oficina.Trim()))
                            escribeinfoparty(InitObj, uow);
                        else
                        {
                            InitObj.PrtEnt07.cboOficina.ListIndex = -1;
                            InitObj.PrtEnt07.cboOficina.Enabled = true;
                            InitObj.PrtEnt07.ejecutivo.ListIndex = -1;
                            InitObj.PrtEnt07.ejecutivo.Enabled = true;
                            InitObj.PrtEnt07.Combo2.ListIndex = -1;
                            InitObj.PrtEnt07.Combo2.Enabled = true;
                            InitObj.PrtEnt07.Combo4.ListIndex = -1;
                            InitObj.PrtEnt07.Combo4.Enabled = true;
                            InitObj.PrtEnt07.aceptar.Enabled = true;
                        }

                        // ------------------ Codigo Nuevo ------------------
                        //if (!Convert.ToBoolean(InitObj.PRTGLOB.Party.PrtGlob.EsBanco) && InitObj.PRTGLOB.Party.oficina.Trim() == "")
                        if (!Convert.ToBoolean(InitObj.PRTGLOB.Party.PrtGlob.EsBanco) && string.IsNullOrEmpty(InitObj.PRTGLOB.Party.oficina.Trim() == null ? string.Empty : InitObj.PRTGLOB.Party.oficina.Trim()))
                        {
                            string msgreto = "";
                            string rut = "";
                            rut = MODWS.ExtraeRut(InitObj.PRTGLOB.Party.rut);
                            
                            //es necesario llamar al servicio y ejecutar la logica que esta en cargar datos 
                            var lista = BCH.Comex.Data.DAL.Services.XGPYServices.ObtenerDatosFichaChica(InitObj.PRTGLOB.Party.swif);
                            Carga_Datos_PrtEnt07(uow, InitObj, lista);
                            InitObj.PrtEnt07.aceptar.Enabled = false;
                            InitObj.PrtEnt07.Fr_CliEsp.Enabled = false;
                            PrtEnt07.HabilitaDeshabilitaFr_CliEsp(InitObj, false);
                            InitObj.PrtEnt07.Combo1.Enabled = false;
                            InitObj.PrtEnt07.Combo4.Enabled = false;
                            InitObj.PrtEnt07.Combo2.Enabled = false;
                            InitObj.PrtEnt07.ejecutivo.Enabled = false;
                            InitObj.PrtEnt07.cboOficina.Enabled = false;
                            InitObj.PrtEnt07.cancelar.Enabled = false;
                        }
                        // ---------------- Fin Codigo Nuevo ----------------                     
                        break;
                }
            }
            else
            {
                if (InitObj.PRTGLOB.Party.tipo == T_PRTGLOB.individuo)
                {
                    InitObj.PrtEnt07.cboOficina.ListIndex = -1;
                    InitObj.PrtEnt07.cboOficina.Enabled = false;
                    InitObj.PrtEnt07.ejecutivo.ListIndex = -1;
                    InitObj.PrtEnt07.ejecutivo.Enabled = false;
                    InitObj.PrtEnt07.Combo2.ListIndex = -1;
                    InitObj.PrtEnt07.Combo2.Enabled = false;
                    InitObj.PrtEnt07.Combo4.ListIndex = -1;
                    InitObj.PrtEnt07.Combo4.Enabled = false;
                }
                else
                {
                    escribeinfoparty(InitObj, uow);
                    InitObj.PrtEnt07.cboOficina.Enabled = true;
                    InitObj.PrtEnt07.ejecutivo.Enabled = true;
                    InitObj.PrtEnt07.Combo2.Enabled = true;
                    InitObj.PrtEnt07.Combo4.Enabled = true;
                }

                InitObj.PrtEnt07.prtcliente[0].Enabled = true;
                InitObj.PrtEnt07.prtcliente[1].Enabled = true;
            }
            if (T_MODWS.ACCESO == "0")
            {
                InitObj.PrtEnt07.cboOficina.Enabled = false;
                InitObj.PrtEnt07.ejecutivo.Enabled = false;
                InitObj.PrtEnt07.Combo2.Enabled = false;
                InitObj.PrtEnt07.Combo4.Enabled = false;
                InitObj.PrtEnt07.Combo1.Enabled = false;
                InitObj.PrtEnt07.Fr_CliEsp.Enabled = false; //Agregar una
                PrtEnt07.HabilitaDeshabilitaFr_CliEsp(InitObj, false); //Agregar una funcion en remplazo de Fr_CliEsp para deshabilitar
                InitObj.PrtEnt07.prtcliente[0].Enabled = false;
                InitObj.PrtEnt07.prtcliente[1].Enabled = false;
            }
        }

        public static void llama08(InitializationObject InitObject, UnitOfWorkCext01 unit)
        {
            string espec = string.Empty;
            string bae_bch = string.Empty;
            int fi = 0;
            int k = 0;
            string oficina = string.Empty;
            string cuenta = string.Empty;
            string moneda = string.Empty;
            int j = 0;
            string a = string.Empty;
            bool salir = false;
            int cext = 0;
            int cnac = 0;
            int fin = 0;
            string s = string.Empty;
            int codof = 0;
            string est = string.Empty;
            int i = 0;
            string ind = string.Empty;
            salir = false;

            //Variables DECLARADAS POR JF
            int regNac = 0;
            int regExt = 0;
            //int Cantcaracteres = 0;

            // ------------------ Codigo Nuevo ------------------
            //Array.Resize(ref InitObject.MODWS.CtaCCOL, 1);
            // ---------------- Fin Codigo Nuevo ----------------
            switch (InitObject.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    InitObject.PrtEnt08.Label2.Text = UTILES.EspaciosAlineado("Oficina", 80); //50
                    InitObject.PrtEnt08.Label7.Visible = true;
                    InitObject.PrtEnt08.Titulo.Text = "Cuentas Corrientes Cliente";
                    InitObject.PrtEnt08.Frame1.Text = "Cuentas Corrientes Moneda Nacional";
                    InitObject.PrtEnt08.Frame2.Text = "Cuentas Corrientes Moneda Extranjera";
                    InitObject.PrtEnt08.Label4.Text = UTILES.EspaciosAlineado("Nº Cuenta", 60); //50

                    switch (InitObject.PRTGLOB.Party.estado)
                    {
                        case T_PRTGLOB.nuevo:
                            fin = -1;
                            fin = InitObject.PRTGLOB.ctaclie.GetUpperBound(0);

                            if (fin != -1)
                            {
                                if (fin == 0 && InitObject.PRTGLOB.ctaclie[0].cuenta == "")
                                {
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                    InitObject.PaginaWebQueAbrir = "Index";
                                    return;
                                }
                            }
                            else
                            {
                                InitObject.PRTGLOB.ctaclie = new prtyccta[0];
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                InitObject.PaginaWebQueAbrir = "Index";
                                return;
                            }

                            break;

                        case T_PRTGLOB.leido:
                            lee_cuentasSy(InitObject, InitObject.PRTGLOB.Party.idparty, unit);
                            break;

                        case T_PRTGLOB.modificado:
                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCuentas) != 0)
                            {
                                if (InitObject.PRTGLOB.ctaclie[0] == null)
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[0];

                                fin = -1;
                                fin = InitObject.PRTGLOB.ctaclie.GetUpperBound(0);

                                if (fin != -1)
                                {
                                    if (fin == 0 && InitObject.PRTGLOB.ctaclie[0].cuenta == null)
                                        lee_cuentasSy(InitObject, InitObject.PRTGLOB.Party.idparty, unit);
                                }
                                else
                                {
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[0];
                                    lee_cuentasSy(InitObject, InitObject.PRTGLOB.Party.idparty, unit);
                                }
                            }
                            else
                            {
                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem());
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem());
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                InitObject.PaginaWebQueAbrir = "Index";
                                return;
                            }
                            break;
                    }
                    for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                    {
                        if (InitObject.PRTGLOB.ctaclie[i] != null)
                        {
                            if (InitObject.PRTGLOB.ctaclie[i].extranjera != 0)
                            {
                                cext = cext + 1;
                            }
                            else
                            {
                                cnac = cnac + 1;
                            }
                        }
                    }
                    if (cext != 0)
                    {
                        for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                        {
                            if (InitObject.PRTGLOB.ctaclie[i] != null && InitObject.PRTGLOB.ctaclie[i].extranjera != 0)
                            {
                                if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty( InitObject.PRTGLOB.Party.Bnumber))
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "############";
                                    InitObject.PrtEnt08.cuenta.Text = "____________";
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadRight(12, '_');
                                }
                                else
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "0000\\-#####\\-##";//"####\\-#####\\-##";
                                    InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadLeft(10, '0');
                                }

                                est = InitObject.PRTGLOB.ctaclie[i].activabco != 0 ? InitObject.PRTGLOB.ctaclie[i].activace != 0 ? "Activa" : "No Activa" : "Cerrada";
                                a = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].estado, String.Empty);
                                
                                for (j = 0; j <= InitObject.PRTGLOB.cod_nom_moneda.GetUpperBound(0); j += 1)
                                {
                                    if (InitObject.PRTGLOB.cod_nom_moneda[j].CodMoneda.ToVal() == InitObject.PRTGLOB.ctaclie[i].moneda.ToVal())
                                    {
                                        moneda = InitObject.PRTGLOB.cod_nom_moneda[j].NombMoneda.UCase();
                                        break;
                                    }
                                }

                                ind = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].indice, String.Empty);
                                cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;

                                if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                                {
                                    codof = MODPRTY.SyGet_Ofi(InitObject, cuenta, 1);
                                    //codof = MODPRTY.Obtiene_Oficina(cuenta); //Preguntar                                  
                                    oficina = "";

                                    for (k = 0; k <= InitObject.PRTGLOB.oficinas.GetUpperBound(0); k += 1)
                                    {
                                        if (codof == InitObject.PRTGLOB.oficinas[k].codigo)
                                        {
                                            oficina = InitObject.PRTGLOB.oficinas[k].nombre;
                                            break;
                                        }
                                    }
                                    if (string.IsNullOrEmpty(oficina))
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitCuentas,
                                            Text = " No encontró Oficina BCH.",
                                            Type = TipoMensaje.Informacion

                                        });

                                        return;
                                    }
                                }
                                //s = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask) + 9.Char() + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, oficina, 10) + 9.Char() + moneda + 9.Char() + est + 9.Char() + a + 9.Char() + ind; //Se agrega formato a la cuenta 01/11/2015

                                //if (!string.IsNullOrEmpty(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, oficina, 10)))
                                //    Cantcaracteres = 70; //50
                                //else
                                //    Cantcaracteres = 140; //100
                                int largo = 33;
                                int largo2 = 35;
                                int largo3 = 37;
                                s = UTILES.EspaciosAlineadoMonoSpace("",7) + UTILES.EspaciosAlineadoMonoSpace(VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask), largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, oficina, 10), largo2) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(moneda, largo3) + VB6Helpers.Chr(9) + est; //Se agrega formato a la cuenta 01/11/2015
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = int.Parse(InitObject.PRTGLOB.ctaclie[i].moneda ?? "0"), Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind, Value = s, ID = regExt.ToString() });
                                regExt = regExt + 1;
                            }
                        }

                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                        InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = regExt, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s }); //Ya estaba                

                        if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool())
                            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                    }
                    else
                    {
                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                        InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                    }

                    if (cnac != 0)
                    {
                        for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                        {
                            if (InitObject.PRTGLOB.ctaclie[i] != null && InitObject.PRTGLOB.ctaclie[i].extranjera == 0)
                            {
                                if (InitObject.PRTGLOB.ctaclie[i].activabco != 0)
                                {
                                    if (InitObject.PRTGLOB.ctaclie[i].activace != 0)
                                        est = "Activa";
                                    else
                                        est = "No Activa";
                                }
                                else
                                    est = "Cerrada";

                                a = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].estado, String.Empty);

                                for (j = 0; j <= InitObject.PRTGLOB.cod_nom_moneda.GetUpperBound(0); j += 1)
                                {
                                    if (InitObject.PRTGLOB.cod_nom_moneda[j].CodMoneda == InitObject.PRTGLOB.ctaclie[i].moneda)
                                    {
                                        moneda = InitObject.PRTGLOB.cod_nom_moneda[j].NombMoneda;
                                        break;
                                    }
                                }

                                ind = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].indice, String.Empty);

                                if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                                {
                                    // RESCATA NUMERO DE OFICINA
                                    cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;
                                    fi = MODPRTY.SyGet_Ofi(InitObject, cuenta, 2);

                                    if (fi == 0)
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitParty,
                                            Text = " No existe Aplicación V_CtaCte.exe. Se asumira que es oficina BCH. Reporte este problema.",
                                            Type = TipoMensaje.Informacion
                                        });

                                        bae_bch = "BCH";
                                    }

                                    if (fi == 1)
                                        bae_bch = "BAE";

                                    if (fi == 2)
                                        bae_bch = "BCH";

                                    if (fi == 3)
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitParty,
                                            Text = " La Aplicación no encontró N° de Cuenta.",
                                            Type = TipoMensaje.Informacion
                                        });
                                        bae_bch = "BCH";
                                    }

                                    // codof = MODPRTY.Obtiene_Oficina(cuenta);                                  
                                    oficina = "";

                                    for (k = 0; k <= InitObject.PRTGLOB.oficinas.GetUpperBound(0); k += 1)
                                    {
                                        if (codof == InitObject.PRTGLOB.oficinas[k].codigo)
                                        {
                                            oficina = InitObject.PRTGLOB.oficinas[k].nombre;
                                            break;
                                        }
                                    }

                                    if (oficina == "" || oficina == null)
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitParty,
                                            Text = " No se encontró Oficina para la Cuenta.",
                                            Type = TipoMensaje.Informacion
                                        });
                                    }
                                }
                                else
                                {
                                    cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;
                                    bae_bch = "BCH";
                                }

                                if (bae_bch == "BAE")
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "00\\-#####\\-##"; //"##\\-######\\-##";
                                    //InitObject.PrtEnt08.cuenta.Text = "__-______-__";                                  
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadLeft(9,'0');

                                }
                                else if (bae_bch == "BCH" && string.IsNullOrEmpty( InitObject.PRTGLOB.Party.Bnumber))
                                {
                                    // Fin Modif: 10-08-2012
                                    // -------------------------------------
                                    InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";

                                    //InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                                    //((dynamic)PrtEnt08.DefInstance.cuenta).SelText = PRTGLOB.ctaclie[i].cuenta;
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadLeft(10,'0');

                                }
                                else if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty( InitObject.PRTGLOB.Party.Bnumber))
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "############";
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadRight(12,'_');//VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].cuenta, "____________");

                                }

                                //if (!string.IsNullOrEmpty(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10)))
                                //    Cantcaracteres = 100; // 50
                                //else
                                //    Cantcaracteres = 200; //100


                                //s = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask) + VB6Helpers.Chr(9) + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10) + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch; //Se agrega formato a la cuenta 01/11/2015
                                //s = UTILES.EspaciosEnBlancoRight(VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask), Cantcaracteres) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10), 70) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(est, 50);//Se  columnas que no se ven 25/02/2016
                                int largo = 50;
                                int largo2 = 40;
                                s = UTILES.EspaciosAlineadoMonoSpace("",7) + UTILES.EspaciosAlineadoMonoSpace(VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask), largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10), largo2) + VB6Helpers.Chr(9) + est;//Se  columnas que no se ven 25/02/2016
                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = int.Parse(InitObject.PRTGLOB.ctaclie[i].moneda ?? "0"), Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch, Value = s, ID = regNac.ToString() });
                                regNac = regNac + 1;
                            }
                        }

                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";

                        if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty( InitObject.PRTGLOB.Party.Bnumber))
                        {
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = regNac, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                            InitObject.PrtEnt08.cuenta.Mask = "############";
                            InitObject.PrtEnt08.cuenta.Text = "____________";
                        }
                        else
                        {
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = regNac, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                            InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                        }
                    }
                    else
                    {
                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                        InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                    }

                    respalda_ctas(InitObject, "ctaclie");
                    break;
                case T_PRTGLOB.tipo_banco:
                    InitObject.PrtEnt08.Label7.Visible = false;
                    InitObject.PrtEnt08.Label2.Text = UTILES.EspaciosAlineado("Moneda", 80); //50
                    InitObject.PrtEnt08.Titulo.Text = "Cuentas Corrientes y/o Líneas Crédito Banco";
                    InitObject.PrtEnt08.Frame1.Text = "Cuentas Corrientes ";
                    InitObject.PrtEnt08.Frame2.Text = "Líneas de Crédito";
                    InitObject.PrtEnt08.Label4.Text = UTILES.EspaciosAlineado("Nº Línea", 60); //50

                    if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) != 0)
                    {
                        switch (InitObject.PRTGLOB.Party.estado)
                        {
                            case T_PRTGLOB.nuevo:
                                fin = -1;

                                fin = InitObject.PRTGLOB.ctabancos.GetUpperBound(0);
                                if (fin != -1)
                                {
                                    if (fin == 0 && InitObject.PRTGLOB.ctabancos[0].cuenta == "")
                                    {
                                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                        InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1" + VB6Helpers.Chr(9) + "0", Value = s });
                                        salir = true;
                                    }
                                }
                                else
                                {
                                    InitObject.PRTGLOB.ctabancos = new prtybcta[0];
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1" + VB6Helpers.Chr(9) + "0", Value = s });
                                    salir = true;
                                }

                                break;
                            case T_PRTGLOB.leido:
                                lee_bctaSy(InitObject, InitObject.PRTGLOB.Party.idparty);
                                break;
                            case T_PRTGLOB.modificado:
                                if (InitObject.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal != 0)
                                {
                                    if (InitObject.PRTGLOB.ctabancos[0] != null)
                                    {
                                        if (InitObject.PRTGLOB.ctabancos[0].cuenta == "")
                                        {
                                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                                            {
                                                Data = 0,
                                                Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1" + VB6Helpers.Chr(9) + "0",
                                                Value = s

                                            });

                                            //InitObject.PrtEnt08.Lista1.Items.Add(new UI_ComboItem { Data = 0, Value = s });
                                            salir = true;
                                        }
                                    }
                                }
                                else
                                {
                                    lee_bctaSy(InitObject, InitObject.PRTGLOB.Party.idparty);
                                }
                                break;
                        }
                        if (!salir)
                        {
                            for (i = 0; i <= InitObject.PRTGLOB.ctabancos.GetUpperBound(0); i += 1)
                            {
                                if (InitObject.PRTGLOB.ctabancos[i].activa != 0)
                                {
                                    est = "Activa";
                                }
                                else
                                {
                                    est = "No Activa";
                                }
                                a = VB6Helpers.Format(InitObject.PRTGLOB.ctabancos[i].estado, String.Empty);
                                for (j = 0; j <= InitObject.PRTGLOB.cod_nom_moneda.GetUpperBound(0); j += 1)
                                {
                                    if (InitObject.PRTGLOB.cod_nom_moneda[j].CodMoneda == InitObject.PRTGLOB.ctabancos[i].moneda)
                                    {
                                        moneda = InitObject.PRTGLOB.cod_nom_moneda[j].NombMoneda;
                                        break;
                                    }
                                }
                                ind = VB6Helpers.Format(InitObject.PRTGLOB.ctabancos[i].indice, String.Empty);
                                if (InitObject.PRTGLOB.ctabancos[i].especial != 0)
                                {
                                    espec = "1";
                                }
                                else
                                {
                                    espec = "0";
                                }
                                s = UTILES.EspaciosAlineado(InitObject.PRTGLOB.ctabancos[i].cuenta,100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(moneda,100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(est,100);

                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = int.Parse(InitObject.PRTGLOB.ctabancos[i].moneda ?? "0"), Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + espec, Value = s, ID = i.ToString() });
                            }

                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem());
                            //s = "" + 9.Char() + "" + 9.Char() + "" + 9.Char() + "0" + 9.Char() + "-1" + 9.Char() + "0";
                            //InitObject.PrtEnt08.Lista1.Items.Add(new UI_ComboItem { Data = 0, Value = s });
                        }
                        InitObject.PrtEnt08.Lista1.ListIndex = 0;
                        respalda_ctas(InitObject, "ctabancos");
                    }
                    else
                    {
                        InitObject.PrtEnt08.Lista1.ListIndex = -1;
                    }
                    salir = false;
                    if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) != 0)
                    {
                        switch (InitObject.PRTGLOB.Party.estado)
                        {
                            case T_PRTGLOB.nuevo:
                                fin = -1;
                                fin = InitObject.PRTGLOB.linbancos.GetUpperBound(0);
                                if (fin != -1)
                                {
                                    if (fin == 0 && InitObject.PRTGLOB.linbancos[0].linea == "")
                                    {
                                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                        InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                        salir = true;
                                    }
                                }
                                else
                                {
                                    InitObject.PRTGLOB.linbancos = new prtyblinea[1];
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                    salir = true;
                                }

                                break;
                            case T_PRTGLOB.leido:
                                lee_blineaSy(InitObject, InitObject.PRTGLOB.Party.idparty);
                                break;
                            case T_PRTGLOB.modificado:
                                if (InitObject.PRTGLOB.Party.PrtGlob.cambio_a_acreedor != 0)
                                {
                                    if (InitObject.PRTGLOB.linbancos[0].linea == "")
                                    {
                                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                        InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                        salir = true;
                                    }
                                }
                                else
                                {
                                    lee_blineaSy(InitObject, InitObject.PRTGLOB.Party.idparty);
                                }
                                break;
                        }
                        if (!salir)
                        {
                            for (i = 0; i <= InitObject.PRTGLOB.linbancos.GetUpperBound(0); i += 1)
                            {
                                if (InitObject.PRTGLOB.linbancos[i] != null)
                                {
                                    if (InitObject.PRTGLOB.linbancos[i].activa != 0)
                                    {
                                        est = "Activa";
                                    }
                                    else
                                    {
                                        est = "No Activa";
                                    }
                                    a = VB6Helpers.Format(InitObject.PRTGLOB.linbancos[i].estado, String.Empty);
                                    for (j = 0; j <= InitObject.PRTGLOB.cod_nom_moneda.GetUpperBound(0); j += 1)
                                    {
                                        if (InitObject.PRTGLOB.cod_nom_moneda[j].CodMoneda == InitObject.PRTGLOB.linbancos[i].moneda)
                                        {
                                            moneda = InitObject.PRTGLOB.cod_nom_moneda[j].NombMoneda;
                                            break;
                                        }
                                    }
                                }

                                ind = VB6Helpers.Format(InitObject.PRTGLOB.linbancos[i].indice, String.Empty);
                                s = UTILES.EspaciosAlineado(InitObject.PRTGLOB.linbancos[i].linea,100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(moneda,100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(est,100);
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = int.Parse(InitObject.PRTGLOB.linbancos[i].moneda ?? "0"), Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind, Value = s, ID = i.ToString() });
                            }

                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                            InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + 9.Char() + "-1", Value = s });
                        }
                        InitObject.PrtEnt08.Lista2.ListIndex = 0;
                        respalda_ctas(InitObject, "linbancos");
                    }
                    else
                    {
                        InitObject.PrtEnt08.Lista2.ListIndex = -1;
                    }
                    if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) != 0)
                    {
                        InitObject.PrtEnt08.Lista2.ListIndex = -1;
                    }
                    else
                    {
                        InitObject.PrtEnt08.Lista1.ListIndex = -1;
                    }
                    break;

                case T_PRTGLOB.individuo:
                    InitObject.PrtEnt08.Label2.Text = UTILES.EspaciosAlineado("Oficina", 80); //50
                    InitObject.PrtEnt08.Label7.Visible = true;
                    InitObject.PrtEnt08.Titulo.Text = "Cuentas Corrientes Cliente";
                    InitObject.PrtEnt08.Frame1.Text = "Cuentas Corrientes Moneda Nacional";
                    InitObject.PrtEnt08.Frame2.Text = "Cuentas Corrientes Moneda Extranjera";
                    InitObject.PrtEnt08.Label4.Text = UTILES.EspaciosAlineado("Nº Cuenta", 60); //50

                    switch (InitObject.PRTGLOB.Party.estado)
                    {
                        case T_PRTGLOB.nuevo:
                            fin = -1;
                            fin = InitObject.PRTGLOB.ctaclie.GetUpperBound(0);
                            if (fin != -1)
                            {
                                if (fin == 0 && InitObject.PRTGLOB.ctaclie[0].cuenta == "")
                                {
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                    InitObject.PaginaWebQueAbrir = "Index";
                                    return;
                                }
                            }
                            else
                            {
                                InitObject.PRTGLOB.ctaclie = new prtyccta[0];
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                InitObject.PaginaWebQueAbrir = "Index";
                                return;
                            }
                            break;

                        case T_PRTGLOB.leido:
                            lee_cuentasSy(InitObject, InitObject.PRTGLOB.Party.idparty, unit);
                            break;
                        case T_PRTGLOB.modificado:
                            if ((InitObject.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCuentas) != 0)
                            {
                                if (InitObject.PRTGLOB.ctaclie[0] == null)
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[0];

                                fin = -1;
                                fin = InitObject.PRTGLOB.ctaclie.GetUpperBound(0);
                                if (fin != -1)
                                {
                                    if (fin == 0 && InitObject.PRTGLOB.ctaclie[0].cuenta == null)
                                        lee_cuentasSy(InitObject, InitObject.PRTGLOB.Party.idparty, unit);
                                    
                                }
                                else
                                {
                                    InitObject.PRTGLOB.ctaclie = new prtyccta[0];
                                    lee_cuentasSy(InitObject, InitObject.PRTGLOB.Party.idparty, unit);
                                }
                            }
                            else
                            {
                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem());
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem());
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                InitObject.PaginaWebQueAbrir = "Index";
                                return;
                            }
                            break;
                    }

                    for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                    {
                        if (InitObject.PRTGLOB.ctaclie[i] != null)
                        {
                            if (InitObject.PRTGLOB.ctaclie[i].extranjera != 0)
                                cext = cext + 1;
                            else
                                cnac = cnac + 1;
                        }
                    }

                    if (cext != 0)
                    {
                        for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                        {
                            if (InitObject.PRTGLOB.ctaclie[i] != null && InitObject.PRTGLOB.ctaclie[i].extranjera != 0)
                            {
                                if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "############";
                                    InitObject.PrtEnt08.cuenta.Text = "____________";
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadRight(12, '_');
                                }
                                else
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "0000\\-#####\\-##";//"####\\-#####\\-##";
                                    InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadLeft(10, '0');
                                }


                                est = InitObject.PRTGLOB.ctaclie[i].activabco != 0 ? InitObject.PRTGLOB.ctaclie[i].activace != 0 ? "Activa" : "No Activa" : "Cerrada";
                                a = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].estado, String.Empty);

                                for (j = 0; j <= InitObject.PRTGLOB.cod_nom_moneda.GetUpperBound(0); j += 1)
                                {
                                    if (InitObject.PRTGLOB.cod_nom_moneda[j].CodMoneda.ToVal() == InitObject.PRTGLOB.ctaclie[i].moneda.ToVal())
                                    {
                                        moneda = InitObject.PRTGLOB.cod_nom_moneda[j].NombMoneda.UCase();
                                        break;
                                    }
                                }

                                ind = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].indice, String.Empty);
                                cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;

                                if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                                {
                                    codof = MODPRTY.SyGet_Ofi(InitObject, cuenta, 1);
                                    //codof = MODPRTY.Obtiene_Oficina(cuenta); //Preguntar                                  
                                    oficina = "";

                                    for (k = 0; k <= InitObject.PRTGLOB.oficinas.GetUpperBound(0); k += 1)
                                    {
                                        if (codof == InitObject.PRTGLOB.oficinas[k].codigo)
                                        {
                                            oficina = InitObject.PRTGLOB.oficinas[k].nombre;
                                            break;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(oficina))
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitCuentas,
                                            Text = " No encontró Oficina BCH.",
                                            Type = TipoMensaje.Informacion

                                        });
                                        return;
                                    }
                                }

                                //s = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask) + 9.Char() + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, oficina, 10) + 9.Char() + moneda + 9.Char() + est + 9.Char() + a + 9.Char() + ind; //Se agrega formato a la cuenta 01/11/2015

                                //if (!string.IsNullOrEmpty(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, oficina, 10)))
                                //    Cantcaracteres = 70; //50
                                //else
                                //    Cantcaracteres = 140; //100
                                int largo = 33;
                                int largo2 = 35;
                                int largo3 = 37;
                                s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask), largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, oficina, 10), largo2) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(moneda, largo3) + VB6Helpers.Chr(9) + est; //Se agrega formato a la cuenta 01/11/2015
                                InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = int.Parse(InitObject.PRTGLOB.ctaclie[i].moneda ?? "0"), Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind, Value = s, ID = regExt.ToString() });
                                regExt = regExt + 1;
                            }
                        }

                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                        InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = regExt, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s }); //Ya estaba                

                        if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool())
                            InitObject.PrtEnt08.cuenta.Text = "____-_____-__";
                    }
                    else
                    {
                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                        InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                    }


                    if (cnac != 0)
                    {
                        for (i = 0; i <= InitObject.PRTGLOB.ctaclie.GetUpperBound(0); i += 1)
                        {
                            if (InitObject.PRTGLOB.ctaclie[i] != null && InitObject.PRTGLOB.ctaclie[i].extranjera == 0)
                            {
                                est = InitObject.PRTGLOB.ctaclie[i].activabco != 0 ? InitObject.PRTGLOB.ctaclie[i].activace != 0 ? "Activa" : "No Activa" : "Cerrada";
                                a = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].estado, String.Empty);

                                for (j = 0; j <= InitObject.PRTGLOB.cod_nom_moneda.GetUpperBound(0); j += 1)
                                {
                                    if (InitObject.PRTGLOB.cod_nom_moneda[j].CodMoneda == InitObject.PRTGLOB.ctaclie[i].moneda)
                                    {
                                        moneda = InitObject.PRTGLOB.cod_nom_moneda[j].NombMoneda;
                                        break;
                                    }
                                }

                                ind = VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].indice, String.Empty);

                                if (!InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                                {
                                    // RESCATA NUMERO DE OFICINA
                                    cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;
                                    fi = MODPRTY.SyGet_Ofi(InitObject, cuenta, 2);
                                    if (fi == 0)
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitParty,
                                            Text = " No existe Aplicación V_CtaCte.exe. Se asumira que es oficina BCH. Reporte este problema.",
                                            Type = TipoMensaje.Informacion
                                        });

                                        bae_bch = "BCH";
                                    }
                                    if (fi == 1)
                                        bae_bch = "BAE";

                                    if (fi == 2)
                                        bae_bch = "BCH";

                                    if (fi == 3)
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitParty,
                                            Text = " La Aplicación no encontró N° de Cuenta.",
                                            Type = TipoMensaje.Informacion
                                        });
                                        bae_bch = "BCH";
                                    }

                                    // codof = MODPRTY.Obtiene_Oficina(cuenta);                                  
                                    oficina = "";

                                    for (k = 0; k <= InitObject.PRTGLOB.oficinas.GetUpperBound(0); k += 1)
                                    {
                                        if (codof == InitObject.PRTGLOB.oficinas[k].codigo)
                                        {
                                            oficina = InitObject.PRTGLOB.oficinas[k].nombre;
                                            break;
                                        }
                                    }
                                    if (oficina == "" || oficina == null)
                                    {
                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Title = T_PRTGLOB.TitParty,
                                            Text = " No se encontró Oficina para la Cuenta.",
                                            Type = TipoMensaje.Informacion
                                        });
                                    }
                                }
                                else
                                {
                                    cuenta = InitObject.PRTGLOB.ctaclie[i].cuenta;
                                    bae_bch = "BCH";
                                }

                                if (bae_bch == "BAE")
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "00\\-#####\\-##"; //"##\\-######\\-##";
                                    //InitObject.PrtEnt08.cuenta.Text = "__-______-__";                                  
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadLeft(9, '0');

                                }
                                else if (bae_bch == "BCH" && string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                {
                                    // Fin Modif: 10-08-2012
                                    // -------------------------------------
                                    InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";

                                    //InitObject.PrtEnt08.cuenta.Text = "___-_____-__";
                                    //((dynamic)PrtEnt08.DefInstance.cuenta).SelText = PRTGLOB.ctaclie[i].cuenta;
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadLeft(10, '0');

                                }
                                else if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                                {
                                    InitObject.PrtEnt08.cuenta.Mask = "############";
                                    InitObject.PrtEnt08.cuenta.Text = InitObject.PRTGLOB.ctaclie[i].cuenta.Trim().PadRight(12, '_');//VB6Helpers.Format(InitObject.PRTGLOB.ctaclie[i].cuenta, "____________");

                                }

                                //if (!string.IsNullOrEmpty(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10)))
                                //    Cantcaracteres = 100; // 50
                                //else
                                //    Cantcaracteres = 200; //100


                                //s = VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask) + VB6Helpers.Chr(9) + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10) + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch; //Se agrega formato a la cuenta 01/11/2015
                                //s = UTILES.EspaciosEnBlancoRight(VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask), Cantcaracteres) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10), 70) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(est, 50);//Se  columnas que no se ven 25/02/2016
                                int largo = 50;
                                int largo2 = 40;
                                s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(VB6Helpers.Format(InitObject.PrtEnt08.cuenta.Text, InitObject.PrtEnt08.cuenta.Mask), largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, oficina, 10), largo2) + VB6Helpers.Chr(9) + est;//Se  columnas que no se ven 25/02/2016
                                InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                                {
                                    Data = int.Parse(InitObject.PRTGLOB.ctaclie[i].moneda ?? "0"),
                                    Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch,
                                    Value = s,
                                    ID = regNac.ToString()
                                });
                                regNac = regNac + 1;
                            }
                        }

                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";

                        if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                        {
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = regNac, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                            InitObject.PrtEnt08.cuenta.Mask = "############";
                            InitObject.PrtEnt08.cuenta.Text = "____________";
                        }
                        else
                        {
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = regNac, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                            InitObject.PrtEnt08.cuenta.Mask = "000\\-#####\\-##"; //"###\\-#####\\-##";
                        }
                    }
                    else
                    {
                        s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                        InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                    }

                    respalda_ctas(InitObject, "ctaclie");
                    break;
            }
        }

        public static void llama11(InitializationObject initObj)
        {
            //string s = "";
            int fin = 0;

            if (initObj.PRTGLOB.Party.codbco == 0)
                initObj.PrtEnt11.prtcodigo.Text = string.Empty;
            else
                initObj.PrtEnt11.prtcodigo.Text = initObj.PRTGLOB.Party.codbco.ToStr();

            if (initObj.PRTGLOB.Party.swif == null || initObj.PRTGLOB.Party.swif == "")
                initObj.PrtEnt11.prtswif.Text = string.Empty;
            else
                initObj.PrtEnt11.prtswif.Text = initObj.PRTGLOB.Party.swif.Trim();

            if (initObj.PRTGLOB.Party.ejecorr == null || initObj.PRTGLOB.Party.ejecorr == "")
                initObj.PrtEnt11.ejecorr.Text = string.Empty;
            else
                initObj.PrtEnt11.ejecorr.Text = initObj.PRTGLOB.Party.ejecorr.Trim();


            if (initObj.PRTGLOB.Party.sirut != 0)
                initObj.PrtEnt11.prtrut.Text = descero(initObj.PRTGLOB.Party.rut);
            else
                initObj.PrtEnt11.prtrut.Text = String.Empty;

            if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) != 0)
            {
                switch (initObj.PRTGLOB.Party.estado)
                {
                    case T_PRTGLOB.leido:
                        if (T_PRTGLOB.blin_eliminadas != 0)
                        {
                            initObj.PrtEnt11._prttipob_[0].Checked = true;
                            initObj.PrtEnt11._prttipob_[0].Enabled = true;
                            initObj.PrtEnt11._prttipob_[2].Checked = false;
                            initObj.PrtEnt11._prttipob_[2].Enabled = true;
                        }
                        else
                        {
                            initObj.PrtEnt11._prttipob_[0].Checked = true;
                            initObj.PrtEnt11._prttipob_[0].Enabled = false;
                            initObj.PrtEnt11._prttipob_[2].Checked = false;
                            initObj.PrtEnt11._prttipob_[2].Enabled = false;
                        }
                        break;

                    case T_PRTGLOB.nuevo:
                        fin = -1;
                        fin = initObj.PRTGLOB.linbancos.GetUpperBound(0);
                        if (fin != -1)
                        {
                            if (fin == 0 && initObj.PRTGLOB.linbancos[0].linea == "")
                            {
                                initObj.PrtEnt11._prttipob_[0].Checked = true;
                                initObj.PrtEnt11._prttipob_[0].Enabled = true;
                                initObj.PrtEnt11._prttipob_[2].Checked = false;
                                initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            }
                            else
                            {
                                if (T_PRTGLOB.blin_eliminadas != 0)
                                {
                                    initObj.PrtEnt11._prttipob_[0].Checked = true;
                                    initObj.PrtEnt11._prttipob_[0].Enabled = true;
                                    initObj.PrtEnt11._prttipob_[2].Checked = false;
                                    initObj.PrtEnt11._prttipob_[2].Enabled = true;
                                }
                                else
                                {
                                    initObj.PrtEnt11._prttipob_[0].Checked = true;
                                    initObj.PrtEnt11._prttipob_[0].Enabled = false;
                                    initObj.PrtEnt11._prttipob_[2].Checked = false;
                                    initObj.PrtEnt11._prttipob_[2].Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            initObj.PrtEnt11._prttipob_[0].Checked = true;
                            initObj.PrtEnt11._prttipob_[0].Enabled = true;
                            initObj.PrtEnt11._prttipob_[2].Checked = false;
                            initObj.PrtEnt11._prttipob_[2].Enabled = true;
                        }

                        break;
                    case T_PRTGLOB.modificado:
                        if (initObj.PRTGLOB.Party.PrtGlob.cambio_a_acreedor != 0)
                        {
                            fin = -1;
                            fin = initObj.PRTGLOB.linbancos.GetUpperBound(0);
                            if (fin != -1)
                            {
                                if (initObj.PRTGLOB.linbancos[0].linea == "")
                                {
                                    initObj.PrtEnt11._prttipob_[0].Checked = true;
                                    initObj.PrtEnt11._prttipob_[0].Enabled = true;
                                    initObj.PrtEnt11._prttipob_[2].Checked = false;
                                    initObj.PrtEnt11._prttipob_[2].Enabled = true;
                                }
                                else
                                {
                                    if (T_PRTGLOB.blin_eliminadas != 0)
                                    {
                                        initObj.PrtEnt11._prttipob_[0].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                        initObj.PrtEnt11._prttipob_[0].Enabled = true;
                                        initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                        initObj.PrtEnt11._prttipob_[2].Enabled = true;
                                    }
                                    else
                                    {

                                        initObj.PrtEnt11._prttipob_[0].Checked = true;
                                        initObj.PrtEnt11._prttipob_[0].Enabled = false;
                                        initObj.PrtEnt11._prttipob_[2].Checked = false;
                                        initObj.PrtEnt11._prttipob_[2].Enabled = false;
                                    }
                                }
                            }
                            else
                            {
                                initObj.PrtEnt11._prttipob_[0].Checked = true;
                                initObj.PrtEnt11._prttipob_[0].Enabled = true;
                                initObj.PrtEnt11._prttipob_[2].Checked = false;
                                initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            }
                        }
                        else
                        {
                            if (T_PRTGLOB.blin_eliminadas != 0)
                            {
                                initObj.PrtEnt11._prttipob_[0].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                initObj.PrtEnt11._prttipob_[0].Enabled = true;
                                initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            }
                            else
                            {
                                initObj.PrtEnt11._prttipob_[0].Checked = true;
                                initObj.PrtEnt11._prttipob_[0].Enabled = false;
                                initObj.PrtEnt11._prttipob_[2].Checked = false;
                                initObj.PrtEnt11._prttipob_[2].Enabled = false;
                            }
                        }
                        break;
                }
            }
            else
            {
                initObj.PrtEnt11._prttipob_[0].Checked = false;
                initObj.PrtEnt11._prttipob_[0].Enabled = true;
                initObj.PrtEnt11._prttipob_[2].Checked = false;
                initObj.PrtEnt11._prttipob_[2].Enabled = true;
            }

            if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) != 0)
            {
                switch (initObj.PRTGLOB.Party.estado)
                {
                    case T_PRTGLOB.leido:
                        if (T_PRTGLOB.bctas_eliminadas != 0)
                        {
                            initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                            initObj.PrtEnt11._prttipob_[1].Enabled = true;
                            initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                            initObj.PrtEnt11._prttipob_[2].Enabled = true;
                        }
                        else
                        {
                            initObj.PrtEnt11._prttipob_[1].Checked = true;
                            initObj.PrtEnt11._prttipob_[1].Enabled = false;
                            initObj.PrtEnt11._prttipob_[2].Checked = false;
                            initObj.PrtEnt11._prttipob_[2].Enabled = false;
                        }
                        break;

                    case T_PRTGLOB.nuevo:
                        fin = -1;

                        fin = initObj.PRTGLOB.ctabancos.GetUpperBound(0);
                        if (fin != -1)
                        {
                            if (fin == 0 && initObj.PRTGLOB.ctabancos[0].cuenta == "")
                            {
                                initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                initObj.PrtEnt11._prttipob_[1].Enabled = true;
                                initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            }
                            else
                            {
                                if (T_PRTGLOB.bctas_eliminadas != 0)
                                {
                                    initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                    initObj.PrtEnt11._prttipob_[1].Enabled = true;
                                    initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                    initObj.PrtEnt11._prttipob_[2].Enabled = true;
                                }
                                else
                                {
                                    initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                    initObj.PrtEnt11._prttipob_[1].Enabled = false;
                                    initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                    initObj.PrtEnt11._prttipob_[2].Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                            initObj.PrtEnt11._prttipob_[1].Enabled = true;
                            initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                            initObj.PrtEnt11._prttipob_[2].Enabled = true;
                        }
                        break;

                    case T_PRTGLOB.modificado:
                        if (initObj.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal != 0)
                        {
                            fin = -1;

                            fin = initObj.PRTGLOB.ctabancos.GetUpperBound(0);
                            if (fin != -1)
                            {
                                if (initObj.PRTGLOB.ctabancos[0].cuenta == "")
                                {
                                    initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                    initObj.PrtEnt11._prttipob_[1].Enabled = true;
                                    initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                    initObj.PrtEnt11._prttipob_[2].Enabled = true;
                                }
                                else
                                {
                                    if (T_PRTGLOB.bctas_eliminadas != 0)
                                    {
                                        initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                        initObj.PrtEnt11._prttipob_[1].Enabled = true;
                                        initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                        initObj.PrtEnt11._prttipob_[2].Enabled = true;
                                    }
                                    else
                                    {
                                        initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                        initObj.PrtEnt11._prttipob_[1].Enabled = false;
                                        initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                        initObj.PrtEnt11._prttipob_[2].Enabled = false;
                                    }
                                }
                            }
                            else
                            {
                                initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                initObj.PrtEnt11._prttipob_[1].Enabled = true;
                                initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            }

                        }
                        else
                        {
                            if (T_PRTGLOB.bctas_eliminadas != 0)
                            {
                                initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                initObj.PrtEnt11._prttipob_[1].Enabled = true;
                                initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            }
                            else
                            {
                                initObj.PrtEnt11._prttipob_[1].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                                initObj.PrtEnt11._prttipob_[1].Enabled = false;
                                initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
                                initObj.PrtEnt11._prttipob_[2].Enabled = false;
                            }
                        }
                        break;
                }
            }
            else
            {
                initObj.PrtEnt11._prttipob_[1].Checked = false;
                initObj.PrtEnt11._prttipob_[1].Enabled = true;
                initObj.PrtEnt11._prttipob_[2].Checked = false;
                initObj.PrtEnt11._prttipob_[2].Enabled = true;
            }

            if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == 0 && (initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == 0)
            {
                initObj.PrtEnt11._prttipob_[2].Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                initObj.PrtEnt11._prttipob_[0].Enabled = false;
                initObj.PrtEnt11._prttipob_[1].Enabled = false;
                initObj.PrtEnt11._prttipob_[2].Enabled = true;
            }
            else
            {
                if ((T_PRTGLOB.blin_eliminadas & T_PRTGLOB.bctas_eliminadas) != 0)
                {
                    initObj.PrtEnt11._prttipob_[2].Checked = false;
                    initObj.PrtEnt11._prttipob_[2].Enabled = true;
                }
                else
                {
                    initObj.PrtEnt11._prttipob_[2].Checked = false;
                    initObj.PrtEnt11._prttipob_[2].Enabled = false;
                }
            }

            if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagAladi) != 0)
            {
                switch (initObj.PRTGLOB.Party.estado)
                {
                    case T_PRTGLOB.leido:
                    case T_PRTGLOB.modificado:
                        initObj.PrtEnt11.prtaladi.Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                        initObj.PrtEnt11.prtaladi.Checked = true;
                        initObj.PrtEnt11.prtplaza.Text = initObj.PRTGLOB.Party.aladi.ToStr();
                        break;

                    case T_PRTGLOB.nuevo:
                        initObj.PrtEnt11.prtaladi.Checked = System.Windows.Forms.CheckState.Checked.ToBool();
                        initObj.PrtEnt11.prtplaza.Text = initObj.PRTGLOB.Party.aladi.ToStr();
                        break;
                }
            }
            else
            {
                //PrtEnt11.prtaladi.CheckState = System.Windows.Forms.CheckState.Unchecked;
                initObj.PrtEnt11.prtaladi.Checked = false;
                initObj.PrtEnt11.prtplaza.Text = "";
            }
            //PrtEnt11.prttasa[0].Checked = PRTGLOB.Party.libor != 0;
            //PrtEnt11.prttasa[1].Checked = PRTGLOB.Party.prime != 0;
            initObj.PrtEnt11._prttasa_[0].Selected = initObj.PRTGLOB.Party.libor != 0;
            initObj.PrtEnt11._prttasa_[1].Selected = initObj.PRTGLOB.Party.prime != 0;

            if ((initObj.PRTGLOB.Party.prime | initObj.PRTGLOB.Party.libor) != 0)
                initObj.PrtEnt11._prttasa_[2].Selected = false;
            else
                initObj.PrtEnt11._prttasa_[2].Selected = true;

            if (initObj.PRTGLOB.Party.spread == 0)
                initObj.PrtEnt11.prtspread.Text = "";
            else
                initObj.PrtEnt11.prtspread.Text = Format.FormatCurrency(initObj.PRTGLOB.Party.spread, "###,##0.00"); //VB6Helpers.Format(initObj.PRTGLOB.Party.spread, "0.000000");


            initObj.PrtEnt11.Combo1.ListIndex = 2;
            initObj.PrtEnt11.Combo1.Enabled = false;
            initObj.PrtEnt11.aceptar.Enabled = false;

            //if (initObj.PRTGLOB.Party.Flag == T_PRTGLOB.nuevo) //Variables hay que setear bien
            //{
            //    habilitaDeshabilitaFrame2(initObj, false);
            //}
            //else
            //{
            //    habilitaDeshabilitaFrame2(initObj, true);
            //}

        }

        private static void habilitaDeshabilitaFrame2(InitializationObject initObj, bool estaHabilitado)
        {
            initObj.PrtEnt11._prttipob_[0].Enabled = estaHabilitado;
            initObj.PrtEnt11._prttipob_[1].Enabled = estaHabilitado;
            initObj.PrtEnt11._prttipob_[2].Enabled = estaHabilitado;
        }

        public static void lee_cuentasSy(InitializationObject initObj, string llave, UnitOfWorkCext01 unit)
        {
            int basura = 0;
            string basura0 = "";
            int k = 0;
            int estado = 0;
            int i = 0;
            int n = 0;
            string TitCtas = "";
            string R = "";
            string Que = "";
            int final = 0;

            final = -1;
            try
            {
                var resultado = new List<prtyccta>();
                var result = unit.SceRepository.sce_ctas_s04_MS(llave);
                foreach (var item in result)
                {
                    resultado.Add(new prtyccta
                    {
                        indice = item.secuencia.ToInt(),
                        estado = item.borrado.ToInt(),
                        extranjera = item.extranjera.ToInt(),
                        cuenta = item.cuenta,
                        activabco = item.activabco.ToInt(),
                        activace = item.activace.ToInt(),
                        moneda = item.moneda.ToString()
                    });
                }
                //unit.SceRepository.ReadQuerySP((reader) =>
                //{
                //    while (reader.Read())
                //    {
                //        prtyccta cta = new prtyccta();
                //        cta.indice = Convert.ToInt32(reader.GetDecimal(1));
                //        cta.cuenta = Convert.ToString(reader.GetString(7));
                //        cta.extranjera = Convert.ToInt32(reader.GetBoolean(5));

                //        estado = Convert.ToInt32(reader.GetBoolean(2));
                //        cta.est2 = estado;
                //        if (estado == 0)
                //        {
                //            cta.estado = T_PRTGLOB.leido;
                //            cta.activabco = Convert.ToInt32(reader.GetBoolean(3));
                //            cta.activace = Convert.ToInt32(reader.GetBoolean(4));
                //            cta.moneda = Convert.ToString(reader.GetDecimal(6));
                //        }
                //        resultado.Add(cta);
                //    }
                //}, "sce_ctas_s04_MS", llave);

                // Resultado nulo de la Consulta.-
                if (resultado.Count() == 0)
                {
                    //initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    //{
                    //    Text = " No se han encontrado datos en la Tabla Sce_Ctas.",
                    //    Title = T_PRTGLOB.TitCuentas,
                    //    Type = TipoMensaje.Informacion
                    //});
                    initObj.PRTGLOB.ctaclie = new prtyccta[0];
                    return;
                }

                initObj.PRTGLOB.ctaclie = new prtyccta[resultado.Count()];
                int z = 0;

                foreach (prtyccta cta in resultado)
                {
                    initObj.PRTGLOB.ctaclie[z] = new prtyccta();
                    if (cta.estado == 0)
                    {
                        initObj.PRTGLOB.ctaclie[z].indice = cta.indice;
                        initObj.PRTGLOB.ctaclie[z].activabco = cta.activabco;
                        initObj.PRTGLOB.ctaclie[z].activace = cta.activace;
                        initObj.PRTGLOB.ctaclie[z].extranjera = cta.extranjera;
                        initObj.PRTGLOB.ctaclie[z].moneda = cta.moneda;
                        initObj.PRTGLOB.ctaclie[z].cuenta = cta.cuenta;
                        initObj.PRTGLOB.ctaclie[z].estado = T_PRTGLOB.leido;
                        initObj.PRTGLOB.ctaclie[z].est2 = cta.estado;
                    }
                    else
                    {
                        initObj.PRTGLOB.ctaclie[z].indice = cta.indice;
                        initObj.PRTGLOB.ctaclie[z].cuenta = cta.cuenta;
                        initObj.PRTGLOB.ctaclie[z].extranjera = cta.extranjera;
                        initObj.PRTGLOB.ctaclie[z].est2 = cta.estado;
                        //initObj.PRTGLOB.ctaclie[z].moneda = string.Empty;
                    }
                    z++;
                }
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Se ha producido un error al tratar de leer la Tabla Sce_Ctas.",
                    Title = T_PRTGLOB.TitCuentas,
                    Type = TipoMensaje.Informacion
                });
                return;
            }
        }

        public static void lee_cuentasSy_Inicializar(ref InitializationObject initObj, XgpyService xS, String llave)
        {
            try
            {
                var listaCuentas = xS.Sce_Ctas_S04_MS(llave);
                if (listaCuentas != null) 
                {
                    initObj.PRTGLOB.ctaclie = new prtyccta[listaCuentas == null ? 0 : listaCuentas.Count()];
                    int z = 0;
                    foreach (var item in listaCuentas)
                    {
                        initObj.PRTGLOB.ctaclie[z] = new prtyccta();
                        if (!item.borrado)
                        {
                            initObj.PRTGLOB.ctaclie[z].indice = item.secuencia.ToInt();
                            initObj.PRTGLOB.ctaclie[z].activabco = item.activabco.ToInt();
                            initObj.PRTGLOB.ctaclie[z].activace = item.activace.ToInt();
                            initObj.PRTGLOB.ctaclie[z].extranjera = item.extranjera.ToInt();
                            initObj.PRTGLOB.ctaclie[z].moneda = item.moneda.ToString();
                            initObj.PRTGLOB.ctaclie[z].cuenta = item.cuenta;
                            initObj.PRTGLOB.ctaclie[z].estado = T_PRTGLOB.leido;
                            initObj.PRTGLOB.ctaclie[z].est2 = item.borrado ? 0 : 1;
                        }
                        else
                        {
                            initObj.PRTGLOB.ctaclie[z].indice = item.secuencia.ToInt();
                            initObj.PRTGLOB.ctaclie[z].cuenta = item.cuenta;
                            initObj.PRTGLOB.ctaclie[z].extranjera = item.extranjera.ToInt();
                            initObj.PRTGLOB.ctaclie[z].est2 = item.borrado ? 0 : 1;
                        }
                        z++;
                    }
                }
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Se ha producido un error al tratar de leer la Tabla Sce_Ctas.",
                    Title = T_PRTGLOB.TitCuentas,
                    Type = TipoMensaje.Informacion
                });
                return;
            }
        }

        public static string cambiasepdec(string obj)
        {
            string cambiasepdec = "";
            int a = 0;
            string s = "";
            a = VB6Helpers.Instr(obj, ",");  // obj.InStr(",", 1, StringComparison.CurrentCulture);
            if (a != 0)
            {
                s = VB6Helpers.Left(obj, (a - 1)) + ".";//obj.Left((a - 1)) + ".";
                s = s + VB6Helpers.Right(obj, (obj.Length - a)); //obj.Right((obj.Len() - a));
                cambiasepdec = s;
            }
            else
                cambiasepdec = obj;
            
            return cambiasepdec;
        }

        public static string saca(string c, string s)
        {
            string saca = "";
            int a = 0;
            a = VB6Helpers.Instr(1, c, s); //c.InStr(s, 1, StringComparison.CurrentCulture);
            if (a != 0)
            {
                saca = VB6Helpers.Left(c, (a - 1)) + VB6Helpers.Right(c, (c.Length - a)); //c.Left((a - 1)) + c.Right((c.Len() - a));
            }
            else
            {
                saca = c;
            }
            return saca;
        }

        public static string QuitarMascaraCuenta(string cuenta)
        {
            try
            {
                string nroCuenta = cuenta;
                if (nroCuenta != "")
                    return nroCuenta.Replace("-", "");
                else
                    return "";
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        public static void carga_riesgo(InitializationObject initObj, BCH.Comex.Common.UI_Modulos.UI_Combo lista, UnitOfWorkCext01 unit)
        {
            int i = 0;
            int fin = 0;
            lista.Clear();
            fin = -1;
            fin = initObj.PRTGLOB.riesgo.GetUpperBound(0);
            for (i = 0; i <= fin; i += 1)
            {
                lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    Data = i,
                    ID = initObj.PRTGLOB.riesgo[i].codigo,
                    Value = initObj.PRTGLOB.riesgo[i].nombre

                });
            }
        }

        public static void llena_moneda(InitializationObject initObject, BCH.Comex.Common.UI_Modulos.UI_Combo lista)
        {
            int i = 0;
            for (i = 0; i <= initObject.PRTGLOB.cod_nom_moneda.Length - 1; i += 1)
            {
                lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    Data = Convert.ToInt32(initObject.PRTGLOB.cod_nom_moneda[i].CodMoneda),
                    Value = initObject.PRTGLOB.cod_nom_moneda[i].NombMoneda
                });
            }
        }

        public static void cargaoficinasSy(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int a = 0;
            int i = 0;
            int n = 0;
            int cod = 0;
            string Que = "";
            try
            {
                IList<sgt_suc_s01_MS_Result> R;
                R = uow.SceRepository.sgt_suc_s01_MS().ToList();
                if (R == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = T_PRTGLOB.TitParty,
                        Text = " No existen registros en tabla de Oficinas.",
                        Type = TipoMensaje.Informacion
                    });

                    return;
                }
                for (i = 0; i < R.Count(); i += 1)
                {
                    Array.Resize(ref initObj.PRTGLOB.oficinas, i + 1);
                    initObj.PRTGLOB.oficinas[i].codigo = R[i].suc_succod.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R).ToInt();
                    initObj.PRTGLOB.oficinas[i].nombre = R[i].suc_sucnom;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                    if (initObj.PRTGLOB.oficinas[i].nombre.UCase() == "CENTRAL")
                        a = 1;
                }
                return;
            }
            catch
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.TitParty,
                    Text = " Error al leer tabla de oficinas.",
                    Type = TipoMensaje.Informacion
                });
            }
        }

        public static int quitaceros(string codigo)
        {
            int largo = codigo.ToString().Length - 1;
            int quitaceros = 0;
            int i = 0;
            for (i = 1; i <= largo; i += 1)
            {
                if (codigo.ToString().Substring(i, 1) != "0")
                {
                    break;
                }
            }
            switch (i)
            {
                case 1:
                    quitaceros = Convert.ToInt32(codigo);
                    break;
                case 2:
                    quitaceros = Convert.ToInt32(codigo.ToString().Substring(codigo.ToString().Length, 2));
                    break;
                case 3:
                    quitaceros = Convert.ToInt32(codigo.ToString().Substring(codigo.ToString().Length, 1));
                    break;
                case 4:
                    quitaceros = 0;
                    break;
            }
            return quitaceros;
        }

        public static string nom_act(InitializationObject initObj, double cod)
        {
            string nom_act = "";
            int i = 0;
            //if (cod == 0)
            //{
            //    return nom_act;
            //}

            for (i = 0; i <= initObj.PRTGLOB.acteco.GetUpperBound(0); i += 1)
            {
                if (initObj.PRTGLOB.acteco[i].codigo == cod)
                {
                    nom_act = initObj.PRTGLOB.acteco[i].nombre;
                    break;
                }
            }

            return nom_act;
        }

        public static string nom_ofi(InitializationObject initObj, string cod)
        {
            string nom_ofi = "";
            int i = 0;
            int result;

            //if (cod == "" || cod == null)
            if (string.IsNullOrEmpty(cod.Trim().ToStr()))
            {
                return nom_ofi;
            }
            for (i = 0; i <= initObj.PRTGLOB.oficinas.GetUpperBound(0); i += 1)
            {
                //INICIO MODIFICACION CNC - ACCENTURE
                
                if(int.TryParse(cod, out result))
                {
                    if (initObj.PRTGLOB.oficinas[i].codigo == Convert.ToInt32(cod))
                    {
                        nom_ofi = initObj.PRTGLOB.oficinas[i].nombre;
                        break;
                    }
                }
            }

            return nom_ofi;
        }

        public static int Siguiente(InitializationObject initObj, string Tabla)
        {
            int Siguiente = 0;
            bool incremento = false;
            string l1 = "";
            string l2 = "";
            int j = 0;
            int encontrado = 0;
            int listo = 0;
            int fin = 0;
            int i = 0;
            l1 = initObj.PrtEnt08.Lista1.Items[0].Value;
            l2 = initObj.PrtEnt08.Lista1.Items[0].Value;
            j = 0;
            encontrado = ((false) ? -1 : 0);
            listo = ((false) ? -1 : 0);

            switch (Tabla)
            {
                case "ctaclie":
                    fin = -1;
                    // OnErrorResumeNext();
                    fin = initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0);
                    if (fin != -1)
                    {
                        if (fin == 0 && initObj.PRTGLOB.ctaclie_aux[0].cuenta == "")
                        {
                            Siguiente = 0;
                            return Siguiente;
                        }
                        else
                        {
                            j = 0;
                            T_PRTGLOB.sig = initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0) + 1;
                            while (j <= initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0))
                            {
                                for (i = 0; i <= initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0); i += 1)
                                {
                                    incremento = false;
                                    if (initObj.PRTGLOB.ctaclie_aux[i].indice == j)
                                    {
                                        j = j + 1;
                                        incremento = true;
                                        break;
                                    }
                                }
                                if (!incremento)
                                    break;
                            }

                            if (j > initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0))
                                Siguiente = T_PRTGLOB.sig;
                            
                            else
                                Siguiente = j;
                            
                            Array.Resize(ref initObj.PRTGLOB.ctaclie_aux, T_PRTGLOB.sig + 1);
                        }
                    }
                    else
                    {
                        initObj.PRTGLOB.ctaclie_aux = new cuenta_indice[1];
                        Siguiente = 0;
                    }
                    break;

                case "ctabancos":
                    fin = -1;
                    // OnErrorResumeNext();
                    fin = initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0);
                    if (fin != -1)
                    {
                        if (fin == 0 && initObj.PRTGLOB.ctaclie_aux[0].cuenta == "")
                        {
                            Siguiente = 0;
                            return Siguiente;
                        }
                        else
                        {
                            j = 0;
                            T_PRTGLOB.sig = initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0) + 1;
                            while (j <= initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0))
                            {
                                for (i = 0; i <= initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0); i += 1)
                                {
                                    incremento = false;
                                    if (initObj.PRTGLOB.ctaclie_aux[i].indice == j)
                                    {
                                        j = j + 1;
                                        incremento = true;
                                        break;
                                    }
                                }
                                if (!incremento)
                                    break;
                                
                            }
                            if (j > initObj.PRTGLOB.ctaclie_aux.GetUpperBound(0))
                                Siguiente = T_PRTGLOB.sig;
                            
                            else
                                Siguiente = j;
                            
                            Array.Resize(ref initObj.PRTGLOB.ctaclie_aux, T_PRTGLOB.sig + 1);
                        }
                    }
                    else
                    {
                        initObj.PRTGLOB.ctaclie_aux = new cuenta_indice[1];
                        Siguiente = 0;
                    }
                    break;

                case "linbancos":
                    fin = -1;
                    // OnErrorResumeNext();
                    fin = initObj.PRTGLOB.linbancos_aux.GetUpperBound(0);
                    if (fin != -1)
                    {
                        if (fin == 0 && initObj.PRTGLOB.linbancos_aux[0].cuenta == "")
                        {
                            Siguiente = 0;
                            return Siguiente;
                        }
                        else
                        {
                            j = 0;
                            T_PRTGLOB.sig = initObj.PRTGLOB.linbancos_aux.GetUpperBound(0) + 1;
                            while (j <= initObj.PRTGLOB.linbancos_aux.GetUpperBound(0))
                            {
                                for (i = 0; i <= initObj.PRTGLOB.linbancos_aux.GetUpperBound(0); i += 1)
                                {
                                    incremento = false;
                                    if (initObj.PRTGLOB.linbancos_aux[i].indice == j)
                                    {
                                        j = j + 1;
                                        incremento = true;
                                        break;
                                    }
                                }
                                if (!incremento)
                                    break;
                                
                            }
                            if (j > initObj.PRTGLOB.linbancos_aux.GetUpperBound(0))
                            {
                                Siguiente = T_PRTGLOB.sig;
                            }
                            else
                                Siguiente = j;
                            
                            Array.Resize(ref initObj.PRTGLOB.linbancos_aux, T_PRTGLOB.sig + 1);
                        }
                    }
                    else
                    {
                        initObj.PRTGLOB.linbancos_aux = new cuenta_indice[1];
                        Siguiente = 0;
                    }
                    break;
            }

            return Siguiente;
        }

        public static void carga_ejecutivos(InitializationObject initObj, BCH.Comex.Common.UI_Modulos.UI_Combo lista)
        {
            string s = "";
            int i = 0;
            int fin = 0;
            lista.Clear();
            fin = -1;
            fin = initObj.PRTGLOB.ejecutivos.GetUpperBound(0);

            for (i = 0; i <= fin; i += 1)
            {
                s = initObj.PRTGLOB.ejecutivos[i].nombre; //VB6Helpers.Format(initObj.PRTGLOB.ejecutivos[i].codigo, "@@@") + " - " + initObj.PRTGLOB.ejecutivos[i].nombre;
                lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    Data = initObj.PRTGLOB.ejecutivos[i].codigo.ToInt(),
                    ID = initObj.PRTGLOB.ejecutivos[i].codigo, //i.ToString(),
                    Value = s
                });
            }
        }

        public static void carga_oficinaCodNom(InitializationObject initObj, BCH.Comex.Common.UI_Modulos.UI_Combo lista)
        {
            lista.Clear();
            for (int i = 0; i < initObj.PRTGLOB.oficinas.Length; i++)
            {
                var oficina = initObj.PRTGLOB.oficinas[i];
                // Se cargan los códigos y los nombres de las oficinas en el dropdown list.
                lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    Data = oficina.codigo.ToInt(),
                    ID = oficina.codigo.ToString(), 
                    Value = oficina.codigo.ToString().PadLeft(3, '0') + " - " + oficina.nombre
                });
            }

            // Si el Party tiene oficina seleccionarla en el dropdown list.
            if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.oficina.Trim()))
                lista.SelectedValue = Convert.ToInt32(initObj.PRTGLOB.Party.oficina.Trim());
            
        }

        public static void lee_actecoSy(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            int k = 0;
            int i = 0;
            int n = 0;
            string TitActeco = "";
            string R = "";
            string Que = "";
            int fin = 0;
            try
            {
                fin = -1;
                fin = initObj.PRTGLOB.acteco.GetUpperBound(0);

                if (fin != -1 && initObj.PRTGLOB.acteco[0].nombre != "")
                    return;

                var result = unit.SceRepository.Sgt_aec_s01_MS();
                // Se realiza la consulta pero no retorna datos

                if (result.Count == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se han encontrado datos en la Tabla de Actividad Economica" });
                    return;
                }

                n = result.Count;

                for (i = 0; i <= result.Count - 1; i += 1)
                {
                    fin = -1;
                    fin = initObj.PRTGLOB.acteco.GetUpperBound(0);

                    if (fin != -1)
                    {
                        if (initObj.PRTGLOB.acteco[0].nombre == "")
                            k = 0;
                        else
                        {
                            k = fin + 1;
                            Array.Resize(ref initObj.PRTGLOB.acteco, k + 1);
                        }
                    }
                    else
                    {
                        k = 0;
                        initObj.PRTGLOB.acteco = new tipo_acteco[k + 1];
                    }

                    initObj.PRTGLOB.acteco[k].codigo = (double)result[i].aec_aeccod;
                    initObj.PRTGLOB.acteco[k].nombre = result[i].aec_aecnom;
                }
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Se ha producido un error al tratar de leer la Lista de Actividad Ecnonomica." });
                return;
            }
        }

        //public static string descero(string Que, string Mascara)
        public static string descero(string rut)
        {
            int cont = 0;
            String format;

            if (rut.Length == 0)
                return "";
            else
            {
                rut = rut.TrimStart('0');
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);

                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;

                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }          
        }

        public static void escribeinfoparty(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int j = 0;
            int fin = 0;
            T_PRTGLOB PRTGLOB = initObj.PRTGLOB;
            string lis = "";
            int i = 0;
            //if (initObj.PRTGLOB.Party.oficina != null)

            if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.oficina.ToString().Trim()))
            {
                initObj.PrtEnt07.cboOficina.Enabled = initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0;
               
                lee_ejecutivosSy(initObj, uow, initObj.PRTGLOB.Party.oficina);
                carga_ejecutivos(initObj, initObj.PrtEnt07.ejecutivo);
                //if (initObj.PRTGLOB.Party.ejecutivo.Trim() != "" || initObj.PRTGLOB.Party.ejecutivo.Trim() != null)

                if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.ejecutivo.ToString().Trim() == null ? string.Empty : initObj.PRTGLOB.Party.ejecutivo.ToString().Trim()))
                {
                    for (j = 0; j <= initObj.PrtEnt07.ejecutivo.Items.Count - 1; j += 1)
                    {
                        if (initObj.PrtEnt07.ejecutivo.get_ItemData_(j).ToDbl() == initObj.PRTGLOB.Party.ejecutivo.ToVal())
                        {
                            initObj.PrtEnt07.ejecutivo.ListIndex = j;
                            break;
                        }
                    }

                    initObj.PrtEnt07.ejecutivo.Enabled = initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0;
                }
                else
                {
                    initObj.PrtEnt07.ejecutivo.ListIndex = -1;
                    initObj.PrtEnt07.ejecutivo.Enabled = true;
                }
            }
            else
            {
                initObj.PrtEnt07.cboOficina.ListIndex = -1;
                initObj.PrtEnt07.cboOficina.Enabled = true;
                initObj.PrtEnt07.ejecutivo.ListIndex = -1;
                initObj.PrtEnt07.ejecutivo.Enabled = true;
            }

            PRTYENT.lee_actecoSy(initObj, uow); //Se cambio de PrtEnt07 formLoad aqui 12-03-2016
            initObj.PrtEnt07.Combo2.Items.Clear();
            fin = PRTGLOB.acteco.Count();

            for (i = 0; i < fin; i += 1)
            {
                lis = PRTGLOB.acteco[i].nombre; 
                initObj.PrtEnt07.Combo2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    ID = PRTGLOB.acteco[i].codigo.ToString(),
                    Data = i,
                    Value = lis
                });
            }

            PRTYENT.lee_riesgoSy(initObj, uow);
            PRTYENT.carga_riesgo(initObj, initObj.PrtEnt07.Combo4, uow);

            if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.actividad.Trim().ToString()))
            {
                for (j = 0; j <= initObj.PrtEnt07.Combo2.Items.Count - 1; j += 1)
                {
                    if (VB6Helpers.Format(initObj.PRTGLOB.acteco[initObj.PrtEnt07.Combo2.get_ItemData_(j).ToInt()].codigo, String.Empty) == initObj.PRTGLOB.Party.actividad)
                    {
                        initObj.PrtEnt07.Combo2.ListIndex = j;
                        break;
                    }
                }

                initObj.PrtEnt07.Combo2.Enabled = initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0;
            }
            else
            {
                initObj.PrtEnt07.Combo2.ListIndex = -1;
                initObj.PrtEnt07.Combo2.Enabled = true;
            }

            //if (initObj.PRTGLOB.Party.riesgo != "")
            if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.riesgo.Trim().ToStr()))
            {
                for (j = 0; j <= initObj.PrtEnt07.Combo4.Items.Count - 1; j += 1)
                {
                    if (initObj.PRTGLOB.riesgo[initObj.PrtEnt07.Combo4.get_ItemData_(j).ToInt()].codigo == initObj.PRTGLOB.Party.riesgo)
                    {
                        initObj.PrtEnt07.Combo4.ListIndex = j;
                        break;
                    }
                }

                initObj.PrtEnt07.Combo4.Enabled = initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0;
            }
            else
            {
                initObj.PrtEnt07.Combo4.ListIndex = -1;
                initObj.PrtEnt07.Combo4.Enabled = true;
            }

            for (j = 0; j <= initObj.PrtEnt07.Combo1.Items.Count - 1; j += 1)
            {
                if (initObj.PrtEnt07.Combo1.get_ItemData_(j).ToInt() == initObj.PRTGLOB.Party.clasificacion)
                {
                    initObj.PrtEnt07.Combo1.ListIndex = j;
                    initObj.PrtEnt07.Combo1.Enabled = true;
                    break;
                }
            }
            initObj.PrtEnt07.aceptar.Enabled = false;
        }

        public static void lee_tcomSy(InitializationObject InitObj, UnitOfWorkCext01 unit, string llave)
        {
            int basura2 = 0;
            int k = 0;
            int estado = 0;
            int i = 0;
            int n = 0;
            int final = 0;

            try
            {
                final = -1;
                IList<sce_tcom_s04_MS_Result> R;
                final = InitObj.PRTGLOB.tasacom.GetUpperBound(0);

                if (final != -1 && InitObj.PRTGLOB.tasacom[0].sistema != "")
                    return;
                    
                R = unit.SceRepository.Sce_Tcom_S04_MS(llave).ToList();
                n = R.Count();
                
                if (n > 0)
                    T_PRTYENT.offsec = R[0].secuencia.ToInt();
                else
                    T_PRTYENT.offsec = 1;

                for (i = 0; i <= n - 1; i += 1)
                {
                    estado = R[i].borrado.ToInt();

                    if (estado == 0)
                    {
                        final = -1;
                        final = InitObj.PRTGLOB.tasacom.GetUpperBound(0);
                        if (final != -1)
                        {
                            if (final == 0 && InitObj.PRTGLOB.tasacom[0].sistema == "")
                            {
                                k = 0;
                                InitObj.PRTGLOB.tasacom = new prtytcom[1];
                            }
                            else
                            {
                                k = final + 1;
                                Array.Resize(ref InitObj.PRTGLOB.tasacom, k + 1);
                            }
                        }
                        else
                        {
                            k = 0;
                            InitObj.PRTGLOB.tasacom = new prtytcom[1];
                        }

                        InitObj.PRTGLOB.tasacom[k].sistema = R[k].sistema;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                        InitObj.PRTGLOB.tasacom[k].producto = R[k].producto; //MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                        InitObj.PRTGLOB.tasacom[k].etapa = R[k].etapa;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                        InitObj.PRTGLOB.tasacom[k].estado = T_PRTGLOB.leido;
                        InitObj.PRTGLOB.tasacom[k].secuencia = R[k].secuencia.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                        basura2 = R[k].borrado.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                        InitObj.PRTGLOB.tasacom[k].manual = R[k].manual_t.ToInt();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                        InitObj.PRTGLOB.tasacom[k].mto_fijo = R[k].monto_fijo.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                        InitObj.PRTGLOB.tasacom[k].tasa = R[k].tasa.ToDbl();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R);
                        InitObj.PRTGLOB.tasacom[k].hasta = R[k].hasta_mon.ToDbl(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R);
                        InitObj.PRTGLOB.tasacom[k].min = R[k].minimo.ToDbl();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R);
                        InitObj.PRTGLOB.tasacom[k].max = R[k].maximo.ToDbl();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R);
                        InitObj.PRTGLOB.tasacom[k].fecha = R[k].fecha.ToShortDateString(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "f", R).ToStr();
                    }
                    else
                    {
                        T_PRTYENT.offsec = R[k].secuencia.ToInt();//(MODGSYB.GetPosSy(5, "N", R) + 1).ToInt();
                    }
                }
            }
            catch
            {
                InitObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Title = T_PRTGLOB.Master_Titulo, Type = TipoMensaje.Error, Text = "Se ha producido un error al tratar de leer la Tabla Sce_Tcom" });
                return;
            }
        }

        public static void lee_tintSy(InitializationObject InitObj, UnitOfWorkCext01 unit, string llave)
        {
            int basura2 = 0;
            // string basura = "";
            int k = 0;
            int estado = 0;
            int i = 0;
            int n = 0;
            int final = 0;

            try
            {
                final = -1;
                final = InitObj.PRTGLOB.tasaint.GetUpperBound(0);

                if (final != -1)
                {
                    if (InitObj.PRTGLOB.tasaint[0].sistema != "" || InitObj.PRTGLOB.tasaint[0].sistema != null)
                        return;
                    

                    IList<sce_tint_s01_MS_Result> R;

                    R = unit.SceRepository.Sce_Tint_S01_MS(llave).ToList();
                    // no trae datos en la consulta
                    //if (R == null)
                    //{
                    //    //MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer la Tabla Sce_Tint", UTILES.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), TitTint);
                    //    //goto Lee_tintSyErr;
                    //    InitObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Title = T_PRTGLOB.Master_Titulo, Type = TipoMensaje.Error, Text = "No existe registros en la tabla Sce_Tint" });
                    //    return;
                    //}
                    n = R.Count();

                    for (i = 0; i <= n - 1; i += 1)
                    {
                        estado = R[0].borrado.ToInt(); //MODGSYB.GetPosSy(5, "N", R).ToInt();

                        if (estado == 0)
                        {
                            final = -1;
                            final = InitObj.PRTGLOB.tasaint.GetUpperBound(0);

                            if (final == 0 && (InitObj.PRTGLOB.tasaint[0].sistema == "" || InitObj.PRTGLOB.tasaint[0].sistema == null))
                                k = 0;
                            else
                                k = final + 1;

                            if (k == 0)
                                InitObj.PRTGLOB.tasaint = new prtytint[1];
                            else
                                Array.Resize(ref InitObj.PRTGLOB.tasaint, k + 1);

                            //basura = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).ToStr();
                            InitObj.PRTGLOB.tasaint[k].sistema = R[k].sistema; //MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                            InitObj.PRTGLOB.tasaint[k].producto = R[k].producto;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                            InitObj.PRTGLOB.tasaint[k].etapa = R[k].etapa;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                            basura2 = R[k].borrado.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                            InitObj.PRTGLOB.tasaint[k].libor = R[k].libor.ToInt();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                            InitObj.PRTGLOB.tasaint[k].prime = R[k].prime.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                            InitObj.PRTGLOB.tasaint[k].flotante = R[k].flotante.ToInt();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                            InitObj.PRTGLOB.tasaint[k].tasa = R[k].tasa.ToDbl();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R);
                            InitObj.PRTGLOB.tasaint[k].estado = T_PRTGLOB.leido;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                InitObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Title = T_PRTGLOB.Master_Titulo, Type = TipoMensaje.Error, Text = " Se ha producido un error al tratar de leer la Tabla Sce_Tint." });
                return;
            }
        }

        public static void lee_tgasSy(InitializationObject InitObj, UnitOfWorkCext01 unit, string llave)
        {
            int basura2 = 0;
            string basura = "";
            int k = 0;
            int estado = 0;
            int i = 0;
            int n = 0;
            int final = 0;

            try
            {
                final = -1;
                final = InitObj.PRTGLOB.tasagas.GetUpperBound(0);

                if (final != -1)
                {
                    if (InitObj.PRTGLOB.tasagas[0].sistema != "" || InitObj.PRTGLOB.tasagas[0].sistema != null)
                        return;
                    
                }

                IList<sce_tgas_s04_MS_Result> R;
                R = unit.SceRepository.Sce_Tgas_S04_MS(llave).ToList();
                n = R.Count();

                for (i = 0; i <= n - 1; i += 1)
                {
                    estado = R[i].borrado.ToInt(); // MODGSYB.GetPosSy(5, "N", R).ToInt();

                    if (estado == 0)
                    {
                        final = -1;
                        final = InitObj.PRTGLOB.tasagas.GetUpperBound(0);

                        if (final == 0 && (InitObj.PRTGLOB.tasagas[0].sistema == "" || InitObj.PRTGLOB.tasagas[0].sistema == null))
                            k = 0;
                        else
                            k = final + 1;

                        if (k == 0)
                            InitObj.PRTGLOB.tasagas = new prtytgas[1];
                        else
                            Array.Resize(ref InitObj.PRTGLOB.tasagas, k + 1);

                        basura = R[k].borrado.ToStr(); //MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).ToStr();
                        InitObj.PRTGLOB.tasagas[k].sistema = R[k].sistema;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                        InitObj.PRTGLOB.tasagas[k].producto = R[k].producto;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                        InitObj.PRTGLOB.tasagas[k].etapa = R[k].etapa;//MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr();
                        basura2 = R[k].borrado.ToInt();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                        InitObj.PRTGLOB.tasagas[k].tarifa = R[k].m_tarifa.ToInt(); //MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                        InitObj.PRTGLOB.tasagas[k].monto = R[k].monto.ToDbl();//MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R);                     
                        InitObj.PRTGLOB.tasagas[k].estado = T_PRTGLOB.leido;
                    }
                }
            }
            catch (Exception e)
            {
                InitObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Title = T_PRTGLOB.Master_Titulo, Type = TipoMensaje.Error, Text = "Se ha producido un error al tratar de leer la Tabla Sce_Tgas" });
                return;
            }
        }

        public static void lee_tgasSy_Inicializar(ref InitializationObject initObj, XgpyService xS, String llave) 
        {
            try
            {
                var listaTasas = xS.Sce_Tgas_S04_MS(llave).ToList();

                if (listaTasas != null) 
                {
                    var listaTasasActivo = listaTasas.Where(x => x.borrado == false); //En el legacy solo se cogen los activos.
                    initObj.PRTGLOB.tasagas = new prtytgas[listaTasasActivo == null ? 0 : listaTasasActivo.Count()];

                    if (listaTasasActivo != null) 
                    {
                        for (int i = 0; i < listaTasasActivo.Count(); i++)
                        {
                            initObj.PRTGLOB.tasagas[i] = new prtytgas();
                            initObj.PRTGLOB.tasagas[i].sistema = listaTasas[i].sistema;
                            initObj.PRTGLOB.tasagas[i].producto = listaTasas[i].producto;
                            initObj.PRTGLOB.tasagas[i].etapa = listaTasas[i].etapa;
                            initObj.PRTGLOB.tasagas[i].tarifa = listaTasas[i].m_tarifa.ToInt();
                            initObj.PRTGLOB.tasagas[i].monto = listaTasas[i].monto.ToDbl();
                            initObj.PRTGLOB.tasagas[i].estado = T_PRTGLOB.leido;
                            initObj.PRTGLOB.tasagas[i].borrado = listaTasas[i].borrado.ToInt();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Title = T_PRTGLOB.Master_Titulo, Type = TipoMensaje.Error, Text = "Se ha producido un error al tratar de leer la Tabla Sce_Tgas" });
                return;
            }
        }

        public static void habilita(InitializationObject initObj)
        {
            // Caracteristicas
            initObj.Mdi_Principal.Opciones[0].Enabled = true;
            initObj.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled = true;

            // instrucciones
            initObj.Mdi_Principal.Opciones[1].Enabled = true;     // menu           
            initObj.Mdi_Principal.BUTTONS["tbr_Instrucciones"].Enabled = true;  // boton

            // cuentas
            initObj.Mdi_Principal.Opciones[2].Enabled = true;     // menu
            initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = true;   // boton

            // tasas
            initObj.Mdi_Principal.Opciones[3].Enabled = true;     // menu
            initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = true;     // boton


            // salvar participante          
            initObj.Mdi_Principal.Archivo[2].Enabled = true;     // menu
            initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;      // boton

            // habilitar el menu de opciones           
            //for (var i = 0; i < initObj.Mdi_Principal.Opciones.Count; i++)
            //{
            //    initObj.Mdi_Principal.Opciones[i].Enabled = true;
            //}

            // opciones de menu archivo
            initObj.Mdi_Principal.Archivo[3].Enabled = initObj.PRTGLOB.PrtControl.Red != 0; ;     // recuperar
            initObj.Mdi_Principal.Archivo[4].Enabled = false;     // Eliminar       
        }

        public static void deshabilita(InitializationObject initObj)
        {
            // Caracteristicas          
            initObj.Mdi_Principal.Opciones[0].Enabled = false;
            initObj.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled = false;
            // instrucciones
            initObj.Mdi_Principal.Opciones[1].Enabled = false;     // menu           
            initObj.Mdi_Principal.BUTTONS["tbr_Instrucciones"].Enabled = false;  // boton
            //// cuentas
            initObj.Mdi_Principal.Opciones[2].Enabled = false;     // menu
            initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = false;   // boton

            //// tasas
            initObj.Mdi_Principal.Opciones[3].Enabled = false;     // menu
            initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;     // boton

            //// Activar Razon
            initObj.Mdi_Principal.Opciones[4].Enabled = false;     // menu --Realsystems 03-09-2008 Migración VB60 a VB30
            initObj.Mdi_Principal.BUTTONS["tbr_Activar"].Enabled = false;   // boton--Realsystems 03-09-2008 Migración VB60 a VB30

            //// salvar
            initObj.Mdi_Principal.Archivo[2].Enabled = false;     // menu
            initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;      // boton

            //// menu opciones
            //for (var i = 0; i < initObj.Mdi_Principal.Opciones.Count; i++)
            //{
            //    initObj.Mdi_Principal.Opciones[i].Enabled = false;
            //}

            //// opciones menu archivo
            initObj.Mdi_Principal.Archivo[3].Enabled = false;     // recuperar
            initObj.Mdi_Principal.Archivo[4].Enabled = false;     // Eliminar

            //// setea titulo
            //PrtEnt01.Text = PRTGLOB.TitParty;
        }

        public static int esrut(string rut)
        {
            int esrut = 0;
            string DvCal = "";
            const string Son = "1234567890K";
            string a = "";
            int i = 0;
            string b = "";
            string dvrut = "";
            int aa = 0;
            int suma = 0;
            int es = 0;

            // limpiar el Rut
            for (i = 1; i <= rut.Len(); i += 1)
            {
                a = rut.Mid(i, 1);
                if (a == "k")
                    a = "K";
                
                if (Son.InStr(a, 1, StringComparison.CurrentCulture) > 0)
                    b = b + a;
            }

            dvrut = b.Right(1);
            b = b.Left((b.Len() - 1));

            for (i = 1; i <= b.Len(); i += 1)
            {
                a = b.Right(i);
                aa = (a.Left(1)).ToInt();
                suma = i < 7 ? suma + aa * (i + 1) : suma + aa * (i - 5);
            }

            es = 11 - suma % 11;

            switch (es)
            {
                case 11:
                    DvCal = "0";
                    break;
                case 10:
                    DvCal = "K";
                    break;
                default:
                    DvCal = VB6Helpers.Format(es, String.Empty);
                    break;
            }

            esrut = 0;

            if (DvCal == dvrut)
                esrut = -1;
            
            return esrut;
        }

        //public static string filcero(string Que, string Mascara)
        //{
        //    string filcero = "";

        //    string NewMask = "";

        //    int i = 0;
        //    string a = "";
        //    int mas = 0;
        //    string Sal = "";

        //    for (i = 1; i <= Mascara.Len(); i += 1)
        //    {
        //        a = Mascara.Mid(i, 1);
        //        if (a != "_")
        //        {
        //            NewMask = NewMask + a;
        //        }
        //        else
        //        {
        //            mas = mas + 1;
        //        }
        //    }

        //    for (i = 1; i <= Que.Len(); i += 1)
        //    {
        //        a = Que.Mid(i, 1);
        //        if (NewMask.InStr(a, 1, StringComparison.CurrentCulture) == 0)
        //        {
        //            Sal = Sal + a;
        //        }
        //    }

        //    filcero = new string(48.ToChar(), mas - Sal.Len()) + Sal;

        //    return filcero;
        //}

        public static int existesy(ref InitializationObject iO, XgpyService xS, String llavesy, Int32 modo)
        {
            int retExisteSy = 0;
            IList<String> R;

            try
            {

                if (iO.PRTGLOB.Party.PrtGlob.EsCITI.ToBool())
                    iO.PRTGLOB.Party.Bnumber = iO.PRTGLOB.Party.PrtGlob.llave.UCase().PadRight(12, '|');
                
                else
                    iO.PRTGLOB.Party.PrtGlob.llave = llavesy.UCase().PadRight(12, '|');
                

                if (iO.PRTGLOB.Party.PrtGlob.EsCITI.ToBool())
                    R = xS.Sce_Prty_S07_MS(iO.PRTGLOB.Party.Bnumber).ToList();
                
                else
                    R = xS.Sce_Prty_S07_MS(iO.PRTGLOB.Party.PrtGlob.llave).ToList();
                
                if (R != null)
                {
                    retExisteSy = true.ToInt();
                    if (modo != 0)
                    {
                    }
                }
                else
                    retExisteSy = false.ToInt();     // Participante no existe
                
            }
            catch { }

            return retExisteSy;
        }

        public static bool ExisteBIC(InitializationObject initObj, UnitOfWorkCext01 uow, string llave)
        {
            bool ExisteBIC = false;
            IList<string> R;

            try
            {
                R = uow.SceRepository.Sce_Bic_S07_MS(llave).ToList();
                // Se realiza la consulta pero no retorna datos
                ExisteBIC = R != null; // Falso = Participante no existe
            }
            catch
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.TitIngreso,
                    Text = " Error en lectura de participantes en la base de datos.",
                    Type = TipoMensaje.Informacion

                });
                return ExisteBIC;
            }

            return ExisteBIC;
        }

        public static bool CreaDesdeBIC(InitializationObject initObj, string llavebic)
        {
            bool CreaDesdeBIC = false;
            string R = "";
            // 07/03/2001 Este programa chequea que el participante se encuentre en la base BIC
            string Que = "";
            CreaDesdeBIC = false;
            T_PRTGLOB.llave = llavebic.UCase() + new string(126.ToChar(), 12 - llavebic.Len());

            try
            {

                Que = "";
                Que = Que + "Exec " + "." + "." + "sce_prty_i01 ";
                Que = Que.LCase();
                Que = Que + MODGSYB.dbcharSy(llavebic) + ", ";
                Que = Que + MODGSYB.dbcharSy(T_PRTGLOB.llave) + ", ";
                Que = Que + MODGSYB.dbcharSy(MODGUSR.UsrEsp.CentroCosto) + ", ";
                Que = Que + MODGSYB.dbcharSy(MODGUSR.UsrEsp.Especialista) + ", ";
                Que = Que + MODGSYB.dbdatetimeSy(DateTime.Now.ToStr());

                // 
                // 
                // Se ejecuta el Procedimiento Almacenado.
                R = "";//MODGSRM.RespuestaQuery(ref Que);

                if (MODGSRM.HayErr_Com(R) != 0)
                {
                    //MigrationSupport.Utils.MsgBox("Se ha producido un error de Comunicación al tratar de Grabar Información. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", UTILES.pito(48).Cast<
                    //      MigrationSupport.MsgBoxStyle>(), "SWIFT");
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = "SWIFT",
                        Text = " Se ha producido un error de Comunicación al tratar de Grabar Información.",
                        Type = TipoMensaje.Informacion
                    });

                    return CreaDesdeBIC;
                }

                if (MODGSRM.HayErr_Syb(R) != 0)
                {
                    //MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de Grabar Información. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", UTILES.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "SWIFT");
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = "SWIFT",
                        Text = " Se ha producido un error al tratar de Grabar Información.",
                        Type = TipoMensaje.Informacion
                    });

                    return CreaDesdeBIC;
                }

                CreaDesdeBIC = true;

                return CreaDesdeBIC;

            }
            catch (Exception exc)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = "Planillas Visibles de Exportación",
                    Text = " Error al crear desde BIC.",
                    Type = TipoMensaje.Informacion
                });
            }

            return CreaDesdeBIC;
        }

        //public static int AceptaParty(InitializationObject initObj, UnitOfWorkCext01 uow, ref string LlaveIng, int modo)
        //{
        //    int AceptaParty = 0;

        //    //int iNewIndex = 0;
        //    string TxtOut = "";
        //    bool encontrado = false;
        //    string[] LlaveBaja = null;
        //    int i = 0;
        //    string l = "";
        //    string llave = "";
        //    string a = "";
        //    int s = 0;
        //    string est = "";
        //    string st = "";
        //    LlaveBaja = new string[1];

        //    encontrado = false;
        //    if (LlaveIng == "")
        //    {

        //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //        {
        //            Title = T_PRTGLOB.TitAbrir,
        //            Text = " Debe ingresar una llave de identificación.",
        //            Type = TipoMensaje.Informacion
        //        });

        //        AceptaParty = true.ToInt();
        //        return AceptaParty;
        //    }

        //    initObj.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = false.ToInt();
        //    initObj.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = false.ToInt();

        //    if (modo != 0)
        //    {
        //        // Búsqueda en servidor de datos Sybase
        //        if (existesy(initObj, uow, LlaveIng, false.ToInt()) != 0)
        //        {
        //            encontrado = true;
        //        }
        //        else
        //        {
        //            TxtOut = "El participante solicitado no se encuentra registrado.";
        //            // FTF:07032001, Revisa existencia en base BIC para recuperar datos basicos
        //            if (ExisteBIC(initObj, uow, LlaveIng))
        //            {
        //                TxtOut = TxtOut + " Desea recuperarlo desde Base BIC?";
        //                if (MigrationSupport.Utils.MsgBox(TxtOut, (T_PRTGLOB.MB_YESNOCANCEL + T_PRTGLOB.MB_ICONQUESTION).Cast<MigrationSupport.MsgBoxStyle>(), T_PRTGLOB.TitAbrir) == System.Windows.Forms.DialogResult.Yes)
        //                {
        //                    if (!CreaDesdeBIC(initObj, LlaveIng))
        //                    {
        //                        LlaveIng = "";
        //                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //                        {
        //                            Title = T_PRTGLOB.TitAbrir,
        //                            Text = " El participante no pudo ser creado desde base BIC.",
        //                            Type = TipoMensaje.Informacion
        //                        });

        //                        AceptaParty = true.ToInt();
        //                        return AceptaParty;
        //                    }

        //                    if (existesy(initObj, uow, LlaveIng, false.ToInt()) == 0)
        //                    {

        //                        LlaveIng = "";
        //                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //                        {
        //                            Title = T_PRTGLOB.TitAbrir,
        //                            Text = " El participante no pudo ser creado desde base BIC.",
        //                            Type = TipoMensaje.Informacion
        //                        });
        //                        AceptaParty = true.ToInt();
        //                        return AceptaParty;
        //                    }
        //                    else
        //                    {
        //                        encontrado = true;
        //                    }
        //                }
        //                else
        //                {
        //                    LlaveIng = "";
        //                    AceptaParty = true.ToInt();
        //                    return AceptaParty;
        //                }
        //            }
        //            else
        //            {
        //                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //                {
        //                    Title = T_PRTGLOB.TitAbrir,
        //                    Text = TxtOut,
        //                    Type = TipoMensaje.Informacion
        //                });
        //                LlaveIng = "";

        //                AceptaParty = true.ToInt();
        //                return AceptaParty;
        //            }
        //        }
        //    }

        //    // 
        //    // fue encontrado o bajado desde el host... Abrirlo
        //    limpia(initObj);
        //    initObj.PRTGLOB.Party.estado = T_PRTGLOB.leido;
        //    //initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 1;

        //    a = LlaveIng + new string(126.ToChar(), 12 - LlaveIng.Len());

        //    //lee_infopartySy(initObj, uow, a, ref s);

        //    if (s != 0)
        //    {
        //        initObj.PRTGLOB.Party.estado = false.ToInt();
        //        AceptaParty = true.ToInt();
        //        return AceptaParty;
        //    }

        //    initObj.PRTGLOB.Party.idparty = a;

        //    //escribe_llave(initObj.PRTGLOB.Party.idparty); 
        //    initObj.PrtEnt01.Listarazones.Clear();
        //    initObj.PrtEnt01.Listadirec.Clear();

        //    //lee_razSy(initObj, uow, initObj.PRTGLOB.Party.idparty);

        //    for (i = 0; i <= initObj.PRTGLOB.nom.GetUpperBound(0); i += 1)
        //    {
        //        switch (initObj.PRTGLOB.nom[i].estado)
        //        {
        //            case T_PRTGLOB.leido:
        //                est = "Leída";
        //                break;
        //            case T_PRTGLOB.eliminado_leido:
        //            case T_PRTGLOB.eliminado_modificado:
        //                est = "Eliminada";
        //                break;
        //            case T_PRTGLOB.modificado:
        //                est = "Modificada";
        //                break;
        //        }

        //        if (initObj.PRTGLOB.nom[i].borrado == "1")
        //        {
        //            // Realsystems 04-09-2008
        //            st = initObj.PRTGLOB.nom[i].nombre + 9.Char() + "[Inactivo]" + 9.Char() + est;     // Realsystems 04-09-2008
        //        }
        //        else
        //        {
        //            st = initObj.PRTGLOB.nom[i].nombre + 9.Char() + "[Activo]" + 9.Char() + est;
        //        }

        //        // iNewIndex = initObj.PrtEnt01.Listarazones.Add(new UI_Combo { 

        //        //});
        //        //initObj.PrtEnt01.Listarazones.SetItemData(iNewIndex, i);

        //    }
        //    l = "" + 9.Char() + "";
        //    // initObj.PrtEnt01.Listarazones.Add(l);

        //    lee_dirSy(initObj, uow, initObj.PRTGLOB.Party.idparty);

        //    for (i = 0; i <= initObj.PRTGLOB.direc.GetUpperBound(0); i += 1)
        //    {
        //        switch (initObj.PRTGLOB.direc[i].estado)
        //        {
        //            case T_PRTGLOB.leido:
        //                est = "Leída";
        //                break;
        //            case T_PRTGLOB.eliminado_leido:
        //            case T_PRTGLOB.eliminado_modificado:
        //                est = "Eliminada";
        //                break;
        //            case T_PRTGLOB.modificado:
        //                est = "Modificada";
        //                break;
        //        }

        //        if (initObj.PRTGLOB.direc[i].borrado == "1")
        //        {
        //            // Realsystems 04-09-2008
        //            st = initObj.PRTGLOB.direc[i].direccion.Mid(1, 20) + 9.Char() + initObj.PRTGLOB.direc[i].ciudad.Mid(1, 10) + 9.Char() + "[Inactivo]" + 9.Char() + est;     // Realsystems 04-09-2008
        //        }
        //        else
        //        {
        //            st = initObj.PRTGLOB.direc[i].direccion.Mid(1, 20) + 9.Char() + initObj.PRTGLOB.direc[i].ciudad.Mid(1, 10) + 9.Char() + "[Activo]" + 9.Char() + est;
        //        }
        //        //iNewIndex = initObj.PrtEnt01.Listadirec.Add(st);
        //        //initObj.PrtEnt01.Listadirec.SetItemData(iNewIndex, i);
        //    }
        //    l = "" + 9.Char() + "";
        //    // initObj.PrtEnt01.Listadirec.Add(l);

        //    switch (initObj.PRTGLOB.Party.tipo)
        //    {
        //        case T_PRTGLOB.tipo_cliente:
        //            habilita(initObj);
        //            break;
        //        case T_PRTGLOB.tipo_banco:
        //            if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == T_PRTGLOB.Gprt_FlagCorresponsal && (initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == T_PRTGLOB.Gprt_FlagAcreedor)
        //            {
        //                habilita(initObj);
        //            }
        //            else
        //            {
        //                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == T_PRTGLOB.Gprt_FlagCorresponsal)
        //                {
        //                    habilita(initObj);
        //                    // excluir tasas especiales                        
        //                    initObj.Mdi_Principal.Opciones[3].Enabled = false;     // menu
        //                    initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;     // boton
        //                    return AceptaParty;
        //                }
        //                else
        //                {
        //                    habilita(initObj);
        //                    // excluir cuentas corrientes
        //                    initObj.Mdi_Principal.Opciones[2].Enabled = false;     // menu
        //                    initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = false;   // boton
        //                }
        //                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == T_PRTGLOB.Gprt_FlagAcreedor)
        //                {
        //                    habilita(initObj);
        //                    // excluir tasas especiales
        //                    initObj.Mdi_Principal.Opciones[3].Enabled = false;     // menu
        //                    initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;     // boton
        //                    return AceptaParty;
        //                }
        //                else
        //                {
        //                    habilita(initObj);
        //                    // excluir cuentas corrientes
        //                    initObj.Mdi_Principal.Opciones[2].Enabled = false;     // menu
        //                    initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = false;   // boton
        //                }
        //            }
        //            // excluir tasas especiales
        //            initObj.Mdi_Principal.Opciones[3].Enabled = false;     // menu
        //            initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;     // boton
        //            break;
        //        case T_PRTGLOB.individuo:
        //            habilita(initObj);
        //            // excluir cuentas
        //            initObj.Mdi_Principal.Opciones[2].Enabled = false;     // menu
        //            initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = false;   // boton
        //            if (initObj.PRTGLOB.Party.rut == "")
        //            {
        //                // excluir caracteristicas
        //                initObj.Mdi_Principal.Opciones[2].Enabled = false;     // menu
        //                initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = false;   // boton
        //                // excluir tasas
        //                initObj.Mdi_Principal.Opciones[3].Enabled = false;     // menu
        //                initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;     // boton
        //            }
        //            else
        //            {
        //                // incluir caracteristicas
        //                initObj.Mdi_Principal.Opciones[0].Enabled = true;
        //                initObj.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled = true;
        //                // incluir tasas
        //                initObj.Mdi_Principal.Opciones[3].Enabled = true;     // menu
        //                initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = true;     // boton
        //            }
        //            break;
        //    }

        //    // ©WaldoSoft 18/04/94.
        //    // Tengo leidos el participante, veamos el acceso.  Si VerDependencia entrega
        //    // que existe dependencia ==>
        //    //    participante es modificable:    Habilitar Salvar
        //    //                                    Habilitar Eliminar
        //    //                                    Deshabilitar Recuperar del Servidor
        //    //    Participante No modificable:    Deshabilitar Salvar
        //    //                                    Deshabilitar Eliminar
        //    //                                    Habilitar Recuperar del Servidor
        //    // Como Inf los datos del Party, Como superior el usuario           

        //    if (true)
        //    {
        //        initObj.Mdi_Principal.Archivo[2].Enabled = true;     // menu
        //        initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;      // boton               
        //        initObj.Mdi_Principal.Archivo[4].Enabled = true;      // eliminar
        //    }
        //    else
        //    {
        //        // salvar participante
        //        initObj.Mdi_Principal.Archivo[2].Enabled = false;     // menu
        //        initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;      // boton
        //        initObj.Mdi_Principal.Archivo[4].Enabled = false;   // eliminar
        //    }
        //    // ---------------------------------------------
        //    if (T_MODWS.ACCESO == "0")
        //    {
        //        MODWS.VistaConsulta(initObj);
        //    }
        //    // ---------------------------------------------
        //    //  RealSystems - Código Nuevo - Termino
        //    // ---------------------------------------------

        //    return AceptaParty;
        //}

        public static int activar_razsoc(InitializationObject initObj, UnitOfWorkCext01 unit, decimal idNombre)
        {
            int estado = 0;

            try
            {
                var Result = unit.SceRepository.sce_ras_u01_MS(initObj.PRTGLOB.Party.idparty, idNombre);

                if (Result == "Actualizacion Exitosa")
                    estado = 1;
            }
            catch (Exception exc)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = MODGUSR.MsgUsr,
                    Text = "Error"
                });

                return -1;
            }
            return estado;
        }

        public static void lee_razSy(ref InitializationObject iO, XgpyService xS, String llave)
        {
            int i = 0;
            short n = 0;
            int final = 0;
            final = -1;
            final = iO.PRTGLOB.nom.Count();

            if (final > 0 && iO.PRTGLOB.nom != null)
            {
                if (iO.PRTGLOB.nom[0] != null)
                {
                    if (!String.IsNullOrEmpty(iO.PRTGLOB.nom[0].nombre))
                        return;
                }
            }

            try
            {
                IList<sce_rsa_s07_MS_Result> _sce_rsa_s07_MS;
                _sce_rsa_s07_MS = xS.Sce_Rsa_S07_MS(llave);
                n = (short)_sce_rsa_s07_MS.Count();

                if (_sce_rsa_s07_MS.Count > 0)
                    iO.PRTGLOB.nom = new prtynombre[n];
                
                if (n > 0)
                {
                    for (i = 0; i < _sce_rsa_s07_MS.Count; i++)
                    {
                        iO.PRTGLOB.nom[i] = new prtynombre();
                        iO.PRTGLOB.nom[i].borrado = _sce_rsa_s07_MS[i].borrado ? "1" : "0";
                        iO.PRTGLOB.nom[i].indice = i;
                        iO.PRTGLOB.nom[i].nombre = _sce_rsa_s07_MS[i].razon_soci;
                        iO.PRTGLOB.nom[i].fantasia = _sce_rsa_s07_MS[i].nom_fantas;
                        iO.PRTGLOB.nom[i].contacto = _sce_rsa_s07_MS[i].contacto;
                        iO.PRTGLOB.nom[i].sortkey = _sce_rsa_s07_MS[i].sortkey;
                        iO.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;
                    }
                }

                return;
            }
            catch { }
        }

        public static void lee_riesgoSy(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int k = 0;
            int i = 0;
            int n = 0;
            int fin = 0;

            try
            {
                fin = -1;
                fin = initObj.PRTGLOB.riesgo.GetUpperBound(0);
                if (fin != -1)
                {
                    if (initObj.PRTGLOB.riesgo[0].nombre != "" || initObj.PRTGLOB.riesgo[0].nombre != null)
                        return;
                }

                IList<sgt_clf_s01_MS_Result> _sgt_clf_s01_MS;
                _sgt_clf_s01_MS = uow.SceRepository.Sgt_clf_s01_MS();

                // Resultado nulo de la Consulta.-
                if (_sgt_clf_s01_MS == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = T_PRTGLOB.Master_Titulo,
                        Text = " No se han encontrado datos en la Tabla Sgt_Clf.",
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }

                for (i = 0; i < _sgt_clf_s01_MS.Count(); i++)
                {
                    fin = -1;
                    fin = initObj.PRTGLOB.riesgo.GetUpperBound(0);

                    if (fin != -1)
                    {
                        if (initObj.PRTGLOB.riesgo[0].nombre == "" || initObj.PRTGLOB.riesgo[0].nombre == null)
                            k = 0;
                        else
                        {
                            k = fin + 1;
                            Array.Resize(ref initObj.PRTGLOB.riesgo, k + 1);
                        }
                    }
                    else
                    {
                        k = 0;
                        initObj.PRTGLOB.riesgo = new tipo_riesgo[k + 1];
                    }

                    initObj.PRTGLOB.riesgo[k].codigo = _sgt_clf_s01_MS[i].clf_clfcod;
                    initObj.PRTGLOB.riesgo[k].nombre = _sgt_clf_s01_MS[i].clf_clfdes;
                }
                return;
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.Master_Titulo,
                    Text = " Se ha producido un error al tratar de leer la Tabla Sgt_Clf.",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
        }

        public static void limpia(ref InitializationObject paramPrtyObject)
        {
            object VerDep_NoDepende = null;
            object PrtDepende = null;
            bool FlagInst = false;
            prtytgas[] resgas = null;
            prtytint[] resint = null;
            prtytcom[] rescom = null;

            paramPrtyObject.PRTGLOB.Party = new prtyprincipal();

            //  D.S.B.
            paramPrtyObject.PRTYENT2.RSGTCliEsp = new CliEsp[1];
            paramPrtyObject.PRTYENT2.VSGTCliEsp = new CliEsp[1];
            T_PRTYENT.Cliente_SGT = false.ToInt();

            paramPrtyObject.PRTGLOB.Party.PrtGlob = new PrtGlob();
            paramPrtyObject.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = false.ToInt();
            paramPrtyObject.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = false.ToInt();
            paramPrtyObject.PRTGLOB.Party.PrtGlob.MemosLeidos = false.ToInt();

            paramPrtyObject.PRTGLOB.ctaclie = new prtyccta[1];
            paramPrtyObject.PRTGLOB.ctabancos = new prtybcta[1];
            paramPrtyObject.PRTGLOB.linbancos = new prtyblinea[1];
            paramPrtyObject.PRTGLOB.instruccion = new prtyinst[1];
            paramPrtyObject.PRTGLOB.direc = new prtydireccion[1];
            paramPrtyObject.PRTGLOB.nom = new prtynombre[1];
            rescom = new prtytcom[1];
            resint = new prtytint[1];
            resgas = new prtytgas[1];
            paramPrtyObject.PRTGLOB.tasacom = new prtytcom[1];
            paramPrtyObject.PRTGLOB.tasaint = new prtytint[1];
            paramPrtyObject.PRTGLOB.tasagas = new prtytgas[1];

            paramPrtyObject.PRTGLOB.Party.PrtGlob.FlagParty = false.ToInt();
            FlagInst = false;
            T_PRTGLOB.FlagRazones = false.ToInt();
            T_PRTGLOB.FlagDireccion = false.ToInt();
            T_PRTGLOB.FlagCuentas = false.ToInt();
            T_PRTGLOB.FlagCtaBco = false.ToInt();
            T_PRTGLOB.FlagLineas = false.ToInt();
            T_PRTGLOB.FlagComision = false.ToInt();
            T_PRTGLOB.FlagInteres = false.ToInt();
            T_PRTGLOB.FlagInstruccion = false.ToInt();
            T_PRTGLOB.FlagGasto = false.ToInt();

            // reseteamos la autorizacion
            PrtDepende = VerDep_NoDepende;
            return;
        }

        public static void lee_dirSy(ref InitializationObject iO, XgpyService xS, String llave)
        {
            int i = 0;
            short n = 0;
            string fono = "";
            string fax = "";
            int final = 0;
            final = -1;

            final = iO.PRTGLOB.direc.Count();
            if (final > 0)
            {
                if (iO.PRTGLOB.direc != null)
                {
                    if (iO.PRTGLOB.direc[0] != null)
                    {
                        if (!String.IsNullOrEmpty(iO.PRTGLOB.direc[0].direccion))
                            return;
                    }
                }
            }

            try
            {
                IList<sce_dad_s08_MS_Result> _sce_dad_s08_MS;

                _sce_dad_s08_MS = xS.Sce_Dad_S08_MS(llave);

                if (_sce_dad_s08_MS == null)
                    return;
                
                n = (short)_sce_dad_s08_MS.Count();

                if (_sce_dad_s08_MS.Count > 0)
                    iO.PRTGLOB.direc = new prtydireccion[n];
                
                for (i = 0; i < _sce_dad_s08_MS.Count; i += 1)
                {
                    iO.PRTGLOB.direc[i] = new prtydireccion();
                    iO.PRTGLOB.direc[i].borrado = _sce_dad_s08_MS[i].borrado ? "1" : "0";
                    iO.PRTGLOB.direc[i].indice = i;
                    iO.PRTGLOB.direc[i].direccion = _sce_dad_s08_MS[i].direccion.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].comuna = _sce_dad_s08_MS[i].comuna.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].CodComuna = _sce_dad_s08_MS[i].cod_comuna.ToInt();
                    iO.PRTGLOB.direc[i].codpostal = _sce_dad_s08_MS[i].cod_postal.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].region = _sce_dad_s08_MS[i].estado.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].ciudad = _sce_dad_s08_MS[i].ciudad.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].pais = _sce_dad_s08_MS[i].pais.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].CodPais = _sce_dad_s08_MS[i].cod_pais.ToInt();

                    fono = _sce_dad_s08_MS[i].telefono.Replace("`", "'").Trim();

                    if (fono.ToVal() != 0)
                        iO.PRTGLOB.direc[i].telefono = fono;
                    else
                        iO.PRTGLOB.direc[i].telefono = "";
                    
                    fax = _sce_dad_s08_MS[i].fax.Replace("`", "'").Trim();

                    if (fax.ToVal() != 0)
                        iO.PRTGLOB.direc[i].fax = fax;
                    else
                        iO.PRTGLOB.direc[i].fax = "";

                    iO.PRTGLOB.direc[i].telex = _sce_dad_s08_MS[i].telex.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].enviar_a = _sce_dad_s08_MS[i].envio_sce.ToInt();
                    iO.PRTGLOB.direc[i].recibe = _sce_dad_s08_MS[i].recibe_sce.ToInt();
                    iO.PRTGLOB.direc[i].CasPostal = _sce_dad_s08_MS[i].cas_postal.Replace("`", "'").Trim();
                    iO.PRTGLOB.direc[i].CasBanco = _sce_dad_s08_MS[i].cas_banco;
                    iO.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                    iO.PRTGLOB.direc[i].email = _sce_dad_s08_MS[i].email == null ? string.Empty : _sce_dad_s08_MS[i].email.Replace("`", "'").Trim();
                }

                return;
            }
            catch { }

            return;
        }

        public static void lee_ejecutivosSy(InitializationObject initObj, UnitOfWorkCext01 uow, string codigo)
        {
            int k = 0;
            int fin = 0;
            int i = 0;
            int n = 0;
            try
            {
                initObj.PRTGLOB.ejecutivos = new tipo_riesgo[0];
                fin = -1;
                fin = initObj.PRTGLOB.ejecutivos.GetUpperBound(0);

                if (fin != -1)
                {
                    if (String.IsNullOrEmpty(initObj.PRTGLOB.ejecutivos[0].nombre))
                        return;
                    
                }

                IList<sgt_ejc_s02_MS_Result> R;
                R = uow.SceRepository.Sgt_ejc_S02_MS(codigo.ToInt());

                if (R == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = T_PRTGLOB.Master_Titulo,
                        Text = " No se han encontrado datos en la Tabla Sce_Ejc.",
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }

                n = (short)R.Count();

                if (n > 0)
                {
                    initObj.PRTGLOB.ejecutivos = new tipo_riesgo[n];

                    for (i = 0; i < R.Count(); i++)
                    {
                        initObj.PRTGLOB.ejecutivos[i].codigo = R[i].ejc_ejccod.ToString();
                        initObj.PRTGLOB.ejecutivos[i].nombre = R[i].ejc_ejcnom;
                    }
                }
                return;
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.Master_Titulo,
                    Text = " Se ha producido un error al tratar de leer la Tabla Sce_Ejc.",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
        }

        public static void lee_ejecutivosSy_Initialize(ref InitializationObject initObj, XgpyService xS, String llave) 
        {
            var listaEjecutivos = xS.ejc_prty_ejc_s_01_MS(initObj.PRTGLOB.Party.rut).ToList();
            initObj.PrtEnt07.Txt_Exp.Text = string.Empty;
            initObj.PrtEnt07.Txt_Imp.Text = string.Empty;
            initObj.PrtEnt07.Txt_Negocios.Text = string.Empty;

            if (listaEjecutivos.Count > 0)
            {
                var ejecutivoImportacion = listaEjecutivos.FirstOrDefault(x => x.ejc_tpo == T_PRTYENT2.EJE_tipopimp);
                var ejecutivoExportacion = listaEjecutivos.FirstOrDefault(x => x.ejc_tpo == T_PRTYENT2.EJE_tipopexp);
                var ejecutivoNegocio = listaEjecutivos.FirstOrDefault(x => x.ejc_tpo == T_PRTYENT2.EJE_tipnegoc);
                initObj.PRTYENT2.VSGTCliEsp = new CliEsp[listaEjecutivos.Count];

                for (int i = 0; i < listaEjecutivos.Count; i++)
                {
                    var ofiEjeF = 
                        listaEjecutivos[i].ejc_ofi.ToStr().Len() == 1 ? String.Format("00{0}", listaEjecutivos[i].ejc_ofi.ToStr()) :
                        listaEjecutivos[i].ejc_ofi.ToStr().Len() == 2 ? String.Format("0{0}", listaEjecutivos[i].ejc_ofi.ToStr()) :
                        listaEjecutivos[i].ejc_ofi.ToStr();

                    var codEje =
                        listaEjecutivos[i].ejc_cod.ToStr().Len() == 1 ? String.Format("00{0}", listaEjecutivos[i].ejc_cod.ToStr()) :
                        listaEjecutivos[i].ejc_cod.ToStr().Len() == 2 ? String.Format("0{0}", listaEjecutivos[i].ejc_cod.ToStr()) :
                        listaEjecutivos[i].ejc_cod.ToStr();

                    initObj.PRTYENT2.VSGTCliEsp[i] = new CliEsp();
                    initObj.PRTYENT2.VSGTCliEsp[i].nrut = listaEjecutivos[i].prty_rut;
                    initObj.PRTYENT2.VSGTCliEsp[i].tipo = listaEjecutivos[i].ejc_tpo;
                    initObj.PRTYENT2.VSGTCliEsp[i].ofieje = ofiEjeF;
                    initObj.PRTYENT2.VSGTCliEsp[i].codeje = codEje;
                    initObj.PRTYENT2.VSGTCliEsp[i].estado = T_PRTGLOB.leido;
                }
            }
        }

        public static void escribe_llave(string llave)
        {
            /*
            string Tit = "";
            string l = "";

            l = UTILES.copiardestring(llave, "~", 1);
            Tit = "Participante " + "[" + l + "]";
             * */
        }

        public static void lee_infopartySy(ref InitializationObject iO, XgpyService xS, string llave, ref int salir)
        {
            string basura1 = "";
            string msj = "";
            string llaveFinal = llave.PadRight(12, '|');
            sce_prty_s08_MS_Result R;
            R = xS.Sce_Prty_S08_MS(llaveFinal);

            if (R == null)
            {
                salir = 99;
                return;
            }

            EstadoParty = R.borrado.ToInt();

            if (EstadoParty != 0)
            {
                salir = 100;
                return;
            }

            iO.PRTGLOB.Party.tipo = R.tipo_party.ToInt();
            iO.PRTGLOB.Party.Bnumber = null;

            if (iO.PRTGLOB.Party.tipo == 2)
                iO.PRTGLOB.Party.PrtGlob.EsCITI = true.ToInt();
            else
            {
                if (iO.PRTGLOB.Party.tipo == 3)
                {
                    iO.PRTGLOB.Party.PrtGlob.EsCITI = true.ToInt();
                    iO.PRTGLOB.Party.tipo = 2;
                    iO.PRTGLOB.Party.Bnumber = R.id_party;
                    //iO.PRTGLOB.Party.Bnumber = basura1;
                }
                else
                    iO.PRTGLOB.Party.PrtGlob.EsCITI = false.ToInt();
            }

            iO.PRTGLOB.Party.Flag = R.flag.ToInt();
            iO.PRTGLOB.Party.clasificacion = R.clasificac.ToInt();
            iO.PRTGLOB.Party.sirut = R.tiene_rut.ToInt();
            iO.PRTGLOB.Party.rut = R.rut;
            iO.PRTGLOB.Party.creacosto = R.crea_costo;
            iO.PRTGLOB.Party.creauser = R.crea_user;

            if (String.IsNullOrEmpty(iO.PRTGLOB.Party.creacosto) || String.IsNullOrEmpty(iO.PRTGLOB.Party.creauser))
            {
                iO.PRTGLOB.Party.creacosto = MODGUSR.UsrEsp.CentroCosto;
                iO.PRTGLOB.Party.creauser = MODGUSR.UsrEsp.Especialista;
                iO.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
            }

            iO.PRTGLOB.Party.modcosto = R.mod_costo;
            iO.PRTGLOB.Party.moduser = R.mod_user;
            iO.PRTGLOB.Party.multiple = R.multiple.ToInt();

            switch (iO.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                case T_PRTGLOB.individuo:
                    iO.PRTGLOB.Party.oficina = R.cod_ofieje;
                    iO.PRTGLOB.Party.ejecutivo = R.cod_eject;
                    iO.PRTGLOB.Party.actividad = R.cod_acteco;
                    iO.PRTGLOB.Party.riesgo = R.clase_ries;
                    break;

                case T_PRTGLOB.tipo_banco:
                    iO.PRTGLOB.Party.codbco = (int)R.cod_bco;
                    iO.PRTGLOB.Party.libor = R.tasa_libor.ToInt();
                    iO.PRTGLOB.Party.prime = R.tasa_prime.ToInt();
                    iO.PRTGLOB.Party.spread = R.spread.ToDbl();
                    iO.PRTGLOB.Party.swif = R.swift;
                    iO.PRTGLOB.Party.aladi = (int)R.plaza_alad;
                    iO.PRTGLOB.Party.ejecorr = R.ejec_corre;
                    break;
            }

            iO.PRTGLOB.Party.flagins = (int)R.flagins;
            iO.PRTGLOB.Party.insgen_imp = R.insgen_imp.ToInt(); //ToStr
            iO.PRTGLOB.Party.insgen_exp = R.insgen_exp.ToInt();
            iO.PRTGLOB.Party.inscob_imp = R.inscob_imp.ToInt();
            iO.PRTGLOB.Party.inscob_exp = R.inscob_exp.ToInt();
            iO.PRTGLOB.Party.inscre_imp = R.inscre_imp.ToInt();
            iO.PRTGLOB.Party.inscre_exp = R.inscre_exp.ToInt();

            return;
        }

        public static void lee_instrucSy_Inicializar(ref InitializationObject initObj, XgpyService xS, String llave) 
        {
            try
            {
                var _sce_prty_s08_MS = xS.Sce_Prty_S08_MS(llave);

                if (_sce_prty_s08_MS == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = T_PRTGLOB.TitInstrucciones,
                        Text = " No se ha encontrado al Participante.",
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
                else
                {
                    initObj.PRTGLOB.Party.insgen_imp = _sce_prty_s08_MS.insgen_imp.ToInt();
                    initObj.PRTGLOB.Party.insgen_exp = _sce_prty_s08_MS.insgen_exp.ToInt();
                    initObj.PRTGLOB.Party.insgen_ser = _sce_prty_s08_MS.insgen_ser.ToInt();
                    initObj.PRTGLOB.Party.inscob_imp = _sce_prty_s08_MS.inscob_imp.ToInt();
                    initObj.PRTGLOB.Party.inscob_exp = _sce_prty_s08_MS.inscob_exp.ToInt();
                    initObj.PRTGLOB.Party.inscre_imp = _sce_prty_s08_MS.inscre_imp.ToInt();
                    initObj.PRTGLOB.Party.inscre_exp = _sce_prty_s08_MS.inscre_exp.ToInt();
                    return;
                }
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.TitInstrucciones,
                    Text = " Error al leer instrucciones del Participante.",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
        }

        public static void lee_instrucSy(InitializationObject initObj, UnitOfWorkCext01 unit, string llave, int cuales)
        {
            try
            {
                sce_prty_s08_MS_Result _sce_prty_s08_MS;
                string llaveFinal = llave.PadRight(12, '|');
                _sce_prty_s08_MS = unit.SceRepository.Sce_Prty_S08_MS(llaveFinal);

                if (_sce_prty_s08_MS == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = T_PRTGLOB.TitInstrucciones,
                        Text = " No se ha encontrado al Participante.",
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
                else
                {
                    initObj.PRTGLOB.Party.insgen_imp = _sce_prty_s08_MS.insgen_imp.ToInt();
                    initObj.PRTGLOB.Party.insgen_exp = _sce_prty_s08_MS.insgen_exp.ToInt();
                    initObj.PRTGLOB.Party.insgen_ser = _sce_prty_s08_MS.insgen_ser.ToInt();
                    initObj.PRTGLOB.Party.inscob_imp = _sce_prty_s08_MS.inscob_imp.ToInt();
                    initObj.PRTGLOB.Party.inscob_exp = _sce_prty_s08_MS.inscob_exp.ToInt();
                    initObj.PRTGLOB.Party.inscre_imp = _sce_prty_s08_MS.inscre_imp.ToInt();
                    initObj.PRTGLOB.Party.inscre_exp = _sce_prty_s08_MS.inscre_exp.ToInt();
                    return;
                }
            }
            catch (Exception e)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.TitInstrucciones,
                    Text = " Error al leer instrucciones del Participante.",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
        }

        public static int actualizapartySy(ref InitializationObject iO, XgpyService xS, String llaveIdPrty)
        {
            try
            {
                if (xS.Sce_Prty_U01_MS(llaveIdPrty, false) >= 1)
                    return 1;
                else
                    return 0;
            }
            catch
            {
                return -1;
            }
        }

        public static void habilitaindiv(InitializationObject initObj)
        {
            ////initObj.Mdi_Principal.menu[1].Enabled = true;          
            initObj.Mdi_Principal.Opciones[0].Enabled = true;
            initObj.Mdi_Principal.Opciones[1].Enabled = true;
            initObj.Mdi_Principal.Opciones[2].Enabled = false;
            initObj.Mdi_Principal.Archivo[2].Enabled = true;     // menu Salvar     
            initObj.Mdi_Principal.Archivo[4].Enabled = true;  // menú eliminar
            initObj.Mdi_Principal.Archivo[5].Enabled = true;  // menú Salir
            initObj.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled = true;
            initObj.Mdi_Principal.BUTTONS["tbr_Instrucciones"].Enabled = true;
            initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = true;

            if (initObj.PRTGLOB.Party.rut != "" || initObj.PRTGLOB.Party.rut != null)
            {
                initObj.Mdi_Principal.Opciones[0].Enabled = true;  //Caracteristicas
                initObj.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled = true;
                initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = true;
                initObj.Mdi_Principal.Opciones[3].Enabled = true;     // Tasas
            }
            else
            {
                initObj.Mdi_Principal.Opciones[0].Enabled = false;  //Caracteristicas
                initObj.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled = false;
                initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;
                initObj.Mdi_Principal.Opciones[3].Enabled = false;     // Tasas
            }

            initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;      // boton Guardar
        }

        public static double Habil_SGTCliEje(UnitOfWorkCext01 uow,InitializationObject init)
        {
            double habil = 0;
            habil = SyGet_Ini(uow,init,"sgtclieje", "xx", 0);
            return habil;
        }

        public static double SyGet_Ini(UnitOfWorkCext01 uow, InitializationObject initObj, string grupo, string elem, int flgAviso)
        {
            double SyGet_Ini = 0.0;
            string tipo =string.Empty;
            string valor = string.Empty;
            decimal largo = 0;
            decimal decim = 0;

            try
            {
                IList<sce_ini_s01_MS_Result> result = uow.SceRepository.sce_ini_s01_MS(grupo, elem);

                if (result == null)
                {
                    if (flgAviso != 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Title = "Tabla sce_ini",
                            Text = " No existe en tabla sce_ini valor para [" + grupo + ", " + elem + "]" + ".",
                            Type = TipoMensaje.Informacion
                        });
                    }
                    return SyGet_Ini;
                }
                else
                {
                    foreach (sce_ini_s01_MS_Result resultado in result)
                    {
                        tipo = resultado.tipov;
                        largo = resultado.largo;
                        decim = resultado.decim;
                        valor = resultado.valor;
                    }
                    if (tipo == "N")
                        SyGet_Ini = valor.ToVal();
                    
                    else if (tipo == "C")
                        SyGet_Ini = valor.ToDbl();           
                }

                return SyGet_Ini;
            }
            catch (Exception exc)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = "Tabla sce_ini",
                    Text = " No existe en tabla sce_ini valor para [" + grupo + ", " + elem + "]" + ".",
                    Type = TipoMensaje.Informacion
                });
                return SyGet_Ini;
            }
        }

        public static void Carga_Datos_PrtEnt07(UnitOfWorkCext01 uow, InitializationObject InitObject, IList<string> MSJRET)
        {
            int Y = 0;
            string codigoOficina = string.Empty;
            string nombreOficina = string.Empty;
            string la_ofi = string.Empty;
            InitObject.PRTGLOB.Party.oficina = MSJRET[0];
            InitObject.PRTGLOB.Party.ejecutivo = MSJRET[1];
            //INICIO MODIFICACION CNC - ACCENTURE
            int result;
            if (int.TryParse(MSJRET[2].ToString(),out result))
            {
                InitObject.PRTGLOB.Party.actividad = int.Parse(MSJRET[2]).ToString();
            }
            else 
            {
                InitObject.PRTGLOB.Party.actividad = MSJRET[2];
            }
            //FIN MODIFICACION CNC - ACCENTURE
            InitObject.PRTGLOB.Party.riesgo = MSJRET[3].Trim();

            // Oficina

            la_ofi = InitObject.PrtEnt07.cboOficina.Text;
            InitObject.PrtEnt07.cboOficina.Text = MSJRET[0];             
            codigoOficina = InitObject.PrtEnt07.cboOficina.get_ItemData_(InitObject.PrtEnt07.cboOficina.ListIndex.ToInt()).ToString().PadLeft(3, '0'); ;
            nombreOficina = PRTYENT.nom_ofi(InitObject, codigoOficina);

            if (string.IsNullOrEmpty(nombreOficina))
            {
                InitObject.PrtEnt07.cboOficina.Items.Clear();
                InitObject.PrtEnt07.ejecutivo.Items.Clear();
                return;
            }
            else
            {
                InitObject.PrtEnt07.oficina.Text = nombreOficina;
                InitObject.PrtEnt07.oficina.Tag = codigoOficina;

                if (InitObject.PrtEnt07.cboOficina.Tag.ToString() != la_ofi)
                {
                    la_ofi = InitObject.PrtEnt07.oficina.Tag.ToStr();
                    InitObject.PrtEnt07.ejecutivo.ListIndex = -1;
                    PRTYENT.lee_ejecutivosSy(InitObject, uow, codigoOficina);
                    PRTYENT.carga_ejecutivos(InitObject, InitObject.PrtEnt07.ejecutivo);
                }
            }
            
            // Ejecutivos
            for (Y = 0; Y <= InitObject.PrtEnt07.ejecutivo.Items.Count - 1; Y += 1)
            {
                if (InitObject.PrtEnt07.ejecutivo.get_ItemData_(Y).ToString().Trim() == MSJRET[1])
                {
                    InitObject.PrtEnt07.ejecutivo.ListIndex = Y;
                    break;
                }
            }

            // act economica
            for (Y = 0; Y <= InitObject.PrtEnt07.Combo2.Items.Count - 1; Y += 1)
            {
                if (MSJRET[2].Trim() != "")
                {
                    if (InitObject.PrtEnt07.Combo2.Items[Y].ToStr().InStr(MSJRET[2], 1, StringComparison.CurrentCulture) > 0)
                    {
                        InitObject.PrtEnt07.Combo2.ListIndex = Y;
                        break;
                    }
                }
                else
                {
                    InitObject.PrtEnt07.Combo2.ListIndex = Y;
                    break;
                }
                // IR68422 20140729
            }

            for (Y = 0; Y <= InitObject.PrtEnt07.Combo1.Items.Count - 1; Y += 1)
            {
                if (InitObject.PrtEnt07.Combo1.Items[Y].Value.ToStr().TrimB().UCase() == "CLIENTE")
                {
                    InitObject.PrtEnt07.Combo1.ListIndex = Y;
                    InitObject.PRTGLOB.Party.clasificacion = Y;
                    break;
                }
            }
            for (Y = 0; Y <= InitObject.PrtEnt07.Combo4.Items.Count - 1; Y += 1)
            {
                if (InitObject.PrtEnt07.Combo4.Items[Y].Value.ToStr().TrimB().UCase() == "SIN CLASIFICACION")
                {
                    InitObject.PrtEnt07.Combo4.ListIndex = Y;
                    break;
                }
            }
        }
    }
}