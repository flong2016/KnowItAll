namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirectory : DbMigration
    {
        public override void Up()
        {

            CreateTable(
               "dbo.County",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   CityId = c.Int(nullable: false),
                   Name = c.String(nullable: false, maxLength: 100),
                   Abbreviation = c.String(maxLength: 100),
                   Published = c.Boolean(nullable: false),
                   DisplayOrder = c.Int(nullable: false),
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
               .Index(t => t.CityId);

            CreateTable(
                "dbo.City",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StateProvinceId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 100),
                    Abbreviation = c.String(maxLength: 100),
                    Published = c.Boolean(nullable: false),
                    DisplayOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StateProvince", t => t.StateProvinceId, cascadeDelete: true)
                .Index(t => t.StateProvinceId);

      

            
        }
        
        public override void Down()
        {
            

            DropForeignKey("dbo.County", "CityId", "dbo.City");
            DropForeignKey("dbo.City", "StateProvinceId", "dbo.StateProvince");
            DropForeignKey("dbo.StateProvince", "CountryId", "dbo.Country");

       
   
            DropIndex("dbo.City", new[] { "StateProvinceId" });
            DropIndex("dbo.County", new[] { "CityId" });

          
            DropTable("dbo.City");
            DropTable("dbo.County");
        }
    }
}
