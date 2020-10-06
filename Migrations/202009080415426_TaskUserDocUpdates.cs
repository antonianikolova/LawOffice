namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskUserDocUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LawOfficeDocuments", "Case_Id", "dbo.Cases");
            DropForeignKey("dbo.LawOfficeTasks", "AssignedTo_Id", "dbo.LawOfficeAppUsers");
            DropIndex("dbo.LawOfficeDocuments", new[] { "Case_Id" });
            DropIndex("dbo.LawOfficeTasks", new[] { "AssignedTo_Id" });
            RenameColumn(table: "dbo.LawOfficeDocuments", name: "Case_Id", newName: "CaseId");
            RenameColumn(table: "dbo.LawOfficeTasks", name: "AssignedTo_Id", newName: "AssignedToId");
            AddColumn("dbo.LawOfficeDocuments", "AddedById", c => c.String());
            AddColumn("dbo.LawOfficeDocuments", "AddedByAppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.LawOfficeTasks", "AssignedToAppUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.LawOfficeDocuments", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.LawOfficeDocuments", "CaseId", c => c.Int(nullable: false));
            AlterColumn("dbo.LawOfficeTasks", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.LawOfficeTasks", "AssignedToId", c => c.Int(nullable: false));
            CreateIndex("dbo.LawOfficeDocuments", "CaseId");
            CreateIndex("dbo.LawOfficeDocuments", "AddedByAppUser_Id");
            CreateIndex("dbo.LawOfficeTasks", "AssignedToId");
            CreateIndex("dbo.LawOfficeTasks", "AssignedToAppUser_Id");
            AddForeignKey("dbo.LawOfficeDocuments", "AddedByAppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.LawOfficeTasks", "AssignedToAppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.LawOfficeDocuments", "CaseId", "dbo.Cases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LawOfficeTasks", "AssignedToId", "dbo.LawOfficeAppUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LawOfficeTasks", "AssignedToId", "dbo.LawOfficeAppUsers");
            DropForeignKey("dbo.LawOfficeDocuments", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.LawOfficeTasks", "AssignedToAppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LawOfficeDocuments", "AddedByAppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LawOfficeTasks", new[] { "AssignedToAppUser_Id" });
            DropIndex("dbo.LawOfficeTasks", new[] { "AssignedToId" });
            DropIndex("dbo.LawOfficeDocuments", new[] { "AddedByAppUser_Id" });
            DropIndex("dbo.LawOfficeDocuments", new[] { "CaseId" });
            AlterColumn("dbo.LawOfficeTasks", "AssignedToId", c => c.Int());
            AlterColumn("dbo.LawOfficeTasks", "Title", c => c.String());
            AlterColumn("dbo.LawOfficeDocuments", "CaseId", c => c.Int());
            AlterColumn("dbo.LawOfficeDocuments", "Title", c => c.String());
            DropColumn("dbo.LawOfficeTasks", "AssignedToAppUser_Id");
            DropColumn("dbo.LawOfficeDocuments", "AddedByAppUser_Id");
            DropColumn("dbo.LawOfficeDocuments", "AddedById");
            RenameColumn(table: "dbo.LawOfficeTasks", name: "AssignedToId", newName: "AssignedTo_Id");
            RenameColumn(table: "dbo.LawOfficeDocuments", name: "CaseId", newName: "Case_Id");
            CreateIndex("dbo.LawOfficeTasks", "AssignedTo_Id");
            CreateIndex("dbo.LawOfficeDocuments", "Case_Id");
            AddForeignKey("dbo.LawOfficeTasks", "AssignedTo_Id", "dbo.LawOfficeAppUsers", "Id");
            AddForeignKey("dbo.LawOfficeDocuments", "Case_Id", "dbo.Cases", "Id");
        }
    }
}
