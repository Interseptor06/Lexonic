using System.Collections.Generic;
using System.Linq;
using FinancialsDPM;
using FinancialsDPM.FinancialsDPM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using NewsTwitterDPM;
using StockList = FinancialsDPM.StockList;


namespace LexonicWebApplication.Pages
{

    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string ValueToPass {get; set;}
/*
        public double OurPrice { get; set; }

        public BalanceSheet balanceSheet { get; set; }
        public CashFlow cashFlow { get; set; }
        public CompanyOverview companyOverview{ get; set; }
        public Earnings earnings { get; set; }
        public IncomeStatement incomeStatement { get; set; }
        
        public List<NewsData> NewsDatas { get; set; }
        
        public void OnGet()
        {
            balanceSheet = FinancialsSelectData.SelectBalanceSheetData(StockList.SList[0]);
            cashFlow = FinancialsSelectData.SelectCashFlowData(StockList.SList[0]);
            companyOverview = FinancialsSelectData.SelectCompanyOverviewData(StockList.SList[0]);
            earnings = FinancialsSelectData.SelectEarningsData(StockList.SList[0]);
            incomeStatement = FinancialsSelectData.SelectIncomeStatementData(StockList.SList[0]);

            NewsDatas = NewsTwitterTableOps.SelectFromNewsTable(StockList.SList[0]);

            OurPrice = double.Parse(companyOverview.AnalystTargetPrice)- 2.176;

        }
        */
    }
}