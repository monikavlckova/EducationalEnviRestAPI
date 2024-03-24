using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EdgeController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public EdgeController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Edges.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var edge = await dbContext.Edges.FindAsync(id);

        if (edge == null) return NotFound();

        return Ok(edge);
    }

    [HttpGet]
    [Route("getByFromVertexId/{vertexId:int}")]
    public async Task<IActionResult> GetByFromVertexId([FromRoute] int vertexId)
    {
        var result = dbContext.Edges.AsQueryable().Where(x => x.fromVertexId == vertexId);

        return Ok(await result.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Edge addEdge)
    {
        if (addEdge is null) throw new ArgumentNullException(nameof(addEdge));

        await dbContext.Edges.AddAsync(addEdge);
        await dbContext.SaveChangesAsync();

        return Ok(addEdge);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Edge updateEdge)
    {
        var edge = await dbContext.Edges.FindAsync(id);

        if (edge == null) return NotFound();

        edge.Value = updateEdge.Value;
        edge.fromVertexId = updateEdge.fromVertexId;
        edge.toVertexId = updateEdge.toVertexId;
        edge.ImageId = updateEdge.ImageId;

        await dbContext.SaveChangesAsync();

        return Ok(edge);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var edge = await dbContext.Edges.FindAsync(id);

        if (edge == null) return NotFound();

        dbContext.Remove(edge);
        await dbContext.SaveChangesAsync();

        return Ok(edge);
    }
}