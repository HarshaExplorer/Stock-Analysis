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

        }
        public override string ToString()
        {
            return $"Date: {Date.ToShortDateString()}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume}";
        }
    }
}
