
GOTO fin_script


SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;

USE [cext01];

/****** Object:  StoredProcedure [dbo].[sgt_vmd_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_vmd_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_vmd_s02_MS]
END 

/****** Object:  StoredProcedure [dbo].[sgt_vmc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_vmc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_vmc_s01_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_suc_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_suc_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_suc_s04_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_suc_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_suc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_suc_s02_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_suc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_suc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_suc_s01_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_pbc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_pbc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_pbc_s01_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_pai_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_pai_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_pai_s02_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_mnd_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_mnd_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_mnd_s02_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_mnd_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_mnd_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_mnd_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_loc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_loc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_loc_s01_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_ejc_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_ejc_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_ejc_s04_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_ejc_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_ejc_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_ejc_s03_MS]
END

/****** Object:  StoredProcedure [dbo].[sgt_ejc_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_ejc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_ejc_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sgt_clf_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_clf_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_clf_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sgt_aec_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_aec_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_aec_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sgt_aec_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_aec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sgt_aec_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[scejdoc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scejdoc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[scejdoc_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[scedev_jcci_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scedev_jcci_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[scedev_jcci_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xret_s05_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xret_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xret_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xprt_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xprt_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xprt_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xprt_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xprt_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xprt_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_w03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_w03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_w03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_w02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_w02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_w02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_u03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_u03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_s11_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_s11_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_s11_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_s10_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_s10_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_s10_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_s08_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_s08_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xplv_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xplv_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xdoc_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xdoc_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xdoc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xdoc_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xdoc_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xdoc_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xdec_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdec_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xdec_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xdec_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xdec_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xcob_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xcob_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xcob_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xanu_u04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_u04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xanu_u04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xanu_u03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xanu_u03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xanu_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xanu_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xanu_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xanu_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xanu_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xanu_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_xanu_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_xanu_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_vvi_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vvi_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_vvi_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_vvi_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vvi_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_vvi_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_vrng_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vrng_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_vrng_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_vex_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vex_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_vex_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_u03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_u03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s35_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s35_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s35_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s34_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s34_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s34_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s25_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s25_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s25_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s16_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s16_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s16_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s12_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s12_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s12_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s10_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s10_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s10_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s09_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s09_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s06_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s05_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_usr_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_usr_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_trng_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_trng_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_trng_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tint_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tint_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tint_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tint_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tint_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tint_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tint_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tint_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tint_d02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_d02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tint_d02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tint_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tint_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_d02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_d02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_d02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tgas_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tgas_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tdme_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tdme_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tdme_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcp_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcp_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcp_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_s04]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_s04]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_s04]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_d02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_d02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_d02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_tcom_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_tcom_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_swf_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_swf_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_swf_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_swf_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_swf_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_swf_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_swf_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_swf_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_swf_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_swf_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_serv_imp_01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_serv_imp_01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_serv_imp_01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_sec_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_sec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_sec_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_s08_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_s08_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_s07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_s06_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_s05_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_parti_listRazon_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_parti_listRazon_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_parti_listRazon_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_parti_listDir_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_parti_listDir_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_parti_listDir_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rsa_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rsa_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rng_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rng_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rng_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_rjte_i05_2]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rjte_i05_2]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rjte_i05_2]
END
/****** Object:  StoredProcedure [dbo].[sce_refe_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_refe_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_refe_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ref_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ref_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ref_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ras_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ras_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ras_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prty_w01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prty_w01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prty_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prty_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prty_s09_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prty_s09_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prty_s08_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prty_s08_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prty_s07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prty_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prty_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prty_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prd_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prd_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prd_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_prd_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prd_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_prd_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ppae_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ppae_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ppae_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plrm_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plrm_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plrm_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plrm_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plrm_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plrm_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plia_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plia_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plia_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_w06_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_w06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_w06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_u04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_u04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_u04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_s07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_s06_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_s05_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pli_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pli_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pldc_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pldc_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pldc_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pldc_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pldc_s02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pldc_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pldc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pldc_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pldc_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pldc_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_u14_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_u14_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_u14_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_u12_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_u12_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_u12_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_u07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_u07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_u07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s18_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s18_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s18_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s17_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s17_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s17_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s16_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s16_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s16_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s05_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_plan_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_plan_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_pcol_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pcol_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_pcol_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_obc_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_obc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_obc_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_nom_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_nom_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_nom_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_netd_ejc_clt_w01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_netd_ejc_clt_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_netd_ejc_clt_w01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mts_u01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mts_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mts_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mts_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mts_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mts_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta3_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta3_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta3_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta3_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta3_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta3_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta2_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta2_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta2_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta1_s09_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta1_s09_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta1_s07_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta1_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta1_s06_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta1_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta1_s05_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta1_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta1_s04_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta1_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta1_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta1_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mta0_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta0_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mta0_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_memg_s03_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_memg_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_memg_s01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_memg_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_memg_i01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_memg_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_memg_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_memg_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mem_d01_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mem_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mem_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mch_u02_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mch_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mch_s11_MS]    Script Date: 22-10-2015 10:33:30 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s11_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mch_s11_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mch_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mch_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mch_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mch_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mch_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mch_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_u70_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_u70_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_u70_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s66_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s66_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s66_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s65_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s65_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s65_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s56_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s56_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s56_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s20_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s20_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s20_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s15_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s15_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s15_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s14_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s14_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s14_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_mcd_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_mcd_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_jprt_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_jprt_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_jprt_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_intr_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_intr_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_intr_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_inpl_w01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_inpl_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_inpl_w01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_inpl_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_inpl_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_inpl_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_inpl_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_inpl_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_inpl_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ini_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ini_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ini_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_impflag_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_impflag_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_impflag_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_grio_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grio_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_grio_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_grio_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grio_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_grio_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_grdo_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grdo_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_grdo_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_grdo_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grdo_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_grdo_d01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gpln_s16_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s16_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gpln_s16_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gpln_s13_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s13_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gpln_s13_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gpln_s12_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s12_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gpln_s12_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gpln_s11_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s11_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gpln_s11_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gpln_s10_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s10_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gpln_s10_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gcar_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gcar_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gcar_u03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gcar_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gcar_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gcar_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_gcar_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gcar_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_gcar_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_fra_s06_MS_alt]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_fra_s06_MS_alt]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_fra_s06_MS_alt]
END
/****** Object:  StoredProcedure [dbo].[sce_fra_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_fra_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_fra_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_fer_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_fer_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_fer_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_doc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_doc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_doc_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_u03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_s09_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_s09_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_s08_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_dad_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_dad_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cvd1_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd1_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cvd1_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cvd1_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd1_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cvd1_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cvd_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cvd_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cvd_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cvd_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cvd_p02]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_p02]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cvd_p02]
END
/****** Object:  StoredProcedure [dbo].[sce_ctas_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ctas_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ctas_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ctas_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ctas_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ctas_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ctas_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ctas_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cta_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cta_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cta_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cta_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cta_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cta_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cta_s01_1_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s01_1_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cta_s01_1_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cpai_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cpai_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cpai_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cov_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cov_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cov_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cov_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cov_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cov_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cou_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cou_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cou_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_cor_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cor_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_cor_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_chq_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_chq_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_chq_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_chq_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_chq_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_chq_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_chq_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_chq_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_chq_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ccpl_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ccpl_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ccpl_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ccof_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ccof_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ccof_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_ccde_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ccde_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_ccde_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_blin_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_blin_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_blin_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_blin_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_blin_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_blin_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_blin_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_blin_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_blin_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_bic_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bic_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_bic_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_bic_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bic_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_bic_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_bcta_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bcta_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_bcta_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_bcta_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bcta_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_bcta_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_bcta_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bcta_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_bcta_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_bco_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bco_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_bco_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_aut_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_aut_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_aut_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_arb_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_arb_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_arb_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_anu_u12_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_anu_u12_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_anu_u12_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_anu_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_anu_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_anu_u03_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_adn_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_adn_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_adn_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_acr_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_acr_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_acr_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_acon_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_acon_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_acon_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[sce_abr_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_abr_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_abr_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[rce_memg_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rce_memg_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[rce_memg_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[proc_sw_env_trae_env_rango]
END
/****** Object:  StoredProcedure [dbo].[proc_rh_swi_001]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_rh_swi_001]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[proc_rh_swi_001]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_xdec_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_xdec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_xdec_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_trxcor_ft_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_trxcor_ft_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_trxcor_ft_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_sup_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_sup_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_sup_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_rev_abocar_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_rev_abocar_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_rev_abocar_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_rev_abocar_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_rev_abocar_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_relacion_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_relacion_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_relacion_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s08_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s07_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s06_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s05_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s04_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_prty_i06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_i06_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_prty_i06_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_parametros_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_parametros_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_parametros_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_ovd_ft_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_ovd_ft_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_ovd_ft_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_inpl_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_inpl_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_inpl_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_fts_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_fts_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_fts_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_datusr_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_datusr_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_datusr_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_cvd_ft_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cvd_ft_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_cvd_ft_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_cvd_ft_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cvd_ft_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_cvd_ft_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_cvd_ft_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cvd_ft_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_cvd_ft_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_cta_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cta_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_cta_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_codtran_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_codtran_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_codtran_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_codtran_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_codtran_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_codtran_i01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_aprty_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_aprty_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_aprty_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_abo_car_u02_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_abo_car_u01_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_abo_car_s03_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_abo_car_s02_MS]
END
/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[pro_sce_abo_car_s01_MS]
END
/****** Object:  StoredProcedure [dbo].[LstRiesgo_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LstRiesgo_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[LstRiesgo_MS]
END
/****** Object:  StoredProcedure [dbo].[LstEjec_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LstEjec_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[LstEjec_MS]
END
/****** Object:  StoredProcedure [dbo].[LstAcEco_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LstAcEco_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[LstAcEco_MS]
END
/****** Object:  StoredProcedure [dbo].[LstAcEco_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LstAcEco_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[LstAcEco_MS]

AS
BEGIN
/*	
Historial:
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	select [aec_aeccod], [aec_aecnom] from sgt_aec
END

' 
END

/****** Object:  StoredProcedure [dbo].[LstEjec_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LstEjec_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[LstEjec_MS]
	
AS
BEGIN
/*	
Historial:
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	select ejc_ejcofi, ejc_ejccod, ejc_ejcrut, ejc_ejcnom, ejc_ejctpo 
	from sgt_ejc
	where 
	ejc_ejctpo = ''3'' OR
	ejc_ejctpo = ''4'' OR
	ejc_ejctpo = ''5''
END

' 
END

/****** Object:  StoredProcedure [dbo].[LstRiesgo_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LstRiesgo_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[LstRiesgo_MS]  

AS
BEGIN
/*	
Historial:
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	select [clf_clfcod], [clf_clfdes] from sgt_clf
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_abo_car_s01_MS] 
	@cencos CHAR(3),
	@codusr CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @fecpro CHAR(10)

   Create Table #tmp_abocar
   (
      numcct         CHAR(15),
      nemcta         CHAR(15),
      fecmov         Datetime,
      nroimp         NUMERIC(3,0),
      nomcli         VARCHAR(30),
      cod_dh         CHAR(1),
      moneda         CHAR(3),
      codmon         NUMERIC(3,0),
      mtomcd         NUMERIC(15,2),
      operacion      CHAR(15),
      nrorpt         NUMERIC(8,0),
      enlinea        NUMERIC(1,0),
      estado         NUMERIC(2,0),
      codcct         CHAR(3),
      codpro         CHAR(2),
      codesp         CHAR(2),
      codofi         CHAR(3),
      codope         CHAR(5),
      codfun         NUMERIC(3,0),
      trx_id         CHAR(27),
      cod_prod       CHAR(4),
      cod_trx_fc     CHAR(3),
      cod_ext        CHAR(5),
      cod_swf        CHAR(3),
      data1          CHAR(35),
      data2          CHAR(35),
      data3          CHAR(35),
      data4          CHAR(35),
      data5          CHAR(35),
      cod_trx_cosmos CHAR(3),
      tip_cta        CHAR(4)      
   )

   select   @fecpro = convert(CHAR(10),GetDate(),101)

   Insert Into #tmp_abocar
   Select  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(convert(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0),
            d.transaction_id,
            f.cod_prod,
            f.cod_trx_fc,
            f.cod_ext,
            f.cod_swf,
            c.data1,
            c.data2,
            c.data3,
            c.data4,
            c.data5,
            f.cod_trx_cosmos,
            ''CITI''
   from  dbo.sce_mcd a,
          dbo.sce_mch b,
          dbo.tbl_sce_tranope_ft c,
          dbo.tbl_sce_relacion_ft d,
          dbo.tbl_sce_parametros_ft e,
          dbo.tbl_sce_codtran_ft f,
          dbo.sgt_mnd g
   where a.codpro  = ''30''
   and   a.enlinea = 0
   and   a.cencos  = @cencos
   and   a.codusr  = @codusr
   and   a.fecmov  = @fecpro
   and	  a.nemcta  = e.tipo_ft
   and   a.codcct  = b.codcct
   and   a.codpro  = b.codpro
   and   a.codesp  = b.codesp
   and   a.codofi  = b.codofi
   and   a.codope  = b.codope
   and   a.nrorpt  = b.nrorpt
   and   a.fecmov  = b.fecmov
   and   a.codcct  = c.codcct
   and   a.codpro  = c.codpro
   and   a.codesp  = c.codesp
   and   a.codofi  = c.codofi
   and   a.codope  = c.codope
   and   a.nrorpt  = c.nrorpt
   and   a.fecmov  = c.fecmov
   and   a.cod_dh  = c.cod_dh
   and   a.codcct  = d.codcct
   and   a.codpro  = d.codpro
   and   a.codesp  = d.codesp
   and   a.codofi  = d.codofi
   and   a.codope  = d.codope
   and   a.mtomcd  = d.monto
   and   a.codmon  = d.moneda
   and   a.nroimp  = d.nroimp
   and   c.nro_trx = f.nro_trx
   and   a.codmon  = g.mnd_mndcod
   and   c.nroimp  = d.nroimp
   and   b.estado <> 9

   Insert Into #tmp_abocar
   select  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            isnull(convert(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            a.nemmon,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            isnull(b.codfun,0),
            c.transaction_id,
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            ''BCH''
   from  dbo.sce_mcd a LEFT OUTER JOIN dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov and b.estado  <> 9, 
dbo.tbl_sce_relacion_ft c
   where a.codpro  = ''30'' AND a.enlinea = 0 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  @fecpro AND a.nemcta  in(''CC$'',''CCE'') AND a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope AND a.codmon  =  c.moneda AND a.mtomcd  =  c.monto
    --and (enlinea = 0 or (estado = 9 and enlinea = 1))
    --     Trae todos los mov. a cursar y a reversar
    --and ( enlinea = 0 or (estado = 1 and enlinea = 1))

   select   numcct     ,
           nemcta     ,
           fecmov     ,
           nroimp     ,
           nomcli     ,
           cod_dh     ,
           moneda     ,
           codmon     ,
           mtomcd     ,
           operacion  ,
           nrorpt     ,
           enlinea    ,
           estado     ,
           codcct     ,
           codpro     ,
           codesp     ,
           codofi     ,
           codope     ,
           codfun     ,
           trx_id     ,
           cod_prod   ,
           cod_trx_fc ,
           cod_ext    ,
           cod_swf    ,
           data1      ,
           data2      ,
           data3      ,
           data4      ,
           data5      ,
           cod_trx_cosmos,
           tip_cta
   from  #tmp_abocar
   order by nrorpt,nroimp

   Drop Table #tmp_abocar
    
End

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_abo_car_s02_MS] 
			@codcct CHAR(3)  ,
            @codpro CHAR(2)  ,
            @codesp CHAR(2)  ,
            @codofi CHAR(3)  ,
            @codope CHAR(5)  ,
            @nroimp NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
      select   a.record_type,	a.branch_number,
            a.contract_reference,
            a.ordering_customer,
            a.act_hist_indicator,
            a.input_date1,
            a.receiver,
            a.credit_entry_date,
            a.order_cost_account,
            a.receiver_account,
            a.authorization_stat,
            a.transac_type_code,
            a.exe_type_code_tran,
            a.product_type,
            a.swf_currency_code,
            a.currency_code,
            a.charges_debit,
            a.transfer_amount,
            a.sign_transfer,
            a.legal_vehicle_code,
            a.debit_value_date,
            a.data_entry_date,
            a.credit_value_date,
            a.texto,
            a.status,
            a.by_order_of,
            a.beneficiary,
            a.last_inp_date,
            a.transfer_charged,
            a.operator_id,
            a.input_date2,
            a.input_time,
            a.authorizer_id,
            a.authorizer_time,
            a.order_cust_account,
            a.input_date3,
            a.alpha_number,
            a.swf_currency_equi,
            a.currency_code_equi,
            a.equivalent_amount,
            a.signo_equivalent,
            a.fcy_exchange_rate,
            a.receiver_account2,
            a.input_date4,
            a.short_benefic_bank,
            a.alpha_reference,
            a.lto_indicator,
            a.benefi_account_num,
            a.commission_rate,
            a.commission_amount,
            a.sing_commssion,
            a.courtage_rate,
            a.courtage_amount,
            a.sign_courtage,
            a.postage_amount,
            a.sign_postage,
            a.swf_currency_charg,
            a.currency_code_chan,
            a.chrg_base_nbr,
            a.short_charges_acou,
            a.reference_number,
            a.central_bank_code,
            a.num_order_customer,
            a.num_receiver,
            a.num_beneficia_bank,
            a.num_beneficiary,
            a.num_reason,
            a.num_bank_to_bank,
            a.num_charges,
            a.total_number,
            a.text_line,
            a.text_line2,
            a.text_line3
   from  dbo.tbl_sce_fts a,
      dbo.tbl_sce_relacion_ft b
   where a.contract_reference = b.contract_reference
   and   a.transfer_amount = b.monto
   and   a.currency_code = b.moneda
   and   b.codcct = @codcct
   and   b.codpro = @codpro
   and   b.codesp = @codesp
   and   b.codofi = @codofi
   and   b.codope = @codope
   and   b.nroimp = @nroimp
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_abo_car_s03_MS] 
@cencos CHAR(3),
@codusr CHAR(2) 
AS
Begin
/*	
Historial:
      2014-04-16 -       Migración desde Sybase (AKZIO)
      2015-09-02   MBP   Proyecto migración Comex.Net (Microsoft) - Se agregan joins para traer cod_prod, cod_trx_fc, y cod_ext
	  en el caso de cuentas BCH (quedaban vacias).
*/


   declare @fecpro CHAR(10)

   Create Table #tmp_abocar
   (
      numcct         CHAR(15),
      nemcta         CHAR(15),
      fecmov         Datetime,
      nroimp         NUMERIC(3,0),
      nomcli         VARCHAR(30),
      cod_dh         CHAR(1),
      moneda         CHAR(3),
      codmon         NUMERIC(3,0),
      mtomcd         NUMERIC(15,2),
      operacion      CHAR(15),
      nrorpt         NUMERIC(8,0),
      enlinea        NUMERIC(1,0),
      estado         NUMERIC(2,0),
      codcct         CHAR(3),
      codpro         CHAR(2),
      codesp         CHAR(2),
      codofi         CHAR(3),
      codope         CHAR(5),
      codfun         NUMERIC(3,0),
      trx_id         CHAR(27),
      cod_prod       CHAR(4),
      cod_trx_fc     CHAR(3),
      cod_ext        CHAR(5),
      cod_swf        CHAR(3),
      data1          CHAR(35),
      data2          CHAR(35),
      data3          CHAR(35),
      data4          CHAR(35),
      data5          CHAR(35),
      cod_trx_cosmos CHAR(3),
      tip_cta        CHAR(4)      
   )

   select   @fecpro = convert(CHAR(10),GetDate(),101)

   Insert Into #tmp_abocar
   Select  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(convert(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0),
            d.transaction_id,
            f.cod_prod,
            f.cod_trx_fc,
            f.cod_ext,
            f.cod_swf,
            c.data1,
            c.data2,
            c.data3,
            c.data4,
            c.data5,
            f.cod_trx_cosmos,
            ''CITI''
   from  dbo.sce_mcd a,
          dbo.sce_mch b,
          dbo.tbl_sce_tranope_ft c,
          dbo.tbl_sce_relacion_ft d,
          dbo.tbl_sce_parametros_ft e,
          dbo.tbl_sce_codtran_ft f,
          dbo.sgt_mnd g
   where /*a.codpro  = ''30''
   and*/   a.enlinea = 0
   and   a.cencos  = @cencos
   and   a.codusr  = @codusr
   and   a.fecmov  = @fecpro
   and	  a.nemcta  = e.tipo_ft
   and   a.codcct  = b.codcct
   and   a.codpro  = b.codpro
   and   a.codesp  = b.codesp
   and   a.codofi  = b.codofi
   and   a.codope  = b.codope
   and   a.nrorpt  = b.nrorpt
   and   a.fecmov  = b.fecmov
   and   a.codcct  = c.codcct
   and   a.codpro  = c.codpro
   and   a.codesp  = c.codesp
   and   a.codofi  = c.codofi
   and   a.codope  = c.codope
   and   a.nrorpt  = c.nrorpt
   and   a.fecmov  = c.fecmov
   and   a.cod_dh  = c.cod_dh
   and   a.codcct  = d.codcct
   and   a.codpro  = d.codpro
   and   a.codesp  = d.codesp
   and   a.codofi  = d.codofi
   and   a.codope  = d.codope
   and   a.nroimp  = d.nroimp
   and   c.nro_trx = f.nro_trx
   and   a.codmon  = g.mnd_mndcod
   and   c.nroimp  = d.nroimp
   and   b.estado <> 9

   Insert Into #tmp_abocar
   SELECT a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            isnull(convert(VARCHAR,b.nomcli),'''') as nomcli,
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope as numope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            isnull(b.codfun,0) as codfun,
			c.transaction_id,
            sc.cod_producto_fc,
            sc.cod_transaccion_fc,
            sc.cod_extendido_fc,
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            ''BCH''
FROM  dbo.sce_mcd a 
   LEFT OUTER JOIN 
   dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov and b.estado  <> 9
   LEFT OUTER JOIN
    dbo.tbl_sce_relacion_ft c ON a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope  and a.nroimp =  c.nroimp,
	dbo.sce_ccce e,
	dbo.sce_cctx tx,
	dbo.tbl_sce_sup_codigos sc,
	dbo.sgt_mnd g
   WHERE a.enlinea = 0 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  @fecpro AND a.nemcta  in(''CC$'',''CCE'')
AND  a.codmon  = g.mnd_mndcod
AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')
AND tx.nemtra = 
			(CASE 
				WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''ABONOMN''
				WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''CARGOMN''
				WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''ABONOME''
				WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''CARGOME'' 
				ELSE ''''
			END) 
AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE 
				WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''77''
				WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''28''
				WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''''
				WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN '''' 
				ELSE ''''
			END)
			
   select   numcct     ,
           nemcta     ,
           fecmov     ,
           nroimp     ,
           nomcli     ,
           cod_dh     ,
           moneda     ,
           codmon     ,
           mtomcd     ,
           operacion  ,
           nrorpt     ,
           enlinea    ,
           estado     ,
           codcct     ,
           codpro     ,
           codesp     ,
           codofi     ,
           codope     ,
           codfun     ,
           trx_id     ,
           cod_prod   ,
           cod_trx_fc ,
           cod_ext    ,
           cod_swf    ,
           data1      ,
           data2      ,
           data3      ,
           data4      ,
           data5      ,
           cod_trx_cosmos,
           tip_cta
   from  #tmp_abocar
   order by nrorpt,nroimp

   Drop Table #tmp_abocar
    
End




' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_abo_car_u01_MS] 
	@gb_codcct      CHAR(3),
    @gb_codpro      CHAR(2),
    @gb_codesp      CHAR(2),
    @gb_codofi      CHAR(3),
    @gb_codope      CHAR(5),
    @gb_nrorpt      NUMERIC(8,0),
    @gb_fecmov      datetime,
    @gb_nroimp      NUMERIC(3,0),
    @gb_resp_tran   CHAR(20),
	@rutais			varchar(8),
    @ls_codigo      CHAR(3)    OUTPUT,
    @ls_mensaje     CHAR(250)  OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @lc_cod_ft CHAR(10),@lc_rowcount INT
BEGIN TRAN
   update dbo.sce_mcd set enlinea = 1, rutais = @rutais from    dbo.sce_mcd a,
                dbo.tbl_sce_parametros_ft b where   a.codcct  = @gb_codcct
   and     a.codpro  = @gb_codpro
   and     a.codesp  = @gb_codesp
   and     a.codofi  = @gb_codofi
   and     a.codope  = @gb_codope
   and     a.nrorpt  = @gb_nrorpt
   and     a.fecmov  = @gb_fecmov
   and     a.nroimp  = @gb_nroimp
   and     b.desc_ft = @gb_resp_tran
   select   @lc_rowcount = @@rowcount
                                     
   if (@lc_rowcount = 0 and @@error <> 0)
   begin
      ROLLBACK 
      Select   @ls_codigo = ''E01''
      Select   @ls_mensaje = ''pro_sce_abo_car_u01:[E01]. Problemas al Actualizar datos en la tabla sce_mcd.''
      Return
   end
else if (@lc_rowcount = 0 and @@error = 0)
   begin
      select   @lc_cod_ft = cod_ft
      from    dbo.tbl_sce_parametros_ft
      where   desc_ft =  @gb_resp_tran
      If @lc_cod_ft IS NULL
      begin
         ROLLBACK 
         Select   @ls_codigo = ''E02''
         Select   @ls_mensaje = ''pro_sce_abo_car_u01:[E02]. Problemas en el Servicio.''+@gb_resp_tran
         Return
      end
   else
      begin
         ROLLBACK 
         Select   @ls_codigo = ''E03''
         Select   @ls_mensaje = ''pro_sce_abo_car_u01:[E03]. Problemas al actualizar los datos en la tabla.''
         Return
      end
   end

   update dbo.tbl_sce_cvd_ft set valida_iny = 1  where   codcct  = @gb_codcct
   and     codpro  = @gb_codpro
   and     codesp  = @gb_codesp
   and     codofi  = @gb_codofi
   and     codope  = @gb_codope
   if (@@rowcount = 0 and @@error <> 0)
   begin
      ROLLBACK 
      Select   @ls_codigo = ''E04''
      Select   @ls_mensaje = ''pro_sce_abo_car_u01:[E04]. Problemas al Actualizar datos en la tabla tbl_sce_cvd_ft.''
      Return
   end                       
           
   COMMIT TRAN

   if (@lc_rowcount <> 0 and @@error = 0)
   begin
      Select   @ls_codigo = ''E00''
      Select   @ls_mensaje = ''pro_sce_abo_car_u01:[E01]. Se realizo exitosamente la actualizacion de la tabla sce_mcd''
      Return
   end               
          --  select ''@lc_rowcount'', @lc_rowcount
          --  select ''@@error'', @@error
End

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_abo_car_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_abo_car_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_abo_car_u02_MS] 
	@gb_codcct      VARCHAR(3),
    @gb_codpro      VARCHAR(2),
    @gb_codesp      VARCHAR(2),
    @gb_codofi      VARCHAR(3),
    @gb_codope      VARCHAR(5),
    @gb_trxid       VARCHAR(27),
    @ls_codigo      VARCHAR(3)    OUTPUT,
    @ls_mensaje     VARCHAR(250)  OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   declare @ln_new_trxid NUMERIC(2,0),@ln_trxid_rev NUMERIC(2,0),@lc_new_trxid VARCHAR(27)
   select   @ln_new_trxid = isnull(convert(NUMERIC(2,0),max(SUBSTRING(transaction_id,26,2))),0)
   from   dbo.tbl_sce_relacion_ft
   where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope
   and    len(SUBSTRING(transaction_id,26,2)) > 0  

   select   @ln_trxid_rev = isnull(convert(NUMERIC(2,0),max(SUBSTRING(transaction_id_rev,26,2))),0)
   from   dbo.tbl_sce_relacion_ft
   where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope
   and    len(SUBSTRING(transaction_id_rev,26,2)) > 0  

   select   @lc_new_trxid = SUBSTRING(transaction_id,1,25)
   from   dbo.tbl_sce_relacion_ft
   where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope
   

   If @ln_new_trxid > @ln_trxid_rev
      select   @ln_new_trxid = @ln_new_trxid+1
Else
   select   @ln_new_trxid = @ln_trxid_rev+1 

   If @ln_new_trxid <= 9
      select   @lc_new_trxid = @lc_new_trxid+''0''+convert(VARCHAR(2),@ln_new_trxid)
Else
   select   @lc_new_trxid = @lc_new_trxid+convert(VARCHAR(2),@ln_new_trxid)

BEGIN TRAN

   update dbo.tbl_sce_relacion_ft set transaction_id = @lc_new_trxid  where  codcct         = @gb_codcct
   and    codpro         = @gb_codpro
   and    codesp         = @gb_codesp
   and    codofi         = @gb_codofi
   and    codope         = @gb_codope
   and    transaction_id = @gb_trxid
   If (@@rowcount = 0)
   Begin
      ROLLBACK 
      Select   @ls_codigo = ''E01''
      Select   @ls_mensaje = ''pro_sce_abo_car_u02. Problemas al actualizar registros en tabla tbl_sce_relacion_ft.''
      Return
   End

   COMMIT TRAN
   Select   @ls_codigo = ''E00''
   Select   @ls_mensaje = ''pro_sce_abo_car_u02. Actualizacion de registro Exitoso en tabla tbl_sce_relacion_ft.''
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_aprty_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_aprty_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sce_aprty_s01_MS] @gb_ctaparty CHAR(15), @gb_flag INT 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:25:25 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   declare @lc_existe   INT
   if @gb_flag = 1
      Select   @lc_existe = count(1)
      From    dbo.sce_mcd
      Where   numcct = @gb_ctaparty
else
   select   @lc_existe  = count(1)
   from    dbo.sce_ctas    a,
            dbo.sce_mcd     b
   where   a.id_party  = @gb_ctaparty
   and     a.cuenta    = b.numcct

   select @lc_existe as Result
END



' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_codtran_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_codtran_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_codtran_i01_MS] 
	@codcct     CHAR(3),
	@codpro     CHAR(2),
	@codesp     CHAR(2),
	@codofi     CHAR(3),
	@codope     CHAR(5),
	@nrorpt     NUMERIC(8,0),
	@fecmov     datetime,
	@nro_trx    NUMERIC(2,0),
	@cod_dh     CHAR(1),
	@data1      CHAR(35),
	@data2      CHAR(35),
	@data3      CHAR(35),
	@data4      CHAR(35),
	@data5      CHAR(35),
	@nroimp     NUMERIC(3,0),
	@lc_retorno CHAR(3)   OUTPUT,
	@lc_mensaje CHAR(250) OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
  SET NOCOUNT ON 

  select   @lc_retorno = ''E00'',
       @lc_mensaje = ''Inserción Exitosa en tabla tbl_sce_tranope_ft''

Begin Tran

   Insert into dbo.tbl_sce_tranope_ft
    Values(@codcct, @codpro, @codesp, @codofi, @codope, @nrorpt, @fecmov, @nro_trx, @cod_dh, @data1, @data2, @data3, @data4, @data5, @nroimp)

    
   If @@error <> 0
   Begin
		rollback tran
      select   @lc_retorno = ''E01'',
                   @lc_mensaje = ''Problemas al insertar en tabla tbl_sce_tranope_ft''
   End

   Commit Tran
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_codtran_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_codtran_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_codtran_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   Select   nro_trx,
           glosa_cosmos,
           cr_dr,
           moneda,
           ''CTA-CTE'' tip_cta,
           cod_trx_cosmos
   From   dbo.tbl_sce_codtran_ft
   Where  cod_sistema =  ''SCE''
   and    moneda      <> ''MM''
   Union
   Select   nro_trx,
           glosa_cosmos,
           cr_dr,
           moneda,
           ''GAP'' tip_cta,
           cod_trx_cosmos
   From   dbo.tbl_sce_codtran_ft
   Where  cod_sistema = ''SCE''
   and    moneda      = ''MM''

END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_cta_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cta_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_cta_s01_MS] 
	@nemonico          VARCHAR(15) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   cta_nem
   from dbo.sce_cta
   where cta_cvt   = 1
   and SUBSTRING(convert(CHAR(6),cta_nroto),1,1) = ''1''
   and cta_nem     = @nemonico
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_cvd_ft_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cvd_ft_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_cvd_ft_i01_MS] 
@codcct     CHAR(3),
@codpro     CHAR(2),
@codesp     CHAR(2),
@codofi     CHAR(3),
@codope     CHAR(5),
@tipcvd     CHAR(3),
@Valida_Iny BIT,
@lc_retorno CHAR(3)   OUTPUT,
@lc_mensaje CHAR(250) OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   Begin Tran

   Insert into dbo.tbl_sce_cvd_ft
    Values(@codcct, @codpro, @codesp, @codofi, @codope, @tipcvd, @Valida_Iny)

    
   If @@error <> 0
   Begin
		rollback tran
      select   @lc_retorno = ''E01'',
               @lc_mensaje = ''Problemas al insertar en tabla tbl_sce_cvd_ft''
   End

   Commit Tran

   select   @lc_retorno = ''E00'',
       @lc_mensaje = ''Inserción Exitosa en tabla tbl_sce_cvd_ft''
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_cvd_ft_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cvd_ft_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_cvd_ft_s01_MS]
	@codcct     CHAR(3),
	@codpro     CHAR(2),
	@codesp     CHAR(2),
	@codofi     CHAR(3),
	@codope     CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   Select   tip_cvd
   From   dbo.tbl_sce_cvd_ft a
   Where  a.codcct = @codcct
   and    a.codpro = @codpro
   and    a.codesp = @codesp
   and    a.codofi = @codofi
   and    a.codope = @codope
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_cvd_ft_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cvd_ft_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_cvd_ft_s02_MS]
	@codcct     CHAR(3),
	@codpro     CHAR(2),
	@codesp     CHAR(2),
	@codofi     CHAR(3),
	@codope     CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   Select   valida_iny
   From   dbo.tbl_sce_cvd_ft a
   Where  a.codcct = @codcct
   and    a.codpro = @codpro
   and    a.codesp = @codesp
   and    a.codofi = @codofi
   and    a.codope = @codope
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_datusr_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_datusr_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Estanislao Tiscornia
-- Description:	Actualiza los datos del usuario para impresion
-- =============================================
CREATE procedure [dbo].[pro_sce_datusr_u01_MS]
	@samAccountName nvarchar(500), 
	@ConfigImpres_ImprimeCartas nvarchar(500),
	@ConfigImpres_ImprimePlanillas nvarchar(500),
	@ConfigImpres_ImprimeReporte nvarchar(500)

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE [dbo].[tbl_datos_usuario]
	   SET [ConfigImpres_ImprimeCartas] = @ConfigImpres_ImprimeCartas
		  ,[ConfigImpres_ImprimePlanillas] = @ConfigImpres_ImprimePlanillas
		  ,[ConfigImpres_ImprimeReporte] = @ConfigImpres_ImprimeReporte
	 WHERE [samAccountName] = @samAccountName
END


' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_fts_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_fts_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_fts_i01_MS]
	@gb_record_type        CHAR(3)      ,
	@gb_branch_number      NUMERIC(3,0) ,
	@gb_contract_reference NUMERIC(10,0),
	@gb_ordering_customer  CHAR(15),
	@gb_act_hist_indicator CHAR(1)      ,
	@gb_input_date1        NUMERIC(6,0) ,
	@gb_receiver           CHAR(15),
	@gb_credit_entry_date  NUMERIC(6,0) ,
	@gb_order_cost_account NUMERIC(11,0),
	@gb_receiver_account   NUMERIC(11,0),
	@gb_authorization_stat CHAR(1)      ,
	@gb_transac_type_code  CHAR(1)      ,
	@gb_exe_type_code_tran CHAR(1)      ,
	@gb_product_type       CHAR(1)      ,
	@gb_swf_currency_code  CHAR(4)      ,
	@gb_currency_code      NUMERIC(3,0) ,
	@gb_charges_debit      NUMERIC(11,0),
	@gb_transfer_amount    DECIMAL(15,2)    , -- numeric(17,0),
	@gb_sign_transfer      CHAR(1)      ,
	@gb_legal_vehicle_code NUMERIC(2,0) ,
	@gb_debit_value_date   NUMERIC(6,0) ,
	@gb_data_entry_date    NUMERIC(6,0) ,
	@gb_credit_value_date  NUMERIC(6,0) ,
	@gb_texto              CHAR(24)     ,
	@gb_status             CHAR(1)      ,
	@gb_by_order_of        CHAR(35)     ,
	@gb_beneficiary        CHAR(35)     ,
	@gb_last_inp_date      NUMERIC(6,0) ,
	@gb_transfer_charged   CHAR(1)      ,
	@gb_operator_id        CHAR(3)      ,
	@gb_input_date2        NUMERIC(6,0) ,
	@gb_input_time         NUMERIC(7,0) ,
	@gb_authorizer_id      CHAR(3)      ,
	@gb_authorizer_time    NUMERIC(7,0) ,
	@gb_order_cust_account NUMERIC(11,0),
	@gb_input_date3        NUMERIC(5,0) ,
	@gb_alpha_number       CHAR(12),
	@gb_swf_currency_equi  CHAR(4)      ,
	@gb_currency_code_equi NUMERIC(3,0) ,
	@gb_equivalent_amount  DECIMAL(15,2)    , -- numeric(17,0),
	@gb_signo_equivalent   CHAR(1)      ,
	@gb_fcy_exchange_rate  NUMERIC(11,0),
	@gb_receiver_account2  NUMERIC(11,0),
	@gb_input_date4        NUMERIC(5,0) ,
	@gb_short_benefic_bank CHAR(32)     ,
	@gb_alpha_reference    CHAR(24)     ,
	@gb_lto_indicator      CHAR(1)      ,
	@gb_benefi_account_num CHAR(16)     ,
	@gb_commission_rate    NUMERIC(9,0) ,
	@gb_commission_amount  DECIMAL(15,2)    , -- numeric(17,0),
	@gb_sing_commssion     CHAR(1)      ,
	@gb_courtage_rate      NUMERIC(5,0) ,
	@gb_courtage_amount    NUMERIC(17,0),
	@gb_sign_courtage      CHAR(1)      ,
	@gb_postage_amount     NUMERIC(17,0),
	@gb_sign_postage       CHAR(1)      ,
	@gb_swf_currency_charg CHAR(4)      ,
	@gb_currency_code_chan NUMERIC(3,0) ,
	@gb_chrg_base_nbr      NUMERIC(11,0),
	@gb_short_charges_acou CHAR(32)     ,
	@gb_reference_number   CHAR(16)     ,
	@gb_central_bank_code  NUMERIC(11,0),
	@gb_num_order_customer NUMERIC(1,0) ,
	@gb_num_receiver       NUMERIC(1,0) ,
	@gb_num_beneficia_bank NUMERIC(1,0) ,
	@gb_num_beneficiary    NUMERIC(1,0) ,
	@gb_num_reason         NUMERIC(1,0) ,
	@gb_num_bank_to_bank   NUMERIC(1,0) ,
	@gb_num_charges        NUMERIC(1,0) ,
	@gb_total_number       NUMERIC(2,0) ,
	@gb_text_line          VARCHAR(250) ,
	@gb_text_line2         VARCHAR(250) ,
	@ls_retorno            CHAR(3)  OUTPUT,     
	@ls_mensaje            CHAR(250) OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

Begin Tran
   Insert Into dbo.tbl_sce_fts(record_type       , branch_number     , contract_reference , ordering_customer , act_hist_indicator , input_date1 ,
            receiver          , credit_entry_date , order_cost_account , receiver_account  , authorization_stat , transac_type_code ,
            exe_type_code_tran, product_type      , swf_currency_code  , currency_code     , charges_debit      , transfer_amount   ,
            sign_transfer     , legal_vehicle_code, debit_value_date   , data_entry_date   , credit_value_date  , texto             ,
            status            , by_order_of       , beneficiary        , last_inp_date     , transfer_charged   , operator_id       ,
            input_date2       , input_time        , authorizer_id      , authorizer_time   , order_cust_account , input_date3       ,
            alpha_number      , swf_currency_equi , currency_code_equi , equivalent_amount , signo_equivalent   , fcy_exchange_rate ,
            receiver_account2 , input_date4       , short_benefic_bank , alpha_reference   , lto_indicator      , benefi_account_num,
            commission_rate   , commission_amount , sing_commssion     , courtage_rate     , courtage_amount    , sign_courtage     ,
            postage_amount    , sign_postage      , swf_currency_charg , currency_code_chan, chrg_base_nbr      , short_charges_acou,
            reference_number  , central_bank_code , num_order_customer , num_receiver      , num_beneficia_bank , num_beneficiary   ,
            num_reason        , num_bank_to_bank  , num_charges        , total_number      , text_line          , text_line2)
	    values(@gb_record_type       , @gb_branch_number     , @gb_contract_reference , @gb_ordering_customer , @gb_act_hist_indicator , @gb_input_date1 ,
            @gb_receiver          , @gb_credit_entry_date , @gb_order_cost_account , @gb_receiver_account  , @gb_authorization_stat , @gb_transac_type_code ,
            @gb_exe_type_code_tran, @gb_product_type      , @gb_swf_currency_code  , @gb_currency_code     , @gb_charges_debit      , @gb_transfer_amount   ,
            @gb_sign_transfer     , @gb_legal_vehicle_code, @gb_debit_value_date   , @gb_data_entry_date   , @gb_credit_value_date  , @gb_texto             ,
            @gb_status            , @gb_by_order_of       , @gb_beneficiary        , @gb_last_inp_date     , @gb_transfer_charged   , @gb_operator_id       ,
            @gb_input_date2       , @gb_input_time        , @gb_authorizer_id      , @gb_authorizer_time   , @gb_order_cust_account , @gb_input_date3       ,
            @gb_alpha_number      , @gb_swf_currency_equi , @gb_currency_code_equi , @gb_equivalent_amount , @gb_signo_equivalent   , @gb_fcy_exchange_rate ,
            @gb_receiver_account2 , @gb_input_date4       , @gb_short_benefic_bank , @gb_alpha_reference   , @gb_lto_indicator      , @gb_benefi_account_num,
            @gb_commission_rate   , @gb_commission_amount , @gb_sing_commssion     , @gb_courtage_rate     , @gb_courtage_amount    , @gb_sign_courtage     ,
            @gb_postage_amount    , @gb_sign_postage      , @gb_swf_currency_charg , @gb_currency_code_chan, @gb_chrg_base_nbr      , @gb_short_charges_acou,
            @gb_reference_number  , @gb_central_bank_code , @gb_num_order_customer , @gb_num_receiver      , @gb_num_beneficia_bank , @gb_num_beneficiary   ,
            @gb_num_reason        , @gb_num_bank_to_bank  , @gb_num_charges        , @gb_total_number      , @gb_text_line          , @gb_text_line2)
                 
        
   If @@error != 0
   Begin
      Rollback 
      Select   @ls_retorno = ''E01''
      Select   @ls_mensaje = ''pro_sce_fts_i01:[E01]-Error al ingresar registro en tabla tbl_sce_fts.''
      Return
   End    
   Commit Tran
   Select   @ls_retorno = ''E00''
   Select   @ls_mensaje = ''El ingreso del registro FTS termino exitosamente''
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_inpl_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_inpl_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_inpl_s01_MS]
	@cencos 	CHAR(3),
	@codpro		CHAR(2),
	@codesp		CHAR(2),
	@codofi		CHAR(3),
	@codope		CHAR(5),
	@nropln 	NUMERIC(10,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   cent_costo     ,
        id_product     ,
        id_especia     ,
        id_empresa     ,
        id_cobranz     ,
        numplan        ,
        fecha          ,
        nro            ,
        concepto       ,
        tipo           ,
        monto          ,
        capbas         ,
        codbas         ,
        tasa           ,
        fini           ,
        ffin           ,
        ndias
   from dbo.sce_inpl
   where 	cent_costo = @cencos and
   id_product = @codpro and
   id_especia = @codesp and
   id_empresa = @codofi and
   id_cobranz = @codope and
   numplan    = @nropln

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_ovd_ft_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_ovd_ft_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_ovd_ft_s01_MS] 
	@cta_nor BIT,--0 Normal, 1 Cosmos
	@cta_cos BIT--0 Manual, 1 Automatico
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   if (@cta_nor = 0)
   begin
      select   numcta, nomcta, nemcta, monnac, ctaori, ctavia, ctavue, codtra
      from    dbo.tbl_sce_ovd_ft
      where   ctanor = @cta_nor
      return
   end
else
   begin
      if (@cta_cos = 0)
      begin
         select   numcta, nomcta, nemcta, monnac, ctaori, ctavia, ctavue, codtra
         from    dbo.tbl_sce_ovd_ft
         where   ctacos = 1
         return
      end
   else
      begin
         if (@cta_cos = 1)
         begin
            select   numcta, nomcta, nemcta, monnac, ctaori, ctavia, ctavue, codtra
            from    dbo.tbl_sce_ovd_ft
            where   ctatip = 1
            return
         end
      end
   end
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_parametros_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_parametros_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_parametros_s01_MS]
	@gb_tipo_ft VARCHAR(200),
	@gb_moneda NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   Select   a.desc_ft
   From   dbo.tbl_sce_parametros_ft a
   Where  a.tipo_ft    = @gb_tipo_ft
   And    a.codmnd_bch = @gb_moneda
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_i06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_i06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_i06_MS] 
	--TABLA sce_prty
	@id_party       CHAR(12),
	@borrado        BIT	,
	@tipo_party     TINYINT	,
	@flag           SMALLINT,
	@clasificac     TINYINT ,
	@tiene_rut      BIT	,
	@rut            CHAR(10),
	@crea_costo     CHAR(3),
	@crea_user      CHAR(2),
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
	--TABLA sce_rsa
	@id_nombre      NUMERIC(2,0),
	@razon_soci     VARCHAR(60),
	@contacto       VARCHAR(40),
	--TABLA sce_dad 
	@id_dir         NUMERIC(2,0),
	@direccion      VARCHAR(60),
	@comuna         VARCHAR(40),
	@estado         VARCHAR(25),
	@ciudad         VARCHAR(30),
	@pais           VARCHAR(30),
	@telefono       CHAR(10),
	@fax            CHAR(10),
	@telex          CHAR(15),
	@envio_sce      NUMERIC(2,0),
	@recibe_sce     NUMERIC(2,0),
	@cod_postal     VARCHAR(20),
	@cas_postal     CHAR(20),
	@cas_banco      CHAR(20),
	@email          VARCHAR(50),
	@fecha          Datetime,
	@cuenta         CHAR(11),
	@moneda         NUMERIC(3,0),
	--
	@ls_retorno      CHAR(3)  OUTPUT,     
	@ls_mensaje      CHAR(250) OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   Declare @lc_codcomuna NUMERIC(4,0)
   Declare @lc_codpais   NUMERIC(3,0)
   Declare @lc_extranjera BIT
Begin Tran
   If Not Exists(SELECT TOP 1 1 From dbo.sce_prty Where id_party = @id_party)
   Begin
      Insert Into dbo.sce_prty(id_party  , borrado    , tipo_party , flag       , clasificac , tiene_rut  ,  rut ,
                crea_costo, crea_user  , mod_costo  , mod_user   , multiple   , cod_ofieje , cod_eject ,
                cod_acteco, clase_ries , cod_bco    , tasa_libor , tasa_prime , spread     ,swift,
                plaza_alad, ejec_corre , flagins    , insgen_imp , insgen_exp , insgen_ser , inscob_imp ,
                inscob_exp, inscre_imp , inscre_exp , fecing     , fecact)
		values(@id_party  , @borrado     , @tipo_party , @flag       , @clasificac , @tiene_rut , @rut,
                @crea_costo, @crea_user   , @crea_costo , @crea_user  , @multiple   , @cod_ofieje, @cod_eject  ,
                @cod_acteco, @clase_ries  , @cod_bco    , @tasa_libor , @tasa_prime , @spread    , @swift      ,
                @plaza_alad, @ejec_corre  , @flagins    , @insgen_imp , @insgen_exp , @insgen_ser, @inscob_imp ,
                @inscob_exp , @inscre_imp , @inscre_exp , @fecha      , @fecha)
           
      If @@error != 0
      Begin
         Rollback 
         Select   @ls_retorno = ''E01''
         Select   @ls_mensaje = ''pro_sce_prty_i06:[E01]-Error al ingresar registro en tabla sce_prty.''
         Return
      End
   End             

   If Not Exists(SELECT TOP 1 1 From dbo.sce_rsa Where id_party = @id_party)
   Begin
      Insert Into dbo.sce_rsa(id_party  , id_nombre   ,borrado   , razon_soci  , nom_fantas ,contacto, sortkey , crea_costo, crea_user)
		values(@id_party  , @id_nombre ,@borrado   , @razon_soci , @razon_soci,@contacto, Upper(@razon_soci) , @crea_costo, @crea_user)
           
      If @@error != 0
      Begin
         Rollback 
         Select   @ls_retorno = ''E02''
         Select   @ls_mensaje = ''pro_sce_prty_i06:[E02]-Error al ingresar registro en tabla sce_rsa.''
         Return
      End
   End      
               
   If Not Exists(SELECT TOP 1 1 From dbo.sce_dad Where id_party = @id_party)
   Begin
        --CODIGO DE LA COMUNA
      select   Upper(@comuna)
      If Upper(@comuna) = ''SANTIAGO'' 
         Select   @lc_codcomuna = 320
      Select   @lc_codcomuna = loc_loccod
      From   dbo.sgt_loc
      Where  loc_locnom = Upper(@comuna)
      if @lc_codcomuna IS NULL  
         Select   @lc_codcomuna = 9999
        
        --CODIGO DEL PAIS
      Select   @lc_codpais = pai_paicod
      From   dbo.sgt_pai
      Where  pai_painom = Upper(@pais)
      if @lc_codpais IS NULL  
         Select   @lc_codpais = 999
      Insert Into dbo.sce_dad(id_party  , id_dir     ,direccion   , comuna    , cod_comuna   ,estado  , ciudad    , pais       , cod_pais  ,
                telefono  , fax        ,telex       , cas_postal, cas_banco    ,email   , envio_sce , recibe_sce ,crea_costo, crea_user,borrado,
                cod_postal)
		values(@id_party  , @id_dir    ,@direccion   , @comuna    ,@lc_codcomuna, @estado, @ciudad   , @pais      ,@lc_codpais,
               @telefono  , @fax       ,@telex       , @cas_postal,@cas_banco   , @email , @envio_sce , @recibe_sce,@crea_costo, @crea_user,@borrado,
               @cod_postal)
           
      If @@error != 0
      Begin
         Rollback 
         Select   @ls_retorno = ''E03''
         Select   @ls_mensaje = ''pro_sce_prty_i06:[E03]-Error al ingresar registro en tabla sce_dad.''
         Return
      End
   End      
        
   If Not Exists(SELECT TOP 1 1 From dbo.sce_ctas Where id_party = @id_party And cuenta = @cuenta)
      --extranjera  ME=1; MN=0  
      if @moneda = 1  
         Select   @lc_extranjera = 0
   If @moneda <> 1 
      Select   @lc_extranjera = 1
   Begin
      Insert Into dbo.sce_ctas(id_party  , secuencia   ,borrado   , activabco  , activace ,extranjera    , moneda , cuenta)
		values(@id_party  , 0           ,@borrado  , 0          , 1       , @lc_extranjera, @moneda ,@cuenta)
           
      If @@error != 0
      Begin
         Rollback 
         Select   @ls_retorno = ''E04''
         Select   @ls_mensaje = ''pro_sce_prty_i06:[E04]-Error al ingresar registro en tabla sce_ctas.''
         Return
      End
   End      

   Commit Tran
   Select   @ls_retorno = ''E00''
   Select   @ls_mensaje = ''El ingreso de participante termina exitosamente''
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s01_MS] 
	@gb_razon_social CHAR(20) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
      2015-09-09   AOC   Corrección para soportar busquedas parciales
*/
   SET NOCOUNT ON 

   Declare @lc_busca VARCHAR(22)
   Select   @lc_busca = ''%''+UPPER(RTRIM(@gb_razon_social)) +''%'' 

   Select   a.razon_soci,
        b.direccion,
        b.ciudad,
        b.pais,
        a.id_party
   From    dbo.sce_rsa a
   INNER LOOP JOIN  dbo.sce_dad b ON a.id_party = b.id_party
   Where  UPPER(a.razon_soci) like @lc_busca
   Order by a.razon_soci

END





' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s02_MS] 
	@gb_llave CHAR(12),
	@gb_opcion INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   If @gb_opcion = 1
   Begin
      Select   borrado,
           tipo_party,
           flag,
           tiene_rut,
           rut, cod_bco,
           swift
      From   dbo.sce_prty
      Where  id_party = @gb_llave
   End   
 
   If @gb_opcion = 2
   Begin
      Select   borrado,
            razon_soci,
            id_nombre
      From   dbo.sce_rsa
      Where  id_party = @gb_llave
   End
 
   If @gb_opcion = 3
   Begin
      Select   borrado,
            direccion,
            id_dir,
            comuna,
            ciudad,
            cod_postal,
            pais,
            cod_pais,
            estado,
            telefono,
            fax,
            telex,
            envio_sce,
            cas_postal,
            cas_banco
      From    dbo.sce_dad
      Where   id_party = @gb_llave
   End
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s03_MS] 
	@gb_ccosto       CHAR(3),
    @gb_producto     CHAR(2),
    @gb_especialista CHAR(2),
    @gb_empresa      CHAR(3), 
    @gb_operacion    CHAR(5),  
    @gb_party        CHAR(2),
    @gb_opcion       INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   If @gb_opcion = 1
   Begin
      Select   esbanco,
            rut,
            razon_soci,
            direccion,
            comuna,
            estado,
            ciudad,
            pais,
            cod_pais,
            cod_postal,
            telefono,
            fax,
            telex,
            envio_sce,
            cas_postal,
            cas_banco
      From  dbo.sce_pope
      Where id_costo   = @gb_ccosto
      And   id_product = @gb_producto
      And   id_especia = @gb_especialista
      And   id_empresa = @gb_empresa
      And   id_operac  = @gb_operacion
      And   id_party   = @gb_party
   End
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s04_MS]	
	@gb_operacion    CHAR(6), 
	@gb_estado CHAR(3) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 14:39:39 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   Select   d.estado            ,
           a.FCCFT             ,  --NºOPERACION
           d.fecingreso        ,  --FECHA DE LA OPERACION
           b.mnd_mndnom        ,  --MONEDA DE LA OPERACION
           a.DRAMT             ,  --MONTO DE LA OPERACION  (PANTALLA COMERCIO INVISIBLE)
           a.XREF              ,  --REFERENCIA.
           e.desc_ft           ,  --ESTADO
           c.codmnd_bch        ,  -- a.DRCCY,        --CODIGO MONEDA          (PANTALLA COMERCIO INVISIBLE)
           a.PRD               ,  --IDENTIFICADOR DE PARTICIPANTE (PANTALLA PARTICIPANTES)
           a.CRACC             ,  --SI COD = 062                  (PANTALLA PARTICIPANTES)
           a.DRACC             ,  --SI COD = 072 O 074            (PANTALLA PARTICIPANTES)
           a.ORD_INST1         ,  --PANTALLA DESTINO DE FONDOS
           a.PMNT_DET1         ,  --PANTALLA DESTINO DE FONDOS
           a.PMNT_DET2         ,  --PANTALLA DESTINO DE FONDOS
           a.PMNT_DET3         ,  --PANTALLA DESTINO DE FONDOS
           a.PMNT_DET4         ,  --PANTALLA DESTINO DE FONDOS
           d.DEBIT_REF         ,  --PANTALLA ORIGENES DE FONDOS
           h.desc_ft           ,  --Observacion 
           f.desc_ft           ,  --DRACC
           a.BEN_INST1         ,  --MENSAJE SWIFT CAMPO 59-1
           a.ULT_BEN1          ,  --MENSAJE SWIFT CAMPO 59-2
           a.ULT_BEN2          ,  --MENSAJE SWIFT CAMPO 59-3
           a.ULT_BEN3          ,  --MENSAJE SWIFT CAMPO 59-4
           a.ULT_BEN4          ,  --MENSAJE SWIFT CAMPO 59-5
           a.CHG_WHOM          ,  --MENSAJE SWIFT CAMPO 71-F
           a.DRVALDT           ,
           a.INTRMD1           ,  --MENSAJE SWIFT CAMPO 56-a
           a.INTRMD2           ,
           d.US_PAY_ID         ,
           a.RECVR_CORRES1     ,  --MENSAJE SWIFT CAMPO 57-a
           a.RECVR_CORRES2     ,  --           
           a.SNDR_RECVR_INFO1  ,  --MENSAJE SWIFT CAMPO 72-1
           a.SNDR_RECVR_INFO2  ,  --MENSAJE SWIFT CAMPO 72-2
           a.SNDR_RECVR_INFO3  ,  --MENSAJE SWIFT CAMPO 72-3
           a.SNDR_RECVR_INFO4  ,  --MENSAJE SWIFT CAMPO 72-4
           d.trxid             ,  --Transaccion ID
           g.contract_reference,  --CRF
           a.SNDR_RECVR_INFO5  ,  --MENSAJE SWIFT CAMPO 72-4
           a.SNDR_RECVR_INFO6     --MENSAJE SWIFT CAMPO 72-4
   From   dbo.tbl_sce_from_ESB2 d LEFT OUTER JOIN dbo.tbl_sce_parametros_ft h ON h.cod_ft = d.FIELD_VALUE_07, 
dbo.tbl_sce_from_ESB a, dbo.sgt_mnd b, dbo.tbl_sce_parametros_ft c, dbo.tbl_sce_parametros_ft e, dbo.tbl_sce_parametros_ft f, dbo.tbl_sce_relacion_ft g
   Where  c.tipo_ft    = ''MND_COSMOS''
   And    c.codmnd_bch = b.mnd_mndcod
   And    e.tipo_ft    = ''APLI''
   And    d.estado     = e.cod_ft
   And    a.DRCCY      = c.cod_ft
   And    d.DRCCY      = c.cod_ft
   And    (a.DRACC     = f.cod_ft or a.CRACC = f.cod_ft)
   And    f.tipo_ft    = ''SWIFT''
   And    (@gb_operacion in(a.XREF) OR @gb_operacion IS NULL)
   And    (@gb_estado in(d.estado) OR @gb_estado IS NULL)
   And    a.XREF       = d.XREF
   And    a.DRCCY      = d.DRCCY
   And    a.DRAMT      = d.DRAMT
   And    d.trxid      = g.transaction_id
   And    h.tipo_ft    = ''REMS''
   AND	  d.fecingreso >= DATEADD(d,DATEDIFF(d,0,getdate()),0)
   AND	  d.fecingreso < DATEADD(d,DATEDIFF(d,0,getdate()),0) + 1
   order by d.fecingreso
END' 

END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s05_MS]
	@gb_llave   CHAR(12), 
    @gb_id      INT,
    @gb_opcion  INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   If @gb_opcion = 1
   Begin
      Select   borrado,
            razon_soci
      From   dbo.sce_rsa
      Where  id_party  = @gb_llave
      and    id_nombre = @gb_id
   End
 
   If @gb_opcion = 2
   Begin
      Select   borrado,
            direccion,
            comuna,
            cod_postal,
            estado,
            ciudad,
            pais,
            cod_pais,
            telefono,
            fax,
            telex,
            envio_sce,
            cas_postal,
            cas_banco
      From    dbo.sce_dad
      Where   id_party = @gb_llave
      and     id_dir   = @gb_id
   End
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s06_MS]
	@gb_razon_social VARCHAR(60) 

AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	DECLARE @lc_busca VARCHAR(60)
	DECLARE @lc_ret INT = 1

	SELECT @lc_busca = ''%''+UPPER(@gb_razon_social)+''%''

	SELECT
			r.id_party,
			r.id_nombre,
			r.razon_soci,
			r.borrado,
			d.direccion,
			d.ciudad,
			d.pais
   FROM		dbo.sce_rsa r, dbo.sce_dad d
   WHERE	r.id_party = d.id_party
			AND UPPER(r.razon_soci) LIKE RTRIM(@lc_busca)
   ORDER BY	r.razon_soci

   RETURN @lc_ret
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s07_MS]
	@gb_razon_social VARCHAR(60) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON

	DECLARE @lc_busca VARCHAR(60)
	DECLARE @lc_ret INT = 1

	SELECT @lc_busca = ''%''+UPPER(@gb_razon_social)+''%''

	SELECT
			r.id_party,
			r.id_nombre,
			d.id_dir,
			r.razon_soci,
			r.borrado,
			d.direccion,
			d.ciudad,
			d.pais
	FROM		dbo.sce_rsa r, dbo.sce_dad d
	WHERE	r.id_party = d.id_party
			AND UPPER(r.razon_soci) LIKE RTRIM(@lc_busca)
	ORDER BY	r.razon_soci

	RETURN @lc_ret
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_prty_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_prty_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_prty_s08_MS]
	@id_party	CHAR(12),
	@id_nombre	INT = -1,
	@id_direccion INT = -1
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
   
   	DECLARE @lc_ret INT = 1

	--(moo) buscamos razones sociales asociadas
	IF @id_nombre = -1
	BEGIN
		SELECT 
			id_party,
			id_nombre,
			borrado,
			razon_soci,
			nom_fantas,
			contacto,
			sortkey,
			crea_costo,
			crea_user
		FROM sce_rsa
			WHERE id_party = @id_party
		ORDER BY razon_soci
	END
	ELSE
	BEGIN
		SELECT 
			id_party,
			id_nombre,
			borrado,
			razon_soci,
			nom_fantas,
			contacto,
			sortkey,
			crea_costo,
			crea_user
		FROM sce_rsa
			WHERE id_party = @id_party
			AND id_nombre = @id_nombre
		ORDER BY razon_soci
	END

	--(moo) buscamos direcciones
	IF @id_direccion = -1
	BEGIN
		SELECT 
			id_party,
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
			email
		FROM sce_dad
			WHERE id_party = @id_party
		ORDER BY direccion
	END
	ELSE
	BEGIN
		SELECT 
			id_party,
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
			email
		FROM sce_dad
			WHERE id_party = @id_party
			AND id_dir = @id_direccion
		ORDER BY direccion
	END

	RETURN @lc_ret
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_relacion_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_relacion_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_relacion_i01_MS] 
	@codcct             CHAR(3),
	@codpro             CHAR(2),
	@codesp             CHAR(2),
	@codofi             CHAR(3),
	@codope             CHAR(5),
	@XREF               CHAR(20),       --XREF
	@transaction_id     CHAR(27),
	@moneda             NUMERIC(3,0),     --Moneda COSMOS
	@monto              NUMERIC(15,2),  --Monto
	@nroimp             NUMERIC(3,0),
	@auto_manual        INT,
	@fecing             Datetime,
	@fecha2             CHAR(8),
	@contract_reference NUMERIC(10,0),
	@ls_codigo          CHAR(3)   OUTPUT,
	@ls_mensaje         CHAR(250) OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   Declare @lc_estado INT
BEGIN TRAN

   If @auto_manual = 1
   Begin
      update dbo.tbl_sce_relacion_ft set codcct = @codcct,codpro = @codpro,codesp = @codesp,codofi = @codofi,codope = @codope,
      moneda = @moneda,monto  = @monto,nroimp = @nroimp,fecing = @fecing  Where  transaction_id = @transaction_id
      and    contract_reference = @contract_reference
      If @@error <> 0
      Begin
         ROLLBACK 
         Select   @ls_codigo = ''E01''
         Select   @ls_mensaje = ''pro_sce_relacion_i01. Problemas al actualizar registros en tabla tbl_sce_relacion_ft.''
         Return
      End    

            --SI ES CARGA AUTOMATICA SE DEBE CAMBIAR EL ESTADO DE LA OPERACIONES
            --DEBE VER SI QUEDO EN ESTADO ANULADA EN TABLA sce_mcd
      Select   @lc_estado = estado
      From   dbo.sce_mcd
      Where  codcct  = @codcct
      And    codpro  = @codpro
      And    codesp  = @codesp
      And    codofi  = @codofi
      And    codope  = @codope
      If @lc_estado <> 9 
         Select   @lc_estado = 2 --CURSADA
      If @lc_estado =  9 
         Select   @lc_estado = 1 --Vigente

      select   @lc_estado
      update dbo.tbl_sce_from_ESB2 set estado     = Convert(CHAR(1),@lc_estado),fecingreso = @fecha2,operacion  = @codcct+@codpro+@codesp+@codofi+@codope  Where  XREF       = @XREF
        --        And    DRCCY      = @DRCCY
      And    DRAMT      = Convert(CHAR(21),@monto)
      If @@error <> 0
      Begin
         ROLLBACK 
         Select   @ls_codigo = ''E02''
         Select   @ls_mensaje = ''pro_sce_relacion_i01. Problemas al actualizar registros en tabla tbl_sce_from_ESB2.''
         Return
      End
   End
Else
   Begin
      INSERT INTO dbo.tbl_sce_relacion_ft(codcct,  codpro,  codesp,  codofi,  codope,  moneda,  monto,  nroimp,  transaction_id,  fecing,  contract_reference,transaction_id_rev)
            VALUES(@codcct, @codpro, @codesp, @codofi, @codope, @moneda, @monto, @nroimp, @transaction_id, @fecing, @contract_reference,'''')
            
      If (@@rowcount = 0)
      Begin
         ROLLBACK 
         Select   @ls_codigo = ''E03''
         Select   @ls_mensaje = ''pro_sce_relacion_i01. Problemas al insertar registros en tabla tbl_sce_relacion_ft.''
         Return
      End
   End
   COMMIT TRAN
   Select   @ls_codigo = ''E00''
   Select   @ls_mensaje = ''pro_sce_relacion_i01. Insercion de registro Exitoso en tabla tbl_sce_relacion_ft.''
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_rev_abocar_s01_MS] 
	@cencos CHAR(3),
	@codusr CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @fecpro CHAR(10)
   declare @fecpro2    CHAR(10)

   Create Table #tmp_rev_abocar
   (
      numcct         CHAR(15),
      nemcta         CHAR(15),
      fecmov         Datetime,
      nroimp         NUMERIC(3,0),
      nomcli         VARCHAR(30),
      cod_dh         CHAR(1),
      moneda         CHAR(3),
      codmon         NUMERIC(3,0),
      mtomcd         NUMERIC(15,2),
      operacion      CHAR(15),
      nrorpt         NUMERIC(8,0),
      enlinea        NUMERIC(1,0),
      estado         NUMERIC(2,0),
      codcct         CHAR(3),
      codpro         CHAR(2),
      codesp         CHAR(2),
      codofi         CHAR(3),
      codope         CHAR(5),
      codfun         NUMERIC(3,0),
      trx_id         CHAR(27),
      cod_prod       CHAR(4),
      cod_trx_fc     CHAR(3),
      cod_ext        CHAR(5),
      cod_swf        CHAR(3),
      data1          CHAR(35),
      data2          CHAR(35),
      data3          CHAR(35),
      data4          CHAR(35),
      data5          CHAR(35),
      cod_trx_cosmos CHAR(3),
      tip_cta        CHAR(4)      
   )

   select   @fecpro = convert(CHAR(10),GetDate(),101)

   Insert Into #tmp_rev_abocar
   Select  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(convert(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0),
            d.transaction_id,
            f.cod_prod,
            f.cod_trx_fc,
            f.cod_ext,
            f.cod_swf,
            c.data1,
            c.data2,
            c.data3,
            c.data4,
            c.data5,
            f.cod_trx_cosmos,
            ''CITI''
   from  dbo.sce_mcd a,
          dbo.sce_mch b,
          dbo.tbl_sce_tranope_ft c,
          dbo.tbl_sce_relacion_ft d,
          dbo.tbl_sce_parametros_ft e,
          dbo.tbl_sce_codtran_ft f,
          dbo.sgt_mnd g
   where a.codpro  = ''30''
   and   a.enlinea = 1
   and   a.cencos  = @cencos
   and   a.codusr  = @codusr
   and   a.fecmov  = @fecpro
   and	  a.nemcta  = e.tipo_ft
   and   a.codcct  = b.codcct
   and   a.codpro  = b.codpro
   and   a.codesp  = b.codesp
   and   a.codofi  = b.codofi
   and   a.codope  = b.codope
   and   a.nrorpt  = b.nrorpt
   and   a.fecmov  = b.fecmov
   and   a.codcct  = c.codcct
   and   a.codpro  = c.codpro
   and   a.codesp  = c.codesp
   and   a.codofi  = c.codofi
   and   a.codope  = c.codope
   and   a.nrorpt  = c.nrorpt
   and   a.fecmov  = c.fecmov
   and   a.cod_dh  = c.cod_dh
   and   a.codcct  = d.codcct
   and   a.codpro  = d.codpro
   and   a.codesp  = d.codesp
   and   a.codofi  = d.codofi
   and   a.codope  = d.codope
   and   a.mtomcd  = d.monto
   and   a.codmon  = d.moneda
   and   a.nroimp  = d.nroimp
   and   c.nro_trx = f.nro_trx
   and   a.codmon  = g.mnd_mndcod
   and   c.nroimp  = d.nroimp
   and   b.estado <> 9

/*---------------------------------------------
  Realsystems-Código Nuevo-Inicio
  Fecha Modificación 20100902
  Responsable: Pablo Millan V.
  Versión: 1.0
  Descripción : Se omiten inyec. de op. auto.
---------------------------------------------*/
   select   @fecpro2 = convert(CHAR(10),GetDate(),112)


   delete from #tmp_rev_abocar
   where  codcct+codpro+codesp+codofi+codope in(select operacion
   from   dbo.tbl_sce_from_ESB2
   where  fecingreso = @fecpro2)
/*----------------------------------------
  RealSystems - Código Nuevo - Termino
  --------------------------------------*/


   Insert Into #tmp_rev_abocar
   select  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            isnull(convert(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            a.nemmon,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            isnull(b.codfun,0),
            c.transaction_id,
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            ''BCH''
   from  dbo.sce_mcd a LEFT OUTER JOIN dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov and b.estado  <> 9, 
dbo.tbl_sce_relacion_ft c
   where a.codpro  = ''30'' AND a.enlinea = 1 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  @fecpro AND a.nemcta  in(''CC$'',''CCE'') AND a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope AND a.codmon  =  c.moneda AND a.mtomcd  =  c.monto
    --and (enlinea = 0 or (estado = 9 and enlinea = 1))
    --     Trae todos los mov. a cursar y a reversar
    --and ( enlinea = 0 or (estado = 1 and enlinea = 1))

   select   numcct     ,
           nemcta     ,
           fecmov     ,
           nroimp     ,
           nomcli     ,
           cod_dh     ,
           moneda     ,
           codmon     ,
           mtomcd     ,
           operacion  ,
           nrorpt     ,
           enlinea    ,
           estado     ,
           codcct     ,
           codpro     ,
           codesp     ,
           codofi     ,
           codope     ,
           codfun     ,
           trx_id     ,
           cod_prod   ,
           cod_trx_fc ,
           cod_ext    ,
           cod_swf    ,
           data1      ,
           data2      ,
           data3      ,
           data4      ,
           data5      ,
           cod_trx_cosmos,
           tip_cta
   from  #tmp_rev_abocar
   order by nrorpt,nroimp

   Drop Table #tmp_rev_abocar
    
End

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_rev_abocar_s02_MS] 
	@gb_codcct CHAR(3),
	@gb_codpro CHAR(2),
	@gb_codesp CHAR(2),
	@gb_codofi CHAR(3),
	@gb_codope CHAR(5),
	@gb_trxid  CHAR(27),
	@gb_opcion NUMERIC(1,0) 

AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   If @gb_opcion = 1
   Begin
      select   count(1)
      from   dbo.sce_mcd a,
           dbo.tbl_sce_relacion_ft b
      where  a.codcct  = b.codcct
      and    a.codpro  = b.codpro
      and    a.codesp  = b.codesp
      and    a.codofi  = b.codofi
      and    a.codope  = b.codope
      and    a.nroimp  = b.nroimp
      and    a.fecmov  = b.fecing
      and    b.codcct  = @gb_codcct
      and    b.codpro  = @gb_codpro
      and    b.codesp  = @gb_codesp
      and    b.codofi  = @gb_codofi
      and    b.codope  = @gb_codope
      and    a.enlinea = 1
   End
Else if @gb_opcion = 2
   Begin
      select   transaction_id_rev
      from   dbo.tbl_sce_relacion_ft
      where  codcct             = @gb_codcct
      and    codpro             = @gb_codpro
      and    codesp             = @gb_codesp
      and    codofi             = @gb_codofi
      and    codope             = @gb_codope
      and    transaction_id     = @gb_trxid
   End

End

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_rev_abocar_s03_MS] 
	@cencos CHAR(3),
	@codusr CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   MBP   Proyecto migración Comex.Net (Microsoft) - Se agregan joins para traer cod_prod, cod_trx_fc, y cod_ext
	  en el caso de cuentas BCH (quedaban vacias).
*/
   SET NOCOUNT ON 

   declare @fecpro CHAR(10)
   declare @fecpro2    CHAR(10)

   CREATE Table #tmp_rev_abocar
   (
      numcct         CHAR(15),
      nemcta         CHAR(15),
      fecmov         Datetime,
      nroimp         NUMERIC(3,0),
      nomcli         VARCHAR(30),
      cod_dh         CHAR(1),
      moneda         CHAR(3),
      codmon         NUMERIC(3,0),
      mtomcd         NUMERIC(15,2),
      operacion      CHAR(15),
      nrorpt         NUMERIC(8,0),
      enlinea        NUMERIC(1,0),
      estado         NUMERIC(2,0),
      codcct         CHAR(3),
      codpro         CHAR(2),
      codesp         CHAR(2),
      codofi         CHAR(3),
      codope         CHAR(5),
      codfun         NUMERIC(3,0),
      trx_id         CHAR(27),
      cod_prod       CHAR(4),
      cod_trx_fc     CHAR(3),
      cod_ext        CHAR(5),
      cod_swf        CHAR(3),
      data1          CHAR(35),
      data2          CHAR(35),
      data3          CHAR(35),
      data4          CHAR(35),
      data5          CHAR(35),
      cod_trx_cosmos CHAR(3),
      tip_cta        CHAR(4)      
   )

   select   @fecpro = convert(CHAR(10),GetDate(),101)

   Insert Into #tmp_rev_abocar
   Select  a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            ISNULL(convert(VARCHAR,b.nomcli),''''),
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            ISNULL(b.codfun,0),
            d.transaction_id,
            f.cod_prod,
            f.cod_trx_fc,
            f.cod_ext,
            f.cod_swf,
            c.data1,
            c.data2,
            c.data3,
            c.data4,
            c.data5,
            f.cod_trx_cosmos,
            ''CITI''
   from  dbo.sce_mcd a,
          dbo.sce_mch b,
          dbo.tbl_sce_tranope_ft c,
          dbo.tbl_sce_relacion_ft d,
          dbo.tbl_sce_parametros_ft e,
          dbo.tbl_sce_codtran_ft f,
          dbo.sgt_mnd g
   where a.enlinea = 1
   and   a.cencos  = @cencos
   and   a.codusr  = @codusr
   and   a.fecmov  = @fecpro
   and	  a.nemcta  = e.tipo_ft
   and   a.codcct  = b.codcct
   and   a.codpro  = b.codpro
   and   a.codesp  = b.codesp
   and   a.codofi  = b.codofi
   and   a.codope  = b.codope
   and   a.nrorpt  = b.nrorpt
   and   a.fecmov  = b.fecmov
   and   a.codcct  = c.codcct
   and   a.codpro  = c.codpro
   and   a.codesp  = c.codesp
   and   a.codofi  = c.codofi
   and   a.codope  = c.codope
   and   a.nrorpt  = c.nrorpt
   and   a.fecmov  = c.fecmov
   and   a.cod_dh  = c.cod_dh
   and   a.codcct  = d.codcct
   and   a.codpro  = d.codpro
   and   a.codesp  = d.codesp
   and   a.codofi  = d.codofi
   and   a.codope  = d.codope
   and   a.nroimp  = d.nroimp
   and   c.nro_trx = f.nro_trx
   and   a.codmon  = g.mnd_mndcod
   and   c.nroimp  = d.nroimp
-- and   b.estado <> 9

/*---------------------------------------------
  Realsystems-Código Nuevo-Inicio
  Fecha Modificación 20100902
  Responsable: Pablo Millan V.
  Versión: 1.0
  Descripción : Se omiten inyec. de op. auto.
---------------------------------------------*/
   select   @fecpro2 = convert(CHAR(10),GetDate(),112)


   delete from #tmp_rev_abocar
   where  codcct+codpro+codesp+codofi+codope in(select operacion
   from   dbo.tbl_sce_from_ESB2
   where  fecingreso = @fecpro2)
/*----------------------------------------
  RealSystems - Código Nuevo - Termino
  --------------------------------------*/


   Insert Into #tmp_rev_abocar
   SELECT a.numcct,
            a.nemcta,
            a.fecmov,
            a.nroimp,
            isnull(convert(VARCHAR,b.nomcli),'''') as nomcli,
            a.cod_dh,
            g.mnd_mndswf,
            a.codmon,
            a.mtomcd,
            a.codcct+a.codpro+a.codesp+a.codofi+a.codope as numope,
            a.nrorpt,
            a.enlinea,
            a.estado,
            a.codcct,
            a.codpro,
            a.codesp,
            a.codofi,
            a.codope,
            isnull(b.codfun,0) as codfun,
			c.transaction_id,
            sc.cod_producto_fc,
            sc.cod_transaccion_fc,
            sc.cod_extendido_fc,
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            '''',
            ''BCH''
FROM  dbo.sce_mcd a 
   LEFT OUTER JOIN 
   dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov /*and b.estado  <> 9*/
   LEFT OUTER JOIN
    dbo.tbl_sce_relacion_ft c ON a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope  and a.nroimp =  c.nroimp,
	dbo.sce_ccce e,
	dbo.sce_cctx tx,
	dbo.tbl_sce_sup_codigos sc,
	dbo.sgt_mnd g
   WHERE a.enlinea = 1 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  @fecpro AND a.nemcta  in(''CC$'',''CCE'')
AND  a.codmon  = g.mnd_mndcod
AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')
AND tx.nemtra = 
			(CASE 
				WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''ABONOMN''
				WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''CARGOMN''
				WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''ABONOME''
				WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''CARGOME'' 
				ELSE ''''
			END) 
AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE 
				WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''77''
				WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''28''
				WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''''
				WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN '''' 
				ELSE ''''
			END)

   select   numcct     ,
           nemcta     ,
           fecmov     ,
           nroimp     ,
           nomcli     ,
           cod_dh     ,
           moneda     ,
           codmon     ,
           mtomcd     ,
           operacion  ,
           nrorpt     ,
           enlinea    ,
           estado     ,
           codcct     ,
           codpro     ,
           codesp     ,
           codofi     ,
           codope     ,
           codfun     ,
           trx_id     ,
           cod_prod   ,
           cod_trx_fc ,
           cod_ext    ,
           cod_swf    ,
           data1      ,
           data2      ,
           data3      ,
           data4      ,
           data5      ,
           cod_trx_cosmos,
           tip_cta
   from  #tmp_rev_abocar
   order by nrorpt,nroimp

   Drop Table #tmp_rev_abocar
    
End


' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_rev_abocar_u01_MS] 
	@gb_codcct  VARCHAR(3),
	@gb_codpro  VARCHAR(2),
	@gb_codesp  VARCHAR(2),
	@gb_codofi  VARCHAR(3),
	@gb_codope  VARCHAR(5),
	@gb_trxid   VARCHAR(27),
	@ls_codigo  VARCHAR(3)    OUTPUT,
	@ls_mensaje VARCHAR(250)  OUTPUT 

AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @ln_new_trxid NUMERIC(2,0),@ln_trxid_rev NUMERIC(2,0),@lc_new_trxid VARCHAR(27)
        
   select   @ln_new_trxid = convert(NUMERIC(2,0),max(SUBSTRING(transaction_id,26,2)))
   from   dbo.tbl_sce_relacion_ft
   where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope

-- select   @ln_trxid_rev = isnull(convert(NUMERIC(2,0),max(SUBSTRING(transaction_id_rev,26,2))),0) --Akzio migracion
   select   @ln_trxid_rev = isnull(convert(NUMERIC(2,0),case max(SUBSTRING(transaction_id_rev,26,2)) when '''' then ''0'' else max(SUBSTRING(transaction_id_rev,26,2)) end), 0)
   from   dbo.tbl_sce_relacion_ft
   where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope

   select   @lc_new_trxid = SUBSTRING(transaction_id,1,25)
   from   dbo.tbl_sce_relacion_ft
   where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope

   If @ln_new_trxid > @ln_trxid_rev
      select   @ln_new_trxid = @ln_new_trxid+1
Else
   select   @ln_new_trxid = @ln_trxid_rev+1 

   If @ln_new_trxid <= 9
      select   @lc_new_trxid = @lc_new_trxid+''0''+convert(VARCHAR(2),@ln_new_trxid)
Else
   select   @lc_new_trxid = @lc_new_trxid+convert(VARCHAR(2),@ln_new_trxid)

BEGIN TRAN

   update dbo.tbl_sce_relacion_ft set transaction_id_rev = @lc_new_trxid  
   where  codcct             = @gb_codcct
   and    codpro             = @gb_codpro
   and    codesp             = @gb_codesp
   and    codofi             = @gb_codofi
   and    codope             = @gb_codope
   and    transaction_id     = @gb_trxid
   If (@@rowcount = 0)
   Begin
      ROLLBACK 
      Select   @ls_codigo = ''E01''
      Select   @ls_mensaje = ''pro_sce_rev_abocar_u01. Problemas al actualizar registros en tabla tbl_sce_relacion_ft.''
      Return
   End

   COMMIT TRAN

   Select   @ls_codigo = ''E00''
   Select   @ls_mensaje = ''pro_sce_rev_abocar_u01. Actualizacion de registro Exitoso en tabla tbl_sce_relacion_ft.''

End

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_rev_abocar_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rev_abocar_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_rev_abocar_u02_MS] 
	@gb_codcct  CHAR(3),
	@gb_codpro  CHAR(2),
	@gb_codesp  CHAR(2),
	@gb_codofi  CHAR(3),
	@gb_codope  CHAR(5),
	@gb_fecmov  datetime,
	@gb_nrorpt  NUMERIC(8,0),
	@gb_nroimp  NUMERIC(3,0),
	@ls_codigo  CHAR(3)    OUTPUT,
	@ls_mensaje CHAR(250)  OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   BEGIN TRAN
   update dbo.sce_mcd set enlinea = 0  where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope
   and    fecmov = @gb_fecmov
   and    nrorpt = @gb_nrorpt
   and    nroimp = @gb_nroimp
   If (@@rowcount = 0)
   Begin
      ROLLBACK 
      Select   @ls_codigo = ''E01''
      Select   @ls_mensaje = ''pro_sce_rev_abocar_u02. Problemas al actualizar registros en tabla sce_mcd.''
      Return
   End
            
   update dbo.tbl_sce_cvd_ft set valida_iny = 0  where  codcct = @gb_codcct
   and    codpro = @gb_codpro
   and    codesp = @gb_codesp
   and    codofi = @gb_codofi
   and    codope = @gb_codope
   --If (@@rowcount = 0)
   --Begin
   --   ROLLBACK 
   --   Select   @ls_codigo = ''E02''
   --   Select   @ls_mensaje = ''pro_sce_rev_abocar_u02. Problemas al actualizar registros en tabla sce_mcd.''
   --   Return
   --End

   COMMIT TRAN

   Select   @ls_codigo = ''E00''
   Select   @ls_mensaje = ''pro_sce_rev_abocar_u02. Actualizacion de registro Exitoso en tabla sce_mcd.''
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_sup_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_sup_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_sup_s04_MS] 
	@gb_codcct              CHAR(3),
    @gb_codpro              CHAR(2),
    @gb_codesp              CHAR(2),
    @gb_codofi              CHAR(3),
    @gb_codope              CHAR(5) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:10:10 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   BEGIN
    select convert(int,max(substring(transaction_id,26,2)))+1
    from   tbl_sce_relacion_ft
    where  codcct = @gb_codcct
    and    codpro = @gb_codpro
    and    codesp = @gb_codesp
    and    codofi = @gb_codofi
    and    codope = @gb_codope

   END
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_trxcor_ft_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_trxcor_ft_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_trxcor_ft_MS] 
    @ls_retorno CHAR(3)         OUTPUT,
    @ls_mensaje CHAR(250)       OUTPUT,
	@li_corre   INT     	    OUTPUT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare
   @fec_hoy    datetime,@fec_cor    datetime,@corre      INT
BEGIN TRAN

   select   @ls_mensaje = ''''
   select   @fec_hoy = GetDate()
    
   select   @fec_cor = fecha,
            @corre   = correlativo
   from    tbl_sce_trxcor_ft WITH (holdlock)
    ---*IR67100 arroja error en aplicacion Comex al tratar de reversar abonos o cargos en las cuentas corrientes (Se corrige inconveniente, en revisión final)
    --if convert(char(10),@fec_cor,101) < convert(char(10),@fec_hoy,101)
    if convert(char(10),@fec_cor,112) < convert(char(10),@fec_hoy,112)

	begin
      select   @li_corre = 1
      update tbl_sce_trxcor_ft set fecha       = @fec_hoy,correlativo = @li_corre
      if (@@error <> 0 or @@rowcount = 0)
      begin
         ROLLBACK 
         select   @ls_retorno = ''E01''
         select   @ls_mensaje = ''pro_sce_trxcor_ft : [E01] Error al Actualizar Fecha y Correlativo de TrxId.''
         return  1
      end
   end
else
   begin
      select   @corre      = @corre+1
      select   @li_corre   = @corre
      update tbl_sce_trxcor_ft set correlativo = @li_corre
      if (@@error <> 0 or @@rowcount = 0)
      begin
         ROLLBACK 
         select   @ls_retorno = ''E02''
         select   @ls_mensaje = ''pro_sce_trxcor_ft : [E02] Error al Actualizar Correlativo de TrxId.''
         return  2
      end
   end

   COMMIT TRAN
END

' 
END

/****** Object:  StoredProcedure [dbo].[pro_sce_xdec_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_xdec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_xdec_s01_MS] 
	@numdec VARCHAR(7) ,
	@fecdec VARCHAR(10),
	@codadn NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   numdec,
            fecdec,
            codadn,
            cencos,
            codusr
   from    dbo.sce_xdec
   where   numdec =  @numdec
   and     (@fecdec in(fecdec) OR @fecdec IS NULL)
   and     (@codadn in(codadn) OR @codadn IS NULL)
    
End

' 
END


/****** Object:  StoredProcedure [dbo].[proc_sw_env_trae_env_rango]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sw_env_trae_env_rango]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[proc_sw_env_trae_env_rango] @p_casilla INT,
	@p_fecha1 datetime,
	@p_fecha2 datetime 

AS
BEGIN
-- This procedure was converted on Thu Apr 17 16:46:46 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   select df.fecha_envio, df.id_mensaje
   into #tmp from dbo.sw_msgsend_detfile df
   where df.fecha_envio >= dateadd(dd,0,@p_fecha1)
   and df.fecha_envio <  dateadd(dd,+1,@p_fecha2)
   ORDER BY df.fecha_envio
  
   if @p_casilla = 0
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
      FROM 	dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
#tmp df
      WHERE   df.id_mensaje      = m.id_mensaje AND m.estado_msg       = ''ENV''
	  --20150413 order by
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
      FROM 	 dbo.sw_msgsend m LEFT OUTER JOIN dbo.sw_tipos_msg t ON m.tipo_msg = t.cod_tipo LEFT OUTER JOIN dbo.sw_casillas c ON m.casilla = c.cod_casilla LEFT OUTER JOIN dbo.sw_bancos b ON m.cod_banco_rec = b.cod_banco and m.branch_rec = b.branch LEFT OUTER JOIN dbo.sw_monedas d ON m.cod_moneda = d.cod_moneda_sw, 
#tmp df
      WHERE   df.id_mensaje     =  m.id_mensaje AND m.casilla         =  @p_casilla AND m.estado_msg      =  ''ENV''
	  --20150413 order by
	  order by m.id_mensaje,df.fecha_envio
   end
END




' 
END

/****** Object:  StoredProcedure [dbo].[rce_memg_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rce_memg_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[rce_memg_s01_MS] 
	@codtab      VARCHAR(2),
	@codmem      NUMERIC(8,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   if @codtab = ''x''
   begin
      select   linmem from dbo.rce_memx where codmem = @codmem
      order by corlin
   end
   if @codtab = ''s''
   begin
      select   linmem from dbo.rce_mems where codmem = @codmem
      order by corlin
   end
   if @codtab = ''i''
   begin
      select   linmem from dbo.rce_memi where codmem = @codmem
      order by corlin
   end
   if @codtab = ''jm''
   begin
      select   linmem from dbo.rce_jmem where codmem = @codmem
      order by corlin
   end
   if @codtab = ''jd''
   begin
      select   linmem from dbo.rce_jdme where codmem = @codmem
      order by corlin
   end
   if @codtab = ''y''
   begin
      select   linmem from dbo.rce_memy where codmem = @codmem
      order by corlin
   end
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_abr_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_abr_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_abr_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON
   
   SELECT 
		cod_abr,
		nom_abr
   FROM 
		sce_abr
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_acon_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_acon_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_acon_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   codacon,
		desacon
   from 	dbo.sce_acon 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_acr_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_acr_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_acr_s05_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select
		acr_bco, 
		acr_mda, 
		acr_swf
   from dbo.sce_acr
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_adn_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_adn_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_adn_s01_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	
   select 
	   [codadn]
      ,[nomadn] 
   from dbo.sce_adn
   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_anu_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_anu_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_anu_u03_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   

   declare @nrocan         TINYINT,@codanu         CHAR(6),@nrorpt         INT,@fecmov         datetime,
   @mtocan         NUMERIC(15,2),@mtodis         NUMERIC(15,2),
   @totcan         TINYINT,@estado         TINYINT,@numdec         CHAR(7),
   @fecdec         datetime,@codadn         INT,@valret1c       NUMERIC(15,2),
   @valcom1c       NUMERIC(15,2),@valgas1c       NUMERIC(15,2),
   @valliq1c       NUMERIC(15,2),@valfle1c       NUMERIC(15,2),
   @valseg1c       NUMERIC(15,2),@valret2c       NUMERIC(15,2),@valcom2c       NUMERIC(15,2),
   @valgas2c       NUMERIC(15,2),@valliq2c       NUMERIC(15,2),
   @valfle2c       NUMERIC(15,2),@valseg2c       NUMERIC(15,2) 
/************************************/
/*Real Systems Ltda *****************/
/*Se declara variable para eliminar**/
/*registro en tabla fts**************/
   declare @lc_contract NUMERIC(10,0)
/****FIN*****************************/


   declare cursor_xdec cursor for 
   select  numdec,
		fecdec,
		codadn,
		valret1c,
		valcom1c,
		valgas1c,
		valliq1c,
		valfle1c,
		valseg1c,
		valret2c,
		valcom2c,
		valgas2c,
		valliq2c,
		valfle2c,
		valseg2c
   from dbo.sce_xdep where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
BEGIN TRAN
	
	-- Si no existe la Operación => Cancelar.-
   if not exists(SELECT TOP 1 1 from dbo.sce_cvd where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope)
   begin
      ROLLBACK 
      Select   -1, ''No existe la Operacion de Compra-Venta.''
      return
   end
	
	-- Se marcan anulados los Cheques.-
   update dbo.sce_chq set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
		
	-- Se marcan anulados los V. Vistas.-
   update dbo.sce_vvi set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

        -- POLY Se marcan como "anulados" los SWIFT generados en tabla sce_mts
   update dbo.sce_mts set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope                                           
		
	-- Se marcan anulados los Swift''s.-
   update dbo.sce_swf set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
		
	-- Se marca anulado el encabezado del Reporte Contable.-
   update dbo.sce_mch set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
	       
	-- Se marca anulado el detalle del Reporte Contable.-
   update dbo.sce_mcd set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

	-- Se marcan anuladas las Planillas Visibles Export.-
   update dbo.sce_xplv set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
	       
	-- Se marcan anuladas las Planillas Invisibles.-
   update dbo.sce_pli set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope


	-- Se marcan anuladas las Planillas Anuladas.-
   update dbo.sce_xanu set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope                        
	
	-- Cursor que identifica los rebajes de xdec.-
	
	
	-- Se abre el cursor y se especifica las variables.-
   open cursor_xdec
   fetch cursor_xdec into
   @numdec,@fecdec,@codadn,@valret1c,@valcom1c,@valgas1c,@valliq1c,@valfle1c,
   @valseg1c,@valret2c,@valcom2c,@valgas2c,@valliq2c,@valfle2c,@valseg2c
	       
	-- Se recorre el cursor para devolver montos.-
   while @@FETCH_STATUS != -1
   begin
	  
	  -- Se verifica error en el cursor.-
      if (@@FETCH_STATUS = -2)
      begin
         ROLLBACK 
         Select   -1, ''Error al recorrer el cursor.''
         return
      end
	  
	  -- Se devuelve el monto del Exp-1 a la Declaracion.-
      update dbo.sce_xdec set valret1c = valret1c -@valret1c,valcom1c = valcom1c -@valcom1c,valgas1c = valgas1c -@valgas1c,
      valliq1c = valliq1c -@valliq1c,valfle1c = valfle1c -@valfle1c,
      valseg1c = valseg1c -@valseg1c  where
      numdec = @numdec and
      fecdec = @fecdec and
      codadn = @codadn

	  -- Se devuelve el monto del Exp-2 a la Declaracion.-
      update dbo.sce_xdec set valret2c = valret2c -@valret2c,valcom2c = valcom2c -@valcom2c,valgas2c = valgas2c -@valgas2c,
      valliq2c = valliq2c -@valliq2c,valfle2c = valfle2c -@valfle2c,
      valseg2c = valseg2c -@valseg2c  where
      numdec = @numdec and
      fecdec = @fecdec and
      codadn = @codadn

	  -- Se accesa el proximo registro del cursor
      fetch cursor_xdec into
      @numdec,@fecdec,@codadn,@valret1c,@valcom1c,@valgas1c,@valliq1c,@valfle1c,
      @valseg1c,@valret2c,@valcom2c,@valgas2c,@valliq2c,@valfle2c,@valseg2c
   end

	-- Se cierra el cursor.-
   close cursor_xdec
   deallocate cursor_xdec

	-- Se marcan el nuevo estado de la Compra/Venta (Anulado).-
   update dbo.sce_cvd set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

	-- Se verifica la existencia de errores.-
   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al Anular una Compra-Venta de Divisas.''
      return
   end
    
    -- Se comenta lo siguiente ya que se desea poder reversar una operación anulada, y la información de tbl_sce_fts, tbl_sce_relacion_ft y tbl_sce_tranope_ft es necesaria para la reversa
/*
   if @codpro = ''30''
   Begin
      select   @lc_contract = contract_reference
      from   dbo.tbl_sce_relacion_ft
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
      delete from dbo.tbl_sce_fts
      where  contract_reference = @lc_contract
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al Eliminar Registro en tabla FTS''
         return
      end
      
	  delete from dbo.tbl_sce_relacion_ft
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_relacion_ft''
         return
      end
      delete from dbo.tbl_sce_tranope_ft
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_relacion_ft''
         return
      end
	  
   End */
    -- Fin Elimina Registro FTS    
    
   COMMIT TRAN

   Select   0, ''Grabacion Exitosa''
   return

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_anu_u12_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_anu_u12_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_anu_u12_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   declare	@numpla		NUMERIC(10,0),@numdec		CHAR(18),@fecdec		SMALLDATETIME,@mtofob		NUMERIC(15,2),
   @mtofle		NUMERIC(15,2),@mtoseg		NUMERIC(15,2),@fobmer		NUMERIC(15,2),
   @flemer		NUMERIC(15,2),@segmer		NUMERIC(15,2),@mtocif		NUMERIC(15,2),
   @pardec		NUMERIC(17,10),@cubcob		NUMERIC(15,2),@cubotr		NUMERIC(15,2),
   @rescub		NUMERIC(15,2),@fecanu		SMALLDATETIME,@indanula	NUMERIC(1,0)
/************************************/
/*Real Systems Ltda *****************/
/*Se declara variable para eliminar**/
/*registro en tabla fts**************/
   declare @lc_contract NUMERIC(10,0)
/****FIN*****************************/



   declare cursor_xdec cursor for 
   select	numpla,
		numdec,
		fecdec,
		mtofob,
		mtofle,
		mtoseg,
		fobmer,
		flemer,
		segmer,
		mtocif,
		pardec,
		fecha

   from dbo.sce_reb where
   cent_costo = @codcct and
   id_product = @codpro and
   id_especia = @codesp and
   id_empresa = @codofi and
   id_operac  = @codope
BEGIN TRAN
	
	-- Si no existe la Operaci›n => Cancelar.-
   if not exists(SELECT TOP 1 1 from dbo.sce_cvd where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope)
   begin
      ROLLBACK 
      Select   -1, ''No existe la Operacion de Compra-Venta.''
      return
   end
	
	-- Se marcan anulados los Cheques.-
   update dbo.sce_chq set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
		
	-- Se marcan anulados los V. Vistas.-
   update dbo.sce_vvi set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
		
	-- Se marcan anulados los Swift''s.-
   update dbo.sce_swf set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
		
	-- Se marca anulado el encabezado del Reporte Contable.-
   update dbo.sce_mch set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
	       
	-- Se marca anulado el detalle del Reporte Contable.-
   update dbo.sce_mcd set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope


	-- Se anula tabla ventas visibles import.- sce_vvim
   update dbo.sce_vvim set estado = 9  where
   codcct = @codcct	and
   codpro = @codpro	and
   codesp = @codesp	and
   codofi = @codofi	and
   codope = @codope      


	-- Eliminar sce_grdo
   delete dbo.sce_grdo
   where
   codcct = @codcct        and
   codpro = @codpro        and
   codesp = @codesp        and
   codofi = @codofi        and
   codope = @codope            	


	-- Se marcan anuladas las Planillas Visibles import.-
   update dbo.sce_plan set estado = 9  where
   cent_costo = @codcct and
   id_product = @codpro and
   id_especia = @codesp and
   id_empresa = @codofi and
   id_cobranz = @codope                         

	
	-- Cursor que identifica los rebajes de xdec.-
	
	
	-- Se abre el cursor y se especifica las variables.-
   open cursor_xdec
   fetch cursor_xdec into
   @numpla,@numdec,@fecdec,@mtofob,@mtofle,@mtoseg,@fobmer,@flemer,@segmer,
   @mtocif,@pardec,@fecanu

	      
	-- Se recorre el cursor para devolver montos.-
   while @@FETCH_STATUS != -1
   begin
	  
	  -- Se verifica error en el cursor.-
      if (@@FETCH_STATUS = -2)
      begin
         ROLLBACK 
         Select   -1, ''Error al recorrer el cursor.''
         return
      end
      select   @indanula = 0
      if exists(SELECT TOP 1 1 from dbo.sce_plan
      where   num_presen = @numpla and
      fechaanula = @fecanu and
      hayanula = 1)
      begin
         select   @indanula = indanula
         from 	dbo.sce_plan
         where   num_presen = @numpla and
         fechaanula = @fecanu and
         hayanula = 1
         if (@indanula = 1)
         begin
            update dbo.sce_plan set hayanula   = 0,indanula   = 0,vencanula  = CONVERT(smalldatetime,0), fechaanula = CONVERT(smalldatetime,0),
            paranula   = 0,totalanula = 0,observ     = ''Ope. ''+cent_costo+id_product+id_especia+id_empresa+id_cobranz  where   num_presen = @numpla and
            fechaanula = @fecanu and
            hayanula = 1
         end
         if (@indanula = 4)
         begin
            update dbo.sce_plan set hayanula   = 0,indanula   = 3,vencanula  = CONVERT(smalldatetime,0), fechaanula = CONVERT(smalldatetime,0),
            paranula   = 0,totalanula = 0,observ     = ''Ope. ''+cent_costo+id_product+id_especia+id_empresa+id_cobranz+'' Planilla de transferencia''  where   num_presen = @numpla and
            fechaanula = @fecanu and
            hayanula = 1
         end
      end
      if @numdec <> '''' and @pardec > 0
      begin
         if @indanula <> 1 and @indanula <> 4
         begin
                                                       
	  	-- Se devuelve el monto de la declaraci¢n.-                     
            update dbo.sce_dec set cubfob = cubfob -(@mtofob/@pardec),cubfle = cubfle -(@mtofle/@pardec),
            cubseg = cubseg -(@mtoseg/@pardec),cubmer =(cubmer -((@fobmer+@flemer+@segmer)/@pardec)),
            cubcif = cubcif -(@mtocif/@pardec),
            relfob =(relfob -((@mtofob -@fobmer)/@pardec)),relfle =(relfle -((@mtofle -@flemer)/@pardec)),
            relseg =(relseg -((@mtoseg -@segmer)/@pardec)),
            relmer =(relmer -((@fobmer+@flemer+@segmer)/@pardec)),
            relcif =(relcif -(@mtocif/@pardec)),disfob = orifob -(relfob -((@mtofob -@fobmer)/@pardec)),
            disfle = orifle -(relfle -((@mtofle -@flemer)/@pardec)),
            disseg = oriseg -(relseg -((@mtoseg -@segmer)/@pardec)),
            discif = oricif -(relcif -(@mtocif/@pardec)),dismer = mtomer -(relmer -((@fobmer+@flemer+@segmer)/@pardec))  where
            numdec = @numdec and
            fecdec = convert(datetime,@fecdec)
         end
      else
         begin
            update dbo.sce_dec set relfob = relfob+round(@mtofob/@pardec,2),cubfob = cubfob+round(@mtofob/@pardec,2),
            disfob = disfob -round(@mtofob/@pardec,2),relfle = relfle+round(@mtofle/@pardec,2),
            cubfle = cubfle+round(@mtofle/@pardec,2),
            disfle = disfle -round(@mtofle/@pardec,2),relseg = relseg+round(@mtoseg/@pardec,2),
            cubseg = cubseg+round(@mtoseg/@pardec,2),disseg = disseg -round(@mtoseg/@pardec,2),
            relcif = relcif+round(@mtocif/@pardec,2),
            cubcif = cubcif+round(@mtocif/@pardec,2),discif = discif -round(@mtocif/@pardec,2)  where   numdec = @numdec and
            fecdec = convert(datetime,@fecdec)
         end
      end                                          

	  -- Se accesa el proximo registro del cursor
      fetch cursor_xdec into
      @numpla,@numdec,@fecdec,@mtofob,@mtofle,@mtoseg,@fobmer,@flemer,@segmer,
      @mtocif,@pardec,@fecanu
   end

	-- Se cierra el cursor.-
   close cursor_xdec
   deallocate cursor_xdec

	-- Se marcan el nuevo estado de la Compra/Venta (Anulado).-
   update dbo.sce_cvd set estado = 9  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

	-- Se verifica la existencia de errores.-
   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al Anular una Compra-Venta de Divisas.''
      return
   end
   
       -- Se comenta lo siguiente ya que se desea poder reversar una operación anulada, y la información de tbl_sce_fts, tbl_sce_relacion_ft y tbl_sce_tranope_ft es necesaria para la reversa
/*
   if @codpro = ''30''
   Begin
      select   @lc_contract = contract_reference
      from   dbo.tbl_sce_relacion_ft
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
      delete from dbo.tbl_sce_fts
      where  contract_reference = @lc_contract
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_fts''
         return
      end
      
	  delete from dbo.tbl_sce_relacion_ft
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_relacion_ft''
         return
      end
      delete from dbo.tbl_sce_tranope_ft
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al Eliminar Registro en tabla tbl_sce_tranope_ft''
         return
      end
	  
   End */
    -- Fin Elimina Registro FTS    
    
    
    
   COMMIT TRAN
   Select   0, ''Grabacion Exitosa''
   return

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_arb_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_arb_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_arb_i01_MS] 
	@codcct         CHAR(3)        ,
	@codpro         CHAR(2)        ,
	@codesp         CHAR(2)        ,
	@codofi         CHAR(3)        ,
	@codope         CHAR(5)        ,
	@nrocor         NUMERIC(2,0)     ,
	@estado         NUMERIC(2,0)     ,
	@codpai         NUMERIC(3,0)     ,
	@mndcom         NUMERIC(3,0)     ,
	@mndvta         NUMERIC(3,0)     ,
	@mtocom         NUMERIC(15,2)   ,
	@mtovta         NUMERIC(15,2)   ,
	@prdarb         NUMERIC(17,10)  ,
	@tipcam         NUMERIC(11,4)   ,
	@mtodol         NUMERIC(15,2)   ,
	@mtopes         NUMERIC(15,2)   ,
	@conven         BIT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   insert into dbo.sce_arb values(@codcct ,
		@codpro ,
		@codesp ,
		@codofi ,
		@codope ,
		@nrocor ,
		@estado ,
		@codpai ,
		@mndcom ,
		@mndvta ,
		@mtocom ,
		@mtovta ,
		@prdarb ,
		@tipcam ,
		@mtodol ,
		@mtopes ,
		@conven)
	
   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   return 0
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_aut_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_aut_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_aut_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   codaut,
		desaut
   from 	dbo.sce_aut 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_bco_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bco_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_bco_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	SELECT 
		[codbco]
		,[nombco]
	FROM [dbo].[sce_bco]

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_bcta_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bcta_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_bcta_i01_MS] 
	@id_party		CHAR(12),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@activa			BIT,
	@moneda			NUMERIC(3,0),
	@cuenta			VARCHAR(25),
	@especial		BIT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON
   
   INSERT INTO 
	sce_bcta(id_party,
			 secuencia,
			 borrado,
			 activa,
			 moneda,
			 cuenta,
			 especial) 
	 VALUES (@id_party,
			 @secuencia,
			 @borrado,
			 @activa,
			 @moneda,
			 @cuenta,
			 @especial)
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_bcta_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bcta_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_bcta_s01_MS] 
	@id_party         CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON
   
   	SELECT 
		id_party,
		secuencia,
		borrado,
		activa,
		moneda,
		cuenta,
		especial
	FROM 
		sce_bcta
	WHERE 
		id_party = @id_party 
	ORDER BY 
		cuenta
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_bcta_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bcta_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_bcta_u01_MS]
	@id_party		CHAR(12),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@activa			BIT,
	@moneda			NUMERIC(3,0),
	@cuenta			VARCHAR(25),
	@especial		BIT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON
   
   	UPDATE sce_bcta 
	SET 
		id_party	= @id_party,
		secuencia	= @secuencia,
		borrado		= @borrado,
		activa		= @activa,
		moneda		= @moneda,
		cuenta		= @cuenta,
		especial	= @especial 
	WHERE 
		id_party	= @id_party AND 
		secuencia	= @secuencia	
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_bic_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bic_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_bic_s02_MS]
	@bicswf         CHAR(8),
	@bicsec         CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   SELECT [bic_swf]
      ,[bic_sec]
      ,[bic_nom]
      ,[bic_des]
      ,[bic_ciu]
      ,[bic_dir]
      ,[bic_pos]
      ,[bic_pai]
      ,[bic_ala]
      ,[bic_cod]
  FROM [dbo].[sce_bic]
  WHERE
   bic_swf = @bicswf and
   bic_sec = @bicsec

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_bic_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_bic_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_bic_s07_MS] 
	@LlaveIng	CHAR(11)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
						 Se reemplaza where "bic_swf + bic_sec = @LlaveIng" por substrings
*/
   SET NOCOUNT ON
   
   	SELECT 
		bic_swf + bic_sec 
	FROM 
		sce_bic 
	WHERE 
		bic_swf = SUBSTRING(@LlaveIng,1,8)
	and	bic_sec = SUBSTRING(@LlaveIng,9,3)

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_blin_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_blin_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_blin_i01_MS] 
	@id_party		CHAR(12),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@activa			BIT,
	@moneda			NUMERIC(3,0),
	@linea			CHAR(5)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	INSERT INTO 
	sce_blin(id_party,
			 secuencia,
			 borrado,
			 activa,
			 moneda,
			 linea) 
	 VALUES (@id_party,
			 @secuencia,
			 @borrado,
			 @activa,
			 @moneda,
			 @linea)
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_blin_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_blin_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_blin_s01_MS] 
	@id_party         CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	SELECT  
		id_party
		,secuencia
		,borrado
		,activa
		,moneda
		,linea
	FROM 
		sce_blin
	WHERE 
		id_party = @id_party 
	ORDER BY 
		linea
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_blin_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_blin_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_blin_u01_MS]
	@id_party		CHAR(12),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@activa			BIT,
	@moneda			NUMERIC(3,0),
	@linea			CHAR(5)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	UPDATE sce_blin 
	SET 
		id_party	= @id_party,
		secuencia	= @secuencia,
		borrado		= @borrado,
		activa		= @activa,
		moneda		= @moneda,
		linea		= @linea
	WHERE 
		id_party	= @id_party AND 
		secuencia	= @secuencia	
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ccde_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ccde_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ccde_s01_MS] 
	@clave CHAR(1) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   valor, signo
   from	dbo.sce_ccde
   where	clave = @clave

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ccof_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ccof_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ccof_s01_MS] 
	@ccosto   CHAR(3)      
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   oficon,
           glsofi
   from dbo.sce_ccof
   where @ccosto = ccosto                           
                                                    
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ccpl_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ccpl_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ccpl_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   codcom,
		cptcom,
		descom,
		tipope,
		flging,
		rutint,
		nomint,
		dirint,
		codpai,
		mtopss,
		dataut,
		operel,
		numins,
		fecins,
		finext,
		vtocic,
		fecdes,
		mondes,
		mtodes,
		impadc,
		decexp,
		infexp,
		vtoret,
		mtoexp,
		vtofin,
		nomcom,
		infimp,
		decimp,
		codfdp,
		conemb,
		vtoope,
		mtoimp,
		datint,
		datder,
		acuaco,
		codccr,
		observ
   from 	dbo.sce_ccpl
   order by codcom,cptcom

   return 
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_chq_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_chq_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_chq_s03_MS] @cctsup         CHAR(3),
	@usrsup         CHAR(2),
	@fecemi         datetime 
AS
begin
-- This procedure was converted on Wed Apr 16 15:26:26 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   if @usrsup = ''''
   begin
      select   codcct, codpro, codesp, codofi, codope,
         	nrocor, estado, nrofol, monswf,
        	mtochq, nomben, nompag, dirpag, swfpag,
        	ciupag, paipag, numcta, nomcli
      from dbo.sce_chq where
      cctsup = @cctsup and
      fecemi = @fecemi and
      estado <> 9 	/* Se agrega el estado <> 9*/
      return
   end
else
   begin
      select   codcct, codpro, codesp, codofi, codope,
		nrocor, estado, nrofol, monswf,
		mtochq, nomben, nompag, dirpag, swfpag,
		ciupag, paipag, numcta, nomcli
      from dbo.sce_chq where
      cctsup = @cctsup and
      usrsup = @usrsup and
      fecemi = @fecemi and
      estado <> 9	/* Se agrega el estado <> 9*/
      return
   end
   return
end


/****** Object:  StoredProcedure [dbo].[sce_grio_s01_MS]    Script Date: 30/09/15 18:08:11 ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[sce_grio_s01_MS]'') AND type in (N''P'', N''PC''))
DROP PROCEDURE [dbo].[sce_grio_s01_MS]

' 
END

/****** Object:  StoredProcedure [dbo].[sce_chq_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_chq_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_chq_u01_MS] @codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@nrocor      NUMERIC(3,0),
	@nrofol      NUMERIC(10,0),
	@estado      NUMERIC(1,0) 
AS
BEGIN
BEGIN TRAN
-- This procedure was converted on Wed Apr 16 15:44:44 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).


   update dbo.sce_chq set estado = @estado,nrofol = @nrofol  where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   nrocor = @nrocor

   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al actualizar estado y numero de folio en Sce_Chq.''
      return
   end
   COMMIT TRAN
   Select   0,''Grabacion Exitosa''
   return
   return
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_chq_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_chq_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_chq_u02_MS]
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@codanu      CHAR(6),
	@estado      NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


begin tran

   update dbo.sce_chq set estado = @estado 
   where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   codanu = @codanu  
   if (@@error <> 0 or @@rowcount = 0)
   begin
      rollback 
      return 9
   end
else
   begin
      declare @res1    NUMERIC(2,0),@monswf  CHAR(3),@mtochq  NUMERIC(15,2),@fecemi  datetime,
      @esppro  CHAR(6),@espown  CHAR(6),@codofin NUMERIC(3,0),@opesin  CHAR(15),
      @codmnd  NUMERIC(3,0),@nomben  VARCHAR(70),@nompag  VARCHAR(40),
      @swfpag  VARCHAR(13),@numcta  VARCHAR(15),@nomcli  VARCHAR(40)
	
	   -- Cursor que identifica los cheques.-                          
      declare cursor_chq cursor for
      select  monswf,
        	   mtochq,
	           fecemi,
		   nomben,
		   nompag,
		   swfpag,
		   numcta,
		   nomcli
      from dbo.sce_chq where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      codanu = @codanu 
	   -- Se abre el cursor y se especifica las variables.-
      open cursor_chq
      fetch cursor_chq into
      @monswf,@mtochq,@fecemi,@nomben,@nompag,@swfpag,@numcta,@nomcli  
	   -- Se recorre el cursor para devolver montos.-
      while @@FETCH_STATUS != -1
      begin
 	      -- Se verifica error en el cursor.-
         declare @var1      datetime
         declare @esppron   NUMERIC(5,0)
         if (@@FETCH_STATUS = -2)
         begin
            ROLLBACK 
            return 9
         end

  	      -- Va a buscar en la tabla de monedas el codigo.-
         select   @codmnd =(select min(mnd_mndcod) from
            dbo.sgt_mnd where mnd_mndnmc = @monswf)
                                                  
                                                                                
              -- Se asignan los datos a grabar.-
         select   @var1    = GetDate()
         select   @esppro  = @codcct+@codesp
         select   @espown  = @codcct+@codesp
                          -- codpro
                                          -- anulac
                                      -- codfun
                                      -- subproe
                                    -- codcct
                                    -- codpro
                                    -- codesp
                                    -- codofi 
                                    -- codope
                                          -- codneg                           
                                          -- tippro                           
                                          -- numcor                           
                                          -- numcuo                           
                                          -- nrocan                           
                                    -- fecpro                           
                                          -- codplan                          
                                          -- cerend                           
                                          -- reldi                            
                                          -- aprobii                          
                                          -- acelet                           
                                         -- filler6                          
                                         -- filler7                          
                                         -- filler8                          
                                         -- filler9                          
                                         -- filler10                         
                        	            -- mndfun1                          
                                          -- mtofun1                          
                                          -- mndfun2                          
                                          -- mtofun2                          
                                          -- mndfun3                          
                                          -- mtofun3                          
                                          -- mndfun4                          
                                          -- mtofun4                          
                                    -- moneda                           
                                    -- monto                            
                                          -- mndint                           
                                          -- mtoint                           
                                          -- mndcom                           
                                          -- mtocomre                         
                                          -- mtocomsi                         
                                          -- mtocomno                         
                                          -- mndimp                           
                                          -- mtoimp                           
                                          -- mndgas
                                          -- mtogas
                                    -- fecfun                           
                               -- fecrec                           
                               -- fecven                           
                               -- fecnul                           
                                    -- esppro                           
                                    -- espown                           
                                   -- oficon                           
                        	            -- rutcli                         
                                    -- nomcli                         
                        	            -- indclin                        
                               	    -- indclid                        
                        	            -- rutben                         
                                    -- nomben                         
                               	    -- indbenn                        
                        	            -- indbend                        
                                        -- numdoc                           
                                    -- fecdoc                           
                                          -- paridad                          
                                          -- tcamtab                          
                                          -- tcamcan                          
                                   -- data
                                    -- swfbco1                          
                                         -- rutbco1                          
                                    -- nomprt1                          
                                  	    -- indprtb1n                        
                                          -- indprtb1d                        
                                         -- swfbco2                          
                                   	    -- nomprt2                          
               	    -- indprtb2n                        
                        	            -- indprtb2d                        
                        	            -- swfbco3                          
                               	    -- swfbco4                          
                                   	    -- regimen                          
                                    	    -- esavis                           
                                    	    -- esconf                           
                                          -- esrstg                           
                                          -- parcial                          
                                          -- discrep                          
                               -- fecemi                           
                               -- fecemb                           
                                          -- tipomod                          
                                          -- totdoc                           
                                          -- tenor                            
                                          -- nrocuot                          
                                          -- limirr                           
                                          -- nuplco                           
                                          -- nuples                           
                                          -- finpag                           
                                         -- codanu                           
                                          -- gastxcta                         
                                          -- mongastx                         
                                          -- mtogastx                         
                                      -- fecest                           
         select   @codofin = convert(NUMERIC(3,0),@codofi)    
              
              -- Se ingresan datos en el Log.-
         EXECUTE @res1 = dbo.sce_gtlg_i01 @codpro,0,''001'',''101'',@codcct,@codpro,@codesp,@codofi,@codope,0,0,0,0,
         0,@fecemi,0,0,0,0,0,'''','''','''','''','''',0,0,0,0,0,0,0,0,@codmnd,@mtochq,0,0,
         0,0,0,0,0,0,0,0,@fecemi,CONVERT(datetime,0),CONVERT(datetime,0),CONVERT(datetime,0),@esppro,
         @espown,@codofin,'''',@nomcli,0,0,'''',@nomben,0,0,''0'',@fecemi,0,0,0,''Cheque'',
         @swfpag,'''',@nompag,0,0,'''','''',0,0,'''','''',0,0,0,0,0,0,CONVERT(datetime,0),
         CONVERT(datetime,0),0,0,0,0,0,0,0,0,'''',0,0,0,@var1,@estado             -- estado                           

         if @res1 <> 0
         begin
            rollback 
            return 9
         end                                                     

	      -- Se accesa el proximo registro del cursor
         fetch cursor_chq into
         @monswf,@mtochq,@fecemi,@nomben,@nompag,@swfpag,@numcta,@nomcli
      end
                                                                        
           -- Se cierra el cursor.-
      close cursor_chq
      deallocate cursor_chq                                           
	   -- Se verifica la existencia de errores.- 
      if (@@error <> 0)
      begin
         rollback 
         return 9
      end
   else
      begin
         commit tran
         return 0
      end
   end
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cor_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cor_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cor_s03_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   select   cor_swf, cor_nom, cor_ciu, cor_dir,
		cor_pos, cor_pai, cor_cpa 
   from dbo.sce_cor 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cou_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cou_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cou_s03_MS]
	@cou_pai        CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

/**************************************************************************/

   select   cou_cod 
   from dbo.sce_cou 
   where cou_pai = @cou_pai
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cov_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cov_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cov_i01_MS] 
	@codcct         CHAR(3)                ,
	@codpro         CHAR(2)                ,
	@codesp         CHAR(2)                ,
	@codofi         CHAR(3)                ,
	@codope         CHAR(5)                ,
	@nrocor         NUMERIC(2,0)             ,
	@estado         NUMERIC(2,0)             ,
	@tipcov         CHAR(1)                ,
	@codpai         NUMERIC(3,0)             ,
	@codmnd         NUMERIC(3,0)             ,
	@mtocov         NUMERIC(15,2)           ,
	@tipcam         NUMERIC(11,4)           ,
	@mtopes         NUMERIC(15,2)           ,
	@mtopar         NUMERIC(17,10)          ,
	@codtcp         CHAR(9)                ,
	@codoci         NUMERIC(3,0)             ,
	@ingegr         CHAR(1)                ,
	@conven         BIT                     ,
	@numdec         CHAR(7)                ,
	@fecdec         datetime                ,
	@codadn         NUMERIC(3,0)             ,
	@dienum         CHAR(7)                ,
	@diefec         datetime                ,
	@diepbc         NUMERIC(2,0)             ,
	@inddec         BIT                     ,
	@fecdeb         datetime                ,
	@docnac         CHAR(15)                ,
	@docext         CHAR(15) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

BEGIN TRAN
   insert into dbo.sce_cov values(@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@nrocor,
		@estado,
		@tipcov,
		@codpai,
		@codmnd,
		@mtocov,
		@tipcam,
		@mtopes,
		@mtopar,
		@codtcp,
		@codoci,
		@ingegr,
		@conven,
		@numdec,
		@fecdec,
		@codadn,
		@dienum,
		@diefec,
		@diepbc,
		@inddec,
		@fecdeb,
		@docnac,
		@docext)
	
   if (@@error <> 0)
   begin
      rollback 
      select   9
   end
else
   begin
      commit tran
      select   0
   end
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cov_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cov_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cov_u01_MS]
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@estado      NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 




   update dbo.sce_cov set estado = @estado  
   where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   return 0
   return
 

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cpai_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cpai_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cpai_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   SELECT   dbo.sce_cpai.cpai_codpaic,
         dbo.sce_cpai.cpai_nompai,
         dbo.sce_cpai.cpai_codentp
   FROM dbo.sce_cpai
   ORDER BY dbo.sce_cpai.cpai_nompai ASC
   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cta_s01_1_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s01_1_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cta_s01_1_MS] 
	@cta_nem        VARCHAR(15) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   cta_nem,
		cta_mon,
		cta_num,
		cta_nom,
		cta_gl,
		cta_nroto,
		cta_indto,
		cta_cit,
		cta_cvt,
		cta_cap,
		cta_ctd,
		cta_pos,
		cta_cdr,
		cta_vigtbl
   from dbo.sce_cta where
   cta_nem = @cta_nem
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cta_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cta_s01_MS]
	@cta_nem        VARCHAR(15) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 




   select   cta_nem,
		cta_mon,
		cta_num,
		cta_nom,
		cta_gl,
		cta_nroto,
		cta_indto,
		cta_cit,
		cta_cvt,
		cta_cap,
		cta_ctd,
		cta_pos,
		cta_cdr
   from dbo.sce_cta 
   where cta_nem = @cta_nem
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cta_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cta_s03_MS]
	@cta_num       CHAR(8) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   select   cta_num 
   from dbo.sce_cta 
   where cta_num = @cta_num
   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cta_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cta_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cta_s06_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   select   cta_nem,
               cta_mon,
               cta_num,
               cta_nom
   from dbo.sce_cta
   where cta_gl = 1 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ctas_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ctas_i01_MS] 
	@id_party		CHAR(12),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@activabco		BIT,
	@activace		BIT,
	@extranjera		BIT,
	@moneda			NUMERIC(3,0),
	@cuenta			CHAR(11)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	INSERT INTO 
	sce_ctas(id_party,
			 secuencia,
			 borrado,
			 activabco,
			 activace,
			 extranjera,
			 moneda,
			 cuenta) 
	 VALUES (@id_party,
			 @secuencia,
			 @borrado,
			 @activabco,
			 @activace,
			 @extranjera,
			 @moneda,
			 @cuenta)
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ctas_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ctas_s03_MS] 
	@id_party       CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
 
   select   cuenta, extranjera, activace, moneda 
   from dbo.sce_ctas 
   where
   id_party = @id_party 	and
   activace = 1		and
   borrado  = 0		 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ctas_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-10   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
CREATE procedure [dbo].[sce_ctas_s04_MS] 
	@id_party       CHAR(12) 
AS
BEGIN
SET NOCOUNT ON 
	 SELECT 
			 [id_party]
			,[secuencia]
			,[borrado]
			,[activabco]
			,[activace]
			,[extranjera]
			,[moneda]
			,[cuenta]
	  FROM 
			[sce_ctas]
	  WHERE
			id_party = @id_party 	
	  ORDER BY 
			cuenta
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ctas_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ctas_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ctas_u01_MS]
	@id_party		CHAR(12),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@activabco		BIT,
	@activace		BIT,
	@extranjera		BIT,
	@moneda			NUMERIC(3,0),
	@cuenta			CHAR(11)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	UPDATE sce_ctas 
	SET 
		id_party	= @id_party,
		secuencia	= @secuencia,
		borrado		= @borrado,
		activabco	= @activabco,
		activace	= @activace,
		extranjera	= @extranjera, 
		moneda		= @moneda,
		cuenta		= @cuenta 
	WHERE 
		id_party	= @id_party AND 
		secuencia	= @secuencia	
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cvd_p02]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_p02]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_cvd_p02]
	@codcct [char](3),
	@codpro [char](2),
	@codesp [char](2),
	@codofi [char](3),
	@codope [char](5),
	@cencos [char](3),
	@codusr [char](2),
	@fecing [datetime],
	@fecact [datetime],
	@tipcvd [numeric](2, 0),
	@estado [numeric](2, 0),
	@operel [char](15),
	@prtcli [char](12),
	@rutcli [char](10),
	@indnomc [numeric](2, 0),
	@inddirc [numeric](2, 0),
	@indpopc [char](2),
	@prtotr [char](12),
	@indnomo [numeric](2, 0),
	@inddiro [numeric](2, 0),
	@indpopo [char](2),
	@moneda [numeric](3, 0),
	@monto [numeric](15, 2),
	@rutcliente [char](10),
	@nombrecliente [char](30)
WITH EXECUTE AS CALLER
AS
BEGIN

-- This procedure was converted on Wed Apr 16 14:39:39 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

print ''------------INICIO-------------''
/************************************************************************/
   declare @oficon_c       CHAR(3),@oficon         NUMERIC(3,0),@filler3        CHAR(3),
   @numreg         NUMERIC(3,0),@data           CHAR(30)
begin tran
   IF not exists(SELECT TOP 1 1 from dbo.sce_cvd
   where 	codcct = @codcct        and
   codpro = @codpro        and
   codesp = @codesp        and
   codofi = @codofi        and
   codope = @codope)
   begin
      insert into dbo.sce_cvd values(@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@cencos,
		@codusr,
		@fecing,
		@fecact,
		@tipcvd,
		@estado,
		@operel,
		@prtcli,
		@rutcli,
		@indnomc,
		@inddirc,
		@indpopc,
		@prtotr,
		@indnomo,
		@inddiro,
		@indpopo)
		
      if (@@rowcount > 0 and @@error = 0)
      begin
			-- Log Comex
			-- Oficina contable
		print ''--------INICIO CAMPO 21---------''
         EXECUTE sce_ccof_s02 @codcct,@oficon_c OUTPUT
         if @oficon_c IS NULL
            select   @oficon_c = @codofi
         select   @oficon = convert(NUMERIC(3,0),@oficon_c)
         if @tipcvd = 2          -- Arbitraje
         begin
            select   @filler3 = ''1''
            select   @data = ''C/V Div con Arbitraj''
         end
      else
         begin
            select   @filler3 = ''0''
            select   @data = ''C/V Div sin Arbitraj''
         end
         insert  into dbo.sce_gtlg(codproe, anulac, codfun, subproe, codcct, codpro,
        		codesp, codofi, codope, moneda, monto, fecpro, filler3, esppro,
        		espown , oficon,rutcli,nomcli,data)
			values(''20'', 0, ''001'', ''000'', @codcct, @codpro,
        		@codesp, @codofi, @codope, @moneda, @monto, GetDate(), @filler3,
        		@codusr, @cencos, @oficon, @rutcliente, @nombrecliente, @data)
			
         if (@@error <> 0 or @@rowcount = 0)
         begin
            rollback 
            return 7
         end
      else
         begin
            commit tran
            select   0
         end
		 print ''--------FIN CAMPO 21---------''
      end
   else
      begin
	  print ''--------ROLLBACK CAMPO 21---------''
         rollback 
         select   8
      end
   end
ELSE
   begin
      update dbo.sce_cvd set codcct  = @codcct,codpro  = @codpro,codesp  = @codesp,codofi  = @codofi,
      codope  = @codope,cencos  = @cencos,codusr  = @codusr,fecing  = @fecing,
      fecact  = @fecact,tipcvd  = @tipcvd,estado  = @estado,operel  = @operel,
      prtcli  = @prtcli,rutcli  = @rutcli,indnomc = @indnomc,inddirc = @inddirc,
      indpopc = @indpopc,prtotr  = @prtotr,indnomo = @indnomo,inddiro = @inddiro,
      indpopo = @indpopo  where	codcct  = @codcct and
      codpro  = @codpro and
      codesp  = @codesp and
      codofi  = @codofi and
      codope  = @codope
      if (@@rowcount > 0 and @@error = 0)
      begin
         commit tran
         select   0
      end
   else
      begin
         rollback 
         select   9
      end
   end

   print ''------------FIN-------------''

   return
END





' 
END

/****** Object:  StoredProcedure [dbo].[sce_cvd_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cvd_s05_MS]
	@codcct  CHAR(3),  
	@codpro  CHAR(2),  
	@codesp  CHAR(2),  
	@codofi  CHAR(3),  
	@codope  CHAR(5)  
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   prtcli,
        indnomc,
        inddirc
   from dbo.sce_cvd
   where   @codcct = codcct and
   @codpro = codpro and
   @codesp = codesp and
   @codofi = codofi and
   @codope = codope              
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cvd_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cvd_s06_MS]
	@codcct    CHAR(3),
	@codpro    CHAR(2),
	@codesp    CHAR(2),
	@codofi    CHAR(3),
	@codope    CHAR(5)  	 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare      @fecing	datetime,@prtcli	CHAR(12),@indnomc	NUMERIC(2,0),@inddirc	NUMERIC(2,0)
   if @codpro = ''05''
   begin
      select   
		@prtcli = prtexp,
		@fecing = fecing
      from dbo.sce_pae
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope

      select   @indnomc = codnom,
               @inddirc = coddir
      from dbo.sce_ppae
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 0

      select   @codcct	,
             @codpro	,
             @codesp	,
             @codofi	,
             @codope	,
             @fecing	,
	     0	,
             @prtcli	,
             @indnomc	,
             @inddirc
   end


   if @codpro = ''03''
   begin
      select   @fecing = fecha_ing
      from dbo.sce_col
      where cent_costo = @codcct and
      id_product = @codpro and
      id_especia = @codesp and
      id_empresa = @codofi and
      id_cobranz = @codope

      select   @indnomc = nombre,
               @inddirc = direccion,
	       @prtcli = id_party
      from dbo.sce_pcol
      where cent_costo = @codcct and
      id_product = @codpro and
      id_especia = @codesp and
      id_empresa = @codofi and
      id_cobranz = @codope and
      posicion = 1

      select   @codcct	,
             @codpro	,
             @codesp	,
             @codofi	,
             @codope	,
             @fecing	,
	     1	,
             @prtcli	,
             @indnomc	,
             @inddirc
   end

   if @codpro = ''07''
   begin
      select   @fecing = fecing
      from dbo.sce_jneg
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope

      select   @prtcli  = codprt,
               @indnomc = indnom,
               @inddirc = inddir
      from dbo.sce_jprt
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 0

      select   @codcct	,
             @codpro	,
             @codesp	,
             @codofi	,
             @codope	,
             @fecing	,
	     0	,
             @prtcli	,
             @indnomc	,
             @inddirc
   end

   if @codpro = ''08''
   begin
      select   @fecing = fecing
      from dbo.sce_jant
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope

      select   @prtcli  = codprt,
               @indnomc = indnom,
               @inddirc = inddir
      from dbo.sce_jprt
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 0

      select   @codcct	,
             @codpro	,
             @codesp	,
             @codofi	,
             @codope	,
             @fecing	,
	     0	,
             @prtcli	,
             @indnomc	,
             @inddirc
   end


   if @codpro = ''20''
   begin
      select   codcct	,
             codpro	,
             codesp	,
             codofi	,
             codope	,
             fecing	,
	     tipcvd	,
             prtcli	,
             indnomc	,
             inddirc
      from dbo.sce_cvd
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end
   if @codpro = ''06''
   begin
      select   codcct	,
             codpro	,
   	     codesp	,
 	     codofi	,
             codope	,
             fecing	,
	     3 		,    /* visible expor */
             prtexp1	,
             indnom1	,
	     inddir1
      from dbo.sce_xcob
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end            

   if @codpro = ''09''
   begin
      select   codcct     ,
             codpro     ,
             codesp     ,
             codofi     ,
             codope     ,
             fecing     ,
             3          ,    /* visible expor */    
             prtexp    ,
             indexpn    ,
             indexpd
      from dbo.sce_ycce
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end                                              

   if @codpro = ''18''
   begin
      select   codcct     ,
             codpro     ,
             codesp     ,
             codofi     ,
             codope     ,
             fecing     ,
             3          ,    /* visible expor */    
             prtexp    ,
             indexpn    ,
             indexpd
      from dbo.sce_ypag
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end                                              


   if @codpro = ''17''
   begin
      select   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		fecing,
	        3     ,   /* visible expor */
		prtexp,
		indnome,
		inddire
      from dbo.sce_xret
      where codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end		

   if @codpro = ''30''
   begin
      select   codcct	 ,
             codpro	 ,
             codesp	 ,
             codofi	 ,
             codope	 ,
             fecing	 ,
	         tipcvd  ,
             prtcli	 ,
             indnomc ,
             inddirc
      from dbo.sce_cvd
      where codcct = @codcct
      and   codpro = @codpro
      and   codesp = @codesp
      and   codofi = @codofi
      and   codope = @codope
   end
		
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cvd1_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd1_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cvd1_s03_MS] 
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
      --numpln          CHAR(7),
      --fecpln          datetime,
      --visinv          CHAR(3),
      --tippln          NUMERIC(3,0),
      --codmnd          NUMERIC(3,0),
      --mtopln          NUMERIC(15,2),
      --tipcam          NUMERIC(11,4),
      --tipcamo         NUMERIC(11,4),
      --plzbcc          NUMERIC(3,0),
      --estado          TINYINT,
      --numdec		CHAR(7),
      --fecdec		datetime,
      --codadn		NUMERIC(3,0),
      --prtexp		CHAR(12),
      --codcom		CHAR(9)

   Select  
		cast(numpre		as char(7))				numpln,
		cast(fecpre		as datetime)			fecpln,
		cast(''VIS''		as char(3)) 			visinv,
		cast(tippln		as numeric(3,0))		tippln,
		cast(codmnd		as numeric(3,0))		codmnd,
		cast(mtoliq		as numeric(15,2))		mtopln,
		cast(tipcam		as numeric(11,4))		tipcam,
		cast(tipcamo	as numeric(11,4))		tipcamo,
		cast(plzbcc		as numeric(3,0))		plzbcc,
		cast(estado		as tinyint)				estado,
		cast(numdec		as char(7))				numdec,
		cast(fecdec		as datetime)			fecdec,
		cast(codadn		as numeric(3,0))		codadn,
		cast(prtexp		as char(12))			prtexp,
		cast(''''			as char(9))				codcom
   from dbo.sce_xplv where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   mtoliq > 0 	 and
   plnest = 0

   UNION ALL

   Select  
		cast(numpre		as char(7))				numpln,
		cast(fecpre		as datetime)			fecpln,
		cast(''VIS''		as char(3)) 			visinv,
		cast(tippln		as numeric(3,0))		tippln,
		cast(codmnd		as numeric(3,0))		codmnd,
		cast(afimto		as numeric(15,2))		mtopln,
		cast(tipcam		as numeric(11,4))		tipcam,
		cast(tipcamo	as numeric(11,4))		tipcamo,
		cast(plzbcc		as numeric(3,0))		plzbcc,
		cast(estado		as tinyint)				estado,
		cast(numdec		as char(7))				numdec,
		cast(fecdec		as datetime)			fecdec,
		cast(codadn		as numeric(3,0))		codadn,
		cast(prtexp		as char(12))			prtexp,
		cast(''''			as char(9))				codcom
   from dbo.sce_xplv where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   afimto > 0       and
   mtobru = 0       and
   mtoliq = 0	 and
   plnest = 0  

   UNION ALL

   Select  
		cast(numpre		as char(7))				numpln,
		cast(fecpre		as datetime)			fecpln,
		cast(''TRN''		as char(3)) 			visinv,
		cast(tippln		as numeric(3,0))		tippln,
		cast(codmnd		as numeric(3,0))		codmnd,
		cast(mtoliq		as numeric(15,2))		mtopln,
		cast(tipcam		as numeric(11,4))		tipcam,
		cast(tipcamo	as numeric(11,4))		tipcamo,
		cast(plzbcc		as numeric(3,0))		plzbcc,
		cast(estado		as tinyint)				estado,
		cast(numdec		as char(7))				numdec,
		cast(fecdec		as datetime)			fecdec,
		cast(codadn		as numeric(3,0))		codadn,
		cast(prtexp		as char(12))			prtexp,
		cast(''''			as char(9))				codcom
   from dbo.sce_xplv where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   mtoliq > 0	 and
   plnest = 1
   
   UNION ALL

   Select  
		cast(numpre		as char(7))				numpln,
		cast(fecpre		as datetime)			fecpln,
		cast(''TRN''		as char(3)) 			visinv,
		cast(tippln		as numeric(3,0))		tippln,
		cast(codmnd		as numeric(3,0))		codmnd,
		cast(afimto		as numeric(15,2))		mtopln,
		cast(tipcam		as numeric(11,4))		tipcam,
		cast(tipcamo	as numeric(11,4))		tipcamo,
		cast(plzbcc		as numeric(3,0))		plzbcc,
		cast(estado		as tinyint)				estado,
		cast(numdec		as char(7))				numdec,
		cast(fecdec		as datetime)			fecdec,
		cast(codadn		as numeric(3,0))		codadn,
		cast(prtexp		as char(12))			prtexp,
		cast(''''			as char(9))				codcom
   from dbo.sce_xplv where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   afimto > 0       and
   mtobru = 0       and
   mtoliq = 0 	 and
   plnest = 1

   UNION ALL

   Select  
		cast(numpli						as char(7))				numpln,
		cast(fecpli						as datetime)			fecpln,
		cast(''INV''						as char(3)) 			visinv,
		cast(tippln						as numeric(3,0))		tippln,
		cast(codmnd						as numeric(3,0))		codmnd,
		cast(mtoope						as numeric(15,2))		mtopln,
		cast(tipcam						as numeric(11,4))		tipcam,
		cast(round(mtonac/mtoope,4)		as numeric(11,4))		tipcamo,
		cast(plzbcc						as numeric(3,0))		plzbcc,
		cast(estado						as tinyint)				estado,
		cast(numdec						as char(7))				numdec,
		cast(fecdec						as datetime)			fecdec,
		cast(codadn						as numeric(3,0))		codadn,
		cast(prtcli						as char(12))			prtexp,
		cast(codcom+concep				as char(9))				codcom
   from dbo.sce_pli where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
   
   UNION ALL

   select  
		cast(num_presen		as char(7))				numpln,
        cast(fechaventa		as datetime)			fecpln,
        cast(''VIS''			as char(3)) 			visinv,
        cast(0				as numeric(3,0))		tippln,
        cast(codmone		as numeric(3,0))		codmnd,
        cast(mtototal		as numeric(15,2))		mtopln,
		cast(tipo_camb		as numeric(11,4))		tipcam,
		cast(tipo_camb		as numeric(11,4))		tipcamo,
		cast(cod_plaza		as numeric(3,0))		plzbcc,
		cast(estado			as tinyint)				estado,
		cast(num_dec		as char(7))				numdec,
		cast(fec_dec		as datetime)			fecdec,
		cast(0				as numeric(3,0))		codadn,
		cast(rut			as char(12))			prtexp,
		cast(''''				as char(9))				codcom
   from dbo.sce_plan where
   cent_costo = @codcct and
   id_product = @codpro and
   id_especia = @codesp and
   id_empresa = @codofi and
   id_cobranz = @codope     
	
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_cvd1_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cvd1_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cvd1_s04_MS] 
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   declare
   @numpla		NUMERIC(10,0),@fecanu		SMALLDATETIME
   begin
      create table #pln
      (
         numpln          CHAR(7),
         fecpln          datetime,
         visinv          CHAR(3),
         tippln          NUMERIC(3,0),
         codmnd          NUMERIC(3,0),
         mtopln          NUMERIC(15,2),
         tipcam          NUMERIC(11,4),
         tipcamo         NUMERIC(11,4),
         plzbcc          NUMERIC(3,0),
         estado          TINYINT,
         numdec	    	CHAR(7),
         fecdec	    	datetime,
         codadn   		NUMERIC(3,0),
         prtexp   		CHAR(12),
         codcom   		CHAR(9)
      )
      insert #pln
      Select  numpre,
		fecpre,
		''VIS'',
		tippln,
		codmnd,
		mtoliq,
		tipcam,
		tipcamo,
		plzbcc,
		estado,
		numdec,
		fecdec,
		codadn,
		prtexp,
		''''
      from  dbo.sce_xplv
      where  codcct = @codcct
      and  codpro = @codpro
      and  codesp = @codesp
      and  codofi = @codofi
      and  codope = @codope
      and  mtoliq > 0
      and  plnest = 0

      Insert #pln
      Select  numpre,
		fecpre,
		''VIS'',
		tippln,
		codmnd,
		afimto,
		tipcam,
		tipcamo,
		plzbcc,
		estado,
		numdec,
		fecdec,
		codadn,
		prtexp,
		''''
      from dbo.sce_xplv
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
      and mtoliq = 0
      and plnest = 0
      and afimto > 0
      and mtobru = 0

      insert #pln
      Select  numpre,
		fecpre,
		''TRN'',
		tippln,
		codmnd,
		mtoliq,
		tipcam,
		tipcamo,
		plzbcc,
		estado,
		numdec,
		fecdec,
		codadn,
		prtexp,
		''''
      from dbo.sce_xplv
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
      and mtoliq > 0
      and plnest = 1

      Insert #pln
      Select  numpre,
		fecpre,
		''TRN'',
		tippln,
		codmnd,
		afimto,
		tipcam,
		tipcamo,
		plzbcc,
		estado,
		numdec,
		fecdec,
		codadn,
		prtexp,
		''''
      from dbo.sce_xplv
      where codcct = @codcct
      and	 codpro = @codpro
      and	 codesp = @codesp
      and	 codofi = @codofi
      and	 codope = @codope
      and	 mtoliq = 0
      and	 plnest = 1
      and	 afimto > 0
      and	 mtobru = 0

      Insert #pln
      Select  numpli,
		fecpli,
		''INV'',
		tippln,
		codmnd,
		mtoope,
		tipcam,
		round(mtonac/mtoope,4),
		plzbcc,
		estado,
		numdec,
		fecdec,
		codadn,
		prtcli,
		codcom+concep
      from dbo.sce_pli
      where codcct = @codcct
      and	 codpro = @codpro
      and	 codesp = @codesp
      and	 codofi = @codofi
      and	 codope = @codope
      Insert #pln
      select  num_presen,
           fechaventa,
       	   ''VIS'',
       	   0,
       	   codmone,
       	   mtototal,
		   tipo_camb,
		   tipo_camb,
		   cod_plaza,
		   estado,
		   num_dec,
		   fec_dec,
		   0,
		   rut,
		   ''''
      from dbo.sce_plan
      where cent_costo = @codcct
      and id_product = @codpro
      and id_especia = @codesp
      and id_empresa = @codofi
      and id_cobranz = @codope
      insert #pln
      select  a.num_presen,
    		a.fechaventa,
    		''VIS'',
    		0,
    		a.codmone,
    		a.mtototal,
        	a.tipo_camb,
            a.tipo_camb,
	a.cod_plaza,
        	a.estado,
        	a.num_dec,
        	a.fec_dec,
        	0,
        	a.rut,
        	''''
      from    dbo.sce_plan a, dbo.sce_reb b
      where   a.cent_costo   = @codcct
      and   a.id_product   = @codpro
      and   a.id_especia   = @codesp
      and   a.id_empresa   = @codofi
      and   a.id_cobranz   = @codope
      and	  b.cent_costo   = @codcct
      and   b.id_product   = @codpro
      and   b.id_especia   = @codesp
      and   b.id_empresa   = @codofi
      and   b.id_operac    = @codope
      and   a.hayanula     = 1
      and   a.cent_costo   = b.cent_costo
      and   a.id_product   = b.id_product
      and   a.id_especia   = b.id_especia
      and   a.id_empresa   = b.id_empresa
      and   a.id_cobranz   = b.id_operac
      and   a.num_presen   = b.numpla
      and   a.fechaanula   = b.fecha
      select   numpln,
		fecpln,
		visinv,
		tippln,
		codmnd,
		mtopln,
		tipcam,
		tipcamo,
		plzbcc,
		estado,
		numdec,
		fecdec,
		codadn,
		prtexp,
		codcom
      from #pln
   end
   Return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_i01_MS] 
	@id_party		CHAR(12),
	@id_dir			NUMERIC(2,0),
	@borrado		BIT,
	@direccion		VARCHAR(60),
	@comuna			VARCHAR(40),
	@cod_comuna		NUMERIC(4,0),
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
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
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

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_s04_MS]
	@id_party       CHAR(12),
	@id_dir         TINYINT ,
	@op             CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   if @op = ''D''
   begin
      select   direccion 
	  from dbo.sce_dad 
	  where
      id_party = @id_party and
      id_dir   = @id_dir
   end
   if @op = ''DC''
   begin
      select   direccion+'' ''+ciudad 
	  from dbo.sce_dad 
	  where
      id_party = @id_party and
      id_dir   = @id_dir
   end

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_s06_MS] 
	@id_party       CHAR(12),
	@id_dir 		TINYINT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	SELECT
		id_party,
		id_dir,
		direccion ,
		comuna	  ,
		ciudad    ,
		pais      ,
		borrado
	FROM dbo.sce_dad
	WHERE	id_party = @id_party
		AND	id_dir = @id_dir

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_s07_MS]
	@id_party       CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	SELECT 
		direccion, 
		ciudad, 
		pais 
	FROM 
		sce_dad 
	WHERE 
		id_party LIKE ''%'' + @id_party + ''%''

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_s08_MS]
	@id_party       CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	SELECT [id_party]
      ,[id_dir]
      ,[borrado]
      ,[direccion]
      ,[comuna]
      ,[cod_comuna]
      ,[cod_postal]
      ,[estado]
      ,[ciudad]
      ,[pais]
      ,[cod_pais]
      ,[telefono]
      ,[fax]
      ,[telex]
      ,[envio_sce]
      ,[recibe_sce]
      ,[cas_postal]
      ,[cas_banco]
      ,[crea_costo]
      ,[crea_user]
      ,[email]
	FROM 
		[sce_dad]
	WHERE 
		id_party = @id_party 
	ORDER BY 
		id_dir
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_s09_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_s09_MS] 
	@id_party         CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   SELECT COUNT(1)
   FROM 
		[sce_dad]
   WHERE
		id_party = @id_party
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_u01_MS]
	@id_party       CHAR(12),
	@id_dir         TINYINT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	UPDATE sce_dad 
	SET borrado = 0 
	WHERE 
		id_party	= @id_party  
		AND id_dir	= @id_dir
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_dad_u02_MS]
	@id_party		CHAR(12),
	@id_dir			NUMERIC(2,0),
	@borrado		BIT,
	@direccion		VARCHAR(60),
	@comuna			VARCHAR(40),
	@cod_comuna		NUMERIC(4,0),
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
	@email			VARCHAR(50)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	UPDATE sce_dad 
	SET 
		id_party	= @id_party,
		id_dir		= @id_dir,
		borrado		= @borrado,
		direccion	= @direccion,
		comuna		= @comuna,
		cod_comuna	= @cod_comuna,
		cod_postal	= @cod_postal,
		estado		= @estado, 
		ciudad		= @ciudad,
		pais		= @pais,
		cod_pais	= @cod_pais,
		telefono	= @telefono,
		fax			= @fax,
		telex		= @telex,
		envio_sce	= @envio_sce,
		recibe_sce	= @recibe_sce,
		cas_postal	= @cas_postal,
		cas_banco	= @cas_banco,
		email		= @email
	WHERE 
		id_party	= @id_party AND 
		id_dir		= @id_dir
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_dad_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dad_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-10   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
CREATE PROCEDURE [dbo].[sce_dad_u03_MS]
	@id_party       CHAR(12),
	@id_dir         TINYINT 
AS
BEGIN
	UPDATE sce_dad 
	SET borrado = 1 
	WHERE 
		id_party	= @id_party  
		AND id_dir	= @id_dir
END
' 
END

/****** Object:  StoredProcedure [dbo].[sce_doc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_doc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_doc_s01_MS] 
	@codcct         CHAR(3),
    @codpro         CHAR(2),    
    @codesp         CHAR(2),
    @codofi         CHAR(3),
    @codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   declare @prtcli  CHAR(12)


   if @codpro = ''07'' or @codpro = ''08''
   begin
      select distinct  codprt
      from dbo.sce_jprt
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
      and posprt = 0
   end
	
   if @codpro = ''03''
   begin
      select distinct  id_party
      from dbo.sce_pcol
      where  cent_costo = @codcct
      and  id_product = @codpro
      and  id_especia = @codesp
      and  id_empresa = @codofi
      and  id_cobranz = @codope
      and  posicion = 1
   end
       
   if @codpro = ''11''
   begin
      select distinct  codprt
      from dbo.sce_lprt
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope  
                   
             -- select @codprt                       	
   end
      
   if @codpro = ''06''
   begin
      select distinct  prtexp1
      from dbo.sce_xcob
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
   end
      
   if @codpro = ''17''
   begin
      select distinct  prtexp
      from dbo.sce_xret
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
   end
      
   if @codpro = ''09''
   begin
      select distinct  prtexp
      from dbo.sce_ycce
      where codcct = @codcct
      and  codpro = @codpro
      and  codesp = @codesp
      and  codofi = @codofi
      and  codope = @codope
   end
      
   if @codpro = ''05''
   begin
      select distinct  prtexp
      from dbo.sce_pae
      where codcct = @codcct
      and  codpro = @codpro
      and  codesp = @codesp
      and  codofi = @codofi
      and  codope = @codope
   end

   if @codpro = ''12'' or @codpro = ''13''
   begin
      select distinct  prtexp
      from dbo.sce_ppae
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
      and posprt = 0
   end                     
                                        
   if @codpro = ''14''
   begin
      select distinct  prtexp
      from dbo.sce_yava
      where codcct = @codcct
      and  codpro = @codpro
      and  codesp = @codesp
      and  codofi = @codofi
      and  codope = @codope
   end
                                
   if @codpro = ''18''
   begin
      select distinct  prtexp
      from dbo.sce_ypag
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
   end 

   if @codpro = ''20''
   begin
      select distinct  prtcli
      from dbo.sce_cvd
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
   end

        /************************************/
        /*Real Systems Ltda *****************/
        /*Se agrega producto 30**************/               
   if @codpro = ''30''
   Begin
      Select   rutcli
      from   dbo.sce_cvd
      where  codcct = @codcct
      and    codpro = @codpro
      and    codesp = @codesp
      and    codofi = @codofi
      and    codope = @codope
   End
        /************************************/
       
   if @codpro = ''10''
   begin
      select   @prtcli = isnull(convert(VARCHAR,prtcli),'''')
      from dbo.sce_mch
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
      select   @prtcli = isnull(convert(VARCHAR,@prtcli),'''')
      if @prtcli = ''''
      begin
         select distinct  @prtcli = isnull(convert(VARCHAR,prtcli),'''')
         from dbo.sce_mchh
         where codcct = @codcct
         and  codpro = @codpro
         and  codesp = @codesp
         and  codofi = @codofi
         and  codope = @codope
      end
      select   @prtcli
   end                     

   if @codpro = ''23''
   begin
      select distinct  codprt
      from dbo.sce_fprt
      where codcct = @codcct
      and codpro = @codpro
      and codesp = @codesp
      and codofi = @codofi
      and codope = @codope
      and posprt = 0
   end

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_fer_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_fer_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[sce_fer_s01_MS] 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:11:11 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
	
   select * from dbo.sce_fer
   return
	
   return
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_fra_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_fra_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_fra_s06_MS] 
	@codfra         NUMERIC(4,0),
	@idioma         CHAR(1) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
if @idioma = ''E''
   begin
      select   nomesp as frase, desesp as numero
      from dbo.sce_fra where
      codfra = @codfra
   end
else
   begin
      select   noming as frase, desing as numero
      from dbo.sce_fra where
      codfra = @codfra
   end
   Return
   Return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_fra_s06_MS_alt]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_fra_s06_MS_alt]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_fra_s06_MS_alt] 
	@codfra         NUMERIC(4,0),
	@idioma         CHAR(1) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	  2015-09-28   LHL   Modificación para retornar un solo tipo de nombre de columna sin importar el idioma
*/
	SET NOCOUNT ON 

	select
		case when @idioma = ''E'' then nomesp else noming end as frase,
		case when @idioma = ''E'' then desesp else desing end as numero
	from
		dbo.sce_fra
	where
		codfra = @codfra

	return
end
' 
END

/****** Object:  StoredProcedure [dbo].[sce_gcar_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gcar_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sce_gcar_s05_MS]
    @cencos CHAR(3),
    @codusr CHAR(2),
    @produc VARCHAR(255) 
AS
BEGIN
   CREATE TABLE #cartera
   (
      prtcli CHAR(12)
   )

   if @cencos is null
   begin
      select
         CAST(null as CHAR(12)) AS prtcli
      return
   end

   if CHARINDEX(''06'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtexp1
      from dbo.sce_xcob
      where cencos = @cencos
      and codusr = @codusr
   end

   if CHARINDEX(''09'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtexp
      from dbo.sce_ycce
      where cencos = @cencos
      and  codusr = @codusr
   end                               
   begin
      insert #cartera
      select distinct prtben
      from dbo.sce_ytyt
      where cencost = @cencos
      and codusrt = @codusr
   end                         

   if CHARINDEX(''04'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtexp1
      from dbo.sce_xdec
      where cencos = @cencos
      and codusr = @codusr
   end

   if CHARINDEX(''17'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtexp
      from dbo.sce_xret
      where cencos = @cencos
      and codusr = @codusr
   end

   if CHARINDEX(''07'',@produc) <> 0
   begin
      insert #cartera
      select    distinct jprt.codprt
      from    dbo.sce_jcci jcci, dbo.sce_jprt jprt
      where     jcci.cencos = @cencos
      and   jcci.codusr = @codusr
      and   jcci.codcct = jprt.codcct
      and   jcci.codpro = jprt.codpro
      and       jcci.codesp = jprt.codesp
      and   jcci.codofi = jprt.codofi
      and   jcci.codope = jprt.codope
      and   jprt.posprt  = 0
      insert #cartera
      select    distinct jprt.codprt
      from    dbo.sce_jant jant, dbo.sce_jprt jprt
      where     jant.codpro  = ''08''
      and   jant.cencos = @cencos
      and   jant.codusr = @codusr
      and   jant.codcct = jprt.codcct
      and       jant.codpro = jprt.codpro
      and   jant.codesp = jprt.codesp
      and   jant.codofi = jprt.codofi
      and   jant.codope = jprt.codope
      and       jprt.posprt  = 0
   end

   if CHARINDEX(''05'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtexp
      from dbo.sce_pae
      where cencos = @cencos
      and codusr = @codusr
   end


   if CHARINDEX(''15'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtimp
      from dbo.sce_idi
      where ccosto = @cencos
      and usresp = @codusr
   end

   if CHARINDEX(''16'',@produc) <> 0
   begin
      insert #cartera
      select distinct prtimp
      from dbo.sce_dec
      where ccosto = @cencos
      and usresp = @codusr
   end

   if CHARINDEX(''03'',@produc) <> 0
   begin
      insert #cartera
      select    b.id_party
      from    dbo.sce_col a, dbo.sce_pcol b
      where     a.cencos = @cencos
      and   a.codusr = @codusr
      and   a.cent_costo = b.cent_costo
      and   a.id_product = b.id_product
      and   a.id_especia = b.id_especia
      and   a.id_empresa = b.id_empresa
      and   a.id_cobranz = b.id_cobranz
      and   b.posicion  = 1
   end

    SELECT DISTINCT prtcli
        FROM #cartera WHERE prtcli > ''-1'';
END' 
END

/****** Object:  StoredProcedure [dbo].[sce_gcar_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gcar_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_gcar_s06_MS]
	@cencos CHAR(3), 
	@codusr CHAR(2),
	@prtcli CHAR(12),
	@produc VARCHAR(255)
AS
BEGIN
    -- Fix para el .NET Entity Framework
    IF (ISNULL(@cencos, '''') = '''') BEGIN
        SELECT
            CAST (NULL AS INT) AS cantidad,
            CAST (NULL AS NUMERIC(2,0)) AS codpro;
        RETURN;
    END

	declare @cont INT, @cont2 INT, @cont3 INT;

	select   @prtcli = upper(@prtcli);

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	create table #tmp
	(
		cantidad INT,
		codpro   NUMERIC(2,0)
	)
	
	if CHARINDEX(''06'',@produc) <> 0
	begin
		select   @cont = count(*)
		from dbo.sce_xcob
		where cencos  = @cencos
		and codusr  = @codusr
		and prtexp1 = @prtcli;
		insert into #tmp values(@cont,6);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''09'',@produc) <> 0
	begin
		select   @cont2 = count(*)
		from dbo.sce_ycce
		where prtexp = @prtcli
		and cencos = @cencos
		and codusr = @codusr;
		select   @cont3 = count(*)
		from dbo.sce_ytyt
		where cencost = @cencos
		and codusrt = @codusr
		and prtben  = @prtcli;
		select   @cont = @cont2+@cont3;
		insert into #tmp values(@cont,9);
	end                                      

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''04'',@produc) <> 0
	begin
		select   @cont = count(*)
		from dbo.sce_xdec
		where  cencos  = @cencos
		and  codusr  = @codusr
		and  prtexp1 = @prtcli;
		insert into #tmp values(@cont,4);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''17'',@produc) <> 0
	begin
		select   @cont = count(*)
		from dbo.sce_xret
		where cencos = @cencos
		and codusr = @codusr
		and prtexp = @prtcli;
		insert into #tmp values(@cont,17);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''07'',@produc) <> 0
	begin
		select   @cont2 = isnull(count(*),0)
		from    dbo.sce_jcci jcci, dbo.sce_jprt jprt
		where   jcci.cencos = @cencos
		and   jcci.codusr = @codusr
		and   jcci.codcct = jprt.codcct
		and   jcci.codpro = jprt.codpro
		and   jcci.codesp = jprt.codesp
		and   jcci.codofi = jprt.codofi
		and   jcci.codope = jprt.codope
		and   jprt.posprt = 0
		and   jprt.codprt = @prtcli;
		select   @cont3 = isnull(count(*),0)
		from    dbo.sce_jant jant, dbo.sce_jprt jprt
		where   jant.cencos = @cencos
		and   jant.codusr = @codusr
		and   jant.codcct = jprt.codcct
		and   jant.codpro = jprt.codpro
		and   jant.codesp = jprt.codesp
		and   jant.codofi = jprt.codofi
		and   jant.codope = jprt.codope
		and   jprt.posprt = 0
		and   jprt.codprt = @prtcli;
		select   @cont = @cont2+@cont3;
		insert into #tmp values(@cont,7);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''05'',@produc) <> 0
	begin
		select   @cont = count(*)
		from dbo.sce_pae
		where cencos = @cencos
		and codusr = @codusr
		and prtexp = @prtcli;
		insert into #tmp values(@cont,5);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''15'',@produc) <> 0
	begin
		select   @cont = count(*)
		from dbo.sce_idi
		where prtimp = @prtcli
		and ccosto = @cencos
		and usresp = @codusr;
		insert into #tmp values(@cont,15);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''16'',@produc) <> 0
	begin
		select   @cont = count(*)
		from dbo.sce_dec
		where usresp = @codusr
		and ccosto = @cencos
		and prtimp = @prtcli;
		insert into #tmp values(@cont,16);
	end

	select   @cont = 0
	select   @cont2 = 0
	select   @cont3 = 0

	if CHARINDEX(''03'',@produc) <> 0
	begin
		select   @cont = count(*)
		from    dbo.sce_col a, dbo.sce_pcol b
		where 	a.cencos     = @cencos
		and   a.codusr     = @codusr
		and   a.cent_costo = b.cent_costo
		and   a.id_product = b.id_product
		and   a.id_especia = b.id_especia
		and   a.id_empresa = b.id_empresa
		and   a.id_cobranz = b.id_cobranz
		and   b.id_party   = @prtcli
		and  	b.posicion   = 1;
		insert into #tmp values(@cont,3);
	end

	select cantidad, codpro from #tmp order by codpro;
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_gcar_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gcar_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_gcar_u03_MS]
	@cencos CHAR(3),
	@codusr CHAR(2),
	@cctact CHAR(3),
	@usract CHAR(2),
	@fecact datetime,
	@prtcli CHAR(12),
	@produc	VARCHAR(255) 
AS
BEGIN
	BEGIN TRANSACTION;

    -- Fix para el .NET Entity Framework
    IF (ISNULL(@cencos, '''') = '''') BEGIN
        ROLLBACK TRANSACTION;
        SELECT
            CAST (NULL AS INT) AS Column1,
            CAST (NULL AS VARCHAR(40)) AS Column2;
        RETURN;
    END

	select   @prtcli = upper(@prtcli)     

	-- Cobranza de Exportación
	if CHARINDEX(''06'',@produc) <> 0
	begin
		update dbo.sce_xcob set cencos = @cencos,codusr = @codusr,fecact = @fecact  where
		cencos = @cctact and
		codusr = @usract and
		prtexp1 = @prtcli;
	end

	--Carta de Crédito Exportaciones
   if CHARINDEX(''09'',@produc) <> 0
   begin
		--Actualiza tabla de Carta Cre.
      update dbo.sce_ycce set cencos = @cencos,codusr = @codusr,fecact = @fecact  where
      cencos = @cctact and
      codusr = @usract and
      prtexp = @prtcli;
		--Actualiza tabla Negociaciones.
      update dbo.sce_yneg set cencos = @cencos,codusr = @codusr,fecact = @fecact  where
      cencos = @cctact and
      codusr = @usract and
      prtben = @prtcli;
		--Actualizo Tabla de Tranf/Trsp.
      update dbo.sce_ytyt set cencosd = @cencos,codusrd = @codusr,fecact  = @fecact  where
      cencosd = @cctact and
      codusrd = @usract and
      prtben  = @prtcli;
      update dbo.sce_ytyt set cencost = @cencos,codusrt = @codusr,fecact  = @fecact  where
      cencost = @cctact and
      codusrt = @usract and
      prtben  = @prtcli;
   end                                      

	-- Declaración de Exportación
   if CHARINDEX(''04'',@produc) <> 0
   begin
      update dbo.sce_xdec set cencos = @cencos,codusr = @codusr,fecact = @fecact  where
      cencos = @cctact and
      codusr = @usract and
      prtexp1 = @prtcli;
   end

	-- Retorno de Exportación
   if CHARINDEX(''17'',@produc) <> 0
   begin
      update dbo.sce_xret set cencos = @cencos,codusr = @codusr,fecact = @fecact  where
      cencos = @cctact and
      codusr = @usract and
      prtexp = @prtcli;
   end

	-- Carta de Crédito Importación
   if CHARINDEX(''07'',@produc) <> 0
   begin
      update dbo.sce_jcci set cencos = @cencos,codusr = @codusr,fecact = @fecact from    dbo.sce_jcci jcci, dbo.sce_jprt jprt where   jcci.cencos = @cctact and
      jcci.codusr = @usract and
		-- Para Encontrar el Party
      jcci.codcct = jprt.codcct and
      jcci.codpro = jprt.codpro and
      jcci.codesp = jprt.codesp and
      jcci.codofi = jprt.codofi and
      jcci.codope = jprt.codope and
      jprt.codprt = @prtcli     and
      jprt.posprt  = 0;

      update dbo.sce_jacp set cencos = @cencos,codusr = @codusr,fecact = @fecact from    dbo.sce_jacp jacp, dbo.sce_jprt jprt where   jacp.cencos = @cctact and
      jacp.codusr = @usract and
		-- Para Encontrar el Party
      jacp.codcct = jprt.codcct and
      jacp.codpro = jprt.codpro and
      jacp.codesp = jprt.codesp and
      jacp.codofi = jprt.codofi and
      jacp.codope = jprt.codope and
      jprt.codprt = @prtcli     and
      jprt.posprt  = 0;

      update dbo.sce_jant set cencos = @cencos,codusr = @codusr,fecact = @fecact from    dbo.sce_jant jant, dbo.sce_jprt jprt where   jant.cencos = @cctact and
      jant.codusr = @usract and
		-- Para Encontrar el Party
      jant.codcct = jprt.codcct and
      jant.codpro = jprt.codpro and
      jant.codesp = jprt.codesp and
      jant.codofi = jprt.codofi and
      jant.codope = jprt.codope and
      jprt.codprt = @prtcli     and
      jprt.posprt  = 0;

      update dbo.sce_jdoe set cencos = @cencos,codusr = @codusr,fecact = @fecact from    dbo.sce_jdoe jdoe, dbo.sce_jprt jprt where   jdoe.cencos = @cctact and
      jdoe.codusr = @usract and
		-- Para Encontrar el Party
      jdoe.codcct = jprt.codcct and
      jdoe.codpro = jprt.codpro and
      jdoe.codesp = jprt.codesp and
      jdoe.codofi = jprt.codofi and
      jdoe.codope = jprt.codope and
      jprt.codprt = @prtcli     and
      jprt.posprt  = 0;

      update dbo.sce_jneg set cencos = @cencos,codusr = @codusr,fecact = @fecact from    dbo.sce_jneg jneg, dbo.sce_jprt jprt where   jneg.cencos = @cctact and
      jneg.codusr = @usract and
		-- Para Encontrar el Party
      jneg.codcct = jprt.codcct and
      jneg.codpro = jprt.codpro and
      jneg.codesp = jprt.codesp and
      jneg.codofi = jprt.codofi and
      jneg.codope = jprt.codope and
      jprt.codprt = @prtcli     and
      jprt.posprt  = 0;
   end
	
	-- Préstamos a Exportadores
   if CHARINDEX(''05'',@produc) <> 0
   begin
      update dbo.sce_pae set cencos = @cencos,codusr = @codusr,fecact = @fecact  where
      cencos = @cctact and
      codusr = @usract and
      prtexp = @prtcli;
   end

   if CHARINDEX(''15'',@produc) <> 0
   begin
      update dbo.sce_idi set ccosto = @cencos,usresp = @codusr,fecact = @fecact  where
      ccosto = @cctact and
      usresp = @usract and
      prtimp = @prtcli;
   end

   if CHARINDEX(''16'',@produc) <> 0
   begin
      update dbo.sce_dec set ccosto = @cencos,usresp = @codusr,fecact = @fecact  where
      ccosto = @cctact and
      usresp = @usract and
      prtimp = @prtcli;
   end

   if CHARINDEX(''03'',@produc) <> 0
   begin
      update dbo.sce_col set cencos = @cencos,codusr = @codusr from    dbo.sce_col a, dbo.sce_pcol b where 	a.cencos     = @cctact      and
      a.codusr     = @usract      and
      a.cent_costo = b.cent_costo and
      a.id_product = b.id_product and
      a.id_especia = b.id_especia and
      a.id_empresa = b.id_empresa and
      a.id_cobranz = b.id_cobranz and
      b.id_party   = @prtcli      and
      b.posicion   = 1;
   end

   if (@@error <> 0)
   begin
      ROLLBACK TRANSACTION;
      Select -1, ''Error al grabar datos en tablas.'';
      return;
   end

   COMMIT TRANSACTION;
   Select   0,''Grabacion Exitosa'';
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_gpln_s10_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s10_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_gpln_s10_MS] @cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecing         CHAR(10) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	/* Esto permite que Entity Framework mapee las columnas correctamente */
	if @codusr is null and @cencos is null and @fecing is null
	begin
		select
			cast(null as CHAR(7)) numpre,
			CAST(null as datetime) AS fecpre,
			CAST(null as CHAR(3)) AS cencos,
			CAST(null as CHAR(2)) AS codusr,
			CAST(null as CHAR(15)) AS operacion,
			CAST(null as CHAR(12)) AS prtexp,
			CAST(null as INT) AS indnom,
			CAST(null as INT) AS tippln,
			CAST(null as INT) AS codmnd,
			CAST(null as NUMERIC(15,2)) AS mtoliq,
			CAST(null as CHAR(3)) AS tipo,
			CAST(null as CHAR(1)) AS ingegr
		return
	end

   declare @fecpro datetime

   select   @fecpro = convert(datetime,@fecing,103)


   create table #pln
   (
      numpre  	CHAR(7),
      fecpre  	datetime,
      cencos  	CHAR(3),
      codusr  	CHAR(2),
      operacion	CHAR(15),
      prtexp  	CHAR(12),
      indnom  	INT,
      tippln  	INT,
      codmnd  	INT,
      mtoliq 		NUMERIC(15,2),
      tipo    	CHAR(3),
      ingegr   	CHAR(1)
   )                               

   If @codusr <> ''99''        --Especialista.-
   begin
	-- PLANILLAS VISIBLES de EXPORTACIONES.-

      Insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		1 tippln,
		codmnd,
		mtoliq,
		''VIS'' tipo,
		''I'' ingegr
      from dbo.sce_xplv
      where cencos = @cencos
      and codusr = @codusr
      and fecing >=  dateadd(dd,0,@fecpro)
      and fecing < dateadd(dd,+1,@fecpro)
      and estado <> 9
      and plnest = 0
		

	
	-- PLANILLAS DE TRANSFERENCIA DE INGRESO DE EXPORTACIONES	
      Insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		8,
		codmnd,
		mtoliq,
		''TRN'' tipo,
		''I'' ingegr
      from dbo.sce_xplv
      where cencos = @cencos
      and codusr = @codusr
      and fecing >= dateadd(dd,0,@fecpro)
      and fecing < dateadd(dd,+1,@fecpro)
      and estado <> 9
      and plnest = 1
	  
	

	
	-- PLANILLAS TRANSFERENCIAS INVISIBLES.-
      Insert #pln
      Select  numpli,
		fecpli,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtcli,
		indnom,
		tippln,
		codmnd,
		mtoope,
		''TRN'' tipo,
		'''' ingegr
      from dbo.sce_pli
      where cencos  =  @cencos
      and codusr  =  @codusr
      and fecing  >= dateadd(dd,0,@fecpro)
      and fecing  < dateadd(dd,+1,@fecpro)
      and estado  <> 9
      and tippln  >  5
	
	
	-- PLANILLAS INVISIBLES POSICION
      Insert #pln
      Select  numpli,
		fecpli,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtcli,
		indnom,
		tippln,
		codmnd,
		mtoope,
		''INV'' tipo,
		'''' ingegr
      from dbo.sce_pli
      where cencos =  @cencos
      and codusr =  @codusr
      and fecing >= dateadd(dd,0,@fecpro)
      and fecing < dateadd(dd,+1,@fecpro)
      and estado <> 9
      and tippln <  5
		
	
	-- PLANILLAS DE ANULACION EXPORTACIONES
      insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		3,
		codmnd,
		mtodolpo,
		''ANU'' tipo,
		''E'' ingegr
      from dbo.sce_xanu
      where cencos =  @cencos
      and codusr =  @codusr
      and fecing >= dateadd(dd,0,@fecpro)
      and fecing < dateadd(dd,+1,@fecpro)
      and estado <> 9
      and plnest =  0
	  
		  

 -- PLANILLAS DE ANULACION DE TRANSFERENCIAS DE INGRESO EXP.
      insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		10,
		codmnd,
		mtodolpo,
		''TRN'' tipo,
		''E'' ingegr
      from dbo.sce_xanu
      where cencos =  @cencos
      and	 codusr =  @codusr
      and  fecing >= dateadd(dd,0,@fecpro)
      and  fecing < dateadd(dd,+1,@fecpro)
      and	 plnest =  1
      and	 estado <> 9
		
	--PLANILLAS VISIBLES DE IMPORT.
      insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		2 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''E'' ingegr
      from dbo.sce_plan
      where cencos     =  @cencos
      and  codusr     =  @codusr
      and  fechaventa =  @fecpro
      and  estado     <  8
--	 and  estado     <> 8
      and  indanula   =  0
      and  hayanula   =  0
	 
	
	--PLANILLAS REEMPLAZO VISIBLES DE IMPORT.
      insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		5 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''E'' ingegr
      from dbo.sce_plan
      where cencos      =  @cencos
      and codusr      =  @codusr
      and fechaventa  =  @fecpro
      and estado     <  8
--	  and estado     <> 8
      and indanula   =  2
      and hayanula   =  0	
	  

	-- PLANILLAS DE TRANSFERENCIA DE EGRESO IMPOR.  
      insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		9 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''TRN'' tipo,
		''E'' ingegr
      from dbo.sce_plan
      where  cencos     =  @cencos
      and  codusr     =  @codusr
      and  fechaventa =  @fecpro
      and  estado     <  8
--	  and  estado     <> 8
      and  indanula   =  3
      and  hayanula   =  0
		  
	-- PLANILLAS ANULACION VISIBLES IMPORT.
      insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		4 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''I'' ingegr
      from dbo.sce_plan
      where cencos     =  @cencos
      and codusr     =  @codusr
      and fechaventa =  @fecpro
      and estado     <  8 
--	  and estado     <> 8 
      and hayanula   =  1
      and indanula   =  1
		

	-- PLANILLAS ANULACION TRANSFERENCIA EGRESO VISIBLES IMPORT.
      insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		11 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''TRN'' tipo,
		''I'' ingegr
      from dbo.sce_plan
      where cencos     =  @cencos
      and codusr     =  @codusr
      and fechaventa =  @fecpro
      and estado     <  8
--	  and estado     <> 8
      and hayanula   =  1
      and indanula   =  4
      update #pln set ingegr = ''I''  where 	tippln in(1,4,8,11)
      update #pln set ingegr = ''E''  where 	tippln in(2,3,5,9,10)
      Select   numpre,
		fecpre,
		cencos,
		codusr,
		operacion,
		prtexp,
		indnom,
		tippln,
		codmnd,
		mtoliq,
		tipo,
		ingegr
      from #pln order by numpre
   end

   If @codusr = ''99''        --Secci>n.-
   begin
  
	-- PLANILLAS VISIBLES de EXPORTACIONES.-
      insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		1 tippln,
		codmnd,
		mtoliq,
		''VIS'' tipo,
		''I'' ingegr
      from dbo.sce_xplv
      where cencos     =  @cencos
      and codusr     < ''99''
      and fecing     >= dateadd(dd,0,@fecpro)
      and fecing     < dateadd(dd,+1,@fecpro)
      and estado     <> 9
      and plnest     =  0
	
	
	-- PLANILLAS DE TRANSFERENCIA DE INGRESO DE EXPORTACIONES	
      Insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		8,
		codmnd,
		mtoliq,
		''TRN'' tipo,
		''I'' ingegr
      from dbo.sce_xplv
      where cencos =  @cencos
      and codusr < ''99''
      and fecing >= dateadd(dd,0,@fecpro)
      and fecing < dateadd(dd,+1,@fecpro)
      and estado <> 9
      and plnest =  1
      
  	-- PLANILLAS TRANSFERENCIAS INVISIBLES.-
      Insert #pln
      Select  numpli,
		fecpli,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtcli,
		indnom,
		tippln,
		codmnd,
		mtoope,
		''TRN'' tipo,
		'''' ingegr
      from dbo.sce_pli
      where cencos =  @cencos
      and codusr <  ''99''
      and fecing >= dateadd(dd,0,@fecpro)
      and fecing < dateadd(dd,+1,@fecpro)
      and estado <> 9
      and tippln >  5
		
	
	-- PLANILLAS INVISIBLES POSICION
      Insert #pln
      Select  numpli,
		fecpli,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtcli,
		indnom,
		tippln,
		codmnd,
		mtoope,
		''INV'' tipo,
		'''' ingegr
      from dbo.sce_pli
      where cencos  =  @cencos
      and codusr  <  ''99''
      and fecing  >= dateadd(dd,0,@fecpro)
      and fecing  < dateadd(dd,+1,@fecpro)
      and estado  <> 9
      and tippln  <  5
		
  	-- PLANILLAS DE ANULACION EXPORTACIONES
      insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		3,
		codmnd,
		mtodolpo,
		''ANU'' tipo,
		''E'' ingegr
      from dbo.sce_xanu
      where cencos =  @cencos
      and codusr <  ''99''
      and fecing =  @fecpro
      and estado <> 9
      and plnest =  0
		
	
	-- PLANILLAS DE ANULACION DE TRANSFERENCIAS DE INGRESO EXP.
      insert #pln
      Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		prtexp,
		indnom,
		10,
		codmnd,
		mtodolpo,
		''TRN'' tipo,
		''E'' ingegr
      from dbo.sce_xanu
      where cencos  =  @cencos
      and codusr  <  ''99''
      and fecing  = @fecpro
      and estado  <> 9
      and plnest  =  1
		  
    
	--PLANILLAS VISIBLES DE IMPORT.
      insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		2 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''E'' ingegr
      from dbo.sce_plan
      where fechaventa  =  @fecpro
      and cencos      =  @cencos
      and estado      <  8
--	  and estado      <> 8
      and indanula = 0
      and hayanula = 0
      UNION ALL
	
	--PLANILLAS REEMPLAZO VISIBLES DE IMPORT.
--	insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		5 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''E'' ingegr
      from dbo.sce_plan
      where fechaventa  =  @fecpro
      and cencos      =  @cencos
      and estado      <  8 
--	  and estado      <> 8 
      and indanula    =  2
      and hayanula    =  0
      UNION ALL

	-- PLANILLAS DE TRANSFERENCIA DE EGRESO IMPOR.  
--	insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		9 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''TRN'' tipo,
		''E'' ingegr
      from dbo.sce_plan
      where fechaventa  =  @fecpro
      and cencos      =  @cencos
      and estado      <  8
--	  and estado      <> 8
      and indanula    =  3
      and hayanula    =  0
      UNION ALL
		 
	-- PLANILLAS ANULACION VISIBLES IMPORT.
--	insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		4 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''I'' ingegr
      from dbo.sce_plan
      where fechaventa  =  @fecpro
      and cencos      =  @cencos
      and estado      <  8
--	  and estado      <> 8
      and hayanula    =  1
      and indanula    =  1
      UNION ALL
		

	-- PLANILLAS ANULACION TRANSFERENCIA EGRESO VISIBLES IMPORT.
--	insert #pln
      select  convert(CHAR(7),num_presen) numpre,
		fechaventa fecpre,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz operacion ,
		party prtexp,
		0 indnom,
		11 tippln ,
		codmone codmnd,
		mtototal mtoliq,
		''TRN'' tipo,
		''I'' ingegr
      from dbo.sce_plan
      where fechaventa  = @fecpro
      and cencos      = @cencos
      and estado      < 8
--	  and estado      <> 8
      and hayanula    =  1
      and indanula    =  4
      update #pln set ingegr = ''I''  where 	tippln in(1,4,8,11)
      update #pln set ingegr = ''E''  where 	tippln in(2,3,5,9,10)
      Select   numpre,
		fecpre,
		cencos,
		codusr,
		operacion,
		prtexp,
		indnom,
		tippln,
		codmnd,
		mtoliq,
		tipo,
		ingegr
      from #pln order by numpre
   end


end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_gpln_s11_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s11_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_gpln_s11_MS] @cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecing         CHAR(10) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	/* Mapping para Entity Framework */
	if (@cencos = null and @codusr = null and @fecing = null) 
	begin
		select
			1 tippln,
			cast(1 as numeric(3,0)) codmnd,
			cast(null as numeric(15,2)) as mtoliq,
			''VIS'' tipo,
			''I'' ingegr
		return
	end

   declare @fecpro datetime

   select   @fecpro = convert(datetime,@fecing,103)

   Select  numpre,
		 1 tippln,
		codmnd,
		mtoliq,
		''VIS'' tipo,
		''I'' ingegr
   into #pln from dbo.sce_xplv
   where cencos =  @cencos
   and codusr =  @codusr
   and fecing >= dateadd(dd,0,@fecpro)
   and fecing <  dateadd(dd,+1,@fecpro)
   and estado <> 9
   and plnest =  0
		
	
   Insert #pln
   select  convert(CHAR(7),num_presen) numpre,
		2 tippln,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''E'' ingegr
   from dbo.sce_plan
   where cencos     =  @cencos
   and codusr     =  @codusr
   and fechaventa =  @fecpro
   and estado     <  8
--	  and estado     <> 8
   and hayanula   =  0
   and indanula   <  3
   UNION ALL
   select  convert(CHAR(7),num_presen) numpre,
		4 tippln,
		codmone codmnd,
		mtototal mtoliq,
		''VIS'' tipo,
		''I'' ingegr
   from dbo.sce_plan
   where cencos      =  @cencos
   and codusr      =  @codusr
   and fechaventa  =  @fecpro
   and estado      <  8
--	  and estado      <> 8
   and hayanula    =  1
   and indanula    <  3

	
   Insert #pln
   Select  numpli,
		tippln,
		codmnd,
		mtoope,
		''INV'' tipo,
		'''' ingegr
   from dbo.sce_pli
   where cencos = @cencos
   and codusr = @codusr
   and fecing >= dateadd(dd,0,@fecpro)
   and fecing <  dateadd(dd,+1,@fecpro)
   and estado <> 9
   and tippln <= 4
		
		
   update #pln set ingegr = ''I''  where tippln = 1
   or tippln = 4	


   update #pln set ingegr = ''E''  where tippln = 2
   or tippln = 3 

   select   tippln,
		   codmnd,
		   SUM(mtoliq) as mtoliq,
		   tipo,
		   ingegr
   from #pln
   group by tippln,codmnd,tipo,ingegr
   order by tippln,codmnd,tipo,ingegr  --Akzio migracion

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_gpln_s12_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s12_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_gpln_s12_MS] @cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecing         CHAR(10) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	/* Para Entity Framework */
	if (@cencos = null and @codusr = null and @fecing = null)
	begin
		select
			CAST(null as SMALLINT) AS codmnd,
			CAST(null as NUMERIC(15,2)) AS mtoingt,
			CAST(null as NUMERIC(15,2)) AS mtoegrt,
			CAST(null as NUMERIC(15,2)) AS mtodeb,
			CAST(null as NUMERIC(15,2)) AS mtohab,
			CAST(null as NUMERIC(15,2)) AS difdeb,
			CAST(null as NUMERIC(15,2)) AS difhab
		return
	end



   declare @fecmin  datetime,@fecmax  datetime,@fecpro  datetime,@fechamc datetime

   select   @fecpro = SUBSTRING(@fecing,4,2)+''/''+SUBSTRING(@fecing,1,2)+''/''+SUBSTRING(@fecing,7,4)
   select   @fecmin = convert(CHAR(10),dateadd(dd,-1,@fecpro),101)+'' ''+''23:59:00''
   select   @fecmax = convert(CHAR(10),dateadd(dd,1,@fecpro),101)+'' ''+''00:00:01''
        
   select   @fechamc = convert(datetime,@fecing,103)

   create table #plning
   (
      codmnd          NUMERIC(3,0),
      mtoing          NUMERIC(15,2)
   )

   create table #plningt
   (
      codmnd          NUMERIC(3,0),
      mtoingt         NUMERIC(15,2)
   )

   create table #plnegr
   (
      codmnd          NUMERIC(3,0),
      mtoegr          NUMERIC(15,2)
   )

   create table #plnegrt
   (
      codmnd          NUMERIC(3,0),
      mtoegrt         NUMERIC(15,2)
   )

   create table #plningegrt
   (
      codmnd          NUMERIC(3,0),
      mtoingt         NUMERIC(15,2),
      mtoegrt         NUMERIC(15,2)
   )

   create table #cnvdeb
   (
      codmnd          NUMERIC(3,0),
      mtodeb          NUMERIC(15,2)
   )

   create table #cnvhab
   (
      codmnd          NUMERIC(3,0),
      mtohab          NUMERIC(15,2)
   )

   create table #cnvdebhab
   (
      codmnd          NUMERIC(3,0),
      mtodeb          NUMERIC(15,2),
      mtohab          NUMERIC(15,2)
   )

   Insert #plning
   Select  codmnd, sum(mtoope)
   from dbo.sce_pli
   where cencos = @cencos
   and codusr = @codusr
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and tippln in(1,4)
   group by codmnd

	--Obtiene las planillas visibles de export. que llevan contabilidad.-
   Insert #plning
   Select  codmnd, sum(mtoliq)
   from dbo.sce_xplv
   where  cencos = @cencos
   and  codusr = @codusr
   and  fecing > @fecmin
   and  fecing < @fecmax
   and  estado <> 9
   and  tippln in(401,403,407,500)
   and  plnest = 0
   group by codmnd

	--Planillas Visibles Import (egreso).-

   Insert #plning
   Select codmone, sum(mtototal)
   from dbo.sce_plan
   where cencos = @cencos
   and codusr = @codusr
   and fechaanula > convert(SMALLDATETIME,@fecmin)
   and fechaanula < convert(SMALLDATETIME,@fecmax)
   and hayanula = 1
   and indanula = 1
   and estado in(4,5,6)
   group by codmone

   Insert #plningt
   Select codmnd, sum(mtoing)
   from #plning group by(codmnd)

	--Planillas Invisibles (egreso).-
   Insert #plnegr
   Select  codmnd, sum(mtoope)
   from dbo.sce_pli
   where cencos = @cencos
   and codusr = @codusr
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and tippln in(2,3)
   group by codmnd

	--Planillas Visibles Export de Anulacion (egreso).-
   Insert #plnegr
   Select codmnd, sum(mtoanu)
   from dbo.sce_xanu
   where cencos = @cencos
   and codusr = @codusr
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and plnest = 0
   and tippln in(401,403,407,500)
   group by codmnd

	--Planillas Visibles Import (egreso).-
   Insert #plnegr
   Select codmone, sum(mtototal)
   from dbo.sce_plan
   where cencos = @cencos
   and codusr = @codusr
   and fechaventa > convert(SMALLDATETIME,@fecmin)
   and fechaventa < convert(SMALLDATETIME,@fecmax)
   and estado in(4,5,6)
   and indanula in(0,2)
   group by codmone

   Insert #plnegrt
   Select codmnd, sum(mtoegr)
   from #plnegr group by(codmnd)

   Insert #cnvdeb
   Select  codmon, sum(mtomcd)
   from dbo.sce_mcd
   where cencos = @cencos
   and codusr = @codusr
   and fecmov >= dateadd(dd,0,@fechamc)
   and fecmov < dateadd(dd,+1,@fechamc)
   and nemcta = ''CONV''
   and cod_dh = ''D''
   and estado <> 9
   group by codmon

   Insert #cnvhab
   Select  codmon, sum(mtomcd)
   from dbo.sce_mcd
   where cencos = @cencos
   and codusr = @codusr
   and fecmov >= dateadd(dd,0,@fechamc)
   and fecmov <  dateadd(dd,+1,@fechamc)
   and nemcta = ''CONV''
   and cod_dh = ''H''
   and estado <> 9
   group by codmon

   Insert #plningegrt
   Select i.codmnd, i.mtoingt, e.mtoegrt
   from #plningt i, #plnegrt e
   where i.codmnd = e.codmnd

   Insert #plningegrt
   Select codmnd, mtoingt, 0
   from #plningt
   where codmnd not in(select codmnd from #plnegrt)

   Insert #plningegrt
   Select codmnd, 0, mtoegrt
   from #plnegrt
   where codmnd not in(select codmnd from #plningt)

   Insert #cnvdebhab
   Select d.codmnd, d.mtodeb, h.mtohab
   from #cnvdeb d, #cnvhab h
   where d.codmnd = h.codmnd

   Insert #cnvdebhab
   Select codmnd, mtodeb, 0
   from #cnvdeb
   where codmnd not in(select codmnd from #cnvhab)

   Insert #cnvdebhab
   Select codmnd, 0, mtohab
   from #cnvhab
   where codmnd not in(select codmnd from #cnvdeb)

   create table #cnvpln
   (
      codmnd          SMALLINT,
      mtoingt         NUMERIC(15,2),
      mtoegrt         NUMERIC(15,2),
      mtodeb          NUMERIC(15,2),
      mtohab          NUMERIC(15,2),
      difdeb          NUMERIC(15,2),
      difhab          NUMERIC(15,2)
   )

   Insert #cnvpln
   Select p.codmnd, p.mtoingt, p.mtoegrt, c.mtodeb, c.mtohab, 0, 0
   from #plningegrt p, #cnvdebhab c
   where p.codmnd = c.codmnd

   Insert #cnvpln
   Select codmnd, mtoingt, mtoegrt, 0, 0, 0, 0
   from #plningegrt
   where codmnd not in(select codmnd from #cnvdebhab)

   Insert #cnvpln
   Select codmnd, 0, 0, mtodeb, mtohab, 0, 0
   from #cnvdebhab
   where codmnd not in(select codmnd from #plningegrt)

   update #cnvpln set difdeb = mtoegrt -mtodeb,difhab = mtoingt -mtohab

   Select   codmnd,
		mtoegrt,
		mtoingt,
		mtodeb,
		mtohab,
		difdeb,
		difhab
   from #cnvpln


end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_gpln_s13_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s13_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_gpln_s13_MS] 
	@cencos         CHAR(3),
	@fecing         CHAR(10) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

   declare @fecmin  datetime,@fecmax  datetime,@fecpro  datetime

   select   @fecpro = SUBSTRING(@fecing,4,2)+''/''+SUBSTRING(@fecing,1,2)+''/''+SUBSTRING(@fecing,7,4)
   select   @fecmin = convert(CHAR(10),dateadd(dd,-1,@fecpro),101)+'' ''+''23:59:00''
   select   @fecmax = convert(CHAR(10),dateadd(dd,1,@fecpro),101)+'' ''+''00:00:01''


	-- Para Entity Framework
	if (@cencos is null and @fecing is null)
	begin
		select  
			numpre,
			fecpre,
			cencos,
			codusr,
			codcct+codpro+codesp+codofi+codope operacion ,
			rutexp,
			tippln,
			codmnd,
			mtoliq,
			mtodol,
			''I'' ingegr
		from 
			dbo.sce_xplv
		where 
			1 = 0

		return
	end


   select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope operacion ,
		rutexp,
		tippln,
		codmnd,
		mtoliq,
		mtodol,
		''I'' ingegr
   into #pln_99
   from dbo.sce_xplv
   where cencos = @cencos
   and codusr < ''99''
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and plnest = 0
   and tippln in(401,407,500)

   Insert #pln_99
   select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope,
		rutexp,
		tippln,
		codmnd,
		mtoliq,
		afimtod,
		''I''
   from dbo.sce_xplv
   where cencos = @cencos
   and codusr < ''99''
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and plnest = 0
   and tippln	= 403

   Insert #pln_99
   Select  numpli,
		fecpli,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope,
		rutcli,
		codoci,
		codmnd,
		mtoope,
		mtodol,
		convert(CHAR(1),tippln)
   from dbo.sce_pli
   where cencos = @cencos
   and codusr < ''99''
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and tippln <= 4       
      
   insert #pln_99
   Select  numpre,
		fecpre,
		cencos,
		codusr,
		codcct+codpro+codesp+codofi+codope,
		rutexp,
		tippln,
		codmnd,
		mtoanu,
		mtodola,
		''E''
   from dbo.sce_xanu
   where cencos = @cencos
   and codusr < ''99''
   and fecing > @fecmin
   and fecing < @fecmax
   and estado <> 9
   and plnest = 0
   and tippln in(401,403,407,500)

   insert #pln_99
   select  convert(CHAR(7),num_presen),
		fechaventa,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz,
		rut,
		300,
		codmone,
		mtototal,
		totaldolar,
		''E''
   from dbo.sce_plan
   where fechaventa > @fecmin
   and fechaventa < @fecmax
   and cencos     = @cencos
   and estado     < 8
   and hayanula   = 0
   and indanula   < 3	  
        

   insert #pln_99
   select  convert(CHAR(7),num_presen),
		fechaventa,
		cencos,
		codusr,
		cent_costo+id_product+id_especia+id_empresa+id_cobranz,
		rut,
		300,
		codmone,
		mtototal,
		totaldolar,
		''I''
   from dbo.sce_plan
   where fechaanula > @fecmin
   and fechaanula < @fecmax
   and cencos     = @cencos
   and estado     < 8
   and hayanula   = 1
   and indanula   < 3      

   update #pln_99 set ingegr = ''I''  where   ingegr in(''1'',''4'')

   update #pln_99 set ingegr = ''E''  where   ingegr in(''2'',''3'')

   update #pln_99 set tippln = tippln -5  where   tippln in(245,235,225,215,145,135,125,115)



   select   numpre,
		fecpre,
		tippln,
		operacion,
		cencos,
		codusr,
		rutexp,
		codmnd,
		mtoliq,
		mtodol,
		ingegr
   from #pln_99 order by tippln,codmnd,operacion

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_gpln_s16_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s16_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_gpln_s16_MS]
	@cencos CHAR(3)
	,@fecha DATETIME
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	SELECT a.codcct
		,a.codpro
		,a.codesp
		,a.codofi
		,a.codope
		,a.nrorpt
		,a.fecmov
		,a.cencos
		,a.codusr
		,a.estado
		,a.nemcta
		,a.numcta
		,a.codmon
		,a.nemmon
		,a.mtomcd
		,a.cod_dh
	FROM dbo.sce_mcd a
	WHERE 
		a.fecmov = @fecha AND
		a.cencos = @cencos AND
		a.numcta = (select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp = ''47'')
		AND a.estado <> 9
		AND NOT EXISTS (
			SELECT TOP 1 1
			FROM dbo.sce_plan b
			WHERE b.cent_costo = a.codcct
				AND b.id_product = a.codpro
				AND b.id_especia = a.codesp
				AND b.id_empresa = a.codofi
				AND b.id_cobranz = a.codope
				AND b.cencos = a.cencos
				AND b.fechaventa = a.fecmov
				AND b.codmone = a.codmon
				AND b.indanula IN (
					3
					,4
					)
				AND b.estado IN (
					4
					,5
					,6
					)
			)
		AND NOT EXISTS (
			SELECT TOP 1 1
			FROM dbo.sce_pli c
			WHERE c.codcct = a.codcct
				AND c.codpro = a.codpro
				AND c.codesp = a.codesp
				AND c.codofi = a.codofi
				AND c.codope = a.codope
				AND c.tippln IN (
					1
					,2
					,8
					,9
					)
				AND c.cencos = a.cencos
				AND c.fecpli = a.fecmov
				AND c.codmnd = a.codmon
				AND c.estado <> 9
			)
		AND NOT EXISTS (
			SELECT TOP 1 1
			FROM dbo.sce_xplv d
			WHERE d.codcct = a.codcct
				AND d.codpro = a.codpro
				AND d.codesp = a.codesp
				AND d.codofi = a.codofi
				AND d.codope = a.codope
				AND d.cencos = a.cencos
				AND d.fecpre = a.fecmov
				AND (
					(
						d.codmnd = a.codmon
						AND d.tippln IN (
							500
							,511
							)
						)
					OR (
						d.afimnd = a.codmon
						AND d.tippln IN (
							401
							,402
							,403
							,407
							)
						)
					)
				AND d.estado <> 9
				AND d.plnest = 1
			)
		AND NOT EXISTS (
			SELECT TOP 1 1
			FROM dbo.sce_xanu e
			WHERE e.codcct = a.codcct
				AND e.codpro = a.codpro
				AND e.codesp = a.codesp
				AND e.codofi = a.codofi
				AND e.codope = a.codope
				AND e.cencos = a.cencos
				AND e.fecpre = a.fecmov
				AND e.codmnd = a.codmon
				AND e.estado <> 9
				AND e.plnest = 1
			)
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_grdo_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grdo_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_grdo_d01_MS] @codcct CHAR(3)        ,
	@codpro	CHAR(2)        ,
	@codesp CHAR(2)        ,
	@codofi CHAR(3)        ,
	@codope CHAR(5) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:06:06 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
begin tran
		
   delete from dbo.sce_grdo
   where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope


   if @@error = 0
   begin
      commit tran
      select   0
   end
else
   begin
      rollback 
      select   9
   end
   return
END




' 
END

/****** Object:  StoredProcedure [dbo].[sce_grdo_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grdo_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_grdo_s01_MS] @codcct CHAR(3)        ,
	@codpro	CHAR(2)        ,
	@codesp CHAR(2)        ,
	@codofi CHAR(3)        ,
	@codope CHAR(5) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:06:06 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   select   numdec  ,
		fecdec  ,
		parrel  ,
		cubfob  ,
		cubfte  ,
		cubseg  ,
		cubmer from dbo.sce_grdo
   where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

   return
END




' 
END

/****** Object:  StoredProcedure [dbo].[sce_grio_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grio_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_grio_d01_MS] @codcct     CHAR(3)        ,
	@codpro     CHAR(2)        ,
	@codesp     CHAR(2)        ,
	@codofi     CHAR(3)        ,
	@codope     CHAR(5) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:52:52 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
begin tran
   delete from dbo.sce_grio
   where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope


   if @@error = 0
   begin
      commit tran
      select   0
   end
else
   begin
      rollback 
      select   9
   end
   return
END




' 
END

/****** Object:  StoredProcedure [dbo].[sce_grio_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_grio_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_grio_s01_MS] @codcct     CHAR(3),
	@codpro     CHAR(2),
	@codesp     CHAR(2),
	@codofi     CHAR(3),
	@codope     CHAR(5) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:06:06 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   select   numidi,
		fecidi,
		parrel,
		mtocub
   from    dbo.sce_grio
   where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
   return
END




' 
END

/****** Object:  StoredProcedure [dbo].[sce_impflag_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_impflag_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_impflag_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   SELECT   cobra_imp
   FROM dbo.sce_impflag

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ini_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ini_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ini_s01_MS] 
		@grupo         CHAR(15),
        @eleme         CHAR(15) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   tipov,largo,decim,valor 
   from dbo.sce_ini 
   where
   grupo = @grupo and
   eleme = @eleme

   Return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_inpl_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_inpl_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_inpl_d01_MS] 
	@cent_costo     CHAR(3),
	@id_product     CHAR(2),
	@id_especia     CHAR(2),
	@id_empresa     CHAR(3),
	@id_cobranz     CHAR(5),
	@numplan        NUMERIC(10,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   begin tran
   delete from dbo.sce_inpl
   where   cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   numplan = @numplan 
	
   if (@@error = 0)
   begin
      commit tran
      select   0
   end
else
   begin
      rollback 
      select   9
   end
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_inpl_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_inpl_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_inpl_s01_MS] 
			@cencos 	CHAR(3),
			@codpro		CHAR(2),
			@codesp		CHAR(2),
			@codofi		CHAR(3),
			@codope		CHAR(5),
			@nropln 	NUMERIC(10,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	select   numplan,
	fecha  ,
	nro    ,
	concepto,
	tipo   ,
	monto  ,
	capbas ,
	codbas ,
	tasa   ,
	fini   ,
	ffin   ,
	ndias
   from dbo.sce_inpl
   where 	cent_costo = @cencos and
   id_product = @codpro and
   id_especia = @codesp and
   id_empresa = @codofi and
   id_cobranz = @codope and
   numplan    = @nropln

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_inpl_w01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_inpl_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_inpl_w01_MS] 
	@cent_costo     CHAR(3),
	@id_product     CHAR(2),
	@id_especia     CHAR(2),
	@id_empresa     CHAR(3),
	@id_cobranz     CHAR(5),
	@numplan        NUMERIC(10,0),
	@fecha          SMALLDATETIME,
	@nro            NUMERIC(2,0),
	@concepto       NUMERIC(2,0),
	@tipo           CHAR(10),
	@monto          NUMERIC(15,2),
	@capbas         NUMERIC(15,2),
	@codbas         NUMERIC(3,0),
	@tasa           NUMERIC(9,6),
	@fini           SMALLDATETIME,
	@ffin           SMALLDATETIME,
	@ndias          NUMERIC(5,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   begin tran
   if not exists(SELECT TOP 1 1 from dbo.sce_inpl
   where   cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   numplan = @numplan and
   fecha = @fecha and nro = @nro)
   begin
      insert into dbo.sce_inpl(cent_costo      ,
				id_product      ,
				id_especia      ,
				id_empresa      ,
				id_cobranz      ,
				numplan         ,
				fecha           ,
				nro             ,
				concepto        ,
				tipo            ,
				monto           ,
				capbas          ,
				codbas          ,
				tasa            ,
				fini            ,
				ffin            ,
				ndias)
		values(@cent_costo     ,
				@id_product     ,
				@id_especia     ,
				@id_empresa     ,
				@id_cobranz     ,
				@numplan        ,
				@fecha          ,
				@nro            ,
				@concepto       ,
				@tipo           ,
				@monto          ,
				@capbas         ,
				@codbas         ,
				@tasa           ,
				@fini           ,
				@ffin           ,
				@ndias)
   end
else
   begin
      update dbo.sce_inpl set concepto = @concepto,tipo    = @tipo,monto   = @monto,capbas  = @capbas,
      codbas  = @codbas,tasa    = @tasa,fini    = @fini,ffin    = @ffin,ndias   = @ndias  where   cent_costo = @cent_costo and
      id_product = @id_product and
      id_especia = @id_especia and
      id_empresa = @id_empresa and
      id_cobranz = @id_cobranz and
      numplan = @numplan and
      fecha = @fecha and nro = @nro
   end             

   if (@@rowcount > 0 and @@error = 0)
   begin
      commit tran
      select   0
   end
else
   begin
      rollback 
      select   9
   end   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_intr_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_intr_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_intr_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   codintr,
		desintr
   from 	dbo.sce_intr 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_jprt_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_jprt_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_jprt_s02_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



     --Cartas de Credito.-	
   select   codprt,
		indnom,
		inddir
   from dbo.sce_jprt where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   posprt = 0

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_i01_MS]
	@codcct  CHAR(3),
	@codpro  CHAR(2),
	@codesp  CHAR(2),
	@codofi  CHAR(3),
	@codope  CHAR(5),
	@codneg  NUMERIC(2,0),
	@codsec  NUMERIC(2,0),
	@nrorpt  NUMERIC(8,0),
	@fecmov  datetime,
	@cencos	 CHAR(3),
	@codusr	 CHAR(2),
	@nroimp  NUMERIC(2,0),
	@estado  NUMERIC(2,0),
	@tipmcd  CHAR(1),
	@idncta  NUMERIC(3,0),
	@nemcta  CHAR(15),
	@numcta  CHAR(8),
	@codmon  NUMERIC(3,0),
	@nemmon  CHAR(3),
	@mtomcd  NUMERIC(15,2),
	@cod_dh  CHAR(1),
	@numemb  NUMERIC(3,0),
	@prtcli  CHAR(12),
	@indcli  NUMERIC(2,0),
	@rutcli  CHAR(10),
	@prtbco  CHAR(12),
	@indbco  NUMERIC(2,0),
	@rutbco  CHAR(10),
	@swibco  CHAR(12),
	@codbco  NUMERIC(4,0),
	@numcct  CHAR(15),
	@numlin  CHAR(10),
	@fecori  datetime,
	@fecven  datetime,
	@fecint  datetime,
	@tasfij  BIT,
	@mtotas  NUMERIC(9,6),
	@ofides  NUMERIC(3,0),
	@numpar  NUMERIC(8,0),
	@tipmov  NUMERIC(1,0),
	@nroref  CHAR(15),	@tipcam  NUMERIC(11,4),
	@nrotop  NUMERIC(6,0),
	@indtop  NUMERIC(2,0),
	@intcit  BIT,
	@intcvt  BIT,
	@intcap  BIT,
	@intctd  BIT,
	@intpos  BIT,
	@intcdr  BIT,
	@mcdvig  BIT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
      


   if (@mtomcd < 0)
   begin
      select   9
      return
   end

	-- Se cambia CCE por CCEBAE para movimientos BAE de Cta. Cte. M/E.-
	--if @nemcta= "CCE" and substring(@numcct,1,8)=@numcct
	--begin
	   --select @nemcta = "CCEBAE"
	   --select @numcta = "22170155"
	--end
	-- FTF: 24/07/2002 a solicitud Sr. C. Tapia B.
	-- Se elimina el codigo anterior con la fusion de la Cta. Cte. M/E


   if @swibco = ''BCHIUS3MXXX''
   begin
      select   @swibco = ''BCHIUS33XXX''
      select   @codbco = 532
   end

begin tran
   insert into dbo.sce_mcd values(@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@codneg,
		@codsec,
		@nrorpt,
		@fecmov,
		@cencos,
		@codusr,
		@nroimp,
		@estado,
		@tipmcd,
		@idncta,
		 upper(@nemcta),
		 upper(@numcta),
		@codmon,
		@nemmon,
		@mtomcd,
		@cod_dh,
		@numemb,
		@prtcli,
		@indcli,
		@rutcli,
		@prtbco,
		@indbco,
		@rutbco,
		@swibco,
		@codbco,
		@numcct,
		@numlin,
		@fecori,
		@fecven,
		@fecint,
		@tasfij,
		@mtotas,
		@ofides,
		@numpar,
		@tipmov,
		@nroref,
		@tipcam,
		@nrotop,
		@indtop,
		@intcit,
		@intcvt,
		@intcap,
		@intctd,
		@intpos,
		@intcdr,
		@mcdvig,
		 0,		 -- Cta Cte Batch
		 '''',		 -- Rut Ais que inyecto Cta Cte Linea
		 NULL  -- Numero Factura)
		 )
     
   if @@rowcount > 0 and @@error = 0
   begin
      commit tran
      select   0        
   end
else
   begin
      rollback 
      select   9
   end
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s04_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


-------------------------------
-- CREACION DE TABLA TEMPORAL
---------------------------------

   create table #consulta 
   (
      codcct  CHAR(3),
      codpro  CHAR(2),
      codesp  CHAR(2),
      codofi  CHAR(3),
      codope  CHAR(5),
      nrofac    NUMERIC(10,0),
      nrorpt    NUMERIC(10,0),
      fecfac    datetime,
      netofac   NUMERIC(13,2),
      ivafac    NUMERIC(13,2),
      montofac  NUMERIC(13,2),
      monedafac NUMERIC(3,0),
      tipofac   CHAR(1)
   )
   begin
      if @codcct = ''829''
      begin
         insert #consulta
         select @codcct, @codpro, @codesp, @codofi, @codope, nrofac, nrorpt, fecfac, netofac, ivafac, montofac, monedafac, tipofac
         from dbo.sce_fact
         where convert(INT,(''00''+@codofi+@codope)) = dbo.sce_fact.nrorpt
      end
      if @codcct <> ''829''
      begin
         insert #consulta
         select @codcct, @codpro, @codesp, @codofi, @codope, nrofac, nrorpt, fecfac, netofac, ivafac, montofac, monedafac, tipofac
         from dbo.sce_fact
         where convert(INT,(''00''+@codesp+''0''+@codope)) = dbo.sce_fact.nrorpt
      end
      DELETE #consulta FROM #consulta A, dbo.sce_mcd B
      where (A.nrofac = B.nrofac)
      DELETE #consulta FROM #consulta C, dbo.sce_mcdh H
      where (C.nrofac = H.nrofac)
      select * from #consulta
   end
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s07_MS]
    @nrorpt         NUMERIC(8,0),
	@fecmov         datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   create table #tmpmcd
   (
      nroimp  NUMERIC(3,0), --Akzio Migracion 201503 :Se agrega para establecer orden de informe contable
      codmon  NUMERIC(3,0),
      nemcta  CHAR(15),
      nemmon  CHAR(3),
      numcta  CHAR(8),
      idncta  NUMERIC(3,0),
      cod_dh  CHAR(1),
      mtomcd  NUMERIC(15,2),
      prtcli  CHAR(12),
      rutcli  CHAR(10),
      swibco  CHAR(12),
      numcct  CHAR(15),
      ofides  NUMERIC(3,0),
      numpar  NUMERIC(8,0),
      tipmov  NUMERIC(1,0),
      nroref  CHAR(15),
      tipcam  NUMERIC(11,4),
      fecven  datetime
   )
   insert into #tmpmcd
   select  nroimp,--Akzio Migracion 201503 : Se agrega para establecer orden de informe contable
        codmon	,
		nemcta	,
		nemmon	,
		numcta	,
		idncta	,
		cod_dh	,
		mtomcd	,
		prtcli	,
		rutcli	,
		swibco	,
		numcct	,
		ofides	,
        numpar  ,
		tipmov	,
		nroref	,
		tipcam	,
		fecven
   from dbo.sce_mcd where
   nrorpt = @nrorpt and
   fecmov = @fecmov
   order by nroimp

   insert into #tmpmcd
   select  nroimp, -- Akzio Migracion 201503 : Se agrega para establecer orden de informe contable
           codmon  ,
                nemcta  ,
                nemmon  ,
                numcta  ,
                idncta  ,
                cod_dh  ,
                mtomcd  ,
                prtcli  ,
                rutcli  ,
                swibco  ,
                numcct  ,
                ofides  ,
                numpar  ,
                tipmov  ,
                nroref  ,
                tipcam  ,
                fecven
   from dbo.sce_mcdh where
   nrorpt = @nrorpt and
   fecmov = @fecmov
   order by nroimp               

   select 
   codmon  ,
      nemcta  ,
      nemmon  ,
      numcta  ,
      idncta  ,
      cod_dh  ,
      mtomcd  ,
      prtcli  ,
      rutcli  ,
      swibco  ,
      numcct  ,
      ofides  ,
      numpar  ,
      tipmov  ,
      nroref  ,
      tipcam  ,
      fecven  
	  from #tmpmcd 
   order by nroimp -- Akzio Migracion 201503 : Se agrega orden para reporte contable
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s14_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s14_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s14_MS]
	@codcct       CHAR(3),                               
	@codpro       CHAR(2),                               
	@codesp       CHAR(2),                               
	@codofi       CHAR(3),                               
	@codope       CHAR(5),
	@tipope	NUMERIC(1,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @mtocom		NUMERIC(15,2),@mtovta		NUMERIC(15,2),@conv_h		NUMERIC(15,2),@conv_d		NUMERIC(15,2),
   @mtoliq		NUMERIC(15,2),@retorno 	NUMERIC(1,0)
   select   @retorno = 0

/*********************************************************************
  Realizo las sumas de las conversiones tanto al debe como al haber
**********************************************************************/

   select   @conv_d =(select sum(mtomcd) from dbo.sce_mcd
      where   codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      nemcta = ''CONV''         and
      cod_dh = ''D'')                   
                                                                
   select   @conv_h =(select sum(mtomcd) from dbo.sce_mcd
      where   codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      nemcta = ''CONV''         and
      cod_dh = ''H'')                   

   if @conv_d IS NULL
      select   @conv_d = 0   
                                
   if @conv_h IS NULL
      select   @conv_h = 0   


                                
/*********************************************************************
  Realizo el proceso si es una Compra / Venta de Divisas
**********************************************************************/

   if @tipope = 1
   begin
      select   @mtocom =(select sum(mtocov) from dbo.sce_cov
         where	codcct = @codcct	and
         codpro = @codpro	and
         codesp = @codesp	and
         codofi = @codofi	and
         codope = @codope	and
         tipcov = ''C'')
      select   @mtovta =(select sum(mtocov) from dbo.sce_cov
         where   codcct = @codcct        and
         codpro = @codpro        and
         codesp = @codesp        and
         codofi = @codofi        and
         codope = @codope        and
         tipcov = ''V'')
      if @mtocom IS NULL
         select   @mtocom = 0
      if @mtovta IS NULL
         select   @mtovta = 0
      if @mtocom <> @conv_h
         select   @retorno = 9
      if @mtovta <> @conv_d
         select   @retorno = 9
   end


/********************************************************************* 
  Realizo el proceso para un arbitraje               
**********************************************************************/

   if @tipope = 2
   begin
      select   @mtocom =(select sum(mtocom) from dbo.sce_arb
         where   codcct = @codcct        and
         codpro = @codpro        and
         codesp = @codesp        and
         codofi = @codofi        and
         codope = @codope)
      select   @mtovta =(select sum(mtovta) from dbo.sce_arb
         where   codcct = @codcct        and
         codpro = @codpro        and
         codesp = @codesp        and
         codofi = @codofi        and
         codope = @codope)
      if @mtocom IS NULL
         select   @mtocom = 0
      if @mtovta IS NULL
         select   @mtovta = 0
      if @mtocom <> @conv_h
         select   @retorno = 9
      if @mtovta <> @conv_d
         select   @retorno = 9
   end


/********************************************************************* 
  Realizo proceso de visible exportaciones                                  
**********************************************************************/


   if @tipope = 3
   begin
      select   @mtoliq =(select mtoliq from dbo.sce_vex
         where   codcct = @codcct        and
         codpro = @codpro        and
         codesp = @codesp        and
         codofi = @codofi        and
         codope = @codope)
      if @mtoliq IS NULL
         select   @mtoliq = 0
      if @mtoliq <> @conv_h
         select   @retorno = 9
   end
	

   return @retorno


   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s15_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s15_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s15_MS]
	@codcct       CHAR(3),                           
	@codpro       CHAR(2),                           
	@codesp       CHAR(2),                           
	@codofi       CHAR(3),                           
	@codope       CHAR(5),
	@codanu       CHAR(6),
	@nrorpt       NUMERIC(8,0),
	@fecmov       datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @mtoconv_h      NUMERIC(15,2),  --Conversion HABER.-
	@mtoconv_d      NUMERIC(15,2),  --Conversion DEBE.-
	@mtoliq400      NUMERIC(15,2),  --PlnVis 400''s.-
   @mtoliq500      NUMERIC(15,2),  --PlnVis 500''s.-
	@mtoanu         NUMERIC(15,2),  --PlnAnu.-
   @mtocom         NUMERIC(15,2),  --PlnInv Compra(1).-
	@mtovta         NUMERIC(15,2),  --PlnInv Venta (2).-
   @mtocom_anu     NUMERIC(15,2),  --PlnInv Compra Anulacion (1).-
	@mtovta_anu     NUMERIC(15,2),  --PlnInv Venta  Anulacion (2).-
	@resultado      NUMERIC(1,0)

/***********************************************************************  
  Valida planillas visibles : CONVERSION AL HABER.-
************************************************************************/

	--CONVERSION al HABER.-
   select   @mtoconv_h =(select sum(mtomcd) from dbo.sce_mcd
      where nrorpt = @nrorpt       and
      fecmov = @fecmov       and
      cod_dh = ''H''           and
      nemcta = ''CONV'')


	--CONVERSION al DEBE.-
   select   @mtoconv_d =(select sum(mtomcd) from dbo.sce_mcd
      where nrorpt = @nrorpt       and
      fecmov = @fecmov       and
      cod_dh = ''D''           and
      nemcta = ''CONV'')           
	

	--Planillas Visible Export (400''s).-
   select   @mtoliq400 =(select sum(afimto) from dbo.sce_xplv where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      codanu = @codanu        and
      plnest = 0		AND
				(tippln = 401  or
      tippln = 403  or
      tippln = 407))


	--Planillas Visible Export (500).-
   select   @mtoliq500 =(select sum(mtoliq) from dbo.sce_xplv where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      codanu = @codanu        and
      plnest = 0		and
      tippln = 500)
	

	--Planillas Anulacion Expor (400''s y 500).-
   select   @mtoanu   =(select sum(mtoanu) from dbo.sce_xanu where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      plnest = 0		and
				(tippln = 401  or
      tippln = 403  or
      tippln = 407  or
      tippln = 500))

	--Planillas Invisible Compra (1).-
   select   @mtocom  =(select sum(mtoope) from dbo.sce_pli where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      codanu = @codanu        and
      tippln = 1)
	

	--Planillas Invisible Compra (1) Anulaci>n.-
   select   @mtocom_anu  =(select sum(mtoope) from dbo.sce_pli where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      codanu = @codanu        and
      tippln = 3)


	--Planillas Invisible Venta (2).-
   select   @mtovta  =(select sum(mtoope) from dbo.sce_pli where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      codanu = @codanu        and
      tippln = 2)


	--Planillas Invisible Venta (2) Anulaci>n.-
   select   @mtovta_anu  =(select sum(mtoope) from dbo.sce_pli where
      codcct = @codcct        and
      codpro = @codpro        and
      codesp = @codesp        and
      codofi = @codofi        and
      codope = @codope        and
      codanu = @codanu        and
      tippln = 4)


	--Inicializacion de Variables.-
   if @mtoconv_h IS NULL
      select   @mtoconv_h = 0

   if @mtoconv_d IS NULL
      select   @mtoconv_d = 0

   if @mtoliq400 IS NULL
      select   @mtoliq400 = 0

   if @mtoliq500 IS NULL
      select   @mtoliq500 = 0

   if @mtoanu IS NULL
      select   @mtoanu = 0

   if @mtocom IS NULL
      select   @mtocom = 0

   if @mtovta IS NULL
      select   @mtovta = 0

   if @resultado IS NULL
      select   @resultado = 0


	--CONVERSION HABER = MTOLIQ(400''S) + MTOLIQ(500) + MTOOPE(INV/1)
   if @mtoconv_h <>(@mtoliq400+@mtoliq500+@mtocom+@mtovta_anu)
      select   @resultado = 9
	
	--CONVERSION DEBE  = MTOANU + MTOOPE(INV/2)
   if @mtoconv_d <>(@mtoanu+@mtovta+@mtocom_anu)
      select   @resultado = 9
	 
   return @resultado

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s20_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s20_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s20_MS] 
	@fecmov         datetime,
	@numcta         CHAR(8) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	/* Para Entity Framework */
	if (@fecmov = null and @numcta = null)
	begin
		select
			CAST(null as CHAR(15)) as codope,
			CAST(null as CHAR(5)) as codusr,
			CAST(null as CHAR(3)) as nemmon,
			CAST(null as NUMERIC(15,2)) as mtomcd,
			CAST(null as CHAR(1)) as cod_dh,
			CAST(null as CHAR(15)) as numcct,
			CAST(null as CHAR(10)) as rutcli,
			CAST(null as CHAR(3)) as oficon,
			CAST(null as NUMERIC(2,0)) estado			
		return
	end

   create table #tmpmcd1
   (
      codope  CHAR(15), --Toda la Operaci¢n
      codusr  CHAR(5), --cencos + codusr
      nemmon  CHAR(3),
      mtomcd  NUMERIC(15,2),
      cod_dh  CHAR(1),
      numcct  CHAR(15),
      rutcli  CHAR(10),
      oficon	CHAR(3),
      estado  NUMERIC(2,0)
   )

   insert #tmpmcd1
   select  codcct+codpro+codesp+codofi+codope,
		cencos+codusr,
		nemmon,
		mtomcd,
		cod_dh,
		numcct,
		rutcli,
		codcct,
		estado
   from  dbo.sce_mcd
   where fecmov = @fecmov
   and numcta = @numcta

   if @@rowcount = 0
   begin
      insert #tmpmcd1
      select codcct+codpro+codesp+codofi+codope,
                 cencos+codusr,
                 nemmon,
                 mtomcd,
                 cod_dh,
                 numcct,
                 rutcli,
	             codcct,
                 estado
      from  dbo.sce_mcdh
      where fecmov = @fecmov
      and numcta = @numcta
   end

   update #tmpmcd1 set oficon = ''500''  where oficon in(''729'',''826'',''827'',''829'',''472'')
		
   select codope,
      codusr,
      nemmon,
      mtomcd,
      cod_dh,
      numcct,
      rutcli,
      oficon,
      estado from #tmpmcd1
   order by oficon,cod_dh,mtomcd desc
   
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s56_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s56_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s56_MS] 
	@fecing datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	-- para entity framework
	if @fecing is null
	begin
		select
			CAST(null as CHAR(15)) AS operacion,
			CAST(null as CHAR(30)) AS nomcli,
			CAST(null as CHAR(8)) AS numcta,
			CAST(null as CHAR(15)) AS nemcta,
			CAST(null as CHAR(3)) AS cencos,
			CAST(null as CHAR(2)) AS codusr,
			CAST(null as CHAR(3)) AS nemmon,
			CAST(null as NUMERIC(15,2)) AS mtohab,
			CAST(null as NUMERIC(15,2)) AS mtodeb
		return
	end


	create table #tmp
   (
      codcct			CHAR(3) collate Latin1_General_100_CS_AS,
      codpro			CHAR(2) collate Latin1_General_100_CS_AS,
      codesp			CHAR(2) collate Latin1_General_100_CS_AS,
      codofi			CHAR(3) collate Latin1_General_100_CS_AS,
      codope			CHAR(5) collate Latin1_General_100_CS_AS,
      cencos			CHAR(3) collate Latin1_General_100_CS_AS,
      codusr			CHAR(2) collate Latin1_General_100_CS_AS,
      nemmon			CHAR(3) collate Latin1_General_100_CS_AS,
      nemcta			CHAR(15) collate Latin1_General_100_CS_AS,
      numcta			CHAR(8) collate Latin1_General_100_CS_AS,
      prtcli 			CHAR(12) collate Latin1_General_100_CS_AS,
      indcli 			NUMERIC(2,0),
      nomcli			CHAR(30) collate Latin1_General_100_CS_AS,
      mtodeb			NUMERIC(15,2),
      mtohab			NUMERIC(15,2),
      nroimp			NUMERIC(3,0),   -- correlativo
      nrorpt			NUMERIC(8,0),
      fecmov			datetime
   )

   insert #tmp
   select
   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		cencos,
		codusr,
		nemmon,
		nemcta,
		numcta,
		prtcli,
		indcli,
		'''',
		mtomcd,	-- DEBE.-
		0,	-- HABER.-
		nroimp,	-- correlativo
		nrorpt,
		fecmov
   from  dbo.sce_mcd
   where fecmov = @fecing
   and cod_dh = ''D''
    and rtrim(ltrim(nemcta)) in(''CANJE$'',''CHEBCHI$'',''VV$'',''CHEBCEN'',''CANJEE'',''COE'',''ACECANJE'',''VALEVISTA'')
    and cencos in(''729'',''827'',''826'',''829'',''472'')
    and estado <> 9


   insert #tmp
   select
   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		cencos,
		codusr,
		nemmon,
		nemcta,
		numcta,
		prtcli,
		indcli,
		'''',
		0,	-- DEBE .-
		mtomcd,	-- HABER.-
		nroimp,
		nrorpt,
		fecmov
   from    dbo.sce_mcd
   where  fecmov = @fecing
   and  cod_dh = ''H''
    and  rtrim(ltrim(nemcta)) in(''VV$'',''VALEVISTA'')
    and  cencos in(''729'',''827'',''826'',''829'',''472'')
    and  estado <> 9

  
   update #tmp set nomcli = b.nomben from #tmp a, dbo.sce_vvi b where a.codcct = b.codcct
   and a.codpro = b.codpro
   and a.codesp = b.codesp
   and a.codofi = b.codofi
   and a.codope = b.codope
   and b.fecemi = a.fecmov
    and b.estado <> 9
    and rtrim(ltrim(a.nemcta)) in(''VV$'',''VALEVISTA'')

   update #tmp set nomcli = a.nompag from dbo.sce_chq a, #tmp b where a.codcct = b.codcct
   and a.codpro = b.codpro
   and a.codesp = b.codesp
   and a.codofi = b.codofi
   and a.codope = b.codope
   and a.fecemi = b.fecmov
    and a.estado <> 9
    and rtrim(ltrim(b.nemcta)) = ''COE''

   update #tmp set nomcli = a.nomcli from 	dbo.sce_mch a, #tmp b where a.codcct = b.codcct
   and a.codpro = b.codpro
   and a.codesp = b.codesp
   and a.codofi = b.codofi
   and a.codope = b.codope
   and a.fecmov = b.fecmov
   and a.nrorpt = b.nrorpt
    and a.estado <> 9
    and rtrim(ltrim(b.nemcta)) in(''CANJE$'',''CHEBCHI$'',''CANJEE'',''ACECANJE'',''VALEVISTA'')

   update #tmp set nomcli = ''Banco Central''  where rtrim(ltrim(nemcta)) = ''CHEBCEN''


	-- Desplegamos los datos
	select
		codcct+codpro+codesp+codofi+codope as operacion,
		nomcli,
		numcta,
		nemcta,
		cencos,
		codusr,
		nemmon,
		mtohab,
		mtodeb
	from
		#tmp
	order by
		nemcta,nemmon
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s65_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s65_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[sce_mcd_s65_MS] @cuenta CHAR(15),
 @fecha datetime 

AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:34:34 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   if convert(CHAR(10),GetDate(),103) = convert(CHAR(10),@fecha,103)
   begin
      select   codcct+codpro+codesp+codofi+codope as referencia,
        	nemmon,
        	mtomcd,
        	cod_dh,
     		nrorpt,
       		numcta,
      		numcct
      from 	dbo.sce_mcd
      where 	fecmov = @fecha
      and   numcta = @cuenta
      and   estado != 9
      and   (LEN(ltrim(rtrim(numcct))) = 8 or LEN(ltrim(rtrim(numcct))) <> 8)
      order by cod_dh,mtomcd asc
   end
else
   begin
      select   codcct+codpro+codesp+codofi+codope as referencia,
        	nemmon,
        	mtomcd,
        	cod_dh,
        	nrorpt,
        	numcta,
        	numcct
      from 	dbo.sce_mcdh
      where 	fecmov = @fecha
      and   numcta = @cuenta
      and  	estado != 9
      and   (LEN(ltrim(rtrim(numcct))) = 8 or LEN(ltrim(rtrim(numcct))) <> 8)
      order by cod_dh,mtomcd asc
   end
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_s66_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s66_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create PROCEDURE [dbo].[sce_mcd_s66_MS] @nemcuenta CHAR(15),
 @fecha datetime 

AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:34:34 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   if convert(CHAR(10),GetDate(),103) = convert(CHAR(10),@fecha,103)
   begin
      select   codcct+codpro+codesp+codofi+codope as referencia,
        	nemmon,
        	mtomcd,
        	cod_dh,
		    nrorpt,
    		numcta,
    		numcct
      from 	dbo.sce_mcd
      where fecmov = @fecha
      and nemcta = @nemcuenta
      and estado != 9
      and (LEN(ltrim(rtrim(numcct))) = 8 or LEN(ltrim(rtrim(numcct))) <> 8)
      order by cod_dh,mtomcd asc
   end
else
   begin
      select   codcct+codpro+codesp+codofi+codope as referencia,
        	nemmon,
        	mtomcd,
        	cod_dh,
        	nrorpt,
        	numcta,
        	numcct
      from 	dbo.sce_mcdh
      where 	fecmov 	= @fecha
      and   nemcta = @nemcuenta
      and   estado != 9
      and   (LEN(rtrim(ltrim(numcct))) = 8 or LEN(rtrim(ltrim(numcct))) <> 8)
      order by cod_dh,mtomcd asc
   end
   return
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_mcd_u70_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_u70_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_u70_MS] @nrorpt 	NUMERIC(8,0),
        @fecmov 	datetime,
	@nroimp		NUMERIC(3,0),
	@enlinea	NUMERIC(1,0),
	@rutais		CHAR(8) 

AS
begin

   if @enlinea = 0
      select   @enlinea = 1
else if @enlinea = 1
      select   @enlinea = 0
		

   update dbo.sce_mcd set enlinea = @enlinea,rutais  = @rutais  where 	nrorpt = @nrorpt
   and	fecmov = @fecmov
   and	nroimp = @nroimp
	
   if @@error != 0 or @@rowcount = 0
	 return   -1
   else 
		return  0

end







' 
END

/****** Object:  StoredProcedure [dbo].[sce_mch_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mch_s01_MS]
	@nrorpt         NUMERIC(8,0),
	@fecmov         datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   
   select   codcct,
               codpro,
               codesp,
               codofi,
               codope,
               nrorpt,
               fecmov,
               nomcli,
               codfun,
               estado,
               desgen,
               cencos,
               codusr
   from dbo.sce_mch
   where nrorpt = @nrorpt
   and fecmov = @fecmov
   UNION ALL
   select   codcct,
                codpro,
                codesp,
                codofi,
                codope,
                nrorpt,
                fecmov,
                nomcli,
                codfun,
                estado,
                desgen,
                cencos,
                codusr
   from dbo.sce_mchh
   where nrorpt = @nrorpt
   and fecmov = @fecmov   

   return
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mch_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mch_s02_MS] 
	@fecmov         datetime,
	@cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-09   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
   SET NOCOUNT ON 


   select   codcct,
           codpro,
           codesp,
           codofi,
           codope,
           nrorpt,
           fecmov,
           codfun
   from dbo.sce_mch
   where cencos = @cencos
   and codusr = @codusr
   and fecmov = @fecmov
   UNION ALL
   select   codcct,
	        codpro,
			codesp,
			codofi,
			codope,
            nrorpt,
			fecmov,
			codfun
   from dbo.sce_mchh
   where cencos = @cencos
   and codusr = @codusr
   and fecmov = @fecmov
                            
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mch_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mch_s05_MS]
	@codcct CHAR(3),
	@codpro CHAR(2),
	@codesp CHAR(2),
	@codofi CHAR(3),
	@codope CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   select   prtcli,
                indcli
   from dbo.sce_mch
   where   codcct = @codcct
   and   codpro = @codpro
   and   codesp = @codesp
   and   codofi = @codofi
   and   codope = @codope
   UNION ALL
   select   prtcli,
                indcli
   from dbo.sce_mchh
   where   codcct = @codcct
   and   codpro = @codpro
   and   codesp = @codesp
   and   codofi = @codofi
   and   codope = @codope               

   return                                                                  
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mch_s11_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s11_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mch_s11_MS]
	@codcct  CHAR(3),
	@codpro  CHAR(2),
	@codesp  CHAR(2),
	@codofi  CHAR(3),
	@codope  CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   nrorpt,
         fecmov
   from dbo.sce_mch
   where codcct = @codcct
   and codpro = @codpro
   and codesp = @codesp
   and codofi = @codofi
   and codope = @codope
   UNION ALL
   select   nrorpt,
         fecmov
   from dbo.sce_mchh
   where codcct = @codcct
   and codpro = @codpro
   and codesp = @codesp
   and codofi = @codofi
   and codope = @codope     

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mch_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mch_u02_MS]
	@nrorpt         NUMERIC(8,0),
	@fecmov         datetime, 
	@estado         NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   

   declare	@codcct	CHAR(3),@codpro CHAR(2),@codesp CHAR(2),@codofi CHAR(3),@codope CHAR(5),
   @cencos CHAR(3),@codusr CHAR(2),@data	CHAR(30)

   declare @oficon_c CHAR(3),@oficon NUMERIC(3,0),@filler3 CHAR(3),@numreg	NUMERIC(3,0)
                           


   update dbo.sce_mcd set estado = @estado  where
   nrorpt = @nrorpt and
   fecmov = @fecmov
	
   update dbo.sce_mch set estado = @estado  where
   nrorpt = @nrorpt and
   fecmov = @fecmov

   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   begin

		-- Log Comex

      select   @codcct = codcct,
			@codpro = codpro,
			@codesp = codesp,
			@codofi = codofi,
			@codope = codope,
			@cencos = cencos,
			@codusr = codusr
      from	dbo.sce_mch
      where 	nrorpt = @nrorpt and
      fecmov = @fecmov
      if @codpro = ''10'' 		-- Es GL
      begin

			-- Oficina contable                                   
                                                      
         EXECUTE sce_ccof_s02 @codcct,@oficon_c OUTPUT
         if @oficon_c IS NULL
            select   @oficon_c = @codofi
         select   @oficon = convert(NUMERIC(3,0),@oficon_c)
         select   @numreg = 0
         select   @numreg = count(*)
         from	dbo.sce_mcd
         where 	nrorpt = @nrorpt and
         fecmov = @fecmov	and
				(ltrim(rtrim(nemcta)) = ''OPE'' 	or
         ltrim(rtrim(nemcta)) = ''BOE'' 	or
         ltrim(rtrim(nemcta)) = ''OPEPEND'')
         if @numreg = 0
         begin
            select   @filler3 = ''0''
            select   @data = ''GL sin Orden de Pago''
         end
      else
         begin
            select   @filler3 = ''1''
            select   @data = ''GL con Orden de Pago''
         end
         insert	into dbo.sce_gtlg(codproe, anulac, codfun, subproe, codcct, codpro,
			codesp, codofi, codope, fecpro, filler3, esppro,
			espown , oficon, data)
			values(''10'', 0, ''001'', ''000'', @codcct, @codpro,
			@codesp, @codofi, @codope, GetDate(), @filler3,
 			@codusr, @cencos, @oficon,
			@data)
			
         if (@@error <> 0 or @@rowcount = 0)
            return 8
      end
   end


   return 0

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mem_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mem_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mem_d01_MS] 
	@tabla VARCHAR(2) 
AS

BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @corlin	NUMERIC(3,0),@codmem NUMERIC(8,0),@num 	INT


   create table #tmpmem
   (
      codmem NUMERIC(8,0),
      corlin NUMERIC(3,0),
      num    INT
   )

   if @tabla = ''jd''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_jdme group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_jdme : '',count(*) from #tmpmem
   end

   if @tabla = ''jm''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_jmem group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_jmem : '',count(*) from #tmpmem
   end

   if @tabla = ''f''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_memf group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_memf : '',count(*) from #tmpmem
   end

   if @tabla = ''i''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_memi group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_memi : '',count(*) from #tmpmem
   end

   if @tabla = ''x''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_memx group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_memx : '',count(*) from #tmpmem
   end

   if @tabla = ''p''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_memp group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_memp : '',count(*) from #tmpmem
   end

   if @tabla = ''y''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_memy group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_memy : '',count(*) from #tmpmem
   end

   if @tabla = ''s''
   begin
      insert #tmpmem select codmem,corlin,count(*)
      from dbo.sce_mems group by codmem,corlin
      delete from #tmpmem where num = 1
      select   ''Registros sce_mems : '',count(*) from #tmpmem
   end

   declare cursor_mem cursor for
   select codmem,corlin,num
   from #tmpmem

   open cursor_mem
   fetch cursor_mem into
   @codmem,@corlin,@num

   while @@FETCH_STATUS != -1
   begin
      if @@FETCH_STATUS = -2
      begin
         select   ''Error al recorrer el cursor''
         return
      end
      if @num = 2
         set rowcount 1
      if @num = 3
         set rowcount 2
      if @num = 4
         set rowcount 3
      if @num = 5
         set rowcount 4
      if @tabla = ''jd''
         delete from dbo.sce_jdme where codmem = @codmem and corlin = @corlin
      if @tabla = ''jm''
         delete from dbo.sce_jmem where codmem = @codmem and corlin = @corlin
      if @tabla = ''f''
         delete from dbo.sce_memf where codmem = @codmem and corlin = @corlin
      if @tabla = ''i''
         delete from dbo.sce_memi where codmem = @codmem and corlin = @corlin
      if @tabla = ''x''
         delete from dbo.sce_memx where codmem = @codmem and corlin = @corlin
      if @tabla = ''p''
         delete from dbo.sce_memp where codmem = @codmem and corlin = @corlin
      if @tabla = ''y''
         delete from dbo.sce_memy where codmem = @codmem and corlin = @corlin
      if @tabla = ''s''
         delete from dbo.sce_mems where codmem = @codmem and corlin = @corlin
      set rowcount 0
      fetch cursor_mem into
      @codmem,@corlin,@num
   end
   close cursor_mem
   deallocate cursor_mem

   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_memg_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_memg_d01_MS] 
	@codtab      VARCHAR(2),
	@codmem      NUMERIC(8,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

BEGIN TRAN
   if @codtab = ''f''
   begin
      delete from dbo.sce_memf where codmem = @codmem
   end
   if @codtab = ''p''
   begin
      delete from dbo.sce_memp where codmem = @codmem
   end
   if @codtab = ''x''
   begin
      delete from dbo.sce_memx where codmem = @codmem
   end
   if @codtab = ''m''
   begin
      delete from dbo.sce_memm where codmem = @codmem
   end
   if @codtab = ''s''
   begin
      delete from dbo.sce_mems where codmem = @codmem
   end
   if @codtab = ''i''
   begin
      delete from dbo.sce_memi where codmem = @codmem
   end
   if @codtab = ''jm''
   begin
      delete from dbo.sce_jmem where codmem = @codmem
   end
   if @codtab = ''jd''
   begin
      delete from dbo.sce_jdme where codmem = @codmem
   end
   if @codtab = ''lm''
   begin
      delete from dbo.sce_lmem where codmem = @codmem
   end
   if @codtab = ''ld''
   begin
      delete from dbo.sce_ldme where codmem = @codmem
   end
   if @codtab = ''y''
   begin
      delete from dbo.sce_memy where codmem = @codmem
   end
   if @codtab = ''e''
   begin
      delete from dbo.sce_meme where codmem = @codmem
   end
   if @codtab = ''fd''
   begin
      delete from dbo.sce_fdme where codmem = @codmem
   end
   if @codtab = ''c''
   begin
      delete from dbo.cam_mem where codmem = @codmem
   end



   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1,''Error al borrar datos en Sce_Mem*''
      Return
   end
   COMMIT TRAN
   Select   0,''Borrado Exitoso''

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_memg_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_memg_i01_MS] 
	@codtab      VARCHAR(2),
	@codmem      NUMERIC(8,0),
	@corlin	     NUMERIC(3,0),
	@linmem      CHAR(255) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

BEGIN TRAN
   if @codtab = ''f''
   begin
      insert into dbo.sce_memf(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''p''
   begin
      insert into dbo.sce_memp(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''x''
   begin
      insert into dbo.sce_memx(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''s''
   begin
      insert into dbo.sce_mems(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''i''
   begin
      insert into dbo.sce_memi(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''jm''
   begin
      insert into dbo.sce_jmem(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end

   if @codtab = ''jd''
   begin
      insert into dbo.sce_jdme(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''lm''
   begin
      insert into dbo.sce_lmem(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''ld''
   begin
      insert into dbo.sce_ldme(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''y''
   begin
      insert into dbo.sce_memy(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''e''
   begin
      insert into dbo.sce_meme(codmem,corlin,linmem)
		values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''m''
   begin
      insert into dbo.sce_memm(codmem,corlin,linmem)
        	values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''fd''
   begin
      insert into dbo.sce_fdme(codmem,corlin,linmem)
            values(@codmem,@corlin,@linmem)
   end
   if @codtab = ''c''
   begin
      insert into dbo.cam_mem(codmem,corlin,linmem)
            values(@codmem,@corlin,@linmem)
   end
   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1 as column1,''Error al ingresar datos en Sce_Mem*'' as column2
      Return
   end
   COMMIT TRAN
   Select   0 as column1,''Grabación Exitosa'' as column2


end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_memg_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_memg_s01_MS] 
	@codtab      VARCHAR(2),
	@codmem      NUMERIC(8,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


--Akzio Migracion Agosto 2014
-- Se declara variable para concatenar la linea de salida.
declare @linea1 as varchar(MAX)
set @linea1 = ''''
-- This procedure was converted on Wed Apr 16 14:58:58 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/


   if @codtab = ''f''
   begin
      --Akzio Migracion 2015
      --select   linmem from dbo.sce_memf where codmem = @codmem
      --order by corlin
		begin
		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.sce_memf where codmem = @codmem
         order by corlin
		 select @linea1
		 end


--		 select   case when ascii(right(linmem,1)) = 9 then linmem + ''0''  else linmem end from dbo.sce_memf where codmem = @codmem
--         order by corlin

   end
   if @codtab = ''p''
   begin
      select   linmem from dbo.sce_memp where codmem = @codmem
      order by corlin
   end
   if @codtab = ''x''
   begin
   
      if exists(SELECT TOP 1 1 from dbo.sce_memx where codmem = @codmem)
	     --Akzio Migracion 2014
         --select   linmem from dbo.sce_memx where codmem = @codmem
		 --select   case when ascii(right(linmem,1)) = 9 and LEN(linmem) >= 255 then left(linmem,253) + char(9) + char(199)  else linmem end from dbo.sce_memx where codmem = @codmem
		 begin
		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.sce_memx where codmem = @codmem
         order by corlin
		 select @linea1
		 end
   else
      --Akzio Migracion 2014
      --select   linmem from dbo.rce_memx where codmem = @codmem
	  --select   case when ascii(right(linmem,1)) = 9 and LEN(linmem) >= 255 then left(linmem,253) + char(9) + char(199) else linmem end from dbo.rce_memx where codmem = @codmem
		 begin
		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.rce_memx where codmem = @codmem
         order by corlin
		 select @linea1
		 end

   end
   if @codtab = ''m''
   begin
      select   linmem from dbo.sce_memm where codmem = @codmem
      order by corlin
   end
   if @codtab = ''s''
   begin
      if exists(SELECT TOP 1 1 from dbo.sce_mems where codmem = @codmem)
         --select   linmem from dbo.sce_mems where codmem = @codmem
		 --select case when CHARINDEX('':*20*:'',substring(linmem,1,10),1) > 0 then ''*'' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)+ linmem  else linmem end from dbo.sce_mems where codmem = @codmem
	      --order by corlin

		 --Akzio Migracion 2015
		 begin
		 		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.sce_mems where codmem = @codmem
         order by corlin
		 select case when CHARINDEX('':*20*:'',substring(@linea1,1,10),1) > 0 then ''*'' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + @linea1  else @linea1 end
		 end
   else
      --select   linmem from dbo.rce_mems where codmem = @codmem
	  --select case when CHARINDEX('':*20*:'',substring(linmem,1,10),1) > 0 then ''*'' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)+ linmem  else linmem end from dbo.rce_mems where codmem = @codmem
      --order by corlin
		 --Akzio Migracion 2015
		 begin
		 		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.rce_mems where codmem = @codmem
         order by corlin
		 select case when CHARINDEX('':*20*:'',substring(@linea1,1,10),1) > 0 then ''*'' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + @linea1  else @linea1 end
		 end

   end
   if @codtab = ''i''
   begin
      if @codmem <> 999999
      begin
         if exists(SELECT TOP 1 1 from dbo.sce_memi where codmem = @codmem)
            --select   linmem from dbo.sce_memi where codmem = @codmem
            --order by corlin
		 --Akzio Migracion 2015
		 begin
		 		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.sce_memi where codmem = @codmem
         order by corlin
		 select @linea1
		 end
 
      else
         --select   linmem from dbo.rce_memi where codmem = @codmem
         --order by corlin
		 --Akzio Migracion 2015
		 begin
		 		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.rce_memi where codmem = @codmem
         order by corlin
		 select @linea1
		 end

      end
   end	
   if @codtab = ''jm''
   begin
      select   linmem from dbo.sce_jmem where codmem = @codmem
      order by corlin
   end
   if @codtab = ''jd''
   begin
      if exists(SELECT TOP 1 1 from dbo.sce_jdme where codmem = @codmem)
	     --Akzio Migracion 2014
         --select   linmem from dbo.sce_jdme where codmem = @codmem
         --order by corlin
		 begin
			 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.sce_jdme where codmem = @codmem
			 order by corlin
			 select @linea1
		 end
   else
      --Akzio Migracion 2014
      --select   linmem from dbo.rce_jdme where codmem = @codmem
      --order by corlin
		 begin
			 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.rce_jdme where codmem = @codmem
			 order by corlin
			 select @linea1
		 end
   end
   if @codtab = ''lm''
   begin
      select   linmem from dbo.sce_lmem where codmem = @codmem
      order by corlin
   end
   if @codtab = ''ld''
   begin
      select   linmem from dbo.sce_ldme where codmem = @codmem
      order by corlin
   end
   if @codtab = ''y''
   begin
      if exists(SELECT TOP 1 1 from dbo.sce_memy where codmem = @codmem)
         select   linmem from dbo.sce_memy where codmem = @codmem
         order by corlin
   else
      select   linmem from dbo.rce_memy where codmem = @codmem
      order by corlin
   end
   if @codtab = ''e''
   begin
      if exists(SELECT TOP 1 1 from dbo.sce_meme where codmem = @codmem)
         --select   linmem from dbo.sce_meme where codmem = @codmem
         --order by corlin
		 begin
		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.sce_meme where codmem = @codmem
         order by corlin
		 select @linea1
		 end
   else
      --select   linmem from dbo.rce_meme where codmem = @codmem
      --order by corlin
	  begin
		 select @linea1 =  @linea1 + REPLACE(linmem,char(199),'' '') from dbo.rce_meme where codmem = @codmem
         order by corlin
		 select @linea1
	   end
   end


   if @codtab = ''fd''
   begin
      select   linmem from dbo.sce_fdme where codmem = @codmem
      order by corlin
   end

   if @codtab = ''c''
   begin
      select   linmem from dbo.cam_mem where codmem = @codmem
      order by corlin
   end
   return



end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_memg_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_memg_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_memg_s03_MS] 
	@codtab      VARCHAR(2),
	@codmem      NUMERIC(8,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   if @codtab = ''f''
   begin
      select   count(*) from dbo.sce_memf where codmem = @codmem
   end
   if @codtab = ''p''
   begin
      select   count(*) from dbo.sce_memp where codmem = @codmem
   end
   if @codtab = ''x''
   begin
      select   count(*) from dbo.sce_memx where codmem = @codmem
   end
   if @codtab = ''m''
   begin
      select   count(*) from dbo.sce_memm where codmem = @codmem
   end
   if @codtab = ''s''
   begin
      select   count(*) from dbo.sce_mems where codmem = @codmem
   end
   if @codtab = ''i''
   begin
      select   count(*) from dbo.sce_memi where codmem = @codmem
   end
   if @codtab = ''jm''
   begin
      select   count(*) from dbo.sce_jmem where codmem = @codmem
   end
   if @codtab = ''jd''
   begin
      select   count(*) from dbo.sce_jdme where codmem = @codmem
   end
   if @codtab = ''lm''
   begin
      select   count(*) from dbo.sce_lmem where codmem = @codmem
   end
   if @codtab = ''ld''
   begin
      select   count(*) from dbo.sce_ldme where codmem = @codmem
   end
   if @codtab = ''y''
   begin
      select   count(*) from dbo.sce_memy where codmem = @codmem
   end
   if @codtab = ''e''
   begin
      select   count(*) from dbo.sce_meme where codmem = @codmem
   end
   if @codtab = ''fd''
   begin
      select   count(*) from dbo.sce_fdme where codmem = @codmem
   end
   if @codtab = ''c''
   begin
      select   count(*) from dbo.cam_mem where codmem = @codmem
   end
   

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta0_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta0_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta0_s01_MS]
	@codsis         CHAR(3),
    @codpro         CHAR(3),
    @codeta         CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


	SELECT [codsis]
			,[codpro]
			,[codeta]
			,[escomi]
			,[esgast]
			,[esinte]
			,[esimpu]
			,[esotro]
			,[detimp]
			,[detotr]
	FROM  dbo.sce_mta0 
	where
	codsis = @codsis and
	codpro = @codpro and
	codeta = @codeta 
	return

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta1_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta1_s01_MS] 
	@codsis         CHAR(3),
    @codpro         CHAR(3),
    @codeta         CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   codsis,
		codpro,
		codeta,
		mtofij,
		tasmin,
		tasmax,
		mtomin,
		mtomax,
		fecini,
		codmon,
		hayiva,
		cta_mn,
		cta_me
   from	dbo.sce_mta1
   where	codsis = @codsis and
   codpro = @codpro and
   codeta = @codeta 
   return

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta1_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta1_s04_MS] 
	@codsis         CHAR(3),
	@codpro         CHAR(3),
	@codeta         CHAR(3),
	@fecref         datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   cta_mn
   from dbo.sce_mta1
   where codsis = @codsis and
   codpro = @codpro and
   codeta = @codeta and
   fecini <= @fecref
   order by fecini desc
 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta1_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[sce_mta1_s05_MS] @codsis         CHAR(3),
        @codpro         CHAR(3),
        @codeta         CHAR(3) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:54:54 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
        
   select   codsis,
		codpro,
		codeta,
		mtofij,
		tasmin,
		tasmax,
		mtomin,
		mtomax,
		fecini,
		codmon,
		hayiva,
		cta_mn,
		cta_me,
		ranmin,
		ranmax,
		cormat1
   from 	dbo.sce_mta1
   where	codsis = @codsis and
   codpro = @codpro and
   codeta = @codeta 
   return

   return
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta1_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta1_s06_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   codsis,
		codpro,
		codeta,
		mtofij,
		tasmin,
		tasmax,
		mtomin,
		mtomax,
		fecini,
		codmon,
		hayiva,
		cta_mn,
		cta_me,
		ranmin,
		ranmax,
		cormat1
   from	dbo.sce_mta1
	
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta1_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta1_s07_MS] 
	@codsis         CHAR(3),
	@codpro         CHAR(3),
	@codeta         CHAR(3),
	@fecref         datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   mtofij,
		tasmin,
		tasmax,
		mtomin,
		mtomax,
		codmon,
		hayiva,
		cta_mn,
		cta_me,
		ranmin,
		ranmax,
		cormat1
   from 	dbo.sce_mta1
   where 	codsis = @codsis and
   codpro = @codpro and
   codeta = @codeta and
   fecini <= @fecref
   order by fecini desc

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta1_s09_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta1_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta1_s09_MS] 
	@codsis		CHAR(3),
	@codpro		CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select distinct  b.glocon, a.cta_mn, a.cta_me, a.hayiva
   from dbo.sce_mta1 a, dbo.sce_mta5 b
   where 	a.codsis = @codsis and
   a.codpro = @codpro and
   a.codeta = b.codcon and
   b.tipcon = ''E'' 
	--Akzio migracion Agosto 2014
	order by a.cta_mn , a.cta_me
	--Fin	

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta2_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta2_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta2_s03_MS]
	@codsis CHAR(3),
	@codpro CHAR(3),
	@codeta CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   
       codsis,
       codpro,
       codeta,
       mtomin,
       mtomax,
       codmon,
       hayiva,
       cta_mn,
       cta_me
   from dbo.sce_mta2
   where @codsis = codsis and
   @codpro = codpro and
   @codeta = codeta

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta3_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta3_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta3_s01_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   SELECT 
	   [codimp]
      ,[nomimp]
      ,[mtofij]
      ,[tasmin]
      ,[tasmax]
      ,[mtomin]
      ,[mtomax]
      ,[cta_mn]
      ,[cta_me]
  FROM [dbo].[sce_mta3]
  
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mta3_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mta3_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mta3_s03_MS] 
	@codimp  CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON
   
   SELECT   
		codimp,
        nomimp,
        mtofij,
        tasmin,
        tasmax,
        mtomin,
        mtomax,
        cta_mn,
        cta_me
   from dbo.sce_mta3
   where codimp = @codimp

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mts_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mts_d01_MS]
	@codcct  	CHAR(3),  
 	@codpro  	CHAR(2),  
 	@codesp  	CHAR(2),  
 	@codofi  	CHAR(3),  
 	@codope  	CHAR(5), 
	@numneg         NUMERIC(3,0),
	@tippro         NUMERIC(2,0),
	@numcpa         NUMERIC(6,0),
	@numcuo         NUMERIC(3,0),
	@numcob         NUMERIC(2,0), 
	@id_mensaje	NUMERIC(10,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   delete from dbo.sce_mts
   where
   @codcct = codcct	and
   @codpro = codpro	and
   @codesp = codesp	and
   @codofi = codofi	and
   @codope = codope	and
   @id_mensaje = id_mensaje
   Return   
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mts_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mts_s01_MS]
	@codcct  	CHAR(3),  
 	@codpro  	CHAR(2),  
 	@codesp  	CHAR(2),  
 	@codofi  	CHAR(3),  
 	@codope  	CHAR(5), 
	@numneg 	NUMERIC(3,0),
	@tippro		NUMERIC(2,0),
	@numcpa		NUMERIC(6,0),
	@numcuo		NUMERIC(3,0),
	@numcob		NUMERIC(2,0),
	@estado		NUMERIC(2,0),
	@tipgra		CHAR(1) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   if @codpro = ''17'' and @numcob = -1
      select   fecmsg,
               id_mensaje,
               nrorpt
      from dbo.sce_mts where
      @codcct = codcct and
      @codpro = codpro and
      @codesp = codesp and
      @codofi = codofi and
      @codope = codope and
      @numneg = numneg and
      @tippro = tippro and
      @numcpa = numcpa and
      @numcuo = numcuo and
      @estado = estado and
      @tipgra = tipgra
else
   select   fecmsg,
        	id_mensaje,
		nrorpt
   from dbo.sce_mts where
   @codcct = codcct and
   @codpro = codpro and
   @codesp = codesp and
   @codofi = codofi and
   @codope = codope and
   @numneg = numneg and
   @tippro = tippro and
   @numcpa = numcpa and
   @numcuo = numcuo and
   @numcob = numcob and
   @estado = estado and
   @tipgra = tipgra
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_mts_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mts_u01_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@numneg         NUMERIC(3,0),
	@tippro         NUMERIC(2,0),
	@numcpa         NUMERIC(6,0),
	@numcuo         NUMERIC(3,0),
	@numcob         NUMERIC(2,0),
	@id_mensaje	NUMERIC(10,0),
	@estado		INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 



   update dbo.sce_mts set estado = @estado 
   where	codcct = @codcct	and
   codpro = @codpro	and
   codesp = @codesp	and
   codofi = @codofi	and
   codope = @codope	and
   id_mensaje = @id_mensaje


   if (@@error <> 0 or @@rowcount = 0)
   begin
      return 9
   end
else
   begin
      return 0
   end


end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_netd_ejc_clt_w01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_netd_ejc_clt_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_netd_ejc_clt_w01_MS]
	@rutcli		VARCHAR(10),
	@codejc		NUMERIC(3,0),
    @oficina    NUMERIC(3,0) 
AS
BEGIN
/*	
		Historial:
														Migración desde Sybase (AKZIO)
		2015-10-16		marco.orellana@xemantics.com	Revisión de código proyecto migración Comex.Net (Microsoft)
*/

   DECLARE @desde NUMERIC(2,0),@hasta NUMERIC(2,0),@codcct VARCHAR(3),@rutejc VARCHAR(10),
   @nomejc VARCHAR(30),@direjc VARCHAR(30),@telejc VARCHAR(10),@faxejc VARCHAR(10)
		
   CREATE TABLE #tmp_codcct
   (
      id      NUMERIC(2,0) identity,
      codcct  VARCHAR(3)
   )

   INSERT INTO #tmp_codcct
   SELECT DISTINCT codcct FROM dbo.sce_mchh

   SELECT   @rutejc = ejc_ejcrut
   FROM	dbo.sgt_ejc
   WHERE	ejc_ejcofi = @oficina
   and		ejc_ejccod = @codejc

   SELECT DISTINCT  @nomejc = nombre,
        @direjc = direccion,
        @telejc = telefono,
        @faxejc = fax
   FROM    dbo.sce_usr
   WHERE   rut     = @rutejc
   and     tipeje  = ''N''

   IF @@ROWCOUNT = 0
   BEGIN
      DROP TABLE #tmp_codcct
      SELECT   ''01''
      RETURN
   END
 
   SELECT   @desde = 1
   SELECT   @hasta = count(1) FROM #tmp_codcct

   DELETE FROM dbo.sce_netd_ejc WHERE rutcli = @rutcli

   WHILE (@desde <= @hasta)
   BEGIN
      SELECT   @codcct = codcct FROM #tmp_codcct WHERE id = @desde
      
	  INSERT INTO dbo.sce_netd_ejc VALUES(@rutcli, @codcct, @nomejc, @direjc, @telejc, @faxejc)
    
      SET @desde = @desde + 1
   END

   DROP TABLE #tmp_codcct

   SELECT   ''00''
   RETURN

END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_nom_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_nom_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_nom_s01_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
	
   select 
		nom_pai,
		nom_mda,
		nom_swf,
		nom_bco,
		nom_cta,
		nom_act,
		nom_ala,
		nom_emi
   from dbo.sce_nom

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_obc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_obc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_obc_s01_MS]
	@sucbch        NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   
   select   codobc from dbo.sce_obc 
   where sucbch = @sucbch 

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pcol_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pcol_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pcol_s01_MS]
	@codcct         CHAR(3),       
    @codpro         CHAR(2),       
    @codesp         CHAR(2),       
    @codofi         CHAR(3),       
    @codope         CHAR(5)                                        
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   
		id_party  ,
		nombre    ,
		direccion
   from dbo.sce_pcol
   where 	@codcct = cent_costo	and
   @codpro = id_product	and
   @codesp = id_especia	and
   @codofi	= id_empresa	and
   @codope = id_cobranz    and
   posicion = 1	



   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s03_MS] 
	@fechaventa	SMALLDATETIME	,
	@cencos		CHAR(3)	,
	@codusr		CHAR(2)        ,
	@tipo		NUMERIC(1,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   if @tipo = 1
   begin
      select   cent_costo,
		id_product,
		id_especia,
		id_empresa,
		id_cobranz,
		num_presen,
		codmone   ,
		fechaventa,
		estado    ,
		mtototal  ,
		cencos    ,
        codusr
      from dbo.sce_plan
      where 	cencos     =  @cencos
      and   codusr     =  @codusr
      and   fechaventa =  @fechaventa
      and	estado     <> 9
      and   estado     <> 8
   end
   if @tipo = 2
   begin
      select   cent_costo,
         	id_product,
        	id_especia,
        	id_empresa,
        	id_cobranz,
        	num_presen,
        	codmone   ,
        	fechaventa,
        	estado    ,
        	mtototal  ,
        	cencos    ,
        	codusr
      from dbo.sce_plan
      where   cencos     = @cencos
      and  	codusr     = @codusr
      and   fechaventa = @fechaventa
      and  	estado = 8
   end

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s04_MS] 
	@cent_costo	CHAR(3),
	@id_product	CHAR(2),
	@id_especia	CHAR(2),
	@id_empresa	CHAR(3),
	@id_cobranz	CHAR(5),
	@num_presen	NUMERIC(10,0),
	@fecplan	datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON


   select   cent_costo,id_product,id_especia,
			id_empresa,id_cobranz,num_presen,
			rut,party,nomimport,fechaventa,num_idi,
			fec_idi,num_dec,fec_dec,num_con,
			fec_con,codigo,codbcch,cod_plaza,
			nombplaza,forma_pag,codpais,nompais,
			codmone,nommone,paridad,tipo_camb,
			mercaderia,hasta_fob,mtofob,mtoflete,
			mtoseguro,mtocif,mtointer,mtogastos,
			mtototal,cifdolar,totaldolar,fechavenc
   from dbo.sce_plan
   where 	cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   num_presen = @num_presen and
   fechaventa = convert(SMALLDATETIME,@fecplan)
   return

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s05_MS] 
	@cent_costo	CHAR(3),
	@id_product	CHAR(2),
	@id_especia	CHAR(2),
	@id_empresa	CHAR(3),
	@id_cobranz	CHAR(5),
	@num_presen	NUMERIC(10,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

   select   haycuadro,
			numcuadro,
			numcuota,
			hayacuerdo,
			numacuerdo,
			acuerdo1,
			acuerdo2,
			acuerdo3,
			hayanula,
			indanula,
			numreg,
			vencanula,
			fechaanula,
			paranula,
			totalanula,
			fecdebito,
			ndoc1,
			ndoc2,
			estado,
			observ,
			obsdecl,
			obsparidad,
			obscobranz,
			obsmerma,
			numpln_r, --datos de reemplazo                    
			fecpln_r,
			codpln_r,
			codent_r,
			numinf_r,
			fecinf_r,
			plzinf_r,
			numcon_r,
			feccon_r,
			hayrpl
   from dbo.sce_plan
   where 	cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   num_presen = @num_presen
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s07_MS] 
	@cencos	CHAR(3)	,
	@codusr	CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	select   
		cent_costo,
		id_product,
		id_especia,
		id_empresa,
		id_cobranz,
		num_presen,
		codmone   ,
		fechaventa,
		estado    ,
		mtototal  ,
		cencos    ,
		codusr
	from
		dbo.sce_plan
	where
		cencos     = @cencos     and
		codusr     = @codusr     and
		estado     = 7            
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s16_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s16_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s16_MS]
	@cent_costo	CHAR(3),
	@id_product	CHAR(2),
	@id_especia	CHAR(2),
	@id_empresa	CHAR(3),
	@id_cobranz	CHAR(5),
	@num_presen	NUMERIC(10,0),
	@fecplan	datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare
   @haycuadro  BIT,@numcuadro  NUMERIC(6,0),@numcuota   NUMERIC(2,0),@hayacuerdo BIT,
   @numacuerdo NUMERIC(1,0),@acuerdo1   CHAR(5),@acuerdo2   CHAR(5),
   @acuerdo3   CHAR(5),@hayanula   BIT,@indanula   NUMERIC(1,0),@numreg     NUMERIC(2,0),
   @vencanula  SMALLDATETIME,@fechaanula SMALLDATETIME,
   @paranula   NUMERIC(17,10),@totalanula NUMERIC(15,2),@fecdebito  SMALLDATETIME,
   @ndoc1      VARCHAR(15),@ndoc2      VARCHAR(15),@estado     NUMERIC(1,0),
   @observ     VARCHAR(200),@obsdecl    VARCHAR(200),@obsparidad VARCHAR(200),
   @obscobranz VARCHAR(60),@obsmerma   VARCHAR(40),
   @numpln_r   NUMERIC(10,0),@fecpln_r   SMALLDATETIME,@codpln_r   NUMERIC(10,0),
   @codent_r   NUMERIC(2,0),@numinf_r   CHAR(7),@fecinf_r   SMALLDATETIME,
   @plzinf_r   NUMERIC(10,0),@numcon_r   CHAR(8),@feccon_r   SMALLDATETIME,
   @hayrpl     NUMERIC(1,0),@zonfra	    BIT,@numpli	    CHAR(7)
   begin
      select   @haycuadro = haycuadro,
			@numcuadro = numcuadro,
			@numcuota = numcuota,
			@hayacuerdo = hayacuerdo,
			@numacuerdo = numacuerdo,
			@acuerdo1 = acuerdo1,
			@acuerdo2 = acuerdo2,
			@acuerdo3 = acuerdo3,
			@hayanula = hayanula,
			@indanula = indanula,
			@numreg = numreg,
			@vencanula = vencanula,
			@fechaanula = fechaanula,
			@paranula = paranula,
			@totalanula = totalanula,
			@fecdebito = fecdebito,
			@ndoc1 = ndoc1,
			@ndoc2 = ndoc2,
			@estado = estado,
			@observ = observ,
			@obsdecl = obsdecl,
			@obsparidad = obsparidad,
			@obscobranz = obscobranz,
			@obsmerma = obsmerma,
			@numpln_r = numpln_r, --datos de reemplazo 
			@fecpln_r = fecpln_r,
			@codpln_r = codpln_r,
			@codent_r = codent_r,
			@numinf_r = numinf_r,
			@fecinf_r = fecinf_r,
			@plzinf_r = plzinf_r,
			@numcon_r = numcon_r,
			@feccon_r = feccon_r,
			@hayrpl = hayrpl
      from dbo.sce_plan
      where 	cent_costo = @cent_costo and
      id_product = @id_product and
      id_especia = @id_especia and
      id_empresa = @id_empresa and
      id_cobranz = @id_cobranz and
      num_presen = @num_presen and
      fechaventa = convert(SMALLDATETIME,@fecplan)

      select   @numpli = convert(CHAR(7),@num_presen)
      select   @numpli = right(''0000000''+rtrim(@numpli),7)
      select   @zonfra = zonfra
      from dbo.sce_plib
      where 	@numpli = numpli and
      @fecplan = fecpli

      select   @haycuadro AS haycuadro, 
		@numcuadro AS numcuadro, 
		@numcuota AS numcuota, 
		@hayacuerdo AS hayacuerdo, 
		@numacuerdo AS numacuerdo, 
		@acuerdo1 AS acuerdo1, 
		@acuerdo2 AS acuerdo2, 
		@acuerdo3 AS acuerdo3, 
		@hayanula AS hayanula, 
		@indanula AS indanula, 
		@numreg AS numreg, 
		@vencanula AS vencanula, 
		@fechaanula AS fechaanula, 
		@paranula AS paranula, 
		@totalanula AS totalanula, 
		@fecdebito AS fecdebito, 
		@ndoc1 AS ndoc1, 
		@ndoc2 AS ndoc2, 
		@estado AS estado, 
		@observ AS observ, 
		@obsdecl AS obsdecl, 
		@obsparidad AS obsparidad, 
		@obscobranz AS obscobranz, 
		@obsmerma AS obsmerma, 
		@numpln_r AS numpln_r, 
		@fecpln_r AS fecpln_r, 
		@codpln_r AS codpln_r, 
		@codent_r AS codent_r, 
		@numinf_r AS numinf_r, 
		@fecinf_r AS fecinf_r, 
		@plzinf_r AS plzinf_r, 
		@numcon_r AS numcon_r, 
		@feccon_r AS feccon_r, 
		@hayrpl AS hayrpl, 
		@zonfra AS zonfra
   end             

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s17_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s17_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s17_MS]
	@cent_costo	CHAR(3),
	@id_product	CHAR(2),
	@id_especia	CHAR(2),
	@id_empresa	CHAR(3),
	@id_cobranz	CHAR(5),
	@num_presen	NUMERIC(10,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   select   cent_costo,id_product,id_especia,
			id_empresa,id_cobranz,num_presen,
			rut,party,nomimport,convert(datetime,fechaventa),
			num_dec,convert(datetime,fec_dec),num_con,
			convert(datetime,fec_con),codigo,codbcch,cod_plaza,
			nombplaza,forma_pag,codpais,nompais,
			codmone,nommone,paridad,tipo_camb,
			mercaderia,hasta_fob,mtofob,mtoflete,
			mtoseguro,mtocif,mtointer,mtogastos,
			mtototal,cifdolar,totaldolar,convert(datetime,fechavenc)
   from dbo.sce_plan
   where 	cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   num_presen = @num_presen
   return

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_s18_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_s18_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_s18_MS]
	@cent_costo	CHAR(3)	,
	@id_product	CHAR(2)	,
	@id_especia	CHAR(2)	,
	@id_empresa	CHAR(3)	,
	@id_cobranz	CHAR(5)	,
	@fechaventa	SMALLDATETIME 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   cent_costo	,
			id_product	,
			id_especia	,
			id_empresa	,
			id_cobranz	,
			num_presen	,
			convert(datetime,fechaventa)	,
			num_dec		,
			convert(datetime,fec_dec)		,
			codmone   	,
			forma_pag	,
			mercaderia	,
			hasta_fob 	,
			mtofob    	,
			mtoflete  	,
			mtoseguro 	,
			mtocif		,
			mtointer	,
			mtogastos	,
			mtototal	,
			cifdolar	,
			totaldolar	,
			paridad		,
			tipo_camb 	,
			indanula
   from 	dbo.sce_plan
   where	cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   fechaventa = @fechaventa and
			(estado	   = 4	 or
   estado     = 5	 or
   estado     = 6)
   return 
				
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_u07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_u07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_u07_MS]
	@cent_costo	CHAR(3)	,
	@id_product	CHAR(2)	,
	@id_especia	CHAR(2)	,
	@id_empresa	CHAR(3)	,
	@id_cobranz	CHAR(5)	,
	@num_presen     NUMERIC(10,0)     ,
	@estado         NUMERIC(1,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   update dbo.sce_plan set estado     = @estado  where   cent_costo = @cent_costo and
   id_product = @id_product and
   id_especia = @id_especia and
   id_empresa = @id_empresa and
   id_cobranz = @id_cobranz and
   num_presen = @num_presen

	
   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   return 0                   



   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_u12_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_u12_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plan_u12_MS] @cent_costo	CHAR(3)	,
	@id_product	CHAR(2)	,
	@id_especia	CHAR(2)	,
	@id_empresa	CHAR(3)	,
	@id_cobranz	CHAR(5)	,
	@num_presen     NUMERIC(10,0)     ,
	@fechaantigua	SMALLDATETIME	,
	@fechaventa	SMALLDATETIME	,
	@num_con	CHAR(8)	,
	@fec_con	SMALLDATETIME	,
	@forma_pag	NUMERIC(2,0)	,
	@codpais	NUMERIC(3,0)	,
	@nompais	VARCHAR(25)	,
	@fechavenc	SMALLDATETIME	,
	@haycuadro	BIT		,
	@numcuadro	NUMERIC(4,0)	,
	@numcuota	NUMERIC(2,0)	,
	@hayacuerdo	BIT		,
	@numacuerdo	NUMERIC(2,0)	,
	@acuerdo1	CHAR(5)	,
	@acuerdo2	CHAR(5)	,
	@fecdebito	SMALLDATETIME	,
	@ndoc1		VARCHAR(15)	,
	@ndoc2		VARCHAR(15)	,
	@observ		VARCHAR(200)	,
	@zonfra		BIT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	begin try
		declare
			@numpli		CHAR(7)
		begin transaction
		
		if exists(SELECT TOP 1 1 from dbo.sce_plan where num_presen = @num_presen and fechaventa = @fechaantigua)
			update 
				dbo.sce_plan 
			set 
				fechaventa = @fechaventa,
				num_con    = @num_con,
				fec_con    = @fec_con,
				forma_pag  = @forma_pag,
				codpais    = @codpais,
				nompais    = @nompais,
				fechavenc  = @fechavenc,
				haycuadro  = @haycuadro,
				numcuadro  = @numcuadro,
				numcuota   = @numcuota,
				hayacuerdo = @hayacuerdo,
				numacuerdo = @numacuerdo,
				acuerdo1   = @acuerdo1,
				acuerdo2   = @acuerdo2,
				fecdebito  = @fecdebito,
				ndoc1      = @ndoc1,
				ndoc2      = @ndoc2,
				observ     = @observ  
			where   
				cent_costo = @cent_costo and
				id_product = @id_product and
				id_especia = @id_especia and
				id_empresa = @id_empresa and
				id_cobranz = @id_cobranz and
				num_presen = @num_presen and
				fechaventa = @fechaantigua

		select   @numpli = convert(CHAR(7),@num_presen)
		select   @numpli = right(''0000000''+rtrim(@numpli),7)

		if exists(SELECT TOP 1 1 from dbo.sce_plib where 	numpli = @numpli and fecpli = @fechaantigua)
		update 
			dbo.sce_plib 
		set
			fecpli = @fechaventa,
			zonfra = @zonfra
		where
			numpli = @numpli and
			fecpli = @fechaantigua

		commit tran
		select   0
	end try

	begin catch
		rollback 
		select   9
	end catch
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plan_u14_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plan_u14_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Renato Herrera
-- Create date: 2015-10-16
-- Description:	marca planilla como anulada (estado 9) [sce_plan]
-- =============================================
CREATE PROCEDURE [dbo].[sce_plan_u14_MS] 
	@numero_presentacion NUMERIC(10,0),
	@fecha_presentacion	SMALLDATETIME
AS
BEGIN
	UPDATE [dbo].[sce_plan]
	SET estado = 9
	WHERE num_presen = @numero_presentacion AND fechaventa = @fecha_presentacion    
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_pldc_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create PROCEDURE [dbo].[sce_pldc_i01_MS] @numpre  CHAR(10),     
	@fecpre  datetime,  
	@tipo	 CHAR(1),   
	@fecact  datetime,     
	@decimp  CHAR(18),     
	@decexp  CHAR(7),     
	@fecdec  datetime,     
	@codadn  NUMERIC(3,0),  
	@mtoapl  NUMERIC(15,2),
	@mtoint  NUMERIC(15,2),
	@mtousd  NUMERIC(15,2),
	@fecvto  datetime 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:23:23 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
/* Archivo       : PRCCpldl_i01.sql                                    	*/
/* Objetivo      : Inserta las Planillas de Importacion o Exportacion	*/
/*		   sin Declaracion con los datos actualizados.		*/
/* Version       : 1.0                                              	*/
/* Autor	 : Paola Contreras P.				    	*/
/************************************************************************/

   insert into dbo.sce_pldc
	values(@numpre	,	--numpre
		@fecpre	,	--fecpre
		@tipo	,	--tipo
		@fecact	,	--fecact
		@decimp	,	--decimp
		@decexp	,	--decexp
		@fecdec	,	--fecdec 
		@codadn	,	--codadn
		@mtoapl	,	--mtoapl
		@mtoint	,	--mtoint
		@mtousd ,	--mtousd
		@fecvto		--fecvto
	)

       	
   if (@@rowcount > 0 and @@error = 0)
      select   0
else
   select   9                   

   return
END
                                     


' 
END

/****** Object:  StoredProcedure [dbo].[sce_pldc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pldc_s01_MS] 
	@dedonde	CHAR(1),
	@fecha1		datetime,
	@fecha2		datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

/************************************************************************/
/* Archivo       : PRCCpldc_s01.sql                                    	*/
/* Objetivo      : Selecciona las Planillas emitidas sin Declaracion de */
/*		   Importacion o Exportacion para un rango de fechas.	        */ 
/* Version       : 1.0                                              	*/
/* Autor	 : Paola Contreras P.               				    	*/
/************************************************************************/

	--Para Importaciones.-
   if @dedonde = ''I''
   begin
      select   convert(CHAR(10),num_presen) AS NumeroPresentacion,
			    fechaventa AS  FechaPresentacion,
			    totaldolar AS TotalDolar,
			    datediff(dd,fechaventa,GetDate()) AS AntiguedadDias,
    			right(''0000000000''+rut,10) AS Rut,
	    		SUBSTRING(nomimport,1,40) AS RazonSocial
      from	dbo.sce_plan
      where 	estado  < 9
      and	num_dec = ''''
      and	fechaventa >= @fecha1
      and   fechaventa <= @fecha2
      UNION 
      select   convert(CHAR(10),num_presen) AS NumeroPresentacion,
			    fechaventa AS FechaPresentacion,
			    totaldolar AS TotalDolar,
			    datediff(dd,fechaventa,GetDate()) AS AntiguedadDias,
			    right(''0000000000''+rut,10) AS Rut,
			    SUBSTRING(nomimport,1,40) AS RazonSocial
      from	dbo.sce_plan
      where 	estado < 9
      and   num_dec = ''________________-_''
      and	fechaventa >= @fecha1
      and   fechaventa <= @fecha2
      order by fechaventa
   end
else if @dedonde = ''E''
   begin
      select   a.numpre AS NumeroPresentacion,
			a.fecpre AS FechaPresentacion,
			a.mtodol AS TotalDolar,
			datediff(dd,a.fecpre,GetDate()) AS AntiguedadDias,
			a.rutexp AS Rut,
			SUBSTRING(b.razon_soci,1,40) AS RazonSocial
      from dbo.sce_xplv a, dbo.sce_rsa b
      where a.estado < 9
      and a.numdec = ''''
      and a.fecpre >= @fecha1
      and a.fecpre <= @fecha2
      and a.tippln = 500
      and a.prtexp = b.id_party
      and a.indnom = b.id_nombre
      UNION
      select   a.numpre AS NumeroPresentacion,
			a.fecpre AS FechaPresentacion,
			a.mtodol AS TotalDolar,
			datediff(dd,a.fecpre,GetDate()) AS AntiguedadDias,
			a.rutexp  AS Rut,
			SUBSTRING(b.razon_soci,1,40) AS RazonSocial
      from  dbo.sce_xplv a, dbo.sce_rsa b
      where a.estado < 9
      and a.numdec = ''''
      and a.fecpre >= @fecha1
      and a.fecpre <= @fecha2
      and a.tippln = 511
      and a.prtexp = b.id_party
      and a.indnom = b.id_nombre
      order by a.fecpre
   end
   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pldc_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pldc_s02_MS]
	@tipo		CHAR(1),
	@fecha		datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

/***************************************************************************/
/* Archivo       : PRCCpldc_s02.sql                                        */
/* Objetivo      : Selecciona las Planillas sin declaracion de             */
/*                 Importacion o Exportacion para la fecha de actualizacion*/
/* Version       : 1.0                                                     */
/* Autor         : Paola Contreras P.                                      */
/***************************************************************************/
/* 25-09-2015 11:28:09 - Migración MS                                      */
/***************************************************************************/

   declare @fecha2 CHAR(10)
   select   @fecha2 = convert(CHAR(10),@fecha,103)
   select   @fecha  = convert(datetime,@fecha2,103)


   select   numpre,
		fecpre,
		correl,
		tipo  ,
		fecact,
		decimp,
		decexp,
		fecdec,
		codadn,
		mtoapl,
		mtoint,
		mtousd,
		fecvto
   from	dbo.sce_pldc
   where 	tipo = @tipo and
   fecact = @fecha

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pldc_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sce_pldc_s03_MS] @tipo		CHAR(1),
	@numpre		CHAR(10),
	@fecpre		datetime 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:18:18 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/****************************************************************************/
/* Archivo       : PRCCpldc_s03.sql                                    	    */
/* Objetivo      : Selecciona el monto de una planilla que se actualizo para*/
/*		           agregarla a la lista.    	                            */	
/* Version       : 1.0                                              	    */
/* Autor	     : Paola Contreras P.			    	                    */
/****************************************************************************/

   if @tipo = ''I''
      select   convert(CHAR(10),num_presen) AS numpre,
                fechaventa AS fecpre,
                totaldolar as mtodol,
                datediff(dd,fechaventa,GetDate()) as antiguedad,
                right(''0000000000''+rut,10) as rut,
                nomimport as nombre
      from	dbo.sce_plan
      where   num_presen =  convert(NUMERIC(10,0),@numpre)
      and   fechaventa >= dateadd(dd,0,@fecpre)
      and   fechaventa <  dateadd(dd,+1,@fecpre)
else if @tipo = ''E''
      select   a.numpre,
                        a.fecpre,
                        a.mtodol ,
                        datediff(dd,a.fecpre,GetDate()) as antiguedad,
                        a.rutexp as rut,
                        (select b.razon_soci
         from dbo.sce_rsa b
         where b.id_party  = a.prtexp
         and   b.id_nombre = a.indnom) as nombre
      from	dbo.sce_xplv a
      where  a.numpre =  convert(NUMERIC(10,0),@numpre)
      and  a.fecpre >= dateadd(dd,0,@fecpre)
      and  a.fecpre <  dateadd(dd,+1,@fecpre)
				   
				   	

   return
END





' 
END

/****** Object:  StoredProcedure [dbo].[sce_pldc_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pldc_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create PROCEDURE [dbo].[sce_pldc_u01_MS] @fecact		datetime 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:02:02 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
/* Archivo       : PRCCpldl_u01.sql                                    	*/
/* Objetivo      : Actualiza datos de las Planillas de Importacion o 	*/
/*		   Exportacion sin Declaracion en las tablas originales	*/
/* Version       : 1.0                                              	*/
/* Autor	 : Paola Contreras P.				    	*/
/************************************************************************/

   declare
   @numpre	 CHAR(10),@fecpre  datetime,@tipo	 CHAR(1),@decimp	 CHAR(18),@decexp  CHAR(7),
   @fecdec	 datetime,@codadn	 NUMERIC(3,0),@fecvto  datetime,
   @fecact2 CHAR(10)


   select   @fecact2 = convert(CHAR(10),@fecact,103)
   select   @fecact  = convert(datetime,@fecact2,103)
   
   declare cursor_pldc cursor for
   select 	numpre,
		fecpre,
		tipo,
		decimp,
		decexp,
		fecdec,
		codadn,
		fecvto
   from	dbo.sce_pldc
   where	fecact = @fecact
   for read only
   
   open 	cursor_pldc
   fetch	cursor_pldc into
   @numpre,@fecpre,@tipo,@decimp,@decexp,@fecdec,@codadn,@fecvto
	
   while @@FETCH_STATUS != -1
   begin                                      
                	-- Se verifica error en el cursor.-
      if (@@FETCH_STATUS = -2)
         return 9
      if @tipo = ''I''
         update dbo.sce_plan set num_dec = @decimp,fec_dec = convert(SMALLDATETIME,@fecdec)  where	num_presen = convert(NUMERIC(10,0),@numpre)  and
         fechaventa = convert(SMALLDATETIME,@fecpre)
      if @tipo = ''E''
         update dbo.sce_xplv set numdec = @decexp,fecdec = @fecdec,codadn = @codadn,fecven = @fecvto  where	numpre = convert(CHAR(7),@numpre) and
         fecpre = @fecpre
      fetch 	cursor_pldc into
      @numpre,@fecpre,@tipo,@decimp,@decexp,@fecdec,@codadn,@fecvto
   end

	        ----------------------------------------------------
        	--Cierra el cursor.-                                
   close cursor_pldc                                    
   deallocate cursor_pldc                        

   if (@@error <> 0)
      select   9
else
   select   0  

   return
END



' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pli_s04_MS]
	@numpli	CHAR(7),
	@fecpli	datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @retorno    CHAR(1)
   if exists(SELECT TOP 1 1 from dbo.sce_pli where
	   numpli = @numpli and
	   fecpli = @fecpli and
	   (anunum <> ''''    or   anufec <> ''''))
	   select   @retorno = ''0''
	else
	   select   @retorno = ''1'' 

   select   @retorno
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_pli_s05_MS]
	@cencos         CHAR(7),
	@codusr         CHAR(2),
	@fecing         datetime 
AS
begin
-- Procedimiento original: sce_pli_s05
   Select   numpli	,
		fecpli	,
		codcct+codpro+codesp+codofi+codope as operacion,
		estado	,
		codmnd	,
		mtoope	,
		mtonac  ,
		tippln
   from dbo.sce_pli
   where cencos = @cencos
   and codusr = @codusr
   and fecing >= dateadd(dd,0,@fecing)
   and fecing <  dateadd(dd,+1,@fecing)
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pli_s06_MS]
	@numpli      CHAR(7),
	@fecpli      datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
						 Se agrega alias a columnas
*/
    SET NOCOUNT ON

   declare
   @numpln   CHAR(7),@fecpln   datetime,@cencos   CHAR(3),@codusr   CHAR(2),
   @fecing   datetime,@fecact   datetime,@codcct   CHAR(3),@codpro   CHAR(2),
   @codesp   CHAR(2),@codofi   CHAR(3),@codope   CHAR(5),@codanu   CHAR(6),
   @estado   NUMERIC(2,0),@codoper  CHAR(6),@plzbcc   NUMERIC(2,0),
   @rutcli   CHAR(10),@prtcli   CHAR(12),@indnom   NUMERIC(2,0),@inddir   NUMERIC(2,0),
   @codoci   NUMERIC(3,0),@tippln   NUMERIC(2,0),@codcom   CHAR(6),
   @concep   CHAR(3),@anunum   CHAR(7),@anufec   datetime,@anupbc   NUMERIC(2,0),
   @apctip   CHAR(2),@apcnum   CHAR(7),@apcfec   datetime,
   @apcpbc   NUMERIC(2,0),@motivo   VARCHAR(25),@numacu   NUMERIC(1,0),
   @desacu   CHAR(25),@codpai   NUMERIC(3,0),@codmnd   NUMERIC(3,0),@codmndbc NUMERIC(3,0),
   @mtoope   NUMERIC(15,2),@mtopar   NUMERIC(17,10),@mtodol   NUMERIC(15,2),
   @tipcam   NUMERIC(11,4),@mtonac   NUMERIC(15,2),
   @dienum   CHAR(7),@diefec   datetime,@diepbc   NUMERIC(2,0),@numdec   CHAR(7),
   @fecdec   datetime,@codadn   NUMERIC(3,0),@fecdeb   datetime,
   @docnac   CHAR(15),@docext   CHAR(14),@bcoext   NUMERIC(4,0),@numcre   NUMERIC(7,0),
   @feccre   datetime,@mndcre   NUMERIC(3,0),@mtocre   NUMERIC(15,2),
   @codacu   CHAR(10),@regacu   CHAR(10),@rutacu   CHAR(10),@obspli   VARCHAR(255),
   @codeor   CHAR(1),@zonfra   BIT,@secben	  NUMERIC(2,0),
   @secfin   NUMERIC(2,0),@prcpar   NUMERIC(4,1)
   select   @numpln = numpli,
		@fecpln = fecpli,
		@cencos = cencos,
		@codusr = codusr,
		@fecing = fecing,
		@fecact = fecact,
		@codcct = codcct,
		@codpro = codpro,
		@codesp = codesp,
		@codofi = codofi,
		@codope = codope,
		@codanu = codanu,
		@estado = estado,
		@codoper = codoper,
		@plzbcc = plzbcc,
		@rutcli = rutcli,
		@prtcli = prtcli,
		@indnom = indnom,
		@inddir = inddir,
		@codoci = codoci,
		@tippln = tippln,
		@codcom = codcom,
		@concep = concep,
		@anunum = anunum,
		@anufec = anufec,
		@anupbc = anupbc,
		@apctip = apctip,
		@apcnum = apcnum,
		@apcfec = apcfec,
		@apcpbc = apcpbc,
		@motivo = motivo,
		@numacu = numacu,
		@desacu = desacu,
		@codpai = codpai,
		@codmnd = codmnd,
		@codmndbc = codmndbc,
		@mtoope = mtoope,
		@mtopar = mtopar,
		@mtodol = mtodol,
		@tipcam = tipcam,
		@mtonac = mtonac,
		@dienum = dienum,
		@diefec = diefec,
		@diepbc = diepbc,
		@numdec = numdec,
		@fecdec = fecdec,
		@codadn = codadn,
		@fecdeb = fecdeb,
		@docnac = docnac,
		@docext = docext,
		@bcoext = bcoext,
		@numcre = numcre,
		@feccre = feccre,
		@mndcre = mndcre,
		@mtocre = mtocre,
		@codacu = codacu,
		@regacu = regacu,
		@rutacu = rutacu,
		@obspli = obspli,
		@codeor = codeor
   from dbo.sce_pli
   where   @numpli = numpli and
   @fecpli = fecpli
   
   select   @zonfra = isnull(zonfra,0),
		@secben = isnull(secben,0),
		@secfin = isnull(secfin,0),
		@prcpar = isnull(prcpar,0)
   from dbo.sce_plib
   where   @numpli = numpli and
   @fecpli = fecpli
   select   
	@numpli as numpli,
	@fecpli as fecpli,
	@cencos as cencos,
	@codusr as codusr,
	@fecing as fecing,
	@fecact as fecact,
	@codcct as codcct,
	@codpro as codpro,
	@codesp as codesp,
	@codofi as codofi,
	@codope as codope,
	@codanu as codanu,
	@estado as estado,
	@codoper as codoper,
	@plzbcc as plzbcc,
	@rutcli as rutcli,
	@prtcli as prtcli,
	@indnom as indnom,
	@inddir as inddir,
	@codoci as codoci,
	@tippln as tippln,
	@codcom as codcom,
	@concep as concep,
	@anunum as anunum,
	@anufec as anufec,
	@anupbc as anupbc,
	@apctip as apctip,
	@apcnum as apcnum,
	@apcfec as apcfec,
	@apcpbc as apcpbc,
	@motivo as motivo,
	@numacu as numacu,
	@desacu as desacu,
	@codpai as codpai,
	@codmnd as codmnd,
	@codmndbc as codmndbc,
	@mtoope as mtoope,
	@mtopar as mtopar,
	@mtodol as mtodol,
	@tipcam as tipcam,
	@mtonac as mtonac,
	@dienum as dienum,
	@diefec as diefec,
	@diepbc as diepbc,
	@numdec as numdec,
	@fecdec as fecdec,
	@codadn as codadn,
	@fecdeb as fecdeb,
	@docnac as docnac,
	@docext as docext,
	@bcoext as bcoext,
	@numcre as numcre,
	@feccre as feccre,
	@mndcre as mndcre,
	@mtocre as mtocre,
	@codacu as codacu,
	@regacu as regacu,
	@rutacu as rutacu,
	@obspli as obspli,
	@codeor as codeor,
	@zonfra as zonfra,
	@secben as secben,
	@secfin as secfin,
	@prcpar as prcpar
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pli_s07_MS]
	@numpli      CHAR(7),
	@fecpli      datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare
   @numpln   CHAR(7),@fecpln   datetime,@plzbcc   NUMERIC(2,0),@cencos   CHAR(3),
   @codusr   CHAR(2),@codcct   CHAR(3),@codpro   CHAR(2),@codesp   CHAR(2),
   @codofi   CHAR(3),@codope   CHAR(5),@codoper  CHAR(6),@rutcli   CHAR(10),    
   @prtcli   CHAR(12),@indnom   NUMERIC(2,0),@inddir   NUMERIC(2,0),
   @codoci   NUMERIC(3,0),@tippln   NUMERIC(2,0),@codcom   CHAR(6),     
   @concep   CHAR(3),@numacu   NUMERIC(1,0),@desacu   CHAR(25),@codpai   NUMERIC(3,0),  
   @codmnd   NUMERIC(3,0),@codmndbc NUMERIC(3,0),@mtoope   NUMERIC(15,2), 
   @mtopar   NUMERIC(17,10),@mtodol   NUMERIC(15,2),@dienum   CHAR(7),      
   @diefec   datetime,@diepbc   NUMERIC(2,0),@numdec   CHAR(7),      
   @fecdec   datetime,@codadn   NUMERIC(3,0),@fecdeb   datetime,@docnac   CHAR(15),     
   @docext   CHAR(14),@bcoext   NUMERIC(4,0),@numcre   NUMERIC(7,0), 
   @feccre   datetime,@mndcre   NUMERIC(3,0),@mtocre   NUMERIC(15,2),
   @codacu   CHAR(10),@regacu   CHAR(10),@rutacu   CHAR(10),@zonfra   BIT,
   @secben   NUMERIC(2,0),@secfin   NUMERIC(2,0),@prcpar   NUMERIC(4,1)
   select   @numpln = numpli,
		@fecpln = fecpli,
		@plzbcc = plzbcc,
		@cencos = cencos,
		@codusr = codusr,
		@codcct = codcct,
		@codpro = codpro,
		@codesp = codesp,
		@codofi = codofi,
		@codope = codope,
		@codoper = codoper,
		@rutcli = rutcli,
		@prtcli = prtcli,
		@indnom = indnom,
		@inddir = inddir,
		@codoci = codoci,
		@tippln = tippln,
		@codcom = codcom,
		@concep = concep,
		@numacu = numacu,
		@desacu = desacu,
		@codpai = codpai,
		@codmnd = codmnd,
		@codmndbc = codmndbc,
		@mtoope = mtoope,
		@mtopar = mtopar,
		@mtodol = mtodol,
		@dienum = dienum,
		@diefec = diefec,
		@diepbc = diepbc,
		@numdec = numdec,
		@fecdec = fecdec,
		@codadn = codadn,
		@fecdeb = fecdeb,
		@docnac = docnac,
		@docext = docext,
		@bcoext = bcoext,
		@numcre = numcre,
		@feccre = feccre,
		@mndcre = mndcre,
		@mtocre = mtocre,
		@codacu = codacu,
		@regacu = regacu,
		@rutacu = rutacu
   from dbo.sce_pli
   where   @numpli = numpli and
   @fecpli = fecpli
   select   @zonfra = isnull(zonfra,0),
		@secben = isnull(secben,0),
		@secfin = isnull(secfin,0),
		@prcpar = isnull(prcpar,0)
   from dbo.sce_plib
   where   @numpli = numpli and
   @fecpli = fecpli
   select   @numpln,
		@fecpln,
		@plzbcc,
		@cencos,
		@codusr,
		@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@codoper,
		@rutcli,
		@prtcli,
		@indnom,
		@inddir,
		@codoci,
		@tippln,
		@codcom,
		@concep,
		@numacu,
		@desacu,
		@codpai,
		@codmnd,
		@codmndbc,
		@mtoope,
		@mtopar,
		@mtodol,
		@dienum,
		@diefec,
		@diepbc,
		@numdec,
		@fecdec,
		@codadn,
		@fecdeb,
		@docnac,
		@docext,
		@bcoext,
		@numcre,
		@feccre,
		@mndcre,
		@mtocre,
		@codacu,
		@regacu,
		@rutacu,
		@zonfra,
		@secben,
		@secfin,
		@prcpar
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pli_u02_MS]
	@codcct      CHAR(3)   ,
	@codpro      CHAR(2)   ,
	@codesp      CHAR(2)   ,
	@codofi      CHAR(3)   ,
	@codope      CHAR(5)   ,
	@codanu      CHAR(6)   ,
	@estado      NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


   update dbo.sce_pli set estado = @estado  where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   codanu = @codanu

      
	
   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   return 0
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_u04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_u04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Renato Herrera
-- Create date: 2015-10-16
-- Description:	marca planilla como anulada (estado 9) [sce_pli]
-- =============================================
CREATE PROCEDURE [dbo].[sce_pli_u04_MS] 
	@numero_presentacion CHAR(7),
	@fecha_presentacion	DATETIME
AS
BEGIN
	UPDATE [dbo].[sce_pli]
	SET estado = 9
	WHERE numpli = @numero_presentacion AND fecpli = @fecha_presentacion    
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_pli_w06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_w06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_pli_w06_MS] 
	@numpli         CHAR(7)       	,
	@fecant		datetime,
	@fecpli     datetime,
	@codpai     NUMERIC(3,0),
	@obspli     VARCHAR(255),
	@zonfra		BIT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON


   begin
   BEGIN TRAN
      update dbo.sce_pli set codpai = @codpai,obspli = @obspli,fecpli = @fecpli,fecing = @fecpli  where
      numpli = @numpli and
      fecpli = @fecant
      if (@@error = 0)
         if not exists(SELECT TOP 1 1 from dbo.sce_plib where
         numpli = @numpli and
         fecpli = @fecant)
            insert into dbo.sce_plib values(@numpli,
					@fecpli,
        				@zonfra,
					0,
					0,
					0)
			
      else
         select   @numpli = right(''0000000''+Rtrim(@numpli),7)
         update dbo.sce_plib set fecpli = @fecpli,zonfra = @zonfra  where 	numpli = @numpli and
         fecpli = @fecant
      if (@@error <> 0)
      begin
         rollback 
         select   9 as resultado
      end
   else
      begin
         commit tran
         select   0 as resultado
      end
   end

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plia_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plia_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_plia_s01_MS] @numpli         CHAR(7),
	@fecpli         datetime 
AS
begin
-- This procedure was converted on Wed Apr 16 16:08:08 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   Select   numpli	,
		fecpli	,
		datimp	,
		fecins  ,
		nomfin  ,
		fecven  ,
		codpai
   from dbo.sce_plia where
   numpli = @numpli  and
   fecpli = @fecpli
   Return
   Return
end







' 
END

/****** Object:  StoredProcedure [dbo].[sce_plrm_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plrm_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plrm_s02_MS]
	@codcct         CHAR(3)	,                                     
    @codpro         CHAR(2)	,                                   
	@codesp		CHAR(2)	,
	@codofi		CHAR(3)	,
	@codope		CHAR(5)	,
	@fecha		SMALLDATETIME 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare
	-- Variables para ir sobre las tablas 
   @inddir		NUMERIC(2,0),@indnom		NUMERIC(2,0),@idprty		CHAR(12)		


	-- Vamos sobre las tablas segun el producto
	-- =======================================
   if @codpro = ''20'' or @codpro = ''30''
      select   @inddir  = inddirc,
				@indnom  = indnomc,
				@idprty  = prtcli
      from	dbo.sce_cvd
      where 	codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
else if (@codpro = ''07'' or @codpro = ''08'')
      select   @inddir = inddir,
				@indnom = indnom,
				@idprty = codprt
      from	dbo.sce_jprt
      where 	codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 0
else if @codpro = ''03''
      select   @inddir  = direccion,
				@indnom  = nombre,
				@idprty  = id_party
      from	dbo.sce_pcol
      where 	cent_costo = @codcct and
      id_product = @codpro and
      id_especia = @codesp and
      id_empresa = @codofi and
      id_cobranz = @codope and
      posicion   = 1	

	
		-- Retornamos los valores.-
		-- =========================	
   select   @inddir ,
        		@indnom ,
        		@idprty 	
		
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_plrm_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_plrm_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_plrm_s03_MS] 
	@numdec		CHAR(18)	,
	@fecdec		datetime	,
	@codpag		NUMERIC(2,0)	,
	@party 	        CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   declare
	-- Variables para ir sobre la tabla sce_dec
   @orifob		NUMERIC(15,2),@relfob		NUMERIC(15,2),@cubfob		NUMERIC(15,2),@disfob		NUMERIC(15,2),
   @orifle		NUMERIC(15,2),@relfle		NUMERIC(15,2),@cubfle		NUMERIC(15,2),
   @disfle		NUMERIC(15,2),@oriseg		NUMERIC(15,2),@relseg		NUMERIC(15,2),
   @cubseg		NUMERIC(15,2),@disseg		NUMERIC(15,2),@oricif		NUMERIC(15,2),
   @relcif		NUMERIC(15,2),@cubcif		NUMERIC(15,2),@discif		NUMERIC(15,2),
   @numapr		NUMERIC(7,0),@fecapr		datetime,@fecemb		datetime,
   @flag		NUMERIC(1,0),@fpagdec	NUMERIC(2,0),@codpai		NUMERIC(3,0),
   @numidi		NUMERIC(7,0),@fecidi		datetime

	-- Asignamos valores a las siguientes variables
	-- ============================================
   select   @orifob	= 0
   select   @relfob	= 0
   select   @cubfob	= 0
   select   @disfob	= 0
   select   @orifle	= 0
   select   @relfle	= 0
   select   @cubfle	= 0
   select   @disfle	= 0 
   select   @oriseg	= 0
   select   @relseg	= 0
   select   @cubseg	= 0
   select   @disseg	= 0
   select   @oricif	= 0
   select   @relcif	= 0
   select   @cubcif	= 0
   select   @discif	= 0 
   select   @flag = 0	
   select   @fpagdec = 0
   select   @codpai = 0


   select   @fpagdec = forpag,
			@numidi  = numapr,
			@fecidi  = fecapr
   from 	dbo.sce_dec
   where   numdec = @numdec  and
   fecdec = @fecdec           			



   if @codpag = @fpagdec
   begin
      select   @orifob	= orifob,
				@relfob	= relfob,
				@cubfob	= cubfob,
				@disfob	= disfob,
				@orifle	= orifle,
				@relfle	= relfle,
				@cubfle	= cubfle,
				@disfle	= disfle,
				@oriseg	= oriseg,
				@relseg	= relseg,
				@cubseg	= cubseg,
				@disseg	= disseg,
				@oricif	= oricif,
				@relcif	= relcif,
				@cubcif	= cubcif,
				@discif	= discif,
				@fecemb = fecemb,
				@codpai = codpai
      from 	dbo.sce_dec
      where 	numdec = @numdec	and
      fecdec = @fecdec
      if (@codpai = 0 or @codpai IS NULL)
      begin
         select   @codpai = paiadq
         from 	dbo.sce_idi
         where	@numidi = numapr and
         @fecidi = fecapr
      end
   end


	-- VEMOS SI LA DECLARACION EXISTE
	-- ==============================  
   if not exists(SELECT TOP 1 1 from    dbo.sce_dec
   where   numdec = @numdec and
   fecdec = @fecdec)
      select   @flag = 1 -- OJO.-	   	



	-- Retornamos los valores.-
	-- =========================	
   select   @flag	,
		@orifob	,
		@relfob	,
		@cubfob	,
		@disfob ,
		@orifle	,
		@relfle	,
		@cubfle	,
		@disfle	,
		@oriseg ,
		@relseg	,
		@cubseg	,
		@disseg	,
		@oricif	,
		@relcif	,
		@cubcif	,
		@discif	,
		@fecemb ,
		@fpagdec,
		@codpai
	
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_ppae_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ppae_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ppae_s02_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

     --Prestamos a exportadores.-	
   if @codpro = ''05''
   begin
      select   prtexp,
		codnom,
		coddir
      from dbo.sce_ppae where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 0
   end

     --Carta de Cr‚dito de Exportaciones.-
   if @codpro = ''12''
   begin
      select   prtexp,
		codnom,
		coddir
      from dbo.sce_ppae where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 0
   end                           
   if @codpro = ''13''
   begin
      if exists(SELECT TOP 1 1 from dbo.sce_ppae where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 2)
         select   prtexp,
			codnom,
			coddir
         from dbo.sce_ppae where
         codcct = @codcct and
         codpro = @codpro and
         codesp = @codesp and
         codofi = @codofi and
         codope = @codope and
         posprt = 2
   else
      select   prtexp,
			codnom,
			coddir
      from dbo.sce_ppae where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      posprt = 3
   end                             

   return
   return

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_prd_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prd_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_prd_i01_MS] @codcct CHAR(3),
 @codusr CHAR(2),
 @mes NUMERIC(2,0),
 @ano NUMERIC(4,0) 
AS
begin
-- This procedure was converted on Wed Apr 16 14:47:47 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).


/************************************************/


   declare @fecha1    CHAR(8),@fecha_ini datetime,@fecha_fin datetime
   create table #tmpprod
   (
      cod_cct  CHAR(3),
      cod_usr  CHAR(2),
      nombre_esp CHAR(30),
      num_cob  NUMERIC(3,0),
      num_ret  NUMERIC(3,0),
      num_pli  NUMERIC(3,0),
      num_plv  NUMERIC(3,0),
      num_dec  NUMERIC(3,0),
      num_gl  NUMERIC(3,0),
      num_cce     NUMERIC(3,0)
   )
    
-- AKZ001  select @fecha1 = right(''0000''+convert(CHAR(4),@ano),04)+right(''00''+convert(CHAR(2),@mes),02)+''01''
   select   @fecha1 = right(''0000''+convert(VARCHAR(4),@ano),04)+right(''00''+convert(VARCHAR(2),@mes),02)+''01''
    
   select   @fecha_ini = convert(datetime,@fecha1,112)
        
   select   @fecha_fin = dateadd(mm,1,@fecha_ini)
   select   @fecha_fin = dateadd(dd,-1,@fecha_fin)



   Insert #tmpprod
   select cent_costo,
    id_especia,
    nombre,
    0,
    0,
    0,
    0,
    0,
    0,
          0
   from dbo.sce_usr
   where cent_super = @codcct
   and id_super   = @codusr
 
   update #tmpprod set num_cob = (select count(*) from #tmpprod,  dbo.sce_xcob where  #tmpprod.cod_cct = dbo.sce_xcob.cencos
   and   #tmpprod.cod_usr = dbo.sce_xcob.codusr
   and   dbo.sce_xcob.fecing >= @fecha_ini
   and   dbo.sce_xcob.fecing <= @fecha_fin)
          
   update #tmpprod set num_ret = (select count(*) from #tmpprod, dbo.sce_xret where  #tmpprod.cod_cct = dbo.sce_xret.cencos
   and  #tmpprod.cod_usr = dbo.sce_xret.codusr
   and  dbo.sce_xret.fecing >= @fecha_ini
   and  dbo.sce_xret.fecing <= @fecha_fin)
      
      
   update #tmpprod set num_pli = (select count(*) from  #tmpprod, dbo.sce_pli where   #tmpprod.cod_cct = dbo.sce_pli.cencos
   and   #tmpprod.cod_usr = dbo.sce_pli.codusr
   and   dbo.sce_pli.fecing >= @fecha_ini
   and   dbo.sce_pli.fecing <= @fecha_fin)
      
   update #tmpprod set num_plv = (select count(*) from #tmpprod, dbo.sce_xplv where  #tmpprod.cod_cct = dbo.sce_xplv.cencos
   and  #tmpprod.cod_usr = dbo.sce_xplv.codusr
   and  dbo.sce_xplv.fecing >= @fecha_ini
   and  dbo.sce_xplv.fecing <= @fecha_fin)
      
   update #tmpprod set num_dec = (select count(*) from #tmpprod, dbo.sce_xdec where   #tmpprod.cod_cct  = dbo.sce_xdec.cencos
   and   #tmpprod.cod_usr  = dbo.sce_xdec.codusr
   and   dbo.sce_xdec.fecing  >= @fecha_ini
   and   dbo.sce_xdec.fecing  <= @fecha_fin)
      
   update #tmpprod set num_gl = (select count(*) from #tmpprod, dbo.sce_mch where   #tmpprod.cod_cct = dbo.sce_mch.cencos
   and   #tmpprod.cod_usr = dbo.sce_mch.codusr
   and   dbo.sce_mch.fecmov >= @fecha_ini
   and   dbo.sce_mch.fecmov <= @fecha_fin)
      
      
   update #tmpprod set num_cce = (select count(*) from #tmpprod, dbo.sce_ycce where   #tmpprod.cod_cct = dbo.sce_ycce.cencos
   and   #tmpprod.cod_usr = dbo.sce_ycce.codusr
   and   dbo.sce_ycce.fecing >= @fecha_ini
   and   dbo.sce_ycce.fecing <= @fecha_fin)
 
   select * from #tmpprod
   order by cod_cct,cod_usr 

   return
   return
end





' 
END

/****** Object:  StoredProcedure [dbo].[sce_prd_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prd_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_prd_s01_MS]
AS
BEGIN
	SELECT codpro, despro FROM dbo.sce_prd;
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_prty_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_prty_i01_MS] 
	@id_prb      CHAR(12),
    @id_prt      CHAR(12),
	@ccosto      CHAR(3),
	@cruser      CHAR(2),
	@ftoday      DATETIME 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

BEGIN TRAN

   INSERT INTO dbo.sce_prty (
		id_party, 
		borrado, 
		tipo_party, 
		flag, 
		clasificac, 
		tiene_rut, 
		rut,
		crea_costo, 
		crea_user, 
		mod_costo, 
		mod_user, 
		multiple, 
		cod_ofieje, 
		cod_eject,
		cod_acteco, 
		clase_ries, 
		cod_bco, 
		tasa_libor, 
		tasa_prime,
		spread, 
		swift, 
		plaza_alad,
		ejec_corre, 
		flagins, 
		insgen_imp, 
		insgen_exp,
		insgen_ser, 
		inscob_imp, 
		inscob_exp, 
		inscre_imp, 
		inscre_exp,
		fecing, fecact
	)
	VALUES(
		@id_prt, 
		0, 
		1, 
		160, 
		2, 
		0, 
		'''',
        @ccosto, 
		@cruser, 
		@ccosto, 
		@cruser, 
		0, 
		'''', 
		'''',
        '''',
		'''',
		0,
		0,
		0,
	    0.0, 
		@id_prb, 
		0,
		'''', 
		0, 
		0, 
		0,
	    0, 
		0, 
		0, 
		0, 
		0,
	    @ftoday, 
		@ftoday
	)

    
   IF @@ROWCOUNT = 0 or @@ERROR <> 0
   BEGIN
      ROLLBACK 
      SELECT   -1 as ResponseCode, ''Error al grabar datos en Sce_prty'' as ResponseMessage
      RETURN 9
   END

   -- Insertar Dirección
   INSERT INTO dbo.sce_dad(
		id_party, 
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
		crea_user)
	SELECT 
		@id_prt, 
		0, 
		0,
		ISNULL(RTRIM(bic_dir), ''-''),
		'''', 
		0,
		bic_pos, 
		'''',
		bic_ciu,
		pai_painom, 
		cou_cod,
		'''',
		'''',
		'''', 
		0, 
		0,
		'''',
		'''',
		@ccosto,
		@cruser
	FROM dbo.sce_bic, dbo.sce_cou, dbo.sgt_pai
	WHERE bic_swf + bic_sec = @id_prb
		AND cou_pai = SUBSTRING(bic_swf, 5, 2)
		AND pai_paicod = cou_cod

	IF @@ROWCOUNT = 0 or @@ERROR <> 0
	BEGIN
		ROLLBACK 
		SELECT   -1 as ResponseCode, ''Error al grabar datos en Sce_dad'' as ResponseMessage
		RETURN 9
	END

	-- Insertar Razón Social
	INSERT INTO dbo.sce_rsa(
		id_party, 
		id_nombre, 
		borrado,
		razon_soci,
		nom_fantas,
		contacto,
		sortkey,
		crea_costo, 
		crea_user)
	SELECT 
		@id_prt, 
		0, 
		0,
		bic_nom b1,
		'''',
		'''',
		bic_nom b2,
		@ccosto,
		@cruser
	FROM dbo.sce_bic
	WHERE bic_swf + bic_sec = @id_prb

	IF @@ROWCOUNT = 0 or @@ERROR <> 0
	BEGIN
		ROLLBACK 
		SELECT   -1 as ResponseCode, ''Error al grabar datos en Sce_rsa'' as ResponseMessage
		RETURN 9
	END

	COMMIT TRAN
	SELECT   0 as ResponseCode, ''Grabacion Exitosa'' as ResponseMessage
	RETURN 0
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_prty_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_prty_s07_MS] 
	@id_prty         CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   SELECT   
		id_party
   FROM   
		sce_prty
   WHERE
		id_party = @id_prty 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_prty_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE procedure [dbo].[sce_prty_s08_MS] 



	@id_prty         CHAR(12) 



AS



BEGIN



/*	



Historial:



                         Migración desde Sybase (AKZIO)



      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)



*/



    SET NOCOUNT ON



	



	SELECT [id_party]



      ,[borrado]



      ,[tipo_party]



      ,[flag]



      ,[clasificac]



      ,[tiene_rut]



      ,[rut]



      ,[crea_costo]



      ,[crea_user]



      ,[mod_costo]



      ,[mod_user]



      ,[multiple]



      ,[cod_ofieje]



      ,[cod_eject]



      ,[cod_acteco]



      ,[clase_ries]



      ,[cod_bco]



      ,[tasa_libor]



      ,[tasa_prime]



      ,[spread]



      ,[swift]



      ,[plaza_alad]



      ,[ejec_corre]



      ,[flagins]



      ,[insgen_imp]



      ,[insgen_exp]



      ,[insgen_ser]



      ,[inscob_imp]



      ,[inscob_exp]



      ,[inscre_imp]



      ,[inscre_exp]



      ,[fecing]



      ,[fecact]



   FROM 



		[sce_prty]



   WHERE



		id_party = @id_prty 



END





' 
END

/****** Object:  StoredProcedure [dbo].[sce_prty_s09_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_prty_s09_MS] 
	@id_party         CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   SELECT COUNT(1)
   FROM 
		[sce_prty]
   WHERE
		id_party = @id_party 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_prty_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_prty_u01_MS]
	@id_party   CHAR(12),
	@borrado    BIT	
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	UPDATE sce_prty 
	SET borrado = @borrado, 
		fecact  = GETDATE() 
	WHERE 
		id_party = @id_party
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_prty_w01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_prty_w01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_prty_w01_MS] @id_party       CHAR(12),
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
	@inscre_exp     NUMERIC(6,0) 
	, @par1 varchar(255) = NULL --AKZ001 no se utiliza 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 14:49:49 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
BEGIN TRAN
   if not exists(SELECT TOP 1 1 from dbo.sce_prty where id_party = @id_party)
   begin
      insert into dbo.sce_prty(id_party  ,
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
   if (@@rowcount = 0 or @@error <> 0)
   begin
      ROLLBACK 
      Select   -1 AS [Column1], ''Error al grabar datos en SCE_PRTY_W01.'' AS [Column2]
      Return
   end
   COMMIT TRAN
   Select   0 AS [Column1], ''Grabación Exitosa'' AS [Column2]
   return

   return
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_ras_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ras_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ras_u01_MS] @id_party CHAR(12)

,@id_nombre NUMERIC(2,0) 

AS

BEGIN

-- This procedure was converted on Wed Apr 16 16:01:01 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).





   update dbo.sce_rsa set borrado = 1  where id_party = @id_party



   update dbo.sce_rsa set borrado = 0  where id_party = @id_party

   and id_nombre = @id_nombre

        

   select   ''Actualizacion Exitosa''

END







' 
END

/****** Object:  StoredProcedure [dbo].[sce_ref_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_ref_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_ref_s01_MS] 
	@ref_bae CHAR(15) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

            
   select   codcct,
           codpro,
           codesp,
           codofi,
           codope,
           refnueva
   from  dbo.sce_ref
   where refnueva like RTRIM(@ref_bae)

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_refe_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_refe_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_refe_s01_MS] 
	@codcct      CHAR(3),
    @codpro      CHAR(2),
    @codesp      CHAR(2),
    @codofi      CHAR(3),
    @codope      CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   refnueva
   from dbo.sce_ref where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope

   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_rjte_i05_2]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rjte_i05_2]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create PROCEDURE [dbo].[sce_rjte_i05_2] @mesrea    NUMERIC(6,0) 

AS
BEGIN
BEGIN TRAN
-- This procedure was converted on Thu May 01 10:15:15 2014 using Ispirer SQLWays 6.0 Build 2286 32bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   declare    @meshoy        NUMERIC(2,0) -- Numero del Mes actual
   declare    @anohoy        NUMERIC(4,0) -- Numero del A¤o actual
   declare    @haypae       INT

--29/09/2008 - Real Systems Ltda.
--Cambio por IFRS Etapa IV
   declare @lc_estado   INT,@lc_num_ope  CHAR(15),@lc_es_penal INT,@cur_codcct  CHAR(3),
   @cur_codpro  CHAR(2),@cur_codesp  CHAR(2),@cur_codofi  CHAR(3),
   @cur_codope  CHAR(5),@cur_monccl  NUMERIC(3,0),@cur_salpla  DECIMAL(15,2),
   @cur_tcapla  DECIMAL(11,4),@cur_fecini  datetime,@cur_fecvto  datetime,
   @lc_codwsh   CHAR(3),@lc_ano_mes  NUMERIC(6,0)

   select   @lc_ano_mes = @mesrea
--Fin

   select   @meshoy = convert(NUMERIC(2,0),SUBSTRING(convert(CHAR(6),@mesrea),5,2))
   select   @anohoy = convert(NUMERIC(4,0),SUBSTRING(convert(CHAR(6),@mesrea),1,4))

   select   @haypae = count(*)
   from    dbo.sce_pae
   where    (codmon = 98)

   if @haypae = 0
      exec x_print ''Atención. No hay operaciones de PAES en UF a reajustar.''

--29/09/2008 - Real Systems Ltda.
--Cambios por IFRS IV
--Codigo Antiguo
/*
-- Prestamos a Exportadores en UF
-- ==============================
    insert into sce_rjte
   select  codcct, 
            codpro, 
            codesp, 
            codofi, 
            codope, 
          0, 
            0, 
            0, 
            codmon, 
            salpae, 
            tc_pae, 
            0, 
            ''001''
        from sce_pae
        where    estado in (1,2) and
                codmon = 98 and 
                escuad = 0


    insert into sce_rjte
   select  codcct, 
            codpro, 
            codesp, 
            codofi, 
            codope, 
          0, 
            0, 
            0, 
            codmon, 
            salpae, 
            tc_pae, 
            0, 
            ''002''
        from sce_pae
        where    estado in (1,2) and
                codmon = 98 and 
                escuad = 1

        
        if @@error <> 0
   return 10
*/
--Codigo Nuevo
   declare dev_rea cursor  LOCAL for
   select codcct, codpro, codesp, codofi, codope,
       codmon, salpae, tc_pae, fecini, fecvto
   from dbo.sce_pae
   where estado in(1,2)
   and   codmon = 98
   and   escuad = 0

   open dev_rea

   while 1 = 1
   begin
      select   @cur_codcct = null,
          @cur_codpro = null,
          @cur_codesp = null,
          @cur_codofi = null,
          @cur_codope = null,
          @cur_monccl = null,
          @cur_salpla = null,
          @cur_tcapla = null,
          @cur_fecini = null,
          @cur_fecvto = null
      fetch dev_rea into @cur_codcct,@cur_codpro,@cur_codesp,@cur_codofi,@cur_codope,@cur_monccl,
      @cur_salpla,@cur_tcapla,@cur_fecini,@cur_fecvto

     EXEC x_print ''''
	 EXEC x_print ''inicio''
	 EXEC x_print ''-----dev_rea-----''
EXEC x_print ''@cur_codcct = %1!'', @cur_codcct
EXEC x_print ''@cur_codpro = %1!'', @cur_codpro
EXEC x_print ''@cur_codesp = %1!'', @cur_codesp
EXEC x_print ''@cur_codofi = %1!'', @cur_codofi
EXEC x_print ''@cur_codope = %1!'', @cur_codope
EXEC x_print ''@cur_monccl = %1!'', @cur_monccl
EXEC x_print ''@cur_salpla = %1!'', @cur_salpla
EXEC x_print ''@cur_tcapla = %1!'', @cur_tcapla
EXEC x_print ''@cur_fecini = %1!'', @cur_fecini
EXEC x_print ''@cur_fecvto = %1!'', @cur_fecvto
EXEC x_print ''-----dev_rea-----''
EXEC x_print ''''
	 
	  if (@@FETCH_STATUS = -1)
         BREAK
      if (@@FETCH_STATUS = -2)
      begin
         rollback 
         return 6
      end
      select   @lc_num_ope = ''''
	  --select   @lc_estado = ''''
	  --select  @lc_estado = 2
      select   @lc_num_ope = @cur_codcct+@cur_codpro+@cur_codesp+@cur_codofi+@cur_codope
      select   @lc_estado = estado
      from dbo.sce_ods_informacion
      where num_ope = @lc_num_ope
      and   ano_mes = @lc_ano_mes
	  EXEC x_print ''''
	  EXEC x_print ''@lc_estado antes = %1!'', @lc_estado
	  EXEC x_print ''''
	  EXEC x_print ''@lc_num_ope antes = %1!'', @lc_num_ope
	  EXEC x_print ''''
	  EXEC x_print ''@lc_ano_mes antes = %1!'', @lc_ano_mes
	  EXEC x_print ''''
      if @lc_estado IS NULL or @lc_estado < 1
         select   @lc_estado = 1
      select   @lc_es_penal = 1
      if @cur_fecvto < GetDate()
         select   @lc_es_penal = 2
  if @lc_estado = 2
      begin
/*ACCENTURE - IR71051 - 11/08/2015 - I
         select   @lc_codwsh = ''101''
      end
   else if @lc_estado = 3
      begin
         select   @lc_codwsh = ''201''
      end
   else
      begin
         select   @lc_codwsh = ''001''
      end
ACCENTURE - IR71051 - 11/08/2015 - F*/
--ACCENTURE - IR71051 - 11/08/2015 - I
        if datediff(dd, @cur_fecini, @cur_fecvto) < 365
          begin
              select @lc_codwsh = ''101''
          end
        else
          begin
              select @lc_codwsh = ''111''
          end
        end
  else if @lc_estado = 3
      begin
        if datediff(dd,@cur_fecini,@cur_fecvto) < 365
          begin
              select @lc_codwsh = ''201''
          end
        else
          begin
			EXEC x_print ''@lc_estado en 211 = %1!'', @lc_estado
              select @lc_codwsh = ''211''
          end
        end
  else
      begin
        if datediff(dd,@cur_fecini,@cur_fecvto) < 365
          begin
              select @lc_codwsh = ''001''
          end
        else
          begin
              select @lc_codwsh = ''011''
          end
		
  end
--ACCENTURE - IR71051 - 11/08/2015 - F
 
EXEC x_print ''''
EXEC x_print ''-----inserta-----''
EXEC x_print ''@cur_codcct = %1!'', @cur_codcct
EXEC x_print ''@cur_codpro = %1!'', @cur_codpro
EXEC x_print ''@cur_codesp = %1!'', @cur_codesp
EXEC x_print ''@cur_codofi = %1!'', @cur_codofi
EXEC x_print ''@cur_codope = %1!'', @cur_codope
EXEC x_print ''@cur_monccl = %1!'', @cur_monccl
EXEC x_print ''@cur_salpla = %1!'', @cur_salpla
EXEC x_print ''@cur_tcapla = %1!'', @cur_tcapla
EXEC x_print ''@lc_codwsh = %1!'', @lc_codwsh
EXEC x_print ''@lc_estado = %1!'', @lc_estado
EXEC x_print ''@lc_es_penal = %1!'', @lc_es_penal
EXEC x_print ''@cur_fecini = %1!'', @cur_fecini
EXEC x_print ''@cur_fecvto = %1!'', @cur_fecvto
EXEC x_print ''-----inserta-----''      
EXEC x_print ''FIN''      

	  
	  insert dbo.sce_rjte
   values( @cur_codcct,
           @cur_codpro,
           @cur_codesp,
           @cur_codofi,
           @cur_codope,
           0,
           0,
           0,
           @cur_monccl,
           @cur_salpla,
           @cur_tcapla,
           0,
           @lc_codwsh,
           @lc_estado,
           @lc_es_penal,
           @cur_fecini,
           @cur_fecvto)

   --select @@rowcount

   
      if @@error <> 0
      begin
         rollback
		 return 8
	  end
   end
   close dev_rea
   deallocate dev_rea

   declare dev_rea cursor LOCAL for
   select codcct, codpro, codesp, codofi, codope,
       codmon, salpae, tc_pae, fecini, fecvto
   from dbo.sce_pae
   where estado in(1,2)
   and   codmon = 98
   and   escuad = 1
   
   open dev_rea

   while 1 = 1
   begin
      select   @cur_codcct = null,
          @cur_codpro = null,
          @cur_codesp = null,
          @cur_codofi = null,
          @cur_codope = null,
          @cur_monccl = null,
          @cur_salpla = null,
          @cur_tcapla = null,
          @cur_fecini = null,
          @cur_fecvto = null
      fetch dev_rea into @cur_codcct,@cur_codpro,@cur_codesp,@cur_codofi,@cur_codope,@cur_monccl,
      @cur_salpla,@cur_tcapla,@cur_fecini,@cur_fecvto
      if (@@FETCH_STATUS = -1)
         BREAK
      if (@@FETCH_STATUS = -2)
      begin
         rollback 
         return 6
      end
      select   @lc_num_ope = ''''
      select   @lc_num_ope = @cur_codcct+@cur_codpro+@cur_codesp+@cur_codofi+@cur_codope
      select   @lc_estado = estado
      from dbo.sce_ods_informacion
      where num_ope = @lc_num_ope
      and   ano_mes = @lc_ano_mes
      if @lc_estado IS NULL or @lc_estado < 1
         select   @lc_estado = 1
      select   @lc_es_penal = 1
      if @cur_fecvto < GetDate()
         select   @lc_es_penal = 2
      if @lc_estado = 2
      begin
/*ACCENTURE - IR71051 - 11/08/2015 - I
         select   @lc_codwsh = ''102''
      end
   else if @lc_estado = 3
      begin
         select   @lc_codwsh = ''202''
      end
   else
      begin
         select   @lc_codwsh = ''002''
      end
ACCENTURE - IR71051 - 11/08/2015 - F*/
--ACCENTURE - IR71051 - 11/08/2015 - I
        if datediff(dd, @cur_fecini, @cur_fecvto) < 365
          begin
              select @lc_codwsh = ''102''
          end
        else
          begin
              select @lc_codwsh = ''112''
          end
        end
  else if @lc_estado = 3
      begin
        if datediff(dd,@cur_fecini,@cur_fecvto) < 365
          begin
              select @lc_codwsh = ''202''
          end
        else
          begin
              select @lc_codwsh = ''212''
          end
        end
  else
      begin
        if datediff(dd,@cur_fecini,@cur_fecvto) < 365
          begin
              select @lc_codwsh = ''002''
          end
        else
          begin
              select @lc_codwsh = ''012''
          end
  end
--ACCENTURE - IR71051 - 11/08/2015 - F
      insert dbo.sce_rjte
   values( @cur_codcct,
           @cur_codpro,
           @cur_codesp,
           @cur_codofi,
           @cur_codope,
           0,
           0,
           0,
           @cur_monccl,
           @cur_salpla,
           @cur_tcapla,
           0,
           @lc_codwsh,
           @lc_estado,
           @lc_es_penal,
           @cur_fecini,
           @cur_fecvto)

   --select @@rowcount

   
      if @@error <> 0
      begin
         rollback
		 return 8
	  end
   end
   close dev_rea
   deallocate dev_rea
--Fin

   commit tran
   return 0
END






' 
END

/****** Object:  StoredProcedure [dbo].[sce_rng_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rng_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rng_i01_MS] 
	@codcct      CHAR(3),
    @codesp      CHAR(2),
    @codfun      CHAR(3),
    @rutesp      CHAR(10),
    @nummin      FLOAT,
    @nummax      FLOAT,
    @numact      FLOAT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

BEGIN TRAN
   if not exists(SELECT TOP 1 1 from dbo.sce_rng where
   codcct = @codcct and
   codesp = @codesp and
   codfun = @codfun)
   begin
      insert into dbo.sce_rng(codcct,
		codesp,
		codfun,
		rutesp,
 		nummin,
		nummax,
		numact)
		values(@codcct,
		@codesp,
		@codfun,
		@rutesp,
		@nummin,
		@nummax,
		@numact)
   end
else
   begin
      update dbo.sce_rng set codcct = @codcct,codesp = @codesp,codfun = @codfun,rutesp = @rutesp,nummin = @nummin,
      nummax = @nummax,numact = @numact  where
      codcct = @codcct and
      codesp = @codesp and
      codfun = @codfun
   end
   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al grabar datos en Sce_Rng''
      return
   end
   COMMIT TRAN
   Select   0,''Grabacion Exitosa''


end

' 
END


/****** Object:  StoredProcedure [dbo].[sce_rng_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
IF  EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rng_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
DROP PROCEDURE [dbo].[sce_rng_u01_MS]
END





SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rng_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rng_u01_MS] 
	@codcct          VARCHAR(3),
	@codesp          VARCHAR(2),
	@codfun          VARCHAR(3),
	@minNumRequerido numeric(10,0) = NULL
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

      declare @numact NUMERIC(10,0), @nummax	NUMERIC(10,0)

BEGIN TRAN



   if (@minNumRequerido is not null)
	   begin

	   SELECT @numact = numact 
		FROM sce_rng
		where
		   codcct = @codcct and
		   codesp = @codesp and
		   codfun = @codfun and
		   numact < nummax	 and
		   numact > 0

		   if(@numact < @minNumRequerido)
				select @numact = @minNumRequerido + 1 -- sumo 1 mas de lo requerido para dar margen para la concurrencia


			update dbo.sce_rng set numact = @numact + 1  where
			   codcct = @codcct and
			   codesp = @codesp and
			   codfun = @codfun and
			   numact < nummax	 and
			   numact > 0

	   end
	else
	begin

	 update dbo.sce_rng set numact = numact+1  where
	   codcct = @codcct and
	   codesp = @codesp and
	   codfun = @codfun and
	   numact < nummax	 and
	   numact > 0

	end
	
  

   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al grabar datos en Sce_Rng''
      return -1
   end

   select   @numact = numact,
		@nummax = nummax
   from 	dbo.sce_rng   WITH (holdlock)
   where 	codcct = @codcct and
   codesp = @codesp and
   codfun = @codfun


   if (@numact > @nummax and @numact > 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al grabar datos en Sce_Rng''
      return -1
   end
		

   COMMIT TRAN
   Select   @numact

   return @numact
END' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_i01_MS]
	@idparty	CHAR(12),
	@id_nombre	INT,
	@razon_soci VARCHAR(60),
	@nom_fantas VARCHAR(40),
	@contacto	VARCHAR(40),
	@sortkey	VARCHAR(60),
	@crea_costo	VARCHAR(3) = NULL,
	@crea_user	VARCHAR(2) = NULL
AS
	DECLARE @_RETVAL INT = 1;

	INSERT INTO dbo.sce_rsa (
		id_party,
		id_nombre,
		borrado,
		razon_soci,
		nom_fantas,
		contacto,
		sortkey,
		crea_costo,
		crea_user) 
	OUTPUT  inserted.*
	VALUES (
		@idparty, 
		@id_nombre,  
		0,
		@razon_soci, 
		@nom_fantas, 
		@contacto, 
		@sortkey,
		@crea_costo, 
		@crea_user);

RETURN @_RETVAL
' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_parti_listDir_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_parti_listDir_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_rsa_parti_listDir_MS] 
  @razon_soci varchar(60)
AS
BEGIN

SELECT sce_rsa.id_party, sce_rsa.razon_soci, sce_dad.direccion, sce_dad.ciudad, sce_dad.pais
FROM     sce_rsa INNER JOIN
         sce_dad ON sce_rsa.id_party = sce_dad.id_party
WHERE  
(upper(sce_rsa.razon_soci) LIKE ''%''+upper(@razon_soci)+''%'')
order by razon_soci

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_parti_listRazon_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_parti_listRazon_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_rsa_parti_listRazon_MS] 
  @razon_soci varchar(60)
AS
BEGIN
select id_party, razon_soci from sce_rsa 
where upper(razon_soci) like ''%''+upper(@razon_soci)+''%'' 
order by razon_soci
--order by id_party
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_rsa_s03_MS] @id_party       CHAR(12),
	@id_nombre      INT 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 16:01:01 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/
   
   select   razon_soci from dbo.sce_rsa where
   id_party  = @id_party and
   id_nombre = @id_nombre    
   return
   return
END




' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_s05_MS] 
	@id_party       CHAR(12),
	@id_nombre      INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	SELECT 
		id_party, 
		id_nombre, 
		razon_soci, 
		borrado 
	FROM dbo.sce_rsa 
	WHERE
		id_party  = @id_party
		AND	id_nombre = @id_nombre
		  
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_s06_MS] 
  @id_nombre	INT,
  @crea_costo	INT,
  @crea_user	INT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	SELECT 
		id_party, 
		razon_soci
	FROM [dbo].[sce_rsa]
	WHERE id_nombre = @id_nombre
		AND crea_costo = @crea_costo
		AND crea_user = @crea_user
	ORDER BY id_party

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_s07_MS] 
  @id_party		CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	SELECT 
		[id_party]
		,[id_nombre]
		,[borrado]
		,[razon_soci]
		,[nom_fantas]
		,[contacto]
		,[sortkey]
		,[crea_costo]
		,[crea_user]
	FROM 
		[sce_rsa]
	WHERE 
		 id_party = @id_party
	ORDER BY 
		id_nombre
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_s08_MS] 
	@id_party         CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
   SELECT COUNT(1)
   FROM 
		[sce_rsa]
   WHERE
		id_party = @id_party 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_u01_MS]
	@idparty	CHAR(12),
	@id_nombre	INT = 0,
	@borrado	INT = 0,
	@razon_soci VARCHAR(60),
	@nom_fantas VARCHAR(40),
	@contacto	VARCHAR(40),
	@sortkey	VARCHAR(60)
AS
	DECLARE @_RETVAL INT = 1;

	UPDATE dbo.sce_rsa
		SET id_nombre = @id_nombre,
			borrado = @borrado,
			razon_soci = @razon_soci,
			nom_fantas = @nom_fantas,
			contacto = @contacto,
			sortkey = @sortkey
		OUTPUT inserted.*
		WHERE id_party = @idparty
			AND id_nombre = @id_nombre

RETURN @_RETVAL
' 
END

/****** Object:  StoredProcedure [dbo].[sce_rsa_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rsa_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_rsa_u02_MS]
	@id_party       CHAR(12),
	@id_nombre		NUMERIC(2,0)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	UPDATE sce_rsa 
	SET borrado		= 1
	WHERE 
		id_party	= @id_party	AND
		id_nombre  <> @id_nombre
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_sec_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_sec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_sec_s01_MS]                                                     
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   
		codsec,
		nomsec
   from dbo.sce_sec

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_serv_imp_01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_serv_imp_01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_serv_imp_01_MS] 
	@rutcli CHAR(10),
	@codcct CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   set @rutcli = replace(@rutcli, ''|'', '''')
   set @rutcli = right(''0000000000'' + rtrim(@rutcli), 10)

   select   rutcli,
          codcct,
          nomejc,
          direjc,
          telejc,
          faxejc
   from dbo.sce_netd_ejc
   where rutcli = @rutcli
   and   codcct = @codcct

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_swf_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_swf_i01_MS] @codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@nrocor         NUMERIC(3,0),
	@cencos         CHAR(3),
	@codusr         CHAR(2),
	@tipswf         NUMERIC(3,0),
	@fecemi         datetime,
	@codanu         CHAR(6),
	@nromem         NUMERIC(8,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-09   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


BEGIN TRAN
   insert into dbo.sce_swf(codcct,
		codpro,
		codesp,
		codofi,
		codope,
		nrocor,
		cencos,
		codusr,
		tipswf,
		fecemi,
		codanu,
		estado,
		nromem)
	values(@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@nrocor,
		@cencos,
		@codusr,
		@tipswf,
		@fecemi,
		@codanu,
		9,
		@nromem)
	
   if (@@error <> 0)
   begin
      ROLLBACK 
      Select   -1, ''Error al grabar datos en Sce_Swf''
      Return
   end
   COMMIT TRAN
   Select   0,''Grabacion Exitosa''
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_swf_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_swf_s02_MS]
	@codcct		CHAR(3),
	@codpro		CHAR(2),
	@codesp		CHAR(2),
	@codofi		CHAR(3),
	@codope		CHAR(5),
	@nrocor      	NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   nromem 
   from dbo.sce_swf 
   where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   nrocor = @nrocor
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_swf_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_swf_s03_MS] 
	@fecemi         datetime,
	@cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-09   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   if convert(char(8),@fecemi,112) = convert(char(8),getdate(),112)
		select   
			sfw.codcct
			, sfw.codpro
			, sfw.codesp
			, sfw.codofi
			, sfw.codope
			, sfw.nrocor
			, sfw.tipswf
			, sfw.fecemi
			, sfw.nromem
			, mcd.nrorpt 
		from dbo.sce_swf sfw 
		inner join sce_mch mcd on sfw.codcct = mcd.codcct and sfw.codpro = mcd.codpro and sfw.codesp = mcd.codesp and sfw.codofi = mcd.codofi and sfw.codope = mcd.codope 
		where
		   sfw.fecemi =  @fecemi and
		   sfw.cencos =  @cencos and
		   sfw.codusr =  @codusr
	else
		select   
			sfw.codcct
			, sfw.codpro
			, sfw.codesp
			, sfw.codofi
			, sfw.codope
			, sfw.nrocor
			, sfw.tipswf
			, sfw.fecemi
			, sfw.nromem
			, mcd.nrorpt 
		from dbo.sce_swf sfw 
		inner join sce_mchh mcd on sfw.codcct = mcd.codcct and sfw.codpro = mcd.codpro and sfw.codesp = mcd.codesp and sfw.codofi = mcd.codofi and sfw.codope = mcd.codope 
		where
		   sfw.fecemi =  @fecemi and
		   sfw.cencos =  @cencos and
		   sfw.codusr =  @codusr
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_swf_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_swf_s04_MS] 
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-09   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   declare @fecemi datetime

   select @fecemi = fecemi
   from 	dbo.sce_swf
   where 	codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope
	
	if convert(char(8),@fecemi,112) = convert(char(8),getdate(),112)
	   select 
			sfw.codcct,
			sfw.codpro,
			sfw.codesp,
			sfw.codofi,
			sfw.codope,
			sfw.nrocor,
			sfw.tipswf,
			sfw.fecemi,
			sfw.nromem
			, mcd.nrorpt 
	   from 	dbo.sce_swf sfw 
		inner join sce_mch mcd on sfw.codcct = mcd.codcct and sfw.codpro = mcd.codpro and sfw.codesp = mcd.codesp and sfw.codofi = mcd.codofi and sfw.codope = mcd.codope 
	   where 	
	   sfw.codcct = @codcct and
	   sfw.codpro = @codpro and
	   sfw.codesp = @codesp and
	   sfw.codofi = @codofi and
	   sfw.codope = @codope
   else
		select  sfw.codcct,
				sfw.codpro,
				sfw.codesp,
				sfw.codofi,
				sfw.codope,
				sfw.nrocor,
				sfw.tipswf,
				sfw.fecemi,
				sfw.nromem
				, mcd.nrorpt 
		   from 	dbo.sce_swf sfw 
		   inner join sce_mchh mcd on sfw.codcct = mcd.codcct and sfw.codpro = mcd.codpro and sfw.codesp = mcd.codesp and sfw.codofi = mcd.codofi and sfw.codope = mcd.codope 
		   where 	
		   sfw.codcct = @codcct and
		   sfw.codpro = @codpro and
		   sfw.codesp = @codesp and
		   sfw.codofi = @codofi and
		   sfw.codope = @codope
   end



' 
END

/****** Object:  StoredProcedure [dbo].[sce_swf_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_swf_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_swf_u01_MS]
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@codanu      CHAR(6),
	@estado      NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

      --begin tran
   update dbo.sce_swf set estado = @estado  where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   codanu = @codanu

   if (@@error <> 0 or @@rowcount = 0)
   begin
  			--rollback tran
      return 9
   end
else
   begin
      declare @res1    NUMERIC(2,0)
      declare @var1    datetime
      declare @esppro  CHAR(6)
      declare @cencos  CHAR(3)
      declare @codusr  CHAR(3)
      declare @espown  CHAR(6)
      declare @opesin  CHAR(15)
      declare @fecemi  datetime
      declare @codofin NUMERIC(3,0)
      declare @tipswfn NUMERIC(3,0)
      declare @nromem  NUMERIC(8,0)
      declare cursor_swf cursor for
      select  cencos,
                   codusr,
		   fecemi,
		   tipswf,
		   nromem
      from dbo.sce_swf where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      codanu = @codanu
	     declare @tipswf  char(3)

      select   @var1   = GetDate()
      select   @fecemi = GetDate()

           -- Cursor que identifica los cheques.-
           
           -- Se abre el cursor y se especifica las variables.-
      open cursor_swf
      fetch cursor_swf into
      @cencos,@codusr,@fecemi,@tipswfn,@nromem

           -- Se recorre el cursor para devolver montos.-
      while @@FETCH_STATUS != -1
      begin
              -- Se verifica error en el cursor.-
         declare @esppron   NUMERIC(5,0)
         declare @espownn   NUMERIC(5,0)
         declare @codnegn   NUMERIC(3,0)
         declare @regimen   NUMERIC(1,0)
         declare @rutcli    CHAR(10)      -- rut cliente.
         declare @nomcli    CHAR(30)      -- nombre cliente.
         declare @indclin   NUMERIC(1,0)   -- indice nombre cliente.
         declare @indclid   NUMERIC(1,0)   -- indice dirección cliente.
         declare @rutben    CHAR(10)      -- rut beneficiario.
         declare @nomben    CHAR(30)      -- nombre beneficiario.
         declare @swfbco1   CHAR(11)      -- swift banco 1.
         declare @rutbco1   CHAR(10)      -- rut banco 1
         declare @nombco1   CHAR(30)      -- nombre banco 1.
         declare @indbco1n  NUMERIC(1,0)   -- ind. nombre party B1.
         declare @indbco1d  NUMERIC(1,0)   -- ind. direc party B1.
         declare @swfbco2   CHAR(11)      -- swift banco 2.
         declare @nombco2   CHAR(30)      -- nombre banco 2.
         declare @indbco2n  NUMERIC(1,0)   -- ind. nombre party B2.
         declare @indbco2d  NUMERIC(1,0)   -- ind. direc party B2.
         declare @swfbco3   CHAR(11)      -- otro banco.
         declare @swfbco4   CHAR(11)      -- otro banco.
         declare @esavis    NUMERIC(1,0)
         declare @esconf    NUMERIC(1,0)
         declare @indbenn   NUMERIC(1,0)
         declare @indbend   NUMERIC(1,0)
         if (@@FETCH_STATUS = -2)
         begin
                   --rollback tran
            return 9
         end
         select   @indclin = @indbenn
         select   @indclid = @indbend
         select   @rutben = ''''
         select   @nomben = ''''
         select   @indbenn = 0
         select   @indbend = 0
         select   @var1    = GetDate()
         select   @esppro  = @cencos+@codusr
         select   @espown  = @codcct+@codesp
         select   @codofin = convert(NUMERIC(3,0),@codofi)

              -- Se asignan los datos a grabar.-
         select   @esppro  = @cencos+@codusr
         select   @espown  = @codcct+@codesp
         select   @codofin = convert(NUMERIC(3,0),@codofi)
	                   -- codproe
                                         -- anulac
                                     -- codfun
                                     -- subproe
                                   -- codcct
                                   -- codpro
                                   -- codesp
                                   -- codofi
                                   -- codope
                                         -- codneg
                                         -- tippro
                                         -- numcor
                                         -- numcuo
                                         -- nrocan
                                   -- fecpro
                                         -- codplan
                                         -- cerend
                                         -- reldi
                                         -- aprobii
                                         -- acelet
                                        -- filler6
                                        -- filler7
                                        -- filler8
                                        -- filler9
                                        -- filler10
                                         -- mndfun1
                                         -- mtofun1
                                         -- mndfun2
                                         -- mtofun2
                                         -- mndfun3
                                         -- mtofun3
                                         -- mndfun4
                                         -- mtofun4
                                         -- moneda
                                         -- monto
                                         -- mndint
                                         -- mtoint
                                         -- mndcom
                                         -- mtocomre
                                         -- mtocomsi
                                         -- mtocomno
                                         -- mndimp
                                         -- mtoimp
                                         -- mndgas
                                         -- mtogas
                                   -- fecfun
                                     -- fecrec
                                     -- fecven
                                     -- fecnul
                                   -- esppro
                                   -- espown
                                         -- oficon
                                   -- rutcli
                                   -- nomcli
                                  -- indclin
                                  -- indclid
                                   -- rutben
                                   -- nomben
                                  -- indbenn
                                  -- indbend
                                   -- numdoc
                                     -- fecdoc
                                         -- paridad
                                         -- tcamtab
                                         -- tcamcan
                          -- data
                                  -- prtbco1
                                  -- rutbco1
                                  -- nomprt1
                                 -- indprtb1n
                                 -- indprtb1d
                                  -- prtbco2
                                  -- nomprt2
                                 -- indprtb2n
                                 -- indprtb2d
                            -- prtbco3
                                  -- prtbco4
                                  -- regimen
                                   -- esavis
                                   -- esconf
                                         -- esrstg
                                         -- parcial
                                         -- discrep
                              -- fecemi
                              -- fecemb
                                         -- tipomod
                                         -- totdoc
                                         -- tenor
                                         -- nrocuot
                                         -- limirr
                                         -- nuplco
                                         -- nuples
                                         -- finpag
                                        -- codanu
                                         -- gastxcta
                                         -- mongastx
                                         -- mtogastx
                                     -- fecest
         select   @tipswf  = convert(CHAR(3),@tipswfn)

              -- Se ingresan datos en el Log.-

         EXECUTE @res1 = dbo.sce_gtlg_i01 @codpro,0,''001'',''220'',@codcct,@codpro,@codesp,@codofi,@codope,0,0,0,0,
         0,@fecemi,0,0,0,0,0,'''','''','''','''','''',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
         0,0,@fecemi,@var1,@var1,@var1,@esppro,@espown,0,@rutcli,@nomcli,@indclin,
         @indclid,@rutben,@nomben,@indbenn,@indbend,@tipswf,@var1,0,0,0,''Creación Swift'',
         @swfbco1,@rutbco1,@nombco1,@indbco1n,@indbco1d,@swfbco2,
         @nombco2,@indbco2n,@indbco2d,@swfbco3,@swfbco4,@regimen,@esavis,@esconf,
         0,0,0,CONVERT(datetime,0),CONVERT(datetime,0),0,0,0,0,0,0,0,0,'''',0,0,0,@var1,@estado             -- estado

         if @res1 <> 0
         begin
                   --rollback tran
            return 9
         end

              -- Se accesa el proximo registro del cursor
         fetch cursor_swf into
         @cencos,@codusr,@fecemi,@tipswfn,@nromem
      end

           -- Se cierra el cursor.-
      close cursor_swf
      deallocate cursor_swf

           -- Se verifica la existencia de errores.-
      if (@@error <> 0)
      begin
                --rollback tran
         return 9
      end
   else
      begin
                --commit tran
         return 0
      end
   end

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_d01_MS] 
	@id_party	CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	DELETE FROM sce_tcom 
	WHERE id_party = @id_party
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_d02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_d02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_d02_MS] 
	@id_party	CHAR(12),
	@sistema    CHAR(3),
	@producto   CHAR(3),
	@etapa		CHAR(3),
	@secuencia	NUMERIC(2,0)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	DELETE FROM sce_tcom 
	WHERE 
		id_party = @id_party AND 
		sistema = @sistema AND 
		producto = @producto AND 
		etapa = @etapa AND 
		secuencia = @secuencia AND 
		fecha > GETDATE()
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_i01_MS] 
	@id_party	CHAR(12),
	@sistema	CHAR(3),
	@producto	CHAR(3),
	@etapa		CHAR(3),
	@secuencia	NUMERIC(2, 0),
	@borrado	BIT,
	@manual_t	BIT,
	@monto_fijo BIT,
	@tasa		NUMERIC(9, 6),
	@hasta_mon	NUMERIC(15, 2),
	@minimo		NUMERIC(15, 2),
	@maximo		NUMERIC(15, 2),
	@fecha		SMALLDATETIME
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	INSERT INTO sce_tcom (id_party,
							sistema, 
							producto, 
							etapa,
							secuencia,
							borrado,
							manual_t, 
							monto_fijo,
							tasa,
							hasta_mon,
							minimo, 
							maximo,
							fecha) 
					VALUES (@id_party,
							@sistema,
							@producto,
							@etapa,
							@secuencia,
							@borrado,
							@manual_t,
							@monto_fijo,
							@tasa,
							@hasta_mon,
							@minimo,
							@maximo,
							@fecha)
END

IF (@@ERROR <> 0)
	RETURN -1
ELSE
    RETURN 0

RETURN

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_s03_MS] 
	@keyprt         CHAR(12),
	@codsis         CHAR(3),
	@codpro         CHAR(3),
	@codeta         CHAR(3),
	@fecref         datetime,
	@mtocom         FLOAT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @mantar         BIT,@mtofij         BIT,@tasmax         FLOAT,@hasta          FLOAT,
   @min            FLOAT,@max            FLOAT,@fecha          datetime,
   @manual         BIT,@fijo           BIT,@tas            FLOAT,           
   @mtoini         FLOAT,@mini           FLOAT,@maxi           FLOAT,
   @fecini         datetime,@tasesp         BIT,@manual_t       BIT,@monto_fijo     BIT,
   @tasa           FLOAT,@minimo         FLOAT,@maximo         FLOAT

/*Declaracion del cursor para el select                                   */        
   declare cur_com cursor LOCAL FAST_FORWARD for 
	   select manual_t, monto_fijo, tasa, hasta_mon, minimo, maximo, fecha 
	   from dbo.sce_tcom where 
	   id_party = @keyprt and
	   sistema = @codsis and 
	   producto = @codpro and 
	   etapa = @codeta and
	   borrado = 0 and
	   hasta_mon >= @mtocom and
	   fecha <= @fecref
	   order by fecha desc
              

/*Se Abre el cursor                                                     */
   open cur_com

/*Se lee el primer registro que contiene la tasa a usar                 */
   fetch cur_com into @mantar,@mtofij,@tasmax,@hasta,@min,@max,@fecha

   if @@rowcount != 0
   begin
      select   @manual = @mantar,
	     @fijo = @mtofij,
	     @tas = @tasmax,
	     @mtoini = @hasta,
	     @mini = @min,
	     @maxi = @max,
	     @fecini = @fecha
      while @@FETCH_STATUS != -1
      begin
         if @@FETCH_STATUS != -2
         begin
            if @fecini = @fecha
            begin
               if @mtoini > @hasta
               begin
                  select   @manual = @mantar,
							@fijo = @mtofij,
							@tas = @tasmax,
							@mtoini = @hasta,
							@mini = @min,
							@maxi = @max
               end
            end
         else
            BREAK
            fetch cur_com into @mantar,@mtofij,@tasmax,@hasta,@min,@max,@fecha
         end
      end
      select   @tasesp = 1,
		@manual_t = @manual,
		@monto_fijo = @fijo,
		@tasa = @tas,
		@minimo = @mini,
		@maximo = @maxi
   end
else
   begin
      select   @tasesp = 0,
		@manual_t = 0,
		@monto_fijo = 0,
		@tasa = 0,
		@minimo = 0,
		@maximo = 0
   end


   CLOSE cur_com;
   DEALLOCATE cur_com;

   select   [tasaesp]=@tasesp
			,[manual]=@manual_t
			,[MtoFij] = @monto_fijo
			,[tasmax] = @tasa
			,[TasMin] = @minimo
			,[MtoMin] = @hasta
			,[MtoMax] = @maximo

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_s04]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_s04]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_s04] 
	@id_party   CHAR(12)                                                                           
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	SELECT 
	   [id_party]
      ,[sistema]
      ,[producto]
      ,[etapa]
      ,[secuencia]
      ,[borrado]
      ,[manual_t]
      ,[monto_fijo]
      ,[tasa]
      ,[hasta_mon]
      ,[minimo]
      ,[maximo]
      ,[fecha]
	FROM 
	   [sce_tcom]
    WHERE 
	   id_party = @id_party  
	ORDER BY 
	   secuencia
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_s04_MS] 
	@id_party   CHAR(12)                                                                           
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	SELECT 
	   [id_party]
      ,[sistema]
      ,[producto]
      ,[etapa]
      ,[secuencia]
      ,[borrado]
      ,[manual_t]
      ,[monto_fijo]
      ,[tasa]
      ,[hasta_mon]
      ,[minimo]
      ,[maximo]
      ,[fecha]
	FROM 
	   [sce_tcom]
    WHERE 
	   id_party = @id_party  
	ORDER BY 
	   secuencia
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_u01_MS]
	@id_party       CHAR(12),
	@borrado		BIT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	UPDATE sce_tcom 
	SET borrado = @borrado
	WHERE id_party = @id_party 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcom_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcom_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcom_u02_MS]
	@id_party       CHAR(12),
	@sistema		CHAR(3),
	@producto		CHAR(3),
	@etapa			CHAR(3),
	@secuencia		NUMERIC(2,0),
	@borrado		BIT,
	@manual_t		BIT,
	@monto_fijo		BIT,
	@tasa			NUMERIC(9,6),
	@hasta_mon		NUMERIC(15,2),
	@minimo			NUMERIC(15,2),
	@maximo			NUMERIC(15,2),
	@fecha          SMALLDATETIME
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	UPDATE sce_tcom 
	SET
		id_party    = @id_party,
		sistema		= @sistema,
		producto	= @producto,
		etapa		= @etapa,
		secuencia	= @secuencia,
		borrado		= @borrado,
		manual_t	= @manual_t,
		monto_fijo	= @monto_fijo,
		tasa		= @tasa,
		hasta_mon	= @hasta_mon,
		minimo		= @minimo,
		maximo		= @maximo,
		fecha       = @fecha
	WHERE 
		id_party	= @id_party	 AND 
		sistema		= @sistema	 AND 
		producto	= @producto  AND 
		etapa		= @etapa	 AND
		secuencia	= @secuencia AND
		fecha		> GETDATE()
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tcp_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tcp_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tcp_s01_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   codcom+cptcom as codtcp,
tipope        as codoci,
descom        as destcp,
0             as tcpimp,
0             as tcpexp,
0             as tcpcam,
0             as tcpzpr,
0             as tcpvis,
0             as tcpinv,
0             as tcpcom,
0             as tcparb,
decexp        as tcpdec,
codpai        as tcppai,
codccr        as tcpcon,
dataut        as tcpaut,
0             as tcpop1,
0             as tcpop2,
0             as tcpop3,
0             as tcpop4
   from	dbo.sce_ccpl

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tdme_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tdme_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tdme_s01_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select 
		coddme,
		desdme
	from dbo.sce_tdme 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_d01_MS] 
	@id_party	CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	DELETE FROM sce_tgas 
	WHERE id_party = @id_party
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_d02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_d02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_d02_MS] 
	@id_party	CHAR(12),
	@sistema    CHAR(3),
	@producto   CHAR(3),
	@etapa		CHAR(3)	
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	DELETE FROM sce_tgas 
	WHERE 
		id_party = @id_party AND 
		sistema = @sistema AND 
		producto = @producto AND 
		etapa = @etapa  
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_i01_MS] 
	@id_party       CHAR(12),
	@sistema		CHAR(3),
	@producto		CHAR(3),
	@etapa			CHAR(3),
	@borrado		BIT,
	@m_tarifa       BIT,
	@monto			NUMERIC(15,2)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	INSERT INTO 
	sce_tgas(id_party,
			 sistema,
			 producto,
			 etapa,
			 borrado,
			 m_tarifa,
			 monto) 
	 VALUES (@id_party,
			 @sistema,
			 @producto,
			 @etapa,
			 @borrado,
			 @m_tarifa,
			 @monto)
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_s03_MS] 
	@id_party CHAR(12),
	@sistema  CHAR(3),
	@producto CHAR(3),
	@etapa    CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   declare @monto NUMERIC(15,2),@flag  BIT
   select   @monto = 0,
       @flag = 0

   select   @flag = 1,@monto = monto 
   from dbo.sce_tgas
   where @id_party = id_party and
   @sistema   = sistema  and
   @producto  = producto and
   @etapa     = etapa    and
   borrado    = 0        and
   m_tarifa   = 0

   select   @flag, @monto 
 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_s04_MS] 
	@id_party CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

   SELECT 
		[id_party]
       ,[sistema]
       ,[producto]
       ,[etapa]
       ,[borrado]
       ,[m_tarifa]
       ,[monto]
   FROM 
		[sce_tgas]
   WHERE 
		@id_party = id_party 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_u01_MS]
	@id_party       CHAR(12),
	@borrado		BIT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	UPDATE sce_tgas 
	SET borrado = @borrado
	WHERE id_party = @id_party 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tgas_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tgas_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tgas_u02_MS]
	@id_party       CHAR(12),
	@sistema		CHAR(3),
	@producto		CHAR(3),
	@etapa			CHAR(3),
	@borrado		BIT,
	@m_tarifa       BIT,
	@monto			NUMERIC(15,2)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	UPDATE sce_tgas 
	SET
		id_party    = @id_party,
		sistema		= @sistema,
		producto	= @producto,
		etapa		= @etapa,
		borrado		= @borrado,
	    m_tarifa	= @m_tarifa,
		monto		= @monto
	WHERE 
		id_party	= @id_party	 AND 
		sistema		= @sistema	 AND 
		producto	= @producto  AND 
		etapa		= @etapa	 		
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tint_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tint_d01_MS] 
	@id_party	CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	DELETE FROM sce_tint 
	WHERE id_party = @id_party
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tint_d02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_d02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tint_d02_MS] 
	@id_party	CHAR(12),
	@sistema    CHAR(3),
	@producto   CHAR(3),
	@etapa		CHAR(3)	
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	DELETE FROM sce_tint 
	WHERE 
		id_party = @id_party AND 
		sistema = @sistema AND 
		producto = @producto AND 
		etapa = @etapa  
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tint_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tint_i01_MS] 
	@id_party       CHAR(12),
	@sistema		CHAR(3),
	@producto		CHAR(3),
	@etapa			CHAR(3),
	@borrado		BIT,
	@libor			BIT,
	@prime			BIT,
	@flotante		BIT,
	@tasa			NUMERIC(9,6)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	INSERT INTO 
	sce_tint(id_party,
			 sistema,
			 producto,
			 etapa,
			 borrado,
			 libor,
			 prime,
			 flotante,
			 tasa) 
	 VALUES (@id_party,
			 @sistema,
			 @producto,
			 @etapa,
			 @borrado,
			 @libor,
			 @prime,
			 @flotante,
			 @tasa)
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tint_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tint_s01_MS] 
	@id_party CHAR(12)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

   SELECT
	   [id_party]
      ,[sistema]
      ,[producto]
      ,[etapa]
      ,[borrado]
      ,[libor]
      ,[prime]
      ,[flotante]
      ,[tasa]
   FROM 
       [sce_tint]
   WHERE 
		@id_party = id_party 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tint_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tint_u01_MS]
	@id_party       CHAR(12),
	@sistema		CHAR(3),
	@producto		CHAR(3),
	@etapa			CHAR(3),
	@libor			BIT,
	@prime			BIT,
	@flotante		BIT,
	@tasa			NUMERIC(9,6)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON

	UPDATE sce_tint 
	set libor		= @libor,
		prime		= @prime,
		flotante	= @flotante,
		tasa		= @tasa
	WHERE 
		id_party	= @id_party	and 
		sistema		= @sistema	and 
		producto	= @producto and 
		etapa		= @etapa
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_tint_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_tint_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_tint_u02_MS]
	@id_party       CHAR(12),
	@borrado		BIT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON
	
	UPDATE sce_tint 
	SET borrado		= @borrado
	WHERE 
		id_party	= @id_party	
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_trng_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_trng_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_trng_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	SELECT   
		codnope, 
		grlnope 
	FROM dbo.sce_trng 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--crear sce_usr_s01_MS
CREATE procedure [dbo].[sce_usr_s01_MS] @cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
begin
-- This procedure was converted on Wed Apr 16 16:02:02 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   Select   reemplazos
   from dbo.sce_usr where
   cent_costo = @cencos and
   id_especia = @codusr
   Return
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s02_MS] 
	@cent_costo     CHAR(3),
	@id_especia     CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   

   Select   cent_costo,
			id_especia,
			nombre,
			direccion,
			ciudad,
			telefono,
			fax
   from dbo.sce_usr where
   cent_costo = @cent_costo and
   id_especia = @id_especia
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s03_MS] 
	@cencos CHAR(3) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   cent_costo,
			id_especia,
			cent_super,
			id_super,
			nombre,
			jerarquia,
			fec_ini,
			fec_fin,
			fec_out
   from dbo.sce_usr where
   cent_costo = @cencos
   
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s04_MS]
	@cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   fec_ini,
			fec_fin,
			fec_out
   from dbo.sce_usr 
   where
   cent_costo = @cencos    and
   id_especia = @codusr

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s05_MS] 
	@cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON

   select   rut, jerarquia, cent_costo, id_especia, delegada,
		cent_super, id_super, nombre, direccion, comuna,
		ciudad, seccion, ofic_orige, telefono, swift, fax, tipeje
   from    dbo.sce_usr
   where
   cent_costo = @cencos and
   id_especia = @codusr


END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s06_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s06_MS] 
	@cent_costo	CHAR(3),
	@id_especia	CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   Select   cent_costo+id_especia from dbo.sce_usr
   where   reemplazos like ''%''+@cent_costo+@id_especia+''%''
 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s07_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s07_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s07_MS]
	@cencos  CHAR(3),
	@codusr  CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   nombre from dbo.sce_usr
   where cent_costo = @cencos and
   id_especia  = @codusr

   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s09_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s09_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s09_MS] 
	@cencos         CHAR(3),
	@codusr         CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   ofixusr
   from    dbo.sce_usr where
   cent_costo = @cencos and
   id_especia = @codusr

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s10_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s10_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s10_MS] 
	@cencos         CHAR(3),
    @codusr         CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   rut, jerarquia, cent_costo, id_especia, delegada,
                cent_super, id_super, nombre, direccion, comuna,
                ciudad, seccion, ofic_orige, telefono, swift, fax
   from    dbo.sce_usr
   where   cent_super = @cencos and
   id_super = @codusr
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s12_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s12_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[sce_usr_s12_MS]
AS
begin
-- This procedure was converted on Wed Apr 16 16:12:12 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   select distinct  cent_costo
   from dbo.sce_usr
   return 
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s16_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s16_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--crear sce_usr_s16_MS
CREATE procedure [dbo].[sce_usr_s16_MS] @cent_costo 	CHAR(3),
	@id_especia 	CHAR(2) 
AS
begin
-- This procedure was converted on Wed Apr 16 16:02:02 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/



   select   clasup
   from 	dbo.sce_usr
   where 	cent_costo = @cent_costo	and
   id_especia = @id_especia
   return

end





' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s25_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s25_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s25_MS] 
	@rut_ejec        CHAR(12) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   direccion from dbo.sce_usr where rut = @rut_ejec

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s34_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s34_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_s34_MS]
     @codCentroCosto varchar(4)
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
	 select rut, jerarquia, cent_costo, id_especia, cent_super, id_super, operando,
            nombre, direccion, comuna, ciudad, seccion, ofic_orige, telefono, swift, fax, pzacentral, reemplazos, ofixusr, impresora
     from sce_usr  where cent_costo = @codCentroCosto

END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_s35_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s35_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--crear sce_usr_s35_MS
CREATE procedure [dbo].[sce_usr_s35_MS]
     @codCentroCosto varchar(4),
	 @soloEspecialistas bit
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
	 if (@soloEspecialistas = 1)
	 
		 select cent_costo as CCtUsr, id_especia as codusr, rut as RutUsr, nombre as NomUsr, cent_super as CctSup, id_super as CodSup
		 from sce_usr  where (@codCentroCosto <> ''729'' or (cent_costo = @codCentroCosto and jerarquia=0)) 
		 order by cent_costo + id_especia

	 else
		 select cent_costo as CCtUsr, id_especia as codusr, rut as RutUsr, nombre as NomUsr, cent_super as CctSup, id_super as CodSup
		 from sce_usr  where (NULLIF(@codCentroCosto, '''') IS NULL or cent_costo = @codCentroCosto) 
		 order by cent_costo + id_especia
	

END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--crear sce_usr_u01_MS
CREATE procedure [dbo].[sce_usr_u01_MS] @cencos         CHAR(3),
	@codusr         CHAR(2),
	@reemplazos     VARCHAR(255) 
AS
BEGIN
-- This procedure was converted on Wed Apr 16 15:35:35 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/*****************************************************************************/
   declare @reempla CHAR(255)
   begin
   BEGIN TRAN
      update dbo.sce_usr set reemplazos = @reemplazos  where
      cent_costo = @cencos and
      id_especia = @codusr
      if (@@error <> 0 or @@rowcount = 0)
      begin
         ROLLBACK 
         Select   -1 as codigo, ''Error al grabar datos en Sce_Usr.'' as mensaje
         Return
      end
      COMMIT TRAN
      select   @reempla = reemplazos from dbo.sce_usr where
      cent_costo = @cencos    and
      id_especia = @codusr
      if @reempla = @reemplazos
         Select   0 as codigo,''Grabacion Exitosa'' as mensaje
   else
      Select   -1 as codigo, ''Error al grabar datos en Sce_Usr.'' as mensaje
      Return
   end
   Return
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_usr_u02_MS] 
	@cencos         CHAR(3),
	@codusr         CHAR(2),
	@codfec         CHAR(1),
	@fecdia		datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   BEGIN TRAN

   declare @hora 	CHAR(8),@fecha 	datetime
   begin
      if @codfec = ''I''
      begin
         select   @fecha = fec_out 
		 from dbo.sce_usr
         where 	cent_costo = @cencos and
         id_especia = @codusr
         if convert(CHAR(8),@fecha,112) = convert(CHAR(8),@fecdia,112)
         begin
            ROLLBACK 
            Select   -1, ''Ya se realizo el cierre diario. Pida al supervisor que deshaga el cierre.''
            Return
         end
         update dbo.sce_usr set fec_ini = @fecdia,fec_out = CONVERT(datetime,0)  where 	cent_costo = @cencos and
         id_especia = @codusr
      end
      if @codfec = ''F''
      begin
         update dbo.sce_usr set fec_fin = @fecdia  where 	cent_costo = @cencos and
         id_especia = @codusr
      end
      if @codfec = ''O''
      begin
         update dbo.sce_usr set fec_out = @fecdia  where 	cent_costo = @cencos
      end
      if @codfec = ''N''
      begin
         select   @hora = convert(CHAR(8),fec_out,108) from dbo.sce_usr group by convert(CHAR(8),fec_out,108) order by convert(CHAR(8),fec_out,108) desc
         if @hora = ''23:59:59''
         begin
            ROLLBACK 
            Select   -1, ''No se pudo desacer el cierre llame al encargado de producci¢n.''
            Return
         end
         update dbo.sce_usr set fec_out = CONVERT(datetime,0)  where cent_costo = @cencos
      end
      if @codfec = ''A''		-- Inicio de Dia Pedro.
      begin
         update dbo.sce_usr set fec_ini = @fecdia
      end
      if @codfec = ''B''		-- Fin de Dia Pedro.
      begin
         update dbo.sce_usr set fec_fin = @fecdia
      end
      if @codfec = ''C''
      begin
         update dbo.sce_usr set fec_out = @fecdia
      end
      if @codfec = ''D''
      begin
         update dbo.sce_usr set fec_out = CONVERT(datetime,0)
      end
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select   -1, ''Error al actualizar fecha en Sce_Usr.''
         Return
      end
      COMMIT TRAN
      Select   0,''Grabacion Exitosa''
      return
   end
   Return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_usr_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_usr_u03_MS] @cencos         CHAR(3),
	@codusr         CHAR(2),
	@clasup		CHAR(9) 
AS
begin
-- This procedure was converted on Wed Apr 16 15:46:46 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/*****************************************************************************/ 

      

   update dbo.sce_usr set clasup = @clasup  where	cent_costo = @cencos	and
   id_especia = @codusr
	
   if @@rowcount = 0 or @@error > 0
      select   -1, '' Error al Actualizar Clave del Supervisor ''
else
   select   0, ''Actualización Exitosa ''
   Return



end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_vex_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vex_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_vex_i01_MS]
	@codcct         CHAR(3)        ,
	@codpro         CHAR(2)        ,
	@codesp         CHAR(2)        ,
	@codofi         CHAR(3)        ,
	@codope         CHAR(5)        ,
	@estado         NUMERIC(1,0)     ,
	@codmnd         NUMERIC(3,0)     ,
	@tipcam         NUMERIC(11,4)   ,
	@mtoliq         NUMERIC(15,2)   ,
	@mtoinf         NUMERIC(15,2)   ,
	@mtoest         NUMERIC(15,2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   insert into dbo.sce_vex values(@codcct ,
		@codpro ,
		@codesp ,
		@codofi ,
		@codope ,
		@estado ,
		@codmnd ,
		@tipcam ,
		@mtoliq ,
		@mtoinf ,
		@mtoest)
	
   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   return 0
   return

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_vrng_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vrng_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_vrng_s01_MS] 
	@codcct		CHAR(3),
	@codpro		CHAR(2),
	@codesp		CHAR(2) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   if @codpro = ''03''
   begin
      SELECT   max(id_cobranz)
      FROM    dbo.sce_col
      WHERE   cent_costo = @codcct and
      id_product = @codpro and
      id_especia = @codesp
      return
   end                                    

   if @codpro = ''05''
   begin
      SELECT   max(codope)
      FROM    dbo.sce_pae
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end                               

   if @codpro = ''06''
   begin
      SELECT   max(codope)
      FROM    dbo.sce_xcob
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end                           

   if @codpro = ''07''
   begin
      SELECT   max(codope)
      FROM    dbo.sce_jcci
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end                              
                                         
   if @codpro = ''08''
   begin
      SELECT   max(codope)
      FROM    dbo.sce_jant
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end                              

   if @codpro = ''09''
   begin
      SELECT   max(codope)
      FROM 	dbo.sce_ycce
      WHERE 	codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end

   if @codpro = ''17''
   begin
      SELECT   max(codope)
      FROM    dbo.sce_xret
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end 
/************************************/
/*Real Systems Ltda *****************/
/*Se agrega producto 30**************/                            
   if @codpro = ''20'' or @codpro = ''30''                
/************************************/
   begin
      SELECT   max(codope)
      FROM    dbo.sce_cvd
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end



   if @codpro = ''23''
   begin
      SELECT   isnull(max(codope),''00000'')
      FROM    dbo.sce_fbc
      WHERE   codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp
      return
   end
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_vvi_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vvi_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_vvi_s03_MS] @cctsup         CHAR(3),
	@usrsup         CHAR(2),
	@fecemi         datetime 
AS
begin
-- This procedure was converted on Wed Apr 16 16:02:02 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   select   codcct,
		codpro,
		codesp,
		codofi,
		codope,
		nrocor,
		estado,
		numfol,
		nomben,
		ruttom,
		nomtom,
		mtovvi
   from dbo.sce_vvi where
   cctsup = @cctsup and
   usrsup = @usrsup and
   fecemi = @fecemi
   return
   return
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_vvi_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vvi_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_vvi_u02_MS]
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@codanu      CHAR(6),
	@estado      NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

begin tran
   update dbo.sce_vvi set estado = @estado  where   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   codanu = @codanu	
     
   if (@@error <> 0 or @@rowcount = 0)
   begin
      ROLLBACK 
      return 9
   end
else
   begin
      declare @res1 	NUMERIC(2,0),@fecemi datetime,@numfol NUMERIC(10,0),@ruttom VARCHAR(10),
      @mtovvi NUMERIC(15,2),@esppro CHAR(6),@opesin CHAR(15)

 		-- Cursor que identifica los cheques.-
      declare cursor_vvi cursor for
      select fecemi,
		       numfol,
		       ruttom,
		       mtovvi
      from dbo.sce_vvi where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      codanu = @codanu
 		
		-- Se abre el cursor y se especifica las variables.-
      open cursor_vvi
      fetch cursor_vvi into
      @fecemi,@numfol,@ruttom,@mtovvi
 		
		-- Se recorre el cursor para devolver montos.-
      while @@FETCH_STATUS != -1
      begin                                         
    		        -- Se verifica error en el cursor.-        
         if (@@FETCH_STATUS = -2)
         begin
            ROLLBACK 
            return 9
         end

		        -- Se asignan variables a grabar.-
         select   @esppro = @codcct+@codesp
                	                --codproe
                                                      --anulacion
              			                  --codfun
              			                  --subproe
              			                --codcct
              			                --codpro
                                                --codesp
                                                --codofi
                                                --codope
              		        			--codneg	
                                                      --tippro
                                                      --numcor
                                                      --numcuo
                                                      --nrocan
                                                --fecpro
				                      --codplan
                                                      --cerend
                                                      --reldi
                                                      --aprobii
                                                      --acelet
              			                     --t6     
              			                     --t7     
              			                     --t8     
              			                     --t9     
              			                     --t10     
              				                --mdnfun1
              			                --mtofun1
                                                      --mndfun2
                                                      --mtofun2
                                                      --mndfun3
                                                      --mtofun3
                                                      --mndfun4
                                                      --mtofun4
              			                      --moneda
              			                      --monto 
              			                      --mndint
 			                      --mtoint
              			                      --mndcom
              			                      --mtocomre
                                                      --mtocomsi
                                                      --mtocomno
              			                      --mndimp
              			                      --mtoimp
              			                      --mndgas
             			                      --mtongas
              			                --fecfun 
              			           --fecrec 
              			           --fecven 
              			           --fecnul 
              			                --esppro 
              			                --espown 
              			                      --oficon 
              			                --rutcli 
                                                     --nomcli
                                                      --indclin
                                                      --indclid
                                                     --rutben
                                                     --nomben
                                                      --indbenn
                                                      --indbend
              			                --numdoc 
              			                --fecdoc
              			                      --paridad
              			                      --tcamtab
                                                      --tcamcan
              			           --data  
				                     --prtbco1
                                                     --rutbco1
                                                     --nomprt1
				                      --indprtb1n
				                      --indprtb1d
				                     --prtbco2
                                                     --nomprt2
				                      --indprtb2n
				                      --indprtb2d
                                                     --prtbco3
                                                     --prtbco4
				                      --regimen
                                                      --esavis
                                                      --esconf
				                      --esrstg
                                                      --parcial
                                                      --discrep
				           --fecemi
				           --fecemb
                                                      --tipomod
                                                      --totdoc
                                                      --tenor
                                                      --nrocuot
                                                      --limirr
                                                      --nuplco
                                                      --nuples
                                                      --finpag
                                                     --codanu
                                                      --gastxcta
                                                      --mongastx
                                                      --mtogastx
                                                     --fecest
         select   @opesin = @codcct+@codpro+@codesp+@codofi+@codope

    			-- Se ingresan datos en el Log.-
         EXECUTE @res1 = dbo.sce_gtlg_i01 @codpro,0,''003'',''102'',@codcct,@codpro,@codesp,@codofi,@codope,0,0,0,0,
         0,@fecemi,0,0,0,0,0,'''','''','''','''','''',1,@mtovvi,0,0,0,0,0,0,0,0,0,0,0,0,0,
         0,0,0,0,0,@fecemi,CONVERT(datetime,0),CONVERT(datetime,0),CONVERT(datetime,0),@esppro,@esppro,
         0,@ruttom,'''',0,0,'''','''',0,0,@numfol,@fecemi,0,0,0,''Vale Vista'','''','''',
         '''',0,0,'''','''',0,0,'''','''',0,0,0,0,0,0,CONVERT(datetime,0),CONVERT(datetime,0),0,0,0,0,
         0,0,0,0,'''',0,0,0,'''',0                       --estado

         if @res1 <> 0
         begin
            rollback 
   return 9
         end                                                            

 			-- Se accesa el proximo registro del cursor
         fetch cursor_vvi into
         @fecemi,@numfol,@ruttom,@mtovvi
      end

 		-- Se cierra el cursor.-
      close cursor_vvi
      deallocate cursor_vvi

 		-- Se verifica la existencia de errores.-
      if (@@error <> 0)
      begin
         rollback 
         return 9
      end
   else
      begin
         commit tran
         return 0
      end
   end
   return 
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xanu_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_xanu_s01_MS]
	@cencos         CHAR(7),
	@codusr         CHAR(2),
	@fecing         datetime 
AS
begin
	select   
		numpre, 
		fecpre, 
		codcct+codpro+codesp+codofi+codope as operacion,
	 	estado, 
		codmnd, 
		mtoanu, 
		tippln
	from 
		dbo.sce_xanu 
	where 
		cencos = @cencos and
		codusr = @codusr and
		fecing = @fecing
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_xanu_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_xanu_s02_MS] 
	@numpre      CHAR(7),
	@fecpre      datetime 
AS
begin
	select
		[numpre]
		,[fecpre]
		,[cencos]
		,[codusr]
		,[fecing]
		,[estado]
		,[codcct]
		,[codpro]
		,[codesp]
		,[codofi]
		,[codope]
		,[tipanu]
		,[plzbcc]
		,[rutexp]
		,[prtexp]
		,[indnom]
		,[inddir]
		,[entaut]
		,[numpreo]
		,[fecpreo]
		,[tippln]
		,[codpbc]
		,[numdec]
		,[fecdec]
		,[codadn]
		,[fecven]
		,[codmnd]
		,[mtodol]
		,[mtopar]
		,[mtoanu]
		,[mtopara]
		,[mtodola]
		,[mtodolpo]
		,[obspln]
		,[plnest]
		,[tipaut]
		,[nroaut]
		,[fecaut]
		,[tipcam]
	from 
		dbo.sce_xanu 
	where
		numpre = @numpre and
		fecpre = @fecpre
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_xanu_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xanu_s03_MS]
	@numpre		CHAR(7),
	@fecpre 	datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @retorno CHAR(2)
   if exists(SELECT TOP 1 1 from dbo.sce_xanu where
   @numpre = numpreo and
   @fecpre = fecpreo)
      select   @retorno = ''00''
else
   select   @retorno = ''01'' 


   select   @retorno
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xanu_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xanu_u02_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2), 
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@codanu         CHAR(6),
	@estado         NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
begin tran
   update dbo.sce_xanu set estado = @estado  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope 
	
   if (@@error <> 0 or @@rowcount = 0)
   begin
      rollback 
      return 9
   end
else
   begin
      declare @res1 	  NUMERIC(2,0),@numpre   CHAR(3),@fecpre   datetime,@cencos   CHAR(3),
      @codusr   CHAR(2),@fecing   datetime,@rutexp   CHAR(10),@tippln   NUMERIC(3,0),
      @numdec   CHAR(7),@fecdec   datetime,@codadn   NUMERIC(3,0),
      @fecven   datetime,@codmnd   NUMERIC(3,0),@mtopar   NUMERIC(17,10),
      @mtoanu   NUMERIC(15,2),@esppro   CHAR(6),@espown   CHAR(6),@codofin  NUMERIC(3,0),
      @opesin   CHAR(15),@tippln_c CHAR(3),@data	  CHAR(20)

 		-- Cursor que identifica los cheques.-
      declare cursor_xanu cursor for
      select  numpre,
			fecpre,
			cencos,
			codusr,
			fecing,
			rutexp,
			tippln,
			numdec,
			fecdec,
			codadn,
			fecven,
			codmnd,
			mtopar,
			mtoanu
      from dbo.sce_xanu where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope 
 		
		-- Se abre el cursor y se especifica las variables.-
      open cursor_xanu
      fetch cursor_xanu into
      @numpre,@fecpre,@cencos,@codusr,@fecing,@rutexp,@tippln,@numdec,@fecdec,
      @codadn,@fecven,@codmnd,@mtopar,@mtoanu 

 		-- Se recorre el cursor para devolver montos.-
      while @@FETCH_STATUS != -1
      begin                                         
 		   -- Se verifica error en el cursor.-        
         if (@@FETCH_STATUS = -2)
         begin
            ROLLBACK 
            return 9
         end

		   -- Se Asignan variables a grabar.-
         select   @esppro  = @cencos+@codusr
         select   @espown  = @codcct+@codesp
         select   @codofin = convert(NUMERIC(3,0),@codofi)
         select   @opesin  = @codcct+@codpro+@codesp+@codofi+@codope
         select   @tippln_c = convert(CHAR(3),@tippln)
                                   --codpro
    		                            --codfun
    		                            --subproe
    		                          --numope1
    		                               --numope2
    		     	  			  --codneg 
			                       --codplan
    		                               --t2     
        	      	                       --t3     
        	                               --t4     
  		                               --t5     
 		                               --t6     
 		                          --mdnfun
 		                          --mtofun
 		                                --moneda
 		                                --monto 
 		                                --mndint
 		                                --mtoint
 		                                --mndcom
 		                                --mtocom
 		                                --mndimp
 		                                --mtoimp
        		                        --mndgas
        		                        --mtongas
        		                  --fecpro 
        		                  --fecfun 
        		             --fecrec 
              		             --fecven 
              	  	             --fecnul 
              		    --esppro 
              		                  --espown 
              		                        --oficon 
              		                       --rutcli 
              		                       --numdoc 
              		                  --fecdoc
              		                        --paridad
              		                        --tipcam
              		                    --data  
         select   @data    = @numdec+''-''+convert(CHAR(2),datepart(dd,@fecdec))+''-''+convert(CHAR(2),datepart(mm,@fecdec))+''-''+convert(CHAR(4),datepart(yy,@fecdec))+''-''+convert(CHAR(3),@codadn)                   
    		   -- Se ingresan datos en el Log.-
         EXECUTE @res1 = dbo.sce_glog_i01 @codpro,''001'',''100'',@opesin,'''','''','''','''','''','''','''','''',@codmnd,@mtoanu,0,
         0,0,0,0,0,0,0,0,0,@fecing,@fecpre,CONVERT(datetime,0),CONVERT(datetime,0),CONVERT(datetime,0),
         @esppro,@espown,0,'''','''',@fecpre,0,0,@data,'''',0,0,'''',0,0,0,0,CONVERT(datetime,0),
         CONVERT(datetime,0),0,0
         if @res1 <> 0
         begin
            rollback 
            return 9
         end 

    		   -- Se accesa el proximo registro del cursor
         fetch cursor_xanu into
         @numpre,@fecpre,@cencos,@codusr,@fecing,@rutexp,@tippln,@numdec,@fecdec,
         @codadn,@fecven,@codmnd,@mtopar,@mtoanu
      end

 		-- Se cierra el cursor.-
      close cursor_xanu
      deallocate cursor_xanu

 		-- Se verifica la existencia de errores.-
      if (@@error <> 0)
      begin
         rollback 
         return 9
      end
   else
      begin
         commit tran
         return 0
      end
   end
   return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xanu_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xanu_u03_MS] 
	@numpre         CHAR(7),
	@fecant			datetime,
	@fecpre         datetime,
	@obspln         VARCHAR(255) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
BEGIN TRAN
   if exists(SELECT TOP 1 1 from dbo.sce_xanu where
   numpre  = @numpre  and
   fecpre  = @fecant)
   begin
      update dbo.sce_xanu set fecpre = @fecpre,fecing = @fecpre,obspln = @obspln  where 	numpre = @numpre and
      fecpre = @fecant
      if (@@error <> 0)
      begin
         ROLLBACK 
         Select -1 as codigo, ''Error al actualizar Sce_xAnu'' as mensaje
         Return
      end
      COMMIT TRAN
      Select 0 as codigo, ''Grabacion Exitosa'' as mensaje
   end
else
   begin
      ROLLBACK 
      Select -1 as codigo, ''La Planillas Anulada no existe'' as mensaje
   end
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xanu_u04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xanu_u04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Renato Herrera
-- Create date: 2015-10-16
-- Description:	marca planilla como anulada (estado 9) [sce_xanu]
-- =============================================
CREATE PROCEDURE [dbo].[sce_xanu_u04_MS] 
	@numero_presentacion CHAR(7),
	@fecha_presentacion	DATETIME
AS
BEGIN
	UPDATE [dbo].[sce_xanu]
	SET estado = 9
	WHERE numpre = @numero_presentacion AND fecpre = @fecha_presentacion    
END



' 
END

/****** Object:  StoredProcedure [dbo].[sce_xcob_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xcob_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xcob_s04_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 


     --Cobranza de Exportaciones.-	
   if @codpro = ''06''
   begin
      select   prtexp1,
		indnom1,
		inddir1
      from dbo.sce_xcob where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end

     --Carta de Crédito de Exportaciones.-
   if @codpro = ''09''
   begin
      select   prtexp,
                indexpn,
                indexpd
      from dbo.sce_ycce where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end                           
   if @codpro = ''18''
   begin
      select   prtexp,
                indexpn,
                indexpd
      from dbo.sce_ypag where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end                             

   return

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xdec_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xdec_s01_MS]
    @numdec      CHAR(7),
	@fecdec      datetime,
	@codadn      NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select 
       [numdec]
      ,[fecdec]
      ,[codadn]
      ,[cencos]
      ,[codusr]
      ,[fecing]
      ,[fecact]
      ,[estado]
      ,[tipdec]
      ,[codccv]
      ,[rutexp1]
      ,[prtexp1]
      ,[indnom1]
      ,[inddir1]
      ,[porcen1]
      ,[valret1]
      ,[valcom1]
      ,[valgas1]
      ,[valliq1]
      ,[valfle1]
      ,[valseg1]
      ,[valret1c]
      ,[valcom1c]
      ,[valgas1c]
      ,[valliq1c]
      ,[valfle1c]
      ,[valseg1c]
      ,[rutexp2]
      ,[prtexp2]
      ,[indnom2]
      ,[inddir2]
      ,[porcen2]
      ,[valret2]
      ,[valcom2]
      ,[valgas2]
      ,[valliq2]
      ,[valfle2]
      ,[valseg2]
      ,[valret2c]
      ,[valcom2c]
      ,[valgas2c]
      ,[valliq2c]
      ,[valfle2c]
      ,[valseg2c]
      ,[diaret]
      ,[fecret]
      ,[codpbc]
      ,[numinf]
      ,[fecinf]
   from dbo.sce_xdec where
   numdec = @numdec and
   fecdec = @fecdec and
   codadn = @codadn

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xdec_u01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdec_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xdec_u01_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@codneg         NUMERIC(2,0),
	@codsec         NUMERIC(2,0),
	@nrocan         NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare @numdec         CHAR(7),@fecdec         datetime,@codadn         NUMERIC(3,0),
   @valret1c       NUMERIC(13,2),@valcom1c       NUMERIC(13,2),@valgas1c       NUMERIC(13,2),
   @valliq1c       NUMERIC(13,2),@valfle1c       NUMERIC(13,2),
   @valseg1c       NUMERIC(13,2),@valret2c       NUMERIC(13,2),
   @valcom2c       NUMERIC(13,2),@valgas2c       NUMERIC(13,2),@valliq2c       NUMERIC(13,2),
   @valfle2c       NUMERIC(13,2),@valseg2c       NUMERIC(13,2)
	
	-- Cursor que identifica los rebajes de xdec.-
   declare cursor_xdec cursor for 
   select  numdec,
		fecdec,
		codadn,
		valret1c,
		valcom1c,
		valgas1c,
		valliq1c,
		valfle1c,
		valseg1c,
		valret2c,
		valcom2c,
		valgas2c,
		valliq2c,
		valfle2c,
		valseg2c
   from dbo.sce_xdep where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   codneg = @codneg and
   codsec = @codsec and
   nrocan = @nrocan
	
	-- Se abre el cursor y se especifica las variables.-
   open cursor_xdec
   fetch cursor_xdec into
   @numdec,@fecdec,@codadn,@valret1c,@valcom1c,@valgas1c,@valliq1c,@valfle1c,
   @valseg1c,@valret2c,@valcom2c,@valgas2c,@valliq2c,@valfle2c,@valseg2c
	       
	-- Se recorre el cursor para devolver montos.-
   while @@FETCH_STATUS != -1
   begin
	  
	  -- Se verifica error en el cursor.-
      if (@@FETCH_STATUS = -2)
         return 9
	  
	  -- Se devuelve el monto del Exp-1 a la Declaracion.-
      update dbo.sce_xdec set valret1c = valret1c+@valret1c,valcom1c = valcom1c+@valcom1c,valgas1c = valgas1c+@valgas1c,
      valliq1c = valliq1c+@valliq1c,valfle1c = valfle1c+@valfle1c,
      valseg1c = valseg1c+@valseg1c  where
      numdec = @numdec and
      fecdec = @fecdec and
      codadn = @codadn

	  -- Se devuelve el monto del Exp-2 a la Declaracion.-
      update dbo.sce_xdec set valret2c = valret2c+@valret2c,valcom2c = valcom2c+@valcom2c,valgas2c = valgas2c+@valgas2c,
      valliq2c = valliq2c+@valliq2c,valfle2c = valfle2c+@valfle2c,
      valseg2c = valseg2c+@valseg2c  where
      numdec = @numdec and
      fecdec = @fecdec and
      codadn = @codadn

	  -- Se accesa el proximo registro del cursor
      fetch cursor_xdec into
      @numdec,@fecdec,@codadn,@valret1c,@valcom1c,@valgas1c,@valliq1c,@valfle1c,
      @valseg1c,@valret2c,@valcom2c,@valgas2c,@valliq2c,@valfle2c,@valseg2c
   end

	-- Se cierra el cursor.-
   close cursor_xdec
   deallocate cursor_xdec

	-- Se verifica la existencia de errores.-
   if (@@error <> 0)
      return 9
else
   return 0
	   
END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xdoc_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xdoc_i01_MS] 
	@codcct      	CHAR(3),
	@codpro      	CHAR(2),
	@codesp      	CHAR(2),
	@codofi      	CHAR(3),
	@codope      	CHAR(5),
	@nrocor      	NUMERIC(3,0),
	@cencos			CHAR(3),
	@codusr			CHAR(2),
	@coddoc      	NUMERIC(3,0),
	@fecing      	datetime,
	@codmem         NUMERIC(8,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

BEGIN TRAN
   if not exists(SELECT TOP 1 1 from dbo.sce_xdoc where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   nrocor = @nrocor)
   begin
      insert into dbo.sce_xdoc(codcct,
			codpro,
			codesp,
			codofi,
			codope,
			nrocor,
			cencos,
			codusr,
			coddoc,
			fecing,
			codmem)
		values(@codcct,
			@codpro,
			@codesp,
			@codofi,
			@codope,
			@nrocor,
			@cencos,
			@codusr,
			@coddoc,
			@fecing,
			@codmem)
   end
   if (@@error <> 0)
   begin
      rollback 
      select   9
   end
else
   begin
      commit tran
      select   0
   end
   Return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xdoc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xdoc_s01_MS] 
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@nrocor      NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   
		fecing,
		codmem
   from 	dbo.sce_xdoc
   where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   nrocor = @nrocor
		
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xdoc_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xdoc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xdoc_s02_MS] 
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   max(nrocor) 
   from dbo.sce_xdoc 
   where
	   codcct = @codcct and
	   codpro = @codpro and
	   codesp = @codesp and
	   codofi = @codofi and
	   codope = @codope

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xplv_MS] @numpre CHAR(7)
AS
BEGIN
	
	IF EXISTS(SELECT TOP 1 1 FROM dbo.sce_xplv WITH(NOLOCK) WHERE numpre = @numpre) /****1º Planillas Visibles***/
	SELECT TOP 1 (codcct + ''-'' + codpro + ''-'' + codesp + ''-'' + codofi + ''-'' + codope) AS Operacion, ''VISIBLE'' AS Tipo
		FROM dbo.sce_xplv WITH(NOLOCK)
		WHERE numpre = @numpre
	ELSE
		IF EXISTS(SELECT TOP 1 1 FROM dbo.sce_xanu WITH(NOLOCK) WHERE numpre = @numpre) /***2º Planillas Anulación***/
		SELECT TOP 1 (codcct + ''-'' + codpro + ''-'' + codesp + ''-'' + codofi + ''-'' + codope) AS Operacion, ''ANULACIÓN'' AS Tipo 
			FROM dbo.sce_xanu WITH(NOLOCK)
			WHERE numpre = @numpre
		ELSE
			IF EXISTS(SELECT TOP 1 1 FROM dbo.sce_pli WITH(NOLOCK) WHERE numpli = @numpre) /***3º Planillas Invisibles***/
			SELECT TOP 1 (codcct + ''-'' + codpro + ''-'' + codesp + ''-'' + codofi + ''-'' + codope) AS Operacion, ''INVISIBLE'' AS Tipo
				FROM dbo.sce_pli WITH(NOLOCK) 
				WHERE numpli = @numpre
			ELSE
				SELECT '''' AS Operacion, '''' AS Tipo
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_s08_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_s08_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_xplv_s08_MS] @cencos         CHAR(7),
	@codusr         CHAR(2),
	@fecing         datetime 
AS
begin

-- Nombre original: dbo.sce_xplv_s08
-- Observaciones: se agrega nombre de columna Operacion
Select   
	numpre, 
	fecpre, 
	codcct+codpro+codesp+codofi+codope as operacion,
	estado, 
	codmnd, 
	mtobru, 
	mtoliq, 
	tippln
from
	dbo.sce_xplv
where
	cencos = @cencos
	and codusr = @codusr
	and fecing >= dateadd(dd,0,@fecing)
	and fecing <  dateadd(dd,+1,@fecing)
end


' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_s10_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_s10_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xplv_s10_MS]
	@numpre      CHAR(7),
	@fecpre      datetime 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   Select   numpre,
		fecpre,
		tippln,
		fecing,
		cencos,
		codusr,
		codcct,
		codpro,
		codesp,
		codofi,
		codope,
		estado,
		numdec,
		fecdec,
		codadn,
		fecven,
		rutexp,
		prtexp,
		indnom,
		inddir,
		codmnd,
		mtopar,
		mtodol,
		plzbcc,
		plnest
   from dbo.sce_xplv where
   numpre = @numpre  and
   fecpre = @fecpre
   Return
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_s11_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_s11_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[sce_xplv_s11_MS] @numpre      CHAR(7),
	@fecpre      datetime 
AS
begin
-- This procedure was converted on Wed Apr 16 15:40:40 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

/************************************************************************/

   Select   numpre,
		fecpre,
		cencos,
		codusr,
		fecing,
		fecact,
		tippln,
		codcct,
		codpro,
		codesp,
		codofi,
		codope,
		codanu,
		estado,
		numdec,
		fecdec,
		codadn,
		fecven,
		rutexp,
		prtexp,
		indnom,
		inddir,
		codmnd,
		mtobru,
		totanu,
		mtocom,
		mtootg,
		mtoliq,
		mtopar,
		mtodol,
		tipcam,
		tipcamo,
		plzbcc,
		dfocea,
		dfoctf,
		dfocbc,
		dfonpr,
		dfofpr,
		afimnd,
		afipar,
		afimto,
		afimtod,
		afiven,
		diepbc,
		dienum,
		diefec,
		obspln,
		dedcom,
		dedfle,
		dedseg,
		plnest
   from dbo.sce_xplv where
   numpre = @numpre  and
   fecpre = @fecpre
   Return
end






' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_u02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xplv_u02_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2), 
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@codanu         CHAR(6),
	@estado         NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

begin tran
   update dbo.sce_xplv set estado = @estado  where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   codanu = @codanu

   if (@@error <> 0 or @@rowcount = 0)
      return 9
else
   begin
      declare 	 @res1 		NUMERIC(2,0),@numpre 	CHAR(7),@fecpre 	datetime,@cencos 	CHAR(3),
      @codusr 	CHAR(2),@fecing 	datetime,@tippln 	NUMERIC(3,0),@numdec 	CHAR(7),
      @fecdec 	datetime,@codadn	NUMERIC(3,0),@fecven	datetime,@rutexp	CHAR(10),
      @codmnd	NUMERIC(3,0),@mtoliq	NUMERIC(15,2),@mtopar	NUMERIC(17,10),
      @tipcam	NUMERIC(11,4),@esppro 	CHAR(6),@espown 	CHAR(6),@opesin	CHAR(15),
      @tippln_c      CHAR(3),@data          CHAR(20)


 	   -- Cursor que identifica los cheques.-
      declare cursor_xpl cursor for
      select  numpre,
		   fecpre,
		   cencos,
		   codusr,
		   fecing,
	 	   tippln,
		   numdec,
		   fecdec,
		   codadn,
		   fecven,
		   rutexp,
		   codmnd,
		   mtoliq,
		   mtopar,
		   tipcam
      from dbo.sce_xplv where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope and
      codanu = @codanu

 	   -- Se abre el cursor y se especifica las variables.-
      open cursor_xpl
      fetch cursor_xpl into
      @numpre,@fecpre,@cencos,@codusr,@fecing,@tippln,@numdec,@fecdec,@codadn,
      @fecven,@rutexp,@codmnd,@mtoliq,@mtopar,@tipcam 

 	   -- Se recorre el cursor para devolver montos.-
      while @@FETCH_STATUS != -1
      begin                                         
    		
		-- Se verifica error en el cursor.-        
         if (@@FETCH_STATUS = -2)
         begin
            ROLLBACK 
            return 9
         end
    		-- Se asignan las variables a grabar.-
         select   @esppro   = @cencos+@codusr
         select   @espown   = @codcct+@codesp
         select   @opesin   = @codcct+@codpro+@codesp+@codofi+@codope
         select   @tippln_c = convert(CHAR(3),@tippln)
		                --codproe
                                              --anulacion
              		                  --codfun
              		                  --subproe
                                        --codcct
              		                --codpro
                                        --codesp
                                        --codofi
                                        --codope
              					--codneg
                                              --tippro
                                              --numcor
                                              --numcuo
                                              --nrocan
                                        --fecpro
			                --codplan
                                              --cerend
                                              --reldi
                                              --aprobii
                                              --acelet
              		                     --t6     
             	 	                     --t7     
              		                     --t8     
              		                     --t9     
              		                     --t10     
              		                      --mdnfun1
 		                      --mtofun1
                                              --mndfun2
                                              --mtofun2
                                              --mndfun3
                                              --mtofun3
                                              --mndfun4
                                              --mtofun4
              		                      --moneda
              		                      --monto 
              		                      --mndint
              		                      --mtoint
              		                      --mndcom
              		                      --mtocomre
                                              --mtocomsi
                                              --mtocomno
              		                      --mndimp
              		                      --mtoimp
              		                      --mndgas
              		                      --mtongas
              		                --fecfun 
              		           --fecrec 
              		                --fecven 
              		           --fecnul 
              		                --esppro 
              		                --espown 
              		                      --oficon 
            		                --rutcli 
                                             --nomcli
                                              --indclin
                                              --indclid
                                             --rutben
                                             --nomben
                                              --indbenn
                                              --indbend
			                --numdoc 
              		                --fecdoc
              		                --paridad
              		                --tcamtab
                                              --tcamcan
              		                  --data  
						--prtbco1
                                             --rutbco1
                                             --nomprt1
						--indprtb1n
						--indprtb1d
						--prtbco2
                                             --nomprt2
						--prtprtb2n
						--prtprtb2d
                                             --prtbco3
                                             --prtbco4
						--regimen
                                              --esavis
                                              --esconf
						--esrstg
                                              --parcial
                                              --discrep
					--fecemi
					--fecemb
                                              --tipomod
                                              --totdoc
                                              --tenor
                                              --nrocuot
                                              --limirr
                                              --nuplco
                                              --nuples
                                              --finpag
                                             --codanu
                                              --gastxcta
                                              --mongastx
                                              --mtogastx
                                             --fecest
         select   @data     = @numdec+''-''+convert(CHAR(2),datepart(dd,@fecdec))+''/''+convert(CHAR(2),datepart(mm,@fecdec))+''/''+convert(CHAR(4),datepart(yy,@fecdec))+''-''+convert(CHAR(3),@codadn) 
		-- Se ingresan datos en el Log.-
         EXECUTE @res1 = dbo.sce_gtlg_i01 @codpro,0,''001'',''100'',@codcct,@codpro,@codesp,@codofi,@codope,0,0,0,0,
         0,@fecpre,@tippln,0,0,0,0,'''','''','''','''','''',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
         0,0,0,0,0,@fecpre,CONVERT(datetime,0),@fecven,CONVERT(datetime,0),@esppro,@espown,0,
         @rutexp,'''',0,0,'''','''',0,0,@numpre,@fecpre,@mtopar,@tipcam,0,@data,'''','''',
         '''',0,0,'''','''',0,0,'''','''',0,0,0,0,0,0,CONVERT(datetime,0),CONVERT(datetime,0),0,0,0,0,
         0,0,0,0,'''',0,0,0,'''',0                       --estado

         if @res1 <> 0
         begin
            rollback 
            return 9
         end                                                     

    		-- Se accesa el proximo registro del cursor
         fetch cursor_xpl into
         @numpre,@fecpre,@cencos,@codusr,@fecing,@tippln,@numdec,@fecdec,@codadn,
         @fecven,@rutexp,@codmnd,@mtoliq,@mtopar,@tipcam
      end

 	   -- Se cierra el cursor.-
      close cursor_xpl
      deallocate cursor_xpl

 	   -- Se verifica la existencia de errores.-
      if (@@error <> 0)
      begin
         rollback 
         return 9
      end
   else
      begin
         commit tran
         return 0
      end
   end
   return	
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_u03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_u03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Renato Herrera
-- Create date: 2015-10-16
-- Description:	marca planilla como anulada (estado 9) [sce_xplv]
-- =============================================
CREATE PROCEDURE [dbo].[sce_xplv_u03_MS] 
	@numero_presentacion CHAR(7),
	@fecha_presentacion	DATETIME
AS
BEGIN
	UPDATE [dbo].[sce_xplv]
	SET estado = 9
	WHERE numpre = @numero_presentacion AND fecpre = @fecha_presentacion    
END


' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_w02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_w02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xplv_w02_MS] 
	@numpre         CHAR(7),
	@fecpre         datetime,
	@cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecact		datetime,
	@tippln         NUMERIC(3,0),        
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@codanu         CHAR(6),
	@estado         NUMERIC(2,0),
	@numdec         CHAR(7),
	@fecdec         datetime,
	@codadn         NUMERIC(3,0),
	@fecven         datetime,
	@rutexp         CHAR(10),
	@prtexp         CHAR(12),
	@indnom         NUMERIC(2,0),
	@inddir         NUMERIC(2,0),
	@codmnd         NUMERIC(3,0),
	@mtobru         NUMERIC(15,2),
	@mtocom         NUMERIC(15,2), 
	@mtootg         NUMERIC(15,2), 
	@mtoliq         NUMERIC(15,2), 
	@mtopar         NUMERIC(17,10), 
	@mtodol         NUMERIC(15,2), 
	@tipcam         NUMERIC(11,4), 
	@tipcamo	NUMERIC(11,4),
	@plzbcc         NUMERIC(2,0),
	@dfocea         NUMERIC(3,0),
	@dfoctf         NUMERIC(3,0),
	@dfocbc         NUMERIC(2,0),
	@dfonpr         CHAR(7),
	@dfofpr         datetime,
	@afimnd         NUMERIC(3,0),
	@afipar         NUMERIC(17,10),
	@afimto         NUMERIC(15,2),
	@afimtod        NUMERIC(15,2), 
	@afiven         NUMERIC(4,0),
	@diepbc         NUMERIC(2,0),
	@dienum         CHAR(7),
	@diefec         datetime,
	@obspln         VARCHAR(255),
	@dedcom		BIT,
	@dedfle		BIT,
	@dedseg		BIT,
	@plnest		BIT,
	@secben		NUMERIC(2,0),
	@secfin		NUMERIC(2,0),
	@prcpar		NUMERIC(4,1),
	@nomcom 	CHAR(30) 

AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-09   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   declare
   @ok_xplv	NUMERIC(1,0),@ok_plib	NUMERIC(1,0)

   begin
   BEGIN TRAN
      select   @ok_xplv = 0
      select   @ok_plib = 0
      if not exists(SELECT TOP 1 1 from dbo.sce_xplv where
      numpre = @numpre and
      fecpre = @fecpre)
      begin
         insert into dbo.sce_xplv values(@numpre,
			@fecpre,
			@cencos,
			@codusr,
			@fecact,
			@fecact,
			@tippln,
			@codcct,
			@codpro,
			@codesp,
			@codofi,
			@codope,
			@codanu,
			@estado,
			@numdec,
			@fecdec,
			@codadn,
			@fecven,
			@rutexp,
			@prtexp,
			@indnom,
			@inddir,
			@codmnd,
			@mtobru,
			0,
			@mtocom,
			@mtootg,
			@mtoliq,
			@mtopar,
			@mtodol,
			@tipcam,
			@tipcamo,
			@plzbcc,
			@dfocea,
			@dfoctf,
			@dfocbc,
			@dfonpr,
			@dfofpr,
			@afimnd,
			@afipar,
			@afimto,
			@afimtod,
			@afiven,
			@diepbc,
			@dienum,
			@diefec,
			@obspln,
			@dedcom,
			@dedfle,
			@dedseg,
			@plnest,
			@nomcom)
		
         if (@@rowcount > 0 and @@error = 0)
            select   @ok_xplv = 1
      end
   else
      begin
         update dbo.sce_xplv set fecact  = @fecact,tippln  = @tippln,codcct  = @codcct,codpro  = @codpro,
         codesp  = @codesp,codofi  = @codofi,codope  = @codope,estado  = @estado,
         numdec  = @numdec,fecdec  = @fecdec,codadn  = @codadn,fecven  = @fecven,
         rutexp  = @rutexp,prtexp  = @prtexp,indnom  = @indnom,inddir  = @inddir,
         codmnd  = @codmnd,mtobru  = @mtobru,mtocom  = @mtocom,mtootg  = @mtootg,
         mtoliq  = @mtoliq,mtopar  = @mtopar,mtodol  = @mtodol,tipcam  = @tipcam,
         tipcamo = @tipcamo,plzbcc  = @plzbcc,dfocea  = @dfocea,dfoctf  = @dfoctf,
         dfocbc  = @dfocbc,dfonpr  = @dfonpr,dfofpr  = @dfofpr,afimnd  = @afimnd,
         afipar  = @afipar,afimto  = @afimto,afimtod = @afimtod,
         afiven  = @afiven,diepbc  = @diepbc,dienum  = @dienum,diefec  = @diefec,
         obspln  = @obspln,dedcom  = @dedcom,dedfle  = @dedfle,dedseg  = @dedseg,
         plnest  = @plnest,nomcom  = @nomcom  where
         numpre = @numpre and
         fecpre = @fecpre
         if (@@rowcount > 0 and @@error = 0)
            select   @ok_xplv = 1
      end
      if not exists(SELECT TOP 1 1 from dbo.sce_plib where
      numpli = @numpre and
      fecpli = @fecpre)
      begin
         insert into dbo.sce_plib values(@numpre,
                	@fecpre,
                	0,
                	@secben,
                	@secfin,
                	@prcpar)
        	
         if (@@rowcount > 0 and @@error = 0)
            select   @ok_plib = 1
      end
   else
      begin
         update dbo.sce_plib set secben  = @secben,secfin  = @secfin,prcpar  = @prcpar  where
         numpli = @numpre and
         fecpli = @fecpre
         if (@@rowcount > 0 and @@error = 0)
            select   @ok_plib = 1
      end
      if (@ok_xplv = 0 or @ok_plib = 0)
      begin
         rollback 
         select   9
      end
   else
      begin
         commit tran
         select   0
      end
   end


END

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xplv_w03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xplv_w03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xplv_w03_MS] 
	@numpre         CHAR(7),
	@fecant		datetime,
	@fecpre         datetime,
	@mtobru         NUMERIC(15,2),
	@mtocom         NUMERIC(15,2), 
	@mtootg         NUMERIC(15,2), 
	@tipcam         NUMERIC(11,4), 
	@obspln         VARCHAR(255) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     

BEGIN TRAN
   update dbo.sce_xplv set fecpre = @fecpre,fecing = @fecpre,mtobru = @mtobru,mtocom = @mtocom,mtootg = @mtootg,
   tipcam = @tipcam,obspln = @obspln  where	numpre = @numpre and
   fecpre = @fecant
	


   if (@@error = 0)
   begin
      if exists(SELECT TOP 1 1 from dbo.sce_plia where
      numpli = @numpre and
      fecpli = @fecant)
         update dbo.sce_plia set fecpli = @fecpre  where numpli = @numpre and
         fecpli = @fecant
   end


   if (@@error <> 0)
   begin
      rollback 
      select   9
   end
else
   begin
      commit tran
      select   0
   end

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xprt_d01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xprt_d01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xprt_d01_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   begin
      delete from dbo.sce_xprt where
      codcct = @codcct and
      codpro = @codpro and
      codesp = @codesp and
      codofi = @codofi and
      codope = @codope
   end
if (@@error <> 0)
   return 9
else
   return 0
end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xprt_i01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xprt_i01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xprt_i01_MS]
	@codcct         CHAR(3),
	@codpro         CHAR(2),
	@codesp         CHAR(2),
	@codofi         CHAR(3),
	@codope         CHAR(5),
	@posprt         NUMERIC(2,0),
	@codprt         CHAR(12),
	@indnom         NUMERIC(2,0),
	@inddir         NUMERIC(2,0),
	@enoper         BIT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   begin
      insert into dbo.sce_xprt(codcct,
			codpro,
			codesp,
			codofi,
			codope,
			posprt,
			codprt,
			indnom,
			inddir,
			enoper)
		values(@codcct,
			@codpro,
			@codesp,
			@codofi,
			@codope,
			@posprt,
			@codprt,
			@indnom,
			@inddir,
			@enoper)
   end
   if (@@error <> 0)
      return 9
else
   return 0
   return

end

' 
END

/****** Object:  StoredProcedure [dbo].[sce_xret_s05_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_xret_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_xret_s05_MS]
	@codcct	CHAR(3),
	@codpro	CHAR(2),
	@codesp CHAR(2),
	@codofi	CHAR(3),
	@codope	CHAR(5),
	@codneg	NUMERIC(2,0),
	@codsec NUMERIC(2,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   prtexp,
       indnome,
       inddire
   from dbo.sce_xret
   where @codcct = codcct and
   @codpro = codpro and
   @codesp = codesp and
   @codofi = codofi and
   @codope = codope and
   @codneg = codneg and
   @codsec = codsec
   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[scedev_jcci_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scedev_jcci_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[scedev_jcci_MS]
	@mesdev NUMERIC(6,0) 

AS
BEGIN
BEGIN TRAN
-- AKZ-RPF 20150108 Se agrega order by al cursor 
-- This procedure was converted on Wed Apr 16 13:47:47 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
   declare @meshoy NUMERIC(2,0) -- Numero del Mes actual
   declare @anohoy NUMERIC(4,0) -- Numero del A¤o actual

--30/08/2008 - Real Systems Ltda.
--Cambio por IFRS Etapa IV
   declare @lc_estado   INT,@lc_num_ope  VARCHAR(15),@lc_es_penal INT,@cur_codcct  VARCHAR(3),
   @cur_codpro  VARCHAR(2),@cur_codesp  VARCHAR(2),@cur_codofi  VARCHAR(3),
   @cur_codope  VARCHAR(5),@cur_fecven  datetime,@lc_codwsh   VARCHAR(3),@lc_ano_mes  NUMERIC(6,0),
   @diasmes     NUMERIC(2,0),@fecdevult   datetime

   select   @lc_ano_mes = @mesdev
--Fin

   select   @meshoy = convert(NUMERIC(2,0),SUBSTRING(convert(VARCHAR(6),@mesdev),5,2))
   select   @anohoy = convert(NUMERIC(4,0),SUBSTRING(convert(VARCHAR(6),@mesdev),1,4))   
   
   declare dev_jcci cursor LOCAL for
   select p.codcct,
       p.codpro,
       p.codesp,
       p.codofi,
       p.codope,
       p.fecven
   from dbo.sce_jcci p,
     dbo.sce_jcco q
   where p.codcct = q.codcct
   and   p.codpro = q.codpro
   and   p.codesp = q.codesp
   and   p.codofi = q.codofi
   and   p.codope = q.codope
   and   (p.estado = 3 or p.estado = 4)
   and   ((datepart(yy,p.fecing) <= @anohoy and
   datepart(mm,p.fecing) <= @meshoy) or
   datepart(yy,p.fecing) < @anohoy)
   and   p.salpla > 0
   ORDER BY p.codcct, p.codpro, p.codesp, p.codofi, p.codope                  -- Akzio migracion 20150108

   --02/08/2008 - Real Systems Ltda.
--Cambios por IFRS IV
--Codigo Antiguo
/*
insert scedev_ent   
select p.codcct, p.codpro, p.codesp, p.codofi, p.codope,
       0       , 0       , 0       , 0       , p.moncci,
       p.salpla, 0       , q.inicre, p.fecven, 0       ,
      0        , 0       , 0       , 0       , 0       ,
      0        , q.tascre, q.anobas, 0       , ''005''   ,
      1
from sce_jcci p, sce_jcco q
where p.codcct = q.codcct and
      p.codpro = q.codpro and
      p.codesp = q.codesp and
      p.codofi = q.codofi and
      p.codope = q.codope and
     (p.estado=3 or p.estado=4) and
     ((datepart(yy,p.fecing) <= @anohoy and
     datepart(mm,p.fecing) <= @meshoy) or 
     datepart(yy,p.fecing) < @anohoy) and
     p.salpla > 0
 
--select @@rowcount

if @@error <> 0
   return 8

insert scedev_ent   
select p.codcct, p.codpro, p.codesp, p.codofi, p.codope,
       0       , 0       , 0       , 0       , p.moncci,
       q.salvis, 0       , q.inicre, p.fecven, 0       ,
       0       , 0       , 0       , 0       , 0       ,
       0       , q.tascre, q.anobas, 0       , ''005''   ,
       1
from sce_jcci p, sce_jcco q
where p.codcct = q.codcct and
      p.codpro = q.codpro and
      p.codesp = q.codesp and
      p.codofi = q.codofi and
      p.codope = q.codope and
      (p.estado=3 or p.estado=4) and
      ((datepart(yy,p.fecing) <= @anohoy and
      datepart(mm,p.fecing) <= @meshoy) or 
      datepart(yy,p.fecing) < @anohoy) and
      q.salvis > 0
 
if @@error <> 0
   return 9
*/
--Codigo Nuevo
--parte plazo
   open dev_jcci

   while 1 = 1
   begin
      fetch dev_jcci into @cur_codcct,@cur_codpro,@cur_codesp,@cur_codofi,@cur_codope,@cur_fecven
      if (@@FETCH_STATUS = -1)
         BREAK
      if (@@FETCH_STATUS = -2)
      begin
         rollback 
         return 6
      end
      select   @lc_num_ope = ''''
      select   @lc_num_ope = @cur_codcct+@cur_codpro+@cur_codesp+@cur_codofi+@cur_codope
      select   @lc_estado = estado
      from dbo.sce_ods_informacion
      where num_ope = @lc_num_ope
      and   ano_mes = @lc_ano_mes
      if @lc_estado IS NULL or @lc_estado < 1
         select   @lc_estado = 1
      EXECUTE scedias_mes @meshoy,@anohoy,@diasmes OUTPUT
      select   @fecdevult = convert(VARCHAR(2),@meshoy)+''.''+convert(VARCHAR(2),@diasmes)+''.''+convert(VARCHAR(4),@anohoy)
      select   @lc_es_penal = 1
      if @cur_fecven < @fecdevult
         select   @lc_es_penal = 2
      if @lc_estado = 2
      begin
         select   @lc_codwsh = ''105''
      end
   else if @lc_estado = 3
      begin
         select   @lc_codwsh = ''205''
      end
   else
      begin
         select   @lc_codwsh = ''005''
      end
      insert dbo.scedev_ent
      select p.codcct,
          p.codpro,
          p.codesp,
          p.codofi,
          p.codope,
          0,
          0,
          0,
          0,
          p.moncci,
          p.salpla,
          0,
          q.inicre,
          p.fecven,
          0,
          0,
          0,
          0,
          0,
          0,
          0,
          q.tascre,
          q.anobas,
          0, --Devengamiento en M/N
          @lc_codwsh,
          1,
          @lc_estado,
          @lc_es_penal
      from dbo.sce_jcci p,
        dbo.sce_jcco q
      where p.codcct = @cur_codcct
      and   p.codpro = @cur_codpro
      and   p.codesp = @cur_codesp
      and   p.codofi = @cur_codofi
      and   p.codope = @cur_codope
      and   p.codcct = q.codcct
      and   p.codpro = q.codpro
      and   p.codesp = q.codesp
      and   p.codofi = q.codofi
      and   p.codope = q.codope
      and   (p.estado = 3 or p.estado = 4)
      and   ((datepart(yy,p.fecing) <= @anohoy and
      datepart(mm,p.fecing) <= @meshoy) or
      datepart(yy,p.fecing) < @anohoy)
      and   p.salpla > 0
          
   --select @@rowcount

      if @@error <> 0
	  begin
         rollback
		 return 8
      end
   end
   close dev_jcci
   deallocate dev_jcci

--parte vista
declare dev_jcci cursor LOCAL for
   select p.codcct,
       p.codpro,
       p.codesp,
       p.codofi,
       p.codope,
       p.fecven
   from dbo.sce_jcci p,
     dbo.sce_jcco q
   where p.codcct = q.codcct
   and   p.codpro = q.codpro
   and   p.codesp = q.codesp
   and   p.codofi = q.codofi
   and   p.codope = q.codope
   and   (p.estado = 3 or p.estado = 4)
   and   ((datepart(yy,p.fecing) <= @anohoy and
   datepart(mm,p.fecing) <= @meshoy) or
   datepart(yy,p.fecing) < @anohoy)
   and   q.salvis > 0
   ORDER BY p.codcct, p.codpro, p.codesp, p.codofi, p.codope                  -- Akzio migracion 20150108

   open dev_jcci

   while 1 = 1
   begin
      fetch dev_jcci into @cur_codcct,@cur_codpro,@cur_codesp,@cur_codofi,@cur_codope,@cur_fecven
      if (@@FETCH_STATUS = -1)
         BREAK
      if (@@FETCH_STATUS = -2)
      begin
         rollback 
         return 6
      end
      select   @lc_num_ope = ''''
      select   @lc_num_ope = @cur_codcct+@cur_codpro+@cur_codesp+@cur_codofi+@cur_codope
      select   @lc_estado = estado
      from dbo.sce_ods_informacion
      where num_ope = @lc_num_ope
      and   ano_mes = @lc_ano_mes
      if @lc_estado IS NULL or @lc_estado < 1
         select   @lc_estado = 1
      EXECUTE scedias_mes @meshoy,@anohoy,@diasmes OUTPUT
      select   @fecdevult = convert(VARCHAR(2),@meshoy)+''.''+convert(VARCHAR(2),@diasmes)+''.''+convert(VARCHAR(4),@anohoy)
      select   @lc_es_penal = 1
      if @cur_fecven < @fecdevult
         select   @lc_es_penal = 2
      if @lc_estado = 2
      begin
         select   @lc_codwsh = ''105''
      end
   else if @lc_estado = 3
      begin
         select   @lc_codwsh = ''205''
      end
   else
      begin
         select   @lc_codwsh = ''005''
      end
      insert dbo.scedev_ent
      select p.codcct,
          p.codpro,
          p.codesp,
          p.codofi,
          p.codope,
          0,
          0,
          0,
          0,
          p.moncci,
          q.salvis,
          0,
          q.inicre,
          p.fecven,
          0,
          0,
          0,
          0,
          0,
          0,
          0,
          q.tascre,
          q.anobas,
          0, --Devengamiento en M/N
          @lc_codwsh,
          1,
          @lc_estado,
          @lc_es_penal
      from dbo.sce_jcci p,
        dbo.sce_jcco q
      where p.codcct = @cur_codcct
      and   p.codpro = @cur_codpro
      and   p.codesp = @cur_codesp
      and   p.codofi = @cur_codofi
      and   p.codope = @cur_codope
      and   p.codcct = q.codcct
      and   p.codpro = q.codpro
      and   p.codesp = q.codesp
      and   p.codofi = q.codofi
      and   p.codope = q.codope
      and   (p.estado = 3 or p.estado = 4)
      and   ((datepart(yy,p.fecing) <= @anohoy and
      datepart(mm,p.fecing) <= @meshoy) or
      datepart(yy,p.fecing) < @anohoy)
      and   q.salvis > 0
      if @@error <> 0
	  begin
         rollback
		 return 8
	  end
   end
   close dev_jcci
   deallocate dev_jcci
--Fin Cambios
   commit tran
   return 0
END

' 
END

/****** Object:  StoredProcedure [dbo].[scejdoc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[scejdoc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[scejdoc_s01_MS]
	@codcct      CHAR(3),
	@codpro      CHAR(2),
	@codesp      CHAR(2),
	@codofi      CHAR(3),
	@codope      CHAR(5),
	@nrocor      NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	/*if exists(SELECT TOP 1 1 from sce_jdoc where
                codcct = @codcct and  
                codpro = @codpro and  
                codesp = @codesp and  
                codofi = @codofi and  
                codope = @codope and  
                nrocor = @nrocor)     
	*/
   select   fecing,codmem 
   from dbo.sce_jdoc 
   where
   codcct = @codcct and
   codpro = @codpro and
   codesp = @codesp and
   codofi = @codofi and
   codope = @codope and
   nrocor = @nrocor


	/*	else
		select 	fecing,codmem from rce_jdoc where 
			codcct = @codcct and 
			codpro = @codpro and 
			codesp = @codesp and 
			codofi = @codofi and 
			codope = @codope and 
			nrocor = @nrocor
	*/


   return
END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_aec_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_aec_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_aec_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
	 	SELECT 
		aec_aeccod, 
		ISNULL(CONVERT(VARCHAR(6),aec_aeccod) + '' - '' + aec_aecnom,'''') AS aec_aecnom 
	FROM 
		sgt_aec

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_aec_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_aec_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--select * from sgt_aec order by aec_aeccod asc
create procedure [dbo].[sgt_aec_s02_MS]
 as
    select aec_aeccod,aec_aecnom from sgt_aec
	
' 
END

/****** Object:  StoredProcedure [dbo].[sgt_clf_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_clf_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_clf_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
	select clf_clfcod,clf_clfdes 
	from sgt_clf

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_ejc_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_ejc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_ejc_s02_MS]
   @codigo int -- Codigo Oficina
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
	 select ejc_ejccod, convert(varchar(10),isnull(ejc_ejccod,0)) + '' - '' + ejc_ejcnom  as ejc_ejcnom 
	 from sgt_ejc
	where ejc_ejcofi = @codigo
	 order by ejc_ejccod

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_ejc_s03_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_ejc_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_ejc_s03_MS]
      @EJCOPIMP AS VARCHAR(2),
	   @EJCOPEXP AS VARCHAR(2),
	   @EJCNEGOC AS VARCHAR(2)	 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
      select ejc_ejcofi, ejc_ejccod, ejc_ejcrut, ejc_ejcnom, ejc_ejctpo 
	  from sgt_ejc
      where ejc_ejctpo =  @EJCOPIMP
			OR ejc_ejctpo = @EJCOPEXP
			OR ejc_ejctpo = @EJCNEGOC

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_ejc_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_ejc_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_ejc_s04_MS] 
	@codofi     SMALLINT
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
    SET NOCOUNT ON 
     
	 SELECT
		[ejc_ejccod],
		[ejc_ejcnom]
	FROM dbo.sgt_ejc
	WHERE ejc_ejcofi = @codofi
	ORDER BY ejc_ejccod
END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_loc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_loc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_loc_s01_MS] 
AS
BEGIN
   select   loc_loccod, loc_locnom, loc_locreg
   from dbo.sgt_loc

   return
END



' 
END

/****** Object:  StoredProcedure [dbo].[sgt_mnd_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_mnd_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_mnd_MS] 
AS
BEGIN
	SELECT mnd_mndnmc, mnd_mndina
	FROM dbo.sgt_mnd
END
' 
END

/****** Object:  StoredProcedure [dbo].[sgt_mnd_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_mnd_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_mnd_s02_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   SELECT   
		mnd_mndcod, 
		mnd_mndcbc, 
		mnd_mndnom,
		mnd_mndnmc, 
		mnd_mndswf 
   FROM dbo.sgt_mnd 

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_pai_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_pai_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_pai_s02_MS] 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   SELECT   pai_paicod, pai_painom, pai_paiala 
   FROM dbo.sgt_pai

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_pbc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_pbc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_pbc_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
	SELECT 
		pbc_pbccod,
		pbc_pbcdes 
	FROM dbo.sgt_pbc 


END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_suc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_suc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_suc_s01_MS]
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
    
   select   suc_succod, suc_sucnom 
   from dbo.sgt_suc 
   

END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_suc_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_suc_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_suc_s02_MS]
	@suc_succod SMALLINT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

   select   suc_sucnom
   from dbo.sgt_suc
   where suc_succod = @suc_succod
END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_suc_s04_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_suc_s04_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_suc_s04_MS] 
	@suc_succod NUMERIC(3,0) 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
   
   select   suc_succor,
		suc_sucdir,
		suc_sucfon,
		suc_sucfax
   from dbo.sgt_suc
   where suc_succod = @suc_succod
END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_vmc_s01_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_vmc_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_vmc_s01_MS]
	@vmccod		SMALLINT,
	@vmcfec  SMALLDATETIME   
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 

	declare @vmcfec_ini smalldatetime = DATEFROMPARTS(datepart(yy,@vmcfec), datepart(mm,@vmcfec), 1)
	declare @vmcfec_fin smalldatetime = dateadd(mm,1,@vmcfec_ini)

	select  
		vmc_vmcprc,
		vmc_vmctca
	from dbo.sgt_vmc 
	where
	   vmc_vmccod = @vmccod and
	   vmc_vmcfec >= @vmcfec_ini and
	   vmc_vmcfec <  @vmcfec_fin 
END

' 
END

/****** Object:  StoredProcedure [dbo].[sgt_vmd_s02_MS]    Script Date: 22-10-2015 10:33:31 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sgt_vmd_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sgt_vmd_s02_MS] 
	@vmdfec         datetime,
	@vmdcod         INT 
AS
BEGIN
/*	
Historial:
                         Migración desde Sybase (AKZIO)
      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
*/
   SET NOCOUNT ON 
	
   select   
		vmd_vmdmbc	,
		vmd_vmdmbv	,
		vmd_vmdmcc	,
		vmd_vmdmcv	,
		vmd_vmdprd	,
		vmd_vmdacd	,
		vmd_vmdobs
   from dbo.sgt_vmd
   where vmd_vmdcod = @vmdcod
   and vmd_vmdfec = @vmdfec
END

' 
END


--ACA TERMINA SCRIPT RELEASE 2

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_cdev_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_cdev_s02_MS] 
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_cdev_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_cdev_s02_MS]
	as
	select distinct periodo from sce_cdev'
END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_ctas_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_ctas_s01_MS] 
END



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_ctas_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_ctas_s01_MS] 
		@id_party       CHAR(12), 
		@borrado		bit,
		@extranjera		bit
	AS
	BEGIN
	-- This procedure was converted on Wed Apr 16 15:59:59 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   select   borrado,activace,cuenta,moneda 
	   from dbo.sce_ctas 
	   where
	   id_party = @id_party 	and
	   extranjera = @extranjera		and
	   borrado  = @borrado	
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_dad_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_dad_s01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_dad_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sce_dad_s01_MS] 
	@idparty 	CHAR(12)
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON

		Select borrado, direccion, id_dir, comuna, ciudad, cod_postal, pais, cod_pais, estado, telefono, fax, telex, envio_sce, cas_postal, cas_banco 
		From dbo.sce_dad
		where id_party = @idparty
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_datos_cuadratura_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_datos_cuadratura_s01_MS]
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_datos_cuadratura_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_datos_cuadratura_s01_MS]
	@tipoConsulta int
	AS

	--declare @tipoConsulta int 
	--set @tipoConsulta = 2 
	begin
		if @tipoConsulta = 1
		begin
			SELECT DISTINCT t_ano_mes from sce_datos_cuadratura WHERE t_ano_mes is not NULL ORDER BY 1
		end
		else
			SELECT DISTINCT t_ano_mes from sce_datos_cuadratura_r WHERE t_ano_mes is not NULL ORDER BY 1

	end'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_int_cdr_S01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_int_cdr_S01_MS] 
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_int_cdr_S01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_int_cdr_S01_MS]
	@tipoConsulta int

	as
	BEGIN
		--declare @tipoConsulta int set @tipoConsulta = 2

		if(@tipoConsulta = 1)
		begin
			SELECT convert(nvarchar(4),DATEPART(year,CRD_FECHA_INT)) +RIGHT(''0''+convert(nvarchar(2),DATEPART(mm,CRD_FECHA_INT)),2) AS periodo 
			FROM dbo.sce_int_cdr_crd with(nolock)
			group by DATEPART(year,CRD_FECHA_INT),DATEPART(mm,CRD_FECHA_INT) ORDER BY 1
		end
		else
			SELECT convert(nvarchar(4),DATEPART(year,DEV_FECHA_INT)) +RIGHT(''0''+convert(nvarchar(2),DATEPART(mm,DEV_FECHA_INT)),2) AS periodo 
			FROM dbo.sce_int_cdr_dev with(nolock)
			group by DATEPART(year,DEV_FECHA_INT),DATEPART(mm,DEV_FECHA_INT) ORDER BY 1
	END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_int_cdr_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_int_cdr_s02_MS]
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_int_cdr_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[pro_sce_int_cdr_s02_MS]
	@tipoConsulta int,
	@Anno int,
	@Mes int 

	as
	BEGIN

	--declare @Anno int  set @Anno = 2015
	--declare	@Mes int set @Mes = 01
	--declare @tipoConsulta int set @tipoConsulta = 1 

		if( @tipoConsulta = 1)
		begin
	
			SELECT  DISTINCT day( dbo.sce_int_cdr_crd.CRD_FECHA_INT) AS DIAS 
			FROM dbo.sce_int_cdr_crd 
			WHERE   YEAR( dbo.sce_int_cdr_crd.CRD_FECHA_INT) =  @Anno 
			AND     MONTH(dbo.sce_int_cdr_crd.CRD_FECHA_INT) =  @Mes 
			ORDER BY 1
		end
		else
			SELECT  DISTINCT day(dbo.sce_int_cdr_dev.DEV_FECHA_INT) AS DIAS 
			FROM dbo.sce_int_cdr_dev 
			WHERE   YEAR( dbo.sce_int_cdr_dev.DEV_FECHA_INT) =  @Anno
			AND     MONTH(dbo.sce_int_cdr_dev.DEV_FECHA_INT) =  @Mes 
			ORDER BY 1
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_rsa_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_rsa_s01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_rsa_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sce_rsa_s01_MS] 
	@idparty 	CHAR(12)
	AS
	BEGIN
	/*	
	Historial:
							 Migración desde Sybase (AKZIO)
		  2015-09-29   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)
	*/
		SET NOCOUNT ON

		Select borrado, razon_soci, id_nombre
		from dbo.sce_rsa
		where id_party = @idparty
	END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_cdev_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE sce_cdev_s01_MS 
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cdev_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_cdev_s01_MS]
		@periodo NUMERIC(6,0),
		@rut CHAR(10),
		@operacion CHAR(15),
		@moneda NUMERIC(1,0),
		@todos CHAR(1) 
	AS
	begin

	   if  @todos = ''S'' and @moneda = 9
		  select   operacion, numneg,moneda,mtovig ,tasbas,fecini,fecfin, numdia,tipcam,mtointer,
	 rut,cta_intxcob,cta_intganado ,mnd ,periodo from dbo.sce_cdev where periodo = @periodo

	   if  @todos = ''S'' and @moneda <> 9
		  select   operacion, numneg,moneda,mtovig ,tasbas,fecini,fecfin, numdia,tipcam,mtointer,
	rut,cta_intxcob,cta_intganado ,mnd ,periodo from dbo.sce_cdev
		  where periodo = @periodo and
		  mondev = @moneda


	   if @rut <> '''' and @moneda = 9
		  select   operacion, numneg,moneda,mtovig ,tasbas,fecini,fecfin, numdia,tipcam,mtointer,
		rut,cta_intxcob,cta_intganado ,mnd ,periodo from dbo.sce_cdev where periodo = @periodo
		  and rut = @rut

	   if @rut <> '''' and @moneda <> 9
		  select   operacion, numneg,moneda,mtovig ,tasbas,fecini,fecfin, numdia,tipcam,mtointer,
		rut,cta_intxcob,cta_intganado ,mnd ,periodo from dbo.sce_cdev where periodo = @periodo
		  and rut = @rut and mondev = @moneda

	   if @operacion <> '''' and @moneda = 9
		  select   operacion, numneg,moneda,mtovig ,tasbas,fecini,fecfin, numdia,tipcam,mtointer,
		rut,cta_intxcob,cta_intganado ,mnd ,periodo from dbo.sce_cdev
		  where periodo = @periodo and
		  operacion = @operacion

	   if @operacion <> '''' and @moneda <> 9
		  select   operacion, numneg,moneda,mtovig ,tasbas,fecini,fecfin, numdia,tipcam,mtointer,
		rut,cta_intxcob,cta_intganado ,mnd ,periodo from dbo.sce_cdev
		  where periodo = @periodo and
		  operacion = @operacion and
		  mondev = @moneda
	   return
	end'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_cuadra_inyecciones_ctacte_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_cuadra_inyecciones_ctacte_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_cuadra_inyecciones_ctacte_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_cuadra_inyecciones_ctacte_MS]
		@cencos		CHAR(3),
		@codusr     CHAR(2),
		@fecmov		datetime 
	AS
	BEGIN
	-- This procedure was converted on Wed Apr 16 15:59:59 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
	   select   codcct,
			codpro,
			codesp,
			codofi,
			codope,
			codneg,
			nrorpt,
			convert(CHAR(10),fecmov,103) as fecmov,
			cod_dh,
			nemcta,
			numcct,
			mtomcd,
			nemmon,
			estado
	   from    dbo.sce_mcd
	   where   codcct = @cencos
	   and     nemcta in(''CCE'',''CC$'')
	   and     enlinea = 1
	   and     fecmov = @fecmov
	   and     (rutais    = ''''
	   or rutais IS NULL)
	   and     codusr in(select  id_especia
		  from    dbo.sce_usr
		  where   cent_costo = @cencos
		  and     id_super   = @codusr)
	   order by nemcta
	END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_dev_cdr_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_dev_cdr_MS]
END



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_dev_cdr_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_dev_cdr_MS] 
	@AnnoMes NUMERIC(6,0),
    @FInicio VARCHAR(10),
    @FTermino VARCHAR(10),
	@RUT CHAR(10),
	@Operacion VARCHAR(9),
	@Moneda CHAR(3),
    @PlazoTO VARCHAR(5),
	@Todos CHAR(1),
	@TipoConsulta INT,
	@NumeroRegistros INT,
	@TipoRegistro VARCHAR(1) 
AS
BEGIN

   SET rowcount @NumeroRegistros

   IF @PlazoTO = ''''
      select   @PlazoTO = NULL

 
   IF @TipoConsulta = 1
   BEGIN
  
  	-- Todos(Rut, Operacion y PlazoTO) y Ambas Monedas
      IF @Todos = ''S'' AND @Moneda = ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_crd.CRD_TIPO_REG
				,dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_OFI_FIC_CONT
				,dbo.sce_int_cdr_crd.CRD_TO
				,dbo.sce_int_cdr_crd.CRD_PPP_EMB
				,dbo.sce_int_cdr_crd.CRD_NUM_DOC
				,dbo.sce_int_cdr_crd.CRD_SITUA_CRED
				,dbo.sce_int_cdr_crd.CRD_ACTIV_ECON_DEST
				,dbo.sce_int_cdr_crd.CRD_TIPO_GARANTIA
				,dbo.sce_int_cdr_crd.CRD_CLAS_RGO_CRED
				,dbo.sce_int_cdr_crd.CRD_OPER_REN
				,dbo.sce_int_cdr_crd.CRD_FEC_OTOR_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_APROB_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_CAMBIO_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_RENOV
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_VENC
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_EJEC
				,dbo.sce_int_cdr_crd.CRD_FEC_DETERIORO
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_VENC_FTRO
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_CAP
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_INT
				,dbo.sce_int_cdr_crd.CRD_FEC_REZAGO_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_IMPAGA_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_PENULT_TASA
				,dbo.sce_int_cdr_crd.CRD_MO
				,dbo.sce_int_cdr_crd.CRD_MC
				,dbo.sce_int_cdr_crd.CRD_MC_INT
				,[CRD_MTO_ORIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,12,4)
				,[CRD_MTO_REN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,12,4)
				,[CRD_CAPIT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,12,4)
				,[CRD_INT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,12,4)
				,[CRD_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,12,4)
				,[CRD_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,12,4)
				,[CRD_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,12,4)
				,[CRD_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,12,4)
				,[CRD_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,12,4)
				,[CRD_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,12,4)
				,[CRD_PRE_JUD_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,12,4)
				,[CRD_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,12,4)
				,dbo.sce_int_cdr_crd.CRD_VIG_MN
				,dbo.sce_int_cdr_crd.CRD_IXC_VENC_MN
				,dbo.sce_int_cdr_crd.CRD_PRE_JUD_MN
				,dbo.sce_int_cdr_crd.CRD_EJEC_MN
				,[CRD_VIG_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,12,4)
				,[CRD_INT_VENC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,12,4)
				,[CRD_PRE_JUD_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,12,4)
				,[CRD_EJEC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,12,4)
				,[CRD_IN_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,12,4)
				,[CRD_IN_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,12,4)
				,[CRD_IN_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,12,4)
				,[CRD_IN_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,12,4)
				,[CRD_IN_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,12,4)
				,[CRD_IP_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,12,4)
				,[CRD_IP_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,12,4)
				,[CRD_IP_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,12,4)
				,[CRD_IP_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,12,4)
				,[CRD_IP_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,12,4)
				,[CRD_IP_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,12,4)
				,[CRD_IP_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,12,4)
				,[CRD_IS_NOK_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,12,4)
				,[CRD_IS_NOK_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_PPP_ORIG
				,dbo.sce_int_cdr_crd.CRD_PPP_RDUAL
				,dbo.sce_int_cdr_crd.CRD_CUOT_CAPIT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_INT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_ATRA
				,dbo.sce_int_cdr_crd.CRD_PROX_CUOT
				,dbo.sce_int_cdr_crd.CRD_TIP_INT
				,dbo.sce_int_cdr_crd.CRD_MTDO_CALC_INT
				,dbo.sce_int_cdr_crd.CRD_EXPRE_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_BASE
				,[CRD_PTO_SOB_BASE]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,4,4)
				,[CRD_TASA_REAL_APLIC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,4,4)
				,[CRD_TASA_EQUI_AA]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,4,4)
				,dbo.sce_int_cdr_crd.CRD_COD_VARI_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_DOC_SUST
				,dbo.sce_int_cdr_crd.CRD_OFI_REA_CONT
				,dbo.sce_int_cdr_crd.CRD_TIPO_CRE
				,dbo.sce_int_cdr_crd.CRD_EST_CRE
				,dbo.sce_int_cdr_crd.CRD_CAUSA_EXTINCION_CRE
				,dbo.sce_int_cdr_crd.CRD_PZO_CONTB_CRE
				,dbo.sce_int_cdr_crd.CRD_IND_DIV_PRORROGADOS
				,[CRD_PARID_CAPIT]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,1,5)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,6,10)
				,[CRD_MTO_VENC_PAG_MM]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,12,4)
				,[CRD_ULT_MTO_PAG_CAP_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,12,4)
				,[CRD_ULT_MTO_PAG_INT_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,12,4)
				,[CRD_PROM_CAP_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,12,4)
				,[CRD_PROM_CAP_MOR_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,12,4)
				,[CRD_PROM_CAP_VEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,12,4)
				,[CRD_PROM_INTN_MORVEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,12,4)
				,[CRD_INTNOR_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,12,4)
				,[CRD_INTPEN_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_REA_RECIB_MES
         FROM    dbo.sce_int_cdr_crd
         WHERE   dbo.sce_int_cdr_crd.CRD_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_crd.CRD_TIPO_INT = @TipoRegistro
         AND     ((dbo.sce_int_cdr_crd.CRD_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END  

  	-- Todos(Rut, Operacion y PlazoTO) y Moneda en particular
      IF @Todos = ''S'' and @Moneda <> ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_crd.CRD_TIPO_REG
				,dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_OFI_FIC_CONT
				,dbo.sce_int_cdr_crd.CRD_TO
				,dbo.sce_int_cdr_crd.CRD_PPP_EMB
				,dbo.sce_int_cdr_crd.CRD_NUM_DOC
				,dbo.sce_int_cdr_crd.CRD_SITUA_CRED
				,dbo.sce_int_cdr_crd.CRD_ACTIV_ECON_DEST
				,dbo.sce_int_cdr_crd.CRD_TIPO_GARANTIA
				,dbo.sce_int_cdr_crd.CRD_CLAS_RGO_CRED
				,dbo.sce_int_cdr_crd.CRD_OPER_REN
				,dbo.sce_int_cdr_crd.CRD_FEC_OTOR_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_APROB_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_CAMBIO_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_RENOV
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_VENC
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_EJEC
				,dbo.sce_int_cdr_crd.CRD_FEC_DETERIORO
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_VENC_FTRO
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_CAP
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_INT
				,dbo.sce_int_cdr_crd.CRD_FEC_REZAGO_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_IMPAGA_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_PENULT_TASA
				,dbo.sce_int_cdr_crd.CRD_MO
				,dbo.sce_int_cdr_crd.CRD_MC
				,dbo.sce_int_cdr_crd.CRD_MC_INT
				,[CRD_MTO_ORIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,12,4)
				,[CRD_MTO_REN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,12,4)
				,[CRD_CAPIT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,12,4)
				,[CRD_INT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,12,4)
				,[CRD_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,12,4)
				,[CRD_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,12,4)
				,[CRD_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,12,4)
				,[CRD_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,12,4)
				,[CRD_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,12,4)
				,[CRD_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,12,4)
				,[CRD_PRE_JUD_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,12,4)
				,[CRD_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,12,4)
				,dbo.sce_int_cdr_crd.CRD_VIG_MN
				,dbo.sce_int_cdr_crd.CRD_IXC_VENC_MN
				,dbo.sce_int_cdr_crd.CRD_PRE_JUD_MN
				,dbo.sce_int_cdr_crd.CRD_EJEC_MN
				,[CRD_VIG_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,12,4)
				,[CRD_INT_VENC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,12,4)
				,[CRD_PRE_JUD_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,12,4)
				,[CRD_EJEC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,12,4)
				,[CRD_IN_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,12,4)
				,[CRD_IN_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,12,4)
				,[CRD_IN_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,12,4)
				,[CRD_IN_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,12,4)
				,[CRD_IN_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,12,4)
				,[CRD_IP_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,12,4)
				,[CRD_IP_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,12,4)
				,[CRD_IP_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,12,4)
				,[CRD_IP_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,12,4)
				,[CRD_IP_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,12,4)
				,[CRD_IP_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,12,4)
				,[CRD_IP_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,12,4)
				,[CRD_IS_NOK_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,12,4)
				,[CRD_IS_NOK_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_PPP_ORIG
				,dbo.sce_int_cdr_crd.CRD_PPP_RDUAL
				,dbo.sce_int_cdr_crd.CRD_CUOT_CAPIT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_INT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_ATRA
				,dbo.sce_int_cdr_crd.CRD_PROX_CUOT
				,dbo.sce_int_cdr_crd.CRD_TIP_INT
				,dbo.sce_int_cdr_crd.CRD_MTDO_CALC_INT
				,dbo.sce_int_cdr_crd.CRD_EXPRE_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_BASE
				,[CRD_PTO_SOB_BASE]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,4,4)
				,[CRD_TASA_REAL_APLIC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,4,4)
				,[CRD_TASA_EQUI_AA]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,4,4)
				,dbo.sce_int_cdr_crd.CRD_COD_VARI_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_DOC_SUST
				,dbo.sce_int_cdr_crd.CRD_OFI_REA_CONT
				,dbo.sce_int_cdr_crd.CRD_TIPO_CRE
				,dbo.sce_int_cdr_crd.CRD_EST_CRE
				,dbo.sce_int_cdr_crd.CRD_CAUSA_EXTINCION_CRE
				,dbo.sce_int_cdr_crd.CRD_PZO_CONTB_CRE
				,dbo.sce_int_cdr_crd.CRD_IND_DIV_PRORROGADOS
				,[CRD_PARID_CAPIT]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,1,5)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,6,10)
				,[CRD_MTO_VENC_PAG_MM]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,12,4)
				,[CRD_ULT_MTO_PAG_CAP_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,12,4)
				,[CRD_ULT_MTO_PAG_INT_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,12,4)
				,[CRD_PROM_CAP_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,12,4)
				,[CRD_PROM_CAP_MOR_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,12,4)
				,[CRD_PROM_CAP_VEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,12,4)
				,[CRD_PROM_INTN_MORVEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,12,4)
				,[CRD_INTNOR_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,12,4)
				,[CRD_INTPEN_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_REA_RECIB_MES
         FROM    dbo.sce_int_cdr_crd
         WHERE   dbo.sce_int_cdr_crd.CRD_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_crd.CRD_MO = @Moneda
         AND     dbo.sce_int_cdr_crd.CRD_TIPO_INT = @TipoRegistro
         AND     ((dbo.sce_int_cdr_crd.CRD_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END  

    -- Rut y Ambas Monedas
      IF @RUT <> '''' and @Moneda = ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_crd.CRD_TIPO_REG
				,dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_OFI_FIC_CONT
				,dbo.sce_int_cdr_crd.CRD_TO
				,dbo.sce_int_cdr_crd.CRD_PPP_EMB
				,dbo.sce_int_cdr_crd.CRD_NUM_DOC
				,dbo.sce_int_cdr_crd.CRD_SITUA_CRED
				,dbo.sce_int_cdr_crd.CRD_ACTIV_ECON_DEST
				,dbo.sce_int_cdr_crd.CRD_TIPO_GARANTIA
				,dbo.sce_int_cdr_crd.CRD_CLAS_RGO_CRED
				,dbo.sce_int_cdr_crd.CRD_OPER_REN
				,dbo.sce_int_cdr_crd.CRD_FEC_OTOR_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_APROB_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_CAMBIO_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_RENOV
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_VENC
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_EJEC
				,dbo.sce_int_cdr_crd.CRD_FEC_DETERIORO
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_VENC_FTRO
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_CAP
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_INT
				,dbo.sce_int_cdr_crd.CRD_FEC_REZAGO_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_IMPAGA_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_PENULT_TASA
				,dbo.sce_int_cdr_crd.CRD_MO
				,dbo.sce_int_cdr_crd.CRD_MC
				,dbo.sce_int_cdr_crd.CRD_MC_INT
				,[CRD_MTO_ORIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,12,4)
				,[CRD_MTO_REN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,12,4)
				,[CRD_CAPIT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,12,4)
				,[CRD_INT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,12,4)
				,[CRD_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,12,4)
				,[CRD_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,12,4)
				,[CRD_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,12,4)
				,[CRD_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,12,4)
				,[CRD_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,12,4)
				,[CRD_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,12,4)
				,[CRD_PRE_JUD_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,12,4)
				,[CRD_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,12,4)
				,dbo.sce_int_cdr_crd.CRD_VIG_MN
				,dbo.sce_int_cdr_crd.CRD_IXC_VENC_MN
				,dbo.sce_int_cdr_crd.CRD_PRE_JUD_MN
				,dbo.sce_int_cdr_crd.CRD_EJEC_MN
				,[CRD_VIG_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,12,4)
				,[CRD_INT_VENC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,12,4)
				,[CRD_PRE_JUD_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,12,4)
				,[CRD_EJEC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,12,4)
				,[CRD_IN_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,12,4)
				,[CRD_IN_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,12,4)
				,[CRD_IN_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,12,4)
				,[CRD_IN_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,12,4)
				,[CRD_IN_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,12,4)
				,[CRD_IP_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,12,4)
				,[CRD_IP_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,12,4)
				,[CRD_IP_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,12,4)
				,[CRD_IP_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,12,4)
				,[CRD_IP_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,12,4)
				,[CRD_IP_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,12,4)
				,[CRD_IP_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,12,4)
				,[CRD_IS_NOK_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,12,4)
				,[CRD_IS_NOK_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_PPP_ORIG
				,dbo.sce_int_cdr_crd.CRD_PPP_RDUAL
				,dbo.sce_int_cdr_crd.CRD_CUOT_CAPIT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_INT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_ATRA
				,dbo.sce_int_cdr_crd.CRD_PROX_CUOT
				,dbo.sce_int_cdr_crd.CRD_TIP_INT
				,dbo.sce_int_cdr_crd.CRD_MTDO_CALC_INT
				,dbo.sce_int_cdr_crd.CRD_EXPRE_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_BASE
				,[CRD_PTO_SOB_BASE]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,4,4)
				,[CRD_TASA_REAL_APLIC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,4,4)
				,[CRD_TASA_EQUI_AA]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,4,4)
				,dbo.sce_int_cdr_crd.CRD_COD_VARI_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_DOC_SUST
				,dbo.sce_int_cdr_crd.CRD_OFI_REA_CONT
				,dbo.sce_int_cdr_crd.CRD_TIPO_CRE
				,dbo.sce_int_cdr_crd.CRD_EST_CRE
				,dbo.sce_int_cdr_crd.CRD_CAUSA_EXTINCION_CRE
				,dbo.sce_int_cdr_crd.CRD_PZO_CONTB_CRE
				,dbo.sce_int_cdr_crd.CRD_IND_DIV_PRORROGADOS
				,[CRD_PARID_CAPIT]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,1,5)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,6,10)
				,[CRD_MTO_VENC_PAG_MM]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,12,4)
				,[CRD_ULT_MTO_PAG_CAP_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,12,4)
				,[CRD_ULT_MTO_PAG_INT_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,12,4)
				,[CRD_PROM_CAP_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,12,4)
				,[CRD_PROM_CAP_MOR_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,12,4)
				,[CRD_PROM_CAP_VEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,12,4)
				,[CRD_PROM_INTN_MORVEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,12,4)
				,[CRD_INTNOR_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,12,4)
				,[CRD_INTPEN_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_REA_RECIB_MES
         FROM    dbo.sce_int_cdr_crd
         WHERE   dbo.sce_int_cdr_crd.CRD_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR+dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR = @RUT
         AND     dbo.sce_int_cdr_crd.CRD_TIPO_INT = @TipoRegistro
         AND     ((dbo.sce_int_cdr_crd.CRD_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  
  
  -- Rut y Moneda
      IF @RUT <> '''' and @Moneda <> ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_crd.CRD_TIPO_REG
				,dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_OFI_FIC_CONT
				,dbo.sce_int_cdr_crd.CRD_TO
				,dbo.sce_int_cdr_crd.CRD_PPP_EMB
				,dbo.sce_int_cdr_crd.CRD_NUM_DOC
				,dbo.sce_int_cdr_crd.CRD_SITUA_CRED
				,dbo.sce_int_cdr_crd.CRD_ACTIV_ECON_DEST
				,dbo.sce_int_cdr_crd.CRD_TIPO_GARANTIA
				,dbo.sce_int_cdr_crd.CRD_CLAS_RGO_CRED
				,dbo.sce_int_cdr_crd.CRD_OPER_REN
				,dbo.sce_int_cdr_crd.CRD_FEC_OTOR_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_APROB_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_CAMBIO_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_RENOV
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_VENC
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_EJEC
				,dbo.sce_int_cdr_crd.CRD_FEC_DETERIORO
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_VENC_FTRO
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_CAP
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_INT
				,dbo.sce_int_cdr_crd.CRD_FEC_REZAGO_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_IMPAGA_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_PENULT_TASA
				,dbo.sce_int_cdr_crd.CRD_MO
				,dbo.sce_int_cdr_crd.CRD_MC
				,dbo.sce_int_cdr_crd.CRD_MC_INT
				,[CRD_MTO_ORIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,12,4)
				,[CRD_MTO_REN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,12,4)
				,[CRD_CAPIT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,12,4)
				,[CRD_INT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,12,4)
				,[CRD_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,12,4)
				,[CRD_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,12,4)
				,[CRD_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,12,4)
				,[CRD_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,12,4)
				,[CRD_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,12,4)
				,[CRD_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,12,4)
				,[CRD_PRE_JUD_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,12,4)
				,[CRD_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,12,4)
				,dbo.sce_int_cdr_crd.CRD_VIG_MN
				,dbo.sce_int_cdr_crd.CRD_IXC_VENC_MN
				,dbo.sce_int_cdr_crd.CRD_PRE_JUD_MN
				,dbo.sce_int_cdr_crd.CRD_EJEC_MN
				,[CRD_VIG_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,12,4)
				,[CRD_INT_VENC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,12,4)
				,[CRD_PRE_JUD_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,12,4)
				,[CRD_EJEC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,12,4)
				,[CRD_IN_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,12,4)
				,[CRD_IN_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,12,4)
				,[CRD_IN_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,12,4)
				,[CRD_IN_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,12,4)
				,[CRD_IN_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,12,4)
				,[CRD_IP_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,12,4)
				,[CRD_IP_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,12,4)
				,[CRD_IP_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,12,4)
				,[CRD_IP_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,12,4)
				,[CRD_IP_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,12,4)
				,[CRD_IP_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,12,4)
				,[CRD_IP_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,12,4)
				,[CRD_IS_NOK_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,12,4)
				,[CRD_IS_NOK_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_PPP_ORIG
				,dbo.sce_int_cdr_crd.CRD_PPP_RDUAL
				,dbo.sce_int_cdr_crd.CRD_CUOT_CAPIT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_INT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_ATRA
				,dbo.sce_int_cdr_crd.CRD_PROX_CUOT
				,dbo.sce_int_cdr_crd.CRD_TIP_INT
				,dbo.sce_int_cdr_crd.CRD_MTDO_CALC_INT
				,dbo.sce_int_cdr_crd.CRD_EXPRE_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_BASE
				,[CRD_PTO_SOB_BASE]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,4,4)
				,[CRD_TASA_REAL_APLIC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,4,4)
				,[CRD_TASA_EQUI_AA]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,4,4)
				,dbo.sce_int_cdr_crd.CRD_COD_VARI_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_DOC_SUST
				,dbo.sce_int_cdr_crd.CRD_OFI_REA_CONT
				,dbo.sce_int_cdr_crd.CRD_TIPO_CRE
				,dbo.sce_int_cdr_crd.CRD_EST_CRE
				,dbo.sce_int_cdr_crd.CRD_CAUSA_EXTINCION_CRE
				,dbo.sce_int_cdr_crd.CRD_PZO_CONTB_CRE
				,dbo.sce_int_cdr_crd.CRD_IND_DIV_PRORROGADOS
				,[CRD_PARID_CAPIT]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,1,5)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,6,10)
				,[CRD_MTO_VENC_PAG_MM]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,12,4)
				,[CRD_ULT_MTO_PAG_CAP_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,12,4)
				,[CRD_ULT_MTO_PAG_INT_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,12,4)
				,[CRD_PROM_CAP_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,12,4)
				,[CRD_PROM_CAP_MOR_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,12,4)
				,[CRD_PROM_CAP_VEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,12,4)
				,[CRD_PROM_INTN_MORVEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,12,4)
				,[CRD_INTNOR_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,12,4)
				,[CRD_INTPEN_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_REA_RECIB_MES
         FROM    dbo.sce_int_cdr_crd
         WHERE   dbo.sce_int_cdr_crd.CRD_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR+dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR = @RUT
         AND     dbo.sce_int_cdr_crd.CRD_MO = @Moneda
         AND     dbo.sce_int_cdr_crd.CRD_TIPO_INT = @TipoRegistro
         AND     ((dbo.sce_int_cdr_crd.CRD_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  
  
    -- Operación y Ambas Monedas
      IF @Operacion <> '''' and @Moneda = ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_crd.CRD_TIPO_REG
				,dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_OFI_FIC_CONT
				,dbo.sce_int_cdr_crd.CRD_TO
				,dbo.sce_int_cdr_crd.CRD_PPP_EMB
				,dbo.sce_int_cdr_crd.CRD_NUM_DOC
				,dbo.sce_int_cdr_crd.CRD_SITUA_CRED
				,dbo.sce_int_cdr_crd.CRD_ACTIV_ECON_DEST
				,dbo.sce_int_cdr_crd.CRD_TIPO_GARANTIA
				,dbo.sce_int_cdr_crd.CRD_CLAS_RGO_CRED
				,dbo.sce_int_cdr_crd.CRD_OPER_REN
				,dbo.sce_int_cdr_crd.CRD_FEC_OTOR_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_APROB_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_CAMBIO_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_RENOV
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_VENC
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_EJEC
				,dbo.sce_int_cdr_crd.CRD_FEC_DETERIORO
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_VENC_FTRO
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_CAP
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_INT
				,dbo.sce_int_cdr_crd.CRD_FEC_REZAGO_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_IMPAGA_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_PENULT_TASA
				,dbo.sce_int_cdr_crd.CRD_MO
				,dbo.sce_int_cdr_crd.CRD_MC
				,dbo.sce_int_cdr_crd.CRD_MC_INT
				,[CRD_MTO_ORIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,12,4)
				,[CRD_MTO_REN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,12,4)
				,[CRD_CAPIT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,12,4)
				,[CRD_INT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,12,4)
				,[CRD_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,12,4)
				,[CRD_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,12,4)
				,[CRD_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,12,4)
				,[CRD_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,12,4)
				,[CRD_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,12,4)
				,[CRD_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,12,4)
				,[CRD_PRE_JUD_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,12,4)
				,[CRD_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,12,4)
				,dbo.sce_int_cdr_crd.CRD_VIG_MN
				,dbo.sce_int_cdr_crd.CRD_IXC_VENC_MN
				,dbo.sce_int_cdr_crd.CRD_PRE_JUD_MN
				,dbo.sce_int_cdr_crd.CRD_EJEC_MN
				,[CRD_VIG_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,12,4)
				,[CRD_INT_VENC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,12,4)
				,[CRD_PRE_JUD_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,12,4)
				,[CRD_EJEC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,12,4)
				,[CRD_IN_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,12,4)
				,[CRD_IN_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,12,4)
				,[CRD_IN_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,12,4)
				,[CRD_IN_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,12,4)
				,[CRD_IN_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,12,4)
				,[CRD_IP_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,12,4)
				,[CRD_IP_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,12,4)
				,[CRD_IP_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,12,4)
				,[CRD_IP_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,12,4)
				,[CRD_IP_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,12,4)
				,[CRD_IP_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,12,4)
				,[CRD_IP_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,12,4)
				,[CRD_IS_NOK_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,12,4)
				,[CRD_IS_NOK_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_PPP_ORIG
				,dbo.sce_int_cdr_crd.CRD_PPP_RDUAL
				,dbo.sce_int_cdr_crd.CRD_CUOT_CAPIT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_INT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_ATRA
				,dbo.sce_int_cdr_crd.CRD_PROX_CUOT
				,dbo.sce_int_cdr_crd.CRD_TIP_INT
				,dbo.sce_int_cdr_crd.CRD_MTDO_CALC_INT
				,dbo.sce_int_cdr_crd.CRD_EXPRE_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_BASE
				,[CRD_PTO_SOB_BASE]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,4,4)
				,[CRD_TASA_REAL_APLIC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,4,4)
				,[CRD_TASA_EQUI_AA]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,4,4)
				,dbo.sce_int_cdr_crd.CRD_COD_VARI_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_DOC_SUST
				,dbo.sce_int_cdr_crd.CRD_OFI_REA_CONT
				,dbo.sce_int_cdr_crd.CRD_TIPO_CRE
				,dbo.sce_int_cdr_crd.CRD_EST_CRE
				,dbo.sce_int_cdr_crd.CRD_CAUSA_EXTINCION_CRE
				,dbo.sce_int_cdr_crd.CRD_PZO_CONTB_CRE
				,dbo.sce_int_cdr_crd.CRD_IND_DIV_PRORROGADOS
				,[CRD_PARID_CAPIT]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,1,5)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,6,10)
				,[CRD_MTO_VENC_PAG_MM]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,12,4)
				,[CRD_ULT_MTO_PAG_CAP_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,12,4)
				,[CRD_ULT_MTO_PAG_INT_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,12,4)
				,[CRD_PROM_CAP_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,12,4)
				,[CRD_PROM_CAP_MOR_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,12,4)
				,[CRD_PROM_CAP_VEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,12,4)
				,[CRD_PROM_INTN_MORVEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,12,4)
				,[CRD_INTNOR_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,12,4)
				,[CRD_INTPEN_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_REA_RECIB_MES
         FROM    dbo.sce_int_cdr_crd
         WHERE   dbo.sce_int_cdr_crd.CRD_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_crd.CRD_NUM_DOC = @Operacion
         AND     dbo.sce_int_cdr_crd.CRD_TIPO_INT = @TipoRegistro
         AND     ((dbo.sce_int_cdr_crd.CRD_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  
  
     -- Operación y Moneda
      IF @Operacion <> '''' and @Moneda <> ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_crd.CRD_TIPO_REG
				,dbo.sce_int_cdr_crd.CRD_CPO_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_DV_RUT_DDOR_DIR
				,dbo.sce_int_cdr_crd.CRD_OFI_FIC_CONT
				,dbo.sce_int_cdr_crd.CRD_TO
				,dbo.sce_int_cdr_crd.CRD_PPP_EMB
				,dbo.sce_int_cdr_crd.CRD_NUM_DOC
				,dbo.sce_int_cdr_crd.CRD_SITUA_CRED
				,dbo.sce_int_cdr_crd.CRD_ACTIV_ECON_DEST
				,dbo.sce_int_cdr_crd.CRD_TIPO_GARANTIA
				,dbo.sce_int_cdr_crd.CRD_CLAS_RGO_CRED
				,dbo.sce_int_cdr_crd.CRD_OPER_REN
				,dbo.sce_int_cdr_crd.CRD_FEC_OTOR_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_APROB_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_CRE
				,dbo.sce_int_cdr_crd.CRD_FEC_EXTIN_LINEA
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_CAMBIO_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_RENOV
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_VENC
				,dbo.sce_int_cdr_crd.CRD_FEC_PASO_EJEC
				,dbo.sce_int_cdr_crd.CRD_FEC_DETERIORO
				,dbo.sce_int_cdr_crd.CRD_FEC_PROX_VENC_FTRO
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_CAP
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_PAG_INT
				,dbo.sce_int_cdr_crd.CRD_FEC_REZAGO_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_IMPAGA_MAS_ANTIG
				,dbo.sce_int_cdr_crd.CRD_FEC_ULT_TASA
				,dbo.sce_int_cdr_crd.CRD_FEC_PENULT_TASA
				,dbo.sce_int_cdr_crd.CRD_MO
				,dbo.sce_int_cdr_crd.CRD_MC
				,dbo.sce_int_cdr_crd.CRD_MC_INT
				,[CRD_MTO_ORIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_ORIG_MO,12,4)
				,[CRD_MTO_REN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_REN_MO,12,4)
				,[CRD_CAPIT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_CAPIT_PROX_VENC_MO,12,4)
				,[CRD_INT_PROX_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_PROX_VENC_MO,12,4)
				,[CRD_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_MO,12,4)
				,[CRD_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_H29_MO,12,4)
				,[CRD_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M30_H59_MO,12,4)
				,[CRD_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_M60_H89_MO,12,4)
				,[CRD_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MORA_D90_MO,12,4)
				,[CRD_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IXC_VENC_MO,12,4)
				,[CRD_PRE_JUD_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_MO,12,4)
				,[CRD_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_MO,12,4)
				,dbo.sce_int_cdr_crd.CRD_VIG_MN
				,dbo.sce_int_cdr_crd.CRD_IXC_VENC_MN
				,dbo.sce_int_cdr_crd.CRD_PRE_JUD_MN
				,dbo.sce_int_cdr_crd.CRD_EJEC_MN
				,[CRD_VIG_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_VIG_ME,12,4)
				,[CRD_INT_VENC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INT_VENC_ME,12,4)
				,[CRD_PRE_JUD_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PRE_JUD_ME,12,4)
				,[CRD_EJEC_ME]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_EJEC_ME,12,4)
				,[CRD_IN_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_VIG_MO,12,4)
				,[CRD_IN_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_H29_MO,12,4)
				,[CRD_IN_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M30_H59_MO,12,4)
				,[CRD_IN_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_M60_H89_MO,12,4)
				,[CRD_IN_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IN_MORA_D90_MO,12,4)
				,[CRD_IP_MORA_H29_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_H29_MO,12,4)
				,[CRD_IP_MORA_M30_H59_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M30_H59_MO,12,4)
				,[CRD_IP_MORA_M60_H89_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_M60_H89_MO,12,4)
				,[CRD_IP_MORA_D90_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_MORA_D90_MO,12,4)
				,[CRD_IP_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_IXC_VENC_MO,12,4)
				,[CRD_IP_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_PRE_JUDIC_MO,12,4)
				,[CRD_IP_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IP_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MO,12,4)
				,[CRD_IS_NOK_PRE_JUDIC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_PRE_JUDIC_MO,12,4)
				,[CRD_IS_NOK_EJEC_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_EJEC_MO,12,4)
				,[CRD_IS_NOK_IXC_VENC_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_IS_NOK_IXC_VENC_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_PPP_ORIG
				,dbo.sce_int_cdr_crd.CRD_PPP_RDUAL
				,dbo.sce_int_cdr_crd.CRD_CUOT_CAPIT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_INT_X_VENC
				,dbo.sce_int_cdr_crd.CRD_CUOT_ATRA
				,dbo.sce_int_cdr_crd.CRD_PROX_CUOT
				,dbo.sce_int_cdr_crd.CRD_TIP_INT
				,dbo.sce_int_cdr_crd.CRD_MTDO_CALC_INT
				,dbo.sce_int_cdr_crd.CRD_EXPRE_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_BASE
				,[CRD_PTO_SOB_BASE]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PTO_SOB_BASE,4,4)
				,[CRD_TASA_REAL_APLIC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_REAL_APLIC,4,4)
				,[CRD_TASA_EQUI_AA]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,1,3)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_TASA_EQUI_AA,4,4)
				,dbo.sce_int_cdr_crd.CRD_COD_VARI_TASA
				,dbo.sce_int_cdr_crd.CRD_TIP_DOC_SUST
				,dbo.sce_int_cdr_crd.CRD_OFI_REA_CONT
				,dbo.sce_int_cdr_crd.CRD_TIPO_CRE
				,dbo.sce_int_cdr_crd.CRD_EST_CRE
				,dbo.sce_int_cdr_crd.CRD_CAUSA_EXTINCION_CRE
				,dbo.sce_int_cdr_crd.CRD_PZO_CONTB_CRE
				,dbo.sce_int_cdr_crd.CRD_IND_DIV_PRORROGADOS
				,[CRD_PARID_CAPIT]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,1,5)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PARID_CAPIT,6,10)
				,[CRD_MTO_VENC_PAG_MM]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_MTO_VENC_PAG_MM,12,4)
				,[CRD_ULT_MTO_PAG_CAP_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_CAP_MO,12,4)
				,[CRD_ULT_MTO_PAG_INT_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_ULT_MTO_PAG_INT_MO,12,4)
				,[CRD_PROM_CAP_VIG_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VIG_MO,12,4)
				,[CRD_PROM_CAP_MOR_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_MOR_MO,12,4)
				,[CRD_PROM_CAP_VEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_CAP_VEN_MO,12,4)
				,[CRD_PROM_INTN_MORVEN_MO]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_PROM_INTN_MORVEN_MO,12,4)
				,[CRD_INTNOR_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTNOR_RECIB_MES_MC,12,4)
				,[CRD_INTPEN_RECIB_MES_MC]=SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,1,11)+''.''+SUBSTRING(dbo.sce_int_cdr_crd.CRD_INTPEN_RECIB_MES_MC,12,4)
				,dbo.sce_int_cdr_crd.CRD_REA_RECIB_MES
         FROM    dbo.sce_int_cdr_crd
         WHERE   dbo.sce_int_cdr_crd.CRD_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_crd.CRD_NUM_DOC = @Operacion
         AND     dbo.sce_int_cdr_crd.CRD_MO = @Moneda
         AND     dbo.sce_int_cdr_crd.CRD_TIPO_INT = @TipoRegistro
         AND     ((dbo.sce_int_cdr_crd.CRD_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
   END
    
   IF @TipoConsulta = 2
   BEGIN
  
 	
  	-- Todos(Rut, Operacion y PlazoTO) y Ambas Monedas
      IF @Todos = ''S'' AND @Moneda = ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_dev.DEV_TIPO_REG
			,dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_OFI_FIC_CONT
			,dbo.sce_int_cdr_dev.DEV_TO
			,dbo.sce_int_cdr_dev.DEV_PPP_EMB
			,dbo.sce_int_cdr_dev.DEV_NUM_DOC
			,[DEV_INT_VIG_MO]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,14,2)
			,[DEV_INT_MORA_H29_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,14,2)
			,[DEV_INT_MORA_M30_H59_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,14,2)
			,[DEV_INT_MORA_M60_H89_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,14,2)
			,[DEV_INT_MORA_D90_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,14,2)
			,[DEV_INT_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,14,2)
			,[DEV_INT_DIF_PCIO_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,14,2)
			,[DEV_INT_VIG_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,14,2)
			,[DEV_INT_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,14,2)
			,[DEV_INT_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,14,2)
			,[DEV_INT_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,14,2)
			,[DEV_INT_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,14,2)
			,[DEV_INT_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,14,2)
			,[DEV_INT_DIF_PCIO_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,14,2)
			,[DEV_IS_CR_VIG_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,14,2)
			,[DEV_IS_CR_MORA_H29_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,14,2)
			,[DEV_IS_CR_MORA_D90_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,14,2)
			,[DEV_IS_CR_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,14,2)
			,[DEV_IS_CR_VIG_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,14,2)
			,[DEV_IS_CR_MORA_H29_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,14,2)
			,[DEV_IS_CR_MORA_D90_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,14,2)
			,[DEV_IS_CR_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,14,2)
			,[DEV_ISN_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_H29_VIG_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISN_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,14,2)
			,[DEV_ISP_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,14,2)
			,[DEV_ISP_CART_DET_MR_H29_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISP_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,14,2)
			,[DEV_IP_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,14,2)
			,[DEV_IP_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,14,2)
			,[DEV_IP_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,14,2)
			,[DEV_IP_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,14,2)
			,[DEV_IP_VENC_COB_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,14,2)
			,[DEV_IP_PRE_JUDIC_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,14,2)
			,[DEV_IP_EJEC_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,14,2)
			,dbo.sce_int_cdr_dev.DEV_REAJ_VIG_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_D90_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIV_PRORR_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIF_PCIO_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET__VIG_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_D90_MN
			,dbo.sce_int_cdr_dev.DEV_RS_DIV_PRORR_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_VIG_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_H29_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M30_H59_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M60_H89_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_D90_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_INT_COB_VENC
			,dbo.sce_int_cdr_dev.DEV_RS_CM_PRE_JUDIC_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_EN_EJEC_MO
			,dbo.sce_int_cdr_dev.DEV_PPP_CONTAB_CRE
			,dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP
         FROM    dbo.sce_int_cdr_dev
         WHERE   dbo.sce_int_cdr_dev.DEV_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     ((dbo.sce_int_cdr_dev.DEV_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  	-- Todos(Rut, Operacion y PlazoTO) y Moneda en particular
      IF @Todos = ''S'' and @Moneda <> ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_dev.DEV_TIPO_REG
			,dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_OFI_FIC_CONT
			,dbo.sce_int_cdr_dev.DEV_TO
			,dbo.sce_int_cdr_dev.DEV_PPP_EMB
			,dbo.sce_int_cdr_dev.DEV_NUM_DOC
			,[DEV_INT_VIG_MO]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,14,2)
			,[DEV_INT_MORA_H29_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,14,2)
			,[DEV_INT_MORA_M30_H59_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,14,2)
			,[DEV_INT_MORA_M60_H89_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,14,2)
			,[DEV_INT_MORA_D90_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,14,2)
			,[DEV_INT_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,14,2)
			,[DEV_INT_DIF_PCIO_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,14,2)
			,[DEV_INT_VIG_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,14,2)
			,[DEV_INT_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,14,2)
			,[DEV_INT_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,14,2)
			,[DEV_INT_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,14,2)
			,[DEV_INT_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,14,2)
			,[DEV_INT_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,14,2)
			,[DEV_INT_DIF_PCIO_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,14,2)
			,[DEV_IS_CR_VIG_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,14,2)
			,[DEV_IS_CR_MORA_H29_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,14,2)
			,[DEV_IS_CR_MORA_D90_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,14,2)
			,[DEV_IS_CR_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,14,2)
			,[DEV_IS_CR_VIG_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,14,2)
			,[DEV_IS_CR_MORA_H29_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,14,2)
			,[DEV_IS_CR_MORA_D90_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,14,2)
			,[DEV_IS_CR_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,14,2)
			,[DEV_ISN_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_H29_VIG_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISN_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,14,2)
			,[DEV_ISP_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,14,2)
			,[DEV_ISP_CART_DET_MR_H29_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISP_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,14,2)
			,[DEV_IP_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,14,2)
			,[DEV_IP_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,14,2)
			,[DEV_IP_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,14,2)
			,[DEV_IP_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,14,2)
			,[DEV_IP_VENC_COB_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,14,2)
			,[DEV_IP_PRE_JUDIC_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,14,2)
			,[DEV_IP_EJEC_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,14,2)
			,dbo.sce_int_cdr_dev.DEV_REAJ_VIG_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_D90_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIV_PRORR_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIF_PCIO_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET__VIG_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_D90_MN
			,dbo.sce_int_cdr_dev.DEV_RS_DIV_PRORR_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_VIG_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_H29_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M30_H59_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M60_H89_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_D90_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_INT_COB_VENC
			,dbo.sce_int_cdr_dev.DEV_RS_CM_PRE_JUDIC_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_EN_EJEC_MO
			,dbo.sce_int_cdr_dev.DEV_PPP_CONTAB_CRE
			,dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP
         FROM    dbo.sce_int_cdr_dev
         WHERE   dbo.sce_int_cdr_dev.DEV_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP = @Moneda
         AND     ((dbo.sce_int_cdr_dev.DEV_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END  

    -- Rut y Ambas Monedas
      IF @RUT <> '''' and @Moneda = ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_dev.DEV_TIPO_REG
			,dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_OFI_FIC_CONT
			,dbo.sce_int_cdr_dev.DEV_TO
			,dbo.sce_int_cdr_dev.DEV_PPP_EMB
			,dbo.sce_int_cdr_dev.DEV_NUM_DOC
			,[DEV_INT_VIG_MO]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,14,2)
			,[DEV_INT_MORA_H29_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,14,2)
			,[DEV_INT_MORA_M30_H59_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,14,2)
			,[DEV_INT_MORA_M60_H89_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,14,2)
			,[DEV_INT_MORA_D90_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,14,2)
			,[DEV_INT_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,14,2)
			,[DEV_INT_DIF_PCIO_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,14,2)
			,[DEV_INT_VIG_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,14,2)
			,[DEV_INT_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,14,2)
			,[DEV_INT_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,14,2)
			,[DEV_INT_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,14,2)
			,[DEV_INT_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,14,2)
			,[DEV_INT_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,14,2)
			,[DEV_INT_DIF_PCIO_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,14,2)
			,[DEV_IS_CR_VIG_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,14,2)
			,[DEV_IS_CR_MORA_H29_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,14,2)
			,[DEV_IS_CR_MORA_D90_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,14,2)
			,[DEV_IS_CR_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,14,2)
			,[DEV_IS_CR_VIG_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,14,2)
			,[DEV_IS_CR_MORA_H29_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,14,2)
			,[DEV_IS_CR_MORA_D90_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,14,2)
			,[DEV_IS_CR_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,14,2)
			,[DEV_ISN_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_H29_VIG_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISN_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,14,2)
			,[DEV_ISP_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,14,2)
			,[DEV_ISP_CART_DET_MR_H29_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISP_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,14,2)
			,[DEV_IP_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,14,2)
			,[DEV_IP_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,14,2)
			,[DEV_IP_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,14,2)
			,[DEV_IP_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,14,2)
			,[DEV_IP_VENC_COB_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,14,2)
			,[DEV_IP_PRE_JUDIC_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,14,2)
			,[DEV_IP_EJEC_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,14,2)
			,dbo.sce_int_cdr_dev.DEV_REAJ_VIG_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_D90_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIV_PRORR_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIF_PCIO_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET__VIG_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_D90_MN
			,dbo.sce_int_cdr_dev.DEV_RS_DIV_PRORR_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_VIG_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_H29_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M30_H59_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M60_H89_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_D90_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_INT_COB_VENC
			,dbo.sce_int_cdr_dev.DEV_RS_CM_PRE_JUDIC_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_EN_EJEC_MO
			,dbo.sce_int_cdr_dev.DEV_PPP_CONTAB_CRE
			,dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP
         FROM    dbo.sce_int_cdr_dev
         WHERE   dbo.sce_int_cdr_dev.DEV_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR+dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR = @RUT
         AND     ((dbo.sce_int_cdr_dev.DEV_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  
  
  -- Rut y Moneda
      IF @RUT <> '''' and @Moneda <> ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_dev.DEV_TIPO_REG
			,dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_OFI_FIC_CONT
			,dbo.sce_int_cdr_dev.DEV_TO
			,dbo.sce_int_cdr_dev.DEV_PPP_EMB
			,dbo.sce_int_cdr_dev.DEV_NUM_DOC
			,[DEV_INT_VIG_MO]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,14,2)
			,[DEV_INT_MORA_H29_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,14,2)
			,[DEV_INT_MORA_M30_H59_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,14,2)
			,[DEV_INT_MORA_M60_H89_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,14,2)
			,[DEV_INT_MORA_D90_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,14,2)
			,[DEV_INT_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,14,2)
			,[DEV_INT_DIF_PCIO_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,14,2)
			,[DEV_INT_VIG_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,14,2)
			,[DEV_INT_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,14,2)
			,[DEV_INT_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,14,2)
			,[DEV_INT_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,14,2)
			,[DEV_INT_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,14,2)
			,[DEV_INT_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,14,2)
			,[DEV_INT_DIF_PCIO_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,14,2)
			,[DEV_IS_CR_VIG_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,14,2)
			,[DEV_IS_CR_MORA_H29_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,14,2)
			,[DEV_IS_CR_MORA_D90_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,14,2)
			,[DEV_IS_CR_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,14,2)
			,[DEV_IS_CR_VIG_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,14,2)
			,[DEV_IS_CR_MORA_H29_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,14,2)
			,[DEV_IS_CR_MORA_D90_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,14,2)
			,[DEV_IS_CR_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,14,2)
			,[DEV_ISN_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_H29_VIG_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISN_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,14,2)
			,[DEV_ISP_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,14,2)
			,[DEV_ISP_CART_DET_MR_H29_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISP_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,14,2)
			,[DEV_IP_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,14,2)
			,[DEV_IP_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,14,2)
			,[DEV_IP_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,14,2)
			,[DEV_IP_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,14,2)
			,[DEV_IP_VENC_COB_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,14,2)
			,[DEV_IP_PRE_JUDIC_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,14,2)
			,[DEV_IP_EJEC_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,14,2)
			,dbo.sce_int_cdr_dev.DEV_REAJ_VIG_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_D90_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIV_PRORR_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIF_PCIO_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET__VIG_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_D90_MN
			,dbo.sce_int_cdr_dev.DEV_RS_DIV_PRORR_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_VIG_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_H29_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M30_H59_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M60_H89_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_D90_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_INT_COB_VENC
			,dbo.sce_int_cdr_dev.DEV_RS_CM_PRE_JUDIC_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_EN_EJEC_MO
			,dbo.sce_int_cdr_dev.DEV_PPP_CONTAB_CRE
			,dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP
         FROM    dbo.sce_int_cdr_dev
         WHERE   dbo.sce_int_cdr_dev.DEV_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR+dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR = @RUT
         AND     dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP = @Moneda
         AND     ((dbo.sce_int_cdr_dev.DEV_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  
  
    -- Operación y Ambas Monedas
      IF @Operacion <> '''' and @Moneda = ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_dev.DEV_TIPO_REG
			,dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_OFI_FIC_CONT
			,dbo.sce_int_cdr_dev.DEV_TO
			,dbo.sce_int_cdr_dev.DEV_PPP_EMB
			,dbo.sce_int_cdr_dev.DEV_NUM_DOC
			,[DEV_INT_VIG_MO]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,14,2)
			,[DEV_INT_MORA_H29_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,14,2)
			,[DEV_INT_MORA_M30_H59_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,14,2)
			,[DEV_INT_MORA_M60_H89_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,14,2)
			,[DEV_INT_MORA_D90_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,14,2)
			,[DEV_INT_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,14,2)
			,[DEV_INT_DIF_PCIO_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,14,2)
			,[DEV_INT_VIG_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,14,2)
			,[DEV_INT_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,14,2)
			,[DEV_INT_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,14,2)
			,[DEV_INT_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,14,2)
			,[DEV_INT_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,14,2)
			,[DEV_INT_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,14,2)
			,[DEV_INT_DIF_PCIO_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,14,2)
			,[DEV_IS_CR_VIG_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,14,2)
			,[DEV_IS_CR_MORA_H29_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,14,2)
			,[DEV_IS_CR_MORA_D90_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,14,2)
			,[DEV_IS_CR_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,14,2)
			,[DEV_IS_CR_VIG_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,14,2)
			,[DEV_IS_CR_MORA_H29_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,14,2)
			,[DEV_IS_CR_MORA_D90_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,14,2)
			,[DEV_IS_CR_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,14,2)
			,[DEV_ISN_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_H29_VIG_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISN_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,14,2)
			,[DEV_ISP_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,14,2)
			,[DEV_ISP_CART_DET_MR_H29_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISP_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,14,2)
			,[DEV_IP_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,14,2)
			,[DEV_IP_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,14,2)
			,[DEV_IP_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,14,2)
			,[DEV_IP_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,14,2)
			,[DEV_IP_VENC_COB_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,14,2)
			,[DEV_IP_PRE_JUDIC_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,14,2)
			,[DEV_IP_EJEC_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,14,2)
			,dbo.sce_int_cdr_dev.DEV_REAJ_VIG_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_D90_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIV_PRORR_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIF_PCIO_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET__VIG_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_D90_MN
			,dbo.sce_int_cdr_dev.DEV_RS_DIV_PRORR_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_VIG_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_H29_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M30_H59_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M60_H89_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_D90_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_INT_COB_VENC
			,dbo.sce_int_cdr_dev.DEV_RS_CM_PRE_JUDIC_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_EN_EJEC_MO
			,dbo.sce_int_cdr_dev.DEV_PPP_CONTAB_CRE
			,dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP
         FROM    dbo.sce_int_cdr_dev
         WHERE   dbo.sce_int_cdr_dev.DEV_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_dev.DEV_NUM_DOC = @Operacion
         AND     ((dbo.sce_int_cdr_dev.DEV_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
  
  
     -- Operación y Moneda
      IF @Operacion <> '''' and @Moneda <> ''9''
      BEGIN
         SELECT   dbo.sce_int_cdr_dev.DEV_TIPO_REG
			,dbo.sce_int_cdr_dev.DEV_CPO_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_DV_RUT_DDOR_DIR
			,dbo.sce_int_cdr_dev.DEV_OFI_FIC_CONT
			,dbo.sce_int_cdr_dev.DEV_TO
			,dbo.sce_int_cdr_dev.DEV_PPP_EMB
			,dbo.sce_int_cdr_dev.DEV_NUM_DOC
			,[DEV_INT_VIG_MO]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MO,14,2)
			,[DEV_INT_MORA_H29_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MO,14,2)
			,[DEV_INT_MORA_M30_H59_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MO,14,2)
			,[DEV_INT_MORA_M60_H89_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MO,14,2)
			,[DEV_INT_MORA_D90_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MO,14,2)
			,[DEV_INT_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MO,14,2)
			,[DEV_INT_DIF_PCIO_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MO,14,2)
			,[DEV_INT_VIG_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_VIG_MC,14,2)
			,[DEV_INT_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_H29_MC,14,2)
			,[DEV_INT_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M30_H59_MC,14,2)
			,[DEV_INT_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_M60_H89_MC,14,2)
			,[DEV_INT_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_MORA_D90_MC,14,2)
			,[DEV_INT_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIV_PRORR_MC,14,2)
			,[DEV_INT_DIF_PCIO_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_INT_DIF_PCIO_MC,14,2)
			,[DEV_IS_CR_VIG_MO]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MO,14,2)
			,[DEV_IS_CR_MORA_H29_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MO,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MO,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MO]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MO,14,2)
			,[DEV_IS_CR_MORA_D90_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MO,14,2)
			,[DEV_IS_CR_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MO,14,2)
			,[DEV_IS_CR_VIG_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_VIG_MC,14,2)
			,[DEV_IS_CR_MORA_H29_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_H29_MC,14,2)
			,[DEV_IS_CR_MORA_M30_H59_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M30_H59_MC,14,2)
			,[DEV_IS_CR_MORA_M60_H89_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_M60_H89_MC,14,2)
			,[DEV_IS_CR_MORA_D90_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_MORA_D90_MC,14,2)
			,[DEV_IS_CR_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CR_DIV_PRORR_MC,14,2)
			,[DEV_ISN_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_H29_VIG_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_H29_VIG_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISN_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISN_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISN_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MO]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MO,14,2)
			,[DEV_ISP_CART_DET_VIG_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_VIG_MC,14,2)
			,[DEV_ISP_CART_DET_MR_H29_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_H29_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M30_H59_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M30_H59_MC,14,2)
			,[DEV_ISP_CART_DET_MR_M60_H89_MC]=	SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_M60_H89_MC,14,2)
			,[DEV_ISP_CART_DET_MR_D90_MC]=		SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_ISP_CART_DET_MR_D90_MC,14,2)
			,[DEV_IS_CV_DIV_PRORR_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IS_CV_DIV_PRORR_MC,14,2)
			,[DEV_IP_MORA_H29_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_H29_MC,14,2)
			,[DEV_IP_MORA_M30_H59_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M30_H59_MC,14,2)
			,[DEV_IP_MORA_M60_H89_MC]=			SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_M60_H89_MC,14,2)
			,[DEV_IP_MORA_D90_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_MORA_D90_MC,14,2)
			,[DEV_IP_VENC_COB_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_VENC_COB_MC,14,2)
			,[DEV_IP_PRE_JUDIC_MC]=				SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_PRE_JUDIC_MC,14,2)
			,[DEV_IP_EJEC_MC]=					SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,1,13)+''.''+SUBSTRING(dbo.sce_int_cdr_dev.DEV_IP_EJEC_MC,14,2)
			,dbo.sce_int_cdr_dev.DEV_REAJ_VIG_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_MORA_D90_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIV_PRORR_MN
			,dbo.sce_int_cdr_dev.DEV_REAJ_DIF_PCIO_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET__VIG_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_MORA_H29_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M30_H59_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_M60_H89_MN
			,dbo.sce_int_cdr_dev.DEV_RS_CART_DET_D90_MN
			,dbo.sce_int_cdr_dev.DEV_RS_DIV_PRORR_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_VIG_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_H29_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M30_H59_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_M60_H89_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_MORA_D90_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_INT_COB_VENC
			,dbo.sce_int_cdr_dev.DEV_RS_CM_PRE_JUDIC_MO
			,dbo.sce_int_cdr_dev.DEV_RS_CM_EN_EJEC_MO
			,dbo.sce_int_cdr_dev.DEV_PPP_CONTAB_CRE
			,dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP
         FROM    dbo.sce_int_cdr_dev
         WHERE   dbo.sce_int_cdr_dev.DEV_FECHA_INT BETWEEN CONVERT(DATETIME,@FInicio,103) AND CONVERT(DATETIME,@FTermino,103)
         AND     dbo.sce_int_cdr_dev.DEV_NUM_DOC = @Operacion
         AND     dbo.sce_int_cdr_dev.DEV_COD_MDA_CONTAB_CAP = @Moneda
         AND     ((dbo.sce_int_cdr_dev.DEV_TO = @PlazoTO) OR (@PlazoTO IS NULL))
      END
   END
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_gpln_s15_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_gpln_s15_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_gpln_s15_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_gpln_s15_MS
	@cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecing         datetime 
AS
begin
	

/************************************************************************/

IF 1=0 BEGIN
		SET FMTONLY OFF
	END

   create table #plntemp
   (
      numpre NUMERIC(10,0),
      fecpre CHAR(8)
   )


   insert #plntemp
   select  num_presen ,
		convert(CHAR(8),fechaventa,112)
   from dbo.sce_plan
   where cencos = @cencos
   and codusr = @codusr
   and fechaventa >= dateadd(dd,0,@fecing)
   and fechaventa <  dateadd(dd,1,@fecing)
   and estado <> 9
   and estado <> 8
   and (rut = '''' or
   rut = ''0000000000'' or
   rut IS NULL or
   cod_plaza = 0)

 
   insert #plntemp
   select  convert(NUMERIC(10,0),numpli),
        	convert(CHAR(8),fecpli,112)
   from dbo.sce_pli
   where   cencos = @cencos
   and 	codusr = @codusr
   and   fecpli  >= dateadd(dd,0,@fecing)
   and	fecpli  <  dateadd(dd,+1,@fecing)
   and  	estado <> 9
   and  	estado <> 8
   and  	(rutcli = '''' or
   rutcli = ''0000000000'' or
   rutcli IS NULL or
   plzbcc = 0)                                

   insert #plntemp
   select  convert(NUMERIC(10,0),numpre) ,
        	convert(CHAR(8),fecpre,112)
   from dbo.sce_xplv
   where   cencos = @cencos
   and  	codusr = @codusr
   and   fecpre >= dateadd(dd,0,@fecing)
   and	fecpre <  dateadd(dd,+1,@fecing)
   and  	estado <> 9
   and  	estado <> 8
   and  	(rutexp = '''' or
   rutexp = ''0000000000'' or
   rutexp IS NULL or
   plzbcc = 0)                                


   insert #plntemp
   select  convert(NUMERIC(10,0),numpre) ,
       		convert(CHAR(8),fecpre,112)
   from dbo.sce_xanu
   where   cencos = @cencos
   and	codusr = @codusr
   and   fecpre >= dateadd(dd,0,@fecing)
   and   fecpre <  dateadd(dd,+1,@fecing)
   and  	estado <> 9
   and  	estado <> 8
   and  	(rutexp = '''' or
   rutexp = ''0000000000'' or
   rutexp IS NULL or
   plzbcc = 0)                                


   select   numpre,
		[Column1]=convert(datetime,fecpre)
   from #plntemp order by numpre
   return

end'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s05_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s05_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s05_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_s05_MS] 
	@cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecmov         datetime,
	@tipope		    NUMERIC(1,0) 
AS
BEGIN

IF 1=0 BEGIN
    SET FMTONLY OFF
END

/************************************************************************/
   declare @sum_debe       NUMERIC(13,2),@sum_haber      NUMERIC(13,2)
   begin
	
	--Se crea la tabla temporal donde se almacenar  resultado.-
      create table #tmp
      (
         nemcta          CHAR(15),
         numcta          CHAR(8),
         nemmon          CHAR(3),
         mtomcd_d        NUMERIC(15,2),
         mtomcd_h        NUMERIC(15,2)
      )
	--Se crea la tabla temporal al debe.-
      create table #tmp_d
      (
         nemcta	CHAR(15),
         numcta	CHAR(8),
         nemmon	CHAR(3),
         mtomcd	NUMERIC(15,2)
      )
	--Se crea la tabla temporal al haber.-
      create table #tmp_h
      (
         nemcta	CHAR(15),
         numcta	CHAR(8),
         nemmon	CHAR(3),
         mtomcd	NUMERIC(15,2)
      )

	--Contabilidad del Usuario.-
      if @tipope = 0
      begin
         insert #tmp_d
         select distinct
         nemcta,
			--numcta,
			'''',
			nemmon,
			sum(mtomcd)
         from dbo.sce_mcd
         where 	cencos = @cencos
         and	codusr = @codusr
         and	fecmov = @fecmov
         and	cod_dh = ''D''
         and	estado <> 9
         group by nemcta,nemmon
         insert #tmp_h
         select distinct
         nemcta,
			--numcta,
			'''',
			nemmon,
			sum(mtomcd)
         from dbo.sce_mcd
         where cencos = @cencos
         and codusr = @codusr
         and fecmov = @fecmov
         and cod_dh = ''H''
         and estado <> 9
         group by nemcta,nemmon
         order by nemcta
      end

	--Contabilidad de la Secci¢n.-
      if @tipope = 1 or @tipope = 2
      begin
         insert #tmp_d
         select distinct
         nemcta,
			--numcta,
			'''',
			nemmon,
			sum(mtomcd)
         from dbo.sce_mcd
         where cencos = @cencos
         and fecmov = @fecmov
         and cod_dh = ''D''
         and estado <> 9
         group by nemcta,nemmon
         insert #tmp_h
         select distinct
         nemcta,
			--numcta,
			'''',
			nemmon,
			sum(mtomcd)
         from dbo.sce_mcd
         where cencos = @cencos
         and fecmov = @fecmov
         and cod_dh = ''H''
         and estado <> 9
         group by nemcta,nemmon
         order by nemcta
      end
    
--	create nonclustered index tmp1 on #tmp_d(nemcta,nemmon)
--    create nonclustered index tmp2 on #tmp_h(nemcta,nemmon)
	--Por cada cuenta y moneda existe debe y haber.-
      insert #tmp
      select d.nemcta,
		   d.numcta,
		   d.nemmon,
		   d.mtomcd,
		   h.mtomcd
      from #tmp_d d, #tmp_h h
      where d.nemcta = h.nemcta
      and d.nemmon = h.nemmon       

	--Por cada cuenta y moneda existe s›lo debe.-
      insert #tmp
      select  d.nemcta,
		d.numcta,
		d.nemmon,
		d.mtomcd,
		0
      from #tmp_d d
      where   d.nemcta+d.nemmon not in(select nemcta+nemmon from #tmp_h)

	--Por cada cuenta y moneda existe s›lo haber.-
      insert #tmp
      select  h.nemcta,
		h.numcta,
		h.nemmon,
		0,
		h.mtomcd
      from #tmp_h h
      where   h.nemcta+h.nemmon not in(select nemcta+nemmon from #tmp_d)

	--Actualiza el n£mero de la cuenta contable.-
      update #tmp set numcta =(select cta_num from dbo.sce_cta where
      #tmp.nemcta = dbo.sce_cta.cta_nem)
      select * from #tmp
      order by nemcta,nemmon
      return
   end

   return
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s06_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s06_MS]
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s06_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_mcd_s06_MS
	@cencos         CHAR(3),
	@codusr         CHAR(2),
	@fecmov         datetime 
AS
BEGIN

/************************************************************************/
   declare @n_reg  NUMERIC(3,0)
   begin
        
	--Verifica Cta. Bco. Corresponsal <> 24 (B. Central).- 	
      select   @n_reg = 0

      select   @n_reg = count(*)
		  from dbo.sce_mcd
		  where cencos = @cencos
		  and codusr = @codusr
		  and fecmov = @fecmov
		  and numcta = (select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp = ''47'')          --ACE
		  and codbco = 24
		  and estado <> 9

      if @n_reg > 0
		  begin
			 select distinct  nrorpt,fecmov
			 from dbo.sce_mcd
			 where cencos = @cencos
			 and codusr = @codusr
			 and fecmov = @fecmov
			 and numcta = (select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp = ''47'') 	          --ACE
			 and codbco = 24
			 and estado <> 9
			 return
		  end

	--Verifica Cta. Bco. Central = 24.-
      select   @n_reg = 0

      select   @n_reg = count(*)
		  from dbo.sce_mcd
		  where cencos = @cencos                      		--(nemcta = ''BOE'' or nemcta = ''BANCENE'') 	and 
		  and codusr = @codusr
		  and fecmov = @fecmov
		  and numcta = (select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp = ''46'')
		  and codbco <> 24
		  and estado <> 9

      if @n_reg > 0
		  begin
			 select distinct  nrorpt,
							   fecmov
			 from dbo.sce_mcd
			 where cencos = @cencos
			 and codusr = @codusr
			 and fecmov = @fecmov
			 and numcta = (select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp = ''46'')
			 and codbco <> 24
			 and estado <> 9
			 return
		  end                                        

        --Verifica Movimientos en Cero.-                    
      select   @n_reg = 0

      select   @n_reg = count(*)
		  from dbo.sce_mcd
		  where cencos = @cencos
		  and codusr = @codusr
		  and fecmov = @fecmov
		  and mtomcd = 0
		  and estado <> 9

      if @n_reg > 0
		  begin
			 select distinct  nrorpt,
					  fecmov
			 from dbo.sce_mcd
			 where cencos = @cencos
			 and codusr = @codusr
			 and fecmov = @fecmov
			 and mtomcd = 0
			 and estado <> 9
			 return
		  end                                                 

	--Verifica Ctas. de Banco con Banco <> 0.-
      select distinct  nrorpt, fecmov
		  from dbo.sce_mcd
		  where cencos = @cencos
		  and codusr = @codusr
		  and fecmov = @fecmov
		  and codbco = 0
		  and estado <> 9
		  and
		  numcta IN
		  (
			select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp in (
				''20'',
				''21'',
				''22'',
				''23'',
				''24'',
				''25'',
				''26'',
				''27'',
				''28'',
				''29'',
				''30'',
				''31'',
				''32'',
				''33'',
				''34'',
				''35'',
				''36'',
				''37'',
				''38'',
				''39'',
				''40'',
				''41'',
				''42'',
				''43'',
				''44'')
		  )
      return
   end
   return
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s28_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s28_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s28_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sce_mcd_s28_MS 
		@cencos CHAR(3),
		@codusr CHAR(2),
		@fecmov datetime 
	AS
	begin
		  select   a.nrorpt,a.fecmov
		  from dbo.sce_mcd a,dbo.sce_mch b
		  where a.nrorpt =  b.nrorpt
		  and a.fecmov =  b.fecmov
		  and b.cencos =  @cencos
		  and b.codusr =  @codusr
		  and a.fecmov =  @fecmov
		  and a.codmon =  1
		  and a.numcct <> ''''
		  and a.nemcta in(''CC$'',''CCE'',''CCEBAE'')
		  and SUBSTRING(a.numcct,1,8) <> a.numcct
		  and SUBSTRING(a.numcct,1,10) <> a.numcct
		  group by a.nrorpt,a.fecmov
	end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s40_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s40_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s40_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sce_mcd_s40_MS 
	@par_codcct      CHAR(3), 
    @par_codesp      CHAR(2),
    @par_fecmov      datetime 

	AS
	BEGIN

	   declare         @retorno        NUMERIC(1,0),@mensaje        VARCHAR(255),@codcct         CHAR(3), 
	   @codesp         CHAR(2),@codofi         CHAR(3),@codpro         CHAR(2),
	   @codope         CHAR(5),@nrorpt         NUMERIC(8,0),@estado         NUMERIC(2,0),
	   @fecmov         datetime
	   if @par_codcct = ''''
	   begin
		  declare cursor1 cursor for
		  select codcct,
					 codpro,
					 codesp,
					 codofi,
					 codope,
					 nrorpt,
					 fecmov,
					 estado
		  from dbo.sce_mch
		  where estado <> 9
		  and fecmov = @par_fecmov
		  for read only
		  open cursor1
		  while 1 = 1
		  begin
			 fetch cursor1 into
			 @codcct,@codpro,@codesp,@codofi,@codope,@nrorpt,@fecmov,@estado
			 if (@@FETCH_STATUS = -1)
			 begin
				BREAK           -- Fin De Archivo   
			 end
			 if (@@FETCH_STATUS = -2)
			 begin
				--select   -1, ''Error en el Cursor ''
				return 23
			 end
			 if exists(	 SELECT TOP 1 1
						 from dbo.sce_mcd
						 where nrorpt = @nrorpt
						 and fecmov = @fecmov
						 and estado <> @estado)
			 begin
				select distinct  codcct,
								   codpro,
								   codesp,
								   codofi,
								   codope,
								   nrorpt,
								   fecmov
				from dbo.sce_mcd
				where nrorpt = @nrorpt
				and fecmov = @fecmov
				return
			 end
		  end
		  close cursor1
		  deallocate cursor1
		  if exists(  SELECT TOP 1 1
					  from dbo.sce_mch  a
					  where not exists(  SELECT TOP 1 1
										 from dbo.sce_mcd b
										 where b.nrorpt = a.nrorpt
										 and b.fecmov = @par_fecmov
										 and b.estado <> 9)
					  and a.fecmov = @par_fecmov
					  and a.estado <> 9)
		  begin
			 select   codcct,
					  codpro,
					  codesp,
					  codofi,
					  codope,
					  nrorpt,
					  fecmov
			 from dbo.sce_mch a
			 where not exists(SELECT TOP 1 1
							from dbo.sce_mcd b
							where b.nrorpt = a.nrorpt
							and b.fecmov = @par_fecmov
							and b.estado <> 9)
			 and a.fecmov = @par_fecmov
			 and a.estado <> 9
			 return
		  end
		  --Si llega a este punto, debe devolver el ultimo select, aun cuando no tenga datos, para poder maperar desde entity
		  --if exists(  SELECT TOP 1 1
					  --from dbo.sce_mcd a
					  --where  not exists(SELECT TOP 1 1
							--			 from dbo.sce_mch b
							--			 where b.nrorpt = a.nrorpt
							--			 and b.fecmov = @par_fecmov
							--			 and b.estado <> 9)
					  --and a.fecmov = @par_fecmov
					  --and a.estado <> 9)
		  --begin
			 select   a.codcct,
					  a.codpro,
					  a.codesp,
					  a.codofi,
					  a.codope,
					  a.nrorpt,
					  a.fecmov
			 from dbo.sce_mcd a
			 where not exists(SELECT TOP 1 1
							from dbo.sce_mch b
							where b.nrorpt = a.nrorpt
							and b.fecmov = @par_fecmov
							and b.estado <> 9)
			 and a.fecmov = @par_fecmov
			 and a.estado <> 9
			 return
		  --end
	   end
	else
	   begin
		  declare cursor1 cursor for
		  select codcct,
					 codpro,
					 codesp,
					 codofi,
					 codope,
					 nrorpt,
					 fecmov,
					 estado
		  from dbo.sce_mch
		  where codcct = @par_codcct
		  and codesp = @par_codesp
		  and fecmov = @par_fecmov
		  and estado <> 9
		  for read only
		  open cursor1
		  while 1 = 1
		  begin
			 fetch cursor1 into
			 @codcct,@codpro,@codesp,@codofi,@codope,@nrorpt,@fecmov,@estado
			 if (@@FETCH_STATUS = -1)
			 begin
				BREAK           -- Fin De Archivo   
			 end
			 if (@@FETCH_STATUS = -2)
			 begin
				--select   -1, ''Error en el Cursor ''
				return 23
			 end
			 if exists(SELECT TOP 1 1
			 from dbo.sce_mcd
			 where nrorpt = @nrorpt
			 and fecmov = @fecmov
			 and estado <> @estado)
			 begin
				select distinct  codcct,
								   codpro,
								   codesp,
								   codofi,
								   codope,
								   nrorpt,
								   fecmov
				from dbo.sce_mcd
				where nrorpt = @nrorpt
				and fecmov = @fecmov
				return
			 end
		  end
		  close cursor1
		  deallocate cursor1
		  if exists(SELECT TOP 1 1
					  from dbo.sce_mch a
					  where not exists(SELECT TOP 1 1
									 from  dbo.sce_mcd b
									 where b.nrorpt = a.nrorpt
									 and b.codcct = @par_codcct
									 and b.codesp = @par_codesp
									 and b.fecmov = @par_fecmov
									 and b.estado <> 9)
					  and a.fecmov = @par_fecmov
					  and a.codcct = @par_codcct
					  and a.codesp = @par_codesp
					  and a.estado <> 9)
		  begin
			 select   a.codcct,
					a.codpro,
					a.codesp,
					a.codofi,
					a.codope,
					a.nrorpt,
					a.fecmov
			 from dbo.sce_mch a
			 where not exists(SELECT TOP 1 1
							from dbo.sce_mcd b
							where b.nrorpt = a.nrorpt
							and b.fecmov = @par_fecmov
							and b.codcct = @par_codcct
							and b.codesp = @par_codesp
							and b.estado <> 9)
			 and a.fecmov = @par_fecmov
			 and a.codcct = @par_codcct
			 and a.codesp = @par_codesp
			 and a.estado <> 9
			 return
		  end
		  --Si llega a este punto, debe devolver el ultimo select, aun cuando no tenga datos, para poder maperar desde entity
		  --if exists(SELECT TOP 1 1
		  --from dbo.sce_mcd  a
		  --where not exists(SELECT TOP 1 1
						 --from  dbo.sce_mch b
						 --where b.nrorpt = a.nrorpt
						 --and b.fecmov = @par_fecmov
						 --and b.codcct = @par_codcct
						 --and b.codesp = @par_codesp
						 --and b.estado <> 9)
		  --and a.fecmov = @par_fecmov
		  --and a.codcct = @par_codcct
		  --and a.codesp = @par_codesp
		  --and a.estado <> 9)
		  --begin
			 select   a.codcct,
					   a.codpro,
					   a.codesp,
					   a.codofi,
					   a.codope,
					   a.nrorpt,
					   a.fecmov
			 from dbo.sce_mcd a
			 where not exists(SELECT TOP 1 1
							from dbo.sce_mch b
							where b.nrorpt = a.nrorpt
							and b.codcct = @par_codcct
							and b.codesp = @par_codesp
							and b.fecmov = @par_fecmov
							and b.estado <> 9)
			 and a.fecmov = @par_fecmov
			 and a.codcct = @par_codcct
			 and a.codesp = @par_codesp
			 and a.estado <> 9
			 return
		 -- end
	   end
	   return
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s61_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s61_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s61_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sce_mcd_s61_MS 
		@cencos         CHAR(3),
		@codusr         CHAR(2),
		@fecmov         datetime 
	AS
	BEGIN
	/************************************************************************/
	   select distinct  a.nrorpt,a.fecmov
	   from  dbo.sce_mcd a, dbo.sce_ccof b
	   where a.cencos = @cencos
	   and a.codusr = @codusr
	   and a.fecmov = @fecmov
	   and a.numcta = (select DISTINCT RIGHT(''00000000''+cta_num,8) from sce_cta where cta_codetp = ''45'')
	   and a.estado <> 9
	   and a.codcct = b.ccosto
	   and a.ofides = convert(NUMERIC(3,0),b.oficon)
	
	   return
	   return
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s70_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s70_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s70_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_s70_MS] 
		@cencos CHAR(3),
		@codusr CHAR(2) 
	AS
	begin

  
	   --declare 	@cencos CHAR(3) 
	  --declare	@codusr CHAR(2) 

	  --set @cencos = ''753''
	  --set @codusr = ''29''

	  --se realizo un cambio para que funcione igual que la consulta de inyeccion
	  --primero se traen los movimientos de CITI
	   select   a.numcct,
					a.nemcta,
					a.fecmov,
					a.nroimp,
					[Column1] = isnull(convert(VARCHAR,b.nomcli),''''),
					a.cod_dh,
					a.nemmon,
					a.codmon,
					a.mtomcd,
					[Column2] = a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
					a.nrorpt,
					a.enlinea,
					a.estado,
					a.codcct,
					a.codpro,
					a.codesp,
					a.codofi,
					a.codope,
					[Column3] = isnull(b.codfun,0)
				  from  dbo.sce_mcd a,
						  dbo.sce_mch b,
						  dbo.tbl_sce_tranope_ft c,
						  dbo.tbl_sce_relacion_ft d,
						  dbo.tbl_sce_parametros_ft e,
						  dbo.tbl_sce_codtran_ft f,
						  dbo.sgt_mnd g
				   where /*a.codpro  = ''30''
				   and*/   a.enlinea = 0
				   and   a.cencos  = @cencos
				   and   a.codusr  = @codusr
				   and   a.fecmov  = convert(CHAR(8),GetDate(),112)
				   and	  a.nemcta  = e.tipo_ft
				   and   a.codcct  = b.codcct
				   and   a.codpro  = b.codpro
				   and   a.codesp  = b.codesp
				   and   a.codofi  = b.codofi
				   and   a.codope  = b.codope
				   and   a.nrorpt  = b.nrorpt
				   and   a.fecmov  = b.fecmov
				   and   a.codcct  = c.codcct
				   and   a.codpro  = c.codpro
				   and   a.codesp  = c.codesp
				   and   a.codofi  = c.codofi
				   and   a.codope  = c.codope
				   and   a.nrorpt  = c.nrorpt
				   and   a.fecmov  = c.fecmov
				   and   a.cod_dh  = c.cod_dh
				   and   a.codcct  = d.codcct
				   and   a.codpro  = d.codpro
				   and   a.codesp  = d.codesp
				   and   a.codofi  = d.codofi
				   and   a.codope  = d.codope
				   and   a.nroimp  = d.nroimp
				   and   c.nro_trx = f.nro_trx
				   and   a.codmon  = g.mnd_mndcod
				   and   c.nroimp  = d.nroimp
				   and   b.estado <> 9

	   union all
	   -- se trae la info de BCH
			select   a.numcct,
					a.nemcta,
					a.fecmov,
					a.nroimp,
					[Column1] = isnull(convert(VARCHAR,b.nomcli),''''),
					a.cod_dh,
					a.nemmon,
					a.codmon,
					a.mtomcd,
					[Column2] = a.codcct+a.codpro+a.codesp+a.codofi+a.codope,
					a.nrorpt,
					a.enlinea,
					a.estado,
					a.codcct,
					a.codpro,
					a.codesp,
					a.codofi,
					a.codope,
					[Column3] = isnull(b.codfun,0)
					FROM  dbo.sce_mcd a 
				   LEFT OUTER JOIN 
				   dbo.sce_mch b ON a.nrorpt = b.nrorpt and a.fecmov = b.fecmov and b.estado  <> 9
				   LEFT OUTER JOIN
					dbo.tbl_sce_relacion_ft c ON a.codcct  =  c.codcct AND a.codpro  =  c.codpro AND a.codesp  =  c.codesp AND a.codofi  =  c.codofi AND a.codope  =  c.codope  and a.nroimp =  c.nroimp,
					dbo.sce_ccce e,
					dbo.sce_cctx tx,
					dbo.tbl_sce_sup_codigos sc,
					dbo.sgt_mnd g
				   WHERE a.enlinea = 0 AND a.cencos  =  @cencos AND a.codusr  =  @codusr AND a.fecmov  =  convert(CHAR(8),GetDate(),112) AND a.nemcta  in(''CC$'',''CCE'')
				AND  a.codmon  = g.mnd_mndcod
				AND e.codsis = codfun and e.moneda = IIF(a.codmon = ''1'', ''N'', ''E'')
				AND tx.nemtra = 
							(CASE 
								WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''ABONOMN''
								WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''CARGOMN''
								WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''ABONOME''
								WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN ''CARGOME'' 
								ELSE ''''
							END) 
				AND  sc.cod_stb = tx.codtra AND sc.cod_extendido = FORMAT(CAST(e.codext AS INT) , ''d5'') AND sc.cod_normal = (CASE 
								WHEN (a.codmon = 1 and cod_dh = ''H'') THEN ''77''
								WHEN (a.codmon = 1 and cod_dh = ''D'') THEN ''28''
								WHEN (a.codmon <> 1 and cod_dh = ''H'') THEN ''''
								WHEN (a.codmon <> 1 and cod_dh = ''D'') THEN '''' 
								ELSE ''''
							END) 
   
	   return


	end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s71_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s71_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s71_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mcd_s71_MS] @cencos  CHAR(3),
	  @codusr  CHAR(2),
	  @rutais  CHAR(8),
	  @fecmov  datetime 
	AS
	begin
	-- This procedure was converted on Wed Apr 16 15:34:34 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).

	/************************************************************************/
	/******** Obtiene Contabilidad del Día realizada por el Especialista ****/
	/************************************************************************/


	   select   codcct, codpro, codesp, codofi, codope, nrorpt, fecmov, cod_dh, nemcta, numcct, mtomcd ,nemmon,estado
	   from	dbo.sce_mcd
	   where  fecmov = @fecmov
	   and  rutais = @rutais	
		--and  estado <> 9
	   and  nemcta = ''CC$''    	-- Cuenta Corriente M.N.
	   and  enlinea = 1
	   UNION ALL
	   select   codcct, codpro, codesp, codofi, codope, nrorpt, fecmov, cod_dh, nemcta, numcct, mtomcd ,nemmon,estado
	   from	dbo.sce_mcd
	   where fecmov = @fecmov
	   and rutais = @rutais	
	   -- and estado <> 9		
	   and nemcta = ''CCE''   -- Cuenta Corriente M.E.
	   and enlinea = 1
	   return	
	end'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_s12_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mch_s12_MS]
END



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s12_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_mch_s12_MS
		@cencos		CHAR(3),
		@codusr 	CHAR(2) 
	AS
	BEGIN

		IF 1=0 BEGIN
			SET FMTONLY OFF
		END

	   declare usr_grupo cursor for
	   select cent_costo, id_especia 
	   from 	dbo.sce_usr
	   where 	cent_super = @cencos and
	   id_super = @codusr

	   declare @codcct   CHAR(3),@codpro   CHAR(2),@codesp   CHAR(2),@codofi   CHAR(3),
	   @codope   CHAR(5),@nrorpt   NUMERIC(8,0),@fecmov   datetime,@es_lider INT
	   if exists(select name from dbo.sysobjects where name = ''mch_tmp#'')
		  drop table dbo.mch_tmp#

	   create table dbo.mch_tmp#
	   (
		  codcct   CHAR(3)     NOT NULL,
		  codpro   CHAR(2)     NOT NULL,
		  codesp   CHAR(2)     NOT NULL,
		  codofi   CHAR(3)     NOT NULL,
		  codope   CHAR(5)     NOT NULL,
		  nrorpt   NUMERIC(8,0)  NOT NULL,
		  fecmov   datetime     NOT NULL 
	   )



	--Verifica si el usuario es lider o supervisor
	--============================================
	   select   @es_lider = convert(INT,id_super)
	   from 	dbo.sce_usr
	   where	cent_costo = @cencos and
	   id_especia = @codusr

	   if @es_lider = 0
	   begin
		  open usr_grupo
		  fetch usr_grupo into @codcct,@codesp
		  while (@@FETCH_STATUS != -1)
		  begin
			 if (@@FETCH_STATUS = -2)
				return -3
			 insert into dbo.mch_tmp#
			 select distinct codcct, codpro, codesp,
					codofi, codope, nrorpt,fecmov
			 from 	dbo.sce_mch
			 where  	(estado <> 9) and
					(codcct = @codcct) and
					(codesp = @codesp) and
					(datediff(dd,fecmov,GetDate()) <> 0) and
					(codcct+codpro+codesp+codofi+codope in(select codcct+codpro+codesp+codofi+codope
				from dbo.sce_ref))
			 if (@@error != 0)
				return -2
			 fetch usr_grupo into @codcct,@codesp
		  end
		  close usr_grupo
		  select * from dbo.mch_tmp#
	   end
	else
	   begin
		  select distinct  codcct, codpro, codesp, codofi, codope, nrorpt,fecmov
		  from 	dbo.sce_mch
		  where  	(estado <> 9) and
			(codcct = @cencos) and
			(codesp = @codusr) and
			(datediff(dd,fecmov,GetDate()) <> 0) and
			(codcct+codpro+codesp+codofi+codope in(select codcct+codpro+codesp+codofi+codope
			 from dbo.sce_ref))
	   end

	   drop table dbo.mch_tmp#

	   deallocate usr_grupo
   
	   return 0
   
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_s14_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mch_s14_MS]
END



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s14_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mch_s14_MS] 
		@cencos		CHAR(3),
		@codusr		CHAR(2) 
	AS
	BEGIN

		IF 1=0 BEGIN
			SET FMTONLY OFF
		END

	   declare usr_grupo cursor for
	   select cent_costo, id_especia
	   from    dbo.sce_usr
	   where   cent_super = @cencos and
	   id_super = @codusr

	   declare @codcct   CHAR(3),@codpro   CHAR(2),@codesp   CHAR(2),@codofi   CHAR(3),
	   @codope   CHAR(5),@nrorpt   NUMERIC(8,0),@fecmov   datetime,@es_lider INT
	   if exists(select name from dbo.sysobjects where name = ''mch_tmp#'')
		  drop table dbo.mch_tmp#

	   create table dbo.mch_tmp#
	   (
		  codcct   CHAR(3)     NOT NULL,
		  codpro   CHAR(2)     NOT NULL,
		  codesp   CHAR(2)     NOT NULL,
		  codofi   CHAR(3)     NOT NULL,
		  codope   CHAR(5)     NOT NULL,
		  nrorpt   NUMERIC(8,0)  NOT NULL,
		  fecmov   datetime     NOT NULL 
	   )
                                        


	--Verifica si el usuario es lider o supervisor
	--============================================
	   select   @es_lider = convert(INT,id_super)
	   from	dbo.sce_usr
	   where	cent_costo = @cencos and
	   id_especia = @codusr

	   if @es_lider = 0
	   begin
		  open usr_grupo
		  fetch usr_grupo into @codcct,@codesp
		  while (@@FETCH_STATUS != -1)
		  begin
			 if (@@FETCH_STATUS = -2)
				return -3
			 insert into dbo.mch_tmp#
			 select distinct codcct, codpro, codesp, codofi,
					codope, nrorpt, fecmov
			 from 	dbo.sce_mch
			 where  	(estado <> 9) and
					(codcct = @codcct) and
					(codesp = @codesp) and
					(datediff(dd,fecmov,GetDate()) <> 0) and
					(codcct+codpro+codesp+codofi+codope not in(select codcct+codpro+codesp+codofi+codope
				from dbo.sce_ref))
			 if (@@error != 0)
				return -2
			 fetch usr_grupo into @codcct,@codesp
		  end
		  close usr_grupo
		  --deallocate usr_grupo
		  select * from dbo.mch_tmp#
	   end
	else
	   begin
		  select distinct  codcct, codpro, codesp, codofi, codope, nrorpt,fecmov
		  from 	dbo.sce_mch
		  where  	(estado <> 9) and
	  		(codcct = @cencos) and
			(codesp = @codusr) and
			(datediff(dd,fecmov,GetDate()) <> 0)
			 and
			(codcct+codpro+codesp+codofi+codope not in(select dbo.sce_mch.codcct+dbo.sce_mch.codpro+dbo.sce_mch.codesp+dbo.sce_mch.codofi+dbo.sce_mch.codope
			 from dbo.sce_ref))
	   end

	   deallocate usr_grupo
	  -- 	(codcct = @cencos) and
			--(codesp = @codusr) and
	   drop table dbo.mch_tmp#

	   return 0
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_u01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE sce_mch_u01_MS 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sce_mch_u01_MS
		@nrorpt         NUMERIC(8,0),
		@fecmov         datetime 
	AS
	BEGIN
	BEGIN TRAN

	/*****************************************************************************/

	   update dbo.sce_mcd set estado = 9,nrofac  =  NULL  where
	   nrorpt = @nrorpt and
	   fecmov = @fecmov 
		

	   update dbo.sce_mch set estado = 9  where
	   nrorpt = @nrorpt  and
	   fecmov = @fecmov
		
	   if (@@error <> 0)
	   begin
		  ROLLBACK 
		  Select   -1, ''Error al modificar el estado en Sce_Mcd, Sce_Mch.''
		  return
	   end
	   COMMIT TRAN
	   Select   0,''Grabacion Exitosa''
	   return
	   return

	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_nov_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_nov_s01_MS] 
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_nov_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_nov_s01_MS] 
		@codpro      CHAR(2),
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
				and exists(SELECT TOP 1 1
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
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_nov_s03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_nov_s03_MS] 
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_nov_s03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_nov_s03_MS] 
		@codpro      CHAR(2),
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
				and exists(SELECT TOP 1 1
				   from dbo.sce_usr b
				   where b.cent_super = @cencos
				   and b.id_super   = @codusr
				   and b.cent_costo = a.cencos
				   and b.id_especia = a.codusr)
			 end
		  end
	   end 
	   return
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcdh_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcdh_s01_MS] 
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcdh_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE dbo.sce_mcdh_s01_MS
		@nemcta         CHAR(8),
		@numcta         CHAR(8),
		@numctacte      CHAR(15),
		@cencos         CHAR(3),
		@fecini         datetime,
		@fecfin         datetime,
		@todos          CHAR(1) 
	AS
	begin


	/************************************************************************/


	   if @todos = ''S''
		   begin
			  select   
				[operacion]=codcct+codpro+codesp+codofi+codope,
				codneg,codsec,nrorpt,fecmov,cencos,codusr,
				nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
				rutcli,numcct,mtotas,ofides,numpar,tipmov
			  from dbo.sce_mcdh
			  where fecmov >= @fecini
			  and fecmov <= @fecfin
		   end

	   if @nemcta <> ''''
		   begin
			  if  @cencos = '''' and @numctacte = ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where fecmov >= @fecini
				 and fecmov <= @fecfin
				 and nemcta  = @nemcta
			  if  @cencos <> '''' and @numctacte = ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where fecmov >= @fecini
				 and fecmov <= @fecfin
				 and nemcta  = @nemcta
				 and codcct  = @cencos
			  if @cencos <> '''' and @numctacte <> ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where numcct  = @numctacte
				 and fecmov >= @fecini
				 and fecmov <= @fecfin
				 and nemcta  = @nemcta
				 and codcct  = @cencos
			  if @cencos = '''' and @numctacte <> ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where numcct  = @numctacte
				 and fecmov >= @fecini
				 and fecmov <= @fecfin
				 and nemcta  = @nemcta
		   end

	   if @numcta <> ''''
		   begin
			  if  @cencos = '''' and @numctacte = ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where numcta  = @numcta
				 and fecmov >= @fecini
				 and fecmov <= @fecfin
			  if   @cencos <> '''' and @numctacte = ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where numcta  = @numcta
				 and fecmov >= @fecini
				 and fecmov <= @fecfin
				 and codcct  = @cencos
			  if @cencos <> '''' and @numctacte <> ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where numcta  = @numcta
				 and fecmov >= @fecini
				 and fecmov <= @fecfin
				 and numcct  = @numctacte
				 and codcct  = @cencos
			  if @cencos = '''' and @numctacte <> ''''
				 select   
					[operacion]=codcct+codpro+codesp+codofi+codope,
					codneg,codsec,nrorpt,fecmov,cencos,codusr,
					nroimp,nemcta,numcta,nemmon,mtomcd,cod_dh,
					rutcli,numcct,mtotas,ofides,numpar,tipmov
				 from dbo.sce_mcdh
				 where numcta  = @numcta
				 and fecmov >= @fecini
				 and fecmov <= @fecfin
				 and numcct  = @numctacte
		   end

	   return
	end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_i01' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_i01]
END

/****** Object:  StoredProcedure [dbo].[pro_sce_swf_pendientes_i01]    Script Date: 15-03-2016 8:02:10 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER OFF

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_swf_pendientes_i01]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pro_sce_swf_pendientes_i01] 
		@ctecct char(3),
		@codesp char(2),
		@archivo varchar(6),
		@rutAis varchar(15),
		@sistema varchar(10),
		@fecha datetime,
		@tipo varchar(10),
		@moneda varchar(10),
		@monto decimal(18,2),
		@referencia varchar(16),
		@msjSwift varchar(max)

	AS

	IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.sce_swf_pendientes WHERE ctecct = @ctecct AND codesp = @codesp AND archivo = @archivo)

	   BEGIN 
		  insert into sce_swf_pendientes
				values 
				(
					@ctecct,
					@codesp ,
					@archivo,
					@rutAis ,
					@sistema ,
					@fecha ,
					@tipo ,
					@moneda ,
					@monto ,
					@referencia,
					@msjSwift 
				)

	   END 

	ELSE 

	   BEGIN 

	   UPDATE  sce_swf_pendientes 
	   SET			rutAis =  @rutAis,
					sistema = @sistema ,
					fecha = @fecha,
					tipo = @tipo ,
					moneda = @moneda ,
					monto = @monto ,
					referencia = @referencia,
					msjSwift = @msjSwift  
	   WHERE ctecct = @ctecct AND codesp = @codesp AND archivo = @archivo
	   END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_s01' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_s01]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_swf_pendientes_s01]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure pro_sce_swf_pendientes_s01 
	@ctecct char(3),
	@codesp char(2)

	as

	select 
		ctecct,
		codesp,
		archivo,
		rutAis,
		sistema,
		fecha,
		tipo,
		moneda,
		monto,
		referencia,
		msjSwift 
	from sce_swf_pendientes
	where ctecct = @ctecct and codesp = @codesp'

END




IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_s02' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_s02]
END



IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_swf_pendientes_s02]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure pro_sce_swf_pendientes_s02
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(6)

as

	select 
		ctecct,
		codesp,
		archivo,
		rutAis,
		sistema,
		fecha,
		tipo,
		moneda,
		monto,
		referencia,
		msjSwift 
	from sce_swf_pendientes
	where ctecct = @ctecct and codesp = @codesp and archivo = @archivo'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_swf_pendientes_d01' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_swf_pendientes_d01]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_swf_pendientes_d01]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure pro_sce_swf_pendientes_d01 
	@ctecct char(3),
	@codesp char(2),
	@archivo varchar(6)

as

	delete from sce_swf_pendientes
	where  ctecct = @ctecct and codesp = @codesp and archivo = @archivo'

END

/******************************Control Integral******************************************/
/****** Object:  StoredProcedure [dbo].[cambios_mift_tarifas_pizarra_02_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_tarifas_pizarra_02_MS' AND schema_id = SCHEMA_ID('dbo'))
--	DROP PROCEDURE [dbo].[cambios_mift_tarifas_pizarra_02_MS]
--

--create procedure [dbo].[cambios_mift_tarifas_pizarra_02_MS]
--(
--@tipo     varchar(15),
--@sub_tipo varchar(20),
--@moneda   char(03),
--@monto    numeric(18,2)
--)
--as
--begin
--declare @tasa    numeric(5,2),
--        @minimo  numeric(12,2),
--        @maximo  numeric(12,2),
--        @swift   numeric(12,2),
--        @impto   numeric(12,2),
--        @ourga   numeric(12,2),
--        @speed   numeric(12,2),
--        @carta_manual    numeric(12,2),
--        @rango_ini       numeric(18,2),
--        @rango_fin       numeric(18,2),
--        @comision_me     numeric(18,2),
--        @comision_neto   numeric(12,2),
--        @comision_iva    numeric(12,2),
--        @comision_total  numeric(12,2),
--        @dolar_obs       numeric(12,2)
    
--     if @sub_tipo = 'b) Swift'
--        select 'Swift' 'Pizarra',
--                swift  'Valor'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin
--     if @sub_tipo = 'c) Speed Transfer'
--        select top 1  'Realizar Gestion' 'Pizarra',
--                -1    'Valor'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin


----        select top 1 'Carta Manual' 'Pizarra',
----                0  'Valor'
----           from cambios_gral_tarifas_pizarra
----           where tipo     = 'OPC'
----             and @monto >  rango_ini
----             and @monto <= rango_fin
--/*
--   select @tasa   = isnull(tasa,0),
--          @minimo = minimo,
--          @maximo = maximo,
--          @swift  = swift,
--          @impto  = impto,
--          @ourga  = ourga
--      from cambios_gral_tarifas_pizarra
--      where tipo     = @tipo
----        and sub_tipo = @sub_tipo
--        and @monto >  rango_ini
--        and @monto <= rango_fin
--   select @comision_me = @monto * @tasa
--   if @comision_me < @minimo
--      select @comision_me = @minimo
--   if @comision_me > @maximo
--      select @comision_me = @maximo
--   if @sub_tipo = 'a) Emision OPC' 
--      select @comision_me = @comision_me
--   if @sub_tipo = 'b) Swift' 
--      select @comision_me = @swift
--   if @sub_tipo = 'c) Speed Transfer'
--      select @comision_me = 0
--   if @sub_tipo = 'd) Carta Manual'
--      select @comision_me = 0
--   select @comision_neto  = @comision_me * 570--@dolar_obs
--   select @comision_iva   = (@comision_neto * (1+(@impto/100))) - @comision_neto
--   select @comision_total = @comision_neto + @comision_iva
--   if @sub_tipo = 'a) Emision OPC'
--      select 'COMITRANS'  'Comision',
--             @comision_me 'Valor'
--   if @sub_tipo = 'b) Swift'
--      select 'COMISWIFT'  'Comision',
--             @comision_me 'Valor'
--   if @sub_tipo = 'c) Speed Transfer'
--      select 'Contactar Ejecutivo',
--             @comision_me 'Valor'
--   if @sub_tipo = 'd) Carta Manual'
--      select 'Contactar Ejecutivo',
--             @comision_me 'Valor'
--*/
--end
--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_tarifas_pizarra_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_tarifas_pizarra_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--	DROP PROCEDURE [dbo].[cambios_mift_tarifas_pizarra_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_tarifas_pizarra_01_MS]
--(
--@tipo     varchar(15),
--@sub_tipo varchar(20),
--@moneda   char(03),
--@monto    numeric(18,2)
--)
--as
--begin
--declare @tasa    numeric(5,2),
--        @minimo  numeric(12,2),
--        @maximo  numeric(12,2),
--        @swift   numeric(12,2),
--        @impto   numeric(12,2),
--        @ourga   numeric(12,2),
--        @speed   numeric(12,2),
--        @carta_manual    numeric(12,2),
--        @rango_ini       numeric(18,2),
--        @rango_fin       numeric(18,2),
--        @comision_me     numeric(18,2),
--        @comision_neto   numeric(12,2),
--        @comision_iva    numeric(12,2),
--        @comision_total  numeric(12,2),
--        @dolar_obs       numeric(12,2)
--     if @sub_tipo = 'a) Emision OPC'
--        select 'Emision OPC' 'Pizarra',
--               rango_ini+1 'USD_Min',
--               rango_fin 'USD_Max',
--               convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--               minimo 'Minimo',
--               maximo 'Maximo'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin
--        order by 1,2
--     if @sub_tipo = 'a) Emision OPC2'
--        select 'Emision OPC' 'Pizarra',
--               rango_ini+1 'USD_Min',
--               rango_fin 'USD_Max',
--               convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--               minimo 'Minimo',
--               maximo 'Maximo'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--        order by 1,2
--     if @sub_tipo = 'b) Swift'
--        select 'Swift' 'Pizarra',
--                swift  'Valor'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin
--     if @sub_tipo = 'c) Speed Transfer'
--        select top 1  'Realizar Gestion' 'Pizarra',
--                -1    'Valor'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin
--     if @sub_tipo = 'd) Carta Manual'
--        select 'Carta Manual' 'Pizarra',
--               rango_ini+1 'USD_Min',
--               rango_fin 'USD_Max',
--               convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--               minimo 'Minimo',
--               maximo 'Maximo'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin
--        order by 1,2
--     if @sub_tipo = 'd) Carta Manual2'
--        select 'Carta Manual' 'Pizarra',
--               rango_ini+1 'USD_Min',
--               rango_fin 'USD_Max',
--               convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--               minimo 'Minimo',
--               maximo 'Maximo'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--        order by 1,2
--     if @sub_tipo = 'e) Liquidacion OPR'
--        select 'Liquidacion OPR' 'Pizarra',
--               rango_ini+1 'USD_Min',
--               rango_fin 'USD_Max',
--               convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--               minimo 'Minimo',
--               maximo 'Maximo'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--             and @monto >  rango_ini
--             and @monto <= rango_fin
--        order by 1,2
--     if @sub_tipo = 'e) Liquidacion OPR2'
--        select 'Liquidacion OPR2' 'Pizarra',
--               rango_ini+1 'USD_Min',
--               rango_fin 'USD_Max',
--               convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--               minimo 'Minimo',
--               maximo 'Maximo'
--           from cambios_gral_tarifas_pizarra
--           where tipo     = 'OPC'
--        order by 1,2
----        select top 1 'Carta Manual' 'Pizarra',
----                0  'Valor'
----           from cambios_gral_tarifas_pizarra
----           where tipo     = 'OPC'
----             and @monto >  rango_ini
----             and @monto <= rango_fin
--/*
--   select @tasa   = isnull(tasa,0),
--          @minimo = minimo,
--          @maximo = maximo,
--          @swift  = swift,
--          @impto  = impto,
--          @ourga  = ourga
--      from cambios_gral_tarifas_pizarra
--      where tipo     = @tipo
----        and sub_tipo = @sub_tipo
--        and @monto >  rango_ini
--        and @monto <= rango_fin
--   select @comision_me = @monto * @tasa
--   if @comision_me < @minimo
--      select @comision_me = @minimo
--   if @comision_me > @maximo
--      select @comision_me = @maximo
--   if @sub_tipo = 'a) Emision OPC' 
--      select @comision_me = @comision_me
--   if @sub_tipo = 'b) Swift' 
--      select @comision_me = @swift
--   if @sub_tipo = 'c) Speed Transfer'
--      select @comision_me = 0
--   if @sub_tipo = 'd) Carta Manual'
--      select @comision_me = 0
--   select @comision_neto  = @comision_me * 570--@dolar_obs
--   select @comision_iva   = (@comision_neto * (1+(@impto/100))) - @comision_neto
--   select @comision_total = @comision_neto + @comision_iva
--   if @sub_tipo = 'a) Emision OPC'
--      select 'COMITRANS'  'Comision',
--             @comision_me 'Valor'
--   if @sub_tipo = 'b) Swift'
--      select 'COMISWIFT'  'Comision',
--             @comision_me 'Valor'
--   if @sub_tipo = 'c) Speed Transfer'
--      select 'Contactar Ejecutivo',
--             @comision_me 'Valor'
--   if @sub_tipo = 'd) Carta Manual'
--      select 'Contactar Ejecutivo',
--             @comision_me 'Valor'
--*/
--end


--





--/****** Object:  StoredProcedure [dbo].[cambios_mift_tarifas_obs_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_tarifas_obs_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_tarifas_obs_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_tarifas_obs_01_MS]
--(
--   @accnum varchar(15)
--)
--as

--begin
--declare @rut bigint,

--        @tf_obs1 varchar(40),
--        @tf_obs2 varchar(40),
--        @tf_obs3 varchar(40),
--        @buscar as char(1)
--select @buscar = 'S'
--   if isnumeric(left(@accnum,len(@accnum)-1)) = 1

--    begin

--       select @rut = left(@accnum,len(@accnum)-1)
--       if exists (select top 1 1 from cambios_gral_tarifas where tf_rut_cli = @rut)
--          select @buscar = 'N'
--    end

--   if @buscar = 'S'

--       if exists (SELECT TOP 1 1 from cambios_gral_clientes where accnum = @accnum and exists (SELECT TOP 1 1 from cambios_gral_tarifas where tf_rut_cli = rut))

--        begin
--           select @buscar = 'N'
--           select @rut = rut
--             from cambios_gral_clientes
--             where accnum = @accnum
--        end
--   if @buscar = 'N'
--    select @tf_obs1 = tf_obs1,
--           @tf_obs2 = tf_obs2,
--           @tf_obs3 = tf_obs3
--      from cambios_gral_tarifas
--     where tf_rut_cli = @rut            

--   if @tf_obs1 is null 
--      select @tf_obs1 = ''''
--   if @tf_obs2 is null 
--      select @tf_obs2 = ''''
--   if @tf_obs3 is null 
--      select @tf_obs3 = ''''	     

--   if @tf_obs1+@tf_obs2+@tf_obs3 = ''''
--      select '''' 'Observaciones'
--   else      
--      select 'a) ' +dbo.correccion_caracter_especial_MS(@tf_obs1) 'Observaciones'
--     union 
--      select 'b) ' +dbo.correccion_caracter_especial_MS(@tf_obs2) 'Observaciones'
--     union 
--      select 'c) ' +dbo.correccion_caracter_especial_MS(@tf_obs3) 'Observaciones'
--end


--




--/****** Object:  StoredProcedure [dbo].[cambios_mift_tarifas_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_tarifas_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_tarifas_01_MS]
--

--CREATE procedure cambios_mift_tarifas_01_MS
--(
--@accnum varchar(15),
--@moneda char(03),
--@monto  numeric(18,2)
--)
--as
--begin
--declare @rut              bigint,
--        @buscar           char(1),
--        @dolar_observado  numeric(18,4),
--        @par_reuters      numeric(18,4),
--        @codmon           smallint
-- create table #tablaPizarra 
-- (
--    Rut varchar(10) null,
--	Tarifas varchar(100) null,
--	Mon varchar(5) null,
--	Valor varchar(20) null,
--	Observacion varchar(100) null,
--	Impto varchar(20) null
-- )
--declare @tipo varchar(1)
--set @tipo = 0
--select @dolar_observado = observado        
--   from cambios_revisor_observado
--   where fecha  = convert(datetime,convert(char(08),getdate(),112),112)
--     and codmon = 1
--select @dolar_observado = par_reuters
--   from cambios_gral_tipo_cambio_pizarra
--   where cod_mnd     = 99 -- Dolar Observado    
--if @dolar_observado is null
--   select @dolar_observado = observado        
--      from cambios_revisor_observado
--      where fecha  = convert(datetime,convert(char(08),getdate(),112),112)
--        and codmon = 11
--if @dolar_observado is null
--   select @dolar_observado = 0
--select @codmon = mnd_mndcod
--   from cambios_gral_moneda
--   where mnd_mndswf = @moneda
--select @par_reuters = par_reuters   
--   from cambios_gral_tipo_cambio_pizarra
--   where cod_mnd     = @codmon
--if @moneda <> 'USD'
--begin
----   if @par_reuters <= 1
--      select @monto = @monto / @par_reuters
----     else
----      select @monto = @monto * @par_reuters
--   select @moneda = 'USD'
--end
--select @buscar = 'S'
--   if isnumeric(left(@accnum,len(@accnum)-1)) = 1
--    begin
--       select @rut = left(@accnum,len(@accnum)-1)
--       if exists (select top 1 1 from CAMBIOS_GRAL_TARIFAS where tf_rut_cli = @rut)
--          select @buscar = 'N'
--    end
--   if @buscar = 'S'
--           if exists (SELECT TOP 1 1 from cambios_gral_clientes where accnum = @accnum and exists (SELECT TOP 1 1 from CAMBIOS_GRAL_TARIFAS where tf_rut_cli = rut))
--           begin
--				   select @buscar = 'N'
--				   select @rut = rut             
--					from cambios_gral_clientes
--					where accnum = @accnum
--           end
--   if @buscar = 'N'
--   begin
--       if @moneda = 'USD'
--	   begin
--	      set @tipo = 1 --'Tarifas_S_I_C_C'
--	      insert into #tablaPizarra
--            select tf_rut_cli,
--					'a) Emision OPC'      'Tarifas S.I.C.C.',
--					'USD'                 
--					'Mon.',
--					dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','a) Emision OPC',@moneda,@monto,isnull(tf_e_evca,0), isnull(tf_ce_evca,''''),@rut) 'Valor', 
--					dbo.CORRECCION_CARACTER_ESPECIAL(dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','a) Emision OPC',@rut,isnull(tf_ce_evca,''''))) 'Observacion',
--					round((dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','a) Emision OPC',@moneda,@monto,isnull(tf_e_evca,0), isnull(tf_ce_evca,''''),@rut) +
--					 dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','b) Swift',@moneda,@monto,isnull(tf_e_gs,0), isnull(tf_ce_gs,''''),@rut)) * @dolar_observado,0) 'Monto$'
--				from CAMBIOS_GRAL_TARIFAS
--				where tf_rut_cli = @rut
--			union          
--			 select tf_rut_cli,
--					'b) Swift',
--					'USD'                 'Mon.',
--					dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','b) Swift',@moneda,@monto,isnull(tf_e_gs,0), isnull(tf_ce_gs,''''),@rut)   'Valor', 
--					dbo.CORRECCION_CARACTER_ESPECIAL(dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','b) Swift',@rut,isnull(tf_ce_gs,''''))) 'Observacion',
--					0 'monto$'
--				from CAMBIOS_GRAL_TARIFAS
--				where tf_rut_cli = @rut
--			union          
--			 select tf_rut_cli,
--				   'c) Speed Transfer',
--				   'USD'                 'Mon.',
--					dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','c) Speed Transfer',@moneda,@monto,isnull(tf_t_env,0), isnull(tf_ct_env,''''),@rut)   'Valor', 
--					dbo.CORRECCION_CARACTER_ESPECIAL(dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','c) Speed Transfer',@rut,isnull(tf_ct_env,''''))) 'Observacion',
--					0 'Monto$'
--			   from CAMBIOS_GRAL_TARIFAS
--			   where tf_rut_cli = @rut
--             union          
--			 select tf_rut_cli,    
--				'd) Carta Manual',
--					'USD'                 'Mon.',
--					dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','d) Carta Manual',@moneda,@monto,isnull(tf_t_cheq,0), isnull(tf_ct_cheq,''''),@rut)   'Valor', 
--					dbo.CORRECCION_CARACTER_ESPECIAL(dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','d) Carta Manual',@rut,isnull(tf_ct_cheq,''''))) 'Observacion',
--					0 'Monto$'
--				from CAMBIOS_GRAL_TARIFAS
--				where tf_rut_cli = @rut
--			union          
--			 select tf_rut_cli,
--                'e) Liquidacion OPR'  'Tarifas S.I.C.C.',
--                'USD'                 'Mon.',
--                dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','e) Liquidacion OPR',@moneda,@monto,isnull(tf_r_ccce,0), isnull(tf_c_cccc,''''),@rut)   'Valor', 
--                dbo.CORRECCION_CARACTER_ESPECIAL(dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','e) Liquidacion OPR',@rut,isnull(tf_c_cccc,''''))) 'Observacion',
--                round(dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','e) Liquidacion O PR',@moneda,@monto,isnull(tf_r_ccce,0), isnull(tf_c_cccc,''''),@rut)*@dolar_observado,0) 'Monto$'
--             from CAMBIOS_GRAL_TARIFAS
--             where tf_rut_cli = @rut
--            order by 1,2
--	   end
--   end
--   else
--   begin
--       if @moneda != 'USD'
--       begin
--           if exists (SELECT TOP 1 1 from CAMBIOS_GRAL_TARIFAS_CONVENIO where rut = @rut)
--		   begin
--		      set @tipo = 1 --'Tarifas_S_I_C_C'
--	             insert into #tablaPizarra
--              select rut 'Rut',
--                   'a) Emision OPC' 'Tarifas S.I.C.C.',
--                   'USD' 'Mon.',
--                   valor,
--                   observacion 'Observacion',
--                   round((select valor from CAMBIOS_GRAL_TARIFAS_CONVENIO where rut = @rut and tipo = 'OPC') + (select valor from CAMBIOS_GRAL_TARIFAS_CONVENIO where rut = @rut and tipo = 'SWIFT') * @dolar_observado,0) 'Monto$'
--               from CAMBIOS_GRAL_TARIFAS_CONVENIO
--               where rut  = @rut
--                 and tipo = 'OPC'
--            UNION      
--            select rut,
--                   'b) Swift',
--                   'USD' 'Mon.',
--                   valor,
--                   observacion 'Observacion',
--                   0 'Monto$'
--               from CAMBIOS_GRAL_TARIFAS_CONVENIO
--               where rut  = @rut
--                 and tipo = 'SWIFT'
--            UNION      
--            select rut,      
--                   'c) Speed Transfer',
--                   'USD' 'Mon.',
--                   valor,
--                   observacion 'Observacion',
--                   0 'Monto$'
--               from CAMBIOS_GRAL_TARIFAS_CONVENIO
--               where rut  = @rut   
--              and tipo = 'SPEED'
--            UNION      
--            select rut,
--                   'd) Carta Manual',
--                   'USD' 'Mon.',
--                   valor,
--                   observacion 'Observacion',
--                   0 'Monto$'
--              from CAMBIOS_GRAL_TARIFAS_CONVENIO
--               where rut  = @rut
--                 and tipo = 'MANUAL'
--            UNION      
--            select rut,
--                   'e) Liquidacion OPR',
--                   'USD' 'Mon.',              
--                    valor,
--                   observacion 'Observacion',
--                   round((select valor from CAMBIOS_GRAL_TARIFAS_CONVENIO where rut = @rut and tipo = 'OPR') * @dolar_observado,0) 'Monto$'
--               from CAMBIOS_GRAL_TARIFAS_CONVENIO       
--              where rut  = @rut
--                 and tipo = 'OPR' order by 1,2
--		   end
--           else 
--		     set @tipo = 2 --'Tarifas_Pizarra' 
--           -- select isnull(@rut,0) 'Rut',
--           --    'Normativa' 'Tarifas Pizarra',
--           --    rango_ini+1 'Min',
--           --    rango_fin 'Max', 
--           --    convert(varchar,isnull(tasa,0))+'%' 'Tasa',
--           --    minimo 'Minimo',
--           --    maximo 'Maximo',
--           --    swift  'Swift',
--           --    impto  'Impto.'
--           --  from CAMBIOS_GRAL_TARIFAS_PIZARRA
--           --where tipo  = 'OPC'
--           end
--       else
--       begin
--		  set @tipo = 2 --'Tarifas_Pizarra'
--	      insert into #tablaPizarra  
--              select isnull(@rut,0) 'Rut',
--                 'a) Emision OPC' 'Tarifas Pizarra',
--                 'USD'            'Mon.',
--                 dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','a) Emision OPC',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','a) Emision OPC',@moneda,@monto), 'Valor Pizarra',@rut) Valor,
--				--  dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','a) Emision OPC',@moneda,@monto) 'Valor',
--                 dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','a) Emision OPC',@rut,'Valor Pizarra') 'Observacion',
----                 'Valor Pizarra' 'Observacion',
--                 round((dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','a) Emision OPC',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','a) Emision OPC',@moneda,@monto), 'Valor Pizarra',@rut) +
--                  dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','b) Swift',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','b) Swift',@moneda,@monto), 'Valor Pizarra',@rut)) * @dolar_observado,0) 'Monto$'
--         union
--             select isnull(@rut,0) 'Rut',
--                 'b) Swift' 'Tarifas Pizarra',
--                 'USD'      'Mon.',
--                 dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','b) Swift',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','b) Swift',@moneda,@monto), 'Valor Pizarra',@rut) Valor,
----             dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','b) Swift',@moneda,@monto)  'Valor',
--                 dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','b) Swift',@rut,'Valor Pizarra') 'Observacion',
----                 'Valor Pizarra' 'Observacion',
--                0 'Monto$'
--         union
--            select isnull(@rut,0) 'Rut',
--                 'c) Speed Transfer' 'Tarifas Pizarra',
--                 'USD'      'Mon.',              
--				dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','c) Speed Transfer',@moneda,@monto,-1, 'Valor Pizarra',@rut) Valor,
----                 -1 'Valor',
--                 dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','c) Speed Transfer',@rut,'Cliente No Existe en S.I.C.C.') 'Observacion',
----                 'Cliente No Existe en S.I.C.C.' 'Observacion',
--                0 'Monto$'
--         union
--             select isnull(@rut,0) 'Rut',
--                 'd) Carta Manual' 'Tarifas Pizarra',
--                 'USD'      'Mon.', 
--                dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','d) Carta Manual',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','d) Carta Manual',@moneda,@monto), 'Valor Pizarra',@rut) Valor,
----                 dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','d) Carta Manual',@moneda,@monto)  'Valor',
--                 dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','d) Carta Manual',@rut,'Valor Pizarra') 'Observacion',
----                 'Valor Pizarra' 'Observacion',
--                0 'Monto$'
--         union     
--              select isnull(@rut,0)       'Rut',
--                 'e) Liquidacion OPR' 'Tarifas Pizarra',
--                 'USD'                'Mon.',
--                 dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','e) Liquidacion OPR',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','e) Liquidacion OPR',@moneda,@monto), 'Valor Pizarra',@rut) Valor,
----                 dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','e) Liquidacion OPR',@moneda,@monto) 'Valor',
--                 dbo.CAMBIOS_TARIFAS_CONVENIO_OBS('OPC','e) Liquidacion OPR',@rut,'Valor Pizarra') 'Observacion',
----                 'Valor Pizarra' 'Observacion',
--                 round(dbo.CAMBIOS_TARIFAS_PIZARRA_VALOR2('OPC','e) Liquidacion OPR',@moneda,@monto,dbo.CAMBIOS_TARIFAS_PIZARRA_CALCULO ('OPC','e) Liquidacion OPR',@moneda,@monto), 'Valor Pizarra',@rut) * @dolar_observado,0) 'Monto$'
--               order by 1,2                 
--      end
--   end
--end
--    select *, @tipo as Tipo from #tablaPizarra

--    drop table #tablaPizar
--



--/****** Object:  StoredProcedure [dbo].[cambios_mift_reparo_log_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_reparo_log_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_reparo_log_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_reparo_log_01_MS]
--(
--    @accnum      varchar(20),
--    @rut         varchar(20),
--    @nomcli      varchar(60),
--    @moneda	     char(3),
--    @monto	     numeric(18, 2),
--    @doc_name    varchar(80),
--    @mailejec    varchar(80),
--    @usuario	 varchar(20),
--    @reparo_list varchar(20),
--    @otro_1	     varchar(100),
--    @otro_2	     varchar(100),
--    @otro_3	     varchar(100),
--    @otro_4	     varchar(100),
--    @otro_5	     varchar(100)
--)
--as
--begin
--   insert into cambios_mift_reparo_log (accnum, rut, nomcli, moneda, monto, doc_name, mailejec, fecha, hora, usuario, reparo_list, otro_1, otro_2, otro_3, otro_4, otro_5) 
--      values (@accnum, @rut, @nomcli, @moneda, @monto, @doc_name, @mailejec, convert(datetime,convert(char(08),getdate(),112),112), getdate(), @usuario, @reparo_list, @otro_1, @otro_2, @otro_3, @otro_4, @otro_5)
--end




--



--/****** Object:  StoredProcedure [dbo].[cambios_mift_recurrencia_manual_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_recurrencia_manual_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_recurrencia_manual_01_MS]
--
--CREATE procedure [dbo].[cambios_mift_recurrencia_manual_01_MS]
--(
--    @opcion       tinyint,
--    @accnum       varchar(60),
--    @ord_nombre   varchar(40),
--    @bnf_cuenta   varchar(40),
--    @bnf_nombre   varchar(60),
--    @bnf_swfbco   varchar(60)
--)
--as
--begin
--declare @ord_rut      varchar(12),
--        @ord_cuenta   varchar(60),
--        @bnf_swbcoint varchar(60),
--        @bnf_nombco   varchar(150)
--    select @ord_rut = rutcli
--       from cambios_gral_clientes
--       where accnum = @accnum  
--   if @ord_rut != right('00000000'+@accnum,8)
--      select @ord_cuenta = @accnum
--   select @bnf_swbcoint = 'LOCAL'
--   insert into cambios_mift_recurrencia (ord_rut, ord_cuenta, ord_nombre, bnf_cuenta, bnf_nombre, bnf_swbcoint, bnf_swfbco, bnf_nombco, cantidad)
--      values (@ord_rut, @ord_cuenta, @ord_nombre, @bnf_cuenta, @bnf_nombre, @bnf_swbcoint, isnull(@bnf_swfbco, ''''), @bnf_nombco, 1)
--end


--



--/****** Object:  StoredProcedure [dbo].[cambios_mift_recurrencia_02_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_recurrencia_02_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_recurrencia_02_MS]
--

--CREATE procedure [dbo].[cambios_mift_recurrencia_02_MS]
--(
-- @opcion        tinyint,
-- @ord_rut       varchar(20), 
-- @ord_cuenta    varchar(20),
-- @bnf_swfbco    varchar(40),
-- @bnf_cuenta    varchar(40),
-- @moneda        varchar(03),
-- @monto         numeric(18,2)
--)
--as
--begin
--declare @indicador_mift varchar(02),
--        @monto_minimo   numeric(18,2),
--        @cantidad       int,
--        @retorno        smallint,
--        @tc_observado1  numeric(12,2),
--        @tc_observado2  numeric(12,2),
--        @ord_rut2       varchar(12),
--        @ind_contrato   tinyint
----exec CAMBIOS_MIFT_RECURRENCIA_02 1, 92040000,'36228111','FW031201467','2000109935692','USD',500000
----exec CAMBIOS_MIFT_RECURRENCIA_02 1, 92040000, Null,      Null,        NULL,             USD, 500000
----exec CAMBIOS_MIFT_RECURRENCIA_02 1, 46669924, '1010701925',      Null,        '1',             USD, 125
----exec CAMBIOS_MIFT_RECURRENCIA_02 1, 466700215, Null,      Null,        '1',             USD, 12548744
--   select @retorno = 0
--   if @ord_rut is null
--      select @ord_rut = 0
--   if @ord_cuenta is null 
--      select @ord_cuenta = ' '
--   select @ord_rut2 = right('0000000000'+convert(varchar,@ord_rut)+ dbo.DigitoVerificador(@ord_rut),10)
--   select @ord_rut2 = rutcli
--      from CAMBIOS_MIFT_CONTRATOS
--      where accnum = @ord_cuenta
--   if exists (SELECT TOP 1 1 from CAMBIOS_GRAL_CLIENTES where accnum = @ord_cuenta and len(rutcli)>2)
--      select @ord_rut = rutcli
--        from cambios_gral_clientes
--        where accnum = @ord_cuenta
--          and len(rutcli)>2
--   else
--       if exists (SELECT TOP 1 1 from CAMBIOS_MIFT_CONTRATOS where accnum = @ord_cuenta and len(rutcli)>2)
--          select @ord_rut = rutcli
--            from CAMBIOS_MIFT_CONTRATOS
--            where accnum = @ord_cuenta
--              and len(rutcli)>2
--   select @ind_contrato = 0
--   if exists (SELECT TOP 1 1 from CAMBIOS_MIFT_CONTRATOS where rutcli = @ord_rut and accnum = @ord_cuenta)
--   begin
--      select @indicador_mift = indicador_mift
--         from CAMBIOS_MIFT_CONTRATOS
--         where rutcli = @ord_rut
--           and accnum = @ord_cuenta 
--      if @@rowcount > 0
--        select @ind_contrato = 1
--  end
-- else
--  if exists (SELECT TOP 1 1 from CAMBIOS_MIFT_CONTRATOS where accnum = @ord_cuenta)
--  begin
--     select @indicador_mift = indicador_mift
--        from CAMBIOS_MIFT_CONTRATOS
--        where accnum = @ord_cuenta 
--      if @@rowcount > 0
--        select @ind_contrato = 1
--  end
-- else
--  begin
--     select @indicador_mift = indicador_mift
--        from CAMBIOS_MIFT_CONTRATOS
--        where rutcli    = @ord_rut
--      if @@rowcount > 0
--        select @ind_contrato = 1
--  end
-- if @bnf_cuenta is not null
--    select @cantidad = count(1)
--      from CAMBIOS_MIFT_RECURRENCIA
--      where (ord_rut    = @ord_rut
--        or ord_cuenta  = @ord_cuenta)
--        and bnf_cuenta = isnull(@bnf_cuenta, '''')
--        and bnf_swfbco = isnull(@bnf_swfbco, '''')
-- if @cantidad > 0
--   select @retorno =  2 -- Aprobar : con contrato Mift, Monto mayor al minimo y con Recurrencia
-- else        
--   select @retorno = -1 -- Rechazar : con contrato Mift, Monto mayor al minimo y sin Recurrencia
---- if @ind_contrato = 0
----   select @retorno = -1
-- select @retorno retorno
-- return
--end

--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_recurrencia_01b_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_recurrencia_01b_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_recurrencia_01b_MS]
--

--CREATE procedure [dbo].[cambios_mift_recurrencia_01b_MS]
--(@ord_rut_aux   bigint, 
-- @ord_cuenta    varchar(20),
-- @bnf_swfbco    varchar(40),
-- @bnf_cuenta    varchar(40),
-- @bnf_nombre    varchar(120),
-- @moneda        varchar(03),
-- @monto         numeric(18,2)
--)
--as
--begin
--declare @nro_reg  bigint,
--        @ord_rut  varchar(20),
--        @monto_minimo   numeric(18,2),
--        @retorno        smallint,
--        @tc_observado1  numeric(12,2),
--        @tc_observado2  numeric(12,2),
--        @sw             tinyint
--   select @ord_rut = right('0000000000'+convert(varchar,@ord_rut_aux)+ dbo.DigitoVerificador_MS(@ord_rut_aux),10)
--   if @ord_rut_aux is null
--      select @ord_rut = null
--   if @ord_rut = 0
--      select @ord_rut = null
--   if @ord_cuenta = '0'
--      select @ord_cuenta = ''''
--   select @tc_observado1 = b.VMDOBS
--      from cambios_mift_parametros a, cambios_gral_tipo_cambio b
--      where  a.MONEDA = b.MNDSWF COLLATE DATABASE_DEFAULT
--   if @tc_observado1 = 0
--      select @tc_observado1 = (b.VMDMBC + b.VMDMBV)/2
--         from cambios_mift_parametros a, cambios_gral_tipo_cambio b
--         where a.MONEDA = b.MNDSWF COLLATE DATABASE_DEFAULT
--   if @tc_observado1 is not null
--      select @monto_minimo = round(MONTO_MINIMO * @tc_observado1,0)
--         from cambios_mift_parametros
--   select @tc_observado2 = VMDOBS
--      from cambios_gral_tipo_cambio
--      where MNDSWF = @moneda
--   if @tc_observado2 = 0
--      select @tc_observado2 = (VMDMBC + VMDMBV)/2
--         from cambios_gral_tipo_cambio
--         where MNDSWF = @moneda
--   if @tc_observado2 is not null
--      select @monto = round(@monto * @tc_observado2,0)
--   if @monto >= @monto_minimo 
--      select @retorno = 1
--     else 
--      select @retorno = 0
--   select @sw = 0
--   if @bnf_cuenta is null select @bnf_cuenta = ''''
--   if @bnf_nombre is null select @bnf_nombre = ''''
--   if @bnf_swfbco is null select @bnf_swfbco = ''''
--   if exists (SELECT TOP 1 1 from cambios_gral_clientes where accnum = @ord_cuenta and len(rutcli)>2)
--      select @ord_rut = rutcli
--        from cambios_gral_clientes
--        where accnum = @ord_cuenta
--          and len(rutcli)>2
--   if exists (SELECT TOP 1 1 from cambios_mift_recurrencia where (ord_rut = @ord_rut or ord_cuenta  = @ord_cuenta) and bnf_cuenta like '%'+@bnf_cuenta+'%' and bnf_nombre like '%'+@bnf_nombre+'%' and bnf_swfbco like '%'+@bnf_swfbco+'%' and @sw = 0)
--   begin
--      select @sw = 1
--      if @ord_rut in ('''',' ') 
--         select @ord_rut = '0'
--      select ' ' ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia 
--         where ord_rut    = @ord_rut
--           and not isnull(ord_rut,'''') = '''' 
--           and isnull(bnf_cuenta,'''') like '%'+isnull(@bnf_cuenta,'''')+'%'
--           and isnull(bnf_nombre,'''') like '%'+isnull(@bnf_nombre,'''')+'%'
--           and isnull(bnf_swfbco,'''') like '%'+isnull(@bnf_swfbco,'''')+'%'
--      UNION
--      select ' ' ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia 
--         where ord_cuenta  = @ord_cuenta
--           and not isnull(ord_cuenta,'''')  = ''''
--           and isnull(bnf_cuenta,'''') like '%'+isnull(@bnf_cuenta,'''')+'%'
--           and isnull(bnf_nombre,'''') like '%'+isnull(@bnf_nombre,'''')+'%'
--           and isnull(bnf_swfbco,'''') like '%'+isnull(@bnf_swfbco,'''')+'%'
--      order by 6, 5, 4 desc
--   end 
--/*
--   if @ord_rut is not null and @ord_cuenta is not null and @bnf_cuenta is not null and @bnf_swfbco is not null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--  cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--            @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_rut    = @ord_rut 
--           and ord_cuenta = @ord_cuenta 
--           and bnf_swfbco = @bnf_swfbco
--           and bnf_cuenta = @bnf_cuenta
--      order by 6, 5, 4 desc
--   end
--/*
--   if @ord_rut is not null and @ord_cuenta is not null and @bnf_cuenta is not null and @bnf_swfbco is null and @bnf_nombre is null and @sw = 0
--   begin
--      select @sw = 1
--      select ord_rut,
--             ord_cuenta,
--             ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_rut    = @ord_rut 
--           and ord_cuenta = @ord_cuenta 
--           and bnf_cuenta = @bnf_cuenta
--      order by 6, 5, 4 desc
--   end
--   if @ord_rut is null and @ord_cuenta is not null and @bnf_cuenta is not null and @bnf_swfbco is null and @bnf_nombre is null  and @sw = 0
--   begin
--      select @sw = 1
--      select ord_rut,
--             ord_cuenta,
--             ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta = @ord_cuenta 
--           and bnf_cuenta = @bnf_cuenta
--      order by 6, 5, 4 desc
--   end
--*/
--   if @ord_rut is not null and @ord_cuenta is null and @bnf_cuenta is not null and @bnf_swfbco is null and @bnf_nombre is null  and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_rut    = @ord_rut
--           and bnf_cuenta = @bnf_cuenta
--      order by 6, 5, 4 desc
--   end 
--   if @ord_cuenta is not null and @bnf_cuenta is not null and @bnf_nombre is not null  and @bnf_swfbco is not null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--           bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 retorno
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_cuenta like @bnf_cuenta+'%'
--           and bnf_nombre like '%'+@bnf_nombre+'%'
--           and bnf_swfbco like @bnf_swfbco+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_cuenta is not null and @bnf_cuenta is not null and @bnf_nombre is not null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--      ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 retorno
--@retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_cuenta like @bnf_cuenta+'%'
--           and bnf_nombre like '%'+@bnf_nombre+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_cuenta is not null and @bnf_cuenta is not null and @bnf_swfbco is not null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--     cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 retorno
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_cuenta like @bnf_cuenta+'%'
--           and bnf_swfbco like @bnf_swfbco+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_cuenta is not null and @bnf_cuenta is not null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 retorno
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_cuenta like @bnf_cuenta+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_cuenta is not null and @bnf_nombre is not null  and @bnf_swfbco is not null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 retorno
--         @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_nombre like '%'+@bnf_nombre+'%'
--           and bnf_swfbco like @bnf_swfbco+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_cuenta is not null and @bnf_swfbco is not null and @bnf_nombre is null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 Retorno
--             @retorno retorno 
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_swfbco like @bnf_swfbco+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_cuenta is not null and @bnf_nombre is not null  and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,--0 retorno
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta  = @ord_cuenta
--           and bnf_nombre like '%'+@bnf_nombre+'%'
--      order by 6, 5, 4 desc
--   end
--   if @ord_rut is not null and @ord_cuenta is not null and @bnf_cuenta is null and @bnf_swfbco is null and @bnf_nombre is null  and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_rut    = @ord_rut
--           and ord_cuenta = @ord_cuenta
--      order by 6, 5, 4 desc
--   end 
--   if @ord_rut is null and @ord_cuenta is not null and @bnf_cuenta is null and @bnf_swfbco is null and @bnf_nombre is null  and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_cuenta = @ord_cuenta 
--      order by 6, 5, 4 desc
--   end
--   if @ord_rut is not null and @ord_cuenta is not null and @bnf_cuenta is null and @bnf_swfbco is null and @bnf_nombre is null  and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--     @retorno retorno
--         from cambios_mift_recurrencia
--         where ord_rut = @ord_rut
--      order by 6, 5, 4 desc
--   end
--   if @ord_rut is null and @ord_cuenta is null and @bnf_cuenta is not null and @bnf_swfbco is null and @bnf_nombre is null and @sw = 0
--   begin
--      select @sw = 1
--      select distinct ' 'ord_rut,
--             ' ' ord_cuenta,
--             ' ' ord_nombre,
--             cantidad,
--             bnf_cuenta,
--             bnf_nombre,
--             bnf_swbcoint,
--             bnf_swfbco,
--             bnf_nombco,
--             @retorno retorno
--         from cambios_mift_recurrencia
--         where bnf_cuenta = @bnf_cuenta
--      order by 6, 5, 4 desc
--   end
--*/
--   if @sw = 0
--      select top 0 
--             @ord_rut_aux ord_rut, 
--             @ord_cuenta  ord_cuenta,
--             ' '          ord_nombre,
--             0            cantidad,
--             @bnf_cuenta  bnf_cuenta,
--             @bnf_nombre  bnf_nombre,
--             ' '      bnf_swbcoint,
--             @bnf_swfbco  bnf_swfbco,
--             ' '          bnf_nombco,
--             @retorno    retorno
--   return
--end



--



--/****** Object:  StoredProcedure [dbo].[cambios_mift_mesa_cvd_00b_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_mesa_cvd_00b_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_mesa_cvd_00b_MS]
--

--CREATE procedure [dbo].[cambios_mift_mesa_cvd_00b_MS] (
--    @opcion      smallint,
--    @rut_cliente bigint,
--    @accnum      varchar(50) = null,
--    @moneda      char(03), 
--    @monto       numeric(18,2)
--)
--as
--begin
--declare @moneda_cod smallint,
--        @buscar as char(1)

--select @buscar = 'S'

--	IF 1=0 BEGIN
--        SET FMTONLY OFF
--    END

--create table #tmpMesaCVDb (
--    chk varchar(1) null ,
--	fecha varchar(10) null,	
--	monto varchar(13) null,
--	precio varchar(13) null,
--	mdn_com varchar(6) null,
--	mnd_vta varchar(6) null,
--	tipo varchar(20) null,
--	mtoPesos varchar(20) null,
--	mtoUSD varchar(20) null,
--	FVALOR varchar(100) null,
--	idComex varchar(12),
--	estado int null
--)


--   if isnumeric(left(@accnum,len(@accnum)-1)) = 1
--   begin
--       select @rut_cliente = left(@accnum,len(@accnum)-1)
--			if exists (select top 1 1 from cambios_revisor_mesa_cvd where rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente))
--          select @buscar = 'N'
--   end
--   if @buscar = 'S'
--       if exists (select top 1 1 from cambios_gral_clientes where accnum = @accnum  and exists (SELECT TOP 1 1 from cambios_revisor_mesa_cvd where rut_cliente = rut))
--        begin
--           select @buscar = 'N'
--           select @rut_cliente = rut from cambios_gral_clientes where accnum = @accnum
--        end
--		  if  @buscar = 'N'
--			begin
--				select @moneda_cod = mnd_mndcod from cambios_gral_moneda where mnd_mndswf = @moneda

--		   if @moneda_cod is null
--			  select @moneda_cod = 11

--		   if exists (SELECT TOP 1 1 from cambios_revisor_mesa_cvd where rut_cliente = @rut_cliente) -- and (moneda_com = @moneda_cod or moneda_vta = @moneda_cod))
--			 begin
				
--				insert into #tmpMesaCVDb

--                select isnull(vigente,'''') 'chk',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio,
--                  b.mnd_mndswf mcom,
--                  'CLP'  mvta, --c.mnd_mndswf moneda_vta,
--                  'Compra' tipo,
--                  round(a.monto_comex*a.precio_cliente,0) Mto$,
--                  0 MtoUsd,
--                  case 
----                     when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                     when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                     when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                        then ':Segun Carta'
--                     else ':Segun Carta'
--                  end 'FVALOR !!!',
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
    
--          where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com = a.moneda_vta
--                and a.id_tipo_transaccion = 1
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
----                and (a.moneda_com = @moneda_cod or a.moneda_vta = @moneda_cod)
--          UNION ALL 
--           select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,

--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  'CLP' moneda_com, --b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Venta' tipo,
--                  round(a.monto_comex*a.precio_cliente,0) Mto_Pesos,
--                  0 Mto_USD,
--                 case 
----                     when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                     when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----   when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                        then ':Segun Carta'
--                     else ':Segun Carta'
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
     
--         where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com = a.moneda_vta
--                and a.id_tipo_transaccion = 2
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
----                and (a.moneda_com = @moneda_cod or a.moneda_vta = @moneda_cod)
--          UNION ALL 
--           select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha, 
--                 replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf mnd_com,
--                  'CLP'  mnd_vta, --c.mnd_mndswf moneda_vta,
--                  'Compra' tipo,
--                  round(a.monto_comex*a.precio_cliente,0) Mto_Pesos,
--                  0 Mto_USD,
--                  case 
----                     when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                     when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                     when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                        then ':Segun Carta'
--                     else ':Segun Carta'
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--                and a.moneda_vta  = 1
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
----                and (a.moneda_com = @moneda_cod or a.moneda_vta = @moneda_cod)
--          UNION ALL 
--           select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  'CLP' moneda_com, --b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Venta' tipo,
--                  round(a.monto_comex*a.precio_cliente,0) Mto_Pesos,
--                  0 Mto_USD,
--                  case 
----                     when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                     when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                     when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                        then ':Segun Carta'
--                     else ':Segun Carta'
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1 
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_vta !=  1 --a.moneda_vta
--                and a.moneda_com  = 1
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--          UNION ALL 
--            select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  --revisar separador de miles
--                  CONVERT(varchar, convert(money, a.monto_comex), 1) monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'A
--rbitraje1' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex/a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--                  else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--			                where a.rut_cliente = 
--				dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--                and a.moneda_vta != 1
--                and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta != 11
--                and a.precio_cliente <= 1
--                and exists (SELECT TOP 1 1 from cambios_revisor_observado d
--                               where a.moneda_vta = d.codmon
--                                 and d.
--paridad <= 1)
--            UNION ALL
--            select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje2' tipo,
--                  0 Mto_Pesos, 
--                 round(a.monto_comex*a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when 
--					a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--                  else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
           
--     and a.moneda_com != 1
--                and a.moneda_vta != 1
--                and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta != 11
      
--          and a.precio_cliente <= 1
--                and exists (SELECT TOP 1 1 from cambios_revisor_observado d
--                               where a.moneda_vta = d.codmon
--                                 and d.paridad > 1)
--            UNION ALL
--            select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje3' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex*a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--                  else ':Segun Carta'      
--             end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
  
--                end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--                and a.moneda_vta != 1
--                and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta != 11
--                and a.precio_cliente > 1               
-- and exists (SELECT TOP 1 1 from cambios_revisor_observado d
--                               where a.moneda_vta = d.codmon
--                                 and d.paridad <= 1)
--            UNION ALL
--            select isnull(vigente,'''') 'check',                 
-- --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje4' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex/a.precio_cliente,2) Mto_USD,
--                  case 
----          when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--                  else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--     and a.moneda_vta != 1
--                and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta != 11
--                and a.precio_cliente > 1
--                and exists (SELECT TOP 1 1 from cambios_revisor_observado d
      
--                         where a.moneda_vta = d.codmon
--                                 and d.paridad > 1)
--          UNION ALL 
--          select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje5' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex*a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--                  else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--                and a.moneda_vta != 1
--                and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
   
--             and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta = 11
--                and a.precio_cliente <= 1
--                and exists (SELECT TOP 1 1 from cambios_revisor_observado d
--                               where a.moneda_com = d.codmon

--                                 and d.paridad <= 1)
--          UNION ALL 
--          select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert
--(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje6' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex*a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)

----  then ':Segun Carta'
--                  else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--                and a.moneda_vta != 1
--             and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta = 11
--                and a.precio_cliente <= 1
--                and exists (SELECT TOP 1 1 from cambios_revisor_observado d
--                               where a.moneda_com = d.codmon
--                                 and d.paridad > 1)
--          UNION ALL 
 
--         select isnull(vigente,'''') 'check',
--                  --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje7' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex*a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
----                  when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--                  else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
                   
--  else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
            
--    and a.moneda_vta != 1
--                and a.moneda_com != a.moneda_vta
--                and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta = 11
--                and a.precio_cliente > 1
     
--           and exists (SELECT TOP 1 1 from cambios_revisor_observado d
--                               where a.moneda_com = d.codmon
--                                 and d.paridad <= 1)
--          UNION ALL 
--          select isnull(vigente,'''') 'check',
          
--        --a.rut_cliente,
--                  convert(char(10),a.fecha_transaccion,103) fecha,
--                  replace(convert(varchar, a.monto_comex), '.', ',') monto_comex,
--                  replace(convert(varchar, a.precio_cliente), '.', ',') precio_cliente,
--                  b.mnd_mndswf moneda_com,
--                  c.mnd_mndswf moneda_vta,
--                  'Arbitraje8' tipo,
--                  0 Mto_Pesos,
--                  round(a.monto_comex/a.precio_cliente,2) Mto_USD,
--                  case 
----                  when a.fecha_valor >= convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (4,126,127)
--                  when a.tipo_operacion in (4,126,127) then ':'+convert(char(10),fecha_valor,103)
---- 				 when a.fecha_valor > convert(datetime,convert(char(08),a.fecha_transaccion,112),112) and a.tipo_operacion in (3,6,128,129)
----                     then ':Segun Carta'
--               else ':Segun Carta' 
--                  end fvalor,
--                  id_comex,
--                  case
--                     when convert(datetime,convert(char(08),a.fecha_transaccion,112),112) < dbo.habil_ant_MS(convert(char(08),getdate(),112),2) then -1
--                     else 1
--                  end estado
--              from  cambios_revisor_mesa_cvd a, cambios_gral_moneda b, cambios_gral_moneda c
--              where a.rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)
--                and a.moneda_com != 1
--                and a.moneda_vta != 1
--                and a.moneda_com !
--= a.moneda_vta
--         and a.moneda_com = b.mnd_mndcod
--                and a.moneda_vta = c.mnd_mndcod
--                and a.moneda_vta = 11
--                and a.precio_cliente > 1
--                and exists (SELECT TOP 1 1 from cambios_revisor_observado d
  
--                             where a.moneda_com = d.codmon
--                                 and d.paridad > 1)
--           order by id_comex desc, 3 desc --a.fecha_transaccion desc, a.monto_comex desc   
--             end
--    end

--	select * from #tmpMesaCVDb

--	drop table #tmpMesaCVDb
--end

--

--/****** Object:  StoredProcedure [dbo].[cambios_mift_mesa_cvd_00a_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_mesa_cvd_00a_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_mesa_cvd_00a_MS]
--

--CREATE procedure [dbo].[cambios_mift_mesa_cvd_00a_MS] (
--    @opcion      smallint,
--    @rut_cliente bigint,
--    @accnum      varchar(50) = null,
--    @moneda      char(03),
--    @monto       numeric(18,2)
--)
--as
--begin

--declare @moneda_cod  smallint,
--        @buscar as char(1)
--   set @buscar = 'S'
--   if isnumeric(left(@accnum,len(@accnum)-1)) = 1
--    begin
--       select @rut_cliente = left(@accnum,len(@accnum)-1)
--       if exists (select top 1 1 from cambios_revisor_mesa_cvd where rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente))
--          set @buscar = 'N'
--        end
--   if @buscar = 'S'
--       if exists (select top 1 1 from cambios_gral_clientes where accnum = @accnum and exists (SELECT TOP 1 1 from cambios_revisor_mesa_cvd where rut_cliente = rut))
--        begin
--           set @buscar = 'N'
--           select @rut_cliente = rut
--             from cambios_gral_clientes
--             where accnum = @accnum
--        end
--   if @buscar = 'N'
--    begin
--       select @moneda_cod = mnd_mndcod
--          from cambios_gral_moneda
--          where mnd_mndswf = @moneda
--       if @moneda_cod is null
--          set @moneda_cod = 'USD'   
--       if exists (select top 1 1 from cambios_revisor_mesa_cvd where rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente) and monto_comex = @monto and (moneda_com = @moneda_cod or moneda_vta = @moneda_cod))
--        begin

--           select 0 'R',255 'G',0 'B',1 ESTADO, 'Tipo de Cambio' MENSAJE --Green
--           goto select1
--        end
--       if exists (select top 1 1 from cambios_revisor_mesa_cvd where rut_cliente = dbo.cambios_cvd_afp_rut_MS(@rut_cliente)) -- and (moneda_com = @moneda_cod or moneda_vta = @moneda_cod))
--        begin

--           select 255 'R',255 'G',128 'B',1 ESTADO, 'Tipo de Cambio' MENSAJE --Yellow
--            goto select1
--        end
--    end
	

--   select  0 'R',0 'G', 0 'B', 0 ESTADO, 'Tipo de Cambio' MENSAJE

--select1:

--end
--

--/****** Object:  StoredProcedure [dbo].[cambios_mift_mensajes_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_mensajes_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_mensajes_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_mensajes_01_MS]
--(
--@tipo   tinyint,
--@accnum varchar(15)
--)
--as
--begin
--declare @rut bigint
--   if isnumeric(left(@accnum,len(@accnum)-1)) = 1
--       select @rut = left(@accnum,len(@accnum)-1)
--      else
--       select @rut = 0
--   select MENSAJE_01 +' - '+ MENSAJE_02 'MENSAJE'
--      from cambios_mift_mensaje_cliente
--      where RUT = @rut
--end



--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_log_insert_2_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_log_insert_2_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_log_insert_2_MS]
--

--CREATE procedure [dbo].[cambios_mift_log_insert_2_MS]
--(
--    @Fecha_hora     varchar(20)  ,
--    @Fecha          varchar(20)  ,
--    @Rut            varchar(12)  ,
--    @RutDv          varchar(12)  ,
--    @Cuenta         varchar(20)  ,
--    @NombreClte     varchar(100) ,
--    @Segmento       varchar(100) ,
--    @Ejecutivo      varchar(100) ,
--    @Moneda         varchar(3)   ,
--    @monto          bigint       ,
--    @CuentaBnf      varchar(50)  ,
--    @NombreBnf      varchar(100) ,
--    @BancoInt       varchar(20)  ,
--    @BancoBnf       varchar(20)  ,
--    @Ind_Con_Otros  varchar(2)   ,
--    @Ind_Con_Mift   varchar(2)   ,
--    @Ind_Con_Fax    varchar(2)   ,
--    @Ind_Con_Citi   varchar(2)   ,
--    @Ind_Con_Fax_NY varchar(2)   ,
--    @Ind_Con_Mail   varchar(2)   ,
--    @Txt_Con_Otros  varchar(100) ,
--    @Resultado      varchar(20)  ,
--    @Mensaje        varchar(100) ,
--    @Usuario        varchar(20)  
--)



--as



--begin







--insert into cambios_mift_log 
--(
--    Fecha_hora     ,
--    Fecha          ,
--    Rut            ,
--    RutDv          ,
--    Cuenta         ,
--    NombreClte     ,
--    Segmento       ,
--    Ejecutivo      ,
--    Moneda         ,
--    monto          ,
--    CuentaBnf      ,
--    NombreBnf      ,
--    BancoInt       ,
--    BancoBnf       ,
--    Ind_Con_Otros  ,
--    Ind_Con_Mift   ,
--    Ind_Con_Fax    ,
--    Ind_Con_Citi   ,
--    Ind_Con_Fax_NY ,
--    Ind_Con_Mail   ,
--    Txt_Con_Otros  ,
--    Resultado      ,
--    Mensaje        ,
--    Usuario
--) values



--(



--    convert(datetime, @Fecha_hora,103)     ,



--    convert(datetime, @Fecha, 103)         ,



--    @Rut            ,



--    @RutDv          ,



--    @Cuenta         ,



--    @NombreClte     ,



--    @Segmento       ,



--    @Ejecutivo      ,



--    @Moneda         ,



--    @monto          ,



--    @CuentaBnf      ,



--    @NombreBnf      ,
--    @BancoInt       ,
--    @BancoBnf       ,
--    @Ind_Con_Otros  ,
--    @Ind_Con_Mift   ,
--    @Ind_Con_Fax    ,
--    @Ind_Con_Citi   ,
--    @Ind_Con_Fax_NY ,
--    @Ind_Con_Mail   ,
--    @Txt_Con_Otros  ,
--    @Resultado      ,
--    @Mensaje        ,
--    @Usuario 
--)
--end

--




--/****** Object:  StoredProcedure [dbo].[cambios_mift_cuenta_00_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_cuenta_00_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_cuenta_00_MS]
--

--CREATE procedure [dbo].[cambios_mift_cuenta_00_MS] 
-- ( @opcion tinyint, 
--   @rut bigint,
--   @cuenta varchar(20)
-- )
--as
--begin
--declare @rut2     varchar(20),

--        @rut3     varchar(20)
		
--/*

--exec CAMBIOS_MIFT_CUENTA_00 1,969662507, null -- 36228111

--exec CAMBIOS_MIFT_CUENTA_00 1,null, '50005228402'

--exec CAMBIOS_MIFT_CUENTA_00 1,92040000, '36228111'

--*/
--   if @cuenta = ''''

--      select cuenta = null

--   select @rut2 =  convert(varchar,@rut)+dbo.DigitoVerificador(@rut)

--   select @rut3 =  right('0000000000'+convert(varchar,@rut)+dbo.DigitoVerificador(@rut),10)



--   select 0 rut, @cuenta cuenta

--   return

--/*

--   if exists (SELECT TOP 1 1 from CAMBIOS_MIFT_RECURRENCIA where ord_rut = right('0000000000'+@cuenta,10))

--   begin

--      select @cuenta = isnull(ord_cuenta,'''')

--         from CAMBIOS_MIFT_RECURRENCIA

--         where ord_rut = right('0000000000'+@cuenta,10)

         

--      select top 1

--             @rut  rut,

--             a.accnum cuenta

--         from CAMBIOS_GRAL_CLIENTES a

--         where a.accnum = @cuenta

--   end

--  else

--   if exists (SELECT TOP 1 1 from CAMBIOS_MIFT_CONTRATOS where rut = @rut2 and accnum = @cuenta)

--         select distinct

--                left(rut,len(rut)-1) rut,

--                isnull(accnum,' ') cuenta

--            from CAMBIOS_MIFT_CONTRATOS 

--            where rut    = @rut2

--              and accnum = @cuenta

--   else

--    if exists (SELECT TOP 1 1 from CAMBIOS_MIFT_CONTRATOS where rut = @rut2)

--       select distinct

--              left(rut,len(rut)-1) rut,

--              isnull(cuenta,' ') cuenta

--          from CAMBIOS_MIFT_CONTRATOS 

--          where accnum   = @rut2

--            and prodtype = 0

--     else

--           select distinct

--                  left(rut,len(rut)-1) rut,

--                  isnull(cuenta,' ') cuenta

--              from CAMBIOS_MIFT_CONTRATOS 

--              where accnum    = @cuenta

--                and prodtype != 0



--*/





--/*



--   if @rut is not null and @cuenta is not null

--    begin

--         select distinct

--                left(rut,len(rut)-1) rut,

--                isnull(accnum,' ') cuenta

--            from CAMBIOS_MIFT_CONTRATOS 

--            where rut    = @rut2

--              and accnum = @cuenta

--    end

--   else

--   if @rut is not null and @cuenta is null

--    begin

--         select distinct

--                left(rut,len(rut)-1) rut,

--                isnull(cuenta,' ') cuenta

--            from CAMBIOS_MIFT_CONTRATOS 

--            where rut = @rut2

--    end

--   else

--    if @rut is null and @cuenta is not null

--     begin

--         select distinct

--                left(rut,len(rut)-1) rut,

--                isnull(cuenta,' ') cuenta

--            from CAMBIOS_MIFT_CONTRATOS 

--            where accnum = @cuenta

--     end

--    else

--     begin

--       if @rut is not null and @cuenta is not null

--          select @rut rut,

--                 @cuenta cuenta



--     end

--*/           

--end

--



--/****** Object:  StoredProcedure [dbo].[cambios_mift_contratos_00_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_contratos_00_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_contratos_00_MS]
--

--CREATE  procedure [dbo].[cambios_mift_contratos_00_MS] 
--(@opcion tinyint, @rut bigint, @cuenta varchar(20) )
--as
--begin

--declare @contrato_fax_local       varchar(120),
--        @contrato_citi_offshore   varchar(120),
--        @contrato_mift            varchar(120),
--        @contrato_fax_NY_Londres  varchar(120),
--        @contrato_otros           varchar(250),
--        @contrato_anexo_mail      varchar(250),
--        @indicador_fax_local      varchar(002),
--        @indicador_citi_offshore  varchar(002),
--        @indicador_mift           varchar(002),
--        @indicador_fax_NY_Londres varchar(002),
--        @indicador_otros          varchar(002),
--        @indicador_anexo_mail     varchar(002),
--        @result_contrato          varchar(03),
--        @rut2                     varchar(20),
--        @rut3                     varchar(20)


----   if @rut is null
----      select @rut = 0
--   if @cuenta is null or @cuenta = '0'    
--      select @cuenta = ''''


--   select @rut2 =  left(convert(varchar,@rut),8)


--   select @result_contrato = 'ORI'

      
--   if exists (SELECT TOP 1 1 from cambios_mift_contratos 
--                 where prodtype = 0
--                   and rut = @rut2 
--                   and (indicador_fax_local = 'SI' or indicador_citi_offshore = 'SI' or indicador_fax_NY_Londres = 'SI'))
--   begin
----      select 'paso 1'
--      select @result_contrato = 'OK'
--   end
      
--   if exists (SELECT TOP 1 1 from cambios_mift_contratos 
--                 where prodtype != 0
--                   and accnum = @cuenta
--                   and (indicador_fax_local = 'SI' or indicador_citi_offshore = 'SI' or indicador_fax_NY_Londres = 'SI'))
--   begin
----      select 'paso 2'
--      select @result_contrato = 'OK'
--   end
   
--   if exists (SELECT TOP 1 1 from cambios_mift_contratos 
--                 where accnum = @cuenta
--                   and (indicador_fax_local = 'SI' or indicador_citi_offshore = 'SI' or indicador_fax_NY_Londres = 'SI'))
--   begin
----      select 'paso 3'
--      select @result_contrato = 'OK'
--   end

   
--   select @rut3 = rut from cambios_gral_clientes where accnum = @cuenta

--   if exists (SELECT TOP 1 1 from sce_mcli_cox where MCX_RUT = convert(bigint, @rut3) 
--                                           and (MCX_NCLI like '%*SOCIEDAD%' or 
--                                                MCX_NCLI like '%(SOCIEDAD%' or 
--                                                MCX_NCLI like '%DISUE%')
--                                           and not MCX_NCLI like '%QUIEBRA%')
--   begin
----      select 'paso 4'
--      select @result_contrato = 'DIS'
--   end


--/*
--   if exists (SELECT TOP 1 1 from cambios_mift_contratos 
--                 where rut = @rut2 
--                   and cuenta = @cuenta
--                   and indicador_mift = 'SI')
--      select @result_contrato = 'OK'

--   if exists (SELECT TOP 1 1 from cambios_mift_contratos 
--                 where rut = @rut2 
--                   and indicador_mift = 'SI')
--      select @result_contrato = 'OK'
--*/  
      
----if exists (SELECT TOP 1 1 from cambios_mift_contratos where isnull(@rut2,0) != 0 and isnull(rut,0) = isnull(@rut2,0))


--if isnull(@rut,0) != 0
--begin
-- if exists (SELECT TOP 1 1 from cambios_mift_contratos where rut = @rut2)
--   select distinct 
--          indicador_fax_local,       -- fax_local
--          case indicador_fax_local
--            when 'SI' then 'Contrato via Fax Firmado'
--            when 'NO' then 'SIN CONTRATO' 
--            else 'SIN CONTRATO' 
--          end contrato_fax_local,
--          indicador_citi_offshore,   -- citi_offshore
--          case indicador_citi_offshore
--             when 'SI' then 'Contingency Means of Communication'
--             when 'NO' then 'SIN CONTRATO' 
--             else 'SIN CONTRATO' 
--          end contrato_citi_offshore,
--          indicador_mift,            -- MIFT
--          case indicador_mift
--             when 'SI' then 'Contrato CallBack OK'
--             when 'NO' then 'SIN CONTRATO' 
--             else 'SIN CONTRATO' 
--          end contrato_mift,
--          indicador_fax_NY_Londres,  -- FAX NY-Londres
--          case indicador_fax_NY_Londres
--             when 'SI' then 'Instrucciones Via Fax Cta. N.YORK'
--             when 'NO' then 'SIN CONTRATO' 
--             else 'SIN CONTRATO' 
--          end contrato_fax_NY_Londres,
--          indicador_otros,           -- Otros
--          contrato_otros,
--          indicador_anexo_mail,      -- Anexo Mail
--          contrato_anexo_mail,
--          @result_contrato result_contrato
--   from cambios_mift_contratos
--   where rut = @rut2
----     and (indicador_fax_local = 'SI' or indicador_citi_offshore = 'SI' or indicador_fax_NY_Londres = 'SI')
-- end   
--else
--begin
---- if exists (SELECT TOP 1 1 from cambios_mift_contratos where isnull(@cuenta,'''') != '''' and isnull(accnum,'''') = isnull(@cuenta,''''))
-- if exists (SELECT TOP 1 1 from cambios_mift_contratos where accnum = @cuenta)
--   select distinct 
--          indicador_fax_local,       -- fax_local
--          case indicador_fax_local
--            when 'SI' then 'Contrato Via Fax Firmado'
--            when 'NO' then 'SIN CONTRATO' 
--            else 'SIN CONTRATO' 
--          end contrato_fax_local,
--          indicador_citi_offshore,   -- citi_offshore
--          case indicador_citi_offshore
--             when 'SI' then 'Contingency Means of Communication'
--             when 'NO' then 'SIN CONTRATO' 
--             else 'SIN CONTRATO' 
--          end contrato_citi_offshore,
--          indicador_mift,            -- MIFT
--          case indicador_mift
--             when 'SI' then 'Contrato CallBack OK'
--             when 'NO' then 'SIN CONTRATO' 
--             else 'SIN CONTRATO' 
--          end contrato_mift,
--          indicador_fax_NY_Londres,  -- FAX NY-Londres
--          case indicador_fax_NY_Londres
--             when 'SI' then 'Instrucciones Via Fax Cta. N.YORK'
--             when 'NO' then 'SIN CONTRATO' 
--             else 'SIN CONTRATO' 
--          end contrato_fax_NY_Londres,
--          indicador_otros,           -- Otros
--          contrato_otros,
--          indicador_anexo_mail,      -- Anexo Mail
--          contrato_anexo_mail,
--          @result_contrato result_contrato
--   from cambios_mift_contratos
--   where accnum = @cuenta
----     and (indicador_fax_local = 'SI' or indicador_citi_offshore = 'SI' or indicador_fax_NY_Londres = 'SI')
--end

--if @@rowcount = 0
--   select null indicador_fax_local,
--          'SIN CONTRATO' contrato_fax_local,
--          null indicador_citi_offshore,
--          'SIN CONTRATO' contrato_citi_offshore,
--          null indicador_mift,
--          'SIN CONTRATO' contrato_mift,
--          null indicador_fax_NY_Londres,
--          'SIN CONTRATO' contrato_fax_NY_Londres,
--          null indicador_otros,           -- Otros
--          null contrato_otros,
--          null indicador_anexo_mail,      -- Anexo Mail
--          null contrato_anexo_mail,
--          'ORI' result_contrato

--return 

--end

--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_cliente_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_cliente_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_cliente_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_cliente_01_MS](
--    @opcion tinyint, 
--    @nombre varchar(60)
--)
--as
--begin
--    set nocount on
--    declare @nombre2 varchar(60)
--    set @nombre2 = '%' + replace(@nombre, '''', '%') + '%'
--	IF 1=0 BEGIN
--        SET FMTONLY OFF
--    END
--    create table #tmp(
--        accnum varchar(20) null,
--        nombre varchar(100) null
--    )
--    if @opcion = 1
--    if exists (SELECT TOP 1 1 from cambios_gral_clientes where nomcli = @nombre)
--    begin
--        set @opcion = 2
--        insert into #tmp
--        select max(accnum) accnum, nomcli nombre
--          from cambios_gral_clientes
--         where nomcli = @nombre
--         group by nomcli
--    end
--    if @opcion = 1
--    if exists (SELECT TOP 1 1 from cambios_gral_clientes where nomcli like @nombre2)
--    begin
--        set @opcion = 2
--        insert into #tmp
--        select max(accnum) accnum, nomcli nombre
--          from cambios_gral_clientes
--         where nomcli like @nombre2
--         group by nomcli
--    end
--    if @opcion = 2
--    if exists (SELECT TOP 1 1 from cambios_mift_contratos where nomcli = @nombre)
--    begin
--        set @opcion = null
--        insert into #tmp
--        select max(accnum) accnum, nomcli nombre
--          from cambios_mift_contratos a
--         where nomcli = @nombre
--           and not exists(SELECT TOP 1 1
--                            from #tmp b
--                           where b.nombre = a.nomcli)
--         group by nomcli
--    end
--    if @opcion = 2
--    if exists (SELECT TOP 1 1 from cambios_mift_contratos where nomcli like @nombre2)
--    begin
--        set @opcion = null
--        insert into #tmp
--        select max(accnum) accnum, nomcli nombre
--          from cambios_mift_contratos a
--         where nomcli like @nombre2
--           and not exists(SELECT TOP 1 1
--                            from #tmp b
--                           where b.nombre = a.nomcli)
--         group by nomcli
--    end
--    /*
--    if @opcion > 0
--    if exists (SELECT TOP 1 1 from SCE_MCLI_COX where replace(mcx_ncli,'/',' ') = @nombre)
--    begin
--        set @opcion = null
--        insert into #tmp
--        select max(convert(varchar, MCX_RUT) + MCX_DV) accnum, replace(upper(mcx_ncli),'/',' ') nombre
--          from SCE_MCLI_COX
--         where replace(mcx_ncli,'/',' ') = @nombre
--         group by replace(upper(mcx_ncli),'/',' ')
--    end
--    if @opcion > 0
--    if exists (SELECT TOP 1 1 from SCE_MCLI_COX where replace(mcx_ncli,'/',' ') like @nombre2)
--    begin
--        set @opcion = null
--        insert into #tmp
--        select max(convert(varchar, MCX_RUT) + MCX_DV) accnum, replace(upper(mcx_ncli),'/',' ') nombre
--          from SCE_MCLI_COX
--         where replace(mcx_ncli,'/',' ') like @nombre2
--         group by replace(upper(mcx_ncli),'/',' ')
--    end
--    */
--    select * from #tmp
--    order by 2, 1
--    drop table #tmp
--end
--



--/****** Object:  StoredProcedure [dbo].[cambios_mift_cliente_00_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_cliente_00_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_cliente_00_MS]
--

--CREATE procedure [dbo].[cambios_mift_cliente_00_MS] (@opcion tinyint, @rut bigint, @cuenta varchar(20))
--as
--begin
--declare @rut2                     varchar(20),
--        @rut3                     varchar(20),
--        @segmento                 varchar(120),
--        @ejecutivo                varchar(60),
--        @nombre                   varchar(60),
--        @est_recurrencia          tinyint

---- exec CAMBIOS_MIFT_CLIENTE_00 1,null,'0784820602'
---- exec CAMBIOS_MIFT_CLIENTE_00 1,92040000,null
---- exec CAMBIOS_MIFT_CLIENTE_00 1,null, '100009'


-- --exec CAMBIOS_MIFT_CLIENTE_00 1,null

--select @rut2 = convert(varchar,@rut)+'-'+dbo.DigitoVerificador_MS(@rut)

--select @rut3 = right('0000000000'+convert(varchar,@rut)+ dbo.DigitoVerificador_MS(@rut),10)

--select @est_recurrencia = 0

--if exists (SELECT TOP 1 1 from cambios_mift_recurrencia where ord_rut = (select top 1 rutcli from cambios_gral_clientes where accnum = @cuenta order by charindex(@cuenta, rutcli) desc))
--   select @est_recurrencia = 1

--if @rut is null
--   select @rut = 0

--if @rut2 is null
--   select @rut2 = ''''
   
--if @rut3 is null
--   select @rut3 = ''''

   
-- if exists (SELECT TOP 1 1 from cambios_gral_clientes  where accnum = @cuenta)
-- begin
--      select top 1
--             @rut  rut,
--             replace(a.nomcli, '''', '') nombre,--(select top 1 b.nomcli from cambios_gral_clientes b where b.rutcli = @rut3) nombre,
--             isnull(a.cod_ofi,'')+isnull(a.cod_ejc,'') segmento,
--             a.mail_ejc_cmx ejecutivo,
--             @est_recurrencia est_recurrencia
----             a.segmento segmento,
----             a.nom_ejc_cmx ejecutivo
--         from cambios_gral_clientes a
--         where a.accnum = @cuenta
-- end
--else
-- begin
--   if exists (SELECT TOP 1 1 from sce_mcli_cox a where a.MCX_RUT = @rut)
--   begin
--      select @rut rut,
--             replace(replace(upper(isnull(a.MCX_NCLI,'')),'/',' '), '''', '') nombre,
--             isnull(a.MCX_COFI,'')+isnull(a.MCX_EJCT,'') segmento,
--             isnull(a.MCX_MAIL,'') ejecutivo,
--             @est_recurrencia est_recurrencia
----             isnull(a.mcx_nzon,'') segmento,
----             isnull(a.mcx_nejn,'') ejecutivo
--         from sce_mcli_cox a
--         where a.MCX_RUT = @rut
--   end
--  else
--   begin
--    if exists (SELECT TOP 1 1 from cambios_gral_clientes  where rutcli = @rut3)
--     begin
--       select top 1
--            @rut  rut,
--            replace(a.nomcli, '''', '') nombre,--(select top 1 b.nomcli from cambios_gral_clientes b where b.rutcli = @rut3) nombre,
--             isnull(a.cod_ofi,'')+isnull(a.cod_ejc,'') segmento,
--             a.mail_ejc_cmx ejecutivo,
--             @est_recurrencia est_recurrencia
----            a.segmento segmento,
----            a.nom_ejc_cmx ejecutivo
--        from cambios_gral_clientes a
--        where a.rutcli = @rut3
--     end
--    else
--     begin    
--        if exists (SELECT TOP 1 1 from cambios_mift_recurrencia where ord_rut = right('0000000000'+@cuenta,10))
--        begin
--           select @cuenta = isnull(ord_cuenta,'')
--              from cambios_mift_recurrencia
--              where ord_rut = right('0000000000'+@cuenta,10)
--           select top 1
--                  @rut  rut,
--                  replace(a.nomcli, '''', '')nombre,--(select top 1 b.nomcli from cambios_gral_clientes b where b.rutcli = @rut3) nombre,
--                  isnull(a.cod_ofi,'')+isnull(a.cod_ejc,'') segmento,
--                  a.mail_ejc_cmx ejecutivo,
--                  @est_recurrencia est_recurrencia
----                  a.segmento segmento,
----                  a.nom_ejc_cmx ejecutivo
--              from cambios_gral_clientes a
--              where a.accnum = @cuenta
--        end
--       else
--        begin
--            select top 1
--                   @rut        rut,
--                   replace(@nombre, '''', '')   nombre,
--                   @segmento   segmento,
--                   b.ejecutivo,
--                   @est_recurrencia est_recurrencia
--               from cambios_mift_contratos b
--               where b.rut     = @rut
--        end
--     end
--   end
-- end
     
--end

--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_citidoc_duplicados_hora_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_citidoc_duplicados_hora_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_citidoc_duplicados_hora_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_citidoc_duplicados_hora_01_MS]
--as
--begin
--   select convert(char(05),max(hora),114)+' Hrs.'
--      from cambios_tc_input_log
--end
--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_citidoc_duplicados_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_citidoc_duplicados_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_citidoc_duplicados_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_citidoc_duplicados_01_MS]
--(
--@accnum  varchar(30),
--@monto   numeric(18,2) 
--)
--as 
--begin
--declare @rut varchar(12)
--   if exists (SELECT TOP 1 1 from cambios_gral_clientes where accnum = @accnum)
----      select @rut = convert(varchar,rut)
--      select @rut = convert(varchar,rut) + dv
--         from cambios_gral_clientes
----         from cambios_mift_contratos 
--         where accnum = @accnum
--     else 
--      if isnumeric(left(@accnum,len(@accnum)-1)) = 1
--         select @rut = @accnum
--   select 
--          isnull((select convert(char(05),max(hora),114) from cambios_tc_input_log),'') hora,      
--          a.Folder,
--          rtrim(a.Document_Name) document_name,
--          isnull(a.Referencia,'') referencia,
--          a.Monto,
--          a.Document_Type,
--          a.base,
--          a.Scanner,
--          a.Fecha,
--          left(a.Rut,12) rut,
--          left(a.Nombre_Cliente,30) nombre_cliente
--      from cambios_tc_input_new a
--      where a.Rut   = @rut
----        and a.monto = @monto
--        and a.Monto >= @monto-1
--        and a.Monto <= @monto+1
--        and a.Rut is not null
--        and isnull(a.Monto,0) <> 0
--        and a.Document_Name not like '%Copia%'
--        and a.Document_Name not like '%Copy%'
--        and a.Document_Name not like '%Duplicate%'
--        and a.Document_Name not like '%-%'  
--        and a.Nombre_Cliente not like '//'
--        and a.Nombre_Cliente not like '%//%'
--        and a.Rut not like '%MAL%'     
--        and 1 < (select count(1) from cambios_tc_input_new b
--                    where b.Rut   = a.Rut
----                      and b.monto = a.monto
--                      and b.Monto >= a.Monto-1
--                      and b.Monto <= a.Monto+1
--                      and exists (SELECT TOP 1 1 from cambios_tc_input_new c
--                                     where b.Rut   = c.Rut
--                                       and b.Monto = c.Monto
--                                       and b.Document_Name = c.Document_Name
--                                       and c.Document_Name not like '%Copia%'
--                                       and c.Document_Name not like '%Copy%'
--                                       and c.Document_Name not like '%Duplicate%'
--                                       and c.Document_Name not like '%-%'  
--                                       and c.Nombre_Cliente not like '//'
--                                       and c.Nombre_Cliente not like '%//%'
--                                       and c.Rut not like '%MAL%'))
--   order by 1,2,4
--end
--

--/****** Object:  StoredProcedure [dbo].[cambios_mift_citidoc_consulta_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_citidoc_consulta_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_citidoc_consulta_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_citidoc_consulta_01_MS]
--(
--@accnum  varchar(30),
--@monto   numeric(18,2) 
--)
--as 
--begin
--declare @rut varchar(12)
--declare @nombre varchar(100)
--   if exists (SELECT TOP 1 1 from cambios_gral_clientes where accnum = @accnum)
--         select @rut = convert(varchar,rut) + dv, @nombre = nomcli from cambios_gral_clientes where accnum = @accnum
     
--  else 

--      if isnumeric(left(@accnum,len(@accnum)-1)) = 1
--         select @rut = @accnum
--			   if exists(select top 1 1 from cambios_tc_input_new a
--					  where a.Rut   = @rut
--						and a.Monto >= @monto-1
--						and a.Monto <= @monto+1
--						and a.Rut is not null
--						and isnull(a.Monto,0) <> 0
--						and a.Document_Name not like '%Copia%'
--						and a.Document_Name not like '%Copy%'
--						and a.Document_Name not like '%Duplicate%'
--						and a.Document_Name not like '%-%'  
--						and a.Nombre_Cliente not like '//'
--						and a.Nombre_Cliente not like '%//%'
--						and a.Rut not like '%MAL%')
--							   select @rut as rut, isnull(@nombre, Nombre_Cliente) Nombre_Cliente, rtrim(a.Document_Name) Document_Name
--								  from cambios_tc_input_new a
--								  where a.Rut   = @rut 
--										and a.Monto >= @monto-1
--										and a.Monto <= @monto+1
--										and a.Rut is not null
--										and isnull(a.Monto,0) <> 0
--										and a.Document_Name not like '%Copia%'
--										and a.Document_Name not like '%Copy%'
--										and a.Document_Name not like '%Duplicate%'
--										and a.Document_Name not like '%-%'  
--										and a.Nombre_Cliente not like '//'
--										and a.Nombre_Cliente not like '%//%'
--										and a.Rut not like '%MAL%'     
--								   order by 1
--      else
 
--          select isnull(@rut, @accnum) as rut , @nombre as Nombre_Cliente, '' as Document_Name
--end
--

--/****** Object:  StoredProcedure [dbo].[cambios_mift_check_list_log_01_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_check_list_log_01_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_check_list_log_01_MS]
--

--CREATE procedure [dbo].[cambios_mift_check_list_log_01_MS]
--(
--@accnum     varchar(20),
--@moneda     char(03),
--@monto      numeric(18,2),
--@usuario    varchar(20),
--@check_list varchar(15)
--)
--as
--begin

--   insert into cambios_mift_check_list_log (accnum,moneda,monto,fecha,hora,usuario,check_list) 
--      values (@accnum,@moneda,@monto,convert(datetime,convert(char(08),getdate(),112),112),getdate(),@usuario,@check_list)
--end

--


--/****** Object:  StoredProcedure [dbo].[cambios_mift_callfax_00_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_mift_callfax_00_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_mift_callfax_00_MS]
--

--CREATE procedure [dbo].[cambios_mift_callfax_00_MS] (@opcion tinyint, @rut bigint, @cuenta varchar(20))
--as
--begin
--   if @cuenta = '0'
--      select @cuenta = ''
----   if @rut is null
----      select @rut = 0
--   if exists (SELECT TOP 1 1 from cambios_mift_contratos where accnum = @cuenta and callfax is not null)
--      select top 1 rut, cuenta, callfax mensaje_01, null mensaje_02, null mensaje_03
--         from cambios_mift_contratos
--         where accnum = @cuenta
--           and callfax is not null
--   else
--      select top 0 rut, cuenta, callfax mensaje_01, null mensaje_02, null mensaje_03
--         from cambios_mift_contratos
--         where rut = @rut
--           and callfax is not null
--end
--


--/****** Object:  StoredProcedure [dbo].[cambios_gral_consulta_00_MS]    Script Date: 06-01-2016 18:17:29 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'cambios_gral_consulta_00_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP PROCEDURE [dbo].[cambios_gral_consulta_00_MS]
--
--CREATE PROCEDURE [dbo].[cambios_gral_consulta_00_MS] (
--    @opcion tinyint
--)
--as

--begin

--if @opcion = 1
--    select mnd_mndswf, mnd_mndnmc, mnd_mndnom
--    from cambios_gral_moneda
--end

--


--/*------------------------------------funciones C.I */
--/****** Object:  UserDefinedFunction [dbo].[habil_ant_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'habil_ant_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[habil_ant_MS]
--

--CREATE FUNCTION [dbo].[habil_ant_MS] (@fecha DATETIME, @ndias SMALLINT )  
--RETURNS DATETIME 
--AS  
--BEGIN
--DECLARE @dia_sem NUMERIC(01), @feri NUMERIC(01), @fecsal DATETIME, @seguir TINYINT, @d SMALLINT
--SET @d = 0
--SET @seguir = 1
--SET @feri   = 0
--while @d < @ndias
--    begin
--	SET @fecha =(SELECT (DATEADD(dd,-1,@fecha)))
--	SET @dia_sem = DATEPART(dw,@fecha)
--	IF EXISTS (SELECT dia_fer FROM gen_feriados WHERE CONVERT(CHAR(08),dia_fer,112) = CONVERT(CHAR(08),@fecha,112))
--	   SET @feri=1
--        ELSE
--           SET @feri=0
--	IF (@dia_sem = 6) OR (@dia_sem = 7) OR (@feri = 1) 
--                 CONTINUE 
--              ELSE
--                SET @d = @d+1
--     end	
--     SET @fecsal = @fecha
--RETURN (@fecsal)
--END
--





--/****** Object:  UserDefinedFunction [dbo].[DigitoVerificador_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'DigitoVerificador_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[DigitoVerificador_MS]
--

--CREATE FUNCTION [dbo].[DigitoVerificador_MS] (
--	@RUT BIGINT
--)  
--RETURNS CHAR(1) 
--AS  
--BEGIN 
--	DECLARE @RUTV VARCHAR(9), @largo SMALLINT, @suma INT, @modificador SMALLINT, @i SMALLINT, @DV CHAR(1)
--	SET @RUTV = CAST(@RUT AS VARCHAR(9))
--	SELECT @i = LEN(@RUTV), @suma = 0, @modificador = 2
--	WHILE @i > 0 
--		BEGIN
--		SET @suma = @suma + (CAST(SUBSTRING(@RUTV, @i, 1) AS SMALLINT) * @modificador)
--		SELECT @modificador = @modificador +1, @i = @i - 1
--		IF @modificador > 7
--			SET @modificador = 2
--		END
--	SET @modificador = 11 - (@suma % 11)
--	SET @i = @suma
--	IF @modificador = 11
--		SET @DV = '0'
--	ELSE
--		IF @modificador = 10
--			SET @DV = 'K'
--		ELSE
--				SET @DV = CAST(@modificador AS CHAR(1))
	
--RETURN (@DV)
--END

--




--/****** Object:  UserDefinedFunction [dbo].[correccion_caracter_especial_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'correccion_caracter_especial_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[correccion_caracter_especial_MS]
--

--CREATE FUNCTION [dbo].[correccion_caracter_especial_MS](
--  @TEXTO VARCHAR (300)
--)  
--RETURNS varchar(300)
--AS  
--BEGIN

--  SET @TEXTO = REPLACE(@TEXTO, 'Á', 'A')
--  SET @TEXTO = REPLACE(@TEXTO, 'Ã', 'A')
--  SET @TEXTO = REPLACE(@TEXTO, 'É', 'E')
--  SET @TEXTO = REPLACE(@TEXTO, 'Í', 'I')
--  SET @TEXTO = REPLACE(@TEXTO, 'Ó', 'O')
--  SET @TEXTO = REPLACE(@TEXTO, 'Ú', 'U')
--  SET @TEXTO = REPLACE(@TEXTO, '¥', 'N')

--  SET @TEXTO = REPLACE(@TEXTO, '“', '"')
--  SET @TEXTO = REPLACE(@TEXTO, '”', '"')

----  SET @TEXTO = REPLACE(@TEXTO, CHAR(13), ' ')
----  SET @TEXTO = REPLACE(@TEXTO, CHAR(10), ' ')
----  SET @TEXTO = REPLACE(@TEXTO, '', ' ')


--RETURN(LTRIM(RTRIM(@TEXTO)))
--END

--




--/****** Object:  UserDefinedFunction [dbo].[cambios_tarifas_pizarra_valor2_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'cambios_tarifas_pizarra_valor2_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[cambios_tarifas_pizarra_valor2_MS]
--

--CREATE function [dbo].[cambios_tarifas_pizarra_valor_MS]
--(
--@tipo        varchar(15),
--@sub_tipo    varchar(20),
--@moneda      char(03),
--@monto       numeric(18,2),
--@valor       numeric(18,2),
--@Observacion varchar(40)
--)
--RETURNS numeric(12,2)
--as
--begin

--declare @tasa    numeric(5,2),
--        @minimo  numeric(12,2),
--        @maximo  numeric(12,2),
--        @swift   numeric(12,2),
--        @impto   numeric(12,2),
--        @ourga   numeric(12,2),
--        @speed   numeric(12,2),
--        @carta_manual    numeric(12,2),
--        @comision_me     numeric(18,2)

--   if @valor = -1 and @Observacion = 'Valor Pizarra'
--      select @comision_me = dbo.cambios_tarifas_pizarra_calculo_MS(@tipo,@sub_tipo,@moneda,@monto) 
--     else
--      select @comision_me = @valor
  
      
--return (@comision_me)
--end

--




--/****** Object:  UserDefinedFunction [dbo].[cambios_tarifas_pizarra_valor_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'cambios_tarifas_pizarra_valor_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[cambios_tarifas_pizarra_valor_MS]
--

--CREATE function [dbo].[cambios_tarifas_pizarra_calculo_MS]
--(
--@tipo     varchar(15),
--@sub_tipo varchar(20),
--@moneda   char(03),
--@monto    numeric(18,2)
--)
--RETURNS numeric(12,2)
--as
--begin
--declare @tasa    numeric(5,2),
--        @minimo  numeric(12,2),
--        @maximo  numeric(12,2),
--        @swift   numeric(12,2),
--        @impto   varchar(15),
--        @ourga   numeric(12,2),
--        @speed   numeric(12,2),
--        @carta_manual    numeric(12,2),
--        @comision_me     numeric(18,2)
--   select @tasa   = isnull(tasa,0),
--          @minimo = minimo,
--          @maximo = maximo,
--          @swift  = swift,
--          @impto  = impto,
--          @ourga  = ourga
--      from cambios_gral_tarifas_pizarra
--      where tipo     = @tipo
----        and sub_tipo = @sub_tipo
--        and @monto >  rango_ini
--        and @monto <= rango_fin
--   select @comision_me = @monto * @tasa/100
--   if @comision_me < @minimo
--      select @comision_me = @minimo
--   if @comision_me > @maximo
--      select @comision_me = @maximo
--   if @sub_tipo = 'a) Emision OPC' or @sub_tipo = 'a) Emision OPC2'
--      select @comision_me = @comision_me
--   if @sub_tipo = 'b) Swift' or @sub_tipo = 'b) Swift2'
--      select @comision_me = @swift
--   if @sub_tipo = 'c) Speed Transfer' or @sub_tipo = 'c) Speed Transfer2'
--      select @comision_me = -1
--   if @sub_tipo = 'd) Carta Manual' or @sub_tipo = 'd) Carta Manual2'
--      select @comision_me = @comision_me
--   if @sub_tipo = 'e) Liquidacion OPR' or @sub_tipo = 'e) Liquidacion OPR2'
--      select @comision_me = @comision_me
--return (@comision_me)
--end

--

--/****** Object:  UserDefinedFunction [dbo].[cambios_tarifas_pizarra_calculo_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'cambios_tarifas_pizarra_calculo_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[cambios_tarifas_pizarra_calculo_MS]
--
--/****** Object:  UserDefinedFunction [dbo].[cambios_tarifas_convenio_valor_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'cambios_tarifas_convenio_valor_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[cambios_tarifas_convenio_valor_MS]
--

--CREATE function [dbo].[cambios_tarifas_convenio_valor_MS]
--(
--@tipo       varchar(15),
--@sub_tipo   varchar(20),
--@rut        bigint,
--@valor      numeric(18,2)
--)
--RETURNS numeric(12,2)
--as
--begin

--   if exists (SELECT TOP 1 1 from CAMBIOS_GRAL_TARIFAS_CONVENIO where rut = @rut)
--   begin
   
--      if @sub_tipo = 'a) Emision OPC'
--         select @valor = valor
--            from CAMBIOS_GRAL_TARIFAS_CONVENIO
--            where rut  = @rut
--              and tipo = 'OPC'
--      if @sub_tipo = 'b) Swift'
--         select @valor = valor
--            from CAMBIOS_GRAL_TARIFAS_CONVENIO
--            where rut  = @rut
--              and tipo = 'SWIFT'
--      if @sub_tipo = 'c) Speed Transfer'
--         select @valor = valor
--            from CAMBIOS_GRAL_TARIFAS_CONVENIO
--            where rut  = @rut
--              and tipo = 'SPEED'
--      if @sub_tipo = 'd) Carta Manual'
--         select @valor = valor
--            from CAMBIOS_GRAL_TARIFAS_CONVENIO
--            where rut  = @rut
--              and tipo = 'MANUAL'
--      if @sub_tipo = 'e) Liquidacion OPR'
--         select @valor = valor
--            from CAMBIOS_GRAL_TARIFAS_CONVENIO
--            where rut  = @rut
--              and tipo = 'OPR'

--   end

--   return (@valor)

--end

--




--/****** Object:  UserDefinedFunction [dbo].[cambios_tarifas_convenio_obs_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'cambios_tarifas_convenio_obs_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[cambios_tarifas_convenio_obs_MS]
--


--CREATE function [dbo].[cambios_tarifas_convenio_obs_MS]
--(
--@tipo       varchar(15),
--@sub_tipo   varchar(20),
--@rut        bigint,
--@obs        varchar(40)

--)

--RETURNS varchar(40)

--as

--begin
--   if exists (SELECT TOP 1 1 from cambios_gral_tarifas_convenio where rut = @rut)

--   begin

--      if @sub_tipo = 'a) Emision OPC' or  @sub_tipo = 'a) Emision OPC2'

--         select @obs = observacion

--            from cambios_gral_tarifas_convenio

--            where rut  = @rut

--              and tipo = 'OPC'

              

--      if @sub_tipo = 'b) Swift' or @sub_tipo = 'b) Swift2'

--         select @obs = observacion

--            from cambios_gral_tarifas_convenio

--            where rut = @rut

--              and tipo = 'SWIFT'

              

--      if @sub_tipo = 'c) Speed Transfer' or @sub_tipo = 'c) Speed Transfer2'

--         select @obs = observacion

--            from cambios_gral_tarifas_convenio

--            where rut = @rut

--              and tipo = 'SPEED'

              

--      if @sub_tipo = 'd) Carta Manual' or @sub_tipo = 'd) Carta Manual2'

--         select @obs = observacion

--            from cambios_gral_tarifas_convenio

--            where rut = @rut

--              and tipo = 'MANUAL'

              

--      if @sub_tipo = 'e) Liquidacion OPR' or @sub_tipo = 'e) Liquidacion OPR2'

--      begin

--         select @obs = observacion

--            from cambios_gral_tarifas_convenio

--            where rut = @rut

--              and tipo = 'OPR'

--      end   



--   end



--   return (@obs)



--end

--




--/****** Object:  UserDefinedFunction [dbo].[cambios_cvd_afp_rut_MS]    Script Date: 06-01-2016 18:55:07 ******/
--IF EXISTS(SELECT TOP 1 1 FROM sys.objects WHERE name = 'cambios_cvd_afp_rut_MS' AND schema_id = SCHEMA_ID('dbo'))
--DROP FUNCTION [dbo].[cambios_cvd_afp_rut_MS]
--

--CREATE function [dbo].[cambios_cvd_afp_rut_MS] (@rut bigint)
--returns bigint
--as 

--begin

--declare @rut_out  bigint

     

--     select @rut_out = rut_hijo

--        from cambios_gral_afp

--        where rut_padre = @rut

        

--     if @rut_out is null

--        select @rut_out = @rut

        

        

--     return (@rut)



--end

--


/********Fin Control Integral****************************************************************/



IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mchh_s01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mchh_s01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mchh_s01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mchh_s01_MS]
		@fecmov	datetime 
	AS
	BEGIN

	/************************************************************************/
	   select   codcct,
					codpro,
					codesp,
					codofi,
					codope,
					nrorpt,
					fecmov,
					codfun,
					desgen
	   from dbo.sce_mchh
	   where fecmov = @fecmov
	   and codfun in(798,799)

	   return
	END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mts_s02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mts_s02_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mts_s02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_mts_s02_MS] 
		@rut		NUMERIC(8,0), 
 		@fecha  	datetime 
	AS
	BEGIN

	/************************************************************************/

	   select   codcct+codpro+codesp+codofi+codope as ''Column1'',
			id_mensaje,
			tipgra,
			tipmt,
			codcct,
			codpro,
			codesp,
			codofi,
			codope
	   from dbo.sce_mts where
	   rutais = @rut   and
	   fecmsg = @fecha and
	   estado = 1     and
	   tipgra = ''R''
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_vgt_s02_s21_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_vgt_s02_s21_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_vgt_s02_s21_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_vgt_s02_s21_MS]
		@tipope         CHAR(5),
		@indcdr         CHAR(2),
		@num_me         CHAR(7),
		@dig_me         CHAR(1),
		@numcli         CHAR(9),
		@digcli         CHAR(1),
		@codcct         CHAR(3),
		@codpro         CHAR(2),
		@codesp         CHAR(2),
		@codofi         CHAR(3),
		@codope         CHAR(5),
		@saldo          CHAR(14),
		@ls_retorno     CHAR(3)  OUTPUT,
		@ls_mensaje     CHAR(250) OUTPUT 
	AS
	BEGIN

		IF 1=0 BEGIN
			SET FMTONLY OFF
		END


	--creacion de tabla temporal
	   CREATE TABLE #tmp_vgt
	   (
		  codcct           CHAR(3)       NOT NULL,
		  codpro           CHAR(2)       NOT NULL,
		  codesp           CHAR(2)       NOT NULL,
		  codofi           CHAR(3)       NOT NULL,
		  codope           CHAR(5)       NOT NULL,
		  numcor           NUMERIC(6,0)  NOT NULL,
		  numcuo           NUMERIC(3,0)  NOT NULL,
		  moneda           CHAR(3)       NOT NULL,
		  numacc           CHAR(3)       NOT NULL,
		  num_me           CHAR(7)       NOT NULL,
		  dig_me           CHAR(1)       NOT NULL,
		  tipope           CHAR(5)       NOT NULL,
		  indcdr           CHAR(2)       NOT NULL,
		  feccon           CHAR(8)       NOT NULL,
		  fecven           CHAR(8)       NOT NULL,
		  fecint           CHAR(8)       NOT NULL,
		  fecori           CHAR(8)       NOT NULL,
		  val_mo           CHAR(14)      NOT NULL,
		  nomcli           CHAR(25)      NOT NULL,
		  tiptas           CHAR(1)       NOT NULL,
		  tasbas           CHAR(8)       NOT NULL,
		  spread           CHAR(8)       NOT NULL,
		  tastot           CHAR(8)       NOT NULL,
		  numcli           CHAR(9)       NOT NULL,
		  digcli           CHAR(1)       NOT NULL,
		  numava1          CHAR(9)       NOT NULL,
		  digava1          CHAR(1)       NOT NULL,
		  numava2          CHAR(9)       NOT NULL,
		  digava2          CHAR(1)       NOT NULL,
		  tipcam           CHAR(10)      NOT NULL,
		  diadev           CHAR(5)       NOT NULL,
		  moneda_int       CHAR(3)       NOT NULL,
		  valori_cre_mo    CHAR(14)      NOT NULL,
		  int_al_ven_mo    CHAR(14)      NOT NULL,
		  dev_normal_mo    CHAR(14)      NOT NULL,
		  rea_normal       CHAR(14)      NOT NULL,
		  tc_origen        CHAR(9)       NOT NULL,
		  instfom          CHAR(7)       NULL,
		  causa_ext_cre    CHAR(1)       NOT NULL,
		  diamor           CHAR(5)       NOT NULL
	   )
	   begin
		--declaracion de constantes para where de update
		  declare @marca_a   CHAR(7),@marca_b   CHAR(7),@marca_c   CHAR(7),@ccos_j    CHAR(3),
		  @ccos_p    CHAR(3),@prod_jcci CHAR(2),@prod_jant CHAR(2),@prod_pae  CHAR(2),
		  @instfom_a CHAR(2),@instfom_b CHAR(2),@instfom_c CHAR(2),
	/**************************************************************
		Realsystems-Código Nuevo-Inicio
		Fecha Modificación 20100806
		Responsable: Mauricio Aguilera V.
		Versión: 1.0
		Descripción : Se declaran variables 
	**************************************************************/   

				@tipope1   CHAR(5),
		  @tipope2   CHAR(5),@tipope3   CHAR(5),@tipope4   CHAR(5),
		  @tipope5   CHAR(5),@tipope6   CHAR(5),@tipope7   CHAR(5),@tipope8   CHAR(5),
		  @tipope9   CHAR(5),@tipope10  CHAR(5),@tipope11  CHAR(5),@tipope12  CHAR(5),
		  @tipope13  CHAR(5)
	/**************************************************************
		RealSystems - Código Nuevo - Termino
	***************************************************************/
		  select   @prod_jcci = ''07''
		  select   @prod_jant = ''08''
		  select   @prod_pae  = ''05''
		  select   @marca_a   = ''NINGUNO''
		  select   @marca_b   = ''FOGAPE''
		  select   @marca_c   = ''COBEX''
		  select   @instfom_a = ''01''
		  select   @instfom_b = ''02''
		  select   @instfom_c = ''03''

	/**************************************************************
		Realsystems-Código Nuevo-Inicio
		Fecha Modificación 20100806
		Responsable: Mauricio Aguilera V.
		Versión: 1.0
		Descripción : Se asignan variables
	**************************************************************/      
		  select   @tipope1   = ''15551''
		  select   @tipope2   = ''15555''
		  select   @tipope3   = ''15571''
		  select   @tipope4   = ''04051''
		  select   @tipope5   = ''15592''
		  select   @tipope6   = ''04052''
		  select   @tipope7   = ''15593''
		  select   @tipope8   = ''18502''
		  select   @tipope9   = ''17000''
		  select   @tipope10  = ''17006''
		  select   @tipope11  = ''17004''
		  select   @tipope12  = ''17008''
		  select   @tipope13  = ''15575''

	/***************************************************************
		RealSystems - Código Nuevo - Termino
	***************************************************************/

           
	   BEGIN TRAN
		  if @tipope <> ''''
			 insert  into #tmp_vgt
			 select  codcct,codpro,codesp,codofi,codope,numcor,numcuo,moneda,numacc,num_me,dig_me,tipope,
					indcdr,feccon,fecven,fecint,fecori,val_mo,nomcli,tiptas,tasbas,spread,tastot,numcli,digcli,numava1,
					digava1,numava2,digava2,tipcam,diadev,moneda_int,valori_cre_mo,int_al_ven_mo ,dev_normal_mo,
					rea_normal,tc_origen,Null,causa_ext_cre,diamor from dbo.sce_vgt_s21
			 where   tipope = @tipope
			 and   indcdr = @indcdr
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E01''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E01] Error al insert en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  if @num_me <> ''''
			 insert  into #tmp_vgt
			 select  codcct,codpro,codesp,codofi,codope,numcor,numcuo,moneda,numacc,num_me,dig_me,tipope,
					indcdr,feccon,fecven,fecint,fecori,val_mo,nomcli,tiptas,tasbas,spread,tastot,numcli,digcli,numava1,
					digava1,numava2,digava2,tipcam,diadev,moneda_int,valori_cre_mo,int_al_ven_mo ,dev_normal_mo,
					rea_normal,tc_origen,Null,causa_ext_cre,diamor from dbo.sce_vgt_s21
			 where   num_me = @num_me
			 and   dig_me = @dig_me
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E02''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E02] Error al insert en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  if @numcli <> ''''
			 insert  into #tmp_vgt
			 select  codcct,codpro,codesp,codofi,codope,numcor,numcuo,moneda,numacc,num_me,dig_me,tipope,
					indcdr,feccon,fecven,fecint,fecori,val_mo,nomcli,tiptas,tasbas,spread,tastot,numcli,digcli,numava1,
					digava1,numava2,digava2,tipcam,diadev,moneda_int,valori_cre_mo,int_al_ven_mo ,dev_normal_mo,
					rea_normal,tc_origen,Null,causa_ext_cre,diamor from dbo.sce_vgt_s21
			 where   numcli = @numcli
			 and   digcli = @digcli
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E03''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E03] Error al insert en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  if @codcct <> ''''  and @codpro <> '''' and @codesp <> '''' and @codofi <> '''' and @codope <> ''''
			 insert  into #tmp_vgt
			 select  codcct,codpro,codesp,codofi,codope,numcor,numcuo,moneda,numacc,num_me,dig_me,tipope,
					indcdr,feccon,fecven,fecint,fecori,val_mo,nomcli,tiptas,tasbas,spread,tastot,numcli,digcli,numava1,
					digava1,numava2,digava2,tipcam,diadev,moneda_int,valori_cre_mo,int_al_ven_mo ,dev_normal_mo,
		  rea_normal,tc_origen,Null,causa_ext_cre,diamor from dbo.sce_vgt_s21
			 where     codcct = @codcct
			 and     codpro = @codpro
			 and     codesp = @codesp
			 and     codofi = @codofi
			 and     codope = @codope
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E04''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E04] Error al insert en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  if @saldo <> ''''
			 insert  into #tmp_vgt
			 select  codcct,codpro,codesp,codofi,codope,numcor,numcuo,moneda,numacc,num_me,dig_me,tipope,
					indcdr,feccon,fecven,fecint,fecori,val_mo,nomcli,tiptas,tasbas,spread,tastot,numcli,digcli,numava1,
					digava1,numava2,digava2,tipcam,diadev,moneda_int,valori_cre_mo,int_al_ven_mo ,dev_normal_mo,
					rea_normal,tc_origen,Null,causa_ext_cre,diamor from dbo.sce_vgt_s21
			 where   val_mo = @saldo
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E05''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E05] Error al insert en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  if @tipope = '''' and @num_me = '''' and @numcli = '''' and @saldo = '''' and @codcct = '''' and @codpro = '''' and
		  @codesp = '''' and @codofi = '''' and @codope = ''''
			 insert  into #tmp_vgt
			 select  codcct,codpro,codesp,codofi,codope,numcor,numcuo,moneda,numacc,num_me,dig_me,tipope,
					indcdr,feccon,fecven,fecint,fecori,val_mo,nomcli,tiptas,tasbas,spread,tastot,numcli,digcli,numava1,
					digava1,numava2,digava2,tipcam,diadev,moneda_int,valori_cre_mo,int_al_ven_mo ,dev_normal_mo,
					rea_normal,tc_origen,Null,causa_ext_cre,diamor from dbo.sce_vgt_s21
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E06''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E06] Error al insert en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  update #tmp_vgt set instfom = b.instfom from dbo.sce_jcci b, #tmp_vgt c Where c.codpro  = @prod_jcci
		  and b.codcct  = c.codcct
		  and b.codpro  = c.codpro
		  and b.codesp  = c.codesp
		  and b.codofi  = c.codofi
		  and b.codope  = c.codope
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E07''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E07] Error al Update en tabla #tmp_vgt desde sce_jcci''
			 drop table #tmp_vgt
			 return
		  end
		  update #tmp_vgt set instfom = b.instfom from dbo.sce_jant b, #tmp_vgt c Where c.codpro  = @prod_jant
		  and b.codcct  = c.codcct
		  and b.codpro  = c.codpro
		  and b.codesp  = c.codesp
		  and b.codofi  = c.codofi
		  and b.codope  = c.codope
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E08''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E08] Error al Update en tabla #tmp_vgt desde sce_jant''
			 drop table #tmp_vgt
			 return
		  end
           
		--actualizacion de tabla temporal con marca de instrumento de fomento (01,02,03) desde la tabla sce_pae       
		  update #tmp_vgt set instfom = b.instfom from dbo.sce_pae b, #tmp_vgt c Where c.codpro  = @prod_pae
		  and b.codcct  = c.codcct
		  and b.codpro  = c.codpro
		  and b.codesp  = c.codesp
		  and b.codofi  = c.codofi
		  and b.codope  = c.codope
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E09''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E09] Error al Update en tabla #tmp_vgt desde sce_pae''
			 drop table #tmp_vgt
			 return
		  end    

		--actualizacion de tabla temporal de marca de instrumento de fomento de numero a palabra (01=NINGUNO, 02=FOGAPE, 03=COBEX)
		  update #tmp_vgt set instfom = @marca_a  Where instfom   = @instfom_a
		  or instfom   = ''''
		  or instfom IS NULL
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E10''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E09] Error al Update en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  update #tmp_vgt set instfom = @marca_b  Where instfom   = @instfom_b
		  or instfom   = ''''
		  or instfom IS NULL
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E11''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E09] Error al Update en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  update #tmp_vgt set instfom = @marca_c  Where instfom   = @instfom_c
		  or instfom   = ''''
		  or instfom IS NULL
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E12''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E09] Error al Update en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end

	/**************************************************************
		Realsystems-Código Nuevo-Inicio
		Fecha Modificación 20100806
		Responsable: Mauricio Aguilera V.
		Versión: 1.0
		Descripción : Se actualizan los datos de diamor y causa_ext_cre
					  segun los datos de definidos en las variables.
	**************************************************************/   
        
		  update #tmp_vgt set causa_ext_cre = ''0'',diamor = ''00000''  Where   tipope = @tipope1
		  or      tipope = @tipope2
		  or      tipope = @tipope3
		  or      tipope = @tipope4
		  or      tipope = @tipope5
		  or      tipope = @tipope6
		  or      tipope = @tipope7
		  or      tipope = @tipope8
		  or      tipope = @tipope9
		  or      tipope = @tipope10
		  or      tipope = @tipope11
		  or      tipope = @tipope12
		  or      tipope = @tipope13
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E13''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E13] Error al Update en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end
		  update #tmp_vgt set val_mo = ''00000000000000'' Where causa_ext_cre = ''5''
		  if @@error <> 0
		  begin
			 rollback 
			 select   @ls_retorno = ''E14''
			 select   @ls_mensaje = ''sce_vgt_s02_s21 : [E14] Error al Update en tabla #tmp_vgt''
			 drop table #tmp_vgt
			 return
		  end                
    
        
	--       Update #tmp_vgt Set val_mo = ''00000000000000''
	--       where   causa_ext_cre <> ''0''
	--       or     diamor <> ''00000''

	/***************************************************************
		RealSystems - Código Nuevo - Termino
	***************************************************************/
		  select   
				[Operacion]=codcct+codpro+codesp+codofi+codope,
				numcor,
				numcuo,
				moneda,
				numacc,
				num_me,
				dig_me,
				tipope,
				indcdr,
				feccon,
				fecven,
			   fecint,
			   fecori,
			   val_mo,
			   nomcli,
			   tiptas,
			   tasbas,
			   spread,
			   tastot,
			   numcli,
			   digcli,
			   numava1,
			   digava1,
			   numava2,
			   digava2,
			   tipcam,
			   diadev,
			   moneda_int,
			   valori_cre_mo,
			   int_al_ven_mo,
			   dev_normal_mo,
			   rea_normal,
			   tc_origen,
			   instfom
		  from #tmp_vgt order by 1,3,5
		--  return
		  COMMIT TRAN
		  Select   @ls_retorno = ''00''
		  Select   @ls_mensaje = ''sce_vgt_s02_s21. Se realizo exitosamente la actualizacion de la tabla #tmp_vgt''
	   end
	   drop table #tmp_vgt
	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'pro_sce_datusr_u02_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[pro_sce_datusr_u02_MS]
END

-- =============================================
-- Author:		Hernán Silva.
-- Create date: 07/12/2015
-- Description:	Actualiza los datos del usuario para impresion de contabilidad generica. 
-- =============================================

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pro_sce_datusr_u02_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pro_sce_datusr_u02_MS]

		@samAccountName nvarchar(500), 

		@ConfigImpres_ContabilidadGenerica varchar(500)
	AS

	BEGIN

		SET NOCOUNT ON;

		UPDATE [dbo].[tbl_datos_usuario]

		SET [ConfigImpres_ContabilidadGenerica] = @ConfigImpres_ContabilidadGenerica

		WHERE [samAccountName] = @samAccountName

	END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_rng_u01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[proc_sce_rng_u01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sce_rng_u01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure proc_sce_rng_u01_MS
		@codcct varchar(3),
		@codfun varchar(3),
		@codesp varchar(2),
		@numopt	int

		AS

	--declare @codcct varchar(3)
	--declare @codfun varchar(3)
	--declare @codesp varchar(2)
	--declare @numopt	int

	--set @codcct = ''753''
	--set	@codfun = ''FTC''
	--set	@codesp = ''44''
	--set @numopt = 416



		if  Exists( select numact from  sce_rng 
			where codcct = @codcct
			and codfun = @codfun
			and codesp = @codesp
			and numact = @numopt )
		begin
			update sce_rng set numact =  @numopt - 1
			where codcct = @codcct
			and codfun = @codfun
			and codesp = @codesp
		end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_pli_w03_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_pli_w03_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_pli_w03_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.sce_pli_w03_MS 
    @numpli         CHAR(7)       ,
	@fecpli         datetime      ,
	@cencos         CHAR(3)       ,
	@codusr         CHAR(2)       ,
	@fecact         datetime      ,
	@codcct         CHAR(3)       ,
	@codpro         CHAR(2)       ,
	@codesp         CHAR(2)       ,
	@codofi         CHAR(3)       ,
	@codope         CHAR(5)       ,
	@codanu         CHAR(6)       ,
	@estado         NUMERIC(2,0)  ,
	@codoper        CHAR(6)       ,
	@plzbcc         NUMERIC(2,0)  ,
	@rutcli         CHAR(10)      ,
	@prtcli         CHAR(12)      ,
	@indnom         NUMERIC(2,0)  ,
	@inddir         NUMERIC(2,0)  ,
	@codoci         NUMERIC(3,0)  ,
	@tippln         NUMERIC(2,0)  ,
	@codcom         CHAR(6)       ,
	@concep         CHAR(3)       ,
	@anunum         CHAR(7)       ,
	@anufec         datetime      ,
	@anupbc         NUMERIC(2,0)  ,
	@apctip         CHAR(2)       ,
	@apcnum         CHAR(7)       ,
	@apcfec         datetime      ,
	@apcpbc         NUMERIC(2,0)  ,
	@motivo         VARCHAR(25)   ,
	@numacu         NUMERIC(1,0)  ,
	@desacu         CHAR(15)      ,
	@codpai         NUMERIC(3,0)  ,
	@codmnd         NUMERIC(3,0)  ,
	@codmndbc       NUMERIC(3,0)  ,
	@mtoope         NUMERIC(15,2) ,
	@mtopar         NUMERIC(17,10),       
	@mtodol         NUMERIC(15,2) ,
	@tipcam         NUMERIC(11,4) ,
	@mtonac         NUMERIC(15,2) ,
	@dienum         CHAR(7)       ,
	@diefec         datetime      ,
	@diepbc         NUMERIC(2,0)  ,
	@numdec         CHAR(7)       ,
	@fecdec         datetime      ,
	@codadn         NUMERIC(3,0)  ,
	@fecdeb         datetime      ,
	@docnac         CHAR(15)      ,
	@docext         CHAR(14)      ,       
	@bcoext         NUMERIC(4,0)  ,
	@numcre         NUMERIC(7,0)  ,
	@feccre         datetime      ,
	@mndcre         NUMERIC(3,0)  ,
	@mtocre         NUMERIC(15,2) ,
	@codacu         CHAR(10)      ,
	@regacu         CHAR(10)      ,
	@rutacu         CHAR(10)      ,
	@obspli         VARCHAR(255)  ,
	@codeor         CHAR(1)       ,
	@datimp		    NUMERIC(15,2) ,
	@fecins		    datetime      ,
	@nomfin		    VARCHAR(60)   ,
	@VenOfi 	    datetime      ,
	@numcon 	    NUMERIC(8,0)  ,
	@fecsus		    datetime      ,
    @VenOd  	    datetime      ,
	@insuti		    NUMERIC(2,0)  ,
	@partip		    NUMERIC(10,4) ,
	@arecon		    NUMERIC(2,0)  ,
	@zonfra		    BIT	          ,
	@secben		    NUMERIC(2,0)  ,
	@secfin		    NUMERIC(2,0)  ,
	@prcpar		    NUMERIC(4,1) 

AS
BEGIN
/*
This procedure was converted on Wed Apr 16 13:37:37 2014 using Ispirer SQLWays 6.0 Build 2114 64bit Licensed to 
AKZIO CONSULTORES LIMITADA - Chile (Professional License, Ispirer SQLWays 6.0 Sybase ASE to MSSQLServer 
Database Migration Tool, Without Support, 1000 Tables, 80GB, 3000 SPs, 500,000 LOC, 6 Months, 20140907).
*/
   DECLARE
   @ok_pli 		NUMERIC(1,0),
   @ok_plia		NUMERIC(1,0),
   @ok_pldr		NUMERIC(1,0),
   @ok_plib		NUMERIC(1,0)
/************************************************************************/
   BEGIN

   BEGIN TRAN
      select   @numpli = right(''0000000''+Rtrim(@numpli),7)
      select   @ok_pli  = 0
      select   @ok_plia = 0
      select   @ok_pldr = 0
      select   @ok_plib = 0

      if not exists(SELECT TOP 1 1 from dbo.sce_pli where

      numpli = @numpli and

      fecpli = @fecpli)

      begin

         insert into dbo.sce_pli values(@numpli,

			@fecpli,

			@cencos,

			@codusr,

			@fecact,

			@fecact,

			@codcct,

			@codpro,

			@codesp,

			@codofi,

			@codope,

			@codanu,

			@estado,

			@codoper,

			@plzbcc,

			@rutcli,

			@prtcli,

			@indnom,

			@inddir,

			@codoci,

			@tippln,

			@codcom,

			@concep,

			right(''0000000''+@anunum,7),

			@anufec,

			@anupbc,

			@apctip,

			@apcnum,

			@apcfec,

			@apcpbc,

			@motivo,

			@numacu,

			@desacu,

			@codpai,

			@codmnd,

			@codmndbc,

			@mtoope,

			@mtopar,

			@mtodol,

			@tipcam,

			@mtonac,

			@dienum,

			@diefec,

			@diepbc,

			@numdec,

			@fecdec,

			@codadn,

			@fecdeb,

			@docnac,

			@docext,

			@bcoext,

			@numcre,

			@feccre,

			@mndcre,

			@mtocre,

			@codacu,

			@regacu,

			@rutacu,

			@obspli,

			@codeor)

		

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_pli = 1

      end

   else
      begin
         update dbo.sce_pli 
		 set fecact  = @fecact,
		     codcct  = @codcct,
			 codpro  = @codpro,
			 codesp  = @codesp,
             codofi  = @codofi,
			 codope  = @codope,
			 estado  = @estado,
			 codoper = @codoper,
             plzbcc  = @plzbcc,
			 rutcli  = @rutcli,
			 prtcli  = @prtcli,
			 indnom  = @indnom,
             inddir  = @inddir,
			 codoci  = @codoci,
			 tippln  = @tippln,
			 codcom  = @codcom,
             concep  = @concep,
			 anunum  = right(''0000000''+@anunum,7),
			 anufec  = @anufec,
             anupbc  = @anupbc,
			 apctip  = @apctip,
			 apcnum  = @apcnum,
			 apcfec  = @apcfec,
             apcpbc  = @apcpbc,
			 motivo  = @motivo,
			 numacu  = @numacu,
			 desacu  = @desacu,
             codpai  = @codpai,
			 codmnd  = @codmnd,
			 codmndbc  = @codmndbc,
             mtoope  = @mtoope,
			 mtopar  = @mtopar,
			 mtodol  = @mtodol,
			 tipcam  = @tipcam,
             mtonac  = @mtonac,dienum  = @dienum,
			 diefec  = @diefec,
			 diepbc  = @diepbc,
             numdec  = @numdec,
			 fecdec  = @fecdec,
			 codadn  = @codadn,
			 fecdeb  = @fecdeb,
             docnac  = @docnac,
			 docext  = @docext,
			 bcoext  = @bcoext,
			 numcre  = @numcre,
             feccre  = @feccre,
			 mndcre  = @mndcre,
			 mtocre  = @mtocre,
			 codacu  = @codacu,
             regacu  = @regacu,
			 rutacu  = @rutacu,
			 obspli  = @obspli,
			 codeor  = @codeor  
	     where  numpli = @numpli 
		 and    fecpli = @fecpli

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_pli = 1

      end

      if not exists(SELECT TOP 1 1 
	                from dbo.sce_plia 
					where numpli = @numpli 
					and   fecpli = @fecpli)

      begin

         insert into dbo.sce_plia 
		 values(@numpli,
      			@fecpli,
    			@datimp,
    			@fecins,
    			@nomfin,
    			@VenOfi,
    			@codpai)

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_plia = 1

      end

   else

      begin
         update dbo.sce_plia 
		 set  datimp  = @datimp,
		      fecins  = @fecins,
			  nomfin  = @nomfin,
			  fecven  = @VenOfi,
              codpai  = @codpai  
		where numpli = @numpli 
		and   fecpli = @fecpli

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_plia = 1

      end

      if not exists(SELECT TOP 1 1 from dbo.sce_pldr where

      numpli = @numpli and

      fecpli = @fecpli)

      begin

         insert into dbo.sce_pldr 
		 values(@numpli,
        		@fecpli,
        		@numcon,
        		@fecsus,
        		@VenOd ,
        		@insuti,
    			@partip,
    			@arecon)

		

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_pldr = 1

      end

   else

      begin

         update dbo.sce_pldr 
		 set numcon  = @numcon,
		     fecsus  = @fecsus,
			 fecven  = @VenOd,
			 insuti  = @insuti,
             partip  = @partip,
			 arecon  = @arecon  
	     where numpli = @numpli 
		 and   fecpli = @fecpli

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_pldr = 1

      end

      if not exists(SELECT TOP 1 1 
	                from dbo.sce_plib 
					where numpli = @numpli 
					and   fecpli = @fecpli)
      begin

         insert into dbo.sce_plib 
		 values(    @numpli,
                	@fecpli,
                	@zonfra,
                	@secben,
                	@secfin,
                	@prcpar)

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_plib = 1

      end

   else
      begin

         update dbo.sce_plib 
		 set zonfra  = @zonfra,
		     secben  = @secben,
			 secfin  = @secfin,
			 prcpar  = @prcpar  
		 where  numpli = @numpli 
		 and    fecpli = @fecpli

         if (@@rowcount > 0 and @@error = 0)

            select   @ok_plib = 1

      end

      if (@ok_pli = 0 or @ok_plia = 0 or @ok_pldr = 0 or @ok_plib = 0)

      begin

         rollback 

         select   9

      end
   else
      begin
         commit tran
         select   0
      end
   end
   return
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'ejc_prty_ejc_s_01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[ejc_prty_ejc_s_01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ejc_prty_ejc_s_01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure ejc_prty_ejc_s_01_MS 
	 (
	   @prty_rut varchar(12) 
	 )
	 AS

	begin

	 select prty_rut,ejc_ofi,ejc_cod,ejc_tpo,create_at,update_at from tbl_prty_ejc_MS where prty_rut = @prty_rut


	 end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'ejc_prty_ejc_i_01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[ejc_prty_ejc_i_01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ejc_prty_ejc_i_01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure ejc_prty_ejc_i_01_MS
	 (@prty_rut varchar(12), 
	 @ejc_ofi decimal(5), 
	 @ejc_cod decimal(5),
	 @ejc_tpo char(1), 
	 @create_at datetime, 
	 @update_at datetime
	 )
	 AS

	 begin
	  begin tran

  		delete from dbo.tbl_prty_ejc_MS where 
					prty_rut = @prty_rut and
					ejc_tpo = @ejc_tpo;
 
		insert dbo.tbl_prty_ejc_MS (prty_rut, ejc_ofi, ejc_cod,ejc_tpo, create_at, update_at)
							values (@prty_rut,@ejc_ofi,@ejc_cod,@ejc_tpo,@create_at,@update_at)

		if @@rowcount > 0 and @@error = 0
	   begin
		  commit tran
		  select   0
	   end
	else
	   begin
		  rollback 
		  select   9
	   end   
 
	end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_usr_s26_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_usr_s26_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_usr_s26_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sce_usr_s26_MS] 

	@rut_ejec        CHAR(12) 

	AS
	BEGIN
	/*	

	Historial:

							 Migración desde Sybase (AKZIO)

		  2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)

	*/

	   SET NOCOUNT ON 
	   SELECT rut,
			  cent_costo,
			  id_especia,
			  ofic_orige,
			  nombre,
			  direccion,
			  comuna,
			  ciudad,
			  telefono,
			  fax,
			  tipeje
          
          
	   FROM   dbo.sce_usr 
	   WHERE  rut = @rut_ejec
	   ORDER BY rut desc

	END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_rng_ui01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_rng_ui01_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_rng_ui01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE  [dbo].[sce_rng_ui01_MS] 

	@codcct      CHAR(3),

    @codesp      CHAR(2),

    @codfun      CHAR(3),

    @rutesp      CHAR(10),

    @nummin      FLOAT,

    @nummax      FLOAT,

    @numact      FLOAT 

AS

BEGIN

/*	

Historial:

                         Migración desde Sybase (AKZIO)

      2015-09-02   AOC   Revisión de código proyecto migración Comex.Net (Microsoft)

*/

   SET NOCOUNT ON 



BEGIN TRAN

   IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.sce_rng WHERE

   codcct = @codcct AND 

   codesp = @codesp AND 

   codfun = @codfun)

   BEGIN 

      INSERT  INTO  dbo.sce_rng(codcct,

		codesp,

		codfun,

		rutesp,

 		nummin,

		nummax,

		numact)

		VALUES (
		@codcct,

		@codesp,

		@codfun,

		@rutesp,

		@nummin,

		@nummax,

		@numact)

   END 

ELSE 

   BEGIN 

   UPDATE  dbo.sce_rng 
   SET  numact = numact+1  
   WHERE codcct = @codcct 
   AND   codesp = @codesp 
   AND   codfun = @codfun 
   AND   numact < nummax	 
   AND   numact > 0
   END 


   IF  (@@ERROR  <> 0)
   BEGIN 

      ROLLBACK 

      SELECT -1, ''Error al grabar datos en Sce_Rng''
      RETURN  -1
   END 

   SELECT @numact = numact,
          @nummax = nummax
   FROM   dbo.sce_rng   WITH (HOLDLOCK)
   WHERE  codcct = @codcct 
   AND    codesp = @codesp 
   AND    codfun = @codfun

   IF  (@numact > @nummax AND  @numact > 0)

   BEGIN 

      ROLLBACK 
      SELECT    -1, ''Error al grabar datos en Sce_Rng''
      RETURN  -1

   END 
   COMMIT TRAN

   SELECT    @numact AS numact 	
   RETURN    @numact
END'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_s76_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].[sce_mcd_s76_MS]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_s76_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sce_mcd_s76_MS]
		@codcct  CHAR(3),
		@codpro  CHAR(2),
		@codesp  CHAR(2),
		@codofi  CHAR(3),
		@codope  CHAR(5) 
	AS
	BEGIN
	/*	
	Historial:
		  2016-05-24   MBP   Arkano: Se agrega este SP que retorna las operaciones ya inyectadas
	*/
	   SET NOCOUNT ON 

	   select *
	   from sce_mcd
	   where codcct = @codcct
	   and codpro = @codpro
	   and codesp = @codesp
	   and codofi = @codofi
	   and codope = @codope
	   and enlinea = 1

	END'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mcd_i01_gl01_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].sce_mcd_i01_gl01_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mcd_i01_gl01_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[sce_mcd_i01_gl01_MS] 
	@codcct  CHAR(3),
	@codpro  CHAR(2),
	@codesp  CHAR(2),
	@codofi  CHAR(3),
	@codope  CHAR(5),
	@codneg  NUMERIC(2,0),
	@codsec  NUMERIC(2,0),
	@nrorpt  NUMERIC(8,0),
	@fecmov  datetime,
	@cencos	CHAR(3),
	@codusr	CHAR(2),
	@nroimp  NUMERIC(2,0),
	@estado  NUMERIC(2,0),
	@tipmcd  CHAR(1),
	@idncta  NUMERIC(3,0),
	@nemcta  CHAR(15),
	@numcta  CHAR(8),
	@codmon  NUMERIC(3,0),
	@nemmon  CHAR(3),
	@mtomcd  NUMERIC(15,2),
	@cod_dh  CHAR(1),
	@numemb  NUMERIC(3,0),
	@prtcli  CHAR(12),
	@indcli  NUMERIC(2,0),
	@rutcli  CHAR(10),
	@prtbco  CHAR(12),
	@indbco  NUMERIC(2,0),
	@rutbco  CHAR(10),
	@swibco  CHAR(12),
	@codbco  NUMERIC(4,0),
	@numcct  CHAR(15),
	@numlin  CHAR(10),
	@fecori  datetime,
	@fecven  datetime,
	@fecint  datetime,
	@tasfij  BIT,
	@mtotas  NUMERIC(9,6),
	@ofides  NUMERIC(3,0),
	@numpar  NUMERIC(8,0),
	@tipmov  NUMERIC(1,0),
	@nroref  CHAR(15), --Real Systems Ltda.
	@tipcam  NUMERIC(11,4),
	@nrotop  NUMERIC(6,0),
	@indtop  NUMERIC(2,0),
	@intcit  BIT,
	@intcvt  BIT,
	@intcap  BIT,
	@intctd  BIT,
	@intpos  BIT,
	@intcdr  BIT,
	@mcdvig  BIT,
	@nrofac  NUMERIC(10,0) 
AS
begin


/************************************************************************/



   if (@mtomcd < 0)
   begin
      select   9
      return
   end

	-- Se cambia CCE por CCEBAE para movimientos BAE de Cta. Cte. M/E.-
	--if @nemcta= "CCE" and substring(@numcct,1,8)=@numcct
	--begin
	   --select @nemcta = "CCEBAE"
	   --select @numcta = "22170155"
	--end
	-- FTF: 24/07/2002 a solicitud Sr. C. Tapia B.
	-- Se elimina el codigo anterior con la fusion de la Cta. Cte. M/E


   if @swibco = ''BCHIUS3MXXX''
   begin
      select   @swibco = ''BCHIUS33XXX''
      select   @codbco = 532
   end

begin tran
   insert into
   dbo.sce_mcd values(@codcct,
		@codpro,
		@codesp,
		@codofi,
		@codope,
		@codneg,
		@codsec,
		@nrorpt,
		@fecmov,
		@cencos,
		@codusr,
		@nroimp,
		@estado,
		@tipmcd,
		@idncta,
	        upper(@nemcta),
	        upper(@numcta),
		@codmon,
		@nemmon,
		@mtomcd,
		@cod_dh,
		@numemb,
		@prtcli,
		@indcli,
		@rutcli,
		@prtbco,
		@indbco,
		@rutbco,
		@swibco,
		@codbco,
		@numcct,
		@numlin,
		@fecori,
		@fecven,
		@fecint,
		@tasfij,
		@mtotas,
		@ofides,
		@numpar,
		@tipmov,
		@nroref,
		@tipcam,
		@nrotop,
		@indtop,
		@intcit,
		@intcvt,
		@intcap,
		@intctd,
		@intpos,
		@intcdr,
		@mcdvig,
		0,		-- Cta Cte Batch
		'''',		-- Rut Ais que inyecto Cta Cte Linea
		@nrofac -- Numero Factura
	)
     
   if @@rowcount > 0 and @@error = 0
   begin
      commit tran
      select   0
   end
else
   begin
      rollback 
      select   9
   end
end'
END

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'sce_mch_s15_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].sce_mch_s15_MS
END


IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sce_mch_s15_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure sce_mch_s15_MS 
		@codcct varchar(3),
		@codpro varchar(2),
		@codesp varchar(2),
		@codofi varchar(3),
		@codope varchar(5)
AS

	begin
		--declare @codcct varchar(3)
		--declare @codpro varchar(2)
		--declare @codesp varchar(2)
		--declare @codofi varchar(3)
		--declare @codope varchar(5)

		--set @codcct =''753''
		--set @codpro =''10''
		--set @codesp =''64''
		--set @codofi =''000''
		--set @codope =''06411''
		
		select nrorpt, estado 
		from sce_mch
		where codcct = @codcct and codpro = @codpro and 
		codesp = @codesp and codofi = @codofi and codope = @codope
	end'
END


IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_get_cantidad_inyecciones_pendientes_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].proc_get_cantidad_inyecciones_pendientes_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_cantidad_inyecciones_pendientes_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure proc_get_cantidad_inyecciones_pendientes_MS
				@codcct varchar(3),
				@codpro varchar(2),
				@codesp varchar(2),
				@codofi varchar(3),
				@codope varchar(5)
				AS
	Begin

		--declare @codcct varchar(3)
		--declare @codpro varchar(2)
		--declare @codesp varchar(2)
		--declare @codofi varchar(3)
		--declare @codope varchar(5)

		--set @codcct = ''753''
		--set @codpro = ''20''
		--set @codesp = ''29''
		--set @codofi = ''000''
		--set @codope = ''73372''

		declare @codcct_rel varchar(3)
		declare @codpro_rel varchar(2)
		declare @codesp_rel varchar(2)
		declare @codofi_rel varchar(3)
		declare @codope_rel varchar(5)

		declare @cont_Ori int
		declare @cont_Rel int

		set @cont_Ori = 0
		set @cont_Rel = 0

		--se revisa que exista registros en sce_mch
		if exists ( select codcct from  sce_mch 
					where	codcct = @codcct 
					and codpro = @codpro
					and codesp = @codesp
					and codofi = @codofi
					and codope = @codope)
			begin
				-- determino si existe operacion relacionada
				select 
					@codcct_rel = SUBSTRING(operel,1,3) ,
					@codpro_rel = SUBSTRING(operel,4,2) ,
					@codesp_rel = SUBSTRING(operel,6,2) ,
					@codofi_rel = SUBSTRING(operel,8,3) ,
					@codope_rel = SUBSTRING(operel,11,5) 
				from  sce_mch 
				where	codcct = @codcct 
						and codpro = @codpro
						and codesp = @codesp
						and codofi = @codofi
						and codope = @codope
						and DATALENGTH(operel) > 0

				--determino la cantidad de movimientos que falta por inyectar
				select @cont_Ori = COUNT(enlinea) 
				from  sce_mcd
				where	codcct = @codcct 
						and codpro = @codpro
						and codesp = @codesp
						and codofi = @codofi
						and codope = @codope
						and enlinea = 0
						and estado = 1
						and cod_dh = ''D''
						and nemcta in ( select Nemonico from tbl_nemonico_validacion_inyeccion_swift where Activo = 1 )
		
			end
		else
			begin
		
				-- determino si existe operacion relacionada
				select 
					@codcct_rel = SUBSTRING(operel,1,3) ,
					@codpro_rel = SUBSTRING(operel,4,2) ,
					@codesp_rel = SUBSTRING(operel,6,2) ,
					@codofi_rel = SUBSTRING(operel,8,3) ,
					@codope_rel = SUBSTRING(operel,11,5) 
				from  sce_mchh
				where	codcct = @codcct 
						and codpro = @codpro
						and codesp = @codesp
						and codofi = @codofi
						and codope = @codope
						and DATALENGTH(operel) > 0

				--determino la cantidad de movimientos que falta por inyectar
				select @cont_Ori = COUNT(enlinea) 
				from  sce_mcdh
				where	codcct = @codcct 
						and codpro = @codpro
						and codesp = @codesp
						and codofi = @codofi
						and codope = @codope
						and enlinea = 0
						and estado = 1
						and nemcta in ( select Nemonico from tbl_nemonico_validacion_inyeccion_swift where Activo = 1 )
				
			end

		--Se determina si existe operacion relacionada
		if ISNULL(@codcct_rel,'''') != ''''
			begin
		
				if exists ( select codcct from  
						sce_mch 
						where	codcct = @codcct_rel 
						and codpro = @codpro_rel
						and codesp = @codesp_rel
						and codofi = @codofi_rel
						and codope = @codope_rel)
					begin
				
						--determino la cantidad de movimientos que falta por inyectar
						select @cont_Rel = COUNT(enlinea) 
						from  sce_mcd
						where	codcct = @codcct_rel 
								and codpro = @codpro_rel
								and codesp = @codesp_rel
								and codofi = @codofi_rel
								and codope = @codope_rel
								and enlinea = 0
								and estado = 1
								and cod_dh = ''D''
								and nemcta in ( select Nemonico from tbl_nemonico_validacion_inyeccion_swift where Activo = 1 )
					end
					else
						begin
				
							--determino la cantidad de movimientos que falta por inyectar
						select @cont_Rel = COUNT(enlinea) 
						from  sce_mcdh
						where	codcct = @codcct_rel 
								and codpro = @codpro_rel
								and codesp = @codesp_rel
								and codofi = @codofi_rel
								and codope = @codope_rel
								and enlinea = 0
								and estado = 1
								and cod_dh = ''D''
								and nemcta in ( select Nemonico from tbl_nemonico_validacion_inyeccion_swift where Activo = 1 )
						end
			end

		--reviso si la operacion esta relacionada a otra operacion
		select @cont_Rel = @cont_Rel + COUNT(enlinea) 
			from sce_mch mch
			inner join sce_mcd mcd on mch.codcct = mcd.codcct and mch.codpro = mcd.codpro and mch.codesp = mcd.codesp and mch.codofi = mcd.codofi and mch.codope = mcd.codope
			where	mch.operel = @codcct + @codpro + @codesp + @codofi + @codope
					and mcd.enlinea = 0
					and mcd.estado = 1
					and mcd.cod_dh = ''D''
					and mcd.nemcta in ( select Nemonico from tbl_nemonico_validacion_inyeccion_swift where Activo = 1 )

		select @cont_Rel = @cont_Rel + COUNT(enlinea) 
			from sce_mchh mch
			inner join sce_mcdh mcd on mch.codcct = mcd.codcct and mch.codpro = mcd.codpro and mch.codesp = mcd.codesp and mch.codofi = mcd.codofi and mch.codope = mcd.codope
			where	mch.operel = @codcct + @codpro + @codesp + @codofi + @codope
					and mcd.enlinea = 0
					and mcd.estado = 1
					and mcd.cod_dh = ''D''
					and mcd.nemcta in ( select Nemonico from tbl_nemonico_validacion_inyeccion_swift where Activo = 1 )

		select [Cantidad_Original] = @cont_Ori , [Cantidad_Relacionada] = @cont_Rel

	end'
END
-----------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sel_montoOperacionAnulacionDia' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [proc_sel_montoOperacionAnulacionDia]
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sel_montoOperacionAnulacionDia]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [proc_sel_montoOperacionAnulacionDia] 
	@codcct CHAR(3),
	@codpro CHAR(2),
	@codesp CHAR(2),
	@codofi CHAR(3),
	@codope CHAR(5)
	
AS
BEGIN

	--DECLARE @codcct CHAR(3) SET @codcct = ''753''
	--DECLARE @codpro CHAR(2) SET @codpro = ''20''
	--DECLARE @codesp CHAR(2) SET @codesp = ''29''
	--DECLARE @codofi CHAR(3) SET @codofi = ''000''
	--DECLARE @codope CHAR(5) SET @codope = ''73440''
	-----------------------------------------------------------------------------------------------
	DECLARE @Monto NVARCHAR(MAX) SET @Monto = '''';

	SELECT @Monto = '' -   Monto: ''+ RTRIM(LTRIM(moneda.mnd_mndnmc)) + '' '' + RTRIM(LTRIM(cast(SUM(conta.mtomcd) as nvarchar(max)))) 
	FROM sce_mcd conta 
	INNER JOIN sce_netmnd moneda ON conta.codmon = moneda.mnd_mndcod
	WHERE	conta.codcct = @codcct
	AND conta.codpro = @codpro	AND conta.codesp = @codesp
	AND conta.codofi = @codofi	AND conta.codope = @codope
	AND conta.cod_dh = ''D''
	GROUP BY conta.mtomcd, moneda.mnd_mndnmc

	SELECT @Monto

END
'
END
----------------------------------------------------------------------------------------------------------------
IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE name = 'proc_sce_version_codigo_MS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	DROP PROCEDURE [dbo].proc_sce_version_codigo_MS
END

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_sce_version_codigo_MS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[proc_sce_version_codigo_MS] 
AS
begin
/************************************************************************/
	/*replace-begin*/select ''1.21.1''/*replace-end*/
end'
END

fin_script:
	print 'fin de script Script_Cext01_codigo.sql'
