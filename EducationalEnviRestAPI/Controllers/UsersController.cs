using System;
using System.Threading.Tasks;
using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : Controller
{
    private readonly UsersAPIDbContext dbContext;

    public UsersController(UsersAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await dbContext.Users.ToListAsync());
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = addUserRequest.Name,
            LastName = addUserRequest.LastName,
            UserName = addUserRequest.UserName,
            Password = addUserRequest.Password
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        
        return Ok(user);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest updateUserRequest)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if(user != null)
        {
            user.Name = updateUserRequest.Name;
            user.LastName = updateUserRequest.LastName;
            user.UserName = updateUserRequest.UserName;
            user.Password = updateUserRequest.Password;

            await dbContext.SaveChangesAsync();
            
            return Ok(user);
        }

        return NotFound();
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if(user != null)
        {
            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();
            
            return Ok(user); 
        }

        return NotFound();
    }
    
    
}