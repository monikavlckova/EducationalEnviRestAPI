using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsTasksController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public GroupsTasksController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.GroupsTasks.ToListAsync());
    }

/*    [HttpGet]
    public async Task<IActionResult> GetGroupsTasks(Guid groupId)
    {
        //TODO
        return Ok();
    }*/

    [HttpPut]
    public async Task<IActionResult> AddTaskToGroup(Guid taskId, Guid groupId)
    {
        var groupTask = new GroupTask()
        {
            Id = Guid.NewGuid(),
            GroupId = groupId,
            TaskId = taskId
        };

        await dbContext.GroupsTasks.AddAsync(groupTask);
        await dbContext.SaveChangesAsync();

        return Ok(groupTask);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var groupTask = await dbContext.GroupsTasks.FindAsync(id);

        if (groupTask == null) return NotFound();
        dbContext.Remove(groupTask);
        await dbContext.SaveChangesAsync();

        return Ok(groupTask);

    }
}