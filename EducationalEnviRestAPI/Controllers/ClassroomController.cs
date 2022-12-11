using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClassroomController : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public ClassroomController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}