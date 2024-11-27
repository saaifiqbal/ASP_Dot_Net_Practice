using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// help to tool generate
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    var response = new
    {
        Message = "This Is Home Page Response Object",
        Success = true
    };
    return Results.Ok(response);
});
app.MapGet("/welcome", () =>
{
    return Results.Ok("Hello Welcome To My First Application");
});

app.MapPost("/welcome", () =>
{
    return Results.Created();
});

app.MapPut("/welcome", () =>
{
    return Results.NoContent();
});

app.MapDelete("/welcome", () =>
{
    return Results.NoContent();
});

// product
var products = new List<ProductDto>()
{
    new ProductDto("Phone",2000),
    new ProductDto("Phone",3000),
    new ProductDto("Phone",4000),
    new ProductDto("Phone",5000),
};

app.MapGet("/products", () => { return Results.Ok(products); });

// crud Item

// get 
List<Category> categories = new List<Category>();
app.MapGet("/api/categories", () =>
{
    return Results.Ok(new { Message = "Successfully Get", isSuccess = true, Results = categories });
});

// Single get 
app.MapGet("/api/category", (string searchValue = "") =>
{
    if (searchValue != null)
    {
        var searchCategories = categories.Where(cat => !string.IsNullOrEmpty(cat.CategoryName) && cat.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Results.Ok(new { Message = "Successfully Get", isSuccess = true, Results = searchCategories, searchValue });
    }
    return Results.Ok(new { Message = "Successfully Get", isSuccess = true, Results = categories, searchValue });
});

//create
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    // return Results.Ok(categoryData);
    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        CategoryName = categoryData.CategoryName,
        CategoryDescription = categoryData.CategoryDescription,
        CreatedAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});


//Put
app.MapPut("/api/categories/{categoryId}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    if (foundCategory == null) return Results.NotFound(new { Message = "No Category Found", isSuccess = false });
    foundCategory.CategoryName = categoryData.CategoryName;
    foundCategory.CategoryDescription = categoryData.CategoryDescription;
    return Results.NoContent();
});


//delete
app.MapDelete("/api/categories/{categoryId:Guid}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    if (foundCategory == null)
    {
        return Results.NotFound(new { Massage = "No Category Found", isSuccess = false });
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
});





app.Run();

public record ProductDto(string ProductName, decimal ProductPrice);
public record Category
{
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
public record Item
{
    public Guid ItemId { get; set; }
    public required string ItemName { get; set; }
    public decimal ItemPrice { get; set; }
    public decimal ItemStockQuantity { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
}