namespace BusinessCardManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initupdategender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessCards", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessCards", "Gender", c => c.Boolean(nullable: false));
        }
    }
}
