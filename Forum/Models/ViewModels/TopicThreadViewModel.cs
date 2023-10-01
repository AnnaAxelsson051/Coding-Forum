using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Forum.Models
{
	public class TopicThreadViewModel
	{
        [Required(ErrorMessage = "Please enter a title")]
        [DisplayName("Thread Title")]
        public string ThreadHeading { get; set; }

        [DisplayName("Title")]
        [AllowNull]
        public string? PostTitle { get; set; }

        public int ThreadId { get; set; }

        public int ThreadReferenceId { get; set; }

        public int TopicReferenceId { get; set; }

        [DisplayName("Content")]
        [Required(ErrorMessage = "Enter text")]
        [DataType(DataType.MultilineText)]
        public string TextBody { get; set; }
    }
}

