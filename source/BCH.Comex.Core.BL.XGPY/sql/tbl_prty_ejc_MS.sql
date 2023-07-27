USE [cext01]
GO

/*	
		Historial:
		2015-10-16		marco.orellana@xemantics.com	Creación tabla intermedia Participante/Especialistas (Microsoft)
*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_prty_ejc_MS](
	[prty_rut] [varchar](12) NOT NULL,
	[ejc_ofi] [numeric](3, 0) NOT NULL,
	[ejc_cod] [numeric](3, 0) NOT NULL,
	[ejc_tpo] [char](1) NOT NULL,
	[create_at] [datetime] NOT NULL,
	[update_at] [datetime] NOT NULL,
 CONSTRAINT [pk_tbl_prty_ejc_MS] PRIMARY KEY NONCLUSTERED 
(
	[prty_rut] ASC,
	[ejc_ofi] ASC,
	[ejc_cod] ASC,
	[ejc_tpo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tbl_prty_ejc_MS] ADD  CONSTRAINT [DF_tbl_prty_ejc_MS_create_at]  DEFAULT (getdate()) FOR [create_at]
GO


