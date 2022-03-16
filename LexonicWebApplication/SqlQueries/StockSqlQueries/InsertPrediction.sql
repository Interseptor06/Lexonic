INSERT INTO [dbo].[StockPredictiontable] ([Ticker],
                                                  [DateAdded],
                                                  [Prediction])
VALUES (@_Ticker,
            @_DateAdded,
            @_Prediction);
    