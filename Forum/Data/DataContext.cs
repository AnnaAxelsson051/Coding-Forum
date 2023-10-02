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

        public DbSet<Post> Post { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Thread> Thread { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().HasData(new Topic[] {
                new Topic{Id=1, Title="Backend Development"},
                new Topic{Id=2, Title="Frontend Development"},
                new Topic{Id=3, Title="Fullstack Development"},
                new Topic{Id=4, Title="Database Design and Management"},
                new Topic{Id=5, Title="Web Security"},
                new Topic{Id=6, Title="Algorithm and Data Structures"},
            });
            modelBuilder.Entity<Thread>().HasData(new Thread[] {
                new Thread{Id=1, Heading=".NET", TopicReferenceId=1 },
                new Thread{Id=2, Heading="Responsive Design", TopicReferenceId=2},
                new Thread{Id=3, Heading="MERN", TopicReferenceId=3},
            });
            modelBuilder.Entity<Post>().HasData(new Post[] {
                new Post{Id=1, Title="Handling Large Data Sets with Entity Framework Core and .NET 6", TextBody="I'm currently developing a RESTful API using ASP.NET Core 6.0 and Entity Framework Core. I've found that when dealing with large datasets, my application tends to slow down..", ThreadReferenceId=1 },
                new Post{Id=2, Title="Breakpoints - Best Practices in 2023?", TextBody="I'm diving deeper into responsive web design and want to ensure I'm setting up my breakpoints correctly for a wide range of devices..", ThreadReferenceId=2 },
                new Post{Id=3, Title="Building a Fullstack MERN App from scratch", TextBody="I've recently started exploring the MERN stack (MongoDB, Express.js, React, and Node.js) and I'm thinking of building a fullstack application..", ThreadReferenceId=3 },
            });
        }
    }
}


