using Domain.DataTransferObjects;

namespace Domain.Infrastructure;

public interface ICdnService
{
    Task<UploadImageCdnResponse> UploadAsync(ImageDto image);
    Task DeleteAsync(string imageId);
}