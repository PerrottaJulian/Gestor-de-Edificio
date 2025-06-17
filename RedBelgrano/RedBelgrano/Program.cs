using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AzureDbConection");
}
else
{
    connection = Environment.GetEnvironmentVariable("AzureDbConection");
}

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connection)
    ) ;

builder.Services.AddControllersWithViews();

//Autenticacion
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/IniciarSesion";
        options.AccessDeniedPath = "/Error/AccesoDenegado";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    } );


//BUILD
var app = builder.Build();

//Despertar Base de Datos Azure


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //Usar Auntenticacion
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Finanzas}/{action=Inicio}/{id?}"); //Volver a cambiar, para que el inicio sea el Home/Index. Ahora se cambia por cuestiones de testeo

// Mapear endpoints de API también (PARA FUTURO, DESPERTADOR DE BASE DE DATOS)
//app.MapControllers();


app.Run();
