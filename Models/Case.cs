using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LawOffice.Models
{
    public class Case
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        [Display(Name ="Client")]
        public int ClientId { get; set; }

        public ClientPerson Client { get; set; }

        [Required]
        [Display(Name = "Opposite Party Name")]
        public string OppositePartyName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Case Register Number")]
        public string CaseRegisterNumber { get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Case Register Date")]
        public DateTime CaseRegisterDate { get; set; }

        [Display(Name = "Case Category")]
        public string CaseCategory { get; set; }

        [Display(Name ="Case Status")]
        public string CaseStatus { get; set; }

        public string UpdatedById { get; set; }

        [Display(Name = "Updated by User")]
        public ApplicationUser UpdatedBy { get; set; }

        [Display(Name = "Updated on Date")]
        public DateTime UpdatedOnDate { get; set; }

        [Display(Name = "Case Documents")]
        public IEnumerable<LawOfficeDocument> CaseDocuments { get; set; }

        //public LawOfficeAppUser MainLawyer { get; set; }
    }
}