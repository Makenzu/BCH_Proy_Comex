
GOTO fin_script 
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;



USE sbcor;

PRINT N'Creating [dbo].[pro_sbc_bancos_MS]...';


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sbc_bancos_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sbc_bancos_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sbc_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sbc_bancos_MS] 
	@swift char(35), 
	@pais char(35), 
	@ciudad char(35), 
	@banco char(35), 
	@direccion char(35), 
	@postal char(35),
	@debug int = 0
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

		DECLARE @SQL nvarchar(max);
	
	DECLARE @p_swift VARCHAR(37) = ''%'' + RTRIM(UPPER(@swift)) + ''%''
	DECLARE @p_pais VARCHAR(35) = RTRIM(UPPER(@pais))
	DECLARE @p_ciudad VARCHAR(37) = ''%'' + RTRIM(UPPER(@ciudad)) + ''%''
	DECLARE @p_banco VARCHAR(37) = ''%'' + RTRIM(UPPER(@banco)) + ''%''
	DECLARE @p_direccion VARCHAR(37) = ''%'' + RTRIM(UPPER(@direccion)) + ''%''
	DECLARE @p_postal VARCHAR(37) = ''%'' + RTRIM(UPPER(@postal)) + ''%''
	

	SET @SQL = ''SELECT sce_bic.bic_swf, sce_bic.bic_sec, sce_bic.bic_nom, sce_bic.bic_des, sce_bic.bic_ciu, sce_bic.bic_dir, paises.cpai_codpaic bic_cod, paises.cpai_nompai bic_pai, (case sce_bic.bic_ala when 0 then ''''No'''' else ''''Si'''' end) as bic_ala, sce_bic.bic_pos
	FROM [dbo].[sce_bic]
	INNER JOIN [dbo].[sbc_cpai] paises on paises.cpai_codpaic = sce_bic.bic_cod
	WHERE (1=1)'';		  
	
	IF (@swift IS NOT NULL AND @swift <> '''')			SET @SQL = @SQL + '' AND sce_bic.bic_swf + sce_bic.bic_sec LIKE @swift'';
	IF (@pais IS NOT NULL AND @pais <> '''')				SET @SQL = @SQL + '' AND sce_bic.bic_cod = @pais'';  
	IF (@ciudad IS NOT NULL AND @ciudad <> '''')			SET @SQL = @SQL + '' AND sce_bic.bic_ciu LIKE @ciudad'';
	IF (@banco IS NOT NULL AND @banco <> '''') 			SET @SQL = @SQL + '' AND sce_bic.bic_nom LIKE @banco'';
	IF (@direccion IS NOT NULL AND @direccion <> '''')	SET @SQL = @SQL + '' AND sce_bic.bic_dir LIKE @direccion'';
	IF (@postal IS NOT NULL AND @postal <> '''')			SET @SQL = @SQL + '' AND sce_bic.bic_pos LIKE @postal'';

	if @debug > 0
	begin
		PRINT @SQL
		PRINT ''@swift = '' + ISNULL(''''+@p_swift+'''', ''NULL'') 
		PRINT ''@pais = '' + ISNULL(''''+@p_pais+'''', ''NULL'') 
		PRINT ''@ciudad = '' + ISNULL(''''+@p_ciudad+'''', ''NULL'') 
		PRINT ''@banco = '' + ISNULL(''''+@p_banco+'''', ''NULL'') 
		PRINT ''@direccion = '' + ISNULL(''''+@p_direccion+'''', ''NULL'') 
		PRINT ''@postal = '' + ISNULL(''''+@p_postal+'''', ''NULL'') 
	end

	if @debug < 2 
		exec sp_executesql 
			@SQL, 
			N''@swift varchar(37), @pais varchar(35), @ciudad varchar(37), @banco varchar(37), @direccion varchar(37), @postal varchar(37)'',
			@swift = @p_swift, 
			@pais = @p_pais,
			@ciudad = @p_ciudad, 
			@banco = @p_banco, 
			@direccion = @p_direccion, 
			@postal = @p_postal
				
		If @@ERROR <> 0 
			GoTo ErrorHandler

		Return(0)
  
	ErrorHandler:   
		Return(@@ERROR)
END'
END


PRINT N'Creating [dbo].[pro_sbc_bancos_count_MS]...';



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sbc_bancos_count_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sbc_bancos_count_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sbc_bancos_count_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sbc_bancos_count_MS] 
	@swift char(35), 
	@pais char(35), 
	@ciudad char(35), 
	@banco char(35), 
	@direccion char(35), 
	@postal char(35),
	@debug int = 0
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

		DECLARE @SQL nvarchar(max);
	
	DECLARE @p_swift VARCHAR(37) = ''%'' + RTRIM(UPPER(@swift)) + ''%''
	DECLARE @p_pais VARCHAR(35) = RTRIM(UPPER(@pais))
	DECLARE @p_ciudad VARCHAR(37) = ''%'' + RTRIM(UPPER(@ciudad)) + ''%''
	DECLARE @p_banco VARCHAR(37) = ''%'' + RTRIM(UPPER(@banco)) + ''%''
	DECLARE @p_direccion VARCHAR(37) = ''%'' + RTRIM(UPPER(@direccion)) + ''%''
	DECLARE @p_postal VARCHAR(37) = ''%'' + RTRIM(UPPER(@postal)) + ''%''
	

	SET @SQL = ''SELECT COUNT(1) FROM [dbo].[sce_bic] WHERE (1=1)'';
	
	IF (@swift IS NOT NULL AND @swift <> '''')			SET @SQL = @SQL + '' AND sce_bic.bic_swf + sce_bic.bic_sec LIKE @swift'';
	IF (@pais IS NOT NULL AND @pais <> '''')				SET @SQL = @SQL + '' AND sce_bic.bic_cod = @pais'';  
	IF (@ciudad IS NOT NULL AND @ciudad <> '''')			SET @SQL = @SQL + '' AND sce_bic.bic_ciu LIKE @ciudad'';
	IF (@banco IS NOT NULL AND @banco <> '''') 			SET @SQL = @SQL + '' AND sce_bic.bic_nom LIKE @banco'';
	IF (@direccion IS NOT NULL AND @direccion <> '''')	SET @SQL = @SQL + '' AND sce_bic.bic_dir LIKE @direccion'';
	IF (@postal IS NOT NULL AND @postal <> '''')			SET @SQL = @SQL + '' AND sce_bic.bic_pos LIKE @postal'';

	if @debug > 0
	begin
		PRINT @SQL
		PRINT ''@swift = '' + ISNULL(''''+@p_swift+'''', ''NULL'') 
		PRINT ''@pais = '' + ISNULL(''''+@p_pais+'''', ''NULL'') 
		PRINT ''@ciudad = '' + ISNULL(''''+@p_ciudad+'''', ''NULL'') 
		PRINT ''@banco = '' + ISNULL(''''+@p_banco+'''', ''NULL'') 
		PRINT ''@direccion = '' + ISNULL(''''+@p_direccion+'''', ''NULL'') 
		PRINT ''@postal = '' + ISNULL(''''+@p_postal+'''', ''NULL'') 
	end

	if @debug < 2 
		exec sp_executesql 
			@SQL, 
			N''@swift varchar(37), @pais varchar(35), @ciudad varchar(37), @banco varchar(37), @direccion varchar(37), @postal varchar(37)'',
			@swift = @p_swift, 
			@pais = @p_pais,
			@ciudad = @p_ciudad, 
			@banco = @p_banco, 
			@direccion = @p_direccion, 
			@postal = @p_postal
				
		If @@ERROR <> 0 
			GoTo ErrorHandler

		Return(0)
  
	ErrorHandler:   
		Return(@@ERROR)
END'
END

PRINT N'Creating [dbo].[sbcd57_MS]...';



SET ANSI_NULLS ON;

SET QUOTED_IDENTIFIER OFF;



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sbcd57_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sbcd57_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sbcd57_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sbcd57_MS
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 
 
	declare @fecha   datetime
	declare @mes     int
	declare @agno    int
	declare @periodo int
	declare @dia  int
	declare @n_reg_e int
	declare @n_reg_r int
	declare @result  int
	declare @log  char(40)

	select @mes  = datepart(mm,getdate())
	select @agno  = datepart(yy,getdate())
	select @periodo = (@agno * 100) + (@mes-1)

	select @log=getdate()
	print ''''
	   print N''(*) Fecha-Hora de Inicio de Proceso:  '' + @log
	print ''===================================''

	print '' ''
	print ''(*) CALCULO DE RESTRICCIONES PARA MATRIZ DE RIESGO.''
	print ''===================================================''

	print '' ''
	print ''(A) Restricciones Generales.''
	print ''=============================''
	exec @result =dbo.sbc_rst_val1
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (A)1. Proceso sbc_rst_val1. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (A)1. Cancelado Proceso sbc_rst_val1. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val2
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (A)2. Proceso sbc_rst_val2. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (A)2. Cancelado Proceso sbc_rst_val2. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val3
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (A)3. Proceso sbc_rst_val3. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (A)3. Cancelado Proceso sbc_rst_val3. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val4
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (A)4. Proceso sbc_rst_val4. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (A)4. Cancelado Proceso sbc_rst_val4. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val5
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (A)5. Proceso sbc_rst_val5. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (A)5. Cancelado Proceso sbc_rst_val5. Retorno:  ''  + @log
	  return
	end

	print '' ''
	print ''(B) Restricciones Paises Desarrollados.''
	print ''=======================================''
	exec @result =dbo.sbc_rst_val6
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (B)6. Proceso sbc_rst_val6. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (B)6. Cancelado Proceso sbc_rst_val6. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val7
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (B)7a. Proceso sbc_rst_val7. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (B)7a. Cancelado Proceso sbc_rst_val7. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val8
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (B)7b. Proceso sbc_rst_val8. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (B)7b. Cancelado Proceso sbc_rst_val8. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val9
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (B)7c. Proceso sbc_rst_val9. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (B)7c. Cancelado Proceso sbc_rst_val9. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val10
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (B)8. Proceso sbc_rst_val10. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (B)8. Cancelado Proceso sbc_rst_val10. Retorno:  ''  + @log
	  return
	end

	print '' ''
	print ''(C) Restricciones Paises Emergentes.''
	print ''====================================''
	exec @result =dbo.sbc_rst_val11
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (C)9. Proceso sbc_rst_val11. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (C)9. Cancelado Proceso sbc_rst_val11. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val12
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (C)10. Proceso sbc_rst_val12. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (C)10. Cancelado Proceso sbc_rst_val12. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val13
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (C)11. Proceso sbc_rst_val13. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (C)11. Cancelado Proceso sbc_rst_val13. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val14
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (C)12. Proceso sbc_rst_val14. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (C)12. Cancelado Proceso sbc_rst_val14. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val15
	select @log = convert(char(03),@result)
	if @result = 0
	begin
	  print ''''
		print N''O.K. (C)13. Proceso sbc_rst_val15. Retorno:  ''  + @log
	end
	else
	begin
	  print ''''
		print N''ERROR (C)13. Cancelado Proceso sbc_rst_val15. Retorno:  ''  + @log
	  return
	end

	exec @result =dbo.sbc_rst_val16                                    
	select @log = convert(char(03),@result)                                   
	if @result = 0                                                            
	begin                                                                     
	  print ''''                                                                
		print N''O.K. (C)14. Proceso sbc_rst_val16. Retorno:  ''  + @log
	end                                                                       
	else                                                                      
	begin                                                                     
	  print ''''                                                                
		print N''ERROR (C)14. Cancelado Proceso sbc_rst_val16. Retorno:  ''  + @log
	  return                                                                  
	end 

	print '' ''                                  
	print ''(D) Restricciones SBIF''
	print ''======================''

	exec @result =dbo.sbc_rst_val17                                    
	select @log = convert(char(03),@result)                                   
	if @result = 0                                                            
	begin                                                                     
	  print ''''                                                                
		print N''O.K. (D)15a. Proceso sbc_rst_val17. Retorno:  ''  + @log
	end                                                                       
	else                                                                      
	begin                                                                     
	  print ''''                                                                
		print N''ERROR (D)15a. Cancelado Proceso sbc_rst_val17. Retorno:  ''  + @log
	  return                                                                  
	end 

	exec @result =dbo.sbc_rst_val18   
	select @log = convert(char(03),@result)                                     
	if @result = 0                                                              
	begin                                                                       
	  print ''''                                                                  
		print N''O.K. (D)15b. Proceso sbc_rst_val18. Retorno:  ''  + @log
	end                                                                         
	else                                                                        
	begin                                                                       
	  print ''''                                                                  
		print N''ERROR (D)15b. Cancelado Proceso sbc_rst_val18. Retorno:  ''  + @log
	  return                                                                    
	end                                                                         

	exec @result =dbo.sbc_rst_val19                                      
	select @log = convert(char(03),@result)                                     
	if @result = 0                                                              
	begin                                                                       
	  print ''''                                                                  
		print N''O.K. (D)16a. Proceso sbc_rst_val19. Retorno:  ''  + @log
	end                                                                         
	else                                                                        
	begin                                                                       
	  print ''''                                                                  
		print N''ERROR (D)16a. Cancelado Proceso sbc_rst_val19. Retorno:  ''  + @log
	  return                                                                    
	end

	exec @result =dbo.sbc_rst_val20                                      
	select @log = convert(char(03),@result)                                     
	if @result = 0                                                              
	begin                                                                       
	  print ''''                                                                  
		print N''O.K. (D)16b. Proceso sbc_rst_val20. Retorno:  ''  + @log
	end                                                                         
	else                                                                        
	begin                                                                       
	  print ''''                                                                  
		print N''ERROR (D)16b. Cancelado Proceso sbc_rst_val20. Retorno:  ''  + @log
	  return                                                                    
	end                                                                         
     
	exec @result =dbo.sbc_rst_val21                                      
	select @log = convert(char(03),@result)                                     
	if @result = 0                                                              
	begin                                                                       
	  print ''''                                                                  
		print N''O.K. (D)17a. Proceso sbc_rst_val21. Retorno:  ''  + @log
	end                                                                         
	else                                                                        
	begin                                                                       
	  print ''''                                                                  
		print N''ERROR (D)17a. Cancelado Proceso sbc_rst_val21. Retorno:  ''  + @log
	  return                                                                    
	end                                                                         
     
	exec @result =dbo.sbc_rst_val22                                      
	select @log = convert(char(03),@result)                                     
	if @result = 0                                                              
	begin                                                                       
	  print ''''                                                                  
		print N''O.K. (D)17b. Proceso sbc_rst_val22. Retorno:  ''  + @log
	end                                                                         
	else                                                                        
	begin                                                                       
	  print ''''                                                                  
		print N''ERROR (D)17b. Cancelado Proceso sbc_rst_val22. Retorno:  ''  + @log
	  return                                                                    
	end                                                                         
                                                               
	select @log=getdate()
	print ''''
	   print N''(*) Fecha-Hora de TÃ©rmino de Proceso:  '' + @log
	print ''=====================================''
	-- AKZ001 go
	end'
END

SET ANSI_NULLS, QUOTED_IDENTIFIER ON;



PRINT N'Update complete.';


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sbc_version_codigo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].proc_sbc_version_codigo_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sbc_version_codigo_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[proc_sbc_version_codigo_MS] 
	AS
	begin
	/************************************************************************/
		/*replace-begin*/select ''1.38.1''/*replace-end*/
	end'
END

fin_script:
	print 'fin script Script_Sbcor_codigo.sql'
