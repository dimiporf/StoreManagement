using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class TransactionDetailForm : Form
    {
        private readonly WarehouseContext _context;
        private readonly IRepository<InventoryTransaction> _transactionRepository;
        private InventoryTransaction _transaction;
        private bool _isNewTransaction;

        public TransactionDetailForm(WarehouseContext context, InventoryTransaction transaction = null)
        {
            InitializeComponent();
            _context = context;
            _transactionRepository = new Repository<InventoryTransaction>(_context);
            _transaction = transaction;

            if (_transaction == null)
            {
                _transaction = new InventoryTransaction();
                _isNewTransaction = true;
            }
            else
            {
                _isNewTransaction = false;
                PopulateFormFields();
            }

            BindDropDowns();
            UpdateFieldVisibility();
            transactionTypeComboBox.SelectedIndexChanged += TransactionTypeComboBox_SelectedIndexChanged;
        }

        private void BindDropDowns()
        {
            // Populate transaction type dropdown
            transactionTypeComboBox.Items.Add("Purchase");
            transactionTypeComboBox.Items.Add("Sale");

            // Populate warehouse dropdown with descriptions
            warehouseComboBox.DataSource = _context.Warehouses.ToList();
            warehouseComboBox.DisplayMember = "Description";
            warehouseComboBox.ValueMember = "WarehouseID";

            // Populate inventory item dropdown
            inventoryItemComboBox.DataSource = _context.InventoryItems.ToList();
            inventoryItemComboBox.DisplayMember = "ItemName";
            inventoryItemComboBox.ValueMember = "InventoryItemID";
        }

        private void PopulateFormFields()
        {
            dateTimePickerDetail.Value = _transaction.TransactionDate;
            transactionTypeComboBox.SelectedItem = _transaction.TransactionType == 1 ? "Purchase" : "Sale";
            warehouseComboBox.SelectedValue = _transaction.WarehouseID;
            inventoryItemComboBox.SelectedValue = _transaction.InventoryItemID;
            quantityTextBox.Text = _transaction.Qty.ToString();
            costItemTextBox.Text = _transaction.Cost?.ToString() ?? string.Empty;
            salePriceItemTextBox.Text = _transaction.SalePrice?.ToString() ?? string.Empty;
            totalCostTextBox.Text = _transaction.TotalCost?.ToString() ?? string.Empty;
            totalSalesTextBox.Text = _transaction.TotalSale?.ToString() ?? string.Empty;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _transaction.TransactionDate = dateTimePickerDetail.Value;
                _transaction.TransactionType = transactionTypeComboBox.SelectedItem.ToString() == "Purchase" ? 1 : 2;
                _transaction.WarehouseID = (int)warehouseComboBox.SelectedValue;
                _transaction.InventoryItemID = (int)inventoryItemComboBox.SelectedValue;
                _transaction.Qty = int.Parse(quantityTextBox.Text);
                _transaction.Cost = decimal.TryParse(costItemTextBox.Text, out decimal cost) ? cost : (decimal?)null;
                _transaction.SalePrice = decimal.TryParse(salePriceItemTextBox.Text, out decimal salePrice) ? salePrice : (decimal?)null;

                if (_isNewTransaction)
                {
                    _transactionRepository.Insert(_transaction);
                }
                else
                {
                    _transactionRepository.Update(_transaction);
                }

                _transactionRepository.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!_isNewTransaction)
            {
                try
                {
                    _transactionRepository.Delete(_transaction.TransactionID);
                    _transactionRepository.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Close();
        }

        private void UpdateFieldVisibility()
        {
            if (transactionTypeComboBox.SelectedItem == null)
                return;

            bool isPurchase = transactionTypeComboBox.SelectedItem.ToString() == "Purchase";
            costItemTextBox.Visible = isPurchase;
            totalCostTextBox.Visible = isPurchase;
            salePriceItemTextBox.Visible = !isPurchase;
            totalSalesTextBox.Visible = !isPurchase;

            costItemLabel.Visible = isPurchase;
            totalCostLabel.Visible = isPurchase;
            salePriceItemLabel.Visible = !isPurchase;
            totalSalesLabel.Visible = !isPurchase;
        }

        private void TransactionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFieldVisibility();
        }
    }
}
