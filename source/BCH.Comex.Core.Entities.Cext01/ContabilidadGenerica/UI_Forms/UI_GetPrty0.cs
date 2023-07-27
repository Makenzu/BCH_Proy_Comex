using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_GetPrty0: UI_Frm
    {
        public int EnLoad = 0;
        public const int Gen_Imp = 1;     // 2^0
        public const int Gen_Exp = 2;     // 2^1
        public const int Gen_Ser = 4;     // 2^2
        public const int Cob_Imp = 8;     // 2^3
        public const int Cob_Exp = 16;     // 2^4
        public const int Cre_Imp = 32;     // 2^5
        public const int Cre_Exp = 64;     // 2^6
                                            // mascara rut y fono
        public const string GPrt_RutMascara = "___.___.___-_";
        public const string GPrt_FonoMascara = "(___) ___-____";
        public const string GPrt_SwiftMascara = "____________";
        public const string GPrt_RutMask = "###\\.###\\.###\\-A";
        public const string GPrt_FonoMask = "(###) ###-####";
        public const string GPrt_SwiftMask = "&&&&&&&&&&&&";
        public const string GPrt_OtroPais = "(Otro pais) ";


        public UI_TextBox _Ins_6;
        public UI_TextBox _Ins_5;
        public UI_TextBox _Ins_4;
        public UI_TextBox _Ins_3;
        public UI_TextBox _Ins_2;
        public UI_TextBox _Ins_1;
        public UI_TextBox _Ins_0;
        public UI_Frame Frame1;
        public UI_Frame Frame3;
        public UI_TextBox Tx_Dir;
        public UI_Button Instrucciones;
        public UI_TextBox Llave;
        public UI_Button Eliminar;
        public UI_Frame Frame2;
        public List<UI_CheckBox> Donde;
        public UI_CheckBox _Donde_1;
        public UI_CheckBox _Donde_0;
        public UI_Button Identificar;
        public UI_Button Bot_Nem;
        public UI_Label Label2;
        public UI_Button Cancelar;
        public UI_Button Aceptar;
        public UI_ListBox LstPartys;
        public UI_Label Label1;

        public int PopeInd { get; set; }
        public int Lim { get; set; }
        public int PopeBase { get; set; }
        public bool AbrirIdentParticipantes { get; set; }
        public string Redireccionar { get; set; }
        public bool MuestraSceInputBox { get; set; }
        public string CAPTION { get; set; }
        public string DESCRIPTION { get; set; }
        public bool MODO { get; set; }
        public bool LOADED { get; set; }
        public int BORRADO { get; set; }
        public int QUETIPO { get; set; }
        public int FLAG { get; set; }
        public double TIENERUT { get; set; }
        public string RUT { get; set; }
        public string BCO { get; set; }
        public string SWIFT { get; set; }
        public string OPE { get; set; }

        public UI_GetPrty0()
        {
            _Ins_6 = new UI_TextBox();
            _Ins_5 = new UI_TextBox();
            _Ins_4 = new UI_TextBox();
            _Ins_3 = new UI_TextBox();
            _Ins_2 = new UI_TextBox();
            _Ins_1 = new UI_TextBox();
            _Ins_0 = new UI_TextBox();
            Frame1 = new UI_Frame();
            Frame3 = new UI_Frame();
            Tx_Dir = new UI_TextBox() { ID = "Tx_Dir" };
            Instrucciones = new UI_Button() { Text="Instrucciones", ID="Instrucciones" };
            Llave = new UI_TextBox();
            Eliminar = new UI_Button() { Text="Eliminar", ID="Eliminar",Enabled=false };
            Frame2 = new UI_Frame();
            Donde = new List<UI_CheckBox>();
            _Donde_1 = new UI_CheckBox() { ID="donde1", Checked=false };
            _Donde_0 = new UI_CheckBox() { ID = "donde2", Checked = true };
            Donde.Add(_Donde_0);
            Donde.Add(_Donde_1);
            Identificar = new UI_Button() { Text="Identificar" };
            Bot_Nem = new UI_Button();
            Label2 = new UI_Label() { Text="Llave de Identificación" };
            Cancelar = new UI_Button() { Text="Cancelar"};
            Aceptar = new UI_Button() { Text="Aceptar", Enabled= false };
            LstPartys = new UI_ListBox();
            Label1 = new UI_Label() { Text="Participantes" };
        }
    }
}
