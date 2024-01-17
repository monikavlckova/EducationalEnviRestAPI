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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Students.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var student = await dbContext.Students.FindAsync(id);

        if (student == null) return NotFound();

        return Ok(student);
    }
    
        
    [HttpGet]
    [Route("getByClassroomId/{classroomId:int}")]
    public async Task<IActionResult> GetAllStudentsInClassroom([FromRoute] int classroomId)
    {
        var students = await dbContext.Students.AsQueryable().Where(x => x.ClassroomId == classroomId).ToListAsync();
        
        return Ok(students);
    }
    
    [HttpGet]
    [Route("getByGroupId/{groupId:int}")]
    public async Task<IActionResult> GetAllStudentsInGroup([FromRoute] int groupId)
    {
        /*var students = await dbContext.Students.AsQueryable()
            .Join(dbContext.StudentsGroups.AsQueryable(), s => s.Id, sg => sg.StudentId, (student, group) => new {student, group.GroupId})
            .Where(x => x.GroupId == groupId).Select(x => x.student)
            .ToListAsync();
        return Ok(students);*/
        
        
        var result = (from s in dbContext.Students.AsQueryable() 
            join sg in dbContext.StudentsGroups.AsQueryable().Where(x => x.GroupId == groupId)
                on s.Id equals sg.StudentId 
            select new { s.Id, s.Name, s.LastName, s.UserName, s.ClassroomId, s.Password, s.ImagePath });
        
        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("getByClassroomIdNotInGroupId/{classroomId:int}/{groupId:int}")]
    public async Task<IActionResult> GetAllStudentsFromClassroomNotInGroup([FromRoute] int classroomId, [FromRoute] int groupId)
    {
        var students = from s in dbContext.Students.AsQueryable().Where(x => x.ClassroomId == classroomId)
            join sg in dbContext.StudentsGroups.AsQueryable().Where(x => x.GroupId == groupId)
                on s.Id equals sg.StudentId into _sg 
            from sg in _sg.DefaultIfEmpty() //Performing Left Outer Join
            where sg == null
            select new { s.Id, s.Name, s.LastName, s.UserName, s.ClassroomId, s.Password, s.ImagePath};
        
        return Ok(await students.ToListAsync());
    }

    [HttpPut]
    public async Task<IActionResult> Add(Student addStudent)
    {
        if (addStudent is null) throw new ArgumentNullException(nameof(addStudent));

        await dbContext.Students.AddAsync(addStudent);
        await dbContext.SaveChangesAsync();

        return Ok(addStudent);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Student updateStudent)
    {
        var student = await dbContext.Students.FindAsync(id);

        if (student == null) return NotFound();
        
        student.ClassroomId = updateStudent.ClassroomId;
        student.Name = updateStudent.Name;
        student.LastName = updateStudent.LastName;
        student.UserName = updateStudent.UserName;
        student.Password = updateStudent.Password;
        student.ImagePath = updateStudent.ImagePath;

        await dbContext.SaveChangesAsync();

        return Ok(student);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var student = await dbContext.Students.FindAsync(id);

        if (student == null) return NotFound();
        
        dbContext.Remove(student);
        await dbContext.SaveChangesAsync();

        return Ok(student);
    }
}