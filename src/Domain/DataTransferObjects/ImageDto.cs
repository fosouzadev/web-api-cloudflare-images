namespace Domain.DataTransferObjects;

public class ImageDto
{
    public ImageDto(string name, Stream stream)
    {
        Name = name;
        Stream = stream;
    }

    public Stream Stream { get; init; }
    public string Name { get; init; }
}