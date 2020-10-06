using LawOffice.Models;
using LawOffice.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawOffice.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext _context;

        private string UploadFolderPath = "C:\\Users\\Antonia\\source\\MyProjects\\LawOffice\\DocumentUploads";

        public string[] DocumentType;
        public string[] Status;

        public DocumentsController()
        {
            _context = new ApplicationDbContext();
            DocumentType = new string[] { "Request", "Reply", "Appeal", "Verdict", "Expertise",  "Other" };
            Status = new string[] { "New", "Received", "For Sending", "Sent", "In Progress", "Completed" };
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Documents
        public ActionResult Index()
        {
            var lawDocs = _context.LawOfficeDocuments.ToList();
            var docsViewModel = new List<DocumentViewModel>();
            var availableCases = _context.Cases;

            foreach (var lawDoc in lawDocs)
            {
                var lawCase = availableCases.First(c => c.Id == lawDoc.CaseId);

                var docViewModel = new DocumentViewModel
                {
                    Doc = lawDoc,
                    CaseTitle = lawCase.Title
                };

                docsViewModel.Add(docViewModel);
            }
            return View(docsViewModel);
        }

        public ActionResult Open(int id)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            var lawDoc = _context.LawOfficeDocuments.SingleOrDefault(c => c.Id == id);
            if (lawDoc == null)
            {
                return HttpNotFound();
            }
            else
            {
                _context.LawOfficeDocuments.Remove(lawDoc);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Documents");
        }

        public ActionResult Edit(int id)
        {
            var lawDoc = _context.LawOfficeDocuments.SingleOrDefault(c => c.Id == id);
            var availableCases = _context.Cases;
            var appUser = _context.Users.SingleOrDefault(c => c.Id == lawDoc.AddedById);           

            if (lawDoc == null)
            {
                return HttpNotFound();
            }

            var lawCase = availableCases.First(c => c.Id == lawDoc.CaseId);

            var docViewModel = new DocumentViewModel
            {
                Doc = lawDoc,
                CaseTitle = lawCase.Title,
                AvailableCases = availableCases.ToList(),
                AddedByName = appUser.FirstName + " " + appUser.LastName,
                DocumentType = DocumentType,
                Status = Status
            };

            ViewBag.Title = "Edit";

            return View("DocumentDetails", docViewModel);
        }

        public ActionResult New()
        {

            var availableCases = _context.Cases.ToList();

            var newDoc = new LawOfficeDocument
            {
                ReceivedOnDate = DateTime.Now.Date,
                DueOnDate = DateTime.Now.Date,
                AddedOnDate = DateTime.Now
            };

            var docViewModel = new DocumentViewModel
            {
                Doc = newDoc,
                AvailableCases = availableCases.ToList(),
                DocumentType = DocumentType,
                Status = Status
            };

            ViewBag.Title = "New";

            return View("DocumentDetails", docViewModel);
        }

        [HttpPost]
        public ActionResult Save(DocumentViewModel docViewModel)
        {
            if (!ModelState.IsValid)
            {
                docViewModel.AvailableCases = _context.Cases.ToList();
                docViewModel.DocumentType = DocumentType;
                docViewModel.Status = Status;

                if (docViewModel.Doc.Id != 0)
                {
                    var appUser = _context.Users.SingleOrDefault(c => c.Id == docViewModel.Doc.AddedById);
                    docViewModel.AddedByName = appUser.FirstName + " " + appUser.LastName;
                }

                return View("DocumentDetails", docViewModel);
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = _context.Users.FirstOrDefault(u => u.Id == currentUserId);

            if (docViewModel.Doc.Id == 0)
            {
                docViewModel.Doc.AddedOnDate = DateTime.Now;
                docViewModel.Doc.AddedById = currentUserId;
                docViewModel.Doc.AddedByAppUser = currentUser;

                docViewModel.Doc.DocumentLink = SaveFile(docViewModel);

                _context.LawOfficeDocuments.Add(docViewModel.Doc);
            }
            else
            {
                var existingDoc = _context.LawOfficeDocuments.Single(c => c.Id == docViewModel.Doc.Id);

                Case lawCase;

                if (docViewModel.Doc.CaseId != 0)
                {
                    lawCase = _context.Cases.SingleOrDefault(c => c.Id == docViewModel.Doc.CaseId);
                }
                else
                {
                    lawCase = null;
                }

                docViewModel.Doc.DocumentLink = SaveFile(docViewModel);

                existingDoc.Title = docViewModel.Doc.Title;
                existingDoc.Description = docViewModel.Doc.Description;
                existingDoc.CaseId = docViewModel.Doc.CaseId;
                existingDoc.Case = lawCase;
                existingDoc.DocumentLink = docViewModel.Doc.DocumentLink;
                existingDoc.DocumentType = docViewModel.Doc.DocumentType;
                existingDoc.Status = docViewModel.Doc.Status;
                existingDoc.DueOnDate = docViewModel.Doc.DueOnDate;
                existingDoc.ReceivedOnDate = docViewModel.Doc.ReceivedOnDate;

                existingDoc.AddedOnDate = docViewModel.Doc.AddedOnDate;
                existingDoc.AddedById = docViewModel.Doc.AddedById;

                var addedByAppUser = _context.Users.SingleOrDefault(c => c.Id == docViewModel.Doc.AddedById);
                existingDoc.AddedByAppUser = addedByAppUser;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Documents");
        }

        private string SaveFile(DocumentViewModel docViewModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(docViewModel.DocumentFile.FileName);
            string fileExtension = Path.GetExtension(docViewModel.DocumentFile.FileName);
            string uploadPath = UploadFolderPath;

            fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileName.Trim() + fileExtension;
            docViewModel.Doc.DocumentLink = uploadPath + fileName;
            docViewModel.DocumentFile.SaveAs(docViewModel.Doc.DocumentLink);

            return docViewModel.Doc.DocumentLink;
        }
    }
}