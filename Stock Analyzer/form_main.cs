using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace Stock_Analyzer
{
    public partial class Form_Main : Form
    {
        //List of all candlesticks read from file
        private List<CandleStick> candlesticks = null;
        /// Binding list of candlesticks bound to chart_OHLCV for graph display
        private BindingList<CandleStick> bindCandlesticks = null;
        // List to store filtered candlestick data based on date range
        private List<CandleStick> filteredCandlesticks = null;
        // Instance of StockLoader for loading stock data from file
        private StockLoader stockLoader = null;
        // Instance of PeakValleyDetector for identifying peaks & valleys
        private PeakValleyDetector peakValleyDetector = null;
        // Variables for storing start and end dates for filtering
        private DateTime startDate, endDate;
        // Stores the name of the currently loaded file
        private String currentInputFileName = null;
        // Stores the state of fibonacci retracement mode - toggle on or off
        private bool retracementMode = false;
        // To track the starting point of mouse interaction for Fibonacci retracement selection
        private Point startPoint;
        // To track the current mouse position during retracement selection
        private Point currentPoint;
        // Boolean flag to indicate whether the user is currently dragging the mouse
        private bool isDragging = false;
        // Index of the starting and ending candlesticks for the retracement selection
        private int selectionStartPointIndex, selectionEndPointIndex;
        // Boolean flag to indicate if a valid wave has been selected by the user
        private bool validWaveSelected = false;
        // Tolerance percentage for retracement confirmations, allowing a leeway for matches
        private decimal retracementPercentLeeway = 0.5M;
        // Array to store beauty levels that will be used for retracement
        private decimal[] beautyLevels;
        // Array of Fibonacci levels used for retracement calculations
        private decimal[] fibLevels = { 0.0M, 0.236M, 0.382M, 0.5M, 0.618M, 0.764M, 1.0M};
        // List to store ellipse annotations for retracement confirmations on the chart
        private List<EllipseAnnotation> retracementConfirmations = null;


        // Instance of RectangleAnnotation for the drawn retracement selection
        private RectangleAnnotation selectionRectangle = null;
        // Array to store the list of supported patterns 
        private String[] candlestickPatterns = { "Bullish", "Bearish", "Neutral", "Marubozu", "Hammer", "Doji", "Dragonfly Doji", "Gravestone Doji", "Peaks", "Valleys", "--Select--"};

        /// <summary>
        /// Initializes a new instance of the form_main class.
        /// </summary>
        public Form_Main()
        {
            // Initialize form components
            InitializeComponent();
            // Set up form elements and initial data
            InitializeForm();
        }

        /// <summary>
        /// Initializes a new instance of the Form_Main class with the specified stock data file and date range.
        /// </summary>
        /// <param name="inputFilePath">The path to the stock data file to be processed.</param>
        /// <param name="start">The start date for data filtering.</param>
        /// <param name="end">The end date for data filtering.</param>
        public Form_Main(string inputFilePath, DateTime start, DateTime end)
        {
            // Initialize form components
            InitializeComponent();
            // Set up form elements and initial data
            InitializeForm();

            // Set the start and end dates based on values passed from the parent form
            dateTimePicker_startDate.Value = startDate = start;
            dateTimePicker_endDate.Value = endDate = end;

            // Process the input file to load and display stock data
            processData(inputFilePath);
        }

        /// <summary>
        /// Initializes the form's key components and settings, including candlestick data, stock loader, date selection, and pattern options.
        /// </summary>
        private void InitializeForm()
        {
            // Initialize the list that will hold all candlesticks parsed from input data
            candlesticks = new List<CandleStick>(1500);
            // Initialize an object of StockLoader that will load the stock input data and parse it
            stockLoader = new StockLoader();
            // Initialize the list that will hold all candlesticks confirmations for chosen wave retracement
            retracementConfirmations = new List<EllipseAnnotation>(50);
            // Set default percent leeway for retracement
            numericUpDown_retracement_leeway.Value = retracementPercentLeeway;
            // Set the beauty levels for retracement: +/- 25%
            beautyLevels = new decimal[] {-0.25M, -0.20M, -0.15M, -0.10M, -0.05M, 0M, 0.05M, 0.10M, 0.15M, 0.20M, 0.25M};
            // Pre-select the start & end dates for user convenience
            preselectDates();
            // Populate the comboBox_patterns
            populateComboBoxPatterns();
        }

        /// <summary>
        /// Event handler for the button to load stocks. Opens the file dialog to select a CSV file.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to button click.</param>
        private void button_loadStocks_Click(object sender, EventArgs e)
        {
            // On button click, change text of the window form
            Text = "Stock Viewer - Opening Stock File...";
            // Display the file explorer to select a input stock data file
            DialogResult dialogResult = openFileDialog_stockFilePick.ShowDialog();
            // If the user close the file dialog with choosing an input file, then adjust the window title
            if(dialogResult == DialogResult.Cancel)
                Text = "Stock Viewer" + (currentInputFileName != null ? (" - "+currentInputFileName):(""));
        }

        /// <summary>
        /// Event handler for file selection confirmation in the openFileDialog. 
        /// Processes selected stock data files and displays each in a separate Form_Main instance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Provides data for a cancelable event.</param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // Get the count of selected files in the open file dialog
            int numOfInputFiles = openFileDialog_stockFilePick.FileNames.Count();
            // Variable to hold the path of each input file
            String InputFile;
            // Variable to reference the form displaying each stock file data
            Form_Main stockViewerForm;

            // Iterate through each selected file to process and display it
            for (int i = 0; i < numOfInputFiles; i++)
            {
                // Retrieve the path of the current file
                InputFile = openFileDialog_stockFilePick.FileNames[i];

                if (i == 0)
                {
                    // If it's the first file, use the current form instance
                    stockViewerForm = this;
                    // Process and display the data in the current form
                    processData(InputFile);
                }
                else
                {
                    // For additional input files, create a new Form_Main instance with specified parameters
                   stockViewerForm = new Form_Main(InputFile, startDate, endDate);

                }

                // Show the form and bring it to the front
                stockViewerForm.Show();
                stockViewerForm.BringToFront();
            }
        }

        /// <summary>
        /// Processes the selected stock data file and updates the candlestick data for display.
        /// </summary>
        /// <param name="inputFile">The path to the stock data file.</param>
        private void processData(string inputFile)
        {
            // Load stock data from file
            List<CandleStick> newCandlesticks = stockLoader.LoadStockData(inputFile);

            // Validate loaded data and apply date filters
            if (newCandlesticks.Count > 0 && validateFilterDates())
            {
                // Sort the candlesticks by date from oldest to newest
                candlesticks = newCandlesticks.OrderBy(c => c.Date).ToList();
                // Filter the sorted candlesticks by selected date range
                filteredCandlesticks = filterCandlesticksByDate();
                // Create a binding list for feeding the candlesticks to the chart controls
                bindCandlesticks = new BindingList<CandleStick>(filteredCandlesticks);
                // Identify all peaks and valleys in the given list of candlesticks
                peakValleyDetector = new PeakValleyDetector(bindCandlesticks);

                // Adjust chart settings based on data
                adjustChart();

                // Clear any annotations on the chart area from previous processing
                ClearAnnotations();


                // Bind data to UI controls
                bindCandlestickData();

                // Update the form title with the loaded file name
                currentInputFileName = Path.GetFileNameWithoutExtension(inputFile);
                Text = "Stock Viewer - " + currentInputFileName;
            }
            else
                // Adjust the window title to reflect the current stock data file being processed
                Text = "Stock Viewer" + (currentInputFileName != null ? (" - " + currentInputFileName) : (""));
        }

        /// <summary>
        /// Clears all annotations from the chart. If a valid wave is selected and retracement mode is active, 
        /// restores retracement confirmation annotations to the chart.
        /// </summary>
        private void ClearAnnotations()
        {
            chart_OHLCV.Annotations.Clear();
            if (validWaveSelected && retracementMode)
            {
                foreach (var confirmation in retracementConfirmations)
                    chart_OHLCV.Annotations.Add(confirmation);
            }
        }

        /// <summary>
        /// Binds the candlestick data to the chart controls.
        /// </summary>
        private void bindCandlestickData()
        {
            // Bind candlesticks to Chart controls
            chart_OHLCV.DataSource = bindCandlesticks;
            chart_OHLCV.DataBind();
        }

        /// <summary>
        /// Filters candlesticks based on the specified date range (startDate to endDate).
        /// </summary>
        /// <returns>A list of filtered candlesticks within the date range.</returns>
        private List<CandleStick> filterCandlesticksByDate()
        {
            // Filtered list initialized with same capacity as candlesticks list
            List<CandleStick> filteredCandleSticks = new List<CandleStick>(candlesticks.Count);

            // Filter candlesticks based on date range
            foreach (CandleStick c in candlesticks)
                if (c.Date >= startDate && c.Date <= endDate)
                    filteredCandleSticks.Add(c);

            return filteredCandleSticks;
        }

        /// <summary>
        /// Adjusts the chart's Y-axis based on the minimum and maximum values of the filtered candlesticks.
        /// </summary>
        private void adjustChart()
        {

            // Initial min value from first candlestick's low and initial max value is set to 0.
            decimal min = bindCandlesticks.First().Low, max = 0;

            // Find the minimum and maximum for the Y-axis based on low and high prices
            foreach (CandleStick c in bindCandlesticks)
            { 
                if (c.Low < min) 
                    min = c.Low;
                if (c.High > max) 
                    max = c.High;
            }
            // Set the Y-axis range to +/- 2% of the min and max values, rounded
            chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = Math.Floor(Decimal.ToDouble(min) * 0.98);
            chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = Math.Ceiling(Decimal.ToDouble(max) * 1.02);
        }

        /// <summary>
        /// Normalizes the Y-axis of the beauty chart by setting the maximum value to the highest beauty value
        /// plus 2%, rounded to the nearest whole number. The minimum value is set to zero.
        /// </summary>
        private void NormalizeBeautyChart()
        {
            // Find the maximum Y-value
            double maxBeauty = chart_Beauty.Series[0].Points.Max(point => point.YValues[0]);

            // Add 2% and round to the nearest whole number
            double normalizedMax = Math.Ceiling(maxBeauty * 1.02);

            // Set the Y-axis range
            chart_Beauty.ChartAreas[0].AxisY.Minimum = 0;       
            chart_Beauty.ChartAreas[0].AxisY.Maximum = normalizedMax; 
        }

        /// <summary>
        /// Preselects initial date range for stock data filtering.
        /// </summary>
        private void preselectDates()
        {
            startDate = new DateTime(2022, 01, 01); // Default start date
            endDate = DateTime.Now; // Default end date

            // Set date pickers to the initial date range
            dateTimePicker_startDate.Value = startDate;
            dateTimePicker_endDate.Value = endDate;
        }

        /// <summary>
        /// Populates the comboBox_patterns with available candlestick patterns from the candlestickPatterns list.
        /// </summary>
        private void populateComboBoxPatterns()
        {
            // Clear any existing items in the comboBox to avoid duplicates
            comboBox_patterns.Items.Clear();

            // Add each candlestick pattern from candlestickPatterns list to the comboBox
            foreach (String pattern in candlestickPatterns)
                comboBox_patterns.Items.Add(pattern);   
        }

        /// <summary>
        /// Event handler for the update button click. Refreshes the display based on the date filter.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to button click.</param>
        private void button_update_Click(object sender, EventArgs e)
        {   
            // If no data is loaded, alert the user to load input stock data first 
            if (candlesticks.Count == 0)
                MessageBox.Show("Load stock data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (validateFilterDates() && !validWaveSelected)
            {
                // If the date range is valid, then filter the candlesticks by the chosen date range
                filteredCandlesticks = filterCandlesticksByDate();
                // Create a new binding list with the updated filtered candlesticks
                bindCandlesticks = new BindingList<CandleStick>(filteredCandlesticks);
                // Find all peaks and valleys in new filtered list of candlesticks
                peakValleyDetector.updatePeaksAndValleys(bindCandlesticks);

                // normalize chart axes
                adjustChart();
                // Bind filtered data to UI controls
                bindCandlestickData();
                
                // If no option is selected, assign default
                if (comboBox_patterns.SelectedItem == null)
                    comboBox_patterns.SelectedItem = "--Select--";

                // Detect & draw patterns if any selected 
                comboBox_patterns_SelectedIndexChanged(comboBox_patterns, EventArgs.Empty);
            }
            //Handles the scenario when the retracement mode is active and a valid wave is selected.
            else if (retracementMode && validWaveSelected)
            {
                // Clear any existing retracement confirmations
                ClearConfirmations();
                // Remove all data points from the first series in the beauty chart
                chart_Beauty.Series[0].Points.Clear();
                // Perform the retracement calculation with the new percent leeway
                performRetracement();
            }
        }

        /// <summary>
        /// Validates the date range for filtering, ensuring the start date is earlier than the end date.
        /// </summary>
        /// <returns>True if the date range is valid; otherwise, false.</returns>
        private bool validateFilterDates()
        {
            // Start date should be earlier than end date
            if (startDate >= endDate)
                MessageBox.Show("Start date must be earlier than end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                // If the date range is valid
                return true;

            // If the date range is invalid
            return false;
        }

        /// <summary>
        /// Event handler for the start date picker value change. Updates the startDate field.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to value change.</param>
        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {
            if (!validWaveSelected)
                // Update the start date to newly chosen start date
                startDate = dateTimePicker_startDate.Value;
            else
                MessageBox.Show("Date controls are locked when there is an active Fibonacci retracement on the chart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            dateTimePicker_startDate.Value = startDate; 
        }

        /// <summary>
        /// Event handler for when a pattern is selected in comboBox_patterns.
        /// Highlights the chosen candlestick pattern on the chart if stock data is loaded.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data associated with the selection change.</param>
        private void comboBox_patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If no candlestick data is loaded, show a warning message to the user and exit
            if (candlesticks.Count == 0)
            {
                MessageBox.Show("Load stock data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Clear any existing annotations on the chart
            ClearAnnotations();

            // Resize the chart axes and redraw
            //chart_OHLCV.Invalidate();
            //chart_OHLCV.Update();

            // Retrieve the selected pattern from the combo box
            String patternChosen = comboBox_patterns.SelectedItem.ToString();
            // Temporary variable to hold each SmartCandleStick instance
            SmartCandleStick scs;

            // Exit if the default "--Select--" option is chosen
            if (patternChosen == "--Select--")
                return;

            // Check if the selected pattern is either "Peaks" or "Valleys"
            if (patternChosen == "Peaks" || patternChosen == "Valleys")
            {
                // Determine if the chosen pattern is "Peaks"; set 'isPeak' to true if so, otherwise false for "Valleys"
                bool isPeak = (patternChosen == "Peaks") ? (true) : (false);
                // Get the list of candlestick indices corresponding to peaks or valleys
                List<int> candlesticksIndexes = (isPeak) ? (peakValleyDetector.Peaks) : (peakValleyDetector.Valleys);
                // Set the annotation color to green for peaks or red for valleys
                Color annotationColor = (isPeak) ? (Color.Lime) : (Color.Red);

                // Loop through each index in the peaks or valleys list
                foreach (int index in candlesticksIndexes)
                {
                        // Get the DataPoint on the chart at the current index
                        DataPoint dp = chart_OHLCV.Series[0].Points[index];
                        // Create an arrow annotation to mark the pattern
                        ArrowAnnotation arrowMarker = new ArrowAnnotation();
                        // Align marker with X-axis and Y-Axis
                        arrowMarker.AxisX = chart_OHLCV.ChartAreas[0].AxisX;
                        arrowMarker.AxisY = chart_OHLCV.ChartAreas[0].AxisY;
                        // Set marker width and height
                        arrowMarker.Width = arrowMarker.Height = 0.5;
                        // Set marker color to green if peak or red if valley
                        arrowMarker.BackColor = annotationColor;

                        // Anchor the marker to the matching DataPoint on the chart
                        arrowMarker.SetAnchor(dp);

                        // If the marker represents a valley, set its Y-position to the Low value of the candlestick
                        if (!isPeak)
                           arrowMarker.Y = (double)bindCandlesticks[index].Low;

                        // Add the annotation marker to the chart
                        chart_OHLCV.Annotations.Add(arrowMarker);
                }
            }
            else
            {
                // Iterate through all bound candlesticks to identify and mark matching patterns
                for (int i = 0; i < bindCandlesticks.Count; i++)
                {
                    // Create a SmartCandleStick instance from the current candlestick in the binding list
                    scs = new SmartCandleStick(bindCandlesticks[i]);
                    // Get the corresponding DataPoint on the chart
                    DataPoint dp = chart_OHLCV.Series[0].Points[i];

                    // If the SmartCandleStick matches the selected pattern, create an annotation marker
                    if (scs != null && scs.patternTypes[patternChosen])
                    {
                        // Create an arrow annotation to mark the pattern
                        ArrowAnnotation marker = new ArrowAnnotation();
                        // Align marker with X-axis and Y-Axis
                        marker.AxisX = chart_OHLCV.ChartAreas[0].AxisX;
                        marker.AxisY = chart_OHLCV.ChartAreas[0].AxisY;
                        // Set marker width and height
                        marker.Width = marker.Height = 0.5;
                        // Set marker color to Azure
                        marker.BackColor = Color.Azure;

                        // Anchor the marker to the matching DataPoint on the chart
                        marker.SetAnchor(dp);
                        // Add the annotation marker to the chart
                        chart_OHLCV.Annotations.Add(marker);
                    }
                }
            }
        }

        /// <summary>
        /// Toggles the Fibonacci retracement mode on or off when the retracement button is clicked.
        /// Updates the button text and color based on the current mode. Hides the volume chart area 
        /// when the retracement mode is active and clears confirmations and beauty chart points when deactivated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void button_retracement_Click(object sender, EventArgs e)
        {
            // Toggle the retracement mode
            retracementMode = !retracementMode;
            button_retracement.Text = (retracementMode) ? ("Fibonacci Retracement: On") : ("Fibonacci Retracement: Off");
            button_retracement.ForeColor = (retracementMode) ? (Color.LimeGreen) : (Color.Red);
            
            // Hide the volume chart area when in retracement mode
            chart_OHLCV.ChartAreas["ChartArea_Volume"].Visible = !retracementMode;

            if (!retracementMode)
            {
                // Clear and reset
                validWaveSelected = false;
                ClearConfirmations();
                chart_Beauty.Series[0].Points.Clear();
            }

        }

        /// <summary>
        /// Clears all existing retracement confirmation annotations from the chart
        /// and resets the list of confirmations.
        /// </summary>
        private void ClearConfirmations()
        {
            foreach (var prevConfirmation in retracementConfirmations)
                chart_OHLCV.Annotations.Remove(prevConfirmation);
            retracementConfirmations.Clear();
        }

        /// <summary>
        /// Handles the MouseDown event for the OHLC chart to initiate the retracement selection.
        /// Detects whether the user is in retracement mode and determines if the mouse click occurred
        /// on a valid peak or valley point. Clears previous confirmations and beauty points if a valid
        /// selection is made.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void chart_OHLCV_MouseDown(object sender, MouseEventArgs e)
        {
            if (retracementMode)
            {
                // Perform a HitTest to detect where the mouse click occurred
                HitTestResult hit = chart_OHLCV.HitTest(e.X, e.Y);

                // If no data is loaded, alert the user to load input stock data first 
                if (candlesticks.Count == 0)
                {
                    MessageBox.Show("Load stock data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (hit.ChartArea != null && hit.ChartArea.Name == "ChartArea_OHLC")
                {
                    // Check if the clicked point is a valid peak or valley
                    if (hit.PointIndex >= 0 && peakValleyDetector.isPeakOrValley(hit.PointIndex)) // Ensure a valid point is hit
                    {
                        selectionStartPointIndex = hit.PointIndex;
                        // Record starting point and set dragging state
                        startPoint = e.Location;
                        isDragging = true; // Start dragging
                        validWaveSelected = false;
                        ClearConfirmations();
                        chart_Beauty.Series[0].Points.Clear();
                    }
                    else
                    {
                        // Stop dragging if the point is invalid
                        isDragging = false;
                        MessageBox.Show("MouseDown for rectangle selection must occur on a valid peak/valley candlestick on the chart! Use the Detect Pattern Tool to find peaks or valleys.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        /// <summary>
        /// Handles the MouseMove event for the OHLC chart to update the current mouse position 
        /// and visualize the selection rectangle while in retracement mode.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void chart_OHLCV_MouseMove(object sender, MouseEventArgs e)
        {
            if (retracementMode)
            {
                // Perform a HitTest to detect where the mouse click occurred
                HitTestResult hit = chart_OHLCV.HitTest(e.X, e.Y);

                // If the user is dragging and the mouse is within the OHLC chart area
                if (isDragging && hit.ChartArea != null && hit.ChartArea.Name == "ChartArea_OHLC")
                {
                    currentPoint = e.Location; // Update current mouse position
                    selectionEndPointIndex = hit.PointIndex;
                    chart_OHLCV.Invalidate(); // Invalidate the chart to trigger a repaint

                }
            }
        }

        /// <summary>
        /// Handles the MouseUp event for the OHLC chart to finalize the selection of the retracement rectangle.
        /// Checks if the selected wave is valid and performs the retracement calculation if valid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void chart_OHLCV_MouseUp(object sender, MouseEventArgs e)
        {
            if (retracementMode && isDragging)
            {
                isDragging = false;

                // Validate the selected wave
                bool isValidRectangleSelection = isValidWave();

                if (isValidRectangleSelection)
                {
                    // Mark that a valid wave has been selected
                    validWaveSelected = true;
                    // Execute the retracement logic
                    performRetracement();
                }
                else
                {
                    // Show an error message if the selected wave is not valid
                    MessageBox.Show("Selected wave is not valid! Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }
            
        }

        /// <summary>
        /// Performs the retracement calculation based on the selected wave and populates the beauty chart
        /// with calculated beauty values for various base prices derived from the wave's high and low points.
        /// </summary>
        private void performRetracement()
        {
            // Determine if the selected wave is downward based on the high values of the start and end candlesticks
            bool isWaveDownward = (bindCandlesticks[selectionStartPointIndex].High > bindCandlesticks[selectionEndPointIndex].High);
            decimal basePrice, waveHeight;

            // Set the base price based on whether the wave is downward or upward
            decimal selectedWaveBasePrice = (isWaveDownward) ? (bindCandlesticks[selectionEndPointIndex].Low) : (bindCandlesticks[selectionEndPointIndex].High);

            // Iterate through predefined beauty levels to calculate and plot beauty values
            foreach (decimal beautyRange in beautyLevels)
            {
                basePrice = selectedWaveBasePrice * (1 + beautyRange);

                // Calculate the wave height based on the direction of the wave
                if (isWaveDownward)
                    waveHeight = bindCandlesticks[selectionStartPointIndex].High - basePrice;
                else
                    waveHeight = basePrice - bindCandlesticks[selectionStartPointIndex].Low;

                // Add calculated beauty values to the chart series
                chart_Beauty.Series[0].Points.AddXY(Math.Round(basePrice, 2), calculateBeauty(basePrice, waveHeight, (beautyRange == 0)));

            }

            // Set tooltips for each point in the beauty chart
            foreach (var point in chart_Beauty.Series[0].Points)
            {
                point.ToolTip = $"Beauty: {point.YValues[0]}";
            }

            // Normalize the beauty chart's Y-axis after populating the points
            NormalizeBeautyChart();
        }

        /// <summary>
        /// Handles the Paint event for the OHLC chart to draw the selection rectangle 
        /// and Fibonacci retracement levels when in retracement mode.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data containing the graphics object for drawing.</param>
        private void chart_OHLCV_Paint(object sender, PaintEventArgs e)
        {
            if ((isDragging || validWaveSelected) && retracementMode) 
            {
                
                // Get the drawing graphics object
                Graphics g = e.Graphics;

                // Calculate rectangle bounds
                int x = startPoint.X;
                int y = Math.Min(startPoint.Y, currentPoint.Y);
                int width = Math.Abs(currentPoint.X - startPoint.X);
                int height = Math.Abs(currentPoint.Y - startPoint.Y);

                // Check if the selected rectangle is valid for a wave
                bool isValidRectangleSelection = isValidWave();

                // Draw the rectangle with a semi-transparent fill and a border
                using (var fillBrush = new SolidBrush(Color.FromArgb(50, (isValidRectangleSelection) ? (Color.Green) : (Color.Red))))
                {
                    g.FillRectangle(fillBrush, x, y, width, height);
                }
                using (var pen = new Pen(Color.Red, 1))
                {
                    g.DrawRectangle(pen, x, y, width, height);
                }

                // If the rectangle selection is valid, draw Fibonacci levels
                if (isValidRectangleSelection)
                {
                    
                    int rectTop = y;
                    int rectBottom = y + height;
                    bool isWaveDownward = bindCandlesticks[selectionStartPointIndex].High > bindCandlesticks[selectionEndPointIndex].High;
                    decimal selectedWaveBasePrice = (isWaveDownward) ? (bindCandlesticks[selectionEndPointIndex].Low) : (bindCandlesticks[selectionEndPointIndex].High);
                    decimal selectedWaveHeight = (isWaveDownward) ? (bindCandlesticks[selectionStartPointIndex].High - selectedWaveBasePrice) : (selectedWaveBasePrice - bindCandlesticks[selectionStartPointIndex].Low);

                    // Draw the Fibonacci label next to the line
                    foreach (double level in fibLevels)
                    {
                        // Calculate Y position for each Fibonacci level
                        int fibY = rectTop + (int)(level * height);

                        // Draw a horizontal line for the Fibonacci level
                        using (var fibPen = new Pen(Color.Blue, 2))
                        {
                            g.DrawLine(fibPen, x, fibY, x + width, fibY);
                        }

                        // Draw the Fibonacci label
                        using (var font = new Font("Arial", 10, FontStyle.Bold))
                        {
                            using (var brush = new SolidBrush(Color.Black))
                            {
                                double levelLabel = (isWaveDownward) ?
                                                    (1 - level) : (level);
                                decimal price = (isWaveDownward) ? (selectedWaveBasePrice + (selectedWaveHeight * (decimal)levelLabel)) : (selectedWaveBasePrice - (selectedWaveHeight * (decimal)levelLabel));
                                price = Math.Round(price,2);

                                // Create the label with percentage and price
                                string label = $"{ (levelLabel * 100):0.0}% ({price})";
                                g.DrawString(label, font, brush, x + width + 5, fibY - 10);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the ValueChanged event for the numeric up-down control that sets the retracement leeway percentage.
        /// Updates the <see cref="retracementPercentLeeway"/> variable with the new value selected by the user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void numericUpDown_retracement_leeway_ValueChanged(object sender, EventArgs e)
        {
            retracementPercentLeeway = numericUpDown_retracement_leeway.Value;
        }

        /// <summary>
        /// Validates the selected wave by checking that all candlesticks between the start and end indices 
        /// are within the bounds defined by the highest and lowest points of the selected range.
        /// </summary>
        /// <returns>True if the selected wave is valid; otherwise, false.</returns>
        private bool isValidWave()
        {
            // Ensure that both selection indices are valid
            if (selectionEndPointIndex < 0 || selectionStartPointIndex < 0)
                return false;
            else
            {
                decimal upperBound, lowerBound;
                // Determine upper and lower bounds based on the high and low of the selected candlesticks
                if (bindCandlesticks[selectionStartPointIndex].High > bindCandlesticks[selectionEndPointIndex].High)
                {
                    upperBound = bindCandlesticks[selectionStartPointIndex].High;
                    lowerBound = bindCandlesticks[selectionEndPointIndex].Low;
                }
                else
                {
                    lowerBound = bindCandlesticks[selectionStartPointIndex].Low;
                    upperBound = bindCandlesticks[selectionEndPointIndex].High;
                }

                // Check all candlesticks in the selection range to ensure they fall within the defined bounds
                for (int i = selectionStartPointIndex+1; i < selectionEndPointIndex; i++)
                {
                    var candlestick = bindCandlesticks[i];

                    // If any candlestick's high exceeds the upper bound or low is below the lower bound, return false
                    if (candlestick.High > upperBound || candlestick.Low < lowerBound)
                        return false;
                }
            }

            return true; // All checks passed, the wave is valid
        }

        /// <summary>
        /// Calculates the beauty value based on the given base price and wave height. It also annotates the 
        /// chart with confirmations for the specified price levels derived from Fibonacci retracement.
        /// </summary>
        /// <param name="basePrice">The base price for the wave calculation.</param>
        /// <param name="waveHeight">The height of the wave used to calculate price levels.</param>
        /// <param name="annotateConfirmations">Indicates whether to annotate the chart with confirmation points.</param>
        /// <returns>The calculated beauty value.</returns>
        private int calculateBeauty(decimal basePrice, decimal waveHeight, bool annotateConfirmations)
        {
            int beauty = 0;
            decimal price = 0;
            // Determine if the wave is downward based on the selected candlesticks
            bool isWaveDownward = (bindCandlesticks[selectionStartPointIndex].High > bindCandlesticks[selectionEndPointIndex].High);
            List<(decimal, decimal)> fibonacciPriceLevels = new List<(decimal, decimal)>(7);

            // Calculate Fibonacci price levels based on the base price and wave height
            for (int i = 0; i < fibLevels.Length; i++)
            {
                // Calculate price based on whether the wave is downward or upward
                price = (isWaveDownward) ? (basePrice + (waveHeight * fibLevels[i])): (basePrice - (waveHeight * fibLevels[i]));
                // Add lower and upper bounds for each Fibonacci level considering the retracement leeway
                fibonacciPriceLevels.Add( (price*(1-(retracementPercentLeeway/100)), price*(1+(retracementPercentLeeway/100))) );
            }

            // Iterate through the selected candlesticks to check for OHLC confirmations
            for (int i = selectionStartPointIndex; i <= selectionEndPointIndex; i++)
            {
                var cs = bindCandlesticks[i];
                var dataPoint = chart_OHLCV.Series[0].Points[i];

                // Inner function to compare values and annotate the chart if they match the Fibonacci levels
                void compareAndAnnotate(decimal value)
                    {
                        foreach ((decimal lowerPrice, decimal upperPrice) in fibonacciPriceLevels)
                        {
                            if (value <= upperPrice && value >= lowerPrice)
                            {
                                beauty++;
                                if (annotateConfirmations)
                                {
                                     // Create an EllipseAnnotation
                                     EllipseAnnotation dot = new EllipseAnnotation
                                     {
                                         AxisX = chart_OHLCV.ChartAreas[0].AxisX,
                                         AxisY = chart_OHLCV.ChartAreas[0].AxisY,
                                         IsSizeAlwaysRelative = true, 
                                         AnchorDataPoint = dataPoint,
                                         Y = (double)value,     // Attach to the confirmed OHLC position
                                         Width = 0.5,             // Small width for a dot
                                         Height = 1,            // Small height for a dot
                                         BackColor = Color.Yellow, // Color of the dot
                                         ClipToChartArea = "ChartArea_OHLC",
                                     };
                                      
                                      retracementConfirmations.Add(dot);
                                     // Add the annotation to the chart
                                     chart_OHLCV.Annotations.Add(dot);
                                }
                            
                            }
                        }

                    }
                     // Compare and annotate for each OHLC value of the candlestick
                     compareAndAnnotate(cs.Open);
                     compareAndAnnotate(cs.High);
                     compareAndAnnotate(cs.Low);
                     compareAndAnnotate(cs.Close);
            }

            return beauty;
        }

        /// <summary>
        /// Event handler for the end date picker value change. Updates the endDate field.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to value change.</param>
        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            if(!validWaveSelected)
               // Update the end date to newly chosen end date
               endDate = dateTimePicker_endDate.Value;
            else
                MessageBox.Show("Date controls are locked when there is an active Fibonacci retracement on the chart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            dateTimePicker_endDate.Value = endDate;
        }

    }
}
