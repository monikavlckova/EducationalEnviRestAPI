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
    
    [HttpPost]
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
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var teacher = await dbContext.Teachers.FindAsync(id);

        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, Teacher updateTeacher)
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
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var teacher = await dbContext.Teachers.FindAsync(id);

        if (teacher == null) return NotFound();
        dbContext.Remove(teacher);
        await dbContext.SaveChangesAsync();

        return Ok(teacher);
    }
    

}