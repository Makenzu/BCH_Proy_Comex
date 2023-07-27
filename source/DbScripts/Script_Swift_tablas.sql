
GOTO fin_script
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;

USE swift

-----------------------------------------------------------------------------------------------------------------
PRINT N'Dropping [dbo].[sw_mensajes].[sk1_mensajes]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_mensajes')
BEGIN
DROP INDEX [sk1_mensajes]
    ON [dbo].[sw_mensajes];
END


PRINT N'Dropping [dbo].[sw_mensajes].[sk5_mensajes]...';



IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk5_mensajes')
BEGIN
DROP INDEX [sk5_mensajes]
    ON [dbo].[sw_mensajes];
END


PRINT N'Dropping [dbo].[sw_msgsend].[sk2_msgsend]...';



IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend')
DROP INDEX [sk2_msgsend]
    ON [dbo].[sw_msgsend];


PRINT N'Dropping [dbo].[sw_msgsend_detfile].[sk1_msgsend_detfile]...';



IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_msgsend_detfile')
DROP INDEX [sk1_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile];



PRINT N'Dropping [dbo].[sw_msgsend_detfile].[sk2_msgsend_detfile]...';



IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend_detfile')
DROP INDEX [sk2_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile];



PRINT N'Dropping [dbo].[sw_bancos].[pk_bancos]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_bancos')
DROP INDEX [pk_bancos]
    ON [dbo].[sw_bancos];


PRINT N'Dropping [dbo].[sw_campos_msg].[pk_campo]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_campo')
DROP INDEX [pk_campo]
    ON [dbo].[sw_campos_msg];


PRINT N'Dropping [dbo].[sw_caracter_error].[pk_caracter_error]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_caracter_error')
DROP INDEX [pk_caracter_error]
    ON [dbo].[sw_caracter_error];


PRINT N'Dropping [dbo].[sw_casillas].[pk_casillas]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_casillas')
DROP INDEX [pk_casillas]
    ON [dbo].[sw_casillas];


PRINT N'Dropping [dbo].[sw_ciclos].[pk_ciclo]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_ciclo')
DROP INDEX [pk_ciclo]
    ON [dbo].[sw_ciclos];


PRINT N'Dropping [dbo].[sw_configura].[pk_configura]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_configura')
DROP INDEX [pk_configura]
    ON [dbo].[sw_configura];


PRINT N'Dropping [dbo].[sw_estados_msg].[pk_estados_msg]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_estados_msg')
DROP INDEX [pk_estados_msg]
    ON [dbo].[sw_estados_msg];


PRINT N'Dropping [dbo].[sw_folios].[pk_folios]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_folios')
DROP INDEX [pk_folios]
    ON [dbo].[sw_folios];


PRINT N'Dropping [dbo].[sw_formatos].[pk_formato]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_formato')
DROP INDEX [pk_formato]
    ON [dbo].[sw_formatos];


PRINT N'Dropping [dbo].[sw_log_msg].[pk_log]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_log')
DROP INDEX [pk_log]
    ON [dbo].[sw_log_msg];


PRINT N'Dropping [dbo].[sw_mensajes].[pk_mensajes]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_mensajes')
DROP INDEX [pk_mensajes]
    ON [dbo].[sw_mensajes];


PRINT N'Dropping [dbo].[sw_mensajes_add].[pk_mensajes_add]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_mensajes_add')
DROP INDEX [pk_mensajes_add]
    ON [dbo].[sw_mensajes_add];


PRINT N'Dropping [dbo].[sw_monedas].[pk_moneda]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_moneda')
DROP INDEX [pk_moneda]
    ON [dbo].[sw_monedas];


PRINT N'Dropping [dbo].[sw_msgsend].[pk_msgsend]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend')
DROP INDEX [pk_msgsend]
    ON [dbo].[sw_msgsend];


PRINT N'Dropping [dbo].[sw_msgsend_add].[pk_msgsend_add]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_add')
DROP INDEX [pk_msgsend_add]
    ON [dbo].[sw_msgsend_add];


PRINT N'Dropping [dbo].[sw_msgsend_cambio_estados].[pk_sw_msgsend_cambio_estados]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_sw_msgsend_cambio_estados')
DROP INDEX [pk_sw_msgsend_cambio_estados]
    ON [dbo].[sw_msgsend_cambio_estados];


PRINT N'Dropping [dbo].[sw_msgsend_cmx_dia].[pk_msgsend_cmx_dia]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_cmx_dia')
DROP INDEX [pk_msgsend_cmx_dia]
    ON [dbo].[sw_msgsend_cmx_dia];


PRINT N'Dropping [dbo].[sw_msgsend_detfile].[pk_msgsend_detfile]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_detfile')
DROP INDEX [pk_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile];


PRINT N'Dropping [dbo].[sw_msgsend_files].[pk_msgsend_files]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_files')
DROP INDEX [pk_msgsend_files]
    ON [dbo].[sw_msgsend_files];


PRINT N'Dropping [dbo].[sw_msgsend_firma].[pk_msgsend_firma]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_firma')
DROP INDEX [pk_msgsend_firma]
    ON [dbo].[sw_msgsend_firma];


PRINT N'Dropping [dbo].[sw_msgsend_firma].[sk_5_msgsend_firma]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk_5_msgsend_firma')
DROP INDEX [sk_5_msgsend_firma]
    ON [dbo].[sw_msgsend_firma];


PRINT N'Dropping [dbo].[sw_msgsend_log].[pk_msgsend_log]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_log')
DROP INDEX [pk_msgsend_log]
    ON [dbo].[sw_msgsend_log];


PRINT N'Dropping [dbo].[sw_msgsend_nop].[pk_msgsend_nop]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_msgsend_nop')
DROP INDEX [pk_msgsend_nop]
    ON [dbo].[sw_msgsend_nop];


PRINT N'Dropping [dbo].[sw_orden_firmas].[pk_orden_firmas]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_orden_firmas')
DROP INDEX [pk_orden_firmas]
    ON [dbo].[sw_orden_firmas];


PRINT N'Dropping [dbo].[sw_paridad].[pk_paridad]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_paridad')
DROP INDEX [pk_paridad]
    ON [dbo].[sw_paridad];


PRINT N'Dropping [dbo].[sw_regla_firmas].[pk_regla_firmas]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_regla_firmas')
DROP INDEX [pk_regla_firmas]
    ON [dbo].[sw_regla_firmas];


PRINT N'Dropping [dbo].[sw_tipos_campos].[pk_campo_tipcam]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_campo_tipcam')
DROP INDEX [pk_campo_tipcam]
    ON [dbo].[sw_tipos_campos];


PRINT N'Dropping [dbo].[sw_tipos_firmas].[pk_tipos_firmas]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_tipos_firmas')
DROP INDEX [pk_tipos_firmas]
    ON [dbo].[sw_tipos_firmas];


PRINT N'Dropping [dbo].[sw_tipos_msg].[pk_tipo]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_tipo')
DROP INDEX [pk_tipo]
    ON [dbo].[sw_tipos_msg];


PRINT N'Dropping [dbo].[sw_users_swift].[pk_users_srm]...';


IF EXISTS(SELECT object_name(object_id), * FROM sys.indexes WHERE name = 'pk_users_srm' and object_id=object_id('sw_users_swift'))
DROP INDEX [pk_users_srm]
    ON [dbo].[sw_users_swift];



PRINT N'Dropping [dbo].[sw_valor_campos].[pk_valor_campo]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'pk_valor_campo')
DROP INDEX [pk_valor_campo]
    ON [dbo].[sw_valor_campos];


PRINT N'Dropping [dbo].[tbl_sw_nets].[sk2_sw_nets]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_sw_nets')
DROP INDEX [sk2_sw_nets]
    ON [dbo].[tbl_sw_nets];


PRINT N'Dropping [dbo].[tbl_sw_nets_det].[sk1_sw_nets_det]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_sw_nets_det')
BEGIN
DROP INDEX [sk1_sw_nets_det]
    ON [dbo].[tbl_sw_nets_det];
END

PRINT N'Creating [dbo].[PK_sw_bancos]...';


IF OBJECT_ID('PK_sw_bancos') IS NOT NULL 
    ALTER TABLE dbo.[sw_bancos] DROP CONSTRAINT [PK_sw_bancos]


	ALTER TABLE [dbo].[sw_bancos]
    ADD CONSTRAINT [PK_sw_bancos] PRIMARY KEY NONCLUSTERED ([cod_banco] ASC, [branch] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_bancos_autorizados]...';


IF OBJECT_ID('PK_sw_bancos_autorizados') IS NOT NULL 
    ALTER TABLE dbo.[sw_bancos_autorizados] DROP CONSTRAINT [PK_sw_bancos_autorizados]



ALTER TABLE [dbo].[sw_bancos_autorizados]
    ADD CONSTRAINT [PK_sw_bancos_autorizados] PRIMARY KEY NONCLUSTERED ([cod_banco] ASC, [branch] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[PK_sw_campos_msg]...';


IF OBJECT_ID('PK_sw_campos_msg') IS NOT NULL 
    ALTER TABLE dbo.[sw_campos_msg] DROP CONSTRAINT [PK_sw_campos_msg]


ALTER TABLE [dbo].[sw_campos_msg]
    ADD CONSTRAINT [PK_sw_campos_msg] PRIMARY KEY NONCLUSTERED ([tag_campo] ASC, [linea_campo] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[PK_sw_caracter_error]...';

IF OBJECT_ID('PK_sw_caracter_error') IS NOT NULL 
    ALTER TABLE dbo.[sw_caracter_error] DROP CONSTRAINT [PK_sw_caracter_error]


ALTER TABLE [dbo].[sw_caracter_error]
    ADD CONSTRAINT [PK_sw_caracter_error] PRIMARY KEY NONCLUSTERED ([valor_ascii] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_casillas]...';


IF OBJECT_ID('PK_sw_casillas') IS NOT NULL 
    ALTER TABLE dbo.[sw_casillas] DROP CONSTRAINT [PK_sw_casillas]


ALTER TABLE [dbo].[sw_casillas]
    ADD CONSTRAINT [PK_sw_casillas] PRIMARY KEY NONCLUSTERED ([cod_casilla] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_ciclos]...';


IF OBJECT_ID('PK_sw_ciclos') IS NOT NULL 
    ALTER TABLE dbo.[sw_ciclos] DROP CONSTRAINT [PK_sw_ciclos]


ALTER TABLE [dbo].[sw_ciclos]
    ADD CONSTRAINT [PK_sw_ciclos] PRIMARY KEY NONCLUSTERED ([tipo_msg_fmt] ASC, [orden_fmt] ASC, [tag_fmt] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[PK_sw_configura]...';

IF OBJECT_ID('PK_sw_configura') IS NOT NULL 
    ALTER TABLE dbo.[sw_configura] DROP CONSTRAINT [PK_sw_configura]


ALTER TABLE [dbo].[sw_configura]
    ADD CONSTRAINT [PK_sw_configura] PRIMARY KEY NONCLUSTERED ([rut_usuario] ASC, [cod_aplicac] ASC) WITH (DATA_COMPRESSION = PAGE);


------------------------------------------------------------------------------------------------------------------------------------------

PRINT N'Creating [dbo].[PK_sw_estados_msg]...';

IF OBJECT_ID('PK_sw_estados_msg') IS NOT NULL 
    ALTER TABLE dbo.[sw_estados_msg] DROP CONSTRAINT [PK_sw_estados_msg]


ALTER TABLE [dbo].[sw_estados_msg]
    ADD CONSTRAINT [PK_sw_estados_msg] PRIMARY KEY NONCLUSTERED ([estado_msg] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[PK_sw_folios]...';

IF OBJECT_ID('PK_sw_folios') IS NOT NULL 
    ALTER TABLE dbo.[sw_folios] DROP CONSTRAINT [PK_sw_folios] 


ALTER TABLE [dbo].[sw_folios]
    ADD CONSTRAINT [PK_sw_folios] PRIMARY KEY NONCLUSTERED ([id_folio] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_formatos]...';

IF OBJECT_ID('PK_sw_formatos') IS NOT NULL 
    ALTER TABLE dbo.[sw_formatos] DROP CONSTRAINT [PK_sw_formatos]


ALTER TABLE [dbo].[sw_formatos]
    ADD CONSTRAINT [PK_sw_formatos] PRIMARY KEY NONCLUSTERED ([tipo_msg_fmt] ASC, [orden_fmt] ASC, [tag_fmt] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_glosa_cmx]...';

IF OBJECT_ID('PK_sw_glosa_cmx') IS NOT NULL 
    ALTER TABLE dbo.[sw_glosa_cmx] DROP CONSTRAINT [PK_sw_glosa_cmx]


ALTER TABLE [dbo].[sw_glosa_cmx]
    ADD CONSTRAINT [PK_sw_glosa_cmx] PRIMARY KEY NONCLUSTERED ([tipo_msg] ASC, [cod_msg] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[PK_sw_log_msg]...';

IF OBJECT_ID('PK_sw_log_msg') IS NOT NULL 
    ALTER TABLE dbo.[sw_log_msg] DROP CONSTRAINT [PK_sw_log_msg]



ALTER TABLE [dbo].[sw_log_msg]
    ADD CONSTRAINT [PK_sw_log_msg] PRIMARY KEY NONCLUSTERED ([sesion_log] ASC, [secuencia_log] ASC, [send_recv_log] ASC, [fecha_log] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_mensajes]...';

IF OBJECT_ID('PK_sw_mensajes') IS NOT NULL 
    ALTER TABLE dbo.[sw_mensajes] DROP CONSTRAINT [PK_sw_mensajes]



ALTER TABLE [dbo].[sw_mensajes]
    ADD CONSTRAINT [PK_sw_mensajes] PRIMARY KEY NONCLUSTERED ([sesion] ASC, [secuencia] ASC, [send_recv] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_mensajes].[sk1_mensajes]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_mensajes')
	DROP INDEX [sk1_mensajes]
    ON [dbo].[sw_mensajes];


	CREATE NONCLUSTERED INDEX [sk1_mensajes]
    ON [dbo].[sw_mensajes]([casilla] ASC, [send_recv] ASC, [estado_msg] ASC)
    INCLUDE([tipo_msg], [cod_banco_rec], [branch_rec]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_mensajes].[sk2_mensajes]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_mensajes')
	ALTER INDEX [sk2_mensajes]
    ON [dbo].[sw_mensajes] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_mensajes].[sk4_mensajes]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk4_mensajes')
	ALTER INDEX [sk4_mensajes]
    ON [dbo].[sw_mensajes] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_mensajes].[sk5_mensajes]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk5_mensajes') 
	DROP INDEX [sk5_mensajes]
    ON [dbo].[sw_mensajes];



	CREATE NONCLUSTERED INDEX [sk5_mensajes]
    ON [dbo].[sw_mensajes]([fecha_send] ASC, [send_recv] ASC, [tipo_msg] ASC, [casilla] ASC, [cod_moneda] ASC)
    INCLUDE([cod_banco_rec], [branch_rec]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_mensajes_add]...';

IF OBJECT_ID('PK_sw_mensajes_add') IS NOT NULL 
    ALTER TABLE dbo.[sw_mensajes_add] DROP CONSTRAINT [PK_sw_mensajes_add]


	ALTER TABLE [dbo].[sw_mensajes_add]
    ADD CONSTRAINT [PK_sw_mensajes_add] PRIMARY KEY NONCLUSTERED ([sesion] ASC, [secuencia] ASC, [send_recv] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_mensajes_add].[sk2_mensajes_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_mensajes_add')
	ALTER INDEX [sk2_mensajes_add]
    ON [dbo].[sw_mensajes_add] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_mensajes_add].[sk3_mensajes_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk3_mensajes_add')
	ALTER INDEX [sk3_mensajes_add]
    ON [dbo].[sw_mensajes_add] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_mensajes_add].[sk4_mensajes_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk4_mensajes_add')
	ALTER INDEX [sk4_mensajes_add]
    ON [dbo].[sw_mensajes_add] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_monedas]...';

IF OBJECT_ID('PK_sw_monedas') IS NOT NULL 
    ALTER TABLE dbo.[sw_monedas] DROP CONSTRAINT [PK_sw_monedas]


	ALTER TABLE [dbo].[sw_monedas]
    ADD CONSTRAINT [PK_sw_monedas] PRIMARY KEY NONCLUSTERED ([cod_moneda_sw] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend]...';

IF OBJECT_ID('PK_sw_msgsend') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend] DROP CONSTRAINT [PK_sw_msgsend]



	ALTER TABLE [dbo].[sw_msgsend]
    ADD CONSTRAINT [PK_sw_msgsend] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend].[sk1_msgsend]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_msgsend')
	ALTER INDEX [sk1_msgsend]
    ON [dbo].[sw_msgsend] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend].[sk2_msgsend]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend')
	DROP INDEX [sk2_msgsend]
    ON [dbo].[sw_mensajes];


	CREATE NONCLUSTERED INDEX [sk2_msgsend]
    ON [dbo].[sw_msgsend]([casilla] ASC)
    INCLUDE([tipo_msg], [cod_banco_em], [branch_em]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend].[sk6_msgsend]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk6_msgsend')
	ALTER INDEX [sk6_msgsend]
    ON [dbo].[sw_msgsend] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend].[sk7_msgsend]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk7_msgsend')
	ALTER INDEX [sk7_msgsend]
    ON [dbo].[sw_msgsend] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend].[sk8_msgsend]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk8_msgsend')
	DROP INDEX [sk8_msgsend]
    ON [dbo].[sw_msgsend];



	CREATE NONCLUSTERED INDEX [sk8_msgsend]
    ON [dbo].[sw_msgsend]([casilla] ASC, [estado_msg] ASC, [id_mensaje] ASC)
    INCLUDE([sesion], [secuencia], [tipo_msg], [prioridad], [rut_ingreso], [fecha_ingreso], [cod_banco_rec], [branch_rec], [cod_banco_em], [branch_em], [cod_moneda], [monto], [referencia], [beneficiario]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend].[sk9_msgsend]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk9_msgsend')
	DROP INDEX [sk9_msgsend]
    ON [dbo].[sw_msgsend];



	CREATE NONCLUSTERED INDEX [sk9_msgsend]
    ON [dbo].[sw_msgsend]([estado_msg] ASC, [id_mensaje] ASC)
    INCLUDE([sesion], [casilla], [secuencia], [tipo_msg], [prioridad], [rut_ingreso], [fecha_ingreso], [cod_banco_rec], [branch_rec], [cod_banco_em], [branch_em], [cod_moneda], [monto], [referencia], [beneficiario]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_add]...';

IF OBJECT_ID('PK_sw_msgsend_add') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_add] DROP CONSTRAINT [PK_sw_msgsend_add]



	ALTER TABLE [dbo].[sw_msgsend_add]
    ADD CONSTRAINT [PK_sw_msgsend_add] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_add].[sk1_msgsend_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_msgsend_add')
	ALTER INDEX [sk1_msgsend_add]
    ON [dbo].[sw_msgsend_add] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_add].[sk2_msgsend_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend_add')
	ALTER INDEX [sk2_msgsend_add]
    ON [dbo].[sw_msgsend_add] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_add].[sk3_msgsend_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk3_msgsend_add') 
	ALTER INDEX [sk3_msgsend_add]
    ON [dbo].[sw_msgsend_add] REBUILD WITH(DATA_COMPRESSION = PAGE); 

PRINT N'Creating [dbo].[sw_msgsend_add].[sk4_msgsend_add]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk4_msgsend_add')
	ALTER INDEX [sk4_msgsend_add]
    ON [dbo].[sw_msgsend_add] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_msgsend_cambio_estados]...';

IF OBJECT_ID('PK_sw_msgsend_cambio_estados') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_cambio_estados] DROP CONSTRAINT [PK_sw_msgsend_cambio_estados]


	ALTER TABLE [dbo].[sw_msgsend_cambio_estados]
    ADD CONSTRAINT [PK_sw_msgsend_cambio_estados] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC, [fecha_estado] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_cmx]...';

IF OBJECT_ID('PK_sw_msgsend_cmx') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_cmx] DROP CONSTRAINT [PK_sw_msgsend_cmx]


	ALTER TABLE [dbo].[sw_msgsend_cmx]
    ADD CONSTRAINT [PK_sw_msgsend_cmx] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_cmx_dia]...';

IF OBJECT_ID('PK_sw_msgsend_cmx_dia') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_cmx_dia] DROP CONSTRAINT [PK_sw_msgsend_cmx_dia]


	ALTER TABLE [dbo].[sw_msgsend_cmx_dia]
    ADD CONSTRAINT [PK_sw_msgsend_cmx_dia] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[sw_msgsend_cmx_dia].[sk1_msgsend_cmx_dia]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_msgsend_cmx_dia')
	ALTER INDEX [sk1_msgsend_cmx_dia]
    ON [dbo].[sw_msgsend_cmx_dia] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_cmx_dia].[sk2_msgsend_cmx_dia]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend_cmx_dia')	
	ALTER INDEX [sk2_msgsend_cmx_dia]
    ON [dbo].[sw_msgsend_cmx_dia] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_cmx_dia].[sk3_msgsend_cmx_dia]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk3_msgsend_cmx_dia')
	ALTER INDEX [sk3_msgsend_cmx_dia]
    ON [dbo].[sw_msgsend_cmx_dia] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_cmx_dia].[sk4_msgsend_cmx_dia]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk4_msgsend_cmx_dia')
	ALTER INDEX [sk4_msgsend_cmx_dia]
    ON [dbo].[sw_msgsend_cmx_dia] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_cmx_dia].[sk5_msgsend_cmx_dia]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk5_msgsend_cmx_dia')
	ALTER INDEX [sk5_msgsend_cmx_dia]
    ON [dbo].[sw_msgsend_cmx_dia] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_msgsend_detfile]...';

IF OBJECT_ID('PK_sw_msgsend_detfile') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_detfile] DROP CONSTRAINT [PK_sw_msgsend_detfile]


	ALTER TABLE [dbo].[sw_msgsend_detfile]
    ADD CONSTRAINT [PK_sw_msgsend_detfile] PRIMARY KEY NONCLUSTERED ([fd_archivo] ASC, [id_mensaje] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_detfile].[sk1_msgsend_detfile]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_msgsend_detfile')
	DROP INDEX [sk1_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile];


	CREATE NONCLUSTERED INDEX [sk1_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile]([id_mensaje] ASC)
    INCLUDE([fecha_envio], [fd_archivo]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_detfile].[sk2_msgsend_detfile]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend_detfile')
	DROP INDEX [sk2_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile];


	CREATE NONCLUSTERED INDEX [sk2_msgsend_detfile]
    ON [dbo].[sw_msgsend_detfile]([fecha_envio] ASC)
    INCLUDE([id_mensaje], [fd_archivo]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_files]...';

IF OBJECT_ID('PK_sw_msgsend_files') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_files] DROP CONSTRAINT [PK_sw_msgsend_files]


	ALTER TABLE [dbo].[sw_msgsend_files]
    ADD CONSTRAINT [PK_sw_msgsend_files] PRIMARY KEY NONCLUSTERED ([fd_archivo] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_files].[sk4_msgsend_files]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk4_msgsend_files')
	ALTER INDEX [sk4_msgsend_files]
    ON [dbo].[sw_msgsend_files] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_files].[sk5_msgsend_files]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk5_msgsend_files')
	ALTER INDEX [sk5_msgsend_files]
    ON [dbo].[sw_msgsend_files] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_files].[sk6_msgsend_files]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk6_msgsend_files')
	ALTER INDEX [sk6_msgsend_files]
    ON [dbo].[sw_msgsend_files] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_firma]...';

IF OBJECT_ID('PK_sw_msgsend_firma') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_firma] DROP CONSTRAINT [PK_sw_msgsend_firma]


	ALTER TABLE [dbo].[sw_msgsend_firma]
    ADD CONSTRAINT [PK_sw_msgsend_firma] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC, [rut_firma] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_firma].[sk1_msgsend_firma]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_msgsend_firma')
	ALTER INDEX [sk1_msgsend_firma]
    ON [dbo].[sw_msgsend_firma] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_firma].[sk2_msgsend_firma]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk2_msgsend_firma')
	ALTER INDEX [sk2_msgsend_firma]
    ON [dbo].[sw_msgsend_firma] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_firma].[sk3_msgsend_firma]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk3_msgsend_firma')
	ALTER INDEX [sk3_msgsend_firma]
    ON [dbo].[sw_msgsend_firma] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_firma].[sk4_msgsend_firma]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk4_msgsend_firma')
	ALTER INDEX [sk4_msgsend_firma]
    ON [dbo].[sw_msgsend_firma] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_firma].[sk5_msgsend_firma]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk5_msgsend_firma')
	DROP INDEX [sk5_msgsend_firma]
    ON [dbo].[sw_msgsend_firma];


	CREATE NONCLUSTERED INDEX [sk5_msgsend_firma]
    ON [dbo].[sw_msgsend_firma]([rut_solic] ASC, [estado_firma] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_log]...';

IF OBJECT_ID('PK_sw_msgsend_log') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_log] DROP CONSTRAINT [PK_sw_msgsend_log]


	ALTER TABLE [dbo].[sw_msgsend_log]
    ADD CONSTRAINT [PK_sw_msgsend_log] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC, [fecha_log] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_msgsend_nop]...';

IF OBJECT_ID('PK_sw_msgsend_nop') IS NOT NULL 
    ALTER TABLE dbo.[sw_msgsend_nop] DROP CONSTRAINT [PK_sw_msgsend_nop]


	ALTER TABLE [dbo].[sw_msgsend_nop]
    ADD CONSTRAINT [PK_sw_msgsend_nop] PRIMARY KEY NONCLUSTERED ([sesion] ASC, [secuencia] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_orden_firmas]...';

IF OBJECT_ID('PK_sw_orden_firmas') IS NOT NULL 
    ALTER TABLE dbo.[sw_orden_firmas] DROP CONSTRAINT [PK_sw_orden_firmas]


	ALTER TABLE [dbo].[sw_orden_firmas]
    ADD CONSTRAINT [PK_sw_orden_firmas] PRIMARY KEY NONCLUSTERED ([orden] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_paridad]...';
	
IF OBJECT_ID('PK_sw_paridad') IS NOT NULL 
    ALTER TABLE dbo.[sw_paridad] DROP CONSTRAINT [PK_sw_paridad]


	ALTER TABLE [dbo].[sw_paridad]
    ADD CONSTRAINT [PK_sw_paridad] PRIMARY KEY NONCLUSTERED ([cod_moneda_banco] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_regla_firmas]...';

IF OBJECT_ID('PK_sw_regla_firmas') IS NOT NULL 
    ALTER TABLE dbo.[sw_regla_firmas] DROP CONSTRAINT [PK_sw_regla_firmas]


	ALTER TABLE [dbo].[sw_regla_firmas]
    ADD CONSTRAINT [PK_sw_regla_firmas] PRIMARY KEY NONCLUSTERED ([id_firma] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_sce_bic]...';

IF OBJECT_ID('PK_sw_sce_bic') IS NOT NULL 
    ALTER TABLE dbo.[sw_sce_bic] DROP CONSTRAINT [PK_sw_sce_bic]


	ALTER TABLE [dbo].[sw_sce_bic]
    ADD CONSTRAINT [PK_sw_sce_bic] PRIMARY KEY NONCLUSTERED ([bankcode] ASC, [branchcode] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_tipos_campos]...';

IF OBJECT_ID('PK_sw_tipos_campos') IS NOT NULL 
    ALTER TABLE dbo.[sw_tipos_campos] DROP CONSTRAINT [PK_sw_tipos_campos]


	ALTER TABLE [dbo].[sw_tipos_campos]
    ADD CONSTRAINT [PK_sw_tipos_campos] PRIMARY KEY NONCLUSTERED ([tag_campo_tipcam] ASC, [tipo_msg_tipcam] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_tipos_firmas]...';

IF OBJECT_ID('PK_sw_tipos_firmas') IS NOT NULL 
    ALTER TABLE dbo.[sw_tipos_firmas] DROP CONSTRAINT [PK_sw_tipos_firmas]


	ALTER TABLE [dbo].[sw_tipos_firmas]
    ADD CONSTRAINT [PK_sw_tipos_firmas] PRIMARY KEY NONCLUSTERED ([codig_rh] ASC, [tipofirma] ASC) WITH (DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[PK_sw_tipos_msg]...';

IF OBJECT_ID('PK_sw_tipos_msg') IS NOT NULL 
    ALTER TABLE dbo.[sw_tipos_msg] DROP CONSTRAINT [PK_sw_tipos_msg]


	ALTER TABLE [dbo].[sw_tipos_msg]
    ADD CONSTRAINT [PK_sw_tipos_msg] PRIMARY KEY NONCLUSTERED ([cod_tipo] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_users_srm]...';

IF OBJECT_ID('PK_sw_users_srm') IS NOT NULL 
    ALTER TABLE dbo.[sw_users_srm] DROP CONSTRAINT [PK_sw_users_srm]


	ALTER TABLE [dbo].[sw_users_srm]
    ADD CONSTRAINT [PK_sw_users_srm] PRIMARY KEY NONCLUSTERED ([rut_user] ASC) WITH (DATA_COMPRESSION = PAGE);



PRINT N'Creating [dbo].[PK_sw_users_swift]...';

IF OBJECT_ID('PK_sw_users_swift') IS NOT NULL 
    ALTER TABLE dbo.[sw_users_swift] DROP CONSTRAINT [PK_sw_users_swift]

	ALTER TABLE [dbo].[sw_users_swift]
    ADD CONSTRAINT [PK_sw_users_swift] PRIMARY KEY NONCLUSTERED ([rut_user] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_sw_valor_campos]...';

IF OBJECT_ID('PK_sw_valor_campos') IS NOT NULL 
    ALTER TABLE dbo.[sw_valor_campos] DROP CONSTRAINT [PK_sw_valor_campos]


	ALTER TABLE [dbo].[sw_valor_campos]
    ADD CONSTRAINT [PK_sw_valor_campos] PRIMARY KEY NONCLUSTERED ([tipo_msg] ASC, [tag_campo] ASC, [linea_campo] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_tbl_sw_nets]...';

IF OBJECT_ID('PK_tbl_sw_nets') IS NOT NULL 
    ALTER TABLE dbo.[tbl_sw_nets] DROP CONSTRAINT [PK_tbl_sw_nets]


	ALTER TABLE [dbo].[tbl_sw_nets]
    ADD CONSTRAINT [PK_tbl_sw_nets] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[PK_tbl_sw_nets_det]...';

IF OBJECT_ID('PK_tbl_sw_nets_det') IS NOT NULL 
    ALTER TABLE dbo.[tbl_sw_nets_det] DROP CONSTRAINT [PK_tbl_sw_nets_det]


	ALTER TABLE [dbo].[tbl_sw_nets_det]
    ADD CONSTRAINT [PK_tbl_sw_nets_det] PRIMARY KEY NONCLUSTERED ([id_mensaje] ASC, [linea] ASC) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_log_msg].[sk1_log_msg]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_log_msg')  
	ALTER INDEX [sk1_log_msg]
    ON [dbo].[sw_log_msg] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_msgsend_cambio_estados].[sk1_sw_msgsend_cambio_estados]...';


	ALTER INDEX [sk1_sw_msgsend_cambio_estados]
    ON [dbo].[sw_msgsend_cambio_estados] REBUILD WITH(DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_nop].[sk_msgsend_nop]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk_msgsend_nop')  
	ALTER INDEX [sk_msgsend_nop]
    ON [dbo].[sw_msgsend_nop] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[tbl_sw_nets].[sk1_sw_nets]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk1_sw_nets')  
	ALTER INDEX [sk1_sw_nets]
    ON [dbo].[tbl_sw_nets] REBUILD WITH(DATA_COMPRESSION = PAGE);

PRINT N'Creating [dbo].[sw_bancos].[IX_sw_bancos]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sw_bancos')
	DROP INDEX [IX_sw_bancos]
    ON [dbo].[sw_bancos];


	CREATE UNIQUE NONCLUSTERED INDEX [IX_sw_bancos]
    ON [dbo].[sw_bancos]([branch] ASC, [cod_banco] ASC)
    INCLUDE([nombre_banco], [ciudad_banco], [pais_banco], [oficina_banco]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_campos_msg].[IX_sw_campos_msg_formato]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'IX_sw_campos_msg_formato')
	DROP INDEX [IX_sw_campos_msg_formato]
    ON [dbo].[sw_campos_msg];


	CREATE NONCLUSTERED INDEX [IX_sw_campos_msg_formato]
    ON [dbo].[sw_campos_msg]([formato_campo] ASC)
    INCLUDE([largo_campo], [nombre_campo]) WITH (DATA_COMPRESSION = PAGE);


PRINT N'Creating [dbo].[sw_msgsend_cmx_dia_v]...';



SET ANSI_NULLS ON;

SET QUOTED_IDENTIFIER OFF;




IF OBJECT_ID('sw_msgsend_cmx_dia_v', 'V') IS NOT NULL 
    DROP VIEW dbo.sw_msgsend_cmx_dia_v 

IF OBJECT_ID('sw_msgsend_cmx_dia_v', 'V') IS NULL 
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE view sw_msgsend_cmx_dia_v AS   
	select  
	id_mensaje,
	sesion    ,
	secuencia ,
	casilla   ,
	unidad    ,
	tipo_msg  ,
	prioridad ,
	fecha_ingreso,
	cod_banco_rec,
	branch_rec,
	cod_banco_em,
	branch_em,
	cod_moneda,
	monto,
	referencia,
	desc_msg,
	desc_unidad,
	nomb_banco,
	ciud_banco,
	pais_banco,
	fecha_envio
	from dbo.sw_msgsend_cmx_dia'
END

SET ANSI_NULLS, QUOTED_IDENTIFIER ON;

PRINT N'Creating [dbo].[tbl_sw_nets_det_v]...';



SET ANSI_NULLS ON;

SET QUOTED_IDENTIFIER OFF;



IF OBJECT_ID('tbl_sw_nets_det_v', 'V') IS NOT NULL 
    DROP VIEW dbo.tbl_sw_nets_det_v 

IF OBJECT_ID('tbl_sw_nets_det_v', 'V') IS NULL 
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE view tbl_sw_nets_det_v AS   
	select  
	detalle
	from dbo.tbl_sw_nets_det'
END

PRINT 'Table [dbo].[tipoder]'

IF OBJECT_ID (N'[dbo].[tipoder]', N'U') IS NULL 
BEGIN
	CREATE TABLE [dbo].[tipoder](
		[tabla_codig] [varchar](10) NOT NULL,
		[tabla_des] [varchar](100) NOT NULL,
	 CONSTRAINT [tipoder_pk] PRIMARY KEY CLUSTERED 
	(
		[tabla_codig] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

PRINT 'Table [dbo].[rh_trabajador]'

IF OBJECT_ID (N'[dbo].[rh_trabajador]', N'U') IS NULL 
BEGIN
	CREATE TABLE [dbo].[rh_trabajador](
		[num_rut] [int] NOT NULL,
		[rut_dve] [char](1) NOT NULL,
		[apell_paterno] [char](20) NOT NULL,
		[apell_materno] [char](20) NULL,
		[nombres] [char](20) NOT NULL,
		[fun_atributo] [char](10) NULL,
		[cod_pago] [char](10) NOT NULL,
	 CONSTRAINT [rh_trabajador_pk] PRIMARY KEY CLUSTERED 
	(
		[num_rut] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

PRINT N'Creating [dbo].[rh_trabajador].[rh_trabajador_idx1]'

IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'rh_trabajador_idx1')
	DROP INDEX [rh_trabajador_idx1] ON [dbo].[rh_trabajador];


	CREATE NONCLUSTERED INDEX [rh_trabajador_idx1] ON [dbo].[rh_trabajador]
	(
		[num_rut] ASC,
		[cod_pago] ASC,
		[fun_atributo] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


PRINT N'Creating [dbo].[sw_mensajes].[sk6_mensajes]'


IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name = 'sk6_mensajes')
	DROP INDEX [sk6_mensajes] ON [dbo].[sw_mensajes];

CREATE NONCLUSTERED INDEX [sk6_mensajes] ON [dbo].[sw_mensajes]
(
[send_recv] ASC,
[fecha_send] ASC,
[casilla] ASC
)
INCLUDE ( [sesion],
[secuencia],
[tipo_msg],
[prioridad],
[estado_msg],
[cod_banco_rec],
[branch_rec],
[cod_banco_em],
[branch_em],
[cod_moneda],
[monto],
[referencia],
[beneficiario],
[total_imp],
[comentario]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_version_tablas_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].proc_sw_version_tablas_MS


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_version_tablas_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[proc_sw_version_tablas_MS] 
AS
begin
/************************************************************************/
	/*replace-begin*/select ''1.27.0''/*replace-end*/
end'
END

fin_script:
	print 'fin script Script_Swift_tablas.sql'