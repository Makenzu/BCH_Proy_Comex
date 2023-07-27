using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using VBNET = Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.IO;
//using iTextSharp.text;
using BCH.Comex.Core.BL.XEGI.Modulos;
using System.Data.Common;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Core.Entities.Cext01;

namespace BCH.Comex.Core.BL.XEGI.Forms
{
    public partial class ServxDoc : System.Windows.Forms.Form
    {
        private bool _disposed = false;
        public ServxDoc()
        {
            if (m_vb6FormDefInstance == null)
            {
                m_vb6FormDefInstance = this;
            }
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            MigrationSupport.Utils.AddNewForm(this);
            if (DesignMode) return;
            Form_Load(null, null);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            _disposed = true;
            base.Dispose(disposing);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.Run(new ServxDoc());
        }
        // Busca al ejecutivo que tiene asignado el participante, en las diferentes tablas
        public string busejc_party(UnitOfWorkCext01 uow, string codcct, string copro, string coesp, string coofi, string coope)
        {
            string busejc_party = "";

            string RutUsado = "";
            object ParamSrmK = null;
            List<object> R = null;
            string Que = "";

            /*string ResultadoQuery = "";
            // On Error GoTo SyGet_UsrErr

            Que = "";
            Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "Sce_doc_s01";
            Que = Que.LCase();
            Que = Que + MODGSYB.dbcharSy(cencos) + "," + MODGSYB.dbcharSy(copro) + "," + MODGSYB.dbcharSy(coesp) + ",";
            Que = Que + MODGSYB.dbcharSy(coofi) + "," + MODGSYB.dbcharSy(coope);


            R = MODGSRM.RespuestaQuery(ref Que);*/

            var result = uow.SceRepository.Sce_doc_s01(
                MODGSYB.dbcharSy(codcct),
                MODGSYB.dbcharSy(copro),
                MODGSYB.dbcharSy(coesp),
                MODGSYB.dbcharSy(coofi),
                MODGSYB.dbcharSy(coope)
                );

            /*if (R == "-1")
            {
                MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de buscar los datos de los participantes. El servidor reporta :[" + ((object)((dynamic)ParamSrmK).Mensaje()).Str().TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                   MigrationSupport.MsgBoxStyle>(), MODGRNG.MsgRng);     //  GoTo busejc_party
                return busejc_party;
                busejc_party = " ";
            }

            if (R == "")
            {
                System.Windows.Forms.MessageBox.Show("No se ha encontrado el Ejecutivo de negocios en la tabla SGT del banco con el rut [" + Module1.rutiparty + "] del cliente en el nodo [" + MODGSRM.ParamSrm8k.Nodo.TrimB() + "] ", "", MessageBoxButtons.OK);
                busejc_party = " ";
                return busejc_party;
            }*/

            RutUsado = result[0];
            //RutUsado = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R[0].ToString()).ToString();            
            RutUsado = MigrationSupport.Utils.Format(MODGDOC.Componer(RutUsado, "~", " ").TrimB(), "0000000000");

            busejc_party = RutUsado;


            return busejc_party;
        }
        public string sacar_direccion(XegiService service, string rut_eje)
        {
            string sacar_direccion = "";

            string TitEjc = "";
            string R = "";
            /*string Que = "";
            Que = "";
            Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_usr_s25";
            Que = Que.LCase();
            Que = Que + MODGSYB.dbcharSy(rut_eje);

            // 
            //     Que$ = ""
            //     Que$ = Que$ + "select direccion from sce_usr"
            //     Que$ = Que$ + " where rut = " + dbcharSy(rut_eje)
            R = MODGSRM.RespuestaQuery(ref Que);*/

            R = service.sce_usr_s25(MODGSYB.dbcharSy(rut_eje));

            if (R == "-1")
            {
                MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer la Tabla Sce_usr", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), TitEjc);
                //   GoTo sacar_direccionErr
                return sacar_direccion;
            }

            // Resultado nulo de la Consulta.-
            if (R == "")
            {
                MigrationSupport.Utils.MsgBox("No se han encontrado datos en la Tabla Sce_Usr", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "");
                return sacar_direccion;
                // GoTo sacar_direccionEnd
            }

            return MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str();
        }
        public double SyGet_Ini(XegiService service, string grupo, string elem, int flgAviso)
        {
            double SyGet_Ini = 0.0;

            string MsgCom = "";
            List<sce_ini_s01_Result> R = null;
            string Query = "";

            string tipo = "";
            string valor = "";
            int largo = 0;
            int decim = 0;


            try
            {

                /*Query = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_ini_s01 ";
                Query = Query + MODGSYB.dbcharSy(grupo) + ",";
                Query = Query + MODGSYB.dbcharSy(elem);
                Query = Query.LCase();
                R = MODGSRM.RespuestaQuery(ref Query);*/
                /*if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer la tabla sce_ini. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MsgCom);
                    return SyGet_Ini;
                }

                // Resultado nulo de la Consulta.-
                // -----------------------------
                if (R == "")
                {
                    if (flgAviso != 0)
                    {
                        MigrationSupport.Utils.MsgBox("No existe en tabla sce_ini valor para [" + grupo + ", " + elem + "]" + ".", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "Tabla sce_ini");
                    }
                    return SyGet_Ini;
                }*/

                /*tipo = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str();
                largo = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                decim = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToInt();
                valor = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();*/

                R = service.sce_ini_s01(MODGSYB.dbcharSy(grupo), MODGSYB.dbcharSy(elem));
                if (R != null)
                {
                    tipo = R[0].tipov;
                    largo = Convert.ToInt32(R[0].largo);
                    decim = Convert.ToInt32(R[0].decim);
                    valor = R[0].valor;
                }

                if (tipo == "N")
                {
                    SyGet_Ini = valor.ToVal();
                }
                else if (tipo == "C")
                {
                    SyGet_Ini = valor.ToDbl();
                }

                return SyGet_Ini;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }
        public int GetDatosSgt2()
        {
            int GetDatosSgt2 = 0;

            string MsgSgt = "";

            try
            {

                // ------------------------------------------------------------------------
                // Se leen los parámetros para trabajar con Sgt.-
                // ------------------------------------------------------------------------
                Module1.ParamSgt.Nodo = MODGDOC.GetSceIni("SgtCliEjc", "Nodo");
                if (Module1.ParamSgt.Nodo == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Nodo, para tabla SGT Cliente Especialista . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                       MsgSgt);
                    throw new System.Exception();
                }

                Module1.ParamSgt.SerLee = MODGDOC.GetSceIni("SgtCliEjc", "SerLee");
                if (Module1.ParamSgt.SerLee == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Servidor de Lectura , para tabla SGT Cliente Especialista . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgSgt);
                    throw new System.Exception();
                }
                Module1.ParamSgt.SerGra = MODGDOC.GetSceIni("SgtCliEjc", "SerGra");
                if (Module1.ParamSgt.SerGra == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Servidor de Grabado , para tabla SGT Cliente Especialista . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgSgt);
                    throw new System.Exception();
                }

                Module1.ParamSgt.VisLee = MODGDOC.GetSceIni("SgtCliEjc", "VisLee");
                if (Module1.ParamSgt.VisLee == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Vista de Lectura, para tabla SGT Cliente Especialista. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgSgt);
                    throw new System.Exception();
                }
                Module1.ParamSgt.VisGra = MODGDOC.GetSceIni("SgtCliEjc", "VisGra");
                if (Module1.ParamSgt.VisGra == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Vista de Grabado, para tabla SGT Cliente Especialista. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgSgt);
                    throw new System.Exception();
                }

                Module1.ParamSgt.VisEli = MODGDOC.GetSceIni("SgtCliEjc", "VisEli");
                if (Module1.ParamSgt.VisEli == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Vista de Eliminación, para tabla SGT Cliente Especialista. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgSgt);
                    throw new System.Exception();
                }

                Module1.ParamSgt.VisClt = MODGDOC.GetSceIni("SgtCliEjc", "VisClt");
                if (Module1.ParamSgt.VisClt == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Vista de Lectura, para tabla SGT Cliente . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                       MsgSgt);
                    throw new System.Exception();
                }

                // 

                GetDatosSgt2 = true.ToInt();

                return GetDatosSgt2;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }

        public int Habil_SGTCliEje(XegiService service)
        {
            int Habil_SGTCliEje = 0;

            //  D.S.B.
            int habil = 0;

            habil = false.ToInt();

            habil = SyGet_Ini(service, "sgtclieje", "xx", false.ToInt()).ToInt();

            Habil_SGTCliEje = habil;


            return Habil_SGTCliEje;
        }
        public void lee_ejecutivosSy(string codigo)
        {
            int k = 0;
            int fin = 0;
            int i = 0;
            int n = 0;
            string TitEjc = "";
            string R = "";
            string Que = "";

            Module1.ejecutivos = new Module1.tipo_riesgo[1];
            Que = "";
            Que = Que + "select ejc_ejccod, ejc_ejcnom, ejc_ejctel, ejc_ejcfax  from " + MODGSRM.ParamSrm8k.base_migname + ".";
            Que = Que + MODGSRM.ParamSrm8k.usuario + ".sgt_ejc ";
            Que = Que + " where ejc_ejcofi = " + MODGSYB.dbnumesy(((int)(codigo.ToVal())));
            Que = Que + " order by ejc_ejccod";

            R = MODGSRM.RespuestaQuery(ref Que);

            if (R == "-1")
            {
                MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer la Tabla Sce_Ejc", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), TitEjc);
                goto Lee_ejcSyErr;
            }

            // Resultado nulo de la Consulta.-
            if (R == "")
            {
                MigrationSupport.Utils.MsgBox("No se han encontrado datos en la Tabla Sce_Ejc", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "");
                return;
            }

            n = MODGSRM.RowCount;

            for (i = 0; i <= n - 1; i += 1)
            {
                fin = -1;
                MigrationSupport.Utils.ResumeNext(() =>
                {
                    fin = Module1.ejecutivos.GetUpperBound(0);
                    if (fin != -1)
                    {
                        if (Module1.ejecutivos[0].nombre == "")
                        {
                            k = 0;
                        }
                        else
                        {
                            k = fin + 1;
                            Array.Resize(ref Module1.ejecutivos, k + 1);
                        }
                    }
                    else
                    {
                        k = 0;
                        Module1.ejecutivos = new Module1.tipo_riesgo[k + 1];
                    }

                    Module1.ejecutivos[k].codigo = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str();
                    Module1.ejecutivos[k].nombre = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                    Module1.ejecutivos[k].telefono = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str();
                    Module1.ejecutivos[k].Fax = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str();
                    // ejecutivos(k%).email = GetPosSy(NumSig(), "C", R$)
                    R = MODGSRM.NuevaRespuesta(4, R);
                });
            }

            return;

        Lee_ejcSyErr:
            System.Windows.Forms.MessageBox.Show("Error", "", MessageBoxButtons.OK);

        }
        public string RespuestaSgt2(string Nodo, string RutCons, string Servidor, string Vista, ref string Tabla, string llave, string Oper)
        {
            string RespuestaSgt2 = "";

            string s = "";
            string R = "";
            int x = 0;
            string m = "";


            int Intentos = 0;
            MODGSRM.RowCount = 0;
            Intentos = 0;

        Conssgt2:
            Intentos = Intentos + 1;
            Tabla = Tabla.UCase();
            m = RutCons + Vista.TrimB() + Oper.TrimB() + Tabla.TrimB() + llave;
            Module1.ParamSgt.Mensaje = m;
            Module1.ParamSgt.largo = m.Len();
            Module1.ParamSgt.Status = "00";
            Module1.ParamSgt.Funcion = "08";
            Module1.ParamSgt.Contexto = "00";
            object argTemp1 = Module1.ParamSgt.largo;
            x = MODGSRM.srmw8(Nodo, Servidor, Module1.ParamSgt.Mensaje, ref argTemp1, Module1.ParamSgt.Status, Module1.ParamSgt.Funcion, Module1.ParamSgt.Contexto, Module1.ParamSgt.Control);
            if (!(x == 0 && Module1.ParamSgt.Mensaje.Left(2) == "00"))
            {
                if (Module1.ParamSgt.Mensaje.Left(2) == "96")
                {
                    RespuestaSgt2 = "";
                    return RespuestaSgt2;
                }

                if (Intentos <= 1)
                {
                    goto Conssgt2;
                }
                RespuestaSgt2 = "-1";
                return RespuestaSgt2;
            }
            R = Module1.ParamSgt.Mensaje.TrimB();

            s = R.Mid(3, R.Len() - 2);

            RespuestaSgt2 = s;


            return RespuestaSgt2;
        }
        public int Es_Cliente(string rutcli)
        {
            int Es_Cliente = 0;

            string R = "";

            string rut = "";
            int fin = 0;
            int i = 0;
            int sig = 0;
            int h = 0;

            try
            {

                rut = "12345678";
                R = "";
                string argTemp1 = "";
                R = RespuestaSgt2(Module1.ParamSgt.Nodo, rut, Module1.ParamSgt.SerLee, Module1.ParamSgt.VisClt, ref argTemp1, rutcli, "V0009");

                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Hay problemas de comunicación con el SRM o bien con la vista L001. No se podrá rescatar el nombre del ejecutivo.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "Atención");
                    return Es_Cliente;
                }

                if (R == "")
                {
                    Es_Cliente = false.ToInt();
                }
                else
                {
                    //  Es Cliente
                    Es_Cliente = true.ToInt();
                }

                // 

                return Es_Cliente;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Es_Cliente = false.ToInt();
                throw new XegiException("Error al Actualizar en tablas SGT : [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }
        public int Lee_SgtCliEsp(string rutcli)
        {
            int Lee_SgtCliEsp = 0;

            string R = "";


            string rut = "";
            int fin = 0;
            int i = 0;
            int sig = 0;
            int h = 0;

            try
            {

                // ParamSgt

                rut = "12345678";
                R = "";
                string argTemp1 = "";
                R = RespuestaSgt2(Module1.ParamSgt.Nodo, rut, Module1.ParamSgt.SerLee, Module1.ParamSgt.VisLee, ref argTemp1, rutcli, "V0009");

                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Hay problemas de comunicación con el SRM o bien con la vista L023. No se podrá rescatar el nombre del ejecutivo.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "Atención");
                    return Lee_SgtCliEsp;
                }


                Module1.RSGTCliEsp = new Module1.CliEsp[1];
                Module1.VSGTCliEsp = new Module1.CliEsp[1];

                if (R != "")
                {
                    fin = (R.Mid(2, 4)).ToInt();

                    R = R.Mid(6, R.Len() - 5);
                }

                h = 0;

                sig = 0;

                for (i = 1; i <= fin; i += 1)
                {

                    if (R.Mid((14 + sig), 2) == Module1.SGT_tipopimp || R.Mid((14 + sig), 2) == Module1.SGT_tipopexp || R.Mid((14 + sig), 2) == Module1.SGT_tipnegoc)
                    {
                        h = h + 1;
                        Array.Resize(ref Module1.RSGTCliEsp, h + 1);
                        Array.Resize(ref Module1.VSGTCliEsp, h + 1);
                        Module1.RSGTCliEsp[h].nrut = R.Mid((5 + sig), 9);
                        Module1.RSGTCliEsp[h].tipo = R.Mid((14 + sig), 2);
                        Module1.RSGTCliEsp[h].ofieje = R.Mid((16 + sig), 3);
                        Module1.RSGTCliEsp[h].codeje = R.Mid((19 + sig), 3);
                        Module1.RSGTCliEsp[h].feccre = R.Mid((22 + sig), 8);
                        Module1.RSGTCliEsp[h].rutope = R.Mid((30 + sig), 9);
                        Module1.RSGTCliEsp[h].drutope = R.Mid((39 + sig), 1);
                        Module1.RSGTCliEsp[h].filler = R.Mid((40 + sig), 35);
                        Module1.RSGTCliEsp[h].borrar = false.ToInt();

                        Module1.VSGTCliEsp[h] = Module1.RSGTCliEsp[h];

                    }
                    sig = sig + 74;

                }

                Lee_SgtCliEsp = true.ToInt();

                return Lee_SgtCliEsp;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Lee_SgtCliEsp = false.ToInt();
                throw new XegiException("Error al Actualizar en tablas SGT : [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }

        public bool Trae_rut_party()
        {
            bool Trae_rut_party = false;

            bool Leyo_Ok = false;
            int i = 0;
            int n = 0;
            string MsgUsr1 = "";
            string R = "";
            string Que = "";

            try
            {

                Que = "select  ";
                Que = Que + " where ejc_tpo = " + MODGSYB.dbcharSy(Module1.EJCOPIMP) + " OR ";
                Que = Que + " ejc_tpo = " + MODGSYB.dbcharSy(Module1.EJCOPEXP) + " OR ";
                Que = Que + " ejc_tpo = " + MODGSYB.dbcharSy(Module1.EJCNEGOC);

                Que = Que.LCase();

                R = MODGSRM.RespuestaQuery(ref Que);
                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer . El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(Module1.MB_ICONEXCLAMATION).Cast<MigrationSupport.MsgBoxStyle
                       >(), MsgUsr1);
                    return Trae_rut_party;
                }

                // Resultado nulo de la Consulta.-
                if (R == "")
                {
                    return Trae_rut_party;
                }

                n = MODGSRM.getocurrs();
                Module1.VEjc = new Module1.T_Especialista[n + 1];
                for (i = 1; i <= n; i += 1)
                {
                    Module1.VEjc[i].codofi = MigrationSupport.Utils.Format(MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R), "000");
                    Module1.VEjc[i].codejc = MigrationSupport.Utils.Format(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R), "000");
                    Module1.VEjc[i].rut = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                    Module1.VEjc[i].nombre = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                    Module1.VEjc[i].tipo = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                    R = MODGSRM.NuevaRespuesta(5, R);
                }

                Trae_rut_party = true;

                return Trae_rut_party;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Leyo_Ok = false;
                throw new XegiException("Error al leer en Sds_Sgt_Ejc: [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }

        private void Co_Documentos_Click(object sender, EventArgs e)
        {
            //int x = 0;
            //string s = "";
            //int bien = 0;
            //string codope = "";
            //string codofi = "";
            //string CodEsp = "";
            //string codpro = "";
            //string centcos = "";
            //int largo_total = 0;
            //string texto_largo = "";
            //string ope = "";
            //string doc = "";
            //string cor = "";
            //object primero = null;
            //object segundo = null;
            //object tercero = null;
            //int cuarto = 0;

            //ope = operacion.Text.TrimB();
            //doc = documento.Text.TrimB();
            //cor = correlativo.Text.TrimB();
            //// ****************************************** modificado por MPN
            //centcos = ope.Mid(1, 3);
            //codpro = ope.Mid(4, 2);
            //CodEsp = ope.Mid(6, 2);
            //codofi = ope.Mid(8, 3);
            //codope = ope.Mid(11, 5);

            //bien = MODGSRM.GetDatosSy();

            //// CFV-10/11/2006
            //// Llenar variable global declarada
            //MODFRA.CENTRO_COSTO = centcos;
            //// CFV
            //XegiService service = new XegiService();

            //Module1.rutiparty = busejc_party(service, centcos, codpro, CodEsp, codofi, codope);

            //// CFV-10/11/2006
            //// Se comenta busqueda de ejecutivo
            //if (Module1.rutiparty != "")
            //{
            //    // lee_ejecutivosSy Val(codofi$)
            //    // bien% = GetDatosSgtEjc()
            //    // If bien% Then
            //    //    x% = Sygetn_Ejecutivos()
            //    // End If
            //    // bien% = GetDatosSy()
            //}
            //// CFV

            //while (Module1.rutiparty.Len() <= 9)
            //{
            //    Module1.rutiparty = "0" + Module1.rutiparty;
            //}

            //MODXDOC.Memo = "1" + 9.Char() + operacion.Text.TrimB() + 9.Char() + documento.Text.TrimB() + 9.Char() + correlativo.Text.TrimB();     //  + Chr(9) + "0" + Chr(9) + "0" + Chr(9) + "-1" + Chr(9) + "0"
            //s = MODXDOC.Memo.TrimB();

            ////x = ProcesaComando(service, s);
            //MODXDOC.UsrEsps = new MODXDOC.EstrucUsuarios[1];
        }
        private void Co_Salir_Click(object sender, EventArgs e)
        {

            Environment.Exit(0);

        }
        private void correlativo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int KeyAscii;
            KeyAscii = (int)e.KeyChar;

            if (KeyAscii == 13)
            {
                impresora.Focus();
                e.Handled = true;
            }

        }
        private void documento_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int KeyAscii;
            KeyAscii = (int)e.KeyChar;

            if (KeyAscii == 13)
            {
                correlativo.Focus();
                e.Handled = true;
            }

        }
        private void Form_LinkExecute(object sender, EventArgs e)
        {
            //string CmdStr = "";//TODO ARKANO 
            ////TODO ARKANO  CmdStr = e.CmdStr;
            //int x = 0;
            //int bien = 0;
            //string codope = "";
            //string codofi = "";
            //string CodEsp = "";
            //string codpro = "";
            //string centcos = "";
            //string dat = "";
            //string s = "";
            //s = CmdStr.TrimB();
            //s = s.Left((s.Len() - 1));
            //s = s.Right((s.Len() - 1));

            //XegiService service = new XegiService();
            //if (s.Len() > 3)
            //{
            //    dat = s.Left((s.Len() - 6));
            //    dat = dat.Right((dat.Len() - 3));
            //    // ****************************************** modificado por MPN
            //    centcos = dat.Mid(1, 3);
            //    codpro = dat.Mid(4, 2);
            //    CodEsp = dat.Mid(6, 2);
            //    codofi = dat.Mid(8, 3);
            //    codope = dat.Mid(11, 5);

            //    bien = MODGSRM.GetDatosSy();

            //    // CFV-10/11/2006
            //    // Llenar variable global declarada
            //    MODFRA.CENTRO_COSTO = centcos;
            //    // CFV
            //    Module1.rutiparty = busejc_party(service, centcos, codpro, CodEsp, codofi, codope);

            //    // CFV-10/11/2006
            //    // Se comenta busqueda del ejecutivo
            //    if (Module1.rutiparty != "")
            //    {
            //        // lee_ejecutivosSy Val(codofi$)
            //        // bien% = GetDatosSgtEjc()
            //        // If bien% Then
            //        //    x% = Sygetn_Ejecutivos()
            //        // End If
            //        // bien% = GetDatosSy()
            //    }
            //    // CFV

            //    while (Module1.rutiparty.Len() <= 9)
            //    {
            //        Module1.rutiparty = "0" + Module1.rutiparty;
            //    }
            //}

            ////x = ProcesaComando(service, s);

            //if (x != 0)
            //{
            //    Text1.Text = "00";
            //}
            //else
            //{
            //    Text1.Text = "01Operación con errores";
            //}

        }
        private void Form_Load(object sender, EventArgs e)
        {
            // Akzio
            // ServxDoc.Top = -4000
            // ServxDoc.Left = -4000
            // 
            //     ServxDoc.Top = 5000
            //     ServxDoc.Left = 5000
            // 
            // 
            // fin akzio
            // 
            // 
            //  creado por D.S.B.
            //  Colocado por MPN
            Module1.Hab_SGTCliEje = Habil_SGTCliEje(new XegiService());//nueva instancia???
            if (Module1.Hab_SGTCliEje != 0)
            {
                if (GetDatosSgt2() == 0)
                {
                    Environment.Exit(0);     // Datos básicos para Sybase.-
                }     // Datos básicos para Sybase.-
            }


        }
        private void impresora_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int KeyAscii;
            KeyAscii = (int)e.KeyChar;

            if (KeyAscii == 13)
            {
                Co_Documentos.Focus();
                e.Handled = true;
            }

        }
        private void ImprimeFirma()
        {
            int i = 0;
            int n = 0;

            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(MigrationSupport.Printer.DefInstance.Font, "Times New Roman");
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, true);
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 9.84F);
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
            if (MigrationSupport.Printer.DefInstance.CurrentY.ToVal() < 10)
            {
                n = (14 - MigrationSupport.Printer.DefInstance.CurrentY.ToVal()).ToInt();
            }
            else
            {
                n = (22 - MigrationSupport.Printer.DefInstance.CurrentY.ToVal()).ToInt();
            }
            for (i = 1; i <= n; i += 1)
            {
                MigrationSupport.Printer.DefInstance.CurrentY = ((int)(MigrationSupport.Printer.DefInstance.CurrentY + 1));
            }            
            //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(80),"_______________________"});
            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(83),"Banco de Chile"});

        }
        private void operacion_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int KeyAscii;
            KeyAscii = (int)e.KeyChar;

            if (KeyAscii == 13)
            {
                documento.Focus();
                e.Handled = true;
            }

        }
        // ****************************************************************************
        //    1.  Detalle de los Cargos y Abonos.
        // ****************************************************************************
        private void Pr_Abonos(DocumentoReporteModel documento)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int b = MODXDOC.VxOri.Length;
            int a = MODXDOC.VxVia.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    a = MODXDOC.VxVia.GetUpperBound(0);
            //    b = MODXDOC.VxOri.GetUpperBound(0);
            //});

            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
            //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(40),"Cargos y/o Abonos"});
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
            MODXDOC.Pr_Salto_Pagina();

            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();

            s = "En consecuencia efectuamos los débitos y/o abonos que se detallan:";

            //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
            //TODO: Hay que revisar por qué las Lineas de MODXDOC llegan en null
            //for (i = 0; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            //{
            //    if (MODXDOC.Lineas[i].TrimB() != "")
            //    {
            //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
            //        MODXDOC.Pr_Salto_Pagina();
            //    }
            //}

            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            for (i = 0; i < a; i += 1)
            {
                var cargoAbono = new CargoAbono { 
                    DescripcionVia = MODXDOC.VxVia[i].Descri.TrimB(), 
                    NombreVia = MODXDOC.VxVia[i].NomVia.TrimB(), 
                    Moneda = MODXDOC.VxVia[i].NemMon.TrimB(), 
                    Monto = MODGDOC.forma(MODXDOC.VxVia[i].MtoTot, MODXDOC.Formato) 
                };
                documento.LineasCargoAbono.Add(cargoAbono);
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(7).Column, MODXDOC.VxVia[i].Descri.TrimB());
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(30).Column, MODXDOC.VxVia[i].NomVia.TrimB());
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(85).Column, MODXDOC.VxVia[i].NemMon.TrimB());
                //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(MODXDOC.VxVia[i].MtoTot,MODXDOC.Formato)});
                MODXDOC.Pr_Salto_Pagina();
            }
            for (i = 0; i < b; i += 1)
            {
                var cargoAbono = new CargoAbono
                {
                    DescripcionVia = MODXDOC.VxOri[i].Descri.TrimB(),
                    NombreVia = MODXDOC.VxOri[i].NomOri.TrimB(),
                    Moneda = MODXDOC.VxOri[i].NemMon.TrimB(),
                    Monto = MODGDOC.forma(MODXDOC.VxOri[i].MtoTot, MODXDOC.Formato)
                };
                documento.LineasCargoAbono.Add(cargoAbono);
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(7).Column, MODXDOC.VxOri[i].Descri.TrimB());
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(30).Column, MODXDOC.VxOri[i].NomOri.TrimB());
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(85).Column, MODXDOC.VxOri[i].NemMon.TrimB());
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(MODXDOC.VxOri[i].MtoTot,MODXDOC.Formato)});
                MODXDOC.Pr_Salto_Pagina();
            }

        }
        // ****************************************************************************
        //    1.  Detalle de la Cuenta Corriente para la carta de Exportador.
        // ****************************************************************************
        private void Pr_Cta_Cte(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta)
        {
            string MndAnt = "";
            int Contador_Arreglo = 0;
            int x = 0;
            int i = 0;
            int c = 0;
            string s = "";
            bool lugar = false;
            int n = MODXDOC.VDet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDet.GetUpperBound(0);
            //});

            // Printer.Print : Call Pr_Salto_Pagina
            // Printer.Print : Call Pr_Salto_Pagina

            lugar = true;
            // Pago Directo Cobranza Export.(610)  - 'Planillas Visible Export.(613)
            if (Carta == MODXDOC.DocxPagDir)
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                s = MODFRA.SyGet_Fra(uow, 6701, "E", "");
                // s$ = "En consecuencia de lo anterior hemos efectuado los débitos que se detallan:"
                // Cancelación Cobranza Export. - Cancelación Retorno Export.
            }
            else if (Carta == MODXDOC.DocxCobCan || Carta == MODXDOC.DocxCanRet)
            {
                s = MODFRA.SyGet_Fra(uow, 6702, "E", "");
                // s$ = "En consecuencia efectuamos los abonos y/o débitos que se detallan:"
            }
            else if (Carta == MODXDOC.DocxCobReg)
            {
                c = 0;
                for (i = 0; i < n; i += 1)
                {
                    if (MODXDOC.VDet[i].tipo.TrimB() == "D")
                    {
                        c = c + 1;
                    }
                }
                if (c > 0)
                {
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
                    MigrationSupport.Printer.DefInstance.Print();
                    MODXDOC.Pr_Salto_Pagina();
                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(MODXDOC.tab_titulos)),"Cargos"});

                    //MigrationSupport.Printer.DefInstance.PrintList(new object[] { MigrationSupport.FileSystem.TAB((short)(MODXDOC.tab_titulos)), "Cargos" });
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
                    MODXDOC.Pr_Salto_Pagina();
                    MigrationSupport.Printer.DefInstance.Print();
                    MODXDOC.Pr_Salto_Pagina();
                }
                lugar = false;
            }

            if (lugar)
            {
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                /*for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                {
                    if (MODXDOC.Lineas[i].TrimB() != "")
                    {
                        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                        MODXDOC.Pr_Salto_Pagina();
                    }
                }*/
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
            }

            Contador_Arreglo = 0;
            for (i = 0; i < n; i += 1)
            {
                if (MODXDOC.VDet[i].tipo.TrimB() == "D")
                {
                    Contador_Arreglo = i;
                    var comisionGasto = new ComisionGasto { Tipo = "D", TipoOperacion = MODXDOC.VDet[i].Glosa, Moneda = MODXDOC.VDet[i].MonDet, Monto = MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(), MODXDOC.Formato) };
                    documento.LineasComisionGasto.Add(comisionGasto);
                    //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MODXDOC.VDet[i].Glosa,MigrationSupport.FileSystem.TAB(79),MODXDOC.VDet[i].MonDet,MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(),MODXDOC.Formato)});
                    MODXDOC.Pr_Salto_Pagina();
                }
                else if (MODXDOC.VDet[i].tipo != null)
                {
                    break;
                }
            }

            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            if (Contador_Arreglo < n)
            {
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Print("Detalle comisiones y gastos cobrados:");
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();

                MndAnt = "";
                for (i = Contador_Arreglo + 1; i < n; i += 1)
                {
                    var comisionGasto = new ComisionGasto();
                    if (MODXDOC.VDet[i].tipo.TrimB() == "C")
                    {
                        comisionGasto.Tipo = "C";
                        if (MndAnt != "" && MODXDOC.VDet[i].MonDet != MndAnt)
                        {
                            MigrationSupport.Printer.DefInstance.Print();
                            MODXDOC.Pr_Salto_Pagina();
                            comisionGasto.Moneda = MODXDOC.VDet[i].MonDet;
                            MndAnt = MODXDOC.VDet[i].MonDet;
                        }
                        else
                        {
                            comisionGasto.Moneda = MODXDOC.VDet[i].MonDet;
                            MndAnt = MODXDOC.VDet[i].MonDet;
                        }
                        if (MODXDOC.VDet[i].Monto.ToVal() > 0)
                        {
                            comisionGasto.TipoOperacion = MODXDOC.VDet[i].Glosa;
                            comisionGasto.Moneda = MODXDOC.VDet[i].MonDet;
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MODXDOC.VDet[i].Glosa, MigrationSupport.FileSystem.TAB(79).Column, MODXDOC.VDet[i].MonDet);
                        }
                        if (i == n)
                        {
                            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
                            if (MODXDOC.VDet[i].Monto.ToVal() > 0)
                            {
                                comisionGasto.Monto = MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(), MODXDOC.Formato);
                                //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(),MODXDOC.Formato)});
                            }
                            else
                            {
                                comisionGasto.Monto = new string(' ', 35);
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(88),new string(' ',35)});
                            }
                            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
                        }
                        else
                        {
                            if (MODXDOC.VDet[i].Monto.ToVal() > 0)
                            {
                                comisionGasto.Monto = MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(), MODXDOC.Formato);
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.VDet[i].Monto.ToVal(),MODXDOC.Formato)});
                            }
                        }
                        if (i < n)
                        {
                            MODXDOC.Pr_Salto_Pagina();
                        }
                        else
                        {
                            MODXDOC.Pr_SaltoPag();
                        }
                    }
                    else
                    {
                        break;
                    }
                    documento.LineasComisionGasto.Add(comisionGasto);
                }

                // Printer.Print : Call Pr_SaltoPag
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(65).Column, "Total");
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                documento.MonedaTotal = MODXDOC.MonTotal;
                documento.TotalComisionGasto = MODGDOC.forma(MODXDOC.MtoTotal.ToVal(), MODXDOC.Formato);
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(79),MODXDOC.MonTotal,MigrationSupport.FileSystem.TAB(88),MODGDOC.forma(MODXDOC.MtoTotal.ToVal(),MODXDOC.Formato)});
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();

            }

        }
        // ****************************************************************************
        //    1.  Despliega la información general para este tipo de carta.
        // ****************************************************************************
        private void Pr_Informacion(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            string s3 = "";
            string s2 = "";
            string s1 = "";
            string s0 = "";
            string p = "";
            int n = 0;

            MigrationSupport.Utils.ResumeNext(() =>
            {
                n = MODXDOC.VDet.GetUpperBound(0);
            });

            if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobCan || MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCanRet)
            {
                n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 1).ToInt();
                if (n > 0)
                {
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 2);
                    s0 = MODFRA.SyGet_Fra(uow, n, "E", p) + 13.Char() + 10.Char();
                }
                else
                {
                    s0 = "";
                }
                if (MODXDOC.VInf.InfMto1.ToVal() > 0)
                {
                    s1 = "Moneda extranjera liquidada: " + MODXDOC.VInf.InfMon1.TrimB() + " " + MODGDOC.forma(MODXDOC.VInf.InfMto1.ToVal(), MODXDOC.Formato).TrimB() + " al tipo de cambio " + MODXDOC.VInf.InfMon2.TrimB() + " " +
                       MODGDOC.forma(MODXDOC.VInf.InfMto2.ToVal(), MODXDOC.Formato).TrimB() + "  =  " + MODXDOC.VInf.InfMon3.TrimB() + " " + MODGDOC.forma(MODXDOC.VInf.InfMto3.ToDbl(), MODXDOC.Formato).TrimB() + 13.Char() + 10.Char();
                }
                if (MODXDOC.FraseVa != 0)
                {
                    s2 = MODFRA.SyGet_Fra(uow, 6703, "E", "") + 13.Char() + 10.Char() + 13.Char() + 10.Char();
                    // s2$ = "Para el retorno creado, sírvase hacernos llegar sus instrucciones para efectuar el pago del mismo, adjuntando la correspondiente declaración de exportación, si procede." + Chr(13) + Chr(10) + Chr(13) + Chr(10)
                }
                else
                {
                    s2 = "";
                }
                if (MODXDOC.Instrucciones.TrimB() != "")
                {
                    s3 = MODXDOC.Instrucciones + 13.Char() + 10.Char();
                }
                else
                {
                    s3 = "";
                }
                s = s0 + s1 + s2 + s3;
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegRet)
            {
                s1 = MODFRA.SyGet_Fra(uow, 6704, "E", "") + 13.Char() + 10.Char() + 13.Char() + 10.Char();
                // s1$ = "A su más pronta conveniencia, sírvase hacernos llegar sus instrucciones para efectuar el pago de este retorno, adjuntando la correspondiente declaración de exportación, si procede." + Chr(13) + Chr(10) + Chr(13) + Chr(10)
                if (MODXDOC.Instrucciones.TrimB() != "")
                {
                    s2 = MODXDOC.Instrucciones + 13.Char() + 10.Char();
                }
                else
                {
                    s2 = "";
                }
                s = s1 + s2;
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocCVD || MODXDOC.VOpe.TipoDoc == MODXDOC.DocArb || MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegPln)
            {
                s = MODXDOC.VInsEsp.InsExp;
            }
            else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegCanRet)
            {
                if (MODXDOC.SaldoRet != "")
                {
                    s = MODFRA.SyGet_Fra(uow, 6725, "E", MODXDOC.SaldoRet);
                }
                if (MODXDOC.Instrucciones != "")
                {
                    s = s + 13.Char() + 10.Char() + MODXDOC.Instrucciones;
                }
            }

            x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);

            for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            {
                if (MODXDOC.Lineas[i].TrimB() != "")
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                    MODXDOC.Pr_Salto_Pagina();
                }
            }

            if (MODXDOC.SaldoRet != "")
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
            }

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el primer Párrafo de los documentos.
        // Observaciones  : Coloca un párrafo en el documento dependiendo del
        //                  tipo de carta
        // ****************************************************************************
        private void Pr_Parrafo_1(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta_Aux)
        {
            int i = 0;
            int x = 0;
            string Paso = "";
            string TipoAviso_t = "";
            string Cas = "";
            string s = "";
            string p = "";
            string d = "";

            switch (Carta_Aux)
            {
                case 0:
                case 20:
                    d = MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado + ", " + MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado);
                    d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado);
                    if (MODXDOC.Idioma == "I")
                    {
                        p = MigrationSupport.Utils.Format(MODXDOC.VxCob.FecIng, "mm/dd/yyyy") + "~" + MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1) + "~" + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                    }
                    else
                    {
                        p = MODXDOC.VxCob.FecIng + "~" + MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1) + "~" + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                    }
                    s = MODFRA.SyGet_Fra(uow, 6706, MODXDOC.Idioma, p);
                    // s$ = "Por cuenta de nuestros clientes, adjuntamos los siguientes documentos adicionales para su cobro, los cuales deben consideranse como parte integrante de nuestro envío original del " + Trim$(Glosa_Fecha_Hoy_Espanol()) + ", y ser entregados contra " + CopiarDeString(VxCob.Condicion, ";", 1) + ", a los girados: " + Trim$(PartysOpe(IExp1).NombreUsado)
                    if (d != "")
                    {
                        s = s + ", " + d;
                    }
                    break;
                case 1:
                    if (MODXDOC.Idioma == "I")
                    {
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal != "")
                        {
                            Cas = "P.O.Box " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                        }
                        d = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado, Cas, MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PaiEnIng(MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado));
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].telefono != "")
                        {
                            d = d + ", Phone : " + MODXDOC.PartysOpe[MODXDOC.IGir].telefono;
                        }
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].Fax != "")
                        {
                            d = d + ", Fax : " + MODXDOC.PartysOpe[MODXDOC.IGir].Fax;
                        }
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].Telex != "")
                        {
                            d = d + ", Telex : " + MODXDOC.PartysOpe[MODXDOC.IGir].Telex;
                        }
                        // P$ = CopiarDeString(VxCob.Condicion, ";", 2) + Trim$(PartysOpe(IGir).NombreUsado)
                        p = MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 2) + ", " + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                        s = MODFRA.SyGet_Fra(uow, 6707, MODXDOC.Idioma, p);
                        // s$ = "On behalf of our Customer, attached find the following documents for collection, which must be delivered to the drawees" + " against " + CopiarDeString(VxCob.Condicion, ";", 2) + Trim$(PartysOpe(IGir).NombreUsado)
                        if (d != "")
                        {
                            s = s + ", " + d;
                        }
                    }
                    else
                    {
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal != "")
                        {
                            Cas = "Casilla Postal " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                        }
                        d = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado, Cas, MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado);
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado);
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].telefono != "")
                        {
                            d = d + ", Teléfono : " + MODXDOC.PartysOpe[MODXDOC.IGir].telefono;
                        }
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].Fax != "")
                        {
                            d = d + ", Fax : " + MODXDOC.PartysOpe[MODXDOC.IGir].Fax;
                        }
                        if (MODXDOC.PartysOpe[MODXDOC.IGir].Telex != "")
                        {
                            d = d + ", Télex : " + MODXDOC.PartysOpe[MODXDOC.IGir].Telex;
                        }
                        p = MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1) + "~" + MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado.TrimB();
                        s = MODFRA.SyGet_Fra(uow, 6708, MODXDOC.Idioma, p);
                        // s$ = "Por cuenta de nuestros clientes, adjuntamos los siguientes documentos para su cobro, los cuales deben ser entregados contra " + CopiarDeString(VxCob.Condicion, ";", 1) + ", a los girados: " + Trim$(PartysOpe(IGir).NombreUsado)
                        if (d != "")
                        {
                            s = s + ", " + d;
                        }
                    }
                    break;
                case 2:
                    s = MODFRA.SyGet_Fra(uow, 6709, "E", MODXDOC.PartysOpe[MODXDOC.ICob].NombreUsado);
                    // s$ = "De acuerdo a su carta instrucción, hemos procedido al registro y envío de carta de cobro de la cobranza indicada en la referencia, a través del " + PartysOpe(ICob).NombreUsado + ", según el siguiente detalle:"
                    break;
                case 3:
                    p = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.ICob].NombreUsado, MODXDOC.PartysOpe[MODXDOC.ICob].DireccionUsado, MODXDOC.PartysOpe[MODXDOC.ICob].CiudadUsado.TrimB());
                    if (MODXDOC.Idioma == "I")
                    {
                        p = MODXDOC.Concatena(p, MODXDOC.PaiEnIng(MODXDOC.PartysOpe[MODXDOC.ICob].PaisUsado), "");
                    }
                    else
                    {
                        p = MODXDOC.Concatena(p, MODXDOC.PartysOpe[MODXDOC.ICob].PaisUsado, "");
                    }
                    if (MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal != "")
                    {
                        switch (MODXDOC.Idioma)
                        {
                            case "I":
                                Cas = "P.O. Box " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                                break;
                            case "E":
                                Cas = "Casilla Postal " + MODXDOC.PartysOpe[MODXDOC.IGir].CasPostal;
                                break;
                        }
                    }
                    d = MODXDOC.Concatena(MODXDOC.PartysOpe[MODXDOC.IGir].NombreUsado, MODXDOC.PartysOpe[MODXDOC.IGir].DireccionUsado, Cas);
                    d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].ComunaUsado, "");
                    d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].CiudadUsado, MODXDOC.PartysOpe[MODXDOC.IGir].EstadoUsado);
                    if (MODXDOC.Idioma == "I")
                    {
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PaiEnIng(MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado));
                    }
                    else
                    {
                        d = MODXDOC.Concatena(d, MODXDOC.PartysOpe[MODXDOC.IGir].PostalUsado, MODXDOC.PartysOpe[MODXDOC.IGir].PaisUsado);
                    }
                    s = MODFRA.SyGet_Fra(uow, 6710, MODXDOC.Idioma, p + "~" + d);
                    // s$ = "De acuerdo a instrucciones del exportador, hemos procedido al registro y envío de carta de cobro de la cobranza indicada en la referencia, a través del Banco " + Trim$(PartysOpe(ICob).NombreUsado) + " " + Trim$(PartysOpe(ICob).CiudadUsado) + " " + Trim$(PartysOpe(ICob).PaisUsado) + ", a cargo de " + Trim$(PartysOpe(IExp1).NombreUsado) + " " + Trim$(PartysOpe(IExp1).CiudadUsado) + " " + Trim$(PartysOpe(IExp1).PaisUsado)
                    break;
                case 4:
                    s = MODFRA.SyGet_Fra(uow, 6711, "E", "");
                    // s$ = "No es grato informarles, que la(s) letra(s) girada(s) por esta cobranza extrajera, ha(n) sido aceptada(s) por el girado, según el siguiente detalle."
                    break;
                case 5:
                    s = MODFRA.SyGet_Fra(uow, 6712, "E", "");
                    // s$ = "Cúmplenos informarles que, con esta fecha hemos procedido a descargar de nuestros registros la cobranza indicada en la referencia, en atención a que ha sido cancelada en forma directa a ustedes."
                    break;
                case 6:
                    p = MODXDOC.Total_Parcial.TrimB();
                    if (p.InStr("cobranza", 1, StringComparison.CurrentCulture) != 0)
                    {
                        s = MODFRA.SyGet_Fra(uow, 6724, "E", p);
                    }
                    else
                    {
                        if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCanRet)
                        {
                            s = p + " producto de lo cual efectuamos la siguiente distribución:";
                        }
                        else
                        {
                            s = p;
                        }
                    }
                    // s$ = "De acuerdo a sus instrucciones hemos procedido al pago " + Trim$(TipoPago) + " de " + Trim$(PagoDe) + " producto de lo cual efectuamos la siguiente distribución:"
                    break;
                case 7:
                    s = MODFRA.SyGet_Fra(uow, 6714, "E", "");
                    // s$ = "Nos es grato poner en su conocimiento, que con esta fecha hemos procedido a registrar el retorno citado en la referencia, según el siguiente detalle:"
                    break;
                case 8:
                    s = MODFRA.SyGet_Fra(uow, 6715, "E", "");
                    // s$ = "De acuerdo a sus instrucciones, hemos procedido a emitir planillas de ingreso de comercio visible, según el siguiente detalle:"
                    break;
                case 9:
                    switch (MODXDOC.TipoDC.TrimB())
                    {
                        case "D":
                            TipoAviso_t = "débito";
                            break;
                        case "C":
                            TipoAviso_t = "crédito";
                            break;
                    }
                    p = TipoAviso_t.TrimB() + "~" + MODXDOC.NroCtaCte.TrimB() + "~" + MODXDOC.Concepto.TrimB();
                    s = MODFRA.SyGet_Fra(uow, 6716, "E", p);
                    // s$ = "Hemos efectuado el siguiente " + Trim$(TipoAviso_t) + " en su cuenta corriente " + Trim$(NroCtaCte) + " por concepto de " + Trim$(Concepto)
                    break;
                case 10:
                    if (MODXDOC.CuantasCompras != 0 && MODXDOC.CuantasVentas != 0)
                    {
                        Paso = "Compras y Ventas de Divisas, ";
                    }
                    else if (MODXDOC.CuantasCompras != 0)
                    {
                        Paso = "Compras de Divisas, ";
                    }
                    else if (MODXDOC.CuantasVentas != 0)
                    {
                        Paso = "Ventas de Divisas, ";
                    }
                    p = Paso.TrimB();
                    s = MODFRA.SyGet_Fra(uow, 6717, "E", p);
                    // s$ = "De acuerdo a vuestras instrucciones hemos efectuado " + Paso$ + " según el siguiente detalle:"
                    break;
                case 11:
                    s = MODFRA.SyGet_Fra(uow, 6718, "E", "");
                    // s$ = "Hemos efectuado Arbitraje de Divisas, según el siguiente detalle/distribución:"
                    break;
                case 14:
                    MigrationSupport.Printer.DefInstance.Print();
                    MODXDOC.Pr_Salto_Pagina();
                    s = MODXDOC.Total_Parcial.TrimB();
                    break;
            }

            documento.Parrafo1 = s;
            //MODXDOC.Lineas = new string[1];
            //TODO ARKANO 
            /*x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);

            for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            {
                if (MODXDOC.Lineas[i].TrimB() != "")
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                    MODXDOC.Pr_Salto_Pagina();
                }
            }*/
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el segundo Párrafo de los documentos.
        // Observaciones  : Coloca un párrafo en el documento dependiendo del
        //                  tipo de carta
        // ****************************************************************************
        private void Pr_Parrafo_2(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta_Aux)
        {
            string s = "";
            int i = 0;
            int x = 0;
            string s7 = "";
            string s6 = "";
            string s5 = "";
            string s4 = "";
            string s3 = "";
            string s2 = "";
            string s1 = "";
            string p = "";
            int n = 0;

            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            switch (Carta_Aux)
            {
                case 0:
                case 1:
                case 20:
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 1).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 2);
                        s1 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 7).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 8);
                        s2 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 9).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 10);
                        s3 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 11).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 12);
                        s4 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 13).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 14);
                        s5 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 15).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 16);
                        s6 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 25).ToInt();
                    if (n > 0)
                    {
                        p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 26);
                        s7 = MODFRA.SyGet_Fra(uow, n, MODXDOC.Idioma, p);
                    }
                    if (s1.TrimB() != "")
                    {
                        s1 = s1 + 13.Char() + 10.Char();
                    }
                    if (s2.TrimB() != "")
                    {
                        s2 = s2 + 13.Char() + 10.Char();
                    }
                    if (s3.TrimB() != "")
                    {
                        s3 = s3 + 13.Char() + 10.Char();
                    }
                    if (s4.TrimB() != "")
                    {
                        s4 = s4 + 13.Char() + 10.Char();
                    }
                    if (s5.TrimB() != "")
                    {
                        s5 = s5 + 13.Char() + 10.Char();
                    }
                    if (s6.TrimB() != "")
                    {
                        s6 = s6 + 13.Char() + 10.Char();
                    }
                    if (s7.TrimB() != "")
                    {
                        s7 = s7 + 13.Char() + 10.Char();
                    }
                    s1 = s1 + s2 + s3 + s4 + s5 + s6 + s7 + 13.Char() + 10.Char();
                    s2 = "";
                    s3 = "";
                    s4 = "";
                    s5 = "";
                    s6 = "";
                    s7 = "";
                    s1 = s1.TrimB() + MODXDOC.VInsEsp.InsCob.TrimB() + 13.Char() + 10.Char();
                    break;
                case 2:
                    // Frase de las comisiones.-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 3).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 4);
                    s1 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    // Frase del Agente.-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 5).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 6);
                    s2 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    // Frase de Protesto.-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 23).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 24);
                    s3 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    if (s1.TrimB() != "")
                    {
                        s1 = s1 + 13.Char() + 10.Char();
                    }
                    if (s2.TrimB() != "")
                    {
                        s2 = s2 + 13.Char() + 10.Char();
                    }
                    if (s3.TrimB() != "")
                    {
                        s3 = s3 + 13.Char() + 10.Char();
                    }
                    s1 = s1 + s2 + s3 + 13.Char() + 10.Char();
                    s2 = "";
                    s3 = "";
                    s1 = s1.TrimB() + MODXDOC.VInsEsp.InsExp.TrimB() + 13.Char() + 10.Char();
                    // Frase de Solicitamos....-
                    n = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 19).ToInt();
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 20);
                    s2 = MODFRA.SyGet_Fra(uow, n, "E", p);
                    s1 = s1 + 13.Char() + 10.Char() + s2 + 13.Char() + 10.Char();
                    s2 = "";
                    break;
                case 3:
                    s1 = MODXDOC.VInsEsp.InsAge + 13.Char() + 10.Char();
                    break;
                case 4:
                    s1 = MODXDOC.VInsEsp.InsExp + 13.Char() + 10.Char();
                    break;
                case 5:
                    s1 = MODFRA.SyGet_Fra(uow, 6719, "E", "") + 13.Char() + 10.Char();
                    // s1$ = "Solicitamos revisar cuidadosamente los datos contenidos en esta carta. Si estos no se ajustan a lo instruído por ustedes, sírvase contactarse a la brevedad con nosotros." + Chr(13) + Chr(10)
                    s2 = "";
                    s3 = "";
                    break;
                case 10:
                case 11:
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, true);
                    s1 = MODFRA.SyGet_Fra(uow, 6720, "E", "");
                    // s1$ = "La moneda extranjera vendida fue distribuida de la siguiente forma:"
                    s2 = "";
                    s3 = "";
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
                    break;
            }

            s4 = s1 + s2 + s3;
            //TODO ARKANO x = MODXDOC.GetLines(s4,CajaMultilinea,ref MODXDOC.Lineas);
            /*for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            {
                if (MODXDOC.Lineas[i].TrimB() != "")
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                    MODXDOC.Pr_Salto_Pagina();
                }
            }*/
            documento.Paso1 = s4;
            if (Carta_Aux == 3)
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                s = MODFRA.SyGet_Fra(uow, 6742, MODXDOC.Idioma, "");
                MigrationSupport.Printer.DefInstance.Print(s);
                MODXDOC.Pr_Salto_Pagina();
            }

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el Pie de Página de los documentos.
        // Observaciones  : Coloca un pie de página en el documento
        // ****************************************************************************
        private void Pr_Pie_Pagina(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int j = 0;
            int x = 0;
            bool restaurar = false;
            string s = "";
            string p = "";
            string fax_eje = "";
            string tel_eje = "";
            string dir_eje = "";
            string nom_eje = "";
            string codofi = "";
            int k = 0;
            int a = 0;
            int n = 0;
            const string Publicacion = " 522.";

            // CFV-10/11/2006
            // Se cambia llamada a otra funcion
            // a% = Fn_Buscar_Indice(UsrEsp.cencos, UsrEsp.CodUsr)
            // 
            // ------------------------------------------------------
            // RealSystems - Código Original - Inicio
            // Fecha Comentado: 20100518
            // Responsable: Mauricio Aguilera V.
            // Version: 1
            // Descripcion: Busca datos del ejecutivo
            // ------------------------------------------------------
            // 
            //  a% = Fn_Buscar_Indice_2(UsrEsp.cencos, UsrEsp.CodUsr, rutiparty, CENTRO_COSTO)
            // CFV
            // 
            //  If a% <> 0 Then
            //     UsrEsp.nombre = UsrEsps(a%).nombre
            //     UsrEsp.Direccion = UsrEsps(a%).Direccion
            //     UsrEsp.Ciudad = UsrEsps(a%).Ciudad
            //     UsrEsp.telefono = UsrEsps(a%).telefono
            //     UsrEsp.Fax = UsrEsps(a%).Fax
            //  End If
            // 
            //  If Idioma = "" Then
            //     Idioma = "E"
            //  End If
            // --------------------------------------------------
            // RealSystems - Código Original - Termino
            // --------------------------------------------------

            MODXDOC.UsrEsps = new MODXDOC.EstrucUsuarios[1];
            n = MODXDOC.UsrEsps.Length;
            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.UsrEsps.GetUpperBound(0);
            //});

            // -------------------------------------------------
            // RealSystems - Código Nuevo - Inicio
            // Fecha Comentado: 20100518
            // Responsable: Mauricio Aguilera V.
            // Version: 1.1
            // Descripcion: Busca datos del ejecutivo
            // -------------------------------------------------

            a = 0;
            for (k = 0; k < n; k += 1)
            {
                if (MODXDOC.UsrEsps[k].cencos == MODXDOC.UsrEsp.cencos && MODXDOC.UsrEsps[k].CodUsr == MODXDOC.UsrEsp.CodUsr && MODXDOC.UsrEsps[k].nombre != "")
                {
                    a = k;
                    break;
                }
            }

            if (a == 0)
            {
                a = MODXDOC.Fn_Buscar_Indice_2(uow, MODXDOC.UsrEsp.cencos, MODXDOC.UsrEsp.CodUsr, Module1.rutiparty, MODFRA.CENTRO_COSTO);
            }
            // --------------------------------------------------
            // RealSystems - Código Nuevo - Termino
            // --------------------------------------------------
            // 
            // 
            // If UsrEsps(a%).nombre = "" Then     Se cambia la condicion del IF  22.06.2010
            if (a == 0 || MODXDOC.UsrEsps[a].nombre == "")
            {
                a = MODXDOC.Fn_Buscar_Indice(uow, MODXDOC.UsrEsp.cencos, MODXDOC.UsrEsp.CodUsr);
            }

            MODXDOC.UsrEsp.nombre = MODXDOC.UsrEsps[a].nombre;
            MODXDOC.UsrEsp.Direccion = MODXDOC.UsrEsps[a].Direccion;
            MODXDOC.UsrEsp.Ciudad = MODXDOC.UsrEsps[a].Ciudad;
            MODXDOC.UsrEsp.telefono = MODXDOC.UsrEsps[a].telefono;
            MODXDOC.UsrEsp.Fax = MODXDOC.UsrEsps[a].Fax;

            if (MODXDOC.Idioma == "")
            {
                MODXDOC.Idioma = "E";
            }

            // 
            // CFV-10/11/2006
            // Se comenta el llenado de datos del ejecutivo
            if (Module1.Hab_SGTCliEje != 0)
            {
                // Cliente_SGT = Es_Cliente(Mid(rutiparty, 1, 9))
                // If Cliente_SGT Then
                //    bien% = Lee_SgtCliEsp(Mid(rutiparty, 1, 9))
                //    If bien% Then
                //       For i% = 1 To UBound(VSGTCliEsp)
                //          If VSGTCliEsp(i%).tipo = SGT_tipopimp Then
                //             nom_eje = Obtiene_NomEsp(VSGTCliEsp(i%).ofieje, VSGTCliEsp(i%).codeje)
                //          ElseIf VSGTCliEsp(i%).tipo = SGT_tipopexp Then
                //             nom_eje = Obtiene_NomEsp(VSGTCliEsp(i%).ofieje, VSGTCliEsp(i%).codeje)
                //          ElseIf VSGTCliEsp(i%).tipo = SGT_tipnegoc Then
                //             nom_eje = Obtiene_NomEsp(VSGTCliEsp(i%).ofieje, VSGTCliEsp(i%).codeje)
                //          End If
                //       Next i%
                //       For i% = 1 To UBound(VEjc)
                //          If nom_eje = Minuscula2(VEjc(i%).nombre) Then
                //             dir_eje = sacar_direccion(VEjc(i%).rut)
                //             tel_eje = VEjc(i%).telefono
                //             fax_eje = VEjc(i%).Fax
                //             If fax_eje = "" Then
                //                fax_eje = " "
                //             End If
                //             If tel_eje = "" Then
                //                tel_eje = " "
                //             End If
                //             Exit For
                //          End If
                //       Next i%
                //    End If
                // End If
            }
            // CFV
            // 
            // FTF:22032001, Si es de sucursales no se imprime el nombre del ejecutivo
            if (codofi != "000" && MODXDOC.UsrEsp.nombre == "")
            {
                a = MODXDOC.GetDatOfi(uow, MODXDOC.VOpe.NumOpe.Mid(0, 3), MODXDOC.VOpe.NumOpe.Mid(7, 3));
            }
            // CFV-10/11/2006
            // Se quita condicion del If y se agrega validacion a telefono y fax
            // si no encuentra el ejecutivo asignado coloca al especialista
            // If Trim(nom_eje) = "" Then
            nom_eje = MODXDOC.UsrEsp.nombre.TrimB();
            dir_eje = MODXDOC.UsrEsp.Direccion.TrimB();
            tel_eje = MODXDOC.UsrEsp.telefono.TrimB();
            fax_eje = MODXDOC.UsrEsp.Fax.TrimB();
            // End If
            if (tel_eje.TrimB() == "")
            {
                tel_eje = " ";
            }
            if (fax_eje.TrimB() == "")
            {
                fax_eje = " ";
            }
            // CFV

            switch (MODXDOC.Carta)
            {
                // Cartas = Nº 5;6;7
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                    if (MODFRA.RefBae == "")
                    {
                        if (MODXDOC.Carta == 6 || MODXDOC.Carta == 7 || MODXDOC.Carta == 9)
                        {
                            // modificado por ... MPN
                            // p$ = Trim$(Referencia) + "~" + Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                            p = MODXDOC.Referencia.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                        }
                        else
                        {
                            // modificado por MPN
                            // p$ = Trim$(VOpe.NumOpe_t) + "~" + Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                            p = MODXDOC.VOpe.NumOpe_t.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                        }
                    }
                    else
                    {
                        // Modificado por MPN
                        // p$ = Trim$(RefBae) + "~" + Trim$(UsrEsp.nombre) + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                        p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                    }
                    s = MODFRA.SyGet_Fra(uow, 9002, "E", p);
                    // s$ = "En toda correspondencia sírvase citar nuestro número " + Trim$(VOpe.NumOpe_t) + " y remitirla a la atención de " + Trim$(UsrEsp.Nombre) + " " + Trim$(UsrEsp.Direccion) + ", fono " + Trim$(UsrEsp.Telefono) + ", fax " + Trim$(UsrEsp.Fax) + "."
                    break;
                default:
                    // Carta = N º1
                    if (MODXDOC.Carta == 1 || MODXDOC.Carta == 20)
                    {
                        if (MODXDOC.Idioma == "I")
                        {
                            if (MODFRA.RefBae == "")
                            {
                                // modificado por ... MPN
                                // p$ = 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Trim$(VOperac.ConRaya) + "~" + Publicacion
                                p = nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + MODXDOC.VOperac.ConRaya.TrimB() + "~" + Publicacion;
                            }
                            else
                            {
                                // modificado por MPN
                                // p$ = 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Trim$(RefBae) + "~" + Publicacion
                                p = nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + MODFRA.RefBae.TrimB() + "~" + Publicacion;
                            }
                            s = MODFRA.SyGet_Fra(uow, 9003, MODXDOC.Idioma, p);
                        }
                        else
                        {
                            if (MODFRA.RefBae == "")
                            {
                                // modificado por MPN
                                // p$ = Trim$(VOperac.ConRaya) + "~" + Trim$(UsrEsp.nombre) + " " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                                p = MODXDOC.VOperac.ConRaya.TrimB() + "~" + nom_eje.TrimB() + " " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                            }
                            else
                            {
                                //  modificado por MPN
                                // p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                                p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                            }
                            s = MODFRA.SyGet_Fra(uow, 9001, MODXDOC.Idioma, p);
                        }
                    }
                    else if (MODXDOC.Carta == 14)
                    {
                        if (MODFRA.RefBae == "")
                        {
                            //  modificado por MPN
                            // p$ = Trim$(Referencia) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                            p = MODXDOC.Referencia.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                        }
                        else
                        {
                            //  modificado por MPN
                            // p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                            p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                        }
                        s = MODFRA.SyGet_Fra(uow, 9002, "E", p);
                        // Carta siguientes
                    }
                    else
                    {
                        if (MODXDOC.VOperac.ConRaya == "")
                        {
                            restaurar = true;
                            MODXDOC.VOperac.ConRaya = MODXDOC.VOpe.NumOpe_t;
                        }
                        switch (MODXDOC.VOperac.ConRaya.Mid(5, 2))
                        {
                            case "10":
                            case "20":
                            case "17":
                                if (MODFRA.RefBae == "")
                                {
                                    //  modificado por MPN
                                    // p$ = Trim$(VOperac.ConRaya) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                                    p = MODXDOC.VOperac.ConRaya.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                                }
                                else
                                {
                                    //  modificado por MPN
                                    //  p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax)
                                    p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB();
                                }
                                s = MODFRA.SyGet_Fra(uow, 9002, "E", p);
                                break;
                            default:
                                if (MODFRA.RefBae == "")
                                {
                                    //  modificado por MPN
                                    // p$ = Trim$(VOperac.ConRaya) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                                    p = MODXDOC.VOperac.ConRaya.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                                }
                                else
                                {
                                    //  modificado por MPN
                                    // p$ = Trim$(RefBae) + "~" + 'Trim$(UsrEsp.nombre) + ", " + Trim$(UsrEsp.Direccion) + "~" + Trim$(UsrEsp.telefono) + "~" + Trim$(UsrEsp.Fax) + "~" + Publicacion
                                    p = MODFRA.RefBae.TrimB() + "~" + nom_eje.TrimB() + ", " + dir_eje.TrimB() + "~" + tel_eje.TrimB() + "~" + fax_eje.TrimB() + "~" + Publicacion;
                                }
                                s = MODFRA.SyGet_Fra(uow, 9004, "E", p);
                                break;
                        }
                        // s$ = "En toda correspondencia sírvase citar nuestro número " + Trim$(VOperac.ConRaya) + " y remitida a la atención de " + Trim$(UsrEsp.Nombre) + " " + Trim$(UsrEsp.Direccion) + ", fono " + Trim$(UsrEsp.Telefono) + ", fax " + Trim$(UsrEsp.Fax) + ". Esta cobranza se acoge a las Reglas y Costumbres de la Cámara Internacional de Comercio, publicación Nº" + Publicacion
                        if (restaurar)
                        {
                            MODXDOC.VOperac.ConRaya = "";
                        }
                    }
                    break;
            }

            documento.PiePagina = s;

            //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
            /*for (j = 1; j <= MODXDOC.Lineas.GetUpperBound(0); j += 1)
            {
                if (j == 1)
                {
                    MigrationSupport.Printer.DefInstance.Print();
                    MODXDOC.Pr_SaltoPag();
                }
                MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[j]);
                MODXDOC.Pr_SaltoPag();
            }*/

            // FTF:22032001, Direccion Internet Negocios Internacionales
            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_SaltoPag();

            //TODO:Revisar, se comenta pero no se deberia de comentar
            //s = MODFRA.SyGet_Fra(uow, 9900, MODXDOC.Idioma, "");

            MigrationSupport.Printer.DefInstance.Print(s);
            MODXDOC.Pr_SaltoPag();

        }
        public string Obtiene_NomEsp(object codofi, string CodEsp)
        {
            string Obtiene_NomEsp = "";


            int i = 0;
            int fin = 0;


            Obtiene_NomEsp = "";

            fin = 0;
            MigrationSupport.Utils.ResumeNext(() =>
            {
                fin = Module1.VEjc.GetUpperBound(0);
            });


            for (i = 1; i <= fin; i += 1)
            {
                if (Module1.VEjc[i].codofi == codofi.ToStr() && Module1.VEjc[i].codejc == CodEsp)
                {
                    Obtiene_NomEsp = Minuscula2(Module1.VEjc[i].nombre);
                    break;
                }
            }
            return Obtiene_NomEsp;
        }
        // Deja un string de varias palabras separadas por un blanco como la primera
        // letra en Mayúscula y el resto de la palabra en minúscula.-
        public string Minuscula2(string PDato)
        {
            string Minuscula2 = "";

            string strCaseArg = "";
            int j = 0;
            string s = "";
            int i = 0;
            string[] Palabras = null;
            // D.S.B.
            Palabras = new string[1];
            string Dato = "";

            // Deja cada palabra separado con un solo blanco
            Dato = MODGDOC.Componer(PDato, "  ", " ");
            i = 1;
            s = MODGDOC.CopiarDeString(Dato, " ", i);
            while (s != "")
            {
                // Verifica si la palabra siguiente contiene la cantidad de caracteres necesarios.
                j = Palabras.GetUpperBound(0) + 1;
                Array.Resize(ref Palabras, j + 1);
                strCaseArg = s.UCase().TrimB();
                if (strCaseArg == "S.A." || strCaseArg == "M/E" || strCaseArg == "M/N")
                {
                    Palabras[j] = s.UCase();
                }
                else if (strCaseArg == "A" || strCaseArg == "DE" || strCaseArg == "Y" || strCaseArg == "O" || strCaseArg == "Y/O" || strCaseArg == "U" || strCaseArg == "AL" || strCaseArg == "DE" || strCaseArg == "LO" || strCaseArg == "LA" || strCaseArg == "EL" ||
                   strCaseArg == "SI" || strCaseArg == "NO" || strCaseArg == "E" || strCaseArg == "POR" || strCaseArg == "OF")
                {
                    Palabras[j] = s.LCase();
                }
                else
                {
                    Palabras[j] = s.Mid(1, 1).UCase() + s.Mid(2, s.Len() - 1).LCase();
                }
                i = i + 1;
                s = MODGDOC.CopiarDeString(Dato, " ", i);
            }
            s = "";
            for (i = 1; i <= Palabras.GetUpperBound(0); i += 1)
            {
                s = s + Palabras[i] + " ";
            }
            Minuscula2 = s.TrimB();

            return Minuscula2;
        }
        public int GetDatosSgtEjc()
        {
            int GetDatosSgtEjc = 0;

            string MsgCVD = "";
            string MsgSgt = "";


            try
            {

                MODGSRM.ParamSrm8k.Nodo = MODGDOC.GetSceIni("SgtEjecutivo", "NodoEjc");
                if (MODGSRM.ParamSrm8k.Nodo == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Nodo, para tabla SGT Cliente Especialista . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                       MsgSgt);
                    throw new System.Exception();
                }

                MODGSRM.ParamSrm8k.Servidor = MODGDOC.GetSceIni("SgtEjecutivo", "ServEjc");
                if (MODGSRM.ParamSrm8k.Servidor == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Servidor de Lectura , para tabla SGT Cliente Especialista . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgSgt);
                    throw new System.Exception();
                }

                MODGSRM.ParamSrm8k.base_migname = MODGDOC.GetSceIni("SgtEjecutivo", "BDEjc");
                if (MODGSRM.ParamSrm8k.base_migname == "")
                {
                    MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Base de Datos SyBase para lee Ejecutivos . La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                       MsgCVD);
                    throw new System.Exception();
                }


                GetDatosSgtEjc = true.ToInt();

                return GetDatosSgtEjc;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }

        public void Pr_Cargar_Espec(System.Windows.Forms.Control Cbo_Us, string oficina, string tipejc)
        {
            string s = "";
            int i = 0;
            int n = 0;

            // 
            // carga Lista Cbo_Us
            MigrationSupport.Utils.ResumeNext(() =>
            {
                n = Module1.VEjc.GetUpperBound(0);
            });

            // 

            ((dynamic)Cbo_Us).Clear();
            if (n > 0)
            {
                for (i = 1; i <= n; i += 1)
                {
                    if (Module1.VEjc[i].codofi == oficina && Module1.VEjc[i].tipo == tipejc)
                    {
                        s = "";
                        s = s + MigrationSupport.Utils.Format(Module1.VEjc[i].codofi, "000") + "-";
                        s = s + MigrationSupport.Utils.Format(Module1.VEjc[i].codejc, "000") + " : ";
                        s = s + Minuscula2(Module1.VEjc[i].nombre);

                        ((dynamic)Cbo_Us).AddItem(s);
                        //TODO ARKANO ((dynamic)Cbo_Us).ItemData(((object)((dynamic)Cbo_Us).NewIndex())) = i;

                    }
                }

                // If Cbo_Us.ListCount > 0 Then Cbo_Us.Selected(0) = True
            }

        }
        public int Sygetn_Ejecutivos()
        {
            int Sygetn_Ejecutivos = 0;

            bool Leyo_Ok = false;
            int i = 0;
            int n = 0;
            string MsgUsr1 = "";
            string R = "";
            string Que = "";

            try
            {

                Module1.VEjc = new Module1.T_Especialista[1];

                // 
                // Que$ = "select ejc_rut, ejc_nom, ejc_tpo from " + ParamSrm8K.base + "." + ParamSrm8K.usuario + ".sce_usr" + " where cent_costo = " + dbcharSy(codigo)
                Que = "select ejc_ofi, ejc_cod, ejc_rut, ejc_nom, ejc_tpo, ejc_tel, ejc_fax from sds_sgt_ejc ";
                Que = Que + " where ejc_tpo = " + MODGSYB.dbcharSy(Module1.EJCOPIMP) + " OR ";
                Que = Que + " ejc_tpo = " + MODGSYB.dbcharSy(Module1.EJCOPEXP) + " OR ";
                Que = Que + " ejc_tpo = " + MODGSYB.dbcharSy(Module1.EJCNEGOC);

                Que = Que.LCase();

                R = MODGSRM.RespuestaQuery(ref Que);
                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer los Especialistas (Sds_Sgt_Ejc). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(Module1.MB_ICONEXCLAMATION).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgUsr1);
                    return Sygetn_Ejecutivos;
                }

                // Resultado nulo de la Consulta.-
                if (R == "")
                {
                    return Sygetn_Ejecutivos;
                }

                n = MODGSRM.getocurrs();
                Module1.VEjc = new Module1.T_Especialista[n + 1];
                for (i = 1; i <= n; i += 1)
                {
                    Module1.VEjc[i].codofi = MigrationSupport.Utils.Format(MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R), "000");
                    Module1.VEjc[i].codejc = MigrationSupport.Utils.Format(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R), "000");
                    Module1.VEjc[i].rut = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                    Module1.VEjc[i].nombre = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                    Module1.VEjc[i].tipo = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                    Module1.VEjc[i].telefono = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str();
                    Module1.VEjc[i].Fax = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str();
                    R = MODGSRM.NuevaRespuesta(7, R);
                }

                Sygetn_Ejecutivos = true.ToInt();

                return Sygetn_Ejecutivos;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                Leyo_Ok = false;
                throw new XegiException("Error al leer en Sds_Sgt_Ejc: [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }
        }

        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Banco Cobrador......Girador
        // Observaciones  : Contiene Párrafos, Documentos (letras, etc),
        //                  Instrucciones al Bco. Cobrador, Pie de Página
        // ****************************************************************************
        private void Pr_Principal_1(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int m = 0;
            int n = 0;

            MigrationSupport.Utils.ResumeNext(() =>
            {
                n = MODXDOC.VxLet.GetUpperBound(0);
                m = MODXDOC.VxDem.GetUpperBound(0);
            });

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 20;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.ICob, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            MODXDOC.Pr_Instrucciones(MODXDOC.Num_Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Num_Carta);     // Segundo Párrafo.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Septiembre 1995
        // Propósito      : Llamada desde Carta:....
        // Observaciones  : Contiene Párrafos, Montos, Pie de Página, etc.
        // ****************************************************************************
        public void Pr_Principal_10(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = MODXDOC.VMontos.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VMontos.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 9;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Mto_DebitoCredito(documento);     // Documentos.
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Octubre 1995
        // Propósito      : Llamada desde Carta: 620.
        // ****************************************************************************
        public void Pr_Principal_11(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int p = MODXDOC.VPlan.Length;
            int e = MODXDOC.VxOri.Length;
            int d = MODXDOC.VxVia.Length;
            int c = MODXDOC.VxRemesa.Length;
            int b = MODXDOC.Ventas.Length;
            int a = MODXDOC.Compras.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    a = MODXDOC.Compras.GetUpperBound(0);
            //    b = MODXDOC.Ventas.GetUpperBound(0);
            //    c = MODXDOC.VxRemesa.GetUpperBound(0);
            //    d = MODXDOC.VxVia.GetUpperBound(0);
            //    e = MODXDOC.VxOri.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 10;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (a > 0)
            {
                MODXDOC.Pr_Compras();     // Compras.
            }
            if (b > 0)
            {
                MODXDOC.Pr_Ventas();     // Ventas.
            }
            if (c > 0)
            {
                Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Segundo Párrafo.
                MODXDOC.Pr_Remesa(documento);     // Remesas.
            }
            if (d > 0 || e > 0)
            {
                Pr_Abonos(documento);     // Cargos y Abonos.
            }

            Pr_Cta_Cte(documento, uow, MODXDOC.DocCVD);     // Cuenta Corriente(Débitos).


            if (p > 0)
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Print("Planilla(s) Invisible(s)");
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                for (i = 1; i <= p; i += 1)
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.VPlan[i].NroPlan.TrimB());
                }
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
            }

            if (MODXDOC.VInsEsp.InsExp != "")
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                Pr_Informacion(documento, uow);     // Información.
            }
            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Octubre 1995
        // Propósito      : Llamada desde Carta:621.
        // ****************************************************************************
        public void Pr_Principal_12(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int p = 0;
            int d = MODXDOC.VxOri.Length;
            int c = MODXDOC.VxVia.Length;
            int b = MODXDOC.VxRemesa.Length;
            int a = MODXDOC.VArb.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    a = MODXDOC.VArb.GetUpperBound(0);
            //    b = MODXDOC.VxRemesa.GetUpperBound(0);
            //    c = MODXDOC.VxVia.GetUpperBound(0);
            //    d = MODXDOC.VxOri.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 11;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (a > 0)
            {
                MODXDOC.Pr_Arbitrajes(documento);     // Arbitraje.
            }
            if (b > 0)
            {
                Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Segundo Párrafo.
                MODXDOC.Pr_Remesa(documento);     // Remesas.
            }
            if (d > 0)
            {
                Pr_Abonos(documento);     // Cargos y Abonos.
            }
            Pr_Cta_Cte(documento, uow, MODXDOC.DocArb);     // Cuenta Corriente(Débitos).

            if (p > 0)
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Print("Planilla(s) Invisible(s)");
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                for (i = 1; i <= p; i += 1)
                {
                    MigrationSupport.Printer.DefInstance.Print(MODXDOC.VPlan[i].NroPlan.TrimB());
                }
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
            }

            if (MODXDOC.VInsEsp.InsExp != "")
            {
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
                Pr_Informacion(documento, uow);     // Información.
            }
            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 611.
        // Observaciones  : Contiene Párrafos, Distribuciones, Información General,
        //                  Abonos y/o Cargos.
        // ****************************************************************************
        public void Pr_Principal_14(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int p = MODXDOC.VPlan.Length;
            int m = MODXDOC.VDet.Length;
            int n = MODXDOC.VDist.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDist.GetUpperBound(0);
            //    m = MODXDOC.VDet.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 14;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Distribucion(documento);     // Distribuciones.
            }
            Pr_Informacion(documento, uow);     // Información.
            if (p > 0)
            {
                Pr_Parrafo_1(documento, uow, 8);     // Primer Párrafo.
                MODXDOC.Pr_Planilla(documento);     // Planillas.
            }
            MODXDOC.Pr_Titulo_Abono();     // Título de Abonos y/o Cargos.
            if (m > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxCobCan);     // Cuenta Corriente(Débitos).
            }
            if (MODXDOC.TipCam != 0)
            {
                s = MODFRA.SyGet_Fra(uow, 8804, "E", MODGDOC.forma(MODXDOC.TipCam, MODXDOC.FormatoTipCamb));
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Banco Cobrador......Girador
        // Observaciones  : Contiene Párrafos, Montos (monto, intereses, gastos,
        //                  totales, vencimientos), Documentos (letras, etc),
        //                  Tipo de Embarque, Instrucciones al Bco. Cobrador,
        //                  Pie de Página
        // ****************************************************************************
        public void Pr_Principal_2(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int m = MODXDOC.VxDem.Length;
            int n = MODXDOC.VxLet.Length;

            /*string FileName = "dale3.ps";            
            MigrationSupport.Printer.DefInstance.PrintFileName = FileName;
            MigrationSupport.Printer.DefInstance.PrintAction = System.Drawing.Printing.PrintAction.PrintToFile;*/

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxLet.GetUpperBound(0);
            //    m = MODXDOC.VxDem.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 1;
            MODXDOC.Num_Carta = MODXDOC.Carta;

            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.ICob, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            MODXDOC.Pr_Montos(documento, MODXDOC.Carta);
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            Pr_Tipo_Embarque(documento, uow, MODXDOC.Carta);     // Detalle de Tipo de Embarque.
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }

            //MigrationSupport.Printer.DefInstance.EndDoc();manda imprimir

            //Ghostscript(FileName);
        }

        private static void Ghostscript(string FileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "C:\\Program Files (x86)\\gs\\gs9.16\\bin\\gswin32.exe";

            string path = System.IO.Directory.GetCurrentDirectory();
            string psName = path + "\\" + FileName;
            string pdfName = psName.Replace(".ps", ".pdf");

            //proc.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(pdfName);
            //output umschreiben
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;

            //dynamic PDfName = System.IO.Path.ChangeExtension(path + "\\" + FileName, "pdf");

            string Args = string.Format("-dSAFER -dBATCH -dNOPAUSE -dCompatibilityLevel=1.4 -sDEVICE=pdfwrite -sOutputFile={0} {1}", pdfName, psName);
            proc.StartInfo.Arguments = Args;
            proc.Start();
            proc.WaitForExit();
            dynamic Output = proc.StandardOutput.ReadToEnd();
            dynamic OutputErrors = proc.StandardError.ReadToEnd();
            //Debug.Print(Output);
            //Debug.Print(OutputErrors);
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Exportador......Girado
        // Observaciones  : Contiene Párrafos, Montos (montos, intereses, gastos,
        //                  totales, vencimientos), Documentos (letras, etc),
        //                  Tipo de Embarque, Información al Exportador,
        //                  Pie de Página
        // ****************************************************************************
        public void Pr_Principal_3(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int p = MODXDOC.VDet.Length;
            int m = MODXDOC.VxDem.Length;
            int n = MODXDOC.VxLet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxLet.GetUpperBound(0);
            //    m = MODXDOC.VxDem.GetUpperBound(0);
            //    p = MODXDOC.VDet.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 2;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            MODXDOC.Pr_Montos(documento, MODXDOC.Carta);
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            Pr_Tipo_Embarque(documento, uow, MODXDOC.Carta);     // Detalle de Tipo de Embarque.
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            if (p > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxCobReg);     // Cuenta Corriente(Débitos).
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Agente......Exportador
        // Observaciones  : Contiene Párrafos, Montos (montos, intereses, gastos,
        //                  totales, vencimientos), Documentos (letras, etc),
        //                  Tipo de Embarque, Información al Agente,
        //                  Pie de Página
        // ****************************************************************************
        public void Pr_Principal_4(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int m = MODXDOC.VxDem.Length;
            int n = MODXDOC.VxLet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxLet.GetUpperBound(0);
            //    m = MODXDOC.VxDem.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 3;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IAge, MODXDOC.IExp1);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            MODXDOC.Pr_Montos(documento, MODXDOC.Carta);
            if (n > 0 || m > 0)
            {
                MODXDOC.Pr_Documentos(documento, MODXDOC.Carta);     // Documentos.
            }
            Pr_Tipo_Embarque(documento, uow, MODXDOC.Carta);     // Detalle de Tipo de Embarque.
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            //Pr_Pie_Pagina() Tomar en cuenta que puede ser necesario llamarlo para el impresor antiguo.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Llamada desde Carta: Exportador......Girado
        // Observaciones  : Contiene Párrafos, Detalle de aceptación (letras, etc),
        //                  Información al Exportador, Pie de Página
        // ****************************************************************************
        public void Pr_Principal_5(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = 0;

            MigrationSupport.Utils.ResumeNext(() =>
            {
                n = MODXDOC.VxLet.GetUpperBound(0);
            });

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 4;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Detalle(documento);     // Detalle de aceptación.
            }
            MODXDOC.Pr_Instrucciones(MODXDOC.Carta);     // Instrucciones.
            Pr_Parrafo_2(documento, uow, MODXDOC.Carta);     // Párrafo 2 semi - variante.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }/**/
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 610.
        // Observaciones  : Contiene Párrafos, Comisiones y Gastos Cobrados.
        // ****************************************************************************
        public void Pr_Principal_6(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = MODXDOC.VDet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDet.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 5;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado sólo para el caso de la carta Exportador.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxPagDir);     // Cuenta Corriente(Débitos).
            }
            // Call Pr_Parrafo_2(Carta)    'Segundo Párrafo.
            Pr_Texto_Libre();     // Despliegue de Texto Libre del Usuario.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 611.
        // Observaciones  : Contiene Párrafos, Distribuciones, Información General,
        //                  Abonos y/o Cargos.
        // ****************************************************************************
        public void Pr_Principal_7(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int p = MODXDOC.VPlan.Length;
            int m = MODXDOC.VDet.Length;
            int n = MODXDOC.VDist.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDist.GetUpperBound(0);
            //    m = MODXDOC.VDet.GetUpperBound(0);
            //    p = MODXDOC.VPlan.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 6;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Cob(documento, MODXDOC.IExp1, MODXDOC.IGir);     // Encabezado de Carta.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Distribucion(documento);     // Distribuciones.
            }
            Pr_Informacion(documento, uow);     // Información.
            // If p% > 0 Then
            //     Call Pr_Planilla        'Planillas.
            // End If
            if (p > 0)
            {
                Pr_Parrafo_1(documento, uow, 8);     // Primer Párrafo.
                MODXDOC.Pr_Planilla(documento);     // Planillas.
            }
            MODXDOC.Pr_Titulo_Abono();     // Título de Abonos y/o Cargos.
            if (m > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxCobCan);     // Cuenta Corriente(Débitos).
            }
            if (MODXDOC.TipCam != 0)
            {
                s = MODFRA.SyGet_Fra(uow, 8804, "E", MODGDOC.forma(MODXDOC.TipCam, MODXDOC.FormatoTipCamb));
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 0; i <= MODXDOC.Lineas.Length; i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 612.
        // Observaciones  : Contiene Párrafos, Detalle, Información General,
        // ****************************************************************************
        public void Pr_Principal_8(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int n = MODXDOC.VDetalle.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VDetalle.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 7;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado sólo para el caso de la carta Exportador.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Ordenante(documento);     // Distribuciones.
            }
            Pr_Informacion(documento, uow);     // Información.
            Pr_Pie_Pagina(documento, uow);     // Pie de Página.
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Agosto 1995
        // Propósito      : Llamada desde Carta: Exportador Nro 613.
        // Observaciones  : Contiene Párrafos, Detalle, Información General,
        // ****************************************************************************
        public void Pr_Principal_9(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int x = 0;
            string s = "";
            int e = MODXDOC.VxOri == null ? 0 : MODXDOC.VxOri.Length;
            int d = MODXDOC.VxVia == null ? 0 : MODXDOC.VxVia.Length;
            int m = MODXDOC.VDet.Length;
            int n = MODXDOC.VPlan.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VPlan.GetUpperBound(0);
            //    m = MODXDOC.VDet.GetUpperBound(0);
            //    d = MODXDOC.VxVia.GetUpperBound(0);
            //    e = MODXDOC.VxOri.GetUpperBound(0);
            //});

            MODXDOC.linea = 1;
            MODXDOC.Pagina = 1;
            MODXDOC.Carta = 8;
            MODXDOC.Num_Carta = MODXDOC.Carta;
            MODXDOC.SetupLetras();     // Seteo de Letras.
            MODXDOC.Pr_Titulos(documento, MODXDOC.Carta);     // Títulos del documento.
            MODXDOC.xDoc_Exp(documento, MODXDOC.IExp1);     // Encabezado sólo para el caso de la carta Exportador.
            Pr_Parrafo_1(documento, uow, MODXDOC.Carta);     // Primer Párrafo.
            if (n > 0)
            {
                MODXDOC.Pr_Planilla(documento);     // Planillas.
            }
            if (d > 0 || e > 0)
            {
                Pr_Abonos(documento);     // Cargos y Abonos.
            }
            if (m > 0)
            {
                Pr_Cta_Cte(documento, uow, MODXDOC.DocxRegPln);     // Cuenta Corriente(Débitos).
            }
            Pr_Informacion(documento, uow);     // Información.
            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();
            if (MODXDOC.TipCam != 0)
            {
                s = MODFRA.SyGet_Fra(uow, 8804, "E", MODGDOC.forma(MODXDOC.TipCam, MODXDOC.FormatoTipCamb));
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                {
                    if (MODXDOC.Lineas[i].TrimB() != "")
                    {
                        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                        MODXDOC.Pr_Salto_Pagina();
                    }
                }
            }
            Pr_Pie_Pagina(documento, uow);     // Pie de Página
            ImprimeFirma();
            if (MODXDOC.Pagina > 1)
            {
                MODXDOC.ImprimePagina();
            }
            //MigrationSupport.Printer.DefInstance.EndDoc();

        }
        // ****************************************************************************
        //    1.  Es el despliegue del texto libre que existe en la carta al Exportador.
        // ****************************************************************************
        private void Pr_Texto_Libre()
        {
            int i = 0;
            int x = 0;
            string s = "";

            // Orieta.
            // Printer.Print : Call Pr_Salto_Pagina
            // Orieta.

            if (MODXDOC.Instrucciones.TrimB() != "")
            {
                s = MODXDOC.Instrucciones + 13.Char() + 10.Char();
            }
            else
            {
                s = "";
            }
            //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
            //for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
            //{
            //    if (MODXDOC.Lineas[i].TrimB() != "")
            //    {
            //        // Orieta.
            //        if (i == 1)
            //        {
            //            MigrationSupport.Printer.DefInstance.Print();
            //            MODXDOC.Pr_Salto_Pagina();
            //        }
            //        // Orieta.
            //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
            //        MODXDOC.Pr_Salto_Pagina();
            //    }
            //}
            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el Tipo de Embarque.
        // Observaciones  : Coloca un texto semi - fijo con la descripción de la
        //                  Mercadería, para luego continuar con el detalle del
        //                  embarque.
        // ****************************************************************************
        private void Pr_Tipo_Embarque(DocumentoReporteModel documento, UnitOfWorkCext01 uow, int Carta_Aux)
        {
            int i = 0;
            int x = 0;
            string s = "";
            string s3 = "";
            string s2 = "";
            int m = 0;
            string s1 = "";
            string p = "";
            int n = MODXDOC.VxCem.Length;

            MigrationSupport.Printer.DefInstance.Print();
            MODXDOC.Pr_Salto_Pagina();

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = MODXDOC.VxCem.GetUpperBound(0);
            //});

            // Al Exportador.-
            if (Carta_Aux == 2)
            {

                // Condición de Entrega.-
                p = MODGDOC.CopiarDeString(MODXDOC.VxCob.Condicion, ";", 1).TrimB();
                s1 = MODFRA.SyGet_Fra(uow, 6723, "E", p);
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                MigrationSupport.Printer.DefInstance.Print(s1.TrimB());
                
                MODXDOC.Pr_Salto_Pagina();
                MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();

                // Frase de Devolución.-
                m = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 21).ToInt();
                if (m > 0)
                {
                    p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 22);
                    s2 = MODFRA.SyGet_Fra(uow, m, "E", p) + 13.Char() + 10.Char();
                    // Printer.Print s$: Call Pr_Salto_Pagina
                }

                // Frase de Protesto.-
                m = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 17).ToInt();
                p = MODGDOC.CopiarDeString(MODXDOC.VInsEsp.Frases, ";", 18);
                s3 = MODFRA.SyGet_Fra(uow, m, "E", p);
                // Printer.Print s$: Call Pr_Salto_Pagina

                s = s2 + s3;
                //TODO ARKANO x = MODXDOC.GetLines(s,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 1; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}


            }

            // Mercadería.
            if (MODXDOC.VxCob.Mercad_t != "")
            {
                switch (Carta_Aux)
                {
                    case 1:
                    case 3:
                        if (MODXDOC.Idioma == "I")
                        {
                            p = MODXDOC.VxCob.Mercad_t.TrimB();
                            s1 = MODFRA.SyGet_Fra(uow, 6721, "I", p);
                            // s1$ = "This collection cover shipment of " + Trim$(VxCob.Mercad_t)
                        }
                        else
                        {
                            p = MODXDOC.VxCob.Mercad_t.TrimB();
                            s1 = MODFRA.SyGet_Fra(uow, 6722, "E", p);
                            // s1$ = "Esta cobranza cubre el embarque de :   " + Trim$(VxCob.Mercad_t)
                        }
                        break;
                    default:
                        p = MODXDOC.VxCob.Mercad_t.TrimB();
                        s1 = MODFRA.SyGet_Fra(uow, 6722, "E", p);
                        // s1$ = "Esta cobranza cubre el embarque de :   " + Trim$(VxCob.Mercad_t)
                        break;
                }

                //TODO ARKANO x = MODXDOC.GetLines(s1,CajaMultilinea,ref MODXDOC.Lineas);
                //for (i = 0; i <= MODXDOC.Lineas.GetUpperBound(0); i += 1)
                //{
                //    if (MODXDOC.Lineas[i].TrimB() != "")
                //    {
                //        MigrationSupport.Printer.DefInstance.Print(MODXDOC.Lineas[i]);
                //        MODXDOC.Pr_Salto_Pagina();
                //    }
                //}
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
            }
            documento.Paso2 = s1;
            // Conocimientos de Embarque.
            if (n != 0)
            {
                switch (Carta_Aux)
                {
                    case 1:
                    case 3:
                        if (MODXDOC.Idioma == "I")
                        {
                            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Document Nr.");
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(37).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Shipment Date");
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(55).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Loading Port");
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(94).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Discharge Port");
                            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        }
                        else
                        {
                            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("C. Embarque");
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(37).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("F. Embarque");
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(55).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Embarque");
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(94).Column);
                            MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Destino");
                            MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        }
                        break;
                    default:
                        MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("C. Embarque");
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(37).Column);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("F. Embarque");
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(55).Column);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Embarque");
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(94).Column);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P. Destino");
                        MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        break;
                }
                MODXDOC.Pr_Salto_Pagina();
                for (i = 0; i < n; i += 1)
                {
                    if (MODXDOC.VxCem[i].NumCem.TrimB() != "")
                    {
                        var embarque = new Embarque();
                        embarque.Codigo = MODXDOC.VxCem[i].NumCem.Mid(1, 30).TrimB();
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(0).Column);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MODXDOC.VxCem[i].NumCem.Mid(1, 30).TrimB());
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(39).Column);
                        // Para la carta en inglés el formato de la fecha es mm/dd/yyyy.
                        switch (Carta_Aux)
                        {
                            case 1:
                            case 3:
                                if (MODXDOC.Idioma == "I")
                                {
                                    MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "mm/dd/yyyy").TrimB());
                                    embarque.Fecha = MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "mm/dd/yyyy").TrimB();
                                }
                                else
                                {
                                    MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "dd/mm/yyyy").TrimB());
                                    embarque.Fecha = MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "dd/mm/yyyy").TrimB();
                                }
                                break;
                            default:
                                MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "dd/mm/yyyy").TrimB());
                                embarque.Fecha = MigrationSupport.Utils.Format(MODXDOC.VxCem[i].FecCem, "dd/mm/yyyy").TrimB();
                                break;
                        }
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(58).Column);
                        embarque.Origen = MODXDOC.VxCem[i].EmbDes.Mid(1, 30).TrimB();
                        embarque.Destino = MODXDOC.VxCem[i].EmbHac.Mid(1, 30).TrimB();
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MODXDOC.VxCem[i].EmbDes.Mid(1, 30).TrimB());
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(100).Column);
                        MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MODXDOC.VxCem[i].EmbHac.Mid(1, 30).TrimB());
                        documento.LineasEmbarque.Add(embarque);
                        MODXDOC.Pr_Salto_Pagina();
                    }
                }
                MigrationSupport.Printer.DefInstance.Print();
                MODXDOC.Pr_Salto_Pagina();
            }

        }

        public int ProcesaComando(DocumentoReporteModel documento, UnitOfWorkCext01 uow, string Comando)
        {
            return 0;
            //int ProcesaComando = 0;

            //string MsgErrImp = "";
            //int c = 0;
            //string Numero_Operacion = "";
            //string Cmd = "";
            //int largo_total = 0;
            //string texto_largo = "";
            //string ope = "";
            //int doc = 0;
            //int cor = 0;
            //double primero = 0.0;
            //double segundo = 0.0;
            //double tercero = 0.0;
            //int cuarto = 0;
            //try
            //{


            //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            //    Cmd = MODGDOC.CopiarDeString(Comando, 9.Char(), 1).TrimB();
            //    if (Cmd == "9")
            //    {
            //        Environment.Exit(0);
            //    }

            //    MODXDOC.VOpe.NumOpe = MODGDOC.CopiarDeString(Comando, 9.Char(), 2).TrimB();
            //    MODXDOC.VOpe.TipoDoc = MODGDOC.CopiarDeString(Comando, 9.Char(), 3).ToInt();
            //    MODXDOC.VOpe.NumCor = MODGDOC.CopiarDeString(Comando, 9.Char(), 4).ToInt();
            //    MODXDOC.VOpe.NumCop = MODGDOC.CopiarDeString(Comando, 9.Char(), 5).ToInt();

            //    if (MODXDOC.VOpe.NumCop == 0)
            //    {
            //        MODXDOC.VOpe.NumCop = 1;
            //    }
            //    Numero_Operacion = MODXDOC.VOpe.NumOpe + MODXDOC.VOpe.NumCor.Str().TrimB();
            //    MODXDOC.VOpe.NumOpe_t = Numero_Operacion.Mid(1, 3).TrimB() + "-" + Numero_Operacion.Mid(4, 2).TrimB() + "-" + Numero_Operacion.Mid(6, 2).TrimB() + "-" + Numero_Operacion.Mid(8, 3).TrimB() + "-" + Numero_Operacion.Mid(11, 5).TrimB();

            //    MODXDOC01.VDocx.NroMem = MODXDOC01.SyGet_xDoc(uow, MODXDOC.VOpe.NumOpe.TrimB(), MODXDOC.VOpe.NumCor);
            //    if (MODXDOC01.VDocx.NroMem == 0)
            //    {
            //        Environment.Exit(0);
            //    }
            //    else
            //    {
            //        MODXDOC.Memo = MODGMEM.SyGetn_Mem(uow, "x", MODXDOC01.VDocx.NroMem);
            //    }

            //    MODXDOC.tabulador = 71;
            //    MODXDOC.tab_doc_letra = 54;
            //    MODXDOC.tab_doc_cod = 59;
            //    MODXDOC.tab_doc_nem = 65;
            //    MODXDOC.tab_doc_monto = 89;
            //    MODXDOC.tab_mto_descr = 57;
            //    MODXDOC.tab_mto_monto = 65;
            //    MODXDOC.tab_titulos = 45;

            //    MODFRA.Get_RefBae(uow, MODXDOC.VOpe.NumOpe.TrimB());

            //    if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobReg)
            //    {
            //        // Registro Cobranza.
            //        primero = MODGDOC.CopiarDeString(Comando, 9.Char(), 6).ToVal();
            //        segundo = MODGDOC.CopiarDeString(Comando, 9.Char(), 7).ToVal();
            //        tercero = MODGDOC.CopiarDeString(Comando, 9.Char(), 8).ToVal();
            //        cuarto = MODGDOC.CopiarDeString(Comando, 9.Char(), 9).ToInt();
            //        MODXDOC.Pr_Load_Datos_Memo();     // Carga datos.-
            //        if (segundo.ToBool())
            //        {
            //            for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //            {
            //                Pr_Principal_2(service);     // Banco Cobrador.-
            //            }
            //        }
            //        if (tercero.ToBool())
            //        {
            //            for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //            {
            //                Pr_Principal_3(service);     // Exportador.-
            //            }
            //        }
            //        if (cuarto.ToBool() && MODXDOC.PartysOpe.GetUpperBound(0) == MODXDOC.IAge)
            //        {
            //            // Agente.-
            //            for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //            {
            //                Pr_Principal_4(service);
            //            }
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxAceLet)
            //    {
            //        // Aceptación Letras.
            //        MODXDOC.Pr_Load_Datos_Letra();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_5(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobRen)
            //    {
            //        MODXDOC.Pr_Load_Datos_Reenvio();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_1(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxPagDir)
            //    {
            //        // Pago Directo.
            //        MODXDOC.Pr_Load_Exp610();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_6(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCobCan || MODXDOC.VOpe.TipoDoc == MODXDOC.DocxCanRet)
            //    {
            //        // Cancelación.
            //        MODXDOC.Pr_Load_Exp611();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_7(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegRet)
            //    {
            //        // Registro Retorno.
            //        MODXDOC.Pr_Load_Exp612();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_8(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegPln)
            //    {
            //        // Registro Planilla.
            //        MODXDOC.Pr_Load_Exp613();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_9(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocxRegCanRet)
            //    {
            //        // Registro Retorno.
            //        MODXDOC.Pr_Load_Exp614();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_14(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocGAcre || MODXDOC.VOpe.TipoDoc == MODXDOC.DocGAdeb)
            //    {
            //        // Aviso Débito/Crédito.
            //        MODXDOC.Pr_Load_Doc999();
            //        for (c = 1; c <= 1; c += 1)
            //        {
            //            // CPCM.-
            //            Pr_Principal_10(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocCVD)
            //    {
            //        // Compra - Venta.
            //        MODXDOC.Pr_Load_Doc620();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_11(service);
            //        }
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocArb)
            //    {
            //        // Arbitarje.
            //        MODXDOC.Pr_Load_Doc621();
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            Pr_Principal_12(service);
            //        }
            //        // -----------------------------------------'
            //        // Realsystems Nov.2008 Estab.Comex.Grp5.4  '
            //        // -----------------------------------------'
            //    }
            //    else if (MODXDOC.VOpe.TipoDoc == MODXDOC.DocCVDI)
            //    {
            //        DOC_CVDI.Pr_Load_Doc402(MODXDOC.Memo);
            //        for (c = 1; c <= MODXDOC.VOpe.NumCop; c += 1)
            //        {
            //            MODXDOC.SetupLetras();     //  Seteo de Letras.
            //            DOC_CVDI.Header06();
            //            DOC_CVDI.Pr_Principal_13(service);
            //            ImprimeFirma();
            //            Pr_Pie_Pagina() TODO Revisar llamado
            //            MigrationSupport.Printer.DefInstance.EndDoc();
            //        }
            //    }

            //    ProcesaComando = true.ToInt();
            //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            //    return ProcesaComando;

            //}
            //catch (Exception exc)
            //{
            //    MigrationSupport.GlobalException.Initialize(exc);
            //    throw new XegiException("Error al leer en Sds_Sgt_Ejc: [" + MigrationSupport.Utils.Format(" ProcesaComando " + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty)), exc);
            //}
        }
    }
}
