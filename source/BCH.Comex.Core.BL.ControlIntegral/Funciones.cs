using BCH.Comex.Core.BL.CONTROLINTEGRAL;


namespace BCH.Comex.Core.BL.ControlIntegral
{
    public class Funciones
    {
        private static ControlIntegralService _XgpyService = new ControlIntegralService();
        //public static List<UI_ComboItem> Cambios_gral_consulta_00_MS(byte Opcion)
        //{
        //    List<UI_ComboItem> listaMonedas = new List<UI_ComboItem>();
        //    //listaActividadEcono.Add(new SelectListItem()
        //    //{
        //    //    Value = "-1",
        //    //    Text = "Seleccione Actividad Economica"
        //    //});
        //    int contador = -1;
        //    foreach (cambios_gral_consulta_00_MS_Result moneda in _XgpyService.Cambios_gral_consulta_00_MS(Opcion))
        //        listaMonedas.Add(new UI_ComboItem()
        //        {                     
        //            Value = moneda.mnd_mndswf   

        //        });
        //    return (listaMonedas);
        //}

        //public static List<SelectListItem> Cambios_gral_consulta_00_MS(byte Opcion)
        //{
        //    List<SelectListItem> listaMonedas = new List<SelectListItem>();

        //    foreach (cambios_gral_consulta_00_MS_Result moneda in _XgpyService.Cambios_gral_consulta_00_MS(Opcion))
        //        listaMonedas.Add(new SelectListItem()
        //        {

        //            Value = moneda.mnd_mndswf,
        //            Text = moneda.mnd_mndnom

        //        });
        //    return (listaMonedas);
        //}

    }
}
