using System;
using System.IO;
using Microsoft.Data.SqlClient;


namespace NewsTwitterDPM
{

    public class CreateTables
    {
        /* News Table Architecture
         * 
         */
        public static void CreateNewsTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries", "CreateNewsTable.sql");
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
            string path = Path.Combine(Environment.CurrentDirectory, @"SqlQueries", "CreateNewsTable.sql");
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


            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}