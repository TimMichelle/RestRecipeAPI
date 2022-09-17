using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Db;
using RestRecipeApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RecipesContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection") ?? string.Empty));

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
public partial class Program {}