using Domain.DataTransferObjects;

namespace Domain.Services;

public interface IImageService
{
    Task<Uri> UploadAsync(ImageDto image);
}