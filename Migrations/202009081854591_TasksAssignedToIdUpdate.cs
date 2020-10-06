namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksAssignedToIdUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LawOfficeTasks", "AssignedToId", "dbo.LawOfficeAppUsers");
            DropIndex("dbo.LawOfficeTasks", new[] { "AssignedToId" });
            AddColumn("dbo.LawOfficeTasks", "AssignedTo_Id", c => c.Int());
            AlterColumn("dbo.LawOfficeTasks", "AssignedToId", c => c.String(nullable: false));
            CreateIndex("dbo.LawOfficeTasks", "AssignedTo_Id");
            AddForeignKey("dbo.LawOfficeTasks", "AssignedTo_Id", "dbo.LawOfficeAppUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LawOfficeTasks", "AssignedTo_Id", "dbo.LawOfficeAppUsers");
            DropIndex("dbo.LawOfficeTasks", new[] { "AssignedTo_Id" });
            AlterColumn("dbo.LawOfficeTasks", "AssignedToId", c => c.Int(nullable: false));
            DropColumn("dbo.LawOfficeTasks", "AssignedTo_Id");
            CreateIndex("dbo.LawOfficeTasks", "AssignedToId");
            AddForeignKey("dbo.LawOfficeTasks", "AssignedToId", "dbo.LawOfficeAppUsers", "Id", cascadeDelete: true);
        }
    }
}
