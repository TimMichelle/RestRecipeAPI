using RestRecipeApp.Persistence;

var builder = WebApplication.CreateBuilder(args);
var myCors = "_myCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                "http://localhost:3001").AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(myCors);

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
public partial class Program {}