using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Analyzer
{
    internal class StockLoader
    {
        public List<CandleStick> LoadStockData(string filePath)
        {
            var candlesticks = new List<CandleStick>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    // Read the header line to get column names
                    var headerLine = reader.ReadLine().Replace("\"", string.Empty); ;
                    var headers = headerLine.Split(',');

                    // Create a dictionary to map header names to their indices
                    var columnIndexMap = new Dictionary<string, int>();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        headers[i] = headers[i].ToLower().Trim();
                        columnIndexMap[headers[i]] = i;
                    }

                    // Read each line of the CSV
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Replace("\"", string.Empty); ;
                        var values = line.Split(',');

                        try
                        {
                            // Get the string values using the mapped indices
                            string dateStr = values[columnIndexMap["date"]];
                            string openStr = values[columnIndexMap["open"]];
                            string highStr = values[columnIndexMap["high"]];
                            string lowStr = values[columnIndexMap["low"]];
                            string closeStr = values[columnIndexMap["close"]];
                            string volumeStr = values[columnIndexMap["volume"]];

                            // Create a new CandleStick and add it to the list
                            var candlestick = new CandleStick(dateStr, openStr, highStr, lowStr, closeStr, volumeStr);
                            candlesticks.Add(candlestick);
                        }
                        catch (ArgumentException ex)
                        {
                            MessageBox.Show("Error parsing the stock data!");
                            Console.WriteLine($"Error creating CandleStick: {ex.Message}. Line: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading stock data!");
                Console.WriteLine($"An error occurred while loading stock data: {ex.Message}.");
            }
            
            return candlesticks;
        }
    }
}
