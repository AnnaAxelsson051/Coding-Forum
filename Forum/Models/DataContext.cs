using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Forum.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

    }
}



