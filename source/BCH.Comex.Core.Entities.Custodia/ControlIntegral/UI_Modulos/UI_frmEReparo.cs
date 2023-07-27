using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Custodia.ControlIntegral.UI_Modulos
{
    public class UI_frmEReparo
    {


        public UI_Combo cmbDocName { get; set; }

        public UI_TextBox txtNomCli { get; set; }
        public UI_TextBox txtMailEjec { get; set; }

        public UI_Label Frame1 { get; set; }
        public UI_Label Frame2 { get; set; }
        public UI_Label Frame3 { get; set; }
        public UI_Label Frame4 { get; set; }
        public UI_Label Frame5 { get; set; }
        public List<UI_CheckBox> chkReparo { get; set; }
        public List<UI_CheckBox> chkOtros { get; set; }

        public List<UI_TextBox> txtOtros { get; set; }

        public UI_frmEReparo()
        {

            this.cmbDocName = new UI_Combo();
            this.txtNomCli = new UI_TextBox();
            this.txtMailEjec = new UI_TextBox();
            this.Frame1 = new UI_Label();
            this.Frame2 = new UI_Label();
            this.Frame3 = new UI_Label();
            this.Frame4 = new UI_Label();
            this.Frame5 = new UI_Label();

            this.chkReparo = new List<UI_CheckBox>();
            for (int i = 0; i <= 19; i++)
            {
                chkReparo.Add(new UI_CheckBox());
            }

            this.chkOtros = new List<UI_CheckBox>();
            for (int i = 0; i <= 4; i++)
            {
                chkOtros.Add(new UI_CheckBox());
            }

            this.txtOtros = new List<UI_TextBox>();
            for (int i = 0; i <= 4; i++)
            {
                txtOtros.Add(new UI_TextBox());
            }


        }



    }
}
