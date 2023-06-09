﻿using System.ComponentModel.DataAnnotations;

namespace PlayCapsViewer.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public decimal Rating { get; set; }

        public Reviewer Reviewer { get; set; }
        public PlayCap PlayCap { get; set; }
    }
}
