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
    
        
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null) return NotFound();

        return Ok(group);
    }
    
    [HttpGet]
    [Route("getByClassroomId/{classroomId:int}")]
    public async Task<IActionResult> GetAllGroupsInClassroom([FromRoute] int classroomId)
    {
        var groups = await dbContext.Groups.AsQueryable().Where(x => x.ClassroomId == classroomId).ToListAsync();
        
        return Ok(groups);
    }
    
    [HttpGet]
    [Route("getByStudentId/{studentId:int}")]
    public async Task<IActionResult> GetAllStudentsGroups([FromRoute] int studentId)
    {
        var result = (from g in dbContext.Groups.AsQueryable() 
            join sg in dbContext.StudentsGroups.AsQueryable().Where(x => x.StudentId == studentId)
                on g.Id equals sg.GroupId 
            select new { g.Id, g.ClassroomId, g.Name, g.ImagePath });
        
        return Ok(await result.ToListAsync());
    }


    [HttpPut]
    public async Task<IActionResult> Add(Group addGroup)
    {
        if (addGroup is null) throw new ArgumentNullException(nameof(addGroup));

        await dbContext.Groups.AddAsync(addGroup);
        await dbContext.SaveChangesAsync();

        return Ok(addGroup);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Group updateGroup)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null) return NotFound();
        
        group.Name = updateGroup.Name;
        group.ImagePath = updateGroup.ImagePath;
        //group.ClassroomId = updateGroup.ClassroomId;

        await dbContext.SaveChangesAsync();

        return Ok(group);

    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null) return NotFound();
        
        dbContext.Remove(group);
        await dbContext.SaveChangesAsync();

        return Ok(group);

    }
}