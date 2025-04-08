var builder = WebApplication.CreateBuilder(args);

// Agrega el servicio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

// OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

// Usa CORS antes del mapeo de rutas
app.UseCors("AllowAll");

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();



app.Run();

