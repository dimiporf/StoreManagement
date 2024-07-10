using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Repositories;

namespace StoreManagement
{
    public partial class TransactionListForm : Form
    {
        private readonly WarehouseContext _context;
        private readonly IRepository<InventoryTransaction> _transactionRepository;
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

        }

        private void retrieveTransactionsBtn_Click(object sender, EventArgs e)
        {
            // Reload transactions (if needed)
            LoadTransactions();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }
    }
}
