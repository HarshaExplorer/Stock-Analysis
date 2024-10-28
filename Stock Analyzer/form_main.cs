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

namespace Stock_Analyzer
{
    public partial class form_main : Form
    {

        //List of all candlesticks read from file
        private List<CandleStick> candlesticks = null;
        /// Binding list of candlesticks bound to DataGridView & chart_OHLCV for data/graph display
        private BindingList<CandleStick> bindCandlesticks = null;
        // List to store filtered candlestick data based on date range
        private List<CandleStick> filteredCandlesticks = null;
        // Instance of StockLoader for loading stock data from file
        private StockLoader stockLoader = null;
        // Variables for storing start and end dates for filtering
        private DateTime startDate, endDate;
        // Stores the name of the currently loaded file
        private String CurrentInputFileName = null;

        /// <summary>
        /// Initializes a new instance of the form_main class.
        /// </summary>
        public form_main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for form load event. Initializes data and pre-selects default dates.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to form loading.</param>
        private void form_main_Load(object sender, EventArgs e)
        {
            candlesticks = new List<CandleStick>(1500);
            stockLoader = new StockLoader();
            openFileDialog_stockFilePick.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog_stockFilePick.Title = "Select a CSV File";
            preselectDates();
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

            if(dialogResult == DialogResult.Cancel)
                Text = "Stock Viewer" + (CurrentInputFileName != null ? (" - "+CurrentInputFileName):(""));
        }

        /// <summary>
        /// Event handler for confirming file selection. Processes the chosen file.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to file selection.</param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            processData(openFileDialog_stockFilePick.FileName);
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
                candlesticks = newCandlesticks.OrderBy(c => c.Date).ToList();
                filteredCandlesticks = filterCandlesticksByDate();
                bindCandlesticks = new BindingList<CandleStick>(filteredCandlesticks);
                
                // Adjust chart settings based on data
                adjustChart();
                // Bind data to UI controls
                bindCandlestickData();

                // Update the form title with the loaded file name
                CurrentInputFileName = Path.GetFileName(inputFile);
                Text = "Stock Viewer - " + CurrentInputFileName;
            }
            else
                Text = "Stock Viewer" + (CurrentInputFileName != null ? (" - " + CurrentInputFileName) : (""));

        }

        /// <summary>
        /// Binds the candlestick data to the DataGridView and chart controls.
        /// </summary>
        private void bindCandlestickData()
        {
            dataGridView_stockview.DataSource = bindCandlesticks;
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
            startDate = new DateTime(2020, 01, 01); // Default start date
            endDate = DateTime.Now; // Default end date

            // Set date pickers to the initial date range
            dateTimePicker_startDate.Value = startDate;
            dateTimePicker_endDate.Value = endDate;
        }

        /// <summary>
        /// Event handler for the update button click. Refreshes the display based on the date filter.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to button click.</param>
        private void button_update_Click(object sender, EventArgs e)
        {
            if (candlesticks.Count == 0)
                MessageBox.Show("Load stock data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (validateFilterDates())
            {
                filteredCandlesticks = filterCandlesticksByDate();
                bindCandlesticks = new BindingList<CandleStick>(filteredCandlesticks);

                adjustChart(); // Adjust chart settings
                bindCandlestickData(); // Bind filtered data to UI controls
            }
        }

        /// <summary>
        /// Validates the date range for filtering, ensuring the start date is earlier than the end date.
        /// </summary>
        /// <returns>True if the date range is valid; otherwise, false.</returns>
        private bool validateFilterDates()
        {
            if (startDate >= endDate)
                MessageBox.Show("Start date must be earlier than end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                return true;

            return false;
        }

        /// <summary>
        /// Event handler for the start date picker value change. Updates the startDate field.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to value change.</param>
        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePicker_startDate.Value;
        }

        /// <summary>
        /// Event handler for the end date picker value change. Updates the endDate field.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data related to value change.</param>
        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePicker_endDate.Value;
        }

    }
}
