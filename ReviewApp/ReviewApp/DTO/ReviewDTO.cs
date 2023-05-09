using PlayCapsViewer.Models;

namespace PlayCapsViewer.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int Rating { get; set; }

        public Reviewer Reviewer { get; set; }
        public PlayCap PlayCap { get; set; }
    }
}
