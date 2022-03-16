using System;
using System.IO;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.Data.SqlClient;


namespace FinancialsDPM
{

    public class FinancialsTableOps
    {
        /// <summary>
        /// Table Initialization
        /// Possible repeat of method in GetData
        /// </summary>
        public static void CreateTables()
        {
            CreateEarningsTable();
            CreateBalanceSheetTable();
            CreateCashFlowTable();
            CreateCompanyOverviewTable();
            CreateIncomeStatementTable();
        }
        /// <summary>
        /// Next Five Methods are for table creation and are relatively straightforward
        /// They take the sql query from the dedicated file and execute using built in .NET SQL libraries 
        /// </summary>
        public static void CreateBalanceSheetTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "CreateBalanceSheetTable.sql");
            
            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void CreateCashFlowTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "CreateCashFlowTable.sql");
            
            string script =
                File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void CreateCompanyOverviewTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries" ,@"FinancialsSqlQueries", "CreateCompanyOverviewTable.sql");

            
            string script =
                File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void CreateEarningsTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "CreateEarningsTable.sql");

            string script =
                File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void CreateIncomeStatementTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "CreateIncomeStatementTable.sql");

            string script =
                File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    
    
    public class FinancialsUpdateTables
    {
        /// <summary>
        /// Next Five Methods are for table updating, since we won't be keeping historic data, and are relatively straightforward
        /// They take the sql query from the dedicated file and execute using built in .NET SQL libraries 
        /// </summary>
        public static void UpdateBalanceSheetTable(BalanceSheet bData)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "UpdateBalanceSheetTable.sql");

            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", bData.Ticker);
            command.Parameters.AddWithValue("@_fiscalDateEnding", bData.fiscalDateEnding);
            command.Parameters.AddWithValue("@_reportedCurrency", bData.reportedCurrency);
            command.Parameters.AddWithValue("@_totalAssets", bData.totalAssets);
            command.Parameters.AddWithValue("@_totalCurrentAssets", bData.totalCurrentAssets);
            command.Parameters.AddWithValue("@_cashAndCashEquivalentsAtCarryingValue", bData.cashAndCashEquivalentsAtCarryingValue);
            command.Parameters.AddWithValue("@_cashAndShortTermInvestments", bData.cashAndShortTermInvestments);
            command.Parameters.AddWithValue("@_inventory", bData.inventory);
            command.Parameters.AddWithValue("@_currentNetReceivables", bData.currentNetReceivables);
            command.Parameters.AddWithValue("@_totalNonCurrentAssets", bData.totalNonCurrentAssets);
            command.Parameters.AddWithValue("@_propertyPlantEquipment", bData.propertyPlantEquipment);
            command.Parameters.AddWithValue("@_accumulatedDepreciationAmortizationPPE", bData.accumulatedDepreciationAmortizationPPE);
            command.Parameters.AddWithValue("@_intangibleAssets", bData.intangibleAssets);
            command.Parameters.AddWithValue("@_intangibleAssetsExcludingGoodwill", bData.intangibleAssetsExcludingGoodwill);
            command.Parameters.AddWithValue("@_goodwill", bData.goodwill);
            command.Parameters.AddWithValue("@_investments", bData.investments);
            command.Parameters.AddWithValue("@_longTermInvestments", bData.longTermInvestments);
            command.Parameters.AddWithValue("@_shortTermInvestments", bData.shortTermInvestments);
            command.Parameters.AddWithValue("@_otherCurrentAssets", bData.otherCurrentAssets);
            command.Parameters.AddWithValue("@_otherNonCurrrentAssets", bData.otherNonCurrrentAssets);
            command.Parameters.AddWithValue("@_totalLiabilities", bData.totalLiabilities);
            command.Parameters.AddWithValue("@_totalCurrentLiabilities", bData.totalCurrentLiabilities);
            command.Parameters.AddWithValue("@_currentAccountsPayable", bData.currentAccountsPayable);
            command.Parameters.AddWithValue("@_deferredRevenue", bData.deferredRevenue);
            command.Parameters.AddWithValue("@_currentDebt", bData.currentDebt);
            command.Parameters.AddWithValue("@_shortTermDebt", bData.shortTermDebt);
            command.Parameters.AddWithValue("@_totalNonCurrentLiabilities", bData.totalNonCurrentLiabilities);
            command.Parameters.AddWithValue("@_capitalLeaseObligations", bData.capitalLeaseObligations);
            command.Parameters.AddWithValue("@_longTermDebt", bData.longTermDebt);
            command.Parameters.AddWithValue("@_currentLongTermDebt", bData.currentLongTermDebt);
            command.Parameters.AddWithValue("@_longTermDebtNoncurrent", bData.longTermDebtNoncurrent);
            command.Parameters.AddWithValue("@_shortLongTermDebtTotal", bData.shortLongTermDebtTotal);
            command.Parameters.AddWithValue("@_otherCurrentLiabilities", bData.otherCurrentLiabilities);
            command.Parameters.AddWithValue("@_otherNonCurrentLiabilities", bData.otherNonCurrentLiabilities);
            command.Parameters.AddWithValue("@_totalShareholderEquity", bData.totalShareholderEquity);
            command.Parameters.AddWithValue("@_treasuryStock", bData.treasuryStock);
            command.Parameters.AddWithValue("@_retainedEarnings", bData.retainedEarnings);
            command.Parameters.AddWithValue("@_commonStock", bData.commonStock);
            command.Parameters.AddWithValue("@_commonStockSharesOutstanding", bData.commonStockSharesOutstanding);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void UpdateCashFlowTable(CashFlow eData)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "UpdateCashFlowTable.sql");

            string script =
                File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", eData.Ticker);
            command.Parameters.AddWithValue("@_fiscalDateEnding ", eData.fiscalDateEnding);
            command.Parameters.AddWithValue("@_reportedCurrency ", eData.reportedCurrency);
            command.Parameters.AddWithValue("@_operatingCashflow ", eData.operatingCashflow);
            command.Parameters.AddWithValue("@_paymentsForOperatingActivities ", eData.paymentsForOperatingActivities);
            command.Parameters.AddWithValue("@_proceedsFromOperatingActivities ", eData.proceedsFromOperatingActivities);
            command.Parameters.AddWithValue("@_changeInOperatingLiabilities ", eData.changeInOperatingLiabilities);
            command.Parameters.AddWithValue("@_changeInOperatingAssets ", eData.changeInOperatingAssets);
            command.Parameters.AddWithValue("@_depreciationDepletionAndAmortization ", eData.depreciationDepletionAndAmortization);
            command.Parameters.AddWithValue("@_capitalExpenditures ", eData.capitalExpenditures);
            command.Parameters.AddWithValue("@_changeInReceivables ", eData.changeInReceivables);
            command.Parameters.AddWithValue("@_changeInInventory ", eData.changeInInventory);
            command.Parameters.AddWithValue("@_profitLoss ", eData.profitLoss);
            command.Parameters.AddWithValue("@_cashflowFromInvestment ", eData.cashflowFromInvestment);
            command.Parameters.AddWithValue("@_cashflowFromFinancing ", eData.cashflowFromFinancing);
            command.Parameters.AddWithValue("@_proceedsFromRepaymentsOfShortTermDebt ", eData.proceedsFromRepaymentsOfShortTermDebt);
            command.Parameters.AddWithValue("@_paymentsForRepurchaseOfCommonStock ", eData.paymentsForRepurchaseOfCommonStock);
            command.Parameters.AddWithValue("@_paymentsForRepurchaseOfEquity ", eData.paymentsForRepurchaseOfEquity);
            command.Parameters.AddWithValue("@_paymentsForRepurchaseOfPreferredStock ", eData.paymentsForRepurchaseOfPreferredStock);
            command.Parameters.AddWithValue("@_dividendPayout ", eData.dividendPayout);
            command.Parameters.AddWithValue("@_dividendPayoutCommonStock ", eData.dividendPayoutCommonStock);
            command.Parameters.AddWithValue("@_dividendPayoutPreferredStock ", eData.dividendPayoutPreferredStock);
            command.Parameters.AddWithValue("@_proceedsFromIssuanceOfCommonStock ", eData.proceedsFromIssuanceOfCommonStock);
            command.Parameters.AddWithValue("@_proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet ", eData.proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet);
            command.Parameters.AddWithValue("@_proceedsFromIssuanceOfPreferredStock ", eData.proceedsFromIssuanceOfPreferredStock);
            command.Parameters.AddWithValue("@_proceedsFromRepurchaseOfEquity ", eData.proceedsFromRepurchaseOfEquity);
            command.Parameters.AddWithValue("@_proceedsFromSaleOfTreasuryStock ", eData.proceedsFromSaleOfTreasuryStock);
            command.Parameters.AddWithValue("@_changeInCashAndCashEquivalents ", eData.changeInCashAndCashEquivalents);
            command.Parameters.AddWithValue("@_changeInExchangeRate ", eData.changeInExchangeRate);
            command.Parameters.AddWithValue("@_netIncome ", eData.netIncome);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void UpdateCompanyOverviewTableTable(CompanyOverview coData)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries" ,@"FinancialsSqlQueries", "UpdateCompanyOverviewTable.sql");

            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", coData.Ticker);
            command.Parameters.AddWithValue("@_AssetType", coData.AssetType);
            command.Parameters.AddWithValue("@_Name", coData.Name);
            command.Parameters.AddWithValue("@_Description", coData.Description);
            command.Parameters.AddWithValue("@_CIK", coData.CIK);
            command.Parameters.AddWithValue("@_Exchange", coData.Exchange);
            command.Parameters.AddWithValue("@_Currency", coData.Currency);
            command.Parameters.AddWithValue("@_Country", coData.Country);
            command.Parameters.AddWithValue("@_Sector", coData.Sector);
            command.Parameters.AddWithValue("@_Industry", coData.Industry);
            command.Parameters.AddWithValue("@_Address", coData.Address);
            command.Parameters.AddWithValue("@_FiscalYearEnd", coData.FiscalYearEnd);
            command.Parameters.AddWithValue("@_LatestQuarter", coData.LatestQuarter);
            command.Parameters.AddWithValue("@_MarketCapitalization", coData.MarketCapitalization);
            command.Parameters.AddWithValue("@_EBITDA", coData.EBITDA);
            command.Parameters.AddWithValue("@_PERatio", coData.PERatio);
            command.Parameters.AddWithValue("@_PEGRatio", coData.PEGRatio);
            command.Parameters.AddWithValue("@_BookValue", coData.BookValue);
            command.Parameters.AddWithValue("@_DividendPerShare", coData.DividendPerShare);
            command.Parameters.AddWithValue("@_DividendYield", coData.DividendYield);
            command.Parameters.AddWithValue("@_EPS", coData.EPS);
            command.Parameters.AddWithValue("@_RevenuePerShareTTM", coData.RevenuePerShareTTM);
            command.Parameters.AddWithValue("@_ProfitMargin", coData.ProfitMargin);
            command.Parameters.AddWithValue("@_OperatingMarginTTM", coData.OperatingMarginTTM);
            command.Parameters.AddWithValue("@_ReturnOnAssetsTTM", coData.ReturnOnAssetsTTM);
            command.Parameters.AddWithValue("@_ReturnOnEquityTTM", coData.ReturnOnEquityTTM);
            command.Parameters.AddWithValue("@_RevenueTTM", coData.RevenueTTM);
            command.Parameters.AddWithValue("@_GrossProfitTTM", coData.GrossProfitTTM);
            command.Parameters.AddWithValue("@_DilutedEPSTTM", coData.DilutedEPSTTM);
            command.Parameters.AddWithValue("@_QuarterlyEarningsGrowthYOY", coData.QuarterlyEarningsGrowthYOY);
            command.Parameters.AddWithValue("@_QuarterlyRevenueGrowthYOY", coData.QuarterlyRevenueGrowthYOY);
            command.Parameters.AddWithValue("@_AnalystTargetPrice", coData.AnalystTargetPrice);
            command.Parameters.AddWithValue("@_TrailingPE", coData.TrailingPE);
            command.Parameters.AddWithValue("@_ForwardPE", coData.ForwardPE);
            command.Parameters.AddWithValue("@_PriceToSalesRatioTTM", coData.PriceToSalesRatioTTM);
            command.Parameters.AddWithValue("@_PriceToBookRatio", coData.PriceToBookRatio);
            command.Parameters.AddWithValue("@_EVToRevenue", coData.EVToRevenue);
            command.Parameters.AddWithValue("@_EVToEBITDA", coData.EVToEBITDA);
            command.Parameters.AddWithValue("@_Beta", coData.Beta);
            command.Parameters.AddWithValue("@_FiftyTwoWeekHigh", coData.FiftyTwoWeekHigh);
            command.Parameters.AddWithValue("@_FiftyTwoWeekLow", coData.FiftyTwoWeekLow);
            command.Parameters.AddWithValue("@_FiftyDayMovingAverage", coData.FiftyDayMovingAverage);
            command.Parameters.AddWithValue("@_TwoHundredDayMovingAverage", coData.TwoHundredDayMovingAverage);
            command.Parameters.AddWithValue("@_SharesOutstanding", coData.SharesOutstanding);
            command.Parameters.AddWithValue("@_DividendDate", coData.DividendDate);
            command.Parameters.AddWithValue("@_ExDividendDate", coData.ExDividendDate);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void UpdateEarningsTable(Earnings eData)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "UpdateEarningsTable.sql");

            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", eData.Ticker);
            command.Parameters.AddWithValue("@_fiscalDateEnding", eData.FiscalDateEnding);
            command.Parameters.AddWithValue("@_ReportedEPS", eData.ReportedEPS);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void UpdateIncomeStatementTable(IncomeStatement isData)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"FinancialsSqlQueries", "UpdateIncomeStatementTable.sql");

            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);

            command.Parameters.AddWithValue("@_Ticker", isData.Ticker);
            command.Parameters.AddWithValue("@_fiscalDateEnding", isData.fiscalDateEnding);
            command.Parameters.AddWithValue("@_reportedCurrency", isData.reportedCurrency);
            command.Parameters.AddWithValue("@_grossProfit", isData.grossProfit);
            command.Parameters.AddWithValue("@_totalRevenue", isData.totalRevenue);
            command.Parameters.AddWithValue("@_costOfRevenue", isData.costOfRevenue);
            command.Parameters.AddWithValue("@_costofGoodsAndServicesSold", isData.costofGoodsAndServicesSold);
            command.Parameters.AddWithValue("@_operatingIncome", isData.operatingIncome);
            command.Parameters.AddWithValue("@_sellingGeneralAndAdministrative", isData.sellingGeneralAndAdministrative);
            command.Parameters.AddWithValue("@_researchAndDevelopment", isData.researchAndDevelopment);
            command.Parameters.AddWithValue("@_operatingExpenses", isData.operatingExpenses);
            command.Parameters.AddWithValue("@_investmentIncomeNet", isData.investmentIncomeNet);
            command.Parameters.AddWithValue("@_netInterestIncome", isData.netInterestIncome);
            command.Parameters.AddWithValue("@_interestIncome", isData.interestIncome);
            command.Parameters.AddWithValue("@_interestExpense", isData.interestExpense);
            command.Parameters.AddWithValue("@_nonInterestIncome", isData.nonInterestIncome);
            command.Parameters.AddWithValue("@_otherNonOperatingIncome", isData.otherNonOperatingIncome);
            command.Parameters.AddWithValue("@_depreciation", isData.depreciation);
            command.Parameters.AddWithValue("@_depreciationAndAmortization", isData.depreciationAndAmortization);
            command.Parameters.AddWithValue("@_incomerBeforeTax", isData.incomerBeforeTax);
            command.Parameters.AddWithValue("@_incomeTaxExpense", isData.incomeTaxExpense);
            command.Parameters.AddWithValue("@_interestAndDebtExpense", isData.interestAndDebtExpense);
            command.Parameters.AddWithValue("@_netIncomeFromContinuingOperations", isData.netIncomeFromContinuingOperations);
            command.Parameters.AddWithValue("@_comprehensiveIncomeNetOfTax", isData.comprehensiveIncomeNetOfTax);
            command.Parameters.AddWithValue("@_ebit", isData.ebit);
            command.Parameters.AddWithValue("@_ebitda", isData.ebitda);
            command.Parameters.AddWithValue("@_netIncome", isData.netIncome);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    
    public static class FinancialsSelectData
    {
        /// <summary>
        /// Next Five Methods are for table selecting and are relatively straightforward
        /// They take the sql query, execute and then read the result using built in .NET SQL libraries 
        /// </summary>   
        public static BalanceSheet SelectBalanceSheetData(string ticker)
        {
            BalanceSheet returnData = new(ticker);
            string script = "select * from [dbo].[BalanceSheetTable] where [Ticker] = @_Ticker";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", ticker);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {    
                    returnData.fiscalDateEnding = oReader["fiscalDateEnding"].ToString() ?? throw new InvalidOperationException();
                    returnData.reportedCurrency = oReader["reportedCurrency"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalAssets = oReader["totalAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalCurrentAssets = oReader["totalCurrentAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.cashAndCashEquivalentsAtCarryingValue = oReader["cashAndCashEquivalentsAtCarryingValue"].ToString() ?? throw new InvalidOperationException();
                    returnData.cashAndShortTermInvestments = oReader["cashAndShortTermInvestments"].ToString() ?? throw new InvalidOperationException();
                    returnData.inventory = oReader["inventory"].ToString() ?? throw new InvalidOperationException();
                    returnData.currentNetReceivables = oReader["currentNetReceivables"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalNonCurrentAssets = oReader["totalNonCurrentAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.propertyPlantEquipment = oReader["propertyPlantEquipment"].ToString() ?? throw new InvalidOperationException();
                    returnData.accumulatedDepreciationAmortizationPPE = oReader["accumulatedDepreciationAmortizationPPE"].ToString() ?? throw new InvalidOperationException();
                    returnData.intangibleAssets = oReader["intangibleAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.intangibleAssetsExcludingGoodwill = oReader["intangibleAssetsExcludingGoodwill"].ToString() ?? throw new InvalidOperationException();
                    returnData.goodwill = oReader["goodwill"].ToString() ?? throw new InvalidOperationException();
                    returnData.investments = oReader["investments"].ToString() ?? throw new InvalidOperationException();
                    returnData.longTermInvestments = oReader["longTermInvestments"].ToString() ?? throw new InvalidOperationException();
                    returnData.shortTermInvestments = oReader["shortTermInvestments"].ToString() ?? throw new InvalidOperationException();
                    returnData.otherCurrentAssets = oReader["otherCurrentAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.otherNonCurrrentAssets = oReader["otherNonCurrrentAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalLiabilities = oReader["totalLiabilities"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalCurrentLiabilities = oReader["totalCurrentLiabilities"].ToString() ?? throw new InvalidOperationException();
                    returnData.currentAccountsPayable = oReader["currentAccountsPayable"].ToString() ?? throw new InvalidOperationException();
                    returnData.deferredRevenue = oReader["deferredRevenue"].ToString() ?? throw new InvalidOperationException();
                    returnData.currentDebt = oReader["currentDebt"].ToString() ?? throw new InvalidOperationException();
                    returnData.shortTermDebt = oReader["shortTermDebt"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalNonCurrentLiabilities = oReader["totalNonCurrentLiabilities"].ToString() ?? throw new InvalidOperationException();
                    returnData.capitalLeaseObligations = oReader["capitalLeaseObligations"].ToString() ?? throw new InvalidOperationException();
                    returnData.longTermDebt = oReader["longTermDebt"].ToString() ?? throw new InvalidOperationException();
                    returnData.currentLongTermDebt = oReader["currentLongTermDebt"].ToString() ?? throw new InvalidOperationException();
                    returnData.longTermDebtNoncurrent = oReader["longTermDebtNoncurrent"].ToString() ?? throw new InvalidOperationException();
                    returnData.shortLongTermDebtTotal = oReader["shortLongTermDebtTotal"].ToString() ?? throw new InvalidOperationException();
                    returnData.otherCurrentLiabilities = oReader["otherCurrentLiabilities"].ToString() ?? throw new InvalidOperationException();
                    returnData.otherNonCurrentLiabilities = oReader["otherNonCurrentLiabilities"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalShareholderEquity = oReader["totalShareholderEquity"].ToString() ?? throw new InvalidOperationException();
                    returnData.treasuryStock = oReader["treasuryStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.retainedEarnings = oReader["retainedEarnings"].ToString() ?? throw new InvalidOperationException();
                    returnData.commonStock = oReader["commonStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.commonStockSharesOutstanding = oReader["commonStockSharesOutstanding"].ToString() ?? throw new InvalidOperationException();

                }
            }

            return returnData;
        }
        
        public static CashFlow SelectCashFlowData(string ticker)
        {
            CashFlow returnData = new(ticker);
            string script = "select * from [dbo].[CashFlowTable] where [Ticker] = @_Ticker";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", ticker);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {    
                    returnData.fiscalDateEnding = oReader["fiscalDateEnding"].ToString() ?? throw new InvalidOperationException();
                    returnData.reportedCurrency = oReader["reportedCurrency"].ToString() ?? throw new InvalidOperationException();
                    returnData.operatingCashflow = oReader["operatingCashflow"].ToString() ?? throw new InvalidOperationException();
                    returnData.paymentsForOperatingActivities = oReader["paymentsForOperatingActivities"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromOperatingActivities = oReader["proceedsFromOperatingActivities"].ToString() ?? throw new InvalidOperationException();
                    returnData.changeInOperatingLiabilities = oReader["changeInOperatingLiabilities"].ToString() ?? throw new InvalidOperationException();
                    returnData.changeInOperatingAssets = oReader["changeInOperatingAssets"].ToString() ?? throw new InvalidOperationException();
                    returnData.depreciationDepletionAndAmortization = oReader["depreciationDepletionAndAmortization"].ToString() ?? throw new InvalidOperationException();
                    returnData.capitalExpenditures = oReader["capitalExpenditures"].ToString() ?? throw new InvalidOperationException();
                    returnData.changeInReceivables = oReader["changeInReceivables"].ToString() ?? throw new InvalidOperationException();
                    returnData.changeInInventory = oReader["changeInInventory"].ToString() ?? throw new InvalidOperationException();
                    returnData.profitLoss = oReader["profitLoss"].ToString() ?? throw new InvalidOperationException();
                    returnData.cashflowFromInvestment = oReader["cashflowFromInvestment"].ToString() ?? throw new InvalidOperationException();
                    returnData.cashflowFromFinancing = oReader["cashflowFromFinancing"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromRepaymentsOfShortTermDebt = oReader["proceedsFromRepaymentsOfShortTermDebt"].ToString() ?? throw new InvalidOperationException();
                    returnData.paymentsForRepurchaseOfCommonStock = oReader["paymentsForRepurchaseOfCommonStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.paymentsForRepurchaseOfEquity = oReader["paymentsForRepurchaseOfEquity"].ToString() ?? throw new InvalidOperationException();
                    returnData.paymentsForRepurchaseOfPreferredStock = oReader["paymentsForRepurchaseOfPreferredStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.dividendPayout = oReader["dividendPayout"].ToString() ?? throw new InvalidOperationException();
                    returnData.dividendPayoutCommonStock = oReader["dividendPayoutCommonStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.dividendPayoutPreferredStock = oReader["dividendPayoutPreferredStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromIssuanceOfCommonStock = oReader["proceedsFromIssuanceOfCommonStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet = oReader["proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromIssuanceOfPreferredStock = oReader["proceedsFromIssuanceOfPreferredStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromRepurchaseOfEquity = oReader["proceedsFromRepurchaseOfEquity"].ToString() ?? throw new InvalidOperationException();
                    returnData.proceedsFromSaleOfTreasuryStock = oReader["proceedsFromSaleOfTreasuryStock"].ToString() ?? throw new InvalidOperationException();
                    returnData.changeInCashAndCashEquivalents = oReader["changeInCashAndCashEquivalents"].ToString() ?? throw new InvalidOperationException();
                    returnData.changeInExchangeRate = oReader["changeInExchangeRate"].ToString() ?? throw new InvalidOperationException();
                    returnData.netIncome = oReader["netIncome"].ToString() ?? throw new InvalidOperationException();

                }
            }

            return returnData;
        }
        
        public static CompanyOverview SelectCompanyOverviewData(string ticker)
        {
            CompanyOverview returnData = new(ticker);
            string script = "select * from [dbo].[CompanyOverviewTable] where [Ticker] = @_Ticker";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", ticker);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {    
                    returnData.AssetType = oReader["AssetType"].ToString() ?? throw new InvalidOperationException();
                    returnData.Name = oReader["Name"].ToString() ?? throw new InvalidOperationException();
                    returnData.Description = oReader["Description"].ToString() ?? throw new InvalidOperationException();
                    returnData.CIK = oReader["CIK"].ToString() ?? throw new InvalidOperationException();
                    returnData.Exchange = oReader["Exchange"].ToString() ?? throw new InvalidOperationException();
                    returnData.Currency = oReader["Currency"].ToString() ?? throw new InvalidOperationException();
                    returnData.Country = oReader["Country"].ToString() ?? throw new InvalidOperationException();
                    returnData.Sector = oReader["Sector"].ToString() ?? throw new InvalidOperationException();
                    returnData.Industry = oReader["Industry"].ToString() ?? throw new InvalidOperationException();
                    returnData.Address = oReader["Address"].ToString() ?? throw new InvalidOperationException();
                    returnData.FiscalYearEnd = oReader["FiscalYearEnd"].ToString() ?? throw new InvalidOperationException();
                    returnData.LatestQuarter = oReader["LatestQuarter"].ToString() ?? throw new InvalidOperationException();
                    returnData.MarketCapitalization = oReader["MarketCapitalization"].ToString() ?? throw new InvalidOperationException();
                    returnData.EBITDA = oReader["EBITDA"].ToString() ?? throw new InvalidOperationException();
                    returnData.PERatio = oReader["PERatio"].ToString() ?? throw new InvalidOperationException();
                    returnData.PEGRatio = oReader["PEGRatio"].ToString() ?? throw new InvalidOperationException();
                    returnData.BookValue = oReader["BookValue"].ToString() ?? throw new InvalidOperationException();
                    returnData.DividendPerShare = oReader["DividendPerShare"].ToString() ?? throw new InvalidOperationException();
                    returnData.DividendYield = oReader["DividendYield"].ToString() ?? throw new InvalidOperationException();
                    returnData.EPS = oReader["EPS"].ToString() ?? throw new InvalidOperationException();
                    returnData.RevenuePerShareTTM = oReader["RevenuePerShareTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.ProfitMargin = oReader["ProfitMargin"].ToString() ?? throw new InvalidOperationException();
                    returnData.OperatingMarginTTM = oReader["OperatingMarginTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.ReturnOnAssetsTTM = oReader["ReturnOnAssetsTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.ReturnOnEquityTTM = oReader["ReturnOnEquityTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.RevenueTTM = oReader["RevenueTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.GrossProfitTTM = oReader["GrossProfitTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.DilutedEPSTTM = oReader["DilutedEPSTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.QuarterlyEarningsGrowthYOY = oReader["QuarterlyEarningsGrowthYOY"].ToString() ?? throw new InvalidOperationException();
                    returnData.QuarterlyRevenueGrowthYOY = oReader["QuarterlyRevenueGrowthYOY"].ToString() ?? throw new InvalidOperationException();
                    returnData.AnalystTargetPrice = oReader["AnalystTargetPrice"].ToString() ?? throw new InvalidOperationException();
                    returnData.TrailingPE = oReader["TrailingPE"].ToString() ?? throw new InvalidOperationException();
                    returnData.ForwardPE = oReader["ForwardPE"].ToString() ?? throw new InvalidOperationException();
                    returnData.PriceToSalesRatioTTM = oReader["PriceToSalesRatioTTM"].ToString() ?? throw new InvalidOperationException();
                    returnData.PriceToBookRatio = oReader["PriceToBookRatio"].ToString() ?? throw new InvalidOperationException();
                    returnData.EVToRevenue = oReader["EVToRevenue"].ToString() ?? throw new InvalidOperationException();
                    returnData.EVToEBITDA = oReader["EVToEBITDA"].ToString() ?? throw new InvalidOperationException();
                    returnData.Beta = oReader["Beta"].ToString() ?? throw new InvalidOperationException();
                    returnData.FiftyTwoWeekHigh = oReader["FiftyTwoWeekHigh"].ToString() ?? throw new InvalidOperationException();
                    returnData.FiftyTwoWeekLow = oReader["FiftyTwoWeekLow"].ToString() ?? throw new InvalidOperationException();
                    returnData.FiftyDayMovingAverage = oReader["FiftyDayMovingAverage"].ToString() ?? throw new InvalidOperationException();
                    returnData.TwoHundredDayMovingAverage = oReader["TwoHundredDayMovingAverage"].ToString() ?? throw new InvalidOperationException();
                    returnData.SharesOutstanding = oReader["SharesOutstanding"].ToString() ?? throw new InvalidOperationException();
                    returnData.DividendDate = oReader["DividendDate"].ToString() ?? throw new InvalidOperationException();
                    returnData.ExDividendDate = oReader["ExDividendDate"].ToString() ?? throw new InvalidOperationException();

                }
            }

            return returnData;
        }
        
        public static Earnings SelectEarningsData(string ticker)
        {
            Earnings returnData = new(ticker);
            string script = "select * from [dbo].[EarningsDataTable] where [Ticker] = @_Ticker";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", ticker);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {    
                    returnData.ReportedEPS = oReader["ReportedEPS"].ToString() ?? throw new InvalidOperationException();

                }
            }
            return returnData;
        }
        
        public static IncomeStatement SelectIncomeStatementData(string ticker)
        {
            IncomeStatement returnData = new(ticker);
            string script = "select * from [dbo].[IncomeStatementDataTable] where [Ticker] = @_Ticker";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", ticker);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {    
                    returnData.fiscalDateEnding = oReader["fiscalDateEnding"].ToString() ?? throw new InvalidOperationException();
                    returnData.reportedCurrency = oReader["reportedCurrency"].ToString() ?? throw new InvalidOperationException();
                    returnData.grossProfit = oReader["grossProfit"].ToString() ?? throw new InvalidOperationException();
                    returnData.totalRevenue = oReader["totalRevenue"].ToString() ?? throw new InvalidOperationException();
                    returnData.costOfRevenue = oReader["costOfRevenue"].ToString() ?? throw new InvalidOperationException();
                    returnData.costofGoodsAndServicesSold = oReader["costofGoodsAndServicesSold"].ToString() ?? throw new InvalidOperationException();
                    returnData.operatingIncome = oReader["operatingIncome"].ToString() ?? throw new InvalidOperationException();
                    returnData.sellingGeneralAndAdministrative = oReader["sellingGeneralAndAdministrative"].ToString() ?? throw new InvalidOperationException();
                    returnData.researchAndDevelopment = oReader["researchAndDevelopment"].ToString() ?? throw new InvalidOperationException();
                    returnData.operatingExpenses = oReader["operatingExpenses"].ToString() ?? throw new InvalidOperationException();
                    returnData.investmentIncomeNet = oReader["investmentIncomeNet"].ToString() ?? throw new InvalidOperationException();
                    returnData.netInterestIncome = oReader["netInterestIncome"].ToString() ?? throw new InvalidOperationException();
                    returnData.interestIncome = oReader["interestIncome"].ToString() ?? throw new InvalidOperationException();
                    returnData.interestExpense = oReader["interestExpense"].ToString() ?? throw new InvalidOperationException();
                    returnData.nonInterestIncome = oReader["nonInterestIncome"].ToString() ?? throw new InvalidOperationException();
                    returnData.otherNonOperatingIncome = oReader["otherNonOperatingIncome"].ToString() ?? throw new InvalidOperationException();
                    returnData.depreciation = oReader["depreciation"].ToString() ?? throw new InvalidOperationException();
                    returnData.depreciationAndAmortization = oReader["depreciationAndAmortization"].ToString() ?? throw new InvalidOperationException();
                    returnData.incomerBeforeTax = oReader["incomerBeforeTax"].ToString() ?? throw new InvalidOperationException();
                    returnData.incomeTaxExpense = oReader["incomeTaxExpense"].ToString() ?? throw new InvalidOperationException();
                    returnData.interestAndDebtExpense = oReader["interestAndDebtExpense"].ToString() ?? throw new InvalidOperationException();
                    returnData.netIncomeFromContinuingOperations = oReader["netIncomeFromContinuingOperations"].ToString() ?? throw new InvalidOperationException();
                    returnData.comprehensiveIncomeNetOfTax = oReader["comprehensiveIncomeNetOfTax"].ToString() ?? throw new InvalidOperationException();
                    returnData.ebit = oReader["ebit"].ToString() ?? throw new InvalidOperationException();
                    returnData.ebitda = oReader["ebitda"].ToString() ?? throw new InvalidOperationException();
                    returnData.netIncome = oReader["netIncome"].ToString() ?? throw new InvalidOperationException();

                }
            }

            return returnData;
        }
    }
}
    
    