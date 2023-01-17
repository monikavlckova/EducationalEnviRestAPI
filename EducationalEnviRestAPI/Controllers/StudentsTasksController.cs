using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsTaskController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsTaskController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.StudentsTasks.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> AddTaskToStudent(Guid taskId, Guid studentId)
    {
        var studentTask = new StudentTask()
        {
            Id = Guid.NewGuid(),
            StudentId = studentId,
            TaskId = taskId
        };

        await dbContext.StudentsTasks.AddAsync(studentTask);
        await dbContext.SaveChangesAsync();

        return Ok(studentTask);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteStudentTask([FromRoute] Guid id)
    {
        var studentTask = await dbContext.StudentsTasks.FindAsync(id);

        if (studentTask == null) return NotFound();
        dbContext.Remove(studentTask);
        await dbContext.SaveChangesAsync();

        return Ok(studentTask);

    }
}