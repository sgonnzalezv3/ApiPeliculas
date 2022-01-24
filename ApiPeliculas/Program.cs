using ApiPeliculas;
using Microsoft.EntityFrameworkCore;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.Repository;
using AutoMapper.Extensions.ExpressionMapping;
using ApiPeliculas.Repository;
using ApiPeliculas.Repository.Azure;
using ApiPeliculas.Local;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()

    //agregando la configuracion de AddNewtonsoftJson
    .AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); } , typeof(MappingProfile).Assembly);
builder.Services.AddService();
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
//builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.

/* Configurar api para archivos (fotos) */
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
