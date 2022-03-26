IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE  TABLE_NAME = 'IncomeStatementDataTable'))
    begin
CREATE TABLE [dbo].[IncomeStatementDataTable](
                                                 [Ticker] varchar(10),
                                                 [fiscalDateEnding] varchar(200),
                                                 [reportedCurrency] varchar(200),
                                                 [grossProfit] varchar(200),
                                                 [totalRevenue] varchar(200),
                                                 [costOfRevenue] varchar(200),
                                                 [costofGoodsAndServicesSold] varchar(200),
                                                 [operatingIncome] varchar(200),
                                                 [sellingGeneralAndAdministrative] varchar(200),
                                                 [researchAndDevelopment] varchar(200),
                                                 [operatingExpenses] varchar(200),
                                                 [investmentIncomeNet] varchar(200),
                                                 [netInterestIncome] varchar(200),
                                                 [interestIncome] varchar(200),
                                                 [interestExpense] varchar(200),
                                                 [nonInterestIncome] varchar(200),
                                                 [otherNonOperatingIncome] varchar(200),
                                                 [depreciation] varchar(200),
                                                 [depreciationAndAmortization] varchar(200),
                                                 [incomerBeforeTax] varchar(200),
                                                 [incomeTaxExpense] varchar(200),
                                                 [interestAndDebtExpense] varchar(200),
                                                 [netIncomeFromContinuingOperations] varchar(200),
                                                 [comprehensiveIncomeNetOfTax] varchar(200),
                                                 [ebit] varchar(200),
                                                 [ebitda] varchar(200),
                                                 [netIncome] varchar(200)

);
end

ELSE
    begin
    DROP TABLE [dbo].[IncomeStatementDataTable]
CREATE TABLE [dbo].[IncomeStatementDataTable](
                                                 [Ticker] varchar(10),
                                                 [fiscalDateEnding] varchar(200),
                                                 [reportedCurrency] varchar(200),
                                                 [grossProfit] varchar(200),
                                                 [totalRevenue] varchar(200),
                                                 [costOfRevenue] varchar(200),
                                                 [costofGoodsAndServicesSold] varchar(200),
                                                 [operatingIncome] varchar(200),
                                                 [sellingGeneralAndAdministrative] varchar(200),
                                                 [researchAndDevelopment] varchar(200),
                                                 [operatingExpenses] varchar(200),
                                                 [investmentIncomeNet] varchar(200),
                                                 [netInterestIncome] varchar(200),
                                                 [interestIncome] varchar(200),
                                                 [interestExpense] varchar(200),
                                                 [nonInterestIncome] varchar(200),
                                                 [otherNonOperatingIncome] varchar(200),
                                                 [depreciation] varchar(200),
                                                 [depreciationAndAmortization] varchar(200),
                                                 [incomerBeforeTax] varchar(200),
                                                 [incomeTaxExpense] varchar(200),
                                                 [interestAndDebtExpense] varchar(200),
                                                 [netIncomeFromContinuingOperations] varchar(200),
                                                 [comprehensiveIncomeNetOfTax] varchar(200),
                                                 [ebit] varchar(200),
                                                 [ebitda] varchar(200),
                                                 [netIncome] varchar(200)
);
    end