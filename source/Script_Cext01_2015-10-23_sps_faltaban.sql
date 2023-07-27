/****** SPs para informar novedades al hacer Inicio de Dia ******/


USE [cext01]
GO
/****** Object:  StoredProcedure [dbo].[sce_nov_s01]    Script Date: 23/10/15 14:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_nov_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[sce_nov_s01_MS] 
GO


CREATE PROCEDURE [dbo].[sce_nov_s01_MS] @codpro      CHAR(2),
	@fecnov      datetime,
	@cencos	     CHAR(3),
	@codusr	     CHAR(2),	
	@jerarquia   NUMERIC(1,0) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:00:00 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
--Si la jerarquia es 0: Especialista
   if @jerarquia = 0
   begin
      select   codcct,
		codpro,
		codesp,
		codofi,
        	codope,
		fecnov,
		codnov,
		codneg,
        	codsec,
		numcuo,
		fecven,
		prtcli,
		codmnd,
        	mtoope,
		cencos,
		codusr,
		estado,
        	glosa,
		tipnov,
		fecini
      from dbo.sce_nov where
      fecnov = @fecnov and
      cencos = @cencos and
      codusr = @codusr
      order by cencos,codusr,tipnov,codpro, codesp, codofi, codope
   end
else
   begin	
	--Si la jerarquia es 2: Jefe Seccion 
      if @jerarquia = 2
      begin
         select   codcct,
				codpro,
				codesp,
				codofi,
        			codope,
				fecnov,
				codnov,
				codneg,
        			codsec,
				numcuo,
				fecven,
				prtcli,
				codmnd,
        			mtoope,
				cencos,
				codusr,
				estado,
        			glosa,
				tipnov,
				fecini
         from dbo.sce_nov where
         fecnov = @fecnov and
         cencos = @cencos
         order by cencos,codusr,tipnov,codpro, codesp, codofi, codope
      end
   else
      begin
		--Si la jerarquia es 1: Supervisor 
         if @jerarquia = 1
         begin
            select   a.codcct,
					a.codpro,
					a.codesp,
					a.codofi,
          			a.codope,
					a.fecnov,
					a.codnov,
					a.codneg,
          			a.codsec,
					a.numcuo,
					a.fecven,
					a.prtcli,
					a.codmnd,
          			a.mtoope,
					a.cencos,
					a.codusr,
					a.estado,
          			a.glosa,
					a.tipnov,
					a.fecini
            from dbo.sce_nov a
            where a.fecnov = @fecnov
            and a.cencos = @cencos
            and exists(select 1
               from dbo.sce_usr b
               where b.cent_super = @cencos
               and b.id_super   = @codusr
               and b.cent_costo = a.cencos
               and b.id_especia = a.codusr)
            order by cencos,codusr,tipnov,codpro, codesp, codofi, codope
         end
      end
   end 
   return
END


IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'sce_nov_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[sce_nov_s03_MS] 
GO

CREATE PROCEDURE [dbo].[sce_nov_s03_MS] @codpro      CHAR(2),
	@fecnov      datetime,
	@cencos	     CHAR(3),
	@codusr	     CHAR(2),	
	@jerarquia   NUMERIC(1,0) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:22:22 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
--Si la jerarquia es 0: Especialista
   if @jerarquia = 0
   begin
      select   count(*)
      from 	dbo.sce_nov where
      fecnov = @fecnov and
      cencos = @cencos and
      codusr = @codusr
	--order by cencos+codusr,tipnov 
   end
else
   begin	
	--Si la jerarquia es 2: Jefe Seccion 
      if @jerarquia = 2
      begin
         select   count(*)
         from 	dbo.sce_nov
         where   fecnov = @fecnov and
         cencos = @cencos
   			--order by cencos+codusr,tipnov
      end
   else
      begin
		--Si la jerarquia es 1: Supervisor 
         if @jerarquia = 1
         begin
            select   count(*)
            from 	dbo.sce_nov a
            where a.fecnov = @fecnov
            and a.cencos = @cencos
            and exists(select 1
               from dbo.sce_usr b
               where b.cent_super = @cencos
               and b.id_super   = @codusr
               and b.cent_costo = a.cencos
               and b.id_especia = a.codusr)
         end
      end
   end 
   return
END



