//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using CodeArchitects.VB6Library;
using System;
using System.Text;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class MODPRTY
    {
        public static int SyGet_Ofi(InitializationObject initObj, string cta, int Flag)
        {
            int SyGet_Ofi = 0;

            string Bco = "";
            string PathExes = "";
            string ofici = "";
            int bien = 0;
            string Existencia = "";
            string oficina = "";
            int lar_exis = 0;
            int lar_ofi = 0;
            int lar_bco = 0;
            string Retorno = "";
            string param = "";
            string Flag_Validez = "";
            string Nodo = "";
            int lar_retorno = 0;
            int lar_param = 0;
            int lar_nodo = 0;
            int lar_flagvalidez = 0;
            string Origen = "";
            double t_pausa = 0.0;

            try
            {

                Existencia = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);
                oficina = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);
                Retorno = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);
                param = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);
                Nodo = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);
                Flag_Validez = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);
                Origen = new string(Convert.ToChar(VB6Helpers.Chr(0)), 255);

                SyGet_Ofi = 0;

                PathExes = UTILES.GetUbicacion("PathExes");

                if (Flag == 1)
                {
                    //bien = MigrationSupport.Utils.Shell(PathExes + "v_ctacte CEX" + cta, MigrationSupport.Utils.AppWinStyle.NormalFocus);
                }
                else
                {
                    //bien = MigrationSupport.Utils.Shell(PathExes + "v_ctacte CTD" + cta, MigrationSupport.Utils.AppWinStyle.NormalFocus);
                }
                t_pausa = 1;
                while (t_pausa < 100000)
                {
                    t_pausa = t_pausa + 1;
                    System.Windows.Forms.Application.DoEvents();
                }

                StringBuilder Retorno2 = new StringBuilder();
                Retorno2.Append(Retorno);

                //lar_retorno = UTILES.GetPrivateProfileString("Datos", "Retorno_Srm", "", Retorno2, Retorno.Length, "c:\\data\\v_ctacte\\v_ctacte.ini");
                //if (lar_retorno != 0)
                //{
                //    Retorno = Retorno.Substring(0, lar_retorno);
                //}

                if (lar_retorno == 0 || Retorno.Substring(1, 2) != "00")
                {
                    //System.Windows.Forms.MessageBox.Show("Error de retorno cuenta corriente", "", MessageBoxButtons.OK);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Error de retorno cuenta corriente",
                        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                        Type = TipoMensaje.Informacion
                    });
                    return SyGet_Ofi;
                }


                StringBuilder param2 = new StringBuilder();
                param2.Append(param);

                //lar_param = UTILES.GetPrivateProfileString("Datos", "Parametros", "", param2, param.Length, "c:\\data\\v_ctacte\\v_ctacte.ini");
                //if (lar_param != 0)
                //{
                //    param = param.Substring(0, lar_param);
                //}

                if (Flag == 1)
                {
                    if (lar_param == 0 || param.Trim() != "CEX" + cta.Trim())
                    {
                        //System.Windows.Forms.MessageBox.Show("Error lo enviado no es lo mismo que quedó en la llave Parametros del archivo v_ctacte.ini " + Flag.ToStr(), "", MessageBoxButtons.OK);
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Error lo enviado no es lo mismo que quedó en la llave Parametros del archivo v_ctacte.ini " + Flag.ToString(),
                            Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                            Type = TipoMensaje.Informacion
                        });
                        return SyGet_Ofi;
                    }
                }
                else
                {
                    if (lar_param == 0 || param.Trim() != "CTD" + cta.Trim())
                    {
                        //System.Windows.Forms.MessageBox.Show("Error lo enviado no es lo mismo que quedó en la llave Parametros del archivo v_ctacte.ini" + Flag.ToStr(), "", MessageBoxButtons.OK);
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Error lo enviado no es lo mismo que quedó en la llave Parametros del archivo v_ctacte.ini " + Flag.ToString(),
                            Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                            Type = TipoMensaje.Informacion
                        });

                        return SyGet_Ofi;
                    }
                }

                StringBuilder Flag_Validez2 = new StringBuilder();
                Flag_Validez2.Append(Flag_Validez);

                //lar_flagvalidez = UTILES.GetPrivateProfileString("Datos", "Flag_Validez", "", Flag_Validez2, Flag_Validez.Length, "c:\\data\\v_ctacte\\v_ctacte.ini");
                if (lar_flagvalidez == 0)
                {
                    //System.Windows.Forms.MessageBox.Show("Error Flag_Validez del archivo v_ctacte.ini debe traer un valor TRUE o FALSE", "", MessageBoxButtons.OK);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Error Flag_Validez del archivo v_ctacte.ini debe traer un valor TRUE o FALSE",
                        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                        Type = TipoMensaje.Informacion
                    });

                    return SyGet_Ofi;
                }
                else
                {
                    Flag_Validez = Flag_Validez.Substring(0, lar_flagvalidez);
                    if (Flag_Validez.ToUpper() != "TRUE")
                    {
                        //System.Windows.Forms.MessageBox.Show("Error Flag_Validez del archivo v_ctacte.ini debe traer un valor TRUE ", "", MessageBoxButtons.OK);
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Error Flag_Validez del archivo v_ctacte.ini debe traer un valor TRUE o FALSE",
                            Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                            Type = TipoMensaje.Informacion
                        });
                        return SyGet_Ofi;
                    }
                }

                StringBuilder Existencia2 = new StringBuilder();
                Existencia2.Append(Existencia);
                //lar_exis = UTILES.GetPrivateProfileString("Datos", "Flag_Existencia", "", Existencia2, Existencia.Length, "c:\\data\\v_ctacte\\v_ctacte.ini ");
                //if (lar_exis == 0)
                //{
                //    Existencia = "";
                //}
                //else
                //{
                //    Existencia = Existencia.Substring(0, lar_exis);
                //}

                StringBuilder oficina2 = new StringBuilder();
                oficina2.Append(oficina);

                //lar_ofi = UTILES.GetPrivateProfileString("Datos", "Oficina", "", oficina2, oficina.Length, "c:\\data\\v_ctacte\\v_ctacte.ini");
                //if (lar_ofi == 0)
                //{
                //    //System.Windows.Forms.MessageBox.Show("Error Oficina  no encontrada.", "", MessageBoxButtons.OK);
                //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                //    {
                //        Text = " Error Oficina  no encontrada.",
                //        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                //        Type = TipoMensaje.Informacion
                //    });
                //    return SyGet_Ofi;
                //}
                //else
                //{
                //    oficina = oficina.Substring(0, lar_ofi);
                //}

                if (Existencia.ToLower().Trim() == "true")
                {
                    ofici = oficina;
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(" Cuenta Inexistente  cuenta : " + cta + " Flag%Existencia : " + Existencia + " Oficina : " + oficina, "", MessageBoxButtons.OK);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Cuenta Inexistente  cuenta : " + cta + " Flag%Existencia : " + Existencia + " Oficina : " + oficina + ".",
                        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                        Type = TipoMensaje.Informacion
                    });
                    return SyGet_Ofi;
                }

                StringBuilder Nodo2 = new StringBuilder();
                Nodo2.Append(Nodo);

                //lar_nodo = UTILES.GetPrivateProfileString("Datos", "Nodo", "", Nodo2, Nodo.Length, "c:\\data\\v_ctacte\\v_ctacte.ini");
                //if (lar_nodo == 0 || Nodo == "")
                //{
                //    //System.Windows.Forms.MessageBox.Show("Error Nodo no encontrado.", "", MessageBoxButtons.OK);
                //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                //    {
                //        Text = " Error Nodo no encontrado.",
                //        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                //        Type = TipoMensaje.Informacion
                //    });
                //    return SyGet_Ofi;
                //}
                //else
                //{
                //    Nodo = Nodo.Substring(0, lar_nodo);
                //}

                if (Nodo == "-1" || Nodo == "")
                {
                    //MigrationSupport.Utils.MsgBox("No se pudo encontrar el Nodo Destino de la oficina " + ofici + " para las Transacciones de Cuenta Corriente en Línea. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", UTILES.pito(48).Cast<
                    //   MigrationSupport.MsgBoxStyle>(), "Cuenta Corriente en Línea ");

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " No se pudo encontrar el Nodo Destino de la oficina " + ofici + " para las Transacciones de Cuenta Corriente en Línea. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",
                        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                        Type = TipoMensaje.Informacion
                    });
                    return SyGet_Ofi;
                }

                StringBuilder Origen2 = new StringBuilder();
                Origen2.Append(Origen);

                //lar_bco = UTILES.GetPrivateProfileString("Datos", "Origen", "", Origen2, Origen.Length, "c:\\data\\v_ctacte\\v_ctacte.ini");
                //if (lar_bco == 0)
                //{
                //    Bco = "";
                //}
                //else
                //{
                //    Bco = Origen.Substring(0, lar_bco);
                //}

                //if (Bco == "BAE")
                //{
                //    SyGet_Ofi = 1;
                //}
                //else if (Bco == "BCH" || Bco == "CTB")
                //{
                //    SyGet_Ofi = 2;
                //}
                //else
                //{
                //    SyGet_Ofi = 3;
                //}

            }
            catch (Exception exc)
            {
            
            }

            return SyGet_Ofi;
        }

    }
}
