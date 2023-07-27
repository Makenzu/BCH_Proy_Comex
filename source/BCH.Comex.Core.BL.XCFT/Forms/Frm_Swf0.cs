using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Swf0
    {
        //****************************************************************************
        //   1.  Cargar la Fecha de Pago inicial con la que comenzará una emisión de
        //       Swift en el caso de no estar generado. Dicha fecha no puede ser fin
        //       de semana ni un día feriado. 
        //****************************************************************************
        public static DateTime Pr_Fecha_Inicial(T_MODGTAB0 mod, UnitOfWorkCext01 uow)
        {
            DateTime fechaPago = DateTime.Now.Date.AddDays(2);
            bool diaValido = false;
            while (!diaValido)
            {
                if(fechaPago.DayOfWeek != DayOfWeek.Saturday && fechaPago.DayOfWeek != DayOfWeek.Sunday)
                {
                    diaValido = !MODGTAB0.FechaEsFeriado(mod, uow, fechaPago);  
                }

                if(!diaValido)
                {
                    fechaPago = fechaPago.AddDays(1);
                }
            }

            return fechaPago;
        }

        //remplazo de funcion Pr_Fecha_Ingresada del legacy
        public static bool ValidarFechaPago(T_MODGTAB0 mod, UnitOfWorkCext01 uow, DateTime fechaPago, out string mensajeError)
        {
            bool esValida = true;
            mensajeError = String.Empty;

            if (fechaPago.Date < DateTime.Now.Date)
            {
                esValida = false;
                mensajeError = "Fecha no puede ser menor a la fecha de hoy";
            }
            else if (fechaPago > DateTime.Now.Date.AddDays(20))
            {
                esValida = false;
                mensajeError = "La Fecha de Pago no puede superar en más de 20 días a la fecha actual.";
            }
            else if (fechaPago.DayOfWeek == DayOfWeek.Saturday || fechaPago.DayOfWeek == DayOfWeek.Sunday)
            {
                esValida = false;
                mensajeError = "La Fecha de Pago no puede ser de un fin de semana.";
            }
            else
            {
                if (MODGTAB0.FechaEsFeriado(mod, uow, fechaPago))
                {
                    esValida = false;
                    mensajeError = "La Fecha de Pago no corresponde, porque existe como fecha feriada.";
                }

            }

            return esValida;
        }

        public static bool ValidarCodComp(T_MODGSWF mod, UnitOfWorkCext01 uow, String codComp, out string mensajeError)
        {
            mensajeError = String.Empty;

            if (codComp != null)
            {
                int length = codComp.Length;

                if (length < 6)
                {
                    mensajeError = "Debe ingresar al menos 6 caracteres";
                    return false;
                }
                else
                {
                    string cc = codComp.Substring(0, 2);

                    int auxInt = 0; //no lo uso, es requerido para parsear

                    IList<ParametroComex> validCCs = uow.ParametroComexRepository.GetParametrosComex("CCOMP", "*", "*");
                    ParametroComex validCC = validCCs.FirstOrDefault(x => x.trans_vlr_parametro.Equals(cc));
                    if (validCC != null)
                    {
                        int ccLength = int.Parse(validCC.trans_dsc_parametro);
                        if (length < ccLength)
                        {
                            mensajeError = validCC.trans_nmb_agrupacion_3;
                            return false;
                        }
                        else if (length > ccLength)
                        {
                            mensajeError = validCC.trans_nmb_agrupacion_3;
                            return false;
                        }
                        if (!int.TryParse(codComp.Substring(2, ccLength-2), out auxInt))
                        {
                            mensajeError = validCC.trans_nmb_agrupacion_3;
                            return false;
                        }
                    }
                    else if (!string.IsNullOrEmpty(cc))
                    {
                        string[] arrayCods = validCCs.Select(x => x.trans_vlr_parametro).ToArray();
                        string cods = string.Join(", ", arrayCods);
                        mensajeError = "Debe ingresar un codigo válido: " + cods + ". Si su codigo es válido reporte a sistemas.";
                        return false;
                    }
                }
            }
            
            return true;
        }

        private static IList<string> GetBicDeConfig(bool emi)
        {
            string keyConfig = (emi ?  "FundTransfer.Swift103.BICEMI": "FundTransfer.Swift103.BICREC");
            string valores = ConfigurationManager.AppSettings[keyConfig];

            if(!string.IsNullOrEmpty(valores))
            {
                return valores.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            return null;
        }

        private static bool validarBancoPaymentPlus(UnitOfWorkSwift uow, string swiftBanco, out string mensajeError)
        {
            mensajeError = "";
            if (swiftBanco != null)
            {
                var result = uow.PaymentPlusRepository.GetBancoPorSwift(swiftBanco.Substring(0, 8), swiftBanco.Substring(8, 3));

                if (result.Count >= 1)
                {
                    return true;
                }
                else
                {
                    mensajeError = "El banco ingresado no existe.";
                    return false;
                }
            }

            return true;
        }

        public static IList<UI_Message> ValidarSwiftCompleto(InitializationObject initObject, UnitOfWorkCext01 uow, UnitOfWorkSwift uowSwift, T_Swf swiftNuevo, T_CliSwf cliente, T_mt103 montos, IList<LineaMensajeSwift> lineasManuales)
        {
            List<UI_Message> mensajes = new List<UI_Message>();
            string mensajeAux = String.Empty;

            if (String.IsNullOrEmpty(swiftNuevo.BenSwf.NomBen))
            {
                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtNombreBeneficiario", Text = "Falta Ingresar el Nombre del Beneficario." });
            }
            else
            {
                if (!Fn_ValidaCaracteresX(swiftNuevo.BenSwf.NomBen, out mensajeAux))
                {
                    mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtNombreBeneficiario", Text = mensajeAux });
                }
                swiftNuevo.BenSwf.NomBen = swiftNuevo.BenSwf.NomBen.ToUpper();
            }

            if (!String.IsNullOrEmpty(swiftNuevo.BenSwf.DirBen1) && !Fn_ValidaCaracteresX(swiftNuevo.BenSwf.DirBen1, out mensajeAux))
            {
                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtDir1Beneficiario", Text = mensajeAux });
            }

            if (!String.IsNullOrEmpty(swiftNuevo.BenSwf.DirBen2) && !Fn_ValidaCaracteresX(swiftNuevo.BenSwf.DirBen2, out mensajeAux))
            {
                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtDir2Beneficiario", Text = mensajeAux });
            }

            if (!String.IsNullOrEmpty(swiftNuevo.BcoInt.NomBco) && String.IsNullOrEmpty(swiftNuevo.BcoPag.NomBco) && String.IsNullOrEmpty(swiftNuevo.BcoPag.CodCom))
            {
                mensajes.Add(new UI_Message() { Type =TipoMensaje.Error, ControlName = String.Empty, Text = "No es posible ingresar sólo el Banco Intermediario, por lo tanto, debe ingresar un Banco Pagador."});    
            }

            if(String.IsNullOrEmpty(cliente.NomCli)){
                mensajes.Add(new UI_Message() { Type =TipoMensaje.Error, ControlName = "txtClienteNombre", Text = "Falta Ingresar el Nombre del Cliente."});    
            }   

            if(String.IsNullOrEmpty(cliente.DirCli1) && String.IsNullOrEmpty(cliente.DirCli2))
            {
                mensajes.Add(new UI_Message() { Type =TipoMensaje.Error, ControlName = "txtClienteDir1", Text = "Falta Ingresar la Dirección del Cliente."});    
            }

            if (String.IsNullOrEmpty(cliente.PaiCli))
            {
                mensajes.Add(new UI_Message() { Type =TipoMensaje.Error, ControlName = "txtClientePais", Text = "Falta Elegir un País determinado del Cliente."});    
            }
            else
                cliente.PaiCli = cliente.PaiCli.ToUpper();

            if (String.IsNullOrEmpty(swiftNuevo.DatSwf.FecPag))
            {
                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtFechaPago", Text = "La fecha de pago es requerida." });
            }
            else
            {
                if (!ValidarFechaPago(initObject.MODGTAB0, uow, DateTime.Parse(swiftNuevo.DatSwf.FecPag), out mensajeAux))
                {
                    mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtFechaPago", Text = mensajeAux });
                }
            }

            if (swiftNuevo.BenSwf.Es59F && string.IsNullOrWhiteSpace(swiftNuevo.DatSwf.ctacte))
            {
                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtCuentaBeneficiario", Text = "Campo 59F requiere el Número de la cuenta corriente." });
            }
            else
            {
                // Evitamos un null reference ya que el check Banco permite que la cuenta este vacía
                if (!string.IsNullOrWhiteSpace(swiftNuevo.DatSwf.ctacte))
                {
                    if (!Fn_ValidaCaracteresX(swiftNuevo.DatSwf.ctacte, out mensajeAux))
                    {
                        mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtCuentaBeneficiario", Text = mensajeAux });
                    }
                    swiftNuevo.DatSwf.ctacte = swiftNuevo.DatSwf.ctacte.ToUpper();
                }
            }

            if (!string.IsNullOrWhiteSpace(swiftNuevo.DatSwf.RefOpe))
            {
                if (!Fn_ValidaCaracteresX(swiftNuevo.DatSwf.RefOpe, out mensajeAux))
                {
                    mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtReferBeneficiario", Text = mensajeAux });
                }
            }
            
            if (mensajes.Count == 0)
            {
                //no hago validaciones adicionales si ya fallaron las validaciones basicas
                List<T_BcoSwf> bancos = new List<T_BcoSwf>() { swiftNuevo.BcoAla, swiftNuevo.BcoCoD, swiftNuevo.BcoCoE, swiftNuevo.BcoInt, swiftNuevo.BcoPag, swiftNuevo.BcoTer };
                foreach (T_BcoSwf banco in bancos)
                {
                    if (!ValidarCodComp(initObject.MODGSWF, uow, banco.CodCom, out mensajeAux))
                    {
                        mensajes.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = mensajeAux,
                            ControlName = "txtCodCompBanco"
                        });
                    }
                    if (!validarBancoPaymentPlus(uowSwift, banco.SwfBco, out mensajeAux))
                    {
                        mensajes.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = mensajeAux,
                            ControlName = "txtSwiftBanco"
                        });
                    }
                }

                if (swiftNuevo.DatSwf.PlzPag == 0)
                {
                    mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "ddlPaisPlazaDePago", Text = "Debe especificar la Plaza de Pago del Swift." });
                }


                if (swiftNuevo.EsAladi)
                {
                    //Cuando sea una O.P. Aladi  =>  Debe ingresar el Banco Aladi.-
                    if (String.IsNullOrEmpty(swiftNuevo.BcoAla.SwfBco))
                    {
                        mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "ddlTipoBanco", Text = "Debe especificar el Banco Aladi sobre el cual efectuará el Reembolso." });
                    }
                }
                else //no es aladi
                {

                    if (String.IsNullOrEmpty(swiftNuevo.DatSwf.SwfCor))
                    {
                        mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "lstCorresponsales", Text = "Debe existir un Banco Corresponsal a través del cual emitir el Swift." });
                    }


                    if (swiftNuevo.NroSwf == "MT_103") //aca entra solo si el swift ya habia sido generado, sino NroSwft todavia no esta cargado
                    {
                        IList<string> bics = GetBicDeConfig(false);
                        if (bics != null)
                        {
                            if (bics.Contains(swiftNuevo.DatSwf.SwfCor.Substring(4, 2)))
                            { //busco el pais del corresopnsal ingresado
                                if (montos.MtoOri == 0 && montos.TipCam == 0)
                                {
                                    mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtParidadOriginal", Text = "Debe ingresar el monto y paridad original." });
                                }
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(swiftNuevo.BcoTer.NomBco))
                {
                    if (String.IsNullOrEmpty(swiftNuevo.BcoCoE.NomBco))
                    {
                        mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "", Text = "Cuando utiliza el Tercer Banco de Reembolso, debe ingresar el Banco Corresponsal del Remitente." });
                    }

                    if (String.IsNullOrEmpty(swiftNuevo.BcoCoD.NomBco))
                    {
                        mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "", Text = "Cuando utiliza el Tercer Banco de Reembolso, debe ingresar el Banco Corresponsal del Destinatario." });
                    }
                }

                if (montos.Ch_Ori)
                {
                    if (swiftNuevo.CodMon != montos.MndOri)
                    {
                        if (montos.TipCam == 0)
                        {
                            mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtParidadOriginal", Text = "Debe ingresar la paridad original." });
                        }
                    }

                    if (montos.TipCam > 0)
                    {
                        double total = (montos.MtoOri / montos.TipCam) - montos.GasEmi;
                        if (swiftNuevo.mtoswf != total)
                        {
                            mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "", Text = "No cuadran los montos." });
                        }
                    }
                }

                if (swiftNuevo.DatSwf.TipGas == 1) //gastos a cargo del beneficiario
                {
                    if (!swiftNuevo.BenSwf.EsBanco)
                    {
                        //solo para los MT-103
                        double cam = 0;
                        if (montos.TipCam > 0)
                        {
                            cam = (montos.MtoOri / montos.TipCam);
                        }
                        else
                        {
                            cam = montos.MtoOri;
                        }

                        double total = cam - montos.GasEmi;
                        if (swiftNuevo.mtoswf != total)
                        {
                            mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtGastosEmisor", Text = "No cuadran los montos." });
                        }
                    }
                }

                //valido las lineas manuales
                if (swiftNuevo.BenSwf.EsBanco && !swiftNuevo.EsAladi)
                {
                    lineasManuales = lineasManuales.Where(lm => lm.CodMT == T_MODGSWF.MT_202 && lm.Incluido == true).ToList();
                }
                else
                {
                    lineasManuales = lineasManuales.Where(lm => lm.CodMT == T_MODGSWF.MT_103 && lm.Incluido == true).ToList();
                }
                
                foreach(LineaMensajeSwift linea in lineasManuales)
                {
                    if(!linea.ValidarLongitudTotal())
                    {
                        mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "El campo manual " + linea.CodCam + " debe tener una longitud total menor o igual a " + linea.LenTotal.ToString() + " caracteres." });
                    }

                    //Valida caracteres MT 103 campo 72
                    if (linea.CodMT == 103 && linea.CodCam.Trim() == "72")
                    {
                        if (!string.IsNullOrEmpty(linea.Detalle) && !Fn_ValidaCaracteresX(linea.Detalle, out mensajeAux))
                        {
                            mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtLineaManualMT" + linea.CodMT + "Cod" + linea.CodCam.Trim() + "Principal", Text = mensajeAux });
                        }
                        for (int i = 0; i < linea.LineasSecundarias.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(linea.LineasSecundarias[i].Detalle) && !Fn_ValidaCaracteresX(linea.LineasSecundarias[i].Detalle, out mensajeAux))
                            {
                                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtLineaManualMT" + linea.CodMT + "Cod" + linea.CodCam.Trim() + "Ind" + i, Text = mensajeAux });
                            }
                        }
                    }
                }
            }

            return mensajes;
        }

        public static bool GenerarSwift(InitializationObject initObject, UnitOfWorkCext01 uow,  T_Swf swiftNuevo, T_CliSwf cliente, T_mt103 montos, IList<CodigoDeOrdenCampo23Swift> codigosCampo23, IList<LineaMensajeSwift> lineasManuales, short indiceSwift)
        {
            swiftNuevo.DatSwf.ctacte = string.IsNullOrEmpty(swiftNuevo.DatSwf.ctacte) ? string.Empty : swiftNuevo.DatSwf.ctacte.Trim().ToUpper();
            swiftNuevo.BenSwf.NomBen = string.IsNullOrEmpty(swiftNuevo.BenSwf.NomBen) ? string.Empty : swiftNuevo.BenSwf.NomBen.Trim().ToUpper();
            swiftNuevo.BenSwf.DirBen1 = string.IsNullOrEmpty(swiftNuevo.BenSwf.DirBen1) ? string.Empty : swiftNuevo.BenSwf.DirBen1.Trim().ToUpper();
            swiftNuevo.BenSwf.DirBen2 = string.IsNullOrEmpty(swiftNuevo.BenSwf.DirBen2) ? string.Empty : swiftNuevo.BenSwf.DirBen2.Trim().ToUpper();
            swiftNuevo.BenSwf.PaiBen_t = string.IsNullOrEmpty(swiftNuevo.BenSwf.PaiBen_t) ? string.Empty : swiftNuevo.BenSwf.PaiBen_t.Trim().ToUpper();

            initObject.MODGSWF.VSwf[indiceSwift] = swiftNuevo;
            initObject.MODGSWF.VCliSwf = cliente;
            initObject.MODGSWF.VMT103[indiceSwift] = montos;

            if (codigosCampo23 != null && codigosCampo23.Count > 0)
            {
                List<T_Campo23E> camposExistentes = initObject.MODGSWF.VCod.ToList();
                camposExistentes.RemoveAll(c => c.numswi == indiceSwift); //remuevo los que ya hubieran para este swift

                camposExistentes.AddRange(codigosCampo23.Select(s => new T_Campo23E()
                    {
                        numswi = indiceSwift,
                        Codigo = (string.IsNullOrEmpty(s.TextoAdicional) ? s.Codigo.ToString() : s.Codigo.ToString() + "/" + s.TextoAdicional),
                        Estado = 1,
                    }).ToList());

                initObject.MODGSWF.VCod = camposExistentes.ToArray();
            }

            if (swiftNuevo.EsAladi)
            {
                swiftNuevo.DatSwf.NroAla = Mdl_Funciones.Fn_Numero_Aladi(initObject, uow);
                if(String.IsNullOrEmpty(swiftNuevo.DatSwf.NroAla))
                {
                    return false;
                }
            }

            //Identifica si el Beneficiario es un Banco.
            if (swiftNuevo.BenSwf.EsBanco && !swiftNuevo.EsAladi)
            {
                swiftNuevo.NroSwf = "MT-202";
                lineasManuales = lineasManuales.Where(lm => lm.CodMT == T_MODGSWF.MT_202 && lm.Incluido == true).ToList();
            }
            else
            {
                swiftNuevo.NroSwf = "MT-103";
                lineasManuales = lineasManuales.Where(lm => lm.CodMT == T_MODGSWF.MT_103 && lm.Incluido == true).ToList();
            }

            //Flag 50F
            //-------------------------------------------
            if ((initObject.MOD_50F.VG_50F == null) || (initObject.MOD_50F.VG_50F.Length == 0))
            {
                initObject.MOD_50F.VG_50F = new string[initObject.MODGSWF.VSwf.Length, 3];
            }
            initObject.MOD_50F.CHK_50F = cliente.Es50F;
            initObject.MOD_50F.VG_50F[indiceSwift, 1] = (cliente.Es50F ? "1" : "0");
            initObject.MOD_50F.VG_50F[indiceSwift, 2] = (cliente.Es50F ? cliente.PaiCliCod : cliente.PaiCli);


            if (string.IsNullOrEmpty(swiftNuevo.DatSwf.SwfCor))
            {
                swiftNuevo.DatSwf.NomBco = string.Empty;
                swiftNuevo.DatSwf.NomBco = string.Empty;
                swiftNuevo.DatSwf.CiuBco = string.Empty;
                swiftNuevo.DatSwf.PaiBco = string.Empty;
                swiftNuevo.DatSwf.CtaCor = string.Empty;
            }
            else
            {
                var banco = initObject.MODGTAB0.VNom.Where(b => b.Nom_Swf == swiftNuevo.DatSwf.SwfCor).FirstOrDefault();
                if (banco != null)
                {
                    swiftNuevo.DatSwf.BcoCor = banco.Nom_Bco;
                    swiftNuevo.DatSwf.CtaCor = banco.Nom_cta.Trim().ToUpper();
                }
            }

            montos.tipope = 1; // "CRED - STANDARD", nunca se puede cambiar
            if(montos.MndOri == 0) montos.MndOri = -1; //cuando no selecciono nada, lo deja en -1

            swiftNuevo.EstaGen = 1;
            
            initObject.MODGSWF.VSwf[indiceSwift].DocSwf = MODGSWF.GeneraDocSwf(initObject.MODGSWF, initObject.MODGUSR, initObject.MODGTAB0,
                initObject.Mdl_Funciones, initObject.Mdl_Funciones_Varias, initObject.MOD_50F, initObject.Mdi_Principal.MESSAGES, 
                initObject.MODGCVD, initObject.Mdl_Funciones_Varias.CARGA_AUTOMATICA,  uow, lineasManuales, indiceSwift);

            return true;
        }


        private static T_BcoSwf GetBancoQueCorresponde(T_MODGSWF mod, int indicePlanilla, short tipoBanco)
        {
            switch(tipoBanco)
            {
                case T_MODGSWF.BcoAla:
                    return mod.VSwf[indicePlanilla].BcoAla;

                case T_MODGSWF.BcoCoD:
                   return mod.VSwf[indicePlanilla].BcoCoD;

                case T_MODGSWF.BcoCoE:
                    return mod.VSwf[indicePlanilla].BcoCoE;
 
                case T_MODGSWF.BcoInt:
                    return mod.VSwf[indicePlanilla].BcoInt;

                case T_MODGSWF.BcoPag:
                    return mod.VSwf[indicePlanilla].BcoPag;

                case T_MODGSWF.BcoTer:
                    return mod.VSwf[indicePlanilla].BcoTer;
            }

            return null;
        }

        private static bool  Fn_ValidaCaracteresX(string cadena, out string mensajeError)
        {
            mensajeError = String.Empty;

            Regex regex = new Regex("[^A-Za-z0-9/\\-?:().,'+\n\r ]");
            MatchCollection invalidCharacters = regex.Matches(cadena);
            string[] arr = new string[] { };

            if (invalidCharacters.Count > 0)
            {
                arr = invalidCharacters.OfType<Match>()
                    .Select(m => m.Groups[0].Value)
                    .Distinct()
                    .ToArray();

                string invalidCharsString = "'" + string.Join("', '", arr) + "'";
                mensajeError = "Caracteres inválidos: " + invalidCharsString;

                return false;
            }

            return true;
        }
    }
}
