

IF EXISTS (SELECT * FROM [dbo].[EarningsDataTable] WHERE Ticker = @_Ticker)
    BEGIN
        UPDATE [dbo].[EarningsDataTable]
        SET [fiscalDateEnding] = @_fiscalDateEnding,
            [ReportedEPS] = @_ReportedEPS

        WHERE [Ticker] = @_Ticker
    END
ELSE
    BEGIN
        Insert into [dbo].[EarningsDataTable](  [Ticker],
                                            [fiscalDateEnding],
                                            [ReportedEPS])
        Values(@_Ticker,
               @_fiscalDateEnding,
               @_ReportedEPS);
    END