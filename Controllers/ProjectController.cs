using Microsoft.AspNetCore.Mvc;
using TicketAPP.Data;
using TicketAPP.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace TicketAPP.Controllers
{
[Microsoft.AspNetCore.Authorization.Authorize]
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

          public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
