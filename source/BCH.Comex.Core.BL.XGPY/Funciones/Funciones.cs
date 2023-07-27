using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace BCH.Comex.Core.BL.XGPY
{
    public class Funciones
    {

        private static XgpyService _XgpyService = new XgpyService();


        public static class Lista
        {

            #region "Caracteristicas Participantes"
            public static List<UI_ComboItem> Ejecutivos(int codigo)
            {
                List<UI_ComboItem> listaEjecutivos = new List<UI_ComboItem>();
                foreach (sgt_ejc_s02_MS_Result ejecutivos in _XgpyService.Sgt_ejc_s02_MS(codigo))
                    listaEjecutivos.Add(new UI_ComboItem()
                    {
                        
                        Value = ejecutivos.ejc_ejcnom,
                        Data = Convert.ToInt32(ejecutivos.ejc_ejccod)
                    });
                return (listaEjecutivos);
            }

            public static List<UI_ComboItem> ActividadEconomica()
            {
                List<UI_ComboItem> listaActividadEcono = new List<UI_ComboItem>();
                //listaActividadEcono.Add(new SelectListItem()
                //{
                //    Value = "-1",
                //    Text = "Seleccione Actividad Economica"
                //});
                foreach (sgt_aec_s01_MS_Result actividadEcono in _XgpyService.Sgt_aec_s01_MS())
                    listaActividadEcono.Add(new UI_ComboItem()
                    {
                        Value = actividadEcono.aec_aecnom,
                        Data = Convert.ToInt32(actividadEcono.aec_aeccod)
                    });
                return (listaActividadEcono);
            }


            //public static List<SelectListItem> ClaseRiesgo()
            //{
            //    List<SelectListItem> listaClaseRiesgo = new List<SelectListItem>();
            //    foreach (sgt_clf_s01_MS_Result claseRiesgo in _XgpyService.Sgt_clf_s01_MS())
            //        listaClaseRiesgo.Add(new SelectListItem()
            //        {
            //            Text = claseRiesgo.clf_clfdes,
            //            Value = claseRiesgo.clf_clfcod
            //        });
            //    return (listaClaseRiesgo);
            //}
            //public static short ClaseRiesgo(T_PRTGLOB PRTGLOB, UnitOfWorkCext01 unit)
            //{
            //    short _retValue;
            //    try
            //    {
            //        IList<sgt_clf_s01_MS_Result> resultados = _XgpyService.Sgt_Clf_S01_MS();
            //        PRTGLOB.riesgo = resultados.Select(x => new tipo_riesgo()
            //        {
            //            codigo = x.clf_clfcod,
            //            nombre = x.clf_clfdes
            //        }).OrderBy(x => x.codigo).ToArray();
            //        _retValue = -1;

            //    }
            //    catch (Exception ex)
            //    {
            //        _retValue = 0;
            //    }
            //    return _retValue;
            //}

            public static List<UI_ComboItem> Clasificacion()
            {
                List<UI_ComboItem> listaClasificacion = new List<UI_ComboItem>();
                listaClasificacion.Add(new UI_ComboItem() { Data = 0, Value = "Agente" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 1, Value = "Asegurador" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 2, Value = "Banco" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 3, Value = "Cliente" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 4, Value = "Comisionista" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 5, Value = "Exportador" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 6, Value = "Importador" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 7, Value = "Transportista" });
                listaClasificacion.Add(new UI_ComboItem() { Data = 8, Value = "Sin Clasificación" });
                return (listaClasificacion);
            }

            #endregion

            #region "Instrucciones Participantes"

            public static List<SelectListItem> Instrucciones()
            {
                List<SelectListItem> listaInstrucciones = new List<SelectListItem>();
                listaInstrucciones.Add(new SelectListItem() { Value = "0", Text = "Generales de Importación" });
                listaInstrucciones.Add(new SelectListItem() { Value = "1", Text = "Generales de Exportación" });
                listaInstrucciones.Add(new SelectListItem() { Value = "2", Text = "Generales de Servicios" });
                listaInstrucciones.Add(new SelectListItem() { Value = "3", Text = "Cobranzas de Importación" });
                listaInstrucciones.Add(new SelectListItem() { Value = "4", Text = "Cobranzas de Exportación" });
                listaInstrucciones.Add(new SelectListItem() { Value = "5", Text = "Carta de Crédito de Importación" });
                listaInstrucciones.Add(new SelectListItem() { Value = "6", Text = "Carta de Crédito de Exportación" });

                return (listaInstrucciones);
            }


            #endregion

            #region "Cuentas Participantes"



            #endregion
            public static UI_Combo Monedas()
            {
                UI_Combo Monedas = new UI_Combo();

                Monedas.Items.Add(new UI_ComboItem { Data = 1, Value = "PESO CHILENO" });
                Monedas.Items.Add(new UI_ComboItem { Data = 11, Value = "DOLAR USA" });
                Monedas.Items.Add(new UI_ComboItem { Data = 15, Value = "DOLAR CANADA" });
                Monedas.Items.Add(new UI_ComboItem { Data = 16, Value = "DOLAR HONG KONG" });
                Monedas.Items.Add(new UI_ComboItem { Data = 18, Value = "DOLAR NEOZELANDES" });

                return Monedas;
            }
        }

        public static String CleanPhoneNumber(String phoneFax)
        {
            String retClean = phoneFax;
            int n;
            Boolean isNumeric;

            isNumeric = Int32.TryParse(phoneFax, out n);
            if (isNumeric)
            {
                if (n == 0)
                {
                    retClean = "";
                }
            }

            return retClean;
        }
    }
}
