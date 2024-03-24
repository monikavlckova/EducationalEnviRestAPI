using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Students.OrderBy(x => x.LastName).ToListAsync());
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
    [Route("getByLogin/{password}")]
    public async Task<IActionResult> Get([FromRoute] string password)
    {
        var student = await dbContext.Students.AsQueryable().FirstOrDefaultAsync(x => x.LoginCode == password);

        if (student == null) return NotFound();

        return Ok(student);
    }


    [HttpGet]
    [Route("getByClassroomId/{classroomId:int}")]
    public async Task<IActionResult> GetAllStudentsInClassroom([FromRoute] int classroomId)
    {
        var students = await dbContext.Students.OrderBy(x => x.LastName).AsQueryable().Where(x => x.ClassroomId == classroomId).ToListAsync();

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


        var result = from s in dbContext.Students.OrderBy(x => x.LastName).AsQueryable()
            join sg in dbContext.StudentGroups.AsQueryable().Where(x => x.GroupId == groupId)
                on s.Id equals sg.StudentId
            select new { s.Id, s.Name, s.LastName, s.ClassroomId, s.LoginCode, s.ImageId };

        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByClassroomIdNotInGroupId/{classroomId:int}/{groupId:int}")]
    public async Task<IActionResult> GetAllStudentsFromClassroomNotInGroup([FromRoute] int classroomId,
        [FromRoute] int groupId)
    {
        var students = from s in dbContext.Students.OrderBy(x => x.LastName).AsQueryable().Where(x => x.ClassroomId == classroomId)
            join sg in dbContext.StudentGroups.AsQueryable().Where(x => x.GroupId == groupId)
                on s.Id equals sg.StudentId into _sg
            from sg in _sg.DefaultIfEmpty() //Performing Left Outer Join
            where sg == null
            select new { s.Id, s.Name, s.LastName, s.ClassroomId, s.LoginCode, s.ImageId };

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
        student.LoginCode = updateStudent.LoginCode;
        student.ImageId = updateStudent.ImageId;

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