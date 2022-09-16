using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.LoginSessao;

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
            /**Unit Of Work**/
            services.AddScoped<DbSession>(); /**SINGLETON**/
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<UsuarioRepository>();
            /**FIM**/
            /**=================================**/
            /**SESSÃO**/
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessao, Sessao>();
            services.AddSession(o => { o.Cookie.HttpOnly = true; o.Cookie.IsEssential = true; });
            /**FIM**/
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
            app.UseSession();
            app.MapControllerRoute(
                name: "default",    
                pattern: "{controller=Usuario}/{action=Create}/{id?}");
        }
    }
}
