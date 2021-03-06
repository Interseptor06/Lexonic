using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;


namespace NewsTwitterDPM
{

    public class NewsTwitterTableOps
    {
        /// <summary>
        /// SQL utilities with self explanatory names and code
        /// One is for Creation of the table, The other is for Insertion and the last is for Selecting Data
        /// </summary>

        public static void CreateNewsTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries",@"NewsSqlQueries", "CreateNewsTable.sql");
            string script = File.ReadAllText(path);

            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            

            connection.Open();
            command.ExecuteNonQuery();
        }
        
        public static void InsertIntoNewsTable(NewsData newsData)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries" ,@"NewsSqlQueries", "InsertToNewsTable.sql");
            string script = File.ReadAllText(path);
            
            string connectionString =
                "Server=localhost;database=testDB;User ID=SA; Password=SM-dab/ftf/SL95!; Encrypt=No;Initial Catalog=TestDB";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(script, connection);
            command.Parameters.AddWithValue("@_Ticker", newsData.Ticker);
            command.Parameters.AddWithValue("@_Title", newsData.Title);
            command.Parameters.AddWithValue("@_ArticleUrl", newsData.Article_url);
            command.Parameters.AddWithValue("@_Date", newsData.Date);
            command.Parameters.AddWithValue("@_Time", newsData.Time);
            command.Parameters.AddWithValue("@_Sentiment", newsData.Sentiment);


            connection.Open();
            command.ExecuteNonQuery();
        }

        public static List<NewsData> SelectFromNewsTable(string ticker)
        {
            List<NewsData> lData = new();
            
            string script = "select * from [dbo].[NewsDataTable] where [Ticker] = @_Ticker";
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

                    lData.Add(new NewsData(oReader["Ticker"].ToString() ?? throw new InvalidOperationException(),
                                                oReader["Title"].ToString() ?? throw new InvalidOperationException(),
                                            oReader["ArticleURL"].ToString() ?? throw new InvalidOperationException(),
                                                oReader["Date"].ToString() ?? throw new InvalidOperationException(),
                                               oReader["Time"].ToString() ?? throw new InvalidOperationException(),
                                                double.Parse(oReader["Sentiment"].ToString() ?? throw new InvalidOperationException())));
                }
            }
            
            return lData;
        }
    }
}