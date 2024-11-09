using Domain.DataTransferObjects;
using Domain.Infrastructure;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.CloudFlareIntegration;

public class CloudFlareService(
    IHttpClientFactory httpClientFactory,
    IOptions<CdnSettings> cdnSettings) : ICdnService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    public async Task<UploadImageCdnResponse> UploadAsync(ImageDto image)
    {
        HttpRequestMessage request = new(HttpMethod.Post, cdnSettings.Value.Url);

        request.Headers.Add("Authorization", $"Bearer {cdnSettings.Value.Token}");
        request.Content = new MultipartFormDataContent()
        {
            { new StreamContent(image.Stream), "file", image.Name }
        };

        HttpResponseMessage httpResponse = await _httpClient.SendAsync(request);
        httpResponse.EnsureSuccessStatusCode();

        UploadImageResponse response = JsonConvert.DeserializeObject<UploadImageResponse>(await httpResponse.Content.ReadAsStringAsync());
        
        return new UploadImageCdnResponse
        {
            Id = response.Result.Id,
            FileName = response.Result.FileName,
            Uploaded = response.Result.Uploaded,
            Variants = response.Result.Variants
        };
    }
}