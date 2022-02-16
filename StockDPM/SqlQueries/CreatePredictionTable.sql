IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'StockPredictionTable'))
CREATE TABLE dbo.StockPredictionTable(
        [Ticker] varchar(10),
        [DateAdded] date,
        [Prediction] money
);

ELSE
DROP TABLE dbo.StockPredictionTable
CREATE TABLE dbo.StockPredictionTable(
                                         [Ticker] varchar(10),
                                         [DateAdded] date,
                                         [Prediction] money
);