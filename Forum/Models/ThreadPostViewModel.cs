using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Forum.Models
{
	public class ThreadPostViewModel
	{
        public string? ThreadHeading { get; set; }

        public string? PostTitle { get; set; }

        public string TextBody { get; set; }

    }
}

