using Domain.Infrastructure;
using Domain.Services;
using Domain.Settings;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<string[]>(builder.Configuration.GetSection("FileExtensionsAllowed"));
builder.Services.Configure<CdnSettings>(builder.Configuration.GetSection(nameof(CdnSettings)));

builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<ICdnService, CloudFlareService>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();