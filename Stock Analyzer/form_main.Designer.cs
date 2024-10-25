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
            this.groupBox_actionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_actionPanel
            // 
            this.groupBox_actionPanel.Controls.Add(this.button_update);
            this.groupBox_actionPanel.Controls.Add(this.button_loadStocks);
            this.groupBox_actionPanel.Location = new System.Drawing.Point(12, 12);
            this.groupBox_actionPanel.Name = "groupBox_actionPanel";
            this.groupBox_actionPanel.Size = new System.Drawing.Size(378, 205);
            this.groupBox_actionPanel.TabIndex = 0;
            this.groupBox_actionPanel.TabStop = false;
            this.groupBox_actionPanel.Text = "Action Panel";
            // 
            // button_loadStocks
            // 
            this.button_loadStocks.Location = new System.Drawing.Point(6, 29);
            this.button_loadStocks.Name = "button_loadStocks";
            this.button_loadStocks.Size = new System.Drawing.Size(103, 56);
            this.button_loadStocks.TabIndex = 0;
            this.button_loadStocks.Text = "Load Stocks";
            this.button_loadStocks.UseVisualStyleBackColor = true;
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(124, 31);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(102, 54);
            this.button_update.TabIndex = 1;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            // 
            // form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 511);
            this.Controls.Add(this.groupBox_actionPanel);
            this.Name = "form_main";
            this.Text = "Stock Trend Analyzer";
            this.Load += new System.EventHandler(this.form_main_Load);
            this.groupBox_actionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_actionPanel;
        private System.Windows.Forms.Button button_loadStocks;
        private System.Windows.Forms.Button button_update;
    }
}

