USE [master]
GO
/****** Object:  Database [ProgPOEDb]    Script Date: 5/14/2025 10:31:02 AM ******/
CREATE DATABASE [ProgPOEDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProgPOEDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProgPOEDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProgPOEDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProgPOEDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProgPOEDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProgPOEDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProgPOEDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProgPOEDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProgPOEDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProgPOEDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProgPOEDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProgPOEDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProgPOEDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProgPOEDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProgPOEDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProgPOEDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProgPOEDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProgPOEDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProgPOEDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProgPOEDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProgPOEDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProgPOEDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProgPOEDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProgPOEDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProgPOEDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProgPOEDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProgPOEDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ProgPOEDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProgPOEDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProgPOEDb] SET  MULTI_USER 
GO
ALTER DATABASE [ProgPOEDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProgPOEDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProgPOEDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProgPOEDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProgPOEDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProgPOEDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProgPOEDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProgPOEDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProgPOEDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/14/2025 10:31:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Farmers]    Script Date: 5/14/2025 10:31:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Farmers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[FarmName] [nvarchar](max) NOT NULL,
	[FarmLocation] [nvarchar](max) NOT NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Farmers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/14/2025 10:31:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[ProductionDate] [datetime2](7) NOT NULL,
	[Quantity] [float] NOT NULL,
	[Unit] [nvarchar](max) NOT NULL,
	[PricePerUnit] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[FarmerId] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/14/2025 10:31:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[FarmerId] [int] NULL,
	[LastLogin] [datetime2](7) NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513213235_InitialCreate', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513221359_InitialMigration', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513231257_AddProductImageUrl', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514081055_AddPasswordHashing', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514081249_FixSeedDataHashes', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514082002_RevertToPlainTextForSeeds', N'9.0.5')
GO
SET IDENTITY_INSERT [dbo].[Farmers] ON 

INSERT [dbo].[Farmers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [FarmName], [FarmLocation], [RegistrationDate]) VALUES (1, N'John', N'Smith', N'john.smith@example.com', N'0123456789', N'Green Acres Farm', N'Eastern Cape', CAST(N'2025-04-13T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Farmers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [FarmName], [FarmLocation], [RegistrationDate]) VALUES (2, N'arshad', N'bhula', N'arshad@gmail.com', N'0987654321', N'Sunrise Valley Farm', N'Western Cape', CAST(N'2025-04-28T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Farmers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [FarmName], [FarmLocation], [RegistrationDate]) VALUES (3, N'Jordan', N'Gardiner', N'jordan@gmail.com', N'0658829242', N'Jordan''s Farm', N'KwaZulu-Natal', CAST(N'2025-05-14T00:20:04.6678337' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Farmers] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Category], [ProductionDate], [Quantity], [Unit], [PricePerUnit], [Description], [FarmerId], [ImageUrl]) VALUES (2, N'Free-range Eggs', N'Dairy & Eggs', CAST(N'2025-05-11T00:00:00.0000000' AS DateTime2), 500, N'dozen', CAST(45.00 AS Decimal(18, 2)), N'Farm-fresh free-range eggs', 1, N'https://images.pexels.com/photos/162712/egg-white-food-protein-162712.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1')
INSERT [dbo].[Products] ([Id], [Name], [Category], [ProductionDate], [Quantity], [Unit], [PricePerUnit], [Description], [FarmerId], [ImageUrl]) VALUES (3, N'Sweet Corn', N'Vegetables', CAST(N'2025-05-10T00:00:00.0000000' AS DateTime2), 200, N'kg', CAST(15.75 AS Decimal(18, 2)), N'Fresh sweet corn', 2, N'https://images.pexels.com/photos/603030/pexels-photo-603030.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1')
INSERT [dbo].[Products] ([Id], [Name], [Category], [ProductionDate], [Quantity], [Unit], [PricePerUnit], [Description], [FarmerId], [ImageUrl]) VALUES (4, N'Organic Apples', N'Fruits', CAST(N'2025-05-06T00:00:00.0000000' AS DateTime2), 300, N'kg', CAST(18.50 AS Decimal(18, 2)), N'Freshly picked organic apples', 2, N'https://images.pexels.com/photos/672101/pexels-photo-672101.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1')
INSERT [dbo].[Products] ([Id], [Name], [Category], [ProductionDate], [Quantity], [Unit], [PricePerUnit], [Description], [FarmerId], [ImageUrl]) VALUES (5, N'Eggs', N'Dairy & Eggs', CAST(N'2025-05-14T00:00:00.0000000' AS DateTime2), 30, N'units', CAST(30.00 AS Decimal(18, 2)), N'Fresh eggs', 3, N'https://afarmgirlinthemaking.com/wp-content/uploads/2018/11/egg-basket.jpg')
INSERT [dbo].[Products] ([Id], [Name], [Category], [ProductionDate], [Quantity], [Unit], [PricePerUnit], [Description], [FarmerId], [ImageUrl]) VALUES (6, N'Fresh Tomotoes', N'Vegetables', CAST(N'2025-05-14T00:00:00.0000000' AS DateTime2), 1, N'units', CAST(8.00 AS Decimal(18, 2)), N'Fresh Tomatoes', 1, N'https://tagawagardens.com/wp-content/uploads/2023/08/TOMATOES-RED-RIPE-VINE-SS-1-1024x681.jpg')
INSERT [dbo].[Products] ([Id], [Name], [Category], [ProductionDate], [Quantity], [Unit], [PricePerUnit], [Description], [FarmerId], [ImageUrl]) VALUES (8, N'Beef', N'Meats', CAST(N'2025-05-14T00:00:00.0000000' AS DateTime2), 1, N'kg', CAST(80.00 AS Decimal(18, 2)), N'Fresh Cut Meat', 3, N'https://www.allrecipes.com/thmb/IE4VHILw_pC6s5gBxUTpziuRuaw=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/42176-all-american-roast-beef-DDMFS-hero-2x1-0350-284cbd9cf03b4ff5916c144414d6b353.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Role], [FarmerId], [LastLogin], [RegistrationDate]) VALUES (1, N'employee', N'$2a$12$g3MD1dbBkiiRqHDqRnA2u.NoSVcKRZ7P30vdxNSppzaAMMz126wqK', N'Employee', NULL, CAST(N'2025-05-14T10:21:18.1693119' AS DateTime2), CAST(N'2025-05-13T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Username], [Password], [Role], [FarmerId], [LastLogin], [RegistrationDate]) VALUES (2, N'farmer', N'$2a$12$ww5dYHM3EX5Wrbo6aAITuuw5JDtRdAK39I4MoCyqgEq128fQTsknO', N'Farmer', 1, CAST(N'2025-05-14T10:23:01.5100259' AS DateTime2), CAST(N'2025-05-13T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Username], [Password], [Role], [FarmerId], [LastLogin], [RegistrationDate]) VALUES (3, N'jordan', N'$2a$12$OoLZpaOLHLDTESkj/7uSVOeLJ6imshgCyakcum.kMRNG6WnNkVxcm', N'Farmer', 3, CAST(N'2025-05-14T10:20:58.7395260' AS DateTime2), CAST(N'2025-05-14T00:20:04.7942480' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Products_FarmerId]    Script Date: 5/14/2025 10:31:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_FarmerId] ON [dbo].[Products]
(
	[FarmerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_FarmerId]    Script Date: 5/14/2025 10:31:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Users_FarmerId] ON [dbo].[Users]
(
	[FarmerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Farmers_FarmerId] FOREIGN KEY([FarmerId])
REFERENCES [dbo].[Farmers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Farmers_FarmerId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Farmers_FarmerId] FOREIGN KEY([FarmerId])
REFERENCES [dbo].[Farmers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Farmers_FarmerId]
GO
USE [master]
GO
ALTER DATABASE [ProgPOEDb] SET  READ_WRITE 
GO
