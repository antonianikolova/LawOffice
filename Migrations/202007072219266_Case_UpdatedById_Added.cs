namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Case_UpdatedById_Added : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cases", name: "UpdatedBy_Id", newName: "UpdatedById");
            RenameIndex(table: "dbo.Cases", name: "IX_UpdatedBy_Id", newName: "IX_UpdatedById");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cases", name: "IX_UpdatedById", newName: "IX_UpdatedBy_Id");
            RenameColumn(table: "dbo.Cases", name: "UpdatedById", newName: "UpdatedBy_Id");
        }
    }
}
