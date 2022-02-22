INSERT INTO [dbo].[NewsDataTable] (           [Ticker],
                                              [Title],
                                              [ArticleURL],
                                              [Date],
                                              [Time])
VALUES (@_Ticker,
        @_Title,
        @_ArticleUrl,
        @_Date,
        @_Time);