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
        public static IServiceCollection AddListFeaturesRazorPage(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListFeaturesController).GetTypeInfo().Assembly));
            });

            return services;
        }

        // ReSharper disable once UnusedMember.Global
        public static IMvcBuilder AddListFeaturesPageApplicationPart(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureApplicationPartManager(apm =>
            {
                var assembly = typeof(ListFeaturesController).GetTypeInfo().Assembly;

                var listFeatureLib = apm.ApplicationParts.FirstOrDefault(part => part.Name == assembly.GetName().Name);
                if (listFeatureLib == null)
                {
                    listFeatureLib = new AssemblyPart(assembly);
                    apm.ApplicationParts.Add(listFeatureLib);
                }
            });

            return builder;
        }
    }
}