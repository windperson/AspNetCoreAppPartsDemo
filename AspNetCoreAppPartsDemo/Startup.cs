using System.Linq;
using System.Reflection;
using ListFeatureLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PartClassLib;
using PartControllerLib;

namespace AspNetCoreAppPartsDemo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //NOTE: Although asp.net core 3.x has auto loadning application part feature, 
            //      It is not recommand to use that coding style when using some non-open sourced or untrusted 3rd party lib(s).
            //      See: https://blog.burgyn.online/2019/12/16/asp-net-core-applicationPart-evilController.html
            services.AddControllersWithViews()
                //.ConfigureApplicationPartManager(apm => 
                //{
                //    apm.ApplicationParts.Clear();
                //    apm.ApplicationParts.Add(new AssemblyPart(typeof(Startup).Assembly));
                //})
                //.ConfigureApplicationPartManager(apm =>
                //{
                //    var partLib = apm.ApplicationParts.FirstOrDefault(part => part.Name == "PartControllerLib");
                //    if (partLib == null)
                //    {
                //        var assembly = typeof(MyPartController).GetTypeInfo().Assembly;
                //        partLib = new AssemblyPart(assembly);
                //        apm.ApplicationParts.Add(partLib);
                //    }
                //})
                //.AddPartClassLibDemoApplicationPart()
                ;


            services.AddRazorPages()
                    .AddRazorRuntimeCompilation(); //see: https://github.com/dotnet/AspNetCore.Docs/issues/14593#issuecomment-538792893

            services.AddPartClassLibDemoRazorPage();
            services.AddListFeaturesModule();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

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
