using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

[Table("Student")]
public class Student : User
{
    public int ClassroomId { get; set; }
}