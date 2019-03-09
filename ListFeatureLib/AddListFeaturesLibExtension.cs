using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
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

            services.AddMvcCore().ConfigureApplicationPartManager(apm =>
            {
                var assembly = typeof(ListFeaturesController).GetTypeInfo().Assembly;

                var listFeatureLib = apm.ApplicationParts.FirstOrDefault(part => part.Name == assembly.GetName().Name);
                if (listFeatureLib == null)
                {
                    listFeatureLib = new AssemblyPart(assembly);
                    apm.ApplicationParts.Add(listFeatureLib);
                }
            });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListFeaturesController).GetTypeInfo().Assembly));
            });

            return services;
        }
    }
}