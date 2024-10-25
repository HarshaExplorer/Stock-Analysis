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
            this.groupBox_actionPanel = new System.Windows.Forms.GroupBox();
            this.button_loadStocks = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.label_startDate = new System.Windows.Forms.Label();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.label_endDate = new System.Windows.Forms.Label();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox_actionPanel.SuspendLayout();
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
            this.groupBox_actionPanel.Location = new System.Drawing.Point(12, 12);
            this.groupBox_actionPanel.Name = "groupBox_actionPanel";
            this.groupBox_actionPanel.Size = new System.Drawing.Size(378, 195);
            this.groupBox_actionPanel.TabIndex = 0;
            this.groupBox_actionPanel.TabStop = false;
            this.groupBox_actionPanel.Text = "Action Panel";
            // 
            // button_loadStocks
            // 
            this.button_loadStocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadStocks.Location = new System.Drawing.Point(6, 29);
            this.button_loadStocks.Name = "button_loadStocks";
            this.button_loadStocks.Size = new System.Drawing.Size(148, 56);
            this.button_loadStocks.TabIndex = 0;
            this.button_loadStocks.Text = "Load Stocks";
            this.button_loadStocks.UseVisualStyleBackColor = true;
            // 
            // button_update
            // 
            this.button_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.Location = new System.Drawing.Point(177, 31);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(148, 54);
            this.button_update.TabIndex = 1;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startDate.Location = new System.Drawing.Point(6, 108);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(84, 16);
            this.label_startDate.TabIndex = 2;
            this.label_startDate.Text = "Start  Date:";
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(96, 108);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(229, 21);
            this.dateTimePicker_startDate.TabIndex = 3;
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_endDate.Location = new System.Drawing.Point(6, 150);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(79, 16);
            this.label_endDate.TabIndex = 4;
            this.label_endDate.Text = "End  Date:";
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(96, 150);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(229, 21);
            this.dateTimePicker_endDate.TabIndex = 5;
            // 
            // form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(978, 511);
            this.Controls.Add(this.groupBox_actionPanel);
            this.Name = "form_main";
            this.Text = "Stock Trend Analyzer";
            this.Load += new System.EventHandler(this.form_main_Load);
            this.groupBox_actionPanel.ResumeLayout(false);
            this.groupBox_actionPanel.PerformLayout();
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
    }
}

