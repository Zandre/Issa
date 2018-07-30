namespace BookALook.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bodices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Overlays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skirts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Skirts");
            DropTable("dbo.Overlays");
            DropTable("dbo.Bodices");
        }
    }
}
