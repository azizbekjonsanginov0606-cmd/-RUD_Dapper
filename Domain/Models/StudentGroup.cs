namespace Domain.Models;

public class StudentGroup
{
    public int StudentGroupId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public StudentStatus Status { get; set; }
}
