using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ProjectPRN221.BusinessObject3;
using ProjectPRN221.Hubs;

namespace ProjectPRN221
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MySessionCookie"; 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<DatabaseTestProjectContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDB")));
            builder.Services.AddScoped<DatabaseTestProjectContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapHub<SignalRhub>("/signalrServer");
            app.UseSession();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}