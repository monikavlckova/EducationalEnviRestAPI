using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class StudentsTaskController : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsTaskController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}