using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassroomsTasksController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public ClassroomsTasksController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpPut]
    public async Task<IActionResult> AddTaskToClassroom(int taskId, int classroomId)
    {
        var classroomTask = new ClassroomTask()
        {
            ClassroomId = classroomId,
            TaskId = taskId
        };

        await dbContext.ClassroomsTasks.AddAsync(classroomTask);
        await dbContext.SaveChangesAsync();

        return Ok(classroomTask);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var classroomTask = await dbContext.ClassroomsTasks.FindAsync(id);

        if (classroomTask == null) return NotFound();
        dbContext.Remove(classroomTask);
        await dbContext.SaveChangesAsync();

        return Ok(classroomTask);
    }
}