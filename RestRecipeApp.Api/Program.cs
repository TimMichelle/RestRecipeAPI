using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestRecipeApp.Persistence;

var builder = WebApplication.CreateBuilder(args);
var myCors = "_myCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recipe's API", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    
    c.AddSecurityDefinition("Bearer", securitySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
});

builder.Services.AddControllers().AddNewtonsoftJson();

var configuration = builder.Configuration;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = $"https://{configuration["Auth0:Domain"]}/";
    options.TokenValidationParameters = 
        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = configuration["Auth0:Audience"],
            ValidIssuer = $"{configuration["Auth0:Domain"]}",
            ValidateLifetime = true,
        };
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
public partial class Program {}
