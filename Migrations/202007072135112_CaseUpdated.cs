namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons");
            DropIndex("dbo.Cases", new[] { "Client_Id" });
            AlterColumn("dbo.Cases", "Client_Id", c => c.Int());
            CreateIndex("dbo.Cases", "Client_Id");
            AddForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons");
            DropIndex("dbo.Cases", new[] { "Client_Id" });
            AlterColumn("dbo.Cases", "Client_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Cases", "Client_Id");
            AddForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons", "Id", cascadeDelete: true);
        }
    }
}
