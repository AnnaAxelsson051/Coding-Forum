using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Forum.Models.Helper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class TopicController : Controller
    {
        private readonly DataContext _context;

        public TopicController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var dbHelper = new DBhelper(_context);

            List<TopicThreadViewModel> threads = dbHelper.LoadThreads(id);

            List<TopicViewModel> subjects = dbHelper.LoadTopics();

            var subject = subjects.Where(subject => subject.Id == id).FirstOrDefault();

            ViewBag.SubjectTitle = subject.Title;

            return View(threads);
        }
    }
}


