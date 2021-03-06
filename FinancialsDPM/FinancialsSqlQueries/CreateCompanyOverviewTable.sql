IF (EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE  TABLE_NAME = 'CompanyOverviewTable'))
    begin
        DROP TABLE [dbo].[CompanyOverviewTable]
    end



CREATE TABLE [dbo].[CompanyOverviewTable](
                                                 [Ticker] varchar(10),
                                                 [AssetType] varchar(200),
                                                 [Name] varchar(200),
                                                 [Description] varchar(2000),
                                                 [CIK] varchar(200),
                                                 [Exchange] varchar(200),
                                                 [Currency] varchar(200),
                                                 [Country] varchar(200),
                                                 [Sector] varchar(200),
                                                 [Industry] varchar(200),
                                                 [Address] varchar(200),
                                                 [FiscalYearEnd] varchar(200),
                                                 [LatestQuarter] varchar(200),
                                                 [MarketCapitalization] varchar(200),
                                                 [EBITDA] varchar(200),
                                                 [PERatio] varchar(200),
                                                 [PEGRatio] varchar(200),
                                                 [BookValue] varchar(200),
                                                 [DividendPerShare] varchar(200),
                                                 [DividendYield] varchar(200),
                                                 [EPS] varchar(200),
                                                 [RevenuePerShareTTM] varchar(200),
                                                 [ProfitMargin] varchar(200),
                                                 [OperatingMarginTTM] varchar(200),
                                                 [ReturnOnAssetsTTM] varchar(200),
                                                 [ReturnOnEquityTTM] varchar(200),
                                                 [RevenueTTM] varchar(200),
                                                 [GrossProfitTTM] varchar(200),
                                                 [DilutedEPSTTM] varchar(200),
                                                 [QuarterlyEarningsGrowthYOY] varchar(200),
                                                 [QuarterlyRevenueGrowthYOY] varchar(200),
                                                 [AnalystTargetPrice] varchar(200),
                                                 [TrailingPE] varchar(200),
                                                 [ForwardPE] varchar(200),
                                                 [PriceToSalesRatioTTM] varchar(200),
                                                 [PriceToBookRatio] varchar(200),
                                                 [EVToRevenue] varchar(200),
                                                 [EVToEBITDA] varchar(200),
                                                 [Beta] varchar(200),
                                                 [FiftyTwoWeekHigh] varchar(200),
                                                 [FiftyTwoWeekLow] varchar(200),
                                                 [FiftyDayMovingAverage] varchar(200),
                                                 [TwoHundredDayMovingAverage] varchar(200),
                                                 [SharesOutstanding] varchar(200),
                                                 [DividendDate] varchar(200),
                                                 [ExDividendDate] varchar(200)
    );
