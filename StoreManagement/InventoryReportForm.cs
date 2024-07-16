using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class InventoryReportForm : Form
    {
        private readonly WarehouseContext _context;
        private readonly IRepository<InventoryTransaction> _transactionRepository;
        private readonly IRepository<InventoryStock> _stockRepository;

        public InventoryReportForm()
        {
            InitializeComponent();

            // Initialize the database context and repositories
            _context = new WarehouseContext();
            _transactionRepository = new Repository<InventoryTransaction>(_context);
            _stockRepository = new Repository<InventoryStock>(_context);

            // Bind the warehouse combobox with warehouse data
            BindWarehouses();
        }

        private void BindWarehouses()
        {
            // Retrieve all warehouses from the database and bind them to the combobox
            var warehouses = _context.Warehouses.ToList();
            warehouseComboBox.DataSource = warehouses;
            warehouseComboBox.DisplayMember = "WarehouseDescription"; // Display the warehouse description
            warehouseComboBox.ValueMember = "WarehouseID"; // Use the warehouse ID as the value
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            // Retrieve selected warehouse ID, from date, and to date from the UI controls
            var selectedWarehouseId = (int)warehouseComboBox.SelectedValue;
            var fromDate = dateFromPicker.Value.Date;
            var toDate = dateToPicker.Value.Date.AddDays(1).AddTicks(-1); // Set toDate to end of day

            // Load the inventory report based on selected criteria
            LoadInventoryReport(selectedWarehouseId, fromDate, toDate);
        }

        private void LoadInventoryReport(int warehouseId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Retrieve all inventory transactions for the selected warehouse within the specified date range
                var inventoryTransactions = _transactionRepository.GetAll()
                    .Where(t => t.WarehouseID == warehouseId && t.TransactionDate >= fromDate && t.TransactionDate <= toDate)
                    .Include("InventoryItem") // Include related InventoryItem entity for each transaction
                    .ToList();

                // Prepare a list to store dynamic objects for the report data
                var reportData = new List<dynamic>();

                // Group inventory transactions by InventoryItemID to calculate aggregates
                var inventoryItems = inventoryTransactions
                    .GroupBy(t => t.InventoryItemID)
                    .Select(g => new
                    {
                        InventoryItemID = g.Key,
                        InventoryItemName = g.First().InventoryItem.InventoryItemDescription,
                        BalanceAtStart = CalculateBalanceAtStart(g.Key, warehouseId, fromDate),
                        IncomingQuantity = g.Where(t => t.TransactionType == 1).Sum(t => t.Qty),
                        OrderQuantity = g.Where(t => t.TransactionType == 2).Sum(t => t.Qty),
                    })
                    .ToList();

                // Calculate and prepare report data for each inventory item
                foreach (var item in inventoryItems)
                {
                    var balanceAtEnd = item.BalanceAtStart + item.IncomingQuantity - item.OrderQuantity;
                    reportData.Add(new
                    {
                        item.InventoryItemID,
                        item.InventoryItemName,
                        item.BalanceAtStart,
                        item.IncomingQuantity,
                        item.OrderQuantity,
                        BalanceAtEnd = balanceAtEnd
                    });
                }

                // Bind the report data to the DataGridView
                dataGridViewReport.DataSource = reportData;

                // Configure DataGridView columns based on the report data
                ConfigureDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory report: {ex.Message}");
            }
        }

        private int CalculateBalanceAtStart(int inventoryItemId, int warehouseId, DateTime fromDate)
        {
            try
            {
                // Check for valid inventoryItemId and warehouseId
                if (inventoryItemId <= 0 || warehouseId <= 0)
                {
                    throw new ArgumentException("Invalid inventory item ID or warehouse ID.");
                }

                // Calculate the balance of the inventory item at the start of the specified period
                var stockBeforePeriod = _transactionRepository.GetAll()
                    .Where(t => t.InventoryItemID == inventoryItemId && t.WarehouseID == warehouseId && t.TransactionDate < fromDate)
                    .GroupBy(t => t.InventoryItemID)
                    .Select(g => new
                    {
                        // Sum the quantities of incoming (TransactionType == 1) and subtract outgoing (TransactionType == 2) transactions
                        Balance = g.Sum(t => t.TransactionType == 1 ? t.Qty : -t.Qty)
                    })
                    .FirstOrDefault();

                // Return the calculated balance or 0 if no data found
                return stockBeforePeriod?.Balance ?? 0;
            }
            catch (Exception ex)
            {
                // Log the exception (implementation of logging depends on your logging framework)
                Console.WriteLine($"Error calculating balance at start: {ex.Message}");
                // Return a default value in case of an error
                return 0;
            }
        }


        private void ConfigureDataGridViewColumns()
        {
            // Configure DataGridView columns for the inventory report
            dataGridViewReport.AutoGenerateColumns = false;
            dataGridViewReport.Columns.Clear();

            // Define columns for the DataGridView
            var columns = new[]
            {
                new { Name = "InventoryItemID", DataPropertyName = "InventoryItemID", HeaderText = "Item ID", Visible = true },
                new { Name = "InventoryItemName", DataPropertyName = "InventoryItemName", HeaderText = "Item Name", Visible = true },
                new { Name = "BalanceAtStart", DataPropertyName = "BalanceAtStart", HeaderText = "Balance at Start", Visible = true },
                new { Name = "IncomingQuantity", DataPropertyName = "IncomingQuantity", HeaderText = "Incoming Quantity", Visible = true },
                new { Name = "OrderQuantity", DataPropertyName = "OrderQuantity", HeaderText = "Order Quantity", Visible = true },
                new { Name = "BalanceAtEnd", DataPropertyName = "BalanceAtEnd", HeaderText = "Balance at End", Visible = true }
            };

            // Add columns to the DataGridView based on the defined properties
            foreach (var column in columns)
            {
                var gridColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.Name,
                    DataPropertyName = column.DataPropertyName,
                    HeaderText = column.HeaderText
                };

                dataGridViewReport.Columns.Add(gridColumn);
                dataGridViewReport.Columns[column.Name].Visible = column.Visible;
            }
        }
    }
}
