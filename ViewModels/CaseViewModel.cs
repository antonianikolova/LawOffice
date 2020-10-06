using LawOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawOffice.ViewModels
{
    public class CaseViewModel
    {
        public Case Case { get; set; }
        public string UpdatedByName { get; set; }
        public IEnumerable<ClientPerson> AvailableClients { get; set; }
        public IEnumerable<string> Category { get; set; }
        public IEnumerable<string> Status { get; set; }
    }
}