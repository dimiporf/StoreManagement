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
            transactionFromDateTimePicker = new DateTimePicker();
            transactionDataGrid = new DataGridView();
            TransactionID = new DataGridViewTextBoxColumn();
            TransactionDate = new DataGridViewTextBoxColumn();
            TransactionType = new DataGridViewTextBoxColumn();
            WarehouseID = new DataGridViewTextBoxColumn();
            InventoryItemID = new DataGridViewTextBoxColumn();
            Qty = new DataGridViewTextBoxColumn();
            Cost = new DataGridViewTextBoxColumn();
            SalePrice = new DataGridViewTextBoxColumn();
            addNewTransactionBtn = new Button();
            retrieveTransactionsBtn = new Button();
            fromLabel = new Label();
            toLabel = new Label();
            transactionToDateTimePicker = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)transactionDataGrid).BeginInit();
            SuspendLayout();
            // 
            // transactionFromDateTimePicker
            // 
            transactionFromDateTimePicker.Location = new Point(70, 15);
            transactionFromDateTimePicker.Name = "transactionFromDateTimePicker";
            transactionFromDateTimePicker.Size = new Size(226, 23);
            transactionFromDateTimePicker.TabIndex = 0;
            // 
            // transactionDataGrid
            // 
            transactionDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transactionDataGrid.Columns.AddRange(new DataGridViewColumn[] { TransactionID, TransactionDate, TransactionType, WarehouseID, InventoryItemID, Qty, Cost, SalePrice });
            transactionDataGrid.Location = new Point(29, 60);
            transactionDataGrid.Name = "transactionDataGrid";
            transactionDataGrid.Size = new Size(838, 391);
            transactionDataGrid.TabIndex = 1;
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
            // addNewTransactionBtn
            // 
            addNewTransactionBtn.Location = new Point(728, 12);
            addNewTransactionBtn.Name = "addNewTransactionBtn";
            addNewTransactionBtn.Size = new Size(139, 33);
            addNewTransactionBtn.TabIndex = 2;
            addNewTransactionBtn.Text = "Add New Transaction";
            addNewTransactionBtn.UseVisualStyleBackColor = true;
            addNewTransactionBtn.Click += addNewTransactionBtn_Click;
            // 
            // retrieveTransactionsBtn
            // 
            retrieveTransactionsBtn.Location = new Point(597, 12);
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
            // transactionToDateTimePicker
            // 
            transactionToDateTimePicker.Location = new Point(327, 15);
            transactionToDateTimePicker.Name = "transactionToDateTimePicker";
            transactionToDateTimePicker.Size = new Size(226, 23);
            transactionToDateTimePicker.TabIndex = 6;
            // 
            // TransactionListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 463);
            Controls.Add(transactionToDateTimePicker);
            Controls.Add(toLabel);
            Controls.Add(fromLabel);
            Controls.Add(retrieveTransactionsBtn);
            Controls.Add(addNewTransactionBtn);
            Controls.Add(transactionDataGrid);
            Controls.Add(transactionFromDateTimePicker);
            Name = "TransactionListForm";
            Text = "Transaction List";
            ((System.ComponentModel.ISupportInitialize)transactionDataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker transactionFromDateTimePicker;
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
        private Label fromLabel;
        private Label toLabel;
        private DateTimePicker transactionToDateTimePicker;
    }
}
