namespace BusinessCardManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initupdatedate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessCards", "DateOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessCards", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
