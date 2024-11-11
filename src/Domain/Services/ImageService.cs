using Domain.DataTransferObjects;
using Domain.Infrastructure;
using Microsoft.Extensions.Options;

namespace Domain.Services;

public sealed class ImageService(
    IOptions<List<string>> extensionsAllowed,
    ICdnService cdnService) : IImageService
{
    public async Task<UploadImageCdnResponse> UploadAsync(ImageDto image)
    {
        if (image == null || string.IsNullOrWhiteSpace(image.Name) || image.Stream == null)
            throw new ArgumentNullException(paramName: nameof(image), "Invalid image.");

        if (extensionsAllowed.Value.Contains(Path.GetExtension(image.Name)) == false)
            throw new ArgumentException(paramName: nameof(image), message: "Extension not allowed.");

        return await cdnService.UploadAsync(image);
    }

    public async Task DeleteAsync(string imageId)
    {
        if (string.IsNullOrWhiteSpace(imageId))
            throw new ArgumentNullException(paramName: nameof(imageId), "Invalid image id.");

        await cdnService.DeleteAsync(imageId);       
    }
}