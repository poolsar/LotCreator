
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/11/2014 13:40:59
-- Generated from EDMX file: C:\Users\victor\Downloads\___БИЗНЕС\Игрушки\Автоматизация\PrestaSharp-master\ToyShopDataLib\ToyShopModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ToyShop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO
-- Creating table 'BegemotCountHistorySet'
alter TABLE [dbo].[BegemotCountHistorySet] 
add    [BegemotProduct_Id] int  NULL

GO

-- Creating foreign key on [BegemotProduct_Id] in table 'BegemotCountHistorySet'
ALTER TABLE [dbo].[BegemotCountHistorySet]
ADD CONSTRAINT [FK_BegemotProductBegemotCountHistory]
    FOREIGN KEY ([BegemotProduct_Id])
    REFERENCES [dbo].[BegemotProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BegemotProductBegemotCountHistory'
CREATE INDEX [IX_FK_BegemotProductBegemotCountHistory]
ON [dbo].[BegemotCountHistorySet]
    ([BegemotProduct_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------