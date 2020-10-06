using LawOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawOffice.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext _context;

        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose (bool disposing)
        {
            _context.Dispose();
        }

        // GET: Clients
        public ActionResult Index()
        {
            var clients = _context.ClientPersons.ToList();
            return View(clients);
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var client = _context.ClientPersons.SingleOrDefault(c => c.Id == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public ActionResult Delete(int id)
        {
            var client = _context.ClientPersons.SingleOrDefault(c => c.Id == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            else
            {
                _context.ClientPersons.Remove(client);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Clients");
        }

        public ActionResult Edit (int id)
        {
            var client = _context.ClientPersons.SingleOrDefault(c => c.Id == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Edit";
            return View("ClientDetails", client);
        }

        public ActionResult New()
        {
            var newClient = new ClientPerson();
            ViewBag.Title = "New";

            return View("ClientDetails", newClient);
        }

        [HttpPost]
        public ActionResult Save(ClientPerson client)
        {
            if (!ModelState.IsValid)
            {
                return View("ClientDetails", client);
            }
            if (client.Id == 0)
            {
                _context.ClientPersons.Add(client);             
            }
            else
            {
                var existingClient = _context.ClientPersons.Single(c => c.Id == client.Id);

                existingClient.FirstName = client.FirstName;
                existingClient.MiddleName = client.MiddleName;
                existingClient.LastName = client.LastName;
                existingClient.Address = client.Address;
                existingClient.IdCardNumber = client.IdCardNumber;
                existingClient.CitizenIdentityNumber = client.CitizenIdentityNumber;
                existingClient.Email = client.Email;
                existingClient.PhoneNumber = client.PhoneNumber;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Clients");
        }
    }
}