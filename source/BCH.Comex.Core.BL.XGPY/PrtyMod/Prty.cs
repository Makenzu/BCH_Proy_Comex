using BCH.Comex.Common.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPY.PrtyMod
{
    public class Prty
    {
        public string idPrty { get; set; }
        public string bNumber { get; set; }
        public EstadoPrty estado { get; set; }
        public TipoPrty tipo { get; set; }
        public int flag { get; set; }
        public int clasificacion { get; set; }
        public bool siRut { get; set; }
        public string rut { get; set; }
        public String creaCosto { get; set; }
        public String creaUser { get; set; }
        public string modCosto { get; set; }
        public string modUser { get; set; }
        public Boolean multiple { get; set; }
        public string oficina { get; set; }
        public string ejecutivo { get; set; }
        public string actividad { get; set; }
        public string riesgo { get; set; }
        public int codigoBanco { get; set; }
        public Boolean tasaPrime { get; set; }
        public Boolean tasaLibor { get; set; }
        public double spread { get; set; }
        public string swif { get; set; }
        public int plazaAladi { get; set; }
        public string ejeCorr { get; set; }
        public int flagIns { get; set; }
        public string insGenImp { get; set; }
        public string insGenExp { get; set; }
        public string insGenSer { get; set; }
        public string insCobImp { get; set; }
        public string insCobExp { get; set; }
        public string insCreImp { get; set; }
        public string insCreExp { get; set; }
        public Boolean isCity { get; set; }
        public String llave { get; set; }
        public String searchIdPrty { get; set; }
        public RespStatus status { get; set; }
        public Boolean cuentasEliminadas { get; set; }
        public Boolean pertenece { get; set; }
        public Boolean borrado { get; set; }
        public NextStatus nextStatus { get; set; }
        public Boolean primera { get; set; }
        public Boolean esBanco { get; set; }
        public Boolean banco { get; set; }

        public List<PrtyRazonSocial> listaRazonSocial { get; set; }
        public List<PrtyDireccion> listaDireccion { get; set; }
        //public List<PrtyEjecutivos> listaEjecutivos { get; set; }
        //public List<PrtyEspecialista> listaEspecialistas { get; set; }
        public List<PrtyTasaCom> listaTasaCom { get; set; }
        public List<PrtyTasaGas> listaTasaGas { get; set; }
        public List<PrtyTasaInt> listaTasaInt { get; set; }
        public List<PrtyCuenta> listaCuentas { get; set; }
        public List<PrtyCtaBanco> listaCtaBanco { get; set; }
        public List<PrtyLinBanco> listaLinBanco { get; set; }
        public List<PrtyCtaCliente> listaCtaCliente { get; set; }
        public NivelAcceso nivelDeAcceso { get; set; }


        private XgpyService m_XgpyService = new XgpyService();

        public Prty()
        {
            this.Init();
        }
        public Prty(String idPrty)
        {
            this.Init();
            this.status = Load(idPrty);
        }

        public RespStatus Load(String searchIdPrty, Boolean typeMode = true)
        {
            String searchId;
            RespStatus respStatus = RespStatus.Error;
            List<String> resultIdPrty = null;

            this.searchIdPrty = searchIdPrty;

            if (typeMode)
            {
                if (this.isCity && !String.IsNullOrEmpty(this.bNumber))
                {
                    this.bNumber = searchIdPrty.PadRight(12, '|');

                    resultIdPrty = m_XgpyService.Sce_Prty_S07_MS(this.bNumber).ToList();
                }
                else
                {
                    this.llave = searchIdPrty.PadRight(12, '|');

                    resultIdPrty = m_XgpyService.Sce_Prty_S07_MS(this.llave).ToList();
                }

                if (resultIdPrty != null)
                {
                    if (resultIdPrty.Count > 0)
                    {
                        respStatus = RespStatus.PrtyExiste;
                    }
                    else
                    {
                        respStatus = RespStatus.PrtyNoExiste;
                    }
                }
                else
                {
                    respStatus = RespStatus.PrtyNoExiste;
                }

                if (respStatus == RespStatus.PrtyNoExiste)
                {
                    if (this.ExistBaseBIC())
                    {
                        respStatus = RespStatus.PrtyExisteEnBaseBic;
                    }

                    return respStatus;
                }
            }

            this.estado = EstadoPrty.Leido;
            this.cuentasEliminadas = true;
            searchId = searchIdPrty.PadRight(12, '|');

            var resultPrty = m_XgpyService.Sce_Prty_S08_MS(searchId);
            if (resultPrty == null)
            {
                this.estado = EstadoPrty.NoExiste;
                return RespStatus.PrtyNoExiste;
            }

            this.idPrty = resultPrty.id_party;
            this.borrado = resultPrty.borrado;
            if (this.borrado)
            {
                if (this.nextStatus == NextStatus.End)
                {
                    this.pertenece = true;
                    this.nextStatus = NextStatus.DialogPrtyEliminado;
                }
            }
            switch ((int)resultPrty.tipo_party)
            {
                case 0:
                    this.tipo = TipoPrty.Individuo;
                    break;

                case 1:
                    this.tipo = TipoPrty.Banco;
                    break;

                case 2:
                    this.tipo = TipoPrty.Cliente;
                    break;

                default:
                    this.tipo = TipoPrty.NoTipificado;
                    break;
            }

            if (this.tipo == TipoPrty.Cliente)
            {
                this.isCity = true;
            }
            else
            {
                if (this.tipo == TipoPrty.NoTipificado)
                {
                    this.isCity = true;
                    this.tipo = TipoPrty.Cliente;
                    this.bNumber = resultPrty.id_party;
                }
                else
                {
                    this.isCity = false;
                }
            }
            this.flag = (int)resultPrty.flag;
            this.clasificacion = (int)resultPrty.clasificac;
            this.siRut = resultPrty.tiene_rut;
            this.rut = resultPrty.rut;
            this.creaCosto = resultPrty.crea_costo;
            this.creaUser = resultPrty.crea_user;
            if (String.IsNullOrEmpty(this.creaCosto) || String.IsNullOrEmpty(this.creaUser))
            {
                if (this.nextStatus == NextStatus.End)
                {
                    this.nextStatus = NextStatus.DialogAsociaCartera;
                }
            }
            this.modCosto = resultPrty.mod_costo;
            this.modUser = resultPrty.mod_user;
            this.multiple = resultPrty.multiple;
            switch (this.tipo)
            {
                case TipoPrty.Cliente:
                case TipoPrty.Individuo:
                    this.oficina = resultPrty.cod_ofieje;
                    this.ejecutivo = resultPrty.cod_eject;
                    this.actividad = resultPrty.cod_acteco;
                    this.riesgo = resultPrty.clase_ries;
                    break;

                case TipoPrty.Banco:
                    this.codigoBanco = (int)resultPrty.cod_bco;
                    this.tasaLibor = resultPrty.tasa_libor;
                    this.tasaPrime = resultPrty.tasa_prime;
                    this.spread = (int)resultPrty.spread;
                    this.swif = resultPrty.swift;
                    this.plazaAladi = (int)resultPrty.plaza_alad;
                    this.ejeCorr = resultPrty.ejec_corre;
                    break;
            }
            this.flagIns = (int)resultPrty.flagins;
            this.insGenImp = Convert.ToString(resultPrty.insgen_imp);
            this.insGenExp = Convert.ToString(resultPrty.insgen_exp);
            this.insCobImp = Convert.ToString(resultPrty.inscob_imp);
            this.insCobExp = Convert.ToString(resultPrty.inscob_exp);
            this.insCreImp = Convert.ToString(resultPrty.inscre_imp);
            this.insCreExp = Convert.ToString(resultPrty.inscre_exp);

            if (this.nextStatus == NextStatus.End)
            {
                var resultPrtyRazonesSociales = m_XgpyService.Sce_Rsa_S07_MS(this.idPrty);
                if (resultPrtyRazonesSociales != null)
                {
                    this.listaRazonSocial = new List<PrtyRazonSocial>();
                    for (int i = 0; i < resultPrtyRazonesSociales.Count; i++)
                    {
                        PrtyRazonSocial prtyRazonSocial = new PrtyRazonSocial();
                        prtyRazonSocial.borrado = resultPrtyRazonesSociales[i].borrado;
                        prtyRazonSocial.indice = (int)resultPrtyRazonesSociales[i].id_nombre;
                        prtyRazonSocial.nombre = resultPrtyRazonesSociales[i].razon_soci.Replace("'", "`");
                        prtyRazonSocial.fantasia = resultPrtyRazonesSociales[i].nom_fantas.Replace("'", "`");
                        prtyRazonSocial.contacto = resultPrtyRazonesSociales[i].contacto.Replace("'", "`");
                        prtyRazonSocial.sortKey = resultPrtyRazonesSociales[i].sortkey.Replace("'", "`");
                        prtyRazonSocial.estado = EstadoPrty.Leido;

                        this.listaRazonSocial.Add(prtyRazonSocial);
                    }
                }

                var resultPrtyDirecciones = m_XgpyService.Sce_Dad_S08_MS(this.idPrty);
                if (resultPrtyDirecciones != null)
                {
                    this.listaDireccion = new List<PrtyDireccion>();
                    for (int i = 0; i < resultPrtyDirecciones.Count; i++)
                    {
                        PrtyDireccion prtyDireccion = new PrtyDireccion();
                        prtyDireccion.borrado = resultPrtyDirecciones[i].borrado;
                        prtyDireccion.indice = (int)resultPrtyDirecciones[i].id_dir;
                        prtyDireccion.indice = (int)resultPrtyDirecciones[i].id_dir;
                        prtyDireccion.direccion = resultPrtyDirecciones[i].direccion.Replace("'", "`");
                        prtyDireccion.comuna = resultPrtyDirecciones[i].comuna.Replace("'", "`");
                        prtyDireccion.codigoComuna = (int)resultPrtyDirecciones[i].cod_comuna;
                        prtyDireccion.codigoPostal = resultPrtyDirecciones[i].cod_postal.Replace("'", "`");
                        prtyDireccion.region = resultPrtyDirecciones[i].estado.Replace("'", "`");
                        prtyDireccion.ciudad = resultPrtyDirecciones[i].ciudad.Replace("'", "`");
                        prtyDireccion.pais = resultPrtyDirecciones[i].pais.Replace("'", "`");
                        prtyDireccion.codigoPais = (int)resultPrtyDirecciones[i].cod_pais;
                        prtyDireccion.telefono = Funciones.CleanPhoneNumber(resultPrtyDirecciones[i].telefono);
                        prtyDireccion.fax = Funciones.CleanPhoneNumber(resultPrtyDirecciones[i].fax);
                        prtyDireccion.telex = resultPrtyDirecciones[i].telex.Replace("'", "`");
                        prtyDireccion.enviarA = (int)resultPrtyDirecciones[i].envio_sce;
                        prtyDireccion.recibe = (int)resultPrtyDirecciones[i].recibe_sce;
                        prtyDireccion.casillaPostal = resultPrtyDirecciones[i].cas_postal.Replace("'", "`");
                        prtyDireccion.casillaBanco = resultPrtyDirecciones[i].cas_banco.Replace("'", "`");
                        prtyDireccion.email = resultPrtyDirecciones[i].email.Replace("'", "`");
                        prtyDireccion.estado = EstadoPrty.Leido;

                        this.listaDireccion.Add(prtyDireccion);
                    }
                }
                resultPrtyDirecciones = null;

                switch (this.tipo)
                {
                    case TipoPrty.Cliente:
                        this.nivelDeAcceso.Habilitar(true);
                        break;

                    case TipoPrty.Banco:
                        if ((this.flag & FlagPrty.Corresponsal) == FlagPrty.Corresponsal && (this.flag & FlagPrty.Acreedor) == FlagPrty.Acreedor)
                        {
                            this.nivelDeAcceso.Habilitar(true);
                        }
                        else
                        {
                            if ((this.flag & FlagPrty.Corresponsal) == FlagPrty.Corresponsal)
                            {
                                // TODO: habilitar y excluir tasas especiales
                                this.nivelDeAcceso.Habilitar(true);
                                this.nivelDeAcceso.tasasEspeciales = false;
                            }
                            else
                            {
                                // TODO: habilitar y excluir cuentas corriente
                                this.nivelDeAcceso.Habilitar(true);
                                this.nivelDeAcceso.cuentas = false;
                            }

                            if ((this.flag & FlagPrty.Acreedor) == FlagPrty.Acreedor)
                            {
                                // TODO: habilitar y excluir tasas especiales
                                this.nivelDeAcceso.Habilitar(true);
                                this.nivelDeAcceso.tasasEspeciales = false;
                            }
                            else
                            {
                                // TODO: habilitar y excluir cuentas corriente
                                this.nivelDeAcceso.Habilitar(true);
                                this.nivelDeAcceso.cuentas = false;
                            }
                        }

                        // TODO: excluir tasas especiales
                        this.nivelDeAcceso.tasasEspeciales = false;
                        break;

                    case TipoPrty.Individuo:
                        // TODO: habilitar
                        this.nivelDeAcceso.Habilitar(true);

                        // TODO: excluir cuentas corrientes
                        this.nivelDeAcceso.cuentas = false;

                        if (String.IsNullOrEmpty(this.rut))
                        {
                            // TODO: excluir caracteristicas
                            this.nivelDeAcceso.caracteristicas = false;
                            // TODO: excluir tasas
                            this.nivelDeAcceso.tasasEspeciales = false;
                        }
                        else
                        {
                            // TODO: incluir caracteristicas
                            this.nivelDeAcceso.caracteristicas = true;
                            // TODO: incluir tasas
                            this.nivelDeAcceso.tasasEspeciales = true;
                        }
                        break;
                }

                // TODO: segun usuario incluir o excluir "Grabar Participante"

                // TODO: segun usuario generar opcion "solo vista"

                // TODO: usuario es "dueño" del participante

                Int16 codigoOficina;
                if (!Int16.TryParse(this.oficina, out codigoOficina))
                {
                    codigoOficina = 0;
                }
                var dbSelectPrtyEjc = m_XgpyService.Sgt_Ejc_S04_MS(codigoOficina);
                //this.listaEjecutivos = new List<PrtyEjecutivos>();
                for (int i = 0; i < dbSelectPrtyEjc.Count; i++)
                {
                    PrtyEjecutivos prtyEjectivos = new PrtyEjecutivos();
                    prtyEjectivos.codigo = Convert.ToString(dbSelectPrtyEjc[i].ejc_ejccod);
                    prtyEjectivos.nombre = dbSelectPrtyEjc[i].ejc_ejcnom;

                    //this.listaEjecutivos.Add(prtyEjectivos);
                }
                dbSelectPrtyEjc = null;

                //(moo) especialistas
                //T_PRTYENT.EJCOPEXP, T_PRTYENT.EJCOPIMP, T_PRTYENT.EJCNEGOC
                //TODO: llenar según privilegios/configuracion de operador
                var dbSelectPrtyEsp = m_XgpyService.Sgt_Ejc_S03_MS("3", "4", "5");
                //this.listaEspecialistas = new List<PrtyEspecialista>();
                for (int i = 0; i < dbSelectPrtyEsp.Count; i++)
                {
                    PrtyEspecialista prtyEspecialista = new PrtyEspecialista();
                    prtyEspecialista.codigoOficina = Convert.ToString(dbSelectPrtyEsp[i].ejc_ejcofi);
                    prtyEspecialista.codigoEjectivo = Convert.ToString(dbSelectPrtyEsp[i].ejc_ejccod);
                    prtyEspecialista.rut = dbSelectPrtyEsp[i].ejc_ejcrut;
                    prtyEspecialista.nombre = dbSelectPrtyEsp[i].ejc_ejcnom;
                    prtyEspecialista.tipo = dbSelectPrtyEsp[i].ejc_ejctpo;

                    //this.listaEspecialistas.Add(prtyEspecialista);
                }
                dbSelectPrtyEsp = null;

                var dbSelectTCom = m_XgpyService.Sce_Tcom_S04_MS(this.idPrty);
                this.listaTasaCom = new List<PrtyTasaCom>();
                for (int i = 0; i < dbSelectTCom.Count; i++)
                {
                    PrtyTasaCom prtyTasaCom = new PrtyTasaCom();
                    prtyTasaCom.sistema = dbSelectTCom[i].sistema;
                    prtyTasaCom.producto = dbSelectTCom[i].producto;
                    prtyTasaCom.etapa = dbSelectTCom[i].etapa;
                    prtyTasaCom.estado = EstadoPrty.Leido;
                    prtyTasaCom.secuencia = (int)dbSelectTCom[i].secuencia;
                    prtyTasaCom.manual = dbSelectTCom[i].manual_t ? 1 : 0;
                    prtyTasaCom.mto_fijo = dbSelectTCom[i].monto_fijo ? 1 : 0;
                    prtyTasaCom.tasa = (double)dbSelectTCom[i].tasa;
                    prtyTasaCom.hasta = (double)dbSelectTCom[i].hasta_mon;
                    prtyTasaCom.min = (double)dbSelectTCom[i].minimo;
                    prtyTasaCom.max = (double)dbSelectTCom[i].maximo;
                    prtyTasaCom.fecha = Convert.ToString(dbSelectTCom[i].fecha);

                    this.listaTasaCom.Add(prtyTasaCom);
                }
                dbSelectTCom = null;

                var dbSelectTGas = m_XgpyService.Sce_Tgas_S04_MS(this.idPrty);
                this.listaTasaGas = new List<PrtyTasaGas>();
                for (int i = 0; i < dbSelectTGas.Count; i++)
                {
                    PrtyTasaGas prtyTasaGas = new PrtyTasaGas();
                    prtyTasaGas.sistema = dbSelectTGas[i].sistema;
                    prtyTasaGas.producto = dbSelectTGas[i].producto;
                    prtyTasaGas.etapa = dbSelectTGas[i].etapa;
                    prtyTasaGas.estado = EstadoPrty.Leido;
                    prtyTasaGas.tarifa = dbSelectTGas[i].m_tarifa ? 1 : 0;
                    prtyTasaGas.monto = (double)dbSelectTGas[i].monto;
                }
                dbSelectTGas = null;

                var dbSelectTInt = m_XgpyService.Sce_Tint_S01_MS(this.idPrty);
                if (dbSelectTInt != null)
                {
                    this.listaTasaInt = new List<PrtyTasaInt>();
                    for (int i = 0; i < dbSelectTInt.Count; i++)
                    {
                        PrtyTasaInt prtyTasaInt = new PrtyTasaInt();
                        prtyTasaInt.sistema = dbSelectTInt[i].sistema;
                        prtyTasaInt.producto = dbSelectTInt[i].producto;
                        prtyTasaInt.etapa = dbSelectTInt[i].etapa;
                        prtyTasaInt.estado = EstadoPrty.Leido;
                        prtyTasaInt.libor = dbSelectTInt[i].libor ? 1 : 0;
                        prtyTasaInt.prime = dbSelectTInt[i].prime ? 1 : 0;
                        prtyTasaInt.flotante = dbSelectTInt[i].flotante ? 1 : 0;
                        prtyTasaInt.tasa = (double)dbSelectTInt[i].tasa;
                    }
                    dbSelectTInt = null;
                }

                var cuentasPorRut = m_XgpyService.ObtenerCuentasPorRut(this.GeneraFormatoBuscarCuentas(this.idPrty));
                if (cuentasPorRut != null)
                {
                    this.listaCuentas = new List<PrtyCuenta>();
                    for (int i = 0; i < cuentasPorRut.Count(); i++)
                    {
                        PrtyCuenta prtyCuenta = new PrtyCuenta();
                        prtyCuenta.nroCuenta = cuentasPorRut[i].numProducto;
                        prtyCuenta.tipo = cuentasPorRut[i].codigoProducto;

                        this.listaCuentas.Add(prtyCuenta);
                    }
                    cuentasPorRut = null;
                }

                var dbCuentaCliente = m_XgpyService.Sce_Ctas_S04_MS(this.idPrty);
                if (dbCuentaCliente != null)
                {
                    this.listaCtaCliente = new List<PrtyCtaCliente>();
                    for (int i = 0; i < dbCuentaCliente.Count(); i++)
                    {
                        PrtyCtaCliente prtyCtaCliente = new PrtyCtaCliente();
                        prtyCtaCliente.indice = (int)dbCuentaCliente[i].secuencia;
                        prtyCtaCliente.activabco = dbCuentaCliente[i].activabco;
                        prtyCtaCliente.activace = dbCuentaCliente[i].activace;
                        prtyCtaCliente.extranjera = dbCuentaCliente[i].extranjera;
                        prtyCtaCliente.moneda = Convert.ToString(dbCuentaCliente[i].moneda);
                        prtyCtaCliente.cuenta = dbCuentaCliente[i].cuenta;
                        prtyCtaCliente.estado = EstadoPrty.Leido;
                        prtyCtaCliente.est2 = dbCuentaCliente[i].borrado;

                        this.listaCtaCliente.Add(prtyCtaCliente);
                    }
                    dbCuentaCliente = null;
                }

                var dbCuentaBanco = m_XgpyService.Sce_Bcta_S01_MS(this.idPrty);
                if (dbCuentaBanco != null)
                {
                    this.listaCtaBanco = new List<PrtyCtaBanco>();
                    for (int i = 0; i < dbCuentaBanco.Count(); i++)
                    {
                        PrtyCtaBanco prtyCtaBanco = new PrtyCtaBanco();
                        prtyCtaBanco.indice = (int)dbCuentaBanco[i].secuencia;
                        prtyCtaBanco.activa = dbCuentaBanco[i].activa;
                        prtyCtaBanco.moneda = Convert.ToString(dbCuentaBanco[i].moneda);
                        prtyCtaBanco.cuenta = dbCuentaBanco[i].cuenta;
                        prtyCtaBanco.especial = dbCuentaBanco[i].especial;
                        prtyCtaBanco.estado = EstadoPrty.Leido;

                        this.listaCtaBanco.Add(prtyCtaBanco);
                    }
                    dbCuentaBanco = null;
                }

                var dbLineaBanco = m_XgpyService.Sce_Blin_S01_MS(this.idPrty);
                if (dbLineaBanco != null)
                {
                    this.listaLinBanco = new List<PrtyLinBanco>();
                    for (int i = 0; i < dbLineaBanco.Count(); i++)
                    {
                        PrtyLinBanco prtyLinBanco = new PrtyLinBanco();
                        prtyLinBanco.indice = (int)dbLineaBanco[i].secuencia;
                        prtyLinBanco.activa = dbLineaBanco[i].activa;
                        prtyLinBanco.moneda = Convert.ToString(dbLineaBanco[i].moneda);
                        prtyLinBanco.linea = dbLineaBanco[i].linea;
                        prtyLinBanco.estado = EstadoPrty.Leido;

                        this.listaLinBanco.Add(prtyLinBanco);
                    }
                    dbLineaBanco = null;
                }
            }
            return respStatus;
        }

        public Boolean ReActivar()
        {
            if (String.IsNullOrEmpty(this.idPrty))
            {
                return false;
            }

            try
            {
                var resultUpdate = m_XgpyService.Sce_Prty_U01_MS(this.idPrty, false);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Int32 Save()
        {
            Boolean razonSocialVacia = false;
            Boolean direccionVacia = false;
            Int32 retFun = 100;

            //(moo) analizamos cambios en razon social
            foreach (PrtyRazonSocial prtyRazonSocial in this.listaRazonSocial)
            {
                if (prtyRazonSocial.estado == EstadoPrty.EliminadoLeido || prtyRazonSocial.estado == EstadoPrty.EliminadoModificado || prtyRazonSocial.estado == EstadoPrty.EliminadoNuevo)
                {
                    razonSocialVacia = true;
                }
                else
                {
                    razonSocialVacia = false;
                }
            }

            //(moo) analizamos cambios en razon social
            foreach (PrtyDireccion prtyDireccion in this.listaDireccion)
            {
                if (prtyDireccion.estado == EstadoPrty.EliminadoLeido || prtyDireccion.estado == EstadoPrty.EliminadoModificado || prtyDireccion.estado == EstadoPrty.EliminadoNuevo)
                {
                    direccionVacia = true;
                }
                else
                {
                    direccionVacia = false;
                }
            }

            //(moo) validar si tiene cuenta asignada
            if (this.tipo == TipoPrty.Cliente)
            {
                if (this.listaCuentas == null)
                    return 101;

                if (String.IsNullOrEmpty(this.listaCuentas[0].nroCuenta))
                    return 101;
            }

            //(moo) solo si existen cambios en datos del participante
            if (this.estado != EstadoPrty.Leido) { }

            return retFun;
        }

        private void Init()
        {
            this.nextStatus = NextStatus.End;
            this.nivelDeAcceso = new NivelAcceso();
        }

        private Boolean ExistBaseBIC()
        {
            if (String.IsNullOrEmpty(this.searchIdPrty))
            {
                return false;
            }

            var resultPrtyIsDataBic = m_XgpyService.Sce_Bic_S07_MS(this.searchIdPrty);
            if (resultPrtyIsDataBic != null)
            {
                if (resultPrtyIsDataBic.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private String GeneraFormatoBuscarCuentas(String idPrty)
        {
            string retGeneraFormatoBuscarCuentas = "";
            int cont = 0;
            string rut_salida = "";
            string ruttmp = "";

            //(moo) eliminamos los |
            var rut = idPrty.Replace("|", "");

            for (cont = 0; cont < rut.Length; cont++)
            {
                if (rut[cont] != '0')
                {
                    ruttmp = rut.Substring(cont, rut.Length - cont);

                    break;
                }
            }

            for (cont = 0; cont < ruttmp.Length; cont++)
            {
                if (ruttmp[cont] != '~')
                {
                    rut_salida = rut_salida + ruttmp[cont];
                }
            }

            retGeneraFormatoBuscarCuentas = rut_salida.Substring(0, rut_salida.Length - 1) + "-" + rut_salida[rut_salida.Length - 1];

            return retGeneraFormatoBuscarCuentas;
        }
    }
}
