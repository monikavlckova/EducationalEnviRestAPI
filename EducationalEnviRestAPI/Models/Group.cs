namespace EducationalEnviRestAPI.Models;

public class Group
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public string Name { get; set; }
}