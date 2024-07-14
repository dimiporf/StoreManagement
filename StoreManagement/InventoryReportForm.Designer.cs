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
            dateFromPicker = new DateTimePicker();
            dateToPicker = new DateTimePicker();
            warehouseComboBox = new ComboBox();
            dateFromLabel = new Label();
            dateToLabel = new Label();
            warehouseLabel = new Label();
            dataGridViewReport = new DataGridView();
            generateReportButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).BeginInit();
            SuspendLayout();
            // 
            // dateFromPicker
            // 
            dateFromPicker.Location = new Point(12, 44);
            dateFromPicker.Name = "dateFromPicker";
            dateFromPicker.Size = new Size(225, 23);
            dateFromPicker.TabIndex = 0;
            // 
            // dateToPicker
            // 
            dateToPicker.Location = new Point(12, 111);
            dateToPicker.Name = "dateToPicker";
            dateToPicker.Size = new Size(225, 23);
            dateToPicker.TabIndex = 1;
            // 
            // warehouseComboBox
            // 
            warehouseComboBox.FormattingEnabled = true;
            warehouseComboBox.Location = new Point(12, 184);
            warehouseComboBox.Name = "warehouseComboBox";
            warehouseComboBox.Size = new Size(225, 23);
            warehouseComboBox.TabIndex = 2;
            // 
            // dateFromLabel
            // 
            dateFromLabel.AutoSize = true;
            dateFromLabel.Location = new Point(12, 26);
            dateFromLabel.Name = "dateFromLabel";
            dateFromLabel.Size = new Size(35, 15);
            dateFromLabel.TabIndex = 3;
            dateFromLabel.Text = "From";
            // 
            // dateToLabel
            // 
            dateToLabel.AutoSize = true;
            dateToLabel.Location = new Point(12, 93);
            dateToLabel.Name = "dateToLabel";
            dateToLabel.Size = new Size(19, 15);
            dateToLabel.TabIndex = 4;
            dateToLabel.Text = "To";
            // 
            // warehouseLabel
            // 
            warehouseLabel.AutoSize = true;
            warehouseLabel.Location = new Point(12, 166);
            warehouseLabel.Name = "warehouseLabel";
            warehouseLabel.Size = new Size(66, 15);
            warehouseLabel.TabIndex = 5;
            warehouseLabel.Text = "Warehouse";
            // 
            // dataGridViewReport
            // 
            dataGridViewReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport.Location = new Point(256, 26);
            dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewReport.Size = new Size(683, 447);
            dataGridViewReport.TabIndex = 6;
            // 
            // generateReportButton
            // 
            generateReportButton.Location = new Point(12, 240);
            generateReportButton.Name = "generateReportButton";
            generateReportButton.Size = new Size(225, 30);
            generateReportButton.TabIndex = 7;
            generateReportButton.Text = "Generate Report";
            generateReportButton.UseVisualStyleBackColor = true;
            generateReportButton.Click += generateReportButton_Click;
            // 
            // InventoryReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(949, 485);
            Controls.Add(generateReportButton);
            Controls.Add(dataGridViewReport);
            Controls.Add(warehouseLabel);
            Controls.Add(dateToLabel);
            Controls.Add(dateFromLabel);
            Controls.Add(warehouseComboBox);
            Controls.Add(dateToPicker);
            Controls.Add(dateFromPicker);
            Name = "InventoryReportForm";
            Text = "InventoryReportForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
