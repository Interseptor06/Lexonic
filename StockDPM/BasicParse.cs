using System;
using System.Collections.Generic;
using System.Text.Json;

namespace StockDPM
{
    public class BasicParse
    {
        public string? RequestError;
        public string RequestStatus = String.Empty;
        
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