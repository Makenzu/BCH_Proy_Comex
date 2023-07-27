using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Forms;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class Module1
    {

        public static T_Module1 Getmodule1() {
            T_Module1 Module1 = new T_Module1();
            return Module1;
        }

        //Captura Partys o incribe pegados a la operacion
        /// <summary>
        /// Usar GetPary1_2 y GetParty2_2
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="DesHabilita"></param>
        /// <param name="Iniciar"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static short GetParty(string[] Tabla, short DesHabilita, short Iniciar, InitializationObject initObj,
            UnitOfWorkCext01 uow)
        {
            string LasMarcas = T_Module1.GPrt_MarcaRequerido + T_Module1.GPrt_MarcaOtros + T_Module1.GPrt_MarcaBanco;
            short Retorno = 0;
            short i = 0;
            short CtaPartys = 0;
            short LimSup = 0;
            short HayEnOperacion = 0;
            short Superior = 0;
            short j = 0;
            short PrtCambio = 0;
            string abc = "";
            string TablaAnt = "";
            string LlaveAnt = "";


            initObj.Frm_Participantes = new UI_Frm_Participantes();

            initObj.Module1.PrtControl.NoOperacion = DesHabilita;

            //Verifico si debo leer parametros
            if (initObj.Module1.PrtControl.Leidos == 0)
            {
                Retorno = VB6Helpers.CShort(LeeParametrosParty(initObj.Module1));
                if (Retorno != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message { Text = T_Module1.GPrt_NoPath, Type = TipoMensaje.Informacion, Title = T_Module1.GPrt_GetParty });

                    return (short)(false ? -1 : 0);
                }
            }

            //Debemos inicializar, dimensionar Partys y resetear modifico
            if (Iniciar != 0)
            {
                Retorno = ResetParty(initObj.Module1, Tabla);
                if (Retorno != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message { Text = T_Module1.GPrt_NoPath, Type = TipoMensaje.Informacion, Title = T_Module1.GPrt_GetParty });

                    return (short)(false ? -1 : 0);
                }
            }

            //preparo datos para operar
            initObj.Module1.Partys = new PartyKey[initObj.Module1.PrtControl.LimSup + 1];
            for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
            {
                if (string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]) && string.IsNullOrEmpty(Tabla[i]))
                {
                    initObj.Module1.PrtTbl[i] = Tabla[i];
                }
                if (!string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]) && string.IsNullOrEmpty(Tabla[i]))
                {
                    initObj.Module1.PrtTbl[i] = "";
                }

                initObj.Module1.Partys[i] = initObj.Module1.PartysOpe[i];
                if (initObj.Module1.Partys[i].Status != T_Module1.GPrt_StatVacio)
                {
                    CtaPartys = (short)(CtaPartys + 1);
                }

            }

            //preparo Pope para operar
            LimSup = -1;
            
            LimSup = (short)VB6Helpers.UBound(initObj.Module1.PopeOpe);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (LimSup >= 0)
            {
                //tiene datos
                initObj.Module1.Pope = new PartysPope[LimSup + 1];
                for (i = 0; i <= (short)LimSup; i++)
                {
                    initObj.Module1.Pope[i] = initObj.Module1.PopeOpe[i];
                }

            }

            //salvo numero operacion
            initObj.Module1.PrtControl.NumOpe = initObj.Module1.Codop;

            //reseteo los cambios
            initObj.Module1.PrtControl.Cambios = "";
            initObj.Module1.PrtControl.PorInsertar = 0;

            //TODO:@Estanislao - Resuelto: ver donde mover esto   ES MODAL!
            //Parametros leidos, invoco el formulario de ingreso
            //Frm_Participantes.DefInstance.Show(1);
            Frm_Participantes.Form_Load(initObj, uow);

            MODXORI.SyGetn_Codtran(initObj.MODXORI, uow);

            if (initObj.Frm_Participantes.Aceptar.Tag != null && (initObj.Frm_Participantes.Aceptar.Tag as int?) != 0)
            {
                //acepto
                initObj.Module1.PrtControl.Leidos = (short)(initObj.Module1.PrtControl.Leidos + 1);  //otra vez
                initObj.Module1.PrtControl.Otros = (short)(initObj.Module1.PrtControl.Otros - initObj.Module1.PrtControl.PorInsertar);
                initObj.Module1.PrtControl.Insertado = (short)(initObj.Module1.PrtControl.Insertado + initObj.Module1.PrtControl.PorInsertar);

                if (~initObj.Module1.PrtControl.Modifico != 0)
                {
                    //si es falso verifico
                    for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
                    {

                        //ver si cambio glosas en la tabla
                        if (string.IsNullOrEmpty(Tabla[i]) && !string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]))
                        {
                            initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                            break;
                        }

                        if (initObj.Module1.Partys[i].Ubicacion == T_Module1.GPrt_EnParty)
                        {
                            //ver cambios en party
                            //cambios de status o llaves ==> modifico
                            if (initObj.Module1.Partys[i].Status != initObj.Module1.PartysOpe[i].Status || initObj.Module1.Partys[i].LlaveArchivo != initObj.Module1.PartysOpe[i].LlaveArchivo)
                            {
                                initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                                break;
                            }

                            //cambios de IndNombre o IndDireccion ==> modifico
                            if (initObj.Module1.Partys[i].IndNombre != initObj.Module1.PartysOpe[i].IndNombre || initObj.Module1.Partys[i].IndDireccion != initObj.Module1.PartysOpe[i].IndDireccion)
                            {
                                initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                                break;
                            }

                        }
                        else
                        {
                            HayEnOperacion = (short)(true ? -1 : 0);
                        }
                    }

                    //si no hay modificacion y HayEnOperacion, verifico cambios
                    if ((~initObj.Module1.PrtControl.Modifico & HayEnOperacion) != 0)
                    {
                        Superior = -1;
                        
                        Superior = (short)VB6Helpers.UBound(initObj.Module1.Pope);

                        for (j = 0; j <= (short)Superior; j++)
                        {
                            if (initObj.Module1.Pope[j].Status != T_Module1.GPrt_StatVacio && initObj.Module1.Pope[j].Status != T_Module1.GPrt_StatIntacto)
                            {
                                initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                                break;
                            }

                        }

                    }

                }

                //acepto los cambio de PartysOpe
                CtaPartys = 0;
                for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
                {
                    PrtCambio = (short)(false ? -1 : 0);
                    abc = VB6Helpers.Left(initObj.Module1.PrtTbl[i], 1);  //busco marca

                    if((VB6Helpers.Instr(1, LasMarcas, abc) & (!string.IsNullOrEmpty(abc) ? -1 : 0)) != 0)
                    {
                        //tiene marca
                        //si es marca de otros
                        if (abc == T_Module1.GPrt_MarcaOtros && Tabla[i] != VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2))
                        {
                            PrtCambio = (short)(true ? -1 : 0);
                            TablaAnt = Tabla[i];
                            Tabla[i] = VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2);
                        }

                        //saco las marcas a glosas que no puedo modificar
                        if (abc != T_Module1.GPrt_MarcaOtros)
                        {
                            Tabla[i] = VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2);
                        }
                    }
                    else
                    {
                        Tabla[i] = initObj.Module1.PrtTbl[i];
                    }

                    if (initObj.Module1.PartysOpe[i].LlaveArchivo != initObj.Module1.Partys[i].LlaveArchivo)
                    {
                        PrtCambio = (short)(true ? -1 : 0);
                        LlaveAnt = initObj.Module1.PartysOpe[i].LlaveArchivo + ":";
                        LlaveAnt = LlaveAnt + VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PartysOpe[i].IndNombre), "0") + ":";
                        LlaveAnt += VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PartysOpe[i].IndDireccion), "00");
                    }

                    initObj.Module1.PartysOpe[i] = initObj.Module1.Partys[i];

                    //reflejar cambios en string de cambios
                    if (PrtCambio != 0)
                    {
                        initObj.Module1.PrtControl.Cambios = initObj.Module1.PrtControl.Cambios + VB6Helpers.Format(VB6Helpers.CStr(i), "00") + ":";
                        initObj.Module1.PrtControl.Cambios = initObj.Module1.PrtControl.Cambios + LlaveAnt + ",";
                    }

                    if (initObj.Module1.PartysOpe[i].Status != T_Module1.GPrt_StatVacio)
                    {
                        CtaPartys = (short)(CtaPartys + 1);
                    }

                    //RTO 2012/03/30 INCIDENTE IR46139
                    if(initObj.Module1.PartysOpe[i].LlaveArchivo == "(00)~~~~~~~~" && !string.IsNullOrWhiteSpace(initObj.Module1.Partys[i].rut))
                    {
                        initObj.Module1.PartysOpe[i].LlaveArchivo = VB6Helpers.Format(initObj.Module1.PartysOpe[i].rut, "#||||||||||||");
                        initObj.Module1.Partys[i].LlaveArchivo = VB6Helpers.Format(initObj.Module1.Partys[i].rut, "#||||||||||||");
                    }

                    //RTO 2012/03/30 INCIDENTE IR46139
                }

                if(!string.IsNullOrEmpty(initObj.Module1.PrtControl.Cambios))
                {
                    initObj.Module1.PrtControl.Cambios = VB6Helpers.Left(initObj.Module1.PrtControl.Cambios, VB6Helpers.Len(initObj.Module1.PrtControl.Cambios) - 1);
                }

                //acepto los cambios de Pope
                LimSup = -1;
                
                LimSup = (short)VB6Helpers.UBound(initObj.Module1.Pope);

                if (LimSup >= 0)
                {
                    //tiene datos
                    initObj.Module1.PopeOpe = new PartysPope[LimSup + 1];
                    for (i = 0; i <= (short)LimSup; i++)
                    {
                        initObj.Module1.PopeOpe[i] = initObj.Module1.Pope[i];
                    }

                }

            }

            //Libero recursos de arreglo
            initObj.Module1.Partys = new PartyKey[1];
            initObj.Module1.Pope = new PartysPope[1];

            //Volvemos

            initObj.Frm_Participantes.Cancelar.Tag = 0;

            Frm_Participantes.Form_Unload(initObj);
            //no maneja el evento
            //VB6Helpers.Unload(Frm_Iden_Paticipantes.DefInstance);

            return CtaPartys;
        }

        
        /// <summary>
        /// Captura Partys o incribe pegados a la operacion
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="DesHabilita"></param>
        /// <param name="Iniciar"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static short GetParty1_2(string[] Tabla, short DesHabilita, short Iniciar, InitializationObject initObj,
            UnitOfWorkCext01 uow, bool? ultimaOperacionEsCosmos = false)
        {
            string LasMarcas = T_Module1.GPrt_MarcaRequerido + T_Module1.GPrt_MarcaOtros + T_Module1.GPrt_MarcaBanco;
            short Retorno = 0;
            short i = 0;
            short CtaPartys = 0;
            short LimSup = 0;
            short HayEnOperacion = 0;
            short Superior = 0;
            short j = 0;
            short PrtCambio = 0;
            string abc = "";
            string TablaAnt = "";
            string LlaveAnt = "";

            initObj.Module1.PrtControl.NoOperacion = DesHabilita;

            //Verifico si debo leer parametros
            if (initObj.Module1.PrtControl.Leidos == 0)
            {
                Retorno = VB6Helpers.CShort(LeeParametrosParty(initObj.Module1));
                if (Retorno != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message { Text = T_Module1.GPrt_NoPath, Type = TipoMensaje.Informacion, Title = T_Module1.GPrt_GetParty });

                    return (short)(false ? -1 : 0);
                }
            }

            //Debemos inicializar, dimensionar Partys y resetear modifico
            if (Iniciar != 0)
            {
                Retorno = ResetParty(initObj.Module1, Tabla);
                if (Retorno != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message { Text = T_Module1.GPrt_NoPath, Type = TipoMensaje.Informacion, Title = T_Module1.GPrt_GetParty });

                    return (short)(false ? -1 : 0);
                }
            }

            //preparo datos para operar
            initObj.Module1.Partys = new PartyKey[initObj.Module1.PrtControl.LimSup + 1];
            for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
            {
                if (string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]) && !string.IsNullOrEmpty(Tabla[i]))
                {
                    initObj.Module1.PrtTbl[i] = Tabla[i];
                }
                if (!string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]) && string.IsNullOrEmpty(Tabla[i]))
                {
                    initObj.Module1.PrtTbl[i] = "";
                }

                initObj.Module1.Partys[i] = initObj.Module1.PartysOpe[i];
                if (initObj.Module1.Partys[i].Status != T_Module1.GPrt_StatVacio)
                {
                    CtaPartys = (short)(CtaPartys + 1);
                }

            }

            //preparo Pope para operar
            LimSup = -1;
            LimSup = (short)VB6Helpers.UBound(initObj.Module1.PopeOpe);

            if (LimSup >= 0)
            {
                //tiene datos
                initObj.Module1.Pope = new PartysPope[LimSup + 1];
                for (i = 0; i <= (short)LimSup; i++)
                {
                    initObj.Module1.Pope[i] = initObj.Module1.PopeOpe[i];
                }

            }

            //salvo numero operacion
            initObj.Module1.PrtControl.NumOpe = initObj.Module1.Codop;

            //reseteo los cambios
            initObj.Module1.PrtControl.Cambios = "";
            initObj.Module1.PrtControl.PorInsertar = 0;

            //Parametros leidos, invoco el formulario de ingreso
            //Frm_Participantes.DefInstance.Show(1);
            Frm_Participantes.Form_Load(initObj, uow, ultimaOperacionEsCosmos);

            return Retorno;
        }

        /// <summary>
        /// Captura Partys o incribe pegados a la operacion
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="DesHabilita"></param>
        /// <param name="Iniciar"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static short GetParty2_2(string[] Tabla, short DesHabilita, short Iniciar, InitializationObject initObj,
            UnitOfWorkCext01 uow)
        {
            string LasMarcas = T_Module1.GPrt_MarcaRequerido + T_Module1.GPrt_MarcaOtros + T_Module1.GPrt_MarcaBanco;
            //short Retorno = 0;
            short i = 0;
            short CtaPartys = 0;
            short LimSup = 0;
            short HayEnOperacion = 0;
            short Superior = 0;
            short j = 0;
            short PrtCambio = 0;
            string abc = "";
            string TablaAnt = "";
            string LlaveAnt = "";

            MODXORI.SyGetn_Codtran(initObj.MODXORI, uow);

            if (initObj.Frm_Participantes.Aceptar.Tag != null && (initObj.Frm_Participantes.Aceptar.Tag as int?) != 0)
            {
                //acepto
                initObj.Module1.PrtControl.Leidos = (short)(initObj.Module1.PrtControl.Leidos + 1);  //otra vez
                initObj.Module1.PrtControl.Otros = (short)(initObj.Module1.PrtControl.Otros - initObj.Module1.PrtControl.PorInsertar);
                initObj.Module1.PrtControl.Insertado = (short)(initObj.Module1.PrtControl.Insertado + initObj.Module1.PrtControl.PorInsertar);

                if (~initObj.Module1.PrtControl.Modifico != 0)
                {
                    //si es falso verifico
                    for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
                    {

                        //ver si cambio glosas en la tabla
                        if (string.IsNullOrEmpty(Tabla[i]) && !string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]))
                        {
                            initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                            break;
                        }

                        if (initObj.Module1.Partys[i].Ubicacion == T_Module1.GPrt_EnParty)
                        {
                            //ver cambios en party
                            //cambios de status o llaves ==> modifico
                            if (initObj.Module1.Partys[i].Status != initObj.Module1.PartysOpe[i].Status || initObj.Module1.Partys[i].LlaveArchivo != initObj.Module1.PartysOpe[i].LlaveArchivo)
                            {
                                initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                                break;
                            }

                            //cambios de IndNombre o IndDireccion ==> modifico
                            if (initObj.Module1.Partys[i].IndNombre != initObj.Module1.PartysOpe[i].IndNombre || initObj.Module1.Partys[i].IndDireccion != initObj.Module1.PartysOpe[i].IndDireccion)
                            {
                                initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                                break;
                            }

                        }
                        else
                        {
                            HayEnOperacion = (short)(true ? -1 : 0);
                        }
                    }

                    //si no hay modificacion y HayEnOperacion, verifico cambios
                    if ((~initObj.Module1.PrtControl.Modifico & HayEnOperacion) != 0)
                    {
                        Superior = -1;
                        
                        Superior = (short)VB6Helpers.UBound(initObj.Module1.Pope);

                        for (j = 0; j <= (short)Superior; j++)
                        {
                            if (initObj.Module1.Pope[j].Status != T_Module1.GPrt_StatVacio && initObj.Module1.Pope[j].Status != T_Module1.GPrt_StatIntacto)
                            {
                                initObj.Module1.PrtControl.Modifico = (short)(true ? -1 : 0);
                                break;
                            }

                        }

                    }

                }

                //acepto los cambio de PartysOpe
                CtaPartys = 0;
                for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
                {
                    PrtCambio = (short)(false ? -1 : 0);
                    abc = VB6Helpers.Left(initObj.Module1.PrtTbl[i], 1);  //busco marca

                    if((VB6Helpers.Instr(1, LasMarcas, abc) & (!string.IsNullOrEmpty(abc) ? -1 : 0)) != 0)
                    {
                        //tiene marca
                        //si es marca de otros
                        if (abc == T_Module1.GPrt_MarcaOtros && Tabla[i] != VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2))
                        {
                            PrtCambio = (short)(true ? -1 : 0);
                            TablaAnt = Tabla[i];
                            Tabla[i] = VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2);
                        }

                        //saco las marcas a glosas que no puedo modificar
                        if (abc != T_Module1.GPrt_MarcaOtros)
                        {
                            Tabla[i] = VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2);
                        }
                    }
                    else
                    {
                        Tabla[i] = initObj.Module1.PrtTbl[i];
                    }

                    if (initObj.Module1.PartysOpe[i].LlaveArchivo != initObj.Module1.Partys[i].LlaveArchivo)
                    {
                        PrtCambio = (short)(true ? -1 : 0);
                        LlaveAnt = initObj.Module1.PartysOpe[i].LlaveArchivo + ":";
                        LlaveAnt = LlaveAnt + VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PartysOpe[i].IndNombre), "0") + ":";
                        LlaveAnt += VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PartysOpe[i].IndDireccion), "00");
                    }

                    initObj.Module1.PartysOpe[i] = initObj.Module1.Partys[i];

                    //reflejar cambios en string de cambios
                    if (PrtCambio != 0)
                    {
                        initObj.Module1.PrtControl.Cambios = initObj.Module1.PrtControl.Cambios + VB6Helpers.Format(VB6Helpers.CStr(i), "00") + ":";
                        initObj.Module1.PrtControl.Cambios = initObj.Module1.PrtControl.Cambios + LlaveAnt + ",";
                    }

                    if (initObj.Module1.PartysOpe[i].Status != T_Module1.GPrt_StatVacio)
                    {
                        CtaPartys = (short)(CtaPartys + 1);
                    }

                    //RTO 2012/03/30 INCIDENTE IR46139
                    if(initObj.Module1.PartysOpe[i].LlaveArchivo == "(00)~~~~~~~~" && !string.IsNullOrWhiteSpace(initObj.Module1.Partys[i].rut))
                    {
                        initObj.Module1.PartysOpe[i].LlaveArchivo = VB6Helpers.Format(initObj.Module1.PartysOpe[i].rut, "#||||||||||||");
                        initObj.Module1.Partys[i].LlaveArchivo = VB6Helpers.Format(initObj.Module1.Partys[i].rut, "#||||||||||||");
                    }

                    //RTO 2012/03/30 INCIDENTE IR46139
                }

                if(!string.IsNullOrEmpty(initObj.Module1.PrtControl.Cambios))
                {
                    initObj.Module1.PrtControl.Cambios = VB6Helpers.Left(initObj.Module1.PrtControl.Cambios, VB6Helpers.Len(initObj.Module1.PrtControl.Cambios) - 1);
                }

                //acepto los cambios de Pope
                LimSup = -1;
                
                LimSup = (short)VB6Helpers.UBound(initObj.Module1.Pope);

                if (LimSup >= 0)
                {
                    //tiene datos
                    initObj.Module1.PopeOpe = new PartysPope[LimSup + 1];
                    for (i = 0; i <= (short)LimSup; i++)
                    {
                        initObj.Module1.PopeOpe[i] = initObj.Module1.Pope[i];
                    }

                }

            }

            //Libero recursos de arreglo
            initObj.Module1.Partys = new PartyKey[0];
            initObj.Module1.Pope = new PartysPope[0];

            //Volvemos

            initObj.Frm_Participantes.Cancelar.Tag = 0;


            Frm_Participantes.Form_Unload(initObj);
            //no maneja el evento
            //VB6Helpers.Unload(Frm_Iden_Paticipantes.DefInstance);

            return CtaPartys;
        }


        public static short PrtyFlag(short flag, short Que)
        {
            if ((flag & Que) != 0)
            {
                return (short)(true ? -1 : 0);
            }

            return 0;
        }

        public static short ResetParty(T_Module1 Module1, string[] Arreglo) {
            if (Module1.PrtControl.Leidos == 0) {
                bool posRes = LeeParametrosParty(Module1);
                if (posRes) {
                    return 0;
                }
            }
            Module1.PrtControl.Leidos = (short)(true ? -1 : 0);
            Module1.PrtControl.Modifico = (short)(false ? -1 : 0);
            Module1.PrtControl.LimInf = (short)VB6Helpers.LBound(Arreglo);
            Module1.PrtControl.LimSup = (short)VB6Helpers.UBound(Arreglo);
            
            Module1.PartysOpe = new PartyKey[Module1.PrtControl.LimSup+1];
            for (int i = 0; i <= Module1.PrtControl.LimSup;i++ )
            {
                if (Module1.PartysOpe[i] == null)
                {
                    Module1.PartysOpe[i] = new PartyKey();
                }
            }

            
            //Module1.PrtTbl = new string[Module1.PrtControl.LimSup + 1];
            //inicio el array con valor "" 
            Module1.PrtTbl = Enumerable.Repeat(string.Empty, Module1.PrtControl.LimSup + 1).ToArray();

            Module1.PrtControl.Insertado = 0;
            Module1.PrtControl.Otros = 0;

            for (int i = Module1.PrtControl.LimInf; i <= Module1.PrtControl.LimSup; i++)
            {
                if (String.IsNullOrEmpty(Arreglo[i]))
                {
                    Module1.PrtControl.Otros += 1;
                }
            }

            short Lim = (short)VB6Helpers.UBound(Module1.PopeOpe);
            if (Lim < 0)
            {
                return 0;
            }

            Module1.PopeOpe = new PartysPope[1] { new PartysPope()};

            return 0;
        }

        public static bool LeeParametrosParty(T_Module1 Module1)
        {
            string path = MODGPYF0.GetUbicacion("PathPartys");
            if(path.Length==0)
            {
                Module1.PrtControl.Leidos = 0;
                return true;
            }
            Module1.PrtControl.PartyPath = path;
            
            string dato = Mdl_Acceso.GetConfigValue("FundTransfer.Participantes.PartyEnRed");
            Module1.PrtControl.Red = (short)(false ? -1 : 0);
            if (dato.Length>0 && int.Parse(dato) > 0) {
                Module1.PrtControl.Red = (short)(true ? -1 : 0);
            }
            
            dato = Mdl_Acceso.GetConfigValue("FundTransfer.Participantes.PartyNodo");
            Module1.PrtControl.Nodo = "";
            if (dato.Length > 0) {
                Module1.PrtControl.Nodo = dato;
            }

            dato = Mdl_Acceso.GetConfigValue("FundTransfer.Participantes.PartyServidor");
            Module1.PrtControl.Servidor = "";
            if (dato.Length > 0)
            {
                Module1.PrtControl.Servidor = dato;
            }

            if(String.IsNullOrEmpty(Module1.PrtControl.Nodo) || String.IsNullOrEmpty(Module1.PrtControl.Servidor)){
                Module1.PrtControl.Red = -1;
            }
            Module1.PrtControl.Leidos = -1;

            return false;
        }

        ///// <summary>
        ///// Elige el Participante.-
        ///// Obtiene las direcciones razones sociales del cliente, y prepara y muestra Frm_Iden_Participantes
        ///// </summary>
        ///// <param name="initObj"></param>
        ///// <param name="uow"></param>
        ///// <returns></returns>
        //public static short EligeSy(InitializationObject initObj, UnitOfWorkCext01 uow)
        //{
        //    short _retValue = 0;
        //    string s = "";
        //    string FileKey = ""; //identificador del cliente (campo llave)
        //    short Borrado = 0;
        //    string razon = "";
        //    string Direccion = "";
        //    short Nombre = 0;
        //    short Id_Direccion = 0;
        //    short EligeNomb_DirSy = 0;

        //    _retValue = 1;//true

        //    try
        //    {
        //        initObj.Frm_Iden_Participantes.Caption = T_Module1.GPrt_Caption + " [" + initObj.Frm_Participantes.Llave.Text + "]";
        //        s = initObj.Frm_Participantes.Llave.Text + VB6Helpers.String(12 - VB6Helpers.Len(initObj.Frm_Participantes.Llave.Text), 126);
        //        s = MODGPYF0.Componer(VB6Helpers.Trim(s), "~", "|");
        //        FileKey = s;

        //        //si llave es igual al respaldo ==> ya lei y no he cambiado
        //        if (FileKey == initObj.Frm_Iden_Participantes.Tag)
        //        {
        //            if ((initObj.Frm_Iden_Participantes.Nome.Items.Count() == 1) && (initObj.Frm_Iden_Participantes.Dire.Items.Count() == 1))
        //            {
        //                _retValue = T_Module1.GPrt_RetExiste; //false
        //                //Frm_Iden_Paticipantes.DefInstance.Hide();
        //            }
        //            else
        //            {
        //                //TODO:@estanislao - Revisado revisar este show
        //                //Frm_Iden_Paticipantes.DefInstance.Show(1);
        //                return (short)VB6Helpers.Val(initObj.Frm_Iden_Participantes.Aceptar.Tag);
        //            }

        //            return _retValue;
        //        }

        //        //Tabla Razones Sociales (Sce_Rsa).-
        //        var lstRazSoc = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_2_Result>("pro_sce_prty_s02_MS", FileKey, "2");
        //        if (lstRazSoc == null)
        //        {
        //            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //            {
        //                Text = T_Module1.GPrt_ErrGetDbf + "(Nombres)",
        //                Type = TipoMensaje.Informacion,
        //                Title = T_Module1.GPrt_Caption
        //            });
        //        }

        //        initObj.Frm_Iden_Participantes.Nome.Items.Clear();
        //        foreach (var item in lstRazSoc)
        //        {
        //            //Borrado = (short)(item.borrado ? -1 : 0);
        //            razon = item.razon_soci;
        //            Nombre = (short)item.id_nombre;
        //            initObj.Frm_Iden_Participantes.Nome.Items.Add(new UI_ComboItem { 
        //                Value=razon, 
        //                Data=((short)Nombre)
        //            });
        //        }

        //        var lstDir = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_3_Result>("pro_sce_prty_s02_MS", FileKey, "3");
        //        if (lstDir == null)
        //        {
        //            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //            {
        //                Text = T_Module1.GPrt_ErrGetDbf + "(Nombres)",
        //                Type = TipoMensaje.Informacion,
        //                Title = T_Module1.GPrt_Caption
        //            });
        //        }

        //        initObj.Frm_Iden_Participantes.Dire.Items.Clear();
        //        initObj.Frm_Iden_Participantes.Otro.Items.Clear();

        //        foreach (var item in lstDir)
        //        {
        //            Borrado = (short)(item.borrado ? -1 : 0);
        //            Direccion = item.direccion;
        //            Id_Direccion = (short)item.id_dir;

        //            if (!item.borrado)
        //            {
        //                initObj.Frm_Iden_Participantes.Dire.Items.Add(new UI_ComboItem {
        //                    Value = item.direccion,
        //                    Data = (short)item.id_dir
        //                });

        //                initObj.Frm_Iden_Participantes.Otro.Items.Add(new UI_ComboItem
        //                {
        //                    //Value = item
        //                    Tag = item
        //                });
        //            }
        //        }

        //        //Mostrar seleccion anterior
        //        if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status != T_Module1.GPrt_StatVacio)
        //        {
        //            for (int i = 0; i < initObj.Frm_Iden_Participantes.Nome.Items.Count; i++)
        //            {
        //                if (initObj.Frm_Iden_Participantes.Nome.Items[i].ID == 
        //                    initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndNombre.ToString())
        //                {
        //                    initObj.Frm_Iden_Participantes.Nome.ListIndex = i;
        //                    break;
        //                }
        //            }

        //            if (initObj.Frm_Iden_Participantes.Nome.ListIndex == -1)
        //            {
        //                initObj.Frm_Iden_Participantes.Nome.ListIndex = 0;
        //            }


        //            for (int i = 0; i < initObj.Frm_Iden_Participantes.Dire.Items.Count; i++)
        //            {
        //                if (initObj.Frm_Iden_Participantes.Dire.Items[i].ID ==
        //                    initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndDireccion.ToString())
        //                {
        //                    initObj.Frm_Iden_Participantes.Dire.ListIndex = i;
        //                    break;
        //                }
        //            }

        //            if (initObj.Frm_Iden_Participantes.Dire.ListIndex == -1)
        //            {
        //                initObj.Frm_Iden_Participantes.Dire.ListIndex = 0;
        //            }
        //        }
        //        else
        //        {
        //            initObj.Frm_Iden_Participantes.Nome.ListIndex = 0;
        //            initObj.Frm_Iden_Participantes.Dire.ListIndex = 0;
        //        }

        //        //Si tiene solo un item cada lista, debemos volver
        //        if (initObj.Frm_Iden_Participantes.Nome.Items.Count() == 1 &&
        //            initObj.Frm_Iden_Participantes.Dire.Items.Count() == 1)
        //        {
        //            EligeNomb_DirSy = T_Module1.GPrt_RetExiste;

        //            return 0;
        //        }

        //        //TODO:@estanislao - Revisado Show!!
        //        //initObj.Frm_Iden_Participantes.Show(1);

        //        //Respaldamos la llave actual.-
        //        initObj.Frm_Iden_Participantes.Tag = FileKey;
        //        _retValue = (short)VB6Helpers.Val(initObj.Frm_Iden_Participantes.Aceptar.Tag);

        //        return _retValue;
        //    }
        //    catch (Exception _ex)
        //    {
        //        //VB6Helpers.SetError(_ex);
        //        //VB6Helpers.MsgBox("[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number), MsgBoxStyle.Information, GPrt_Caption);

        //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //        {
        //            Text = "[" + _ex.HResult + "] "  + _ex.Message,
        //            Type = TipoMensaje.Informacion,
        //            Title = T_Module1.GPrt_Caption
        //        });

        //        return _retValue;
        //    }
        //}

        /// <summary>
        /// Elige el Participante.-
        /// Obtiene las direcciones razones sociales del cliente, y prepara y muestra Frm_Iden_Participantes
        /// Pre show de Frm_Iden_Participantes
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static void EligeSy1_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short _retValue = 0;
            string s = "";
            string FileKey = ""; //identificador del cliente (campo llave)
            short Borrado = 0;
            string razon = "";
            string Direccion = "";
            short Nombre = 0;
            short Id_Direccion = 0;

            _retValue = 1;//true

            try
            {
                initObj.Frm_Participantes.AbrirIdentParticipantes = false;

                initObj.Frm_Iden_Participantes.Caption = T_Module1.GPrt_Caption + " [" + initObj.Frm_Participantes.Llave.Text.ToUpper() + "]";
                s = initObj.Frm_Participantes.Llave.Text.ToUpper() + VB6Helpers.String(12 - VB6Helpers.Len(initObj.Frm_Participantes.Llave.Text.ToUpper()), 126);
                s = MODGPYF0.Componer(VB6Helpers.Trim(s), "~", "|");
                FileKey = s;

                //si llave es igual al respaldo ==> ya lei y no he cambiado
                if (FileKey == initObj.Frm_Iden_Participantes.Tag)
                {
                    if ((initObj.Frm_Iden_Participantes.Nome.Items.Count() == 1) && (initObj.Frm_Iden_Participantes.Dire.Items.Count() == 1))
                    {
                        _retValue = T_Module1.GPrt_RetExiste; //false
                        //Frm_Iden_Paticipantes.DefInstance.Hide();
                    }
                    else
                    {
                        //Frm_Iden_Paticipantes.DefInstance.Show(1);
                        initObj.Frm_Participantes.AbrirIdentParticipantes = true;
                        return;
                    }

                    return;
                }

                //Tabla Razones Sociales (Sce_Rsa).-
                var lstRazSoc = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_2_Result>("pro_sce_prty_s02_MS", FileKey.ToUpper(), "2");
                if (lstRazSoc == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = T_Module1.GPrt_ErrGetDbf + "(Nombres)",
                        Type = TipoMensaje.Informacion,
                        Title = T_Module1.GPrt_Caption
                    });
                }

                initObj.Frm_Iden_Participantes.Nome.Items.Clear();
                foreach (var item in lstRazSoc)
                {
                    //Borrado = (short)(item.borrado ? -1 : 0);
                    razon = item.razon_soci;
                    Nombre = (short)item.id_nombre;
                    initObj.Frm_Iden_Participantes.Nome.Items.Add(new UI_ComboItem
                    {
                        Value = razon,
                        Data = ((short)Nombre)
                    });
                }

                var lstDir = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_3_Result>("pro_sce_prty_s02_MS", FileKey.ToUpper(), "3");
                if (lstDir == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = T_Module1.GPrt_ErrGetDbf + "(Nombres)",
                        Type = TipoMensaje.Informacion,
                        Title = T_Module1.GPrt_Caption
                    });
                }

                initObj.Frm_Iden_Participantes.Dire.Items.Clear();
                initObj.Frm_Iden_Participantes.Otro.Items.Clear();

                foreach (var item in lstDir)
                {
                    Borrado = (short)(item.borrado ? -1 : 0);
                    Direccion = item.direccion;
                    Id_Direccion = (short)item.id_dir;

                    if (!item.borrado)
                    {
                        initObj.Frm_Iden_Participantes.Dire.Items.Add(new UI_ComboItem
                        {
                            Value = item.direccion,
                            Data = (short)item.id_dir
                        });
                        //Se guarda el resto del string devuelto en este cmb
                        initObj.Frm_Iden_Participantes.Otro.Items.Add(new UI_ComboItem
                        {
                            //Value = item
                            Tag = item
                        });
                    }
                }

                //Mostrar seleccion anterior
                if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status != T_Module1.GPrt_StatVacio)
                {
                    for (int i = 0; i < initObj.Frm_Iden_Participantes.Nome.Items.Count; i++)
                    {
                        if (initObj.Frm_Iden_Participantes.Nome.Items[i].Data ==
                            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndNombre)
                        {
                            initObj.Frm_Iden_Participantes.Nome.ListIndex = i;
                            break;
                        }
                    }

                    if (initObj.Frm_Iden_Participantes.Nome.ListIndex == -1)
                    {
                        initObj.Frm_Iden_Participantes.Nome.ListIndex = 0;
                    }


                    for (int i = 0; i < initObj.Frm_Iden_Participantes.Dire.Items.Count; i++)
                    {
                        if (initObj.Frm_Iden_Participantes.Dire.Items[i].Data ==
                            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndDireccion)
                        {
                            initObj.Frm_Iden_Participantes.Dire.ListIndex = i;
                            break;
                        }
                    }

                    if (initObj.Frm_Iden_Participantes.Dire.ListIndex == -1)
                    {
                        initObj.Frm_Iden_Participantes.Dire.ListIndex = 0;
                    }
                }
                else
                {
                    initObj.Frm_Iden_Participantes.Nome.ListIndex = 0;
                    initObj.Frm_Iden_Participantes.Dire.ListIndex = 0;
                }

                //Si tiene solo un item cada lista, debemos volver
                if (initObj.Frm_Iden_Participantes.Nome.Items.Count() == 1 &&
                    initObj.Frm_Iden_Participantes.Dire.Items.Count() == 1)
                {
                    //EligeNomb_DirSy = T_Module1.GPrt_RetExiste;

                    return;
                }

                //initObj.Frm_Iden_Participantes.Show(1);
                initObj.Frm_Participantes.AbrirIdentParticipantes = true;;
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "[" + _ex.HResult + "] " + _ex.Message,
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption
                });
            }
        }

        /// <summary>
        /// Elige el Participante.-
        /// Obtiene las direcciones razones sociales del cliente, y prepara y muestra Frm_Iden_Participantes
        /// Post show de Frm_Iden_Participantes
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static short EligeSy2_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short _retValue = 1;//true
            string FileKey = ""; //identificador del cliente (campo llave)

            try
            {
                //Respaldamos la llave actual.-
                initObj.Frm_Iden_Participantes.Tag = FileKey;
                _retValue = (short)VB6Helpers.Val(initObj.Frm_Iden_Participantes.Aceptar.Tag);

                return _retValue;
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "[" + _ex.HResult + "] " + _ex.Message,
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption
                });

                return _retValue;
            }
        }

        public static short EsRut(string rut)
        {
            short _retValue = 0;
            short i = 0;
            string a = "";
            string DvRut = "";
            string b = "";
            short suma = 0;
            short es = 0;
            short aa = 0;
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
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'b' variable as a StringBuilder6 object.
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

        public static string DesCero(string Que, string Mascara, short Modo)
        {
            string Tmp = VB6Helpers.Trim(Que);
            short Aqui = 0;
            string sal = "";
            short i = 0;
            string b = "";
            if (Modo != 0)
            {
                //es para swift
                if (VB6Helpers.Len(Tmp) == 0)
                {
                    return Mascara;
                }

                sal = Tmp + VB6Helpers.Right(Mascara, VB6Helpers.Len(Mascara) - VB6Helpers.Len(Tmp));
            }
            else
            {
                //sacar ceros a la izquierda
                Aqui = (short)VB6Helpers.Len(Tmp);
                if (VB6Helpers.Len(Tmp) == 0)
                {
                    return Mascara;
                }

                Tmp = MODGPYF0.TrimChar(Que, "0", 0);
                if (VB6Helpers.Len(Tmp) == 0)
                {
                    return Mascara;
                }

                Tmp = VB6Helpers.String(Aqui - VB6Helpers.Len(Tmp), "_") + Tmp;

                for (i = (short)VB6Helpers.Len(Mascara); i >= 1; i--)
                {
                    b = VB6Helpers.Mid(Mascara, i, 1);
                    if (((b == "_" ? -1 : 0) & Aqui) != 0)
                    {
                        sal = VB6Helpers.Mid(Tmp, Aqui, 1) + sal;
                        Aqui = (short)(Aqui - 1);
                    }
                    else
                    {
                        sal = b + sal;
                    }

                }

            }

            return sal;
        }

        public static string FilCero(string Que, string Mascara, short Modo)
        {
            string Tmp = VB6Helpers.Trim(Que);
            short i = 0;
            string a = "";
            short Mas = 0;
            string sal = "";
            string NewMask = "";

            for (i = 1; i <= (short)VB6Helpers.Len(Mascara); i++)
            {
                a = VB6Helpers.Mid(Mascara, i, 1);
                if (a != "_")
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'NewMask' variable as a StringBuilder6 object.
                    NewMask += a;
                }
                else
                {
                    Mas = (short)(Mas + 1);
                }

            }

            for (i = 1; i <= (short)VB6Helpers.Len(Tmp); i++)
            {
                a = VB6Helpers.Mid(Tmp, i, 1);
                if (VB6Helpers.Instr(1, NewMask, a) == 0)
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'sal' variable as a StringBuilder6 object.
                    sal += a;
                }

            }

            if (Modo != 0)
            {
                return sal + VB6Helpers.Space(Mas - VB6Helpers.Len(sal));
            }
            else
            {
                return VB6Helpers.String((int)VB6Helpers.Abs(Mas - VB6Helpers.Len(sal)), 48) + sal;
            }

        }

        //salva los datos de los partys pegados a la operacion
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SySalvarGetParty(InitializationObject initObject,UnitOfWorkCext01 unit, CdOper Operacion)
        {
            T_Module1 Module1 = initObject.Module1;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short Lim = -1;
            short i = 0;
            short abc = 0;
            short flg = 0;
            string a = "";
            string Que = "";
            // UPGRADE_INFO (#0501): The 'b' member isn't used anywhere in current application.
            string b = "";
            string dd = "";
            string patpope = "";
            string R = "";
            string Txt = "";
            short Sale = 0;
            Lim = (short)VB6Helpers.UBound(Module1.PopeOpe);

            if (Lim < 0)
            {
                return 0;
                //nada que salvar
            }

            for (i = 0; i <= (short)Lim; i++)
            {
                abc = (short)(false ? -1 : 0);
                flg = (short)(false ? -1 : 0);
                string nomProc = String.Empty;
                List<string> parameters = new List<string>();
                
                short _switchVar1 = Module1.PopeOpe[i].Status;
                if (_switchVar1 == T_Module1.GPrt_StatNuevo)
                {
                    nomProc = "sce_pope_w01";
                    abc = 1;
                }
                else if (_switchVar1 == T_Module1.GPrt_StatCambio)
                {
                    nomProc = "sce_pope_w01";
                    abc = 2;
                }
                else if (_switchVar1 == T_Module1.GPrt_StatBorro)
                {
                    nomProc = "pro_sce_pope_d01 ";
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Que' variable as a StringBuilder6 object.
                    parameters.Add(MODGSYB.dbcharSy(Operacion.Cent_Costo));
                    parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Product));
                    parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Especia) );
                    parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Empresa) );
                    parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Operacion));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Format(VB6Helpers.CStr(Module1.PopeOpe[i].Secuencia), "00")));

                    abc = 3;
                }

                //expresamos los valores
                if (abc != 0)
                {
                    //editar valores
                    if (abc < 3)
                    {

                        parameters.Add(MODGSYB.dbcharSy(Operacion.Cent_Costo));
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'dd' variable as a StringBuilder6 object.
                        parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Product));
                        parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Especia));
                        parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Empresa));
                        parameters.Add(MODGSYB.dbcharSy(Operacion.Id_Operacion));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Format(VB6Helpers.CStr(Module1.PopeOpe[i].Secuencia), "00")));

                        parameters.Add(MODGSYB.dblogisy(Module1.PopeOpe[i].EsBanco));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].RutSwift));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Nombre));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Direccion));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].comuna));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].estado));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Ciudad));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Pais));
                        parameters.Add(MODGSYB.dbnumesy(Module1.PopeOpe[i].CodPais));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Postal));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Telefono));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Fax));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].Telex));
                        parameters.Add(MODGSYB.dbnumesy(Module1.PopeOpe[i].Enviara));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].CasPostal));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PopeOpe[i].CasBanco));
                        
                    }
                    int resOpe = -1;
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {
                            if (reader.GetInt32(0)==0)
                            {
                                resOpe = 0;
                            }
                            else
                            {
                                resOpe = -1;
                            }
                        }
                        else
                        {
                            resOpe = -1;
                        }
                    }, nomProc, parameters.ToArray());
                    if (resOpe == -1)
                    {
                        switch (abc)
                        {
                            case 1:
                                Txt = "insertar";
                                break;
                            case 2:
                                Txt = "modificar";
                                break;
                            case 3:
                                Txt = "eliminar";
                                break;
                        }
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error al tratar de " + Txt + " los datos del Participante en operación [SySalvarGetParty]. Reporte este problema.",
                            Title = "¡¡ Problemas !!"
                        });
                        Sale = (short)(true ? -1 : 0);
                    }
                }

            }

            return Sale;
        }

        public static short NoEsRut(string rut)
        {
            string D_V = VB6Helpers.Right(rut, 1);
            string a = "";
            short aa = 0;
            short suma = 0;
            short i = 0;
            string b = VB6Helpers.Left(rut, VB6Helpers.Len(rut) - 1);
            short es = 0;
            string DvCal = "";

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
                case 11:
                    DvCal = "0";
                    break;
                case 10:
                    DvCal = "K";
                    break;
                default:
                    DvCal = VB6Helpers.Format(VB6Helpers.CStr(es));
                    break;
            }

            if (DvCal != VB6Helpers.UCase(D_V))
            {
                return (short)(true ? -1 : 0);
            }

            return 0;
        }
    }
}
