IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'NewsDataTable'))
    begin
        CREATE TABLE [dbo].[NewsDataTable](
                                                 [Ticker] varchar(10),
                                                 [Title] varchar(500),
                                                 [ArticleURL] varchar(250),
                                                 [Date] date,
                                                 [Time] varchar(50),
                                                 [id] int NOT NULL IDENTITY(1,1)
        );
    end

ELSE
    begin
        DROP TABLE [dbo].[NewsDataTable]
        CREATE TABLE [dbo].[NewsDataTable](
                                              [Ticker] varchar(10),
                                              [Title] varchar(500),
                                              [ArticleURL] varchar(250),
                                              [Date] date,
                                              [Time] varchar(50),
                                              [id] int NOT NULL IDENTITY(1,1)
        );
    end
