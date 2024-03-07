using Application.Services.Repositories;
using Core.Application.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    } 

    //Git Assembly içerisinde benim Subclass(BaseBussiness) olarak verdiğim sınıfları bul ve IOC ye ekle.
    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services,Assembly assembly,Type type,Func<IServiceCollection,Type,IServiceCollection>? addWithLifeCycle=null)
    {
        var types=assembly.GetTypes().Where(t=>t.IsSubclassOf(type) && type != t).ToList();
        foreach(var t in types)
            if (addWithLifeCycle == null)
                services.AddScoped(t);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
