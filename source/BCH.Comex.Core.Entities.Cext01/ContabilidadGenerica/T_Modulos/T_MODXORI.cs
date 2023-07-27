using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{

        //****************************************************************************
        //Arreglo para los Codigos de Transaccion.
        //****************************************************************************
        public class T_CodTran
        {
            public short nro_trx;  //Codigo de Trx
            public string glosa_cosmos;  //Glosa
            public string cr_dr;  //Codigo Abono o Cargo
            public string Moneda;  //Moneda
            public string tip_cta;  //Tipo de Cuenta (CTA CTE o GAP'
            public string cod_trx_cosmos;  //Tipo de TRX Cosmos
        }

        public class S_Codtran
        {
            public short nro_trx;
            public string Moneda;
            public string Via;
            public short ID;
            public short Estado;
        }

        //****************************************************************************
        //Variable para Montos de Orígenes de Fondo.
        //****************************************************************************
        public class T_xMtoOri
        {
            public short CodMon;  //Moneda Comisión.
            public string NemMon;  //Nemónico Moneda.
            public double MtoTot;  //Monto + IVA.
            public string Party;  //Party que paga el Origen.-
            public short PosPrt;  //Posicion del Participante.-

            #region Initialization

            public T_xMtoOri()
            {
                NemMon = String.Empty;
                CodMon = 0;
                MtoTot = 0;
                Party = "";
                PosPrt = 0;
            }

            #endregion
        }

        //**********************************************************
        //Variable para Orígenes de Fondo.
        //**********************************************************
        public class T_xOri
        {
            public int NumCta;  //Id. Cta Contable.
            public string NemCta;  //Nemónico Cta Contable.
            public string NomOri;  //Nombre Origen Fondo.
            public short CodMon;  //Moneda de la Comisión.
            public double MtoTot;  //Monto incluyendo IVA.
            public string NemMon;  //Nemónico Moneda.
            public short Status;  //1:Ing;2:Mod;3:Eli.
            //------------------------------------------------------
            //Datos adicionales.
            //------------------------------------------------------
            public string IdPrty;  //Llave del Party.
            public short PosPrty;  //Posición en PartysOpe del que Paga.
            public short PosOri;  //Posición en PartysOpe del Original.
            public string ctacte;  //# Cuenta Corriente.
            public string CtaCte_t;  //# Cuenta Corriente_t.
            public short MonExt;  //Indica si es Mon. Ext.
            public short codofi;  //Oficina   ScS.
            public short TipMov;  //Tipo Mov. ScS.
            public int NumPar;  //# Partida ScS.
            public string CodSwf;  //Swift  del Banco.
            public string numdoc;  //Número del Documento.
            public short cctlin;  //Se realizó transacción Cta. Cte. en Línea
            public string NroRef;
            public string SwiBco;
            public string Text1;
            public string Text2;
            public string Text3;
            public string Text4;
            public string Text5;
            public short IdCtran;
            public short ID;
            public short nroimp;


            public T_xOri()
            {
                NemMon = String.Empty;
                ctacte = String.Empty;
                NumCta = 0;
                NemCta = "";
                NomOri = "";
                CodMon = 0;
                MtoTot = 0;
                Status = 0;
                IdPrty = "";
                PosPrty = 0;
                PosOri = 0;
                CtaCte_t = "";
                MonExt = 0;
                codofi = 0;
                TipMov = 0;
                NumPar = 0;
                CodSwf = "";
                numdoc = "";
                cctlin = 0;
                NroRef = "";
                SwiBco = "";
                Text1 = "";
                Text2 = "";
                Text3 = "";
                Text4 = "";
                Text5 = "";
                IdCtran = 0;
                ID = 0;
                nroimp = 0;
            }
        }

        //**********************************************************
        //Variable General para Orígenes de Fondo.
        //**********************************************************
        public class T_gxOri
        {
            public string Partys;  //Id del Party.
            public short Acepto;  //Indica si Aceptó.-
            public short HayVue;  //Indica si Hay Vuelto.-
            public short ImpDeb;  //Indica si hay Impuesto al Débito.-
        }

        public class T_MODXORI
        {
            public T_Suc[] Vx_Suc;
            public T_CodTran[] Vx_CodTran;
            public S_Codtran[] Vx_SCodTran;
            public T_OriCC[] Vx_OriCC;
            public T_Ovd[] VOvd;
            public T_xMtoOri[] VxMtoOri;
            public T_xOri[] VxOri;

            public T_MODXORI()
            {
                Vx_Suc = new T_Suc[0];
                Vx_CodTran = new T_CodTran[0];
                Vx_SCodTran = new S_Codtran[0];
                Vx_OriCC = new T_OriCC[0];
                VOvd = new T_Ovd[0];
                VxMtoOri = new T_xMtoOri[0];
                VxOri = new T_xOri[0];
                VgxOri = new T_gxOri();
                VgxOriNul = new T_gxOri();
            }

            public string ori_des = "";
            public T_gxOri VgxOri;
            public T_gxOri VgxOriNul;

            //**********************************************************
            //Constantes para Tipo de Movimientos en Saldos con Sucursales.-
            //**********************************************************
            public const short TP_INI = 1;
            //Iniciativa.
            public const short TP_CON = 2;
            //Contrapartida.
            public const short TP_COM = 3;
            //Comunicado.
            public const short TP_COR = 4;
            //Corrección.
            //**********************************************************
            public const string MsgxOri = "Orígenes de Fondos";
            //Título de caja de Mensajes.
            public short Indice_Partys;  //Almacena el indice de la lista de los Partys.
            public const short ExOri_Eli = 3;
            //Estado Eliminado.-
            //**********************************************************
            public bool gb_esCosmos;
            public string gs_ctacte_party = "";
            private const string cSOAPCta = @"<?xml version=""1.0"" encoding=""utf-8""?>" + @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:con=""http://osb.bancochile.cl/ConsultaCuentaCorriente/"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:opc=""http://osb.bancochile.cl/ESB/ConsultaCuentaCorriente/OpConsultaCuentaCorrienteRequest"">" + "<soapenv:Header>" + "<con:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode></head:internalCode>" + "<head:idTransaccionNegocio>CDSWCX091028000093013915400</head:idTransaccionNegocio>" + "<head:fechaHora>2009-10-28T10:52:34</head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</con:headerRequest>";

            private const string cSOAPCta2 = "</soapenv:Header>" + "<soapenv:Body>" + "<con:ConsultaCuentaCorriente>" + "<reqConsultaCuentaCorriente>" + "<opc:cuenta>1</opc:cuenta>" + "</reqConsultaCuentaCorriente>" + "</con:ConsultaCuentaCorriente>" + "</soapenv:Body>" + "</soapenv:Envelope>";
        }
}
