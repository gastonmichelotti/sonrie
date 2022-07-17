using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netCoreNew.Business;
using netCoreNew.Data;
using netCoreNew.Logic;
using netCoreNew.ViewModels;
using System.Globalization;

namespace netCoreNew
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NetCoreNewContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DbInitializer.Initialize(app, dataContext);
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Logging>(Configuration.GetSection("Logging"));
            services.AddSingleton<IConfiguration>(Configuration);

            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture = new RequestCulture("es-AR");
            //});

            var cultureInfo = new CultureInfo("es-AR");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
            cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
            cultureInfo.NumberFormat.CurrencyGroupSeparator = ".";
            cultureInfo.NumberFormat.CurrencySymbol = "$";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddSession();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/Home/Index/";
                options.LoginPath = "/Home/Index/";
            });

            services.AddDbContext<NetCoreNewContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("cs"));
            });

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRolService, RolService>();           
            services.AddScoped<IInsumoxCategoriaService, InsumoxCategoriaService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IObraSocialService, ObraSocialService>();
            services.AddScoped<IPrestacionService, PrestacionService>();
            services.AddScoped<IAtencionService, AtencionService>();
            services.AddScoped<IPrestacionxAtencionService, PrestacionxAtencionService>();
            services.AddScoped<ICategoriaPrestacionService, CategoriaPrestacionService>();
            services.AddScoped<IInsumoService, InsumoService>();
            services.AddScoped<IPrecioService, PrecioService>();
            services.AddScoped<IDolarService, DolarService>();
            

            services.AddScoped<IEntidadesLogicService, EntidadesLogicService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
