using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Linq;
using System.Reflection;

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

            //see: https://github.com/dotnet/AspNetCore.Docs/issues/14593#issuecomment-538792893
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
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
