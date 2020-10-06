using LawOffice.Models;
using LawOffice.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawOffice.Controllers
{
    [Authorize]
    public class CasesController : Controller
    {
        private ApplicationDbContext _context;

        public string[] Category;
        public string[] Status;
        public CasesController()
        {
            _context = new ApplicationDbContext();
            Category = new string[] { "Business", "Citizen", "Government" };
            Status = new string[] {"New", "In Progress", "Waiting for Decision", "Waiting for Documents", "Needs Response", "Finished" };
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Cases
        public ActionResult Index()
        {
            var lawCases = _context.Cases.OrderByDescending(d => d.CaseRegisterDate).ToList();

            return View(lawCases);
        }

        public ActionResult Delete(int id)
        {
            var lawCase = _context.Cases.SingleOrDefault(c => c.Id == id);
            if (lawCase == null)
            {
                return HttpNotFound();
            }
            else
            {
                _context.Cases.Remove(lawCase);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Cases");
        }

        public ActionResult Edit(int id)
        {
            var lawCase = _context.Cases.SingleOrDefault(c => c.Id == id);
            var availableClients = _context.ClientPersons;
            var appUser = _context.Users.SingleOrDefault(c => c.Id == lawCase.UpdatedById);

            if (lawCase == null)
            {
                return HttpNotFound();
            }
            var caseViewModel = new CaseViewModel
            {
                Case = lawCase,
                AvailableClients = availableClients.ToList(),
                UpdatedByName = appUser.FirstName + " " + appUser.LastName,
                Category = Category,
                Status = Status
            };

            ViewBag.Title = "Edit";

            return View("CaseDetails", caseViewModel);
        }

        public ActionResult New()
        {

            var availableClients = _context.ClientPersons.ToList();

            var newCase = new Case
            {
                CaseRegisterDate = DateTime.Now.Date,
                UpdatedOnDate = DateTime.Now
            }; 

            var caseViewModel = new CaseViewModel
            {
                Case = newCase,
                AvailableClients = availableClients,
                Category = Category,
                Status = Status
            };

            ViewBag.Title = "New";

            return View("CaseDetails", caseViewModel);
        }

        [HttpPost]
        public ActionResult Save(CaseViewModel caseViewModel)
        {
            if (!ModelState.IsValid)
            {
                caseViewModel.AvailableClients = _context.ClientPersons.ToList();
                caseViewModel.Category = Category;
                caseViewModel.Status = Status;

                if (caseViewModel.Case.Id != 0)
                {
                    var appUser = _context.Users.SingleOrDefault(c => c.Id == caseViewModel.Case.UpdatedById);
                    caseViewModel.UpdatedByName = appUser.FirstName + " " + appUser.LastName;                 
                }

                return View("CaseDetails", caseViewModel);
            }
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _context.Users.FirstOrDefault(u => u.Id == currentUserId);

            if (caseViewModel.Case.Id == 0)
            {
                caseViewModel.Case.UpdatedOnDate = DateTime.Now;
                caseViewModel.Case.UpdatedById = currentUserId;
                caseViewModel.Case.UpdatedBy = currentUser;

                _context.Cases.Add(caseViewModel.Case);
            }
            else
            {
                var existingCase = _context.Cases.Single(c => c.Id == caseViewModel.Case.Id);

                ClientPerson client;

                if (caseViewModel.Case.ClientId != 0)
                {
                    client = _context.ClientPersons.SingleOrDefault(c => c.Id == caseViewModel.Case.ClientId);
                }
                else
                {
                    client = null;
                }

                existingCase.Title = caseViewModel.Case.Title;
                existingCase.Description = caseViewModel.Case.Description;
                existingCase.CaseRegisterNumber = caseViewModel.Case.CaseRegisterNumber;
                existingCase.CaseRegisterDate = caseViewModel.Case.CaseRegisterDate;
                existingCase.ClientId = caseViewModel.Case.ClientId;
                existingCase.Client = client;
                existingCase.OppositePartyName = caseViewModel.Case.OppositePartyName;
                existingCase.CaseCategory = caseViewModel.Case.CaseCategory;
                existingCase.CaseStatus = caseViewModel.Case.CaseStatus;
                existingCase.UpdatedOnDate = DateTime.Now;
                existingCase.UpdatedById = currentUserId;
                existingCase.UpdatedBy = currentUser;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Cases");
        }
    }
}