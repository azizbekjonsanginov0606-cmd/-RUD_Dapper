namespace Domain.Models;

public class Mentor
{
    public int MentorId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string Specialization { get; set; } = null!;
}
