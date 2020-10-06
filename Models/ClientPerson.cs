using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawOffice.Models
{
    public class ClientPerson
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Id Card Number")]
        public string IdCardNumber { get; set; }

        [Display(Name = "Citizen Identity Number")]
        public string CitizenIdentityNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}