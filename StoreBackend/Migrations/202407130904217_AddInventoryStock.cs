namespace StoreBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddInventoryStock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryStocks",
                c => new
                {
                    InventoryStockID = c.Int(nullable: false, identity: true),
                    InventoryItemID = c.Int(nullable: false),
                    WarehouseID = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    MovingAverageCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.InventoryStockID)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItemID, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseID, cascadeDelete: true)
                .Index(t => t.InventoryItemID)
                .Index(t => t.WarehouseID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.InventoryStocks", "WarehouseID", "dbo.Warehouses");
            DropForeignKey("dbo.InventoryStocks", "InventoryItemID", "dbo.InventoryItems");
            DropIndex("dbo.InventoryStocks", new[] { "WarehouseID" });
            DropIndex("dbo.InventoryStocks", new[] { "InventoryItemID" });
            DropTable("dbo.InventoryStocks");
        }
    }

}
