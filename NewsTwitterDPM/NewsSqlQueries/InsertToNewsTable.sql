INSERT INTO [dbo].[NewsDataTable] (           [Ticker],
                                              [Title],
                                              [ArticleURL],
                                              [Date],
                                              [Time],
                                            [Sentiment])
VALUES (@_Ticker,
        @_Title,
        @_ArticleUrl,
        @_Date,
        @_Time,
        @_Sentiment);