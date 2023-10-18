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
    [Route("getAllStudents/{id:int}")]
    public async Task<IActionResult> GetAllStudents([FromRoute] int id)
    {
        var query = dbContext.Students.AsQueryable();
        query = query.Where(student => student.ClassroomId.Equals(id));
        return Ok(await query.ToListAsync());
    }
    
        
    [HttpGet]
    [Route("getAllGroups/{id:int}")]
    public async Task<IActionResult> GetAllGroups([FromRoute] int id)
    {
        var result = (from g in dbContext.Groups.AsQueryable() 
            join c in dbContext.Classrooms
                on g.ClassroomId equals c.Id 
            where c.Id == id
            select new { g.Id, g.ClassroomId, g.Name });
        
        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("getAllTasks/{id:int}")]
    public async Task<IActionResult> GetAllTasks([FromRoute] int id)
    {
        var result = (from t in dbContext.Tasks.AsQueryable()
            join ct in dbContext.ClassroomsTasks on t.Id equals ct.ClassroomId
            where ct.ClassroomId == id
            select new { t.Id, t.Name, t.Text });
        
        return Ok(await result.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Classroom addClassroom)
    {
        var classroom = new Classroom()
        {
            TeacherId = addClassroom.TeacherId,
            Name = addClassroom.Name
        };

        await dbContext.Classrooms.AddAsync(classroom);
        await dbContext.SaveChangesAsync();

        return Ok(classroom);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Classroom updateClassroom)
    {
        var classroom = await dbContext.Classrooms.FindAsync(id);

        if (classroom == null) return NotFound();
        classroom.Name = updateClassroom.Name;

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