IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE  TABLE_NAME = 'HistoricalStockDataTable'))
CREATE TABLE [dbo].[HistoricalStockDataTable](
    [Ticker] varchar(10),
    [DateAdded] date,
    [Open] money,
    [Close] money,
    [High] money,
    [Low] money,
    [Volume] bigint,
    [NumOfTransacts] bigint
);

ELSE
DROP TABLE [dbo].[HistoricalStockDataTable]
CREATE TABLE [dbo].[HistoricalStockDataTable](
    [Ticker] varchar(10),
    [DateAdded] date,
    [Open] money,
    [Close] money,
    [High] money,
    [Low] money,
    [Volume] bigint,
    [NumOfTransacts] bigint
);