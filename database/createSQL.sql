USE [master]
GO
/****** Object:  Database [SimpleFinanceSys]    Script Date: 2020/2/16 23:33:20 ******/
CREATE DATABASE [SimpleFinanceSys]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimpleFinanceSys', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SimpleFinanceSys.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SimpleFinanceSys_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SimpleFinanceSys_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SimpleFinanceSys] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleFinanceSys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimpleFinanceSys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SimpleFinanceSys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleFinanceSys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleFinanceSys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SimpleFinanceSys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleFinanceSys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimpleFinanceSys] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleFinanceSys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleFinanceSys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimpleFinanceSys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimpleFinanceSys] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SimpleFinanceSys] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SimpleFinanceSys] SET QUERY_STORE = OFF
GO
USE [SimpleFinanceSys]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SimpleFinanceSys]
GO
/****** Object:  Table [dbo].[Base_Object]    Script Date: 2020/2/16 23:33:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Base_Object](
	[id] [varchar](128) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user] [varchar](128) NOT NULL,
	[last_time] [datetime] NULL,
	[last_user] [varchar](128) NULL,
	[state] [int] NOT NULL,
	[name] [varchar](128) NOT NULL,
	[explain] [varchar](1024) NULL,
	[sort] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Base_Type]    Script Date: 2020/2/16 23:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Base_Type](
	[id] [varchar](128) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user] [varchar](128) NOT NULL,
	[last_time] [datetime] NULL,
	[last_user] [varchar](128) NULL,
	[state] [int] NOT NULL,
	[name] [varchar](32) NOT NULL,
	[sort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeRecord]    Script Date: 2020/2/16 23:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeRecord](
	[id] [varchar](128) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user] [varchar](128) NOT NULL,
	[last_time] [datetime] NULL,
	[last_user] [varchar](128) NULL,
	[state] [int] NOT NULL,
	[orderId] [varchar](64) NOT NULL,
	[orderType] [int] NOT NULL,
	[payType] [varchar](128) NOT NULL,
	[beforeAmount] [decimal](18, 2) NOT NULL,
	[afterAmount] [decimal](18, 2) NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[payObject] [varchar](128) NULL,
	[remark] [varchar](4096) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanRecored]    Script Date: 2020/2/16 23:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanRecored](
	[id] [varchar](128) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user] [varchar](128) NOT NULL,
	[last_time] [datetime] NULL,
	[last_user] [varchar](128) NULL,
	[state] [int] NOT NULL,
	[recordType] [int] NOT NULL,
	[orderId] [varchar](50) NOT NULL,
	[topOrderId] [varchar](50) NULL,
	[title] [varchar](128) NOT NULL,
	[orderObject] [varchar](128) NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[interestRateType] [varchar](128) NULL,
	[interestRate] [decimal](7, 4) NULL,
	[endTime] [datetime] NULL,
	[nowAmount] [decimal](18, 2) NULL,
	[isFinish] [smallint] NULL,
	[remark] [varchar](4096) NULL,
 CONSTRAINT [PK__LoanReco__3213E83FDF7A5B7D] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2020/2/16 23:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[id] [varchar](128) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[last_time] [datetime] NULL,
	[last_user] [varchar](128) NULL,
	[state] [int] NOT NULL,
	[username] [varchar](64) NOT NULL,
	[userid] [varchar](64) NOT NULL,
	[email] [varchar](128) NULL,
	[pwd] [varchar](128) NOT NULL,
	[headUrl] [varchar](max) NULL,
	[dayMaxPay] [decimal](18, 2) NOT NULL,
	[monthMaxPay] [decimal](18, 2) NOT NULL,
	[lastSignTime] [datetime] NULL,
	[tipsState] [int] NOT NULL,
	[baseAmount] [decimal](18, 2) NOT NULL,
	[nowMoney] [decimal](18, 2) NOT NULL,
	[create_user] [varchar](128) NULL,
 CONSTRAINT [PK__UserInfo__3213E83F813DED77] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'101adda2-c7f3-4640-9ce0-b434a4adcf81', CAST(N'2020-02-11T14:39:10.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', CAST(N'2020-02-11T14:44:56.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', 0, N'大润发超市', N'大润发超市购物消费', 10)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'5f1ca4e0-7061-48f0-b8ac-7756e1113863', CAST(N'2020-02-16T16:22:15.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'工商银行', N'工商银行', 40)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'6df7a337-bfa0-4685-9dfd-cc58204cd977', CAST(N'2020-02-16T16:21:31.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'支付宝', N'支付宝', 1)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'82a7fc16-7426-4af1-8c86-3d04f29f8795', CAST(N'2020-02-16T16:21:47.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'微信', N'微信支付', 3)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'851991ab-d8fd-45d3-8a99-21aea505b2f4', CAST(N'2020-02-16T17:28:05.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'支付宝借呗', N'支付宝借呗', 101)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'8d865dad-ff4f-4e2d-9a65-2c3432e710ec', CAST(N'2020-02-16T16:21:14.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'支付宝花呗', N'支付宝花呗', 2)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'cb32e979-ad07-4716-b8e6-cc776a769fed', CAST(N'2020-02-11T14:45:33.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'每一天餐厅', N'饮食', 30)
INSERT [dbo].[Base_Object] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [explain], [sort]) VALUES (N'f4d577cb-232f-45e1-a81c-dfed7549cf2b', CAST(N'2020-02-16T17:13:56.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'家人', NULL, 100)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'508636ed-f49d-4136-9c37-ac4ed09db2a6', CAST(N'2020-02-11T14:01:47.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', CAST(N'2020-02-11T14:24:28.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', 0, N'饮食', 1)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'5696085b-ba41-4667-b1e8-cd19a8ad9031', CAST(N'2020-02-16T17:25:22.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'错账处理', 40)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'67754baa-c5fe-429b-936d-4ea691faf867', CAST(N'2020-02-16T17:05:57.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 1, N'借款收入', 9999999)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'8aca9b90-24ce-4bc0-a338-7c0491d56c76', CAST(N'2020-02-16T17:05:57.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 1, N'取款收入', 9999999)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'8fcf8b59-15eb-4309-a78b-9475f8ce5b6f', CAST(N'2020-02-11T14:25:36.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'日用品', 2)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'a3740b2c-7547-439f-9ee7-62311cadb5a8', CAST(N'2020-02-16T17:05:57.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 1, N'转存支出', 9999999)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'ae5cec74-4bf3-46e4-8a68-97e07171ae42', CAST(N'2020-02-16T16:48:21.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', CAST(N'2020-02-16T16:48:39.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', 0, N'生活费', 10)
INSERT [dbo].[Base_Type] ([id], [create_time], [create_user], [last_time], [last_user], [state], [name], [sort]) VALUES (N'fa6d37d0-0cd2-47b8-a663-cc1219ba574b', CAST(N'2020-02-16T17:05:57.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 1, N'还款支出', 9999999)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'01b5ef71-3acb-4673-a98c-f8fa2c365930', CAST(N'2020-02-15T10:59:01.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200215105901264001', 1, N'508636ed-f49d-4136-9c37-ac4ed09db2a6', CAST(100.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'cb32e979-ad07-4716-b8e6-cc776a769fed', N'早餐')
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'2af44093-aaa2-4533-849d-3f0976634910', CAST(N'2020-02-15T10:58:48.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200215105848595001', 1, N'508636ed-f49d-4136-9c37-ac4ed09db2a6', CAST(110.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'cb32e979-ad07-4716-b8e6-cc776a769fed', N'错账抹平10元')
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'3e6affd9-a021-4bba-9861-ba233e99f8f2', CAST(N'2020-02-16T17:26:43.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216172642995001', 1, N'a3740b2c-7547-439f-9ee7-62311cadb5a8', CAST(1390.00 AS Decimal(18, 2)), CAST(1090.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'6df7a337-bfa0-4685-9dfd-cc58204cd977', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'494085dd-e74d-4f41-bcee-7bbbf4aefb38', CAST(N'2020-02-16T17:28:59.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216172859789001', 0, N'67754baa-c5fe-429b-936d-4ea691faf867', CAST(90.00 AS Decimal(18, 2)), CAST(1105.00 AS Decimal(18, 2)), CAST(1015.00 AS Decimal(18, 2)), N'851991ab-d8fd-45d3-8a99-21aea505b2f4', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'4b640042-b465-4b8e-a01c-9c03e81c6bff', CAST(N'2020-02-10T13:04:43.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200210130443065001', 2, N' ', CAST(500.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'6224e52c-8601-4ac9-85e5-733df6f2ebcb', CAST(N'2020-02-16T17:32:53.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216173253135001', 1, N'fa6d37d0-0cd2-47b8-a663-cc1219ba574b', CAST(1105.00 AS Decimal(18, 2)), CAST(1090.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'851991ab-d8fd-45d3-8a99-21aea505b2f4', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'6cc4e0b4-18f5-4fc2-bba8-2748befb2a07', CAST(N'2020-02-16T17:24:27.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216172427520001', 0, N'8aca9b90-24ce-4bc0-a338-7c0491d56c76', CAST(390.00 AS Decimal(18, 2)), CAST(1390.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), N'6df7a337-bfa0-4685-9dfd-cc58204cd977', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'6fb5d669-7be1-42ca-92d9-52baf7db34df', CAST(N'2020-02-16T16:49:37.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216164937510001', 1, N'a3740b2c-7547-439f-9ee7-62311cadb5a8', CAST(1390.00 AS Decimal(18, 2)), CAST(390.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), N'6df7a337-bfa0-4685-9dfd-cc58204cd977', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'7a8e32ca-b452-4248-9cb8-505dead351db', CAST(N'2020-02-10T13:05:58.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200210130558947001', 2, N' ', CAST(100.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), CAST(400.00 AS Decimal(18, 2)), N' ', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'7ab39163-daf0-4a6d-95d3-84a9361d85ff', CAST(N'2020-02-10T13:06:05.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200210130605714001', 2, N' ', CAST(500.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(-400.00 AS Decimal(18, 2)), N' ', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'7dd42daf-8e16-4a8e-9901-763b2d4d91a7', CAST(N'2020-02-16T17:26:04.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216172604150001', 0, N'ae5cec74-4bf3-46e4-8a68-97e07171ae42', CAST(90.00 AS Decimal(18, 2)), CAST(1390.00 AS Decimal(18, 2)), CAST(1300.00 AS Decimal(18, 2)), N'f4d577cb-232f-45e1-a81c-dfed7549cf2b', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'91b1bd24-6d2a-4d98-83e7-564fabffbe59', CAST(N'2020-02-16T17:27:20.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216172720299001', 1, N'a3740b2c-7547-439f-9ee7-62311cadb5a8', CAST(1090.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), N'5f1ca4e0-7061-48f0-b8ac-7756e1113863', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'a4081e23-3b2b-46cf-a2db-edb0e5d1581f', CAST(N'2020-02-16T17:25:41.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216172541453001', 1, N'5696085b-ba41-4667-b1e8-cd19a8ad9031', CAST(1390.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), CAST(1300.00 AS Decimal(18, 2)), N'6df7a337-bfa0-4685-9dfd-cc58204cd977', N'错账回退')
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'ac881bf6-6add-4afe-8fd4-78ea2e414c73', CAST(N'2020-02-10T13:05:17.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200210130517470001', 2, N' ', CAST(500.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(400.00 AS Decimal(18, 2)), N' ', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'acce8218-05bd-4b47-818a-4fb92de11fc5', CAST(N'2020-02-10T13:04:43.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200210130443512001', 2, N' ', CAST(500.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', NULL)
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'de581196-2917-4dea-815c-e5b4e0830ba2', CAST(N'2020-02-15T10:57:48.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200215105748325001', 0, N'508636ed-f49d-4136-9c37-ac4ed09db2a6', CAST(100.00 AS Decimal(18, 2)), CAST(110.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'cb32e979-ad07-4716-b8e6-cc776a769fed', N'早餐')
INSERT [dbo].[ChangeRecord] ([id], [create_time], [create_user], [last_time], [last_user], [state], [orderId], [orderType], [payType], [beforeAmount], [afterAmount], [amount], [payObject], [remark]) VALUES (N'f63dd720-a2df-4a27-b370-688af126d92e', CAST(N'2020-02-16T16:48:54.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, N'20200216164854345001', 0, N'ae5cec74-4bf3-46e4-8a68-97e07171ae42', CAST(90.00 AS Decimal(18, 2)), CAST(1390.00 AS Decimal(18, 2)), CAST(1300.00 AS Decimal(18, 2)), N'6df7a337-bfa0-4685-9dfd-cc58204cd977', NULL)
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'46dac8fd-2bcf-47b2-a471-d2913b780764', CAST(N'2020-02-16T17:32:53.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 3, N'20200216173253135001', N'20200216172859789001', N'抹平利息', N'851991ab-d8fd-45d3-8a99-21aea505b2f4', CAST(15.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, NULL, 0, N'抹平利息，30天共计15元')
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'5a95a83c-927d-43d5-b23a-0549a3fb1ace', CAST(N'2020-02-16T16:49:37.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 0, N'20200216164937510001', NULL, N'现金转存', N'6df7a337-bfa0-4685-9dfd-cc58204cd977', CAST(1000.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, NULL, 0, N'现金转存到支付宝')
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'63168978-d938-40c9-b863-5d9fa9d09593', CAST(N'2020-02-16T17:24:27.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 2, N'20200216172427520001', NULL, N'错账回退', N'6df7a337-bfa0-4685-9dfd-cc58204cd977', CAST(1000.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, NULL, 0, N'错账回退')
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'89afadcd-ca58-44f7-8af5-781f57c52320', CAST(N'2020-02-16T17:26:42.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 0, N'20200216172642995001', NULL, N'现金转入支付宝', N'6df7a337-bfa0-4685-9dfd-cc58204cd977', CAST(300.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, NULL, 0, NULL)
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'cda7078d-dc41-4479-a22f-568410af8274', CAST(N'2020-02-16T16:24:54.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 0, N'20200216162454784001', NULL, N'剩余伙食费', N'5f1ca4e0-7061-48f0-b8ac-7756e1113863', CAST(300.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, NULL, 0, N'2月剩余伙食费')
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'e0607574-96ae-47c2-bd17-7e5a1dc89728', CAST(N'2020-02-16T17:28:59.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 1, N'20200216172859789001', NULL, N'借呗提现', N'851991ab-d8fd-45d3-8a99-21aea505b2f4', CAST(1015.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, CAST(1000.00 AS Decimal(18, 2)), 0, N'借呗提现,含15元手续费')
INSERT [dbo].[LoanRecored] ([id], [create_time], [create_user], [last_time], [last_user], [state], [recordType], [orderId], [topOrderId], [title], [orderObject], [amount], [interestRateType], [interestRate], [endTime], [nowAmount], [isFinish], [remark]) VALUES (N'f1851f90-80b5-4124-bef8-9dda44ac68d7', CAST(N'2020-02-16T17:27:20.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', NULL, NULL, 0, 0, N'20200216172720299001', NULL, N'现金转入银行卡', N'5f1ca4e0-7061-48f0-b8ac-7756e1113863', CAST(1000.00 AS Decimal(18, 2)), N'0', CAST(0.0000 AS Decimal(7, 4)), NULL, NULL, 0, NULL)
INSERT [dbo].[UserInfo] ([id], [create_time], [last_time], [last_user], [state], [username], [userid], [email], [pwd], [headUrl], [dayMaxPay], [monthMaxPay], [lastSignTime], [tipsState], [baseAmount], [nowMoney], [create_user]) VALUES (N'22f520ed-3298-4bf5-8b9e-aa0a4303b106', CAST(N'2020-02-09T10:57:43.000' AS DateTime), NULL, NULL, 0, N'user1@123.com', N'user1', N'user1@123.com', N'86EAF068979CC4005272DE418677ACF0', NULL, CAST(9999.00 AS Decimal(18, 2)), CAST(99999.00 AS Decimal(18, 2)), CAST(N'1970-01-01T00:00:00.000' AS DateTime), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[UserInfo] ([id], [create_time], [last_time], [last_user], [state], [username], [userid], [email], [pwd], [headUrl], [dayMaxPay], [monthMaxPay], [lastSignTime], [tipsState], [baseAmount], [nowMoney], [create_user]) VALUES (N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', CAST(N'2020-02-09T10:56:16.000' AS DateTime), CAST(N'2020-02-10T13:06:05.000' AS DateTime), N'3fbdb382-fd09-40b7-85a0-7646d8fa9c5f', 1, N'admin@123.com', N'admin', N'admin@123.com', N'A38874A8768617193AD79A3550BC86F7', N'/Content/TempFile/f432ae2b-66d3-47af-aea9-8ad9c3c0622b.jpg', CAST(9999.00 AS Decimal(18, 2)), CAST(99999.00 AS Decimal(18, 2)), CAST(N'1970-01-01T00:00:00.000' AS DateTime), 0, CAST(100.00 AS Decimal(18, 2)), CAST(1090.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[UserInfo] ([id], [create_time], [last_time], [last_user], [state], [username], [userid], [email], [pwd], [headUrl], [dayMaxPay], [monthMaxPay], [lastSignTime], [tipsState], [baseAmount], [nowMoney], [create_user]) VALUES (N'be729655-eeaf-484b-8b50-3b894ee8025e', CAST(N'2020-02-14T19:45:34.000' AS DateTime), NULL, NULL, 0, N'li@163.com', N'li', N'li@163.com', N'86EAF068979CC4005272DE418677ACF0', NULL, CAST(9999.00 AS Decimal(18, 2)), CAST(99999.00 AS Decimal(18, 2)), CAST(N'1970-01-01T00:00:00.000' AS DateTime), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL)
ALTER TABLE [dbo].[Base_Object] ADD  DEFAULT (CONVERT([varchar](32),getdate(),(120))) FOR [create_time]
GO
ALTER TABLE [dbo].[Base_Object] ADD  DEFAULT ((0)) FOR [state]
GO
ALTER TABLE [dbo].[Base_Type] ADD  DEFAULT (CONVERT([varchar](32),getdate(),(120))) FOR [create_time]
GO
ALTER TABLE [dbo].[Base_Type] ADD  DEFAULT ((0)) FOR [state]
GO
ALTER TABLE [dbo].[ChangeRecord] ADD  DEFAULT (CONVERT([varchar](32),getdate(),(120))) FOR [create_time]
GO
ALTER TABLE [dbo].[ChangeRecord] ADD  DEFAULT ((0)) FOR [state]
GO
ALTER TABLE [dbo].[LoanRecored] ADD  CONSTRAINT [DF__LoanRecor__creat__4CA06362]  DEFAULT (CONVERT([varchar](32),getdate(),(120))) FOR [create_time]
GO
ALTER TABLE [dbo].[LoanRecored] ADD  CONSTRAINT [DF__LoanRecor__state__4D94879B]  DEFAULT ((0)) FOR [state]
GO
ALTER TABLE [dbo].[LoanRecored] ADD  CONSTRAINT [DF__LoanRecor__inter__4F7CD00D]  DEFAULT ((0)) FOR [interestRate]
GO
ALTER TABLE [dbo].[LoanRecored] ADD  CONSTRAINT [DF__LoanRecor__isFin__5070F446]  DEFAULT ((0)) FOR [isFinish]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__create__37A5467C]  DEFAULT (CONVERT([varchar](32),getdate(),(120))) FOR [create_time]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__state__38996AB5]  DEFAULT ((0)) FOR [state]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__dayMax__398D8EEE]  DEFAULT ((9999)) FOR [dayMaxPay]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__monthM__3A81B327]  DEFAULT ((99999)) FOR [monthMaxPay]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__tipsSt__3B75D760]  DEFAULT ((0)) FOR [tipsState]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__baseAm__3C69FB99]  DEFAULT ((0)) FOR [baseAmount]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF__UserInfo__nowMon__3D5E1FD2]  DEFAULT ((0)) FOR [nowMoney]
GO
ALTER TABLE [dbo].[ChangeRecord]  WITH CHECK ADD CHECK  (([orderType]=(0) OR [orderType]=(1) OR [orderType]=(2)))
GO
ALTER TABLE [dbo].[LoanRecored]  WITH CHECK ADD  CONSTRAINT [CK__LoanRecor__recor__4E88ABD4] CHECK  (([recordType]=(3) OR [recordType]=(2) OR [recordType]=(1) OR [recordType]=(0)))
GO
ALTER TABLE [dbo].[LoanRecored] CHECK CONSTRAINT [CK__LoanRecor__recor__4E88ABD4]
GO
USE [master]
GO
ALTER DATABASE [SimpleFinanceSys] SET  READ_WRITE 
GO
