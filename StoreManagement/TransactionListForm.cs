using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Repositories;
using System.ComponentModel;
using System.Transactions;

namespace StoreManagement
{
    public partial class TransactionListForm : Form
    {
        private readonly WarehouseContext _context;
        private readonly IRepository<InventoryTransaction> _transactionRepository;
        private BindingList<InventoryTransaction> transactions = new BindingList<InventoryTransaction>();

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
            using (var form = new TransactionDetailForm(_context))
            {
                form.ShowDialog();
            }

            // Refresh the transactions list after closing the form
            LoadTransactions();
        }

        private void retrieveTransactionsBtn_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateFromPicker.Value.Date;
            DateTime toDate = dateToPicker.Value.Date;

            // Retrieve transactions from repository
            transactions.Clear(); // Clear the existing data

            var retrievedTransactions = _transactionRepository.GetAll()
                                 .Where(t => t.TransactionDate >= fromDate && t.TransactionDate <= toDate)
                                 .ToList<InventoryTransaction>(); // Ensure it's InventoryTransaction

            // Add retrieved transactions to the BindingList
            foreach (var transaction in retrievedTransactions)
            {
                transactions.Add(transaction);
            }

            // Set DataGridView's DataSource to the BindingList
            transactionDataGrid.DataSource = transactions;


        }

        private void transactionDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index
            {
                // Get the selected transaction
                var selectedTransaction = transactionDataGrid.Rows[e.RowIndex].DataBoundItem as InventoryTransaction;

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
        }

        private void LoadTransactions()
        {
            try
            {
                // Retrieve transactions from repository
                var transactions = _transactionRepository.GetAll();

                

                // Bind transactions to DataGridView
                transactionDataGrid.AutoGenerateColumns = false; // Ensure auto generation is off
                transactionDataGrid.DataSource = transactions.ToList(); // Bind data source

                // Manually bind each column to the corresponding property of InventoryTransaction
                TransactionID.DataPropertyName = "TransactionID";
                TransactionDate.DataPropertyName = "TransactionDate";
                TransactionType.DataPropertyName = "TransactionType";
                
                WarehouseID.DataPropertyName = "WarehouseID";
                InventoryItemID.DataPropertyName = "InventoryItemID";
                Qty.DataPropertyName = "Qty";
                Cost.DataPropertyName = "Cost";
                SalePrice.DataPropertyName = "SalePrice";
                TotalCost.DataPropertyName = "TotalCost";
                TotalSales.DataPropertyName = "TotalSale";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }
    }
}
