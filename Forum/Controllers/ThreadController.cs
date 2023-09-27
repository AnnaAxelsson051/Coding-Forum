using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class ThreadController : Controller
    {

        private readonly DataContext _context;

        public ThreadController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

