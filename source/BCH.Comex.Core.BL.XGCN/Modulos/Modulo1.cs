using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BCH.Comex.Core.BL.XGCN.Modulos
{
    public static class Modulo1
    {
        public const string FormatoMto = "#,###,###,###,##0.00";
        public const string GPrt_RutMascara = "__.___.___-_";
        // Global Const guiones = ""     '"********************"
        public const string formato_tasa = "##0.000000";
        // Global Const guiones_tasa = ""  ' "**********"
        // Global Const Fono_nac = "(###) ###-####"
        // Global Const Limpia_Fono = "(___) ___-____"
        public const string formato_rut = "@@@.@@@.@@@-@";
        
        public static string MonthLastDay(DateTime dCurrDate)
        {
            string MonthLastDay = "";

            DateTime dFirstDayNextMonth = DateTime.MinValue;

            try
            {

                MonthLastDay = null;
                dFirstDayNextMonth = MigrationSupport.Utils.DateSerial(MigrationSupport.Utils.Format(dCurrDate, "yyyy").ToInt(), MigrationSupport.Utils.Format(dCurrDate, "mm").ToInt() + 1, 1);
                MonthLastDay = (dFirstDayNextMonth.AddDate("d", (-1))).ToStr();

                return MonthLastDay;
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                //System.Windows.Forms.MessageBox.Show(MigrationSupport.GlobalException.Instance.Description, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return MonthLastDay;
        }
        
        //public static void Exporta_Grilla(/*System.Windows.Forms.Control*/ string grilla, string pr_dir_arch_exp, string pr_arch_exportacion, string pr_ruta_excel)
        //{
        //    object FECHA_SERVIDOR1 = null;
        //    string DIR_Arch_Export = "";
        //    int i = 0;
        //    int J = 0;
        //    int li_ValRet = 0;
        //    string ls_linea = "";
        //    string ls_arch_exp = "";
        //    int li_error = 0;
        //    string ls_campo = "";
        //    // Procedimiento para Exportar el contenido de una Grilla
        //    // a un archivo txt
        //    // Modif :  C.G.P 05/07/2004

        //    MigrationSupport.Utils.ResumeNext(() =>
        //    {

        //        string ls_arch_exportacion = "";
        //        string ls_ruta_excel = "";
        //        string ls_nombre = "";
        //        string nom_inic_arch = "";
        //        string ext_arch_inic = "";
        //        string arch_exportacion = "";

        //        // Validar
        //        if (pr_ruta_excel.TrimB() == "")
        //        {
        //            //System.Windows.Forms.MessageBox.Show("Falta la ruta de la aplicación Excel." + 13.Char() + "No se puede exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return;
        //        }

        //        if (pr_dir_arch_exp.TrimB() == "")
        //        {
        //            //System.Windows.Forms.MessageBox.Show("Falta la ruta del archivo de exportación." + 13.Char() + "No se puede exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return;
        //        }

        //        if (pr_arch_exportacion.TrimB() == "")
        //        {
        //            //System.Windows.Forms.MessageBox.Show("Falta el nombre del archivo de exportación." + 13.Char() + "No se puede exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return;
        //        }

        //        // -------------------
        //        // ''"UFP.tab"
        //        // nom_inic_arch = Mid(pr_arch_exportacion, 1, 3)  '''"UFP"
        //        // ext_arch_inic = Mid(pr_arch_exportacion, 4)      '''".tab"
        //        arch_exportacion = pr_arch_exportacion;

        //        if (pr_dir_arch_exp.Mid(pr_dir_arch_exp.Len(), 1) == "\\")
        //        {
        //            ls_arch_exp = pr_dir_arch_exp + arch_exportacion;
        //        }
        //        else
        //        {
        //            ls_arch_exp = pr_dir_arch_exp + "\\" + arch_exportacion;
        //        }


        //        MigrationSupport.GlobalException.Clear();

        //        // Abrir archivo en modo escritura
        //        MigrationSupport.FileSystem.FileOpen(1, ls_arch_exp, MigrationSupport.FileSystem.OpenMode.Output, MigrationSupport.FileSystem.OpenAccess.Default, MigrationSupport.FileSystem.OpenShare.Default, -1);

        //        li_error = MigrationSupport.GlobalException.Instance.Number;

        //        if (MigrationSupport.GlobalException.Instance.Number == 76)
        //        {
        //            // ruta no existe
        //            MigrationSupport.GlobalException.Clear();
        //            // Crear directorio
        //            System.IO.Directory.CreateDirectory(DIR_Arch_Export);
        //            if (MigrationSupport.GlobalException.Instance.Number != 0)
        //            {
        //                if (MigrationSupport.GlobalException.Instance.Number == 52)
        //                {
        //                    //System.Windows.Forms.MessageBox.Show("El archivo de exportación se encuentra en uso." + 13.Char() + "Por favor, cierre el archivo y vuelva a Exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //                else
        //                {
        //                    //System.Windows.Forms.MessageBox.Show(MigrationSupport.GlobalException.Instance.Number.ToStr() + 13.Char() + MigrationSupport.GlobalException.Instance.Description, "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //                return;
        //            }

        //            MigrationSupport.GlobalException.Clear();
        //            MigrationSupport.FileSystem.FileOpen(1, ls_arch_exp, MigrationSupport.FileSystem.OpenMode.Output, MigrationSupport.FileSystem.OpenAccess.Default, MigrationSupport.FileSystem.OpenShare.Default, -1);
        //            if (MigrationSupport.GlobalException.Instance.Number != 0)
        //            {
        //                //System.Windows.Forms.MessageBox.Show(MigrationSupport.GlobalException.Instance.Number.ToStr() + 13.Char() + MigrationSupport.GlobalException.Instance.Description, "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }
        //        }


        //    });
        //    try
        //    {

        //        if (li_error != 0)
        //        {
        //            if (li_error == 70)
        //            {
        //                //System.Windows.Forms.MessageBox.Show("El archivo de exportación se encuentra en uso." + 13.Char() + "Por favor, cierre el archivo y vuelva a Exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }
        //        }

        //        // 
        //        // ''Print #1, "Exportación Consulta de Simulación Cartera Forward - Sistema Consulta Forward" + Chr(9)
        //        MigrationSupport.FileSystem.PrintLine(1, "Fecha Consulta: " + FECHA_SERVIDOR1 + 9.Char());
        //        // Print #1, Chr(9)
        //        // 
        //        // Iniciar ciclo para recorrer la grilla
        //        for (i = 0; i <= ((object)((dynamic)grilla).Rows()).ToInt() - 1; i += 1)
        //        {
        //            // Leer campos de una fila i, para formar una linea
        //            ((dynamic)grilla).Row = i;

        //            // Escribir linea en archivo
        //            // Recorre columnas de la grilla
        //            ls_linea = "";
        //            for (J = 1; J <= ((object)((dynamic)grilla).cols()).ToInt() - 1; J += 1)
        //            {
        //                ((dynamic)grilla).Col = J;

        //                // If Grilla.Col = 0 And Grilla.Text <> "" Then
        //                //    ls_nombre = Grilla.Text
        //                // End If
        //                // 
        //                // If Grilla.Col = 0 Then
        //                //    ls_campo = ls_nombre
        //                // Else
        //                ls_campo = grilla.Text;
        //                // End If

        //                ls_linea = ls_linea + ls_campo + 9.Char();
        //            }
        //            // Para identificar si la linea a imprimir es una linea de separación
        //            // verificamos si está llena la Columna del numero de operacion
        //            // Grilla.Col = Grilla.Cols - 1
        //            // If Grilla.Text <> "" Then
        //            MigrationSupport.FileSystem.PrintLine(1, ls_linea);
        //            // End If

        //        }
        //        // Cerrar ciclo
        //        MigrationSupport.FileSystem.FileClose(1);
        //        // Cerrar archivo.
        //        // 
        //        // 
        //        // 
        //        // Llamar al Excel para expotar el archivo
        //        li_ValRet = MigrationSupport.Utils.Shell(pr_ruta_excel + " " + ls_arch_exp, MigrationSupport.Utils.AppWinStyle.NormalFocus);

        //        return;


        //    }
        //    catch (Exception exc)
        //    {
        //        MigrationSupport.GlobalException.Initialize(exc);
        //        if (MigrationSupport.GlobalException.Instance.Number == 70)
        //        {
        //            System.Windows.Forms.MessageBox.Show("El archivo de exportación se encuentra en uso." + 13.Char() + "Por favor, cierre el archivo y vuelva a Exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //        else
        //        {
        //            System.Windows.Forms.MessageBox.Show(MigrationSupport.GlobalException.Instance.Number.ToStr() + 13.Char() + MigrationSupport.GlobalException.Instance.Description, "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }

        //    }
        //}
        
        public static string saca_caracter(ref string Cadena, string Caracter)
        {
            string saca_caracter = "";

            int i = 0;
            string sinformato = "";
            int pos = 0;
            sinformato = "";
            for (i = 1; i <= Cadena.Len(); i += 1)
            {
                pos = Cadena.InStr(Caracter, i, StringComparison.CurrentCulture);
                if (pos != 0)
                {
                    sinformato = Cadena.Mid(1, pos - 1) + Cadena.Mid((pos + 1), Cadena.Len());
                    Cadena = sinformato;
                }
                else
                {
                    saca_caracter = sinformato;
                    return saca_caracter;
                }
            }
            return saca_caracter;
        }
        public static int NoEsRut(string rut)
        {
            int NoEsRut = 0;

            string DvCal = "";
            int Es = 0;
            int suma = 0;
            int Aa = 0;
            string a = "";
            int i = 0;
            string b = "";
            string D_V = "";
            const string Son = "1234567890K";

            D_V = rut.Right(1);
            b = rut.Left((rut.Len() - 1));

            for (i = 1; i <= b.Len(); i += 1)
            {
                a = b.Right(i);
                Aa = (a.Left(1)).ToInt();

                if (i < 7)
                {
                    suma = suma + Aa * (i + 1);
                }
                else
                {
                    suma = suma + Aa * (i - 5);
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
                NoEsRut = true.ToInt();
            }

            return NoEsRut;
        }
        
        //public static void Limpia_GrillaDVG(System.Windows.Forms.Control grilla, MigrationSupport.UI.Form formulario)
        //{
        //    int J = 0;
        //    int i = 0;
        //    ((dynamic)grilla).Rows = 15;
        //    ((dynamic)formulario).Lbltitulo.Caption = "";
        //    ((dynamic)formulario).Lblcont.Caption = "";
        //    ((dynamic)formulario).Lbltitulo.Caption = "Limpiando Grilla.....";

        //    for (i = 1; i <= ((object)((dynamic)grilla).Rows()).ToInt() - 1; i += 1)
        //    {
        //        for (J = 0; J <= ((object)((dynamic)grilla).cols()).ToInt() - 1; J += 1)
        //        {
        //            ((dynamic)grilla).Row = i;
        //            ((dynamic)grilla).Col = J;
        //            grilla.Text = "";
        //        }
        //    }
        //    Modgri.SelPriRegGrilla(grilla);
        //    ((dynamic)grilla).Rows = 15;
        //    // grilla.Enabled = False
        //    ((dynamic)formulario).Lbltitulo.Caption = "";
        //}

        private static string FormaRut(string Dato, ref int Raya)
        {
            string FormaRut = "";

            int Este = 0;
            int Resto = 0;
            string vale = "";
            string valor = "";
            string a = "";
            int i = 0;
            int largo = 0;
            const string Que = "1234567890K";

            // obtenemos el largo de entrada
            largo = Dato.Len();

            // Desformatear el rut desplegado
            for (i = 1; i <= Dato.Len(); i += 1)
            {
                a = Dato.Mid(i, 1);
                if (Que.InStr(a, 1, StringComparison.CurrentCulture) > 0)
                {
                    valor = valor + a;
                }
            }

            // Extraemos los ceros iniciales
            while (valor.Left(1) == "0")
            {
                valor = valor.Right((valor.Len() - 1));
                Raya = Raya - 1;
            }

            // Preparamos la salida
            if (valor.Len() != 0)
            {
                vale = "-" + valor.Right(1);
                Resto = 2;
                for (i = valor.Len() - 1; i >= 1; i += -1)
                {
                    vale = valor.Mid(i, 1) + vale;
                    Este = ((int)Math.Floor((double)(vale.Len() - Resto) / 3)).ToInt();
                    if (Este == (vale.Len() - Resto) / 3 && Este != 0)
                    {
                        vale = "." + vale;
                        Resto = Resto + 1;
                    }
                }
            }
            if (vale.Len() > largo)
            {
                Raya = Raya + (vale.Len() - largo);
            }

            FormaRut = vale;

            return FormaRut;
        }
        //[STAThread]
        public static void main()
        {

            //if (MigrationSupport.Utils.AppPrevInstance())
            //{
            //    MigrationSupport.Utils.MsgBox("La aplicación ya se encuentra en ejecución y no puede ejecutar más copias de ella.", MODGPYF0.pito(16).Cast<MigrationSupport.MsgBoxStyle>(), "Aplicación En Ejecución");
            //    Environment.Exit(0);
            //}

            //if (MODGSRM.GetDatosSy() == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("No se pudo Leer los datos de Sybase.", "", MessageBoxButtons.OK);
            //    Environment.Exit(0);
            //}

            //if (MODGUSR.VerRegistroUsuario(true.ToInt()) != 0)
            //{
            //    Environment.Exit(0);
            //}

            //System.Windows.Forms.Application.Run(FrmPrincipal.DefInstance);

        }

        public static string transfecha(string fecha)
        {
            string transfecha = "";


            if (fecha.Mid(1, 3) == "Jan")
            {
                transfecha = "Ene" + fecha.Mid(4, fecha.Len());
            }
            else
            {
                if (fecha.Mid(1, 3) == "Apr")
                {
                    transfecha = "Abr" + fecha.Mid(4, fecha.Len());
                }
                else
                {
                    if (fecha.Mid(1, 3) == "Aug")
                    {
                        transfecha = "Ago" + fecha.Mid(4, fecha.Len());
                    }
                    else
                    {
                        if (fecha.Mid(1, 3) == "Dec")
                        {
                            transfecha = "Dic" + fecha.Mid(4, fecha.Len());
                        }
                        else
                        {
                            transfecha = fecha;
                        }
                    }
                }
            }
            return transfecha;
        }

        //public static string respuesta(string Comando)
        //{
        //    string respuesta = "";

        //    string s = "";
        //    double n = 0.0;
        //    string R = "";
        //    int x = 0;
        //    string m = "";
        //    string BaseSy = "";
        //    int intentos = 0;

        //    MODGSRM.RowCount = 0;
        //    intentos = 0;
        //    MODGSRM.ResQuery = "";
        //Consulta:
        //    intentos = intentos + 1;
        //    Comando = Comando.TrimB();
        //    BaseSy = MODGSRM.ParamSrm8k.Base.LCase().TrimB();
        //    BaseSy = BaseSy + new string(' ', 10 - BaseSy.Len());
        //    m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(MODGSRM.ParamSrm8k.APartirDe, "0000000") + "N" + BaseSy + Comando;
        //    MODGSRM.ParamSrm8k.mensaje = m;
        //    MODGSRM.ParamSrm8k.largo = m.Len();
        //    MODGSRM.ParamSrm8k.Status = "00";
        //    MODGSRM.ParamSrm8k.funcion = "01";
        //    MODGSRM.ParamSrm8k.Contexto = "00";

        //    // Akzio Migracion SYBASE - Inicio
        //    // Abril 2014
        //    Migrado.sBdd = MODGSRM.ParamSrm8k.mensaje.Mid(22, 10).TrimB();
        //    // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.NodoCCL$, ParamSrm8K.ServCCL$, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
        //    Migrado.Valida_Migracion(ref Migrado.sBdd, ref MODGSRM.ParamSrm8k.Servidor, ref MODGSRM.ParamSrm8k.Nodo);

        //    Migrado.resultado_log = Migrado.Escribe_log("E", "respuesta", MODGSRM.ParamSrm8k.Nodo, MODGSRM.ParamSrm8k.Servidor, MODGSRM.ParamSrm8k.mensaje);

        //    // Llamada SRM
        //    object argTemp1 = MODGSRM.ParamSrm8k.largo;
        //    x = MODGSRM.srmw8(MODGSRM.ParamSrm8k.Nodo, MODGSRM.ParamSrm8k.Servidor, MODGSRM.ParamSrm8k.mensaje, ref argTemp1, MODGSRM.ParamSrm8k.Status, MODGSRM.ParamSrm8k.funcion, MODGSRM.ParamSrm8k.Contexto, MODGSRM.ParamSrm8k.Control);
        //    // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.NodoCCL$, ParamSrm8K.ServCCL$, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
        //    MODGSRM.ParamSrm8k.Nodo = Migrado.sNodoOri;
        //    MODGSRM.ParamSrm8k.Servidor = Migrado.sApliOri;

        //    Migrado.resultado_log = Migrado.Escribe_log("S", x.Str(), "", "", MODGSRM.ParamSrm8k.mensaje);

        //    // Akzio Migracion SYBASE - Fin
        //    // 
        //    // x% = srmw8(ParamSrm8k.Nodo, ParamSrm8k.Servidor, ParamSrm8k.mensaje, ParamSrm8k.largo, ParamSrm8k.Status, ParamSrm8k.funcion, ParamSrm8k.Contexto, ParamSrm8k.Control)
        //    if (!(x == 0 && MODGSRM.ParamSrm8k.mensaje.Left(2) == "00"))
        //    {
        //        if (intentos <= 1)
        //        {
        //            goto Consulta;
        //        }
        //        respuesta = "-1";
        //        return respuesta;
        //    }
        //    R = MODGSRM.ParamSrm8k.mensaje.Mid(1, MODGSRM.ParamSrm8k.largo).TrimB();
        //    n = (R.Mid(4, 7)).ToVal();
        //    if (n > 0)
        //    {
        //        s = R.Right((R.Len() - 14));
        //    }
        //    else
        //    {
        //        s = "";
        //    }
        //    MODGSRM.ResQuery = MODGSRM.ResQuery + s;
        //    MODGSRM.RowCount = (MODGSRM.RowCount + n).ToInt();
        //    if (MODGSRM.ResQuery.Len() >= 30000)
        //    {
        //        MODGSRM.ParamSrm8k.APartirDe = (MODGSRM.ParamSrm8k.APartirDe + n).ToInt();
        //        Continua = R.Mid(3, 1);
        //        respuesta = MODGSRM.ResQuery;
        //        return respuesta;
        //    }
        //    if (R.Mid(3, 1) == "S")
        //    {
        //        MODGSRM.ParamSrm8k.APartirDe = (MODGSRM.ParamSrm8k.APartirDe + n).ToInt();
        //        Continua = "S";
        //        goto Consulta;
        //    }
        //    else
        //    {
        //        // ParamSrm8k.APartirDe = 1
        //        MODGSRM.ParamSrm8k.APartirDe = (MODGSRM.ParamSrm8k.APartirDe + n).ToInt();
        //        Continua = "N";
        //    }
        //    return MODGSRM.ResQuery;
        //}
        //public static string respuesta2(ref string Comando, string Nodo, string Servidor)
        //{
        //    string respuesta2 = "";

        //    string s = "";
        //    double n = 0.0;
        //    string R = "";
        //    int x = 0;
        //    string m = "";
        //    string BaseSy = "";
        //    int intentos = 0;

        //    MODGSRM.RowCount = 0;
        //    intentos = 0;
        //    MODGSRM.ResQuery = "";
        ////  ParamSrm8k.APartirDe = 1

        //Consulta:
        //    intentos = intentos + 1;
        //    Comando = Comando.TrimB();
        //    BaseSy = MODGSRM.ParamSrm8k.Base.LCase().TrimB();
        //    BaseSy = BaseSy + new string(' ', 10 - BaseSy.Len());
        //    m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(MODGSRM.ParamSrm8k.APartirDe, "0000000") + "N" + BaseSy + Comando;
        //    MODGSRM.ParamSrm8k.mensaje = m;
        //    MODGSRM.ParamSrm8k.largo = m.Len();
        //    MODGSRM.ParamSrm8k.Status = "00";
        //    MODGSRM.ParamSrm8k.funcion = "01";
        //    MODGSRM.ParamSrm8k.Contexto = "00";
        //    object argTemp1 = MODGSRM.ParamSrm8k.largo;
        //    x = MODGSRM.srmw8(Nodo, Servidor, MODGSRM.ParamSrm8k.mensaje, ref argTemp1, MODGSRM.ParamSrm8k.Status, MODGSRM.ParamSrm8k.funcion, MODGSRM.ParamSrm8k.Contexto, MODGSRM.ParamSrm8k.Control);
        //    if (!(x == 0 && MODGSRM.ParamSrm8k.mensaje.Left(2) == "00"))
        //    {
        //        if (intentos <= 1)
        //        {
        //            goto Consulta;
        //        }
        //        respuesta2 = "-1";
        //        return respuesta2;
        //    }
        //    R = MODGSRM.ParamSrm8k.mensaje.TrimB();
        //    // R$ = Mid$(ParamSrm8k.mensaje, 1, ParamSrm8k.largo)
        //    n = (R.Mid(4, 7)).ToVal();
        //    if (n > 0)
        //    {
        //        s = R.Right((R.Len() - 14));
        //    }
        //    else
        //    {
        //        s = "";
        //    }
        //    MODGSRM.ResQuery = MODGSRM.ResQuery + s;
        //    MODGSRM.RowCount = (MODGSRM.RowCount + n).ToInt();
        //    if (MODGSRM.ResQuery.Len() >= 30000)
        //    {
        //        respuesta2 = MODGSRM.ResQuery;
        //        return respuesta2;
        //    }
        //    if (R.Mid(3, 1) == "S")
        //    {
        //        MODGSRM.ParamSrm8k.APartirDe = (MODGSRM.ParamSrm8k.APartirDe + n).ToInt();
        //        Continua = "S";
        //        goto Consulta;
        //    }
        //    else
        //    {
        //        // ParamSrm8k.APartirDe = 1
        //        MODGSRM.ParamSrm8k.APartirDe = (MODGSRM.ParamSrm8k.APartirDe + n).ToInt();
        //        Continua = "N";
        //    }
        //    respuesta2 = MODGSRM.ResQuery;

        //    return respuesta2;
        //}
       
        public static string FormatoNeg(string Cadena)
        {
            string Retorno = "";

            if (Cadena != "0000000000000" && Cadena.InStr("-", 1, StringComparison.CurrentCulture) != 0)
            {
                Retorno = Cadena.Mid(Cadena.InStr("-", 1, StringComparison.CurrentCulture), Cadena.Len());
            }
            else
            {
                Retorno = Cadena;
            }

            return Retorno;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    
    }
}
