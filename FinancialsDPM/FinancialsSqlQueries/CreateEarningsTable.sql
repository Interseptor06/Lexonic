IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE  TABLE_NAME = 'EarningsDataTable'))
    begin
CREATE TABLE [dbo].[EarningsDataTable](
                                          [Ticker] varchar(10),
                                          [FiscalDateEnding] varchar(100),
                                          [ReportedEPS] varchar(100)
);
END 
    
ELSE
    begin
    DROP TABLE [dbo].[EarningsDataTable]
CREATE TABLE [dbo].[EarningsDataTable](
                                          [Ticker] varchar(10),
                                          [FiscalDateEnding] varchar(100),
                                          [ReportedEPS] varchar(100)
);
    end