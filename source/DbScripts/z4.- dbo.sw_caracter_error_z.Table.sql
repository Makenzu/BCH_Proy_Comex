USE [swift]
GO
/****** Object:  Table [dbo].[sw_caracter_error_z]    Script Date: 11/05/2018 10:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id('sw_caracter_error_z') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE sw_caracter_error_z
GO
CREATE TABLE [dbo].[sw_caracter_error_z](
	[valor_ascii] [int] NOT NULL,
	[caracter] [varchar](1) NULL,
	[descripcion] [varchar](20) NULL,
 CONSTRAINT [PK_sw_caracter_error_z] PRIMARY KEY NONCLUSTERED 
(
	[valor_ascii] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
