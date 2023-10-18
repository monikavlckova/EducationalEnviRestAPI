using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public UsersController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Users.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Add(User addUser)
    {
        var user = new User()
        {
            Name = addUser.Name,
            LastName = addUser.LastName,
            UserName = addUser.UserName,
            Password = addUser.Password
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, User updateUser)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null) return NotFound();
        user.Name = updateUser.Name;
        user.LastName = updateUser.LastName;
        user.UserName = updateUser.UserName;
        user.Password = updateUser.Password;

        await dbContext.SaveChangesAsync();

        return Ok(user);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null) return NotFound();
        dbContext.Remove(user);
        await dbContext.SaveChangesAsync();

        return Ok(user);
    }
}