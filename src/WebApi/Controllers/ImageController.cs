using Domain.DataTransferObjects;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController(IImageService imageService) : ControllerBase
{
    [HttpPost()]
    [ProducesResponseType<UploadImageCdnResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadAsync(IFormFile image) =>
        Ok(await imageService.UploadAsync(new ImageDto(image?.FileName, image?.OpenReadStream())));

    [HttpDelete("{imageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] string imageId)
    {
        await imageService.DeleteAsync(imageId);
        return Ok();
    }
}