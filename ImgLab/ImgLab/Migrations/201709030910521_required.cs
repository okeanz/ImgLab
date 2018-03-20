namespace ImgLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SettingsDataModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SettingsDataModels", "Path", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SettingsDataModels", "Path", c => c.String());
            AlterColumn("dbo.SettingsDataModels", "Name", c => c.String());
        }
    }
}
