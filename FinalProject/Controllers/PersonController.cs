using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers;

public class PersonController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;
    
    public ViewResult View(int id)
    {
        var person = _context.People
            .Include(p => p.Hobbies)
            .Include(p => p.Degree)
            .FirstOrDefault(p => p.Id == id);
        
        return View(person);
    }
    
    [HttpGet]
    public ViewResult Add()
    {
        var degrees = _context.Degrees.ToList();
        ViewBag.Degrees = degrees;
        
        var hobbies = _context.Hobbies.ToList();
        ViewBag.Hobbies = hobbies;
        
        return View(new AddPerson()
        {
            Hobbies = new List<int>()
        });
    }
    
    [HttpPost]
    public IActionResult Add(AddPerson person)
    {
        var degrees = _context.Degrees.ToList();
        ViewBag.Degrees = degrees;

        
        if (ModelState.IsValid && _context.Degrees.Any(d => d.Id == person.DegreeId))
        {
            var finalPerson = new Person()
            {
                Id = 0,
                Name = person.Name,
                About = person.About,
                Degree = _context.Degrees.FirstOrDefault(d => d.Id == person.DegreeId) ?? new Degree(),
                Hobbies = _context
                    .Hobbies
                    .Where(h => person.Hobbies!.Contains(h.Id))
                    .ToList()
            };
            
            _context.People.Add(finalPerson);
            _context.SaveChanges();
            
            return Redirect($"/");
        }

        ModelState.AddModelError(String.Empty, "Please Correct All Errors");
        return Add();
    }
}