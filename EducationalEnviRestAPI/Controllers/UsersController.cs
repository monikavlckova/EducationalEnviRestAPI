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
    [Route("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Add(User addUser)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = addUser.Name,
            LastName = addUser.LastName,
            UserName = addUser.UserName,
            Password = addUser.Password
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, User updateUser)
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
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null) return NotFound();
        dbContext.Remove(user);
        await dbContext.SaveChangesAsync();

        return Ok(user);
    }
}