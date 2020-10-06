namespace LawOffice.Migrations
{
    using LawOffice.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    internal sealed class Configuration : DbMigrationsConfiguration<LawOffice.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LawOffice.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string adminsRole = "Administrator";
            string regUserRole = "RegisteredUser";
            string trustedUserRole = "TrustedUser";

            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(adminsRole))
            {
                roleResult = RoleManager.Create(new IdentityRole(adminsRole));
            }

            if (!RoleManager.RoleExists(regUserRole))
            {
                roleResult = RoleManager.Create(new IdentityRole(regUserRole));
            }

            if (!RoleManager.RoleExists(trustedUserRole))
            {
                roleResult = RoleManager.Create(new IdentityRole(trustedUserRole));
            }

            UserManager.AddToRole("f2f8ff11-babf-4d37-838e-609424ec954c", adminsRole);

            context.ClientPersons.AddOrUpdate(x => x.Id,
                new ClientPerson() 
                { 
                    Id = 1,        
                    FirstName = "Peter",
                    MiddleName = "Ivanov",
                    LastName = "Dobrev",
                    Address = "Varna, 11 Hrizantema Str., entrance B, floor 2, ap.5",
                    IdCardNumber = "604345345",
                    CitizenIdentityNumber = "9109121020",
                    Email = "peter.dobrev@gmail.com",
                    PhoneNumber = "0889212121"
                },
                new ClientPerson()
                {
                    Id = 2,
                    FirstName = "Brigita",
                    MiddleName = "Krasimirova",
                    LastName = "Dimitrova",
                    Address = "Varna, 118 Slivnitza Blvd., entrance A, floor 1, ap.3",
                    IdCardNumber = "602444123",
                    CitizenIdentityNumber = "9301031232",
                    Email = "brigita.dimitrova@gmail.com",
                    PhoneNumber = "0887222222"
                },
                new ClientPerson()
                {
                    Id = 3,
                    FirstName = "Elisaveta",
                    MiddleName = "Petrova",
                    LastName = "Nikolaeva",
                    Address = "Varna, 2 Rosa Str., entrance C, floor 5, ap.10",
                    IdCardNumber = "644345341",
                    CitizenIdentityNumber = "8509101030",
                    Email = "eli.nikolaeva@gmail.com",
                    PhoneNumber = "0889232323"
                }
            );
        }
    }
}
