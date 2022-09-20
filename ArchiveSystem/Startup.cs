using ArchiveSystem.Data.DbConnection;
using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.Domain.Regras;
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
            /**MVC**/
            services.AddControllersWithViews();

            /**DATA**/
            services.AddScoped<DbSession>(); /**SINGLETON**/

            /**UOW**/
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<UsuarioRepository>();
            services.AddTransient<ArquivoRepository>();

            /**SESSÃO**/
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessao, Sessao>();
            services.AddSession(o => { o.Cookie.HttpOnly = true; o.Cookie.IsEssential = true; });

            /**USUARIO**/
            services.AddScoped<UsuarioRegra>();

            /**ARQUIVO**/
            services.AddScoped<ArquivoRegra>();
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
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
