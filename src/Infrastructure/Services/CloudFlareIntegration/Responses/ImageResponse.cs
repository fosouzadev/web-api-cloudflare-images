namespace Infrastructure.Services.CloudFlareIntegration;

internal class ImageResponse
{
    public ImageResponseData Result { get; set; }
    public bool Success { get; set; }
    public IEnumerable<MessageResponse> Errors { get; set; }
    public IEnumerable<MessageResponse> Messages { get; set; }
}

internal class ImageResponseData
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public DateTimeOffset Uploaded { get; set; }
    public bool RequireSignedURLs { get; set; }
    public IEnumerable<string> Variants { get; set; }
}

internal class MessageResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
}