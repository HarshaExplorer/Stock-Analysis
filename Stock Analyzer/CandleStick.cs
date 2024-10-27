using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Analyzer
{
    internal class CandleStick
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public ulong Volume { get; set; }


        public CandleStick(string dateStr, string openStr, string highStr, string lowStr, string closeStr, string volumeStr)
        {
            if (DateTime.TryParse(dateStr, out DateTime date) &&
                decimal.TryParse(openStr, out decimal open) &&
                decimal.TryParse(highStr, out decimal high) &&
                decimal.TryParse(lowStr, out decimal low) &&
                decimal.TryParse(closeStr, out decimal close) &&
                ulong.TryParse(volumeStr, out ulong volume))
            {
                Date = date;
                Open = open;
                High = high;
                Low = low;
                Close = close;
                Volume = volume;
            }
            else
            {
                throw new ArgumentException("One or more values could not be parsed for CandleStick.");
            }
        }
        public override string ToString()
        {
            return $"Date: {Date.ToShortDateString()}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume}";
        }
    }
}
