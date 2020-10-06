using LawOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawOffice.ViewModels
{
    public class DocumentViewModel
    {
        public LawOfficeDocument Doc { get; set; }
        public string AddedByName { get; set; }
        public string CaseTitle { get; set; }

        [Display(Name = "Document Upload")]
        public HttpPostedFileBase DocumentFile { get; set; }
        public IEnumerable<Case> AvailableCases { get; set; }
        public IEnumerable<string> DocumentType { get; set; }
        public IEnumerable<string> Status { get; set; }
    }
}