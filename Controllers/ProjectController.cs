using Microsoft.AspNetCore.Mvc;
using TicketAPP.Data;
using TicketAPP.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace TicketAPP.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(project).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var project = _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}
