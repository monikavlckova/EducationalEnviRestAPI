using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = EducationalEnviRestAPI.Models.Task;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public TaskController(EduEnviAPIDbContext dbContext)
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
    public async Task<IActionResult> GetTasksInClassroom([FromRoute] int classroomId)
    {
        var result = from t in dbContext.Tasks.AsQueryable()
            join ct in dbContext.ClassroomTasks.AsQueryable().Where(x => x.ClassroomId == classroomId)
                on t.Id equals ct.TaskId
            select new { t.Id, t.TeacherId, t.TaskTypeId, t.Name, t.Text, t.ImageId, t.Deadline };

        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByGroupId/{groupId:int}")]
    public async Task<IActionResult> GetTasksInGroup([FromRoute] int groupId)
    {
        var result = from t in dbContext.Tasks.AsQueryable()
            join gt in dbContext.GroupTasks.AsQueryable().Where(x => x.GroupId == groupId)
                on t.Id equals gt.TaskId
            select new { t.Id, t.TeacherId, t.TaskTypeId, t.Name, t.Text, t.ImageId, t.Deadline };

        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByStudentId/{studentId:int}")]
    public async Task<IActionResult> GetStudentsTasks([FromRoute] int studentId)
    {
        var result = from t in dbContext.Tasks.AsQueryable()
            join st in dbContext.StudentTasks.AsQueryable().Where(x => x.StudentId == studentId)
                on t.Id equals st.TaskId
            select new { t.Id, t.TeacherId, t.TaskTypeId, t.Name, t.Text, t.ImageId, t.Deadline };

        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByTeacherIdNotInGroupId/{teacherId:int}/{groupId:int}")]
    public async Task<IActionResult> GetTasksNotInGroup([FromRoute] int teacherId, [FromRoute] int groupId)
    {
        var result = from t in dbContext.Tasks.AsQueryable().Where(x => x.TeacherId == teacherId)
            join gt in dbContext.GroupTasks.AsQueryable().Where(x => x.GroupId == groupId)
                on t.Id equals gt.TaskId into _gt
            from gt in _gt.DefaultIfEmpty() //Performing Left Outer Join
            where gt == null
            select new { t.Id, t.TeacherId, t.TaskTypeId, t.Name, t.Text, t.ImageId, t.Deadline };

        return Ok(await result.ToListAsync());
    }

    [HttpGet]
    [Route("getByTeacherIdNotInStudentId/{teacherId:int}/{studentId:int}")]
    public async Task<IActionResult> GetTasksNotInStudent([FromRoute] int teacherId, [FromRoute] int studentId)
    {
        var result = from t in dbContext.Tasks.AsQueryable().Where(x => x.TeacherId == teacherId)
            join st in dbContext.StudentTasks.AsQueryable().Where(x => x.StudentId == studentId)
                on t.Id equals st.TaskId into _st
            from st in _st.DefaultIfEmpty() //Performing Left Outer Join
            where st == null
            select new { t.Id, t.TeacherId, t.TaskTypeId, t.Name, t.Text, t.ImageId, t.Deadline };

        return Ok(await result.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add(Task addTask)
    {
        if (addTask is null) throw new ArgumentNullException(nameof(addTask));

        await dbContext.Tasks.AddAsync(addTask);
        await dbContext.SaveChangesAsync();

        return Ok(addTask);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Task updateTask)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null) return NotFound();

        task.Name = updateTask.Name;
        task.Text = updateTask.Text;
        task.ImageId = updateTask.ImageId;
        task.Deadline = updateTask.Deadline;

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