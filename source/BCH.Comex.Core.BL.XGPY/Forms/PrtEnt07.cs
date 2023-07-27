using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Data.DAL.Services;
//using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public class PrtEnt07
    {
        private enum _especialistas { Importacion, Exportacion, Negocio };
        //static int tipo = 0;
        //static int modrut = 0;
        //static string la_ofi = "";
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int X = 0;
            int bien = 0;
            string lis = "";
            T_PRTGLOB PRTGLOB = initObj.PRTGLOB;
            int i = 0;
            int fin = 0;
            int j = 0;

            initObj.PrtEnt07.Combo1.Items.Clear();

            //CLASIFICACION
            foreach (BCH.Comex.Common.UI_Modulos.UI_ComboItem var in Funciones.Lista.Clasificacion())
            {
                initObj.PrtEnt07.Combo1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    Data = var.Data,
                    Value = var.Value
                });
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

            //INICIO ACTIVIDAD ECONOMICA         
            // PRTYENT.lee_actecoSy(initObj, uow); //Se cambio al metodo escribeinfoparty del modulo PRTYENT 12-03-2016

            initObj.PrtEnt07.Combo2.Items.Clear();


            fin = PRTGLOB.acteco.Count();
            for (i = 0; i < fin; i += 1)
            {
                lis = PRTGLOB.acteco[i].nombre; //VB6Helpers.Format(PRTGLOB.acteco[i].codigo, String.Empty) + " - " + PRTGLOB.acteco[i].nombre; // Mod el 22/10/2015
                initObj.PrtEnt07.Combo2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    ID = PRTGLOB.acteco[i].codigo.ToString(),
                    Data = i,
                    Value = lis
                });
            }

            //if (initObj.PRTGLOB.Party.actividad != "" || initObj.PRTGLOB.Party.actividad != null)  //Se agrego el 22/10/2015
            if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.actividad == null ? string.Empty : initObj.PRTGLOB.Party.actividad.ToString().Trim()))  //Se agrego el 12/03/2015 
            {
                for (j = 0; j <= initObj.PrtEnt07.Combo2.Items.Count - 1; j += 1)
                {
                    if (VB6Helpers.Format(initObj.PRTGLOB.acteco[initObj.PrtEnt07.Combo2.get_ItemData_(j).ToInt()].codigo, String.Empty) == initObj.PRTGLOB.Party.actividad.Trim())
                    {
                        initObj.PrtEnt07.Combo2.ListIndex = j;
                        break;
                    }
                }
                if (initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0)
                    initObj.PrtEnt07.Combo2.Enabled = true;
                else
                    initObj.PrtEnt07.Combo2.Enabled = false;
            }
            else
            {
                initObj.PrtEnt07.Combo2.ListIndex = -1;
                initObj.PrtEnt07.Combo2.Enabled = true;
            }

            //FIN ACTIVIDAD ECONOMICA



            //INICIO CLASE RIESGO       
            PRTYENT.lee_riesgoSy(initObj, uow);
            PRTYENT.carga_riesgo(initObj, initObj.PrtEnt07.Combo4, uow);

            if (!string.IsNullOrEmpty(initObj.PRTGLOB.Party.riesgo == null ? string.Empty : initObj.PRTGLOB.Party.riesgo.ToString().Trim()))  //Se agrego el 12/03/2015 
            {
                for (j = 0; j <= initObj.PrtEnt07.Combo4.Items.Count - 1; j += 1)
                {
                    if (initObj.PRTGLOB.riesgo[initObj.PrtEnt07.Combo4.get_ItemData_(j).ToInt()].codigo.Trim() == initObj.PRTGLOB.Party.riesgo)
                    {
                        initObj.PrtEnt07.Combo4.ListIndex = j;
                        break;
                    }
                }
                if (initObj.PRTGLOB.Party.PrtGlob.ctas_eliminadas != 0)
                    initObj.PrtEnt07.Combo4.Enabled = true;
                else
                    initObj.PrtEnt07.Combo4.Enabled = false;
            }
            else
            {
                initObj.PrtEnt07.Combo4.ListIndex = -1;
                initObj.PrtEnt07.Combo4.Enabled = true;
            }
            //FIN CLASE RIESGO       



            initObj.PrtEnt07.la_ofi = initObj.PRTGLOB.Party.oficina;
            initObj.PrtEnt07.modrut = 0;

            //if (bien != 1)
            //{
            X = Sygetn_Ejecutivos(initObj, uow);
            //    X = initObj.PRTYENT.VEjc.Count();

            //}

            bien = SyGetn_CenCos(initObj);
            Carga_CenCos(initObj);

            if ((initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente || initObj.PRTGLOB.Party.tipo == T_PRTGLOB.individuo || initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco) && initObj.PRTYENT.Hab_SGTCliEje.ToBool())
            {
                var rut_Format = initObj.PRTGLOB.Party.rut.TrimStart('0').Substring(0, initObj.PRTGLOB.Party.rut.TrimStart('0').Length - 1) + "-" + initObj.PRTGLOB.Party.rut.TrimStart('0').Substring(initObj.PRTGLOB.Party.rut.TrimStart('0').Length - 1);
                T_PRTYENT.Cliente_SGT = PRTYENT2.Es_Cliente(initObj, rut_Format);  
                if (T_PRTYENT.Cliente_SGT != 0)
                    bien = PRTYENT2.Lee_SgtCliEsp(initObj,uow, initObj.PRTGLOB.Party.rut);
            }

            // antes de escribir sobre los campos de texto los limpiamos en caso de no poder escribir.
            initObj.PrtEnt07.Txt_Imp.Text = string.Empty;
            initObj.PrtEnt07.Txt_Exp.Text = string.Empty;
            initObj.PrtEnt07.Txt_Negocios.Text = string.Empty;

            if ((initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente || initObj.PRTGLOB.Party.tipo == T_PRTGLOB.individuo || initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco) && T_PRTYENT.Cliente_SGT != 0 && initObj.PRTYENT.Hab_SGTCliEje == -1) //T_PRTYENT.Hab_SGTCliEje == 0)
            {               
                for (i = 0; i <= initObj.PRTYENT2.VSGTCliEsp.GetUpperBound(0); i += 1)
                {
                    if (initObj.PRTYENT2.VSGTCliEsp[i] != null)
                    {
                        if (initObj.PRTYENT2.VSGTCliEsp[i].tipo == T_PRTYENT2.EJE_tipopimp)
                            initObj.PrtEnt07.Txt_Imp.Text = initObj.PRTYENT2.VSGTCliEsp[i].ofieje + "-" + initObj.PRTYENT2.VSGTCliEsp[i].codeje + " - " + Obtiene_NomEsp(initObj, initObj.PRTYENT2.VSGTCliEsp[i].ofieje, initObj.PRTYENT2.VSGTCliEsp[i].codeje);
                        else if (initObj.PRTYENT2.VSGTCliEsp[i].tipo == T_PRTYENT2.EJE_tipopexp)
                            initObj.PrtEnt07.Txt_Exp.Text = initObj.PRTYENT2.VSGTCliEsp[i].ofieje + "-" + initObj.PRTYENT2.VSGTCliEsp[i].codeje + " - " + Obtiene_NomEsp(initObj, initObj.PRTYENT2.VSGTCliEsp[i].ofieje, initObj.PRTYENT2.VSGTCliEsp[i].codeje);
                        else if (initObj.PRTYENT2.VSGTCliEsp[i].tipo == T_PRTYENT2.EJE_tipnegoc)
                            initObj.PrtEnt07.Txt_Negocios.Text = initObj.PRTYENT2.VSGTCliEsp[i].ofieje + "-" + initObj.PRTYENT2.VSGTCliEsp[i].codeje + " - " + Obtiene_NomEsp(initObj, initObj.PRTYENT2.VSGTCliEsp[i].ofieje, initObj.PRTYENT2.VSGTCliEsp[i].codeje);

                    }
                }
            }          
            initObj.PrtEnt07.Fr_CliEsp.Enabled = true;
            HabilitaDeshabilitaFr_CliEsp(initObj, true);
          

            //if (T_MODWS.ACCESO == "0")
            if (initObj.UsrEsp.Jerarquia != 1 && initObj.UsrEsp.Tipeje != "N")
            //if (initObj.UsrEsp.Jerarquia == 0)
            {
                initObj.PrtEnt07.cboOficina.Enabled = false;
                initObj.PrtEnt07.ejecutivo.Enabled = false;
                initObj.PrtEnt07.Combo2.Enabled = false;
                initObj.PrtEnt07.Combo4.Enabled = false;
                initObj.PrtEnt07.Combo1.Enabled = false;
                HabilitaDeshabilitaFr_CliEsp(initObj, false);
            }
            else
            {
                initObj.PrtEnt07.cboOficina.Enabled = true;
                initObj.PrtEnt07.ejecutivo.Enabled = true;
                initObj.PrtEnt07.Combo2.Enabled = true;
                initObj.PrtEnt07.Combo4.Enabled = true;
                initObj.PrtEnt07.Combo1.Enabled = true;
                HabilitaDeshabilitaFr_CliEsp(initObj, true);
            }

        }
        public static void HabilitaDeshabilitaFr_CliEsp(InitializationObject initObj, bool opcion)
        {
            initObj.PrtEnt07.Cbo_CenCosImp.Enabled = opcion;
            initObj.PrtEnt07.Cbo_EspecImp.Enabled = opcion;
            initObj.PrtEnt07.Bot_IngImp.Enabled = opcion;
            initObj.PrtEnt07.Bot_EliImp.Enabled = opcion;
            initObj.PrtEnt07.Txt_Imp.Enabled = opcion;

            initObj.PrtEnt07.Cbo_CenCosExp.Enabled = opcion;
            initObj.PrtEnt07.Cbo_EspecExp.Enabled = opcion;
            initObj.PrtEnt07.Bot_IngExp.Enabled = opcion;
            initObj.PrtEnt07.Bot_EliExp.Enabled = opcion;
            initObj.PrtEnt07.Txt_Exp.Enabled = opcion;

            initObj.PrtEnt07.Cbo_CenCosNeg.Enabled = opcion;
            initObj.PrtEnt07.Cbo_EspecNeg.Enabled = opcion;
            initObj.PrtEnt07.Bot_IngNeg.Enabled = opcion;
            initObj.PrtEnt07.Bot_EliNeg.Enabled = opcion;
            initObj.PrtEnt07.Txt_Negocios.Enabled = opcion;

        }

        public static void prtcliente_Click(InitializationObject initObj)
        {
            int X = 0;

            initObj.PrtEnt07.aceptar.Enabled = true;
            if (initObj.PrtEnt07.prtcliente[0].Enabled)
            {
                if (initObj.PrtEnt07.prtcliente[0].Selected)
                {
                    if (initObj.PRTGLOB.Party.tipo == T_PRTGLOB.individuo)
                    {
                        initObj.PrtEnt07.aceptar.Enabled = true;
                        initObj.PrtEnt07.cboOficina.Enabled = true;
                        initObj.PrtEnt07.ejecutivo.Enabled = true;

                        initObj.PrtEnt07.Combo2.Enabled = true;
                        initObj.PrtEnt07.Combo4.Enabled = true;
                        initObj.PrtEnt07.Combo1.Enabled = true;

                        initObj.PrtEnt07.Combo2.ListIndex = -1;
                        initObj.PrtEnt07.Fr_CliEsp.Enabled = true;
                        HabilitaDeshabilitaFr_CliEsp(initObj, true);
                        Carga_CenCos(initObj);
                    }
                }
                else
                {
                    if (initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                    {
                        initObj.PrtEnt07.ejecutivo.Text = "";
                        initObj.PrtEnt07.Combo2.ListIndex = -1;
                        initObj.PrtEnt07.Combo4.ListIndex = -1;
                        initObj.PrtEnt07.aceptar.Enabled = true;
                    }
                    initObj.PrtEnt07.cboOficina.Enabled = false;
                    initObj.PrtEnt07.ejecutivo.Enabled = false;
                    initObj.PrtEnt07.Combo2.Enabled = false;
                    initObj.PrtEnt07.Combo4.Enabled = false;
                    initObj.PrtEnt07.Combo1.ListIndex = 8;
                    initObj.PrtEnt07.Combo1.Enabled = false;
                }
            }
        }

        public static void cliente_Click(InitializationObject initObj)
        {
            initObj.PrtEnt07.aceptar.Enabled = true;

            if (initObj.PrtEnt07.prtcliente[1].Enabled)
            {
                if (initObj.PrtEnt07.prtcliente[1].Selected)
                {
                    if (initObj.PRTGLOB.Party.tipo == T_PRTGLOB.individuo)
                    {
                        initObj.PrtEnt07.aceptar.Enabled = true;
                        initObj.PrtEnt07.cboOficina.Enabled = true;
                        initObj.PrtEnt07.ejecutivo.Enabled = true;
                        initObj.PrtEnt07.Combo2.Enabled = true;
                        initObj.PrtEnt07.Combo4.Enabled = true;
                        initObj.PrtEnt07.Combo1.Enabled = true;

                        initObj.PrtEnt07.Cbo_EspecImp.ListIndex = -1;
                        initObj.PrtEnt07.Cbo_CenCosImp.ListIndex = -1;

                        initObj.PrtEnt07.Cbo_EspecExp.ListIndex = -1;
                        initObj.PrtEnt07.Cbo_CenCosExp.ListIndex = -1;

                        initObj.PrtEnt07.Cbo_EspecNeg.ListIndex = -1;
                        initObj.PrtEnt07.Cbo_CenCosNeg.ListIndex = -1;
                        initObj.PrtEnt07.Fr_CliEsp.Enabled = true;
                        HabilitaDeshabilitaFr_CliEsp(initObj, true);
                    }
                }
                else
                {
                    if (initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                    {
                        initObj.PrtEnt07.ejecutivo.Text = "";
                        initObj.PrtEnt07.Combo2.ListIndex = -1;
                        initObj.PrtEnt07.Combo4.ListIndex = -1;
                        initObj.PrtEnt07.aceptar.Enabled = true;
                    }
                    initObj.PrtEnt07.cboOficina.Enabled = false;
                    initObj.PrtEnt07.ejecutivo.Enabled = false;

                    initObj.PrtEnt07.Combo2.Enabled = false;
                    initObj.PrtEnt07.Combo4.Enabled = false;
                    initObj.PrtEnt07.Combo1.ListIndex = 8;
                    initObj.PrtEnt07.Combo1.Enabled = false;
                }
            }

        }

        public static void ejecutivo_Click(InitializationObject initObj)
        {
            string cod = string.Empty;

            if (initObj.PrtEnt07.ejecutivo.ListIndex == -1)
            {
                return;
            }
            cod = VB6Helpers.Format(initObj.PrtEnt07.ejecutivo.get_ItemData_(initObj.PrtEnt07.ejecutivo.ListIndex), String.Empty);

            if (cod != initObj.PRTGLOB.Party.ejecutivo)
                initObj.PrtEnt07.aceptar.Enabled = true;//PRTYENT.setaceptar(PrtEnt07.DefInstance) != 0;       
            else
                initObj.PrtEnt07.aceptar.Enabled = false;
        }

        public static void oficina_Click(InitializationObject initObj, UnitOfWorkCext01 uow, int idcbOficina)
        {
            // Código de la Oficina seleccionada
            string codigoOficina = idcbOficina.ToString().PadLeft(3, '0');

            if (codigoOficina != initObj.PrtEnt07.la_ofi.ToString().Trim())
            {
                initObj.PrtEnt07.la_ofi = codigoOficina;
                initObj.PrtEnt07.ejecutivo.ListIndex = -1;

                // Cargamos los Ejecutivos segun la Oficina Seleccionada
                PRTYENT.lee_ejecutivosSy(initObj, uow, codigoOficina);
                PRTYENT.carga_ejecutivos(initObj, initObj.PrtEnt07.ejecutivo);
            }

            if (codigoOficina != initObj.PRTGLOB.Party.oficina)
            {
                initObj.PrtEnt07.aceptar.Enabled = true;
            }
            else
            {
                initObj.PrtEnt07.aceptar.Enabled = false;
            }
        }

        public static void Combo2_Click(InitializationObject initObj)
        {
            if (initObj.PrtEnt07.Combo2.ListIndex != -1)
            {
                if (initObj.PRTGLOB.Party.actividad != MigrationSupport.Utils.Format(initObj.PrtEnt07.Combo2.get_ItemData_(initObj.PrtEnt07.Combo2.ListIndex), String.Empty))
                {
                    initObj.PrtEnt07.aceptar.Enabled = true; //PRTYENT.setaceptar(PrtEnt07.DefInstance) != 0;
                }
            }

        }

        public static void Combo4_Click(InitializationObject initObj)
        {

            if (initObj.PrtEnt07.Combo4.ListIndex != -1)
            {
                if (initObj.PRTGLOB.Party.riesgo != MigrationSupport.Utils.Format(initObj.PrtEnt07.Combo4.get_ItemData_(initObj.PrtEnt07.Combo4.ListIndex), String.Empty))
                {
                    initObj.PrtEnt07.aceptar.Enabled = true;//PRTYENT.setaceptar(PrtEnt07.DefInstance) != 0;
                }
            }

        }

        public static void Combo1_Click(InitializationObject initObj)
        {

            if (initObj.PrtEnt07.Combo1.ListIndex == -1)
            {
                return;
            }
            if (initObj.PRTGLOB.Party.clasificacion != initObj.PrtEnt07.Combo1.get_ItemData_(initObj.PrtEnt07.Combo1.ListIndex).ToInt())
            {
                initObj.PrtEnt07.aceptar.Enabled = true;
            }


        }

        private static void Carga_CenCos(InitializationObject initObj)
        {
            int i = 0;
            int n = 0;
            n = initObj.PRTGLOB.CenCos.GetUpperBound(0);
            if (n > 0)
            {
                initObj.PrtEnt07.Cbo_CenCosImp.Items.Clear();
                initObj.PrtEnt07.Cbo_CenCosExp.Items.Clear();
                initObj.PrtEnt07.Cbo_CenCosNeg.Items.Clear();
            }
            for (i = 0; i <= n; i += 1)
            {
                initObj.PrtEnt07.Cbo_CenCosImp.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    ID = i.ToString(),
                    Data = i,
                    Value = initObj.PRTGLOB.CenCos[i].Cent_Costo
                });
                initObj.PrtEnt07.Cbo_CenCosExp.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    ID = i.ToString(),
                    Data = i,
                    Value = initObj.PRTGLOB.CenCos[i].Cent_Costo
                });
                initObj.PrtEnt07.Cbo_CenCosNeg.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                {
                    ID = i.ToString(),
                    Data = i,
                    Value = initObj.PRTGLOB.CenCos[i].Cent_Costo
                });
            }
        }
        private static string Obtiene_NomEsp(InitializationObject initObj, string codofi, string codesp)
        {
            string nombreEspecialista = string.Empty;
            int i = 0;

            // se recorre la lista de ejecutivos
            for (i = 0; i <= initObj.PRTYENT.VEjc.Count(); i++)
            {
                // si encontramos el especialista devolvemos su nombre formateado a minusculas.
                if (initObj.PRTYENT.VEjc[i].codofi == codofi && initObj.PRTYENT.VEjc[i].codejc == codesp)
                {
                    nombreEspecialista = PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);
                    break;
                }
            }

            return nombreEspecialista;
        }
        public static void Aceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int bien = 0;
            string rut = "";
            int si = 0;
            string oficina = string.Empty;
            string prtrut = string.Empty;
            bool EsModificado = false;

            if (initObj.PrtEnt07.prtcliente[0].Selected)
            {

                if(initObj.PrtEnt07.cboOficina.ListIndex == -1 && initObj.PrtEnt07.cboOficina.Enabled)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe seleccionar la oficina del ejecutivo.",
                        Title = T_PRTGLOB.TitDatos,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }

                if (initObj.PrtEnt07.ejecutivo.ListIndex == -1 && initObj.PrtEnt07.ejecutivo.Enabled)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe ingresar ejecutivo de cuentas.",
                        Title = T_PRTGLOB.TitDatos,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }

                if (initObj.PrtEnt07.Combo2.ListIndex == -1 && initObj.PrtEnt07.Combo2.Enabled)
                {

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe ingresar actividad económica.",
                        Title = T_PRTGLOB.TitDatos,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
                if (initObj.PrtEnt07.Combo4.ListIndex == -1 && initObj.PrtEnt07.Combo4.Enabled)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe ingresar clase de riesgo.",
                        Title = T_PRTGLOB.TitDatos,
                        Type = TipoMensaje.Informacion
                    });

                    return;
                }
            }

            //  D.S.B.
            //if (initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
            //{

            //}
       
            if (initObj.PRTGLOB.Party.rut == "" || initObj.PRTGLOB.Party.rut == null)
            {
                prtrut = initObj.PrtEnt07.prtrut.Text == null ? string.Empty : initObj.PrtEnt07.prtrut.Text;
                if (!string.IsNullOrEmpty(prtrut))
                {
                    rut = initObj.PrtEnt07.prtrut.Text;
                    si = PRTYENT.esrut(rut);
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
                    else
                    {
                        initObj.PRTGLOB.Party.rut = PRTYENT.descero(rut); //PRTYENT.filcero(rut, T_PRTGLOB.formato_rut);
                        initObj.PRTGLOB.Party.sirut = 1;
                        if (initObj.PrtEnt07.prtcliente[0].Enabled)
                        {
                            if (initObj.PrtEnt07.prtcliente[0].Selected)
                                initObj.PRTGLOB.Party.tipo = T_PRTGLOB.tipo_cliente;
                            else
                                initObj.PRTGLOB.Party.tipo = T_PRTGLOB.individuo;

                        }
                        if (initObj.PrtEnt07.cboOficina.Enabled)
                            initObj.PRTGLOB.Party.oficina = initObj.PrtEnt07.cboOficina.get_ItemData_(initObj.PrtEnt07.cboOficina.ListIndex.ToInt()).ToString().PadLeft( 3, '0');
                        else
                            initObj.PRTGLOB.Party.oficina = string.Empty;

                        if (initObj.PrtEnt07.ejecutivo.Enabled)
                            initObj.PRTGLOB.Party.ejecutivo = VB6Helpers.Format(initObj.PrtEnt07.ejecutivo.get_ItemData_(initObj.PrtEnt07.ejecutivo.ListIndex.ToInt()), "000"); //initObj.PrtEnt07.ejecutivo.get_ItemData_(initObj.PrtEnt07.ejecutivo.ListIndex).ToString(); //MigrationSupport.Utils.Format(ejecutivo.GetItemData(ejecutivo.SelectedIndex), "000");                   
                        else
                            initObj.PRTGLOB.Party.ejecutivo = string.Empty;

                        if (initObj.PrtEnt07.Combo2.Enabled)
                            initObj.PRTGLOB.Party.actividad = initObj.PrtEnt07.ejecutivo.get_ItemData_(initObj.PrtEnt07.Combo2.ListIndex).ToString();//MigrationSupport.Utils.Format(PRTGLOB.acteco[Combo2.GetItemData(Combo2.SelectedIndex).ToInt()].codigo, String.Empty);                       
                        else
                            initObj.PRTGLOB.Party.actividad = string.Empty;

                        if (initObj.PrtEnt07.Combo4.Enabled)
                            initObj.PRTGLOB.Party.riesgo = initObj.PRTGLOB.riesgo[initObj.PrtEnt07.Combo4.get_ItemData_(initObj.PrtEnt07.Combo4.ListIndex)].codigo;
                        else
                            initObj.PRTGLOB.Party.riesgo = string.Empty;

                    }
                }
                else
                {
                    if (initObj.PrtEnt07.prtcliente[0].Selected)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Se necesita el rut del cliente.",
                            Title = T_PRTGLOB.TitDatos,
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }
                }
            }
            else
            {
                if (initObj.PrtEnt07.prtcliente[0].Enabled)
                {
                    if (initObj.PrtEnt07.prtcliente[0].Selected)
                        initObj.PRTGLOB.Party.tipo = T_PRTGLOB.tipo_cliente;
                    else
                        initObj.PRTGLOB.Party.tipo = T_PRTGLOB.individuo;
                }
                if (initObj.PrtEnt07.cboOficina.Enabled)
                {
                    if(!string.IsNullOrEmpty(initObj.PrtEnt07.cboOficina.Text))
                        initObj.PRTGLOB.Party.oficina = initObj.PrtEnt07.cboOficina.get_ItemData_(initObj.PrtEnt07.cboOficina.ListIndex.ToInt()).ToString().PadLeft(3, '0');
                    else
                        initObj.PRTGLOB.Party.oficina = string.Empty;
                }
                if (initObj.PrtEnt07.ejecutivo.Enabled)
                {
                    if (!string.IsNullOrEmpty(initObj.PrtEnt07.ejecutivo.Text))
                        initObj.PRTGLOB.Party.ejecutivo = VB6Helpers.Format(initObj.PrtEnt07.ejecutivo.get_ItemData_(initObj.PrtEnt07.ejecutivo.ListIndex.ToInt()), "000");// "0";//MigrationSupport.Utils.Format(ejecutivo.GetItemData(ejecutivo.SelectedIndex), "000");
                    else
                        initObj.PRTGLOB.Party.ejecutivo = string.Empty;

                }

                if (initObj.PrtEnt07.Combo2.Enabled)
                {
                    if (initObj.PrtEnt07.Combo2.ListIndex >= 0)
                        initObj.PRTGLOB.Party.actividad = VB6Helpers.Format(initObj.PRTGLOB.acteco[initObj.PrtEnt07.Combo2.get_ItemData_(initObj.PrtEnt07.Combo2.ListIndex).ToInt()].codigo, String.Empty);//"0";//MigrationSupport.Utils.Format(PRTGLOB.acteco[Combo2.GetItemData(Combo2.SelectedIndex).ToInt()].codigo, String.Empty);                 
                    else
                        initObj.PRTGLOB.Party.actividad = string.Empty;

                }
                if (initObj.PrtEnt07.Combo4.Enabled)
                {
                    if (initObj.PrtEnt07.Combo4.ListIndex >= 0)
                        initObj.PRTGLOB.Party.riesgo = initObj.PRTGLOB.riesgo[initObj.PrtEnt07.Combo4.get_ItemData_(initObj.PrtEnt07.Combo4.ListIndex)].codigo;
                    else
                        initObj.PRTGLOB.Party.riesgo = string.Empty;
                }
            }

            if (initObj.PrtEnt07.Combo1.ListIndex == -1)
            {
                initObj.PRTGLOB.Party.clasificacion = initObj.PrtEnt07.Combo1.get_ItemData_(initObj.PrtEnt07.Combo1.Items.Count - 1);
            }
            else
            {
                if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.nuevo)
                {
                    initObj.PRTGLOB.Party.clasificacion = initObj.PrtEnt07.Combo1.get_ItemData_(initObj.PrtEnt07.Combo1.ListIndex);
                }
                else
                {
                    if (initObj.PRTGLOB.Party.clasificacion != initObj.PrtEnt07.Combo1.get_ItemData_(initObj.PrtEnt07.Combo1.ListIndex))
                    {
                        initObj.PRTGLOB.Party.clasificacion = initObj.PrtEnt07.Combo1.get_ItemData_(initObj.PrtEnt07.Combo1.ListIndex);
                        if (!(Convert.ToBoolean(initObj.PRTGLOB.Party.PrtGlob.FlagParty)))
                        {
                            initObj.PRTGLOB.Party.PrtGlob.FlagParty = true.ToInt();
                            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }
                    }
                }
            }

            if (!Convert.ToBoolean(initObj.PRTGLOB.Party.PrtGlob.FlagParty))
            {
                initObj.PRTGLOB.Party.PrtGlob.FlagParty = 1;
                if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                    initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
            }

            if ((initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente) && Convert.ToBoolean(initObj.PRTYENT.Hab_SGTCliEje))
            {
                var rut_Format = initObj.PRTGLOB.Party.rut.TrimStart('0').Substring(0, initObj.PRTGLOB.Party.rut.TrimStart('0').Length - 1) + "-" + initObj.PRTGLOB.Party.rut.TrimStart('0').Substring(initObj.PRTGLOB.Party.rut.TrimStart('0').Length - 1);
                T_PRTYENT.Cliente_SGT = PRTYENT2.Es_Cliente(initObj, rut_Format);    //PRTYENT2.Es_Cliente(initObj.PRTGLOB.Party.rut.Mid(1, 9));
                if (T_PRTYENT.Cliente_SGT != 0)
                    bien = PRTYENT2.Lee_SgtCliEsp(initObj, uow, initObj.PRTGLOB.Party.rut);
            }

            if (initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente || initObj.PRTGLOB.Party.tipo == T_PRTGLOB.individuo || initObj.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
            {
                //  D.S.B.
                if (initObj.PRTYENT.Hab_SGTCliEje != 0)
                    Guarda_SgtCliEsp(initObj);
                if (initObj.PRTGLOB.Party.PrtGlob.Pertenece != 0)
                    PRTYENT.habilita(initObj);

            }
            else
            {
                if (initObj.PRTGLOB.Party.PrtGlob.Pertenece != 0)
                    PRTYENT.habilitaindiv(initObj);
            }

            initObj.PRTGLOB.Party.estado = 5;

            initObj.PaginaWebQueAbrir = "Index";
        }

        private static void VerificaExisteCambios(InitializationObject initObj)
        {
            int SeHizoCambios = 0;

            if (initObj.PRTGLOB.Party.oficina.Trim() != initObj.PrtEnt07.cboOficina.get_ItemData_(initObj.PrtEnt07.ejecutivo.ListIndex.ToInt()).ToString().PadLeft(3, '0'))
                SeHizoCambios++;

            if (initObj.PRTGLOB.Party.ejecutivo.Trim() != initObj.PrtEnt07.ejecutivo.get_ItemData_(initObj.PrtEnt07.ejecutivo.ListIndex.ToInt()).ToString().PadLeft(3, '0'))
                SeHizoCambios++;

            if (initObj.PRTGLOB.Party.actividad.Trim() != VB6Helpers.Format(initObj.PRTGLOB.acteco[initObj.PrtEnt07.Combo2.get_ItemData_(initObj.PrtEnt07.Combo2.ListIndex).ToInt()].codigo, String.Empty))
                SeHizoCambios++;

            if (SeHizoCambios > 0)
                initObj.PRTGLOB.Party.estado = 5;

        }
        public static void Cbo_CenCosImp_Click(InitializationObject initObj)
        {
            string oficina = string.Empty;
            if (initObj.PrtEnt07.Cbo_CenCosImp.ListIndex > -1)
                oficina = initObj.PrtEnt07.Cbo_CenCosImp.Items[initObj.PrtEnt07.Cbo_CenCosImp.ListIndex].Value;
            else
                oficina = "Seleccione";
            Pr_Cargar_Espec(initObj, initObj.PrtEnt07.Cbo_EspecImp, oficina, T_PRTYENT.EJCOPIMP);
        }
        public static void Cbo_CenCosExp_Click(InitializationObject initObj)
        {

            string oficina = string.Empty;
            //oficina = initObj.PrtEnt07.Cbo_CenCosExp.Text;
            if (initObj.PrtEnt07.Cbo_CenCosExp.ListIndex > -1)
                oficina = initObj.PrtEnt07.Cbo_CenCosExp.Items[initObj.PrtEnt07.Cbo_CenCosExp.ListIndex].Value;
            else
                oficina = "Seleccione";
            Pr_Cargar_Espec(initObj, initObj.PrtEnt07.Cbo_EspecExp, oficina, T_PRTYENT.EJCOPEXP);

        }
        public static void Cbo_CenCosNeg_Click(InitializationObject initObj)
        {
            string oficina = string.Empty;
            if (initObj.PrtEnt07.Cbo_CenCosNeg.ListIndex > -1)
                oficina = initObj.PrtEnt07.Cbo_CenCosNeg.Items[initObj.PrtEnt07.Cbo_CenCosNeg.ListIndex].Value;
            else
                oficina = "Seleccione";
            Pr_Cargar_Espec(initObj, initObj.PrtEnt07.Cbo_EspecNeg, oficina, T_PRTYENT.EJCNEGOC);

        }
        private static void Pr_Cargar_Espec(InitializationObject initObj, BCH.Comex.Common.UI_Modulos.UI_Combo Cbo_Us, string oficina, string tipejc)
        {
            string s = "";
            int i = 0;
            int n = 0;
            // carga Lista Cbo_Us           
            n = initObj.PRTYENT.VEjc.GetUpperBound(0);
            Cbo_Us.Clear();
            if (n > 0)
            {
                for (i = 0; i <= n; i += 1)
                {
                    if (initObj.PRTYENT.VEjc[i].codofi == oficina && initObj.PRTYENT.VEjc[i].tipo == tipejc)
                    {
                        s = "";
                        s = s + MigrationSupport.Utils.Format(initObj.PRTYENT.VEjc[i].codofi, "000") + "-";
                        s = s + MigrationSupport.Utils.Format(initObj.PRTYENT.VEjc[i].codejc, "000") + " : ";
                        s = s + PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);
                        Cbo_Us.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                ID = i.ToString(),
                                Data = i,
                                Value = s
                            });
                    }
                }

                if (Cbo_Us.ListCount == 0)
                {
                    Cbo_Us.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                    {
                        ID = "-1",
                        Data = -1,
                        Value = "--Seleccione--"
                    });
                }


            }

        }
        public static void actividad_LostFocus(InitializationObject initObj)
        {
            string nome = "";
            double cod = 0.0;

            initObj.PrtEnt07.actividad.MaxLength = 0;
            cod = initObj.PrtEnt07.actividad.Text.ToVal();
            nome = PRTYENT.nom_act(initObj, cod);

            if (nome == "")
            {
                initObj.PrtEnt07.actividad.Text = string.Empty;
                initObj.PrtEnt07.actividad.Tag = string.Empty;
                return;
            }
            else
            {
                initObj.PrtEnt07.actividad.Text = nome;
                initObj.PrtEnt07.actividad.Tag = MigrationSupport.Utils.Format(cod, String.Empty);

                if (initObj.PrtEnt07.actividad.Tag.ToStr() != initObj.PRTGLOB.Party.actividad)
                {
                    initObj.PrtEnt07.aceptar.Enabled = true; //PRTYENT.setaceptar(PrtEnt07.DefInstance) != 0;
                }
            }
        }
        public static void Bot_EliExp_Click(InitializationObject initObj)
        {
            initObj.PrtEnt07.Txt_Exp.Text = string.Empty;
            initObj.PrtEnt07.Cbo_EspecExp.ListIndex = -1;
            initObj.PrtEnt07.Cbo_CenCosExp.ListIndex = -1;
            initObj.PrtEnt07.aceptar.Enabled = true;
        }
        public static void Bot_EliImp_Click(InitializationObject initObj)
        {
            initObj.PrtEnt07.Txt_Imp.Text = string.Empty;
            initObj.PrtEnt07.Cbo_EspecImp.ListIndex = -1;
            initObj.PrtEnt07.Cbo_CenCosImp.ListIndex = -1;
            initObj.PrtEnt07.aceptar.Enabled = true;
        }
        public static void Bot_EliNeg_Click(InitializationObject initObj)
        {
            initObj.PrtEnt07.Txt_Negocios.Text = string.Empty;
            initObj.PrtEnt07.Cbo_EspecNeg.ListIndex = -1;
            initObj.PrtEnt07.Cbo_CenCosNeg.ListIndex = -1;
            initObj.PrtEnt07.aceptar.Enabled = true;
        }
        public static void Bot_IngExp_Click(InitializationObject initObj)
        {
            int i = 0;
            string CenCos = string.Empty;
            int bien = 0;

            bien = Valida_Esp(initObj, initObj.PrtEnt07.Cbo_CenCosExp, initObj.PrtEnt07.Cbo_EspecExp, _especialistas.Exportacion);

            if (bien != 0)
            {
                i = initObj.PrtEnt07.Cbo_EspecExp.get_ItemData_(initObj.PrtEnt07.Cbo_EspecExp.ListIndex);
                CenCos = initObj.PRTYENT.VEjc[i].codofi;
                //initObj.PrtEnt07.Txt_Exp.Text = CenCos + "-" + initObj.PRTYENT.VEjc[i].codejc.ToString("000") + " - " + PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);
                initObj.PrtEnt07.Txt_Exp.Text = CenCos + "-" + initObj.PRTYENT.VEjc[i].codejc + " - " + PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);

                initObj.PrtEnt07.aceptar.Enabled = true;
            }
        }
        public static void Bot_IngImp_Click(InitializationObject initObj)
        {
            int i = 0;
            string CenCos = string.Empty;
            int bien = 0;
            bien = Valida_Esp(initObj, initObj.PrtEnt07.Cbo_CenCosImp, initObj.PrtEnt07.Cbo_EspecImp, _especialistas.Importacion);
            if (bien != 0)
            {
                i = initObj.PrtEnt07.Cbo_EspecImp.get_ItemData_(initObj.PrtEnt07.Cbo_EspecImp.ListIndex);
                CenCos = initObj.PRTYENT.VEjc[i].codofi;
                initObj.PrtEnt07.Txt_Imp.Text = CenCos + "-" + initObj.PRTYENT.VEjc[i].codejc + " - " + PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);
                initObj.PrtEnt07.aceptar.Enabled = true;
            }
        }
        public static void Bot_IngNeg_Click(InitializationObject initObj)
        {
            int i = 0;
            string CenCos = string.Empty;
            int bien = 0;
            bien = Valida_Esp(initObj, initObj.PrtEnt07.Cbo_CenCosNeg, initObj.PrtEnt07.Cbo_EspecNeg, _especialistas.Negocio);
            if (bien != 0)
            {
                i = initObj.PrtEnt07.Cbo_EspecNeg.get_ItemData_(initObj.PrtEnt07.Cbo_EspecNeg.ListIndex);
                CenCos = initObj.PRTYENT.VEjc[i].codofi;
                initObj.PrtEnt07.Txt_Negocios.Text = CenCos + "-" + initObj.PRTYENT.VEjc[i].codejc + " - " + PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);
                initObj.PrtEnt07.aceptar.Enabled = true;
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {

            return;

        }

        private static int Valida_Esp(InitializationObject initObj, BCH.Comex.Common.UI_Modulos.UI_Combo CBO_CenCos, BCH.Comex.Common.UI_Modulos.UI_Combo Cbo_Espec, _especialistas especialista)
        {
            int Valida_Esp = 0;
            Valida_Esp = 1;
            if (CBO_CenCos.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe seleccionar Centro de Costo.",
                    Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                    Type = TipoMensaje.Informacion,
                    ControlName = especialista == _especialistas.Importacion ? "CbCenCosImportacion_SelectedValue" 
                                  : especialista == _especialistas.Exportacion ? "CbCenCosExportacion_SelectedValue"
                                  : "CbCenCosNegocio_SelectedValue"
                });
                Valida_Esp = 0;
                return Valida_Esp;
            }
            else if (Cbo_Espec.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe seleccionar Especialista.",
                    Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                    Type = TipoMensaje.Informacion,
                    ControlName = especialista == _especialistas.Importacion ? "CbEspecImportacion_SelectedValue"
                                  : especialista == _especialistas.Exportacion ? "CbEspecExportacion_SelectedValue"
                                  : "CbEspecNegocio_SelectedValue"
                });
                Valida_Esp = 0;
                return Valida_Esp;
            }
            return Valida_Esp;
        }
        private static int SyGetn_CenCos(InitializationObject initObj)
        {
            int SyGetn_CenCos = false.ToInt();
            int fin = 0;
            int i = 0;
            try
            {
                IEnumerable<string> listaCentroCosto = null;
                initObj.PRTGLOB.CenCos = new T_Cencos[0];
                fin = initObj.PRTYENT.VEjc.GetUpperBound(0);
                listaCentroCosto = initObj.PRTYENT.VEjc.Select(c => c.codofi).Distinct();
                initObj.PRTGLOB.CenCos = new T_Cencos[listaCentroCosto.Count()];
                i = 0;
                foreach (var centro in listaCentroCosto)
                {
                    initObj.PRTGLOB.CenCos[i++] = new T_Cencos()
                    {
                        Cent_Costo = centro
                    };
                }
                SyGetn_CenCos = true.ToInt();

                return SyGetn_CenCos;
            }
            catch (Exception exc)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Error al leer en Sce_Usr.",
                    Title = T_PRTGLOB.TitDatos,
                    Type = TipoMensaje.Informacion
                });
                SyGetn_CenCos = 0;
            }
            return SyGetn_CenCos;
        }
        
        public static void listarOficinas_CodNom(InitializationObject initObj)
        {
            PRTYENT.carga_oficinaCodNom(initObj, initObj.PrtEnt07.cboOficina);

        }

        private static void Guarda_SgtCliEsp(InitializationObject initObj)
        {
            int h = 0;
            //int R = 0;
            initObj.PRTYENT2.VSGTCliEsp = new CliEsp[0];
            h = 0;
            if (!string.IsNullOrEmpty(initObj.PrtEnt07.Txt_Imp.Text))
            {
                Array.Resize(ref initObj.PRTYENT2.VSGTCliEsp, h + 1);
                initObj.PRTYENT2.VSGTCliEsp[h] = new CliEsp();

                initObj.PRTYENT2.VSGTCliEsp[h].nrut = initObj.PRTGLOB.Party.rut.Mid(1, 9);
                initObj.PRTYENT2.VSGTCliEsp[h].tipo = T_PRTYENT2.EJE_tipopimp;
                initObj.PRTYENT2.VSGTCliEsp[h].ofieje = initObj.PrtEnt07.Txt_Imp.Text.Mid(1, 3);
                initObj.PRTYENT2.VSGTCliEsp[h].codeje = initObj.PrtEnt07.Txt_Imp.Text.Mid(5, 3);
                initObj.PRTYENT2.VSGTCliEsp[h].feccre = "";
                initObj.PRTYENT2.VSGTCliEsp[h].estado = T_PRTGLOB.nuevo;

                //R = initObj.PRTYENT2.AISGetRutUsr(T_PRTYENT2.RutwAis);
                initObj.PRTYENT2.VSGTCliEsp[h].rutope = string.Empty;
                initObj.PRTYENT2.VSGTCliEsp[h].drutope = string.Empty;
                if (!string.IsNullOrEmpty(T_PRTYENT2.RutwAis))
                {
                    initObj.PRTYENT2.VSGTCliEsp[h].rutope = VB6Helpers.Format(T_PRTYENT2.RutwAis, "000000000").Mid(1, 8);
                    initObj.PRTYENT2.VSGTCliEsp[h].drutope = VB6Helpers.Format(T_PRTYENT2.RutwAis, "000000000").Mid(9, 1);
                }

                initObj.PRTYENT2.VSGTCliEsp[h].filler = new string(' ', 35);
                h = h + 1;
            }

            if (!string.IsNullOrEmpty(initObj.PrtEnt07.Txt_Exp.Text))
            {
                Array.Resize(ref initObj.PRTYENT2.VSGTCliEsp, h + 1);
                initObj.PRTYENT2.VSGTCliEsp[h] = new CliEsp();

                initObj.PRTYENT2.VSGTCliEsp[h].nrut = initObj.PRTGLOB.Party.rut.Mid(1, 9);
                initObj.PRTYENT2.VSGTCliEsp[h].tipo = T_PRTYENT2.EJE_tipopexp;
                initObj.PRTYENT2.VSGTCliEsp[h].ofieje = initObj.PrtEnt07.Txt_Exp.Text.Mid(1, 3);
                initObj.PRTYENT2.VSGTCliEsp[h].codeje = initObj.PrtEnt07.Txt_Exp.Text.Mid(5, 3);
                initObj.PRTYENT2.VSGTCliEsp[h].feccre = "";
                //R = T_PRTYENT2.AISGetRutUsr(T_PRTYENT2.RutwAis);
                initObj.PRTYENT2.VSGTCliEsp[h].rutope = string.Empty;
                initObj.PRTYENT2.VSGTCliEsp[h].drutope = string.Empty;
                initObj.PRTYENT2.VSGTCliEsp[h].estado = T_PRTGLOB.nuevo;

                if (!string.IsNullOrEmpty(T_PRTYENT2.RutwAis))
                {
                    initObj.PRTYENT2.VSGTCliEsp[h].rutope = VB6Helpers.Format(T_PRTYENT2.RutwAis, "000000000").Mid(1, 8);
                    initObj.PRTYENT2.VSGTCliEsp[h].drutope = VB6Helpers.Format(T_PRTYENT2.RutwAis, "000000000").Mid(9, 1);
                }
                initObj.PRTYENT2.VSGTCliEsp[h].filler = new string(' ', 35);
                h = h + 1;
            }

            if (!string.IsNullOrEmpty(initObj.PrtEnt07.Txt_Negocios.Text))
            {
                Array.Resize(ref initObj.PRTYENT2.VSGTCliEsp, h + 1);
                initObj.PRTYENT2.VSGTCliEsp[h] = new CliEsp();

                initObj.PRTYENT2.VSGTCliEsp[h].nrut = initObj.PRTGLOB.Party.rut.Mid(1, 9);
                initObj.PRTYENT2.VSGTCliEsp[h].tipo = T_PRTYENT2.EJE_tipnegoc;
                initObj.PRTYENT2.VSGTCliEsp[h].ofieje = initObj.PrtEnt07.Txt_Negocios.Text.Mid(1, 3);
                initObj.PRTYENT2.VSGTCliEsp[h].codeje = initObj.PrtEnt07.Txt_Negocios.Text.Mid(5, 3);
                initObj.PRTYENT2.VSGTCliEsp[h].feccre = "";
                //R = T_PRTYENT2.AISGetRutUsr(T_PRTYENT2.RutwAis);
                initObj.PRTYENT2.VSGTCliEsp[h].rutope = string.Empty;
                initObj.PRTYENT2.VSGTCliEsp[h].drutope = string.Empty;
                initObj.PRTYENT2.VSGTCliEsp[h].estado = T_PRTGLOB.nuevo;

                if (!string.IsNullOrEmpty(T_PRTYENT2.RutwAis))
                {
                    initObj.PRTYENT2.VSGTCliEsp[h].rutope = VB6Helpers.Format(T_PRTYENT2.RutwAis, "000000000").Mid(1, 8);
                    initObj.PRTYENT2.VSGTCliEsp[h].drutope = VB6Helpers.Format(T_PRTYENT2.RutwAis, "000000000").Mid(9, 1);
                }

                initObj.PRTYENT2.VSGTCliEsp[h].filler = new string(' ', 35);
                h = h + 1;

            }
        }


        private static int Sygetn_Ejecutivos(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int Sygetn_Ejecutivos = 0;

            bool Leyo_Ok = false;
            //int i = 0;
            int n = 0;
            string MsgUsr1 = "";
            string R = "";
            string Que = "";

            try
            {


                var dbSelectPrtyEsp = uow.SceRepository.Sgt_ejc_S03_MS(T_PRTYENT.EJCOPIMP, T_PRTYENT.EJCOPEXP, T_PRTYENT.EJCNEGOC);
                initObj.PRTYENT.VEjc = new T_Especialista[dbSelectPrtyEsp.Count];
                for (int i = 0; i < dbSelectPrtyEsp.Count; i++)
                {
                    initObj.PRTYENT.VEjc[i] = new T_Especialista();
                    initObj.PRTYENT.VEjc[i].codofi = string.Format("{0:000}", dbSelectPrtyEsp[i].ejc_ejcofi); //Convert.ToString(dbSelectPrtyEsp[i].ejc_ejcofi); // Se agrega formato "000" a la izquierda, segun fix reportados
                    initObj.PRTYENT.VEjc[i].codejc = string.Format("{0:000}", dbSelectPrtyEsp[i].ejc_ejccod); //Convert.ToString(dbSelectPrtyEsp[i].ejc_ejccod); // Se agrega formato "000" a la izquierda, segun fix reportados
                    initObj.PRTYENT.VEjc[i].rut = dbSelectPrtyEsp[i].ejc_ejcrut;
                    initObj.PRTYENT.VEjc[i].nombre = dbSelectPrtyEsp[i].ejc_ejcnom;
                    initObj.PRTYENT.VEjc[i].tipo = dbSelectPrtyEsp[i].ejc_ejctpo;
                }

                Sygetn_Ejecutivos = true.ToInt();

                return Sygetn_Ejecutivos;

            }
            catch (Exception exc)
            {
                //MigrationSupport.GlobalException.Initialize(exc);
                //System.Windows.Forms.MessageBox.Show("Error al leer en Sgt_Ejc: [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescrption(
                //   MigrationSupport.GlobalException.Instance.Number), "", MessageBoxButtons.OK);
                Leyo_Ok = false;


            }
            return Sygetn_Ejecutivos;
        }








    }
}
