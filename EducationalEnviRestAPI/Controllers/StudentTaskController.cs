using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentTaskController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentTaskController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.StudentTasks.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> AddTaskToStudent(StudentTask addStudentTask)
    {
        var existingStudent = await dbContext.Students.FindAsync(addStudentTask.StudentId);
        if (existingStudent == null) return BadRequest("Student with the specified Id not found.");

        var existingTask = await dbContext.Tasks.FindAsync(addStudentTask.TaskId);
        if (existingTask == null) return BadRequest("Task with the specified Id not found.");

        await dbContext.StudentTasks.AddAsync(addStudentTask);
        await dbContext.SaveChangesAsync();

        return Ok(addStudentTask);
    }

    [HttpDelete]
    [Route("{studentId:int}/{taskId:int}")]
    public async Task<IActionResult> DeleteStudentTask([FromRoute] int studentId, [FromRoute] int taskId)
    {
        var studentTask = await dbContext.StudentTasks.FindAsync(studentId, taskId);

        if (studentTask == null) return NotFound();

        dbContext.Remove(studentTask);
        await dbContext.SaveChangesAsync();

        return Ok(studentTask);
    }
}