var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy - allow all origins, headers, methods
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Swagger enabled in both Development and Production
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FarmerAPI v1");
        c.RoutePrefix = "swagger"; // Swagger UI accessible at /swagger
    });
}

// Apply CORS
app.UseCors("AllowAll");

// Redirect root URL to Swagger
app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

// Only use HTTPS redirection in Development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();