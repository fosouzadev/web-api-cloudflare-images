using Domain.Infrastructure;
using Domain.Services;
using Domain.Settings;
using Infrastructure.Services.CloudFlareIntegration;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<List<string>>(builder.Configuration.GetSection("FileExtensionsAllowed"));
builder.Services.Configure<CdnSettings>(builder.Configuration.GetSection(nameof(CdnSettings)));

builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<ICdnService, CloudFlareService>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();

app.Run();