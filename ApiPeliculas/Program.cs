using Microsoft.EntityFrameworkCore;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.Repository;
using AutoMapper.Extensions.ExpressionMapping;
using ApiPeliculas.Repository;
using ApiPeliculas.Repository.Azure;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using AutoMapper;
using ApiPeliculas.Repository.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiPeliculas.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()

    //agregando la configuracion de AddNewtonsoftJson
    .AddNewtonsoftJson();
/* inyectando NetTopologySuite */
builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        /* Trayendo de la libreria NetTopologySuite */
    , sqlServerOptions => sqlServerOptions.UseNetTopologySuite()         
        );
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); } , typeof(MappingProfile).Assembly);
builder.Services.AddService();

/* como se está usando el applicationDbContext, será scoped */
builder.Services.AddScoped<PeliculaExisteAttribute>();

// ---SEGURIDAD---
/* Tipo dato usuario */
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
                ClockSkew = TimeSpan.Zero
            });




/* Utilizar geometry Factory en Inyecciond e dependencias */

/*
builder.Services.AddSingleton(provider =>
    new MapperConfiguration(config =>
    {
        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
        config.AddProfile(new MappingProfile(geometryFactory));
    })
);
*/
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
