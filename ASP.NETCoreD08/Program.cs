using ASP.NETCoreD08.Data.Context;
using ASP.NETCoreD08.Helper;
using ASP.NETCoreD08.Repositories.DepartmentRespository;
using ASP.NETCoreD08.Repositories.EmployeeRepository;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD08
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a new instance of the WebApplicationBuilder class,
            // which is used to configure and build the web application

            // Create and configure the foundation of ASP.NET Core MVC application
            // Create a WebApplicationBuilder Object
            // Builder is responsible for configuring application before it is built and run
            // Sets up default congiguration, logging, and dependency injection services
            // Load Settings from appsettings.json and environment variables
            // Configure Dependency Injection (DI) container
            // Configure web server (Kestrel) and other services
            // Prepare the application to handle HTTP requests and responses
            // Prepare middleware pipeline and routing for the application
            var builder = WebApplication.CreateBuilder(args);

            #region Services Container
            // Add services to the container. "Register Services"
            // 1- Built in services and already configured (in IOC Container) by the framework "IConfiguration",
            // 2- Built in services but not configured by the framework "AddSession", "AddDbContext"
            // 3- Custom services created by the developer "IPrint", "PrintV02" 
            builder.Services.AddControllersWithViews();

            // Scoped: Create a new instance of the service for each HTTP request and share it within that request
            builder.Services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ASPNETCoreD08"));
                });

            // IOC Container "Service Provider"
            //builder.Services.AddScoped<IPrint, PrintV03>(); XXXXX
            // Register the PrintV02 implementation of the IPrint interface in the dependency injection container
            //builder.Services.AddScoped<IPrint, Print>(); 
            builder.Services.AddScoped<IPrint, Print>(); 
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            #endregion


            // To Build app and configure middleware pipeline and routing for the application
            // UseRouting()
            // app.UseAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
