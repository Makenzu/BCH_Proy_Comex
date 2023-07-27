using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt07
    {
        //public List<UI_CheckBox> prtcliente { set; get; }
        //public List<UI_CheckBox> cliente { set; get; }


        //public UI_CheckBox prtcliente { set; get; }
        //public UI_CheckBox cliente { set; get; }
        public UI_TextBox prtrut { set; get; }
        public UI_TextBox oficina { set; get; }
        public UI_Combo cboOficina { get; set; }
        public UI_Combo ejecutivo { set; get; }
        public UI_Combo Combo2 { set; get; }
        public UI_Combo Combo4 { set; get; }
        public UI_Combo Combo1 { set; get; }

        public UI_Button aceptar { set; get; }
        public UI_Button cancelar { set; get; }

        //Comision
        public UI_Combo Cbo_CenCosImp { set; get; }
        public UI_Combo Cbo_EspecImp { set; get; }
        public UI_Button Bot_IngImp { set; get; }
        public UI_Button Bot_EliImp { set; get; }
        public UI_TextBox Txt_Imp { set; get; }

        //Exportacion 
        public UI_Combo Cbo_CenCosExp { set; get; }
        public UI_Combo Cbo_EspecExp { set; get; }
        public UI_Button Bot_IngExp { set; get; }
        public UI_Button Bot_EliExp { set; get; }
        public UI_TextBox Txt_Exp { set; get; }

        //Negocios 
        public UI_Combo Cbo_CenCosNeg { set; get; }
        public UI_Combo Cbo_EspecNeg { set; get; }
        public UI_Button Bot_IngNeg { set; get; }
        public UI_Button Bot_EliNeg { set; get; }
        public UI_TextBox Txt_Negocios { set; get; }
        public List<UI_OptionItem> prtcliente { get; set; }
        public UI_TextBox actividad { get; set; } //Hidden

        public int tipo { get; set; }
        public int modrut { get; set; }
        public string la_ofi { get; set; }

        public UI_Frame Fr_CliEsp { get; set; }
        public UI_PrtEnt07(bool iniciar)
        {
            //this.prtcliente = new List<UI_CheckBox>();
            //this.cliente = new List<UI_CheckBox>();
            //this.prtcliente = new UI_CheckBox();
            //this.cliente = new UI_CheckBox();
            this.prtrut = new UI_TextBox();
            this.oficina = new UI_TextBox();
            this.cboOficina = new UI_Combo();
            this.ejecutivo = new UI_Combo();
            this.Combo2 = new UI_Combo();
            this.Combo4 = new UI_Combo();
            this.Combo1 = new UI_Combo();
            this.aceptar = new UI_Button()
            {
                ID = "BtnAceptar",
                Text = "Aceptar"
            };
            this.cancelar = new UI_Button()
            {
                ID = "BtnCancelar",
                Text = "Cancelar"
            };
            //Comision
            this.Cbo_CenCosImp = new UI_Combo();
            this.Cbo_EspecImp = new UI_Combo();
            this.Bot_IngImp = new UI_Button();
            this.Bot_EliImp = new UI_Button();
            this.Txt_Imp = new UI_TextBox();
            //Exportacion
            this.Cbo_CenCosExp = new UI_Combo();
            this.Cbo_EspecExp = new UI_Combo();
            this.Bot_IngExp = new UI_Button();
            this.Bot_EliExp = new UI_Button();
            this.Txt_Exp = new UI_TextBox();
            //Negocios
            this.Cbo_CenCosNeg = new UI_Combo();
            this.Cbo_EspecNeg = new UI_Combo();
            this.Bot_IngNeg = new UI_Button();
            this.Bot_EliNeg = new UI_Button();
            this.Txt_Negocios = new UI_TextBox();
            this.prtcliente = new List<UI_OptionItem>();
            this.actividad = new UI_TextBox();

            tipo = new int();
            modrut = new int();
            la_ofi = string.Empty;
            Fr_CliEsp = new UI_Frame();

            if (iniciar)
            {
                inicializar();
            }
        }

        //se duplica porque al usar el toObject genera problema al agregar los elementos prtcliente, ya que agrega cada vez que se hace 2 valores mas
        public void inicializar()
        {
            this.aceptar = new UI_Button()
            {
                ID = "BtnAceptar",
                Text = "Aceptar"
            };
            this.cancelar = new UI_Button()
            {
                ID = "BtnCancelar",
                Text = "Cancelar"
            };
            prtcliente = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="0", Value="Cliente Comex", Selected=true },
                new UI_OptionItem { ID="1", Value="Cliente Banco" },
            };
            
        }

    }
}
