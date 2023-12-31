USE [cext01_R2]
GO
/****** Object:  StoredProcedure [dbo].[sce_prty_w01_MS]    Script Date: 11-01-2016 13:49:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sce_prty_w01_MS] @id_party       CHAR(12),
	@borrado        BIT	,
	@tipo_party     TINYINT	,
	@flag           SMALLINT,
	@clasificac     TINYINT ,
	@tiene_rut      BIT	,
	@rut            CHAR(10),
	@crea_costo     CHAR(3),
	@crea_user      CHAR(2),
	@mod_costo      CHAR(3),
	@mod_user       CHAR(2),
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
	@par1			VARCHAR(255) = NULL --AKZ001 no se utiliza 
AS
BEGIN
/*	
Historial:
							Migración desde Sybase (AKZIO)
      2015-09-02	AOC		Revisión de código proyecto migración Comex.Net (Microsoft)
	  2016-01-11	MOO		Correción retorno de nombre de columnas (Xemantics)
*/
BEGIN TRAN
   if not exists(select * from dbo.sce_prty where id_party = @id_party)
   begin
      insert into dbo.sce_prty(
				id_party  ,
				borrado   ,
				tipo_party,
				flag      ,
				clasificac,
				tiene_rut ,
				rut       ,
				crea_costo,
				crea_user ,
				mod_costo ,
				mod_user  ,
				multiple  ,
				cod_ofieje,
				cod_eject ,
				cod_acteco,
				clase_ries,
				cod_bco   ,
				tasa_libor,
				tasa_prime,
				spread    ,
				swift     ,
				plaza_alad,
				ejec_corre,
				flagins   ,
				insgen_imp,
				insgen_exp,
				insgen_ser,
				inscob_imp,
				inscob_exp,
				inscre_imp,
				inscre_exp,
				fecing	  ,
				fecact)
		values(@id_party  ,
				@borrado   ,
				@tipo_party,
				@flag      ,
				@clasificac,
				@tiene_rut ,
				@rut       ,
				@crea_costo,
				@crea_user ,
				@mod_costo ,
				@mod_user  ,
				@multiple  ,
				@cod_ofieje,
				@cod_eject ,
				@cod_acteco,
				@clase_ries,
				@cod_bco   ,
				@tasa_libor,
				@tasa_prime,
				@spread    ,
				@swift     ,
				@plaza_alad,
				@ejec_corre,
				@flagins   ,
				@insgen_imp,
				@insgen_exp,
				@insgen_ser,
				@inscob_imp,
				@inscob_exp,
				@inscre_imp,
				@inscre_exp,
				GetDate(),
				GetDate())
   end
else
   begin
      update dbo.sce_prty set borrado = @borrado,tipo_party = @tipo_party,flag = @flag,clasificac = @clasificac,
      tiene_rut = @tiene_rut,rut = @rut,crea_costo = @crea_costo,
      crea_user = @crea_user,mod_costo = @mod_costo,mod_user = @mod_user,multiple = @multiple,
      cod_ofieje = @cod_ofieje,cod_eject = @cod_eject,cod_acteco = @cod_acteco,
      clase_ries = @clase_ries,cod_bco = @cod_bco,tasa_libor = @tasa_libor,
      tasa_prime = @tasa_prime,spread = @spread,swift = @swift,
      plaza_alad = @plaza_alad,ejec_corre = @ejec_corre,flagins = @flagins,
      insgen_imp = @insgen_imp,insgen_exp = @insgen_exp,insgen_ser = @insgen_ser,
      inscob_imp = @inscob_imp,inscob_exp = @inscob_exp,inscre_imp = @inscre_imp,
      inscre_exp = @inscre_exp,fecact = GetDate()  where id_party = @id_party
   end
   		
   IF (@@rowcount = 0 or @@error <> 0)
   BEGIN
      ROLLBACK 
      SELECT   -1 AS Column1, 'Error al grabar datos en SCE_PRTY_W01.' AS Column2
      RETURN
   END

   COMMIT TRAN

   SELECT   0 AS Column1, 'Grabación Exitosa' AS Column2
   RETURN

END


