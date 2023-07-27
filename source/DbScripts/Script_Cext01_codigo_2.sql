USE [cext01];

BEGIN TRY

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_version_codigo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sce_version_codigo_MS]...';
	DROP PROCEDURE [dbo].proc_sce_version_codigo_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sce_version_codigo_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sce_version_codigo_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[proc_sce_version_codigo_MS] AS
	begin
		/*replace-begin*/select ''1.50.2''/*replace-end*/ + ''.2''
	end'
END
----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_d01' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_swf_pendientes_d01]...';
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_d01]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_d01' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_swf_pendientes_d01]...';
	EXEC dbo.sp_executesql @statement = N'create procedure pro_sce_swf_pendientes_d01 
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(255)
as
	delete from sce_swf_pendientes
	where  ctecct = @ctecct and codesp = @codesp and archivo = @archivo'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_vrng_s01_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_vrng_s01_MS]...';
	DROP PROCEDURE [dbo].[sce_vrng_s01_MS] 
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_vrng_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_vrng_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_vrng_s01_MS] 
	@codcct		CHAR(3),
	@codpro		CHAR(2),
	@codesp		CHAR(2) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

	   if @codpro = ''03''
	   begin
		  SELECT   max(id_cobranz)
		  FROM    dbo.sce_col
		  WHERE   cent_costo = @codcct and
		  id_product = @codpro and
		  id_especia = @codesp
		  return
	   end                                    

	   if @codpro = ''05''
	   begin
		  SELECT   max(codope)
		  FROM    dbo.sce_pae
		  WHERE   codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end                               

	   if @codpro = ''06''
	   begin
		  SELECT   max(codope)
		  FROM    dbo.sce_xcob
		  WHERE   codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end                           

	   if @codpro = ''07''
	   begin
		  SELECT   max(codope)
		  FROM    dbo.sce_jcci
		  WHERE   codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end                              
                                         
	   if @codpro = ''08''
	   begin
		  SELECT   max(codope)
		  FROM    dbo.sce_jant
		  WHERE   codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end                              

	   if @codpro = ''09''
	   begin
		  SELECT   max(codope)
		  FROM 	dbo.sce_ycce
		  WHERE 	codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end

	   if @codpro = ''17''
	   begin
		  SELECT   max(codope)
		  FROM    dbo.sce_xret
		  WHERE   codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end 
                            
		if @codpro = ''20''
	   begin
		  SELECT isnull(max(codope), (select numact from sce_rng where codcct = @codcct and codesp = @codesp and codfun = ''CVI''))
		  FROM dbo.sce_cvd
		  WHERE codcct = @codcct
		  and codpro = @codpro 
		  and codesp = @codesp
		  return
	   end

	   if @codpro = ''30''
	   begin
		  SELECT isnull(max(codope), (select numact from sce_rng where codcct = @codcct and codesp = @codesp and codfun = ''FTC''))
		  FROM dbo.sce_cvd
		  WHERE codcct = @codcct
		  and codpro = @codpro 
		  and codesp = @codesp
		  return
	   end 

	   if @codpro = ''23''
	   begin
		  SELECT   isnull(max(codope),''00000'')
		  FROM    dbo.sce_fbc
		  WHERE   codcct = @codcct and
		  codpro = @codpro and
		  codesp = @codesp
		  return
	   end
	END'


END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_rng_u01_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_rng_u01_MS]...';
	DROP PROCEDURE [dbo].[sce_rng_u01_MS] 
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_rng_u01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_rng_u01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_rng_u01_MS] 
		@codcct          VARCHAR(3),
		@codesp          VARCHAR(2),
		@codfun          VARCHAR(3),
		@minNumRequerido numeric(10,0) = NULL
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

		  declare @numact NUMERIC(10,0), @nummax	NUMERIC(10,0), @errorReturn NUMERIC(10,0)
		  set @errorReturn = -1

	BEGIN TRAN



	   if (@minNumRequerido is not null)
		   begin

		   SELECT @numact = numact 
			FROM sce_rng
			where
			   codcct = @codcct and
			   codesp = @codesp and
			   codfun = @codfun and
			   numact < nummax	 and
			   numact > 0

			   if(@numact < @minNumRequerido)
					select @numact = @minNumRequerido + 1 -- sumo 1 mas de lo requerido para dar margen para la concurrencia


				update dbo.sce_rng set numact = @numact + 1  where
				   codcct = @codcct and
				   codesp = @codesp and
				   codfun = @codfun and
				   numact < nummax	 and
				   numact > 0

		   end
		else
		begin

		 update dbo.sce_rng set numact = numact+1  where
		   codcct = @codcct and
		   codesp = @codesp and
		   codfun = @codfun and
		   numact < nummax	 and
		   numact > 0

		end
	
  

	   if (@@error <> 0)
	   begin
		  ROLLBACK 
		  Select   @errorReturn as valor, ''Error al grabar datos en Sce_Rng'' as mensaje
		  return -1
	   end

	   select   @numact = numact,
			@nummax = nummax
	   from 	dbo.sce_rng   WITH (holdlock)
	   where 	codcct = @codcct and
	   codesp = @codesp and
	   codfun = @codfun


	   if (@numact > @nummax and @numact > 0)
	   begin
		  ROLLBACK 
		  Select   @errorReturn as valor, ''Error al grabar datos en Sce_Rng'' as mensaje
		  return -1
	   end
		

	   COMMIT TRAN
	   Select   @numact as valor,''Éxito'' as mensaje

	   return @numact
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_i01_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_swf_pendientes_i01_MS]...';
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_i01_MS] 
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_i01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_swf_pendientes_i01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_swf_pendientes_i01_MS] 
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(255),
	@rutAis varchar(15),
	@sistema varchar(10),
	@fecha datetime,
	@tipo varchar(10),
	@moneda varchar(10),
	@monto decimal(18,2),
	@referencia varchar(16),
	@msjSwift varchar(max),
	@esPlantilla bit

AS

IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.sce_swf_pendientes WHERE ctecct = @ctecct AND codesp = @codesp AND archivo = @archivo)

   BEGIN 
      insert into sce_swf_pendientes
			values 
			(
				@ctecct,
				@codesp ,
				@archivo,
				@rutAis ,
				@sistema ,
				@fecha ,
				@tipo ,
				@moneda ,
				@monto ,
				@referencia,
				@msjSwift,
				@esPlantilla
			)
   END 
ELSE 
   BEGIN 
   UPDATE  sce_swf_pendientes 
   SET			rutAis =  @rutAis,
				sistema = @sistema ,
				fecha = @fecha,
				tipo = @tipo ,
				moneda = @moneda ,
				monto = @monto ,
				referencia = @referencia,
				msjSwift = @msjSwift,
				esPlantilla = @esPlantilla  
   WHERE ctecct = @ctecct AND codesp = @codesp AND archivo = @archivo
   END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_s01_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_swf_pendientes_s01_MS]...';
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_s01_MS]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_swf_pendientes_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_swf_pendientes_s01_MS]
	@ctecct char(3),
	@codesp char(2)
as
	select 
		ctecct,
		codesp,
		archivo,
		rutAis,
		sistema,
		fecha,
		tipo,
		moneda,
		monto,
		referencia,
		msjSwift,
		esPlantilla
	from sce_swf_pendientes
	where ctecct = @ctecct and (codesp = @codesp OR esPlantilla = 1)
	order by esPlantilla desc, fecha asc'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_s02_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_swf_pendientes_s02_MS]...';
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_s02_MS]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_swf_pendientes_s02_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_swf_pendientes_s02_MS]
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(255)
as
	select 
		ctecct,
		codesp,
		archivo,
		rutAis,
		sistema,
		fecha,
		tipo,
		moneda,
		monto,
		referencia,
		msjSwift,
		esPlantilla
	from sce_swf_pendientes
	where ctecct = @ctecct and codesp = @codesp and archivo = @archivo'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sel_TBLSceTabcomex_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sel_TBLSceTabcomex_MS]...';
	DROP PROCEDURE [dbo].[proc_sel_TBLSceTabcomex_MS]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sel_TBLSceTabcomex_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [proc_sel_TBLSceTabcomex_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sel_TBLSceTabcomex_MS]
	@parametro NVARCHAR(50)
	AS
	BEGIN
		SELECT tc_vhcar_valor FROM tbl_sce_tabcomex_vchar WHERE tc_vchar_metadata = @parametro
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sel_validaOperacionSiTieneInyeccion_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sel_validaOperacionSiTieneInyeccion_MS]...';
	DROP PROCEDURE [dbo].[proc_sel_validaOperacionSiTieneInyeccion_MS]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sel_validaOperacionSiTieneInyeccion_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [proc_sel_validaOperacionSiTieneInyeccion_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sel_validaOperacionSiTieneInyeccion_MS] 
		@codcct CHAR(3),
		@codpro CHAR(2),
		@codesp CHAR(2),
		@codofi CHAR(3),
		@codope CHAR(5)	
	AS
	BEGIN
		--DECLARE @codcct CHAR(3) SET @codcct = ''753''
		--DECLARE @codpro CHAR(2) SET @codpro = ''30''
		--DECLARE @codesp CHAR(2) SET @codesp = ''29''
		--DECLARE @codofi CHAR(3) SET @codofi = ''000''
		--DECLARE @codope CHAR(5) SET @codope = ''53761''
		-----------------------------------------------------------------------------------------------
		DECLARE @tieneInyeccion BIT SET @tieneInyeccion = 0;
		-- Validamos si la Operación tiene una inyeccion realizada en la contabilidad
		IF	(SELECT 
				SUM(conta.enlinea)
			FROM 
				sce_mcd conta 
			WHERE	
				conta.codcct = @codcct
				AND conta.codpro = @codpro	AND conta.codesp = @codesp
				AND conta.codofi = @codofi	AND conta.codope = @codope
				) > 0
		BEGIN
			SET @tieneInyeccion = 1
		END
		SELECT @tieneInyeccion as [TieneInyeccion]
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sgt_mnd_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [sgt_mnd_s03_MS]
END
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_mnd_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [sgt_mnd_s03_MS] 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:04:04 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
   
   select   mnd_mndcod,
		mnd_mndcbc,
		mnd_mndsbf,
		mnd_mndnom,
		mnd_mndnmc,
		mnd_mndswf,
		mnd_mndpai,
		mnd_mndfiv,
		mnd_mndftv,
		mnd_mndina
   from 	dbo.sgt_mnd 
   return
   return
END
'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_vvi_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [sce_vvi_s02_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vvi_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_vvi_s02_MS] @codcct CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5) 
AS
BEGIN
	DECLARE @nro INT SET @nro = 0
	SELECT
		@nro = MAX(nrocor)
	FROM dbo.sce_vvi
	WHERE codcct = @codcct
	AND codpro = @codpro
	AND codesp = @codesp
	AND codofi = @codofi
	AND codope = @codope
	IF @nro > -1
	BEGIN
		SELECT @nro
	END
	ELSE
	BEGIN
		SELECT 0
	END
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_prty_i06_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_prty_i06_MS]...'; 
	DROP procedure pro_sce_prty_i06_MS;
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_i06_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [pro_sce_prty_i06_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_prty_i06_MS] 
		--TABLA sce_prty
		@id_party       CHAR(12),
		@borrado        BIT	,
		@tipo_party     TINYINT	,
		@flag           SMALLINT,
		@clasificac     TINYINT ,
		@tiene_rut      BIT	,
		@rut            CHAR(10),
		@crea_costo     CHAR(3),
		@crea_user      CHAR(2),
		@multiple       BIT	,
		@cod_ofieje     CHAR(3),
		@cod_eject      CHAR(3),
		@cod_acteco     CHAR(5),
		@clase_ries     CHAR(2),
		@cod_bco        SMALLINT,
		@tasa_libor     BIT	,
		@tasa_prime     BIT	,
		@spread         FLOAT	,
		@swift          CHAR(12),
		@plaza_alad     NUMERIC(5,0),
		@ejec_corre     CHAR(40),
		@flagins        SMALLINT,
		@insgen_imp     NUMERIC(6,0),
		@insgen_exp     NUMERIC(6,0),
		@insgen_ser     NUMERIC(6,0),
		@inscob_imp     NUMERIC(6,0),
		@inscob_exp     NUMERIC(6,0),
		@inscre_imp     NUMERIC(6,0),
		@inscre_exp     NUMERIC(6,0),
		--TABLA sce_rsa
		@id_nombre      NUMERIC(2,0),
		@razon_soci     VARCHAR(60),
		@contacto       VARCHAR(40),
		--TABLA sce_dad 
		@id_dir         NUMERIC(2,0),
		@direccion      VARCHAR(60),
		@comuna         VARCHAR(40),
		@estado         VARCHAR(25),
		@ciudad         VARCHAR(30),
		@pais           VARCHAR(30),
		@telefono       CHAR(10),
		@fax            CHAR(10),
		@telex          CHAR(15),
		@envio_sce      NUMERIC(2,0),
		@recibe_sce     NUMERIC(2,0),
		@cod_postal     VARCHAR(20),
		@cas_postal     CHAR(20),
		@cas_banco      CHAR(20),
		@email          VARCHAR(500),
		@fecha          Datetime,
		@cuenta         CHAR(11),
		@moneda         NUMERIC(3,0),
		--
		@ls_retorno      CHAR(3)  OUTPUT,     
		@ls_mensaje      CHAR(250) OUTPUT 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 
   
	   Declare @lc_codcomuna NUMERIC(4,0)
	   Declare @lc_codpais   NUMERIC(3,0)
	   Declare @lc_extranjera BIT
	Begin Tran
	   If Not Exists(SELECT TOP 1 1 From dbo.sce_prty Where id_party = @id_party)
	   Begin
		  Insert Into dbo.sce_prty(id_party  , borrado    , tipo_party , flag       , clasificac , tiene_rut  ,  rut ,
					crea_costo, crea_user  , mod_costo  , mod_user   , multiple   , cod_ofieje , cod_eject ,
					cod_acteco, clase_ries , cod_bco    , tasa_libor , tasa_prime , spread     ,swift,
					plaza_alad, ejec_corre , flagins    , insgen_imp , insgen_exp , insgen_ser , inscob_imp ,
					inscob_exp, inscre_imp , inscre_exp , fecing     , fecact)
			values(@id_party  , @borrado     , @tipo_party , @flag       , @clasificac , @tiene_rut , @rut,
					@crea_costo, @crea_user   , @crea_costo , @crea_user  , @multiple   , @cod_ofieje, @cod_eject  ,
					@cod_acteco, @clase_ries  , @cod_bco    , @tasa_libor , @tasa_prime , @spread    , @swift      ,
					@plaza_alad, @ejec_corre  , @flagins    , @insgen_imp , @insgen_exp , @insgen_ser, @inscob_imp ,
					@inscob_exp , @inscre_imp , @inscre_exp , @fecha      , @fecha)
           
		  If @@error != 0
		  Begin
			 Rollback 
			 Select   @ls_retorno = ''E01''
			 Select   @ls_mensaje = ''pro_sce_prty_i06:[E01]-Error al ingresar registro en tabla sce_prty.''
			 Return
		  End
	   End             

	   If Not Exists(SELECT TOP 1 1 From dbo.sce_rsa Where id_party = @id_party)
	   Begin
		  Insert Into dbo.sce_rsa(id_party  , id_nombre   ,borrado   , razon_soci  , nom_fantas ,contacto, sortkey , crea_costo, crea_user)
			values(@id_party  , @id_nombre ,@borrado   , @razon_soci , @razon_soci,@contacto, Upper(@razon_soci) , @crea_costo, @crea_user)
           
		  If @@error != 0
		  Begin
			 Rollback 
			 Select   @ls_retorno = ''E02''
			 Select   @ls_mensaje = ''pro_sce_prty_i06:[E02]-Error al ingresar registro en tabla sce_rsa.''
			 Return
		  End
	   End      
               
	   If Not Exists(SELECT TOP 1 1 From dbo.sce_dad Where id_party = @id_party)
	   Begin
			--CODIGO DE LA COMUNA
		  select   Upper(@comuna)
		  If Upper(@comuna) = ''SANTIAGO''
			 Select   @lc_codcomuna = 320
		  Select   @lc_codcomuna = loc_loccod
		  From   dbo.sgt_loc
		  Where  loc_locnom = Upper(@comuna)
		  if @lc_codcomuna IS NULL  
			 Select   @lc_codcomuna = 9999
        
			--CODIGO DEL PAIS
		  Select   @lc_codpais = pai_paicod
		  From   dbo.sgt_pai
		  Where  pai_painom = Upper(@pais)
		  if @lc_codpais IS NULL  
			 Select   @lc_codpais = 999
		  Insert Into dbo.sce_dad(id_party  , id_dir     ,direccion   , comuna    , cod_comuna   ,estado  , ciudad    , pais       , cod_pais  ,
					telefono  , fax        ,telex       , cas_postal, cas_banco    ,email   , envio_sce , recibe_sce ,crea_costo, crea_user,borrado,
					cod_postal)
			values(@id_party  , @id_dir    ,@direccion   , @comuna    ,@lc_codcomuna, @estado, @ciudad   , @pais      ,@lc_codpais,
				   @telefono  , @fax       ,@telex       , @cas_postal,@cas_banco   , @email , @envio_sce , @recibe_sce,@crea_costo, @crea_user,@borrado,
				   @cod_postal)
           
		  If @@error != 0
		  Begin
			 Rollback 
			 Select   @ls_retorno = ''E03''
			 Select   @ls_mensaje = ''pro_sce_prty_i06:[E03]-Error al ingresar registro en tabla sce_dad.''
			 Return
		  End
	   End      
        
	   If Not Exists(SELECT TOP 1 1 From dbo.sce_ctas Where id_party = @id_party And cuenta = @cuenta)
		  --extranjera  ME=1; MN=0  
		  if @moneda = 1  
			 Select   @lc_extranjera = 0
	   If @moneda <> 1 
		  Select   @lc_extranjera = 1
	   Begin
		  Insert Into dbo.sce_ctas(id_party  , secuencia   ,borrado   , activabco  , activace ,extranjera    , moneda , cuenta)
			values(@id_party  , 0           ,@borrado  , 0          , 1       , @lc_extranjera, @moneda ,@cuenta)
           
		  If @@error != 0
		  Begin
			 Rollback 
			 Select   @ls_retorno = ''E04''
			 Select   @ls_mensaje = ''pro_sce_prty_i06:[E04]-Error al ingresar registro en tabla sce_ctas.''
			 Return
		  End
	   End      

	   Commit Tran
	   Select   @ls_retorno = ''E00''
	   Select   @ls_mensaje = ''El ingreso de participante termina exitosamente''
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_dad_i01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_dad_i01_MS]...'; 
	DROP procedure sce_dad_i01_MS;
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_dad_i01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_dad_i01_MS]
		@id_party		CHAR(12),
		@id_dir			NUMERIC(2,0),
		@borrado		BIT,
		@direccion		VARCHAR(60),
		@comuna			VARCHAR(40),
		@cod_comuna		NUMERIC(4,0),
		@cod_postal		VARCHAR(20),
		@estado			VARCHAR(25),
		@ciudad			VARCHAR(30),
		@pais			VARCHAR(30),
		@cod_pais		NUMERIC(3,0),
		@telefono		CHAR(10),
		@fax			CHAR(10),
		@telex			CHAR(15),
		@envio_sce		NUMERIC(1,0),
		@recibe_sce		NUMERIC(1,0),
		@cas_postal		CHAR(20),
		@cas_banco		CHAR(20),
		@crea_costo		VARCHAR(3),
		@crea_user		VARCHAR(2),
		@email			VARCHAR(500)
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON
	
		INSERT INTO 
		sce_dad(id_party,
				id_dir,
				borrado,
				direccion,
				comuna,
				cod_comuna,
				cod_postal,
				estado,
				ciudad,
				pais,
				cod_pais,
				telefono,
				fax,
				telex,
				envio_sce,
				recibe_sce,
				cas_postal,
				cas_banco,
				crea_costo,
				crea_user, 
				email) 
		 VALUES (@id_party,
				@id_dir,
				@borrado,
				@direccion,
				@comuna,
				@cod_comuna,
				@cod_postal,
				@estado,
				@ciudad,
				@pais,
				@cod_pais,
				@telefono,
				@fax,
				@telex,
				@envio_sce,
				@recibe_sce,
				@cas_postal,
				@cas_banco,
				@crea_costo,
				@crea_user, 
				@email)
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_dad_u02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_dad_u02_MS]...'; 
	DROP procedure sce_dad_u02_MS;
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_dad_u02_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_dad_u02_MS]
		@id_party		CHAR(12),
		@id_dir			NUMERIC(2,0),
		@borrado		BIT,
		@direccion		VARCHAR(60),
		@comuna			VARCHAR(40),
		@cod_comuna		NUMERIC(4,0),
		@cod_postal		VARCHAR(20),
		@estado			VARCHAR(25),
		@ciudad			VARCHAR(30),
		@pais			VARCHAR(30),
		@cod_pais		NUMERIC(3,0),
		@telefono		CHAR(10),
		@fax			CHAR(10),
		@telex			CHAR(15),
		@envio_sce		NUMERIC(1,0),
		@recibe_sce		NUMERIC(1,0),
		@cas_postal		CHAR(20),
		@cas_banco		CHAR(20),
		@email			VARCHAR(500)
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON

		UPDATE sce_dad 
		SET 
			id_party	= @id_party,
			id_dir		= @id_dir,
			borrado		= @borrado,
			direccion	= @direccion,
			comuna		= @comuna,
			cod_comuna	= @cod_comuna,
			cod_postal	= @cod_postal,
			estado		= @estado, 
			ciudad		= @ciudad,
			pais		= @pais,
			cod_pais	= @cod_pais,
			telefono	= @telefono,
			fax			= @fax,
			telex		= @telex,
			envio_sce	= @envio_sce,
			recibe_sce	= @recibe_sce,
			cas_postal	= @cas_postal,
			cas_banco	= @cas_banco,
			email		= @email
		WHERE 
			id_party	= @id_party AND 
			id_dir		= @id_dir
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_anu_u03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_anu_u03_MS]...'; 
	DROP procedure sce_anu_u03_MS;
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_anu_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_anu_u03_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_anu_u03_MS]
		@codcct         CHAR(3),
		@codpro         CHAR(2),
		@codesp         CHAR(2),
		@codofi         CHAR(3),
		@codope         CHAR(5) 
	AS
	BEGIN
		/*	
		Historial:
								 Migración desde Sybase (AKZIO)
			  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
		*/
		SET NOCOUNT ON 

   

		declare 
		@nrocan         TINYINT,@codanu         CHAR(6),@nrorpt         INT,@fecmov         datetime,
		@mtocan         NUMERIC(15,2),@mtodis         NUMERIC(15,2),
		@totcan         TINYINT,@estado         TINYINT,@numdec         CHAR(7),
		@fecdec         datetime,@codadn         INT,@valret1c       NUMERIC(15,2),
		@valcom1c       NUMERIC(15,2),@valgas1c       NUMERIC(15,2),
		@valliq1c       NUMERIC(15,2),@valfle1c       NUMERIC(15,2),
		@valseg1c       NUMERIC(15,2),@valret2c       NUMERIC(15,2),@valcom2c       NUMERIC(15,2),
		@valgas2c       NUMERIC(15,2),@valliq2c       NUMERIC(15,2),
		@valfle2c       NUMERIC(15,2),@valseg2c       NUMERIC(15,2) 
		/************************************/
		/*Real Systems Ltda *****************/
		/*Se declara variable para eliminar**/
		/*registro en tabla fts**************/
		   declare @lc_contract NUMERIC(10,0)
		/****FIN*****************************/


		declare cursor_xdec cursor for 
		select  
			numdec,
			fecdec,
			codadn,
			valret1c,
			valcom1c,
			valgas1c,
			valliq1c,
			valfle1c,
			valseg1c,
			valret2c,
			valcom2c,
			valgas2c,
			valliq2c,
			valfle2c,
			valseg2c
		from dbo.sce_xdep where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope

		BEGIN TRAN
	
		-- Si no existe la Operación => Cancelar.-
		if not exists(SELECT TOP 1 1 from dbo.sce_cvd where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope)
		begin
			ROLLBACK 
			Select   -1, ''No existe la Operacion de Compra-Venta.''
			return
		end
	
		-- Se marcan anulados los Cheques.-
		update dbo.sce_chq set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
		
		-- Se marcan anulados los V. Vistas.-
		update dbo.sce_vvi set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope

		-- POLY Se marcan como "anulados" los SWIFT generados en tabla sce_mts
		update dbo.sce_mts set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope                                           
		
		-- Se marcan anulados los Swifts.-
		update dbo.sce_swf set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
		
		-- Se marca anulado el encabezado del Reporte Contable.-
		update dbo.sce_mch set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
	       
		-- Se marca anulado el detalle del Reporte Contable.-
		update dbo.sce_mcd set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope

		-- Se marcan anuladas las Planillas Visibles Export.-
		update dbo.sce_xplv set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
	       
		-- Se marcan anuladas las Planillas Invisibles.-
		update dbo.sce_pli set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope


		-- Se marcan anuladas las Planillas Anuladas.-
		update dbo.sce_xanu set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope                        
	
		-- si viene de speedtransfer se marca speed como anulado.
		if exists(SELECT TOP 1 1 from tbl_sce_from_ESB2 where operacion = @codcct + @codpro + @codesp + @codofi + @codope)
		begin
			update tbl_sce_from_ESB2
			set estado = 3 -- anulado
			where operacion = @codcct + @codpro + @codesp + @codofi + @codope
		end 

		-- Cursor que identifica los rebajes de xdec.-
	
	
		-- Se abre el cursor y se especifica las variables.-
		open cursor_xdec
		fetch cursor_xdec into
		@numdec,@fecdec,@codadn,@valret1c,@valcom1c,@valgas1c,@valliq1c,@valfle1c,
		@valseg1c,@valret2c,@valcom2c,@valgas2c,@valliq2c,@valfle2c,@valseg2c
	       
		-- Se recorre el cursor para devolver montos.-
		while @@FETCH_STATUS != -1
		begin
	  
			-- Se verifica error en el cursor.-
			if (@@FETCH_STATUS = -2)
			begin
				ROLLBACK 
				Select   -1, ''Error al recorrer el cursor.''
				return
			end
	  
			-- Se devuelve el monto del Exp-1 a la Declaracion.-
			update dbo.sce_xdec set valret1c = valret1c -@valret1c,valcom1c = valcom1c -@valcom1c,valgas1c = valgas1c -@valgas1c,
			valliq1c = valliq1c -@valliq1c,valfle1c = valfle1c -@valfle1c,
			valseg1c = valseg1c -@valseg1c  where
			numdec = @numdec and
			fecdec = @fecdec and
			codadn = @codadn

			-- Se devuelve el monto del Exp-2 a la Declaracion.-
			update dbo.sce_xdec set valret2c = valret2c -@valret2c,valcom2c = valcom2c -@valcom2c,valgas2c = valgas2c -@valgas2c,
			valliq2c = valliq2c -@valliq2c,valfle2c = valfle2c -@valfle2c,
			valseg2c = valseg2c -@valseg2c  where
			numdec = @numdec and
			fecdec = @fecdec and
			codadn = @codadn

			-- Se accesa el proximo registro del cursor
			fetch cursor_xdec into
			@numdec,@fecdec,@codadn,@valret1c,@valcom1c,@valgas1c,@valliq1c,@valfle1c,
			@valseg1c,@valret2c,@valcom2c,@valgas2c,@valliq2c,@valfle2c,@valseg2c
		end

		-- Se cierra el cursor.-
		close cursor_xdec
		deallocate cursor_xdec

		-- Se marcan el nuevo estado de la Compra/Venta (Anulado).-
		update dbo.sce_cvd set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope

		-- Se verifica la existencia de errores.-
		if (@@error <> 0)
		begin
			ROLLBACK 
			Select   -1, ''Error al Anular una Compra-Venta de Divisas.''
			return
		end
    
		-- Se comenta lo siguiente ya que se desea poder reversar una operación anulada, y la información de tbl_sce_fts, tbl_sce_relacion_ft y tbl_sce_tranope_ft es necesaria para la reversa
	/*
	   if @codpro = ''30''
	   Begin
		  select   @lc_contract = contract_reference
		  from   dbo.tbl_sce_relacion_ft
		  where  codcct = @codcct
		  and    codpro = @codpro
		  and    codesp = @codesp
		  and    codofi = @codofi
		  and    codope = @codope
		  delete from dbo.tbl_sce_fts
		  where  contract_reference = @lc_contract
		  if (@@error <> 0)
		  begin
			 ROLLBACK 
			 Select   -1, ''Error al Eliminar Registro en tabla FTS''
			 return
		  end
      
		  delete from dbo.tbl_sce_relacion_ft
		  where  codcct = @codcct
		  and    codpro = @codpro
		  and    codesp = @codesp
		  and    codofi = @codofi
		  and    codope = @codope
		  if (@@error <> 0)
		  begin
			 ROLLBACK 
			 Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_relacion_ft''
			 return
		  end
		  delete from dbo.tbl_sce_tranope_ft
		  where  codcct = @codcct
		  and    codpro = @codpro
		  and    codesp = @codesp
		  and    codofi = @codofi
		  and    codope = @codope
		  if (@@error <> 0)
		  begin
			 ROLLBACK 
			 Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_relacion_ft''
			 return
		  end
	  
	   End */
		-- Fin Elimina Registro FTS    
    
	   COMMIT TRAN

	   Select   0, ''Grabacion Exitosa''
	   return

	   return
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_anu_u12_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_anu_u12_MS]...'; 
	DROP procedure sce_anu_u12_MS;
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_anu_u12_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_anu_u12_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_anu_u12_MS]
		@codcct         CHAR(3),
		@codpro         CHAR(2),
		@codesp         CHAR(2),
		@codofi         CHAR(3),
		@codope         CHAR(5) 
	AS
	BEGIN
		/*	
		Historial:
								 Migración desde Sybase (AKZIO)
			  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
		*/
		SET NOCOUNT ON 
   
		declare	
		@numpla		NUMERIC(10,0),@numdec		CHAR(18),@fecdec			SMALLDATETIME,@mtofob		NUMERIC(15,2),
		@mtofle		NUMERIC(15,2),@mtoseg		NUMERIC(15,2),@fobmer		NUMERIC(15,2),
		@flemer		NUMERIC(15,2),@segmer		NUMERIC(15,2),@mtocif		NUMERIC(15,2),
		@pardec		NUMERIC(17,10),@cubcob		NUMERIC(15,2),@cubotr		NUMERIC(15,2),
		@rescub		NUMERIC(15,2),@fecanu		SMALLDATETIME,@indanula		NUMERIC(1,0)
		/************************************/
		/*Real Systems Ltda *****************/
		/*Se declara variable para eliminar**/
		/*registro en tabla fts**************/
		declare @lc_contract NUMERIC(10,0)
		/****FIN*****************************/

		declare cursor_xdec cursor for 
		select	
			numpla,
			numdec,
			fecdec,
			mtofob,
			mtofle,
			mtoseg,
			fobmer,
			flemer,
			segmer,
			mtocif,
			pardec,
			fecha
		from dbo.sce_reb where
		cent_costo = @codcct and
		id_product = @codpro and
		id_especia = @codesp and
		id_empresa = @codofi and
		id_operac  = @codope

		BEGIN TRAN
	
		-- Si no existe la Operaci›n => Cancelar.-
		if not exists(SELECT TOP 1 1 from dbo.sce_cvd where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope)
		begin
			ROLLBACK 
			Select   -1, ''No existe la Operacion de Compra-Venta.''
			return
		end
	
		-- Se marcan anulados los Cheques.-
		update dbo.sce_chq set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
		
		-- Se marcan anulados los V. Vistas.-
		update dbo.sce_vvi set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
		
		-- Se marcan anulados los Swifts.-
		update dbo.sce_swf set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
		
		-- Se marca anulado el encabezado del Reporte Contable.-
		update dbo.sce_mch set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope
	       
		-- Se marca anulado el detalle del Reporte Contable.-
		update dbo.sce_mcd set estado = 9  where
		codcct = @codcct and
		codpro = @codpro and
		codesp = @codesp and
		codofi = @codofi and
		codope = @codope


		-- Se anula tabla ventas visibles import.- sce_vvim
		update dbo.sce_vvim set estado = 9  where
		codcct = @codcct	and
		codpro = @codpro	and
		codesp = @codesp	and
		codofi = @codofi	and
		codope = @codope      


		-- Eliminar sce_grdo
		delete dbo.sce_grdo
		where
		codcct = @codcct        and
		codpro = @codpro        and
		codesp = @codesp        and
		codofi = @codofi        and
		codope = @codope            	


		-- Se marcan anuladas las Planillas Visibles import.-
		update dbo.sce_plan set estado = 9  where
		cent_costo = @codcct and
		id_product = @codpro and
		id_especia = @codesp and
		id_empresa = @codofi and
		id_cobranz = @codope                         


		-- si viene de speedtransfer se marca speed como anulado.
		if exists(SELECT TOP 1 1 from tbl_sce_from_ESB2 where operacion = @codcct + @codpro + @codesp + @codofi + @codope)
		begin
			update tbl_sce_from_ESB2
			set estado = 3 -- anulado
			where operacion = @codcct + @codpro + @codesp + @codofi + @codope
		end 
	
		-- Cursor que identifica los rebajes de xdec.-
	
	
		-- Se abre el cursor y se especifica las variables.-
		open cursor_xdec
		fetch cursor_xdec into
		@numpla,@numdec,@fecdec,@mtofob,@mtofle,@mtoseg,@fobmer,@flemer,@segmer,
		@mtocif,@pardec,@fecanu

	      
		-- Se recorre el cursor para devolver montos.-
		while @@FETCH_STATUS != -1
		begin
	  
			-- Se verifica error en el cursor.-
			if (@@FETCH_STATUS = -2)
			begin
				ROLLBACK 
				Select   -1, ''Error al recorrer el cursor.''
				return
			end

			select @indanula = 0
			
			if exists(SELECT TOP 1 1 from dbo.sce_plan
					  where   num_presen = @numpla and
					  fechaanula = @fecanu and
					  hayanula = 1)
			begin
				select   @indanula = indanula
				from 	dbo.sce_plan
				where   num_presen = @numpla and
				fechaanula = @fecanu and
				hayanula = 1

				if (@indanula = 1)
				begin
					update dbo.sce_plan set hayanula   = 0,indanula   = 0,vencanula  = CONVERT(smalldatetime,0) ,fechaanula = CONVERT(smalldatetime,0),
					paranula   = 0,totalanula = 0,observ     = ''Ope. ''+cent_costo+id_product+id_especia+id_empresa+id_cobranz  where   num_presen = @numpla and
					fechaanula = @fecanu and
					hayanula = 1
				end
				if (@indanula = 4)
				begin
					update dbo.sce_plan set hayanula   = 0,indanula   = 3,vencanula  = CONVERT(smalldatetime,0) ,fechaanula = CONVERT(smalldatetime,0),
					paranula   = 0,totalanula = 0,observ     = ''Ope. ''+cent_costo+id_product+id_especia+id_empresa+id_cobranz+'' Planilla de transferencia''  where   num_presen = @numpla and
					fechaanula = @fecanu and
					hayanula = 1
				end
			end
			if @numdec <> '''' and @pardec > 0
				begin
					if @indanula <> 1 and @indanula <> 4
						begin
                                                       
	  					-- Se devuelve el monto de la declaraci¢n.-                     
						update dbo.sce_dec set cubfob = cubfob -(@mtofob/@pardec),cubfle = cubfle -(@mtofle/@pardec),
						cubseg = cubseg -(@mtoseg/@pardec),cubmer =(cubmer -((@fobmer+@flemer+@segmer)/@pardec)),
						cubcif = cubcif -(@mtocif/@pardec),
						relfob =(relfob -((@mtofob -@fobmer)/@pardec)),relfle =(relfle -((@mtofle -@flemer)/@pardec)),
						relseg =(relseg -((@mtoseg -@segmer)/@pardec)),
						relmer =(relmer -((@fobmer+@flemer+@segmer)/@pardec)),
						relcif =(relcif -(@mtocif/@pardec)),disfob = orifob -(relfob -((@mtofob -@fobmer)/@pardec)),
						disfle = orifle -(relfle -((@mtofle -@flemer)/@pardec)),
						disseg = oriseg -(relseg -((@mtoseg -@segmer)/@pardec)),
						discif = oricif -(relcif -(@mtocif/@pardec)),dismer = mtomer -(relmer -((@fobmer+@flemer+@segmer)/@pardec))  where
						numdec = @numdec and
						fecdec = convert(datetime,@fecdec)
						end
					else
						begin
							update dbo.sce_dec set relfob = relfob+round(@mtofob/@pardec,2),cubfob = cubfob+round(@mtofob/@pardec,2),
							disfob = disfob -round(@mtofob/@pardec,2),relfle = relfle+round(@mtofle/@pardec,2),
							cubfle = cubfle+round(@mtofle/@pardec,2),
							disfle = disfle -round(@mtofle/@pardec,2),relseg = relseg+round(@mtoseg/@pardec,2),
							cubseg = cubseg+round(@mtoseg/@pardec,2),disseg = disseg -round(@mtoseg/@pardec,2),
							relcif = relcif+round(@mtocif/@pardec,2),
							cubcif = cubcif+round(@mtocif/@pardec,2),discif = discif -round(@mtocif/@pardec,2)  where   numdec = @numdec and
							fecdec = convert(datetime,@fecdec)
						 end
				end                                          

			  -- Se accesa el proximo registro del cursor
			  fetch cursor_xdec into
			  @numpla,@numdec,@fecdec,@mtofob,@mtofle,@mtoseg,@fobmer,@flemer,@segmer,
			  @mtocif,@pardec,@fecanu
	   end

		-- Se cierra el cursor.-
	   close cursor_xdec
	   deallocate cursor_xdec

		-- Se marcan el nuevo estado de la Compra/Venta (Anulado).-
	   update dbo.sce_cvd set estado = 9  where
	   codcct = @codcct and
	   codpro = @codpro and
	   codesp = @codesp and
	   codofi = @codofi and
	   codope = @codope

		-- Se verifica la existencia de errores.-
	   if (@@error <> 0)
	   begin
		  ROLLBACK 
		  Select   -1, ''Error al Anular una Compra-Venta de Divisas.''
		  return
	   end
   
		   -- Se comenta lo siguiente ya que se desea poder reversar una operación anulada, y la información de tbl_sce_fts, tbl_sce_relacion_ft y tbl_sce_tranope_ft es necesaria para la reversa
	/*
	   if @codpro = ''30''
	   Begin
		  select   @lc_contract = contract_reference
		  from   dbo.tbl_sce_relacion_ft
		  where  codcct = @codcct
		  and    codpro = @codpro
		  and    codesp = @codesp
		  and    codofi = @codofi
		  and    codope = @codope
		  delete from dbo.tbl_sce_fts
		  where  contract_reference = @lc_contract
		  if (@@error <> 0)
		  begin
			 ROLLBACK 
			 Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_fts''
			 return
		  end
      
		  delete from dbo.tbl_sce_relacion_ft
		  where  codcct = @codcct
		  and    codpro = @codpro
		  and    codesp = @codesp
		  and    codofi = @codofi
		  and    codope = @codope
		  if (@@error <> 0)
		  begin
			 ROLLBACK 
			 Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_relacion_ft''
			 return
		  end
		  delete from dbo.tbl_sce_tranope_ft
		  where  codcct = @codcct
		  and    codpro = @codpro
		  and    codesp = @codesp
		  and    codofi = @codofi
		  and    codope = @codope
		  if (@@error <> 0)
		  begin
			 ROLLBACK 
			 Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_tranope_ft''
			 return
		  end
	  
	   End */
		-- Fin Elimina Registro FTS    

	   COMMIT TRAN
	   Select   0, ''Grabacion Exitosa''
	   return

	   return
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_xplv_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xplv_MS]...';
	DROP PROCEDURE [dbo].[sce_xplv_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_xplv_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_xplv_MS] 
	@numPre CHAR(7),
	@CUI CHAR(3),
	@fecDesde DATETIME,
	@fecHasta DATETIME
AS
BEGIN

	set @fecDesde = ISNULL(@fecDesde, '''');
	set @fecHasta = ISNULL(@fecHasta, '''');

	SET FMTONLY OFF
	CREATE TABLE #planillas
	(
		Operacion VARCHAR(20),
		Tipo VARCHAR(10),
		FechaIngreso DATETIME
	)

	-- Planillas Visibles
	INSERT INTO #planillas
	SELECT (codcct + ''-'' + codpro + ''-'' + codesp + ''-'' + codofi + ''-'' + codope) AS Operacion, ''VISIBLE'' AS Tipo, fecing AS FechaIngreso
	FROM dbo.sce_xplv WITH(NOLOCK)
	WHERE numpre = @numPre
	AND (@CUI = '''' OR codcct = @CUI)
	AND ((@fecDesde = '''' AND @fecHasta = '''') OR fecing >= @fecDesde and fecing <= @fecHasta)
		
	-- Planillas Anulación
	INSERT INTO #planillas
	SELECT (codcct + ''-'' + codpro + ''-'' + codesp + ''-'' + codofi + ''-'' + codope) AS Operacion, ''ANULACIÓN'' AS Tipo, fecing AS FechaIngreso
	FROM dbo.sce_xanu WITH(NOLOCK)
	WHERE numpre = @numPre
	AND (@CUI = '''' OR codcct = @CUI)
	AND ((@fecDesde = '''' AND @fecHasta = '''') OR fecing >= @fecDesde and fecing <= @fecHasta)

	-- Planillas Invisibles
	INSERT INTO #planillas
	SELECT (codcct + ''-'' + codpro + ''-'' + codesp + ''-'' + codofi + ''-'' + codope) AS Operacion, ''INVISIBLE'' AS Tipo, fecing AS FechaIngreso
	FROM dbo.sce_pli WITH(NOLOCK) 
	WHERE numpli = @numPre
	AND (@CUI = '''' OR codcct = @CUI)
	AND ((@fecDesde = '''' AND @fecHasta = '''') OR fecing >= @fecDesde and fecing <= @fecHasta)


	SELECT DISTINCT 
		Operacion, Tipo, FechaIngreso
	FROM #planillas
	ORDER BY FechaIngreso DESC

	DROP TABLE #planillas

END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_abo_car_s03_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_abo_car_s03_MS]...';
	DROP PROCEDURE [dbo].[pro_sce_abo_car_s03_MS] 
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_abo_car_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_abo_car_s03_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sce_abo_car_s03_MS] 
	@cencos CHAR(3),
	@codusr CHAR(2) 
AS
BEGIN

   DECLARE @fecpro CHAR(10)

   CREATE TABLE #tmp_abocar
   (
      numcct         CHAR(15),
      nemcta         CHAR(15),
      fecmov         DATETIME,
      nroimp         NUMERIC(3,0),
      nomcli         VARCHAR(30),
      cod_dh         CHAR(1),
      moneda         CHAR(3),
      codmon         NUMERIC(3,0),
      mtomcd         NUMERIC(15,2),
      operacion      CHAR(15),
      nrorpt         NUMERIC(8,0),
      enlinea        NUMERIC(1,0),
      estado         NUMERIC(2,0),
      codcct         CHAR(3),
      codpro         CHAR(2),
      codesp         CHAR(2),
      codofi         CHAR(3),
      codope         CHAR(5),
      codfun         NUMERIC(3,0),
      trx_id         CHAR(27),
      cod_prod       CHAR(4),
      cod_trx_fc     CHAR(3),
      cod_ext        CHAR(5),
      cod_swf        CHAR(3),
      data1          CHAR(35),
      data2          CHAR(35),
      data3          CHAR(35),
      data4          CHAR(35),
      data5          CHAR(35),
      cod_trx_cosmos CHAR(3),
      tip_cta        CHAR(4)      
   )

   SELECT   @fecpro = CONVERT(CHAR(10),GETDATE(),101)

   INSERT INTO #tmp_abocar
   SELECT  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(CONVERT(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0),
            d.transaction_id,
            f.cod_prod,
            f.cod_trx_fc,
            f.cod_ext,
            f.cod_swf,
            c.data1,
            c.data2,
            c.data3,
            c.data4,
            c.data5,
            f.cod_trx_cosmos,
            ''CITI''
   FROM  dbo.sce_mcd a,
          dbo.sce_mch b,
          dbo.tbl_sce_tranope_ft c,
          dbo.tbl_sce_relacion_ft d,
          dbo.tbl_sce_parametros_ft e,
          dbo.tbl_sce_codtran_ft f,
          dbo.sgt_mnd g
   WHERE /*a.codpro  = ''30''
   and*/   a.enlinea = 0
   AND   a.cencos  = @cencos
   AND   a.codusr  = @codusr
   AND   a.fecmov  = @fecpro
   AND	  a.nemcta  = e.tipo_ft
   AND   a.codcct  = b.codcct
   AND   a.codpro  = b.codpro
   AND   a.codesp  = b.codesp
   AND   a.codofi  = b.codofi
   AND   a.codope  = b.codope
   AND   a.nrorpt  = b.nrorpt
   AND   a.fecmov  = b.fecmov
   AND   a.codcct  = c.codcct
   AND   a.codpro  = c.codpro
   AND   a.codesp  = c.codesp
   AND   a.codofi  = c.codofi
   AND   a.codope  = c.codope
   AND   a.nrorpt  = c.nrorpt
   AND   a.fecmov  = c.fecmov
   AND   a.cod_dh  = c.cod_dh
   AND   a.codcct  = d.codcct
   AND   a.codpro  = d.codpro
   AND   a.codesp  = d.codesp
   AND   a.codofi  = d.codofi
   AND   a.codope  = d.codope
   AND   a.nroimp  = d.nroimp
   AND   c.nro_trx = f.nro_trx
   AND   a.codmon  = g.mnd_mndcod
   AND   c.nroimp  = d.nroimp
   AND   b.estado <> 9

   INSERT INTO #tmp_abocar
   SELECT a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(CONVERT(VARCHAR,b.nomcli),'''') AS nomcli,
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope AS numope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0) AS codfun,
			c.transaction_id,
            sc.cod_producto_fc,
            sc.cod_transaccion_fc,
            sc.cod_extendido_fc,
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            ''BCH''
FROM  dbo.sce_mcd a 
   LEFT OUTER JOIN dbo.sce_mch b ON
								 a.nrorpt = b.nrorpt 
								 AND a.fecmov = b.fecmov 
								 AND b.estado  <> 9
   LEFT OUTER JOIN dbo.tbl_sce_relacion_ft c ON 
								a.codcct  =  c.codcct 
								AND a.codpro  =  c.codpro 
								AND a.codesp  =  c.codesp 
								AND a.codofi  =  c.codofi 
								AND a.codope  =  c.codope  
								AND a.nroimp =  c.nroimp
								AND a.nrorpt = c.contract_reference,
	dbo.sce_ccce e,
	dbo.sce_cctx tx,
	dbo.tbl_sce_sup_codigos sc,
	dbo.sgt_mnd g
WHERE a.enlinea = 0 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  @fecpro AND a.nemcta  in(''CC$'',''CCE'')
	AND  a.codmon  = g.mnd_mndcod
	AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')
	AND tx.nemtra = 
				(CASE 
					WHEN (a.codmon = 1 AND cod_dh = ''H'') THEN ''ABONOMN''
					WHEN (a.codmon = 1 AND cod_dh = ''D'') THEN ''CARGOMN''
					WHEN (a.codmon <> 1 AND cod_dh = ''H'') THEN ''ABONOME''
					WHEN (a.codmon <> 1 AND cod_dh = ''D'') THEN ''CARGOME'' 
					ELSE ''''
				END) 
	AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE 
					WHEN (a.codmon = 1 AND cod_dh = ''H'') THEN ''77''
					WHEN (a.codmon = 1 AND cod_dh = ''D'') THEN ''28''
					WHEN (a.codmon <> 1 AND cod_dh = ''H'') THEN ''''
					WHEN (a.codmon <> 1 AND cod_dh = ''D'') THEN '''' 
					ELSE ''''
				END)
			
   SELECT   numcct     ,
           nemcta     ,
           fecmov     ,
           nroimp     ,
           nomcli     ,
           cod_dh     ,
           moneda     ,
           codmon     ,
           mtomcd     ,
           operacion  ,
           nrorpt     ,
           enlinea    ,
           estado     ,
           codcct     ,
           codpro     ,
           codesp     ,
           codofi     ,
           codope     ,
           codfun     ,
           trx_id     ,
           cod_prod   ,
           cod_trx_fc ,
           cod_ext    ,
           cod_swf    ,
           data1      ,
           data2      ,
           data3      ,
           data4      ,
           data5      ,
           cod_trx_cosmos,
           tip_cta
   FROM  #tmp_abocar
   ORDER BY nrorpt,nroimp

   DROP TABLE #tmp_abocar
    
END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_rev_abocar_s03_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_rev_abocar_s03_MS]...';
	DROP PROCEDURE [dbo].[pro_sce_rev_abocar_s03_MS] 
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_rev_abocar_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_rev_abocar_s03_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sce_rev_abocar_s03_MS] 
	@cencos CHAR(3),
	@codusr CHAR(2) 
AS
BEGIN

   SET NOCOUNT ON 

   DECLARE @fecpro CHAR(10)
   DECLARE @fecpro2    CHAR(10)

   CREATE TABLE #tmp_rev_abocar
   (
      numcct         CHAR(15),
      nemcta         CHAR(15),
      fecmov         DATETIME,
      nroimp         NUMERIC(3,0),
      nomcli         VARCHAR(30),
      cod_dh         CHAR(1),
      moneda         CHAR(3),
      codmon         NUMERIC(3,0),
      mtomcd         NUMERIC(15,2),
      operacion      CHAR(15),
      nrorpt         NUMERIC(8,0),
      enlinea        NUMERIC(1,0),
      estado         NUMERIC(2,0),
      codcct         CHAR(3),
      codpro         CHAR(2),
      codesp         CHAR(2),
      codofi         CHAR(3),
      codope         CHAR(5),
      codfun         NUMERIC(3,0),
      trx_id         CHAR(27),
      cod_prod       CHAR(4),
      cod_trx_fc     CHAR(3),
      cod_ext        CHAR(5),
      cod_swf        CHAR(3),
      data1          CHAR(35),
      data2          CHAR(35),
      data3          CHAR(35),
      data4          CHAR(35),
      data5          CHAR(35),
      cod_trx_cosmos CHAR(3),
      tip_cta        CHAR(4)      
   )

   SELECT   @fecpro = CONVERT(CHAR(10),GETDATE(),101)

   INSERT INTO #tmp_rev_abocar
   SELECT  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(CONVERT(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0),
            d.transaction_id,
            f.cod_prod,
            f.cod_trx_fc,
            f.cod_ext,
            f.cod_swf,
            c.data1,
            c.data2,
            c.data3,
            c.data4,
            c.data5,
            f.cod_trx_cosmos,
            ''CITI''
   FROM  dbo.sce_mcd a,
          dbo.sce_mch b,
          dbo.tbl_sce_tranope_ft c,
          dbo.tbl_sce_relacion_ft d,
          dbo.tbl_sce_parametros_ft e,
          dbo.tbl_sce_codtran_ft f,
          dbo.sgt_mnd g
   WHERE a.enlinea = 1
   AND   a.cencos  = @cencos
   AND   a.codusr  = @codusr
   AND   a.fecmov  = @fecpro
   AND	  a.nemcta  = e.tipo_ft
   AND   a.codcct  = b.codcct
   AND   a.codpro  = b.codpro
   AND   a.codesp  = b.codesp
   AND   a.codofi  = b.codofi
   AND   a.codope  = b.codope
   AND   a.nrorpt  = b.nrorpt
   AND   a.fecmov  = b.fecmov
   AND   a.codcct  = c.codcct
   AND   a.codpro  = c.codpro
   AND   a.codesp  = c.codesp
   AND   a.codofi  = c.codofi
   AND   a.codope  = c.codope
   AND   a.nrorpt  = c.nrorpt
   AND   a.fecmov  = c.fecmov
   AND   a.cod_dh  = c.cod_dh
   AND   a.codcct  = d.codcct
   AND   a.codpro  = d.codpro
   AND   a.codesp  = d.codesp
   AND   a.codofi  = d.codofi
   AND   a.codope  = d.codope
   AND   a.nroimp  = d.nroimp
   AND   c.nro_trx = f.nro_trx
   AND   a.codmon  = g.mnd_mndcod
   AND   c.nroimp  = d.nroimp
-- and   b.estado <> 9

/*---------------------------------------------
  Realsystems-Código Nuevo-Inicio
  Fecha Modificación 20100902
  Responsable: Pablo Millan V.
  Versión: 1.0
  Descripción : Se omiten inyec. de op. auto.
---------------------------------------------*/
   SELECT   @fecpro2 = CONVERT(CHAR(10),GETDATE(),112)


   DELETE FROM #tmp_rev_abocar
   WHERE  codcct+codpro+codesp+codofi+codope IN 
			(
				   SELECT operacion
				   FROM   dbo.tbl_sce_from_ESB2
				   WHERE  fecingreso = @fecpro2
			)
/*----------------------------------------
  RealSystems - Código Nuevo - Termino
  --------------------------------------*/


   INSERT INTO #tmp_rev_abocar
   SELECT a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(CONVERT(VARCHAR,b.nomcli),'''') AS nomcli,
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope AS numope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0) AS codfun,
			c.transaction_id,
            sc.cod_producto_fc,
            sc.cod_transaccion_fc,
            sc.cod_extendido_fc,
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            ''BCH''
FROM  dbo.sce_mcd a 
   LEFT OUTER JOIN dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov /*and b.estado  <> 9*/
   LEFT OUTER JOIN dbo.tbl_sce_relacion_ft c ON
				a.codcct  =  c.codcct 
				AND a.codpro  =  c.codpro 
				AND a.codesp  =  c.codesp 
				AND a.codofi  =  c.codofi 
				AND a.codope  =  c.codope  
				AND a.nroimp =  c.nroimp
				AND a.nrorpt = c.contract_reference,
	dbo.sce_ccce e,
	dbo.sce_cctx tx,
	dbo.tbl_sce_sup_codigos sc,
	dbo.sgt_mnd g
WHERE a.enlinea = 1 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  @fecpro AND a.nemcta  in(''CC$'',''CCE'')
	AND  a.codmon  = g.mnd_mndcod
	AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')
	AND tx.nemtra = 
				(CASE 
					WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''ABONOMN''
					WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''CARGOMN''
					WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''ABONOME''
					WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''CARGOME'' 
					ELSE ''''
				END) 
	AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE 
					WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''77''
					WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''28''
					WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''''
					WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''''
					ELSE ''''
				END)

   SELECT   numcct     ,
           nemcta     ,
           fecmov     ,
           nroimp     ,
           nomcli     ,
           cod_dh     ,
           moneda     ,
           codmon     ,
           mtomcd     ,
           operacion  ,
           nrorpt     ,
           enlinea    ,
           estado     ,
           codcct     ,
           codpro     ,
           codesp     ,
           codofi     ,
           codope     ,
           codfun     ,
           trx_id     ,
           cod_prod   ,
           cod_trx_fc ,
           cod_ext    ,
           cod_swf    ,
           data1      ,
           data2      ,
           data3      ,
           data4      ,
           data5      ,
           cod_trx_cosmos,
           tip_cta
   FROM  #tmp_rev_abocar
   ORDER BY nrorpt,nroimp

   DROP TABLE #tmp_rev_abocar
    
END
'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s77_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mcd_s77_MS]...';
	DROP PROCEDURE [dbo].[sce_mcd_s77_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s77_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mcd_s77_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure sce_mcd_s77_MS
			@codcct varchar(3),
			@codpro varchar(2),
			@codesp varchar(2),
			@codofi varchar(3),
			@codope varchar(5),
			@nemcta char(15),
			@nrorpt numeric(8),
			@nroimp numeric(3)
	AS
	begin
		/*	
		Descripción:
				Retorna el estado del abono/cargo para evitar ejecutar la misma accion dos veces o desde legacy y .net al mismo tiempo
		Comando de Prueba:
				exec sce_mcd_s16_MS ''753'', ''20'', ''29'', ''000'', ''74946'', ''CCE'', 29164828, 2
		*/
		select enlinea
		from sce_mcd
		where codcct = @codcct and codpro = @codpro and 
		codesp = @codesp and codofi = @codofi and codope = @codope and 
		nemcta = @nemcta and nrorpt = @nrorpt and nroimp = @nroimp
	end'
END
-----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mts_u01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mts_u01_MS]...';
	DROP PROCEDURE [dbo].[sce_mts_u01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mts_u01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_mts_u01_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@numneg         NUMERIC(3,0),
	@tippro         NUMERIC(2,0),
	@numcpa         NUMERIC(6,0),
	@numcuo         NUMERIC(3,0),
	@numcob         NUMERIC(2,0),
	@id_mensaje	NUMERIC(10,0),
	@estado		INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
	SET NOCOUNT ON 
	BEGIN TRY

	update dbo.sce_mts set estado = @estado 
	where	codcct = @codcct	and
	codpro = @codpro	and
	codesp = @codesp	and
	codofi = @codofi	and
	codope = @codope	and
	id_mensaje = @id_mensaje

	IF (@@error <> 0 or @@rowcount = 0)
	BEGIN
		SELECT 9
	END
	ELSE
	BEGIN
		SELECT 0
	END

	END TRY
	BEGIN CATCH  
		SELECT -1
	END CATCH 
END'
END
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s70_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mcd_s70_MS]...'; 
	DROP procedure sce_mcd_s70_MS;
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s70_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mcd_s70_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_s70_MS]   
  @cencos CHAR(3),  
  @codusr CHAR(2)   
 AS  
 begin  
   --se realizo un cambio para que funcione igual que la consulta de inyeccion  
   --primero se traen los movimientos de CITI  
    select   a.numcct,  
     a.nemcta,  
     a.fecmov,  
     a.nroimp,  
     [Column1] = isnull(convert(VARCHAR,b.nomcli),''''),  
     a.cod_dh,  
     a.nemmon,  
     a.codmon,  
     a.mtomcd,  
     [Column2] = a.codcct+a.codpro+a.codesp+a.codofi+a.codope,  
     a.nrorpt,  
     a.enlinea,  
     a.estado,  
     a.codcct,  
     a.codpro,  
     a.codesp,  
     a.codofi,  
     a.codope,  
     [Column3] = isnull(b.codfun,0)  
      from  dbo.sce_mcd a,  
        dbo.sce_mch b,  
        dbo.tbl_sce_tranope_ft c,  
        dbo.tbl_sce_relacion_ft d,  
        dbo.tbl_sce_parametros_ft e,  
        dbo.tbl_sce_codtran_ft f,  
        dbo.sgt_mnd g  
       where /*a.codpro  = ''30''  
       and*/   a.enlinea = 0  
       and   a.cencos  = @cencos  
       and   a.codusr  = @codusr  
       and   a.fecmov  = convert(CHAR(8),GetDate(),112)  
       and   a.nemcta  = e.tipo_ft  
       and   a.codcct  = b.codcct  
       and   a.codpro  = b.codpro  
       and   a.codesp  = b.codesp  
       and   a.codofi  = b.codofi  
       and   a.codope  = b.codope  
       and   a.nrorpt  = b.nrorpt  
       and   a.fecmov  = b.fecmov  
       and   a.codcct  = c.codcct  
       and   a.codpro  = c.codpro  
       and   a.codesp  = c.codesp  
       and   a.codofi  = c.codofi  
       and   a.codope  = c.codope  
       and   a.nrorpt  = c.nrorpt  
       and   a.fecmov  = c.fecmov  
       and   a.cod_dh  = c.cod_dh  
       and   a.codcct  = d.codcct  
       and   a.codpro  = d.codpro  
       and   a.codesp  = d.codesp  
       and   a.codofi  = d.codofi  
       and   a.codope  = d.codope  
       and   a.nroimp  = d.nroimp  
       and   c.nro_trx = f.nro_trx  
       and   a.codmon  = g.mnd_mndcod  
       and   c.nroimp  = d.nroimp  
       and   b.estado <> 9  
  
    union all  
    -- se trae la info de BCH  
   select   a.numcct,  
     a.nemcta,  
     a.fecmov,  
     a.nroimp,  
     [Column1] = isnull(convert(VARCHAR,b.nomcli),''''),  
     a.cod_dh,  
     a.nemmon,  
     a.codmon,  
     a.mtomcd,  
     [Column2] = a.codcct+a.codpro+a.codesp+a.codofi+a.codope,  
     a.nrorpt,  
     a.enlinea,  
     a.estado,  
     a.codcct,  
     a.codpro,  
     a.codesp,  
     a.codofi,  
     a.codope,  
     [Column3] = isnull(b.codfun,0)  
     FROM  dbo.sce_mcd a   
       LEFT OUTER JOIN   
       dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov and b.estado  <> 9  
       LEFT OUTER JOIN  
     dbo.tbl_sce_relacion_ft c ON a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope  and a.nroimp =  c.nroimp,  
     dbo.sce_ccce e,  
     dbo.sce_cctx tx,  
     dbo.tbl_sce_sup_codigos sc,  
     dbo.sgt_mnd g  
       WHERE a.enlinea = 0 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  convert(CHAR(8),GetDate(),112) AND a.nemcta  in(''CC$'',''CCE'')  
    AND  a.codmon  = g.mnd_mndcod  
    AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')  
    AND tx.nemtra =   
       (CASE   
        WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''ABONOMN''  
        WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''CARGOMN''  
        WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''ABONOME''  
        WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''CARGOME''   
        ELSE ''''  
       END)   
    AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE   
        WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''77''  
        WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''28''  
        WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''''  
        WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''''   
        ELSE ''''  
       END)   
  
       UNION ALL  
  
    select   a.numcct,  
     a.nemcta,  
     a.fecmov,  
     a.nroimp,  
     [Column1] = isnull(convert(VARCHAR,b.nomcli),''''),  
     a.cod_dh,  
     a.nemmon,  
     a.codmon,  
     a.mtomcd,  
     [Column2] = a.codcct+a.codpro+a.codesp+a.codofi+a.codope,  
     a.nrorpt,  
     a.enlinea,  
     a.estado,  
     a.codcct,  
     a.codpro,  
     a.codesp,  
     a.codofi,  
     a.codope,  
     [Column3] = isnull(b.codfun,0)  
      from  dbo.sce_mcd a,  
        dbo.sce_mch b,  
        dbo.tbl_sce_tranope_ft c,  
        dbo.tbl_sce_relacion_ft d,  
        dbo.tbl_sce_parametros_ft e,  
        dbo.tbl_sce_codtran_ft f,  
        dbo.sgt_mnd g  
       where /*a.codpro  = ''30''  
       and*/   a.enlinea = 1  
       and   a.cencos  = @cencos  
       and   a.codusr  = @codusr  
       and   a.fecmov  = convert(CHAR(8),GetDate(),112)  
       and   a.nemcta  = e.tipo_ft  
       and   a.codcct  = b.codcct  
       and   a.codpro  = b.codpro  
       and   a.codesp  = b.codesp  
       and   a.codofi  = b.codofi  
       and   a.codope  = b.codope  
       and   a.nrorpt  = b.nrorpt  
       and   a.fecmov  = b.fecmov  
       and   a.codcct  = c.codcct  
       and   a.codpro  = c.codpro  
       and   a.codesp  = c.codesp  
       and   a.codofi  = c.codofi  
       and   a.codope  = c.codope  
       and   a.nrorpt  = c.nrorpt  
       and   a.fecmov  = c.fecmov  
       and   a.cod_dh  = c.cod_dh  
       and   a.codcct  = d.codcct  
       and   a.codpro  = d.codpro  
       and   a.codesp  = d.codesp  
       and   a.codofi  = d.codofi  
       and   a.codope  = d.codope  
       and   a.nroimp  = d.nroimp  
       and   c.nro_trx = f.nro_trx  
       and   a.codmon  = g.mnd_mndcod  
       and   c.nroimp  = d.nroimp  
       and   b.estado = 9  
union all  
    -- se trae la info de BCH  
   select   a.numcct,  
     a.nemcta,  
     a.fecmov,  
     a.nroimp,  
     [Column1] = isnull(convert(VARCHAR,b.nomcli),''''),  
     a.cod_dh,  
     a.nemmon,  
     a.codmon,  
     a.mtomcd,  
     [Column2] = a.codcct+a.codpro+a.codesp+a.codofi+a.codope,  
     a.nrorpt,  
     a.enlinea,  
     a.estado,  
     a.codcct,  
     a.codpro,  
     a.codesp,  
     a.codofi,  
     a.codope,  
     [Column3] = isnull(b.codfun,0)  
     FROM  dbo.sce_mcd a   
       LEFT OUTER JOIN   
       dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov and b.estado  = 9  
       LEFT OUTER JOIN  
     dbo.tbl_sce_relacion_ft c ON a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope  and a.nroimp =  c.nroimp,  
     dbo.sce_ccce e,  
     dbo.sce_cctx tx,  
     dbo.tbl_sce_sup_codigos sc,  
     dbo.sgt_mnd g  
       WHERE a.enlinea = 1 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  convert(CHAR(8),GetDate(),112) AND a.nemcta  in(''CC$'',''CCE'')  
    AND  a.codmon  = g.mnd_mndcod  
    AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')  
    AND tx.nemtra =   
       (CASE   
        WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''ABONOMN''  
        WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''CARGOMN''  
        WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''ABONOME''  
        WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''CARGOME''   
        ELSE ''''  
       END)   
    AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE   
        WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''77''  
        WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''28''  
        WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''''  
        WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''''   
        ELSE ''''  
       END)   
     
    return  
 end'
 END
-----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_rng_u01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_rng_u01_MS]...';
	DROP PROCEDURE [dbo].[sce_rng_u01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rng_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_rng_u01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_rng_u01_MS] 
		@codcct          VARCHAR(3),
		@codesp          VARCHAR(2),
		@codfun          VARCHAR(3),
		@minNumRequerido NUMERIC(10,0) = NULL
	AS
	BEGIN

	SET NOCOUNT ON 

	DECLARE 
		@numact NUMERIC(10,0), 
		@nummax	NUMERIC(10,0), 
		@errorReturn NUMERIC(10,0)
	SET @errorReturn = -1

	BEGIN TRAN

	-- Validar si llego al valor máximo
	IF NOT EXISTS(SELECT TOP 1 1 FROM sce_rng WITH (holdlock) WHERE codcct = @codcct AND codesp = @codesp AND codfun = @codfun AND numact < nummax AND numact > 0)
	BEGIN
		SELECT @errorReturn AS valor, ''No hay más número de rango para la planilla '' + @codfun AS mensaje
		RETURN -1
	END

	IF (@minNumRequerido IS NOT NULL)
	BEGIN

		SELECT @numact = numact 
		FROM sce_rng WITH (HOLDLOCK)
		WHERE
			codcct = @codcct AND
			codesp = @codesp AND
			codfun = @codfun AND
			numact < nummax	 AND
			numact > 0

		IF(@numact < @minNumRequerido)
			SELECT @numact = @numact + 1 -- sumo 1 mas de lo requerido para dar margen para la concurrencia


		UPDATE dbo.sce_rng SET numact = @numact + 1  WHERE
			codcct = @codcct AND
			codesp = @codesp AND
			codfun = @codfun AND
			numact < nummax	 AND
			numact > 0

	END
	ELSE
	BEGIN

		UPDATE dbo.sce_rng SET numact = numact + 1  WHERE
			codcct = @codcct AND
			codesp = @codesp AND
			codfun = @codfun AND
			numact < nummax	 AND
			numact > 0
	END

	IF (@@error <> 0)
	BEGIN
			ROLLBACK 
			SELECT   @errorReturn AS valor, ''No es posible actualizar el número del rango del especialista.'' AS mensaje
			RETURN -1
	END

	   SELECT   @numact = numact, @nummax = nummax
	   FROM 	dbo.sce_rng   WITH (HOLDLOCK)
	   WHERE 	codcct = @codcct AND
	   codesp = @codesp AND
	   codfun = @codfun

		IF (@numact > @nummax AND @numact > 0)
		BEGIN
			ROLLBACK 
			SELECT   @errorReturn AS valor, ''No es posible obtener el número del rango del especialista.'' AS mensaje
			RETURN -1
		END
		
		COMMIT TRAN

		SELECT   @numact AS valor,''Éxito'' AS mensaje
		RETURN @numact
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_ovd_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_ovd_s02_MS]...';
	DROP PROCEDURE [dbo].sce_ovd_s02_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ovd_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_ovd_s02_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE sce_ovd_s02_MS
		@numCta INT
	AS
	BEGIN
		/*	
		Descripción:
				Retorna 0 si no es una obligación o 1 en caso de serlo
		Comando de Prueba:
				exec sce_ovd_s02_MS 52
		*/

		DECLARE 
			@minRng INT, 
			@maxRng INT,
			@esObligacion BIT


		SELECT @minRng = CONVERT(INT, tc_num_valor) FROM tbl_sce_tabcomex_num 
		WHERE tc_num_metadata = ''RNG_MIN_NUM_CTA_OBLIGACIONES''
		SELECT @maxRng = CONVERT(INT, tc_num_valor) FROM tbl_sce_tabcomex_num 
		WHERE tc_num_metadata = ''RNG_MAX_NUM_CTA_OBLIGACIONES''

		-- Por defecto no es obligacion 
		SET @esObligacion = 0; 
		-- En caso de encontrar un registro marcamos la variable como 1 (true)
		SELECT @esObligacion = 1
		FROM sce_ovd ovd
		INNER JOIN sce_cta cta ON ovd.nemcta = cta.cta_nem
		WHERE ovd.numcta = @numCta 
		-- obtenemos los 3 primeros digitos y los convertimos a numero para realizar el between
		AND CONVERT(INT, SUBSTRING(cta.cta_num, 1, 3)) BETWEEN @minRng AND @maxRng 
	
		SELECT @esObligacion;
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'tbl_sce_tabcomex_num_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [tbl_sce_tabcomex_num_s01_MS]...';
	DROP PROCEDURE [dbo].tbl_sce_tabcomex_num_s01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_sce_tabcomex_num_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [tbl_sce_tabcomex_num_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE tbl_sce_tabcomex_num_s01_MS
	AS
	BEGIN
		/*	
		Descripción:
				Retorna el rango mínimo y máximo de los numeros de cuenta de obligaciones.
		Comando de Prueba:
				exec tbl_sce_tabcomex_num_s01_MS
		*/

		SELECT CONVERT(INT, num1.tc_num_valor) AS rng_min, CONVERT(INT, num2.tc_num_valor) AS rng_max FROM tbl_sce_tabcomex_cabecera cab
		INNER JOIN tbl_sce_tabcomex_num num1 ON cab.tc_id = num1.tc_id
		INNER JOIN tbl_sce_tabcomex_num num2 ON cab.tc_id = num2.tc_id
		WHERE 
		num1.tc_num_metadata = ''RNG_MIN_NUM_CTA_OBLIGACIONES''
		AND num2.tc_num_metadata = ''RNG_MAX_NUM_CTA_OBLIGACIONES''

	END'
END
-----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_prd_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_prd_s01_MS]...';
	DROP PROCEDURE [dbo].sce_prd_s01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prd_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_prd_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE sce_prd_s01_MS
	@EsAsociacion bit
	AS
	BEGIN
		 
		 SELECT 
			codpro, 
			UPPER(CASE codpro 
				WHEN ''03'' THEN ''Cobranzas Importaciones''
				WHEN ''05'' THEN ''Préstamos a Exportadores''
				WHEN ''06'' THEN ''Cobranza de Exportaciones''
				WHEN ''07'' THEN ''Carta de Crédito Importaciones''
				WHEN ''09'' THEN ''Carta de Crédito Exportaciones''
				WHEN ''10'' THEN ''Contabilidad Genérica''
				WHEN ''12'' THEN ''Descuentos Documentos Exportaciones''
				WHEN ''13'' THEN ''Compra Documentos Exportaciones''
				WHEN ''17'' THEN ''Retornos de Exportaciones''
				WHEN ''18'' THEN ''Compra Anticipada de Carta de Crédito de Exportaciones''
				WHEN ''30'' THEN ''Compra / Venta Exportaciones''
			ELSE despro END) as despro
		FROM dbo.sce_prd
		WHERE (@EsAsociacion = 1 AND codpro in (''03'',''05'',''06'',''07'',''09'',''10'',''12'',''13'',''17'',''18'',''30'')) OR (@EsAsociacion = 0)
		ORDER BY codpro ASC

	END'
END
-----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_tcom_i01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_tcom_i01_MS]...';
	DROP PROCEDURE [dbo].sce_tcom_i01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_tcom_i01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_tcom_i01_MS] 
		@id_party	CHAR(12),
		@sistema	CHAR(3),
		@producto	CHAR(3),
		@etapa		CHAR(3),
		@secuencia	NUMERIC(2, 0),
		@borrado	BIT,
		@manual_t	BIT,
		@monto_fijo BIT,
		@tasa		NUMERIC(9, 6),
		@hasta_mon	NUMERIC(15, 2),
		@minimo		NUMERIC(15, 2),
		@maximo		NUMERIC(15, 2),
		@fecha		SMALLDATETIME
	AS
	BEGIN

		SET NOCOUNT ON
		BEGIN TRY

			INSERT INTO sce_tcom (id_party,
								sistema, 
								producto, 
								etapa,
								secuencia,
								borrado,
								manual_t, 
								monto_fijo,
								tasa,
								hasta_mon,
								minimo, 
								maximo,
								fecha) 
						VALUES (@id_party,
								@sistema,
								@producto,
								@etapa,
								@secuencia,
								@borrado,
								@manual_t,
								@monto_fijo,
								@tasa,
								@hasta_mon,
								@minimo,
								@maximo,
								@fecha)

			SELECT 0;
		END TRY
		BEGIN CATCH
			SELECT -1;
		END CATCH
	END'
END
-----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_UpdateCargoYAbono_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_UpdateCargoYAbono_MS]...';
	DROP PROCEDURE [dbo].proc_UpdateCargoYAbono_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_UpdateCargoYAbono_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_UpdateCargoYAbono_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE proc_UpdateCargoYAbono_MS
	@CentroCosto		NVARCHAR(3),
	@Producto			NVARCHAR(2),
	@Especialista		NVARCHAR(2),
	@Oficina			NVARCHAR(3),
	@Operacion			NVARCHAR(5),
	@NumeroImpresion	NVARCHAR(5),
	@TransaccionID		NVARCHAR(27),
	@NumeroReporte		NVARCHAR(9)
AS
BEGIN

	DECLARE @codcct				NVARCHAR(3)		SET @codcct = @CentroCosto
	DECLARE @codpro				NVARCHAR(2)		SET @codpro = @Producto
	DECLARE @codesp				NVARCHAR(2)		SET @codesp = @Especialista
	DECLARE @codofi				NVARCHAR(3)		SET @codofi = @Oficina
	DECLARE @codope				NVARCHAR(5)		SET @codope = @Operacion
	DECLARE @nroimp				NVARCHAR(5)		SET @nroimp = @NumeroImpresion
	DECLARE @transaction_id		NVARCHAR(27)	SET @transaction_id = @TransaccionID
	DECLARE @contract_reference	NVARCHAR(9)		SET @contract_reference = @NumeroReporte

	DECLARE @TablaSalida TABLE(
		codcct	char(3),
		codpro	char(2),
		codesp	char(2),
		codofi	char(3),
		codope	char(5),
		monto	numeric,
		moneda	numeric,
		nroimp	numeric,
		transaction_id	char(27),
		fecing	datetime,
		contract_reference	numeric,
		transaction_id_rev	varchar(27)
	)

	INSERT INTO @TablaSalida
	SELECT	
			codcct
			,codpro
			,codesp
			,codofi
			,codope
			,monto
			,moneda
			,nroimp
			,transaction_id
			,fecing
			,contract_reference
			,transaction_id_rev
	FROM	tbl_sce_relacion_ft
	WHERE	codcct = @codcct AND
			codpro = @codpro AND
			codesp = @codesp AND
			codofi = @codofi AND
			codope = @codope AND
			nroimp = @nroimp AND 
			--transaction_id = @transaction_id AND
			contract_reference = @contract_reference

	SELECT 
		codcct
		,codpro
		,codesp
		,codofi
		,codope
		,monto
		,moneda
		,nroimp
		,transaction_id
		,fecing
		,contract_reference
		,transaction_id_rev
	FROM @TablaSalida
END'
END
-----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'scejacp_s05' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [scejacp_s05]...';
	DROP PROCEDURE [dbo].[scejacp_s05]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scejacp_s05]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [scejacp_s05]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[scejacp_s05] 
	@codcct	CHAR(3),
	@codesp	CHAR(2),
	@fecpro	datetime 
AS
BEGIN
	
	SELECT   
		codcct,
		codesp,
		codpro,
		codofi,
		codope,
		numneg,
		numacp,
		monacp,
		salacp,
		venacp,
		rollover
	FROM 
		dbo.sce_jacp
	WHERE 
		@codcct = codcct	and
		@codesp = codesp	and
		@fecpro >= venacp	and
		salacp > 0			and
		(estacp = 3 or estacp = 4)
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mch_s03_MS]...';
	DROP PROCEDURE [dbo].sce_mch_s03_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mch_s03_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_mch_s03_MS @codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:34:34 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   select   codcct	,
		codpro	,
		codesp	,
		codofi	,
		codope	,
		nrorpt	,
		fecmov	,
		codfun
   from dbo.sce_mch where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
   union
   select   codcct  ,
		codpro  ,
		codesp  ,
		codofi  ,
		codope  ,
		nrorpt  ,
		fecmov  ,
		codfun
   from dbo.sce_mchh where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope    


   return
   return
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_xdoc_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xdoc_s03_MS]...';
	DROP PROCEDURE [dbo].sce_xdoc_s03_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_xdoc_s03_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_xdoc_s03_MS 
	@fecing      datetime,
	@cencos      CHAR(3),
	@codusr      CHAR(2) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:56:56 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
   select   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		nrocor,
		fecing,
		coddoc
   from 	dbo.sce_xdoc
   where 	fecing = @fecing and
   cencos = @cencos and
   codusr = @codusr

   return
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_xdoc_s04_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xdoc_s04_MS]...';
	DROP PROCEDURE [dbo].sce_xdoc_s04_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_xdoc_s04_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_xdoc_s04_MS
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:18:18 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
   select   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		nrocor,
		fecing,
		coddoc
   from 	dbo.sce_xdoc
   where 	codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

   return

end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_relacion_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_relacion_s01_MS]...';
	DROP PROCEDURE [dbo].pro_sce_relacion_s01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_relacion_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [pro_sce_relacion_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'Create PROCEDURE dbo.pro_sce_relacion_s01_MS
	@gb_llave CHAR(27), 
	@flag INT 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:33:33 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   if @flag = 0
   begin
      Select   CONVERT(CHAR(10),a.contract_reference)
      From   dbo.tbl_sce_relacion_ft a
      Where  a.transaction_id  = @gb_llave
   end
else
   begin
      Select   codcct, codpro, codesp, codofi, codope
      From   dbo.tbl_sce_relacion_ft
      Where  contract_reference  = convert(NUMERIC(10,0),@gb_llave)
   end
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_d01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_swf_pendientes_d01_MS]...';
	DROP PROCEDURE [dbo].pro_sce_swf_pendientes_d01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_swf_pendientes_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [pro_sce_swf_pendientes_d01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure pro_sce_swf_pendientes_d01_MS
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(255)
as
begin
	delete from sce_swf_pendientes
	where  ctecct = @ctecct and codesp = @codesp and archivo = @archivo
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_cvd_w01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_cvd_w01_MS]...';
	DROP PROCEDURE [dbo].sce_cvd_w01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_cvd_w01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_cvd_w01_MS 
	@codcct          CHAR(3)       ,
	@codpro          CHAR(2)       ,
	@codesp          CHAR(2)       ,
	@codofi          CHAR(3)       ,
	@codope          CHAR(5)       ,
	@cencos          CHAR(3)       ,
	@codusr          CHAR(2)       ,
	@fecing          datetime       ,
	@fecact          datetime       ,
	@tipcvd          NUMERIC(2,0)    ,
	@estado          NUMERIC(2,0)    ,
	@operel          CHAR(15)       ,
	@prtcli          CHAR(12)       ,
	@rutcli			 CHAR(10)	,
	@indnomc         NUMERIC(2,0)    ,
	@inddirc         NUMERIC(2,0)    ,
	@indpopc         CHAR(2)       ,
	@prtotr          CHAR(12)       ,
	@indnomo         NUMERIC(2,0)    ,
	@inddiro         NUMERIC(2,0)    ,
	@indpopo         CHAR(2) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 14:37:37 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
/************************************************************************/
   declare @oficon_c       CHAR(3),@oficon         NUMERIC(3,0),@filler3        CHAR(3),   
   @numreg         NUMERIC(3,0),@data           CHAR(30)
begin tran
   IF not exists(SELECT TOP 1 1 from dbo.sce_cvd
   where 	codcct = @codcct        and
   codpro = @codpro        and
   codesp = @codesp        and
   codofi = @codofi        and
   codope = @codope)
   begin
      insert into dbo.sce_cvd values(@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@cencos,
		@codusr,
		@fecing,
		@fecact,
		@tipcvd,
		@estado,
		@operel,
		@prtcli,
		@rutcli,
		@indnomc,
		@inddirc,
		@indpopc,
		@prtotr,
		@indnomo,
		@inddiro,
		@indpopo)
		
      if (@@rowcount > 0 and @@error = 0)
      begin       
			-- Log Comex
			-- Oficina contable  
         EXECUTE sce_ccof_s02 @codcct,@oficon_c OUTPUT
         if @oficon_c IS NULL
            select   @oficon_c = @codofi
         select   @oficon = convert(NUMERIC(3,0),@oficon_c)
         if @tipcvd = 2          -- Arbitraje 
         begin
            select   @filler3 = ''1''
            select   @data = ''C/V Div con Arbitraje''
         end
      else
         begin
            select   @filler3 = ''0''
            select   @data = ''C/V Div sin Arbitraje''
         end
         insert  into dbo.sce_gtlg(codproe, anulac, codfun, subproe, codcct, codpro,
        		codesp, codofi, codope, fecpro, filler3, esppro,
        		espown , oficon,rutcli, data)
			values(''20'', 0, ''001'', ''000'', @codcct, @codpro,
        		@codesp, @codofi, @codope, GetDate(), @filler3,
        		@codusr, @cencos, @oficon, @rutcli, @data)
			
         if (@@error <> 0 or @@rowcount = 0)
         begin
            rollback 
            return 7
         end
      else
         begin
            commit tran
            select   0
         end
      end
   else
      begin
         rollback 
         select   8
      end
   end
ELSE
   begin
      update dbo.sce_cvd set codcct  = @codcct,codpro  = @codpro,codesp  = @codesp,codofi  = @codofi,
      codope  = @codope,cencos  = @cencos,codusr  = @codusr,fecing  = @fecing,
      fecact  = @fecact,tipcvd  = @tipcvd,estado  = @estado,operel  = @operel,
      prtcli  = @prtcli,rutcli  = @rutcli,indnomc = @indnomc,inddirc = @inddirc,
      indpopc = @indpopc,prtotr  = @prtotr,indnomo = @indnomo,inddiro = @inddiro,
      indpopo = @indpopo  where	codcct  = @codcct and
      codpro  = @codpro and
      codesp  = @codesp and
      codofi  = @codofi and
      codope  = @codope
      if (@@rowcount > 0 and @@error = 0)
      begin
         commit tran
         select   0
      end
   else
      begin
         rollback 
         select   9
      end
   end	
   return
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mts_i01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mts_i01_MS]...';
	DROP PROCEDURE [dbo].sce_mts_i01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mts_i01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_mts_i01_MS @codcct  	CHAR(3),  
 	@codpro  	CHAR(2),  
 	@codesp  	CHAR(2),  
 	@codofi  	CHAR(3),  
 	@codope  	CHAR(5),  
 	@numneg  	NUMERIC(3,0),  
 	@tippro  	NUMERIC(2,0),  
 	@numcpa  	NUMERIC(6,0),  
 	@numcuo  	NUMERIC(3,0),  
	@numcob		NUMERIC(2,0),
	@rutais		NUMERIC(8,0),
	@fecmsg		datetime,
	@id_mensaje	NUMERIC(10,0),
	@estado		NUMERIC(2,0),
	@tipgra		CHAR(1),
	@nrorpt		NUMERIC(8,0),
	@tipmt          NUMERIC(3,0) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:21:21 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

BEGIN TRAN
   insert into dbo.sce_mts(codcct,
		codpro,
		codesp,
		codofi,
		codope,
 		numneg,
 		tippro,
 		numcpa,
 		numcuo,
		numcob,
        	rutais,
        	fecmsg,
        	id_mensaje,
        	estado,
        	tipgra,
		nrorpt,
		tipmt)
	values(@codcct,
        	@codpro,
        	@codesp,
        	@codofi,
        	@codope,
 		@numneg,
 		@tippro,
 		@numcpa,
 		@numcuo,
		@numcob,
        	@rutais,
        	@fecmsg,
        	@id_mensaje,
        	@estado,
        	@tipgra,
		@nrorpt,
		@tipmt)
	
   if (@@error <> 0)
   begin
      ROLLBACK 
      select   -1
   end
   COMMIT TRAN
   select   0
   Return
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'scedev_difdh_dev_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [scedev_difdh_dev_MS]...';
	DROP PROCEDURE [dbo].scedev_difdh_dev_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scedev_difdh_dev_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [scedev_difdh_dev_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.scedev_difdh_dev_MS
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:14:14 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   select   numope,
        mesdif,
        codmon,
        mtodeb,
        mtohab,
        mtodif,
        tipmov
   from dbo.scedev_difdev

   return
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_dev_cons_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_dev_cons_MS]...';
	DROP PROCEDURE [dbo].sce_dev_cons_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dev_cons_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_dev_cons_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sce_dev_cons_MS @Periodo NUMERIC(6,0),
	@RUT VARCHAR(12),
	@Operacion VARCHAR(15),
	@Moneda NUMERIC(3,0),
	@Todos CHAR(1),
	@TipoConsulta NUMERIC(2,0),
	@NumeroRegistros INT 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 12:54:54 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).


   SET rowcount @NumeroRegistros
   IF (@TipoConsulta = 1 OR @TipoConsulta = 2)
   BEGIN
      IF @TipoConsulta = 1
      begin
         if  @Todos = ''S'' and @Moneda = 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,
				convert(CHAR(10),t_fecfin,103) as t_fecfin,t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,
				t_mtovig,t_cuenta_k,t_nemonico_k,t_mtome_c,t_mtomn_c,t_cuenta_c,t_nemonico_c,t_mtome_gn,
				t_mtomn_gn,t_cuenta_gn,t_nemonico_gn,t_mtome_gd,t_mtomn_gd,t_cuenta_gd,t_nemonico_gd,t_mtome_gdd,
				t_mtomn_gdd,t_cuenta_gdd,t_nemonico_gdd,t_mtome_cp,t_mtomn_cp,t_cuenta_cp,t_nemonico_cp,
				t_mtome_gpn,t_mtomn_gpn,t_cuenta_gpn,t_nemonico_gpn,t_mtome_gpd,t_mtomn_gpd,t_cuenta_gpd,
				t_nemonico_gpd,t_mtome_gpdd,t_mtomn_gpdd,t_cuenta_gpdd,t_nemonico_gpdd,t_tippro,t_numpro,
				t_numcuo,t_fec_deterioro,t_tasa_penal,t_to,t_to_plazo 
			from dbo.sce_datos_cuadratura where t_ano_mes = @Periodo
            order by t_num_ope
         end
         if  @Todos = ''S'' and @Moneda <> 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias, t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_mtome_c,t_mtomn_c,t_cuenta_c,t_nemonico_c,t_mtome_gn,
				t_mtomn_gn,t_cuenta_gn,t_nemonico_gn,t_mtome_gd,t_mtomn_gd,t_cuenta_gd,t_nemonico_gd,t_mtome_gdd,t_mtomn_gdd,t_cuenta_gdd,
				t_nemonico_gdd,t_mtome_cp,t_mtomn_cp,t_cuenta_cp,t_nemonico_cp,t_mtome_gpn,t_mtomn_gpn,t_cuenta_gpn,t_nemonico_gpn,t_mtome_gpd,
				t_mtomn_gpd,t_cuenta_gpd,t_nemonico_gpd,t_mtome_gpdd,t_mtomn_gpdd,t_cuenta_gpdd,t_nemonico_gpdd,t_tippro,t_numpro,t_numcuo,
				t_fec_deterioro,t_tasa_penal,t_to,t_to_plazo 
			from dbo.sce_datos_cuadratura
            where t_ano_mes = @Periodo
            AND t_monpro = @Moneda
            order by t_num_ope
         end
         if @RUT <> '''' and @Moneda = 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_mtome_c,t_mtomn_c,t_cuenta_c,t_nemonico_c,t_mtome_gn,
				t_mtomn_gn,t_cuenta_gn,t_nemonico_gn,t_mtome_gd,t_mtomn_gd,t_cuenta_gd,t_nemonico_gd,t_mtome_gdd,t_mtomn_gdd,t_cuenta_gdd,
				t_nemonico_gdd,t_mtome_cp,t_mtomn_cp,t_cuenta_cp,t_nemonico_cp,t_mtome_gpn,t_mtomn_gpn,t_cuenta_gpn,t_nemonico_gpn,t_mtome_gpd,
				t_mtomn_gpd,t_cuenta_gpd,t_nemonico_gpd,t_mtome_gpdd,t_mtomn_gpdd,t_cuenta_gpdd,t_nemonico_gpdd,t_tippro,t_numpro,t_numcuo,
				t_fec_deterioro,t_tasa_penal,t_to,t_to_plazo 
			from dbo.sce_datos_cuadratura
            where t_ano_mes = @Periodo
            AND t_rut = @RUT
            order by t_num_ope
         end
         if @RUT <> '''' and @Moneda <> 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_mtome_c,t_mtomn_c,t_cuenta_c,t_nemonico_c,
				t_mtome_gn,t_mtomn_gn,t_cuenta_gn,t_nemonico_gn,t_mtome_gd,t_mtomn_gd,t_cuenta_gd,t_nemonico_gd,t_mtome_gdd,t_mtomn_gdd,
				t_cuenta_gdd,t_nemonico_gdd,t_mtome_cp,t_mtomn_cp,t_cuenta_cp,t_nemonico_cp,t_mtome_gpn,t_mtomn_gpn,t_cuenta_gpn,t_nemonico_gpn,
				t_mtome_gpd,t_mtomn_gpd,t_cuenta_gpd,t_nemonico_gpd,t_mtome_gpdd,t_mtomn_gpdd,t_cuenta_gpdd,t_nemonico_gpdd,t_tippro,t_numpro,
				t_numcuo,t_fec_deterioro,t_tasa_penal,t_to,t_to_plazo 
			from dbo.sce_datos_cuadratura
            where t_ano_mes = @Periodo
            AND t_monpro = @Moneda
            AND t_rut = @RUT
            order by t_num_ope
         end
         if @Operacion <> '''' and @Moneda = 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_mtome_c,t_mtomn_c,t_cuenta_c,t_nemonico_c,
				t_mtome_gn,t_mtomn_gn,t_cuenta_gn,t_nemonico_gn,t_mtome_gd,t_mtomn_gd,t_cuenta_gd,t_nemonico_gd,t_mtome_gdd,t_mtomn_gdd,
				t_cuenta_gdd,t_nemonico_gdd,t_mtome_cp,t_mtomn_cp,t_cuenta_cp,t_nemonico_cp,t_mtome_gpn,t_mtomn_gpn,t_cuenta_gpn,t_nemonico_gpn,
				t_mtome_gpd,t_mtomn_gpd,t_cuenta_gpd,t_nemonico_gpd,t_mtome_gpdd,t_mtomn_gpdd,t_cuenta_gpdd,t_nemonico_gpdd,t_tippro,t_numpro,
				t_numcuo,t_fec_deterioro,t_tasa_penal,t_to,t_to_plazo 
			from dbo.sce_datos_cuadratura
            where t_ano_mes = @Periodo
            AND t_num_ope like ''%''+@Operacion+''%''
            order by t_num_ope
         end
         if @Operacion <> '''' and @Moneda <> 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_mtome_c,t_mtomn_c,t_cuenta_c,t_nemonico_c,t_mtome_gn,
				t_mtomn_gn,t_cuenta_gn,t_nemonico_gn,t_mtome_gd,t_mtomn_gd,t_cuenta_gd,t_nemonico_gd,t_mtome_gdd,t_mtomn_gdd,t_cuenta_gdd,
				t_nemonico_gdd,t_mtome_cp,t_mtomn_cp,t_cuenta_cp,t_nemonico_cp,t_mtome_gpn,t_mtomn_gpn,t_cuenta_gpn,t_nemonico_gpn,t_mtome_gpd,
				t_mtomn_gpd,t_cuenta_gpd,t_nemonico_gpd,t_mtome_gpdd,t_mtomn_gpdd,t_cuenta_gpdd,t_nemonico_gpdd,t_tippro,t_numpro,t_numcuo,
				t_fec_deterioro,t_tasa_penal,t_to,t_to_plazo 
			from dbo.sce_datos_cuadratura
            where t_ano_mes = @Periodo
            AND t_num_ope like ''%''+@Operacion+''%''
            AND t_monpro = @Moneda
            order by t_num_ope
         end
      end
   ELSE IF @TipoConsulta = 2
      begin
         if  @Todos = ''S'' and @Moneda = 9
         begin
            select   t_num_ope
                        ,t_cui
                        ,t_numneg
                        ,t_monpro
                        ,t_tastot
                        ,convert(CHAR(10),t_fecini,103) as t_fecini
                        ,convert(CHAR(10),t_fecfin,103) as t_fecfin
                        ,t_dias
                        ,t_tipcam
                        ,t_rut
                        ,t_ano_mes
                        ,t_estado
                        ,t_mtovig
                        ,t_cuenta_k
                        ,t_nemonico_k
                        ,t_rmtome_c
                        ,t_rmtomn_c
                        ,t_rcuenta_c
                        ,t_rnemonico_c
                        ,t_rmtome_gn
                        ,t_rmtomn_gn
                        ,t_rcuenta_gn
                        ,t_rnemonico_gn
                        ,t_rmtome_gd
                        ,t_rmtomn_gd
                        ,t_rcuenta_gd
                        ,t_rnemonico_gd
                        ,t_rmtome_gdd
                        ,t_rmtomn_gdd
                        ,t_rcuenta_gdd
                        ,t_rnemonico_gdd
                        ,t_tippro
                        ,t_numpro
                        ,t_sprwsh
                        ,t_to
                        ,t_to_plazo
            from    dbo.sce_datos_cuadratura_r
            where   t_ano_mes = @Periodo
            order by t_num_ope
         end
         if  @Todos = ''S'' and @Moneda <> 9
         begin
            select   t_num_ope
                        ,t_cui
                        ,t_numneg
                        ,t_monpro
                   ,t_tastot
                        ,convert(CHAR(10),t_fecini,103) as t_fecini
                        ,convert(CHAR(10),t_fecfin,103) as t_fecfin
                        ,t_dias
                        ,t_tipcam
                        ,t_rut
                        ,t_ano_mes
                        ,t_estado
                        ,t_mtovig
                        ,t_cuenta_k
                        ,t_nemonico_k
                        ,t_rmtome_c
                        ,t_rmtomn_c
                        ,t_rcuenta_c
                        ,t_rnemonico_c
                        ,t_rmtome_gn
                        ,t_rmtomn_gn
                        ,t_rcuenta_gn
                        ,t_rnemonico_gn
                        ,t_rmtome_gd
                        ,t_rmtomn_gd
                        ,t_rcuenta_gd
                        ,t_rnemonico_gd
                        ,t_rmtome_gdd
                        ,t_rmtomn_gdd
                        ,t_rcuenta_gdd
                        ,t_rnemonico_gdd
                        ,t_tippro
                        ,t_numpro
                        ,t_sprwsh
                        ,t_to
                        ,t_to_plazo
            from dbo.sce_datos_cuadratura_r
            where t_ano_mes = @Periodo
            AND t_monpro = @Moneda
            order by t_num_ope
         end
         if @RUT <> '''' and @Moneda = 9
         begin
            select   t_num_ope
                        ,t_cui
                        ,t_numneg
                        ,t_monpro
                        ,t_tastot
                        ,convert(CHAR(10),t_fecini,103) as t_fecini
                        ,convert(CHAR(10),t_fecfin,103) as t_fecfin
                        ,t_dias
                        ,t_tipcam
                        ,t_rut
                        ,t_ano_mes
                        ,t_estado
                        ,t_mtovig
                        ,t_cuenta_k
                        ,t_nemonico_k
                        ,t_rmtome_c
                        ,t_rmtomn_c
                        ,t_rcuenta_c
                        ,t_rnemonico_c
                        ,t_rmtome_gn
                        ,t_rmtomn_gn
                        ,t_rcuenta_gn
                        ,t_rnemonico_gn
                        ,t_rmtome_gd
                        ,t_rmtomn_gd
                        ,t_rcuenta_gd
                        ,t_rnemonico_gd
                        ,t_rmtome_gdd
                        ,t_rmtomn_gdd
                        ,t_rcuenta_gdd
                        ,t_rnemonico_gdd
                        ,t_tippro
                        ,t_numpro
                        ,t_sprwsh
                        ,t_to
                        ,t_to_plazo
            from dbo.sce_datos_cuadratura_r
            where t_ano_mes = @Periodo
            AND t_rut = @RUT
            order by t_num_ope
         end
         if @RUT <> '''' and @Moneda <> 9
         begin
            select   t_num_ope
                ,t_cui
                ,t_numneg
                ,t_monpro
                ,t_tastot
                ,convert(CHAR(10),t_fecini,103) as t_fecini
                ,convert(CHAR(10),t_fecfin,103) as t_fecfin
                ,t_dias
                ,t_tipcam
                ,t_rut
                ,t_ano_mes
                ,t_estado
                ,t_mtovig
                ,t_cuenta_k
                ,t_nemonico_k
                ,t_rmtome_c
                ,t_rmtomn_c
                ,t_rcuenta_c
                ,t_rnemonico_c
                ,t_rmtome_gn
                ,t_rmtomn_gn,
    t_rcuenta_gn,
                t_rnemonico_gn,
                t_rmtome_gd
                ,t_rmtomn_gd
                ,t_rcuenta_gd
                ,t_rnemonico_gd
                ,t_rmtome_gdd
                ,t_rmtomn_gdd
                ,t_rcuenta_gdd
                ,t_rnemonico_gdd
                ,t_tippro
                ,t_numpro
                ,t_sprwsh
                ,t_to
                ,t_to_plazo
            from dbo.sce_datos_cuadratura_r
            where t_ano_mes = @Periodo
            AND t_monpro = @Moneda
            AND t_rut = @RUT
            order by t_num_ope
         end
         if @Operacion <> '''' and @Moneda = 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_rmtome_c,t_rmtomn_c,t_rcuenta_c,t_rnemonico_c,
				t_rmtome_gn,t_rmtomn_gn,t_rcuenta_gn,t_rnemonico_gn,t_rmtome_gd	,t_rmtomn_gd	,t_rcuenta_gd	,t_rnemonico_gd	,t_rmtome_gdd,
				t_rmtomn_gdd	,t_rcuenta_gdd	,t_rnemonico_gdd	,t_tippro	,t_numpro	,t_sprwsh	,t_to	,t_to_plazo
            from dbo.sce_datos_cuadratura_r
            where t_ano_mes = @Periodo
            AND t_num_ope like ''%''+@Operacion+''%''
            order by t_num_ope
         end
         if @Operacion <> '''' and @Moneda <> 9
         begin
            select   
				t_num_ope,t_cui,t_numneg,t_monpro,t_tastot,convert(CHAR(10),t_fecini,103) as t_fecini,convert(CHAR(10),t_fecfin,103) as t_fecfin,
				t_dias,t_tipcam,t_rut,t_ano_mes,t_estado,t_mtovig,t_cuenta_k,t_nemonico_k,t_rmtome_c,t_rmtomn_c,t_rcuenta_c,t_rnemonico_c,
				t_rmtome_gn,t_rmtomn_gn,t_rcuenta_gn,t_rnemonico_gn,t_rmtome_gd	,t_rmtomn_gd	,t_rcuenta_gd	,t_rnemonico_gd	,t_rmtome_gdd	,
				t_rmtomn_gdd	,t_rcuenta_gdd	,t_rnemonico_gdd	,t_tippro	,t_numpro	,t_sprwsh	,t_to	,t_to_plazo
            from dbo.sce_datos_cuadratura_r
            where t_ano_mes = @Periodo
            AND t_num_ope like ''%''+@Operacion+''%''
            AND t_monpro = @Moneda
            order by t_num_ope
         end
      end
   END
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_cctx_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_cctx_s01_MS]...';
	DROP PROCEDURE [dbo].sce_cctx_s01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cctx_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_cctx_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_cctx_s01_MS
	AS
	BEGIN
	-- This procedure was converted on Wed Apr 16 16:13:13 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   select   codtra, nemtra, destra
	   from	dbo.sce_cctx

	   return
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'scejacp_s07_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [scejacp_s07_MS]...';
	DROP PROCEDURE [dbo].scejacp_s07_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scejacp_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [scejacp_s07_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.scejacp_s07_MS 
			  @codcct	CHAR(3),
			  @codesp	CHAR(2),
			  @fecpro	datetime 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:02:02 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   select   count(*)
   from 	dbo.sce_jacp
   where 	@codcct = codcct  and
   @codesp = codesp  and
   @fecpro > venacp  and
   salacp > 0        and
	--rollover <> ''S''
	(estacp = 3 or estacp = 4)

   return
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'scejacp_s05_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [scejacp_s05_MS]...';
	DROP PROCEDURE [dbo].scejacp_s05_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scejacp_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [scejacp_s05_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[scejacp_s05_MS] 
	@codcct	CHAR(3),
	@codesp	CHAR(2),
	@fecpro	datetime 
AS
BEGIN
	
	SELECT   
		codcct,
		codesp,
		codpro,
		codofi,
		codope,
		numneg,
		numacp,
		monacp,
		salacp,
		venacp,
		rollover
	FROM 
		dbo.sce_jacp
	WHERE 
		@codcct = codcct	and
		@codesp = codesp	and
		@fecpro >= venacp	and
		salacp > 0			and
		(estacp = 3 or estacp = 4)
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_d01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_swf_pendientes_d01_MS]...';
	DROP PROCEDURE [dbo].pro_sce_swf_pendientes_d01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_swf_pendientes_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [pro_sce_swf_pendientes_d01_MS]...';
	EXEC dbo.sp_executesql @statement = N'create procedure pro_sce_swf_pendientes_d01_MS
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(255)
as
begin
	delete from sce_swf_pendientes
	where  ctecct = @ctecct and codesp = @codesp and archivo = @archivo
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sel_montoOperacionAnulacionDia_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sel_montoOperacionAnulacionDia_MS]...';
	DROP PROCEDURE [dbo].proc_sel_montoOperacionAnulacionDia_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sel_montoOperacionAnulacionDia_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sel_montoOperacionAnulacionDia_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [proc_sel_montoOperacionAnulacionDia_MS] 
	@codcct CHAR(3),
	@codpro CHAR(2),
	@codesp CHAR(2),
	@codofi CHAR(3),
	@codope CHAR(5)
	
AS
BEGIN

	--DECLARE @codcct CHAR(3) SET @codcct = ''753''
	--DECLARE @codpro CHAR(2) SET @codpro = ''20''
	--DECLARE @codesp CHAR(2) SET @codesp = ''29''
	--DECLARE @codofi CHAR(3) SET @codofi = ''000''
	--DECLARE @codope CHAR(5) SET @codope = ''73440''
	-----------------------------------------------------------------------------------------------
	DECLARE @Monto NVARCHAR(MAX) SET @Monto = '''';

	SELECT @Monto = '' -   Monto: ''+ RTRIM(LTRIM(moneda.mnd_mndnmc)) + '' '' + RTRIM(LTRIM(cast(SUM(conta.mtomcd) as nvarchar(max)))) 
	FROM sce_mcd conta 
	INNER JOIN sce_netmnd moneda ON conta.codmon = moneda.mnd_mndcod
	WHERE	conta.codcct = @codcct
	AND conta.codpro = @codpro	AND conta.codesp = @codesp
	AND conta.codofi = @codofi	AND conta.codope = @codope
	AND conta.cod_dh = ''D''
	GROUP BY conta.mtomcd, moneda.mnd_mndnmc

	SELECT @Monto

END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mch_s02_MS]...';
	DROP PROCEDURE [dbo].sce_mch_s02_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mch_s02_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mch_s02_MS] 
	@fecmov         datetime,
	@cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-09   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
   SET NOCOUNT ON 


   select   codcct,
           codpro,
           codesp,
           codofi,
           codope,
           nrorpt,
           fecmov,
           codfun
   from dbo.sce_mch
   where cencos = @cencos
   and codusr = @codusr
   and fecmov = @fecmov
   UNION ALL
   select   codcct,
	        codpro,
			codesp,
			codofi,
			codope,
            nrorpt,
			fecmov,
			codfun
   from dbo.sce_mchh
   where cencos = @cencos
   and codusr = @codusr
   and fecmov = @fecmov
                            
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mch_s03_MS]...';
	DROP PROCEDURE [dbo].sce_mch_s03_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mch_s03_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_mch_s03_MS @codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:34:34 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   select   codcct	,
		codpro	,
		codesp	,
		codofi	,
		codope	,
		nrorpt	,
		fecmov	,
		codfun
   from dbo.sce_mch where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
   union
   select   codcct  ,
		codpro  ,
		codesp  ,
		codofi  ,
		codope  ,
		nrorpt  ,
		fecmov  ,
		codfun
   from dbo.sce_mchh where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope   

   return
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_xdoc_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xdoc_s03_MS]...';
	DROP PROCEDURE [dbo].sce_xdoc_s03_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_xdoc_s03_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_xdoc_s03_MS 
	@fecing      datetime,
	@cencos      CHAR(3),
	@codusr      CHAR(2) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:56:56 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
   select   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		nrocor,
		fecing,
		coddoc
   from 	dbo.sce_xdoc
   where 	fecing = @fecing and
   cencos = @cencos and
   codusr = @codusr

   return
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_xdoc_s04_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xdoc_s04_MS]...';
	DROP PROCEDURE [dbo].sce_xdoc_s04_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_xdoc_s04_MS]...';
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_xdoc_s04_MS
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:18:18 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
   select   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		nrocor,
		fecing,
		coddoc
   from 	dbo.sce_xdoc
   where 	codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

   return

end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sgt_mnd_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sgt_mnd_s02_MS]...';
	DROP PROCEDURE [dbo].sgt_mnd_s02_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_mnd_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sgt_mnd_s02_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_mnd_s02_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   SELECT   
		mnd_mndcod, 
		mnd_mndcbc, 
		mnd_mndnom,
		mnd_mndnmc, 
		mnd_mndswf 
   FROM dbo.sgt_mnd 

END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sgt_pai_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sgt_pai_s02_MS]...';
	DROP PROCEDURE [dbo].sgt_pai_s02_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_pai_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sgt_pai_s02_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_pai_s02_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   SELECT   pai_paicod, pai_painom, pai_paiala 
   FROM dbo.sgt_pai

END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s71_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mcd_s71_MS]...';
	DROP PROCEDURE [dbo].sce_mcd_s71_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s71_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sce_mcd_s71_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_s71_MS] @cencos  CHAR(3),
	  @codusr  CHAR(2),
	  @rutais  CHAR(8),
	  @fecmov  datetime 
	AS
	begin
	-- This procedure was converted on Wed Apr 16 15:34:34 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Su
	--pport, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

	/************************************************************************/
	/******** Obtiene Contabilidad del Día realizada por el Especialista ****/
	/************************************************************************/


	   select   codcct, codpro, codesp, codofi, codope, nrorpt, fecmov, cod_dh, nemcta, numcct, mtomcd ,nemmon,estado
	   from	dbo.sce_mcd
	   where  fecmov = @fecmov
	   and  rutais = @rutais	
		--and  estado <> 9
	   and  nemcta = ''CC$''    	-- Cuenta Corriente M.N.
	   and  enlinea = 1
	   UNION ALL
	   select   codcct, codpro, codesp, codofi, codope, nrorpt, fecmov, cod_dh, nemcta, numcct, mtomcd ,nemmon,estado
	   from	dbo.sce_mcd
	   where fecmov = @fecmov
	   and rutais = @rutais	
	   -- and estado <> 9		
	   and nemcta = ''CCE''   -- Cuenta Corriente M.E.
	   and enlinea = 1
	   return	
	end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_suc_s01_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [pro_sce_suc_s01_MS]...';
	DROP PROCEDURE [dbo].pro_sce_suc_s01_MS 
END


IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_suc_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [pro_sce_suc_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.pro_sce_suc_s01_MS @suc_succod VARCHAR(255) 
	AS
	BEGIN
	-- This procedure was converted on Wed Apr 16 15:25:25 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile 
	-- (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
		SET FMTONLY OFF
	
	   declare @cont    INT,@fin     INT,@sucu    CHAR(3)
	   create table #tmp_suc
	   (
		  succod      NUMERIC(3,0),
		  sucnom      VARCHAR(30)
	   )

	   select   @fin     = LEN(@suc_succod)
	   select   @cont    = 1

	   select   @cont, @fin

	   while (@cont <= @fin)
	   begin
		  select   @sucu = SUBSTRING(@suc_succod,@cont,3)
		  insert #tmp_suc
		  select  suc_succod,
				suc_sucnom
		  from    dbo.sgt_suc
		  where   suc_succod = convert(NUMERIC(3,0),@sucu)
		  select   @cont = @cont+4
	   end

	   select succod as succod, sucnom as sucnom from #tmp_suc
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_mcd_s78_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mcd_s78_MS]...';
	DROP PROCEDURE [dbo].sce_mcd_s78_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_mcd_s78_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_mcd_s78_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_s78_MS]   
  @codcct      CHAR(3),    
  @codpro      CHAR(2),    
  @codesp      CHAR(2),    
  @codofi      CHAR(3),    
  @codope      CHAR(5)  
AS    
	BEGIN    
	   SET NOCOUNT ON   
	   BEGIN    
		 IF EXISTS (SELECT * FROM sce_mcd WHERE codcct = @codcct and codpro = @codpro and codesp = @codesp and codofi = @codofi and codope = @codope)   
		 BEGIN  
		  SELECT   
		   codcct,   
		   codpro,   
		   codesp,   
		   codofi,   
		   codope,   
		   codneg,   
		   codsec,   
		   nrorpt,   
		   fecmov,   
		   cencos,   
		   codusr,   
		   nroimp,   
		   estado,   
		   tipmcd,   
		   idncta,   
		   nemcta,   
		   numcta,   
		   codmon,   
		   nemmon,   
		   mtomcd,   
		   cod_dh,   
		   prtcli,   
		   rutcli,   
		   swibco,   
		   codbco,   
		   numcct,   
		   nroref,   
		   tipcam,   
		   rutais   
		  FROM   
		   sce_mcd   
		  WHERE   
		   codcct = @codcct and   
		   codpro = @codpro and   
		   codesp = @codesp and   
		   codofi = @codofi and   
		   codope = @codope  
	 END  
	 ELSE  
		 BEGIN  
		  SELECT   
		   codcct,   
		   codpro,   
		   codesp,   
		   codofi,   
		   codope,   
		   codneg,   
		   codsec,   
		   nrorpt,   
		   fecmov,   
		   cencos,   
		   codusr,   
		   nroimp,   
		   estado,   
		   tipmcd,   
		   idncta,   
		   nemcta,   
		   numcta,   
		   codmon,   
		   nemmon,   
		   mtomcd,   
		   cod_dh,   
		   prtcli,   
		   rutcli,   
		   swibco,   
		   codbco,   
		   numcct,   
		   nroref,   
		   tipcam,   
		   rutais   
		  FROM   
		   sce_mcdh   
		  WHERE   
		   codcct = @codcct and   
		   codpro = @codpro and   
		   codesp = @codesp and   
		   codofi = @codofi and   
		   codope = @codope  
		 END  
	 END  
END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_mch_s16_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_mch_s16_MS]...';
	DROP PROCEDURE [dbo].sce_mch_s16_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_mch_s16_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_mch_s16_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mch_s16_MS]   
  @codcct      CHAR(3),    
  @codpro      CHAR(2),    
  @codesp      CHAR(2),    
  @codofi      CHAR(3),    
  @codope      CHAR(5)  
AS    
	BEGIN    
	   SET NOCOUNT ON   
	   BEGIN    
		IF EXISTS (SELECT * FROM sce_mch WHERE codcct = @codcct and codpro = @codpro and codesp = @codesp and codofi = @codofi and codope = @codope)   
		BEGIN  
		  SELECT   
		   codcct,   
		   codpro,   
		   codesp,   
		   codofi,   
		   codope,   
		   nrorpt,   
		   fecmov,   
		   cencos,   
		   codusr,   
		   estado,   
		   oficon,   
		   prtcli,   
		   rutcli,   
		   codfun,   
		   operel,   
		   desgen   
		  FROM   
		   sce_mch   
		  WHERE   
		   codcct = @codcct and   
		   codpro = @codpro and   
		   codesp = @codesp and   
		   codofi = @codofi and   
		   codope = @codope  
		 END  
		 ELSE  
		 BEGIN  
		  SELECT   
		   codcct,   
		   codpro,   
		   codesp,   
		   codofi,   
		   codope,   
		   nrorpt,   
		   fecmov,   
		   cencos,   
		   codusr,   
		   estado,   
		   oficon,   
		   prtcli,   
		   rutcli,   
		   codfun,   
		   operel,   
		   desgen   
		  FROM   
		   sce_mchh   
		  WHERE   
		   codcct = @codcct and   
		   codpro = @codpro and   
		   codesp = @codesp and   
		   codofi = @codofi and   
		   codope = @codope  
		 END  
		END  
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_plan_s19_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_plan_s19_MS]...';
	DROP PROCEDURE [dbo].sce_plan_s19_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_plan_s19_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_plan_s19_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_plan_s19_MS]   
  @codcct      CHAR(3),    
  @codpro      CHAR(2),    
  @codesp      CHAR(2),    
  @codofi      CHAR(3),    
  @codope      CHAR(5)  
	AS    
	BEGIN    
	   SET NOCOUNT ON   
	   BEGIN    
		SELECT   
		  cent_costo,   
		  id_product,   
		  id_especia,   
		  id_empresa,   
		  id_cobranz,   
		  num_presen,   
		  cencos,   
		  codusr,   
		  rut,   
		  party,   
		  nomimport,   
		  fechaventa,   
		  num_idi,   
		  fec_idi,   
		  num_dec,   
		  fec_dec,   
		  num_con,   
		  fec_con,   
		  codigo,   
		  codbcch,   
		  cod_plaza,   
		  nombplaza,   
		  forma_pag,   
		  codpais,   
		  nompais,   
		  codmone,   
		  paridad,   
		  tipo_camb,   
		  mtofob,   
		  mtoflete,   
		  mtoseguro,   
		  mtocif,   
		  mtointer,   
		  mtogastos,   
		  mtototal,   
		  cifdolar,   
		  totaldolar,   
		  fechavenc,   
		  hayanula,   
		  indanula,   
		  vencanula,   
		  fechaanula,   
		  paranula,   
		  totalanula,   
		  estado,   
		  observ,   
		  obsdecl,   
		  obscobranz,   
		  numpln_r,   
		  fecpln_r,   
		  codpln_r,   
		  tippln,   
		  hayrpl  
		 FROM   
			sce_plan   
		WHERE   
		  cent_costo = @codcct and   
		  id_especia = @codesp and   
		  id_product = @codpro and   
		  id_cobranz = @codope  
		END  
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_pli_s08_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_pli_s08_MS]...';
	DROP PROCEDURE [dbo].sce_pli_s08_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_pli_s08_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_pli_s08_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_pli_s08_MS]     
  @codcct      CHAR(3),      
  @codpro      CHAR(2),      
  @codesp      CHAR(2),      
  @codofi      CHAR(3),      
  @codope      CHAR(5)    
	AS      
	BEGIN      
		SET NOCOUNT ON     
		BEGIN      
			SELECT     
				numpli,     
				fecpli,     
				cencos,     
				codusr,     
				fecing,     
				fecact,     
				tippln,     
				codcct,     
				codpro,     
				codesp,     
				codofi,     
				codope,     
				codanu,     
				estado,     
				plzbcc,     
				rutcli,     
				prtcli,     
				codoci,     
				codcom,     
				anunum,     
				anufec,     
				motivo,     
				numacu,     
				codpai,     
				codmnd,     
				codmndbc,     
				mtoope,     
				mtopar,     
				mtodol,    
				tipcam,     
				mtonac,     
				numdec,     
				fecdec,     
				numcre,     
				feccre,     
				obspli    
			FROM     
				sce_pli     
			WHERE     
				codcct = @codcct and     
				codusr = @codesp and     
				codpro = @codpro and     
				codope = @codope    
   END    
END '
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_prty_s10_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_prty_s10_MS]...';
	DROP PROCEDURE [dbo].sce_prty_s10_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_prty_s10_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_prty_s10_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_prty_s10_MS]   
  @idparty      CHAR(12),    
  @rutparty      CHAR(10)    
	AS    
		BEGIN    
		   SET NOCOUNT ON   
		   BEGIN    
			 SELECT   
			  id_party,   
			  borrado,   
			  tipo_party,   
			  flag,   
			  clasificac,   
			  tiene_rut,   
			  rut,   
			  crea_costo,   
			  crea_user,   
			  mod_costo,   
			  mod_user,   
			  multiple,   
			  cod_ofieje,   
			  cod_eject,   
			  cod_acteco,   
			  swift,   
			  fecing,   
			  fecact  
			 FROM   
			  sce_prty  
			 WHERE   
			  id_party = @idparty and  
			  rut = @rutparty  
	   END  
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_xanu_s04_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xanu_s04_MS]...';
	DROP PROCEDURE [dbo].sce_xanu_s04_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_xanu_s04_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_xanu_s04_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_xanu_s04_MS]   
  @codcct      CHAR(3),    
  @codpro      CHAR(2),    
  @codesp      CHAR(2),    
  @codofi      CHAR(3),    
  @codope      CHAR(5)  
	AS    
	BEGIN    
		SET NOCOUNT ON   
		BEGIN    
			SELECT   
				numpre,   
				fecpre,   
				cencos,   
				codusr,   
				fecing,   
				estado,   
				codcct,   
				codpro,   
				codesp,   
				codofi,   
				codope,   
				tipanu,   
				plzbcc,   
				rutexp,   
				prtexp,   
				entaut,   
				numpreo,   
				fecpreo,   
				tippln,   
				codpbc,   
				numdec,   
				fecdec,   
				fecven,   
				codmnd,   
				mtodol,   
				mtopar,   
				mtoanu,   
				mtopara,   
				mtodola,   
				mtodolpo,   
				obspln,   
				plnest,   
				tipcam  
			FROM   
				sce_xanu  
			WHERE   
				codcct = @codcct and   
				codesp = @codesp and   
				codpro = @codpro and   
				codope = @codope  
		END  
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_xdoc_s05_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xdoc_s05_MS]...';
	DROP PROCEDURE [dbo].sce_xdoc_s05_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_xdoc_s05_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_xdoc_s05_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_xdoc_s05_MS]     
  @codcct      CHAR(3),      
  @codpro      CHAR(2),      
  @codesp      CHAR(2),      
  @codofi      CHAR(3),      
  @codope      CHAR(5)    
	AS      
	BEGIN      
	   SET NOCOUNT ON     
	   BEGIN      
		 SELECT     
		  codcct,     
		  codpro,     
		  codesp,     
		  codofi,     
		  codope,     
		  nrocor,     
		  cencos,     
		  codusr,     
		  coddoc,     
		  fecing,     
		  xdoc.codmem,    
		  memx.corlin,    
		  memx.linmem    
		 FROM     
		  sce_xdoc xdoc    
		  INNER JOIN sce_memx memx ON xdoc.codmem = memx.codmem    
			WHERE     
			  cencos = @codcct and     
			  codpro = @codpro and     
			  codesp = @codesp and     
			  codofi = @codofi and     
			  codope = @codope    
	 END    
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_xplv_s12_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_xplv_s12_MS]...';
	DROP PROCEDURE [dbo].sce_xplv_s12_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_xplv_s12_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_xplv_s12_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_xplv_s12_MS]   
  @codcct      CHAR(3),    
  @codpro      CHAR(2),    
  @codesp      CHAR(2),    
  @codofi      CHAR(3),    
  @codope      CHAR(5)  
	AS    
	BEGIN    
	   SET NOCOUNT ON   
	   BEGIN    
		  SELECT  
			numpre,   
			fecpre,   
			cencos,   
			codusr,   
			fecing,   
			fecact,   
			tippln,   
			codcct,   
			codpro,   
			codesp,   
			codofi,   
			codope,   
			codanu,   
			estado,   
			numdec,  
			fecven,   
			rutexp,   
			prtexp,   
			codmnd,   
			mtobru,   
			totanu,   
			mtocom,   
			mtoliq,   
			mtopar,   
			mtodol,   
			tipcam,   
			tipcamo,   
			plzbcc,   
			afimnd,   
			afipar,   
			afimto,   
			afiven,   
			diefec,   
			obspln   
		 FROM   
			sce_xplv   
		 WHERE   
		  cencos = @codcct and   
		  codusr = @codesp and   
		  codpro = @codpro and   
		  codope = @codope  
	   END  
	END'
END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'tbl_datos_usuario_s01_MS' and schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [tbl_datos_usuario_s01_MS]...';
	DROP PROCEDURE [dbo].tbl_datos_usuario_s01_MS 
END

IF NOT EXISTS(SELECT * FROM sys.procedures WHERE name = 'tbl_datos_usuario_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [tbl_datos_usuario_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[tbl_datos_usuario_s01_MS]       
  @cencos      CHAR(3),        
  @codusr      CHAR(2)        
AS        
BEGIN        
	SET NOCOUNT ON       
	BEGIN        
		SELECT       
			samAccountName,
			ConfigImpres_ImprimeCartas,
			ConfigImpres_ImprimePlanillas,
			ConfigImpres_ImprimeReporte,
			Configuracion_Sonidos,
			CtaCteLin_ArcHCCL,
			CtaCteLin_ArcLCCL,
			CtaCteLin_ServCCL,
			CtaCteLin_ServSOL,
			CtaCteLin_VistSOL,
			Entry_Usuario,
			Exportaciones_DocSwf,
			Exportaciones_TcpConDec,
			Exportaciones_TcpConvenio,
			Exportaciones_TcpSinPai,
			EXPOTAR_arch_export,
			EXPOTAR_dir_arch_export,
			EXPOTAR_ruta_excel,
			General_MndDol,
			General_MndNac,
			General_MndSinDec,
			General_MontoIVA,
			Identificacion_Alias,
			Identificacion_CCtUsr,
			Identificacion_CCtUsro,
			Identificacion_Impresora,
			Identificacion_Rut,
			Importaciones_TcpAutBcoCen,
			MODGUSR_UsrEsp_CentroCosto_CodBCCH,
			MODGUSR_UsrEsp_CentroCosto_CodBCH,
			MODGUSR_UsrEsp_CentroCosto_CodPBC,
			MODGUSR_UsrEsp_CentroCosto_SucBCH,
			Monedas_CodMonedaDolar,
			Monedas_CodMonedaNacional,
			Oficinas_UsrEsp_CentroCosto,
			Pais_CodPais,
			Participantes_PartyEnRed,
			Participantes_PartyNodo,
			Participantes_PartyServidor,
			SaldosCtaCte_NodoSalME,
			SaldosCtaCte_SerSalCCL,
			SaldosCtaCte_VisSalME,
			SaldosCtaCte_VisSalMN,
			SceIdi_PlazaCentral,
			SgtCCLin_NodoSgt,
			SgtCCLin_ServSgt,
			SgtCCLin_TabSgt,
			SgtCCLin_VisSgt,
			Swift103_BICEMI,
			Swift103_BICREC,
			Swift_23E_Reglas,
			SyBase_Base,
			SyBase_Nodo,
			SyBase_Servidor,
			SyBase_Usuario,
			Ubicacion_Entry,
			WebServices_IP,
			FirmasLocales,
			MinsAlertaEnvioSwift,
			MinsAlertaAutorizacionSwift,
			MinsAlertaAdminEnvioSwift,
			MinsAlertaRecepcionSwift,
			BCHComexSwem_Casillas,
			ConfigImpres_ContabilidadGenerica,
			ConfigImpres_Formato
		FROM       
			tbl_datos_usuario       
		WHERE       
		Identificacion_CCtUsro = @cencos + @codusr       
	END      
END '
END

----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_gpln_s10_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sce_gpln_s10_MS]...';
	DROP PROCEDURE [dbo].[sce_gpln_s10_MS]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_gpln_s10_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [sce_gpln_s10_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_gpln_s10_MS] @cencos CHAR(3),
 @codusr         CHAR(2),      
 @fecing         CHAR(10)       
AS      
BEGIN      
/*       
Historial:      
                         Migración desde Sybase (AKZIO)      
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)      
*/      
    SET NOCOUNT ON      
      
 /* Esto permite que Entity Framework mapee las columnas correctamente */      
 if @codusr is null and @cencos is null and @fecing is null      
 begin      
  select      
 CAST(null as CHAR(3))  AS codcct,      
 CAST(null as CHAR(2))  AS codpro,      
 CAST(null as CHAR(2))  AS codesp,      
 CAST(null as CHAR(3))  AS codofi,      
 CAST(null as CHAR(5))  AS codope,      
 CAST(null as CHAR(7))  AS numpre,      
 CAST(null as datetime) AS fecpre,      
 CAST(null as CHAR(3)) AS cencos,      
 CAST(null as CHAR(2)) AS codusr,      
 CAST(null as VARCHAR(70)) AS nomcli,    
 CAST(null as CHAR(15)) AS operacion,      
 CAST(null as CHAR(12)) AS rutexp,      
 CAST(null as INT) AS indnom,      
 CAST(null as INT) AS tippln,       
 CAST(null as CHAR(10)) AS codcom,    
 CAST(null as INT) AS codmnd,      
 CAST(null as NUMERIC(15,2)) AS mtoliqing,   
 CAST(null as NUMERIC(15,2)) AS mtoliqegr,      
 CAST(null as CHAR(3)) AS tipo,      
 CAST(null as CHAR(1)) AS ingegr,    
 CAST(null as CHAR(15)) AS operel    
  return      
 end      
      
   declare @fecpro datetime      
      
   select   @fecpro = convert(datetime,@fecing,103)      
      
      
   create table #pln      
   (      
  codcct   CHAR(3),    
  codpro   CHAR(2),    
  codesp   CHAR(2),    
  codofi   CHAR(3),    
  codope   CHAR(5),    
  numpre   CHAR(7),      
  fecpre   datetime,      
  cencos   CHAR(3),      
  codusr   CHAR(2),      
  nomcli   VARCHAR(70),    
  operacion CHAR(15),      
  rutexp   CHAR(12),      
  indnom   INT,      
  tippln   INT,      
  codcom   CHAR(10),    
  codmnd   INT,      
  mtoliqing   NUMERIC(15,2),      
  mtoliqegr   NUMERIC(15,2),    
  tipo     CHAR(3),      
  ingegr   CHAR(1),    
  operel   CHAR(15)    
   )                                     
      
   If @codusr <> ''99''        --Especialista.-      
   begin      
 -- PLANILLAS VISIBLES de EXPORTACIONES.-      
      
      Insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecing,      
  cencos,      
  codusr,    
  prtexp nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,      
  indnom,      
  1 tippln,     
  '''' codcom,     
  codmnd,      
  mtoliq mtoliqing, 
  mtoliq mtoliqegr,
  ''VIS'' tipo,      
  ''I'' ingegr,    
  '''' operel    
      from dbo.sce_xplv  xplv    
      where cencos = @cencos      
    and codusr = @codusr      
    and fecing >=  dateadd(dd,0,@fecpro)      
    and fecing < dateadd(dd,+1,@fecpro)      
    and estado <> 9      
    and plnest = 0      
      
    
       
 -- PLANILLAS DE TRANSFERENCIA DE INGRESO DE EXPORTACIONES       
      Insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecing,      
  cencos,      
  codusr,    
  prtexp nomcli,      
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,      
  indnom,      
  8 tippln,    
  '''' codcom,      
  codmnd,      
  mtoliq mtoliqing, 
  mtoliq mtoliqegr,      
  ''TRN'' tipo,      
  ''I'' ingegr,      
  '''' operel    
      from dbo.sce_xplv xplv    
      where cencos = @cencos      
    and codusr = @codusr      
    and fecing >= dateadd(dd,0,@fecpro)      
    and fecing < dateadd(dd,+1,@fecpro)      
    and estado <> 9      
    and plnest = 1      
       
      
       
 -- PLANILLAS TRANSFERENCIAS INVISIBLES.-      
      Insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpli,      
  fecpli,      
  cencos,      
  codusr,      
  prtcli nomcli,    
  codcct+pli.codpro+pli.codesp+pli.codofi+pli.codope operacion ,      
  rutcli,      
  indnom,      
  tippln,     
  codcom,     
  codmnd,      
  mtoope mtoliqing, 
  mtoope mtoliqegr,      
  ''TRN'' tipo,      
  '''' ingegr,      
  '''' operel       
      from dbo.sce_pli pli    
      where    
  cencos  =  @cencos      
  and codusr  =  @codusr      
  and fecing  >= dateadd(dd,0,@fecpro)      
  and fecing  < dateadd(dd,+1,@fecpro)      
  and estado  <> 9      
  and tippln  >  5      
    
    
 -- PLANILLAS INVISIBLES POSICION      
      Insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpli,      
  fecpli,      
  cencos,      
  codusr,      
  prtcli nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutcli,      
  indnom,      
  tippln,      
  codcom,    
  codmnd,      
  mtoope mtoliqing, 
  mtoope mtoliqegr,          
  ''INV'' tipo,      
  '''' ingegr,      
  '''' operel       
      from dbo.sce_pli pli    
      where    
  cencos =  @cencos      
  and codusr =  @codusr      
  and fecing >= dateadd(dd,0,@fecpro)      
  and fecing < dateadd(dd,+1,@fecpro)      
  and estado <> 9      
  and tippln <  5      
    
    
 -- PLANILLAS DE ANULACION EXPORTACIONES      
      insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecpre,      
  cencos,      
  codusr,      
  prtexp nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,      
  indnom,      
  3 tippln,      
  '''' codcom,    
  codmnd,      
  mtodolpo mtoliqing, 
  mtodolpo mtoliqegr,         
  ''ANU'' tipo,      
  ''E'' ingegr,      
  '''' operel      
      from dbo.sce_xanu  xanu    
      where   
    cencos =  @cencos      
    and codusr =  @codusr      
    and fecing >= dateadd(dd,0,@fecpro)      
    and fecing < dateadd(dd,+1,@fecpro)      
    and estado <> 9      
    and plnest =  0      
      
    
 -- PLANILLAS DE ANULACION DE TRANSFERENCIAS DE INGRESO EXP.      
      insert #pln      
      Select     
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecpre,      
  cencos,      
  codusr,      
  prtexp nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,    
  indnom,      
  10 tippln,      
  '''' codcom,    
  codmnd,      
  mtodolpo mtoliqing, 
  mtodolpo mtoliqegr,         
  ''TRN'' tipo,      
  ''E'' ingegr,      
  '''' operel      
      from dbo.sce_xanu  xanu    
      where   
    cencos =  @cencos      
    and  codusr =  @codusr      
    and  fecing >= dateadd(dd,0,@fecpro)      
    and  fecing < dateadd(dd,+1,@fecpro)      
    and  plnest =  1      
    and  estado <> 9      
    
    
        
 --PLANILLAS VISIBLES DE IMPORT.      
      insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  cencos,      
  codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  2 tippln ,    
  codigo codcom,      
  codmone codmnd,      
  mtototal mtoliqing, 
  mtototal mtoliqegr,         
  ''VIS'' tipo,      
  ''E'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where     
  cencos     =  @cencos      
  and  codusr     =  @codusr      
  and  estado     <  8      
--  and  estado     <> 8      
  and  indanula   =  0      
  and  hayanula   =  0    
  and  fechaventa >=  dateadd(dd,0,@fecpro)      
  and  fechaventa < dateadd(dd,+1,@fecpro)       
        
       
    
 --PLANILLAS REEMPLAZO VISIBLES DE IMPORT.      
      insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  5 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing, 
  mtototal mtoliqegr,      
  ''VIS'' tipo,      
  ''E'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where     
  cencos      =  @cencos      
  and codusr      =  @codusr      
  and estado     <  8      
 --  and estado     <> 8      
  and indanula   =  2      
  and hayanula   =  0       
  and fechaventa >=  dateadd(dd,0,@fecpro)      
  and fechaventa < dateadd(dd,+1,@fecpro)     
      
    
    
 -- PLANILLAS DE TRANSFERENCIA DE EGRESO IMPOR.        
      insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  9 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing, 
  mtototal mtoliqegr,    
  ''TRN'' tipo,      
  ''E'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where    
  cencos     =  @cencos      
  and  codusr     =  @codusr      
  and  estado     <  8      
--  and  estado     <> 8      
  and  indanula   =  3      
  and  hayanula   =  0      
  and fechaventa >=  dateadd(dd,0,@fecpro)      
  and fechaventa < dateadd(dd,+1,@fecpro)     
    
    
    
 -- PLANILLAS ANULACION VISIBLES IMPORT.      
      insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  cencos,      
  codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  4 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing, 
  mtototal mtoliqegr,   
  ''VIS'' tipo,      
  ''I'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where     
  cencos     =  @cencos      
  and codusr     =  @codusr      
  and estado     <  8       
--      and estado     <> 8       
  and hayanula   =  1      
  and indanula   =  1      
  and fechaventa >=  dateadd(dd,0,@fecpro)      
  and fechaventa < dateadd(dd,+1,@fecpro)     
      
    
    
 -- PLANILLAS ANULACION TRANSFERENCIA EGRESO VISIBLES IMPORT.      
      insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  cencos,      
  codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  11 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing, 
  mtototal mtoliqegr,      
  ''TRN'' tipo,      
  ''I'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where     
  cencos      =  @cencos      
  and codusr  =  @codusr      
  and estado  <  8      
--      and estado     <> 8      
  and hayanula   =  1      
  and indanula   =  4      
  and fechaventa >=  dateadd(dd,0,@fecpro)      
  and fechaventa < dateadd(dd,+1,@fecpro)     
        
   update #pln set ingegr = ''I''  where  tippln in(1,4,8,11)      
      update #pln set ingegr = ''E''  where  tippln in(2,3,5,9,10)      
    
   IF  @fecpro = CONVERT(date, getdate()) -- Comparamos con la fecha de hoy para saber si consultar las operaciones relacionadas a mch o mchh      
      Select       
  pln.codcct,    
  pln.codpro,    
  pln.codesp,    
  pln.codofi,    
  pln.codope,    
  pln.numpre,      
  pln.fecpre,      
  pln.cencos,      
  pln.codusr,      
  UPPER(min(rsa.razon_soci)) nomcli,    
  pln.operacion,      
  pln.rutexp,      
  pln.indnom,      
  pln.tippln,    
  pln.codcom,      
  pln.codmnd,      
   mtoliqing = 
   case ingegr
     when ''I'' then pln.mtoliqing
	 else 0.00
	 end,
  mtoliqegr =
   case ingegr
     when ''E'' then pln.mtoliqegr      
	 else 0.00
	 end,     
  pln.tipo,      
  pln.ingegr,    
  isnull(mch.operel, '''') operel      
     from #pln pln    
		left join dbo.sce_rsa rsa     
			on rsa.id_party = pln.nomcli    
				and rsa.borrado = 0 
		left join sce_mch mch     
			on (pln.codcct + pln.codpro + pln.codesp + pln.codofi + pln.codope) = (mch.codcct + mch.codpro + mch.codesp + mch.codofi + mch.codope) and mch.fecmov = dateadd(dd,0,@fecpro)      
 group by pln.codcct, pln.codpro, pln.codesp, pln.codofi, pln.codope, codcom, numpre, fecpre, pln.cencos, pln.codusr, pln.operacion, pln.numpre, pln.fecpre, pln.cencos, pln.codusr, pln.operacion, pln.rutexp, pln.indnom, pln.tippln, pln.codmnd, pln.ingegr, pln.mtoliqing, pln.mtoliqegr, pln.tipo, mch.operel

   ELSE -- Hacemos join a mchh    
   Select       
  pln.codcct,    
  pln.codpro,    
  pln.codesp,    
  pln.codofi,    
  pln.codope,    
  pln.numpre,      
  pln.fecpre,      
  pln.cencos,      
  pln.codusr,      
  UPPER(min(rsa.razon_soci)) nomcli,   
  pln.operacion,      
  pln.rutexp,      
  pln.indnom,      
  pln.tippln,    
  pln.codcom,      
  pln.codmnd,      
   mtoliqing = 
   case ingegr
     when ''I'' then mtoliqing
	 else 0.00
	 end,
  mtoliqegr =
   case ingegr
     when ''E'' then mtoliqegr      
	 else 0.00
	 end,    
  pln.tipo,      
  pln.ingegr,    
  isnull(mchh.operel, '''') operel      
 from #pln pln    
 left join dbo.sce_rsa rsa     
			on rsa.id_party = pln.nomcli    
				and rsa.borrado = 0 
  left join sce_mchh mchh     
 on (pln.codcct + pln.codpro + pln.codesp + pln.codofi + pln.codope) = (mchh.codcct + mchh.codpro + mchh.codesp + mchh.codofi + mchh.codope) and mchh.fecmov = dateadd(dd,0,@fecpro)      
group by pln.codcct, pln.codpro, pln.codesp, pln.codofi, pln.codope, codcom, numpre, fecpre, pln.cencos, pln.codusr, pln.operacion, pln.numpre, pln.fecpre, pln.cencos, pln.codusr, pln.operacion, pln.rutexp, pln.indnom, pln.tippln, pln.codmnd, pln.ingegr, pln.mtoliqing, pln.mtoliqegr, pln.tipo, mchh.operel
        
   end      
      
   If @codusr = ''99''        --Secci>n.-      
   begin      
        
 -- PLANILLAS VISIBLES de EXPORTACIONES.-      
      insert #pln      
      Select     
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecing,      
  cencos,      
  codusr,      
  prtexp nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,      
  indnom,      
  1 tippln,     
  '''' codcom,     
  codmnd,      
  mtoliq mtoliqing,      
  mtoliq mtoliqegr,
  ''VIS'' tipo,      
  ''I'' ingegr,      
  '''' operel      
      from dbo.sce_xplv xplv    
      where cencos     =  @cencos      
    and codusr     < ''99''      
    and fecing >=  dateadd(dd,0,@fecpro)  
    and fecing < dateadd(dd,+1,@fecpro)      
    and estado     <> 9      
    and plnest     =  0      
       
    
    
 -- PLANILLAS DE TRANSFERENCIA DE INGRESO DE EXPORTACIONES       
      Insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecing,      
  cencos,      
  codusr,      
  prtexp nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,      
  indnom,      
  8 tippln,      
  '''' codcom,    
  codmnd,      
  mtoliq mtoliqing,      
  mtoliq mtoliqegr,      
  ''TRN'' tipo,      
  ''I'' ingegr ,      
  '''' operel     
      from dbo.sce_xplv xplv    
      where cencos =  @cencos      
    and codusr < ''99''      
    and fecing >= dateadd(dd,0,@fecpro)      
    and fecing < dateadd(dd,+1,@fecpro)      
    and estado <> 9      
    and plnest =  1      
            
    
    
-- PLANILLAS TRANSFERENCIAS INVISIBLES.-      
    Insert #pln      
    Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpli,      
  fecpli,      
  cencos,      
  codusr,      
  prtcli nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutcli,      
  indnom,      
  tippln,      
  codcom,    
  codmnd,      
  mtoope mtoliqing,      
  mtoope mtoliqegr,      
  ''TRN'' tipo,      
  ''''ingegr,      
  '''' operel          
    from dbo.sce_pli pli    
    where     
  cencos =  @cencos      
  and codusr <  ''99''      
  and fecing >= dateadd(dd,0,@fecpro)      
  and fecing < dateadd(dd,+1,@fecpro)      
  and estado <> 9      
  and tippln >  5     
        
    
       
 -- PLANILLAS INVISIBLES POSICION      
      Insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpli,      
  fecpli,      
  cencos,      
  codusr,      
  prtcli nomcli,    
  codcct+codpro+codesp+codofi+codope operacion,      
  rutcli,      
  indnom,      
  tippln,    
  codcom,      
  codmnd,      
  mtoope mtoliqing,      
  mtoope mtoliqegr,
  ''INV'' tipo,      
  '''' ingegr ,      
  '''' operel        
      from dbo.sce_pli pli    
      where     
  pli.cencos  =  @cencos      
  and pli.codusr  <  ''99''      
  and fecing  >= dateadd(dd,0,@fecpro)      
  and fecing  < dateadd(dd,+1,@fecpro)      
  and pli.estado  <> 9      
  and tippln  <  5      
    
    
    
-- PLANILLAS DE ANULACION EXPORTACIONES      
 insert #pln      
   Select      
   codcct codcct,    
   codpro codpro,    
   codesp codesp,    
   codofi codofi,    
   codope codope,    
   numpre,      
   fecpre,      
   cencos,      
   codusr,      
   xanu.prtexp nomcli,    
   codcct+codpro+codesp+codofi+codope operacion ,      
   rutexp,      
   indnom,      
   3 tippln,      
   '''' codcom,    
   codmnd,      
   mtodolpo mtoliqing,      
   mtodolpo mtoliqegr,      
   ''ANU'' tipo,      
   ''E'' ingegr,      
   '''' operel      
  from dbo.sce_xanu  xanu    
  where cencos =  @cencos      
   and codusr <  ''99''      
   and fecing =  @fecpro      
   and estado <> 9      
   and plnest =  0      
        
    
       
-- PLANILLAS DE ANULACION DE TRANSFERENCIAS DE INGRESO EXP.      
      insert #pln      
      Select      
  codcct codcct,    
  codpro codpro,    
  codesp codesp,    
  codofi codofi,    
  codope codope,    
  numpre,      
  fecpre,      
  cencos,      
  codusr,      
  xanu.prtexp nomcli,    
  codcct+codpro+codesp+codofi+codope operacion ,      
  rutexp,      
  indnom,      
  10 tippln,      
  '''' codcom,    
  codmnd,      
  mtodolpo mtoliqing,      
  mtodolpo mtoliqegr,  
  ''TRN'' tipo,      
  ''E'' ingegr,      
  '''' operel      
       from dbo.sce_xanu  xanu    
      where cencos  =  @cencos     
    and codusr  <  ''99''      
    and fecing  = @fecpro      
    and estado  <> 9      
    and plnest  =  1      
          
    
    
 --PLANILLAS VISIBLES DE IMPORT.      
      insert #pln      
      select     
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  2 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing,      
  mtototal mtoliqegr,      
  ''VIS'' tipo,      
  ''E'' ingegr,      
  '''' operel         
      from dbo.sce_plan pla    
      where     
  pla.cencos      =  @cencos      
  and pla.estado      <  8      
--  and estado      <> 8      
  and indanula = 0      
  and hayanula = 0      
  and fechaventa >=  dateadd(dd,0,@fecpro)      
  and fechaventa < dateadd(dd,+1,@fecpro)     
      UNION ALL      
       
    
    
 --PLANILLAS REEMPLAZO VISIBLES DE IMPORT.      
-- insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  5 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing,      
  mtototal mtoliqegr,        
  ''VIS'' tipo,      
  ''E'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where     
  pla.cencos    =  @cencos      
  and pla.estado     <  8       
 --  and estado      <> 8       
  and indanula    =  2      
  and hayanula    =  0      
  and fechaventa >=  dateadd(dd,0,@fecpro)      
  and fechaventa < dateadd(dd,+1,@fecpro)     
      UNION ALL      
      
    
    
 -- PLANILLAS DE TRANSFERENCIA DE EGRESO IMPOR.        
-- insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  9 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing,      
  mtototal mtoliqegr,     
  ''TRN'' tipo,      
  ''E'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
  left join dbo.sce_rsa rsa     
   on rsa.id_party = pla.party    
    and rsa.borrado = 0     
      where     
    pla.cencos      =  @cencos      
    and pla.estado      <  8      
 --    and estado      <> 8      
    and indanula    =  3      
    and hayanula    =  0      
    and fechaventa >=  dateadd(dd,0,@fecpro)      
    and fechaventa < dateadd(dd,+1,@fecpro)     
      UNION ALL      
         
    
    
 -- PLANILLAS ANULACION VISIBLES IMPORT.      
-- insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  4 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing,      
  mtototal mtoliqegr,        
  ''VIS'' tipo,      
  ''I'' ingegr,      
  '''' operel       
      from dbo.sce_plan pla    
      where     
    pla.cencos      =  @cencos      
    and pla.estado      <  8      
 --    and estado      <> 8      
    and hayanula    =  1      
    and indanula    =  1      
    and fechaventa >=  dateadd(dd,0,@fecpro)      
          and fechaventa < dateadd(dd,+1,@fecpro)     
      UNION ALL      
        
    
      
-- PLANILLAS ANULACION TRANSFERENCIA EGRESO VISIBLES IMPORT.      
-- insert #pln      
      select      
  cent_costo codcct,    
  id_product codpro,    
  id_especia codesp,    
  id_empresa codofi,    
  id_cobranz codope,    
  convert(CHAR(7),num_presen) numpre,      
  fechaventa fecpre,      
  pla.cencos,      
  pla.codusr,      
  pla.party nomcli,    
  cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,      
  rut rutexp,      
  0 indnom,      
  11 tippln ,      
  codigo codcom,    
  codmone codmnd,      
  mtototal mtoliqing,      
  mtototal mtoliqegr,        
  ''TRN'' tipo,      
  ''I'' ingegr,      
  '''' operel        
      from dbo.sce_plan pla    
      where      
    pla.cencos      = @cencos      
    and pla.estado      < 8      
 --    and estado      <> 8      
    and hayanula    =  1      
    and indanula    =  4      
    and fechaventa >=  dateadd(dd,0,@fecpro)      
    and fechaventa < dateadd(dd,+1,@fecpro)     
      
 update #pln set ingegr = ''I''  where  tippln in(1,4,8,11)      
 update #pln set ingegr = ''E''  where  tippln in(2,3,5,9,10)      
    
   IF  @fecpro = CONVERT(date, getdate()) -- Comparamos con la fecha de hoy para saber si consultar las operaciones relacionadas a mch o mchh      
      Select     
  pln.codcct,    
  pln.codpro,    
  pln.codesp,    
  pln.codofi,    
  pln.codope,    
  numpre,      
  fecpre,      
  pln.cencos,      
  pln.codusr,      
  UPPER(min(rsa.razon_soci)) nomcli,
  operacion,      
  rutexp,      
  indnom,      
  tippln,      
  codcom,    
  codmnd,      
   mtoliqing = 
   case ingegr
     when ''I'' then mtoliqing
	 else 0.00
	 end,
  mtoliqegr =
   case ingegr
     when ''E'' then mtoliqegr      
	 else 0.00
	 end,
  tipo,      
  ingegr,      
  isnull(mch.operel, '''') operel        
      from #pln pln  
	  left join dbo.sce_rsa rsa     
   on rsa.id_party = pln.nomcli    
    and rsa.borrado = 0   
  left join sce_mch mch     
   on (pln.codcct + pln.codpro + pln.codesp + pln.codofi + pln.codope) = (mch.codcct + mch.codpro + mch.codesp + mch.codofi + mch.codope) and mch.fecmov = dateadd(dd,0,@fecpro)    
   group by pln.codcct, pln.codpro, pln.codesp, pln.codofi, pln.codope, codcom, numpre, fecpre, pln.cencos, pln.codusr, pln.operacion, pln.numpre, pln.fecpre, pln.cencos, pln.codusr, pln.operacion, pln.rutexp, pln.indnom, pln.tippln, pln.codmnd, pln.ingegr, pln.mtoliqing, pln.mtoliqegr, pln.tipo, mch.operel       
    
   ELSE -- Hacemos join a mchh    
 Select       
  pln.codcct,    
  pln.codpro,    
  pln.codesp,    
  pln.codofi,    
  pln.codope,    
  numpre,      
  fecpre,      
  pln.cencos,      
  pln.codusr,      
  UPPER(min(rsa.razon_soci)) nomcli,
  operacion,      
  rutexp,      
  indnom,      
  tippln,      
  codcom,    
  codmnd,      
  mtoliqing = 
   case ingegr
     when ''I'' then mtoliqing
	 else 0.00
	 end,
  mtoliqegr =
   case ingegr
     when ''E'' then mtoliqegr      
	 else 0.00
	 end,
  tipo,      
  ingegr,      
  isnull(mchh.operel, '''') operel        
      from #pln pln    
		left join dbo.sce_rsa rsa     
			on rsa.id_party = pln.nomcli 
				and rsa.borrado = 0 
		left join sce_mchh mchh     
			on (pln.codcct + pln.codpro + pln.codesp + pln.codofi + pln.codope) = (mchh.codcct + mchh.codpro + mchh.codesp + mchh.codofi + mchh.codope) and mchh.fecmov = dateadd(dd,0,@fecpro)     
   group by pln.codcct, pln.codpro, pln.codesp, pln.codofi, pln.codope, codcom, numpre, fecpre, pln.cencos, pln.codusr, pln.operacion, pln.numpre, pln.fecpre, pln.cencos, pln.codusr, pln.operacion, pln.rutexp, pln.indnom, pln.tippln, pln.codmnd, pln.ingegr, pln.mtoliqing, pln.mtoliqegr, pln.tipo, mchh.operel  
   end      
end 
'
END
-----------------------------------------------------------------------------------------------------------------
END TRY
BEGIN CATCH
	-- Si ocurre un error en la ejecución, no aplica cambios
    SELECT 
        ERROR_NUMBER() AS ErrorNumber
        ,ERROR_SEVERITY() AS ErrorSeverity
        ,ERROR_STATE() AS ErrorState
        ,ERROR_PROCEDURE() AS ErrorProcedure
        ,ERROR_LINE() AS ErrorLine
        ,ERROR_MESSAGE() AS ErrorMessage;

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
END CATCH;

IF @@TRANCOUNT > 0
    COMMIT TRANSACTION;
GO
-----------------------------------------------------------------------------------------------------------------
