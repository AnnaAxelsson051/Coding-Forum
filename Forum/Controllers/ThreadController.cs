using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Forum.Models.Helper;
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

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Index(int id)
        {
            var dbHelper = new DBhelper(_context);

            // Gets post in thread
            List<ThreadPostViewModel> posts = dbHelper.LoadPosts(id);

            var thread = posts.Where(post => post.ThreadReferenceId == id).FirstOrDefault();

            ViewBag.ThreadHeading = thread.ThreadHeading;

            return View(posts);
        }

        // attempts to create a new thread and an associated post in the
        // database if successful redirects to the topic index page
        // else to an error page

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TopicThreadViewModel thread)
        {
            var dbHelper = new DBhelper(_context);

            if (ModelState.IsValid)
            {
                var newThread = new Models.Thread
                {
                    Heading = thread.ThreadHeading,
                    TopicReferenceId = thread.TopicReferenceId,

                };
                var newThreadPost = new Post
                {
                    TextBody = thread.TextBody,
                    Title = thread.PostTitle,
                    ThreadReferenceId = thread.ThreadReferenceId,
                };

                try
                {
                    dbHelper.AddThreadPost(newThread, newThreadPost);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
                return RedirectToAction("Index", "Topic", new { id = thread.TopicReferenceId });
            }
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            Debug.WriteLine(errors + "Error");
            return RedirectToAction("Error", "Home");
        }
    }
}

