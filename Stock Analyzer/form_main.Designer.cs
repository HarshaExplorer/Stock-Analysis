namespace Stock_Analyzer
{
    partial class form_main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox_actionPanel = new System.Windows.Forms.GroupBox();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.label_endDate = new System.Windows.Forms.Label();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.label_startDate = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.button_loadStocks = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DataGridViewTextBoxColumn_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn_open = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn_high = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn_low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn_close = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn_volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_actionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_actionPanel
            // 
            this.groupBox_actionPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox_actionPanel.Controls.Add(this.dateTimePicker_endDate);
            this.groupBox_actionPanel.Controls.Add(this.label_endDate);
            this.groupBox_actionPanel.Controls.Add(this.dateTimePicker_startDate);
            this.groupBox_actionPanel.Controls.Add(this.label_startDate);
            this.groupBox_actionPanel.Controls.Add(this.button_update);
            this.groupBox_actionPanel.Controls.Add(this.button_loadStocks);
            this.groupBox_actionPanel.Location = new System.Drawing.Point(32, 29);
            this.groupBox_actionPanel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox_actionPanel.Name = "groupBox_actionPanel";
            this.groupBox_actionPanel.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox_actionPanel.Size = new System.Drawing.Size(900, 465);
            this.groupBox_actionPanel.TabIndex = 0;
            this.groupBox_actionPanel.TabStop = false;
            this.groupBox_actionPanel.Text = "Action Panel";
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(256, 358);
            this.dateTimePicker_endDate.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(604, 41);
            this.dateTimePicker_endDate.TabIndex = 5;
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_endDate.Location = new System.Drawing.Point(16, 358);
            this.label_endDate.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(181, 38);
            this.label_endDate.TabIndex = 4;
            this.label_endDate.Text = "End  Date:";
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(256, 258);
            this.dateTimePicker_startDate.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(604, 41);
            this.dateTimePicker_startDate.TabIndex = 3;
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startDate.Location = new System.Drawing.Point(16, 258);
            this.label_startDate.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(194, 38);
            this.label_startDate.TabIndex = 2;
            this.label_startDate.Text = "Start  Date:";
            // 
            // button_update
            // 
            this.button_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.Location = new System.Drawing.Point(472, 74);
            this.button_update.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(395, 129);
            this.button_update.TabIndex = 1;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            // 
            // button_loadStocks
            // 
            this.button_loadStocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadStocks.Location = new System.Drawing.Point(16, 69);
            this.button_loadStocks.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.button_loadStocks.Name = "button_loadStocks";
            this.button_loadStocks.Size = new System.Drawing.Size(395, 134);
            this.button_loadStocks.TabIndex = 0;
            this.button_loadStocks.Text = "Load Stocks";
            this.button_loadStocks.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn_date,
            this.DataGridViewTextBoxColumn_open,
            this.DataGridViewTextBoxColumn_high,
            this.DataGridViewTextBoxColumn_low,
            this.DataGridViewTextBoxColumn_close,
            this.DataGridViewTextBoxColumn_volume});
            this.dataGridView1.Location = new System.Drawing.Point(974, 47);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(2000, 447);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 102;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.Size = new System.Drawing.Size(2000, 447);
            this.dataGridView1.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(32, 549);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(2942, 729);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // DataGridViewTextBoxColumn_date
            // 
            this.DataGridViewTextBoxColumn_date.HeaderText = "Date";
            this.DataGridViewTextBoxColumn_date.MinimumWidth = 12;
            this.DataGridViewTextBoxColumn_date.Name = "DataGridViewTextBoxColumn_date";
            this.DataGridViewTextBoxColumn_date.ReadOnly = true;
            this.DataGridViewTextBoxColumn_date.Width = 150;
            // 
            // DataGridViewTextBoxColumn_open
            // 
            this.DataGridViewTextBoxColumn_open.HeaderText = "Open";
            this.DataGridViewTextBoxColumn_open.MinimumWidth = 12;
            this.DataGridViewTextBoxColumn_open.Name = "DataGridViewTextBoxColumn_open";
            this.DataGridViewTextBoxColumn_open.ReadOnly = true;
            this.DataGridViewTextBoxColumn_open.Width = 150;
            // 
            // DataGridViewTextBoxColumn_high
            // 
            this.DataGridViewTextBoxColumn_high.HeaderText = "High";
            this.DataGridViewTextBoxColumn_high.MinimumWidth = 12;
            this.DataGridViewTextBoxColumn_high.Name = "DataGridViewTextBoxColumn_high";
            this.DataGridViewTextBoxColumn_high.ReadOnly = true;
            this.DataGridViewTextBoxColumn_high.Width = 150;
            // 
            // DataGridViewTextBoxColumn_low
            // 
            this.DataGridViewTextBoxColumn_low.HeaderText = "Low";
            this.DataGridViewTextBoxColumn_low.MinimumWidth = 12;
            this.DataGridViewTextBoxColumn_low.Name = "DataGridViewTextBoxColumn_low";
            this.DataGridViewTextBoxColumn_low.ReadOnly = true;
            this.DataGridViewTextBoxColumn_low.Width = 150;
            // 
            // DataGridViewTextBoxColumn_close
            // 
            this.DataGridViewTextBoxColumn_close.HeaderText = "Close";
            this.DataGridViewTextBoxColumn_close.MinimumWidth = 12;
            this.DataGridViewTextBoxColumn_close.Name = "DataGridViewTextBoxColumn_close";
            this.DataGridViewTextBoxColumn_close.ReadOnly = true;
            this.DataGridViewTextBoxColumn_close.Width = 150;
            // 
            // DataGridViewTextBoxColumn_volume
            // 
            this.DataGridViewTextBoxColumn_volume.HeaderText = "Volume";
            this.DataGridViewTextBoxColumn_volume.MinimumWidth = 12;
            this.DataGridViewTextBoxColumn_volume.Name = "DataGridViewTextBoxColumn_volume";
            this.DataGridViewTextBoxColumn_volume.ReadOnly = true;
            this.DataGridViewTextBoxColumn_volume.Width = 150;
            // 
            // form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(3025, 1343);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox_actionPanel);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "form_main";
            this.Padding = new System.Windows.Forms.Padding(30);
            this.Text = "Stock Trend Analyzer";
            this.Load += new System.EventHandler(this.form_main_Load);
            this.groupBox_actionPanel.ResumeLayout(false);
            this.groupBox_actionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_actionPanel;
        private System.Windows.Forms.Button button_loadStocks;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Label label_startDate;
        private System.Windows.Forms.Label label_endDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn_open;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn_high;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn_low;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn_close;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn_volume;
    }
}

