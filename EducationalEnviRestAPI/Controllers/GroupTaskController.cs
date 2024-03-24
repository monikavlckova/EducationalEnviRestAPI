using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupTaskController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public GroupTaskController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.GroupTasks.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> AddTaskToGroup(GroupTask addGroupTask)
    {
        if (addGroupTask is null) throw new ArgumentNullException(nameof(addGroupTask));

        var existingGroup = await dbContext.Groups.FindAsync(addGroupTask.GroupId);
        if (existingGroup == null) return BadRequest("Group with the specified Id not found.");

        var existingTask = await dbContext.Tasks.FindAsync(addGroupTask.TaskId);
        if (existingTask == null) return BadRequest("Task with the specified Id not found.");

        await dbContext.GroupTasks.AddAsync(addGroupTask);
        await dbContext.SaveChangesAsync();

        return Ok(addGroupTask);
    }

    [HttpDelete]
    [Route("{groupId:int}/{taskId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int groupId, [FromRoute] int taskId)
    {
        var groupTask = await dbContext.GroupTasks.FindAsync(groupId, taskId);

        if (groupTask == null) return NotFound();

        dbContext.Remove(groupTask);
        await dbContext.SaveChangesAsync();

        return Ok(groupTask);
    }
}