using System.Threading;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.Extensions.Logging;

namespace FinancialsDPM
{
    public class GetFinancialsData
    {
        /// <summary>
        /// Data Initialization and storage
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="cancellationToken"></param>
        public static async void GetAndStoreData(ILogger _logger, CancellationToken cancellationToken)
        {
            InitFinancialsTable();
            FinancialsUpdateTables.UpdateEarningsTable(GetEarningsData.ProcessEarningsData(await GetEarningsData.EarningsRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateCashFlowTable(GetCashFlowData.ProcessCashFlowData(await GetCashFlowData.CashFlowDataRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateBalanceSheetTable(GetBalanceSheetData.ProcessBalanceSheetData(await GetBalanceSheetData.BalanceSheetRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateIncomeStatementTable(GetIncomeStatementData.ProcessIncomeStatementsData(await GetIncomeStatementData.IncomeStatementRequest(_logger, cancellationToken, true))[0]);
            FinancialsUpdateTables.UpdateCompanyOverviewTableTable(GetCompanyOverviewData.ProcessCompanyOverviewData(await GetCompanyOverviewData.CompanyOverviewRequest("This Bug Is Hard To Find",_logger, cancellationToken))[0]);
        }
        /// <summary>
        /// Table Initialization
        /// </summary>
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