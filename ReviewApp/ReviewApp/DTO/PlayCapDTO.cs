﻿using PlayCapsViewer.Data.Enums;

namespace PlayCapsViewer.DTO
{
    public class PlayCapDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PlayCapRarity Rarity { get; set; }
    }
}
