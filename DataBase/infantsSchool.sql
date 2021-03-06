USE [master]
GO
/****** Object:  Database [InfantsSchoolSystem]    Script Date: 2020/7/10 13:42:55 ******/
CREATE DATABASE [InfantsSchoolSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'infantsSchoolSystem', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\infantsSchoolSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'infantsSchoolSystem_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\infantsSchoolSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [InfantsSchoolSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InfantsSchoolSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InfantsSchoolSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InfantsSchoolSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InfantsSchoolSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [InfantsSchoolSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InfantsSchoolSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [InfantsSchoolSystem] SET  MULTI_USER 
GO
ALTER DATABASE [InfantsSchoolSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InfantsSchoolSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InfantsSchoolSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InfantsSchoolSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InfantsSchoolSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'InfantsSchoolSystem', N'ON'
GO
ALTER DATABASE [InfantsSchoolSystem] SET QUERY_STORE = OFF
GO
USE [InfantsSchoolSystem]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 2020/7/10 13:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Path] [nvarchar](50) NULL,
	[Method] [nvarchar](20) NULL,
	[Icon] [nvarchar](50) NULL,
	[PId] [int] NULL,
	[Remark] [nvarchar](50) NULL,
	[ActionTypeId] [int] NULL,
	[OrderBy] [int] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK__Action__3214EC07BAF5615E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActionType]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](20) NULL,
 CONSTRAINT [PK_ActionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[StartTime] [date] NULL,
	[Remark] [nvarchar](max) NULL,
	[GradeId] [int] NULL,
 CONSTRAINT [PK__Activity__3214EC07431FDB3A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityPicture]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityPicture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](50) NULL,
	[ActivityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostType]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](30) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
	[CreateTime] [date] NULL,
	[UserId] [int] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GradeCost]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GradeCost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsEpense] [bit] NULL,
	[CostTypeId] [int] NULL,
	[Remark] [nvarchar](50) NULL,
	[GradeId] [int] NULL,
	[CreateTime] [date] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Remark] [nvarchar](20) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleAction]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[ActionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Gender] [bit] NULL,
	[Birthday] [date] NULL,
	[GradeId] [int] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK__Student__3214EC07FBF5CA6C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account] [nvarchar](20) NULL,
	[Pwd] [nvarchar](20) NULL,
	[Name] [nvarchar](20) NULL,
	[Gender] [bit] NULL,
	[Birthday] [date] NULL,
	[Phone] [nvarchar](20) NULL,
	[Photo] [nvarchar](100) NULL,
	[Address] [nvarchar](20) NULL,
	[AddressDetail] [nvarchar](50) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK__Account__3214EC072CA325CD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 2020/7/10 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[RoleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Action] ON 

INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (2, N'学生管理', N'student', NULL, N'el-icon-s-custom', 0, N'一级权限', 1, 4, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (3, N'学生列表', N'/student', NULL, N'el-icon-user', 2, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (4, N'权限管理', N'action', NULL, N'el-icon-setting', 0, N'一级权限', 1, 2, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (5, N'角色列表', N'/role', N'GET', N'el-icon-user', 4, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (6, N'权限列表', N'/action', N'GET', N'el-icon-menu', 4, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (7, N'班级管理', N'grade', NULL, N'el-icon-s-flag', 0, N'一级权限', 1, 3, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (8, N'班级列表', N'/grade', N'GET', N'el-icon-share', 7, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (21, N'用户管理', N'user', NULL, N'el-icon-user', 0, N'一级权限', 1, 1, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (22, N'用户列表', N'/user', N'GET', N'el-icon-user', 21, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (23, N'个人中心', N'cente', NULL, N'el-icon-user', 0, N'一级权限', 1, 6, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (24, N'个人详情', N'/center', N'GET', N'el-icon-user', 23, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (25, N'添加用户', N'/user', N'POST', N'', 22, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (26, N'编辑用户', N'/user', N'PUT', NULL, 22, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (27, N'添加角色', N'/role', N'POST', NULL, 5, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (28, N'编辑角色', N'/role', N'PUT', NULL, 5, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (29, N'添加班级', N'/grade', N'POST', NULL, 8, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (30, N'编辑班级', N'/grade', NULL, NULL, 8, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (31, N'添加权限', N'/action/addAction', N'POST', NULL, 6, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (47, N'删除角色', N'/role/', N'DELETE', NULL, 5, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (50, N'编辑权限', N'/action/EditAction', N'PUT', NULL, 6, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (51, N'删除权限', N'/Action/DeleteAction/', N'DELETE', NULL, 6, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (55, N'分配角色权限', N'/action/setACtionByRoleId', N'POST', NULL, 5, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (56, N'删除用户', N'/user/', N'DELETE', NULL, 22, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (59, N'修改密码', N'/editpass', NULL, N'el-icon-user', 23, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (60, N'删除学生', N'/Student/DeleteStudent/', N'DELETE', NULL, 3, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (61, N'查找学生', N'/student/GetStudents', N'GET', NULL, 3, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (62, N'查询某学生', N'/Student/GetStudentById/', N'GET', NULL, 3, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (63, N'添加学生', N'/student/addStudent', N'POST', NULL, 3, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (64, N'编辑学生', N'/student/editStudent', N'PUT', NULL, 3, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (65, N'活动管理', N'activity', NULL, N'el-icon-camera-solid
', 0, N'一级权限', 1, 5, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (66, N'活动列表', N'/activity', N'GET', N'el-icon-camera', 65, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (67, N'添加活动', N'/addActivity', N'GET', N'el-icon-camera', 65, N'二级权限', 2, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (68, N'添加活动', N'/activity/addActivity', N'POST', NULL, 67, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (69, N'删除角色权限', N'/Action/DeleteActionByRoleId/', N'DELETE', NULL, 5, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (74, N'班级信息', N'/Student/GetGradesInfo', N'GET', NULL, 3, N'三级权限', 3, NULL, 0)
INSERT [dbo].[Action] ([Id], [Name], [Path], [Method], [Icon], [PId], [Remark], [ActionTypeId], [OrderBy], [IsDelete]) VALUES (77, N'删除班级', N'/grade/', N'DELETE', NULL, 8, N'三级权限', 3, NULL, 0)
SET IDENTITY_INSERT [dbo].[Action] OFF
GO
SET IDENTITY_INSERT [dbo].[ActionType] ON 

INSERT [dbo].[ActionType] ([Id], [TypeName]) VALUES (1, N'一级权限')
INSERT [dbo].[ActionType] ([Id], [TypeName]) VALUES (2, N'二级权限')
INSERT [dbo].[ActionType] ([Id], [TypeName]) VALUES (3, N'三级权限')
SET IDENTITY_INSERT [dbo].[ActionType] OFF
GO
SET IDENTITY_INSERT [dbo].[Activity] ON 

INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (1, N'幸福大家庭', CAST(N'2020-06-10' AS Date), N'全班组织一起吃零食看电影', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (2, N'快乐的端午节', CAST(N'2020-06-06' AS Date), N'小朋友们一起做粽子吃粽子', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (3, N'春游大作战', CAST(N'2020-05-05' AS Date), N'一起去春游拉！！！', 3)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (4, N'大扫除的一天', CAST(N'2019-03-02' AS Date), N'大家都在搞卫生，好辛苦', 2)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (5, N'防溺水主题班会', CAST(N'2020-01-11' AS Date), N'开展了一次防溺水的主题班会', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (6, N'你的小天使', CAST(N'2020-03-23' AS Date), N'每个人的心中都有一个小天使', 2)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (7, N'组团动物园', CAST(N'2020-02-20' AS Date), N'一起去动物园，看了许多的小动物', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (8, N'asdasd', CAST(N'2020-06-14' AS Date), N'<p>asdsad</p>', 4)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (9, N'asdasd', CAST(N'2020-06-14' AS Date), N'<p>asdsad</p>', 4)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (10, N'小宝哈哈哈哈', CAST(N'2020-06-14' AS Date), N'<p>今天又是<strong>一个好天气</strong>，看到小<em>宝在哈哈哈哈asdas<span class="ql-cursor">﻿</span></em></p>', 9)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (11, N'班级主题会议', CAST(N'2020-06-15' AS Date), N'<p>好开心的一天哇！！！</p>', 5)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (12, N'lllll', CAST(N'2020-06-16' AS Date), N'<p><strong>jkhjkhhjkkjh<em>，fjgjkgjhkjhj</em></strong></p>', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (13, N'今天端午', CAST(N'2020-06-25' AS Date), N'<p>今天好开心啊啊啊啊</p>', 3)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (14, N'哈哈哈', CAST(N'2020-06-29' AS Date), N'<p>哈哈<strong>哈哈a</strong><strong style="color: rgb(255, 153, 0);">sdasdsadas撒</strong><strong style="color: rgb(230, 0, 0);">打算<span class="ql-cursor">﻿</span></strong></p>', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (15, N'小宝哈哈哈', CAST(N'2020-06-30' AS Date), N'<p>爱睡觉的拉克斯基的</p>', 4)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (1015, N'adsad', CAST(N'2020-07-06' AS Date), N'<p>阿萨的贺卡收<strong>到adsad</strong></p>', 1)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (1016, N'jj4', CAST(N'2020-07-07' AS Date), N'<p>发范<strong>德萨发达</strong></p>', 10)
INSERT [dbo].[Activity] ([Id], [Name], [StartTime], [Remark], [GradeId]) VALUES (1017, N'今天答辩', CAST(N'2020-07-09' AS Date), N'<p>哈哈<strong>哈哈</strong></p>', 1)
SET IDENTITY_INSERT [dbo].[Activity] OFF
GO
SET IDENTITY_INSERT [dbo].[ActivityPicture] ON 

INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1, N'http://localhost:5000/images\060815565110.png', 1)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (2, N'http://localhost:5000/images\060920122415.png', 2)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (3, N'http://localhost:5000/images\06092030587.png', 3)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (4, N'http://localhost:5000/images\060709290811.png', 1)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (5, N'http://localhost:5000/images\06070940145.png', 1)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (6, N'http://localhost:5000/images\060709290811.png', 2)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (7, N'http://localhost:5000/images\060815565110.png', 2)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (8, N'http://localhost:5000/images\061409455923.png', 8)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (9, N'http://localhost:5000/images\061409460232.png', 8)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (10, N'http://localhost:5000/images\06140946056.png', 8)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (11, N'http://localhost:5000/images\061409455923.png', 9)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (12, N'http://localhost:5000/images\061409460232.png', 9)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (13, N'http://localhost:5000/images\06140946056.png', 9)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (14, N'http://localhost:5000/images\061410161618.png', 10)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (15, N'http://localhost:5000/images\061410162120.png', 10)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (16, N'http://localhost:5000/images\061519292727.png', 11)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (17, N'http://localhost:5000/images\061519293129.png', 11)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (18, N'http://localhost:5000/images\061617491517.png', 12)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (19, N'http://localhost:5000/images\061617491818.png', 12)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (20, N'http://localhost:5000/images\062511100829.png', 13)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (21, N'http://localhost:5000/images\062511101226.png', 13)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (22, N'http://localhost:5000/images\06290944262.png', 14)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (23, N'http://localhost:5000/images\062909443225.png', 14)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (24, N'http://localhost:5000/images\06301003108.png', 15)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (25, N'http://localhost:5000/images\06301003235.png', 15)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (26, N'http://localhost:5000/images\06301003306.png', 15)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1024, N'http://localhost:5000/images\070610330023.png', 1015)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1025, N'http://localhost:5000/images\07061033024.png', 1015)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1026, N'http://localhost:5000/images\070717420114..png', 1016)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1027, N'http://localhost:5000/images\07071742109.png', 1016)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1028, N'http://localhost:5000/images\07091520389.png', 1017)
INSERT [dbo].[ActivityPicture] ([Id], [Path], [ActivityId]) VALUES (1029, N'http://localhost:5000/images\070915204018.png', 1017)
SET IDENTITY_INSERT [dbo].[ActivityPicture] OFF
GO
SET IDENTITY_INSERT [dbo].[CostType] ON 

INSERT [dbo].[CostType] ([Id], [TypeName], [IsDelete]) VALUES (1, N'餐饮', 0)
INSERT [dbo].[CostType] ([Id], [TypeName], [IsDelete]) VALUES (2, N'活动', 0)
INSERT [dbo].[CostType] ([Id], [TypeName], [IsDelete]) VALUES (3, N'缴费', 0)
INSERT [dbo].[CostType] ([Id], [TypeName], [IsDelete]) VALUES (4, N'日常用品', 0)
INSERT [dbo].[CostType] ([Id], [TypeName], [IsDelete]) VALUES (5, N'其他', 0)
SET IDENTITY_INSERT [dbo].[CostType] OFF
GO
SET IDENTITY_INSERT [dbo].[Grade] ON 

INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (1, N'WE', CAST(N'2020-05-11' AS Date), 2, 0)
INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (2, N'RNG', CAST(N'2020-05-11' AS Date), 2, 0)
INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (3, N'EDG', CAST(N'2020-05-11' AS Date), 4, NULL)
INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (4, N'IG', CAST(N'2020-05-11' AS Date), 2, 0)
INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (5, N'JDG', CAST(N'2020-05-26' AS Date), 4, NULL)
INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (9, N'SKT', CAST(N'2020-06-02' AS Date), 2, 0)
INSERT [dbo].[Grade] ([Id], [Name], [CreateTime], [UserId], [IsDelete]) VALUES (10, N'v5', CAST(N'2020-06-29' AS Date), 2, 0)
SET IDENTITY_INSERT [dbo].[Grade] OFF
GO
SET IDENTITY_INSERT [dbo].[GradeCost] ON 

INSERT [dbo].[GradeCost] ([Id], [IsEpense], [CostTypeId], [Remark], [GradeId], [CreateTime], [IsDelete]) VALUES (1, 1, 1, N'春游卖零食L', 1, CAST(N'2020-05-11' AS Date), 0)
SET IDENTITY_INSERT [dbo].[GradeCost] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Remark], [IsDelete]) VALUES (1, N'超级管理员', N'超级管理员', 0)
INSERT [dbo].[Role] ([Id], [Name], [Remark], [IsDelete]) VALUES (2, N'老师', N'老师', 0)
INSERT [dbo].[Role] ([Id], [Name], [Remark], [IsDelete]) VALUES (6, N'test', N'测试角色', 0)
INSERT [dbo].[Role] ([Id], [Name], [Remark], [IsDelete]) VALUES (8, N'啦啦啦', N'lalalla', 0)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleAction] ON 

INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1342, 8, 25)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1343, 8, 26)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1345, 8, 21)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1346, 8, 22)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1518, 2, 3)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1519, 2, 59)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1520, 2, 24)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1521, 2, 23)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1522, 2, 74)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1523, 2, 62)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1524, 2, 61)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1525, 2, 30)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1526, 2, 29)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1527, 2, 8)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1528, 2, 2)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1529, 2, 7)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1600, 1, 59)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1601, 1, 23)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1602, 1, 50)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1603, 1, 31)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1604, 1, 6)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1605, 1, 69)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1606, 1, 55)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1607, 1, 47)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1608, 1, 51)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1609, 1, 28)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1610, 1, 5)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1611, 1, 4)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1612, 1, 56)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1613, 1, 26)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1614, 1, 25)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1615, 1, 22)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1616, 1, 27)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1617, 1, 24)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1618, 1, 7)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1619, 1, 29)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1620, 1, 68)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1621, 1, 67)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1622, 1, 66)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1623, 1, 65)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1624, 1, 74)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1625, 1, 64)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1626, 1, 8)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1627, 1, 63)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1628, 1, 61)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1629, 1, 60)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1630, 1, 3)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1631, 1, 2)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1632, 1, 77)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1633, 1, 30)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1634, 1, 62)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1635, 1, 21)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1746, 6, 59)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1747, 6, 23)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1748, 6, 51)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1749, 6, 50)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1750, 6, 31)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1751, 6, 6)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1752, 6, 69)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1753, 6, 55)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1754, 6, 47)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1755, 6, 28)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1756, 6, 27)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1757, 6, 5)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1758, 6, 24)
INSERT [dbo].[RoleAction] ([Id], [RoleId], [ActionId]) VALUES (1759, 6, 4)
SET IDENTITY_INSERT [dbo].[RoleAction] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (1, N'小宝', 1, CAST(N'1999-11-24' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (2, N'刘毕', 1, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (3, N'唐三', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (4, N'小舞', 1, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (5, N'马儿咋哈', 1, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (6, N'小鱼人', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (7, N'盲僧', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (8, N'喜之郎', 1, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (9, N'EZ', 1, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (10, N'薇恩', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (11, N'盖伦', 1, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (12, N'寒冰', 1, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (13, N'大虫子', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (14, N'船长', 0, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (15, N'比比东', 1, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (16, N'机器人', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (17, N'女枪', 1, CAST(N'2010-10-21' AS Date), 2, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (18, N'卡萨丁', 1, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (19, N'卡萨', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (20, N'霞', 1, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (21, N'洛', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (23, N'曙光女神', 0, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (24, N'小橘子', 0, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (25, N'钟无艳', 0, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (26, N'鬼谷子', 0, CAST(N'2010-10-21' AS Date), 4, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (27, N'雷欧娜', 0, CAST(N'2010-10-21' AS Date), 3, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (29, N'兰博', 1, CAST(N'2010-10-21' AS Date), 1, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (31, N'龙啸天', 1, CAST(N'2020-06-01' AS Date), 9, NULL)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (32, N'压缩', 1, CAST(N'2020-06-01' AS Date), 4, 0)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (33, N'神圣天使', 0, CAST(N'2001-03-08' AS Date), 5, NULL)
INSERT [dbo].[Student] ([Id], [Name], [Gender], [Birthday], [GradeId], [IsDelete]) VALUES (34, N'堕落天使', 0, CAST(N'2001-03-08' AS Date), 9, NULL)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (1, N'1435646097', N'12345678', N'排骨一号', 1, CAST(N'1999-10-10' AS Date), N'17674111269', N'http://localhost:5000/images\070915212110.png', N'湖南省,长沙市,芙蓉区', N'南塔公园哈哈哈', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (2, N'874585097', N'123456', N'小宝', 1, CAST(N'1999-11-13' AS Date), N'17674111343', N'http://localhost:5000/images\060920122415.png', N'湖南省,郴州市,北湖区', N'南塔公园', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (3, N'1020073404', N'123456', N'小舞', 0, CAST(N'1999-10-10' AS Date), N'17674111268', NULL, N'湖南省,郴州市,北湖区', N'南塔公园', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (4, N'liaoliao', N'123456', N'王也', 1, CAST(N'2020-05-13' AS Date), N'17674111222', N'http://localhost:5000/images\06092030587.png', N'湖南省,郴州市,北湖区', N'北湖公园', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (6, N'123213', N'123456', N'小屁', 0, CAST(N'2017-12-04' AS Date), N'17674111233', N'http://localhost:5000/images\062910594817.png', N'湖南省,长沙市,芙蓉区', N'桐籽坪路127号11', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (7, N'tangxiaowen', N'123456', N'骆义文', 0, CAST(N'2009-05-04' AS Date), N'17656555431', NULL, N'湖南省,长沙市,长沙县', N'湖南工程职业技术学院', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (11, N'12314asd', N'123456', N'小王', 0, CAST(N'2020-06-29' AS Date), N'17441112542', NULL, N'湖南省,长沙市,长沙县', N'湖南工程职业技术学院1', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (1012, N'14432342312', N'123456', N'小乖', NULL, CAST(N'2020-07-08' AS Date), N'14565567652', NULL, N'湖南省,郴州市,北湖区', N'小公园边', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (1013, N'14432342311', N'123456', N'小乖', NULL, CAST(N'2020-07-08' AS Date), N'14565567652', NULL, N'湖南省,郴州市,北湖区', N'小公园边', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (1014, N'14432342310', N'123456', N'小乖', NULL, CAST(N'2020-07-08' AS Date), N'14565567652', NULL, N'湖南省,郴州市,北湖区', N'小公园边', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (1015, N'14432342317', N'123456', N'小乖', 0, CAST(N'2020-07-08' AS Date), N'14565567652', N'http://localhost:5000/images\070815543015.png', N'湖南省,郴州市,北湖区', N'小公园边', 0)
INSERT [dbo].[User] ([Id], [Account], [Pwd], [Name], [Gender], [Birthday], [Phone], [Photo], [Address], [AddressDetail], [IsDelete]) VALUES (1017, N'10223213', N'123456', N'李白', 0, CAST(N'2000-06-13' AS Date), N'17765456523', N'http://localhost:5000/images\07081547524.png', N'湖南省,郴州市,北湖区', N'同仁街道', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (18, 2, 2)
INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (19, 4, 2)
INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (21, 7, 1)
INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (1029, 1, 1)
INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (1030, 6, 6)
INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (1034, 1017, 6)
INSERT [dbo].[UserRole] ([id], [AccountId], [RoleId]) VALUES (1035, 1015, 6)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
ALTER TABLE [dbo].[Action] ADD  CONSTRAINT [DF__Action__IsDelete__37A5467C]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[CostType] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Grade] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[GradeCost] ADD  DEFAULT ((0)) FOR [IsEpense]
GO
ALTER TABLE [dbo].[GradeCost] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__IsDelet__4316F928]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__Account__IsDelet__25869641]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_ActionType] FOREIGN KEY([ActionTypeId])
REFERENCES [dbo].[ActionType] ([Id])
GO
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_ActionType]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK__Activity__GradeI__3F466844] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grade] ([Id])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK__Activity__GradeI__3F466844]
GO
ALTER TABLE [dbo].[ActivityPicture]  WITH CHECK ADD  CONSTRAINT [FK__ActivityP__Activ__412EB0B6] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
GO
ALTER TABLE [dbo].[ActivityPicture] CHECK CONSTRAINT [FK__ActivityP__Activ__412EB0B6]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK__Grade__AccountId__35BCFE0A] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK__Grade__AccountId__35BCFE0A]
GO
ALTER TABLE [dbo].[GradeCost]  WITH CHECK ADD FOREIGN KEY([CostTypeId])
REFERENCES [dbo].[CostType] ([Id])
GO
ALTER TABLE [dbo].[GradeCost]  WITH CHECK ADD FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grade] ([Id])
GO
ALTER TABLE [dbo].[RoleAction]  WITH CHECK ADD  CONSTRAINT [FK__RoleActio__Actio__47DBAE45] FOREIGN KEY([ActionId])
REFERENCES [dbo].[Action] ([Id])
GO
ALTER TABLE [dbo].[RoleAction] CHECK CONSTRAINT [FK__RoleActio__Actio__47DBAE45]
GO
ALTER TABLE [dbo].[RoleAction]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK__Student__GradeId__4222D4EF] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grade] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK__Student__GradeId__4222D4EF]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK__AccountRo__Accou__2B3F6F97] FOREIGN KEY([AccountId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK__AccountRo__Accou__2B3F6F97]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
USE [master]
GO
ALTER DATABASE [InfantsSchoolSystem] SET  READ_WRITE 
GO
