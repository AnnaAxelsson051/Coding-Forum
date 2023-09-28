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
    public class PostController : Controller
    {

        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(ThreadPostViewModel post)
        {
            var dbHelper = new DBhelper(_context);

            if (ModelState.IsValid)
            {
                var newPost = new Post
                {
                    TextBody = post.TextBody,
                    Title = post.PostTitle,
                    ThreadReferenceId = post.ThreadReferenceId,
                };
                try
                {

                    dbHelper.AddPost(newPost);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
                return RedirectToAction("Index", "Thread", new { id = post.ThreadReferenceId });
            }
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();

            Debug.WriteLine(errors + "Errors occured");
            return RedirectToAction("Error", "Home");
        }


    }
}


