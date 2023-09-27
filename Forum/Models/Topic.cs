using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
	public class Topic
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
    }
}

