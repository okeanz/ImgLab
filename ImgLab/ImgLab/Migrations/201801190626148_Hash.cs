namespace ImgLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettingsDataModels", "MD5Hash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SettingsDataModels", "MD5Hash");
        }
    }
}
