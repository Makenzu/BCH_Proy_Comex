//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;




namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class MODGSGT
    {

        public static T_MODGSGT GetMODGSGT()
        {
            return new T_MODGSGT();
        }


        //public static string RespuestaSgt2(InitializationObject initObj, string Nodo, string RutCons, string Servidor, string Vista, ref string Tabla, string llave, string Oper)
        //{
        //    string RespuestaSgt2 = "";

        //    string s = "";
        //    string R = "";
        //    int x = 0;
        //    string m = "";


        //    int Intentos = 0;
        //    MODGSRM.RowCount = 0;
        //    Intentos = 0;

        //Conssgt2:
        //    Intentos = Intentos + 1;
        //    Tabla = Tabla.UCase();
        //    m = RutCons + Vista.TrimB() + Oper.TrimB() + Tabla.TrimB() + llave;
        //    initObj.MODGSGT.ParamSgt.Mensaje = m;
        //    initObj.MODGSGT.ParamSgt.largo = m.Len();
        //    initObj.MODGSGT.ParamSgt.Status = "00";
        //    initObj.MODGSGT.ParamSgt.Funcion = "08";
        //    initObj.MODGSGT.ParamSgt.Contexto = "00";
        //    object argTemp1 = initObj.MODGSGT.ParamSgt.largo;
        //    x = MODGSRM.srmw8(Nodo, Servidor, initObj.MODGSGT.ParamSgt.Mensaje, ref argTemp1, initObj.MODGSGT.ParamSgt.Status, initObj.MODGSGT.ParamSgt.Funcion, initObj.MODGSGT.ParamSgt.Contexto, initObj.MODGSGT.ParamSgt.Control);
        //    if (!(x == 0 && initObj.MODGSGT.ParamSgt.Mensaje.Left(2) == "00"))
        //    {
        //        if (initObj.MODGSGT.ParamSgt.Mensaje.Left(2) == "96")
        //        {
        //            RespuestaSgt2 = "";
        //            return RespuestaSgt2;
        //        }

        //        if (Intentos <= 1)
        //        {
        //            goto Conssgt2;
        //        }
        //        RespuestaSgt2 = "-1";
        //        return RespuestaSgt2;
        //    }
        //    R = initObj.MODGSGT.ParamSgt.Mensaje.TrimB();

        //    s = R.Mid(3, R.Len() - 2);

        //    RespuestaSgt2 = s;


        //    return RespuestaSgt2;
        //}
    }
}
