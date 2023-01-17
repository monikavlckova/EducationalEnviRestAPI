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
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.StudentsGroups.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> AddStudentToGroup(Guid studentId, Guid groupId)
    {
        var studentGroup = new StudentGroup()
        {
            Id = Guid.NewGuid(),
            StudentId = studentId,
            GroupId = groupId
        };

        await dbContext.StudentsGroups.AddAsync(studentGroup);
        await dbContext.SaveChangesAsync();

        return Ok(studentGroup);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteStudentFromGroup([FromRoute] Guid id)
    {
        var studentGroup = await dbContext.StudentsGroups.FindAsync(id);

        if (studentGroup == null) return NotFound();
        dbContext.Remove(studentGroup);
        await dbContext.SaveChangesAsync();

        return Ok(studentGroup);

    }
}