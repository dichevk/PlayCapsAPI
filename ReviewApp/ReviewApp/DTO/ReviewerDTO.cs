﻿using PlayCapsViewer.Models;

namespace PlayCapsViewer.DTO
{
    public class ReviewerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Country Country { get; set; }
    }
}
