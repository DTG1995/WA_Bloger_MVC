USE [WA_Bloger]
GO
/****** Object:  Table [dbo].[WA_BlogPosts]    Script Date: 5/22/2017 2:07:11 AM ******/
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
/****** Object:  Table [dbo].[WA_Blogs]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Blogs](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Parent] [int] NULL,
	[Order] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Comments]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[ContenComment] [ntext] NULL,
	[PostID] [int] NULL,
	[Author] [int] NULL,
	[Created] [datetime] NULL,
	[Parent] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_GroupUser]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_GroupUser](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_WA_GroupUser] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Likes]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Likes](
	[PostID] [int] NOT NULL,
	[Author] [int] NOT NULL,
	[IsLike] [int] NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Options]    Script Date: 5/22/2017 2:07:11 AM ******/
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
/****** Object:  Table [dbo].[WA_Posts]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [ntext] NULL,
	[Description] [ntext] NULL,
	[ContentPost] [ntext] NULL,
	[Created] [datetime] NOT NULL,
	[Author] [int] NULL,
	[Picture] [ntext] NULL,
	[UseDescription] [bit] NULL,
	[Seen] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Roles]    Script Date: 5/22/2017 2:07:11 AM ******/
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
/****** Object:  Table [dbo].[WA_UserRoles]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WA_UserRoles](
	[GroupID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WA_Users]    Script Date: 5/22/2017 2:07:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WA_Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
	[Avatar] [ntext] NULL,
	[LastLogin] [datetime] NULL,
	[GroupID] [int] NULL,
	[IsAdmin] [bit] NOT NULL,
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
ALTER TABLE [dbo].[WA_Users] ADD  CONSTRAINT [DF_WA_Users_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
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
ALTER TABLE [dbo].[WA_Comments]  WITH CHECK ADD  CONSTRAINT [FK_WA_Comments_WA_Comments] FOREIGN KEY([Parent])
REFERENCES [dbo].[WA_Comments] ([CommentID])
GO
ALTER TABLE [dbo].[WA_Comments] CHECK CONSTRAINT [FK_WA_Comments_WA_Comments]
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
ALTER TABLE [dbo].[WA_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_WA_UserRoles_WA_GroupUser] FOREIGN KEY([GroupID])
REFERENCES [dbo].[WA_GroupUser] ([GroupID])
GO
ALTER TABLE [dbo].[WA_UserRoles] CHECK CONSTRAINT [FK_WA_UserRoles_WA_GroupUser]
GO
ALTER TABLE [dbo].[WA_Users]  WITH CHECK ADD  CONSTRAINT [FK_WA_Users_WA_GroupUser] FOREIGN KEY([GroupID])
REFERENCES [dbo].[WA_GroupUser] ([GroupID])
GO
ALTER TABLE [dbo].[WA_Users] CHECK CONSTRAINT [FK_WA_Users_WA_GroupUser]
GO
