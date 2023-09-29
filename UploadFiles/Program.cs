using Microsoft.Extensions.FileProviders;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IWebHostEnvironment environment = builder.Environment;
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(environment.WebRootPath),
    RequestPath = "/wwwroot"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
