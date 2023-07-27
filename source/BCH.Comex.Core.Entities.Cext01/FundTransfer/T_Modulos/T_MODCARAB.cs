
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class CargoAbono
    {
        public string numcct;
        public string NemCta;
        public string fecmov;
        public short nroimp;
        public string Nombre;
        public string cod_dh;
        public string NemMon;
        public short CodMon;
        public double mtomcd;
        public string Operacion;
        public int NroRpt;
        public short enlinea;
        public short Estado;
        public string codcct;
        public string codpro;
        public string codesp;
        public string codofi;
        public string codope;
        public short codfun;
        public short Indice;
        public string numorden;
        public string cod_pro;
        public string cod_trx_fc;
        public string cod_ext;
        public string cod_swf;
        public string Data1;
        public string Data2;
        public string Data3;
        public string Data4;
        public string Data5;
        public string cod_trx_cosmos;
        public string tip_abo_car;
    }

    public class Reversa
    {
        public string codcct;
        public string codpro;
        public string codesp;
        public string codofi;
        public string codope;
        public string fecmov;
        public short nroimp;
        public string cod_dh;
        public short Estado;
        public short Fila_Grilla;
    }

    public class consulta_opera
    {
        public string record_type;
        public string branch_number;
        public string contract_reference;
        public string ordering_customer;
        public string act_hist_indicator;
        public string input_date1;
        public string receiver;
        public string credit_entry_date;
        public string order_cost_account;
        public string receiver_account;
        public string authorization_stat;
        public string transac_type_code;
        public string exe_type_code_tran;
        public string product_type;
        public string swf_currency_code;
        public string currency_code;
        public string charges_debit;
        public string transfer_amount;
        public string sign_transfer;
        public string legal_vehicle_code;
        public string debit_value_date;
        public string data_entry_date;
        public string credit_value_date;
        public string Texto;
        public string Status;
        public string by_order_of;
        public string beneficiary;
        public string last_inp_date;
        public string transfer_charged;
        public string operator_id;
        public string input_date2;
        public string input_time;
        public string authorizer_id;
        public string authorizer_time;
        public string order_cust_account;
        public string input_date3;
        public string alpha_number;
        public string swf_currency_equi;
        public string currency_code_equi;
        public string equivalent_amount;
        public string signo_equivalent;
        public string fcy_exchange_rate;
        public string receiver_account2;
        public string input_date4;
        public string short_benefic_bank;
        public string alpha_reference;
        public string lto_indicator;
        public string benefi_account_num;
        public string commission_rate;
        public string commission_amount;
        public string sing_commssion;
        public string courtage_rate;
        public string courtage_amount;
        public string sign_courtage;
        public string postage_amount;
        public string sign_postage;
        public string swf_currency_charg;
        public string currency_code_chan;
        public string chrg_base_nbr;
        public string short_charges_acou;
        public string reference_number;
        public string central_bank_code;
        public string num_order_customer;
        public string num_receiver;
        public string num_beneficia_bank;
        public string num_beneficiary;
        public string num_reason;
        public string num_bank_to_bank;
        public string num_charges;
        public string total_number;
        public string text_line;
        public string text_line2;
        public string text_line3;
    }
    
    public class T_MODCARAB
    {
        public CargoAbono[] V_CYA;
	    public Reversa[] V_REV;
	    public consulta_opera[] V_COP;

        public T_MODCARAB()
        {
            V_CYA = new CargoAbono[0];
	        V_REV = new Reversa[0];
            V_COP = new consulta_opera[0];
        }


	    // UPGRADE_INFO (#0561): The 'MsgAboCar' symbol was defined without an explicit "As" clause.
	    public const string MsgAboCar = "Inyección Abono/Cargo";
	    // UPGRADE_INFO (#0561): The 'Rev_o_Iny' symbol was defined without an explicit "As" clause.
        public dynamic Rev_o_Iny;
	    // UPGRADE_INFO (#0561): The 'Cuenta_Rev' symbol was defined without an explicit "As" clause.
        public dynamic Cuenta_Rev;
	    // UPGRADE_INFO (#0561): The 'Valida_Rev' symbol was defined without an explicit "As" clause.
        public dynamic Valida_Rev;
	    // Estas definiciones están tomadas de lo mostrado en el explorador
	    // al seleccionar cada una de las funciones del servicio Web
	    // UPGRADE_INFO (#0561): The 'cSOAPCta31' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31 = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:abon=""http://osb.bancochile.cl/AbonoCuentaCosmosCodFC/"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:opab=""http://osb.bancochile.cl/ESB/AbonoCuentaCosmosCodFC/OpAbonoCuentaCosmosCodFCRequest"">" + "<soapenv:Header>" + "<abon:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode></head:internalCode>" + "<head:idTransaccionNegocio></head:idTransaccionNegocio>" + "<head:fechaHora></head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</abon:headerRequest>" + "</soapenv:Header>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta31_1' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31_1 = "<soapenv:Body>" + "<abon:AbonoCuentaCosmosCodFC>" + "<reqAbonoCuentaCosmosCodFC>" + "<opab:DatosCabeceraNegocio>" + "<opab:rutOperadora>12345678</opab:rutOperadora>" + "<opab:Cajero></opab:Cajero>" + "<opab:bancoDestino>001</opab:bancoDestino>" + "<opab:bancoOrigen>001</opab:bancoOrigen>" + "<opab:rutCliente></opab:rutCliente>" + "<opab:oficinaOrigenTx>000</opab:oficinaOrigenTx>" + "<opab:CUIOrigenTx></opab:CUIOrigenTx>" + "<opab:oficinaOrigenExternaTx></opab:oficinaOrigenExternaTx>" + "<opab:canalOrigenTx>COMEX00001</opab:canalOrigenTx>" + "<opab:rutSupervisor></opab:rutSupervisor>" + "<opab:supervisor></opab:supervisor>" + "<opab:fechaContable></opab:fechaContable>" + "<opab:fechaHoraCorrienteTx></opab:fechaHoraCorrienteTx>" + "<opab:Horario>01</opab:Horario>" + "<opab:txid></opab:txid>" + "<opab:txidParaReversar></opab:txidParaReversar>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta31_2' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31_2 = "</opab:DatosCabeceraNegocio>" + "<opab:DatosNegocio>" + "<opab:Cuenta></opab:Cuenta>" + "<opab:Serialdelcheque></opab:Serialdelcheque>" + "<opab:CodBanco></opab:CodBanco>" + "<opab:CodPlaza></opab:CodPlaza>" + "<opab:CodProductoFC></opab:CodProductoFC>" + "<opab:CodTransaccionFC></opab:CodTransaccionFC>" + "<opab:CodExtendidoFC></opab:CodExtendidoFC>" + "<opab:Monto></opab:Monto>" + "<opab:Moneda></opab:Moneda>" + "<opab:nroBoleta></opab:nroBoleta>" + "<opab:CuentadeCargo></opab:CuentadeCargo>" + "<opab:ListadataCartola>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta31_3' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31_3 = "</opab:ListadataCartola>" + "</opab:DatosNegocio>" + "<opab:dataMT942>" + "<opab:Mt942Campo61>" + "<opab:FechaValuta>100110</opab:FechaValuta>" + "<opab:TxType>TRF</opab:TxType>" + "<opab:ReferenciaCliente>NONREF</opab:ReferenciaCliente>" + "<opab:ReferenciaBCH></opab:ReferenciaBCH>" + "</opab:Mt942Campo61>" + "<opab:mt942CodigoTxCosmos></opab:mt942CodigoTxCosmos>" + "<opab:mt942TipoProductoCosmos>FT</opab:mt942TipoProductoCosmos>" + "<opab:mt942InfoAdicional-2></opab:mt942InfoAdicional-2>" + "</opab:dataMT942>" + "</reqAbonoCuentaCosmosCodFC>" + "</abon:AbonoCuentaCosmosCodFC>" + "</soapenv:Body>" + "</soapenv:Envelope>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta31_NomCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31_NomCam = "<opab:DataCartola>" + "<opab:NombreCampo>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta31_ValCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31_ValCam = "</opab:NombreCampo>" + "<opab:ValorCampo>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta31_CierreCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta31_CierreCam = "</opab:ValorCampo>" + "</opab:DataCartola>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32 = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:car=""http://osb.bancochile.cl/CargoCuentaCosmosCodFC/"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:opc=""http://osb.bancochile.cl/ESB/CargoCuentaCosmosCodFC/OpCargoCuentaCosmosCodFCRequest"">" + "<soapenv:Header>" + "<car:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode></head:internalCode>" + "<head:idTransaccionNegocio></head:idTransaccionNegocio>" + "<head:fechaHora></head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</car:headerRequest>" + "</soapenv:Header>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32_1' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32_1 = "<soapenv:Body>" + "<car:CargoCuentaCosmosCodFC>" + "<reqCargoCuentaCosmosCodFC>" + "<opc:DatosCabeceraNegocio>" + "<opc:rutOperadora>12345678</opc:rutOperadora>" + "<opc:Cajero></opc:Cajero>" + "<opc:bancoDestino>001</opc:bancoDestino>" + "<opc:bancoOrigen>001</opc:bancoOrigen>" + "<opc:rutCliente></opc:rutCliente>" + "<opc:oficinaOrigenTx>000</opc:oficinaOrigenTx>" + "<opc:CUIOrigenTx></opc:CUIOrigenTx>" + "<opc:oficinaOrigenExternaTx></opc:oficinaOrigenExternaTx>" + "<opc:canalOrigenTx>COMEX00001</opc:canalOrigenTx>" + "<opc:rutSupervisor></opc:rutSupervisor>" + "<opc:supervisor></opc:supervisor>" + "<opc:fechaContable></opc:fechaContable>" + "<opc:fechaHoraCorrienteTx></opc:fechaHoraCorrienteTx>" + "<opc:Horario>01</opc:Horario>" + "<opc:txid></opc:txid>" + "<opc:txidParaReversar></opc:txidParaReversar>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32_2' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32_2 = "</opc:DatosCabeceraNegocio>" + "<opc:DatosNegocio>" + "<opc:Cuenta></opc:Cuenta>" + "<opc:Serialdelcheque></opc:Serialdelcheque>" + "<opc:CodBanco></opc:CodBanco>" + "<opc:CodPlaza></opc:CodPlaza>" + "<opc:CodProductoFC></opc:CodProductoFC>" + "<opc:CodTransaccionFC></opc:CodTransaccionFC>" + "<opc:CodExtendidoFC></opc:CodExtendidoFC>" + "<opc:Monto></opc:Monto>" + "<opc:Moneda></opc:Moneda>" + "<opc:CuentadeAbono></opc:CuentadeAbono>" + "<opc:ListadataCartola>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32_3' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32_3 = "</opc:ListadataCartola>" + "</opc:DatosNegocio>" + "<opc:dataMT942>" + "<opc:mt942Campo61>" + "<opc:FechaValuta>100110</opc:FechaValuta>" + "<opc:TxType>TRF</opc:TxType>" + "<opc:ReferenciaCliente>NONREF</opc:ReferenciaCliente>" + "<opc:ReferenciaBCH></opc:ReferenciaBCH>" + "</opc:mt942Campo61>" + "<opc:mt942CodigoTxCosmos></opc:mt942CodigoTxCosmos>" + "<opc:mt942TipoProductoCosmos>FT</opc:mt942TipoProductoCosmos>" + "<opc:mt942InfoAdicional-2></opc:mt942InfoAdicional-2>" + "</opc:dataMT942>" + "</reqCargoCuentaCosmosCodFC>" + "</car:CargoCuentaCosmosCodFC>" + "</soapenv:Body>" + "</soapenv:Envelope>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32_NomCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32_NomCam = "<opc:DataCartola>" + "<opc:NombreCampo>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32_ValCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32_ValCam = "</opc:NombreCampo>" + "<opc:ValorCampo>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta32_CierreCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta32_CierreCam = "</opc:ValorCampo>" + "</opc:DataCartola>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta18' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta18 = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:abon=""http://osb.sb:8011/AbonosCuentasCorrientesSTB"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:abon1=""http://osb.sb:8011/AbonosCuentasCorrientesSTBRequest"">" + "<soapenv:Header>" + "<abon:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode>1</head:internalCode>" + "<head:idTransaccionNegocio></head:idTransaccionNegocio>" + "<head:fechaHora></head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</abon:headerRequest>" + "</soapenv:Header>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta18_1' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta18_1 = "<soapenv:Body>" + "<abon:DatosAbonosCuentasCorrientesSTB>" + "<request>" + "<abon1:DatosCabeceraNegocio>" + "<abon1:rutOperadora>12345678</abon1:rutOperadora>" + "<abon1:cajero></abon1:cajero>" + "<abon1:bancoDestino>001</abon1:bancoDestino>" + "<abon1:bancoOrigen>001</abon1:bancoOrigen>" + "<abon1:Modo>D</abon1:Modo>" + "<abon1:rutCliente></abon1:rutCliente> " + "<abon1:oficinaOrigenTransaccion>000</abon1:oficinaOrigenTransaccion>" + "<abon1:CUIOrigenTx></abon1:CUIOrigenTx>" + "<abon1:oficinaOrigenExternaTransaccion></abon1:oficinaOrigenExternaTransaccion>" + "<abon1:canalOrigenTransaccion>COMEX00001</abon1:canalOrigenTransaccion>" + "<abon1:rutSupervisor></abon1:rutSupervisor>" + "<abon1:supervisor></abon1:supervisor>" + "<abon1:fechaContable></abon1:fechaContable>" + "<abon1:fechaHoraCorrienteTransaccion></abon1:fechaHoraCorrienteTransaccion>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta18_2' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta18_2 = "<abon1:Horario></abon1:Horario>" + "<abon1:txid></abon1:txid>" + "<abon1:idTransaccionParaReversar></abon1:idTransaccionParaReversar>" + "</abon1:DatosCabeceraNegocio>" + "<abon1:DatosNegocio>" + "<abon1:cuenta></abon1:cuenta>" + "<abon1:serial></abon1:serial>" + "<abon1:moneda></abon1:moneda>" + "<abon1:codigo></abon1:codigo>" + "<abon1:codigoExtendido>00000</abon1:codigoExtendido>" + "<abon1:monto></abon1:monto>" + "</abon1:DatosNegocio>" + "</request>" + "</abon:DatosAbonosCuentasCorrientesSTB>" + "</soapenv:Body>" + "</soapenv:Envelope>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta19' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta19 = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:car=""http://osb.sb:8011/CargosCuentasCorrientesSTB"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:car1=""http://osb.sb:8011/CargosCuentasCorrientesSTBRequest"">" + "<soapenv:Header>" + "<car:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode>1</head:internalCode>" + "<head:idTransaccionNegocio></head:idTransaccionNegocio>" + "<head:fechaHora></head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</car:headerRequest>" + "</soapenv:Header>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta19_1' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta19_1 = "<soapenv:Body>" + "<car:DatosCargosCuentasCorrientesSTB>" + "<request>" + "<car1:DatosCabeceraNegocio>" + "<car1:rutOperadora>12345678</car1:rutOperadora>" + "<car1:cajero></car1:cajero>" + "<car1:bancoDestino>001</car1:bancoDestino>" + "<car1:bancoOrigen>001</car1:bancoOrigen>" + "<car1:Modo>D</car1:Modo>" + "<car1:rutCliente></car1:rutCliente>" + "<car1:oficinaOrigenTransaccion>000</car1:oficinaOrigenTransaccion>" + "<car1:CUIOrigenTx></car1:CUIOrigenTx>" + "<car1:oficinaOrigenExternaTransaccion></car1:oficinaOrigenExternaTransaccion>" + "<car1:canalOrigenTransaccion>COMEX00001</car1:canalOrigenTransaccion>" + "<car1:rutSupervisor></car1:rutSupervisor>" + "<car1:supervisor></car1:supervisor>" + "<car1:fechaContable></car1:fechaContable>" + "<car1:fechaHoraCorrienteTransaccion></car1:fechaHoraCorrienteTransaccion>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta19_2' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta19_2 = "<car1:horario></car1:horario>" + "<car1:idTransaccion></car1:idTransaccion>" + "<car1:idTransaccionParaReversar></car1:idTransaccionParaReversar>" + "</car1:DatosCabeceraNegocio>" + "<car1:DatosNegocio>" + "<car1:cuenta></car1:cuenta>" + "<car1:serial></car1:serial>" + "<car1:moneda></car1:moneda>" + "<car1:codigo></car1:codigo>" + "<car1:codigoExtendido>00000</car1:codigoExtendido>" + "<car1:monto></car1:monto>" + "</car1:DatosNegocio>" + "</request>" + "</car:DatosCargosCuentasCorrientesSTB>" + "</soapenv:Body>" + "</soapenv:Envelope>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25 = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:rev=""http://osb.bancochile.cl/ReversaCosmos/"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:opr=""http://osb.bancochile.cl/ESB/ReversaCosmos/OpReversaCosmosRequest"">" + "<soapenv:Header>" + "<rev:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode>1</head:internalCode>" + "<head:idTransaccionNegocio></head:idTransaccionNegocio>" + "<head:fechaHora></head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</rev:headerRequest>" + "</soapenv:Header>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25_1' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25_1 = "<soapenv:Body>" + "<rev:ReversaCosmos>" + "<reqReversaCosmos>" + "<opr:DatosCabeceraNegocio>" + "<opr:rutOperadora>12345678</opr:rutOperadora>" + "<opr:Cajero></opr:Cajero>" + "<opr:bancoDestino>001</opr:bancoDestino>" + "<opr:bancoOrigen>001</opr:bancoOrigen>" + "<opr:rutCliente></opr:rutCliente>" + "<opr:oficinaOrigenTx>000</opr:oficinaOrigenTx>" + "<opr:CUIOrigenTx></opr:CUIOrigenTx>" + "<opr:oficinaOrigenExternaTx></opr:oficinaOrigenExternaTx>" + "<opr:canalOrigenTx>COMEX00001</opr:canalOrigenTx>" + "<opr:rutSupervisor></opr:rutSupervisor>" + "<opr:supervisor></opr:supervisor>" + "<opr:fechaContable></opr:fechaContable>" + "<opr:fechaHoraCorrienteTx></opr:fechaHoraCorrienteTx>" + "<opr:Horario></opr:Horario>" + "<opr:txid></opr:txid>" + "<opr:txidParaReversar></opr:txidParaReversar>" + "</opr:DatosCabeceraNegocio>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25_2' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25_2 = "<opr:DatosNegocio>" + "<opr:Cuenta></opr:Cuenta>" + "<opr:Serialdelcheque></opr:Serialdelcheque>" + "<opr:codigoCartolaCosmos></opr:codigoCartolaCosmos>" + "<opr:Monto></opr:Monto>" + "<opr:Moneda></opr:Moneda>" + "<opr:CuentadeCargo></opr:CuentadeCargo>" + "<opr:ListadataCartola>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25_3' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25_3 = "</opr:ListadataCartola>" + "<opr:dataMT942>" + "<opr:mt942Campo61>" + "<opr:FechaValuta></opr:FechaValuta>" + "<opr:MarkDebitCredit></opr:MarkDebitCredit>" + "<opr:TxType>TRF</opr:TxType>" + "<opr:ReferenciaCliente>NONREF</opr:ReferenciaCliente>" + "<opr:ReferenciaBCH></opr:ReferenciaBCH>" + "</opr:mt942Campo61>" + "<opr:mt942CodigoTxCosmos></opr:mt942CodigoTxCosmos>" + "<opr:mt942TipoProductoCosmos>FT</opr:mt942TipoProductoCosmos>" + "<opr:mt942InfoAdicional-2></opr:mt942InfoAdicional-2>" + "</opr:dataMT942>" + "</opr:DatosNegocio>" + "</reqReversaCosmos>" + "</rev:ReversaCosmos>" + "</soapenv:Body>" + "</soapenv:Envelope>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25_NomCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25_NomCam = "<opr:dataCartola>" + "<opr:NombreCampo>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25_ValCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25_ValCam = "</opr:NombreCampo>" + "<opr:ValorCampo>";

	    // UPGRADE_INFO (#0561): The 'cSOAPCta25_CierreCam' symbol was defined without an explicit "As" clause.
	    public const string cSOAPCta25_CierreCam = "</opr:ValorCampo>" + "</opr:dataCartola>";
    }
}
