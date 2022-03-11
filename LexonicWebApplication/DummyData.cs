using System.Collections.Generic;
using FinancialsDPM;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.AspNetCore.Authentication;
using StockDPM;

namespace LexonicWebApplication
{
    public class DummyData
    {
        public static List<StockData> infoForChart()
        {
            List<StockData> chartInfo = new List<StockData>();
            chartInfo.Add(new StockData());
            chartInfo[0].Open = 110;
            chartInfo[0].Close = 130;
            chartInfo[0].High = 200;
            chartInfo[0].Low = 100;
            
            chartInfo.Add(new StockData());
            chartInfo[1].Open = 140;
            chartInfo[1].Close = 130;
            chartInfo[1].High = 190;
            chartInfo[1].Low = 100;
            
            chartInfo.Add(new StockData());
            chartInfo[2].Open = 110;
            chartInfo[2].Close = 300;
            chartInfo[2].High = 310;
            chartInfo[2].Low = 100;
            
            chartInfo.Add(new StockData());
            chartInfo[3].Open = 300;
            chartInfo[3].Close = 290;
            chartInfo[3].High = 309;
            chartInfo[3].Low = 280;
            
            chartInfo.Add(new StockData());
            chartInfo[4].Open = 293;
            chartInfo[4].Close = 190;
            chartInfo[4].High = 320;
            chartInfo[4].Low = 182;
            
            chartInfo.Add(new StockData());
            chartInfo[5].Open = 192;
            chartInfo[5].Close = 200;
            chartInfo[5].High = 209;
            chartInfo[5].Low = 190;
            
            chartInfo.Add(new StockData());
            chartInfo[6].Open = 180;
            chartInfo[6].Close = 185;
            chartInfo[6].High = 199;
            chartInfo[6].Low = 180;
            
            chartInfo.Add(new StockData());
            chartInfo[7].Open = 200;
            chartInfo[7].Close = 390;
            chartInfo[7].High = 305;
            chartInfo[7].Low = 198;
            
            chartInfo.Add(new StockData());
            chartInfo[8].Open = 400;
            chartInfo[8].Close = 420;
            chartInfo[8].High = 420;
            chartInfo[8].Low = 399;
            
            chartInfo.Add(new StockData());
            chartInfo[9].Open = 430;
            chartInfo[9].Close = 400;
            chartInfo[9].High = 432;
            chartInfo[9].Low = 429;
            
            chartInfo.Add(new StockData());
            chartInfo[10].Open = 390;
            chartInfo[10].Close = 200;
            chartInfo[10].High = 390;
            chartInfo[10].Low = 200;
            
            chartInfo.Add(new StockData());
            chartInfo[11].Open = 190;
            chartInfo[11].Close = 180;
            chartInfo[11].High = 190;
            chartInfo[11].Low = 178;
            
            chartInfo.Add(new StockData());
            chartInfo[12].Open = 170;
            chartInfo[12].Close = 167;
            chartInfo[12].High = 173;
            chartInfo[12].Low = 166 ;
            
            chartInfo.Add(new StockData());
            chartInfo[13].Open = 160;
            chartInfo[13].Close = 163;
            chartInfo[13].High = 170;
            chartInfo[13].Low = 160;
            
            chartInfo.Add(new StockData());
            chartInfo[14].Open = 170;
            chartInfo[14].Close = 160;
            chartInfo[14].High = 170;
            chartInfo[14].Low = 140;

            return chartInfo;
        }

        public static CompanyOverview getCompanyOverview(string ticker)
        {
            CompanyOverview coInfo = new CompanyOverview(ticker);

            coInfo.Description = "Google LLC is an American multinational technology company that specializes in Internet-related services and products, which include a search engine, online advertising technologies, cloud computing, software, and hardware.";
            coInfo.Industry = "Artificial intelligence Advertising Cloud computing Computer software Computer hardware Internet";
            coInfo.Sector = "Technology";
            coInfo.DividendYield = " - ";
            coInfo.MarketCapitalization = "1.76T";
            coInfo.FiftyTwoWeekHigh = "3,030.93";
            coInfo.FiftyTwoWeekLow = "1,996.09";
            coInfo.PERatio = "23.78";

            return coInfo;
        }

        public static BalanceSheet getBalanceSheet(string ticker)
        {
            BalanceSheet bsInfo = new BalanceSheet(ticker);

            bsInfo.goodwill = "";
            bsInfo.commonStock = "";
            bsInfo.totalAssets = "";
            bsInfo.totalLiabilities = "";

            return bsInfo;
        }

        public static IncomeStatement getIncomeStatement(string ticker)
        {
            IncomeStatement isInfo = new IncomeStatement(ticker);

            isInfo.grossProfit = "";
            isInfo.netIncome = "";
            isInfo.netInterestIncome = "";
            isInfo.totalRevenue = "";
            isInfo.depreciation = "";

            return isInfo;
        }

        public static Earnings getEarnings(string ticker)
        {
            Earnings eInfo = new Earnings(ticker);

            eInfo.ReportedEPS = "";

            return eInfo;
        }

        public static CashFlow getCashFlow(string ticker)
        {
            CashFlow cfInfo = new CashFlow(ticker);

            cfInfo.operatingCashflow = "";
            cfInfo.changeInExchangeRate = "";
            cfInfo.profitLoss = "";
            cfInfo.dividendPayout = "";

            return cfInfo;
        }
    }
}