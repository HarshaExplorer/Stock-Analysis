# Stock Analysis

https://github.com/user-attachments/assets/6544f46e-28cb-4453-9d0f-fb7290ff2dd1

A C# .NET desktop application that allows users to load stock data, visualize candlestick charts, analyze key patterns, and apply Fibonacci retracement across multiple windows for comparative insights.

## Features

### 1. **Candlestick Chart Visualization**
- Displays stock data in candlestick chart format for intuitive analysis.
- Supports importing CSV files containing stock data fields (in any order): `Date`, `Open`, `High`, `Low`, `Close`, and `Volume`.

### 2. **Candlestick Pattern Detection**
Automatically detects and highlights (with annotations) the following candlestick patterns:
- **Bullish**: Indicates upward price movement (Close > Open).
- **Bearish**: Reflects downward price movement (Close < Open).
- **Doji**: Denotes market indecision (Open ≈ Close).
- **Hammer**: Suggests potential reversals with a small body and long lower shadow.
- **Dragonfly Doji**: Signals potential bullish reversals with no upper tail and a long lower tail.
- **Gravestone Doji**: Signals potential bearish reversals with no lower tail and a long upper tail.
- **Neutral**: Indicates minor indecision with a small body relative to the total range.
- **Marubozu**: Represents strong momentum with no upper or lower tails.

### 3. **Peak and Valley Detection**
- Identifies key **peaks** (local highs) and **valleys** (local lows) within stock data trends.
- Peaks are highlighted with **green arrows** across the chart.
- Valleys are highlighted with **red arrows** across the chart.

### 4. **Multi-Window Stock View**
- Allows users to open multiple stock files in separate windows for side-by-side comparison.
- Each window operates independently, enabling multi-stock analysis.

### 5. **Fibonacci Retracement**
- Allows users to select a **valid wave** of candlesticks on the chart using the rubber banding method (mouse dragging).
- A dynamic rectangle is drawn on the current selection, displaying Fibonacci levels (0%, 23.6%, 38.2%, 50%, 61.8%, 76.4%, and 100%) along with their corresponding price labels—only if the selection is valid.
   - If the selection is invalid, the rectangle is filled with red color, and the user will be prompted to adjust their selection.
- Confirmations between candlestick OHLC values and Fibonacci levels are marked as small yellow dots.
-  **Beauty Chart:** To help visualize the "beauty" of the selected wave, which represents the number of confirmations between candlestick OHLC values and Fibonacci levels.
   - The selected wave is extended by ±25% beyond its original range, and for every extension, its beauty is computed, allowing deeper analysis of price action beyond its original boundaries.
   - Essentially, this chart helps identify prices with high beauty values, indicating areas of significant support or resistance.
## Robust Error Handling
- Validates input data and alerts users about missing or corrupt files.
- Prevents crashes by skipping invalid or null data entries during analysis.
- Displays meaningful error messages when operations cannot be performed, such as attempting to detect patterns without loaded data.

