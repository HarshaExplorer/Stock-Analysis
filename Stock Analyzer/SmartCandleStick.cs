using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Stock_Analyzer
{
    /// <summary>
    /// Represents an enhanced candlestick with additional properties and pattern detection capabilities.
    /// </summary>
    internal class SmartCandleStick : CandleStick
    {
        // Properties for enhanced candlestick information
        public decimal Range { get; set; } // Full range of the candlestick
        public decimal BodyRange { get; set; } // Range of the candlestick body
        public decimal TopPrice { get; set; } // Higher of Open and Close prices
        public decimal BottomPrice { get; set; } // Lower of Open and Close prices
        public decimal UpperTail { get; set; } // Length of the upper tail
        public decimal LowerTail { get; set; } // Length of the lower tail

        // Dictionary to hold pattern types and their boolean evaluations
        public Dictionary<string, bool> patternTypes;

        /// <summary>
        /// Initializes a new instance of the SmartCandleStick class.
        /// </summary>
        /// <param name="dateStr">The date as a string.</param>
        /// <param name="openStr">The opening price as a string.</param>
        /// <param name="highStr">The highest price as a string.</param>
        /// <param name="lowStr">The lowest price as a string.</param>
        /// <param name="closeStr">The closing price as a string.</param>
        /// <param name="volumeStr">The trading volume as a string.</param>
        public SmartCandleStick(string dateStr, string openStr, string highStr, string lowStr, string closeStr, string volumeStr) : base(dateStr, openStr, highStr, lowStr, closeStr, volumeStr)
        {
            ComputeSmartProperties();
            InitializePatternTypes();
        }

        public SmartCandleStick(CandleStick cs)
        {
            Date = cs.Date;
            Open = cs.Open;
            High = cs.High;
            Low = cs.Low;
            Close = cs.Close;
            Volume = cs.Volume;

            ComputeSmartProperties();
            InitializePatternTypes();
        }

        /// <summary>
        /// Computes the "smart" properties of a smart candlestick and stores in member variables
        /// </summary>
        private void ComputeSmartProperties()
        {
            Range = High - Low;
            TopPrice = Math.Max(Open, Close);
            BottomPrice = Math.Min(Open, Close);
            BodyRange = TopPrice - BottomPrice;
            UpperTail = High - TopPrice;
            LowerTail = BottomPrice - Low;
        }

        /// <summary>
        /// Populates the patternTypes dictionary with the evaluated pattern types.
        /// </summary>
        private void InitializePatternTypes()
        {
            patternTypes = new Dictionary<string, bool>
            {
                // Bullish
                { "Bullish", Close > Open }, 

                // Bearish
                { "Bearish", Close < Open },

                // Neutral - tolerance 10%
                { "Neutral", BodyRange < (Range * 0.1m) }, 

                // Marubozu - tolerance 4%
                { "Marubozu", BodyRange > (Range * 0.96m) },

                // Hammer (min and max range detection)
                { "Hammer", BodyRange >= Range * 0.15m && BodyRange <= Range * 0.3m && LowerTail >= Range * 0.6m},
               
                // Doji - tolerance 5%
                { "Doji", BodyRange <= Range * 0.05m },
              
                // Dragonfly doji
                { "Dragonfly Doji", BodyRange <= (Range * 0.05m) && LowerTail > (Range * 0.6m) },
              
                // Gravestone doji
                { "Gravestone Doji", BodyRange <= (Range * 0.05m) && UpperTail > (Range * 0.6m) }
            };
        }

    }
}
