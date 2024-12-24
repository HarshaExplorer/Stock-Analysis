# Stock Analysis

https://github.com/user-attachments/assets/936bcfd2-1485-4129-a33a-c977fc18a12d

A C# .NET desktop application that allows users to load stock data, visualize candlestick charts, and analyze key patterns across multiple windows for comparative insights.

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
- Peaks are highlighted with **green arrows** and horizontal lines across the chart.
- Valleys are highlighted with **red arrows** and horizontal lines across the chart.

### 4. **Multi-Window Stock View**
- Allows users to open multiple stock files in separate windows for side-by-side comparison.
- Each window operates independently, enabling multi-stock analysis.

### 5. **Fibonacci Retracement Levels (Upcoming Feature)**
- Fibonacci retracement levels on the charts for advanced support/resistance analysis.
- Enables users to anticipate potential price reversals or continuations.

## Robust Error Handling
- Validates input data and alerts users about missing or corrupt files.
- Prevents crashes by skipping invalid or null data entries during analysis.
- Displays meaningful error messages when operations cannot be performed, such as attempting to detect patterns without loaded data.

