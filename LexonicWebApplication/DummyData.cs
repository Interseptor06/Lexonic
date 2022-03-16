using System.Collections.Generic;
using FinancialsDPM;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.AspNetCore.Authentication;
using NewsTwitterDPM;
using StockDPM;

namespace LexonicWebApplication
{
    public class DummyData
    {
        public static List<StockData> infoForChart()
        {
            return StockTableOps.SelectHistoricData("GOOGL");
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
    }
}