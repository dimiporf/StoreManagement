using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Repositories;

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
            dateFromPicker = new DateTimePicker();
            transactionDataGrid = new DataGridView();
            addNewTransactionBtn = new Button();
            retrieveTransactionsBtn = new Button();
            fromLabel = new Label();
            toLabel = new Label();
            dateToPicker = new DateTimePicker();
            TransactionID = new DataGridViewTextBoxColumn();
            TransactionDate = new DataGridViewTextBoxColumn();
            TransactionType = new DataGridViewTextBoxColumn();
            WarehouseID = new DataGridViewTextBoxColumn();
            InventoryItemID = new DataGridViewTextBoxColumn();
            Qty = new DataGridViewTextBoxColumn();
            Cost = new DataGridViewTextBoxColumn();
            SalePrice = new DataGridViewTextBoxColumn();
            TotalCost = new DataGridViewTextBoxColumn();
            TotalSales = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)transactionDataGrid).BeginInit();
            SuspendLayout();
            // 
            // dateFromPicker
            // 
            dateFromPicker.Location = new Point(70, 15);
            dateFromPicker.Name = "dateFromPicker";
            dateFromPicker.Size = new Size(226, 23);
            dateFromPicker.TabIndex = 0;
            // 
            // transactionDataGrid
            // 
            transactionDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transactionDataGrid.Columns.AddRange(new DataGridViewColumn[] { TransactionID, TransactionDate, TransactionType, WarehouseID, InventoryItemID, Qty, Cost, SalePrice, TotalCost, TotalSales });
            transactionDataGrid.Location = new Point(29, 60);
            transactionDataGrid.Name = "transactionDataGrid";
            transactionDataGrid.Size = new Size(945, 391);
            transactionDataGrid.TabIndex = 1;
            transactionDataGrid.CellDoubleClick += transactionDataGrid_CellDoubleClick;
            // 
            // addNewTransactionBtn
            // 
            addNewTransactionBtn.Location = new Point(835, 12);
            addNewTransactionBtn.Name = "addNewTransactionBtn";
            addNewTransactionBtn.Size = new Size(139, 33);
            addNewTransactionBtn.TabIndex = 2;
            addNewTransactionBtn.Text = "Add New Transaction";
            addNewTransactionBtn.UseVisualStyleBackColor = true;
            addNewTransactionBtn.Click += addNewTransactionBtn_Click;
            // 
            // retrieveTransactionsBtn
            // 
            retrieveTransactionsBtn.Location = new Point(704, 12);
            retrieveTransactionsBtn.Name = "retrieveTransactionsBtn";
            retrieveTransactionsBtn.Size = new Size(125, 33);
            retrieveTransactionsBtn.TabIndex = 3;
            retrieveTransactionsBtn.Text = "Retrieve Transactions";
            retrieveTransactionsBtn.UseVisualStyleBackColor = true;
            retrieveTransactionsBtn.Click += retrieveTransactionsBtn_Click;
            // 
            // fromLabel
            // 
            fromLabel.AutoSize = true;
            fromLabel.Location = new Point(29, 21);
            fromLabel.Name = "fromLabel";
            fromLabel.Size = new Size(35, 15);
            fromLabel.TabIndex = 4;
            fromLabel.Text = "From";
            // 
            // toLabel
            // 
            toLabel.AutoSize = true;
            toLabel.Location = new Point(302, 21);
            toLabel.Name = "toLabel";
            toLabel.Size = new Size(19, 15);
            toLabel.TabIndex = 5;
            toLabel.Text = "To";
            // 
            // dateToPicker
            // 
            dateToPicker.Location = new Point(327, 15);
            dateToPicker.Name = "dateToPicker";
            dateToPicker.Size = new Size(226, 23);
            dateToPicker.TabIndex = 6;
            // 
            // TransactionID
            // 
            TransactionID.HeaderText = "Transaction ID";
            TransactionID.Name = "TransactionID";
            TransactionID.Visible = false;
            // 
            // TransactionDate
            // 
            TransactionDate.HeaderText = "Date";
            TransactionDate.Name = "TransactionDate";
            // 
            // TransactionType
            // 
            TransactionType.HeaderText = "Transaction Type";
            TransactionType.Name = "TransactionType";
            // 
            // WarehouseID
            // 
            WarehouseID.HeaderText = "Warehouse";
            WarehouseID.Name = "WarehouseID";
            // 
            // InventoryItemID
            // 
            InventoryItemID.HeaderText = "Inventory Item";
            InventoryItemID.Name = "InventoryItemID";
            // 
            // Qty
            // 
            Qty.HeaderText = "Quantity";
            Qty.Name = "Qty";
            // 
            // Cost
            // 
            Cost.HeaderText = "Cost / Item";
            Cost.Name = "Cost";
            // 
            // SalePrice
            // 
            SalePrice.HeaderText = "Sale Price / Item";
            SalePrice.Name = "SalePrice";
            // 
            // TotalCost
            // 
            TotalCost.HeaderText = "Total Cost";
            TotalCost.Name = "TotalCost";
            // 
            // TotalSales
            // 
            TotalSales.HeaderText = "TotalSales";
            TotalSales.Name = "TotalSales";
            // 
            // TransactionListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 497);
            Controls.Add(dateToPicker);
            Controls.Add(toLabel);
            Controls.Add(fromLabel);
            Controls.Add(retrieveTransactionsBtn);
            Controls.Add(addNewTransactionBtn);
            Controls.Add(transactionDataGrid);
            Controls.Add(dateFromPicker);
            Name = "TransactionListForm";
            Text = "Transaction List";
            ((System.ComponentModel.ISupportInitialize)transactionDataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateFromPicker;
        private DataGridView transactionDataGrid;
        private Button addNewTransactionBtn;
        private Button retrieveTransactionsBtn;
        private Label fromLabel;
        private Label toLabel;
        private DateTimePicker dateToPicker;
        private DataGridViewTextBoxColumn TransactionID;
        private DataGridViewTextBoxColumn TransactionDate;
        private DataGridViewTextBoxColumn TransactionType;
        private DataGridViewTextBoxColumn WarehouseID;
        private DataGridViewTextBoxColumn InventoryItemID;
        private DataGridViewTextBoxColumn Qty;
        private DataGridViewTextBoxColumn Cost;
        private DataGridViewTextBoxColumn SalePrice;
        private DataGridViewTextBoxColumn TotalCost;
        private DataGridViewTextBoxColumn TotalSales;
    }
}
