USE [cext01_R2]
GO
/****** Object:  StoredProcedure [dbo].[sce_dad_i01_MS]    Script Date: 12-01-2016 11:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER procedure [dbo].[sce_dad_i01_MS] 
	@id_party		CHAR(12),
	@id_dir			NUMERIC(2,0),
	@borrado		BIT,
	@direccion		VARCHAR(60),
	@comuna			VARCHAR(40),
	@cod_comuna		NUMERIC(4,0), -- (moo) 2016-01-12 | Existen códigos de comuna de largo 4 (antes, estaba largo 2)
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
	@email			VARCHAR(50)
AS
BEGIN
/*	
Historial:
							Migración desde Sybase (AKZIO)
      2015-09-29	AOC		Revisión de código proyecto migración Comex.Net (Microsoft)
	  2016-01-11	MOO		Existen códigos de comuna de largo 3 (antes, estaba largo 2) (Xemantics)
	  2016-01-12	MOO		Existen códigos de comuna de largo 4 (antes, estaba largo 2) (Xemantics)
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
END

