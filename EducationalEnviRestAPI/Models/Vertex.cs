using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class Vertex
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Column { get; set; }
    public int Row { get; set; }
    public int ColumnsCount { get; set; }
    public int RowsCount { get; set; }
    public int? ImageId { get; set; }
    public int TaskId { get; set; }
}