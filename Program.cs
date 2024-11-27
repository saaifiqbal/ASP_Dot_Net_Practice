using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddControllers(); // add controller for use controller
builder.Services.AddEndpointsApiExplorer(); // for use Swagger
builder.Services.AddSwaggerGen(); // for use Swagger

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
