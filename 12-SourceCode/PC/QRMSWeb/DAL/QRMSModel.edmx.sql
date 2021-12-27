
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/26/2021 07:37:26
-- Generated from EDMX file: C:\Users\w10\Desktop\UTECH\QRCODE-MAG-SYS-UTECH\12-SourceCode\PC\QRMSWeb\DAL\QRMSModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QRMS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PurchaseOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseOrder];
GO
IF OBJECT_ID(N'[dbo].[PurchaseOrderItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseOrderItem];
GO
IF OBJECT_ID(N'[dbo].[SaleOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleOrder];
GO
IF OBJECT_ID(N'[dbo].[SaleOrderItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleOrderItem];
GO
IF OBJECT_ID(N'[dbo].[TransactionHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionHistory];
GO
IF OBJECT_ID(N'[dbo].[TransferInstruction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransferInstruction];
GO
IF OBJECT_ID(N'[dbo].[TransferInstructionItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransferInstructionItem];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PurchaseOrders'
CREATE TABLE [dbo].[PurchaseOrders] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PurchaseOrderNo] nvarchar(50)  NOT NULL,
    [PurchaseOrderDate] datetime  NULL,
    [ExportStatus] char(1)  NOT NULL,
    [InputStatus] char(1)  NOT NULL,
    [PrintStatus] char(1)  NOT NULL,
    [GetDataStatus] char(1)  NOT NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UserUpdate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'PurchaseOrderItems'
CREATE TABLE [dbo].[PurchaseOrderItems] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PurchaseOrderID] int  NOT NULL,
    [PurchaseOrderNo] nvarchar(50)  NOT NULL,
    [PurchaseOrderDate] datetime  NULL,
    [ItemCode] nvarchar(50)  NOT NULL,
    [ItemName] nvarchar(200)  NULL,
    [ItemType] nvarchar(50)  NOT NULL,
    [Quantity] decimal(18,3)  NOT NULL,
    [Unit] nvarchar(50)  NULL,
    [LocationCode] nvarchar(50)  NULL,
    [LocationName] nvarchar(200)  NULL,
    [SupplierCode] nvarchar(50)  NULL,
    [SupplierName] nvarchar(200)  NULL,
    [InputStatus] char(1)  NOT NULL,
    [PrintStatus] char(1)  NOT NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [UserUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'SaleOrders'
CREATE TABLE [dbo].[SaleOrders] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SaleOrderNo] nvarchar(50)  NOT NULL,
    [SaleOrderDate] datetime  NULL,
    [ExportStatus] char(1)  NOT NULL,
    [GetDataStatus] char(1)  NOT NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UserUpdate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'SaleOrderItems'
CREATE TABLE [dbo].[SaleOrderItems] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SaleOrderID] int  NOT NULL,
    [SaleOrderNo] nvarchar(50)  NOT NULL,
    [SaleOrderDate] datetime  NULL,
    [DeliveryDate] datetime  NULL,
    [CustomerCode] nvarchar(50)  NULL,
    [CustomerName] nvarchar(200)  NULL,
    [LocationCode] nvarchar(50)  NULL,
    [LocationName] nvarchar(200)  NULL,
    [ItemType] nvarchar(50)  NOT NULL,
    [ItemCode] nvarchar(50)  NOT NULL,
    [ItemName] nvarchar(200)  NULL,
    [Quantity] decimal(18,3)  NOT NULL,
    [Unit] nvarchar(50)  NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [UserUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'TransactionHistories'
CREATE TABLE [dbo].[TransactionHistories] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [TransactionType] nvarchar(50)  NOT NULL,
    [OrderNo] nvarchar(50)  NULL,
    [OrderDate] datetime  NULL,
    [LocationCode_From] nvarchar(50)  NULL,
    [LocationName_From] nvarchar(200)  NULL,
    [WarehouseCode_From] nvarchar(50)  NULL,
    [WarehouseName_From] nvarchar(200)  NULL,
    [WarehouseType_From] char(1)  NULL,
    [LocationCode_To] nvarchar(50)  NULL,
    [LocationName_To] nvarchar(200)  NULL,
    [WarehouseCode_To] nvarchar(50)  NULL,
    [WarehouseName_To] nvarchar(200)  NULL,
    [WarehouseType_To] char(1)  NULL,
    [ItemCode] nvarchar(50)  NULL,
    [ItemName] nvarchar(200)  NULL,
    [ItemType] nvarchar(2)  NULL,
    [Quantity] decimal(18,3)  NULL,
    [Unit] nvarchar(50)  NULL,
    [EXT_OtherCode] nvarchar(50)  NULL,
    [EXT_Serial] nvarchar(50)  NULL,
    [EXT_PartNo] nvarchar(50)  NULL,
    [EXT_LotNo] nvarchar(50)  NULL,
    [EXT_MfDate] datetime  NULL,
    [EXT_RecDate] datetime  NULL,
    [EXT_ExpDate] datetime  NULL,
    [EXT_QRCode] nvarchar(200)  NULL,
    [CustomerCode] nvarchar(50)  NULL,
    [CustomerName] nvarchar(200)  NULL,
    [SupplierCode] nvarchar(50)  NULL,
    [SupplierName] nvarchar(200)  NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [UserUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'TransferInstructions'
CREATE TABLE [dbo].[TransferInstructions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TransferOrderNo] nvarchar(50)  NOT NULL,
    [InstructionDate] datetime  NULL,
    [GetDataStatus] char(1)  NOT NULL,
    [TransferStatus] char(1)  NOT NULL,
    [LocationCode_From] nvarchar(50)  NULL,
    [LocationName_From] nvarchar(200)  NULL,
    [WarehouseCode_From] nvarchar(50)  NULL,
    [WarehouseName_From] nvarchar(200)  NULL,
    [WarehouseType_From] char(1)  NULL,
    [LocationCode_To] nvarchar(50)  NULL,
    [LocationName_To] nvarchar(200)  NULL,
    [WarehouseCode_To] nvarchar(50)  NULL,
    [WarehouseName_To] nvarchar(200)  NULL,
    [WarehouseType_To] char(1)  NULL,
    [TransferType] char(1)  NOT NULL,
    [Remark] nvarchar(600)  NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [UserUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'TransferInstructionItems'
CREATE TABLE [dbo].[TransferInstructionItems] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TransferOrderID] int  NOT NULL,
    [TransferOrderNo] nvarchar(50)  NOT NULL,
    [InstructionDate] datetime  NULL,
    [GetDataStatus] char(1)  NOT NULL,
    [TransferStatus] char(1)  NOT NULL,
    [LocationCode_From] nvarchar(50)  NULL,
    [LocationName_From] nvarchar(200)  NULL,
    [WarehouseCode_From] nvarchar(50)  NULL,
    [WarehouseName_From] nvarchar(200)  NULL,
    [WarehouseType_From] char(1)  NULL,
    [LocationCode_To] nvarchar(50)  NULL,
    [LocationName_To] nvarchar(200)  NULL,
    [WarehouseCode_To] nvarchar(50)  NULL,
    [WarehouseName_To] nvarchar(200)  NULL,
    [WarehouseType_To] char(1)  NULL,
    [TransferType] char(1)  NOT NULL,
    [ItemCode] nvarchar(50)  NOT NULL,
    [ItemName] nvarchar(200)  NULL,
    [ItemType] nvarchar(50)  NOT NULL,
    [Quantity] decimal(18,3)  NOT NULL,
    [Unit] nvarchar(50)  NULL,
    [Remark] nvarchar(600)  NULL,
    [RecordStatus] char(1)  NULL,
    [CreateDate] datetime  NULL,
    [UserCreate] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [UserUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Ctype] char(1)  NULL,
    [Code] nvarchar(50)  NOT NULL,
    [FullName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Role] nvarchar(50)  NULL,
    [WarehouseCode] nvarchar(50)  NULL,
    [RecordStatus] char(1)  NOT NULL,
    [CreateDate] datetime  NULL,
    [CreateUser] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateUser] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'PurchaseOrders'
ALTER TABLE [dbo].[PurchaseOrders]
ADD CONSTRAINT [PK_PurchaseOrders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PurchaseOrderItems'
ALTER TABLE [dbo].[PurchaseOrderItems]
ADD CONSTRAINT [PK_PurchaseOrderItems]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SaleOrders'
ALTER TABLE [dbo].[SaleOrders]
ADD CONSTRAINT [PK_SaleOrders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SaleOrderItems'
ALTER TABLE [dbo].[SaleOrderItems]
ADD CONSTRAINT [PK_SaleOrderItems]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TransactionHistories'
ALTER TABLE [dbo].[TransactionHistories]
ADD CONSTRAINT [PK_TransactionHistories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TransferInstructions'
ALTER TABLE [dbo].[TransferInstructions]
ADD CONSTRAINT [PK_TransferInstructions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TransferInstructionItems'
ALTER TABLE [dbo].[TransferInstructionItems]
ADD CONSTRAINT [PK_TransferInstructionItems]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------