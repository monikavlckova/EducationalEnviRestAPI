using System.Diagnostics.CodeAnalysis;
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
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var classroom = await dbContext.Classrooms.FindAsync(id);

        if (classroom == null) return NotFound();

        return Ok(classroom);
    }
    
    [HttpGet]
    [Route("getByTeacherId/{teacherId:int}")]
    public async Task<IActionResult> GetAllTeachersClassrooms([FromRoute] int teacherId)
    {
        var result = (from c in dbContext.Classrooms.AsQueryable()
            join t in dbContext.Teachers.AsQueryable().Where(x => x.Id == teacherId)
                on c.TeacherId equals t.Id 
            select new { c.Id, c.TeacherId, c.Name, c.ImagePath });

        return Ok(await result.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Classroom addClassroom)
    {
        if (addClassroom is null) throw new ArgumentNullException(nameof(addClassroom));

        await dbContext.Classrooms.AddAsync(addClassroom);
        await dbContext.SaveChangesAsync();

        return Ok(addClassroom);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Classroom updateClassroom)
    {
        var classroom = await dbContext.Classrooms.FindAsync(id);

        if (classroom == null) return NotFound();
        
        classroom.Name = updateClassroom.Name;
        classroom.ImagePath = updateClassroom.ImagePath;

        await dbContext.SaveChangesAsync();

        return Ok(classroom);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var classroom = await dbContext.Classrooms.FindAsync(id);

        if (classroom == null) return NotFound();
        
        dbContext.Remove(classroom);
        await dbContext.SaveChangesAsync();

        return Ok(classroom);
    }
}