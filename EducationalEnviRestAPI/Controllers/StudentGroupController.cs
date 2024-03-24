using System.Diagnostics;
using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentGroupController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentGroupController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.StudentGroups.ToListAsync());
    }

    [HttpGet]
    [Route("getByClassroomId/{classroomId:int}")]
    public async Task<IActionResult> GetAllClassroomStudentsGroups([FromRoute] int classroomId)
    {
        var result = from g in dbContext.Groups.AsQueryable().Where(x => x.ClassroomId == classroomId)
            join sg in dbContext.StudentGroups
                on g.Id equals sg.GroupId
            select new { sg.StudentId, sg.GroupId };

        return Ok(await result.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> AddStudentToGroup(StudentGroup addStudentGroup)
    {
        var existingGroup = await dbContext.Groups.FindAsync(addStudentGroup.GroupId);
        if (existingGroup == null) return BadRequest("Group with the specified Id not found.");

        var existingStudent = await dbContext.Students.FindAsync(addStudentGroup.StudentId);
        if (existingStudent == null) return BadRequest("Student with the specified Id not found.");

        await dbContext.StudentGroups.AddAsync(addStudentGroup);
        await dbContext.SaveChangesAsync();

        return Ok(addStudentGroup);
    }

    [HttpDelete]
    [Route("{studentId:int}/{taskId:int}")]
    public async Task<IActionResult> DeleteStudentFromGroup([FromRoute] int studentId, [FromRoute] int taskId)
    {
        var studentGroup = await dbContext.StudentGroups.FindAsync(studentId, taskId);

        if (studentGroup == null) return NotFound();

        dbContext.Remove(studentGroup);
        await dbContext.SaveChangesAsync();

        return Ok(studentGroup);
    }
}