using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class TransactionListForm : Form
    {
        private readonly WarehouseContext _context;
        private readonly IRepository<InventoryTransaction> _transactionRepository;
        private BindingList<dynamic> transactions = new BindingList<dynamic>();

        public TransactionListForm()
        {
            InitializeComponent();

            // Initialize context and repository
            _context = new WarehouseContext();
            _transactionRepository = new Repository<InventoryTransaction>(_context);

            // Load transactions initially
            LoadTransactions();
        }

        private void addNewTransactionBtn_Click(object sender, EventArgs e)
        {
            // Open the TransactionDetailForm in new transaction mode
            OpenTransactionDetailForm(null);
        }

        private void retrieveTransactionsBtn_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateFromPicker.Value.Date;
            DateTime toDate = dateToPicker.Value.Date;

            // Load transactions within the specified date range
            LoadTransactions(fromDate, toDate);
        }

        private void transactionDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected transaction ID from the DataGridView
                var selectedTransactionID = (int)transactionDataGrid.Rows[e.RowIndex].Cells["TransactionID"].Value;

                try
                {
                    // Retrieve the actual InventoryTransaction object from the repository
                    var selectedTransaction = _transactionRepository.GetById(selectedTransactionID);

                    if (selectedTransaction != null)
                    {
                        // Open the TransactionDetailForm in edit mode
                        using (var form = new TransactionDetailForm(_context, selectedTransaction))
                        {
                            form.ShowDialog();
                        }

                        // Refresh the transactions list after closing the form
                        LoadTransactions();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading transaction details: {ex.Message}");
                }
            }
        }


        private void OpenTransactionDetailForm(dynamic transaction)
        {
            using (var form = new TransactionDetailForm(_context, transaction))
            {
                form.ShowDialog();
            }

            // Refresh the transactions list after closing the form
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;
            LoadTransactions(fromDate, toDate);
        }

        private void LoadTransactions(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Retrieve transactions from the repository
                var transactionsQuery = _transactionRepository.GetAll()
                    .Include("Warehouse")
                    .Include("InventoryItem")
                    .Where(t => t.TransactionDate >= fromDate && t.TransactionDate <= toDate);

                // Project into anonymous type for DataGridView binding
                var transactionsToShow = transactionsQuery.ToList()
                    .Select(t => new
                    {
                        t.TransactionID,
                        t.TransactionDate,
                        TransactionTypeDescription = t.TransactionType == 1 ? "Purchase" : "Sale",
                        WarehouseDescription = t.Warehouse.WarehouseDescription,
                        InventoryItemDescription = t.InventoryItem.InventoryItemDescription,
                        t.Qty,
                        t.Cost,
                        t.SalePrice,
                        TotalCost = t.TransactionType == 1 ? t.Qty * t.Cost : (decimal?)null,
                        TotalSale = t.TransactionType == 2 ? t.Qty * t.SalePrice : (decimal?)null
                    })
                    .ToList();

                // Convert to BindingList and bind to DataGridView
                transactions = new BindingList<dynamic>(transactionsToShow.Select(t => (dynamic)t).ToList());
                transactionDataGrid.DataSource = transactions;

                // Manually configure DataGridView columns
                ConfigureDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }


        private void ConfigureDataGridViewColumns()
        {
            transactionDataGrid.AutoGenerateColumns = false;
            transactionDataGrid.Columns.Clear();

            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransactionID", DataPropertyName = "TransactionID", HeaderText = "Transaction ID" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransactionDate", DataPropertyName = "TransactionDate", HeaderText = "Date" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransactionTypeDescription", DataPropertyName = "TransactionTypeDescription", HeaderText = "Transaction Type" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "WarehouseDescription", DataPropertyName = "WarehouseDescription", HeaderText = "Warehouse" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "InventoryItemDescription", DataPropertyName = "InventoryItemDescription", HeaderText = "Inventory Item" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qty", DataPropertyName = "Qty", HeaderText = "Quantity" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cost", DataPropertyName = "Cost", HeaderText = "Cost" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalePrice", DataPropertyName = "SalePrice", HeaderText = "Sale Price" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalCost", DataPropertyName = "TotalCost", HeaderText = "Total Cost" });
            transactionDataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalSale", DataPropertyName = "TotalSale", HeaderText = "Total Sale" });
        }
    }
}
