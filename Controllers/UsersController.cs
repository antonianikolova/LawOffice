using LawOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawOffice.Controllers
{
    [Authorize(Roles ="Administrator")] 
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
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
    }
}