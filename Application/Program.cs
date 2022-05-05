// See https://aka.ms/new-console-template for more information

using Application;
using Application.DTOs;
using MediatR;
using Application.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Meetings;
using Application.Extensions;
using Application.Models;

internal class Program
{
    const string DATACONTEXT_FILE_NAME = "DataContext";

    static void Main(string[] args)
    {
        var host = createHostBuilder(args).Build();





        using var serviceScope = host.Services.CreateScope();
        var provider = serviceScope.ServiceProvider;
        var exp = provider.GetRequiredService<DataContext>();


        App app = new App(host.Services);
        app.run();
    }

    private static IHostBuilder createHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
                    services.AddMediatR(typeof(Create.Handler).Assembly)
                    .AddDataContext(DATACONTEXT_FILE_NAME)
                  );
    }

}