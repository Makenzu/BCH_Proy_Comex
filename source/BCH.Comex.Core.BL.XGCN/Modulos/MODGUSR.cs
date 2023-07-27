using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGCN.Modulos
{
    public static class MODGUSR
    {
        //public static int VerRegistroUsuario(int Valida)
        //{
        //    int VerRegistroUsuario = 0;

        //    string UsrOrig = "";
        //    int X = 0;
        //    string usuario = "";

        //    // ----------------------------------------------------------------------------
        //    // © 13/10/93 by WaldoSoft, Version 1.0
        //    // Esta rutina carga los atributos del especialista asociado al Rut, conectando
        //    // solo aquel que esta marcado como "en operacion".-
        //    // ----------------------------------------------------------------------------

        //    const string Validos = "0123456789K";
        //    const string FormaRut = "@@@.@@@.@@@-@";
        //    string LeRut = "";
        //    string Rut = "";
        //    string Aux = "";
        //    int Ind = 0;
        //    string BaseUser = "";
        //    string Selec = "";
        //    int Cuantos = 0;
        //    int Operando = 0;
        //    string LeRutOtro = "";

        //    // ok, preparamos para posible error.-
        //    try
        //    {

        //        // Obtiene los datos del Especialista.-
        //        usuario = MODGPYF0.GetSceIni("Identificacion", "CCtUsr");
        //        if (usuario == "")
        //        {
        //            MigrationSupport.Utils.MsgBox("No está especificado el usuario correspondiente a la Estación de Trabajo. Reporte este problema.", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MODGUSR.MsgUsr);
        //            Environment.Exit(0);
        //        }
        //        X = SyGet_Usr(usuario.Left(3), usuario.Right(2));
        //        if (X == 0)
        //        {
        //            Environment.Exit(0);
        //        }
        //        X = SyGet_OfiUsr(usuario.Left(3), usuario.Right(2));
        //        if (X == 0)
        //        {
        //            Environment.Exit(0);
        //        }

        //        // Verifica que se haya hecho Inicio de Día Hoy.-
        //        // If UsrEsp.Tipeje = "O" Then
        //        //     If Valida Then
        //        //          X% = SyGetf_Usr(Left$(usuario, 3), Right$(usuario, 2), "I")
        //        //         If Not X% Then End
        //        //     End If
        //        // End If
        //        // 
        //        // Identifica Usuario Original.-
        //        UsrOrig = MODGPYF0.GetSceIni("Identificacion", "CCtUsro");
        //        UsrEsp.CCtOrig = UsrOrig.Left(3);
        //        UsrEsp.EspOrig = UsrOrig.Right(2);

        //        // Reemplzaos del Usuario Original.-
        //        UsrEsp.RempOrig = SyGet_RempOrig(UsrEsp.CCtOrig, UsrEsp.EspOrig);


        //        return VerRegistroUsuario;

        //    }
        //    catch (Exception exc)
        //    {
        //        MigrationSupport.GlobalException.Initialize(exc);
        //        LeRut = MODGPYF0.Componer(RegUsr_ErrAccess, "@", MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + ":VerRegistroUsuario");
        //        MigrationSupport.Utils.MsgBox(LeRut + MigrationSupport.Utils.GetErrorDescrption(MigrationSupport.GlobalException.Instance.Number), MODGPYF0.pito(16).Cast<MigrationSupport.MsgBoxStyle>(), RegUsr_Titulo);
        //        VerRegistroUsuario = true.ToInt();

        //    }
        //    return VerRegistroUsuario;
        //}

        public static bool SyGet_Usr(string cencos, string codusr, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {

                try
                {
                    var r = uow.SceRepository.Sce_Usr_S05_MS(cencos, codusr);

                    // Se realizó el Query pero la consulta no retornó datos.-
                    if (r == null)
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Los Datos del Usuario no han sido encontrados (Sce_Usr)",
                            Title = "Devengo",
                            Type = TipoMensaje.Informacion
                        });
                        tracer.TraceError("Los Datos del Usuario no han sido encontrados (Sce_Usr)");
                        return false;
                    }

                    sce_usr datosUsuario = new sce_usr()
                    {
                        cent_costo = r.cent_costo,
                        cent_super = r.cent_super,
                        ciudad = r.ciudad,
                        comuna = r.comuna,
                        delegada = r.delegada,
                        direccion = r.direccion,
                        fax = r.fax,
                        id_especia = r.id_especia,
                        id_super = r.id_super,
                        jerarquia = r.jerarquia,
                        nombre = r.nombre,
                        ofic_orige = r.ofic_orige,
                        rut = r.rut,
                        seccion = r.seccion,
                        swift = r.swift,
                        telefono = r.telefono,
                        tipeje = r.tipeje
                    };

                    globales.MODGUSR.UsrEsp = datosUsuario;

                    if (SyGet_Remp(datosUsuario.cent_costo, datosUsuario.id_especia, globales, uow) == "-1")
                    {
                        return false;
                    }

                    return true;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("SyGet_Usr", exc);
                    //todo: manejar la excepcion como corresponde
                    throw;
                }
            }
        }

        public static string SyGet_Remp(string CodCct, string CodEsp, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {

                    var queryResult = uow.SceRepository.Sce_Usr_S06_MS(CodCct, CodEsp);
                    if (queryResult != null && queryResult.Count > 0)
                    {
                        globales.MODGUSR.UsrEsp.reemplazos = queryResult.Aggregate((x, y) => x + ";" + y); //concateno separando por ;
                    }

                    return globales.MODGUSR.UsrEsp.reemplazos;

                }
                catch (Exception exc)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer los reemplazos de los Usuarios",
                        Title = "Identificación de Usuarios",
                        Type = TipoMensaje.Informacion
                    });
                    tracer.TraceException("Alerta, no se han podido leer los reemplazos de los Usuario", exc);
                    throw;
                }
            }

        }

        public static bool SyGet_OfiUsr(string cencos, string codusr, T_MODGUSR MODGUSR, UnitOfWorkCext01 unit)
        {
            bool _retValue = false;
            try
            {

                string userOfi = unit.SceRepository.EjecutarSP<string>("sce_usr_s09_MS", cencos, codusr).First();
                MODGUSR.UsrEsp.ofixusr = userOfi;

                _retValue = true;
            }
            catch (Exception e)
            {

            }
            return _retValue;
        }

        //Lee las Fechas de la Tabla de Usuarios : Sce_Usr.-
        public static bool SyGetf_Usr(T_MODGUSR MODGUSR, DatosGlobales globales, UnitOfWorkCext01 unit, string cencos, string codusr, string Etapa)
        {
            bool _retValue;
            try
            {
                var fechas = unit.SceRepository.EjecutarSP<sce_usr_s04_MS_Result>("sce_usr_s04_MS", cencos, codusr).First();
                DateTime FechaIni = fechas.fec_fin;
                DateTime FechaFin = fechas.fec_fin;
                DateTime FechaOut = fechas.fec_out;
                if (MODGUSR.UsrEsp.id_especia != "00")
                {
                    switch (Etapa)
                    {
                        case "I":
                            if (FechaIni < FechaFin)
                            {
                                globales.ListaMensajesError.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia." });
                            }
                            if (FechaIni.Date == FechaOut.Date)
                            {
                                globales.ListaMensajesError.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación." });
                            }
                            break;
                    }
                }
                _retValue = true;

            }
            catch (Exception ex)
            {
                _retValue = false;
            }
            return _retValue;
        }

    }
}