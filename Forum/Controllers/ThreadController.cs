using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Forum.Models.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ThreadController : Controller
    {

        private readonly DataContext _context;

        public ThreadController(DataContext context)
        {
            _context = context;
        }


        // Retrieves all posts for a specified thread from the db,
        // sets the threads heading to the ViewBag and returns a
        // view displaying these posts
        public ActionResult Index(int id)
        {
            var dbHelper = new DBhelper(_context);

            List<ThreadPostViewModel> posts = dbHelper.LoadPosts(id);

            var thread = posts.Where(post => post.ThreadReferenceId == id).FirstOrDefault();

            ViewBag.ThreadHeading = thread.ThreadHeading;

            return View(posts);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        // attempts to create a new thread and an associated post in the
        // database if successful redirects to the topic index page
        // else to an error page

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TopicThreadViewModel thread)
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
            .Where(modelStateEntry => modelStateEntry.Value.Errors.Count > 0)
            .Select(modelStateEntry => new { modelStateEntry.Key, modelStateEntry.Value.Errors })
            .ToArray();

            Debug.WriteLine("Validation Errors occured in Create Method: " + errors);
            return RedirectToAction("Error", "Home");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }


        // Attempts to delete a post with a specified ID from the db
        // and then redirects to the Index action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var postToDelete = _context.Post.Find(id);

                if (postToDelete == null)
                {
                      return NotFound();
                }

                  _context.Post.Remove(postToDelete);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

