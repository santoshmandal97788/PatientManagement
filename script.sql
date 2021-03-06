USE [HospitalDB]
GO
/****** Object:  Table [dbo].[tblPatient]    Script Date: 6/3/2021 10:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPatient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatImage] [varbinary](max) NULL,
	[PatName] [nvarchar](100) NOT NULL,
	[PatContact] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[EntryDate] [date] NOT NULL,
 CONSTRAINT [PK_tblPatient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spCreatePatient]    Script Date: 6/3/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spCreatePatient]
(
@patImage varbinary(max),
@patName nvarchar(50),
@patContact nvarchar(10),
@isActive bit,
@entryDate date
)
as 
begin 
insert into tblPatient (PatImage, PatName, PatContact, IsActive, EntryDate) values (@patImage, @patName, @patContact, @isActive, @entryDate)
end
GO
/****** Object:  StoredProcedure [dbo].[spDeletePatient]    Script Date: 6/3/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spDeletePatient]
(
@id int

)
as 
begin 
delete from tblPatient where Id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[spGetAllPatient]    Script Date: 6/3/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spGetAllPatient]

as 
begin 
select * from tblPatient 
end
GO
/****** Object:  StoredProcedure [dbo].[spGetPatientById]    Script Date: 6/3/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spGetPatientById]
(
@id int
)
as 
begin 
select * from tblPatient where Id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePatient]    Script Date: 6/3/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spUpdatePatient]
(
@id int,
@patImage varbinary(max),
@patName nvarchar(50),
@patContact nvarchar(10),
@isActive bit,
@entryDate date
)
as 
begin 
update  tblPatient set PatImage= @patImage, PatName=@patName, PatContact=@patContact, IsActive=@isActive, EntryDate=@entryDate where Id=@id
end
GO
