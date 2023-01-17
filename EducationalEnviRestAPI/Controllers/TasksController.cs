using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public TaskController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Tasks.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Taskk addTask)
    {
        var task = new Taskk()
        {
            Id = Guid.NewGuid(),
            Name = addTask.Name,
            Text = addTask.Text
        };

        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();

        return Ok(task);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, Taskk updateTask)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();
        task.Name = updateTask.Name;
        task.Text = updateTask.Text;

        await dbContext.SaveChangesAsync();

        return Ok(task);

    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();
        dbContext.Remove(task);
        await dbContext.SaveChangesAsync();

        return Ok(task);

    }
}