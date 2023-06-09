﻿using System.ComponentModel.DataAnnotations;

namespace PlayCapsViewer.Models
{
    public class Reviewer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Country Country { get; set; }
        public ICollection <Review> Reviews { get; set; }
    }
}
