USE [cext01];

BEGIN TRY

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_version_tablas_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Eliminando [proc_sce_version_tablas_MS]...';
	DROP PROCEDURE [dbo].proc_sce_version_tablas_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_version_tablas_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	PRINT 'Creando [proc_sce_version_tablas_MS]...';
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sce_version_tablas_MS] AS
	BEGIN
		/*replace-begin*/select ''1.50.2''/*replace-end*/ + ''.2''
	END'
END
-----------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT TOP 1 1 FROM sys.columns WHERE name = N'esPlantilla' AND Object_ID = Object_ID(N'sce_swf_pendientes'))
BEGIN
	ALTER TABLE dbo.sce_swf_pendientes ADD esPlantilla bit NULL; 
END
-----------------------------------------------------------------------------------------------------------------
IF (SELECT max_length FROM sys.columns WHERE name = N'archivo' AND Object_ID = Object_ID(N'sce_swf_pendientes')) = 6
BEGIN
	PRINT 'Eliminan PK tabla sce_swf_pendientes...';
	ALTER TABLE dbo.sce_swf_pendientes DROP CONSTRAINT PK_sce_swf_pendientes
	PRINT 'Se modifica largo columna archivo de la tabla sce_swf_pendientes...';
	ALTER TABLE dbo.sce_swf_pendientes ALTER Column archivo nvarchar(255) NOT NULL
	PRINT 'Se crean PK tabla sce_swf_pendientes...';
	ALTER TABLE dbo.sce_swf_pendientes ADD CONSTRAINT PK_sce_swf_pendientes PRIMARY KEY (ctecct, codesp, archivo)
END

-- si no acepta valores nullables cambiamos las columnas de la tabla para que lo acepten
IF (SELECT COLUMNPROPERTY(OBJECT_ID('sce_swf_pendientes', 'U'), 'moneda', 'AllowsNull')) = 0
BEGIN
	PRINT 'Se modifica null constraints...';
    ALTER TABLE dbo.sce_swf_pendientes ALTER Column moneda nvarchar(10) NULL
    ALTER TABLE dbo.sce_swf_pendientes ALTER Column monto decimal(18, 2) NULL
    ALTER TABLE dbo.sce_swf_pendientes ALTER Column referencia nvarchar(16) NULL
END

-- verificamos que exista la columna email en la tabla sce_dad
IF (SELECT max_length FROM sys.columns WHERE name = N'email' AND Object_ID = Object_ID(N'sce_dad')) != 500
BEGIN
	PRINT 'Se modifica el largo del campo email de la tabla sce_dad...';
	ALTER TABLE sce_dad
	ALTER COLUMN email VARCHAR(500);
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

-----------------------------------------------------------------------------------------------------------------
