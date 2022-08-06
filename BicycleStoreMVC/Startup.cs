using BicycleStoreMVC.Data;
using BicycleStoreMVC.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BicycleStoreMVC.Services;
using BicycleStoreMVC.Services.User;

namespace BicycleStoreMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BicycleStoreDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("BicycleStore.Data")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BicycleStoreDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();

            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc().AddXmlDataContractSerializerFormatters();
            services.AddMemoryCache();

            services.AddScoped(typeof(ICrud<>), typeof(Crud<>));
            services.AddScoped<IUserService, UserService>();


            //Authentication Middleware Services with the AddAuthentication and AddCookie method.
            // Authentication scheme passed to AddAuthentication sets to the default authentication scheme for the app.
            // CookieAuthenticationDefaults.AuthenticationScheme provides "Cookies" for the scheme.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => x.LoginPath = "/account/login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
