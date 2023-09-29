using System;
using Forum.Models;
using Forum.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Forum.ViewComponents
{
   // similar to a partial view but can have its
   //own logic, encapsulated in a class, and can
   //be invoked from a view.


    public class Navigation : ViewComponent
	{
        private readonly DataContext _context;

        public Navigation(DataContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var dbHelper = new DBhelper(_context);
            List<TopicViewModel> topics = dbHelper.LoadTopics();

            return View(topics);
        }
    }
}
/*
private readonly DataContext _context;

public Navigation(DataContext context)
{
    _context = context;
}
}
}*/

