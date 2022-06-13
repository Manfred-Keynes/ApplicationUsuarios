using PostgreSQLEF.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//configuracion de la cadena de conexion a postgres
//builder.Services.AddDbContext<pruebaEFContext>(opciones => 
//    opciones.UsePostgreSQL(builder.Configuration.GetConnectionString("ConexionPostresql"))
//);
//************************
//builder.Services.AddEntityFrameworkNpgsql()
//    .AddDbContext<pruebaEFContext>(options => options.UseNpgsql("ConexionPostresql"));

//builder.Services.AddDbContext<pruebaEFContext>(optionsBuilder =>
//    optionsBuilder.UseNpgsql(connectionString: _config.GetConnectionString("ConexionPostresql"));
//);


//---------------configuracion de la cadena de conexion postgress
builder.Services.AddDbContext<ApplicationUsuariosContext>(opciones =>
    opciones.UseNpgsql(builder.Configuration.GetConnectionString("ConexionPostresql"))
);

var build = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
/*Agregar dependencia del token*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("asp net core Manfred")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


//Agregamos autenticacion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/Login";
        options.SlidingExpiration = true;
    });



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});







// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
