using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;

namespace ArchiveSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigurationServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<DbSession>(); /**SINGLETON**/
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<UsuarioRepository>();
        }
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");
        }
    }
}
