USE [AVS-Poloniex]

--OrderBook table
create nonclustered index IX_OrderBook_Pair on [Market].[OrderBook] ([PAIR]) include (TimeStampUtc)