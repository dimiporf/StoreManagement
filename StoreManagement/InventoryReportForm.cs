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
            _context = new WarehouseContext();
            _transactionRepository = new Repository<InventoryTransaction>(_context);
            _stockRepository = new Repository<InventoryStock>(_context);

            BindWarehouses();
        }

        private void BindWarehouses()
        {
            var warehouses = _context.Warehouses.ToList();
            warehouseComboBox.DataSource = warehouses;
            warehouseComboBox.DisplayMember = "WarehouseDescription";
            warehouseComboBox.ValueMember = "WarehouseID";
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            var selectedWarehouseId = (int)warehouseComboBox.SelectedValue;
            var fromDate = dateFromPicker.Value.Date;
            var toDate = dateToPicker.Value.Date;

            LoadInventoryReport(selectedWarehouseId, fromDate, toDate);
        }

        private void LoadInventoryReport(int warehouseId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var inventoryTransactions = _transactionRepository.GetAll()
                    .Where(t => t.WarehouseID == warehouseId && t.TransactionDate >= fromDate && t.TransactionDate <= toDate)
                    .Include("InventoryItem")
                    .ToList();

                var reportData = new List<dynamic>();

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

                dataGridViewReport.DataSource = reportData;
                ConfigureDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory report: {ex.Message}");
            }
        }

        private int CalculateBalanceAtStart(int inventoryItemId, int warehouseId, DateTime fromDate)
        {
            var stockBeforePeriod = _transactionRepository.GetAll()
                .Where(t => t.InventoryItemID == inventoryItemId && t.WarehouseID == warehouseId && t.TransactionDate < fromDate)
                .GroupBy(t => t.InventoryItemID)
                .Select(g => new
                {
                    Balance = g.Where(t => t.TransactionType == 1).Sum(t => t.Qty) - g.Where(t => t.TransactionType == 2).Sum(t => t.Qty)
                })
                .FirstOrDefault();

            return stockBeforePeriod?.Balance ?? 0;
        }

        private void ConfigureDataGridViewColumns()
        {
            dataGridViewReport.AutoGenerateColumns = false;
            dataGridViewReport.Columns.Clear();

            var columns = new[]
            {
                new { Name = "InventoryItemID", DataPropertyName = "InventoryItemID", HeaderText = "Item ID", Visible = true },
                new { Name = "InventoryItemName", DataPropertyName = "InventoryItemName", HeaderText = "Item Name", Visible = true },
                new { Name = "BalanceAtStart", DataPropertyName = "BalanceAtStart", HeaderText = "Balance at Start", Visible = true },
                new { Name = "IncomingQuantity", DataPropertyName = "IncomingQuantity", HeaderText = "Incoming Quantity", Visible = true },
                new { Name = "OrderQuantity", DataPropertyName = "OrderQuantity", HeaderText = "Order Quantity", Visible = true },
                new { Name = "BalanceAtEnd", DataPropertyName = "BalanceAtEnd", HeaderText = "Balance at End", Visible = true }
            };

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
