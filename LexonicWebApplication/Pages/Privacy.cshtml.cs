using System;
using System.Collections.Generic;
using System.Linq;
using FinancialsDPM;
using FinancialsDPM.FinancialsDPM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using NewsTwitterDPM;
using StockDPM;
using System.Globalization;
using System.IO;
using System.Threading;
using NumSharp;
using StockList = FinancialsDPM.StockList;
using SO=System.IO.File;

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

        public List<StockData> stockData = new List<StockData>();

        public CompanyOverview overview = new("");
        public List<NewsData> NewsDatas { get; set; }
        public string Sentiment { get; set; }
        public string Prediction { get; set; }

        //TODO: Fix the way the date is shown 
        public DateTime date = DateTime.UtcNow.Date;
        
        
        
        //overview = FinancialsSelectData.SelectCompanyOverviewData(ValueToPass);
        public void OnGet()
        {
            NewsDatas = NewsTwitterTableOps.SelectFromNewsTable(ValueToPass);
            overview = FinancialsSelectData.SelectCompanyOverviewData(ValueToPass);
            stockData = StockTableOps.SelectHistoricData(ValueToPass).OrderByDescending(x => x.Date).ToList();
            int index = FinancialsDPM.StockList.SList.IndexOf(ValueToPass);
            Prediction = StockTableOps.SelectPredictionData()[index];
            if (NewsTwitterTableOps.SelectFromNewsTable(ValueToPass).Select(x => x.Sentiment).Average() < 0.5 & NewsTwitterTableOps.SelectFromNewsTable(ValueToPass).Select(x => x.Sentiment).Average() > -0.5)
            {
                Sentiment = "Neutral";
                
            }
            else if (NewsTwitterTableOps.SelectFromNewsTable(ValueToPass).Select(x => x.Sentiment).Average() > 0.5)
            {
                Sentiment = "Positive";
            
            }
            else
            {
                Sentiment = "Negative";
                
            }
        }
        
    }
}





