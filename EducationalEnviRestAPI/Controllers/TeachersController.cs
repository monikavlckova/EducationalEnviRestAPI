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

        if (teacher == null) return NotFound();

        return Ok(teacher);
    }
    
    [HttpGet]
    [Route("getByEmail/{email}")]
    public async Task<IActionResult> Get([FromRoute] string email)
    {
        var teacher = await dbContext.Teachers.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);
        
        if (teacher == null) return NotFound();
        
        return Ok(teacher);
    }
    
    [HttpGet]
    [Route("getByUserName/{username}")]
    public async Task<IActionResult> GetByUserName([FromRoute] string username)
    {
        var teacher = await dbContext.Teachers.AsQueryable().FirstOrDefaultAsync(x => x.UserName == username);
        
        if (teacher == null) return NotFound();
        
        return Ok(teacher);
    }
    
    [HttpGet]
    [Route("getByLogin/{username}/{password}")]
    public async Task<IActionResult> Get([FromRoute] string username, [FromRoute] string password)
    {
        var teacher = await dbContext.Teachers.AsQueryable().FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        
        if (teacher == null) return NotFound();
        
        return Ok(teacher);
    }

    [HttpPut]
    public async Task<IActionResult> Add(Teacher addTeacher)
    {
        if (addTeacher is null) throw new ArgumentNullException(nameof(addTeacher));

        await dbContext.Teachers.AddAsync(addTeacher);
        await dbContext.SaveChangesAsync();

        return Ok(addTeacher);
    }
    
    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Teacher updateTeacher)
    {
        var teacher = await dbContext.Teachers.FindAsync(id);

        if (teacher == null) return NotFound();
        
        teacher.Email = updateTeacher.Email;
        teacher.Name = updateTeacher.Name;
        teacher.LastName = updateTeacher.LastName;
        teacher.UserName = updateTeacher.UserName;
        teacher.Password = updateTeacher.Password;
        teacher.ImagePath = updateTeacher.ImagePath;

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