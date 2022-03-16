using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

namespace StockDPM
{
    
    public static class StockTableOps
    {
        /// <summary>
        /// Relatively self-explanatory
        /// SQL utility methods for Table creation, selection and insertion
        /// Both for Historical stock table and Prediction Table
        /// </summary>

        public static void CreateHistoricStockTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries", "CreateHistoricalDataTableQuery.sql");
            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void CreatePredictionDataTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries", "CreatePredictionTable.sql");
            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();        
        }

        public static void InsertHistoricStockTable(string _ticker, string _date, decimal _open, decimal _close, decimal _high, decimal _low, Int64 _volume, Int64 _numoftransacts)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries", "InsertHistoricData.sql");

            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(script, connection);
                command.Parameters.AddWithValue("@_Ticker", _ticker);
                command.Parameters.AddWithValue("@_DateAdded", _date);
                command.Parameters.AddWithValue("@_Open", _open);
                command.Parameters.AddWithValue("@_Close", _close);
                command.Parameters.AddWithValue("@_High", _high);
                command.Parameters.AddWithValue("@_Low", _low);
                command.Parameters.AddWithValue("@_Volume", _volume);
                command.Parameters.AddWithValue("@_NumOfTransacts", _numoftransacts);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void InsertPredictionDataTable(string _ticker, DateTime _date, float _prediction)
        {
            string script =
                File.ReadAllText(@"/home/martin/RiderProjects/Lexonic/StockDPM/SqlQueries/InsertPrediction.sql");

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(script, connection);
                command.Parameters.AddWithValue("@_Ticker", _ticker);
                command.Parameters.AddWithValue("@_DateAdded", _date);
                command.Parameters.AddWithValue("@_Prediction", _prediction);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void BulkInsertHistoricStockTable(List<StockData> data)
        {

            foreach (StockData stock in data)
            {
                InsertHistoricStockTable(stock.Ticker,
                    stock.Date,
                    stock.Open,
                    stock.Close,
                    stock.High,
                    stock.Low,
                    stock.Volume,
                    stock.NumberOfTransactions
                );
            }
        }
        
        public static List<StockData> SelectHistoricData(string Ticker)
        {
            List<StockData> returnData = new();
            string script = "select * from [dbo].[HistoricalStockDataTable] where [Ticker] = @_Ticker";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", Ticker);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {    
                    
                    returnData.Add(new StockData()
                    {
                        Ticker = oReader["Ticker"].ToString(),
                        Open = decimal.Parse(oReader["Open"].ToString()),
                        Close = decimal.Parse(oReader["Close"].ToString()),
                        High = decimal.Parse(oReader["High"].ToString()),
                        Low = decimal.Parse(oReader["Low"].ToString()),
                        Volume = Int64.Parse(oReader["Volume"].ToString()),
                        NumberOfTransactions = Int64.Parse(oReader["NumOfTransacts"].ToString()),
                        Date = oReader["DateAdded"].ToString()
                    });

                }
            }
            return returnData;
        }
        public static List<string> SelectPredictionData()
        {
            List<string> returnData = new();
            string script = "select * from [dbo].[StockPredictiontable]";
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            
            connection.Open();
            
            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {
                    string TempStr = "Hold";
                    if (float.Parse(oReader["Prediction"].ToString()) == 0)
                    {
                        TempStr = "Hold";
                    }
                    else if (float.Parse(oReader["Prediction"].ToString()) > 0)
                    {
                        TempStr = "Buy";
                    }
                    else
                    {
                        TempStr = "Sell";
                    }
                    
                    returnData.Add(TempStr);

                }
            }
            return returnData;
        }
        
    }
}