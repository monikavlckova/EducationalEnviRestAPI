using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TeachersController : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public TeachersController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}