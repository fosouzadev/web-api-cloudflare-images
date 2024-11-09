namespace Domain.DataTransferObjects;

public class UploadImageCdnResponse
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public DateTimeOffset Uploaded { get; set; }
    public IEnumerable<string> Variants { get; set; }
}