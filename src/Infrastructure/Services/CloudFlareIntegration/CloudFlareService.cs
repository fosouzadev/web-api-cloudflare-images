using System.Net.Http.Headers;
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

    private HttpRequestMessage GetHttpRequestMessage(HttpMethod httpMethod, string url)
    {
        HttpRequestMessage request = new(httpMethod, url);

        request.Headers.Add("Authorization", $"Bearer {cdnSettings.Value.Token}");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return request;
    }

    private async Task<ImageResponse> SendAsync(HttpRequestMessage request)
    {
        HttpResponseMessage httpResponse = await _httpClient.SendAsync(request);
        httpResponse.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<ImageResponse>(await httpResponse.Content.ReadAsStringAsync());
    }

    public async Task<UploadImageCdnResponse> UploadAsync(ImageDto image)
    {
        HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Post, cdnSettings.Value.Url);

        request.Content = new MultipartFormDataContent()
        {
            { new StreamContent(image.Stream), "file", image.Name }
        };

        ImageResponse response = await SendAsync(request);
        
        return new UploadImageCdnResponse
        {
            Id = response.Result.Id,
            FileName = response.Result.FileName,
            Uploaded = response.Result.Uploaded,
            Variants = response.Result.Variants
        };
    }

    public async Task DeleteAsync(string imageId)
    {
        HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Delete, $"{cdnSettings.Value.Url}/{imageId}");
        
        _ = await SendAsync(request);
    }
}