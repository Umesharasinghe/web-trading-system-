namespace NewWebTrader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        UploadedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsUploads");
        }
    }
}
