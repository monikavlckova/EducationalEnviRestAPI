namespace EducationalEnviRestAPI.Models;

public class Classroom
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public string Name { get; set; }
}