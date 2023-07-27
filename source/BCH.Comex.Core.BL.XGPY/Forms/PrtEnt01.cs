using BCH.Comex.Core.BL.XGPY.Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;


namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public class PrtEnt01
    {
        private static int llama0711_ya_llamo = 0;
        private static XgpyService _Service;
        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            int a = 0;
            int[] tabul = null;
            int[] tabu = null;

            initObject.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = 0;
            initObject.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = 0;

            tabu = new int[3];
            tabu[0] = 120;
            tabu[1] = 220;
            tabu[2] = 280;

            tabul = new int[2];
            tabul[0] = 220;
            tabul[1] = 280;

            a = UTILES.seteaTabulador(initObject.PrtEnt01.Listarazones, tabul);
            a = UTILES.seteaTabulador(initObject.PrtEnt01.Listadirec, tabu);
            T_PRTGLOB.otro = "";
            // PRTYENT.deshabilita(initObject);          
            if (T_MODWS.ACCESO == "0")
            {
                // MODWS.VistaConsulta(initObject);
            }
        }

        public static void llama04(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            // si no he leido los memos aun...
            if (initObj.PRTGLOB.Party.PrtGlob.MemosLeidos == 0)
            {
                if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagInstrucciones) == 0) // T_PRTGLOB.GPRT_FlagInstrucciones)
                {
                    // si el flag indica que tiene memos debo leerlos
                    PRTYENT.lee_instrucSy(initObj, uow, initObj.PRTGLOB.Party.idparty, initObj.PRTGLOB.Party.flagins);
                }
                // ok, marcarlos como leidos
                initObj.PRTGLOB.Party.PrtGlob.MemosLeidos = true.ToInt();
            }


        }
        public static void llama06(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            switch (initObj.PRTGLOB.Party.estado)
            {
                case T_PRTGLOB.leido:
                case T_PRTGLOB.modificado:
                    if (initObj.PRTGLOB.Party.Flag > 0 && T_PRTGLOB.Gprt_FlagTasas > 0)
                    {
                        PRTYENT.lee_tcomSy(initObj, uow, initObj.PRTGLOB.Party.idparty);
                        PRTYENT.lee_tintSy(initObj, uow, initObj.PRTGLOB.Party.idparty);
                        PRTYENT.lee_tgasSy(initObj, uow, initObj.PRTGLOB.Party.idparty);
                    }
                    else
                    {
                        T_PRTYENT.offsec = 1;                    
                        initObj.PRTGLOB.tasacom = new prtytcom[0];
                        initObj.PRTGLOB.tasagas = new prtytgas[0];
                        initObj.PRTGLOB.tasaint = new prtytint[0];
                    }
                    break;
                case T_PRTGLOB.nuevo:

                    if (initObj.PRTGLOB.tasacom[0] != null)
                    {
                        if (initObj.PRTGLOB.tasacom[0].sistema == "")
                        {
                            T_PRTYENT.offsec = 1;
                            initObj.PRTGLOB.tasacom = new prtytcom[0];
                        }
                        if (initObj.PRTGLOB.tasagas[0].sistema == "")
                        {
                            initObj.PRTGLOB.tasagas = new prtytgas[0];
                        }
                        if (initObj.PRTGLOB.tasaint[0].sistema == "")
                        {
                            initObj.PRTGLOB.tasaint = new prtytint[0];
                        }
                    }
                    break;
            }

        }
        public static void llama0711(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            if (llama0711_ya_llamo != 0)
            {
                return;
            }
            llama0711_ya_llamo = ((true) ? -1 : 0);

            switch (initObj.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    PRTYENT.llama07(initObj, uow);
                    _Service.CaracteristicasParticipanteInit(initObj);

                    break;
                case T_PRTGLOB.tipo_banco:
                    PRTYENT.llama11(initObj);
                    initObj.PaginaWebQueAbrir = "DatosBancoParticipante";

                    break;
                case T_PRTGLOB.individuo:
                    if (initObj.PRTGLOB.Party.sirut != 0)
                    {
                        initObj.PrtEnt07.prtrut.Text = PRTYENT.descero(initObj.PRTGLOB.Party.rut);
                        initObj.PrtEnt07.prtrut.Enabled = false;
                        initObj.PrtEnt07.prtcliente[0].Enabled = true;
                        initObj.PrtEnt07.prtcliente[1].Enabled = true;
                    }
                    else
                    {
                        initObj.PrtEnt07.prtrut.Text = T_PRTGLOB.formato_rut;
                        initObj.PrtEnt07.prtrut.Enabled = true;
                        initObj.PrtEnt07.prtcliente[0].Enabled = false;
                        initObj.PrtEnt07.prtcliente[1].Enabled = false;
                    }
                    initObj.PrtEnt07.prtcliente[0].Selected = false;
                    //initObj.PrtEnt07.prtcliente[1].Selected = true;                
                    if (initObj.PRTGLOB.Party.oficina != null || initObj.PRTGLOB.Party.ejecutivo != null || initObj.PRTGLOB.Party.actividad != null || initObj.PRTGLOB.Party.riesgo != null)
                    {
                        initObj.PrtEnt07.prtcliente[0].Selected = false;
                        initObj.PrtEnt07.prtcliente[1].Selected = true; //La Lleva 
                        PRTYENT.escribeinfoparty(initObj, uow);
                        initObj.PrtEnt07.oficina.Enabled = true;
                        initObj.PrtEnt07.ejecutivo.Enabled = true;
                        initObj.PrtEnt07.Combo2.Enabled = true;
                        initObj.PrtEnt07.Combo4.Enabled = true;
                        initObj.PrtEnt07.Combo1.ListIndex = 8;
                        initObj.PrtEnt07.Combo1.Enabled = true;
                    }
                    else
                    {
                        initObj.PrtEnt07.prtcliente[0].Selected = true;
                        initObj.PrtEnt07.prtcliente[1].Selected = false;      //La LLeva                 
                        initObj.PrtEnt07.oficina.Enabled = false;
                        initObj.PrtEnt07.ejecutivo.Enabled = false;

                        initObj.PrtEnt07.Combo2.Enabled = false;
                        initObj.PrtEnt07.Combo4.Enabled = false;
                        initObj.PrtEnt07.Combo1.ListIndex = 8;
                        initObj.PrtEnt07.Combo1.Enabled = false;
                    }
                    initObj.PrtEnt07.aceptar.Enabled = false;

                    _Service.CaracteristicasParticipanteInit(initObj);
                    break;
            }

            llama0711_ya_llamo = ((false) ? -1 : 0);

        }

        public static void Timer1_Timer(InitializationObject initObj, UnitOfWorkCext01 unit, int opcion)
        {
            string msgreto = "";
            if (initObj.PrtEnt01.Link.Text != "")
            {

                T_MODWS.MSJRET = initObj.PrtEnt01.Link.Text;
                if (T_MODWS.MSJRET.TrimB().Mid(1, 1) == "~")
                {
                    //System.Windows.Forms.MessageBox.Show("Error. El Webservice no responde. Favor intentar mas tarde.", "", MessageBoxButtons.OK);
                    initObj.PrtEnt01.Link.Text = "";
                  
                    // ------------------ Codigo Nuevo ------------------              
                    switch (opcion)
                    {
                        //case "PRTENT08":
                        case (int)Enums.Pagina.PRTENT08:
                            initObj.PrtEnt08.aceptar.Enabled = true;
                            initObj.PrtEnt08.cancelar.Enabled = true;
                            //MODWS.espera(PrtEnt08.DefInstance, "D");
                            break;
                        //case "PRTENT07":
                        case (int)Enums.Pagina.PRTENT07:
                            initObj.PrtEnt07.aceptar.Enabled = true;
                            initObj.PrtEnt07.cancelar.Enabled = true;
                            //initObj.PrtEnt07.Fr_CliEsp.Enabled = false;
                            PrtEnt07.HabilitaDeshabilitaFr_CliEsp(initObj, false);
                            initObj.PrtEnt07.Combo1.Enabled = false;
                            initObj.PrtEnt07.Combo4.Enabled = false;
                            initObj.PrtEnt07.Combo2.Enabled = false;
                            initObj.PrtEnt07.ejecutivo.Enabled = false;
                            initObj.PrtEnt07.oficina.Enabled = false;
                            //MODWS.espera(PrtEnt07.DefInstance, "D");
                            break;
                    }
                    // ---------------- Fin Codigo Nuevo ----------------

                    if (T_MODWS.FLAGOPEN == "ABIERTO")
                    {
                        //msgreto = initObj.MODWS.MensajeWs("SALIR", "");
                        T_MODWS.FLAGOPEN = "CERRADO";
                    }
                    T_MODWS.RUTTIT = "SALIR";

                    if (T_MODWS.MSJRET.TrimB().Mid(2, 7) == "SINRESP")
                    {
                        //PrtEnt02.DefInstance.prtrazon.Enabled = true;
                        //PrtEnt02.DefInstance.prtfantasia.Enabled = true;
                        //PrtEnt02.DefInstance.prtcontacto.Enabled = true;
                        //PrtEnt02.DefInstance.sort.Enabled = true;
                        //PrtEnt02.DefInstance.cancelar.Enabled = true;
                        //PrtEnt02.DefInstance.prtrazon.Focus();
                    }
                    T_MODWS.MSJRET = "";
                    // --------------------------------------------------
                    // Modificacion Partys online - Realsystems
                    // Fecha: 04-10-2012
                    // ------------------ Codigo Nuevo ------------------
                    //switch (PrtEnt01.DefInstance.Tag.ToStr())
                    switch (opcion)
                    {                     
                        case (int)Enums.Pagina.PRTENT02:
                            //initObj.MODWS.espera(initObj.PrtEnt02, "C");
                            break;                     
                        case (int)Enums.Pagina.PRTENT10:
                            //MODWS.espera(PrtEnt10.DefInstance, "C");
                            break;                  
                        case (int)Enums.Pagina.PRTENT08:
                            //MODWS.espera(PrtEnt08.DefInstance, "C");
                            break;                      
                        case (int)Enums.Pagina.PRTENT07:
                            //MODWS.espera(PrtEnt07.DefInstance, "C");
                            break;
                    }
                    // ---------------- Fin Codigo Nuevo ----------------
                }
                else
                {                   
                    //        If MSJRET = "TIEMPO~" Then
                    // ------------------ Codigo Nuevo ------------------
                    if (T_MODWS.MSJRET.Mid(1, 7) == "TIEMPO~")
                    {
                        // ---------------- Fin Codigo Nuevo ----------------                       
                        T_MODWS.FLAGOPEN = "CERRADO";
                        T_MODWS.RUTTIT = "SALIR";
                        initObj.PrtEnt01.Link.Text = "";
                      
                        switch (opcion)
                        {                         
                            case (int)Enums.Pagina.PRTENT08:
                                initObj.PrtEnt08.aceptar.Enabled = true;
                                initObj.PrtEnt08.cancelar.Enabled = true;
                                //MODWS.espera(PrtEnt08.DefInstance, "D");
                                break;                         
                            case (int)Enums.Pagina.PRTENT07:
                                initObj.PrtEnt07.aceptar.Enabled = true;
                                initObj.PrtEnt07.cancelar.Enabled = true;
                                PrtEnt07.HabilitaDeshabilitaFr_CliEsp(initObj, false);                          
                                initObj.PrtEnt07.Combo1.Enabled = false;
                                initObj.PrtEnt07.Combo4.Enabled = false;
                                initObj.PrtEnt07.Combo2.Enabled = false;
                                initObj.PrtEnt07.ejecutivo.Enabled = false;
                                //MODWS.espera(PrtEnt07.DefInstance, "D");
                                break;
                        }
                        // ---------------- Fin Codigo Nuevo ----------------
                        if (T_MODWS.MSJRET.TrimB().Mid(8, 7) == "SINRESP")
                        {
                            //PrtEnt02.DefInstance.prtrazon.Enabled = true;
                            //PrtEnt02.DefInstance.prtfantasia.Enabled = true;
                            //PrtEnt02.DefInstance.prtcontacto.Enabled = true;
                            //PrtEnt02.DefInstance.sort.Enabled = true;
                            //PrtEnt02.DefInstance.cancelar.Enabled = true;

                        }
                        T_MODWS.MSJRET = "";
                        // --------------------------------------------------
                        // Modificacion Partys online - Realsystems
                        // Fecha: 04-10-2012
                        // ------------------ Codigo Nuevo ------------------
                        //switch (initObj.PrtEnt01.DefInstance.Tag.ToStr())
                        switch (opcion)
                        {                           
                            case (int)Enums.Pagina.PRTENT02:
                                //MODWS.espera(PrtEnt02.DefInstance, "C");
                                break;                           
                            case (int)Enums.Pagina.PRTENT10:                            
                                break;                           
                            case (int)Enums.Pagina.PRTENT08:
                                //MODWS.espera(PrtEnt08.DefInstance, "C");
                                break;                      
                            case (int)Enums.Pagina.PRTENT07:
                                //MODWS.espera(PrtEnt07.DefInstance, "C");
                                break;
                        }
                        // ---------------- Fin Codigo Nuevo ----------------
                    }
                    else
                    {
                        initObj.PrtEnt01.Link.Text = "";
                        MODWS.Carga_datos(initObj, unit,opcion);
                    }
                }
            }
            else
            {
                //if (initObj.PrtEnt01.Tag.ToStr() != "")
                //{
                //    switch (initObj.PrtEnt01.Tag.ToStr())
                //    {
                //        case "PRTENT02":
                //            MODWS.espera(PrtEnt02.DefInstance, "P");
                //            break;
                //        case "PRTENT10":
                //            MODWS.espera(PrtEnt10.DefInstance, "P");
                //            break;
                //        case "PRTENT08":
                //            MODWS.espera(PrtEnt08.DefInstance, "P");
                //            break;
                //        case "PRTENT07":
                //            MODWS.espera(PrtEnt07.DefInstance, "P");
                //            break;
                //    }
                //}

            }

            //if (MODWS.ACCESO == "0")
            //{
            //    PrtEnt03.DefInstance.prtdirec.Enabled = false;
            //    PrtEnt03.DefInstance.Combo1.Enabled = false;
            //    PrtEnt03.DefInstance.Ciudad.Enabled = false;
            //    PrtEnt03.DefInstance.Combo3.Enabled = false;
            //    PrtEnt03.DefInstance.Combo4.Enabled = false;
            //    PrtEnt03.DefInstance.cas_bco.Enabled = false;
            //    PrtEnt03.DefInstance.cas_postal.Enabled = false;
            //    PrtEnt03.DefInstance.Txt_email.Enabled = false;
            //    PrtEnt03.DefInstance.prtfono.Enabled = false;
            //    PrtEnt03.DefInstance.prtfax.Enabled = false;
            //    PrtEnt03.DefInstance.prttelex.Enabled = false;
            //    PrtEnt03.DefInstance.prtcpostal.Enabled = false;
            //    PrtEnt03.DefInstance.Frame1.Enabled = false;
            //    PrtEnt03.DefInstance.aceptar.Enabled = false;
            //    PrtEnt03.DefInstance.otro.Enabled = false;
            //    PrtEnt03.DefInstance.eliminar.Enabled = false;
            //    PrtEnt03.DefInstance.activar.Enabled = false;

            //}

            // ---------------------------------------------
            //  RealSystems - Código Nuevo - Termino
            // ---------------------------------------------

        }




    }
}
