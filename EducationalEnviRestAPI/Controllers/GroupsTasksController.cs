﻿using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsTasksController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public GroupsTasksController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpPut]
    public async Task<IActionResult> AddTaskToGroup(int taskId, int groupId)
    {
        var groupTask = new GroupTask()
        {
            GroupId = groupId,
            TaskId = taskId
        };

        await dbContext.GroupsTasks.AddAsync(groupTask);
        await dbContext.SaveChangesAsync();

        return Ok(groupTask);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var groupTask = await dbContext.GroupsTasks.FindAsync(id);

        if (groupTask == null) return NotFound();
        dbContext.Remove(groupTask);
        await dbContext.SaveChangesAsync();

        return Ok(groupTask);
    }
}