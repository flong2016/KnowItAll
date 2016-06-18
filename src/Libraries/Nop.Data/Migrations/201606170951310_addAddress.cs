namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "CityId", c => c.Int());
            AddColumn("dbo.Address", "CountyId", c => c.Int());
            CreateIndex("dbo.Address", "CityId");
            CreateIndex("dbo.Address", "CountyId");
            AddForeignKey("dbo.Address", "CityId", "dbo.City", "Id");
            AddForeignKey("dbo.Address", "CountyId", "dbo.County", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "CountyId", "dbo.County");
            DropForeignKey("dbo.Address", "CityId", "dbo.City");
            DropIndex("dbo.Address", new[] { "CountyId" });
            DropIndex("dbo.Address", new[] { "CityId" });
            DropColumn("dbo.Address", "CountyId");
            DropColumn("dbo.Address", "CityId");
        }
    }
}
