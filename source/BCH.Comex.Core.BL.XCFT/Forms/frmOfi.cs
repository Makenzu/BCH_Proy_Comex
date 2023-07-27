using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class frmOfi
    {

        public static void BAceptar_Click(InitializationObject initObj)
        {
            if ((short)initObj.Frm_SeleccionOficina.Cb_Oficina.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type= TipoMensaje.Informacion , Text = "Debe seleccionar una oficina antes de continuar", ControlName = "Cb_Oficina_SelectedValue" });
                return;
            }

            if (initObj.Frm_SeleccionOficina.opt_CVD.Selected)
            {
                initObj.MODGCVD.COMISION = false;
            }
            else
            {
                initObj.MODGCVD.COMISION = true;
            }

            initObj.Module1.Codop.Id_Empresa = VB6Helpers.Format(VB6Helpers.CStr(initObj.Frm_SeleccionOficina.Cb_Oficina.get_ItemData_((short)initObj.Frm_SeleccionOficina.Cb_Oficina.ListIndex)), "000");
            //SE ASIGNA LA OFICINA A TODOS LOS CODOP, YA QUE AL AGREGAR UN PARTICIPANTE, SE CLONA DESDE CODOP_FT O CODOP_CVD HACIA CODOP, Y SI NO SE ASIGNA, EL VALOR QUEDA EN NULO Y GENERA PROBLEMAS AL GRABAR
            initObj.Module1.Codop_FT.Id_Empresa = initObj.Module1.Codop.Id_Empresa;
            initObj.Module1.Codop_CVD.Id_Empresa = initObj.Module1.Codop.Id_Empresa;
            initObj.MODXANU.Habilita = true;

            //Se setea el focus en participante
            initObj.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObj.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_participantes" ? true:false) ; });
            //initObj.Mdi_Principal.BUTTONS["tbr_participantes"].Focus = true;
        }

        public static void BCancelar_Click(InitializationObject initObj)
        {
            initObj.Module1.Codop.Id_Empresa = "";
            initObj.MODXANU.Habilita = false;
            // mdi_Principal.DefInstance.NuevaCVD2("L");
        }

        private static short EnVjSuc(string codsuc, InitializationObject initObj)
        {
            short _retValue = 0;

            short i = 0;
            short Fin = 0;

            Fin = (short)VB6Helpers.UBound(initObj.Frm_SeleccionOficina.VjSuc);

            _retValue = (short)(false ? -1 : 0);

            if (Fin >= 1)
            {
                for (i = 1; i <= (short)Fin; i++)
                {
                    if (initObj.Frm_SeleccionOficina.VjSuc[i].codsuc == VB6Helpers.Val(codsuc))
                    {
                        _retValue = (short)(true ? -1 : 0);
                        break;
                    }

                }

            }

            return _retValue;
        }

        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            string Cant_Sucursales;
            Cant_Sucursales = initObj.MODGUSR.UsrEsp.OfixUser;
            initObj.Frm_SeleccionOficina.Oficinas = initObj.MODGUSR.UsrEsp.OfixUser;
            Llenasucur(ref initObj.Frm_SeleccionOficina.Oficinas, initObj);

            if (~SyGet_Suc_Dos(Cant_Sucursales, unit, initObj) != 0)
            {
                return;
            }
            else
            {
                LlenaLista(initObj.Frm_SeleccionOficina.Cb_Oficina, initObj);
                if (initObj.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Anu || initObj.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Rev || initObj.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_AnuVisI)
                {
                    initObj.Frm_SeleccionOficina.opt_Comision.Enabled = false;
                }
            }

            initObj.Frm_SeleccionOficina.Cb_Oficina.ListIndex = 0;
            initObj.Frm_SeleccionOficina.opt_CVD.Enabled = true;
            if (initObj.MODCARMAS.CARGA_MASIVA == true)
            {
                initObj.Frm_SeleccionOficina.opt_Comision.Enabled = false;
            }
        }

        private static void LlenaLista(UI_Combo Cb, InitializationObject initObj)
        {
            Cb.Items = initObj.Frm_SeleccionOficina.VjSuc.Select(x => new UI_ComboItem
            {
                Value = x.nomsuc,
                Data = x.codsuc,
            }).ToList();

        }

        private static void Llenasucur(ref string sucur, InitializationObject initObj)
        {
            short pos = -1;
            short arr = 0;

            initObj.Frm_SeleccionOficina.nomsuc = new string[1];
            sucur = VB6Helpers.Trim(sucur);
            while (!(pos == 0))
            {

                pos = (short)VB6Helpers.Instr(sucur, ";");
                if (pos > 0)
                {
                    arr = (short)(VB6Helpers.UBound(initObj.Frm_SeleccionOficina.nomsuc) + 1);
                    VB6Helpers.RedimPreserve(ref initObj.Frm_SeleccionOficina.nomsuc, 0, arr);
                    initObj.Frm_SeleccionOficina.nomsuc[arr] = VB6Helpers.Trim(VB6Helpers.Mid(sucur, 1, pos - 1));
                    sucur = VB6Helpers.Mid(sucur, pos + 1);
                }
                else
                {
                    arr = (short)(VB6Helpers.UBound(initObj.Frm_SeleccionOficina.nomsuc) + 1);
                    VB6Helpers.RedimPreserve(ref initObj.Frm_SeleccionOficina.nomsuc, 0, arr);
                    initObj.Frm_SeleccionOficina.nomsuc[arr] = VB6Helpers.Trim(sucur);
                }

            }

        }

        private static short SyGet_Suc(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            short i = 0;

		    var Fin = initObj.Frm_SeleccionOficina.nomsuc.Length;
		    if ( Fin >= 1 )
		    {
			    for (i = 0; i < Fin; i++)
			    {
                    if (~EnVjSuc(initObj.Frm_SeleccionOficina.nomsuc[i], initObj) != 0)
				    {
                        try
                        {
                            var result = unit.SceRepository.sgt_suc_s02_MS(short.Parse(initObj.Frm_SeleccionOficina.nomsuc[i]));
                            if (result.Count == 0)
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "No se han encontrado oficinas."
                                });

                                return (short)(false ? -1 : 0);
                            }
                            //fin2 = initObj.Frm_SeleccionOficina.VjSuc.Count;
                            //VB6Helpers.RedimPreserve(ref VjSuc, 0, fin2);
                            var item = new T_Suc { codsuc = short.Parse(initObj.Frm_SeleccionOficina.nomsuc[i]), nomsuc = result[0] };
                        
                            //VjSuc[fin2].codsuc = (short)VB6Helpers.Val(nomsuc[i]);
                            //VjSuc[fin2].nomsuc = VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R));
                        }
                        catch
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Ha ocurrido un error al traer sucursales del especialista."
                            });
                            return (short)(false ? -1 : 0);
                        }
                    }
                }
            }
            else
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No existen oficinas registradas."
                });

                return (short)(false ? -1 : 0);
            }

            _retValue = (short)(true ? -1 : 0);

            return _retValue;

		}   


        private static short SyGet_Suc_Dos(string Sucursales, UnitOfWorkCext01 unit, InitializationObject initObj)
        {
            short _retValue = 0;
            string ls_oficina = "";

            if(!string.IsNullOrEmpty(Sucursales))
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
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al leer los reportes contables."
                    });

                    return (short)(false ? -1 : 0);
                }

                if (RESULTADO == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
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
                        initObj.Frm_SeleccionOficina.VjSuc.Add(new T_Suc
                        {
                            codsuc = (short)item.succod,
                            nomsuc = item.sucnom
                        });
                    }
                }
            }
            else
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No existen oficinas registradas."
                });

                return (short)(false ? -1 : 0);
            }

            _retValue = (short)(true ? -1 : 0);

            return _retValue;
        }

    }
}
