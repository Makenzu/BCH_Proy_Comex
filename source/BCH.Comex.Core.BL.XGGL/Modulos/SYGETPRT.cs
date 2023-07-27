using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class SYGETPRT
    {
        
        private const string GPrt_GetParty = "Captura de Participantes";
        private const string GPrt_NoPath = "Error del path a Bases de Datos";
        public static PartyParametros PrtControl = new PartyParametros();


        public static int ResetParty(DatosGlobales Globales,string[] Arreglo)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;//validar

            int ResetParty = 0;

            int Lim = 0;
            int i = 0;
            bool Retorno = false;

            if (SYGETPRT.PrtControl.Leidos == 0)
            {
                Retorno = LeeParametrosParty(Globales);
                if (Retorno)
                {
                    ResetParty = Convert.ToInt32(Retorno);
                    return ResetParty;
                }
            }

            // reset iniciar
            SYGETPRT.PrtControl.Leidos = 1;

            // reset modifico
            SYGETPRT.PrtControl.Modifico = Convert.ToInt32(false);

            // obtenemos los limites de Nombres
            SYGETPRT.PrtControl.LimInf = Arreglo.GetLowerBound(0);
            SYGETPRT.PrtControl.LimSup = Arreglo.GetUpperBound(0);

            // redimencionamos los partys y las tablas
            SYGETPRT.PartysOpe = new PartyKey[SYGETPRT.PrtControl.LimSup + 1];
            for(var k = 0; k < SYGETPRT.PartysOpe.Length; k++)
            {
                SYGETPRT.PartysOpe[k] = new PartyKey();
            }
            SYGETPRT.PrtTbl = new string[SYGETPRT.PrtControl.LimSup + 1];
            for (var k = 0; k < SYGETPRT.PrtTbl.Length; k++)
            {
                SYGETPRT.PrtTbl[k] = string.Empty;
            }

            // definimos el numero de participantes variables
            SYGETPRT.PrtControl.Insertado = 0;
            SYGETPRT.PrtControl.Otros = 0;
            for (i = SYGETPRT.PrtControl.LimInf; i <= SYGETPRT.PrtControl.LimSup; i += 1)
            {
                if (Arreglo[i] == string.Empty)
                {
                    SYGETPRT.PrtControl.Otros = SYGETPRT.PrtControl.Otros + 1;
                }
            }

            // reset PopeOpe
            Lim = -1;
            // OnErrorResumeNext();
            Lim = SYGETPRT.PopeOpe.GetUpperBound(0);
            if (Lim < 0)
            {
                return ResetParty;
            }
            SYGETPRT.PopeOpe = new PartysPope[1] { new PartysPope() };

            return ResetParty;
        }

        // Lee Parametros de la seccion de Party de SCE.INI

        public static bool LeeParametrosParty(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            bool LeeParametrosParty = false;

            int largo = 0;
            string Dato = string.Empty;

            SYGETPRT.PrtControl.PartyPath = MODGPYF0.GetUbicacion("PathPartys").ToUpper();
            if (SYGETPRT.PrtControl.PartyPath.Length == 0)
            {
                SYGETPRT.PrtControl.Leidos = Convert.ToInt32(false);
                LeeParametrosParty = true;
                return LeeParametrosParty;
            }

            Dato = MODGPYF0.GetSceIni("Participantes", "PartyEnRed").ToUpper();
            largo = Dato.Length;
            SYGETPRT.PrtControl.Red = Convert.ToInt32(false);
            if (Convert.ToBoolean(Convert.ToInt32(Dato)))
            {
                SYGETPRT.PrtControl.Red = Convert.ToInt32(true);
            }

            Dato = MODGPYF0.GetSceIni("Participantes", "PartyNodo").ToUpper();
            largo = Dato.Length;
            SYGETPRT.PrtControl.Nodo = string.Empty;
            if (largo != 0)
            {
                SYGETPRT.PrtControl.Nodo = Dato;
            }

            Dato = MODGPYF0.GetSceIni("Participantes", "PartyServidor").ToUpper();
            largo = Dato.Length;
            SYGETPRT.PrtControl.Servidor = string.Empty;
            if (largo != 0)
            {
                SYGETPRT.PrtControl.Servidor = Dato;
            }

            if (SYGETPRT.PrtControl.Nodo == string.Empty || SYGETPRT.PrtControl.Servidor == string.Empty)
            {
                SYGETPRT.PrtControl.Red = Convert.ToInt32(false);
            }

            if (SYGETPRT.PrtControl.Red != 0)
            {
                // Srm.Host = PrtControl.Nodo
                // Srm.Server = PrtControl.Servidor
            }

            SYGETPRT.PrtControl.Leidos = 1;
            LeeParametrosParty = false;

            return LeeParametrosParty;
        }

        // Captura Partys o incribe pegados a la operacion
        public static int GetParty_1_2(DatosGlobales Globales, string[] Tabla, CdOper Operacion, int DesHabilita, int Iniciar)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            int GetParty = 0;
            int j = 0;
            int LimSup = 0;
            SYGETPRT.CtaPartys = 0;
            int i = 0;
            int Retorno = 0;

            SYGETPRT.LasMarcas = T_SYGETPRT.GPrt_MarcaRequerido + T_SYGETPRT.GPrt_MarcaOtros + T_SYGETPRT.GPrt_MarcaBanco;
            SYGETPRT.PrtControl.NoOperacion = DesHabilita;

            // Verifico si debo leer parametros
            if (SYGETPRT.PrtControl.Leidos == 0)
            {
                Retorno = Convert.ToInt32(LeeParametrosParty(Globales));
                if (Retorno != 0)
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type=Common.UI_Modulos.TipoMensaje.Error,
                        Title= T_SYGETPRT.GPrt_GetParty,
                        Text = T_SYGETPRT.GPrt_NoPath
                    });
                    GetParty = Convert.ToInt32(false);
                    return GetParty;
                }
            }

            // Debemos inicializar, dimensionar Partys y resetear modifico
            if (Iniciar != 0)
            {
                Retorno = ResetParty(Globales, Tabla);
                if (Retorno != 0)
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Title = T_SYGETPRT.GPrt_GetParty,
                        Text = T_SYGETPRT.GPrt_NoPath
                    });
                    GetParty = Convert.ToInt32(false);
                    return GetParty;
                }
            }

            // preparo datos para operar
            SYGETPRT.Partys = new PartyKey[SYGETPRT.PrtControl.LimSup + 1];
            for (i = SYGETPRT.PrtControl.LimInf; i <= SYGETPRT.PrtControl.LimSup; i += 1)
            {
                if (SYGETPRT.PrtTbl[i] == string.Empty && Tabla[i] != "")
                {
                    SYGETPRT.PrtTbl[i] = Tabla[i];
                }
                if (SYGETPRT.PrtTbl[i] != string.Empty && Tabla[i] == "")
                {
                    SYGETPRT.PrtTbl[i] = string.Empty;
                }

                SYGETPRT.Partys[i] = SYGETPRT.PartysOpe[i].Copy();
                if (SYGETPRT.Partys[i].Status != T_SYGETPRT.GPrt_StatVacio)
                {
                    SYGETPRT.CtaPartys = SYGETPRT.CtaPartys + 1;
                }
            }

            // preparo Pope para operar
            LimSup = -1;

            LimSup = SYGETPRT.PopeOpe.GetUpperBound(0);

            if (LimSup >= 0)
            {
                // tiene datos
                SYGETPRT.Pope = new PartysPope[LimSup + 1];
                for (i = 0; i <= LimSup; i += 1)
                {
                    SYGETPRT.Pope[i] = SYGETPRT.PopeOpe[i];
                }
            }

            // salvo numero operacion
            SYGETPRT.PrtControl.NumOpe = Operacion;

            // reseteo los cambios
            SYGETPRT.PrtControl.Cambios = string.Empty;
            SYGETPRT.PrtControl.PorInsertar = 0;

            // Parametros leidos, invoco el formulario de ingreso
            Globales.Action = String.Empty;
            Globales.Controller = String.Empty;
            Globales.GetPrty0 = new UI_GetPrty0();
            GetParty = Convert.ToInt32(true);
            return GetParty;
        }

        public static int GetParty_2_2(DatosGlobales Globales, string[] Tabla, CdOper Operacion, int DesHabilita, int Iniciar)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            int i = 0;
            int j = 0;
            bool HayEnOperacion = false;
            int Superior = 0;
            bool PrtCambio = false;
            int GetParty = Convert.ToInt32(false);
            string LlaveAnt = string.Empty;
            string TablaAnt = string.Empty;
            string abc = string.Empty;
            int LimSup = SYGETPRT.PopeOpe.GetUpperBound(0);

            if (GetPrty0.Aceptar.Tag.ToBool())
            {
                // acepto
                SYGETPRT.PrtControl.Leidos = SYGETPRT.PrtControl.Leidos + 1;     // otra vez
                SYGETPRT.PrtControl.Otros = SYGETPRT.PrtControl.Otros - SYGETPRT.PrtControl.PorInsertar;
                SYGETPRT.PrtControl.Insertado = SYGETPRT.PrtControl.Insertado + SYGETPRT.PrtControl.PorInsertar;

                if (SYGETPRT.PrtControl.Modifico == 0)
                {
                    // si es falso verifico
                    for (i = SYGETPRT.PrtControl.LimInf; i <= SYGETPRT.PrtControl.LimSup; i += 1)
                    {

                        // ver si cambio glosas en la tabla
                        if (Tabla[i] == "" && SYGETPRT.PrtTbl[i] != "")
                        {
                            SYGETPRT.PrtControl.Modifico = Convert.ToInt32(true);
                            break;
                        }

                        if (SYGETPRT.Partys[i].Ubicacion == T_SYGETPRT.GPrt_EnParty)
                        {
                            // ver cambios en party
                            // cambios de status o llaves ==> modifico
                            if (SYGETPRT.Partys[i].Status != SYGETPRT.PartysOpe[i].Status || SYGETPRT.Partys[i].LlaveArchivo != SYGETPRT.PartysOpe[i].LlaveArchivo)
                            {
                                SYGETPRT.PrtControl.Modifico = Convert.ToInt32(true);
                                break;
                            }
                            // cambios de IndNombre o IndDireccion ==> modifico
                            if (SYGETPRT.Partys[i].IndNombre != SYGETPRT.PartysOpe[i].IndNombre || SYGETPRT.Partys[i].IndDireccion != SYGETPRT.PartysOpe[i].IndDireccion)
                            {
                                SYGETPRT.PrtControl.Modifico = Convert.ToInt32(true);
                                break;
                            }
                        }
                        else
                        {
                            HayEnOperacion = true;
                        }
                    }

                    // si no hay modificacion y HayEnOperacion, verifico cambios
                    if (!SYGETPRT.PrtControl.Modifico.ToBool() && HayEnOperacion)
                    {
                        Superior = -1;

                            Superior = SYGETPRT.Pope.GetUpperBound(0);

                            for (j = 0; j <= Superior; j += 1)
                            {
                                if (SYGETPRT.Pope[j].Status != T_SYGETPRT.GPrt_StatVacio && SYGETPRT.Pope[j].Status != T_SYGETPRT.GPrt_StatIntacto)
                                {
                                    SYGETPRT.PrtControl.Modifico = Convert.ToInt32(true);
                                    break;
                                }
                            }

                    }
                }

                // acepto los cambio de PartysOpe
                SYGETPRT.CtaPartys = 0;
                for (i = SYGETPRT.PrtControl.LimInf; i <= SYGETPRT.PrtControl.LimSup; i += 1)
                {
                    PrtCambio = false;
                    abc = SYGETPRT.PrtTbl[i].Left(1);     // busco marca

                    if ((SYGETPRT.LasMarcas.InStr(abc, 1, StringComparison.CurrentCulture) > 0) && abc != "")
                    {
                        // tiene marca
                        // si es marca de otros
                        if (abc == T_SYGETPRT.GPrt_MarcaOtros && Tabla[i] != SYGETPRT.PrtTbl[i].Mid(2))
                        {
                            PrtCambio = true;
                            TablaAnt = Tabla[i];
                            Tabla[i] = SYGETPRT.PrtTbl[i].Mid(2);
                        }

                        // saco las marcas a glosas que no puedo modificar
                        if (abc != T_SYGETPRT.GPrt_MarcaOtros)
                        {
                            Tabla[i] = SYGETPRT.PrtTbl[i].Mid(2);
                        }
                    }
                    else
                    {
                        Tabla[i] = SYGETPRT.PrtTbl[i];
                    }

                    if (SYGETPRT.PartysOpe[i].LlaveArchivo != SYGETPRT.Partys[i].LlaveArchivo)
                    {
                        PrtCambio = true;
                        LlaveAnt = SYGETPRT.PartysOpe[i].LlaveArchivo + ":";
                        LlaveAnt = LlaveAnt + MigrationSupport.Utils.Format(SYGETPRT.PartysOpe[i].IndNombre, "0") + ":";
                        LlaveAnt = LlaveAnt + MigrationSupport.Utils.Format(SYGETPRT.PartysOpe[i].IndDireccion, "00");
                    }
                    SYGETPRT.PartysOpe[i] = SYGETPRT.Partys[i].Copy();

                    // reflejar cambios en string de cambios
                    if (PrtCambio)
                    {
                        SYGETPRT.PrtControl.Cambios = SYGETPRT.PrtControl.Cambios + MigrationSupport.Utils.Format(i, "00") + ":";
                        SYGETPRT.PrtControl.Cambios = SYGETPRT.PrtControl.Cambios + LlaveAnt + ",";
                    }

                    if (SYGETPRT.PartysOpe[i].Status != T_SYGETPRT.GPrt_StatVacio)
                    {
                        SYGETPRT.CtaPartys = SYGETPRT.CtaPartys + 1;
                    }
                    // RTO 2012/03/30 INCIDENTE IR46139
                    if (SYGETPRT.PartysOpe[i].LlaveArchivo == "(00)~~~~~~~~" && SYGETPRT.Partys[i].Rut.TrimB() != "")
                    {
                        SYGETPRT.PartysOpe[i].LlaveArchivo = MigrationSupport.Utils.Format(SYGETPRT.PartysOpe[i].Rut, "#||||||||||||");
                        SYGETPRT.Partys[i].LlaveArchivo = MigrationSupport.Utils.Format(SYGETPRT.Partys[i].Rut, "#||||||||||||");
                    }
                    // RTO 2012/03/30 INCIDENTE IR46139

                }
                if (SYGETPRT.PrtControl.Cambios != "")
                {
                    SYGETPRT.PrtControl.Cambios = SYGETPRT.PrtControl.Cambios.Left((SYGETPRT.PrtControl.Cambios.Len() - 1));
                }

                // acepto los cambios de Pope
                LimSup = -1;

                    LimSup = SYGETPRT.Pope.GetUpperBound(0);

                    if (LimSup >= 0)
                    {
                        // tiene datos
                        SYGETPRT.PopeOpe = new PartysPope[LimSup + 1];
                        for (i = 0; i <= LimSup; i += 1)
                        {
                            SYGETPRT.PopeOpe[i] = SYGETPRT.Pope[i];
                        }
                    }

            }

            // Libero recursos de arreglo
            SYGETPRT.Partys = new PartyKey[1] { new PartyKey() };
            SYGETPRT.Pope = new PartysPope[1] { new PartysPope() };

            // Volvemos
            GetPrty0.Cancelar.Tag = "0";
            //Globales.GetPrty0 = null;
            //Globales.GetPrty1 = null;
            
            
            GetParty = SYGETPRT.CtaPartys;

            return GetParty;
        }

        // Elige el Participante.-
        public static int EligeSy_1_2(DatosGlobales Globales,UnitOfWorkCext01 unit)
        
        {
            using (var tracer = new Tracer())
            {

                UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

                int EligeSy = 0;
                int iNewIndex = 0;
                int EligeNomb_DirSy = 0;
                string q = string.Empty;
                int j = 0;
                int l = 0;
                int Id_Direccion = 0;
                string Direccion = string.Empty;
                int Nombre = 0;
                string razon = string.Empty;
                int Borrado = 0;
                int i = 0;
                int n = 0;
                string R = string.Empty;
                string Que = string.Empty;
                string dd = string.Empty;
                string FileKey = string.Empty;
                string s = string.Empty;
                int paso = 0;

                try
                {
                    Globales.GetPrty1 = new UI_GetPrty1();
                    UI_GetPrty1 GetPrty1 = Globales.GetPrty1;
                    GetPrty1.Text = T_SYGETPRT.GPrt_Caption + " [" + GetPrty0.Llave.Text + "]";
                    s = GetPrty0.Llave.Text + new string(126.ToChar(), 12 - GetPrty0.Llave.Text.Len());
                    s = MODGPYF0.Componer(s.TrimB(), "~", "|");
                    FileKey = s;

                    // si llave es igual al respaldo ==> ya lei y no he cambiado
                    if (FileKey == GetPrty1.Tag.ToStr())
                    {
                        if (GetPrty1.Nome.Items.Count == 1 && GetPrty1.Dire.Items.Count == 1)
                        {
                            EligeSy = T_SYGETPRT.GPrt_RetExiste;
                        }
                        else
                        {
                            Globales.GetPrty1 = new UI_GetPrty1();
                            EligeSy = Convert.ToInt32("0" + GetPrty1.Aceptar.Tag);
                        }
                        return EligeSy;
                    }
                    paso = 1;
                    var items = unit.SceRepository.EjecutarSP<pro_sce_rsa_s01_MS_Result>("pro_sce_rsa_s01_MS", FileKey.ToUpper());
                    // Tabla Razones Sociales (Sce_Rsa).-
                    if (items.Count == 0)
                    {
                        paso = 2;
                        throw new Exception();
                    }


                    GetPrty1.Nome.Items.Clear();
                    n = items.Count;
                    for (i = 1; i <= n; i += 1)
                    {
                        Borrado = Convert.ToInt32(items[i - 1].borrado);
                        razon = items[i - 1].razon_soci;
                        Nombre = Convert.ToInt32(items[i - 1].id_nombre);
                        GetPrty1.Nome.Items.Add(new Common.UI_Modulos.UI_ComboItem() { ID = Nombre.ToString(), Data = Nombre, Value = razon });
                    }

                    paso = 3;

                    var dads = unit.SceRepository.EjecutarSP<pro_sce_dad_s01_MS_Result>("pro_sce_dad_s01_MS", FileKey.ToUpper());
                    // Tabla Direcciones (Sce_Dad).-

                    // Hubo Error.-
                    paso = 4;
                    if (dads.Count == 0)
                    {
                        throw new Exception();
                    }

                    GetPrty1.Dire.Items.Clear();
                    GetPrty1.Otro.Items.Clear();
                    n = dads.Count;
                    for (i = 1; i <= n; i += 1)
                    {
                        Borrado = Convert.ToInt32(dads[i - 1].borrado);
                        Direccion = dads[i - 1].direccion;
                        Id_Direccion = Convert.ToInt32(dads[i - 1].id_dir);

                        if (Borrado == 0)
                        {
                            GetPrty1.Dire.Items.Add(new Common.UI_Modulos.UI_ComboItem()
                            {
                                ID = Id_Direccion.ToString(),
                                Data = Id_Direccion,
                                Value = Direccion
                            });
                            GetPrty1.Otro.Items.Add(new Common.UI_Modulos.UI_ComboItem()
                            {
                                ID = (i - 1).ToString(),
                                Data = i - 1,
                                Value = dads[i - 1].borrado + "~" + dads[i - 1].direccion + "~" + dads[i - 1].id_dir + "~" + dads[i - 1].comuna + "~" + dads[i - 1].ciudad + "~" + dads[i - 1].cod_postal + "~" + dads[i - 1].pais + "~" + dads[i - 1].cod_pais + "~" + dads[i - 1].estado + "~" + dads[i - 1].telefono + "~" + dads[i - 1].fax + "~" + dads[i - 1].telex + "~" + dads[i - 1].envio_sce + "~" + dads[i - 1].cas_postal + "~" + dads[i - 1].cas_banco
                            });
                        }
                    }

                    // Mostrar seleccion anterior
                    if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status != T_SYGETPRT.GPrt_StatVacio)
                    {
                        for (i = 0; i <= GetPrty1.Nome.Items.Count - 1; i += 1)
                        {
                            if (Convert.ToInt32(GetPrty1.Nome.get_ItemData_(i)) == SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].IndNombre)
                            {
                                GetPrty1.Nome.ListIndex = i;
                                break;
                            }
                        }
                        if (GetPrty1.Nome.ListIndex == -1)
                        {
                            GetPrty1.Nome.ListIndex = 0;
                        }

                        for (i = 0; i <= GetPrty1.Dire.Items.Count - 1; i += 1)
                        {
                            if (Convert.ToInt32(GetPrty1.Dire.get_ItemData_(i)) == SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].IndDireccion)
                            {
                                GetPrty1.Dire.ListIndex = i;
                                break;
                            }
                        }
                        if (GetPrty1.Dire.ListIndex == -1)
                        {
                            GetPrty1.Dire.ListIndex = 0;
                        }
                    }
                    else
                    {
                        GetPrty1.Nome.ListIndex = 0;
                        GetPrty1.Dire.ListIndex = 0;
                    }

                    // Si tiene solo un item cada lista, debemos volver
                    if (GetPrty1.Nome.Items.Count == 1 && GetPrty1.Dire.Items.Count == 1)
                    {
                        EligeNomb_DirSy = T_SYGETPRT.GPrt_RetExiste;
                        Globales.GetPrty0.AbrirIdentParticipantes = false;
                        //Globales.GetPrty1 = null;
                        return EligeSy;
                    }

                    Globales.GetPrty0.AbrirIdentParticipantes = true;
                    //guardo la llave para despues
                    SYGETPRT.FileKey = FileKey;
                    return EligeSy;
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta:", exc);

                    string text = String.Empty;
                    switch (paso)
                    {
                        case 1:
                            text = "Se ha producido un error al leer los datos de los Participantes.";
                            break;
                        case 2:
                            text = T_SYGETPRT.GPrt_ErrGetDbf + "(Nombres)";
                            break;
                        case 3:
                            text = "Se ha producido un error al leer las Direcciones de los Participantes.";
                            break;
                        case 4:
                            text = T_SYGETPRT.GPrt_ErrGetDbf + "(Direccion)";
                            break;
                    }
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Title = T_SYGETPRT.GPrt_Caption,
                        Text = text,
                        Type = TipoMensaje.Error
                    });
                }
                EligeSy = Convert.ToInt32(true);
                return EligeSy;
            }
        }

        public static int EligeSy_2_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            if (Globales.GetPrty1 == null)
            {
                Globales.GetPrty1 = new UI_GetPrty1();
            }
            UI_GetPrty1 GetPrty1 = Globales.GetPrty1;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            // Respaldamos la llave actual.-
            GetPrty1.Tag = SYGETPRT.FileKey;
            var EligeSy = Convert.ToInt32("0" + GetPrty1.Aceptar.Tag);
            return EligeSy;
        }

        // valida si es rut
        public static int NoEsRut(string Rut)
        {
            int NoEsRut = 0;
            string DvCal = string.Empty;
            int Es = 0;
            int suma = 0;
            int aa = 0;
            string a = string.Empty;
            int i = 0;
            string b = string.Empty;
            string D_V = string.Empty;
            const string Son = "1234567890K";

            D_V = Rut.Right(1);
            b = Rut.Left((Rut.Len() - 1));

            for (i = 1; i <= b.Len(); i += 1)
            {
                a = b.Right(i);
                aa = Convert.ToInt32((a.Left(1)));

                if (i < 7)
                {
                    suma = suma + aa * (i + 1);
                }
                else
                {
                    suma = suma + aa * (i - 5);
                }
            }

            Es = 11 - suma % 11;
            switch (Es)
            {
                case 11:
                    DvCal = "0";
                    break;
                case 10:
                    DvCal = "K";
                    break;
                default:
                    DvCal = MigrationSupport.Utils.Format(Es, String.Empty);
                    break;
            }

            if (DvCal != D_V.UCase())
            {
                NoEsRut = Convert.ToInt32(true);
            }

            return NoEsRut;
        }

        // valida si es rut
        public static int EsRut(string Rut)
        {
            int EsRut = 0;
            string DvCal = string.Empty;
            int Es = 0;
            int suma = 0;
            int aa = 0;
            string a = string.Empty;
            int i = 0;
            string b = string.Empty;
            string D_V = string.Empty;
            EsRut = Convert.ToInt32(false);
            const string Son = "1234567890K";

            D_V = Rut.Right(1);
            b = Rut.Left((Rut.Len() - 1));

            for (i = 1; i <= b.Len(); i += 1)
            {
                a = b.Right(i);
                aa = Convert.ToInt32((a.Left(1)));

                if (i < 7)
                {
                    suma = suma + aa * (i + 1);
                }
                else
                {
                    suma = suma + aa * (i - 5);
                }
            }

            Es = 11 - suma % 11;
            switch (Es)
            {
                case 11:
                    DvCal = "0";
                    break;
                case 10:
                    DvCal = "K";
                    break;
                default:
                    DvCal = MigrationSupport.Utils.Format(Es, String.Empty);
                    break;
            }

            if (DvCal == D_V.UCase())
            {
                EsRut = Convert.ToInt32(true);
            }

            return EsRut;
        }

        // ve si el flag tiene el seteo Que
        public static int PrtyFlag(int flag, int Que)
        {
            int PrtyFlag = 0;

            if ((flag & Que) != 0)
            {
                PrtyFlag = Convert.ToInt32(true);
            }
            return PrtyFlag;
        }

        public static string FilCero(string Que, string Mascara, int Modo)
        {
            string FilCero = string.Empty;
            string sal = string.Empty;
            int Mas = 0;
            string NewMask = string.Empty;
            string a = string.Empty;
            int i = 0;
            string tmp = string.Empty;

            tmp = Que.TrimB();

            for (i = 1; i <= Mascara.Len(); i += 1)
            {
                a = Mascara.Mid(i, 1);
                if (a != "_")
                {
                    NewMask = NewMask + a;
                }
                else
                {
                    Mas = Mas + 1;
                }
            }

            for (i = 1; i <= tmp.Len(); i += 1)
            {
                a = tmp.Mid(i, 1);
                if (NewMask.InStr(a, 1, StringComparison.CurrentCulture) == 0)
                {
                    sal = sal + a;
                }
            }

            if (Modo != 0)
            {
                FilCero = sal + new string(' ', Mas - sal.Len());
            }
            else
            {
                FilCero = new string(48.ToChar(), Math.Abs(Mas - sal.Len())) + sal;
            }

            return FilCero;
        }

        //Lee los datos de los partys faltantes
        public static short SyGet_Prt(ref CdOper Operacion, short NoOperacion, DatosGlobales initObj, UnitOfWorkCext01 unit)
        {
            #region Inicializacion Variables
            short _retValue = 0;
            short Retorno = 0;
            short i = 0;
            string Llave = string.Empty;
            string Nome = string.Empty;
            string Dire = string.Empty;
            string R = string.Empty;
            short Borrado = 0;
            short T = 0;
            string Empresa = string.Empty;
            string Opera = string.Empty;
            short RR = 0;
            short k = 0;
            #endregion

            try
            {
                #region Verifico si debo leer parametros
                if (initObj.SYGETPRT.PrtControl.Leidos == 0)
                {
                    Retorno = VB6Helpers.CShort(SYGETPRT.LeeParametrosParty(initObj));
                    if (Retorno != 0)
                    {
                        return (short)(true ? -1 : 0);
                    }
                }
                #endregion
                #region Recorrer los PartysOpe viendo cada llave distinta de vacio.
                for (i = (short)VB6Helpers.LBound(initObj.SYGETPRT.PartysOpe); i <= (short)VB6Helpers.UBound(initObj.SYGETPRT.PartysOpe); i++)
                {
                    Llave = VB6Helpers.Trim(initObj.SYGETPRT.PartysOpe[i].LlaveArchivo); //Por si es NULL
                    Nome = VB6Helpers.Format(VB6Helpers.CStr(initObj.SYGETPRT.PartysOpe[i].IndNombre));
                    Dire = VB6Helpers.Format(VB6Helpers.CStr(initObj.SYGETPRT.PartysOpe[i].IndDireccion), "00");

                    if (!string.IsNullOrEmpty(Llave))
                    {
                        if (initObj.SYGETPRT.PartysOpe[i].Ubicacion == T_SYGETPRT.GPrt_EnParty)
                        {
                            #region Datos del Participante.-
                            var ret = unit.SceRepository.pro_sce_prty_s02_MS_1(MODGSYB.dbcharSy(Llave), "1");
                            if (ret != null)
                            {
                                _retValue = (short)(true ? -1 : 0);

                                Borrado = (short)(ret.borrado ? -1 : 0);
                                initObj.SYGETPRT.PartysOpe[i].TipoParty = (short)ret.tipo_party;
                                initObj.SYGETPRT.PartysOpe[i].FlagParty = (short)ret.flag;
                                T = (short)(ret.tiene_rut ? -1 : 0);
                                initObj.SYGETPRT.PartysOpe[i].Rut = ret.rut;
                                initObj.SYGETPRT.PartysOpe[i].CodBanco = ret.cod_bco.ToString();
                                initObj.SYGETPRT.PartysOpe[i].Swift = ret.swift;
                            }
                            #endregion

                            #region Datos del Nombre del Participante.-
                            //Procedimiento Almacenado generado de forma automatica.
                            var result = unit.SceRepository.pro_sce_prty_s05_MS(MODGSYB.dbcharSy(Llave), Convert.ToInt32(Nome), 1);
                            if (result != null)
                            {
                                _retValue = (short)(true ? -1 : 0);

                                Borrado = Convert.ToSByte(result.borrado);
                                initObj.SYGETPRT.PartysOpe[i].NombreUsado = result.razon_soci;
                            }
                            #endregion

                            #region Datos de la Direccion del Participante.-
                            //Procedimiento Almacenado generado de forma manual:retorna un solo registro.
                            var result1 = unit.SceRepository.pro_sce_prty_s05_MS_01(MODGSYB.dbcharSy(Llave), Convert.ToInt32(Dire), 2);
                            if (result1 != null)
                            {
                                _retValue = (short)(true ? -1 : 0);


                                var tmpModule1 = new List<T_SYGETPRT>();

                                //Inicializar clase
                                Borrado = Convert.ToSByte(result1.borrado);
                                initObj.SYGETPRT.PartysOpe[i].DireccionUsado = result1.direccion;
                                initObj.SYGETPRT.PartysOpe[i].ComunaUsado = result1.comuna;
                                initObj.SYGETPRT.PartysOpe[i].PostalUsado = result1.cod_postal;
                                initObj.SYGETPRT.PartysOpe[i].EstadoUsado = result1.estado;
                                initObj.SYGETPRT.PartysOpe[i].CiudadUsado = result1.ciudad;
                                initObj.SYGETPRT.PartysOpe[i].PaisUsado = result1.pais;
                                initObj.SYGETPRT.PartysOpe[i].codpais = (short)result1.cod_pais;
                                initObj.SYGETPRT.PartysOpe[i].Telefono = VB6Helpers.Format(result1.telefono, "0");
                                initObj.SYGETPRT.PartysOpe[i].Fax = VB6Helpers.Format(result1.fax, "0");
                                initObj.SYGETPRT.PartysOpe[i].Telex = result1.telex;
                                initObj.SYGETPRT.PartysOpe[i].Enviara = (short)result1.envio_sce;
                                initObj.SYGETPRT.PartysOpe[i].CasPostal = result1.cas_postal;
                                initObj.SYGETPRT.PartysOpe[i].CasBanco = result1.cas_banco;
                            }
                            if (VB6Helpers.Val(initObj.SYGETPRT.PartysOpe[i].Telefono) == 0)
                            {
                                initObj.SYGETPRT.PartysOpe[i].Telefono = "";
                            }
                            if (VB6Helpers.Val(initObj.SYGETPRT.PartysOpe[i].Fax) == 0)
                            {
                                initObj.SYGETPRT.PartysOpe[i].Fax = "";
                            }

                            _retValue = (short)(true ? -1 : 0);
                            #endregion
                        }
                        else
                        {
                            #region En operacion
                            //Procedimiento Almacenado generado de forma automatica.
                            Empresa = VB6Helpers.Left(Operacion.Id_Empresa, 2);
                            Opera = VB6Helpers.Right(Operacion.Id_Empresa, 1) + Operacion.Id_Operacion;

                            var result2 = unit.SceRepository.pro_sce_prty_s03_MS(
                                       MODGSYB.dbcharSy(Operacion.Cent_costo),
                                       MODGSYB.dbcharSy(Operacion.Id_Product),
                                       MODGSYB.dbcharSy(Operacion.Id_Especia),
                                       MODGSYB.dbcharSy(Operacion.Id_Empresa),
                                       MODGSYB.dbcharSy(Operacion.Id_Operacion),
                                       MODGSYB.dbcharSy(VB6Helpers.Format(VB6Helpers.CStr(i), "00")),
                                       1
                            );

                            if (result2 == null)
                            {

                                RR = VB6Helpers.CShort(MODGSYB.GetPosSy(MODGSYB.NumIni(), "L", R));
                                if (RR != 0)
                                {
                                    initObj.SYGETPRT.PartysOpe[i].TipoParty = T_SYGETPRT.GPrt_TipoBcoOperacion;  //bco operacion
                                    initObj.SYGETPRT.PartysOpe[i].Rut = string.Empty;
                                    initObj.SYGETPRT.PartysOpe[i].Swift = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                                }
                                else
                                {
                                    initObj.SYGETPRT.PartysOpe[i].TipoParty = T_SYGETPRT.GPrt_TipoEnOperacion;  //en operacion
                                    initObj.SYGETPRT.PartysOpe[i].Rut = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                                    initObj.SYGETPRT.PartysOpe[i].Swift = string.Empty;
                                }
                                //Globales.Module1.PartysOpe[i].NombreUsado = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                                initObj.SYGETPRT.PartysOpe[i].DireccionUsado = result2.direccion;
                                initObj.SYGETPRT.PartysOpe[i].ComunaUsado = result2.comuna;
                                initObj.SYGETPRT.PartysOpe[i].EstadoUsado = result2.estado;
                                initObj.SYGETPRT.PartysOpe[i].CiudadUsado = result2.ciudad;
                                initObj.SYGETPRT.PartysOpe[i].PaisUsado = result2.pais;
                                initObj.SYGETPRT.PartysOpe[i].codpais = (short)result2.cod_pais;
                                initObj.SYGETPRT.PartysOpe[i].PostalUsado = result2.cod_postal;
                                initObj.SYGETPRT.PartysOpe[i].Telefono = result2.telefono;
                                initObj.SYGETPRT.PartysOpe[i].Fax = result2.fax;
                                initObj.SYGETPRT.PartysOpe[i].Telex = result2.telex;
                                //Globales.Module1.PartysOpe[i].Enviara =result2.envio_sce;
                                initObj.SYGETPRT.PartysOpe[i].CasPostal = result2.cas_postal;
                                initObj.SYGETPRT.PartysOpe[i].CasBanco = result2.cas_banco;

                                #region Llenar estructura con partys pegados a la operación
                                k = MODXANU.Nuevo_Pope(initObj);
                                initObj.SYGETPRT.PopeOpe[k].Status = T_SYGETPRT.GPrt_StatIntacto;
                                initObj.SYGETPRT.PopeOpe[k].Secuencia = i;
                                initObj.SYGETPRT.PopeOpe[k].EsBanco = RR;
                                if (RR != 0)
                                {
                                    initObj.SYGETPRT.PopeOpe[k].RutSwift = initObj.SYGETPRT.PartysOpe[i].Swift;
                                }
                                else
                                {
                                    initObj.SYGETPRT.PopeOpe[k].RutSwift = initObj.SYGETPRT.PartysOpe[i].Rut;
                                }

                                initObj.SYGETPRT.PopeOpe[k].Nombre = initObj.SYGETPRT.PartysOpe[i].NombreUsado;
                                initObj.SYGETPRT.PopeOpe[k].Direccion = initObj.SYGETPRT.PartysOpe[i].DireccionUsado;
                                initObj.SYGETPRT.PopeOpe[k].comuna = initObj.SYGETPRT.PartysOpe[i].ComunaUsado;
                                initObj.SYGETPRT.PopeOpe[k].Ciudad = initObj.SYGETPRT.PartysOpe[i].CiudadUsado;
                                initObj.SYGETPRT.PopeOpe[k].estado = initObj.SYGETPRT.PartysOpe[i].EstadoUsado;
                                initObj.SYGETPRT.PopeOpe[k].Pais = initObj.SYGETPRT.PartysOpe[i].PaisUsado;
                                initObj.SYGETPRT.PopeOpe[k].codpais = initObj.SYGETPRT.PartysOpe[i].codpais;
                                initObj.SYGETPRT.PopeOpe[k].Postal = initObj.SYGETPRT.PartysOpe[i].PostalUsado;
                                initObj.SYGETPRT.PopeOpe[k].Telefono = initObj.SYGETPRT.PartysOpe[i].Telefono;
                                initObj.SYGETPRT.PopeOpe[k].Fax = initObj.SYGETPRT.PartysOpe[i].Fax;
                                initObj.SYGETPRT.PopeOpe[k].Telex = initObj.SYGETPRT.PartysOpe[i].Telex;
                                initObj.SYGETPRT.PopeOpe[k].CasPostal = initObj.SYGETPRT.PartysOpe[i].CasPostal;
                                initObj.SYGETPRT.PopeOpe[k].CasBanco = initObj.SYGETPRT.PartysOpe[i].CasBanco;
                                #endregion
                            }
                            else
                            {
                                return (short)(true ? -1 : 0);
                            }

                            #endregion
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                #region Excepcion
                initObj.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODGCVD.MsgCVD + ex
                });
                #endregion
            }
            _retValue = (short)(false ? -1 : 0);
            return _retValue;
        }


        public static string DesCero(string Que, string Mascara, int Modo)
        {
            string DesCero = string.Empty;
            string b = string.Empty;
            int i = 0;
            int Aqui = 0;
            string sal = string.Empty;
            string tmp = string.Empty;

            tmp = Que.TrimB();
            if (Modo != 0)
            {
                // es para swift
                if (tmp.Len() == 0)
                {
                    DesCero = Mascara;
                    return DesCero;
                }

                sal = tmp + Mascara.Right((Mascara.Len() - tmp.Len()));
            }
            else
            {
                // sacar ceros a la izquierda
                Aqui = tmp.Len();
                if (tmp.Len() == 0)
                {
                    DesCero = Mascara;
                    return DesCero;
                }

                tmp = MODGPYF0.TrimChar(Que, "0", Convert.ToInt32(false));
                if (tmp.Len() == 0)
                {
                    DesCero = Mascara;
                    return DesCero;
                }

                tmp = new string('_', Aqui - tmp.Len()) + tmp;

                for (i = Mascara.Len(); i >= 1; i += -1)
                {
                    b = Mascara.Mid(i, 1);
                    if (b == "_" && Aqui.ToBool())
                    {
                        sal = tmp.Mid(Aqui, 1) + sal;
                        Aqui = Aqui - 1;
                    }
                    else
                    {
                        sal = b + sal;
                    }
                }
            }

            return sal;
        }

    }
}
