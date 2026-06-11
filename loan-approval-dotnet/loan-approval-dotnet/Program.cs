using LoanApproval.Services;

var builder = WebApplication.CreateBuilder(args);

// Use controllers + health checks and register services
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<LoanApprovalService>();

// Register OpenAPI/Swagger only in Development
if (builder.Environment.IsDevelopment())
{
    try
    {
        // Register Swashbuckle for classic Swagger UI
        builder.Services.AddSwaggerGen();

        // Log Swashbuckle assembly versions to help diagnose TypeLoad issues
        var swashbuckleGen = typeof(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator).Assembly;
        var swashbuckleGenVersion = swashbuckleGen.GetName().Version;
        builder.Logging.AddConsole();
        //builder.Logging.CreateLogger("Startup").LogInformation($"Loaded Swashbuckle.SwaggerGen: {swashbuckleGenVersion}");
    }
    catch (System.TypeLoadException ex)
    {
        // Re-throw with additional diagnostic info
        var loadedAssemblies = System.AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name?.StartsWith("Swashbuckle") == true || a.GetName().Name?.StartsWith("Microsoft.OpenApi") == true)
            .Select(a => a.GetName().Name + " " + a.GetName().Version)
            .ToArray();

        var message = $"TypeLoadException during AddSwaggerGen: {ex.Message}. Loaded related assemblies: {string.Join(", ", loadedAssemblies)}";
        throw new System.TypeLoadException(message, ex);
    }
}

var app = builder.Build();

// Map Swagger UI only in Development and provide /swagger redirects
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan Approval API v1");
        c.RoutePrefix = "swagger"; // serve UI at /swagger
    });
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
