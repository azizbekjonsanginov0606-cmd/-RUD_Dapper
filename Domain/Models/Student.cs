namespace Domain.Models;

public class Student
{
    public int StudentId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateOnly EnrollmentDate { get; set; }
}
