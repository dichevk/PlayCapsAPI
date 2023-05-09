using PlayCapsViewer.Models;

namespace PlayCapsViewer.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gym { get; set; }

        public Country Country { get; set; }

    }
}
