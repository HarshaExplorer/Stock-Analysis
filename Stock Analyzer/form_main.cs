using System;
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
            candlesticks = stockLoader.LoadStockData(openFileDialog_stockFilePick.FileName);
            bindCandlesticks = new BindingList<CandleStick>(candlesticks);
            dataGridView_stockview.DataSource = bindCandlesticks;
            
        }
    }
}
