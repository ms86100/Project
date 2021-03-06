USE [master]
GO
/****** Object:  Database [FileUploadEngine]    Script Date: 5/17/2021 9:15:14 PM ******/
CREATE DATABASE [FileUploadEngine]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FileUploadEngine', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FileUploadEngine.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FileUploadEngine_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FileUploadEngine_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FileUploadEngine] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FileUploadEngine].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FileUploadEngine] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FileUploadEngine] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FileUploadEngine] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FileUploadEngine] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FileUploadEngine] SET ARITHABORT OFF 
GO
ALTER DATABASE [FileUploadEngine] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FileUploadEngine] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FileUploadEngine] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FileUploadEngine] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FileUploadEngine] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FileUploadEngine] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FileUploadEngine] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FileUploadEngine] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FileUploadEngine] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FileUploadEngine] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FileUploadEngine] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FileUploadEngine] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FileUploadEngine] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FileUploadEngine] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FileUploadEngine] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FileUploadEngine] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FileUploadEngine] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FileUploadEngine] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FileUploadEngine] SET  MULTI_USER 
GO
ALTER DATABASE [FileUploadEngine] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FileUploadEngine] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FileUploadEngine] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FileUploadEngine] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FileUploadEngine] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FileUploadEngine] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FileUploadEngine] SET QUERY_STORE = OFF
GO
USE [FileUploadEngine]
GO
/****** Object:  Table [dbo].[utblMstConnections]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utblMstConnections](
	[ConnectionID] [int] IDENTITY(1,1) NOT NULL,
	[ConnectionName] [varchar](50) NOT NULL,
	[URL] [varchar](max) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[DownloadURL] [varchar](max) NULL,
 CONSTRAINT [PK_utblMstConnections] PRIMARY KEY CLUSTERED 
(
	[ConnectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utblMstImportMapHeaders]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utblMstImportMapHeaders](
	[ImportMapHeaderID] [int] IDENTITY(1,1) NOT NULL,
	[ImportMapName] [varchar](max) NOT NULL,
 CONSTRAINT [PK_utblMstImportMapHeaders] PRIMARY KEY CLUSTERED 
(
	[ImportMapHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utblMstImports]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utblMstImports](
	[ImportID] [int] IDENTITY(1,1) NOT NULL,
	[ImportName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_utblMstImports] PRIMARY KEY CLUSTERED 
(
	[ImportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utblMstSampleImports]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utblMstSampleImports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](max) NULL,
	[Parameter] [nvarchar](max) NULL,
	[SourceTable] [nvarchar](max) NULL,
	[APIDataSource] [nvarchar](max) NULL,
	[LastExecution] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.utblMstSampleImports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utblMstSteps]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utblMstSteps](
	[StepsID] [int] IDENTITY(1,1) NOT NULL,
	[ImportID] [int] NOT NULL,
	[StepOrder] [int] NOT NULL,
	[Action] [varchar](50) NOT NULL,
	[Parameter1] [varchar](max) NULL,
	[Parameter2] [varchar](max) NULL,
	[Parameter3] [varchar](max) NULL,
 CONSTRAINT [PK_utblMstSteps] PRIMARY KEY CLUSTERED 
(
	[StepsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utblMstTableMappings]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utblMstTableMappings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ImportMapHeaderID] [int] NOT NULL,
	[FromField] [varchar](50) NOT NULL,
	[ToField] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbl101] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[utblMstConnections] ON 

INSERT [dbo].[utblMstConnections] ([ConnectionID], [ConnectionName], [URL], [UserName], [Password], [DownloadURL]) VALUES (1, N'FTP1', N'test.rebex.net', N'demo', N'password', N'C:\Test\')
SET IDENTITY_INSERT [dbo].[utblMstConnections] OFF
GO
SET IDENTITY_INSERT [dbo].[utblMstImportMapHeaders] ON 

INSERT [dbo].[utblMstImportMapHeaders] ([ImportMapHeaderID], [ImportMapName]) VALUES (1, N'tbl_NH_Ownership')
SET IDENTITY_INSERT [dbo].[utblMstImportMapHeaders] OFF
GO
SET IDENTITY_INSERT [dbo].[utblMstImports] ON 

INSERT [dbo].[utblMstImports] ([ImportID], [ImportName]) VALUES (1, N'Provider')
INSERT [dbo].[utblMstImports] ([ImportID], [ImportName]) VALUES (2, N'Owner')
INSERT [dbo].[utblMstImports] ([ImportID], [ImportName]) VALUES (3, N'Covid')
INSERT [dbo].[utblMstImports] ([ImportID], [ImportName]) VALUES (4, N'RCA HRPR')
SET IDENTITY_INSERT [dbo].[utblMstImports] OFF
GO
SET IDENTITY_INSERT [dbo].[utblMstSteps] ON 

INSERT [dbo].[utblMstSteps] ([StepsID], [ImportID], [StepOrder], [Action], [Parameter1], [Parameter2], [Parameter3]) VALUES (1, 1, 1, N'Import API', N'https://data.cms.gov/provider-data/api/1/datastore/sql/?query=[SELECT * FROM 38edd288-fecb-5f4f-a994-f5d2cbc9df81][LIMIT 20 OFFSET 0]
', N'tbl_NH_Ownership', N'MAPTable:1')
INSERT [dbo].[utblMstSteps] ([StepsID], [ImportID], [StepOrder], [Action], [Parameter1], [Parameter2], [Parameter3]) VALUES (2, 1, 2, N'MappingMove', N'From:tbl_NH_Ownership', N'Destination:tbl_NH_Ownership', N'MAPTable:1')
INSERT [dbo].[utblMstSteps] ([StepsID], [ImportID], [StepOrder], [Action], [Parameter1], [Parameter2], [Parameter3]) VALUES (3, 4, 1, N'FTP', N'Connection:1', N'FilesToGet*', N'Destination:C:\Test\')
SET IDENTITY_INSERT [dbo].[utblMstSteps] OFF
GO
SET IDENTITY_INSERT [dbo].[utblMstTableMappings] ON 

INSERT [dbo].[utblMstTableMappings] ([ID], [ImportMapHeaderID], [FromField], [ToField]) VALUES (6, 1, N'Provider Zip Code', N'ZIP')
SET IDENTITY_INSERT [dbo].[utblMstTableMappings] OFF
GO
/****** Object:  StoredProcedure [dbo].[udspGetDataFromStagingToProduction]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[udspGetDataFromStagingToProduction]
	
@FromTable varchar(Max)=null,
@ToTable varchar(Max)=null
AS
BEGIN

declare @SQL varchar(MAX)
	
set @SQL= 'INSERT  INTO '+@ToTable+'
SELECT DISTINCT * FROM '+@FromTable+''

	
--set @SQL= 'SELECT DISTINCT * INTO '+@ToTable+'
--FROM '+@FromTable+''



 EXEC(@SQL)
 END
GO
/****** Object:  StoredProcedure [dbo].[udspMstColumnRename]    Script Date: 5/17/2021 9:15:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udspMstColumnRename]
@TableAndColumnName varchar(max),
@NewColumnName varchar(Max)
AS

BEGIN

--EXEC sp_RENAME 'tbl_NH_Ownership.OwnerFullName' , '@NewColumnName', 'COLUMN'


EXEC sp_RENAME @TableAndColumnName , @NewColumnName, 'COLUMN'

END
GO
USE [master]
GO
ALTER DATABASE [FileUploadEngine] SET  READ_WRITE 
GO
