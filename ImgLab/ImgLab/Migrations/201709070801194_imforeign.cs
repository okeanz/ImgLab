namespace ImgLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imforeign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "SourceSdm_Id", c => c.Long());
            CreateIndex("dbo.ImageModels", "SourceSdm_Id");
            AddForeignKey("dbo.ImageModels", "SourceSdm_Id", "dbo.SettingsDataModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageModels", "SourceSdm_Id", "dbo.SettingsDataModels");
            DropIndex("dbo.ImageModels", new[] { "SourceSdm_Id" });
            DropColumn("dbo.ImageModels", "SourceSdm_Id");
        }
    }
}
