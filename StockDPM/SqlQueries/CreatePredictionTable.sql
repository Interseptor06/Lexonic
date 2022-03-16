IF (EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'StockPredictionTable'))
    begin
        DROP TABLE dbo.StockPredictionTable
    end
CREATE TABLE dbo.StockPredictionTable(
        [Ticker] varchar(10),
        [DateAdded] date,
        [Prediction] float
);
