using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Analyzer
{
    /// <summary>
    /// Represents a candlestick in stock data, containing information about the stock's performance for a specific date.
    /// </summary>
    internal class CandleStick
    {
        /// <summary>
        /// Gets or sets the date of the candlestick.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the opening price of the stock for the day.
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Gets or sets the highest price of the stock for the day.
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Gets or sets the lowest price of the stock for the day.
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Gets or sets the closing price of the stock for the day.
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Gets or sets the volume of stocks traded for the day.
        /// </summary>
        public ulong Volume { get; set; }

        /// <summary>
        /// Initializes a new instance of the CandleStick class using the provided string representations of its properties.
        /// </summary>
        /// <param name="dateStr">The date as a string.</param>
        /// <param name="openStr">The opening price as a string.</param>
        /// <param name="highStr">The highest price as a string.</param>
        /// <param name="lowStr">The lowest price as a string.</param>
        /// <param name="closeStr">The closing price as a string.</param>
        /// <param name="volumeStr">The trading volume as a string.</param>
        /// <exception cref="ArgumentException">Thrown when one or more values cannot be parsed.</exception>
        public CandleStick(string dateStr, string openStr, string highStr, string lowStr, string closeStr, string volumeStr)
        {
            // Attempt to parse each value from the provided string inputs
            if (DateTime.TryParse(dateStr, out DateTime date) &&
                decimal.TryParse(openStr, out decimal open) &&
                decimal.TryParse(highStr, out decimal high) &&
                decimal.TryParse(lowStr, out decimal low) &&
                decimal.TryParse(closeStr, out decimal close) &&
                ulong.TryParse(volumeStr, out ulong volume))
            {
                // Assign parsed values to properties
                Date = date;
                Open = open;
                High = high;
                Low = low;
                Close = close;
                Volume = volume;
            }
            else
            {
                // Throw an exception if parsing fails
                throw new ArgumentException("One or more values could not be parsed for CandleStick.");
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CandleStick()
        {
        }

        /// <summary>
        /// Returns a string representation of the CandleStick object, including its date, open, high, low, close, and volume.
        /// </summary>
        /// <returns>A formatted string representing the CandleStick data.</returns>
        public override string ToString()
        {
            return $"Date: {Date.ToShortDateString()}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume}";
        }
    }
}
