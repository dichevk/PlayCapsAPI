namespace PlayCapsViewer.Models
{
    public class PlayCapsPlayer
    {
        public int PlayCapId { get; set; }

        public int PlayerId { get; set; }   

        public PlayCap PlayCap { get; set; }

        public Player Player { get; set; }
    }
}
