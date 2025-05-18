using Klinik.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer("server=DESKTOP-2NH4Q56\\SQLEXPRESS;database=Clinic1DB;trusted_connection=true;TrustServerCertificate=true");

});
var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "Default",
    pattern: "{Controller=Home}/{Action=Index}"
    );

app.Run();
