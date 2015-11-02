USE [master]
GO
/****** Object:  Database [QLPHS]    Script Date: 31/10/2015 21:41:35 PM ******/
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
/****** Object:  User [jerry]    Script Date: 31/10/2015 21:41:35 PM ******/
CREATE USER [jerry] FOR LOGIN [jerry] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [jerry]
GO
/****** Object:  Table [dbo].[CHITIETHOADONDAILY]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETHOADONDAILY](
	[masohoadon] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
 CONSTRAINT [PK_CHITIETHOADONDAILY] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHITIETHOADONNXB]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETHOADONNXB](
	[masohoadon] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
 CONSTRAINT [PK_CHITIETHOADONNXB] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHITIETPHIEUNHAP]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETPHIEUNHAP](
	[masophieunhap] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
 CONSTRAINT [PK_CHITIETPHIEUNHAP] PRIMARY KEY CLUSTERED 
(
	[masophieunhap] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHITIETPHIEUXUAT]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETPHIEUXUAT](
	[masophieuxuat] [int] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
 CONSTRAINT [PK_CHITIETPHIEUXUAT] PRIMARY KEY CLUSTERED 
(
	[masophieuxuat] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONGNODAILY]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONGNODAILY](
	[masosach] [int] NOT NULL,
	[masodaily] [int] NOT NULL,
	[thang] [date] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
 CONSTRAINT [PK_CONGNODAILY] PRIMARY KEY CLUSTERED 
(
	[masosach] ASC,
	[masodaily] ASC,
	[thang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONGNONXB]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONGNONXB](
	[masosach] [int] NOT NULL,
	[masonxb] [int] NOT NULL,
	[thang] [date] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
 CONSTRAINT [PK_CONGNONXB] PRIMARY KEY CLUSTERED 
(
	[masosach] ASC,
	[masonxb] ASC,
	[thang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DAILY]    Script Date: 31/10/2015 21:41:36 PM ******/
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
 CONSTRAINT [PK_DAILY] PRIMARY KEY CLUSTERED 
(
	[masodaily] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HOADONDAILY]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADONDAILY](
	[masohoadon] [int] IDENTITY(1,1) NOT NULL,
	[masodaily] [int] NOT NULL,
	[ngaylap] [datetime] NOT NULL,
	[tongtien] [int] NOT NULL,
 CONSTRAINT [PK_HOADONDAILY] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HOADONNXB]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADONNXB](
	[masohoadon] [int] IDENTITY(1,1) NOT NULL,
	[masonxb] [int] NOT NULL,
	[ngaylap] [datetime] NOT NULL,
	[tongtien] [int] NOT NULL,
 CONSTRAINT [PK_HOADONNXB] PRIMARY KEY CLUSTERED 
(
	[masohoadon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LINHVUC]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LINHVUC](
	[masolinhvuc] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LINHVUC] PRIMARY KEY CLUSTERED 
(
	[masolinhvuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NXB]    Script Date: 31/10/2015 21:41:36 PM ******/
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
 CONSTRAINT [PK_NXB] PRIMARY KEY CLUSTERED 
(
	[masonxb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHIEUNHAP]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUNHAP](
	[masophieunhap] [int] IDENTITY(1,1) NOT NULL,
	[masonxb] [int] NOT NULL,
	[nguoigiaosach] [nvarchar](50) NOT NULL,
	[ngaylap] [datetime] NOT NULL,
	[tongtien] [int] NOT NULL,
 CONSTRAINT [PK_PHIEUNHAP] PRIMARY KEY CLUSTERED 
(
	[masophieunhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHIEUXUAT]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUXUAT](
	[masophieuxuat] [int] IDENTITY(1,1) NOT NULL,
	[masodaily] [int] NOT NULL,
	[nguoinhasach] [nvarchar](50) NOT NULL,
	[ngaylap] [datetime] NOT NULL,
	[tongtien] [int] NOT NULL,
 CONSTRAINT [PK_PHIEUXUAT] PRIMARY KEY CLUSTERED 
(
	[masophieuxuat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SACH]    Script Date: 31/10/2015 21:41:36 PM ******/
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
	[giaban] [int] NOT NULL,
	[gianhap] [int] NOT NULL,
	[soluong] [int] NOT NULL,
	[hinhanh] [text] NULL,
 CONSTRAINT [PK_SACH] PRIMARY KEY CLUSTERED 
(
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[THEKHO]    Script Date: 31/10/2015 21:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THEKHO](
	[ngayghi] [datetime] NOT NULL,
	[masosach] [int] NOT NULL,
	[soluong] [int] NOT NULL,
 CONSTRAINT [PK_THEKHO] PRIMARY KEY CLUSTERED 
(
	[ngayghi] ASC,
	[masosach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[DANHMUCSACH]    Script Date: 31/10/2015 21:41:36 PM ******/
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
