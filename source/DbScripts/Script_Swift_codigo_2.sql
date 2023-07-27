USE [swift]

BEGIN TRY

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_version_codigo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_version_codigo_MS]...';
	DROP PROCEDURE [dbo].proc_sw_version_codigo_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_version_codigo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [proc_sw_version_codigo_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_version_codigo_MS]  AS
	BEGIN
		/*replace-begin*/select ''1.50.2''/*replace-end*/ + ''.2''
	END'
END
--------------------------------------------------------------------------------------------------------------------
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_nul_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Eliminado proc_sw_env_graba_nul_MS...'
	DROP PROCEDURE [dbo].[proc_sw_env_graba_nul_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_nul_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creado proc_sw_env_graba_nul_MS...'
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_nul_MS] 
		@p_casilla 	INT, 
		@p_id_mensaje 	INT, 
		@p_rut_log 	INT, 
		@p_comentario 	CHAR(80) 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:45:45 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@fecha_actual   datetime,@opcion_log 	CHAR(8)
	   
	begin tran

	   SELECT   @estado_actual = estado_msg, @casilla_actual = casilla
	   FROM 	dbo.sw_msgsend
	   WHERE 	id_mensaje = @p_id_mensaje

	   if @estado_actual = ''PRO'' or @estado_actual = ''ENV''
	   begin
		  select   @estado_actual
	   end
	else
	   begin	 
		------------------------------------------------------------------------------------------------------------------
		-- SI EXISTEN FIRMAS AUTORIZADAS, NO ES POSIBLE ANULAR EL SWIFT
		IF exists(select estado_firma from dbo.sw_msgsend_firma where id_mensaje = @p_id_mensaje and estado_firma = ''F'' AND 
			(@estado_actual = ''AUM'' or @estado_actual = ''AUT'' or @estado_actual = ''ENV'' or @estado_actual = ''NUL'' or @estado_actual = ''PRO''))
		begin
			rollback
			RETURN -4
		end
		------------------------------------------------------------------------------------------------------------------

		  ------------------------------------------------------------------------------------------------------------------
		  select   @fecha_actual = GetDate()
		  update dbo.sw_msgsend set estado_msg = ''NUL'',comentario = ''Mensaje Anulado''  WHERE 	(id_mensaje = @p_id_mensaje)
		  if (@@error <> 0) or (@@rowcount = 0)
		  begin
			 rollback 
			 return -1
		  end
		  IF exists(SELECT TOP 1 1 from dbo.sw_msgsend_add where id_mensaje = @p_id_mensaje)
		  begin
			 update dbo.sw_msgsend_add set fecha_anula = @fecha_actual,unidad_anula = @p_casilla,rut_anula = @p_rut_log,
			 texto_anula = @p_comentario  WHERE 	(id_mensaje = @p_id_mensaje)
		  end
	   ELSE
		  begin
			 INSERT INTO dbo.sw_msgsend_add(id_mensaje, fecha_anula, rut_anula, unidad_anula,
			  texto_anula, veces_modifica, veces_rechazo, veces_bloqueo)
			VALUES(@p_id_mensaje, @fecha_actual, @p_rut_log, @p_casilla,
			  @p_comentario, 0, 0, 0)
		  end
		  if (@@error <> 0) or (@@rowcount = 0)
		  begin
			 ROLLBACK 
			 return -2
		  end
		  select   @opcion_log = lower(@estado_actual)+''grnul''
		  INSERT INTO dbo.sw_msgsend_log
		VALUES(@p_id_mensaje, @fecha_actual,
			 @p_rut_log, SUBSTRING(@@servername,0,11),
				 ''GS24'', @opcion_log,
			 @casilla_actual, @estado_actual,
			 @casilla_actual, ''NUL'',
			 @p_casilla, ''A'', ''Mensaje Anulado'')
	
		  if (@@error <> 0) or (@@rowcount = 0)
		  begin
			 ROLLBACK 
			 return -3
		  end
		  SELECT ''NUL''
	   end

	   COMMIT tran
	END'
	PRINT 'Creado exitosa'
END
-----------------------------------------------------------------------------------------------------------------
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_validaFirmaAnular_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Eliminado proc_sw_validaFirmaAnular_MS...'
	DROP PROCEDURE [dbo].proc_sw_validaFirmaAnular_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_validaFirmaAnular_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando proc_sw_validaFirmaAnular_MS...'
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_validaFirmaAnular_MS] 
	@p_id_mensaje INT
AS

DECLARE @estado_actual  CHAR(3)
------------------------------------------------------------------------------------------------------------------
SELECT   @estado_actual = estado_msg
FROM 	dbo.sw_msgsend
WHERE 	id_mensaje = @p_id_mensaje
------------------------------------------------------------------------------------------------------------------
-- SI EXISTEN FIRMAS AUTORIZADAS, NO ES POSIBLE ANULAR EL SWIFT
IF exists(select estado_firma from dbo.sw_msgsend_firma where id_mensaje = @p_id_mensaje and estado_firma = ''F'' AND 
	(@estado_actual = ''AUM'' or @estado_actual = ''AUT'' or @estado_actual = ''ENV'' or @estado_actual = ''NUL'' or @estado_actual = ''PRO''))
	SELECT 1 -- Falso
ELSE
	SELECT 0 -- Verdadero
------------------------------------------------------------------------------------------------------------------'
PRINT 'Creado exitosa'
END

-----------------------------------------------------------------------------------------------------------------
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_enc_rango_MS]') AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminado proc_sw_log_trae_enc_rango_MS...'
	DROP PROCEDURE [dbo].[proc_sw_log_trae_enc_rango_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_enc_rango_MS]') AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creado proc_sw_log_trae_enc_rango_MS...'
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_log_trae_enc_rango_MS] 
	 @p_casilla  INT,      
	  @p_fecha1  datetime,      
	  @p_fecha2  datetime       
	 AS      
	 BEGIN      
	 /*       
	 Historial:      
	        Migración desde Sybase (AKZIO)      
	    2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)      
	 */      
	   SET NOCOUNT ON       
	      
	    if @p_casilla = 0      
	    begin      
	    SELECT DISTINCT  m.sesion as sesion, m.secuencia as secuencia,      
	   m.tipo_msg as tipo_msg, m.prioridad as prioridad,      
	   m.estado_msg as estado_msg,      
	   convert(CHAR(10),m.fecha_send,103) as fecha1,      
	   convert(CHAR(8),m.fecha_send,108) as hora1,      
	   convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) as fecha_pro,      
	   convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) as hora_pro,      
	   isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,      
	   isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,      
	   isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,      
	   isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,      
	   isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,      
	   isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora      
	    FROM  dbo.sw_mensajes_add a,  dbo.sw_mensajes m, dbo.sw_log_msg l      
	    WHERE   a.fecha_reenvio is not null      
	    and   a.fecha_reenvio >=  dateadd(dd,0,@p_fecha1)      
	    and   a.fecha_reenvio <   dateadd(dd,+1,@p_fecha2)      
	    and   a.sesion        =  m.sesion      
	    and a.secuencia     =  m.secuencia      
	    and a.send_recv     =  m.send_recv      
	    and   m.send_recv     =  ''R''      
	    and m.estado_msg    in(''ENC'',''IMP'',''CNF'',''REE'')      
	    and   m.sesion        =  l.sesion_log      
	    and m.secuencia     =  l.secuencia_log      
	    and opcion_log      in(''sengrenc'',''sengrree'')      
	    UNION ALL      
	    select DISTINCT m.sesion, m.secuencia,      
	   m.tipo_msg, m.prioridad,      
	   m.estado_msg,      
	   convert(CHAR(10),m.fecha_send,103) as fecha1,      
	   convert(CHAR(8),m.fecha_send,108) as hora1,      
	   convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) fecha_pro,      
	   convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) hora_pro,      
	   isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,      
	   isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,      
	   isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,      
	   isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,      
	   isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,      
	   isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora      
	   FROM  dbo.sw_mensajes m, dbo.sw_mensajes_add a, dbo.sw_log_msg l WHERE  m.fecha_ack     >= dateadd(dd,0,@p_fecha1)      
	    and  m.fecha_ack     <  dateadd(dd,+1,@p_fecha2)      
	    and  m.send_recv     =  ''R''      
	    and  m.estado_msg    in(''ENC'',''IMP'',''CNF'',''REE'')      
	    and  m.sesion        =  a.sesion      
	    and  m.secuencia     =  a.secuencia      
	    and  m.send_recv     =  a.send_recv      
	    and  a.fecha_reenvio is null      
	    and  m.sesion        =  l.sesion_log      
	    and  m.secuencia     =  l.secuencia_log      
	    and  opcion_log      in(''sengrenc'',''sengrree'')      
	    order by fecha_pro,hora_pro      
	    end      
	 else      
	    begin      
	    SELECT   m.sesion, m.secuencia,      
	   m.tipo_msg, m.prioridad,      
	   m.estado_msg,      
	   convert(CHAR(10),m.fecha_send,103) as fecha1,      
	   convert(CHAR(8),m.fecha_send,108) as hora1,      
	   convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) fecha_pro,      
	   convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) hora_pro,      
	   isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,      
	   isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,      
	   isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,      
	   isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,      
	   isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,      
	   isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora      
	    FROM  dbo.sw_mensajes_add a, dbo.sw_mensajes m,  dbo.sw_log_msg l      
	   WHERE  a.fecha_reenvio   is not null      
	    and   a.fecha_reenvio  >=  dateadd(dd,0,@p_fecha1)      
	    and a.fecha_reenvio  <  dateadd(dd,+1,@p_fecha2)      
	    and   a.sesion         =  m.sesion      
	    and a.secuencia      =  m.secuencia      
	    and a.send_recv      =  m.send_recv      
	    and   m.send_recv      =  ''R''      
	    and   m.estado_msg     in(''ENC'',''IMP'',''CNF'',''REE'')      
	    and   m.casilla        =  @p_casilla      
	    and m.secuencia      =  l.secuencia_log      
	    and   m.sesion         =  l.sesion_log      
	    and   l.opcion_log     in(''sengrenc'',''sengrree'')      
	    UNION ALL      
	    SELECT   m.sesion, m.secuencia,      
	   m.tipo_msg, m.prioridad,      
	   m.estado_msg,      
	   convert(CHAR(10),m.fecha_send,103) as fecha1,      
	   convert(CHAR(8),m.fecha_send,108) as hora1,      
	   convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) fecha_pro,      
	   convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) hora_pro,      
	   isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,      
	   isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,      
	   isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,      
	   isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,      
	   isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,      
	   isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora      
	    FROM  dbo.sw_mensajes m, dbo.sw_mensajes_add a, dbo.sw_log_msg l      
	    WHERE  m.fecha_ack      >= dateadd(dd,0,@p_fecha1)      
	    and   m.fecha_ack      <  dateadd(dd,+1,@p_fecha2)      
	    and   m.send_recv      =  ''R''      
	    and   m.estado_msg     in(''ENC'',''IMP'',''CNF'',''REE'')      
	    and   m.casilla        =  @p_casilla      
	    and   m.sesion         =  a.sesion      
	    and m.secuencia      =  a.secuencia      
	    and m.send_recv      =  a.send_recv      
	    and   a.fecha_reenvio  is null      
	    and m.secuencia      =  l.secuencia_log      
	    and   m.sesion         =  l.sesion_log      
	    and   l.opcion_log     in(''sengrenc'',''sengrree'')      
	    order by fecha_pro,hora_pro      
	    end      
	 END'
	PRINT 'Creado exitosamente'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_noaut_rango_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_trae_noaut_rango_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_trae_noaut_rango_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_noaut_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_trae_noaut_rango_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_trae_noaut_rango_MS 
	@p_casilla INT,
	@p_fecha1 datetime,
	@p_fecha2 datetime 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:48:48 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   CREATE table #ing_noaut_rango
   (
      id_mensaje        INT           null,
      sesion            INT           null,
      secuencia         INT           null,
      casilla           INT           null,
      nombre_casilla    VARCHAR(30)   null,
      tipo_msg          CHAR(7)       null,
      nombre_tipo       VARCHAR(40)   null,
      prioridad         CHAR(1)       null,
      estado_msg        CHAR(3)       null,
      fecha_ingr        CHAR(10)      null,
      hora_ingr         CHAR(8)       null,
      cod_banco_em      CHAR(9)       null,
      branch_em         CHAR(4)       null,
      cod_banco_rec     CHAR(9)       null,
      branch_rec        CHAR(4)       null,
      nombre_banco      VARCHAR(70)   null,
      ciudad_banco      VARCHAR(35)   null,
      pais_banco        VARCHAR(35)   null,
      oficina_banco     VARCHAR(50)   null,
      cod_moneda        CHAR(3)       null,
      nombre_moneda     VARCHAR(50)   null,
      cod_moneda_banco  CHAR(3)       null,
      monto             FLOAT         null,
      referencia        VARCHAR(17)   null,
      beneficiario      VARCHAR(37)   null,
      fecha_de_orden    datetime      null
   )

   CREATE clustered index idx_ing_noaut_rango on #ing_noaut_rango(fecha_de_orden)

   if @p_casilla = 0
   begin
      insert  #ing_noaut_rango
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''DIG''
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''INY''
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''MOD''
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''REV''
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''SAP''
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''AUP''
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM   dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''DEV''
   end
else
   begin
      insert  #ing_noaut_rango
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =   ''DIG'' 
	  AND m.casilla        =   @p_casilla
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =   ''INY'' 
	  AND m.casilla        =   @p_casilla
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
          m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg      =  ''MOD'' 
	  AND m.casilla         =   @p_casilla
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =   ''REV'' 
	  AND m.casilla        =   @p_casilla
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =  ''SAP'' 
	  AND m.casilla        =   @p_casilla
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =   ''AUP'' 
	  AND m.casilla        =   @p_casilla
      UNION
      SELECT  m.id_mensaje, m.sesion, m.secuencia,
             m.casilla, c.nombre_casilla,
             m.tipo_msg, t.nombre_tipo,
             m.prioridad, m.estado_msg,
             convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
             convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
             m.cod_banco_em, m.branch_em,
             m.cod_banco_rec, m.branch_rec,
             b.nombre_banco, b.ciudad_banco,
             b.pais_banco, b.oficina_banco,
             m.cod_moneda, d.nombre_moneda,
             d.cod_moneda_banco, m.monto,
             m.referencia, m.beneficiario,m.fecha_ingreso
      FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
      WHERE  m.fecha_ingreso  >=   dateadd(dd,0,@p_fecha1) 
	  AND m.fecha_ingreso  <    dateadd(dd,+1,@p_fecha2) 
	  AND m.estado_msg     =   ''DEV'' 
	  AND m.casilla        =   @p_casilla
   end
   select   id_mensaje,
		      sesion,
		      secuencia,
              casilla,
              nombre_casilla,
              tipo_msg,
		      nombre_tipo,
              prioridad,
		      estado_msg,
              fecha_ingr,
              hora_ingr,
              cod_banco_em,
		      branch_em ,
              cod_banco_rec,
		      branch_rec,
              nombre_banco,
		      ciudad_banco,
              pais_banco,
		      oficina_banco,
              cod_moneda,
		      nombre_moneda,
              cod_moneda_banco,
		      monto,
              referencia,
		      beneficiario
   from #ing_noaut_rango
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_blo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_graba_blo_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_graba_blo_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_blo_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_graba_blo_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_blo_MS
	@p_id_mensaje 	INT, 
	@p_casilla 	INT, 
	@p_rut_log 	INT, 
	@p_comentario 	VARCHAR(80) 

AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:53:53 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@fecha_actual   datetime,@opcion_log 	CHAR(8)
begin tran

   SELECT   @estado_actual = estado_msg,
	@casilla_actual = casilla
   FROM 	dbo.sw_msgsend
   WHERE 	id_mensaje = @p_id_mensaje

   select   @fecha_actual = GetDate()

   update dbo.sw_msgsend set estado_msg = ''BLO'',comentario = ''Mensaje Bloqueado''  WHERE 	(id_mensaje = @p_id_mensaje)

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      rollback 
      return -1
   end

   IF exists(SELECT TOP 1 1 from dbo.sw_msgsend_add where id_mensaje = @p_id_mensaje)
   begin
      update dbo.sw_msgsend_add set fecha_bloqueo = @fecha_actual,rut_bloqueo = @p_rut_log,texto_bloqueo = @p_comentario,
      veces_bloqueo = veces_bloqueo+1  WHERE 	(id_mensaje = @p_id_mensaje)
   end
ELSE
   begin
      INSERT INTO dbo.sw_msgsend_add(id_mensaje, fecha_bloqueo, rut_bloqueo, texto_bloqueo,
	  veces_modifica, veces_rechazo, veces_bloqueo)
	VALUES(@p_id_mensaje, @fecha_actual, @p_rut_log, @p_comentario,
	  0, 0, 1)
   end
   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -2
   end

   select   @opcion_log = lower(@estado_actual)+''grblo''

   INSERT INTO dbo.sw_msgsend_log
VALUES(@p_id_mensaje, @fecha_actual,
	 @p_rut_log, @@servername,
         ''GS24'', @opcion_log,
	 @casilla_actual, @estado_actual,
	 @casilla_actual, ''BLO'',
	 @p_casilla, ''A'', ''Mensaje Bloqueado'')

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -3
   end

   COMMIT tran

   return 0
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_desblo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_graba_desblo_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_graba_desblo_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_desblo_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_graba_desblo_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_desblo_MS 
	@p_id_mensaje 	INT, 
	@p_casilla 	INT, 
	@p_rut_log 	INT 

AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:54:54 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@opcion_log 	CHAR(8),@estado_ant	CHAR(3),
   @coment_ant	VARCHAR(80)
begin tran

   SELECT   @estado_actual = estado_msg,
	@casilla_actual = casilla
   FROM 	dbo.sw_msgsend
   WHERE 	id_mensaje = @p_id_mensaje


   EXECUTE proc_sw_env_trae_ant_blo @p_id_mensaje,@estado_ant OUTPUT,@coment_ant OUTPUT

   update dbo.sw_msgsend set estado_msg = @estado_ant,comentario = @coment_ant  WHERE 	(id_mensaje = @p_id_mensaje)

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      rollback 
      return -1
   end

   select   @opcion_log = ''blogr''+lower(@estado_ant)

   INSERT INTO dbo.sw_msgsend_log
VALUES(@p_id_mensaje, GetDate(),
	 @p_rut_log, @@servername,
         ''GS24'', @opcion_log,
	 @casilla_actual, @estado_actual,
	 @casilla_actual, @estado_ant,
	 @p_casilla, ''A'', ''Mensaje Desbloqueado'')

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -2
   end

   COMMIT tran

   return 0
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_trae_firma_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_trae_firma_MS]...';
	DROP PROCEDURE [dbo].proc_sw_trae_firma_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_firma_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_trae_firma_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_firma_MS] 
	@Idmensaje INT
AS
BEGIN
	/*	
	Historial:
								Migración desde Sybase (AKZIO)
			2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	SET NOCOUNT ON 

	SELECT sw_msgsend_firma.id_mensaje, 
	sw_msgsend_firma.rut_firma, 
	sw_msgsend_firma.tipo_firma, 
	sw_msgsend_firma.estado_firma, 
	sw_msgsend_firma.revisa_firma, 
	sw_msgsend_firma.rut_solic, 
	sw_msgsend_firma.fecha_solic, 
	sw_msgsend_firma.fecha_firma, 
	sw_msgsend_firma.avisado,
	'' '' as nombre_firma,
	1 as estaba, 
	avisado as avis_antes 
	FROM sw_msgsend_firma 
	WHERE ( id_mensaje = @Idmensaje ) AND ( estado_firma <> ''N'' ) AND ( estado_firma <> ''D'' )

END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_ftp_err_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_trae_ftp_err_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_trae_ftp_err_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_ftp_err_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_trae_ftp_err_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_trae_ftp_err_MS
	@p_fecha1	datetime,
	@p_fecha2	datetime 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:47:47 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   SELECT   fd_archivo,
          nombre_archivo,
          fecha_creacion,
          fecha_confirma,
          estado_archivo,
          total_mensajes,
          total_envios,
          total_rechazos
   from   dbo.sw_msgsend_files
   where  fecha_creacion  >=  dateadd(dd,0,@p_fecha1)
   and  fecha_creacion  <   dateadd(dd,+1,@p_fecha2)
   and  estado_ftp      =  ''E''
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_detfile_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_trae_detfile_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_trae_detfile_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_detfile_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_trae_detfile_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_trae_detfile_MS
	@fd_archivo INT 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:46:46 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   select   d.id_mensaje, m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
		convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
		m.cod_banco_em, m.branch_em,
		m.cod_banco_rec, m.branch_rec,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, m.monto,
		m.referencia, m.beneficiario,
		f.fecha_creacion,
		convert(CHAR(10),d.fecha_envio,103) as fecha_envio,
		convert(CHAR(8),d.fecha_envio,108) as hora_envio,
		convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
		convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
		a.texto_rechazo
   from dbo.sw_msgsend m 
   LEFT OUTER JOIN dbo.sw_msgsend_add a ON m.id_mensaje = a.id_mensaje 
   LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
   LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
   LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch, 
   dbo.sw_msgsend_detfile d, 
   dbo.sw_msgsend_files f
   where 
   d.fd_archivo     = @fd_archivo 
   AND d.fd_archivo     = f.fd_archivo 
   AND d.id_mensaje     = m.id_mensaje
   order by d.id_mensaje
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_file_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_graba_file_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_graba_file_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_file_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_graba_file_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_file_MS
	@fd_archivo 	INT,
	@p_estado	CHAR(1) 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:54:54 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   update dbo.sw_msgsend_files 
   set estado_archivo = @p_estado,fecha_confirma = GetDate()  
   WHERE   fd_archivo = @fd_archivo
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_ftp_ree_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_graba_ftp_ree_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_graba_ftp_ree_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_ftp_ree_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_graba_ftp_ree_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_ftp_ree_MS
	@fd_archivo INT 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:44:44 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   update dbo.sw_msgsend_files 
   set estado_ftp = ''R''  
   WHERE   fd_archivo = @fd_archivo
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_nop_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_trae_nop_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_trae_nop_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_nop_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_trae_nop_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_trae_nop_MS
	@p_fecha1 datetime,
	@p_fecha2 datetime 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:48:48 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   SELECT   
		sesion, secuencia,
        tipo_msg, estado_msg,
        cod_banco_rec, branch_rec,
        cod_moneda, monto,
        referencia,
        convert(CHAR(10),fecha_proceso,103) fecha_pro,
        convert(CHAR(8),fecha_proceso,108) hora_pro,
        comentario
   FROM    dbo.sw_msgsend_nop
   WHERE  fecha_proceso  >= dateadd(dd,0,@p_fecha1)
   and   fecha_proceso  < dateadd(dd,+1,@p_fecha2)
   ORDER BY  fecha_proceso  asc
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_env_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_graba_env_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_graba_env_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_env_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_graba_env_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_env_MS
	@p_id_mensaje 	INT, 
	@p_sesion 	INT,
	@p_secuencia 	INT,
	@p_fecha_env 	datetime,
	@p_rut_log	INT,
	@p_unidad	INT 

AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:54:54 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@fecha_actual   datetime,@opcion_log 	CHAR(8),
   @fd_archivo	INT
begin tran

   SELECT   @estado_actual = estado_msg,
	@casilla_actual = casilla
   FROM 	dbo.sw_msgsend
   WHERE 	id_mensaje = @p_id_mensaje

   select   @fecha_actual = GetDate()

/* Actualiza Tabla Principal */
   update dbo.sw_msgsend set sesion = @p_sesion,secuencia = @p_secuencia,estado_msg = ''ENV'',comentario = ''Mensaje Enviado al Exterior''  WHERE 	(id_mensaje = @p_id_mensaje)

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      rollback 
      return -1
   end

/* Actualiza Tabla de Archivos y Detalle */
   SELECT   @fd_archivo = max(fd_archivo)
   FROM	dbo.sw_msgsend_detfile
   WHERE	id_mensaje = @p_id_mensaje

   update dbo.sw_msgsend_detfile set fecha_envio = @p_fecha_env  WHERE	id_mensaje = @p_id_mensaje and
   fd_archivo = @fd_archivo
   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -2
   end

   update dbo.sw_msgsend_files set total_envios = total_envios+1  WHERE  fd_archivo = @fd_archivo
   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -3
   end

/* Registra en Tabla LOG */
   select   @opcion_log = lower(@estado_actual)+''grenv''

   INSERT INTO dbo.sw_msgsend_log
VALUES(@p_id_mensaje, @fecha_actual,
	 @p_rut_log, @@servername,
         ''GS24'', @opcion_log,
	 @casilla_actual, @estado_actual,
	 @casilla_actual, ''ENV'',
	 @p_unidad, ''A'', ''Mensaje Enviado por Sistema'')

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -4
   end

   INSERT INTO dbo.sw_msgsend_cambio_estados
VALUES(@p_id_mensaje,
        ''ENV'',
        @fecha_actual)


   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -5
   end


   COMMIT tran

   return 0
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_res_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_graba_res_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_graba_res_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_res_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_graba_res_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_res_MS
	@p_id_mensaje 	INT, 
	@p_fecha_res 	datetime,
	@p_rut_log	INT,
	@p_unidad	INT,
	@p_texto	VARCHAR(90) 

AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:45:45 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@fecha_actual   datetime,@opcion_log 	CHAR(8),
   @fd_archivo	INT
begin tran

   SELECT   @estado_actual = estado_msg,
	@casilla_actual = casilla
   FROM 	dbo.sw_msgsend
   WHERE 	id_mensaje = @p_id_mensaje

   select   @fecha_actual = GetDate()

/* Actualiza Tabla Principal */
   update dbo.sw_msgsend set estado_msg = ''RES'',comentario = ''Mensaje Rechazado por Sistema''  WHERE 	(id_mensaje = @p_id_mensaje)

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      rollback 
      return -1
   end

/* Actualiza Tabla Secundaria */
   IF not exists(SELECT TOP 1 1 from dbo.sw_msgsend_add where id_mensaje = @p_id_mensaje)
      INSERT INTO dbo.sw_msgsend_add(id_mensaje, unidad_rechazo, rut_rechazo, fecha_rechazo,
	  texto_rechazo, veces_rechazo, veces_modifica, veces_bloqueo)
	VALUES(@p_id_mensaje, @p_unidad, @p_rut_log, @p_fecha_res,
	  @p_texto, 1, 0, 0)

ELSE
   update dbo.sw_msgsend_add set unidad_rechazo = @p_unidad,rut_rechazo    = @p_rut_log,fecha_rechazo  = @p_fecha_res,
   texto_rechazo  = @p_texto,veces_rechazo  = veces_rechazo+1  WHERE id_mensaje = @p_id_mensaje 
   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -2
   end

   DELETE 	dbo.sw_msgsend_firma
   WHERE 	id_mensaje = @p_id_mensaje
   IF (@@error <> 0) AND (@@rowcount = 0)
   begin
      ROLLBACK 
      return -3
   end

/* Actualiza Tabla de Archivos */
   SELECT   @fd_archivo = fd_archivo
   FROM	dbo.sw_msgsend_detfile
   WHERE	id_mensaje = @p_id_mensaje

   update dbo.sw_msgsend_files set total_rechazos = total_rechazos+1  WHERE  fd_archivo = @fd_archivo

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -4
   end


/* Registra en Tabla LOG */
   select   @opcion_log = lower(@estado_actual)+''grres''

   INSERT INTO dbo.sw_msgsend_log
VALUES(@p_id_mensaje, @fecha_actual,
	 @p_rut_log, @@servername,
         ''GS24'', @opcion_log,
	 @casilla_actual, @estado_actual,
	 @casilla_actual, ''RES'',
	 @p_unidad, ''A'', ''Mensaje Rechazado por Sistema'')

   if (@@error <> 0) or (@@rowcount = 0)
   begin
      ROLLBACK 
      return -5
   end

   COMMIT tran

   return 0
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_del_nop_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_del_nop_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_del_nop_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_del_nop_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_del_nop_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_del_nop_MS
	@p_sesion 	INT, 
	@p_secuencia 	INT 
AS
BEGIN
BEGIN TRAN
-- This procedure was converted on Thu Apr 17 16:53:53 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   DELETE	dbo.sw_msgsend_nop
   WHERE 	sesion   = @p_sesion 	and
   secuencia = @p_secuencia

   if @@error <> 0
   begin
      rollback 
      return -1
   end

   commit tran
   
   return 0
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_rh_swi_001_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_rh_swi_001_MS]...';
	DROP PROCEDURE [dbo].proc_rh_swi_001_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_rh_swi_001_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_rh_swi_001_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE proc_rh_swi_001_MS 
	@nurut INT
AS
SELECT a.fun_atributo,
		SUBSTRING(b.tabla_des,1,80) as ''Poder''
FROM   rh_trabajador a,tipoder b
WHERE  a.num_rut = @nurut        AND
		a.cod_pago = ''CDPAG00000'' AND
		a.fun_atributo = b.tabla_codig
RETURN'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_por_idmensaje_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_trae_por_idmensaje_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_trae_por_idmensaje_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_por_idmensaje_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_trae_por_idmensaje_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_por_idmensaje_MS] 
	@idMensaje int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT   
		m.id_mensaje, m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
		convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
		convert(CHAR(10),df.fecha_envio,103) as fecha_env,
		convert(CHAR(8),df.fecha_envio,108) as hora_env,
		m.cod_banco_em, m.branch_em,
		m.cod_banco_rec, m.branch_rec,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, d.nombre_moneda,
		d.cod_moneda_banco, m.monto,
		m.referencia, m.beneficiario, m.rut_ingreso
	FROM  dbo.sw_msgsend m 
	LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
	LEFT OUTER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje = m.id_mensaje 
	WHERE   m.id_mensaje = @idMensaje 
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_msg_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_msg_s01_MS]...';
	DROP PROCEDURE [dbo].proc_sw_msg_s01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_msg_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_msg_s01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_msg_s01_MS
	@id_msg     INT 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	SELECT   id_mensaje
	FROM    dbo.sw_msgsend
	WHERE   id_mensaje = @id_msg    
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_config_ing_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_config_ing_MS]...';
	DROP PROCEDURE [dbo].proc_sw_config_ing_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_config_ing_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_config_ing_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_config_ing_MS 
	@rut	INT,
	@apli	CHAR(1) 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:52:52 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	insert into dbo.sw_configura
	values(@rut, @apli)


	if @@error <> 0 or @@rowcount = 0
		return -1
	else
		return 0
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_msg_por_rut_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_env_msg_por_rut_MS]...';
	DROP PROCEDURE [dbo].proc_sw_env_msg_por_rut_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_msg_por_rut_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_env_msg_por_rut_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_msg_por_rut_MS
	@rut	INT,
	@fecha	datetime 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:45:45 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   SELECT   m.id_mensaje,
	m.casilla,
	m.tipo_msg,
	m.referencia,
	m.tipo_ingreso,
	m.estado_msg,
	e.descripcion
   FROM 	dbo.sw_msgsend m, dbo.sw_estados_msg e
   WHERE 	(m.estado_msg = e.estado_msg) and
	(e.send_recv = ''S'') and
	(m.rut_ingreso = @rut) and
	(datediff(dd,m.fecha_ingreso,@fecha) = 0)
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sw_mensajes_u01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [sw_mensajes_u01_MS]...';
	DROP PROCEDURE [dbo].sw_mensajes_u01_MS
END
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sw_mensajes_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [sw_mensajes_u01_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sw_mensajes_u01_MS
		 @sesion      int,  
	 @secuencia   int
AS  
BEGIN  
   SET NOCOUNT ON 
   BEGIN  
	 UPDATE sw_mensajes SET total_imp = total_imp + 1 where sesion = @sesion and secuencia = @secuencia and send_recv = ''R''
   END
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_rec_graba_imp_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sw_rec_graba_imp_MS]...';
	DROP PROCEDURE [dbo].proc_sw_rec_graba_imp_MS
END
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_imp_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [proc_sw_rec_graba_imp_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_rec_graba_imp_MS
		@p_casilla   INT,
		@p_sesion    INT,
		@p_secuencia INT,
		@p_rut_log   INT,
		@p_estado    CHAR(3),
		@comentario  VARCHAR(80) 

	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@rut_actual     INT,
	   @unidad_actual  INT,@fecha_actual   datetime,@rut_recibe     INT,
	   @unidad_recibe  INT,@fecha_recibe   datetime,@opcion_log	CHAR(8)
	begin tran

	   SELECT   @estado_actual = m.estado_msg,
		@casilla_actual = m.casilla,
		@rut_actual = m.rut_autoriza,
		@unidad_actual = a.unidad_recibe,
		@fecha_actual = a.fecha_recibe
	   FROM	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_mensajes_add a ON m.sesion = a.sesion and m.secuencia = a.secuencia and m.send_recv = a.send_recv
	   WHERE 	m.sesion = @p_sesion AND m.secuencia = @p_secuencia AND m.send_recv = ''R''

	   if @rut_actual is null
		  select   @rut_recibe = @p_rut_log
	else
	   select   @rut_recibe = @rut_actual

	   if @unidad_actual is null
		  select   @unidad_recibe = @p_casilla
	else
	   select   @unidad_recibe = @unidad_actual

	   if @fecha_actual is null
		  select   @fecha_recibe = GetDate()
	else
	   select   @fecha_recibe = @fecha_actual

	   update dbo.sw_mensajes set estado_msg = @p_estado, rut_autoriza = @rut_recibe,
	   comentario = @comentario  WHERE 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'')

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  select -1 as Result
	   end

	   if exists(select * from dbo.sw_mensajes_add where sesion = @p_sesion and
	   secuencia = @p_secuencia and send_recv = ''R'')
	   begin
		  update dbo.sw_mensajes_add set fecha_recibe = @fecha_recibe,unidad_recibe = @unidad_recibe,unidad_imprime = @p_casilla,
		  rut_imprime = @p_rut_log,fecha_imprime = GetDate()  WHERE 	(sesion = @p_sesion) and
			(secuencia = @p_secuencia) and
			(send_recv = ''R'')
	   end
	else
	   begin
		  INSERT INTO dbo.sw_mensajes_add(sesion, secuencia, send_recv, casilla,
		  fecha_recibe, unidad_recibe, unidad_imprime, rut_imprime,
		  fecha_imprime, veces_rechazo, veces_reenvio)
		VALUES(@p_sesion, @p_secuencia, ''R'', @casilla_actual,
		  @fecha_recibe, @unidad_recibe, @p_casilla, @p_rut_log,
		  GetDate(), 0, 0)
	   end
	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  select -2 as Result
	   end

	   select   @opcion_log = lower(@estado_actual)+''grimp''

	   INSERT INTO dbo.sw_log_msg
	VALUES(@p_sesion, @p_secuencia, ''R'', GetDate(),
		 @p_rut_log, NULL, ''GS24'', @opcion_log,
		 @casilla_actual, @estado_actual,
		 @casilla_actual, ''IMP'', @p_casilla, NULL,
		 ''A'', @comentario)

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  select -3 as Result
	   end

	   COMMIT tran

	   select 0 as Result
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'EncriptaMensaje_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [EncriptaMensaje_MS]...';
	DROP PROCEDURE [dbo].EncriptaMensaje_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EncriptaMensaje_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [EncriptaMensaje_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.EncriptaMensaje_MS (
	@sesion int , 
	@secuencia int , 
	@send_recv varchar(1), 
	@casilla int , 
	@unidad int, 
	@tipo_msg varchar(7), 
	@prioridad varchar(1), 
	@estado_msg varchar(3), 
	@rut_entry int,
	@rut_autoriza int, 
	@fecha_send varchar(26), 
	@fecha_ack varchar(26), 
	@cod_banco_rec varchar(9), 
	@branch_rec varchar(4), 
	@cod_banco_em varchar(9), 
	@branch_em varchar(4), 
	@cod_moneda varchar(3), 
	@monto float, 
	@referencia varchar(17), 
	@beneficiario varchar(37), 
	@total_imp int, 
	@mensaje_txt varchar(MAX), 
	@comentario varchar(80)
)
AS
BEGIN
/*	
Este procedimiento desencripta un mensaje de la tabla sw_mensajesH

Historial:
						 Migración desde Sybase (AKZIO)
	  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


	set ansi_padding off

	declare @i int,@ind int,@valor_aux char(1), @largo_llave int,@clave varchar(255);
	declare @mensaje varchar(MAX),  @mensaje_sal varchar(MAX);
	declare @monto_a varchar(6);
	declare @bin_aux binary(8);
	declare  @cant_reg int, @men_aux char(1),@j int,@fecha_sen datetime,@fecha_ac datetime;

	--***********************************************
	-- Akzio 2018
	-- Modificacion Enzabezado 
	Declare
	@par_mensaje_modificado varchar(max) ,
	@par_codRet      integer      ,
	@par_levRet      varchar(010) ,
	@par_gloRet      varchar(500) 
	--***********************************************

	--******************************************
	-- Akzio 2018
	-- Llamada a modificacion encabezado
	--******************************************
	exec pr_sw_mod_encabezado_rec
	@tipo_msg ,
	@sesion ,
	@secuencia ,
	@send_recv ,
	@mensaje_txt ,
	@par_mensaje_modificado  OUTPUT,
	@par_codRet            OUTPUT,
	@par_levRet      OUTPUT,
	@par_gloRet       OUTPUT

	IF @par_codRet = 0
	BEGIN
		SET @mensaje_txt = @par_mensaje_modificado
	END


	select @fecha_sen = convert (datetime,@fecha_send); 
	select @fecha_ac = convert (datetime,@fecha_ack); 

--Agrega ceros a secuencia si el largo es menor que 6

   if (@secuencia >= 0 and @secuencia < 10)
	  select @monto_a = ''00000'' + convert(varchar,@secuencia);
   else
   if (@secuencia >= 10 and @secuencia < 100)
	  select @monto_a = ''0000'' + convert(varchar,@secuencia);
   else
   if (@secuencia >= 100 and @secuencia < 1000)
	  select @monto_a = ''000'' + convert(varchar,@secuencia);
   else
   if (@secuencia >= 1000 and @secuencia < 10000)
	   select @monto_a = ''00'' + convert(varchar,@secuencia);
   else
   if (@secuencia >= 10000 and @secuencia < 100000)
	   select @monto_a = ''0'' + convert(varchar,@secuencia);
   else
   if (@secuencia >= 100000 and @secuencia < 1000000)
	   select @monto_a = convert(varchar,@secuencia);
   else
		select 9;
		
 --Crea tabla temporal que almacenarÃ¡ la clave
   create table #clave(
		indic int PRIMARY KEY CLUSTERED, 
		valor char(1),
		clave char(1) NULL,
		clave_e char(1) NULL);
 
 -- Inicia valores para encriptar llave  
   select @i = 1;
   select @clave = NULL;
   while  ( @i < 257)
   begin
	 insert into #clave (indic,valor) values (@i,char(@i-1));
	 select @i = @i + 1;
   end;


   select @largo_llave = len(@monto_a);
   
   select @i = 1;
   select @ind = 1;
   while  ( @i < 257)
   begin
	 update #clave set clave = substring(@monto_a,@ind,1) where indic = @i;
	 select @i = @i + 1;
	 select @ind =@ind + 1;
	  if (@ind = @largo_llave+1)
		 select @ind = 1;
   end;
   

--Encripta llave
   
   select @i = 1;
   select @ind = 1;

   while (@i < 257)
   begin
	  select @valor_aux = valor, @monto_a = clave from #clave where indic = @i; 
	  select @bin_aux = (ascii(@valor_aux)) ^ (ascii(@monto_a));
	  update #clave set clave_e = char(@bin_aux) where indic = @i;
	  
	  select @i = @i + 1;
	  select @ind =@ind + 1;
	  if (@ind = @largo_llave)
		 select @ind = 0;
   end
   
--Encripta mensaje y elimina caracteres
 
   select @i = 1;
   select @ind = 1;
   select @mensaje_sal = '''';
   
   while (@i <= len(@mensaje_txt))
   begin
	  select @valor_aux = clave_e from #clave where indic = @ind;
	  select @men_aux = substring(@mensaje_txt,@i,1); 
	  select @bin_aux = (ascii(@men_aux)) ^ (ascii(@valor_aux));

	  if (@bin_aux <> 34 and @bin_aux <> 35 and @bin_aux <> 10 and @bin_aux <> 0)
	   select @mensaje_sal = @mensaje_sal + char(@bin_aux);
	  else
	  begin
		 select @mensaje_sal = @mensaje_sal + char(35);
		 if (@bin_aux = 35)
			select @mensaje_sal = @mensaje_sal + char(35);
		else
		   if (@bin_aux = 34)
			 select @mensaje_sal = @mensaje_sal + char(36);
		   else
			 if (@bin_aux = 10)
					select @mensaje_sal = @mensaje_sal + char(38);
			   else
				  select @mensaje_sal = @mensaje_sal + char(37);
	  end
			
	  select @i = @i + 1;
	  select @ind =@ind + 1;
	  if (@ind = 257)
		 select @ind = 1;
   end
   
--Elimina caracteres generados durante la encrpitacion
 
-- Elimina tabla temporal
   drop table #clave;

--Inserta mensaje en tabla sw_mensajes
   
   insert into sw_mensajes values(
   @sesion, 
   @secuencia, 
   @send_recv, 
   @casilla, 
   @unidad, 
   @tipo_msg, 
   @prioridad, 
   @estado_msg, 
   @rut_entry,
   @rut_autoriza, 
   @fecha_sen, 
   @fecha_ac, 
   @cod_banco_rec, 
   @branch_rec, 
   @cod_banco_em, 
   @branch_em, 
   @cod_moneda, 
   @monto, 
   @referencia, 
   @beneficiario, 
   @total_imp, 
   @mensaje_sal, 
   @comentario) 
   
	   if @@error <> 0 
		return 1;
	else
		return 0;	


end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'EncriptaMensajeS_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [EncriptaMensajeS_MS]...';
	DROP PROCEDURE [dbo].EncriptaMensajeS_MS
END
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EncriptaMensajeS_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [EncriptaMensajeS_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.EncriptaMensajeS_MS (
	@id_mensaje int ,
	@sesion int ,
	@secuencia int ,
	@casilla int ,
	@unidad int ,
	@tipo_msg varchar(7),
	@prioridad varchar(1),
	@estado_msg varchar(3),
	@tipo_ingreso varchar(1),
	@rut_digita int,
	@banco_re varchar(9),
	@branch_re varchar(4),
	@banco_em varchar(9),
	@branch_em varchar(4),
	@moneda varchar(3),
	@monto float,
	@referencia varchar(17),
	@beneficiario varchar(37),
	@txt_mensaje varchar(MAX),
	@comentario varchar(80)
)
AS
BEGIN
/*	
Historial:
						 Migración desde Sybase (AKZIO)
	  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

set ansi_padding off

	--***********************************************
	-- Akzio 2018
	-- Modificacion Enzabezado 
	Declare
	@par_mensaje_modificado varchar(max) ,
	@par_codRet      integer      ,
	@par_levRet      varchar(010) ,
	@par_gloRet      varchar(500) 
	--***********************************************

	--********************************************
	-- Akzio 2018
	-- Llamada a modificacion encabezado
	--********************************************
	exec pr_sw_mod_encabezado_env
	@id_mensaje,
	@tipo_msg ,
	@sesion ,
	@secuencia ,
	''S'' ,
	@txt_mensaje ,
	@par_mensaje_modificado  OUTPUT,
	@par_codRet            OUTPUT,
	@par_levRet      OUTPUT,
	@par_gloRet       OUTPUT

	IF @par_codRet = 0
	BEGIN
		SET @txt_mensaje = @par_mensaje_modificado
	END

	--********************************************


declare @i int,@j int,@clave varchar(255), @llave varchar(101);
declare @mensaje varchar(MAX),  @mensaje_sal varchar(MAX);;
declare @mensaje_aux char(1),@aux char(1),@f int,@ran int,@e int;
	   
		select @llave = convert(varchar(100),@id_mensaje) + ''S''
		
		select @llave =  right(''0000000000'' + rtrim(ltrim(@llave)),11)
		
		select @i = 1;
		
		select @txt_mensaje	= replace(@txt_mensaje,''##'',char(39));
		
		create table #llave (
			indic int PRIMARY KEY CLUSTERED, 
			valor char(1) 
		);
		
		while( @i <= len(@llave))
		begin
			  if(ascii(substring(@llave,@i,1)) > 115)
				 insert into #llave values (@i - 1,char(ascii(substring(@llave,@i,1)) - 10));
			  else
				 insert into #llave values (@i - 1,substring(@llave,@i,1));
			  select @i = @i + 1;
		end
		  
		select @ran = 0;
		select @j = 0;
		select @f = 1;
		select @mensaje_sal = '''';
		   
		while (@f <= len(@txt_mensaje))
		begin
				if( @j = len(@llave)) 
					 select @j = 0;
				if( @ran > 9 ) 
					 select @ran = 0;
				
				select @aux = valor from #llave where indic = @j;
				select @e = ( ascii(substring(@txt_mensaje,@f,1)) + ascii(@aux) + @ran);
				
				if( @e > 255 ) 
				   select @e = @e - 256;
				
				select @mensaje_aux = char(@e ^ @ran);
		   
				select @mensaje_sal = @mensaje_sal + @mensaje_aux;
			   
				select @f = @f + 1;
				select @j = @j + 1;
				select @ran = @ran + 1;
		end
		
		drop table #llave;

		insert into sw_msgsend  values(@id_mensaje,
									   @sesion,
									   @secuencia,
									   @casilla,
									   @unidad,
									   @tipo_msg,
									   @prioridad,
									   @estado_msg,
									   @tipo_ingreso,
									   @rut_digita,
									   getdate(),
									   @banco_re,
									   @branch_re,
									   @banco_em,
									   @branch_em,
									   @moneda,
									   @monto,
									   REPLACE(REPLACE(ltrim(rtrim(@referencia)),CHAR(10),'''') ,CHAR(13),''''),
									   @beneficiario,
									   @mensaje_sal,
									   @comentario);

	if @@error <> 0 
		return 1;
	else
	begin
		--******************************************
		-- Akzio 2018
		-- Llamada a grabado mensaje encabezado
		--******************************************
		exec pr_sw_mod_msgmod
		@id_mensaje,
		@tipo_msg ,
		@sesion ,
		@secuencia ,
		''S'' ,
		@mensaje_sal ,
		@par_codRet            OUTPUT,
		@par_levRet      OUTPUT,
		@par_gloRet       OUTPUT
        
        return 0;	
	end

									   
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'UpdateMensajeS_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [UpdateMensajeS_MS]...';
	DROP PROCEDURE [dbo].UpdateMensajeS_MS
END
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateMensajeS_MS]') AND type in (N'P', N'PC'))
BEGIN
	PRINT 'Creando [UpdateMensajeS_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.UpdateMensajeS_MS (
	@unidad int ,
	@prioridad varchar(1),
	@estado_msg varchar(3),
	@banco_re varchar(9),
	@branch_re varchar(4),
	@moneda varchar(3),
	@monto float,
	@referencia varchar(17),
	@beneficiario varchar(37),
	@comentario varchar(80),
	@txt_mensaje varchar(MAX),
	@id_mensaje int 
)
AS
BEGIN
/*	
Historial:
						 Migración desde Sybase (AKZIO)
	  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/

--********************************************
-- Akzio 2018
-- Llamada a modificacion encabezado
--********************************************
DECLARE
@sesion int ,
@secuencia int ,
@casilla int ,
@tipo_msg varchar(7),
@par_mensaje_modificado varchar(max) ,
@par_codRet      integer      ,
@par_levRet      varchar(010) ,
@par_gloRet      varchar(500) 

SELECT @tipo_msg=tipo_msg ,@sesion=sesion ,@secuencia=secuencia, @casilla=casilla FROM sw_msgsend 	where id_mensaje = @id_mensaje;

exec pr_sw_mod_encabezado_env
@id_mensaje,
@tipo_msg ,
@sesion ,
@secuencia ,
''S'' ,
@txt_mensaje ,
@par_mensaje_modificado  OUTPUT,
@par_codRet            OUTPUT,
@par_levRet      OUTPUT,
@par_gloRet       OUTPUT
IF @par_codRet = 0
BEGIN
	SET @txt_mensaje = @par_mensaje_modificado
END

	SET NOCOUNT ON

	set ansi_padding off

	declare @i int,@j int,@clave varchar(255), @llave varchar(101);
	declare @mensaje varchar(MAX),  @mensaje_sal varchar(MAX);;
	declare @mensaje_aux char(1),@aux char(1),@f int,@ran int,@e int;
	   
	select @llave = convert(varchar(100),@id_mensaje) + ''S''
		
	select @llave =  right(''0000000000'' + rtrim(ltrim(@llave)),11)
		
	select @i = 1;
		
	select @txt_mensaje	= replace(@txt_mensaje,''##'',char(39));
		
	create table #llave (
		indic int PRIMARY KEY CLUSTERED, 
		valor char(1));
		
	while( @i <= len(@llave))
	begin
			if(ascii(substring(@llave,@i,1)) > 115)
				insert into #llave values (@i - 1,char(ascii(substring(@llave,@i,1)) - 10));
			else
				insert into #llave values (@i - 1,substring(@llave,@i,1));
			select @i = @i + 1;
	end
		  
	select @ran = 0;
	select @j = 0;
	select @f = 1;
	select @mensaje_sal = '''';
		   
	while (@f <= len(@txt_mensaje))
	begin
			if( @j = len(@llave)) 
					select @j = 0;
			if( @ran > 9 ) 
					select @ran = 0;
				
			select @aux = valor from #llave where indic = @j;
			select @e = ( ascii(substring(@txt_mensaje,@f,1)) + ascii(@aux) + @ran);
				
			if( @e > 255 ) 
				select @e = @e - 256;
				
			select @mensaje_aux = char(@e ^ @ran);
		   
			select @mensaje_sal = @mensaje_sal + @mensaje_aux;
			   
			select @f = @f + 1;
			select @j = @j + 1;
			select @ran = @ran + 1;
	end
		
	drop table #llave;

	update sw_msgsend set unidad = @unidad ,
							prioridad = @prioridad,
							estado_msg = @estado_msg,
							cod_banco_rec = @banco_re,
							branch_rec = @branch_re,
							cod_moneda = @moneda,
							monto = @monto,
							referencia = @referencia,
							beneficiario = @beneficiario,
							mensaje = @mensaje_sal,
							comentario = @comentario
	where id_mensaje = @id_mensaje;
		
		
	if @@error <> 0 
		return 1;
	else
		return 0;	



end'
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
