using System;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System.Collections.Generic;
using BCH.Comex.Data.DAL.Services;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public static class PRTYENT2
    {
        public static T_PRTYENT2 GetPRTYENT2()
        {
            return new T_PRTYENT2();
        }

        public static CliEsp[] VSGTCliEsp = null;
        public static CliEsp[] RSGTCliEsp = null;

        public static string nom_act(InitializationObject initObj, double cod)
        {
            string nom_act = "";

            int i = 0;

            for (i = 0; i <= initObj.PRTGLOB.acteco.GetUpperBound(0); i += 1)
            {
                if (initObj.PRTGLOB.acteco[i].codigo == cod)
                {
                    nom_act = initObj.PRTGLOB.acteco[i].nombre;
                    break;
                }
            }
            return nom_act;
        }

        public static string Minuscula2(string Pdato)
        {
            string[] Palabras = null;
            Palabras = new string[1];
            string Dato = UTILES.Componer(Pdato, "  ", " ");
            short i = 1;
            string s = UTILES.copiardestring(Dato, " ", i);
            short j = 0;
            while (s != "")
            {
                j = (short)(VB6Helpers.UBound(Palabras) + 1);
                VB6Helpers.RedimPreserve(ref Palabras, 0, j);
                string _switchVar1 = VB6Helpers.Trim(VB6Helpers.UCase(s));
                if (_switchVar1 == "S.A." || _switchVar1 == "M/E" || _switchVar1 == "M/N")
                {
                    Palabras[j] = VB6Helpers.UCase(s);
                }
                else if (_switchVar1 == "A" || _switchVar1 == "DE" || _switchVar1 == "Y" || _switchVar1 == "O" || _switchVar1 == "Y/O" || _switchVar1 == "U" || _switchVar1 == "AL" || _switchVar1 == "DE" || _switchVar1 == "LO" || _switchVar1 == "LA" || _switchVar1 == "EL" || _switchVar1 == "SI" || _switchVar1 == "NO" || _switchVar1 == "E" || _switchVar1 == "POR" || _switchVar1 == "OF")
                {
                    Palabras[j] = VB6Helpers.LCase(s);
                }
                else
                {
                    Palabras[j] = VB6Helpers.UCase(VB6Helpers.Mid(s, 1, 1)) + VB6Helpers.LCase(VB6Helpers.Mid(s, 2, VB6Helpers.Len(s) - 1));
                }

                i = (short)(i + 1);
                s = UTILES.copiardestring(Dato, " ", i);
            }
            s = "";
            for (i = 0; i <= (short)VB6Helpers.UBound(Palabras); i++)
            {
                s = s + Palabras[i] + " ";
            }
            return VB6Helpers.Trim(s);
        }

        public static int Lee_SgtCliEsp(InitializationObject initObj, UnitOfWorkCext01 uow, string rutcli)
        {
            int Lee_SgtCliEsp = 0;
            string rut = "";
            int fin = 0;
            int i = 0;
            int sig = 0;
            int h = 0;

            try
            {

                IList<ejc_prty_ejc_s_01_MS_Result> R = uow.SceRepository.ejc_prty_ejc_s_01_MS(rutcli);

                if (R == null || R.Count == 0)
                    return Lee_SgtCliEsp;



                foreach (ejc_prty_ejc_s_01_MS_Result result in R)
                {
                    if (VB6Helpers.Format(result.ejc_tpo, "00") == T_PRTYENT2.SGT_tipopimp || VB6Helpers.Format(result.ejc_tpo, "00") == T_PRTYENT2.SGT_tipopexp || VB6Helpers.Format(result.ejc_tpo, "00") == T_PRTYENT2.SGT_tipnegoc)
                    {
                        h = h + 1;
                        initObj.PRTYENT2.RSGTCliEsp[h] = new CliEsp();
                        initObj.PRTYENT2.RSGTCliEsp[h].nrut = result.prty_rut; //R.Mid((5 + sig), 9);
                        initObj.PRTYENT2.RSGTCliEsp[h].tipo = result.ejc_tpo; //R.Mid((14 + sig), 2);
                        initObj.PRTYENT2.RSGTCliEsp[h].ofieje = VB6Helpers.Format(result.ejc_ofi, "00"); //R.Mid((16 + sig), 3);
                        initObj.PRTYENT2.RSGTCliEsp[h].codeje = VB6Helpers.Format(result.ejc_cod, "00");//R.Mid((19 + sig), 3);
                        initObj.PRTYENT2.RSGTCliEsp[h].feccre = result.create_at.ToShortDateString();
                        initObj.PRTYENT2.RSGTCliEsp[h].rutope = string.Empty;
                        initObj.PRTYENT2.RSGTCliEsp[h].drutope = string.Empty;
                        initObj.PRTYENT2.RSGTCliEsp[h].filler = string.Empty;
                        initObj.PRTYENT2.RSGTCliEsp[h].borrar = 0;
                        initObj.PRTYENT2.VSGTCliEsp[h] = initObj.PRTYENT2.RSGTCliEsp[h];

                        
                    }
                }
                Lee_SgtCliEsp = 1;
                return Lee_SgtCliEsp;
            }
            catch (Exception exc)
            {

                Lee_SgtCliEsp = 0;
            }

            return Lee_SgtCliEsp;
        }

        public static int Es_Cliente(InitializationObject InitObject, string rutcli)
        {
            using (var tracer = new Tracer("EsCliente"))
            {
                int esCliente = 0;
                string respuestaServicio = string.Empty;

                try
                {
                    respuestaServicio = XGPYServices.ConsultaOficinaPorRut(rutcli);
                    if (respuestaServicio != null && respuestaServicio != string.Empty)
                    {
                        esCliente = 1;
                    }
                    else
                    {
                        // Si el servicio no se cayó pero no respondio nada, se lo avisamos al usuario
                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "El servicio no devolvió información, por lo que no se puede determinar si el cliente está registrado en las tablas generales, ni tampoco su información comercial.",
                            Title = "Administrador de Participantes ",
                            Type = TipoMensaje.Warning
                        });
                    }

                    return esCliente;
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta, no se pudo actulizar tabla SGT", _e);
                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = _e.Message,
                        Title = T_PRTGLOB.MB_ICONEXCLAMATION.ToString(),
                        Type = TipoMensaje.Error
                    });
                    esCliente = 0;
                }
                return esCliente;
            } 
        }
    }
}
