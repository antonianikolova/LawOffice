using LawOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawOffice.ViewModels
{
    public class TaskViewModel
    {
        public LawOfficeTask Task { get; set; }
        public string AssignedToName { get; set; }
        public IEnumerable<ApplicationUser> AvailableUsers { get; set; }
        public IEnumerable<string> Status { get; set; }
    }
}