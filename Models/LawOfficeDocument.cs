using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LawOffice.Models
{
    public class LawOfficeDocument
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [Display(Name = "Case")]
        public int CaseId { get; set; }
        public Case Case { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Received on Date")]
        public DateTime ReceivedOnDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due on Date")]
        public DateTime DueOnDate { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Added on Date")]
        public DateTime AddedOnDate { get; set; }

        public LawOfficeAppUser AddedBy { get; set; }

        public string AddedById { get; set; }

        [Display(Name = "Added by User")]
        public ApplicationUser AddedByAppUser { get; set; }

        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }
        public string Status { get; set; }

        [Display(Name = "Document Path")]
        public string DocumentLink { get; set; }
    }
}