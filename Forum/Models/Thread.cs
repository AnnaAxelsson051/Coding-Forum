using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Heading { get; set; }

        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
