/*using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; }
    
    [Column(TypeName = "varchar(30)")]
    public string LastName { get; set; }
    
    [Column(TypeName = "varchar(30)")]
    public string UserName { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string Password { get; set; }
    
    //public Student? Student
    //public Teacher? Teacher
}*/