namespace PlayCapsViewer.Models
{
    public class PlayCapsCategory
    {
        public int PlayCapId { get; set; }
        public int CategoryId { get; set; }
        public PlayCap PlayCap { get; set; }
        public Category Category { get; set; }
    }
}
