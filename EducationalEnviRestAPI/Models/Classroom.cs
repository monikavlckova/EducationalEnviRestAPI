using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class Classroom
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? TeacherId { get; set; }
    [Column(TypeName = "varchar(50)")] public string Name { get; set; }
    public string? ImagePath { get; set; }
    
    
}