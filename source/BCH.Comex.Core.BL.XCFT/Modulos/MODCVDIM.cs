using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODCVDIM
    {
        public static T_MODCVDIM GetMODCVDIM() {
            return new T_MODCVDIM();
        }

        public static void Inicializa(T_MODCVDIM MODCVDIM, T_MODGFYS MODGFYS, T_MODGASO MODGASO, T_MODCVDIMMM MODCVDIMMM , short AsignaNum)
        {
            // UPGRADE_INFO (#05B1): The 'CodopVacio' variable wasn't declared explicitly.
            CdOper[] CodopVacio = null;
            // UPGRADE_INFO (#05B1): The 'En_Comision' variable wasn't declared explicitly.
            dynamic En_Comision = null;
            // UPGRADE_INFO (#05B1): The 'Tipo_Op' variable wasn't declared explicitly.
            dynamic Tipo_Op = null;
            // UPGRADE_INFO (#05B1): The 'EsAnulacion' variable wasn't declared explicitly.
            dynamic EsAnulacion = null;
            // UPGRADE_INFO (#05B1): The 'ConReempl' variable wasn't declared explicitly.
            dynamic ConReempl = null;
            // UPGRADE_INFO (#05B1): The 'OkRev' variable wasn't declared explicitly.
            dynamic OkRev = null;
            // UPGRADE_INFO (#05B1): The 'PorBoton' variable wasn't declared explicitly.
            dynamic PorBoton = null;
            // UPGRADE_INFO (#05B1): The 'los_cheques' variable wasn't declared explicitly.
            dynamic los_cheques = null;
            // UPGRADE_INFO (#05B1): The 'Party' variable wasn't declared explicitly.
            dynamic Party = null;
            // UPGRADE_INFO (#05B1): The 'origen' variable wasn't declared explicitly.
            dynamic origen = null;
            // UPGRADE_INFO (#05B1): The 'MSwift' variable wasn't declared explicitly.
            dynamic MSwift = null;
            // UPGRADE_INFO (#05B1): The 'Doc_Val' variable wasn't declared explicitly.
            dynamic Doc_Val = null;
            // UPGRADE_INFO (#05B1): The 'HayVuelto' variable wasn't declared explicitly.
            dynamic HayVuelto = null;
            // UPGRADE_INFO (#05B1): The 'HaySwift' variable wasn't declared explicitly.
            dynamic HaySwift = null;
            // UPGRADE_INFO (#05B1): The 'HayDoc' variable wasn't declared explicitly.
            dynamic HayDoc = null;
            // UPGRADE_INFO (#05B1): The 'Doble_Click' variable wasn't declared explicitly.
            dynamic Doble_Click = null;
            // UPGRADE_INFO (#05B1): The 'Final' variable wasn't declared explicitly.
            dynamic Final = null;
            // UPGRADE_INFO (#05B1): The 'Finaliza' variable wasn't declared explicitly.
            dynamic Finaliza = null;
            // UPGRADE_INFO (#05B1): The 'Canc_Origen' variable wasn't declared explicitly.
            dynamic Canc_Origen = null;
            // UPGRADE_INFO (#05B1): The 'reversion_hecha' variable wasn't declared explicitly.
            dynamic reversion_hecha = null;
            // UPGRADE_INFO (#05B1): The 'modificacion' variable wasn't declared explicitly.
            dynamic modificacion = null;
            // UPGRADE_INFO (#05B1): The 'solicitud_manual' variable wasn't declared explicitly.
            dynamic solicitud_manual = null;
            // UPGRADE_INFO (#05B1): The 'ultimo_quiensoy' variable wasn't declared explicitly.
            dynamic ultimo_quiensoy = null;
            // UPGRADE_INFO (#05B1): The 'Tipo_Operacion' variable wasn't declared explicitly.
            dynamic Tipo_Operacion = null;
            // UPGRADE_INFO (#05B1): The 'num_oper_comex' variable wasn't declared explicitly.
            dynamic num_oper_comex = null;
            // UPGRADE_INFO (#05B1): The 'Producto_Seccion' variable wasn't declared explicitly.
            dynamic Producto_Seccion = null;
            // UPGRADE_INFO (#05B1): The 'PRODUCTO_VARIOS' variable wasn't declared explicitly.
            dynamic PRODUCTO_VARIOS = null;

            MODGFYS.VgFyS = new estr_cv[0];
            MODGASO.VgAso = MODGASO.VgAsoNul.Clone();

            En_Comision = false;
            Tipo_Op = 0;
            CodopVacio = new CdOper[0];
            EsAnulacion = false;
            MODCVDIMMM.EsReverso = -1;

            ConReempl = false;
            OkRev = false;
            PorBoton = false;

            MODCVDIMMM.UltServ = 0;
            MODCVDIMMM.ServAct = 0;
            los_cheques = 0;
            En_Comision = false;

            Party = false;
            origen = false;
            MSwift = false;
            Doc_Val = false;

            HayVuelto = false;
            HaySwift = false;
            HayDoc = false;

            Doble_Click = false;
            Final = false;
            Finaliza = false;
            Canc_Origen = false;

            MODGFYS.Es_Nuevo = -1;

            reversion_hecha = false;
            modificacion = false;
            solicitud_manual = false;

            ultimo_quiensoy = "";
            Tipo_Operacion = "";
            num_oper_comex = "";

            Producto_Seccion = PRODUCTO_VARIOS;

            MODGFYS.IdiIni = new Idi[0];
            MODGFYS.Idifin = new Idi[0];
            MODGFYS.DecFin = new dec[0];
            MODCVDIMMM.RDecIni = new r_dec[0];
            MODCVDIMMM.RDecFin = new r_dec[0];


            MODGFYS.CVD.EsVisible = -1;
            MODGFYS.CVD.Operacion = 0;
            MODGFYS.CVD.Int = 0;

        }
    }
}
