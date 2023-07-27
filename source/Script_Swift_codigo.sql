USE [swift]
GO
/****** Object:  StoredProcedure [dbo].[proc_sw_trae_tiposMensajeConFormato_MS]    Script Date: 14/10/15 18:59:29 ******/
IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'proc_sw_trae_tiposMensajeConFormato_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].sce_xplv_MS 
GO

/* Trae los tipos de MT que pueden armados desde Envio de Swift. Estos son los MT que o bien no tienen campos con repeticion,
en ese caso tienen que figurar en sw_formatos, o bien tienen campos con repeticion y en ese caso tienen que figurar en
sw_ciclos
*/

CREATE PROCEDURE [dbo].[proc_sw_trae_tiposMensajeConFormato_MS]
AS
BEGIN
	SET NOCOUNT ON;

   select * from sw_tipos_msg where cod_tipo 
in
(
select distinct tipo_msg_fmt
from dbo.sw_formatos where repeticion_fmt > 0
AND tipo_msg_fmt in (select distinct tipo_msg_fmt from sw_ciclos)
UNION
select distinct tipo_msg_fmt from sw_formatos
where tipo_msg_fmt not in (select distinct tipo_msg_fmt
from dbo.sw_formatos where repeticion_fmt > 0)
)
ORDER BY cod_tipo

END

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'proc_sw_trae_fmt_campos_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].sce_xplv_MS 
GO

CREATE PROCEDURE [dbo].[proc_sw_trae_fmt_campos_MS] @p_tipo_mt CHAR(5) 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   CREATE table #tmp_fmt
   (
      status     CHAR(1)      null,
      tag	       CHAR(3)      null,
      nombre     VARCHAR(40)  null,
      orden	   INT          null,
      repeticion INT          null,
      formato    VARCHAR(30)  null,
      largo	   INT          null,
      linea	   INT          null
   )


   if SUBSTRING(@p_tipo_mt,4,1) = '9'
      select   @p_tipo_mt = stuff(@p_tipo_mt,3,1,'0')

   insert  #tmp_fmt
   select  b.status_fmt,
        b.tag_fmt,
        c.nombre_campo_tipcam,
        b.orden_fmt,
        b.repeticion_fmt,
        a.formato_campo,
        a.largo_campo,
        a.linea_campo
   from    dbo.sw_formatos b LEFT OUTER JOIN dbo.sw_tipos_campos c ON b.tipo_msg_fmt = c.tipo_msg_tipcam and b.tag_fmt = c.tag_campo_tipcam, 
dbo.sw_campos_msg a
   where  b.tipo_msg_fmt  =   @p_tipo_mt AND b.tag_fmt       =   a.tag_campo


   update #tmp_fmt set nombre = isnull(convert(VARCHAR,b.nombre_campo_tipcam),'') from dbo.sw_tipos_campos b, #tmp_fmt a where b.tag_campo_tipcam = a.tag
   and tipo_msg_tipcam    = 'MT000'
   and (a.nombre is null or rtrim(a.nombre) = '')

   select * from #tmp_fmt
   order by orden,tag,linea
END


IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'proc_sw_trae_fmt_ciclos_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].sce_xplv_MS 
GO

CREATE PROCEDURE [dbo].[proc_sw_trae_fmt_ciclos_MS] @p_tipo_mt CHAR(5) 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   CREATE table #tmp_fmt
   (
      status     CHAR(1)       null,
      tag	       CHAR(3)       null,
      nombre     VARCHAR(40)   null,
      orden	   INT           null,
      repeticion INT           null,
      formato    VARCHAR(30)   null,
      largo	   INT           null,
      linea	   INT           null
   )


   if SUBSTRING(@p_tipo_mt,4,1) = '9'
      select   @p_tipo_mt = stuff(@p_tipo_mt,3,1,'0')


   insert  #tmp_fmt
   select  b.status_fmt,
        b.tag_fmt,
        c.nombre_campo_tipcam,
        b.orden_fmt,
        b.repeticion_fmt,
        a.formato_campo,
        a.largo_campo,
        a.linea_campo
   from    dbo.sw_ciclos b LEFT OUTER JOIN dbo.sw_tipos_campos c ON b.tipo_msg_fmt = c.tipo_msg_tipcam and b.tag_fmt = c.tag_campo_tipcam, 
dbo.sw_campos_msg a
   where   b.tipo_msg_fmt  =  @p_tipo_mt AND b.tag_fmt       =  a.tag_campo

 

   update #tmp_fmt set nombre = isnull(convert(VARCHAR,b.nombre_campo_tipcam),'') from dbo.sw_tipos_campos b, #tmp_fmt a where b.tag_campo_tipcam = a.tag
   and tipo_msg_tipcam = 'MT000'
   and (a.nombre is null or rtrim(a.nombre) = '')

   select * from #tmp_fmt
   order by orden,tag,linea
END