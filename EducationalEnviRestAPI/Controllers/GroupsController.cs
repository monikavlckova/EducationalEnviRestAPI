using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    [Route("getAllStudents/{id:int}")]
    public async Task<IActionResult> GetAllStudents([FromRoute] int id)
    {
        var result = (from std in dbContext.Students.AsQueryable() 
            join sg in dbContext.StudentsGroups 
                on std.Id equals sg.StudentId 
            where sg.GroupId == id
            select new { std.Id, std.Name, std.LastName, std.UserName, std.ClassroomId, std.Password });

        //var queryS = dbContext.Students.AsQueryable();
        //var querySg = dbContext.StudentsGroups.AsQueryable().Where(sg => sg.GroupId == groupId);
        //queryS = queryS.Join(querySg, s => s.Id, sg => sg.StudentId, (s, sg) => s);

        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("getAllStudents/{groupId:int}/{classroomId:int}")]
    public async Task<IActionResult> GetAllStudentsFromClassroomNotInGorup([FromRoute] int groupId, [FromRoute] int classroomId)
    {
        var result = (from std in dbContext.Students.AsQueryable() 
            where not exists (from std in dbContext.Students.AsQueryable() 
            join sg in dbContext.StudentsGroups 
                on std.Id equals sg.StudentId 
            where sg.GroupId != groupId && std.ClassroomId == classroomId)
            select new { std.Id, std.Name, std.LastName, std.UserName, std.ClassroomId, std.Password });
        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("getAllTasks/{id:int}")]
    public async Task<IActionResult> GetAllTasks([FromRoute] int id)
    {
        var result = (from tsk in dbContext.Tasks.AsQueryable() 
            join gt in dbContext.GroupsTasks 
                on tsk.Id equals gt.TaskId 
            where gt.GroupId == id
            select new { tsk.Id, tsk.Name, tsk.Text });
        
        return Ok(await result.ToListAsync());
    }


    [HttpPut]
    public async Task<IActionResult> Add(Group addGroup)
    {
        var group = new Group()
        {
            Name = addGroup.Name,
            ClassroomId = addGroup.ClassroomId
        };

        await dbContext.Groups.AddAsync(group);
        await dbContext.SaveChangesAsync();

        return Ok(group);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Group updateGroup)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null) return NotFound();
        group.Name = updateGroup.Name;
        group.ClassroomId = updateGroup.ClassroomId;

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