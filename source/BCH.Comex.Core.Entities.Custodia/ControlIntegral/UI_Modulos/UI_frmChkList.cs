using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Custodia.ControlIntegral.UI_Modulos
{
    public class UI_frmChkList
    {
        public List<UI_OptionItem> _opCtrl1 { get; set; }
        public List<UI_OptionItem> _opCtrl2 { get; set; }
        public List<UI_OptionItem> _opCtrl3 { get; set; }
        public List<UI_OptionItem> _opCtrl4 { get; set; }
        public List<UI_OptionItem> _opCtrl5 { get; set; }
        public List<UI_OptionItem> _opCtrl6 { get; set; }
        public List<UI_OptionItem> _opCtrl7 { get; set; }
        public List<UI_OptionItem> _opCtrl8 { get; set; }
        public UI_frmChkList()
        {

            this._opCtrl1 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="0", Value="OK", Tag="lblCtrl1Ok", Selected=false },
                new UI_OptionItem { ID="1", Value="REPARO", Tag="lblCtrl1Reparo", Selected=false },
            };

            this._opCtrl2 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="OK", Tag="lblCtrl2Ok",Selected = false},
                new UI_OptionItem { ID= "1", Value="REPARO", Tag="lblCtrl2Reparo", Selected=false},
                new UI_OptionItem { ID= "2", Value="CORREGIDA",Tag="lblCtrl2Corregida", Selected=false}
             };

            this._opCtrl3 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="OK", Tag="lblCtrl3Ok",Selected = false},
                new UI_OptionItem { ID= "1", Value="REPARO",Tag="lblCtrl3Reparo"},
                new UI_OptionItem { ID= "2", Value="NO REQUIERE", Tag="lblCtrl3NoRequiere"}
             };

            this._opCtrl4 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="SI",Tag="lblCtrl4Si", Selected = false},
                new UI_OptionItem { ID= "1", Value="REPARO",Tag="lblCtrl4Reparo"},
                new UI_OptionItem { ID= "2", Value="NO REQUIERE",Tag="lblCtrl4NoRequiere"}
             };

            this._opCtrl5 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="OK",Tag="lblCtrl5Ok",Selected = false},
                new UI_OptionItem { ID= "1", Value="DUPLICADA",Tag="lblCtrl5Duplicada"}             
             };
            this._opCtrl6 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="OK",Tag="lblCtrl6Ok",Selected = false},
                new UI_OptionItem { ID= "1", Value="REPARO",Tag="lblCtrl6Reparo"},
                new UI_OptionItem { ID= "2", Value="NO REQUIERE",Tag="lblCtrl6NoRequiere"}
             };
            this._opCtrl7 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="OK",Tag="lblCtrl7Ok",Selected = false},
                new UI_OptionItem { ID= "1", Value="REPARO",Tag="lblCtrl7Reparo"},
                new UI_OptionItem { ID= "2", Value="NO REQUIERE",Tag="lblCtrl7NoRequiere"}
             };
            this._opCtrl8 = new List<UI_OptionItem>() {
                new UI_OptionItem { ID= "0", Value="OK",Tag="lblCtrl8Ok",Selected = false},
                new UI_OptionItem { ID= "1", Value="REPARO",Tag="lblCtrl8Reparo"},
                new UI_OptionItem { ID= "2", Value="NO REQUIERE",Tag="lblCtrl8NoRequiere"}
             };

        }
    }
}
