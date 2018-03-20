namespace ImgLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Path = c.String(),
                        DCreation = c.DateTime(nullable: false),
                        FocusDist = c.String(),
                        Exposure = c.String(),
                        CameraModel = c.String(),
                        LensModel = c.String(),
                        SubSatCoord = c.String(),
                        CenterCoord = c.String(),
                        ExpNum = c.String(),
                        RadiogramNum = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageModels");
        }
    }
}
