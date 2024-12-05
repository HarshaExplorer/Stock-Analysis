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
                chart_OHLCV.Annotations.Clear();

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
            else if (validateFilterDates())
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
            // Update the start date to newly chosen start date
            startDate = dateTimePicker_startDate.Value;
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
            chart_OHLCV.Annotations.Clear();
            // Resize the chart axes and redraw
            chart_OHLCV.Invalidate();
            chart_OHLCV.Update();

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
        /// Event handler for the end date picker value change. Updates the endDate field.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to value change.</param>
        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            // Update the end date to newly chosen end date
            endDate = dateTimePicker_endDate.Value;
        }

    }
}
