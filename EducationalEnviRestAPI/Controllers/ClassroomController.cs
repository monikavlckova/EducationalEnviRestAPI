using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassroomController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public ClassroomController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Classrooms.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Classroom addClassroom)
    {
        var classroom = new Classroom()
        {
            Id = Guid.NewGuid(),
            TeacherId = addClassroom.TeacherId,
            Name = addClassroom.Name
        };

        await dbContext.Classrooms.AddAsync(classroom);
        await dbContext.SaveChangesAsync();

        return Ok(classroom);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, Classroom updateClassroom)
    {
        var classroom = await dbContext.Classrooms.FindAsync(id);

        if (classroom == null) return NotFound();
        classroom.Name = updateClassroom.Name;

        await dbContext.SaveChangesAsync();

        return Ok(classroom);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var classroom = await dbContext.Classrooms.FindAsync(id);

        if (classroom == null) return NotFound();
        dbContext.Remove(classroom);
        await dbContext.SaveChangesAsync();

        return Ok(classroom);
    }
}