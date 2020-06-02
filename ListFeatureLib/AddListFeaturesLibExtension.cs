using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ListFeatureLib
{
    public static class AddListFeaturesLibExtension
    {
        // ReSharper disable once UnusedMember.Global
        public static IServiceCollection AddListFeaturesModule(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //Since .NET Core 3.0, auto Controller feature discovery & load is the default behavior
            //See: https://andrewlock.net/when-asp-net-core-cant-find-your-controller-debugging-application-parts/
            //services.AddControllersWithViews().ConfigureApplicationPartManager(apm =>
            //{
            //    var assembly = typeof(ListFeaturesController).GetTypeInfo().Assembly;

            //    var listFeatureLib = apm.ApplicationParts.FirstOrDefault(part => part.Name == assembly.GetName().Name);
            //    if (listFeatureLib == null)
            //    {
            //        listFeatureLib = new AssemblyPart(assembly);
            //        apm.ApplicationParts.Add(listFeatureLib);
            //    }
            //});

            //see: https://github.com/dotnet/AspNetCore.Docs/issues/14593#issuecomment-538792893
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListFeaturesController).GetTypeInfo().Assembly));
            });

            return services;
        }
    }
}