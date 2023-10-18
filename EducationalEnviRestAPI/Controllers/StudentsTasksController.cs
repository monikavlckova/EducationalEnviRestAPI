using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsTasksController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsTasksController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPut]
    public async Task<IActionResult> AddTaskToStudent(int taskId, int studentId)
    {
        var studentTask = new StudentTask()
        {
            StudentId = studentId,
            TaskId = taskId
        };

        await dbContext.StudentsTasks.AddAsync(studentTask);
        await dbContext.SaveChangesAsync();

        return Ok(studentTask);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteStudentTask([FromRoute] int id)
    {
        var studentTask = await dbContext.StudentsTasks.FindAsync(id);

        if (studentTask == null) return NotFound();
        dbContext.Remove(studentTask);
        await dbContext.SaveChangesAsync();

        return Ok(studentTask);

    }
}