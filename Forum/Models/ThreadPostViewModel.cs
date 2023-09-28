using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Forum.Models
{
	public class ThreadPostViewModel
	{
        public string? ThreadHeading { get; set; }

        [DisplayName("Post Title")]
        [AllowNull]
        public string? PostTitle { get; set; }

        [Required(ErrorMessage = "Please add content")]
        [DisplayName("Content")]
        public string TextBody { get; set; }

        public int PostId { get; set; }

        public int ThreadReferenceId { get; set; }
    }
}

