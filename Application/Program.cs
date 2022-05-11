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
        var host = CreateHostBuilder(args).Build();

        App app = new App(host.Services);
        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
                    services.AddMediatR(typeof(Create.Handler).Assembly)
                    .AddDataContext(DATACONTEXT_FILE_NAME)
                  );
    }

}