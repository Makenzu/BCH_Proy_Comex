using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Data.DAL.Swift.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Core.BL.SWI200;


namespace BCH.Comex.Core.BL.SWG3
{
    public class Swi200ServiceMT300
    {
        private readonly UnitOfWorkSwift unitOfWork;
        private const string HEADER_BASIC = "{1:F01";
        private const string HEADER_APPLI = "}{2:I";
        private const string TEXTO = "}{4:";
        private const string TEXTO_BENEFICIARIO = "59";
        private List<sw_valor_campos> valores;
        private List<sw_campos_msg> montos;
        private List<sw_campos_msg> bancos;
        private List<sw_campos_msg> fechas;
        private const int MAX_MSG = 10000;
        private const string campoQuince = "15";
        private const string campoVeintdosC = "22C";
        public Swi200ServiceMT300()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public bool IngresaModificaMensajeSwift(int vista, MensajeSwiftSwi200 msg)
        {
            if (vista == 1234)
            {
                if (msg.TipoIngreso == "M")
                {
                    msg.EstadoMensaje = "DIG";
                }
                else
                {
                    msg.EstadoMensaje = "INY";
                }
                msg.Comentario = "Mensaje Ingresado en Origen.";
                return IngresarMensaje(msg);
            }
            else
            {
                msg.EstadoMensaje = "MOD";
                msg.Comentario = "Mensaje Modificado.";
                return EditarMensaje(msg);
            }
        }

        public string Swi200(string mensajeSwift)
        {
            return null;
        }

        private bool IngresarMensaje(MensajeSwiftSwi200 msg)
        {
            VerificaIdMensaje(msg.IdMensaje);
            VerificaExisteMensaje(msg.IdMensaje);

            VerificaSwift(msg);

            if (unitOfWork.MensajeRepository.IngresaMensaje(msg))
            {
                return unitOfWork.MensajeRepository.proc_sw_msgsend_log_i01(msg.IdMensaje, DateTime.Now, msg.RutDigita, "SWIV", msg.TipoIngreso, msg.Casilla, msg.EstadoMensaje, msg.Unidad, "I", msg.Comentario);
            }

            return false;
        }

        private bool EditarMensaje(MensajeSwiftSwi200 msg)
        {
            VerificaIdMensaje(msg.IdMensaje);
            VerificaNoExisteMensaje(msg.IdMensaje);

            VerificaSwift(msg);

            bool retorno = false;

            if (unitOfWork.MensajeRepository.ModificaMensaje(msg))
            {
                retorno = unitOfWork.MensajeRepository.proc_sw_env_graba_mod(msg.IdMensaje, msg.Unidad, msg.RutDigita);
            }
            return retorno;
        }

        private static void VerificaIdMensaje(int idMensaje)
        {
            if (idMensaje <= 0)
            {
                throw new Swi200Exception("04Error en correlativo, debe ser mayor a cero.");
            }
        }

        private void VerificaNoExisteMensaje(int id_mensaje)
        {
            if (unitOfWork.MensajeRepository.Get(id_mensaje) == null)
            {
                throw new Swi200Exception(Swi200Constants.ERROR_CORRELATIVO_NO_EXISTE);
            }
        }

        private void VerificaExisteMensaje(int id_mensaje)
        {
            if (unitOfWork.MensajeRepository.Get(id_mensaje) != null)
            {
                throw new Swi200Exception("06Mensaje " + id_mensaje.ToString() + " ya existe en servidor, verificar correlativo.");
            }
        }

        public void VerificaSwift(MensajeSwiftSwi200 msg)
        {
            VerificaCasillas(msg.Casilla);
            VerificarTexto(msg);
            VerificaMt(msg.TipoMensaje);

            VerificaBanco(msg.BancoEm, msg.BranchEm);

            VerificarPrioridad(msg);

            VerificaTextoMensaje(msg);//deberia ser 56 //TODO CH
        }

        private void VerificaMt(string tipo_msg)
        {
            if (unitOfWork.TiposMensajeRepository.Get(tm => tm.cod_tipo == tipo_msg).FirstOrDefault() == null)
            {
                throw new Swi200Exception(Swi200Constants.ERROR_TIPO_MENSAJE_NO_CORRESPONDE);
            }
        }

        public void VerificarTexto(MensajeSwiftSwi200 msg)
        {
            if ((int)(msg.SubTexto[0]) != 1)
            {
                throw new Swi200Exception("10Error de formato, caracter de inicio del mensaje es incorrecto.");
            }

            if ((int)msg.SubTexto[msg.SubTexto.Length - 1] != 3)
            {
                //TODO, NO COINCIDE EL FINAL DEL TEXTO CON UN 3
                throw new Swi200Exception("11Error de formato, caracter de termino del mensaje es incorrecto.");
            }

            int i = 0;
            while (msg.SubTexto[i++] != '\n') ;
            if (i > 56)
            {
                throw new Swi200Exception(Swi200Constants.ERROR_LARGO_ENCABEZADO);
            }

            if (msg.SubTexto.Substring(1, 6) != HEADER_BASIC)
            {
                throw new Swi200Exception("13Error de formato, encabezado del mensaje no comienza con {1:F01");
            }

            if (msg.SubTexto.Substring(29, 5) != HEADER_APPLI)
            {
                throw new Swi200Exception("15Error de formato, no se encuentra texto }{2:I en encabezado del mensaje.");
            }

            if (msg.SubTexto.Substring(50, 4) != TEXTO)//deberia ser 50, 4
            {
                throw new Swi200Exception("20Error de formato, no se encuentra texto }{4: en el mensaje.");
            }
        }

        private void VerificaCasillas(int casilla)
        {
            if (unitOfWork.CasillaRepository.Get(c => c.cod_casilla == casilla).FirstOrDefault() == null)
            {
                throw new Swi200Exception(Swi200Constants.ERROR_CASILLA_NO_EXISTE);
            }
        }

        private void VerificarPrioridad(MensajeSwiftSwi200 msg)
        {
            if (msg.Prioridad != "S" && msg.Prioridad != "N" && msg.Prioridad != "U")
            {
                throw new Swi200Exception(string.Format("19Error en prioridad, codigo ({0}) no existe.", msg.Prioridad));
            }
        }

        private void VerificaBanco(string banco_em, string branch_em)
        {
            var banco = unitOfWork.PaymentPlusRepository.GetBancoPorSwift(banco_em, branch_em).FirstOrDefault();

            if (banco == null)
            {
                throw new Swi200Exception(string.Format("Código ({0}{1}) no existe.", banco_em, branch_em));
            }
        }

        private List<sw_valor_campos> LlenaMatrizValores(string tipoMensaje)
        {
            return unitOfWork.ValorCamposRepository.LlenaMatrizValores(tipoMensaje);
        }

        private List<sw_campos_msg> LlenaMatrizBancos()
        {
            return unitOfWork.CamposMsgRepository.LlenaMatrizBancos();
        }

        private List<sw_campos_msg> LlenaMatrizMontos()
        {
            return unitOfWork.CamposMsgRepository.LlenaMatrizMontos();
        }

        private List<sw_campos_msg> LlenaMatrizFechas()
        {
            return unitOfWork.CamposMsgRepository.LlenaMatrizFechas();
        }

        private List<proc_sw_trae_fmt_largo_MS_Result> LlenaMatrizMensaje(string tipoMensaje)
        {
            var mensajes = unitOfWork.MensajeRepository.LlenaMatrizMensaje(tipoMensaje);
            if (mensajes.ToList().Count == 0)
            {
                throw new Swi200Exception(string.Format("09Error, no existe formato de mensaje {0}", tipoMensaje));
            }
            return mensajes;
        }

        private void VerificaTextoMensaje(MensajeSwiftSwi200 msg)
        {
            fechas = LlenaMatrizFechas();
            montos = LlenaMatrizMontos();
            bancos = LlenaMatrizBancos();

            valores = LlenaMatrizValores(msg.TipoMensaje);
            List<proc_sw_trae_fmt_largo_MS_Result> Reg_Fmt = LlenaMatrizMensaje(msg.TipoMensaje);

            string texto_msg = msg.SubTexto.Substring(56);
            int i, j, k, caracter, fin_campo, largo_campo, largo_campo_valido, largo_msg;
            int orden_total, orden_ciclo, sec_ciclo, ciclo_actual;
            int pertenece, mismo_orden, tag_veces;
            int mismo_ciclo, ini_ciclo = 0;
            string campo_msg;
            string campo_generico = new string(new char[3]);
            string texto_campo = new string(new char[MAX_MSG]);
            largo_msg = texto_msg.Length;

            orden_total = 0;
            orden_ciclo = 0;
            ciclo_actual = 0;
            sec_ciclo = 0;
            mismo_ciclo = 0;
            i = 0;

            while (i < largo_msg)
            {
                /* Guarda Campo para validar */
                if (texto_msg[i] != ':')
                {
                    throw new Swi200Exception("21Error de formato, codigo de campo no comienza con :");
                }

                i++;
                if (texto_msg[i + 2] == ':')
                {
                    campo_msg = texto_msg.Substring(i, 2);
                    i = i + 2;

                    orden_total++;
                    orden_ciclo++;
                }
                else
                {
                    if (texto_msg[i + 3] == ':')
                    {
                        campo_msg = texto_msg.Substring(i, 3);

                        i = i + 3;

                        orden_total++;
                        orden_ciclo++;
                    }
                    else
                    {
                        throw new Swi200Exception("22Error de formato, codigo de campo no termina con :");
                    }
                }

                /*  Marca campo en la matriz como existente */
                pertenece = 0;
                for (j = 0; j < Reg_Fmt.Count; j++)
                {
                    if ((string.Compare(Reg_Fmt[j].tag_fmt, campo_msg) == 0) && (Reg_Fmt[j].orden_fmt >= orden_total))
                    {
                        Reg_Fmt[j].fmt_existe = "S";
                        pertenece = 1;
                        break;
                    }
                }
                if (pertenece == 0)
                {
                    throw new Swi200Exception(string.Format("23Error en mensaje, campo {0} no corresponde a MT.", campo_msg));
                }

                /* Verifica si hay mas campos genericos obligatorios */
                if (campo_msg.Length >= 2)
                {
                    mismo_orden = 0;

                    for (j = 0; j < Reg_Fmt.Count; j++)
                    {
                        if ((string.Compare(Reg_Fmt[j].status_fmt, "M") == 0) && (string.Compare(Reg_Fmt[j].tag_fmt, campo_msg) == 0) &&
                            (Reg_Fmt[j].orden_fmt >= orden_total))
                        {
                            campo_generico = campo_msg.Substring(0, 2);

                            mismo_orden = Reg_Fmt[j].orden_fmt;
                            mismo_ciclo = Reg_Fmt[j].repeticion_fmt;
                            break;
                        }
                    }

                    for (j = 0; j < Reg_Fmt.Count; j++)
                    {
                        if (string.Compare(Reg_Fmt[j].status_fmt, "M") == 0 && (string.Compare(Reg_Fmt[j].tag_fmt, 0, campo_generico, 0, 2) == 0) && 
                            (Reg_Fmt[j].orden_fmt == mismo_orden) && (Reg_Fmt[j].repeticion_fmt == mismo_ciclo))
                        {
                            Reg_Fmt[j].fmt_existe = "S";
                        }
                    }
                }

                /* Verifica Orden de campo en el mensaje */
                for (j = 0; j < Reg_Fmt.Count; j++)
                {
                    if (string.Compare(Reg_Fmt[j].tag_fmt, campo_msg) == 0)
                        break;
                }
                if (Reg_Fmt[j].repeticion_fmt == 0)
                {
                    tag_veces = 0;
                    for (k = j + 1; k < Reg_Fmt.Count; k++)
                    {
                        if (string.Compare(Reg_Fmt[k].tag_fmt, campo_msg) == 0)
                        {
                            tag_veces++;
                        }
                    }

                    ciclo_actual = 0;
                    if (tag_veces == 0)
                    {
                        if (orden_total <= Reg_Fmt[j].orden_fmt)
                        {
                            orden_total = Reg_Fmt[j].orden_fmt;
                        }
                        else
                        {
                            throw new Swi200Exception(string.Format("24Error en mensaje, orden de campo {0} no corresponde.", campo_msg));
                        }
                    }
                }
                else
                {
                    if (ciclo_actual != Reg_Fmt[j].repeticion_fmt)
                    {
                        sec_ciclo++;
                        ciclo_actual = Reg_Fmt[j].repeticion_fmt;
                        orden_ciclo = Reg_Fmt[j].orden_fmt;
                        ini_ciclo = orden_ciclo;
                    }
                    else
                    {
                        if (Reg_Fmt[j].orden_fmt == ini_ciclo)
                        {
                            orden_ciclo = ini_ciclo;
                            orden_total = ini_ciclo;
                            for (k = 0; k < Reg_Fmt.Count; k++)
                            {
                                if (string.Compare(Reg_Fmt[k].status_fmt, "M") == 0 &&
                                    (string.Compare(Reg_Fmt[k].tag_fmt, campo_msg) != 0) && (Reg_Fmt[k].repeticion_fmt == ciclo_actual))
                                {
                                    Reg_Fmt[k].fmt_existe = "N";
                                }
                            }
                        }
                    }
                    if ((orden_ciclo <= Reg_Fmt[j].orden_fmt) && (ciclo_actual >= sec_ciclo))
                    {
                        orden_ciclo = Reg_Fmt[j].orden_fmt;
                    }
                    else
                    {
                        throw new Swi200Exception(string.Format("24Error en ciclo, orden de campo {0} no corresponde.", campo_msg));
                    }
                }

                /* Guarda Texto del Campo */
                largo_campo_valido = 0;
                largo_campo = 0;
                fin_campo = 0;
                while (fin_campo == 0 && i < largo_msg)
                {
                    caracter = texto_msg[i];
                    i++;
                    if (caracter == 10 && (string.Compare(texto_msg, i, ":", 0, 1) == 0))
                    {
                        fin_campo = 1;
                    }
                    else
                    {
                        if (string.Compare(texto_msg, i, "-}", 0, 2) == 0)
                        {
                            i = i + 3;
                            fin_campo = 1;
                        }
                        else
                        {
                            caracter = texto_msg[i];
                            if ((caracter != 10) && (caracter != 13))
                            {
                                largo_campo_valido++;
                            }
                            texto_campo = texto_campo.Substring(0, largo_campo) + texto_msg[i];

                            largo_campo++;
                        }
                    }
                }
                texto_campo = texto_campo.Substring(0, largo_campo);

                if (largo_campo_valido > 0)
                {
                    for (j = 0; j < Reg_Fmt.Count; j++)
                    {
                        if (string.Compare(Reg_Fmt[j].tag_fmt, campo_msg) == 0)
                        {
                            if (largo_campo_valido > Reg_Fmt[j].largo_total)
                            {
                                throw new Swi200Exception(string.Format("26Error de formato, largo del campo {0} no corresponde.", campo_msg));
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (!campo_msg.StartsWith(campoQuince) && !campo_msg.StartsWith(campoVeintdosC))
                    {
                        throw new Swi200Exception(string.Format("27Error de formato, largo del campo {0} debe ser mayor a cero.", campo_msg));
                    }
                }
                if (!campo_msg.StartsWith(campoQuince) && !campo_msg.StartsWith(campoVeintdosC))
                {
                    VerificaTextoCampo(msg, campo_msg, texto_campo);
                }
            }

            /* Verifica si falta campo mandatorio */
            for (j = 0; j < Reg_Fmt.Count; j++)
            {
                if ((string.Compare(Reg_Fmt[j].status_fmt, "M") == 0) && (string.Compare(Reg_Fmt[j].fmt_existe, "N") == 0))
                {
                    if (Reg_Fmt[j].repeticion_fmt == 0)
                    {
                        throw new Swi200Exception(string.Format("25Error en mensaje, campo {0} es mandatorio.", Reg_Fmt[j].tag_fmt));
                    }
                    else
                    {
                        throw new Swi200Exception(string.Format("25Error en ciclo, campo {0} es mandatorio.", Reg_Fmt[j].tag_fmt));
                    }
                }
            }
        }

        //TODO CH
        private void VerificaTextoCampo(MensajeSwiftSwi200 msg, string campo, string textoCampo)
        {
            int largo_campo;
            int largo_fila;
            int j;
            int fin_fila;
            int no_fila;
            int caracter;
            int Existe_Ben = 0;
            string texto_fila = new string(new char[MAX_MSG]);
            string Beneficiario;

            no_fila = 0;
            largo_campo = textoCampo.Length;
            j = 0;
            while (j < largo_campo)
            {
                no_fila++;
                fin_fila = 0;
                largo_fila = 0;
                while (fin_fila == 0 && j < largo_campo)
                {
                    caracter = textoCampo[j];
                    if (caracter == 13)
                    {
                        caracter = textoCampo[j + 1];
                        if (caracter != 10)
                        {
                            throw new Swi200Exception(string.Format("28Error en campo {0}, falta caracter de fin de linea.", campo));
                        }
                        fin_fila = 1;
                        j = j + 2;
                    }
                    else
                    {
                        texto_fila = texto_fila.Substring(0, largo_fila) + textoCampo[j];
                        largo_fila++;
                        j++;
                    }
                }
                texto_fila = texto_fila.Substring(0, largo_fila);
                VerificaLargoFila(campo, no_fila, largo_fila);

                //Si es el campo 59 y que es la primera fila y que la linea no trae datos, no realiza la validacion
                if (!(string.Compare(campo, TEXTO_BENEFICIARIO) == 0 && no_fila == 1 && texto_fila.Trim().Length == 0))
                {
                    VerificaFilaCampo(campo, texto_fila, no_fila);
                }

                VerificaCampoMonto(msg, campo, no_fila, texto_fila);

                VerificaCampoBanco(campo, no_fila, texto_fila);

                VerificaCampoFecha(campo, no_fila, texto_fila);

                if (string.Compare(campo, 0, TEXTO_BENEFICIARIO, 0, 2) == 0 && no_fila != 1 && (Existe_Ben != 2))
                {
                    Existe_Ben = 2;
                    Beneficiario = texto_fila;
                    largo_campo = Beneficiario.Length;
                }
            }
        }

        private void VerificaFilaCampo(string campo, string texto_fila, int n_fila)
        {
            int i;
            int j;
            int k;
            int largo_texto;
            int caracter;
            int cont_blancos;
            int tot_valor;
            int pos_sep;
            string cond_valor;
            string valor_campo;
            string valor_opc;
            string valor_ok;
            string lista_valor;
            string DetalleOut = string.Empty;

            cont_blancos = 0;
            largo_texto = texto_fila.Length;
            i = 0;
            while (i < largo_texto)
            {
                caracter = (int)texto_fila[i];

                if (caracter == 32)
                {
                    cont_blancos++;
                }

                i++;
            }

            if (cont_blancos == i)
            {
                throw new Swi200Exception(string.Format("Campo {0} no puede estar completo con blancos.", campo));
            }

            /* Verifica Valores en Texto de Campos */
            for (j = 0; j < valores.Count; j++)
            {
                if ((string.Compare(campo, valores[j].tag_campo) == 0) && (n_fila == valores[j].linea_campo))
                    break;
            }

            if (j < valores.Count)
            {
                valor_ok = "N";
                cond_valor = valores[j].cond_valor;
                tot_valor = valores[j].total_valor.Value;
                valor_campo = valores[j].valor_campo;

                lista_valor = valor_campo;
                var indice = lista_valor.Substring(0, lista_valor.Length - 1).LastIndexOf(";");
                if (indice >= 0)
                {
                    lista_valor = lista_valor.Substring(indice + 1);
                }

                if (string.Compare(cond_valor, 0, "=", 0, 1) == 0)
                {
                    DetalleOut = string.Format("Valor en campo {0} linea {1:D}, debe ser igual a :\n{2}\n", campo, n_fila, lista_valor);
                    for (k = 1; k <= tot_valor; k++)
                    {

                        pos_sep = valor_campo.IndexOf(";");
                        valor_opc = valor_campo.Substring(0, pos_sep);

                        pos_sep++;
                        valor_campo = valor_campo.Substring(pos_sep);
                        if (string.Compare(texto_fila, valor_opc) == 0)
                        {
                            valor_ok = "S";
                            break;
                        }
                    }
                }

                if (string.Compare(cond_valor, 0, "<>", 0, 2) == 0)
                {
                    DetalleOut = string.Format("Valor en campo {0} linea {1:D}, debe ser distinto a :\n{2}\n", campo, n_fila, lista_valor);
                    for (k = 1; k <= tot_valor; k++)
                    {

                        pos_sep = valor_campo.IndexOf(";");
                        valor_opc = valor_campo.Substring(0, pos_sep);

                        pos_sep++;
                        valor_campo = valor_campo.Substring(pos_sep);
                        if (string.Compare(texto_fila, valor_opc) == 0)
                        {
                            valor_ok = "N";
                            break;
                        }
                    }
                    if (k >= tot_valor)
                    {
                        valor_ok = "S";
                    }
                }

                if (string.Compare(cond_valor, 0, "like", 0, 4) == 0)
                {
                    DetalleOut = string.Format("Valor en campo {0} linea {1:D}, debe comenzar con :\n{2}\n", campo, n_fila, lista_valor);
                    for (k = 1; k <= tot_valor; k++)
                    {

                        pos_sep = valor_campo.IndexOf(";");
                        valor_opc = valor_campo.Substring(0, pos_sep);

                        if (string.Compare(texto_fila, 0, valor_opc, 0, pos_sep) == 0)
                        {
                            valor_ok = "S";
                            break;
                        }
                        pos_sep++;
                        valor_campo = valor_campo.Substring(pos_sep);
                    }
                }

                if (string.Compare(cond_valor, 0, "not like", 0, 8) == 0)
                {
                    DetalleOut = string.Format("Valor en campo {0} linea {1:D}, no debe comenzar con:\n{2}", campo, n_fila, lista_valor);
                    for (k = 1; k <= tot_valor; k++)
                    {

                        pos_sep = valor_campo.IndexOf(";");
                        valor_opc = valor_campo.Substring(0, pos_sep);

                        if (string.Compare(texto_fila, 0, valor_opc, 0, pos_sep) == 0)
                        {
                            valor_ok = "N";
                            break;
                        }
                        pos_sep++;
                        valor_campo = valor_campo.Substring(pos_sep);
                    }
                    if (k >= tot_valor)
                    {
                        valor_ok = "S";
                    }
                }
                if (string.Compare(valor_ok, "N") == 0)
                {
                    throw new Swi200Exception(DetalleOut);
                }
            }
        }

        private void VerificaLargoFila(string campo, int no_fila, int largo_fila)
        {
            var registro = unitOfWork.CamposMsgRepository.Get(c => c.tag_campo == campo && c.linea_campo == no_fila).FirstOrDefault();
            if (registro == null)
                throw new Swi200Exception(string.Format("No existe la fila {0:D} para el campo {1}.", no_fila, campo));
            if (largo_fila > registro.largo_campo)
                throw new Swi200Exception(string.Format("La fila {0:D} del campo {1}, sobrepasa el largo máximo.", no_fila, campo));
        }

        private void VerificaCampoMonto(MensajeSwiftSwi200 msg, string campo, int no_fila, string texto_fila)
        {
            int i;
            int largo_monto;
            int caracter;
            int veces_coma;
            string ch_monto = new string(new char[31]);
            string ch_moneda = new string(new char[4]);
            double monto;

            for (i = 0; i < montos.Count; i++)
            {
                if ((string.Compare(montos[i].tag_campo, campo) == 0) && (montos[i].linea_campo == no_fila))
                {
                    if (string.Compare(montos[i].formato_campo, 0, "06G 03C 15", 0, 10) == 0)
                    {
                        ch_moneda = texto_fila.Substring(6, 3);
                        ch_monto = texto_fila.Substring(9);
                        break;
                    }
                    else
                    {
                        if ((string.Compare(montos[i].formato_campo, 0, "03C 15", 0, 6) == 0) ||
                            (string.Compare(montos[i].formato_campo, 0, "03A 15", 0, 6) == 0))
                        {
                            ch_moneda = texto_fila.Substring(0, 3);
                            ch_monto = texto_fila.Substring(3);
                            break;
                        }
                        else
                        {
                            if (string.Compare(montos[i].formato_campo, 0, "01A 06G 03C 15", 0, 14) == 0)
                            {
                                ch_moneda = texto_fila.Substring(7, 3);
                                ch_monto = texto_fila.Substring(10);
                                break;
                            }
                            else
                            {
                                if (string.Compare(montos[i].formato_campo, 0, "01A 03D 02A 03C 15", 0, 18) == 0)
                                {
                                    ch_moneda = texto_fila.Substring(6, 3);
                                    ch_monto = texto_fila.Substring(9);
                                    break;
                                }
                                else
                                {
                                    if (string.Compare(montos[i].formato_campo, 0, "05D 03C 15", 0, 10) == 0)
                                    {
                                        ch_moneda = texto_fila.Substring(5, 3);
                                        ch_monto = texto_fila.Substring(8);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (i >= montos.Count)
            {
                return;
            }
            VerificaMoneda(ch_moneda);

            /* Verifica formato de monto */
            largo_monto = ch_monto.Length;

            for (i = 0; i < largo_monto; i++)
            {
                caracter = (int)ch_monto[i];
                if (((caracter < 48) || (caracter > 57)) && (caracter != 46) && (caracter != 44))
                    break;
            }
            if (i < largo_monto)
            {
                throw new Swi200Exception(string.Format("37Error en campo {0}, el monto ({1}) contiene espacios o caracteres no permitidos.", campo, ch_monto));
            }

            for (i = 0; i < largo_monto; i++)
            {
                caracter = (int)ch_monto[i];
                if (caracter == 46)
                    break;
            }
            if (i < largo_monto)
            {
                throw new Swi200Exception(string.Format("37Error en campo {0}, el monto no debe contener puntos.", campo));
            }

            veces_coma = 0;
            for (i = 0; i < largo_monto; i++)
            {
                caracter = (int)ch_monto[i];
                if (caracter == 44)
                {
                    veces_coma++;
                }
            }
            if (veces_coma == 0)
            {
                throw new Swi200Exception(string.Format("37Error en campo {0}, el monto debe contener coma decimal.", campo));
            }
            if (veces_coma > 1)
            {
                throw new Swi200Exception(string.Format("37Error en campo {0}, el monto debe contener solo una coma decimal.", campo));
            }

            monto = Double.Parse(ch_monto);

            if (msg.Moneda == ch_moneda && msg.Monto < monto)
            {
                msg.Monto = monto;
            }
        }

        private void VerificaCampoBanco(string campo, int no_fila, string texto_fila)
        {
            int i;
            string ch_codbco = new string(new char[9]);
            string ch_branch = new string(new char[4]);

            texto_fila = texto_fila.PadRight(11);
            for (i = 0; i < bancos.Count; i++)
            {
                if ((string.Compare(bancos[i].tag_campo, campo) == 0) && (bancos[i].linea_campo == no_fila) && 
                    (string.Compare(bancos[i].formato_campo, 0, "11T", 0, 3) == 0))
                {
                    ch_codbco = texto_fila.Substring(0, 8);
                    ch_branch = texto_fila.Substring(8, 3);
                    if (string.IsNullOrWhiteSpace(ch_branch))
                    {
                        ch_branch = "XXX";
                    }
                    break;
                }
            }
            if (i >= bancos.Count)
            {
                return;
            }
            VerificaBanco(ch_codbco, ch_branch);
        }

        private void VerificaCampoFecha(string campo, int no_fila, string texto_fila)
        {
            int i;
            string siglo = new string(new char[3]);
            string decada;
            string ch_ano;
            string ch_mes;
            string ch_dia;
            int n_ano = 0;
            int n_mes = 0;
            int n_dia = 0;

            for (i = 0; i < fechas.Count; i++)
            {
                if ((string.Compare(fechas[i].tag_campo, campo) == 0) && (fechas[i].linea_campo == no_fila))
                {
                    if (string.Compare(fechas[i].formato_campo, 0, "06G", 0, 3) == 0)
                    {
                        decada = texto_fila.Substring(0, 2);
                        ch_mes = texto_fila.Substring(2, 2);
                        ch_dia = texto_fila.Substring(4, 2);
                        n_ano = Convert.ToInt32(decada);
                        if (n_ano > 79)
                        {
                            siglo = "19";
                        }
                        else
                        {
                            if (n_ano <= 79)
                            {
                                siglo = "20";
                            }
                        }
                        ch_ano = siglo + decada;
                        n_ano = Convert.ToInt32(ch_ano);
                        n_mes = Convert.ToInt32(ch_mes);
                        n_dia = Convert.ToInt32(ch_dia);
                        break;
                    }
                    else
                    {
                        if (string.Compare(fechas[i].formato_campo, 0, "08G", 0, 3) == 0)
                        {
                            ch_ano = texto_fila.Substring(0, 4);
                            ch_mes = texto_fila.Substring(4, 2);
                            ch_dia = texto_fila.Substring(6, 2);
                            n_ano = Convert.ToInt32(ch_ano);
                            n_mes = Convert.ToInt32(ch_mes);
                            n_dia = Convert.ToInt32(ch_dia);
                            break;
                        }
                    }
                }
            }
            if (i >= fechas.Count)
            {
                return;
            }
            if (n_ano <= 1900)
            {
                throw new Swi200Exception("Error con el año");
            }
            if ((n_mes < 1) || (n_mes > 12))
            {
                throw new Swi200Exception("Error en el mes");
            }
            else
            {
                if ((n_mes == 1) || (n_mes == 3) || (n_mes == 5) || (n_mes == 7) || (n_mes == 8) || (n_mes == 10) || (n_mes == 12))
                {
                    if ((n_dia < 1) || (n_dia > 31))
                    {
                        throw new Swi200Exception("Día fuera de rango");
                    }
                }
                else
                {
                    if ((n_mes == 4) || (n_mes == 6) || (n_mes == 9) || (n_mes == 11))
                    {
                        if ((n_dia < 1) || (n_dia > 30))
                        {
                            throw new Swi200Exception("Día fuera de rango");
                        }
                    }
                    else
                    {
                        if (n_mes == 2)
                        {
                            if ((n_ano % 4 == 0) && (n_ano % 100 != 0) || (n_ano % 400 == 0))
                            {
                                if ((n_dia < 1) || (n_dia > 29))
                                {
                                    throw new Swi200Exception("Día fuera de rango en  bisiesto");
                                }
                            }
                            else
                            {
                                if ((n_dia < 1) || (n_dia > 28))
                                {
                                    throw new Swi200Exception("Día fuera de rango en febrero");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void VerificaMoneda(string moneda)
        {
            var registro = unitOfWork.MonedaRepository.Get(m => m.cod_moneda_sw == moneda && m.uso_moneda_banco == "S").FirstOrDefault();
            if (registro == null)
                throw new Swi200Exception("Código de moneda no existe");
        }
    }
}
