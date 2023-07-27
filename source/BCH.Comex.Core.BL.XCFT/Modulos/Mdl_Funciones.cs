using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BCH.Comex.Common.Tracing;
using System.Text.RegularExpressions;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Core.Entities.Swift;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class Mdl_Funciones
    {
        public static T_Mdl_Funciones GetMdl_Funciones()
        {
            return new T_Mdl_Funciones();
        }

        //Lee una Declaración de Exportación.-
        //Retorno    True  : Lectura Exitosa.-
        //           False : Error o Lectura no Exitosa.-
        public static short SyGet_xDec(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UnitOfWorkCext01 unit, string numdec, string FecDec, short CodAdn)
        {
            using (var trace = new Tracer("SyGet_xDec: Lee una Declaración de Exportación"))
            {
                short _retValue = 0;
                string Que = "";
                string R = "";
                dynamic MsgxCob = null;
                dynamic MsgxDec = null;

                string ResultadoQuery = "";
                try
                {
                    // IGNORED: On Error GoTo SyGet_xDecErr

                    Mdl_Funciones_Varias.VxDec = Mdl_Funciones_Varias.VxDecNul;
                    sce_xdec_s01_MS_Result res = unit.SceRepository.EjecutarSP<sce_xdec_s01_MS_Result>("sce_xdec_s01_MS", numdec, FecDec, CodAdn.ToString()).First();


                    Mdl_Funciones_Varias.VxDec.numdec = res.numdec;
                    Mdl_Funciones_Varias.VxDec.FecDec = res.fecdec.ToString("dd/MM/yyy");
                    Mdl_Funciones_Varias.VxDec.CodAdn = (short)res.codadn;
                    Mdl_Funciones_Varias.VxDec.cencos = res.cencos;
                    Mdl_Funciones_Varias.VxDec.codusr = res.codusr;
                    Mdl_Funciones_Varias.VxDec.Fecing = res.fecing.ToString("dd/MM/yyyy");
                    Mdl_Funciones_Varias.VxDec.FecAct = res.fecact.ToString("dd/MM/yyyy");
                    Mdl_Funciones_Varias.VxDec.estado = (short)res.estado;
                    Mdl_Funciones_Varias.VxDec.TipDec = (short)res.tipdec;
                    Mdl_Funciones_Varias.VxDec.CodCCv = (short)res.codccv;
                    Mdl_Funciones_Varias.VxDec.RutExp1 = res.rutexp1;
                    Mdl_Funciones_Varias.VxDec.PrtExp1 = res.prtexp1;
                    Mdl_Funciones_Varias.VxDec.IndNom1 = (short)res.indnom1;
                    Mdl_Funciones_Varias.VxDec.IndDir1 = (short)res.inddir1;
                    Mdl_Funciones_Varias.VxDec.Porcen1 = (double)res.porcen1;

                    Mdl_Funciones_Varias.VxDec.ValRet1 = (double)res.valret1;
                    Mdl_Funciones_Varias.VxDec.ValCom1 = (double)res.valcom1;
                    Mdl_Funciones_Varias.VxDec.ValGas1 = (double)res.valgas1;
                    Mdl_Funciones_Varias.VxDec.ValLiq1 = (double)res.valliq1;
                    Mdl_Funciones_Varias.VxDec.ValFle1 = (double)res.valfle1;
                    Mdl_Funciones_Varias.VxDec.ValSeg1 = (double)res.valseg1;

                    Mdl_Funciones_Varias.VxDec.ValRet1c = (double)res.valret1c;
                    Mdl_Funciones_Varias.VxDec.ValCom1c = (double)res.valcom1c;
                    Mdl_Funciones_Varias.VxDec.ValGas1c = (double)res.valgas1c;
                    Mdl_Funciones_Varias.VxDec.ValLiq1c = (double)res.valliq1c;
                    Mdl_Funciones_Varias.VxDec.ValFle1c = (double)res.valfle1c;
                    Mdl_Funciones_Varias.VxDec.ValSeg1c = (double)res.valseg1c;

                    Mdl_Funciones_Varias.VxDec.RutExp2 = res.rutexp2;
                    Mdl_Funciones_Varias.VxDec.PrtExp2 = res.prtexp2;
                    Mdl_Funciones_Varias.VxDec.IndNom2 = (short)res.indnom2;
                    Mdl_Funciones_Varias.VxDec.IndDir2 = (short)res.inddir2;
                    Mdl_Funciones_Varias.VxDec.Porcen2 = (double)res.porcen2;

                    Mdl_Funciones_Varias.VxDec.ValRet2 = (double)res.valret2;
                    Mdl_Funciones_Varias.VxDec.ValCom2 = (double)res.valcom2;
                    Mdl_Funciones_Varias.VxDec.ValGas2 = (double)res.valgas2;
                    Mdl_Funciones_Varias.VxDec.ValLiq2 = (double)res.valliq2;
                    Mdl_Funciones_Varias.VxDec.ValFle2 = (double)res.valfle2;
                    Mdl_Funciones_Varias.VxDec.ValSeg2 = (double)res.valseg2;

                    Mdl_Funciones_Varias.VxDec.ValRet2c = (double)res.valret2c;
                    Mdl_Funciones_Varias.VxDec.ValCom2c = (double)res.valcom2c;
                    Mdl_Funciones_Varias.VxDec.ValGas2c = (double)res.valgas2c;
                    Mdl_Funciones_Varias.VxDec.ValLiq2c = (double)res.valliq2c;
                    Mdl_Funciones_Varias.VxDec.ValFle2c = (double)res.valfle2c;
                    Mdl_Funciones_Varias.VxDec.ValSeg2c = (double)res.valseg2c;

                    Mdl_Funciones_Varias.VxDec.DiaRet = (short)res.diaret;
                    Mdl_Funciones_Varias.VxDec.FecRet = res.fecret.ToString("dd/MM/yyyy");
                    Mdl_Funciones_Varias.VxDec.CodPbc = (short)res.codpbc;
                    Mdl_Funciones_Varias.VxDec.NumInf = res.numinf;
                    Mdl_Funciones_Varias.VxDec.FecInf = res.fecinf.ToString("dd/MM/yyyy");

                    Mdl_Funciones_Varias.VxDec.ValDis1 = Mdl_Funciones_Varias.VxDec.ValRet1 - Mdl_Funciones_Varias.VxDec.ValRet1c;
                    Mdl_Funciones_Varias.VxDec.ValDis2 = Mdl_Funciones_Varias.VxDec.ValRet2 - Mdl_Funciones_Varias.VxDec.ValRet2c;

                    _retValue = (short)(true ? -1 : 0);

                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    _retValue = 0;
                }

                return _retValue;
            }
        }

        //Retorna :  FecDec + CodAdn : si    encuentra la Declaración.
        //           Blanco          : si NO encuentra la Declaración.
        public static string SyExis_xDec(UnitOfWorkCext01 unit, string numdec, string fecDec, short codAdn)
        {
            using (var trace = new Tracer("SyExis_xDec"))
            {
                string _retValue = "";
                string n = "";
                string f = "";
                short a = 0;
                string c = "";
                string u = "";

                try
                {
                    trace.TraceInformation("Datos para pro_sce_xdec_s01_MS_MS: numdec: {0}, fecDec: {1}, codAdn: {2}", numdec, fecDec, codAdn);
                    pro_sce_xdec_s01_MS_Result res = unit.SceRepository.pro_sce_xdec_s01_MS_MS(numdec, fecDec, codAdn);
                    if (res != null)
                    {
                        n = res.numdec;
                        f = res.fecdec.ToString("dd/MM/yyyy");
                        a = (short)res.codadn;
                        c = res.cencos;
                        u = res.codusr;
                        _retValue = n + ";" + f + ";" + VB6Helpers.Trim(VB6Helpers.Str(a)) + ";" + c + u;
                    }

                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    _retValue = String.Empty;
                }

                return _retValue;
            }
        }

        //****************************************************************************
        //   1.  Lee una frase dependiendo del código de esta + Idioma.
        //   2.  Luego la concatena con una cadena variable.
        //****************************************************************************
        public static string SyGet_Fra(InitializationObject initObj, UnitOfWorkCext01 uow, short CodFra, string Idioma, string cadena)
        {
            string _retValue = "";
            //// UPGRADE_INFO (#05B1): The 'MsgFra' variable wasn't declared explicitly.
            //dynamic MsgFra = null;
            //// UPGRADE_INFO (#05B1): The 'Frase' variable wasn't declared explicitly.
            //string Frase = "";
            //// UPGRADE_INFO (#05B1): The 'CodMemo' variable wasn't declared explicitly.
            //int CodMemo = 0;
            //// UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            //short n = 0;
            //// UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            //short i = 0;
            //// UPGRADE_INFO (#05B1): The 's' variable wasn't declared explicitly.
            //string s = "";
            //string Que = "";
            //string R = "";
            //try
            //{
            //    // IGNORED: On Error GoTo SyGet_FraErr

            //    /*Que = "Exec " + Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + "." + "sce_fra_s06_MS ";
            //    Que = VB6Helpers.LCase(Que);
            //    Que = Que + MODGSYB.dbnumesy(CodFra) + " , ";
            //    Que += MODGSYB.dbcharSy(Idioma);

            //    //Se ejecuta el Query.
            //    R = Mdl_SRM.RespuestaQuery(ref Que);TODO ARKANO*/

            //    //Error en el Query.
            //    if (R == "-1")
            //    {
            //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
            //        {
            //            Type = TipoMensaje.Error,
            //            Text = "Se ha producido un error al tratar al tratar de leer una Frase Estandar (Sce_Fra).",
            //            Title = MsgFra
            //        });
            //    }

            //    //Resultado nulo del Query.
            //    if (R == "")
            //    {
            //        return string.Empty;
            //    }

            //    //Resultado nulo de la Consulta.
            //    Frase = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R)));
            //    CodMemo = VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));

            //    //Rescata en campo Memo.
            //    if (CodMemo > 0)
            //    {
            //        Frase = MODGMEM.SyGetn_Mem(uow, "f", CodMemo);
            //    }

            //    //Concatena la Frase con la Cadena variable.
            //    if (VB6Helpers.Trim(cadena) != "")
            //    {
            //        n = MODGPYF0.cuentadestring(cadena, "~");
            //        for (i = 1; i <= (short)n; i++)
            //        {
            //            s = MODGPYF0.copiardestring(cadena, "~", i);
            //            if (VB6Helpers.Trim(Frase) != "")
            //            {
            //                Frase = ComponerUna(Frase, "@", s);
            //            }
            //        }
            //    }
            //    _retValue = VB6Helpers.Trim(Frase);            
            //}
            //catch (Exception _ex)
            //{
            //    // IGNORED: SyGet_FraErr:
            //    VB6Helpers.SetError(_ex);
            //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
            //    {
            //        Type = TipoMensaje.Informacion,
            //        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
            //        Title = VB6Helpers.CStr(MsgFra)
            //    });                
            //}
                return _retValue;
        }

        //forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
        //en "Donde".  Si no encuentra ninguna retorna "Donde"
        public static string ComponerUna(string Donde, string Que, string En)
        {
            string Sale = Donde;
            short Aqui = (short)VB6Helpers.Instr(1, Sale, Que);
            if (Aqui != 0)
            {
                Sale = VB6Helpers.Left(Sale, Aqui - 1) + En + VB6Helpers.Mid(Sale, Aqui + VB6Helpers.Len(Que));
            }

            return Sale;
        }

        public static string Det_Vias(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            T_Module1 Module1 = initObject.Module1;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGSWF MODGSWF = initObject.MODGSWF;
            
            short m = 0;
            string s = "";
            short n = 0;
            string NomBen = "";

            m = Nro_Vias(MODXVIA);
            s = "";

            if (m != -1)
            {

                s = s + VB6Helpers.Trim(VB6Helpers.Str(m)) + VB6Helpers.Chr(9);

                //Vía de la Remesa.
                //Datos correspondientes a las Vías en Moneda Extranjera.
                for (int i = 0; i < MODXVIA.VxVia.Length; i++)
                {
                    if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].Status != 0) && (MODXVIA.VxVia[i].CodMon != MODGCVD.CodMonedaNac))
                    {
                        //Incluir Beneficiarios Cheques, Vales Vistas, Swift's.
                        short _switchVar1 = MODXVIA.VxVia[i].NumCta;
                        if (_switchVar1 == T_MODGCON0.IdCta_CHMEBCH || _switchVar1 == T_MODGCON0.IdCta_VVBCH)
                        {
                            n = MODXVIA.VxVia[i].IndChq;
                            NomBen = MODGCHQ.V_Chq_VVi[n].NomBen;
                        }
                        else if (_switchVar1 == T_MODGCON0.IdCta_OPC || _switchVar1 == T_MODGCON0.IdCta_OPOP)
                        {
                            n = MODXVIA.VxVia[i].IndSwf;
                            NomBen = MODGSWF.VSwf[n-1].BenSwf.NomBen;
                        }
                        else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC)
                        {
                            NomBen = Mdl_Funciones_Varias.GetDatPrt(initObject,unit, MODXVIA.VxVia[i].IdPrty, 0, 0, "N");
                        }
                        else
                        {
                            NomBen = Module1.PartysOpe[MODXVIA.VxVia[i].PosPrty].NombreUsado;
                        }
                        
                        s = s + MODGFYS.Escribe_Nombre(ref NomBen) + VB6Helpers.Chr(9);

                        if (string.IsNullOrWhiteSpace(MODXVIA.VxVia[i].CtaCte_t))
                        {
                            s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + VB6Helpers.Chr(9);
                        }
                        else
                        {
                            s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + " " + VB6Helpers.Trim(MODXVIA.VxVia[i].CtaCte_t) + VB6Helpers.Chr(9);
                        }

                        s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NemMon) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[i].MtoTot)) + VB6Helpers.Chr(9);
                    }

                }

                //Datos correspondientes a las Vías en Moneda Nacional.
                for (int i = 0; i < MODXVIA.VxVia.Length; i++)
                {
                    if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].Status != 0) && (MODXVIA.VxVia[i].CodMon == MODGCVD.CodMonedaNac))
                    {
                        s = s + "Abono" + VB6Helpers.Chr(9);
                        if (string.IsNullOrWhiteSpace(MODXVIA.VxVia[i].CtaCte_t))
                        {
                            s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + VB6Helpers.Chr(9);
                        }
                        else
                        {
                            s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + " " + VB6Helpers.Trim(MODXVIA.VxVia[i].CtaCte_t) + VB6Helpers.Chr(9);
                        }

                        s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NemMon) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[i].MtoTot)) + VB6Helpers.Chr(9);
                    }

                }

            }
            else
            {
                s = s + VB6Helpers.Str(m) + VB6Helpers.Chr(9);
            }

            return s;
        }

        public static short Nro_Vias(T_MODXVIA MODXVIA)
        {
            short _retValue = -1;
            short Contador1 = 0;
            using (Tracer tracer = new Tracer())
            {
                //Contar cuantas Vías existen que no esten eliminadas.
                try
                {
                    Contador1 = 0;
                    
                    for (int i = 0; i < MODXVIA.VxVia.Length; i++)
                    {
                        //se agrega que sea diferente a 0, por si viene algun dummy no lo cuente como via
                        if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && MODXVIA.VxVia[i].Status != 0)
                        {
                            Contador1++;
                        }
                    }
                    _retValue = Contador1;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta, problemas al calcular las vias en la funcion Nro_Vias", _ex);
                    _retValue = -1;
                }
            }
            return _retValue;
        }

        //****************************************************************************
        //   1.  Llena Datos Adicionales según Cuenta Contable. Esto se hace
        //       dependiendo del arreglo (VxOri -- VxVia).
        //****************************************************************************
        public static short SyDatosAdic(InitializationObject initObject, UnitOfWorkCext01 unit, string OriVia, short Indice)
        {
            T_MODGCON0 MODGCON0 = initObject.MODGCON0;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGSWF MODGSWF = initObject.MODGSWF;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;


            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;

            if (MODGCON0.VMcd.IdnCta == 0)
            {
                return 0;
            }

            string _switchVar1 = VB6Helpers.Trim(VB6Helpers.UCase(OriVia));
            //Origenes de Fondos.
            if (_switchVar1 == "O")
            {
                short _switchVar2 = MODGCON0.VMcd.IdnCta;
                if (_switchVar2 == T_MODGCON0.IdCta_CtaCteMN)
                {
                    //3
                    n = MODXORI.VxOri[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXORI.VxOri[Indice].ctacte);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CtaCteME || _switchVar2 == T_MODGCON0.IdCta_ChqCCME)
                {
                    //10
                    n = MODXORI.VxOri[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXORI.VxOri[Indice].ctacte);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_SCSMN)
                {
                    //9
                    MODGCON0.VMcd.OfiDes = MODXORI.VxOri[Indice].codofi;
                    MODGCON0.VMcd.NumPar = MODXORI.VxOri[Indice].NumPar;
                    MODGCON0.VMcd.TipMov = MODXORI.VxOri[Indice].TipMov;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_SCSME)
                {
                    //16
                    MODGCON0.VMcd.OfiDes = MODXORI.VxOri[Indice].codofi;
                    MODGCON0.VMcd.NumPar = MODXORI.VxOri[Indice].NumPar;
                    MODGCON0.VMcd.TipMov = MODXORI.VxOri[Indice].TipMov;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CHMEBCH)
                {
                    //11
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == -1)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VNom[n].Nom_Bco;
                    //Se trae la logica desde GL, para que al usar un cheque cambie la cuenta contable(bug 2049)
                    if (MODGTAB0.VNom[n].Nom_Emi != 0)
                    {
                        MODGCON0.VMcd.NemCta = "CHEME";
                    }
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CHMEOBC)
                {
                    //13
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == -1)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VNom[n].Nom_Bco;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_VVOB)
                {
                    //5
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CTACTEBC)
                {
                    //22
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    MODGCON0.VMcd.CodBco = T_MODGCON0.CodBcoBC;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CTAORD || _switchVar2 == T_MODGCON0.IdCta_CHVBNYM || _switchVar2 == T_MODGCON0.IdCta_BOEREG || _switchVar2 == T_MODGCON0.IdCta_CHEREG || _switchVar2 == T_MODGCON0.IdCta_OBLREG || _switchVar2 == T_MODGCON0.IdCta_OBLARE || _switchVar2 == T_MODGCON0.IdCta_ACEREG || _switchVar2 == 54)
                {
                    //IdCta_OBCCIPLZ, 54 '23,29,31,32,33,34,35
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == -1)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VNom[n].Nom_Bco;
                }
                else if (_switchVar2 >= 40 && _switchVar2 <= 53)
                {
                    //Cuentas de Obligaciones y Check Verification
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VAcr(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == 0)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VAcr[n].acr_bco;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_DIVENPEN)
                {
                    //24
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == -1)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VNom[n].Nom_Bco;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_VAM)
                {
                    //19
                    n = MODXORI.VxOri[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_VAX)
                {
                    //20
                    n = MODXORI.VxOri[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_VAMC || _switchVar2 == T_MODGCON0.IdCta_VAMCC || _switchVar2 == T_MODGCON0.IdCta_VASC)
                {
                    //21
                    n = MODXORI.VxOri[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_VVBCH)
                {
                    //4
                    MODGCON0.VMcd.OfiDes = MODXORI.VxOri[Indice].codofi;
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_OPC)
                {
                    //17
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    MODGCON0.VMcd.CodBco = T_MODGCON0.CodBcoBC;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_OPOP)
                {
                    //18
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXORI.VxOri[Indice].CodSwf);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXORI.VxOri[Indice].ctacte);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXORI.VxOri[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == -1)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VNom[n].Nom_Bco;
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_ONMN)
                {
                    //50
                    //Nemónico
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_ONME)
                {
                    //60
                    //Nemónico

                    //Cuentas GAP
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_GAPMN || _switchVar2 == T_MODGCON0.IdCta_GAPME)
                {
                    MODGCON0.VMcd.SwiBco = MODXORI.VxOri[Indice].SwiBco;
                    MODGCON0.VMcd.NroRef = MODXORI.VxOri[Indice].NroRef;
                    MODGCON0.VMcd.numcct = MODXORI.VxOri[Indice].ctacte;

                    //Cuentas Cosmos
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar2 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANE)
                {
                    n = MODXORI.VxOri[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXORI.VxOri[Indice].ctacte);
                }

                //Destinos de Fondos(Vías).
            }
            else if (_switchVar1 == "V")
            {
                short _switchVar3 = MODGCON0.VMcd.IdnCta;
                if (_switchVar3 == T_MODGCON0.IdCta_CtaCteMN)
                {
                    //3
                    n = MODXVIA.VxVia[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXVIA.VxVia[Indice].ctacte);
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_CtaCteME || _switchVar3 == T_MODGCON0.IdCta_ChqCCME)
                {
                    //10
                    n = MODXVIA.VxVia[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXVIA.VxVia[Indice].ctacte);
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_SCSMN)
                {
                    //9
                    MODGCON0.VMcd.OfiDes = MODXVIA.VxVia[Indice].codofi;
                    MODGCON0.VMcd.NumPar = MODXVIA.VxVia[Indice].NumPar;
                    MODGCON0.VMcd.TipMov = MODXVIA.VxVia[Indice].TipMov;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_SCSME)
                {
                    //16
                    MODGCON0.VMcd.OfiDes = MODXVIA.VxVia[Indice].codofi;
                    MODGCON0.VMcd.NumPar = MODXVIA.VxVia[Indice].NumPar;
                    MODGCON0.VMcd.TipMov = MODXVIA.VxVia[Indice].TipMov;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_VVOB)
                {
                    //5
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXVIA.VxVia[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXVIA.VxVia[Indice].numdoc);
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_CTACTEBC)
                {
                    //22
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXVIA.VxVia[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXVIA.VxVia[Indice].numdoc);
                    MODGCON0.VMcd.CodBco = T_MODGCON0.CodBcoBC;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_CTAORD || _switchVar3 == T_MODGCON0.IdCta_CHVBNYM || _switchVar3 == T_MODGCON0.IdCta_BOEREG || _switchVar3 == T_MODGCON0.IdCta_CHEREG || _switchVar3 == T_MODGCON0.IdCta_OBLREG || _switchVar3 == T_MODGCON0.IdCta_OBLARE || _switchVar3 == T_MODGCON0.IdCta_ACEREG || _switchVar3 == 54)
                {
                    //IdCta_OBCCIPLZ, 54 '23,29,31,32,33,34,35
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXVIA.VxVia[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXVIA.VxVia[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == -1)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VNom[n].Nom_Bco;

                    //Cuentas de Obligaciones y Check Verification
                }
                else if (_switchVar3 >= 40 && _switchVar3 <= 53)
                {
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXVIA.VxVia[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXVIA.VxVia[Indice].numdoc);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VAcr(initObject,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                    if (n == 0)
                    {
                        throw new Exception("No se podido establecer el Código del Banco para el Swift " + MODGCON0.VMcd.SwiBco + ". La Operación será cancelada.");
                    }

                    MODGCON0.VMcd.CodBco = MODGTAB0.VAcr[n].acr_bco;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_DIVENPEN)
                {
                    //24
                    MODGCON0.VMcd.SwiBco = VB6Helpers.Trim(MODXVIA.VxVia[Indice].CodSwf);
                    MODGCON0.VMcd.NroRef = VB6Helpers.Trim(MODXVIA.VxVia[Indice].numdoc);
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_VAM)
                {
                    //19
                    MODGCON0.VMcd.rutcli = MODXVIA.VxVia[Indice].IdPrty;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_VAX)
                {
                    //20
                    MODGCON0.VMcd.rutcli = MODXVIA.VxVia[Indice].IdPrty;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_VAMC || _switchVar3 == T_MODGCON0.IdCta_VAMCC || _switchVar3 == T_MODGCON0.IdCta_VASC)
                {
                    //21
                    MODGCON0.VMcd.rutcli = MODXVIA.VxVia[Indice].IdPrty;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_OPC)
                {
                    //17
                    MODGCON0.VMcd.SwiBco = MODGSWF.VSwf[Indice].BcoAla.SwfBco;
                    MODGCON0.VMcd.CodBco = MODGSCE.VGen.CodBCCh;
                    MODGCON0.VMcd.NroRef = MODGSWF.VSwf[Indice].DatSwf.NroAla;
                    MODGCON0.VMcd.FecVen = MODGSWF.VSwf[Indice].DatSwf.FecPag;
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_OPOP)
                {
                    //18
                    n = (short)(MODXVIA.VxVia[Indice].IndSwf-1);
                    MODGCON0.VMcd.SwiBco = MODGSWF.VSwf[n].DatSwf.SwfCor;
                    MODGCON0.VMcd.CodBco = MODGSWF.VSwf[n].DatSwf.BcoCor;
                    MODGCON0.VMcd.numcct = MODGSWF.VSwf[n].DatSwf.CtaCor;
                    MODGCON0.VMcd.NroRef = MODGSWF.VSwf[n].DatSwf.RefOpe;
                    MODGCON0.VMcd.FecVen = MODGSWF.VSwf[n].DatSwf.FecPag;
                    //Cambia la Cuenta Contable a OpePend.-
                    if (VB6Helpers.Format(MODGSWF.VSwf[n].DatSwf.FecPag, "yyyymmdd") != DateTime.Now.ToString("yyyyMMdd"))
                    {
                        MODGCON0.VMcd.IdnCta = T_MODGCON0.IdCta_OPEPEND;
                        MODGCON0.VMcd.NemCta = "OPEPEND";
                    }

                }
                else if (_switchVar3 == T_MODGCON0.IdCta_CHMEBCH)
                {
                    //11
                    n = MODXVIA.VxVia[Indice].IndChq;
                    MODGCON0.VMcd.SwiBco = MODGCHQ.V_Chq_VVi[n].SwfPag;
                    MODGCON0.VMcd.CodBco = MODGCHQ.V_Chq_VVi[n].BcoPag;
                    MODGCON0.VMcd.numcct = MODGCHQ.V_Chq_VVi[n].NumCta;
                    MODGCON0.VMcd.NroRef = MODGCON0.VMcd.codcct + MODGCON0.VMcd.codpro + MODGCON0.VMcd.codesp + MODGCON0.VMcd.codofi + MODGCON0.VMcd.codope;
                    //Cheques Emitidos xxx.-
                    if (MODGCHQ.V_Chq_VVi[n].ChqEmi != 0)
                    {
                        MODGCON0.VMcd.NemCta = "CHEME";
                    }

                }
                else if (_switchVar3 == T_MODGCON0.IdCta_ONMN)
                {
                    //50
                    //Nemónico
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_ONME)
                {
                    //60
                    //Nemónico

                    //Cuentas GAP
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_GAPMN || _switchVar3 == T_MODGCON0.IdCta_GAPME)
                {
                    MODGCON0.VMcd.SwiBco = MODXVIA.VxVia[Indice].SwiBco;
                    MODGCON0.VMcd.NroRef = MODXVIA.VxVia[Indice].NroRef;
                    MODGCON0.VMcd.numcct = MODXVIA.VxVia[Indice].ctacte;
                    //Cuentas Cosmos
                }
                else if (_switchVar3 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar3 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar3 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar3 == T_MODGCON0.IdCta_CtaCteMANE)
                {
                    n = MODXVIA.VxVia[Indice].PosPrty;
                    MODGCON0.VMcd.rutcli = VB6Helpers.Trim(Module1.PartysOpe[n].rut);
                    MODGCON0.VMcd.PrtCli = VB6Helpers.Trim(Module1.PartysOpe[n].LlaveArchivo);
                    MODGCON0.VMcd.numcct = VB6Helpers.Trim(MODXVIA.VxVia[Indice].ctacte);
                }

            }

            //Agregar numero partida en arreglo que graba el comprobante
            if ((VB6Helpers.Trim(VB6Helpers.UCase(OriVia)) == "O"))
            {
                if ((MODXORI.VxOri[Indice].NumPar > 0))
                {
                    MODGCON0.VMcd.NumPar = MODXORI.VxOri[Indice].NumPar;
                }

            }
            else if ((VB6Helpers.Trim(VB6Helpers.UCase(OriVia)) == "V"))
            {
                if ((MODXVIA.VxVia[Indice].NumPar > 0))
                {
                    MODGCON0.VMcd.NumPar = MODXVIA.VxVia[Indice].NumPar;
                }

            }

            return (short)(true ? -1 : 0);
        }

        //Realiza el Rebaje de los montos de Fletes y Seguro de las
        //Declaraciones de Exportaciones.-
        public static short Rebaja_xDec_Inv(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer("Rebaja_xDec_Inv: Realiza el Rebaje de los montos de Fletes y Seguro de las Declaraciones de Exportaciones"))
            {
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                T_Module1 Module1 = initObject.Module1;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                dynamic TcpSeg = null;
                short mnd = 0;
                string numdec = "";
                short i = 0;
                short indiceM = 0;
                short n = 0;
                short X = 0;
                string p = "";
                const string TcpFle = "25111901K";
                TcpSeg = "251305014";

                MODXPLN0.VxDecP = new T_xDecP[0];

                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if ((MODGCVD.VgPli[i].TipCVD == "V" || MODGCVD.VgPli[i].TipCVD == "W") &&
                        !string.IsNullOrEmpty(MODGCVD.VgPli[i].numdec) &&
                        (MODGCVD.VgPli[i].CodTcp == TcpFle || MODGCVD.VgPli[i].CodTcp == TcpSeg) &&
                        (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli))
                    {
                        mnd = MODGCVD.VgPli[i].CodMnd;
                        indiceM = (short)(indiceM + 1);
                        numdec = VB6Helpers.Right("0000000" + MODGCVD.VgPli[i].numdec, 7);
                        n = BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.Get_xDec(initObject, numdec, MODGCVD.VgPli[i].FecDec,
                            MODGCVD.VgPli[i].CodAdn);
                        if (n < 0)
                        {
                            X = SyGet_xDec(Mdl_Funciones_Varias, unit, numdec, MODGCVD.VgPli[i].FecDec, MODGCVD.VgPli[i].CodAdn);
                            if (X != 0)
                            {
                                n = BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.Put_xDec(initObject, unit, MODGCVD.VgPli[i].CodMnd, -1);
                                BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.GetDis_xDec(MODXPLN0, n);
                            }
                            else
                            {
                                return (short)(true ? -1 : 0);
                            }

                        }

                        if (X != 0)
                        {
                            //Rebaja Saldo Dec. Exportador 1.
                            p = Module1.PartysOpe[0].LlaveArchivo;
                            p = MODGPYF0.Componer(p, "~", "");
                            if (p == MODXPLN0.VxDecP[n].PrtExp1 && Module1.PartysOpe[0].IndNombre == MODXPLN0.VxDecP[n].IndNom1)
                            {
                                string _switchVar1 = MODGCVD.VgPli[i].CodTcp;
                                if (_switchVar1 == "25111901K")
                                {
                                    MODXPLN0.VxDecP[n].ValFle1c = MODXPLN0.VxDecP[n].ValFle1c + Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle1c) + (MODGCVD.VgPli[i].MtoCVD), "0.00"));
                                }
                                else if (_switchVar1 == "251305014")
                                {
                                    MODXPLN0.VxDecP[n].ValSeg1c = MODXPLN0.VxDecP[n].ValSeg1c + Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg1c) + (MODGCVD.VgPli[i].MtoCVD), "0.00"));
                                }

                                MODXPLN0.VxDecP[n].ValFle1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle1 - MODXPLN0.VxDecP[n].ValFle1c), "0.00"));
                                MODXPLN0.VxDecP[n].ValSeg1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg1 - MODXPLN0.VxDecP[n].ValSeg1c), "0.00"));
                            }

                            //Rebaja Saldo Dec. Exportador 2.
                            if (p == MODXPLN0.VxDecP[n].PrtExp2 && Module1.PartysOpe[MODXPLN0.VxDatP.IndPrt].IndNombre == MODXPLN0.VxDecP[n].IndNom2)
                            {
                                string _switchVar2 = MODGCVD.VgPli[i].CodTcp;
                                if (_switchVar2 == "25111901K")
                                {
                                    MODXPLN0.VxDecP[n].ValFle2c = MODXPLN0.VxDecP[n].ValFle2c + Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle2c) + (MODGCVD.VgPli[i].MtoCVD), "0.00"));
                                }
                                else if (_switchVar2 == "251305014")
                                {
                                    MODXPLN0.VxDecP[n].ValSeg2c = MODXPLN0.VxDecP[n].ValSeg2c + Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg2c) + (MODGCVD.VgPli[i].MtoCVD), "0.00"));
                                }

                                MODXPLN0.VxDecP[n].ValFle2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle2 - MODXPLN0.VxDecP[n].ValFle2c), "0.00"));
                                MODXPLN0.VxDecP[n].ValSeg2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg2 - MODXPLN0.VxDecP[n].ValSeg2c), "0.00"));
                            }

                            //Verifica Rebajes por sobre el disponible.-
                            if ((MODXPLN0.VxDecP[n].ValFle1d < 0 || MODXPLN0.VxDecP[n].ValFle2d < 0))
                            {
                                Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "El valor de venta de Flete de la Declaración " + MODGCVD.VgPli[i].numdec + " excede el monto disponible. Corrija los valores y reintente la Operación."
                                });
                                return 0;
                            }

                            if ((MODXPLN0.VxDecP[n].ValSeg1d < 0 || MODXPLN0.VxDecP[n].ValSeg2d < 0))
                            {
                                string errorMessage = "El valor de venta de Seguro de la Declaración " + MODGCVD.VgPli[i].numdec + " excede el monto disponible. Corrija los valores y reintente la Operación.";
                                Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = errorMessage
                                });
                                trace.TraceError(errorMessage);
                                return 0;
                            }

                        }

                    }

                }

                if (indiceM == 0)
                {
                    return (short)(true ? -1 : 0);
                }

                //Se convierten los montos a la moneda original.-
                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.RPut_xDec(initObject, unit, mnd) != 0)
                {
                    return 0;
                }

                return (short)(true ? -1 : 0);
            }
        }

        public static void Log_Cvd(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            T_MODGPLI1 MODGPLI1 = initObject.MODGPLI1;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_Module1 Module1 = initObject.Module1;
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            // UPGRADE_INFO (#05B1): The 'Vli' variable wasn't declared explicitly.
            short Vli = 0;
            // UPGRADE_INFO (#05B1): The 'Arb' variable wasn't declared explicitly.
            short Arb = 0;
            // UPGRADE_INFO (#05B1): The 'pli' variable wasn't declared explicitly.
            short pli = 0;
            // UPGRADE_INFO (#05B1): The 'Ope' variable wasn't declared explicitly.
            string Ope = "";
            // UPGRADE_INFO (#05B1): The 'rut' variable wasn't declared explicitly.
            string rut = "";
            // UPGRADE_INFO (#05B1): The 'tip' variable wasn't declared explicitly.
            string tip = "";
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'Tpli' variable wasn't declared explicitly.
            string Tpli = "";
            // UPGRADE_INFO (#05B1): The 'Nem' variable wasn't declared explicitly.
            string Nem = "";
            // UPGRADE_INFO (#05B1): The 'mto' variable wasn't declared explicitly.
            string mto = "";
            // UPGRADE_INFO (#05B1): The 'TC' variable wasn't declared explicitly.
            string TC = "";
            // UPGRADE_INFO (#05B1): The 'datos' variable wasn't declared explicitly.
            string datos = "";
            // UPGRADE_INFO (#05B1): The 'NemC' variable wasn't declared explicitly.
            string NemC = "";
            // UPGRADE_INFO (#05B1): The 'MtoC' variable wasn't declared explicitly.
            string MtoC = "";
            // UPGRADE_INFO (#05B1): The 'NemV' variable wasn't declared explicitly.
            string NemV = "";
            // UPGRADE_INFO (#05B1): The 'MtoV' variable wasn't declared explicitly.
            string MtoV = "";
            // UPGRADE_INFO (#05B1): The 'NemM' variable wasn't declared explicitly.
            string NemM = "";
            // UPGRADE_INFO (#05B1): The 'MtoL' variable wasn't declared explicitly.
            string MtoL = "";
            // UPGRADE_INFO (#05B1): The 'MtoI' variable wasn't declared explicitly.
            string MtoI = "";
            // UPGRADE_INFO (#05B1): The 'MtoE' variable wasn't declared explicitly.
            string MtoE = "";
            // UPGRADE_INFO (#05B1): The 'TipC' variable wasn't declared explicitly.
            string TipC = "";
            // UPGRADE_INFO (#05B1): The 'lvs' variable wasn't declared explicitly.
            short lvs = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'MtoB' variable wasn't declared explicitly.
            string MtoB = "";
            // UPGRADE_INFO (#05B1): The 'MtoP' variable wasn't declared explicitly.
            string MtoP = "";
            // UPGRADE_INFO (#05B1): The 'MtoD' variable wasn't declared explicitly.
            string MtoD = "";
            // UPGRADE_INFO (#05B1): The 'NumPli' variable wasn't declared explicitly.
            string NumPli = "";
            // UPGRADE_INFO (#05B1): The 'FecPli' variable wasn't declared explicitly.
            string FecPli = "";
            // UPGRADE_INFO (#05B1): The 'MtoOpe' variable wasn't declared explicitly.
            string MtoOpe = "";
            // UPGRADE_INFO (#05B1): The 'Mtopar' variable wasn't declared explicitly.
            string Mtopar = "";
            // UPGRADE_INFO (#05B1): The 'MtoDol' variable wasn't declared explicitly.
            string MtoDol = "";
            // UPGRADE_INFO (#05B1): The 'TipCam' variable wasn't declared explicitly.
            string TipCam = "";
            // UPGRADE_INFO (#05B1): The 'MtoNac' variable wasn't declared explicitly.
            string MtoNac = "";

            
            Vli = (short)VB6Helpers.UBound(MODGPLI1.Vplis);
            Arb = (short)VB6Helpers.UBound(MODGARB.VArb);
            pli = (short)VB6Helpers.UBound(MODGCVD.VgPli);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            Ope = MODGCVD.VgCvd.OpeSin;
            rut = Module1.PartysOpe[0].rut;
            if (MODGCVD.VgCvd.TipCVD == 1)
            {
                tip = "CVD";
            }

            if (MODGCVD.VgCvd.TipCVD == 2)
            {
                tip = "ARB";
            }

            if (MODGCVD.VgCvd.TipCVD == 3)
            {
                tip = "VEX";
            }

            //Se verifica el tipo de operción realizada
            //-----------------------------------------
            short _switchVar1 = MODGCVD.VgCvd.TipCVD;
            //Planillas invisibles ( Compra/Venta )
            if (_switchVar1 == 1)
            {
                for (i = 1; i <= (short)pli; i++)
                {
                    Tpli = MODGCVD.VgPli[i].TipCVD;
                    Nem = VB6Helpers.Right(VB6Helpers.Space(3) + MODGCVD.VgPli[i].NemMnd, 3);
                    mto = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGCVD.VgPli[i].MtoCVD), T_MODGCON0.FormatoConDec), 20);
                    TC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGCVD.VgPli[i].TipCam), "###,###,###,##0.0000"), 20);
                }

                datos = Tpli + " " + Nem + " " + mto + " " + TC;
                

                //Arbitrajes
            }
            else if (_switchVar1 == 2)
            {
                for (i = 1; i <= (short)Arb; i++)
                {
                    NemC = VB6Helpers.Right(VB6Helpers.Space(3) + MODGARB.VArb[i].NemMndC, 3);
                    MtoC = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.CStr(MODGARB.VArb[i].MtoCom), T_MODGCON0.FormatoConDec), 20);
                    NemV = VB6Helpers.Right(VB6Helpers.Space(3) + MODGARB.VArb[i].NemMndV, 3);
                    MtoV = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.CStr(MODGARB.VArb[i].MtoVta), T_MODGCON0.FormatoConDec), 20);
                }

                datos = NemC + " " + MtoC + " " + NemV + " " + MtoV;
                

                //Planillas exportaciones
            }
            else if (_switchVar1 == 3)
            {
                NemM = VB6Helpers.Right(VB6Helpers.Space(3) + MODXPLN0.VxDatP.NemMnd, 3);
                MtoL = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN0.VxDatP.MtoLiq), T_MODGCON0.FormatoConDec), 20);
                MtoI = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN0.VxDatP.MtoInf), T_MODGCON0.FormatoConDec), 20);
                MtoE = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN0.VxDatP.mtotran), T_MODGCON0.FormatoConDec), 20);
                TipC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN0.VxDatP.TipCam), "###,###,###,##0.0000"), 20);
                datos = NemM + " " + MtoL + " " + MtoI + " " + MtoE + " " + TipC;
                
                lvs = (short)VB6Helpers.UBound(MODXPLN1.VxPlvs);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0
                for (i = 0; i <= (short)lvs; i++)
                {
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit, MODXPLN1.VxPlvs[i].CodMnd);
                    NemM = VB6Helpers.Right(VB6Helpers.Space(3) + MODGTAB0.VMnd[n].Mnd_MndNmc, 3);
                    MtoB = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN1.VxPlvs[i].MtoBru), T_MODGCON0.FormatoConDec), 20);
                    MtoL = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN1.VxPlvs[i].MtoLiq), T_MODGCON0.FormatoConDec), 20);
                    MtoP = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN1.VxPlvs[i].Mtopar), "###,###,###,##0.0000"), 20);
                    MtoD = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN1.VxPlvs[i].MtoDol), T_MODGCON0.FormatoConDec), 20);
                    TipC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXPLN1.VxPlvs[i].TipCam), "###,###,###,##0.0000"), 20);
                    datos = NemM + " " + MtoB + " " + MtoL + " " + MtoP + " " + MtoD + " " + TipC;
                }

            }

        
            Vli = (short)VB6Helpers.UBound(MODGPLI1.Vplis);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            for (i = 0; i <= (short)Vli; i++)
            {
                NumPli = MODGPLI1.Vplis[i].NumPli;
                FecPli = VB6Helpers.Format(MODGPLI1.Vplis[i].FecPli, "dd/MM/yyyy");
                MtoOpe = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGPLI1.Vplis[i].MtoOpe), T_MODGCON0.FormatoConDec), 20);
                Mtopar = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGPLI1.Vplis[i].Mtopar), "###,###,###,##0.0000"), 20);
                MtoDol = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGPLI1.Vplis[i].MtoDol), T_MODGCON0.FormatoConDec), 20);
                TipCam = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGPLI1.Vplis[i].TipCam), "###,###,###,##0.0000"), 20);
                MtoNac = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGPLI1.Vplis[i].MtoNac), T_MODGCON0.FormatoConDec), 20);
                datos = NumPli + " " + FecPli + " " + MtoOpe + " " + Mtopar + " " + MtoDol + " " + TipCam + " " + MtoNac;
            }

        }

        public static void Log_Anu(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGANU MODGANU = initObject.MODGANU;
            T_MODXANU MODXANU = initObject.MODXANU;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;


            // UPGRADE_INFO (#05B1): The 'Ope' variable wasn't declared explicitly.
            string Ope = "";
            // UPGRADE_INFO (#05B1): The 'rut' variable wasn't declared explicitly.
            string rut = "";
            // UPGRADE_INFO (#05B1): The 'Upl' variable wasn't declared explicitly.
            short Upl = 0;
            // UPGRADE_INFO (#05B1): The 'Nus' variable wasn't declared explicitly.
            short Nus = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'NumPln' variable wasn't declared explicitly.
            string NumPln = "";
            // UPGRADE_INFO (#05B1): The 'FecPln' variable wasn't declared explicitly.
            string FecPln = "";
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'NemM' variable wasn't declared explicitly.
            string NemM = "";
            // UPGRADE_INFO (#05B1): The 'MtoA' variable wasn't declared explicitly.
            string MtoA = "";
            // UPGRADE_INFO (#05B1): The 'datos' variable wasn't declared explicitly.
            string datos = "";
            // UPGRADE_INFO (#05B1): The 'NumPre' variable wasn't declared explicitly.
            string NumPre = "";
            // UPGRADE_INFO (#05B1): The 'fecpre' variable wasn't declared explicitly.
            string fecpre = "";
            // UPGRADE_INFO (#05B1): The 'MtoD' variable wasn't declared explicitly.
            string MtoD = "";
            // UPGRADE_INFO (#05B1): The 'MtoP' variable wasn't declared explicitly.
            string MtoP = "";
            //****************************************************************
            // genero log para reverso de operaciones                        *
            //****************************************************************

            Ope = MODGCVD.VgCvd.OpeSin;
            rut = MODGCVD.VgCvd.PrtCli;
            
            Upl = (short)VB6Helpers.UBound(MODGANU.VAnuPl);
            Nus = (short)VB6Helpers.UBound(MODXANU.VxAnus);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (Upl > 0 || Nus > 0)
            {
                for (i = 1; i <= (short)Upl; i++)
                {
                    if (MODGANU.VAnuPl[i].MtoAnu > 0)
                    {
                        NumPln = MODGANU.VAnuPl[i].NumPln;
                        FecPln = MODGANU.VAnuPl[i].FecPln;
                        n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit, MODGANU.VAnuPl[i].CodMnd);
                        NemM = VB6Helpers.Right(VB6Helpers.Space(3) + MODGTAB0.VMnd[n].Mnd_MndNmc, 3);
                        MtoA = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODGANU.VAnuPl[i].MtoAnu), T_MODGCON0.FormatoConDec), 20);
                        datos = NumPln + " " + FecPln + " " + NemM + " " + MtoA;
                    }

                }

                for (i = 1; i <= (short)Nus; i++)
                {
                    NumPre = VB6Helpers.Right(VB6Helpers.Space(7) + MODXANU.VxAnus[i].NumPre, 7);
                    fecpre = MODXANU.VxAnus[i].fecpre;
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit, MODXANU.VxAnus[i].CodMnd);
                    NemM = VB6Helpers.Right(VB6Helpers.Space(3) + MODGTAB0.VMnd[n].Mnd_MndNmc, 3);
                    MtoD = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXANU.VxAnus[i].MtoDol), T_MODGCON0.FormatoConDec), 20);
                    MtoP = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXANU.VxAnus[i].Mtopar), "###,###,###,##0.0000"), 20);
                    MtoA = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((MODXANU.VxAnus[i].MtoAnu), T_MODGCON0.FormatoConDec), 20);
                    datos = NumPre + " " + fecpre + " " + NemM + " " + MtoD + " " + MtoP + " " + MtoA;
                }

            }

        }

        //TODO: GRABAR - IMPRIMIR
        //****************************************************************************
        //   1.  Imprime las n copias de todas las planillas Invisible-Export.-
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_Imprime_nPli(InitializationObject initObject, UnitOfWorkCext01 unit, short Copias)
        {
            using (Tracer tracer = new Tracer("Imprime las n copias de todas las planillas Invisible-Export de FundTransfer"))
            {
                tracer.TraceVerbose("Entrando a Pr_Imprime_nPli de FT...");
                
                T_MODGPLI1 MODGPLI1 = initObject.MODGPLI1;
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                T_MODABDC MODABDC = initObject.MODABDC;

                short n = 0;
                short i = 0;
                short j = 0;

                n = (short)VB6Helpers.UBound(MODGPLI1.Vplis);
                if (n > 0)
                {
                    //------------------------------------------------------------------------------------------------------------------
                    //Accenture-Código Nuevo-Inicio
                    //Fecha Modificación 22022012
                    //Responsable: Angel Donoso Gonzalez.
                    //Versión:
                    //Descripción : se agrega nueva condición para controlar cuando debe mostrar o no el mensaje de impresión planillas
                    //-------------------------------------------------------------------------------------------------------------------
                    if (MODGCVD.TIN != true || MODABDC.Ftin == 1)
                    {
                        //--------------------------------------------------------------------------------------------------
                        // Accenture - Código Nuevo - Termino
                        //--------------------------------------------------------------------------------------------------
                        initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "Prepare Formulario de Planillas Invisibles.",
                            Title = "Impresión de Planillas"
                        });
                    }
                    //--------------------------------------------------------------------------------------------------
                    // Accenture - Código Nuevo - Inicio
                    // Fecha Modificación 22022012
                    // Responsable: Angel Donoso Gonzalez.
                    // Versión:
                    // Descripción : cierre de if de nueva condición.
                    //--------------------------------------------------------------------------------------------------
                }
                //--------------------------------------------------------------------------------------------------
                // Accenture - Código Nuevo - Termino
                //--------------------------------------------------------------------------------------------------
                for (i = 0; i <= (short)n; i++)
                {
                    if (MODGPLI1.Vplis[i].Status != T_MODGCVD.EstadoEli)
                    {
                        for (j = 1; j <= (short)Copias; j++)
                        {
                            Pr_Imprime_Pli(initObject, unit, i);
                        }
                    }
                }
            }
        }
        
        //***************************************************************************
        // Sub       : Pr_Imprime_Pli()                                             *
        // Objetivo  : Imprime la Planilla Invisible.                               *
        // Parametro : Indice .- indica la posicion dentro del arreglo sobre         *
        //             el cual se va a imprimir.                                     *
        //***************************************************************************
        public static void Pr_Imprime_Pli(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            using (Tracer tracer = new Tracer("Imprime la Planilla Invisible de FundTransfer"))
            {
                tracer.TraceVerbose("Entrando a Pr_Imprime_Pli de FT...");
                
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                T_MODGPLI1 MODGPLI1 = initObject.MODGPLI1;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                T_MODGTAB1 MODGTAB1 = initObject.MODGTAB1;

                short i = 0;
                string n = "";
                short m = 0;
                string rut = "";
                string s = "";
                short a = 0;
                string Texto = "";
                string palabra = "";
                dynamic co = null;
                string letra = "";
                //--------------------------------------------------------------------------------------------------------
                //Accenture-Código Nuevo-Inicio
                //Fecha Modificación 22022012
                //Responsable: Angel Donoso Gonzalez.
                //Versión:
                //Descripción : cambio de lugar de asignacion del indice para recorrer estructura,
                //              se agrega condicion para que solo imprima las planillas distintas a Transferencia Interna y
                //              declaramos variable para diferenciar las operaciones con transferencia interna.
                //--------------------------------------------------------------------------------------------------------
                bool pasa = false;
                i = Indice;
                if (MODGCVD.VgPli.Length == 0)
                {
                    pasa = true;
                }
                else if (MODGCVD.VgPli[i].TipCVD != "TIN")
                {
                    pasa = true;
                }

                if (pasa)
                {
                    PlanillaInvisible model = new PlanillaInvisible();
                    //-------------------
                    //Número Presentación
                    //-------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].NumPli))
                    {
                        n = MODGPLI1.Vplis[i].NumPli;
                        model.Vplis_NumPli = (VB6Helpers.Trim(n));
                    }

                    //-----------
                    //Código Pais
                    //-----------
                    if (MODGPLI1.Vplis[i].codpai != 0)
                    {
                        m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VPai(initObject, unit, MODGPLI1.Vplis[i].codpai);
                        if (m != 0)
                        {
                            model.VPai_Pai_PaiNom = (VB6Helpers.Trim(MODGPYF1.Minuscula(MODGTAB0.VPai[m].Pai_PaiNom)));
                        }
                        model.Vplis_codpai = VB6Helpers.Trim(VB6Helpers.Format(MODGPLI1.Vplis[i].codpai, "000"));
                    }

                    //-------------------
                    //Código de Operación
                    //-------------------
                    if (MODGPLI1.Vplis[i].CodOci != 0)
                    {
                        model.Vplis_CodOci = (MODGPLI1.Vplis[i].CodOci);
                    }

                    //------------------
                    //Fecha Presentación
                    //------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].FecPli))
                    {
                        model.Vplis_FecPli = (DateTime.Parse(MODGPLI1.Vplis[i].FecPli).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }

                    //-----------------------------------
                    //Plaza Banco Central que Contabiliza
                    //-----------------------------------
                    if (MODGPLI1.Vplis[i].PlzBcc != 0)
                    {
                        m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, MODGPLI1.Vplis[i].PlzBcc);
                        if (m >= 0)
                        {
                            model.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                        }

                        //------------------------------------------
                        //Código Plaza Banco Central que Contabiliza
                        //------------------------------------------
                        model.Vplis_PlzBcc = (VB6Helpers.Format(MODGPLI1.Vplis[i].PlzBcc, "00"));
                    }

                    //------
                    //Moneda
                    //------
                    if (MODGPLI1.Vplis[i].CodMnd != 0)
                    {
                        m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODGPLI1.Vplis[i].CodMnd);
                        if (m != 0)
                        {
                            model.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGPYF1.Minuscula(MODGTAB0.VMnd[m].Mnd_MndNom)));
                        }

                        //-------------
                        //Código Moneda
                        //-------------
                        model.Vplis_CodMndBC = (VB6Helpers.Trim(VB6Helpers.Format(MODGPLI1.Vplis[i].CodMndBC, "000")));
                    }

                    //-----------------
                    //Tipo de Operación
                    //-----------------
                    if (MODGPLI1.Vplis[i].TipPln != 0)
                    {
                        model.Vplis_TipPln = (MODGPLI1.Vplis[i].TipPln);
                    }

                    //---
                    //Rut
                    //---
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].rutcli))
                    {
                        rut = MODXPLN1.ConvRut(VB6Helpers.Trim(MODGPLI1.Vplis[i].rutcli));
                        model.Vplis_rutcli = (VB6Helpers.Mid(rut, 1, VB6Helpers.Len(rut) - 1) + "-" + VB6Helpers.Mid(rut, VB6Helpers.Len(rut), 1));
                    }

                    //---------------------
                    //Nombre del Interesado
                    //---------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].PrtCli))
                    {
                        model.DatPrt1 = (VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObject, unit, MODGPLI1.Vplis[i].PrtCli, MODGPLI1.Vplis[i].IndNom, MODGPLI1.Vplis[i].IndDir, "N")));
                    }

                    //---------------
                    //Monto Operación
                    //---------------
                    if (MODGPLI1.Vplis[i].MtoOpe != 0)
                    {
                        n = Format.FormatCurrency(MODGPLI1.Vplis[i].MtoOpe, "#,###,###,###,##0.00");
                        model.Vplis_MtoOpe = n;
                    }

                    //------------------
                    //Dirección completa
                    //------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].PrtCli))
                    {
                        model.DatPrt2 = (VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObject, unit, MODGPLI1.Vplis[i].PrtCli, MODGPLI1.Vplis[i].IndNom, MODGPLI1.Vplis[i].IndDir, "D")));
                    }

                    //-------------
                    //Valor Paridad
                    //-------------
                    if (MODGPLI1.Vplis[i].Mtopar != 0)
                    {
                        n = Format.FormatCurrency(MODGPLI1.Vplis[i].Mtopar, "#,###,##0.0000");
                        model.Vplis_Mtopar = n;
                    }

                    //-------------------------
                    //Nombre Código de Comercio
                    //-------------------------
                    s = MODGPLI1.Vplis[i].codcom + MODGPLI1.Vplis[i].Concep;
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VTcp(MODGTAB1, unit, s);
                        if (m >= 0)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGTAB1.VTcp[m].DesTcp))
                            {
                                model.VTcp_DesTcp = (VB6Helpers.Trim(MODGTAB1.VTcp[m].DesTcp));
                            }
                        }
                    }

                    s = MODGPLI1.Vplis[i].codcom;
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        model.Vplis_codcom = (VB6Helpers.Trim(VB6Helpers.Left(s, 2) + "." + VB6Helpers.Mid(s, 3, 2) + "." + VB6Helpers.Right(s, 2)));
                    }

                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].Concep))
                    {
                        model.Vplis_Concep = (VB6Helpers.Format(MODGPLI1.Vplis[i].Concep, "000"));
                    }

                    //----------------
                    //Monto en Dolares
                    //----------------
                    if (MODGPLI1.Vplis[i].MtoDol != 0)
                    {
                        n = Format.FormatCurrency(MODGPLI1.Vplis[i].MtoDol, "#,###,###,###,##0.00");
                        model.Vplis_MtoDol = n;
                    }

                    //-------------------------
                    //Datos de Planilla Anulada
                    //Nro. Planilla Anulada
                    //-------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].AnuNum))
                    {
                        n = VB6Helpers.Format(MODGPLI1.Vplis[i].AnuNum, "0000000");
                        model.Vplis_AnuNum = (VB6Helpers.Trim(n));
                    }

                    //----------------------
                    //Fecha Planilla Anulada
                    //----------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].AnuFec))
                    {
                        model.Vplis_AnuFec = (DateTime.Parse(MODGPLI1.Vplis[i].AnuFec).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }

                    //---------------------------------------
                    //Plaza Banco Central de Planilla Anulada
                    //---------------------------------------
                    if (MODGPLI1.Vplis[i].AnuPbc != 0)
                    {
                        n = VB6Helpers.Format(VB6Helpers.CStr(MODGPLI1.Vplis[i].AnuPbc), "000");
                        model.Vplis_AnuPbc = (VB6Helpers.Trim(n));
                    }

                    //------------------------------
                    //Tipo de Cambio de la Operación
                    //------------------------------
                    if (MODGPLI1.Vplis[i].TipCam != 0)
                    {
                        n = Format.FormatCurrency(MODGPLI1.Vplis[i].TipCam, "#,###,##0.0000");
                        model.Vplis_TipCam = n;
                    }

                    //--------------------
                    //Tipo de Autorización
                    //--------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].ApcTip))
                    {
                        model.Vplis_ApcTip = (VB6Helpers.Trim(VB6Helpers.UCase(MODGPLI1.Vplis[i].ApcTip)));
                    }

                    //----------------
                    //Nro. de Planilla
                    //----------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].ApcNum))
                    {
                        n = VB6Helpers.Format(MODGPLI1.Vplis[i].ApcNum, "000000");
                        model.VplisApcNum = (VB6Helpers.Trim(n));
                    }

                    //--------------------
                    //Fecha de la Planilla
                    //--------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].ApcFec))
                    {
                        model.Vplis_ApcFec = (DateTime.Parse(MODGPLI1.Vplis[i].ApcFec).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }

                    //---------------------------------------
                    //Plaza Banco Central de Planilla Anulada
                    //---------------------------------------
                    if (MODGPLI1.Vplis[i].ApcPbc != 0)
                    {
                        n = VB6Helpers.Format(VB6Helpers.CStr(MODGPLI1.Vplis[i].ApcPbc), "000");
                        model.Vplis_ApcPbc = (VB6Helpers.Trim(n));
                    }
                    //--------------
                    //Monto Nacional
                    //--------------
                    if (MODGPLI1.Vplis[i].MtoNac != 0)
                    {
                        //9
                        n = Format.FormatCurrency(MODGPLI1.Vplis[i].MtoNac, "#,###,###,###,##0.00");
                        model.Vplis_MtoNac = n;
                    }

                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].Desacu))
                    {
                        m = MODGPYF0.cuentadestring(MODGPLI1.Vplis[i].Desacu, ";");
                        model.Vplis_Desacu.Add(VB6Helpers.Trim(VB6Helpers.Str(m)));
                        for (a = 1; a <= (short)m; a++)
                        {
                            model.Vplis_Desacu.Add(VB6Helpers.Trim(MODGPYF0.copiardestring(MODGPLI1.Vplis[Indice].Desacu, ";", a)));
                        }

                    }

                    //-----------------------
                    //Fecha de Aut. de Débito
                    //-----------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].FecDeb))
                    {
                        model.Vplis_FecDeb = (DateTime.Parse(MODGPLI1.Vplis[i].FecDeb).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }

                    //------------------
                    //Documento Nacional
                    //------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].DocNac))
                    {
                        model.Vplis_DocNac = (VB6Helpers.Trim(MODGPLI1.Vplis[i].DocNac));
                    }

                    //---------------------------------
                    //Número del IDE....INF.EXPORTACION
                    //---------------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].DieNum))
                    {
                        n = MODGPLI1.Vplis[Indice].DieNum;
                        model.Vplis_DieNum = (VB6Helpers.Trim(VB6Helpers.Mid(n, 1, VB6Helpers.Len(n) - 1) + "-" + VB6Helpers.Mid(n, VB6Helpers.Len(n), 1)));
                    }

                    //--------------------------------
                    //Fecha del IDE....INF.EXPORTACION
                    //--------------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].DieFec))
                    {
                        model.Vplis_DieFec = (DateTime.Parse(MODGPLI1.Vplis[i].DieFec).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }

                    //-----------------------------------
                    //PLAZA BCO CENTRAL...INF.EXPORTACION
                    //-----------------------------------
                    if (MODGPLI1.Vplis[i].DiePbc != 0)
                    {
                        model.Vplis_DiePbc = (VB6Helpers.Trim(VB6Helpers.Str(MODGPLI1.Vplis[i].DiePbc)));
                    }

                    //------------------------
                    //Documento Extranjero....
                    //------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].DocExt))
                    {
                        model.Vplis_DocExt = (VB6Helpers.Trim(MODGPLI1.Vplis[i].DocExt) + " QQ");
                    }

                    //---------------------------------------------
                    // Documento de Reexportación...INF.EXPORTACION
                    //---------------------------------------------
                    if (!string.IsNullOrEmpty(MODGPLI1.Vplis[i].CodEOR))
                    {
                        model.Vplis_CodEOR = (MODGPLI1.Vplis[i].CodEOR);
                    }

                    //-----------------------------------
                    //Código de Aduana....INF.EXPORTACION
                    //-----------------------------------
                    if (MODGPLI1.Vplis[i].CodAdn != 0)
                    {
                        model.Vplis_CodAdn = (VB6Helpers.Trim(VB6Helpers.Str(MODGPLI1.Vplis[i].CodAdn)));
                    }

                    //------------------------------------------
                    //Número de la Declaración...INF.EXPORTACION
                    //------------------------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].numdec))
                    {
                        model.Vplis_numdec = (VB6Helpers.Trim(VB6Helpers.Left(MODGPLI1.Vplis[i].numdec, VB6Helpers.Len(MODGPLI1.Vplis[i].numdec) - 1) + "-" + VB6Helpers.Right(MODGPLI1.Vplis[i].numdec, 1)));
                    }

                    //-----------------------------------------
                    //Fecha de la Declaración...INF.EXPORTACION
                    //-----------------------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].FecDec))
                    {
                        model.Vplis_FecDec = (DateTime.Parse(MODGPLI1.Vplis[i].FecDec).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }

                    //-------------------------------
                    //Observaciones...INF.EXPORTACION
                    //-------------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].ObsPli))
                    {
                        Texto = MODGPYF0.Componer(VB6Helpers.Trim(MODGPLI1.Vplis[i].ObsPli), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                        Texto = Texto + "&";
                        palabra = "";
                        for (double co_Alias = 1; co_Alias <= VB6Helpers.Len(Texto); co_Alias++)
                        {
                            co = co_Alias;
                            letra = VB6Helpers.Mid(Texto, VB6Helpers.CInt(co), 1);
                            if (letra == " " || letra == "&")
                            {
                                if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                                {
                                    model.Palabras.Add(palabra);
                                    palabra = "";
                                }
                                else
                                {
                                    palabra += " ";
                                }
                            }
                            else
                            {
                                palabra += letra;
                            }
                        }
                    }

                    //----------------------
                    //Numéro Credito Externo
                    //----------------------
                    if (MODGPLI1.Vplis[i].NumCre != 0)
                    {
                        n = VB6Helpers.Str(MODGPLI1.Vplis[Indice].NumCre);
                        model.Vplis_NumCre = (VB6Helpers.Trim(n));
                    }

                    //--------------------------------
                    //Fecha desembolso Credito Externo
                    //--------------------------------
                    if (!string.IsNullOrWhiteSpace(MODGPLI1.Vplis[i].FecCre))
                    {
                        model.Vplis_FecCr = (DateTime.Parse(MODGPLI1.Vplis[i].FecCre).ToString("dd/MM/yyyy"));
                    }

                    //----------------------------------------
                    //Codigo Moneda Desembolso Credito Externo
                    //----------------------------------------
                    if (MODGPLI1.Vplis[i].MndCre != 0)
                    {
                        model.Vplis_MndCre = (VB6Helpers.Trim(VB6Helpers.Str(MODGPLI1.Vplis[i].MndCre)));
                    }

                    //----------------------------------------
                    //Codigo Moneda Desembolso Credito Externo
                    //----------------------------------------
                    if (MODGPLI1.Vplis[i].MndCre != 0)
                    {
                        model.Vplis_MndCreRepeat = (VB6Helpers.Trim(VB6Helpers.Str(MODGPLI1.Vplis[i].MndCre)));
                    }

                    //---------------------------------------------------
                    //Monto Equivalente Moneda Desembolso Credito Externo
                    //---------------------------------------------------
                    if (MODGPLI1.Vplis[i].MtoCre != 0)
                    {
                        n = Format.FormatCurrency(MODGPLI1.Vplis[i].MtoCre, "#,###,###,###,##0.00");
                        model.Vplis_MtoCre = (MODGPYF1.PoneChar(n, " ", "H", 20));
                    }
                    initObject.PlanillasInvisibles.Add(model);

                    string paramStr = "Impresion/Planillas/ImprimirPlanillaInvisibleExportacion?numeroPresentacion={0}&fechaPresentacion={1}";
                    string urlStr = string.Format(paramStr, VB6Helpers.Format(model.Vplis_NumPli, "0000000"), DateTime.Parse(model.Vplis_FecPli).ToString("yyy-MM-dd"));
                     
                    initObject.DocumentosAImprimir.Add(new DataImpresion()
                    {
                        URL = urlStr,
                        //URL = "FundTransfer/ImprimirPlanillaInvisible/" + initObject.PlanillasInvisibles.Count
                        nroPresentacion = VB6Helpers.Format(model.Vplis_NumPli, "0000000"),
                        fechaOp = DateTime.Parse(model.Vplis_FecPli),
                        tipoDoc = 5,
                        fileName = initObject.MODGCVD.VgCvd.OpeSin
                    });
                    
                    //--------------------------------------------------------------------------------------------------
                    //Accenture-Código Nuevo-Inicio
                    //Fecha Modificación 22022012
                    //Responsable: Angel Donoso Gonzalez.
                    //Versión:
                    //Descripción : Fin If de nueva condición agregada
                    //--------------------------------------------------------------------------------------------------
                }

                //--------------------------------------------------------------------------------------------------
                // Accenture - Código Nuevo - Termino
                //--------------------------------------------------------------------------------------------------
            }
        }
   
        //Retorno    :  = "", si el número está bueno.-
        //             <> "", si el número está malo.-
        public static short Fn_ValidaAladiold(InitializationObject initObj, string NumAla)
        {
            string s = "";
            short i = 0;
            short mul = 0;
            short Sum = 0;
            short X = 0;
            string dv = "";
            if (VB6Helpers.Len(NumAla) != 18)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "El Número Aladi debe tener 18 dígitos.",
                    Title = "Validación Número Aladi"
                });                
                return 0;
            }

            //Correlativo.-
            if (VB6Helpers.Val(VB6Helpers.Mid(NumAla, 10, 6)) <= 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "El Correlativo del Número Aladi debe ser mayor que cero.",
                    Title = "Validación Número Aladi"
                });                
                return 0;
            }

            //Calcula Dígito Verificador.-
            s = VB6Helpers.Left(NumAla, 15);
            for (i = 1; i <= (short)VB6Helpers.Len(s); i++)
            {
                if (i % 2 == 0)
                {
                    mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(s, i, 1)) * 2);
                    Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 1, 1)) + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                }
                else
                {
                    mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(s, i, 1)) * 1);
                    Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                }

            }

            if (Sum == 0)
            {
                X = 0;
            }
            else
            {
                X = (short)((VB6Helpers.Int((Sum - 1) / 10) * 10) + 10);
            }

            X = (short)(X - Sum);
            dv = VB6Helpers.Right(VB6Helpers.Format(VB6Helpers.CStr(X), "00"), 1) + "00";

            //Dígito Verificador.-
            if (dv != VB6Helpers.Right(NumAla, 3))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "El Dígito Verificador del Número Aladi no está correcto.",
                    Title = "Validación Número Aladi"
                });                
                return 0;
            }

            return (short)(true ? -1 : 0);
        }


        //****************************************************************************
        //   1.  Recorre los Corresponsales donde se pueda pagar dado un país y
        //       moneda. Sólo se seleccionan los indicados en los parámetros como
        //       Aladi/NoAladi.
        //****************************************************************************
        public static IList<T_Cor> Filtra_Cor(InitializationObject initObj, short codPais, short codMoneda, bool aladi)
        {
            IList<T_Nom> bancos = initObj.MODGTAB0.VNom.Where(b => b.Nom_Mda == codMoneda && 
                ((aladi && b.Nom_Ala != 0) || (!aladi && b.Nom_Ala == 0))).ToList();
            if (codPais > 0)
            {
                bancos = bancos.Where(b => b.Nom_Pai == codPais).ToList();  
            }

            List<T_Cor> corresponsales = new List<T_Cor>();
            foreach (T_Nom banco in bancos)
            {
                corresponsales.AddRange(
                    initObj.MODGTAB0.VCor.Where(x => x.Cor_Swf == banco.Nom_Swf).ToList()
                    );
            }

            return corresponsales;
        }

        public static IList<T_Bic> SyGet_VBic(string swiftBanco, string secuencia, UnitOfWorkCext01 uow, InitializationObject initObject)
        {
            List<sce_bic> result = new List<sce_bic>();
            if (String.IsNullOrEmpty(secuencia))
            {
                secuencia = "XXX";
            }
            result = uow.BancoRepository.sce_bic_s02_MS(swiftBanco, secuencia).ToList();
            #region Llena la clase Mdl_Funciones.VBic
            foreach (var item in result) { 
                initObject.Mdl_Funciones.VBic = new T_Bic();
                initObject.Mdl_Funciones.VBic.BicAla = item.bic_ala;
                initObject.Mdl_Funciones.VBic.BicCiu = item.bic_ciu;
                initObject.Mdl_Funciones.VBic.BicCod = item.bic_cod;
                initObject.Mdl_Funciones.VBic.BicDes = item.bic_des;
                initObject.Mdl_Funciones.VBic.BicDir = item.bic_dir;
                initObject.Mdl_Funciones.VBic.BicNom = item.bic_nom;
                initObject.Mdl_Funciones.VBic.BicPai = item.bic_pai;
                initObject.Mdl_Funciones.VBic.BicPos = item.bic_pos;
                initObject.Mdl_Funciones.VBic.BicSec = item.bic_sec;
                initObject.Mdl_Funciones.VBic.BicSwf = item.bic_swf;
            }
            #endregion
            #region Retorna la clase T_Bic()
            return result.Select(x => new T_Bic() {
                BicAla = x.bic_ala,
                BicCiu = x.bic_ciu,
                BicCod = x.bic_cod,
                BicDes = x.bic_des,
                BicDir = x.bic_dir,
                BicNom = x.bic_nom,
                BicPai = x.bic_pai,
                BicPos = x.bic_pos,
                BicSec = x.bic_sec,
                BicSwf = x.bic_swf,
            }).ToList();
            #endregion
        }

        public static IList<T_Bic> GetBancoPaymentplus(string swiftBanco, string secuencia, UnitOfWorkSwift uowSwift, InitializationObject initObject)
        {
            List<PaymentPlus> result = new List<PaymentPlus>();
            if (String.IsNullOrEmpty(secuencia))
            {
                secuencia = "XXX";
            }
            result = uowSwift.PaymentPlusRepository.GetBancoPorSwift(swiftBanco, secuencia).ToList();
            #region Llena la clase Mdl_Funciones.VBic
            foreach (var item in result)
            {
                initObject.Mdl_Funciones.VBic = new T_Bic();
                initObject.Mdl_Funciones.VBic.BicAla = false;    //TODO
                initObject.Mdl_Funciones.VBic.BicCiu = item.trans_dsc_city;
                initObject.Mdl_Funciones.VBic.BicCod = item.trans_dsc_iso_country_code;
                initObject.Mdl_Funciones.VBic.BicDes = item.trans_dsc_zip_code + " " + item.trans_dsc_city;
                initObject.Mdl_Funciones.VBic.BicDir = string.Join("", item.trans_dsc_street_address_1, 
                    item.trans_dsc_street_address_2, item.trans_dsc_street_address_3, item.trans_dsc_street_address_4);
                initObject.Mdl_Funciones.VBic.BicNom = item.trans_dsc_institution_name;
                initObject.Mdl_Funciones.VBic.BicPai = item.trans_dsc_country_name;
                initObject.Mdl_Funciones.VBic.BicPos = item.trans_dsc_zip_code;
                initObject.Mdl_Funciones.VBic.BicSec = item.trans_dsc_branch_bic;
                initObject.Mdl_Funciones.VBic.BicSwf = item.trans_dsc_bic8;
            }
            #endregion
            #region Retorna la clase T_Bic()
            return result.Select(x => new T_Bic()
            {
                BicAla = false,   //TODO
                BicCiu = x.trans_dsc_city,
                BicCod = x.trans_dsc_iso_country_code,
                BicDes = x.trans_dsc_zip_code + " " + x.trans_dsc_city,
                BicDir = string.Join(" ", x.trans_dsc_street_address_1,
                    x.trans_dsc_street_address_2, x.trans_dsc_street_address_3, x.trans_dsc_street_address_4),
                BicNom = x.trans_dsc_institution_name,
                BicPai = x.trans_dsc_country_name,
                BicPos = x.trans_dsc_zip_code,
                BicSec = x.trans_dsc_branch_bic,
                BicSwf = x.trans_dsc_bic8,
            }).ToList();
            #endregion
        }

        //****************************************************************************
        //   1.  Recorre los Corresponsales donde se pueda pagar dado un país y
        //       moneda. Sólo se seleccionan los indicados en los parámetros como
        //       Activo/NoActivo y Aladi/NoAladi.
        //       P_Activo =>    0: Activo.
        //                      1: No Activo.
        //                      2: Activos y No Activos.
        //****************************************************************************
        // UPGRADE_INFO (#0561): The 'P_Moneda' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'P_Todos' symbol was defined without an explicit "As" clause.
        public static short Filtra_Cor(InitializationObject initObject, short P_Pais, dynamic P_Moneda, ref dynamic P_Todos, short P_Aladi, UI_ListBox P_Lista)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'Selecciona' variable wasn't declared explicitly.
            short Selecciona = 0;
            // UPGRADE_INFO (#05B1): The 'j' variable wasn't declared explicitly.
            short j = 0;
            // UPGRADE_INFO (#05B1): The 'k' variable wasn't declared explicitly.
            short k = -1;

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'P_Todos'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble(P_Todos) != 0)
            {
                // UPGRADE_WARNING (#0364): Unable to assign default member of symbol 'P_Todos'. Consider using the SetDefaultMember6 helper method.
                P_Todos = true;
            }

            //VB6Helpers.Invoke(VB6Helpers.CObj(P_Lista), "Clear");
            P_Lista.Clear();
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VNom); i++)
            {
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'P_Moneda'. Consider using the GetDefaultMember6 helper method.
                if ((P_Pais == 0 || MODGTAB0.VNom[i].Nom_Pai == P_Pais) && MODGTAB0.VNom[i].Nom_Mda == Format.StringToDouble(P_Moneda))
                {
                    Selecciona = (short)(true ? -1 : 0);
                    if ((P_Aladi & ~MODGTAB0.VNom[i].Nom_Ala) != 0)
                    {
                        Selecciona = (short)(false ? -1 : 0);
                    }

                    if ((~P_Aladi & MODGTAB0.VNom[i].Nom_Ala) != 0)
                    {
                        Selecciona = (short)(false ? -1 : 0);
                    }

                    if (Selecciona != 0)
                    {
                        j = Find_Cor(initObject,MODGTAB0.VNom[i].Nom_Swf);
                        if (j != -1)
                        {
                            var item = new UI_ListBoxItem();
                            item.Value = MODGTAB0.VNom[i].Nom_Swf + "\xA0\xA0\xA0\xA0" + MODGTAB0.VCor[j].Cor_Nom;
                            item.Data = i;
                            P_Lista.Items.Add(item);
                            //VB6Helpers.Invoke(VB6Helpers.CObj(P_Lista), "AddItem", MODGTAB0.VNom[i].Nom_Swf + VB6Helpers.Chr(9) + MODGTAB0.VCor[j].Cor_Nom);
                            //VB6Helpers.Set(VB6Helpers.CObj(P_Lista), "ItemData", VB6Helpers.Invoke(VB6Helpers.CObj(P_Lista), "NewIndex"), i);
                            if (MODGTAB0.VNom[i].Nom_Swf == "BCHIUS33XXX")
                            {
                                k = i;
                            }

                        }

                    }

                }

            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'P_Lista'. Consider using the GetDefaultMember6 helper method.
            if (P_Lista.Items.Count != 0 && P_Pais == 225)
            {
                return k;
            }
            else
            {
                return 0;
            }

        }

        //****************************************************************************
        //   1.  Busca en Arreglo COR (direcciones Corresponsales) un Banco
        //       dado su Swift.
        //****************************************************************************
        public static short Find_Cor(InitializationObject initObject, string Codigo)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            //Inicializa como no encontrado.-
            _retValue = -1;
            for (i = 1; i <= (short)VB6Helpers.UBound(MODGTAB0.VCor); i++)
            {
                if (MODGTAB0.VCor[i].Cor_Swf == Codigo)
                {
                    _retValue = i;
                    break;
                }

            }

            return _retValue;
        }


        internal static string Fn_Numero_Aladi(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            double q = 0;
            string Agno = "";
            string Correlativo = "";
            string Numero = "";
            short i = 0;
            short mul = 0;
            short Sum = 0;
            short X = 0;

            //Lectura de Correlativo.-
            q = MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, T_MODGRNG.Rng_OpeAla);
            if (q == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No se pudo obtener el Número para la Orden de Pago Aladi. Reporte este problema inmediatamente y cancele esta operación.",
                    Title = "Generación Número Aladi"
                });

                return String.Empty;
            }

            //Parametrizar Plaza y Producto.-
            Agno = DateTime.Now.Year.ToString();
            Correlativo = VB6Helpers.Format(VB6Helpers.CStr(q), "000000");

            //Oficina Banco Central.-
            decimal? sucBCH = uow.SceRepository.sce_obc_s01_MS(initObj.MODGSCE.VGen.SucBCH);
            if (!sucBCH.HasValue || sucBCH.Value == 0)
            {
                return String.Empty;
            }

            //Generación del Número.-
            Numero = sucBCH.Value.ToString("0000") + "4" + Agno + Correlativo;
            for (i = 1; i <= (short)VB6Helpers.Len(Numero); i++)
            {
                if (i % 2 == 0)
                {
                    mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(Numero, i, 1)) * 2);
                    Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 1, 1)) + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                }
                else
                {
                    mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(Numero, i, 1)) * 1);
                    Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                }
            }

            if (Sum == 0)
            {
                X = 0;
            }
            else
            {
                X = (short)((VB6Helpers.Int((Sum - 1) / 10) * 10) + 10);
            }

            X = (short)(X - Sum);
            return Numero + VB6Helpers.Right(VB6Helpers.Format(VB6Helpers.CStr(X), "00"), 1) + "00";
        }

        // Procedimiento que carga los valores de los Cheques.-
        // Requiere las Tablas :
        //  Sgt_Pai
        //  Sce_Nom
        //  Sce_Cor
        public static bool InterfazSwf(InitializationObject io, UnitOfWorkCext01 uow, string numOpe)
        {
            string d = "";
            short i = 0;
            int m = 0;
            io.Mdi_Principal.MESSAGES.Clear();

            // Carga las Tablas.-
            MODGTAB0.SyGetn_Pai(io.MODGTAB0, uow); // Países.-
            MODGTAB0.SyGetn_Nom(io, uow);     // Nómina.-
            MODGTAB0.SyGetn_Cor(io.MODGTAB0, uow);     // Corresponsales.-

            // Carga otras Tablas.-
           //cargo los feriados si aun no estan cargados
            if (io.MODGTAB0.VFer == null || io.MODGTAB0.VFer.Length == 0)
            {
                MODGTAB0.SyGetn_VFer(io.MODGTAB0, uow);
            }

            // Total Swift's.-
            IList<T_xVia> swiftsEnLasVias =  MODXVIA.GetSwifts(io.MODXVIA);
            if (swiftsEnLasVias.Any())
            {
                // Se cargan los Documentos según Vías.-
                if (io.MODGSWF.VSwf == null || io.MODGSWF.VSwf.Length == 0)
                {
                    io.MODGSWF.VSwf = new T_Swf[swiftsEnLasVias.Count];
                    io.MODGSWF.VMT103 = new T_mt103[swiftsEnLasVias.Count];
                }

                short j = 0;
                // Si los Cheques NO están cargados => Se cargan según Vías.-
                foreach(T_xVia viaSwift in swiftsEnLasVias)
                {
                    if (viaSwift.IndSwf == 0) //si no esta generado
                    {
                        viaSwift.IndSwf = (short)(j+1);
                        T_Swf swift = new T_Swf();
                        T_mt103 monto = new T_mt103();

                        if (io.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                        {
                            if (viaSwift.NumCta == T_MODGCON0.IdCta_OPOP)
                            {
                                swift.mtoswf = viaSwift.MtoTot;
                                swift.CodMon = viaSwift.CodMon;
                                swift.EsAladi = false;
                                m = MODGTAB0.Get_VMnd(io.MODGTAB0, uow, viaSwift.CodMon);
                                swift.SwfMon = io.MODGTAB0.VMnd[m].Mnd_MndSwf;
                            }
                            if (viaSwift.NumCta == T_MODGCON0.IdCta_OPC)
                            {
                                swift.mtoswf = viaSwift.MtoTot;
                                swift.CodMon = viaSwift.CodMon;
                                swift.EsAladi = true;
                                m = MODGTAB0.Get_VMnd(io.MODGTAB0, uow, viaSwift.CodMon);
                                swift.SwfMon = io.MODGTAB0.VMnd[m].Mnd_MndSwf;
                            }
                        }
                        else
                        {
                            viaSwift.IndSwf = (short)(j + 1); ;
                            swift.mtoswf = Format.StringToDouble(io.Mdl_Funciones_Varias.LC_MONTO);     //  VxVia(i%).MtoTot
                            swift.CodMon = viaSwift.CodMon;
                            swift.EsAladi = true;
                            m = MODGTAB0.Get_VMnd(io.MODGTAB0, uow, viaSwift.CodMon);
                            swift.SwfMon = io.MODGTAB0.VMnd[m].Mnd_MndSwf;


                            swift.DatSwf.ctacte = io.Mdl_Funciones_Varias.LC_BEN_INST1;
                            swift.BenSwf.NomBen = io.Mdl_Funciones_Varias.LC_ULT_BEN1;
                            swift.BenSwf.DirBen1 = io.Mdl_Funciones_Varias.LC_ULT_BEN2;
                            swift.BenSwf.DirBen2 = io.Mdl_Funciones_Varias.LC_ULT_BEN3;
                            swift.BenSwf.PaiBen_t = io.Mdl_Funciones_Varias.LC_ULT_BEN4;
                        }

                        monto.MtoOri = swift.mtoswf;
                        monto.MndOri = swift.CodMon;

                        io.MODGSWF.VSwf[j] = swift;
                        io.MODGSWF.VMT103[j] = monto;
                        j++;
                    }
                }
                                
                // Carga los posibles Beneficiarios.-
                List<PartyKey> parties = io.Module1.PartysOpe.Where(p => !String.IsNullOrEmpty(p.NombreUsado)).ToList();
                io.MODGSWF.VBenSwf = new T_BenSwf[parties.Count + 1]; //por el beneficiario "Beneficiario" que se agrega si o si.
                PartyKey party = null;
                for (i = 0; i < parties.Count; i++)
                {
                    T_BenSwf benSwf = new T_BenSwf();
                    io.MODGSWF.VBenSwf[i] = benSwf;
                    party = parties[i];

                    if (party.TipoParty == T_Module1.GPrt_TipoBanco)
                    {
                        benSwf.EsBanco = true;
                    }
                    else
                    {
                        benSwf.EsBanco = false;
                    }

                    if (io.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                    {
                        if (i == 0)
                        {
                            benSwf.NomBen = io.Mdl_Funciones_Varias.LC_ULT_BEN1;
                            benSwf.DirBen1 = io.Mdl_Funciones_Varias.LC_ULT_BEN2;
                            benSwf.DirBen2 = io.Mdl_Funciones_Varias.LC_ULT_BEN3;
                            benSwf.PaiBen_t = io.Mdl_Funciones_Varias.LC_ULT_BEN4;
                        }
                    }
                    else
                    {
                        benSwf.SwfBen = party.Swift;
                        benSwf.IndBen = i;
                        benSwf.FunBen = io.MODGCVD.Beneficiario[i];
                        benSwf.NomBen = party.NombreUsado;
                        benSwf.DirBen1 = party.DireccionUsado;
                        benSwf.DirBen2 = party.CiudadUsado;
                        if (!String.IsNullOrEmpty(party.PostalUsado) && !String.IsNullOrEmpty(party.PostalUsado.Trim()))
                        {
                            benSwf.DirBen2 += ", " + party.PostalUsado.Trim();
                        }
                        benSwf.PaiBen = party.CodPais;
                        benSwf.PaiBen_t = party.PaisUsado;
                    }
                }

                io.MODGSWF.VBenSwf[io.MODGSWF.VBenSwf.Length - 1] = new T_BenSwf() { IndBen = (short)(io.MODGSWF.VBenSwf.Length - 1), FunBen = "Beneficiario" };

                // Carga el Cliente.-
                party = io.Module1.PartysOpe[io.Mdl_Funciones_Varias.IExp];

                T_CliSwf cliente = new T_CliSwf();
                io.MODGSWF.VCliSwf = cliente;

                cliente.NomCli = party.NombreUsado;
                cliente.DirCli1 = party.DireccionUsado;
                cliente.DirCli2 = party.CiudadUsado;
                cliente.PaiCli = party.PaisUsado;
                cliente.CiuCli = party.CiudadUsado.ToUpper();
                cliente.rutcli = party.rut;
                if (!(io.MODXORI == null))
                {
                    var ope = io.MODXORI.gs_ctacte_party;
                    if (ope != null)
                    {
                        cliente.CtaCli = ope;
                    }
                    else
                    {
                        cliente.CtaCli = "";
                    }
                }
                else
                {
                    cliente.CtaCli = MODXORI.Get_CtaCte(uow, cliente.rutcli.TrimStart('0').PadRight(12, '|'));
                }
                cliente.CtaCli = cliente.CtaCli.TrimStart('0');

                io.MODGSWF.VGSwf.NumOpe = numOpe;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
