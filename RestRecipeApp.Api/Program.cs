using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Persistence;

var builder = WebApplication.CreateBuilder(args);
var myCors = "_myCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors,
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:3000",
                "http://localhost:3001",
                "http://recipe_nextjs:*",
                "http://recipe_api:*",
                "http://192.168.1.111:*"
            ).AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<RecipesDbContext>();

    // Apply pending migrations
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
        Console.Write("Migrated database");
    } else {
        Console.Write("Nothing to migrate");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(myCors);

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
public partial class Program {}
