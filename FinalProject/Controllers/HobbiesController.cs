﻿using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class HobbiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
