USE [QRMS]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderNo] [nvarchar](50) NOT NULL,
	[PurchaseOrderDate] [datetime] NULL,
	[ExportStatus] [char](1) NOT NULL,
	[PrintStatus] [char](1) NOT NULL,
	[GetDataStatus] [char](1) NOT NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UserUpdate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderItem]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderID] [int] NOT NULL,
	[PurchaseOrderNo] [nvarchar](50) NOT NULL,
	[PurchaseOrderDate] [date] NULL,
	[ItemCode] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](200) NULL,
	[ItemType] [nvarchar](50) NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
	[Unit] [nvarchar](50) NULL,
	[LocationCode] [nvarchar](50) NULL,
	[LocationName] [nvarchar](200) NULL,
	[SupplierCode] [nvarchar](50) NULL,
	[SupplierName] [nvarchar](200) NULL,
	[PrintStatus] [char](1) NOT NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PurchaseOrderItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrder]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrderNo] [nvarchar](50) NOT NULL,
	[SaleOrderDate] [datetime] NULL,
	[ExportStatus] [char](1) NOT NULL,
	[GetDataStatus] [char](1) NOT NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UserUpdate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_SaleOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrderItem]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrderItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrderID] [int] NOT NULL,
	[SaleOrderNo] [nvarchar](50) NOT NULL,
	[SaleOrderDate] [datetime] NULL,
	[DeliveryDate] [datetime] NULL,
	[CustomerCode] [nvarchar](50) NULL,
	[CustomerName] [nvarchar](200) NULL,
	[LocationCode] [nvarchar](50) NULL,
	[LocationName] [nvarchar](200) NULL,
	[ItemType] [nvarchar](50) NOT NULL,
	[ItemCode] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](200) NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
	[Unit] [nvarchar](50) NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [nvarchar](50) NULL,
 CONSTRAINT [PK_SaleOrderItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionHistory]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionHistory](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionType] [nvarchar](50) NOT NULL,
	[OrderNo] [nvarchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[LocationCode_From] [nvarchar](50) NULL,
	[LocationName_From] [nvarchar](200) NULL,
	[WarehouseCode_From] [nvarchar](50) NULL,
	[WarehouseName_From] [nvarchar](200) NULL,
	[WarehouseType_From] [char](1) NULL,
	[LocationCode_To] [nvarchar](50) NULL,
	[LocationName_To] [nvarchar](200) NULL,
	[WarehouseCode_To] [nvarchar](50) NULL,
	[WarehouseName_To] [nvarchar](200) NULL,
	[WarehouseType_To] [char](1) NULL,
	[ItemCode] [nvarchar](50) NULL,
	[ItemName] [nvarchar](200) NULL,
	[ItemType] [nvarchar](2) NULL,
	[Quantity] [decimal](18, 3) NULL,
	[Unit] [nvarchar](50) NULL,
	[EXT_OtherCode] [nvarchar](50) NULL,
	[EXT_Serial] [nvarchar](50) NULL,
	[EXT_PartNo] [nvarchar](50) NULL,
	[EXT_LotNo] [nvarchar](50) NULL,
	[EXT_MfDate] [datetime] NULL,
	[EXT_RecDate] [datetime] NULL,
	[EXT_ExpDate] [datetime] NULL,
	[EXT_QRCode] [nvarchar](200) NULL,
	[CustomerCode] [nvarchar](50) NULL,
	[CustomerName] [nvarchar](200) NULL,
	[SupplierCode] [nvarchar](50) NULL,
	[SupplierName] [nvarchar](200) NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [nvarchar](50) NULL,
 CONSTRAINT [PK_TransactionHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferInstruction]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferInstruction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransferOrderNo] [nvarchar](50) NOT NULL,
	[InstructionDate] [datetime] NULL,
	[GetDataStatus] [char](1) NOT NULL,
	[TransferStatus] [char](1) NOT NULL,
	[LocationCode_From] [nvarchar](50) NULL,
	[LocationName_From] [nvarchar](200) NULL,
	[WarehouseCode_From] [nvarchar](50) NULL,
	[WarehouseName_From] [nvarchar](200) NULL,
	[WarehouseType_From] [char](1) NULL,
	[LocationCode_To] [nvarchar](50) NULL,
	[LocationName_To] [nvarchar](200) NULL,
	[WarehouseCode_To] [nvarchar](50) NULL,
	[WarehouseName_To] [nvarchar](200) NULL,
	[WarehouseType_To] [char](1) NULL,
	[TransferType] [char](1) NOT NULL,
	[Remark] [nvarchar](600) NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [nvarchar](50) NULL,
 CONSTRAINT [PK_TransferInstruction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferInstructionItem]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferInstructionItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransferOrderID] [int] NOT NULL,
	[TransferOrderNo] [nvarchar](50) NOT NULL,
	[InstructionDate] [datetime] NULL,
	[GetDataStatus] [char](1) NOT NULL,
	[TransferStatus] [char](1) NOT NULL,
	[LocationCode_From] [nvarchar](50) NULL,
	[LocationName_From] [nvarchar](200) NULL,
	[WarehouseCode_From] [nvarchar](50) NULL,
	[WarehouseName_From] [nvarchar](200) NULL,
	[WarehouseType_From] [char](1) NULL,
	[LocationCode_To] [nvarchar](50) NULL,
	[LocationName_To] [nvarchar](200) NULL,
	[WarehouseCode_To] [nvarchar](50) NULL,
	[WarehouseName_To] [nvarchar](200) NULL,
	[WarehouseType_To] [char](1) NULL,
	[TransferType] [char](1) NOT NULL,
	[ItemCode] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](200) NULL,
	[ItemType] [nvarchar](50) NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
	[Unit] [nvarchar](50) NULL,
	[Remark] [nvarchar](600) NULL,
	[RecordStatus] [char](1) NULL,
	[CreateDate] [datetime] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [nvarchar](50) NULL,
 CONSTRAINT [PK_TransferInstructionItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/24/2021 11:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ctype] [char](1) NULL,
	[Code] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Role] [nvarchar](50) NULL,
	[WarehouseCode] [nvarchar](50) NULL,
	[RecordStatus] [char](1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  CONSTRAINT [DF_PurchaseOrder_ExportStatus]  DEFAULT ('N') FOR [ExportStatus]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  CONSTRAINT [DF_PurchaseOrder_PrintStatus]  DEFAULT ('N') FOR [PrintStatus]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  CONSTRAINT [DF_PurchaseOrder_GetDataStatus]  DEFAULT ('N') FOR [GetDataStatus]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  CONSTRAINT [DF_PurchaseOrder_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
ALTER TABLE [dbo].[PurchaseOrderItem] ADD  CONSTRAINT [DF_PurchaseOrderItem_PrintStatus]  DEFAULT ('N') FOR [PrintStatus]
GO
ALTER TABLE [dbo].[PurchaseOrderItem] ADD  CONSTRAINT [DF_PurchaseOrderItem_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
ALTER TABLE [dbo].[SaleOrder] ADD  CONSTRAINT [DF_SaleOrder_ExportStatus]  DEFAULT ('N') FOR [ExportStatus]
GO
ALTER TABLE [dbo].[SaleOrder] ADD  CONSTRAINT [DF_SaleOrder_GetDataStatus]  DEFAULT ('N') FOR [GetDataStatus]
GO
ALTER TABLE [dbo].[SaleOrder] ADD  CONSTRAINT [DF_SaleOrder_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
ALTER TABLE [dbo].[SaleOrderItem] ADD  CONSTRAINT [DF_SaleOrderItem_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
ALTER TABLE [dbo].[TransactionHistory] ADD  CONSTRAINT [DF_TransactionHistory_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
ALTER TABLE [dbo].[TransferInstruction] ADD  CONSTRAINT [DF_TransferInstruction_GetDataStatus]  DEFAULT ('N') FOR [GetDataStatus]
GO
ALTER TABLE [dbo].[TransferInstruction] ADD  CONSTRAINT [DF_TransferInstruction_TransferStatus]  DEFAULT ('N') FOR [TransferStatus]
GO
ALTER TABLE [dbo].[TransferInstruction] ADD  CONSTRAINT [DF_TransferInstruction_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
ALTER TABLE [dbo].[TransferInstructionItem] ADD  CONSTRAINT [DF_TransferInstructionItem_GetDataStatus]  DEFAULT ('N') FOR [GetDataStatus]
GO
ALTER TABLE [dbo].[TransferInstructionItem] ADD  CONSTRAINT [DF_TransferInstructionItem_TransferStatus]  DEFAULT ('N') FOR [TransferStatus]
GO
ALTER TABLE [dbo].[TransferInstructionItem] ADD  CONSTRAINT [DF_TransferInstructionItem_RecordStatus]  DEFAULT ('N') FOR [RecordStatus]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N: new; U: update; D: delete' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
