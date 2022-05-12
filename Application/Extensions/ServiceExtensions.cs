using Application.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;


public static class ServiceExtensions
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, string fileName)
    {
        JsonSerializer<DataContextDTO> dataContextDTOSerializer = new() { FileName = fileName };
        JsonSerializer<DataContext> dataContextSerializer = new() { FileName = fileName };

        DataContextDTO? dataContextDTO = null;
        DataContext? dataContext = null;

        try
        {
            dataContextDTO = dataContextDTOSerializer.Deserialize();
        }
        catch (FileNotFoundException)
        {
            dataContext = new(dataContextSerializer);
            dataContext.SaveChanges();
            services.AddSingleton(dataContext);
            return services;
        }
        catch (FileLoadException)
        {
            dataContext = new(dataContextSerializer);
            dataContext.SaveChanges();
            services.AddSingleton(dataContext);
            return services;
        }

        dataContext = new DataContext(dataContextSerializer)
        {
            Meetings = dataContextDTO.Meetings
        };

        services.AddSingleton(dataContext);
        return services;
    }
}

