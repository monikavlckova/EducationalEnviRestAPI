using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTypeController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public TaskTypeController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.TaskTypes.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var taskType = await dbContext.TaskTypes.FindAsync(id);

        if (taskType == null) return NotFound();

        return Ok(taskType);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TaskType addTaskType)
    {
        if (addTaskType is null) throw new ArgumentNullException(nameof(addTaskType));


        await dbContext.TaskTypes.AddAsync(addTaskType);
        await dbContext.SaveChangesAsync();

        return Ok(addTaskType);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, TaskType updateTaskType)
    {
        var taskType = await dbContext.TaskTypes.FindAsync(id);

        if (taskType == null) return NotFound();

        taskType.Name = updateTaskType.Name;
        taskType.ImageId = updateTaskType.ImageId;

        await dbContext.SaveChangesAsync();

        return Ok(taskType);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var taskType = await dbContext.TaskTypes.FindAsync(id);

        if (taskType == null) return NotFound();

        dbContext.Remove(taskType);
        await dbContext.SaveChangesAsync();

        return Ok(taskType);
    }
}