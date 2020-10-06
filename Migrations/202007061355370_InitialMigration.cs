namespace LawOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        OppositePartyName = c.String(),
                        CaseRegisterNumber = c.String(),
                        CaseRegisterDate = c.DateTime(nullable: false),
                        CaseCategory = c.String(),
                        CaseStatus = c.String(),
                        AddedOnDate = c.DateTime(nullable: false),
                        UpdatedOnDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                        MainLawyer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientPersons", t => t.Client_Id)
                .ForeignKey("dbo.LawOfficeAppUsers", t => t.MainLawyer_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.MainLawyer_Id);
            
            CreateTable(
                "dbo.ClientPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        IdCardNumber = c.String(),
                        CitizenIdentityNumber = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LawOfficeAppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        UserCategory = c.String(),
                        UserAppRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LawOfficeDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ReceivedOnDate = c.DateTime(nullable: false),
                        DueOnDate = c.DateTime(nullable: false),
                        AddedOnDate = c.DateTime(nullable: false),
                        DocumentType = c.String(),
                        Status = c.String(),
                        DocumentLink = c.String(),
                        AddedBy_Id = c.Int(),
                        Case_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LawOfficeAppUsers", t => t.AddedBy_Id)
                .ForeignKey("dbo.Cases", t => t.Case_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.Case_Id);
            
            CreateTable(
                "dbo.LawOfficeTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        AssignedTo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LawOfficeAppUsers", t => t.AssignedTo_Id)
                .Index(t => t.AssignedTo_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LawOfficeTasks", "AssignedTo_Id", "dbo.LawOfficeAppUsers");
            DropForeignKey("dbo.LawOfficeDocuments", "Case_Id", "dbo.Cases");
            DropForeignKey("dbo.LawOfficeDocuments", "AddedBy_Id", "dbo.LawOfficeAppUsers");
            DropForeignKey("dbo.Cases", "MainLawyer_Id", "dbo.LawOfficeAppUsers");
            DropForeignKey("dbo.Cases", "Client_Id", "dbo.ClientPersons");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LawOfficeTasks", new[] { "AssignedTo_Id" });
            DropIndex("dbo.LawOfficeDocuments", new[] { "Case_Id" });
            DropIndex("dbo.LawOfficeDocuments", new[] { "AddedBy_Id" });
            DropIndex("dbo.Cases", new[] { "MainLawyer_Id" });
            DropIndex("dbo.Cases", new[] { "Client_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LawOfficeTasks");
            DropTable("dbo.LawOfficeDocuments");
            DropTable("dbo.LawOfficeAppUsers");
            DropTable("dbo.ClientPersons");
            DropTable("dbo.Cases");
        }
    }
}
