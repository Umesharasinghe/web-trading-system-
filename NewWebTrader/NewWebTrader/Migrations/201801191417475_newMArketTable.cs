namespace NewWebTrader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMArketTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewMarkets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                        date = c.DateTime(nullable: false),
                        time = c.DateTime(nullable: false),
                        openingPrice = c.Single(nullable: false),
                        highestPrice = c.Single(nullable: false),
                        closingPrice = c.Single(nullable: false),
                        lowestPrice = c.Single(nullable: false),
                        temp = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewMarkets");
        }
    }
}
