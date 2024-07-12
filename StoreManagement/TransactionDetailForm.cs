using StoreBackend.Data; // Importing the data context
using StoreBackend.Models; // Importing the models
using StoreBackend.Repositories; // Importing the repositories
using System; // Importing the base system library
using System.Linq; // Importing LINQ for data queries
using System.Windows.Forms; // Importing the Windows Forms library

namespace StoreManagement
{
    public partial class TransactionDetailForm : Form
    {
        private readonly WarehouseContext _context; // Data context for accessing the database
        private readonly IRepository<InventoryTransaction> _transactionRepository; // Repository for managing inventory transactions
        private InventoryTransaction _transaction; // The current transaction being managed
        private bool _isNewTransaction; // Flag to determine if the transaction is new

        // Constructor for the form
        public TransactionDetailForm(WarehouseContext context, InventoryTransaction transaction = null)
        {
            InitializeComponent(); // Initialize form components
            _context = context; // Assign the data context
            _transactionRepository = new Repository<InventoryTransaction>(_context); // Initialize the repository
            _transaction = transaction; // Assign the transaction

            if (_transaction == null) // If no transaction is passed, create a new one
            {
                _transaction = new InventoryTransaction();
                _isNewTransaction = true; // Mark this as a new transaction
            }
            else // If a transaction is passed, mark it as an existing transaction
            {
                _isNewTransaction = false;
                PopulateFormFields(); // Populate the form with the existing transaction details
            }

            BindDropDowns(); // Bind data to dropdown lists
            UpdateFieldVisibility(); // Update the visibility of certain fields based on transaction type
            transactionTypeComboBox.SelectedIndexChanged += TransactionTypeComboBox_SelectedIndexChanged; // Add event handler for transaction type change
        }

        // Method to bind data to dropdown lists
        private void BindDropDowns()
        {
            // Bind transaction types to the dropdown list
            var transactionTypes = new[]
            {
                new { TransactionTypeID = 1, Description = "Purchase" },
                new { TransactionTypeID = 2, Description = "Sale" }
            };
            transactionTypeComboBox.DataSource = transactionTypes;
            transactionTypeComboBox.ValueMember = "TransactionTypeID";
            transactionTypeComboBox.DisplayMember = "Description";
            // Set the selected index of transactionTypeComboBox based on TransactionType value
            if (_transaction.TransactionType == 1)
            {
                transactionTypeComboBox.SelectedIndex = 0; // Assuming index 0 corresponds to "Purchase"
            }
            else if (_transaction.TransactionType == 2)
            {
                transactionTypeComboBox.SelectedIndex = 1; // Assuming index 1 corresponds to "Sale"
            }


            // Bind warehouses to the warehouse dropdown list
            warehouseComboBox.DataSource = _context.Warehouses.ToList();
            warehouseComboBox.DisplayMember = "WarehouseDescription"; // Show the description
            warehouseComboBox.ValueMember = "WarehouseID"; // Use the ID as the value

            // Bind inventory items to the inventory item dropdown list
            inventoryItemComboBox.DataSource = _context.InventoryItems.ToList();
            inventoryItemComboBox.DisplayMember = "InventoryItemDescription"; // Show the item name
            inventoryItemComboBox.ValueMember = "InventoryItemID"; // Use the ID as the value
        }

        // Method to populate the form fields with transaction data
        private void PopulateFormFields()
        {
            dateTimePickerDetail.Value = _transaction.TransactionDate; // Set the date
            transactionTypeComboBox.SelectedValue = _transaction.TransactionType; // Set the transaction type
            warehouseComboBox.SelectedValue = _transaction.WarehouseID; // Set the selected warehouse
            inventoryItemComboBox.SelectedValue = _transaction.InventoryItemID; // Set the selected inventory item
            quantityTextBox.Text = _transaction.Qty.ToString(); // Set the quantity
            costItemTextBox.Text = _transaction.Cost?.ToString() ?? string.Empty; // Set the cost (if available)
            salePriceItemTextBox.Text = _transaction.SalePrice?.ToString() ?? string.Empty; // Set the sale price (if available)
            totalCostTextBox.Text = _transaction.TotalCost?.ToString() ?? string.Empty; // Set the total cost (if available)
            totalSalesTextBox.Text = _transaction.TotalSale?.ToString() ?? string.Empty; // Set the total sales (if available)

            totalCostTextBox.ReadOnly = true; // Make total cost textbox read-only
            totalSalesTextBox.ReadOnly = true; // Make total sales textbox read-only

        }

        // Event handler for the Save button click
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Update the transaction details with the form data
                _transaction.TransactionDate = dateTimePickerDetail.Value;
                _transaction.TransactionType = (int)transactionTypeComboBox.SelectedValue; // Use the selected value
                _transaction.WarehouseID = (int)warehouseComboBox.SelectedValue;
                _transaction.InventoryItemID = (int)inventoryItemComboBox.SelectedValue;
                _transaction.Qty = int.Parse(quantityTextBox.Text);
                _transaction.Cost = decimal.TryParse(costItemTextBox.Text, out decimal cost) ? cost : (decimal?)null;
                _transaction.SalePrice = decimal.TryParse(salePriceItemTextBox.Text, out decimal salePrice) ? salePrice : (decimal?)null;

                // Insert or update the transaction in the database
                if (_isNewTransaction)
                {
                    _transactionRepository.Insert(_transaction);
                }
                else
                {
                    _transactionRepository.Update(_transaction);
                }

                _transactionRepository.Save(); // Save the changes to the database
                this.Close(); // Close the form
            }
            catch (Exception ex) // Handle any exceptions that occur during the save operation
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for the Delete button click
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!_isNewTransaction) // Only allow deletion if the transaction is not new
            {
                try
                {
                    _transactionRepository.Delete(_transaction.TransactionID); // Delete the transaction from the database
                    _transactionRepository.Save(); // Save the changes to the database
                }
                catch (Exception ex) // Handle any exceptions that occur during the delete operation
                {
                    MessageBox.Show($"Error deleting transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Close(); // Close the form
        }

        // Method to update the visibility of fields based on transaction type
        private void UpdateFieldVisibility()
        {
            if (transactionTypeComboBox.SelectedItem == null)
                return;

            int selectedValue = (int)transactionTypeComboBox.SelectedValue;

            // Display "Purchase" or "Sale" based on selectedValue
            string transactionTypeDescription = selectedValue == 1 ? "Purchase" : (selectedValue == 2 ? "Sale" : string.Empty);

            costItemTextBox.Visible = selectedValue == 1;
            totalCostTextBox.Visible = selectedValue == 1;
            salePriceItemTextBox.Visible = selectedValue == 2;
            totalSalesTextBox.Visible = selectedValue == 2;

            costItemLabel.Visible = selectedValue == 1;
            totalCostLabel.Visible = selectedValue == 1;
            salePriceItemLabel.Visible = selectedValue == 2;
            totalSalesLabel.Visible = selectedValue == 2;

            // Set labels and textboxes
            transactionTypeComboBox.Text = transactionTypeDescription;
        }

        // Event handler for transaction type combo box selection change
        private void TransactionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFieldVisibility(); // Update field visibility when transaction type changes
        }
    }
}
