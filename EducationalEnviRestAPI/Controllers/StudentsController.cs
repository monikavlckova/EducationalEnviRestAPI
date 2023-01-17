using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Students.ToListAsync());
    }

    [HttpGet]
    [Route("getAllInClass/{classroomId:guid}")]
    public async Task<IActionResult> GetAllInClass([FromRoute] Guid classroomId)
    {
        var query = dbContext.Students.AsQueryable();
        query = query.Where(student => student.ClassroomId.Equals(classroomId));
        return Ok(await query.ToListAsync());
    }

    [HttpGet]
    [Route("getAllInGroup/{groupId:guid}")]
    public async Task<IActionResult> GetAllInGroup([FromRoute] Guid groupId)
    {
        var result = (from std in dbContext.Students.AsQueryable() 
                                             join sg in dbContext.StudentsGroups 
                                                 on std.Id equals sg.StudentId 
                                             where sg.GroupId == groupId
                                             select new { std });

        //var queryS = dbContext.Students.AsQueryable();
        //var querySg = dbContext.StudentsGroups.AsQueryable().Where(sg => sg.GroupId == groupId);
        //queryS = queryS.Join(querySg, s => s.Id, sg => sg.StudentId, (s, sg) => s);

        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("get/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var student = await dbContext.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Student addStudent)
    {
        var student = new Student()
        {
            Id = addStudent.Id,
            Name = addStudent.Name,
            LastName = addStudent.LastName,
            UserName = addStudent.UserName,
            Password = addStudent.Password,
            ClassroomId = addStudent.ClassroomId
        };

        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();

        return Ok(student);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, Student updateStudent)
    {
        var student = await dbContext.Students.FindAsync(id);

        if (student == null) return NotFound();
        student.Name = updateStudent.Name;
        student.LastName = updateStudent.LastName;
        student.UserName = updateStudent.UserName;
        student.Password = updateStudent.Password;
        student.ClassroomId = updateStudent.ClassroomId;

        await dbContext.SaveChangesAsync();

        return Ok(student);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var student = await dbContext.Students.FindAsync(id);

        if (student == null) return NotFound();
        dbContext.Remove(student);
        await dbContext.SaveChangesAsync();

        return Ok(student);
    }
}