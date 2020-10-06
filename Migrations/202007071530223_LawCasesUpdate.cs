namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LawCasesUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cases", "MainLawyer_Id", "dbo.LawOfficeAppUsers");
            DropForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons");
            DropIndex("dbo.Cases", new[] { "Client_Id" });
            DropIndex("dbo.Cases", new[] { "MainLawyer_Id" });
            AddColumn("dbo.Cases", "UpdatedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Cases", "Title", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Cases", "OppositePartyName", c => c.String(nullable: false));
            AlterColumn("dbo.Cases", "CaseRegisterNumber", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Cases", "Client_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Cases", "Client_Id");
            CreateIndex("dbo.Cases", "UpdatedBy_Id");
            AddForeignKey("dbo.Cases", "UpdatedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons", "Id", cascadeDelete: true);
            DropColumn("dbo.Cases", "AddedOnDate");
            DropColumn("dbo.Cases", "MainLawyer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cases", "MainLawyer_Id", c => c.Int());
            AddColumn("dbo.Cases", "AddedOnDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons");
            DropForeignKey("dbo.Cases", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Cases", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Cases", new[] { "Client_Id" });
            AlterColumn("dbo.Cases", "Client_Id", c => c.Int());
            AlterColumn("dbo.Cases", "CaseRegisterNumber", c => c.String());
            AlterColumn("dbo.Cases", "OppositePartyName", c => c.String());
            AlterColumn("dbo.Cases", "Title", c => c.String());
            DropColumn("dbo.Cases", "UpdatedBy_Id");
            CreateIndex("dbo.Cases", "MainLawyer_Id");
            CreateIndex("dbo.Cases", "Client_Id");
            AddForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons", "Id");
            AddForeignKey("dbo.Cases", "MainLawyer_Id", "dbo.LawOfficeAppUsers", "Id");
        }
    }
}
