using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.Linq;
using System.Transactions;

namespace StoreBackend.Services
{
    public class InventoryService
    {
        private readonly IRepository<InventoryTransaction> _transactionRepository;
        private readonly IRepository<InventoryStock> _stockRepository;

        public InventoryService(IRepository<InventoryTransaction> transactionRepository, IRepository<InventoryStock> stockRepository)
        {
            _transactionRepository = transactionRepository;
            _stockRepository = stockRepository;
        }

        // Adds a new transaction and updates the inventory stock accordingly
        public void AddTransaction(InventoryTransaction transaction)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    // Insert the new transaction into the repository
                    _transactionRepository.Insert(transaction);
                    _transactionRepository.Save();

                    // Update the inventory stock based on the new transaction
                    UpdateInventoryStockOnAdd(transaction);

                    // Complete the transaction scope, committing changes to both repositories
                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding transaction: {ex.Message}");
                    throw; // Propagate the exception to indicate failure
                }
            }
        }

        // Updates an existing transaction and adjusts the inventory stock accordingly
        public void UpdateTransaction(InventoryTransaction transaction)
        {
            var existingTransaction = _transactionRepository.GetById(transaction.TransactionID);
            if (existingTransaction != null)
            {
                using (var transactionScope = new TransactionScope())
                {
                    try
                    {
                        // Revert the original transaction's impact on stock
                        RevertInventoryStock(existingTransaction);

                        // Update the existing transaction with the new values
                        existingTransaction.InventoryItemID = transaction.InventoryItemID;
                        existingTransaction.WarehouseID = transaction.WarehouseID;
                        existingTransaction.TransactionType = transaction.TransactionType;
                        existingTransaction.Qty = transaction.Qty;
                        existingTransaction.Cost = transaction.Cost;
                        existingTransaction.SalePrice = transaction.SalePrice;
                        existingTransaction.TransactionDate = transaction.TransactionDate;

                        // Save the changes to the transaction repository
                        _transactionRepository.Update(existingTransaction);
                        _transactionRepository.Save();

                        // Apply the updated transaction's impact on stock
                        UpdateInventoryStock(existingTransaction);

                        // Complete the transaction scope, committing changes to both repositories
                        transactionScope.Complete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating transaction: {ex.Message}");
                        throw; // Propagate the exception to indicate failure
                    }
                }
            }
        }

        // Deletes a transaction and adjusts the inventory stock accordingly
        public void DeleteTransaction(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction != null)
            {
                using (var transactionScope = new TransactionScope())
                {
                    try
                    {
                        // Revert the transaction's impact on stock
                        RevertInventoryStock(transaction);

                        // Delete the transaction from the repository
                        _transactionRepository.Delete(transactionId);
                        _transactionRepository.Save();

                        // Complete the transaction scope, committing changes to both repositories
                        transactionScope.Complete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting transaction: {ex.Message}");
                        throw; // Propagate the exception to indicate failure
                    }
                }
            }
        }

        // Updates the inventory stock based on the given transaction when adding a new transaction
        private void UpdateInventoryStockOnAdd(InventoryTransaction transaction)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == transaction.InventoryItemID && s.WarehouseID == transaction.WarehouseID);

            if (stock != null)
            {
                // Calculate current total cost of stock
                decimal currentTotalCost = stock.Quantity * stock.MovingAverageCost;

                // Update stock quantity based on transaction type
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity += transaction.Qty;
                    currentTotalCost += transaction.Qty * transaction.Cost.Value;
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    // Ensure we have enough stock to sell
                    if (stock.Quantity < transaction.Qty)
                    {
                        throw new InvalidOperationException("Insufficient stock for sale.");
                    }

                    stock.Quantity -= transaction.Qty;
                    currentTotalCost -= transaction.Qty * stock.MovingAverageCost;
                }

                // Update moving average cost
                if (stock.Quantity > 0)
                {
                    stock.MovingAverageCost = currentTotalCost / stock.Quantity;
                }
                else
                {
                    stock.MovingAverageCost = 0; // If no stock, reset cost to 0
                }

                // Update stock entity in repository
                _stockRepository.Update(stock);
                _stockRepository.Save();
            }
            else
            {
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock = new InventoryStock
                    {
                        InventoryItemID = transaction.InventoryItemID,
                        WarehouseID = transaction.WarehouseID,
                        Quantity = transaction.Qty,
                        MovingAverageCost = transaction.Cost.Value
                    };

                    _stockRepository.Insert(stock);
                    _stockRepository.Save();
                }
                else
                {
                    // Handle case where stock does not exist and transaction is a sale
                    throw new InvalidOperationException("Stock not found for sale transaction.");
                }
            }
        }

        // Updates the inventory stock based on the given transaction when updating a transaction
        private void UpdateInventoryStock(InventoryTransaction transaction)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == transaction.InventoryItemID && s.WarehouseID == transaction.WarehouseID);

            if (stock != null)
            {
                // Calculate current total cost of stock
                decimal currentTotalCost = stock.Quantity * stock.MovingAverageCost;

                // Update stock quantity based on transaction type
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity += transaction.Qty;
                    currentTotalCost += transaction.Qty * transaction.Cost.Value;
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    // Ensure we have enough stock to sell
                    if (stock.Quantity < transaction.Qty)
                    {
                        throw new InvalidOperationException("Insufficient stock for sale.");
                    }

                    stock.Quantity -= transaction.Qty;
                    currentTotalCost -= transaction.Qty * stock.MovingAverageCost;
                }

                // Update moving average cost
                if (stock.Quantity > 0)
                {
                    stock.MovingAverageCost = currentTotalCost / stock.Quantity;
                }
                else
                {
                    stock.MovingAverageCost = 0; // If no stock, reset cost to 0
                }

                // Update stock entity in repository
                _stockRepository.Update(stock);
                _stockRepository.Save();
            }
            else
            {
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock = new InventoryStock
                    {
                        InventoryItemID = transaction.InventoryItemID,
                        WarehouseID = transaction.WarehouseID,
                        Quantity = transaction.Qty,
                        MovingAverageCost = transaction.Cost.Value
                    };

                    _stockRepository.Insert(stock);
                    _stockRepository.Save();
                }
                else
                {
                    // Handle case where stock does not exist and transaction is a sale
                    throw new InvalidOperationException("Stock not found for sale transaction.");
                }
            }
        }

        // Reverts the inventory stock based on the given transaction
        private void RevertInventoryStock(InventoryTransaction transaction)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == transaction.InventoryItemID && s.WarehouseID == transaction.WarehouseID);

            if (stock != null)
            {
                // Calculate current total cost of stock
                decimal currentTotalCost = stock.Quantity * stock.MovingAverageCost;

                // Revert stock quantity based on transaction type
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity -= transaction.Qty;
                    // Ensure we don't go negative on stock
                    if (stock.Quantity < 0)
                    {
                        throw new InvalidOperationException("Insufficient stock to revert purchase.");
                    }
                    currentTotalCost += transaction.Qty * transaction.Cost.Value;
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    stock.Quantity += transaction.Qty;
                    currentTotalCost -= transaction.Qty * stock.MovingAverageCost;
                }

                // Update moving average cost
                if (stock.Quantity > 0)
                {
                    stock.MovingAverageCost = currentTotalCost / stock.Quantity;
                }
                else
                {
                    stock.MovingAverageCost = 0; // If no stock, reset cost to 0
                }

                // Update stock entity in repository
                _stockRepository.Update(stock);
                _stockRepository.Save();
            }
        }

        // Calculates and returns the moving average cost for a given inventory item in a warehouse
        public decimal GetMovingAverageCost(int inventoryItemId, int warehouseId)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == inventoryItemId && s.WarehouseID == warehouseId);

            if (stock != null)
            {
                return stock.MovingAverageCost;
            }

            return 0m; // Return 0 if no stock entry found
        }

        // Retrieves the current stock for a given inventory item in a warehouse
        public InventoryStock GetStock(int inventoryItemId, int warehouseId)
        {
            try
            {
                var stock = _stockRepository.GetAll()
                    .FirstOrDefault(s => s.InventoryItemID == inventoryItemId && s.WarehouseID == warehouseId);

                return stock;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving stock: {ex.Message}");
                return null; // Return null in case of any exception
            }
        }
    }
}
