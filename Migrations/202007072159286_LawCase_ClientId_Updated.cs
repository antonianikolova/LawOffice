namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LawCase_ClientId_Updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons");
            DropIndex("dbo.Cases", new[] { "Client_Id" });
            RenameColumn(table: "dbo.Cases", name: "Client_Id", newName: "ClientId");
            AlterColumn("dbo.Cases", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cases", "ClientId");
            AddForeignKey("dbo.Cases", "ClientId", "dbo.ClientPersons", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cases", "ClientId", "dbo.ClientPersons");
            DropIndex("dbo.Cases", new[] { "ClientId" });
            AlterColumn("dbo.Cases", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.Cases", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.Cases", "Client_Id");
            AddForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons", "Id");
        }
    }
}
