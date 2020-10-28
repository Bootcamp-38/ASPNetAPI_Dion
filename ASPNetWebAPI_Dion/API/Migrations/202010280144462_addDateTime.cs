namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_M_Department", "dateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_M_Department", "dateTime");
        }
    }
}
