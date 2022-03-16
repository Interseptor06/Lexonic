using System;
using System.Collections.Generic;
using System.Text.Json;

namespace FinancialsDPM
{
    /// <summary>
    /// Used to assure data processing method, that the response from the API isn't garbage.
    /// </summary>
    public class BasicParse
    {
        public string? RequestError;
        public string RequestStatus = String.Empty;

        public Int32 BasicNewsParseJson(string responseBody)
        {
            Dictionary<string, JsonElement> jsonData =
                JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody) ??
                throw new InvalidOperationException();
            // Handles case in which there is no error and assigns null to RequestError which is nullable
            RequestError = jsonData["status"].ToString() == "ERROR" ? jsonData["error"].ToString() : null;
            RequestStatus = jsonData["status"].ToString();

            return jsonData["status"].ToString() == "OK" ? 1 : 0;
        }

        public Int32 BasicStockParseJson(string responseBody)
        {
            Dictionary<string, JsonElement> jsonData =
                JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody) ??
                throw new InvalidOperationException();
            // Instead of "count" it's "queryCount"
            return jsonData["queryCount"].GetInt32();
        }
    }
}