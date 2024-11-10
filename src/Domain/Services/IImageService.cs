using Domain.DataTransferObjects;

namespace Domain.Services;

public interface IImageService
{
    Task<UploadImageCdnResponse> UploadAsync(ImageDto image);
}