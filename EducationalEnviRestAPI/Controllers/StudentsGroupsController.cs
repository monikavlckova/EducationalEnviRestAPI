using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsGroupsController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsGroupsController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpPut]
    public async Task<IActionResult> AddStudentToGroup(int studentId, int groupId)
    {
        var studentGroup = new StudentGroup()
        {
            StudentId = studentId,
            GroupId = groupId
        };

        await dbContext.StudentsGroups.AddAsync(studentGroup);
        await dbContext.SaveChangesAsync();

        return Ok(studentGroup);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteStudentFromGroup([FromRoute] int id)
    {
        var studentGroup = await dbContext.StudentsGroups.FindAsync(id);

        if (studentGroup == null) return NotFound();
        dbContext.Remove(studentGroup);
        await dbContext.SaveChangesAsync();

        return Ok(studentGroup);

    }
}