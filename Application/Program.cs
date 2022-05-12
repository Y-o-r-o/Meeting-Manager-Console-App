// See https://aka.ms/new-console-template for more information

using MediatR;
using Microsoft.Extensions.Hosting;
using Application.Meetings;
using Application.Extensions;

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