USE [WA_Bloger]
GO
/****** Object:  Table [dbo].[WA_BlogPosts]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_BlogPosts](
	[BlogID] [int] NOT NULL,
	[PostID] [int] NOT NULL,
 CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC,
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Blogs]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Blogs](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Parent] [int] NULL,
	[Order] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Comments]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WA_Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[ContenComment] [ntext] NULL,
	[PostID] [int] NULL,
	[Author] [char](10) NULL,
	[Created] [datetime] NULL,
	[Parent] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WA_Likes]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WA_Likes](
	[PostID] [int] NOT NULL,
	[Author] [char](10) NOT NULL,
	[IsLike] [int] NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WA_Options]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Options](
	[Option_ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [ntext] NULL,
	[Value] [ntext] NULL,
	[Group] [ntext] NULL,
 CONSTRAINT [PK_WA_Options] PRIMARY KEY CLUSTERED 
(
	[Option_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Posts]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WA_Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [ntext] NULL,
	[Description] [ntext] NULL,
	[ContentPost] [ntext] NULL,
	[Created] [datetime] NULL,
	[Author] [char](10) NULL,
	[Picture] [ntext] NULL,
	[UseDescription] [bit] NULL,
	[Seen] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WA_Roles]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_UserRoles]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WA_UserRoles](
	[UserID] [char](10) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WA_Users]    Script Date: 5/4/2017 11:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WA_Users](
	[UserID] [char](10) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
	[Avatar] [ntext] NULL,
	[LastLogin] [datetime] NULL,
	[IPLast] [varchar](50) NULL,
	[IPCreated] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[WA_Blogs] ON 

INSERT [dbo].[WA_Blogs] ([BlogID], [Name], [Parent], [Order], [Active]) VALUES (1, N'Lập trình Web', NULL, 1, 1)
INSERT [dbo].[WA_Blogs] ([BlogID], [Name], [Parent], [Order], [Active]) VALUES (2, N'Lập trình Web Tĩnh', 1, 1, 1)
INSERT [dbo].[WA_Blogs] ([BlogID], [Name], [Parent], [Order], [Active]) VALUES (3, N'Lập trình HTML', 2, 1, 1)
SET IDENTITY_INSERT [dbo].[WA_Blogs] OFF
ALTER TABLE [dbo].[WA_BlogPosts]  WITH CHECK ADD  CONSTRAINT [FK_BlogPosts_Blogs] FOREIGN KEY([BlogID])
REFERENCES [dbo].[WA_Blogs] ([BlogID])
GO
ALTER TABLE [dbo].[WA_BlogPosts] CHECK CONSTRAINT [FK_BlogPosts_Blogs]
GO
ALTER TABLE [dbo].[WA_BlogPosts]  WITH CHECK ADD  CONSTRAINT [FK_BlogPosts_Posts] FOREIGN KEY([PostID])
REFERENCES [dbo].[WA_Posts] ([PostID])
GO
ALTER TABLE [dbo].[WA_BlogPosts] CHECK CONSTRAINT [FK_BlogPosts_Posts]
GO
ALTER TABLE [dbo].[WA_Blogs]  WITH CHECK ADD  CONSTRAINT [FK_WA_Blogs_WA_Blogs] FOREIGN KEY([Parent])
REFERENCES [dbo].[WA_Blogs] ([BlogID])
GO
ALTER TABLE [dbo].[WA_Blogs] CHECK CONSTRAINT [FK_WA_Blogs_WA_Blogs]
GO
ALTER TABLE [dbo].[WA_Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Posts] FOREIGN KEY([PostID])
REFERENCES [dbo].[WA_Posts] ([PostID])
GO
ALTER TABLE [dbo].[WA_Comments] CHECK CONSTRAINT [FK_Comments_Posts]
GO
ALTER TABLE [dbo].[WA_Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([Author])
REFERENCES [dbo].[WA_Users] ([UserID])
GO
ALTER TABLE [dbo].[WA_Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[WA_Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Posts] FOREIGN KEY([PostID])
REFERENCES [dbo].[WA_Posts] ([PostID])
GO
ALTER TABLE [dbo].[WA_Likes] CHECK CONSTRAINT [FK_Likes_Posts]
GO
ALTER TABLE [dbo].[WA_Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Users] FOREIGN KEY([Author])
REFERENCES [dbo].[WA_Users] ([UserID])
GO
ALTER TABLE [dbo].[WA_Likes] CHECK CONSTRAINT [FK_Likes_Users]
GO
ALTER TABLE [dbo].[WA_Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([Author])
REFERENCES [dbo].[WA_Users] ([UserID])
GO
ALTER TABLE [dbo].[WA_Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO
ALTER TABLE [dbo].[WA_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[WA_Roles] ([RoleID])
GO
ALTER TABLE [dbo].[WA_UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[WA_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[WA_Users] ([UserID])
GO
ALTER TABLE [dbo].[WA_UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
