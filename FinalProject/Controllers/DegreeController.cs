using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class DegreeController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public ViewResult Index()
        {
            return View(
                _context
                    .Degrees
                    .ToList()
            );
        }

        [HttpGet]
        public ViewResult View(int id)
        {
            return View(
                _context
                    .Degrees
                    .Include(d => d.People)
                    .FirstOrDefault(d => d.Id == id)
            );
        }

        [HttpGet]
        public ViewResult Add()
        {
            var people = _context.People.ToList();
            ViewBag.People = people;

            return View(new AddDegree()
            {
                People = new List<int>()
            });
        }

        [HttpPost]
        public IActionResult Add(AddDegree degree)
        {
            if (ModelState.IsValid)
            {
                var finalDegree = new Degree()
                {
                    Id = 0,
                    Name = degree.Name,
                    Description = degree.Description,
                    People = _context
                        .People
                        .Where(p => degree.People!.Contains(p.Id))
                        .ToList()
                };

                _context.Degrees.Add(finalDegree);
                _context.SaveChanges();

                return RedirectToAction("Index", "Degree");
            }

            ModelState.AddModelError(string.Empty, "Please Correct All Errors");
            return Add();
        }
    }
}
