using PlayCapsViewer.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlayCapsViewer.Models
{
    public class PlayCap
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<PlayCapsPlayer> PlayCapsPlayer { get; set; }
        public ICollection<PlayCapsCategory> PlayCapsCategory { get; set; }
        public PlayCapRarity Rarity { get; set; }

    }
}
