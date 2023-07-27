using BCH.Comex.Core.Entities.Cext01.Common;
using System;

namespace BCH.Comex.Core.Entities.Cext01.Impresion.T_Modulos
{
    public class T_ModImpresion
    {
        #region Constantes
        //----------------------------------------------------------------------------
        //Indicadores de Impresión (Cartas).
        public const short DocxAceLet = 602;
        //Aceptación de Letras.
        public const short DocxCobReg = 601;
        //Registro Cobranza Export.
        public const short DocxCobRen = 603;
        //Reenvío Cobranza Export.
        public const short DocxPagDir = 610;
        //Pago Directo Cobranza Export.
        public const short DocxCobCan = 611;
        //Cancelación Cobranza Export.
        public const short DocxCanRet = 611;
        //Cancelación Retorno Export.
        public const short DocxRegRet = 612;
        //Registro Retorno.
        public const short DocxRegPln = 613;
        //Planillas Visible Export.
        public const short DocxRegCanRet = 614;
        //Registro y Cancelación Retorno Export.
        public const short DocCVD = 620;
        //Compra Venta.
        public const short DocArb = 621;
        //Arbitraje.
        public const short DocCvdI = 402;
        //Ventas Visibles Import.-
        private const short DocGAdeb = 998;//Aviso de Crédito.
        private const short DocGAcre = 999;//Aviso de Débito.
        //Carta de Crédito Export.
        public const short Doc901 = 901;
        //Aviso al Exportador Avisada s/sol conf.
        public const short Doc902 = 902;
        //Consulta de Banco Aladi confirmable a Corresponsales
        public const short Doc903 = 903;
        //Aviso a través de otro Banco Nacional
        public const short Doc904 = 904;
        //Aviso a Beneficiario de Confirmación de Carta de Crédito Disponible por...
        public const short Doc905 = 905;
        //Aviso de Rechazo    Confirmación.-
        public const short Doc906 = 906;
        //Aviso de Aceptación Confirmación.-
        public const short Doc907 = 907;
        //Modificación de CCE a Beneficiarios Tr.-
        public const short Doc908 = 908;
        //Aviso de Transferencia de CCE a Beneficiario Traspasado.-
        public const short Doc909 = 909;
        //Aviso de Traspaso de CCE a Banco de Traspaso.-
        public const short Doc910 = 910;
        //Saldo de Transferencia al BO.-
        public const short Doc911 = 911;
        //Aviso de Modificación al B. O. de la CCE
        public const short Doc912 = 912;
        //Aviso de Traspaso de CCE a Beneficiario Transferido.-
        public const short Doc913 = 913;
        //Aviso de Modificación Banco de Traspaso .-
        public const short Doc914 = 914;
        public const short Doc915 = 915;
        //Aviso de Comisiones.-
        public const short Doc916 = 916;
        public const short Doc917 = 917;
        public const short Doc918 = 918;
        //"Recha Avance s/Conf"
        public const short Doc920 = 920;
        //Solicitud de Confirmación.-
        public const short Doc930 = 930;
        //Aviso de Modificación al Banco Avisador.-
        public const short Doc950 = 950;
        //Remesa al Banco Emisor de Documentos Negociados.
        public const short Doc951 = 951;
        //Remesa al Banco Emisor de Documentos Negociados.
        public const short Doc952 = 952;
        //Carta de Cancelación Carta de Crédito.
        public const short Doc953 = 953;
        public const short Doc954 = 954;
        //Carta al Banco Reembolsante.
        public const short Doc955 = 955;
        //Carta aviso al beneficiario alza Discrepancias.
        public const short Doc801 = 801;
        //"Compra Anticipada L/C"
        public const short Doc802 = 802;
        
        #endregion

        public static string GetTipoCartaDesc(short tipoCarta)
        {
            string desc = "";

            //--------------Etapa de la Carta-----------------
            if (tipoCarta == DocxAceLet)
            {
                desc = "Aceptación";
            }
            else if (tipoCarta == DocxCobReg)
            {
                desc = "Registro";
            }
            else if (tipoCarta == DocxCobRen)
            {
                desc = "Reenvío";
            }
            else if (tipoCarta == DocxPagDir)
            {
                desc = "Cancelación";
            }
            else if (tipoCarta == DocxCobCan)
            {
                desc = "Cancelación";
            }
            else if (tipoCarta == DocxCanRet)
            {
                desc = "Canc. Retorno";
            }
            else if (tipoCarta == DocxRegRet)
            {
                desc = "Reg. Retorno";
            }
            else if (tipoCarta == DocxRegCanRet)
            {
                desc = "Reg. Retorno";
            }
            else if (tipoCarta == DocxRegPln)
            {
                desc = "Planillas";
            }
            else if (tipoCarta == DocGAdeb)
            {
                desc = "Aviso de Débito";
            }
            else if (tipoCarta == DocGAcre)
            {
                desc = "Aviso de Crédito";
            }
            else if (tipoCarta == DocCVD)
            {
                desc = "Compra/Venta";
            }
            else if (tipoCarta == DocCvdI)
            {
                desc = "Vtas. Vis. Import.";
            }
            else if (tipoCarta == DocArb)
            {
                desc = "Arbitraje";
                //Documentos de Carta de Crédito.-
            }
            else if (tipoCarta == Doc901)
            {
                desc = "Registro";
            }
            else if (tipoCarta == Doc902)
            {
                desc = "Cons. de Bco Aladi";
            }
            else if (tipoCarta == Doc903)
            {
                desc = "Aviso otro Bco Nac";
            }
            else if (tipoCarta == Doc904)
            {
                desc = "Aviso de Conf ";
            }
            else if (tipoCarta == Doc905)
            {
                desc = "Aviso de Rech. Conf";
            }
            else if (tipoCarta == Doc906)
            {
                desc = "Aviso de Acep. Conf";
            }
            else if (tipoCarta == Doc907)
            {
                desc = "Modificación";
            }
            else if (tipoCarta == Doc908)
            {
                desc = "Aviso de Transferencia";
            }
            else if (tipoCarta == Doc909)
            {
                desc = "Aviso de Traspaso Bco.";
            }
            else if (tipoCarta == Doc910)
            {
                desc = "Aviso de Saldo";
            }
            else if (tipoCarta == Doc911)
            {
                desc = "Modificación B. O.";
            }
            else if (tipoCarta == Doc912)
            {
                desc = "Aviso de Traspaso B. P.";
            }
            else if (tipoCarta == Doc913)
            {
                desc = "Aviso Mod. Bco. Trasp.";
            }
            else if (tipoCarta == Doc914)
            {
                desc = "Aviso Mod. Bco. Trasp.";
            }
            else if (tipoCarta == Doc915)
            {
                desc = "Aviso de Comisiones";
            }
            else if (tipoCarta == Doc916)
            {
                desc = "Transferencia Rest Esp.";
            }
            else if (tipoCarta == Doc917)
            {
                desc = "Aprob Avance s/Conf";
            }
            else if (tipoCarta == Doc918)
            {
                desc = "Recha Avance s/Conf";
            }
            else if (tipoCarta == Doc920)
            {
                desc = "Solicitud Crédito";
            }
            else if (tipoCarta == Doc950)
            {
                desc = "Utilizac. Bco. Reemb.";
            }
            else if (tipoCarta == Doc951)
            {
                desc = "Utilización Cliente";
            }
            else if (tipoCarta == Doc952)
            {
                desc = "Cancelación Utilización";
            }
            else if (tipoCarta == Doc953)
            {
                desc = "Cancelación Utilización";
            }
            else if (tipoCarta == Doc954)
            {
                desc = "Reembolso";
            }
            else if (tipoCarta == Doc955)
            {
                desc = "Alzamiento Discrepanc.";
            }
            else if (tipoCarta == 611)
            {
                desc = "Cancelación Cob-Ret";
            }
            else if (tipoCarta == 613)
            {
                desc = "";
            }
            else if (tipoCarta == 614)
            {
                desc = "";
            }
            else if (tipoCarta == Doc801)
            {
                desc = "Compra Anticipada L/C";
            }
            else if (tipoCarta == Doc802)
            {
                desc = "Memo Banco Acreedor";
            }
            else
            {
                desc = "***Indeterminado***";
            }

            return desc;
        }

        public static string GetProductoDesc(string codPro)
        {
            string result = String.Empty;
            if (codPro == T_MODGUSR.IdPro_CobExp)
            {
                result = "Exp. Cobranza";
            }
            else if (codPro == T_MODGUSR.IdPro_RetExp)
            {
                result = "Exp. Retorno";
            }
            else if (codPro == T_MODGUSR.IdPro_CobImp)
            {
                result = "Imp. Cobranza";
            }
            else if (codPro == T_MODGUSR.IdPro_CreImp)
            {
                result = "Cart.Cré.Imp.";
            }
            else if (codPro == T_MODGUSR.IdPro_CreCon)
            {
                result = "Cart.Cré.Cont.";
            }
            else if (codPro == T_MODGUSR.IdPro_CreExp)
            {
                result = "Cart.Cré.Exp.";
            }
            else if (codPro == T_MODGUSR.IdPro_PreExp)
            {
                result = "Prés. Exp.";
            }
            else if (codPro == T_MODGUSR.IdPro_ComVen)
            {
                result = "Compra/Venta";
            }
            else if (codPro == T_MODGUSR.IdPro_ConGen)
            {
                result = "GL";
            }
            else if (codPro == T_MODGUSR.IdPro_InfImp)
            {
                result = "Inf. Import.";
            }
            else if (codPro == T_MODGUSR.IdPro_DecImp)
            {
                result = "Dec. Import.";
            }
            else if (codPro == T_MODGUSR.IdPro_PagCCE)
            {
                result = "Compra L/C";
            }
            else if (codPro == T_MODGUSR.IdPro_AvanEx)
            {
                result = "Avance Exp.";
            }

            return result;
        }


    }
}
