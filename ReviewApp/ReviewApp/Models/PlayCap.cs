using System.ComponentModel.DataAnnotations;

namespace PlayCapsViewer.Models
{
    public class PlayCap
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Reviewer> Reviewers { get; set; }
        public ICollection<Player> Players { get; set; }

    }
}
