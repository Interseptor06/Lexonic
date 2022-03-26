IF (EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'NewsDataTable'))
    begin
        DROP TABLE [dbo].[NewsDataTable]
    end

CREATE TABLE [dbo].[NewsDataTable](
                                         [Ticker] varchar(10),
                                         [Title] varchar(500),
                                         [ArticleURL] varchar(500),
                                         [Date] date,
                                         [Time] varchar(50),
                                         [Sentiment] float,
                                         [id] int NOT NULL IDENTITY(1,1)
);
