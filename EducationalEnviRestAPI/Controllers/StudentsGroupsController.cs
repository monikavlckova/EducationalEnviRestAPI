using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class StudentsGroupsController : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public StudentsGroupsController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}