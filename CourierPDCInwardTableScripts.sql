USE [HDFC_LIve]
GO
/****** Object:  Table [dbo].[CourierMaster]    Script Date: 19-10-2022 17:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourierMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Courier] [varchar](50) NULL,
 CONSTRAINT [PK_CourierMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InwardManualUpload]    Script Date: 19-10-2022 17:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InwardManualUpload](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AGEEMENTNO] [numeric](18, 0) NULL,
	[BARCODE] [nvarchar](100) NULL,
	[PRODUCTNAME] [nvarchar](200) NULL,
	[DISBURSEMENTDATE] [datetime] NULL,
	[CUSTOMERNAME] [nvarchar](200) NULL,
	[SYSTEM] [nvarchar](10) NULL,
	[CASESTARTERBRANCH] [nvarchar](50) NULL,
	[GENERATEDON] [datetime] NULL,
	[CPUID] [nvarchar](10) NULL,
	[batch_id] [nvarchar](200) NULL,
	[RepaymentMode] [nvarchar](10) NULL,
	[VendorLocation] [nvarchar](200) NULL,
	[VendorID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_date] [datetime] NULL,
 CONSTRAINT [PK_ManualUpload] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PDCUpload]    Script Date: 19-10-2022 17:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PDCUpload](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AGREEMENTID] [numeric](18, 0) NULL,
	[APPROVALDATE] [datetime] NULL,
	[SecurityPDCCHQCount] [int] NULL,
 CONSTRAINT [PK_PDCUpload] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
