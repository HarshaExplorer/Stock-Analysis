namespace Stock_Analyzer
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_OHLCV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog_stockFilePick = new System.Windows.Forms.OpenFileDialog();
            this.button_loadStocks = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.label_startDate = new System.Windows.Forms.Label();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.label_endDate = new System.Windows.Forms.Label();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.comboBox_patterns = new System.Windows.Forms.ComboBox();
            this.label_pattern = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.chart_Beauty = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_retracement = new System.Windows.Forms.Button();
            this.numericUpDown_retracement_leeway = new System.Windows.Forms.NumericUpDown();
            this.label_percentLeeway = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip_percent_leeway = new System.Windows.Forms.ToolTip(this.components);
            this.candleStickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.formmainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Beauty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_retracement_leeway)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formmainBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_OHLCV
            // 
            chartArea1.AxisX.Title = "Date";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.Title = "Price";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.BorderWidth = 2;
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.AxisX.Title = "Date";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.Title = "Volume";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.BorderWidth = 2;
            chartArea2.Name = "ChartArea_Volume";
            this.chart_OHLCV.ChartAreas.Add(chartArea1);
            this.chart_OHLCV.ChartAreas.Add(chartArea2);
            this.chart_OHLCV.Location = new System.Drawing.Point(44, 248);
            this.chart_OHLCV.Name = "chart_OHLCV";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.IsXValueIndexed = true;
            series1.Name = "Series_OHLC";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "High,Low,Open,Close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_Volume";
            series2.IsXValueIndexed = true;
            series2.Name = "Series_Volume";
            series2.XValueMember = "Date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "Volume";
            this.chart_OHLCV.Series.Add(series1);
            this.chart_OHLCV.Series.Add(series2);
            this.chart_OHLCV.Size = new System.Drawing.Size(3403, 953);
            this.chart_OHLCV.TabIndex = 2;
            this.chart_OHLCV.Text = "chart_OHLCV";
            this.chart_OHLCV.Paint += new System.Windows.Forms.PaintEventHandler(this.chart_OHLCV_Paint);
            this.chart_OHLCV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_OHLCV_MouseDown);
            this.chart_OHLCV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_OHLCV_MouseMove);
            this.chart_OHLCV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_OHLCV_MouseUp);
            // 
            // openFileDialog_stockFilePick
            // 
            this.openFileDialog_stockFilePick.Filter = "CSV files (*.csv)|*.csv|Monthly files|*-Month.csv|Weekly files|*-Week.csv|Daily f" +
    "iles|*-Day.csv";
            this.openFileDialog_stockFilePick.Multiselect = true;
            this.openFileDialog_stockFilePick.Title = "Select a CSV File";
            this.openFileDialog_stockFilePick.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button_loadStocks
            // 
            this.button_loadStocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadStocks.Location = new System.Drawing.Point(1134, 33);
            this.button_loadStocks.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.button_loadStocks.Name = "button_loadStocks";
            this.button_loadStocks.Size = new System.Drawing.Size(395, 134);
            this.button_loadStocks.TabIndex = 0;
            this.button_loadStocks.Text = "Load Stocks";
            this.button_loadStocks.UseVisualStyleBackColor = true;
            this.button_loadStocks.Click += new System.EventHandler(this.button_loadStocks_Click);
            // 
            // button_update
            // 
            this.button_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.Location = new System.Drawing.Point(1590, 38);
            this.button_update.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(406, 129);
            this.button_update.TabIndex = 1;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startDate.Location = new System.Drawing.Point(654, 50);
            this.label_startDate.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(194, 38);
            this.label_startDate.TabIndex = 2;
            this.label_startDate.Text = "Start  Date:";
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(475, 112);
            this.dateTimePicker_startDate.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(604, 41);
            this.dateTimePicker_startDate.TabIndex = 3;
            this.dateTimePicker_startDate.ValueChanged += new System.EventHandler(this.dateTimePicker_startDate_ValueChanged);
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_endDate.Location = new System.Drawing.Point(2230, 50);
            this.label_endDate.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(181, 38);
            this.label_endDate.TabIndex = 4;
            this.label_endDate.Text = "End  Date:";
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(2043, 112);
            this.dateTimePicker_endDate.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(604, 41);
            this.dateTimePicker_endDate.TabIndex = 5;
            this.dateTimePicker_endDate.ValueChanged += new System.EventHandler(this.dateTimePicker_endDate_ValueChanged);
            // 
            // comboBox_patterns
            // 
            this.comboBox_patterns.FormattingEnabled = true;
            this.comboBox_patterns.Location = new System.Drawing.Point(1601, 193);
            this.comboBox_patterns.Name = "comboBox_patterns";
            this.comboBox_patterns.Size = new System.Drawing.Size(269, 39);
            this.comboBox_patterns.TabIndex = 6;
            this.comboBox_patterns.SelectedIndexChanged += new System.EventHandler(this.comboBox_patterns_SelectedIndexChanged);
            // 
            // label_pattern
            // 
            this.label_pattern.AutoSize = true;
            this.label_pattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pattern.Location = new System.Drawing.Point(1272, 193);
            this.label_pattern.Name = "label_pattern";
            this.label_pattern.Size = new System.Drawing.Size(257, 39);
            this.label_pattern.TabIndex = 7;
            this.label_pattern.Text = "Detect Pattern:";
            // 
            // chart_Beauty
            // 
            chartArea3.AxisX.Interval = 1D;
            chartArea3.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea3.AxisX.Title = "Price";
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.Title = "Beauty";
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.Name = "ChartArea_Beauty";
            this.chart_Beauty.ChartAreas.Add(chartArea3);
            this.chart_Beauty.Location = new System.Drawing.Point(44, 1207);
            this.chart_Beauty.Name = "chart_Beauty";
            series3.ChartArea = "ChartArea_Beauty";
            series3.IsVisibleInLegend = false;
            series3.IsXValueIndexed = true;
            series3.Name = "Series_Fibonacci_Beauty";
            this.chart_Beauty.Series.Add(series3);
            this.chart_Beauty.Size = new System.Drawing.Size(3403, 545);
            this.chart_Beauty.TabIndex = 8;
            this.chart_Beauty.Text = "chart_beauty";
            // 
            // button_retracement
            // 
            this.button_retracement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_retracement.Location = new System.Drawing.Point(2891, 20);
            this.button_retracement.Name = "button_retracement";
            this.button_retracement.Size = new System.Drawing.Size(568, 147);
            this.button_retracement.TabIndex = 10;
            this.button_retracement.Text = "Fibonacci Retracement: Off";
            this.button_retracement.UseVisualStyleBackColor = true;
            this.button_retracement.Click += new System.EventHandler(this.button_retracement_Click);
            // 
            // numericUpDown_retracement_leeway
            // 
            this.numericUpDown_retracement_leeway.DecimalPlaces = 1;
            this.numericUpDown_retracement_leeway.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_retracement_leeway.Location = new System.Drawing.Point(3256, 193);
            this.numericUpDown_retracement_leeway.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_retracement_leeway.Name = "numericUpDown_retracement_leeway";
            this.numericUpDown_retracement_leeway.Size = new System.Drawing.Size(195, 38);
            this.numericUpDown_retracement_leeway.TabIndex = 11;
            this.toolTip_percent_leeway.SetToolTip(this.numericUpDown_retracement_leeway, "Click Update after done.");
            this.numericUpDown_retracement_leeway.ValueChanged += new System.EventHandler(this.numericUpDown_retracement_leeway_ValueChanged);
            // 
            // label_percentLeeway
            // 
            this.label_percentLeeway.AutoSize = true;
            this.label_percentLeeway.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_percentLeeway.Location = new System.Drawing.Point(2901, 190);
            this.label_percentLeeway.Name = "label_percentLeeway";
            this.label_percentLeeway.Size = new System.Drawing.Size(285, 39);
            this.label_percentLeeway.TabIndex = 12;
            this.label_percentLeeway.Text = "Percent Leeway:";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(29, 31);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 1713);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // toolTip_percent_leeway
            // 
            this.toolTip_percent_leeway.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip_percent_leeway.ToolTipTitle = "Max 25%";
            // 
            // candleStickBindingSource
            // 
            this.candleStickBindingSource.DataSource = typeof(Stock_Analyzer.CandleStick);
            // 
            // formmainBindingSource
            // 
            this.formmainBindingSource.DataSource = typeof(Stock_Analyzer.Form_Main);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(3491, 1775);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.label_percentLeeway);
            this.Controls.Add(this.numericUpDown_retracement_leeway);
            this.Controls.Add(this.button_retracement);
            this.Controls.Add(this.chart_Beauty);
            this.Controls.Add(this.label_pattern);
            this.Controls.Add(this.comboBox_patterns);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Controls.Add(this.label_endDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Controls.Add(this.button_loadStocks);
            this.Controls.Add(this.label_startDate);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.chart_OHLCV);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Form_Main";
            this.Padding = new System.Windows.Forms.Padding(29, 31, 29, 31);
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Stock Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Beauty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_retracement_leeway)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formmainBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_OHLCV;
        private System.Windows.Forms.OpenFileDialog openFileDialog_stockFilePick;
        private System.Windows.Forms.BindingSource candleStickBindingSource;
        private System.Windows.Forms.BindingSource formmainBindingSource;
        private System.Windows.Forms.Button button_loadStocks;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Label label_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.Label label_endDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.ComboBox comboBox_patterns;
        private System.Windows.Forms.Label label_pattern;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Beauty;
        private System.Windows.Forms.Button button_retracement;
        private System.Windows.Forms.NumericUpDown numericUpDown_retracement_leeway;
        private System.Windows.Forms.Label label_percentLeeway;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolTip toolTip_percent_leeway;
    }
}

