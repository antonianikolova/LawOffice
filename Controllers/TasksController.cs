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
    public class TasksController : Controller
    {
        private ApplicationDbContext _context;

        public string[] Status;

        public TasksController() 
        {
            _context = new ApplicationDbContext();
            Status = new string[] { "New", "In Progress", "Done" };
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = _context.LawOfficeTasks.ToList();
            var appUsers = _context.Users;

            foreach (var t in tasks)
            {
                var assignedToAppUser = appUsers.SingleOrDefault(u => u.Id == t.AssignedToId);
                t.AssignedToAppUser = assignedToAppUser;
            }
            return View(tasks);
        }

        public ActionResult Delete(int id)
        {
            var task = _context.LawOfficeTasks.SingleOrDefault(c => c.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            else
            {
                _context.LawOfficeTasks.Remove(task);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }

        public ActionResult Edit(int id)
        {
            var task = _context.LawOfficeTasks.SingleOrDefault(c => c.Id == id);
            var availableUsers = _context.Users;
            var assignedToUser = availableUsers.SingleOrDefault(c => c.Id == task.AssignedToId.ToString());

            if (task == null)
            {
                return HttpNotFound();
            }
            var taskViewModel = new TaskViewModel
            {
                Task = task,
                AvailableUsers = availableUsers.ToList(),
                //AssignedToName = assignedToUser.FirstName + " " + assignedToUser.LastName,
                Status = Status
            };

            ViewBag.Title = "Edit";

            return View("TaskDetails", taskViewModel);
        }

        public ActionResult New()
        {

            var availableUsers = _context.Users.ToList();

            var newTask = new LawOfficeTask
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date
            };

            var taskViewModel = new TaskViewModel
            {
                Task = newTask,
                AvailableUsers = availableUsers,
                Status = Status
            };

            ViewBag.Title = "New";

            return View("TaskDetails", taskViewModel);
        }

        [HttpPost]
        public ActionResult Save(TaskViewModel taskViewModel)
        {
            if (!ModelState.IsValid)
            {
                taskViewModel.AvailableUsers = _context.Users.ToList();
                taskViewModel.Status = Status;

                if (taskViewModel.Task.Id != 0)
                {
                    //var assignedToUser = _context.Users.SingleOrDefault(c => c.Id == taskViewModel.Task.AssignedToId.ToString());
                    //taskViewModel.AssignedToName = assignedToUser.FirstName + " " + assignedToUser.LastName;
                }

                return View("TaskDetails", taskViewModel);
            }

            if (taskViewModel.Task.Id == 0)
            {
                _context.LawOfficeTasks.Add(taskViewModel.Task);
            }
            else
            {
                var existingTask = _context.LawOfficeTasks.Single(c => c.Id == taskViewModel.Task.Id);

                ApplicationUser assignedToUser;

                if (taskViewModel.Task.AssignedToId != null)
                {
                    assignedToUser = _context.Users.SingleOrDefault(c => c.Id == taskViewModel.Task.AssignedToId.ToString());
                }
                else
                {
                    assignedToUser = null;
                }

                existingTask.Title = taskViewModel.Task.Title;
                existingTask.Description = taskViewModel.Task.Description;
                existingTask.AssignedToId = taskViewModel.Task.AssignedToId;
                existingTask.AssignedToAppUser = assignedToUser;
                existingTask.Status = taskViewModel.Task.Status;
                existingTask.StartDate = taskViewModel.Task.StartDate;
                existingTask.EndDate = taskViewModel.Task.EndDate;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }
    }
}