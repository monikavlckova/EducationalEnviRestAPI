using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class Task : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public Task(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}