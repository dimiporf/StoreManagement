namespace StoreManagement
{
    partial class TransactionListForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            transactionDateTimePicker = new DateTimePicker();
            transactionDataGrid = new DataGridView();
            addNewTransactionBtn = new Button();
            retrieveTransactionsBtn = new Button();
            TransactionID = new DataGridViewTextBoxColumn();
            TransactionDate = new DataGridViewTextBoxColumn();
            TransactionType = new DataGridViewTextBoxColumn();
            WarehouseID = new DataGridViewTextBoxColumn();
            InventoryItemID = new DataGridViewTextBoxColumn();
            Qty = new DataGridViewTextBoxColumn();
            Cost = new DataGridViewTextBoxColumn();
            SalePrice = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)transactionDataGrid).BeginInit();
            SuspendLayout();
            // 
            // transactionDateTimePicker
            // 
            transactionDateTimePicker.Location = new Point(12, 12);
            transactionDateTimePicker.Name = "transactionDateTimePicker";
            transactionDateTimePicker.Size = new Size(255, 23);
            transactionDateTimePicker.TabIndex = 0;
            // 
            // transactionDataGrid
            // 
            transactionDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transactionDataGrid.Columns.AddRange(new DataGridViewColumn[] { TransactionID, TransactionDate, TransactionType, WarehouseID, InventoryItemID, Qty, Cost, SalePrice });
            transactionDataGrid.Location = new Point(12, 41);
            transactionDataGrid.Name = "transactionDataGrid";
            transactionDataGrid.Size = new Size(844, 358);
            transactionDataGrid.TabIndex = 1;
            // 
            // addNewTransactionBtn
            // 
            addNewTransactionBtn.Location = new Point(580, 405);
            addNewTransactionBtn.Name = "addNewTransactionBtn";
            addNewTransactionBtn.Size = new Size(139, 33);
            addNewTransactionBtn.TabIndex = 2;
            addNewTransactionBtn.Text = "Add New Transaction";
            addNewTransactionBtn.UseVisualStyleBackColor = true;
            // 
            // retrieveTransactionsBtn
            // 
            retrieveTransactionsBtn.Location = new Point(725, 405);
            retrieveTransactionsBtn.Name = "retrieveTransactionsBtn";
            retrieveTransactionsBtn.Size = new Size(125, 33);
            retrieveTransactionsBtn.TabIndex = 3;
            retrieveTransactionsBtn.Text = "Retrieve Transactions";
            retrieveTransactionsBtn.UseVisualStyleBackColor = true;
            // 
            // TransactionID
            // 
            TransactionID.HeaderText = "Transaction ID";
            TransactionID.Name = "TransactionID";
            // 
            // TransactionDate
            // 
            TransactionDate.HeaderText = "Transaction Date";
            TransactionDate.Name = "TransactionDate";
            // 
            // TransactionType
            // 
            TransactionType.HeaderText = "Transaction Type";
            TransactionType.Name = "TransactionType";
            // 
            // WarehouseID
            // 
            WarehouseID.HeaderText = "Warehouse ID";
            WarehouseID.Name = "WarehouseID";
            // 
            // InventoryItemID
            // 
            InventoryItemID.HeaderText = "Inventory Item ID";
            InventoryItemID.Name = "InventoryItemID";
            // 
            // Qty
            // 
            Qty.HeaderText = "Quantity";
            Qty.Name = "Qty";
            // 
            // Cost
            // 
            Cost.HeaderText = "Cost";
            Cost.Name = "Cost";
            // 
            // SalePrice
            // 
            SalePrice.HeaderText = "Sale Price";
            SalePrice.Name = "SalePrice";
            // 
            // TransactionListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(862, 450);
            Controls.Add(retrieveTransactionsBtn);
            Controls.Add(addNewTransactionBtn);
            Controls.Add(transactionDataGrid);
            Controls.Add(transactionDateTimePicker);
            Name = "TransactionListForm";
            Text = "Transaction List";
            ((System.ComponentModel.ISupportInitialize)transactionDataGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker transactionDateTimePicker;
        private DataGridView transactionDataGrid;
        private Button addNewTransactionBtn;
        private Button retrieveTransactionsBtn;
        private DataGridViewTextBoxColumn TransactionID;
        private DataGridViewTextBoxColumn TransactionDate;
        private DataGridViewTextBoxColumn TransactionType;
        private DataGridViewTextBoxColumn WarehouseID;
        private DataGridViewTextBoxColumn InventoryItemID;
        private DataGridViewTextBoxColumn Qty;
        private DataGridViewTextBoxColumn Cost;
        private DataGridViewTextBoxColumn SalePrice;
    }
}
