using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Analyzer
{
    internal class PeakValleyDetector
    {
        public List<int> Peaks { get; set; }

        public List<int> Valleys { get; set; }

        public PeakValleyDetector(BindingList<CandleStick> candlesticks)
        {
            Peaks = new List<int>();
            Valleys = new List<int>();

            CandleStick prev, curr, next;

            for (int i = 1; i < candlesticks.Count - 1; i++)
            {
                prev = candlesticks[i - 1];
                curr = candlesticks[i];
                next = candlesticks[i + 1];

                if (prev == null || curr == null || next == null)
                    continue;

                if (curr.High > prev.High && curr.High > next.High)
                    Peaks.Add(i);

                else if (curr.Low < prev.Low && curr.Low < next.Low)
                    Valleys.Add(i);

            }
        }
    }
}
