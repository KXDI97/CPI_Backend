using Microsoft.EntityFrameworkCore;
using APIUsuarios.Models;


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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));


var app = builder.Build();

// Usa CORS antes del mapeo de rutas
app.UseCors("AllowAll");

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/usuarios", async (AppDbContext db) =>
{
    var usuarios = await db.Usuarios.ToListAsync();
    return Results.Ok(usuarios);
});


app.UseHttpsRedirection();



app.Run();

