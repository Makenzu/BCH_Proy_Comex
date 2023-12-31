USE [cext01_R2]
GO
/****** Object:  StoredProcedure [dbo].[sce_rsa_u02_MS]    Script Date: 11-01-2016 16:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER procedure [dbo].[sce_rsa_u02_MS]
	@id_party       CHAR(12),
	@id_nombre		NUMERIC(2,0)
AS
BEGIN
/*	
Historial:
							Migración desde Sybase (AKZIO)
      2015-09-29	AOC		Revisión de código proyecto migración Comex.Net (Microsoft)
	  2016-01-11	MOO		Se cambia por "=", con "<>" elimina todos los distintos
*/
    SET NOCOUNT ON

	UPDATE sce_rsa 
	SET borrado		= 1
	WHERE 
		id_party	= @id_party
		AND id_nombre  = @id_nombre -- (moo) 2016-01-11 | Se cambia por "=", con "<>" elimina todos los distintos
END

