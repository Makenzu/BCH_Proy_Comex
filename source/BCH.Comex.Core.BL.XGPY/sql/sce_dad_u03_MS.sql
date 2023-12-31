USE [cext01_R2]
GO
/****** Object:  StoredProcedure [dbo].[sce_dad_u03_MS]    Script Date: 12-01-2016 15:56:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*	
Historial:
							Migración desde Sybase (AKZIO)
      2015-09-10	AOC		Revisión de código proyecto migración Comex.Net (Microsoft)
	  2016-01-12	MOO		Correción Update para marcar para eliminar una dirección (0 -> 1) (Xemantics)
*/
ALTER PROCEDURE [dbo].[sce_dad_u03_MS]
	@id_party       CHAR(12),
	@id_dir         TINYINT 
AS
BEGIN
	UPDATE sce_dad 
	SET borrado = 1		-- (moo) 2016-01-12 | Por error, flag borrado en "0" cuando debe ser "1"
	WHERE 
		id_party	= @id_party  
		AND id_dir	= @id_dir
END
