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
        
        return View(new Person()
        {
            Hobbies = new List<Hobby>()
        });
    }
    
    [HttpPost]
    public IActionResult Add(Person person)
    {
        var degrees = _context.Degrees.ToList();
        ViewBag.Degrees = degrees;
        
        if (ModelState.IsValid)
        {
            if (person.Hobbies.Any())
            {
                person.Hobbies = _context.Hobbies
                    .Where(h => person.Hobbies.Select(ph => ph.Id).Contains(h.Id))
                    .ToList();
            }
            
            var addedPerson = _context.People.Add(person);
            _context.SaveChanges();
            
            return Redirect($"/");
        }

        ModelState.AddModelError(String.Empty, "Please Correct All Errors");
        return Add();
    }
}