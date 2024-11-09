using Domain.DataTransferObjects;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController(IImageService imageService) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadAsync(IFormFile image)
    {
        Uri uri = await imageService.UploadAsync(new ImageDto(image.FileName, image.OpenReadStream()));
        return Ok(uri);
    }
}