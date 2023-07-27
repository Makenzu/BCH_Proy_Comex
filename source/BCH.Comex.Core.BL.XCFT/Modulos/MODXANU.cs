using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
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

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODXANU
    {
        public static T_MODXANU GetMODXANU() {
            return new T_MODXANU();
        }

        //****************************************************************************
        //   1.  Graba varias Planillas Visibles Anuladas.
        //   2.  Retorno    <> 0 : Grabación Exitosa.
        //                  =  0 : Error o Grabación no Exitosa.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyPutn_xAnu(InitializationObject initObject,UnitOfWorkCext01 unit, short Estado)
        {
            using (var tracer = new Tracer("Graba varias Planillas Visibles Anuladas - SyPutn_xAnu"))
            {
                T_MODXANU MODXANU = initObject.MODXANU;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                
                short _retValue = 0;
                string Que = "";
                short n = 0;
                short i = 0;
                string R = "";
                short ConError = 0;

                n = (short)VB6Helpers.UBound(MODXANU.VxAnus);


                /**************************
                Metodo: MODXANU.SyPutn_xAnu
                Fecha de Modificacion: 15-09-2015
                Modificado por: Wilder Romero
                Caso Detectado: A este metodo se le ha cambiado el indice de inicio del ciclo for por presentarse caso en el modulo Reverso de Operacion el cual no requiere entrar en este flujo por venir MODXANU.VxAnus con un registro de indice cero (0), al verificar la aplicacion original efectivamente maneja el proceso es de la misma forma, solo entra en caso de venir mas de 1 registro.
                Nota: Por no haber detectado en que otro proceso se llena este objeto y para continuar con le migracion se ha colocado un throw new Exception para detectar si existe algun proceso que en algun momento si entra en el for por tener mas de 1 registro y este debe ser evaluado para hacer el cambio correspondiente.
                **************************/
                for (i = 1; i <= (short)n; i++)
                {
                    throw new Exception("Metodo a revisar, el flujo ha entrado en esta seccion y requiere atención. Metodo: MODXANU.SyPutn_xAnu");

                    try
                    {
                        List<string> parameters = new List<string>();

                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].NumPre));
                        parameters.Add(MODGSYB.dbdatesy(MODXANU.VxAnus[i].fecpre));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].cencos));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].codusr));

                        parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(MODXANU.VxAnus[i].Fecing, "yyyy-mm-dd")));
                        //-------------------------------------------
                        if (Estado == T_MODXPLN1.ExPlv_Anulada)
                        {
                            parameters.Add(MODGSYB.dbnumesy(T_MODXPLN1.ExPlv_Anulada));
                        }
                        else
                        {
                            parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].Estado));
                        }

                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].codcct));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].codpro));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].codesp));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].codofi));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].codope));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].TipAnu));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].PlzBcc));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].RutExp));
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].PrtExp));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].IndNom));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].IndDir));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].EntAut));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].NumpreO));
                        parameters.Add(MODGSYB.dbdatesy(MODXANU.VxAnus[i].FecpreO));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].TipPln));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].CodPbc));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].numdec));
                        parameters.Add(MODGSYB.dbdatesy(MODXANU.VxAnus[i].FecDec));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].CodAdn));
                        parameters.Add(MODGSYB.dbdatesy(MODXANU.VxAnus[i].FecVen));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].CodMnd));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].MtoDol));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].Mtopar));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].MtoAnu));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].MtoParA));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].MtoDolA));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].MtoDolPo));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[i].ObsPln));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[i].PlnEst));
                        //-------------------------------------------
                        parameters.Add(MODGSYB.dbcharSy(MODXANU.VxAnus[n].TipAut));
                        parameters.Add(MODGSYB.dbnumesy(MODXANU.VxAnus[n].NroAut));
                        parameters.Add(MODGSYB.dbdatesy(MODXANU.VxAnus[n].FecAut));
                        parameters.Add(MODGSYB.dbTCamSy(MODXANU.VxAnus[n].TipCam));

                        int res = unit.SceRepository.EjecutarSP<int>("sce_xanu_i01", parameters.ToArray()).First();

                        if (res == 9)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Se ha producido un error de Comunicación al tratar de grabar los registros anulados.",
                                Type = TipoMensaje.Error
                            });
                            ConError = (short)(true ? -1 : 0);
                        }
                        //Se ejecuta el Procedimiento Almacenado.
                    }
                    catch (Exception e)
                    {
                        ConError = -1;
                    }
                }

                if (~ConError != 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                }
                return _retValue;
            }
        }

        //****************************************************************************
        //   1.  Imprime las n copias de todas las planillas Visible-Export.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_Imprime_nxAnu(InitializationObject initObject,UnitOfWorkCext01 unit, short Copias)
        {
            T_MODXANU MODXANU = initObject.MODXANU;

            short n = 0;
            short i = 0;
            short j = 0;
            n = (short)VB6Helpers.UBound(MODXANU.VxAnus);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            VB6Helpers.DoEvents();
            for (i = 0; i <= (short)n; i++)
            {
                for (j = 1; j <= (short)Copias; j++)
                {
                    Pr_Imprime_xPlvAnu(initObject,unit, i);
                }

            }

        }


        //****************************************************************************
        //   1.  Imprime la Planilla Visible Anulada.
        //****************************************************************************
        public static void Pr_Imprime_xPlvAnu(InitializationObject initObject,UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXANU MODXANU = initObject.MODXANU;
            T_MODGTAB1 MODGTAB1 = initObject.MODGTAB1;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;


            short va = 1;
            short i = Indice;
            short num_cop = 0;
            short z = 0;
            short copia = 0;
            short a = 0;
            short Impresora;
            float coordy = 0;
            string n = "";
            string R = "";
            short m = 0;
            string Texto = "";
            dynamic lin = null;
            string palabra = "";
            short co = 0;
            string letra = "";

            PlanillaVisibleAnulada model = new PlanillaVisibleAnulada();

            num_cop = (short)(num_cop + 1);
            copia = (short)(copia + 1);

            //Tipo de Anulación....OK¡¡¡
            if (MODXANU.VxAnus[i].TipAnu != 0)
            {
                model.VxAnus_TipAnu = (VB6Helpers.Format(VB6Helpers.CStr(MODXANU.VxAnus[i].TipAnu), "000"));
            }

            //Número Presentación....OK¡¡¡
            if(!string.IsNullOrEmpty(MODXANU.VxAnus[i].NumPre))
            {
                
                  //+ .25
                n = VB6Helpers.Format(MODXANU.VxAnus[i].NumPre, "0000000");
                model.VxAnus_NumPre = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Fecha Presentación....OK¡¡¡
            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].fecpre))
            {
                  //+ .15
                model.VxAnus_fecpre = (DateTime.Parse(MODXANU.VxAnus[i].fecpre).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            }

            //Plaza Banco Central que Contabiliza
            
            //Código Plaza Banco Central que Contabiliza
            
              //+ .15
            model.COD_PLAZA_25 = (VB6Helpers.Format("25", "00"));
            //Nombre.
            if (VB6Helpers.Mid(MODXANU.VxAnus[i].PrtExp, 1, 1) == "0")
            {
                MODXANU.VxAnus[i].PrtExp = VB6Helpers.Mid(MODXANU.VxAnus[i].PrtExp, 2) + "~";
            }

            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].PrtExp))
            {
                model.DatPrt = (Mdl_Funciones_Varias.GetDatPrt(initObject,unit, MODXANU.VxAnus[i].PrtExp, MODXANU.VxAnus[i].IndNom, MODXANU.VxAnus[i].IndDir, "N"));
                //Dirección.
                
                  //.15
                model.DatPrt2 = (Mdl_Funciones_Varias.GetDatPrt(initObject, unit, MODXANU.VxAnus[i].PrtExp, MODXANU.VxAnus[i].IndNom, MODXANU.VxAnus[i].IndDir, "D"));
            }

            //Rut.
            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].RutExp))
            {
                R = MODXPLN1.ConvRut(VB6Helpers.Trim(MODXANU.VxAnus[i].RutExp));
                model.VxAnus_RutExp = (VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1));
            }

            //**********************************************************************
            //Entidad Autorizada.
            
            
            m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_Bco(initObject,unit, MODXANU.VxAnus[i].EntAut);
            if (m >= 0)
            {
                model.VBco_NomBco = (VB6Helpers.Left(VB6Helpers.Trim(MODGTAB0.VBco[m].NomBco), 25));
            }

            //Código Entidad Autorizada.
            model.VxAnus_EntAut = (VB6Helpers.Format(VB6Helpers.CStr(MODXANU.VxAnus[i].EntAut), "000"));

            //Datos de Aduana.
            if (MODXANU.VxAnus[i].CodAdn != 0)
            {
                //Aduana.
                m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VAdn(MODGTAB1,unit,MODXANU.VxAnus[i].CodAdn);
                if (m >= 0)
                {
                    model.VAdn_NomAdn = (VB6Helpers.Trim(MODGTAB1.VAdn[m].NomAdn));
                }

                //Código Aduana.
                model.VxAnus_CodAdn = (VB6Helpers.Format(VB6Helpers.CStr(MODXANU.VxAnus[i].CodAdn), "00"));
            }

            //Moneda.
            if (MODXANU.VxAnus[i].CodMnd != 0)
            {
                
                
                m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit, MODXANU.VxAnus[i].CodMnd);
                if (m != 0)
                {
                    model.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGTAB0.VMnd[m].Mnd_MndNom));
                }

                //Código Moneda.
                model.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.VMnd[m].Mnd_MndCbc), "000"));
            }

            //Número Presentación Original.
            if(!string.IsNullOrEmpty(MODXANU.VxAnus[i].NumpreO))
            {
                
                
                n = VB6Helpers.Format(MODXANU.VxAnus[i].NumpreO, "0000000");
                model.VxAnus_NumpreO = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Número Aceptación.
            if(!string.IsNullOrEmpty(MODXANU.VxAnus[i].numdec))
            {
                
                
                n = VB6Helpers.Format(MODXANU.VxAnus[i].numdec, "0000000");
                model.VxAnus_numdec = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Monto Anulado.
            if (MODXANU.VxAnus[i].MtoAnu != 0)
            {
                n = Format.FormatCurrency(MODXANU.VxAnus[i].MtoAnu, "#,###,###,###,##0.00");
                model.VxAnus_MtoAnu = (MODGPYF1.PoneChar(n, " ", "H", 20));
            }

            //Fecha Presentación Original.
            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].FecpreO))
            {
                model.VxAnus_FecpreO = (DateTime.Parse(MODXANU.VxAnus[i].FecpreO).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            }

            //Fecha Aceptación.
            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].FecDec))
            {
                model.VxAnus_FecDec = (DateTime.Parse(MODXANU.VxAnus[i].FecDec).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            }

            //Paridad Anulada.
            if (MODXANU.VxAnus[i].MtoParA != 0)
            {
                n = Format.FormatCurrency(MODXANU.VxAnus[i].MtoParA, "#,###,##0.0000");
                model.VxAnus_MtoParA = (MODGPYF1.PoneChar(n, " ", "H", 20));
            }

            //Tipo de Operación.
            if (MODXANU.VxAnus[i].TipPln != 0)
            {
                model.VxAnus_TipPln = (VB6Helpers.Trim(VB6Helpers.Mid(MODXPLN1.GetNomPLn(MODXANU.VxAnus[i].TipPln), 1, 17)));
                //Código Tipo de Operación.
                model.VxAnus_TipPln = (VB6Helpers.Format(VB6Helpers.CStr(MODXANU.VxAnus[i].TipPln), "000"));
            }

            //Fecha Vencimiento Retorno.
            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].FecVen))
            {
                model.VxAnus_FecVen = (DateTime.Parse(MODXANU.VxAnus[i].FecVen).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            }

            //Monto en Dolar Anulado.
            if (MODXANU.VxAnus[i].MtoDolA != 0)
            {
                n = Format.FormatCurrency(MODXANU.VxAnus[i].MtoDolA, "#,###,###,###,##0.00");
                model.VxAnus_MtoDolA = (MODGPYF1.PoneChar(VB6Helpers.Format(n, "0.00"), " ", "H", 20));
            }

            //Plaza Banco Central.
            if (MODXANU.VxAnus[i].PlzBcc != 0)
            {
                //Descripción Plaza Banco Central.
                m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject,unit, MODXANU.VxAnus[i].PlzBcc);
                if (m >= 0)
                {
                    model.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                }

                //Código Plaza Banco Central.
                
                
                model.VxAnus_PlzBcc = (VB6Helpers.Format(VB6Helpers.CStr(MODXANU.VxAnus[i].PlzBcc) + " 00"));
            }

            //Monto Paridad Original.
            if (MODXANU.VxAnus[i].MtoDolPo != 0)
            {
                n = Format.FormatCurrency(MODXANU.VxAnus[i].MtoDolPo, "#,###,###,###,##0.00");
                model.VxAnus_MtoDolPo = (MODGPYF1.PoneChar(n, " ", "H", 20));
            }

            //Monto Dolar.
            if (MODXANU.VxAnus[i].MtoDol != 0)
            {
                n = Format.FormatCurrency(MODXANU.VxAnus[i].MtoDol, "#,###,###,###,##0.00");
                model.VxAnus_MtoDol = (MODGPYF1.PoneChar(n, " ", "H", 20));
            }

            //Monto Paridad.
            if (MODXANU.VxAnus[i].Mtopar != 0)
            {
                n = Format.FormatCurrency(MODXANU.VxAnus[i].Mtopar, "#,###,##0.0000");
                model.VxAnus_Mtopar = (MODGPYF1.PoneChar(n, " ", "H", 20));
            }

            //-------------------------
            //Observaciones.
            //-------------------------
            if(!string.IsNullOrWhiteSpace(MODXANU.VxAnus[i].ObsPln))
            {
                Texto = MODGPYF0.Componer(VB6Helpers.Trim(MODXANU.VxAnus[i].ObsPln), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                Texto = Texto + "&";
                palabra = "";
                for (co = 1; co <= (short)VB6Helpers.Len(Texto); co++)
                {
                    letra = VB6Helpers.Mid(Texto, co, 1);
                    if (letra == " " || letra == "&")
                    {
                        if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                        {
                            model.Palabras.Add(palabra);
                            lin = Format.StringToDouble(lin) + 0.3;
                            palabra = "";
                        }
                        else
                        {
                            // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'palabra' variable as a StringBuilder6 object.
                            palabra += " ";
                        }

                    }
                    else
                    {
                        palabra += letra;
                    }

                }

            }
            initObject.DocumentosAImprimir.Add(new DataImpresion()
            {
                URL="FundTransfer/PlanillaVisibleAnulada/"+initObject.PlanillasVisiblesAnuladas.Count
            });
            initObject.PlanillasVisiblesAnuladas.Add(model);
        }

        //****************************************************************************
        //   1.  Lee una Planilla Visible de Exportación para su posterior Anulación.
        //   2.  Retorno    <> 0    : Retorna los datos de la Planilla.
        //                  =  0    : No existen datos de esa Planilla.
        //****************************************************************************
        public static short SyGet_xPlAnu(string NumPre, DateTime fecpre, double MtoAnu, string Observaciones, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            #region Inicializacion Variables
            short _retValue = 0;
            short largo = 0;
            short n = 0;
            short i = 0;
            int p = 0;
            short pp = 0;
            short MN = 0;
            #endregion

            try
            {
                largo = (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus);
                n = 1;

                VB6Helpers.RedimPreserve(ref initObj.MODXANU.VxAnus, 0, largo + n);
                i = (short)(largo + n);

                //Llamada StoredProcedure.
                var Result = unit.SceRepository.sce_xplv_s10_MS(NumPre, fecpre);
                if (Result != null)
                {
                    //-----------------------------------------
                    //Se genera una nueva planilla.-
                    //-----------------------------------------
                    //se valida que queden numeros para las PVX
                    p = (int)MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, unit, "PVX");
                    //Si retorna -1 no se logro conseguir numero
                    if (p == -1)
                    {
                        return _retValue;
                    }

                    //Algoritmo para Dígito Verificador.-
                    initObj.MODXANU.VxAnus[i].NumPre = MODXPLN1.Fn_DigVer_xPlv(initObj.MODGSCE.VGen.CodPbc, initObj.MODGSCE.VGen.CodBch, p, VB6Helpers.Year(DateTime.Now));
                    initObj.MODXANU.VxAnus[i].fecpre = DateTime.Now.ToString("dd/mm/yyyy");
                    initObj.MODXANU.VxAnus[i].NumpreO = Result.numpre;
                    initObj.MODXANU.VxAnus[i].FecpreO = Result.fecpre.ToString("dd/MM/yyyy");
                    initObj.MODXANU.VxAnus[i].TipPln = (short)Result.tippln;

                    initObj.MODXANU.VxAnus[i].Fecing = Result.fecing.ToString("dd/MM/yyyy");
                    initObj.MODXANU.VxAnus[i].Fecing = DateTime.Now.ToString("dd/mm/yyyy");

                    initObj.MODXANU.VxAnus[i].cencos = initObj.MODGUSR.UsrEsp.CentroCosto;
                    initObj.MODXANU.VxAnus[i].codusr = initObj.MODGUSR.UsrEsp.Especialista;
                    //pp = VB6Helpers.CShort(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R));
                    //pp = VB6Helpers.CShort(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R));

                    initObj.MODXANU.VxAnus[i].codcct = Result.codcct;
                    initObj.MODXANU.VxAnus[i].codpro = Result.codpro;
                    initObj.MODXANU.VxAnus[i].codesp = Result.codesp;
                    initObj.MODXANU.VxAnus[i].codofi = Result.codofi;
                    initObj.MODXANU.VxAnus[i].codope = Result.codope;

                    initObj.MODXANU.VxAnus[i].Estado = (short)Result.estado;
                    initObj.MODXANU.VxAnus[i].numdec = Result.numdec;
                    initObj.MODXANU.VxAnus[i].FecDec = Result.fecdec.ToString("dd/MM/yyyy");
                    initObj.MODXANU.VxAnus[i].CodAdn = (short)Result.codadn;
                    initObj.MODXANU.VxAnus[i].FecVen = Result.fecven.ToString("dd/MM/yyyy");

                    initObj.MODXANU.VxAnus[i].RutExp = Result.rutexp;
                    initObj.MODXANU.VxAnus[i].PrtExp = Result.prtexp;
                    initObj.MODXANU.VxAnus[i].IndNom = (short)Result.indnom;
                    initObj.MODXANU.VxAnus[i].IndDir = (short)Result.inddir;

                    initObj.MODXANU.VxAnus[i].CodMnd = (short)Result.codmnd;
                    initObj.MODXANU.VxAnus[i].Mtopar = (double)Result.mtopar;
                    initObj.MODXANU.VxAnus[i].MtoDol = (double)Result.mtodol;
                    initObj.MODXANU.VxAnus[i].PlzBcc = (short)Result.plzbcc;
                    initObj.MODXANU.VxAnus[i].PlnEst = Convert.ToInt16(Result.plnest);
                    initObj.MODXANU.VxAnus[i].ObsPln = Observaciones;
                    initObj.MODXANU.VxAnus[i].EntAut = initObj.MODGSCE.VGen.CodBch;
                    initObj.MODXANU.VxAnus[i].CodPbc = initObj.MODXANU.VxAnus[i].PlzBcc;

                    initObj.MODXANU.VxAnus[i].MtoAnu = MtoAnu;
                    MN = initObj.MODXANU.VxAnus[i].CodMnd;
                    if (initObj.MODXANU.VxAnus[i].CodMnd != 11)
                    {
                        initObj.MODXANU.VxAnus[i].MtoParA =
                             MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, unit, MN, DateTime.Now.ToString("dd/mm/yyyy"), "P");
                    }
                    else
                    {
                        initObj.MODXANU.VxAnus[i].MtoParA = 1;
                    }

                    if (initObj.MODXANU.VxAnus[i].MtoParA > 0)
                    {
                        initObj.MODXANU.VxAnus[i].MtoDolA = Format.StringToDouble(Format.FormatCurrency((initObj.MODXANU.VxAnus[i].MtoAnu / initObj.MODXANU.VxAnus[i].MtoParA), "0.00"));
                    }

                    if (initObj.MODXANU.VxAnus[i].Mtopar != 0)
                    {
                        initObj.MODXANU.VxAnus[i].MtoDolPo = Format.StringToDouble(Format.FormatCurrency((initObj.MODXANU.VxAnus[i].MtoAnu / initObj.MODXANU.VxAnus[i].Mtopar), "0.00"));
                    }

                    _retValue = (short)(true ? -1 : 0);
                }
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODGCVD.MsgCVD
                });
                _retValue = 0;
            }
            return _retValue;
        }

        public static short Nuevo_Pope(InitializationObject initObject)
        {
            short Fin = -1;
            
            Fin = (short)VB6Helpers.UBound(initObject.Module1.PopeOpe);
            if (Fin == -1)
            {
                initObject.Module1.PopeOpe = new PartysPope[1];
            }
            else
            {
                if (Fin == 0 && string.IsNullOrEmpty(initObject.Module1.PopeOpe[0].Nombre))
                {
                    initObject.Module1.PopeOpe = new PartysPope[1];
                }
                else
                {
                    VB6Helpers.RedimPreserve(ref initObject.Module1.PopeOpe, 0, Fin + 1);
                }

            }
            return (short)VB6Helpers.UBound(initObject.Module1.PopeOpe);
        }
    }
}
