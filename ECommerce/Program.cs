using ECommerce.Data;
using ECommerce.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce;

public class Program
{ 
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //var connectionString = builder.Configuration.GetConnectionString("ConnectionStrings:DefaultConnectionString");
        // Add services to the container.

        builder.Services.AddScoped<IActorsService, ActorsService>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=UY04-OGRT\\SQLEXPRESS;Initial Catalog=ECommerceDb;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True"));
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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        AppDbInitializer.Seed(app);

        app.Run();
    }
}
