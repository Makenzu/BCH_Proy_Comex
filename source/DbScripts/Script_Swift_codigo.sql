
GOTO fin_script  

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;

USE [swift]

/****** Object:  StoredProcedure [dbo].[UpdateMensajeS_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateMensajeS_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateMensajeS_MS]

/****** Object:  StoredProcedure [dbo].[sw_mensajes_add_s01_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sw_mensajes_add_s01_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sw_mensajes_add_s01_MS]

/****** Object:  StoredProcedure [dbo].[sw_configura_s01_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sw_configura_s01_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sw_configura_s01_MS]

/****** Object:  StoredProcedure [dbo].[proc_trae_tipo_campos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_tipo_campos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_trae_tipo_campos_MS]

/****** Object:  StoredProcedure [dbo].[proc_trae_tipo_campos]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_tipo_campos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_trae_tipo_campos]

/****** Object:  StoredProcedure [dbo].[proc_trae_formato_campos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_formato_campos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_trae_formato_campos_MS]

/****** Object:  StoredProcedure [dbo].[proc_trae_formato_campos]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_formato_campos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_trae_formato_campos]

/****** Object:  StoredProcedure [dbo].[proc_trae_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_bancos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_trae_bancos_MS]

/****** Object:  StoredProcedure [dbo].[proc_trae_bancos]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_bancos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_trae_bancos]

/****** Object:  StoredProcedure [dbo].[proc_sw_valida_estado_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_valida_estado_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_valida_estado_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_valoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_valoresCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_valoresCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_usuarios_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_usuarios_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_TiposMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_TiposMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_paridades_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_paridades_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_paridades_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_paridad_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_paridad_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_paridad_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_monedas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_monedas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_GlosaCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_GlosaCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_FormatoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_folio_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_folio_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_folio_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_fmt_largo_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_fmt_largo_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_fmt_largo_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_fmt_ciclos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_fmt_ciclos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_fmt_ciclos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_fmt_campos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_fmt_campos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_fmt_campos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_firma]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_firma]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_firma]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_casillas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_casillas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_CaracterInvalido_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_CampoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_CampoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_bancos_verificados_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_bancos_verificados_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_bancos_verificados_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_bancos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_bancos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_resumen_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_resumen_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_resumen_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_ree_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_ree_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_ree_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_por_id_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_por_id_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_por_id_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_por_id]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_por_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_por_id]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_otr_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_otr_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_otr_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_iny_rango_paginado_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_iny_rango_paginado_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_iny_rango_paginado_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_iny_rango_paginado]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_iny_rango_paginado]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_iny_rango_paginado]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_iny_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_iny_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_iny_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_imp_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_imp_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_imp_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_enc_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_enc_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_enc_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_datos_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_datos_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_datos_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_cnf_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_cnf_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_cnf_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_enc_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_enc_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_graba_enc_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_estadist_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_estadist_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_estadist_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_control_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_control_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_control_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_anula_enc_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_anula_enc_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_anula_enc_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_msgsend_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_msgsend_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_log_trae_msgsend_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_log_trae_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_enc_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_enc_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_log_trae_enc_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_aut_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_aut_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_log_trae_aut_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_ValoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_ValoresCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_ValoresCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_usuarios_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_usuarios_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_TiposMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_TiposMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_monedas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_monedas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_GlosaCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_GlosaCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_FormatoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_casillas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_casillas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_CaracterInvalido_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_CampoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_CampoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_bancos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_graba_bancos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_rech_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_rech_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_rech_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_pro_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_pro_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_pro_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_nul_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_nul_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_nul_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_mod_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_mod_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_mod_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_ing_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_ing_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_ing_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_firmas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_firmas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_firmas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_files_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_files_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_files_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango_paginado_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango_paginado_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_env_rango_paginado_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango_paginado]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango_paginado]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_env_rango_paginado]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_env_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_dev_firma_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_dev_firma_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_dev_firma_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_datos_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_datos_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_datos_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_blo_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_blo_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_blo_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_aut_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_aut_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_aut_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_aut_pend_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_aut_pend_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_aut_pend_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_aup_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_aup_rango_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_trae_aup_rango_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_sap_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_sap_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_graba_sap_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_rec_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_rec_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_graba_rec_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_dev_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_dev_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_graba_dev_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_apr_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_apr_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_graba_apr_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_estadist_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_estadist_msg_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_estadist_msg_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_del_firnul_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_del_firnul_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_del_firnul_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_cons_sim_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_cons_sim_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_cons_sim_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_env_cons_fipe_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_cons_fipe_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_env_cons_fipe_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_ValoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_ValoresCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_ValoresCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_usuarios_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_usuarios_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_TiposMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_TiposMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_TiposCambios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_TiposCambios_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_TiposCambios_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_monedas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_monedas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_GlosaCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_GlosaCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_FormatoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_casillas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_casillas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_CaracterInvalido_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_CampoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_CampoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_bancos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_bancos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_valoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_valoresCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_valoresCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_usuarios_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_usuarios_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_TiposMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_TiposMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_monedas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_monedas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_GlosaCampos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_GlosaCampos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_FormatoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_casillas_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_casillas_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_CaracterInvalido_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_CampoMensajes_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_CampoMensajes_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_bancos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_bancos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_bancos_claves_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_bancos_claves_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_Actualiza_bancos_claves_MS]

/****** Object:  StoredProcedure [dbo].[origen_recep_s01_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[origen_recep_s01_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[origen_recep_s01_MS]

/****** Object:  StoredProcedure [dbo].[EncriptaMensajeS_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EncriptaMensajeS_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EncriptaMensajeS_MS]

/****** Object:  StoredProcedure [dbo].[EncriptaMensaje_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EncriptaMensaje_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EncriptaMensaje_MS]

/****** Object:  StoredProcedure [dbo].[DesencriptaMensajeS_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DesencriptaMensajeS_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DesencriptaMensajeS_MS]

/****** Object:  StoredProcedure [dbo].[DesencriptaMensaje_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DesencriptaMensaje_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DesencriptaMensaje_MS]

/****** Object:  StoredProcedure [dbo].[DesencriptaMensaje_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DesencriptaMensaje_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[DesencriptaMensaje_MS] 
	@sesion int, 
	@secuencia int
	AS
	BEGIN
	/*	
	Este procedimiento desencripta un mensaje de la tabla sw_mensajes

	Historial:
							Migración desde Sybase (AKZIO)
		2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	SET NOCOUNT ON 



	declare @i int,@ind int,@valor_aux char(1), @largo_llave int,@clave varchar(255);
	declare @mensaje varchar(MAX),  @mensaje_sal varchar(MAX);
	declare @monto varchar(6);
	declare @bin_aux binary(8);
	declare  @cant_reg int, @men_aux char(1),@j int;

	--Agrega ceros a secuencia si el largo es menor que 6

	if (@secuencia >= 0 and @secuencia < 10)
		select @monto = ''00000'' + convert(varchar,@secuencia);
	else
	if (@secuencia >= 10 and @secuencia < 100)
		select @monto = ''0000'' + convert(varchar,@secuencia);
	else
	if (@secuencia >= 100 and @secuencia < 1000)
		select @monto = ''000'' + convert(varchar,@secuencia);
	else
	if (@secuencia >= 1000 and @secuencia < 10000)
		select @monto = ''00'' + convert(varchar,@secuencia);
	else
	if (@secuencia >= 10000 and @secuencia < 100000)
		select @monto = ''0'' + convert(varchar,@secuencia);
	else
	if (@secuencia >= 100000 and @secuencia < 1000000)
		select @monto = convert(varchar,@secuencia);
	else
		select 9;
		
	--Crea tabla temporal que almacenara la clave
	create table #clave(
		indic int PRIMARY KEY CLUSTERED, 
		valor char(1),
		clave char(1) NULL,
		clave_e char(1) NULL);
 
	-- Inicia valore para encriptar llave  
	select @i = 1;
	select @clave = NULL;
	while  ( @i < 257)
	begin
		insert into #clave (indic,valor) values (@i,char(@i-1));
		select @i = @i + 1;
	end;


	select @largo_llave = len(@monto);
   
	select @i = 1;
	select @ind = 1;
	while  ( @i < 257)
	begin
		update #clave set clave = substring(@monto,@ind,1) where indic = @i;
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
		select @valor_aux = valor, @monto = clave from #clave where indic = @i; 
		select @bin_aux = (ascii(@valor_aux)) ^ (ascii(@monto));
		update #clave set clave_e = char(@bin_aux) where indic = @i;
	  
		select @i = @i + 1;
		select @ind =@ind + 1;
		if (@ind = @largo_llave)
			select @ind = 0;
	end

	-- Valida si mensaje existe
	if not exists (SELECT TOP 1 1 from sw_mensajes where sesion = @sesion and secuencia = @secuencia)
	begin
		drop table #clave;
		return 1
	end   
   
	--Obtiene el mensaje desda la tabla sw_mensajes
   
	select @mensaje = mensaje from sw_mensajes where sesion = @sesion and secuencia = @secuencia;
   
	--Agrega caracteres eliminados surante la encrpitacion

	-- set @mensaje = replace(@mensaje,''##'',char(35));
	-- set @mensaje = replace(@mensaje,''#$'',char(34));
	-- set @mensaje = replace(@mensaje,''#%'',char(00));
	-- set @mensaje = replace(@mensaje,''#&'',char(10));

	create table #mensajes_aux (
		indic int PRIMARY KEY CLUSTERED, 
		caracter char(1) 
	);
   
	select @i = 1;
	select @j = 1;

	while (@i <= len(@mensaje))
	begin
		if substring(@mensaje,@i,1) <> ''#''
			insert into #mensajes_aux values (@j,substring(@mensaje,@i,1));
		else
		begin
			select @i = @i + 1;
			if (substring(@mensaje,@i,1) = ''#'')
			insert into #mensajes_aux values (@j,char(35));
		else
			if (substring(@mensaje,@i,1) = ''$'')
				insert into #mensajes_aux values (@j,char(34));
			else
				if (substring(@mensaje,@i,1) = ''&'')
					insert into #mensajes_aux values (@j,char(10));
				else
					insert into #mensajes_aux values (@j,char(00));
		end
		select @i = @i + 1;
		select @j = @j + 1;
	end
		
	--Desencripta mensaje
 
	select @i = 1;
	select @ind = 1;
	select @mensaje_sal = '''';
	select @cant_reg = count(*) from #mensajes_aux;
   
	while (@i <= @cant_reg)
	begin
		select @valor_aux = clave_e from #clave where indic = @ind;
		select @men_aux = caracter from #mensajes_aux where indic = @i; 
		select @bin_aux = (ascii(@men_aux)) ^ (ascii(@valor_aux));
		select @mensaje_sal = @mensaje_sal + char(@bin_aux);
	  
		select @i = @i + 1;
		select @ind =@ind + 1;
		if (@ind = 257)
			select @ind = 1;
	end

	-- Elimina tabla temporal
	drop table #clave;
	drop table #mensajes_aux;

	--Retorna mensaje desencriptado
	select @mensaje_sal;

	end
	' 
END

/****** Object:  StoredProcedure [dbo].[DesencriptaMensajeS_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DesencriptaMensajeS_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[DesencriptaMensajeS_MS] 
		@valor int,
		@llave varchar(100)
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 


		declare @i int,@j int,@clave varchar(255);
		declare @mensaje varchar(MAX),  @mensaje_sal varchar(MAX);;
		declare @mensaje_aux char(1),@aux char(1),@f int,@ran int,@e int;

		set nocount on
		
		select @i = 1;
		
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

		-- Valida si mensaje existe
		if not exists (SELECT TOP 1 1 from sw_msgsend where id_mensaje = @valor)
		begin
			drop table #llave;
			return 1
		end   
   
		select @mensaje = mensaje from sw_msgsend where id_mensaje = @valor;
	   
		select @ran = 0;
		select @j = 0;
		select @f = 1;
		select @mensaje_sal = '''';
		   
		while (@f <= len(@mensaje))
		begin
				if( @j = len(@llave)) 
						select @j = 0;
				if( @ran > 9 ) 
						select @ran = 0;
					 
				select @mensaje_aux = char(ascii(substring(@mensaje,@f,1)) ^ @ran);
				select @aux = valor from #llave where indic = @j;
				select @e = ( ascii(@mensaje_aux) - ascii(@aux) - @ran);
				
				if( @e < 0 ) 
					select @e = @e + 256;
				  
				select @mensaje_sal = @mensaje_sal + char(@e);
			   
				select @f = @f + 1;
				select @j = @j + 1;
				select @ran = @ran + 1;
		end
		
		drop table #llave;


		select @mensaje_sal


	end
	' 
END

/****** Object:  StoredProcedure [dbo].[EncriptaMensaje_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EncriptaMensaje_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EncriptaMensaje_MS] (
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


	end
	' 
END

/****** Object:  StoredProcedure [dbo].[EncriptaMensajeS_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EncriptaMensajeS_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EncriptaMensajeS_MS] (
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
			return 0;	

									   
	end
	' 
END

/****** Object:  StoredProcedure [dbo].[origen_recep_s01_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[origen_recep_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[origen_recep_s01_MS]  
    AS
		SELECT cod_casilla, nombre_casilla, origen_recep 
		FROM sw_casillas WHERE origen_recep <> ''A''' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_bancos_claves_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_bancos_claves_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_Actualiza_bancos_claves_MS]
	@intercambio_clave nvarchar(50)=null,@cod_banco nvarchar(50)=null,@branch nvarchar(50)=null,@Flag int
	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		if(@Flag=0)
		begin
	   UPDATE sw_bancos SET intercambio_clave = @intercambio_clave 
	   WHERE cod_banco = @cod_banco and branch = @branch
	   end
	  if(@Flag=1)
		begin
		  UPDATE sw_bancos SET intercambio_clave = @intercambio_clave
		  WHERE cod_banco = @cod_banco
	   end
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_Actualiza_bancos_MS]
@Codigo nvarchar(50),@nombre nvarchar(50),@direccion nvarchar(50),@Ciudad nvarchar(50),@Oficina nvarchar(50),@clave nvarchar(50),
@Localidad nvarchar(50),@Pob nvarchar(2),@Pais nvarchar(50),@branch nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	declare @bit BIT
	BEGIN TRY
	UPDATE sw_bancos
SET nombre_banco=@nombre,
	direccion_banco=@direccion,
	ciudad_banco=@Ciudad,
	pais_banco=@Pais,
	oficina_banco=@Oficina,
	intercambio_clave=@clave,
	localidad_banco=@Localidad,
	pobnr_banco=@Pob
WHERE cod_banco=@Codigo and branch=@branch;
set @bit=1
END TRY
BEGIN CATCH
set @bit=0
END CATCH
select @bit as ''status''
END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_CampoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_Actualiza_CampoMensajes_MS]
	@Codigo nvarchar(4),@Linea int,@Nombre nvarchar(50),@Largo int,@Formato nvarchar(35)
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_campos_msg
	SET nombre_campo=@Nombre,
		largo_campo=@Largo,
		formato_campo=@Formato
	WHERE tag_campo=@Codigo and linea_campo=@Linea;
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_Actualiza_CaracterInvalido_MS]
	@Codigo int,@Nombre nvarchar(20)	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_caracter_error
	SET descripcion=@Nombre
		where valor_ascii=@Codigo
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_casillas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	<SP utilizado para actualziar los registros 
	--de las casillas de la tabla sw_casillas utilizado en el aplicativo mantenedores Swift>
	-- =============================================
	create PROCEDURE [dbo].[proc_sw_Actualiza_casillas_MS]
	@codigo INT,
		@nombre nvarchar(50),
		@origen nvarchar(2)
	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_casillas
	SET sw_casillas.nombre_casilla=@nombre,sw_casillas.origen_recep=@origen
	WHERE sw_casillas.cod_casilla=@codigo;
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[proc_sw_Actualiza_FormatoMensajes_MS]
	@Codigo nvarchar(50),@Orden int,@Secuencia nvarchar(15),@Repeticion int,@Tag nvarchar(15),@Status nvarchar(2),
	@OrdenOriginal int,@TagOriginal nvarchar(15)
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_formatos
	SET orden_fmt=@Orden,
		secuencia_fmt=@Secuencia,
		repeticion_fmt=@Repeticion,
		tag_fmt=@Tag,
		status_fmt=@Status
	WHERE tipo_msg_fmt=@Codigo and orden_fmt=@OrdenOriginal and tag_fmt=@TagOriginal;
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_GlosaCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[proc_sw_Actualiza_GlosaCampos_MS]
	@Codigo nvarchar(10),@Tag nvarchar(5),@Nombre nvarchar(50)	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_tipos_campos
	SET nombre_campo_tipcam=@Nombre
		where tipo_msg_tipcam=@Codigo and tag_campo_tipcam=@Tag
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_monedas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	<SP utilizado para actualziar los registros 
	--de las monedas de la tabla sw_monedas utilizado en el aplicativo mantenedores Swift>
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_Actualiza_monedas_MS]
	@CodigoSwMoneda nvarchar(50),@NombreMoneda nvarchar(50)=null,@UsoMoneda nvarchar(2),@CodigoBcMoneda int=null,@DecimalesMoneda int
	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_monedas
	SET sw_monedas.cod_moneda_banco=@CodigoBcMoneda,
		sw_monedas.uso_moneda_banco=@UsoMoneda,
		sw_monedas.decimales=@DecimalesMoneda,
		sw_monedas.nombre_moneda=@NombreMoneda
	WHERE sw_monedas.cod_moneda_sw=@CodigoSwMoneda;
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_TiposMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_Actualiza_TiposMensajes_MS]
	@Codigo nvarchar(20),
		@Nombre nvarchar(50)
	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_tipos_msg
	SET sw_tipos_msg.nombre_tipo=@Nombre
	WHERE sw_tipos_msg.cod_tipo=@Codigo;
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_usuarios_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Create date: <01 Octubre 2015>
	-- Description:	<SP utilizado para actualziar los registros 
	--de los usuarios de la tabla sw_users_swift utilizado en el aplicativo mantenedores Swift>
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_Actualiza_usuarios_MS]
	@p_rut INT,
		@p_nombre nvarchar(50),
		@p_tipo nvarchar(50)
	
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_users_swift
	SET sw_users_swift.nombre_user=@p_nombre,sw_users_swift.tipo_user=@p_tipo
	WHERE sw_users_swift.rut_user=@p_rut;
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_Actualiza_valoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_Actualiza_valoresCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_Actualiza_valoresCampos_MS]
	@Tipo nvarchar(20),@Tag nvarchar(10),@Condicion nvarchar(10),@valor nvarchar(255), @Linea int, @Total_valor	int
	AS
	BEGIN
		SET NOCOUNT ON;
		declare @bit BIT
		BEGIN TRY
		UPDATE sw_valor_campos
	SET cond_valor=@Condicion,
		valor_campo=@valor,
		total_valor=@Total_valor
		where tipo_msg=@Tipo and tag_campo=@Tag and linea_campo=@Linea
	set @bit=1
	END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_bancos_MS] 
	@flag INT,
	@codigo nvarchar(20),
	@branch nvarchar(20)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		if(@flag=0)
		begin
		delete from dbo.sw_bancos where cod_banco=@codigo and branch=@branch
		end
		if(@flag=1)
		begin
		delete from dbo.sw_bancos where cod_banco=@codigo
		end
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_CampoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_CampoMensajes_MS] 
	@Codigo  nvarchar(4),
	@Linea int
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;	
		delete from dbo.sw_campos_msg where tag_campo=@Codigo and linea_campo=@Linea
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_CaracterInvalido_MS] 
	@codigo int
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_caracter_error where valor_ascii=@codigo
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_casillas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	Elimina Casilla SWI_MAN 
	-- =============================================
	create PROCEDURE [dbo].[proc_sw_elimina_casillas_MS] 
	@codigo INT
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_casillas where cod_casilla=@codigo
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_FormatoMensajes_MS] 
	@Codigo  nvarchar(20),
	@Orden int,
	@Tag nvarchar(3)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;	
		delete from dbo.sw_formatos where tipo_msg_fmt=@Codigo and orden_fmt=@Orden and tag_fmt=@Tag
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_GlosaCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[proc_sw_elimina_GlosaCampos_MS] 
	@Codigo nvarchar(15),
	@Tag nvarchar (15)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_tipos_campos where tipo_msg_tipcam=@Codigo and tag_campo_tipcam=@Tag
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_monedas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	Elimina moneda SWI_MAN 
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_elimina_monedas_MS] 
	@codigo nvarchar(50)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_monedas where cod_moneda_sw=@codigo
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_TiposCambios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_TiposCambios_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_TiposCambios_MS] 
	@codigo nvarchar(20)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_tipos_msg where cod_tipo=@codigo
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_TiposMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_TiposMensajes_MS] 
	@codigo nvarchar(20)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_tipos_msg where cod_tipo=@codigo
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_usuarios_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	Elimina usuario SWI_MAN 
	-- =============================================
	create PROCEDURE [dbo].[proc_sw_elimina_usuarios_MS] 
	@p_rut INT
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_users_swift where rut_user=@p_rut
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_ValoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_ValoresCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_ValoresCampos_MS] 
	@codigo nvarchar(30),
	@tag nvarchar (15),
	@linea int
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;
		delete from dbo.sw_valor_campos where tipo_msg=@codigo and tag_campo=@tag and linea_campo=@linea
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_cons_fipe_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_cons_fipe_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_cons_fipe_MS] 
		@id_mensaje	INT,
		@p_rut_log 	INT,
		@monto_swift 	FLOAT,
		@par1 varchar(255) = NULL --AKZ001 no se utiliza
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   declare @cont		INT,@regla		CHAR(10),@tipfir		CHAR(1),@sigdo		BIT,@sigcur		BIT,
	   @rr		CHAR(1),@esta		CHAR(1),@rutact		INT,@rutant		INT,@hayfir		INT,@monto_usd	FLOAT,
	   @cod_mnda_sw 	CHAR(3),@cod_mnda_bco 	CHAR(3),@paridad	FLOAT,
	   @firmas_cursor  CHAR(4),@est_fir	CHAR(1),@totfir		INT
	   SELECT   @sigdo  = 1
	   SELECT   @sigcur = 1
	   SELECT   @cont = 0
	   SELECT   @rutact = 0
	   SELECT   @rutant = 0
	   SELECT   @hayfir = 0

	   SELECT   @totfir = count(*)
	   FROM	dbo.sw_msgsend_firma
	   WHERE	id_mensaje = @id_mensaje AND
	   estado_firma = ''P'' 
	/*AND @p_rut_log <> rut_firma*/

	   IF @totfir > 1
		  SELECT   0
	else
	   begin

		  if @monto_swift > 0
		  begin
			/*Rutina para calcular el monto en dolares con la parid diaria*/
			 select   @cod_mnda_sw = cod_moneda
			 from    dbo.sw_msgsend
			 where   id_mensaje = @id_mensaje
			 select   @cod_mnda_bco = cod_moneda_banco
			 from    dbo.sw_monedas
			 where   cod_moneda_sw = @cod_mnda_sw
			 select   @paridad = paridad
			 from    dbo.sw_paridad
			 where   cod_moneda_banco = @cod_mnda_bco
			 if @paridad = 0 or @paridad is null
				select   @paridad = 1
			 select   @monto_usd = Round(@monto_swift/@paridad,2)
		  end
	   else
		  select   @monto_usd = 0
	  
		  DECLARE cursor_temp CURSOR FOR
		  SELECT  isnull(firma1,'''')+isnull(firma2,'''')+isnull(firma3,'''')+isnull(firma4,'''') regla
		  FROM    dbo.sw_regla_firmas
		  WHERE	monto_fin >= @monto_usd
		  ORDER BY regla desc
	  
		/*Cursor con todas las firmas necesarias*/
		  OPEN cursor_temp
		  FETCH cursor_temp INTO
		  @firmas_cursor
		  WHILE @@FETCH_STATUS != -1 AND @sigcur = 1
		  begin
			/*Inicializo Variables*/
			 SELECT   @rutact = 0
			 SELECT   @rutant = 0
			 SELECT   @sigdo  = 1
			 SELECT   @cont = 0
			 WHILE @sigdo = 1 /*Recorro el registro de la firma por caractr*/
			 begin
				SELECT   @cont = @cont+1
				IF @cont <= LEN(@firmas_cursor)
				begin
				   SELECT   @rr = SUBSTRING(@firmas_cursor,@cont,1)
				   SELECT   @totfir = count(*)
				   FROM 	dbo.sw_msgsend_firma
				   WHERE id_mensaje = @id_mensaje
				   SELECT   @rutact = rut_firma,
						@est_fir = estado_firma
				   FROM 	dbo.sw_msgsend_firma
				   WHERE 	id_mensaje = @id_mensaje AND
				   tipo_firma = @rr 	 AND
				   rut_firma <> @rutant
				   IF @rutact IS NOT NULL AND @rutact <> 0
					  IF @rutact <> @rutant
					  begin
						 SELECT   @rutant = @rutact
						 IF @cont = LEN(@firmas_cursor)
						 begin
							SELECT   @hayfir = 1
							SELECT   @sigdo = 1
							SELECT   @rutant = @rutact
						 end
					  ELSE IF @est_fir = ''P'' AND @rutact <> @p_rut_log
						 begin
							SELECT   @hayfir = 1
							SELECT   @sigdo = 1
							SELECT   @rutant = @rutact
						 end
					  ELSE IF @cont < @totfir
							SELECT   @sigdo = 1
					  ELSE
						 SELECT   @sigdo = 0
					  end
				   ELSE
					  SELECT   @sigdo = 0
				ELSE
				   SELECT   @sigdo = 0
				end
				IF @sigdo = 1 AND @cont = LEN(@firmas_cursor) OR @hayfir = 1
				begin
				   SELECT   @esta = ''S''
				   SELECT   @sigcur = 0
				   SELECT   @sigdo  = 0
				end
			 end
			 FETCH cursor_temp INTO
			 @firmas_cursor
		  end

		-- Se cierra el cursor.-
		  CLOSE cursor_temp
		  DEALLOCATE cursor_temp

		-- Se verifica la existencia de errores.-
		  IF (@@error <> 0)
			 RETURN 9
	   ELSE IF @esta = ''S''
			 SELECT   0
	   ELSE
		  SELECT   1
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_cons_sim_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_cons_sim_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_cons_sim_MS] 
		@id_mensaje INT,
		@p_rut_log INT 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   declare @casilla_actual INT,@estado_actual  CHAR(3),@estado_nuevo   CHAR(3),@tipo_firma CHAR(10),
	   @c   INT
	   SELECT   @c = count(*)
	   FROM    dbo.sw_msgsend_firma
	   WHERE   id_mensaje = @id_mensaje and
	   revisa_firma = ''F'' and
	   avisado = ''S'' and
	   estado_firma = ''P''               

	   IF @c = 0
	-- AKZ001  SELECT right(''00000000''+convert(VARCHAR(8),rut_firma),8)
		  SELECT   right(''00000000''+convert(VARCHAR(8),rut_firma),8)
		  FROM dbo.sw_msgsend_firma
		  WHERE   id_mensaje = @id_mensaje and
		  revisa_firma = ''F'' and
		  avisado = ''N'' and
		  estado_firma = ''P''
		  ORDER BY tipo_firma DESC
	ELSE
	   SELECT   ''00000000''
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_del_firnul_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_del_firnul_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_del_firnul_MS] 
		@p_id_mensaje 	INT, 
		@p_rut_solic 	INT, 
		@p_fecha_solic 	datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   delete dbo.sw_msgsend_firma
	   where  id_mensaje   = @p_id_mensaje
	   and  estado_firma = ''N''
	   and  fecha_solic <= @p_fecha_solic
  
	   if @@error <> 0
		  return -1
	else
	   return 0
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_estadist_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_estadist_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_estadist_msg_MS] 
		@p_casilla 	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

	 DECLARE @p_fecha1_2 DATETIME = dateadd(dd,0,@p_fecha1)  
	 DECLARE @p_fecha2_2 DATETIME = dateadd(dd,+1,@p_fecha2)  
	 declare @min_id_mensaje int  
	 declare @max_id_mensaje int  
  
	 SELECT   
	  @min_id_mensaje = MIN(id_mensaje),   
	  @max_id_mensaje = MAX(id_mensaje)  
	 FROM dbo.sw_msgsend_detfile df with (index=sk2_msgsend_detfile) -- Se requiere este hint para evitar que haga un scan utilizando el indice por id_mensaje  
	 WHERE  df.fecha_envio >= @p_fecha1_2 and df.fecha_envio < @p_fecha2_2  

	   if @p_casilla = 0
	   begin
		  SELECT   m.casilla,
			c.nombre_casilla,
			m.tipo_msg,
			isnull(convert(VARCHAR,t.nombre_tipo),'''') as nombre_tipo,
			m.cod_banco_em,
			m.branch_em,
			b.nombre_banco,
			count(*) as cantidad
		  FROM 	dbo.sw_msgsend m 
		  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo, 
			dbo.sw_msgsend_detfile d, 
			dbo.sw_casillas c, 
			dbo.sw_bancos b
		  WHERE  
				d.fecha_envio >= @p_fecha1_2
			AND d.fecha_envio < @p_fecha2_2
			AND d.id_mensaje    =   m.id_mensaje 
			AND c.cod_casilla   =   m.casilla 
			AND m.cod_banco_em  =   b.cod_banco 
			AND m.branch_em     =   b.branch
			AND m.id_mensaje >= @min_id_mensaje   
			AND m.id_mensaje <= @max_id_mensaje   
			AND d.id_mensaje >= @min_id_mensaje   
			AND d.id_mensaje <= @max_id_mensaje   
		  GROUP BY   m.casilla,c.nombre_casilla,m.tipo_msg,t.nombre_tipo,m.cod_banco_em,m.branch_em, b.nombre_banco
		  ORDER BY m.casilla ASC,m.tipo_msg ASC
	   end
	else
	   begin
		  SELECT   m.casilla,
			c.nombre_casilla,
			m.tipo_msg,
			isnull(convert(VARCHAR,t.nombre_tipo),'''') as nombre_tipo,
			m.cod_banco_em,
			m.branch_em,
			b.nombre_banco,
			count(*) as cantidad
		  FROM    dbo.sw_msgsend m 
		  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo, 
			dbo.sw_msgsend_detfile d, 
			dbo.sw_casillas c, 
			dbo.sw_bancos b
		  WHERE  
				m.casilla        =   @p_casilla 	      
			AND d.fecha_envio >= @p_fecha1_2
			AND d.fecha_envio < @p_fecha2_2
			AND d.id_mensaje     =   m.id_mensaje 
			AND c.cod_casilla    =   m.casilla 
			AND m.cod_banco_em   =   b.cod_banco 
			AND m.branch_em      =   b.branch
			AND m.id_mensaje >= @min_id_mensaje   
			AND m.id_mensaje <= @max_id_mensaje   
			AND d.id_mensaje >= @min_id_mensaje   
			AND d.id_mensaje <= @max_id_mensaje
		  GROUP BY    m.casilla,c.nombre_casilla,m.tipo_msg,t.nombre_tipo,m.cod_banco_em,m.branch_em, b.nombre_banco
		  ORDER BY m.casilla ASC,m.tipo_msg ASC
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_apr_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_apr_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_apr_MS] 
		@id_mensaje INT,
		@p_rut_log INT 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 


	/* Procedimiento que firma un Swift   */
	   declare @casilla_actual INT,@estado_actual  CHAR(3),@estado_nuevo   CHAR(3),@rev_firma      CHAR(3),
	   @est_firma      CHAR(1),@monto_swift	FLOAT,@cont		INT,
	   @todas		CHAR(1),@comentario     VARCHAR(90),@fecha_firma    datetime,
	   @fecha_aprob    datetime,@tot_firmas	INT
	BEGIN tran

	   SELECT   @fecha_firma = GetDate()
	   SELECT   @cont   = 0

	   SELECT   @estado_actual = estado_msg,
		@casilla_actual = casilla,
		@monto_swift = monto
	   FROM 	dbo.sw_msgsend
	   WHERE 	id_mensaje = @id_mensaje

	   SELECT   @rev_firma = revisa_firma,
		@est_firma = estado_firma
	   FROM 	dbo.sw_msgsend_firma
	   WHERE   id_mensaje = @id_mensaje and
	   rut_firma  = @p_rut_log

	   if @est_firma <> ''F''
	   begin
		  update dbo.sw_msgsend_firma set estado_firma = ''F'',fecha_firma = @fecha_firma  WHERE   id_mensaje = @id_mensaje and
		  rut_firma  = @p_rut_log
		  IF (@@error <> 0) or (@@rowcount = 0)
		  begin
			 if @@rowcount = 0
			 begin
				ROLLBACK 
				RETURN -1
			 end
		  else
			 begin
				ROLLBACK 
				RETURN -2
			 end
		  end
	   end

	   SELECT   @fecha_aprob = fecha_aprobac
	   FROM    dbo.sw_msgsend_add
	   WHERE   id_mensaje = @id_mensaje

	   EXECUTE dbo.proc_sw_env_cons_fito @id_mensaje,@monto_swift,@todas OUTPUT
	   if @todas = ''S'' and @fecha_aprob IS NOT NULL
	   begin
		  commit tran
		  return 0
	   end

	   if @rev_firma = ''F''
	   BEGIN
		  EXECUTE dbo.proc_sw_env_cons_fito @id_mensaje,@monto_swift,@todas OUTPUT
		  if @todas = ''S''
		  begin
			 SELECT   @estado_nuevo = ''AUT''
			 update dbo.sw_msgsend set estado_msg = @estado_nuevo,comentario = ''Mensaje Autorizado Automaticamente''  WHERE 	id_mensaje = @id_mensaje
			 if @@error <> 0 or @@rowcount = 0
			 begin
				ROLLBACK 
				RETURN -3
			 end
			 if not exists(SELECT TOP 1 1 from dbo.sw_msgsend_add
			 where id_mensaje = @id_mensaje)
				INSERT INTO dbo.sw_msgsend_add(id_mensaje, fecha_aprobac,
				  veces_rechazo, veces_modifica, veces_bloqueo)
				VALUES(@id_mensaje, @fecha_firma, 0, 0, 0)
		
		  else
			 update dbo.sw_msgsend_add set fecha_aprobac = @fecha_firma  WHERE 	id_mensaje = @id_mensaje
			 if (@@error <> 0) or (@@rowcount = 0)
			 begin
				ROLLBACK 
				RETURN -4
			 end
		  end
	   else
		  begin
			 select   @tot_firmas = count(*)
			 from   	dbo.sw_msgsend_firma
			 where  	id_mensaje = @id_mensaje and
			 estado_firma = ''D''
			 if @tot_firmas > 0
			 begin
				SELECT   @estado_nuevo = ''DEV''
				SELECT   @comentario = ''Mensaje Devuelto''
			 end
		  else
			 begin
				SELECT   @estado_nuevo = ''AUP''
				SELECT   @comentario = ''Mensaje en Proceso de Autorización''
			 end
			 update dbo.sw_msgsend set estado_msg = @estado_nuevo,comentario = @comentario  WHERE id_mensaje = @id_mensaje
			 if (@@error <> 0) or (@@rowcount = 0)
			 begin
				ROLLBACK 
				RETURN -4
			 end
		  end
	   END
	ELSE
	   BEGIN
		  select   @estado_nuevo = ''AUP''
		  select   @comentario = ''Mensaje en Proceso de Autorización''
		  update dbo.sw_msgsend set estado_msg = @estado_nuevo,comentario = @comentario  WHERE 	id_mensaje = @id_mensaje
		  if (@@error <> 0) or (@@rowcount = 0)
		  begin
			 ROLLBACK 
			 RETURN -4
		  end
	   END

	   INSERT INTO dbo.sw_msgsend_log
	VALUES(@id_mensaje,	@fecha_firma, @p_rut_log,
		 left(@@servername, 10),	''GS24'',	 ''firmamsg'',
		 @casilla_actual, @estado_actual,  @casilla_actual,
		 @estado_nuevo,  @casilla_actual,  ''A'',
		 ''Mensaje Firmado o Revisado'')

	   if (@@error = 0) and (@@rowcount <> 0)
	   begin
		  COMMIT tran
		  RETURN 0
	   end
	else
	   begin
		  ROLLBACK 
		  RETURN -5
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_dev_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_dev_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_dev_MS] 
	   @id_mensaje 	   INT, 
	   @p_rut_log 	   INT, 
	   @p_casilla	   INT,
	   @p_estado 	   CHAR(3) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   declare @casilla_actual INT,@estado_actual  CHAR(3),@opcion_log     CHAR(8)
	BEGIN TRAN

	   SELECT   @casilla_actual = casilla,
		  @estado_actual  = estado_msg
	   FROM dbo.sw_msgsend
	   WHERE id_mensaje = @id_mensaje
 
	   update dbo.sw_msgsend set estado_msg = @p_estado  WHERE (id_mensaje = @id_mensaje)

	   IF (@@error = 0) AND (@@rowcount <> 0)
	   BEGIN
		  update dbo.sw_msgsend_firma set estado_firma = ''D''  WHERE (id_mensaje = @id_mensaje and
		  rut_firma = @p_rut_log)
		  IF (@@error = 0) AND (@@rowcount <> 0)
		  BEGIN
			 select   @opcion_log = lower(@estado_actual)+''gr''+lower(@p_estado)
			 INSERT INTO dbo.sw_msgsend_log
			VALUES(@id_mensaje,
				GetDate(),
				@p_rut_log,
				left(@@servername, 10),
				''GS24'',
				@opcion_log,
				@casilla_actual,
				@estado_actual,
				@p_casilla,
				@p_estado,
				@p_casilla,
				''A'',
				''Mensaje Devuelto por Firma'')
			 
			 if (@@error = 0) and (@@rowcount <> 0)
			 BEGIN
				COMMIT tran
				RETURN 0
			 END
		  ELSE
			 BEGIN
				ROLLBACK 
				RETURN -4
			 END
		  END
	   ELSE
		  BEGIN
			 ROLLBACK 
			 RETURN -1
		  END
	   end
	ELSE
	   BEGIN
		  ROLLBACK 
		  RETURN -2
	   END
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_rec_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_rec_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_rec_MS] 
		@id_mensaje 	   INT, 
	   @p_rut_log 	   INT, 
	   @p_casilla	   INT,
	   @p_comentario1  VARCHAR(80),
	   @p_comentario2  VARCHAR(80),
	   @p_estado 	   CHAR(3) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/

		SET NOCOUNT ON 

		declare @casilla_actual INT, @estado_actual CHAR(3), @opcion_log CHAR(8), @fd_archivo INT
	
		BEGIN TRAN

			SELECT	@casilla_actual = casilla,
					@estado_actual  = estado_msg
			FROM 	dbo.sw_msgsend
			WHERE	id_mensaje = @id_mensaje
 
			/* Actualiza Tabla Principal */
			update dbo.sw_msgsend set estado_msg = @p_estado,comentario = @p_comentario1  WHERE (id_mensaje = @id_mensaje)
			if (@@error <> 0) or (@@rowcount = 0)
			begin
				ROLLBACK 
				return -1
			end

			/* Actualiza Tabla Secundaria */
			IF not exists(SELECT TOP 1 1 FROM dbo.sw_msgsend_add WHERE id_mensaje = @id_mensaje)
			BEGIN
				INSERT INTO dbo.sw_msgsend_add(	id_mensaje,		unidad_rechazo,
												rut_rechazo,    fecha_rechazo,
												texto_rechazo,  veces_rechazo,
												veces_modifica, veces_bloqueo)
										VALUES(@id_mensaje,     @p_casilla,
												@p_rut_log,     GetDate(),
												@p_comentario2, 1,
												0,				0)
			END
			ELSE
			BEGIN
				update dbo.sw_msgsend_add 
				set unidad_rechazo	= @p_casilla,
					rut_rechazo		= @p_rut_log,
					fecha_rechazo	= GetDate(),
					texto_rechazo	= @p_comentario2,
					veces_rechazo  = veces_rechazo+1  
				WHERE 	id_mensaje = @id_mensaje 
				if (@@error <> 0) or (@@rowcount = 0)
				begin
					ROLLBACK 
					return -2
				end
	
				/* Elimina las firmas */

				DELETE dbo.sw_msgsend_firma WHERE id_mensaje = @id_mensaje
				if (@@error <> 0) or (@@rowcount = 0)
				begin
					ROLLBACK 
					return -3
				end
			
				/* Actualiza Tabla de Archivos */
				if @estado_actual = ''PRO''
				begin
					SELECT   @fd_archivo = fd_archivo
					FROM	dbo.sw_msgsend_detfile
					WHERE	id_mensaje = @id_mensaje
					update dbo.sw_msgsend_files set total_rechazos = total_rechazos+1  WHERE  fd_archivo = @fd_archivo
					if (@@error <> 0) or (@@rowcount = 0)
					begin
						ROLLBACK 
						return -4
					end
				end

				select   @opcion_log = lower(@estado_actual)+''gr''+lower(@p_estado)
			
				INSERT INTO dbo.sw_msgsend_log
				VALUES(@id_mensaje,
				GetDate(),
				@p_rut_log,
				left(@@servername, 10),
				''GS24'',
				@opcion_log,
				@casilla_actual,
				@estado_actual,
				@p_casilla,
				@p_estado,
				@p_casilla,
				''A'',
				@p_comentario1)

				if (@@error <> 0) or (@@rowcount = 0)
				begin
					ROLLBACK 
					RETURN -5
				end 
			
			END

			COMMIT TRAN
			RETURN 0

	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_graba_sap_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_sap_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_sap_MS] 
		@p_casilla 	INT, 
		@p_id_mensaje 	INT, 
		@p_rut_log 	INT, 
		@p_fecha_sap 	datetime,
		@p_comentario   VARCHAR(90) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 


	   DECLARE @casilla_actual INT,@estado_actual 	CHAR(3),@p_estado	CHAR(3),@p_opcion 	CHAR(8),
	   @tot_firmas	INT
	begin tran

	   SELECT   @estado_actual = estado_msg,
		@casilla_actual = casilla
	   FROM 	dbo.sw_msgsend
	   WHERE 	id_mensaje = @p_id_mensaje

	   if @estado_actual = ''INY'' or @estado_actual = ''DIG'' or @estado_actual = ''MOD''
		  select   @p_estado = ''SAP''
	else if @estado_actual = ''DEV''
	   begin
		  select   @tot_firmas = count(*)
		  from   dbo.sw_msgsend_firma
		  where  id_mensaje = @p_id_mensaje and
		  estado_firma = ''D''
		  if @tot_firmas > 0
		  begin
			 select   @p_estado = ''DEV''
			 select   @p_comentario = ''Mensaje Devuelto por Firma que no corresponde''
		  end
	   else
		  begin
			 select   @tot_firmas = count(*)
			 from   dbo.sw_msgsend_firma
			 where  id_mensaje = @p_id_mensaje and
			 estado_firma = ''F''
			 if @tot_firmas > 0
				select   @p_estado = ''AUP''
		  else
			 select   @p_estado = ''SAP''
		  end
	   end
	else
	   select   @p_estado = @estado_actual

	   update dbo.sw_msgsend set estado_msg = @p_estado,comentario = @p_comentario  WHERE 	id_mensaje = @p_id_mensaje

	   if @@error <> 0 or @@rowcount = 0
	   begin
		  rollback 
		  return -1
	   end

	   update dbo.sw_msgsend_firma set estado_firma = ''P''  WHERE 	id_mensaje   = @p_id_mensaje 	and
	   fecha_solic  <= @p_fecha_sap 	and
	   estado_firma = ''N''              and
	   rut_solic    = @p_rut_log

	   if @@error <> 0
	   begin
		  rollback 
		  return -2
	   end

	   select   @p_opcion = lower(@estado_actual)+''gr''+lower(@p_estado)

	   INSERT INTO dbo.sw_msgsend_log
	VALUES(@p_id_mensaje, GetDate(), @p_rut_log, left(@@servername, 10), ''GS24'',
		  @p_opcion, @casilla_actual, @estado_actual,
		  @casilla_actual, @p_estado, @casilla_actual, ''A'',
		  ''Solicitud de Firma o Revisión'')

	   if @@error <> 0 or @@rowcount = 0
	   begin
		  rollback 
		  return -3
	   end

	   commit tran
	   return 0
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_aup_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_aup_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_aup_rango_MS] 
		@p_casilla 	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   --select f.fecha_solic, f.id_mensaje
	   --into #tmpfirma
	   --select f.fecha_solic, f.id_mensaje
	   --from dbo.sw_msgsend_firma f
	   --where f.fecha_solic   >=  dateadd(dd,0,@p_fecha1)
	   --and f.fecha_solic   <   dateadd(dd,+1,@p_fecha2)

	   if @p_casilla = 0
	   begin
		  select DISTINCT m.id_mensaje , m.sesion , m.secuencia ,
						m.casilla , c.nombre_casilla ,
						m.tipo_msg , t.nombre_tipo ,
						m.prioridad , m.estado_msg ,
						convert(CHAR(10),m.fecha_ingreso,103) AS fecha_ingr,
						convert(CHAR(8),m.fecha_ingreso,108) AS hora_ingr,
						m.cod_banco_em , m.branch_em ,
						m.cod_banco_rec , m.branch_rec ,
						b.nombre_banco , b.ciudad_banco ,
						b.pais_banco , b.oficina_banco ,
						m.cod_moneda , d.nombre_moneda ,
						d.cod_moneda_banco , m.monto ,
						m.referencia , m.beneficiario , m.rut_ingreso ,m.fecha_ingreso FROM 	 dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	(select f.fecha_solic, f.id_mensaje
	   from dbo.sw_msgsend_firma f
	   where f.fecha_solic   >=  dateadd(dd,0,@p_fecha1)
	   and f.fecha_solic   <   dateadd(dd,+1,@p_fecha2)) f WHERE f.id_mensaje    =  m.id_mensaje AND m.estado_msg    in(''SAP'',''AUP'',''DEV'')
		  order by m.fecha_ingreso
	   end
	else
	   begin
		  select DISTINCT m.id_mensaje , m.sesion , m.secuencia ,
						m.casilla , c.nombre_casilla ,
						m.tipo_msg , t.nombre_tipo ,
						m.prioridad , m.estado_msg ,
						convert(CHAR(10),m.fecha_ingreso,103) AS fecha_ingr,
						convert(CHAR(8),m.fecha_ingreso,108) AS hora_ingr,
						m.cod_banco_em , m.branch_em ,
						m.cod_banco_rec , m.branch_rec ,
						b.nombre_banco , b.ciudad_banco ,
						b.pais_banco , b.oficina_banco ,
						m.cod_moneda , d.nombre_moneda ,
						d.cod_moneda_banco , m.monto ,
						m.referencia , m.beneficiario , m.rut_ingreso ,m.fecha_ingreso FROM 	 dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	(select f.fecha_solic, f.id_mensaje
	   from dbo.sw_msgsend_firma f
	   where f.fecha_solic   >=  dateadd(dd,0,@p_fecha1)
	   and f.fecha_solic   <   dateadd(dd,+1,@p_fecha2)) f WHERE f.id_mensaje    =  m.id_mensaje AND m.casilla       =  @p_casilla AND m.estado_msg    in(''SAP'',''AUP'',''DEV'')
		  order by m.fecha_ingreso
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_aut_pend_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_aut_pend_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_aut_pend_MS] 
		@p_casilla INT, 
		@rut 	   INT 
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
		  SELECT   m.id_mensaje,
			m.casilla,
			c.nombre_casilla,
			m.tipo_msg,
			s.nombre_tipo,
			m.estado_msg,
			m.prioridad,
			m.rut_ingreso,
			convert(CHAR(10),m.fecha_ingreso,103) as fec_ing,
			convert(CHAR(8),m.fecha_ingreso,108) as hor_ing,
			m.cod_banco_rec,
			m.branch_rec,
					m.cod_banco_em,
					m.branch_em,
			b.nombre_banco,
			b.ciudad_banco,
			b.pais_banco,
			b.oficina_banco,
			m.cod_moneda,
			n.nombre_moneda,
			n.cod_moneda_banco,
			m.monto,
			m.referencia,
			m.beneficiario,
			m.comentario,
					f.rut_solic,
			m.sesion,
			m.secuencia
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_monedas n ON m.cod_moneda = n.cod_moneda_sw, 
	dbo.sw_tipos_msg s, dbo.sw_bancos b, dbo.sw_casillas c, dbo.sw_msgsend_firma f
		  WHERE (f.id_mensaje = m.id_mensaje		  and
		  f.rut_firma = @rut  			  and
		  f.estado_firma = ''P''			  and
			(m.estado_msg = ''AUP'' or m.estado_msg = ''SAP'') and
		  m.tipo_msg = s.cod_tipo 		  and
		  m.cod_banco_rec = b.cod_banco 		  and
		  m.branch_rec = b.branch  		  and
		  m.casilla = c.cod_casilla)
		  order by fecha_ingreso
	   end
	else
	   begin
		  SELECT   m.id_mensaje,
					m.casilla,
					c.nombre_casilla,
					m.tipo_msg,
					s.nombre_tipo,
					m.estado_msg,
					m.prioridad,
					m.rut_ingreso,
					convert(CHAR(10),m.fecha_ingreso,103) as fec_ing,
					convert(CHAR(8),m.fecha_ingreso,108) as hor_ing,
					m.cod_banco_rec,
					m.branch_rec,
					m.cod_banco_em,
					m.branch_em,
					b.nombre_banco,
					b.ciudad_banco,
					b.pais_banco,
			b.oficina_banco,
					m.cod_moneda,
					n.nombre_moneda,
					n.cod_moneda_banco,
					m.monto,
					m.referencia,
					m.beneficiario,
			m.comentario,
					f.rut_solic,
			m.sesion,
			m.secuencia
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_monedas n ON m.cod_moneda = n.cod_moneda_sw, 
	dbo.sw_tipos_msg s, dbo.sw_bancos b, dbo.sw_casillas c, dbo.sw_msgsend_firma f
		  WHERE (f.id_mensaje = m.id_mensaje               and
		  f.rut_firma = @rut                        and
		  f.estado_firma = ''P''                      and
		  m.casilla = @p_casilla                    and
					(m.estado_msg = ''AUP'' or m.estado_msg = ''SAP'') and
		  m.tipo_msg = s.cod_tipo                   and
		  m.cod_banco_rec = b.cod_banco             and
		  m.branch_rec = b.branch                   and
		  m.casilla = c.cod_casilla)
		  order by fecha_ingreso
	   end
	end
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_aut_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_aut_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_aut_rango_MS] 
		@p_casilla 	INT, 
		@p_fecha1 	datetime, 
		@p_fecha2 	datetime 
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
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.prioridad, m.estado_msg,
			convert(CHAR(10),fecha_ingreso,103) fecha_ingr,
			convert(CHAR(8),fecha_ingreso,108) hora_ingr,
			convert(CHAR(10),fecha_aprobac,103) fecha_apr,
			convert(CHAR(8),fecha_aprobac,108) hora_apr,
			cod_banco_em, branch_em,
			cod_banco_rec, branch_rec,
			nombre_banco, ciudad_banco,
			pais_banco, oficina_banco,
			cod_moneda, nombre_moneda,
			cod_moneda_banco, monto,
			m.referencia, m.beneficiario, m.rut_ingreso
		  FROM 	dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE 	 m.id_mensaje = a.id_mensaje  and
		  datediff(dd,a.fecha_aprobac,@p_fecha1) <= 0  and
		  datediff(dd,a.fecha_aprobac,@p_fecha2) >= 0  and
			(m.estado_msg = ''AUT'' or m.estado_msg = ''AUM'') 
		  order by fecha_aprobac
	   end
	else
	   begin
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.prioridad, m.estado_msg,
			convert(CHAR(10),fecha_ingreso,103) fecha_ingr,
			convert(CHAR(8),fecha_ingreso,108) hora_ingr,
			convert(CHAR(10),fecha_aprobac,103) fecha_apr,
			convert(CHAR(8),fecha_aprobac,108) hora_apr,
			cod_banco_em, branch_em,
			cod_banco_rec, branch_rec,
			nombre_banco, ciudad_banco,
			pais_banco, oficina_banco,
			m.cod_moneda, d.nombre_moneda,
			d.cod_moneda_banco, m.monto,
			m.referencia, m.beneficiario, m.rut_ingreso
		  FROM 	dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE 	 m.id_mensaje = a.id_mensaje  and
		  datediff(dd,a.fecha_aprobac,@p_fecha1) <= 0  and
		  datediff(dd,a.fecha_aprobac,@p_fecha2) >= 0  and
		  m.casilla = @p_casilla  and
			(m.estado_msg = ''AUT'' or m.estado_msg = ''AUM'') 
		  order by fecha_aprobac
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_blo_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_blo_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_blo_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 

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
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.prioridad, m.estado_msg,
			convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
			convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
			convert(CHAR(10),a.fecha_bloqueo,103) as fecha_bloq,
			convert(CHAR(8),a.fecha_bloqueo,108) as hora_bloq,
			m.cod_banco_em, m.branch_em,
			m.cod_banco_rec, m.branch_rec,
			b.nombre_banco, b.ciudad_banco,
			b.pais_banco, b.oficina_banco,
			m.cod_moneda, d.nombre_moneda,
			d.cod_moneda_banco, m.monto,
			m.referencia, m.beneficiario, m.rut_ingreso
		  FROM 	dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_bloqueo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_bloqueo   <  dateadd(dd,+1,@p_fecha2) AND a.id_mensaje      =  m.id_mensaje AND m.estado_msg      =  ''BLO''
		  ORDER BY a.fecha_bloqueo
	   end
	else
	   begin
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.prioridad, m.estado_msg,
			convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
			convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
			convert(CHAR(10),a.fecha_bloqueo,103) as fecha_bloq,
			convert(CHAR(8),a.fecha_bloqueo,108) as hora_bloq,
			m.cod_banco_em, m.branch_em,
			m.cod_banco_rec, m.branch_rec,
			b.nombre_banco, b.ciudad_banco,
			b.pais_banco, b.oficina_banco,
			m.cod_moneda, d.nombre_moneda,
			d.cod_moneda_banco, m.monto,
			m.referencia, m.beneficiario, m.rut_ingreso
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE   a.fecha_bloqueo   >= dateadd(dd,0,@p_fecha1) AND a.fecha_bloqueo   <  dateadd(dd,+1,@p_fecha2) AND a.id_mensaje      =  m.id_mensaje AND m.casilla         =  @p_casilla AND m.estado_msg      =  ''BLO''
		  ORDER BY a.fecha_bloqueo
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_datos_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_datos_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_datos_msg_MS] 
		@p_id_mensaje	INT 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

	   SELECT   m.id_mensaje,
			m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.estado_msg, e.descripcion,
			m.prioridad, m.rut_ingreso,
			m.fecha_ingreso, m.tipo_ingreso,
			a.unidad_rechazo, a.rut_rechazo,
			a.fecha_rechazo, a.texto_rechazo,
			a.veces_rechazo, a.unidad_modifica,
			a.rut_modifica, a.fecha_modifica,
			a.veces_modifica, a.fecha_aprobac,
			a.rut_bloqueo, a.fecha_bloqueo,
			a.veces_bloqueo, a.texto_bloqueo,
			a.unidad_anula, a.rut_anula,
			a.fecha_anula, a.texto_anula,
			d.fecha_envio,
			m.cod_banco_rec, m.branch_rec,
			b.nombre_banco, b.ciudad_banco,
			b.pais_banco, b.oficina_banco,
			m.cod_moneda, m.monto,
			m.referencia, m.beneficiario, m.comentario
	   FROM dbo.sw_msgsend m 
	   LEFT OUTER JOIN dbo.sw_msgsend_add a ON m.id_mensaje = a.id_mensaje 
	   LEFT OUTER JOIN dbo.sw_msgsend_detfile d ON m.id_mensaje = d.id_mensaje 
	   LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	   LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	   LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	   LEFT OUTER JOIN dbo.sw_estados_msg e ON m.estado_msg = e.estado_msg
	   WHERE m.id_mensaje     = @p_id_mensaje
	   ORDER BY m.id_mensaje
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_dev_firma_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_dev_firma_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_dev_firma_MS] 
		@p_casilla INT, 
		@rut 	   INT 
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
		  select   Swt_Alias.id_mensaje,
							 Swt_Alias.sesion,
							 Swt_Alias.secuencia,
							 Swt_Alias.casilla,
							 Swt_Alias.nombre_casilla,
							 Swt_Alias.tipo_msg,
							 Swt_Alias.nombre_tipo,
							 Swt_Alias.estado_msg,Swt_Alias.prioridad,
							 Swt_Alias.rut_ingreso,
							 Swt_Alias.fecha_ingr,
							 Swt_Alias.hora_ingr,
							Swt_Alias.cod_banco_rec,
							Swt_Alias.branch_rec,
							Swt_Alias.cod_banco_em,
							Swt_Alias.branch_em,
							Swt_Alias.nombre_banco,
							Swt_Alias.ciudad_banco,
							Swt_Alias.pais_banco,
							Swt_Alias.oficina_banco,
							Swt_Alias.cod_moneda,
							Swt_Alias.nombre_moneda,
							Swt_Alias.cod_moneda_banco,
							Swt_Alias.monto, Swt_Alias.referencia,
							Swt_Alias.beneficiario,
							Swt_Alias.rut_firma
		  from(select DISTINCT m.id_mensaje,
							 m.sesion,
							 m.secuencia,
							 m.casilla,
							 c.nombre_casilla,
							 m.tipo_msg,
							 s.nombre_tipo,
							 m.estado_msg,m.prioridad,
							 m.rut_ingreso,
							 convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
							 convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
							 m.cod_banco_rec,
							 m.branch_rec,
							 m.cod_banco_em,
							 m.branch_em,
							 b.nombre_banco,
							 b.ciudad_banco,
							 b.pais_banco,
							 b.oficina_banco,
							 m.cod_moneda,
							 n.nombre_moneda,
							 n.cod_moneda_banco,
							 m.monto, m.referencia,
							 m.beneficiario,
							 f.rut_firma,m.fecha_ingreso from   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_monedas n ON m.cod_moneda = n.cod_moneda_sw, 
	dbo.sw_msgsend_firma f, dbo.sw_tipos_msg s, dbo.sw_bancos b, dbo.sw_casillas c where 	 f.rut_solic     = @rut AND f.estado_firma  = ''D'' AND f.id_mensaje    = m.id_mensaje AND m.estado_msg    = ''DEV'' AND m.tipo_msg      = s.cod_tipo AND m.cod_banco_rec = b.cod_banco AND m.branch_rec    = b.branch AND m.casilla       = c.cod_casilla) Swt_Alias
		  order by Swt_Alias.fecha_ingreso
	   end
	else
	   begin
		  select   Swt_Alias.id_mensaje,
							 Swt_Alias.sesion,
							 Swt_Alias.secuencia,
							 Swt_Alias.casilla,
							 Swt_Alias.nombre_casilla,
							 Swt_Alias.tipo_msg,
							 Swt_Alias.nombre_tipo,
							 Swt_Alias.estado_msg, Swt_Alias.prioridad,
							 Swt_Alias.rut_ingreso,
							 Swt_Alias.fecha_ingr,
							 Swt_Alias.hora_ingr,
							 Swt_Alias.cod_banco_rec,
							 Swt_Alias.branch_rec,
							 Swt_Alias.cod_banco_em,
							 Swt_Alias.branch_em,
							 Swt_Alias.nombre_banco,
							 Swt_Alias.ciudad_banco,
							 Swt_Alias.pais_banco,
							 Swt_Alias.oficina_banco,
							 Swt_Alias.cod_moneda,
							 Swt_Alias.nombre_moneda,
							 Swt_Alias.cod_moneda_banco,
							 Swt_Alias.monto,
							 Swt_Alias.referencia,
							 Swt_Alias.beneficiario,
							 Swt_Alias.rut_firma
		  from(select DISTINCT m.id_mensaje,
							 m.sesion,
							 m.secuencia,
							 m.casilla,
							 c.nombre_casilla,
							 m.tipo_msg,
							 s.nombre_tipo,
							 m.estado_msg, m.prioridad,
							 m.rut_ingreso,
							 convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
							 convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
							 m.cod_banco_rec,
							 m.branch_rec,
							 m.cod_banco_em,
							 m.branch_em,
							 b.nombre_banco,
							 b.ciudad_banco,
							 b.pais_banco,
							 b.oficina_banco,
							 m.cod_moneda,
							 n.nombre_moneda,
							 n.cod_moneda_banco,
							 m.monto,
							 m.referencia,
							 m.beneficiario,
							 f.rut_firma,m.fecha_ingreso from   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_monedas n ON m.cod_moneda = n.cod_moneda_sw, 
	dbo.sw_msgsend_firma f, dbo.sw_tipos_msg s, dbo.sw_bancos b, dbo.sw_casillas c where 	f.rut_solic     =  @rut AND f.estado_firma  =  ''D'' AND f.id_mensaje    =  m.id_mensaje AND m.estado_msg    =  ''DEV'' AND m.tipo_msg      =  s.cod_tipo AND m.cod_banco_rec =  b.cod_banco AND m.branch_rec    =  b.branch AND m.casilla       =  c.cod_casilla) Swt_Alias
		  order by Swt_Alias.fecha_ingreso
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'

	CREATE PROCEDURE [dbo].[proc_sw_env_trae_env_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON

		DECLARE @p_fecha1_2 DATETIME = dateadd(dd,0,@p_fecha1)
		DECLARE @p_fecha2_2 DATETIME = dateadd(dd,+1,@p_fecha2)

		declare @min_id_mensaje int
		declare @max_id_mensaje int

		SELECT 
			@min_id_mensaje = MIN(id_mensaje), 
			@max_id_mensaje = MAX(id_mensaje)
		FROM dbo.sw_msgsend_detfile df with (index=sk2_msgsend_detfile) -- Se requiere este hint para evitar que haga un scan utilizando el indice por id_mensaje
		WHERE  df.fecha_envio >= @p_fecha1_2 and df.fecha_envio < @p_fecha2_2
   
		if @p_casilla = 0
		begin
			SELECT   
				m.id_mensaje, 
				m.sesion, m.secuencia,
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
			FROM 	dbo.sw_msgsend m 
			INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje = m.id_mensaje 
			LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
			LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
			LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
			LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
			WHERE  m.estado_msg       = ''ENV''
				AND df.fecha_envio >= @p_fecha1_2 and df.fecha_envio <  @p_fecha2_2
				AND m.id_mensaje >= @min_id_mensaje 
				AND m.id_mensaje <= @max_id_mensaje 
				AND df.id_mensaje >= @min_id_mensaje 
				AND df.id_mensaje <= @max_id_mensaje 
			order by m.id_mensaje,df.fecha_envio

		end
	else
	   begin
			SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
				FROM 	 dbo.sw_msgsend m 
				INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje = m.id_mensaje 
				LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
				LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
				LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
				LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
			WHERE  m.casilla         =  @p_casilla 
				AND m.estado_msg       = ''ENV''
				AND df.fecha_envio >= @p_fecha1_2 and df.fecha_envio <  @p_fecha2_2
				AND m.id_mensaje >= @min_id_mensaje 
				AND m.id_mensaje <= @max_id_mensaje 
				AND df.id_mensaje >= @min_id_mensaje 
				AND df.id_mensaje <= @max_id_mensaje 
			order by m.id_mensaje,df.fecha_envio
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango_paginado]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango_paginado]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_env_rango_paginado]
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime,
		@offset int,
		@fetchRows smallint 
	AS
	BEGIN

		SET NOCOUNT ON

		declare @fechaMin datetime = dateadd(dd,0,@p_fecha1)
		declare @fechaMax datetime = dateadd(dd,+1,@p_fecha2)



		declare @maxRows int
		declare @min_id_mensaje int
		declare @max_id_mensaje int

	   if @p_casilla = 0
	   begin
			select @maxRows=count(1), @min_id_mensaje=min(m.id_mensaje), @max_id_mensaje=max(m.id_mensaje)
			FROM 	dbo.sw_msgsend m 
				INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			WHERE  
					m.estado_msg       = ''ENV''
				and df.fecha_envio >= @fechaMin
				and df.fecha_envio <  @fechaMax  

			SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
					m.referencia, m.beneficiario, m.rut_ingreso,
					@maxRows MaxRows
			FROM 	dbo.sw_msgsend m 
			INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
			LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
			LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
			LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw			  
			WHERE  
					m.estado_msg       = ''ENV''
				AND df.fecha_envio >= @fechaMin
				AND df.fecha_envio <  @fechaMax
				AND m.id_mensaje >= @min_id_mensaje
				AND m.id_mensaje <= @max_id_mensaje
				AND df.id_mensaje >= @min_id_mensaje
				AND df.id_mensaje <= @max_id_mensaje		
			order by m.id_mensaje, df.fecha_envio
			OFFSET @offset ROWS
			FETCH NEXT @fetchRows ROWS ONLY

	   end
	else
	   begin
			select @maxRows=count(1), @min_id_mensaje=min(m.id_mensaje), @max_id_mensaje=max(m.id_mensaje)
			FROM 	dbo.sw_msgsend m 
				INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			WHERE  
					m.estado_msg       = ''ENV''
				and df.fecha_envio >= @fechaMin
				and df.fecha_envio <  @fechaMax
				AND m.casilla =  @p_casilla  

			SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
					 m.referencia, m.beneficiario, m.rut_ingreso,
					@maxRows MaxRows
			FROM 	 dbo.sw_msgsend m 
			INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
			LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
			LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
			LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
			WHERE   
					m.casilla =  @p_casilla 
				AND m.estado_msg      =  ''ENV''
				AND df.fecha_envio >= @fechaMin
				AND df.fecha_envio <  @fechaMax
				AND m.id_mensaje >= @min_id_mensaje
				AND m.id_mensaje <= @max_id_mensaje
				AND df.id_mensaje >= @min_id_mensaje
				AND df.id_mensaje <= @max_id_mensaje		

			order by m.id_mensaje, df.fecha_envio
			OFFSET @offset ROWS
			FETCH NEXT @fetchRows ROWS ONLY
		end
	END


	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango_paginado_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango_paginado_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_env_rango_paginado_MS]
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime,
		@offset int,
		@fetchRows smallint,
		@searchText varchar(max) = null,
		@rutIngreso int = null
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
		  2016-07-04   MBP	 Proyecto Comex.net, se agrega parametros para filtrar los resultados por texto
	*/
		SET NOCOUNT ON

		declare @fechaMin datetime = dateadd(dd,0,@p_fecha1)
		declare @fechaMax datetime = dateadd(dd,+1,@p_fecha2)
	
		declare @maxRows int
		declare @min_id_mensaje int
		declare @max_id_mensaje int

	   if @p_casilla = 0
	   begin
			select @maxRows=count(1), @min_id_mensaje=min(m.id_mensaje), @max_id_mensaje=max(m.id_mensaje)
			FROM 	dbo.sw_msgsend m 
				INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			WHERE  
					m.estado_msg       = ''ENV''
				and m.rut_ingreso = ISNULL(@rutIngreso, m.rut_ingreso)
				and df.fecha_envio >= @fechaMin
				and df.fecha_envio <  @fechaMax 
				AND (@searchText is null OR (m.id_mensaje like ''%'' + @searchText + ''%'' OR m.sesion like ''%'' + @searchText + ''%'' OR m.secuencia like ''%'' + @searchText + ''%'' OR m.casilla LIKE @searchText
				 OR m.tipo_msg like @searchText OR m.referencia LIKE ''%'' + @searchText + ''%'' OR m.beneficiario like ''%'' + @searchText + ''%'' OR m.cod_moneda LIKE @searchText OR
				 m.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(m.cod_banco_em) + RTRIM(m.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(m.cod_banco_rec) + RTRIM(m.branch_rec) like ''%'' + @searchText + ''%'') 
				))

			SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
					m.referencia, m.beneficiario, m.rut_ingreso,
					@maxRows MaxRows
			FROM 	dbo.sw_msgsend m 
			INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
			LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
			LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
			LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw			  
			WHERE  
						m.estado_msg       = ''ENV''
				AND m.rut_ingreso = ISNULL(@rutIngreso, m.rut_ingreso)
				AND df.fecha_envio >= @fechaMin
				AND df.fecha_envio <  @fechaMax
				AND m.id_mensaje >= @min_id_mensaje
				AND m.id_mensaje <= @max_id_mensaje
				AND df.id_mensaje >= @min_id_mensaje
				AND df.id_mensaje <= @max_id_mensaje
				AND (@searchText is null OR (m.id_mensaje like ''%'' + @searchText + ''%'' OR m.sesion like ''%'' + @searchText + ''%'' OR m.secuencia like ''%'' + @searchText + ''%'' OR m.casilla LIKE @searchText
				 OR m.tipo_msg like @searchText OR m.referencia LIKE ''%'' + @searchText + ''%'' OR m.beneficiario like ''%'' + @searchText + ''%'' OR m.cod_moneda LIKE @searchText OR
				 m.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(m.cod_banco_em) + RTRIM(m.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(m.cod_banco_rec) + RTRIM(m.branch_rec) like ''%'' + @searchText + ''%'')  
				))	
			order by m.id_mensaje, df.fecha_envio
			OFFSET @offset ROWS
			FETCH NEXT @fetchRows ROWS ONLY

	   end
	else
	   begin
			select @maxRows=count(1), @min_id_mensaje=min(m.id_mensaje), @max_id_mensaje=max(m.id_mensaje)
			FROM 	dbo.sw_msgsend m 
				INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			WHERE  
				m.estado_msg       = ''ENV''
				and m.rut_ingreso = ISNULL(@rutIngreso, m.rut_ingreso)
				and df.fecha_envio >= @fechaMin
				and df.fecha_envio <  @fechaMax
				AND m.casilla =  @p_casilla
				AND (@searchText is null OR (m.id_mensaje like ''%'' + @searchText + ''%'' OR m.sesion like ''%'' + @searchText + ''%'' OR m.secuencia like ''%'' + @searchText + ''%'' OR m.casilla LIKE @searchText
				 OR m.tipo_msg like @searchText OR m.referencia LIKE ''%'' + @searchText + ''%'' OR m.beneficiario like ''%'' + @searchText + ''%'' OR m.cod_moneda LIKE @searchText OR
				 m.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(m.cod_banco_em) + RTRIM(m.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(m.cod_banco_rec) + RTRIM(m.branch_rec) like ''%'' + @searchText + ''%'') 
				)) 		
		   

			SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
					 m.referencia, m.beneficiario, m.rut_ingreso,
					@maxRows MaxRows
			FROM 	 dbo.sw_msgsend m 
			INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje      = m.id_mensaje 
			LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
			LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
			LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
			LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
			WHERE   
					m.casilla =  @p_casilla 
				AND m.estado_msg      =  ''ENV''
				AND m.rut_ingreso = ISNULL(@rutIngreso, m.rut_ingreso)
				AND df.fecha_envio >= @fechaMin
				AND df.fecha_envio <  @fechaMax
				AND m.id_mensaje >= @min_id_mensaje
				AND m.id_mensaje <= @max_id_mensaje
				AND df.id_mensaje >= @min_id_mensaje
				AND df.id_mensaje <= @max_id_mensaje
				AND (@searchText is null OR (m.id_mensaje like ''%'' + @searchText + ''%'' OR m.sesion like ''%'' + @searchText + ''%'' OR m.secuencia like ''%'' + @searchText + ''%'' OR m.casilla LIKE @searchText
				 OR m.tipo_msg like @searchText OR m.referencia LIKE ''%'' + @searchText + ''%'' OR m.beneficiario like ''%'' + @searchText + ''%'' OR m.cod_moneda LIKE @searchText OR
				 m.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(m.cod_banco_em) + RTRIM(m.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(m.cod_banco_rec) + RTRIM(m.branch_rec) like ''%'' + @searchText + ''%'') 
				))		
			order by m.id_mensaje, df.fecha_envio
			OFFSET @offset ROWS
			FETCH NEXT @fetchRows ROWS ONLY
		end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_files_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_files_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_files_MS] 
		@p_estado CHAR(1),
		@p_fecha1 datetime,
		@p_fecha2 datetime 

	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   if @p_estado = ''P''
		/* Lee Pendientes */
		  SELECT   fd_archivo,
			   nombre_archivo,
			   fecha_creacion,
			   fecha_confirma,
			   estado_archivo,
			   total_mensajes,
			   total_envios,
			   total_rechazos,
			   case  when (total_mensajes = (total_envios + total_rechazos)) then 2 else 3 end campo1
		  FROM     dbo.sw_msgsend_files
		  WHERE   fecha_creacion   >=  dateadd(dd,0,@p_fecha1)
		  and   fecha_creacion   <   dateadd(dd,+1,@p_fecha2)
		  and   estado_ftp       =  ''T''
		  and   estado_archivo   =  ''P''
		  ORDER BY fecha_creacion
	else
		/* Lee Recibidos y Enviados */
	   SELECT   fd_archivo,
			 nombre_archivo,
			 fecha_creacion,
			 fecha_confirma,
			 estado_archivo,
			 total_mensajes,
			 total_envios,
			 total_rechazos,
			 case  when (total_mensajes = (total_envios + total_rechazos)) then 2 else 3 end campo1
	   FROM   dbo.sw_msgsend_files
	   WHERE fecha_confirma  >=   dateadd(dd,0,@p_fecha1)
	   and fecha_confirma  <    dateadd(dd,+1,@p_fecha2)
	   and estado_ftp      =   ''T''
	   and estado_archivo  <>  ''P''
	   ORDER BY fecha_confirma
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_firmas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_firmas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_firmas_MS] 
		@p_casilla INT, 
		@rut 	   INT 
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
		  select   Swt_Alias.id_mensaje,
			Swt_Alias.casilla,
			Swt_Alias.nombre_casilla,
			Swt_Alias.tipo_msg,
			Swt_Alias.nombre_tipo,
			Swt_Alias.estado_msg,
			Swt_Alias.prioridad,
			Swt_Alias.rut_ingreso,
			Swt_Alias.fec_ing,
			Swt_Alias.hor_ing,
			Swt_Alias.cod_banco_rec,
			Swt_Alias.branch_rec,
			Swt_Alias.cod_banco_em,
			Swt_Alias.branch_em,
			Swt_Alias.nombre_banco,
			Swt_Alias.ciudad_banco,
			Swt_Alias.pais_banco,
			Swt_Alias.oficina_banco,
			Swt_Alias.cod_moneda,
			Swt_Alias.nombre_moneda,
			Swt_Alias.cod_moneda_banco,
			Swt_Alias.monto,
			Swt_Alias.referencia,
			Swt_Alias.beneficiario,
			Swt_Alias.comentario,
			Swt_Alias.rut_solic,
			Swt_Alias.sesion,
			Swt_Alias.secuencia
		  from(select DISTINCT m.id_mensaje,
			m.casilla,
			c.nombre_casilla,
			m.tipo_msg,
			s.nombre_tipo,
			m.estado_msg,
			m.prioridad,
			m.rut_ingreso,
			convert(CHAR(10),m.fecha_ingreso,103) as fec_ing,
			convert(CHAR(8),m.fecha_ingreso,108) as hor_ing,
			m.cod_banco_rec,
			m.branch_rec,
			m.cod_banco_em,
			m.branch_em,
			b.nombre_banco,
			b.ciudad_banco,
			b.pais_banco,
			b.oficina_banco,
			m.cod_moneda,
			n.nombre_moneda,
			n.cod_moneda_banco,
			m.monto,
			m.referencia,
			m.beneficiario,
			m.comentario,
			f.rut_solic,
			m.sesion,
			m.secuencia,fecha_ingreso from   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_monedas n ON m.cod_moneda = n.cod_moneda_sw, 
	dbo.sw_msgsend_firma f, dbo.sw_tipos_msg s, dbo.sw_bancos b, dbo.sw_casillas c where f.rut_solic     = @rut AND f.estado_firma  = ''P'' AND f.id_mensaje    = m.id_mensaje AND m.estado_msg   in(''SAP'',''AUP'') AND m.tipo_msg      = s.cod_tipo AND m.cod_banco_rec = b.cod_banco AND m.branch_rec    = b.branch AND m.casilla       = c.cod_casilla) Swt_Alias
		  order by Swt_Alias.fecha_ingreso
	   end
	else
	   begin
		  select    Swt_Alias.id_mensaje,
					Swt_Alias.casilla,
					Swt_Alias.nombre_casilla,
					Swt_Alias.tipo_msg,
					Swt_Alias.nombre_tipo,
					Swt_Alias.estado_msg,
					Swt_Alias.prioridad,
					Swt_Alias.rut_ingreso,
					Swt_Alias.fec_ing,
					Swt_Alias.hor_ing,
					Swt_Alias.cod_banco_rec,
					Swt_Alias.branch_rec,
					Swt_Alias.cod_banco_em,
					Swt_Alias.branch_em,
					Swt_Alias.nombre_banco,
					Swt_Alias.ciudad_banco,
					Swt_Alias.pais_banco,
					Swt_Alias.oficina_banco,
					Swt_Alias.cod_moneda,
					Swt_Alias.nombre_moneda,
					Swt_Alias.cod_moneda_banco,
					Swt_Alias.monto,
					Swt_Alias.referencia,
					Swt_Alias.beneficiario,
					Swt_Alias.comentario,
					Swt_Alias.rut_solic,
					Swt_Alias.sesion,
					Swt_Alias.secuencia
		  from(select DISTINCT m.id_mensaje,
					m.casilla,
					c.nombre_casilla,
					m.tipo_msg,
					s.nombre_tipo,
					m.estado_msg,
					m.prioridad,
					m.rut_ingreso,
					convert(CHAR(10),m.fecha_ingreso,103) as fec_ing,
					convert(CHAR(8),m.fecha_ingreso,108) as hor_ing,
					m.cod_banco_rec,
					m.branch_rec,
					m.cod_banco_em,
					m.branch_em,
					b.nombre_banco,
					b.ciudad_banco,
					b.pais_banco,
					b.oficina_banco,
					m.cod_moneda,
					n.nombre_moneda,
					n.cod_moneda_banco,
					m.monto,
					m.referencia,
					m.beneficiario,
					m.comentario,
					f.rut_solic,
					m.sesion,
					m.secuencia,fecha_ingreso from   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_monedas n ON m.cod_moneda = n.cod_moneda_sw, 
	dbo.sw_msgsend_firma f, dbo.sw_tipos_msg s, dbo.sw_bancos b, dbo.sw_casillas c where  f.rut_solic     = @rut AND f.estado_firma  = ''P'' AND f.id_mensaje    = m.id_mensaje AND m.casilla       = @p_casilla AND m.estado_msg   in(''SAP'',''AUP'') AND m.tipo_msg      = s.cod_tipo AND m.cod_banco_rec = b.cod_banco AND m.branch_rec    = b.branch AND m.casilla       = c.cod_casilla) Swt_Alias
		  order by Swt_Alias.fecha_ingreso
	   end
	end
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_ing_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_ing_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_ing_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	-- This procedure was converted on Thu Apr 17 16:47:47 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   --CREATE table #ing_rango
	   --(
	   --   id_mensaje        INT           null,
	   --   sesion            INT           null,
	   --   secuencia         INT           null,
	   --   casilla           INT           null,
	   --   nombre_casilla    VARCHAR(30)   null,
	   --   tipo_msg          CHAR(7)       null,
	   --   nombre_tipo       VARCHAR(40)   null,
	   --   prioridad         CHAR(1)       null,
	   --   estado_msg        CHAR(3)       null,
	   --   fecha_ingr        CHAR(10)      null,
	   --   hora_ingr         CHAR(8)       null,
	   --   cod_banco_em      CHAR(9)       null,
	   --   branch_em         CHAR(4)       null,
	   --   cod_banco_rec     CHAR(9)       null,
	   --   branch_rec        CHAR(4)       null,
	   --   nombre_banco      VARCHAR(70)   null,
	   --   ciudad_banco      VARCHAR(35)   null,
	   --   pais_banco        VARCHAR(35)   null,
	   --   oficina_banco     VARCHAR(50)   null,
	   --   cod_moneda        CHAR(3)       null,
	   --   nombre_moneda     VARCHAR(50)   null,
	   --   cod_moneda_banco  CHAR(3)       null,
	   --   monto             FLOAT         null,
	   --   referencia        VARCHAR(17)   null,
	   --   beneficiario      VARCHAR(37)   null,
	   --   rut_ingreso       INT           null,
	   --   fecha_de_orden    datetime      null
	   --)

	   --CREATE clustered index idx_ing_rango on #ing_rango(fecha_de_orden)

	   if @p_casilla = 0
	   begin
		  --insert  #ing_rango
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
				m.referencia, m.beneficiario, m.rut_ingreso,m.fecha_ingreso
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
		  WHERE  m.fecha_ingreso   >=  dateadd(dd,0,@p_fecha1) AND m.fecha_ingreso   <   dateadd(dd,+1,@p_fecha2) AND m.estado_msg      =  ''DIG''
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
				m.referencia, m.beneficiario, m.rut_ingreso,m.fecha_ingreso
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
		  WHERE  m.fecha_ingreso   >=  dateadd(dd,0,@p_fecha1) AND m.fecha_ingreso   <  dateadd(dd,+1,@p_fecha2) AND m.estado_msg      =  ''INY''
	   end
	else
	   begin
		  --insert  #ing_rango
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
			  m.referencia, m.beneficiario, m.rut_ingreso,m.fecha_ingreso
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
		  WHERE  m.fecha_ingreso  >= dateadd(dd,0,@p_fecha1) AND m.fecha_ingreso  < dateadd(dd,+1,@p_fecha2) AND m.estado_msg     = ''DIG'' AND m.casilla        = @p_casilla
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
			  m.referencia, m.beneficiario, m.rut_ingreso,m.fecha_ingreso
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
		  WHERE  m.fecha_ingreso  >=  dateadd(dd,0,@p_fecha1) AND m.fecha_ingreso  <   dateadd(dd,+1,@p_fecha2) AND m.estado_msg     =  ''INY'' AND m.casilla        =  @p_casilla
	   end

	   --select   id_mensaje,
			 --     sesion,
			 --     secuencia,
	   --           casilla,
	   --           nombre_casilla,
	   --           tipo_msg,
			 --     nombre_tipo,
	   --           prioridad,
			 --     estado_msg,
	   --           fecha_ingr,
	   --           hora_ingr,
	   --           cod_banco_em,
			 --     branch_em ,
	   --           cod_banco_rec,
			 --     branch_rec,
	   --           nombre_banco,
			 --     ciudad_banco,
	   --           pais_banco,
			 --     oficina_banco,
	   --           cod_moneda,
			 --     nombre_moneda,
	   --           cod_moneda_banco,
			 --     monto,
	   --           referencia,
			 --     beneficiario,
		  --        rut_ingreso
	   --from #ing_rango
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_mod_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_mod_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_mod_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
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
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
			   m.casilla, c.nombre_casilla,
			   m.tipo_msg, t.nombre_tipo,
			   m.prioridad, m.estado_msg,
			   convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
			   convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
			   convert(CHAR(10),a.fecha_modifica,103) as fecha_mod,
			   convert(CHAR(8),a.fecha_modifica,108) as hora_mod,
			   m.cod_banco_em, m.branch_em,
			   m.cod_banco_rec, m.branch_rec,
			   b.nombre_banco, b.ciudad_banco,
			   b.pais_banco, b.oficina_banco,
			   m.cod_moneda, d.nombre_moneda,
			   d.cod_moneda_banco, m.monto,
			   m.referencia, m.beneficiario,
			   m.rut_ingreso
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_modifica >=  dateadd(dd,0,@p_fecha1) AND a.fecha_modifica <  dateadd(dd,+1,@p_fecha2) AND a.id_mensaje     =  m.id_mensaje AND m.estado_msg     =  ''MOD''
		  ORDER BY a.fecha_modifica
	   end
	else
	   begin
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
			   m.casilla, c.nombre_casilla,
			   m.tipo_msg, t.nombre_tipo,
			   m.prioridad, m.estado_msg,
			   convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
			   convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
			   convert(CHAR(10),a.fecha_modifica,103) as fecha_mod,
			   convert(CHAR(8),a.fecha_modifica,108) as hora_mod,
			   m.cod_banco_em, m.branch_em,
			   m.cod_banco_rec, m.branch_rec,
			   b.nombre_banco, b.ciudad_banco,
			   b.pais_banco, b.oficina_banco,
			   m.cod_moneda, d.nombre_moneda,
			   d.cod_moneda_banco, m.monto,
			   m.referencia, m.beneficiario, m.rut_ingreso
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_modifica  >=  dateadd(dd,0,@p_fecha1) AND a.fecha_modifica  <  dateadd(dd,+1,@p_fecha2) AND a.id_mensaje      =  m.id_mensaje AND m.estado_msg      =  ''MOD'' AND m.casilla         =  @p_casilla
		  ORDER BY a.fecha_modifica
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_nul_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_nul_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_nul_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
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
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
				 m.casilla, c.nombre_casilla,
				 m.tipo_msg, t.nombre_tipo,
				 m.prioridad, m.estado_msg,
				 convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
				 convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
				 convert(CHAR(10),a.fecha_anula,103) as fecha_anula,
				 convert(CHAR(8),a.fecha_anula,108) as hora_anula,
				 m.cod_banco_em, m.branch_em,
				 m.cod_banco_rec, m.branch_rec,
				 b.nombre_banco, b.ciudad_banco,
				 b.pais_banco, b.oficina_banco,
				 m.cod_moneda, d.nombre_moneda,
				 d.cod_moneda_banco, m.monto,
				 m.referencia, m.beneficiario, m.rut_ingreso
		  FROM dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_anula    >=  dateadd(dd,0,@p_fecha1) AND a.fecha_anula    <   dateadd(dd,+1,@p_fecha2) AND a.id_mensaje     =  m.id_mensaje AND m.estado_msg     =  ''NUL''
		  ORDER BY a.fecha_anula
	   end
	else
	   begin
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
				 m.casilla, c.nombre_casilla,
				 m.tipo_msg, t.nombre_tipo,
				 m.prioridad, m.estado_msg,
				 convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
				 convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
				 convert(CHAR(10),a.fecha_anula,103) as fecha_anula,
				 convert(CHAR(8),a.fecha_anula,108) as hora_anula,
				 m.cod_banco_em, m.branch_em,
				 m.cod_banco_rec, m.branch_rec,
				 b.nombre_banco, b.ciudad_banco,
				 b.pais_banco, b.oficina_banco,
				 m.cod_moneda, d.nombre_moneda,
				 d.cod_moneda_banco, m.monto,
				 m.referencia, m.beneficiario, m.rut_ingreso
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_anula   >=   dateadd(dd,0,@p_fecha1) AND a.fecha_anula   <    dateadd(dd,+1,@p_fecha2) AND a.id_mensaje    =   m.id_mensaje AND m.estado_msg    =   ''NUL'' AND m.casilla       =   @p_casilla
		  ORDER BY a.fecha_anula
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_pro_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_pro_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_pro_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
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
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
				 m.casilla, c.nombre_casilla,
				 m.tipo_msg, t.nombre_tipo,
				 m.prioridad, m.estado_msg,
				 convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
				 convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
				 convert(CHAR(10),f.fecha_creacion,103) as fecha_pro,
				 convert(CHAR(8),f.fecha_creacion,108) as hora_pro,
				 m.cod_banco_em, m.branch_em,
				 m.cod_banco_rec, m.branch_rec,
				 b.nombre_banco, b.ciudad_banco,
				 b.pais_banco, b.oficina_banco,
				 m.cod_moneda, d.nombre_moneda,
				 d.cod_moneda_banco, m.monto,
				 m.referencia, m.beneficiario, m.rut_ingreso
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_files f, dbo.sw_msgsend_detfile df
		  WHERE  f.fecha_creacion >=  dateadd(dd,0,@p_fecha1) AND f.fecha_creacion <   dateadd(dd,+1,@p_fecha2) AND f.fd_archivo     =  df.fd_archivo AND df.id_mensaje    =  m.id_mensaje AND m.estado_msg     =  ''PRO''
		  ORDER BY f.fecha_creacion
	   end
	else
	   begin
		  SELECT   m.id_mensaje, m.sesion, m.secuencia,
				  m.casilla, c.nombre_casilla,
				  m.tipo_msg, t.nombre_tipo,
				  m.prioridad, m.estado_msg,
				  convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
				  convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
				  convert(CHAR(10),f.fecha_creacion,103) as fecha_pro,
				  convert(CHAR(8),f.fecha_creacion,108) as hora_pro,
				  m.cod_banco_em, m.branch_em,
				  m.cod_banco_rec, m.branch_rec,
				  b.nombre_banco, b.ciudad_banco,
				  b.pais_banco, b.oficina_banco,
				  m.cod_moneda, d.nombre_moneda,
				  d.cod_moneda_banco, m.monto,
				  m.referencia, m.beneficiario, m.rut_ingreso
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_files f, dbo.sw_msgsend_detfile df
		  WHERE    f.fecha_creacion >=  dateadd(dd,0,@p_fecha1) AND f.fecha_creacion <  dateadd(dd,+1,@p_fecha2) AND f.fd_archivo     =  df.fd_archivo AND df.id_mensaje     = m.id_mensaje AND m.estado_msg     =  ''PRO'' AND m.casilla        =  @p_casilla
		  ORDER BY f.fecha_creacion
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_rech_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_rech_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_rech_rango_MS] 
		@p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 


	   --CREATE table #ing_rech_rango
	   --(
	   --   id_mensaje        INT           null,
	   --   sesion            INT           null,
	   --   secuencia         INT           null,
	   --   casilla           INT           null,
	   --   nombre_casilla    VARCHAR(30)   null,
	   --   tipo_msg          CHAR(7)       null,
	   --   nombre_tipo       VARCHAR(40)   null,
	   --   prioridad         CHAR(1)       null,
	   --   estado_msg        CHAR(3)       null,
	   --   fecha_ingr        CHAR(10)      null,
	   --   hora_ingr         CHAR(8)       null,
	   --   fecha_rechazo     CHAR(10)      null,
	   --   hora_rechazo      CHAR(8)       null,
	   --   cod_banco_em      CHAR(9)       null,
	   --   branch_em         CHAR(4)       null,
	   --   cod_banco_rec     CHAR(9)       null,
	   --   branch_rec        CHAR(4)       null,
	   --   nombre_banco      VARCHAR(70)   null,
	   --   ciudad_banco      VARCHAR(35)   null,
	   --   pais_banco        VARCHAR(35)   null,
	   --   oficina_banco     VARCHAR(50)   null,
	   --   cod_moneda        CHAR(3)       null,
	   --   nombre_moneda     VARCHAR(50)   null,
	   --   cod_moneda_banco  CHAR(3)       null,
	   --   monto             FLOAT         null,
	   --   referencia        VARCHAR(17)   null,
	   --   beneficiario      VARCHAR(37)   null,
	   --   rut_ingreso       INT           null,
	   --   fecha_de_orden    datetime      null
	   --)

	   --CREATE clustered index idx_ing_rech_rango on #ing_rech_rango(fecha_de_orden)

	   if @p_casilla = 0
	   begin
		  --insert  #ing_rech_rango
		  SELECT  m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario, m.rut_ingreso,
					a.fecha_rechazo
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE   a.fecha_rechazo  >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo  <   dateadd(dd,+1,@p_fecha2) AND a.id_mensaje     =  m.id_mensaje AND m.estado_msg     =  ''REM''
		  UNION
		  SELECT  m.id_mensaje, m.sesion, m.secuencia,
				 m.casilla, c.nombre_casilla,
				 m.tipo_msg, t.nombre_tipo,
				 m.prioridad, m.estado_msg,
				 convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
				 convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
				 convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
				 convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
				 m.cod_banco_em, m.branch_em,
				 m.cod_banco_rec, m.branch_rec,
				 b.nombre_banco, b.ciudad_banco,
				 b.pais_banco, b.oficina_banco,
				 m.cod_moneda, d.nombre_moneda,
				 d.cod_moneda_banco, m.monto,
				 m.referencia, m.beneficiario, m.rut_ingreso,
				 a.fecha_rechazo
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE   a.fecha_rechazo  >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo  <   dateadd(dd,+1,@p_fecha2) AND a.id_mensaje     =  m.id_mensaje AND m.estado_msg     =  ''RES''
	   end
	else
	   begin
		  --insert  #ing_rech_rango
		  SELECT 	m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario, m.rut_ingreso,
					a.fecha_rechazo
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_rechazo  >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo  <   dateadd(dd,+1,@p_fecha2) AND a.id_mensaje     =  m.id_mensaje AND m.estado_msg     =  ''REM'' AND m.casilla        =  @p_casilla
		  UNION
		  SELECT 	m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario, m.rut_ingreso,
					a.fecha_rechazo
		  FROM   dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_rechazo  >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo  <   dateadd(dd,+1,@p_fecha2) AND a.id_mensaje     =  m.id_mensaje AND m.estado_msg     =  ''RES'' AND m.casilla        =  @p_casilla
	   end
	   --select   id_mensaje,
			 --     sesion,
			 --     secuencia,
	   --           casilla,
	   --           nombre_casilla,
	   --           tipo_msg,
			 --     nombre_tipo,
	   --           prioridad,
			 --     estado_msg,
	   --           fecha_ingr,
	   --           hora_ingr,
				--  fecha_rechazo,
				--  hora_rechazo,
	   --           cod_banco_em,
			 --     branch_em ,
	   --           cod_banco_rec,
			 --     branch_rec,
	   --           nombre_banco,
			 --     ciudad_banco,
	   --           pais_banco,
			 --     oficina_banco,
	   --           cod_moneda,
			 --     nombre_moneda,
	   --           cod_moneda_banco,
			 --     monto,
	   --           referencia,
			 --     beneficiario,
				--  rut_ingreso
	   --from #ing_rech_rango
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_graba_bancos_MS] 
	@codigo nvarchar(50),
	@Branch nvarchar(50),
	@Nombre nvarchar(50),
	@direccion nvarchar(50),
	@Ciudad nvarchar(50),
	@Pais nvarchar(50),
	@Oficina nvarchar(50),
	@Clave nvarchar(50),
	@Localidad nvarchar(50),
	@Pob nvarchar(50)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_bancos
		VALUES(@codigo,
			   @Branch,
			   @Nombre,
			   @direccion,
			   @Ciudad,
			   @Pais,
			   @Oficina,
			   @Clave,
			   @Localidad,
			   @Pob)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_CampoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[proc_sw_graba_CampoMensajes_MS] 
	@Codigo nvarchar(4),
	@Linea INT,
	@Nombre nvarchar(50),
	@Largo INT,
	@Formato nvarchar(40)

	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_campos_msg
		VALUES(@Codigo ,@Linea,@Nombre,@Largo,@Formato)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_graba_CaracterInvalido_MS] 
	@Codigo int,
	@Caracter nvarchar(2),
	@Descripcion nvarchar(30)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_caracter_error
		VALUES(@Codigo,
			   @Caracter,
			   @Descripcion)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_casillas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_casillas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		Hernan Palominos
	-- Description:	Inserta una nueva casilla SWIMAN.
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_graba_casillas_MS] 
	@codigo INT,
	@nombre nvarchar(50),
	@origen nvarchar(2)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_casillas
		VALUES(@codigo,
			   @nombre,
			   @origen)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_graba_FormatoMensajes_MS] 
	@Codigo nvarchar(20),
	@Orden INT,
	@Tag nvarchar(20),
	@Secuencia nvarchar(15),
	@Repeticion INT,
	@Status nvarchar(2)

	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_formatos
		VALUES(@Codigo ,@Orden,@Secuencia,@Repeticion,@Tag,@Status)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_GlosaCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_graba_GlosaCampos_MS] 
	@Codigo nvarchar(30),
	@Tag nvarchar(20),
	@Nombre nvarchar(50)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_tipos_campos
		VALUES(@Codigo,
			   @Tag,
			   @Nombre)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_monedas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	Registra moneda SWI_MAN 
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_graba_monedas_MS] 
	@CodigoSw nvarchar(50),
	@CodigoBc INT=null,
	@Nombre nvarchar(50),
	@Decimales INT,
	@Uso nvarchar(2)

	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_monedas
		VALUES(@CodigoSw ,@Decimales,@Nombre,@CodigoBc,@Uso)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_TiposMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_graba_TiposMensajes_MS] 
	@Codigo nvarchar(20),
	@Nombre nvarchar(50)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_tipos_msg
		VALUES(@Codigo,
		@Nombre)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_usuarios_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_usuarios_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	Registra usuario SWI_MAN 
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_graba_usuarios_MS] 
	@p_rut INT,
	@p_dvRut INT,
	@p_nombre nvarchar(50),
	@p_tipo nvarchar(50)
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_users_swift
		VALUES(@p_rut ,
		@p_dvRut,
			@p_nombre,
			@p_tipo)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_graba_ValoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_graba_ValoresCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CReate PROCEDURE [dbo].[proc_sw_graba_ValoresCampos_MS] 
	@Codigo nvarchar(30),
	@Tag nvarchar(20),
	@Linea int,
	@Condicion nvarchar(15),
	@Campos nvarchar(100),
	@Total int
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

		  INSERT INTO dbo.sw_valor_campos
		VALUES(@Codigo,
			   @Tag,
			   @Linea,
			   @Condicion,
			   @Campos,
			   @Total)
			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_aut_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_aut_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_log_trae_aut_rango_MS] 
		@p_casilla 	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   CREATE table #tmprango
	   (
		  sesion        INT            NULL,
		  secuencia     INT            NULL,
		  tipo_msg      CHAR(7)       NULL,
		  prioridad     CHAR(1)        NULL,
		  estado_msg    CHAR(3)        NULL,
		  fecha_send    CHAR(10)       NULL,
		  hora_send     CHAR(8)       NULL,
		  fecha_ack     CHAR(10)       NULL,
		  hora_ack      CHAR(8)       NULL,
		  cod_banco_rec CHAR(9)       NULL,
		  branch_rec    CHAR(4)       NULL,
		  cod_banco_em  CHAR(9)       NULL,
		  branch_em     CHAR(4)       NULL,
		  cod_moneda    CHAR(3)       NULL,
		  monto         FLOAT          NULL,
		  referencia    VARCHAR(17)    NULL,
		  beneficiario  VARCHAR(37)    NULL,
		  total_imp     INT            NULL,
		  casilla       INT            NULL,
		  fecha_log     datetime
	   )

	   CREATE clustered index idx_rengo on #tmprango(fecha_log,sesion,secuencia)


	   if @p_casilla = 0
	   begin
		  insert #tmprango
		  SELECT 	m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),m.fecha_ack,103),
			convert(CHAR(8),m.fecha_ack,108),
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, m.casilla,l.fecha_log
		  FROM 	dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack   >=  dateadd(dd,0,@p_fecha1)
		  and	m.fecha_ack   <  dateadd(dd,+1,@p_fecha2)
		  and	m.send_recv   =  ''R''
		  and	m.estado_msg  =  ''ENC''
		  and   m.sesion      =  l.sesion_log
		  and	m.secuencia   =  l.secuencia_log
		  and   l.opcion_log  =  ''A''
		  UNION
		  SELECT 	m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),m.fecha_ack,103),
			convert(CHAR(8),m.fecha_ack,108),
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, m.casilla,l.fecha_log
		  FROM 	dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack   >=  dateadd(dd,0,@p_fecha1)
		  and	m.fecha_ack   <  dateadd(dd,+1,@p_fecha2)
		  and	m.send_recv   =  ''R''
		  and	m.estado_msg  =  ''IMP''
		  and   m.sesion      =  l.sesion_log
		  and	m.secuencia   =  l.secuencia_log
		  and   l.opcion_log  =  ''A''
		  UNION
		  SELECT 	m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),m.fecha_ack,103),
			convert(CHAR(8),m.fecha_ack,108),
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, m.casilla,l.fecha_log
		  FROM 	dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack   >=  dateadd(dd,0,@p_fecha1)
		  and	m.fecha_ack   <  dateadd(dd,+1,@p_fecha2)
		  and	m.send_recv   =  ''R''
		  and	m.estado_msg  =  ''CNF''
		  and   m.sesion      =  l.sesion_log
		  and	m.secuencia   =  l.secuencia_log
		  and   l.opcion_log  =  ''A''
	   end
	else
	   begin
		  insert  #tmprango
		  SELECT 	m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),m.fecha_ack,103),
			convert(CHAR(8),m.fecha_ack,108),
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, m.casilla,l.fecha_log
		  FROM 	dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack       >=  dateadd(dd,0,@p_fecha1)
		  and	m.fecha_ack       <  dateadd(dd,+1,@p_fecha2)
		  and	m.send_recv       =  ''R''
		  and	m.estado_msg      =  ''ENC''
		  and   m.sesion          =  l.sesion_log
		  and	m.secuencia       =  l.secuencia_log
		  and	l.opcion_log      =  ''A''
		  and	l.casilla_destino =  @p_casilla
		  UNION
		  SELECT 	m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),m.fecha_ack,103),
			convert(CHAR(8),m.fecha_ack,108),
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, m.casilla,l.fecha_log
		  FROM 	dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack       >=  dateadd(dd,0,@p_fecha1)
		  and	m.fecha_ack       <  dateadd(dd,+1,@p_fecha2)
		  and	m.send_recv       =  ''R''
		  and	m.estado_msg      =  ''IMP''
		  and   m.sesion          =  l.sesion_log
		  and	m.secuencia       =  l.secuencia_log
		  and	l.opcion_log      =  ''A''
		  and	l.casilla_destino =  @p_casilla
		  UNION
		  SELECT 	m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),m.fecha_ack,103),
			convert(CHAR(8),m.fecha_ack,108),
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, m.casilla,l.fecha_log
		  FROM 	dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack       >=  dateadd(dd,0,@p_fecha1)
		  and	m.fecha_ack       <  dateadd(dd,+1,@p_fecha2)
		  and	m.send_recv       =  ''R''
		  and	m.estado_msg      =  ''CNF''
		  and   m.sesion          =  l.sesion_log
		  and	m.secuencia       =  l.secuencia_log
		  and	l.opcion_log      =  ''A''
		  and	l.casilla_destino =  @p_casilla
	   end
	   select   sesion,
				secuencia,
				tipo_msg,
				prioridad,
				estado_msg,
				fecha_send,
				hora_send,
				fecha_ack,
				hora_ack,
				cod_banco_rec,
				branch_rec,
				cod_banco_em,
				branch_em,
				cod_moneda,
				monto,
				referencia,
				beneficiario,
				total_imp,
				casilla
	   from #tmprango

	   drop table #tmprango
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_enc_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_enc_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_log_trae_enc_rango_MS] 
		@p_casilla 	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime 
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
		  FROM 	dbo.sw_mensajes_add a,  dbo.sw_mensajes m, dbo.sw_log_msg l
		  WHERE   a.fecha_reenvio is not null
		  and   a.fecha_reenvio >=  dateadd(dd,0,@p_fecha1)
		  and   a.fecha_reenvio <   dateadd(dd,+1,@p_fecha2)
		  and   a.sesion        =  m.sesion
		  and	a.secuencia     =  m.secuencia
		  and	a.send_recv     =  m.send_recv
		  and   m.send_recv     =  ''R''
		  and	m.estado_msg    in(''ENC'',''IMP'',''CNF'',''REE'')
		  and   m.sesion        =  l.sesion_log
		  and	m.secuencia     =  l.secuencia_log
		  and	opcion_log      in(''sengrenc'',''sengrree'')
		  UNION ALL
		  select DISTINCT m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			m.estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) fecha_pro,
			convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) hora_pro,
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,
			isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora
			FROM 	dbo.sw_mensajes m, dbo.sw_mensajes_add a, dbo.sw_log_msg l WHERE  m.fecha_ack     >= dateadd(dd,0,@p_fecha1)
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
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) fecha_pro,
			convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) hora_pro,
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,
			isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora
		  FROM 	dbo.sw_mensajes_add a, dbo.sw_mensajes m,  dbo.sw_log_msg l
		  WHERE 	a.fecha_reenvio   is not null
		  and   a.fecha_reenvio  >=  dateadd(dd,0,@p_fecha1)
		  and	a.fecha_reenvio  <  dateadd(dd,+1,@p_fecha2)
		  and   a.sesion         =  m.sesion
		  and	a.secuencia      =  m.secuencia
		  and	a.send_recv      =  m.send_recv
		  and   m.send_recv      =  ''R''
		  and   m.estado_msg     in(''ENC'',''IMP'',''CNF'',''REE'')
		  and   m.casilla        =  @p_casilla
		  and	m.secuencia      =  l.secuencia_log
		  and   m.sesion         =  l.sesion_log
		  and   l.opcion_log     in(''sengrenc'',''sengrree'')
		  UNION ALL
		  SELECT   m.sesion, m.secuencia,
			m.tipo_msg, m.prioridad,
			m.estado_msg,
			convert(CHAR(10),m.fecha_send,103),
			convert(CHAR(8),m.fecha_send,108),
			convert(CHAR(10),isnull(a.fecha_reenvio,m.fecha_ack),103) fecha_pro,
			convert(CHAR(8),isnull(a.fecha_reenvio,m.fecha_ack),108) hora_pro,
			isnull(m.cod_banco_rec,'''') as cod_banco_rec, isnull(m.branch_rec,'''') as branch_rec,
			isnull(m.cod_banco_em,'''') as cod_banco_em, isnull(m.branch_em,'''') as branch_em,
			isnull(m.cod_moneda,'''') as cod_moneda, isnull(m.monto,0) as monto,
			isnull(m.referencia,'''') as referencia, isnull(m.beneficiario,'''') as beneficiario,
			isnull(m.total_imp,0) as total_imp, isnull(m.casilla,0) as casilla,
			isnull(a.fecha_reenvio,m.fecha_ack) as fecha_hora
		  FROM 	dbo.sw_mensajes m, dbo.sw_mensajes_add a, dbo.sw_log_msg l
		  WHERE 	m.fecha_ack      >= dateadd(dd,0,@p_fecha1)
		  and   m.fecha_ack      <  dateadd(dd,+1,@p_fecha2)
		  and   m.send_recv      =  ''R''
		  and   m.estado_msg     in(''ENC'',''IMP'',''CNF'',''REE'')
		  and   m.casilla        =  @p_casilla
		  and   m.sesion         =  a.sesion
		  and	m.secuencia      =  a.secuencia
		  and	m.send_recv      =  a.send_recv
		  and   a.fecha_reenvio  is null
		  and	m.secuencia      =  l.secuencia_log
		  and   m.sesion         =  l.sesion_log
		  and   l.opcion_log     in(''sengrenc'',''sengrree'')
		  order by fecha_pro,hora_pro
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_log_trae_msg_MS] 
		@p_sesion 	INT, 
		@p_secuencia 	INT, 
		@p_send_recv 	CHAR(1) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

	   SELECT   fecha_log,
			rutais_log,
			casilla_origen,
			estado_origen,
			casilla_destino,
			estado_destino,
			nombre_casilla,
			comentario_log
	   FROM 	dbo.sw_log_msg 
	   LEFT OUTER JOIN dbo.sw_casillas ON sw_log_msg.unidad_log = sw_casillas.cod_casilla
	   WHERE 	
		   sw_log_msg.sesion_log = @p_sesion 
	   AND sw_log_msg.secuencia_log = @p_secuencia 
	   AND sw_log_msg.send_recv_log = @p_send_recv
	   ORDER BY fecha_log
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_log_trae_msgsend_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_log_trae_msgsend_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_log_trae_msgsend_MS] 
		@p_id_mensaje 	INT 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

	   SELECT   fecha_log,
			rutais_log,
			casilla_origen,
			estado_origen,
			casilla_destino,
			estado_destino,
			nombre_casilla,
			comentario_log
	   FROM 	dbo.sw_msgsend_log 
	   LEFT OUTER JOIN dbo.sw_casillas ON sw_msgsend_log.unidad_log = sw_casillas.cod_casilla
	   WHERE 	sw_msgsend_log.id_mensaje = @p_id_mensaje
	   ORDER BY fecha_log
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_anula_enc_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_anula_enc_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_anula_enc_MS] @p_casilla   INT, 
		@p_sesion    INT, 
		@p_secuencia INT, 
		@p_rut_log   INT 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual CHAR(3)
	begin tran

	   SELECT   @estado_actual = estado_msg,
		@casilla_actual = casilla
	   FROM 	dbo.sw_mensajes
	   WHERE 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'') 

	   update dbo.sw_mensajes set casilla = 748,unidad = 748,estado_msg = ''SEN'',rut_entry = @p_rut_log,fecha_ack = GetDate(),
	   comentario = ''Deshace Encasillamiento''  WHERE 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'')
	   if (@@error <> 0) and (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -1
	   end
	  

	   if exists(SELECT TOP 1 1 from dbo.sw_mensajes_add where sesion = @p_sesion and
	   secuencia = @p_secuencia and send_recv = ''R'')
	   begin
		  update dbo.sw_mensajes_add set casilla = 748,observ_encas = ''''  where  	(sesion = @p_sesion) and
			(secuencia = @p_secuencia) and
			(send_recv = ''R'')
	   end
	   if (@@error <> 0) and (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -2
	   end

	   INSERT INTO dbo.sw_log_msg
	VALUES(@p_sesion, @p_secuencia, ''R'', GetDate(),
			@p_rut_log, NULL, ''GS24'', ''encgrsen'',
			@casilla_actual, @estado_actual,
			748, ''SEN'', @p_casilla, NULL, ''A'',
			''Deshace Encasillamiento'')

	   if (@@error <> 0) and (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -3
	   end

	   COMMIT tran
	   return 0
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_control_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_control_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_control_MS] 
		@p_casilla	INT,
		@p_fecha1 	datetime 
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
		  SELECT   sesion, secuencia,
				casilla, estado_msg,
				convert(CHAR(10),fecha_send,103),
				convert(CHAR(8),fecha_send,108),
				cod_banco_rec, branch_rec
		  FROM 	dbo.sw_mensajes
		  WHERE 	fecha_send  >= dateadd(dd,0,@p_fecha1)
		  and   fecha_send  <  dateadd(dd,+1,@p_fecha1)
		  and	send_recv   = ''R''
		  order by sesion asc, secuencia asc
		  --order by cod_banco_rec desc, 4 asc, sesion asc, secuencia asc
	   end
	else
	   begin
		  SELECT   sesion, secuencia,
				casilla, estado_msg,
				convert(CHAR(10),fecha_send,103),
				convert(CHAR(8),fecha_send,108),
				cod_banco_rec, branch_rec
		  FROM 	dbo.sw_mensajes
		  WHERE 	fecha_send  >= dateadd(dd,0,@p_fecha1)
		  and   fecha_send  < dateadd(dd,+1,@p_fecha1)
		  and	send_recv   =  ''R''
		  and   casilla     =  @p_casilla
		  order by sesion asc, secuencia asc
		  --order by cod_banco_rec desc, 4 asc, sesion asc, secuencia asc
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_estadist_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_estadist_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_estadist_msg_MS]
		@p_casilla 	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

	 DECLARE @p_fecha1_2 DATETIME = dateadd(dd,0,@p_fecha1)  
	 DECLARE @p_fecha2_2 DATETIME = dateadd(dd,+1,@p_fecha2)  

	   if @p_casilla = 0
	   begin
		  SELECT   m.casilla,
			c.nombre_casilla,
			m.tipo_msg,
			isnull(convert(VARCHAR,t.nombre_tipo),'''') as nombre_tipo,
			m.cod_banco_rec,
			b.nombre_banco,
			count(*) as cantidad
		  FROM 	dbo.sw_mensajes m 
		  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo, 
				dbo.sw_casillas c, 
				dbo.sw_bancos b
		  WHERE 	
			  m.fecha_send     >=  @p_fecha1_2
		  AND m.fecha_send     <  @p_fecha2_2
		  AND m.casilla        =  c.cod_casilla 
		  AND m.cod_banco_rec  =  b.cod_banco 
		  AND m.branch_rec     =  b.branch
		  GROUP BY m.casilla,c.nombre_casilla,m.tipo_msg,t.nombre_tipo,m.cod_banco_rec,b.nombre_banco
		  ORDER BY m.casilla ASC,m.tipo_msg ASC
	   end
	else
	   begin
		  SELECT   m.casilla,
			c.nombre_casilla,
			m.tipo_msg,
			isnull(convert(VARCHAR,t.nombre_tipo),'''') as nombre_tipo,
			m.cod_banco_rec,
			b.nombre_banco,
			count(*) as cantidad
		  FROM 	dbo.sw_mensajes m 
		  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo, 
				dbo.sw_casillas c, 
				dbo.sw_bancos b
		  WHERE 	   
			  m.fecha_send     >=  @p_fecha1_2
		  AND m.fecha_send     <  @p_fecha2_2
		  AND m.casilla        =  @p_casilla 
		  AND m.casilla        =  c.cod_casilla 
		  AND m.cod_banco_rec  =  b.cod_banco 
		  AND m.branch_rec     =  b.branch
		  GROUP BY m.casilla,c.nombre_casilla,m.tipo_msg,t.nombre_tipo,m.cod_banco_rec,b.nombre_banco
		  ORDER BY m.casilla ASC,m.tipo_msg ASC
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_enc_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_enc_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_graba_enc_MS] @p_casilla   INT, 
		@p_sesion    INT, 
		@p_secuencia INT, 
		@p_rut_log   INT,
		@p_coment    VARCHAR(90) 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual CHAR(3)
	begin tran

	   SELECT   @estado_actual = estado_msg,
		@casilla_actual = casilla
	   FROM 	dbo.sw_mensajes
	   WHERE 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'') 

	   update dbo.sw_mensajes set casilla = @p_casilla,unidad = @p_casilla,estado_msg = ''ENC'',rut_entry = @p_rut_log,
	   fecha_ack = GetDate(),comentario = ''Encasillado Manual por Unidad Swift''  WHERE 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'')
	   if (@@error <> 0) and (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -1
	   end
	  
	   if exists(SELECT TOP 1 1 from dbo.sw_mensajes_add where sesion = @p_sesion and
	   secuencia = @p_secuencia and send_recv = ''R'')
	   begin
		  update dbo.sw_mensajes_add set casilla = @p_casilla,observ_encas = @p_coment  where  (sesion = @p_sesion) and
			(secuencia = @p_secuencia) and
			(send_recv = ''R'')
	   end
	else
	   begin
		  insert into dbo.sw_mensajes_add(sesion, secuencia, send_recv, casilla,
		  observ_encas, veces_rechazo, veces_reenvio)
		values(@p_sesion, @p_secuencia, ''R'', @p_casilla,
		  @p_coment, 0, 0)
	   end

	   if (@@error <> 0) and (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -2
	   end

	   INSERT INTO dbo.sw_log_msg
	VALUES(@p_sesion, @p_secuencia, ''R'', GetDate(),
			@p_rut_log, NULL, ''GS24'', ''sengrenc'',
			@casilla_actual, @estado_actual,
			@p_casilla, ''ENC'', 748, NULL, ''A'',
			''Mensaje Encasillado por U.SWIFT'') 

	   if (@@error <> 0) and (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -3
	   end

	   COMMIT tran
	   return 0
	END



	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_cnf_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_cnf_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'--exec proc_sw_rec_trae_cnf_rango_MS 999, ''20150101'',''20151010''

	CREATE PROCEDURE [dbo].[proc_sw_rec_trae_cnf_rango_MS] @p_casilla INT,

		@p_fecha1 datetime,
		@p_fecha2 datetime 
	AS
	begin



	-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).


	   SELECT   m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.prioridad, m.estado_msg,
			convert(CHAR(10),m.fecha_ack,103) as fecha_enc,
			convert(CHAR(8),m.fecha_ack,108) as hora_enc,
			convert(CHAR(10),a.fecha_recibe,103) as fecha_rcb,
			convert(CHAR(8),a.fecha_recibe,108) as hora_rcb,
			m.cod_banco_rec, m.branch_rec,
			m.cod_banco_em, m.branch_em,
			b.nombre_banco, b.ciudad_banco,
			b.pais_banco, b.oficina_banco,
			m.cod_moneda, m.monto,
			m.referencia, m.beneficiario,
			m.total_imp, m.comentario
	   FROM dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 

	   (select a.secuencia, a.send_recv, a.sesion, a.fecha_recibe, a.casilla FROM
		  sw_mensajes_add a 
	   where a.fecha_recibe  >= dateadd(dd,0,@p_fecha1)
	   and a.fecha_recibe  <  dateadd(dd,+1,@p_fecha2)
	   and a.casilla       =  @p_casilla
	   and a.send_recv     =  ''R'') a
	   WHERE a.secuencia     =  m.secuencia AND a.send_recv     =  m.send_recv AND a.sesion        =  m.sesion AND m.estado_msg    =  ''CNF''

	end
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_datos_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_datos_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_datos_msg_MS] 
		@p_sesion    INT,
		@p_secuencia INT,
		@p_sendrecv  CHAR(1) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON

	   SELECT   m.sesion, m.secuencia,
			m.casilla, c.nombre_casilla,
			m.tipo_msg, t.nombre_tipo,
			m.prioridad, e.descripcion,
			m.fecha_send, m.fecha_ack, m.rut_entry,
			a.observ_encas, a.unidad_recibe,
			m.rut_autoriza,	a.fecha_recibe,
			m.cod_banco_em, m.branch_em,
			b.nombre_banco, b.ciudad_banco,
			b.pais_banco, b.oficina_banco,
			m.cod_moneda, m.monto,
			m.total_imp, a.unidad_imprime,
			a.rut_imprime, a.fecha_imprime,
			a.unidad_rechazo, a.rut_rechazo,
			a.fecha_rechazo, a.texto_rechazo,
			a.veces_rechazo, a.rut_reenvio,
			a.fecha_reenvio, a.texto_reenvio,
			a.veces_reenvio, m.referencia,
			m.beneficiario, m.comentario
	   FROM 	dbo.sw_mensajes m 
	   LEFT OUTER JOIN dbo.sw_mensajes_add a ON m.sesion = a.sesion and m.secuencia = a.secuencia and m.send_recv = a.send_recv 
	   LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	   LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	   LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch 
	   LEFT OUTER JOIN dbo.sw_estados_msg e ON m.estado_msg = e.estado_msg
	   WHERE	m.sesion = @p_sesion AND m.secuencia = @p_secuencia AND m.send_recv = @p_sendrecv
	   ORDER BY m.secuencia
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_enc_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_enc_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'--exec [proc_sw_rec_trae_enc_rango_MS]

	CREATE PROCEDURE [dbo].[proc_sw_rec_trae_enc_rango_MS] @p_casilla INT, 



		@p_fecha1 datetime, 



		@p_fecha2 datetime 



	AS



	begin



	-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).







	   if @p_casilla = 0



	   begin



		  SELECT   m.sesion, m.secuencia,



			m.casilla, c.nombre_casilla,



			m.tipo_msg, t.nombre_tipo,



			m.prioridad, m.estado_msg,



			convert(CHAR(10),m.fecha_send,103) as fecha_env,



			convert(CHAR(8),m.fecha_send,108) as hora_env,



			convert(CHAR(10),m.fecha_ack,103) as fecha_enc,



			convert(CHAR(8),m.fecha_ack,108) as hora_enc,



			m.cod_banco_rec, m.branch_rec,



			m.cod_banco_em, m.branch_em,



			b.nombre_banco, b.ciudad_banco,



			b.pais_banco, b.oficina_banco,



			m.cod_moneda, m.monto,



			m.referencia, m.beneficiario,



			m.total_imp, m.comentario



		  FROM   dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 



	dbo.sw_casillas c



		  WHERE  m.fecha_ack     >= dateadd(dd,0,@p_fecha1) AND m.fecha_ack     <  dateadd(dd,+1,@p_fecha2) AND m.send_recv     = ''R'' AND m.estado_msg    = ''ENC'' AND c.cod_casilla   = 748



		  ORDER BY m.fecha_ack



	   end



	else



	   begin



		  SELECT   m.sesion, m.secuencia,



			m.casilla, c.nombre_casilla,



			m.tipo_msg, t.nombre_tipo,



			m.prioridad, m.estado_msg,



			convert(CHAR(10),m.fecha_send,103) as fecha_env,



			convert(CHAR(8),m.fecha_send,108) as hora_env,



			convert(CHAR(10),m.fecha_ack,103) as fecha_enc,



			convert(CHAR(8),m.fecha_ack,108) as hora_enc,



			m.cod_banco_rec, m.branch_rec,



			m.cod_banco_em, m.branch_em,



			b.nombre_banco, b.ciudad_banco,



			b.pais_banco, b.oficina_banco,



			m.cod_moneda, m.monto,



			m.referencia, m.beneficiario,



			m.total_imp, m.comentario



		  FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla



		  WHERE  m.fecha_ack     >= dateadd(dd,0,@p_fecha1) AND m.fecha_ack     <  dateadd(dd,+1,@p_fecha2) AND m.send_recv     =  ''R'' AND m.estado_msg    =  ''ENC'' AND m.casilla       =  @p_casilla



		  ORDER BY m.fecha_ack



	   end



	end
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_imp_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_imp_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'--exec proc_sw_rec_trae_imp_rango_MS  999, ''20140101'',''20151010''
	CREATE PROCEDURE [dbo].[proc_sw_rec_trae_imp_rango_MS] @p_casilla INT,

		@p_fecha1 datetime,

		@p_fecha2 datetime 

	AS

	begin

	-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   SELECT   m.sesion, m.secuencia,

			m.casilla, c.nombre_casilla,

			m.tipo_msg, t.nombre_tipo,

			m.prioridad, m.estado_msg,

			convert(CHAR(10),m.fecha_ack,103) as fecha_enc,

			convert(CHAR(8),m.fecha_ack,108) as hora_enc,

			convert(CHAR(10),a.fecha_imprime,103) as fecha_imp,

			convert(CHAR(8),a.fecha_imprime,108) as hora_imp,

			m.cod_banco_rec, m.branch_rec,

			m.cod_banco_em, m.branch_em,

			b.nombre_banco, b.ciudad_banco,

			b.pais_banco, b.oficina_banco,

			m.cod_moneda, m.monto,

			m.referencia, m.beneficiario,

			m.total_imp, m.comentario

	   FROM dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 

	(select a.sesion, a.secuencia , a.send_recv ,a.fecha_imprime
	from  dbo.sw_mensajes_add a
	   where a.fecha_imprime   >=  dateadd(dd,0,@p_fecha1)
	   and a.fecha_imprime   <   dateadd(dd,+1,@p_fecha2)) a

	   WHERE   a.sesion          =  m.sesion
	   and   a.secuencia       =  m.secuencia
	   and	a.send_recv       =  m.send_recv
	   and   m.casilla         =   @p_casilla
	   and   m.send_recv       =   ''R''
	   and   (m.estado_msg     =  ''IMP'' or m.total_imp > 0)
	   ORDER BY secuencia

	end
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_iny_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_iny_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_iny_rango_MS] 
		@p_casilla	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime 
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
			SELECT A.sesion, 
				   A.secuencia,
				   A.casilla, 
				   isnull(C.nombre_casilla,'''') as nombre_casilla,
				   isnull(A.tipo_msg,'''') as tipo_msg,
				   isnull(B.nombre_tipo,'''') as nombre_tipo,
				   isnull(A.prioridad,'''') as prioridad, 
				   isnull(A.estado_msg,'''') as estado_msg,
				   convert(CHAR(10),A.fecha_send,103) as fecha1,
				   convert(CHAR(8),A.fecha_send,108) as fecha2,
				   isnull(A.cod_banco_rec,'''') as cod_banco_rec, 
				   isnull(A.branch_rec,'''') as branch_rec,
				   isnull(A.cod_banco_em,'''') as cod_banco_em, 
				   isnull(A.branch_em,'''') as branch_em,
				   isnull(D.nombre_banco,'''') as nombre_banco, 
				   isnull(D.ciudad_banco,'''') as ciudad_banco,
				   isnull(D.pais_banco,'''') as pais_banco, 
				   isnull(D.oficina_banco,'''') as oficina_banco,
				   isnull(A.cod_moneda,'''')  as cod_moneda, 
				   isnull(A.monto,0) as monto,
				   isnull(A.referencia,'''') as referencia, 
				   isnull(A.beneficiario,'''') as beneficiario,
				   isnull(A.total_imp,0) as total_imp, 
				   isnull(A.comentario,'''') as comentario 
			FROM dbo.sw_mensajes A 
			LEFT OUTER JOIN dbo.sw_tipos_msg B ON A.tipo_msg = B.cod_tipo 
			LEFT OUTER JOIN dbo.sw_casillas C ON A.casilla = C.cod_casilla 
			LEFT OUTER JOIN dbo.sw_bancos D ON A.cod_banco_em = D.cod_banco and A.branch_em = D.branch
			WHERE A.fecha_send >= dateadd(dd,0,@p_fecha1) AND A.fecha_send < dateadd(dd,+1,@p_fecha2) AND A.send_recv = ''R''
			--WHERE A.fecha_send >= dateadd(dd,0,''2015-04-01'') AND A.fecha_send < dateadd(dd,+1,''2015-04-01'') AND A.send_recv = ''R''
			order by A.secuencia
	   end
	else
	   begin
			SELECT A.sesion, 
				   A.secuencia,
				   A.casilla, 
				   isnull(C.nombre_casilla,'''') as nombre_casilla,
				   isnull(A.tipo_msg,'''') as tipo_msg,
				   isnull(B.nombre_tipo,'''') as nombre_tipo,
				   isnull(A.prioridad,'''') as prioridad, 
				   isnull(A.estado_msg,'''') as estado_msg,
				   convert(CHAR(10),A.fecha_send,103) as fecha1,
				   convert(CHAR(8),A.fecha_send,108) as fecha2,
				   isnull(A.cod_banco_rec,'''') as cod_banco_rec, 
				   isnull(A.branch_rec,'''')    as branch_rec,
				   isnull(A.cod_banco_em,'''')  as cod_banco_em, 
				   isnull(A.branch_em,'''')     as branch_em,
				   isnull(D.nombre_banco,'''')  as nombre_banco, 
				   isnull(D.ciudad_banco,'''')  as ciudad_banco,
				   isnull(D.pais_banco,'''')    as pais_banco, 
				   isnull(D.oficina_banco,'''') as oficina_banco,
				   isnull(A.cod_moneda,'''')    as cod_moneda, 
				   isnull(A.monto,0) as monto,
				   isnull(A.referencia,'''') as referencia, 
				   isnull(A.beneficiario,'''') as beneficiario,
				   isnull(A.total_imp,0) as total_imp, 
				   isnull(A.comentario,'''')as comentario
			FROM dbo.sw_mensajes A 
			LEFT OUTER JOIN dbo.sw_tipos_msg B ON A.tipo_msg = B.cod_tipo 
			LEFT OUTER JOIN dbo.sw_casillas C ON A.casilla = C.cod_casilla 
			LEFT OUTER JOIN dbo.sw_bancos D ON A.cod_banco_em = D.cod_banco and A.branch_em = D.branch
			WHERE A.fecha_send >= dateadd(dd,0,@p_fecha1) AND A.fecha_send < dateadd(dd,+1,@p_fecha2) AND A.send_recv = ''R'' AND A.casilla = @p_casilla
			--WHERE A.fecha_send >= dateadd(dd,0,''2015-04-01'') AND A.fecha_send < dateadd(dd,+1,''2015-04-01'') AND A.send_recv = ''R'' AND A.casilla = 729
			order by A.secuencia
	   end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_iny_rango_paginado]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_iny_rango_paginado]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_iny_rango_paginado] 
		@p_casilla	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime,
		@offset int,
		@fetchRows smallint
	 
	AS

	BEGIN

		declare @fechaMin datetime = dateadd(dd,0,@p_fecha1)
		declare @fechaMax datetime = dateadd(dd,+1,@p_fecha2)

		declare @maxRows int
		declare @minSesion int
		declare @maxSesion int
				 
	   IF @p_casilla = 0
	   BEGIN

			SELECT  @maxRows=count(1),
					@minSesion=min(sesion),
					@maxSesion=max(sesion)
				FROM   dbo.sw_mensajes s
				WHERE 
					s.fecha_send    >= @fechaMin  
				AND s.fecha_send	< @fechaMax  
				AND s.send_recv     =  ''R''

			SELECT   sesion, secuencia,
						casilla, nombre_casilla,
						tipo_msg, nombre_tipo,
						prioridad, estado_msg,
						convert(CHAR(10),fecha_send,103) AS fecha_send,
						convert(CHAR(8),fecha_send,108) as hora_send,
						cod_banco_rec, branch_rec,
						cod_banco_em, branch_em,
						nombre_banco, ciudad_banco,
						pais_banco, oficina_banco,
						cod_moneda, monto,
						referencia, beneficiario,
						total_imp, comentario,
						@maxRows MaxRows
				FROM   dbo.sw_mensajes s
				LEFT OUTER JOIN dbo.sw_tipos_msg ON s.tipo_msg = sw_tipos_msg.cod_tipo 
				LEFT OUTER JOIN dbo.sw_casillas ON s.casilla = sw_casillas.cod_casilla 
				LEFT OUTER JOIN dbo.sw_bancos ON s.cod_banco_em = sw_bancos.cod_banco and s.branch_em = sw_bancos.branch
				WHERE 
					s.fecha_send    >= @fechaMin  
				AND s.fecha_send	< @fechaMax  
				AND s.send_recv     =  ''R''
				and s.sesion >= @minSesion
				and s.sesion <= @maxSesion
				order by secuencia
				OFFSET @offset ROWS
				FETCH NEXT @fetchRows ROWS ONLY
		END
		ELSE
		BEGIN
			SELECT  @maxRows=count(1),
				@minSesion=min(sesion),
				@maxSesion=max(sesion)
			FROM   dbo.sw_mensajes s
			WHERE 
				s.fecha_send    >= @fechaMin  
			AND s.fecha_send	< @fechaMax  
			AND s.send_recv     =  ''R''
			AND s.casilla		= @p_casilla

			SELECT   sesion, secuencia,
					casilla, nombre_casilla,
					tipo_msg, nombre_tipo,
					prioridad, estado_msg,
					convert(CHAR(10),fecha_send,103) as fecha_send,
					convert(CHAR(8),fecha_send,108) as hora_send,
					cod_banco_rec, branch_rec,
					cod_banco_em, branch_em,
					nombre_banco, ciudad_banco,
					pais_banco, oficina_banco,
					cod_moneda, monto,
					referencia, beneficiario,
					total_imp, comentario,
					@maxRows MaxRows
				FROM   dbo.sw_mensajes s
				LEFT OUTER JOIN dbo.sw_tipos_msg ON s.tipo_msg = sw_tipos_msg.cod_tipo 
				LEFT OUTER JOIN dbo.sw_casillas ON s.casilla = sw_casillas.cod_casilla 
				LEFT OUTER JOIN dbo.sw_bancos ON s.cod_banco_em = sw_bancos.cod_banco and s.branch_em = sw_bancos.branch
				WHERE 
					s.fecha_send    >= @fechaMin  
				AND s.fecha_send	< @fechaMax 
				AND s.send_recv     =  ''R'' 
				AND s.casilla       =  @p_casilla
				AND s.sesion >= @minSesion
				AND s.sesion <= @maxSesion				  
				order by secuencia
				OFFSET @offset ROWS
				FETCH NEXT @fetchRows ROWS ONLY
		END
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_iny_rango_paginado_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_iny_rango_paginado_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_iny_rango_paginado_MS] 
		@p_casilla	INT,
		@p_fecha1 	datetime,
		@p_fecha2 	datetime,
		@offset int,
		@fetchRows smallint,
		@searchText varchar(max) = null
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
		  2016-07-04   MBP	 Proyecto Comex.net, se agrega parametros para filtrar los resultados por texto
	*/
	   SET NOCOUNT ON

		declare @fechaMin datetime = dateadd(dd,0,@p_fecha1)
		declare @fechaMax datetime = dateadd(dd,+1,@p_fecha2)

		declare @maxRows int
		declare @minSesion int
		declare @maxSesion int
				 
	   IF @p_casilla = 0
	   BEGIN

			SELECT  @maxRows=count(1),
					@minSesion=min(sesion),
					@maxSesion=max(sesion)
				FROM   dbo.sw_mensajes s
				WHERE 
					s.fecha_send    >= @fechaMin  
				AND s.fecha_send	< @fechaMax  
				AND s.send_recv     =  ''R''
				AND (@searchText is null OR (s.sesion like ''%'' + @searchText + ''%'' OR s.secuencia like ''%'' + @searchText + ''%'' OR s.casilla LIKE @searchText
				 OR s.tipo_msg like @searchText OR s.referencia LIKE ''%'' + @searchText + ''%'' OR s.beneficiario like ''%'' + @searchText + ''%'' OR s.cod_moneda LIKE @searchText OR
				 s.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(s.cod_banco_em) + RTRIM(s.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(s.cod_banco_rec) + RTRIM(s.branch_rec) like ''%'' + @searchText + ''%'') 
				))

			SELECT   sesion, secuencia,
						casilla, nombre_casilla,
						tipo_msg, nombre_tipo,
						prioridad, estado_msg,
						convert(CHAR(10),fecha_send,103) AS fecha_send,
						convert(CHAR(8),fecha_send,108) as hora_send,
						cod_banco_rec, branch_rec,
						cod_banco_em, branch_em,
						nombre_banco, ciudad_banco,
						pais_banco, oficina_banco,
						cod_moneda, monto,
						referencia, beneficiario,
						total_imp, comentario,
						@maxRows MaxRows
				FROM   dbo.sw_mensajes s
				LEFT OUTER JOIN dbo.sw_tipos_msg ON s.tipo_msg = sw_tipos_msg.cod_tipo 
				LEFT OUTER JOIN dbo.sw_casillas ON s.casilla = sw_casillas.cod_casilla 
				LEFT OUTER JOIN dbo.sw_bancos ON s.cod_banco_em = sw_bancos.cod_banco and s.branch_em = sw_bancos.branch
				WHERE 
					s.fecha_send    >= @fechaMin  
				AND s.fecha_send	< @fechaMax  
				AND s.send_recv     =  ''R''
				and s.sesion >= @minSesion
				and s.sesion <= @maxSesion
				AND (@searchText is null OR (s.sesion like ''%'' + @searchText + ''%'' OR s.secuencia like ''%'' + @searchText + ''%'' OR s.casilla LIKE @searchText
				 OR s.tipo_msg like @searchText OR s.referencia LIKE ''%'' + @searchText + ''%'' OR s.beneficiario like ''%'' + @searchText + ''%'' OR s.cod_moneda LIKE @searchText OR
				 s.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(s.cod_banco_em) + RTRIM(s.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(s.cod_banco_rec) + RTRIM(s.branch_rec) like ''%'' + @searchText + ''%'') 
				))
				order by secuencia
				OFFSET @offset ROWS
				FETCH NEXT @fetchRows ROWS ONLY
		END
		ELSE
		BEGIN
			SELECT  @maxRows=count(1),
				@minSesion=min(sesion),
				@maxSesion=max(sesion)
			FROM   dbo.sw_mensajes s
			WHERE 
				s.fecha_send    >= @fechaMin  
			AND s.fecha_send	< @fechaMax  
			AND s.send_recv     =  ''R''
			AND s.casilla		= @p_casilla
			AND (@searchText is null OR (s.sesion like ''%'' + @searchText + ''%'' OR s.secuencia like ''%'' + @searchText + ''%'' OR s.casilla LIKE @searchText
				 OR s.tipo_msg like @searchText OR s.referencia LIKE ''%'' + @searchText + ''%'' OR s.beneficiario like ''%'' + @searchText + ''%'' OR s.cod_moneda LIKE @searchText OR
				 s.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(s.cod_banco_em) + RTRIM(s.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(s.cod_banco_rec) + RTRIM(s.branch_rec) like ''%'' + @searchText + ''%'') 
				))

			SELECT   sesion, secuencia,
					casilla, nombre_casilla,
					tipo_msg, nombre_tipo,
					prioridad, estado_msg,
					convert(CHAR(10),fecha_send,103) as fecha_send,
					convert(CHAR(8),fecha_send,108) as hora_send,
					cod_banco_rec, branch_rec,
					cod_banco_em, branch_em,
					nombre_banco, ciudad_banco,
					pais_banco, oficina_banco,
					cod_moneda, monto,
					referencia, beneficiario,
					total_imp, comentario,
					@maxRows MaxRows
				FROM   dbo.sw_mensajes s
				LEFT OUTER JOIN dbo.sw_tipos_msg ON s.tipo_msg = sw_tipos_msg.cod_tipo 
				LEFT OUTER JOIN dbo.sw_casillas ON s.casilla = sw_casillas.cod_casilla 
				LEFT OUTER JOIN dbo.sw_bancos ON s.cod_banco_em = sw_bancos.cod_banco and s.branch_em = sw_bancos.branch
				WHERE 
					s.fecha_send    >= @fechaMin  
				AND s.fecha_send	< @fechaMax 
				AND s.send_recv     =  ''R'' 
				AND s.casilla       =  @p_casilla
				AND s.sesion >= @minSesion
				AND s.sesion <= @maxSesion
				AND (@searchText is null OR (s.sesion like ''%'' + @searchText + ''%'' OR s.secuencia like ''%'' + @searchText + ''%'' OR s.casilla LIKE @searchText
				 OR s.tipo_msg like @searchText OR s.referencia LIKE ''%'' + @searchText + ''%'' OR s.beneficiario like ''%'' + @searchText + ''%'' OR s.cod_moneda LIKE @searchText OR
				 s.monto like ''%'' + @searchText + ''%'' or
				 (RTRIM(s.cod_banco_em) + RTRIM(s.branch_em) like ''%'' + @searchText + ''%'')  OR (RTRIM(s.cod_banco_rec) + RTRIM(s.branch_rec) like ''%'' + @searchText + ''%'') 
				))				  
				order by secuencia
				OFFSET @offset ROWS
				FETCH NEXT @fetchRows ROWS ONLY
		END
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_otr_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_otr_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_otr_rango_MS] @p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   SELECT   m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_send,103) fecha_send,
		convert(CHAR(8),m.fecha_send,108) hora_send,
		convert(CHAR(10),m.fecha_ack,103) fecha_pro,
		convert(CHAR(8),m.fecha_ack,108) hora_pro,
		m.cod_banco_rec, m.branch_rec,
		m.cod_banco_em, m.branch_em,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, m.monto,
		m.referencia, m.beneficiario,
		m.total_imp, m.unidad
	   FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.unidad = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch
	   WHERE 	m.fecha_ack     >=  dateadd(dd,0,@p_fecha1) AND m.fecha_ack     <   dateadd(dd,+1,@p_fecha2) AND m.send_recv     =  ''R'' AND m.estado_msg    =  ''SEN'' AND m.casilla       =  @p_casilla
	   UNION
	   SELECT   m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_send,103) fecha_send,
		convert(CHAR(8),m.fecha_send,108) hora_send,
		convert(CHAR(10),m.fecha_ack,103) fecha_pro,
		convert(CHAR(8),m.fecha_ack,108) hora_pro,
		m.cod_banco_rec, m.branch_rec,
		m.cod_banco_em, m.branch_em,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, m.monto,
		m.referencia, m.beneficiario,
		m.total_imp, m.unidad
	   FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.unidad = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch
	   WHERE 	m.fecha_ack     >=  dateadd(dd,0,@p_fecha1) AND m.fecha_ack     <   dateadd(dd,+1,@p_fecha2) AND m.send_recv     =  ''R'' AND m.estado_msg    =  ''PDU'' AND m.casilla       =  @p_casilla
	   UNION
	   SELECT   m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_send,103) fecha_send,
		convert(CHAR(8),m.fecha_send,108) hora_send,
		convert(CHAR(10),a.fecha_rechazo,103) fecha_pro,
		convert(CHAR(8),a.fecha_rechazo,108) hora_pro,
		m.cod_banco_rec, m.branch_rec,
		m.cod_banco_em, m.branch_em,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, m.monto,
		m.referencia, m.beneficiario,
		m.total_imp, a.unidad_rechazo
	   FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.unidad = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 
	dbo.sw_mensajes_add a
	   WHERE 	a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo   <   dateadd(dd,+1,@p_fecha2) AND a.sesion          =  m.sesion AND a.secuencia       =  m.secuencia AND a.send_recv       =  m.send_recv AND m.send_recv       =  ''R'' AND m.estado_msg      =  ''REC'' AND m.casilla         =  @p_casilla
	   UNION
	   SELECT   m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_send,103) fecha_send,
		convert(CHAR(8),m.fecha_send,108) hora_send,
		convert(CHAR(10),a.fecha_rechazo,103) fecha_pro,
		convert(CHAR(8),a.fecha_rechazo,108) hora_pro,
		m.cod_banco_rec, m.branch_rec,
		m.cod_banco_em, m.branch_em,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, m.monto,
		m.referencia, m.beneficiario,
		m.total_imp, a.unidad_rechazo
	   FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.unidad = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 
	dbo.sw_mensajes_add a
	   WHERE 	a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo   <   dateadd(dd,+1,@p_fecha2) AND a.sesion          =  m.sesion AND a.secuencia       =  m.secuencia AND a.send_recv       =  m.send_recv AND m.send_recv       =  ''R'' AND m.estado_msg      =  ''IMR'' AND m.casilla         =  @p_casilla
	   UNION
	   SELECT   m.sesion, m.secuencia,
		m.casilla, c.nombre_casilla,
		m.tipo_msg, t.nombre_tipo,
		m.prioridad, m.estado_msg,
		convert(CHAR(10),m.fecha_send,103) fecha_send,
		convert(CHAR(8),m.fecha_send,108) hora_send,
		convert(CHAR(10),a.fecha_rechazo,103) fecha_pro,
		convert(CHAR(8),a.fecha_rechazo,108) hora_pro,
		m.cod_banco_rec, m.branch_rec,
		m.cod_banco_em, m.branch_em,
		b.nombre_banco, b.ciudad_banco,
		b.pais_banco, b.oficina_banco,
		m.cod_moneda, m.monto,
		m.referencia, m.beneficiario,
		m.total_imp, a.unidad_rechazo
	   FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.unidad = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 
	dbo.sw_mensajes_add a
	   WHERE 	a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo   <   dateadd(dd,+1,@p_fecha2) AND a.sesion          =  m.sesion AND a.secuencia       =  m.secuencia AND a.send_recv       =  m.send_recv AND m.send_recv       =  ''R'' AND m.estado_msg      =  ''CNR'' AND m.casilla         =  @p_casilla
	   ORDER BY secuencia
	END



	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_por_id]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_por_id]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_por_id]
		@sesion int, 
		@secuencia int
	AS
	BEGIN
		SET NOCOUNT ON;

	SELECT   sesion, secuencia,
			casilla, nombre_casilla,
			tipo_msg, nombre_tipo,
			prioridad, estado_msg,
			convert(CHAR(10),fecha_send,103) as fecha_env,
			convert(CHAR(8),fecha_send,108) as hora_env,
			cod_banco_rec, branch_rec,
			cod_banco_em, branch_em,
			nombre_banco, ciudad_banco,
			pais_banco, oficina_banco,
			cod_moneda, monto,
			referencia, beneficiario,
			total_imp, comentario
		  FROM 	dbo.sw_mensajes 
		  LEFT OUTER JOIN dbo.sw_tipos_msg ON sw_mensajes.tipo_msg = sw_tipos_msg.cod_tipo 
		  LEFT OUTER JOIN dbo.sw_casillas ON sw_mensajes.casilla = sw_casillas.cod_casilla 
		  LEFT OUTER JOIN dbo.sw_bancos ON sw_mensajes.cod_banco_em = sw_bancos.cod_banco and sw_mensajes.branch_em = sw_bancos.branch
		  WHERE sesion=@sesion and secuencia=@secuencia AND sw_mensajes.send_recv =  ''R'' 
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_por_id_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_por_id_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_por_id_MS]
		@sesion int, 
		@secuencia int
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON

	SELECT   sesion, secuencia,
			casilla, nombre_casilla,
			tipo_msg, nombre_tipo,
			prioridad, estado_msg,
			convert(CHAR(10),fecha_send,103) as fecha_env,
			convert(CHAR(8),fecha_send,108) as hora_env,
			cod_banco_rec, branch_rec,
			cod_banco_em, branch_em,
			nombre_banco, ciudad_banco,
			pais_banco, oficina_banco,
			cod_moneda, monto,
			referencia, beneficiario,
			total_imp, comentario
		  FROM 	dbo.sw_mensajes 
		  LEFT OUTER JOIN dbo.sw_tipos_msg ON sw_mensajes.tipo_msg = sw_tipos_msg.cod_tipo 
		  LEFT OUTER JOIN dbo.sw_casillas ON sw_mensajes.casilla = sw_casillas.cod_casilla 
		  LEFT OUTER JOIN dbo.sw_bancos ON sw_mensajes.cod_banco_em = sw_bancos.cod_banco and sw_mensajes.branch_em = sw_bancos.branch
		  WHERE sesion=@sesion and secuencia=@secuencia AND sw_mensajes.send_recv =  ''R'' 
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_ree_rango_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_ree_rango_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'--EXEC proc_sw_rec_trae_ree_rango_MS 999, ''20100101'',''20151006''

	CREATE PROCEDURE [dbo].[proc_sw_rec_trae_ree_rango_MS] @p_casilla INT,

		@p_fecha1 datetime,

		@p_fecha2 datetime 

	AS

	begin

	-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

	   SELECT top 20  m.sesion, m.secuencia,

			m.casilla, c.nombre_casilla,

			m.tipo_msg, t.nombre_tipo,

			m.prioridad, m.estado_msg,

			convert(CHAR(10),a.fecha_rechazo,103) as fecha_rec,

			convert(CHAR(8),a.fecha_rechazo,108) as hora_rec,

			convert(CHAR(10),a.fecha_reenvio,103) as fecha_ree,

			convert(CHAR(8),a.fecha_reenvio,108) as hora_ree,

			m.cod_banco_rec, m.branch_rec,

			m.cod_banco_em, m.branch_em,

			b.nombre_banco, b.ciudad_banco,

			b.pais_banco, b.oficina_banco,

			m.cod_moneda, m.monto,

			m.referencia, m.beneficiario,

			m.total_imp, m.comentario

	   FROM 	dbo.sw_mensajes m LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_em = b.cod_banco and m.branch_em = b.branch, 

	dbo.sw_mensajes_add a
	WHERE	a.fecha_reenvio >=  dateadd(dd,0,@p_fecha1) AND a.fecha_reenvio <   dateadd(dd,+1,@p_fecha2) AND a.sesion        =  m.sesion AND a.secuencia     =  m.secuencia AND a.send_recv     =  m.send_recv AND m.send_recv     =  ''R'' AND m.estado_msg    =  ''
	REE'' AND m.casilla       =  @p_casilla

	ORDER BY m.secuencia,a.fecha_reenvio

	end' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_resumen_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_resumen_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_resumen_msg_MS] 
		@p_fecha1 datetime,
		@p_fecha2 datetime 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

	   SELECT   m.casilla,
			 c.nombre_casilla,
			 m.estado_msg,
			 e.descripcion,
			 count(*) as cantidad
	   FROM dbo.sw_mensajes m, dbo.sw_casillas c, dbo.sw_estados_msg e
	   WHERE m.fecha_send  >=  dateadd(dd,0,@p_fecha1)
	   and m.fecha_send  <   dateadd(dd,+1,@p_fecha2)
	   and m.casilla     =  c.cod_casilla
	   and m.send_recv   =  e.send_recv
	   and m.estado_msg  =  e.estado_msg
	   GROUP BY m.casilla,c.nombre_casilla,m.estado_msg,e.descripcion
	   ORDER BY m.casilla ASC,m.estado_msg ASC
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_bancos_MS]
	@Codigo nvarchar(50)=NULL,@Nombre nvarchar(50)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL and @Nombre is not NULL)
	 begin
		 SELECT cod_banco,branch,nombre_banco,direccion_banco,ciudad_banco,pais_banco,oficina_banco,intercambio_clave, localidad_banco,
		 pobnr_banco FROM sw_bancos where cod_banco=@Codigo and nombre_banco= @Nombre
	end
	 if (@Codigo is not NULL and @Nombre is NULL)
	 begin
		 SELECT cod_banco,branch,nombre_banco,direccion_banco,ciudad_banco,pais_banco,oficina_banco,intercambio_clave, localidad_banco,
		 pobnr_banco FROM sw_bancos where cod_banco=@Codigo
	end
	 if (@Codigo is NULL and @Nombre is  not NULL)
	 begin
		 SELECT cod_banco,branch,nombre_banco,direccion_banco,ciudad_banco,pais_banco,oficina_banco,intercambio_clave, localidad_banco,
		 pobnr_banco FROM sw_bancos where nombre_banco= @Nombre
	end
	if (@Codigo is NULL and @Nombre is NULL)
	begin
		 SELECT top 1000 cod_banco,branch,nombre_banco,direccion_banco,ciudad_banco,pais_banco,oficina_banco,intercambio_clave, localidad_banco,
		 pobnr_banco FROM sw_bancos
	end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_bancos_verificados_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_bancos_verificados_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_bancos_verificados_MS]
	@codigo nvarchar(50)
	
	AS
	BEGIN
		select cod_banco, 
	nombre_banco,
	 direccion_banco, 
	 ciudad_banco, 
	 pais_banco, 
	 oficina_banco, 
	 intercambio_clave, 
	 localidad_banco, 
	 pobnr_banco from 
	 (SELECT DISTINCT cod_banco, 
	 nombre_banco, direccion_banco, 
	 ciudad_banco, pais_banco, 
	 oficina_banco, intercambio_clave, 
	 localidad_banco, pobnr_banco,branch 
	 FROM sw_bancos  WHERE sw_bancos.cod_banco = @codigo) a ORDER BY branch desc
	 END
	  ' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_CampoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_CampoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_CampoMensajes_MS]
	@Codigo nvarchar(15)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL)
	 begin
	 SELECT tag_campo, linea_campo, nombre_campo, largo_campo, formato_campo 
	FROM sw_campos_msg where tag_campo=@Codigo
	end
	if (@Codigo is NULL)
	begin
	SELECT tag_campo, linea_campo, nombre_campo, largo_campo, formato_campo 
	FROM sw_campos_msg
	end
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_CaracterInvalido_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_CaracterInvalido_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_CaracterInvalido_MS]
	@Codigo nvarchar(2)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL)
	 begin
	SELECT sw_caracter_error.valor_ascii, sw_caracter_error.caracter, sw_caracter_error.descripcion FROM sw_caracter_error
	where caracter=@Codigo
	end
	if (@Codigo is NULL)
	begin
	SELECT sw_caracter_error.valor_ascii, sw_caracter_error.caracter, sw_caracter_error.descripcion FROM sw_caracter_error
	end
	END' 
END



/****** Object:  StoredProcedure [dbo].[proc_sw_trae_firma]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_firma]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_firma] 
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
	 sw_msgsend_firma.avisado, '' '' as nombre_firma ,
	 1 as estaba, avisado as avis_antes 
	 FROM sw_msgsend_firma 
	 WHERE ( id_mensaje = @Idmensaje ) AND ( estado_firma <> ''N'' ) AND ( estado_firma <> ''D'' )

	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_fmt_campos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_fmt_campos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'
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


	   if SUBSTRING(@p_tipo_mt,4,1) = ''9''
		  select   @p_tipo_mt = stuff(@p_tipo_mt,3,1,''0'')

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


	   update #tmp_fmt set nombre = isnull(convert(VARCHAR,b.nombre_campo_tipcam),'''') from dbo.sw_tipos_campos b, #tmp_fmt a where b.tag_campo_tipcam = a.tag
	   and tipo_msg_tipcam    = ''MT000''
	   and (a.nombre is null or rtrim(a.nombre) = '''')

	   select * from #tmp_fmt
	   order by orden,tag,linea
	END


	IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = ''proc_sw_trae_fmt_ciclos_MS'' AND schema_id = SCHEMA_ID(''dbo''))
		DROP PROCEDURE [dbo].sce_xplv_MS 
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_fmt_ciclos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_fmt_ciclos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'
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


	   if SUBSTRING(@p_tipo_mt,4,1) = ''9''
		  select   @p_tipo_mt = stuff(@p_tipo_mt,3,1,''0'')


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

 

	   update #tmp_fmt set nombre = isnull(convert(VARCHAR,b.nombre_campo_tipcam),'''') from dbo.sw_tipos_campos b, #tmp_fmt a where b.tag_campo_tipcam = a.tag
	   and tipo_msg_tipcam = ''MT000''
	   and (a.nombre is null or rtrim(a.nombre) = '''')

	   select * from #tmp_fmt
	   order by orden,tag,linea
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_fmt_largo_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_fmt_largo_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'
	CREATE PROCEDURE [dbo].[proc_sw_trae_fmt_largo_MS] 
		@p_tipo_mt CHAR(7) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 
   
	   if SUBSTRING(@p_tipo_mt,4,1) = ''9''
		  select   @p_tipo_mt = stuff(@p_tipo_mt,3,1,''0'')

	   if exists(SELECT TOP 1 1 from dbo.sw_formatos where repeticion_fmt > 0 and tipo_msg_fmt = @p_tipo_mt)
		  SELECT   
			dbo.sw_ciclos.tag_fmt,
			dbo.sw_ciclos.status_fmt,
			dbo.sw_ciclos.repeticion_fmt,
			dbo.sw_ciclos.orden_fmt,
			dbo.sw_ciclos.secuencia_fmt,
			sum(dbo.sw_campos_msg.largo_campo) as largo_total
		  FROM  	dbo.sw_ciclos 
		  LEFT OUTER JOIN dbo.sw_campos_msg ON dbo.sw_ciclos.tag_fmt = dbo.sw_campos_msg.tag_campo
		  WHERE 	dbo.sw_ciclos.tipo_msg_fmt = @p_tipo_mt
		  GROUP BY dbo.sw_ciclos.tipo_msg_fmt,dbo.sw_ciclos.tag_fmt,dbo.sw_ciclos.repeticion_fmt, 
		  dbo.sw_ciclos.orden_fmt,dbo.sw_ciclos.status_fmt,dbo.sw_ciclos.secuencia_fmt
		  ORDER BY dbo.sw_ciclos.orden_fmt
	else
	   SELECT   
			dbo.sw_formatos.tag_fmt,
			dbo.sw_formatos.status_fmt,
			dbo.sw_formatos.repeticion_fmt,
			dbo.sw_formatos.orden_fmt,
			dbo.sw_formatos.secuencia_fmt,
			sum(dbo.sw_campos_msg.largo_campo) as largo_total
	   FROM  	dbo.sw_formatos 
	   LEFT OUTER JOIN dbo.sw_campos_msg ON dbo.sw_formatos.tag_fmt = dbo.sw_campos_msg.tag_campo
	   WHERE 	dbo.sw_formatos.tipo_msg_fmt = @p_tipo_mt
	   GROUP BY dbo.sw_formatos.tipo_msg_fmt,dbo.sw_formatos.tag_fmt,dbo.sw_formatos.repeticion_fmt, 
	   dbo.sw_formatos.orden_fmt,dbo.sw_formatos.status_fmt,dbo.sw_formatos.secuencia_fmt
	   ORDER BY dbo.sw_formatos.orden_fmt
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_folio_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_folio_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'
	CREATE PROCEDURE [dbo].[proc_sw_trae_folio_MS] 
		@id_appl CHAR(10) 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Reescritura utilizando capacidades de SQL 2012 para evitar problemas de concurrencia.  Proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

		DECLARE @folios TABLE(nro_folio INT)
		DECLARE @folio INT

		BEGIN TRAN

		UPDATE dbo.sw_folios SET nro_folio = nro_folio+1
		OUTPUT inserted.nro_folio INTO @folios
		WHERE id_folio = @id_appl

		IF @@error <> 0
		BEGIN
			ROLLBACK 
			SELECT   -1
			RETURN -1
		END

		SELECT @folio=nro_folio from @folios
	
		IF (@folio is null)
		BEGIN
			INSERT INTO dbo.sw_folios VALUES(@id_appl, 1)

			IF @@error <> 0
			BEGIN
				ROLLBACK 
				SELECT   -1
				RETURN -1
			END
			SET @folio = 1
		END
	
	
	   COMMIT TRAN
	   SELECT   @folio

	   RETURN 0
	END




	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_FormatoMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_FormatoMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_FormatoMensajes_MS]
	@Codigo nvarchar(15)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL)
	 begin
	SELECT tipo_msg_fmt, orden_fmt, secuencia_fmt, repeticion_fmt, tag_fmt, status_fmt 
	FROM sw_formatos where tipo_msg_fmt=@Codigo 
	end
	if (@Codigo is NULL)
	begin
	SELECT tipo_msg_fmt, orden_fmt, secuencia_fmt, repeticion_fmt, tag_fmt, status_fmt 
	FROM sw_formatos
	end
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_GlosaCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_GlosaCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_GlosaCampos_MS]
	@Codigo nvarchar(15)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL)
	 begin
	SELECT tipo_msg_tipcam, tag_campo_tipcam, nombre_campo_tipcam FROM sw_tipos_campos
	where tipo_msg_tipcam=@Codigo
	end
	if (@Codigo is NULL)
	begin
	SELECT tipo_msg_tipcam, tag_campo_tipcam, nombre_campo_tipcam FROM sw_tipos_campos
	end
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_monedas_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_monedas_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'-- =============================================
	-- Author:		<Hernan Palominos>
	-- Description:	<SP utilizado para obtener los registros 
	--de los usuarios de la tabla sw_monedas utilizado en el aplicativo mantenedores Swift>
	-- =============================================
	CREATE PROCEDURE [dbo].[proc_sw_trae_monedas_MS]
	
	AS
	BEGIN
		SET NOCOUNT ON;
	
		 SELECT sw_monedas.cod_moneda_sw, sw_monedas.cod_moneda_banco, sw_monedas.nombre_moneda, sw_monedas.uso_moneda_banco,
		  sw_monedas.decimales FROM sw_monedas
  
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_paridad_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_paridad_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_paridad_MS]
	@Codigo nvarchar(5)=NULL,@Nombre nvarchar(50)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL and @Nombre is not NULL)
	 begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) and sw_monedas.cod_moneda_sw=@Codigo 
	and sw_monedas.nombre_moneda=@Nombre
	end
	 if (@Codigo is not NULL and @Nombre is NULL)
	 begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) and sw_monedas.cod_moneda_sw=@Codigo
	end
	 if (@Codigo is NULL and @Nombre is  not NULL)
	 begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) and sw_monedas.nombre_moneda=@Nombre
	end
	if (@Codigo is NULL and @Nombre is NULL)
	begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) 
	end
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_paridades_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_paridades_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_paridades_MS]
	@Codigo nvarchar(5)=NULL,@Nombre nvarchar(50)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL and @Nombre is not NULL)
	 begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) and sw_monedas.cod_moneda_sw=@Codigo 
	and sw_monedas.nombre_moneda=@Nombre
	end
	 if (@Codigo is not NULL and @Nombre is NULL)
	 begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) and sw_monedas.cod_moneda_sw=@Codigo
	end
	 if (@Codigo is NULL and @Nombre is  not NULL)
	 begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) and sw_monedas.nombre_moneda=@Nombre
	end
	if (@Codigo is NULL and @Nombre is NULL)
	begin
	SELECT sw_monedas.cod_moneda_sw, sw_monedas.nombre_moneda, sw_monedas.cod_moneda_banco, sw_paridad.paridad, sw_paridad.tipcam_observ, 
	convert(CHAR(10),sw_paridad.fecha_valor,103) as fecha_valor FROM sw_monedas, 
	sw_paridad WHERE ( sw_monedas.cod_moneda_banco = sw_paridad.cod_moneda_banco) 
	end
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_TiposMensajes_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_TiposMensajes_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_TiposMensajes_MS]
	@Codigo nvarchar(15)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL)
	 begin
	SELECT cod_tipo, nombre_tipo FROM sw_tipos_msg  where sw_tipos_msg.cod_tipo=@Codigo 
	end
	if (@Codigo is NULL)
	begin
	SELECT cod_tipo, nombre_tipo FROM sw_tipos_msg 
	end
	END' 
END



/****** Object:  StoredProcedure [dbo].[proc_sw_trae_valoresCampos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_valoresCampos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[proc_sw_trae_valoresCampos_MS]
	@Codigo nvarchar(15)=NULL
	AS
	BEGIN
	 if (@Codigo is not NULL)
	 begin
	SELECT sw_valor_campos.tipo_msg, 
	sw_valor_campos.tag_campo, 
	sw_valor_campos.linea_campo, 
	sw_valor_campos.cond_valor, 
	sw_valor_campos.valor_campo, sw_valor_campos.total_valor
	FROM sw_valor_campos where sw_valor_campos.tipo_msg=@Codigo  ORDER BY
	sw_valor_campos.tag_campo ASC, sw_valor_campos.tipo_msg ASC, sw_valor_campos.linea_campo ASC
	end
	if (@Codigo is NULL)
	begin
	SELECT sw_valor_campos.tipo_msg, 
	sw_valor_campos.tag_campo, 
	sw_valor_campos.linea_campo, 
	sw_valor_campos.cond_valor, 
	sw_valor_campos.valor_campo, sw_valor_campos.total_valor
	FROM sw_valor_campos ORDER BY
	sw_valor_campos.tag_campo ASC, sw_valor_campos.tipo_msg ASC, sw_valor_campos.linea_campo ASC
	end
	END' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_valida_estado_msg_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_valida_estado_msg_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_valida_estado_msg_MS]
		@sesion int , 
		@secuencia int
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	  SET NOCOUNT ON 

		SELECT estado_msg 
		FROM sw_mensajes 
		WHERE sesion = @sesion and 
		secuencia = @secuencia and 
		send_recv = ''R'' 

	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_trae_bancos]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_bancos]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_trae_bancos] 
		@Codbanco char(9), 
		@Branch char(3)
	AS
	BEGIN
	/*
		Descripcion: Busca destos de los bancos

		Historia: AOC20150730 primera version

		Ejemplos
			 EXEC [proc_trae_bancos] ''AAALSARI'', ''   ''
			 EXEC [proc_trae_bancos] ''AAALSARI'', ''XXX''
			 EXEC [proc_trae_bancos] ''ZURIUS44'', ''ZNA'' 

	*/
		SET NOCOUNT ON;
	
		SELECT 
			cod_banco,
			branch,
			nombre_banco,
			direccion_banco,
			ciudad_banco,
			pais_banco,
			oficina_banco,
			intercambio_clave,
			localidad_banco,
			pobnr_banco
		FROM sw_bancos where (cod_banco = @Codbanco) and branch = ''   ''
		UNION ALL
		SELECT 
			cod_banco,
			branch,
			nombre_banco,
			direccion_banco,
			ciudad_banco,
			pais_banco,
			oficina_banco,
			intercambio_clave,
			localidad_banco,
			pobnr_banco
		FROM sw_bancos where (cod_banco = @Codbanco) and branch = @Branch
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_trae_bancos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_trae_bancos_MS] 
		@Codbanco char(9), 
		@Branch char(3)
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON
	
		SELECT 
			cod_banco,
			branch,
			nombre_banco,
			direccion_banco,
			ciudad_banco,
			pais_banco,
			oficina_banco,
			intercambio_clave,
			localidad_banco,
			pobnr_banco
		FROM sw_bancos where (cod_banco = @Codbanco) and branch = ''   ''
		UNION ALL
		SELECT 
			cod_banco,
			branch,
			nombre_banco,
			direccion_banco,
			ciudad_banco,
			pais_banco,
			oficina_banco,
			intercambio_clave,
			localidad_banco,
			pobnr_banco
		FROM sw_bancos where (cod_banco = @Codbanco) and branch = @Branch
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_trae_formato_campos]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_formato_campos]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'

	CREATE PROCEDURE [dbo].[proc_trae_formato_campos] 
	AS
	BEGIN
	/*	
	Historial:
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

		select 
			tag_campo,
			linea_campo,
			nombre_campo,
			largo_campo,
			formato_campo
		from sw_campos_msg where (
			formato_campo like ''01A 03D 02A 03C 15%'' or 
			formato_campo like ''01A 06G 03C%'' or 
			formato_campo like ''05D 03C 15%'' or 
			formato_campo like ''06G 03C%'' or 
			formato_campo like ''03C%'' or 
			formato_campo like ''03A%'' or 
			formato_campo like ''06G%'' or 
			formato_campo like ''08G%'' or 
			formato_campo like ''11T%'')
	END

	' 
END

/****** Object:  StoredProcedure [dbo].[proc_trae_formato_campos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_formato_campos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'

	CREATE PROCEDURE [dbo].[proc_trae_formato_campos_MS] 
	AS
	BEGIN
	/*	
	Historial:
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
	   SET NOCOUNT ON 

		select 
			tag_campo,
			linea_campo,
			nombre_campo,
			largo_campo,
			formato_campo
		from sw_campos_msg where (
			formato_campo like ''01A 03D 02A 03C 15%'' or 
			formato_campo like ''01A 06G 03C%'' or 
			formato_campo like ''05D 03C 15%'' or 
			formato_campo like ''06G 03C%'' or 
			formato_campo like ''03C%'' or 
			formato_campo like ''03A%'' or 
			formato_campo like ''06G%'' or 
			formato_campo like ''08G%'' or 
			formato_campo like ''11T%'')
	END

	' 
END

/****** Object:  StoredProcedure [dbo].[proc_trae_tipo_campos]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_tipo_campos]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_trae_tipo_campos] 
	
	AS
	BEGIN
		SET NOCOUNT ON;

		select 
			sw_tipos_campos.tipo_msg_tipcam, 
			sw_tipos_campos.tag_campo_tipcam,
			sw_tipos_campos.nombre_campo_tipcam, 
			(select max(largo_campo) 
			from dbo.sw_campos_msg
			where sw_campos_msg.tag_campo = sw_tipos_campos.tag_campo_tipcam) as ''max_largo''  
		from dbo.sw_tipos_campos
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_trae_tipo_campos_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_trae_tipo_campos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_trae_tipo_campos_MS] 
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON

		select 
			sw_tipos_campos.tipo_msg_tipcam, 
			sw_tipos_campos.tag_campo_tipcam,
			sw_tipos_campos.nombre_campo_tipcam, 
			(select max(largo_campo) 
			from dbo.sw_campos_msg
			where sw_campos_msg.tag_campo = sw_tipos_campos.tag_campo_tipcam) as ''max_largo''  
		from dbo.sw_tipos_campos
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[sw_configura_s01_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sw_configura_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sw_configura_s01_MS] 
	(
	  @rut_usuario int
	)

	as


	declare @estado int 

		set @estado = 0;

	IF  EXISTS(SELECT rut_usuario, cod_aplicac FROM sw_configura WHERE rut_usuario = @rut_usuario  AND cod_aplicac = ''R'' )
		set @estado = 1;

	select @estado as estado;

	' 
END

/****** Object:  StoredProcedure [dbo].[sw_mensajes_add_s01_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sw_mensajes_add_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'
	--exec sw_mensajes_add_s01_MS 1,0
	--exec  sw_mensajes_add_s01_MS  0,460497
	create procedure [dbo].[sw_mensajes_add_s01_MS]
	(
	 @sesion int,
	 @secuencia int
	)
	as

	declare @estado int;

	set @estado = 0;

	if exists(SELECT sw_mensajes_add.observ_encas FROM sw_mensajes_add WHERE (sw_mensajes_add.sesion = @sesion ) AND ( sw_mensajes_add.secuencia = @secuencia ) AND ( sw_mensajes_add.send_recv = ''R'' ))
	set @estado = 1;

	select @estado as estado
	' 
END

/****** Object:  StoredProcedure [dbo].[UpdateMensajeS_MS]    Script Date: 22-10-2015 11:11:59 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateMensajeS_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateMensajeS_MS] (
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



	end
	' 
END



-- FIN de script Release 2

PRINT N'Creating [dbo].[proc_sw_env_trae_env_rango]...';



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_env_rango' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_env_trae_env_rango] 


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_env_rango] 
	@p_casilla INT,
	@p_fecha1 datetime,
	@p_fecha2 datetime 

	AS
	BEGIN

		DECLARE @p_fecha1_2 DATETIME = dateadd(dd,0,@p_fecha1)
		DECLARE @p_fecha2_2 DATETIME = dateadd(dd,+1,@p_fecha2)

		declare @min_id_mensaje int
		declare @max_id_mensaje int

		SELECT 
			@min_id_mensaje = MIN(id_mensaje), 
			@max_id_mensaje = MAX(id_mensaje)
		FROM dbo.sw_msgsend_detfile df with (index=sk2_msgsend_detfile) -- Se requiere este hint para evitar que haga un scan utilizando el indice por id_mensaje
		WHERE  df.fecha_envio >= @p_fecha1_2 and df.fecha_envio < @p_fecha2_2
   
	if @p_casilla = 0
	begin
		SELECT   
			m.id_mensaje, 
			m.sesion, m.secuencia,
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
		FROM 	dbo.sw_msgsend m 
		INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje = m.id_mensaje 
		LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
		LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
		LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
		LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
		WHERE  m.estado_msg       = ''ENV''
			AND df.fecha_envio >= @p_fecha1_2 and df.fecha_envio <  @p_fecha2_2
			AND m.id_mensaje >= @min_id_mensaje 
			AND m.id_mensaje <= @max_id_mensaje 
			AND df.id_mensaje >= @min_id_mensaje 
			AND df.id_mensaje <= @max_id_mensaje 
		order by m.id_mensaje,df.fecha_envio

	end
else
   begin
		SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
			FROM 	 dbo.sw_msgsend m 
			INNER JOIN dbo.sw_msgsend_detfile df ON df.id_mensaje = m.id_mensaje 
			LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
			LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
			LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
			LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw
		WHERE  m.casilla         =  @p_casilla 
			AND m.estado_msg       = ''ENV''
			AND df.fecha_envio >= @p_fecha1_2 and df.fecha_envio <  @p_fecha2_2
			AND m.id_mensaje >= @min_id_mensaje 
			AND m.id_mensaje <= @max_id_mensaje 
			AND df.id_mensaje >= @min_id_mensaje 
			AND df.id_mensaje <= @max_id_mensaje 
		order by m.id_mensaje,df.fecha_envio
   end
END'
END


PRINT N'Creating [dbo].[proc_sw_env_trae_por_idmensaje]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_por_idmensaje' AND schema_id = SCHEMA_ID('dbo')) 
  DROP PROCEDURE [dbo].[proc_sw_env_trae_por_idmensaje]


SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_por_idmensaje]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_por_idmensaje] 
	  @idMensaje int
	AS
	BEGIN
	  -- SET NOCOUNT ON added to prevent extra result sets from
	  -- interfering with SELECT statements.
	  SET NOCOUNT ON;

	SELECT   m.id_mensaje, m.sesion, m.secuencia,
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





/****** Object:  StoredProcedure [dbo].[proc_sw_trae_MensajesEliminar_MS]    Script Date: 18-11-2015 4:20:47 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_MensajesEliminar_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_MensajesEliminar_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_count_bancos_MS]    Script Date: 18-11-2015 4:20:47 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_count_bancos_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_trae_count_bancos_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_MensajeSwift_MS]    Script Date: 18-11-2015 4:20:47 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_MensajeSwift_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_MensajeSwift_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_MensajeSwift_MS]    Script Date: 18-11-2015 4:20:47 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_MensajeSwift_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[proc_sw_elimina_MensajeSwift_MS] 
	@codigo INT
	AS
	BEGIN
	declare @bit BIT
		BEGIN TRY
		SET NOCOUNT ON;

	 delete sw_msgsend where id_mensaje = @codigo
	 delete sw_msgsend_add where id_mensaje = @codigo
	 delete sw_msgsend_log where id_mensaje = @codigo

			set @bit=1
			END TRY
	BEGIN CATCH
	set @bit=0
	END CATCH
	select @bit as ''status''
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_count_bancos_MS]    Script Date: 18-11-2015 4:20:47 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_count_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_count_bancos_MS]
	@swift nvarchar(50)=NULL, @pais nvarchar(50)=NULL,@ciudad nvarchar(50)=NULL,
	@direccion nvarchar(50)=NULL,@banco nvarchar(50)=NULL
	AS
	BEGIN
	if(@swift is null and @pais is null and @ciudad is null and @direccion is null and @banco is null)
	begin
		 SELECT count(cod_banco) as ''total'' FROM sw_bancos
		 end
		 else
		 begin
		  SELECT count(cod_banco) as ''total'' FROM sw_bancos where cod_banco=@swift or pais_banco  LIKE ''''+@pais+''%''
		 or ciudad_banco=@ciudad or direccion_banco=@direccion or nombre_banco=@banco
		 end
	END
	' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_trae_MensajesEliminar_MS]    Script Date: 18-11-2015 4:20:47 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_MensajesEliminar_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_MensajesEliminar_MS]
	@Codigo INT=null
	
	AS
	BEGIN
		SET NOCOUNT ON;
		SELECT sw_msgsend.id_mensaje, 
	sw_msgsend.tipo_msg, 
	sw_msgsend.fecha_ingreso, 
	sw_msgsend.estado_msg, 
	sw_estados_msg.descripcion, 
	sw_msgsend.casilla, 
	sw_casillas.nombre_casilla 
	FROM sw_estados_msg, sw_casillas, 
	sw_msgsend WHERE 
	( sw_estados_msg.estado_msg = sw_msgsend.estado_msg ) and 
	( sw_msgsend.casilla = sw_casillas.cod_casilla ) and 
	( sw_estados_msg.send_recv = ''S'' ) and ( sw_msgsend.id_mensaje = @Codigo )
	END
	' 
END


PRINT N'Creating [dbo].[proc_sw_env_test_firma_MS]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_test_firma_MS' AND schema_id = SCHEMA_ID('dbo')) 
  DROP PROCEDURE [dbo].[proc_sw_env_test_firma_MS] 


/****** Object:  StoredProcedure [dbo].[proc_sw_env_test_firma_MS]    Script Date: 19/11/15 14:07:53 ******/
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_test_firma_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_test_firma_MS] 
	@rut	INT,
	@fecha	datetime 
AS
BEGIN
   declare @fecha2 datetime
   select   @fecha2 = dateadd(mi,-30,@fecha)

   SELECT   m.id_mensaje, m.sesion, m.secuencia,
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
			 m.referencia, m.beneficiario, m.rut_ingreso
	  FROM  dbo.sw_msgsend m 
	  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
	  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
	  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
	  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw 
	  LEFT OUTER JOIN dbo.sw_estados_msg e on m.estado_msg = e.estado_msg
	  WHERE  m.fecha_ingreso <=  @fecha2 AND e.send_recv = ''S'' AND
	  m.rut_ingreso = @rut AND
	  (m.estado_msg = ''INY'' OR m.estado_msg = ''DIG'' OR m.estado_msg = ''MOD'')
	  ORDER BY m.fecha_ingreso
END'
END



PRINT N'Creating [dbo].[proc_sw_env_test_firma_count_MS]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_test_firma_count_MS' AND schema_id = SCHEMA_ID('dbo'))  
  DROP PROCEDURE [dbo].[proc_sw_env_test_firma_count_MS] 


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_test_firma_count_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_test_firma_count_MS] 
	@rut	INT,
	@fecha	datetime 
AS
BEGIN
   declare  @fecha2 datetime
   select   @fecha2 = dateadd(mi,-30,@fecha)

   SELECT   count(*) FROM 	dbo.sw_msgsend m, dbo.sw_estados_msg e
   WHERE 	 m.fecha_ingreso <= @fecha2
   and    m.rut_ingreso = @rut
   and    (m.estado_msg = ''INY'' OR m.estado_msg = ''DIG'' or m.estado_msg = ''MOD'')
   and    m.estado_msg = e.estado_msg
   and    e.send_recv = ''S''
END'
END


PRINT N'Creating [dbo].[proc_sw_env_ing_firma_MS]...';

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_ing_firma_MS' AND schema_id = SCHEMA_ID('dbo'))  
  DROP PROCEDURE [dbo].proc_sw_env_ing_firma_MS 


/****** Object:  StoredProcedure [dbo].[proc_sw_env_ing_firma]    Script Date: 26/11/15 17:02:04 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_ing_firma_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_ing_firma_MS] 
		@p_id_mensaje 	INT, 
		@p_rut_firma 	INT,
		@p_tipo_firma   CHAR(1),
		@p_estado       CHAR(1),
		@p_revfir       CHAR(1),
		@p_rut_solic    INT,
		@p_fecha_solic  datetime,
		@p_avisado      CHAR(1) 
	AS
	BEGIN
	   if exists(SELECT TOP 1 1 from dbo.sw_msgsend_firma
	   where id_mensaje = @p_id_mensaje and rut_firma = @p_rut_firma)
	   begin
		  update dbo.sw_msgsend_firma set tipo_firma   = @p_tipo_firma,estado_firma = @p_estado,revisa_firma = @p_revfir,
		  fecha_solic  = @p_fecha_solic,avisado      = @p_avisado  WHERE   id_mensaje = @p_id_mensaje and
		  rut_firma  = @p_rut_firma and
		  rut_solic  = @p_rut_solic
	   end
	else
	   begin
		  INSERT INTO dbo.sw_msgsend_firma(id_mensaje,
			  rut_firma,
			  tipo_firma,
			  estado_firma,
			  revisa_firma,
			  rut_solic,
			  fecha_solic,
			  avisado)
		VALUES(@p_id_mensaje,
			  @p_rut_firma,
			  @p_tipo_firma,
			  @p_estado,
			  @p_revfir,
			  @p_rut_solic,
			  @p_fecha_solic,
			  @p_avisado)
	   end

	   if @@error <> 0
		  return -1
	else
	   return 0
	END'
END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_trae_bancos2_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_trae_bancos2_MS] 

-- =============================================
-- Author:	Microsoft
-- Create date: 2015-11-24
-- Description:	Obtiene un listado de bancos
-- =============================================
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_bancos2_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_bancos2_MS] 
	-- Add the parameters for the stored procedure here
	@swift varchar(13) = NULL,
	@pais varchar(35) = NULL,
	@ciudad varchar(35) = NULL,
	@banco varchar(70) = NULL,
	@direccion varchar(70) = NULL,
	@branch varchar(70) = NULL,
	@intercambioClave varchar(70) = NULL
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		[cod_banco]
		,[branch]
		,[nombre_banco]
		,[direccion_banco]
		,[ciudad_banco]
		,[pais_banco]
		,[oficina_banco]
		,[intercambio_clave]
		,[localidad_banco]
		,[pobnr_banco]
  FROM [dbo].[sw_bancos]
  WHERE ((@swift IS NULL) OR (@swift = '''') OR (cod_banco LIKE @swift + ''%''))
	AND ((@pais is NULL) OR (@pais = '''') OR (pais_banco LIKE @pais + ''%''))
	AND ((@ciudad is NULL) OR (@ciudad = '''') OR (ciudad_banco LIKE @ciudad + ''%''))
	AND ((@banco is NULL) OR (@banco = '''') OR (nombre_banco LIKE @banco + ''%''))
	AND ((@direccion is NULL) OR (@direccion = '' OR (direccion_banco LIKE @direccion + ''%''))
	AND ((@branch is NULL) OR (@branch = '''') OR (branch LIKE @branch + ''%''))
	AND ((@intercambioClave is NULL) OR (@intercambioClave = '''') OR (intercambio_clave LIKE @intercambioClave + ''%''))

END'
END


/****** Object:  StoredProcedure [dbo].[proc_sw_msgsend_log_i01]    ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_msgsend_log_i01_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_msgsend_log_i01_MS]


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_msgsend_log_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure proc_sw_msgsend_log_i01_MS
	@id_mensaje int,
	@fecha_log datetime,
	@rutais_log int,
	@idprog_log varchar(8),
	@opcion_log varchar(8),
	@casilla_destino int,
	@estado_destino varchar(3),
	@unidad_log int,
	@resultado_log varchar(1),
	@comentario_log varchar(80)
AS
	insert into sw_msgsend_log
		(id_mensaje,fecha_log,rutais_log,nodoais_log,idprog_log,opcion_log,
		casilla_destino,estado_destino,unidad_log,resultado_log,comentario_log)
	values
		(@id_mensaje,@fecha_log,@rutais_log,'''',@idprog_log,@opcion_log,
		@casilla_destino,@estado_destino,@unidad_log,@resultado_log,@comentario_log)'
END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_mod_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_env_graba_mod_MS]



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_mod_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_mod_MS 
	@p_id_mensaje   INT,
	@p_casilla 	INT, 
	@p_rut		INT 	
AS
BEGIN

	BEGIN TRAN

	   DECLARE @casilla_actual INT,@estado_actual 	CHAR(3),@opcion_log 	CHAR(8),@fecha_actual 	datetime,
	   @fecha_log	datetime
	   SELECT   @fecha_log = max(fecha_log)
	   FROM 	dbo.sw_msgsend_log
	   WHERE 	id_mensaje = @p_id_mensaje

	   SELECT   @estado_actual = estado_destino,
			@casilla_actual = casilla_destino
	   FROM 	dbo.sw_msgsend_log
	   WHERE 	id_mensaje = @p_id_mensaje and
	   fecha_log = @fecha_log	


	   select   @fecha_actual = GetDate()

	   if exists(SELECT TOP 1 1 from dbo.sw_msgsend_add where id_mensaje = @p_id_mensaje)
	   begin
		  update dbo.sw_msgsend_add set fecha_modifica = @fecha_actual,unidad_modifica = @p_casilla,rut_modifica = @p_rut,
		  veces_modifica = veces_modifica+1  WHERE 	(id_mensaje = @p_id_mensaje)
	   end
	else
	   begin
		  INSERT INTO dbo.sw_msgsend_add(id_mensaje, fecha_modifica, rut_modifica, unidad_modifica,
		  veces_modifica, veces_rechazo, veces_bloqueo)
		VALUES(@p_id_mensaje, @fecha_actual, @p_rut, @p_casilla,
		  1, 0, 0)
	   end
	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -1
	   end

   select   @opcion_log = lower(@estado_actual)+''grmod''

   INSERT INTO dbo.sw_msgsend_log
VALUES(@p_id_mensaje, @fecha_actual,
	 @p_rut, left(@@servername, 10),
		 ''SWIV'', @opcion_log,
	 @casilla_actual, @estado_actual,
	 @casilla_actual, ''MOD'',
	 @p_casilla, ''A'', ''Mensaje Modificado'')

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -2
	   end

	   COMMIT tran

   return 0
END'

END







IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_graba_cambios_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_env_graba_cambios_MS]



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_cambios_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.proc_sw_env_graba_cambios_MS
	@p_id_mensaje   INT,
	@cambios        CHAR(80) 	
AS
BEGIN

	   DECLARE @estado_actual  CHAR(3),@fecha_log	datetime
	begin tran

	   SELECT   @estado_actual = estado_msg
	   FROM	dbo.sw_msgsend
	   WHERE 	id_mensaje = @p_id_mensaje 

   if @estado_actual = ''MOD''
   begin
	  update dbo.sw_msgsend set comentario = ''Mensaje Modificado.''+@cambios  WHERE 	id_mensaje = @p_id_mensaje
	  if (@@error <> 0) or (@@rowcount = 0)
	  begin
		 ROLLBACK 
		 return -1
	  end
	  select   @fecha_log = max(fecha_log)
	  from 	dbo.sw_msgsend_log
	  where 	id_mensaje = @p_id_mensaje and
	  estado_destino = ''MOD''

	  update dbo.sw_msgsend_log set comentario_log =  ''Mensaje Modificado.''+@cambios  
	  WHERE 	id_mensaje = @p_id_mensaje and
	  estado_destino = ''MOD'' and
	  fecha_log = @fecha_log

		  if (@@error <> 0) or (@@rowcount = 0)
		  begin
			 ROLLBACK 
			 return -2
		  end
	   end

   COMMIT tran
   return 0
END'
END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_rec_borra_casilla_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_rec_borra_casilla_MS]



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_borra_casilla_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_borra_casilla_MS] 
	@p_casilla   INT 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	begin tran

	   delete dbo.sw_mensajes
	   where casilla = @p_casilla
	   if (@@error <> 0)
	   begin
		  ROLLBACK 
		  select -1
	   end
	  

	   delete dbo.sw_mensajes_add
	   where casilla = @p_casilla
	   if (@@error <> 0)
	   begin
		  ROLLBACK 
		  select -2
	   end

	   delete dbo.sw_log_msg
	   where casilla_destino = @p_casilla
	   if (@@error <> 0)
	   begin
		  ROLLBACK 
		  select -3
	   end

	   COMMIT tran
	   select 0
	END'
END






IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_del_firma_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_env_del_firma_MS]


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_del_firma_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_del_firma_MS] 
	@p_id_mensaje 	INT, 
	@p_rut_firma 	INT 
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:53:53 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   DELETE	dbo.sw_msgsend_firma
   WHERE 	id_mensaje = @p_id_mensaje 	and
   rut_firma  = @p_rut_firma

   if @@error <> 0
	  return -1
else
   return 0
END'
END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_trae_usuarios_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_trae_usuarios_MS]



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_usuarios_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[proc_sw_trae_usuarios_MS]
	@p_rut INT,
	@p_nombre nvarchar(50),
	@p_tipo nvarchar(50)
	
	AS

	BEGIN
		SET NOCOUNT ON;

	declare @p_rut_l INT 
	declare	@p_nombre_l nvarchar(50)
	declare	@p_tipo_l nvarchar(50)

set @p_rut_l = ISNULL(@p_rut,0)
set @p_nombre_l = upper(ISNULL(@p_nombre,''''))
set @p_tipo_l = isnull(@p_tipo,'''')

	  select sw_users_swift.rut_user, sw_users_swift.digv_user, sw_users_swift.nombre_user, sw_users_swift.tipo_user 
	  FROM sw_users_swift
	  where 
	  ( @p_rut_l = 0 or @p_rut_l = rut_user)
	  and ( @p_nombre_l = '''' or upper(nombre_user) LIKE ''%''+@p_nombre_l+''%'')
	  and (@p_tipo_l = ''T'' or @p_tipo_l = upper(tipo_user))
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_trae_casillas_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_trae_casillas_MS]



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_casillas_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[proc_sw_trae_casillas_MS]
	@codCasilla INT,
	@nombreCasilla nvarchar(50),
	@origenCasilla nvarchar(2)
	
	AS
	BEGIN
		SET NOCOUNT ON;

		declare @codCasilla_l int 
		declare @nombreCasilla_l nvarchar(50)
		declare @origenCasilla_l nvarchar(2)

	set @codCasilla_l = ISNULL(@codCasilla,0)
	set @nombreCasilla_l = upper(ISNULL(@nombreCasilla,''''))
	set @origenCasilla_l = upper(ISNULL(@origenCasilla,''''))

	 SELECT sw_casillas.cod_casilla, sw_casillas.nombre_casilla, sw_casillas.origen_recep 
	 FROM sw_casillas 
	 where ( @codCasilla_l = 0 or @codCasilla_l = cod_casilla) 
	 and (@nombreCasilla_l = '''' or nombre_casilla like ''%''+@nombreCasilla_l+''%'' ) 
	 and (@origenCasilla_l = '''' or @origenCasilla_l = origen_recep)
	 order by cod_casilla
	 

  
END'
END



IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_mensaje_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_elimina_mensaje_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_busca_eliminar_MS]    Script Date: 04-01-2016 10:28:41 a. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_busca_eliminar_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_busca_eliminar_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_busca_eliminar_MS]    Script Date: 04-01-2016 10:28:41 a. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_busca_eliminar_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_busca_eliminar_MS] @sesion     INT, @secuencia INT
	AS
	begin
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).


	   SELECT sw_mensajes.casilla, sw_mensajes.estado_msg, sw_estados_msg.descripcion, sw_casillas.nombre_casilla 
	   FROM sw_mensajes, sw_estados_msg, sw_casillas 
	WHERE ( sw_mensajes.send_recv = sw_estados_msg.send_recv ) and 
	( sw_estados_msg.estado_msg = sw_mensajes.estado_msg ) and 
	( sw_mensajes.casilla = sw_casillas.cod_casilla ) and
	( sw_mensajes.sesion = @sesion ) AND ( sw_mensajes.secuencia = @secuencia)

	
	End' 
END

/****** Object:  StoredProcedure [dbo].[proc_sw_elimina_mensaje_MS]    Script Date: 04-01-2016 10:28:41 a. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_elimina_mensaje_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_elimina_mensaje_MS] @sesion     INT, @secuencia INT
	AS
	begin

		delete sw_mensajes where sesion =@sesion and secuencia=@secuencia  and send_recv = ''R''
		delete sw_mensajes_add where sesion=@sesion and secuencia=@secuencia  and send_recv = ''R''
		delete sw_log_msg where sesion_log=@sesion and secuencia_log=@secuencia and send_recv_log = ''R''
	End
' 
END



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_test_apr_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].[proc_sw_env_test_apr_MS]


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_test_apr_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_test_apr_MS] 
		@p_casilla INT,                  
		@rut       INT                                         
AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:45:45 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   declare @registros INT
   select   @registros = count(*)
   from   dbo.sw_msgsend_firma f, dbo.sw_msgsend m
   where  f.rut_firma    =  @rut
   and  f.estado_firma =  ''P''
   and  f.id_mensaje   =  m.id_mensaje
   and  m.estado_msg   in(''AUP'',''SAP'')

	   if @@error = 0
		  SELECT   @registros
	else
	   SELECT   -2

   return
END'
END

--select * from sw_msgsend


PRINT 'Stored Procedure [dbo].[tipoder]'

IF OBJECT_ID('proc_rh_swi_001', 'P') IS NOT NULL
	DROP PROC proc_rh_swi_001


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_rh_swi_001]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [proc_rh_swi_001] (@nurut INT)
AS
   SELECT a.fun_atributo,
		  SUBSTRING(b.tabla_des,1,80) as ''Poder''
   FROM   rh_trabajador a,tipoder b
   WHERE  a.num_rut = @nurut        AND
		  a.cod_pago = ''CDPAG00000'' AND
		  a.fun_atributo = b.tabla_codig
RETURN'
END



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_count_bancos_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_count_bancos_MS] AS BEGIN SET NOCOUNT ON END' 
	EXEC dbo.sp_executesql @statement = N'ALTER PROCEDURE [dbo].[proc_sw_trae_count_bancos_MS]
		@swift nvarchar(50)=NULL,
		@pais nvarchar(50)=NULL,
		@ciudad nvarchar(50)=NULL,
		@direccion nvarchar(50)=NULL,
		@banco nvarchar(50)=NULL,
		@branch nvarchar(50)=NULL,
		@intercambioClave nvarchar(50)=NULL
	AS
	BEGIN

		SELECT count(cod_banco) as ''total''
		FROM sw_bancos 
		WHERE 
			(cod_banco LIKE CASE WHEN (@swift is NULL OR @swift = '''') THEN cod_banco ELSE @swift + ''%'' END)
			and (branch LIKE CASE WHEN (@branch is NULL OR @branch = '''')  THEN branch ELSE @branch + ''%'' END)
			AND (ciudad_banco LIKE CASE WHEN (@ciudad is NULL OR @ciudad = '''') THEN ciudad_banco ELSE @ciudad + ''%'' END)
			AND (nombre_banco LIKE CASE WHEN (@banco is NULL OR @banco = '''') THEN nombre_banco ELSE @banco + ''%'' END)
			AND (direccion_banco LIKE CASE WHEN (@direccion is NULL OR @direccion = '''') THEN direccion_banco ELSE ''%'' + @direccion + ''%'' END)
			AND (UPPER(ISNULL(@pais, '''')) = CASE WHEN (ISNULL(pais_banco, '''') = '''') THEN '''' ELSE UPPER(pais_banco) END)
			AND (intercambio_clave LIKE CASE WHEN (@intercambioClave is NULL OR @intercambioClave = '''') THEN intercambio_clave ELSE @intercambioClave + ''%'' END)

	END'
END





IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_trae_tiposMensajeConFormato_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_trae_tiposMensajeConFormato_MS] AS BEGIN SET NOCOUNT ON END' 
	EXEC dbo.sp_executesql @statement = N'ALTER PROCEDURE [dbo].[proc_sw_trae_tiposMensajeConFormato_MS]
	AS
	BEGIN
	SELECT cod_tipo,
		   nombre_tipo
	FROM sw_tipos_msg
	WHERE (CASE WHEN SUBSTRING(cod_tipo,4,1) = ''9'' THEN stuff(cod_tipo,3,1,''0'') ELSE cod_tipo END) IN
		(SELECT DISTINCT tipo_msg_fmt
		 FROM dbo.sw_formatos
		 WHERE repeticion_fmt > 0
		   AND tipo_msg_fmt IN
			 (SELECT DISTINCT tipo_msg_fmt
			  FROM sw_ciclos)
		 UNION SELECT DISTINCT b.tipo_msg_fmt
		 FROM dbo.sw_formatos b
		 LEFT OUTER JOIN dbo.sw_tipos_campos c ON b.tipo_msg_fmt = c.tipo_msg_tipcam
		 AND b.tag_fmt = c.tag_campo_tipcam,
			 dbo.sw_campos_msg a
		 WHERE b.repeticion_fmt = 0)
	GROUP BY cod_tipo,
			 nombre_tipo
	ORDER BY cod_tipo

	END'
END





IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_test_file_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].proc_sw_env_test_file_MS


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_test_file_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_test_file_MS]
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:45:45 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   declare @registros INT
	   SELECT   @registros = count(*)
	   FROM 	dbo.sw_msgsend_files
	   WHERE 	(estado_archivo = ''P'') and
			(estado_ftp = ''T'')

	   if @@error <> 0
		  return -1

	   SELECT   @registros
	   return 0
	END'
END



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sw_configura_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].sw_configura_s02_MS


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sw_configura_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sw_configura_s02_MS] 
	(
	  @rut_usuario int,
	  @apli      CHAR(1) 
	)

	as

	declare @estado int 

		   set @estado = 0;

	IF  EXISTS(SELECT rut_usuario, cod_aplicac FROM sw_configura WHERE rut_usuario = @rut_usuario  AND cod_aplicac = @apli )
		   set @estado = 1;

	select @estado as estado;'
END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_update_aut_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].proc_sw_env_update_aut_MS



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_update_aut_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE proc_sw_env_update_aut_MS (
		 @idMensaje int,
		 @casilla int
	)

	AS 

	UPDATE sw_msgsend SET estado_msg = ''AUT'' WHERE id_mensaje = @idMensaje  AND casilla = @casilla

	IF exists(SELECT TOP 1 1 FROM sw_msgsend_add WHERE id_mensaje = @idMensaje)
	BEGIN
		UPDATE sw_msgsend_add SET fecha_aprobac = GetDate() WHERE (id_mensaje = @idMensaje)
	END
	ELSE
	BEGIN
		INSERT INTO sw_msgsend_add(id_mensaje, fecha_aprobac) VALUES (@idMensaje, GetDate())
	END'
END





IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_env_trae_rech_swi_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].proc_sw_env_trae_rech_swi_MS


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_rech_swi_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_rech_swi_MS] @p_casilla INT,
		@p_fecha1 datetime,
		@p_fecha2 datetime 

	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:49:49 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   CREATE table #ing_rech_swi
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
		  fecha_rechazo     CHAR(10)      null,
		  hora_rechazo      CHAR(8)       null,
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

	   CREATE clustered index idx_ing_rech_swi on #ing_rech_swi(fecha_de_orden)


	   if @p_casilla = 0
	   begin
		  insert  #ing_rech_swi
		  SELECT  m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario,
					a.fecha_rechazo
		  FROM  dbo.sw_msgsend m 
		  LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo 
		  LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla 
		  LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
		  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
		  dbo.sw_msgsend_add a
		  WHERE  a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) 
		  AND a.fecha_rechazo   <  dateadd(dd,+1,@p_fecha2) 
		  -- AND a.unidad_rechazo  =  753
		  AND a.id_mensaje      =  m.id_mensaje 
		  AND m.estado_msg      =  ''REM''
		  UNION
		  SELECT  m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario,
					a.fecha_rechazo
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
		  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE  a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo   <   dateadd(dd,+1,@p_fecha2) 
		  AND a.unidad_rechazo  =  @p_casilla AND a.id_mensaje      =  m.id_mensaje AND m.estado_msg      =  ''RES''
	   end
	else
	   begin
		  insert  #ing_rech_swi
		  SELECT 	m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario,
					a.fecha_rechazo
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
		  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE   a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo   <  dateadd(dd,+1,@p_fecha2) AND a.unidad_rechazo  =  @p_casilla AND a.id_mensaje      =  m.id_mensaje AND m.estado_msg      =  ''REM'' AND m.casilla         =  @p_casilla
		  UNION
		  SELECT 	m.id_mensaje, m.sesion, m.secuencia,
					m.casilla, c.nombre_casilla,
					m.tipo_msg, t.nombre_tipo,
					m.prioridad, m.estado_msg,
					convert(CHAR(10),m.fecha_ingreso,103) as fecha_ingr,
					convert(CHAR(8),m.fecha_ingreso,108) as hora_ingr,
					convert(CHAR(10),a.fecha_rechazo,103) as fecha_rech,
					convert(CHAR(8),a.fecha_rechazo,108) as hora_rech,
					m.cod_banco_em, m.branch_em,
					m.cod_banco_rec, m.branch_rec,
					b.nombre_banco, b.ciudad_banco,
					b.pais_banco, b.oficina_banco,
					m.cod_moneda, d.nombre_moneda,
					d.cod_moneda_banco, m.monto,
					m.referencia, m.beneficiario,
					a.fecha_rechazo
		  FROM  dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch 
		  LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
	dbo.sw_msgsend_add a
		  WHERE   a.fecha_rechazo   >=  dateadd(dd,0,@p_fecha1) AND a.fecha_rechazo   <  dateadd(dd,+1,@p_fecha2) AND a.unidad_rechazo  = @p_casilla 
		  AND a.id_mensaje      =  m.id_mensaje AND m.estado_msg      =  ''RES'' AND m.casilla         =  @p_casilla
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
				  fecha_rechazo,
				  hora_rechazo,
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
	   from #ing_rech_swi
	END'
END




IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_aum_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_aum_MS] AS BEGIN SET NOCOUNT ON END' 
	EXEC dbo.sp_executesql @statement = N'ALTER PROCEDURE [dbo].[proc_sw_env_graba_aum_MS] @p_id_mensaje 	INT, 
		@p_casilla 	INT, 
		@p_rut_log 	INT, 
		@p_fecha_aum 	datetime,
		@p_comentario   VARCHAR(90) 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:53:53 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual 	CHAR(3),@p_opcion 	CHAR(8),@monto_swift    FLOAT,
	   @todas		CHAR(1),@fecha_aprob	datetime
	begin tran

	   SELECT   @estado_actual = estado_msg,
		@casilla_actual = casilla,
		@monto_swift = monto
	   FROM 	dbo.sw_msgsend
	   WHERE 	id_mensaje = @p_id_mensaje

	   SELECT   @fecha_aprob = fecha_aprobac
	   FROM	dbo.sw_msgsend_add
	   WHERE	id_mensaje = @p_id_mensaje

	   EXECUTE proc_sw_env_cons_fito @p_id_mensaje,@monto_swift,@todas OUTPUT
	   if @todas = ''S'' and @fecha_aprob IS NOT NULL
	   begin
		  commit tran
		  select   @todas
		  return 0
	   end

	   update dbo.sw_msgsend set estado_msg = ''AUM'',comentario = @p_comentario  WHERE 	id_mensaje = @p_id_mensaje

	   if @@error <> 0 or @@rowcount = 0
	   begin
		  rollback 
		  return -1
	   end

	   if not exists(SELECT TOP 1 1 from dbo.sw_msgsend_add where id_mensaje = @p_id_mensaje)
		  INSERT INTO dbo.sw_msgsend_add(id_mensaje, fecha_aprobac,
		   veces_rechazo, veces_modifica, veces_bloqueo)
    		VALUES(@p_id_mensaje, @p_fecha_aum,
		   0, 0, 0)

	else
	   update dbo.sw_msgsend_add set fecha_aprobac = @p_fecha_aum  WHERE 	id_mensaje = @p_id_mensaje
	   if @@error <> 0 or @@rowcount = 0
	   begin
		  rollback 
		  return -2
	   end

	   update dbo.sw_msgsend_firma set estado_firma = ''F'',fecha_firma = @p_fecha_aum  WHERE 	id_mensaje    = @p_id_mensaje 	and
	   fecha_solic  <= @p_fecha_aum 	and
	   estado_firma <> ''F''
	   if @@error <> 0
	   begin
		  rollback 
		  return -3
	   end

	   EXECUTE proc_sw_env_cons_fito @p_id_mensaje,@monto_swift,@todas OUTPUT
	   if @todas = ''N''
	   begin
		  rollback 
		  select   @todas
		  return -100
	   end

	   select   @p_opcion = lower(@estado_actual)+''graum''

	   INSERT INTO dbo.sw_msgsend_log
	VALUES(@p_id_mensaje, @p_fecha_aum, @p_rut_log,
		  SUBSTRING(@@servername,0,11), ''GS24'', @p_opcion,
		  @casilla_actual, @estado_actual, @casilla_actual, ''AUM'',
		  @p_casilla, ''A'', ''Autorización Manual de Mensaje'')

	   if @@error <> 0 or @@rowcount = 0
	   begin
		  rollback 
		  return -4
	   end

	   commit tran
	   return 0
	END'
END










IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_count_bancos2_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].proc_sw_count_bancos2_MS



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_count_bancos2_MS]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_count_bancos2_MS] 
		-- Add the parameters for the stored procedure here
		@swift varchar(13) = NULL,
		@pais varchar(35) = NULL,
		@ciudad varchar(35) = NULL,
		@banco varchar(70) = NULL,
		@direccion varchar(70) = NULL,
		@branch varchar(70) = NULL,
		@intercambioClave varchar(70) = NULL
	AS
	BEGIN

		SET NOCOUNT ON;
	--	declare	@swift varchar(13)  set @swift = ''''
	--declare	@pais varchar(35) set @pais= ''''
	--declare	@ciudad varchar(35)  set @ciudad= ''''
	--declare	@banco varchar(70)  set @banco= ''''
	--declare	@direccion varchar(70) set @direccion= ''''
	--declare	@branch varchar(70) set @branch= NULL
	--declare	@intercambioClave varchar(70)  set @intercambioClave=NULL
		SELECT [CountBancos]=count([cod_banco])		
	  FROM [dbo].[sw_bancos]
	  WHERE ((@swift IS NULL) OR (@swift = '''') OR (cod_banco LIKE @swift + ''%''))
		AND ((@pais is NULL) OR (@pais = '''') OR (pais_banco LIKE @pais + ''%''))
		AND ((@ciudad is NULL) OR (@ciudad = '''') OR (ciudad_banco LIKE @ciudad + ''%''))
		AND ((@banco is NULL) OR (@banco = '''') OR (nombre_banco LIKE @banco + ''%''))
		AND ((@direccion is NULL) OR (@direccion = '''') OR (direccion_banco LIKE @direccion + ''%''))
		AND ((@branch is NULL) OR (@branch = '''') OR (branch LIKE @branch + ''%''))
		AND ((@intercambioClave is NULL) OR (@intercambioClave = '''') OR (intercambio_clave LIKE @intercambioClave + ''%''))

	END'
END





/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_nro_imp_MS]    Script Date: 11-03-2016 17:09:25 ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_nro_imp_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_trae_nro_imp_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_imp_MS]    Script Date: 11-03-2016 17:09:25 ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_imp_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_graba_imp_MS]

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_imp_MS]    Script Date: 11-03-2016 17:09:25 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_imp_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_graba_imp_MS] AS' 
EXEC dbo.sp_executesql @statement = N'ALTER PROCEDURE [dbo].[proc_sw_rec_graba_imp_MS] @p_casilla   INT,
		@p_sesion    INT,
		@p_secuencia INT,
		@p_rut_log   INT,
		@p_estado    CHAR(3),
		@comentario  VARCHAR(80) 

	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @tot_impresos   INT,@casilla_actual INT,@estado_actual  CHAR(3),@rut_actual     INT,
	   @unidad_actual  INT,@fecha_actual   datetime,@rut_recibe     INT,
	   @unidad_recibe  INT,@fecha_recibe   datetime,@opcion_log	CHAR(8)
	begin tran

	   SELECT   @estado_actual = m.estado_msg,
		@tot_impresos = m.total_imp,
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

	   select   @tot_impresos =  @tot_impresos+1

	   update dbo.sw_mensajes set estado_msg = @p_estado,total_imp = @tot_impresos,rut_autoriza = @rut_recibe,
	   comentario = @comentario  WHERE 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'')

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  select -1 as Result
	   end

	   if exists(SELECT TOP 1 1 from dbo.sw_mensajes_add where sesion = @p_sesion and
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
		 ''A'' @comentario)

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  select -3 as Result
	   end

	   COMMIT tran

	   select   total_imp -1
	   from    dbo.sw_mensajes
	   where 	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'')

	   select 0 as Result
	END'
END







/****** Object:  StoredProcedure [dbo].[proc_sw_rec_trae_nro_imp_MS]    Script Date: 11-03-2016 17:09:26 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_trae_nro_imp_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_trae_nro_imp_MS] AS'
EXEC dbo.sp_executesql @statement = N' ALTER PROCEDURE [dbo].[proc_sw_rec_trae_nro_imp_MS] @p_sesion    INT,
		@p_secuencia INT 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:51:51 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   SELECT   total_imp as TotalImp
	   FROM 	dbo.sw_mensajes
	   WHERE	(sesion = @p_sesion) and
		(secuencia = @p_secuencia) and
		(send_recv = ''R'')
	END'
END




/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_conf_MS]    Script Date: 11-03-2016 17:09:25 ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_conf_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_graba_conf_MS]


/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_conf_MS]    Script Date: 09/05/2016 14:27:10 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_conf_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_graba_conf_MS] @p_casilla   INT,
		   @p_sesion    INT,
		   @p_secuencia INT,
		   @p_rut_log   INT,
		   @comentario  VARCHAR(80) 

	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@opcion_log CHAR(8)
	begin tran

	   SELECT   @estado_actual = estado_msg,
		   @casilla_actual = casilla
	   FROM      dbo.sw_mensajes
	   WHERE     sesion = @p_sesion and
	   secuencia = @p_secuencia and
	   send_recv = ''R''

	   update dbo.sw_mensajes set estado_msg = ''CNF'',rut_autoriza = @p_rut_log,comentario = @comentario  WHERE     (sesion = @p_sesion) and
		   (secuencia = @p_secuencia) and
		   (send_recv = ''R'')

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -1
	   end

	   if exists(SELECT TOP 1 1 from dbo.sw_mensajes_add where sesion = @p_sesion and
	   secuencia = @p_secuencia and send_recv = ''R'')
	   begin
		  update dbo.sw_mensajes_add set fecha_recibe = GetDate(),unidad_recibe = @p_casilla  WHERE   (sesion = @p_sesion) and
				 (secuencia = @p_secuencia) and
				 (send_recv = ''R'')
	   end
	else
	   begin
		  INSERT INTO dbo.sw_mensajes_add(sesion, secuencia, send_recv, casilla, fecha_recibe,
			 unidad_recibe, veces_rechazo, veces_reenvio)
		   VALUES(@p_sesion, @p_secuencia, ''R'', @casilla_actual, GetDate(),
			 @p_casilla, 0, 0)
	   end
	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -2
	   end

	   select   @opcion_log = lower(@estado_actual)+''grcnf''

	   INSERT INTO dbo.sw_log_msg
	VALUES(@p_sesion, @p_secuencia, ''R'', GetDate(),
		   @p_rut_log, NULL, ''GS24'', @opcion_log,
		   @casilla_actual, @estado_actual,
		   @casilla_actual, ''CNF'',
		   @p_casilla, NULL, ''A'', @comentario)

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -3
	   end

	   COMMIT tran
	   return 0
	END'
END

/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_rech_MS]    Script Date: 11-03-2016 17:09:25 ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_rech_MS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_sw_rec_graba_rech_MS]


/****** Object:  StoredProcedure [dbo].[proc_sw_rec_graba_rech_MS]    Script Date: 09/05/2016 14:27:10 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_rec_graba_rech_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_rec_graba_rech_MS] @p_casilla    INT,
		   @p_sesion     INT,
		   @p_secuencia  INT,
		   @p_rut_log    INT,
		   @p_estado     CHAR(3),
		   @p_texto      VARCHAR(90) 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:50:50 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual CHAR(3),@opcion_log     CHAR(8)
	begin tran


	   SELECT   @estado_actual = estado_msg,
		   @casilla_actual = casilla
	   FROM    dbo.sw_mensajes
	  WHERE     sesion = @p_sesion and
	   secuencia = @p_secuencia and
	   send_recv = ''R''

	   update dbo.sw_mensajes set casilla = 748,unidad = 748,estado_msg = @p_estado,comentario = ''Mensaje Rechazado por Usuario''  WHERE    (sesion = @p_sesion) and
		   (secuencia = @p_secuencia) and
			(send_recv = ''R'') 
	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -1
	   end

	   if exists(SELECT TOP 1 1 from dbo.sw_mensajes_add where sesion = @p_sesion and
	   secuencia = @p_secuencia and send_recv = ''R'')
	   begin
		  update dbo.sw_mensajes_add set casilla = 748,unidad_rechazo = @p_casilla,rut_rechazo = @p_rut_log,fecha_rechazo = GetDate(),
		  texto_rechazo = @p_texto,veces_rechazo = veces_rechazo+1  WHERE        (sesion = @p_sesion) and
				 (secuencia = @p_secuencia) and
				   (send_recv = ''R'')
	   end
	else
	   begin
		  INSERT INTO dbo.sw_mensajes_add(sesion, secuencia, send_recv, casilla,
			 unidad_rechazo, rut_rechazo, fecha_rechazo,
			 texto_rechazo, veces_rechazo, veces_reenvio)
		   VALUES(@p_sesion, @p_secuencia, ''R'', 748,
			 @p_casilla, @p_rut_log, GetDate(),
			 @p_texto, 1, 0)
	   end

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -2
	   end

	   select   @opcion_log = lower(@estado_actual)+''grrch''

	   INSERT INTO dbo.sw_log_msg
	VALUES(@p_sesion, @p_secuencia, ''R'', GetDate(),
			 @p_rut_log, NULL, ''GS24'', @opcion_log,
			 @casilla_actual, @estado_actual,
			 748, @p_estado, @p_casilla, NULL, ''A'',
			 ''Mensaje Rechazado por Usuario'')

	   if (@@error <> 0) or (@@rowcount = 0)
	   begin
		  ROLLBACK 
		  return -3
	   end

	   COMMIT tran
	   return 0
	END'
END

----------------------------------------------------------------------------------------------------------------------------
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_nul_MS]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[proc_sw_env_graba_nul_MS]


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_graba_nul_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_graba_nul_MS] 
		@p_casilla 	INT, 
		@p_id_mensaje 	INT, 
		@p_rut_log 	INT, 
		@p_comentario 	CHAR(80) 
	AS
	BEGIN
	-- This procedure was converted on Thu Apr 17 16:45:45 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   DECLARE @casilla_actual INT,@estado_actual  CHAR(3),@fecha_actual   datetime,@opcion_log 	CHAR(8)

	   ------------------------------------------------------------------------------------------------------------------
		-- SI EXISTEN FIRMAS AUTORIZADAS, NO ES POSIBLE ANULAR EL SWIFT
		IF exists(select estado_firma from dbo.sw_msgsend_firma where id_mensaje = @p_id_mensaje and estado_firma = ''F'')
		begin
			RETURN -4
		end
		------------------------------------------------------------------------------------------------------------------
	begin tran

	   SELECT   @estado_actual = estado_msg,
		@casilla_actual = casilla
	   FROM 	dbo.sw_msgsend
	   WHERE 	id_mensaje = @p_id_mensaje

	   if @estado_actual = ''PRO'' or @estado_actual = ''ENV''
	   begin
		  select   @estado_actual
	   end
	else
	   begin	 
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
		  SELECT   ''NUL''
	   end

	   COMMIT tran

	   return 0
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sw_version_codigo_MS' AND schema_id = SCHEMA_ID('dbo'))
	DROP PROCEDURE [dbo].proc_sw_version_codigo_MS


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_version_codigo_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[proc_sw_version_codigo_MS] 
AS
begin
/************************************************************************/
	/*replace-begin*/select ''1.27.0''/*replace-end*/
end'
END

--------------------------------------------------------------------------------------------------------------------
fin_script: 
	print 'fin script Script_Swift_codigo.sql'