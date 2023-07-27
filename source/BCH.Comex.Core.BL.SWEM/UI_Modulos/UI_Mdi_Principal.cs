using System.Collections.Generic;

namespace BCH.Comex.Core.BL.SWEM.UI_Modulos
{
    public class UI_Mdi_Principal
    {
        public List<UI_Button> Opciones;
        public List<UI_Button> Mensajes;
        public UI_Mdi_Principal()
        {
            Opciones = new List<UI_Button>{
                new UI_Button{
                    ID="1",
                    Tag = "~/ConsultaSwift/ConfigurarCasilla",
                    Enabled = false,
                    Text = "Configurar Casilla"
                },            
                new UI_Button{
                    ID="2",
                    Tag = "~/ConsultaSwift/ConfiguraImpresora",
                    Enabled = false,
                    Text = "Configurar Impresora"
                },
                new UI_Button{
                    ID="3",
                    Tag = "~/ConsultaSwift/PersonalizarMensaje",
                    Enabled = true,
                    Text = "Personalizar Mensaje"
                } ,
                 new UI_Button{
                    ID="4",
                    Tag = "separator",
                    Enabled = true,
                    Text = ""
                },
                 new UI_Button{
                    ID="5",
                    Tag = "~/",
                    Enabled = true,
                    Text = "Salir"
                }  
            };
            Mensajes = new List<UI_Button>{
                new UI_Button{
                    ID="1",
                    Tag = "~/ConsultaSwift/RecibirMensaje",
                    Enabled = true,
                    Text = "Recibir Mensajes"
                }  
            };


        }



    }
}
