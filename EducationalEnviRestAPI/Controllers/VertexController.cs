using System.Diagnostics.CodeAnalysis;
using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VertexController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public VertexController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Vertices.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var vertex = await dbContext.Vertices.FindAsync(id);

        if (vertex == null) return NotFound();

        return Ok(vertex);
    }

    [HttpGet]
    [Route("getByTaskId/{taskkId:int}")]
    public async Task<IActionResult> GetByTaskIdVertices([FromRoute] int taskkId)
    {
        var result = dbContext.Vertices.AsQueryable().Where(x => x.TaskId == taskkId);

        return Ok(await result.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Vertex addVertex)
    {
        if (addVertex is null) throw new ArgumentNullException(nameof(addVertex));

        await dbContext.Vertices.AddAsync(addVertex);
        await dbContext.SaveChangesAsync();

        return Ok(addVertex);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Vertex updateVertex)
    {
        var vertex = await dbContext.Vertices.FindAsync(id);

        if (vertex == null) return NotFound();
        
        vertex.TaskId = updateVertex.TaskId;
        vertex.Column = updateVertex.Column;
        vertex.Row = updateVertex.Row;
        vertex.ColumnsCount = updateVertex.ColumnsCount;
        vertex.RowsCount = updateVertex.RowsCount;
        vertex.ImageId = updateVertex.ImageId;

        await dbContext.SaveChangesAsync();

        return Ok(vertex);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var vertex = await dbContext.Vertices.FindAsync(id);

        if (vertex == null) return NotFound();

        dbContext.Remove(vertex);
        await dbContext.SaveChangesAsync();

        return Ok(vertex);
    }
}