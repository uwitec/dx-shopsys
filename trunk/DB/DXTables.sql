USE [DXDB]
GO

/****** Object:  Table [dbo].[assets]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[assets](
	[AssetId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[First] [nvarchar](128) NULL,
	[Last] [nvarchar](128) NULL,
	[Gender] [nvarchar](128) NULL,
	[IC] [nvarchar](128) NULL,
	[BirthDate] [datetime] NOT NULL,
	[Photo] [nvarchar](128) NULL,
	[TagName] [nvarchar](128) NULL,
	[TagMacAddress] [nvarchar](128) NULL,
	[Name] [nvarchar](128) NULL,
	[BatteryState] [int] NULL,
	[PowerLowTime] [datetime] NULL,
	[InTime] [datetime] NULL,
	[OutTIme] [datetime] NULL,
	[InMineTime] [datetime] NULL,
	[OutMineTime] [datetime] NULL,
	[Distance] [float] NULL,
	[GasDensity] [float] NULL,
	[State] [int] NULL,
	[ClassTypeId] [varchar](50) NULL,
	[AddTime] [datetime] NULL,
	[IsValid] [int] NULL,
	[Show] [int] NULL,
	[RegionId] [int] NULL,
	[AnchorId] [int] NULL,
	[DepartmentId] [int] NULL,
	[JobFunctionId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	[X] [float] NULL,
	[Y] [float] NULL,
	[Z] [float] NULL,
	[LastInMineTime] [datetime] NULL,
	[LastOutMineTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[DXAdmin]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DXAdmin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Pwd] [varchar](50) NULL,
	[LoginTime] [datetime] NULL,
	[isAdmin] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_DXAdmin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[DXLb]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DXLb](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LbName] [varchar](50) NULL,
	[ParentId] [int] NULL,
	[Description] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[OrderId] [int] NULL,
 CONSTRAINT [PK_DXLb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[DXMember]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DXMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Pwd] [varchar](50) NULL,
	[LoginTime] [datetime] NULL,
	[RegTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dxMember] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[DXProd]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DXProd](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[lbid] [int] NULL,
	[Name] [varchar](500) NOT NULL,
	[Description] [ntext] NULL,
	[Body] [ntext] NOT NULL,
	[Price] [float] NULL,
	[Price2] [float] NULL,
	[Price3] [float] NULL,
	[CreateTime] [datetime] NULL,
	[EditorName] [varchar](50) NULL,
	[CreatorName] [varchar](50) NULL,
	[EditTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[OrderId] [int] NULL,
	[isIndex] [bit] NOT NULL,
	[isTop] [bit] NOT NULL,
	[pic] [varchar](8000) NULL,
	[picSmall] [varchar](100) NULL,
	[pics] [varchar](5000) NULL,
 CONSTRAINT [PK_dxProd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[Regions]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Regions](
	[RegionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Type] [int] NOT NULL,
	[PerNum] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[TagBlinkLogs]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TagBlinkLogs](
	[TagId] [int] NOT NULL,
	[AnchorId] [int] NOT NULL,
	[Distance] [int] NULL,
	[GasDensity] [float] NULL,
	[Capabilities] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Status] [int] NOT NULL
) ON [PRIMARY]

GO

USE [DXDB]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 01/17/2014 18:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[RoleId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[LoginName] [nvarchar](128) NULL,
	[HashPassword] [nvarchar](128) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DXProd] ADD  CONSTRAINT [DF_dxProd_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[DXProd] ADD  CONSTRAINT [DF_dxProd_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[DXProd] ADD  CONSTRAINT [DF_DXProd_isIndex]  DEFAULT ((0)) FOR [isIndex]
GO

ALTER TABLE [dbo].[DXProd] ADD  CONSTRAINT [DF_DXProd_isTop]  DEFAULT ((0)) FOR [isTop]
GO


