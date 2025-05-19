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
    await db.Usuarios.ToListAsync());

app.MapGet("/usuarios/{id}", async (string id, AppDbContext db) =>
    await db.Usuarios.FindAsync(id) is Usuario u ? Results.Ok(u) : Results.NotFound());


app.MapPost("/usuarios", async (Usuario usuario, AppDbContext db) =>
{
    db.Usuarios.Add(usuario);
    await db.SaveChangesAsync();
    return Results.Created($"/usuarios/{usuario.DocIdentidad}", usuario);
});


app.MapPut("/usuarios/{id}", async (string id, Usuario input, AppDbContext db) =>
{
    var usuario = await db.Usuarios.FindAsync(id);
    if (usuario is null) return Results.NotFound();

    usuario.NomUsuario = input.NomUsuario;
    usuario.Correo = input.Correo;
    usuario.NumTel = input.NumTel;
    usuario.Contrasenia = input.Contrasenia;
    usuario.CodRol = input.CodRol;

    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.MapDelete("/usuarios/{id}", async (string id, AppDbContext db) =>
{
    var usuario = await db.Usuarios.FindAsync(id);
    if (usuario is null) return Results.NotFound();

    db.Usuarios.Remove(usuario);
    await db.SaveChangesAsync();
    return Results.NoContent();
});



app.UseHttpsRedirection();



app.Run();

