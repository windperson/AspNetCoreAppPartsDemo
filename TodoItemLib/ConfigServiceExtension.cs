using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoItemLib.Controllers;
using TodoItemLib.Models;

namespace TodoItemLib
{
    public static class ConfigServiceExtension
    {
        public static IServiceCollection AddTodoItemEfCoreConfiguration(this IServiceCollection services,
            Action<DbContextOptionsBuilder> builderAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (builderAction == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //Since .NET Core 3.0, auto Controller feature discovery & load is the default behavior
            //See: https://andrewlock.net/when-asp-net-core-cant-find-your-controller-debugging-application-parts/
            services.AddControllersWithViews().ConfigureApplicationPartManager(apm =>
            {
                var assembly = typeof(TodoItemsController).GetTypeInfo().Assembly;

                var listFeatureLib = apm.ApplicationParts.FirstOrDefault(part => part.Name == assembly.GetName().Name);
                if (listFeatureLib != null) { return; }
                listFeatureLib = new AssemblyPart(assembly);
                apm.ApplicationParts.Add(listFeatureLib);
            });

            services.AddDbContext<TodoContext>(builderAction);
            return services;
        }

    }
}
