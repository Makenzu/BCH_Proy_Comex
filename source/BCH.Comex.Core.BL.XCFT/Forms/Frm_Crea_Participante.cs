using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services.XCFT.ConsultaDireccionesProducto;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Crea_Participante
    {

        public static void Form_Load(InitializationObject initObj)
        {
            string KeyOper = "";
            short i = 0;

            initObj.Frm_Crea_Participante.rut.Visible = true;
            //initObj.Frm_Crea_Participante.txt_rut.Text = "";

            //Recuperamos las llaves y parametros
            KeyOper = initObj.Module1.PrtControl.NumOpe.Cent_Costo + "-";
            KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Product + "-";
            KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Especia + "-";
            KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Empresa + "-";
            KeyOper += initObj.Module1.PrtControl.NumOpe.Id_Operacion;

            initObj.Frm_Crea_Participante.Caption = " (" + KeyOper + ")";


            //cargamos la tabla de paises
            initObj.Frm_Crea_Participante.Pais.Items.Clear();
            for (i = 0; i < initObj.MODGTAB0.VPai.Length; i++)
            {
                initObj.Frm_Crea_Participante.Pais.Items.Add(new UI_ComboItem() {
                    Value = initObj.MODGTAB0.VPai[i].Pai_PaiNom,
                    Data = initObj.MODGTAB0.VPai[i].Pai_PaiCod
                });
            }
            initObj.Frm_Crea_Participante.Pais.ListIndex = -1;

            if (initObj.Frm_Crea_Participante.Pais.Items.Count == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message{
                    Text = UI_Frm_Crea_Participante.GPrt_ErrTablaPais,
                    Title = T_Module1.GPrt_Caption,
                    Type = TipoMensaje.Informacion
                });
            }

            //agregar a la lista el "Otro Pais"
            //Pais.AddItem(GPrt_OtroPais);
            //Pais.set_ItemData(Pais.NewIndex, -1);

            initObj.Mdl_Funciones_Varias.CARGA_PARTY = 0;

            initObj.Frm_Crea_Participante.nuestro_pais = (short)VB6Helpers.Val(Mdl_Acceso.GetSceIni("FundTransfer.Pais.CodPais")); ;




            if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                var itemIndex = initObj.Frm_Crea_Participante.Pais.Items.FindIndex(x => x.Data == 997);
                if (itemIndex > 0 )
                    initObj.Frm_Crea_Participante.Pais.ListIndex = itemIndex;
            }
        }

        
        public static bool Aceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string Txt = "";

            //RTO 2012/03/2012 INCIDENTE IR46139
            if (string.IsNullOrEmpty(initObj.Frm_Crea_Participante.rut.Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = T_Module1.GPrt_ErrRut,
                    Title = T_Module1.GPrt_Caption,
                    Type = TipoMensaje.Informacion
                });

                return false;
            }
            else
            {
                if (Module1.NoEsRut((initObj.Frm_Crea_Participante.rut.Text)) != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = T_Module1.GPrt_ErrRut,
                        Title = T_Module1.GPrt_Caption,
                        Type = TipoMensaje.Informacion
                    });

                    return false;
                }
            }

            //RTO 2012/03/2012 INCIDENTE IR46139

            //validar ingreso minimo
            if (string.IsNullOrEmpty(initObj.Frm_Crea_Participante.Nombre.Text))
            {
                Txt = "Nombre o Razon Social" + T_Module1.GPrt_ErrRequerido;
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = Txt,
                    Title = T_Module1.GPrt_Caption,
                    Type = TipoMensaje.Informacion
                });

                return false;
            }

            if (string.IsNullOrEmpty(initObj.Frm_Crea_Participante.Direccion.Text) && 
                initObj.Frm_Crea_Participante.envio[0].Selected)
            {
                Txt = "Direccion" + T_Module1.GPrt_ErrRequerido;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = Txt,
                    Title = T_Module1.GPrt_Caption,
                    Type = TipoMensaje.Informacion
                });

                return false;
            }

            //ok, marcar como ingreso ok
            if (initObj.Mdl_Funciones_Varias.CARGA_PARTY == 1)
            {
                //DEBE GRABAR PARTICIPANTE EN LA BD
                if (!Graba_Participante(initObj, uow))
                    return false;
            }

            initObj.Frm_Crea_Participante.Aceptar.Tag = T_Module1.GPrt_RetExiste;
            initObj.Frm_Crea_Participante.Aceptar.Enabled = false;

            return true;
        }

        //@TODO: Mascara
        //private void Fax_LostFocus()
        //{
        //    if (Fax.ClipText == "")
        //    {
        //        return;
        //    }

        //    //alinear a la derecha
        //    if (Pais.ListIndex >= 0)
        //    {
        //        if (Pais.get_ItemData((short)Pais.ListIndex) == nuestro_pais)
        //        {
        //            Fax.SelText = Module1.DesCero((Fax.ClipText), GPrt_FonoMascara, 0);
        //        }
        //    }
        //}

        public static bool Cancelar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.Frm_Crea_Participante.Aceptar.Tag = VB6Helpers.Format(VB6Helpers.CStr(T_Module1.GPrt_RetCancelo));
            if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0){}
            initObj.Frm_Crea_Participante.Aceptar.Enabled = false;
            //Desaparece formulario CreaParticipante.
            //this.Hide();
            return true; 
        }

        public static void Consulta_Click(InitializationObject initObj)
        {
            if (!string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.Lc_BaseNumber))
            {
                var datosHeader = new Data.DAL.Services.XCFT.ConsultaCuentaCorriente.datosHeaderRequest();
                var reqConsultaCtaCte = new Data.DAL.Services.XCFT.ConsultaCuentaCorriente.reqConsultaCuentaCorriente();
                reqConsultaCtaCte.cuenta = initObj.Mdl_Funciones_Varias.Lc_BaseNumber;

                Data.DAL.Services.XCFT.ConsultaCuentaCorriente.respConsultaCuentaCorriente respConsultaCtaCte;
                Data.DAL.Services.XCFT.ConsultaCuentaCorriente.datosHeaderResponse resp;

                try
                {
                    var client = new Data.DAL.Services.XCFT.ConsultaCuentaCorriente.ConsultaCuentaCorrienteClient();

                    resp = client.ConsultaCuentaCorriente(datosHeader, reqConsultaCtaCte, out respConsultaCtaCte);
                    if (respConsultaCtaCte != null)
                    {
                        initObj.Mdl_Funciones_Varias.CARGA_PARTY = 1;
                        //initObj.Frm_Crea_Participante.rut.Visible = false;
                        //initObj.Frm_Crea_Participante.rut.Text = "";
                        //initObj.Frm_Crea_Participante.txt_rut.Visible = true;
                        //         txt_rut.Text = parser.selectSingleNode("/soapenv:Envelope/soapenv:Body/ns1:ConsultaCuentaCorrienteResponse/respConsultaCuentaCorriente/ns0:Respuesta/ns0:RutTitular").Text
                        //         Nombre.Text = parser.selectSingleNode("/soapenv:Envelope/soapenv:Body/ns1:ConsultaCuentaCorrienteResponse/respConsultaCuentaCorriente/ns0:Respuesta/ns0:NombreTitular").Text
                        // PFI 2012-03-30
                        initObj.Frm_Crea_Participante.rut.Text = respConsultaCtaCte.Respuesta.RutTitular;
                        initObj.Frm_Crea_Participante.Nombre.Text = respConsultaCtaCte.Respuesta.NombreTitular;

                        datos_Click(initObj);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ha ocurrido un error al obtener la cuenta corriente", ex);
                }
            }
        }

        public static void datos_Click(InitializationObject initObj)
        {
            try
            {
                var header = new Data.DAL.Services.XCFT.ConsultaDireccionesProducto.datosHeaderRequest();
                header.consumidor = new Data.DAL.Services.XCFT.ConsultaDireccionesProducto.datosConsumidor()
                {
                    idApp = "Teller1.0",
                    usuario = "jtilleria"
                };
                header.transaccion = new Data.DAL.Services.XCFT.ConsultaDireccionesProducto.datosTransaccion()
                {
                    internalCode = "?",
                    idTransaccionNegocio = "GENlkjdsadljalskjd0001",
                    canal = "CAJA",
                    sucursal = "000",
                    fechaHora = DateTime.Now,// "2009-11-03T13:13:13
                };

                Data.DAL.Services.XCFT.ConsultaDireccionesProducto.datosEntrada entrada = new datosEntrada()
                {
                    numeroProducto = initObj.Mdl_Funciones_Varias.Lc_BaseNumber,
                    rutCliente = initObj.Frm_Crea_Participante.rut.Text,
                };

                Data.DAL.Services.XCFT.ConsultaDireccionesProducto.datosSalida salida;

                var client = new Data.DAL.Services.XCFT.ConsultaDireccionesProducto.ConsultaDireccionesProductoClient();
                var resp = client.ConsultarDireccionesProducto(header, entrada, out salida);

                if (salida != null && salida.listaDireccionesPro != null && (salida.listaDireccionesPro.Count() > 0))
                {
                    //salida.listaDireccionesPro
                    var datos = salida.listaDireccionesPro[0];
                    initObj.Frm_Crea_Participante.Direccion.Text = datos.direccion;
                    initObj.Frm_Crea_Participante.Ciudad.Text = datos.ciudad;
                    initObj.Frm_Crea_Participante.comuna.Text = datos.comuna;
                    initObj.Frm_Crea_Participante.Estado.Text = datos.region;
                    initObj.Frm_Crea_Participante.Postal.Text = datos.codigoPostal;
                    initObj.Frm_Crea_Participante.Aceptar.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Ha ocurrido un error al consultar las direcciones", ex);
            }
        }


        public static bool Graba_Participante(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            bool ok = false;

            short Li_Largo1 = 0;
            short Li_Largo2 = 0;
            short APartirDe = 0;
            bool sigue = false;
            short num_registros_msg = 0;
            string ls_ret = "";
            string ls_sql = "";
            string ls_retorno_venta = "";
            string ls_mensaje = "";
            string lc_rut = "";
            short i = 0;
            string lc_rut2 = "";
            string lc_rut3 = "";

            string s = initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO +
                VB6Helpers.String(12 - VB6Helpers.Len(initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO), 126);

            lc_rut2 = initObj.Frm_Crea_Participante.rut.Text.Replace("-", "");
            lc_rut2 = lc_rut2.Replace("~", "|");
            lc_rut3 = Mdl_Funciones_Varias.llena_ceros("0", (10 - VB6Helpers.Len(lc_rut2))) + lc_rut2;

            try
            {
                string lc_retorno, lc_mensaje;
                string ret = uow.SceRepository.pro_sce_prty_i06_MS(s, false, 0, 0, 0, false, lc_rut3, initObj.MODGUSR.UsrEsp.CCtOrig,
                    initObj.MODGUSR.UsrEsp.EspOrig, false, "", "", "", "", 0, false, false, 0, "", 0, "", 0, 0, 0, 0,
                    0, 0, 0, 0, 0, initObj.Frm_Crea_Participante.Nombre.Text, "", 0,
                    initObj.Frm_Crea_Participante.Direccion.Text, initObj.Frm_Crea_Participante.comuna.Text,
                    initObj.Frm_Crea_Participante.Estado.Text, initObj.Frm_Crea_Participante.Ciudad.Text,
                    initObj.Frm_Crea_Participante.Pais.Text, "", "", "", 0, 0, initObj.Frm_Crea_Participante.Postal.Text,
                    "", "", "", DateTime.Now, initObj.Mdl_Funciones_Varias.Lc_BaseNumber, initObj.Mdl_Funciones_Varias.LC_MONEDA,
                    out lc_retorno, out lc_mensaje);

                //Captura de Mensaje de retorno
                Li_Largo1 = (short)VB6Helpers.Len(ls_ret);
                Li_Largo2 = 1;

                //llena datos
                ls_retorno_venta = lc_retorno;
                ls_mensaje = lc_mensaje;

                ok = (ls_retorno_venta == "E00");

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = "Ingreso Participante",
                    Type = TipoMensaje.Informacion,
                    Text = ls_mensaje
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al acceder a la base de datos", ex);
            }

            return ok;
        }

        //public void EsBanco_Click()
        //{
        //    short ls_valida = (short)EsBanco.Value;

        //    if (ls_valida != 0)
        //    {
        //        OldRut = Module1.FilCero((rut.ClipText), GPrt_RutMascara, 0);
        //        rut.Mask = GPrt_SwiftMask;
        //        rut.Text = Module1.DesCero(OldSwift, GPrt_SwiftMascara, -1);
        //        rut.Tag = 1;
        //        Label[0].Caption = CapSwift;
        //    }
        //    else
        //    {
        //        OldSwift = Module1.FilCero((rut.ClipText), GPrt_SwiftMascara, -1);
        //        rut.Mask = GPrt_RutMask;
        //        rut.Text = Module1.DesCero(VB6Helpers.Trim(OldRut), GPrt_RutMascara, 0);
        //        rut.Tag = 0;
        //        Label[0].Caption = CapRut;
        //    }
        //}

        ////valida el rut
        //private void Rut_LostFocus()
        //{

        //    if (rut.ClipText == "")
        //    {
        //        return;
        //    }

        //    if (VB6Helpers.Val(rut.Tag) == 0)
        //    {
        //        //alinear a la derecha
        //        rut.Text = Module1.DesCero((rut.ClipText), GPrt_RutMascara, 0);

        //        if (Module1.NoEsRut((rut.ClipText)) != 0)
        //        {
        //            VB6Helpers.MsgBox(Module1.GPrt_ErrRut, MsgBoxStyle.Information, Module1.GPrt_Caption);
        //            rut.SetFocus();
        //        }

        //    }
        //    else
        //    {
        //        //alinear a la izquierda
        //        rut.Text = Module1.DesCero((rut.ClipText), GPrt_SwiftMascara, -1);
        //    }
        //}

        //    if (Pais.get_ItemData((short)Pais.ListIndex) == nuestro_pais)
        //    {
        //        Telefono.Mask = GPrt_FonoMask;
        //        Fax.Mask = GPrt_FonoMask;
        //        Telefono.Text = GPrt_FonoMascara;
        //        Fax.Text = GPrt_FonoMascara;
        //    }
        //    else
        //    {
        //        Telefono.Mask = "";
        //        Fax.Mask = "";
        //        Telefono.Text = "";
        //        Fax.Text = "";
        //    }

        //    Aceptar.Enabled = true;
        //}

    }
}
