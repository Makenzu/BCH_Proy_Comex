using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGLORI
    {
        public static int Traspasa_Contab(DatosGlobales Globales)
        {
            using(var tracer = new Tracer("Traspasa_Contab"))
            {
                T_MODGLORI MODGLORI = Globales.MODGLORI;
                T_MODGCON0 MODGCON0 = Globales.MODGCON0;

                int Traspasa_Contab = 0;

                string ccte = "";
                //  D.S.B.

                int fin = 0;
                int i = 0;
                int h = 0;
                try
                {
                    Traspasa_Contab = true.ToInt();

                    MODGLORI.VxVia = new T_xVia[1] { new T_xVia() };
                    MODGLORI.VxOri = new T_xOri[1] { new T_xOri() };

                    //  Impuesto al debito es siempre falso porque ya
                    //  ya esta incluido en monto de VMcds

                    MODGLORI.VgxOri.ImpDeb = false.ToShort();

                    //  Origenes de Fondo
                    fin = MODGCON0.VMcds.GetUpperBound(0);


                    for (i = 1; i <= fin; i += 1)
                    {

                        //  Cargos
                        if (MODGCON0.VMcds[i].cod_dh.TrimB() == "D")
                        {

                            h = MODGLORI.VxOri.GetUpperBound(0);
                            h = h + 1;
                            Array.Resize(ref MODGLORI.VxOri, h + 1);
                            MODGLORI.VxOri[h] = new T_xOri();
                            MODGLORI.VxOri[h].NumCta = MODGCON0.VMcds[i].numcta.ToInt();
                            MODGLORI.VxOri[h].NemCta = MODGCON0.VMcds[i].NemCta;
                            MODGLORI.VxOri[h].CodMon = MODGCON0.VMcds[i].CodMon.ToShort();
                            MODGLORI.VxOri[h].MtoTot = MODGCON0.VMcds[i].mtomcd;
                            MODGLORI.VxOri[h].NemMon = MODGCON0.VMcds[i].NemMon;
                            MODGLORI.VxOri[h].ctacte = MODGCON0.VMcds[i].numcct;
                            MODGLORI.VxOri[h].Status = 1;
                            MODGLORI.VxOri[h].cctlin = false.ToShort();

                            //  Abonos
                        }
                        else if (MODGCON0.VMcds[i].cod_dh.TrimB() == "H")
                        {

                            h = MODGLORI.VxVia.GetUpperBound(0);
                            h = h + 1;
                            Array.Resize(ref MODGLORI.VxVia, h + 1);
                            MODGLORI.VxVia[h] = new T_xVia();
                            MODGLORI.VxVia[h].numcta = MODGCON0.VMcds[i].numcta.ToInt();
                            MODGLORI.VxVia[h].NemCta = MODGCON0.VMcds[i].NemCta;
                            MODGLORI.VxVia[h].CodMon = MODGCON0.VMcds[i].CodMon;
                            MODGLORI.VxVia[h].MtoTot = MODGCON0.VMcds[i].mtomcd;
                            MODGLORI.VxVia[h].NemMon = MODGCON0.VMcds[i].NemMon;
                            MODGLORI.VxVia[h].ctacte = MODGCON0.VMcds[i].numcct;
                            MODGLORI.VxVia[h].Status = 1;
                            MODGLORI.VxVia[h].cctlin = false.ToInt();
                        }
                    }
                }
                catch (Exception e)
                {
                    Traspasa_Contab = 0;
                    tracer.AddToContext("Excepcion", e.Message);
                }

                return Traspasa_Contab;
            }
        }


        // ****************************************************************************
        //    1.  Función que genera un número asociado a una operación. Esto se realiza
        //        además con la suma del número del Centro Costo más un dígito que se
        //        calcula para entregar la generación de este número.
        // ****************************************************************************
        public static string Fn_Genera_Num(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGRNG MODGRNG = Globales.MODGRNG;
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            T_MODGUSR MODGUSR = Globales.MODGUSR;

            string Fn_Genera_Num = "";

            string num = "";

            string num_gen = "";
            string dv = "";
            double nro = 0.0;

            nro = BCH.Comex.Core.BL.XGGL.Modulos.MODGRNG.LeeSceRng(Globales,unit, T_MODGRNG.Rng_SconSu);

            if (nro == -1)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text= "Error al generar el número de partida.  Debe elegir otro tipo de movimiento u otra cuenta contable.",
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Title=MODGLORI.Titulo
                });
                return Fn_Genera_Num;
            }

            num = MigrationSupport.Utils.Format(nro, String.Empty);

            if (num.Len() > 3)
            {
                num = num.Right(3);
            }

            num_gen = MODGUSR.UsrEsp.CentroCosto + MigrationSupport.Utils.Format(num.ToVal(), "000");

            dv = "";
            dv = Fn_Calcula_Dig(num_gen.TrimB());

            Fn_Genera_Num = num_gen + dv;

            return Fn_Genera_Num;
        }

        // ****************************************************************************
        //    1.  Función que calcula el dígito de una cuenta enviado por medio
        //        de un parámetro.
        // ****************************************************************************
        public static string Fn_Calcula_Dig(string nums)
        {
            string Fn_Calcula_Dig = "";

            int i = 0;

            object suma = null;
            int digito = 0;
            int Resultado = 0;
            const string cuales = "765432";

            for (i = 1; i <= 6; i += 1)
            {
                suma = suma.ToDbl() + (nums.Mid(i, 1)).ToVal() * (cuales.Mid(i, 1)).ToVal();
            }


            Resultado = suma.ToInt() % 11;

            digito = 11 - Resultado;

            if (digito == 11)
            {
                Fn_Calcula_Dig = "00";
            }
            else
            {
                Fn_Calcula_Dig = MigrationSupport.Utils.Format(digito, "00");
            }

            return Fn_Calcula_Dig;
        }

        // ****************************************************************************
        //    1.  Función que Valida un Banco Aladi.
        // ****************************************************************************'
        public static int Fn_Valida_Aladi(DatosGlobales Globales, string num)
        {
            int Fn_Valida_Aladi = 0;

            string el_dv = "";
            int X = 0;
            int Sum = 0;
            int mul = 0;
            int i = 0;
            string dv = "";
            string el_num = "";

            Fn_Valida_Aladi = false.ToInt();
            if (num != "")
            {
                if (num.Len() != 15)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type=TipoMensaje.Error,
                        Text= "Número ingresado no es correcto.",
                    });
                    return Fn_Valida_Aladi;
                }
                el_num = num.Left(12);
                dv = num.Mid(13, 1);

                for (i = 1; i <= el_num.Len(); i += 1)
                {
                    if (i % 2 == 0)
                    {
                        mul = ((el_num.Mid(i, 1)).ToVal() * 2).ToInt();
                        Sum = (Sum + (MigrationSupport.Utils.Format(mul, "00").Mid(1, 1)).ToVal() + (MigrationSupport.Utils.Format(mul, "00").Mid(2, 1)).ToVal()).ToInt();
                    }
                    else
                    {
                        mul = ((el_num.Mid(i, 1)).ToVal() * 1).ToInt();
                        Sum = (Sum + (MigrationSupport.Utils.Format(mul, "00").Mid(2, 1)).ToVal()).ToInt();
                    }
                }

                if (Sum == 0)
                {
                    X = 0;
                }
                else
                {
                    X = (((int)Math.Floor((double)(Sum - 1) / 10)) * 10 + 10).ToInt();
                }

                X = X - Sum;
                el_dv = MigrationSupport.Utils.Format(X, "00").Right(1);
                if (el_dv != dv)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Error en dígito verificador del número ingresado.",
                    });
                    return Fn_Valida_Aladi;
                }

                Fn_Valida_Aladi = true.ToInt();

            }

            return Fn_Valida_Aladi;
        }

        public static void CambiaBcxEntrada(string band)
        {
            string BcxEntrada2 = "";
            //  Javier

            string Ent = "";

            Ent = BcxEntrada2;

            //  Ventana de Participantes
            if (band == "P")
            {
                BcxEntrada2 = "1" + BcxEntrada2.Mid(2, 4);
            }
            //  Ventana de Anticipo C.
            if (band == "A")
            {
                BcxEntrada2 = BcxEntrada2.Mid(1, 1) + "1" + BcxEntrada2.Mid(3, 3);
            }
            //  Ventana de Vias
            if (band == "V")
            {
                BcxEntrada2 = BcxEntrada2.Mid(1, 2) + "1" + BcxEntrada2.Mid(4, 2);
            }
            //  Ventana de Cheques
            if (band == "C")
            {
                BcxEntrada2 = BcxEntrada2.Mid(1, 3) + "1" + BcxEntrada2.Mid(5, 1);
            }

            //  Ventana de Orden de Pagos
            if (band == "O")
            {
                BcxEntrada2 = BcxEntrada2.Mid(1, 4) + "1";
            }


        }
    }
}
