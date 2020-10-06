using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LawOffice.Models
{
    public class LawOfficeTask
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Assignee")]
        public string AssignedToId { get; set; }

        public LawOfficeAppUser AssignedTo { get; set; }
        public ApplicationUser AssignedToAppUser { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}