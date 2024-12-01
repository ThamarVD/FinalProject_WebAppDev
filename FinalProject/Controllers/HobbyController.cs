using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class HobbyController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public ViewResult Index()
        {
            return View(
                _context
                    .Hobbies
                    .ToList()
            );
        }

        [HttpGet]
        public ViewResult View(int id)
        {
            return View(
                _context
                    .Hobbies
                    .Include(h => h.People)
                    .FirstOrDefault(h => h.Id == id)
            );
        }

        [HttpGet]
        public ViewResult Add()
        {
            var people = _context.People.ToList();
            ViewBag.People = people;
            
            return View(new AddHobby()
            {
                People = new List<int>()
            });
        }

        [HttpPost]
        public IActionResult Add(AddHobby hobby)
        {
            if (ModelState.IsValid)
            {
                var finalHobby = new Hobby()
                {
                    Id = 0,
                    Name = hobby.Name,
                    Description = hobby.Description,
                    People = _context
                        .People
                        .Where(p => hobby.People!.Contains(p.Id))
                        .ToList()
                };
                
                _context.Hobbies.Add(finalHobby);
                _context.SaveChanges();
                
                return RedirectToAction("Index", "Hobby");
            }
            
            ModelState.AddModelError(String.Empty, "Please Correct All Errors");
            return Add();
        }
    }
}
