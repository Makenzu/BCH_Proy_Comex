using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class Mdl_Acceso
    {
        public static string GetConfigValue(string key) 
        {
            return WebConfigurationManager.AppSettings[key];
        }

        public static InitializationObject Inicializar(UnitOfWorkCext01 unit, IDatosUsuario datosUsuario)
        {
            InitializationObject res = new InitializationObject();

            res.MODGCVD = MODGCVD.GetMODGCVD();
            res.Module1 = Module1.Getmodule1();
            res.MODGFRA = MODGFRA.GetMODGFRA();
            res.MODSWENN = MODSWENN.GetMODSWENN();
            res.MODGMTA = MODGMTA.GetMODGMTA();
            res.MODGSCE = MODGSCE.GetMODGSCE();
            res.MODGTAB0 = MODGTAB0.GetMODGTAB0();
            res.MODGTAB1 = MODGTAB1.GetMODGTAB1();
            res.MODCVDIM = MODCVDIM.GetMODCVDIM();
            res.MODGFYS = MODGFYS.GetMODGFYS();
            res.MODCVDIMMM = MODCVDIMMM.GetMODCVDIMMM();
            res.ModChVrf = ModChVrf.GetModChVrf();
            res.MODPREEM = MODPREEM.GetMODPREEM();
            res.MODVPLE = MODVPLE.GetMODVPLE();
            res.Mdl_Funciones_Varias = Mdl_Funciones_Varias.GetMdl_Funciones_Varias();
            res.MODXVIA = MODXVIA.GetMODXVIA();
            res.MODGARB = MODGARB.GetMODGARB();
            res.MODGPLI1 = MODGPLI1.GetMODGPLI1();
            res.MODXPLN1 = MODXPLN1.GetMODXPLN1();
            res.MODXANU = MODXANU.GetMODXANU();
            res.MODGRNG = MODGRNG.GetMODGRNG();
            res.MODCARAB = MODCARAB.GetMODCARAB();
            res.MODCARMAS = MODCARMAS.GetMODCARMAS();
            res.MODGSWF = MODGSWF.GetMODGSWF();
            res.MOD_50F = new T_MOD_50F();
            res.MODGCHQ = MODGCHQ.GetMODGCHQ();
            res.MODXPLN0 = MODXPLN0.GetMODXPLN0();
            res.MODGUSR = MODGUSR.GetMODGUSR();
            res.MODGANU = MODGANU.GetMODGANU();
            res.MODXORI = MODXORI.GetMODXORI();
            res.MODGASO = MODGASO.GetMODGASO();
            res.MODGCON0 = MODGCON0.GetMODGCON0();
            res.MODGPYF0 = MODGPYF0.GetMODGPYF0();
            res.MODCONT = MODCONT.getMODCONT();
            res.Mdl_Funciones = Mdl_Funciones.GetMdl_Funciones();
            res.MODANUVI = MODANUVI.GetMODANUVI();
            res.MODGPYF2 = MODGPYF2.GetMODGPYF2();
            res.ModSaldo = ModSaldo.GetModSaldo();
            res.MODABDC = MODABDC.GetMODABDC();
            res.MODXSWF = MODXSWF.GetMODXSWF();
            res.MODGNCTA = MODGNCTA.GetMODGNCTA();
            res.Usuario = datosUsuario;
            
            //Validación de usuario.
            if (VerRegistroUsuario(res.MODGUSR, res.Mdi_Principal, datosUsuario, unit, -1) != 0)
            {
                res.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Error al cargar el registro del usuario. Reporte este problema." });
                return res;
            }
            if (~MODGSCE.GetSceGen(res.MODGMTA, res.MODGUSR, res.MODGSCE, res.Mdi_Principal, res.Usuario, unit) != 0)
            {
                res.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Error al cargar las variables generales. Reporte este problema." });
                return res;
            }
            if(~MODGTAB0.SyGetn_Mnd(res.MODGTAB0,unit)!=0){
                res.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Error al cargar las monedas. Reporte este problema." });
                return res;
            }
            if (~MODGTAB0.SyGetn_MndPai(res.MODGTAB0, unit) != 0)
            {
                res.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Error al cargar las monedas de los paises. Reporte este problema." });
                return res;
            }
            if (~MODGTAB0.SyGetn_Pai(res.MODGTAB0,unit)!=0){
                res.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Error al cargar los países. Reporte este problema." });
                return res;
            }
            if (~MODGTAB1.SyGetn_Pbc(res.MODGTAB1,unit) != 0) {
                res.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se pudo encontrar el código de Plaza del Banco Central. Reporte este problema." });
                return res;
            }

            res.MODGCVD.CodMonDolar = (short)VB6Helpers.Val(GetConfigValue("FundTransfer.Monedas.CodMonedaDolar"));
            res.MODGCVD.CodMonedaNac = (short)VB6Helpers.Val(GetConfigValue("FundTransfer.Monedas.CodMonedaNacional"));
            res.MODGCVD.VgCVDVacia.TcpConDec = GetConfigValue("FundTransfer.Exportaciones.TcpConDec");
            res.MODGCVD.VgCVDVacia.TcpSinPai = GetConfigValue("FundTransfer.Exportaciones.TcpSinPai");
            res.MODGCVD.VgCVDVacia.TcpConvenio = GetConfigValue("FundTransfer.Exportaciones.TcpConvenio");
            res.MODGCVD.VgCVDVacia.TcpAutBcoCen = GetConfigValue("FundTransfer.Importaciones.TcpAutBcoCen");

            if (string.IsNullOrWhiteSpace(res.MODGCVD.VgCVDVacia.TcpAutBcoCen))
            {
                if (MODGPYF0.SetConfigValue("FundTransfer.Importaciones.TcpAutBcoCen", "251119044;252603015;251704016;26135101K") != 0)
                {
                    res.MODGCVD.VgCVDVacia.TcpAutBcoCen = GetConfigValue("FundTransfer.Importaciones.TcpAutBcoCen");
                }
            }

            Module1.ResetParty(res.Module1,res.MODGCVD.Beneficiario);
            res.MODGCVD.Beneficiario[T_MODGCVD.ICli] = "Cliente";
            res.MODGCVD.Beneficiario[T_MODGCVD.IOtr] = "Otro";
            res.MODGCVD.Beneficiario[T_MODGCVD.ICom] = "Comprador";
            Module1.ResetParty(res.Module1, res.MODGCVD.Beneficiario);

            res.MODGFRA.V_FraGen.Idioma = "E";
            res.MODGFRA.V_FraGen.codpro = short.Parse(T_MODGUSR.IdPro_CobExp);
            res.MODGFRA.V_FraGen.codfun = MODGFRA.FunIng_CobExp;

            //res.MODXPRN1.IdProd_xPrn = T_MODGUSR.IdPro_ComVen;

            res.MODSWENN.Hab_Swift = (short)(MODSWENN.Habil_SWIFT(unit, res.MODSWENN, res.MODGUSR, T_MODGUSR.IdPro_ComVen, T_MODGUSR.IdPro_CVD) ? -1 : 0);

            

            //lo de SyGet_NodeSal() no se puso porque se supone que los datos estan en la DAL

            return res;
        }

        public static short VerRegistroUsuario(T_MODGUSR MODGUSR, UI_Mdi_Principal Mdi_Principal, IDatosUsuario datosUsuario, UnitOfWorkCext01 unit, short Valida)
        {
            string Usuario = "";
            short x = 0;
            string UsrOrig = "";
            
            //Usuario = GetConfigValue("FundTransfer.Identificacion.CCtUsr"); //usuario correspondiente a la estacion de trabajo
            Usuario = datosUsuario.Identificacion_CCtUsr;
            if (Usuario.Equals("")) {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Text = "No está especificado el usuario correspondiente a la Estación de Trabajo. Reporte este problema.", Type = TipoMensaje.Error });
                return -1;
            }
            x = BCH.Comex.Core.BL.XCFT.Modulos.MODGUSR.SyGet_Usr(MODGUSR, Mdi_Principal,unit, VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2));
            if (~x != 0) {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Text = "No está se pudo obtener el usuario correspondiente a la Estación de Trabajo. Reporte este problema.", Type = TipoMensaje.Error });
                return -1;
            }
            x = BCH.Comex.Core.BL.XCFT.Modulos.MODGUSR.SyGet_OfiUsr(MODGUSR,unit, VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2));
            if (~x != 0)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Text = "No está se pudo obtener la oficina del usuario correspondiente a la Estación de Trabajo. Reporte este problema.", Type = TipoMensaje.Error });
                return -1;
            }
            //Verifica que se haya hecho Inicio de Día Hoy.-
            if (MODGUSR.UsrEsp.Tipeje == "O")
            {
                if (Valida != 0)
                {
                    x = BCH.Comex.Core.BL.XCFT.Modulos.MODGUSR.SyGetf_Usr(MODGUSR, Mdi_Principal, unit, VB6Helpers.Left(Usuario, 3), 
                        VB6Helpers.Right(Usuario, 2), "I");
                    if (~x != 0)
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message() { Text = "No está se pudo obtener las fechas del usuario correspondiente a la Estación de Trabajo. Reporte este problema.", Type = TipoMensaje.Error });
                        return -1;
                    }
                }

            }
            //UsrOrig = GetConfigValue("FundTransfer.Identificacion.CCtUsro");
            UsrOrig = datosUsuario.Identificacion_CCtUsro;
            MODGUSR.UsrEsp.CCtOrig = VB6Helpers.Left(UsrOrig, 3);
            MODGUSR.UsrEsp.EspOrig = VB6Helpers.Right(UsrOrig, 2);
            MODGUSR.UsrEsp.RempOrig = Comex.Core.BL.XCFT.Modulos.MODGUSR.SyGet_RempOrig(MODGUSR,unit, MODGUSR.UsrEsp.CCtOrig, MODGUSR.UsrEsp.EspOrig);
            return 0;
        }

        public static string GetSceIni(string Key)
        {
            return WebConfigurationManager.AppSettings[Key];
        }
    }
}
