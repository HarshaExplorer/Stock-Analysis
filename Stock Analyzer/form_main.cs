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
        //Binding list of candlesticks bound to DataGridView 
        private BindingList<CandleStick> bindCandlesticks = null;
        private List<CandleStick> filteredCandlesticks = null;
        private StockLoader stockLoader = null;
        private DateTime startDate, endDate;
        private String CurrentInputFileName = null;

        public form_main()
        {
            InitializeComponent();
        }

        private void form_main_Load(object sender, EventArgs e)
        {
            candlesticks = new List<CandleStick>(1500);
            stockLoader = new StockLoader();
            openFileDialog_stockFilePick.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog_stockFilePick.Title = "Select a CSV File";
            preselectDates();
        }

        private void button_loadStocks_Click(object sender, EventArgs e)
        {
            //On button click change text of window form
            Text = "Stock Viewer - Opening Stock File...";
            //On button click display the file explorer
            DialogResult dialogResult = openFileDialog_stockFilePick.ShowDialog();

            if(dialogResult == DialogResult.Cancel)
                Text = "Stock Viewer" + (CurrentInputFileName != null ? (" - "+CurrentInputFileName):(""));
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            processData(openFileDialog_stockFilePick.FileName);
        }

        private void processData(string inputFile)
        {
            List<CandleStick> newCandlesticks = stockLoader.LoadStockData(inputFile);

            if (newCandlesticks.Count > 0)
            {
                candlesticks = newCandlesticks.OrderBy(c => c.Date).ToList();
                filteredCandlesticks = filterCandlesticksByDate();
                bindCandlesticks = new BindingList<CandleStick>(filteredCandlesticks);

                adjustChart();
                bindCandlestickData();

                CurrentInputFileName = Path.GetFileName(inputFile);
                Text = "Stock Viewer - " + CurrentInputFileName;
            }
            else
                Text = "Stock Viewer" + (CurrentInputFileName != null ? (" - " + CurrentInputFileName) : (""));

        }

        private void bindCandlestickData()
        {
            dataGridView_stockview.DataSource = bindCandlesticks;
            chart_OHLCV.DataSource = bindCandlesticks;
            chart_OHLCV.DataBind();
        }
        private List<CandleStick> filterCandlesticksByDate()
        {
            List<CandleStick> filteredCandleSticks = new List<CandleStick>(candlesticks.Count);

            foreach(CandleStick c in candlesticks)
                if(c.Date >= startDate && c.Date <= endDate)
                    filteredCandleSticks.Add(c);

            return filteredCandleSticks;
        }

        private void adjustChart()
        {
      
            decimal min = bindCandlesticks.First().Low, max = 0;

            foreach (CandleStick c in bindCandlesticks)
            { 
                if (c.Low < min) 
                    min = c.Low;
                if (c.High > max) 
                    max = c.High;
            }
            //Set the Y axis of the chart area to (+-)2% of the ranges rounded to 2 decimal places
            chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = Math.Floor(Decimal.ToDouble(min) * 0.98);
            chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = Math.Ceiling(Decimal.ToDouble(max) * 1.02);
        }

        private void preselectDates()
        {
            startDate = new DateTime(2020, 01, 01);
            endDate = DateTime.Now;

            dateTimePicker_startDate.Value = startDate;
            dateTimePicker_endDate.Value = endDate;
        }
        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePicker_endDate.Value;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (candlesticks.Count == 0)
                MessageBox.Show("Load stock data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (startDate >= endDate)
                MessageBox.Show("Start date must be earlier than end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                filteredCandlesticks = filterCandlesticksByDate();
                bindCandlesticks = new BindingList<CandleStick>(filteredCandlesticks);

                adjustChart();
                bindCandlestickData();
            }
        }

        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePicker_startDate.Value;
        }

    }
}
