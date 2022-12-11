namespace EducationalEnviRestAPI.Models;

public class StudentTask
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid TaskId { get; set; }
}