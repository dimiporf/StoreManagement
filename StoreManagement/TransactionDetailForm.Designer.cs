namespace StoreManagement
{
    partial class TransactionDetailForm
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
            dateLabel = new Label();
            transactionTypeLabel = new Label();
            warehouseLabel = new Label();
            inventoryItemLabel = new Label();
            quantityLabel = new Label();
            costItemLabel = new Label();
            salePriceItemLabel = new Label();
            totalCostLabel = new Label();
            totalSalesLabel = new Label();
            dateTimePickerDetail = new DateTimePicker();
            transactionTypeComboBox = new ComboBox();
            warehouseComboBox = new ComboBox();
            inventoryItemComboBox = new ComboBox();
            quantityTextBox = new TextBox();
            costItemTextBox = new TextBox();
            salePriceItemTextBox = new TextBox();
            totalCostTextBox = new TextBox();
            totalSalesTextBox = new TextBox();
            saveButton = new Button();
            deleteButton = new Button();
            SuspendLayout();
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Font = new Font("Segoe UI", 11.25F);
            dateLabel.Location = new Point(12, 40);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(41, 20);
            dateLabel.TabIndex = 0;
            dateLabel.Text = "Date";
            // 
            // transactionTypeLabel
            // 
            transactionTypeLabel.AutoSize = true;
            transactionTypeLabel.Font = new Font("Segoe UI", 11.25F);
            transactionTypeLabel.Location = new Point(12, 76);
            transactionTypeLabel.Name = "transactionTypeLabel";
            transactionTypeLabel.Size = new Size(119, 20);
            transactionTypeLabel.TabIndex = 1;
            transactionTypeLabel.Text = "Transaction Type";
            // 
            // warehouseLabel
            // 
            warehouseLabel.AutoSize = true;
            warehouseLabel.Font = new Font("Segoe UI", 11.25F);
            warehouseLabel.Location = new Point(12, 117);
            warehouseLabel.Name = "warehouseLabel";
            warehouseLabel.Size = new Size(82, 20);
            warehouseLabel.TabIndex = 2;
            warehouseLabel.Text = "Warehouse";
            // 
            // inventoryItemLabel
            // 
            inventoryItemLabel.AutoSize = true;
            inventoryItemLabel.Font = new Font("Segoe UI", 11.25F);
            inventoryItemLabel.Location = new Point(12, 154);
            inventoryItemLabel.Name = "inventoryItemLabel";
            inventoryItemLabel.Size = new Size(104, 20);
            inventoryItemLabel.TabIndex = 3;
            inventoryItemLabel.Text = "Inventory Item";
            // 
            // quantityLabel
            // 
            quantityLabel.AutoSize = true;
            quantityLabel.Font = new Font("Segoe UI", 11.25F);
            quantityLabel.Location = new Point(12, 186);
            quantityLabel.Name = "quantityLabel";
            quantityLabel.Size = new Size(65, 20);
            quantityLabel.TabIndex = 4;
            quantityLabel.Text = "Quantity";
            // 
            // costItemLabel
            // 
            costItemLabel.AutoSize = true;
            costItemLabel.Font = new Font("Segoe UI", 11.25F);
            costItemLabel.Location = new Point(12, 218);
            costItemLabel.Name = "costItemLabel";
            costItemLabel.Size = new Size(82, 20);
            costItemLabel.TabIndex = 5;
            costItemLabel.Text = "Cost / Item";
            // 
            // salePriceItemLabel
            // 
            salePriceItemLabel.AutoSize = true;
            salePriceItemLabel.Font = new Font("Segoe UI", 11.25F);
            salePriceItemLabel.Location = new Point(12, 250);
            salePriceItemLabel.Name = "salePriceItemLabel";
            salePriceItemLabel.Size = new Size(117, 20);
            salePriceItemLabel.TabIndex = 6;
            salePriceItemLabel.Text = "Sale Price / Item";
            // 
            // totalCostLabel
            // 
            totalCostLabel.AutoSize = true;
            totalCostLabel.Font = new Font("Segoe UI", 11.25F);
            totalCostLabel.Location = new Point(12, 284);
            totalCostLabel.Name = "totalCostLabel";
            totalCostLabel.Size = new Size(75, 20);
            totalCostLabel.TabIndex = 7;
            totalCostLabel.Text = "Total Cost";
            // 
            // totalSalesLabel
            // 
            totalSalesLabel.AutoSize = true;
            totalSalesLabel.Font = new Font("Segoe UI", 11.25F);
            totalSalesLabel.Location = new Point(12, 322);
            totalSalesLabel.Name = "totalSalesLabel";
            totalSalesLabel.Size = new Size(80, 20);
            totalSalesLabel.TabIndex = 8;
            totalSalesLabel.Text = "Total Sales";
            // 
            // dateTimePickerDetail
            // 
            dateTimePickerDetail.Location = new Point(212, 38);
            dateTimePickerDetail.Name = "dateTimePickerDetail";
            dateTimePickerDetail.Size = new Size(200, 23);
            dateTimePickerDetail.TabIndex = 9;
            // 
            // transactionTypeComboBox
            // 
            transactionTypeComboBox.FormattingEnabled = true;
            transactionTypeComboBox.Location = new Point(212, 77);
            transactionTypeComboBox.Name = "transactionTypeComboBox";
            transactionTypeComboBox.Size = new Size(200, 23);
            transactionTypeComboBox.TabIndex = 10;
            transactionTypeComboBox.SelectedIndexChanged += TransactionTypeComboBox_SelectedIndexChanged;
            // 
            // warehouseComboBox
            // 
            warehouseComboBox.FormattingEnabled = true;
            warehouseComboBox.Location = new Point(212, 114);
            warehouseComboBox.Name = "warehouseComboBox";
            warehouseComboBox.Size = new Size(200, 23);
            warehouseComboBox.TabIndex = 11;
            // 
            // inventoryItemComboBox
            // 
            inventoryItemComboBox.FormattingEnabled = true;
            inventoryItemComboBox.Location = new Point(212, 151);
            inventoryItemComboBox.Name = "inventoryItemComboBox";
            inventoryItemComboBox.Size = new Size(200, 23);
            inventoryItemComboBox.TabIndex = 12;
            // 
            // quantityTextBox
            // 
            quantityTextBox.Location = new Point(212, 187);
            quantityTextBox.Name = "quantityTextBox";
            quantityTextBox.Size = new Size(200, 23);
            quantityTextBox.TabIndex = 13;
            // 
            // costItemTextBox
            // 
            costItemTextBox.Location = new Point(212, 219);
            costItemTextBox.Name = "costItemTextBox";
            costItemTextBox.Size = new Size(200, 23);
            costItemTextBox.TabIndex = 14;
            // 
            // salePriceItemTextBox
            // 
            salePriceItemTextBox.Location = new Point(212, 251);
            salePriceItemTextBox.Name = "salePriceItemTextBox";
            salePriceItemTextBox.Size = new Size(200, 23);
            salePriceItemTextBox.TabIndex = 15;
            // 
            // totalCostTextBox
            // 
            totalCostTextBox.Location = new Point(212, 285);
            totalCostTextBox.Name = "totalCostTextBox";
            totalCostTextBox.Size = new Size(200, 23);
            totalCostTextBox.TabIndex = 16;
            // 
            // totalSalesTextBox
            // 
            totalSalesTextBox.Location = new Point(212, 323);
            totalSalesTextBox.Name = "totalSalesTextBox";
            totalSalesTextBox.Size = new Size(200, 23);
            totalSalesTextBox.TabIndex = 17;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(12, 407);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(112, 31);
            saveButton.TabIndex = 18;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(130, 407);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(112, 31);
            deleteButton.TabIndex = 19;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // TransactionDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(deleteButton);
            Controls.Add(saveButton);
            Controls.Add(totalSalesTextBox);
            Controls.Add(totalCostTextBox);
            Controls.Add(salePriceItemTextBox);
            Controls.Add(costItemTextBox);
            Controls.Add(quantityTextBox);
            Controls.Add(inventoryItemComboBox);
            Controls.Add(warehouseComboBox);
            Controls.Add(transactionTypeComboBox);
            Controls.Add(dateTimePickerDetail);
            Controls.Add(totalSalesLabel);
            Controls.Add(totalCostLabel);
            Controls.Add(salePriceItemLabel);
            Controls.Add(costItemLabel);
            Controls.Add(quantityLabel);
            Controls.Add(inventoryItemLabel);
            Controls.Add(warehouseLabel);
            Controls.Add(transactionTypeLabel);
            Controls.Add(dateLabel);
            Name = "TransactionDetailForm";
            Text = "TransactionDetailForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label dateLabel;
        private Label transactionTypeLabel;
        private Label warehouseLabel;
        private Label inventoryItemLabel;
        private Label quantityLabel;
        private Label costItemLabel;
        private Label salePriceItemLabel;
        private Label totalCostLabel;
        private Label totalSalesLabel;
        private DateTimePicker dateTimePickerDetail;
        private ComboBox transactionTypeComboBox;
        private ComboBox warehouseComboBox;
        private ComboBox inventoryItemComboBox;
        private TextBox quantityTextBox;
        private TextBox costItemTextBox;
        private TextBox salePriceItemTextBox;
        private TextBox totalCostTextBox;
        private TextBox totalSalesTextBox;
        private Button saveButton;
        private Button deleteButton;
    }
}