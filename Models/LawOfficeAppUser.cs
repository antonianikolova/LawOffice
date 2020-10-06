using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawOffice.Models
{
    public class LawOfficeAppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserCategory { get; set; }
        public string UserAppRole { get; set; }

       // public ApplicationUser AppUser { get; set; }
    }
}