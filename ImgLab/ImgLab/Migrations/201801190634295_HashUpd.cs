namespace ImgLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HashUpd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "MD5Hash", c => c.String());
            DropColumn("dbo.SettingsDataModels", "MD5Hash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SettingsDataModels", "MD5Hash", c => c.String());
            DropColumn("dbo.ImageModels", "MD5Hash");
        }
    }
}
