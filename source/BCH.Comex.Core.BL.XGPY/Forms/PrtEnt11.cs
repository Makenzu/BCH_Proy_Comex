using BCH.Comex.Core.BL.XGPY.Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using System;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public class PrtEnt11
    {

        public static void Form_Load(InitializationObject initObj)
        {
            foreach (BCH.Comex.Common.UI_Modulos.UI_ComboItem var in Funciones.Lista.Clasificacion())
            {
                initObj.PrtEnt11.Combo1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    Data = var.Data,
                    Value = var.Value
                });
            }
            initObj.PrtEnt07.Combo1.ListIndex = -1;
            initObj.PrtEnt11.prtplaza.Enabled = false;

        }

        public static void prtaladi_Click(InitializationObject initObj)
        {
            if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagAladi) != 0)
            {
                if (initObj.PrtEnt11.prtaladi.Checked != System.Windows.Forms.CheckState.Checked.ToBool())
                    initObj.PrtEnt11.aceptar.Enabled = true;
            }
            else
            {
                if (initObj.PrtEnt11.prtaladi.Checked == System.Windows.Forms.CheckState.Checked.ToBool())
                    initObj.PrtEnt11.aceptar.Enabled = true;
            }
            if (initObj.PrtEnt11.prtaladi.Checked == System.Windows.Forms.CheckState.Checked.ToBool())
                initObj.PrtEnt11.prtplaza.Enabled = true;
            else
                initObj.PrtEnt11.prtplaza.Enabled = false;

            initObj.PrtEnt11.prtplaza.Text = string.Empty;

        }
        public static void Aceptar_Click(InitializationObject initObj)
        {

            int si = 0;
            string s = "";
            string spread = "";
            // 
            // ******************************* MPN ***********************************************
            //if (initObj.PRTGLOB.Party.PrtGlob.Pertenece == 0)
            //{
            //    initObj.Mdi_Principal.Archivo[2].Enabled = false;  // menú salvar
            //    initObj.Mdi_Principal.Archivo[4].Enabled = false;  // menú eliminar
            //    initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;
            //}
            //else
            //{
            //    initObj.Mdi_Principal.Archivo[2].Enabled = false;  // menú salvar
            //    initObj.Mdi_Principal.Archivo[4].Enabled = false;  // menú eliminar
            //    initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;
            //}
            // *********************************************************************************

            if (initObj.PrtEnt11.prtaladi.Checked)
            {               
                if (string.IsNullOrEmpty(initObj.PrtEnt11.prtplaza.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe ingresar plaza aladi.",
                        Title = T_PRTGLOB.TitDatos,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
                else
                {
                    initObj.PRTGLOB.Party.aladi = initObj.PrtEnt11.prtplaza.Text.ToInt();
                    if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagAladi) == 0)
                    {
                        initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 64;
                        if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                        {
                            initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            {
                                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                            }
                        }
                    }
                }
            }
            else
            {
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagAladi) != 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag - 64;
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
            }           
            if (!string.IsNullOrEmpty(initObj.PrtEnt11.prtcodigo.Text))
            {
                if (initObj.PRTGLOB.Party.codbco == 0)
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.codbco = initObj.PrtEnt11.prtcodigo.Text.ToInt();
            }
            else
            {
                if (initObj.PRTGLOB.Party.codbco == 0)
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.codbco = 0;
            }

            //if (initObj.PrtEnt11.prtrut.Text.ToStr() != "" || initObj.PrtEnt11.prtrut.Text != null)
            if (initObj.PrtEnt11.prtrut.Text.ToStr() == "___.___.___-_")
                initObj.PrtEnt11.prtrut.Text = string.Empty;

            if (!string.IsNullOrEmpty(initObj.PrtEnt11.prtrut.Text))
            {
                T_PRTGLOB.llave = initObj.PrtEnt11.prtrut.Text;
                si = PRTYENT.esrut(T_PRTGLOB.llave);
                if (si == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Error en el Dígito Verificador del rut ingresado.",
                        Title = T_PRTGLOB.TitDatos,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }

                T_PRTGLOB.llave = PRTYENT.descero(T_PRTGLOB.llave); //PRTYENT.filcero(T_PRTGLOB.llave, T_PRTGLOB.formato_rut);
                initObj.PRTGLOB.Party.rut = T_PRTGLOB.llave;
                if (!initObj.PRTGLOB.Party.sirut.ToBool())
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.sirut = true.ToInt();
            }
            else
            {
                initObj.PRTGLOB.Party.rut = string.Empty;
                if (initObj.PRTGLOB.Party.sirut.ToBool())
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.sirut = false.ToInt();
            }

            initObj.PRTGLOB.Party.libor = initObj.PrtEnt11._prttasa_[0].Selected.ToInt();   //  _prttasa_ _prttasa_0.Checked.ToInt();
            initObj.PRTGLOB.Party.prime = initObj.PrtEnt11._prttasa_[1].Selected.ToInt();
                     
            if (!string.IsNullOrEmpty(initObj.PrtEnt11.prtspread.Text))
            {
                if (initObj.PRTGLOB.Party.spread == 0)
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }

                spread = initObj.PrtEnt11.prtspread.Text;
                initObj.PRTGLOB.Party.spread = spread.ToVal();  //PRTYENT.cambiasepdec(spread).ToVal();
            }
            else
            {
                if (initObj.PRTGLOB.Party.spread != 0)
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.spread = 0;
            }
                    
            if (!string.IsNullOrEmpty(initObj.PrtEnt11.prtswif.Text))
            {
                initObj.PRTGLOB.Party.swif = initObj.PrtEnt11.prtswif.Text;
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagSwift) == 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 32;
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
            }
            else
            {
                initObj.PRTGLOB.Party.swif = string.Empty;
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagSwift) != 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag - 32;
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
            }
           
            if (initObj.PrtEnt11._prttipob_[1].Checked == System.Windows.Forms.CheckState.Checked.ToBool())
            {
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) == 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 8;
                    initObj.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = true.ToInt();
                    initObj.PRTGLOB.ctabancos = new prtybcta[0];
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
            }
            else
            {
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagCorresponsal) != 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag - 8;
                    initObj.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = false.ToInt();
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                    }
                }
            }
                       
            if (initObj.PrtEnt11._prttipob_[0].Checked == System.Windows.Forms.CheckState.Checked.ToBool())
            {
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) == 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 16;
                    initObj.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = true.ToInt();
                    initObj.PRTGLOB.linbancos = new prtyblinea[0]; //1
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
            }
            else
            {
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagAcreedor) != 0)
                {
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag - 16;
                    initObj.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = false.ToInt();
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                    }
                }
            }

            //if (_prttipob_0.CheckState == System.Windows.Forms.CheckState.Unchecked && _prttipob_1.CheckState == System.Windows.Forms.CheckState.Unchecked)
            if ((!initObj.PrtEnt11._prttipob_[0].Checked == System.Windows.Forms.CheckState.Unchecked.ToBool()) && (!initObj.PrtEnt11._prttipob_[1].Checked == System.Windows.Forms.CheckState.Unchecked.ToBool()))
            {
                initObj.Mdi_Principal.Opciones[0].Enabled = true;
                initObj.Mdi_Principal.Opciones[1].Enabled = true;
                initObj.Mdi_Principal.Opciones[2].Enabled = false;
                initObj.Mdi_Principal.Opciones[3].Enabled = false;
                initObj.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled = false;
                initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;

            }
            else
            {
                //     habilita
                initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;  //Comando7 Boton Tasas
                initObj.Mdi_Principal.Opciones[3].Enabled = false; //Opciones Tasas
            }

            if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
            {
                initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                {
                    initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                }
            }
       
            if (!string.IsNullOrEmpty(initObj.PrtEnt11.ejecorr.Text))
            {
                if (initObj.PRTGLOB.Party.ejecorr.Trim() == "" || initObj.PRTGLOB.Party.ejecorr.Trim() == null)
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.ejecorr = initObj.PrtEnt11.ejecorr.Text;
            }
            else
            {
                if (initObj.PRTGLOB.Party.ejecorr.Trim() != "" || initObj.PRTGLOB.Party.ejecorr.Trim() != null)
                {
                    if (!initObj.PRTGLOB.Party.PrtGlob.FlagParty.ToBool())
                    {
                        initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                        if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                        {
                            initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
                initObj.PRTGLOB.Party.ejecorr = "";
            }

            if (initObj.PrtEnt11.Combo1.ListIndex == -1)
            {
                initObj.PRTGLOB.Party.clasificacion = initObj.PrtEnt11.Combo1.get_ItemData_(initObj.PrtEnt11.Combo1.Items.Count - 1).ToInt();
            }
            else
            {
                initObj.PRTGLOB.Party.clasificacion = initObj.PrtEnt11.Combo1.get_ItemData_(initObj.PrtEnt11.Combo1.ListIndex).ToInt();
            }

            initObj.PRTGLOB.Party.estado = 5;

            initObj.PaginaWebQueAbrir = "index";
        }
        public static void Cancelar_Click(InitializationObject initObj)
        {

        }

        public static void prtcodigo_Change(InitializationObject initObj)
        {
            if (initObj.PRTGLOB.Party.codbco != initObj.PrtEnt11.prtcodigo.Text.ToVal())
                initObj.PrtEnt11.aceptar.Enabled = true;//PRTYENT.setaceptar(initObj.PrtEnt11) != 0;           
            else
                initObj.PrtEnt11.aceptar.Enabled = false;
        }
        public static void prtplaza_Change(InitializationObject initObj)
        {
            if (initObj.PRTGLOB.Party.aladi != initObj.PrtEnt11.prtplaza.Text.ToVal())
            {
                initObj.PrtEnt11.aceptar.Enabled = true;
            }
            else
                initObj.PrtEnt11.aceptar.Enabled = false;
        }
        public static void prtrut_Change(InitializationObject initObj)
        {
            if (initObj.PRTGLOB.Party.rut != "" && initObj.PrtEnt11.prtrut.Text.ToStr() != null)
            {
                initObj.PrtEnt11.aceptar.Enabled = true;
            }
            else
                initObj.PrtEnt11.aceptar.Enabled = false;        
        }
        public static void prtrut_LostFocus(InitializationObject initObj)
        {
            string ll = "";
            ll = initObj.PrtEnt11.prtrut.Text.ToStr();
            ll = PRTYENT.descero(ll); //PRTYENT.filcero(ll, T_PRTGLOB.formato_rut);
           // initObj.PrtEnt11.prtrut.Text = T_PRTGLOB.formato_rut;
            initObj.PrtEnt11.prtrut.Text = PRTYENT.descero(ll);

        }
        public static void prtspread_Change(InitializationObject initObj)
        {
            string spread = "";
            spread = initObj.PrtEnt11.prtspread.Text;
            if (initObj.PRTGLOB.Party.spread != PRTYENT.cambiasepdec(spread).ToVal())
                initObj.PrtEnt11.aceptar.Enabled = true; //PRTYENT.setaceptar(PrtEnt11.DefInstance) != 0;          
            else
                initObj.PrtEnt11.aceptar.Enabled = false;
        }

        private void prtspread_LostFocus(InitializationObject initObj)
        {
            int a = 0;
            // a = UTILES.mascaralost(prtspread);
        }
        public static void prtswif_Change(InitializationObject initObj)
        {
            if (initObj.PRTGLOB.Party.swif != initObj.PrtEnt11.prtswif.Text)
            {
                initObj.PrtEnt11.aceptar.Enabled = true; //PRTYENT.setaceptar(PrtEnt11.DefInstance) != 0;
            }
            else
                initObj.PrtEnt11.aceptar.Enabled = false;
        }
        public static void prttasa_Click(InitializationObject initObj)
        {
            var Index = 0;
            for (int i = 0; i < initObj.PrtEnt11._prttasa_.Count; i++)
            {
                if (initObj.PrtEnt11._prttasa_[i].Selected)
                {
                    Index = i;
                    break;
                }
            }
            switch (Index)
            {
                case 0:
                    if (initObj.PrtEnt11._prttasa_[0].Selected)
                    {
                        if (!initObj.PRTGLOB.Party.libor.ToBool())
                        {
                            initObj.PrtEnt11.aceptar.Enabled = true;
                        }
                        else
                        {
                            initObj.PrtEnt11.aceptar.Enabled = false;
                        }
                    }
                    break;
                case 1:
                    if (initObj.PrtEnt11._prttasa_[1].Selected)
                    {
                        if (!initObj.PRTGLOB.Party.prime.ToBool())
                        {
                            initObj.PrtEnt11.aceptar.Enabled = true;
                        }
                        else
                        {
                            initObj.PrtEnt11.aceptar.Enabled = false;
                        }
                    }
                    break;
                case 2:
                    if (initObj.PrtEnt11._prttasa_[2].Selected)
                    {
                        if (!initObj.PRTGLOB.Party.libor.ToBool() || !initObj.PRTGLOB.Party.prime.ToBool())
                        {
                            initObj.PrtEnt11.aceptar.Enabled = true;
                        }
                        else
                        {
                            initObj.PrtEnt11.aceptar.Enabled = false;
                        }
                    }
                    break;
            }

        }
        public static void prttipob_Click(InitializationObject initObj, string Index)
        {
            //for (int i = 0; i < initObj.PrtEnt11._prttipob_.Count; i++)
            //{
            //    if (initObj.PrtEnt11._prttipob_[i].Value)
            //    {
            //        Index = i;
            //        break;
            //    }
            //}
            switch (Index)
            {
                case "ch_Avisador_Checked": //2                   
                    if (initObj.PrtEnt11._prttipob_[2].Checked == System.Windows.Forms.CheckState.Checked.ToBool())
                    {
                        initObj.PrtEnt11._prttipob_[0].Checked = false; //System.Windows.Forms.CheckState.Unchecked;
                        initObj.PrtEnt11._prttipob_[1].Checked = false;
                        initObj.PrtEnt11._prttipob_[0].Enabled = false;
                        initObj.PrtEnt11._prttipob_[1].Enabled = false;
                    }
                    else
                    {
                        initObj.PrtEnt11._prttipob_[0].Checked = false;
                        initObj.PrtEnt11._prttipob_[1].Checked = false;
                        initObj.PrtEnt11._prttipob_[0].Enabled = true;
                        initObj.PrtEnt11._prttipob_[1].Enabled = true;
                    }
                    break;
                case "ch_Acreedor_Checked": //0
                    if (initObj.PrtEnt11._prttipob_[0].Checked == System.Windows.Forms.CheckState.Checked.ToBool())
                    {
                        initObj.PrtEnt11._prttipob_[2].Enabled = false;
                    }
                    else
                    {                      
                        if (!initObj.PrtEnt11._prttipob_[1].Checked == System.Windows.Forms.CheckState.Unchecked.ToBool())
                        {
                            initObj.PrtEnt11._prttipob_[2].Enabled = true;
                            initObj.PrtEnt11.ejecorr.Enabled = false;
                        }
                    }
                    break;
                case "ch_Corresponsal_Checked": //1
                    if (initObj.PrtEnt11._prttipob_[1].Checked == System.Windows.Forms.CheckState.Checked.ToBool())
                    {
                        initObj.PrtEnt11._prttipob_[2].Enabled = false;
                        initObj.PrtEnt11.ejecorr.Enabled = true;
                    }
                    else
                    {
                        if (initObj.PrtEnt11._prttipob_[0].Checked == System.Windows.Forms.CheckState.Unchecked.ToBool())
                            initObj.PrtEnt11._prttipob_[2].Enabled = true;
                        initObj.PrtEnt11.ejecorr.Enabled = false;
                    }
                    break;
            }
            initObj.PrtEnt11.aceptar.Enabled = true;
        }
    }
}
