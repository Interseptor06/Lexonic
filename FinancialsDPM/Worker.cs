using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GetBalanceSheetData = FinancialsDPM.FinancialsDPM.GetBalanceSheetData;

namespace FinancialsDPM
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private bool isInit = false;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (isInit == false)
                {
                    FinancialsTableOps.CreateTables();
                    isInit = true;
                }
                FinancialsUpdateTables.UpdateEarningsTable(
                    GetEarningsData.ProcessBalanceSheetData(
                        await GetEarningsData.EarningsRequest(_logger, stoppingToken, true))[0]);
                
                FinancialsUpdateTables.UpdateCashFlowTable(
                    GetCashFlowData.ProcessCashFlowData(
                        await GetCashFlowData.CashFlowDataRequest(_logger, stoppingToken, true))[0]);
                
                FinancialsUpdateTables.UpdateBalanceSheetTable(
                    GetBalanceSheetData.ProcessBalanceSheetData(
                        await GetBalanceSheetData.BalanceSheetRequest(_logger, stoppingToken, true))[0]);
                
                FinancialsUpdateTables.UpdateIncomeStatementTable(
                    GetIncomeStatementData.ProcessIncomeStatementsData(
                        await GetIncomeStatementData.IncomeStatementRequest(_logger, stoppingToken, true))[0]);
                
                FinancialsUpdateTables.UpdateCompanyOverviewTableTable(
                    GetCompanyOverviewData.ProcessCompanyOverviewData(
                        await GetCompanyOverviewData.CompanyOverviewRequest(_logger, stoppingToken, true))[0]);
                
                //Console.WriteLine("ALO");
                // 24 hour minutes delay
                await Task.Delay(1000*60*60*24, stoppingToken);
            }
        }
    }
}