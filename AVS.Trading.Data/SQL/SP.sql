SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AVE
-- Create date: 04.02.2018
-- Description:	Shrinks orderbook buy and sell order records  
-- =============================================
CREATE PROCEDURE ShrinkOpenOrders	
	@date  datetime,
	@currencyPair nvarchar (10)
AS
BEGIN

--take first order book of the day and assign to it all the buy and sell orders
declare @orderbookId int 
select top 1 @orderbookId = id from [Market].[OrderBook]
where TimeStampUtc >= @date-1 and TimeStampUtc < @date
and Market = @currencyPair
select @orderbookId

update [Market].buyorder 
set orderbookid = @orderbookId where id in (select id from [Market].OrderBook 
where TimeStampUtc >= @date-1 and TimeStampUtc < @date and Market = @currencyPair) 

update [Market].sellorder 
set orderbookid = @orderbookId where id in (select id from [Market].OrderBook 
where TimeStampUtc >= @date-1 and TimeStampUtc < @date and Market = @currencyPair) 

;WITH CTE AS (
  SELECT Price, AmountBase, AmountQuote, 
     row_number() OVER(PARTITION BY Price, AmountBase, AmountQuote ORDER BY Id) AS [rn]
  FROM [Market].BuyOrder
  where orderbookid = @orderbookId 
)
DELETE CTE WHERE [rn] > 1

;WITH CTE AS (
  SELECT Price, AmountBase, AmountQuote, 
     row_number() OVER(PARTITION BY Price, AmountBase, AmountQuote ORDER BY Id) AS [rn]
  FROM [Market].SellOrder
  where orderbookid = @orderbookId
)
DELETE CTE WHERE [rn] > 1

DELETE FROM [Market].OrderBook 
WHERE (TimeStampUtc >= @date-1 and TimeStampUtc < @date) 
		and id <> @orderbookId 
		and Market = @currencyPair

END