using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Analyzer
{
    /// <summary>
    /// Detects peaks and valleys in a series of candlesticks.
    /// </summary>
    internal class PeakValleyDetector
    {
        /// <summary>
        /// List of indices where peaks are found.
        /// </summary>
        public List<int> Peaks { get; set; }

        /// <summary>
        /// List of indices where valleys are found.
        /// </summary>
        public List<int> Valleys { get; set; }

        /// <summary>
        /// Initializes a new instance of the PeakValleyDetector class
        /// and identifies peaks and valleys in the provided candlestick data.
        /// </summary>
        /// <param name="candlesticks">The candlestick data to analyze.</param>
        public PeakValleyDetector(BindingList<CandleStick> candlesticks)
        {
            Peaks = new List<int>();
            Valleys = new List<int>();

            // Populate Peaks and Valleys based on the initial candlestick data.
            updatePeaksAndValleys(candlesticks);
        }

        /// <summary>
        /// Updates the Peaks and Valleys lists by detecting new peaks and valleys
        /// in the provided candlestick data.
        /// </summary>
        /// <param name="candlesticks">The candlestick data to analyze.</param>
        public void updatePeaksAndValleys(BindingList<CandleStick> candlesticks)
        {
            // Clear any previous peak and valley data.
            Peaks.Clear();
            Valleys.Clear();

            // Temporary variables to hold the previous, current, and next candlesticks for comparison.
            CandleStick prev, curr, next;

            // Loop through candlesticks, skipping the first and last ones (boundary check).
            for (int i = 1; i < candlesticks.Count - 1; i++)
            {
                // Assign previous, current, and next candlesticks.
                prev = candlesticks[i - 1];
                curr = candlesticks[i];
                next = candlesticks[i + 1];

                // Skip if any of the required candlesticks are null.
                if (prev == null || curr == null || next == null)
                    continue;

                // Check if the current candlestick is a peak (higher than both previous and next).
                if (curr.High > prev.High && curr.High > next.High)
                    Peaks.Add(i);

                // Check if the current candlestick is a valley (lower than both previous and next).
                else if (curr.Low < prev.Low && curr.Low < next.Low)
                    Valleys.Add(i);
            }
        }

        public bool isPeakOrValley(int pointIndex)
        {
            return Peaks.Contains(pointIndex) || Valleys.Contains(pointIndex);
        }
    }
}
