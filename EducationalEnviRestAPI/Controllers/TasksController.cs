using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public TasksController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Tasks.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(Taskk addTask)
    {
        var task = new Taskk()
        {
            Name = addTask.Name,
            Text = addTask.Text
        };

        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();

        return Ok(task);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Taskk updateTask)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();
        task.Name = updateTask.Name;
        task.Text = updateTask.Text;

        await dbContext.SaveChangesAsync();

        return Ok(task);

    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();
        dbContext.Remove(task);
        await dbContext.SaveChangesAsync();

        return Ok(task);

    }
}