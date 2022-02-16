using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

namespace StockDPM
{
    
    public static class CreateTables
    {

        public static void CreateHistoricStockTable()
        {
            string script =
                File.ReadAllText(@"/home/martin/RiderProjects/Lexonic/StockDPM/SqlQueries/CreateHistoricalDataTableQuery.sql");

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void CreatePredictionDataTable()
        {
            string script =
                File.ReadAllText(@"/home/martin/RiderProjects/Lexonic/StockDPM/SqlQueries/CreatePredictionTable.sql");

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();        
        }

        public static void InsertHistoricStockTable(string _ticker, string _date, decimal _open, decimal _close, decimal _high, decimal _low, UInt64 _volume, UInt64 _numoftransacts)
        {
            string script =
                File.ReadAllText(@"/home/martin/RiderProjects/Lexonic/StockDPM/SqlQueries/InsertHistoricData.sql");

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

        public static void InsertPredictionDataTable(string _ticker, string _date, decimal _prediction)
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
                command.Parameters.AddWithValue("@_Open", _prediction);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void BulkInsertHistoricStockTable(Dictionary<DateOnly, List<StockData>> data)
        {
            foreach (KeyValuePair<DateOnly, List<StockData>> pair in data)
            {
                foreach (StockData stock in pair.Value)
                {
                    InsertHistoricStockTable(stock.Ticker,
                                        pair.Key.ToString(),
                                                stock.Open,
                                                stock.Close,
                                                stock.High,
                                                stock.Low,
                                                stock.Volume,
                                stock.NumberOfTransactions
                        );
                }
            }
        }
    }
}