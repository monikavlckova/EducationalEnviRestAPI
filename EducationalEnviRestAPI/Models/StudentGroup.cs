namespace EducationalEnviRestAPI.Models;

public class StudentGroup
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid GroupId { get; set; }
}