using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public class FrmOfi
    {
        public static void Aceptar_Click(DatosGlobales Globales)
        {
            if (Globales.Frm_SeleccionOficina.Cb_Oficina.ListIndex == -1)
            {
                Globales.MESSAGES.Add(new Comex.Common.UI_Modulos.UI_Message() { Text = "Debe seleccionar una oficina antes de continuar", Type = Comex.Common.UI_Modulos.TipoMensaje.Informacion, ControlName = "Cb_Oficina_SelectedValue" });
            }
            else
            {
                Globales.SYGETPRT.Codop.Id_Empresa = VB6Helpers.Format(VB6Helpers.CStr(Globales.Frm_SeleccionOficina.Cb_Oficina.get_ItemData_((short)Globales.Frm_SeleccionOficina.Cb_Oficina.ListIndex)), "000");
            }
        }

        public static void Cancelar_Click(DatosGlobales Globales)
        {
            Globales.SYGETPRT.Codop.Id_Empresa = string.Empty;
        }

        public static void Form_Load(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            string Cant_Sucursales;
            Cant_Sucursales = Globales.MODGUSR.UsrEsp.OfixUser;
           
            Globales.Frm_SeleccionOficina.Oficinas = Globales.MODGUSR.UsrEsp.OfixUser;
            Llenasucur(ref Globales.Frm_SeleccionOficina.Oficinas, Globales);

            if (~SyGet_Suc_Dos(Cant_Sucursales, unit, Globales) != 0)
            {
                return;
            }
            else
            {
                LlenaLista(Globales.Frm_SeleccionOficina.Cb_Oficina, Globales);
            }

            Globales.Frm_SeleccionOficina.Cb_Oficina.ListIndex = 0;
        }

        private static void LlenaLista(UI_Combo Cb, DatosGlobales Globales)
        {
            Cb.Items = Globales.Frm_SeleccionOficina.VjSuc.Select(x => new UI_ComboItem
            {
                Value = x.nomsuc,
                Data = x.codsuc,
            }).ToList();

        }

        private static void Llenasucur(ref string sucur, DatosGlobales Globales)
        {
            short pos = -1;
            short arr = 0;

            Globales.Frm_SeleccionOficina.nomsuc = new string[1];
            sucur = VB6Helpers.Trim(sucur);
            while (!(pos == 0))
            {

                pos = (short)VB6Helpers.Instr(sucur, ";");
                if (pos > 0)
                {
                    arr = (short)(VB6Helpers.UBound(Globales.Frm_SeleccionOficina.nomsuc) + 1);
                    VB6Helpers.RedimPreserve(ref Globales.Frm_SeleccionOficina.nomsuc, 0, arr);
                    Globales.Frm_SeleccionOficina.nomsuc[arr] = VB6Helpers.Trim(VB6Helpers.Mid(sucur, 1, pos - 1));
                    sucur = VB6Helpers.Mid(sucur, pos + 1);
                }
                else
                {
                    arr = (short)(VB6Helpers.UBound(Globales.Frm_SeleccionOficina.nomsuc) + 1);
                    VB6Helpers.RedimPreserve(ref Globales.Frm_SeleccionOficina.nomsuc, 0, arr);
                    Globales.Frm_SeleccionOficina.nomsuc[arr] = VB6Helpers.Trim(sucur);
                }

            }

        }

        private static short SyGet_Suc(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            short i = 0;

            var Fin = Globales.Frm_SeleccionOficina.nomsuc.Length;
            if (Fin >= 1)
            {
                for (i = 0; i < Fin; i++)
                {
                    if (~EnVjSuc(Globales.Frm_SeleccionOficina.nomsuc[i], Globales) != 0)
                    {
                        try
                        {
                            var result = unit.SceRepository.sgt_suc_s02_MS(short.Parse(Globales.Frm_SeleccionOficina.nomsuc[i]));
                            if (result.Count == 0)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "No se han encontrado oficinas.",
                                    AutoClose = true
                                });

                                return (short)(false ? -1 : 0);
                            }
                            var item = new T_Suc { codsuc = short.Parse(Globales.Frm_SeleccionOficina.nomsuc[i]), nomsuc = result[0] };
                        }
                        catch
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Ha ocurrido un error al traer sucursales del especialista.",
                                AutoClose = true
                            });
                            return (short)(false ? -1 : 0);
                        }
                    }
                }
            }
            else
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No existen oficinas registradas.",
                    AutoClose = true
                });

                return (short)(false ? -1 : 0);
            }

            _retValue = (short)(true ? -1 : 0);

            return _retValue;

        }

        private static short SyGet_Suc_Dos(string Sucursales, UnitOfWorkCext01 unit, DatosGlobales Globales)
        {
            short _retValue = 0;
            string ls_oficina = "";

            if (!string.IsNullOrEmpty(Sucursales))
            {
                //Debe reemplazar los ; por ,
                ls_oficina = Sucursales.Replace(";", ",");

                List<pro_sce_suc_s01_MS_Result> RESULTADO = null;
                try
                {
                    RESULTADO = unit.SceRepository.pro_sce_suc_s01_MS(MODGSYB.dbcharSy(ls_oficina));//MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 11, 5)));
                }
                catch
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al leer los reportes contables."
                    });

                    return (short)(false ? -1 : 0);
                }

                if (RESULTADO == null)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "No se han encontrado oficinas."
                    });

                    return (short)(false ? -1 : 0);
                }
                else
                {
                    foreach (var item in RESULTADO)
                    {
                        Globales.Frm_SeleccionOficina.VjSuc.Add(new T_Suc
                        {
                            codsuc = (short)item.succod,
                            nomsuc = item.sucnom
                        });
                    }
                }
            }
            else
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No existen oficinas registradas."
                });

                return (short)(false ? -1 : 0);
            }

            _retValue = (short)(true ? -1 : 0);

            return _retValue;
        }

        private static short EnVjSuc(string codsuc, DatosGlobales Globales)
        {
            short _retValue = 0;

            short i = 0;
            short Fin = 0;

            Fin = (short)VB6Helpers.UBound(Globales.Frm_SeleccionOficina.VjSuc);

            _retValue = (short)(false ? -1 : 0);

            if (Fin >= 1)
            {
                for (i = 1; i <= (short)Fin; i++)
                {
                    if (Globales.Frm_SeleccionOficina.VjSuc[i].codsuc == VB6Helpers.Val(codsuc))
                    {
                        _retValue = (short)(true ? -1 : 0);
                        break;
                    }

                }

            }

            return _retValue;
        }

    }
}
