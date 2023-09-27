using System;
using Forum.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Forum.ViewComponents
{
   // similar to a partial view but can have its
   //own logic, encapsulated in a class, and can
   //be invoked from a view.


    public class Navigation 
	{
        private readonly DataContext _context;

        public Navigation(DataContext context)
        {
            _context = context;
        }
    }
}

