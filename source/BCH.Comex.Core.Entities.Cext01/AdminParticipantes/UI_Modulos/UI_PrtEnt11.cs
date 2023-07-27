using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt11
    {
        public UI_TextBox prtcodigo;
        public UI_TextBox prtswif;

        public UI_CheckBox prtaladi;

        public UI_TextBox prtplaza;

        public UI_TextBox ejecorr;

        //Tipo Banco
        public List<UI_CheckBox> _prttipob_;
        // public List<UI_OptionItem_> _prttipob_;

        //Tasa de Rifanciamiento
        public List<UI_OptionItem> _prttasa_ { get; set; }

        public UI_TextBox prtspread;

        public UI_Combo Combo1;

        public UI_Button aceptar;
        public UI_Button cancelar;
        public UI_TextBox prtrut;
        public UI_PrtEnt11()
        {
            prtcodigo = new UI_TextBox();
            prtswif = new UI_TextBox();
            prtaladi = new UI_CheckBox();
            //prtaladi = new UI_CheckBox_(
            //   new UI_CheckBox_ { ID = "0", Tag = "Acreedor", Value = 0, Checked = true }

            //    );


            prtplaza = new UI_TextBox();
            ejecorr = new UI_TextBox();

            _prttipob_ = new List<UI_CheckBox>(){
                new UI_CheckBox { ID="0", Tag="Acreedor", Value= 0, Checked=true },
                new UI_CheckBox { ID="1", Tag="Corresponsal", Value = 1 },
                new UI_CheckBox { ID="2", Tag="Avisador", Value = 2 }
            };
            //_prttipob_ = new List<UI_CheckBox_>();
            //for (var i = 0; i <= 2; i++)
            //{
            //    _prttipob_.Add(new UI_CheckBox_());
            //}
            _prttasa_ = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="0", Value="Libor", Selected=true },
                new UI_OptionItem { ID="1", Value="Prime" },
                new UI_OptionItem { ID="2", Value="Nominal" }
            };

            prtspread = new UI_TextBox();
            Combo1 = new UI_Combo();
            aceptar = new UI_Button();
            cancelar = new UI_Button();
            prtrut = new UI_TextBox();

        }


    }
}
