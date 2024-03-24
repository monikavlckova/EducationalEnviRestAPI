using EducationalEnviRestAPI.Data;
using EducationalEnviRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalEnviRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly EduEnviAPIDbContext dbContext;

    public ImageController(EduEnviAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await dbContext.Images.ToListAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var image = await dbContext.Images.FindAsync(id);

        if (image == null) return NotFound();

        return Ok(image);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Image addImage)
    {
        if (addImage is null) throw new ArgumentNullException(nameof(addImage));

        await dbContext.Images.AddAsync(addImage);
        await dbContext.SaveChangesAsync();

        return Ok(addImage);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, Image updateImage)
    {
        var image = await dbContext.Images.FindAsync(id);

        if (image == null) return NotFound();

        image.Name = updateImage.Name;
        image.ByteMap = updateImage.ByteMap;

        await dbContext.SaveChangesAsync();

        return Ok(image);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var image = await dbContext.Images.FindAsync(id);

        if (image == null) return NotFound();

        dbContext.Remove(image);
        await dbContext.SaveChangesAsync();

        return Ok(image);
    }
}