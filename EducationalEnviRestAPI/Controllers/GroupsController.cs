using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public GroupsController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Groups.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Group addGroup)
    {
        var group = new Group()
        {
            Id = Guid.NewGuid(),
            Name = addGroup.Name
        };

        await dbContext.Groups.AddAsync(group);
        await dbContext.SaveChangesAsync();

        return Ok(group);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, Group updateGroup)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null) return NotFound();
        group.Name = updateGroup.Name;

        await dbContext.SaveChangesAsync();

        return Ok(group);

    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null) return NotFound();
        dbContext.Remove(group);
        await dbContext.SaveChangesAsync();

        return Ok(group);

    }
}