IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'StockPredictionTable'))
    begin
CREATE TABLE dbo.StockPredictionTable(
        [Ticker] varchar(10),
        [DateAdded] date,
        [Prediction] money
);
end

ELSE
    begin
DROP TABLE dbo.StockPredictionTable
CREATE TABLE dbo.StockPredictionTable(
                                         [Ticker] varchar(10),
                                         [DateAdded] date,
                                         [Prediction] money
);
end