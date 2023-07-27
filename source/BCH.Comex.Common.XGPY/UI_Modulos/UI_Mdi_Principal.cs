using System;
using System.Collections.Generic;

namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_Mdi_Principal
    {
        public List<UI_Button_> Archivo;
        public List<UI_Button_> Opciones;
        public Dictionary<string, UI_Button_> BUTTONS { set; get; }
        public List<UI_Message> MESSAGES { set; get; }

        public UI_Mdi_Principal()
        {
            #region "ARCHIVO"
            Archivo = new List<UI_Button_>{
                new UI_Button_ {                   
                    ID = "mnuNuevoParticipante",
                    Tag = "#",
                    Enabled = true,
                    Text = "Nuevo Participante",
                    Icon = "fa fa-user-plus"
                },               
                new UI_Button_{
                    ID = "mnuAbrirParticipante",
                    Tag = "#",
                    Enabled = true,
                    Text = "Abrir Participante",
                    Icon = "fa fa-user"
                },
                new UI_Button_{
                    ID = "mnuSalvarParticipante",
                    Tag = "#",
                    Enabled = true,
                    Text = "Salvar Participante",
                    Icon = "fa fa-floppy-o"
                },
                new UI_Button_ {
                    Tag = "separator"
                },
                new UI_Button_{
                    ID="mnuRecuperarParticipante",
                    Tag = "#",
                    Enabled = true,
                    Text = "Recuperar Participante desde Servidor",
                    Icon = "fa fa-download"
                },
                new UI_Button_{
                    ID="mnuEliminarParticipante",
                    Tag = "#",
                    Enabled = true,
                    Text = "Eliminar Participante",
                    Icon = "fa fa-user-times"
                },
                new UI_Button_ {
                    Tag = "separator"
                },
                new UI_Button_{
                    ID="mnuSalirParticipante",
                    Tag = "~/",
                    Enabled = true,
                    Text = "Salir",
                    Icon = "fa fa-home"
                },
        };
            #endregion

            #region MENSAJES
            MESSAGES = new List<UI_Message>();
            #endregion

            #region OPCIONES
            Opciones = new List<UI_Button_>{
                new UI_Button_{
                    //ID="mnu_party",
                    ID="OpcionCaracteristica",
                    Tag = "~/AdminParticipantes/CaracteristicasParticipante",
                    Enabled = true,
                    Text = "Características",
                    Icon = "fa fa-file-text-o"
                },
                //new UI_Button_{
                //    ID="1",
                //    Tag = "separator",
                //    Enabled = true,
                //    Text = ""
                //},
                new UI_Button_{
                    ID="OpcionInstrucciones", //1
                    Tag = "~/AdminParticipantes/InstruccionesEspeciales",
                    Enabled = true,
                    Text = "Instrucciones Especiales",
                    Icon = "fa fa-pencil-square-o"
                },
                new UI_Button_{
                    ID="OpcionCuentas", //2
                    Tag = "~/AdminParticipantes/CuentasParticipante",
                    Enabled = true,
                    Text = "Cuentas",
                    Icon = "fa fa-list-alt"
                },
                new UI_Button_{
                    ID="OpcionTasas", //3
                    Tag = "~/AdminParticipantes/TasasEspecialesParticipante",
                    Enabled = true,
                    Text = "Tasas Especiales",
                    Icon = "fa fa-line-chart"
                },
                 new UI_Button_{
                    ID="mnuPrtyActivar",
                    Tag = "#",
                    Enabled = true,
                    Text = "Activar",
                    Icon = "fa fa-check-square-o"
                }
        };
            #endregion

            #region BOTONES

            this.BUTTONS = new Dictionary<string, UI_Button_>();
            this.BUTTONS.Add("tbr_nuevoParticipante", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_nuevoParticipante",
                Text = "Nuevo Participante",
                Tag = "~/AdminParticipantes/NuevoParticipante",
                ImgPath = "~/Content/images/ButtonsBarMnu/Nuevo.png"
            });
            this.BUTTONS.Add("tbr_AbrirParticipante", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_AbrirParticipante",
                Text = "Abrir Participante",
                Tag = "~/AdminParticipantes/AbrirParticipante",
                ImgPath = "~/Content/images/ButtonsBarMnu/AbrirConsulta.png"
            });
            this.BUTTONS.Add("tbr_Grabar", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_Grabar",
                Text = "Grabar",
                Tag = "~/",
                ImgPath = "~/Content/images/ButtonsBarMnu/Grabar.png"
            });
            this.BUTTONS.Add("tbr_Caracteristicas", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_Caracteristicas",
                Text = "Características",
                Tag = "~/AdminParticipantes/CaracteristicasParticipante",
                ImgPath = "~/Content/images/ButtonsBarMnu/Caracteristicas.png"
            });
            this.BUTTONS.Add("tbr_Instrucciones", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_Instrucciones",
                Text = "Instrucciones Especiales",
                Tag = "~/AdminParticipantes/InstruccionesEspeciales",
                ImgPath = "~/Content/images/ButtonsBarMnu/Instrucciones.png"
            });
            this.BUTTONS.Add("tbr_Cuentas", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_Cuentas",
                Text = "Cuentas Participante",
                Tag = "~/AdminParticipantes/CuentasParticipante",
                ImgPath = "~/Content/images/ButtonsBarMnu/Cuentas.png"
            });
            this.BUTTONS.Add("tbr_Tasas", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_Tasas",
                Text = "Tasas Especiales",
                Tag = "~/AdminParticipantes/TasasEspecialesParticipante",
                ImgPath = "~/Content/images/ButtonsBarMnu/TasasEspeciales.png"
            });
            this.BUTTONS.Add("tbr_Activar", new UI_Button_()
            {
                Enabled = true,
                ID = "tbr_Activar",
                Text = "Activar",
                Tag = "~/AdminParticipantes/ActivarRazon",
                ImgPath = "~/Content/images/ButtonsBarMnu/Activar.png"
            });


            #endregion
        }

        //public static string InicializarObjeto()
        //{
        //    InitializationObject Initialization = new InitializationObject();

        //    return "~/";
            
        //}
    }

    public enum TipoMensaje
    {
        Nada = 0,
        Correcto = 1,
        Informacion = 2,
        Error = 3,
        Critical = 4,
        YesNo = 5,
        Warning = 6
    }

    public class UI_Message
    {
        public TipoMensaje Type { set; get; }
        public string Text { set; get; }
        public string Title { get; set; }
        public string ControlName { get; set; } //se agrega para hacer referencia a un control determinado
        public bool AutoClose { get; set; }

        public UI_Message()
        {
            this.Type = TipoMensaje.Nada;
            this.Text = String.Empty;
            this.Title = String.Empty;
        }

        public bool IsError
        {
            get
            {
                return (this.Type == TipoMensaje.Error || this.Type == TipoMensaje.Critical);
            }
        }

    }





}
