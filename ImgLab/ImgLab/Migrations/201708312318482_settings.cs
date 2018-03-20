namespace ImgLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class settings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettingsDataModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        LastUpdate = c.DateTime(nullable: false),
                        Count = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SettingsDataModels");
        }
    }
}
