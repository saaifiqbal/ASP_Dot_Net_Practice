using asp_net_ecommerce_web_api.Controllers;
using asp_net_ecommerce_web_api.DTO;
using asp_net_ecommerce_web_api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddAutoMapper(typeof (Program));
builder.Services.AddControllers(); // add controller for use controller
builder.Services.AddEndpointsApiExplorer(); // for use Swagger
builder.Services.AddSwaggerGen(); // for use Swagger
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // var errors = context.ModelState.Where(e => e.Value!.Errors.Count > 0).Select(e => new { Field = e.Key, Errors = e.Value!.Errors.Select(x => x.ErrorMessage).ToArray() }).ToList();
        var errors = context.ModelState.Where(e => e.Value!.Errors.Count > 0).SelectMany(e =>  e.Value!.Errors.Select(x => x.ErrorMessage)).ToList();
        return new BadRequestObjectResult(ApiResponse<ReadCategoryDto>.ErrorResponse("Validation Error", errors));
    };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
