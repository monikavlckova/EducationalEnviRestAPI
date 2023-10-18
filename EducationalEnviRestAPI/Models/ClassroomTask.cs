using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class ClassroomTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ClassroomId { get; set; }
    public int TaskId { get; set; }
}