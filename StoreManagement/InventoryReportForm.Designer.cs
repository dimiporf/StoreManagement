namespace StoreManagement
{
    partial class InventoryReportForm
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
            this.dateFromPicker = new System.Windows.Forms.DateTimePicker();
            this.dateToPicker = new System.Windows.Forms.DateTimePicker();
            this.warehouseComboBox = new System.Windows.Forms.ComboBox();
            this.dateFromLabel = new System.Windows.Forms.Label();
            this.dateToLabel = new System.Windows.Forms.Label();
            this.warehouseLabel = new System.Windows.Forms.Label();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.generateReportButton = new System.Windows.Forms.Button(); // Added generateReportButton
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.SuspendLayout();
            // 
            // dateFromPicker
            // 
            this.dateFromPicker.Location = new System.Drawing.Point(12, 44);
            this.dateFromPicker.Name = "dateFromPicker";
            this.dateFromPicker.Size = new System.Drawing.Size(225, 23);
            this.dateFromPicker.TabIndex = 0;
            // 
            // dateToPicker
            // 
            this.dateToPicker.Location = new System.Drawing.Point(12, 111);
            this.dateToPicker.Name = "dateToPicker";
            this.dateToPicker.Size = new System.Drawing.Size(225, 23);
            this.dateToPicker.TabIndex = 1;
            // 
            // warehouseComboBox
            // 
            this.warehouseComboBox.FormattingEnabled = true;
            this.warehouseComboBox.Location = new System.Drawing.Point(12, 184);
            this.warehouseComboBox.Name = "warehouseComboBox";
            this.warehouseComboBox.Size = new System.Drawing.Size(225, 23);
            this.warehouseComboBox.TabIndex = 2;
            // 
            // dateFromLabel
            // 
            this.dateFromLabel.AutoSize = true;
            this.dateFromLabel.Location = new System.Drawing.Point(12, 26);
            this.dateFromLabel.Name = "dateFromLabel";
            this.dateFromLabel.Size = new System.Drawing.Size(35, 15);
            this.dateFromLabel.TabIndex = 3;
            this.dateFromLabel.Text = "From";
            // 
            // dateToLabel
            // 
            this.dateToLabel.AutoSize = true;
            this.dateToLabel.Location = new System.Drawing.Point(12, 93);
            this.dateToLabel.Name = "dateToLabel";
            this.dateToLabel.Size = new System.Drawing.Size(19, 15);
            this.dateToLabel.TabIndex = 4;
            this.dateToLabel.Text = "To";
            // 
            // warehouseLabel
            // 
            this.warehouseLabel.AutoSize = true;
            this.warehouseLabel.Location = new System.Drawing.Point(12, 166);
            this.warehouseLabel.Name = "warehouseLabel";
            this.warehouseLabel.Size = new System.Drawing.Size(66, 15);
            this.warehouseLabel.TabIndex = 5;
            this.warehouseLabel.Text = "Warehouse";
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Location = new System.Drawing.Point(256, 26);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.Size = new System.Drawing.Size(743, 447);
            this.dataGridViewReport.TabIndex = 6;
            // 
            // generateReportButton
            // 
            this.generateReportButton.Location = new System.Drawing.Point(12, 240); // Adjusted position
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(225, 30);
            this.generateReportButton.TabIndex = 7;
            this.generateReportButton.Text = "Generate Report";
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.generateReportButton_Click); // Attach event handler
            // 
            // InventoryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 485);
            this.Controls.Add(this.generateReportButton); // Add generateReportButton to form
            this.Controls.Add(this.dataGridViewReport);
            this.Controls.Add(this.warehouseLabel);
            this.Controls.Add(this.dateToLabel);
            this.Controls.Add(this.dateFromLabel);
            this.Controls.Add(this.warehouseComboBox);
            this.Controls.Add(this.dateToPicker);
            this.Controls.Add(this.dateFromPicker);
            this.Name = "InventoryReportForm";
            this.Text = "InventoryReportForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateFromPicker;
        private System.Windows.Forms.DateTimePicker dateToPicker;
        private System.Windows.Forms.ComboBox warehouseComboBox;
        private System.Windows.Forms.Label dateFromLabel;
        private System.Windows.Forms.Label dateToLabel;
        private System.Windows.Forms.Label warehouseLabel;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.Button generateReportButton; // Added generateReportButton
    }
}
