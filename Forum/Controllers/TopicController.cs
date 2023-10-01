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

        // Retrieves a list of threads for a given topic and a list of all topics from the db
        // Finds the topic with the provided ID and sets its title to the ViewBag
        // Returns a view displaying the list of threads for the specified topic

        public IActionResult Index(int id)
        {
            var dbHelper = new DBhelper(_context);

            List<TopicThreadViewModel> threads = dbHelper.LoadThreads(id);

            List<TopicViewModel> topics = dbHelper.LoadTopics();

            var topic = topics.Where(topic => topic.Id == id).FirstOrDefault();

            ViewBag.TopicTitle = topic.Title;

            return View(threads);
        }
    }
}


