using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class GetPrty3_Logic
    {
        public static void Form_Load(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            int i = 0;
            int X = 0;
            string KeyOper = "";

            GetPrty3._Label_0.Text = "Rut";
            
            // Recuperamos las llaves y parametros
            KeyOper = SYGETPRT.PrtControl.NumOpe.Cent_costo + "-";
            KeyOper = KeyOper + SYGETPRT.PrtControl.NumOpe.Id_Product + "-";
            KeyOper = KeyOper + SYGETPRT.PrtControl.NumOpe.Id_Especia + "-";
            KeyOper = KeyOper + SYGETPRT.PrtControl.NumOpe.Id_Empresa + "-";
            KeyOper = KeyOper + SYGETPRT.PrtControl.NumOpe.Id_Operacion;

            GetPrty3.Text = T_SYGETPRT.GPrt_Caption + " (" + KeyOper + ")";

            // cargamos la tabla de paises
            GetPrty3.Pais.Items.Clear();

            // CargaEnLista_Pai pais
            X = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGetn_Pai(Globales,unit);
            for (i = 1; i <= MODGTAB0.VPai.GetUpperBound(0); i += 1)
            {
                GetPrty3.Pais.Items.Add(new Common.UI_Modulos.UI_ComboItem()
                {
                    Value= MODGTAB0.VPai[i].Pai_PaiNom,
                    Data= MODGTAB0.VPai[i].Pai_PaiCod
                });
            }

            if (GetPrty3.Pais.Items.Count == 0)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text=UI_GetPrty3.GPrt_ErrTablaPais,
                    Title= T_SYGETPRT.GPrt_Caption,
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });
            }
            GetPrty3.Pais.Items.Add(new Common.UI_Modulos.UI_ComboItem()
            {
                Value = UI_GetPrty3.GPrt_OtroPais,
                Data = -1
            });
            // agregar a la lista el "Otro Pais"
            //Es necesaario setear el tag del rut
            GetPrty3.Rut.Tag = GetPrty3.EsBanco.Checked ? "1" : "0";
        }

        public static void EsBanco_Click(DatosGlobales Globales)
        {
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;
            if (GetPrty3.EsBanco.Checked == true) 
            {
                GetPrty3.EsBanco_Click_OldRut = SYGETPRT.FilCero(GetPrty3.Rut.Text, UI_GetPrty3.GPrt_RutMascara, false.ToInt());
                GetPrty3.Rut.Mask = UI_GetPrty3.GPrt_SwiftMask;
                GetPrty3.Rut.Text = SYGETPRT.DesCero(GetPrty3.EsBanco_Click_OldSwift, UI_GetPrty3.GPrt_SwiftMascara, true.ToInt());
                GetPrty3.Rut.Tag = "1";
                GetPrty3._Label_0.Text = UI_GetPrty3.CapSwift;
            }
            else
            {
                GetPrty3.EsBanco_Click_OldSwift = SYGETPRT.FilCero(GetPrty3.Rut.Text, UI_GetPrty3.GPrt_SwiftMascara, true.ToInt());
                GetPrty3.Rut.Mask = UI_GetPrty3.GPrt_RutMask;
                GetPrty3.Rut.Text = SYGETPRT.DesCero(GetPrty3.EsBanco_Click_OldRut.TrimB(), UI_GetPrty3.GPrt_RutMascara, false.ToInt());
                GetPrty3.Rut.Tag = "0";
                GetPrty3._Label_0.Text = UI_GetPrty3.CapRut;
            }
        }

        public static void Aceptar_Click(DatosGlobales Globales)
        {
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;
            string Txt = "";

            // RTO 2012/03/2012 INCIDENTE IR46139
            if (GetPrty3.Rut.Text == "")
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Text= T_SYGETPRT.GPrt_ErrRut,
                    Title= T_SYGETPRT.GPrt_Caption
                });
                return;
            }
            else
            {
                if (SYGETPRT.NoEsRut(GetPrty3.Rut.Text) != 0)
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = T_SYGETPRT.GPrt_ErrRut,
                        Title = T_SYGETPRT.GPrt_Caption
                    });
                    return;
                }
            }
            // RTO 2012/03/2012 INCIDENTE IR46139
            // 
            // 
            // validar ingreso minimo
            if (string.IsNullOrEmpty(GetPrty3.Nombre.Text))
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "Nombre o Razon Social" + T_SYGETPRT.GPrt_ErrRequerido,
                    Title = T_SYGETPRT.GPrt_Caption
                });
                return;
            }

            if (string.IsNullOrEmpty(GetPrty3.Direccion.Text) && GetPrty3._envio_0.Checked)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "Direccion" + T_SYGETPRT.GPrt_ErrRequerido,
                    Title = T_SYGETPRT.GPrt_Caption
                });
                return;
            }

            // If Pais.ListIndex = -1 Then
            //     txt$ = "Pais" + GPrt_ErrRequerido
            //     MsgBox txt$, Pito(48), GPrt_Caption
            //     Pais.SetFocus
            //     Exit Sub
            // End If
            // 
            // ok, marcar como ingreso ok
            GetPrty3.Aceptar.Tag = "0";
            GetPrty3.Aceptar.Enabled = false;
        }

        public static void Cancelar_Click(DatosGlobales Globales)
        {
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;
            GetPrty3.Aceptar.Tag = "1";
            GetPrty3.Aceptar.Enabled = false;
        }
    }
}
