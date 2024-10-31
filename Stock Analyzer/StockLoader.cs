using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Analyzer
{
    /// <summary>
    /// Handles loading and parsing stock data from a CSV file.
    /// </summary>
    internal class StockLoader
    {
        /// <summary>
        /// Loads stock data from a CSV file and creates a list of candlestick objects.
        /// </summary>
        /// <param name="filePath">The path to the CSV file containing stock data.</param>
        /// <returns>A list of <see cref="CandleStick"/> objects representing the parsed stock data.</returns>
        public List<CandleStick> LoadStockData(string filePath)
        {
            // List to hold parsed candlestick objects
            var candlesticks = new List<CandleStick>();
            // Flag to warn about any blank rows encountered
            bool blankRowEncounterWarned = false;

            try
            {
                // Read the file line by line using a StreamReader
                using (var reader = new StreamReader(filePath))
                {
                    // Read the header line to get column names
                    var headerLine = reader.ReadLine().Replace("\"", string.Empty); ;
                    var headers = headerLine.Split(',');

                    // Expected headers for validation
                    var headerKeys = new List<string> { "date", "open", "high", "low", "close", "volume" };

                    // Create a dictionary to map header names to their column indices
                    var columnIndexMap = new Dictionary<string, int>();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        // Trim the excess whitespaces and convert to lowercase for each header column
                        headers[i] = headers[i].ToLower().Trim();
                        columnIndexMap[headers[i]] = i;
                    }

                    // Ensure all required headers are present
                    foreach (var key in headerKeys) 
                        if (!columnIndexMap.ContainsKey(key))
                            // If the a required column is not present in the header of the input file, throw InvalidDataException
                            throw new InvalidDataException("CSV headers are not as expected. Headers must contain (in any order): " + String.Join(",",headerKeys));
                    

                    // Read each line of the CSV
                    while (!reader.EndOfStream)
                    {
                        // Read and clean the line of text
                        var line = reader.ReadLine().Replace("\"", string.Empty); ;
                        var values = line.Split(',');

                        try
                        {
                            // Map CSV columns to corresponding candlestick properties
                            string dateStr = values[columnIndexMap["date"]];
                            string openStr = values[columnIndexMap["open"]];
                            string highStr = values[columnIndexMap["high"]];
                            string lowStr = values[columnIndexMap["low"]];
                            string closeStr = values[columnIndexMap["close"]];
                            string volumeStr = values[columnIndexMap["volume"]];

                            // Create a new CandleStick object and add it to the list
                            var candlestick = new CandleStick(dateStr, openStr, highStr, lowStr, closeStr, volumeStr);
                            candlesticks.Add(candlestick);
                        }
                        catch (ArgumentException ex)
                        {
                            // Warn once if blank rows are encountered
                            if (!blankRowEncounterWarned)
                            {
                                blankRowEncounterWarned = true;
                                // Alert the user regarding unclean input data
                                MessageBox.Show("While parsing the input stock data, some rows in the CSV contained blank values. These rows will be omitted during processing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (InvalidDataException ex)
            {
                // Handle missing or invalid headers
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle general exceptions such as file format issues
                MessageBox.Show("An error occurred while loading stock data! Make sure your input file is properly formatted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Return the list of parsed candlestick objects
            return candlesticks; 
        }
    }
}
