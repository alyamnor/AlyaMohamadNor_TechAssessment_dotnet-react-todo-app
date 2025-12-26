var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Enable HTTPS
app.UseHttpsRedirection();

// Enable CORS
app.UseCors();

app.MapControllers();

app.Run();
