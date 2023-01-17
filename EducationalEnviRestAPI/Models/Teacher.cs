using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

[Table("Teacher")]
public class Teacher : User
{
    public string Email { get; set; }
}