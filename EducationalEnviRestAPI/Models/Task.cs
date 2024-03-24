using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int TaskTypeId { get; set; }
    [Column(TypeName = "varchar(50)")] public String Name { get; set; }
    public String Text { get; set; }
    public int? ImageId { get; set; }
    
    public DateTime? Deadline { get; set; }
}