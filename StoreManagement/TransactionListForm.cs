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

            // Attach DataError event handler
            transactionDataGrid.DataError += transactionDataGrid_DataError;

            // Disable option for new row by user - only use database entries
            transactionDataGrid.AllowUserToAddRows = false;
        }

        private void addNewTransactionBtn_Click(object sender, EventArgs e)
        {
            // Open the TransactionDetailForm in new transaction mode
            OpenTransactionDetailForm(null);
        }

        private void retrieveTransactionsBtn_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateFromPicker.Value.Date;
            DateTime toDate = dateToPicker.Value.Date.AddDays(1).AddTicks(-1); // Set toDate to end of day
            

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

        // This method sets a default date range from DateTime.MinValue to DateTime.MaxValue.
        protected void LoadTransactions()
        {
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;
            LoadTransactions(fromDate, toDate);
        }

        // Loads transactions from the database within the specified date range and binds them to the DataGridView. 
        // Calculates and displays totals for Total Cost and Total Sale at the bottom of the DataGridView.
        protected void LoadTransactions(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Retrieve transactions from the repository
                var transactionsQuery = _transactionRepository.GetAll()
                    .Include("Warehouse")
                    .Include("InventoryItem")
                    .Where(t => t.TransactionDate >= fromDate && t.TransactionDate <= toDate)
                    .ToList(); // Materialize the query to avoid multiple enumeration

                // Project into anonymous type for DataGridView binding
                var transactionsToShow = transactionsQuery
                    .Select(t => new
                    {
                        t.TransactionID,
                        TransactionDate = t.TransactionDate.ToString(), // Convert to string if necessary
                        TransactionTypeDescription = t.TransactionType == 1 ? "Purchase" : "Sale",
                        WarehouseDescription = t.Warehouse.WarehouseDescription,
                        InventoryItemDescription = t.InventoryItem.InventoryItemDescription,
                        Qty = t.Qty.ToString(), // Convert to string if necessary
                        Cost = t.Cost?.ToString() ?? "", // Convert to string if necessary
                        SalePrice = t.SalePrice?.ToString() ?? "", // Convert to string if necessary
                        TotalCost = t.TransactionType == 1 ? (t.Qty * (t.Cost ?? 0m)) : 0m, // Calculate TotalCost
                        TotalSale = t.TransactionType == 2 ? (t.Qty * (t.SalePrice ?? 0m)) : 0m // Calculate TotalSale
                    })
                    .ToList();

                // Add totals row to the transactionsToShow list
                var totalCost = transactionsToShow.Sum(t => t.TotalCost);
                var totalSale = transactionsToShow.Sum(t => t.TotalSale);

                // Mock transaction to hold the sum values of TotalCost and TotalSale
                transactionsToShow.Add(new
                {
                    TransactionID = 0, // Set to 0 or any unique value for totals row
                    TransactionDate = "", // Leave empty for totals row
                    TransactionTypeDescription = "", // Leave empty for totals row
                    WarehouseDescription = "", // Leave empty for totals row
                    InventoryItemDescription = "", // Leave empty for totals row
                    Qty = "", // Leave empty for totals row
                    Cost = "", // Leave empty for totals row
                    SalePrice = "", // Leave empty for totals row
                    TotalCost = totalCost, // Set total cost
                    TotalSale = totalSale // Set total sale
                });

                // Convert to BindingList and bind to DataGridView
                transactions = new BindingList<dynamic>(transactionsToShow.Select(t => (dynamic)t).ToList());

                // Bind the transactions list to DataGridView
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
            // Disable automatic column generation to manually define columns
            transactionDataGrid.AutoGenerateColumns = false;

            // Clear any existing columns to start fresh
            transactionDataGrid.Columns.Clear();

            // Define an array of anonymous objects to hold column details
            var columns = new[]
            {
        // Each object represents a column and includes:
        // - Name: The name of the column
        // - DataPropertyName: The property name of the data source bound to this column
        // - HeaderText: The text displayed in the column header
        // - Visible: Boolean to determine if the column should be visible
        new { Name = "TransactionID", DataPropertyName = "TransactionID", HeaderText = "Transaction ID", Visible = false },
        new { Name = "TransactionDate", DataPropertyName = "TransactionDate", HeaderText = "Date", Visible = true },
        new { Name = "TransactionTypeDescription", DataPropertyName = "TransactionTypeDescription", HeaderText = "Transaction Type", Visible = true },
        new { Name = "WarehouseDescription", DataPropertyName = "WarehouseDescription", HeaderText = "Warehouse", Visible = true },
        new { Name = "InventoryItemDescription", DataPropertyName = "InventoryItemDescription", HeaderText = "Inventory Item", Visible = true },
        new { Name = "Qty", DataPropertyName = "Qty", HeaderText = "Quantity", Visible = true },
        new { Name = "Cost", DataPropertyName = "Cost", HeaderText = "Cost", Visible = true },
        new { Name = "SalePrice", DataPropertyName = "SalePrice", HeaderText = "Sale Price", Visible = true },
        new { Name = "TotalCost", DataPropertyName = "TotalCost", HeaderText = "Total Cost", Visible = true },
        new { Name = "TotalSale", DataPropertyName = "TotalSale", HeaderText = "Total Sale", Visible = true }
    };

            // Loop through the array of column definitions
            foreach (var column in columns)
            {
                // Create a new DataGridViewTextBoxColumn for each column definition
                var gridColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.Name, // Set the column name
                    DataPropertyName = column.DataPropertyName, // Set the data property name
                    HeaderText = column.HeaderText // Set the header text
                };

                // Add the column to the DataGridView
                transactionDataGrid.Columns.Add(gridColumn);

                // Set the visibility of the column based on the 'Visible' property
                transactionDataGrid.Columns[column.Name].Visible = column.Visible;
            }
        }


        private void openInventoryReportFormBtn_Click(object sender, EventArgs e)
        {
            using (var reportForm = new InventoryReportForm())
            {
                reportForm.ShowDialog();
            }
        }



        private void transactionDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Handle data errors here
            MessageBox.Show($"Data error occurred: {e.Exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Optionally, log the exception or take other actions as needed
            // Logging.LogException(e.Exception);
        }
    }
}
