using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

[Table("Student")]
public class Student
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? ClassroomId { get; set; }
    [Column(TypeName = "varchar(30)")] public string Name { get; set; }
    [Column(TypeName = "varchar(30)")] public string LastName { get; set; }
    [Column(TypeName = "varchar(50)")] public string LoginCode { get; set; }
    public int? ImageId { get; set; }
}