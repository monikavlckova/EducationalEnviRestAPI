using EducationalEnviRestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GroupsController : Controller
{
    private readonly EduEnviAPIDbContext dbContext;

    public GroupsController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
}