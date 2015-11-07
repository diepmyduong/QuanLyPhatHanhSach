USE [master]
GO
/****** Object:  Database [QLPHS]    Script Date: 5/11/2015 23:05:46 PM ******/
CREATE DATABASE [QLPHS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLPHS', FILENAME = N'E:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLPHS.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLPHS_log', FILENAME = N'E:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLPHS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLPHS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLPHS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLPHS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLPHS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLPHS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLPHS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLPHS] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLPHS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLPHS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLPHS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLPHS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLPHS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLPHS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLPHS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLPHS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLPHS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLPHS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLPHS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLPHS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLPHS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLPHS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLPHS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLPHS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLPHS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLPHS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLPHS] SET  MULTI_USER 
GO
ALTER DATABASE [QLPHS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLPHS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLPHS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLPHS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLPHS] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QLPHS]
GO
/****** Object:  User [jerry]    Script Date: 5/11/2015 23:05:46 PM ******/
CREATE USER [jerry] FOR LOGIN [jerry] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [jerry]
GO
/****** Object:  Table [dbo].[CHITIETHOADONDAILY]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETHOADONDAILY](
	[masohoadon] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[dongia] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CHITIETHOADONDAILY] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHITIETHOADONNXB]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETHOADONNXB](
	[masohoadon] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[dongia] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CHITIETHOADONNXB] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHITIETPHIEUNHAP]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETPHIEUNHAP](
	[masophieunhap] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[dongia] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CHITIETPHIEUNHAP] PRIMARY KEY CLUSTERED 
(
	[masophieunhap] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHITIETPHIEUXUAT]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETPHIEUXUAT](
	[masophieuxuat] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[dongia] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CHITIETPHIEUXUAT] PRIMARY KEY CLUSTERED 
(
	[masophieuxuat] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONGNODAILY]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONGNODAILY](
	[masosach] [int] NOT NULL,
	[masodaily] [int] NOT NULL,
	[thang] [date] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[dongia] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CONGNODAILY] PRIMARY KEY CLUSTERED 
(
	[masosach] ASC,
	[masodaily] ASC,
	[thang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONGNONXB]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONGNONXB](
	[masosach] [int] NOT NULL,
	[masonxb] [int] NOT NULL,
	[thang] [date] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[dongia] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CONGNONXB] PRIMARY KEY CLUSTERED 
(
	[masosach] ASC,
	[masonxb] ASC,
	[thang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DAILY]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DAILY](
	[masodaily] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[diachi] [nvarchar](50) NOT NULL,
	[sodienthoai] [nchar](15) NOT NULL,
	[sotaikhoan] [nchar](10) NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_DAILY] PRIMARY KEY CLUSTERED 
(
	[masodaily] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HOADONDAILY]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADONDAILY](
	[masohoadon] [int] IDENTITY(1,1) NOT NULL,
	[masodaily] [int] NOT NULL,
	[ngaylap] [date] NOT NULL,
	[tongtien] [decimal](18, 0) NOT NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_HOADONDAILY] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HOADONNXB]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADONNXB](
	[masohoadon] [int] IDENTITY(1,1) NOT NULL,
	[masonxb] [int] NOT NULL,
	[ngaylap] [date] NOT NULL,
	[tongtien] [decimal](18, 0) NOT NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_HOADONNXB] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LINHVUC]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LINHVUC](
	[masolinhvuc] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_LINHVUC] PRIMARY KEY CLUSTERED 
(
	[masolinhvuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NXB]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NXB](
	[masonxb] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[diachi] [nvarchar](50) NOT NULL,
	[sodienthoai] [nchar](15) NOT NULL,
	[sotaikhoan] [nchar](10) NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_NXB] PRIMARY KEY CLUSTERED 
(
	[masonxb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHIEUNHAP]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUNHAP](
	[masophieunhap] [int] IDENTITY(1,1) NOT NULL,
	[masonxb] [int] NOT NULL,
	[nguoigiaosach] [nvarchar](50) NOT NULL,
	[ngaylap] [date] NOT NULL,
	[tongtien] [decimal](18, 0) NOT NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_PHIEUNHAP] PRIMARY KEY CLUSTERED 
(
	[masophieunhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHIEUXUAT]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUXUAT](
	[masophieuxuat] [int] IDENTITY(1,1) NOT NULL,
	[masodaily] [int] NOT NULL,
	[nguoinhasach] [nvarchar](50) NOT NULL,
	[ngaylap] [date] NOT NULL,
	[tongtien] [decimal](18, 0) NOT NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_PHIEUXUAT] PRIMARY KEY CLUSTERED 
(
	[masophieuxuat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SACH]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SACH](
	[masosach] [int] IDENTITY(1,1) NOT NULL,
	[masonxb] [int] NOT NULL,
	[masolinhvuc] [int] NOT NULL,
	[tensach] [nvarchar](50) NOT NULL,
	[tacgia] [nvarchar](50) NOT NULL,
	[giaban] [decimal](18, 0) NOT NULL,
	[gianhap] [decimal](18, 0) NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
	[hinhanh] [text] NULL,
	[trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_SACH] PRIMARY KEY CLUSTERED 
(
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[THEKHO]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THEKHO](
	[ngayghi] [date] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_THEKHO] PRIMARY KEY CLUSTERED 
(
	[ngayghi] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[DANHMUCSACH]    Script Date: 5/11/2015 23:05:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DANHMUCSACH]
AS
SELECT        TOP (100) PERCENT dbo.SACH.masosach, dbo.SACH.tensach, dbo.LINHVUC.ten, dbo.SACH.tacgia, dbo.NXB.ten AS Expr3, dbo.SACH.soluong, dbo.SACH.giaban, dbo.SACH.gianhap
FROM            dbo.LINHVUC INNER JOIN
                         dbo.SACH ON dbo.LINHVUC.masolinhvuc = dbo.SACH.masolinhvuc INNER JOIN
                         dbo.NXB ON dbo.SACH.masonxb = dbo.NXB.masonxb
ORDER BY dbo.SACH.masosach

GO
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (1, 2, CAST(30 AS Decimal(18, 0)), CAST(32000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (1, 12, CAST(8 AS Decimal(18, 0)), CAST(41000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (2, 3, CAST(10 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (2, 8, CAST(20 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (3, 3, CAST(5 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (3, 13, CAST(2 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (4, 8, CAST(10 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (4, 13, CAST(19 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (5, 3, CAST(5 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONDAILY] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (5, 13, CAST(1 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (1, 2, CAST(30 AS Decimal(18, 0)), CAST(32000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (2, 8, CAST(20 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (3, 8, CAST(10 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (4, 3, CAST(5 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (5, 3, CAST(5 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (6, 3, CAST(10 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (7, 13, CAST(2 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (8, 13, CAST(1 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (9, 13, CAST(19 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETHOADONNXB] ([masohoadon], [masosach], [soluong], [dongia]) VALUES (10, 12, CAST(8 AS Decimal(18, 0)), CAST(41000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (1, 13, CAST(22 AS Decimal(18, 0)), CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (1, 18, CAST(34 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (2, 4, CAST(53 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (2, 12, CAST(13 AS Decimal(18, 0)), CAST(24000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (3, 2, CAST(37 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (3, 25, CAST(60 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (4, 8, CAST(48 AS Decimal(18, 0)), CAST(26000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (4, 11, CAST(98 AS Decimal(18, 0)), CAST(19000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (5, 6, CAST(37 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (6, 3, CAST(91 AS Decimal(18, 0)), CAST(22000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (6, 10, CAST(27 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (7, 10, CAST(75 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (7, 15, CAST(89 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (8, 2, CAST(99 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (8, 27, CAST(41 AS Decimal(18, 0)), CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (9, 13, CAST(27 AS Decimal(18, 0)), CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (9, 18, CAST(79 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (10, 24, CAST(55 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUNHAP] ([masophieunhap], [masosach], [soluong], [dongia]) VALUES (10, 26, CAST(73 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (1, 4, CAST(53 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (1, 10, CAST(27 AS Decimal(18, 0)), CAST(39000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (2, 2, CAST(37 AS Decimal(18, 0)), CAST(32000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (2, 12, CAST(10 AS Decimal(18, 0)), CAST(41000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (3, 3, CAST(10 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (3, 8, CAST(20 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (4, 3, CAST(10 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (4, 13, CAST(3 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (5, 8, CAST(10 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETPHIEUXUAT] ([masophieuxuat], [masosach], [soluong], [dongia]) VALUES (5, 13, CAST(19 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (2, 3, CAST(N'2012-04-03' AS Date), CAST(7 AS Decimal(18, 0)), CAST(32000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (3, 4, CAST(N'2012-09-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (3, 7, CAST(N'2012-07-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (4, 3, CAST(N'2012-07-03' AS Date), CAST(53 AS Decimal(18, 0)), CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (8, 6, CAST(N'2012-05-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(42000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (8, 7, CAST(N'2012-07-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(37000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (10, 3, CAST(N'2012-07-03' AS Date), CAST(27 AS Decimal(18, 0)), CAST(39000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (12, 3, CAST(N'2012-04-03' AS Date), CAST(2 AS Decimal(18, 0)), CAST(41000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (13, 4, CAST(N'2012-09-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNODAILY] ([masosach], [masodaily], [thang], [soluong], [dongia]) VALUES (13, 6, CAST(N'2012-05-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(31000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (2, 3, CAST(N'2012-03-06' AS Date), CAST(7 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (2, 3, CAST(N'2012-08-17' AS Date), CAST(99 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (3, 6, CAST(N'2012-06-12' AS Date), CAST(71 AS Decimal(18, 0)), CAST(22000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (4, 10, CAST(N'2012-02-23' AS Date), CAST(53 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (6, 1, CAST(N'2012-05-16' AS Date), CAST(37 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (8, 5, CAST(N'2012-04-13' AS Date), CAST(18 AS Decimal(18, 0)), CAST(26000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (10, 6, CAST(N'2012-06-12' AS Date), CAST(27 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (10, 6, CAST(N'2012-07-14' AS Date), CAST(75 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (11, 5, CAST(N'2012-04-13' AS Date), CAST(98 AS Decimal(18, 0)), CAST(19000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (12, 10, CAST(N'2012-02-23' AS Date), CAST(1 AS Decimal(18, 0)), CAST(24000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (13, 9, CAST(N'2012-01-03' AS Date), CAST(0 AS Decimal(18, 0)), CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (13, 9, CAST(N'2012-09-08' AS Date), CAST(27 AS Decimal(18, 0)), CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (15, 6, CAST(N'2012-07-14' AS Date), CAST(89 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (18, 9, CAST(N'2012-01-03' AS Date), CAST(34 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (18, 9, CAST(N'2012-09-08' AS Date), CAST(79 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (24, 10, CAST(N'2012-10-21' AS Date), CAST(55 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (25, 3, CAST(N'2012-03-06' AS Date), CAST(60 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (26, 10, CAST(N'2012-10-21' AS Date), CAST(73 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)))
INSERT [dbo].[CONGNONXB] ([masosach], [masonxb], [thang], [soluong], [dongia]) VALUES (27, 3, CAST(N'2012-08-17' AS Date), CAST(41 AS Decimal(18, 0)), CAST(11000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[DAILY] ON 

INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (1, N'Hoa H?ng', N'Qu?n 1', N'01299586325    ', N'0222658942', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (2, N'Tien Kirschke', N'Quận 1', N'01219563460    ', N'0228512185', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (3, N'Tỷ Phú', N'Quận 2', N'01249658956    ', N'0228259347', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (4, N'An Khánh', N'Quận 3', N'01264126539    ', N'0223471394', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (5, N'Hùng Vương', N'Quận 4', N'01216123452    ', N'0228427333', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (6, N'Trường Trinh', N'Quận 5', N'01299013599    ', N'0228808880', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (7, N'Lê Hồng Phong', N'Quận 6', N'01295068925    ', N'0226084836', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (8, N'Lê Quý Đôn', N'Quận 7', N'01282737304    ', N'0229038522', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (9, N'Đăng Quang', N'Quận 8', N'01226705332    ', N'0228714199', NULL)
INSERT [dbo].[DAILY] ([masodaily], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (10, N'Trần Hưng Đạo', N'Quận 9', N'01297137095    ', N'0222842562', NULL)
SET IDENTITY_INSERT [dbo].[DAILY] OFF
SET IDENTITY_INSERT [dbo].[HOADONDAILY] ON 

INSERT [dbo].[HOADONDAILY] ([masohoadon], [masodaily], [ngaylap], [tongtien], [trangthai]) VALUES (1, 3, CAST(N'2012-05-03' AS Date), CAST(10000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONDAILY] ([masohoadon], [masodaily], [ngaylap], [tongtien], [trangthai]) VALUES (2, 7, CAST(N'2012-08-03' AS Date), CAST(10000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONDAILY] ([masohoadon], [masodaily], [ngaylap], [tongtien], [trangthai]) VALUES (3, 4, CAST(N'2012-10-03' AS Date), CAST(10000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONDAILY] ([masohoadon], [masodaily], [ngaylap], [tongtien], [trangthai]) VALUES (4, 6, CAST(N'2012-06-03' AS Date), CAST(10000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONDAILY] ([masohoadon], [masodaily], [ngaylap], [tongtien], [trangthai]) VALUES (5, 4, CAST(N'2012-11-03' AS Date), CAST(10000 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[HOADONDAILY] OFF
SET IDENTITY_INSERT [dbo].[HOADONNXB] ON 

INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (1, 3, CAST(N'2012-05-15' AS Date), CAST(960000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (2, 5, CAST(N'2012-08-15' AS Date), CAST(840000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (3, 5, CAST(N'2012-06-15' AS Date), CAST(420000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (4, 6, CAST(N'2012-11-15' AS Date), CAST(200000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (5, 6, CAST(N'2012-10-15' AS Date), CAST(200000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (6, 6, CAST(N'2012-08-15' AS Date), CAST(400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (7, 9, CAST(N'2012-10-15' AS Date), CAST(62000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (8, 9, CAST(N'2012-11-15' AS Date), CAST(31000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (9, 9, CAST(N'2012-06-15' AS Date), CAST(589000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HOADONNXB] ([masohoadon], [masonxb], [ngaylap], [tongtien], [trangthai]) VALUES (10, 10, CAST(N'2012-05-15' AS Date), CAST(328000 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[HOADONNXB] OFF
SET IDENTITY_INSERT [dbo].[LINHVUC] ON 

INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (1, N'Kinh dị', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (2, N'Hài', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (3, N'Khoa Học', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (4, N'Truyện tranh', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (5, N'Toán', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (6, N'Vinh2', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (7, N'Vinh', NULL)
INSERT [dbo].[LINHVUC] ([masolinhvuc], [ten], [trangthai]) VALUES (8, N'Vinh3', NULL)
SET IDENTITY_INSERT [dbo].[LINHVUC] OFF
SET IDENTITY_INSERT [dbo].[NXB] ON 

INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (1, N'Kim Đồng', N'Quận 1', N'01248536875    ', N'0222856742', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (2, N'Deshawn Clubb', N'Quận 2', N'01248242278    ', N'0222780700', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (3, N'Delta Tippy', N'Quận 3', N'01256107409    ', N'0221609067', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (4, N'Otto Milot', N'Quận 4', N'01274645693    ', N'0222444265', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (5, N'Doreatha Balazs', N'Quận 5', N'01239818131    ', N'0226127056', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (6, N'Kamala Challenger', N'Quận 6', N'01243253019    ', N'0227149814', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (7, N'Violeta Schartz', N'Quận 7', N'01287860636    ', N'0228958934', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (8, N'Gene Luckman', N'Quận 8', N'01272974802    ', N'0225957033', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (9, N'Darci Spieker', N'Quận 9', N'01238026730    ', N'0229211493', NULL)
INSERT [dbo].[NXB] ([masonxb], [ten], [diachi], [sodienthoai], [sotaikhoan], [trangthai]) VALUES (10, N'Bong Esteves', N'Quận 10', N'01225343766    ', N'0224349700', NULL)
SET IDENTITY_INSERT [dbo].[NXB] OFF
SET IDENTITY_INSERT [dbo].[PHIEUNHAP] ON 

INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (1, 9, N'Stapleford', CAST(N'2012-01-03' AS Date), CAST(1294000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (2, 10, N'Pittelkow', CAST(N'2012-02-23' AS Date), CAST(1849000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (3, 3, N'Hoffmeister', CAST(N'2012-03-06' AS Date), CAST(2236000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (4, 5, N'Manson', CAST(N'2012-04-13' AS Date), CAST(3110000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (5, 1, N'Portsche', CAST(N'2012-05-16' AS Date), CAST(629000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (6, 6, N'Pfalmer', CAST(N'2012-06-12' AS Date), CAST(2758000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (7, 6, N'Lenoue', CAST(N'2012-07-14' AS Date), CAST(3613000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (8, 3, N'Casley', CAST(N'2012-08-17' AS Date), CAST(3223000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (9, 9, N'Audirsch', CAST(N'2012-09-08' AS Date), CAST(2669000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUNHAP] ([masophieunhap], [masonxb], [nguoigiaosach], [ngaylap], [tongtien], [trangthai]) VALUES (10, 10, N'Ledet', CAST(N'2012-10-21' AS Date), CAST(2048000 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[PHIEUNHAP] OFF
SET IDENTITY_INSERT [dbo].[PHIEUXUAT] ON 

INSERT [dbo].[PHIEUXUAT] ([masophieuxuat], [masodaily], [nguoinhasach], [ngaylap], [tongtien], [trangthai]) VALUES (1, 3, N'Mullineaux', CAST(N'2012-07-03' AS Date), CAST(3173000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUXUAT] ([masophieuxuat], [masodaily], [nguoinhasach], [ngaylap], [tongtien], [trangthai]) VALUES (2, 3, N'Buzek', CAST(N'2012-04-03' AS Date), CAST(1594000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUXUAT] ([masophieuxuat], [masodaily], [nguoinhasach], [ngaylap], [tongtien], [trangthai]) VALUES (3, 7, N'Beckley', CAST(N'2012-07-03' AS Date), CAST(1240000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUXUAT] ([masophieuxuat], [masodaily], [nguoinhasach], [ngaylap], [tongtien], [trangthai]) VALUES (4, 4, N'Maglione', CAST(N'2012-09-03' AS Date), CAST(493000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[PHIEUXUAT] ([masophieuxuat], [masodaily], [nguoinhasach], [ngaylap], [tongtien], [trangthai]) VALUES (5, 6, N'Russum', CAST(N'2012-05-03' AS Date), CAST(1009000 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[PHIEUXUAT] OFF
SET IDENTITY_INSERT [dbo].[SACH] ON 

INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (1, 4, 2, N'Taylor Passantino', N'Randa Sofia', CAST(49000 AS Decimal(18, 0)), CAST(19000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'http://www.fromthemixedupfiles.com/wp-content/uploads/2015/10/book-clip-art-774.jpg', NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (2, 3, 5, N'Johanna', N'Jeffry Kinneman', CAST(32000 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)), CAST(99 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (3, 6, 2, N'Rogacion', N'Debora Lamme', CAST(40000 AS Decimal(18, 0)), CAST(22000 AS Decimal(18, 0)), CAST(71 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (4, 10, 5, N'Larry Prestley', N'Jacque Forro', CAST(40000 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (5, 4, 5, N'Alexa Laude', N'Hue Rogal', CAST(45000 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (6, 1, 4, N'Cyndy Dettloff', N'Cristie Lipovsky', CAST(31000 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)), CAST(37 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (7, 7, 4, N'Zora Creacy', N'Max Syzdek', CAST(37000 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (8, 5, 4, N'Talia Rasheed', N'Deidre Delaguila', CAST(42000 AS Decimal(18, 0)), CAST(26000 AS Decimal(18, 0)), CAST(18 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (9, 2, 4, N'Syreeta Burdge', N'Tisha Chajon', CAST(42000 AS Decimal(18, 0)), CAST(15000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (10, 6, 1, N'Hollie Barquera', N'Judie Mateus', CAST(39000 AS Decimal(18, 0)), CAST(28000 AS Decimal(18, 0)), CAST(75 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (11, 5, 1, N'Yasmin Truax', N'Albina Seilhymer', CAST(45000 AS Decimal(18, 0)), CAST(19000 AS Decimal(18, 0)), CAST(98 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (12, 10, 1, N'Florencio Pizano', N'Yoshie Spies', CAST(41000 AS Decimal(18, 0)), CAST(24000 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (13, 9, 3, N'Donella Schaunaman', N'Eleanor Paramore', CAST(31000 AS Decimal(18, 0)), CAST(14000 AS Decimal(18, 0)), CAST(27 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (14, 5, 1, N'Caron Benesh', N'Luisa Evanosky', CAST(38000 AS Decimal(18, 0)), CAST(19000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (15, 6, 5, N'Michal Succar', N'Morgan Zagacki', CAST(36000 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)), CAST(89 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (16, 4, 3, N'Inga Koffler', N'Mitch Dresner', CAST(31000 AS Decimal(18, 0)), CAST(12000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (17, 8, 5, N'Kimberlee Biss', N'Antonio Montefusco', CAST(44000 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (18, 9, 1, N'Daine Kurtz', N'Jeri Tarrenis', CAST(39000 AS Decimal(18, 0)), CAST(29000 AS Decimal(18, 0)), CAST(113 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (19, 8, 4, N'Rhoda Howell', N'Rachal Stinebaugh', CAST(30000 AS Decimal(18, 0)), CAST(11000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (20, 2, 4, N'Twanna Sirmon', N'Mitchell Zullinger', CAST(33000 AS Decimal(18, 0)), CAST(17000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (21, 4, 4, N'Blanca Haselhuhn', N'Magda Diaz', CAST(32000 AS Decimal(18, 0)), CAST(27000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (22, 7, 2, N'Dominic Tashjian', N'Fabiola Thwaites', CAST(45000 AS Decimal(18, 0)), CAST(10000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (23, 4, 3, N'Jackson Bowdler', N'Britney Caimi', CAST(35000 AS Decimal(18, 0)), CAST(24000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (24, 10, 3, N'Erwin Odebralski', N'Valrie Gaud', CAST(45000 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)), CAST(55 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (25, 3, 3, N'Magdalen Boshell', N'Klara Mallari', CAST(49000 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)), CAST(60 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (26, 10, 2, N'Amado Sugiki', N'Epifania Klimavicius', CAST(37000 AS Decimal(18, 0)), CAST(16000 AS Decimal(18, 0)), CAST(73 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (27, 3, 5, N'Guillermina Dungey', N'Winnifred Creary', CAST(35000 AS Decimal(18, 0)), CAST(11000 AS Decimal(18, 0)), CAST(41 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (28, 5, 3, N'test2', N'test', CAST(1000 AS Decimal(18, 0)), CAST(1000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (29, 1, 1, N'test 2', N'test', CAST(1000 AS Decimal(18, 0)), CAST(10000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'http://www.fromthemixedupfiles.com/wp-content/uploads/2015/10/book-clip-art-774.jpg', NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (30, 6, 5, N'Test 3', N'Test 3', CAST(20000 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[SACH] ([masosach], [masonxb], [masolinhvuc], [tensach], [tacgia], [giaban], [gianhap], [soluong], [hinhanh], [trangthai]) VALUES (31, 1, 1, N'test 2', N'test', CAST(2000 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL)
SET IDENTITY_INSERT [dbo].[SACH] OFF
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-01-03' AS Date), 13, CAST(22 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-01-03' AS Date), 18, CAST(34 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-02-23' AS Date), 4, CAST(53 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-02-23' AS Date), 12, CAST(13 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-03-06' AS Date), 2, CAST(37 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-03-06' AS Date), 25, CAST(60 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-04-03' AS Date), 2, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-04-03' AS Date), 12, CAST(3 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-04-13' AS Date), 8, CAST(48 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-04-13' AS Date), 11, CAST(98 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-05-03' AS Date), 8, CAST(38 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-05-03' AS Date), 13, CAST(3 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-05-16' AS Date), 6, CAST(37 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-06-12' AS Date), 3, CAST(91 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-06-12' AS Date), 10, CAST(27 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-07-03' AS Date), 3, CAST(81 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-07-03' AS Date), 4, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-07-03' AS Date), 8, CAST(18 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-07-03' AS Date), 10, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-07-14' AS Date), 10, CAST(75 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-07-14' AS Date), 15, CAST(89 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-08-17' AS Date), 2, CAST(99 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-08-17' AS Date), 27, CAST(41 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-09-03' AS Date), 3, CAST(71 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-09-03' AS Date), 13, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-09-08' AS Date), 13, CAST(27 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-09-08' AS Date), 18, CAST(113 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-10-21' AS Date), 24, CAST(55 AS Decimal(18, 0)))
INSERT [dbo].[THEKHO] ([ngayghi], [masosach], [soluong]) VALUES (CAST(N'2012-10-21' AS Date), 26, CAST(73 AS Decimal(18, 0)))
ALTER TABLE [dbo].[CHITIETHOADONDAILY]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETHOADONDAILY_HOADONDAILY] FOREIGN KEY([masohoadon])
REFERENCES [dbo].[HOADONDAILY] ([masohoadon])
GO
ALTER TABLE [dbo].[CHITIETHOADONDAILY] CHECK CONSTRAINT [FK_CHITIETHOADONDAILY_HOADONDAILY]
GO
ALTER TABLE [dbo].[CHITIETHOADONDAILY]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETHOADONDAILY_SACH] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[CHITIETHOADONDAILY] CHECK CONSTRAINT [FK_CHITIETHOADONDAILY_SACH]
GO
ALTER TABLE [dbo].[CHITIETHOADONNXB]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETHOADONNXB_HOADONNXB] FOREIGN KEY([masohoadon])
REFERENCES [dbo].[HOADONNXB] ([masohoadon])
GO
ALTER TABLE [dbo].[CHITIETHOADONNXB] CHECK CONSTRAINT [FK_CHITIETHOADONNXB_HOADONNXB]
GO
ALTER TABLE [dbo].[CHITIETHOADONNXB]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETHOADONNXB_SACH1] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[CHITIETHOADONNXB] CHECK CONSTRAINT [FK_CHITIETHOADONNXB_SACH1]
GO
ALTER TABLE [dbo].[CHITIETPHIEUNHAP]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETPHIEUNHAP_PHIEUNHAP] FOREIGN KEY([masophieunhap])
REFERENCES [dbo].[PHIEUNHAP] ([masophieunhap])
GO
ALTER TABLE [dbo].[CHITIETPHIEUNHAP] CHECK CONSTRAINT [FK_CHITIETPHIEUNHAP_PHIEUNHAP]
GO
ALTER TABLE [dbo].[CHITIETPHIEUNHAP]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETPHIEUNHAP_SACH] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[CHITIETPHIEUNHAP] CHECK CONSTRAINT [FK_CHITIETPHIEUNHAP_SACH]
GO
ALTER TABLE [dbo].[CHITIETPHIEUXUAT]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETPHIEUXUAT_PHIEUXUAT] FOREIGN KEY([masophieuxuat])
REFERENCES [dbo].[PHIEUXUAT] ([masophieuxuat])
GO
ALTER TABLE [dbo].[CHITIETPHIEUXUAT] CHECK CONSTRAINT [FK_CHITIETPHIEUXUAT_PHIEUXUAT]
GO
ALTER TABLE [dbo].[CHITIETPHIEUXUAT]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETPHIEUXUAT_SACH] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[CHITIETPHIEUXUAT] CHECK CONSTRAINT [FK_CHITIETPHIEUXUAT_SACH]
GO
ALTER TABLE [dbo].[CONGNODAILY]  WITH CHECK ADD  CONSTRAINT [FK_CONGNODAILY_DAILY] FOREIGN KEY([masodaily])
REFERENCES [dbo].[DAILY] ([masodaily])
GO
ALTER TABLE [dbo].[CONGNODAILY] CHECK CONSTRAINT [FK_CONGNODAILY_DAILY]
GO
ALTER TABLE [dbo].[CONGNODAILY]  WITH CHECK ADD  CONSTRAINT [FK_CONGNODAILY_SACH] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[CONGNODAILY] CHECK CONSTRAINT [FK_CONGNODAILY_SACH]
GO
ALTER TABLE [dbo].[CONGNONXB]  WITH CHECK ADD  CONSTRAINT [FK_CONGNONXB_NXB] FOREIGN KEY([masonxb])
REFERENCES [dbo].[NXB] ([masonxb])
GO
ALTER TABLE [dbo].[CONGNONXB] CHECK CONSTRAINT [FK_CONGNONXB_NXB]
GO
ALTER TABLE [dbo].[CONGNONXB]  WITH CHECK ADD  CONSTRAINT [FK_CONGNONXB_SACH] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[CONGNONXB] CHECK CONSTRAINT [FK_CONGNONXB_SACH]
GO
ALTER TABLE [dbo].[HOADONDAILY]  WITH CHECK ADD  CONSTRAINT [FK_HOADONDAILY_DAILY] FOREIGN KEY([masodaily])
REFERENCES [dbo].[DAILY] ([masodaily])
GO
ALTER TABLE [dbo].[HOADONDAILY] CHECK CONSTRAINT [FK_HOADONDAILY_DAILY]
GO
ALTER TABLE [dbo].[HOADONNXB]  WITH CHECK ADD  CONSTRAINT [FK_HOADONNXB_NXB] FOREIGN KEY([masonxb])
REFERENCES [dbo].[NXB] ([masonxb])
GO
ALTER TABLE [dbo].[HOADONNXB] CHECK CONSTRAINT [FK_HOADONNXB_NXB]
GO
ALTER TABLE [dbo].[PHIEUNHAP]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUNHAP_NXB] FOREIGN KEY([masonxb])
REFERENCES [dbo].[NXB] ([masonxb])
GO
ALTER TABLE [dbo].[PHIEUNHAP] CHECK CONSTRAINT [FK_PHIEUNHAP_NXB]
GO
ALTER TABLE [dbo].[PHIEUXUAT]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUXUAT_DAILY] FOREIGN KEY([masodaily])
REFERENCES [dbo].[DAILY] ([masodaily])
GO
ALTER TABLE [dbo].[PHIEUXUAT] CHECK CONSTRAINT [FK_PHIEUXUAT_DAILY]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK_SACH_LINHVUC] FOREIGN KEY([masolinhvuc])
REFERENCES [dbo].[LINHVUC] ([masolinhvuc])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK_SACH_LINHVUC]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK_SACH_NXB] FOREIGN KEY([masonxb])
REFERENCES [dbo].[NXB] ([masonxb])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK_SACH_NXB]
GO
ALTER TABLE [dbo].[THEKHO]  WITH CHECK ADD  CONSTRAINT [FK_THEKHO_SACH] FOREIGN KEY([masosach])
REFERENCES [dbo].[SACH] ([masosach])
GO
ALTER TABLE [dbo].[THEKHO] CHECK CONSTRAINT [FK_THEKHO_SACH]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LINHVUC"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NXB"
            Begin Extent = 
               Top = 7
               Left = 751
               Bottom = 137
               Right = 921
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SACH"
            Begin Extent = 
               Top = 0
               Left = 398
               Bottom = 130
               Right = 568
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 1410
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DANHMUCSACH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DANHMUCSACH'
GO
USE [master]
GO
ALTER DATABASE [QLPHS] SET  READ_WRITE 
GO
