namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientPersonUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientPersons", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ClientPersons", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ClientPersons", "IdCardNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientPersons", "IdCardNumber", c => c.String());
            AlterColumn("dbo.ClientPersons", "LastName", c => c.String());
            AlterColumn("dbo.ClientPersons", "FirstName", c => c.String());
        }
    }
}
