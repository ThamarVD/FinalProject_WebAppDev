using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers;

public class PersonController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;
    
    public IActionResult View(int id)
    {
        var person = _context.People
            .Include(p => p.Hobbies)
            .Include(p => p.Degree)
            .FirstOrDefault(p => p.Id == id);
        
        if (person == null)
        {
            return NotFound();
        }
        
        return View(person);
    }
}