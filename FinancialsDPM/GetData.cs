using System.Threading;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.Extensions.Logging;

namespace FinancialsDPM
{
    public class GetFinancialsData
    {
        public static async void GetAndStoreData(ILogger _logger, CancellationToken cancellationToken)
        {
            InitFinancialsTable();
            FinancialsUpdateTables.UpdateEarningsTable(GetEarningsData.ProcessBalanceSheetData(await GetEarningsData.EarningsRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateCashFlowTable(GetCashFlowData.ProcessCashFlowData(await GetCashFlowData.CashFlowDataRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateBalanceSheetTable(GetBalanceSheetData.ProcessBalanceSheetData(await GetBalanceSheetData.BalanceSheetRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateIncomeStatementTable(GetIncomeStatementData.ProcessIncomeStatementsData(await GetIncomeStatementData.IncomeStatementRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateCompanyOverviewTableTable(GetCompanyOverviewData.ProcessCompanyOverviewData(await GetCompanyOverviewData.CompanyOverviewRequest(_logger, cancellationToken, true))[0]);
        }
        
        public static void InitFinancialsTable()
        {
            if (isInited == false)
            { 
                FinancialsTableOps.CreateEarningsTable();
                FinancialsTableOps.CreateBalanceSheetTable();
                FinancialsTableOps.CreateCashFlowTable();
                FinancialsTableOps.CreateCompanyOverviewTable();
                FinancialsTableOps.CreateIncomeStatementTable();
                isInited = true;
            }
        }

        private static bool isInited = false;
    }
}