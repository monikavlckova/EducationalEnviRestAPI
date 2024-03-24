using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public GroupController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Groups.OrderBy(x => x.Name).ToListAsync());
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
    public async Task<IActionResult> GetGroupsInClassroom([FromRoute] int classroomId)
    {
        var groups = await dbContext.Groups.OrderBy(x => x.Name).AsQueryable().Where(x => x.ClassroomId == classroomId).ToListAsync();

        return Ok(groups);
    }

    [HttpGet]
    [Route("getByStudentId/{studentId:int}")]
    public async Task<IActionResult> GetStudentsGroups([FromRoute] int studentId)
    {
        var result = from g in dbContext.Groups.OrderBy(x => x.Name).AsQueryable()
            join sg in dbContext.StudentGroups.AsQueryable().Where(x => x.StudentId == studentId)
                on g.Id equals sg.GroupId
            select new { g.Id, g.ClassroomId, g.Name, g.ImageId };

        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByClassroomIdNotInStudentId/{classroomId:int}/{studentId:int}")]
    public async Task<IActionResult> GetGroupsNotInStudent([FromRoute] int classroomId, [FromRoute] int studentId)
    {
        var result = from g in dbContext.Groups.OrderBy(x => x.Name).AsQueryable().Where(x => x.ClassroomId == classroomId)
            join st in dbContext.StudentGroups.AsQueryable().Where(x => x.StudentId == studentId)
                on g.Id equals st.GroupId into _gt
            from gt in _gt.DefaultIfEmpty() //Performing Left Outer Join
            where gt == null
            select new { g.Id, g.ClassroomId, g.Name, g.ImageId };

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
        group.ImageId = updateGroup.ImageId;
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