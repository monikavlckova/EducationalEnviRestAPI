using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public TasksController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Tasks.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();

        return Ok(task);
    }
    
    [HttpGet]
        [Route("getByClassroomId/{classroomId:int}")]
    public async Task<IActionResult> GetAllTasksInClassroom([FromRoute] int classroomId)
    {
        var result = (from t in dbContext.Tasks.AsQueryable()
            join ct in dbContext.ClassroomsTasks.AsQueryable().Where(x => x.ClassroomId == classroomId)
                on t.Id equals ct.TaskkId
            select new { t.Id, t.Name, t.Text, t.ImagePath });
        
        return Ok(await result.ToListAsync());
    }
    
    [HttpGet]
    [Route("getByGroupId/{groupId:int}")]
    public async Task<IActionResult> GetAllTasksInGroup([FromRoute] int groupId)
    {
        var result = (from t in dbContext.Tasks.AsQueryable() 
            join gt in dbContext.GroupsTasks.AsQueryable().Where(x => x.GroupId == groupId)
                on t.Id equals gt.TaskkId 
            select new { t.Id, t.Name, t.Text, t.ImagePath });
        
        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByStudentId/{studentId:int}")]
    public async Task<IActionResult> GetAllStudentsTasks([FromRoute] int studentId)
    {
        var result = (from t in dbContext.Tasks.AsQueryable() 
            join st in dbContext.StudentsTasks.AsQueryable().Where(x => x.StudentId == studentId)
                on t.Id equals st.TaskkId 
            select new { t.Id, t.Name, t.Text, t.ImagePath });
        
        return Ok(await result.ToListAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(Taskk addTask)
    {
        if (addTask is null) throw new ArgumentNullException(nameof(addTask));

        await dbContext.Tasks.AddAsync(addTask);
        await dbContext.SaveChangesAsync();

        return Ok(addTask);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Taskk updateTask)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();
        
        task.Name = updateTask.Name;
        task.Text = updateTask.Text;
        task.ImagePath = updateTask.ImagePath;

        await dbContext.SaveChangesAsync();

        return Ok(task);

    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();
        
        dbContext.Remove(task);
        await dbContext.SaveChangesAsync();

        return Ok(task);

    }
}