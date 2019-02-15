using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PartClassLib
{
    public static class RegisterPartClassLibExtension
    {
        public static IServiceCollection AddPartClassLibDemoRazorPage(this IServiceCollection services)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(NetCoreLibDemoController).GetTypeInfo().Assembly));
            });

            return services;
        }

        public static IMvcBuilder AddPartClassLibDemoApplicationPart(this IMvcBuilder builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureApplicationPartManager(apm => 
            {
                var assembly = typeof(NetCoreLibDemoController).GetTypeInfo().Assembly;

                var partLib = apm.ApplicationParts.FirstOrDefault(part => part.Name == assembly.GetName().Name);
                if(partLib == null)
                {
                    partLib = new AssemblyPart(assembly);
                    apm.ApplicationParts.Add(partLib);
                }
            });

            return builder;
        }
    }
}
