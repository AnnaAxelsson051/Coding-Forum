using System;
namespace Forum.Models.Helper
{
	public class DBhelper
	{
        private readonly DataContext _context;

        public DBhelper(DataContext context)
        {
            _context = context;
        }
    }
}

