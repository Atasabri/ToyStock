
CREATE TABLE [dbo].[Admins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order_Products]    Script Date: 11/19/2019 10:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Order_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Size] [nvarchar](10) NULL,
	[Color] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/19/2019 10:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[TotalPrice] [float] NOT NULL,
	[Finished] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/19/2019 10:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

GO
INSERT [dbo].[Admins] ([ID], [UserName], [Password]) VALUES (1, N'AtaSabri', N'01142229025')
GO
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Products] ON 

GO
INSERT [dbo].[Order_Products] ([ID], [Order_ID], [Product_ID], [Count], [Size], [Color]) VALUES (5, 2, 1, 5, N'XL', N'Red')
GO
SET IDENTITY_INSERT [dbo].[Order_Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

GO
INSERT [dbo].[Orders] ([ID], [DateTime], [Name], [Phone], [TotalPrice], [Finished]) VALUES (2, CAST(0x0000A9CB00000000 AS DateTime), N'Ata Sabri', N'01142229025', 100, 1)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

GO
INSERT [dbo].[Products] ([ID], [Code], [Name], [Price], [Description]) VALUES (1, N'e.156', N'Product 1', 20, N'test')
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Admins__C9F284560D8F666C]    Script Date: 11/19/2019 10:28:46 AM ******/
ALTER TABLE [dbo].[Admins] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__tmp_ms_x__A25C5AA7D4FE2F19]    Script Date: 11/19/2019 10:28:46 AM ******/
ALTER TABLE [dbo].[Products] ADD UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order_Products]  WITH CHECK ADD  CONSTRAINT [FK__Order_Pro__Order__173876EA] FOREIGN KEY([Order_ID])
REFERENCES [dbo].[Orders] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order_Products] CHECK CONSTRAINT [FK__Order_Pro__Order__173876EA]
GO
ALTER TABLE [dbo].[Order_Products]  WITH CHECK ADD  CONSTRAINT [FK__Order_Pro__Produ__182C9B23] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Products] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order_Products] CHECK CONSTRAINT [FK__Order_Pro__Produ__182C9B23]
GO
USE [master]
GO
ALTER DATABASE [ToyStock] SET  READ_WRITE 
GO
