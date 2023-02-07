USE [AVS-Poloniex]

DROP TABLE [CoreLib].[QueuedEmail]
GO
DROP TABLE [CoreLib].[EmailAccount]
DROP TABLE [CoreLib].[ScheduleTask]
DROP TABLE [CoreLib].[Log]

---===== Markets data tables ======-------
GO
DROP TABLE [Market].[BuyOrder]
DROP TABLE [Market].[SellOrder]
DROP TABLE [Market].[OrderBook]
DROP TABLE [Market].[MarketTradeItem]
--DROP TABLE [dbo].[Market_MarketTradeHistory]
DROP TABLE [Market].[MarketData]
DROP TABLE [Market].[ChartDataItem]
DROP TABLE [Market].[ChartData]
GO
---=========== Trading API tables  ====----
DROP TABLE [Trading].[MyTradeItem]
DROP TABLE [Trading].[MyOrder]
GO
--SELECT count(1)   FROM [AVS-Poloniex].[dbo].[OrderBook]
--SELECT count(1)   FROM [AVS-Poloniex].[dbo].[SellOrder]
--SELECT count(1)   FROM [AVS-Poloniex].[dbo].[BuyOrder]
--SELECT count(1)   FROM [AVS-Poloniex].[dbo].[MarketData]

--INSERT INTO [dbo].[__CoreLib_ScheduleTask]
--           ([Name],[Seconds] ,[Type]   ,[Enabled] ,[StopOnError] ,[ApplicationInstanceId]  ,[LastStartUtc]  ,[LastEndUtc],[LastSuccessUtc])
--     VALUES
--           ('Shrink order books'           ,60
--           ,'AVS.Trading.DataFiller.Tasks.ShrinkOpenOrdersTask, AVS.Trading.DataFiller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
--           ,1           ,0           ,'Poloniex-DataFiller'
--           ,NULL           ,NULL           ,NULL)
GO