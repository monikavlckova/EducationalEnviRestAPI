using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassroomTaskController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public ClassroomTaskController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.ClassroomTasks.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> AddTaskToClassroom(ClassroomTask addClassroomTask)
    {
        if (addClassroomTask is null) throw new ArgumentNullException(nameof(addClassroomTask));

        var existingClassroom = await dbContext.Classrooms.FindAsync(addClassroomTask.ClassroomId);
        if (existingClassroom == null) return BadRequest("Classroom with the specified Id not found.");

        var existingTask = await dbContext.Tasks.FindAsync(addClassroomTask.TaskId);
        if (existingTask == null) return BadRequest("Task with the specified Id not found.");

        await dbContext.ClassroomTasks.AddAsync(addClassroomTask);
        await dbContext.SaveChangesAsync();

        return Ok(addClassroomTask);
    }

    [HttpDelete]
    [Route("{classroomId:int}/{taskId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int classroomId, [FromRoute] int taskId)
    {
        var classroomTask = await dbContext.ClassroomTasks.FindAsync(classroomId, taskId);

        if (classroomTask == null) return NotFound();

        dbContext.Remove(classroomTask);
        await dbContext.SaveChangesAsync();

        return Ok(classroomTask);
    }
}