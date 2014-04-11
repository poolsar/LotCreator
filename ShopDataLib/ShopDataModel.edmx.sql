
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/08/2014 18:23:45
-- Generated from EDMX file: C:\Users\victor\Downloads\___БИЗНЕС\Игрушки\Автоматизация\PrestaSharp-master\ShopDataLib\ShopDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LadShop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CategoryProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopProductSet] DROP CONSTRAINT [FK_CategoryProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageShopCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageSet] DROP CONSTRAINT [FK_ImageShopCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageShopCategory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopCategorySet] DROP CONSTRAINT [FK_ImageShopCategory1];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageShopProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageSet] DROP CONSTRAINT [FK_ImageShopProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageShopProduct1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopProductSet] DROP CONSTRAINT [FK_ImageShopProduct1];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopCategoryShopCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopCategorySet] DROP CONSTRAINT [FK_ShopCategoryShopCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopCategorySupplierCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierCategorySet] DROP CONSTRAINT [FK_ShopCategorySupplierCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierCategorySupplierCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierCategorySet] DROP CONSTRAINT [FK_SupplierCategorySupplierCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierCategorySupplierImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageSet] DROP CONSTRAINT [FK_SupplierCategorySupplierImage];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierCategorySupplierProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierProductSet] DROP CONSTRAINT [FK_SupplierCategorySupplierProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierProductImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageSet] DROP CONSTRAINT [FK_SupplierProductImage];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierProductProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierProductSet] DROP CONSTRAINT [FK_SupplierProductProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierProductSupplierImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageSet] DROP CONSTRAINT [FK_SupplierProductSupplierImage];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierSupplierCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierCategorySet] DROP CONSTRAINT [FK_SupplierSupplierCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierSupplierProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierProductSet] DROP CONSTRAINT [FK_SupplierSupplierProduct];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ImageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageSet];
GO
IF OBJECT_ID(N'[dbo].[PropertyHystorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropertyHystorySet];
GO
IF OBJECT_ID(N'[dbo].[SettingsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingsSet];
GO
IF OBJECT_ID(N'[dbo].[ShopCategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShopCategorySet];
GO
IF OBJECT_ID(N'[dbo].[ShopProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShopProductSet];
GO
IF OBJECT_ID(N'[dbo].[SupplierCategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierCategorySet];
GO
IF OBJECT_ID(N'[dbo].[SupplierProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierProductSet];
GO
IF OBJECT_ID(N'[dbo].[SupplierSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SupplierProductSet'
CREATE TABLE [dbo].[SupplierProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [IdOnSource] nvarchar(max)  NOT NULL,
    [UriOnSource] nvarchar(max)  NOT NULL,
    [DiscountPrice] decimal(18,0)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [IsSale] bit  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CostPrice] decimal(18,0)  NOT NULL,
    [StatusCode] int  NOT NULL,
    [Supplier_Id] int  NOT NULL,
    [ShopProduct_Id] int  NULL,
    [Category_Id] int  NULL
);
GO

-- Creating table 'SupplierSet'
CREATE TABLE [dbo].[SupplierSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Uri] nvarchar(max)  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'SupplierCategorySet'
CREATE TABLE [dbo].[SupplierCategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [IdOnSource] nvarchar(max)  NOT NULL,
    [UriOnSource] nvarchar(max)  NOT NULL,
    [StatusCode] int  NOT NULL,
    [Supplier_Id] int  NOT NULL,
    [Parent_Id] int  NULL,
    [ShopCategory_Id] int  NULL
);
GO

-- Creating table 'ShopProductSet'
CREATE TABLE [dbo].[ShopProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [DiscountPrice] decimal(18,0)  NOT NULL,
    [IsSale] bit  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ShortDescription] nvarchar(max)  NOT NULL,
    [InShop] bit  NOT NULL,
    [IdOnWebStore] int  NULL,
    [Unity] nvarchar(max)  NULL,
    [LinkRewrite] nvarchar(max)  NULL,
    [Quantity] int  NULL,
    [Category_Id] int  NOT NULL,
    [DefaultImage_Id] int  NULL
);
GO

-- Creating table 'ShopCategorySet'
CREATE TABLE [dbo].[ShopCategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [InShop] bit  NOT NULL,
    [IdOnWebStore] int  NULL,
    [Description] nvarchar(max)  NULL,
    [LinkRewrite] nvarchar(max)  NULL,
    [Parent_Id] int  NULL,
    [DefaultImage_Id] int  NULL
);
GO

-- Creating table 'ImageSet'
CREATE TABLE [dbo].[ImageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UriOnSupplier] nvarchar(max)  NOT NULL,
    [LocalPath] nvarchar(max)  NOT NULL,
    [StatusCode] int  NOT NULL,
    [IdOnWebStore] int  NULL,
    [SupplierProduct_Id] int  NULL,
    [SupplierCategory_Id] int  NULL,
    [ShopCategory_Id] int  NULL,
    [ShopProduct_Id] int  NULL,
    [SupplierProductAsDefault_Id] int  NULL
);
GO

-- Creating table 'PropertyHystorySet'
CREATE TABLE [dbo].[PropertyHystorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Entity] nvarchar(max)  NOT NULL,
    [Property] nvarchar(max)  NOT NULL,
    [OldValue] nvarchar(max)  NOT NULL,
    [NewValue] nvarchar(max)  NOT NULL,
    [DateUpdate] datetime  NOT NULL,
    [EntityId] int  NOT NULL
);
GO

-- Creating table 'SettingsSet'
CREATE TABLE [dbo].[SettingsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SupplierProductSet'
ALTER TABLE [dbo].[SupplierProductSet]
ADD CONSTRAINT [PK_SupplierProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierSet'
ALTER TABLE [dbo].[SupplierSet]
ADD CONSTRAINT [PK_SupplierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierCategorySet'
ALTER TABLE [dbo].[SupplierCategorySet]
ADD CONSTRAINT [PK_SupplierCategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShopProductSet'
ALTER TABLE [dbo].[ShopProductSet]
ADD CONSTRAINT [PK_ShopProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShopCategorySet'
ALTER TABLE [dbo].[ShopCategorySet]
ADD CONSTRAINT [PK_ShopCategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [PK_ImageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PropertyHystorySet'
ALTER TABLE [dbo].[PropertyHystorySet]
ADD CONSTRAINT [PK_PropertyHystorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SettingsSet'
ALTER TABLE [dbo].[SettingsSet]
ADD CONSTRAINT [PK_SettingsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Supplier_Id] in table 'SupplierProductSet'
ALTER TABLE [dbo].[SupplierProductSet]
ADD CONSTRAINT [FK_SupplierSupplierProduct]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupplierProduct'
CREATE INDEX [IX_FK_SupplierSupplierProduct]
ON [dbo].[SupplierProductSet]
    ([Supplier_Id]);
GO

-- Creating foreign key on [Supplier_Id] in table 'SupplierCategorySet'
ALTER TABLE [dbo].[SupplierCategorySet]
ADD CONSTRAINT [FK_SupplierSupplierCategory]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupplierCategory'
CREATE INDEX [IX_FK_SupplierSupplierCategory]
ON [dbo].[SupplierCategorySet]
    ([Supplier_Id]);
GO

-- Creating foreign key on [Category_Id] in table 'ShopProductSet'
ALTER TABLE [dbo].[ShopProductSet]
ADD CONSTRAINT [FK_CategoryProduct]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[ShopCategorySet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProduct'
CREATE INDEX [IX_FK_CategoryProduct]
ON [dbo].[ShopProductSet]
    ([Category_Id]);
GO

-- Creating foreign key on [ShopProduct_Id] in table 'SupplierProductSet'
ALTER TABLE [dbo].[SupplierProductSet]
ADD CONSTRAINT [FK_SupplierProductProduct]
    FOREIGN KEY ([ShopProduct_Id])
    REFERENCES [dbo].[ShopProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierProductProduct'
CREATE INDEX [IX_FK_SupplierProductProduct]
ON [dbo].[SupplierProductSet]
    ([ShopProduct_Id]);
GO

-- Creating foreign key on [SupplierProduct_Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_SupplierProductSupplierImage]
    FOREIGN KEY ([SupplierProduct_Id])
    REFERENCES [dbo].[SupplierProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierProductSupplierImage'
CREATE INDEX [IX_FK_SupplierProductSupplierImage]
ON [dbo].[ImageSet]
    ([SupplierProduct_Id]);
GO

-- Creating foreign key on [SupplierCategory_Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_SupplierCategorySupplierImage]
    FOREIGN KEY ([SupplierCategory_Id])
    REFERENCES [dbo].[SupplierCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierCategorySupplierImage'
CREATE INDEX [IX_FK_SupplierCategorySupplierImage]
ON [dbo].[ImageSet]
    ([SupplierCategory_Id]);
GO

-- Creating foreign key on [ShopCategory_Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_ImageShopCategory]
    FOREIGN KEY ([ShopCategory_Id])
    REFERENCES [dbo].[ShopCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageShopCategory'
CREATE INDEX [IX_FK_ImageShopCategory]
ON [dbo].[ImageSet]
    ([ShopCategory_Id]);
GO

-- Creating foreign key on [ShopProduct_Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_ImageShopProduct]
    FOREIGN KEY ([ShopProduct_Id])
    REFERENCES [dbo].[ShopProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageShopProduct'
CREATE INDEX [IX_FK_ImageShopProduct]
ON [dbo].[ImageSet]
    ([ShopProduct_Id]);
GO

-- Creating foreign key on [Parent_Id] in table 'SupplierCategorySet'
ALTER TABLE [dbo].[SupplierCategorySet]
ADD CONSTRAINT [FK_SupplierCategorySupplierCategory]
    FOREIGN KEY ([Parent_Id])
    REFERENCES [dbo].[SupplierCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierCategorySupplierCategory'
CREATE INDEX [IX_FK_SupplierCategorySupplierCategory]
ON [dbo].[SupplierCategorySet]
    ([Parent_Id]);
GO

-- Creating foreign key on [Category_Id] in table 'SupplierProductSet'
ALTER TABLE [dbo].[SupplierProductSet]
ADD CONSTRAINT [FK_SupplierCategorySupplierProduct]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[SupplierCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierCategorySupplierProduct'
CREATE INDEX [IX_FK_SupplierCategorySupplierProduct]
ON [dbo].[SupplierProductSet]
    ([Category_Id]);
GO

-- Creating foreign key on [SupplierProductAsDefault_Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_SupplierProductImage]
    FOREIGN KEY ([SupplierProductAsDefault_Id])
    REFERENCES [dbo].[SupplierProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierProductImage'
CREATE INDEX [IX_FK_SupplierProductImage]
ON [dbo].[ImageSet]
    ([SupplierProductAsDefault_Id]);
GO

-- Creating foreign key on [Parent_Id] in table 'ShopCategorySet'
ALTER TABLE [dbo].[ShopCategorySet]
ADD CONSTRAINT [FK_ShopCategoryShopCategory]
    FOREIGN KEY ([Parent_Id])
    REFERENCES [dbo].[ShopCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopCategoryShopCategory'
CREATE INDEX [IX_FK_ShopCategoryShopCategory]
ON [dbo].[ShopCategorySet]
    ([Parent_Id]);
GO

-- Creating foreign key on [ShopCategory_Id] in table 'SupplierCategorySet'
ALTER TABLE [dbo].[SupplierCategorySet]
ADD CONSTRAINT [FK_ShopCategorySupplierCategory]
    FOREIGN KEY ([ShopCategory_Id])
    REFERENCES [dbo].[ShopCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopCategorySupplierCategory'
CREATE INDEX [IX_FK_ShopCategorySupplierCategory]
ON [dbo].[SupplierCategorySet]
    ([ShopCategory_Id]);
GO

-- Creating foreign key on [DefaultImage_Id] in table 'ShopProductSet'
ALTER TABLE [dbo].[ShopProductSet]
ADD CONSTRAINT [FK_ImageShopProduct1]
    FOREIGN KEY ([DefaultImage_Id])
    REFERENCES [dbo].[ImageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageShopProduct1'
CREATE INDEX [IX_FK_ImageShopProduct1]
ON [dbo].[ShopProductSet]
    ([DefaultImage_Id]);
GO

-- Creating foreign key on [DefaultImage_Id] in table 'ShopCategorySet'
ALTER TABLE [dbo].[ShopCategorySet]
ADD CONSTRAINT [FK_ImageShopCategory1]
    FOREIGN KEY ([DefaultImage_Id])
    REFERENCES [dbo].[ImageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageShopCategory1'
CREATE INDEX [IX_FK_ImageShopCategory1]
ON [dbo].[ShopCategorySet]
    ([DefaultImage_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------