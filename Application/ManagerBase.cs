using System.Collections.Generic;
using Application.Meetings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public class ManagerBase
{
    protected IServiceProvider Services { get; }
    protected IMediator Mediator { get; }

    public ManagerBase(IServiceProvider services)
    {
        Services = services;

        using var serviceScope = services.CreateScope();
        var provider = serviceScope.ServiceProvider;

        Mediator = provider.GetRequiredService<IMediator>();
    }
}