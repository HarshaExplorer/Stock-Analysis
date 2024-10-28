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
        private StockLoader stockLoader = null;
        //Binding list of candlesticks bound to DataGridView 
        private BindingList<CandleStick> bindCandlesticks = null;
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
        }

        private void button_loadStocks_Click(object sender, EventArgs e)
        {
            //On button click change text of window form
            Text = "Stock Viewer - Opening Stock File...";
            //On button click display the file explorer
            openFileDialog_stockFilePick.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //On button click change text of window form
            Text = "Stock Viewer - Loading Stock File...";
            processData(openFileDialog_stockFilePick.FileName);
        }

        private void processData(string inputFile)
        {
            candlesticks = stockLoader.LoadStockData(inputFile);
            candlesticks = candlesticks.OrderBy(c => c.Date).ToList();

            bindCandlesticks = new BindingList<CandleStick>(candlesticks);
            dataGridView_stockview.DataSource = bindCandlesticks;

            AdjustChart();
            chart_OHLCV.DataSource = bindCandlesticks;
            chart_OHLCV.DataBind();
            Text = "Stock Viewer - " + Path.GetFileName(inputFile);
        }

        private void AdjustChart()
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
    }
}
