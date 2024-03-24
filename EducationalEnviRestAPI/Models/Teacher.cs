using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

[Table("Teacher")]
public class Teacher
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(254)")] public string Email { get; set; }
    [Column(TypeName = "varchar(30)")] public string Name { get; set; }
    [Column(TypeName = "varchar(30)")] public string LastName { get; set; }
    [Column(TypeName = "varchar(30)")] public string UserName { get; set; }
    [Column(TypeName = "varchar(50)")] public string Password { get; set; }
    public int? ImageId { get; set; }
}