using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class GroupTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int TaskId { get; set; }
}