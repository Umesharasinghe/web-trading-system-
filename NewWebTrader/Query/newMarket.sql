USE [WebTraderNewDB]
GO

/****** Object:  Table [dbo].[NewMarkets]    Script Date: 1/21/2018 7:23:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewMarkets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](max) NULL,
	[date] [datetime] NOT NULL,
	[time] [datetime] NOT NULL,
	[openingPrice] [real] NOT NULL,
	[highestPrice] [real] NOT NULL,
	[closingPrice] [real] NOT NULL,
	[lowestPrice] [real] NOT NULL,
	[temp] [real] NOT NULL,
 CONSTRAINT [PK_dbo.NewMarkets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


