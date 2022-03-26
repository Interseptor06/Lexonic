IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'NewsDataTable'))
    begin
        CREATE TABLE [dbo].[NewsDataTable](
                                              [Sentiment] decimal,
                                              [id] int NOT NULL IDENTITY(1,1)
        );
    end

ELSE
    begin
        DROP TABLE [dbo].[NewsDataTable]
        CREATE TABLE [dbo].[NewsDataTable](
                                              [Sentiment] decimal,
                                              [id] int NOT NULL IDENTITY(1,1)
        );
    end
