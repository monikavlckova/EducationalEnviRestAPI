using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalEnviRestAPI.Models;

public class Edge
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Value { get; set; }
    public int? ImageId { get; set; }
    public int fromVertexId { get; set; }
    public int toVertexId { get; set; }
}