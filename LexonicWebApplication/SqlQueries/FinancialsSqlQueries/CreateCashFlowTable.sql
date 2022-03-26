IF (NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE  TABLE_NAME = 'CashFlowTable'))
    begin
CREATE TABLE [dbo].[CashFlowTable](
                                    [Ticker] varchar(10),
                                    [fiscalDateEnding] varchar(200),
                                    [reportedCurrency] varchar(200),
                                    [operatingCashflow] varchar(200),
                                    [paymentsForOperatingActivities] varchar(200),
                                    [proceedsFromOperatingActivities] varchar(200),
                                    [changeInOperatingLiabilities] varchar(200),
                                    [changeInOperatingAssets] varchar(200),
                                    [depreciationDepletionAndAmortization] varchar(200),
                                    [capitalExpenditures] varchar(200),
                                    [changeInReceivables] varchar(200),
                                    [changeInInventory] varchar(200),
                                    [profitLoss] varchar(200),
                                    [cashflowFromInvestment] varchar(200),
                                    [cashflowFromFinancing] varchar(200),
                                    [proceedsFromRepaymentsOfShortTermDebt] varchar(200),
                                    [paymentsForRepurchaseOfCommonStock] varchar(200),
                                    [paymentsForRepurchaseOfEquity] varchar(200),
                                    [paymentsForRepurchaseOfPreferredStock] varchar(200),
                                    [dividendPayout] varchar(200),
                                    [dividendPayoutCommonStock] varchar(200),
                                    [dividendPayoutPreferredStock] varchar(200),
                                    [proceedsFromIssuanceOfCommonStock] varchar(200),
                                    [proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet] varchar(200),
                                    [proceedsFromIssuanceOfPreferredStock] varchar(200),
                                    [proceedsFromRepurchaseOfEquity] varchar(200),
                                    [proceedsFromSaleOfTreasuryStock] varchar(200),
                                    [changeInCashAndCashEquivalents] varchar(200),
                                    [changeInExchangeRate] varchar(200),
                                    [netIncome] varchar(200)
);
end

ELSE
    begin
    DROP TABLE [dbo].[CashFlowTable]
    CREATE TABLE [dbo].[CashFlowTable](
                                        [Ticker] varchar(10),
                                        [fiscalDateEnding] varchar(200),
                                        [reportedCurrency] varchar(200),
                                        [operatingCashflow] varchar(200),
                                        [paymentsForOperatingActivities] varchar(200),
                                        [proceedsFromOperatingActivities] varchar(200),
                                        [changeInOperatingLiabilities] varchar(200),
                                        [changeInOperatingAssets] varchar(200),
                                        [depreciationDepletionAndAmortization] varchar(200),
                                        [capitalExpenditures] varchar(200),
                                        [changeInReceivables] varchar(200),
                                        [changeInInventory] varchar(200),
                                        [profitLoss] varchar(200),
                                        [cashflowFromInvestment] varchar(200),
                                        [cashflowFromFinancing] varchar(200),
                                        [proceedsFromRepaymentsOfShortTermDebt] varchar(200),
                                        [paymentsForRepurchaseOfCommonStock] varchar(200),
                                        [paymentsForRepurchaseOfEquity] varchar(200),
                                        [paymentsForRepurchaseOfPreferredStock] varchar(200),
                                        [dividendPayout] varchar(200),
                                        [dividendPayoutCommonStock] varchar(200),
                                        [dividendPayoutPreferredStock] varchar(200),
                                        [proceedsFromIssuanceOfCommonStock] varchar(200),
                                        [proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet] varchar(200),
                                        [proceedsFromIssuanceOfPreferredStock] varchar(200),
                                        [proceedsFromRepurchaseOfEquity] varchar(200),
                                        [proceedsFromSaleOfTreasuryStock] varchar(200),
                                        [changeInCashAndCashEquivalents] varchar(200),
                                        [changeInExchangeRate] varchar(200),
                                        [netIncome] varchar(200)
);
    end