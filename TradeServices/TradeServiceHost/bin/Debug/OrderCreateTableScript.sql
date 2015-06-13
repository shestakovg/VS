

/****** Object:  Table [dbo].[orderDetail]    Script Date: 12.06.2015 12:55:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[orderDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderUUID] [uniqueidentifier] NULL,
	[skuId] [uniqueidentifier] NULL,
	[qty1] [int] NULL,
	[qty2] [int] NULL,
 CONSTRAINT [PK_orderDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Index [NonClusteredIndex-20150611-173146]    Script Date: 12.06.2015 12:55:32 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20150611-173146] ON [dbo].[orderDetail]
(
	[orderUUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO





/****** Object:  Table [dbo].[orderHeader]    Script Date: 12.06.2015 12:55:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[orderHeader](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[orderUUID] [uniqueidentifier] NULL,
	[outletId] [uniqueidentifier] NULL,
	[orderDate] [datetime] NULL,
	[orderNumber] [int] NULL,
	[notes] [nvarchar](500) NULL,
	[responseText] [nvarchar](500) NULL,
	[_1CDocNumber1] [varchar](100) NULL,
	[_1CDocNumber2] [varchar](100) NULL,
	[payType] [int] NULL,
	[autoLoad] [int] NULL,
	[_send] [int] NULL,
	[_1CDocId1] [uniqueidentifier] NULL,
	[_1CDocId2] [uniqueidentifier] NULL,
	[sendToClient] [int] NULL,
	[sendTime] [datetime] NULL,
	[routeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_orderHeader] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Index [NonClusteredIndex-20150611-201935]    Script Date: 12.06.2015 12:55:57 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20150611-201935] ON [dbo].[orderHeader]
(
	[orderUUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


/****** Object:  Index [NonClusteredIndex-orderHeader]    Script Date: 12.06.2015 12:56:07 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-orderHeader] ON [dbo].[orderHeader]
(
	[orderDate] ASC,
	[_send] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

