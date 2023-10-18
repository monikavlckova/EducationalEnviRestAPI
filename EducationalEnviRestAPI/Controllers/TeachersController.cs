using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public TeachersController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Teachers.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var teacher = await dbContext.Teachers.FindAsync(id);

        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }
    
    [HttpGet]
    [Route("getAllClassrooms/{id:int}")]
    public async Task<IActionResult> GetAllClassrooms([FromRoute] int id)
    {
        var result = (from c in dbContext.Classrooms.AsQueryable() 
            join t in dbContext.Teachers
                on c.TeacherId equals t.Id 
            where t.Id == id
            select new { c.Id, c.TeacherId, c.Name });
        
        return Ok(await result.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Teacher addTeacher)
    {
        var teacher = new Teacher()
        {
            Id = addTeacher.Id,
            Name = addTeacher.Name,
            LastName = addTeacher.LastName,
            UserName = addTeacher.UserName,
            Password = addTeacher.Password,
            Email = addTeacher.Email
        };

        await dbContext.Teachers.AddAsync(teacher);
        await dbContext.SaveChangesAsync();

        return Ok(teacher);
    }
    
    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Teacher updateTeacher)
    {
        var teacher = await dbContext.Teachers.FindAsync(id);

        if (teacher == null) return NotFound();
        teacher.Name = updateTeacher.Name;
        teacher.LastName = updateTeacher.LastName;
        teacher.UserName = updateTeacher.UserName;
        teacher.Password = updateTeacher.Password;
        teacher.Email = updateTeacher.Email;

        await dbContext.SaveChangesAsync();

        return Ok(teacher);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var teacher = await dbContext.Teachers.FindAsync(id);

        if (teacher == null) return NotFound();
        dbContext.Remove(teacher);
        await dbContext.SaveChangesAsync();

        return Ok(teacher);
    }
    

}