using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class StudentsController : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}