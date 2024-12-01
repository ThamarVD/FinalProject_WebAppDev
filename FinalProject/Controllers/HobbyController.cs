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
    }
}