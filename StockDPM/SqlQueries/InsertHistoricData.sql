IF (EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE  TABLE_NAME = 'HistoricalStockDataTable'))
INSERT INTO [dbo].[HistoricalStockDataTable] ([Ticker],
                                              [DateAdded],
                                              [Open],
                                              [Close],
                                              [High],
                                              [Low],
                                              [Volume],
                                              [NumOfTransacts])
VALUES (@_Ticker,
        @_DateAdded,
        @_Open,
        @_Close,
        @_High,
        @_Low,
        @_Volume,
        @_NumOfTransacts);