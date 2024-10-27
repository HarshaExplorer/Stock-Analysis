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
                    var headerLine = reader.ReadLine();
                    var headers = headerLine.Split(',');

                    // Create a dictionary to map header names to their indices
                    var columnIndexMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < headers.Length; i++)
                    {
                        columnIndexMap[headers[i].Trim()] = i;
                    }

                    // Read each line of the CSV
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        try
                        {
                            // Get the string values using the mapped indices
                            string dateStr = values[columnIndexMap["Date"]];
                            string openStr = values[columnIndexMap["Open"]];
                            string highStr = values[columnIndexMap["High"]];
                            string lowStr = values[columnIndexMap["Low"]];
                            string closeStr = values[columnIndexMap["Close"]];
                            string volumeStr = values[columnIndexMap["Volume"]];

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
