using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketAPP.Data;
using TicketAPP.Models;
using Microsoft.AspNetCore.Authorization;


namespace TicketAPP.Controllers
{
    public class TaskController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public TaskController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index(string searchString)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title != null && t.Title.ToLower().Contains(searchString.ToLower()));
            }

            return View(tasks.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Projects = _context.Projects.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Task task, int projectId)
        {
            if (string.IsNullOrEmpty(task.Title))
            {
                ModelState.AddModelError("Title", "Title is required.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Projects = _context.Projects.ToList();
                return View(task);
            }

            var project = _context.Projects.Find(projectId);

            if (project == null)
            {
                return NotFound();
            }

            task.ProjectId = projectId;
            task.createdAt = DateTime.Now;
            _context.Tasks.Add(task);
            _context.SaveChanges();

            return RedirectToAction("Index", "Task");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            ViewBag.Projects = _context.Projects.ToList();
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Projects = _context.Projects.ToList();
                return View(task);
            }

            _context.Tasks.Update(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
