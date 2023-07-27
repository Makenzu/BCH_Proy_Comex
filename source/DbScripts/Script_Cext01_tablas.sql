
GOTO fin_script 

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;


USE [cext01];
------------------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------------------------------------
PRINT N'Creating [dbo].[sce_cta].[IX_sce_cta_num]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_cta_num')
BEGIN
DROP INDEX [IX_sce_cta_num]
	ON [dbo].[sce_cta];
END

CREATE NONCLUSTERED INDEX [IX_sce_cta_num]
ON [dbo].[sce_cta]([cta_num] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sce_ctas].[ix_sce_ctas]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'ix_sce_ctas')
BEGIN
	DROP INDEX [ix_sce_ctas]
		ON [dbo].[sce_ctas];
END

CREATE NONCLUSTERED INDEX [ix_sce_ctas]
ON [dbo].[sce_ctas]([id_party] ASC, [cuenta] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sce_ini].[IX_sce_ini]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_ini')
BEGIN
DROP INDEX [IX_sce_ini]
	ON [dbo].[sce_ini];
END


CREATE NONCLUSTERED INDEX [IX_sce_ini]
ON [dbo].[sce_ini]([grupo] ASC, [eleme] ASC)
INCLUDE([tipov], [largo], [decim], [valor]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sce_rsa].[IX_sce_rsa_razon_soci]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_rsa_razon_soci')
BEGIN
DROP INDEX [IX_sce_rsa_razon_soci]
	ON [dbo].[sce_rsa];
END

CREATE NONCLUSTERED INDEX [IX_sce_rsa_razon_soci]
ON [dbo].[sce_rsa]([razon_soci] ASC)
INCLUDE([id_party]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sce_usr].[IX_sce_usr_rut]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_usr_rut')
BEGIN
DROP INDEX [IX_sce_usr_rut]
	ON [dbo].[sce_usr];
END


CREATE NONCLUSTERED INDEX [IX_sce_usr_rut]
ON [dbo].[sce_usr]([rut] ASC)
INCLUDE([direccion], [nombre]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sgt_loc].[IX_sgt_loc]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sgt_loc')
BEGIN
DROP INDEX [IX_sgt_loc]
	ON [dbo].[sgt_loc];
END


CREATE NONCLUSTERED INDEX [IX_sgt_loc]
ON [dbo].[sgt_loc]([loc_locnom] ASC)
INCLUDE([loc_loccod]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sgt_pai].[IX_sgt_pai]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sgt_pai')
BEGIN
DROP INDEX [IX_sgt_pai]
	ON [dbo].[sgt_pai];
END


CREATE NONCLUSTERED INDEX [IX_sgt_pai]
ON [dbo].[sgt_pai]([pai_painom] ASC)
INCLUDE([pai_paicod]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[tbl_sce_from_ESB2].[IX_tbl_sce_from_ESB2]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_tbl_sce_from_ESB2')
BEGIN
DROP INDEX [IX_tbl_sce_from_ESB2]
	ON [dbo].[tbl_sce_from_ESB2];
END


CREATE NONCLUSTERED INDEX [IX_tbl_sce_from_ESB2]
ON [dbo].[tbl_sce_from_ESB2]([XREF] ASC, [DRAMT] ASC) WITH (DATA_COMPRESSION = PAGE);




PRINT N'Creating [dbo].[tbl_sce_cvd_ft].[IX_tbl_sce_cvd_ft]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_tbl_sce_cvd_ft')
BEGIN
DROP INDEX [IX_tbl_sce_cvd_ft]
	ON [dbo].[tbl_sce_cvd_ft];
END


CREATE NONCLUSTERED INDEX [IX_tbl_sce_cvd_ft] ON [dbo].[tbl_sce_cvd_ft]
(
	[codcct] ASC,
	[codpro] ASC,
	[codesp] ASC,
	[codofi] ASC,
	[codope] ASC
)
INCLUDE ( 	[tip_cvd],	[valida_iny]) 
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)



PRINT N'Creating [dbo].[sce_inpl].[IX_sce_inpl]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_inpl')
BEGIN
DROP INDEX [IX_sce_inpl]
	ON [dbo].[sce_inpl];

END


CREATE NONCLUSTERED INDEX [IX_sce_inpl] ON [dbo].[sce_inpl]
(
	[cent_costo] ASC,
	[id_product] ASC,
	[id_especia] ASC,
	[id_empresa] ASC,
	[id_cobranz] ASC,
	[numplan] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)


PRINT N'Creating [dbo].[tbl_sce_parametros_ft].[IX_tbl_sce_parametros_ft]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_tbl_sce_parametros_ft')
BEGIN
DROP INDEX [IX_tbl_sce_parametros_ft]
	ON [dbo].[tbl_sce_parametros_ft];
END


CREATE NONCLUSTERED INDEX [IX_tbl_sce_parametros_ft] ON [dbo].[tbl_sce_parametros_ft]
(
	[tipo_ft] ASC,
	[codmnd_bch] ASC
)
INCLUDE ([cod_ft], [desc_ft])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)



PRINT N'Creating [dbo].[tbl_sce_tranope_ft].[IX_tbl_sce_tranope_ft]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_tbl_sce_tranope_ft')
BEGIN
DROP INDEX [IX_tbl_sce_tranope_ft]
	ON [dbo].[tbl_sce_tranope_ft];
END


CREATE NONCLUSTERED INDEX [IX_tbl_sce_tranope_ft] ON [dbo].[tbl_sce_tranope_ft]
(
	[codcct] ASC,
	[codpro] ASC,
	[codesp] ASC,
	[codofi] ASC,
	[codope] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)


PRINT N'Creating [dbo].[sce_vvi].[IX_sce_vvi]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_vvi')
BEGIN
DROP INDEX [IX_sce_vvi]
	ON [dbo].[sce_vvi];
END


CREATE NONCLUSTERED INDEX [IX_sce_vvi] ON [dbo].[sce_vvi]
(
	[codcct] ASC,
	[codpro] ASC,
	[codesp] ASC,
	[codofi] ASC,
	[codope] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)


PRINT N'Creating [dbo].[tbl_sce_tranope_ft].[IX_tbl_sce_tranope_ft]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_tbl_sce_tranope_ft')
BEGIN
DROP INDEX [IX_tbl_sce_tranope_ft]
	ON [dbo].[tbl_sce_tranope_ft];
END


CREATE NONCLUSTERED INDEX [IX_tbl_sce_tranope_ft] ON [dbo].[tbl_sce_tranope_ft]
(
	[codcct] ASC,
	[codpro] ASC,
	[codesp] ASC,
	[codofi] ASC,
	[codope] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)


PRINT N'Creating [dbo].[sce_cta].[IX_sce_cta_gl]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sce_cta_gl')
BEGIN
	DROP INDEX [IX_sce_cta_gl]
	ON [dbo].[sce_cta];
END


CREATE NONCLUSTERED INDEX [IX_sce_cta_gl] ON [dbo].[sce_cta]
(
	[cta_gl] ASC
)
INCLUDE ([cta_nem],[cta_mon],[cta_num],[cta_nom])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)



PRINT N'Creating [dbo].[sgt_vmc].[IX_sgt_vmc]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sgt_vmc')
BEGIN
DROP INDEX [IX_sgt_vmc]
	ON [dbo].[sgt_vmc];
END


CREATE NONCLUSTERED INDEX [IX_sgt_vmc] ON [dbo].[sgt_vmc]
(
	[vmc_vmccod] ASC,
	[vmc_vmcfec] ASC
)
include ([vmc_vmcprc],[vmc_vmctca])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE) ON [PRIMARY]


PRINT N'Update complete.';



SET QUOTED_IDENTIFIER ON


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'tbl_datos_usuario')
BEGIN
	CREATE TABLE [dbo].[tbl_datos_usuario](
		[samAccountName] [nvarchar](100) NOT NULL,
		[ConfigImpres_ImprimeCartas] [nvarchar](500) NULL,
		[ConfigImpres_ImprimePlanillas] [nvarchar](500) NULL,
		[ConfigImpres_ImprimeReporte] [nvarchar](500) NULL,
		[Configuracion_Sonidos] [nvarchar](500) NULL,
		[CtaCteLin_ArcHCCL] [nvarchar](500) NULL,
		[CtaCteLin_ArcLCCL] [nvarchar](500) NULL,
		[CtaCteLin_ServCCL] [nvarchar](500) NULL,
		[CtaCteLin_ServSOL] [nvarchar](500) NULL,
		[CtaCteLin_VistSOL] [nvarchar](500) NULL,
		[Entry_Usuario] [nvarchar](500) NULL,
		[Exportaciones_DocSwf] [nvarchar](500) NULL,
		[Exportaciones_TcpConDec] [nvarchar](500) NULL,
		[Exportaciones_TcpConvenio] [nvarchar](500) NULL,
		[Exportaciones_TcpSinPai] [nvarchar](500) NULL,
		[EXPOTAR_arch_export] [nvarchar](500) NULL,
		[EXPOTAR_dir_arch_export] [nvarchar](500) NULL,
		[EXPOTAR_ruta_excel] [nvarchar](500) NULL,
		[General_MndDol] [nvarchar](500) NULL,
		[General_MndNac] [nvarchar](500) NULL,
		[General_MndSinDec] [nvarchar](500) NULL,
		[General_MontoIVA] [nvarchar](500) NULL,
		[Identificacion_Alias] [nvarchar](500) NULL,
		[Identificacion_CCtUsr] [nvarchar](500) NULL,
		[Identificacion_CCtUsro] [nvarchar](500) NULL,
		[Identificacion_Impresora] [nvarchar](500) NULL,
		[Identificacion_Rut] [nvarchar](8) NULL,
		[Importaciones_TcpAutBcoCen] [nvarchar](500) NULL,
		[MODGUSR_UsrEsp_CentroCosto_CodBCCH] [nvarchar](500) NULL,
		[MODGUSR_UsrEsp_CentroCosto_CodBCH] [nvarchar](500) NULL,
		[MODGUSR_UsrEsp_CentroCosto_CodPBC] [nvarchar](500) NULL,
		[MODGUSR_UsrEsp_CentroCosto_SucBCH] [nvarchar](500) NULL,
		[Monedas_CodMonedaDolar] [nvarchar](500) NULL,
		[Monedas_CodMonedaNacional] [nvarchar](500) NULL,
		[Oficinas_UsrEsp_CentroCosto] [nvarchar](500) NULL,
		[Pais_CodPais] [nvarchar](500) NULL,
		[Participantes_PartyEnRed] [nvarchar](500) NULL,
		[Participantes_PartyNodo] [nvarchar](500) NULL,
		[Participantes_PartyServidor] [nvarchar](500) NULL,
		[SaldosCtaCte_NodoSalME] [nvarchar](500) NULL,
		[SaldosCtaCte_SerSalCCL] [nvarchar](500) NULL,
		[SaldosCtaCte_VisSalME] [nvarchar](500) NULL,
		[SaldosCtaCte_VisSalMN] [nvarchar](500) NULL,
		[SceIdi_PlazaCentral] [nvarchar](500) NULL,
		[SgtCCLin_NodoSgt] [nvarchar](500) NULL,
		[SgtCCLin_ServSgt] [nvarchar](500) NULL,
		[SgtCCLin_TabSgt] [nvarchar](500) NULL,
		[SgtCCLin_VisSgt] [nvarchar](500) NULL,
		[Swift103_BICEMI] [nvarchar](500) NULL,
		[Swift103_BICREC] [nvarchar](500) NULL,
		[Swift_23E_Reglas] [nvarchar](500) NULL,
		[SyBase_Base] [nvarchar](500) NULL,
		[SyBase_Nodo] [nvarchar](500) NULL,
		[SyBase_Servidor] [nvarchar](500) NULL,
		[SyBase_Usuario] [nvarchar](500) NULL,
		[Ubicacion_Entry] [nvarchar](500) NULL,
		[WebServices_IP] [nvarchar](500) NULL,
		[FirmasLocales] [varchar](MAX) NULL
	 CONSTRAINT [PK_tbl_datos_usuario] PRIMARY KEY CLUSTERED 
	(
		[samAccountName] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'MinsAlertaEnvioSwift' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD MinsAlertaEnvioSwift smallint
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'MinsAlertaAutorizacionSwift' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD MinsAlertaAutorizacionSwift smallint
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'MinsAlertaAdminEnvioSwift' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD MinsAlertaAdminEnvioSwift smallint
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'MinsAlertaRecepcionSwift' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD MinsAlertaRecepcionSwift smallint
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'BCHComexSwem_Casillas' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD BCHComexSwem_Casillas varchar(500)
END


SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER ON


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'tbl_grupoaplicacion')
BEGIN
	CREATE TABLE [dbo].[tbl_grupoaplicacion](
		[id] [nvarchar](100) NOT NULL,
		[name] [nvarchar](500) NOT NULL,
	 CONSTRAINT [PK_tbl_grupoaplicacion] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'tbl_aplicacion')
BEGIN
	CREATE TABLE [dbo].[tbl_aplicacion](
		[id] [nvarchar](100) NOT NULL,
		[name] [nvarchar](500) NOT NULL,
		[grupo_id] [nvarchar](100) NOT NULL,
		[ad_group_name] [nvarchar](500) NOT NULL,
		[controller] [nvarchar](500) NULL,
		[action] [nvarchar](500) NULL,
		[parameters] [nvarchar](2000) NULL,
	 CONSTRAINT [PK_tbl_aplicacion] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbl_aplicacion_tbl_grupoaplicacion]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbl_aplicacion]'))
BEGIN
ALTER TABLE [dbo].[tbl_aplicacion]  WITH CHECK ADD  CONSTRAINT [FK_tbl_aplicacion_tbl_grupoaplicacion] FOREIGN KEY([grupo_id])
REFERENCES [dbo].[tbl_grupoaplicacion] ([id])
END

IF  EXISTS (SELECT TOP 1 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbl_aplicacion_tbl_grupoaplicacion]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbl_aplicacion]'))
BEGIN
ALTER TABLE [dbo].[tbl_aplicacion] CHECK CONSTRAINT [FK_tbl_aplicacion_tbl_grupoaplicacion]
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'tbl_prty_ejc_MS')
BEGIN
	CREATE TABLE [dbo].[tbl_prty_ejc_MS](
		[prty_rut] [varchar] (12) COLLATE Latin1_General_100_CS_AS NOT NULL,
		[ejc_ofi] [numeric] (3, 0) NOT NULL,
		[ejc_cod] [numeric] (3, 0) NOT NULL,
		[ejc_tpo] [char] (1) COLLATE Latin1_General_100_CS_AS NOT NULL,
		[create_at] [datetime] NOT NULL,
		[update_at] [datetime] NOT NULL,
	 CONSTRAINT [pk_tbl_prty_ejc_MS] PRIMARY KEY NONCLUSTERED 
	(
		[prty_rut], [ejc_ofi], [ejc_cod], [ejc_tpo]
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END





IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'sce_swf_pendientes')
BEGIN
		CREATE TABLE [dbo].[sce_swf_pendientes](
			[ctecct] [char](3) NOT NULL,
			[codesp] [char](2) NOT NULL,
			[archivo] [varchar](6) NOT NULL,
			[rutAis] [varchar](15) NOT NULL,
			[sistema] [varchar](10) NOT NULL,
			[fecha] [datetime] NOT NULL,
			[tipo] [varchar](10) NOT NULL,
			[moneda] [varchar](10) NOT NULL,
			[monto] [decimal](18, 2) NOT NULL,
			[referencia] [varchar](16) NOT NULL,
			[msjSwift] [varchar](max) NOT NULL,
		 CONSTRAINT [PK_sce_swf_pendientes] PRIMARY KEY CLUSTERED 
		(
			[ctecct] ASC,
			[codesp] ASC,
			[archivo] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END



---------------------------------------------------------------------
IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'tbl_datos_usuario_codigos_sucursal')
BEGIN

	CREATE TABLE [dbo].[tbl_datos_usuario_codigos_sucursal](
		[samAccountName] [nvarchar](100) NOT NULL,
		[CentroCosto] [varchar](3) NOT NULL,
		[CodPBC] [varchar](5) NOT NULL,
		[CodBCH] [varchar](5) NOT NULL,
		[SucBCH] [varchar](5) NOT NULL,
		[CodBCCH] [varchar](5) NOT NULL,
	 CONSTRAINT [PK_tbl_datos_usuario_codigos_sucursal] PRIMARY KEY CLUSTERED 
	(
		[samAccountName] ASC,
		[CentroCosto] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	SET ANSI_PADDING OFF
	
	ALTER TABLE [dbo].[tbl_datos_usuario_codigos_sucursal]  WITH CHECK ADD  CONSTRAINT [FK_tbl_datos_usuario_codigos_sucursal_tbl_datos_usuario] FOREIGN KEY([samAccountName])
	REFERENCES [dbo].[tbl_datos_usuario] ([samAccountName])
	
	ALTER TABLE [dbo].[tbl_datos_usuario_codigos_sucursal] CHECK CONSTRAINT [FK_tbl_datos_usuario_codigos_sucursal_tbl_datos_usuario]
	
END


---------------------------------------------------------------------
--////////////Control Integral................

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_afp')
--BEGIN
 
--CREATE TABLE [dbo].[cambios_gral_afp](
--	[rut_padre] [bigint] NOT NULL,
--	[dv_padre] [char](1) NOT NULL,
--	[rut_hijo] [bigint] NOT NULL,
--	[dv_hijo] [char](1) NOT NULL
--) ON [PRIMARY]

--END
--go


--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_clientes')
--BEGIN
--CREATE TABLE [dbo].[cambios_gral_clientes](
--	[rutcli] [char](10) NULL,
--	[rut] [bigint] NULL,
--	[dv] [char](1) NULL,
--	[nomcli] [varchar](150) NULL,
--	[rut_sbl] [varchar](15) NULL,
--	[red] [varchar](120) NULL,
--	[segmento] [varchar](120) NULL,
--	[banca] [varchar](120) NULL,
--	[name_pos] [varchar](10) NULL,
--	[cod_ofi] [varchar](5) NULL,
--	[cod_ejc] [varchar](5) NULL,
--	[nom_ofi] [varchar](60) NULL,
--	[nom_ejc] [varchar](60) NULL,
--	[nom_ejc_cmx] [varchar](60) NULL,
--	[mail_ejc_cmx] [varchar](60) NULL,
--	[prodtype] [varchar](3) NULL,
--	[nomtype] [varchar](30) NULL,
--	[accnum] [varchar](20) NULL
--) ON [PRIMARY]

--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_moneda')
--BEGIN
--CREATE TABLE [dbo].[cambios_gral_moneda](
--	[mnd_mndcod] [int] NULL,
--	[mnd_mndcbc] [int] NULL,
--	[mnd_mndsbf] [int] NULL,
--	[mnd_mndnom] [varchar](30) NULL,
--	[mnd_mndnmc] [varchar](5) NULL,
--	[mnd_mndswf] [varchar](5) NULL,
--	[mnd_mndpai] [int] NULL,
--	[mnd_mndfiv] [char](8) NULL,
--	[mnd_mndftv] [char](8) NULL,
--	[mnd_mndina] [varchar](30) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_tarifas')
--BEGIN
--CREATE TABLE [dbo].[cambios_gral_tarifas](
--	[tf_rut_cli] [int] NOT NULL,
--	[tf_r_ccce] [numeric](12, 2) NULL,
--	[tf_r_goae] [numeric](12, 2) NULL,
--	[tf_r_dae] [numeric](12, 2) NULL,
--	[tf_r_taob] [numeric](12, 2) NULL,
--	[tf_r_caes] [numeric](12, 2) NULL,
--	[tf_r_gs] [numeric](12, 2) NULL,
--	[tf_e_evca] [numeric](12, 2) NULL,
--	[tf_e_caes] [numeric](12, 2) NULL,
--	[tf_e_gs] [numeric](12, 2) NULL,
--	[tf_obs1] [varchar](37) NULL,
--	[tf_obs2] [varchar](37) NULL,
--	[tf_obs3] [varchar](37) NULL,
--	[tf_t_env] [numeric](12, 2) NULL,
--	[tf_t_cheq] [numeric](12, 2) NULL,
--	[tf_t_reg] [numeric](12, 2) NULL,
--	[tf_c_cccc] [varchar](40) NULL,
--	[tf_c_goae] [varchar](40) NULL,
--	[tf_c_dae] [varchar](40) NULL,
--	[tf_c_taob] [varchar](40) NULL,
--	[tf_c_caes] [varchar](40) NULL,
--	[tf_c_gs] [varchar](40) NULL,
--	[tf_ce_evca] [varchar](40) NULL,
--	[tf_ce_caes] [varchar](40) NULL,
--	[tf_ce_gs] [varchar](40) NULL,
--	[tf_ct_env] [varchar](40) NULL,
--	[tf_ct_cheq] [varchar](40) NULL,
--	[tf_ct_reg] [varchar](40) NULL,
--	[tf_dv] [char](1) NULL,
--	[tf_rutcli] [char](10) NULL,
--	[tf_rutsbl] [varchar](12) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_tarifas_convenio')
--BEGIN
--CREATE TABLE [dbo].[cambios_gral_tarifas_convenio](
--	[rut] [bigint] NOT NULL,
--	[dv] [char](1) NULL,
--	[convenio] [varchar](15) NULL,
--	[tipo] [varchar](30) NULL,
--	[valor] [numeric](12, 2) NULL,
--	[observacion] [varchar](40) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_tarifas_pizarra')
--BEGIN
--CREATE TABLE [dbo].[cambios_gral_tarifas_pizarra](
--	[tipo] [varchar](15) NOT NULL,
--	[sub_tipo] [varchar](15) NOT NULL,
--	[rango_ini] [numeric](18, 2) NULL,
--	[rango_fin] [numeric](18, 2) NULL,
--	[tasa] [numeric](5, 2) NULL,
--	[minimo] [numeric](12, 2) NULL,
--	[maximo] [numeric](12, 2) NULL,
--	[swift] [numeric](12, 2) NULL,
--	[impto] [varchar](15) NULL,
--	[ourga] [numeric](12, 2) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_gral_tipo_cambio')
--BEGIN
--CREATE TABLE [dbo].[cambios_gral_tipo_cambio](
--	[MNDCOD] [smallint] NULL,
--	[MNDNOM] [varchar](30) NULL,
--	[MNDSBF] [smallint] NULL,
--	[MNDSWF] [varchar](3) NULL,
--	[MNDNMC] [varchar](3) NULL,
--	[VMDPRD] [numeric](17, 10) NULL,
--	[VMDMBC] [numeric](11, 4) NULL,
--	[VMDMBV] [numeric](11, 4) NULL,
--	[VMDOBS] [numeric](11, 4) NULL
--) ON [PRIMARY]
--END
--go


--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_check_list_log')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_check_list_log](
--	[accnum] [varchar](20) NULL,
--	[moneda] [char](3) NULL,
--	[monto] [numeric](18, 2) NULL,
--	[fecha] [datetime] NULL,
--	[hora] [datetime] NULL,
--	[usuario] [varchar](20) NULL,
--	[check_list] [varchar](15) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_contactos')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_contactos](
--	[Rut] [bigint] NULL,
--	[DV] [varchar](1) NULL,
--	[Rutdv] [varchar](12) NULL,
--	[NombreContacto] [varchar](150) NULL,
--	[CargoContacto] [varchar](50) NULL,
--	[Telefono1] [numeric](18, 0) NULL,
--	[Telefono2] [numeric](18, 0) NULL,
--	[Telefono3] [numeric](18, 0) NULL,
--	[Telefono4] [numeric](18, 0) NULL,
--	[usuario] [varchar](20) NULL,
--	[ID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_contratos')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_contratos](
--	[cargar] [varchar](20) NULL,
--	[rutcli] [varchar](12) NULL,
--	[rut] [bigint] NULL,
--	[dv] [char](1) NULL,
--	[rut_sbl] [varchar](12) NULL,
--	[nomcli] [varchar](255) NULL,
--	[segmento] [varchar](255) NULL,
--	[ejecutivo] [varchar](255) NULL,
--	[prodtype] [smallint] NULL,
--	[nomtype] [varchar](30) NULL,
--	[accnum] [varchar](20) NULL,
--	[cuenta] [varchar](20) NULL,
--	[base] [varchar](20) NULL,
--	[contrato_fax_local] [varchar](120) NULL,
--	[contrato_citi_offshore] [varchar](120) NULL,
--	[contrato_mift] [varchar](120) NULL,
--	[contrato_fax_NY_Londres] [varchar](120) NULL,
--	[contrato_otros] [varchar](250) NULL,
--	[contrato_anexo_mail] [varchar](250) NULL,
--	[indicador_fax_local] [varchar](2) NULL,
--	[indicador_citi_offshore] [varchar](2) NULL,
--	[indicador_mift] [varchar](2) NULL,
--	[indicador_fax_NY_Londres] [varchar](2) NULL,
--	[indicador_otros] [varchar](2) NULL,
--	[indicador_anexo_mail] [varchar](2) NULL,
--	[callfax] [text] NULL,
--	[fecha_ingreso_sist] [datetime] NULL
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_log')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_log](
--	[Fecha_hora] [datetime] NULL,
--	[Fecha] [datetime] NULL,
--	[Rut] [varchar](12) NULL,
--	[RutDv] [varchar](12) NULL,
--	[Cuenta] [varchar](20) NULL,
--	[NombreClte] [varchar](100) NULL,
--	[Segmento] [varchar](100) NULL,
--	[Ejecutivo] [varchar](100) NULL,
--	[Moneda] [varchar](3) NULL,
--	[monto] [bigint] NULL,
--	[CuentaBnf] [varchar](50) NULL,
--	[NombreBnf] [varchar](100) NULL,
--	[BancoInt] [varchar](20) NULL,
--	[BancoBnf] [varchar](20) NULL,
--	[Ind_Con_Otros] [varchar](2) NULL,
--	[Ind_Con_Mift] [varchar](2) NULL,
--	[Ind_Con_Fax] [varchar](2) NULL,
--	[Ind_Con_Citi] [varchar](2) NULL,
--	[Ind_Con_Fax_NY] [varchar](2) NULL,
--	[Ind_Con_Mail] [varchar](2) NULL,
--	[Txt_Con_Otros] [varchar](100) NULL,
--	[Txt_Con_Mift] [varchar](100) NULL,
--	[Txt_Con_Fax] [varchar](100) NULL,
--	[Txt_Con_Citi] [varchar](100) NULL,
--	[Txt_Con_Fax_NY] [varchar](100) NULL,
--	[Txt_Con_Mail] [varchar](100) NULL,
--	[Resultado] [varchar](20) NULL,
--	[Mensaje] [varchar](100) NULL,
--	[Usuario] [varchar](20) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_mensaje_cliente')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_mensaje_cliente](
--	[RUT] [bigint] NULL,
--	[CUENTA] [varchar](30) NULL,
--	[MENSAJE_01] [varchar](80) NULL,
--	[MENSAJE_02] [varchar](80) NULL,
--	[MENSAJE_03] [varchar](80) NULL,
--	[USU_INGRESO] [varchar](30) NULL,
--	[FECHA_INGRESO] [datetime] NULL,
--	[USU_ELIMINA] [varchar](30) NULL,
--	[FECHA_ELIMINA] [datetime] NULL,
--	[ACTIVO] [bit] NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_mesa_cvd_estado')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_mesa_cvd_estado](
--	[estado] [tinyint] NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_parametros')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_parametros](
--	[MONEDA] [varchar](3) NOT NULL,
--	[MONTO_MINIMO] [numeric](18, 2) NOT NULL
--) ON [PRIMARY]

--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_recurrencia')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_recurrencia](
--	[ord_rut] [varchar](35) NULL,
--	[ord_cuenta] [varchar](60) NULL,
--	[ord_nombre] [varchar](200) NULL,
--	[bnf_cuenta] [varchar](40) NULL,
--	[bnf_nombre] [varchar](200) NULL,
--	[bnf_swbcoint] [varchar](60) NULL,
--	[bnf_swfbco] [varchar](60) NULL,
--	[bnf_nombco] [varchar](150) NULL,
--	[cantidad] [bigint] NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_reparo_log')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_reparo_log](
--	[accnum] [varchar](20) NULL,
--	[rut] [varchar](20) NULL,
--	[nomcli] [varchar](60) NULL,
--	[moneda] [char](3) NULL,
--	[monto] [numeric](18, 2) NULL,
--	[doc_name] [varchar](80) NULL,
--	[mailejec] [varchar](80) NULL,
--	[fecha] [datetime] NULL,
--	[hora] [datetime] NULL,
--	[usuario] [varchar](20) NULL,
--	[reparo_list] [varchar](20) NULL,
--	[otro_1] [varchar](100) NULL,
--	[otro_2] [varchar](100) NULL,
--	[otro_3] [varchar](100) NULL,
--	[otro_4] [varchar](100) NULL,
--	[otro_5] [varchar](100) NULL
--) ON [PRIMARY]

--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_reparos_informe_detalle')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_reparos_informe_detalle](
--	[ejec_mail] [varchar](80) NULL,
--	[ejec_nom] [varchar](50) NULL,
--	[fecha] [char](10) NULL,
--	[rut] [varchar](15) NULL,
--	[CL] [varchar](80) NULL,
--	[rep_01] [varchar](2) NULL,
--	[rep_02] [varchar](2) NULL,
--	[rep_03] [varchar](2) NULL,
--	[rep_04] [varchar](2) NULL,
--	[rep_05] [varchar](2) NULL,
--	[rep_06] [varchar](2) NULL,
--	[rep_07] [varchar](2) NULL,
--	[rep_08] [varchar](2) NULL,
--	[rep_09] [varchar](2) NULL,
--	[rep_10] [varchar](2) NULL,
--	[rep_11] [varchar](2) NULL,
--	[rep_12] [varchar](2) NULL,
--	[rep_13] [varchar](2) NULL,
--	[rep_14] [varchar](2) NULL,
--	[rep_15] [varchar](2) NULL,
--	[rep_16] [varchar](2) NULL,
--	[rep_17] [varchar](2) NULL,
--	[rep_18] [varchar](2) NULL,
--	[rep_19] [varchar](2) NULL,
--	[rep_20] [varchar](2) NULL,
--	[otro_1] [varchar](100) NULL,
--	[otro_2] [varchar](100) NULL,
--	[otro_3] [varchar](100) NULL,
--	[otro_4] [varchar](100) NULL,
--	[otro_5] [varchar](100) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_reparos_informe_detalle_num')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_reparos_informe_detalle_num](
--	[ejec_mail] [varchar](80) NULL,
--	[ejec_nom] [varchar](50) NULL,
--	[rep_01] [smallint] NULL,
--	[rep_02] [smallint] NULL,
--	[rep_03] [smallint] NULL,
--	[rep_04] [smallint] NULL,
--	[rep_05] [smallint] NULL,
--	[rep_06] [smallint] NULL,
--	[rep_07] [smallint] NULL,
--	[rep_08] [smallint] NULL,
--	[rep_09] [smallint] NULL,
--	[rep_10] [smallint] NULL,
--	[rep_11] [smallint] NULL,
--	[rep_12] [smallint] NULL,
--	[rep_13] [smallint] NULL,
--	[rep_14] [smallint] NULL,
--	[rep_15] [smallint] NULL,
--	[rep_16] [smallint] NULL,
--	[rep_17] [smallint] NULL,
--	[rep_18] [smallint] NULL,
--	[rep_19] [smallint] NULL,
--	[rep_20] [smallint] NULL,
--	[otros] [smallint] NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_mift_reparos_informe_resumen')
--BEGIN
--CREATE TABLE [dbo].[cambios_mift_reparos_informe_resumen](
--	[ejec_mail] [varchar](80) NULL,
--	[ejec_nom] [varchar](50) NULL,
--	[ejec_cant] [int] NULL
--) ON [PRIMARY]
--END
--go


--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_revisor_mesa_cvd')
--BEGIN
--CREATE TABLE [dbo].[cambios_revisor_mesa_cvd](
--	[id_comex] [bigint] NOT NULL,
--	[fecha_transaccion] [datetime] NULL,
--	[id_tipo_transaccion] [int] NULL,
--	[id_estado] [int] NULL,
--	[rut_cliente] [int] NULL,
--	[monto_comex] [numeric](14, 2) NULL,
--	[punta_comex] [numeric](12, 5) NULL,
--	[tipo_cambio] [numeric](12, 5) NULL,
--	[id_referencia] [bigint] NULL,
--	[rut_operador] [int] NULL,
--	[rut_usu_oficina] [int] NULL,
--	[dv_usu_oficina] [char](1) NULL,
--	[nombre_usu_oficina] [varchar](50) NULL,
--	[tipo_operacion] [int] NULL,
--	[marca_destacada] [varchar](20) NULL,
--	[descripcion] [varchar](70) NULL,
--	[id_calzada] [int] NULL,
--	[id_forward] [int] NULL,
--	[rut_supervisor] [int] NULL,
--	[descripcion_acordada] [char](70) NULL,
--	[cod_oficina] [int] NULL,
--	[fecha_valor] [datetime] NULL,
--	[id_forma_pago_mn] [int] NULL,
--	[id_forma_pago_me] [int] NULL,
--	[utilidad] [float] NULL,
--	[pta_compra] [float] NULL,
--	[pta_venta] [float] NULL,
--	[pmo_compra] [float] NULL,
--	[pmo_venta] [float] NULL,
--	[id_origen_carga] [char](1) NULL,
--	[numero_origen] [char](25) NULL,
--	[vigente] [char](1) NULL,
--	[moneda_com] [smallint] NULL,
--	[moneda_vta] [smallint] NULL,
--	[precio_cliente] [numeric](12, 5) NULL
--) ON [PRIMARY]
--END
--go


--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_revisor_observado')
--BEGIN
--CREATE TABLE [dbo].[cambios_revisor_observado](
--	[codmon] [int] NOT NULL,
--	[fecha] [datetime] NOT NULL,
--	[observado] [numeric](18, 2) NOT NULL,
--	[paridad] [decimal](18, 8) NOT NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_revisor_sce_cvd')
--BEGIN
--CREATE TABLE [dbo].[cambios_revisor_sce_cvd](
--	[tiptrx] [tinyint] NULL,
--	[nrorpt] [bigint] NULL,
--	[fecmov] [datetime] NULL,
--	[codcct] [char](3) NULL,
--	[codpro] [char](2) NULL,
--	[codesp] [char](2) NULL,
--	[codofi] [char](3) NULL,
--	[codope] [char](5) NULL,
--	[rutcli] [varchar](12) NULL,
--	[nomcli] [varchar](60) NULL,
--	[tipcov] [char](1) NULL,
--	[codmnd] [smallint] NULL,
--	[mtocov] [numeric](16, 2) NULL,
--	[tipcam] [numeric](12, 4) NULL,
--	[mtopes] [numeric](16, 2) NULL,
--	[codtcp] [varchar](15) NULL,
--	[err_contab] [tinyint] NULL,
--	[err_valores] [tinyint] NULL,
--	[err_mensaje] [varchar](100) NULL,
--	[mesa_codmnd] [char](10) NULL,
--	[mesa_tipcam] [numeric](12, 4) NULL,
--	[mesa_mtoori] [numeric](14, 2) NULL,
--	[prom_tipcam] [numeric](12, 4) NULL,
--	[email_revisor] [varchar](50) NULL
--) ON [PRIMARY]
--END
--go


--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_tc_input_dia')
--BEGIN
--CREATE TABLE [dbo].[cambios_tc_input_dia](
--	[Document_Name] [nvarchar](255) NULL,
--	[Document_Type] [nvarchar](255) NULL,
--	[Folder] [nvarchar](255) NULL,
--	[Fecha] [datetime] NULL,
--	[Scanner] [nvarchar](255) NULL,
--	[F6] [varchar](15) NULL,
--	[Nombre_Cliente] [nvarchar](150) NULL,
--	[base] [bigint] NULL,
--	[Rut] [nvarchar](100) NULL,
--	[Monto] [numeric](20, 2) NULL,
--	[Referencia] [nvarchar](150) NULL,
--	[Secuencia] [varchar](10) NULL,
--	[F13] [nvarchar](50) NULL,
--	[N_operacion] [nvarchar](255) NULL,
--	[Fecha_Doc] [nvarchar](50) NULL,
--	[Ejecutivo] [nvarchar](255) NULL,
--	[I_producto] [nvarchar](255) NULL,
--	[fecha_hora_proceso] [datetime] NOT NULL,
--	[fecha_proceso] [datetime] NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_tc_input_log')
--BEGIN
--CREATE TABLE [dbo].[cambios_tc_input_log](
--	[userid] [varchar](20) NULL,
--	[fecha] [datetime] NULL,
--	[hora] [datetime] NULL,
--	[QRegistros] [numeric](18, 0) NULL,
--	[Mensaje] [varchar](3000) NULL
--) ON [PRIMARY]
--END
--go


--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'cambios_tc_input_new')
--BEGIN
--CREATE TABLE [dbo].[cambios_tc_input_new](
--	[Document_Name] [nvarchar](255) NULL,
--	[Document_Type] [nvarchar](255) NULL,
--	[Folder] [nvarchar](255) NULL,
--	[Fecha] [datetime] NULL,
--	[Scanner] [nvarchar](255) NULL,
--	[F6] [varchar](15) NULL,
--	[Nombre_Cliente] [nvarchar](150) NULL,
--	[base] [bigint] NULL,
--	[Rut] [nvarchar](100) NULL,
--	[Monto] [numeric](20, 2) NULL,
--	[Referencia] [nvarchar](150) NULL,
--	[Secuencia] [varchar](10) NULL,
--	[F13] [nvarchar](50) NULL,
--	[N_operacion] [nvarchar](255) NULL,
--	[Fecha_Doc] [nvarchar](50) NULL,
--	[Ejecutivo] [nvarchar](255) NULL,
--	[I_producto] [nvarchar](255) NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'gen_feriados')
--BEGIN
--CREATE TABLE [dbo].[gen_feriados](
--	[dia_fer] [datetime] NOT NULL
--) ON [PRIMARY]
--END
--go

--IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'sce_mcli_cox')
--BEGIN
--CREATE TABLE [dbo].[sce_mcli_cox](
--	[MCX_RUT] [int] NULL,
--	[MCX_DV] [nvarchar](1) NULL,
--	[MCX_NCLI] [nvarchar](50) NULL,
--	[MCX_BANCO] [nvarchar](2) NULL,
--	[MCX_EJCT] [int] NULL,
--	[MCX_NEJC] [nvarchar](50) NULL,
--	[MCX_EJNG] [int] NULL,
--	[MCX_NEJN] [nvarchar](50) NULL,
--	[MCX_INIC] [nvarchar](3) NULL,
--	[MCX_OFIE] [int] NULL,
--	[MCX_VOFI] [int] NULL,
--	[MCX_CDIV] [int] NULL,
--	[MCX_CARE] [int] NULL,
--	[MCX_CZON] [int] NULL,
--	[MCX_COFI] [int] NULL,
--	[MCX_NOFI] [nvarchar](50) NULL,
--	[MCX_MARC] [int] NULL,
--	[MCX_CLIC] [nvarchar](1) NULL,
--	[MCX_CLIE] [nvarchar](1) NULL,
--	[MCX_NRUT] [nvarchar](10) NULL,
--	[MCX_DIG] [nvarchar](1) NULL,
--	[MCX_DIVI] [nvarchar](5) NULL,
--	[MCX_NDIV] [nvarchar](50) NULL,
--	[MCX_AREA] [nvarchar](5) NULL,
--	[MCX_NARE] [nvarchar](50) NULL,
--	[MCX_ZONA] [nvarchar](5) NULL,
--	[MCX_NZON] [nvarchar](50) NULL,
--	[MCX_COX_CB] [int] NULL,
--	[MCX_DEPEND] [int] NULL,
--	[MCX_FCRE] [nvarchar](8) NULL,
--	[MCX_CLARIE] [nvarchar](2) NULL,
--	[MCX_FULTMV] [nvarchar](8) NULL,
--	[MCX_APERT] [int] NULL,
--	[MCX_INGRE] [int] NULL,
--	[MCX_NEW] [nvarchar](2) NULL,
--	[MCX_MAIL] [nvarchar](20) NULL,
--	[MCX_CTA_PE] [nvarchar](20) NULL,
--	[MCX_CTA_US] [nvarchar](20) NULL,
--	[MCX_TIPOC] [nvarchar](2) NULL,
--	[MCX_ZORI] [int] NULL,
--	[MCX_CAE] [int] NULL,
--	[MCX_TIPOX] [nvarchar](1) NULL,
--	[MCX_CUICLI] [int] NULL,
--	[MCX_NCUI] [nvarchar](50) NULL,
--	[MCX_REJC] [int] NULL
--) ON [PRIMARY]
--END
--go


--Final Control Integral


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'ConfigImpres_ContabilidadGenerica' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD ConfigImpres_ContabilidadGenerica varchar(500)
END



IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE Name = N'FirmasLocales' AND Object_ID = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD FirmasLocales varchar(max)
END



PRINT N'Dropping [sce_datos_cuadratura].[sce_datos_cuadratura_idx5]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sce_datos_cuadratura_idx5')
BEGIN
DROP INDEX [sce_datos_cuadratura_idx5]
    ON [dbo].[sce_datos_cuadratura];
END

SET ANSI_PADDING ON


CREATE NONCLUSTERED INDEX [sce_datos_cuadratura_idx5] ON [dbo].[sce_datos_cuadratura]
(
       [t_ano_mes] ASC,
       [t_num_ope] ASC
)
INCLUDE (    [t_cui],
       [t_numneg],
       [t_monpro],
       [t_tastot],
       [t_fecini],
       [t_fecfin],
       [t_dias],
       [t_tipcam],
       [t_rut],
       [t_estado],
       [t_mtovig],
       [t_cuenta_k],
       [t_nemonico_k],
       [t_mtome_c],
       [t_mtomn_c],
       [t_cuenta_c],
       [t_nemonico_c],
       [t_mtome_gn],
       [t_mtomn_gn],
       [t_cuenta_gn],
       [t_nemonico_gn],
       [t_mtome_gd],
       [t_mtomn_gd],
       [t_cuenta_gd],
       [t_nemonico_gd],
       [t_mtome_gdd],
       [t_mtomn_gdd],
       [t_cuenta_gdd],
       [t_nemonico_gdd],
       [t_mtome_cp],
       [t_mtomn_cp],
       [t_cuenta_cp],
       [t_nemonico_cp],
       [t_mtome_gpn],
       [t_mtomn_gpn],
       [t_cuenta_gpn],
       [t_nemonico_gpn],
       [t_mtome_gpd],
       [t_mtomn_gpd],
       [t_cuenta_gpd],
       [t_nemonico_gpd],
       [t_mtome_gpdd],
       [t_mtomn_gpdd],
       [t_cuenta_gpdd],
       [t_nemonico_gpdd],
       [t_tippro],
       [t_numpro],
       [t_numcuo],
       [t_fec_deterioro],
       [t_tasa_penal],
       [t_to],
       [t_to_plazo]) WITH (
       PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION=PAGE)



PRINT N'Creating [dbo].[tbl_datos_usuario].[ConfigImpres_Formato]...';

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns 
			WHERE name = N'ConfigImpres_Formato' AND object_id = Object_ID(N'tbl_datos_usuario'))
BEGIN
	ALTER TABLE dbo.tbl_datos_usuario
	ADD ConfigImpres_Formato varchar(50) NOT NULL CONSTRAINT DF_tbl_datos_usuario_ConfigImpres_Formato DEFAULT 'TIFF'
			
	exec('UPDATE [dbo].[tbl_datos_usuario] SET [ConfigImpres_Formato] = ''TIFF''')
   
	exec('UPDATE [dbo].[tbl_datos_usuario] SET [ConfigImpres_Formato] = ''HTML'' WHERE samAccountName like ''%nvainternet%''')     
END

PRINT N'Creating [dbo].[tbl_nemonico_validacion_inyeccion_swift]...';

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'tbl_nemonico_validacion_inyeccion_swift')
BEGIN
		CREATE TABLE [dbo].[tbl_nemonico_validacion_inyeccion_swift](
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[Nemonico] [varchar](50) NOT NULL,
			[CuentaContable] [varchar](10) NOT NULL,
			[Activo] [bit] NOT NULL,
		 CONSTRAINT [PK_tbl_nemonico_validacion_inyeccion_swift] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		insert into tbl_nemonico_validacion_inyeccion_swift values ('CC$','40001018',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCE','22110217',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCM-ME-1','22141228',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCM-ME-2','22141236',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCM-ME-3','22141252',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCM-MN-1','22140558',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCM-MN-2','22140574',1)
		insert into tbl_nemonico_validacion_inyeccion_swift values ('CCM-MN-3','',1)

end
-----------------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_version_tablas_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].proc_sce_version_tablas_MS
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_version_tablas_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sce_version_tablas_MS] AS
	BEGIN
		/*replace-begin*/select ''1.50.2''/*replace-end*/
	END'
END
-----------------------------------------------------------------------------------------------------------
fin_script: 
	print 'fin script Script_Cext01_tablas.sql'
