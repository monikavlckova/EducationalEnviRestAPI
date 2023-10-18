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

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }
    
    [HttpGet]
    [Route("getAllGroups/{id:int}")]
    public async Task<IActionResult> GetAllGroups([FromRoute] int id)
    {
        var result = (from gr in dbContext.Groups.AsQueryable() 
            join sg in dbContext.StudentsGroups
                on gr.Id equals sg.GroupId 
            where sg.StudentId == id
            select new { gr.Id, gr.ClassroomId, gr.Name });
        
        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("getAllTasks/{id:int}")]
    public async Task<IActionResult> GetAllTasks([FromRoute] int id)
    {
        var result = (from task in dbContext.Tasks.AsQueryable() 
            join st in dbContext.StudentsTasks 
                on task.Id equals st.TaskId 
            where st.StudentId == id
            select new { task.Id, task.Name, task.Text });
        
        return Ok(await result.ToListAsync());
    }
    
    /*[HttpGet]
    [Route("{studentId:int}")]
    public async Task<IActionResult> GetAllGroupsTasks([FromRoute] int studentId)
    {
        var result = (from task in dbContext.Tasks.AsQueryable()
            join gt in dbContext.GroupsTasks on task.Id equals gt.TaskId
            join sg in dbContext.StudentsGroups on sg.StudentId equals studentId
            select new { task });


        return Ok(await result.ToListAsync());
    }*/

    [HttpPut]
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

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Student updateStudent)
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